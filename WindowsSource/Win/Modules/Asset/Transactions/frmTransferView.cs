using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACPP.Modules.Asset.Transactions
{
    public partial class frmTransferView : frmBase
    {
        public frmTransferView()
        {
            InitializeComponent();
        }

        private void frmTransferView_Load(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            frmTransfer objtransfer = new frmTransfer();
            objtransfer.ShowDialog();
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_DownloadExcel(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            frmTransfer objtransfer = new frmTransfer();
            objtransfer.ShowDialog();
        }

        private void ucToolBar1_MoveTransaction(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_NatureofPayments(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_NegativeBalanceClicked(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {

        }
    }
}
