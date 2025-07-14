using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AcMEDSync;
using System.ServiceProcess;
using System.IO;
using System.Diagnostics;

namespace AcMEDS
{
    public partial class frmTrayIcon : Form
    {
        public frmTrayIcon()
        {
            InitializeComponent();

            trayIcon.BalloonTipText = Common.ACMEDS_TITLE + " Installed";
            trayIcon.BalloonTipTitle = Common.ACMEDS_TITLE;
            trayIcon.Text = Common.ACMEDS_TITLE;
            trayIcon.Visible = true;
        }

       
        private void frmTryIcon_Load(object sender, EventArgs e)
        {
            trayIcon.ShowBalloonTip(100);
            WindowState = FormWindowState.Minimized;
            Hide();
            this.ShowInTaskbar = false;
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            ValidateTrayMenu();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
                        
        private void mnuCreate_Click(object sender, EventArgs e)
        {
            if (Common.CreateService())
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " Created", ToolTipIcon.Info);
            }
            else
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " not Created", ToolTipIcon.Info);
            }
            ValidateTrayMenu();
        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            if (Common.StartService())
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " Started", ToolTipIcon.Info);
            }
            else
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " not Started", ToolTipIcon.Info);
            }
            ValidateTrayMenu();
        }

        private void mnuStop_Click(object sender, EventArgs e)
        {
            if (Common.StopService())
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " Stopped", ToolTipIcon.Info);
            }
            else
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " not Stopped", ToolTipIcon.Info);
            }
            ValidateTrayMenu();
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            if (Common.RemoveService())
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " Removed", ToolTipIcon.Info);
            }
            else
            {
                trayIcon.ShowBalloonTip(100, Common.ACMEDS_TITLE, Common.ACMEDS_TITLE + " not Rmoved", ToolTipIcon.Info);
            }
            ValidateTrayMenu();
        }

        public void ValidateTrayMenu()
        {
            mnuCreate.Enabled = false;
            mnuRemove.Enabled = false;
            mnuStart.Enabled = false;
            mnuStop.Enabled = false;

            using (ServiceController service = new ServiceController(Common.ACMEDS_SERVICE_NAME))
            {
                try
                {
                    switch (service.Status)
                    {
                        case ServiceControllerStatus.Running:
                            {
                                mnuStop.Enabled = true;
                                break;
                            }
                        case ServiceControllerStatus.Stopped:
                            {
                                mnuStart.Enabled = true;
                                mnuRemove.Enabled = true;
                                break;
                            }
                    }
                }
                catch (InvalidOperationException erServiceNotFound)
                {
                    string sErrMsg = erServiceNotFound.Message;
                    mnuCreate.Enabled = true;
                }
            }
        }
    }
}
