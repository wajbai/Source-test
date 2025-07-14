/************************************************************************************************************************
 *                                              Form Name   : frmTemplates.cs
 *                                              Purpose     : To download templates for all the Modules
 *                                              Author      : Carmel Raj M
 *                                              Created On  : August-21-2015
 ************************************************************************************************************************/
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using System.Windows.Forms;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmTemplates : frmFinanceBaseAdd
    {
        #region Variables
        List<string> Module;
        List<dynamic> ModuleList;
        #endregion

        #region Constructor
        public frmTemplates()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmTemplates_Load(object sender, EventArgs e)
        {
            ShowWaitDialog();
            LoadModules();
            CloseWaitDialog();
        }

        private void grdlModuleName_EditValueChanged(object sender, EventArgs e)
        {
            if (ModuleList != null)
            {
                if (grdlModuleName.Text.Equals("<--All-->"))
                {
                    var SelectedModule = from m in ModuleList
                                         select m;
                    gcTemplates.DataSource = SelectedModule.ToList();
                }
                else
                {
                    using (AcMEERPFTP Load = new AcMEERPFTP())
                    {
                        gcTemplates.DataSource = Load.ApplyFilter(ModuleList, grdlModuleName.Text);

                    }
                }
            }
            else gcTemplates.DataSource = null;
        }

        private void rDownload_Click(object sender, EventArgs e)
        {
            //if (ShowConfirmationMessage("Are you sure to download?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question).Equals(DialogResult.OK))
            if (ShowConfirmationMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.CONFIRMATION_DELETE), MessageBoxButtons.OKCancel, MessageBoxIcon.Question).Equals(DialogResult.OK))
            {
                string SourcePath = gvTemplates.GetFocusedRowCellValue(colFilePath).ToString();
                DownloadFile(SourcePath);
            }

        }
        #endregion

        #region Methods
        private void LoadModules()
        {
            try
            {
                LoadModuleList();
                if (CheckForInternetConnection())
                {
                    using (AcMEERPFTP LoadModules = new AcMEERPFTP())
                    {
                        ModuleList = LoadModules.DownloadTemplates(Module.SkipWhile(x => x == "<--All-->").ToList());
                        gcTemplates.DataSource = ModuleList;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }



        }

        private void DownloadFile(string RemotePath)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|.xlsx";
                saveDialog.Title = "DownLoad Templates";
                saveDialog.Title = this.GetMessage(MessageCatalog.Master.DataUtilityForms.DOWNLOAD_TEMPLATE);
                saveDialog.FileName = gvTemplates.GetFocusedRowCellValue(colFileName).ToString();
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (AcMEERPFTP downloadFile = new AcMEERPFTP())
                    {
                        ShowWaitDialog();
                        ResultArgs resulArg = downloadFile.download(RemotePath, saveDialog.FileName);
                        CloseWaitDialog();
                        if (resulArg != null && resulArg.Success)
                        {
                            //ShowSuccessMessage("Successfully downloaded");
                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DOWNLOAD_SUCCESS));
                        }
                        else
                        {
                            ShowMessageBox(resulArg.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void LoadModuleList()
        {
            Module = new List<string>();
            Module.Add("<--All-->");
            Module.Add(Moudule.Finance.ToString());
            Module.Add(Moudule.TDS.ToString());
            if (SettingProperty.EnableAsset)
                Module.Add(Moudule.FixedAsset.ToString());
            if (SettingProperty.EnableStock)
                Module.Add(Moudule.Stock.ToString());
            if (SettingProperty.EnablePayroll)
                Module.Add(Moudule.Payroll.ToString());
            grdlModuleName.Properties.DataSource = Module;
            grdlModuleName.EditValue = grdlModuleName.Properties.GetKeyValue(0);
        }
        #endregion
    }
}