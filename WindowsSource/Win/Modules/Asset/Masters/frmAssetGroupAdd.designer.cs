namespace ACPP.Modules.Asset.Masters
{
    partial class frmAssetGroupAdd
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glpUnderGroup = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colgroup_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtDepreciationPercent = new DevExpress.XtraEditors.TextEdit();
            this.glkDepreciationMethod = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtAssGroupName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDepreciationPercent = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblMethod = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblUnderGroup = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glpUnderGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepreciationPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDepreciationMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssGroupName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnderGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glpUnderGroup);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.txtDepreciationPercent);
            this.layoutControl1.Controls.Add(this.glkDepreciationMethod);
            this.layoutControl1.Controls.Add(this.txtAssGroupName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(283, 350, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(351, 125);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // glpUnderGroup
            // 
            this.glpUnderGroup.EditValue = "Under Group";
            this.glpUnderGroup.Location = new System.Drawing.Point(87, 2);
            this.glpUnderGroup.Name = "glpUnderGroup";
            this.glpUnderGroup.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.glpUnderGroup.Properties.AppearanceFocused.Options.UseFont = true;
            this.glpUnderGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glpUnderGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glpUnderGroup.Properties.NullText = "";
            this.glpUnderGroup.Properties.PopupFormSize = new System.Drawing.Size(90, 80);
            this.glpUnderGroup.Properties.View = this.gridView1;
            this.glpUnderGroup.Size = new System.Drawing.Size(262, 20);
            this.glpUnderGroup.StyleController = this.layoutControl1;
            this.glpUnderGroup.TabIndex = 12;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colgroup_id,
            this.col_name});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colgroup_id
            // 
            this.colgroup_id.Caption = "colgroup_id";
            this.colgroup_id.FieldName = "GROUP_ID";
            this.colgroup_id.Name = "colgroup_id";
            // 
            // col_name
            // 
            this.col_name.Caption = "col_name";
            this.col_name.FieldName = "NAME";
            this.col_name.Name = "col_name";
            this.col_name.Visible = true;
            this.col_name.VisibleIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(215, 97);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.StyleController = this.layoutControl1;
            toolTipTitleItem1.Appearance.Image = global::ACPP.Properties.Resources.bullet;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::ACPP.Properties.Resources.bullet;
            toolTipTitleItem1.Text = "Save (Alt+S)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "            Click on this to Save Group details";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.btnSave.SuperTip = superToolTip1;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(284, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 25);
            this.btnCancel.StyleController = this.layoutControl1;
            toolTipTitleItem2.Appearance.Image = global::ACPP.Properties.Resources.bullet;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = global::ACPP.Properties.Resources.bullet;
            toolTipTitleItem2.Text = "Close (Alt+C)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "            Click on this to Close the form";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.btnCancel.SuperTip = superToolTip2;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtDepreciationPercent
            // 
            this.txtDepreciationPercent.Location = new System.Drawing.Point(86, 72);
            this.txtDepreciationPercent.Name = "txtDepreciationPercent";
            this.txtDepreciationPercent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtDepreciationPercent.Properties.Mask.EditMask = "d";
            this.txtDepreciationPercent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDepreciationPercent.Properties.MaxLength = 3;
            this.txtDepreciationPercent.Size = new System.Drawing.Size(80, 20);
            this.txtDepreciationPercent.StyleController = this.layoutControl1;
            this.txtDepreciationPercent.TabIndex = 8;
            this.txtDepreciationPercent.Leave += new System.EventHandler(this.txtDepreciationPercent_Leave);
            // 
            // glkDepreciationMethod
            // 
            this.glkDepreciationMethod.EditValue = "";
            this.glkDepreciationMethod.Location = new System.Drawing.Point(86, 49);
            this.glkDepreciationMethod.Name = "glkDepreciationMethod";
            this.glkDepreciationMethod.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.glkDepreciationMethod.Properties.AppearanceFocused.Options.UseFont = true;
            this.glkDepreciationMethod.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkDepreciationMethod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.glkDepreciationMethod.Properties.NullText = "";
            this.glkDepreciationMethod.Properties.PopupFormSize = new System.Drawing.Size(90, 80);
            this.glkDepreciationMethod.Properties.View = this.gridLookUpEdit1View;
            this.glkDepreciationMethod.Size = new System.Drawing.Size(263, 20);
            this.glkDepreciationMethod.StyleController = this.layoutControl1;
            this.glkDepreciationMethod.TabIndex = 6;
            this.glkDepreciationMethod.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkDepreciationMethod_ButtonClick);
            this.glkDepreciationMethod.Leave += new System.EventHandler(this.glkDepreciationMethod_Leave);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colNAME});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "DEP_ID";
            this.colId.Name = "colId";
            // 
            // colNAME
            // 
            this.colNAME.Caption = "gridColumn2";
            this.colNAME.FieldName = "NAME";
            this.colNAME.Name = "colNAME";
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 0;
            // 
            // txtAssGroupName
            // 
            this.txtAssGroupName.Location = new System.Drawing.Point(87, 26);
            this.txtAssGroupName.Name = "txtAssGroupName";
            this.txtAssGroupName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAssGroupName.Properties.MaxLength = 50;
            this.txtAssGroupName.Size = new System.Drawing.Size(262, 20);
            this.txtAssGroupName.StyleController = this.layoutControl1;
            this.txtAssGroupName.TabIndex = 4;
            this.txtAssGroupName.Leave += new System.EventHandler(this.txtAssGroupName_Leave_1);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblName,
            this.lblDepreciationPercent,
            this.lblMethod,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.lblUnderGroup});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(351, 125);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblName
            // 
            this.lblName.AllowHtmlStringInCaption = true;
            this.lblName.Control = this.txtAssGroupName;
            this.lblName.CustomizationFormText = "Name <Color=Red>*";
            this.lblName.Location = new System.Drawing.Point(0, 24);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 2, 2, 3);
            this.lblName.Size = new System.Drawing.Size(351, 25);
            this.lblName.Text = "Name <Color=Red>*";
            this.lblName.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblDepreciationPercent
            // 
            this.lblDepreciationPercent.AllowHtmlStringInCaption = true;
            this.lblDepreciationPercent.Control = this.txtDepreciationPercent;
            this.lblDepreciationPercent.CustomizationFormText = "Depreciation % <Color=Red>*";
            this.lblDepreciationPercent.Location = new System.Drawing.Point(0, 72);
            this.lblDepreciationPercent.Name = "lblDepreciationPercent";
            this.lblDepreciationPercent.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 1, 0, 3);
            this.lblDepreciationPercent.Size = new System.Drawing.Size(167, 23);
            this.lblDepreciationPercent.Text = "Depreciation % <Color=Red>*";
            this.lblDepreciationPercent.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblMethod
            // 
            this.lblMethod.AllowHtmlStringInCaption = true;
            this.lblMethod.Control = this.glkDepreciationMethod;
            this.lblMethod.CustomizationFormText = "Method <Color=Red>*";
            this.lblMethod.Location = new System.Drawing.Point(0, 49);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 0, 3);
            this.lblMethod.Size = new System.Drawing.Size(351, 23);
            this.lblMethod.Text = "Method <Color=Red>*";
            this.lblMethod.TextSize = new System.Drawing.Size(83, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(167, 72);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(184, 23);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 95);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(213, 30);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(282, 95);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(213, 95);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(69, 30);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblUnderGroup
            // 
            this.lblUnderGroup.AllowHtmlStringInCaption = true;
            this.lblUnderGroup.Control = this.glpUnderGroup;
            this.lblUnderGroup.CustomizationFormText = "UnderGroup <Color=Red>*";
            this.lblUnderGroup.Location = new System.Drawing.Point(0, 0);
            this.lblUnderGroup.MaxSize = new System.Drawing.Size(0, 24);
            this.lblUnderGroup.MinSize = new System.Drawing.Size(140, 24);
            this.lblUnderGroup.Name = "lblUnderGroup";
            this.lblUnderGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 2, 2, 3);
            this.lblUnderGroup.Size = new System.Drawing.Size(351, 24);
            this.lblUnderGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblUnderGroup.Text = "Under Group <Color=Red>*";
            this.lblUnderGroup.TextSize = new System.Drawing.Size(83, 13);
            // 
            // frmAssetGroupAdd
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(361, 135);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetGroupAdd";
            this.Text = "Group";
            this.Load += new System.EventHandler(this.frmAssetGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glpUnderGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepreciationPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDepreciationMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssGroupName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnderGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtDepreciationPercent;
        private DevExpress.XtraEditors.GridLookUpEdit glkDepreciationMethod;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit txtAssGroupName;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem lblDepreciationPercent;
        private DevExpress.XtraLayout.LayoutControlItem lblMethod;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME;
        private DevExpress.XtraEditors.GridLookUpEdit glpUnderGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lblUnderGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colgroup_id;
        private DevExpress.XtraGrid.Columns.GridColumn col_name;
    }
}