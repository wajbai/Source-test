namespace ACPP.Modules.Master
{
    partial class frmAuditingInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditingInfo));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpAuditTypeData = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAuditTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AuditType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpAuditor = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtAuditedOn = new DevExpress.XtraEditors.DateEdit();
            this.txtMemoNotes = new DevExpress.XtraEditors.MemoEdit();
            this.dtAuditEnd = new DevExpress.XtraEditors.DateEdit();
            this.dtAuditBegin = new DevExpress.XtraEditors.DateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblNote = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblStartedOn = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblClosedON = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAuditor = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAuditTypeValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpAuditTypeData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpAuditor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditedOn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditedOn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditBegin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartedOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClosedON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuditTypeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.glkpAuditTypeData);
            this.layoutControl1.Controls.Add(this.glkpAuditor);
            this.layoutControl1.Controls.Add(this.glkProject);
            this.layoutControl1.Controls.Add(this.dtAuditedOn);
            this.layoutControl1.Controls.Add(this.txtMemoNotes);
            this.layoutControl1.Controls.Add(this.dtAuditEnd);
            this.layoutControl1.Controls.Add(this.dtAuditBegin);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnClose);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(539, 194, 446, 527);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // glkpAuditTypeData
            // 
            resources.ApplyResources(this.glkpAuditTypeData, "glkpAuditTypeData");
            this.glkpAuditTypeData.Name = "glkpAuditTypeData";
            this.glkpAuditTypeData.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpAuditTypeData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpAuditTypeData.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpAuditTypeData.Properties.Buttons1"))), resources.GetString("glkpAuditTypeData.Properties.Buttons2"), ((int)(resources.GetObject("glkpAuditTypeData.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpAuditTypeData.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpAuditTypeData.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpAuditTypeData.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpAuditTypeData.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpAuditTypeData.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("glkpAuditTypeData.Properties.Buttons9"), ((object)(resources.GetObject("glkpAuditTypeData.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpAuditTypeData.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpAuditTypeData.Properties.Buttons12"))))});
            this.glkpAuditTypeData.Properties.ImmediatePopup = true;
            this.glkpAuditTypeData.Properties.NullText = resources.GetString("glkpAuditTypeData.Properties.NullText");
            this.glkpAuditTypeData.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpAuditTypeData.Properties.PopupFormSize = new System.Drawing.Size(140, 0);
            this.glkpAuditTypeData.Properties.View = this.gridView3;
            this.glkpAuditTypeData.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpAuditType_Properties_ButtonClick_1);
            this.glkpAuditTypeData.StyleController = this.layoutControl1;
            this.glkpAuditTypeData.Leave += new System.EventHandler(this.glkpAuditTypeData_Leave);
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAuditTypeId,
            this.AuditType});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowColumnHeaders = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // colAuditTypeId
            // 
            resources.ApplyResources(this.colAuditTypeId, "colAuditTypeId");
            this.colAuditTypeId.FieldName = "AUDIT_TYPE_ID";
            this.colAuditTypeId.Name = "colAuditTypeId";
            // 
            // AuditType
            // 
            resources.ApplyResources(this.AuditType, "AuditType");
            this.AuditType.FieldName = "AUDIT_TYPE";
            this.AuditType.Name = "AuditType";
            // 
            // glkpAuditor
            // 
            resources.ApplyResources(this.glkpAuditor, "glkpAuditor");
            this.glkpAuditor.Name = "glkpAuditor";
            this.glkpAuditor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpAuditor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpAuditor.Properties.Buttons"))), resources.GetString("glkpAuditor.Properties.Buttons1"), ((int)(resources.GetObject("glkpAuditor.Properties.Buttons2"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpAuditor.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("glkpAuditor.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("glkpAuditor.Properties.Buttons8"), ((object)(resources.GetObject("glkpAuditor.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpAuditor.Properties.Buttons10"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons11")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpAuditor.Properties.Buttons12"))), resources.GetString("glkpAuditor.Properties.Buttons13"), ((int)(resources.GetObject("glkpAuditor.Properties.Buttons14"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons15"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons16"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons17"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpAuditor.Properties.Buttons18"))), ((System.Drawing.Image)(resources.GetObject("glkpAuditor.Properties.Buttons19"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("glkpAuditor.Properties.Buttons20"), ((object)(resources.GetObject("glkpAuditor.Properties.Buttons21"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpAuditor.Properties.Buttons22"))), ((bool)(resources.GetObject("glkpAuditor.Properties.Buttons23"))))});
            this.glkpAuditor.Properties.ImmediatePopup = true;
            this.glkpAuditor.Properties.NullText = resources.GetString("glkpAuditor.Properties.NullText");
            this.glkpAuditor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpAuditor.Properties.PopupFormMinSize = new System.Drawing.Size(327, 0);
            this.glkpAuditor.Properties.PopupFormSize = new System.Drawing.Size(327, 0);
            this.glkpAuditor.Properties.View = this.gridView2;
            this.glkpAuditor.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpAuditor_Properties_ButtonClick);
            this.glkpAuditor.StyleController = this.layoutControl1;
            this.glkpAuditor.Leave += new System.EventHandler(this.glkpAuditor_Leave);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAuditor,
            this.colName});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowColumnHeaders = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // colAuditor
            // 
            resources.ApplyResources(this.colAuditor, "colAuditor");
            this.colAuditor.FieldName = "DONAUD_ID";
            this.colAuditor.Name = "colAuditor";
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            // 
            // glkProject
            // 
            resources.ApplyResources(this.glkProject, "glkProject");
            this.glkProject.Name = "glkProject";
            this.glkProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkProject.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkProject.Properties.Buttons1"))), resources.GetString("glkProject.Properties.Buttons2"), ((int)(resources.GetObject("glkProject.Properties.Buttons3"))), ((bool)(resources.GetObject("glkProject.Properties.Buttons4"))), ((bool)(resources.GetObject("glkProject.Properties.Buttons5"))), ((bool)(resources.GetObject("glkProject.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkProject.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkProject.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, resources.GetString("glkProject.Properties.Buttons9"), ((object)(resources.GetObject("glkProject.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkProject.Properties.Buttons11"))), ((bool)(resources.GetObject("glkProject.Properties.Buttons12"))))});
            this.glkProject.Properties.NullText = resources.GetString("glkProject.Properties.NullText");
            this.glkProject.Properties.PopupFormMinSize = new System.Drawing.Size(327, 0);
            this.glkProject.Properties.PopupFormSize = new System.Drawing.Size(327, 0);
            this.glkProject.Properties.View = this.gridLookUpEdit1View;
            this.glkProject.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkProject_Properties_ButtonClick);
            this.glkProject.StyleController = this.layoutControl1;
            this.glkProject.Tag = "PR";
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "PROJECT_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "PROJECT";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // dtAuditedOn
            // 
            resources.ApplyResources(this.dtAuditedOn, "dtAuditedOn");
            this.dtAuditedOn.Name = "dtAuditedOn";
            this.dtAuditedOn.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtAuditedOn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtAuditedOn.Properties.Buttons"))))});
            this.dtAuditedOn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtAuditedOn.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dtAuditedOn.Properties.Mask.MaskType")));
            this.dtAuditedOn.StyleController = this.layoutControl1;
            // 
            // txtMemoNotes
            // 
            resources.ApplyResources(this.txtMemoNotes, "txtMemoNotes");
            this.txtMemoNotes.Name = "txtMemoNotes";
            this.txtMemoNotes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtMemoNotes.Properties.MaxLength = 500;
            this.txtMemoNotes.StyleController = this.layoutControl1;
            this.txtMemoNotes.UseOptimizedRendering = true;
            // 
            // dtAuditEnd
            // 
            resources.ApplyResources(this.dtAuditEnd, "dtAuditEnd");
            this.dtAuditEnd.Name = "dtAuditEnd";
            this.dtAuditEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtAuditEnd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtAuditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtAuditEnd.Properties.Buttons"))))});
            this.dtAuditEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtAuditEnd.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dtAuditEnd.Properties.Mask.MaskType")));
            this.dtAuditEnd.StyleController = this.layoutControl1;
            // 
            // dtAuditBegin
            // 
            resources.ApplyResources(this.dtAuditBegin, "dtAuditBegin");
            this.dtAuditBegin.Name = "dtAuditBegin";
            this.dtAuditBegin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtAuditBegin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtAuditBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtAuditBegin.Properties.Buttons"))))});
            this.dtAuditBegin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtAuditBegin.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dtAuditBegin.Properties.Mask.MaskType")));
            this.dtAuditBegin.StyleController = this.layoutControl1;
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
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem1,
            this.lblNote,
            this.lblStartedOn,
            this.lblClosedON,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.lblAuditor,
            this.lblAuditTypeValue,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(434, 168);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(367, 142);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem6.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(298, 142);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 142);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(298, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblNote
            // 
            this.lblNote.Control = this.txtMemoNotes;
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.Location = new System.Drawing.Point(0, 91);
            this.lblNote.Name = "lblNote";
            this.lblNote.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblNote.Size = new System.Drawing.Size(434, 51);
            this.lblNote.TextSize = new System.Drawing.Size(61, 13);
            // 
            // lblStartedOn
            // 
            this.lblStartedOn.AllowHtmlStringInCaption = true;
            this.lblStartedOn.Control = this.dtAuditBegin;
            resources.ApplyResources(this.lblStartedOn, "lblStartedOn");
            this.lblStartedOn.Location = new System.Drawing.Point(0, 22);
            this.lblStartedOn.Name = "lblStartedOn";
            this.lblStartedOn.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblStartedOn.Size = new System.Drawing.Size(211, 23);
            this.lblStartedOn.TextSize = new System.Drawing.Size(61, 13);
            // 
            // lblClosedON
            // 
            this.lblClosedON.AllowHtmlStringInCaption = true;
            this.lblClosedON.Control = this.dtAuditEnd;
            resources.ApplyResources(this.lblClosedON, "lblClosedON");
            this.lblClosedON.Location = new System.Drawing.Point(222, 22);
            this.lblClosedON.Name = "lblClosedON";
            this.lblClosedON.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblClosedON.Size = new System.Drawing.Size(212, 20);
            this.lblClosedON.TextSize = new System.Drawing.Size(61, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dtAuditedOn;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 45);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem1.Size = new System.Drawing.Size(211, 23);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(61, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AllowHtmlStringInCaption = true;
            this.layoutControlItem2.Control = this.glkProject;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 2);
            this.layoutControlItem2.Size = new System.Drawing.Size(434, 22);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(61, 13);
            // 
            // lblAuditor
            // 
            this.lblAuditor.AllowHtmlStringInCaption = true;
            this.lblAuditor.Control = this.glkpAuditor;
            resources.ApplyResources(this.lblAuditor, "lblAuditor");
            this.lblAuditor.Location = new System.Drawing.Point(0, 68);
            this.lblAuditor.Name = "lblAuditor";
            this.lblAuditor.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblAuditor.Size = new System.Drawing.Size(434, 23);
            this.lblAuditor.TextSize = new System.Drawing.Size(61, 13);
            // 
            // lblAuditTypeValue
            // 
            this.lblAuditTypeValue.AllowHtmlStringInCaption = true;
            this.lblAuditTypeValue.Control = this.glkpAuditTypeData;
            resources.ApplyResources(this.lblAuditTypeValue, "lblAuditTypeValue");
            this.lblAuditTypeValue.Location = new System.Drawing.Point(222, 42);
            this.lblAuditTypeValue.MinSize = new System.Drawing.Size(114, 23);
            this.lblAuditTypeValue.Name = "lblAuditTypeValue";
            this.lblAuditTypeValue.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.lblAuditTypeValue.Size = new System.Drawing.Size(212, 26);
            this.lblAuditTypeValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAuditTypeValue.TextSize = new System.Drawing.Size(61, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(211, 22);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(11, 46);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmAuditingInfo
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAuditingInfo";
            this.Load += new System.EventHandler(this.frmAuditingInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpAuditTypeData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpAuditor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditedOn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditedOn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditBegin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAuditBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartedOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClosedON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuditTypeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.DateEdit dtAuditEnd;
        private DevExpress.XtraEditors.DateEdit dtAuditBegin;
        private DevExpress.XtraLayout.LayoutControlItem lblStartedOn;
        private DevExpress.XtraLayout.LayoutControlItem lblClosedON;
        private DevExpress.XtraEditors.MemoEdit txtMemoNotes;
        private DevExpress.XtraLayout.LayoutControlItem lblNote;
        private DevExpress.XtraEditors.DateEdit dtAuditedOn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.GridLookUpEdit glkProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpAuditor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlItem lblAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.GridLookUpEdit glkpAuditTypeData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraLayout.LayoutControlItem lblAuditTypeValue;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn AuditType;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;

    }
}