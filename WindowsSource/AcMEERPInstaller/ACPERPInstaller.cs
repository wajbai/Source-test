using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace AcMEERPInstaller
{
    ///DIR="[TARGETDIR]\"
    [RunInstaller(true)]
    public partial class ACPERPInstaller : System.Configuration.Install.Installer
    {
        string strInstallationDirPath = string.Empty;
        string strSourceDirPath = string.Empty;

        public ACPERPInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            AssignPath();
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            AssignPath();
        }

        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);
            DialogResult dialogresult = DialogResult.No;
            try
            {
                AssignPath();
                string strInstallationDirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                
                if (General.IsMySQL5_6Installed())
                {
                    IntPtr acmeerp = GetOwnerWindow();
                    WindowWrapper wrapper = new WindowWrapper(acmeerp);

                    frmMySQLInstaller mysqlinstaller = new frmMySQLInstaller();
                    dialogresult = mysqlinstaller.ShowDialog(wrapper);
                }
                else
                {
                    MessageBox.Show("Acme.erp Prerequisites (MySQL 5.6) is not installed properly, Contact Support Team", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                //------------------------------------------------------------------------------------------------
            }
            catch (Exception err)
            {
                if (File.Exists(General.ACPERP_INSTALLER_LOG))
                {
                    MessageBox.Show("Could not complete installation, contact support team" + err.Message, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Could not complete installation, contact support team" + err.Message, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            using (DBHandler dbhander = new DBHandler())
            {
                if (!General.LIC_UPDATED || !dbhander.TestConnection(false))
                {
                    throw new Exception("Falied in installation, Acme.erp Licence/Database is not configured");
                }

            }
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
       
       
        private IntPtr GetOwnerWindow()
        {
            //Get the process Hwnd
            IntPtr hwnd = IntPtr.Zero;
            Process[] procs = Process.GetProcessesByName("msiexec");
            foreach (Process pr in procs)
            {
                if (pr.MainWindowHandle != null)
                {
                    hwnd = pr.MainWindowHandle;
                    if (hwnd != IntPtr.Zero)
                    {
                        if (pr.MainWindowTitle.ToUpper().Contains("ACME.ERP"))   //ACMEERP
                        {
                            //MessageBox.Show(hwnd.ToString());
                            break;
                        }
                    }
                }
            }
            return hwnd;
        }

        private void AssignPath()
        {
            if (this.Context.Parameters["DIR"] != null)
            {
                strInstallationDirPath = this.Context.Parameters["DIR"].ToString();
                //MessageBox.Show("Check " + strInstallationDirPath);
            }
                        
            if (this.Context.Parameters["SDIR"] != null)
            {
                strSourceDirPath = this.Context.Parameters["SDIR"].ToString();
            }

            General.FROM_INSTALLTION_KIT = true;
            General.ACPERP_TARGET_PATH = strInstallationDirPath;
            General.ACPERP_SETUP_PATH = strSourceDirPath;
            General.ACPERP_INSTALLER_LOG = Path.Combine(General.ACPERP_TARGET_PATH, "acperpinstaller.txt");
        }
    }   
}