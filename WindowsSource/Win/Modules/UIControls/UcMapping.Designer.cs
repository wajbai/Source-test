namespace ACPP.Modules.UIControls
{
    partial class UcMapping
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gvProjectName = new DevExpress.XtraGrid.GridControl();
            this.gvProjectDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOPBal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOPLedgerBal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtOPLedgerBal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvColProjectType = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtOPLedgerBal)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup6.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup6.CustomizationFormText = "Selected Project(s)";
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 29);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup6.Size = new System.Drawing.Size(619, 362);
            this.layoutControlGroup6.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup6.Text = "Selected Project(s)";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.CustomizationFormText = "Selected Project(s)";
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 29);
            this.layoutControlGroup1.Name = "layoutControlGroup6";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(619, 362);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Text = "Selected Project(s)";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.CustomizationFormText = "Selected Project(s)";
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlGroup2.Name = "layoutControlGroup6";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup2.Size = new System.Drawing.Size(619, 362);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup2.Text = "Selected Project(s)";
            // 
            // gvProjectName
            // 
            this.gvProjectName.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gvProjectName.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gvProjectName.Location = new System.Drawing.Point(0, 0);
            this.gvProjectName.MainView = this.gvProjectDetails;
            this.gvProjectName.Name = "gvProjectName";
            this.gvProjectName.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtOPLedgerBal});
            this.gvProjectName.Size = new System.Drawing.Size(376, 307);
            this.gvProjectName.TabIndex = 6;
            this.gvProjectName.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProjectDetails});
            // 
            // gvProjectDetails
            // 
            this.gvProjectDetails.Appearance.FocusedCell.BackColor = System.Drawing.Color.Lavender;
            this.gvProjectDetails.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvProjectDetails.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvProjectDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvProjectDetails.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvProjectDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvProjectDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvProjectDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colSelect,
            this.colProjectName,
            this.colOPBal,
            this.colOPLedgerBal,
            this.gvColProjectType});
            this.gvProjectDetails.GridControl = this.gvProjectName;
            this.gvProjectDetails.Name = "gvProjectDetails";
            this.gvProjectDetails.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvProjectDetails.OptionsView.ShowGroupPanel = false;
            this.gvProjectDetails.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            this.colProjectId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProjectId.AppearanceHeader.Options.UseFont = true;
            this.colProjectId.Caption = "ProjectId";
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            this.colProjectId.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            // 
            // colSelect
            // 
            this.colSelect.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSelect.AppearanceHeader.Options.UseFont = true;
            this.colSelect.Caption = "Select";
            this.colSelect.FieldName = "SELECT";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSelect.OptionsColumn.ShowCaption = false;
            this.colSelect.OptionsFilter.AllowAutoFilter = false;
            this.colSelect.OptionsFilter.AllowFilter = false;
            this.colSelect.Visible = true;
            this.colSelect.VisibleIndex = 0;
            this.colSelect.Width = 20;
            // 
            // colProjectName
            // 
            this.colProjectName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProjectName.AppearanceHeader.Options.UseFont = true;
            this.colProjectName.Caption = "Project";
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowEdit = false;
            this.colProjectName.OptionsColumn.AllowFocus = false;
            this.colProjectName.Visible = true;
            this.colProjectName.VisibleIndex = 1;
            this.colProjectName.Width = 218;
            // 
            // colOPBal
            // 
            this.colOPBal.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colOPBal.AppearanceCell.Options.UseBackColor = true;
            this.colOPBal.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colOPBal.AppearanceHeader.Options.UseFont = true;
            this.colOPBal.Caption = "O/P Balance";
            this.colOPBal.DisplayFormat.FormatString = "N";
            this.colOPBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOPBal.FieldName = "AMOUNT";
            this.colOPBal.Name = "colOPBal";
            this.colOPBal.Width = 99;
            // 
            // colOPLedgerBal
            // 
            this.colOPLedgerBal.Caption = "O/P Balance";
            this.colOPLedgerBal.ColumnEdit = this.rtxtOPLedgerBal;
            this.colOPLedgerBal.FieldName = "AMOUNT";
            this.colOPLedgerBal.Name = "colOPLedgerBal";
            this.colOPLedgerBal.Visible = true;
            this.colOPLedgerBal.VisibleIndex = 2;
            this.colOPLedgerBal.Width = 101;
            // 
            // rtxtOPLedgerBal
            // 
            this.rtxtOPLedgerBal.AutoHeight = false;
            this.rtxtOPLedgerBal.Mask.EditMask = "n";
            this.rtxtOPLedgerBal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtOPLedgerBal.MaxLength = 13;
            this.rtxtOPLedgerBal.Name = "rtxtOPLedgerBal";
            // 
            // gvColProjectType
            // 
            this.gvColProjectType.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.gvColProjectType.AppearanceCell.Options.UseBackColor = true;
            this.gvColProjectType.Caption = "Type";
            this.gvColProjectType.FieldName = "TRANS_MODE";
            this.gvColProjectType.Name = "gvColProjectType";
            this.gvColProjectType.OptionsColumn.ShowCaption = false;
            this.gvColProjectType.Visible = true;
            this.gvColProjectType.VisibleIndex = 3;
            this.gvColProjectType.Width = 40;
            // 
            // UcMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gvProjectName);
            this.Name = "UcMapping";
            this.Size = new System.Drawing.Size(376, 307);
            this.Load += new System.EventHandler(this.UcMapping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtOPLedgerBal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.GridControl gvProjectName;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProjectDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colOPBal;
        private DevExpress.XtraGrid.Columns.GridColumn colOPLedgerBal;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtOPLedgerBal;
        private DevExpress.XtraGrid.Columns.GridColumn gvColProjectType;
    }
}
