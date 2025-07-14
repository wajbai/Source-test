namespace ACPP.Modules.Inventory.Asset
{
    partial class frmAssetItemLedgerMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetItemLedgerMapping));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.glkpLedgerMapping = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.gcAssetItemLedgerMapping = new DevExpress.XtraGrid.GridControl();
            this.gvAssetItemLedgerMapping = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colParentClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkAccountLedger = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPreviousAccountLedgerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLedgerMapping = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedgerMapping.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItemLedgerMapping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItemLedgerMapping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkAccountLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerMapping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.glkpLedgerMapping);
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.gcAssetItemLedgerMapping);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(305, 223, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            // glkpLedgerMapping
            // 
            resources.ApplyResources(this.glkpLedgerMapping, "glkpLedgerMapping");
            this.glkpLedgerMapping.EnterMoveNextControl = true;
            this.glkpLedgerMapping.Name = "glkpLedgerMapping";
            this.glkpLedgerMapping.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpLedgerMapping.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpLedgerMapping.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpLedgerMapping.Properties.Buttons1"))), resources.GetString("glkpLedgerMapping.Properties.Buttons2"), ((int)(resources.GetObject("glkpLedgerMapping.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpLedgerMapping.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpLedgerMapping.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpLedgerMapping.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpLedgerMapping.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpLedgerMapping.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("glkpLedgerMapping.Properties.Buttons9"), ((object)(resources.GetObject("glkpLedgerMapping.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpLedgerMapping.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpLedgerMapping.Properties.Buttons12"))))});
            this.glkpLedgerMapping.Properties.ImmediatePopup = true;
            this.glkpLedgerMapping.Properties.NullText = resources.GetString("glkpLedgerMapping.Properties.NullText");
            this.glkpLedgerMapping.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpLedgerMapping.Properties.View = this.gridLookUpEdit1View;
            this.glkpLedgerMapping.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpLedgerMapping_Properties_ButtonClick);
            this.glkpLedgerMapping.StyleController = this.layoutControl1;
            this.glkpLedgerMapping.EditValueChanged += new System.EventHandler(this.glkpLedgerMapping_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colLedgerName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colLedgerId
            // 
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gcAssetItemLedgerMapping
            // 
            resources.ApplyResources(this.gcAssetItemLedgerMapping, "gcAssetItemLedgerMapping");
            this.gcAssetItemLedgerMapping.MainView = this.gvAssetItemLedgerMapping;
            this.gcAssetItemLedgerMapping.Name = "gcAssetItemLedgerMapping";
            this.gcAssetItemLedgerMapping.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkAccountLedger});
            this.gcAssetItemLedgerMapping.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetItemLedgerMapping});
            // 
            // gvAssetItemLedgerMapping
            // 
            this.gvAssetItemLedgerMapping.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetItemLedgerMapping.Appearance.HeaderPanel.Font")));
            this.gvAssetItemLedgerMapping.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetItemLedgerMapping.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colParentClass,
            this.colAssetClass,
            this.colAssetItem,
            this.colAccountLedger,
            this.colPreviousAccountLedgerID});
            this.gvAssetItemLedgerMapping.GridControl = this.gcAssetItemLedgerMapping;
            this.gvAssetItemLedgerMapping.Name = "gvAssetItemLedgerMapping";
            this.gvAssetItemLedgerMapping.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvAssetItemLedgerMapping.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetItemLedgerMapping.OptionsSelection.MultiSelect = true;
            this.gvAssetItemLedgerMapping.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvAssetItemLedgerMapping.OptionsView.ShowGroupPanel = false;
            this.gvAssetItemLedgerMapping.OptionsView.ShowIndicator = false;
            this.gvAssetItemLedgerMapping.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvAssetItemLedgerMapping_SelectionChanged);
            this.gvAssetItemLedgerMapping.ColumnFilterChanged += new System.EventHandler(this.gvAssetItemLedgerMapping_ColumnFilterChanged);
            this.gvAssetItemLedgerMapping.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvAssetItemLedgerMapping_MouseDown);
            this.gvAssetItemLedgerMapping.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvAssetItemLedgerMapping_MouseUp);
            // 
            // colParentClass
            // 
            resources.ApplyResources(this.colParentClass, "colParentClass");
            this.colParentClass.FieldName = "PARENT_CLASS";
            this.colParentClass.Name = "colParentClass";
            this.colParentClass.OptionsColumn.AllowEdit = false;
            this.colParentClass.OptionsColumn.AllowFocus = false;
            this.colParentClass.OptionsColumn.FixedWidth = true;
            this.colParentClass.OptionsColumn.TabStop = false;
            // 
            // colAssetClass
            // 
            resources.ApplyResources(this.colAssetClass, "colAssetClass");
            this.colAssetClass.FieldName = "ASSET_GROUP";
            this.colAssetClass.Name = "colAssetClass";
            this.colAssetClass.OptionsColumn.AllowEdit = false;
            this.colAssetClass.OptionsColumn.AllowFocus = false;
            this.colAssetClass.OptionsColumn.FixedWidth = true;
            this.colAssetClass.OptionsColumn.TabStop = false;
            // 
            // colAssetItem
            // 
            resources.ApplyResources(this.colAssetItem, "colAssetItem");
            this.colAssetItem.FieldName = "ASSET_ITEM";
            this.colAssetItem.Name = "colAssetItem";
            this.colAssetItem.OptionsColumn.AllowEdit = false;
            this.colAssetItem.OptionsColumn.AllowFocus = false;
            this.colAssetItem.OptionsColumn.FixedWidth = true;
            this.colAssetItem.OptionsColumn.TabStop = false;
            // 
            // colAccountLedger
            // 
            resources.ApplyResources(this.colAccountLedger, "colAccountLedger");
            this.colAccountLedger.ColumnEdit = this.rglkAccountLedger;
            this.colAccountLedger.FieldName = "LEDGER_ID";
            this.colAccountLedger.Name = "colAccountLedger";
            // 
            // rglkAccountLedger
            // 
            resources.ApplyResources(this.rglkAccountLedger, "rglkAccountLedger");
            this.rglkAccountLedger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkAccountLedger.Buttons"))))});
            this.rglkAccountLedger.Name = "rglkAccountLedger";
            this.rglkAccountLedger.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountLedgerId,
            this.colAccLedger});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colAccountLedgerId
            // 
            this.colAccountLedgerId.FieldName = "LEDGER_ID";
            this.colAccountLedgerId.Name = "colAccountLedgerId";
            // 
            // colAccLedger
            // 
            resources.ApplyResources(this.colAccLedger, "colAccLedger");
            this.colAccLedger.FieldName = "LEDGER_NAME";
            this.colAccLedger.Name = "colAccLedger";
            // 
            // colPreviousAccountLedgerID
            // 
            resources.ApplyResources(this.colPreviousAccountLedgerID, "colPreviousAccountLedgerID");
            this.colPreviousAccountLedgerID.FieldName = "ACCOUNT_LEDGER_ID";
            this.colPreviousAccountLedgerID.Name = "colPreviousAccountLedgerID";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lblLedgerMapping,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1055, 469);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAssetItemLedgerMapping;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1055, 417);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblLedgerMapping
            // 
            this.lblLedgerMapping.Control = this.glkpLedgerMapping;
            resources.ApplyResources(this.lblLedgerMapping, "lblLedgerMapping");
            this.lblLedgerMapping.Location = new System.Drawing.Point(679, 0);
            this.lblLedgerMapping.Name = "lblLedgerMapping";
            this.lblLedgerMapping.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 3, 0, 0);
            this.lblLedgerMapping.Size = new System.Drawing.Size(309, 26);
            this.lblLedgerMapping.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 2);
            this.lblLedgerMapping.TextSize = new System.Drawing.Size(75, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(679, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 443);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(922, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(985, 443);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(922, 443);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnApply;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(988, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmAssetItemLedgerMapping
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetItemLedgerMapping";
            this.ShowFilterClicked += new System.EventHandler(this.frmAssetItemLedgerMapping_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmAssetItemLedgerMapping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedgerMapping.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetItemLedgerMapping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetItemLedgerMapping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkAccountLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerMapping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.GridLookUpEdit glkpLedgerMapping;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.GridControl gcAssetItemLedgerMapping;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetItemLedgerMapping;
        private DevExpress.XtraGrid.Columns.GridColumn colParentClass;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClass;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetItem;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountLedger;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblLedgerMapping;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkAccountLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccLedger;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colPreviousAccountLedgerID;
    }
}