using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using Bosco.Utility;
using ACPP.Modules.Dsync;


using ACPP.Modules.Master;

namespace ACPP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.CurrentCulture = new CultureInfo("en-GB");

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("iMaginary");
            Application.SetCompatibleTextRenderingDefault(false);

            //Check Acmeerp Updater Running, prompt message
            if (!Bosco.Utility.Common.clsGeneral.IsAcmeerpUpdaterRunning())
            {
                //Register
                if (EmbeddedAssembly.Load("Bosco.Utility.Resources.SimpleEncrypt.dll", "SimpleEncrypt.dll"))
                {
                    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                }
                //on 15/02/2018, to show caps lock tooltip for Temp purpose (if anthing changed in design, we can remove and we have to handle manually for password text boxes
                Application.EnableVisualStyles();
                frmAcMELogin login = new frmAcMELogin();
                login.TopMost = true;

                if (login.ShowDialog() == DialogResult.OK)
                {
                    //  Application.Run(new ACPP.Modules.TDS.frmPaymentsParty());
                    try
                    {

                        Application.Run(new frmMain());
                        // Application.Run(new frmTest()); 
                    }
                    catch (Exception ex)
                    {
                        MessageRender.ShowMessage("Could not proceed, " + ex.Message);
                        //Application.Run(new frmMain()); 
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                MessageRender.ShowMessage("Acme.erp Updater is running. Open Acme.erp after Updater is completed");
                Application.Exit();
            }

        }

        private static bool CheckEncryptComponent()
        {
            bool Rtn = false;
            try
            {
                SimpleEncrypt.SimpleEncDec test = new SimpleEncrypt.SimpleEncDec();
                Rtn = true;
            }
            catch
            {
                Rtn = false;
            }
            return Rtn;
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //return EmbeddedAssembly.Get(args.Name);
            return EmbeddedAssembly.Get("SimpleEncrypt, Version=1.0.3048.24602, Culture=neutral, PublicKeyToken=null");
        }
    }
}
