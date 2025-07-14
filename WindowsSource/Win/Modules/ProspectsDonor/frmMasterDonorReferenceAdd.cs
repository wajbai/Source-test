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
    public partial class frmMasterDonorReferenceAdd : frmFinanceBaseAdd
    {
        #region Constructor

        public frmMasterDonorReferenceAdd()
        {
            InitializeComponent();
        }

        public frmMasterDonorReferenceAdd(int referredStaffId)
            : this()
        {
            ReferredStaffId = referredStaffId;
        }

        #endregion

        #region Properties

        int ReferredStaffId = 0;
        public event EventHandler UpdateHeld;

        #endregion

        #region Methods

        public bool ValidateStaffDetails()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtStaffName.Text.Trim()))
            {
                //this.ShowMessageBox("Referred Staff name is Empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.NetworkingMasterDonorReference.NETWORKING_MASTER_DONOR_REF_STAFFNAME_EMTPY));
                this.SetBorderColor(txtStaffName);
                isValue = false;
                txtStaffName.Focus();
            }
            return isValue;
        }

        private void SetTitle()
        {
            this.Text = ReferredStaffId == 0 ? this.GetMessage(MessageCatalog.Master.Donor.REFERRED_STAFF_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Donor.REFERRED_STAFF_EDIT_CAPTION);

        }

        public void AssignReferredStaffDetails()
        {
            try
            {
                if (ReferredStaffId != 0)
                {
                    using (MasterDonorReferenceSystem donorReferenceSystem = new MasterDonorReferenceSystem(ReferredStaffId))
                    {
                        txtStaffName.Text = donorReferenceSystem.RefferedStaffName;
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

        #region Events

        private void frmMasterDonorReferenceAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignReferredStaffDetails();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateStaffDetails())
                {
                    ResultArgs resultArgs = null;
                    using (MasterDonorReferenceSystem donorReferenceSystem = new MasterDonorReferenceSystem())
                    {
                        donorReferenceSystem.RefferedStaffId = ReferredStaffId == 0 ? (int)AddNewRow.NewRow : ReferredStaffId;
                        donorReferenceSystem.RefferedStaffName = txtStaffName.Text.Trim();
                        resultArgs = donorReferenceSystem.SaveReferedStaffDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            if (ReferredStaffId > 0)
                            {
                                this.Close();
                            }
                            else
                            {
                                txtStaffName.Text = string.Empty;
                                txtStaffName.Focus();
                            }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtStaffName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtStaffName);
        }

        #endregion
    }
}