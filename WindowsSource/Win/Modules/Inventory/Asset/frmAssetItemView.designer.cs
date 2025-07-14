namespace ACPP.Modules.Inventory.Asset
{
    partial class frmAssetItemView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetItemView));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.gvAssetItemDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBlock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustodian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAssetItems = new DevExpress.XtraGrid.GridControl();
            this.gvAssetItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRetentionYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepreciation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colParentClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWidth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucAssetItemsView = new Bosco.Utility.Controls.ucToolBar();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItemDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvAssetItemDetail
            // 
            this.gvAssetItemDetail.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetItemDetail.Appearance.FocusedRow.Font")));
            this.gvAssetItemDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetItemDetail.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetItemDetail.Appearance.HeaderPanel.Font")));
            this.gvAssetItemDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetItemDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemId,
            this.colAssetID,
            this.colBlock,
            this.colLocation,
            this.colCustodian,
            this.colAmount,
            this.colType,
            this.colStatus});
            this.gvAssetItemDetail.CustomizationFormBounds = new System.Drawing.Rectangle(808, 436, 216, 190);
            this.gvAssetItemDetail.GridControl = this.gcAssetItems;
            this.gvAssetItemDetail.Name = "gvAssetItemDetail";
            this.gvAssetItemDetail.OptionsBehavior.Editable = false;
            this.gvAssetItemDetail.OptionsBehavior.ReadOnly = true;
            this.gvAssetItemDetail.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetItemDetail.OptionsView.ShowGroupPanel = false;
            // 
            // colItemId
            // 
            resources.ApplyResources(this.colItemId, "colItemId");
            this.colItemId.FieldName = "ITEM_ID";
            this.colItemId.Name = "colItemId";
            // 
            // colAssetID
            // 
            resources.ApplyResources(this.colAssetID, "colAssetID");
            this.colAssetID.FieldName = "ASSET_ID";
            this.colAssetID.Name = "colAssetID";
            this.colAssetID.OptionsColumn.AllowEdit = false;
            this.colAssetID.OptionsColumn.AllowFocus = false;
            this.colAssetID.OptionsColumn.ReadOnly = true;
            // 
            // colBlock
            // 
            resources.ApplyResources(this.colBlock, "colBlock");
            this.colBlock.FieldName = "BLOCK";
            this.colBlock.Name = "colBlock";
            this.colBlock.OptionsColumn.AllowEdit = false;
            this.colBlock.OptionsColumn.AllowFocus = false;
            this.colBlock.OptionsFilter.AllowAutoFilter = false;
            this.colBlock.OptionsFilter.AllowFilter = false;
            // 
            // colLocation
            // 
            resources.ApplyResources(this.colLocation, "colLocation");
            this.colLocation.FieldName = "LOCATION";
            this.colLocation.Name = "colLocation";
            this.colLocation.OptionsColumn.AllowEdit = false;
            this.colLocation.OptionsColumn.AllowFocus = false;
            this.colLocation.OptionsColumn.ReadOnly = true;
            // 
            // colCustodian
            // 
            resources.ApplyResources(this.colCustodian, "colCustodian");
            this.colCustodian.FieldName = "CUSTODIAN";
            this.colCustodian.Name = "colCustodian";
            this.colCustodian.OptionsColumn.AllowEdit = false;
            this.colCustodian.OptionsColumn.AllowFocus = false;
            this.colCustodian.OptionsColumn.FixedWidth = true;
            this.colCustodian.OptionsFilter.AllowAutoFilter = false;
            this.colCustodian.OptionsFilter.AllowFilter = false;
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.AllowFocus = false;
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.OptionsColumn.ReadOnly = true;
            // 
            // colType
            // 
            resources.ApplyResources(this.colType, "colType");
            this.colType.FieldName = "FLAG";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.OptionsColumn.AllowFocus = false;
            this.colType.OptionsColumn.FixedWidth = true;
            this.colType.OptionsColumn.ReadOnly = true;
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.OptionsColumn.FixedWidth = true;
            this.colStatus.OptionsColumn.ReadOnly = true;
            // 
            // gcAssetItems
            // 
            gridLevelNode1.LevelTemplate = this.gvAssetItemDetail;
            gridLevelNode1.RelationName = "Item Details";
            this.gcAssetItems.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            resources.ApplyResources(this.gcAssetItems, "gcAssetItems");
            this.gcAssetItems.MainView = this.gvAssetItems;
            this.gcAssetItems.Name = "gcAssetItems";
            this.gcAssetItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetItems,
            this.gvAssetItemDetail});
            // 
            // gvAssetItems
            // 
            this.gvAssetItems.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetItems.Appearance.FocusedRow.Font")));
            this.gvAssetItems.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetItems.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetItems.Appearance.HeaderPanel.Font")));
            this.gvAssetItems.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colAssetItem,
            this.colAssetClass,
            this.colCategory,
            this.colUnit,
            this.colRetentionYear,
            this.colDepreciation,
            this.colQuantity,
            this.colParentClass,
            this.colWidth});
            this.gvAssetItems.GridControl = this.gcAssetItems;
            this.gvAssetItems.Name = "gvAssetItems";
            this.gvAssetItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetItems.OptionsView.ShowGroupPanel = false;
            this.gvAssetItems.DoubleClick += new System.EventHandler(this.gvAssetItems_DoubleClick);
            this.gvAssetItems.RowCountChanged += new System.EventHandler(this.gvAssetItems_RowCountChanged);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "ITEM_ID";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            // 
            // colAssetItem
            // 
            this.colAssetItem.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAssetItem.AppearanceHeader.Font")));
            this.colAssetItem.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAssetItem, "colAssetItem");
            this.colAssetItem.FieldName = "ASSET_NAME";
            this.colAssetItem.Name = "colAssetItem";
            this.colAssetItem.OptionsColumn.AllowEdit = false;
            this.colAssetItem.OptionsColumn.AllowFocus = false;
            // 
            // colAssetClass
            // 
            this.colAssetClass.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAssetClass.AppearanceHeader.Font")));
            this.colAssetClass.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAssetClass, "colAssetClass");
            this.colAssetClass.FieldName = "ASSET_GROUP";
            this.colAssetClass.Name = "colAssetClass";
            this.colAssetClass.OptionsColumn.AllowEdit = false;
            this.colAssetClass.OptionsColumn.AllowFocus = false;
            // 
            // colCategory
            // 
            this.colCategory.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCategory.AppearanceHeader.Font")));
            this.colCategory.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCategory, "colCategory");
            this.colCategory.FieldName = "CATEGORY";
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.AllowEdit = false;
            this.colCategory.OptionsColumn.AllowFocus = false;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUnit.AppearanceHeader.Font")));
            this.colUnit.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUnit, "colUnit");
            this.colUnit.FieldName = "SYMBOL";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.AllowFocus = false;
            // 
            // colRetentionYear
            // 
            this.colRetentionYear.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRetentionYear.AppearanceHeader.Font")));
            this.colRetentionYear.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRetentionYear, "colRetentionYear");
            this.colRetentionYear.FieldName = "RETENTION_YRS";
            this.colRetentionYear.Name = "colRetentionYear";
            this.colRetentionYear.OptionsColumn.AllowEdit = false;
            this.colRetentionYear.OptionsColumn.FixedWidth = true;
            this.colRetentionYear.OptionsFilter.AllowAutoFilter = false;
            this.colRetentionYear.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDepreciation
            // 
            this.colDepreciation.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDepreciation.AppearanceHeader.Font")));
            this.colDepreciation.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDepreciation, "colDepreciation");
            this.colDepreciation.FieldName = "DEPRECIATION_YRS";
            this.colDepreciation.Name = "colDepreciation";
            this.colDepreciation.OptionsColumn.AllowEdit = false;
            this.colDepreciation.OptionsColumn.FixedWidth = true;
            this.colDepreciation.OptionsFilter.AllowAutoFilter = false;
            this.colDepreciation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colQuantity
            // 
            resources.ApplyResources(this.colQuantity, "colQuantity");
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.OptionsColumn.AllowFocus = false;
            this.colQuantity.OptionsColumn.FixedWidth = true;
            // 
            // colParentClass
            // 
            resources.ApplyResources(this.colParentClass, "colParentClass");
            this.colParentClass.FieldName = "PARENT_CLASS";
            this.colParentClass.Name = "colParentClass";
            this.colParentClass.OptionsColumn.AllowEdit = false;
            this.colParentClass.OptionsColumn.AllowFocus = false;
            // 
            // colWidth
            // 
            resources.ApplyResources(this.colWidth, "colWidth");
            this.colWidth.FieldName = "WIDTH";
            this.colWidth.Name = "colWidth";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucAssetItemsView);
            this.layoutControl1.Controls.Add(this.gcAssetItems);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(386, 250, 254, 300);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucAssetItemsView
            // 
            this.ucAssetItemsView.ChangeAddCaption = "&Add";
            this.ucAssetItemsView.ChangeCaption = "&Edit";
            this.ucAssetItemsView.ChangeDeleteCaption = "&Delete";
            this.ucAssetItemsView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucAssetItemsView.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucAssetItemsView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAssetItemsView.ChangePostInterestCaption = "P&ost Interest";
            this.ucAssetItemsView.ChangePrintCaption = "&Print";
            this.ucAssetItemsView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucAssetItemsView.DisableAddButton = true;
            this.ucAssetItemsView.DisableAMCRenew = true;
            this.ucAssetItemsView.DisableCloseButton = true;
            this.ucAssetItemsView.DisableDeleteButton = true;
            this.ucAssetItemsView.DisableDownloadExcel = true;
            this.ucAssetItemsView.DisableEditButton = true;
            this.ucAssetItemsView.DisableInsertVoucher = true;
            this.ucAssetItemsView.DisableMoveTransaction = true;
            this.ucAssetItemsView.DisableNatureofPayments = true;
            this.ucAssetItemsView.DisablePostInterest = true;
            this.ucAssetItemsView.DisablePrintButton = true;
            this.ucAssetItemsView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucAssetItemsView, "ucAssetItemsView");
            this.ucAssetItemsView.Name = "ucAssetItemsView";
            this.ucAssetItemsView.ShowHTML = true;
            this.ucAssetItemsView.ShowMMT = true;
            this.ucAssetItemsView.ShowPDF = true;
            this.ucAssetItemsView.ShowRTF = true;
            this.ucAssetItemsView.ShowText = true;
            this.ucAssetItemsView.ShowXLS = true;
            this.ucAssetItemsView.ShowXLSX = true;
            this.ucAssetItemsView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.AddClicked += new System.EventHandler(this.ucAssetItemsView_AddClicked);
            this.ucAssetItemsView.EditClicked += new System.EventHandler(this.ucAssetItemsView_EditClicked);
            this.ucAssetItemsView.DeleteClicked += new System.EventHandler(this.ucAssetItemsView_DeleteClicked);
            this.ucAssetItemsView.PrintClicked += new System.EventHandler(this.ucAssetItemsView_PrintClicked);
            this.ucAssetItemsView.CloseClicked += new System.EventHandler(this.ucAssetItemsView_CloseClicked);
            this.ucAssetItemsView.RefreshClicked += new System.EventHandler(this.ucAssetItemsView_RefreshClicked);
            this.ucAssetItemsView.DownloadExcel += new System.EventHandler(this.ucAssetItemsView_DownloadExcel);
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
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.lblCountNumber,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(975, 485);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucAssetItemsView;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(196, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(975, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcAssetItems;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(975, 430);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 461);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 461);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(853, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.AllowHotTrack = false;
            this.lblCountNumber.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCountNumber.AppearanceItemCaption.Font")));
            this.lblCountNumber.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountNumber.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lblCountNumber, "lblCountNumber");
            this.lblCountNumber.Location = new System.Drawing.Point(945, 461);
            this.lblCountNumber.MaxSize = new System.Drawing.Size(0, 17);
            this.lblCountNumber.MinSize = new System.Drawing.Size(11, 17);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(30, 24);
            this.lblCountNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountNumber.TextSize = new System.Drawing.Size(9, 13);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(932, 461);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(13, 24);
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmAssetItemView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetItemView";
            this.ShowFilterClicked += new System.EventHandler(this.frmAssetItemView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmAssetItemView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmAssetItemView_Activated);
            this.Load += new System.EventHandler(this.frmAssetItemView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItemDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar ucAssetItemsView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcAssetItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClass;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetItem;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountNumber;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetItemDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetID;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colRetentionYear;
        private DevExpress.XtraGrid.Columns.GridColumn colDepreciation;
        private DevExpress.XtraGrid.Columns.GridColumn colCustodian;
        private DevExpress.XtraGrid.Columns.GridColumn colBlock;
        private DevExpress.XtraGrid.Columns.GridColumn colParentClass;
        private DevExpress.XtraGrid.Columns.GridColumn colWidth;
    }
}