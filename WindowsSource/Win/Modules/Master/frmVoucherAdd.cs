/************************************************************************************************************************
 *                                              Form Name  :frmVoucherAdd.cs
 *                                              Purpose    :To Add or Update the Voucher details
 *                                              Author     : Carmel Raj M
 *                                              Created On :02-Sep-2013
 * 
 * **********************************************************************************************************************/
using System;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.ConfigSetting;

namespace ACPP.Modules.Master
{
    public partial class frmVoucherAdd : frmFinanceBaseAdd
    {
        #region Variables
        private int VoucherId = 0;
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor

        /// <summary>
        /// This default constructor is used to add the new Voucher details
        /// when the default constructor is called the default value of the 
        /// VoucherId is taken
        /// </summary>
        public frmVoucherAdd()
        {
            InitializeComponent();

        }

        /// <summary>
        /// To override the default value of the VoucherId as new VoucherId
        /// This is used in edit mode
        /// </summary>
        /// <param name="VoucherId">New Voucher Id  </param>
        public frmVoucherAdd(int VoucherId)
            : this()
        {
            this.VoucherId = VoucherId;
        }

        #endregion

        #region Events

        /// <summary>
        /// Load events of the Voucher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVoucherAdd_Load(object sender, EventArgs e)
        {
            this.Text = SetTitle();
            lkpApplicableFrom.Enabled = false;
            BindLookupEditControls();
            //This method is to assign the details of the pariticular VoucherId
            AssignVoucherDetails();
        }

        /// <summary>
        /// Saves or Updates the voucher details based on the VoucherId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateUserInput())
            {
                using (VoucherSystem voucherSystem = new VoucherSystem())
                {
                    ResultArgs resultArgs;
                    voucherSystem.VoucherName = txtVoucherName.Text.Trim();
                    voucherSystem.VoucherId = this.VoucherId.Equals(0) ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : this.VoucherId;
                    voucherSystem.VoucherType = lkpVoucherType.EditValue.ToString();
                    voucherSystem.VoucherMethod = lkpVoucherMethod.EditValue.ToString();
                    voucherSystem.PrefixCharacter = txtPrefixCharacter.Text.Trim();
                    voucherSystem.SuffixCharacter = txtSufixCharacter.Text.Trim();
                    voucherSystem.StartingNumber = this.UtilityMember.NumberSet.ToInteger(txtStartingNumber.Text.Trim());
                    voucherSystem.NumericalWith = this.UtilityMember.NumberSet.ToInteger(txtNumericalWidth.Text.Trim());
                    voucherSystem.PrefixWithZero = rgbPrefixWidthZero.SelectedIndex.Equals((int)YesNo.Yes) ? (int)YesNo.Yes : (int)YesNo.No;
                    voucherSystem.Month = lkpApplicableFrom.ItemIndex.ToString();
                    voucherSystem.Duration = this.UtilityMember.NumberSet.ToInteger(lkpResetAfter.Text.Trim());
                    voucherSystem.Allow_Duplicate = rgbAllowDuplicate.SelectedIndex.Equals((int)YesNo.Yes) ? (int)YesNo.Yes : (int)YesNo.No;
                    voucherSystem.Note = meNote.Text;
                    if (this.UtilityMember.NumberSet.ToInteger(lkpVoucherType.EditValue.ToString()) == (int)DefaultVoucherTypes.Contra)
                    {
                        voucherSystem.NarrationEnabled = 0;
                    }
                    else
                    {
                        voucherSystem.NarrationEnabled = chkEnableEntryNarration.Checked == true ? 1 : 0;
                    }
                    resultArgs = voucherSystem.SaveVocherDetails();
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        ClearControls();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// Sets the border color if the Textbox is empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtVoucherName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtVoucherName);
            txtVoucherName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtVoucherName.Text);
        }

