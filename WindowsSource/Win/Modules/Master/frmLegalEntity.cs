using System;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.UIModel.Master;
using Bosco.DAO.Data;
using Bosco.Model.UIModel;



namespace ACPP.Modules.Master
{
    public partial class frmLegalEntity : frmFinanceBaseAdd
    {
        #region Variables
        int CustomerId = 0;
        int TempLedgerId = 0;
        bool AssignValuesStarted = false;
        public event EventHandler UpdateHeld;
        StateSystem state = new StateSystem();
        ResultArgs resultArgs = null;
        #endregion

        #region Construcor
        public frmLegalEntity()
        {
            InitializeComponent();
        }
        public frmLegalEntity(int CustomerId)
            : this()
        {
            this.CustomerId = CustomerId;
        }
        #endregion

        #region Events
        private void deRegDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EnableDate(deRegDate, e);
        }

        private void deRegDate_QueryPopUp(object sender, CancelEventArgs e)
        {
            PopUpClose(deRegDate, e);
        }

        private void dePermissionDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            EnableDate(dePermissionDate, e);
        }

        private void dePermissionDate_QueryPopUp(object sender, CancelEventArgs e)
        {
            PopUpClose(dePermissionDate, e);
        }

        private void frmLegalEntity_Load(object sender, EventArgs e)
        {
            SetHeight();
            LoadCountryDetails();
            LoadBankAccountDetails();
            LoadStateDetails();
            this.Text = SetTitle();
            lciOtherAssociationNature.Visibility = empOtherAssociationnature.Visibility = LayoutVisibility.Never;
            lciOtherDenomiation.Visibility = empOtherDenomiation.Visibility = LayoutVisibility.Never;
            AssignLegalEntityDetails();
            this.StartPosition = FormStartPosition.CenterParent;
            if (this.AppSetting.EnableGST.ToString().Equals("1"))
            {
                lblGST.Visibility = LayoutVisibility.Always;
            }
            // DisableControls();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
            }
            else
            {
                glkpState.Properties.Buttons[1].Visible = true;
                glkpCountry.Properties.Buttons[1].Visible = true;
            }

            //10/07/2024, If other than india country
            if (this.AppSetting.IsCountryOtherThanIndia)
            {
                //lcPANNo.Visibility = LayoutVisibility.Never;
                layoutControl1.HideItem(lcPANNo);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUserControl())
                {
                    using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                    {
                        legalEntitySystem.CustomerId = CustomerId;
                        legalEntitySystem.InstitueName = txtInstituteName.Text;
                        legalEntitySystem.SocietyName = txtsocietyName.Text;
                        legalEntitySystem.ContactPerson = txtConductPerson.Text;
                        legalEntitySystem.Address = MemoAddress.Text;
                        legalEntitySystem.Place = txtPlace.Text;
                        legalEntitySystem.Fax = txtFax.Text;
                        legalEntitySystem.GIRNo = txtGIRNo.Text;
                        legalEntitySystem.A12No = txt12ANo.Text;
                        legalEntitySystem.Phone = txtPhone.Text;
                        legalEntitySystem.StateId = this.UtilityMember.NumberSet.ToInteger(glkpState.EditValue.ToString());
                        legalEntitySystem.EMail = txtEmail.Text;
                        legalEntitySystem.PANNo = txtPanNo.Text;
                        legalEntitySystem.TANNo = txtTANNo.Text;
                        legalEntitySystem.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString());
                        legalEntitySystem.LedgerId = (glkpBankAccount.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpBankAccount.EditValue.ToString()) : 0;
                        legalEntitySystem.Pincode = txtPincode.Text;
                        legalEntitySystem.URL = txtUrl.Text;
                        legalEntitySystem.RegNo = txtRegNo.Text;
                        legalEntitySystem.PermissionNo = txtPermissionNo.Text;
                        legalEntitySystem.RegDate = deRegDate.DateTime;
                        legalEntitySystem.PermissionDate = dePermissionDate.DateTime;
                        //legalEntitySystem.AssoicationNature = GetSelectedNatureofAssociation();
                        legalEntitySystem.Denomination = rdogDemomination.SelectedIndex;
                        legalEntitySystem.FCRINo = txtFCRINo.Text;
                        legalEntitySystem.FCRIRegDate = deFCRIRegDate.DateTime;
                        legalEntitySystem.EightyGNo = txt80GNo.Text;
                        legalEntitySystem.EightyGNoRegDate = deEigtyGRegDate.DateTime;
                        legalEntitySystem.GSTNo = txtGSTINNo.Text;

                        if (chkOthers.Checked)
                        {
                            legalEntitySystem.OtherAssociationNature = txtOtherAssociationNature.Text;
                        }
                        legalEntitySystem.AssoicationNature = GetSelectedNatureofAssociation();
                        if (rdogDemomination.SelectedIndex == (int)Association.Others)
                        {
                            legalEntitySystem.OtherDenomination = txtOtherDenomination.Text;
                        }
                        else
                        {
                            legalEntitySystem.Denomination = rdogDemomination.SelectedIndex;
                        }
                        ResultArgs resultArgs = legalEntitySystem.SaveLegalEntityDetails();
                        if (resultArgs.Success)
                        {
                            this.ReturnValue = resultArgs.RowUniqueId;
                            this.ReturnDialog = System.Windows.Forms.DialogResult.OK;

                            //legalentityDialogResult = DialogResult.OK;
                            ShowSuccessMessage(GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_SAVE_SUCCESS));
                            if (CustomerId.Equals(0))
                                ClearControls();
                            txtsocietyName.Focus();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            //this.ShowMessageBox("Problem in Saving Legal Entity Details." + resultArgs.Message);
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.LegalEntity.PROBLEM_IN_SAVING) + resultArgs.Message);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                ShowMessageBox(ee.Message);
            }
        }
        private string GetSelectedNatureofAssociation()
        {
            string Selected = string.Empty;
            if (chkCultural.Checked)
            {
                Selected += (int)Association.Cultural + ",";
            }
            if (chkEconomic.Checked)
            {
                Selected += (int)Association.Economic + ",";
            }
            if (chkEducational.Checked)
            {
                Selected += (int)Association.Educational + ",";
            }
            if (chkReligious.Checked)
            {
                Selected += (int)Association.Religious + ",";
            }
            if (chkSocial.Checked)
            {
                Selected += (int)Association.Social + ",";
            }
            if (chkOthers.Checked)
            {
                Selected += (int)Association.Others + ",";
            }
            Selected = Selected.TrimEnd(',');
            return Selected;
        }

        private void txtInstituteName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtInstituteName);
            txtInstituteName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtInstituteName.Text);
        }

        private void txtsocietyName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtsocietyName);
            txtsocietyName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtsocietyName.Text);
        }

        private void txtConductPerson_Leave(object sender, EventArgs e)
        {
            txtConductPerson.Text = this.UtilityMember.StringSet.ToSentenceCase(txtConductPerson.Text);
        }

        private void memoEdit1_Leave(object sender, EventArgs e)
        {
            MemoAddress.Text = this.UtilityMember.StringSet.ToSentenceCase(MemoAddress.Text);
        }

        private void txtPlace_Leave(object sender, EventArgs e)
        {
            txtPlace.Text = this.UtilityMember.StringSet.ToSentenceCase(txtPlace.Text);
        }

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            glkpCountry.Text = this.UtilityMember.StringSet.ToSentenceCase(glkpCountry.Text);
        }

        //private void rdogAssociationNature_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SetHeight();
        //    if (rdogAssociationNature.SelectedIndex == (int)Association.Others)
        //    {
        //        lciOtherAssociationNature.Visibility = empOtherAssociationnature.Visibility = LayoutVisibility.Always;
        //        txtOtherAssociationNature.Focus();
        //        if (CustomerId.Equals(0))
        //        {
        //            txtOtherAssociationNature.Text = "";
        //        }
        //    }
        //    else
        //    {
        //        lciOtherAssociationNature.Visibility = empOtherAssociationnature.Visibility = LayoutVisibility.Never;
        //    }
        //    rdogDemomination.Enabled = txtOtherDenomination.Enabled = (rdogAssociationNature.SelectedIndex == (int)Association.Religious) ? true : false;
        //}

        private void rdogDemomination_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetHeight();
            if (rdogDemomination.SelectedIndex == (int)Association.Others)
            {
                lciOtherDenomiation.Visibility = empOtherDenomiation.Visibility = LayoutVisibility.Always;
                txtOtherDenomination.Focus();
                if (CustomerId.Equals(0))
                {
                    txtOtherDenomination.Text = "";
                }
            }
            else
            {
                lciOtherDenomiation.Visibility = empOtherDenomiation.Visibility = LayoutVisibility.Never;
            }
        }

        private void txtRegNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtRegNo);
        }

        private void txtOtherAssociationNature_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtOtherAssociationNature);
        }

        private void txtOtherDenomination_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtOtherDenomination);
        }

        private void glkpCountry_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpCountry);
        }
        #endregion

        #region Rights
        private void ApplyRights()
        {
            bool CreateState = (CommonMethod.ApplyUserRights((int)State.CreateState) != 0);
            glkpState.Properties.Buttons[1].Visible = CreateState;

            bool CreateCountry = (CommonMethod.ApplyUserRights((int)Forms.CreateCountry) != 0);
            glkpCountry.Properties.Buttons[1].Visible = CreateCountry;
        }
        #endregion

        #region Methods
        private string SetTitle()
        {
            return CustomerId.Equals(0) ? this.GetMessage(MessageCatalog.Master.LegalEntity.LEGAL_ENTITY_ADD) : this.GetMessage(MessageCatalog.Master.LegalEntity.LEGAL_ENTITY_EDIT);
        }
        /// <summary>
        /// Bind the country details to the lookup control
        /// </summary>

        private void LoadBankAccountDetails()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchUnmappedBankAccounts();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpBankAccount.Properties.DataSource = resultArgs.DataSource.Table;
                        glkpBankAccount.Properties.DisplayMember = "BANK_BRANCH";
                        glkpBankAccount.Properties.ValueMember = "LEDGER_ID";

                        glkpBankAccount.Properties.ImmediatePopup = true;
                        glkpBankAccount.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                        glkpBankAccount.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadCountryDetails()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryListDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ColumnName, countrySystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }


        private void SetHeight()
        {
            if (chkOthers.Checked &&
                rdogDemomination.SelectedIndex == (int)Association.Others)
            {
                this.Height = 693;// 650;
            }
            //else if (rdogDemomination.SelectedIndex == (int)Association.Others)
            //{
            //    this.Height = 630;
            //}
            //else if (rdogDemomination.SelectedIndex == (int)Association.Others && chkOthers.Checked == false)
            //{
            //    this.Height = 630;
            //}
            else
            {
                this.Height = 660;
            }
        }

        private void AssignLegalEntityDetails()
        {
            try
            {
                if (CustomerId != 0)
                {
                    AssignValuesStarted = true;
                    using (LegalEntitySystem legalEntity = new LegalEntitySystem(CustomerId))
                    {
                        //  txtInstituteName.Text = legalEntity.InstitueName;
                        txtsocietyName.Text = legalEntity.SocietyName;
                        txtConductPerson.Text = legalEntity.ContactPerson;
                        MemoAddress.Text = legalEntity.Address;
                        txtPlace.Text = legalEntity.Place;
                        txtFax.Text = legalEntity.Fax;
                        txtGIRNo.Text = legalEntity.GIRNo;
                        txt12ANo.Text = legalEntity.A12No;
                        txtPhone.Text = legalEntity.Phone;
                        glkpState.EditValue = legalEntity.StateId.ToString();
                        txtEmail.Text = legalEntity.EMail;
                        txtPanNo.Text = legalEntity.PANNo;
                        txtTANNo.Text = legalEntity.TANNo;
                        glkpCountry.EditValue = legalEntity.CountryId.ToString();
                        glkpBankAccount.EditValue = legalEntity.LedgerId.ToString();
                        TempLedgerId = this.UtilityMember.NumberSet.ToInteger(legalEntity.LedgerId.ToString());
                        txtPincode.Text = legalEntity.Pincode;
                        txtUrl.Text = legalEntity.URL;
                        txtRegNo.Text = legalEntity.RegNo;
                        txtPermissionNo.Text = legalEntity.PermissionNo;
                        if (!legalEntity.RegDate.Equals(deRegDate.Properties.MinValue))
                        {
                            deRegDate.DateTime = legalEntity.RegDate;
                        }
                        //deRegDate.DateTime = legalEntity.RegDate;
                        //  }
                        //  else
                        //   {
                        // deRegDate.Text = "";
                        //   }
                        if (legalEntity.PermissionDate.Equals(DateTime.MinValue))
                        {
                            dePermissionDate.Text = "";
                        }
                        else
                        {
                            dePermissionDate.DateTime = legalEntity.PermissionDate;
                        }
                        SetAssociationNature(legalEntity.AssoicationNature);
                        rdogDemomination.SelectedIndex = legalEntity.Denomination;
                        txtFCRINo.Text = legalEntity.FCRINo;
                        if (!legalEntity.FCRIRegDate.Equals(DateTime.MinValue))
                        {
                            deFCRIRegDate.DateTime = legalEntity.FCRIRegDate;
                        }
                        else
                        {
                            deFCRIRegDate.Text = string.Empty;
                        }
                        txt80GNo.Text = legalEntity.EightyGNo;
                        txtGSTINNo.Text = legalEntity.GSTNo;

                        if (!legalEntity.EightyGNoRegDate.Equals(DateTime.MinValue))
                        {
                            deEigtyGRegDate.DateTime = legalEntity.EightyGNoRegDate;
                        }
                        else
                        {
                            deEigtyGRegDate.Text = string.Empty;
                        }


                        if (chkOthers.Checked)
                        {
                            txtOtherAssociationNature.Text = legalEntity.OtherAssociationNature;
                        }
                        if (rdogDemomination.SelectedIndex == (int)Association.Others)
                        {
                            txtOtherDenomination.Text = legalEntity.OtherDenomination;
                        }
                        else
                        {
                            rdogDemomination.SelectedIndex = legalEntity.Denomination;
                        }
                        //EnableEntity(false);
                    }
                    AssignValuesStarted = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }
        private void SetAssociationNature(string assNature)
        {
            string[] nature = assNature.Split(',');
            for (int i = 0; i < nature.Length; i++)
            {
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Cultural)
                {
                    chkCultural.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Economic)
                {
                    chkEconomic.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Educational)
                {
                    chkEducational.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Religious)
                {
                    chkReligious.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Social)
                {
                    chkSocial.Checked = true;
                }
                if (this.UtilityMember.NumberSet.ToInteger(nature[i].ToString()) == (int)Association.Others)
                {
                    chkOthers.Checked = true;
                }
            }
        }

        private void EnableDate(DateEdit deControl, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
                {
                    if (deControl.Properties.ReadOnly)
                    {
                        deControl.Properties.Buttons[1].Image = imgCollection.Images[0];
                        deControl.Tag = true;
                        deControl.Properties.ReadOnly = false;
                        deControl.Text = DateTime.Now.ToShortDateString();
                        deControl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                        deControl.Properties.Appearance.BackColor = Color.White;
                    }
                    else
                    {
                        deControl.Properties.Buttons[1].Image = imgCollection.Images[1];
                        deControl.Tag = false;
                        deControl.Text = "";
                        deControl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                        deControl.Text = string.Empty;
                        deControl.Properties.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.StackTrace, true);
            }
            finally { }
        }

        private void PopUpClose(DateEdit deControl, CancelEventArgs e)
        {
            if (deControl.Properties.ReadOnly)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private bool ValidateUserControl()
        {
            bool Valid = true;
            //if (string.IsNullOrEmpty(txtInstituteName.Text.Trim()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.License.INSTITUTE_NAME_EMPTY));
            //    this.SetBorderColor(txtInstituteName);
            //    txtInstituteName.Focus();
            //    Valid = false;
            //    return Valid;
            //}
            if (string.IsNullOrEmpty(txtsocietyName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.SOCIETY_NAME_EMPTY));
                this.SetBorderColor(txtsocietyName);
                txtsocietyName.Focus();
                Valid = false;
                return Valid;
            }
            else if (glkpCountry.EditValue == null || glkpCountry.EditValue.ToString() == "0")
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Country.COUNTRY_NAME_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpCountry);
                glkpCountry.Focus();
                Valid = false;
                return Valid;
            }
            else if (glkpState.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.State.STATE_NAME_EMPTY));
                this.SetBorderColorForGridLookUpEdit(glkpState);
                glkpState.Focus();
                Valid = false;
                return Valid;
            }

            //else if (string.IsNullOrEmpty(txtRegNo.Text.Trim()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.License.SOCIETY_REG_NO));
            //    this.SetBorderColor(txtRegNo);
            //    txtRegNo.Focus();
            //    Valid = false;
            //    return Valid;
            //}
            else if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!this.IsValidEmail(txtEmail.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                    Valid = false;
                    txtEmail.Focus();
                    return Valid;
                }
            }
            else if (string.IsNullOrEmpty(txtRegNo.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.LEGAL_ENTITY_REGNO_EMPTY));
                Valid = false;
                txtRegNo.Focus();
                return Valid;
            }
            else if (!IsValidBankAccountSelection())
            {
                Valid = false;
                glkpBankAccount.Focus();
                return Valid;
            }
            //else if (string.IsNullOrEmpty(glkpBankAccount.Text))
            //{
            //    this.ShowMessageBox("FCRA Bank Account is empty");
            //    this.SetBorderColorForGridLookUpEdit(glkpBankAccount);
            //    glkpBankAccount.Focus();
            //    Valid = false;
            //    return Valid;
            //}
            else if (chkOthers.Checked && string.IsNullOrEmpty(txtOtherAssociationNature.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.LICENSE_ASSOCIATION_NATURE_EMPTY));
                this.SetBorderColor(txtOtherAssociationNature);
                txtOtherAssociationNature.Focus();
                Valid = false;
            }
            else if (chkReligious.Checked && rdogDemomination.SelectedIndex == -1)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.LICENSE_DENOMIATION_EMPTY));
                rdogDemomination.SelectedIndex = 4;
                Valid = false;
            }
            else if (rdogDemomination.SelectedIndex == (int)Association.Others && (string.IsNullOrEmpty(txtOtherDenomination.Text.Trim())))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.LICENSE_DENOMIATION_EMPTY));
                this.SetBorderColor(txtOtherDenomination);
                txtOtherDenomination.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(GetSelectedNatureofAssociation()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.LICENSE_ASSOCIATION_NATURE_EMPTY));
                chkCultural.Focus();
                Valid = false;
            }
            return Valid;
        }

        private bool IsValidBankAccountSelection()
        {
            bool isvalid = true;
            if (!AssignValuesStarted)
            {
                if (glkpBankAccount.EditValue != null && !string.IsNullOrEmpty(glkpBankAccount.Text))
                {
                    int LedgerId = this.UtilityMember.NumberSet.ToInteger(glkpBankAccount.EditValue.ToString());
                    if (LedgerId > 0)
                    {
                        if (TempLedgerId != LedgerId)
                        {
                            if (CustomerId > 0)
                            {
                                if (HasLedgerBalance(TempLedgerId.ToString()))
                                {
                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.LegalEntity.TRANSACTION_EXISTS));
                                    glkpBankAccount.EditValue = TempLedgerId.ToString();
                                    isvalid = false;
                                }
                            }

                            if (!IsBankMappedToLegalEntity(LedgerId))
                            {
                                isvalid = false;
                            }
                            else if (!IsBankMappedToProject(LedgerId)) //Mapping logic has been commanded because the logic must be changed
                            {
                                isvalid = false;
                            }

                        }
                    }
                }
            }
            return isvalid;
        }

        private bool IsBankMappedToProject(int LedgerId)
        {
            bool isValid = true;
            using (LedgerSystem ledger = new LedgerSystem())
            {
                ledger.LedgerId = LedgerId;
                resultArgs = ledger.CheckBankAccountMappedToProject();
                //Commanded by Carmel Raj, Validation should be done against the project mapped with Legal Entity

                //if (resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                //{
                //    this.ShowMessageBox("The selected FCRA Bank Account is already mapped with another Project.");
                //    glkpBankAccount.EditValue = null;
                //    isValid = false;
                //}
            }
            return isValid;
        }

        private bool IsBankMappedToLegalEntity(int LedgerId)
        {
            bool isValid = true;
            using (LedgerSystem ledger = new LedgerSystem())
            {
                ledger.LedgerId = LedgerId;
                resultArgs = ledger.CheckBankAccountMappedToLegalEntity(CustomerId);
                if (resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.LegalEntity.ALREADY_MAPPPED_WITH_ANOTHER_LEGAL_ENTITY));
                    if (CustomerId == 0)
                    {
                        glkpBankAccount.EditValue = null;
                    }
                    isValid = false;
                }
            }
            return isValid;
        }

        private bool HasLedgerBalance(string LedgerId)
        {
            bool isValid = false;
            if (glkpBankAccount.EditValue != null && !string.IsNullOrEmpty(glkpBankAccount.Text))
            {
                using (LedgerSystem ledger = new LedgerSystem())
                {
                    resultArgs = ledger.MadeTransactionByLedger(glkpBankAccount.EditValue.ToString());
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        private string GetCheckedItem()
        {
            string SelectedItem = string.Empty;
            return SelectedItem;
        }

        private void ClearControls()
        {
            foreach (Control ctrl in this.Controls[0].Controls)
            {
                if (ctrl is GridLookUpEdit)
                    ctrl.Text = string.Empty;
                else if (ctrl is TextEdit)
                    ctrl.Text = string.Empty;
            }
            chkCultural.Checked = chkEconomic.Checked = chkEducational.Checked = chkOthers.Checked = chkReligious.Checked = chkSocial.Checked = false;
            rdogDemomination.SelectedIndex = -1;
            glkpState.EditValue = null;
            txtInstituteName.Focus();
            deRegDate.Text = "";
        }

        public void DisableControls()
        {
            foreach (Control ctrl in this.Controls[0].Controls)
            {
                if (ctrl is GridLookUpEdit)
                    ctrl.Enabled = false;
                else if (ctrl is TextEdit)
                    ctrl.Enabled = false;
            }
            chkCultural.Enabled = chkEconomic.Enabled = chkEconomic.Checked = chkEducational.Enabled = chkOthers.Enabled = chkReligious.Enabled = chkSocial.Enabled =
            rdogDemomination.Enabled = glkpState.Enabled = txtInstituteName.Enabled = deRegDate.Enabled = false;
            glkpBankAccount.Enabled = true;
        }
        public void EnableEntity(bool isValue)
        {
            txtInstituteName.Enabled = txtsocietyName.Enabled = txtConductPerson.Enabled = txtPlace.Enabled = txtFax.Enabled = txtGIRNo.Enabled =
            txt12ANo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtPanNo.Enabled = txtTANNo.Enabled =
            txtPincode.Enabled = txtUrl.Enabled = txtPermissionNo.Enabled = deRegDate.Enabled = dePermissionDate.Enabled =
            chkCultural.Enabled = chkEconomic.Enabled = chkEducational.Enabled = chkOthers.Enabled = chkReligious.Enabled = chkSocial.Enabled =
            rdogDemomination.Enabled = txtOtherAssociationNature.Enabled = txtOtherDenomination.Enabled =
            txtFCRINo.Enabled = deFCRIRegDate.Enabled = txt80GNo.Enabled = isValue;

            // glkpCountry.Enabled = txtRegNo.Enabled = txtState.Enabled =
        }

        #endregion

        private void glkpCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpCountry.EditValue != null)
            {
                state.CountryId = glkpCountry.EditValue != null && !string.IsNullOrEmpty(glkpCountry.Text) ? this.UtilityMember.NumberSet.ToInteger(glkpCountry.EditValue.ToString()) : 0;
                resultArgs = state.FetchStateListDetails();
                if (resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpState, resultArgs.DataSource.Table, state.AppSchema.State.STATE_NAMEColumn.ColumnName, state.AppSchema.State.STATE_IDColumn.ColumnName);
                    // glkpState.EditValue = glkpState.Properties.GetKeyValue(0);
                }
            }
        }

        private void glkpBankAccount_EditValueChanged(object sender, EventArgs e)
        {
            IsValidBankAccountSelection();
        }

        private void glkpBankAccount_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                if (glkpBankAccount.EditValue != null && !string.IsNullOrEmpty(glkpBankAccount.Text))
                {
                    if (HasLedgerBalance(glkpBankAccount.EditValue.ToString()))
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.LegalEntity.CANNOT_BE_UNMAPPED));
                    }
                    else
                    {
                        glkpBankAccount.EditValue = null;
                    }
                }
            }
        }

        // Previous Given 

        //    else if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
        //    {
        //        //frmBankAccount objBankAccount = new frmBankAccount();
        //        //objBankAccount.ShowDialog();
        //        frmLedgerDetailAdd objBankAccount = new frmLedgerDetailAdd(ledgerSubType.BK);
        //        objBankAccount.ShowDialog();
        //        LoadBankAccountDetails();

        //    }


        private void glkpCountry_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmCountry frmCountryAdd = new frmCountry();
                    frmCountryAdd.ShowDialog();
                    if (frmCountryAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadCountryDetails();
                        if (frmCountryAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmCountryAdd.ReturnValue.ToString()) > 0)
                        {
                            glkpCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(frmCountryAdd.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }
        /// <summary>
        /// Bind the State details to the lookup control
        /// </summary>
        /// 
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
                        //glkpState.EditValue = glkpState.Properties.GetDisplayValue(0);
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

        private void glkpState_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
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
        }

        private void chkOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthers.Checked) // && rdogDemomination.SelectedIndex != (int)Association.Others && rdogDemomination.Enabled == true
            {
                this.Height = 660;
                lciOtherAssociationNature.Visibility = empOtherAssociationnature.Visibility = LayoutVisibility.Always;
                txtOtherAssociationNature.Focus();
                if (CustomerId.Equals(0))
                {
                    txtOtherAssociationNature.Text = "";
                }
            }
            else if (chkOthers.Checked && rdogDemomination.SelectedIndex == (int)Association.Others)
            {
                this.Height = 660;
                lciOtherAssociationNature.Visibility = empOtherAssociationnature.Visibility = LayoutVisibility.Always;
                txtOtherAssociationNature.Focus();
                if (CustomerId.Equals(0))
                {
                    txtOtherAssociationNature.Text = "";
                }
            }
            else
            {
                this.Height = 630;
                lciOtherAssociationNature.Visibility = empOtherAssociationnature.Visibility = LayoutVisibility.Never;
            }


        }
        private void chkReligious_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReligious.Checked)
            {
                rdogDemomination.Enabled = txtOtherDenomination.Enabled = true;
                rdogDemomination.SelectedIndex = 3;
            }
            else
            {
                rdogDemomination.SelectedIndex = -1;
                txtOtherDenomination.Text = string.Empty;
                rdogDemomination.Enabled = txtOtherDenomination.Enabled = false;
            }
        }

        private void glkpState_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkpBankAccount_Properties_ButtonClick_1(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    frmLedgerDetailAdd frmLedgerDetailAdd = new frmLedgerDetailAdd(ledgerSubType.BK);
                    frmLedgerDetailAdd.ShowDialog();
                    if (frmLedgerDetailAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadBankAccountDetails();
                        if (frmLedgerDetailAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString()) > 0)
                        {
                            glkpBankAccount.EditValue = this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }
    }
}
