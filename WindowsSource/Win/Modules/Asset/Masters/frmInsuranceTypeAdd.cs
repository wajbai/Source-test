using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using System.Text.RegularExpressions;
using Bosco.Model.ASSET;
using Bosco.Model.Asset;

namespace ACPP.Modules.Asset.Masters
{
    public partial class frmInsuranceTypeAdd : frmBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelaration
        int InsuranceId = 0;
        private ResultArgs resultArgs = null;
        #endregion

        #region Constructors
        public frmInsuranceTypeAdd()
        {
            InitializeComponent();
        }

        public frmInsuranceTypeAdd(int Insurance_Id)
            :this()
         {
            InsuranceId = Insurance_Id;
            AssignInsuranceDetails();
        }
        #endregion    

        #region Events
        /// <summary>
        /// Insurance Load
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void frmInsuranceType_Load(object sender, EventArgs e)
        {
            SetTitle();
        }
        /// <summary>
        /// Fire the Name is empty and sets border color for controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
        }
        /// <summary>
        /// Save the Insurance details
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInsuranceType())
                {
                    ResultArgs resultArgs = null;

                    using (AssetInsuranceTypeSystem InsuranceSystme = new AssetInsuranceTypeSystem())
                    {
                        InsuranceSystme.InsuranceId = InsuranceId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : InsuranceId;
                        InsuranceSystme.Name = txtName.Text.Trim();
                        InsuranceSystme.Description = meDescription.Text.Trim();
                        resultArgs = InsuranceSystme.SaveInsuranceDetials();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            claerControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                            else
                            {
                                txtName.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        #endregion

        #region Method
        /// <summary>
        /// Purpose:To Assgin insurance Details.
        /// </summary>
        /// <returns></returns>
        private void AssignInsuranceDetails()
        {
            try
            {
                if (InsuranceId != 0)
                {
                    using (AssetInsuranceTypeSystem InsuranceSystem = new AssetInsuranceTypeSystem(InsuranceId))
                    {
                        txtName.Text = InsuranceSystem.Name;
                        meDescription.Text = InsuranceSystem.Description;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        /// <summary>
        /// Purpose:To Claer text fields.
        /// </summary>
        /// <returns></returns>
        private void claerControls()
        {
            if (InsuranceId == 0)
            {
                txtName.Text = meDescription.Text = null;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Purpose:To validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInsuranceType()
        {
            bool isInsuranceTrue = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_NAME_EMPTY));
                this.SetBorderColor(txtName);
                isInsuranceTrue = false;
                this.txtName.Focus();
            }
            return isInsuranceTrue;

        }

        /// <summary>
        /// Purpose:To Set the title.
        /// </summary>
        /// <returns></returns>
        public void SetTitle()
        {
           this.Text = InsuranceId == 0 ? this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_EDIT_CAPTION);
        }
        #endregion
    }
}
