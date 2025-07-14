namespace ACPP.Modules.Asset.Masters
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucAssetItemsView = new ACPP.Modules.UIControls.ucToolBar();
            this.gcAssetItems = new DevExpress.XtraGrid.GridControl();
            this.gvAssetItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colassetItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccoutLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepreciationLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDisposalledger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRatePerItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).BeginInit();
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
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucAssetItemsView);
            this.layoutControl1.Controls.Add(this.gcAssetItems);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(386, 250, 254, 300);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(975, 485);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ucAssetItemsView
            // 
            this.ucAssetItemsView.ChangeAddCaption = "&Add";
            this.ucAssetItemsView.ChangeCaption = "&Edit";
            this.ucAssetItemsView.ChangeDeleteCaption = "&Delete";
            this.ucAssetItemsView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAssetItemsView.ChangePrintCaption = "&Print";
            this.ucAssetItemsView.DisableAddButton = true;
            this.ucAssetItemsView.DisableCloseButton = true;
            this.ucAssetItemsView.DisableDeleteButton = true;
            this.ucAssetItemsView.DisableDownloadExcel = true;
            this.ucAssetItemsView.DisableEditButton = true;
            this.ucAssetItemsView.DisableMoveTransaction = true;
            this.ucAssetItemsView.DisableNatureofPayments = true;
            this.ucAssetItemsView.DisablePrintButton = true;
            this.ucAssetItemsView.DisableRestoreVoucher = true;
            this.ucAssetItemsView.Location = new System.Drawing.Point(0, 0);
            this.ucAssetItemsView.Name = "ucAssetItemsView";
            this.ucAssetItemsView.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucAssetItemsView.ShowHTML = true;
            this.ucAssetItemsView.ShowMMT = true;
            this.ucAssetItemsView.ShowPDF = true;
            this.ucAssetItemsView.ShowRTF = true;
            this.ucAssetItemsView.ShowText = true;
            this.ucAssetItemsView.ShowXLS = true;
            this.ucAssetItemsView.ShowXLSX = true;
            this.ucAssetItemsView.Size = new System.Drawing.Size(975, 31);
            this.ucAssetItemsView.TabIndex = 4;
            this.ucAssetItemsView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetItemsView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetItemsView.AddClicked += new System.EventHandler(this.ucAssetItemsView_AddClicked);
            this.ucAssetItemsView.EditClicked += new System.EventHandler(this.ucAssetItemsView_EditClicked);
            this.ucAssetItemsView.DeleteClicked += new System.EventHandler(this.ucAssetItemsView_DeleteClicked);
            this.ucAssetItemsView.CloseClicked += new System.EventHandler(this.ucAssetItemsView_CloseClicked);
            // 
            // gcAssetItems
            // 
            this.gcAssetItems.Location = new System.Drawing.Point(0, 31);
            this.gcAssetItems.MainView = this.gvAssetItems;
            this.gcAssetItems.Name = "gcAssetItems";
            this.gcAssetItems.Size = new System.Drawing.Size(975, 430);
            this.gcAssetItems.TabIndex = 7;
            this.gcAssetItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetItems});
            this.gcAssetItems.Click += new System.EventHandler(this.gcAssetItems_Click);
            // 
            // gvAssetItems
            // 
            this.gvAssetItems.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAssetItems.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetItems.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAssetItems.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colAssetGroup,
            this.colCategory,
            this.colUnit,
            this.colassetItemName,
            this.colAccoutLedger,
            this.colDepreciationLedger,
            this.colDisposalledger,
            this.colMethod,
            this.colQuantity,
            this.colRatePerItem,
            this.colTotal});
            this.gvAssetItems.GridControl = this.gcAssetItems;
            this.gvAssetItems.Name = "gvAssetItems";
            this.gvAssetItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetItems.OptionsView.ShowGroupPanel = false;
            this.gvAssetItems.DoubleClick += new System.EventHandler(this.gvAssetItems_DoubleClick);
            this.gvAssetItems.RowCountChanged += new System.EventHandler(this.gvAssetItems_RowCountChanged);
            // 
            // colId
            // 
            this.colId.Caption = "id";
            this.colId.FieldName = "ITEM_ID";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            // 
            // colAssetGroup
            // 
            this.colAssetGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAssetGroup.AppearanceHeader.Options.UseFont = true;
            this.colAssetGroup.Caption = "Asset Group";
            this.colAssetGroup.FieldName = "ASSET_GROUP";
            this.colAssetGroup.Name = "colAssetGroup";
            this.colAssetGroup.OptionsColumn.AllowEdit = false;
            this.colAssetGroup.Visible = true;
            this.colAssetGroup.VisibleIndex = 1;
            // 
            // colCategory
            // 
            this.colCategory.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCategory.AppearanceHeader.Options.UseFont = true;
            this.colCategory.Caption = "Category";
            this.colCategory.FieldName = "CATEGORY";
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.AllowEdit = false;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colUnit.AppearanceHeader.Options.UseFont = true;
            this.colUnit.Caption = "Unit";
            this.colUnit.FieldName = "UNIT";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 0;
            // 
            // colassetItemName
            // 
            this.colassetItemName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colassetItemName.AppearanceHeader.Options.UseFont = true;
            this.colassetItemName.Caption = "Name";
            this.colassetItemName.FieldName = "NAME";
            this.colassetItemName.Name = "colassetItemName";
            this.colassetItemName.OptionsColumn.AllowEdit = false;
            this.colassetItemName.Visible = true;
            this.colassetItemName.VisibleIndex = 2;
            // 
            // colAccoutLedger
            // 
            this.colAccoutLedger.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAccoutLedger.AppearanceHeader.Options.UseFont = true;
            this.colAccoutLedger.Caption = "Account Ledger";
            this.colAccoutLedger.FieldName = "ACCOUNT_LEDGER";
            this.colAccoutLedger.Name = "colAccoutLedger";
            this.colAccoutLedger.OptionsColumn.AllowEdit = false;
            this.colAccoutLedger.Visible = true;
            this.colAccoutLedger.VisibleIndex = 3;
            // 
            // colDepreciationLedger
            // 
            this.colDepreciationLedger.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDepreciationLedger.AppearanceHeader.Options.UseFont = true;
            this.colDepreciationLedger.Caption = "Depreciation Ledger";
            this.colDepreciationLedger.FieldName = "DEPRECIATION_LEDGER";
            this.colDepreciationLedger.Name = "colDepreciationLedger";
            this.colDepreciationLedger.OptionsColumn.AllowEdit = false;
            this.colDepreciationLedger.Visible = true;
            this.colDepreciationLedger.VisibleIndex = 4;
            // 
            // colDisposalledger
            // 
            this.colDisposalledger.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDisposalledger.AppearanceHeader.Options.UseFont = true;
            this.colDisposalledger.Caption = "Disposal ledger";
            this.colDisposalledger.FieldName = "DISPOSAL_LEDGER";
            this.colDisposalledger.Name = "colDisposalledger";
            this.colDisposalledger.OptionsColumn.AllowEdit = false;
            this.colDisposalledger.Visible = true;
            this.colDisposalledger.VisibleIndex = 5;
            // 
            // colMethod
            // 
            this.colMethod.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colMethod.AppearanceHeader.Options.UseFont = true;
            this.colMethod.Caption = "Method";
            this.colMethod.FieldName = "METHOD";
            this.colMethod.Name = "colMethod";
            this.colMethod.OptionsColumn.AllowEdit = false;
            this.colMethod.Visible = true;
            this.colMethod.VisibleIndex = 6;
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.DisplayFormat.FormatString = "n";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 7;
            // 
            // colRatePerItem
            // 
            this.colRatePerItem.Caption = "Rate Per Item";
            this.colRatePerItem.DisplayFormat.FormatString = "n";
            this.colRatePerItem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRatePerItem.FieldName = "RATE_PER_ITEM";
            this.colRatePerItem.Name = "colRatePerItem";
            this.colRatePerItem.OptionsColumn.AllowEdit = false;
            this.colRatePerItem.Visible = true;
            this.colRatePerItem.VisibleIndex = 8;
            // 
            // colTotal
            // 
            this.colTotal.Caption = "Total";
            this.colTotal.DisplayFormat.FormatString = "n";
            this.colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal.FieldName = "TOTAL";
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.AllowEdit = false;
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 9;
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(0, 461);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(79, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
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
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucAssetItemsView;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(196, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(975, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcAssetItems;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(975, 430);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 461);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 461);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(831, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.AllowHotTrack = false;
            this.lblCountNumber.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountNumber.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountNumber.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCountNumber.CustomizationFormText = "0";
            this.lblCountNumber.Location = new System.Drawing.Point(928, 461);
            this.lblCountNumber.MaxSize = new System.Drawing.Size(0, 17);
            this.lblCountNumber.MinSize = new System.Drawing.Size(11, 17);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(47, 24);
            this.lblCountNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountNumber.Text = "0";
            this.lblCountNumber.TextSize = new System.Drawing.Size(9, 13);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.CustomizationFormText = "#";
            this.simpleLabelItem1.Location = new System.Drawing.Point(910, 461);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(18, 24);
            this.simpleLabelItem1.Text = "#";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmAssetItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 485);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetItemView";
            this.Text = "Items";
            this.Load += new System.EventHandler(this.frmAssetItemView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).EndInit();
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
        private UIControls.ucToolBar ucAssetItemsView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcAssetItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetGroup;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colassetItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccoutLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colDepreciationLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colDisposalledger;
        private DevExpress.XtraGrid.Columns.GridColumn colMethod;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colRatePerItem;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountNumber;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
    }
}