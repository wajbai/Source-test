namespace AcMEDSync.View
{
    partial class frmImportHeadofficeMasters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportHeadofficeMasters));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnReadXML = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.pgSynchronizationProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.rgSynMode = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciSynMode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciPath = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciElipse = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciEmptySpace = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgProgress = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem4 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnReadXML.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgSynchronizationProgress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSynMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSynMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciElipse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEmptySpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnReadXML);
            this.layoutControl1.Controls.Add(this.txtPath);
            this.layoutControl1.Controls.Add(this.pgSynchronizationProgress);
            this.layoutControl1.Controls.Add(this.btnImport);
            this.layoutControl1.Controls.Add(this.rgSynMode);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(716, 1, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnReadXML
            // 
            resources.ApplyResources(this.btnReadXML, "btnReadXML");
            this.btnReadXML.Name = "btnReadXML";
            this.btnReadXML.Properties.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("btnReadXML.Properties.Appearance.BackColor")));
            this.btnReadXML.Properties.Appearance.Options.UseBackColor = true;
            this.btnReadXML.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnReadXML.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnReadXML.StyleController = this.layoutControl1;
            this.btnReadXML.Click += new System.EventHandler(this.btnReadXML_Click);
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.Name = "txtPath";
            this.txtPath.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("txtPath.Properties.Appearance.Font")));
            this.txtPath.Properties.Appearance.Options.UseFont = true;
            this.txtPath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPath.Properties.ReadOnly = true;
            this.txtPath.StyleController = this.layoutControl1;
            // 
            // pgSynchronizationProgress
            // 
            resources.ApplyResources(this.pgSynchronizationProgress, "pgSynchronizationProgress");
            this.pgSynchronizationProgress.Name = "pgSynchronizationProgress";
            this.pgSynchronizationProgress.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.pgSynchronizationProgress.Properties.ShowTitle = true;
            this.pgSynchronizationProgress.StyleController = this.layoutControl1;
            // 
            // btnImport
            // 
            resources.ApplyResources(this.btnImport, "btnImport");
            this.btnImport.Name = "btnImport";
            this.btnImport.StyleController = this.layoutControl1;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // rgSynMode
            // 
            resources.ApplyResources(this.rgSynMode, "rgSynMode");
            this.rgSynMode.Name = "rgSynMode";
            this.rgSynMode.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rgSynMode.Properties.Appearance.Font")));
            this.rgSynMode.Properties.Appearance.Options.UseFont = true;
            this.rgSynMode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgSynMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgSynMode.Properties.Items"))), resources.GetString("rgSynMode.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgSynMode.Properties.Items2"))), resources.GetString("rgSynMode.Properties.Items3"))});
            this.rgSynMode.StyleController = this.layoutControl1;
            this.rgSynMode.SelectedIndexChanged += new System.EventHandler(this.rgSynMode_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.lcgProgress,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(410, 192);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciSynMode,
            this.lciPath,
            this.lciElipse,
            this.lciEmptySpace});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup2.Size = new System.Drawing.Size(410, 67);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // lciSynMode
            // 
            this.lciSynMode.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lciSynMode.AppearanceItemCaption.Font")));
            this.lciSynMode.AppearanceItemCaption.Options.UseFont = true;
            this.lciSynMode.Control = this.rgSynMode;
            resources.ApplyResources(this.lciSynMode, "lciSynMode");
            this.lciSynMode.Location = new System.Drawing.Point(0, 0);
            this.lciSynMode.MaxSize = new System.Drawing.Size(396, 34);
            this.lciSynMode.MinSize = new System.Drawing.Size(396, 34);
            this.lciSynMode.Name = "lciSynMode";
            this.lciSynMode.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciSynMode.Size = new System.Drawing.Size(398, 34);
            this.lciSynMode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciSynMode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 5);
            this.lciSynMode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciSynMode.TextSize = new System.Drawing.Size(90, 16);
            this.lciSynMode.TextToControlDistance = 5;
            // 
            // lciPath
            // 
            this.lciPath.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lciPath.AppearanceItemCaption.Font")));
            this.lciPath.AppearanceItemCaption.Options.UseFont = true;
            this.lciPath.Control = this.txtPath;
            resources.ApplyResources(this.lciPath, "lciPath");
            this.lciPath.Location = new System.Drawing.Point(66, 34);
            this.lciPath.MaxSize = new System.Drawing.Size(278, 21);
            this.lciPath.MinSize = new System.Drawing.Size(278, 21);
            this.lciPath.Name = "lciPath";
            this.lciPath.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciPath.Size = new System.Drawing.Size(278, 21);
            this.lciPath.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciPath.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciPath.TextSize = new System.Drawing.Size(24, 14);
            this.lciPath.TextToControlDistance = 5;
            // 
            // lciElipse
            // 
            this.lciElipse.Control = this.btnReadXML;
            resources.ApplyResources(this.lciElipse, "lciElipse");
            this.lciElipse.Location = new System.Drawing.Point(344, 34);
            this.lciElipse.Name = "lciElipse";
            this.lciElipse.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciElipse.Size = new System.Drawing.Size(54, 21);
            this.lciElipse.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lciElipse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciElipse.TextSize = new System.Drawing.Size(0, 0);
            this.lciElipse.TextToControlDistance = 0;
            this.lciElipse.TextVisible = false;
            // 
            // lciEmptySpace
            // 
            this.lciEmptySpace.AllowHotTrack = false;
            resources.ApplyResources(this.lciEmptySpace, "lciEmptySpace");
            this.lciEmptySpace.Location = new System.Drawing.Point(0, 34);
            this.lciEmptySpace.Name = "lciEmptySpace";
            this.lciEmptySpace.Size = new System.Drawing.Size(66, 21);
            this.lciEmptySpace.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgProgress
            // 
            this.lcgProgress.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgProgress.AppearanceGroup.Font")));
            this.lcgProgress.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgProgress, "lcgProgress");
            this.lcgProgress.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.simpleLabelItem3,
            this.simpleLabelItem4});
            this.lcgProgress.Location = new System.Drawing.Point(0, 93);
            this.lcgProgress.Name = "lcgProgress";
            this.lcgProgress.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgProgress.Size = new System.Drawing.Size(410, 99);
            this.lcgProgress.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 0);
            this.lcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.pgSynchronizationProgress;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 10, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(398, 33);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem3.AppearanceItemCaption.Font")));
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem3, "simpleLabelItem3");
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 33);
            this.simpleLabelItem3.MinSize = new System.Drawing.Size(76, 20);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(83, 30);
            this.simpleLabelItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.simpleLabelItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(72, 13);
            // 
            // simpleLabelItem4
            // 
            this.simpleLabelItem4.AllowHotTrack = false;
            this.simpleLabelItem4.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem4.AppearanceItemCaption.Font")));
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem4, "simpleLabelItem4");
            this.simpleLabelItem4.Location = new System.Drawing.Point(83, 33);
            this.simpleLabelItem4.MinSize = new System.Drawing.Size(165, 21);
            this.simpleLabelItem4.Name = "simpleLabelItem4";
            this.simpleLabelItem4.Size = new System.Drawing.Size(315, 30);
            this.simpleLabelItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.simpleLabelItem4.TextSize = new System.Drawing.Size(161, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnImport;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(314, 67);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(54, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(96, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 67);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(314, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(314, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(314, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmImportHeadofficeMasters
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportHeadofficeMasters";
            this.Load += new System.EventHandler(this.frmImportHeadofficeMasters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnReadXML.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgSynchronizationProgress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSynMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSynMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciElipse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEmptySpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ProgressBarControl pgSynchronizationProgress;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.RadioGroup rgSynMode;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem lciSynMode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgProgress;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.ButtonEdit btnReadXML;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private DevExpress.XtraLayout.LayoutControlItem lciPath;
        private DevExpress.XtraLayout.LayoutControlItem lciElipse;
        private DevExpress.XtraLayout.EmptySpaceItem lciEmptySpace;
    }
}