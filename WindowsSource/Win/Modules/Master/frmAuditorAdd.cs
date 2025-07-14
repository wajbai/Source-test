using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Controls;
using Bosco.Model.UIModel;
using Bosco.DAO.Schema;
using System.Net.Mail;

namespace ACPP.Modules.Master
{
    public partial class frmAuditorAdd : frmBaseAdd
    {
        #region Event Handler
        public event EventHandler UpdateHeld;
        #endregion

        #region Declaration
        private int donAudId = 0;
        private ViewDetails viewform;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmAuditorAdd()
        {
            InitializeComponent();
        }

        public frmAuditorAdd(ViewDetails ViewForm, int DonAudId)
            : this()
        {
            viewform = ViewForm;
            donAudId = DonAudId;
        }
        #endregion

        #region Events
        /// <summary>
        /// Invoke Loadcountrydetails to bind the country and invoke assign to assign the values to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmDonorAuditorAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadCountryDetails();
            AssignDonAudDetails();
        }

        /// <summary>
        /// Validate Donor or Auditor information and Save the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDonorAuditorDetails())
                {
                    //this.ShowWaitDialog();
                    using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem())
                    {
                        donaudSystem.DonAudId = donAudId == 0 ? this.LoginUser.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : donAudId;
                        donaudSystem.Name = txtName.Text.Trim();
                        if (viewform == ViewDetails.Donor)
                        {
                            donaudSystem.Type = rgType.SelectedIndex == 0 ? (int)DonorType.Institutional : (int)DonorType.Individual;
                        }
                        donaudSystem.Place = txtCity.Text.Trim();
                        donaudSystem.CompanyName = txtCompanyName.Text.Trim();
                        donaudSystem.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                        donaudSystem.Pincode = txtPinCode.Text.Trim();
                        donaudSystem.Phone = txtPhone.Text.Trim();
                        donaudSystem.Fax = txtFax.Text.Trim();
                        donaudSystem.Email = txtEmail.Text.Trim();
                        donaudSystem.URL = txtURL.Text.Trim();
                        donaudSystem.IdentityKey = viewform == ViewDetails.Donor ?(int)IdentityKey.Donor: (int)IdentityKey.Auditor;
                        donaudSystem.State = txtState.Text.Trim();
                        donaudSystem.Address = txtAddress.Text.Trim();
                        donaudSystem.Notes = txtNotes.Text.Trim();
                        donaudSystem.PAN = txtPAN.Text.Trim();

                        resultArgs = donaudSystem.SaveDonorAuditorDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(viewform == ViewDetails.Donor ? this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION) : this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
               // this.CloseWaitDialog();
            }
        }

        /// <summary>
        /// Close the currently opened form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Remove the border color while leaving the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
        }

        /// <summary>
        /// To show the country form to add new country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lkpCountry_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == ButtonPredefines.Plus)
                {
                    frmCountry frmcountry = new frmCountry();
                    frmcountry.ShowDialog();
                    glkpCountry.Text = frmcountry.DonorAduitorCountryName;
                    LoadCountryDetails();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// To enable foregn account if the country is India
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lkpCountry_EditValueChanged(object sender, EventArgs e)
        {
           // chkForeignAc.Enabled = glkpCountry.Text == "India" ? true : false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To set the title based on the value clicked in the browse screen
        /// </summary>

        private void SetTitle()
        {
            switch (viewform)
            {
                case ViewDetails.Donor:
                    {
                        this.Text = donAudId == 0 ? this.GetMessage(MessageCatalog.Master.Donor.DONOR_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Donor.DONOR_EDIT_CAPTION);
                        break;
                    }
                case ViewDetails.Auditor:
                    {
                        this.Text = donAudId == 0 ? this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_EDIT_CAPTION);
                        lblType.Visibility = LayoutVisibility.Never;
                        break;
                    }
            }
        }

        /// <summary>
        /// Bind the country details to the lookup control
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
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ColumnName, countrySystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName);
                        glkpCountry.EditValue = glkpCountry.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message, true);
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
        /// Assign the values to the controls to edit the donor/auditor details
        /// </summary>

        private void AssignDonAudDetails()
        {
            try
            {
                if (donAudId != 0)
                {
                    using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem(donAudId))
                    {
                        txtName.Text = donaudSystem.Name;
                        rgType.SelectedIndex = donaudSystem.Type ==this.UtilityMember.NumberSet.ToInteger( DonorType.Institutional.ToString()) ? (int)YesNo.Yes : (int)YesNo.No;
                        txtCity.Text = donaudSystem.Place;
                        txtCompanyName.Text = donaudSystem.CompanyName;
                        txtState.Text = donaudSystem.State;
                        txtAddress.Text = donaudSystem.Address;
                        txtPinCode.Text = donaudSystem.Pincode;
                        txtPhone.Text = donaudSystem.Phone;
                        txtFax.Text = donaudSystem.Fax;
                        txtEmail.Text = donaudSystem.Email;
                        txtURL.Text = donaudSystem.URL;
                        glkpCountry.Text = donaudSystem.Country;
                        txtNotes.Text = donaudSystem.Notes;
                        txtPAN.Text = donaudSystem.PAN;
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
        /// Validate user input for the mandatory fields
        /// </summary>
        /// <returns></returns>

        private bool ValidateDonorAuditorDetails()
        {
            bool isDonorAuditor = true;
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    this.ShowMessageBox(viewform == ViewDetails.Donor ? this.GetMessage(MessageCatalog.Master.Donor.DONOR_NAME_EMPTY) : this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_NAME_EMPTY));
                    this.SetBorderColor(txtName);
                    isDonorAuditor = false;
                    txtName.Focus();
                }
                //else if (string.IsNullOrEmpty(glkpCountry.Text))
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Donor.DONOR_COUNTRY_EMPTY));
                //    isDonorAuditor = false;
                //    glkpCountry.Focus();
                //}
                //else if(!string.IsNullOrEmpty(txtEmail.Text))
                //{
                //    if (!this.IsValidEmail(txtEmail.Text))
                //    {
                //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                //        isDonorAuditor = false;
                //        txtEmail.Focus();
                //    }
                //}
                else
                {
                    txtAddress.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return isDonorAuditor;
        }


        /// <summary>
        /// To clear all the controls and focus to the first control
        /// </summary>

        private void ClearControls()
        {
            if (donAudId == 0)
            {
                txtName.Text = txtCity.Text = txtState.Text = txtAddress.Text = txtCompanyName.Text= txtPinCode.Text = txtPhone.Text = txtFax.Text = txtEmail.Text = txtURL.Text=txtNotes.Text=txtPAN.Text = string.Empty;
            }
            txtName.Focus();
        }
        #endregion
    }
}