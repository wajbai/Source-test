namespace ACPP.Modules.User_Management
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.lcChangePassword = new DevExpress.XtraLayout.LayoutControl();
            this.txtCurrentpassword = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtConfirmPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.lcgChanagePassword = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblUserName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNewPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblConfirmNewPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCloseButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSaveButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcChangePassword)).BeginInit();
            this.lcChangePassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentpassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgChanagePassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConfirmNewPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCloseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSaveButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // lcChangePassword
            // 
            this.lcChangePassword.AllowCustomizationMenu = false;
            this.lcChangePassword.Controls.Add(this.txtCurrentpassword);
            this.lcChangePassword.Controls.Add(this.btnSave);
            this.lcChangePassword.Controls.Add(this.btnClose);
            this.lcChangePassword.Controls.Add(this.txtConfirmPassword);
            this.lcChangePassword.Controls.Add(this.txtNewPassword);
            this.lcChangePassword.Controls.Add(this.txtUsername);
            resources.ApplyResources(this.lcChangePassword, "lcChangePassword");
            this.lcChangePassword.Name = "lcChangePassword";
            this.lcChangePassword.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(410, 121, 321, 470);
            this.lcChangePassword.Root = this.lcgChanagePassword;
            // 
            // txtCurrentpassword
            // 
            resources.ApplyResources(this.txtCurrentpassword, "txtCurrentpassword");
            this.txtCurrentpassword.Name = "txtCurrentpassword";
            this.txtCurrentpassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCurrentpassword.Properties.PasswordChar = '*';
            this.txtCurrentpassword.StyleController = this.lcChangePassword;
            this.txtCurrentpassword.Leave += new System.EventHandler(this.txtCurrentpassword_Leave);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcChangePassword;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcChangePassword;
            // 
            // txtConfirmPassword
            // 
            resources.ApplyResources(this.txtConfirmPassword, "txtConfirmPassword");
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtConfirmPassword.Properties.PasswordChar = '*';
            this.txtConfirmPassword.StyleController = this.lcChangePassword;
            this.txtConfirmPassword.Leave += new System.EventHandler(this.txtConfirmPassword_Leave);
            // 
            // txtNewPassword
            // 
            resources.ApplyResources(this.txtNewPassword, "txtNewPassword");
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNewPassword.Properties.PasswordChar = '*';
            this.txtNewPassword.StyleController = this.lcChangePassword;
            this.txtNewPassword.Leave += new System.EventHandler(this.txtNewPassword_Leave);
            // 
            // txtUsername
            // 
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUsername.Properties.ReadOnly = true;
            this.txtUsername.StyleController = this.lcChangePassword;
            // 
            // lcgChanagePassword
            // 
            resources.ApplyResources(this.lcgChanagePassword, "lcgChanagePassword");
            this.lcgChanagePassword.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgChanagePassword.GroupBordersVisible = false;
            this.lcgChanagePassword.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblUserName,
            this.lblNewPassword,
            this.emptySpaceItem1,
            this.lblConfirmNewPassword,
            this.lblCloseButton,
            this.lblSaveButton,
            this.layoutControlItem1});
            this.lcgChanagePassword.Location = new System.Drawing.Point(0, 0);
            this.lcgChanagePassword.Name = "lcgChanagePassword";
            this.lcgChanagePassword.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgChanagePassword.Size = new System.Drawing.Size(310, 133);
            this.lcgChanagePassword.TextVisible = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AllowHtmlStringInCaption = true;
            this.lblUserName.Control = this.txtUsername;
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Location = new System.Drawing.Point(0, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblUserName.Size = new System.Drawing.Size(300, 26);
            this.lblUserName.TextSize = new System.Drawing.Size(122, 13);
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AllowHtmlStringInCaption = true;
            this.lblNewPassword.Control = this.txtNewPassword;
            resources.ApplyResources(this.lblNewPassword, "lblNewPassword");
            this.lblNewPassword.Location = new System.Drawing.Point(0, 52);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblNewPassword.Size = new System.Drawing.Size(300, 23);
            this.lblNewPassword.TextSize = new System.Drawing.Size(122, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 95);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(168, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblConfirmNewPassword
            // 
            this.lblConfirmNewPassword.AllowHtmlStringInCaption = true;
            this.lblConfirmNewPassword.Control = this.txtConfirmPassword;
            resources.ApplyResources(this.lblConfirmNewPassword, "lblConfirmNewPassword");
            this.lblConfirmNewPassword.Location = new System.Drawing.Point(0, 75);
            this.lblConfirmNewPassword.Name = "lblConfirmNewPassword";
            this.lblConfirmNewPassword.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblConfirmNewPassword.Size = new System.Drawing.Size(300, 20);
            this.lblConfirmNewPassword.TextSize = new System.Drawing.Size(122, 13);
            // 
            // lblCloseButton
            // 
            this.lblCloseButton.Control = this.btnClose;
            resources.ApplyResources(this.lblCloseButton, "lblCloseButton");
            this.lblCloseButton.Location = new System.Drawing.Point(235, 95);
            this.lblCloseButton.Name = "lblCloseButton";
            this.lblCloseButton.Size = new System.Drawing.Size(65, 28);
            this.lblCloseButton.TextSize = new System.Drawing.Size(0, 0);
            this.lblCloseButton.TextToControlDistance = 0;
            this.lblCloseButton.TextVisible = false;
            // 
            // lblSaveButton
            // 
            this.lblSaveButton.Control = this.btnSave;
            resources.ApplyResources(this.lblSaveButton, "lblSaveButton");
            this.lblSaveButton.Location = new System.Drawing.Point(168, 95);
            this.lblSaveButton.Name = "lblSaveButton";
            this.lblSaveButton.Size = new System.Drawing.Size(67, 28);
            this.lblSaveButton.TextSize = new System.Drawing.Size(0, 0);
            this.lblSaveButton.TextToControlDistance = 0;
            this.lblSaveButton.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHtmlStringInCaption = true;
            this.layoutControlItem1.Control = this.txtCurrentpassword;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.layoutControlItem1.Size = new System.Drawing.Size(300, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(122, 13);
            // 
            // frmChangePassword
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcChangePassword);
            this.Name = "frmChangePassword";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcChangePassword)).EndInit();
            this.lcChangePassword.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentpassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgChanagePassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConfirmNewPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCloseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSaveButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcChangePassword;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtConfirmPassword;
        private DevExpress.XtraEditors.TextEdit txtNewPassword;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraLayout.LayoutControlGroup lcgChanagePassword;
        private DevExpress.XtraLayout.LayoutControlItem lblUserName;
        private DevExpress.XtraLayout.LayoutControlItem lblNewPassword;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblConfirmNewPassword;
        private DevExpress.XtraLayout.LayoutControlItem lblCloseButton;
        private DevExpress.XtraLayout.LayoutControlItem lblSaveButton;
        private DevExpress.XtraEditors.TextEdit txtCurrentpassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}