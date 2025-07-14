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
using Bosco.Model.TallyMigration;
using Bosco.Model.Dsync;
using Bosco.Utility.ConfigSetting;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmMigrationBOSCOPAC : frmFinanceBaseAdd
    {

        #region Properties
        private string BasePath
        {
            get
            {
                return txtBasePath.Text;
            }
        }

        private Int32 BOSCOPACHouse
        {
            get
            {
                return glkpBOSCOPACBranch.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpBOSCOPACBranch.EditValue.ToString()) : 0;
            }
        }

        private string BOSCOPACSelectedActivitiesId
        {
            get
            {
                string selectedactiviycode = string.Empty;
                foreach (int index in gvBOSCOPACActivity.GetSelectedRows())
                {
                    DataRow dr =  gvBOSCOPACActivity.GetDataRow(index) as DataRow;
                    selectedactiviycode +=  dr["tcode"].ToString().Trim() + ",";
                }

                return selectedactiviycode.TrimEnd(',');
            }
        }

        #endregion

        public frmMigrationBOSCOPAC()
        {
            InitializeComponent();
            lclblMessageInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        
        private void frmMigrationBOSCOPAC_Load(object sender, EventArgs e)
        {
            lblAcYearValue.Text = "01/04/2017 to 31/03/2018";
            //txtBasePath.Text = @"D:\BOSCOPAC\";
            LoadBOSCOPACData();
            this.Height = layoutControl1.Height + 10;
            this.CenterToScreen();
        }

        private void btnLoadXMLFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog opendialog = new FolderBrowserDialog();
            opendialog.SelectedPath = txtBasePath.Text;
            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                txtBasePath.Text = opendialog.SelectedPath;
                LoadBOSCOPACData();
            }
        }

        private void glkpBOSCOPACProject_Properties_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            if (BOSCOPACHouse > 0)
            {
                int[] selectedrows = gvBOSCOPACActivity.GetSelectedRows();
                e.DisplayText = "(" + selectedrows.Length.ToString() + ") project(s) are selected";
            }
        }

        private void BtnMigrate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(BasePath) && BOSCOPACHouse > 0 &&
                    gvBOSCOPACActivity.GetSelectedRows().Length > 0)
                {
                    Migrate();
                }
                else
                {
                    this.UseWaitCursor = false;
                    bool isvalidpath = false;
                    using (BOSCOPACMigrationSystem boscopacsystem = new BOSCOPACMigrationSystem(BasePath))
                    {
                        ResultArgs resultarg = boscopacsystem.CheckVFPOLEDBDriverInstalled();
                        isvalidpath = resultarg.Success;
                        if (resultarg.Success)
                        {
                            if (string.IsNullOrEmpty(BasePath))
                            {
                                MessageRender.ShowMessage("Select BOSCOPAC Path");
                                txtBasePath.Focus();
                            }
                            else if (BOSCOPACHouse <= 0)
                            {
                                MessageRender.ShowMessage("Select BOSCOPAC House");
                                glkpBOSCOPACBranch.Focus();
                            }
                            else if (gvBOSCOPACActivity.GetSelectedRows().Length <= 0)
                            {
                                MessageRender.ShowMessage("Select BOSCOPAC Activty/Project");
                                gvBOSCOPACActivity.Focus();
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultarg.Message);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DATA_MIGRATION_COULD_NOT_PROCEED_INFO) + " " + err.Message);
                txtBasePath.Focus();
            }
        }

        private void boscopacsystem_IncreaseProgressBar(object sender, EventArgs e)
        {
            progressBar.PerformStep();
            Application.DoEvents();
        }

        private void boscopacsystem_InitProgressBar(object sender, EventArgs e)
        {
            EventMigrationProcessbarArgs progressevent = (e as EventMigrationProcessbarArgs);
            lblMessage.Text = progressevent.ProgressMessage;
            progressBar.Properties.Minimum = 0;
            progressBar.Properties.Maximum = progressevent.MaxRecord;
            progressBar.Properties.Step = 1;
            progressBar.PerformStep();
            progressBar.Visible = true;
        }

        private void glkpBOSCOPACBranch_EditValueChanged(object sender, EventArgs e)
        {
            using (BOSCOPACMigrationSystem boscopacsystem = new BOSCOPACMigrationSystem(BasePath))
            {
                ResultArgs resultarg = boscopacsystem.GetActivity();
                if (resultarg.Success && resultarg.DataSource.Table != null)
                {
                    DataTable dtProjects = resultarg.DataSource.Table;
                    dtProjects.DefaultView.RowFilter = "code = " +  BOSCOPACHouse;
                    UtilityMember.ComboSet.BindGridLookUpCombo(glkpBOSCOPACProject, dtProjects.DefaultView.ToTable(), "activity", "tcode");
                    if (resultarg.DataSource.Table.Rows.Count > 0)
                    {
                        glkpBOSCOPACProject.EditValue = glkpBOSCOPACProject.Properties.GetKeyValue(0);
                    }
                }
            }
        }

        private bool BOSCOPACAlreadyMigrated()
        {
            bool rtn = false;
            string selectedactiviy= string.Empty;
            foreach (int index in gvBOSCOPACActivity.GetSelectedRows())
            {
                DataRow dr = gvBOSCOPACActivity.GetDataRow(index) as DataRow;
                selectedactiviy = dr["activity"].ToString().Trim();
                using (BOSCOPACMigrationSystem bosocopacsystem = new BOSCOPACMigrationSystem())
                {
                    int DivisionId = bosocopacsystem.DEF_DIVISIONID;
                    rtn = bosocopacsystem.IsBOSCOPACMigrationMade(selectedactiviy, DivisionId);
                    if (rtn)
                    {
                        break;
                    }
                }
            }
            return rtn;
        }

        private bool RemovePreviousMigration()
        {
            bool rtn = false;
            string selectedactiviy = string.Empty;
            foreach (int index in gvBOSCOPACActivity.GetSelectedRows())
            {
                string ProjectDivision = "";
                DataRow dr = gvBOSCOPACActivity.GetDataRow(index) as DataRow;
                selectedactiviy = dr["activity"].ToString().Trim();
                if (selectedactiviy.Contains("-"))
                {
                    ProjectDivision = selectedactiviy.Substring(selectedactiviy.IndexOf('-'));
                }
                int DivisionId = ProjectDivision.Contains("Local") ? 1 : ProjectDivision.Contains("Foreign") ? 2 : 1;
                using (BOSCOPACMigrationSystem bosocopacsystem = new BOSCOPACMigrationSystem())
                {
                    bosocopacsystem.RemovePriviousMigration(selectedactiviy, DivisionId);
                }
            }
            return rtn;
        }

        private bool IsAuditVouchersLockedVoucherDate()
        {
            bool rtn = false;
            string selectedactiviy = string.Empty;
            foreach (int index in gvBOSCOPACActivity.GetSelectedRows())
            {
                string ProjectDivision = "";
                DataRow dr = gvBOSCOPACActivity.GetDataRow(index) as DataRow;
                selectedactiviy = dr["activity"].ToString().Trim();
                if (selectedactiviy.Contains("-"))
                {
                    ProjectDivision = selectedactiviy.Substring(selectedactiviy.IndexOf('-'));
                }
                int DivisionId = ProjectDivision.Contains("Local") ? 1 : ProjectDivision.Contains("Foreign") ? 2 : 1;
                using (BOSCOPACMigrationSystem bosocopacsystem = new BOSCOPACMigrationSystem())
                {
                    rtn = bosocopacsystem.IsAuditVouchersLockedVoucherDate(selectedactiviy, DivisionId);
                    if (rtn)
                   {
                       break;
                   }
                }
            }
            return rtn;
        }

        private void LoadBOSCOPACData()
        {
            using (BOSCOPACMigrationSystem boscopacsystem = new BOSCOPACMigrationSystem(txtBasePath.Text))
            {
                glkpBOSCOPACBranch.Properties.DataSource = null;
                glkpBOSCOPACProject.Properties.DataSource = null;
                if (!string.IsNullOrEmpty(BasePath))
                {
                    ResultArgs resultarg = boscopacsystem.CheckVFPOLEDBDriverInstalled();
                    if (resultarg.Success)
                    {
                        resultarg = boscopacsystem.CheckValidBOSCOPACPath();
                        if (resultarg.Success)
                        {
                            resultarg = boscopacsystem.GetHouses();
                            if (resultarg.Success && resultarg.DataSource.Table != null)
                            {
                                UtilityMember.ComboSet.BindGridLookUpCombo(glkpBOSCOPACBranch, resultarg.DataSource.Table, "house", "code");
                                if (resultarg.DataSource.Table.Rows.Count > 0)
                                {
                                    glkpBOSCOPACBranch.EditValue = glkpBOSCOPACBranch.Properties.GetKeyValue(0);
                                }
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultarg.Message);
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultarg.Message);
                    }
                }
            }
        }

        private void Migrate()
        {
            using (BOSCOPACMigrationSystem boscopacsystem = new BOSCOPACMigrationSystem(BasePath))
            {
                ResultArgs resultarg = boscopacsystem.CheckValidBOSCOPACPath();
                if (resultarg.Success)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.DataMigration.MIGRATION_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (!IsAuditVouchersLockedVoucherDate())
                        {
                            this.UseWaitCursor = true;
                            bool startmigration = true;
                            if (BOSCOPACAlreadyMigrated())
                            {
                                string strMessage = String.Format("{4} are available.{5}Do you want to delete the Project(s) and continue migration?{3}{0}Yes      : Delete and Continue{1}No       : Merge with old records{2}Cancel: Stop Migration.",
                                Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Selected Project(s)", Environment.NewLine);
                                DialogResult result = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                btnMigrate.Enabled = false;
                                if (result == DialogResult.Yes)
                                {
                                    RemovePreviousMigration();
                                    startmigration = true;
                                }
                                else if (result == DialogResult.Cancel)
                                {
                                    btnMigrate.Enabled = true;
                                    this.UseWaitCursor = false;
                                }
                                else
                                {
                                    startmigration = true;
                                }
                            }

                            if (startmigration)
                            {
                                boscopacsystem.InitProgressBar += new EventHandler(boscopacsystem_InitProgressBar);
                                boscopacsystem.IncreaseProgressBar += new EventHandler(boscopacsystem_IncreaseProgressBar);
                                this.Height = 225;
                                this.CenterToScreen();
                                lclblMessageInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                lcProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                                resultarg = boscopacsystem.MigrateBOSCOPAC(BOSCOPACHouse, BOSCOPACSelectedActivitiesId);
                                if (resultarg.Success)
                                {
                                    this.UseWaitCursor = false;
                                    btnMigrate.Enabled = true;
                                    lcProgressbar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    this.Height = 210;
                                    this.CenterToScreen();
                                    SettingProperty.Is_Application_Logout = true;
                                    frmBalanceRefresh refreshBalance = new frmBalanceRefresh();
                                    refreshBalance.ShowDialog(this);
                                }
                                else
                                {
                                    this.UseWaitCursor = false;
                                    btnMigrate.Enabled = true;
                                    MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Master.DataUtilityForms.DATA_MIGRATION_COULD_NOT_PROCEED_INFO) + " " + resultarg.Message);
                                    txtBasePath.Focus();
                                }
                            }
                            this.UseWaitCursor = false;
                        }
                    }
                }
                else
                {
                    this.UseWaitCursor = false;
                    MessageRender.ShowMessage(resultarg.Message);
                    txtBasePath.Focus();
                }
            }
        }

        private void frmMigrationBOSCOPAC_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SettingProperty.Is_Application_Logout)
                Application.Restart();
            else
                this.Close();
        }
    }
}