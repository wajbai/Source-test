using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SUPPORT
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        // This is to bring ho branch data as .xml format. Config files should be set as ho database 
        private void nbiExportBranchData_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // To Update the windows application all script files 
            // ho and bo code license is set
            // try to export the data..
            frmSplitProject exportData = new frmSplitProject();
            exportData.ShowDialog();
        }

        private void nbiImportBranchData_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmImportSplitedProject importData = new frmImportSplitedProject();
            importData.ShowDialog();
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }
    }
}