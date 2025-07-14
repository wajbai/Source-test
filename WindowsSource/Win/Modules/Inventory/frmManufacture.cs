using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using ACPP.Modules;
using ACPP.Modules.Inventory;
using Bosco.Utility;
using System.Text.RegularExpressions;
using Bosco.Model;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Inventory;

namespace ACPP.Modules.Inventory
{
    public partial class frmManufacture : frmBaseAdd
    {
        #region Constructor
        public frmManufacture()
        {
            InitializeComponent();
        }
        public frmManufacture(int Id)
              :this()
        {
             ManufactureId = Id;
        }
        #endregion

        #region VariableDeclaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        int ManufactureId = 0;
        #endregion

        #region Properties

        #endregion

        #region Events

        /// <summary>
        /// Load the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManufacture_Load_1(object sender, EventArgs e)
        {
            SetTittle();
            LoadCountryDetails();
            AssignValuesToControls();
        }

        /// <summary>
        /// Save the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateVendorDetails())
                {
                    ResultArgs resultArgs = null;
                    using (ManufactureSystem manufactureSystem = new ManufactureSystem())
                    {
                        manufactureSystem.ManufactureId = ManufactureId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : ManufactureId;
                        manufactureSystem.Name = txtName.Text.Trim();
                        manufactureSystem.Address = meAddress.Text.Trim();
                        manufactureSystem.City = txtCity.Text.Trim();
                        manufactureSystem.Country = glkpCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString()) : 0;
                        manufactureSystem.State = glkpState.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString()) : 0;
                        manufactureSystem.PostalCode = txtPostalCode.Text.Trim();
                        manufactureSystem.PanNo = txtPanNo.Text.Trim();
                        manufactureSystem.TelephoneNo = txtTelephoneNo.Text.Trim();
                        manufactureSystem.Email = txtEmail.Text.Trim();
                        resultArgs = manufactureSystem.SaveManufactureDetails();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearControls();
                            if (UpdateHeld != null)
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
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            { }
        }

        /// <summary>
        /// Set the Color 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
        }
        /// <summary>
        /// close the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        /// <summary>
        /// Load state based on the country selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpCountry_EditValueChanged_1(object sender, EventArgs e)
        {
            using (StateSystem state = new StateSystem())
            {
                if (glkpCountry.EditValue != null && this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString()) != 0)
                {
                    state.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                    resultArgs = state.FetchStateListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, resultArgs.DataSource.Table, state.AppSchema.State.STATE_NAMEColumn.ColumnName, state.AppSchema.State.STATE_IDColumn.ColumnName);
                        //         glkpState.EditValue = glkpState.Properties.GetKeyValue(0);
                    }
                }
                else
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, null, state.AppSchema.State.STATE_NAMEColumn.ColumnName, state.AppSchema.State.STATE_IDColumn.ColumnName);
                }
            }
        }

        private void glkpCountry_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmCountry objCountry = new frmCountry(0);
                objCountry.ShowDialog();
                LoadCountryDetails();
            }
        }

        private void glkpState_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                frmStateAdd objState = new frmStateAdd(0);
                objState.ShowDialog();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate the Details
        /// </summary>
        /// <returns></returns>
        private bool ValidateVendorDetails()
        {
            bool isItemTrue = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.VendorInfo.VENDOR_NAME_EMPTY));
                this.SetBorderColor(txtName);
                isItemTrue = false;
                this.txtName.Focus();
            }
            else
            {
                txtName.Focus();
            }
            return isItemTrue;
        }

        /// <summary>
        /// Edit the Vendor Details
        /// </summary>
        private void ClearControls()
        {
            if (ManufactureId == 0)
            {
                txtName.Text = meAddress.Text = txtCity.Text = txtPostalCode.Text = txtPanNo.Text = txtTelephoneNo.Text = txtEmail.Text = string.Empty;
                glkpCountry.EditValue = glkpState.EditValue = 0;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Set the Title
        /// </summary>
        private void SetTittle()
        {
            this.Text = ManufactureId == 0 ? this.GetMessage(MessageCatalog.Asset.Manufacture.MANUFACTURE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Manufacture.MANUFACTURE_EDIT_CAPTION);
            txtName.Focus();
        }

        /// <summary>
        /// Assign the Values to controls
        /// </summary>
        public void AssignValuesToControls()
        {
            if (ManufactureId > 0)
            {
                using (ManufactureSystem manufactureSystem = new ManufactureSystem(ManufactureId))
                {
                    txtName.Text = manufactureSystem.Name;
                    meAddress.Text = manufactureSystem.Address;
                    txtCity.Text = manufactureSystem.City;
                    glkpCountry.EditValue = manufactureSystem.Country;
                    glkpState.EditValue = manufactureSystem.State;
                    txtPostalCode.Text = manufactureSystem.PostalCode;
                    txtPanNo.Text = manufactureSystem.PanNo;
                    txtTelephoneNo.Text = manufactureSystem.TelephoneNo;
                    txtEmail.Text = manufactureSystem.Email;
                }
            }
        }

        private void LoadCountryDetails()
        {
            try
            {
                using (CountrySystem ObjCountrySystem = new CountrySystem())
                {
                    resultArgs = ObjCountrySystem.FetchCountryDetails();
                    if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCountry, resultArgs.DataSource.Table, ObjCountrySystem.AppSchema.Country.COUNTRYColumn.ColumnName, ObjCountrySystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally
            { }
        }

        #endregion
    }
}