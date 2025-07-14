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
using Bosco.Model.Donor;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorRegistrationType :frmFinanceBaseAdd
    {
        #region Events Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region VariableDeclaration
        private int RegTypeID = 0;
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmDonorRegistrationType()
        {
            InitializeComponent();
        }
        public frmDonorRegistrationType(int RegId)
            : this()
        {
            RegTypeID = RegId;
        }
        #endregion

        #region Events
        /// <summary>
        /// Load Donor Registration Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDonorRegistrationType_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignRegistrationDetails();
        }
        /// <summary>
        /// Save Donor Registration Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidationRegistrationType())
                {
                    ResultArgs resultArgs = null;
                    using (DonorRegistrationTypeSystem donorRegistrationType = new DonorRegistrationTypeSystem())
                    {
                        donorRegistrationType.RegTypeID = RegTypeID == 0 ? (int)AddNewRow.NewRow : RegTypeID;
                        donorRegistrationType.RegistrationTypeName = txtRegistrationType.Text.Trim();
                        resultArgs = donorRegistrationType.SaveRegistrationType();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        txtRegistrationType.Focus();
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Apply the Boder color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRegistrationType_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtRegistrationType);
            txtRegistrationType.Text = this.UtilityMember.StringSet.ToSentenceCase(txtRegistrationType.Text);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validation Registration Type Details
        /// </summary>
        /// <returns></returns>
        private bool IsValidationRegistrationType()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtRegistrationType.Text.Trim()))
            {
                this.ShowMessageBox(GetMessage(MessageCatalog.Master.Donor.DONOR_REGISTRATION_TYPE_EMPTY));
                this.SetBorderColor(txtRegistrationType);
                isValue = false;
                txtRegistrationType.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// Set Title the form
        /// </summary>
        private void SetTitle()
        {
            this.Text = RegTypeID == 0 ? this.GetMessage(MessageCatalog.Master.Donor.DONOR_REGISTRATION_TYPE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Donor.DONOR_REGISTRATION_TYPE_EDIT_CAPTION);
            txtRegistrationType.Focus();
        }

        /// <summary>
        /// Clear controls form
        /// </summary>
        private void ClearControls()
        {
            if (RegTypeID == 0)
            {
                txtRegistrationType.Text = string.Empty;
            }
        }

        /// <summary>
        /// Assign Registration Details
        /// </summary>
        private void AssignRegistrationDetails()
        {
            try
            {
                if (RegTypeID > 0)
                {
                    using (DonorRegistrationTypeSystem donorRegistrationType = new DonorRegistrationTypeSystem(RegTypeID))
                    {
                        txtRegistrationType.Text = donorRegistrationType.RegistrationTypeName;
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}