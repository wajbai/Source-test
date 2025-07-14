namespace ACPP.Modules.Master
{
    partial class frmBudgetDevProjectsDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBudgetDevProjectsDetails));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcBudgetNewProject = new DevExpress.XtraGrid.GridControl();
            this.gvBudgetNewProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcAcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColBudgetNewProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtBudgetNewProject = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ColBudgetPExpenseAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtPExpenseAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColBudgetPIncomeAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtPIncomeAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColBudgetPGovtIncomeAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtGHelpAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColPProvinceHelp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtPHelpAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColBudgetPRemakrs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtBudgetNewProjectRemarks = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDeleteBudgetNewProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.chkFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcDevProjectsActivities = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNote = new DevExpress.XtraLayout.SimpleLabelItem();
            this.colSequenceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetNewProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetNewProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPExpenseAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPIncomeAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtGHelpAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPHelpAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProjectRemarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDevProjectsActivities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcBudgetNewProject);
            this.layoutControl1.Controls.Add(this.chkFilter);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(180, 210, 266, 369);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1087, 355);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcBudgetNewProject
            // 
            this.gcBudgetNewProject.Location = new System.Drawing.Point(7, 7);
            this.gcBudgetNewProject.MainView = this.gvBudgetNewProject;
            this.gcBudgetNewProject.Name = "gcBudgetNewProject";
            this.gcBudgetNewProject.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtBudgetNewProject,
            this.repositoryItemCalcEdit1,
            this.rctxtPIncomeAmt,
            this.rctxtPExpenseAmt,
            this.rctxtPHelpAmt,
            this.rbtnDelete,
            this.rctxtGHelpAmt,
            this.rtxtBudgetNewProjectRemarks});
            this.gcBudgetNewProject.Size = new System.Drawing.Size(1073, 313);
            this.gcBudgetNewProject.TabIndex = 29;
            this.gcBudgetNewProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBudgetNewProject});
            this.gcBudgetNewProject.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcBudgetNewProject_ProcessGridKey);
            // 
            // gvBudgetNewProject
            // 
            this.gvBudgetNewProject.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetNewProject.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBudgetNewProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcAcId,
            this.colSequenceNo,
            this.ColBudgetNewProject,
            this.ColBudgetPExpenseAmount,
            this.ColBudgetPIncomeAmount,
            this.ColBudgetPGovtIncomeAmount,
            this.ColPProvinceHelp,
            this.ColBudgetPRemakrs,
            this.colDeleteBudgetNewProject});
            this.gvBudgetNewProject.GridControl = this.gcBudgetNewProject;
            this.gvBudgetNewProject.Name = "gvBudgetNewProject";
            this.gvBudgetNewProject.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvBudgetNewProject.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvBudgetNewProject.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvBudgetNewProject.OptionsBehavior.AutoPopulateColumns = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowColumnMoving = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowFilter = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowGroup = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowSort = false;
            this.gvBudgetNewProject.OptionsNavigation.AutoFocusNewRow = true;
            this.gvBudgetNewProject.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvBudgetNewProject.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvBudgetNewProject.OptionsView.ShowGroupPanel = false;
            this.gvBudgetNewProject.OptionsView.ShowIndicator = false;
            // 
            // gcAcId
            // 
            this.gcAcId.Caption = "AcId";
            this.gcAcId.FieldName = "ACC_YEAR_ID";
            this.gcAcId.Name = "gcAcId";
            this.gcAcId.OptionsColumn.AllowEdit = false;
            this.gcAcId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcAcId.OptionsColumn.AllowMove = false;
            this.gcAcId.OptionsColumn.AllowSize = false;
            this.gcAcId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcAcId.Width = 26;
            // 
            // ColBudgetNewProject
            // 
            this.ColBudgetNewProject.Caption = "Activity/Project";
            this.ColBudgetNewProject.ColumnEdit = this.rtxtBudgetNewProject;
            this.ColBudgetNewProject.FieldName = "NEW_PROJECT";
            this.ColBudgetNewProject.Name = "ColBudgetNewProject";
            this.ColBudgetNewProject.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetNewProject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetNewProject.OptionsColumn.AllowMove = false;
            this.ColBudgetNewProject.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetNewProject.ToolTip = "New Project";
            this.ColBudgetNewProject.Visible = true;
            this.ColBudgetNewProject.VisibleIndex = 0;
            this.ColBudgetNewProject.Width = 250;
            // 
            // rtxtBudgetNewProject
            // 
            this.rtxtBudgetNewProject.AutoHeight = false;
            this.rtxtBudgetNewProject.MaxLength = 100;
            this.rtxtBudgetNewProject.Name = "rtxtBudgetNewProject";
            // 
            // ColBudgetPExpenseAmount
            // 
            this.ColBudgetPExpenseAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.ColBudgetPExpenseAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ColBudgetPExpenseAmount.Caption = "Total Cost";
            this.ColBudgetPExpenseAmount.ColumnEdit = this.rctxtPExpenseAmt;
            this.ColBudgetPExpenseAmount.FieldName = "PROPOSED_EXPENSE_AMOUNT";
            this.ColBudgetPExpenseAmount.Name = "ColBudgetPExpenseAmount";
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowMove = false;
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowSize = false;
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPExpenseAmount.OptionsColumn.FixedWidth = true;
            this.ColBudgetPExpenseAmount.ToolTip = "Expenditure";
            this.ColBudgetPExpenseAmount.Visible = true;
            this.ColBudgetPExpenseAmount.VisibleIndex = 1;
            this.ColBudgetPExpenseAmount.Width = 150;
            // 
            // rctxtPExpenseAmt
            // 
            this.rctxtPExpenseAmt.AutoHeight = false;
            this.rctxtPExpenseAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rctxtPExpenseAmt.Mask.EditMask = "n";
            this.rctxtPExpenseAmt.Mask.UseMaskAsDisplayFormat = true;
            this.rctxtPExpenseAmt.Name = "rctxtPExpenseAmt";
            // 
            // ColBudgetPIncomeAmount
            // 
            this.ColBudgetPIncomeAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.ColBudgetPIncomeAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ColBudgetPIncomeAmount.Caption = "Own/Local";
            this.ColBudgetPIncomeAmount.ColumnEdit = this.rctxtPIncomeAmt;
            this.ColBudgetPIncomeAmount.FieldName = "PROPOSED_INCOME_AMOUNT";
            this.ColBudgetPIncomeAmount.Name = "ColBudgetPIncomeAmount";
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowMove = false;
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowSize = false;
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPIncomeAmount.OptionsColumn.FixedWidth = true;
            this.ColBudgetPIncomeAmount.ToolTip = "Income";
            this.ColBudgetPIncomeAmount.Visible = true;
            this.ColBudgetPIncomeAmount.VisibleIndex = 2;
            this.ColBudgetPIncomeAmount.Width = 150;
            // 
            // rctxtPIncomeAmt
            // 
            this.rctxtPIncomeAmt.AutoHeight = false;
            this.rctxtPIncomeAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rctxtPIncomeAmt.Mask.EditMask = "n";
            this.rctxtPIncomeAmt.Mask.UseMaskAsDisplayFormat = true;
            this.rctxtPIncomeAmt.MaxLength = 13;
            this.rctxtPIncomeAmt.Name = "rctxtPIncomeAmt";
            // 
            // ColBudgetPGovtIncomeAmount
            // 
            this.ColBudgetPGovtIncomeAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.ColBudgetPGovtIncomeAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ColBudgetPGovtIncomeAmount.Caption = "Govt./Foreign";
            this.ColBudgetPGovtIncomeAmount.ColumnEdit = this.rctxtGHelpAmt;
            this.ColBudgetPGovtIncomeAmount.FieldName = "GN_HELP_PROPOSED_AMOUNT";
            this.ColBudgetPGovtIncomeAmount.Name = "ColBudgetPGovtIncomeAmount";
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowMove = false;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowSize = false;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.FixedWidth = true;
            this.ColBudgetPGovtIncomeAmount.ToolTip = "Govt./ Foreign Agencies";
            this.ColBudgetPGovtIncomeAmount.Visible = true;
            this.ColBudgetPGovtIncomeAmount.VisibleIndex = 3;
            this.ColBudgetPGovtIncomeAmount.Width = 150;
            // 
            // rctxtGHelpAmt
            // 
            this.rctxtGHelpAmt.AutoHeight = false;
            this.rctxtGHelpAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rctxtGHelpAmt.Mask.EditMask = "n";
            this.rctxtGHelpAmt.Mask.UseMaskAsDisplayFormat = true;
            this.rctxtGHelpAmt.MaxLength = 13;
            this.rctxtGHelpAmt.Name = "rctxtGHelpAmt";
            // 
            // ColPProvinceHelp
            // 
            this.ColPProvinceHelp.AppearanceHeader.Options.UseTextOptions = true;
            this.ColPProvinceHelp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ColPProvinceHelp.Caption = "Province";
            this.ColPProvinceHelp.ColumnEdit = this.rctxtPHelpAmt;
            this.ColPProvinceHelp.FieldName = "HO_HELP_PROPOSED_AMOUNT";
            this.ColPProvinceHelp.Name = "ColPProvinceHelp";
            this.ColPProvinceHelp.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColPProvinceHelp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColPProvinceHelp.OptionsColumn.AllowMove = false;
            this.ColPProvinceHelp.OptionsColumn.AllowSize = false;
            this.ColPProvinceHelp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColPProvinceHelp.OptionsColumn.FixedWidth = true;
            this.ColPProvinceHelp.UnboundExpression = "Province Help";
            this.ColPProvinceHelp.Visible = true;
            this.ColPProvinceHelp.VisibleIndex = 4;
            this.ColPProvinceHelp.Width = 150;
            // 
            // rctxtPHelpAmt
            // 
            this.rctxtPHelpAmt.AutoHeight = false;
            this.rctxtPHelpAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rctxtPHelpAmt.Mask.EditMask = "n";
            this.rctxtPHelpAmt.Mask.UseMaskAsDisplayFormat = true;
            this.rctxtPHelpAmt.Name = "rctxtPHelpAmt";
            // 
            // ColBudgetPRemakrs
            // 
            this.ColBudgetPRemakrs.Caption = "Remarks";
            this.ColBudgetPRemakrs.ColumnEdit = this.rtxtBudgetNewProjectRemarks;
            this.ColBudgetPRemakrs.FieldName = "REMARKS";
            this.ColBudgetPRemakrs.Name = "ColBudgetPRemakrs";
            this.ColBudgetPRemakrs.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPRemakrs.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPRemakrs.OptionsColumn.AllowMove = false;
            this.ColBudgetPRemakrs.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPRemakrs.ToolTip = "Remarks";
            this.ColBudgetPRemakrs.UnboundExpression = "Remarks";
            this.ColBudgetPRemakrs.Visible = true;
            this.ColBudgetPRemakrs.VisibleIndex = 5;
            this.ColBudgetPRemakrs.Width = 167;
            // 
            // rtxtBudgetNewProjectRemarks
            // 
            this.rtxtBudgetNewProjectRemarks.AutoHeight = false;
            this.rtxtBudgetNewProjectRemarks.MaxLength = 100;
            this.rtxtBudgetNewProjectRemarks.Name = "rtxtBudgetNewProjectRemarks";
            // 
            // colDeleteBudgetNewProject
            // 
            this.colDeleteBudgetNewProject.ColumnEdit = this.rbtnDelete;
            this.colDeleteBudgetNewProject.Name = "colDeleteBudgetNewProject";
            this.colDeleteBudgetNewProject.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowMove = false;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowShowHide = false;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowSize = false;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDeleteBudgetNewProject.OptionsColumn.FixedWidth = true;
            this.colDeleteBudgetNewProject.OptionsColumn.ShowCaption = false;
            this.colDeleteBudgetNewProject.OptionsColumn.TabStop = false;
            this.colDeleteBudgetNewProject.OptionsFilter.AllowAutoFilter = false;
            this.colDeleteBudgetNewProject.OptionsFilter.AllowFilter = false;
            this.colDeleteBudgetNewProject.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colDeleteBudgetNewProject.Visible = true;
            this.colDeleteBudgetNewProject.VisibleIndex = 6;
            this.colDeleteBudgetNewProject.Width = 20;
            // 
            // rbtnDelete
            // 
            this.rbtnDelete.AutoHeight = false;
            this.rbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("rbtnDelete.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Delete current row", null, null, true)});
            this.rbtnDelete.Name = "rbtnDelete";
            this.rbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.rbtnDelete.Click += new System.EventHandler(this.rbtnDelete_Click);
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // chkFilter
            // 
            this.chkFilter.Location = new System.Drawing.Point(7, 324);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkFilter.Properties.Caption = "Show <b>&F</b>ilter";
            this.chkFilter.Size = new System.Drawing.Size(75, 19);
            this.chkFilter.StyleController = this.layoutControl1;
            this.chkFilter.TabIndex = 15;
            this.chkFilter.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1008, 324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.ToolTip = "Close the Budget Form";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(931, 324);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 24);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.ToolTip = "Save the Budget Details ";
            this.btnOk.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.lcFilter,
            this.emptySpaceItem2,
            this.lcDevProjectsActivities,
            this.lblNote});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1087, 355);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnOk;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(924, 317);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(77, 28);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(77, 28);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(77, 28);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(1001, 317);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(76, 28);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(76, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(76, 28);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lcFilter
            // 
            this.lcFilter.Control = this.chkFilter;
            this.lcFilter.CustomizationFormText = "lcFilter";
            this.lcFilter.Location = new System.Drawing.Point(0, 317);
            this.lcFilter.MaxSize = new System.Drawing.Size(79, 28);
            this.lcFilter.MinSize = new System.Drawing.Size(79, 28);
            this.lcFilter.Name = "lcFilter";
            this.lcFilter.Size = new System.Drawing.Size(79, 28);
            this.lcFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcFilter.Text = "lcFilter";
            this.lcFilter.TextSize = new System.Drawing.Size(0, 0);
            this.lcFilter.TextToControlDistance = 0;
            this.lcFilter.TextVisible = false;
            this.lcFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(520, 317);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(404, 28);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcDevProjectsActivities
            // 
            this.lcDevProjectsActivities.Control = this.gcBudgetNewProject;
            this.lcDevProjectsActivities.CustomizationFormText = "lcDevProjectsActivities";
            this.lcDevProjectsActivities.Location = new System.Drawing.Point(0, 0);
            this.lcDevProjectsActivities.Name = "lcDevProjectsActivities";
            this.lcDevProjectsActivities.Size = new System.Drawing.Size(1077, 317);
            this.lcDevProjectsActivities.Text = "lcDevProjectsActivities";
            this.lcDevProjectsActivities.TextSize = new System.Drawing.Size(0, 0);
            this.lcDevProjectsActivities.TextToControlDistance = 0;
            this.lcDevProjectsActivities.TextVisible = false;
            // 
            // lblNote
            // 
            this.lblNote.AllowHotTrack = false;
            this.lblNote.AllowHtmlStringInCaption = true;
            this.lblNote.CustomizationFormText = " <b><color=\"Blue\">  Alt + D </color> Delete current row </b>";
            this.lblNote.Location = new System.Drawing.Point(79, 317);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(441, 28);
            this.lblNote.Text = " <b><color=\"Blue\">  Alt + D </color> Delete current row </b>";
            this.lblNote.TextSize = new System.Drawing.Size(163, 13);
            // 
            // colSequenceNo
            // 
            this.colSequenceNo.Caption = "gridColumn1";
            this.colSequenceNo.FieldName = "SEQUENCE_NO";
            this.colSequenceNo.Name = "colSequenceNo";
            this.colSequenceNo.OptionsColumn.AllowEdit = false;
            this.colSequenceNo.OptionsColumn.AllowFocus = false;
            // 
            // frmBudgetDevProjectsDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1087, 355);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBudgetDevProjectsDetails";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Text = "Developmental Project / New Project Budget Details";
            this.Load += new System.EventHandler(this.frmBudgetDevProjectsDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetNewProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetNewProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPExpenseAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPIncomeAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtGHelpAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPHelpAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProjectRemarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDevProjectsActivities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.CheckEdit chkFilter;
        private DevExpress.XtraLayout.LayoutControlItem lcFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.GridControl gcBudgetNewProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBudgetNewProject;
        private DevExpress.XtraGrid.Columns.GridColumn gcAcId;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetNewProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtBudgetNewProject;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPExpenseAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtPExpenseAmt;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPIncomeAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtPIncomeAmt;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPGovtIncomeAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtGHelpAmt;
        private DevExpress.XtraGrid.Columns.GridColumn ColPProvinceHelp;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtPHelpAmt;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPRemakrs;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtBudgetNewProjectRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colDeleteBudgetNewProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraLayout.LayoutControlItem lcDevProjectsActivities;
        private DevExpress.XtraLayout.SimpleLabelItem lblNote;
        private DevExpress.XtraGrid.Columns.GridColumn colSequenceNo;
    }
}