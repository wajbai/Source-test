namespace ACPP.Modules.Data_Utility
{
    partial class frmMergeAllLedgers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMergeAllLedgers));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coluProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coluProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.radioMergeType = new DevExpress.XtraEditors.RadioGroup();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gcMergeLedgers = new DevExpress.XtraGrid.GridControl();
            this.gvMergeLedgers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCatogoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.capMergeLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpMergeLedgers = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMergeLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMergeLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colgrdMergeLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnMergeLedgers = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcProject = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioMergeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMergeLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMergeLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpMergeLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.radioMergeType);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.gcMergeLedgers);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.btnMergeLedgers);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(504, 301, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(394, 0);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.Tag = "PR";
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coluProject,
            this.coluProjectId});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // coluProject
            // 
            this.coluProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("coluProject.AppearanceHeader.Font")));
            this.coluProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.coluProject, "coluProject");
            this.coluProject.FieldName = "PROJECT";
            this.coluProject.Name = "coluProject";
            // 
            // coluProjectId
            // 
            resources.ApplyResources(this.coluProjectId, "coluProjectId");
            this.coluProjectId.FieldName = "PROJECT_ID";
            this.coluProjectId.Name = "coluProjectId";
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
            // radioMergeType
            // 
            resources.ApplyResources(this.radioMergeType, "radioMergeType");
            this.radioMergeType.Name = "radioMergeType";
            this.radioMergeType.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("radioMergeType.Properties.Appearance.Font")));
            this.radioMergeType.Properties.Appearance.Options.UseFont = true;
            this.radioMergeType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("radioMergeType.Properties.Items"))), resources.GetString("radioMergeType.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("radioMergeType.Properties.Items2"))), resources.GetString("radioMergeType.Properties.Items3"))});
            this.radioMergeType.StyleController = this.layoutControl1;
            this.radioMergeType.SelectedIndexChanged += new System.EventHandler(this.radioMergeType_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gcMergeLedgers
            // 
            resources.ApplyResources(this.gcMergeLedgers, "gcMergeLedgers");
            this.gcMergeLedgers.MainView = this.gvMergeLedgers;
            this.gcMergeLedgers.Name = "gcMergeLedgers";
            this.gcMergeLedgers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpMergeLedgers});
            this.gcMergeLedgers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMergeLedgers});
            // 
            // gvMergeLedgers
            // 
            this.gvMergeLedgers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colLedgerCode,
            this.colLedgerName,
            this.colProjectCatogoryName,
            this.colLedgerGroup,
            this.capMergeLedger,
            this.colgrdMergeLedgerId});
            this.gvMergeLedgers.GridControl = this.gcMergeLedgers;
            this.gvMergeLedgers.Name = "gvMergeLedgers";
            this.gvMergeLedgers.OptionsView.ShowAutoFilterRow = true;
            this.gvMergeLedgers.OptionsView.ShowGroupPanel = false;
            this.gvMergeLedgers.OptionsView.ShowIndicator = false;
            this.gvMergeLedgers.ShownEditor += new System.EventHandler(this.gvUnmappedLedgers_ShownEditor);
            this.gvMergeLedgers.RowCountChanged += new System.EventHandler(this.gvMergeLedgers_RowCountChanged);
            // 
            // colLedgerId
            // 
            this.colLedgerId.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerId.AppearanceCell.Font")));
            this.colLedgerId.AppearanceCell.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colLedgerCode
            // 
            this.colLedgerCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerCode.AppearanceHeader.Font")));
            this.colLedgerCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerCode, "colLedgerCode");
            this.colLedgerCode.FieldName = "LEDGER_CODE";
            this.colLedgerCode.Name = "colLedgerCode";
            this.colLedgerCode.OptionsColumn.AllowEdit = false;
            this.colLedgerCode.OptionsColumn.AllowMove = false;
            this.colLedgerCode.OptionsColumn.AllowSize = false;
            this.colLedgerCode.OptionsColumn.ReadOnly = true;
            // 
            // colLedgerName
            // 
            this.colLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerName.AppearanceHeader.Font")));
            this.colLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsColumn.AllowEdit = false;
            this.colLedgerName.OptionsColumn.AllowFocus = false;
            this.colLedgerName.OptionsColumn.AllowMove = false;
            this.colLedgerName.OptionsColumn.AllowSize = false;
            this.colLedgerName.OptionsColumn.ReadOnly = true;
            this.colLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectCatogoryName
            // 
            this.colProjectCatogoryName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectCatogoryName.AppearanceHeader.Font")));
            this.colProjectCatogoryName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectCatogoryName, "colProjectCatogoryName");
            this.colProjectCatogoryName.FieldName = "PROJECT_CATOGORY_NAME";
            this.colProjectCatogoryName.Name = "colProjectCatogoryName";
            this.colProjectCatogoryName.OptionsColumn.AllowEdit = false;
            this.colProjectCatogoryName.OptionsColumn.AllowFocus = false;
            this.colProjectCatogoryName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colProjectCatogoryName.OptionsColumn.AllowMove = false;
            this.colProjectCatogoryName.OptionsColumn.AllowSize = false;
            this.colProjectCatogoryName.OptionsColumn.ReadOnly = true;
            this.colProjectCatogoryName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLedgerGroup
            // 
            this.colLedgerGroup.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerGroup.AppearanceHeader.Font")));
            this.colLedgerGroup.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerGroup, "colLedgerGroup");
            this.colLedgerGroup.FieldName = "LEDGER_GROUP";
            this.colLedgerGroup.Name = "colLedgerGroup";
            this.colLedgerGroup.OptionsColumn.AllowEdit = false;
            this.colLedgerGroup.OptionsColumn.AllowMove = false;
            this.colLedgerGroup.OptionsColumn.AllowSize = false;
            this.colLedgerGroup.OptionsColumn.ReadOnly = true;
            // 
            // capMergeLedger
            // 
            this.capMergeLedger.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("capMergeLedger.AppearanceCell.BackColor")));
            this.capMergeLedger.AppearanceCell.Options.UseBackColor = true;
            this.capMergeLedger.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("capMergeLedger.AppearanceHeader.Font")));
            this.capMergeLedger.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.capMergeLedger, "capMergeLedger");
            this.capMergeLedger.ColumnEdit = this.rglkpMergeLedgers;
            this.capMergeLedger.FieldName = "MERGE_LEDGER_ID";
            this.capMergeLedger.Name = "capMergeLedger";
            this.capMergeLedger.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rglkpMergeLedgers
            // 
            this.rglkpMergeLedgers.ActionButtonIndex = 1;
            resources.ApplyResources(this.rglkpMergeLedgers, "rglkpMergeLedgers");
            this.rglkpMergeLedgers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpMergeLedgers.Buttons"))), resources.GetString("rglkpMergeLedgers.Buttons1"), ((int)(resources.GetObject("rglkpMergeLedgers.Buttons2"))), ((bool)(resources.GetObject("rglkpMergeLedgers.Buttons3"))), ((bool)(resources.GetObject("rglkpMergeLedgers.Buttons4"))), ((bool)(resources.GetObject("rglkpMergeLedgers.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rglkpMergeLedgers.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("rglkpMergeLedgers.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rglkpMergeLedgers.Buttons8"), ((object)(resources.GetObject("rglkpMergeLedgers.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rglkpMergeLedgers.Buttons10"))), ((bool)(resources.GetObject("rglkpMergeLedgers.Buttons11")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpMergeLedgers.Buttons12"))))});
            this.rglkpMergeLedgers.ImmediatePopup = true;
            this.rglkpMergeLedgers.Name = "rglkpMergeLedgers";
            this.rglkpMergeLedgers.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D;
            this.rglkpMergeLedgers.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpMergeLedgers.PopupFormSize = new System.Drawing.Size(366, 0);
            this.rglkpMergeLedgers.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpMergeLedgers.ValueMember = "MERGE_LEDGER_ID";
            this.rglkpMergeLedgers.View = this.repositoryItemGridLookUpEdit1View;
            this.rglkpMergeLedgers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rglkpMergeLedgers_ButtonClick);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMergeLedgerId,
            this.colMergeLedgerName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colMergeLedgerId
            // 
            resources.ApplyResources(this.colMergeLedgerId, "colMergeLedgerId");
            this.colMergeLedgerId.FieldName = "MERGE_LEDGER_ID";
            this.colMergeLedgerId.Name = "colMergeLedgerId";
            // 
            // colMergeLedgerName
            // 
            resources.ApplyResources(this.colMergeLedgerName, "colMergeLedgerName");
            this.colMergeLedgerName.FieldName = "MERGE_LEDGER_NAME";
            this.colMergeLedgerName.Name = "colMergeLedgerName";
            // 
            // colgrdMergeLedgerId
            // 
            resources.ApplyResources(this.colgrdMergeLedgerId, "colgrdMergeLedgerId");
            this.colgrdMergeLedgerId.FieldName = "MERGE_LEDGER_ID";
            this.colgrdMergeLedgerId.Name = "colgrdMergeLedgerId";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl2.Appearance.Image")));
            this.labelControl2.Appearance.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.StyleController = this.layoutControl1;
            // 
            // btnMergeLedgers
            // 
            this.btnMergeLedgers.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnMergeLedgers, "btnMergeLedgers");
            this.btnMergeLedgers.Name = "btnMergeLedgers";
            this.btnMergeLedgers.StyleController = this.layoutControl1;
            this.btnMergeLedgers.Click += new System.EventHandler(this.btnMapLedgers_Click);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem6,
            this.simpleLabelItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.lblRecordCount,
            this.emptySpaceItem2,
            this.lcProject});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(862, 461);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnMergeLedgers;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(670, 403);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.labelControl2;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(53, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(448, 40);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMergeLedgers;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(856, 339);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(763, 403);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AllowHtmlStringInCaption = true;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 403);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(670, 26);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(670, 26);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(670, 26);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(192, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.radioMergeType;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(448, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(408, 40);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 429);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 5, 2);
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 429);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(720, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(720, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(720, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.AppearanceItemCaption.Font")));
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblRecordCount.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblRecordCount.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblRecordCount.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(799, 429);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 5, 2, 2);
            this.lblRecordCount.Size = new System.Drawing.Size(57, 26);
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(50, 20);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 40);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(448, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcProject
            // 
            this.lcProject.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcProject.AppearanceItemCaption.Font")));
            this.lcProject.AppearanceItemCaption.Options.UseFont = true;
            this.lcProject.Control = this.glkpProject;
            resources.ApplyResources(this.lcProject, "lcProject");
            this.lcProject.Location = new System.Drawing.Point(448, 40);
            this.lcProject.MaxSize = new System.Drawing.Size(408, 24);
            this.lcProject.MinSize = new System.Drawing.Size(408, 24);
            this.lcProject.Name = "lcProject";
            this.lcProject.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcProject.Size = new System.Drawing.Size(408, 24);
            this.lcProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcProject.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcProject.TextSize = new System.Drawing.Size(44, 13);
            this.lcProject.TextToControlDistance = 5;
            // 
            // frmMergeAllLedgers
            // 
            this.AcceptButton = this.btnMergeLedgers;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmMergeAllLedgers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMergeAllLedgers_FormClosing);
            this.Load += new System.EventHandler(this.frmUnMappedLedgers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioMergeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMergeLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMergeLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpMergeLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnMergeLedgers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcMergeLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMergeLedgers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerGroup;
        private DevExpress.XtraGrid.Columns.GridColumn capMergeLedger;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpMergeLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colMergeLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colMergeLedgerName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.RadioGroup radioMergeType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colgrdMergeLedgerId;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCatogoryName;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn coluProject;
        private DevExpress.XtraGrid.Columns.GridColumn coluProjectId;
        private DevExpress.XtraLayout.LayoutControlItem lcProject;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}