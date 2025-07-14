using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Schema;
using System.Xml;
using System.Collections;
using System.Net.Mail;
using System.IO;
using System.Drawing.Imaging;
using ACPP.Modules.User_Management;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.Master
{
    public partial class frmEMembers : frmFinanceBaseAdd
    {
        #region EventsDeclartion
        public event EventHandler UpdateHeld;
        /// <summary>
        /// ///testing
        /// </summary>
        #endregion

        #region VariableDeclaration
        private int ExecutiveId = 0;
        string sPath;
        private string selectedLang = string.Empty;
        ResultArgs resultArgs = new ResultArgs();
        StateSystem state = new StateSystem();
        #endregion

        #region Constructor
        public frmEMembers()
        {
            InitializeComponent();
        }
        public frmEMembers(int ExId)
            : this()
        {
            ExecutiveId = ExId;
        }
        #endregion

        #region Events
        /// <summary>
        /// To load the Executive Member Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEMembers_Load(object sender, EventArgs e)
        {
            selectedLang = this.AppSetting.LanguageId;
            //if (selectedLang == "pt-PT")
            //{
            //    this.Width = 650;
            //}
            //else if (selectedLang == "id-ID")
            //{
            //    this.Width = 600;
            //}
            //else
            //{
            //    this.Width = 558;
            //}
            SetTitle();
            LoadSocietyNames();
            LoadCountryDetails();
            AssignExecutiveMemberDetails();
            // LoadStateDetails();
            // dtDOB.Text = string.Empty;
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpState.Properties.Buttons[1].Visible = true;
                glkpCountry.Properties.Buttons[1].Visible = true;
               // glkpSocietyname.Properties.Buttons[1].Visible = true;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateExecuteMemberDetails())
                {
                    using (ExecutiveMemberSystem executiveMemberSystem = new ExecutiveMemberSystem())
                    {
                        executiveMemberSystem.ExecutiveId = ExecutiveId == 0 ? (int)AddNewRow.NewRow : ExecutiveId;
                        executiveMemberSystem.ExecutiveName = txtName.Text.Trim();
                        executiveMemberSystem.FatherName = string.Empty;
                        executiveMemberSystem.DateOfBirth = string.Empty; //this.UtilityMember.DateSet.ToDate(dtDOB.Text);
                        // executiveMemberSystem.DateOfBirth = dtDOB.Text.Trim();
                        executiveMemberSystem.Religion = string.Empty;
                        executiveMemberSystem.Role = txtRole.Text.Trim();
                        executiveMemberSystem.Nationality = string.Empty;
                        executiveMemberSystem.Occupation = string.Empty;
                        executiveMemberSystem.Association = string.Empty;
                        executiveMemberSystem.OfficeBearer = string.Empty;
                        executiveMemberSystem.Place = txtCity.Text.Trim();
                        executiveMemberSystem.StateId = glkpState.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString()) : 0;
                        executiveMemberSystem.CountryId = glkpCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString()) : 0;
                        executiveMemberSystem.Address = txtMemoAddress.Text;
                        executiveMemberSystem.PinCode = txtPinCode.Text.Trim();
                        executiveMemberSystem.Pan_SSN = txtPan.Text.Trim();
                        executiveMemberSystem.AadharNo = txtAadhar.Text.Trim();
                        executiveMemberSystem.Phone = txtPhone.Text.Trim();
                        executiveMemberSystem.Fax = string.Empty;
                        executiveMemberSystem.EMail = txtEmail.Text.Trim();
                        executiveMemberSystem.URL = string.Empty;
                        executiveMemberSystem.DateOfAppointment = this.UtilityMember.DateSet.ToDate(dtDOJ.Text);
                        executiveMemberSystem.DateOfExit = this.UtilityMember.DateSet.ToDate(dtDateOfExit.Text);
                        //  executiveMemberSystem.ImageData = (peExImage.Image as Bitmap) == null ? ImageProcessing.ImageToByteArray(global::ACPP.Properties.Resources.Default_Photo as Bitmap) : ImageProcessing.ImageToByteArray(peExImage.Image as Bitmap);
                        executiveMemberSystem.Notes = txtMemoNote.Text;
                        executiveMemberSystem.LegalEntityId = (glkpSocietyname.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpSocietyname.EditValue.ToString()) : 0;
                        resultArgs = executiveMemberSystem.SaveExecutiveMemberDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            ClearControl();
                            txtName.Focus();
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
        /// To enable the Path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void peExImage_Click(object sender, EventArgs e)
        {
            ofdExecutive.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (ofdExecutive.ShowDialog() == DialogResult.OK)
            {
                sPath = ofdExecutive.FileName;
                Bitmap eMembers = new Bitmap(sPath);
                //frmCroppingImage.AssignImage = eMembers;
                //frmCroppingImage cropImage = new frmCroppingImage();
                //cropImage.ShowDialog();
                //if (ImageProcessing.FinalCroppedImage != null)
                //{
                //    int PicBoxWidth = peExImage.Width;
                //    int PicBoxHeight = peExImage.Height;
                //    peExImage.Image = ImageProcessing.ResizeImage(ImageProcessing.FinalCroppedImage, ref PicBoxWidth, ref PicBoxHeight);
                //    peExImage.Width = PicBoxWidth;
                //    peExImage.Height = PicBoxHeight;
                //}
                peExImage.Image = eMembers;
            }
        }

        /// <summary>
        /// Set the Border Color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
        }

        /// <summary>
        /// Set the Border Color for Nationality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void txtNationality_Leave(object sender, EventArgs e)
        //{
        //    SetBorderColor(txtNationality);
        //}

        /// <summary>
        /// To close the Add form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region User Rights
        private void ApplyRights()
        {
            bool CreateState = (CommonMethod.ApplyUserRights((int)State.CreateState) != 0);
            glkpState.Properties.Buttons[1].Visible = CreateState;

            bool CreateCountry = (CommonMethod.ApplyUserRights((int)Forms.CreateCountry) != 0);
            glkpCountry.Properties.Buttons[1].Visible = CreateCountry;

            bool createlegalEntity = (CommonMethod.ApplyUserRights((int)LegalEntity.CreateLegalEntity) != 0);
            glkpSocietyname.Properties.Buttons[1].Visible = createlegalEntity;
        }

        #endregion

        #region Methods
        /// <summary>
        /// To Validate the ExecutiveMembers
        /// </summary>
        /// <returns></returns>
        private bool ValidateExecuteMemberDetails()
        {
            bool isExecutiveMember = true;
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_NAME_EMPTY));
                this.SetBorderColor(txtName);
                txtName.Focus();
                isExecutiveMember = false;
            }
            else if (glkpSocietyname.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.SOCIETY_NAME_EMPTY));
                glkpSocietyname.Focus();
                isExecutiveMember = false;
            }
            else if (glkpCountry.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_COUNTRY_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpCountry);
                glkpCountry.Focus();
                isExecutiveMember = false;
            }
            else if (glkpState.EditValue == null)
            {
                //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_COUNTRY_EMPTY));
                this.ShowMessageBox("State is empty.");
                this.SetBorderColorForGridLookUpEdit(glkpState);
                glkpState.Focus();
                isExecutiveMember = false;
            }
            else if (string.IsNullOrEmpty(txtAadhar.Text.Trim()))
            {
                this.ShowMessageBox("Aadhar is empty");
                this.SetBorderColor(txtAadhar);
                txtAadhar.Focus();
                isExecutiveMember = false;
            }
            else if (string.IsNullOrEmpty(txtPan.Text.Trim()))
            {
                this.ShowMessageBox("Pan is empty");
                this.SetBorderColor(txtPan);
                txtPan.Focus();
                isExecutiveMember = false;
            }
            else if (string.IsNullOrEmpty(txtMemoAddress.Text.Trim()))
            {
                this.ShowMessageBox("Pan is empty");
                this.SetBorderColor(txtMemoAddress);
                txtMemoAddress.Focus();
                isExecutiveMember = false;
            }
            else if (string.IsNullOrEmpty(txtCity.Text.Trim()))
            {
                this.ShowMessageBox("Pan is empty");
                this.SetBorderColor(txtCity);
                txtCity.Focus();
                isExecutiveMember = false;
            }
            else if (string.IsNullOrEmpty(txtPinCode.Text.Trim()))
            {
                this.ShowMessageBox("Pan is empty");
                this.SetBorderColor(txtPinCode);
                txtPinCode.Focus();
                isExecutiveMember = false;
            }
            else if (string.IsNullOrEmpty(dtDOJ.Text.Trim()))
            {
                this.ShowMessageBox("DOJ is empty");
                this.SetBorderColor(dtDOJ);
                dtDOJ.Focus();
                isExecutiveMember = false;
            }
            else if (dtDOJ.DateTime != DateTime.MinValue && dtDateOfExit.DateTime != DateTime.MinValue)
            {
                if (!this.UtilityMember.DateSet.ValidateDate(dtDOJ.DateTime, dtDateOfExit.DateTime))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_EXIT_JOIN));
                    dtDateOfExit.Focus();
                    isExecutiveMember = false;
                }
            }
            else if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!this.IsValidEmail(txtEmail.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                    txtEmail.Focus();
                    isExecutiveMember = false;
                }
            }
            //else if (string.IsNullOrEmpty(txtNationality.Text.Trim()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_NAIONALITY_EMPTY));
            //    SetBorderColor(txtNationality);
            //    txtNationality.Focus();
            //    isExecutiveMember = false;
            //}
            //else if (glkpSocietyname.EditValue == null)
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.SOCIETY_NAME_EMPTY));
            //    glkpSocietyname.Focus();
            //    isExecutiveMember = false;
            //}
            //|| (!this.UtilityMember.DateSet.ValidateStirng(dtDOB.Text, dtDOJ.Text))
            // else if ((!string.IsNullOrEmpty(dtDOB.Text) && !string.IsNullOrEmpty(dtDOJ.Text)))
            //else if (dtDOB.DateTime != DateTime.MinValue && dtDOJ.DateTime != DateTime.MinValue && dtDateOfExit.DateTime != DateTime.MinValue)
            //{
            //    if (!this.UtilityMember.DateSet.ValidateDate(dtDOB.DateTime, dtDOJ.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_JOIN_DOB));
            //        isExecutiveMember = false;
            //        dtDOJ.Focus();
            //    }
            //    else if (!this.UtilityMember.DateSet.ValidateDate(dtDOB.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_DOB_EXIT));
            //        isExecutiveMember = false;
            //        dtDateOfExit.Focus();
            //    }
            //    else if (!this.UtilityMember.DateSet.ValidateDate(dtDOJ.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_EXIT_JOIN));
            //        dtDateOfExit.Focus();
            //        isExecutiveMember = false;
            //    }

           //}
            //else if (dtDOB.DateTime != DateTime.MinValue && dtDOJ.DateTime != DateTime.MinValue)
            //{
            //    if (!this.UtilityMember.DateSet.ValidateDate(dtDOB.DateTime, dtDOJ.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_JOIN_DOB));
            //        isExecutiveMember = false;
            //        dtDOJ.Focus();
            //    }
            //}
            ////((!string.IsNullOrEmpty(dtDOB.Text) && !string.IsNullOrEmpty(dtDateOfExit.Text)))
            //else if (dtDOB.DateTime != DateTime.MinValue && dtDateOfExit.DateTime != DateTime.MinValue)
            //{
            //    if (!this.UtilityMember.DateSet.ValidateDate(dtDOB.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_DOB_EXIT));
            //        isExecutiveMember = false;
            //        dtDateOfExit.Focus();
            //    }
            //}
            // else if ((string.IsNullOrEmpty(dtDOJ.Text) && !string.IsNullOrEmpty(dtDateOfExit.Text)))
            // {

           //}
            //else if ((string.IsNullOrEmpty(dtDOB.Text) && !string.IsNullOrEmpty(dtDateOfExit.Text)) || (string.IsNullOrEmpty(dtDOB.Text) || !string.IsNullOrEmpty(dtDateOfExit.Text)))
            //{
            //    if (!this.UtilityMember.DateSet.ValidateDate(dtDOB.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_DOB_EXIT));
            //        isExecutiveMember = false;
            //        dtDateOfExit.Focus();
            //    }
            //    else if (!this.UtilityMember.DateSet.ValidateDate(dtDOJ.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_EXIT_JOIN));
            //        dtDateOfExit.Focus();
            //        isExecutiveMember = false;
            //    }
            //}
            //else if ((!string.IsNullOrEmpty(dtDOJ.Text) && !string.IsNullOrEmpty(dtDateOfExit.Text)) || (!string.IsNullOrEmpty(dtDOJ.Text) && !string.IsNullOrEmpty(dtDateOfExit.Text)))
            //{
            //    if (!this.UtilityMember.DateSet.ValidateDate(dtDOJ.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_EXIT_JOIN));
            //        dtDateOfExit.Focus();
            //        isExecutiveMember = false;
            //    }
            //}
            //else if ((string.IsNullOrEmpty(dtDOB.Text.Trim()) && !string.IsNullOrEmpty(dtDOJ.Text.Trim()) && !string.IsNullOrEmpty(dtDateOfExit.Text.Trim()) || string.IsNullOrEmpty(dtDOB.Text.Trim()) && !string.IsNullOrEmpty(dtDOJ.Text.Trim()) && !string.IsNullOrEmpty(dtDateOfExit.Text.Trim())))
            //{
            //    if (!this.UtilityMember.DateSet.ValidateDate(dtDOB.DateTime, dtDOJ.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_JOIN_DOB));
            //        isExecutiveMember = false;
            //        dtDOJ.Focus();
            //    }
            //    // if(dtDateOfExit.DateTime!=DateTime.MinValue)
            //    //   {
            //    else if (!this.UtilityMember.DateSet.ValidateDate(dtDOB.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_DOB_EXIT));
            //        isExecutiveMember = false;
            //        dtDateOfExit.Focus();
            //    }
            //    // }
            //    else if (!this.UtilityMember.DateSet.ValidateDate(dtDOJ.DateTime, dtDateOfExit.DateTime))
            //    {
            //        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_EXIT_JOIN));
            //        dtDateOfExit.Focus();
            //        isExecutiveMember = false;
            //    }
            //}



            else
            {
                txtMemoNote.Focus();
            }
            return isExecutiveMember;
        }

        /// <summary>
        /// Clear the controls after adding 
        /// </summary>
        private void ClearControl()
        {
            if (ExecutiveId == 0)
            {
                txtName.Text = txtRole.Text = txtMemoAddress.Text = txtCity.Text = glkpState.Text = txtPinCode.Text = txtPan.Text = txtAadhar.Text = txtPhone.Text = txtEmail.Text = txtMemoNote.Text = string.Empty;
                // peExImage.Image = global::ACPP.Properties.Resources.Default_Photo;
                glkpCountry.EditValue = null;
                glkpSocietyname.EditValue = null;
                glkpState.EditValue = null;
            }
            txtName.Focus();
        }

        /// <summary>
        /// To Capture the Details
        /// </summary>
        private void SetTitle()
        {
            this.Text = ExecutiveId == 0 ? this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_EDIT_CAPTION);
            if (ExecutiveId == 0)
            {
                //dtDOB.Properties.MaxValue = DateTime.Today;
                // dtDOB.Text = string.Empty;
            }
        }

        /// <summary>
        /// To Assign the Details
        /// </summary>
        private void AssignExecutiveMemberDetails()
        {
            try
            {
                if (ExecutiveId != 0)
                {
                    using (ExecutiveMemberSystem executiveMemberSystem = new ExecutiveMemberSystem(ExecutiveId))
                    {

                        txtName.Text = executiveMemberSystem.ExecutiveName;
                        //dtDOB.Text = string.Empty; // this.UtilityMember.DateSet.ToDate(executiveMemberSystem.DateOfBirth);
                        //txtFather.Text = executiveMemberSystem.FatherName;
                        //txtReligion.Text = executiveMemberSystem.Religion;
                        txtRole.Text = executiveMemberSystem.Role;
                        //txtNationality.Text = string.Empty; //executiveMemberSystem.Nationality;
                        //txtOccupation.Text = string.Empty; // executiveMemberSystem.Occupation;
                        // txtAssociation.Text = executiveMemberSystem.Association;
                        //txtRelationship.Text = executiveMemberSystem.OfficeBearer;
                        txtCity.Text = executiveMemberSystem.Place;
                        glkpState.EditValue = executiveMemberSystem.StateId;
                        glkpCountry.EditValue = executiveMemberSystem.CountryId;
                        glkpSocietyname.EditValue = executiveMemberSystem.LegalEntityId;
                        txtMemoAddress.Text = executiveMemberSystem.Address;
                        txtPinCode.Text = executiveMemberSystem.PinCode;
                        txtPan.Text = executiveMemberSystem.Pan_SSN;
                        txtAadhar.Text = executiveMemberSystem.AadharNo;
                        txtEmail.Text = executiveMemberSystem.EMail;
                        txtPhone.Text = executiveMemberSystem.Phone;
                        //txtFax.Text = executiveMemberSystem.Fax;
                        dtDOJ.Text = this.UtilityMember.DateSet.ToDate(executiveMemberSystem.DateOfAppointment);
                        dtDateOfExit.Text = this.UtilityMember.DateSet.ToDate(executiveMemberSystem.DateOfExit);
                        // txtURL.Text = executiveMemberSystem.URL;
                        //  peExImage.Image = (executiveMemberSystem.ImageData) == null ? (global::ACPP.Properties.Resources.Default_Photo) : ImageProcessing.ByteArrayToImage(executiveMemberSystem.ImageData);
                        txtMemoNote.Text = executiveMemberSystem.Notes;
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
        /// Bind value to the look up control.
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
                        //this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
                        //lkpCountry.EditValue = lkpCountry.Properties.GetDataSourceValue(lkpCountry.Properties.ValueMember, 0);
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
        /// Bind value to the look up control.
        /// </summary>
        private void LoadSocietyNames()
        {
            try
            {
                BindSociety();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Bind Society Name to lookup edit contrls
        /// </summary>
        private void BindSociety()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    resultArgs = projectSystem.FetchSocietyName();
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpSocietyname, resultArgs.DataSource.Table, projectSystem.AppSchema.ExecutiveMembers.SOCIETYNAMEColumn.ColumnName, projectSystem.AppSchema.ExecutiveMembers.CUSTOMERIDColumn.ColumnName);
                    // if (projectId == 0) { glkSocietyName.EditValue = glkSocietyName.Properties.GetKeyValue(0); }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        public void LoadSociety()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmLegalEntity frmlegalentity = new frmLegalEntity();
                frmlegalentity.ShowDialog();
                if (frmlegalentity.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    BindSociety();
                    if (frmlegalentity.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmlegalentity.ReturnValue.ToString()) > 0)
                    {
                        glkpSocietyname.EditValue = this.UtilityMember.NumberSet.ToInteger(frmlegalentity.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }



        #endregion

        private void glkpCountry_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCountry);
        }

        //private void glkpScoietyname_Leave(object sender, EventArgs e)
        //{
        //    this.SetBorderColorForGridLookUpEdit(glkpSocietyname);
        //}

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

        private void glkpState_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                try
                {
                    if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                    {
                        if (this.AppSetting.LockMasters == (int)YesNo.No)
                        {
                            frmStateAdd frmstateAdd = new frmStateAdd();
                            frmstateAdd.ShowDialog();
                            if (frmstateAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                            {
                                LoadStateDetails();
                                if (frmstateAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmstateAdd.ReturnValue.ToString()) > 0)
                                {
                                    glkpState.EditValue = this.UtilityMember.NumberSet.ToInteger(frmstateAdd.ReturnValue.ToString());
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageRender.ShowMessage(ex.ToString(), true);
                }
                finally { }
            }
        }

        private void glkpCountry_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                try
                {
                    if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                    {
                        if (this.AppSetting.LockMasters == (int)YesNo.No)
                        {
                            frmCountry frmcountryAdd = new frmCountry();
                            frmcountryAdd.ShowDialog();
                            if (frmcountryAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                            {
                                LoadCountryDetails();
                                if (frmcountryAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmcountryAdd.ReturnValue.ToString()) > 0)
                                {
                                    glkpCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(frmcountryAdd.ReturnValue.ToString());
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageRender.ShowMessage(ex.ToString(), true);
                }
                finally { }
            }
        }

        private void glkpScoietyname_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadSociety();
            }
        }

        private void txtAadhar_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtAadhar);
        }

        private void txtPan_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtPan);
        }

        private void txtMemoAddress_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtMemoAddress);

        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtCity);
        }

        private void txtPinCode_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtPinCode);
        }

        private void glkpSocietyname_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpSocietyname);
        }

        private void dtDOJ_Leave(object sender, EventArgs e)
        {
            SetBorderColor(dtDOJ);
        }
    }
}