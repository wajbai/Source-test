namespace ACPP.Modules.ProspectsDonor
{
    partial class frmDonorList
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
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcDonorList = new DevExpress.XtraGrid.GridControl();
            this.gvDonorList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStateId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastAppeal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonAudId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.deDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkSelectAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDonorList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonorList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcDonorList);
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.deDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(594, 205, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(792, 430);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(7, 404);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(778, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 7;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcDonorList
            // 
            this.gcDonorList.Location = new System.Drawing.Point(7, 33);
            this.gcDonorList.MainView = this.gvDonorList;
            this.gcDonorList.Name = "gcDonorList";
            this.gcDonorList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkSelect});
            this.gcDonorList.Size = new System.Drawing.Size(778, 367);
            this.gcDonorList.TabIndex = 6;
            this.gcDonorList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDonorList});
            // 
            // gvDonorList
            // 
            this.gvDonorList.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvDonorList.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDonorList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvDonorList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDonorList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonor,
            this.colType,
            this.colCountryId,
            this.colStateId,
            this.colState,
            this.colCountry,
            this.colLastAppeal,
            this.colDonAudId,
            this.colSelect});
            this.gvDonorList.GridControl = this.gcDonorList;
            this.gvDonorList.Name = "gvDonorList";
            this.gvDonorList.OptionsFind.AllowFindPanel = false;
            this.gvDonorList.OptionsFind.ShowClearButton = false;
            this.gvDonorList.OptionsFind.ShowCloseButton = false;
            this.gvDonorList.OptionsFind.ShowFindButton = false;
            this.gvDonorList.OptionsView.ShowGroupPanel = false;
            this.gvDonorList.OptionsView.ShowIndicator = false;
            // 
            // colDonor
            // 
            this.colDonor.Caption = "Donor";
            this.colDonor.FieldName = "NAME";
            this.colDonor.Name = "colDonor";
            this.colDonor.Visible = true;
            this.colDonor.VisibleIndex = 1;
            this.colDonor.Width = 148;
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.FieldName = "TYPE";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 148;
            // 
            // colCountryId
            // 
            this.colCountryId.Caption = "Country Id";
            this.colCountryId.FieldName = "COUNTRY_ID";
            this.colCountryId.Name = "colCountryId";
            // 
            // colStateId
            // 
            this.colStateId.Caption = "State Id";
            this.colStateId.FieldName = "STATE_ID";
            this.colStateId.Name = "colStateId";
            // 
            // colState
            // 
            this.colState.Caption = "State";
            this.colState.FieldName = "STATE";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 3;
            this.colState.Width = 148;
            // 
            // colCountry
            // 
            this.colCountry.Caption = "Country";
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            this.colCountry.Visible = true;
            this.colCountry.VisibleIndex = 4;
            this.colCountry.Width = 148;
            // 
            // colLastAppeal
            // 
            this.colLastAppeal.Caption = "Last Appeal";
            this.colLastAppeal.FieldName = "APPEAL_SENT_DATE";
            this.colLastAppeal.Name = "colLastAppeal";
            this.colLastAppeal.Visible = true;
            this.colLastAppeal.VisibleIndex = 5;
            this.colLastAppeal.Width = 159;
            // 
            // colDonAudId
            // 
            this.colDonAudId.Caption = "Donor Id";
            this.colDonAudId.FieldName = "DONAUD_ID";
            this.colDonAudId.Name = "colDonAudId";
            // 
            // colSelect
            // 
            this.colSelect.Caption = "Select";
            this.colSelect.ColumnEdit = this.rchkSelect;
            this.colSelect.FieldName = "SELECT";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.FixedWidth = true;
            this.colSelect.OptionsColumn.ShowCaption = false;
            this.colSelect.OptionsFilter.AllowAutoFilter = false;
            this.colSelect.OptionsFilter.AllowFilter = false;
            this.colSelect.Visible = true;
            this.colSelect.VisibleIndex = 0;
            this.colSelect.Width = 25;
            // 
            // rchkSelect
            // 
            this.rchkSelect.AutoHeight = false;
            this.rchkSelect.Caption = "Check";
            this.rchkSelect.Name = "rchkSelect";
            this.rchkSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkSelect.ValueChecked = 1;
            this.rchkSelect.ValueGrayed = 2;
            this.rchkSelect.ValueUnchecked = 0;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(717, 7);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(68, 22);
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "&Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // deDate
            // 
            this.deDate.EditValue = null;
            this.deDate.Location = new System.Drawing.Point(585, 7);
            this.deDate.Name = "deDate";
            this.deDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deDate.Size = new System.Drawing.Size(128, 20);
            this.deDate.StyleController = this.layoutControl1;
            this.deDate.TabIndex = 4;
            this.deDate.EditValueChanged += new System.EventHandler(this.deDate_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(792, 430);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.deDate;
            this.layoutControlItem1.CustomizationFormText = "Date";
            this.layoutControlItem1.Location = new System.Drawing.Point(552, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(158, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(158, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Date";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(23, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnApply;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(710, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(72, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(72, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(552, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcDonorList;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(782, 371);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkShowFilter;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 397);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(782, 23);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Location = new System.Drawing.Point(12, 34);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Properties.Caption = "";
            this.chkSelectAll.Size = new System.Drawing.Size(20, 19);
            this.chkSelectAll.TabIndex = 34;
            // 
            // frmDonorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 430);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDonorList";
            this.Text = "frmDonorList";
            this.Load += new System.EventHandler(this.frmDonorList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDonorList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonorList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcDonorList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDonorList;
        private DevExpress.XtraGrid.Columns.GridColumn colDonor;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colLastAppeal;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.DateEdit deDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colStateId;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colDonAudId;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelect;
        private DevExpress.XtraEditors.CheckEdit chkSelectAll;
    }
}