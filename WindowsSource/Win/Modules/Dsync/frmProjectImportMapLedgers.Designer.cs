namespace ACPP.Modules.Dsync
{
    partial class frmProjectImportMapLedgers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectImportMapLedgers));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gcUnMappedLedgers = new DevExpress.XtraGrid.GridControl();
            this.gvUnmappedLedgers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colImportedLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportedLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportedLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportedLedgerGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExistingLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpExistingLedgers = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repGrdListLedgerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.collkpExistingLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collkpExistingLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExistingLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnMapLedgers = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUnMappedLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnmappedLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpExistingLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repGrdListLedgerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.gcUnMappedLedgers);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.btnMapLedgers);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(579, 301, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gcUnMappedLedgers
            // 
            resources.ApplyResources(this.gcUnMappedLedgers, "gcUnMappedLedgers");
            this.gcUnMappedLedgers.MainView = this.gvUnmappedLedgers;
            this.gcUnMappedLedgers.Name = "gcUnMappedLedgers";
            this.gcUnMappedLedgers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpExistingLedgers});
            this.gcUnMappedLedgers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUnmappedLedgers});
            // 
            // gvUnmappedLedgers
            // 
            this.gvUnmappedLedgers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colImportedLedgerId,
            this.colImportedLedgerCode,
            this.colImportedLedgerName,
            this.colImportedLedgerGroup,
            this.colExistingLedger,
            this.colExistingLedgerName});
            this.gvUnmappedLedgers.GridControl = this.gcUnMappedLedgers;
            this.gvUnmappedLedgers.Name = "gvUnmappedLedgers";
            this.gvUnmappedLedgers.OptionsView.ShowAutoFilterRow = true;
            this.gvUnmappedLedgers.OptionsView.ShowGroupPanel = false;
            this.gvUnmappedLedgers.OptionsView.ShowIndicator = false;
            this.gvUnmappedLedgers.ShownEditor += new System.EventHandler(this.gvUnmappedLedgers_ShownEditor);
            // 
            // colImportedLedgerId
            // 
            this.colImportedLedgerId.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colImportedLedgerId.AppearanceCell.Font")));
            this.colImportedLedgerId.AppearanceCell.Options.UseFont = true;
            resources.ApplyResources(this.colImportedLedgerId, "colImportedLedgerId");
            this.colImportedLedgerId.FieldName = "LEDGER_ID";
            this.colImportedLedgerId.Name = "colImportedLedgerId";
            // 
            // colImportedLedgerCode
            // 
            this.colImportedLedgerCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colImportedLedgerCode.AppearanceHeader.Font")));
            this.colImportedLedgerCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colImportedLedgerCode, "colImportedLedgerCode");
            this.colImportedLedgerCode.FieldName = "LEDGER_CODE";
            this.colImportedLedgerCode.Name = "colImportedLedgerCode";
            this.colImportedLedgerCode.OptionsColumn.AllowEdit = false;
            this.colImportedLedgerCode.OptionsColumn.AllowFocus = false;
            this.colImportedLedgerCode.OptionsColumn.AllowMove = false;
            this.colImportedLedgerCode.OptionsColumn.AllowSize = false;
            this.colImportedLedgerCode.OptionsColumn.ReadOnly = true;
            this.colImportedLedgerCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colImportedLedgerName
            // 
            this.colImportedLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colImportedLedgerName.AppearanceHeader.Font")));
            this.colImportedLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colImportedLedgerName, "colImportedLedgerName");
            this.colImportedLedgerName.FieldName = "LEDGER_NAME";
            this.colImportedLedgerName.Name = "colImportedLedgerName";
            this.colImportedLedgerName.OptionsColumn.AllowEdit = false;
            this.colImportedLedgerName.OptionsColumn.AllowFocus = false;
            this.colImportedLedgerName.OptionsColumn.AllowMove = false;
            this.colImportedLedgerName.OptionsColumn.AllowSize = false;
            this.colImportedLedgerName.OptionsColumn.ReadOnly = true;
            this.colImportedLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colImportedLedgerGroup
            // 
            this.colImportedLedgerGroup.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colImportedLedgerGroup.AppearanceHeader.Font")));
            this.colImportedLedgerGroup.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colImportedLedgerGroup, "colImportedLedgerGroup");
            this.colImportedLedgerGroup.FieldName = "LEDGER_GROUP";
            this.colImportedLedgerGroup.Name = "colImportedLedgerGroup";
            this.colImportedLedgerGroup.OptionsColumn.AllowEdit = false;
            this.colImportedLedgerGroup.OptionsColumn.AllowFocus = false;
            this.colImportedLedgerGroup.OptionsColumn.AllowMove = false;
            this.colImportedLedgerGroup.OptionsColumn.AllowSize = false;
            this.colImportedLedgerGroup.OptionsColumn.ReadOnly = true;
            this.colImportedLedgerGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colExistingLedger
            // 
            this.colExistingLedger.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colExistingLedger.AppearanceCell.BackColor")));
            this.colExistingLedger.AppearanceCell.Options.UseBackColor = true;
            this.colExistingLedger.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colExistingLedger.AppearanceHeader.Font")));
            this.colExistingLedger.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colExistingLedger, "colExistingLedger");
            this.colExistingLedger.ColumnEdit = this.rglkpExistingLedgers;
            this.colExistingLedger.FieldName = "MERGE_LEDGER_ID";
            this.colExistingLedger.Name = "colExistingLedger";
            this.colExistingLedger.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rglkpExistingLedgers
            // 
            resources.ApplyResources(this.rglkpExistingLedgers, "rglkpExistingLedgers");
            this.rglkpExistingLedgers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpExistingLedgers.Buttons"))))});
            this.rglkpExistingLedgers.ImmediatePopup = true;
            this.rglkpExistingLedgers.Name = "rglkpExistingLedgers";
            this.rglkpExistingLedgers.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D;
            this.rglkpExistingLedgers.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpExistingLedgers.PopupFormSize = new System.Drawing.Size(366, 0);
            this.rglkpExistingLedgers.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpExistingLedgers.View = this.repGrdListLedgerView;
            this.rglkpExistingLedgers.Validating += new System.ComponentModel.CancelEventHandler(this.rglkpExistingLedgers_Validating);
            // 
            // repGrdListLedgerView
            // 
            this.repGrdListLedgerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.collkpExistingLedgerId,
            this.collkpExistingLedgerName});
            this.repGrdListLedgerView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repGrdListLedgerView.Name = "repGrdListLedgerView";
            this.repGrdListLedgerView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repGrdListLedgerView.OptionsView.ShowColumnHeaders = false;
            this.repGrdListLedgerView.OptionsView.ShowGroupPanel = false;
            this.repGrdListLedgerView.OptionsView.ShowIndicator = false;
            // 
            // collkpExistingLedgerId
            // 
            resources.ApplyResources(this.collkpExistingLedgerId, "collkpExistingLedgerId");
            this.collkpExistingLedgerId.FieldName = "MERGE_LEDGER_ID";
            this.collkpExistingLedgerId.Name = "collkpExistingLedgerId";
            // 
            // collkpExistingLedgerName
            // 
            resources.ApplyResources(this.collkpExistingLedgerName, "collkpExistingLedgerName");
            this.collkpExistingLedgerName.FieldName = "MERGE_LEDGER_NAME";
            this.collkpExistingLedgerName.Name = "collkpExistingLedgerName";
            // 
            // colExistingLedgerName
            // 
            resources.ApplyResources(this.colExistingLedgerName, "colExistingLedgerName");
            this.colExistingLedgerName.FieldName = "MERGE_LEDGER_NAME";
            this.colExistingLedgerName.Name = "colExistingLedgerName";
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
            // btnMapLedgers
            // 
            resources.ApplyResources(this.btnMapLedgers, "btnMapLedgers");
            this.btnMapLedgers.Name = "btnMapLedgers";
            this.btnMapLedgers.StyleController = this.layoutControl1;
            this.btnMapLedgers.Click += new System.EventHandler(this.btnMapLedgers_Click);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem6,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1053, 488);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(267, 452);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(592, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnMapLedgers;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(859, 452);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(111, 26);
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
            this.layoutControlItem5.Size = new System.Drawing.Size(1043, 42);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcUnMappedLedgers;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1043, 410);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(970, 452);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(73, 26);
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
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 452);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(267, 26);
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(192, 14);
            // 
            // frmProjectImportMapLedgers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectImportMapLedgers";
            this.Load += new System.EventHandler(this.frmUnMappedLedgers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUnMappedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnmappedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpExistingLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repGrdListLedgerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton btnMapLedgers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcUnMappedLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUnmappedLedgers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colImportedLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colImportedLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colImportedLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colImportedLedgerGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colExistingLedger;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpExistingLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView repGrdListLedgerView;
        private DevExpress.XtraGrid.Columns.GridColumn collkpExistingLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn collkpExistingLedgerName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colExistingLedgerName;
    }
}