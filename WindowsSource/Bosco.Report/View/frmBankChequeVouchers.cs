using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Repository;
using System.Collections;

using Bosco.Report.SQL;
using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraBars.Docking;
using Bosco.Report.View;
using System.Globalization;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;


namespace Bosco.Report.View
{
    public partial class frmBankChequeVouchers : Bosco.Utility.Base.frmBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        SettingProperty setttings = new SettingProperty();
        private int UserRoleId { get; set; }
        private string reportId = string.Empty;
        private DataTable dtBankVouchers { get; set; }
        private string VoucherIds = "";
        private DataTable dtVouchers { get; set; }
        private DataTable dtProjectDetails { get; set; }
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        #endregion

        #region Constructor
        public frmBankChequeVouchers()
        {
            InitializeComponent();
        }

        public frmBankChequeVouchers(string ReportId)
            : this()
        {
            this.reportId = ReportId;

        }

        #endregion

        private int voucherid = 0;
        public int VoucherId
        {
            get
            {
                voucherid = gvBankVouchers.GetFocusedRowCellValue(colVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBankVouchers.GetFocusedRowCellValue(colVoucherId).ToString()) : 0;
                return voucherid;
            }
        }

        private int bankid = 0;
        public int BankId
        {
            get
            {
                bankid = gvBankVouchers.GetFocusedRowCellValue(colBankId) != null ? this.UtilityMember.NumberSet.ToInteger(gvBankVouchers.GetFocusedRowCellValue(colBankId).ToString()) : 0;
                return bankid;
            }
        }

        #region Events

        private void frmFinancialRecords_Load(object sender, EventArgs e)
        {
            SetDefaults();
            FetchProjects();

            if (ReportProperty.Current.ChequeBankVoucherId > 0)
            {
                glkpProject.EditValue = ReportProperty.Current.CashBankProjectId;
                deDateFrom.DateTime = ReportProperty.Current.CashBankVoucherDateFrom;
                deDateTo.DateTime = ReportProperty.Current.CashBankVoucherDateTo;
                glkpBankLedger.EditValue = ReportProperty.Current.ChequeBankId;
            }
        }

        private void rchkSelectProject_CheckedChanged(object sender, EventArgs e)
        {
            gvBankVouchers.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

       
        private void dteMonths_EditValueChanged(object sender, EventArgs e)
        {
            BindBankVoucherDetails();
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            FetchBankAccounts();
            BindBankVoucherDetails();
        }
        #endregion

        #region Methods
        private void FetchProjects()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, setttings.RoleId);
                    }

                    dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, deDateFrom.Text);

                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            dtProjectDetails = resultArgs.DataSource.Table;
                            ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, "PROJECT_NAME", "PROJECT_ID");
                            glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        private void FetchBankAccounts()
        {
            try
            {
                glkpBankLedger.Properties.DataSource = null;
                Int32 projectId = glkpProject.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankByProject))
                {
                    dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, projectId);
                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            ReportProperty.Current.ComboSet.BindGridLookUpCombo(glkpBankLedger, resultArgs.DataSource.Table, "BANK", "LEDGER_ID");
                            glkpBankLedger.EditValue = glkpBankLedger.Properties.GetKeyValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
        }

        public ResultArgs BindBankVoucherDetails()
        {
            ResultArgs resultArgs = new ResultArgs();
            Int32 projectId = glkpProject.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
            Int32 bankledgerid = glkpBankLedger.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpBankLedger.EditValue.ToString()) : 0;
            string DateFrom = UtilityMember.DateSet.ToDate(deDateFrom.DateTime.ToShortDateString());
            string DateTo = UtilityMember.DateSet.ToDate(deDateTo.DateTime.ToShortDateString());
            if (projectId > 0 && bankledgerid > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankVoucher))
                {
                    dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, projectId);
                    dataManager.Parameters.Add(this.appSchema.Ledger.LEDGER_IDColumn, bankledgerid);
                    dataManager.Parameters.Add(this.appSchema.FDRegisters.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.appSchema.FDRegisters.DATE_TOColumn, DateTo);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                }
                if (resultArgs.Success)
                {
                    DataTable dtBankVoucher = resultArgs.DataSource.Table;
                    gcBankVouchers.DataSource = dtBankVoucher;
                }
                else
                {
                    MessageRender.ShowMessage(resultArgs.Message);
                }
            }
            return resultArgs;
        }

        private void SetDefaults()
        {
            this.Text = "Bank Voucher - Cheque Printing";
            deDateFrom.DateTime = ReportProperty.Current.DateSet.ToDate(setttings.YearFrom, false);
            deDateTo.Properties.MaxValue = deDateTo.DateTime = ReportProperty.Current.DateSet.ToDate(setttings.YearTo, false);
            UserRoleId = setttings.RoleId;
        }

        #endregion

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            FetchProjects();
            BindBankVoucherDetails();
        }

        private void deDateTo_EditValueChanged(object sender, EventArgs e)
        {
            BindBankVoucherDetails();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvBankVouchers.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
        }

        private void gvProject_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordsCount.Text = "# " + gvBankVouchers.RowCount.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (deDateTo.DateTime >= deDateFrom.DateTime)
            {
                if (gcBankVouchers.DataSource != null && gvBankVouchers.RowCount != 0)
                {
                    if (VoucherId > 0)
                    {
                        //Check Bank setting available
                        bool ChequePrintingSetting = false;
                        using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchChequePrintingSetting))
                        {
                            dataManager.Parameters.Add("BANK_ID", BankId, DataType.Int32);
                            resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                            {
                                ChequePrintingSetting = true;
                            }
                        }
                        if (ChequePrintingSetting)
                        {
                            ReportProperty.Current.CashBankProjectId = glkpProject.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                            ReportProperty.Current.CashBankVoucherDateFrom = deDateFrom.DateTime;
                            ReportProperty.Current.CashBankVoucherDateTo = deDateTo.DateTime;
                            ReportProperty.Current.ChequeBankVoucherId = VoucherId;
                            ReportProperty.Current.ChequeBankId = glkpBankLedger.EditValue != null ? ReportProperty.Current.NumberSet.ToInteger(glkpBankLedger.EditValue.ToString()) : 0; ; 
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageRender.ShowMessage("Cheque Printing Setting is not available");
                        }
                    }
                }
                else
                {
                    MessageRender.ShowMessage("Select Bank Voucher");
                }
            }
            else
            {
                MessageRender.ShowMessage("Date To is Less than Date From");
            }
        }

        private void glkpBankLedger_EditValueChanged(object sender, EventArgs e)
        {
            BindBankVoucherDetails();
        }
    }
}