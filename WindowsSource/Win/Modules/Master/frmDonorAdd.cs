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
using ACPP.Modules.ProspectsDonor;
using Bosco.Model.Donor;
using Bosco.Model;
using Bosco.Utility.ConfigSetting;

namespace ACPP.Modules.Master
{
    public partial class frmDonorAdd : frmFinanceBaseAdd
    {
        #region Event Handler
        public event EventHandler UpdateHeld;
        #endregion

        #region Declaration
        private int donAudId = 0;
        private ViewDetails viewform;
        ResultArgs resultArgs = null;
        StateSystem state = new StateSystem();
        DialogResult mappingDialogResult = DialogResult.Cancel;
        #endregion

        #region Properties

        public Form frmParent;

        private int donorId = 0;
        private int DonorId
        {
            get { return donorId; }
            set { donorId = value; }
        }
        public int ProspectId { get; set; }
        #endregion

        #region Constructor
        public frmDonorAdd()
        {
            InitializeComponent();
        }

        public frmDonorAdd(ViewDetails ViewForm, int DonAudId, int ProjetId = 0)
            : this()
        {

            viewform = ViewForm;
            if (viewform.Equals(ViewDetails.Donor))
            {
                UcMappingDonor.ProjectId = ProjetId;
                UcMappingDonor.VisibleUserControl = true;
                UcMappingDonor.Id = donAudId = DonAudId;
                UcMappingDonor.FormType = MapForm.Donor;
                UcMappingDonor.SelectFixedWidth = true;
                this.Height = 667;
            }
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
            layoutControlItem31.Visibility = LayoutVisibility.Never;
            layoutControlItem29.Visibility = (SettingProperty.EnableNetworking) ? LayoutVisibility.Always : LayoutVisibility.Never;
            UcMappingDonor.sViewTypeValue = ViewDetails.Donor.ToString();
            LoadCountryDetails();
            LoadDonorDetails();
            DonorTitleDetails();
            AssignPropectToDonorDetails();
            LoadDefaults();
            SetTitle();
            AssignDonAudDetails();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpState.Properties.Buttons[1].Visible = true;
                glkpCountry.Properties.Buttons[1].Visible = true;
            }

