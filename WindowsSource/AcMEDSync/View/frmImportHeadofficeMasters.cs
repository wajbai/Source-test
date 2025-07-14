using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Xml;
using Bosco.Utility;
using System.ServiceModel;
using AcMEDSync.Model;

namespace AcMEDSync.View
{
    public partial class frmImportHeadofficeMasters : DevExpress.XtraEditors.XtraForm
    {
        #region Decelaration
        ResultArgs resultargs = new ResultArgs();
        DsyncSystemBase acMEDSyncSQL = new DsyncSystemBase();
        #endregion

        #region Properties
        private string FileName { get; set; }
        #endregion

        #region Constructor
        public frmImportHeadofficeMasters()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmImportHeadofficeMasters_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void rgSynMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgSynMode.SelectedIndex !=(int)Mode.Offline)
            {
                lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciElipse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                pgSynchronizationProgress.Properties.Minimum = 0;
                btnImport.Enabled = true;
                this.Height = 220;
            }
            else
            {
                lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciElipse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtPath.Text = string.Empty;
                btnImport.Enabled = (!string.IsNullOrEmpty(txtPath.Text));
                this.Height = 145;
            }
        }

        /// <summary>
        /// Read XML file from the user , this is done when the mode is on offline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadXML_Click(object sender, EventArgs e)
        {
            try
            {
                AcMEDataSynLog.WriteLog("Welcome to AcME ERP Data Sync...");
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = opendialog.FileName;
                    txtPath.Text = FileName;
                    if (!string.IsNullOrEmpty(opendialog.FileName))
                    {
                        btnImport.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// Import Head office data to branch office 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (rgSynMode.SelectedIndex ==(int)Mode.Offline) 
            {
                if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(txtPath.Text))
                {
                    EnableProgressBar();
                    if (XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Import.CONFIRMATION_IMPORT_MASTERS),acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.ACMEERP), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        AcMEDSync.AcMEDataSyn objDataSyn = new AcMEDataSyn();
                        resultargs =objDataSyn.ImportMasters(FileName);
                        if (!resultargs.Success)
                        {
                            XtraMessageBox.Show(resultargs.Message, MessageCatalog.DataSynchronization.Common.ACMEERP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AcMEDSync.AcMEDataSynLog.WriteLog(resultargs.Message);
                        }
                        else
                        {
                            XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.DATA_SYNCHRONISATION_SUCCESS), MessageCatalog.DataSynchronization.Common.ACMEERP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AcMEDSync.AcMEDataSynLog.WriteLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.DATA_SYNCHRONISATION_SUCCESSLOG));
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.DATA_SYN_FILE_SELECTION), acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.ACMEERP), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AcMEDSync.AcMEDataSynLog.WriteLog(MessageCatalog.DataSynchronization.Common.SELECT_XML_IMPORT);
                }
            }
            else
            {
                AcMEDataSynLog.ClearLog();
                DataSet dsReadXML = null;
                AcMEDSync.AcMEDataSynLog.WriteLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Import.DATA_SYN_ONLINE));
                try
                {
                    //Call wait dialogue (Checking for internet connection
                    DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                    dsReadXML = dataClient.GetMasters("sdbinm", "sdbinmdbcylg");
                    //Close Wait dialogue
                }
                catch (FaultException ex)
                {
                    AcMEDSync.AcMEDataSynLog.WriteLog(ex.Message);
                }
                catch (CommunicationException ex)
                {
                    AcMEDSync.AcMEDataSynLog.WriteLog(ex.Message);
                    XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Import.CHECK_INTERNET_CONNECTION), acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.ACMEERP), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                using (AcMEDSync.Model.ImportMasterSystem importSystem = new Model.ImportMasterSystem())
                {
                    resultargs=importSystem.ImportMasters(dsReadXML);
                    if (resultargs !=null && resultargs.Success)
                    {
                        XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Import.MASTER_SUCCESS), acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.ACMEERP), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AcMEDSync.AcMEDataSynLog.WriteLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Import.MASTER_SUCCESS));
                    }
                    else if (resultargs !=null)
                    {
                        XtraMessageBox.Show(resultargs.Message, acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.ACMEERP), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AcMEDSync.AcMEDataSynLog.WriteLog(resultargs.Message);
                    }
                }
                AcMEDSync.AcMEDataSynLog.WriteLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Import.MASTER_SUCCESS));
            }
        }
        #endregion

        #region Methods
        private void SetDefaults()
        {
            lcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.Height = 145;
            if (rgSynMode.SelectedIndex == (int)Mode.Offline)
            {
                btnImport.Enabled = false;
            }
        }

        private void ShowProgress()
        {
            try
            {
                pgSynchronizationProgress.Properties.Minimum = 1;
                pgSynchronizationProgress.Properties.Maximum = 100000;
                for (int i = 0; i < 100000; i++)
                {
                    pgSynchronizationProgress.PerformStep();
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteLog(ex.Message);
            }
            finally { }
        }

        private void EnableProgressBar()
        {
            lcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (rgSynMode.SelectedIndex == (int)Mode.Offline)
            {
                this.Height = 225;
            }
            else
            {
                this.Height = 190;
            }
        }

        #endregion

    }
}