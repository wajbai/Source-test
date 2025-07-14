namespace ACPP.Modules.Inventory
{
    partial class frmLocationFloor
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
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnFloorSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtFloor = new DevExpress.XtraEditors.TextEdit();
            this.glkpFloorBlock = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBlockId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBlockName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblBlock = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFloor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpFloorBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.btnClose);
            this.layoutControl4.Controls.Add(this.btnFloorSave);
            this.layoutControl4.Controls.Add(this.txtFloor);
            this.layoutControl4.Controls.Add(this.glkpFloorBlock);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(5, 5);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(312, 81);
            this.layoutControl4.TabIndex = 1;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(242, 56);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 22);
            this.btnClose.StyleController = this.layoutControl4;
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFloorSave
            // 
            this.btnFloorSave.Location = new System.Drawing.Point(169, 56);
            this.btnFloorSave.Name = "btnFloorSave";
            this.btnFloorSave.Size = new System.Drawing.Size(69, 22);
            this.btnFloorSave.StyleController = this.layoutControl4;
            this.btnFloorSave.TabIndex = 8;
            this.btnFloorSave.Text = "&Save";
            this.btnFloorSave.Click += new System.EventHandler(this.btnFloorSave_Click);
            // 
            // txtFloor
            // 
            this.txtFloor.Location = new System.Drawing.Point(39, 29);
            this.txtFloor.Name = "txtFloor";
            this.txtFloor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtFloor.Properties.MaxLength = 50;
            this.txtFloor.Size = new System.Drawing.Size(271, 20);
            this.txtFloor.StyleController = this.layoutControl4;
            this.txtFloor.TabIndex = 7;
            this.txtFloor.Leave += new System.EventHandler(this.txtFloor_Leave);
            // 
            // glkpFloorBlock
            // 
            this.glkpFloorBlock.Location = new System.Drawing.Point(39, 2);
            this.glkpFloorBlock.Name = "glkpFloorBlock";
            this.glkpFloorBlock.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpFloorBlock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpFloorBlock.Properties.NullText = "";
            this.glkpFloorBlock.Properties.PopupFormSize = new System.Drawing.Size(321, 0);
            this.glkpFloorBlock.Properties.View = this.gridLookUpEdit3View;
            this.glkpFloorBlock.Size = new System.Drawing.Size(271, 20);
            this.glkpFloorBlock.StyleController = this.layoutControl4;
            this.glkpFloorBlock.TabIndex = 6;
            this.glkpFloorBlock.Leave += new System.EventHandler(this.glkpFloorBlock_Leave);
            // 
            // gridLookUpEdit3View
            // 
            this.gridLookUpEdit3View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBlockId,
            this.colBlockName});
            this.gridLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit3View.Name = "gridLookUpEdit3View";
            this.gridLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit3View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit3View.OptionsView.ShowIndicator = false;
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
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem4,
            this.lblBlock,
            this.lblName,
            this.layoutControlItem14,
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(312, 81);
            this.layoutControlGroup4.Text = "layoutControlGroup4";
            this.layoutControlGroup4.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 54);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(167, 27);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblBlock
            // 
            this.lblBlock.AllowHtmlStringInCaption = true;
            this.lblBlock.Control = this.glkpFloorBlock;
            this.lblBlock.CustomizationFormText = "Block";
            this.lblBlock.Location = new System.Drawing.Point(0, 0);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(312, 27);
            this.lblBlock.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblBlock.Text = "Block <color=red>*";
            this.lblBlock.TextSize = new System.Drawing.Size(33, 13);
            // 
            // lblName
            // 
            this.lblName.Control = this.txtFloor;
            this.lblName.CustomizationFormText = "Name";
            this.lblName.Location = new System.Drawing.Point(0, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(312, 27);
            this.lblName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblName.Text = "Name";
            this.lblName.TextSize = new System.Drawing.Size(33, 13);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnFloorSave;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(167, 54);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(73, 27);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(240, 54);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(72, 27);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmLocationFloor
            // 
            this.AcceptButton = this.btnFloorSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(322, 91);
            this.Controls.Add(this.layoutControl4);
            this.Name = "frmLocationFloor";
            this.Text = "Floor";
            this.Load += new System.EventHandler(this.frmLocationFloor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFloor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpFloorBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraEditors.SimpleButton btnFloorSave;
        private DevExpress.XtraEditors.TextEdit txtFloor;
        private DevExpress.XtraEditors.GridLookUpEdit glkpFloorBlock;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit3View;
        private DevExpress.XtraGrid.Columns.GridColumn colBlockId;
        private DevExpress.XtraGrid.Columns.GridColumn colBlockName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem lblBlock;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}