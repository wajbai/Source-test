namespace ACPP.Modules.Inventory.Stock
{
    partial class frmStockOpeningBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockOpeningBalance));
            this.lcOPBalance = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.gcStockOPBalance = new DevExpress.XtraGrid.GridControl();
            this.gvStockOPBalance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtRatePer = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.glkpLocation = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgOPBalance = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblLocation = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgGroupOPBalance = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcOPBalance)).BeginInit();
            this.lcOPBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockOPBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockOPBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRatePer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgOPBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroupOPBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // lcOPBalance
            // 
            this.lcOPBalance.Controls.Add(this.chkShowFilter);
            this.lcOPBalance.Controls.Add(this.btnSave);
            this.lcOPBalance.Controls.Add(this.btnClose);
            this.lcOPBalance.Controls.Add(this.gcStockOPBalance);
            this.lcOPBalance.Controls.Add(this.glkpLocation);
            this.lcOPBalance.Controls.Add(this.glkpProject);
            resources.ApplyResources(this.lcOPBalance, "lcOPBalance");
            this.lcOPBalance.Name = "lcOPBalance";
            this.lcOPBalance.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(639, 171, 250, 350);
            this.lcOPBalance.OptionsView.UseDefaultDragAndDropRendering = false;
            this.lcOPBalance.Root = this.lcgOPBalance;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.lcOPBalance;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcOPBalance;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcOPBalance;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gcStockOPBalance
            // 
            resources.ApplyResources(this.gcStockOPBalance, "gcStockOPBalance");
            this.gcStockOPBalance.MainView = this.gvStockOPBalance;
            this.gcStockOPBalance.Name = "gcStockOPBalance";
            this.gcStockOPBalance.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtRatePer,
            this.rtxtQuantity});
            this.gcStockOPBalance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStockOPBalance});
            this.gcStockOPBalance.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcStockOPBalance_ProcessGridKey);
            this.gcStockOPBalance.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcStockOPBalance_PreviewKeyDown);
            // 
            // gvStockOPBalance
            // 
            this.gvStockOPBalance.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvStockOPBalance.Appearance.FocusedRow.Font")));
            this.gvStockOPBalance.Appearance.FocusedRow.Options.UseFont = true;
            this.gvStockOPBalance.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvStockOPBalance.Appearance.HeaderPanel.Font")));
            this.gvStockOPBalance.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvStockOPBalance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemId,
            this.colItemName,
            this.colRate,
            this.colQuantity});
            this.gvStockOPBalance.GridControl = this.gcStockOPBalance;
            this.gvStockOPBalance.Name = "gvStockOPBalance";
            this.gvStockOPBalance.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvStockOPBalance.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvStockOPBalance.OptionsView.ShowGroupPanel = false;
            this.gvStockOPBalance.OptionsView.ShowIndicator = false;
            this.gvStockOPBalance.RowCountChanged += new System.EventHandler(this.gvStockOPBalance_RowCountChanged);
            // 
            // colItemId
            // 
            resources.ApplyResources(this.colItemId, "colItemId");
            this.colItemId.FieldName = "ITEM_ID";
            this.colItemId.Name = "colItemId";
            // 
            // colItemName
            // 
            resources.ApplyResources(this.colItemName, "colItemName");
            this.colItemName.FieldName = "NAME";
            this.colItemName.Name = "colItemName";
            this.colItemName.OptionsColumn.AllowEdit = false;
            this.colItemName.OptionsColumn.AllowFocus = false;
            this.colItemName.OptionsColumn.AllowMove = false;
            this.colItemName.OptionsColumn.AllowSize = false;
            // 
            // colRate
            // 
            resources.ApplyResources(this.colRate, "colRate");
            this.colRate.ColumnEdit = this.rtxtRatePer;
            this.colRate.FieldName = "RATE";
            this.colRate.MinWidth = 85;
            this.colRate.Name = "colRate";
            this.colRate.OptionsColumn.AllowMove = false;
            this.colRate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRate.OptionsColumn.FixedWidth = true;
            this.colRate.OptionsFilter.AllowAutoFilter = false;
            this.colRate.OptionsFilter.AllowFilter = false;
            this.colRate.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.colRate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colRate.Summary"))), resources.GetString("colRate.Summary1"), resources.GetString("colRate.Summary2"))});
            // 
            // rtxtRatePer
            // 
            resources.ApplyResources(this.rtxtRatePer, "rtxtRatePer");
            this.rtxtRatePer.Mask.EditMask = resources.GetString("rtxtRatePer.Mask.EditMask");
            this.rtxtRatePer.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtRatePer.Mask.MaskType")));
            this.rtxtRatePer.MaxLength = 13;
            this.rtxtRatePer.Name = "rtxtRatePer";
            // 
            // colQuantity
            // 
            resources.ApplyResources(this.colQuantity, "colQuantity");
            this.colQuantity.ColumnEdit = this.rtxtQuantity;
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.MinWidth = 70;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowMove = false;
            this.colQuantity.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colQuantity.OptionsColumn.FixedWidth = true;
            this.colQuantity.OptionsFilter.AllowAutoFilter = false;
            this.colQuantity.OptionsFilter.AllowFilter = false;
            this.colQuantity.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.colQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colQuantity.Summary"))), resources.GetString("colQuantity.Summary1"), resources.GetString("colQuantity.Summary2"))});
            // 
            // rtxtQuantity
            // 
            resources.ApplyResources(this.rtxtQuantity, "rtxtQuantity");
            this.rtxtQuantity.Mask.EditMask = resources.GetString("rtxtQuantity.Mask.EditMask");
            this.rtxtQuantity.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtQuantity.Mask.MaskType")));
            this.rtxtQuantity.MaxLength = 10;
            this.rtxtQuantity.Name = "rtxtQuantity";
            // 
            // glkpLocation
            // 
            this.glkpLocation.EnterMoveNextControl = true;
            resources.ApplyResources(this.glkpLocation, "glkpLocation");
            this.glkpLocation.Name = "glkpLocation";
            this.glkpLocation.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpLocation.Properties.Buttons"))))});
            this.glkpLocation.Properties.ImmediatePopup = true;
            this.glkpLocation.Properties.NullText = resources.GetString("glkpLocation.Properties.NullText");
            this.glkpLocation.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpLocation.Properties.PopupFormSize = new System.Drawing.Size(326, 0);
            this.glkpLocation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpLocation.Properties.View = this.gridView1;
            this.glkpLocation.StyleController = this.lcOPBalance;
            this.glkpLocation.EditValueChanged += new System.EventHandler(this.glkpLocation_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLocationId,
            this.colLocationName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colLocationId
            // 
            resources.ApplyResources(this.colLocationId, "colLocationId");
            this.colLocationId.FieldName = "LOCATION_ID";
            this.colLocationId.Name = "colLocationId";
            // 
            // colLocationName
            // 
            resources.ApplyResources(this.colLocationName, "colLocationName");
            this.colLocationName.FieldName = "LOCATION";
            this.colLocationName.Name = "colLocationName";
            // 
            // glkpProject
            // 
            this.glkpProject.EnterMoveNextControl = true;
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(326, 0);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.StyleController = this.lcOPBalance;
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProject});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            // 
            // lcgOPBalance
            // 
            resources.ApplyResources(this.lcgOPBalance, "lcgOPBalance");
            this.lcgOPBalance.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgOPBalance.GroupBordersVisible = false;
            this.lcgOPBalance.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject,
            this.emptySpaceItem1,
            this.lblLocation,
            this.lcgGroupOPBalance,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.lcgOPBalance.Location = new System.Drawing.Point(0, 0);
            this.lcgOPBalance.Name = "Root";
            this.lcgOPBalance.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgOPBalance.Size = new System.Drawing.Size(392, 355);
            this.lcgOPBalance.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AllowHtmlStringInCaption = true;
            this.lblProject.Control = this.glkpProject;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblProject.Size = new System.Drawing.Size(382, 23);
            this.lblProject.TextSize = new System.Drawing.Size(52, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 319);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(245, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblLocation
            // 
            this.lblLocation.AllowHtmlStringInCaption = true;
            this.lblLocation.Control = this.glkpLocation;
            resources.ApplyResources(this.lblLocation, "lblLocation");
            this.lblLocation.Location = new System.Drawing.Point(0, 23);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblLocation.Size = new System.Drawing.Size(382, 23);
            this.lblLocation.TextSize = new System.Drawing.Size(52, 13);
            // 
            // lcgGroupOPBalance
            // 
            this.lcgGroupOPBalance.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgGroupOPBalance.AppearanceGroup.Font")));
            this.lcgGroupOPBalance.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgGroupOPBalance, "lcgGroupOPBalance");
            this.lcgGroupOPBalance.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.lblRecordCount,
            this.simpleLabelItem1,
            this.emptySpaceItem2});
            this.lcgGroupOPBalance.Location = new System.Drawing.Point(0, 46);
            this.lcgGroupOPBalance.Name = "lcgGroupOPBalance";
            this.lcgGroupOPBalance.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcgGroupOPBalance.Size = new System.Drawing.Size(382, 273);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcStockOPBalance;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(370, 218);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 218);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(79, 23);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(324, 218);
            this.lblRecordCount.MaxSize = new System.Drawing.Size(46, 23);
            this.lblRecordCount.MinSize = new System.Drawing.Size(46, 23);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(46, 23);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(3, 13);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(312, 218);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(12, 23);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(12, 23);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(12, 23);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(79, 218);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(233, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(314, 319);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(68, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(68, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(68, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(245, 319);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmStockOpeningBalance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcOPBalance);
            this.Name = "frmStockOpeningBalance";
            this.Load += new System.EventHandler(this.frmStockOpeningBalance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcOPBalance)).EndInit();
            this.lcOPBalance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockOPBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockOPBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRatePer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgOPBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroupOPBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcOPBalance;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gcStockOPBalance;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStockOPBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraEditors.GridLookUpEdit glkpLocation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlGroup lcgOPBalance;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblLocation;
        private DevExpress.XtraLayout.LayoutControlGroup lcgGroupOPBalance;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationId;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtRatePer;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtQuantity;
    }
}