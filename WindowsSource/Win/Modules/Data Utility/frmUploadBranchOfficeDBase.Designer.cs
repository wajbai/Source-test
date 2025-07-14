namespace ACPP.Modules.Data_Utility
{
    partial class frmUploadBranchOfficeDBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadBranchOfficeDBase));
            this.pgUploadDatabase = new DevExpress.XtraEditors.ProgressBarControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnUploadDB = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblInfoMessage = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblUploadStatus = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.pgUploadDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInfoMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUploadStatus)).BeginInit();
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
            this.lblInfoMessage,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.lblUploadStatus});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(527, 76);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblInfoMessage
            // 
            this.lblInfoMessage.AllowHotTrack = false;
            this.lblInfoMessage.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblInfoMessage.AppearanceItemCaption.Font")));
            this.lblInfoMessage.AppearanceItemCaption.Options.UseFont = true;
            this.lblInfoMessage.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblInfoMessage.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.lblInfoMessage, "lblInfoMessage");
            this.lblInfoMessage.Image = global::ACPP.Properties.Resources.info;
            this.lblInfoMessage.Location = new System.Drawing.Point(0, 0);
            this.lblInfoMessage.MinSize = new System.Drawing.Size(111, 17);
            this.lblInfoMessage.Name = "lblInfoMessage";
            this.lblInfoMessage.Size = new System.Drawing.Size(521, 44);
            this.lblInfoMessage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblInfoMessage.TextSize = new System.Drawing.Size(442, 24);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnUploadDB;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(338, 44);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(55, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(107, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(445, 44);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(41, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblUploadStatus
            // 
            this.lblUploadStatus.AllowHotTrack = false;
            this.lblUploadStatus.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblUploadStatus.AppearanceItemCaption.Font")));
            this.lblUploadStatus.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblUploadStatus.AppearanceItemCaption.ForeColor")));
            this.lblUploadStatus.AppearanceItemCaption.Options.UseFont = true;
            this.lblUploadStatus.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblUploadStatus, "lblUploadStatus");
            this.lblUploadStatus.Location = new System.Drawing.Point(0, 44);
            this.lblUploadStatus.MinSize = new System.Drawing.Size(111, 18);
            this.lblUploadStatus.Name = "lblUploadStatus";
            this.lblUploadStatus.Size = new System.Drawing.Size(338, 26);
            this.lblUploadStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblUploadStatus.TextSize = new System.Drawing.Size(442, 14);
            // 
            // frmUploadBranchOfficeDBase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUploadBranchOfficeDBase";
            this.Load += new System.EventHandler(this.frmUploadBranchOfficeDBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pgUploadDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInfoMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUploadStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl pgUploadDatabase;
        private DevExpress.XtraEditors.SimpleButton btnUploadDB;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblUploadStatus;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblInfoMessage;
    }
}