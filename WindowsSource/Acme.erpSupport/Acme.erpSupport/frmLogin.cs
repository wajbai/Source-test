using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acme.erpSupport
{
    public partial class frmLogin : Form
    {
        private string admin_pwd = "acmeerp*007*";              //Only for Alex, Kalis
        private string user_pwd = "acmeerp*123*";               //Other Support guys
        private string USER_GET_PWD_pwd = "acmeerp*2015*";      //for antony (only get user passwrod);
        private string UPDATE_LOCATION_pwd = "acmeerp*2019*";   //for savio (update location);
        private string DELETE_PROJECT_pwd = "acmeerp1*2019*";   //for savio (update location);

        public frmLogin()
        {
            InitializeComponent();
            this.Text = General.ACMEERP_TITLE;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (txtPassword.Text == user_pwd ||
                txtPassword.Text == admin_pwd ||
                txtPassword.Text == USER_GET_PWD_pwd ||
                txtPassword.Text == UPDATE_LOCATION_pwd ||
                txtPassword.Text == DELETE_PROJECT_pwd)
            {
                General.IS_ADMIN_User = (txtPassword.Text == admin_pwd);
                General.IS_GET_USER_PWD_User = (txtPassword.Text == USER_GET_PWD_pwd);
                General.IS_UPDATE_LOCATION_User = (txtPassword.Text == UPDATE_LOCATION_pwd);
                General.IS_DELETE_PROJECT_User = (txtPassword.Text == DELETE_PROJECT_pwd);
                frmMain frmmain = new frmMain();
                frmmain.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Password", General.ACMEERP_TITLE, MessageBoxButtons.OK);
                txtPassword.Select();
                txtPassword.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
