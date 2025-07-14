namespace ACPP.Modules.Inventory
{
    partial class frmAssetClassView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetClassView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.trlGroup = new DevExpress.XtraTreeList.TreeList();
            this.trlcolGroupID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trlcolName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trlcolAssetGroupId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.lblCountNumber = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gcGroupView = new DevExpress.XtraGrid.GridControl();
            this.gvGroupView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colgroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCatogry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblSelectedGroup = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ucAssetGroups = new ACPP.Modules.UIControls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroupView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroupView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectedGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Controls.Add(this.ucAssetGroups);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(238, 186, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(948, 387);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 33);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.trlGroup);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(944, 352);
            this.splitContainerControl1.SplitterPosition = 404;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // trlGroup
            // 
            this.trlGroup.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlGroup.Appearance.FocusedCell.Options.UseFont = true;
            this.trlGroup.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlGroup.Appearance.HeaderPanel.Options.UseFont = true;
            this.trlGroup.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlGroup.Appearance.SelectedRow.Options.UseFont = true;
            this.trlGroup.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.trlGroup.AppearancePrint.Row.Options.UseFont = true;
            this.trlGroup.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.trlcolGroupID,
            this.trlcolName,
            this.trlcolAssetGroupId});
            this.trlGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trlGroup.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.trlGroup.ImageIndexFieldName = "IMAGE_ID";
            this.trlGroup.KeyFieldName = "GROUP_ID";
            this.trlGroup.Location = new System.Drawing.Point(0, 0);
            this.trlGroup.Name = "trlGroup";
            this.trlGroup.OptionsBehavior.Editable = false;
            this.trlGroup.OptionsPrint.PrintAllNodes = true;
            this.trlGroup.OptionsPrint.PrintFilledTreeIndent = true;
            this.trlGroup.OptionsPrint.PrintHorzLines = false;
            this.trlGroup.OptionsPrint.PrintImages = false;
            this.trlGroup.OptionsPrint.PrintReportFooter = false;
            this.trlGroup.OptionsPrint.PrintTreeButtons = false;
            this.trlGroup.OptionsPrint.PrintVertLines = false;
            this.trlGroup.OptionsView.ShowHorzLines = false;
            this.trlGroup.OptionsView.ShowVertLines = false;
            this.trlGroup.ParentFieldName = "PARENT_GROUP_ID";
            this.trlGroup.PreviewFieldName = "GROUP_NAME";
            this.trlGroup.SelectImageList = this.imageSmall;
            this.trlGroup.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.Default;
            this.trlGroup.Size = new System.Drawing.Size(404, 352);
            this.trlGroup.TabIndex = 0;
            this.trlGroup.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.trlGroup_FocusedNodeChanged);
            this.trlGroup.DoubleClick += new System.EventHandler(this.trlGroup_DoubleClick);
            // 
            // trlcolGroupID
            // 
            this.trlcolGroupID.Caption = "treeListColumn1";
            this.trlcolGroupID.FieldName = "GROUP_ID";
            this.trlcolGroupID.Name = "trlcolGroupID";
            // 
            // trlcolName
            // 
            this.trlcolName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlcolName.AppearanceHeader.Options.UseFont = true;
            this.trlcolName.Caption = "Asset Class";
            this.trlcolName.FieldName = "GROUP_NAME";
            this.trlcolName.MinWidth = 33;
            this.trlcolName.Name = "trlcolName";
            this.trlcolName.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.trlcolName.Visible = true;
            this.trlcolName.VisibleIndex = 0;
            // 
            // trlcolAssetGroupId
            // 
            this.trlcolAssetGroupId.Caption = "treeListColumn3";
            this.trlcolAssetGroupId.FieldName = "PARENT_GROUP_ID";
            this.trlcolAssetGroupId.Name = "trlcolAssetGroupId";
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.layoutControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(535, 352);
            this.panelControl2.TabIndex = 2;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.layoutControl3);
            this.layoutControl2.Controls.Add(this.gcGroupView);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 2);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(30, 233, 250, 350);
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(531, 348);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.lblCountNumber);
            this.layoutControl3.Controls.Add(this.chkShowFilter);
            this.layoutControl3.Controls.Add(this.labelControl1);
            this.layoutControl3.Location = new System.Drawing.Point(2, 323);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(282, 317, 250, 350);
            this.layoutControl3.Root = this.Root;
            this.layoutControl3.Size = new System.Drawing.Size(527, 23);
            this.layoutControl3.TabIndex = 5;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountNumber.Location = new System.Drawing.Point(500, 2);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(7, 13);
            this.lblCountNumber.StyleController = this.layoutControl3;
            this.lblCountNumber.TabIndex = 5;
            this.lblCountNumber.Text = "0";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 2);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(106, 19);
            this.chkShowFilter.StyleController = this.layoutControl3;
            this.chkShowFilter.TabIndex = 2;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(487, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(9, 13);
            this.labelControl1.StyleController = this.layoutControl3;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "#";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem3,
            this.emptySpaceItem1});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(527, 23);
            this.Root.Text = "Root";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(110, 23);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelControl1;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(485, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(13, 23);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lblCountNumber;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(498, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(11, 23);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(110, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(375, 23);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(509, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(18, 23);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gcGroupView
            // 
            this.gcGroupView.Location = new System.Drawing.Point(0, 24);
            this.gcGroupView.MainView = this.gvGroupView;
            this.gcGroupView.Name = "gcGroupView";
            this.gcGroupView.Size = new System.Drawing.Size(531, 297);
            this.gcGroupView.TabIndex = 4;
            this.gcGroupView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGroupView});
            // 
            // gvGroupView
            // 
            this.gvGroupView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colgroup,
            this.colAssetName,
            this.colCatogry,
            this.colnUnit,
            this.colnQuantity});
            this.gvGroupView.GridControl = this.gcGroupView;
            this.gvGroupView.Name = "gvGroupView";
            this.gvGroupView.OptionsView.ShowGroupPanel = false;
            // 
            // colgroup
            // 
            this.colgroup.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colgroup.AppearanceHeader.Options.UseFont = true;
            this.colgroup.Caption = "Asset Class";
            this.colgroup.FieldName = "GROUP_NAME";
            this.colgroup.Name = "colgroup";
            this.colgroup.OptionsColumn.AllowEdit = false;
            this.colgroup.OptionsColumn.AllowFocus = false;
            this.colgroup.Visible = true;
            this.colgroup.VisibleIndex = 0;
            // 
            // colAssetName
            // 
            this.colAssetName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAssetName.AppearanceHeader.Options.UseFont = true;
            this.colAssetName.Caption = "Asset Item";
            this.colAssetName.FieldName = "NAME";
            this.colAssetName.Name = "colAssetName";
            this.colAssetName.OptionsColumn.AllowEdit = false;
            this.colAssetName.OptionsColumn.AllowFocus = false;
            this.colAssetName.Visible = true;
            this.colAssetName.VisibleIndex = 1;
            // 
            // colCatogry
            // 
            this.colCatogry.Caption = "Catogry";
            this.colCatogry.FieldName = "CATOGRY";
            this.colCatogry.Name = "colCatogry";
            this.colCatogry.OptionsColumn.AllowEdit = false;
            this.colCatogry.OptionsColumn.AllowFocus = false;
            // 
            // colnUnit
            // 
            this.colnUnit.Caption = "Unit";
            this.colnUnit.FieldName = "UNIT";
            this.colnUnit.Name = "colnUnit";
            this.colnUnit.OptionsColumn.AllowEdit = false;
            this.colnUnit.OptionsColumn.AllowFocus = false;
            // 
            // colnQuantity
            // 
            this.colnQuantity.Caption = "Quantity";
            this.colnQuantity.FieldName = "QUNATITY";
            this.colnQuantity.Name = "colnQuantity";
            this.colnQuantity.OptionsColumn.AllowEdit = false;
            this.colnQuantity.OptionsColumn.AllowFocus = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Root";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblSelectedGroup,
            this.layoutControlItem1,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(531, 348);
            this.layoutControlGroup2.Text = "Root";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // lblSelectedGroup
            // 
            this.lblSelectedGroup.AllowHotTrack = false;
            this.lblSelectedGroup.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedGroup.AppearanceItemCaption.Options.UseFont = true;
            this.lblSelectedGroup.CustomizationFormText = "Selected Group";
            this.lblSelectedGroup.Location = new System.Drawing.Point(0, 0);
            this.lblSelectedGroup.Name = "lblSelectedGroup";
            this.lblSelectedGroup.Size = new System.Drawing.Size(531, 24);
            this.lblSelectedGroup.Text = "Selected Group";
            this.lblSelectedGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblSelectedGroup.TextSize = new System.Drawing.Size(50, 20);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcGroupView;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(531, 297);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.layoutControl3;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 321);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(531, 27);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // ucAssetGroups
            // 
            this.ucAssetGroups.ChangeAddCaption = "&Add";
            this.ucAssetGroups.ChangeCaption = "&Edit";
            this.ucAssetGroups.ChangeDeleteCaption = "&Delete";
            this.ucAssetGroups.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAssetGroups.ChangePostInterestCaption = "P&ost Interest";
            this.ucAssetGroups.ChangePrintCaption = "&Print";
            this.ucAssetGroups.DisableAddButton = true;
            this.ucAssetGroups.DisableCloseButton = true;
            this.ucAssetGroups.DisableDeleteButton = true;
            this.ucAssetGroups.DisableDownloadExcel = true;
            this.ucAssetGroups.DisableEditButton = true;
            this.ucAssetGroups.DisableMoveTransaction = true;
            this.ucAssetGroups.DisableNatureofPayments = true;
            this.ucAssetGroups.DisablePostInterest = true;
            this.ucAssetGroups.DisablePrintButton = true;
            this.ucAssetGroups.DisableRestoreVoucher = true;
            this.ucAssetGroups.Location = new System.Drawing.Point(0, 0);
            this.ucAssetGroups.Name = "ucAssetGroups";
            this.ucAssetGroups.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucAssetGroups.ShowHTML = true;
            this.ucAssetGroups.ShowMMT = true;
            this.ucAssetGroups.ShowPDF = true;
            this.ucAssetGroups.ShowRTF = true;
            this.ucAssetGroups.ShowText = true;
            this.ucAssetGroups.ShowXLS = true;
            this.ucAssetGroups.ShowXLSX = true;
            this.ucAssetGroups.Size = new System.Drawing.Size(948, 31);
            this.ucAssetGroups.TabIndex = 0;
            this.ucAssetGroups.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.AddClicked += new System.EventHandler(this.ucAssetGroups_AddClicked);
            this.ucAssetGroups.EditClicked += new System.EventHandler(this.ucAssetGroups_EditClicked);
            this.ucAssetGroups.DeleteClicked += new System.EventHandler(this.ucAssetGroups_DeleteClicked);
            this.ucAssetGroups.PrintClicked += new System.EventHandler(this.ucAssetGroups_PrintClicked);
            this.ucAssetGroups.CloseClicked += new System.EventHandler(this.ucAssetGroups_CloseClicked);
            this.ucAssetGroups.RefreshClicked += new System.EventHandler(this.ucAssetGroups_RefreshClicked);
            this.ucAssetGroups.DownloadExcel += new System.EventHandler(this.ucAssetGroups_DownloadExcel);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(948, 387);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucAssetGroups;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(196, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(948, 31);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.splitContainerControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(948, 356);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // colGroupName
            // 
            this.colGroupName.Caption = "Class";
            this.colGroupName.FieldName = "GROUP_NAME";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsColumn.AllowEdit = false;
            this.colGroupName.OptionsColumn.AllowFocus = false;
            this.colGroupName.OptionsColumn.ReadOnly = true;
            this.colGroupName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 0;
            this.colGroupName.Width = 237;
            // 
            // colNAME
            // 
            this.colNAME.Caption = "Asset Item";
            this.colNAME.FieldName = "NAME";
            this.colNAME.Name = "colNAME";
            this.colNAME.OptionsColumn.AllowEdit = false;
            this.colNAME.OptionsColumn.AllowFocus = false;
            this.colNAME.OptionsColumn.ReadOnly = true;
            this.colNAME.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 1;
            this.colNAME.Width = 270;
            // 
            // colCategory
            // 
            this.colCategory.Caption = "Category";
            this.colCategory.FieldName = "CATEGORY";
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.AllowEdit = false;
            this.colCategory.OptionsColumn.AllowFocus = false;
            this.colCategory.OptionsColumn.ReadOnly = true;
            this.colCategory.Width = 119;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "Unit";
            this.colUnit.FieldName = "UNIT";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.AllowFocus = false;
            this.colUnit.OptionsColumn.ReadOnly = true;
            this.colUnit.Width = 119;
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.OptionsColumn.AllowFocus = false;
            this.colQuantity.OptionsColumn.ReadOnly = true;
            this.colQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colQuantity.Width = 101;
            // 
            // frmGroupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 387);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmGroupView";
            this.Text = "Group";
            this.ShowFilterClicked += new System.EventHandler(this.frmGroupView_ShowFilterClicked);
            this.Activated += new System.EventHandler(this.frmGroupView_Activated);
            this.Load += new System.EventHandler(this.frmGroupView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroupView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroupView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectedGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UIControls.ucToolBar ucAssetGroups;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.TreeList trlGroup;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolGroupID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolAssetGroupId;
        private DevExpress.Utils.ImageCollection imageSmall;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.SimpleLabelItem lblSelectedGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.GridControl gcGroupView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGroupView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colgroup;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetName;
        private DevExpress.XtraGrid.Columns.GridColumn colCatogry;
        private DevExpress.XtraGrid.Columns.GridColumn colnUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colnQuantity;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LabelControl lblCountNumber;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}