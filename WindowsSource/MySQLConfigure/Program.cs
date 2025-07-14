using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Configuration.Install;

namespace MySQLConfigure
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
            DialogResult DBconfigResult = DialogResult.No;
            
            //Register
            if (EmbeddedAssembly.Load("MySQLConfigure.Resources.SimpleEncrypt.dll", "SimpleEncrypt.dll"))
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            }

            if (arg.Length == 0) //for testing
            {
                string strInstallationDirPath = @"C:\Program Files (x86)\BoscoSoft\Acme.erp";
                General.TARGET_PATH = strInstallationDirPath;
                General.INSTALLER_LOG = Path.Combine(General.TARGET_PATH, General.name_log_file);
                General.LIC_PATH = Path.Combine(General.TARGET_PATH, General.name_license_file);

                frmMySQLInstaller frminstaller = new frmMySQLInstaller();
                DBconfigResult = frminstaller.ShowDialog();
            }
            else if (arg.Length == 1) //for acmeerp supporting tool
            {
                //arg[0] :: Acmeerp installationn path
                //arg[1] :: parent window handler    
                if (File.Exists(Path.Combine(arg[0].ToString(), "ACPP.exe")))
                {
                    IntPtr supportformhandler = GetOwnerWindow("Acme.erpSupport");
                    General.TARGET_PATH = arg[0].ToString();
                    General.INSTALLER_LOG = Path.Combine(General.TARGET_PATH, General.name_log_file);
                    General.LIC_PATH = Path.Combine(General.TARGET_PATH, General.name_license_file);
                    WindowWrapper wrapper = new WindowWrapper(supportformhandler);
                    General.Title = "Supporting :: " + General.Title;
                    frmMySQLInstaller frminstaller = new frmMySQLInstaller();
                    DBconfigResult = frminstaller.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Acme.erp is installed, can't open MySQL Configuation", General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (arg.Length >= 1)  //For Uninstall, may come from setup installer
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string productcode = arg[0].ToString();
                    if (productcode != string.Empty)
                    {
                        if (productcode == "Remove")
                        {
                            if (General.ChangeMyServiceStatus(false))
                            {
                                if (General.RemoveMyService())
                                {
                                    General.WriteLog("Sucessfully My service service removed");
                                }
                                else
                                {
                                    General.WriteLog("My service is not removed");
                                }
                            }
                        }
                        else
                        {
                            if (!General.UninstallMyApplication(productcode))
                            {
                                MessageBox.Show("Could not Uninstall "+ General.MYAPPLICATION , General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Could not start the Installer, " + err.Message, General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //return EmbeddedAssembly.Get(args.Name);
            return EmbeddedAssembly.Get("SimpleEncrypt, Version=1.0.3048.24602, Culture=neutral, PublicKeyToken=null");
        }

        private static IntPtr GetOwnerWindow(string processname)
        {
            //Get the process Hwnd

            IntPtr hwnd = IntPtr.Zero;
            Process[] procs = Process.GetProcessesByName(processname);

            foreach (Process pr in procs)
            {
                if (pr.MainWindowHandle != null)
                {
                    hwnd = pr.MainWindowHandle;
                    //if (hwnd != IntPtr.Zero)
                    //{
                    //    if (pr.MainWindowTitle.ToUpper().Contains(General.MYAPPLICATION.ToUpper()))
                    //    {
                    //        break;
                    //    }
                    //}
                    break;
                }
            }
            return hwnd;
        }
    }
}
