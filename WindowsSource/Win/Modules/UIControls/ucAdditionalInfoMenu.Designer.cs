namespace ACPP.Modules.UIControls
{
    partial class ucAdditionalInfoMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAdditionalInfoMenu));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiDonorInfo = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteVocuher = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPrintVoucher = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAttachVoucherBills = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEntryMethod = new DevExpress.XtraBars.BarButtonItem();
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
            this.bbiDonorInfo,
            this.bbiEntryMethod,
            this.bbiDeleteVocuher,
            this.bbiPrintVoucher,
            this.bbiAttachVoucherBills});
            this.barManager1.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarAppearance.Normal.BackColor2 = ((System.Drawing.Color)(resources.GetObject("bar1.BarAppearance.Normal.BackColor2")));
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDonorInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeleteVocuher, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPrintVoucher, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAttachVoucherBills, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiEntryMethod)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // bbiDonorInfo
            // 
            this.bbiDonorInfo.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDonorInfo, "bbiDonorInfo");
            this.bbiDonorInfo.Id = 0;
            this.bbiDonorInfo.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("bbiDonorInfo.ItemAppearance.Normal.Font")));
            this.bbiDonorInfo.ItemAppearance.Normal.ForeColor = ((System.Drawing.Color)(resources.GetObject("bbiDonorInfo.ItemAppearance.Normal.ForeColor")));
            this.bbiDonorInfo.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiDonorInfo.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bbiDonorInfo.Name = "bbiDonorInfo";
            this.bbiDonorInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDonorInfo_ItemClick);
            // 
            // bbiDeleteVocuher
            // 
            this.bbiDeleteVocuher.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDeleteVocuher, "bbiDeleteVocuher");
            this.bbiDeleteVocuher.Id = 4;
            this.bbiDeleteVocuher.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("bbiDeleteVocuher.ItemAppearance.Normal.Font")));
            this.bbiDeleteVocuher.ItemAppearance.Normal.ForeColor = ((System.Drawing.Color)(resources.GetObject("bbiDeleteVocuher.ItemAppearance.Normal.ForeColor")));
            this.bbiDeleteVocuher.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiDeleteVocuher.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bbiDeleteVocuher.Name = "bbiDeleteVocuher";
            this.bbiDeleteVocuher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDeleteVocuher_ItemClick);
            // 
            // bbiPrintVoucher
            // 
            this.bbiPrintVoucher.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiPrintVoucher, "bbiPrintVoucher");
            this.bbiPrintVoucher.Id = 6;
            this.bbiPrintVoucher.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("bbiPrintVoucher.ItemAppearance.Normal.Font")));
            this.bbiPrintVoucher.ItemAppearance.Normal.ForeColor = ((System.Drawing.Color)(resources.GetObject("bbiPrintVoucher.ItemAppearance.Normal.ForeColor")));
            this.bbiPrintVoucher.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiPrintVoucher.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bbiPrintVoucher.Name = "bbiPrintVoucher";
            this.bbiPrintVoucher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPrintVoucher_ItemClick);
            // 
            // bbiAttachVoucherBills
            // 
            this.bbiAttachVoucherBills.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiAttachVoucherBills, "bbiAttachVoucherBills");
            this.bbiAttachVoucherBills.Id = 7;
            this.bbiAttachVoucherBills.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("bbiAttachVoucherBills.ItemAppearance.Normal.Font")));
            this.bbiAttachVoucherBills.ItemAppearance.Normal.ForeColor = ((System.Drawing.Color)(resources.GetObject("bbiAttachVoucherBills.ItemAppearance.Normal.ForeColor")));
            this.bbiAttachVoucherBills.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiAttachVoucherBills.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bbiAttachVoucherBills.Name = "bbiAttachVoucherBills";
            this.bbiAttachVoucherBills.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAttachVoucherBills_ItemClick);
            // 
            // bbiEntryMethod
            // 
            this.bbiEntryMethod.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbiEntryMethod.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiEntryMethod, "bbiEntryMethod");
            this.bbiEntryMethod.Id = 3;
            this.bbiEntryMethod.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("bbiEntryMethod.ItemAppearance.Normal.Font")));
            this.bbiEntryMethod.ItemAppearance.Normal.ForeColor = ((System.Drawing.Color)(resources.GetObject("bbiEntryMethod.ItemAppearance.Normal.ForeColor")));
            this.bbiEntryMethod.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiEntryMethod.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bbiEntryMethod.Name = "bbiEntryMethod";
            this.bbiEntryMethod.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEntryMethod_ItemClick);
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
            // ucAdditionalInfoMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucAdditionalInfoMenu";
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
        private DevExpress.XtraBars.BarButtonItem bbiDonorInfo;
        private DevExpress.XtraBars.BarButtonItem bbiEntryMethod;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteVocuher;
        private DevExpress.XtraBars.BarButtonItem bbiPrintVoucher;
        private DevExpress.XtraBars.BarButtonItem bbiAttachVoucherBills;
    }
}
