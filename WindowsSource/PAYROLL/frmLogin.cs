using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using Payroll.Model.UIModel;
using Bosco.Utility;
using Bosco.Utility.Common;
 

namespace PAYROLL
{ 
	public class frmLogin : System.Windows.Forms.Form
	{
		public System.Windows.Forms.ToolTip ToolTip1;//TODO Not used
		public System.Windows.Forms.Label _lblCaption_0;//TODO Not used
		public System.Windows.Forms.PictureBox Image1; //TODO Not used
		public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnOk;
		public System.Windows.Forms.Label lblPassword;
		public System.Windows.Forms.Label lblUsername;
        private TextBox txtUsername;
        private TextBox txtPassword;
		private System.ComponentModel.IContainer components;
        

		public frmLogin()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this._lblCaption_0 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPassword.Location = new System.Drawing.Point(8, 74);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 15;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblUsername.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUsername.Location = new System.Drawing.Point(8, 49);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUsername.Size = new System.Drawing.Size(59, 13);
            this.lblUsername.TabIndex = 14;
            this.lblUsername.Text = "User Name";
            // 
            // _lblCaption_0
            // 
            this._lblCaption_0.AutoSize = true;
            this._lblCaption_0.BackColor = System.Drawing.Color.Transparent;
            this._lblCaption_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._lblCaption_0.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblCaption_0.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblCaption_0.Location = new System.Drawing.Point(62, 14);
            this._lblCaption_0.Name = "_lblCaption_0";
            this._lblCaption_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblCaption_0.Size = new System.Drawing.Size(200, 13);
            this._lblCaption_0.TabIndex = 13;
            this._lblCaption_0.Text = "Enter User Name and Password to login.";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.Location = new System.Drawing.Point(198, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOk.Location = new System.Drawing.Point(126, 100);
            this.btnOk.Name = "btnOk";
            this.btnOk.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOk.Size = new System.Drawing.Size(64, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.MouseLeave += new System.EventHandler(this.btnOk_MouseLeave);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOk.MouseEnter += new System.EventHandler(this.btnOk_MouseEnter);
            // 
            // Image1
            // 
            this.Image1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Image1.BackgroundImage")));
            this.Image1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Image1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Image1.Location = new System.Drawing.Point(8, 4);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(40, 40);
            this.Image1.TabIndex = 19;
            this.Image1.TabStop = false;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(85, 46);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(177, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(85, 74);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(177, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // frmLogin
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(280, 130);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this._lblCaption_0);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.btnCancel);
            this.ForeColor = System.Drawing.Color.Maroon;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Closed += new System.EventHandler(this.frmLogin_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private string strUsername = "";
		private string strPassword = "";

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmLogin_Closed(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{		
			this.Cursor = Cursors.WaitCursor;
			strUsername = txtUsername.Text.Trim().ToUpper();
			strPassword = txtPassword.Text.Trim();

			if (strUsername == "" && strPassword == "")
			{
				MessageBox.Show("Enter the User Name and Password", clsGeneral.MsgCaption,
								 MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtUsername.Focus();
				this.Cursor = Cursors.Default;
				return;
			}
			else if (strUsername == "")
			{
				MessageBox.Show("Enter the User Name", clsGeneral.MsgCaption,
								 MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtUsername.Focus();
				this.Cursor = Cursors.Default;
				return;
			}
			else if (strPassword == "")
			{
				MessageBox.Show("Enter the Password", clsGeneral.MsgCaption,
								 MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtPassword.Focus();
				this.Cursor = Cursors.Default;
				return;
			}
            
            //if (checkBackendSetting()) 
				invokeOk(); //TODO Give Meaningfull name for this could be authenticateUser()
            //else
            //    MessageBox.Show ("Database setting required", clsGeneral.MsgCaption,
            //                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
			this.Cursor = Cursors.Default;
		}

        //private string verifyLoginDetails()
        //{
        ////	clsPatient objPatient = new clsPatient();

			
        //    /*
        //     * TODO the following  function should be changed to verifyLoginInfo(strConditionalValues)
        //     *		and fields and table name should be passed from the business logic.
        //    */	 
        //    // return objPatient.verifyLoginInfo("Users", strConditionalFields, strConditionalValues);
        //}

		private void invokeOk()
		{
			
			string strErrMsg = "";
	
//			try
//			{
				strUsername = txtUsername.Text.Trim().ToUpper();
				strPassword = txtPassword.Text.Trim();
            	
				if (strUsername != "" && strPassword != "")
				{
                    using (UserSystem userSystem = new UserSystem())
                    {
                        this.Cursor = Cursors.WaitCursor;

                        ResultArgs result = userSystem.AuthenticateUser(txtUsername.Text, txtPassword.Text);

                        if (result.Success && result.RowsAffected >0 )
                        {
                          //  mdiHMS mdi = new mdiHMS(strUsername);
                            this.Visible = false;
                            this.ShowInTaskbar = false;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(result.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtUsername.Text = "";
                            txtPassword.Text = "";
                            txtUsername.Focus();
                        }
                        this.Cursor = Cursors.Default;
                    }
                    //if (strErrMsg != "")
                    //{
                    //    MessageBox.Show(strErrMsg, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					
                    //    txtUsername.Text = "";
                    //    txtPassword.Text = "";
                    //    txtUsername.Focus();
                    //}
                    //else
                    //{
                    //    //clsPharmacySettings.getPharmacySettings();
                    //    //clsRecordingSettings.getRecordingSettings();
                    ////	clsStoreSettings.getStoreSettings();
                    //    mdiPayroll mdi = new mdiPayroll(strUsername);
                    //    this.Visible = false ;
                    //    this.ShowInTaskbar = false;
                    //    mdi.ShowDialog();
                    //    this.Close();
                    //}
				}
//			}
//			catch{}
		}

		private void frmLogin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Return)
			{
				if (this.ActiveControl.Name == "txtPassword")
					btnOk_Click(sender, new EventArgs());
				else
					SendKeys.Send("{TAB}");
			}
			if(e.KeyCode == Keys.F5) invokeOk();
		}

        //private bool checkBackendSetting()
        //{
        //    clsBackEndSetting objBackEnd = new clsBackEndSetting();
        //    if (!objBackEnd.ReadSetting())
        //    {
        //        frmBackEndSetting frmBackEnd = new frmBackEndSetting();
        //        return (!frmBackEnd.DisplayMe());
        //    }
        //    return true;
        //}

		private void frmLogin_Load(object sender, System.EventArgs e)
		{
            
		}

        private void btnOk_MouseEnter(object sender, EventArgs e)
        {
            btnOk.UseVisualStyleBackColor = false;
            this.btnOk.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Wizard_4615A142_copy3));
        }

        private void btnOk_MouseLeave(object sender, EventArgs e)
        {
            this.btnOk.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Wizard_4615A142_copy2));
            btnOk.UseVisualStyleBackColor = true;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Wizard_4615A142_copy2));
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Wizard_4615A142_copy3));
        }

	}
}
