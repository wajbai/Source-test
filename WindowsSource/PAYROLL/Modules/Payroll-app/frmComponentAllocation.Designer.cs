namespace PAYROLL.Modules.Payroll_app
{
    partial class frmComponentAllocation
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (resultArgs != null)
                {
                    resultArgs.Dispose();
                    resultArgs = null;
                }
                if (dtConst != null)
                {
                    dtConst.Dispose();
                    dtConst = null;
                }
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem7 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucComponentAllocation = new PAYROLL.Modules.Payroll_app.ucComponentAllocation();
            this.btnDecrease = new DevExpress.XtraEditors.SimpleButton();
            this.btnIncrease = new DevExpress.XtraEditors.SimpleButton();
            this.deProcessDate = new DevExpress.XtraEditors.DateEdit();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.glkpPayrollGroups = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnAllocateComponent = new DevExpress.XtraEditors.SimpleButton();
            this.btnProcess = new DevExpress.XtraEditors.SimpleButton();
            this.fraProcess = new System.Windows.Forms.ProgressBar();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.gcComponent = new DevExpress.XtraGrid.GridControl();
            this.gvComponent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colComponentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDefValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblProcessDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.btnNewComponent = new DevExpress.XtraEditors.SimpleButton();
            this.btnProcessComponent = new DevExpress.XtraEditors.SimpleButton();
            this.btnAllocaCompo = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveCompo = new DevExpress.XtraEditors.SimpleButton();
            this.btnCloseCompon = new DevExpress.XtraEditors.SimpleButton();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProcessDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProcessDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPayrollGroups.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProcessDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.ucComponentAllocation);
            this.layoutControl1.Controls.Add(this.btnDecrease);
            this.layoutControl1.Controls.Add(this.btnIncrease);
            this.layoutControl1.Controls.Add(this.deProcessDate);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.glkpPayrollGroups);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnAllocateComponent);
            this.layoutControl1.Controls.Add(this.btnProcess);
            this.layoutControl1.Controls.Add(this.fraProcess);
            this.layoutControl1.Controls.Add(this.btnNew);
            this.layoutControl1.Controls.Add(this.gcComponent);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem6});
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(297, 87, 291, 393);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1004, 429);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // glkpProject
            // 
            this.glkpProject.EnterMoveNextControl = true;
            this.glkpProject.Location = new System.Drawing.Point(94, 10);
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.AutoHeight = false;
            this.glkpProject.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = "";
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(314, 150);
            this.glkpProject.Properties.View = this.gvProject;
            this.glkpProject.Size = new System.Drawing.Size(314, 20);
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.TabIndex = 0;
            // 
            // gvProject
            // 
            this.gvProject.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvProject.Appearance.FocusedRow.Options.UseFont = true;
            this.gvProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProjectName});
            this.gvProject.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvProject.Name = "gvProject";
            this.gvProject.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvProject.OptionsView.ShowColumnHeaders = false;
            this.gvProject.OptionsView.ShowGroupPanel = false;
            this.gvProject.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            this.colProjectId.Caption = "Project Id";
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProjectName
            // 
            this.colProjectName.Caption = "Project";
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowEdit = false;
            this.colProjectName.Visible = true;
            this.colProjectName.VisibleIndex = 0;
            // 
            // ucComponentAllocation
            // 
            this.ucComponentAllocation.Location = new System.Drawing.Point(852, 43);
            this.ucComponentAllocation.Name = "ucComponentAllocation";
            this.ucComponentAllocation.Size = new System.Drawing.Size(134, 376);
            this.ucComponentAllocation.TabIndex = 20;
            this.ucComponentAllocation.CreateComponentClicked += new System.EventHandler(this.ucComponentAllocation_CreateComponentClicked);
            this.ucComponentAllocation.MapComponentClicked += new System.EventHandler(this.ucComponentAllocation_MapComponentClicked);
            // 
            // btnDecrease
            // 
            this.btnDecrease.Image = global::PAYROLL.Properties.Resources.arrow__down;
            this.btnDecrease.Location = new System.Drawing.Point(7, 139);
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.Size = new System.Drawing.Size(34, 30);
            this.btnDecrease.StyleController = this.layoutControl1;
            this.btnDecrease.TabIndex = 10;
            this.btnDecrease.Click += new System.EventHandler(this.btnDecrease_Click);
            // 
            // btnIncrease
            // 
            this.btnIncrease.Image = global::PAYROLL.Properties.Resources.arrow_up;
            this.btnIncrease.Location = new System.Drawing.Point(7, 104);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(34, 31);
            this.btnIncrease.StyleController = this.layoutControl1;
            this.btnIncrease.TabIndex = 10;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // deProcessDate
            // 
            this.deProcessDate.EditValue = new System.DateTime(2010, 4, 7, 0, 0, 0, 0);
            this.deProcessDate.Location = new System.Drawing.Point(756, 10);
            this.deProcessDate.Name = "deProcessDate";
            this.deProcessDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deProcessDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deProcessDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deProcessDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deProcessDate.Size = new System.Drawing.Size(86, 20);
            this.deProcessDate.StyleController = this.layoutControl1;
            this.deProcessDate.TabIndex = 2;
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Location = new System.Drawing.Point(45, 374);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(731, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            toolTipTitleItem1.Appearance.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem1.Text = "Show Filter  (Alt+F)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "         Check this to filter the records.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.chkShowFilter.SuperTip = superToolTip1;
            this.chkShowFilter.TabIndex = 13;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(780, 397);
            this.btnClose.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnClose.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 25);
            this.btnClose.StyleController = this.layoutControl1;
            toolTipTitleItem2.Appearance.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem2.Text = "Close (Alt + C)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "        Click here to close.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.btnClose.SuperTip = superToolTip2;
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "<u>&C</u>lose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // glkpPayrollGroups
            // 
            this.glkpPayrollGroups.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.glkpPayrollGroups.EnterMoveNextControl = true;
            this.glkpPayrollGroups.Location = new System.Drawing.Point(457, 10);
            this.glkpPayrollGroups.Name = "glkpPayrollGroups";
            this.glkpPayrollGroups.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpPayrollGroups.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpPayrollGroups.Properties.NullText = "";
            this.glkpPayrollGroups.Properties.PopupFormSize = new System.Drawing.Size(175, 150);
            this.glkpPayrollGroups.Properties.View = this.gridLookUpEdit1View;
            this.glkpPayrollGroups.Size = new System.Drawing.Size(196, 20);
            this.glkpPayrollGroups.StyleController = this.layoutControl1;
            toolTipTitleItem3.Appearance.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem3.Appearance.Options.UseImage = true;
            toolTipTitleItem3.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem3.Text = "Group";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "       Click here to select the group.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.glkpPayrollGroups.SuperTip = superToolTip3;
            this.glkpPayrollGroups.TabIndex = 1;
            this.glkpPayrollGroups.EditValueChanged += new System.EventHandler(this.glkpPayrollGroups_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
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
            this.colGroupId.Caption = "Group Id";
            this.colGroupId.FieldName = "GROUP ID";
            this.colGroupId.Name = "colGroupId";
            // 
            // colGroupName
            // 
            this.colGroupName.Caption = "Group ";
            this.colGroupName.FieldName = "Group Name";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnSave.Location = new System.Drawing.Point(711, 397);
            this.btnSave.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnSave.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.StyleController = this.layoutControl1;
            toolTipTitleItem4.Appearance.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem4.Appearance.Options.UseImage = true;
            toolTipTitleItem4.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem4.Text = "Save (Alt + S)";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "         Click here to save the order.";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.btnSave.SuperTip = superToolTip4;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "<u>&S</u>ave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAllocateComponent
            // 
            this.btnAllocateComponent.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnAllocateComponent.Location = new System.Drawing.Point(780, 380);
            this.btnAllocateComponent.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnAllocateComponent.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnAllocateComponent.Name = "btnAllocateComponent";
            this.btnAllocateComponent.Size = new System.Drawing.Size(65, 0);
            this.btnAllocateComponent.StyleController = this.layoutControl1;
            toolTipTitleItem5.Appearance.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem5.Appearance.Options.UseImage = true;
            toolTipTitleItem5.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem5.Text = "Allocate (Alt + A)";
            toolTipItem5.LeftIndent = 6;
            toolTipItem5.Text = "       Click here to allocate the component to group.";
            superToolTip5.Items.Add(toolTipTitleItem5);
            superToolTip5.Items.Add(toolTipItem5);
            this.btnAllocateComponent.SuperTip = superToolTip5;
            this.btnAllocateComponent.TabIndex = 9;
            this.btnAllocateComponent.Text = "<u>&A</u>llocate";
            this.btnAllocateComponent.Click += new System.EventHandler(this.btnAllocateComponent_Click_1);
            // 
            // btnProcess
            // 
            this.btnProcess.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnProcess.Location = new System.Drawing.Point(642, 397);
            this.btnProcess.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnProcess.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(65, 25);
            this.btnProcess.StyleController = this.layoutControl1;
            toolTipTitleItem6.Appearance.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem6.Appearance.Options.UseImage = true;
            toolTipTitleItem6.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem6.Text = "Process (Alt + P)";
            toolTipItem6.LeftIndent = 6;
            toolTipItem6.Text = "         Click here to process the group.";
            superToolTip6.Items.Add(toolTipTitleItem6);
            superToolTip6.Items.Add(toolTipItem6);
            this.btnProcess.SuperTip = superToolTip6;
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "<u>&P</u>rocess";
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // fraProcess
            // 
            this.fraProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.fraProcess.Location = new System.Drawing.Point(45, 397);
            this.fraProcess.Name = "fraProcess";
            this.fraProcess.Size = new System.Drawing.Size(146, 25);
            this.fraProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.fraProcess.TabIndex = 19;
            this.fraProcess.Visible = false;
            // 
            // btnNew
            // 
            this.btnNew.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnNew.Location = new System.Drawing.Point(375, 397);
            this.btnNew.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnNew.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 0);
            this.btnNew.StyleController = this.layoutControl1;
            toolTipTitleItem7.Appearance.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem7.Appearance.Options.UseImage = true;
            toolTipTitleItem7.Image = global::PAYROLL.Properties.Resources.bullet;
            toolTipTitleItem7.Text = "New (Alt + N)";
            toolTipItem7.LeftIndent = 6;
            toolTipItem7.Text = "         Click here to add the new component.";
            superToolTip7.Items.Add(toolTipTitleItem7);
            superToolTip7.Items.Add(toolTipItem7);
            this.btnNew.SuperTip = superToolTip7;
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "<u>&N</u>ew";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // gcComponent
            // 
            this.gcComponent.AllowDrop = true;
            this.gcComponent.Location = new System.Drawing.Point(48, 40);
            this.gcComponent.MainView = this.gvComponent;
            this.gcComponent.Name = "gcComponent";
            this.gcComponent.Size = new System.Drawing.Size(794, 327);
            this.gcComponent.TabIndex = 7;
            this.gcComponent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvComponent});
            this.gcComponent.DragDrop += new System.Windows.Forms.DragEventHandler(this.gcComponent_DragDrop);
            this.gcComponent.DragOver += new System.Windows.Forms.DragEventHandler(this.gcComponent_DragOver);
            // 
            // gvComponent
            // 
            this.gvComponent.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvComponent.Appearance.FocusedRow.Options.UseFont = true;
            this.gvComponent.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvComponent.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvComponent.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colComponentId,
            this.colComponent,
            this.colType,
            this.colDefValue,
            this.colLinkValue,
            this.colEquation,
            this.colmax});
            this.gvComponent.GridControl = this.gcComponent;
            this.gvComponent.Name = "gvComponent";
            this.gvComponent.OptionsBehavior.Editable = false;
            this.gvComponent.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvComponent.OptionsView.ShowGroupPanel = false;
            this.gvComponent.OptionsView.ShowIndicator = false;
            this.gvComponent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvComponent_MouseDown);
            this.gvComponent.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gvComponent_MouseMove);
            this.gvComponent.RowCountChanged += new System.EventHandler(this.gvComponent_RowCountChanged_1);
            // 
            // colComponentId
            // 
            this.colComponentId.Caption = "ComponentId";
            this.colComponentId.FieldName = "componentid";
            this.colComponentId.Name = "colComponentId";
            this.colComponentId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colComponent
            // 
            this.colComponent.Caption = "Component";
            this.colComponent.FieldName = "Component";
            this.colComponent.Name = "colComponent";
            this.colComponent.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colComponent.Visible = true;
            this.colComponent.VisibleIndex = 0;
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            // 
            // colDefValue
            // 
            this.colDefValue.Caption = "Def.Value";
            this.colDefValue.FieldName = "Def.Value";
            this.colDefValue.Name = "colDefValue";
            this.colDefValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDefValue.Visible = true;
            this.colDefValue.VisibleIndex = 2;
            // 
            // colLinkValue
            // 
            this.colLinkValue.Caption = "Link Value";
            this.colLinkValue.FieldName = "Link Value";
            this.colLinkValue.Name = "colLinkValue";
            this.colLinkValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colLinkValue.Visible = true;
            this.colLinkValue.VisibleIndex = 3;
            // 
            // colEquation
            // 
            this.colEquation.Caption = "Equation";
            this.colEquation.FieldName = "Equation";
            this.colEquation.Name = "colEquation";
            this.colEquation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colEquation.Visible = true;
            this.colEquation.VisibleIndex = 4;
            // 
            // colmax
            // 
            this.colmax.Caption = "Max Slab";
            this.colmax.FieldName = "Max Slab";
            this.colmax.Name = "colmax";
            this.colmax.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colmax.Visible = true;
            this.colmax.VisibleIndex = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnNew;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(368, 390);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(69, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnAllocateComponent;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(773, 390);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(69, 28);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(69, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.layoutControlItem13,
            this.emptySpaceItem3,
            this.layoutControlItem2,
            this.layoutControlItem14,
            this.simpleLabelItem1,
            this.layoutControlItem9,
            this.emptySpaceItem4,
            this.simpleLabelItem2,
            this.lblRecordCount,
            this.layoutControlGroup2,
            this.layoutControlItem7,
            this.layoutControlItem11,
            this.lblProcessDate,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1004, 429);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcComponent;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(38, 30);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(804, 337);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(804, 337);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(804, 337);
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
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 166);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(38, 253);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(38, 253);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(38, 253);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnProcess;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(635, 390);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(69, 29);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnClose;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(773, 390);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(69, 29);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.chkShowFilter;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(38, 367);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(735, 23);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(735, 23);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(735, 23);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem13.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem13.Control = this.glkpPayrollGroups;
            this.layoutControlItem13.CustomizationFormText = "Group";
            this.layoutControlItem13.Location = new System.Drawing.Point(408, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem13.Size = new System.Drawing.Size(245, 30);
            this.layoutControlItem13.Text = "Group";
            this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(34, 13);
            this.layoutControlItem13.TextToControlDistance = 5;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(38, 97);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(38, 97);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(38, 97);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnIncrease;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 97);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(38, 35);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(38, 35);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(38, 35);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnDecrease;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 132);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(38, 34);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.CustomizationFormText = "Click on the Arrows  to  Order the  Components";
            this.simpleLabelItem1.Image = global::PAYROLL.Properties.Resources.info;
            this.simpleLabelItem1.Location = new System.Drawing.Point(188, 390);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(296, 28);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(447, 29);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.Text = "Click on the Arrows  to  Order the  Components";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(292, 24);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.fraProcess;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(38, 390);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(150, 29);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(150, 29);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(150, 29);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            this.layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(842, 0);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(152, 33);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(152, 33);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(152, 33);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.CustomizationFormText = "#";
            this.simpleLabelItem2.Location = new System.Drawing.Point(773, 367);
            this.simpleLabelItem2.MaxSize = new System.Drawing.Size(23, 23);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(23, 23);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(23, 23);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.Text = "#";
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.CustomizationFormText = "0";
            this.lblRecordCount.Location = new System.Drawing.Point(796, 367);
            this.lblRecordCount.MaxSize = new System.Drawing.Size(46, 23);
            this.lblRecordCount.MinSize = new System.Drawing.Size(46, 23);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(46, 23);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.Text = "0";
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(6, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(842, 33);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(152, 386);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucComponentAllocation;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(138, 380);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(138, 380);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(146, 380);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSave;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(704, 390);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(69, 29);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem11.Control = this.glkpProject;
            this.layoutControlItem11.CustomizationFormText = "Project";
            this.layoutControlItem11.Location = new System.Drawing.Point(38, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem11.Size = new System.Drawing.Size(370, 30);
            this.layoutControlItem11.Text = "Project";
            this.layoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(41, 13);
            this.layoutControlItem11.TextToControlDistance = 5;
            // 
            // lblProcessDate
            // 
            this.lblProcessDate.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblProcessDate.AppearanceItemCaption.Options.UseFont = true;
            this.lblProcessDate.Control = this.deProcessDate;
            this.lblProcessDate.CustomizationFormText = "Process Date";
            this.lblProcessDate.Location = new System.Drawing.Point(667, 0);
            this.lblProcessDate.MinSize = new System.Drawing.Size(118, 30);
            this.lblProcessDate.Name = "lblProcessDate";
            this.lblProcessDate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lblProcessDate.Size = new System.Drawing.Size(175, 30);
            this.lblProcessDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblProcessDate.Text = "Process Date";
            this.lblProcessDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblProcessDate.TextSize = new System.Drawing.Size(74, 13);
            this.lblProcessDate.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(653, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(14, 30);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btnNewComponent
            // 
            this.btnNewComponent.Location = new System.Drawing.Point(133, 367);
            this.btnNewComponent.Name = "btnNewComponent";
            this.btnNewComponent.Size = new System.Drawing.Size(105, 22);
            this.btnNewComponent.StyleController = this.layoutControl1;
            this.btnNewComponent.TabIndex = 7;
            this.btnNewComponent.Text = "New";
            // 
            // btnProcessComponent
            // 
            this.btnProcessComponent.Location = new System.Drawing.Point(242, 367);
            this.btnProcessComponent.Name = "btnProcessComponent";
            this.btnProcessComponent.Size = new System.Drawing.Size(105, 22);
            this.btnProcessComponent.StyleController = this.layoutControl1;
            this.btnProcessComponent.TabIndex = 8;
            this.btnProcessComponent.Text = "Process";
            // 
            // btnAllocaCompo
            // 
            this.btnAllocaCompo.Location = new System.Drawing.Point(351, 367);
            this.btnAllocaCompo.Name = "btnAllocaCompo";
            this.btnAllocaCompo.Size = new System.Drawing.Size(105, 22);
            this.btnAllocaCompo.StyleController = this.layoutControl1;
            this.btnAllocaCompo.TabIndex = 9;
            this.btnAllocaCompo.Text = "Allocate";
            // 
            // btnSaveCompo
            // 
            this.btnSaveCompo.Location = new System.Drawing.Point(460, 367);
            this.btnSaveCompo.Name = "btnSaveCompo";
            this.btnSaveCompo.Size = new System.Drawing.Size(104, 22);
            this.btnSaveCompo.StyleController = this.layoutControl1;
            this.btnSaveCompo.TabIndex = 10;
            this.btnSaveCompo.Text = "simpleButton4";
            // 
            // btnCloseCompon
            // 
            this.btnCloseCompon.Location = new System.Drawing.Point(568, 367);
            this.btnCloseCompon.Name = "btnCloseCompon";
            this.btnCloseCompon.Size = new System.Drawing.Size(105, 22);
            this.btnCloseCompon.StyleController = this.layoutControl1;
            this.btnCloseCompon.TabIndex = 11;
            this.btnCloseCompon.Text = "simpleButton5";
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.CustomizationFormText = "emptySpaceItem8";
            this.emptySpaceItem8.Location = new System.Drawing.Point(247, 0);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(10, 30);
            this.emptySpaceItem8.Text = "emptySpaceItem8";
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmComponentAllocation
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1004, 429);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComponentAllocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Payroll";
            this.ShowFilterClicked += new System.EventHandler(this.frmComponentAllocation_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmComponentAllocation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmComponentAllocation_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProcessDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deProcessDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPayrollGroups.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProcessDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcComponent;
        private DevExpress.XtraGrid.Views.Grid.GridView gvComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colDefValue;
        private DevExpress.XtraGrid.Columns.GridColumn colLinkValue;
        private DevExpress.XtraGrid.Columns.GridColumn colEquation;
        private DevExpress.XtraGrid.Columns.GridColumn colmax;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnAllocateComponent;
        private DevExpress.XtraEditors.SimpleButton btnProcess;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SimpleButton btnNewComponent;
        private DevExpress.XtraEditors.SimpleButton btnProcessComponent;
        private DevExpress.XtraEditors.SimpleButton btnAllocaCompo;
        private DevExpress.XtraEditors.SimpleButton btnSaveCompo;
        private DevExpress.XtraEditors.SimpleButton btnCloseCompon;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.GridLookUpEdit glkpPayrollGroups;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraEditors.SimpleButton btnIncrease;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnDecrease;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private System.Windows.Forms.ProgressBar fraProcess;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraGrid.Columns.GridColumn colComponentId;
        private ucComponentAllocation ucComponentAllocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProject;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.DateEdit deProcessDate;
        private DevExpress.XtraLayout.LayoutControlItem lblProcessDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
    }
}