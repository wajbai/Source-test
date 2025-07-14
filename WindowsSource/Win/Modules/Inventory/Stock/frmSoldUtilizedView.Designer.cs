namespace ACPP.Modules.Inventory.Stock
{
    partial class frmSoldUtilizedView
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
            this.gvItemDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colitemSalesId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colitmItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colitmLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colitmQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colitmUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colitmAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSoldUtilized = new DevExpress.XtraGrid.GridControl();
            this.gvSoldUtilized = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSalesId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsalesDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsalesRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOtherCharges = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNetpay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.deDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.ucSoldUtilized = new Bosco.Utility.Controls.ucToolBar();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deDateTo = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSoldUtilized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSoldUtilized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvItemDetails
            // 
            this.gvItemDetails.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvItemDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvItemDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colitemSalesId,
            this.colitmItemName,
            this.colitmLocationName,
            this.colitmQuantity,
            this.colitmUnitPrice,
            this.colitmAmount});
            this.gvItemDetails.GridControl = this.gcSoldUtilized;
            this.gvItemDetails.Name = "gvItemDetails";
            this.gvItemDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvItemDetails.OptionsView.ShowGroupPanel = false;
            // 
            // colitemSalesId
            // 
            this.colitemSalesId.Caption = "Sales Id";
            this.colitemSalesId.FieldName = "SALES_ID";
            this.colitemSalesId.Name = "colitemSalesId";
            // 
            // colitmItemName
            // 
            this.colitmItemName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colitmItemName.AppearanceHeader.Options.UseFont = true;
            this.colitmItemName.Caption = "Stock";
            this.colitmItemName.FieldName = "ITEM_NAME";
            this.colitmItemName.Name = "colitmItemName";
            this.colitmItemName.OptionsColumn.AllowEdit = false;
            this.colitmItemName.OptionsColumn.AllowFocus = false;
            this.colitmItemName.Visible = true;
            this.colitmItemName.VisibleIndex = 0;
            // 
            // colitmLocationName
            // 
            this.colitmLocationName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colitmLocationName.AppearanceHeader.Options.UseFont = true;
            this.colitmLocationName.Caption = "Location";
            this.colitmLocationName.FieldName = "LOCATION";
            this.colitmLocationName.Name = "colitmLocationName";
            this.colitmLocationName.OptionsColumn.AllowEdit = false;
            this.colitmLocationName.Visible = true;
            this.colitmLocationName.VisibleIndex = 1;
            // 
            // colitmQuantity
            // 
            this.colitmQuantity.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colitmQuantity.AppearanceHeader.Options.UseFont = true;
            this.colitmQuantity.Caption = "Quantity";
            this.colitmQuantity.DisplayFormat.FormatString = "n";
            this.colitmQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colitmQuantity.FieldName = "QUANTITY";
            this.colitmQuantity.Name = "colitmQuantity";
            this.colitmQuantity.OptionsColumn.AllowEdit = false;
            this.colitmQuantity.OptionsColumn.AllowFocus = false;
            this.colitmQuantity.Visible = true;
            this.colitmQuantity.VisibleIndex = 2;
            // 
            // colitmUnitPrice
            // 
            this.colitmUnitPrice.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colitmUnitPrice.AppearanceHeader.Options.UseFont = true;
            this.colitmUnitPrice.Caption = "Rate per";
            this.colitmUnitPrice.DisplayFormat.FormatString = "n";
            this.colitmUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colitmUnitPrice.FieldName = "UNIT_PRICE";
            this.colitmUnitPrice.Name = "colitmUnitPrice";
            this.colitmUnitPrice.OptionsColumn.AllowEdit = false;
            this.colitmUnitPrice.OptionsColumn.AllowFocus = false;
            this.colitmUnitPrice.Visible = true;
            this.colitmUnitPrice.VisibleIndex = 3;
            // 
            // colitmAmount
            // 
            this.colitmAmount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colitmAmount.AppearanceHeader.Options.UseFont = true;
            this.colitmAmount.Caption = "Amount";
            this.colitmAmount.DisplayFormat.FormatString = "n";
            this.colitmAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colitmAmount.FieldName = "AMOUNT";
            this.colitmAmount.Name = "colitmAmount";
            this.colitmAmount.OptionsColumn.AllowEdit = false;
            this.colitmAmount.OptionsColumn.AllowFocus = false;
            this.colitmAmount.Visible = true;
            this.colitmAmount.VisibleIndex = 4;
            // 
            // gcSoldUtilized
            // 
            gridLevelNode1.LevelTemplate = this.gvItemDetails;
            gridLevelNode1.RelationName = "Item Details";
            this.gcSoldUtilized.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcSoldUtilized.Location = new System.Drawing.Point(2, 66);
            this.gcSoldUtilized.MainView = this.gvSoldUtilized;
            this.gcSoldUtilized.Name = "gcSoldUtilized";
            this.gcSoldUtilized.Size = new System.Drawing.Size(980, 300);
            this.gcSoldUtilized.TabIndex = 7;
            this.gcSoldUtilized.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSoldUtilized,
            this.gvItemDetails});
            this.gcSoldUtilized.DoubleClick += new System.EventHandler(this.gcSoldUtilized_DoubleClick);
            // 
            // gvSoldUtilized
            // 
            this.gvSoldUtilized.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvSoldUtilized.Appearance.FocusedRow.Options.UseFont = true;
            this.gvSoldUtilized.ChildGridLevelName = "ItemDetails";
            this.gvSoldUtilized.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSalesId,
            this.colsalesDate,
            this.colsalesRefNo,
            this.colCustomerName,
            this.colDiscount,
            this.colOtherCharges,
            this.colTax,
            this.colNetpay,
            this.colSalesType});
            this.gvSoldUtilized.GridControl = this.gcSoldUtilized;
            this.gvSoldUtilized.Name = "gvSoldUtilized";
            this.gvSoldUtilized.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvSoldUtilized.OptionsView.ShowGroupPanel = false;
            this.gvSoldUtilized.RowCountChanged += new System.EventHandler(this.gvSoldUtilized_RowCountChanged);
            // 
            // colSalesId
            // 
            this.colSalesId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSalesId.AppearanceHeader.Options.UseFont = true;
            this.colSalesId.Caption = "Sales Id";
            this.colSalesId.FieldName = "SALES_ID";
            this.colSalesId.Name = "colSalesId";
            // 
            // colsalesDate
            // 
            this.colsalesDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colsalesDate.AppearanceHeader.Options.UseFont = true;
            this.colsalesDate.Caption = "Date";
            this.colsalesDate.FieldName = "SALES_DATE";
            this.colsalesDate.Name = "colsalesDate";
            this.colsalesDate.OptionsColumn.AllowEdit = false;
            this.colsalesDate.OptionsColumn.AllowFocus = false;
            this.colsalesDate.Visible = true;
            this.colsalesDate.VisibleIndex = 0;
            this.colsalesDate.Width = 120;
            // 
            // colsalesRefNo
            // 
            this.colsalesRefNo.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colsalesRefNo.AppearanceHeader.Options.UseFont = true;
            this.colsalesRefNo.Caption = "Ref. No";
            this.colsalesRefNo.FieldName = "REF_NO";
            this.colsalesRefNo.Name = "colsalesRefNo";
            this.colsalesRefNo.OptionsColumn.AllowEdit = false;
            this.colsalesRefNo.OptionsColumn.AllowFocus = false;
            this.colsalesRefNo.Visible = true;
            this.colsalesRefNo.VisibleIndex = 1;
            this.colsalesRefNo.Width = 120;
            // 
            // colCustomerName
            // 
            this.colCustomerName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCustomerName.AppearanceHeader.Options.UseFont = true;
            this.colCustomerName.Caption = "Recipient Name";
            this.colCustomerName.FieldName = "CUSTOMER_NAME";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.OptionsColumn.AllowFocus = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 2;
            this.colCustomerName.Width = 120;
            // 
            // colDiscount
            // 
            this.colDiscount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDiscount.AppearanceHeader.Options.UseFont = true;
            this.colDiscount.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiscount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDiscount.Caption = "Discount";
            this.colDiscount.DisplayFormat.FormatString = "n";
            this.colDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDiscount.FieldName = "DISCOUNT";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.OptionsColumn.AllowEdit = false;
            this.colDiscount.OptionsColumn.AllowFocus = false;
            this.colDiscount.Visible = true;
            this.colDiscount.VisibleIndex = 3;
            this.colDiscount.Width = 120;
            // 
            // colOtherCharges
            // 
            this.colOtherCharges.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colOtherCharges.AppearanceHeader.Options.UseFont = true;
            this.colOtherCharges.AppearanceHeader.Options.UseTextOptions = true;
            this.colOtherCharges.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colOtherCharges.Caption = "Other Charges";
            this.colOtherCharges.DisplayFormat.FormatString = "n";
            this.colOtherCharges.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOtherCharges.FieldName = "OTHER_CHARGES";
            this.colOtherCharges.Name = "colOtherCharges";
            this.colOtherCharges.OptionsColumn.AllowEdit = false;
            this.colOtherCharges.OptionsColumn.AllowFocus = false;
            this.colOtherCharges.Visible = true;
            this.colOtherCharges.VisibleIndex = 4;
            this.colOtherCharges.Width = 120;
            // 
            // colTax
            // 
            this.colTax.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTax.AppearanceHeader.Options.UseFont = true;
            this.colTax.AppearanceHeader.Options.UseTextOptions = true;
            this.colTax.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTax.Caption = "Tax Amount";
            this.colTax.DisplayFormat.FormatString = "n";
            this.colTax.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTax.FieldName = "TAX_AMOUNT";
            this.colTax.Name = "colTax";
            this.colTax.OptionsColumn.AllowEdit = false;
            this.colTax.OptionsColumn.AllowFocus = false;
            this.colTax.Visible = true;
            this.colTax.VisibleIndex = 5;
            this.colTax.Width = 159;
            // 
            // colNetpay
            // 
            this.colNetpay.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNetpay.AppearanceHeader.Options.UseFont = true;
            this.colNetpay.AppearanceHeader.Options.UseTextOptions = true;
            this.colNetpay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNetpay.Caption = "Net Amount";
            this.colNetpay.DisplayFormat.FormatString = "n";
            this.colNetpay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNetpay.FieldName = "NET_PAY";
            this.colNetpay.Name = "colNetpay";
            this.colNetpay.OptionsColumn.AllowEdit = false;
            this.colNetpay.OptionsColumn.AllowFocus = false;
            this.colNetpay.Visible = true;
            this.colNetpay.VisibleIndex = 6;
            this.colNetpay.Width = 100;
            // 
            // colSalesType
            // 
            this.colSalesType.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSalesType.AppearanceHeader.Options.UseFont = true;
            this.colSalesType.Caption = "Sales Type";
            this.colSalesType.FieldName = "SALES_TYPE";
            this.colSalesType.Name = "colSalesType";
            this.colSalesType.OptionsColumn.AllowEdit = false;
            this.colSalesType.OptionsColumn.AllowFocus = false;
            this.colSalesType.Width = 103;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.deDateFrom);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.ucSoldUtilized);
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.deDateTo);
            this.layoutControl1.Controls.Add(this.gcSoldUtilized);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(123, 246, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(984, 391);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(726, 38);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(59, 22);
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "&Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // deDateFrom
            // 
            this.deDateFrom.EditValue = null;
            this.deDateFrom.EnterMoveNextControl = true;
            this.deDateFrom.Location = new System.Drawing.Point(531, 39);
            this.deDateFrom.Name = "deDateFrom";
            this.deDateFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deDateFrom.Size = new System.Drawing.Size(83, 20);
            this.deDateFrom.StyleController = this.layoutControl1;
            this.deDateFrom.TabIndex = 9;
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 370);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(75, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 6;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // ucSoldUtilized
            // 
            this.ucSoldUtilized.ChangeAddCaption = "&Add";
            this.ucSoldUtilized.ChangeCaption = "&Edit";
            this.ucSoldUtilized.ChangeDeleteCaption = "&Delete";
            this.ucSoldUtilized.ChangeMoveVoucherCaption = "&Move Voucher";
            toolTipTitleItem1.Text = "Move Transaction (Alt + T)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "         Click on this to Move Transaction  Record.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucSoldUtilized.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucSoldUtilized.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucSoldUtilized.ChangePostInterestCaption = "P&ost Interest";
            this.ucSoldUtilized.ChangePrintCaption = "&Print";
            this.ucSoldUtilized.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucSoldUtilized.DisableAddButton = true;
            this.ucSoldUtilized.DisableAMCRenew = true;
            this.ucSoldUtilized.DisableCloseButton = true;
            this.ucSoldUtilized.DisableDeleteButton = true;
            this.ucSoldUtilized.DisableDownloadExcel = true;
            this.ucSoldUtilized.DisableEditButton = true;
            this.ucSoldUtilized.DisableMoveTransaction = false;
            this.ucSoldUtilized.DisableNatureofPayments = false;
            this.ucSoldUtilized.DisablePostInterest = true;
            this.ucSoldUtilized.DisablePrintButton = true;
            this.ucSoldUtilized.DisableRestoreVoucher = false;
            this.ucSoldUtilized.Location = new System.Drawing.Point(2, 2);
            this.ucSoldUtilized.Name = "ucSoldUtilized";
            this.ucSoldUtilized.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucSoldUtilized.ShowHTML = true;
            this.ucSoldUtilized.ShowMMT = true;
            this.ucSoldUtilized.ShowPDF = true;
            this.ucSoldUtilized.ShowRTF = true;
            this.ucSoldUtilized.ShowText = true;
            this.ucSoldUtilized.ShowXLS = true;
            this.ucSoldUtilized.ShowXLSX = true;
            this.ucSoldUtilized.Size = new System.Drawing.Size(980, 32);
            this.ucSoldUtilized.TabIndex = 4;
            this.ucSoldUtilized.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucSoldUtilized.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucSoldUtilized.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucSoldUtilized.AddClicked += new System.EventHandler(this.ucSoldUtilized_AddClicked);
            this.ucSoldUtilized.EditClicked += new System.EventHandler(this.ucSoldUtilized_EditClicked);
            this.ucSoldUtilized.DeleteClicked += new System.EventHandler(this.ucSoldUtilized_DeleteClicked);
            this.ucSoldUtilized.PrintClicked += new System.EventHandler(this.ucSoldUtilized_PrintClicked);
            this.ucSoldUtilized.CloseClicked += new System.EventHandler(this.ucSoldUtilized_CloseClicked);
            this.ucSoldUtilized.RefreshClicked += new System.EventHandler(this.ucSoldUtilized_RefreshClicked);
            // 
            // glkpProject
            // 
            this.glkpProject.EnterMoveNextControl = true;
            this.glkpProject.Location = new System.Drawing.Point(49, 39);
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = "";
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(545, 20);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.Size = new System.Drawing.Size(428, 20);
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.TabIndex = 8;
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.ColProject});
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
            // ColProject
            // 
            this.ColProject.Caption = "Project";
            this.ColProject.FieldName = "PROJECT";
            this.ColProject.Name = "ColProject";
            this.ColProject.Visible = true;
            this.ColProject.VisibleIndex = 0;
            // 
            // deDateTo
            // 
            this.deDateTo.EditValue = null;
            this.deDateTo.EnterMoveNextControl = true;
            this.deDateTo.Location = new System.Drawing.Point(636, 39);
            this.deDateTo.Name = "deDateTo";
            this.deDateTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deDateTo.Size = new System.Drawing.Size(88, 20);
            this.deDateTo.StyleController = this.layoutControl1;
            this.deDateTo.TabIndex = 10;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.lblRecordCount,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(984, 391);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucSoldUtilized;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 36);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 36);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(984, 36);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 368);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 23);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblRecordCount.CustomizationFormText = "0";
            this.lblRecordCount.Location = new System.Drawing.Point(947, 368);
            this.lblRecordCount.MinSize = new System.Drawing.Size(10, 17);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(37, 23);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.Text = "0";
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(6, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcSoldUtilized;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(984, 304);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.glkpProject;
            this.layoutControlItem4.CustomizationFormText = "Project";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 3, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(477, 28);
            this.layoutControlItem4.Text = "Project";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(41, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(477, 36);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(12, 28);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.deDateFrom;
            this.layoutControlItem5.CustomizationFormText = "From ";
            this.layoutControlItem5.Location = new System.Drawing.Point(489, 36);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(125, 28);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(125, 28);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 3, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(125, 28);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "From ";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(32, 13);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.Control = this.deDateTo;
            this.layoutControlItem6.CustomizationFormText = "To";
            this.layoutControlItem6.Location = new System.Drawing.Point(614, 36);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(110, 28);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(110, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 3, 5);
            this.layoutControlItem6.Size = new System.Drawing.Size(110, 28);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "To";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(14, 13);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnApply;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(724, 36);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(63, 28);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(79, 368);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(855, 23);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(787, 36);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(197, 28);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.CustomizationFormText = "#";
            this.simpleLabelItem1.Location = new System.Drawing.Point(934, 368);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(13, 23);
            this.simpleLabelItem1.Text = "#";
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmSoldUtilizedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 391);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSoldUtilizedView";
            this.ShowIcon = false;
            this.ShowFilterClicked += new System.EventHandler(this.frmSoldUtilizedView_ShowFilterClicked);
            this.Activated += new System.EventHandler(this.frmSoldUtilizedView_Activated);
            this.Load += new System.EventHandler(this.frmSoldUtilizedView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvItemDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSoldUtilized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSoldUtilized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private Bosco.Utility.Controls.ucToolBar ucSoldUtilized;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraGrid.GridControl gcSoldUtilized;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSoldUtilized;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesId;
        private DevExpress.XtraGrid.Columns.GridColumn colsalesDate;
        private DevExpress.XtraGrid.Columns.GridColumn colsalesRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colOtherCharges;
        private DevExpress.XtraGrid.Columns.GridColumn colTax;
        private DevExpress.XtraGrid.Columns.GridColumn colNetpay;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesType;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.DateEdit deDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DateEdit deDateTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn ColProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colitemSalesId;
        private DevExpress.XtraGrid.Columns.GridColumn colitmItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colitmLocationName;
        private DevExpress.XtraGrid.Columns.GridColumn colitmQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colitmUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colitmAmount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;

    }
}