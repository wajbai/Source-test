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
using Bosco.Model.Dsync;

namespace ACPP.Modules.Dsync
{
    public partial class frmImportHeadofficeMasters : frmFinanceBaseAdd
    {
        #region Decelaration
        ResultArgs resultargs = new ResultArgs();
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
            this.Height = 110;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                AcMELog.WriteLog("Welcome to Acme.erp Data Sync...");
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "XML Files (.xml)|*.xml";
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
                AcMELog.WriteLog(ex.Message);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgSynMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgSynMode.SelectedIndex != (int)Mode.Offline)
            {
                lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciElipse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // lciEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btnImport.Enabled = true;
                this.Height = 220;
            }
            else
            {
                lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciElipse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                // lciEmptySpace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtPath.Text = string.Empty;
                btnImport.Enabled = (!string.IsNullOrEmpty(txtPath.Text));
                this.Height = 145;
            }
        }

        private bool isValidInput()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(FileName))
            {
                //XtraMessageBox.Show("File Name is Empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_HO_MASTERS_FILE_NAME_INFO));
                isValid = false;
            }
            else if (string.IsNullOrEmpty(txtPath.Text))
            {
                //XtraMessageBox.Show("Path is Empty.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_HO_MASTERS_FILE_PATH_INFO));
                isValid = false;
            }
            else if (!File.Exists(FileName))
            {
                //XtraMessageBox.Show("File does not exists in the path.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_HO_MASTERS_FILE_NOT_EXITS_INFO));
                isValid = false;
            }

            return isValid;
        }
        /// <summary>
        /// Import Head office data to branch office, 
        /// IF selected option is Off line
        /// online 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            DataSet dsMasters = null;
            try
            {
                if (isValidInput())
                {
                    using (ImportMasterSystem objMasters = new ImportMasterSystem())
                    {
                        if (rgSynMode.SelectedIndex == (int)Mode.Offline)
                        {
                            resultargs = XMLConverter.ConvertXMLToDataSetWithResultArgs(FileName);
                            if (resultargs.Success)
                            {
                                dsMasters = resultargs.DataSource.TableSet;
                            }
                        }
                        else
                        {
                            DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                            dsMasters = dataClient.GetMasterDetails(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                        }

                        if (dsMasters != null && dsMasters.Tables.Count > 0 && resultargs != null && resultargs.Success)
                        {
                            frmMapHeadOfficeLedgers frmMapMismatchedLedger = new frmMapHeadOfficeLedgers();
                            frmMapHeadOfficeProjects frmMappedheadOfficeprojects = new frmMapHeadOfficeProjects();
                            this.ShowWaitDialog();
                            resultargs = objMasters.ImportMasters(dsMasters, frmMapMismatchedLedger, frmMappedheadOfficeprojects);
                            this.CloseWaitDialog();
                        }
                    }
                }
            }
            catch (FaultException ex)
            {
                resultargs.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (CommunicationException ex)
            {
                resultargs.Message = ex.Message;
                AcMELog.WriteLog(ex.Message);
            }
            catch (Exception ex)
            {
                resultargs.Message = ex.ToString();
                AcMELog.WriteLog(ex.ToString());
            }

            if (resultargs.Success)
            {
                //XtraMessageBox.Show("Head Office Masters imported Successfully.", MessageCatalog.Common.COMMON_MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.IMPORT_HO_MASTERS_SUCCESS_INFO));
                this.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(resultargs.Message))
                {
                    XtraMessageBox.Show(resultargs.Message, MessageCatalog.Common.COMMON_MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region Methods
        private void SetDefaults()
        {
            this.Height = 145;
            if (rgSynMode.SelectedIndex == (int)Mode.Offline)
            {
                btnImport.Enabled = false;
            }
        }

        #endregion
    }
}