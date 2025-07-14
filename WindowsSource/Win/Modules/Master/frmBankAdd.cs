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
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;

namespace ACPP
{
    public partial class frmBankAdd : frmbaseAdd
    {

        #region Variable Decelaration
        private int MasterBankId = 0;
        frmBankView frmBank = new frmBankView();
        #endregion

        #region Constructor

        public frmBankAdd()
        {
            InitializeComponent();
        }

        public frmBankAdd(frmBankView frmbnk, int BankId)
        {
            InitializeComponent();
            frmBank = frmbnk;
            MasterBankId = BankId;
        }
        #endregion

        #region Events

        private void frmBankAdd_Load(object sender, EventArgs e)
        {
            if (MasterBankId != 0) { UpdateBank(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBankDetails())
                {
                    ResultArgs resultArgs = null;
                    using (MasterSystem.Bank masterSystem = new MasterSystem.Bank())
                    {
                        if (MasterBankId == 0)
                        {
                            resultArgs = masterSystem.InsertBankDetails(txtBankCode.Text, txtBankName.Text, txtBranch.Text, meAddress.Text);
                        }
                        else
                        {
                            resultArgs = masterSystem.UpdateBankDetails(MasterBankId, txtBankCode.Text, txtBankName.Text, txtBranch.Text, meAddress.Text);
                        }
                        if (resultArgs.Success)
                        {
                            lblResponseMsg.Text = this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.BANK_SAVED_SUCCESS);
                            ClearControls();
                            frmBank.LoadBank();
                        }
                        else
                        {
                            XtraMessageBox.Show(this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.BANK_SAVED_FAILURE), this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Added by:Michael r
        /// Date:07-08-2013
        /// Purpose:To validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        public bool ValidateBankDetails()
        {
            bool isBankTrue = false;
            if (string.IsNullOrEmpty(txtBankCode.Text))
            {
                XtraMessageBox.Show(this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.BANK_CODE_EMPTY), this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBankCode.Focus();
            }
            else if (string.IsNullOrEmpty(txtBankName.Text))
            {
                XtraMessageBox.Show(this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.BANK_NAME_EMPTY), this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBankName.Focus();
            }
            else if (string.IsNullOrEmpty(txtBranch.Text))
            {
                XtraMessageBox.Show(this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.BANK_BRANCH_EMPTY), this.GetMessageCatalog(Bosco.Utility.MessageCatalog.Master.Bank.MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBranch.Focus();
            }
            else
            {
                isBankTrue = true;
            }
            return isBankTrue;
        }
        /// <summary>
        /// Added by:Michael R
        /// Date:07-08-2013
        /// Purpose: Load the values to the controls while editing.
        /// </summary>
        /// <param name="sender">Assign the bank details to forms </param>
        /// <param name="e"></param>
        public void UpdateBank()
        {
            using (MasterSystem.Bank masterSystem = new MasterSystem.Bank())
            {
                ResultArgs resultArgs = masterSystem.FetchBankDetailsById(MasterBankId);
                DataTable dtBank = resultArgs.DataSource.Table;
                if (dtBank != null && dtBank.Rows.Count != 0)
                {
                    txtBankCode.Text = dtBank.Rows[0]["ABBREVATION"].ToString();
                    txtBankName.Text = dtBank.Rows[0]["BANK"].ToString();
                    txtBranch.Text = dtBank.Rows[0]["BRANCH"].ToString();
                    meAddress.Text = dtBank.Rows[0]["ADDRESS"].ToString();
                }
            }
        }
        public void ClearControls()
        {
            txtBankCode.Text = txtBankName.Text = txtBranch.Text = meAddress.Text = string.Empty;
            txtBankCode.Focus();
        }
        #endregion
    }
}