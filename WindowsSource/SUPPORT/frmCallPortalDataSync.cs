using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AcMEDSync.Model;

namespace SUPPORT
{
    public partial class frmCallPortalDataSync : DevExpress.XtraEditors.XtraForm
    {
        public frmCallPortalDataSync()
        {
            InitializeComponent();
        }

        private void btnCallPortalDataSync_Click(object sender, EventArgs e)
        {
            using (BalanceSystem portaldatasync = new BalanceSystem())
            {
                portaldatasync.VoucherDate = "2019-01-01 00:00:00";
                portaldatasync.UpdateBulkTransBalance(21, true);
            }
        }
    }
}