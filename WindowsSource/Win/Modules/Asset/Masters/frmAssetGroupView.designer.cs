namespace ACPP.Modules.Asset.Masters
{
    partial class frmAssetGroupView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetGroupView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.trlGroup = new DevExpress.XtraTreeList.TreeList();
            this.trlcolGroupID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trlcolName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.trlcolAssetGroupId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gcGroupView = new DevExpress.XtraGrid.GridControl();
            this.gvGroupView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colgroup_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.ucAssetGroups = new ACPP.Modules.UIControls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroupView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroupView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.ucAssetGroups);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(367, 313, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(745, 477);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 33);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.trlGroup);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcGroupView);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(741, 418);
            this.splitContainerControl1.SplitterPosition = 245;
            this.splitContainerControl1.TabIndex = 6;
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
            this.trlGroup.OptionsView.ShowHorzLines = false;
            this.trlGroup.OptionsView.ShowVertLines = false;
            this.trlGroup.ParentFieldName = "PARENT_GROUP_ID";
            this.trlGroup.SelectImageList = this.imageSmall;
            this.trlGroup.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.Default;
            this.trlGroup.Size = new System.Drawing.Size(245, 418);
            this.trlGroup.TabIndex = 0;
            this.trlGroup.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.trlGroup_FocusedNodeChanged);
            // 
            // trlcolGroupID
            // 
            this.trlcolGroupID.Caption = "treeListColumn1";
            this.trlcolGroupID.FieldName = "GROUP_ID";
            this.trlcolGroupID.Name = "trlcolGroupID";
            // 
            // trlcolName
            // 
            this.trlcolName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.trlcolName.AppearanceHeader.Options.UseFont = true;
            this.trlcolName.Caption = "Group Name";
            this.trlcolName.FieldName = "NAME";
            this.trlcolName.MinWidth = 33;
            this.trlcolName.Name = "trlcolName";
            this.trlcolName.Visible = true;
            this.trlcolName.VisibleIndex = 0;
            // 
            // trlcolAssetGroupId
            // 
            this.trlcolAssetGroupId.Caption = "treeListColumn3";
            this.trlcolAssetGroupId.FieldName = "PARENT_GROUP_ID";
            this.trlcolAssetGroupId.Name = "trlcolAssetGroupId";
            // 
            // gcGroupView
            // 
            this.gcGroupView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGroupView.Location = new System.Drawing.Point(0, 0);
            this.gcGroupView.MainView = this.gvGroupView;
            this.gcGroupView.Name = "gcGroupView";
            this.gcGroupView.Size = new System.Drawing.Size(491, 418);
            this.gcGroupView.TabIndex = 0;
            this.gcGroupView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGroupView});
            // 
            // gvGroupView
            // 
            this.gvGroupView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colgroup_Id,
            this.colNAME,
            this.colGroupName});
            this.gvGroupView.GridControl = this.gcGroupView;
            this.gvGroupView.Name = "gvGroupView";
            this.gvGroupView.OptionsBehavior.Editable = false;
            this.gvGroupView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvGroupView.OptionsView.ShowGroupPanel = false;
            this.gvGroupView.RowCountChanged += new System.EventHandler(this.gvGroupView_RowCountChanged);
            // 
            // colgroup_Id
            // 
            this.colgroup_Id.Caption = "groupId";
            this.colgroup_Id.FieldName = "ASSET_GROUP_ID";
            this.colgroup_Id.Name = "colgroup_Id";
            this.colgroup_Id.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colNAME
            // 
            this.colNAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNAME.AppearanceHeader.Options.UseFont = true;
            this.colNAME.Caption = "NAME";
            this.colNAME.FieldName = "NAME";
            this.colNAME.Name = "colNAME";
            this.colNAME.OptionsColumn.AllowEdit = false;
            this.colNAME.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 0;
            // 
            // colGroupName
            // 
            this.colGroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGroupName.AppearanceHeader.Options.UseFont = true;
            this.colGroupName.Caption = "Group Name";
            this.colGroupName.FieldName = "GROUP_NAME";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsColumn.AllowEdit = false;
            this.colGroupName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 1;
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 455);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(96, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 5;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // ucAssetGroups
            // 
            this.ucAssetGroups.ChangeAddCaption = "&Add";
            this.ucAssetGroups.ChangeCaption = "&Edit";
            this.ucAssetGroups.ChangeDeleteCaption = "&Delete";
            this.ucAssetGroups.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAssetGroups.ChangePrintCaption = "&Print";
            this.ucAssetGroups.DisableAddButton = true;
            this.ucAssetGroups.DisableCloseButton = true;
            this.ucAssetGroups.DisableDeleteButton = true;
            this.ucAssetGroups.DisableDownloadExcel = true;
            this.ucAssetGroups.DisableEditButton = true;
            this.ucAssetGroups.DisableMoveTransaction = true;
            this.ucAssetGroups.DisableNatureofPayments = true;
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
            this.ucAssetGroups.Size = new System.Drawing.Size(745, 31);
            this.ucAssetGroups.TabIndex = 4;
            this.ucAssetGroups.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetGroups.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetGroups.AddClicked += new System.EventHandler(this.ucAssetGroups_AddClicked);
            this.ucAssetGroups.EditClicked += new System.EventHandler(this.ucAssetGroups_EditClicked);
            this.ucAssetGroups.DeleteClicked += new System.EventHandler(this.ucAssetGroups_DeleteClicked);
            this.ucAssetGroups.PrintClicked += new System.EventHandler(this.ucAssetGroups_PrintClicked);
            this.ucAssetGroups.CloseClicked += new System.EventHandler(this.ucAssetGroups_CloseClicked);
            this.ucAssetGroups.RefreshClicked += new System.EventHandler(this.ucAssetGroups_RefreshClicked);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.lblCountNumber,
            this.lblCount,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(745, 477);
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
            this.layoutControlItem2.Size = new System.Drawing.Size(745, 31);
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
            this.layoutControlItem4.Size = new System.Drawing.Size(745, 422);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 453);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 24);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.AllowHotTrack = false;
            this.lblCountNumber.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountNumber.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountNumber.CustomizationFormText = "0";
            this.lblCountNumber.Location = new System.Drawing.Point(713, 453);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(32, 24);
            this.lblCountNumber.Text = "0";
            this.lblCountNumber.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCount.CustomizationFormText = "#";
            this.lblCount.Location = new System.Drawing.Point(699, 453);
            this.lblCount.Name = "lblCount";
            this.lblCount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCount.Size = new System.Drawing.Size(14, 24);
            this.lblCount.Text = "#";
            this.lblCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(100, 453);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(599, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // frmAssetGroupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 477);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetGroupView";
            this.Text = "Group";
            this.Load += new System.EventHandler(this.frmAssetGroupView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroupView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroupView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UIControls.ucToolBar ucAssetGroups;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountNumber;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.TreeList trlGroup;
        private DevExpress.XtraGrid.GridControl gcGroupView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGroupView;
        private DevExpress.XtraGrid.Columns.GridColumn colgroup_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolGroupID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn trlcolAssetGroupId;
        private DevExpress.Utils.ImageCollection imageSmall;
    }
}