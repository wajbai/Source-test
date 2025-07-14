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


namespace ACPP.Modules.Master
{
    public partial class frmStateAdd : frmFinanceBaseAdd
    {
        #region Variable Decelaration
        private int stateId = 0;
        private ResultArgs resultArgs = null;

        #endregion

        #region Event Declaration
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructors


        public frmStateAdd()
        {
            InitializeComponent();
        }


        public frmStateAdd(int StateId)
            : this()
        {
            stateId = StateId;
        }

        #endregion

        private void frmStateAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadCountryDetails();
            AssignStateDetails();
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
                        //glkpCountry.EditValue = glkpCountry.Properties.GetKeyValue(0);
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateStateDetails())
                {
                    ResultArgs resultArgs = null;
                    using (StateSystem stateSystem = new StateSystem())
                    {
                        stateSystem.StateId = stateId == 0 ? this.UtilityMember.NumberSet.ToInteger(AddNewRow.NewRow.ToString()) : stateId;
                        stateSystem.StateCode = txtStateCode.Text;
                        stateSystem.StateName = txtStateName.Text.Trim();
                        stateSystem.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());

                        resultArgs = stateSystem.SaveStateDetails();

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
                            glkpCountry.Focus();
                           // this.ShowMessageBoxError("Problem in Saving State Details. " +resultArgs.Message);
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
        public bool ValidateStateDetails()
        {
            bool isStateTrue = true;

            if (string.IsNullOrEmpty(glkpCountry.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.State.STATE_COUNTRY_NAME_EMPTY));
                this.SetBorderColor(glkpCountry);
                isStateTrue = false;
                glkpCountry.Focus();
            }
            else if (string.IsNullOrEmpty(txtStateName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.State.STATE_NAME_EMPTY));
                this.SetBorderColor(txtStateName);
                isStateTrue = false;
                txtStateName.Focus();
            }
            return isStateTrue;

        }
        public void AssignStateDetails()
        {
            try
            {
                if (stateId != 0)
                {
                    using (StateSystem stateSystem = new StateSystem(stateId))
                    {
                        txtStateName.Text = stateSystem.StateName;
                        txtStateCode.Text = stateSystem.StateCode;
                        glkpCountry.EditValue = stateSystem.CountryId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void ClearControls()
        {
            if (stateId == 0)
            {
                txtStateName.Text = glkpCountry.Text = string.Empty;
                glkpCountry.EditValue = null;
            }
            glkpCountry.Focus();
        }
        private void SetTitle()
        {
            this.Text = stateId == 0 ? this.GetMessage(MessageCatalog.Master.State.STATE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.State.STATE_EDIT_CAPTION);
            txtStateName.Focus();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void glkpCountry_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpCountry);
        }
        private void txtStateName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtStateName);
            txtStateName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtStateName.Text);
        }

        private void glkpCountry_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
                {
                    if (this.AppSetting.LockMasters == (int)YesNo.No)
                    {
                        frmCountry frmcountryAdd = new frmCountry();
                        frmcountryAdd.ShowDialog();
                        if (frmcountryAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                        {
                            LoadCountryDetails();
                            if (frmcountryAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmcountryAdd.ReturnValue.ToString()) > 0)
                            {
                                glkpCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(frmcountryAdd.ReturnValue.ToString());
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                    }
                    //frmCountry frmcountry = new frmCountry();
                    //frmcountry.ShowDialog();
                    //LoadCountryDetails();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

        }

    }
}