namespace ACPP.Modules.UIControls
{
    partial class ucVoucherShortcut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucVoucherShortcut));
            this.barRightShortcut = new DevExpress.XtraBars.Bar();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiDate = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNextVoucherDate = new DevExpress.XtraBars.BarButtonItem();
            this.bbiProject = new DevExpress.XtraBars.BarButtonItem();
            this.bbiReceipts = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPayment = new DevExpress.XtraBars.BarButtonItem();
            this.bbiContra = new DevExpress.XtraBars.BarButtonItem();
            this.bbiJournal = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLedgerAdd = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBankAccount = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCostCentre = new DevExpress.XtraBars.BarButtonItem();
            this.bbiConfigure = new DevExpress.XtraBars.BarButtonItem();
            this.bbiTransactionVoucherView = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMapping = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLedgerOptions = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDonor = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteVoucher = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBottomShortcut = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barRightShortcut
            // 
            this.barRightShortcut.BarAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("barRightShortcut.BarAppearance.Normal.Font")));
            this.barRightShortcut.BarAppearance.Normal.Options.UseFont = true;
            this.barRightShortcut.BarName = "Right ShortCut";
            this.barRightShortcut.DockCol = 0;
            this.barRightShortcut.DockRow = 0;
            this.barRightShortcut.DockStyle = DevExpress.XtraBars.BarDockStyle.Right;
            this.barRightShortcut.FloatLocation = new System.Drawing.Point(901, 219);
            this.barRightShortcut.FloatSize = new System.Drawing.Size(114, 290);
            this.barRightShortcut.OptionsBar.AllowQuickCustomization = false;
            this.barRightShortcut.OptionsBar.DisableClose = true;
            this.barRightShortcut.OptionsBar.DisableCustomization = true;
            this.barRightShortcut.OptionsBar.DrawBorder = false;
            this.barRightShortcut.OptionsBar.DrawDragBorder = false;
            this.barRightShortcut.OptionsBar.MultiLine = true;
            this.barRightShortcut.OptionsBar.RotateWhenVertical = false;
            this.barRightShortcut.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.barRightShortcut, "barRightShortcut");
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
            this.bbiDate,
            this.bbiProject,
            this.bbiReceipts,
            this.bbiPayment,
            this.bbiContra,
            this.bbiJournal,
            this.bbiCostCentre,
            this.bbiDonor,
            this.bbiConfigure,
            this.bbiBankAccount,
            this.bbiMapping,
            this.bbiTransactionVoucherView,
            this.bbiLedgerAdd,
            this.bbiLedgerOptions,
            this.bbiNextVoucherDate,
            this.bbiDeleteVoucher});
            this.barManager1.MaxItemId = 26;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(31, 139);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDate, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNextVoucherDate, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiProject, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiReceipts, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPayment, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiContra, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiJournal, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLedgerAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiBankAccount, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCostCentre, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiConfigure, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiTransactionVoucherView, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMapping, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLedgerOptions, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDonor, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeleteVoucher)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // bbiDate
            // 
            this.bbiDate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiDate.AllowAllUp = true;
            this.bbiDate.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDate, "bbiDate");
            this.bbiDate.Id = 2;
            this.bbiDate.Name = "bbiDate";
            this.bbiDate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDate_ItemClick);
            // 
            // bbiNextVoucherDate
            // 
            this.bbiNextVoucherDate.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiNextVoucherDate, "bbiNextVoucherDate");
            this.bbiNextVoucherDate.Id = 24;
            this.bbiNextVoucherDate.Name = "bbiNextVoucherDate";
            this.bbiNextVoucherDate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNextVoucherDate_ItemClick);
            // 
            // bbiProject
            // 
            this.bbiProject.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiProject.AllowAllUp = true;
            this.bbiProject.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiProject, "bbiProject");
            this.bbiProject.Id = 3;
            this.bbiProject.Name = "bbiProject";
            this.bbiProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiProject_ItemClick);
            // 
            // bbiReceipts
            // 
            this.bbiReceipts.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiReceipts.AllowAllUp = true;
            this.bbiReceipts.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiReceipts, "bbiReceipts");
            this.bbiReceipts.Id = 4;
            this.bbiReceipts.Name = "bbiReceipts";
            this.bbiReceipts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiReceipts_ItemClick);
            // 
            // bbiPayment
            // 
            this.bbiPayment.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiPayment.AllowAllUp = true;
            this.bbiPayment.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiPayment, "bbiPayment");
            this.bbiPayment.Id = 5;
            this.bbiPayment.Name = "bbiPayment";
            this.bbiPayment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPayment_ItemClick);
            // 
            // bbiContra
            // 
            this.bbiContra.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiContra.AllowAllUp = true;
            this.bbiContra.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiContra, "bbiContra");
            this.bbiContra.Id = 6;
            this.bbiContra.Name = "bbiContra";
            this.bbiContra.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiContra_ItemClick);
            // 
            // bbiJournal
            // 
            this.bbiJournal.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiJournal.AllowAllUp = true;
            this.bbiJournal.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiJournal, "bbiJournal");
            this.bbiJournal.Id = 7;
            this.bbiJournal.Name = "bbiJournal";
            this.bbiJournal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiJournal_ItemClick);
            // 
            // bbiLedgerAdd
            // 
            this.bbiLedgerAdd.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiLedgerAdd, "bbiLedgerAdd");
            this.bbiLedgerAdd.Id = 22;
            this.bbiLedgerAdd.Name = "bbiLedgerAdd";
            this.bbiLedgerAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLedgerAdd_ItemClick);
            // 
            // bbiBankAccount
            // 
            this.bbiBankAccount.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiBankAccount.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiBankAccount, "bbiBankAccount");
            this.bbiBankAccount.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiBankAccount.Glyph")));
            this.bbiBankAccount.Id = 13;
            this.bbiBankAccount.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiBankAccount.LargeGlyph")));
            this.bbiBankAccount.Name = "bbiBankAccount";
            this.bbiBankAccount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBankAccount_ItemClick);
            // 
            // bbiCostCentre
            // 
            this.bbiCostCentre.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiCostCentre.AllowAllUp = true;
            this.bbiCostCentre.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiCostCentre, "bbiCostCentre");
            this.bbiCostCentre.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiCostCentre.Glyph")));
            this.bbiCostCentre.Id = 9;
            this.bbiCostCentre.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiCostCentre.LargeGlyph")));
            this.bbiCostCentre.Name = "bbiCostCentre";
            this.bbiCostCentre.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCostCentre_ItemClick);
            // 
            // bbiConfigure
            // 
            this.bbiConfigure.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiConfigure.AllowAllUp = true;
            this.bbiConfigure.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiConfigure, "bbiConfigure");
            this.bbiConfigure.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiConfigure.Glyph")));
            this.bbiConfigure.Id = 11;
            this.bbiConfigure.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiConfigure.LargeGlyph")));
            this.bbiConfigure.Name = "bbiConfigure";
            this.bbiConfigure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiConfigure_ItemClick);
            // 
            // bbiTransactionVoucherView
            // 
            this.bbiTransactionVoucherView.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiTransactionVoucherView, "bbiTransactionVoucherView");
            this.bbiTransactionVoucherView.Id = 16;
            this.bbiTransactionVoucherView.Name = "bbiTransactionVoucherView";
            this.bbiTransactionVoucherView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiTransactionVoucherView_ItemClick);
            // 
            // bbiMapping
            // 
            this.bbiMapping.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiMapping, "bbiMapping");
            this.bbiMapping.Id = 15;
            this.bbiMapping.Name = "bbiMapping";
            this.bbiMapping.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMapping_ItemClick);
            // 
            // bbiLedgerOptions
            // 
            this.bbiLedgerOptions.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiLedgerOptions, "bbiLedgerOptions");
            this.bbiLedgerOptions.Id = 23;
            this.bbiLedgerOptions.Name = "bbiLedgerOptions";
            this.bbiLedgerOptions.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLedgerOptions_ItemClick);
            // 
            // bbiDonor
            // 
            this.bbiDonor.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.bbiDonor.AllowAllUp = true;
            this.bbiDonor.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDonor, "bbiDonor");
            this.bbiDonor.Id = 10;
            this.bbiDonor.Name = "bbiDonor";
            this.bbiDonor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDonor_ItemClick);
            // 
            // bbiDeleteVoucher
            // 
            this.bbiDeleteVoucher.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDeleteVoucher, "bbiDeleteVoucher");
            this.bbiDeleteVoucher.Id = 25;
            this.bbiDeleteVoucher.Name = "bbiDeleteVoucher";
            this.bbiDeleteVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiDeleteVoucher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDeleteVoucher_ItemClick);
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
            // barBottomShortcut
            // 
            this.barBottomShortcut.BarAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("barBottomShortcut.BarAppearance.Normal.Font")));
            this.barBottomShortcut.BarAppearance.Normal.Options.UseFont = true;
            this.barBottomShortcut.BarName = "Bottom ShortCut";
            this.barBottomShortcut.DockCol = 0;
            this.barBottomShortcut.DockRow = 0;
            this.barBottomShortcut.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barBottomShortcut.FloatLocation = new System.Drawing.Point(-45, 566);
            this.barBottomShortcut.OptionsBar.AllowCollapse = true;
            this.barBottomShortcut.OptionsBar.AllowQuickCustomization = false;
            this.barBottomShortcut.OptionsBar.DisableClose = true;
            this.barBottomShortcut.OptionsBar.DisableCustomization = true;
            this.barBottomShortcut.OptionsBar.DrawBorder = false;
            this.barBottomShortcut.OptionsBar.MultiLine = true;
            this.barBottomShortcut.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.barBottomShortcut, "barBottomShortcut");
            // 
            // ucVoucherShortcut
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucVoucherShortcut";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Bar barRightShortcut;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar barBottomShortcut;
        private DevExpress.XtraBars.BarButtonItem bbiDate;
        private DevExpress.XtraBars.BarButtonItem bbiProject;
        private DevExpress.XtraBars.BarButtonItem bbiReceipts;
        private DevExpress.XtraBars.BarButtonItem bbiPayment;
        private DevExpress.XtraBars.BarButtonItem bbiContra;
        private DevExpress.XtraBars.BarButtonItem bbiJournal;
        private DevExpress.XtraBars.BarButtonItem bbiCostCentre;
        private DevExpress.XtraBars.BarButtonItem bbiDonor;
        private DevExpress.XtraBars.BarButtonItem bbiConfigure;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bbiBankAccount;
        private DevExpress.XtraBars.BarButtonItem bbiMapping;
        private DevExpress.XtraBars.BarButtonItem bbiTransactionVoucherView;
        private DevExpress.XtraBars.BarButtonItem bbiLedgerAdd;
        private DevExpress.XtraBars.BarButtonItem bbiLedgerOptions;
        private DevExpress.XtraBars.BarButtonItem bbiNextVoucherDate;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteVoucher;
    }
}
