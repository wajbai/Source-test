namespace ACPP.Modules.Inventory.Stock
{
    partial class frmStockJournalView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockJournalView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ucStockJournal = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcStockJornal = new DevExpress.XtraGrid.GridControl();
            this.gvStockJornal = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCountSymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockJornal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockJornal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblCount);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcStockJornal);
            this.layoutControl1.Controls.Add(this.ucStockJournal);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(418, 236, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem5,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.lblCountSymbol});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(948, 415);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(81, 381);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(797, 24);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucStockJournal
            // 
            this.ucStockJournal.ChangeAddCaption = "&Add";
            this.ucStockJournal.ChangeCaption = "&Edit";
            this.ucStockJournal.ChangeDeleteCaption = "&Delete";
            this.ucStockJournal.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucStockJournal.ChangePrintCaption = "&Print";
            this.ucStockJournal.DisableAddButton = true;
            this.ucStockJournal.DisableCloseButton = true;
            this.ucStockJournal.DisableDeleteButton = true;
            this.ucStockJournal.DisableDownloadExcel = true;
            this.ucStockJournal.DisableEditButton = true;
            this.ucStockJournal.DisableMoveTransaction = true;
            this.ucStockJournal.DisableNatureofPayments = true;
            this.ucStockJournal.DisablePrintButton = true;
            this.ucStockJournal.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucStockJournal, "ucStockJournal");
            this.ucStockJournal.Name = "ucStockJournal";
            this.ucStockJournal.ShowHTML = true;
            this.ucStockJournal.ShowMMT = true;
            this.ucStockJournal.ShowPDF = true;
            this.ucStockJournal.ShowRTF = true;
            this.ucStockJournal.ShowText = true;
            this.ucStockJournal.ShowXLS = true;
            this.ucStockJournal.ShowXLSX = true;
            this.ucStockJournal.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockJournal.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStockJournal.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStockJournal.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockJournal.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockJournal.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStockJournal.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockJournal.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockJournal.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockJournal.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStockJournal.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStockJournal.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucStockJournal;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(938, 32);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // gcStockJornal
            // 
            resources.ApplyResources(this.gcStockJornal, "gcStockJornal");
            this.gcStockJornal.MainView = this.gvStockJornal;
            this.gcStockJornal.Name = "gcStockJornal";
            this.gcStockJornal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStockJornal});
            // 
            // gvStockJornal
            // 
            this.gvStockJornal.GridControl = this.gcStockJornal;
            this.gvStockJornal.Name = "gvStockJornal";
            this.gvStockJornal.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcStockJornal;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(938, 349);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("checkEdit1.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 381);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(81, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.StyleController = this.layoutControl1;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblCount;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(893, 381);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(11, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(45, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblCountSymbol
            // 
            this.lblCountSymbol.AllowHotTrack = false;
            this.lblCountSymbol.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.lblCountSymbol.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCountSymbol, "lblCountSymbol");
            this.lblCountSymbol.Location = new System.Drawing.Point(878, 381);
            this.lblCountSymbol.Name = "lblCountSymbol";
            this.lblCountSymbol.Size = new System.Drawing.Size(15, 24);
            this.lblCountSymbol.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmStockJournalView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmStockJournalView";
            this.Load += new System.EventHandler(this.frmStockJournalView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockJornal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockJornal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraGrid.GridControl gcStockJornal;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStockJornal;
        private Bosco.Utility.Controls.ucToolBar ucStockJournal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountSymbol;
    }
}