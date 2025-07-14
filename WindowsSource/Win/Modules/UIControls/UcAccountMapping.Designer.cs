namespace ACPP.Modules.UIControls
{
    partial class UcAccountMapping
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcAccountMapping));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcMapProject = new DevExpress.XtraGrid.GridControl();
            this.gvMapProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvColSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gvColProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLegalEntity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColOPBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ritxtOPBalance = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvColFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboFlag = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colProjectLedgerApplicableDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbnProjectLedgerApplicableDate = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gvColSetOPBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSetOPBalance = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colSocietyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.LayGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emtpSpaceFilter = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutShowFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkSelectAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMapProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMapProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtOPBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbnProjectLedgerApplicableDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetOPBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emtpSpaceFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutShowFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcMapProject);
            this.layoutControl1.Controls.Add(this.checkEdit1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(545, 128, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
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
            // gcMapProject
            // 
            resources.ApplyResources(this.gcMapProject, "gcMapProject");
            this.gcMapProject.MainView = this.gvMapProject;
            this.gcMapProject.Name = "gcMapProject";
            this.gcMapProject.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ritxtOPBalance,
            this.cboFlag,
            this.chkSelect,
            this.btnSetOPBalance,
            this.rbnProjectLedgerApplicableDate});
            this.gcMapProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMapProject});
            this.gcMapProject.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcMapProject_ProcessGridKey);
            // 
            // gvMapProject
            // 
            this.gvMapProject.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("gvMapProject.Appearance.FocusedCell.BackColor")));
            this.gvMapProject.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvMapProject.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMapProject.Appearance.FocusedRow.Font")));
            this.gvMapProject.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMapProject.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvMapProject.Appearance.HeaderPanel.Font")));
            this.gvMapProject.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMapProject.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvMapProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvColSelect,
            this.gvColProjectId,
            this.gvColProject,
            this.colLegalEntity,
            this.gvColOPBalance,
            this.gvColFlag,
            this.colProjectLedgerApplicableDate,
            this.gvColSetOPBalance,
            this.colSocietyName});
            this.gvMapProject.GridControl = this.gcMapProject;
            this.gvMapProject.Name = "gvMapProject";
            this.gvMapProject.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvMapProject.OptionsView.ShowGroupPanel = false;
            this.gvMapProject.OptionsView.ShowIndicator = false;
            this.gvMapProject.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvMapProject_RowClick);
            this.gvMapProject.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMapProject_RowStyle);
            this.gvMapProject.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvMapProject_CustomRowCellEdit);
            this.gvMapProject.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvMapProject_ShowingEditor);
            // 
            // gvColSelect
            // 
            this.gvColSelect.ColumnEdit = this.chkSelect;
            this.gvColSelect.FieldName = "SELECT";
            this.gvColSelect.Name = "gvColSelect";
            this.gvColSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gvColSelect.OptionsColumn.FixedWidth = true;
            this.gvColSelect.OptionsColumn.ShowCaption = false;
            this.gvColSelect.OptionsFilter.AllowAutoFilter = false;
            this.gvColSelect.OptionsFilter.AllowFilter = false;
            this.gvColSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            resources.ApplyResources(this.gvColSelect, "gvColSelect");
            // 
            // chkSelect
            // 
            resources.ApplyResources(this.chkSelect, "chkSelect");
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkSelect.ValueChecked = 1;
            this.chkSelect.ValueGrayed = 2;
            this.chkSelect.ValueUnchecked = 0;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            this.chkSelect.Click += new System.EventHandler(this.chkSelect_Click);
            // 
            // gvColProjectId
            // 
            resources.ApplyResources(this.gvColProjectId, "gvColProjectId");
            this.gvColProjectId.FieldName = "PROJECT_ID";
            this.gvColProjectId.Name = "gvColProjectId";
            this.gvColProjectId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColProject
            // 
            this.gvColProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColProject.AppearanceHeader.Font")));
            this.gvColProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColProject, "gvColProject");
            this.gvColProject.FieldName = "PROJECT";
            this.gvColProject.Name = "gvColProject";
            this.gvColProject.OptionsColumn.AllowEdit = false;
            this.gvColProject.OptionsColumn.AllowFocus = false;
            this.gvColProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLegalEntity
            // 
            resources.ApplyResources(this.colLegalEntity, "colLegalEntity");
            this.colLegalEntity.FieldName = "SOCIETYNAME";
            this.colLegalEntity.Name = "colLegalEntity";
            this.colLegalEntity.OptionsColumn.AllowEdit = false;
            this.colLegalEntity.OptionsColumn.AllowFocus = false;
            this.colLegalEntity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColOPBalance
            // 
            this.gvColOPBalance.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColOPBalance.AppearanceHeader.Font")));
            this.gvColOPBalance.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColOPBalance, "gvColOPBalance");
            this.gvColOPBalance.ColumnEdit = this.ritxtOPBalance;
            this.gvColOPBalance.DisplayFormat.FormatString = "N";
            this.gvColOPBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gvColOPBalance.FieldName = "AMOUNT";
            this.gvColOPBalance.Name = "gvColOPBalance";
            this.gvColOPBalance.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // ritxtOPBalance
            // 
            resources.ApplyResources(this.ritxtOPBalance, "ritxtOPBalance");
            this.ritxtOPBalance.Mask.EditMask = resources.GetString("ritxtOPBalance.Mask.EditMask");
            this.ritxtOPBalance.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("ritxtOPBalance.Mask.MaskType")));
            this.ritxtOPBalance.Name = "ritxtOPBalance";
            // 
            // gvColFlag
            // 
            this.gvColFlag.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColFlag.AppearanceHeader.Font")));
            this.gvColFlag.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColFlag, "gvColFlag");
            this.gvColFlag.ColumnEdit = this.cboFlag;
            this.gvColFlag.FieldName = "TRANS_MODE";
            this.gvColFlag.Name = "gvColFlag";
            this.gvColFlag.OptionsColumn.AllowFocus = false;
            this.gvColFlag.OptionsColumn.FixedWidth = true;
            this.gvColFlag.OptionsColumn.ShowCaption = false;
            this.gvColFlag.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // cboFlag
            // 
            resources.ApplyResources(this.cboFlag, "cboFlag");
            this.cboFlag.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboFlag.Buttons"))))});
            this.cboFlag.Items.AddRange(new object[] {
            resources.GetString("cboFlag.Items"),
            resources.GetString("cboFlag.Items1")});
            this.cboFlag.Name = "cboFlag";
            this.cboFlag.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colProjectLedgerApplicableDate
            // 
            this.colProjectLedgerApplicableDate.ColumnEdit = this.rbnProjectLedgerApplicableDate;
            this.colProjectLedgerApplicableDate.Name = "colProjectLedgerApplicableDate";
            this.colProjectLedgerApplicableDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProjectLedgerApplicableDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colProjectLedgerApplicableDate.OptionsColumn.AllowMove = false;
            this.colProjectLedgerApplicableDate.OptionsColumn.FixedWidth = true;
            this.colProjectLedgerApplicableDate.OptionsColumn.TabStop = false;
            this.colProjectLedgerApplicableDate.OptionsFilter.AllowAutoFilter = false;
            this.colProjectLedgerApplicableDate.OptionsFilter.AllowFilter = false;
            this.colProjectLedgerApplicableDate.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.colProjectLedgerApplicableDate, "colProjectLedgerApplicableDate");
            // 
            // rbnProjectLedgerApplicableDate
            // 
            resources.ApplyResources(this.rbnProjectLedgerApplicableDate, "rbnProjectLedgerApplicableDate");
            this.rbnProjectLedgerApplicableDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons"))), resources.GetString("rbnProjectLedgerApplicableDate.Buttons1"), ((int)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons2"))), ((bool)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons3"))), ((bool)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons4"))), ((bool)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons6"))), global::ACPP.Properties.Resources.project_mapping, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbnProjectLedgerApplicableDate.Buttons7"), ((object)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons9"))), ((bool)(resources.GetObject("rbnProjectLedgerApplicableDate.Buttons10"))))});
            this.rbnProjectLedgerApplicableDate.Name = "rbnProjectLedgerApplicableDate";
            this.rbnProjectLedgerApplicableDate.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rbnProjectLedgerApplicableDate_ButtonClick);
            // 
            // gvColSetOPBalance
            // 
            resources.ApplyResources(this.gvColSetOPBalance, "gvColSetOPBalance");
            this.gvColSetOPBalance.ColumnEdit = this.btnSetOPBalance;
            this.gvColSetOPBalance.FieldName = "SetOPBalance";
            this.gvColSetOPBalance.Name = "gvColSetOPBalance";
            this.gvColSetOPBalance.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gvColSetOPBalance.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // btnSetOPBalance
            // 
            resources.ApplyResources(this.btnSetOPBalance, "btnSetOPBalance");
            this.btnSetOPBalance.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("btnSetOPBalance.Buttons"))), resources.GetString("btnSetOPBalance.Buttons1"), ((int)(resources.GetObject("btnSetOPBalance.Buttons2"))), ((bool)(resources.GetObject("btnSetOPBalance.Buttons3"))), ((bool)(resources.GetObject("btnSetOPBalance.Buttons4"))), ((bool)(resources.GetObject("btnSetOPBalance.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("btnSetOPBalance.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("btnSetOPBalance.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("btnSetOPBalance.Buttons8"), ((object)(resources.GetObject("btnSetOPBalance.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("btnSetOPBalance.Buttons10"))), ((bool)(resources.GetObject("btnSetOPBalance.Buttons11"))))});
            this.btnSetOPBalance.Name = "btnSetOPBalance";
            this.btnSetOPBalance.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnSetOPBalance.Click += new System.EventHandler(this.btnSetOPBalance_Click);
            // 
            // colSocietyName
            // 
            resources.ApplyResources(this.colSocietyName, "colSocietyName");
            this.colSocietyName.FieldName = "SOCIETYNAME";
            this.colSocietyName.Name = "colSocietyName";
            this.colSocietyName.OptionsColumn.AllowEdit = false;
            this.colSocietyName.OptionsColumn.AllowFocus = false;
            // 
            // checkEdit1
            // 
            resources.ApplyResources(this.checkEdit1, "checkEdit1");
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = resources.GetString("checkEdit1.Properties.Caption");
            this.checkEdit1.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.LayGroup});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(415, 311);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // LayGroup
            // 
            this.LayGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("LayGroup.AppearanceGroup.Font")));
            this.LayGroup.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.LayGroup, "LayGroup");
            this.LayGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emtpSpaceFilter,
            this.layoutShowFilter});
            this.LayGroup.Location = new System.Drawing.Point(0, 0);
            this.LayGroup.Name = "LayGroup";
            this.LayGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 1);
            this.LayGroup.Size = new System.Drawing.Size(415, 311);
            this.LayGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcMapProject;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(403, 262);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emtpSpaceFilter
            // 
            this.emtpSpaceFilter.AllowHotTrack = false;
            resources.ApplyResources(this.emtpSpaceFilter, "emtpSpaceFilter");
            this.emtpSpaceFilter.Location = new System.Drawing.Point(128, 262);
            this.emtpSpaceFilter.Name = "emtpSpaceFilter";
            this.emtpSpaceFilter.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 1);
            this.emtpSpaceFilter.Size = new System.Drawing.Size(275, 21);
            this.emtpSpaceFilter.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutShowFilter
            // 
            this.layoutShowFilter.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutShowFilter, "layoutShowFilter");
            this.layoutShowFilter.Location = new System.Drawing.Point(0, 262);
            this.layoutShowFilter.Name = "layoutShowFilter";
            this.layoutShowFilter.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 2, 1, 1);
            this.layoutShowFilter.Size = new System.Drawing.Size(128, 21);
            this.layoutShowFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutShowFilter.TextToControlDistance = 0;
            this.layoutShowFilter.TextVisible = false;
            // 
            // chkSelectAll
            // 
            resources.ApplyResources(this.chkSelectAll, "chkSelectAll");
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Properties.Caption = resources.GetString("chkSelectAll.Properties.Caption");
            this.chkSelectAll.StyleController = this.layoutControl1;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // UcAccountMapping
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcAccountMapping";
            this.Load += new System.EventHandler(this.UcAccountMapping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMapProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMapProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ritxtOPBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbnProjectLedgerApplicableDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetOPBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emtpSpaceFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutShowFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcMapProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMapProject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gvColProject;
        private DevExpress.XtraGrid.Columns.GridColumn gvColOPBalance;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ritxtOPBalance;
        private DevExpress.XtraGrid.Columns.GridColumn gvColFlag;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboFlag;
        private DevExpress.XtraGrid.Columns.GridColumn gvColProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn gvColSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSelect;
        private DevExpress.XtraLayout.LayoutControlGroup LayGroup;
        private DevExpress.XtraLayout.EmptySpaceItem emtpSpaceFilter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutShowFilter;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit chkSelectAll;
        private DevExpress.XtraGrid.Columns.GridColumn gvColSetOPBalance;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnSetOPBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colLegalEntity;
        private DevExpress.XtraGrid.Columns.GridColumn colSocietyName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectLedgerApplicableDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbnProjectLedgerApplicableDate;
    }
}
