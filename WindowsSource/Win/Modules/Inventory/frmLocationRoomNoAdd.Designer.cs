namespace ACPP.Modules.Inventory
{
    partial class frmLocationRoomNo
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
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnRoomSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtRoomNo = new DevExpress.XtraEditors.TextEdit();
            this.glkpRoomFloor = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit4View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFloorId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFloor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpRoomBlock = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBlockId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBlockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblBlock = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFloor = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRoomNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoomNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRoomFloor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit4View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRoomBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFloor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRoomNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.btnClose);
            this.layoutControl5.Controls.Add(this.btnRoomSave);
            this.layoutControl5.Controls.Add(this.txtRoomNo);
            this.layoutControl5.Controls.Add(this.glkpRoomFloor);
            this.layoutControl5.Controls.Add(this.glkpRoomBlock);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(5, 5);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup5;
            this.layoutControl5.Size = new System.Drawing.Size(356, 107);
            this.layoutControl5.TabIndex = 1;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(297, 83);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(57, 22);
            this.btnClose.StyleController = this.layoutControl5;
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRoomSave
            // 
            this.btnRoomSave.Location = new System.Drawing.Point(236, 83);
            this.btnRoomSave.Name = "btnRoomSave";
            this.btnRoomSave.Size = new System.Drawing.Size(57, 22);
            this.btnRoomSave.StyleController = this.layoutControl5;
            this.btnRoomSave.TabIndex = 9;
            this.btnRoomSave.Text = "&Save";
            this.btnRoomSave.Click += new System.EventHandler(this.btnRoomSave_Click);
            // 
            // txtRoomNo
            // 
            this.txtRoomNo.Location = new System.Drawing.Point(57, 56);
            this.txtRoomNo.Name = "txtRoomNo";
            this.txtRoomNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtRoomNo.Properties.MaxLength = 50;
            this.txtRoomNo.Size = new System.Drawing.Size(297, 20);
            this.txtRoomNo.StyleController = this.layoutControl5;
            this.txtRoomNo.TabIndex = 8;
            this.txtRoomNo.Leave += new System.EventHandler(this.txtRoomNo_Leave);
            // 
            // glkpRoomFloor
            // 
            this.glkpRoomFloor.Location = new System.Drawing.Point(57, 29);
            this.glkpRoomFloor.Name = "glkpRoomFloor";
            this.glkpRoomFloor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpRoomFloor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpRoomFloor.Properties.NullText = "";
            this.glkpRoomFloor.Properties.PopupFormSize = new System.Drawing.Size(306, 0);
            this.glkpRoomFloor.Properties.View = this.gridLookUpEdit4View;
            this.glkpRoomFloor.Size = new System.Drawing.Size(297, 20);
            this.glkpRoomFloor.StyleController = this.layoutControl5;
            this.glkpRoomFloor.TabIndex = 7;
            this.glkpRoomFloor.Leave += new System.EventHandler(this.glkpRoomFloor_Leave);
            // 
            // gridLookUpEdit4View
            // 
            this.gridLookUpEdit4View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFloorId,
            this.colFloor});
            this.gridLookUpEdit4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit4View.Name = "gridLookUpEdit4View";
            this.gridLookUpEdit4View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit4View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit4View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit4View.OptionsView.ShowIndicator = false;
            // 
            // colFloorId
            // 
            this.colFloorId.Caption = "FloorId";
            this.colFloorId.FieldName = "FLOOR_ID";
            this.colFloorId.Name = "colFloorId";
            // 
            // colFloor
            // 
            this.colFloor.Caption = "Floor";
            this.colFloor.FieldName = "FLOOR_NAME";
            this.colFloor.Name = "colFloor";
            this.colFloor.Visible = true;
            this.colFloor.VisibleIndex = 0;
            // 
            // glkpRoomBlock
            // 
            this.glkpRoomBlock.Location = new System.Drawing.Point(57, 2);
            this.glkpRoomBlock.Name = "glkpRoomBlock";
            this.glkpRoomBlock.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpRoomBlock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpRoomBlock.Properties.NullText = "";
            this.glkpRoomBlock.Properties.PopupFormSize = new System.Drawing.Size(306, 0);
            this.glkpRoomBlock.Properties.View = this.gridView6;
            this.glkpRoomBlock.Size = new System.Drawing.Size(297, 20);
            this.glkpRoomBlock.StyleController = this.layoutControl5;
            this.glkpRoomBlock.TabIndex = 6;
            this.glkpRoomBlock.EditValueChanged += new System.EventHandler(this.glkpRoomBlock_EditValueChanged);
            this.glkpRoomBlock.Leave += new System.EventHandler(this.glkpRoomBlock_Leave);
            // 
            // gridView6
            // 
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBlockId,
            this.colBlockName});
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowColumnHeaders = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            this.gridView6.OptionsView.ShowIndicator = false;
            // 
            // colBlockId
            // 
            this.colBlockId.Caption = "BlockId";
            this.colBlockId.FieldName = "BLOCK_ID";
            this.colBlockId.Name = "colBlockId";
            // 
            // colBlockName
            // 
            this.colBlockName.Caption = "Block";
            this.colBlockName.FieldName = "BLOCK_NAME";
            this.colBlockName.Name = "colBlockName";
            this.colBlockName.Visible = true;
            this.colBlockName.VisibleIndex = 0;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblBlock,
            this.lblFloor,
            this.lblRoomNo,
            this.layoutControlItem20,
            this.layoutControlItem1,
            this.emptySpaceItem5});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(356, 107);
            this.layoutControlGroup5.Text = "layoutControlGroup5";
            this.layoutControlGroup5.TextVisible = false;
            // 
            // lblBlock
            // 
            this.lblBlock.AllowHtmlStringInCaption = true;
            this.lblBlock.Control = this.glkpRoomBlock;
            this.lblBlock.CustomizationFormText = "Block";
            this.lblBlock.Location = new System.Drawing.Point(0, 0);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(356, 27);
            this.lblBlock.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblBlock.Text = "Block <color=red>*";
            this.lblBlock.TextSize = new System.Drawing.Size(52, 13);
            // 
            // lblFloor
            // 
            this.lblFloor.AllowHtmlStringInCaption = true;
            this.lblFloor.Control = this.glkpRoomFloor;
            this.lblFloor.CustomizationFormText = "Floor";
            this.lblFloor.Location = new System.Drawing.Point(0, 27);
            this.lblFloor.Name = "lblFloor";
            this.lblFloor.Size = new System.Drawing.Size(356, 27);
            this.lblFloor.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblFloor.Text = "Floor <color=red>*";
            this.lblFloor.TextSize = new System.Drawing.Size(52, 13);
            // 
            // lblRoomNo
            // 
            this.lblRoomNo.AllowHtmlStringInCaption = true;
            this.lblRoomNo.Control = this.txtRoomNo;
            this.lblRoomNo.CustomizationFormText = "Name";
            this.lblRoomNo.Location = new System.Drawing.Point(0, 54);
            this.lblRoomNo.Name = "lblRoomNo";
            this.lblRoomNo.Size = new System.Drawing.Size(356, 27);
            this.lblRoomNo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblRoomNo.Text = "Room No <color=red>*";
            this.lblRoomNo.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.btnRoomSave;
            this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
            this.layoutControlItem20.Location = new System.Drawing.Point(234, 81);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(61, 26);
            this.layoutControlItem20.Text = "layoutControlItem20";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(295, 81);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(61, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 81);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(234, 26);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmLocationRoomNo
            // 
            this.AcceptButton = this.btnRoomSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(366, 117);
            this.Controls.Add(this.layoutControl5);
            this.Name = "frmLocationRoomNo";
            this.Text = "Room No / Section";
            this.Load += new System.EventHandler(this.frmLocationRoomNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRoomNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRoomFloor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit4View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRoomBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFloor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRoomNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraEditors.SimpleButton btnRoomSave;
        private DevExpress.XtraEditors.TextEdit txtRoomNo;
        private DevExpress.XtraEditors.GridLookUpEdit glkpRoomFloor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit4View;
        private DevExpress.XtraEditors.GridLookUpEdit glkpRoomBlock;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem lblBlock;
        private DevExpress.XtraLayout.LayoutControlItem lblFloor;
        private DevExpress.XtraLayout.LayoutControlItem lblRoomNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colFloorId;
        private DevExpress.XtraGrid.Columns.GridColumn colFloor;
        private DevExpress.XtraGrid.Columns.GridColumn colBlockId;
        private DevExpress.XtraGrid.Columns.GridColumn colBlockName;
    }
}