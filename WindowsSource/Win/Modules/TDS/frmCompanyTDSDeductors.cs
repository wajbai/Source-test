using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.TDS;
using Bosco.Utility;

namespace ACPP.Modules.TDS
{
    public partial class frmCompanyTDSDeductors : frmFinanceBaseAdd
    {
        public frmCompanyTDSDeductors()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResultArgs resultArgs = null;
            if (ValidateEmail())
            {
                using (TDSCompanyDeductorSystem TDSDeductor = new TDSCompanyDeductorSystem())
                {
                    TDSDeductor.TaxAccountNo = string.Empty;
                    TDSDeductor.HeadOfficeTANNo = txtTANNo.Text;
                    TDSDeductor.PANNo = txtPANNo.Text;
                    TDSDeductor.TaxRegistrationNo = string.Empty;
                    TDSDeductor.IncomeTaxCircle = txtIncomeTaxCircle.Text;
                    TDSDeductor.DeductorType = cboDeductorType.Text;
                    TDSDeductor.ResponsiblePerson = txtPersonResponsible.Text;
                    TDSDeductor.SonDaughterOf = txtSonDaughterOf.Text;
                    TDSDeductor.Designation = txtDesignation.Text;
                    TDSDeductor.Address = txtAddress.Text;
                    TDSDeductor.FlatNo = txtFlatNo.Text;
                    TDSDeductor.Premises = txtPremises.Text;
                    TDSDeductor.Street = txtStreet.Text;
                    TDSDeductor.Location = txtLocation.Text;
                    TDSDeductor.District = txtDistrict.Text;
                    TDSDeductor.State = txtState.Text;
                    TDSDeductor.Pincode = txtPincode.Text;
                    TDSDeductor.TelephoneNo = txtTelephoneNo.Text;
                    TDSDeductor.Email = txtEmail.Text;
                    TDSDeductor.FullName = txtFullName.Text;

                    resultArgs = TDSDeductor.SaveTDSCompanyDeductor();
                    if (!resultArgs.Success)
                    {
                        //this.ShowMessageBoxWarning("Problem in Saving TDS company Deductor Details." + Environment.NewLine + resultArgs.Message);
                        this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.TDS.CompanyTDSDedutors.TDS_PROBLEM_SOLVING) + Environment.NewLine + resultArgs.Message);
                    }
                    else
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                    }
                }
            }
        }

        private void frmCompanyTDSDeductors_Load(object sender, EventArgs e)
        {
            AssignTDSDeductorDetails();
        }

        private void AssignTDSDeductorDetails()
        {
            using (TDSCompanyDeductorSystem TDSDeductor = new TDSCompanyDeductorSystem())
            {
                ResultArgs result = TDSDeductor.FetchCompanyDeductor();
                if (result != null && result.Success && result.DataSource.Table.Rows.Count > 0)
                {
                    //txtAccountNumber.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.TAX_DEDUCTION_ACCOUNT_NOColumn.ColumnName].ToString();
                    txtDesignation.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.DESIGNATIONColumn.ColumnName].ToString();
                    txtIncomeTaxCircle.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.INCOME_TAX_CIRCLEColumn.ColumnName].ToString();
                    txtPANNo.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.PAN_NOColumn.ColumnName].ToString();
                    txtPersonResponsible.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.RESPONSIBLE_PERSONColumn.ColumnName].ToString();
                    txtSonDaughterOf.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.SON_DAUGHTER_OFColumn.ColumnName].ToString();
                    txtTANNo.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.HEAD_OFFICE_TAN_NOColumn.ColumnName].ToString();
                    txtFullName.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.FULL_NAMEColumn.ColumnName].ToString();
                    if (string.IsNullOrEmpty(txtFullName.Text))
                    {
                        txtFullName.Text = this.LoginUser.SocietyName;
                    }
                    cboDeductorType.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.DEDUCTOR_TYPEColumn.ColumnName].ToString();
                    txtAddress.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.ADDRESSColumn.ColumnName].ToString();
                    txtFlatNo.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.FLAT_NOColumn.ColumnName].ToString();
                    txtPremises.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.PREMISESColumn.ColumnName].ToString();
                    txtStreet.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.STREETColumn.ColumnName].ToString();
                    txtLocation.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.LOCATIONColumn.ColumnName].ToString();
                    txtDistrict.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.DISTRICTColumn.ColumnName].ToString();
                    txtState.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.STATEColumn.ColumnName].ToString();
                    txtPincode.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.PINCODEColumn.ColumnName].ToString();
                    txtTelephoneNo.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.TELEPHONE_NOColumn.ColumnName].ToString();
                    txtEmail.Text = result.DataSource.Table.Rows[0][TDSDeductor.AppSchema.TdsCompanyDeductor.EMAILColumn.ColumnName].ToString();
                }
                else
                {
                    txtFullName.Text = this.LoginUser.SocietyName;
                }
            }
        }


        private bool ValidateEmail()
        {
            bool isSuccess = true;
            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (!this.IsValidEmail(txtEmail.Text.Trim()))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_EMAIL_INVALID));
                    isSuccess = false;
                    txtEmail.Focus();
                }
            }
            return isSuccess;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}