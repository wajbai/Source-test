namespace ACPP.Modules.User_Management
{
    partial class frmManageSecurity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageSecurity));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lcManageSecurity = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.gcManageSecurity = new DevExpress.XtraGrid.GridControl();
            this.gvManageSecurity = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserRoles = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResetPassword = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribtnResetPassword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rglkpUserRole = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.glkpUserRole = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gvGridLookupEdit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colgUserRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpRole = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rbtnResetPassword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.lkpUseRole = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rlkpUserRole = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rhlResetPassword = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.lcgManageSecurity = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblManageSecurity = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcManageSecurity)).BeginInit();
            this.lcManageSecurity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcManageSecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManageSecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribtnResetPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpUserRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpUserRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridLookupEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnResetPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUseRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpUserRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rhlResetPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgManageSecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblManageSecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // lcManageSecurity
            // 
            this.lcManageSecurity.AllowCustomizationMenu = false;
            this.lcManageSecurity.Controls.Add(this.btnSave);
            this.lcManageSecurity.Controls.Add(this.btnClose);
            this.lcManageSecurity.Controls.Add(this.gcManageSecurity);
            resources.ApplyResources(this.lcManageSecurity, "lcManageSecurity");
            this.lcManageSecurity.Name = "lcManageSecurity";
            this.lcManageSecurity.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(81, 112, 250, 350);
            this.lcManageSecurity.Root = this.lcgManageSecurity;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcManageSecurity;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcManageSecurity;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gcManageSecurity
            // 
            resources.ApplyResources(this.gcManageSecurity, "gcManageSecurity");
            this.gcManageSecurity.MainView = this.gvManageSecurity;
            this.gcManageSecurity.Name = "gcManageSecurity";
            this.gcManageSecurity.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.rglkpUserRole,
            this.repositoryItemButtonEdit2,
            this.glkpUserRole,
            this.glkpRole,
            this.rbtnResetPassword,
            this.lkpUseRole,
            this.ribtnResetPassword,
            this.rlkpUserRole,
            this.repositoryItemButtonEdit3,
            this.rhlResetPassword});
            this.gcManageSecurity.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvManageSecurity});
            // 
            // gvManageSecurity
            // 
            this.gvManageSecurity.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvManageSecurity.Appearance.FocusedRow.Font")));
            this.gvManageSecurity.Appearance.FocusedRow.Options.UseFont = true;
            this.gvManageSecurity.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRoleId,
            this.colUserName,
            this.colUserRoles,
            this.colResetPassword});
            this.gvManageSecurity.GridControl = this.gcManageSecurity;
            this.gvManageSecurity.Name = "gvManageSecurity";
            this.gvManageSecurity.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvManageSecurity.OptionsView.ShowGroupPanel = false;
            // 
            // colRoleId
            // 
            this.colRoleId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRoleId.AppearanceHeader.Font")));
            this.colRoleId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRoleId, "colRoleId");
            this.colRoleId.FieldName = "USER_ID";
            this.colRoleId.Name = "colRoleId";
            // 
            // colUserName
            // 
            this.colUserName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUserName.AppearanceHeader.Font")));
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUserName, "colUserName");
            this.colUserName.FieldName = "USER_NAME";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            // 
            // colUserRoles
            // 
            this.colUserRoles.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUserRoles.AppearanceHeader.Font")));
            this.colUserRoles.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUserRoles, "colUserRoles");
            this.colUserRoles.FieldName = "USERROLE";
            this.colUserRoles.Name = "colUserRoles";
            this.colUserRoles.OptionsColumn.AllowEdit = false;
            // 
            // colResetPassword
            // 
            this.colResetPassword.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colResetPassword.AppearanceHeader.Font")));
            this.colResetPassword.AppearanceHeader.ForeColor = ((System.Drawing.Color)(resources.GetObject("colResetPassword.AppearanceHeader.ForeColor")));
            this.colResetPassword.AppearanceHeader.Options.UseFont = true;
            this.colResetPassword.AppearanceHeader.Options.UseForeColor = true;
            resources.ApplyResources(this.colResetPassword, "colResetPassword");
            this.colResetPassword.ColumnEdit = this.ribtnResetPassword;
            this.colResetPassword.Name = "colResetPassword";
            this.colResetPassword.OptionsColumn.AllowMove = false;
            this.colResetPassword.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colResetPassword.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // ribtnResetPassword
            // 
            this.ribtnResetPassword.AppearanceFocused.ForeColor = ((System.Drawing.Color)(resources.GetObject("ribtnResetPassword.AppearanceFocused.ForeColor")));
            this.ribtnResetPassword.AppearanceFocused.Options.UseForeColor = true;
            resources.ApplyResources(this.ribtnResetPassword, "ribtnResetPassword");
            this.ribtnResetPassword.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ribtnResetPassword.Buttons"))), resources.GetString("ribtnResetPassword.Buttons1"), ((int)(resources.GetObject("ribtnResetPassword.Buttons2"))), ((bool)(resources.GetObject("ribtnResetPassword.Buttons3"))), ((bool)(resources.GetObject("ribtnResetPassword.Buttons4"))), ((bool)(resources.GetObject("ribtnResetPassword.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("ribtnResetPassword.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("ribtnResetPassword.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("ribtnResetPassword.Buttons8"), ((object)(resources.GetObject("ribtnResetPassword.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("ribtnResetPassword.Buttons10"))), ((bool)(resources.GetObject("ribtnResetPassword.Buttons11"))))});
            this.ribtnResetPassword.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.ribtnResetPassword.HideSelection = false;
            this.ribtnResetPassword.Name = "ribtnResetPassword";
            this.ribtnResetPassword.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.ribtnResetPassword.Click += new System.EventHandler(this.ribtnResetPassword_Click_1);
            // 
            // repositoryItemButtonEdit1
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // rglkpUserRole
            // 
            resources.ApplyResources(this.rglkpUserRole, "rglkpUserRole");
            this.rglkpUserRole.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpUserRole.Buttons"))))});
            this.rglkpUserRole.DisplayMember = "UserRole";
            this.rglkpUserRole.Name = "rglkpUserRole";
            this.rglkpUserRole.ValueMember = "UserRoleId";
            this.rglkpUserRole.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.DetailHeight = 150;
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemButtonEdit2
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemButtonEdit2.Buttons"))))});
            this.repositoryItemButtonEdit2.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            // 
            // glkpUserRole
            // 
            resources.ApplyResources(this.glkpUserRole, "glkpUserRole");
            this.glkpUserRole.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpUserRole.Buttons"))))});
            this.glkpUserRole.Name = "glkpUserRole";
            this.glkpUserRole.PopupFormSize = new System.Drawing.Size(205, 70);
            this.glkpUserRole.View = this.gvGridLookupEdit;
            // 
            // gvGridLookupEdit
            // 
            this.gvGridLookupEdit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserRoleId,
            this.colgUserRole});
            this.gvGridLookupEdit.DetailHeight = 100;
            this.gvGridLookupEdit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvGridLookupEdit.Name = "gvGridLookupEdit";
            this.gvGridLookupEdit.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvGridLookupEdit.OptionsView.ShowColumnHeaders = false;
            this.gvGridLookupEdit.OptionsView.ShowGroupPanel = false;
            this.gvGridLookupEdit.OptionsView.ShowIndicator = false;
            // 
            // colUserRoleId
            // 
            resources.ApplyResources(this.colUserRoleId, "colUserRoleId");
            this.colUserRoleId.FieldName = "USERROLE_ID";
            this.colUserRoleId.Name = "colUserRoleId";
            // 
            // colgUserRole
            // 
            resources.ApplyResources(this.colgUserRole, "colgUserRole");
            this.colgUserRole.FieldName = "USERROLE";
            this.colgUserRole.Name = "colgUserRole";
            // 
            // glkpRole
            // 
            resources.ApplyResources(this.glkpRole, "glkpRole");
            this.glkpRole.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpRole.Buttons"))))});
            this.glkpRole.DisplayMember = "Role";
            this.glkpRole.Name = "glkpRole";
            this.glkpRole.ValueMember = "Id";
            this.glkpRole.View = this.gridView2;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // rbtnResetPassword
            // 
            resources.ApplyResources(this.rbtnResetPassword, "rbtnResetPassword");
            this.rbtnResetPassword.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnResetPassword.Buttons"))), resources.GetString("rbtnResetPassword.Buttons1"), ((int)(resources.GetObject("rbtnResetPassword.Buttons2"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons3"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons4"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnResetPassword.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("rbtnResetPassword.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("rbtnResetPassword.Buttons8"), ((object)(resources.GetObject("rbtnResetPassword.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnResetPassword.Buttons10"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons11"))))});
            this.rbtnResetPassword.Name = "rbtnResetPassword";
            this.rbtnResetPassword.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // lkpUseRole
            // 
            resources.ApplyResources(this.lkpUseRole, "lkpUseRole");
            this.lkpUseRole.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkpUseRole.Buttons"))))});
            this.lkpUseRole.Name = "lkpUseRole";
            // 
            // rlkpUserRole
            // 
            resources.ApplyResources(this.rlkpUserRole, "rlkpUserRole");
            this.rlkpUserRole.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rlkpUserRole.Buttons"))))});
            this.rlkpUserRole.DisplayMember = "Role";
            this.rlkpUserRole.Name = "rlkpUserRole";
            this.rlkpUserRole.ValueMember = "Id";
            // 
            // repositoryItemButtonEdit3
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
            this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
            // 
            // rhlResetPassword
            // 
            resources.ApplyResources(this.rhlResetPassword, "rhlResetPassword");
            this.rhlResetPassword.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rhlResetPassword.Buttons"))), resources.GetString("rhlResetPassword.Buttons1"), ((int)(resources.GetObject("rhlResetPassword.Buttons2"))), ((bool)(resources.GetObject("rhlResetPassword.Buttons3"))), ((bool)(resources.GetObject("rhlResetPassword.Buttons4"))), ((bool)(resources.GetObject("rhlResetPassword.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rhlResetPassword.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("rhlResetPassword.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("rhlResetPassword.Buttons8"), ((object)(resources.GetObject("rhlResetPassword.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rhlResetPassword.Buttons10"))), ((bool)(resources.GetObject("rhlResetPassword.Buttons11"))))});
            this.rhlResetPassword.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.rhlResetPassword.Name = "rhlResetPassword";
            this.rhlResetPassword.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // lcgManageSecurity
            // 
            resources.ApplyResources(this.lcgManageSecurity, "lcgManageSecurity");
            this.lcgManageSecurity.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgManageSecurity.GroupBordersVisible = false;
            this.lcgManageSecurity.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblManageSecurity,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.lcgManageSecurity.Location = new System.Drawing.Point(0, 0);
            this.lcgManageSecurity.Name = "lcgManageSecurity";
            this.lcgManageSecurity.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgManageSecurity.Size = new System.Drawing.Size(615, 332);
            this.lcgManageSecurity.TextVisible = false;
            // 
            // lblManageSecurity
            // 
            this.lblManageSecurity.Control = this.gcManageSecurity;
            resources.ApplyResources(this.lblManageSecurity, "lblManageSecurity");
            this.lblManageSecurity.Location = new System.Drawing.Point(0, 0);
            this.lblManageSecurity.Name = "lblManageSecurity";
            this.lblManageSecurity.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblManageSecurity.Size = new System.Drawing.Size(605, 296);
            this.lblManageSecurity.TextSize = new System.Drawing.Size(0, 0);
            this.lblManageSecurity.TextToControlDistance = 0;
            this.lblManageSecurity.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 296);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(467, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(536, 296);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(467, 296);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmManageSecurity
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcManageSecurity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmManageSecurity";
            this.Load += new System.EventHandler(this.frmManageSecurity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcManageSecurity)).EndInit();
            this.lcManageSecurity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcManageSecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManageSecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribtnResetPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpUserRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpUserRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridLookupEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnResetPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUseRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpUserRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rhlResetPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgManageSecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblManageSecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcManageSecurity;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gcManageSecurity;
        private DevExpress.XtraGrid.Views.Grid.GridView gvManageSecurity;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpUserRole;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgManageSecurity;
        private DevExpress.XtraLayout.LayoutControlItem lblManageSecurity;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkpUserRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGridLookupEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleId;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit glkpRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnResetPassword;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkpUseRole;
        private DevExpress.XtraGrid.Columns.GridColumn colResetPassword;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribtnResetPassword;
        private DevExpress.XtraGrid.Columns.GridColumn colUserRoles;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlkpUserRole;
        private DevExpress.XtraGrid.Columns.GridColumn colUserRoleId;
        private DevExpress.XtraGrid.Columns.GridColumn colgUserRole;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rhlResetPassword;
    }
}