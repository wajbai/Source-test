/// On 03/03/2021, In Budget Annual Summary - New Project, 
/// Few Clients asks us to name it as New Budget with income, expenditure and Province Help, but 
/// Manfort asks us to name it as Development Projects and wanted few more extra details like local fund and govt fund


using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using Bosco.Utility;
using Bosco.Model.Setting;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using Bosco.Utility.ConfigSetting;
using System.Windows.Forms;
using Payroll.Model.UIModel;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPayrollSetting : frmPayrollBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        DataTable dtPayrollSetting = new DataTable();
        private DataTable dtCompareSetting = new DataTable();
        #endregion

        #region Property

        #endregion

        #region Constructors
        public frmPayrollSetting()
        {
            InitializeComponent();
        }
        #endregion

      
        #region Events
        private void frmPayrollSetting_Load(object sender, EventArgs e)
        {
            lcCustomTotalDaysLOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcPayMonthTotalDaysLOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            BindValues();
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidation())
                {
                    AssignPayrollSetting();
                    using (SettingSystem payrollSetting = new SettingSystem())
                    {
                        resultArgs = payrollSetting.SaveSetting(dtPayrollSetting);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }


        private void radioLOPMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lcgrpLOPAutomatic.Enabled = (radioLOPMode.SelectedIndex == 0);
        }

        private void radioTotalLOPDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            lcPayMonthTotalDaysLOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcCustomTotalDaysLOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (radioTotalLOPDays.SelectedIndex == 0)
            {
                lcCustomTotalDaysLOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (radioTotalLOPDays.SelectedIndex == 1)
            {
                lcPayMonthTotalDaysLOP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        #endregion


        #region Methods
        private void BindValues()
        {
            using (SettingSystem payrollsetting = new SettingSystem())
            {
                ResultArgs resultArg = payrollsetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
                if (resultArg.Success && resultArg.DataSource.Table != null && resultArg.DataSource.Table.Rows.Count >0)
                {
                    dtPayrollSetting = resultArg.DataSource.Table;
                    DataTable dt = dtCompareSetting = dtPayrollSetting.AsDataView().ToTable();
                }
            } 
        }

        private string GetPayrollSettingInfo(string name)
        {
            string val = "";
            try
            {
                if (dtPayrollSetting != null && dtPayrollSetting.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPayrollSetting.Rows.Count; i++)
                    {
                        string record = dtPayrollSetting.Rows[i]["NAME"].ToString();
                        if (name == record)
                        {
                            val = dtPayrollSetting.Rows[i]["Value"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return val;
        }

        private void AssignPayrollSetting()
        {

            PayrollSetting PayrollSetting = new PayrollSetting();
            DataView dvPayrollSetting = null;
            dvPayrollSetting = this.UtilityMember.EnumSet.GetEnumDataSource(PayrollSetting, Sorting.Ascending);
            dtPayrollSetting = dvPayrollSetting.ToTable();
            if (dtPayrollSetting != null)
            {
                dtPayrollSetting.Columns.Add("Value", typeof(string));
                dtPayrollSetting.Columns.Add("UserId", typeof(string));

                for (int i = 0; i < dtPayrollSetting.Rows.Count; i++)
                {
                    PayrollSetting SettingName = (PayrollSetting)Enum.Parse(typeof(PayrollSetting), dtPayrollSetting.Rows[i][1].ToString());
                    string Value = "";
                    switch (SettingName)
                    {

                        case PayrollSetting.LOPMode:
                            {
                                Value = radioLOPMode.SelectedIndex.ToString();
                                break;
                            }
                        case PayrollSetting.LOPAutomaticTotalDaysOption:
                            {
                                Value = radioTotalLOPDays.SelectedIndex.ToString();
                                break;
                            }
                        case PayrollSetting.LOPAutomaticCustomTotalDays:
                            {
                                Value = txtCustomTotalDaysLOP.Text;
                                break;
                            }
                        case PayrollSetting.LOPAutomaticTotalDaysComponent:
                            {
                                Value = "0";
                                break;
                            }
                        case PayrollSetting.LOPAutomaticShowProcessedValues:
                            {
                                Value = radioShowProcessedValues.SelectedIndex.ToString();
                                break;
                            }            
                    }
                    dtPayrollSetting.Rows[i][2] = Value;
                    dtPayrollSetting.Rows[i][3] = this.LoginUser.LoginUserId;
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
            FetchDataStatus();
        }

        public void FetchDataStatus()
        {
            DataTable dtAssignedSource = dtPayrollSetting;
            DataTable dtOldAssigneSource = dtCompareSetting; // dtCompareSetting;

            var matched = from table1 in dtAssignedSource.AsEnumerable()
                          join table2 in dtOldAssigneSource.AsEnumerable() on table1.Field<string>("Value") equals table2.Field<string>("Value")
                          select table1;

            var missing = from table1 in dtAssignedSource.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }


        /// <summary>
        /// Check inputs and alert and lock updating to DB
        /// </summary>
        /// <returns></returns>
        private bool CheckValidation()
        {
            bool isValid = true;
            ////1. Check FD Reinvestment exists
            //if (HasFDReinvestment())
            //{
            //    isValid = false ;
            //}
            //else if (HasBudgetStatistics())
            //{
            //    isValid = false;
            //}
            //else if (HasBudgetIncomeLedgers())
            //{
            //    isValid = false;
            //}
            //else if (HasGSTVouchers())
            //{
            //    isValid = false; //Alert message alone
            //}
            //else
            //{
            //    isValid = true;
            //}


            return isValid;
        }

        #endregion

    }
}