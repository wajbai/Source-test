namespace ACPP.Modules.Data_Utility
{
    partial class frmMigrationBOSCOPAC
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
            this.lblMessage = new DevExpress.XtraEditors.LabelControl();
            this.lblAcYearValue = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.glkpBOSCOPACProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvBOSCOPACActivity = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colActivityId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActivity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpBOSCOPACBranch = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvBOSCOPACHouse = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBranchId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnMigrate = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadXMLFile = new DevExpress.XtraEditors.SimpleButton();
            this.txtBasePath = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcBasePath = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProgressbar = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcitemAcYear = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAcYear = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lclblMessageInfo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBOSCOPACProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOSCOPACActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBOSCOPACBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOSCOPACHouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBasePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBasePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProgressbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcitemAcYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAcYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lclblMessageInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblMessage);
            this.layoutControl1.Controls.Add(this.lblAcYearValue);
            this.layoutControl1.Controls.Add(this.progressBar);
            this.layoutControl1.Controls.Add(this.glkpBOSCOPACProject);
            this.layoutControl1.Controls.Add(this.glkpBOSCOPACBranch);
            this.layoutControl1.Controls.Add(this.btnMigrate);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnLoadXMLFile);
            this.layoutControl1.Controls.Add(this.txtBasePath);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(1);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(788, 170, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(581, 176);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblMessage
            // 
            this.lblMessage.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.lblMessage.Location = new System.Drawing.Point(7, 105);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(564, 16);
            this.lblMessage.StyleController = this.layoutControl1;
            this.lblMessage.TabIndex = 9;
            this.lblMessage.Text = "Message";
            // 
            // lblAcYearValue
            // 
            this.lblAcYearValue.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.lblAcYearValue.Location = new System.Drawing.Point(129, 81);
            this.lblAcYearValue.Name = "lblAcYearValue";
            this.lblAcYearValue.Size = new System.Drawing.Size(118, 16);
            this.lblAcYearValue.StyleController = this.layoutControl1;
            this.lblAcYearValue.TabIndex = 8;
            this.lblAcYearValue.Text = "A/c Year From to To";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 125);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(567, 17);
            this.progressBar.StyleController = this.layoutControl1;
            this.progressBar.TabIndex = 27;
            // 
            // glkpBOSCOPACProject
            // 
            this.glkpBOSCOPACProject.Location = new System.Drawing.Point(125, 57);
            this.glkpBOSCOPACProject.Name = "glkpBOSCOPACProject";
            this.glkpBOSCOPACProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpBOSCOPACProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpBOSCOPACProject.Properties.NullText = "";
            this.glkpBOSCOPACProject.Properties.View = this.gvBOSCOPACActivity;
            this.glkpBOSCOPACProject.Properties.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.glkpBOSCOPACProject_Properties_CustomDisplayText);
            this.glkpBOSCOPACProject.Size = new System.Drawing.Size(446, 20);
            this.glkpBOSCOPACProject.StyleController = this.layoutControl1;
            this.glkpBOSCOPACProject.TabIndex = 10;
            
            // 
            // gvBOSCOPACActivity
            // 
            this.gvBOSCOPACActivity.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colActivityId,
            this.colActivity});
            this.gvBOSCOPACActivity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvBOSCOPACActivity.Name = "gvBOSCOPACActivity";
            this.gvBOSCOPACActivity.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvBOSCOPACActivity.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBOSCOPACActivity.OptionsSelection.MultiSelect = true;
            this.gvBOSCOPACActivity.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvBOSCOPACActivity.OptionsView.ShowGroupPanel = false;
            // 
            // colActivityId
            // 
            this.colActivityId.Caption = "ID";
            this.colActivityId.FieldName = "code";
            this.colActivityId.Name = "colActivityId";
            // 
            // colActivity
            // 
            this.colActivity.Caption = "Project";
            this.colActivity.FieldName = "activity";
            this.colActivity.Name = "colActivity";
            this.colActivity.Visible = true;
            this.colActivity.VisibleIndex = 1;
            // 
            // glkpBOSCOPACBranch
            // 
            this.glkpBOSCOPACBranch.Location = new System.Drawing.Point(125, 33);
            this.glkpBOSCOPACBranch.Name = "glkpBOSCOPACBranch";
            this.glkpBOSCOPACBranch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpBOSCOPACBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpBOSCOPACBranch.Properties.DisplayMember = "house";
            this.glkpBOSCOPACBranch.Properties.NullText = "";
            this.glkpBOSCOPACBranch.Properties.ValueMember = "code";
            this.glkpBOSCOPACBranch.Properties.View = this.gvBOSCOPACHouse;
            this.glkpBOSCOPACBranch.Size = new System.Drawing.Size(446, 20);
            this.glkpBOSCOPACBranch.StyleController = this.layoutControl1;
            this.glkpBOSCOPACBranch.TabIndex = 9;
            this.glkpBOSCOPACBranch.EditValueChanged += new System.EventHandler(this.glkpBOSCOPACBranch_EditValueChanged);
            // 
            // gvBOSCOPACHouse
            // 
            this.gvBOSCOPACHouse.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBranchId,
            this.colBranch,
            this.colRegion});
            this.gvBOSCOPACHouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvBOSCOPACHouse.Name = "gvBOSCOPACHouse";
            this.gvBOSCOPACHouse.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBOSCOPACHouse.OptionsView.ShowGroupPanel = false;
            // 
            // colBranchId
            // 
            this.colBranchId.Caption = "code";
            this.colBranchId.FieldName = "code";
            this.colBranchId.Name = "colBranchId";
            // 
            // colBranch
            // 
            this.colBranch.Caption = "House";
            this.colBranch.FieldName = "house";
            this.colBranch.Name = "colBranch";
            this.colBranch.Visible = true;
            this.colBranch.VisibleIndex = 0;
            // 
            // colRegion
            // 
            this.colRegion.Caption = "Region";
            this.colRegion.FieldName = "region";
            this.colRegion.Name = "colRegion";
            this.colRegion.Visible = true;
            this.colRegion.VisibleIndex = 1;
            // 
            // btnMigrate
            // 
            this.btnMigrate.Location = new System.Drawing.Point(418, 146);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(73, 23);
            this.btnMigrate.StyleController = this.layoutControl1;
            this.btnMigrate.TabIndex = 1;
            this.btnMigrate.Text = "&Migrate";
            this.btnMigrate.Click += new System.EventHandler(this.BtnMigrate_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(495, 146);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 23);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            // 
            // btnLoadXMLFile
            // 
            this.btnLoadXMLFile.Location = new System.Drawing.Point(489, 7);
            this.btnLoadXMLFile.Name = "btnLoadXMLFile";
            this.btnLoadXMLFile.Size = new System.Drawing.Size(82, 22);
            this.btnLoadXMLFile.StyleController = this.layoutControl1;
            this.btnLoadXMLFile.TabIndex = 0;
            this.btnLoadXMLFile.Text = "&Browse";
            this.btnLoadXMLFile.Click += new System.EventHandler(this.btnLoadXMLFile_Click);
            // 
            // txtBasePath
            // 
            this.txtBasePath.Location = new System.Drawing.Point(125, 7);
            this.txtBasePath.Name = "txtBasePath";
            this.txtBasePath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBasePath.Size = new System.Drawing.Size(360, 20);
            this.txtBasePath.StyleController = this.layoutControl1;
            this.txtBasePath.TabIndex = 3;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcBasePath,
            this.layoutControlItem2,
            this.emptySpaceItem3,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.lcBranch,
            this.lcProject,
            this.lcProgressbar,
            this.lcitemAcYear,
            this.lblAcYear,
            this.emptySpaceItem1,
            this.lclblMessageInfo});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(581, 176);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcBasePath
            // 
            this.lcBasePath.AllowHtmlStringInCaption = true;
            this.lcBasePath.Control = this.txtBasePath;
            this.lcBasePath.CustomizationFormText = "Base Path<color=\"red\"> *";
            this.lcBasePath.Location = new System.Drawing.Point(0, 0);
            this.lcBasePath.MaxSize = new System.Drawing.Size(482, 26);
            this.lcBasePath.MinSize = new System.Drawing.Size(482, 26);
            this.lcBasePath.Name = "lcBasePath";
            this.lcBasePath.Size = new System.Drawing.Size(482, 26);
            this.lcBasePath.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcBasePath.Text = "BOSCOPAC Base Path<color=\"red\"> *";
            this.lcBasePath.TextSize = new System.Drawing.Size(115, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnLoadXMLFile;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(482, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(86, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(86, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 140);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(411, 27);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(488, 139);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(83, 27);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(83, 27);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(83, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnMigrate;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(411, 139);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(77, 27);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(77, 27);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(77, 27);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lcBranch
            // 
            this.lcBranch.AllowHtmlStringInCaption = true;
            this.lcBranch.Control = this.glkpBOSCOPACBranch;
            this.lcBranch.CustomizationFormText = "BOSCOPAC Branch<color=\"red\"> *";
            this.lcBranch.Location = new System.Drawing.Point(0, 26);
            this.lcBranch.MaxSize = new System.Drawing.Size(568, 24);
            this.lcBranch.MinSize = new System.Drawing.Size(568, 24);
            this.lcBranch.Name = "lcBranch";
            this.lcBranch.Size = new System.Drawing.Size(571, 24);
            this.lcBranch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcBranch.Text = "BOSCOPAC Branch<color=\"red\"> *";
            this.lcBranch.TextSize = new System.Drawing.Size(115, 13);
            // 
            // lcProject
            // 
            this.lcProject.AllowHtmlStringInCaption = true;
            this.lcProject.Control = this.glkpBOSCOPACProject;
            this.lcProject.CustomizationFormText = "BOSCOPAC Project<color=\"red\"> *";
            this.lcProject.Location = new System.Drawing.Point(0, 50);
            this.lcProject.MaxSize = new System.Drawing.Size(568, 24);
            this.lcProject.MinSize = new System.Drawing.Size(568, 24);
            this.lcProject.Name = "lcProject";
            this.lcProject.Size = new System.Drawing.Size(571, 24);
            this.lcProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcProject.Text = "BOSCOPAC Project<color=\"red\"> *";
            this.lcProject.TextSize = new System.Drawing.Size(115, 13);
            // 
            // lcProgressbar
            // 
            this.lcProgressbar.Control = this.progressBar;
            this.lcProgressbar.CustomizationFormText = "lcProgressbar";
            this.lcProgressbar.Location = new System.Drawing.Point(0, 118);
            this.lcProgressbar.Name = "lcProgressbar";
            this.lcProgressbar.Size = new System.Drawing.Size(571, 21);
            this.lcProgressbar.Text = "lcProgressbar";
            this.lcProgressbar.TextSize = new System.Drawing.Size(0, 0);
            this.lcProgressbar.TextToControlDistance = 0;
            this.lcProgressbar.TextVisible = false;
            // 
            // lcitemAcYear
            // 
            this.lcitemAcYear.AllowHotTrack = false;
            this.lcitemAcYear.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lcitemAcYear.CustomizationFormText = "A/c Year";
            this.lcitemAcYear.Location = new System.Drawing.Point(0, 74);
            this.lcitemAcYear.MaxSize = new System.Drawing.Size(122, 24);
            this.lcitemAcYear.MinSize = new System.Drawing.Size(122, 24);
            this.lcitemAcYear.Name = "lcitemAcYear";
            this.lcitemAcYear.Size = new System.Drawing.Size(122, 24);
            this.lcitemAcYear.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcitemAcYear.Text = "A/c Year";
            this.lcitemAcYear.TextSize = new System.Drawing.Size(115, 13);
            // 
            // lblAcYear
            // 
            this.lblAcYear.Control = this.lblAcYearValue;
            this.lblAcYear.CustomizationFormText = "lblAcYear";
            this.lblAcYear.Location = new System.Drawing.Point(122, 74);
            this.lblAcYear.Name = "lblAcYear";
            this.lblAcYear.Size = new System.Drawing.Size(122, 24);
            this.lblAcYear.Text = "lblAcYear";
            this.lblAcYear.TextSize = new System.Drawing.Size(0, 0);
            this.lblAcYear.TextToControlDistance = 0;
            this.lblAcYear.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(244, 74);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(327, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lclblMessageInfo
            // 
            this.lclblMessageInfo.Control = this.lblMessage;
            this.lclblMessageInfo.CustomizationFormText = "lclblMessageInfo";
            this.lclblMessageInfo.Location = new System.Drawing.Point(0, 98);
            this.lclblMessageInfo.MaxSize = new System.Drawing.Size(568, 20);
            this.lclblMessageInfo.MinSize = new System.Drawing.Size(568, 20);
            this.lclblMessageInfo.Name = "lclblMessageInfo";
            this.lclblMessageInfo.Size = new System.Drawing.Size(571, 20);
            this.lclblMessageInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lclblMessageInfo.Text = "lclblMessageInfo";
            this.lclblMessageInfo.TextSize = new System.Drawing.Size(0, 0);
            this.lclblMessageInfo.TextToControlDistance = 0;
            this.lclblMessageInfo.TextVisible = false;
            // 
            // frmMigrationBOSCOPAC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 186);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmMigrationBOSCOPAC";
            this.Text = "Migration BOSCOPAC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMigrationBOSCOPAC_FormClosed);
            this.Load += new System.EventHandler(this.frmMigrationBOSCOPAC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBOSCOPACProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOSCOPACActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBOSCOPACBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOSCOPACHouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBasePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBasePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProgressbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcitemAcYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAcYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lclblMessageInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnMigrate;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnLoadXMLFile;
        private DevExpress.XtraEditors.TextEdit txtBasePath;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lcBasePath;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.GridLookUpEdit glkpBOSCOPACBranch;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBOSCOPACHouse;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchId;
        private DevExpress.XtraGrid.Columns.GridColumn colBranch;
        private DevExpress.XtraLayout.LayoutControlItem lcBranch;
        private DevExpress.XtraEditors.GridLookUpEdit glkpBOSCOPACProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBOSCOPACActivity;
        private DevExpress.XtraGrid.Columns.GridColumn colActivityId;
        private DevExpress.XtraGrid.Columns.GridColumn colActivity;
        private DevExpress.XtraLayout.LayoutControlItem lcProject;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
        private DevExpress.XtraLayout.LayoutControlItem lcProgressbar;
        private DevExpress.XtraLayout.SimpleLabelItem lcitemAcYear;
        private DevExpress.XtraEditors.LabelControl lblAcYearValue;
        private DevExpress.XtraLayout.LayoutControlItem lblAcYear;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LabelControl lblMessage;
        private DevExpress.XtraLayout.LayoutControlItem lclblMessageInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colRegion;
    }
}