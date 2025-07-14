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
    public partial class frmDepreciationMethodsAdd : frmBaseAdd
    {    
        #region Variable Decelartion
        int DepreciationId = 0;
        private ResultArgs resultArgs = null;
        #endregion

        #region Constructors
        public frmDepreciationMethodsAdd()
        {
            InitializeComponent();
        }


        public frmDepreciationMethodsAdd(int Depreciation_Id)
            :this()
         {
            DepreciationId = Depreciation_Id;
            AssginDepreciationDetails();
        }
        #endregion

        # region Properties

        #endregion 

        # region  Events
        public event EventHandler UpdateHeld;
        /// <summary>
        /// Close the Depreciation form
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Save Depreciation details
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click_1(object sender, EventArgs e)
         {
            try
            {
                if (ValidateDepreciationDetials())
                {
                    ResultArgs resultArgs = null;
                    using (AssetDepreciationSystem DepreciationSystem = new AssetDepreciationSystem())
                    {
                        DepreciationSystem.DepreciationId = DepreciationId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : DepreciationId;
                       
                        DepreciationSystem.Name = txtName.Text.Trim();
                        DepreciationSystem.Description = meDescription.Text.Trim();
                        resultArgs = DepreciationSystem.SaveDepreciationDetials();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            clearControls();
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
            {
            }


        }
        #endregion

        #region Methods

        /// <summary>
        /// Purpose:To validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateDepreciationDetials()
        {
            bool isDepreciationTrue = true;
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_NAME_EMPTY));
                this.SetBorderColor(txtName);
                isDepreciationTrue = false;
                this.txtName.Focus();
            }
            
          return isDepreciationTrue;
        }
        /// <summary>
        /// Purpose:To Clear the text fields.
        /// </summary>
        /// <returns></returns>
        private void clearControls()
        {
            if (DepreciationId == 0)
            {
                txtName.Text = meDescription.Text = null;
            }
            else 
            {
                this.Close();
            }
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
        /// Set the title.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetTitle()
        {
            this.Text = DepreciationId == 0 ? this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Depreciation.DEPRECIATION_EDIT_CAPTION);
 
        }
        /// <summary>
        /// Load the values to the controls while editing.
        /// </summary>
        /// <param name="sender"></param> Assgin Depreciation values to form.
        /// <param name="e"></param>
        public void AssginDepreciationDetails()
        {
            try
            {
                if (DepreciationId != 0)
                {
                    using (AssetDepreciationSystem depreciationsystem = new AssetDepreciationSystem(DepreciationId))
                    {
                        txtName.Text = depreciationsystem.Name;
                        meDescription.Text = depreciationsystem.Description;
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
        private void frmDepreciationMethods_Load(object sender, EventArgs e)
        {
            SetTitle();
        }
        #endregion
    }

   
}
