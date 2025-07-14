using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACPP.Modules.UIControls
{
    public partial class UCTDSPayments : DevExpress.XtraEditors.XtraUserControl
    {
        public event EventHandler GetPendingDetails;

       // public string changeCaption = string.Empty;
        public string ChangeCaptionName
        {
           // get { return bbiTDSPendingPayments.Caption; }
            set { bbiTDSPendingPayments.Caption = value; }
        }


        public UCTDSPayments()
        {
            InitializeComponent();
        }

        private void bbiTDSPendingPayments_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (GetPendingDetails != null)
            {
                GetPendingDetails(this, e);
            }
        }

    }
}
