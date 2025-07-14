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
using Bosco.DAO.Schema;
using DevExpress.XtraEditors.Controls;

namespace ACPP.Modules.Inventory
{
    public partial class frmVendorInfoAdd : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        int Id = 0;
        AppSchemaSet appSchema = new AppSchemaSet();
        private object mode = null;
        private VendorManufacture vendorManufacture { get; set; }
        AssetStockProduct.IVendorManufacture IVendor = null;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public frmVendorInfoAdd(int vendorId, VendorManufacture module)
            : this()
        {
            this.Id = vendorId;
            this.vendorManufacture = module;
            IVendor = AssetStockFactory.GetUnitVendorInstance(module);
        }

        public frmVendorInfoAdd()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        /// <summary>
        /// Load the Vendor Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVendorInfoAdd_Load(object sender, EventArgs e)
        {
            SetTittle();
            LoadCountryDetails();
            AssignValuesToControls();


            //10/07/2024, If other than india country
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                lcGSTNo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblPan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
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
                    IVendor.Id = Id == 0 ? 0 : Id;
                    IVendor.Name = txtName.Text.Trim();
                    IVendor.Address = meAddress.Text.Trim();
                    IVendor.StateId = glkpState.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString());
                    IVendor.CountryId = glkpCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                    IVendor.PanNo = txtPanNo.Text.Trim();
                    IVendor.GSTNo = txtGSTNo.Text.Trim();
                    IVendor.TelephoneNo = txtTelephoneNo.Text.Trim();
                    IVendor.Email = txtEmail.Text.Trim();
                    resultArgs = IVendor.SaveDetails();
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
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text.Trim());
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
            else if (!string.IsNullOrEmpty(txtPanNo.Text) || !string.IsNullOrEmpty(txtGSTNo.Text) || !string.IsNullOrEmpty(txtEmail.Text) )
            {
                if (!string.IsNullOrEmpty(txtPanNo.Text)  && IsPANNoExists())
                {
                    this.ShowMessageBox("'" + txtPanNo.Text + "' PAN No is already exists in Vendors list");
                    this.SetBorderColor(txtPanNo);
                    isItemTrue = false;
                    this.txtPanNo.Focus();
                }
                else if (!string.IsNullOrEmpty(txtGSTNo.Text) && IsGSTNoExists())
                {
                    this.ShowMessageBox("'" + txtGSTNo.Text + "' GST No is already exists in Vendors list");
                    this.SetBorderColor(txtGSTNo);
                    isItemTrue = false;
                    this.txtGSTNo.Focus();
                }
                else if (!string.IsNullOrEmpty(txtEmail.Text) && IsEamilExists())
                {
                    this.ShowMessageBox("'" + txtEmail.Text + "' Email is already exists in Vendors list");
                    this.SetBorderColor(txtEmail);
                    isItemTrue = false;
                    this.txtEmail.Focus();
                }
            }
            else
            {
                txtName.Focus();
            }
            return isItemTrue;
        }

        /// <summary>
        /// Check PAN No is already exits
        /// </summary>
        /// <returns></returns>
        private bool IsPANNoExists()
        {
            bool exists = true;

            try
            {
                using (VendorInfoSystem vendorsys = new VendorInfoSystem())
                {
                    vendorsys.Id = Id;
                    vendorsys.PanNo= this.txtPanNo.Text.Trim();
                    ResultArgs resultarg = vendorsys.FetchDetailsByPANNo();
                    if (resultarg.Success && resultarg.DataSource.Table != null && resultarg.DataSource.Table.Rows.Count==0)
                    {
                        exists = false;
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return exists;
        }

        /// <summary>
        /// Check GST No is already exits
        /// </summary>
        /// <returns></returns>
        private bool IsGSTNoExists()
        {
            bool exists = true;

            try
            {
                using (VendorInfoSystem vendorsys = new VendorInfoSystem())
                {
                    vendorsys.Id = Id;
                    vendorsys.GSTNo = this.txtGSTNo.Text.Trim();
                    ResultArgs resultarg = vendorsys.FetchDetailsByGSTNo();
                    if (resultarg.Success && resultarg.DataSource.Table != null && resultarg.DataSource.Table.Rows.Count == 0)
                    {
                        exists = false;
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return exists;
        }

        /// <summary>
        /// Check Eamil is already exits
        /// </summary>
        /// <returns></returns>
        private bool IsEamilExists()
        {
            bool exists = true;

            try
            {
                using (VendorInfoSystem vendorsys = new VendorInfoSystem())
                {
                    vendorsys.Id = Id;
                    vendorsys.Email = this.txtEmail.Text.Trim();
                    ResultArgs resultarg = vendorsys.FetchDetailsByEmail();
                    if (resultarg.Success && resultarg.DataSource.Table != null && resultarg.DataSource.Table.Rows.Count == 0)
                    {
                        exists = false;
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return exists;
        }


        /// <summary>
        /// Edit the Vendor Details
        /// </summary>
        private void ClearControls()
        {
            if (Id == 0)
            {
                txtName.Text = meAddress.Text = txtGSTNo.Text= txtPanNo.Text = txtTelephoneNo.Text = txtEmail.Text = string.Empty;
                txtName.Focus();
            }
            else
            {
                //this.Close();
            }
        }

        /// <summary>
        /// Set the Title and Visible pan number for vendor
        /// </summary>
        private void SetTittle()
        {
            if (vendorManufacture.Equals(VendorManufacture.Vendor))
            {
                this.Text = Id == 0 ? this.GetMessage(MessageCatalog.Asset.VendorInfo.VENDOR_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.VendorInfo.VENDOR_EDIT_CAPTION);
            }
            else
            {
                this.Text = Id == 0 ? this.GetMessage(MessageCatalog.Asset.Manufacture.MANUFACTURE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Manufacture.MANUFACTURE_EDIT_CAPTION);
            }
            if (vendorManufacture.Equals(VendorManufacture.Vendor))
            {
                lblPan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcGSTNo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                //commanded by sudhakar to enable Pan No in Manufacture
                //lblPan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lcGSTNo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// Assign the Values to controls
        /// </summary>
        public void AssignValuesToControls()
        {
            if (this.Id > 0)
            {
                IVendor.Id = this.Id;
                IVendor.FillProperties();
                txtName.Text = IVendor.Name;
                txtEmail.Text = IVendor.Email;
                txtGSTNo.Text = IVendor.GSTNo;
                txtPanNo.Text = IVendor.PanNo;
                txtTelephoneNo.Text = IVendor.TelephoneNo;
                meAddress.Text = IVendor.Address;
                glkpState.EditValue = IVendor.StateId;
                glkpCountry.EditValue = IVendor.CountryId;
            }
        }

        public void SetTitle()
        {

            this.Text = this.Id == 0 ? this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.AssetCategory.ASSETCATEGORY_EDIT_CAPTION);
        }

        /// <summary>
        /// Bind the country details to the lookup control
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
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ColumnName, countrySystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName);
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

        public void LoadState()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmStateAdd frmState = new frmStateAdd();
                frmState.ShowDialog();
                if (frmState.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadStateDetails();
                    if (frmState.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmState.ReturnValue.ToString()) > 0)
                    {
                        glkpState.EditValue = this.UtilityMember.NumberSet.ToInteger(frmState.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }
        #endregion

        private void glkpState_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void glkpCountry_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void glkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpCountry.EditValue != null)
            {
                using (StateSystem state = new StateSystem())
                {
                    state.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                    resultArgs = state.FetchStateListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, resultArgs.DataSource.Table, state.AppSchema.State.STATE_NAMEColumn.ColumnName, state.AppSchema.State.STATE_IDColumn.ColumnName);
                    }
                }
            }
        }

        private void glkpState_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                LoadState();
            }
        }

        private void glkpCountry_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                frmCountry frmcountry = new frmCountry();
                frmcountry.ShowDialog();
                glkpCountry.Text = frmcountry.DonorAduitorCountryName;
                LoadCountryDetails();
            }
        }
    }
}