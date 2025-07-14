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
using System.ServiceModel;

namespace ACPP.Modules.Dsync
{
    public partial class frmAmendmentNotes : frmFinanceBaseAdd
    {

        #region Declaration

        #endregion

        #region Constructor
        public frmAmendmentNotes()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void frmAmendmentNotes_Load(object sender, EventArgs e)
        {
            GetAmendments();
        }
        #endregion

        #region Methods
        public void GetAmendments()
        {
            try
            {
                DataSyncService.DataSynchronizerClient cliAmendment = new DataSyncService.DataSynchronizerClient();
                DataTable dtAmendments = cliAmendment.GetVoucherAmendments(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                if (dtAmendments != null && dtAmendments.Rows.Count > 0)
                {
                    gcAmendmentNotes.DataSource = dtAmendments;
                }
                else
                {
                    gcAmendmentNotes.DataSource = null;
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                this.ShowMessageBox(ex.Detail.Message);
                AcMELog.WriteLog(ex.Detail.Message);
            }
        }
        #endregion

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAmendmentNotes.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAmendmentNotes, gcolParticulars);
            }
        }

        private void gvAmendmentNotes_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvAmendmentNotes.RowCount.ToString();
        }

        private void frmAmendmentNotes_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void ucAmendments_RefreshClicked(object sender, EventArgs e)
        {
            try
            {
                //this.ShowWaitDialog("Connecting to Portal..");
                this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.AMENDMENTS_NOTES_CONNECTING_PORTAL));
                if (CommonMethod.CheckForInternetConnection())
                {
                    try
                    {
                        DataSyncService.DataSynchronizerClient cliAmendment = new DataSyncService.DataSynchronizerClient();
                        DataTable dtAmendments = cliAmendment.GetVoucherAmendments(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                        if (dtAmendments != null && dtAmendments.Rows.Count > 0)
                        {
                            gcAmendmentNotes.DataSource = dtAmendments;
                        }
                        else
                        {
                            gcAmendmentNotes.DataSource = null;
                        }
                    }
                    catch (FaultException<DataSyncService.AcMeServiceException> ex)
                    {
                        this.ShowMessageBox(ex.Detail.Message);
                        AcMELog.WriteLog(ex.Detail.Message);
                    }
                }
                else
                {
                    CloseWaitDialog();
                    //this.ShowMessageBox("Internet Connection is not  available.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.AMENDMENTS_NOTES_INTERNET_CONNECTION_AVAIL));
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.ToString());
            }
        }

        private void ucAmendments_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}