//Purpose: This is starting porint of the installation Kit
//
//1. Get Installation Target of the setup from setup and assing to global variable
//2. at end of the instllation, check my application lic updated and my db succesfull connected, if not so..alter error message and rollback installation

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

namespace MySQLConfigure
{
    //DIR="[TARGETDIR]\" /SDIR="[SOURCEDIR]\" /INSMODE="[INSTALLATIONMODE]"
    [RunInstaller(true)]
    public partial class SetupInstaller : System.Configuration.Install.Installer
    {
        string strInstallationTargetPath = string.Empty;
        string  strinstallmode = "1"; //default client mode;

        public SetupInstaller()
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DialogResult dialogresult = DialogResult.No;

            /*MessageBox.Show("savedState count : " + this.Context.Parameters.Count.ToString());
            MessageBox.Show("savedState keys : " + this.Context.Parameters.Keys.Count.ToString());
            MessageBox.Show("savedState values : " + this.Context.Parameters.Values.Count.ToString());

            foreach (string k in this.Context.Parameters.Keys)
            {
                MessageBox.Show("savedState key[" + k + "]= " + this.Context.Parameters[k].ToString());
            }*/

            try
            {
                AssignPath();
                
                //------------------------------------------------------------------------------------------------------------------------------------------------
                IntPtr mysetup = GetOwnerWindow();
                WindowWrapper wrapper = new WindowWrapper(mysetup);
                
                frmMySQLInstaller mysqlinstaller = new frmMySQLInstaller();
                dialogresult = mysqlinstaller.ShowDialog(wrapper);
                //--------------------------------------------------------------------------------------------------------------------------------------------------
            }
            catch (Exception err)
            {
                if (File.Exists(General.INSTALLER_LOG))
                {
                    MessageBox.Show("Could not complete installation, Contact Support Team" + err.Message, General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Could not complete installation, Contact Support Team" + err.Message, General.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            bool abortinstalltion = false;
            using (DBHandler dbhander = new DBHandler())
            {
                if (General.LIC_UPDATED)
                {
                    //If database is not configured propely, get confirmation from user to abort instalation or not
                    if (!dbhander.TestConnection(false))
                    {
                        DialogResult dialogresule = MessageBox.Show(General.MYAPPLICATION + " database is not configured properly, Do you want to abort the installtion ?",
                                        General.Title, MessageBoxButtons.YesNo);

                        if (dialogresule == DialogResult.Yes)
                        {
                            abortinstalltion = true;
                        }
                    }
                }
                else
                {
                    abortinstalltion = true;
                }
            }

            if (abortinstalltion)
            {
                throw new Exception(Environment.NewLine + Environment.NewLine +
                    Environment.NewLine + "Falied in installation, " + General.MYAPPLICATION + " licence/Database is not configured. Contact Support Team");
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
                        if (pr.MainWindowTitle.ToUpper().Contains(General.MYAPPLICATION.ToUpper()))   
                        {
                            /*var currentScreen = Screen.FromHandle(hwnd);
                            MessageBox.Show(currentScreen.Bounds.Width.ToString());*/
                            break;
                        }
                    }
                }
            }
            return hwnd;
        }

        /// <summary>
        /// This method is used to get target path and type of installation from setup
        /// </summary>
        private void AssignPath()
        {
            //DIR="[TARGETDIR]\" /SDIR="[SOURCEDIR]\" /INSMODE="[INSTALLATIONMODE]"
            if (this.Context.Parameters["DIR"] != null)
            {
                strInstallationTargetPath = this.Context.Parameters["DIR"].ToString();
            }

            //if (this.Context.Parameters["INSMODE"] != null)
            //{
            //    strinstallmode =  this.Context.Parameters["INSMODE"].ToString();
            //}

            strinstallmode = "1";
            General.enInstallationMode = General.InstallationMode.CLIENT;
            if (strinstallmode == "1") //Server
                General.enInstallationMode = General.InstallationMode.SERVER;

            General.TARGET_PATH = strInstallationTargetPath;
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