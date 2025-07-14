using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Configuration.Install;

namespace AcMEERPInstaller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMySQLInstaller());
            DialogResult DBconfigResult = DialogResult.No;

            //For Uninstall
            if (arg.Length >= 1)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //Register
                    if (EmbeddedAssembly.Load("AcMEERPInstaller.Resources.SimpleEncrypt.dll", "SimpleEncrypt.dll"))
                    {
                        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                    }
                    string productcode = arg[0].ToString();
                    //MessageBox.Show(productcode);
                    //{8E78268A-8B00-4BB3-9FAE-3E13844BDD7A}
                    if (productcode != string.Empty)
                    {
                        if (productcode == "Remove")
                        {
                            //Remove MySQLACPERP service
                            if (General.ChangeMySQLACPERPServiceStatus(false))
                            {
                                using (DBHandler dbh = new DBHandler())
                                {
                                    if (General.RemoveMySQLACPERPService())
                                    {
                                        General.WriteLog("Sucessfully MySQLACPER service removed");
                                    }
                                    else
                                    {
                                        General.WriteLog("MySQLACPER service is not removed");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!General.UninstallAcMEERP(productcode))
                            {
                                MessageBox.Show("Could not Uninstall AcMEERP", General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Could not start AcMEERP installer, " + err.Message, General.ACPERP_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                General.FROM_INSTALLTION_KIT = true;
            }
            else //for testing
            {
                General.FROM_INSTALLTION_KIT = true;
                string strInstallationDirPath = AppDomain.CurrentDomain.BaseDirectory;
                string strSetupSourcePath = AppDomain.CurrentDomain.BaseDirectory;
                General.ACPERP_TARGET_PATH = strInstallationDirPath;
                General.ACPERP_SETUP_PATH = strSetupSourcePath;
                General.ACPERP_INSTALLER_LOG = Path.Combine(General.ACPERP_TARGET_PATH, "acperpinstaller.txt");
                General.ACPERP_LIC_PATH = Path.Combine(General.ACPERP_SETUP_PATH, "AcMEERPLicense.xml");
                               
                //frmACPERPDBConnection frmdbconnection = new frmACPERPDBConnection();
                //DBconfigResult = frmdbconnection.ShowDialog();

                frmMySQLInstaller frminstaller = new frmMySQLInstaller();
                DBconfigResult = frminstaller.ShowDialog();

                //frmUpdateLicence updatelicence = new frmUpdateLicence(string.Empty, string.Empty, string.Empty);
                //updatelicence.ShowDialog();
            }
        }

        private static WindowWrapper GetOwnerWindow()
        {
            //Get the process Hwnd
            IntPtr hwnd = IntPtr.Zero;
            WindowWrapper windowwrapper = null;
            Process[] procs = Process.GetProcessesByName("msiexec");
            foreach (Process pr in procs)
            {
                if (pr.MainWindowHandle != null)
                {
                    hwnd = pr.MainWindowHandle;
                    if (hwnd != IntPtr.Zero)
                    {
                        if (pr.MainWindowTitle.ToUpper().Contains("ACMEERP") )
                        {
                            //MessageBox.Show(hwnd.ToString());
                            windowwrapper = new WindowWrapper(hwnd);
                            break;
                        }
                    }
                }
            }
            return windowwrapper;
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //return EmbeddedAssembly.Get(args.Name);
            return EmbeddedAssembly.Get("SimpleEncrypt, Version=1.0.3048.24602, Culture=neutral, PublicKeyToken=null");
        }
    }

    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        private IntPtr _hwnd;

        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }
    }
}
