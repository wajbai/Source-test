using System;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using AcMEDSync.Model;
using DevExpress.XtraLayout.Utils;

using Bosco.Model.Dsync;
using Bosco.Utility;
using Bosco.Utility.Base;
using Bosco.Model.UIModel;

namespace SUPPORT
{
    public partial class frmImportSplitedProject : frmBase
    {
        #region Variables
        ResultArgs resultargs = new ResultArgs();
        SettingProperty setting = new SettingProperty();
        #endregion

        #region Properties
        private string FileName { get; set; }

        private bool IsLicenceDetailsValid
        {
            get
            {
                bool IsValid = true;
                using (ImportVoucherSystem importVoucher = new ImportVoucherSystem())
                {
                    layoutCapHO.Visibility = LayoutVisibility.Always;
                    layoutCapBO.Visibility = LayoutVisibility.Always;
                    layoutHO.Visibility = LayoutVisibility.Always;
                    layoutBO.Visibility = LayoutVisibility.Always;

                    importVoucher.InitializeLicenceDetails(FileName);
                    lblBOCode.Text = CommonMethod.BranchOfficeCode.ToUpper();
                    lblHOCode.Text = CommonMethod.HeadOfficeCode.ToUpper();
                    if (!lblBOCode.Text.ToLower().Equals(SettingProperty.branachOfficeCode.ToLower()))
                    {
                        if (ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.BRANCH_CODE_MATCH_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.No))
                            IsValid = false;
                        //ShowMessageBox(GetMessage(MessageCatalog.DataSynchronization.Import.BRANCH_CODE_MATCH_ERROR));
                    }
                    else if (!lblHOCode.Text.ToLower().Equals(SettingProperty.headofficecode.ToLower()))
                    {
                        if (ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.HEAD_OFFICE_CODE_MATCH_ERROR), MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.No))
                            IsValid = false;
                        // ShowMessageBox(GetMessage(MessageCatalog.DataSynchronization.Import.HEAD_OFFICE_CODE_MATCH_ERROR));
                    }
                }
                return IsValid;
            }
        }
        #endregion

        #region Constructor
        public frmImportSplitedProject()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmImportSplitedProject_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            ApplyAccPeriod();
        }

        private void btnLoadXMLFile_Click(object sender, EventArgs e)
        {
            try
            {
                AcMELog.WriteLog("Welcome to Acme.erp Import Splited Project...");
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "XML Files (.xml)|*.xml";
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = opendialog.FileName;
                    txtPath.Text = FileName;
                    btnImport.Enabled = IsLicenceDetailsValid;
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
        }

        private void ApplyAccPeriod()
        {
            try
            {
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    ResultArgs resultArgs = accountingSystem.FetchActiveTransactionPeriod();
                    if (resultArgs.Success)
                    {
                        this.AppSetting.AccPeriodInfo = resultArgs.DataSource.Table.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (IsInputValid())
            {
                if (chkCanOverride.Checked ? ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.OVERWRITE_CONFORM), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning).Equals(DialogResult.OK) :
                    ShowConfirmationMessage(GetMessage(MessageCatalog.DataSynchronization.Import.APPEND_CONFORM), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning).Equals(DialogResult.OK))
                {
                    ShowWaitDialog();
                    resultargs = ImportProject();
                    this.CloseWaitDialog();
                    if (resultargs != null && resultargs.Success)
                    {
                        ShowMessageBox(GetMessage(MessageCatalog.DataSynchronization.Import.IMPORT_SUCCESSFULLY));
                    }
                    else if (!string.IsNullOrEmpty(resultargs.Message)) ShowMessageBox(resultargs.Message);
                    this.Close();
                }
            }
        }

        private ResultArgs ImportProject()
        {
            using (ImportVoucherSystem importVoucher = new ImportVoucherSystem())
            {
                importVoucher.CanOverride = chkCanOverride.Checked;
                importVoucher.MergeProject = glkpMergeProject.EditValue != null ? glkpMergeProject.Text : string.Empty;
                importVoucher.MergerProjectId = glkpMergeProject.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpMergeProject.EditValue.ToString()) : 0;
                importVoucher.ImportHeadOfficeBranchData = true;
                resultargs = importVoucher.ImportVouchers(FileName);
                if (resultargs.Success)
                {
                    resultargs = RefereshBalance();
                }
            }
            return resultargs;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private bool IsInputValid()
        {
            bool IsValid = true;
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                ShowMessageBox(GetMessage(MessageCatalog.DataSynchronization.Import.FILE_NAME_EMPTY));
                IsValid = false;
            }
            return (IsValid);
        }

        private ResultArgs RefereshBalance()
        {
            using (BalanceSystem RefereshBalance = new BalanceSystem())
            {
                RefereshBalance.VoucherDate = setting.BookBeginFrom;
                ResultArgs result = RefereshBalance.UpdateBulkTransBalance();
            }
            return resultargs;
        }

        public void LoadDefaults()
        {
            using (ExportVoucherSystem vouchersystem = new ExportVoucherSystem())
            {
                ResultArgs resultArgs = vouchersystem.FetchProjectsLookup();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpMergeProject, resultArgs.DataSource.Table, vouchersystem.AppSchema.Project.PROJECTColumn.ColumnName, vouchersystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                }
            }
        }
        #endregion
    }
}