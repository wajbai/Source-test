using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmInsuranceVoucher : frmBase
    {
        #region VariableDeclaration

        #endregion

        #region Constructor
        public frmInsuranceVoucher()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        public void FetchInsurancePlan()
        {
        }
        #endregion

        
    }
}