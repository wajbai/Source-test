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
    public partial class frmSalesView : frmBase
    {
        public frmSalesView()
        {
            InitializeComponent();
        }

        private void ucTransViewOpeningBalDetails1_Load(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            frmSales objSales = new frmSales();
            objSales.ShowDialog();
        }

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            frmSales objSales = new frmSales();
            objSales.ShowDialog();
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

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {

        }

        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {

        }
    }
}
