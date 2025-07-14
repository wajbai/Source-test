using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;
using AcMEDSync;
using System.Management;
using System.ServiceProcess;
using System.Diagnostics;
using System.Reflection;
using System.IO;


namespace AcMEDS
{
    [RunInstaller(true)]
    public partial class AcMEDSInstaller : System.Configuration.Install.Installer
    {
        public AcMEDSInstaller()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\AcMEDS.exe","AcMEDS Trayicon");
        }

        private void AcMEDSInstaller_AfterUninstall(object sender, InstallEventArgs e)
        {
            //Remove TrayIcon appliation
            try
            {
                //if (Common.StopService())
                //{
                //    Common.RemoveService();
                //}
            }
            catch (Exception err)
            {
                AcMEDataSynLog.WriteLog("Error in removing service while uninstall " + err.Message);
            }
        }
    }
}
