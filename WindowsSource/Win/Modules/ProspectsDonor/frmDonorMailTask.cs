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
using Bosco.Model;
using Bosco.Model.Donor;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.ConfigSetting;
using System.Diagnostics;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorMailTask : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        private string FilePath = string.Empty;
        public event EventHandler UpdataHeld;

        public int TagId { get; set; }
        public CommunicationMode ComMode { get; set; }
        byte[] bytesnewsletter { get; set; }
        private bool newsletterfromLocal = false;

        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public frmDonorMailTask()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Load Default Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDonorMailTask_Load(object sender, EventArgs e)
        {
            LoadDonorDetails();
            LoadProspectDetails();
            AssignValues();
        }

        /// <summary>
        /// Save the Task Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateTask())
                {
                    int[] SelectedDonor = GetCheckedDonors();
                    int[] SelectedProspects = GetCheckedProspects();
                    ResultArgs resultArgs = null;
                    using (DonorFrontOfficeSystem donorOffice = new DonorFrontOfficeSystem())
                    {
                        donorOffice.Communicationmode = ComMode;
                        donorOffice.TagId = TagId;
                        donorOffice.TagName = txtTask.Text.Trim();
                        donorOffice.NewsLetterPath = FilePath;
                        if (!newsletterfromLocal)
                        {
                            bytesnewsletter = GetReturnPath(donorOffice.NewsLetterPath);
                        }
                        donorOffice.NewsLetter = bytesnewsletter != null ? bytesnewsletter : null;
                        donorOffice.TemplateType = DonorMailTemplate.NewsLetter;
                        donorOffice.TemplateId = (int)DonorMailTemplate.NewsLetter;
                        donorOffice.SelectedDonors = SelectedDonor;
                        donorOffice.SelectedProspects = SelectedProspects;
                        resultArgs = donorOffice.SaveFeastTask();
                        if (resultArgs.Success)
                        {
                            //this.ShowSuccessMessage("Task has been created successfully.");
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Networking.DonorMailTask.DONOR_MAIL_TASK_CREATE_INFO));
                            if (UpdataHeld != null)
                            {
                                UpdataHeld(this, e);
                            }
                            txtTask.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Browse the Path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FilePath = SelectedPathName();
            if (!string.IsNullOrEmpty(FilePath))
            {
                newsletterfromLocal = false;

                txtPath.Text = FilePath;
                //txtPath.Visible = true;
                //lblNewsletter.Visible = false;
                lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciNewsLetterLable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        /// <summary>
        /// filter the Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvDonor.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvDonor, gccolDonor);
            }
        }

        /// <summary>
        /// Assign the donor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDonor_RowCountChanged(object sender, EventArgs e)
        {
            lblAssignDonorCount.Text = gvDonor.RowCount.ToString();
        }

        /// <summary>
        /// rowcount 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProspects_RowCountChanged(object sender, EventArgs e)
        {
            lblAssignProspectCounts.Text = gvProspects.RowCount.ToString();
        }

        /// <summary>
        /// filter Prospects Records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkProspectFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvProspects.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvProspects, gccolProspect);
            }
        }

        /// <summary>
        /// Close the forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Select the PathName
        /// </summary>
        private string SelectedPathName()
        {
            string selectedfolderPath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.pdf";
            openFileDialog.Filter = "Pdf Files|*.pdf";
            openFileDialog.Title = "Upload";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
            }
            return FilePath;
        }

        /// <summary>
        /// Return the path name 
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns></returns>
        private byte[] GetReturnPath(string PathName)
        {
            byte[] GetPathBytes;
            if (PathName != string.Empty)
            {
                string NewsLetterContent = PathName;
                string filename = Path.GetFileName(NewsLetterContent);
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                GetPathBytes = bytesnewsletter = br.ReadBytes((Int32)fs.Length);
                br.Close();
                fs.Close();
            }
            else
            {
                GetPathBytes = bytesnewsletter;
            }
            return GetPathBytes;
        }

        /// <summary>
        /// To Validate the Purpose Details
        /// </summary>
        /// <returns></returns>
        public bool ValidateTask()
        {
            bool isValue = true;
            if (string.IsNullOrEmpty(txtTask.Text.Trim()))
            {
                //this.ShowMessageBox(this.GetMessage("Task is empty"));
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorMailTask.DONOR_MAIL_TASK_EMPTY_INFO));
                this.SetBorderColor(txtTask);
                isValue = false;
                txtTask.Focus();
            }
            return isValue;
        }

        /// <summary>
        /// Get Checked Donors
        /// </summary>
        /// <returns></returns>
        private int[] GetCheckedDonors()
        {
            int[] SelectedIds = gvDonor.GetSelectedRows();
            int[] sCheckedProjects = new int[SelectedIds.Count()];
            int ArrayIndex = 0;
            if (SelectedIds.Count() > 0)
            {
                foreach (int RowIndex in SelectedIds)
                {
                    DataRow drDonor = gvDonor.GetDataRow(RowIndex);
                    if (drDonor != null)
                    {
                        sCheckedProjects[ArrayIndex] = UtilityMember.NumberSet.ToInteger(drDonor["DONAUD_ID"].ToString());
                        ArrayIndex++;
                    }
                }
            }
            return sCheckedProjects;
        }

        /// <summary>
        /// Get Checked Prospects
        /// </summary>
        /// <returns></returns>
        private int[] GetCheckedProspects()
        {
            int[] SelectedIds = gvProspects.GetSelectedRows();
            int[] sCheckedProjects = new int[SelectedIds.Count()];
            int ArrayIndex = 0;
            if (SelectedIds.Count() > 0)
            {
                foreach (int RowIndex in SelectedIds)
                {
                    DataRow drProspect = gvProspects.GetDataRow(RowIndex);
                    if (drProspect != null)
                    {
                        sCheckedProjects[ArrayIndex] = UtilityMember.NumberSet.ToInteger(drProspect["PROSPECT_ID"].ToString());
                        ArrayIndex++;
                    }
                }
            }
            return sCheckedProjects;
        }

        /// <summary>
        /// Load the Donor
        /// </summary>
        private void LoadDonorDetails()
        {
            try
            {
                using (DonorFrontOfficeSystem donaudSystem = new DonorFrontOfficeSystem())
                {
                    donaudSystem.TagId = TagId;
                    donaudSystem.Communicationmode = ComMode;
                    resultArgs = donaudSystem.FetchDonorMappedStatus();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcDonor.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Load Prospect Details
        /// </summary>
        private void LoadProspectDetails()
        {
            try
            {
                using (DonorFrontOfficeSystem prospectSystem = new DonorFrontOfficeSystem())
                {
                    prospectSystem.TagId = TagId;
                    prospectSystem.Communicationmode = ComMode;
                    resultArgs = prospectSystem.FetchProspectsMappedStatus();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        gcProspects.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Assign value to controls
        /// </summary>
        private void AssignValues()
        {
            try
            {
                //lblNewsletter.Visible = false;
                lciNewsLetterLable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (TagId > 0)
                {
                    using (DonorFrontOfficeSystem donorSystem = new DonorFrontOfficeSystem())
                    {
                        donorSystem.TagId = TagId;
                        resultArgs = donorSystem.FetchTaskByTagId();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            txtTask.Text = resultArgs.DataSource.Table.Rows[0][donorSystem.AppSchema.DonorTags.TAG_NAMEColumn.ColumnName].ToString();
                            //  txtPath.Text = txtTask.Text.ToString();
                            if (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][donorSystem.AppSchema.DonorTags.NEWS_LETTERColumn.ColumnName].ToString()))
                            {
                                bytesnewsletter = (byte[])(resultArgs.DataSource.Table.Rows[0][donorSystem.AppSchema.DonorTags.NEWS_LETTERColumn.ColumnName]);
                            }
                            AssignMappedDonors();
                            AssignMappedDonors();
                            if (bytesnewsletter != null)
                            {
                                lblNewsletter.Text = txtTask.Text + ".pdf";
                                newsletterfromLocal = true;
                            }

                            newsletterfromLocal = false;
                            newsletterfromLocal = (bytesnewsletter != null);
                            // txtPath.Visible = !newsletterfromLocal;
                            if (newsletterfromLocal)
                            {
                                lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                lciNewsLetterLable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                // !newsletterfromLocal;
                            }
                            else if (string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][donorSystem.AppSchema.DonorTags.NEWS_LETTERColumn.ColumnName].ToString()))
                            {
                                //lblNewsletter.Visible = newsletterfromLocal;
                                lciPath.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                lciNewsLetterLable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Assign the Donors
        /// </summary>
        private void AssignMappedDonors()
        {
            DataTable dtDonors = gcDonor.DataSource as DataTable;
            if (dtDonors != null && dtDonors.Rows.Count > 0)
            {
                gvDonor.ClearSelection();
                foreach (DataRow dr in dtDonors.Rows)
                {
                    if (this.UtilityMember.NumberSet.ToInteger(dr["MAPPED_STATUS"].ToString()) == 1)
                    {
                        int Index = dtDonors.Rows.IndexOf(dr);
                        gvDonor.SelectRow(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Mapped the Status
        /// </summary>
        private void AssignMappedProspects()
        {
            DataTable dtProspects = gcProspects.DataSource as DataTable;
            if (dtProspects != null && dtProspects.Rows.Count > 0)
            {
                gvDonor.ClearSelection();
                foreach (DataRow dr in dtProspects.Rows)
                {
                    if (this.UtilityMember.NumberSet.ToInteger(dr["MAPPED_STATUS"].ToString()) == 1)
                    {
                        int Index = dtProspects.Rows.IndexOf(dr);
                        gvDonor.SelectRow(Index);
                    }
                }
            }
        }

        #endregion

        private void lblNewsletter_Click(object sender, EventArgs e)
        {
            if (bytesnewsletter != null)
            {
                string apppath = Path.Combine(SettingProperty.ApplicationStartUpPath, lblNewsletter.Text);
                File.WriteAllBytes(apppath, bytesnewsletter);
                Process.Start(apppath);
            }
        }

        private void gcDonor_Click(object sender, EventArgs e)
        {

        }
    }
}