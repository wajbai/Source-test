using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Acme.erpSupport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
                //Register supporting files (MySql.Data, SimpleEncrypt)
                if (EmbeddedAssembly.Load("Acme.erpSupport.Resources.MySql.Data.dll", "MySql.Data.dll"))
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                }

                if (EmbeddedAssembly.Load("Acme.erpSupport.Resources.SimpleEncrypt.dll", "SimpleEncrypt.dll"))
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //if (!General.IsAcmeerpRunning())
                //{
                    Application.Run(new frmLogin());
                //}
                //else
                //{
                //    MessageRender.ShowMessage("Acme.erp is running, Close Acme.erp Application");
                //}
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
