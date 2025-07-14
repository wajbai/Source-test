namespace ACPP.Modules.Inventory.Asset
{
    partial class frmGenerateAsseIdManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerateAsseIdManual));
            this.btnGenerate = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtStartingNo = new DevExpress.XtraEditors.TextEdit();
            this.txtSuffix = new DevExpress.XtraEditors.TextEdit();
            this.txtPrefix = new DevExpress.XtraEditors.TextEdit();
            this.txtWidth = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblPrefix = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblStartingNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblSuffix = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblWidth = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAssetItemName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAssetItem = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartingNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSuffix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrefix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartingNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSuffix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAssetItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAssetItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            resources.ApplyResources(this.btnGenerate, "btnGenerate");
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.StyleController = this.layoutControl1;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnGenerate);
            this.layoutControl1.Controls.Add(this.txtStartingNo);
            this.layoutControl1.Controls.Add(this.txtSuffix);
            this.layoutControl1.Controls.Add(this.txtPrefix);
            this.layoutControl1.Controls.Add(this.txtWidth);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(74, 285, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtStartingNo
            // 
            this.txtStartingNo.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtStartingNo, "txtStartingNo");
            this.txtStartingNo.Name = "txtStartingNo";
            this.txtStartingNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtStartingNo.Properties.Mask.EditMask = resources.GetString("txtStartingNo.Properties.Mask.EditMask");
            this.txtStartingNo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtStartingNo.Properties.Mask.MaskType")));
            this.txtStartingNo.Properties.MaxLength = 3;
            this.txtStartingNo.StyleController = this.layoutControl1;
            this.txtStartingNo.Leave += new System.EventHandler(this.txtStartingNo_Leave);
            // 
            // txtSuffix
            // 
            this.txtSuffix.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtSuffix, "txtSuffix");
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSuffix.Properties.MaxLength = 6;
            this.txtSuffix.StyleController = this.layoutControl1;
            // 
            // txtPrefix
            // 
            this.txtPrefix.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPrefix.Properties.MaxLength = 6;
            this.txtPrefix.StyleController = this.layoutControl1;
            this.txtPrefix.Leave += new System.EventHandler(this.txtPrefix_Leave);
            // 
            // txtWidth
            // 
            this.txtWidth.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtWidth, "txtWidth");
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtWidth.Properties.Mask.EditMask = resources.GetString("txtWidth.Properties.Mask.EditMask");
            this.txtWidth.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtWidth.Properties.Mask.MaskType")));
            this.txtWidth.Properties.MaxLength = 6;
            this.txtWidth.StyleController = this.layoutControl1;
            this.txtWidth.Leave += new System.EventHandler(this.txtWidth_Leave);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblPrefix,
            this.lblStartingNo,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.lblSuffix,
            this.lblWidth,
            this.lblAssetItemName,
            this.lblAssetItem,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(394, 101);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblPrefix
            // 
            this.lblPrefix.AllowHtmlStringInCaption = true;
            this.lblPrefix.Control = this.txtPrefix;
            resources.ApplyResources(this.lblPrefix, "lblPrefix");
            this.lblPrefix.Location = new System.Drawing.Point(0, 22);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(198, 26);
            this.lblPrefix.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 2);
            this.lblPrefix.TextSize = new System.Drawing.Size(71, 13);
            // 
            // lblStartingNo
            // 
            this.lblStartingNo.AllowHtmlStringInCaption = true;
            this.lblStartingNo.Control = this.txtStartingNo;
            resources.ApplyResources(this.lblStartingNo, "lblStartingNo");
            this.lblStartingNo.Location = new System.Drawing.Point(0, 48);
            this.lblStartingNo.Name = "lblStartingNo";
            this.lblStartingNo.Size = new System.Drawing.Size(198, 26);
            this.lblStartingNo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 2);
            this.lblStartingNo.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnGenerate;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(262, 74);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(67, 27);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(67, 27);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(67, 27);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 74);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(262, 27);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblSuffix
            // 
            this.lblSuffix.AllowHtmlStringInCaption = true;
            this.lblSuffix.Control = this.txtSuffix;
            resources.ApplyResources(this.lblSuffix, "lblSuffix");
            this.lblSuffix.Location = new System.Drawing.Point(198, 22);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(196, 26);
            this.lblSuffix.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 2);
            this.lblSuffix.TextSize = new System.Drawing.Size(71, 13);
            // 
            // lblWidth
            // 
            this.lblWidth.AllowHtmlStringInCaption = true;
            this.lblWidth.Control = this.txtWidth;
            resources.ApplyResources(this.lblWidth, "lblWidth");
            this.lblWidth.Location = new System.Drawing.Point(198, 48);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(196, 26);
            this.lblWidth.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 2);
            this.lblWidth.TextSize = new System.Drawing.Size(71, 13);
            // 
            // lblAssetItemName
            // 
            this.lblAssetItemName.AllowHotTrack = false;
            this.lblAssetItemName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblAssetItemName.AppearanceItemCaption.Font")));
            this.lblAssetItemName.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblAssetItemName, "lblAssetItemName");
            this.lblAssetItemName.Location = new System.Drawing.Point(0, 0);
            this.lblAssetItemName.MaxSize = new System.Drawing.Size(75, 22);
            this.lblAssetItemName.MinSize = new System.Drawing.Size(75, 22);
            this.lblAssetItemName.Name = "lblAssetItemName";
            this.lblAssetItemName.Size = new System.Drawing.Size(75, 22);
            this.lblAssetItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAssetItemName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 4);
            this.lblAssetItemName.TextSize = new System.Drawing.Size(71, 14);
            // 
            // lblAssetItem
            // 
            this.lblAssetItem.AllowHotTrack = false;
            this.lblAssetItem.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblAssetItem.AppearanceItemCaption.Font")));
            this.lblAssetItem.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblAssetItem, "lblAssetItem");
            this.lblAssetItem.Location = new System.Drawing.Point(75, 0);
            this.lblAssetItem.MaxSize = new System.Drawing.Size(313, 22);
            this.lblAssetItem.MinSize = new System.Drawing.Size(313, 22);
            this.lblAssetItem.Name = "lblAssetItem";
            this.lblAssetItem.Size = new System.Drawing.Size(319, 22);
            this.lblAssetItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAssetItem.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblAssetItem.TextSize = new System.Drawing.Size(71, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(329, 74);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(65, 27);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(65, 27);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(65, 27);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmGenerateAsseIdManual
            // 
            this.AcceptButton = this.btnGenerate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmGenerateAsseIdManual";
            this.Load += new System.EventHandler(this.frmGenerateAsseIdManual_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStartingNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSuffix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrefix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartingNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSuffix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAssetItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAssetItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnGenerate;
        private DevExpress.XtraEditors.TextEdit txtStartingNo;
        private DevExpress.XtraEditors.TextEdit txtSuffix;
        private DevExpress.XtraEditors.TextEdit txtPrefix;
        private DevExpress.XtraEditors.TextEdit txtWidth;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblPrefix;
        private DevExpress.XtraLayout.LayoutControlItem lblStartingNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblSuffix;
        private DevExpress.XtraLayout.LayoutControlItem lblWidth;
        private DevExpress.XtraLayout.SimpleLabelItem lblAssetItemName;
        private DevExpress.XtraLayout.SimpleLabelItem lblAssetItem;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}