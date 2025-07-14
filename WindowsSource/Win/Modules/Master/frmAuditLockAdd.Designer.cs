namespace ACPP.Modules.Master
{
    partial class frmAuditLockAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditLockAdd));
            this.lcAuditType = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtLockType = new DevExpress.XtraEditors.TextEdit();
            this.lcgAuditLockType = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblLockType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcAuditType)).BeginInit();
            this.lcAuditType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLockType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAuditLockType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLockType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // lcAuditType
            // 
            this.lcAuditType.Controls.Add(this.btnClose);
            this.lcAuditType.Controls.Add(this.btnSave);
            this.lcAuditType.Controls.Add(this.txtLockType);
            resources.ApplyResources(this.lcAuditType, "lcAuditType");
            this.lcAuditType.Name = "lcAuditType";
            this.lcAuditType.Root = this.lcgAuditLockType;
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcAuditType;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcAuditType;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtLockType
            // 
            resources.ApplyResources(this.txtLockType, "txtLockType");
            this.txtLockType.Name = "txtLockType";
            this.txtLockType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtLockType.Properties.MaxLength = 50;
            this.txtLockType.StyleController = this.lcAuditType;
            this.txtLockType.Leave += new System.EventHandler(this.txtLockType_Leave);
            // 
            // lcgAuditLockType
            // 
            resources.ApplyResources(this.lcgAuditLockType, "lcgAuditLockType");
            this.lcgAuditLockType.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgAuditLockType.GroupBordersVisible = false;
            this.lcgAuditLockType.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblLockType,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2});
            this.lcgAuditLockType.Location = new System.Drawing.Point(0, 0);
            this.lcgAuditLockType.Name = "lcgAuditLockType";
            this.lcgAuditLockType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgAuditLockType.Size = new System.Drawing.Size(351, 50);
            this.lcgAuditLockType.TextVisible = false;
            // 
            // lblLockType
            // 
            this.lblLockType.AllowHtmlStringInCaption = true;
            this.lblLockType.Control = this.txtLockType;
            resources.ApplyResources(this.lblLockType, "lblLockType");
            this.lblLockType.Location = new System.Drawing.Point(0, 0);
            this.lblLockType.Name = "lblLockType";
            this.lblLockType.Size = new System.Drawing.Size(351, 24);
            this.lblLockType.TextSize = new System.Drawing.Size(57, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(213, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(282, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(213, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmAuditLockAdd
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcAuditType);
            this.Name = "frmAuditLockAdd";
            this.Load += new System.EventHandler(this.frmAuditLockAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcAuditType)).EndInit();
            this.lcAuditType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLockType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAuditLockType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLockType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcAuditType;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtLockType;
        private DevExpress.XtraLayout.LayoutControlGroup lcgAuditLockType;
        private DevExpress.XtraLayout.LayoutControlItem lblLockType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}