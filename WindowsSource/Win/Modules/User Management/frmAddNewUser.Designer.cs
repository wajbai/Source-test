namespace ACPP.Modules.User_Management
{
    partial class frmAddNewUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddNewUser));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lcNewUser = new DevExpress.XtraLayout.LayoutControl();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLastName = new DevExpress.XtraEditors.TextEdit();
            this.txtFirstName = new DevExpress.XtraEditors.TextEdit();
            this.lkpUserRole = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvlkpColUser_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvlkpColUserRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.meNotes = new DevExpress.XtraEditors.MemoEdit();
            this.rgGender = new DevExpress.XtraEditors.RadioGroup();
            this.peUserPhoto = new DevExpress.XtraEditors.PictureEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmailAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtContactNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtmeAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtRetypePassword = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.lcgAddNewUser = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblUserName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRetypePassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblContactNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblEmailAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lkpUserRole1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFirstName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLastName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcNewUser)).BeginInit();
            this.lcNewUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUserRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgGender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUserPhoto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetypePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAddNewUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRetypePassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblContactNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEmailAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUserRole1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFirstName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcNewUser;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lcNewUser
            // 
            this.lcNewUser.AllowCustomizationMenu = false;
            this.lcNewUser.Controls.Add(this.label1);
            this.lcNewUser.Controls.Add(this.txtLastName);
            this.lcNewUser.Controls.Add(this.txtFirstName);
            this.lcNewUser.Controls.Add(this.lkpUserRole);
            this.lcNewUser.Controls.Add(this.meNotes);
            this.lcNewUser.Controls.Add(this.rgGender);
            this.lcNewUser.Controls.Add(this.peUserPhoto);
            this.lcNewUser.Controls.Add(this.btnSave);
            this.lcNewUser.Controls.Add(this.btnClose);
            this.lcNewUser.Controls.Add(this.txtEmailAddress);
            this.lcNewUser.Controls.Add(this.txtContactNumber);
            this.lcNewUser.Controls.Add(this.txtmeAddress);
            this.lcNewUser.Controls.Add(this.txtRetypePassword);
            this.lcNewUser.Controls.Add(this.txtPassword);
            this.lcNewUser.Controls.Add(this.txtUserName);
            resources.ApplyResources(this.lcNewUser, "lcNewUser");
            this.lcNewUser.Name = "lcNewUser";
            this.lcNewUser.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(600, 151, 360, 444);
            this.lcNewUser.Root = this.lcgAddNewUser;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtLastName
            // 
            resources.ApplyResources(this.txtLastName, "txtLastName");
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtLastName.Properties.MaxLength = 100;
            this.txtLastName.StyleController = this.lcNewUser;
            // 
            // txtFirstName
            // 
            resources.ApplyResources(this.txtFirstName, "txtFirstName");
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtFirstName.Properties.MaxLength = 100;
            this.txtFirstName.StyleController = this.lcNewUser;
            this.txtFirstName.Leave += new System.EventHandler(this.txtFirstName_Leave);
            // 
            // lkpUserRole
            // 
            resources.ApplyResources(this.lkpUserRole, "lkpUserRole");
            this.lkpUserRole.Name = "lkpUserRole";
            this.lkpUserRole.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lkpUserRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkpUserRole.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkpUserRole.Properties.Buttons1"))))});
            this.lkpUserRole.Properties.ImmediatePopup = true;
            this.lkpUserRole.Properties.NullText = resources.GetString("lkpUserRole.Properties.NullText");
            this.lkpUserRole.Properties.PopupFormSize = new System.Drawing.Size(250, 50);
            this.lkpUserRole.Properties.View = this.gridLookUpEdit1View;
            this.lkpUserRole.StyleController = this.lcNewUser;
            this.lkpUserRole.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lkpUserRole_ButtonClick);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvlkpColUser_Id,
            this.gvlkpColUserRole});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsFilter.ColumnFilterPopupRowCount = 5;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gvlkpColUser_Id
            // 
            resources.ApplyResources(this.gvlkpColUser_Id, "gvlkpColUser_Id");
            this.gvlkpColUser_Id.FieldName = "USERROLE_ID";
            this.gvlkpColUser_Id.Name = "gvlkpColUser_Id";
            this.gvlkpColUser_Id.OptionsColumn.ShowCaption = false;
            // 
            // gvlkpColUserRole
            // 
            resources.ApplyResources(this.gvlkpColUserRole, "gvlkpColUserRole");
            this.gvlkpColUserRole.FieldName = "USERROLE";
            this.gvlkpColUserRole.Name = "gvlkpColUserRole";
            this.gvlkpColUserRole.OptionsColumn.ShowCaption = false;
            // 
            // meNotes
            // 
            resources.ApplyResources(this.meNotes, "meNotes");
            this.meNotes.Name = "meNotes";
            this.meNotes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.meNotes.Properties.MaxLength = 500;
            this.meNotes.StyleController = this.lcNewUser;
            this.meNotes.UseOptimizedRendering = true;
            // 
            // rgGender
            // 
            resources.ApplyResources(this.rgGender, "rgGender");
            this.rgGender.Name = "rgGender";
            this.rgGender.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgGender.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgGender.Properties.Items"))), resources.GetString("rgGender.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgGender.Properties.Items2"))), resources.GetString("rgGender.Properties.Items3")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgGender.Properties.Items4"))), resources.GetString("rgGender.Properties.Items5"))});
            this.rgGender.StyleController = this.lcNewUser;
            // 
            // peUserPhoto
            // 
            this.peUserPhoto.EditValue = global::ACPP.Properties.Resources.Default_Photo;
            resources.ApplyResources(this.peUserPhoto, "peUserPhoto");
            this.peUserPhoto.Name = "peUserPhoto";
            this.peUserPhoto.Properties.NullText = resources.GetString("peUserPhoto.Properties.NullText");
            this.peUserPhoto.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.peUserPhoto.StyleController = this.lcNewUser;
            this.peUserPhoto.Click += new System.EventHandler(this.peUserPhoto_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcNewUser;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.CausesValidation = false;
            resources.ApplyResources(this.txtEmailAddress, "txtEmailAddress");
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtEmailAddress.Properties.Mask.EditMask = resources.GetString("txtEmailAddress.Properties.Mask.EditMask");
            this.txtEmailAddress.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtEmailAddress.Properties.Mask.MaskType")));
            this.txtEmailAddress.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtEmailAddress.Properties.Mask.UseMaskAsDisplayFormat")));
            this.txtEmailAddress.Properties.MaxLength = 50;
            this.txtEmailAddress.StyleController = this.lcNewUser;
            // 
            // txtContactNumber
            // 
            resources.ApplyResources(this.txtContactNumber, "txtContactNumber");
            this.txtContactNumber.Name = "txtContactNumber";
            this.txtContactNumber.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtContactNumber.Properties.MaxLength = 15;
            this.txtContactNumber.StyleController = this.lcNewUser;
            // 
            // txtmeAddress
            // 
            resources.ApplyResources(this.txtmeAddress, "txtmeAddress");
            this.txtmeAddress.Name = "txtmeAddress";
            this.txtmeAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtmeAddress.Properties.MaxLength = 300;
            this.txtmeAddress.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtmeAddress.StyleController = this.lcNewUser;
            this.txtmeAddress.UseOptimizedRendering = true;
            // 
            // txtRetypePassword
            // 
            resources.ApplyResources(this.txtRetypePassword, "txtRetypePassword");
            this.txtRetypePassword.Name = "txtRetypePassword";
            this.txtRetypePassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtRetypePassword.Properties.MaxLength = 50;
            this.txtRetypePassword.Properties.PasswordChar = '*';
            this.txtRetypePassword.StyleController = this.lcNewUser;
            this.txtRetypePassword.Leave += new System.EventHandler(this.txtRetypePassword_Leave);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPassword.Properties.MaxLength = 50;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.StyleController = this.lcNewUser;
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUserName.Properties.MaxLength = 50;
            this.txtUserName.StyleController = this.lcNewUser;
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // lcgAddNewUser
            // 
            resources.ApplyResources(this.lcgAddNewUser, "lcgAddNewUser");
            this.lcgAddNewUser.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgAddNewUser.GroupBordersVisible = false;
            this.lcgAddNewUser.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblUserName,
            this.lblPassword,
            this.lblRetypePassword,
            this.lblContactNumber,
            this.lblEmailAddress,
            this.lblClose,
            this.lblSave,
            this.emptySpaceItem1,
            this.lblAddress,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.lkpUserRole1,
            this.lblFirstName,
            this.lblLastName,
            this.layoutControlItem4});
            this.lcgAddNewUser.Location = new System.Drawing.Point(0, 0);
            this.lcgAddNewUser.Name = "Root";
            this.lcgAddNewUser.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgAddNewUser.Size = new System.Drawing.Size(488, 329);
            this.lcgAddNewUser.TextVisible = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AllowHtmlStringInCaption = true;
            this.lblUserName.Control = this.txtUserName;
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Location = new System.Drawing.Point(0, 46);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblUserName.Size = new System.Drawing.Size(348, 23);
            this.lblUserName.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblPassword
            // 
            this.lblPassword.AllowHtmlStringInCaption = true;
            this.lblPassword.Control = this.txtPassword;
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Location = new System.Drawing.Point(0, 69);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblPassword.Size = new System.Drawing.Size(348, 23);
            this.lblPassword.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblRetypePassword
            // 
            this.lblRetypePassword.AllowHtmlStringInCaption = true;
            this.lblRetypePassword.Control = this.txtRetypePassword;
            resources.ApplyResources(this.lblRetypePassword, "lblRetypePassword");
            this.lblRetypePassword.Location = new System.Drawing.Point(0, 92);
            this.lblRetypePassword.MinSize = new System.Drawing.Size(149, 23);
            this.lblRetypePassword.Name = "lblRetypePassword";
            this.lblRetypePassword.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblRetypePassword.Size = new System.Drawing.Size(348, 25);
            this.lblRetypePassword.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRetypePassword.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblContactNumber
            // 
            this.lblContactNumber.Control = this.txtContactNumber;
            resources.ApplyResources(this.lblContactNumber, "lblContactNumber");
            this.lblContactNumber.Location = new System.Drawing.Point(0, 140);
            this.lblContactNumber.Name = "lblContactNumber";
            this.lblContactNumber.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblContactNumber.Size = new System.Drawing.Size(348, 23);
            this.lblContactNumber.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.Control = this.txtEmailAddress;
            resources.ApplyResources(this.lblEmailAddress, "lblEmailAddress");
            this.lblEmailAddress.Location = new System.Drawing.Point(0, 163);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblEmailAddress.Size = new System.Drawing.Size(348, 23);
            this.lblEmailAddress.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblClose
            // 
            this.lblClose.Control = this.btnClose;
            resources.ApplyResources(this.lblClose, "lblClose");
            this.lblClose.Location = new System.Drawing.Point(421, 303);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(67, 26);
            this.lblClose.TextSize = new System.Drawing.Size(0, 0);
            this.lblClose.TextToControlDistance = 0;
            this.lblClose.TextVisible = false;
            // 
            // lblSave
            // 
            this.lblSave.Control = this.btnSave;
            resources.ApplyResources(this.lblSave, "lblSave");
            this.lblSave.Location = new System.Drawing.Point(355, 303);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(66, 26);
            this.lblSave.TextSize = new System.Drawing.Size(0, 0);
            this.lblSave.TextToControlDistance = 0;
            this.lblSave.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 303);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(355, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblAddress
            // 
            this.lblAddress.Control = this.txtmeAddress;
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Location = new System.Drawing.Point(0, 218);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblAddress.Size = new System.Drawing.Size(488, 42);
            this.lblAddress.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.peUserPhoto;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(348, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(140, 173);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.rgGender;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 186);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(348, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(348, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem2.Size = new System.Drawing.Size(348, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.meNotes;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 260);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(488, 43);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lkpUserRole1
            // 
            this.lkpUserRole1.AllowHtmlStringInCaption = true;
            this.lkpUserRole1.Control = this.lkpUserRole;
            resources.ApplyResources(this.lkpUserRole1, "lkpUserRole1");
            this.lkpUserRole1.Location = new System.Drawing.Point(0, 117);
            this.lkpUserRole1.Name = "lkpUserRole1";
            this.lkpUserRole1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lkpUserRole1.Size = new System.Drawing.Size(348, 23);
            this.lkpUserRole1.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AllowHtmlStringInCaption = true;
            this.lblFirstName.Control = this.txtFirstName;
            resources.ApplyResources(this.lblFirstName, "lblFirstName");
            this.lblFirstName.Location = new System.Drawing.Point(0, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblFirstName.Size = new System.Drawing.Size(348, 23);
            this.lblFirstName.TextSize = new System.Drawing.Size(95, 13);
            // 
            // lblLastName
            // 
            this.lblLastName.Control = this.txtLastName;
            resources.ApplyResources(this.lblLastName, "lblLastName");
            this.lblLastName.Location = new System.Drawing.Point(0, 23);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblLastName.Size = new System.Drawing.Size(348, 23);
            this.lblLastName.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.label1;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(348, 173);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(140, 45);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmAddNewUser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcNewUser);
            this.Name = "frmAddNewUser";
            this.Load += new System.EventHandler(this.frmAddNewUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcNewUser)).EndInit();
            this.lcNewUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLastName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUserRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgGender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUserPhoto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRetypePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAddNewUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRetypePassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblContactNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEmailAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUserRole1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFirstName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcNewUser;
        private DevExpress.XtraLayout.LayoutControlGroup lcgAddNewUser;
        private DevExpress.XtraEditors.TextEdit txtEmailAddress;
        private DevExpress.XtraEditors.TextEdit txtContactNumber;
        private DevExpress.XtraEditors.MemoEdit txtmeAddress;
        private DevExpress.XtraEditors.TextEdit txtRetypePassword;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraLayout.LayoutControlItem lblUserName;
        private DevExpress.XtraLayout.LayoutControlItem lblPassword;
        private DevExpress.XtraLayout.LayoutControlItem lblRetypePassword;
        private DevExpress.XtraLayout.LayoutControlItem lblAddress;
        private DevExpress.XtraLayout.LayoutControlItem lblContactNumber;
        private DevExpress.XtraLayout.LayoutControlItem lblEmailAddress;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem lblClose;
        private DevExpress.XtraLayout.LayoutControlItem lblSave;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.PictureEdit peUserPhoto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.RadioGroup rgGender;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.MemoEdit meNotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.GridLookUpEdit lkpUserRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lkpUserRole1;
        private DevExpress.XtraGrid.Columns.GridColumn gvlkpColUser_Id;
        private DevExpress.XtraGrid.Columns.GridColumn gvlkpColUserRole;
        private DevExpress.XtraEditors.TextEdit txtLastName;
        private DevExpress.XtraEditors.TextEdit txtFirstName;
        private DevExpress.XtraLayout.LayoutControlItem lblFirstName;
        private DevExpress.XtraLayout.LayoutControlItem lblLastName;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}