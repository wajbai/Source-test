using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility.ConfigSetting;
using Bosco.Model;
using Bosco.Model.Donor;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;

using Bosco.Utility;
using System.Reflection;
using Bosco.Model.UIModel.Master;
using Bosco.DAO.Schema;

namespace ACPP.Modules.ProspectsDonor
{
    public partial class frmDonorSMSTemplate : frmFinanceBase
    {
        #region Declaration
        public static string ACPERP_INSTALLED_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SMS Templates");
        public string DonorsettingFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Modules\ProspectsDonor", "DonorSetting.xml"); //"DonorSetting.xml"; //Application.StartupPath.ToString()// Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        ResultArgs resultArgs = new ResultArgs();
        public DonorMailTemplate TemplateType { get; set; }
        public AnniversaryType AnniversaryTemplatetype { get; set; }
        public int FeastTemplateId { get; set; }
        AppSchemaSet appSchema = new AppSchemaSet();
        public DialogResult dialogResult = DialogResult.Cancel;
        #endregion

        #region Properties

        #endregion

        #region Constrcutor
        public frmDonorSMSTemplate()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void SaveTemplate()
        {
            try
            {
                string TemplateName = "";
                resultArgs.Success = false;

                if (ValidateTemplate())
                {
                    TemplateName = GetTemplateName();
                    if (!string.IsNullOrEmpty(reSMSTemplate.Text))
                    {
                        resultArgs = SaveTemplateDetails();
                    }
                    else
                    {
                        //resultArgs.Message = "Template is Empty";
                        resultArgs.Message = this.GetMessage(MessageCatalog.Networking.DonorSMSTemplate.DONOR_SMS_TEMPLATE_EMPTY);
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            {
                if (resultArgs.Success)
                {
                    //this.ShowMessageBox("Template saved successfully.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorSMSTemplate.DONOR_SMS_TEMPLATE_SAVED_INFO));
                    dialogResult = System.Windows.Forms.DialogResult.OK;
                    txtFeastName.Text = string.Empty;
                }
                else
                {
                    if (!string.IsNullOrEmpty(resultArgs.Message))
                    {
                        this.ShowMessageBoxError(resultArgs.Message);
                    }
                }
            }
        }

        private bool ValidateTemplate()
        {
            bool isValid = true;
            if (glkpSMSTemplateType.EditValue == null)
            {
                isValid = false;
                //this.ShowMessageBoxWarning("Template Type is not selected.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorSMSTemplate.DONOR_SMS_TEMPLATE_TYPE_SELECT_INFO));
                glkpSMSTemplateType.Focus();
            }
            else if ((this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Anniversary) &&
                string.IsNullOrEmpty(cboAnniversaryType.Text))
            {
                isValid = false;
                //this.ShowMessageBoxWarning("Anniversary Type is not selected.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorSMSTemplate.DONOR_SMS_TEMPLATE_SELECT_ANIVERSARY_INFO));
            }
            else if (!IsValidFeastDay())
            {
                isValid = false;
            }
            return isValid;
        }

        private bool IsValidFeastDay()
        {
            bool isValid = true;
            if (this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Tasks &&
               this.UtilityMember.NumberSet.ToInteger(glkpFeastName.EditValue.ToString()) == 0 && string.IsNullOrEmpty(txtFeastName.Text))
            {
                isValid = false;
                //this.ShowMessageBoxWarning("Feast Name is Empty.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorSMSTemplate.DONOR_SMS_TEMPLATE_FESAT_NAME_EMPTY));
            }
            else if (this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Tasks &&
                (glkpFeastName.EditValue.ToString() == "0" && !string.IsNullOrEmpty(txtFeastName.Text)))
            {
                isValid = CheckifFeastNameExists();
            }
            return isValid;
        }

        private bool CheckifFeastNameExists()
        {
            using (DonorFrontOfficeSystem donorfrontofficeSystem = new DonorFrontOfficeSystem())
            {
                donorfrontofficeSystem.Communicationmode = CommunicationMode.ContactDesk;
                donorfrontofficeSystem.Name = txtFeastName.Text + ".rtf";
                if (donorfrontofficeSystem.CheckFeastNameExists() > 0)
                {
                    //ShowMessageBox("Feast name exists");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Networking.DonorSMSTemplate.DONOR_SMS_TEMPLATE_FEAST_NAME_EXISTS_INFO));
                    txtFeastName.Text = string.Empty;
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private string GetTemplateName()
        {
            string TemplateName = "";

            if (glkpSMSTemplateType.EditValue != null)
            {
                if (this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Anniversary)
                {
                    TemplateName = (cboAnniversaryType.SelectedIndex == 0) ? AnniversaryType.Birthday.ToString() : AnniversaryType.Wedding.ToString();
                }
                else if (this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Tasks)
                {
                    if (glkpFeastName.EditValue != null)
                    {
                        if (this.UtilityMember.NumberSet.ToInteger(glkpFeastName.EditValue.ToString()) == 0)
                        {
                            TemplateName = txtFeastName.Text;
                        }
                        else
                        {
                            TemplateName = glkpFeastName.Text;
                        }
                    }
                }
                else
                {
                    TemplateName = glkpSMSTemplateType.Text;
                }
            }
            return TemplateName + ".rtf";
        }

        private void HideControls()
        {
            if (glkpSMSTemplateType.EditValue != null)
            {
                if (this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Anniversary)
                {
                    layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lciFeastType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciFeastName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //  lciEmptySpace.Width = lciEmptySpace.Width + lciFeastType.Width + lciFeastName.Width;
                }
                else if (this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Tasks)
                {
                    layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciFeastType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    if (glkpFeastName.EditValue != null)
                    {
                        if (glkpFeastName.EditValue.ToString().Equals("0")) //selecting new
                        {
                            txtFeastName.Text = string.Empty;
                            reSMSTemplate.Text = string.Empty;
                            lciFeastName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                        else
                        {
                            FeastTemplateId = this.UtilityMember.NumberSet.ToInteger(glkpFeastName.EditValue.ToString());
                            txtFeastName.Text = string.Empty;
                            lciFeastName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            LoadTemplate();
                        }
                    }
                }
                else
                {
                    layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciFeastType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciFeastName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    // lciEmptySpace.Width = lciEmptySpace.Width + lciAnniversaryType.Width + lciFeastType.Width + lciFeastName.Width;
                }
            }
        }

        private void LoadTemplateType()
        {
            using (DonorFrontOfficeSystem donorFrontOfficesystem = new DonorFrontOfficeSystem())
            {
                resultArgs = donorFrontOfficesystem.FetchTemplatesForSMS();
                if (resultArgs.Success == true && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpSMSTemplateType, resultArgs.DataSource.Table, "LETTER_NAME", "LETTER_TYPE_ID");
                    glkpSMSTemplateType.EditValue = glkpSMSTemplateType.Properties.GetKeyValue(0);
                }
            }
        }
        public void LoadTemplate()
        {
            //Load the template if created already
            string MailTemplatePath = string.Empty;
            string TemplateName = string.Empty;
            if (ValidateTemplateWithoutMessage())
            {
                using (DonorFrontOfficeSystem donorfrontofficesystem = new DonorFrontOfficeSystem())
                {
                    donorfrontofficesystem.Communicationmode = CommunicationMode.ContactDesk;
                    DataTable dtContent = new DataTable();
                    AssignTemplateFields();
                    reSMSTemplate.Options.Fields.HighlightMode = FieldsHighlightMode.Always;
                    reSMSTemplate.Options.MailMerge.ViewMergedData = false;
                    reSMSTemplate.Text = string.Empty;
                    reSMSTemplate.Focus();
                    donorfrontofficesystem.LetterTypeId = this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString());
                    donorfrontofficesystem.TemplateId = FeastTemplateId;
                    if (glkpSMSTemplateType.Text == DonorMailTemplate.Tasks.ToString())
                    {
                        dtContent = donorfrontofficesystem.FetchTemplateContentById().DataSource.Table;
                    }
                    else if (glkpSMSTemplateType.Text == DonorMailTemplate.Anniversary.ToString())
                    {
                        donorfrontofficesystem.Name = cboAnniversaryType.Text + ".rtf";
                        dtContent = donorfrontofficesystem.FetchContentByname().DataSource.Table;
                    }
                    else
                    {
                        dtContent = donorfrontofficesystem.FetchTemplateContent().DataSource.Table;
                    }
                    if (dtContent != null && dtContent.Rows.Count > 0)
                    {
                        byte[] content = (byte[])dtContent.Rows[dtContent.Rows.Count - 1]["CONTENT"];
                        if (content != null && content.Count() > 0)
                        {
                            MemoryStream ms = new MemoryStream(content);
                            ms.Seek(0, SeekOrigin.Begin);
                            reSMSTemplate.LoadDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
                            ms.Close();
                        }
                    }

                    donorfrontofficesystem.Content = reSMSTemplate.Document.GetOpenXmlBytes(reSMSTemplate.Document.Range);
                }

                ShowAllFieldResultsCommand fieldResults = new ShowAllFieldResultsCommand(this.reSMSTemplate);
                fieldResults.Execute();

            }
        }
        private void AssignTemplateFields()
        {
            if (glkpSMSTemplateType != null)
            {
                if (this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Thanksgiving)
                {
                    reSMSTemplate.Options.MailMerge.DataSource = appSchema.AppSchema.ThanksgivingFields as DataTable;
                }
                else
                {
                    reSMSTemplate.Options.MailMerge.DataSource = appSchema.AppSchema.MemberFields as DataTable;
                }
                reSMSTemplate.Options.Fields.HighlightMode = FieldsHighlightMode.Always;
                reSMSTemplate.Options.MailMerge.ViewMergedData = false;
            }
        }
        private bool ValidateTemplateWithoutMessage()
        {
            bool isValid = true;
            if (glkpSMSTemplateType.EditValue == null)
            {
                isValid = false;
            }
            else if ((this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Anniversary) &&
                string.IsNullOrEmpty(cboAnniversaryType.Text))
            {
                reSMSTemplate.Text = string.Empty;
                isValid = false;
            }
            else if ((this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Tasks) &&
               glkpFeastName.EditValue == "0")//---<New>--
            {
                isValid = false;
                //this.ShowMessageBoxWarning("Feast name is not selected.");
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Networking.DonorSMSTemplate.DONOR_SMS_TEMPLATE_FEAST_NAME_NOTSELECT_INFO));
            }
            return isValid;
        }

        /// <summary>
        /// To get the DataSource Fields
        /// </summary>
        /// <returns></returns>
        private DataTable GetDatasource()
        {
            // ResultArgs resultArgs = new ResultArgs();
            DataTable dtSample = new DataTable("Donor Info");
            using (DonorFrontOfficeSystem frontofficesys = new DonorFrontOfficeSystem())
            {
                resultArgs = frontofficesys.FetchDonorDetails();
                dtSample = resultArgs.DataSource.Table.Clone();
            }
            return dtSample;
        }

        /// <summary>
        /// Append to xml file
        /// </summary>
        private ResultArgs SaveTemplateDetails()
        {
            try
            {
                using (DonorFrontOfficeSystem donorfrontofficesystem = new DonorFrontOfficeSystem())
                {
                    donorfrontofficesystem.LetterTypeId = this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString());
                    donorfrontofficesystem.Communicationmode = CommunicationMode.ContactDesk;
                    donorfrontofficesystem.Name = GetTemplateName();
                    donorfrontofficesystem.Content = reSMSTemplate.Document.GetOpenXmlBytes(reSMSTemplate.Document.Range);
                    resultArgs = donorfrontofficesystem.SaveTemplate();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// to construct the empty data source from donorsetting XML
        /// </summary>
        private DataTable ConstructDataToXML(string feastname)
        {
            DataTable dtDNRNodes = new DataTable();
            try
            {
                DataSet dsDonorSetting = XMLConverter.ConvertXMLToDataSet(DonorsettingFilePath);
                if (dsDonorSetting.Tables.Count > 0)
                {
                    DataTable dtDonorsetting = dsDonorSetting.Tables[0];
                    DataView dvDonorSetting = dtDonorsetting.AsDataView();
                    // dvRenewal.RowFilter = "FD_RENEWAL_ID NOT IN(" + TempId + ") AND FD_ACCOUNT_ID=" + FDAccountId + "";
                    dvDonorSetting.RowFilter = "Name='" + feastname + "' AND TemplateType='" + DonorMailTemplate.Tasks.ToString() + "'";
                    dtDNRNodes = dvDonorSetting.ToTable();
                    dvDonorSetting.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            return dtDNRNodes;
        }
        /// <summary>
        /// to construct the empty data source from donorsetting XML
        /// </summary>
        private DataTable LoadFeastDetails()
        {
            DataTable dtFeastList = new DataTable();
            try
            {
                if ((this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString()) == (int)DonorMailTemplate.Tasks))
                {
                    using (DonorFrontOfficeSystem donorsys = new DonorFrontOfficeSystem())
                    {
                        donorsys.LetterTypeId = this.UtilityMember.NumberSet.ToInteger(glkpSMSTemplateType.EditValue.ToString());
                        donorsys.Communicationmode = CommunicationMode.ContactDesk;
                        dtFeastList = donorsys.FetchFeastDonorTemplateTypes().DataSource.Table;
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            dtFeastList = SelectAll.AddHeaderColumn(dtFeastList, "TEMPLATE_ID", "NAME", "<--New-->");
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpFeastName, dtFeastList, "NAME", "TEMPLATE_ID");
                            if (dtFeastList.Rows.Count > 1)
                            {
                                glkpFeastName.EditValue = glkpFeastName.Properties.GetKeyValue(1);
                            }
                            else
                            {
                                glkpFeastName.EditValue = glkpFeastName.Properties.GetKeyValue(0);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBoxError(ex.Message);
            }
            return dtFeastList;
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTemplate();
            HideControls();
            LoadFeastDetails();
            LoadTemplate();
        }

        private void frmDonorSMSTemplate_Load(object sender, EventArgs e)
        {
            reSMSTemplate.Document.DefaultCharacterProperties.FontName = "Courier New";
            reSMSTemplate.Document.DefaultCharacterProperties.FontSize = 10;
            reSMSTemplate.Options.DocumentCapabilities.CharacterFormatting = DocumentCapability.Disabled;

            LoadTemplateType();
            if (TemplateType == DonorMailTemplate.Thanksgiving || TemplateType == DonorMailTemplate.Appeal)
            {
                glkpSMSTemplateType.EditValue = (int)TemplateType;
            }
            else if (TemplateType == DonorMailTemplate.Tasks)
            {
                glkpSMSTemplateType.EditValue = (int)TemplateType;
                if (FeastTemplateId > 0)
                {
                    glkpFeastName.EditValue = FeastTemplateId;
                }
            }
            else if (TemplateType == DonorMailTemplate.Anniversary)
            {
                glkpSMSTemplateType.EditValue = (int)TemplateType;
                if (AnniversaryTemplatetype == AnniversaryType.Birthday)
                {
                    cboAnniversaryType.SelectedIndex = (int)AnniversaryType.Birthday;
                }
                else
                {
                    cboAnniversaryType.SelectedIndex = (int)AnniversaryType.Wedding;
                }
            }
            HideControls();
            LoadTemplate();
        }

        private void glkpSMSTemplateType_EditValueChanged(object sender, EventArgs e)
        {
            HideControls();
            LoadFeastDetails();
            LoadTemplate();
            AssignTemplateFields();
        }

        private void cboAnniversaryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideControls();
            LoadTemplate();
        }

        private void glkpFeastName_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpFeastName.EditValue.ToString().Equals("0")) //selecting new
            {
                txtFeastName.Text = string.Empty;
                reSMSTemplate.Text = string.Empty;
                lciFeastName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                FeastTemplateId = this.UtilityMember.NumberSet.ToInteger(glkpFeastName.EditValue.ToString());
                txtFeastName.Text = string.Empty;
                lciFeastName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                LoadTemplate();
            }
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}