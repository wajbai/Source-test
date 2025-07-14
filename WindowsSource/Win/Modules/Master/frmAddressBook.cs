using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel.Master;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors.Controls;
using Bosco.Utility;
namespace ACPP.Modules.Master
{
    public partial class frmAddressBook : frmFinanceBaseAdd
    {
        #region Events Declartion
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Declaration
        ResultArgs resultArgs = null;
        private int AddressId = 0;
        private int SelectedIndex = 0;
        #endregion

        #region Constructor
        public frmAddressBook()
        {
            InitializeComponent();
        }
        public frmAddressBook(int Id, int AddressIndex)
            : this()
        {
            AddressId = Id;
            SelectedIndex = AddressIndex;
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the AddressBook Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddress_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadCountryDetails();
            AssignAddressDetails();
        }

        /// <summary>
        /// To Save Address Book Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateAddressBookDetails())
                {
                    using (AddressBookSystem AddressBook = new AddressBookSystem())
                    {
                        AddressBook.AddressId = AddressId == 0 ? this.LoginUser.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : AddressId;
                        AddressBook.Name = txtName.Text.Trim();
                        AddressBook.Type = rgType.SelectedIndex == 0 ? (int)Types.Donor : rgType.SelectedIndex;
                        AddressBook.Address = MemoAddress.Text.Trim();
                        AddressBook.Place = txtPlace.Text.Trim();
                        AddressBook.State = txtState.Text.Trim();
                        AddressBook.CountryId = this.UtilityMember.NumberSet.ToInteger(glkCountry.EditValue.ToString());
                        AddressBook.Pincode = txtPinCode.Text.Trim();
                        AddressBook.Phone = txtPhone.Text.Trim();
                        AddressBook.Fax = txtFax.Text.Trim();
                        AddressBook.Email = txtEmail.Text.Trim();
                        AddressBook.IdentityKey = rgType.SelectedIndex == 0 ? (int)IdentityKey.Donor : (int)IdentityKey.Auditor;
                        AddressBook.URL = txtURL.Text.Trim();
                        resultArgs = AddressBook.SaveAddressBook();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.AddressBook.ADDRESS_SAVE_SUCCESS));
                            if (UpdateHeld != null) { UpdateHeld(this, e); }
                            ClearControls();
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
        /// To set Border Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
        }
        /// <summary>
        /// Close the Address Book form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// to Validate the Address Book Details
        /// </summary>
        /// <returns></returns>
        public bool ValidateAddressBookDetails()
        {
            bool isDonAudcommon = true;
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AddressBook.ADDRESS_NAME_EMPTY));
                    this.SetBorderColor(txtName);
                    isDonAudcommon = false;
                    txtName.Focus();
                }
                else if (string.IsNullOrEmpty(glkCountry.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.AddressBook.ADDRESS_COUNTRY_EMPTY));
                    isDonAudcommon = false;
                    glkCountry.Focus();
                }
                else if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    if (!this.IsValidEmail(txtEmail.Text))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                        isDonAudcommon = false;
                        txtEmail.Focus();
                    }
                }
                else
                {
                    MemoAddress.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return isDonAudcommon;

        }

        /// <summary>
        /// To load the Country Details 
        /// </summary>
        private void LoadCountryDetails()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindLookUpEditCombo(glkCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
                        glkCountry.EditValue = glkCountry.Properties.GetDataSourceValue(glkCountry.Properties.ValueMember, 0);
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
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
        /// To Set Title for Donor and Auditor
        /// </summary>
        private void SetTitle()
        {
            rgType.SelectedIndex = SelectedIndex;
            this.Text = AddressId == 0 ? this.GetMessage(MessageCatalog.Master.AddressBook.ADDRESS_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.AddressBook.ADDRESS_EDIT_CAPTION);
            txtName.Focus();
        }

        /// <summary>
        /// To Clear the Controls after Adding the Records
        /// </summary>
        private void ClearControls()
        {
            if (AddressId == 0)
            {
                txtName.Text = txtState.Text = MemoAddress.Text = txtPinCode.Text = txtPhone.Text = txtFax.Text = txtEmail.Text = txtURL.Text = txtPlace.Text = string.Empty;
            }
            txtName.Focus();
        }

        /// <summary>
        /// To Fill the Record to the Controls
        /// </summary>
        private void AssignAddressDetails()
        {
            try
            {
                if (AddressId != 0)
                {
                    using (AddressBookSystem addressBook = new AddressBookSystem(AddressId))
                    {
                        txtName.Text = addressBook.Name;
                        txtPlace.Text = addressBook.Place;
                        txtState.Text = addressBook.State;
                        MemoAddress.Text = addressBook.Address;
                        txtPinCode.Text = addressBook.Pincode;
                        txtPhone.Text = addressBook.Phone;
                        txtFax.Text = addressBook.Fax;
                        txtEmail.Text = addressBook.Email;
                        rgType.SelectedIndex = addressBook.IdentityKey;
                        txtURL.Text = addressBook.URL;
                        glkCountry.EditValue = addressBook.CountryId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        private void txtState_Leave(object sender, EventArgs e)
        {
            txtState.Text = this.UtilityMember.StringSet.ToSentenceCase(txtState.Text);
        }

        private void txtPlace_Leave(object sender, EventArgs e)
        {
            txtPlace.Text = this.UtilityMember.StringSet.ToSentenceCase(txtPlace.Text);
        }

        private void MemoAddress_Leave(object sender, EventArgs e)
        {
            MemoAddress.Text = this.UtilityMember.StringSet.ToSentenceCase(txtPlace.Text);
        }
    }
}