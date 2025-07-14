namespace ACPP.Modules.Inventory.Stock
{
    partial class frmPurchaseReturnsView
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.gvPurchaseReturnsDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocaiton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVendor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVendorId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPurchaseReturns = new DevExpress.XtraGrid.GridControl();
            this.gvPurchaseReturns = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRetrunId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedger_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lyMain = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.dtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.dtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkFilter = new DevExpress.XtraEditors.CheckEdit();
            this.ucToolbar = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lyToolBar = new DevExpress.XtraLayout.LayoutControlItem();
            this.lyGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.lyFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lyProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lyDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lyDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowCountCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseReturnsDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPurchaseReturns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseReturns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyMain)).BeginInit();
            this.lyMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyToolBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCountCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // gvPurchaseReturnsDetails
            // 
            this.gvPurchaseReturnsDetails.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvPurchaseReturnsDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPurchaseReturnsDetails.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPurchaseReturnsDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPurchaseReturnsDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItem,
            this.colLocaiton,
            this.colQuantity,
            this.colUnitPrice,
            this.colVendor,
            this.colReturnId,
            this.colItemId,
            this.colVendorId,
            this.colLocationId});
            this.gvPurchaseReturnsDetails.GridControl = this.gcPurchaseReturns;
            this.gvPurchaseReturnsDetails.Name = "gvPurchaseReturnsDetails";
            this.gvPurchaseReturnsDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPurchaseReturnsDetails.OptionsView.ShowGroupPanel = false;
            // 
            // colItem
            // 
            this.colItem.Caption = "Stock";
            this.colItem.FieldName = "ITEM_NAME";
            this.colItem.Name = "colItem";
            this.colItem.OptionsColumn.AllowEdit = false;
            this.colItem.Visible = true;
            this.colItem.VisibleIndex = 0;
            // 
            // colLocaiton
            // 
            this.colLocaiton.Caption = "Location";
            this.colLocaiton.FieldName = "LOCATION";
            this.colLocaiton.Name = "colLocaiton";
            this.colLocaiton.OptionsColumn.AllowEdit = false;
            this.colLocaiton.Visible = true;
            this.colLocaiton.VisibleIndex = 1;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 2;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnitPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colUnitPrice.Caption = "Rate per";
            this.colUnitPrice.FieldName = "UNIT_PRICE";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.OptionsColumn.AllowEdit = false;
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 3;
            // 
            // colVendor
            // 
            this.colVendor.Caption = "Vendor";
            this.colVendor.FieldName = "VENDOR";
            this.colVendor.Name = "colVendor";
            this.colVendor.OptionsColumn.AllowEdit = false;
            this.colVendor.Visible = true;
            this.colVendor.VisibleIndex = 4;
            // 
            // colReturnId
            // 
            this.colReturnId.Caption = "RetrunId";
            this.colReturnId.FieldName = "RETURN_ID";
            this.colReturnId.Name = "colReturnId";
            this.colReturnId.OptionsColumn.AllowEdit = false;
            // 
            // colItemId
            // 
            this.colItemId.Caption = "Item Id";
            this.colItemId.FieldName = "ITEM_ID";
            this.colItemId.Name = "colItemId";
            this.colItemId.OptionsColumn.AllowEdit = false;
            // 
            // colVendorId
            // 
            this.colVendorId.Caption = "Vendor Id";
            this.colVendorId.FieldName = "VENDOR_ID";
            this.colVendorId.Name = "colVendorId";
            this.colVendorId.OptionsColumn.AllowEdit = false;
            // 
            // colLocationId
            // 
            this.colLocationId.Caption = "Location Id";
            this.colLocationId.FieldName = "LOCATION_ID";
            this.colLocationId.Name = "colLocationId";
            this.colLocationId.OptionsColumn.AllowEdit = false;
            // 
            // gcPurchaseReturns
            // 
            gridLevelNode1.LevelTemplate = this.gvPurchaseReturnsDetails;
            gridLevelNode1.RelationName = "Detail";
            this.gcPurchaseReturns.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcPurchaseReturns.Location = new System.Drawing.Point(2, 61);
            this.gcPurchaseReturns.MainView = this.gvPurchaseReturns;
            this.gcPurchaseReturns.Name = "gcPurchaseReturns";
            this.gcPurchaseReturns.Size = new System.Drawing.Size(706, 396);
            this.gcPurchaseReturns.TabIndex = 5;
            this.gcPurchaseReturns.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPurchaseReturns,
            this.gvPurchaseReturnsDetails});
            // 
            // gvPurchaseReturns
            // 
            this.gvPurchaseReturns.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvPurchaseReturns.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPurchaseReturns.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPurchaseReturns.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPurchaseReturns.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDate,
            this.colLedger,
            this.colReturnType,
            this.colAmount,
            this.colRetrunId,
            this.colLedger_Id,
            this.colReason});
            this.gvPurchaseReturns.GridControl = this.gcPurchaseReturns;
            this.gvPurchaseReturns.Name = "gvPurchaseReturns";
            this.gvPurchaseReturns.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPurchaseReturns.OptionsView.ShowGroupPanel = false;
            this.gvPurchaseReturns.DoubleClick += new System.EventHandler(this.gvPurchaseReturns_DoubleClick);
            this.gvPurchaseReturns.RowCountChanged += new System.EventHandler(this.gvPurchaseReturns_RowCountChanged);
            // 
            // colDate
            // 
            this.colDate.Caption = "Date";
            this.colDate.FieldName = "RETURN_DATE";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 84;
            // 
            // colLedger
            // 
            this.colLedger.Caption = "Ledger";
            this.colLedger.FieldName = "LEDGER_NAME";
            this.colLedger.Name = "colLedger";
            this.colLedger.OptionsColumn.AllowEdit = false;
            this.colLedger.Visible = true;
            this.colLedger.VisibleIndex = 1;
            this.colLedger.Width = 558;
            // 
            // colReturnType
            // 
            this.colReturnType.Caption = "Return Type";
            this.colReturnType.FieldName = "RETURN_TYPE";
            this.colReturnType.Name = "colReturnType";
            this.colReturnType.OptionsColumn.AllowEdit = false;
            this.colReturnType.Visible = true;
            this.colReturnType.VisibleIndex = 3;
            this.colReturnType.Width = 134;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAmount.Caption = "Net Amount";
            this.colAmount.FieldName = "NET_PAY";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 2;
            this.colAmount.Width = 157;
            // 
            // colRetrunId
            // 
            this.colRetrunId.Caption = "ReturnId";
            this.colRetrunId.FieldName = "RETURN_ID";
            this.colRetrunId.Name = "colRetrunId";
            this.colRetrunId.OptionsColumn.AllowEdit = false;
            // 
            // colLedger_Id
            // 
            this.colLedger_Id.Caption = "Ledger Id";
            this.colLedger_Id.FieldName = "LEDGER_ID";
            this.colLedger_Id.Name = "colLedger_Id";
            this.colLedger_Id.OptionsColumn.AllowEdit = false;
            // 
            // colReason
            // 
            this.colReason.Caption = "Reason";
            this.colReason.FieldName = "REASON";
            this.colReason.Name = "colReason";
            // 
            // lyMain
            // 
            this.lyMain.Controls.Add(this.btnApply);
            this.lyMain.Controls.Add(this.dtDateTo);
            this.lyMain.Controls.Add(this.dtDateFrom);
            this.lyMain.Controls.Add(this.glkpProject);
            this.lyMain.Controls.Add(this.chkFilter);
            this.lyMain.Controls.Add(this.gcPurchaseReturns);
            this.lyMain.Controls.Add(this.ucToolbar);
            this.lyMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lyMain.Location = new System.Drawing.Point(0, 0);
            this.lyMain.Name = "lyMain";
            this.lyMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(199, 266, 250, 350);
            this.lyMain.Root = this.layoutControlGroup1;
            this.lyMain.Size = new System.Drawing.Size(710, 482);
            this.lyMain.TabIndex = 0;
            this.lyMain.Text = "layoutControl1";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(454, 35);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(59, 22);
            this.btnApply.StyleController = this.lyMain;
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dtDateTo
            // 
            this.dtDateTo.EditValue = null;
            this.dtDateTo.EnterMoveNextControl = true;
            this.dtDateTo.Location = new System.Drawing.Point(368, 35);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtDateTo.Size = new System.Drawing.Size(82, 20);
            this.dtDateTo.StyleController = this.lyMain;
            this.dtDateTo.TabIndex = 9;
            // 
            // dtDateFrom
            // 
            this.dtDateFrom.EditValue = null;
            this.dtDateFrom.EnterMoveNextControl = true;
            this.dtDateFrom.Location = new System.Drawing.Point(253, 35);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtDateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtDateFrom.Size = new System.Drawing.Size(92, 20);
            this.dtDateFrom.StyleController = this.lyMain;
            this.dtDateFrom.TabIndex = 8;
            // 
            // glkpProject
            // 
            this.glkpProject.EnterMoveNextControl = true;
            this.glkpProject.Location = new System.Drawing.Point(46, 35);
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = "";
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(414, 0);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.Size = new System.Drawing.Size(169, 20);
            this.glkpProject.StyleController = this.lyMain;
            this.glkpProject.TabIndex = 7;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
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
            this.colProjectId.Caption = "Project Id";
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            this.colProject.Caption = "Project";
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 0;
            // 
            // chkFilter
            // 
            this.chkFilter.Location = new System.Drawing.Point(2, 461);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkFilter.Size = new System.Drawing.Size(75, 19);
            this.chkFilter.StyleController = this.lyMain;
            this.chkFilter.TabIndex = 6;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // ucToolbar
            // 
            this.ucToolbar.ChangeAddCaption = "&Add";
            this.ucToolbar.ChangeCaption = "&Edit";
            this.ucToolbar.ChangeDeleteCaption = "&Delete";
            this.ucToolbar.ChangeMoveVoucherCaption = "&Move Voucher";
            toolTipTitleItem1.Text = "Move Transaction (Alt + T)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "         Click on this to Move Transaction  Record.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolbar.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolbar.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolbar.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolbar.ChangePrintCaption = "&Print";
            this.ucToolbar.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolbar.DisableAddButton = true;
            this.ucToolbar.DisableAMCRenew = true;
            this.ucToolbar.DisableCloseButton = true;
            this.ucToolbar.DisableDeleteButton = true;
            this.ucToolbar.DisableDownloadExcel = true;
            this.ucToolbar.DisableEditButton = true;
            this.ucToolbar.DisableMoveTransaction = true;
            this.ucToolbar.DisableNatureofPayments = true;
            this.ucToolbar.DisablePostInterest = true;
            this.ucToolbar.DisablePrintButton = true;
            this.ucToolbar.DisableRestoreVoucher = true;
            this.ucToolbar.Location = new System.Drawing.Point(2, 2);
            this.ucToolbar.Name = "ucToolbar";
            this.ucToolbar.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucToolbar.ShowHTML = true;
            this.ucToolbar.ShowMMT = true;
            this.ucToolbar.ShowPDF = true;
            this.ucToolbar.ShowRTF = true;
            this.ucToolbar.ShowText = true;
            this.ucToolbar.ShowXLS = true;
            this.ucToolbar.ShowXLSX = true;
            this.ucToolbar.Size = new System.Drawing.Size(706, 29);
            this.ucToolbar.TabIndex = 4;
            this.ucToolbar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolbar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolbar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbar.AddClicked += new System.EventHandler(this.ucToolbar_AddClicked);
            this.ucToolbar.EditClicked += new System.EventHandler(this.ucToolbar_EditClicked);
            this.ucToolbar.DeleteClicked += new System.EventHandler(this.ucToolbar_DeleteClicked);
            this.ucToolbar.PrintClicked += new System.EventHandler(this.ucToolbar_PrintClicked);
            this.ucToolbar.CloseClicked += new System.EventHandler(this.ucToolbar_CloseClicked);
            this.ucToolbar.RefreshClicked += new System.EventHandler(this.ucToolbar_RefreshClicked);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lyToolBar,
            this.lyGrid,
            this.lyFilter,
            this.emptySpaceItem1,
            this.lblRowCount,
            this.lyProject,
            this.lyDateFrom,
            this.lyDateTo,
            this.layoutControlItem1,
            this.lblRowCountCaption,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(710, 482);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lyToolBar
            // 
            this.lyToolBar.Control = this.ucToolbar;
            this.lyToolBar.CustomizationFormText = "lyToolBar";
            this.lyToolBar.Location = new System.Drawing.Point(0, 0);
            this.lyToolBar.MaxSize = new System.Drawing.Size(0, 33);
            this.lyToolBar.MinSize = new System.Drawing.Size(104, 33);
            this.lyToolBar.Name = "lyToolBar";
            this.lyToolBar.Size = new System.Drawing.Size(710, 33);
            this.lyToolBar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lyToolBar.Text = "lyToolBar";
            this.lyToolBar.TextSize = new System.Drawing.Size(0, 0);
            this.lyToolBar.TextToControlDistance = 0;
            this.lyToolBar.TextVisible = false;
            // 
            // lyGrid
            // 
            this.lyGrid.Control = this.gcPurchaseReturns;
            this.lyGrid.CustomizationFormText = "lyGrid";
            this.lyGrid.Location = new System.Drawing.Point(0, 59);
            this.lyGrid.Name = "lyGrid";
            this.lyGrid.Size = new System.Drawing.Size(710, 400);
            this.lyGrid.Text = "lyGrid";
            this.lyGrid.TextSize = new System.Drawing.Size(0, 0);
            this.lyGrid.TextToControlDistance = 0;
            this.lyGrid.TextVisible = false;
            // 
            // lyFilter
            // 
            this.lyFilter.Control = this.chkFilter;
            this.lyFilter.CustomizationFormText = "lyFilter";
            this.lyFilter.Location = new System.Drawing.Point(0, 459);
            this.lyFilter.Name = "lyFilter";
            this.lyFilter.Size = new System.Drawing.Size(79, 23);
            this.lyFilter.Text = "lyFilter";
            this.lyFilter.TextSize = new System.Drawing.Size(0, 0);
            this.lyFilter.TextToControlDistance = 0;
            this.lyFilter.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 459);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(523, 23);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblRowCount
            // 
            this.lblRowCount.AllowHotTrack = false;
            this.lblRowCount.CustomizationFormText = "0";
            this.lblRowCount.Location = new System.Drawing.Point(647, 459);
            this.lblRowCount.MinSize = new System.Drawing.Size(63, 17);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(63, 23);
            this.lblRowCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRowCount.Text = "0";
            this.lblRowCount.TextSize = new System.Drawing.Size(41, 13);
            // 
            // lyProject
            // 
            this.lyProject.Control = this.glkpProject;
            this.lyProject.CustomizationFormText = "Project";
            this.lyProject.Location = new System.Drawing.Point(0, 33);
            this.lyProject.Name = "lyProject";
            this.lyProject.Size = new System.Drawing.Size(217, 26);
            this.lyProject.Text = "Project";
            this.lyProject.TextSize = new System.Drawing.Size(41, 13);
            // 
            // lyDateFrom
            // 
            this.lyDateFrom.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lyDateFrom.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lyDateFrom.Control = this.dtDateFrom;
            this.lyDateFrom.CustomizationFormText = "Date From";
            this.lyDateFrom.Location = new System.Drawing.Point(217, 33);
            this.lyDateFrom.MaxSize = new System.Drawing.Size(130, 26);
            this.lyDateFrom.MinSize = new System.Drawing.Size(130, 26);
            this.lyDateFrom.Name = "lyDateFrom";
            this.lyDateFrom.Size = new System.Drawing.Size(130, 26);
            this.lyDateFrom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lyDateFrom.Text = "From";
            this.lyDateFrom.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lyDateFrom.TextSize = new System.Drawing.Size(29, 13);
            this.lyDateFrom.TextToControlDistance = 5;
            // 
            // lyDateTo
            // 
            this.lyDateTo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lyDateTo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lyDateTo.Control = this.dtDateTo;
            this.lyDateTo.CustomizationFormText = "Date To";
            this.lyDateTo.Location = new System.Drawing.Point(347, 33);
            this.lyDateTo.MaxSize = new System.Drawing.Size(105, 26);
            this.lyDateTo.MinSize = new System.Drawing.Size(105, 26);
            this.lyDateTo.Name = "lyDateTo";
            this.lyDateTo.Size = new System.Drawing.Size(105, 26);
            this.lyDateTo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lyDateTo.Text = "To";
            this.lyDateTo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lyDateTo.TextSize = new System.Drawing.Size(14, 13);
            this.lyDateTo.TextToControlDistance = 5;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnApply;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(452, 33);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblRowCountCaption
            // 
            this.lblRowCountCaption.AllowHotTrack = false;
            this.lblRowCountCaption.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblRowCountCaption.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblRowCountCaption.CustomizationFormText = "#";
            this.lblRowCountCaption.Location = new System.Drawing.Point(602, 459);
            this.lblRowCountCaption.Name = "lblRowCountCaption";
            this.lblRowCountCaption.Size = new System.Drawing.Size(45, 23);
            this.lblRowCountCaption.Text = "#";
            this.lblRowCountCaption.TextSize = new System.Drawing.Size(41, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(515, 33);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(195, 26);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(195, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(195, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmPurchaseReturnsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 482);
            this.Controls.Add(this.lyMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPurchaseReturnsView";
            this.Text = "Purchase Returns";
            this.Activated += new System.EventHandler(this.frmPurchaseReturnsView_Activated);
            this.Load += new System.EventHandler(this.frmPurchaseReturnsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseReturnsDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPurchaseReturns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchaseReturns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyMain)).EndInit();
            this.lyMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyToolBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCountCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lyMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar ucToolbar;
        private DevExpress.XtraLayout.LayoutControlItem lyToolBar;
        private DevExpress.XtraGrid.GridControl gcPurchaseReturns;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPurchaseReturns;
        private DevExpress.XtraLayout.LayoutControlItem lyGrid;
        private DevExpress.XtraEditors.CheckEdit chkFilter;
        private DevExpress.XtraLayout.LayoutControlItem lyFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lyProject;
        private DevExpress.XtraEditors.DateEdit dtDateTo;
        private DevExpress.XtraEditors.DateEdit dtDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lyDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lyDateTo;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPurchaseReturnsDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnType;
        private DevExpress.XtraGrid.Columns.GridColumn colItem;
        private DevExpress.XtraGrid.Columns.GridColumn colLocaiton;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colVendor;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCountCaption;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colVendorId;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationId;
        private DevExpress.XtraGrid.Columns.GridColumn colRetrunId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedger_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}