        private void lkpVoucherMethod_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToInteger(lkpVoucherMethod.EditValue.ToString()) == (int)TransactionVoucherMethod.Manual)
            {
                txtNumericalWidth.Text = txtPrefixCharacter.Text = txtSufixCharacter.Text = txtStartingNumber.Text = rgbPrefixWidthZero.Text = string.Empty;
                txtNumericalWidth.Enabled = txtPrefixCharacter.Enabled = txtSufixCharacter.Enabled = txtStartingNumber.Enabled = rgbPrefixWidthZero.Enabled = false;
                rgbAllowDuplicate.Enabled = true;
            }
            else
            {
                txtNumericalWidth.Enabled = txtPrefixCharacter.Enabled = txtSufixCharacter.Enabled = txtStartingNumber.Enabled = rgbPrefixWidthZero.Enabled = true;
                rgbAllowDuplicate.Enabled = false;
                txtStartingNumber.Text = "1";
            }
        }

        private void lkpVoucherType_EditValueChanged(object sender, EventArgs e)
        {
            //On 27/08/2019, to show enable narration feature for all voucher types (Receipts, Payments, Contra and Journal)----------------------
            //this.Height = 393;
            //lciEnablenarration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //if (this.UtilityMember.NumberSet.ToInteger(lkpVoucherType.EditValue.ToString()) == (int)DefaultVoucherTypes.Journal)
            //{
            //    lciEnablenarration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //   // this.Height = 380;
            //}

            lciEnablenarration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (this.UtilityMember.NumberSet.ToInteger(lkpVoucherType.EditValue.ToString()) == (int)DefaultVoucherTypes.Contra)
            {
                lciEnablenarration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // this.Height = 380;
            }

            //--------------------------------------------------------------------------------------------------------------------------------------
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the Title for the form
        /// </summary>
        /// <returns>Title of the form</returns>
        private string SetTitle()
        {
            return VoucherId.Equals(0) ? this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_EDIT_CAPTION);
        }

        /// <summary>
        /// Binds the LookUpEdit control
        /// </summary>
        private void BindLookupEditControls()
        {
            try
            {
                DefaultVoucherTypes voucherType = new DefaultVoucherTypes();
                TransactionVoucherMethod vouchermethod = new TransactionVoucherMethod();
                Month month = new Month();
                BindEnumValue(lkpVoucherType, voucherType, EnumColumns.Name.ToString(), EnumColumns.Id.ToString(), 0);
                BindEnumValue(lkpVoucherMethod, vouchermethod, EnumColumns.Name.ToString(), EnumColumns.Id.ToString(), 0);
                BindEnumValue(lkpApplicableFrom, month, EnumColumns.Name.ToString(), EnumColumns.Name.ToString(), this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false).Month);
                lkpApplicableFrom.EditValue = lkpApplicableFrom.Properties.GetDataSourceValue(lkpApplicableFrom.Properties.ValueMember, this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false).Month - 1);
                BindEnumValue(lkpResetAfter, month, EnumColumns.Id.ToString(), EnumColumns.Id.ToString(), 1);
            }
            catch (Exception e)
            {
                MessageRender.ShowMessage(e.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// Binds enum values to LookUpEdit control
        /// </summary>
        /// <param name="lkpEdit">look up object to be binded</param>
        /// <param name="enumType">enum object</param>
        /// <param name="DisplayMember"> Display member of the lookup edit</param>
        /// <param name="ValueMember">Value member of the lookup edit</param>
        /// <param name="HidIndex">Index of the column to be hidden in the LookUpEdit</param>
        private void BindEnumValue(LookUpEdit lkpEdit, Enum enumType, string DisplayMember, string ValueMember, int HideIndex)
        {
            EnumSetMember eumSetMembers = new EnumSetMember();
            //To convert the enum type to DataSouce and binds it to the LookUpEdit
            this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpEdit, eumSetMembers.GetEnumDataSource(enumType, Sorting.None).ToTable(), DisplayMember, ValueMember);
            lkpEdit.EditValue = lkpEdit.Properties.GetDataSourceValue(lkpEdit.Properties.ValueMember, 0);
        }

        /// <summary>
        /// Validate the mandatory field
        /// </summary>
        /// <returns>return true if the Validation is Successful otherwise returns false</returns>
        private bool ValidateUserInput()
        {
            bool IsValidated = true;
            if (string.IsNullOrEmpty(txtVoucherName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_NAME_EMPTY));
                this.SetBorderColor(txtVoucherName);
                txtVoucherName.Focus();
                IsValidated = false;
            }
            else if (string.IsNullOrEmpty(lkpVoucherType.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_TYPE_EMPTY));
                this.SetBorderColorForLookUpEdit(lkpVoucherType);
                lkpVoucherType.Focus();
                IsValidated = false;
            }
            else if (string.IsNullOrEmpty(lkpVoucherMethod.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Voucher.VOUCHER_METHOD_EMPTY));
                this.SetBorderColorForLookUpEdit(lkpVoucherMethod);
                lkpVoucherMethod.Focus();
                IsValidated = false;
            }
            else if (!ValidateReceiptModuleRights())
            { //03/05/2022 to validate Receipt Module rights
                IsValidated = false;
                lkpVoucherMethod.EditValue = ((int)TransactionVoucherMethod.Automatic).ToString();
            }
            return IsValidated;
        }

        /// <summary>
        /// On 02/05/2022, to validate and prompt proper message for sdbinm or locking receipt module 
        /// </summary>
        /// <returns></returns>
        private bool ValidateReceiptModuleRights()
        {
            bool rtn = true;
            string msg = string.Empty;
            try
            {
                if (this.AppSetting.IS_SDB_INM )
                {
                    if (this.UtilityMember.NumberSet.ToInteger(lkpVoucherType.EditValue.ToString()) == (int)DefaultVoucherTypes.Receipt &&
                        (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= UtilityMember.DateSet.ToDate(SettingProperty.Enforce_Receipt_Module_FY, false)))
                    {
                        if (this.UtilityMember.NumberSet.ToInteger(lkpVoucherMethod.EditValue.ToString()) == (int)TransactionVoucherMethod.Manual)
                        {
                            msg = MessageCatalog.Common.COMMON_RECEIPT_DISABLED_CHANGE_RECEIPT_METHOD;
                            rtn = false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
                rtn = false;
            }
            finally
            {
                if (this.AppSetting.IS_SDB_INM && !rtn && !string.IsNullOrEmpty(msg))
                {
                    this.ShowMessageBox(msg);
                }
                else
                {
                    rtn = true;
                }
            }

            return rtn;
        }


        /// <summary>
        /// Assigns the details of the voucher by its Id
        /// </summary>
        private void AssignVoucherDetails()
        {
            try
            {
                if (VoucherId != 0)
                {
                    using (VoucherSystem voucherSystem = new VoucherSystem(VoucherId))
                    {
                        txtVoucherName.Text = voucherSystem.VoucherName;
                        txtPrefixCharacter.Text = voucherSystem.PrefixCharacter;
                        txtSufixCharacter.Text = voucherSystem.SuffixCharacter;
                        txtStartingNumber.Text = voucherSystem.StartingNumber.Equals(0) ? "1" : voucherSystem.StartingNumber.ToString();
                        txtNumericalWidth.Text = voucherSystem.NumericalWith.Equals(0) ? string.Empty : voucherSystem.NumericalWith.ToString();
                        rgbAllowDuplicate.SelectedIndex = voucherSystem.Allow_Duplicate.Equals((int)YesNo.Yes) ? (int)YesNo.Yes : (int)YesNo.No;
                        rgbPrefixWidthZero.SelectedIndex = voucherSystem.PrefixWithZero.Equals((int)YesNo.Yes) ? (int)YesNo.Yes : (int)YesNo.No;
                        //  lkpApplicableFrom.ItemIndex = this.UtilityMember.NumberSet.ToInteger(voucherSystem.Month);
                        lkpResetAfter.EditValue = voucherSystem.Duration.ToString();
                        lkpVoucherMethod.EditValue = voucherSystem.VoucherMethod;
                        lkpVoucherType.EditValue = voucherSystem.VoucherType;
                        meNote.Text = voucherSystem.Note;
                        chkEnableEntryNarration.Checked = voucherSystem.NarrationEnabled == 1 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// Clears all the controls and foucus to the first control
        /// </summary>
        private void ClearControls()
        {
            if (VoucherId.Equals(0))
            {
                txtVoucherName.Text = txtSufixCharacter.Text = txtPrefixCharacter.Text = txtStartingNumber.Text = meNote.Text = txtNumericalWidth.Text = string.Empty;
                txtStartingNumber.Text = "1";
                lkpVoucherMethod.Properties.DataSource = null;
                lkpVoucherType.Properties.DataSource = null;
                // lkpApplicableFrom.Properties.DataSource = null;
                lkpResetAfter.Properties.DataSource = null;
                rgbAllowDuplicate.SelectedIndex = 0;
                rgbPrefixWidthZero.SelectedIndex = 0;
                lkpVoucherType.Properties.NullText = lkpVoucherType.Properties.NullText;
                lkpVoucherMethod.Properties.NullText = lkpVoucherMethod.Properties.NullText;
                BindLookupEditControls();
                chkEnableEntryNarration.Checked = false;
            }
            txtVoucherName.Focus();
        }

        #endregion


    }
}