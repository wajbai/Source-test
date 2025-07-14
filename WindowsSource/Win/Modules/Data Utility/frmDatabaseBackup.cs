using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraEditors;
using Bosco.DAO.MySQL;
using Bosco.DAO;
using Bosco.Utility;
using System.Xml.Linq;
using DevExpress.XtraLayout.Utils;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmDatabaseRestore : frmFinanceBaseAdd
    {
        #region Variables
        string BackupDirectoryPath = string.Empty;
        string RestorePath = string.Empty;
        BackupAndRestore BackRestore = new BackupAndRestore();
        #endregion

        #region Constructor
        public frmDatabaseRestore()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// To Restore Backup Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestore_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    OpenFileDialog Openfile = new OpenFileDialog();
            //    Openfile.Title = "Restore";
            //    if (Openfile.ShowDialog() == DialogResult.OK)
            //    {
            //        if (XtraMessageBox.Show("Do you want to Restore AcMeERP data?", "Restored", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //        {
            //            RestorePath = Openfile.FileName;
            //            string Message = BackRestore.RestoreDB(RestorePath);
            //            this.CloseWaitDialog();
            //            if (Message.Equals(string.Empty))
            //            {
            //                MessageBox.Show("Restored successfully" + Environment.NewLine + "The File " + RestorePath, "Restored finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                Application.Restart();
            //            }
            //            else
            //            {
            //                MessageBox.Show(Message + Environment.NewLine + "The File " + RestorePath, "Restored finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ee)
            //{
            //    XtraMessageBox.Show(ee.ToString());
            //}
        }

        private void frmDatabaseBackup_Load(object sender, EventArgs e)
        {
            flyoutPanel1.HidePopup();
            lcgTilte.Visibility = LayoutVisibility.Never;
            this.Height = 75;
            lblPathSelected.Text = string.Empty;
        }
        #endregion

        private void btnNewDataBase_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    OpenFileDialog Openfile = new OpenFileDialog();
            //    Openfile.Title = "Restore";
            //    if (Openfile.ShowDialog() == DialogResult.OK)
            //    {
            //        if (XtraMessageBox.Show("Do you want to Restore AcMeERP data?", "Restored", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //        {
            //            RestorePath = Openfile.FileName;
            //            string Message = BackRestore.CreateDB(RestorePath);
            //            this.CloseWaitDialog();
            //            if (Message.Equals(string.Empty))
            //            {
            //                MessageBox.Show("Restored successfully" + Environment.NewLine + "The File " + RestorePath, "Restored finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                Application.Restart();
            //            }
            //            else
            //            {
            //                MessageBox.Show(Message + Environment.NewLine + "The File " + RestorePath, "Restored finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ee)
            //{
            //    XtraMessageBox.Show(ee.ToString());
            //}
        }
    }
}
