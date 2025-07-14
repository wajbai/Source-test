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
using Bosco.Model;
using System.IO;
using System.Threading;
using DevExpress.XtraSplashScreen;
using System.Reflection;
using Bosco.Model.Dsync;
using System.ServiceModel;
using System.Net;
using System.Configuration;
using Bosco.Model.UIModel;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmExportDonorData : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmExportDonorData()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        public string projectid
        {
            get
            {
                return GetProjectsIds();
            }
            set
            {
                projectid = value;
            }
        }
        #endregion

        #region Methods
        public bool IsValidInput()
        {
            bool isValid = true;
            if (chklstProjects.CheckedItems.Count == 0)
            {
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_SELECT_ONE_PROJECT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
            }
            else if (deDateTo.DateTime < deDateFrom.DateTime)
            {
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataSynchronization.Export.DSYNC_VALIDATE_DATE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
            }
            return isValid;
        }
        private void GenerateExcel()
        {
            try
            {
                //this.ShowWaitDialog("Exporting Donor Vouchers...");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.ExportDonorData.EXPORT_DONOR_DATA_VOUCHERS_INFO));

            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.ToString();
            }
        }
        public string GetProjectsIds()
        {
            string SelectedProjectId = string.Empty;
            try
            {
                string pid = string.Empty;
                //AcMELog.WriteLog("Get Selected Project Id's");
                AcMELog.WriteLog(this.GetMessage(MessageCatalog.Networking.ExportDonorData.EXPORT_DONOR_DATA_SELECT_PROJECT_INFO));
                foreach (DataRowView drProject in chklstProjects.CheckedItems)
                {
                    pid += drProject[0].ToString();
                    pid += ',';
                }
                SelectedProjectId = pid.TrimEnd(',');
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return SelectedProjectId;
        }
        /// <summary>
        /// Loading the project details
        /// </summary>
        private void LoadProjects()
        {
            using (DonorFrontOfficeSystem donorfrontoffice = new DonorFrontOfficeSystem())
            {
                resultArgs = donorfrontoffice.FetchDonorMappedProjects();
                if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    chklstProjects.DataSource = resultArgs.DataSource.Table;
                    chklstProjects.DisplayMember = "PROJECT";
                    chklstProjects.ValueMember = "PROJECT_ID";
                }
            }

        }
        private void LoadDefaults()
        {
            LoadProjects();
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }
        #endregion
        
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidInput())
                {
                    GenerateExcel();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally
            {
                //AcMELog.WriteLog("-----Exporting Donor Vouchers Ended ----");
                AcMELog.WriteLog(this.GetMessage(MessageCatalog.Networking.ExportDonorData.EXPORT_DONOR_DATA_EXPORT_VOUCHERS_INFO));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                chklstProjects.CheckAll();
            }
            else
            {
                chklstProjects.UnCheckAll();
            }
        }

        private void frmExportDonorData_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }
    }
}