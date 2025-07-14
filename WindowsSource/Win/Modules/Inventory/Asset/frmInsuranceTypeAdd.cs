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
using Bosco.Model;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmInsuranceTypeAdd : frmBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelaration
        int InsuranceTypeId = 0;
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
             InsuranceTypeId = Insurance_Id;
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
            LoadDefalut();
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
                        InsuranceSystme.InsuranceTypeId = InsuranceTypeId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : InsuranceTypeId;
                        InsuranceSystme.Name = txtName.Text.Trim();
                        InsuranceSystme.Company = txtCompany.Text.Trim();
                        InsuranceSystme.Product = txtProduct.Text.Trim();
                        resultArgs = InsuranceSystme.SaveInsuranceDetials();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            claerControls();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
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
                if (InsuranceTypeId>0)
                {
                    using (AssetInsuranceTypeSystem InsuranceSystem = new AssetInsuranceTypeSystem(InsuranceTypeId))
                    {
                        txtName.Text = InsuranceSystem.Name;
                        txtCompany.Text = InsuranceSystem.Company;
                        txtProduct.Text = InsuranceSystem.Product;
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
            if (InsuranceTypeId == 0)
            {
                txtName.Text = txtProduct.Text =txtCompany.Text=string.Empty;
                txtName.Focus();
            }
            else
            {
                //this.Close();
            }
        }

        private void LoadDefalut()
        {
            LoadInsurancePlans();
            LoadInsuranceCompany();
            LoadInsuranceProduct();
        }

        private void LoadInsurancePlans()
        {
            try
            {
                using (AssetInsuranceTypeSystem assetInsuranceSystem = new AssetInsuranceTypeSystem())
                {
                    resultArgs = assetInsuranceSystem.AutoFetchInsurancePlans();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[assetInsuranceSystem.AppSchema.ASSETInsuranceDetails.NAMEColumn.ColumnName].ToString());
                        }
                        txtName.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtName.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtName.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadInsuranceCompany()
        {
            try
            {
                using (AssetInsuranceTypeSystem assetInsuranceSystem = new AssetInsuranceTypeSystem())
                {
                    resultArgs = assetInsuranceSystem.AutoFetchInsurancePlans();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[assetInsuranceSystem.AppSchema.ASSETInsuranceDetails.COMPANYColumn.ColumnName].ToString());
                        }
                        txtCompany.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtCompany.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtCompany.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadInsuranceProduct()
        {
            try
            {
                using (AssetInsuranceTypeSystem assetInsuranceSystem = new AssetInsuranceTypeSystem())
                {
                    resultArgs = assetInsuranceSystem.AutoFetchInsurancePlans();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[assetInsuranceSystem.AppSchema.ASSETInsuranceDetails.PRODUCTColumn.ColumnName].ToString());
                        }
                        txtProduct.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtProduct.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtProduct.MaskBox.AutoCompleteCustomSource = collection;
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
            else if (string.IsNullOrEmpty(txtCompany.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_COMPANY_NAME_EMPTY));
                this.SetBorderColor(txtCompany);
                isInsuranceTrue = false;
                this.txtCompany.Focus();
            }
            else if (string.IsNullOrEmpty(txtProduct.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_PRODUCT_NAME_EMPTY));
                this.SetBorderColor(txtProduct);
                isInsuranceTrue = false;
                this.txtProduct.Focus();
            }
            return isInsuranceTrue;
        }

        /// <summary>
        /// Purpose:To Set the title.
        /// </summary>
        /// <returns></returns>
        public void SetTitle()
        {
            this.Text = InsuranceTypeId == 0 ? this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_EDIT_CAPTION);
        }
        #endregion

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCompany);
            txtCompany.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCompany.Text);
        }

        private void txtProduct_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtProduct);
            txtProduct.Text = this.UtilityMember.StringSet.ToSentenceCase(txtProduct.Text);
        }
    }
}
