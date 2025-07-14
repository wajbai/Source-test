namespace ACPP.Modules.Master
{
    partial class frmAuditLockTransAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditLockTransAdd));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtPasswordHint = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.meReason = new DevExpress.XtraEditors.MemoEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.glkpLockType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLockId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLockType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deDateTo = new DevExpress.XtraEditors.DateEdit();
            this.deDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgTrans = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblLockType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblReason = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblPasswordHint = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordHint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLockType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLockType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPasswordHint)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtPasswordHint);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.meReason);
            this.layoutControl1.Controls.Add(this.txtPassword);
            this.layoutControl1.Controls.Add(this.glkpLockType);
            this.layoutControl1.Controls.Add(this.deDateTo);
            this.layoutControl1.Controls.Add(this.deDateFrom);
            this.layoutControl1.Controls.Add(this.glkpProject);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(288, 136, 250, 350);
            this.layoutControl1.Root = this.lcgTrans;
            // 
            // txtPasswordHint
            // 
            resources.ApplyResources(this.txtPasswordHint, "txtPasswordHint");
            this.txtPasswordHint.Name = "txtPasswordHint";
            this.txtPasswordHint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPasswordHint.Properties.MaxLength = 40;
            this.txtPasswordHint.StyleController = this.layoutControl1;
            this.txtPasswordHint.Leave += new System.EventHandler(this.txtPasswordHint_Leave);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // meReason
            // 
            resources.ApplyResources(this.meReason, "meReason");
            this.meReason.Name = "meReason";
            this.meReason.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.meReason.Properties.MaxLength = 500;
            this.meReason.StyleController = this.layoutControl1;
            this.meReason.UseOptimizedRendering = true;
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPassword.Properties.MaxLength = 100;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.StyleController = this.layoutControl1;
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // glkpLockType
            // 
            resources.ApplyResources(this.glkpLockType, "glkpLockType");
            this.glkpLockType.Name = "glkpLockType";
            this.glkpLockType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpLockType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpLockType.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpLockType.Properties.Buttons1"))), resources.GetString("glkpLockType.Properties.Buttons2"), ((int)(resources.GetObject("glkpLockType.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpLockType.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpLockType.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpLockType.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpLockType.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpLockType.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("glkpLockType.Properties.Buttons9"), ((object)(resources.GetObject("glkpLockType.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpLockType.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpLockType.Properties.Buttons12"))))});
            this.glkpLockType.Properties.ImmediatePopup = true;
            this.glkpLockType.Properties.NullText = resources.GetString("glkpLockType.Properties.NullText");
            this.glkpLockType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpLockType.Properties.View = this.gridView1;
            this.glkpLockType.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpLockType_Properties_ButtonClick);
            this.glkpLockType.StyleController = this.layoutControl1;
            this.glkpLockType.Leave += new System.EventHandler(this.glkpLockType_Leave);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLockId,
            this.colLockType});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colLockId
            // 
            resources.ApplyResources(this.colLockId, "colLockId");
            this.colLockId.FieldName = "LOCK_TYPE_ID";
            this.colLockId.Name = "colLockId";
            // 
            // colLockType
            // 
            resources.ApplyResources(this.colLockType, "colLockType");
            this.colLockType.FieldName = "LOCK_TYPE";
            this.colLockType.Name = "colLockType";
            // 
            // deDateTo
            // 
            resources.ApplyResources(this.deDateTo, "deDateTo");
            this.deDateTo.Name = "deDateTo";
            this.deDateTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.Buttons"))))});
            this.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.CalendarTimeProperties.Buttons"))))});
            this.deDateTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateTo.Properties.Mask.MaskType")));
            this.deDateTo.StyleController = this.layoutControl1;
            this.deDateTo.Leave += new System.EventHandler(this.deDateTo_Leave);
            // 
            // deDateFrom
            // 
            resources.ApplyResources(this.deDateFrom, "deDateFrom");
            this.deDateFrom.Name = "deDateFrom";
            this.deDateFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateFrom.Properties.Buttons"))))});
            this.deDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateFrom.Properties.CalendarTimeProperties.Buttons"))))});
            this.deDateFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateFrom.Properties.Mask.MaskType")));
            this.deDateFrom.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("deDateFrom.Properties.Mask.UseMaskAsDisplayFormat")));
            this.deDateFrom.StyleController = this.layoutControl1;
            this.deDateFrom.Leave += new System.EventHandler(this.deDateFrom_Leave);
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons1"))), resources.GetString("glkpProject.Properties.Buttons2"), ((int)(resources.GetObject("glkpProject.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpProject.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpProject.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpProject.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpProject.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpProject.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("glkpProject.Properties.Buttons9"), ((object)(resources.GetObject("glkpProject.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpProject.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpProject.Properties.Buttons12"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpProject_Properties_ButtonClick);
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.Tag = "PR";
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            this.glkpProject.Click += new System.EventHandler(this.glkpProject_Click);
            this.glkpProject.Leave += new System.EventHandler(this.glkpProject_Leave);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProject});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            // 
            // lcgTrans
            // 
            resources.ApplyResources(this.lcgTrans, "lcgTrans");
            this.lcgTrans.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgTrans.GroupBordersVisible = false;
            this.lcgTrans.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject,
            this.lblDateFrom,
            this.lblDateTo,
            this.emptySpaceItem2,
            this.lblLockType,
            this.lblPassword,
            this.lblReason,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem3,
            this.lblPasswordHint});
            this.lcgTrans.Location = new System.Drawing.Point(0, 0);
            this.lcgTrans.Name = "Root";
            this.lcgTrans.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgTrans.Size = new System.Drawing.Size(445, 197);
            this.lcgTrans.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AllowHtmlStringInCaption = true;
            this.lblProject.Control = this.glkpProject;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(445, 24);
            this.lblProject.TextSize = new System.Drawing.Size(59, 13);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AllowHtmlStringInCaption = true;
            this.lblDateFrom.Control = this.deDateFrom;
            resources.ApplyResources(this.lblDateFrom, "lblDateFrom");
            this.lblDateFrom.Location = new System.Drawing.Point(0, 24);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(179, 24);
            this.lblDateFrom.TextSize = new System.Drawing.Size(59, 13);
            // 
            // lblDateTo
            // 
            this.lblDateTo.AllowHtmlStringInCaption = true;
            this.lblDateTo.Control = this.deDateTo;
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Location = new System.Drawing.Point(249, 24);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(196, 24);
            this.lblDateTo.TextSize = new System.Drawing.Size(59, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(179, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(70, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblLockType
            // 
            this.lblLockType.AllowHtmlStringInCaption = true;
            this.lblLockType.Control = this.glkpLockType;
            resources.ApplyResources(this.lblLockType, "lblLockType");
            this.lblLockType.Location = new System.Drawing.Point(0, 48);
            this.lblLockType.Name = "lblLockType";
            this.lblLockType.Size = new System.Drawing.Size(445, 24);
            this.lblLockType.TextSize = new System.Drawing.Size(59, 13);
            // 
            // lblPassword
            // 
            this.lblPassword.AllowHtmlStringInCaption = true;
            this.lblPassword.Control = this.txtPassword;
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Location = new System.Drawing.Point(0, 72);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(248, 24);
            this.lblPassword.TextSize = new System.Drawing.Size(59, 13);
            // 
            // lblReason
            // 
            this.lblReason.Control = this.meReason;
            resources.ApplyResources(this.lblReason, "lblReason");
            this.lblReason.Location = new System.Drawing.Point(0, 96);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(445, 75);
            this.lblReason.TextSize = new System.Drawing.Size(59, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(307, 171);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(376, 171);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 171);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(307, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblPasswordHint
            // 
            this.lblPasswordHint.AllowHtmlStringInCaption = true;
            this.lblPasswordHint.Control = this.txtPasswordHint;
            resources.ApplyResources(this.lblPasswordHint, "lblPasswordHint");
            this.lblPasswordHint.Location = new System.Drawing.Point(248, 72);
            this.lblPasswordHint.Name = "lblPasswordHint";
            this.lblPasswordHint.Size = new System.Drawing.Size(197, 24);
            this.lblPasswordHint.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblPasswordHint.TextSize = new System.Drawing.Size(59, 13);
            this.lblPasswordHint.TextToControlDistance = 5;
            // 
            // frmAuditLockTransAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAuditLockTransAdd";
            this.Load += new System.EventHandler(this.frmAuditLockTransAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordHint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLockType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLockType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPasswordHint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.MemoEdit meReason;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.GridLookUpEdit glkpLockType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colLockId;
        private DevExpress.XtraGrid.Columns.GridColumn colLockType;
        private DevExpress.XtraEditors.DateEdit deDateTo;
        private DevExpress.XtraEditors.DateEdit deDateFrom;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlGroup lcgTrans;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.LayoutControlItem lblDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblDateTo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblLockType;
        private DevExpress.XtraLayout.LayoutControlItem lblPassword;
        private DevExpress.XtraLayout.LayoutControlItem lblReason;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.TextEdit txtPasswordHint;
        private DevExpress.XtraLayout.LayoutControlItem lblPasswordHint;
    }
}