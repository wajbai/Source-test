namespace ACPP.Modules.Inventory.Stock
{
    partial class frmStockItemView
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
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcStockView = new DevExpress.XtraGrid.GridControl();
            this.gvStockView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucStockView = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcStockView);
            this.layoutControl1.Controls.Add(this.ucStockView);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(152, 167, 295, 386);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1038, 439);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 418);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(75, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 9;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcStockView
            // 
            this.gcStockView.Location = new System.Drawing.Point(0, 30);
            this.gcStockView.MainView = this.gvStockView;
            this.gcStockView.Name = "gcStockView";
            this.gcStockView.Size = new System.Drawing.Size(1038, 386);
            this.gcStockView.TabIndex = 5;
            this.gcStockView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStockView});
            // 
            // gvStockView
            // 
            this.gvStockView.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvStockView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvStockView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemId,
            this.colName,
            this.colGroup,
            this.colCategoryName,
            this.colUnitName,
            this.colQuantity,
            this.colRate});
            this.gvStockView.GridControl = this.gcStockView;
            this.gvStockView.Name = "gvStockView";
            this.gvStockView.OptionsBehavior.Editable = false;
            this.gvStockView.OptionsFind.AllowFindPanel = false;
            this.gvStockView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvStockView.OptionsView.ShowGroupPanel = false;
            this.gvStockView.DoubleClick += new System.EventHandler(this.gvStockView_DoubleClick);
            this.gvStockView.RowCountChanged += new System.EventHandler(this.gvStockView_RowCountChanged);
            // 
            // colItemId
            // 
            this.colItemId.Caption = "Id";
            this.colItemId.FieldName = "ITEM_ID";
            this.colItemId.Name = "colItemId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.Caption = "Name";
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 431;
            // 
            // colGroup
            // 
            this.colGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGroup.AppearanceHeader.Options.UseFont = true;
            this.colGroup.Caption = "Group";
            this.colGroup.FieldName = "ASSET_CLASS";
            this.colGroup.Name = "colGroup";
            this.colGroup.OptionsColumn.AllowEdit = false;
            this.colGroup.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colGroup.Visible = true;
            this.colGroup.VisibleIndex = 1;
            this.colGroup.Width = 242;
            // 
            // colCategoryName
            // 
            this.colCategoryName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCategoryName.AppearanceHeader.Options.UseFont = true;
            this.colCategoryName.Caption = "Category";
            this.colCategoryName.FieldName = "CATEGORY_NAME";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.Width = 242;
            // 
            // colUnitName
            // 
            this.colUnitName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colUnitName.AppearanceHeader.Options.UseFont = true;
            this.colUnitName.Caption = "Measure";
            this.colUnitName.FieldName = "SYMBOL";
            this.colUnitName.Name = "colUnitName";
            this.colUnitName.OptionsColumn.FixedWidth = true;
            this.colUnitName.Width = 133;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colQuantity.AppearanceHeader.Options.UseFont = true;
            this.colQuantity.Caption = "Stock Quantity";
            this.colQuantity.DisplayFormat.FormatString = "n";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 140;
            // 
            // colRate
            // 
            this.colRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colRate.AppearanceHeader.Options.UseFont = true;
            this.colRate.Caption = "Rate per";
            this.colRate.DisplayFormat.FormatString = "n";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "RATE";
            this.colRate.Name = "colRate";
            this.colRate.OptionsColumn.FixedWidth = true;
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 2;
            this.colRate.Width = 148;
            // 
            // ucStockView
            // 
            this.ucStockView.ChangeAddCaption = "&Add";
            this.ucStockView.ChangeCaption = "&Edit";
            this.ucStockView.ChangeDeleteCaption = "&Delete";
            this.ucStockView.ChangeMoveVoucherCaption = "&Move Voucher";
            toolTipTitleItem2.Text = "Move Transaction (Alt + T)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "         Click on this to Move Transaction  Record.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucStockView.ChangeMoveVoucherTooltip = superToolTip2;
            this.ucStockView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucStockView.ChangePostInterestCaption = "P&ost Interest";
            this.ucStockView.ChangePrintCaption = "&Print";
            this.ucStockView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucStockView.DisableAddButton = true;
            this.ucStockView.DisableAMCRenew = true;
            this.ucStockView.DisableCloseButton = true;
            this.ucStockView.DisableDeleteButton = true;
            this.ucStockView.DisableDownloadExcel = true;
            this.ucStockView.DisableEditButton = true;
            this.ucStockView.DisableMoveTransaction = true;
            this.ucStockView.DisableNatureofPayments = true;
            this.ucStockView.DisablePostInterest = true;
            this.ucStockView.DisablePrintButton = true;
            this.ucStockView.DisableRestoreVoucher = true;
            this.ucStockView.Location = new System.Drawing.Point(0, 1);
            this.ucStockView.Name = "ucStockView";
            this.ucStockView.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucStockView.ShowHTML = true;
            this.ucStockView.ShowMMT = true;
            this.ucStockView.ShowPDF = true;
            this.ucStockView.ShowRTF = true;
            this.ucStockView.ShowText = true;
            this.ucStockView.ShowXLS = true;
            this.ucStockView.ShowXLSX = true;
            this.ucStockView.Size = new System.Drawing.Size(1038, 29);
            this.ucStockView.TabIndex = 4;
            this.ucStockView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStockView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStockView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStockView.AddClicked += new System.EventHandler(this.ucStockView_AddClicked);
            this.ucStockView.EditClicked += new System.EventHandler(this.ucStockView_EditClicked);
            this.ucStockView.DeleteClicked += new System.EventHandler(this.ucStockView_DeleteClicked);
            this.ucStockView.PrintClicked += new System.EventHandler(this.ucStockView_PrintClicked);
            this.ucStockView.CloseClicked += new System.EventHandler(this.ucStockView_CloseClicked);
            this.ucStockView.RefreshClicked += new System.EventHandler(this.ucStockView_RefreshClicked);
            this.ucStockView.DownloadExcel += new System.EventHandler(this.ucStockView_DownloadExcel);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.lblRecordCount,
            this.simpleLabelItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 1, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1038, 439);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucStockView;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 29);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 29);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1038, 29);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcStockView;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(1038, 386);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 415);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(903, 23);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 415);
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
            this.lblRecordCount.CustomizationFormText = "0";
            this.lblRecordCount.Location = new System.Drawing.Point(995, 415);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(43, 23);
            this.lblRecordCount.Text = "0";
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(6, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.CustomizationFormText = "#";
            this.simpleLabelItem2.Location = new System.Drawing.Point(982, 415);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.Text = "#";
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmStockItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 439);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmStockItemView";
            this.Text = "Stock Item";
            this.ShowFilterClicked += new System.EventHandler(this.frmStockItemView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmStockItemView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmStockItemView_Activated);
            this.Load += new System.EventHandler(this.frmStockItemView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStockView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStockView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar ucStockView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcStockView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStockView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colCategoryName;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
    }
}