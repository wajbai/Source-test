namespace ACPP.Modules.Inventory
{
    partial class frmLocationBlock
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
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnBlockSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtBlock = new DevExpress.XtraEditors.TextEdit();
            this.glkpBlockBuilding = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBuildingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuildingName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblBuuilding = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBlockBuilding.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBuuilding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.btnClose);
            this.layoutControl3.Controls.Add(this.btnBlockSave);
            this.layoutControl3.Controls.Add(this.txtBlock);
            this.layoutControl3.Controls.Add(this.glkpBlockBuilding);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(5, 5);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(274, 81);
            this.layoutControl3.TabIndex = 1;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(212, 56);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 22);
            this.btnClose.StyleController = this.layoutControl3;
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnBlockSave
            // 
            this.btnBlockSave.Location = new System.Drawing.Point(151, 56);
            this.btnBlockSave.Name = "btnBlockSave";
            this.btnBlockSave.Size = new System.Drawing.Size(57, 22);
            this.btnBlockSave.StyleController = this.layoutControl3;
            this.btnBlockSave.TabIndex = 7;
            this.btnBlockSave.Text = "&Save";
            this.btnBlockSave.Click += new System.EventHandler(this.btnBlockSave_Click);
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(51, 29);
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBlock.Properties.MaxLength = 50;
            this.txtBlock.Size = new System.Drawing.Size(221, 20);
            this.txtBlock.StyleController = this.layoutControl3;
            this.txtBlock.TabIndex = 6;
            this.txtBlock.Leave += new System.EventHandler(this.txtBlock_Leave);
            // 
            // glkpBlockBuilding
            // 
            this.glkpBlockBuilding.Location = new System.Drawing.Point(51, 2);
            this.glkpBlockBuilding.Name = "glkpBlockBuilding";
            this.glkpBlockBuilding.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpBlockBuilding.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpBlockBuilding.Properties.NullText = "";
            this.glkpBlockBuilding.Properties.PopupFormSize = new System.Drawing.Size(312, 0);
            this.glkpBlockBuilding.Properties.View = this.gridLookUpEdit2View;
            this.glkpBlockBuilding.Size = new System.Drawing.Size(221, 20);
            this.glkpBlockBuilding.StyleController = this.layoutControl3;
            this.glkpBlockBuilding.TabIndex = 5;
            this.glkpBlockBuilding.Leave += new System.EventHandler(this.glkpBlockBuilding_Leave);
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBuildingId,
            this.colBuildingName});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // colBuildingId
            // 
            this.colBuildingId.Caption = "BuildingId";
            this.colBuildingId.FieldName = "BUILDING_ID";
            this.colBuildingId.Name = "colBuildingId";
            // 
            // colBuildingName
            // 
            this.colBuildingName.Caption = "Building";
            this.colBuildingName.FieldName = "BUILDING_NAME";
            this.colBuildingName.Name = "colBuildingName";
            this.colBuildingName.Visible = true;
            this.colBuildingName.VisibleIndex = 0;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem3,
            this.lblBuuilding,
            this.lblName,
            this.layoutControlItem9,
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(274, 81);
            this.layoutControlGroup3.Text = "layoutControlGroup3";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 54);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(149, 27);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblBuuilding
            // 
            this.lblBuuilding.AllowHtmlStringInCaption = true;
            this.lblBuuilding.Control = this.glkpBlockBuilding;
            this.lblBuuilding.CustomizationFormText = "Building";
            this.lblBuuilding.Location = new System.Drawing.Point(0, 0);
            this.lblBuuilding.Name = "lblBuuilding";
            this.lblBuuilding.Size = new System.Drawing.Size(274, 27);
            this.lblBuuilding.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblBuuilding.Text = "Building <color=red>*";
            this.lblBuuilding.TextSize = new System.Drawing.Size(45, 13);
            // 
            // lblName
            // 
            this.lblName.AllowHtmlStringInCaption = true;
            this.lblName.Control = this.txtBlock;
            this.lblName.CustomizationFormText = "Name";
            this.lblName.Location = new System.Drawing.Point(0, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(274, 27);
            this.lblName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblName.Text = "Name <color=red>*";
            this.lblName.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnBlockSave;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(149, 54);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(61, 27);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(210, 54);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(64, 27);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmLocationBlock
            // 
            this.AcceptButton = this.btnBlockSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 91);
            this.Controls.Add(this.layoutControl3);
            this.Name = "frmLocationBlock";
            this.Text = "Block";
            this.Load += new System.EventHandler(this.frmLocationBlock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBlockBuilding.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBuuilding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraEditors.SimpleButton btnBlockSave;
        private DevExpress.XtraEditors.TextEdit txtBlock;
        private DevExpress.XtraEditors.GridLookUpEdit glkpBlockBuilding;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn colBuildingId;
        private DevExpress.XtraGrid.Columns.GridColumn colBuildingName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem lblBuuilding;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}