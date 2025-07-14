using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;


namespace ACPP
{
    public partial class frmBankAdd : frmFinanceBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelaration
        private int bankId = 0;
        private ResultArgs resultArgs = null;
        //  string Existing_Bank_Code = string.Empty;
        #endregion

        #region Constructor

        public frmBankAdd()
        {
            InitializeComponent();
        }

        public frmBankAdd(int BankId)
            : this()
        {
            bankId = BankId;
            AssignBankDetails();
        }
        #endregion

        #region Events

        /// <summary>
        /// Load bank details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmBankAdd_Load(object sender, EventArgs e)
        {
            //SuggestCode();
            SetTitle();

            // LoadBankCodes();
        }

        /// <summary>
        /// Save bank details
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBankDetails())
                {
                    ResultArgs resultArgs = null;
                    using (BankSystem bankSystem = new BankSystem())
                    {
                        bankSystem.BankId = bankId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : bankId;
                        bankSystem.BankCode = txtBankCode.Text.Trim().ToUpper();
                        bankSystem.BankName = txtBankName.Text.Trim();
                        bankSystem.Branch = txtBranch.Text.Trim();
                        bankSystem.Address = txtMemoAddress.Text.Trim();
                        bankSystem.BSRCode = txtBSRCode.Text.Trim();
                        bankSystem.IFSCCode = txtIFSCCode.Text.Trim();
                        bankSystem.MICRCode = txtMICRCode.Text.Trim();
                        bankSystem.ContactNumber = txtContactNumber.Text.Trim();
                        //bankSystem.AccountName = txtAccountName.Text.Trim();
                        bankSystem.SWIFTCode = txtSWIFTCODE.Text.Trim();
                        bankSystem.Notes = txtmeNotes.Text.Trim();
                        resultArgs = bankSystem.SaveBankDetails();

                        if (resultArgs.Success)
                        {
                            if (this.AppSetting.EnableChequePrinting == "1")
                            {
                                if (bankId == 0) //assing recent bankid for new insert
                                {
                                    this.ReturnValue = resultArgs.RowUniqueId;
                                    bankId = UtilityMember.NumberSet.ToInteger(this.ReturnValue.ToString());
                                    SetTitle();
                                }
                            }
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            txtBankCode.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Fire the bank code is empty and sets border color for controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //private void txtBankCode_Leave(object sender, EventArgs e)
        //{
        //    this.SetBorderColor(txtBankCode);
        //    if (bankId == 0 || txtBankCode.Text!=Existing_Bank_Code)
        //    {
        //        if (!string.IsNullOrEmpty(txtBankCode.Text.Trim()))
        //        {
        //            ucBankUsedCodes.FetchExistCodes(MapForm.Bank, txtBankCode.Text);
        //        }
        //    }
        //    else
        //    {
        //        ucBankUsedCodes.ClearLedgerName = string.Empty;
        //    }
        //   // ucBankUsedCodes.FetchCodes(MapForm.Bank);
        //}

        /// <summary>
        /// Fire the bank code is empty and sets border color for controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtBankName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBankName);
            txtBankName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtBankName.Text);
        }

        /// <summary>
        /// Fire the bank code is empty and sets border color for controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtBranch_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBranch);
            txtBranch.Text = this.UtilityMember.StringSet.ToSentenceCase(txtBranch.Text);
        }

        /// <summary>
        /// close the bank form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Purpose:To validate the mandatory fields.
        /// </summary>
        /// <returns></returns>

        public bool ValidateBankDetails()
        {
            bool isBankTrue = true;
            //if (string.IsNullOrEmpty(txtBankCode.Text.Trim()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_CODE_EMPTY));
            //    this.SetBorderColor(txtBankCode);
            //    isBankTrue = false;
            //    txtBankCode.Focus();
            //}
            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_NAME_EMPTY));
                this.SetBorderColor(txtBankName);
                isBankTrue = false;
                txtBankName.Focus();
            }
            else if (string.IsNullOrEmpty(txtBranch.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_BRANCH_EMPTY));
                this.SetBorderColor(txtBranch);
                isBankTrue = false;
                txtBranch.Focus();
            }
            else
            {
                txtMemoAddress.Focus();
            }
            return isBankTrue;
        }

        /// <summary>
        /// Purpose: Load the values to the controls while editing.
        /// </summary>
        /// <param name="sender">Assign the bank details to forms </param>
        /// <param name="e"></param>

        public void AssignBankDetails()
        {
            try
            {
                if (bankId != 0)
                {
                    using (BankSystem bankSystem = new BankSystem(bankId))
                    {
                        txtBankCode.Text = bankSystem.BankCode;
                        txtBankName.Text = bankSystem.BankName;
                        txtBranch.Text = bankSystem.Branch;
                        txtMemoAddress.Text = bankSystem.Address;
                        txtBSRCode.Text = bankSystem.BSRCode;
                        txtIFSCCode.Text = bankSystem.IFSCCode;
                        txtMICRCode.Text = bankSystem.MICRCode;
                        txtContactNumber.Text = bankSystem.ContactNumber;
                        txtSWIFTCODE.Text = bankSystem.SWIFTCode;
                        txtmeNotes.Text = bankSystem.Notes;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Clear the controls
        /// </summary>

        public void ClearControls()
        {
            if (bankId == 0)
            {
                txtBankCode.Text = txtBankName.Text = txtBranch.Text = txtMemoAddress.Text = txtBSRCode.Text = txtIFSCCode.Text = txtMICRCode.Text = txtContactNumber.Text = txtmeNotes.Text = txtSWIFTCODE.Text = string.Empty;
            }

            lcChequePrinting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (bankId > 0 && this.AppSetting.EnableChequePrinting == "1")
            {
                lcChequePrinting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcNew.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }


            txtBankCode.Focus();
        }

        /// <summary>
        /// Set Form Title for Bank
        /// </summary>
        /// <param name="BankId"></param>

        private void SetTitle()
        {
            this.Text = bankId == 0 ? this.GetMessage(MessageCatalog.Master.Bank.BANK_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Bank.BANK_EDIT_CAPTION);
            txtBankCode.Focus();
        }
        /// <summary>
        /// Load the bank codes in the gridlookupedit
        /// </summary>
        //private void LoadBankCodes()
        //{
        //    try
        //    {
        //        string code="";
        //        using (BankSystem bankSystem = new BankSystem())
        //        {
        //            resultArgs = bankSystem.FetchBankCodes();
        //            if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
        //            {
        //                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAvailableBankCodes, resultArgs.DataSource.Table, bankSystem.AppSchema.Bank.BANK_CODEColumn.ColumnName, bankSystem.AppSchema.Bank.BANK_CODEColumn.ColumnName);
        //                glkpAvailableBankCodes.EditValue = glkpAvailableBankCodes.Properties.GetKeyValue(0);
        //                if (bankId == 0)
        //                {
        //                    code = CodePredictor(glkpAvailableBankCodes.Properties.GetKeyValue(0).ToString(),resultArgs.DataSource.Table);
        //                    //  for (int i = 0; i < resultArgs.DataSource.Table.Rows.Count; i++)
        //                    //  {
        //                    //      if (resultArgs.DataSource.Table.Rows[i]["bank_code"].ToString().Equals(code))
        //                    //      {
        //                    //          code = CodePredictor(code);
        //                    //          i = 0;
        //                    //      }
        //                    //  }
        //                    ////  txtBankCode.Text = glkpAvailableBankCodes.EditValue != null ? CodePredictor(glkpAvailableBankCodes.Properties.GetKeyValue(0).ToString()) : "001";
        //                    txtBankCode.Text = code;
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string excepting = ex.ToString();
        //    }
        //}

        private void txtMemoAddress_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtMemoAddress_Leave(object sender, EventArgs e)
        {
            txtMemoAddress.Text = this.UtilityMember.StringSet.ToSentenceCase(txtMemoAddress.Text);
        }

        //private void ucBankUsedCodes_Click(object sender, EventArgs e)
        //{

        //}

        //private void ucBankUsedCodes_Iconclicked(object sender, EventArgs e)
        //{
        //    ucBankUsedCodes.FetchCodes(MapForm.Bank);
        //}

        private void txtBankCode_Click(object sender, EventArgs e)
        {
            //  ucBankUsedCodes.FetchCodes(MapForm.Bank);
        }

        private void btnChequePrinting_Click(object sender, EventArgs e)
        {
            if (bankId > 0)
            {
                frmChequePrintingSetting frmchequeprinting = new frmChequePrintingSetting(bankId, txtBankName.Text.Trim(), txtBranch.Text.Trim());
                frmchequeprinting.ShowDialog();
            }
        }

        private void frmBankAdd_Activated(object sender, EventArgs e)
        {

        }

        private void frmBankAdd_Shown(object sender, EventArgs e)
        {
            lcChequePrinting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcNew.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if ((this.AppSetting.EnableChequePrinting == "1") && bankId > 0)
            {
                lcChequePrinting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcNew.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bankId = 0;
            ClearControls();
            SetTitle();
        }

        //private void SuggestCode()
        //{
        //    string Code = "AB12CD";
        //    string[] resultString;
        //    resultString = Regex.Split(Code, @"\D+");
        //}

        #endregion
    }
}