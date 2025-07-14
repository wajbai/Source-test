namespace ACPP.Modules.Inventory.Asset
{
    partial class frmAssetSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetSearch));
            this.gvAssetItemDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUsefulLife = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalvageLife = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSourceFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAsset = new DevExpress.XtraGrid.GridControl();
            this.gvAssetItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAssetItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAsseID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustodians = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpLocation = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.uctbLocationView = new Bosco.Utility.Controls.ucToolBar();
            this.trlLocation = new DevExpress.XtraTreeList.TreeList();
            this.colNAME = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trcolId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCountSymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItemDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAsset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trlLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).BeginInit();
            this.SuspendLayout();
            // 
            // gvAssetItemDetail
            // 
            this.gvAssetItemDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAssetItemDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetItemDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAssetItemDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetItemDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.colAmount,
            this.colUsefulLife,
            this.colSalvageLife,
            this.colSourceFlag});
            this.gvAssetItemDetail.GridControl = this.gcAsset;
            this.gvAssetItemDetail.Name = "gvAssetItemDetail";
            this.gvAssetItemDetail.OptionsBehavior.Editable = false;
            this.gvAssetItemDetail.OptionsBehavior.ReadOnly = true;
            this.gvAssetItemDetail.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetItemDetail.OptionsView.ShowAutoFilterRow = true;
            this.gvAssetItemDetail.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ItemId";
            this.gridColumn1.FieldName = "ITEM_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Asset ID";
            this.gridColumn2.FieldName = "ASSET_ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Location";
            this.gridColumn3.FieldName = "LOCATION_NAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Amount";
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 2;
            // 
            // colUsefulLife
            // 
            this.colUsefulLife.Caption = "Useful Life";
            this.colUsefulLife.FieldName = "USEFUL_LIFE";
            this.colUsefulLife.Name = "colUsefulLife";
            this.colUsefulLife.Visible = true;
            this.colUsefulLife.VisibleIndex = 3;
            // 
            // colSalvageLife
            // 
            this.colSalvageLife.Caption = "Salvage Life";
            this.colSalvageLife.FieldName = "SALVAGE_LIFE";
            this.colSalvageLife.Name = "colSalvageLife";
            this.colSalvageLife.Visible = true;
            this.colSalvageLife.VisibleIndex = 4;
            // 
            // colSourceFlag
            // 
            this.colSourceFlag.Caption = "Source";
            this.colSourceFlag.FieldName = "SOURCE_FLAG";
            this.colSourceFlag.Name = "colSourceFlag";
            this.colSourceFlag.Visible = true;
            this.colSourceFlag.VisibleIndex = 5;
            // 
            // gcAsset
            // 
            this.gcAsset.Location = new System.Drawing.Point(2, 30);
            this.gcAsset.MainView = this.gvAssetItems;
            this.gcAsset.Name = "gcAsset";
            this.gcAsset.Size = new System.Drawing.Size(894, 333);
            this.gcAsset.TabIndex = 8;
            this.gcAsset.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetItems,
            this.gvAssetItemDetail});
            // 
            // gvAssetItems
            // 
            this.gvAssetItems.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gvAssetItems.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAssetItems.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvAssetItems.Appearance.FilterPanel.Options.UseFont = true;
            this.gvAssetItems.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gvAssetItems.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetItems.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAssetItems.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAssetItemId,
            this.colProject,
            this.colItemName,
            this.colAsseID,
            this.colAssetGroup,
            this.colCategory,
            this.colLocationName,
            this.colCustodians,
            this.colLocationID,
            this.colSource,
            this.colStatus});
            this.gvAssetItems.GridControl = this.gcAsset;
            this.gvAssetItems.Name = "gvAssetItems";
            this.gvAssetItems.OptionsBehavior.Editable = false;
            this.gvAssetItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetItems.OptionsView.ShowAutoFilterRow = true;
            this.gvAssetItems.OptionsView.ShowGroupPanel = false;
            this.gvAssetItems.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAssetItems_FocusedRowChanged);
            this.gvAssetItems.RowCountChanged += new System.EventHandler(this.gvAssetItems_RowCountChanged);
            // 
            // colAssetItemId
            // 
            this.colAssetItemId.Caption = "ItemID";
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
            this.colProject.OptionsColumn.ReadOnly = true;
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 0;
            this.colProject.Width = 155;
            // 
            // colItemName
            // 
            this.colItemName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colItemName.AppearanceHeader.Options.UseFont = true;
            this.colItemName.Caption = "Asset";
            this.colItemName.FieldName = "ASSET_NAME";
            this.colItemName.Name = "colItemName";
            this.colItemName.OptionsColumn.AllowEdit = false;
            this.colItemName.OptionsColumn.AllowFocus = false;
            this.colItemName.OptionsColumn.ReadOnly = true;
            this.colItemName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colItemName.Visible = true;
            this.colItemName.VisibleIndex = 1;
            this.colItemName.Width = 137;
            // 
            // colAsseID
            // 
            this.colAsseID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAsseID.AppearanceHeader.Options.UseFont = true;
            this.colAsseID.Caption = "Asset ID";
            this.colAsseID.FieldName = "ASSET_ID";
            this.colAsseID.Name = "colAsseID";
            this.colAsseID.OptionsColumn.AllowEdit = false;
            this.colAsseID.OptionsColumn.AllowFocus = false;
            this.colAsseID.OptionsColumn.ReadOnly = true;
            this.colAsseID.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAsseID.Visible = true;
            this.colAsseID.VisibleIndex = 2;
            this.colAsseID.Width = 89;
            // 
            // colAssetGroup
            // 
            this.colAssetGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAssetGroup.AppearanceHeader.Options.UseFont = true;
            this.colAssetGroup.Caption = "Group";
            this.colAssetGroup.FieldName = "GROUP_NAME";
            this.colAssetGroup.Name = "colAssetGroup";
            this.colAssetGroup.OptionsColumn.AllowEdit = false;
            this.colAssetGroup.OptionsColumn.AllowFocus = false;
            this.colAssetGroup.OptionsColumn.ReadOnly = true;
            this.colAssetGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAssetGroup.Visible = true;
            this.colAssetGroup.VisibleIndex = 3;
            this.colAssetGroup.Width = 128;
            // 
            // colCategory
            // 
            this.colCategory.Caption = "Category";
            this.colCategory.FieldName = "CATEGORY_NAME";
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.AllowEdit = false;
            this.colCategory.OptionsColumn.AllowFocus = false;
            this.colCategory.OptionsColumn.ReadOnly = true;
            this.colCategory.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colCategory.Width = 140;
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
            this.colLocationName.OptionsColumn.ReadOnly = true;
            this.colLocationName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colLocationName.Visible = true;
            this.colLocationName.VisibleIndex = 4;
            this.colLocationName.Width = 133;
            // 
            // colCustodians
            // 
            this.colCustodians.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCustodians.AppearanceHeader.Options.UseFont = true;
            this.colCustodians.Caption = "Custodian";
            this.colCustodians.FieldName = "NAME";
            this.colCustodians.Name = "colCustodians";
            this.colCustodians.OptionsColumn.AllowEdit = false;
            this.colCustodians.OptionsColumn.AllowFocus = false;
            this.colCustodians.Visible = true;
            this.colCustodians.VisibleIndex = 5;
            this.colCustodians.Width = 80;
            // 
            // colLocationID
            // 
            this.colLocationID.Caption = "LocationID";
            this.colLocationID.FieldName = "LOCATION_ID";
            this.colLocationID.Name = "colLocationID";
            // 
            // colSource
            // 
            this.colSource.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSource.AppearanceHeader.Options.UseFont = true;
            this.colSource.Caption = "Source";
            this.colSource.FieldName = "SOURCE_FLAG";
            this.colSource.Name = "colSource";
            this.colSource.OptionsColumn.AllowEdit = false;
            this.colSource.OptionsColumn.AllowFocus = false;
            this.colSource.Width = 111;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowFocus = false;
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 6;
            this.colStatus.Width = 123;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcAsset);
            this.layoutControl1.Controls.Add(this.glkpLocation);
            this.layoutControl1.Controls.Add(this.uctbLocationView);
            this.layoutControl1.Controls.Add(this.trlLocation);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(174, 60, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1192, 388);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // glkpLocation
            // 
            this.glkpLocation.Location = new System.Drawing.Point(943, 30);
            this.glkpLocation.Name = "glkpLocation";
            this.glkpLocation.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpLocation.Properties.NullText = "";
            this.glkpLocation.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpLocation.Properties.PopupFormSize = new System.Drawing.Size(331, 0);
            this.glkpLocation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpLocation.Properties.View = this.gridLookUpEdit2View;
            this.glkpLocation.Size = new System.Drawing.Size(247, 20);
            this.glkpLocation.StyleController = this.layoutControl1;
            this.glkpLocation.TabIndex = 6;
            this.glkpLocation.EditValueChanged += new System.EventHandler(this.glkpLocation_EditValueChanged);
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colLocation});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "LOCATION_ID";
            this.colId.Name = "colId";
            // 
            // colLocation
            // 
            this.colLocation.Caption = "Location";
            this.colLocation.FieldName = "LOCATION_NAME";
            this.colLocation.Name = "colLocation";
            this.colLocation.Visible = true;
            this.colLocation.VisibleIndex = 0;
            // 
            // uctbLocationView
            // 
            this.uctbLocationView.ChangeAddCaption = "&Add";
            this.uctbLocationView.ChangeCaption = "&Edit";
            this.uctbLocationView.ChangeDeleteCaption = "&Delete";
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
            this.uctbLocationView.DisableMoveTransaction = true;
            this.uctbLocationView.DisableNatureofPayments = true;
            this.uctbLocationView.DisablePostInterest = true;
            this.uctbLocationView.DisablePrintButton = true;
            this.uctbLocationView.DisableRestoreVoucher = true;
            this.uctbLocationView.Location = new System.Drawing.Point(0, 0);
            this.uctbLocationView.Name = "uctbLocationView";
            this.uctbLocationView.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.uctbLocationView.ShowHTML = true;
            this.uctbLocationView.ShowMMT = true;
            this.uctbLocationView.ShowPDF = true;
            this.uctbLocationView.ShowRTF = true;
            this.uctbLocationView.ShowText = true;
            this.uctbLocationView.ShowXLS = true;
            this.uctbLocationView.ShowXLSX = true;
            this.uctbLocationView.Size = new System.Drawing.Size(1192, 28);
            this.uctbLocationView.TabIndex = 0;
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
            this.uctbLocationView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.uctbLocationView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.uctbLocationView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.uctbLocationView.PrintClicked += new System.EventHandler(this.uctbLocationView_PrintClicked);
            this.uctbLocationView.CloseClicked += new System.EventHandler(this.uctbLocationView_CloseClicked);
            this.uctbLocationView.RefreshClicked += new System.EventHandler(this.uctbLocationView_RefreshClicked);
            // 
            // trlLocation
            // 
            this.trlLocation.Appearance.FocusedCell.BorderColor = System.Drawing.Color.Black;
            this.trlLocation.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.trlLocation.Appearance.FocusedCell.Options.UseBorderColor = true;
            this.trlLocation.Appearance.FocusedCell.Options.UseFont = true;
            this.trlLocation.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.trlLocation.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.trlLocation.Appearance.FocusedRow.Options.UseFont = true;
            this.trlLocation.Appearance.FocusedRow.Options.UseForeColor = true;
            this.trlLocation.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlLocation.Appearance.HeaderPanel.Options.UseFont = true;
            this.trlLocation.Appearance.SelectedRow.BackColor = System.Drawing.Color.LightGray;
            this.trlLocation.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
            this.trlLocation.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Transparent;
            this.trlLocation.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlLocation.Appearance.SelectedRow.Options.UseBackColor = true;
            this.trlLocation.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.trlLocation.Appearance.SelectedRow.Options.UseFont = true;
            this.trlLocation.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colNAME,
            this.colParentId,
            this.trcolId});
            this.trlLocation.DragNodesMode = DevExpress.XtraTreeList.TreeListDragNodesMode.Advanced;
            this.trlLocation.ImageIndexFieldName = "IMAGE_ID";
            this.trlLocation.KeyFieldName = "LOCATION_ID";
            this.trlLocation.Location = new System.Drawing.Point(900, 54);
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
            this.trlLocation.Size = new System.Drawing.Size(290, 309);
            this.trlLocation.TabIndex = 0;
            this.trlLocation.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.trlLocation_FocusedNodeChanged);
            // 
            // colNAME
            // 
            this.colNAME.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.5F, System.Drawing.FontStyle.Bold);
            this.colNAME.AppearanceHeader.Options.UseFont = true;
            this.colNAME.Caption = "Location";
            this.colNAME.FieldName = "LOCATION_NAME";
            this.colNAME.MinWidth = 33;
            this.colNAME.Name = "colNAME";
            this.colNAME.OptionsColumn.AllowEdit = false;
            this.colNAME.OptionsColumn.AllowFocus = false;
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 0;
            // 
            // colParentId
            // 
            this.colParentId.Caption = "ParentID";
            this.colParentId.FieldName = "PARENT_ID";
            this.colParentId.Name = "colParentId";
            this.colParentId.OptionsColumn.AllowEdit = false;
            this.colParentId.OptionsColumn.AllowFocus = false;
            // 
            // trcolId
            // 
            this.trcolId.Caption = "ID";
            this.trcolId.FieldName = "LOCATION_ID";
            this.trcolId.Name = "trcolId";
            this.trcolId.OptionsColumn.AllowEdit = false;
            this.trcolId.OptionsColumn.AllowFocus = false;
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.lblCount,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.emptySpaceItem3,
            this.layoutControlItem3,
            this.lblCountSymbol});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1192, 388);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.uctbLocationView;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 28);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(196, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(1192, 28);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 365);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(729, 23);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCount.CustomizationFormText = "0";
            this.lblCount.Location = new System.Drawing.Point(745, 365);
            this.lblCount.MaxSize = new System.Drawing.Size(70, 23);
            this.lblCount.MinSize = new System.Drawing.Size(70, 23);
            this.lblCount.Name = "lblCount";
            this.lblCount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCount.Size = new System.Drawing.Size(70, 23);
            this.lblCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCount.Text = "0";
            this.lblCount.TextSize = new System.Drawing.Size(40, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.trlLocation;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(898, 52);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(294, 313);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAsset;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(898, 337);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(815, 365);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(377, 23);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(377, 23);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(377, 23);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.glkpLocation;
            this.layoutControlItem3.CustomizationFormText = "Location";
            this.layoutControlItem3.Location = new System.Drawing.Point(898, 28);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(294, 24);
            this.layoutControlItem3.Text = "Location";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(40, 13);
            this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lblCountSymbol
            // 
            this.lblCountSymbol.AllowHotTrack = false;
            this.lblCountSymbol.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCountSymbol.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountSymbol.CustomizationFormText = "#";
            this.lblCountSymbol.Location = new System.Drawing.Point(729, 365);
            this.lblCountSymbol.Name = "lblCountSymbol";
            this.lblCountSymbol.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCountSymbol.Size = new System.Drawing.Size(16, 23);
            this.lblCountSymbol.Text = "#";
            this.lblCountSymbol.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCountSymbol.TextSize = new System.Drawing.Size(10, 14);
            // 
            // frmAssetSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 388);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetSearch";
            this.Text = "Search Asset";
            this.Load += new System.EventHandler(this.frmLocationsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItemDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAsset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trlLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTreeList.TreeList trlLocation;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar uctbLocationView;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNAME;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountSymbol;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trcolId;
        private DevExpress.Utils.ImageCollection imageSmall;
        private DevExpress.XtraEditors.GridLookUpEdit glkpLocation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcAsset;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetItemDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colUsefulLife;
        private DevExpress.XtraGrid.Columns.GridColumn colSalvageLife;
        private DevExpress.XtraGrid.Columns.GridColumn colSourceFlag;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colAsseID;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colSource;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colCustodians;
    }
}