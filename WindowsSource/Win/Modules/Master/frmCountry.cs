/*  Class Name      :frmCountry
 *  Purpose         :To Save Country Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Linq;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Schema;
using System.Drawing;
using System.Collections;
using Bosco.DAO.Data;
using Bosco.DAO;

namespace ACPP.Modules.Master
{
    public partial class frmCountry : frmFinanceBaseAdd
    {
        #region EventsDeclaration
        public event EventHandler UpdateHeld;
        #endregion

        #region VariableDeclaration
        private int countryId = 0;
        ResultArgs resultArgs = null;
        BackupAndRestore bak = new BackupAndRestore();

        #endregion

        #region Properties
        public string DonorAduitorCountryName { get; set; }
        #endregion

        #region Constructor
        public frmCountry()
        {
            InitializeComponent();
        }
        public frmCountry(int CountryId)
            : this()
        {
            countryId = CountryId;
        }
        #endregion

        #region Events
        /// <summary>
        /// Load Country Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCountry_Load(object sender, EventArgs e)
        {
            CommonMethod common = new CommonMethod();
            DataTable dt = common.GetCurrencySymbolList();
            SetTitle();
            AssignCountryDetails();
            //if (countryId == 0)
            //{
            //    GetCurrencySymbolsDetails();
            //}
            //else
            //{
            //    GetCurrencySymbols();
            //}

            //bak.getCurrencySymbols();

            // GetCountry();
            // GetCountryCode();
            //GetCurrencyCode();
            //GetCurrencyName();
        }

        /// <summary>
        /// Save Country Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateCountryDetails())
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        countrySystem.CountryId = countryId == 0 ? (int)AddNewRow.NewRow : countryId;
                        countrySystem.CountryName = txtCountry.Text;
                        countrySystem.CountryCode = txtCountryCode.Text;
                        countrySystem.CurrencyCode = txtCode.Text;
                        //countrySystem.CurrencySymbol = glkSymbols.Text;
                        countrySystem.CurrencySymbol = txtSymbols.Text;
                        countrySystem.CurrencyName = txtName.Text;
                        countrySystem.ExchangeRate = UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                        resultArgs = countrySystem.SaveCountryDetails();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;
                            //this.ShowsuccessWaitDialog();
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Country.COUNTRY_SAVE_SUCCESS));
                            ClearControls();
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
            {
                this.CloseWaitDialog();
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// Load the Country
        /// </summary>
        //private void GetCountry()
        //{
        //    try
        //    {
        //        using (CountrySystem countrySystem = new CountrySystem())
        //        {
        //            resultArgs = countrySystem.FetchCountryListDetails();
        //            if (resultArgs.Success)
        //            {
        //                this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
        //                lkpCountry.EditValue = lkpCountry.Properties.GetDataSourceValue(lkpCountry.Properties.ValueMember, 0);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}

        /// <summary>
        /// Load the Country Code
        /// </summary>
        /// 
        //private void GetCountryCode()
        //{
        //    try
        //    {
        //        using (CountrySystem countrySystem = new CountrySystem())
        //        {
        //            resultArgs = countrySystem.FetchCountryCodeListDetails();
        //            if (resultArgs.Success)
        //            {
        //                this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpCountryCode, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRY_CODEColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
        //                lkpCountryCode.EditValue = lkpCountryCode.Properties.GetDataSourceValue(lkpCountryCode.Properties.ValueMember, 0);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}

        /// <summary>
        /// Load the Currency symbols
        /// </summary>
        // private void GetCurrencySymbols()
        // {
        //     try
        //     {
        //         using (CountrySystem countrySystem = new CountrySystem())
        //         {
        //             resultArgs = countrySystem.FetchCurrencySymbolsListDetails(countryId);
        //             if (resultArgs.Success)
        //             {
        //                 this.UtilityMember.ComboSet.BindGridLookUpCombo(glkSymbols, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCY_SYMBOLColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
        //                 glkSymbols.EditValue = glkSymbols.Properties.GetKeyValue(0);
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageRender.ShowMessage(ex.ToString(), true);
        //     }
        //     finally { }
        // }
        //public void GetCurrencySymbolsDetails()
        // {
        //     try
        //     {
        //         using (CountrySystem countrySystem = new CountrySystem())
        //         {
        //             resultArgs = countrySystem.FetchCurrencySymbolsList();
        //             if (resultArgs.Success)
        //             {
        //                 this.UtilityMember.ComboSet.BindGridLookUpCombo(glkSymbols, resultArgs.DataSource.Table, "CURRENCY_SYMBOLS", "CURRENCY_SYMBOLS");
        //                 glkSymbols.EditValue = glkSymbols.Properties.GetKeyValue(0);

        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageRender.ShowMessage(ex.ToString(), true);
        //     }
        //     finally { }
        // }

        /// <summary>
        /// Load the Currency Code
        /// </summary>
        /// 
        //private void GetCurrencyCode()
        //{
        //    try
        //    {
        //        using (CountrySystem countrySystem = new CountrySystem())
        //        {
        //            resultArgs = countrySystem.FetchCurrencyCodeListDetails();
        //            if (resultArgs.Success)
        //            {
        //                this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpCode, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCY_CODEColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
        //                lkpCode.EditValue = lkpCode.Properties.GetDataSourceValue(lkpCode.Properties.ValueMember, 0);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}

        /// <summary>
        /// Load the Currency Name
        /// </summary>
        //private void GetCurrencyName()
        //{
        //    try
        //    {
        //        using (CountrySystem countrySystem = new CountrySystem())
        //        {
        //            resultArgs = countrySystem.FetchCurrencyNameListDetails();
        //            if (resultArgs.Success)
        //            {
        //                this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpName, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCY_NAMEColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
        //                lkpName.EditValue = lkpName.Properties.GetDataSourceValue(lkpName.Properties.ValueMember, 0);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}


        public bool ValidateCountryDetails()
        {
            bool iscountryTrue = true;
            if (string.IsNullOrEmpty(txtCountry.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Country.COUNTRY_NAME_EMPTY));
                this.SetBorderColor(txtCountry);
                iscountryTrue = false;
                txtCountry.Focus();
            }
            else if (string.IsNullOrEmpty(txtCountryCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Country.COUNTRY_CODE_EMPTY));
                this.SetBorderColor(txtCountryCode);
                iscountryTrue = false;
                txtCountryCode.Focus();
            }
            //else if (string.IsNullOrEmpty(glkSymbols.Text.Trim()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Country.CURRENCY_SYMBOL_EMPTY));
            //    this.SetBorderColor(glkSymbols);
            //    iscountryTrue = false;
            //    glkSymbols.Focus();
            //}
            else if (string.IsNullOrEmpty(txtSymbols.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Country.CURRENCY_SYMBOL_EMPTY));
                this.SetBorderColor(txtSymbols);
                iscountryTrue = false;
                txtSymbols.Focus();
            }
            else if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Country.CURRENCY_CODE_EMPTY));
                this.SetBorderColor(txtCode);
                iscountryTrue = false;
                txtCode.Focus();

            }
            else if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Country.CURRENCY_NAME_EMPTY));
                this.SetBorderColor(txtName);
                iscountryTrue = false;
                txtName.Focus();
            }
            else if (this.AppSetting.AllowMultiCurrency == 1)
            {
                if (UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text) == 0)
                {
                    this.ShowMessageBox("Exchange Rate is empty");
                    this.SetBorderColor(txtExchangeRate);
                    iscountryTrue = false;
                    txtExchangeRate.Focus();
                }
                else
                {
                    /*DialogResult msgDialog = System.Windows.Forms.DialogResult.Yes;
                    if (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) == this.AppSetting.FirstFYDateFrom)
                    {
                        msgDialog = this.ShowConfirmationMessage("Since this is first Finance Year, Are you sure to update all " +
                                "Cash and Bank Opening Balance too ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                    iscountryTrue = (msgDialog == System.Windows.Forms.DialogResult.Yes);
                    txtExchangeRate.Focus();*/
                }
            }
            else
            {
                txtCountry.Focus();
            }
            return iscountryTrue;
        }

        /// <summary>
        /// Reload the Details
        /// </summary>
        /// 
        public void AssignCountryDetails()
        {
            try
            {
                if (countryId != 0)
                {
                    using (CountrySystem countryDetails = new CountrySystem(countryId))
                    {
                        txtCountry.Text = countryDetails.CountryName;
                        txtCountryCode.Text = countryDetails.CountryCode;
                        //glkSymbols.EditValue = countryDetails.CurrencySymbol;
                        txtSymbols.Text = countryDetails.CurrencySymbol;
                        txtCode.Text = countryDetails.CurrencyCode;
                        txtName.Text = countryDetails.CurrencyName;
                        txtExchangeRate.Text = countryDetails.ExchangeRate.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

        }
        public void ClearControls()
        {
            if (countryId == 0)
            {
                txtCountry.Text = txtCountryCode.Text = txtCode.Text = txtName.Text = txtSymbols.Text = string.Empty;
            }
            txtCountry.Focus();
        }
        /// <summary>
        /// Set Caption
        /// </summary>
        private void SetTitle()
        {
            this.Text = countryId == 0 ? this.GetMessage(MessageCatalog.Master.Country.COUNTRY_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Country.COUNTRY_EDIT_CAPTION);
            txtCountry.Focus();

            lcgExchangeRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                lcgExchangeRate.Text = lcgExchangeRate.Text + " for the period of " + UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).ToShortDateString() +
                    " - " + UtilityMember.DateSet.ToDate(AppSetting.YearTo, false).ToShortDateString();
                lcgExchangeRate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        #endregion

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCountry);
            txtCountry.Text = this.UtilityMember.StringSet.ToSentenceCase(txtCountry.Text);
        }

        private void txtCountryCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCountryCode);
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtCode);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
        }

        private void txtSymbols_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSymbols);
        }

        private void txtName_Leave_1(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
        }
    }
}


