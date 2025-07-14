/*  Class Name      :frmExecutive Member
 *  Purpose         :To Save Executive Member Details
 *  Author          : Chinna
 *  Created on      : 
 */
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

namespace ACPP.Modules.Master
{
    public partial class frmExecutiveMember : frmBaseAdd
    {
        #region EventDeclaration
        public event EventHandler UpdateHeld;
        #endregion

        #region VariableDeclaration
        private int ExecutiveId = 0;
        ResultArgs resultArgs = new ResultArgs();
        Bitmap ImagePhoto;
        Byte[] IPhoto = null;
        #endregion

        #region Constructor
        public frmExecutiveMember()
        {
            InitializeComponent();
        }

        public frmExecutiveMember(int ExecutiveId)
            : this()
        {
            this.ExecutiveId = ExecutiveId;
        }
        #endregion

        #region Events

        /// <summary>
        /// Load the details of Executive Members
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExecutiveMember_Load(object sender, EventArgs e)
        {
            SetTitle();
         //   LoadCountryDetails();
            AssignExecutiveMemberDetails();
            
        }

        /// <summary>
        /// Save the details of Executive Members
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        executiveMemberSystem.FatherName = txtFather.Text.Trim();
                        //executiveMemberSystem.DateOfBirth = dtDateOfBirth.DateTime;
                        executiveMemberSystem.Religion = txtReligion.Text.Trim();
                        executiveMemberSystem.Role = txtRole.Text.Trim();
                        executiveMemberSystem.Nationality = txtNationality.Text.Trim();
                        executiveMemberSystem.Occupation = txtOccupation.Text.Trim();
                        executiveMemberSystem.Association = txtAssociation.Text.Trim();
                        executiveMemberSystem.OfficeBearer = txtOfficeBearer.Text.Trim();
                        executiveMemberSystem.Place = txtPlace.Text.Trim();
                        executiveMemberSystem.State = txtState.Text.Trim();
                        executiveMemberSystem.CountryId = this.UtilityMember.NumberSet.ToInteger(lkpCountry.EditValue.ToString());
                        executiveMemberSystem.PinCode = txtPinCode.Text.Trim();
                        executiveMemberSystem.Pan_SSN = txtPan.Text.Trim();
                        executiveMemberSystem.Phone = txtPhone.Text.Trim();
                        executiveMemberSystem.Fax = txtFax.Text.Trim();
                        executiveMemberSystem.URL = txtUrl.Text.Trim();
                        //executiveMemberSystem.DateOfAppointment = dtDateOfAppointment.DateTime;
                        //executiveMemberSystem.DateOfExit = dtDateOfExit.DateTime;
                        byte[] bmpB = imageToByteArray(ImagePhoto);
                        executiveMemberSystem.ImageData = bmpB;
                        executiveMemberSystem.Notes = txtMemoNotes.Text;
                        resultArgs = executiveMemberSystem.SaveExecutiveMemberDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControl();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }

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
        /// Set border color for Nationality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtNationality_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtNationality);
        }

        /// <summary>
        /// Set border color for txtName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtName);
        }

        /// <summary>
        /// Set border color for lkpCountry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lkpCountry_Leave(object sender, EventArgs e)
        {
            SetBorderColorForLookUpEdit(lkpCountry);
        }

        /// <summary>
        /// Close the form.
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
        /// Validate the mandatory fields for Executive Members.
        /// </summary>
        /// <returns></returns>
        private bool ValidateExecuteMemberDetails()
        {
            bool isExecutiveMember = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_NAME_EMPTY));
                SetBorderColor(txtName);
                isExecutiveMember = false;
                txtName.Focus();
            }
            else if (string.IsNullOrEmpty(txtNationality.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_NAIONALITY_EMPTY));
                SetBorderColor(txtNationality);
                isExecutiveMember = false;
                txtNationality.Focus();
            }
            else if (string.IsNullOrEmpty(lkpCountry.Text))
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_COUNTRY_EMPTY));
                SetBorderColor(lkpCountry);
                isExecutiveMember = false;
                lkpCountry.Focus();
            }
            else if (!string.IsNullOrEmpty(txtEMail.Text))
            {
                if (!this.IsValidEmail(txtEMail.Text))
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                    txtEMail.Focus();
                    isExecutiveMember = false;
                }
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
                txtName.Text = txtNationality.Text = txtFather.Text = txtOccupation.Text = txtAssociation.Text = txtOfficeBearer.Text = txtPlace.Text = txtState.Text = txtPinCode.Text = txtPhone.Text = txtFax.Text = txtEMail.Text = txtUrl.Text = string.Empty;
            }
            txtName.Focus();
        }

        /// <summary>
        /// Set title for forms based on Executive Id.
        /// </summary>
        private void SetTitle()
        {
            this.Text = ExecutiveId == 0 ? this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.ExecutiveMembers.EXECUTIVE_EDIT_CAPTION);
        }

        /// <summary>
        /// Assign the values to the control in edit mode.
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
                        txtFather.Text = executiveMemberSystem.FatherName;
                        txtReligion.Text = executiveMemberSystem.Religion;
                        txtRole.Text = executiveMemberSystem.Role;
                        txtNationality.Text = executiveMemberSystem.Nationality;
                        txtOccupation.Text = executiveMemberSystem.Occupation;
                        txtAssociation.Text = executiveMemberSystem.Association;
                        txtOfficeBearer.Text = executiveMemberSystem.OfficeBearer;
                        txtPlace.Text = executiveMemberSystem.Place;
                        txtState.Text = executiveMemberSystem.State;
                        lkpCountry.EditValue = executiveMemberSystem.CountryId;
                        txtPinCode.Text = executiveMemberSystem.PinCode;
                        txtPan.Text = executiveMemberSystem.Pan_SSN;
                        txtEMail.Text = executiveMemberSystem.EMail;
                        txtPhone.Text = executiveMemberSystem.Phone;
                        txtFax.Text = executiveMemberSystem.Fax;
                        txtUrl.Text = executiveMemberSystem.URL;
                        Bitmap BImage = ByteArrayToImage(IPhoto);
                        peExecutive.Image = BImage;
                        txtMemoNotes.Text = executiveMemberSystem.Notes;
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
        ///To select the Path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void peExecutive_Click(object sender, EventArgs e)
        {
            if (ofdExecutiveMember.ShowDialog() == DialogResult.OK)
            {
                string sPath = ofdExecutiveMember.FileName;
                Bitmap eMembers = new Bitmap(sPath);
                peExecutive.Image = eMembers;
                ImagePhoto = eMembers;
            }
        }
        /// <summary>
        /// Converting Image to Byte[]
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public Bitmap ByteArrayToImage(byte[] byteArrayIn)
        {
            Image ExecutiveImage = null;
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                ExecutiveImage = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return (Bitmap)ExecutiveImage;
        }
        public byte[] imageToByteArray(Bitmap imageIn)
        {
            byte[] bmpBytes = null;
            try
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, ImageFormat.Jpeg);
                bmpBytes = ms.GetBuffer();
                imageIn.Dispose();
                ms.Close();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return bmpBytes;
        }
        #endregion
    }
}