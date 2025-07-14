using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model;
using Bosco.DAO;
using System.IO;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmLicenseKey :frmFinanceBaseAdd
    {
        #region Variables
        int CustomerId = 0;
        public event EventHandler UpdateHeld;
        SimpleEncrypt.SimpleEncDec objSimpleEncrypt = new SimpleEncrypt.SimpleEncDec();
        #endregion

        #region Constructor
        public frmLicenseKey()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        
        private void frmLicenseKey_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string XmlPath = string.Empty;
            try
            {
                if (ValidateInputDetails())
                {
                    DataTable dtEncrypt = ConstructDataColumns();
                    dtEncrypt.TableName = "LicenseKey";
                    dtEncrypt.Rows.Add(txtHeadOfficeCode.Text,txtBranchOfficeCode.Text, txtInstituteName.Text, txtSocietyName.Text, txtContactPerson.Text,memAddress.Text,txtPhone.Text,txtPlace.Text,txtState.Text,txtFax.Text,txtEmail.Text,txtPincode.Text,txtURL.Text,txtCountry.Text,"10","5",this.UtilityMember.NumberSet.ToInteger(rgbAccessToMultiDb.SelectedIndex.ToString())==1?0:1);
                    DataTable dres = EncryptLicenseDetails(dtEncrypt.AsDataView());
                    
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.License.LICENSE_GENERATE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        saveFileDialog1.Filter = "Xml Files|.xml";
                        saveFileDialog1.Title = "License Key";
                        saveFileDialog1.FileName = "AcMEERPLicense";
                        saveFileDialog1.DefaultExt = "xml";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            dtEncrypt.WriteXml(saveFileDialog1.FileName);
                        }
                        Clearcontrols();
                        //this.Close();
                    }
                }
              
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
          
        }
        private void Clearcontrols()
        {
            txtHeadOfficeCode.Text = txtBranchOfficeCode.Text = txtInstituteName.Text = txtSocietyName.Text = string.Empty;
            txtContactPerson.Text = memAddress.Text= txtURL.Text = txtState.Text = txtPlace.Text = txtPincode.Text = txtPhone.Text = string.Empty;
            txtFax.Text = txtEmail.Text = txtCountry.Text = string.Empty;
            txtHeadOfficeCode.Focus();
        }
        private void txtInstituteName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtInstituteName);
            //txtInstituteName.Text = String.IsNullOrEmpty(txtInstituteName.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtInstituteName.Text);
        }

        private void txtSocietyName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSocietyName);
            //txtSocietyName.Text = String.IsNullOrEmpty(txtSocietyName.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtSocietyName.Text);
        }

        private void txtContactPerson_Leave(object sender, EventArgs e)
        {
            //this.SetBorderColor(txtContactPerson);
            txtContactPerson.Text = String.IsNullOrEmpty(txtContactPerson.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtContactPerson.Text);
        }

        private void memAddress_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(memAddress);
            //memAddress.Text = String.IsNullOrEmpty(memAddress.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(memAddress.Text);
        }

        private void txtPlace_Leave(object sender, EventArgs e)
        {
            txtPlace.Text = String.IsNullOrEmpty(txtPlace.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtPlace.Text);
        }

        private void txtState_Leave(object sender, EventArgs e)
        {
            txtState.Text = String.IsNullOrEmpty(txtState.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtState.Text);
        }

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            txtCountry.Text = String.IsNullOrEmpty(txtCountry.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtCountry.Text);
        }

        private void txtPincode_Leave(object sender, EventArgs e)
        {
            //txtPincode.Text = String.IsNullOrEmpty(txtPincode.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtPincode.Text);
            txtPincode.Text = String.IsNullOrEmpty(txtPincode.Text.Trim()) ? string.Empty : txtPincode.Text;
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            //txtPhone.Text = String.IsNullOrEmpty(txtPhone.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtPhone.Text);
            txtPhone.Text = String.IsNullOrEmpty(txtPhone.Text.Trim()) ? string.Empty : txtPhone.Text;
        }

        private void txtFax_Leave(object sender, EventArgs e)
        {
            //txtFax.Text = String.IsNullOrEmpty(txtFax.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtFax.Text);
            txtFax.Text = String.IsNullOrEmpty(txtFax.Text.Trim()) ? string.Empty : txtFax.Text;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            //txtEmail.Text = String.IsNullOrEmpty(txtEmail.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtEmail.Text);
            txtEmail.Text = String.IsNullOrEmpty(txtEmail.Text.Trim()) ? string.Empty : txtEmail.Text;
        }

        private void txtURL_Leave(object sender, EventArgs e)
        {
            //txtURL.Text = String.IsNullOrEmpty(txtURL.Text.Trim()) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(txtURL.Text);
            txtURL.Text = String.IsNullOrEmpty(txtURL.Text.Trim()) ? string.Empty : txtURL.Text;
        }
        private void txtHeadOfficeCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtHeadOfficeCode);
        }

        private void txtBranchOfficeCode_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtBranchOfficeCode);
        }
        #endregion

        #region Methods
        private bool ValidateInputDetails()
        {
            bool Valid = true;
            if (string.IsNullOrEmpty(txtHeadOfficeCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.HEAD_OFFICE_CODE_EMPTY));
                this.SetBorderColor(txtHeadOfficeCode);
                txtHeadOfficeCode.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(txtBranchOfficeCode.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.BRANCH_OFFICE_CODE_EMPTY));
                this.SetBorderColor(txtBranchOfficeCode);
                txtBranchOfficeCode.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(txtInstituteName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.INSTITUTE_NAME_EMPTY));
                this.SetBorderColor(txtInstituteName);
                txtInstituteName.Focus();
                Valid = false;
            }
            else if (string.IsNullOrEmpty(txtSocietyName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.License.SOCIETY_NAME_EMPTY));
                this.SetBorderColor(txtSocietyName);
                txtSocietyName.Focus();
                Valid = false;
            }
            else if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!this.IsValidEmail(txtEmail.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                    Valid = false;
                    txtEmail.Focus();
                }
            }
            return Valid;
        }

        private DataTable EncryptLicenseDetails(DataView dvLicense)
        {
            if (dvLicense != null && dvLicense.Table.Rows.Count > 0)
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.URLColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.URLColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName].ToString());

                }
            }
            return dvLicense.Table;
        }

        //private DataTable DecryptLicenseDetails(DataView dvLicense)
        //{
        //    if (dvLicense != null && dvLicense.Table.Rows.Count > 0)
        //    {
        //        using (UserSystem userSystem = new UserSystem())
        //        {
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString());
        //            dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.URLColumn.ColumnName] = objSimpleEncrypt.DecryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.URLColumn.ColumnName].ToString());
                   //dvLicense.Table.Rows[0][userSystem.AppSchema.l.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName].ToString());
                   //dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.URLColumn.ColumnName] = objSimpleEncrypt.EncryptString(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName].ToString());
        //        }
        //    }
        //    return dvLicense.Table;
        //}

        public DataTable ConstructDataColumns()
        {
            DataTable dtColumns = new DataTable();
            using (UserSystem userSystem = new UserSystem())
            {
                DataColumn dcolHeadOfficeCode = new DataColumn(userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn.ColumnName, typeof(string));
                DataColumn dcolBranchOfficeCode = new DataColumn(userSystem.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn.ColumnName, typeof(string));
                DataColumn dcolInstituteName = new DataColumn(userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName, typeof(string));
                DataColumn dcolSocietyName = new DataColumn(userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName, typeof(string));
                DataColumn dcolContactPerson = new DataColumn(userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName, typeof(string));
                DataColumn dcolAddress = new DataColumn(userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName, typeof(string));
                DataColumn dcolPhone = new DataColumn(userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName, typeof(string));
                DataColumn dcolPlace = new DataColumn(userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName, typeof(string));
                DataColumn dcolstate = new DataColumn(userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName, typeof(string));
                DataColumn dcolfax = new DataColumn(userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName, typeof(string));
                DataColumn dcolEmail = new DataColumn(userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName, typeof(string));
                DataColumn dcolPincode = new DataColumn(userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName, typeof(string));
                DataColumn dcolCountry = new DataColumn(userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName, typeof(string));
                DataColumn dcolUrl = new DataColumn(userSystem.AppSchema.LegalEntity.URLColumn.ColumnName, typeof(string));
                DataColumn dcolNoofmodules = new DataColumn(userSystem.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName, typeof(string));
                DataColumn dcolNoNodes = new DataColumn(userSystem.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName, typeof(string));
                DataColumn dcolAccessToMultiDb = new DataColumn(userSystem.AppSchema.LicenseDataTable.AccessToMultiDBColumn.ColumnName, typeof(Int32));

                dtColumns.Columns.Add(dcolHeadOfficeCode);
                dtColumns.Columns.Add(dcolBranchOfficeCode);
                dtColumns.Columns.Add(dcolInstituteName);
                dtColumns.Columns.Add(dcolSocietyName);
                dtColumns.Columns.Add(dcolContactPerson);
                dtColumns.Columns.Add(dcolAddress);
                dtColumns.Columns.Add(dcolPhone);
                dtColumns.Columns.Add(dcolPlace);
                dtColumns.Columns.Add(dcolstate);
                dtColumns.Columns.Add(dcolfax);
                dtColumns.Columns.Add(dcolEmail);
                dtColumns.Columns.Add(dcolPincode);
                dtColumns.Columns.Add(dcolUrl);
                dtColumns.Columns.Add(dcolCountry);
                dtColumns.Columns.Add(dcolNoofmodules);
                dtColumns.Columns.Add(dcolNoNodes);
                dtColumns.Columns.Add(dcolAccessToMultiDb);
            }
            return dtColumns;
        }
        #endregion

        
    }
}