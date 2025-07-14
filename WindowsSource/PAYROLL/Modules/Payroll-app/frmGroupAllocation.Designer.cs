namespace PAYROLL.Modules.Payroll_app
{
    partial class frmGroupAllocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroupAllocation));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcSelectGroup = new DevExpress.XtraGrid.GridControl();
            this.gvSelectGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGrpId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colgrpName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucGroupAllocation = new PAYROLL.UserControl.UcToolBar();
            this.lblFooter = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnAllocate = new DevExpress.XtraEditors.SimpleButton();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.gcAlloted = new DevExpress.XtraGrid.GridControl();
            this.gvAlloted = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStaffCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnDeleteStaff = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAlloted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlloted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDeleteStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcSelectGroup);
            this.layoutControl1.Controls.Add(this.ucGroupAllocation);
            this.layoutControl1.Controls.Add(this.lblFooter);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnAllocate);
            this.layoutControl1.Controls.Add(this.lblRecordCount);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnRemove);
            this.layoutControl1.Controls.Add(this.gcAlloted);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(616, 22, 309, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // gcSelectGroup
            // 
            resources.ApplyResources(this.gcSelectGroup, "gcSelectGroup");
            this.gcSelectGroup.MainView = this.gvSelectGroup;
            this.gcSelectGroup.Name = "gcSelectGroup";
            this.gcSelectGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSelectGroup});
            // 
            // gvSelectGroup
            // 
            this.gvSelectGroup.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvSelectGroup.Appearance.FocusedRow.Font")));
            this.gvSelectGroup.Appearance.FocusedRow.Options.UseFont = true;
            this.gvSelectGroup.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvSelectGroup.Appearance.HeaderPanel.Font")));
            this.gvSelectGroup.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvSelectGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGrpId,
            this.colgrpName});
            this.gvSelectGroup.GridControl = this.gcSelectGroup;
            this.gvSelectGroup.Name = "gvSelectGroup";
            this.gvSelectGroup.OptionsBehavior.Editable = false;
            this.gvSelectGroup.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvSelectGroup.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvSelectGroup.OptionsSelection.MultiSelect = true;
            this.gvSelectGroup.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvSelectGroup.OptionsView.ShowGroupPanel = false;
            this.gvSelectGroup.OptionsView.ShowIndicator = false;
            this.gvSelectGroup.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvSelectGroup_SelectionChanged);
            // 
            // colGrpId
            // 
            resources.ApplyResources(this.colGrpId, "colGrpId");
            this.colGrpId.FieldName = "GROUP ID";
            this.colGrpId.Name = "colGrpId";
            // 
            // colgrpName
            // 
            resources.ApplyResources(this.colgrpName, "colgrpName");
            this.colgrpName.FieldName = "Group Name";
            this.colgrpName.Name = "colgrpName";
            this.colgrpName.OptionsColumn.FixedWidth = true;
            // 
            // ucGroupAllocation
            // 
            this.ucGroupAllocation.ChangeAddCaption = "<u>&A</u>llocate";
            this.ucGroupAllocation.DisableAddButton = true;
            this.ucGroupAllocation.DisableCloseButton = true;
            this.ucGroupAllocation.DisableDeleteButton = true;
            this.ucGroupAllocation.DisableEditButton = true;
            this.ucGroupAllocation.DisableImportButton = true;
            this.ucGroupAllocation.DisablePrintButton = true;
            this.ucGroupAllocation.DisableRefreshButton = true;
            resources.ApplyResources(this.ucGroupAllocation, "ucGroupAllocation");
            this.ucGroupAllocation.Name = "ucGroupAllocation";
            this.ucGroupAllocation.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucGroupAllocation.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucGroupAllocation.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucGroupAllocation.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucGroupAllocation.VisibleImport = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucGroupAllocation.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucGroupAllocation.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucGroupAllocation.AddClicked += new System.EventHandler(this.ucGroupAllocation_AddClicked);
            this.ucGroupAllocation.CloseClicked += new System.EventHandler(this.ucGroupAllocation_CloseClicked);
            this.ucGroupAllocation.RefreshClicked += new System.EventHandler(this.ucGroupAllocation_RefreshClicked);
            // 
            // lblFooter
            // 
            this.lblFooter.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblFooter.Appearance.Font")));
            resources.ApplyResources(this.lblFooter, "lblFooter");
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.StyleController = this.layoutControl1;
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
            // btnAllocate
            // 
            this.btnAllocate.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnAllocate, "btnAllocate");
            this.btnAllocate.Name = "btnAllocate";
            this.btnAllocate.StyleController = this.layoutControl1;
            this.btnAllocate.Click += new System.EventHandler(this.btnAllocate_Click);
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.StyleController = this.layoutControl1;
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.StyleController = this.layoutControl1;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // gcAlloted
            // 
            resources.ApplyResources(this.gcAlloted, "gcAlloted");
            this.gcAlloted.MainView = this.gvAlloted;
            this.gcAlloted.Name = "gcAlloted";
            this.gcAlloted.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rbtnDeleteStaff});
            this.gcAlloted.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAlloted});
            // 
            // gvAlloted
            // 
            this.gvAlloted.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAlloted.Appearance.FocusedRow.Font")));
            this.gvAlloted.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAlloted.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAlloted.Appearance.HeaderPanel.Font")));
            this.gvAlloted.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAlloted.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colGroupId,
            this.colGroup,
            this.colStaffCode,
            this.colName,
            this.colDepartment,
            this.colDelete});
            this.gvAlloted.GridControl = this.gcAlloted;
            this.gvAlloted.Name = "gvAlloted";
            this.gvAlloted.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAlloted.OptionsView.ShowGroupPanel = false;
            this.gvAlloted.OptionsView.ShowIndicator = false;
            this.gvAlloted.RowCountChanged += new System.EventHandler(this.gvAlloted_RowCountChanged);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "Staff ID";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            this.colId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colGroupId
            // 
            resources.ApplyResources(this.colGroupId, "colGroupId");
            this.colGroupId.FieldName = "Group ID";
            this.colGroupId.Name = "colGroupId";
            this.colGroupId.OptionsColumn.AllowEdit = false;
            this.colGroupId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colGroup
            // 
            resources.ApplyResources(this.colGroup, "colGroup");
            this.colGroup.FieldName = "Group";
            this.colGroup.Name = "colGroup";
            this.colGroup.OptionsColumn.AllowEdit = false;
            this.colGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStaffCode
            // 
            resources.ApplyResources(this.colStaffCode, "colStaffCode");
            this.colStaffCode.FieldName = "Staff Code";
            this.colStaffCode.Name = "colStaffCode";
            this.colStaffCode.OptionsColumn.AllowEdit = false;
            this.colStaffCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDepartment
            // 
            resources.ApplyResources(this.colDepartment, "colDepartment");
            this.colDepartment.FieldName = "Department";
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.OptionsColumn.AllowEdit = false;
            this.colDepartment.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDelete
            // 
            resources.ApplyResources(this.colDelete, "colDelete");
            this.colDelete.ColumnEdit = this.rbtnDeleteStaff;
            this.colDelete.FieldName = "ACTION";
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowMove = false;
            this.colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.OptionsColumn.TabStop = false;
            this.colDelete.OptionsFilter.AllowAutoFilter = false;
            this.colDelete.OptionsFilter.AllowFilter = false;
            this.colDelete.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtnDeleteStaff
            // 
            resources.ApplyResources(this.rbtnDeleteStaff, "rbtnDeleteStaff");
            this.rbtnDeleteStaff.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnDeleteStaff.Buttons"))), resources.GetString("rbtnDeleteStaff.Buttons1"), ((int)(resources.GetObject("rbtnDeleteStaff.Buttons2"))), ((bool)(resources.GetObject("rbtnDeleteStaff.Buttons3"))), ((bool)(resources.GetObject("rbtnDeleteStaff.Buttons4"))), ((bool)(resources.GetObject("rbtnDeleteStaff.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnDeleteStaff.Buttons6"))), global::PAYROLL.Properties.Resources.delete_icon, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbtnDeleteStaff.Buttons7"), ((object)(resources.GetObject("rbtnDeleteStaff.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnDeleteStaff.Buttons9"))), ((bool)(resources.GetObject("rbtnDeleteStaff.Buttons10"))))});
            this.rbtnDeleteStaff.Name = "rbtnDeleteStaff";
            this.rbtnDeleteStaff.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.rbtnDeleteStaff.Click += new System.EventHandler(this.rbtnDeleteStaff_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(462, 392);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnRemove;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(462, 382);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnAllocate;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(531, 382);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(600, 382);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlItem1,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(679, 376);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup3.AppearanceGroup.Font")));
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup3, "layoutControlGroup3");
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.layoutControlGroup3.Location = new System.Drawing.Point(173, 29);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlGroup3.Size = new System.Drawing.Size(508, 349);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcAlloted;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(110, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(504, 302);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 302);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(0, 23);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(79, 23);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(481, 23);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lblFooter;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(481, 302);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(0, 23);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(13, 23);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(13, 23);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lblRecordCount;
            resources.ApplyResources(this.layoutControlItem10, "layoutControlItem10");
            this.layoutControlItem10.Location = new System.Drawing.Point(494, 302);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(0, 23);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(10, 23);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(10, 23);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucGroupAllocation;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 29);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(1, 29);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(681, 29);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup2.AppearanceGroup.Font")));
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlGroup2.Size = new System.Drawing.Size(173, 349);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcSelectGroup;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(173, 329);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmGroupAllocation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGroupAllocation";
            this.ShowFilterClicked += new System.EventHandler(this.frmGroupAllocation_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmGroupAllocation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAlloted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlloted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDeleteStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcAlloted;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAlloted;
        private DevExpress.XtraEditors.SimpleButton btnAllocate;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colStaffCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartment;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl lblFooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnDeleteStaff;
        private UserControl.UcToolBar ucGroupAllocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.GridControl gcSelectGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSelectGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colGrpId;
        private DevExpress.XtraGrid.Columns.GridColumn colgrpName;
    }
}