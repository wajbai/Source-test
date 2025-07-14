namespace PAYROLL.Modules.Payroll_app
{
    partial class frmOrderComponents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderComponents));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDecrease = new DevExpress.XtraEditors.SimpleButton();
            this.btnIncrease = new DevExpress.XtraEditors.SimpleButton();
            this.gcComponent = new DevExpress.XtraGrid.GridControl();
            this.gvComponent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colComponentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDefValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpGroup = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnDecrease);
            this.layoutControl1.Controls.Add(this.btnIncrease);
            this.layoutControl1.Controls.Add(this.gcComponent);
            this.layoutControl1.Controls.Add(this.glkpGroup);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(942, 229, 250, 350);
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
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnClose.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnSave.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDecrease
            // 
            this.btnDecrease.Image = global::PAYROLL.Properties.Resources.arrow__down;
            resources.ApplyResources(this.btnDecrease, "btnDecrease");
            this.btnDecrease.Name = "btnDecrease";
            this.btnDecrease.StyleController = this.layoutControl1;
            this.btnDecrease.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnIncrease
            // 
            this.btnIncrease.Image = global::PAYROLL.Properties.Resources.arrow_up;
            resources.ApplyResources(this.btnIncrease, "btnIncrease");
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.StyleController = this.layoutControl1;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // gcComponent
            // 
            resources.ApplyResources(this.gcComponent, "gcComponent");
            this.gcComponent.MainView = this.gvComponent;
            this.gcComponent.Name = "gcComponent";
            this.gcComponent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvComponent});
            // 
            // gvComponent
            // 
            this.gvComponent.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvComponent.Appearance.FocusedRow.Font")));
            this.gvComponent.Appearance.FocusedRow.Options.UseFont = true;
            this.gvComponent.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvComponent.Appearance.HeaderPanel.Font")));
            this.gvComponent.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvComponent.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colComponentId,
            this.colComponent,
            this.colType,
            this.colDefValue,
            this.colLinkValue,
            this.colEquation,
            this.colMax});
            this.gvComponent.GridControl = this.gcComponent;
            this.gvComponent.Name = "gvComponent";
            this.gvComponent.OptionsBehavior.Editable = false;
            this.gvComponent.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvComponent.OptionsView.ShowGroupPanel = false;
            this.gvComponent.OptionsView.ShowIndicator = false;
            this.gvComponent.RowCountChanged += new System.EventHandler(this.gvComponent_RowCountChanged);
            // 
            // colComponentId
            // 
            resources.ApplyResources(this.colComponentId, "colComponentId");
            this.colComponentId.FieldName = "componentid";
            this.colComponentId.Name = "colComponentId";
            this.colComponentId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colComponent
            // 
            resources.ApplyResources(this.colComponent, "colComponent");
            this.colComponent.FieldName = "Component";
            this.colComponent.Name = "colComponent";
            this.colComponent.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colType
            // 
            resources.ApplyResources(this.colType, "colType");
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDefValue
            // 
            resources.ApplyResources(this.colDefValue, "colDefValue");
            this.colDefValue.FieldName = "Def.Value";
            this.colDefValue.Name = "colDefValue";
            this.colDefValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLinkValue
            // 
            resources.ApplyResources(this.colLinkValue, "colLinkValue");
            this.colLinkValue.FieldName = "Link Value";
            this.colLinkValue.Name = "colLinkValue";
            this.colLinkValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colEquation
            // 
            resources.ApplyResources(this.colEquation, "colEquation");
            this.colEquation.FieldName = "Equation";
            this.colEquation.Name = "colEquation";
            this.colEquation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colMax
            // 
            resources.ApplyResources(this.colMax, "colMax");
            this.colMax.FieldName = "Max Slab";
            this.colMax.Name = "colMax";
            this.colMax.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // glkpGroup
            // 
            resources.ApplyResources(this.glkpGroup, "glkpGroup");
            this.glkpGroup.Name = "glkpGroup";
            this.glkpGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpGroup.Properties.Buttons"))))});
            this.glkpGroup.Properties.NullText = resources.GetString("glkpGroup.Properties.NullText");
            this.glkpGroup.Properties.PopupFormMinSize = new System.Drawing.Size(223, 0);
            this.glkpGroup.Properties.PopupFormSize = new System.Drawing.Size(223, 0);
            this.glkpGroup.Properties.View = this.gridLookUpEdit1View;
            this.glkpGroup.StyleController = this.layoutControl1;
            this.glkpGroup.EditValueChanged += new System.EventHandler(this.glkpGroup_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGroupId,
            this.colGroupName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colGroupId
            // 
            resources.ApplyResources(this.colGroupId, "colGroupId");
            this.colGroupId.FieldName = "GROUP ID";
            this.colGroupId.Name = "colGroupId";
            // 
            // colGroupName
            // 
            resources.ApplyResources(this.colGroupName, "colGroupName");
            this.colGroupName.FieldName = "Group Name";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(38, 353);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(703, 23);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(703, 23);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(703, 23);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
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
            this.emptySpaceItem3,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.simpleLabelItem3,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.lblRecordCount,
            this.simpleLabelItem2,
            this.emptySpaceItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(798, 424);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem1.AppearanceItemCaption.Font")));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.glkpGroup;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(38, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(270, 30);
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(34, 13);
            this.layoutControlItem1.TextToControlDistance = 3;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcComponent;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(38, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(740, 323);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(38, 114);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(38, 114);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(38, 114);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 189);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(38, 215);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(38, 215);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(38, 215);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnIncrease;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 114);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(38, 41);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(38, 41);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(38, 41);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnDecrease;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 155);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(38, 34);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(38, 34);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(38, 34);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(308, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(470, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem3.AppearanceItemCaption.Font")));
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem3, "simpleLabelItem3");
            this.simpleLabelItem3.Image = global::PAYROLL.Properties.Resources.info;
            this.simpleLabelItem3.Location = new System.Drawing.Point(38, 376);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(602, 28);
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(292, 24);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(640, 376);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(709, 376);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.AppearanceItemCaption.Font")));
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(758, 353);
            this.lblRecordCount.MaxSize = new System.Drawing.Size(20, 23);
            this.lblRecordCount.MinSize = new System.Drawing.Size(20, 23);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(20, 23);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRecordCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font")));
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(745, 353);
            this.simpleLabelItem2.MaxSize = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(38, 353);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(707, 23);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmOrderComponents
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrderComponents";
            this.Load += new System.EventHandler(this.frmOrderComponents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.GridLookUpEdit glkpGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcComponent;
        private DevExpress.XtraGrid.Views.Grid.GridView gvComponent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colComponentId;
        private DevExpress.XtraGrid.Columns.GridColumn colComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colDefValue;
        private DevExpress.XtraGrid.Columns.GridColumn colLinkValue;
        private DevExpress.XtraGrid.Columns.GridColumn colEquation;
        private DevExpress.XtraGrid.Columns.GridColumn colMax;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SimpleButton btnDecrease;
        private DevExpress.XtraEditors.SimpleButton btnIncrease;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
    }
}