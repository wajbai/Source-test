using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACPP.Modules.UIControls
{
	public partial class ucDashboard: UserControl
	{

        public EventHandler RefreshClicked;

		public ucDashboard()
		{
			InitializeComponent();
		}

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (RefreshClicked != null)
            {
                RefreshClicked(this, e);
            }
        }
	}
}
