namespace ACPP.Modules.Dsync
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
            this.lblImportMasterNote = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.rgSynMode = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciSynMode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciPath = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciElipse = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSynMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSynMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciElipse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblImportMasterNote);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnLoad);
            this.layoutControl1.Controls.Add(this.txtPath);
            this.layoutControl1.Controls.Add(this.btnImport);
            this.layoutControl1.Controls.Add(this.rgSynMode);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(716, 1, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblImportMasterNote
            // 
            this.lblImportMasterNote.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblImportMasterNote.Appearance.Font")));
            resources.ApplyResources(this.lblImportMasterNote, "lblImportMasterNote");
            this.lblImportMasterNote.Name = "lblImportMasterNote";
            this.lblImportMasterNote.StyleController = this.layoutControl1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLoad
            // 
            resources.ApplyResources(this.btnLoad, "btnLoad");
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.StyleController = this.layoutControl1;
            this.btnLoad.Click += new System.EventHandler(this.simpleButton1_Click);
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
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgSynMode.Properties.Items2"))), resources.GetString("rgSynMode.Properties.Items3"), ((bool)(resources.GetObject("rgSynMode.Properties.Items4"))))});
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
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(410, 115);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciSynMode,
            this.lciPath,
            this.lciElipse});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup2.Size = new System.Drawing.Size(410, 68);
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
            this.lciSynMode.TextSize = new System.Drawing.Size(73, 16);
            this.lciSynMode.TextToControlDistance = 5;
            this.lciSynMode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lciPath
            // 
            this.lciPath.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lciPath.AppearanceItemCaption.Font")));
            this.lciPath.AppearanceItemCaption.Options.UseFont = true;
            this.lciPath.Control = this.txtPath;
            resources.ApplyResources(this.lciPath, "lciPath");
            this.lciPath.Location = new System.Drawing.Point(0, 34);
            this.lciPath.MaxSize = new System.Drawing.Size(327, 22);
            this.lciPath.MinSize = new System.Drawing.Size(327, 22);
            this.lciPath.Name = "lciPath";
            this.lciPath.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciPath.Size = new System.Drawing.Size(327, 22);
            this.lciPath.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciPath.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciPath.TextSize = new System.Drawing.Size(24, 14);
            this.lciPath.TextToControlDistance = 5;
            // 
            // lciElipse
            // 
            this.lciElipse.Control = this.btnLoad;
            resources.ApplyResources(this.lciElipse, "lciElipse");
            this.lciElipse.Location = new System.Drawing.Point(327, 34);
            this.lciElipse.Name = "lciElipse";
            this.lciElipse.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciElipse.Size = new System.Drawing.Size(71, 22);
            this.lciElipse.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 0, 0, 0);
            this.lciElipse.TextSize = new System.Drawing.Size(0, 0);
            this.lciElipse.TextToControlDistance = 0;
            this.lciElipse.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnImport;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(258, 68);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(79, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(79, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 68);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(258, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(258, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(258, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(337, 68);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblImportMasterNote;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 94);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(410, 21);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmImportHeadofficeMasters
            // 
            this.AcceptButton = this.btnImport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "frmImportHeadofficeMasters";
            this.Load += new System.EventHandler(this.frmImportHeadofficeMasters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSynMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSynMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciElipse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.RadioGroup rgSynMode;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem lciSynMode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private DevExpress.XtraLayout.LayoutControlItem lciPath;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraLayout.LayoutControlItem lciElipse;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl lblImportMasterNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}