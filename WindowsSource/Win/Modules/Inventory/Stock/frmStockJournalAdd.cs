using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockJournalAdd : frmFinanceBaseAdd
    {
        #region Variable Decleration
        int VoucherId = 0;
        #endregion
        public frmStockJournalAdd()
        {
            InitializeComponent();
        }

        private void frmStockJournalAdd_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        #region Methods
        private void ClearControls()
        {
            txtNarration.Text = string.Empty;
        }

        private void SetTitle()
        {
            //this.Text = VoucherId == 0 ? this.GetMessage(MessageCatalog.Stock.StockCategory.STOCKCATEGORY_VIEW_CAPTION) : this.GetMessage(MessageCatalog.Stock.StockCategory.STOCKCATEGORY_VIEW_CAPTION);
        }
        #endregion
    }
}