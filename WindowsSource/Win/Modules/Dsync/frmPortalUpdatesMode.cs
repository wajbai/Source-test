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
using Bosco.Utility.ConfigSetting;

using System.ServiceModel;

namespace ACPP.Modules.Dsync
{
    public partial class frmPortalUpdatesMode : frmFinanceBaseAdd
    {
        #region Declaration
        PortalUpdates portalUpdates { get; set; }
        #endregion

        #region Constructor
        public frmPortalUpdatesMode()
        {
            InitializeComponent();
        }

        public frmPortalUpdatesMode(PortalUpdates updates)
            : this()
        {
            portalUpdates = updates;
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void frmPortalUpdatesMode_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Methods
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
            if (rgUpdatesMode.SelectedIndex == 0)
            {
                //SettingProperty.VoucherExportMode = ExportMode.Offline;
                frmPortalUpdates portalupdates = new frmPortalUpdates(portalUpdates);
                portalupdates.ShowDialog();
            }
            else
            {
                //this.ShowWaitDialog("Connecting to Portal..");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.CONNECTING_TO_PORTAL));
                if (this.CheckForInternetConnection())
                {
                    // SettingProperty.VoucherExportMode = ExportMode.Online;
                    bool IsExists = false;
                    CloseWaitDialog();
                    if (!string.IsNullOrEmpty(this.AppSetting.BranchOfficeCode) && !string.IsNullOrEmpty(this.AppSetting.HeadofficeCode))
                    {
                        try
                        {
                            DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                            IsExists = dataClient.IsBranchExists(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                        }
                        catch (FaultException<DataSyncService.AcMeServiceException> ex)
                        {
                            IsExists = false;
                            this.ShowMessageBox(ex.Detail.Message);
                        }
                    }
                    else
                    {
                        IsExists = false;
                        //this.ShowMessageBox("Head Office Code and Branch Office Code not found in Branch Office." + Environment.NewLine + "Kindly download your Branch License from Head Office using your credentials");
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.HO_CODE_AND_BO_CODE_NOT_FOUND_IN_BO) + Environment.NewLine + this.GetMessage(MessageCatalog.Master.FinanceDataSynch.DOWNLOAD_BO_LICENSE_FROM_HO));
                    }

                    if (IsExists)
                    {
                        frmPortalUpdates frmUpdates = new frmPortalUpdates(portalUpdates);
                        frmUpdates.Owner = this;
                        frmUpdates.ShowDialog();
                    }
                    else
                    {
                        frmPortalLogin frmportalLogin = new frmPortalLogin();
                        frmportalLogin.Owner = this;
                        frmportalLogin.ShowDialog();
                    }
                }
                else
                {
                    CloseWaitDialog();
                    //this.ShowMessageBox("Unable to reach Portal. Please check your internet connection.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.CHECK_INTERNET_CONNECTION));
                }
            }

        }

    }
}