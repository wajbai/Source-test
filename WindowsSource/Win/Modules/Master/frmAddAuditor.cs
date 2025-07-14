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
using Bosco.Model.UIModel.Master;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Controls;
using Bosco.Model.UIModel;
using Bosco.DAO.Schema;
using System.Net.Mail;
using ACPP.Modules.UIControls;

namespace ACPP.Modules.Master
{
    public partial class frmAddAuditor : frmFinanceBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelaration
        private int AuditorId = 0;
        private ResultArgs resultArgs = null;
        StateSystem state = new StateSystem();
        #endregion

        #region Constructor
        public frmAddAuditor()
        {
            InitializeComponent();
        }
        public frmAddAuditor(int id)
            : this()
        {
            AuditorId = id;

        }
        #endregion

        #region Events
        private void frmAddAuditor_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadCountryDetails();
            AssignAuditorDetails();

            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpState.Properties.Buttons[1].Visible = true;
                glkpCountry.Properties.Buttons[1].Visible = true;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDonorAuditorDetails())
                {
                    using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem())
                    {
                        donaudSystem.DonAudId = AuditorId == 0 ? this.LoginUser.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : AuditorId;
                        donaudSystem.Name = txtName.Text.Trim();
                        donaudSystem.Place = txtCity.Text.Trim();
                        donaudSystem.CompanyName = txtCompanyName.Text.Trim();
                        donaudSystem.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                        donaudSystem.Pincode = txtPinCode.Text.Trim();
                        donaudSystem.Phone = txtPhone.Text.Trim();
                        donaudSystem.Fax = txtFax.Text.Trim();
                        donaudSystem.Email = txtEmail.Text.Trim();
                        donaudSystem.URL = txtURL.Text.Trim();
                        donaudSystem.IdentityKey = (int)IdentityKey.Auditor;
                        donaudSystem.StateId = this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString());
                        donaudSystem.Address = txtAddress.Text.Trim();
                        donaudSystem.Notes = txtNotes.Text.Trim();
                        donaudSystem.PAN = txtPAN.Text.Trim();
                        resultArgs = donaudSystem.SaveDonorAuditor();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
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
            }
        }

        #endregion


        #region User Rights

        private void ApplyRights()
        {
            bool CreateState = (CommonMethod.ApplyUserRights((int)State.CreateState) != 0);
            glkpState.Properties.Buttons[1].Visible = CreateState;

            bool CreateCountry = (CommonMethod.ApplyUserRights((int)Forms.CreateCountry) != 0);
            glkpCountry.Properties.Buttons[1].Visible = CreateCountry;
        }

        #endregion


        #region Methods

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
                        // glkpCountry.EditValue = glkpCountry.Properties.GetKeyValue(0);
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


        private void SetTitle()
        {
            this.Text = AuditorId == 0 ? this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_EDIT_CAPTION);
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
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_NAME_EMPTY));
                    this.SetBorderColor(txtName);
                    isDonorAuditor = false;
                    txtName.Focus();
                }
                else if (string.IsNullOrEmpty(glkpCountry.Text.Trim()))
                {
                    this.ShowMessageBox("Country is empty");
                    isDonorAuditor = false;
                    glkpCountry.Focus();
                }
                else if (string.IsNullOrEmpty(glkpState.Text.Trim()))
                {
                    this.ShowMessageBox("state is empty");
                    isDonorAuditor = false;
                    glkpState.Focus();
                }
                else if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    if (!this.IsValidEmail(txtEmail.Text.Trim()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                        isDonorAuditor = false;
                        txtEmail.Focus();
                    }
                }
                else if (!string.IsNullOrEmpty(txtURL.Text.Trim()))
                {
                    if (!this.IsValidURL(txtURL.Text.Trim()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_URL_INVALID));
                        isDonorAuditor = false;
                        txtEmail.Focus();
                    }
                }
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
            if (AuditorId == 0)
            {
                txtName.Text = txtCity.Text = txtAddress.Text = txtPinCode.Text = txtPhone.Text = txtFax.Text = txtEmail.Text = txtCompanyName.Text = txtURL.Text = txtNotes.Text = txtPAN.Text = string.Empty;
                glkpCountry.EditValue = null;
                glkpState.EditValue = null;
            }
            txtName.Focus();
        }
        #endregion

        private void txtCity_Leave(object sender, EventArgs e)
        {
            txtCity.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCity.Text);
        }

        private void txtCompanyName_Leave(object sender, EventArgs e)
        {
            txtCompanyName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCompanyName.Text);
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAddress.Text);
        }

        /// <summary>
        /// Bind the country details to the lookup control
        /// </summary>
        private void LoadStateDetails()
        {
            try
            {
                using (StateSystem stateSystem = new StateSystem())
                {
                    stateSystem.CountryId = glkpCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString()) : 0;
                    resultArgs = stateSystem.FetchStateListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, resultArgs.DataSource.Table, stateSystem.AppSchema.State.STATE_NAMEColumn.ColumnName, stateSystem.AppSchema.State.STATE_IDColumn.ColumnName);
                        glkpState.EditValue = glkpState.Properties.GetDisplayValue(0);
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

        private void AssignAuditorDetails()
        {
            try
            {
                if (AuditorId != 0)
                {
                    using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem(AuditorId))
                    {
                        txtName.Text = donaudSystem.Name;
                        txtCity.Text = donaudSystem.Place;
                        txtCompanyName.Text = donaudSystem.CompanyName;
                        glkpState.EditValue = donaudSystem.StateId;
                        txtAddress.Text = donaudSystem.Address;
                        txtPinCode.Text = donaudSystem.Pincode;
                        txtPhone.Text = donaudSystem.Phone;
                        txtFax.Text = donaudSystem.Fax;
                        txtEmail.Text = donaudSystem.Email;
                        txtURL.Text = donaudSystem.URL;
                        glkpCountry.EditValue = donaudSystem.CountryId;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkpCountry_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                {
                    if (this.AppSetting.LockMasters == (int)YesNo.No)
                    {
                        frmCountry frmCountry = new frmCountry();
                        frmCountry.ShowDialog();
                        if (frmCountry.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                        {
                            LoadCountryDetails();
                            if (frmCountry.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmCountry.ReturnValue.ToString()) > 0)
                            {
                                glkpCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(frmCountry.ReturnValue.ToString());
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                    }

                    //frmCountry frmcountry = new frmCountry();
                    //frmcountry.ShowDialog();
                    //glkpCountry.Text = frmcountry.DonorAduitorCountryName;
                    //LoadCountryDetails();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            // txtName.Text = string.IsNullOrEmpty(txtName.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtName.Text);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
            //FirstLetterToUpper();
        }

        private void txtPinCode_EditValueChanged(object sender, EventArgs e)
        {

        }

        // Load the Country into the Country Combo
        private void glkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpCountry.EditValue != null)
            {
                state.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                resultArgs = state.FetchStateListDetails();
                if (resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, resultArgs.DataSource.Table, state.AppSchema.State.STATE_NAMEColumn.ColumnName, state.AppSchema.State.STATE_IDColumn.ColumnName);
                    glkpState.EditValue = glkpState.Properties.GetKeyValue(0);
                }
            }
        }

        private void glkpState_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpState);
        }

        private void glkpCountry_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCountry);
        }
         // Load the immediate added State in to the State Combo 
        private void glkpState_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                {

                    if (this.AppSetting.LockMasters == (int)YesNo.No)
                    {
                        frmStateAdd frmStateAdd = new frmStateAdd();
                        frmStateAdd.ShowDialog();
                        if (frmStateAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                        {
                            LoadStateDetails();
                            if (frmStateAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmStateAdd.ReturnValue.ToString()) > 0)
                            {
                                glkpState.EditValue = this.UtilityMember.NumberSet.ToInteger(frmStateAdd.ReturnValue.ToString());
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally{ }
            }
        }
    }
