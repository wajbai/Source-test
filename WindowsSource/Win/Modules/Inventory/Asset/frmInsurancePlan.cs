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
    public partial class frmInsuranceAddPlan : frmFinanceBaseAdd
    {
        #region Event Decelaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Variable Decelaration
        int InsuranceTypeId = 0;
        private ResultArgs resultArgs = null;
        #endregion

        #region Constructors
        public frmInsuranceAddPlan()
        {
            InitializeComponent();
        }

        public frmInsuranceAddPlan(int Insurance_Id)
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

                    using (AssetInsurancePlanSystem InsuranceSystme = new AssetInsurancePlanSystem())
                    {
                        InsuranceSystme.InsuranceTypeId = InsuranceTypeId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : InsuranceTypeId;
                        InsuranceSystme.Name = txtName.Text.Trim();
                        InsuranceSystme.Company = txtCompany.Text.Trim();
                        resultArgs = InsuranceSystme.SaveInsuranceDetials();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

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
                    using (AssetInsurancePlanSystem InsuranceSystem = new AssetInsurancePlanSystem(InsuranceTypeId))
                    {
                        txtName.Text = InsuranceSystem.Name;
                        txtCompany.Text = InsuranceSystem.Company;
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
                txtName.Text = txtCompany.Text = string.Empty;
                //txtName.Focus();
                txtCompany.Focus();
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
        }

        private void LoadInsurancePlans()
        {
            try
            {
                using (AssetInsurancePlanSystem assetInsuranceSystem = new AssetInsurancePlanSystem())
                {
                    resultArgs = assetInsuranceSystem.AutoFetchInsurancePlans();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[assetInsuranceSystem.AppSchema.InsurancePlan.INSURANCE_PLANColumn.ColumnName].ToString());
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
                using (AssetInsurancePlanSystem assetInsuranceSystem = new AssetInsurancePlanSystem())
                {
                    resultArgs = assetInsuranceSystem.AutoFetchInsurancePlans();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[assetInsuranceSystem.AppSchema.InsurancePlan.COMPANYColumn.ColumnName].ToString());
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
        
        /// <summary>
        /// Purpose:To validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInsuranceType()
        {
            bool isInsuranceTrue = true;
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_COMPANY_NAME_EMPTY));
                this.SetBorderColor(txtCompany);
                isInsuranceTrue = false;
                this.txtCompany.Focus();
            }
            else if (string.IsNullOrEmpty(txtName.Text))
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
            this.Text = InsuranceTypeId == 0 ? this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.Insurance.INSURANCE_EDIT_CAPTION);
        }
        #endregion

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCompany);
            txtCompany.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCompany.Text);
        }
    }
}
