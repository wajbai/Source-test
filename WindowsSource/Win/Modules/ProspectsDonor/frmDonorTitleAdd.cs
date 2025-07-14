using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ACPP;
using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.CommonMemberSet;
using Bosco.Model.Donor;



namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorTitleAdd :frmFinanceBaseAdd
    {
        #region Variable Declaration
        private int titleId = 0;
        private ResultArgs resultArgs = null;
        #endregion

        #region Event Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors

        public frmDonorTitleAdd()
        {
            InitializeComponent();
        }

        public frmDonorTitleAdd(int TitleId)
            :this()
        {
            this.titleId = TitleId;
            AssignDonorTitleDetails();
        }

        #endregion

        #region Methods

        public bool ValidateDonorTitleDetails()
        {
            bool isTitleTrue= true;

            if (string.IsNullOrEmpty(txtDonorTitle.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.DonorTitle.DONOR_TITLENAME_EMPTY));
                this.SetBorderColor(txtDonorTitle);
                isTitleTrue = false;
                txtDonorTitle.Focus();
            }
            return isTitleTrue;

        }

        /// <summary>
        /// To Load the values to control
        /// </summary>

        public void AssignDonorTitleDetails()
        {
            try
            {
                if (titleId != 0)
                {
                    using (DonorTitleSystem DonorTitleSystem = new DonorTitleSystem(titleId))
                    {
                        txtDonorTitle.Text = DonorTitleSystem.DonorTitle;
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
        /// To Clear the Control
        /// </summary>
        private void ClearControls()
        {
            if (titleId == 0)
            {
                txtDonorTitle.Text = string.Empty;
            }
            txtDonorTitle.Focus();
        }
        /// <summary>
        /// To set the Title to Donor Title
        /// </summary>
        private void SetTitle()
        {
            this.Text = titleId == 0 ? this.GetMessage(MessageCatalog.Master.DonorTitle.DONORTITLE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.DonorTitle.DONORTITLE_EDIT_CAPTION);
            txtDonorTitle.Focus();
        }

        #endregion

        #region Events
        /// <summary>
        /// To Save the record
        /// </summary>
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDonorTitleDetails())
                {
                    ResultArgs resultArgs = null;
                    using (DonorTitleSystem donorTitleSystem = new DonorTitleSystem())
                    {
                        donorTitleSystem.TitleId = titleId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : titleId;
                        donorTitleSystem.DonorTitle = txtDonorTitle.Text.Trim();
                        resultArgs = donorTitleSystem.SaveDonorTitleDetails();

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
                        else
                        {
                            txtDonorTitle.Focus();
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
        /// To Close the Dornor Title
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To Load the Values to Control
        /// </summary>
        private void frmDonorTitleAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            AssignDonorTitleDetails();
        }

        private void txtDonorTitle_Leave(object sender, EventArgs e)
        {
            SetBorderColor(txtDonorTitle);
            txtDonorTitle.Text = this.UtilityMember.StringSet.ToSentenceCase(txtDonorTitle.Text);
        }
    }
}
#endregion
