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

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmMaintananceOrService : frmBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Declearation
        ResultArgs resultArgs = null;
        int serviceid = 0;
        #endregion

        public frmMaintananceOrService()
        {
            InitializeComponent();
        }

        public frmMaintananceOrService(int service_id)
        {
            serviceid = service_id;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                using (ServiceSystem serviceSystem = new ServiceSystem())
                {
                    serviceSystem.Servicecode = txtCode.Text.Trim();
                    serviceSystem.Name = txtName.Text.Trim();
                    serviceSystem.description = meDescription.Text.Trim();
                    //resultArgs = serviceSystem.Save();
                    if (resultArgs.Success)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        clearControls();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                    else
                    {
                        txtCode.Focus();
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

        #region Methods
        public bool Validate()
        {
            bool isServiceTrue=true;
            if(string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Service.SERVICE_NAME_EMPTY));
                this.SetBorderColor(txtName);
                isServiceTrue = false;
                this.txtName.Focus();
            }
            return isServiceTrue;
        }

        public void clearControls()
        {
            if (serviceid == 0)
            {
                txtCode.Text = txtName.Text = meDescription.Text = null;
            }
        }

        public void SetTitle()
        {
            this.Text = serviceid == 0 ? this.GetMessage(MessageCatalog.Asset.Service.SERVICE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Service.SERVICE_EDIT_CAPTION);
        }
        #endregion

        
    }
}
