namespace ACPP.Modules.Data_Utility
{
    partial class frmManageMultiDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageMultiDB));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcMultiBranch = new DevExpress.XtraGrid.GridControl();
            this.gvMultiBranch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDBName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtDBName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDatabasename = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLicensekey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDBName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcMultiBranch);
            this.layoutControl1.Controls.Add(this.btnClose);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(751, 82, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // gcMultiBranch
            // 
            resources.ApplyResources(this.gcMultiBranch, "gcMultiBranch");
            this.gcMultiBranch.MainView = this.gvMultiBranch;
            this.gcMultiBranch.Name = "gcMultiBranch";
            this.gcMultiBranch.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rbtDelete,
            this.rtxtDBName});
            this.gcMultiBranch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMultiBranch});
            // 
            // gvMultiBranch
            // 
            this.gvMultiBranch.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMultiBranch.Appearance.FocusedRow.Font")));
            this.gvMultiBranch.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMultiBranch.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDBName,
            this.colDatabasename,
            this.colLicensekey,
            this.colDelete});
            this.gvMultiBranch.GridControl = this.gcMultiBranch;
            this.gvMultiBranch.Name = "gvMultiBranch";
            this.gvMultiBranch.OptionsFind.ShowFindButton = false;
            this.gvMultiBranch.OptionsView.ShowGroupPanel = false;
            this.gvMultiBranch.OptionsView.ShowIndicator = false;
            this.gvMultiBranch.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDBName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvMultiBranch.BeforePrintRow += new DevExpress.XtraGrid.Views.Base.BeforePrintRowEventHandler(this.gvMultiBranch_BeforePrintRow);
            this.gvMultiBranch.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvMultiBranch_ValidateRow);
            this.gvMultiBranch.LostFocus += new System.EventHandler(this.gvMultiBranch_LostFocus);
            this.gvMultiBranch.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gvMultiBranch_ValidatingEditor);
            this.gvMultiBranch.RowCountChanged += new System.EventHandler(this.gvMultiBranch_RowCountChanged);
            // 
            // colDBName
            // 
            this.colDBName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDBName.AppearanceHeader.Font")));
            this.colDBName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDBName, "colDBName");
            this.colDBName.ColumnEdit = this.rtxtDBName;
            this.colDBName.FieldName = "RestoreDBName";
            this.colDBName.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colDBName.Name = "colDBName";
            this.colDBName.OptionsColumn.AllowSize = false;
            this.colDBName.OptionsColumn.FixedWidth = true;
            this.colDBName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rtxtDBName
            // 
            resources.ApplyResources(this.rtxtDBName, "rtxtDBName");
            this.rtxtDBName.MaxLength = 40;
            this.rtxtDBName.Name = "rtxtDBName";
            // 
            // colDatabasename
            // 
            this.colDatabasename.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDatabasename.AppearanceHeader.Font")));
            this.colDatabasename.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDatabasename, "colDatabasename");
            this.colDatabasename.FieldName = "Restore_Db";
            this.colDatabasename.Name = "colDatabasename";
            this.colDatabasename.OptionsColumn.AllowEdit = false;
            // 
            // colLicensekey
            // 
            this.colLicensekey.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLicensekey.AppearanceHeader.Font")));
            this.colLicensekey.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLicensekey, "colLicensekey");
            this.colLicensekey.FieldName = "MultipleLicenseKey";
            this.colLicensekey.Name = "colLicensekey";
            // 
            // colDelete
            // 
            this.colDelete.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDelete.AppearanceHeader.Font")));
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDelete, "colDelete");
            this.colDelete.ColumnEdit = this.rbtDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowSize = false;
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.OptionsColumn.TabStop = false;
            this.colDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtDelete
            // 
            resources.ApplyResources(this.rbtDelete, "rbtDelete");
            this.rbtDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtDelete.Buttons"))), resources.GetString("rbtDelete.Buttons1"), ((int)(resources.GetObject("rbtDelete.Buttons2"))), ((bool)(resources.GetObject("rbtDelete.Buttons3"))), ((bool)(resources.GetObject("rbtDelete.Buttons4"))), ((bool)(resources.GetObject("rbtDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtDelete.Buttons6"))), global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbtDelete.Buttons7"), ((object)(resources.GetObject("rbtDelete.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtDelete.Buttons9"))), ((bool)(resources.GetObject("rbtDelete.Buttons10"))))});
            this.rbtDelete.Name = "rbtDelete";
            this.rbtDelete.Click += new System.EventHandler(this.rbtDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.lblRecordCount,
            this.simpleLabelItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(474, 246);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMultiBranch;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(474, 201);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 201);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(81, 19);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(81, 19);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(81, 19);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(81, 201);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Size = new System.Drawing.Size(346, 19);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.AppearanceItemCaption.Font")));
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(440, 201);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblRecordCount.Size = new System.Drawing.Size(34, 19);
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font")));
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(427, 201);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.simpleLabelItem2.Size = new System.Drawing.Size(13, 19);
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(399, 220);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(75, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 220);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(325, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(325, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(325, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(325, 220);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(74, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmManageMultiDB
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmManageMultiDB";
            this.ShowFilterClicked += new System.EventHandler(this.frmManageMultiDB_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmManageMultiDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDBName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcMultiBranch;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMultiBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colDBName;
        private DevExpress.XtraGrid.Columns.GridColumn colDatabasename;
        private DevExpress.XtraGrid.Columns.GridColumn colLicensekey;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtDelete;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtDBName;
    }
}