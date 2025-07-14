using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;

namespace ACPP.Modules.User_Management
{
    public partial class frmUserPhoto : frmFinanceBaseAdd
    {
        public frmUserPhoto()
        {
            InitializeComponent();

            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;

            // this.StartPosition = FormStartPosition.Manual;
            // this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            // //  this.Location = new Point(Screen.PrimaryScreen.Bounds.Bottom - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height);
        }

        public frmUserPhoto(RibbonControl ribbonMain)
            : this()
        {
            this.Location = new Point(655, 44);
            // this.Invoke(new MethodInvoker(delegate { this.Location = new Point(ribbonMain.Left, ribbonMain.Height + ribbonMain.Top); }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmMain.IsProfileClicked = true;
            this.Close();
        }

        private void frmUserPhoto_Load(object sender, EventArgs e)
        {

        }

        private void frmUserPhoto_MouseLeave(object sender, EventArgs e)
        {
            //frmMain.IsProfileClicked = true;
            //this.Close();
        }
    }
}
