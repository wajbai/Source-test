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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.trlAssetClass = new DevExpress.XtraTreeList.TreeList();
            this.trlcolAssetClassId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trlcolName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
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
            this.gcAssetClassView = new DevExpress.XtraGrid.GridControl();
            this.gvAssetClassView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colParentClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCatogry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblSelectedGroup = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucAssetClass = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlAssetClass)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetClassView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetClassView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectedGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.trlAssetClass);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.SplitterPosition = 420;
            // 
            // trlAssetClass
            // 
            this.trlAssetClass.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("trlAssetClass.Appearance.FocusedCell.Font")));
            this.trlAssetClass.Appearance.FocusedCell.Options.UseFont = true;
            this.trlAssetClass.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("trlAssetClass.Appearance.HeaderPanel.Font")));
            this.trlAssetClass.Appearance.HeaderPanel.Options.UseFont = true;
            this.trlAssetClass.Appearance.SelectedRow.Font = ((System.Drawing.Font)(resources.GetObject("trlAssetClass.Appearance.SelectedRow.Font")));
            this.trlAssetClass.Appearance.SelectedRow.Options.UseFont = true;
            this.trlAssetClass.AppearancePrint.Row.Font = ((System.Drawing.Font)(resources.GetObject("trlAssetClass.AppearancePrint.Row.Font")));
            this.trlAssetClass.AppearancePrint.Row.Options.UseFont = true;
            this.trlAssetClass.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.trlcolAssetClassId,
            this.trlcolName});
            resources.ApplyResources(this.trlAssetClass, "trlAssetClass");
            this.trlAssetClass.ImageIndexFieldName = "IMAGE_ID";
            this.trlAssetClass.KeyFieldName = "ASSET_CLASS_ID";
            this.trlAssetClass.Name = "trlAssetClass";
            this.trlAssetClass.OptionsBehavior.Editable = false;
            this.trlAssetClass.OptionsPrint.PrintAllNodes = true;
            this.trlAssetClass.OptionsPrint.PrintFilledTreeIndent = true;
            this.trlAssetClass.OptionsPrint.PrintHorzLines = false;
            this.trlAssetClass.OptionsPrint.PrintImages = false;
            this.trlAssetClass.OptionsPrint.PrintReportFooter = false;
            this.trlAssetClass.OptionsPrint.PrintTreeButtons = false;
            this.trlAssetClass.OptionsPrint.PrintVertLines = false;
            this.trlAssetClass.OptionsView.ShowHorzLines = false;
            this.trlAssetClass.OptionsView.ShowVertLines = false;
            this.trlAssetClass.ParentFieldName = "PARENT_CLASS_ID";
            this.trlAssetClass.PreviewFieldName = "ASSET_CLASS";
            this.trlAssetClass.SelectImageList = this.imageSmall;
            this.trlAssetClass.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.trlAssetClass_FocusedNodeChanged);
            this.trlAssetClass.DoubleClick += new System.EventHandler(this.trlAssetClass_DoubleClick);
            // 
            // trlcolAssetClassId
            // 
            resources.ApplyResources(this.trlcolAssetClassId, "trlcolAssetClassId");
            this.trlcolAssetClassId.FieldName = "ASSETC_CLASS_ID";
            this.trlcolAssetClassId.Name = "trlcolAssetClassId";
            // 
            // trlcolName
            // 
            this.trlcolName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("trlcolName.AppearanceHeader.Font")));
            this.trlcolName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.trlcolName, "trlcolName");
            this.trlcolName.FieldName = "ASSET_CLASS";
            this.trlcolName.Name = "trlcolName";
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.layoutControl2);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.layoutControl3);
            this.layoutControl2.Controls.Add(this.gcAssetClassView);
            resources.ApplyResources(this.layoutControl2, "layoutControl2");
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(30, 233, 250, 350);
            this.layoutControl2.Root = this.layoutControlGroup2;
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.lblCountNumber);
            this.layoutControl3.Controls.Add(this.chkShowFilter);
            this.layoutControl3.Controls.Add(this.labelControl1);
            resources.ApplyResources(this.layoutControl3, "layoutControl3");
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(282, 317, 250, 350);
            this.layoutControl3.Root = this.Root;
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCountNumber.Appearance.Font")));
            resources.ApplyResources(this.lblCountNumber, "lblCountNumber");
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.StyleController = this.layoutControl3;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl3;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.StyleController = this.layoutControl3;
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // Root
            // 
            resources.ApplyResources(this.Root, "Root");
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
            this.Root.Size = new System.Drawing.Size(511, 23);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(107, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.labelControl1;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(470, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(13, 23);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lblCountNumber;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(483, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(11, 23);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(107, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(363, 23);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(494, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(17, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gcAssetClassView
            // 
            resources.ApplyResources(this.gcAssetClassView, "gcAssetClassView");
            this.gcAssetClassView.MainView = this.gvAssetClassView;
            this.gcAssetClassView.Name = "gcAssetClassView";
            this.gcAssetClassView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetClassView});
            // 
            // gvAssetClassView
            // 
            this.gvAssetClassView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetClassView.Appearance.FocusedRow.Font")));
            this.gvAssetClassView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetClassView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetClassView.Appearance.HeaderPanel.Font")));
            this.gvAssetClassView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetClassView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colParentClass,
            this.colAssetClass,
            this.colAssetName,
            this.colCatogry,
            this.colnUnit,
            this.colnQuantity});
            this.gvAssetClassView.GridControl = this.gcAssetClassView;
            this.gvAssetClassView.Name = "gvAssetClassView";
            this.gvAssetClassView.OptionsBehavior.Editable = false;
            this.gvAssetClassView.OptionsView.ShowGroupPanel = false;
            this.gvAssetClassView.DoubleClick += new System.EventHandler(this.gvAssetClassView_DoubleClick);
            this.gvAssetClassView.RowCountChanged += new System.EventHandler(this.gvAssetClassView_RowCountChanged);
            // 
            // colParentClass
            // 
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
            // colAssetName
            // 
            this.colAssetName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAssetName.AppearanceHeader.Font")));
            this.colAssetName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAssetName, "colAssetName");
            this.colAssetName.FieldName = "ASSET_ITEM";
            this.colAssetName.Name = "colAssetName";
            this.colAssetName.OptionsColumn.AllowEdit = false;
            this.colAssetName.OptionsColumn.AllowFocus = false;
            // 
            // colCatogry
            // 
            resources.ApplyResources(this.colCatogry, "colCatogry");
            this.colCatogry.FieldName = "CATOGRY";
            this.colCatogry.Name = "colCatogry";
            this.colCatogry.OptionsColumn.AllowEdit = false;
            this.colCatogry.OptionsColumn.AllowFocus = false;
            // 
            // colnUnit
            // 
            resources.ApplyResources(this.colnUnit, "colnUnit");
            this.colnUnit.FieldName = "UNIT";
            this.colnUnit.Name = "colnUnit";
            this.colnUnit.OptionsColumn.AllowEdit = false;
            this.colnUnit.OptionsColumn.AllowFocus = false;
            // 
            // colnQuantity
            // 
            resources.ApplyResources(this.colnQuantity, "colnQuantity");
            this.colnQuantity.FieldName = "QUNATITY";
            this.colnQuantity.Name = "colnQuantity";
            this.colnQuantity.OptionsColumn.AllowEdit = false;
            this.colnQuantity.OptionsColumn.AllowFocus = false;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblSelectedGroup,
            this.layoutControlItem1,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(515, 348);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // lblSelectedGroup
            // 
            this.lblSelectedGroup.AllowHotTrack = false;
            this.lblSelectedGroup.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblSelectedGroup.AppearanceItemCaption.Font")));
            this.lblSelectedGroup.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblSelectedGroup, "lblSelectedGroup");
            this.lblSelectedGroup.Location = new System.Drawing.Point(0, 0);
            this.lblSelectedGroup.Name = "lblSelectedGroup";
            this.lblSelectedGroup.Size = new System.Drawing.Size(515, 24);
            this.lblSelectedGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblSelectedGroup.TextSize = new System.Drawing.Size(50, 20);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAssetClassView;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(515, 297);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.layoutControl3;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 321);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(515, 27);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Controls.Add(this.ucAssetClass);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(238, 186, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucAssetClass
            // 
            this.ucAssetClass.ChangeAddCaption = "&Add";
            this.ucAssetClass.ChangeCaption = "&Edit";
            this.ucAssetClass.ChangeDeleteCaption = "&Delete";
            this.ucAssetClass.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucAssetClass.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucAssetClass.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAssetClass.ChangePostInterestCaption = "P&ost Interest";
            this.ucAssetClass.ChangePrintCaption = "&Print";
            this.ucAssetClass.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucAssetClass.DisableAddButton = true;
            this.ucAssetClass.DisableAMCRenew = true;
            this.ucAssetClass.DisableCloseButton = true;
            this.ucAssetClass.DisableDeleteButton = true;
            this.ucAssetClass.DisableDownloadExcel = true;
            this.ucAssetClass.DisableEditButton = true;
            this.ucAssetClass.DisableInsertVoucher = true;
            this.ucAssetClass.DisableMoveTransaction = true;
            this.ucAssetClass.DisableNatureofPayments = true;
            this.ucAssetClass.DisablePostInterest = true;
            this.ucAssetClass.DisablePrintButton = true;
            this.ucAssetClass.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucAssetClass, "ucAssetClass");
            this.ucAssetClass.Name = "ucAssetClass";
            this.ucAssetClass.ShowHTML = true;
            this.ucAssetClass.ShowMMT = true;
            this.ucAssetClass.ShowPDF = true;
            this.ucAssetClass.ShowRTF = true;
            this.ucAssetClass.ShowText = true;
            this.ucAssetClass.ShowXLS = true;
            this.ucAssetClass.ShowXLSX = true;
            this.ucAssetClass.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetClass.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetClass.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetClass.AddClicked += new System.EventHandler(this.ucAssetGroups_AddClicked);
            this.ucAssetClass.EditClicked += new System.EventHandler(this.ucAssetGroups_EditClicked);
            this.ucAssetClass.DeleteClicked += new System.EventHandler(this.ucAssetGroups_DeleteClicked);
            this.ucAssetClass.PrintClicked += new System.EventHandler(this.ucAssetGroups_PrintClicked);
            this.ucAssetClass.CloseClicked += new System.EventHandler(this.ucAssetGroups_CloseClicked);
            this.ucAssetClass.RefreshClicked += new System.EventHandler(this.ucAssetGroups_RefreshClicked);
            this.ucAssetClass.DownloadExcel += new System.EventHandler(this.ucAssetGroups_DownloadExcel);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(948, 387);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucAssetClass;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(196, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(948, 31);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.splitContainerControl1;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(948, 356);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // colGroupName
            // 
            resources.ApplyResources(this.colGroupName, "colGroupName");
            this.colGroupName.FieldName = "GROUP_NAME";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsColumn.AllowEdit = false;
            this.colGroupName.OptionsColumn.AllowFocus = false;
            this.colGroupName.OptionsColumn.ReadOnly = true;
            this.colGroupName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colNAME
            // 
            resources.ApplyResources(this.colNAME, "colNAME");
            this.colNAME.FieldName = "NAME";
            this.colNAME.Name = "colNAME";
            this.colNAME.OptionsColumn.AllowEdit = false;
            this.colNAME.OptionsColumn.AllowFocus = false;
            this.colNAME.OptionsColumn.ReadOnly = true;
            this.colNAME.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCategory
            // 
            resources.ApplyResources(this.colCategory, "colCategory");
            this.colCategory.FieldName = "CATEGORY";
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.AllowEdit = false;
            this.colCategory.OptionsColumn.AllowFocus = false;
            this.colCategory.OptionsColumn.ReadOnly = true;
            // 
            // colUnit
            // 
            resources.ApplyResources(this.colUnit, "colUnit");
            this.colUnit.FieldName = "UNIT";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.AllowFocus = false;
            this.colUnit.OptionsColumn.ReadOnly = true;
            // 
            // colQuantity
            // 
            resources.ApplyResources(this.colQuantity, "colQuantity");
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.OptionsColumn.AllowFocus = false;
            this.colQuantity.OptionsColumn.ReadOnly = true;
            this.colQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colQuantity.Summary"))))});
            // 
            // frmAssetClassView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetClassView";
            this.ShowFilterClicked += new System.EventHandler(this.frmGroupView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmAssetClassView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmGroupView_Activated);
            this.Load += new System.EventHandler(this.frmGroupView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlAssetClass)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetClassView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetClassView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectedGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar ucAssetClass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.TreeList trlAssetClass;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolAssetClassId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolName;
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
        private DevExpress.XtraGrid.GridControl gcAssetClassView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetClassView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClass;
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
        private DevExpress.XtraGrid.Columns.GridColumn colParentClass;
    }
}