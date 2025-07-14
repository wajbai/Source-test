namespace ACPP.Modules.Asset.Masters
{
    partial class frmAssetCategoryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetCategoryView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblCountchanged = new DevExpress.XtraEditors.LabelControl();
            this.lblCountRecord = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.ucAssetCategories = new ACPP.Modules.UIControls.ucToolBar();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.trlCategories = new DevExpress.XtraTreeList.TreeList();
            this.colCategoryId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ParentGroupId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.pnlName = new DevExpress.XtraEditors.PanelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.gcAssetItems = new DevExpress.XtraGrid.GridControl();
            this.gvAssetItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlName)).BeginInit();
            this.pnlName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblCountchanged);
            this.layoutControl1.Controls.Add(this.lblCountRecord);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.ucAssetCategories);
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(435, 239, 409, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(735, 510);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblCountchanged
            // 
            this.lblCountchanged.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountchanged.Location = new System.Drawing.Point(682, 488);
            this.lblCountchanged.Name = "lblCountchanged";
            this.lblCountchanged.Size = new System.Drawing.Size(14, 20);
            this.lblCountchanged.StyleController = this.layoutControl1;
            this.lblCountchanged.TabIndex = 8;
            this.lblCountchanged.Text = "#";
            // 
            // lblCountRecord
            // 
            this.lblCountRecord.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountRecord.Location = new System.Drawing.Point(700, 488);
            this.lblCountRecord.Name = "lblCountRecord";
            this.lblCountRecord.Size = new System.Drawing.Size(33, 20);
            this.lblCountRecord.StyleController = this.layoutControl1;
            this.lblCountRecord.TabIndex = 7;
            this.lblCountRecord.Text = "0";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 488);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(105, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 6;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // ucAssetCategories
            // 
            this.ucAssetCategories.ChangeAddCaption = "&Add";
            this.ucAssetCategories.ChangeCaption = "&Edit";
            this.ucAssetCategories.ChangeDeleteCaption = "&Delete";
            this.ucAssetCategories.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAssetCategories.ChangePrintCaption = "&Print";
            this.ucAssetCategories.DisableAddButton = true;
            this.ucAssetCategories.DisableCloseButton = true;
            this.ucAssetCategories.DisableDeleteButton = true;
            this.ucAssetCategories.DisableDownloadExcel = true;
            this.ucAssetCategories.DisableEditButton = true;
            this.ucAssetCategories.DisableMoveTransaction = true;
            this.ucAssetCategories.DisableNatureofPayments = true;
            this.ucAssetCategories.DisablePrintButton = true;
            this.ucAssetCategories.DisableRestoreVoucher = true;
            this.ucAssetCategories.Location = new System.Drawing.Point(0, 0);
            this.ucAssetCategories.Name = "ucAssetCategories";
            this.ucAssetCategories.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucAssetCategories.ShowHTML = true;
            this.ucAssetCategories.ShowMMT = true;
            this.ucAssetCategories.ShowPDF = true;
            this.ucAssetCategories.ShowRTF = true;
            this.ucAssetCategories.ShowText = true;
            this.ucAssetCategories.ShowXLS = true;
            this.ucAssetCategories.ShowXLSX = true;
            this.ucAssetCategories.Size = new System.Drawing.Size(735, 30);
            this.ucAssetCategories.TabIndex = 5;
            this.ucAssetCategories.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetCategories.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetCategories.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetCategories.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetCategories.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetCategories.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetCategories.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetCategories.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetCategories.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetCategories.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetCategories.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetCategories.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetCategories.AddClicked += new System.EventHandler(this.ucAssetCategories_AddClicked);
            this.ucAssetCategories.EditClicked += new System.EventHandler(this.ucAssetCategories_EditClicked);
            this.ucAssetCategories.DeleteClicked += new System.EventHandler(this.ucAssetCategories_DeleteClicked);
            this.ucAssetCategories.PrintClicked += new System.EventHandler(this.ucAssetCategories_PrintClicked);
            this.ucAssetCategories.CloseClicked += new System.EventHandler(this.ucAssetCategories_CloseClicked);
            this.ucAssetCategories.RefreshClicked += new System.EventHandler(this.ucAssetCategories_RefreshClicked);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 30);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.trlCategories);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(735, 456);
            this.splitContainerControl1.SplitterPosition = 248;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // trlCategories
            // 
            this.trlCategories.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlCategories.Appearance.FocusedCell.Options.UseFont = true;
            this.trlCategories.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.trlCategories.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlCategories.Appearance.FocusedRow.Options.UseBackColor = true;
            this.trlCategories.Appearance.FocusedRow.Options.UseFont = true;
            this.trlCategories.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlCategories.Appearance.HeaderPanel.Options.UseFont = true;
            this.trlCategories.Appearance.SelectedRow.BackColor = System.Drawing.Color.LightGray;
            this.trlCategories.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
            this.trlCategories.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Transparent;
            this.trlCategories.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlCategories.Appearance.SelectedRow.Options.UseBackColor = true;
            this.trlCategories.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.trlCategories.Appearance.SelectedRow.Options.UseFont = true;
            this.trlCategories.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCategoryId,
            this.Name,
            this.ParentGroupId});
            this.trlCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trlCategories.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.trlCategories.ImageIndexFieldName = "IMAGE_ID";
            this.trlCategories.KeyFieldName = "CATEGORY_ID";
            this.trlCategories.Location = new System.Drawing.Point(0, 0);
            this.trlCategories.Name = "trlCategories";
            this.trlCategories.OptionsBehavior.Editable = false;
            this.trlCategories.OptionsBehavior.PopulateServiceColumns = true;
            this.trlCategories.OptionsView.ShowHorzLines = false;
            this.trlCategories.OptionsView.ShowVertLines = false;
            this.trlCategories.ParentFieldName = "PARENT_CATEGORY_ID";
            this.trlCategories.PreviewFieldName = "NAME";
            this.trlCategories.SelectImageList = this.imageSmall;
            this.trlCategories.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.Default;
            this.trlCategories.Size = new System.Drawing.Size(248, 456);
            this.trlCategories.TabIndex = 0;
            this.trlCategories.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.trlCategories_FocusedNodeChanged);
            // 
            // colCategoryId
            // 
            this.colCategoryId.Caption = "Category Id";
            this.colCategoryId.FieldName = "CATEGORY_ID";
            this.colCategoryId.Name = "colCategoryId";
            // 
            // Name
            // 
            this.Name.Caption = "Category";
            this.Name.FieldName = "NAME";
            this.Name.MinWidth = 33;
            this.Name.Name = "Name";
            this.Name.Visible = true;
            this.Name.VisibleIndex = 0;
            // 
            // ParentGroupId
            // 
            this.ParentGroupId.Caption = "ParentCategoryId";
            this.ParentGroupId.FieldName = "PARENT_CATEGORY_ID";
            this.ParentGroupId.Name = "ParentGroupId";
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.pnlName);
            this.layoutControl2.Controls.Add(this.gcAssetItems);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(181, 346, 266, 276);
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(482, 456);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // pnlName
            // 
            this.pnlName.Controls.Add(this.lblName);
            this.pnlName.Location = new System.Drawing.Point(0, 0);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(482, 21);
            this.pnlName.TabIndex = 5;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(5, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Items";
            // 
            // gcAssetItems
            // 
            this.gcAssetItems.Location = new System.Drawing.Point(0, 21);
            this.gcAssetItems.MainView = this.gvAssetItems;
            this.gcAssetItems.Margin = new System.Windows.Forms.Padding(0);
            this.gcAssetItems.Name = "gcAssetItems";
            this.gcAssetItems.Size = new System.Drawing.Size(482, 435);
            this.gcAssetItems.TabIndex = 4;
            this.gcAssetItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetItems});
            // 
            // gvAssetItems
            // 
            this.gvAssetItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName,
            this.colGroupName});
            this.gvAssetItems.GridControl = this.gcAssetItems;
            this.gvAssetItems.Name = "gvAssetItems";
            this.gvAssetItems.OptionsBehavior.Editable = false;
            this.gvAssetItems.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetItems.OptionsView.ShowGroupPanel = false;
            this.gvAssetItems.RowCountChanged += new System.EventHandler(this.gvAssetItems_RowCountChanged);
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "ITEM_ID";
            this.colId.Name = "colId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.Caption = "Name";
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 64;
            // 
            // colGroupName
            // 
            this.colGroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGroupName.AppearanceHeader.Options.UseFont = true;
            this.colGroupName.Caption = "Group Name";
            this.colGroupName.FieldName = "GROUP_NAME";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 1;
            this.colGroupName.Width = 332;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Root";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(482, 456);
            this.layoutControlGroup2.Text = "Root";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcAssetItems;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 21);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(482, 435);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.pnlName;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem4.Size = new System.Drawing.Size(482, 21);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(735, 510);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.splitContainerControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(735, 456);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucAssetCategories;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(735, 30);
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
            this.emptySpaceItem2.Location = new System.Drawing.Point(109, 486);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(571, 24);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkShowFilter;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 486);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(79, 23);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(109, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lblCountRecord;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(698, 486);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(11, 17);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(37, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lblCountchanged;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(680, 486);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(13, 17);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(18, 24);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // frmAssetCategoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 510);
            this.Controls.Add(this.layoutControl1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Category";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAssetCategoriesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlName)).EndInit();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UIControls.ucToolBar ucAssetCategories;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList trlCategories;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraGrid.GridControl gcAssetItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetItems;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        //private DevExpress.XtraTreeList.Columns.TreeListColumn CategoryId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ParentGroupId;
        private DevExpress.XtraEditors.PanelControl pnlName;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCategoryId;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LabelControl lblCountchanged;
        private DevExpress.XtraEditors.LabelControl lblCountRecord;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.Utils.ImageCollection imageSmall;

    }
}