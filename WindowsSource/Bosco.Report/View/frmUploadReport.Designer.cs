namespace Bosco.Report.View
{
    partial class frmUploadReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadReport));
            this.pgUploadDatabase = new DevExpress.XtraEditors.ProgressBarControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblFileName = new DevExpress.XtraEditors.LabelControl();
            this.rgFileType = new DevExpress.XtraEditors.RadioGroup();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnUploadDB = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcUpload = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblUploadStatus = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcFileType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcFileName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pgUploadDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgFileType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUploadStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFileType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFileName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // pgUploadDatabase
            // 
            resources.ApplyResources(this.pgUploadDatabase, "pgUploadDatabase");
            this.pgUploadDatabase.Name = "pgUploadDatabase";
            this.pgUploadDatabase.Properties.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("pgUploadDatabase.Properties.Appearance.BackColor")));
            this.pgUploadDatabase.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("pgUploadDatabase.Properties.Appearance.Font")));
            this.pgUploadDatabase.Properties.EndColor = System.Drawing.Color.Maroon;
            this.pgUploadDatabase.Properties.StartColor = System.Drawing.Color.Maroon;
            this.pgUploadDatabase.StyleController = this.layoutControl1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblFileName);
            this.layoutControl1.Controls.Add(this.rgFileType);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.pgUploadDatabase);
            this.layoutControl1.Controls.Add(this.btnUploadDB);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(839, 122, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblFileName
            // 
            resources.ApplyResources(this.lblFileName, "lblFileName");
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.StyleController = this.layoutControl1;
            // 
            // rgFileType
            // 
            resources.ApplyResources(this.rgFileType, "rgFileType");
            this.rgFileType.Name = "rgFileType";
            this.rgFileType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgFileType.Properties.Items"))), resources.GetString("rgFileType.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgFileType.Properties.Items2"))), resources.GetString("rgFileType.Properties.Items3"))});
            this.rgFileType.StyleController = this.layoutControl1;
            this.rgFileType.SelectedIndexChanged += new System.EventHandler(this.rgFileType_SelectedIndexChanged);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUploadDB
            // 
            resources.ApplyResources(this.btnUploadDB, "btnUploadDB");
            this.btnUploadDB.Name = "btnUploadDB";
            this.btnUploadDB.StyleController = this.layoutControl1;
            this.btnUploadDB.Click += new System.EventHandler(this.btnUploadDB_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pgUploadDatabase;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 18);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(308, 22);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcUpload,
            this.lcClose,
            this.lblUploadStatus,
            this.lcFileType,
            this.lcFileName,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(516, 96);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcUpload
            // 
            this.lcUpload.Control = this.btnUploadDB;
            resources.ApplyResources(this.lcUpload, "lcUpload");
            this.lcUpload.Location = new System.Drawing.Point(360, 63);
            this.lcUpload.MaxSize = new System.Drawing.Size(75, 27);
            this.lcUpload.MinSize = new System.Drawing.Size(75, 27);
            this.lcUpload.Name = "lcUpload";
            this.lcUpload.Size = new System.Drawing.Size(75, 27);
            this.lcUpload.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcUpload.TextSize = new System.Drawing.Size(0, 0);
            this.lcUpload.TextToControlDistance = 0;
            this.lcUpload.TextVisible = false;
            // 
            // lcClose
            // 
            this.lcClose.Control = this.btnClose;
            resources.ApplyResources(this.lcClose, "lcClose");
            this.lcClose.Location = new System.Drawing.Point(435, 63);
            this.lcClose.MaxSize = new System.Drawing.Size(75, 27);
            this.lcClose.MinSize = new System.Drawing.Size(75, 27);
            this.lcClose.Name = "lcClose";
            this.lcClose.Size = new System.Drawing.Size(75, 27);
            this.lcClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcClose.TextSize = new System.Drawing.Size(0, 0);
            this.lcClose.TextToControlDistance = 0;
            this.lcClose.TextVisible = false;
            // 
            // lblUploadStatus
            // 
            this.lblUploadStatus.AllowHotTrack = false;
            this.lblUploadStatus.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblUploadStatus.AppearanceItemCaption.Font")));
            this.lblUploadStatus.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblUploadStatus.AppearanceItemCaption.ForeColor")));
            this.lblUploadStatus.AppearanceItemCaption.Options.UseFont = true;
            this.lblUploadStatus.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblUploadStatus, "lblUploadStatus");
            this.lblUploadStatus.Location = new System.Drawing.Point(0, 63);
            this.lblUploadStatus.MinSize = new System.Drawing.Size(111, 18);
            this.lblUploadStatus.Name = "lblUploadStatus";
            this.lblUploadStatus.Size = new System.Drawing.Size(360, 27);
            this.lblUploadStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblUploadStatus.TextSize = new System.Drawing.Size(6, 14);
            // 
            // lcFileType
            // 
            this.lcFileType.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcFileType.AppearanceItemCaption.Font")));
            this.lcFileType.AppearanceItemCaption.Options.UseFont = true;
            this.lcFileType.Control = this.rgFileType;
            resources.ApplyResources(this.lcFileType, "lcFileType");
            this.lcFileType.Location = new System.Drawing.Point(0, 0);
            this.lcFileType.MaxSize = new System.Drawing.Size(255, 29);
            this.lcFileType.MinSize = new System.Drawing.Size(255, 29);
            this.lcFileType.Name = "lcFileType";
            this.lcFileType.Size = new System.Drawing.Size(255, 29);
            this.lcFileType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcFileType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcFileType.TextSize = new System.Drawing.Size(60, 20);
            this.lcFileType.TextToControlDistance = 5;
            // 
            // lcFileName
            // 
            this.lcFileName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcFileName.AppearanceItemCaption.Font")));
            this.lcFileName.AppearanceItemCaption.Options.UseFont = true;
            this.lcFileName.Control = this.lblFileName;
            resources.ApplyResources(this.lcFileName, "lcFileName");
            this.lcFileName.Location = new System.Drawing.Point(0, 29);
            this.lcFileName.Name = "lcFileName";
            this.lcFileName.Size = new System.Drawing.Size(510, 34);
            this.lcFileName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcFileName.TextSize = new System.Drawing.Size(60, 30);
            this.lcFileName.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(255, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(255, 29);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmUploadReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUploadReport";
            this.Load += new System.EventHandler(this.frmUploadBranchOfficeDBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pgUploadDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgFileType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUploadStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFileType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFileName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl pgUploadDatabase;
        private DevExpress.XtraEditors.SimpleButton btnUploadDB;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lcUpload;
        private DevExpress.XtraLayout.SimpleLabelItem lblUploadStatus;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem lcClose;
        private DevExpress.XtraEditors.LabelControl lblFileName;
        private DevExpress.XtraEditors.RadioGroup rgFileType;
        private DevExpress.XtraLayout.LayoutControlItem lcFileType;
        private DevExpress.XtraLayout.LayoutControlItem lcFileName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}