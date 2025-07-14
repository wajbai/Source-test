using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace AcMEERPUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!IsAcmeerpUpdaterAlreadyRunning())
            {
                if (EmbeddedAssembly.Load("AcMEERPUpdater.Resources.MySql.Data.dll", "MySql.Data.dll"))
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                }

                if (EmbeddedAssembly.Load("AcMEERPUpdater.Resources.SimpleEncrypt.dll", "SimpleEncrypt.dll"))
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve1);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                General.IS_SILENT_INSTALLATION = true;
                Application.Run(new frmUpdater());
            }
            else
            {
                MessageBox.Show("Acme.erp Updater is already running", General.ACMEERP_UPDATER_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve1(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        public static bool IsAcmeerpUpdaterAlreadyRunning()
        {
            bool Rtn = false;
            try
            {
                Process[] processByName = Process.GetProcessesByName("AcMEERPUpdater");
                Rtn = (processByName.Length > 1);
            }
            catch (Exception err)
            {
                General.WriteLog("Error in IsAcMEERPRunning " + err.Message);
            }
            return Rtn;
        }
    }
}