namespace ACPP.Modules.User_Management
{
    partial class frmUserManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserManagement));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lcUserManagement = new DevExpress.XtraLayout.LayoutControl();
            this.trListUserManagement = new DevExpress.XtraTreeList.TreeList();
            this.colActivityId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colObjectName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colObjectType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colObectSubType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcProject = new DevExpress.XtraGrid.GridControl();
            this.gvProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvColSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repchkSelectAll = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gvColProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpUserRoles = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExpandAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnCollapseAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lcgUserManagement = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.chkSelectAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUserManagement)).BeginInit();
            this.lcUserManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trListUserManagement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repchkSelectAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpUserRoles.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgUserManagement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lcUserManagement
            // 
            this.lcUserManagement.AllowCustomizationMenu = false;
            this.lcUserManagement.Controls.Add(this.trListUserManagement);
            this.lcUserManagement.Controls.Add(this.chkShowFilter);
            this.lcUserManagement.Controls.Add(this.gcProject);
            this.lcUserManagement.Controls.Add(this.glkpUserRoles);
            this.lcUserManagement.Controls.Add(this.btnExpandAll);
            this.lcUserManagement.Controls.Add(this.btnCollapseAll);
            this.lcUserManagement.Controls.Add(this.btnSave);
            this.lcUserManagement.Controls.Add(this.btnClose);
            resources.ApplyResources(this.lcUserManagement, "lcUserManagement");
            this.lcUserManagement.Name = "lcUserManagement";
            this.lcUserManagement.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(403, 215, 339, 350);
            this.lcUserManagement.OptionsView.UseDefaultDragAndDropRendering = false;
            this.lcUserManagement.Root = this.lcgUserManagement;
            // 
            // trListUserManagement
            // 
            this.trListUserManagement.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("trListUserManagement.Appearance.FocusedRow.Font")));
            this.trListUserManagement.Appearance.FocusedRow.Options.UseFont = true;
            this.trListUserManagement.AppearancePrint.HeaderPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("trListUserManagement.AppearancePrint.HeaderPanel.ForeColor")));
            this.trListUserManagement.AppearancePrint.HeaderPanel.Options.UseForeColor = true;
            this.trListUserManagement.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colActivityId,
            this.colParentId,
            this.colObjectName,
            this.colObjectType,
            this.colObectSubType});
            this.trListUserManagement.KeyFieldName = "ACTIVITY_ID";
            resources.ApplyResources(this.trListUserManagement, "trListUserManagement");
            this.trListUserManagement.Name = "trListUserManagement";
            this.trListUserManagement.BeginUnboundLoad();
            this.trListUserManagement.AppendNode(new object[] {
            null,
            0,
            null,
            null,
            null}, -1);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 0);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 1);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 2);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            0,
            null,
            null,
            null}, -1);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 4);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 5);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 6);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            0,
            null,
            null,
            null}, -1);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 8);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 9);
            this.trListUserManagement.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 10);
            this.trListUserManagement.EndUnboundLoad();
            this.trListUserManagement.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.trListUserManagement.OptionsBehavior.Editable = false;
            this.trListUserManagement.OptionsBehavior.PopulateServiceColumns = true;
            this.trListUserManagement.OptionsView.ShowCheckBoxes = true;
            this.trListUserManagement.OptionsView.ShowIndicator = false;
            this.trListUserManagement.ParentFieldName = "PARENT_ID";
            this.trListUserManagement.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.trListUserManagement_NodeCellStyle);
            // 
            // colActivityId
            // 
            resources.ApplyResources(this.colActivityId, "colActivityId");
            this.colActivityId.FieldName = "ACTIVITY_ID";
            this.colActivityId.Name = "colActivityId";
            // 
            // colParentId
            // 
            resources.ApplyResources(this.colParentId, "colParentId");
            this.colParentId.FieldName = "PARENT_ID";
            this.colParentId.Name = "colParentId";
            // 
            // colObjectName
            // 
            this.colObjectName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colObjectName.AppearanceHeader.Font")));
            this.colObjectName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colObjectName, "colObjectName");
            this.colObjectName.FieldName = "OBJECT_NAME";
            this.colObjectName.Name = "colObjectName";
            // 
            // colObjectType
            // 
            resources.ApplyResources(this.colObjectType, "colObjectType");
            this.colObjectType.FieldName = "OBJECT_TYPE";
            this.colObjectType.Name = "colObjectType";
            // 
            // colObectSubType
            // 
            resources.ApplyResources(this.colObectSubType, "colObectSubType");
            this.colObectSubType.FieldName = "OBJECT_SUB_TYPE";
            this.colObectSubType.Name = "colObectSubType";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.lcUserManagement;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcProject
            // 
            resources.ApplyResources(this.gcProject, "gcProject");
            this.gcProject.MainView = this.gvProject;
            this.gcProject.Name = "gcProject";
            this.gcProject.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repchkSelectAll});
            this.gcProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProject});
            // 
            // gvProject
            // 
            this.gvProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvColSelect,
            this.gvColProjectId,
            this.gvColProjectName});
            this.gvProject.GridControl = this.gcProject;
            this.gvProject.Name = "gvProject";
            this.gvProject.OptionsView.ShowGroupPanel = false;
            this.gvProject.OptionsView.ShowIndicator = false;
            this.gvProject.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvProject_CustomRowCellEdit);
            // 
            // gvColSelect
            // 
            resources.ApplyResources(this.gvColSelect, "gvColSelect");
            this.gvColSelect.ColumnEdit = this.repchkSelectAll;
            this.gvColSelect.FieldName = "SELECT_COL";
            this.gvColSelect.Name = "gvColSelect";
            this.gvColSelect.OptionsColumn.ShowCaption = false;
            this.gvColSelect.OptionsFilter.AllowAutoFilter = false;
            this.gvColSelect.OptionsFilter.AllowFilter = false;
            this.gvColSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // repchkSelectAll
            // 
            resources.ApplyResources(this.repchkSelectAll, "repchkSelectAll");
            this.repchkSelectAll.Name = "repchkSelectAll";
            this.repchkSelectAll.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repchkSelectAll.ValueChecked = 1;
            this.repchkSelectAll.ValueGrayed = 2;
            this.repchkSelectAll.ValueUnchecked = 0;
            // 
            // gvColProjectId
            // 
            resources.ApplyResources(this.gvColProjectId, "gvColProjectId");
            this.gvColProjectId.FieldName = "PROJECT_ID";
            this.gvColProjectId.Name = "gvColProjectId";
            // 
            // gvColProjectName
            // 
            this.gvColProjectName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColProjectName.AppearanceHeader.Font")));
            this.gvColProjectName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColProjectName, "gvColProjectName");
            this.gvColProjectName.FieldName = "PROJECT";
            this.gvColProjectName.Name = "gvColProjectName";
            this.gvColProjectName.OptionsColumn.AllowEdit = false;
            this.gvColProjectName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // glkpUserRoles
            // 
            resources.ApplyResources(this.glkpUserRoles, "glkpUserRoles");
            this.glkpUserRoles.Name = "glkpUserRoles";
            this.glkpUserRoles.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpUserRoles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpUserRoles.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpUserRoles.Properties.Buttons1"))), resources.GetString("glkpUserRoles.Properties.Buttons2"), ((int)(resources.GetObject("glkpUserRoles.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpUserRoles.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpUserRoles.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpUserRoles.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpUserRoles.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpUserRoles.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("glkpUserRoles.Properties.Buttons9"), ((object)(resources.GetObject("glkpUserRoles.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpUserRoles.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpUserRoles.Properties.Buttons12"))))});
            this.glkpUserRoles.Properties.NullText = resources.GetString("glkpUserRoles.Properties.NullText");
            this.glkpUserRoles.Properties.PopupFormMinSize = new System.Drawing.Size(162, 0);
            this.glkpUserRoles.Properties.PopupFormSize = new System.Drawing.Size(162, 0);
            this.glkpUserRoles.Properties.View = this.gridLookUpEdit1View;
            this.glkpUserRoles.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpUserRoles_Properties_ButtonClick);
            this.glkpUserRoles.StyleController = this.lcUserManagement;
            this.glkpUserRoles.EditValueChanged += new System.EventHandler(this.glkpUserRoles_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.HeaderPanel.Font")));
            this.gridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserRoleId,
            this.colUserRole});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colUserRoleId
            // 
            resources.ApplyResources(this.colUserRoleId, "colUserRoleId");
            this.colUserRoleId.FieldName = "USERROLE_ID";
            this.colUserRoleId.Name = "colUserRoleId";
            this.colUserRoleId.OptionsColumn.ShowCaption = false;
            // 
            // colUserRole
            // 
            resources.ApplyResources(this.colUserRole, "colUserRole");
            this.colUserRole.FieldName = "USERROLE";
            this.colUserRole.Name = "colUserRole";
            this.colUserRole.OptionsColumn.ShowCaption = false;
            // 
            // btnExpandAll
            // 
            resources.ApplyResources(this.btnExpandAll, "btnExpandAll");
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.StyleController = this.lcUserManagement;
            this.btnExpandAll.Click += new System.EventHandler(this.btnExpandAll_Click);
            // 
            // btnCollapseAll
            // 
            resources.ApplyResources(this.btnCollapseAll, "btnCollapseAll");
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.StyleController = this.lcUserManagement;
            this.btnCollapseAll.Click += new System.EventHandler(this.btnCollapseAll_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcUserManagement;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcUserManagement;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lcgUserManagement
            // 
            resources.ApplyResources(this.lcgUserManagement, "lcgUserManagement");
            this.lcgUserManagement.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgUserManagement.GroupBordersVisible = false;
            this.lcgUserManagement.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem3,
            this.layoutControlGroup1,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.lcgUserManagement.Location = new System.Drawing.Point(0, 0);
            this.lcgUserManagement.Name = "Root";
            this.lcgUserManagement.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgUserManagement.Size = new System.Drawing.Size(838, 428);
            this.lcgUserManagement.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(237, 402);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(465, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(770, 402);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(68, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(702, 402);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(68, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(251, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(433, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnCollapseAll;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(761, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(77, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnExpandAll;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(684, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(77, 26);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem3.AppearanceItemCaption.Font")));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.glkpUserRoles;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(251, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(81, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup1.AppearanceGroup.Font")));
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(255, 376);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcProject;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(245, 347);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 402);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(237, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.trListUserManagement;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(255, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(583, 376);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(239, 380);
            this.emptySpaceItem3.Name = "emptySpaceItem1";
            this.emptySpaceItem3.Size = new System.Drawing.Size(317, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(239, 380);
            this.emptySpaceItem4.Name = "emptySpaceItem1";
            this.emptySpaceItem4.Size = new System.Drawing.Size(317, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // chkSelectAll
            // 
            resources.ApplyResources(this.chkSelectAll, "chkSelectAll");
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Properties.Caption = resources.GetString("chkSelectAll.Properties.Caption");
            this.chkSelectAll.StyleController = this.lcUserManagement;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // frmUserManagement
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.lcUserManagement);
            this.Name = "frmUserManagement";
            this.ShowFilterClicked += new System.EventHandler(this.frmUserManagement_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmUserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcUserManagement)).EndInit();
            this.lcUserManagement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trListUserManagement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repchkSelectAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpUserRoles.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgUserManagement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcUserManagement;
        private DevExpress.XtraLayout.LayoutControlGroup lcgUserManagement;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnExpandAll;
        private DevExpress.XtraEditors.SimpleButton btnCollapseAll;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.GridLookUpEdit glkpUserRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colUserRoleId;
        private DevExpress.XtraGrid.Columns.GridColumn colUserRole;
        private DevExpress.XtraGrid.GridControl gcProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProject;
        private DevExpress.XtraGrid.Columns.GridColumn gvColProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn gvColProjectName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn gvColSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repchkSelectAll;
        private DevExpress.XtraEditors.CheckEdit chkSelectAll;
        private DevExpress.XtraTreeList.TreeList trListUserManagement;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colActivityId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colObjectName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colObjectType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colObectSubType;
    }
}