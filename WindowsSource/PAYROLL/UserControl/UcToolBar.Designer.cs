namespace PAYROLL.UserControl
{
    partial class UcToolBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcToolBar));
            this.bmToolBar = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btiAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiImport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btiPrint = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btiRefresh = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bmToolBar)).BeginInit();
            this.SuspendLayout();
            // 
            // bmToolBar
            // 
            this.bmToolBar.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.bmToolBar.DockControls.Add(this.barDockControlTop);
            this.bmToolBar.DockControls.Add(this.barDockControlBottom);
            this.bmToolBar.DockControls.Add(this.barDockControlLeft);
            this.bmToolBar.DockControls.Add(this.barDockControlRight);
            this.bmToolBar.Form = this;
            this.bmToolBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btiAdd,
            this.btiEdit,
            this.btiDelete,
            this.btiPrint,
            this.btiRefresh,
            this.btnClose,
            this.bbiDelete,
            this.bbiRefresh,
            this.bbiImport});
            this.bmToolBar.MaxItemId = 9;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btiAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btiEdit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiImport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btiPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnClose, "", true, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar2, "bar2");
            // 
            // btiAdd
            // 
            this.btiAdd.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btiAdd, "btiAdd");
            this.btiAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("btiAdd.Glyph")));
            this.btiAdd.Id = 0;
            this.btiAdd.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btiAdd.LargeGlyph")));
            this.btiAdd.Name = "btiAdd";
            this.btiAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btiAdd_ItemClick);
            // 
            // btiEdit
            // 
            this.btiEdit.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btiEdit, "btiEdit");
            this.btiEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("btiEdit.Glyph")));
            this.btiEdit.Id = 1;
            this.btiEdit.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btiEdit.LargeGlyph")));
            this.btiEdit.Name = "btiEdit";
            this.btiEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btiEdit_ItemClick);
            // 
            // bbiImport
            // 
            this.bbiImport.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiImport, "bbiImport");
            this.bbiImport.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiImport.Glyph")));
            this.bbiImport.Id = 8;
            this.bbiImport.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiImport.LargeGlyph")));
            this.bbiImport.Name = "bbiImport";
            this.bbiImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiImport_ItemClick);
            // 
            // bbiDelete
            // 
            this.bbiDelete.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDelete, "bbiDelete");
            this.bbiDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiDelete.Glyph")));
            this.bbiDelete.Id = 6;
            this.bbiDelete.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiDelete.LargeGlyph")));
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btiDelete_ItemClick);
            // 
            // btiPrint
            // 
            this.btiPrint.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btiPrint, "btiPrint");
            this.btiPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("btiPrint.Glyph")));
            this.btiPrint.Id = 3;
            this.btiPrint.Name = "btiPrint";
            this.btiPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btiPrint_ItemClick);
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiRefresh, "bbiRefresh");
            this.bbiRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.Glyph")));
            this.bbiRefresh.Id = 7;
            this.bbiRefresh.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.LargeGlyph")));
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btiRefresh_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Glyph = ((System.Drawing.Image)(resources.GetObject("btnClose.Glyph")));
            this.btnClose.Id = 5;
            this.btnClose.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnClose.LargeGlyph")));
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // btiDelete
            // 
            this.btiDelete.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btiDelete, "btiDelete");
            this.btiDelete.Id = 2;
            this.btiDelete.Name = "btiDelete";
            this.btiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btiDelete_ItemClick);
            // 
            // btiRefresh
            // 
            this.btiRefresh.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btiRefresh, "btiRefresh");
            this.btiRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("btiRefresh.Glyph")));
            this.btiRefresh.Id = 4;
            this.btiRefresh.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btiRefresh.LargeGlyph")));
            this.btiRefresh.Name = "btiRefresh";
            this.btiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btiRefresh_ItemClick);
            // 
            // UcToolBar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UcToolBar";
            ((System.ComponentModel.ISupportInitialize)(this.bmToolBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager bmToolBar;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btiAdd;
        private DevExpress.XtraBars.BarButtonItem btiEdit;
        private DevExpress.XtraBars.BarButtonItem btiDelete;
        private DevExpress.XtraBars.BarButtonItem btiPrint;
        private DevExpress.XtraBars.BarButtonItem btiRefresh;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        private DevExpress.XtraBars.BarButtonItem bbiImport;

    }
}
