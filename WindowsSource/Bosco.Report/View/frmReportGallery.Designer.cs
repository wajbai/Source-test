namespace Bosco.Report.View
{
    partial class frmReportGallery
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportGallery));
            this.nvbReport = new DevExpress.XtraNavBar.NavBarControl();
            this.nvbGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.rptViewer = new Bosco.Report.View.ReportViewer();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpReportMenu = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.barReportMenu = new DevExpress.XtraBars.BarManager(this.components);
            this.barMenu = new DevExpress.XtraBars.Bar();
            this.barSubItemReports = new DevExpress.XtraBars.BarSubItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItemReports1 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItemAbstract = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.dockManager2 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerBottom = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dpReportDescription = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel5_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.bar1 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.nvbReport)).BeginInit();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel3.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dpReportMenu.SuspendLayout();
            this.dockPanel4_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barReportMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager2)).BeginInit();
            this.hideContainerBottom.SuspendLayout();
            this.dpReportDescription.SuspendLayout();
            this.dockPanel5_Container.SuspendLayout();
            this.dockPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nvbReport
            // 
            this.nvbReport.ActiveGroup = this.nvbGroup1;
            resources.ApplyResources(this.nvbReport, "nvbReport");
            this.nvbReport.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nvbGroup1});
            this.nvbReport.LinkInterval = 5;
            this.nvbReport.Name = "nvbReport";
            this.nvbReport.OptionsNavPane.ExpandedWidth = ((int)(resources.GetObject("resource.ExpandedWidth")));
            // 
            // nvbGroup1
            // 
            this.nvbGroup1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("nvbGroup1.Appearance.Font")));
            this.nvbGroup1.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.nvbGroup1, "nvbGroup1");
            this.nvbGroup1.Name = "nvbGroup1";
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblReportTitle);
            resources.ApplyResources(this.pnlTitle, "pnlTitle");
            this.pnlTitle.Name = "pnlTitle";
            // 
            // lblReportTitle
            // 
            resources.ApplyResources(this.lblReportTitle, "lblReportTitle");
            this.lblReportTitle.ForeColor = System.Drawing.Color.OliveDrab;
            this.lblReportTitle.Name = "lblReportTitle";
            // 
            // rptViewer
            // 
            resources.ApplyResources(this.rptViewer, "rptViewer");
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.ReportId = "";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel3,
            this.dockPanel1,
            this.dpReportMenu});
            this.dockManager1.MenuManager = this.barReportMenu;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanel3.FloatLocation = new System.Drawing.Point(229, 167);
            this.dockPanel3.ID = new System.Guid("f50373c6-6690-4595-96ea-c7bcc97a2bc9");
            resources.ApplyResources(this.dockPanel3, "dockPanel3");
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.OriginalSize = new System.Drawing.Size(214, 200);
            this.dockPanel3.SavedIndex = 0;
            this.dockPanel3.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel3_Container
            // 
            resources.ApplyResources(this.dockPanel3_Container, "dockPanel3_Container");
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanel1.FloatLocation = new System.Drawing.Point(233, 219);
            this.dockPanel1.ID = new System.Guid("8923a928-f69c-4ba5-b19e-3a49a6e0e796");
            resources.ApplyResources(this.dockPanel1, "dockPanel1");
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.SavedIndex = 0;
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel1_Container
            // 
            resources.ApplyResources(this.dockPanel1_Container, "dockPanel1_Container");
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            // 
            // dpReportMenu
            // 
            this.dpReportMenu.Controls.Add(this.dockPanel4_Container);
            this.dpReportMenu.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpReportMenu.ID = new System.Guid("513f4f19-16f3-4b3c-a8ea-a81d51b8a6e7");
            resources.ApplyResources(this.dpReportMenu, "dpReportMenu");
            this.dpReportMenu.Name = "dpReportMenu";
            this.dpReportMenu.Options.AllowDockAsTabbedDocument = false;
            this.dpReportMenu.Options.AllowDockBottom = false;
            this.dpReportMenu.Options.AllowDockFill = false;
            this.dpReportMenu.Options.AllowDockRight = false;
            this.dpReportMenu.Options.AllowDockTop = false;
            this.dpReportMenu.Options.AllowFloating = false;
            this.dpReportMenu.Options.FloatOnDblClick = false;
            this.dpReportMenu.Options.ShowCloseButton = false;
            this.dpReportMenu.Options.ShowMaximizeButton = false;
            this.dpReportMenu.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpReportMenu.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpReportMenu.SavedIndex = 0;
            this.dpReportMenu.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel4_Container
            // 
            this.dockPanel4_Container.Controls.Add(this.nvbReport);
            resources.ApplyResources(this.dockPanel4_Container, "dockPanel4_Container");
            this.dockPanel4_Container.Name = "dockPanel4_Container";
            // 
            // barReportMenu
            // 
            this.barReportMenu.AllowCustomization = false;
            this.barReportMenu.AllowQuickCustomization = false;
            this.barReportMenu.AllowShowToolbarsPopup = false;
            this.barReportMenu.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMenu});
            this.barReportMenu.DockControls.Add(this.barDockControlTop);
            this.barReportMenu.DockControls.Add(this.barDockControlBottom);
            this.barReportMenu.DockControls.Add(this.barDockControlLeft);
            this.barReportMenu.DockControls.Add(this.barDockControlRight);
            this.barReportMenu.DockManager = this.dockManager1;
            this.barReportMenu.Form = this;
            this.barReportMenu.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem3,
            this.barSubItemReports1,
            this.barSubItemAbstract,
            this.barButtonItem1,
            this.barSubItemReports});
            this.barReportMenu.MaxItemId = 10;
            this.barReportMenu.HighlightedLinkChanged += new DevExpress.XtraBars.HighlightedLinkChangedEventHandler(this.barReportMenu_HighlightedLinkChanged);
            // 
            // barMenu
            // 
            this.barMenu.BarName = "Custom 3";
            this.barMenu.DockCol = 0;
            this.barMenu.DockRow = 0;
            this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemReports)});
            this.barMenu.OptionsBar.AllowQuickCustomization = false;
            this.barMenu.OptionsBar.DrawDragBorder = false;
            this.barMenu.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.barMenu, "barMenu");
            // 
            // barSubItemReports
            // 
            this.barSubItemReports.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.barSubItemReports, "barSubItemReports");
            this.barSubItemReports.Id = 9;
            this.barSubItemReports.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("barSubItemReports.ItemAppearance.Normal.Font")));
            this.barSubItemReports.ItemAppearance.Normal.Options.UseFont = true;
            this.barSubItemReports.Name = "barSubItemReports";
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
            // barSubItem3
            // 
            resources.ApplyResources(this.barSubItem3, "barSubItem3");
            this.barSubItem3.Id = 3;
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barSubItemReports1
            // 
            resources.ApplyResources(this.barSubItemReports1, "barSubItemReports1");
            this.barSubItemReports1.Id = 5;
            this.barSubItemReports1.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("barSubItemReports1.ItemAppearance.Normal.Font")));
            this.barSubItemReports1.ItemAppearance.Normal.Options.UseFont = true;
            this.barSubItemReports1.Name = "barSubItemReports1";
            // 
            // barSubItemAbstract
            // 
            resources.ApplyResources(this.barSubItemAbstract, "barSubItemAbstract");
            this.barSubItemAbstract.Id = 7;
            this.barSubItemAbstract.Name = "barSubItemAbstract";
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // lblDescription
            // 
            this.lblDescription.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("lblDescription.Appearance.BackColor")));
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // dockManager2
            // 
            this.dockManager2.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerBottom});
            this.dockManager2.Form = this;
            this.dockManager2.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel2});
            this.dockManager2.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // hideContainerBottom
            // 
            this.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.hideContainerBottom.Controls.Add(this.dpReportDescription);
            resources.ApplyResources(this.hideContainerBottom, "hideContainerBottom");
            this.hideContainerBottom.Name = "hideContainerBottom";
            // 
            // dpReportDescription
            // 
            this.dpReportDescription.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("dpReportDescription.Appearance.BackColor")));
            this.dpReportDescription.Appearance.Options.UseBackColor = true;
            this.dpReportDescription.Controls.Add(this.dockPanel5_Container);
            this.dpReportDescription.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            resources.ApplyResources(this.dpReportDescription, "dpReportDescription");
            this.dpReportDescription.ID = new System.Guid("3ea13df8-a7e9-4988-aedb-dbc515b0b713");
            this.dpReportDescription.Name = "dpReportDescription";
            this.dpReportDescription.Options.AllowDockAsTabbedDocument = false;
            this.dpReportDescription.Options.AllowDockFill = false;
            this.dpReportDescription.Options.AllowDockLeft = false;
            this.dpReportDescription.Options.AllowDockRight = false;
            this.dpReportDescription.Options.AllowDockTop = false;
            this.dpReportDescription.Options.AllowFloating = false;
            this.dpReportDescription.Options.FloatOnDblClick = false;
            this.dpReportDescription.Options.ShowCloseButton = false;
            this.dpReportDescription.Options.ShowMaximizeButton = false;
            this.dpReportDescription.OriginalSize = new System.Drawing.Size(200, 86);
            this.dpReportDescription.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpReportDescription.SavedIndex = 0;
            this.dpReportDescription.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel5_Container
            // 
            this.dockPanel5_Container.Controls.Add(this.lblDescription);
            resources.ApplyResources(this.dockPanel5_Container, "dockPanel5_Container");
            this.dockPanel5_Container.Name = "dockPanel5_Container";
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanel2.ID = new System.Guid("2f01c4a8-7564-4ce2-a110-c9eb5d9377d9");
            resources.ApplyResources(this.dockPanel2, "dockPanel2");
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.SavedIndex = 0;
            this.dockPanel2.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel2_Container
            // 
            resources.ApplyResources(this.dockPanel2_Container, "dockPanel2_Container");
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // frmReportGallery
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rptViewer);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.hideContainerBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmReportGallery";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportGallery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nvbReport)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel3.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dpReportMenu.ResumeLayout(false);
            this.dockPanel4_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barReportMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager2)).EndInit();
            this.hideContainerBottom.ResumeLayout(false);
            this.dpReportDescription.ResumeLayout(false);
            this.dockPanel5_Container.ResumeLayout(false);
            this.dockPanel5_Container.PerformLayout();
            this.dockPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl nvbReport;
        private DevExpress.XtraNavBar.NavBarGroup nvbGroup1;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblReportTitle;
        private ReportViewer rptViewer;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpReportDescription;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel5_Container;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraBars.Docking.DockPanel dpReportMenu;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel4_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockManager dockManager2;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barReportMenu;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarSubItem barSubItemReports1;
        private DevExpress.XtraBars.BarSubItem barSubItemAbstract;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerBottom;
        private DevExpress.XtraBars.Bar barMenu;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarSubItem barSubItemReports;
    }
}