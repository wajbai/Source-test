namespace ACPP.Modules.Dsync
{
    partial class frmUnMappedLedgers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnMappedLedgers));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gcUnMappedLedgers = new DevExpress.XtraGrid.GridControl();
            this.gvUnmappedLedgers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.capHeadOfficeLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpHOLedgers = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHeadOfficeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHeadOfficeName = new DevExpress.XtraGrid.Columns.GridColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.rglkpHOLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
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
            this.rglkpHOLedgers});
            this.gcUnMappedLedgers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUnmappedLedgers});
            // 
            // gvUnmappedLedgers
            // 
            this.gvUnmappedLedgers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colLedgerCode,
            this.colLedgerName,
            this.colProject,
            this.colLedgerGroup,
            this.capHeadOfficeLedger});
            this.gvUnmappedLedgers.GridControl = this.gcUnMappedLedgers;
            this.gvUnmappedLedgers.Name = "gvUnmappedLedgers";
            this.gvUnmappedLedgers.OptionsView.ShowAutoFilterRow = true;
            this.gvUnmappedLedgers.OptionsView.ShowGroupPanel = false;
            this.gvUnmappedLedgers.OptionsView.ShowIndicator = false;
            this.gvUnmappedLedgers.ShownEditor += new System.EventHandler(this.gvUnmappedLedgers_ShownEditor);
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
            // colProject
            // 
            this.colProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProject.AppearanceHeader.Font")));
            this.colProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            this.colProject.OptionsColumn.AllowFocus = false;
            this.colProject.OptionsColumn.AllowMove = false;
            this.colProject.OptionsColumn.AllowSize = false;
            this.colProject.OptionsColumn.ReadOnly = true;
            this.colProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
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
            // capHeadOfficeLedger
            // 
            this.capHeadOfficeLedger.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("capHeadOfficeLedger.AppearanceCell.BackColor")));
            this.capHeadOfficeLedger.AppearanceCell.Options.UseBackColor = true;
            this.capHeadOfficeLedger.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("capHeadOfficeLedger.AppearanceHeader.Font")));
            this.capHeadOfficeLedger.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.capHeadOfficeLedger, "capHeadOfficeLedger");
            this.capHeadOfficeLedger.ColumnEdit = this.rglkpHOLedgers;
            this.capHeadOfficeLedger.FieldName = "HEADOFFICE_LEDGER_ID";
            this.capHeadOfficeLedger.Name = "capHeadOfficeLedger";
            this.capHeadOfficeLedger.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rglkpHOLedgers
            // 
            this.rglkpHOLedgers.ActionButtonIndex = 1;
            resources.ApplyResources(this.rglkpHOLedgers, "rglkpHOLedgers");
            this.rglkpHOLedgers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpHOLedgers.Buttons"))), resources.GetString("rglkpHOLedgers.Buttons1"), ((int)(resources.GetObject("rglkpHOLedgers.Buttons2"))), ((bool)(resources.GetObject("rglkpHOLedgers.Buttons3"))), ((bool)(resources.GetObject("rglkpHOLedgers.Buttons4"))), ((bool)(resources.GetObject("rglkpHOLedgers.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rglkpHOLedgers.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("rglkpHOLedgers.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rglkpHOLedgers.Buttons8"), ((object)(resources.GetObject("rglkpHOLedgers.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rglkpHOLedgers.Buttons10"))), ((bool)(resources.GetObject("rglkpHOLedgers.Buttons11")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpHOLedgers.Buttons12"))))});
            this.rglkpHOLedgers.ImmediatePopup = true;
            this.rglkpHOLedgers.Name = "rglkpHOLedgers";
            this.rglkpHOLedgers.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D;
            this.rglkpHOLedgers.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpHOLedgers.PopupFormSize = new System.Drawing.Size(366, 0);
            this.rglkpHOLedgers.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpHOLedgers.View = this.repositoryItemGridLookUpEdit1View;
            this.rglkpHOLedgers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rglkpHOLedgers_ButtonClick);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHeadOfficeId,
            this.colHeadOfficeName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colHeadOfficeId
            // 
            resources.ApplyResources(this.colHeadOfficeId, "colHeadOfficeId");
            this.colHeadOfficeId.FieldName = "HEADOFFICE_LEDGER_ID";
            this.colHeadOfficeId.Name = "colHeadOfficeId";
            // 
            // colHeadOfficeName
            // 
            resources.ApplyResources(this.colHeadOfficeName, "colHeadOfficeName");
            this.colHeadOfficeName.FieldName = "HEADOFFICELEDGER";
            this.colHeadOfficeName.Name = "colHeadOfficeName";
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
            this.btnMapLedgers.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(984, 488);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(250, 452);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(553, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnMapLedgers;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(803, 452);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(103, 26);
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
            this.layoutControlItem5.Size = new System.Drawing.Size(974, 42);
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
            this.layoutControlItem1.Size = new System.Drawing.Size(974, 410);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(906, 452);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(68, 26);
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
            this.simpleLabelItem1.Size = new System.Drawing.Size(250, 26);
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(192, 14);
            // 
            // frmUnMappedLedgers
            // 
            this.AcceptButton = this.btnMapLedgers;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUnMappedLedgers";
            this.Load += new System.EventHandler(this.frmUnMappedLedgers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUnMappedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnmappedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpHOLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerGroup;
        private DevExpress.XtraGrid.Columns.GridColumn capHeadOfficeLedger;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpHOLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colHeadOfficeId;
        private DevExpress.XtraGrid.Columns.GridColumn colHeadOfficeName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
    }
}