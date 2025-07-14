namespace ACPP.Modules.Inventory
{
    partial class frmLocationsAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocationsAdd));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dtResponsibleToDate = new DevExpress.XtraEditors.DateEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rgLocationType = new DevExpress.XtraEditors.RadioGroup();
            this.dtResponsibleDate = new DevExpress.XtraEditors.DateEdit();
            this.glkpCustodian = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCustodianId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustodianName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.gklpBlock = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBlockId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBlock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblResponsibleDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.Custodian = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblBlock = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLocationType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblResponsibleToDate = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgLocationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCustodian.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gklpBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResponsibleDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Custodian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLocationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResponsibleToDate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dtResponsibleToDate);
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.rgLocationType);
            this.layoutControl1.Controls.Add(this.dtResponsibleDate);
            this.layoutControl1.Controls.Add(this.glkpCustodian);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.gklpBlock);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // dtResponsibleToDate
            // 
            resources.ApplyResources(this.dtResponsibleToDate, "dtResponsibleToDate");
            this.dtResponsibleToDate.EnterMoveNextControl = true;
            this.dtResponsibleToDate.Name = "dtResponsibleToDate";
            this.dtResponsibleToDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtResponsibleToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtResponsibleToDate.Properties.Buttons"))))});
            this.dtResponsibleToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtResponsibleToDate.Properties.CalendarTimeProperties.Buttons"))))});
            this.dtResponsibleToDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dtResponsibleToDate.Properties.Mask.MaskType")));
            this.dtResponsibleToDate.StyleController = this.layoutControl1;
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gvProject;
            this.glkpProject.Properties.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.glkpProject_Properties_CustomDisplayText);
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.Popup += new System.EventHandler(this.glkpProject_Popup);
            this.glkpProject.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.glkpProject_QueryPopUp);
            // 
            // gvProject
            // 
            this.gvProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectID,
            this.colProjectName});
            this.gvProject.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvProject.Name = "gvProject";
            this.gvProject.OptionsSelection.CheckBoxSelectorColumnWidth = 15;
            this.gvProject.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvProject.OptionsSelection.MultiSelect = true;
            this.gvProject.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvProject.OptionsView.ShowColumnHeaders = false;
            this.gvProject.OptionsView.ShowGroupPanel = false;
            this.gvProject.OptionsView.ShowIndicator = false;
            // 
            // colProjectID
            // 
            resources.ApplyResources(this.colProjectID, "colProjectID");
            this.colProjectID.FieldName = "PROJECT_ID";
            this.colProjectID.Name = "colProjectID";
            // 
            // colProjectName
            // 
            resources.ApplyResources(this.colProjectName, "colProjectName");
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowEdit = false;
            this.colProjectName.OptionsColumn.AllowFocus = false;
            // 
            // rgLocationType
            // 
            resources.ApplyResources(this.rgLocationType, "rgLocationType");
            this.rgLocationType.EnterMoveNextControl = true;
            this.rgLocationType.Name = "rgLocationType";
            this.rgLocationType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgLocationType.Properties.Columns = 2;
            this.rgLocationType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgLocationType.Properties.Items"))), resources.GetString("rgLocationType.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgLocationType.Properties.Items2"))), resources.GetString("rgLocationType.Properties.Items3"))});
            this.rgLocationType.StyleController = this.layoutControl1;
            // 
            // dtResponsibleDate
            // 
            resources.ApplyResources(this.dtResponsibleDate, "dtResponsibleDate");
            this.dtResponsibleDate.EnterMoveNextControl = true;
            this.dtResponsibleDate.Name = "dtResponsibleDate";
            this.dtResponsibleDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtResponsibleDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtResponsibleDate.Properties.Buttons"))))});
            this.dtResponsibleDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtResponsibleDate.Properties.CalendarTimeProperties.Buttons"))))});
            this.dtResponsibleDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dtResponsibleDate.Properties.Mask.MaskType")));
            this.dtResponsibleDate.StyleController = this.layoutControl1;
            // 
            // glkpCustodian
            // 
            this.glkpCustodian.EnterMoveNextControl = true;
            resources.ApplyResources(this.glkpCustodian, "glkpCustodian");
            this.glkpCustodian.Name = "glkpCustodian";
            this.glkpCustodian.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCustodian.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCustodian.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCustodian.Properties.Buttons1"))))});
            this.glkpCustodian.Properties.ImmediatePopup = true;
            this.glkpCustodian.Properties.NullText = resources.GetString("glkpCustodian.Properties.NullText");
            this.glkpCustodian.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpCustodian.Properties.PopupFormSize = new System.Drawing.Size(276, 0);
            this.glkpCustodian.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpCustodian.Properties.View = this.gridView1;
            this.glkpCustodian.StyleController = this.layoutControl1;
            this.glkpCustodian.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpCustodian_ButtonClick);
            this.glkpCustodian.Leave += new System.EventHandler(this.glkpCustodian_Leave);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCustodianId,
            this.colCustodianName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCustodianId, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colCustodianId
            // 
            resources.ApplyResources(this.colCustodianId, "colCustodianId");
            this.colCustodianId.FieldName = "CUSTODIAN_ID";
            this.colCustodianId.Name = "colCustodianId";
            // 
            // colCustodianName
            // 
            resources.ApplyResources(this.colCustodianName, "colCustodianName");
            this.colCustodianName.FieldName = "CUSTODIAN";
            this.colCustodianName.Name = "colCustodianName";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtName
            // 
            this.txtName.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // gklpBlock
            // 
            this.gklpBlock.EnterMoveNextControl = true;
            resources.ApplyResources(this.gklpBlock, "gklpBlock");
            this.gklpBlock.Name = "gklpBlock";
            this.gklpBlock.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gklpBlock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("gklpBlock.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("gklpBlock.Properties.Buttons1"))))});
            this.gklpBlock.Properties.ImmediatePopup = true;
            this.gklpBlock.Properties.NullText = resources.GetString("gklpBlock.Properties.NullText");
            this.gklpBlock.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.gklpBlock.Properties.PopupFormSize = new System.Drawing.Size(276, 0);
            this.gklpBlock.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.gklpBlock.Properties.View = this.gridLookUpEdit1View;
            this.gklpBlock.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.gklpBlock_Properties_ButtonClick);
            this.gklpBlock.StyleController = this.layoutControl1;
            this.gklpBlock.Leave += new System.EventHandler(this.gklpBlock_Leave);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBlockId,
            this.colBlock});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            this.gridLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBlock, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colBlockId
            // 
            resources.ApplyResources(this.colBlockId, "colBlockId");
            this.colBlockId.FieldName = "BLOCK_ID";
            this.colBlockId.Name = "colBlockId";
            // 
            // colBlock
            // 
            resources.ApplyResources(this.colBlock, "colBlock");
            this.colBlock.FieldName = "BLOCK";
            this.colBlock.Name = "colBlock";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.lblResponsibleDate,
            this.Custodian,
            this.lblBlock,
            this.lblLocationType,
            this.lblProject,
            this.lblResponsibleToDate,
            this.lblName});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(362, 168);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 141);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(232, 27);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(297, 141);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(65, 27);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(65, 27);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(65, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(232, 141);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(65, 27);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(65, 27);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(65, 27);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblResponsibleDate
            // 
            this.lblResponsibleDate.Control = this.dtResponsibleDate;
            resources.ApplyResources(this.lblResponsibleDate, "lblResponsibleDate");
            this.lblResponsibleDate.Location = new System.Drawing.Point(0, 121);
            this.lblResponsibleDate.Name = "lblResponsibleDate";
            this.lblResponsibleDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblResponsibleDate.Size = new System.Drawing.Size(176, 20);
            this.lblResponsibleDate.TextSize = new System.Drawing.Size(84, 13);
            // 
            // lblName
            // 
            this.lblName.AllowHtmlStringInCaption = true;
            this.lblName.Control = this.txtName;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblName.Size = new System.Drawing.Size(362, 23);
            this.lblName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblName.TextSize = new System.Drawing.Size(84, 13);
            // 
            // Custodian
            // 
            this.Custodian.AllowHtmlStringInCaption = true;
            this.Custodian.Control = this.glkpCustodian;
            resources.ApplyResources(this.Custodian, "Custodian");
            this.Custodian.Location = new System.Drawing.Point(0, 98);
            this.Custodian.Name = "Custodian";
            this.Custodian.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Custodian.Size = new System.Drawing.Size(362, 23);
            this.Custodian.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.Custodian.TextSize = new System.Drawing.Size(84, 13);
            // 
            // lblBlock
            // 
            this.lblBlock.AllowHtmlStringInCaption = true;
            this.lblBlock.Control = this.gklpBlock;
            resources.ApplyResources(this.lblBlock, "lblBlock");
            this.lblBlock.Location = new System.Drawing.Point(0, 46);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblBlock.Size = new System.Drawing.Size(362, 23);
            this.lblBlock.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblBlock.TextSize = new System.Drawing.Size(84, 13);
            // 
            // lblLocationType
            // 
            this.lblLocationType.AllowHtmlStringInCaption = true;
            this.lblLocationType.Control = this.rgLocationType;
            resources.ApplyResources(this.lblLocationType, "lblLocationType");
            this.lblLocationType.Location = new System.Drawing.Point(0, 69);
            this.lblLocationType.Name = "lblLocationType";
            this.lblLocationType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 2);
            this.lblLocationType.Size = new System.Drawing.Size(362, 29);
            this.lblLocationType.TextSize = new System.Drawing.Size(84, 13);
            this.lblLocationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lblProject
            // 
            this.lblProject.AllowHtmlStringInCaption = true;
            this.lblProject.Control = this.glkpProject;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 23);
            this.lblProject.Name = "lblProject";
            this.lblProject.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblProject.Size = new System.Drawing.Size(362, 23);
            this.lblProject.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblProject.TextSize = new System.Drawing.Size(84, 13);
            // 
            // lblResponsibleToDate
            // 
            this.lblResponsibleToDate.Control = this.dtResponsibleToDate;
            resources.ApplyResources(this.lblResponsibleToDate, "lblResponsibleToDate");
            this.lblResponsibleToDate.Location = new System.Drawing.Point(176, 121);
            this.lblResponsibleToDate.Name = "lblResponsibleToDate";
            this.lblResponsibleToDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblResponsibleToDate.Size = new System.Drawing.Size(186, 20);
            this.lblResponsibleToDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblResponsibleToDate.TextSize = new System.Drawing.Size(84, 13);
            this.lblResponsibleToDate.TextToControlDistance = 5;
            // 
            // frmLocationsAdd
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmLocationsAdd";
            this.Load += new System.EventHandler(this.frmLocationsAdd_Load);
            this.Shown += new System.EventHandler(this.frmLocationsAdd_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgLocationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtResponsibleDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCustodian.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gklpBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResponsibleDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Custodian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLocationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResponsibleToDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.GridLookUpEdit gklpBlock;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblBlock;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colBlockId;
        private DevExpress.XtraGrid.Columns.GridColumn colBlock;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCustodian;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem Custodian;
        private DevExpress.XtraGrid.Columns.GridColumn colCustodianId;
        private DevExpress.XtraGrid.Columns.GridColumn colCustodianName;
        private DevExpress.XtraEditors.DateEdit dtResponsibleDate;
        private DevExpress.XtraLayout.LayoutControlItem lblResponsibleDate;
        private DevExpress.XtraEditors.RadioGroup rgLocationType;
        private DevExpress.XtraLayout.LayoutControlItem lblLocationType;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProject;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectID;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraEditors.DateEdit dtResponsibleToDate;
        private DevExpress.XtraLayout.LayoutControlItem lblResponsibleToDate;
    }
}