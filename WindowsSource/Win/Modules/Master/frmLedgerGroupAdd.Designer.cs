namespace ACPP.Modules.Master
{
    partial class frmLedgerGroupAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLedgerGroupAdd));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lkpLedgerGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.txtLedgerName = new DevExpress.XtraEditors.TextEdit();
            this.txtLedgerCode = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lcgLedgerGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLedgerGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpLedgerGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.lkpLedgerGroup);
            this.layoutControl1.Controls.Add(this.txtLedgerName);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtLedgerCode);
            this.layoutControl1.Controls.Add(this.btnClose);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(649, 130, 250, 350);
            this.layoutControl1.Root = this.lcgLedgerGroup;
            // 
            // lkpLedgerGroup
            // 
            resources.ApplyResources(this.lkpLedgerGroup, "lkpLedgerGroup");
            this.lkpLedgerGroup.Name = "lkpLedgerGroup";
            this.lkpLedgerGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lkpLedgerGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkpLedgerGroup.Properties.Buttons"))))});
            this.lkpLedgerGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpLedgerGroup.Properties.Columns"), resources.GetString("lkpLedgerGroup.Properties.Columns1"), ((int)(resources.GetObject("lkpLedgerGroup.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lkpLedgerGroup.Properties.Columns3"))), resources.GetString("lkpLedgerGroup.Properties.Columns4"), ((bool)(resources.GetObject("lkpLedgerGroup.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lkpLedgerGroup.Properties.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpLedgerGroup.Properties.Columns7"), resources.GetString("lkpLedgerGroup.Properties.Columns8"))});
            this.lkpLedgerGroup.Properties.ImmediatePopup = true;
            this.lkpLedgerGroup.Properties.NullText = resources.GetString("lkpLedgerGroup.Properties.NullText");
            this.lkpLedgerGroup.Properties.PopupFormMinSize = new System.Drawing.Size(10, 0);
            this.lkpLedgerGroup.Properties.ShowHeader = false;
            this.lkpLedgerGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lkpLedgerGroup.StyleController = this.layoutControl1;
            // 
            // txtLedgerName
            // 
            resources.ApplyResources(this.txtLedgerName, "txtLedgerName");
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtLedgerName.Properties.MaxLength = 75;
            this.txtLedgerName.StyleController = this.layoutControl1;
            this.txtLedgerName.Leave += new System.EventHandler(this.txtLedgerName_Leave);
            // 
            // txtLedgerCode
            // 
            resources.ApplyResources(this.txtLedgerCode, "txtLedgerCode");
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtLedgerCode.Properties.MaxLength = 5;
            this.txtLedgerCode.StyleController = this.layoutControl1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lcgLedgerGroup
            // 
            resources.ApplyResources(this.lcgLedgerGroup, "lcgLedgerGroup");
            this.lcgLedgerGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgLedgerGroup.GroupBordersVisible = false;
            this.lcgLedgerGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblCode,
            this.lblName,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.lblLedgerGroup,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.lcgLedgerGroup.Location = new System.Drawing.Point(0, 0);
            this.lcgLedgerGroup.Name = "Root";
            this.lcgLedgerGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgLedgerGroup.Size = new System.Drawing.Size(349, 104);
            this.lcgLedgerGroup.TextVisible = false;
            // 
            // lblCode
            // 
            this.lblCode.AllowHtmlStringInCaption = true;
            this.lblCode.Control = this.txtLedgerCode;
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Location = new System.Drawing.Point(0, 28);
            this.lblCode.Name = "lblCode";
            this.lblCode.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCode.Size = new System.Drawing.Size(171, 23);
            this.lblCode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCode.TextSize = new System.Drawing.Size(92, 13);
            // 
            // lblName
            // 
            this.lblName.AllowHtmlStringInCaption = true;
            this.lblName.Control = this.txtLedgerName;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Location = new System.Drawing.Point(0, 51);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblName.Size = new System.Drawing.Size(349, 23);
            this.lblName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblName.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(284, 74);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(65, 30);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(65, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(65, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(214, 74);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(70, 30);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(70, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(70, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lblLedgerGroup
            // 
            this.lblLedgerGroup.AllowHtmlStringInCaption = true;
            this.lblLedgerGroup.Control = this.lkpLedgerGroup;
            resources.ApplyResources(this.lblLedgerGroup, "lblLedgerGroup");
            this.lblLedgerGroup.Location = new System.Drawing.Point(0, 0);
            this.lblLedgerGroup.Name = "lblLedgerGroup";
            this.lblLedgerGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblLedgerGroup.Size = new System.Drawing.Size(349, 28);
            this.lblLedgerGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 3);
            this.lblLedgerGroup.TextSize = new System.Drawing.Size(92, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(171, 28);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(178, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 74);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(214, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmLedgerGroupAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmLedgerGroupAdd";
            this.Load += new System.EventHandler(this.frmLedgerGroupAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkpLedgerGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtLedgerCode;
        private DevExpress.XtraLayout.LayoutControlGroup lcgLedgerGroup;
        private DevExpress.XtraLayout.LayoutControlItem lblCode;
        private DevExpress.XtraEditors.TextEdit txtLedgerName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit lkpLedgerGroup;
        private DevExpress.XtraLayout.LayoutControlItem lblLedgerGroup;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;

    }
}