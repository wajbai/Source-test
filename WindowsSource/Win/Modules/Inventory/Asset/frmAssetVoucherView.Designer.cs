namespace ACPP.Modules.Inventory.Asset
{
    partial class frmAssetVoucherView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetVoucherView));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gvAssetVoucherDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.collInOutID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colParentClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAssetVoucherView = new DevExpress.XtraGrid.GridControl();
            this.gvAssetVoucherView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colInOutID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVendor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.deDateTo = new DevExpress.XtraEditors.DateEdit();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.deDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.ucProjectName = new ACPP.Modules.UIControls.UcCaptionPanel();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFromdate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblToDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblNo = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetVoucherDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetVoucherView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetVoucherView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // gvAssetVoucherDetails
            // 
            this.gvAssetVoucherDetails.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetVoucherDetails.Appearance.FocusedRow.Font")));
            this.gvAssetVoucherDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetVoucherDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.collInOutID,
            this.colParentClass,
            this.colAssetClass,
            this.colAssetItem,
            this.colLocation,
            this.collQuantity,
            this.collAmount});
            this.gvAssetVoucherDetails.GridControl = this.gcAssetVoucherView;
            this.gvAssetVoucherDetails.Name = "gvAssetVoucherDetails";
            this.gvAssetVoucherDetails.OptionsView.ShowGroupPanel = false;
            // 
            // collInOutID
            // 
            resources.ApplyResources(this.collInOutID, "collInOutID");
            this.collInOutID.FieldName = "IN_OUT_ID";
            this.collInOutID.Name = "collInOutID";
            // 
            // colParentClass
            // 
            this.colParentClass.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colParentClass.AppearanceHeader.Font")));
            this.colParentClass.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colParentClass, "colParentClass");
            this.colParentClass.FieldName = "PARENT_CLASS";
            this.colParentClass.Name = "colParentClass";
            this.colParentClass.OptionsColumn.AllowEdit = false;
            this.colParentClass.OptionsColumn.AllowFocus = false;
            // 
            // colAssetClass
            // 
            this.colAssetClass.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAssetClass.AppearanceHeader.Font")));
            this.colAssetClass.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAssetClass, "colAssetClass");
            this.colAssetClass.FieldName = "ASSET_CLASS";
            this.colAssetClass.Name = "colAssetClass";
            this.colAssetClass.OptionsColumn.AllowEdit = false;
            this.colAssetClass.OptionsColumn.AllowFocus = false;
            // 
            // colAssetItem
            // 
            this.colAssetItem.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAssetItem.AppearanceHeader.Font")));
            this.colAssetItem.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAssetItem, "colAssetItem");
            this.colAssetItem.FieldName = "ASSET_ITEM";
            this.colAssetItem.Name = "colAssetItem";
            this.colAssetItem.OptionsColumn.AllowEdit = false;
            this.colAssetItem.OptionsColumn.AllowFocus = false;
            // 
            // colLocation
            // 
            this.colLocation.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLocation.AppearanceHeader.Font")));
            this.colLocation.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLocation, "colLocation");
            this.colLocation.FieldName = "LOCATION";
            this.colLocation.Name = "colLocation";
            this.colLocation.OptionsColumn.AllowEdit = false;
            this.colLocation.OptionsColumn.AllowFocus = false;
            // 
            // collQuantity
            // 
            this.collQuantity.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("collQuantity.AppearanceHeader.Font")));
            this.collQuantity.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.collQuantity, "collQuantity");
            this.collQuantity.FieldName = "QUANTITY";
            this.collQuantity.Name = "collQuantity";
            this.collQuantity.OptionsColumn.AllowEdit = false;
            this.collQuantity.OptionsColumn.AllowFocus = false;
            // 
            // collAmount
            // 
            this.collAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("collAmount.AppearanceHeader.Font")));
            this.collAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.collAmount, "collAmount");
            this.collAmount.DisplayFormat.FormatString = "n";
            this.collAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.collAmount.FieldName = "AMOUNT";
            this.collAmount.Name = "collAmount";
            this.collAmount.OptionsColumn.AllowEdit = false;
            this.collAmount.OptionsColumn.AllowFocus = false;
            // 
            // gcAssetVoucherView
            // 
            gridLevelNode1.LevelTemplate = this.gvAssetVoucherDetails;
            gridLevelNode1.RelationName = "Asset Items";
            this.gcAssetVoucherView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            resources.ApplyResources(this.gcAssetVoucherView, "gcAssetVoucherView");
            this.gcAssetVoucherView.MainView = this.gvAssetVoucherView;
            this.gcAssetVoucherView.Name = "gcAssetVoucherView";
            this.gcAssetVoucherView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetVoucherView,
            this.gvAssetVoucherDetails});
            this.gcAssetVoucherView.DoubleClick += new System.EventHandler(this.gcAssetVoucherView_DoubleClick);
            // 
            // gvAssetVoucherView
            // 
            this.gvAssetVoucherView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetVoucherView.Appearance.FocusedRow.Font")));
            this.gvAssetVoucherView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetVoucherView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInOutID,
            this.colDate,
            this.colBillInvoiceNo,
            this.colVoucherID,
            this.colVendor,
            this.colItem,
            this.colQuantity,
            this.colAmount});
            this.gvAssetVoucherView.GridControl = this.gcAssetVoucherView;
            this.gvAssetVoucherView.Name = "gvAssetVoucherView";
            this.gvAssetVoucherView.OptionsView.ShowGroupPanel = false;
            this.gvAssetVoucherView.RowCountChanged += new System.EventHandler(this.gvAssetVoucherView_RowCountChanged);
            // 
            // colInOutID
            // 
            resources.ApplyResources(this.colInOutID, "colInOutID");
            this.colInOutID.FieldName = "IN_OUT_ID";
            this.colInOutID.Name = "colInOutID";
            // 
            // colDate
            // 
            this.colDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDate.AppearanceHeader.Font")));
            this.colDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDate, "colDate");
            this.colDate.FieldName = "IN_OUT_DATE";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.OptionsColumn.AllowFocus = false;
            // 
            // colBillInvoiceNo
            // 
            this.colBillInvoiceNo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBillInvoiceNo.AppearanceHeader.Font")));
            this.colBillInvoiceNo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBillInvoiceNo, "colBillInvoiceNo");
            this.colBillInvoiceNo.FieldName = "BILL_INVOICE_NO";
            this.colBillInvoiceNo.Name = "colBillInvoiceNo";
            this.colBillInvoiceNo.OptionsColumn.AllowEdit = false;
            this.colBillInvoiceNo.OptionsColumn.AllowFocus = false;
            // 
            // colVoucherID
            // 
            this.colVoucherID.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherID.AppearanceHeader.Font")));
            this.colVoucherID.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherID, "colVoucherID");
            this.colVoucherID.FieldName = "VOUCHER_ID";
            this.colVoucherID.Name = "colVoucherID";
            this.colVoucherID.OptionsColumn.AllowEdit = false;
            this.colVoucherID.OptionsColumn.AllowFocus = false;
            // 
            // colVendor
            // 
            this.colVendor.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVendor.AppearanceHeader.Font")));
            this.colVendor.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVendor, "colVendor");
            this.colVendor.FieldName = "VENDOR";
            this.colVendor.Name = "colVendor";
            this.colVendor.OptionsColumn.AllowEdit = false;
            this.colVendor.OptionsColumn.AllowFocus = false;
            // 
            // colItem
            // 
            this.colItem.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colItem.AppearanceHeader.Font")));
            this.colItem.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colItem, "colItem");
            this.colItem.FieldName = "ITEM";
            this.colItem.Name = "colItem";
            this.colItem.OptionsColumn.AllowEdit = false;
            this.colItem.OptionsColumn.AllowFocus = false;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colQuantity.AppearanceHeader.Font")));
            this.colQuantity.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colQuantity, "colQuantity");
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.OptionsColumn.AllowFocus = false;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAmount.AppearanceHeader.Font")));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.FieldName = "TOT_AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.AllowFocus = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcAssetVoucherView);
            this.layoutControl1.Controls.Add(this.deDateTo);
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.deDateFrom);
            this.layoutControl1.Controls.Add(this.ucProjectName);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(241, 105, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
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
            // deDateTo
            // 
            this.deDateTo.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            resources.ApplyResources(this.deDateTo, "deDateTo");
            this.deDateTo.Name = "deDateTo";
            this.deDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.Buttons"))))});
            this.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.CalendarTimeProperties.Buttons"))))});
            this.deDateTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateTo.Properties.Mask.MaskType")));
            this.deDateTo.StyleController = this.layoutControl1;
            this.deDateTo.UseWaitCursor = true;
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            // 
            // deDateFrom
            // 
            resources.ApplyResources(this.deDateFrom, "deDateFrom");
            this.deDateFrom.Name = "deDateFrom";
            this.deDateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateFrom.Properties.Buttons"))))});
            this.deDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateFrom.Properties.CalendarTimeProperties.Buttons"))))});
            this.deDateFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateFrom.Properties.Mask.MaskType")));
            this.deDateFrom.StyleController = this.layoutControl1;
            // 
            // ucProjectName
            // 
            this.ucProjectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.ucProjectName, "ucProjectName");
            this.ucProjectName.Name = "ucProjectName";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lblFromdate,
            this.layoutControlItem2,
            this.emptySpaceItem5,
            this.emptySpaceItem1,
            this.lblToDate,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.layoutControlItem6,
            this.lblCount,
            this.lblNo});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(965, 377);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucProjectName;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(654, 39);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblFromdate
            // 
            this.lblFromdate.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblFromdate.AppearanceItemCaption.Font")));
            this.lblFromdate.AppearanceItemCaption.Options.UseFont = true;
            this.lblFromdate.Control = this.deDateFrom;
            this.lblFromdate.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            resources.ApplyResources(this.lblFromdate, "lblFromdate");
            this.lblFromdate.Location = new System.Drawing.Point(666, 12);
            this.lblFromdate.MaxSize = new System.Drawing.Size(121, 27);
            this.lblFromdate.MinSize = new System.Drawing.Size(121, 27);
            this.lblFromdate.Name = "lblFromdate";
            this.lblFromdate.Size = new System.Drawing.Size(121, 27);
            this.lblFromdate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFromdate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblFromdate.TextSize = new System.Drawing.Size(29, 13);
            this.lblFromdate.TextToControlDistance = 5;
            this.lblFromdate.TrimClientAreaToControl = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnApply;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(893, 12);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(72, 27);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(72, 27);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(72, 27);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(666, 0);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(299, 12);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(299, 12);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(299, 12);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(654, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(12, 39);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(12, 39);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(12, 39);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblToDate.AppearanceItemCaption.Font")));
            this.lblToDate.AppearanceItemCaption.Options.UseFont = true;
            this.lblToDate.Control = this.deDateTo;
            resources.ApplyResources(this.lblToDate, "lblToDate");
            this.lblToDate.Location = new System.Drawing.Point(787, 12);
            this.lblToDate.MaxSize = new System.Drawing.Size(106, 27);
            this.lblToDate.MinSize = new System.Drawing.Size(106, 27);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(106, 27);
            this.lblToDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblToDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblToDate.TextSize = new System.Drawing.Size(14, 13);
            this.lblToDate.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcAssetVoucherView;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(965, 314);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(171, 353);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(751, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 353);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(171, 24);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AllowHtmlStringInCaption = true;
            this.lblCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.AppearanceItemCaption.Font")));
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Location = new System.Drawing.Point(936, 353);
            this.lblCount.MinSize = new System.Drawing.Size(11, 17);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(29, 24);
            this.lblCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // lblNo
            // 
            this.lblNo.AllowHotTrack = false;
            this.lblNo.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblNo.AppearanceItemCaption.Font")));
            this.lblNo.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblNo, "lblNo");
            this.lblNo.Location = new System.Drawing.Point(922, 353);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(14, 24);
            this.lblNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblNo.TextSize = new System.Drawing.Size(10, 20);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem3.AppearanceItemCaption.Font")));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.deDateFrom;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(664, 12);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(122, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(122, 26);
            this.layoutControlItem3.Name = "lblFromdate";
            this.layoutControlItem3.Size = new System.Drawing.Size(122, 36);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(29, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            this.layoutControlItem3.TrimClientAreaToControl = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font")));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.deDateFrom;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(664, 12);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(122, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(122, 26);
            this.layoutControlItem4.Name = "lblFromdate";
            this.layoutControlItem4.Size = new System.Drawing.Size(122, 36);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(29, 13);
            this.layoutControlItem4.TextToControlDistance = 5;
            this.layoutControlItem4.TrimClientAreaToControl = false;
            // 
            // frmAssetVoucherView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetVoucherView";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmAssetVoucherView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetVoucherDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetVoucherView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetVoucherView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UIControls.UcCaptionPanel ucProjectName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.DateEdit deDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblFromdate;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraEditors.DateEdit deDateTo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblToDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gcAssetVoucherView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetVoucherView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colInOutID;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherID;
        private DevExpress.XtraGrid.Columns.GridColumn colItem;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetVoucherDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colBillInvoiceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colVendor;
        private DevExpress.XtraGrid.Columns.GridColumn collInOutID;
        private DevExpress.XtraGrid.Columns.GridColumn colParentClass;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClass;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetItem;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn collQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn collAmount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblNo;
    }
}