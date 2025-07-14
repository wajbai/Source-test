namespace PAYROLL.Modules.Payroll_app
{
    partial class frmStaffOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStaffOrder));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnGenerateOrder = new DevExpress.XtraEditors.SimpleButton();
            this.gcGroupMembers = new DevExpress.XtraGrid.GridControl();
            this.gvGroupMembers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStaffId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStaffCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKnownAs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldateofjoin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAge = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSortOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveUp = new DevExpress.XtraEditors.SimpleButton();
            this.glkpGroup = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroupMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroupMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnGenerateOrder);
            this.layoutControl1.Controls.Add(this.gcGroupMembers);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnMoveDown);
            this.layoutControl1.Controls.Add(this.btnMoveUp);
            this.layoutControl1.Controls.Add(this.glkpGroup);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(683, 186, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnGenerateOrder
            // 
            this.btnGenerateOrder.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnGenerateOrder, "btnGenerateOrder");
            this.btnGenerateOrder.Name = "btnGenerateOrder";
            this.btnGenerateOrder.StyleController = this.layoutControl1;
            this.btnGenerateOrder.Click += new System.EventHandler(this.btnGenerateOrder_Click);
            // 
            // gcGroupMembers
            // 
            resources.ApplyResources(this.gcGroupMembers, "gcGroupMembers");
            this.gcGroupMembers.MainView = this.gvGroupMembers;
            this.gcGroupMembers.Name = "gcGroupMembers";
            this.gcGroupMembers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGroupMembers});
            // 
            // gvGroupMembers
            // 
            this.gvGroupMembers.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvGroupMembers.Appearance.FocusedRow.Font")));
            this.gvGroupMembers.Appearance.FocusedRow.Options.UseFont = true;
            this.gvGroupMembers.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvGroupMembers.Appearance.HeaderPanel.Font")));
            this.gvGroupMembers.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvGroupMembers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStaffId,
            this.colStaffCode,
            this.colName,
            this.colKnownAs,
            this.coldateofjoin,
            this.colAge,
            this.colSortOrder,
            this.colGroup});
            this.gvGroupMembers.GridControl = this.gcGroupMembers;
            this.gvGroupMembers.Name = "gvGroupMembers";
            this.gvGroupMembers.OptionsFind.AllowFindPanel = false;
            this.gvGroupMembers.OptionsFind.ShowClearButton = false;
            this.gvGroupMembers.OptionsFind.ShowCloseButton = false;
            this.gvGroupMembers.OptionsFind.ShowFindButton = false;
            this.gvGroupMembers.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvGroupMembers.OptionsView.ShowGroupPanel = false;
            this.gvGroupMembers.OptionsView.ShowIndicator = false;
            this.gvGroupMembers.EndSorting += new System.EventHandler(this.gvGroupMembers_EndSorting);
            this.gvGroupMembers.RowCountChanged += new System.EventHandler(this.gvGroupMembers_RowCountChanged);
            // 
            // colStaffId
            // 
            resources.ApplyResources(this.colStaffId, "colStaffId");
            this.colStaffId.FieldName = "STAFF ID";
            this.colStaffId.Name = "colStaffId";
            // 
            // colStaffCode
            // 
            resources.ApplyResources(this.colStaffCode, "colStaffCode");
            this.colStaffCode.FieldName = "STAFF_CODE";
            this.colStaffCode.Name = "colStaffCode";
            this.colStaffCode.OptionsColumn.AllowEdit = false;
            this.colStaffCode.OptionsColumn.AllowFocus = false;
            this.colStaffCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colStaffCode.OptionsColumn.AllowIncrementalSearch = false;
            this.colStaffCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStaffCode.OptionsColumn.AllowMove = false;
            this.colStaffCode.OptionsColumn.AllowShowHide = false;
            this.colStaffCode.OptionsColumn.AllowSize = false;
            this.colStaffCode.OptionsColumn.FixedWidth = true;
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "Staff Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colName.OptionsColumn.AllowIncrementalSearch = false;
            this.colName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colName.OptionsColumn.AllowMove = false;
            this.colName.OptionsColumn.AllowShowHide = false;
            this.colName.OptionsColumn.AllowSize = false;
            this.colName.OptionsColumn.FixedWidth = true;
            this.colName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colKnownAs
            // 
            resources.ApplyResources(this.colKnownAs, "colKnownAs");
            this.colKnownAs.FieldName = "KNOWN_AS";
            this.colKnownAs.Name = "colKnownAs";
            this.colKnownAs.OptionsColumn.AllowEdit = false;
            this.colKnownAs.OptionsColumn.AllowFocus = false;
            this.colKnownAs.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colKnownAs.OptionsColumn.AllowIncrementalSearch = false;
            this.colKnownAs.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colKnownAs.OptionsColumn.AllowMove = false;
            this.colKnownAs.OptionsColumn.AllowShowHide = false;
            this.colKnownAs.OptionsColumn.AllowSize = false;
            // 
            // coldateofjoin
            // 
            resources.ApplyResources(this.coldateofjoin, "coldateofjoin");
            this.coldateofjoin.FieldName = "DATE_OF_JOIN";
            this.coldateofjoin.Name = "coldateofjoin";
            this.coldateofjoin.OptionsColumn.AllowEdit = false;
            this.coldateofjoin.OptionsColumn.AllowFocus = false;
            this.coldateofjoin.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.coldateofjoin.OptionsColumn.AllowIncrementalSearch = false;
            this.coldateofjoin.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.coldateofjoin.OptionsColumn.AllowMove = false;
            this.coldateofjoin.OptionsColumn.AllowShowHide = false;
            this.coldateofjoin.OptionsColumn.AllowSize = false;
            // 
            // colAge
            // 
            resources.ApplyResources(this.colAge, "colAge");
            this.colAge.FieldName = "Age";
            this.colAge.Name = "colAge";
            this.colAge.OptionsColumn.AllowEdit = false;
            this.colAge.OptionsColumn.AllowFocus = false;
            this.colAge.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAge.OptionsColumn.AllowIncrementalSearch = false;
            this.colAge.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAge.OptionsColumn.AllowMove = false;
            this.colAge.OptionsColumn.AllowShowHide = false;
            this.colAge.OptionsColumn.AllowSize = false;
            // 
            // colSortOrder
            // 
            resources.ApplyResources(this.colSortOrder, "colSortOrder");
            this.colSortOrder.FieldName = "STAFFORDER";
            this.colSortOrder.Name = "colSortOrder";
            this.colSortOrder.OptionsColumn.AllowEdit = false;
            this.colSortOrder.OptionsColumn.AllowFocus = false;
            this.colSortOrder.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colSortOrder.OptionsColumn.AllowIncrementalSearch = false;
            this.colSortOrder.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colSortOrder.OptionsColumn.AllowMove = false;
            this.colSortOrder.OptionsColumn.AllowShowHide = false;
            this.colSortOrder.OptionsColumn.AllowSize = false;
            // 
            // colGroup
            // 
            resources.ApplyResources(this.colGroup, "colGroup");
            this.colGroup.FieldName = "GROUP";
            this.colGroup.Name = "colGroup";
            this.colGroup.OptionsColumn.AllowEdit = false;
            this.colGroup.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colGroup.OptionsColumn.AllowIncrementalSearch = false;
            this.colGroup.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colGroup.OptionsColumn.AllowMove = false;
            this.colGroup.OptionsColumn.AllowShowHide = false;
            this.colGroup.OptionsColumn.AllowSize = false;
            this.colGroup.OptionsColumn.FixedWidth = true;
            this.colGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
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
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Image = global::PAYROLL.Properties.Resources.arrow__down;
            resources.ApplyResources(this.btnMoveDown, "btnMoveDown");
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.StyleController = this.layoutControl1;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = global::PAYROLL.Properties.Resources.arrow_up;
            resources.ApplyResources(this.btnMoveUp, "btnMoveUp");
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.StyleController = this.layoutControl1;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // glkpGroup
            // 
            resources.ApplyResources(this.glkpGroup, "glkpGroup");
            this.glkpGroup.Name = "glkpGroup";
            this.glkpGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpGroup.Properties.Buttons"))))});
            this.glkpGroup.Properties.NullText = resources.GetString("glkpGroup.Properties.NullText");
            this.glkpGroup.Properties.PopupFormMinSize = new System.Drawing.Size(223, 0);
            this.glkpGroup.Properties.PopupFormSize = new System.Drawing.Size(223, 0);
            this.glkpGroup.Properties.View = this.gridLookUpEdit1View;
            this.glkpGroup.StyleController = this.layoutControl1;
            this.glkpGroup.EditValueChanged += new System.EventHandler(this.glkpGroup_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGroupId,
            this.colGroupName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colGroupId
            // 
            resources.ApplyResources(this.colGroupId, "colGroupId");
            this.colGroupId.FieldName = "GROUP ID";
            this.colGroupId.Name = "colGroupId";
            // 
            // colGroupName
            // 
            resources.ApplyResources(this.colGroupName, "colGroupName");
            this.colGroupName.FieldName = "Group Name";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(38, 353);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(703, 23);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(703, 23);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(703, 23);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.simpleLabelItem3,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.lblRecordCount,
            this.simpleLabelItem2,
            this.emptySpaceItem4,
            this.layoutControlItem8,
            this.emptySpaceItem5,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.emptySpaceItem6,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(683, 424);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem1.AppearanceItemCaption.Font")));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.glkpGroup;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(283, 30);
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(34, 13);
            this.layoutControlItem1.TextToControlDistance = 3;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(283, 0);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(251, 30);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem3.AppearanceItemCaption.Font")));
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem3, "simpleLabelItem3");
            this.simpleLabelItem3.Image = global::PAYROLL.Properties.Resources.info;
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 386);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(497, 28);
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(426, 24);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(497, 386);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(566, 386);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.AppearanceItemCaption.Font")));
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(615, 363);
            this.lblRecordCount.MaxSize = new System.Drawing.Size(20, 23);
            this.lblRecordCount.MinSize = new System.Drawing.Size(20, 23);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(20, 23);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font")));
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(602, 363);
            this.simpleLabelItem2.MaxSize = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 363);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(602, 23);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gcGroupMembers;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(635, 333);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(635, 0);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(38, 167);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(38, 167);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(38, 167);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnMoveDown;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(635, 201);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(38, 34);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnMoveUp;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(635, 167);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(38, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem6, "emptySpaceItem6");
            this.emptySpaceItem6.Location = new System.Drawing.Point(635, 235);
            this.emptySpaceItem6.MaxSize = new System.Drawing.Size(38, 179);
            this.emptySpaceItem6.MinSize = new System.Drawing.Size(38, 179);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(38, 179);
            this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnGenerateOrder;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(534, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(90, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(101, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmStaffOrder
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStaffOrder";
            this.Load += new System.EventHandler(this.frmOrderComponents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcGroupMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroupMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.GridLookUpEdit glkpGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnMoveDown;
        private DevExpress.XtraEditors.SimpleButton btnMoveUp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraGrid.GridControl gcGroupMembers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGroupMembers;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colStaffId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn colStaffCode;
        private DevExpress.XtraGrid.Columns.GridColumn colKnownAs;
        private DevExpress.XtraGrid.Columns.GridColumn colAge;
        private DevExpress.XtraGrid.Columns.GridColumn colSortOrder;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraEditors.SimpleButton btnGenerateOrder;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn coldateofjoin;
    }
}