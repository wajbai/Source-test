namespace PAYROLL.Modules.Payroll_app
{
   public partial class frmComponenttoGroupWise
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComponenttoGroupWise));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcGroups = new DevExpress.XtraGrid.GridControl();
            this.gvGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblComponents = new DevExpress.XtraEditors.LabelControl();
            this.glkpComponent = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.glkpGroups = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpComponent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcGroups);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.lblComponents);
            this.layoutControl1.Controls.Add(this.glkpComponent);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(520, 199, 250, 350);
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
            // gcGroups
            // 
            resources.ApplyResources(this.gcGroups, "gcGroups");
            this.gcGroups.MainView = this.gvGroups;
            this.gcGroups.Name = "gcGroups";
            this.gcGroups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchSelect});
            this.gcGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGroups});
            this.gcGroups.Click += new System.EventHandler(this.gcGroups_Click);
            // 
            // gvGroups
            // 
            this.gvGroups.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvGroups.Appearance.FocusedRow.Font")));
            this.gvGroups.Appearance.FocusedRow.Options.UseFont = true;
            this.gvGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGroupId,
            this.colGroupName,
            this.colSelect});
            this.gvGroups.GridControl = this.gcGroups;
            this.gvGroups.Name = "gvGroups";
            this.gvGroups.OptionsBehavior.Editable = false;
            this.gvGroups.OptionsSelection.MultiSelect = true;
            this.gvGroups.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvGroups.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvGroups.OptionsView.ShowGroupPanel = false;
            this.gvGroups.OptionsView.ShowIndicator = false;
            this.gvGroups.RowCountChanged += new System.EventHandler(this.gvGroups_RowCountChanged);
            // 
            // colGroupId
            // 
            resources.ApplyResources(this.colGroupId, "colGroupId");
            this.colGroupId.FieldName = "GROUP ID";
            this.colGroupId.Name = "colGroupId";
            // 
            // colGroupName
            // 
            this.colGroupName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colGroupName.AppearanceHeader.Font")));
            this.colGroupName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colGroupName, "colGroupName");
            this.colGroupName.FieldName = "Group Name";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colSelect
            // 
            this.colSelect.ColumnEdit = this.rchSelect;
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.AllowSize = false;
            resources.ApplyResources(this.colSelect, "colSelect");
            // 
            // rchSelect
            // 
            resources.ApplyResources(this.rchSelect, "rchSelect");
            this.rchSelect.Name = "rchSelect";
            this.rchSelect.ValueChecked = 1;
            this.rchSelect.ValueUnchecked = 0;
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblComponents
            // 
            this.lblComponents.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblComponents.Appearance.Font")));
            resources.ApplyResources(this.lblComponents, "lblComponents");
            this.lblComponents.Name = "lblComponents";
            this.lblComponents.StyleController = this.layoutControl1;
            // 
            // glkpComponent
            // 
            resources.ApplyResources(this.glkpComponent, "glkpComponent");
            this.glkpComponent.Name = "glkpComponent";
            this.glkpComponent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpComponent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpComponent.Properties.Buttons"))))});
            this.glkpComponent.Properties.NullText = resources.GetString("glkpComponent.Properties.NullText");
            this.glkpComponent.Properties.PopupFormMinSize = new System.Drawing.Size(183, 0);
            this.glkpComponent.Properties.PopupFormSize = new System.Drawing.Size(183, 10);
            this.glkpComponent.Properties.View = this.gridLookUpEdit1View;
            this.glkpComponent.StyleController = this.layoutControl1;
            this.glkpComponent.EditValueChanged += new System.EventHandler(this.glkpComponent_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.glkpGroups,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem6,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.lblRowCount,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(421, 323);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // glkpGroups
            // 
            this.glkpGroups.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("glkpGroups.AppearanceItemCaption.Font")));
            this.glkpGroups.AppearanceItemCaption.Options.UseFont = true;
            this.glkpGroups.Control = this.glkpComponent;
            resources.ApplyResources(this.glkpGroups, "glkpGroups");
            this.glkpGroups.Location = new System.Drawing.Point(0, 0);
            this.glkpGroups.MinSize = new System.Drawing.Size(119, 30);
            this.glkpGroups.Name = "glkpGroups";
            this.glkpGroups.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.glkpGroups.Size = new System.Drawing.Size(421, 30);
            this.glkpGroups.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.glkpGroups.TextSize = new System.Drawing.Size(65, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(352, 297);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(283, 297);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem6, "emptySpaceItem6");
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 297);
            this.emptySpaceItem6.MaxSize = new System.Drawing.Size(283, 26);
            this.emptySpaceItem6.MinSize = new System.Drawing.Size(283, 26);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(283, 26);
            this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lblComponents;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(421, 17);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcGroups;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 47);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(421, 227);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(82, 274);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(307, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 274);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(82, 23);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AllowHotTrack = false;
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Location = new System.Drawing.Point(405, 274);
            this.lblRowCount.MaxSize = new System.Drawing.Size(16, 23);
            this.lblRowCount.MinSize = new System.Drawing.Size(16, 23);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(16, 23);
            this.lblRowCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRowCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRowCount.TextSize = new System.Drawing.Size(6, 13);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(389, 274);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(16, 23);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(16, 23);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(16, 23);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(8, 13);
            // 
            // frmComponenttoGroupWise
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComponenttoGroupWise";
            this.Load += new System.EventHandler(this.frmComponenttoGroupWise_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpComponent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.GridLookUpEdit glkpComponent;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem glkpGroups;
        private DevExpress.XtraEditors.LabelControl lblComponents;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcGroups;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGroups;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchSelect;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;

    }
}