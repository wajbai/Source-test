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
using ACPP.Modules.Master;
using Bosco.Model;
using Bosco.Model.Donor;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmProspectAdd : frmFinanceBaseAdd
    {

        #region Event Handler
        public event EventHandler UpdateHeld;
        #endregion

        #region Declaration
        private int donAudId = 0;
        private ViewDetails viewform;
        ResultArgs resultArgs = null;
        StateSystem state = new StateSystem();
        #endregion

        #region Properties

        private int prospectId = 0;
        private int ProspectId
        {
            get { return prospectId; }
            set { prospectId = value; }
        }

        #endregion

        #region Constructor
        public frmProspectAdd()
        {
            InitializeComponent();
        }
        public frmProspectAdd(int prospectId)
            : this()
        {

            ProspectId = prospectId;
        }
        #endregion

        #region Events

        private void frmProspectAdd_Load(object sender, EventArgs e)
        {
            LoadProspectDefaults();
            AssignProspectDetails();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateProspectDetails())
                {
                    using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                    {
                        prospectSystem.ProspectId = ProspectId == 0 ? this.LoginUser.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : ProspectId;
                        prospectSystem.Name = txtName.Text.Trim();
                        prospectSystem.RegNo = txtRegNo.Text.Trim();
                        prospectSystem.LastName = txtLastName.Text.Trim();
                        prospectSystem.Type = rgType.SelectedIndex == 0 ? (int)DonorType.Institutional : (int)DonorType.Individual;
                        prospectSystem.RegistrationTypeId = glkpRegType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpRegType.EditValue.ToString()) : 0;
                        if (prospectSystem.Type == 2)
                        {
                            prospectSystem.Title = glkpTitle.EditValue != null ? glkpTitle.EditValue.ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(dtDOB.Text))
                                prospectSystem.DOB = this.UtilityMember.DateSet.ToDate(dtDOB.DateTime.ToString(), false);
                            prospectSystem.Gender = glkpGender.EditValue != null ? (glkpGender.EditValue.Equals("2") ? (int)Gender.Female : (int)Gender.Male) : 0;
                            prospectSystem.MaritalStatus = glkpMaritalStatus.EditValue != null ? (glkpMaritalStatus.EditValue.Equals(MaritalStatus.Single.ToString()) ? (int)MaritalStatus.Single : (int)MaritalStatus.Married) : 0;
                            if (!string.IsNullOrEmpty(dtAnniversaries.Text))
                                prospectSystem.AnniversaryDate = this.UtilityMember.DateSet.ToDate(dtAnniversaries.EditValue.ToString(), false);

                            prospectSystem.Language = txtLanguage.Text.Trim();
                            prospectSystem.Religion = txtReligion.Text.Trim();
                            prospectSystem.OrgEmployed = txtOrgEmplayed.Text.Trim();
                            prospectSystem.Occupation = txtOccupation.Text.Trim();

                        }
                        else
                        {
                            prospectSystem.InstitutionalTypeId = glkpInstitutionType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpInstitutionType.EditValue.ToString()) : 0;
                        }
                        prospectSystem.Place = txtPlace.Text.Trim();
                        prospectSystem.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                        prospectSystem.Pincode = txtPinCode.Text.Trim();
                        prospectSystem.Phone = txtPhone.Text.Trim();
                        prospectSystem.Fax = txtFax.Text.Trim();
                        prospectSystem.Email = txtEmail.Text.Trim();
                        prospectSystem.URL = txtURL.Text.Trim();
                        prospectSystem.StateId = this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString());
                        prospectSystem.Address = txtAddress.Text.Trim();
                        prospectSystem.ReferredStaff = txtReferredStaff.Text.Trim();
                        prospectSystem.Notes = txtNotes.Text.Trim();
                        prospectSystem.PAN = txtPAN.Text.Trim();
                        prospectSystem.SourceInformation = txtSourceInfo.Text.Trim();
                        prospectSystem.ReferenceNumber = txtReferenceNumber.Text.Trim();
                        //  prospectSystem.PaymentModeId = this.UtilityMember.NumberSet.ToInteger(glkpPaymode.EditValue.ToString());
                        resultArgs = prospectSystem.SaveDonorProspect();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(viewform == ViewDetails.Donor ? this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION) : this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                                UpdateHeld(this, e);
                        }
                        else if (!string.IsNullOrEmpty(resultArgs.Message))
                        {
                            XtraMessageBox.Show(resultArgs.Message);
                            txtName.Focus();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
        }

        private void txtPlace_Leave(object sender, EventArgs e)
        {
            //this.SetBorderColor(txtPlace);
            txtPlace.Text = this.UtilityMember.StringSet.ToSentenceCase(txtPlace.Text);
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtAddress);
            txtAddress.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAddress.Text);
        }

        private void txtNotes_Leave(object sender, EventArgs e)
        {
            btnSave.Focus();
        }

        private void glkpState_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpState);
        }

        private void glkpCountry_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCountry);
        }

        private void glkpCountry_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmCountry frmCountryAdd = new frmCountry();
                    frmCountryAdd.ShowDialog();
                    if (frmCountryAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadCountryDetails();
                        if (frmCountryAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmCountryAdd.ReturnValue.ToString()) > 0)
                        {
                            glkpCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(frmCountryAdd.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }

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

        private void glkpRegType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmDonorRegistrationType frmDonorRegistrationType = new frmDonorRegistrationType();
                    frmDonorRegistrationType.ShowDialog();
                    if (frmDonorRegistrationType.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadRegistrationTypeDetails();
                        if (frmDonorRegistrationType.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmDonorRegistrationType.ReturnValue.ToString()) > 0)
                        {
                            glkpRegType.EditValue = this.UtilityMember.NumberSet.ToInteger(frmDonorRegistrationType.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }

        private void glkpInstitutionType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmInstitutionType frmInstitutionType = new frmInstitutionType();
                    frmInstitutionType.ShowDialog();
                    if (frmInstitutionType.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadInstitutionalTypeDetails();
                        if (frmInstitutionType.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmInstitutionType.ReturnValue.ToString()) > 0)
                        {
                            glkpInstitutionType.EditValue = this.UtilityMember.NumberSet.ToInteger(frmInstitutionType.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }

        private void glkpState_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmStateAdd frmState = new frmStateAdd();
                    frmState.ShowDialog();
                    if (frmState.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadStateDetails();
                        if (frmState.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmState.ReturnValue.ToString()) > 0)
                        {
                            glkpState.EditValue = this.UtilityMember.NumberSet.ToInteger(frmState.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }

        private void rgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrHidePersonalInfo();
        }

        private void glkpMaritalStatus_EditValueChanged(object sender, EventArgs e)
        {
            //dteAnniveryDate.Enabled = glkpMaritalStatus.EditValue.Equals(MaritalStatus.Married.ToString()) ? true : false;
            //if (!dteAnniveryDate.Enabled)
            //{
            //    dteAnniveryDate.EditValue = null;
            //}
        }



        #endregion

        #region Methods

        private void SetTitle()
        {
            this.Text = ProspectId == 0 ? this.GetMessage(MessageCatalog.Master.Prospects.PROSPECT_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Prospects.PROSPECT_EDIT_CAPTION);

        }

        private void ShowOrHidePersonalInfo()
        {
            if (rgType.EditValue.Equals((int)DonorType.Institutional))
            {
                lciInstitutionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcPersonalInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.Height = this.Height - 45;
            }
            else if (rgType.EditValue.Equals((int)DonorType.Individual))
            {
                LoadPersonalInfoDefaults();
                lciInstitutionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcPersonalInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.Height = 520;
            }
        }


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


        private void DonorTitleDetails()
        {
            try
            {
                using (DonorTitleSystem donorSystem = new DonorTitleSystem())
                {
                    resultArgs = donorSystem.FetchDonorTitleDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpTitle, resultArgs.DataSource.Table, donorSystem.AppSchema.DonorTitle.TITLEColumn.ColumnName, donorSystem.AppSchema.DonorTitle.TITLE_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }


        private void LoadPaymentModeDetails()
        {
            try
            {
                using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                {
                    resultArgs = prospectSystem.FetchDonorPaymentMode();
                    if (resultArgs.Success)
                    {
                        //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPaymode, resultArgs.DataSource.Table, prospectSystem.AppSchema.DonorPaymentMode.PAYMENT_MODEColumn.ColumnName, prospectSystem.AppSchema.DonorPaymentMode.PAYMENT_MODE_IDColumn.ColumnName);
                        //glkpPaymode.EditValue = glkpPaymode.Properties.GetKeyValue(0);
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

        private void LoadInstitutionalTypeDetails()
        {
            try
            {
                using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                {
                    resultArgs = prospectSystem.FetchDonorInstitutionalType();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpInstitutionType, resultArgs.DataSource.Table, prospectSystem.AppSchema.DonorInstitutionalType.INSTITUTIONAL_TYPEColumn.ColumnName, prospectSystem.AppSchema.DonorInstitutionalType.INSTITUTIONAL_TYPE_IDColumn.ColumnName);
                        //glkpInstitutionType.EditValue = glkpInstitutionType.Properties.GetKeyValue(0);
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

        private void LoadRegistrationTypeDetails()
        {
            try
            {
                using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                {
                    resultArgs = prospectSystem.FetchDonorRegistrationType();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpRegType, resultArgs.DataSource.Table, prospectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPEColumn.ColumnName, prospectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn.ColumnName);
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



        private void LoadGender()
        {
            try
            {
                Gender gender = new Gender();
                DataView dvGendger = this.UtilityMember.EnumSet.GetEnumDataSource(gender, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGender, dvGendger.ToTable(), "Name", "Id");
                glkpGender.EditValue = glkpGender.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadMaritalStatus()
        {
            try
            {
                MaritalStatus maritalStatus = new MaritalStatus();
                DataView dvMaritalStatus = this.UtilityMember.EnumSet.GetEnumDataSource(maritalStatus, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpMaritalStatus, dvMaritalStatus.ToTable(), "Name", "Name");
                glkpMaritalStatus.EditValue = glkpMaritalStatus.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ClearControls()
        {
            if (ProspectId == 0)
            {
                txtName.Text = txtRegNo.Text = txtPlace.Text = txtAddress.Text = txtReferredStaff.Text = txtPinCode.Text = txtPhone.Text = txtFax.Text = txtEmail.Text = txtURL.Text = txtNotes.Text = txtPAN.Text = txtSourceInfo.Text = txtReferenceNumber.Text = string.Empty;
                glkpCountry.EditValue = null;
                glkpState.EditValue = null;
                rgType.SelectedIndex = 0;
                glkpInstitutionType.EditValue = null;
                glkpRegType.EditValue = null;
                //glkpPaymode.EditValue = null;

                ////Personal Info Clear
                //glkpTitle.EditValue = null;
                dtDOB.EditValue = null;
                //glkpGender.EditValue = null;
                //glkpMaritalStatus.EditValue = null;
                dtAnniversaries.EditValue = null;
                //txtLanguage.Text = txtReligion.Text = txtOccupation.Text = txtRefStaff.Text = txtorgEmployed.Text = string.Empty;
            }
            txtName.Focus();
        }

        private void LoadProspectDefaults()
        {
            SetTitle();
            if (ProspectId == 0)
            {
                rgType.SelectedIndex = 0;
            }
            //dtAnniversaries.Properties.MaxValue = dtDOB.Properties.MaxValue = DateTime.Now;
            LoadCountryDetails();
            LoadStateDetails();
            LoadInstitutionalTypeDetails();
            LoadRegistrationTypeDetails();
            LoadPaymentModeDetails();
        }

        private void LoadPersonalInfoDefaults()
        {
            // LoadTitle();
            DonorTitleDetails();
            LoadGender();
            LoadMaritalStatus();
        }


        private void AssignProspectDetails()
        {
            try
            {
                if (ProspectId > 0)
                {
                    ProspectId = ProspectId;
                    using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem(ProspectId))
                    {
                        txtName.Text = prospectSystem.Name;
                        txtRegNo.Text = prospectSystem.RegNo;
                        txtLastName.Text = prospectSystem.LastName;
                        rgType.SelectedIndex = prospectSystem.Type == 2 ? (int)YesNo.Yes : (int)YesNo.No;
                        if (prospectSystem.Type == 2)//Individual
                        {
                            //lciInstitutionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            //lcPersonalInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            LoadPersonalInfoDefaults();
                            glkpTitle.EditValue = prospectSystem.Title;
                            glkpGender.EditValue = glkpGender.Properties.GetKeyValue(prospectSystem.Gender - 1);
                            glkpMaritalStatus.EditValue = prospectSystem.MaritalStatus;

                            // dtDOB.EditValue = prospectSystem.DOB;

                            if (prospectSystem.DOB != DateTime.MinValue)
                            {
                                dtDOB.EditValue = prospectSystem.DOB;
                            }
                            if (glkpMaritalStatus.EditValue != MaritalStatus.Single.ToString())
                            {
                                if (prospectSystem.AnniversaryDate != DateTime.MinValue)
                                {
                                    dtAnniversaries.EditValue = prospectSystem.AnniversaryDate;
                                }
                            }

                            txtLanguage.Text = prospectSystem.Language;
                            txtReligion.Text = prospectSystem.Religion;
                            txtReferredStaff.Text = prospectSystem.ReferredStaff;
                            txtOccupation.Text = prospectSystem.Occupation;
                            txtOrgEmplayed.Text = prospectSystem.OrgEmployed;
                        }
                        else //Institutional Type
                        {
                            lciInstitutionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            LoadInstitutionalTypeDetails();
                            glkpInstitutionType.EditValue = prospectSystem.InstitutionalTypeId;
                        }

                        glkpMaritalStatus.EditValue = prospectSystem.MaritalStatus == 0 ? MaritalStatus.Single : MaritalStatus.Married;

                        txtPlace.Text = prospectSystem.Place;
                        glkpCountry.EditValue = prospectSystem.CountryId;
                        glkpState.EditValue = prospectSystem.StateId;
                        txtAddress.Text = prospectSystem.Address;
                        txtPinCode.Text = prospectSystem.Pincode;
                        txtPhone.Text = prospectSystem.Phone;
                        txtFax.Text = prospectSystem.Fax;
                        txtEmail.Text = prospectSystem.Email;
                        txtURL.Text = prospectSystem.URL;
                        glkpRegType.EditValue = prospectSystem.RegistrationTypeId;
                        txtNotes.Text = prospectSystem.Notes;
                        txtPAN.Text = prospectSystem.PAN;
                        //  glkpPaymode.EditValue = prospectSystem.PaymentModeId;
                        txtSourceInfo.Text = prospectSystem.SourceInformation;
                        txtReferenceNumber.Text = prospectSystem.ReferenceNumber;
                        txtReferredStaff.Text = prospectSystem.ReferredStaff;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private bool ValidateProspectDetails()
        {
            bool isProspect = true;
            try
            {
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_NAME_EMPTY));
                    this.SetBorderColor(txtName);
                    isProspect = false;
                    txtName.Focus();
                }
                else if (string.IsNullOrEmpty(glkpCountry.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Donor.DONOR_COUNTRY_EMPTY));
                    isProspect = false;
                    glkpCountry.Focus();
                }
                else if (string.IsNullOrEmpty(glkpState.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Donor.DONOR_STATE_EMPTY));
                    isProspect = false;
                    glkpState.Focus();
                }
                //else if (string.IsNullOrEmpty(txtPlace.Text.Trim()))
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Prospects.PROSPECT_CITY_EMPTY));
                //    isProspect = false;
                //    txtPlace.Focus();
                //}
                else if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Prospects.PROSPECT_ADDRESS_EMPTY));
                    isProspect = false;
                    txtAddress.Focus();

                }
                else if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    if (!this.IsValidEmail(txtEmail.Text.Trim()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                        isProspect = false;
                        txtEmail.Focus();
                    }
                }

                else if (!string.IsNullOrEmpty(txtURL.Text.Trim()))
                {
                    if (!this.IsValidURL(txtURL.Text.Trim()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_URL_INVALID));
                        isProspect = false;
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
            return isProspect;
        }


        private void glkpTitle_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                loadTitle();
            }
        }

        //private void LoadTitle()
        //{
        //    try
        //    {
        //        NameTitle nameTitle = new NameTitle();
        //        DataView dvNameTitle = this.UtilityMember.EnumSet.GetEnumDataSource(nameTitle, Sorting.None);
        //        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpTitle, dvNameTitle.ToTable(), "Name", "Name");
        //        glkpTitle.EditValue = glkpTitle.Properties.GetKeyValue(0);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}

        private void loadTitle()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmDonorTitleAdd frmDonorTitleAdd = new frmDonorTitleAdd();
                frmDonorTitleAdd.ShowDialog();
                if (frmDonorTitleAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    DonorTitleDetails();
                    Refresh();
                    if (frmDonorTitleAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmDonorTitleAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpTitle.EditValue = this.UtilityMember.NumberSet.ToInteger(frmDonorTitleAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        #endregion
    }
}