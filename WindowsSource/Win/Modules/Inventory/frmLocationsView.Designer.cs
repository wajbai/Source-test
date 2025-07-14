namespace ACPP.Modules.Inventory
{
    partial class frmLocationsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocationsView));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcLocationView = new DevExpress.XtraGrid.GridControl();
            this.gvLocationView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustodians = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBlock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResponsibleFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.uctbLocationView = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.chkShoeFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCountSymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.gccolProject = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLocationView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocationView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShoeFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcLocationView);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.uctbLocationView);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(27, 272, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // gcLocationView
            // 
            resources.ApplyResources(this.gcLocationView, "gcLocationView");
            this.gcLocationView.MainView = this.gvLocationView;
            this.gcLocationView.Name = "gcLocationView";
            this.gcLocationView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLocationView});
            this.gcLocationView.DoubleClick += new System.EventHandler(this.gcLocationView_DoubleClick);
            // 
            // gvLocationView
            // 
            this.gvLocationView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLocationView.Appearance.FocusedRow.Font")));
            this.gvLocationView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLocationView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLocationView.Appearance.HeaderPanel.Font")));
            this.gvLocationView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLocationView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colLocationName,
            this.colCustodians,
            this.colBlock,
            this.gccolProject,
            this.colResponsibleFrom,
            this.colLocationType,
            this.colProjectID});
            this.gvLocationView.GridControl = this.gcLocationView;
            this.gvLocationView.Name = "gvLocationView";
            this.gvLocationView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvLocationView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvLocationView.OptionsBehavior.Editable = false;
            this.gvLocationView.OptionsBehavior.ReadOnly = true;
            this.gvLocationView.OptionsCustomization.AllowColumnMoving = false;
            this.gvLocationView.OptionsCustomization.AllowSort = false;
            this.gvLocationView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLocationView.OptionsView.ShowGroupPanel = false;
            this.gvLocationView.RowCountChanged += new System.EventHandler(this.gvLocationView_RowCountChanged);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "LOCATION_ID";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            // 
            // colLocationName
            // 
            this.colLocationName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLocationName.AppearanceHeader.Font")));
            this.colLocationName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLocationName, "colLocationName");
            this.colLocationName.FieldName = "LOCATION";
            this.colLocationName.Name = "colLocationName";
            this.colLocationName.OptionsColumn.AllowEdit = false;
            // 
            // colCustodians
            // 
            this.colCustodians.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCustodians.AppearanceHeader.Font")));
            this.colCustodians.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCustodians, "colCustodians");
            this.colCustodians.FieldName = "CUSTODIAN";
            this.colCustodians.Name = "colCustodians";
            this.colCustodians.OptionsColumn.AllowEdit = false;
            // 
            // colBlock
            // 
            this.colBlock.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBlock.AppearanceHeader.Font")));
            this.colBlock.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBlock, "colBlock");
            this.colBlock.FieldName = "BLOCK";
            this.colBlock.Name = "colBlock";
            this.colBlock.OptionsColumn.AllowEdit = false;
            // 
            // colResponsibleFrom
            // 
            this.colResponsibleFrom.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colResponsibleFrom.AppearanceHeader.Font")));
            this.colResponsibleFrom.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colResponsibleFrom, "colResponsibleFrom");
            this.colResponsibleFrom.FieldName = "RESPONSIBLE_FROM";
            this.colResponsibleFrom.Name = "colResponsibleFrom";
            this.colResponsibleFrom.OptionsColumn.AllowEdit = false;
            // 
            // colLocationType
            // 
            this.colLocationType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLocationType.AppearanceHeader.Font")));
            this.colLocationType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLocationType, "colLocationType");
            this.colLocationType.FieldName = "LOCATION_TYPE";
            this.colLocationType.Name = "colLocationType";
            this.colLocationType.OptionsColumn.AllowEdit = false;
            // 
            // colProjectID
            // 
            resources.ApplyResources(this.colProjectID, "colProjectID");
            this.colProjectID.FieldName = "PROJECT_ID";
            this.colProjectID.Name = "colProjectID";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // uctbLocationView
            // 
            this.uctbLocationView.ChangeAddCaption = "&Add";
            this.uctbLocationView.ChangeCaption = "&Edit";
            this.uctbLocationView.ChangeDeleteCaption = "&Delete";
            this.uctbLocationView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.uctbLocationView.ChangeMoveVoucherTooltip = superToolTip1;
            this.uctbLocationView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.uctbLocationView.ChangePostInterestCaption = "P&ost Interest";
            this.uctbLocationView.ChangePrintCaption = "&Print";
            this.uctbLocationView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.uctbLocationView.DisableAddButton = true;
            this.uctbLocationView.DisableAMCRenew = true;
            this.uctbLocationView.DisableCloseButton = true;
            this.uctbLocationView.DisableDeleteButton = true;
            this.uctbLocationView.DisableDownloadExcel = true;
            this.uctbLocationView.DisableEditButton = true;
            this.uctbLocationView.DisableInsertVoucher = true;
            this.uctbLocationView.DisableMoveTransaction = true;
            this.uctbLocationView.DisableNatureofPayments = true;
            this.uctbLocationView.DisablePostInterest = true;
            this.uctbLocationView.DisablePrintButton = true;
            this.uctbLocationView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.uctbLocationView, "uctbLocationView");
            this.uctbLocationView.Name = "uctbLocationView";
            this.uctbLocationView.ShowHTML = true;
            this.uctbLocationView.ShowMMT = true;
            this.uctbLocationView.ShowPDF = true;
            this.uctbLocationView.ShowRTF = true;
            this.uctbLocationView.ShowText = true;
            this.uctbLocationView.ShowXLS = true;
            this.uctbLocationView.ShowXLSX = true;
            this.uctbLocationView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.uctbLocationView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.uctbLocationView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.AddClicked += new System.EventHandler(this.uctbLocationView_AddClicked);
            this.uctbLocationView.EditClicked += new System.EventHandler(this.uctbLocationView_EditClicked);
            this.uctbLocationView.DeleteClicked += new System.EventHandler(this.uctbLocationView_DeleteClicked);
            this.uctbLocationView.PrintClicked += new System.EventHandler(this.uctbLocationView_PrintClicked);
            this.uctbLocationView.CloseClicked += new System.EventHandler(this.uctbLocationView_CloseClicked);
            this.uctbLocationView.RefreshClicked += new System.EventHandler(this.uctbLocationView_RefreshClicked);
            this.uctbLocationView.DownloadExcel += new System.EventHandler(this.uctbLocationView_DownloadExcel);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.chkShoeFilter,
            this.layoutControlItem1,
            this.lblCountSymbol,
            this.lblCount});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(916, 411);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.uctbLocationView;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(196, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(916, 28);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(79, 388);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(794, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // chkShoeFilter
            // 
            this.chkShoeFilter.AllowHtmlStringInCaption = true;
            this.chkShoeFilter.Control = this.chkShowFilter;
            resources.ApplyResources(this.chkShoeFilter, "chkShoeFilter");
            this.chkShoeFilter.Location = new System.Drawing.Point(0, 388);
            this.chkShoeFilter.MinSize = new System.Drawing.Size(79, 23);
            this.chkShoeFilter.Name = "chkShoeFilter";
            this.chkShoeFilter.Size = new System.Drawing.Size(79, 23);
            this.chkShoeFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.chkShoeFilter.TextSize = new System.Drawing.Size(0, 0);
            this.chkShoeFilter.TextToControlDistance = 0;
            this.chkShoeFilter.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcLocationView;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(916, 360);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblCountSymbol
            // 
            this.lblCountSymbol.AllowHotTrack = false;
            this.lblCountSymbol.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCountSymbol.AppearanceItemCaption.Font")));
            this.lblCountSymbol.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCountSymbol, "lblCountSymbol");
            this.lblCountSymbol.Location = new System.Drawing.Point(873, 388);
            this.lblCountSymbol.MaxSize = new System.Drawing.Size(11, 23);
            this.lblCountSymbol.MinSize = new System.Drawing.Size(11, 23);
            this.lblCountSymbol.Name = "lblCountSymbol";
            this.lblCountSymbol.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCountSymbol.Size = new System.Drawing.Size(11, 23);
            this.lblCountSymbol.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountSymbol.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.AppearanceItemCaption.Font")));
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Location = new System.Drawing.Point(884, 388);
            this.lblCount.MaxSize = new System.Drawing.Size(32, 23);
            this.lblCount.MinSize = new System.Drawing.Size(32, 23);
            this.lblCount.Name = "lblCount";
            this.lblCount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCount.Size = new System.Drawing.Size(32, 23);
            this.lblCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // gccolProject
            // 
            this.gccolProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gridColumn1.AppearanceHeader.Font")));
            this.gccolProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gccolProject, "gccolProject");
            this.gccolProject.FieldName = "PROJECT";
            this.gccolProject.Name = "gccolProject";
            // 
            // frmLocationsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmLocationsView";
            this.ShowFilterClicked += new System.EventHandler(this.frmLocationsView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmLocationsView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmLocationsView_Activated);
            this.Load += new System.EventHandler(this.frmLocationsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLocationView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocationView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShoeFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar uctbLocationView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem chkShoeFilter;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountSymbol;
        private DevExpress.Utils.ImageCollection imageSmall;
        private DevExpress.XtraGrid.GridControl gcLocationView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLocationView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustodians;
        private DevExpress.XtraGrid.Columns.GridColumn colBlock;
        private DevExpress.XtraGrid.Columns.GridColumn colResponsibleFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationType;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectID;
        private DevExpress.XtraGrid.Columns.GridColumn gccolProject;
    }
}