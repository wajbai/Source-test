namespace ACPP.Modules.Inventory
{
    partial class frmLocationBuilding
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
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuildingSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuildingName = new DevExpress.XtraEditors.TextEdit();
            this.glkpBuildingArea = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAreaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAreaName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblArea = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuildingName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBuildingArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.btnClose);
            this.layoutControl2.Controls.Add(this.btnBuildingSave);
            this.layoutControl2.Controls.Add(this.txtBuildingName);
            this.layoutControl2.Controls.Add(this.glkpBuildingArea);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(5, 5);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(274, 80);
            this.layoutControl2.TabIndex = 1;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(208, 56);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 22);
            this.btnClose.StyleController = this.layoutControl2;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnBuildingSave
            // 
            this.btnBuildingSave.Location = new System.Drawing.Point(148, 56);
            this.btnBuildingSave.Name = "btnBuildingSave";
            this.btnBuildingSave.Size = new System.Drawing.Size(56, 22);
            this.btnBuildingSave.StyleController = this.layoutControl2;
            this.btnBuildingSave.TabIndex = 6;
            this.btnBuildingSave.Text = "&Save";
            this.btnBuildingSave.Click += new System.EventHandler(this.btnBuildingSave_Click);
            // 
            // txtBuildingName
            // 
            this.txtBuildingName.Location = new System.Drawing.Point(42, 29);
            this.txtBuildingName.Name = "txtBuildingName";
            this.txtBuildingName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBuildingName.Properties.MaxLength = 50;
            this.txtBuildingName.Size = new System.Drawing.Size(230, 20);
            this.txtBuildingName.StyleController = this.layoutControl2;
            this.txtBuildingName.TabIndex = 5;
            this.txtBuildingName.Leave += new System.EventHandler(this.txtBuildingName_Leave);
            // 
            // glkpBuildingArea
            // 
            this.glkpBuildingArea.Location = new System.Drawing.Point(42, 2);
            this.glkpBuildingArea.Name = "glkpBuildingArea";
            this.glkpBuildingArea.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpBuildingArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpBuildingArea.Properties.NullText = "";
            this.glkpBuildingArea.Properties.PopupFormSize = new System.Drawing.Size(321, 0);
            this.glkpBuildingArea.Properties.View = this.gridLookUpEdit1View;
            this.glkpBuildingArea.Size = new System.Drawing.Size(230, 20);
            this.glkpBuildingArea.StyleController = this.layoutControl2;
            this.glkpBuildingArea.TabIndex = 4;
            this.glkpBuildingArea.Leave += new System.EventHandler(this.glkpBuildingArea_Leave);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAreaId,
            this.colAreaName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colAreaId
            // 
            this.colAreaId.Caption = "AreaId";
            this.colAreaId.FieldName = "AREA_ID";
            this.colAreaId.Name = "colAreaId";
            // 
            // colAreaName
            // 
            this.colAreaName.Caption = "Area";
            this.colAreaName.FieldName = "AREA_NAME";
            this.colAreaName.Name = "colAreaName";
            this.colAreaName.Visible = true;
            this.colAreaName.VisibleIndex = 0;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.lblArea,
            this.txtName,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(274, 80);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 54);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(146, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblArea
            // 
            this.lblArea.AllowHtmlStringInCaption = true;
            this.lblArea.Control = this.glkpBuildingArea;
            this.lblArea.CustomizationFormText = "Area";
            this.lblArea.Location = new System.Drawing.Point(0, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(274, 27);
            this.lblArea.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblArea.Text = "Area <color=red>*";
            this.lblArea.TextSize = new System.Drawing.Size(36, 13);
            // 
            // txtName
            // 
            this.txtName.AllowHtmlStringInCaption = true;
            this.txtName.Control = this.txtBuildingName;
            this.txtName.CustomizationFormText = "Name";
            this.txtName.Location = new System.Drawing.Point(0, 27);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(274, 27);
            this.txtName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.txtName.Text = "Name <color=red>*";
            this.txtName.TextSize = new System.Drawing.Size(36, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnBuildingSave;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(146, 54);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(60, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(206, 54);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(68, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmLocationBuilding
            // 
            this.AcceptButton = this.btnBuildingSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 90);
            this.Controls.Add(this.layoutControl2);
            this.Name = "frmLocationBuilding";
            this.Text = "Building";
            this.Load += new System.EventHandler(this.frmLocationBuilding_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBuildingName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBuildingArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.SimpleButton btnBuildingSave;
        private DevExpress.XtraEditors.TextEdit txtBuildingName;
        private DevExpress.XtraEditors.GridLookUpEdit glkpBuildingArea;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colAreaId;
        private DevExpress.XtraGrid.Columns.GridColumn colAreaName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblArea;
        private DevExpress.XtraLayout.LayoutControlItem txtName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}