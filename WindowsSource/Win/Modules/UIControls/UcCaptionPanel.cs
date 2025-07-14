using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Frames;

namespace ACPP.Modules.UIControls
{
	public partial class UcCaptionPanel: UserControl
	{
        ApplicationCaption8_1 captionPanel;
		
        #region Properties
        public string Caption
        {
            set { captionPanel.Text = value; }
        }

        public int CaptionSize
        {
            set { captionPanel.Font = new Font("", value,FontStyle.Bold); }
        }


        #endregion



        public UcCaptionPanel()
		{
			InitializeComponent();
            InitializeTitlePanel();
		}

        private void InitializeTitlePanel()

        {
            System.ComponentModel.ComponentResourceManager resources = new DevExpress.ExpressApp.Win.Templates.XafComponentResourceManager(typeof(UcCaptionPanel));
            captionPanel = new ACPP.frmMain.AppCpation();
            resources.ApplyResources(this.captionPanel, "captionPanel");
            captionPanel.MinimumSize = new System.Drawing.Size(1000, 25);
            captionPanel.Font = new Font("", 20);
            captionPanel.Name = "captionPanel";
            captionPanel.Text = "";
            captionPanel.TabStop = false;
            captionPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            pnlTitle.Controls.Add(captionPanel);
            //pnlTitle.Controls.Add(captionPanel);
            captionPanel.Location = new Point(-15, 0);
        }
	}
}