            //10/07/2024, If other than india country
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                lcPANNo.Text = "UID #";
            }

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
                    // this.ShowWaitDialog();
                    using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem())
                    {
                        donaudSystem.DonAudId = donAudId == 0 ? this.LoginUser.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : donAudId;
                        donaudSystem.Name = txtName.Text.Trim();
                        donaudSystem.RegNo = textEdit4.Text.Trim(); //Reg No
                        donaudSystem.LastName = txtLastName.Text.Trim();
                        if (viewform == ViewDetails.Donor)
                        {
                            donaudSystem.Type = rgType.SelectedIndex == 0 ? (int)DonorType.Institutional : (int)DonorType.Individual;
                            //   donaudSystem.FcDonor = chkForeignAc.Checked == true ? (int)YesNo.Yes : (int)YesNo.No;
                        }
                        donaudSystem.Place = txtCity.Text.Trim();
                        donaudSystem.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                        donaudSystem.Pincode = txtPinCode.Text.Trim();
                        donaudSystem.Phone = txtPhone.Text.Trim();
                        donaudSystem.Fax = txtFax.Text.Trim();
                        donaudSystem.Email = txtEmail.Text.Trim();
                        donaudSystem.URL = txtURL.Text.Trim();
                        donaudSystem.IdentityKey = viewform == ViewDetails.Donor ? (int)IdentityKey.Donor : (int)IdentityKey.Auditor;

                        //On 09/05/2017, to make state is optional
                        donaudSystem.StateId = 0;
                        if (glkpState.EditValue != null)
                        {
                            donaudSystem.StateId = this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString());
                        }

                        donaudSystem.Address = txtAddress.Text.Trim();
                        donaudSystem.Notes = txtNotes.Text.Trim();
                        donaudSystem.PAN = txtPAN.Text.Trim();

                        if (viewform.Equals(ViewDetails.Donor))
                        {
                            donaudSystem.IsDonor = true;
                            donaudSystem.dtMapDonor = UcMappingDonor.GetMappingDetails;
                            donaudSystem.DonAudId = donAudId;
                            donaudSystem.MapDonorId = DonorId > 0 ? DonorId : donAudId;
                        }
                        if (rgType.SelectedIndex == 0) //Institutional
                        {
                            donaudSystem.InstitutionalType = glkpInsType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpInsType.EditValue.ToString()) : 0;

                        }
                        else if (rgType.SelectedIndex == 1)//individual
                        {
                            // donaudSystem.Title = glkpTitle.EditValue.ToString();
                            donaudSystem.Title = glkpTitle.EditValue != null ? glkpTitle.EditValue.ToString() : string.Empty;
                            if (!string.IsNullOrEmpty(deDOB.Text))
                            {
                                donaudSystem.DOB = this.UtilityMember.DateSet.ToDate(deDOB.DateTime.ToString(), false);
                            }

                            donaudSystem.Gender = glkpGender.EditValue != null ? (glkpGender.EditValue.Equals(Gender.Female.ToString()) ? (int)Gender.Female : (int)Gender.Male) : 0;
                            donaudSystem.MaritalStatus = glkpMaritalStatus.EditValue != null ? (glkpMaritalStatus.EditValue.Equals(MaritalStatus.Single.ToString()) ? (int)MaritalStatus.Single : (int)MaritalStatus.Married) : 0;
                            if (!string.IsNullOrEmpty(deAnniverdate.Text))
                            {
                                donaudSystem.Anniversarydate = this.UtilityMember.DateSet.ToDate(deAnniverdate.DateTime.ToString(), false);
                            }
                            donaudSystem.Language = txtLanguage.Text;
                            donaudSystem.Religion = txtReligion.Text;
                            donaudSystem.Occupation = txtOccupation.Text;
                            if (!string.IsNullOrEmpty(deDOJ.Text))
                            {
                                donaudSystem.DOJ = this.UtilityMember.DateSet.ToDate(deDOJ.DateTime.ToString(), false);
                            }
                            if (!string.IsNullOrEmpty(dateEdit3.Text))
                            {
                                donaudSystem.DOE = this.UtilityMember.DateSet.ToDate(dateEdit3.DateTime.ToString(), false);
                            }

                            donaudSystem.Organizationemployed = txtOrgEmployed.Text;
                        }
                        donaudSystem.ReferedStaff = txtRefStaff.Text;
                        donaudSystem.RegType = glkpRegType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpRegType.EditValue.ToString()) : 0;
                        donaudSystem.PaymentMode = glkpPayMode.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPayMode.EditValue.ToString()) : 0;
                        donaudSystem.EcsDuration = gridLookUpEdit6.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(gridLookUpEdit6.EditValue.ToString()) : 0;
                        donaudSystem.Isactive = chkDonorActive.Checked == true ? 1 : 0;
                        donaudSystem.ReasonForInactive = !string.IsNullOrEmpty(txtReasonforInactive.Text) ? txtReasonforInactive.Text : string.Empty;
                        donaudSystem.Prospectid = ProspectId > 0 ? ProspectId : 0;
                        resultArgs = donaudSystem.SaveDonorAuditor();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                            //if (mappingDialogResult.Equals(DialogResult.Cancel))
                            //    this.DialogResult = DialogResult.OK;
                            //mappingDialogResult = DialogResult.OK;
                            if (donAudId.Equals(0) && viewform.Equals(ViewDetails.Donor))
                            {
                                UcMappingDonor.GridClear = true;
                                ClearControls();
                            }
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
                        LoadReasonForInactive();
                        //resultArgs = donaudSystem.SaveDonorAuditorDetails();
                        //if (resultArgs.Success)
                        //{
                        //    DonorId = this.UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                        //    // if (donAudId > 0 || DonorId > 0) { MapDonor(); }
                        //    this.ShowSuccessMessage(viewform == ViewDetails.Donor ? this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION) : this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        //    ClearControls();
                        //    if (UpdateHeld != null)
                        //    {
                        //        UpdateHeld(this, e);
                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
                //this.CloseWaitDialog();
            }
        }

        /// <summary>
        /// Close the currently opened form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResult;
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
            // txtName.Text = string.IsNullOrEmpty(txtName.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtName.Text);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
            //FirstLetterToUpper();
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


        #endregion

        #region Methods
        #region UserRights

        private void ApplyRights()
        {
            bool CreateState = (CommonMethod.ApplyUserRights((int)State.CreateState) != 0);
            glkpState.Properties.Buttons[1].Visible = CreateState;

            bool CreateCountry = (CommonMethod.ApplyUserRights((int)Forms.CreateCountry) != 0);
            glkpCountry.Properties.Buttons[1].Visible = CreateCountry;
        }


        #endregion

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

            }
        }

        private void LoadDefaults()
        {
            try
            {
                if (rgType.SelectedIndex == 0)
                {
                    layoutControlItem9.Visibility = LayoutVisibility.Always;
                    layoutControlGroup3.Visibility = LayoutVisibility.Never;
                    //emptySpaceItem14.Visibility = LayoutVisibility.Always;
                    layoutControlItem22.Visibility = LayoutVisibility.Never;
                    layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                    layoutControlItem9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
                }
                else if (rgType.SelectedIndex == 1)
                {
                    layoutControlItem9.Visibility = LayoutVisibility.Never;
                    layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                    //emptySpaceItem14.Visibility = LayoutVisibility.Never;
                    //layoutControlGroup3.Visibility = LayoutVisibility.Always;

                    // 30/04/2025, *Chinna
                    layoutControlGroup3.Visibility = LayoutVisibility.Never;
                    layoutControlItem32.Visibility = layoutControlItem16.Visibility = layoutControlItem22.Visibility = LayoutVisibility.Never;
                }
                if (chkDonorActive.Checked == true)
                {
                    layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
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
        /// <summary>
        /// Assign the values to the controls to edit the donor/auditor details
        /// </summary>

        private void AssignDonAudDetails()
        {
            try
            {
                if (donAudId != 0)
                {
                    DonorId = donAudId;
                    using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem(donAudId))
                    {
                        txtName.Text = donaudSystem.Name;
                        textEdit4.Text = donaudSystem.RegNo;
                        txtLastName.Text = donaudSystem.LastName;
                        rgType.SelectedIndex = donaudSystem.Type == 2 ? (int)YesNo.Yes : (int)YesNo.No;
                        gridLookUpEdit6.EditValue = donaudSystem.EcsDuration;
                        txtCity.Text = donaudSystem.Place;
                        glkpState.EditValue = donaudSystem.StateId;
                        txtAddress.Text = donaudSystem.Address;
                        txtPinCode.Text = donaudSystem.Pincode;
                        txtPhone.Text = donaudSystem.Phone;
                        txtFax.Text = donaudSystem.Fax;
                        txtEmail.Text = donaudSystem.Email;
                        txtURL.Text = donaudSystem.URL;
                        glkpCountry.EditValue = donaudSystem.CountryId;
                        // chkForeignAc.Checked = donaudSystem.FcDonor == (int)YesNo.Yes ? true : false;
                        txtNotes.Text = donaudSystem.Notes;
                        txtPAN.Text = donaudSystem.PAN;

                        glkpInsType.EditValue = donaudSystem.InstitutionalType;
                        glkpRegType.EditValue = donaudSystem.RegType;
                        glkpTitle.EditValue = donaudSystem.Title;
                        if (rgType.SelectedIndex == (int)YesNo.Yes)
                        {
                            if (donaudSystem.DOB != DateTime.MinValue)
                            {
                                deDOB.EditValue = donaudSystem.DOB;
                            }
                        }
                        glkpGender.EditValue = glkpGender.Properties.GetKeyValue(donaudSystem.Gender - 1);
                        glkpMaritalStatus.EditValue = donaudSystem.MaritalStatus != 0 ? glkpMaritalStatus.Properties.GetKeyValue(donaudSystem.MaritalStatus) : null;
                        if (glkpMaritalStatus.EditValue != MaritalStatus.Single.ToString())
                        {
                            if (donaudSystem.Anniversarydate != DateTime.MinValue)
                            {
                                deAnniverdate.EditValue = donaudSystem.Anniversarydate;
                            }
                        }
                        txtLanguage.Text = donaudSystem.Language;
                        txtReligion.Text = donaudSystem.Religion;
                        txtOccupation.Text = donaudSystem.Occupation;
                        if (donaudSystem.DOJ != DateTime.MinValue)
                        {
                            deDOJ.EditValue = donaudSystem.DOJ;
                        }
                        if (donaudSystem.DOE != DateTime.MinValue)
                        {
                            dateEdit3.EditValue = donaudSystem.DOE;
                        }
                        txtRefStaff.Text = donaudSystem.ReferedStaff;
                        txtOrgEmployed.Text = donaudSystem.Organizationemployed;
                        glkpPayMode.EditValue = donaudSystem.PaymentMode != 0 ? donaudSystem.PaymentMode.ToString() : null;
                        chkDonorActive.Checked = donaudSystem.Isactive == 0 ? false : true;
                        txtReasonforInactive.Text = chkDonorActive.Checked == false ? donaudSystem.ReasonForInactive : string.Empty;
                    }
                    FetchMappedDonor();
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

        private void AssignPropectToDonorDetails()
        {
            try
            {
                if (ProspectId > 0)
                {
                    using (ProspectManagementSystem donaudSystem = new ProspectManagementSystem(ProspectId))
                    {
                        txtName.Text = donaudSystem.Name;
                        txtLastName.Text = donaudSystem.LastName;
                        rgType.SelectedIndex = donaudSystem.Type == 2 ? (int)YesNo.Yes : (int)YesNo.No;
                        deDOB.DateTime = donaudSystem.DOB;
                        txtReligion.Text = donaudSystem.Religion;
                        txtLanguage.Text = donaudSystem.Language;
                        glkpGender.EditValue = glkpGender.Properties.GetKeyValue(donaudSystem.Gender - 1);
                        glkpMaritalStatus.EditValue = donaudSystem.MaritalStatus != 0 ? glkpMaritalStatus.Properties.GetKeyValue(donaudSystem.MaritalStatus) : null;
                        textEdit4.Text = donaudSystem.RegNo;
                        txtOccupation.Text = donaudSystem.Occupation;
                        txtRefStaff.Text = donaudSystem.ReferredStaff;
                        txtOrgEmployed.Text = donaudSystem.OrgEmployed;

                        glkpTitle.EditValue = donaudSystem.Title;
                        deAnniverdate.DateTime = donaudSystem.AnniversaryDate;
                        glkpInsType.EditValue = donaudSystem.InstitutionalTypeId;
                        glkpRegType.EditValue = donaudSystem.RegistrationTypeId;
                        txtCity.Text = donaudSystem.Place;
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
                        FetchMappedDonor();
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
            int Type = rgType.SelectedIndex == 0 ? (int)DonorType.Institutional : (int)DonorType.Individual;
            try
            {
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    this.ShowMessageBox(viewform == ViewDetails.Donor ? this.GetMessage(MessageCatalog.Master.Donor.DONOR_NAME_EMPTY) : this.GetMessage(MessageCatalog.Master.Auditor.AUDITOR_NAME_EMPTY));
                    this.SetBorderColor(txtName);
                    isDonorAuditor = false;
                    txtName.Focus();
                }
                //else if (Type == 2 && deDOJ.Text.Trim() == string.Empty)
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Donor.DONOR_DOJ_EMPTY));
                //    deDOJ.Focus();
                //    isDonorAuditor = false;
                //}
                else if (dateEdit3.Text.Trim() != string.Empty)
                {
                    if (!this.UtilityMember.DateSet.ValidateDate(deDOJ.DateTime, dateEdit3.DateTime))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Donor.DONOR_DATE_OF_VALIDATION));
                        dateEdit3.Focus();
                        isDonorAuditor = false;
                    }
                }
                else if (string.IsNullOrEmpty(glkpCountry.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Donor.DONOR_COUNTRY_EMPTY));
                    isDonorAuditor = false;
                    glkpCountry.Focus();
                }
                //else if (string.IsNullOrEmpty(glkpState.Text.Trim())) //On 09/05/2017, to make state is optional
                //{
                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Donor.DONOR_STATE_EMPTY));
                //    isDonorAuditor = false;
                //    glkpState.Focus();
                //}
                else if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Prospects.PROSPECT_ADDRESS_EMPTY));
                    isDonorAuditor = false;
                    txtAddress.Focus();
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
            if (donAudId == 0)
            {
                txtName.Text = txtCity.Text = txtAddress.Text = txtPinCode.Text = txtPhone.Text = txtFax.Text = txtEmail.Text = txtURL.Text = txtNotes.Text = txtPAN.Text = string.Empty;
                glkpCountry.EditValue = null;
                glkpTitle.EditValue = glkpRegType.EditValue = gridLookUpEdit6.EditValue = glkpInsType.EditValue = glkpGender.EditValue =
                 glkpMaritalStatus.EditValue == null;
                deDOB.Text = deAnniverdate.Text = deDOJ.Text = dateEdit3.Text = txtReasonforInactive.Text = txtLastName.Text = txtReligion.Text = txtLanguage.Text = txtOrgEmployed.Text = txtOccupation.Text = txtRefStaff.Text = string.Empty;
                glkpState.EditValue = null;
                glkpPayMode.EditValue = null;
                layoutControlItem31.Visibility = LayoutVisibility.Never;
            }
            txtName.Focus();
        }

        //private void MapDonor()
        //{
        //    DataTable Mapping = UcMappingDonor.GetMappingDetails;
        //    var MappedProject = (from ledger in Mapping.AsEnumerable()
        //                         where (ledger.Field<Int64?>("SELECT") == 1)
        //                         select ledger);
        //    if (MappedProject.Count() > 0)
        //        Mapping = MappedProject.CopyToDataTable();
        //    else
        //        Mapping = Mapping.Clone();

        //    using (MappingSystem mappingSystem = new MappingSystem())
        //    {
        //        mappingSystem.DonorId = DonorId > 0 ? DonorId : donAudId;
        //        resultArgs = mappingSystem.AccountMappingDonorByDonorId(Mapping);
        //        if (resultArgs.Success)
        //        {
        //            if (mappingDialogResult.Equals(DialogResult.Cancel))
        //                mappingDialogResult = DialogResult.OK;
        //            if (donAudId.Equals(0))
        //            {
        //                UcMappingDonor.GetMappingDetails.Select().ToList<DataRow>().ForEach(r => r["SELECT"] = 0);
        //                UcMappingDonor.GetMappingDetails = UcMappingDonor.GetMappingDetails;
        //                UcMappingDonor.CheckAllVisible = false;
        //            }
        //        }
        //    }
        //}
        private DataTable FetchMappedDonor()
        {
            using (MappingSystem mapSystem = new MappingSystem())
            {
                mapSystem.DonorId = DonorId;
                resultArgs = mapSystem.FetchMappedDonorById();
                return resultArgs.DataSource.Table;
            }
        }
        private void LoadGender()
        {
            try
            {
                Gender gender = new Gender();
                DataView dvGendger = this.UtilityMember.EnumSet.GetEnumDataSource(gender, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGender, dvGendger.ToTable(), "Name", "Name");
                //glkpGender.EditValue = glkpGender.Properties.GetKeyValue(0);
                //glkpGender.EditValue = Gender;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadECSDuration()
        {
            try
            {
                ECSDuration ecsDuration = new ECSDuration();
                DataView dvEcsDuration = this.UtilityMember.EnumSet.GetEnumDataSource(ecsDuration, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(gridLookUpEdit6, dvEcsDuration.ToTable(), "Name", "Id");
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
        }

        private void LoadMaritalStatus()
        {
            try
            {
                MaritalStatus maritalStatus = new MaritalStatus();
                DataView dvMaritalStatus = this.UtilityMember.EnumSet.GetEnumDataSource(maritalStatus, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpMaritalStatus, dvMaritalStatus.ToTable(), "Name", "Name");
                //glkpMaritalStatus.EditValue = glkpMaritalStatus.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadNameTitles()
        {
            try
            {
                NameTitle NameTitlelist = new NameTitle();
                DataView dvNameTitle = this.UtilityMember.EnumSet.GetEnumDataSource(NameTitlelist, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpTitle, dvNameTitle.ToTable(), "Name", "Name");
                glkpTitle.EditValue = glkpTitle.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadInstitutionType()
        {
            try
            {
                using (ProspectManagementSystem ProspectSystem = new ProspectManagementSystem())
                {
                    resultArgs = ProspectSystem.FetchDonorInstitutionalType();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpInsType, resultArgs.DataSource.Table, ProspectSystem.AppSchema.DonorInstitutionalType.INSTITUTIONAL_TYPEColumn.ColumnName, ProspectSystem.AppSchema.DonorInstitutionalType.INSTITUTIONAL_TYPE_IDColumn.ColumnName);
                        //glkpInsType.EditValue = glkpInsType.Properties.GetKeyValue(0);
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

        private void LoadRegistrationType()
        {
            try
            {
                using (ProspectManagementSystem ProspectSystem = new ProspectManagementSystem())
                {
                    resultArgs = ProspectSystem.FetchDonorRegistrationType();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpRegType, resultArgs.DataSource.Table, ProspectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPEColumn.ColumnName, ProspectSystem.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn.ColumnName);
                        //   glkpRegType.EditValue = glkpRegType.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadPaymentMode()
        {
            try
            {
                using (ProspectManagementSystem ProspectSystem = new ProspectManagementSystem())
                {
                    resultArgs = ProspectSystem.FetchDonorPaymentMode();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPayMode, resultArgs.DataSource.Table, ProspectSystem.AppSchema.DonorPaymentMode.PAYMENT_MODEColumn.ColumnName, ProspectSystem.AppSchema.DonorPaymentMode.PAYMENT_MODE_IDColumn.ColumnName);
                        //  glkpPayMode.EditValue = glkpPayMode.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadDonorDetails()
        {
            try
            {
                LoadReasonForInactive();
                //LoadDefaults();
                LoadInstitutionType();
                LoadRegistrationType();
                DonorTitleDetails();
                LoadGender();
                LoadNameTitles();
                LoadMaritalStatus();
                LoadPaymentMode();
                LoadECSDuration();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadReasonForInactive()
        {
            try
            {
                using (DonorAuditorSystem DonorAudSys = new DonorAuditorSystem())
                {
                    resultArgs = DonorAudSys.AutoFetchReasonForActive();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvReasons = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[DonorAudSys.AppSchema.DonorAuditor.REASON_FOR_ACTIVEColumn.ColumnName].ToString());
                        }
                        txtReasonforInactive.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtReasonforInactive.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtReasonforInactive.MaskBox.AutoCompleteCustomSource = collection;


                        //dvNarration.RowFilter = ("VOUCHER_ID=" + dvNarration.ToTable().Compute("max(voucher_id)", string.Empty)).ToString();
                        //txtNarration.Text = dvNarration.ToTable().Rows[0]["NARRATION"].ToString();
                        //dvNarration.RowFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void CloseFormInMDI(string formName)
        {
            bool hasForm = false;
            foreach (Form frmActive in frmParent.MdiChildren)
            {
                hasForm = (frmActive.Name.ToLower() == formName.ToLower());

                if (hasForm)
                {
                    frmActive.Close();
                    break;
                }
                else
                    frmActive.Select();
            }

            //return hasForm;
        }
        #endregion

        private void glkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpCountry.EditValue != null)
            {
                state.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                resultArgs = state.FetchStateListDetails();
                if (resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, resultArgs.DataSource.Table, state.AppSchema.State.STATE_NAMEColumn.ColumnName, state.AppSchema.State.STATE_IDColumn.ColumnName);
                    //  glkpState.EditValue = glkpState.Properties.GetKeyValue(0);
                }
            }
        }

        private void btnDonorMapProject_Click(object sender, EventArgs e)
        {
            frmMapProjectLedger ProjectLedger = new frmMapProjectLedger(MapForm.Donor, 0);
            ProjectLedger.ShowDialog();
        }

        //private void FirstLetterToUpper()
        //{
        //    string Name = txtName.Text;
        //    Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Name);
        //    txtName.Text = Name;
        //}

        private void txtCity_Leave(object sender, EventArgs e)
        {
            txtCity.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCity.Text);
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.Text = this.UtilityMember.StringSet.ToSentenceCase(txtAddress.Text);
        }

        private void txtNotes_Leave(object sender, EventArgs e)
        {
            if (ViewDetails.Auditor == viewform)
            {
                btnSave.Focus();
            }
        }
        private void UcMappingDonor_PreviewKeyDown(object sender, EventArgs e)
        {
            if (UcMappingDonor.ucGridControl.IsLastRow)
            {
                btnSave.Focus();
            }
        }

        private void glkpCountry_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCountry);
        }

        private void glkpState_Leave(object sender, EventArgs e)
        {
            //On 18/05/2017, to make state is optional
            //this.SetBorderColorForGridLookUpEdit(glkpState);
        }

        private void glkpCountry_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadCountry();
            }
        }

        private void LoadCountry()
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

        private void glkpState_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadState();
            }
        }

        public void LoadState()
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

        private void rgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDefaults();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void glkpMaritalStatus_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (glkpMaritalStatus.EditValue == MaritalStatus.Single.ToString())
                {
                    deAnniverdate.Enabled = false;
                    deAnniverdate.EditValue = null;
                }
                else
                {
                    deAnniverdate.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void chkDonorActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDonorActive.Checked == true)
                {
                    layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    layoutControlItem28.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void btnGetfromProspects_Click(object sender, EventArgs e)
        {
            this.Close();
            CloseFormInMDI(typeof(frmProspectsView).Name);
            frmProspectsView frmProspects = new frmProspectsView();
            frmProspects.MdiParent = frmParent;
            frmProspects.Show();
        }

        private void glkpInsType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadInsType();
            }
        }

        private void LoadInsType()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmInstitutionType frmInstitutionTypeAdd = new frmInstitutionType();
                frmInstitutionTypeAdd.ShowDialog();
                if (frmInstitutionTypeAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadInstitutionType();
                    if (frmInstitutionTypeAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmInstitutionTypeAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpInsType.EditValue = this.UtilityMember.NumberSet.ToInteger(frmInstitutionTypeAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private void glkpRegType_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadRegistration();
            }
        }

        private void LoadRegistration()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmDonorRegistrationType frmDonorRegistrationTypeAdd = new frmDonorRegistrationType();
                frmDonorRegistrationTypeAdd.ShowDialog();
                if (frmDonorRegistrationTypeAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadRegistrationType();
                    if (frmDonorRegistrationTypeAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmDonorRegistrationTypeAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpRegType.EditValue = this.UtilityMember.NumberSet.ToInteger(frmDonorRegistrationTypeAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private void glkpTitle_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                loadTitle();
            }
        }

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

        private void glkpRegType_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpRegType.EditValue != null)
            {
                if (glkpRegType.Text.ToUpper().Equals("ECS"))
                {
                    layoutControlItem31.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    gridLookUpEdit6.EditValue = null;
                    layoutControlItem31.Visibility = LayoutVisibility.Never;
                }
            }
        }

    }
}