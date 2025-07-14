namespace ACPP.Modules.Data_Utility
{
    partial class frmMapMigration
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.bandedgcMappedLedgers = new DevExpress.XtraGrid.GridControl();
            this.bandedgvMappedLedgers = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.grdBandSource = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSLedger = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.grdBandAcMEERP = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colAId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colACode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.glkpLedgerCode = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.glkpColId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpColCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpColLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpColGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colALedger = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.glkpLedger = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.glkprepColId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkprepLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkprepLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkprepGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnSkip = new DevExpress.XtraEditors.SimpleButton();
            this.btnMap = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bandedgcMappedLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedgvMappedLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedgerCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.bandedgcMappedLedgers);
            this.layoutControl1.Controls.Add(this.btnSkip);
            this.layoutControl1.Controls.Add(this.btnMap);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(384, 140, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1043, 526);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // bandedgcMappedLedgers
            // 
            this.bandedgcMappedLedgers.Location = new System.Drawing.Point(7, 7);
            this.bandedgcMappedLedgers.MainView = this.bandedgvMappedLedgers;
            this.bandedgcMappedLedgers.Name = "bandedgcMappedLedgers";
            this.bandedgcMappedLedgers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.glkpLedgerCode,
            this.glkpLedger});
            this.bandedgcMappedLedgers.Size = new System.Drawing.Size(1029, 486);
            this.bandedgcMappedLedgers.TabIndex = 6;
            this.bandedgcMappedLedgers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedgvMappedLedgers});
            this.bandedgcMappedLedgers.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.bandedgcMappedLedgers_ProcessGridKey);
            // 
            // bandedgvMappedLedgers
            // 
            this.bandedgvMappedLedgers.BandPanelRowHeight = 30;
            this.bandedgvMappedLedgers.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.grdBandSource,
            this.grdBandAcMEERP});
            this.bandedgvMappedLedgers.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSId,
            this.colSCode,
            this.colSLedger,
            this.colSGroup,
            this.colAId,
            this.colACode,
            this.colALedger,
            this.colAGroup});
            this.bandedgvMappedLedgers.GridControl = this.bandedgcMappedLedgers;
            this.bandedgvMappedLedgers.Name = "bandedgvMappedLedgers";
            this.bandedgvMappedLedgers.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            this.bandedgvMappedLedgers.OptionsNavigation.UseOfficePageNavigation = false;
            this.bandedgvMappedLedgers.OptionsNavigation.UseTabKey = false;
            this.bandedgvMappedLedgers.OptionsView.ShowGroupPanel = false;
            this.bandedgvMappedLedgers.GotFocus += new System.EventHandler(this.bandedgvMappedLedgers_GotFocus);
            // 
            // grdBandSource
            // 
            this.grdBandSource.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.grdBandSource.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.grdBandSource.AppearanceHeader.Options.UseBackColor = true;
            this.grdBandSource.AppearanceHeader.Options.UseFont = true;
            this.grdBandSource.AppearanceHeader.Options.UseTextOptions = true;
            this.grdBandSource.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdBandSource.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grdBandSource.Caption = "Source Ledgers";
            this.grdBandSource.Columns.Add(this.colSId);
            this.grdBandSource.Columns.Add(this.colSCode);
            this.grdBandSource.Columns.Add(this.colSLedger);
            this.grdBandSource.Columns.Add(this.colSGroup);
            this.grdBandSource.Name = "grdBandSource";
            this.grdBandSource.VisibleIndex = 0;
            this.grdBandSource.Width = 343;
            // 
            // colSId
            // 
            this.colSId.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro;
            this.colSId.AppearanceCell.Options.UseBackColor = true;
            this.colSId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSId.AppearanceHeader.Options.UseFont = true;
            this.colSId.Caption = "Id";
            this.colSId.FieldName = "LedgerId";
            this.colSId.Name = "colSId";
            this.colSId.OptionsColumn.AllowEdit = false;
            this.colSId.OptionsColumn.AllowFocus = false;
            this.colSId.OptionsColumn.ReadOnly = true;
            this.colSId.Width = 32;
            // 
            // colSCode
            // 
            this.colSCode.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro;
            this.colSCode.AppearanceCell.Options.UseBackColor = true;
            this.colSCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSCode.AppearanceHeader.Options.UseFont = true;
            this.colSCode.Caption = "Code";
            this.colSCode.FieldName = "LedgerCode";
            this.colSCode.Name = "colSCode";
            this.colSCode.OptionsColumn.AllowEdit = false;
            this.colSCode.OptionsColumn.AllowFocus = false;
            this.colSCode.OptionsColumn.ReadOnly = true;
            this.colSCode.Visible = true;
            this.colSCode.Width = 111;
            // 
            // colSLedger
            // 
            this.colSLedger.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro;
            this.colSLedger.AppearanceCell.Options.UseBackColor = true;
            this.colSLedger.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSLedger.AppearanceHeader.Options.UseFont = true;
            this.colSLedger.Caption = "Ledger";
            this.colSLedger.FieldName = "LedgerName";
            this.colSLedger.Name = "colSLedger";
            this.colSLedger.OptionsColumn.AllowEdit = false;
            this.colSLedger.OptionsColumn.AllowFocus = false;
            this.colSLedger.OptionsColumn.ReadOnly = true;
            this.colSLedger.Visible = true;
            this.colSLedger.Width = 111;
            // 
            // colSGroup
            // 
            this.colSGroup.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro;
            this.colSGroup.AppearanceCell.Options.UseBackColor = true;
            this.colSGroup.AppearanceHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.colSGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSGroup.AppearanceHeader.Options.UseBackColor = true;
            this.colSGroup.AppearanceHeader.Options.UseFont = true;
            this.colSGroup.Caption = "Group";
            this.colSGroup.FieldName = "Parent";
            this.colSGroup.Name = "colSGroup";
            this.colSGroup.OptionsColumn.AllowEdit = false;
            this.colSGroup.OptionsColumn.AllowFocus = false;
            this.colSGroup.OptionsColumn.ReadOnly = true;
            this.colSGroup.Visible = true;
            this.colSGroup.Width = 121;
            // 
            // grdBandAcMEERP
            // 
            this.grdBandAcMEERP.AppearanceHeader.BackColor = System.Drawing.Color.DarkGray;
            this.grdBandAcMEERP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.grdBandAcMEERP.AppearanceHeader.Options.UseBackColor = true;
            this.grdBandAcMEERP.AppearanceHeader.Options.UseFont = true;
            this.grdBandAcMEERP.AppearanceHeader.Options.UseTextOptions = true;
            this.grdBandAcMEERP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdBandAcMEERP.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grdBandAcMEERP.Caption = "Acme.erp Ledgers";
            this.grdBandAcMEERP.Columns.Add(this.colAId);
            this.grdBandAcMEERP.Columns.Add(this.colACode);
            this.grdBandAcMEERP.Columns.Add(this.colALedger);
            this.grdBandAcMEERP.Columns.Add(this.colAGroup);
            this.grdBandAcMEERP.Name = "grdBandAcMEERP";
            this.grdBandAcMEERP.VisibleIndex = 1;
            this.grdBandAcMEERP.Width = 542;
            // 
            // colAId
            // 
            this.colAId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAId.AppearanceHeader.Options.UseFont = true;
            this.colAId.Caption = "Id";
            this.colAId.FieldName = "LEDGER_ID";
            this.colAId.Name = "colAId";
            this.colAId.OptionsColumn.ReadOnly = true;
            this.colAId.Width = 95;
            // 
            // colACode
            // 
            this.colACode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colACode.AppearanceHeader.Options.UseFont = true;
            this.colACode.Caption = "Code";
            this.colACode.ColumnEdit = this.glkpLedgerCode;
            this.colACode.FieldName = "Ledger_Code";
            this.colACode.Name = "colACode";
            this.colACode.Visible = true;
            this.colACode.Width = 100;
            // 
            // glkpLedgerCode
            // 
            this.glkpLedgerCode.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.glkpLedgerCode.AutoHeight = false;
            this.glkpLedgerCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpLedgerCode.ImmediatePopup = true;
            this.glkpLedgerCode.Name = "glkpLedgerCode";
            this.glkpLedgerCode.NullText = "";
            this.glkpLedgerCode.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpLedgerCode.PopupFormSize = new System.Drawing.Size(600, 0);
            this.glkpLedgerCode.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpLedgerCode.View = this.repositoryItemGridLookUpEdit1View;
            this.glkpLedgerCode.EditValueChanged += new System.EventHandler(this.glkpLedgerCode_EditValueChanged);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.glkpColId,
            this.glkpColCode,
            this.glkpColLedger,
            this.glkpColGroup});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // glkpColId
            // 
            this.glkpColId.Caption = "Id";
            this.glkpColId.FieldName = "LEDGER_ID";
            this.glkpColId.Name = "glkpColId";
            // 
            // glkpColCode
            // 
            this.glkpColCode.Caption = "Code";
            this.glkpColCode.FieldName = "LEDGER_CODE";
            this.glkpColCode.Name = "glkpColCode";
            this.glkpColCode.Visible = true;
            this.glkpColCode.VisibleIndex = 0;
            // 
            // glkpColLedger
            // 
            this.glkpColLedger.Caption = "Ledger";
            this.glkpColLedger.FieldName = "LEDGER_NAME";
            this.glkpColLedger.Name = "glkpColLedger";
            this.glkpColLedger.Visible = true;
            this.glkpColLedger.VisibleIndex = 1;
            // 
            // glkpColGroup
            // 
            this.glkpColGroup.Caption = "Group";
            this.glkpColGroup.FieldName = "GROUP";
            this.glkpColGroup.Name = "glkpColGroup";
            this.glkpColGroup.Visible = true;
            this.glkpColGroup.VisibleIndex = 2;
            // 
            // colALedger
            // 
            this.colALedger.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colALedger.AppearanceHeader.Options.UseFont = true;
            this.colALedger.Caption = "Ledger";
            this.colALedger.ColumnEdit = this.glkpLedger;
            this.colALedger.FieldName = "Ledger_Name";
            this.colALedger.Name = "colALedger";
            this.colALedger.Visible = true;
            this.colALedger.Width = 320;
            // 
            // glkpLedger
            // 
            this.glkpLedger.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.glkpLedger.AutoHeight = false;
            this.glkpLedger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpLedger.ImmediatePopup = true;
            this.glkpLedger.Name = "glkpLedger";
            this.glkpLedger.NullText = "";
            this.glkpLedger.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpLedger.PopupFormSize = new System.Drawing.Size(500, 0);
            this.glkpLedger.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpLedger.View = this.gridView1;
            this.glkpLedger.EditValueChanged += new System.EventHandler(this.glkpLedger_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.glkprepColId,
            this.glkprepLedgerCode,
            this.glkprepLedger,
            this.glkprepGroup});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // glkprepColId
            // 
            this.glkprepColId.Caption = "Id";
            this.glkprepColId.FieldName = "LEDGER_ID";
            this.glkprepColId.Name = "glkprepColId";
            // 
            // glkprepLedgerCode
            // 
            this.glkprepLedgerCode.Caption = "Code";
            this.glkprepLedgerCode.FieldName = "LEDGER_CODE";
            this.glkprepLedgerCode.Name = "glkprepLedgerCode";
            this.glkprepLedgerCode.Visible = true;
            this.glkprepLedgerCode.VisibleIndex = 0;
            // 
            // glkprepLedger
            // 
            this.glkprepLedger.Caption = "Ledger";
            this.glkprepLedger.FieldName = "LEDGER_NAME";
            this.glkprepLedger.Name = "glkprepLedger";
            this.glkprepLedger.Visible = true;
            this.glkprepLedger.VisibleIndex = 1;
            // 
            // glkprepGroup
            // 
            this.glkprepGroup.Caption = "Group";
            this.glkprepGroup.FieldName = "GROUP";
            this.glkprepGroup.Name = "glkprepGroup";
            this.glkprepGroup.Visible = true;
            this.glkprepGroup.VisibleIndex = 2;
            // 
            // colAGroup
            // 
            this.colAGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAGroup.AppearanceHeader.Options.UseFont = true;
            this.colAGroup.Caption = "Group";
            this.colAGroup.FieldName = "Group";
            this.colAGroup.Name = "colAGroup";
            this.colAGroup.OptionsColumn.AllowEdit = false;
            this.colAGroup.Visible = true;
            this.colAGroup.Width = 122;
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(958, 497);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(78, 22);
            this.btnSkip.StyleController = this.layoutControl1;
            this.btnSkip.TabIndex = 5;
            this.btnSkip.Text = "&Skip";
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(876, 497);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(78, 22);
            this.btnMap.StyleController = this.layoutControl1;
            this.btnMap.TabIndex = 4;
            this.btnMap.Text = "&Map";
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1043, 526);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnMap;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(869, 490);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSkip;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(951, 490);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(82, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bandedgcMappedLedgers;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1033, 490);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 490);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(869, 26);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(869, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(869, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmMapMigration
            // 
            this.AcceptButton = this.btnMap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 536);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmMapMigration";
            this.Text = "Migration Mapping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMapMigration_FormClosing);
            this.Load += new System.EventHandler(this.frmMapMigration_Load);
            this.Shown += new System.EventHandler(this.frmMapMigration_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bandedgcMappedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedgvMappedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedgerCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnSkip;
        private DevExpress.XtraEditors.SimpleButton btnMap;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl bandedgcMappedLedgers;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkpLedgerCode;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn glkpColId;
        private DevExpress.XtraGrid.Columns.GridColumn glkpColCode;
        private DevExpress.XtraGrid.Columns.GridColumn glkpColLedger;
        private DevExpress.XtraGrid.Columns.GridColumn glkpColGroup;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkpLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn glkprepColId;
        private DevExpress.XtraGrid.Columns.GridColumn glkprepLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn glkprepLedger;
        private DevExpress.XtraGrid.Columns.GridColumn glkprepGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedgvMappedLedgers;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand grdBandSource;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSLedger;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSGroup;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand grdBandAcMEERP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAId;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colACode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colALedger;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAGroup;
    }
}