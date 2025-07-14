namespace ACPP.Modules.UIControls
{
    partial class ucMailMergeOptions
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMailMergeOptions));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiSendMail = new DevExpress.XtraBars.BarButtonItem();
            this.bbiModifyTemplate = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSendSMS = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPrintLabel = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiModifyTemplate,
            this.bbiSendMail,
            this.bbiSendSMS,
            this.bbiClose,
            this.bbiPrintLabel});
            this.barManager1.MaxItemId = 9;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSendMail, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiModifyTemplate, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSendSMS, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiPrintLabel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiSendMail
            // 
            this.bbiSendMail.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbiSendMail.Caption = "Send Mail";
            this.bbiSendMail.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiSendMail.Glyph")));
            this.bbiSendMail.Id = 1;
            this.bbiSendMail.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiSendMail.LargeGlyph")));
            this.bbiSendMail.Name = "bbiSendMail";
            this.bbiSendMail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSendMail_ItemClick);
            // 
            // bbiModifyTemplate
            // 
            this.bbiModifyTemplate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbiModifyTemplate.Caption = "Modify Template";
            this.bbiModifyTemplate.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiModifyTemplate.Glyph")));
            this.bbiModifyTemplate.Id = 0;
            this.bbiModifyTemplate.Name = "bbiModifyTemplate";
            this.bbiModifyTemplate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiModifyTemplate_ItemClick);
            // 
            // bbiSendSMS
            // 
            this.bbiSendSMS.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbiSendSMS.Caption = "Send SMS";
            this.bbiSendSMS.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiSendSMS.Glyph")));
            this.bbiSendSMS.Id = 4;
            this.bbiSendSMS.Name = "bbiSendSMS";
            this.bbiSendSMS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSendSMS_ItemClick);
            // 
            // bbiPrintLabel
            // 
            this.bbiPrintLabel.Caption = "Print Label";
            this.bbiPrintLabel.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiPrintLabel.Glyph")));
            this.bbiPrintLabel.Id = 8;
            this.bbiPrintLabel.Name = "bbiPrintLabel";
            this.bbiPrintLabel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPrintLabel_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbiClose.Caption = "Close";
            this.bbiClose.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiClose.Glyph")));
            this.bbiClose.Id = 7;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(592, 47);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 47);
            this.barDockControlBottom.Size = new System.Drawing.Size(592, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 47);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(592, 47);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // ucMailMergeOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucMailMergeOptions";
            this.Size = new System.Drawing.Size(592, 47);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiModifyTemplate;
        private DevExpress.XtraBars.BarButtonItem bbiSendMail;
        private DevExpress.XtraBars.BarButtonItem bbiSendSMS;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraBars.BarButtonItem bbiPrintLabel;
    }
}
