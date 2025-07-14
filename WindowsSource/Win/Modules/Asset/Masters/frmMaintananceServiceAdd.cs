using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.ASSET;
using Bosco.Model.Asset;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmMaintenanceOrService : frmBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        private int ServiceId { get; set; }
        #endregion

        #region Variable Declearation
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public frmMaintenanceOrService()
        {
            InitializeComponent();
        }

        public frmMaintenanceOrService(int ServiceId)
            : this()
        {
            this.ServiceId = ServiceId;
            AssignMaintanaceService();
        }
        #endregion

        #region Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateMaintanceService())
            {
                using (AssetServiceSystem serviceSystem = new AssetServiceSystem())
                {
                    serviceSystem.ServiceId = this.ServiceId;
                    serviceSystem.ServiceName = txtName.Text;
                    serviceSystem.Description = txtDescription.Text;
                    resultArgs = serviceSystem.SaveMaintanceService();
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        ClearControls();
                        if (UpdateHeld!=null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                    else
                    {
                        txtName.Focus();
                    }
                }
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
        }

        private void frmMaintananceOrService_Load(object sender, EventArgs e)
        {
            SetTitle();
        }
        #endregion

        #region Methods

        public bool ValidateMaintanceService()
        {
            bool isServiceTrue = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Service.SERVICE_NAME_EMPTY));
                this.SetBorderColor(txtName);
                isServiceTrue = false;
                this.txtName.Focus();
            }
            return isServiceTrue;
        }

        public void ClearControls()
        {
            if (this.ServiceId == 0)
            {
                txtName.Text = txtDescription.Text = string.Empty;
            }
            else
            {
                this.Close();
            }
        }

        public void SetTitle()
        {
            this.Text = this.ServiceId == 0 ? this.GetMessage(MessageCatalog.Asset.Service.SERVICE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Service.SERVICE_EDIT_CAPTION);
        }

        public void AssignMaintanaceService()
        {
            if (this.ServiceId > 0)
            {
                using (AssetServiceSystem serviceSystem = new AssetServiceSystem(this.ServiceId))
                {
                    txtName.Text = serviceSystem.ServiceName;
                    txtDescription.Text = serviceSystem.Description;
                }
            }
        }

        #endregion
    }
}
