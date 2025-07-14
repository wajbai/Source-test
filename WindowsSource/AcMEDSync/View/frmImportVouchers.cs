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
using AcMEDSync.Model;

namespace AcMEDSync.View
{
    public partial class frmImportVouchers : DevExpress.XtraEditors.XtraForm
    {
        #region VariableDeclaration
        public string FileName = string.Empty;
        DsyncSystemBase acMEDSyncSQL = new DsyncSystemBase();
        #endregion
        
        public frmImportVouchers()
        {
            InitializeComponent();
        }

        private void btnImportVoucher_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(textEdit1.Text))
            {
                AcMEDataSynLog.WriteErrorLog("Data Synchronization is Started......");
                if (XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.ImportVoucher.CONFIRMATION_BRANCH_HEADOFFICE_VOUCHERS), "AcMEERP", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    AcMEDSync.AcMEDataSyn objDataSyn = new AcMEDataSyn();
                    ResultArgs resultArgs = objDataSyn.SynchronizeVouchers(FileName);
                    if (!resultArgs.Success)
                    {
                        XtraMessageBox.Show(resultArgs.Message, "AcMEERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AcMEDataSynLog.WriteErrorLog(resultArgs.Message);
                    }
                    else
                    {
                        XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.DATA_SYNCHRONISATION_SUCCESS), "AcMEERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AcMEDataSynLog.WriteErrorLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.DATA_SYNCHRONISATION_SUCCESSLOG));
                    }
                }
            }
            else
            {
                XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.DATA_SYN_FILE_SELECTION), "AcMEERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AcMEDSync.AcMEDataSynLog.WriteErrorLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.SELECT_XML_IMPORT));
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                AcMEDataSynLog.ClearErrorLog();
                AcMEDataSynLog.WriteErrorLog(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Common.WELCOME_SYNCHRONISATION));
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = opendialog.FileName;
                    textEdit1.Text = FileName;
                    //if (!string.IsNullOrEmpty(opendialog.FileName))
                    //{
                    //    btnImport.Enabled = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                AcMEDataSynLog.WriteErrorLog(ex.Message, ex.Source.ToString(), ex.TargetSite.ToString(), ex.StackTrace.ToString());
            }
            finally { }
        }
    }
}