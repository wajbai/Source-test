using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Transaction
{
    public partial class frmRegenerateVoucher : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs result = null;
        public bool IsDateLoaded = false;
        #endregion

        #region Constructor
        public frmRegenerateVoucher()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmRegenerateVoucher_Load(object sender, EventArgs e)
        {
            dtDateTo.DateTime = dtDateFrom.DateTime.AddMonths(1).AddDays(-1);
            LoadProjects();
            SetDefaults();
            this.EnforceReceiptModule(new object[] { chkReceipts }, false);
            if (!chkReceipts.Enabled) { 
                chkPayments.Checked = true;
                chkReceipts.Checked = false;
            }
        }

        private void dtDateTo_Leave(object sender, EventArgs e)
        {
            if (dtDateFrom.DateTime > dtDateTo.DateTime)
            {
                DateTime dateTo = dtDateTo.DateTime;
                dtDateTo.DateTime = dtDateFrom.DateTime;
                dtDateFrom.DateTime = dateTo.Date;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                ResultArgs result = null;
                if (IsValidInput())
                {
                    string TransType = string.Empty;
                    int ProjectId = 0;
                    string ProjectName = string.Empty;

                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REGENERATING_VOUCHER_NUMBER));
                    foreach (DataRowView drProject in chklstProjects.CheckedItems)
                    {
                        ProjectId = this.UtilityMember.NumberSet.ToInteger(drProject[0].ToString());
                        ProjectName = drProject[5].ToString();

                        //If Receipts checked and voucher method automatic
                        //On 15/02/2019, for multi voucher type, check its generation mode for every receipts type
                        //if (chkReceipts.Checked && isVoucherMethodAutomatic(ProjectId, DefaultVoucherTypes.Receipt))
                        if (chkReceipts.Checked)
                        {
                            result = GenerateVoucher(ProjectId, "RC", DefaultVoucherTypes.Receipt);
                            if (!result.Success) { break; }
                        }

                        //If Payments checked and voucher method automatic
                        //On 15/02/2019, for multi voucher type, check its generation mode for every payments type
                        //if (chkPayments.Checked && isVoucherMethodAutomatic(ProjectId, DefaultVoucherTypes.Payment))
                        if (chkPayments.Checked)
                        {
                            result = GenerateVoucher(ProjectId, "PY", DefaultVoucherTypes.Payment);
                            if (!result.Success) { break; }
                        }

                        //If Contra checked and voucher method automatic
                        //On 15/02/2019, for multi voucher type, check its generation mode for every contra type
                        //if (chkContra.Checked && isVoucherMethodAutomatic(ProjectId, DefaultVoucherTypes.Contra))
                        if (chkContra.Checked)
                        {
                            result = GenerateVoucher(ProjectId, "CN", DefaultVoucherTypes.Contra);
                            if (!result.Success) { break; }
                        }

                        //If Journal checked and voucher method automatic
                        //On 15/02/2019, for multi voucher type, check its generation mode for every journal type
                        //if (chkContra.Checked && isVoucherMethodAutomatic(ProjectId, DefaultVoucherTypes.Journal))
                        if (chkJournal.Checked)
                        {
                            result = GenerateVoucher(ProjectId, "JN", DefaultVoucherTypes.Journal);
                            if (!result.Success) { break; }
                        }
                    }
                    this.CloseWaitDialog();
                    if (result != null && result.Success)
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_REGENERATION_SUCCESS));
                        this.Close();
                    }
                    else
                    {
                        this.ShowMessageBox(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Setting the default values in the date fields
        /// </summary>
        private void SetDefaults()
        {
            dtDateFrom.Properties.MinValue = dtDateTo.Properties.MinValue = dtDateFrom.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtDateFrom.Properties.MaxValue = dtDateTo.Properties.MaxValue = dtDateTo.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
        }

        /// <summary>
        /// Loading the project details
        /// </summary>
        private void LoadProjects()
        {
            using (MappingSystem projectSystem = new MappingSystem())
            {
                projectSystem.ProjectClosedDate = dtDateFrom.Text;
                ResultArgs resultArgs = projectSystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    chklstProjects.DataSource = resultArgs.DataSource.Table;
                    chklstProjects.DisplayMember = "PROJECT";
                    chklstProjects.ValueMember = "PROJECT_ID";
                }
            }
        }

        /// <summary>
        /// Finding the reset start date and setting it as Date From and generating the voucher
        /// </summary>
        /// <param name="Project">Project ID</param>
        /// <param name="ReceiptType">Receipt Type</param>
        /// <param name="vType">Voucher Type</param>
        /// <returns></returns>
        private ResultArgs GenerateVoucher(int Project, string VoucherType, DefaultVoucherTypes vType)
        {
            bool CanRegenerateBasedOnDate = chkResetBasedOnVoucherDate.Checked;
            ResultArgs result = null;
            using (VoucherTransactionSystem voucher = new VoucherTransactionSystem())
            {
                using (ProjectSystem projectsystem = new ProjectSystem())
                {
                    result  = projectsystem.FetchVoucherByProjectId(Project, ((int)vType).ToString());
                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtVTypesDefinitions = result.DataSource.Table;
                        foreach (DataRow dr in dtVTypesDefinitions.Rows)
                        {
                            Int32 voucherdefinitionid = this.UtilityMember.NumberSet.ToInteger(dr[projectsystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
                            int VoucherMethod = this.UtilityMember.NumberSet.ToInteger(dr[voucher.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                            //On 15/02/2019, for multi voucher type check for every voucher type generation mode is automatic
                            if (VoucherMethod == (int)TransactionVoucherMethod.Automatic)
                            {
                                voucher.VoucherType = VoucherType;
                                voucher.ProjectId = Project;
                                voucher.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                                voucher.VoucherDefinitionId = voucherdefinitionid;
                                result = voucher.RegenerateVoucher(dtDateFrom.DateTime, dtDateTo.DateTime, CanRegenerateBasedOnDate);
                            }
                            if (!result.Success) break;
                        }
                    }
                    else
                    {
                        result.Message = vType.ToString() + " - Voucher definition is not found";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// To find whether the voucher method is automatic or Manual
        /// </summary>
        /// <param name="Project">Project Id</param>
        /// <param name="TransType">Voucher Type</param>
        /// <returns></returns>
        private bool isVoucherMethodAutomatic(int Project, DefaultVoucherTypes TransType)
        {
            DataTable dtVoucher;
            bool isValid = true;

            using (VoucherTransactionSystem voucher = new VoucherTransactionSystem())
            {
                result = voucher.FetchVoucherMethod(Project, (int)TransType);

                if (result.Success && result.RowsAffected > 0)
                {
                    int VoucherMethod = 0;
                    dtVoucher = result.DataSource.Table;
                    VoucherMethod = this.UtilityMember.NumberSet.ToInteger(dtVoucher.Rows[0][voucher.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                    if (VoucherMethod == (int)TransactionVoucherMethod.Manual)
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Validating the User Input
        /// </summary>
        /// <returns></returns>
        private bool IsValidInput()
        {
            bool isValid = true;

            if (!chkReceipts.Checked && !chkPayments.Checked && !chkContra.Checked && !chkJournal.Checked)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_TYPE_NO_SELECTION));
                isValid = false;
            }
            else if (chklstProjects.CheckedItems.Count == 0)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_REGENERATE_SELECT_PROJECT));
                isValid = false;
            }
            else if (!isValidVoucherMethod(DefaultVoucherTypes.Receipt, chkReceipts.Checked))
            {
                isValid = false;
            }
            else if (!isValidVoucherMethod(DefaultVoucherTypes.Payment, chkPayments.Checked))
            {
                isValid = false;
            }
            else if (!isValidVoucherMethod(DefaultVoucherTypes.Contra, chkContra.Checked))
            {
                isValid = false;
            }
            else if (!isValidVoucherMethod(DefaultVoucherTypes.Journal, chkJournal.Checked))
            {
                isValid = false;
            }

            //As on 29/10/2021, To get confirmation for regeneate voucher -----------------------------------------------------------------------
            if (isValid && chkResetBasedOnVoucherDate.Checked)
            {
                if (this.ShowConfirmationMessage("Are you sure to Regenerate Voucher Numbers based on Voucher Date and Voucher Entry Order",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    chkResetBasedOnVoucherDate.Checked = false;
                    isValid = false;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------------------
            return isValid;
        }

        private bool isValidVoucherMethod(DefaultVoucherTypes TransType, bool isChecked)
        {
            int Project = 0;
            string ProjectName = string.Empty;
            bool isValid = true;
            string TempProject = string.Empty;

            foreach (DataRowView drProject in chklstProjects.CheckedItems)
            {
                Project = this.UtilityMember.NumberSet.ToInteger(drProject[0].ToString());
                ProjectName = drProject[5].ToString();

                if (isChecked && !isVoucherMethodAutomatic(Project, TransType))
                {
                    TempProject += Environment.NewLine + ProjectName + ",";
                    isValid = false;
                }
            }
            if (!isValid)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REGENERATION_FAILS) + " '" + TransType.ToString() + "' " + this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_METHOD_MANUAL) + " " + TempProject.TrimEnd(','));
            }

            return isValid;
        }

        /// <summary>
        /// Clear all the controls after save
        /// </summary>
        private void ClearAll()
        {
            chklstProjects.UnCheckAll();
            chkReceipts.Checked = true;
            chkPayments.Checked = chkContra.Checked = chkJournal.Checked = false;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                dtDateFrom.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
        #endregion

        private void dtDateFrom_Leave(object sender, EventArgs e)
        {
            if (IsDateLoaded)
            {
                dtDateTo.DateTime = dtDateFrom.DateTime.AddMonths(1).AddDays(-1);
                IsDateLoaded = true;
            }
            if (dtDateFrom.DateTime > dtDateTo.DateTime)
            {
                DateTime dateTo = dtDateTo.DateTime;
                dtDateTo.DateTime = dtDateFrom.DateTime;
                dtDateFrom.DateTime = dateTo.Date;
            }
        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckAll.Checked)
            {
                chklstProjects.CheckAll();
            }
            else
            {
                chklstProjects.UnCheckAll();
            }
        }

        private void dtDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            LoadProjects();
            //--------------------------------------
        }
    }
}