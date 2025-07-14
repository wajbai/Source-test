namespace ACPP.Modules.Inventory.Asset
{
    partial class frmAssetDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetDashboard));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.trlLocation = new DevExpress.XtraTreeList.TreeList();
            this.colTrlId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLocation = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.ucToolBar1 = new Bosco.Utility.Controls.ucToolBar();
            this.gcAssetDashboard = new DevExpress.XtraGrid.GridControl();
            this.gvAssetDashboard = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAssetItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAsset = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustodian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSourceFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblSymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.trlLocation);
            this.layoutControl1.Controls.Add(this.ucToolBar1);
            this.layoutControl1.Controls.Add(this.gcAssetDashboard);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(228, 265, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1138, 484);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // trlLocation
            // 
            this.trlLocation.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlLocation.Appearance.FocusedRow.Options.UseFont = true;
            this.trlLocation.Appearance.FocusedRow.Options.UseForeColor = true;
            this.trlLocation.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colTrlId,
            this.colParentId,
            this.colLocation});
            this.trlLocation.DragNodesMode = DevExpress.XtraTreeList.TreeListDragNodesMode.Advanced;
            this.trlLocation.ImageIndexFieldName = "IMAGE_ID";
            this.trlLocation.Location = new System.Drawing.Point(911, 41);
            this.trlLocation.Name = "trlLocation";
            this.trlLocation.OptionsBehavior.Editable = false;
            this.trlLocation.OptionsBehavior.KeepSelectedOnClick = false;
            this.trlLocation.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.trlLocation.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.trlLocation.OptionsView.ShowFocusedFrame = false;
            this.trlLocation.OptionsView.ShowHorzLines = false;
            this.trlLocation.OptionsView.ShowVertLines = false;
            this.trlLocation.ParentFieldName = "PARENT_LOCATION_ID";
            this.trlLocation.SelectImageList = this.imageSmall;
            this.trlLocation.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.Default;
            this.trlLocation.Size = new System.Drawing.Size(221, 420);
            this.trlLocation.TabIndex = 0;
            this.trlLocation.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.trlLocation_FocusedNodeChanged);
            // 
            // colTrlId
            // 
            this.colTrlId.Caption = "Id";
            this.colTrlId.FieldName = "LCOATION_ID";
            this.colTrlId.Name = "colTrlId";
            // 
            // colParentId
            // 
            this.colParentId.Caption = "ParentID";
            this.colParentId.FieldName = "PARENT_ID";
            this.colParentId.Name = "colParentId";
            // 
            // colLocation
            // 
            this.colLocation.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLocation.AppearanceHeader.Options.UseFont = true;
            this.colLocation.Caption = "Location Name";
            this.colLocation.FieldName = "LOCATION_NAME";
            this.colLocation.MinWidth = 33;
            this.colLocation.Name = "colLocation";
            this.colLocation.Visible = true;
            this.colLocation.VisibleIndex = 0;
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "&Add";
            this.ucToolBar1.ChangeCaption = "&Edit";
            this.ucToolBar1.ChangeDeleteCaption = "&Delete";
            this.ucToolBar1.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBar1.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBar1.ChangePrintCaption = "&Print";
            this.ucToolBar1.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBar1.DisableAddButton = false;
            this.ucToolBar1.DisableAMCRenew = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = false;
            this.ucToolBar1.DisableDownloadExcel = false;
            this.ucToolBar1.DisableEditButton = false;
            this.ucToolBar1.DisableMoveTransaction = false;
            this.ucToolBar1.DisableNatureofPayments = false;
            this.ucToolBar1.DisablePostInterest = false;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRestoreVoucher = false;
            this.ucToolBar1.Location = new System.Drawing.Point(6, 6);
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucToolBar1.ShowHTML = true;
            this.ucToolBar1.ShowMMT = true;
            this.ucToolBar1.ShowPDF = true;
            this.ucToolBar1.ShowRTF = true;
            this.ucToolBar1.ShowText = true;
            this.ucToolBar1.ShowXLS = true;
            this.ucToolBar1.ShowXLSX = true;
            this.ucToolBar1.Size = new System.Drawing.Size(1126, 31);
            this.ucToolBar1.TabIndex = 11;
            this.ucToolBar1.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
            // 
            // gcAssetDashboard
            // 
            this.gcAssetDashboard.Location = new System.Drawing.Point(6, 41);
            this.gcAssetDashboard.MainView = this.gvAssetDashboard;
            this.gcAssetDashboard.Name = "gcAssetDashboard";
            this.gcAssetDashboard.Size = new System.Drawing.Size(901, 420);
            this.gcAssetDashboard.TabIndex = 10;
            this.gcAssetDashboard.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetDashboard});
            // 
            // gvAssetDashboard
            // 
            this.gvAssetDashboard.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAssetDashboard.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetDashboard.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAssetItemId,
            this.colProject,
            this.colAsset,
            this.colAssetId,
            this.colGroupName,
            this.colLocationName,
            this.colCustodian,
            this.colSourceFlag,
            this.colStatus,
            this.colLocationId});
            this.gvAssetDashboard.GridControl = this.gcAssetDashboard;
            this.gvAssetDashboard.Name = "gvAssetDashboard";
            this.gvAssetDashboard.OptionsView.ShowAutoFilterRow = true;
            this.gvAssetDashboard.OptionsView.ShowGroupPanel = false;
            this.gvAssetDashboard.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAssetDashboard_FocusedRowChanged);
            this.gvAssetDashboard.RowCountChanged += new System.EventHandler(this.gvAssetDashboard_RowCountChanged);
            // 
            // colAssetItemId
            // 
            this.colAssetItemId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAssetItemId.AppearanceHeader.Options.UseFont = true;
            this.colAssetItemId.Caption = "Asset Item ID";
            this.colAssetItemId.FieldName = "ITEM_ID";
            this.colAssetItemId.Name = "colAssetItemId";
            // 
            // colProject
            // 
            this.colProject.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProject.AppearanceHeader.Options.UseFont = true;
            this.colProject.Caption = "Project";
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            this.colProject.OptionsColumn.AllowFocus = false;
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 0;
            this.colProject.Width = 156;
            // 
            // colAsset
            // 
            this.colAsset.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAsset.AppearanceHeader.Options.UseFont = true;
            this.colAsset.Caption = "Asset";
            this.colAsset.FieldName = "ASSET_NAME";
            this.colAsset.Name = "colAsset";
            this.colAsset.OptionsColumn.AllowEdit = false;
            this.colAsset.OptionsColumn.AllowFocus = false;
            this.colAsset.Visible = true;
            this.colAsset.VisibleIndex = 1;
            this.colAsset.Width = 140;
            // 
            // colAssetId
            // 
            this.colAssetId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAssetId.AppearanceHeader.Options.UseFont = true;
            this.colAssetId.Caption = "Asset ID";
            this.colAssetId.FieldName = "ASSET_ID";
            this.colAssetId.Name = "colAssetId";
            this.colAssetId.OptionsColumn.AllowEdit = false;
            this.colAssetId.OptionsColumn.AllowFocus = false;
            this.colAssetId.Visible = true;
            this.colAssetId.VisibleIndex = 2;
            this.colAssetId.Width = 77;
            // 
            // colGroupName
            // 
            this.colGroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGroupName.AppearanceHeader.Options.UseFont = true;
            this.colGroupName.Caption = "Group ";
            this.colGroupName.FieldName = "GROUP_NAME";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsColumn.AllowEdit = false;
            this.colGroupName.OptionsColumn.AllowFocus = false;
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 3;
            this.colGroupName.Width = 102;
            // 
            // colLocationName
            // 
            this.colLocationName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLocationName.AppearanceHeader.Options.UseFont = true;
            this.colLocationName.Caption = "Location";
            this.colLocationName.FieldName = "LOCATION_NAME";
            this.colLocationName.Name = "colLocationName";
            this.colLocationName.OptionsColumn.AllowEdit = false;
            this.colLocationName.OptionsColumn.AllowFocus = false;
            this.colLocationName.Visible = true;
            this.colLocationName.VisibleIndex = 4;
            this.colLocationName.Width = 109;
            // 
            // colCustodian
            // 
            this.colCustodian.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCustodian.AppearanceHeader.Options.UseFont = true;
            this.colCustodian.Caption = "Custodian";
            this.colCustodian.FieldName = "NAME";
            this.colCustodian.Name = "colCustodian";
            this.colCustodian.OptionsColumn.AllowEdit = false;
            this.colCustodian.OptionsColumn.AllowFocus = false;
            this.colCustodian.Visible = true;
            this.colCustodian.VisibleIndex = 5;
            this.colCustodian.Width = 110;
            // 
            // colSourceFlag
            // 
            this.colSourceFlag.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSourceFlag.AppearanceHeader.Options.UseFont = true;
            this.colSourceFlag.Caption = "Source ";
            this.colSourceFlag.FieldName = "SOURCE_FLAG";
            this.colSourceFlag.Name = "colSourceFlag";
            this.colSourceFlag.OptionsColumn.AllowEdit = false;
            this.colSourceFlag.OptionsColumn.AllowFocus = false;
            this.colSourceFlag.Visible = true;
            this.colSourceFlag.VisibleIndex = 6;
            this.colSourceFlag.Width = 85;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 7;
            this.colStatus.Width = 104;
            // 
            // colLocationId
            // 
            this.colLocationId.Caption = "LocationId";
            this.colLocationId.FieldName = "LOCATION_ID";
            this.colLocationId.Name = "colLocationId";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.emptySpaceItem6,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.lblCount,
            this.lblSymbol,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1138, 484);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcAssetDashboard;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(905, 424);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBar1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 35);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1130, 35);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 459);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(703, 17);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(713, 459);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(118, 17);
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.trlLocation;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(905, 35);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(225, 424);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(1073, 459);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(57, 17);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCount.CustomizationFormText = "0";
            this.lblCount.Location = new System.Drawing.Point(844, 459);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(229, 17);
            this.lblCount.Text = "0";
            this.lblCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblSymbol
            // 
            this.lblSymbol.AllowHotTrack = false;
            this.lblSymbol.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSymbol.AppearanceItemCaption.Options.UseFont = true;
            this.lblSymbol.CustomizationFormText = "#";
            this.lblSymbol.Location = new System.Drawing.Point(831, 459);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Size = new System.Drawing.Size(13, 17);
            this.lblSymbol.Text = "#";
            this.lblSymbol.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(703, 459);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 17);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmAssetDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 484);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAssetDashboard";
            this.Text = "Asset Dashboard";
            this.Load += new System.EventHandler(this.frmAssetDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetDashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetDashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraGrid.GridControl gcAssetDashboard;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetDashboard;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Bosco.Utility.Controls.ucToolBar ucToolBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colAsset;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private DevExpress.XtraGrid.Columns.GridColumn colSourceFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraTreeList.TreeList trlLocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTrlId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLocation;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblSymbol;
        private DevExpress.XtraGrid.Columns.GridColumn colCustodian;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.Utils.ImageCollection imageSmall;
    }
}