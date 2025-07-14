using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorSMSCredits : frmFinanceBase
    {
        #region Constructor
        public frmDonorSMSCredits()
        {
            InitializeComponent();
        }
        public frmDonorSMSCredits(DataTable dtDonorData)
            : this()
        {
            dtDonorCredits = dtDonorData;
        }
        #endregion

        #region Properties
        private DataTable dtDonorCredits { get; set; }
        #endregion

        #region Methods
        private void Loaddata()
        {
            if (dtDonorCredits!=null && dtDonorCredits.Rows.Count>0)
            {
                gcDonorCredits.DataSource = dtDonorCredits;    
            }
        }
        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmDonorSMSCredits_Load(object sender, EventArgs e)
        {
            Loaddata();
        }
    }
}