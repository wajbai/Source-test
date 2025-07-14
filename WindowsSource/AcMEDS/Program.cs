using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using AcMEDSync;
using System.Threading;

namespace AcMEDS
{
    static class Program
    {
        private static string appGuid = "AcMEDS";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            //if (arg.Length > 0)
            //{
            //    using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            //    {
            //        if (!mutex.WaitOne(0, false))
            //        {
            //            MessageBox.Show("Instance already running", Common.ACMEDS_TITLE);
            //            return;
            //        }

            //        Application.EnableVisualStyles();
            //        Application.SetCompatibleTextRenderingDefault(false);
            //        Application.Run(new frmTrayIcon());
            //    }
            //}
            //else
            //{
            //    ServiceBase[] ServicesToRun;
            //    ServicesToRun = new ServiceBase[] { new AcMEDS() };
            //    ServiceBase.Run(ServicesToRun);
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmTestVoucher());

        }
    }
}
