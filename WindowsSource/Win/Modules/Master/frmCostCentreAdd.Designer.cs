namespace ACPP.Modules.Master
{
    partial class frmCostCentreAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCostCentreAdd));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lcCostCentre = new DevExpress.XtraLayout.LayoutControl();
            this.ucCostcentre = new ACPP.Modules.UIControls.UcUsedCodesIcon();
            this.glkpCostcentreCategory = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCostCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCebtreCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UcMappingCostCentre = new ACPP.Modules.UIControls.UcAccountMapping();
            this.txtmeNotes = new DevExpress.XtraEditors.MemoEdit();
            this.glkpAvailableCostCentreCodes = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtCostCentreName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCostCentreNew = new DevExpress.XtraEditors.SimpleButton();
            this.glkpExistingCostCentreCodes = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgCostCentre = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCostCentreName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNotes = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCostCenterCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcCostCentre)).BeginInit();
            this.lcCostCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCostcentreCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpAvailableCostCentreCodes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostCentreName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpExistingCostCentreCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCostCentreName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCostCenterCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcCostCentre;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lcCostCentre
            // 
            this.lcCostCentre.AllowCustomizationMenu = false;
            this.lcCostCentre.Controls.Add(this.ucCostcentre);
            this.lcCostCentre.Controls.Add(this.glkpCostcentreCategory);
            this.lcCostCentre.Controls.Add(this.UcMappingCostCentre);
            this.lcCostCentre.Controls.Add(this.txtmeNotes);
            this.lcCostCentre.Controls.Add(this.glkpAvailableCostCentreCodes);
            this.lcCostCentre.Controls.Add(this.txtCostCentreName);
            this.lcCostCentre.Controls.Add(this.txtCode);
            this.lcCostCentre.Controls.Add(this.btnSave);
            this.lcCostCentre.Controls.Add(this.btnClose);
            this.lcCostCentre.Controls.Add(this.btnCostCentreNew);
            resources.ApplyResources(this.lcCostCentre, "lcCostCentre");
            this.lcCostCentre.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.glkpExistingCostCentreCodes});
            this.lcCostCentre.Name = "lcCostCentre";
            this.lcCostCentre.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(717, 196, 250, 350);
            this.lcCostCentre.OptionsView.UseDefaultDragAndDropRendering = false;
            this.lcCostCentre.Root = this.lcgCostCentre;
            // 
            // ucCostcentre
            // 
            this.ucCostcentre.ExistUsedCode = "";
            resources.ApplyResources(this.ucCostcentre, "ucCostcentre");
            this.ucCostcentre.Name = "ucCostcentre";
            this.ucCostcentre.TabStop = false;
            // 
            // glkpCostcentreCategory
            // 
            resources.ApplyResources(this.glkpCostcentreCategory, "glkpCostcentreCategory");
            this.glkpCostcentreCategory.Name = "glkpCostcentreCategory";
            this.glkpCostcentreCategory.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCostcentreCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCostcentreCategory.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCostcentreCategory.Properties.Buttons1"))))});
            this.glkpCostcentreCategory.Properties.NullText = resources.GetString("glkpCostcentreCategory.Properties.NullText");
            this.glkpCostcentreCategory.Properties.PopupFormSize = new System.Drawing.Size(285, 60);
            this.glkpCostcentreCategory.Properties.View = this.gridView1;
            this.glkpCostcentreCategory.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpCostcentreCategory_Properties_ButtonClick);
            this.glkpCostcentreCategory.StyleController = this.lcCostCentre;
            this.glkpCostcentreCategory.EditValueChanged += new System.EventHandler(this.glkpCostcentreCategory_EditValueChanged);
            this.glkpCostcentreCategory.Leave += new System.EventHandler(this.glkpCostcentreCategory_Leave);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCostCategoryId,
            this.colCostCebtreCategoryName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colCostCategoryId
            // 
            resources.ApplyResources(this.colCostCategoryId, "colCostCategoryId");
            this.colCostCategoryId.FieldName = "COST_CENTRECATEGORY_ID";
            this.colCostCategoryId.Name = "colCostCategoryId";
            this.colCostCategoryId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCostCebtreCategoryName
            // 
            resources.ApplyResources(this.colCostCebtreCategoryName, "colCostCebtreCategoryName");
            this.colCostCebtreCategoryName.FieldName = "COST_CENTRE_CATEGORY_NAME";
            this.colCostCebtreCategoryName.Name = "colCostCebtreCategoryName";
            this.colCostCebtreCategoryName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // UcMappingCostCentre
            // 
            this.UcMappingCostCentre.FDAccountID = 0;
            this.UcMappingCostCentre.FDLedgerSubType = Bosco.Utility.ledgerSubType.CA;
            this.UcMappingCostCentre.FormType = Bosco.Utility.MapForm.Ledger;
            this.UcMappingCostCentre.Id = 0;
            resources.ApplyResources(this.UcMappingCostCentre, "UcMappingCostCentre");
            this.UcMappingCostCentre.Name = "UcMappingCostCentre";
            this.UcMappingCostCentre.ProjectId = 0;
            this.UcMappingCostCentre.RefreshGrid = false;
            this.UcMappingCostCentre.ProcessGridKey += new System.EventHandler(this.UcMappingCostCentre_ProcessGridKey);
            // 
            // txtmeNotes
            // 
            resources.ApplyResources(this.txtmeNotes, "txtmeNotes");
            this.txtmeNotes.Name = "txtmeNotes";
            this.txtmeNotes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtmeNotes.Properties.MaxLength = 500;
            this.txtmeNotes.StyleController = this.lcCostCentre;
            this.txtmeNotes.UseOptimizedRendering = true;
            // 
            // glkpAvailableCostCentreCodes
            // 
            resources.ApplyResources(this.glkpAvailableCostCentreCodes, "glkpAvailableCostCentreCodes");
            this.glkpAvailableCostCentreCodes.Name = "glkpAvailableCostCentreCodes";
            this.glkpAvailableCostCentreCodes.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("glkpAvailableCostCentreCodes.Properties.Appearance.Font")));
            this.glkpAvailableCostCentreCodes.Properties.Appearance.Options.UseFont = true;
            this.glkpAvailableCostCentreCodes.Properties.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("glkpAvailableCostCentreCodes.Properties.AppearanceFocused.Font")));
            this.glkpAvailableCostCentreCodes.Properties.AppearanceFocused.Options.UseFont = true;
            this.glkpAvailableCostCentreCodes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpAvailableCostCentreCodes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpAvailableCostCentreCodes.Properties.Buttons"))))});
            this.glkpAvailableCostCentreCodes.Properties.NullText = resources.GetString("glkpAvailableCostCentreCodes.Properties.NullText");
            this.glkpAvailableCostCentreCodes.Properties.PopupFormMinSize = new System.Drawing.Size(95, 0);
            this.glkpAvailableCostCentreCodes.Properties.PopupFormSize = new System.Drawing.Size(50, 40);
            this.glkpAvailableCostCentreCodes.Properties.View = this.gridLookUpEdit1View;
            this.glkpAvailableCostCentreCodes.StyleController = this.lcCostCentre;
            this.glkpAvailableCostCentreCodes.TabStop = false;
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
            // txtCostCentreName
            // 
            resources.ApplyResources(this.txtCostCentreName, "txtCostCentreName");
            this.txtCostCentreName.Name = "txtCostCentreName";
            this.txtCostCentreName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCostCentreName.Properties.MaxLength = 50;
            this.txtCostCentreName.StyleController = this.lcCostCentre;
            this.txtCostCentreName.Leave += new System.EventHandler(this.txtCostCentreName_Leave);
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCode.Properties.MaxLength = 5;
            this.txtCode.StyleController = this.lcCostCentre;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcCostCentre;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCostCentreNew
            // 
            resources.ApplyResources(this.btnCostCentreNew, "btnCostCentreNew");
            this.btnCostCentreNew.Name = "btnCostCentreNew";
            this.btnCostCentreNew.StyleController = this.lcCostCentre;
            this.btnCostCentreNew.Click += new System.EventHandler(this.btnCostCentreNew_Click);
            // 
            // glkpExistingCostCentreCodes
            // 
            this.glkpExistingCostCentreCodes.Control = this.glkpAvailableCostCentreCodes;
            resources.ApplyResources(this.glkpExistingCostCentreCodes, "glkpExistingCostCentreCodes");
            this.glkpExistingCostCentreCodes.Location = new System.Drawing.Point(305, 0);
            this.glkpExistingCostCentreCodes.Name = "glkpExistingCostCentreCodes";
            this.glkpExistingCostCentreCodes.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.glkpExistingCostCentreCodes.Size = new System.Drawing.Size(107, 26);
            this.glkpExistingCostCentreCodes.TextSize = new System.Drawing.Size(0, 0);
            this.glkpExistingCostCentreCodes.TextToControlDistance = 0;
            this.glkpExistingCostCentreCodes.TextVisible = false;
            // 
            // lcgCostCentre
            // 
            resources.ApplyResources(this.lcgCostCentre, "lcgCostCentre");
            this.lcgCostCentre.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgCostCentre.GroupBordersVisible = false;
            this.lcgCostCentre.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblCostCentreName,
            this.layoutControlItem3,
            this.lblNotes,
            this.emptySpaceItem1,
            this.lblCostCenterCode,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.lcgCostCentre.Location = new System.Drawing.Point(0, 0);
            this.lcgCostCentre.Name = "Root";
            this.lcgCostCentre.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgCostCentre.Size = new System.Drawing.Size(412, 396);
            this.lcgCostCentre.TextVisible = false;
            // 
            // lblCostCentreName
            // 
            this.lblCostCentreName.AllowHtmlStringInCaption = true;
            this.lblCostCentreName.Control = this.txtCostCentreName;
            resources.ApplyResources(this.lblCostCentreName, "lblCostCentreName");
            this.lblCostCentreName.Location = new System.Drawing.Point(0, 52);
            this.lblCostCentreName.Name = "lblCostCentreName";
            this.lblCostCentreName.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 3, 3);
            this.lblCostCentreName.Size = new System.Drawing.Size(412, 29);
            this.lblCostCentreName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCostCentreName.TextSize = new System.Drawing.Size(118, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.UcMappingCostCentre;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 81);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(412, 224);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblNotes
            // 
            this.lblNotes.Control = this.txtmeNotes;
            resources.ApplyResources(this.lblNotes, "lblNotes");
            this.lblNotes.Location = new System.Drawing.Point(0, 305);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 3);
            this.lblNotes.Size = new System.Drawing.Size(412, 65);
            this.lblNotes.TextSize = new System.Drawing.Size(118, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(177, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.emptySpaceItem1.Size = new System.Drawing.Size(16, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCostCenterCode
            // 
            this.lblCostCenterCode.AllowHtmlStringInCaption = true;
            this.lblCostCenterCode.Control = this.txtCode;
            resources.ApplyResources(this.lblCostCenterCode, "lblCostCenterCode");
            this.lblCostCenterCode.Location = new System.Drawing.Point(0, 0);
            this.lblCostCenterCode.Name = "lblCostCenterCode";
            this.lblCostCenterCode.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lblCostCenterCode.Size = new System.Drawing.Size(177, 26);
            this.lblCostCenterCode.TextSize = new System.Drawing.Size(118, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 370);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(198, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(342, 370);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCostCentreNew;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(270, 370);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(198, 370);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AllowHtmlStringInCaption = true;
            this.layoutControlItem5.Control = this.glkpCostcentreCategory;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem5.Size = new System.Drawing.Size(412, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(118, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ucCostcentre;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(193, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Size = new System.Drawing.Size(219, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // frmCostCentreAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcCostCentre);
            this.Name = "frmCostCentreAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCostCentreAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmCostCentreAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcCostCentre)).EndInit();
            this.lcCostCentre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpCostcentreCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpAvailableCostCentreCodes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostCentreName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpExistingCostCentreCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCostCentreName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCostCenterCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcCostCentre;
        private DevExpress.XtraLayout.LayoutControlGroup lcgCostCentre;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlItem lblCostCenterCode;
        private DevExpress.XtraEditors.TextEdit txtCostCentreName;
        private DevExpress.XtraLayout.LayoutControlItem lblCostCentreName;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.MemoEdit txtmeNotes;
        private DevExpress.XtraLayout.LayoutControlItem lblNotes;
        private DevExpress.XtraEditors.SimpleButton btnCostCentreNew;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.GridLookUpEdit glkpAvailableCostCentreCodes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem glkpExistingCostCentreCodes;
        private UIControls.UcAccountMapping UcMappingCostCentre;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCostcentreCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCebtreCategoryName;
        private UIControls.UcUsedCodesIcon ucCostcentre;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;


    }
}