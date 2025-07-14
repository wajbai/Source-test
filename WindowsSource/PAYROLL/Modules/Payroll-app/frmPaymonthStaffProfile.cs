using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Columns;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPaymonthStaffProfile : frmPayrollBase
    {
        #region Declaration

        public event EventHandler UpdateHeld;
        #endregion

        #region Properties

        private Int32 staffid = 0;
        private Int32 StaffId
        {
            set
            {
                staffid = value;
            }
            get { return staffid; }
        }

        private int payrollgroupid = 0;
        private int PayrollGroupid
        {
            get
            {
                return (glkpGroup.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpGroup.EditValue.ToString()) : 0;
            }
            set
            {
                glkpGroup.EditValue = value;
            }
        }

        private int paymentmoideid = 0;
        private int PaymentModeId
        {
            get
            {
                return (glkpPaymentMode.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpPaymentMode.EditValue.ToString()) : 0;
            }
            set
            {
                glkpPaymentMode.EditValue = value;
            }
        }
        
        private string ApplicableStatutoryCompliances
        {
            get
            {
                string fromledgers = string.Empty;
                chkListStatutoryCompliances.RefreshEditValue();
                List<object> selecteditems = chkListStatutoryCompliances.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    fromledgers += item.ToString() + ",";
                }
                fromledgers = fromledgers.TrimEnd(',');
                return fromledgers;
            }
            set
            {
                chkListStatutoryCompliances.SetEditValue(value);
            }
        }

        private int payrolldepartmentid = 0;
        private int PayrollDepartmentId
        {
            get
            {
                return (glkpDepartment.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpDepartment.EditValue.ToString()) : 0;
            }
            set
            {
                glkpDepartment.EditValue = value;
            }
        }

        private int payrollworklocationid = 0;
        private int PayrollWorkLocationId
        {
            get
            {
                return (glkpWorkLocation.EditValue != null) ? UtilityMember.NumberSet.ToInteger(glkpWorkLocation.EditValue.ToString()) : 0;
            }
            set
            {
                glkpWorkLocation.EditValue = value;
            }
        }
        
       
        #endregion

        #region Constructor
        public frmPaymonthStaffProfile(Int32 nstaffid)
        {
            InitializeComponent();
            staffid = nstaffid;

            LoadPayrollGroups();
            LoadPaymentModes();
            LoadStatutoryCompliances();
            FillPaymonthStaffProfileDetails();

            //On 21/11/2023, To load Payroll Departments and Work Location
            LoadPayrollDepartments();
            LoadPayrollWorkLocation();

             //On 06/12/2024, To hide few properties for other or multi currency enabled ----------------
            if (this.AppSetting.AllowMultiCurrency == 1 || this.AppSetting.IsCountryOtherThanIndia)
            {
                lcStatutoryCompliances.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        #endregion

        #region Methods

        private void LoadPayrollGroups()
        {
            try
            {
                using (clsPayrollStaff paystaff = new clsPayrollStaff())
                {
                    ResultArgs resultArgs = paystaff.FetchGroups("Group");
                    glkpGroup.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroup, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
                        glkpGroup.EditValue = glkpGroup.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadPaymentModes()
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    ResultArgs resultArgs = Paysystem.FetchPayrollPaymentMode();
                    glkpPaymentMode.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpPaymentMode, resultArgs.DataSource.Table, Paysystem.AppSchema.STFPERSONAL.PAYMENT_MODEColumn.ColumnName,
                                Paysystem.AppSchema.STFPERSONAL.PAYMENT_MODE_IDColumn.ColumnName, true, " ");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadPayrollDepartments()
        {
            try
            {
                using (PayrollDepartmentSystem payrolldep = new PayrollDepartmentSystem())
                {
                    ResultArgs resultArgs = payrolldep.FetchPayrollDepartments();
                    glkpDepartment.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpDepartment, resultArgs.DataSource.Table,
                                payrolldep.AppSchema.PayrollDepartment.DEPARTMENTColumn.ColumnName,
                                payrolldep.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName, true, " ");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadPayrollWorkLocation()
        {
            try
            {
                using (PayrollWorkLocationSystem payrollworklocation = new PayrollWorkLocationSystem())
                {
                    ResultArgs resultArgs = payrollworklocation.FetchPayrollWorkLocation();
                    glkpWorkLocation.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpWorkLocation, resultArgs.DataSource.Table,
                                payrollworklocation.AppSchema.PayrollWorkLocation.WORK_LOCATIONColumn.ColumnName,
                                payrollworklocation.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn.ColumnName, true, " ");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadStatutoryCompliances()
        {
            PayRollProcessComponent payrollcomponent = new PayRollProcessComponent();
            DataView dvProcessComponent = this.UtilityMember.EnumSet.GetEnumDataSource(payrollcomponent, Sorting.Descending);
            string onlyStatutoryCompliances = "ID IN (" + (Int32)PayRollProcessComponent.EPF + "," + (Int32)PayRollProcessComponent.ESI + "," + (Int32)PayRollProcessComponent.PT + ")";
            dvProcessComponent.RowFilter = onlyStatutoryCompliances;
            dvProcessComponent.Sort = "NAME";

            chkListStatutoryCompliances.Properties.DataSource = dvProcessComponent.ToTable();
            chkListStatutoryCompliances.Properties.ValueMember = "Id";
            chkListStatutoryCompliances.Properties.DisplayMember = "Name";
        }

        private void FillPaymonthStaffProfileDetails()
        {
            try
            {
                lcPayMonth.Text = "Paymonth - " + clsGeneral.PAYROLL_MONTH;
                using (clsPayrollStaff payrollstaff = new clsPayrollStaff())
                {
                    ResultArgs resultArgs = payrollstaff.FetchPaymonthStaffProfile(StaffId, clsGeneral.PAYROLL_ID);
                    
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtStaffProfile =  resultArgs.DataSource.Table;
                        lblStaffCodeValue.Text = dtStaffProfile.Rows[0][payrollstaff.AppSchema.STFPERSONAL.EMPNOColumn.ColumnName].ToString().ToString();
                        lblStaffNameValue.Text = dtStaffProfile.Rows[0]["Name"].ToString().ToString();
                        PayrollGroupid = dtStaffProfile.Rows[0][payrollstaff.AppSchema.PRSTAFFGROUP.GROUPIDColumn.ColumnName] == null ? 0 : UtilityMember.NumberSet.ToInteger(dtStaffProfile.Rows[0][payrollstaff.AppSchema.PRSTAFFGROUP.GROUPIDColumn.ColumnName].ToString());
                        PaymentModeId = dtStaffProfile.Rows[0][payrollstaff.AppSchema.STFPERSONAL.PAYMENT_MODE_IDColumn.ColumnName] == null ? 0 : UtilityMember.NumberSet.ToInteger(dtStaffProfile.Rows[0][payrollstaff.AppSchema.STFPERSONAL.PAYMENT_MODE_IDColumn.ColumnName].ToString());
                        PayrollDepartmentId = dtStaffProfile.Rows[0][payrollstaff.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName] == null ? 0 : UtilityMember.NumberSet.ToInteger(dtStaffProfile.Rows[0][payrollstaff.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName].ToString());
                        PayrollWorkLocationId = dtStaffProfile.Rows[0][payrollstaff.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn.ColumnName] == null ? 0 : UtilityMember.NumberSet.ToInteger(dtStaffProfile.Rows[0][payrollstaff.AppSchema.PayrollWorkLocation.WORK_LOCATION_IDColumn.ColumnName].ToString());
                        txtAccountNumber.Text = dtStaffProfile.Rows[0][payrollstaff.AppSchema.STFPERSONAL.ACCOUNT_NUMBERColumn.ColumnName].ToString();
                        txtAccountIFSCCode.Text = dtStaffProfile.Rows[0][payrollstaff.AppSchema.STFPERSONAL.ACCOUNT_IFSC_CODEColumn.ColumnName].ToString();
                        txtAccountBankBranch.Text = dtStaffProfile.Rows[0][payrollstaff.AppSchema.STFPERSONAL.ACCOUNT_BANK_BRANCHColumn.ColumnName].ToString();
                        ApplicableStatutoryCompliances = dtStaffProfile.Rows[0][payrollstaff.AppSchema.STFPERSONAL.STATUTORY_COMPLIANCEColumn.ColumnName].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        #endregion

        #region Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ResultArgs resultArgs = new ResultArgs();
                if (this.ShowConfirmationMessage("Are you sure changes will be updated in the current Paymonth only ?",MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (clsPayrollStaff payrollstaff = new clsPayrollStaff())
                    {
                        //On 14/02/2023, To assing Bank Account, IFSCOOCDE, BankBranch and payment Mode
                        payrollstaff.AccountNo = txtAccountNumber.Text.Trim();
                        payrollstaff.AccountIFSCCODE = txtAccountIFSCCode.Text.Trim();
                        payrollstaff.AccountBankBranch = txtAccountBankBranch.Text.Trim();
                        payrollstaff.PayrollPaymentModeId = PaymentModeId;
                        payrollstaff.PayrollDepartmentId= PayrollDepartmentId;
                        payrollstaff.PayrollWorkLocationId = PayrollWorkLocationId;
                        payrollstaff.StaffStatutoryCompliance = ApplicableStatutoryCompliances;

                        resultArgs = payrollstaff.UpdatePaymonthStaffProfileDetails(StaffId, PayrollGroupid);
                    }
                }
                if (resultArgs.Success)
                {
                    if (UpdateHeld != null)
                    {
                        UpdateHeld(this, e);
                    }
                    this.ShowMessageBox("Updated Successfully");
                }
                else
                {
                    this.ShowMessageBox(resultArgs.Message);
                }
            }
            catch(Exception err) {
                this.ShowMessageBox(err.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        
        #endregion

       

    }
}