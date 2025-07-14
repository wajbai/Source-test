using System;
using System.Linq;

using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using AcMEDSync;
using DevExpress.XtraRichEdit.API.Native;
using System.Net.Mail;
using System.IO;

namespace Bosco.Model
{
    public class DonorFrontOfficeSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Properties
        public DonorMailTemplate TemplateType { get; set; }
        public CommunicationMode Communicationmode { get; set; }
        public string Template { get; set; }
        public DataTable dtSelectedDataSource { get; set; }
        public DevExpress.XtraRichEdit.API.Native.Document document { get; set; }
        public AlternateViewCollection DocumentAlternateViewCollection { get; set; }
        public int DonorId { get; set; }
        public int VoucherId { get; set; }
        public string DonorEmailId { get; set; }
        public string DonorPhoneNo { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime CurrentBirthDayDate { get; set; }
        public DateTime CurrentMarriageDayDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public byte[] NewsLetter { get; set; }

        public int TemplateId { get; set; }
        public int LetterTypeId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }

        public int TagId { get; set; }
        public string TagName { get; set; }
        public string NewsLetterPath { get; set; }
        public int[] SelectedDonors { get; set; }
        public int[] SelectedProspects { get; set; }
        public int Status { get; set; }
        public int TypeId { get; set; }
        #endregion

        #region Common Methods
        public ResultArgs FetchTemplates()
        {
            using (DataManager dtmanger = new DataManager(SQLCommand.FrontOffice.FetchLetterTypes))
            {
                resultArgs = dtmanger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchTemplatesForSMS()
        {
            using (DataManager dtmanger = new DataManager(SQLCommand.FrontOffice.FetchLetterTypesForSMS))
            {
                resultArgs = dtmanger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchTemplateContent()
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.FrontOffice.FetchLetterTypeContent))
            {
                dtmanager.Parameters.Add(this.AppSchema.DonorMailTemplateType.LETTER_TYPE_IDColumn, LetterTypeId);
                dtmanager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = dtmanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int FetchTemplateIdByname(string Temaplatename)
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.FrontOffice.FetchLetterTypeIdByName))
            {
                dtmanager.Parameters.Add(this.AppSchema.DonorMailTemplateType.NAMEColumn, Temaplatename);
                //dtmanager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = dtmanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchTemplateContentById()
        {
            using (DataManager dtmanger = new DataManager(SQLCommand.FrontOffice.FetchContentById))
            {
                dtmanger.Parameters.Add(this.AppSchema.DonorMailTemplateType.IDColumn, TemplateId);
                dtmanger.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = dtmanger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchContentByname()
        {
            using (DataManager dtmanger = new DataManager(SQLCommand.FrontOffice.FetchContentByName))
            {
                dtmanger.Parameters.Add(this.AppSchema.DonorMailTemplateType.NAMEColumn, Name);
                dtmanger.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = dtmanger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Mail & SMS
        #region Common Methods
        public ResultArgs SendDonorMail()
        {
            resultArgs.Success = true;
            bool isBeginTransaction = false;
            using (DataManager dataManager = new DataManager())
            {
                try
                {
                    if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                    {
                        dataManager.BeginTransaction();
                        isBeginTransaction = true;
                        if (TemplateType == DonorMailTemplate.Thanksgiving)
                        {
                            resultArgs = SendThanksGivingMail();
                        }
                        else if (TemplateType == DonorMailTemplate.Appeal)
                        {
                            resultArgs = SendAppealMail();
                        }
                        else if (TemplateType == DonorMailTemplate.Anniversary)
                        {
                            resultArgs = SendAnniversaryMail();
                        }
                        else if (TemplateType == DonorMailTemplate.Tasks)
                        {
                            resultArgs = SendFeastDayMail();
                        }
                        else if (TemplateType == DonorMailTemplate.NewsLetter)
                        {
                            resultArgs = SendNewsLetterMail();
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Records are not available to send mail";
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.Message;
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }

                    if (isBeginTransaction)
                    {
                        dataManager.EndTransaction();
                    }
                }
            }
            return resultArgs;
        }

        private string GetSMSContent(int rowindex)
        {
            string emailcontent = string.Empty;
            if (document != null)
            {
                if (document.Sections.Count >= rowindex)
                {
                    Section section = document.Sections[rowindex];
                    int start = section.Paragraphs[0].Range.Start.ToInt();
                    int end = section.Paragraphs[section.Paragraphs.Count - 1].Range.End.ToInt() - start - 1;
                    DocumentRange sectionRange = document.CreateRange(start, end);
                    emailcontent = document.GetText(sectionRange);
                }
            }
            return emailcontent;
        }

        private AlternateView GetMailContent(int rowindex)
        {
            AlternateView alternateView = null;

            if (DocumentAlternateViewCollection != null)
            {
                if (DocumentAlternateViewCollection.Count >= rowindex)
                {
                    alternateView = DocumentAlternateViewCollection[rowindex];
                }
            }
            return alternateView;
        }

        public ResultArgs SendDonorSMS()
        {
            resultArgs.Success = true;
            bool isBeginTransaction = false;
            using (DataManager dataManager = new DataManager())
            {
                try
                {
                    if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                    {
                        dataManager.BeginTransaction();
                        isBeginTransaction = true;
                        if (TemplateType == DonorMailTemplate.Thanksgiving)
                        {
                            resultArgs = SendThanksGivingSMS();
                        }
                        else if (TemplateType == DonorMailTemplate.Appeal)
                        {
                            resultArgs = SendAppealSMS();
                        }
                        else if (TemplateType == DonorMailTemplate.Anniversary)
                        {
                            resultArgs = SendAnniversarySMS();
                        }
                        else if (TemplateType == DonorMailTemplate.Tasks)
                        {
                            resultArgs = SendFeastSMS();
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Records are not available to send sms";
                    }
                }
                catch (Exception ex)
                {
                    resultArgs.Message = ex.Message;
                }
                finally
                {
                    if (!resultArgs.Success)
                    {
                        dataManager.TransExecutionMode = ExecutionMode.Fail;
                    }

                    if (isBeginTransaction)
                    {
                        dataManager.EndTransaction();
                    }
                }
            }
            return resultArgs;
        }
        #endregion

        #region Thanksgiving
        /// <summary>
        /// Send the Thanks giving Mail
        /// </summary>
        /// <returns></returns>
        private ResultArgs SendThanksGivingMail()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorEmailId = dr[this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();

                        if (!string.IsNullOrEmpty(DonorEmailId))
                        {
                            AlternateView emailbodyconent = GetMailContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (emailbodyconent != null)
                            {
                                if (dr["RECEIPT"] != null)
                                {
                                    resultArgs = Common.SendEmail(DonorEmailId, (string.IsNullOrEmpty(this.ThanksGivingSubject)) ? "Thanksgiving Mail Regarding" : this.ThanksGivingSubject,
                                        true, emailbodyconent, dr["RECEIPT"] as MemoryStream, "Donor Receipt");

                                    if (resultArgs.Success)
                                    {
                                        DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString());
                                        VoucherId = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());
                                        resultArgs = UpdateThanksGivingStatus();

                                        //to miantain mailinghistory
                                        if (resultArgs.Success)
                                        {
                                            int MemberType = 1; //1--Donor,2---Propsect
                                            resultArgs = SaveSentLetters(MemberType);
                                        }
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Update Email Status for Thanksgiving
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateThanksGivingStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.ThanksgivingMailStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        ///send Thanks giving SMS ...
        ///Parameters are: donor id,phone no
        /// </summary>
        /// <returns></returns>
        private ResultArgs SendThanksGivingSMS()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorPhoneNo = dr["MOBILE NO"].ToString();

                        if (!string.IsNullOrEmpty(DonorPhoneNo))
                        {
                            string Messageconent = GetSMSContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (!string.IsNullOrEmpty(Messageconent))
                            {
                                resultArgs = Common.SendSMS(DonorPhoneNo, Messageconent);
                                if (resultArgs.Success)
                                {
                                    VoucherId = this.NumberSet.ToInteger(dr[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());

                                    resultArgs = UpdateThanksGivingSMSStatus();
                                    //to miantain mailinghistory
                                    if (resultArgs.Success)
                                    {
                                        DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString());
                                        int MemberType = 1; //1--Donor,2---Prospect
                                        resultArgs = SaveSentLetters(MemberType);
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private ResultArgs UpdateThanksGivingSMSStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.UpdateThanksgivingSMSStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Appeal
        /// <summary>
        /// Get the Appeal Mail
        /// </summary>
        /// <returns></returns>
        private ResultArgs SendAppealMail()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorEmailId = dr[this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();

                        if (!string.IsNullOrEmpty(DonorEmailId))
                        {
                            AlternateView emailbodyconent = GetMailContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (emailbodyconent != null)
                            {
                                resultArgs = Common.SendEmail(DonorEmailId, (string.IsNullOrEmpty(this.AppealSubject)) ? "Appeal Mail Regarding" : this.AppealSubject,
                                    string.Empty, true, emailbodyconent);
                                if (resultArgs.Success)
                                {
                                    DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString());
                                    resultArgs = UpdateAppealStatus();
                                    if (resultArgs.Success)
                                    {
                                        int MemberType = 1;//1---Donor 2---Propsect
                                        resultArgs = SaveSentLetters(MemberType);
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Update Email Status for Appeal
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateAppealStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.AppealMailStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        ///send Thanks giving SMS ...
        ///Parameters are: donor id,phone no
        /// </summary>
        /// <returns></returns>
        private ResultArgs SendAppealSMS()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorPhoneNo = dr["MOBILE NO"].ToString();

                        if (!string.IsNullOrEmpty(DonorPhoneNo))
                        {
                            string Messageconent = GetSMSContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (!string.IsNullOrEmpty(Messageconent))
                            {
                                resultArgs = Common.SendSMS(DonorPhoneNo, Messageconent);
                                if (resultArgs.Success)
                                {
                                    DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString());
                                    resultArgs = UpdateAppealSMSStatus();
                                    if (resultArgs.Success)
                                    {
                                        int MemberType = 1;//1---Donor 2---Propsect
                                        resultArgs = SaveSentLetters(MemberType);
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private ResultArgs UpdateAppealSMSStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.UpdateAppealSMSStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region News Letter
        /// <summary>
        /// Get the News Letter Details
        /// </summary>
        /// <returns></returns>
        private ResultArgs SendNewsLetterMail()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorEmailId = dr[this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();
                        if (!string.IsNullOrEmpty(DonorEmailId))
                        {
                            AlternateView emailbodyconent = GetMailContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (emailbodyconent != null)
                            {
                                // byte[] content = (byte[])dr[this.AppSchema.DonorTags.NEWS_LETTERColumn.ColumnName];
                                if (Content != null && Content.Count() > 0)
                                {
                                    MemoryStream ms = new MemoryStream(Content);

                                    resultArgs = Common.SendEmail(string.Empty, DonorEmailId, string.Empty, "News Letter Regarding", string.Empty, string.Empty, true, emailbodyconent, ms, "News Letter");
                                    if (resultArgs.Success)
                                    {
                                        DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.REF_IDColumn.ColumnName].ToString());
                                        TagId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.TAG_IDColumn.ColumnName].ToString());
                                        TypeId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.TYPE_IDColumn.ColumnName].ToString());
                                        Communicationmode = CommunicationMode.MailDesk;

                                        if (DonorId > 0 && TagId > 0)
                                        {
                                            resultArgs = UpdateFeastStatus();
                                        }
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Update Email Status for News Letter
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateNewsLetterStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.NewsLetterMailStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.DONOR_IDColumn, DonorId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Anniversary
        private ResultArgs SendAnniversaryMail()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorEmailId = dr[this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();

                        if (!string.IsNullOrEmpty(DonorEmailId))
                        {
                            AlternateView emailbodyconent = GetMailContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (emailbodyconent != null)
                            {
                                string AnniversarySub = (MaritalStatus == 0) ? (string.IsNullOrEmpty(this.BirthdaySubject) ? "Birthday Wishes" : this.BirthdaySubject)
                                    : (string.IsNullOrEmpty(this.WeddingdaySubject) ? "Wedding Anniversary Wishes" : this.WeddingdaySubject);
                                resultArgs = Common.SendEmail(DonorEmailId, AnniversarySub, string.Empty, true, emailbodyconent);
                                if (resultArgs.Success)
                                {
                                    DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString());
                                    DateTime DateOfBirth = this.DateSet.ToDate(dr[this.AppSchema.DonorProspects.DOBColumn.ColumnName].ToString(), false);
                                    CurrentBirthDayDate = new DateTime(DateTime.Now.Year, DateOfBirth.Month, DateOfBirth.Day);
                                    DateTime AnniversaryDate = this.DateSet.ToDate(dr[this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn.ColumnName].ToString(), false);
                                    CurrentMarriageDayDate = new DateTime(DateTime.Now.Year, AnniversaryDate.Month, AnniversaryDate.Day);
                                    if (DonorId > 0)
                                    {
                                        resultArgs = UpdateAnniversaries();
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Update Email status for Anniversaries
        /// </summary>
        /// <returns></returns>
        private ResultArgs UpdateAnniversaries()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.UpdateAnniversaryMailStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                if (MaritalStatus == 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.CURRENT_BIRTHDAY_DATE_EMAILColumn, CurrentBirthDayDate);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.CURRENT_MARRIAGE_DATE_EMAILColumn, CurrentMarriageDayDate);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        ///send Thanks giving SMS ...
        ///Parameters are: donor id,phone no
        /// </summary>
        /// <returns></returns>
        private ResultArgs SendAnniversarySMS()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorPhoneNo = dr["MOBILE NO"].ToString();

                        if (!string.IsNullOrEmpty(DonorPhoneNo))
                        {
                            string Messageconent = GetSMSContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (!string.IsNullOrEmpty(Messageconent))
                            {
                                resultArgs = Common.SendSMS(DonorPhoneNo, Messageconent);
                                if (resultArgs.Success)
                                {
                                    DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString());
                                    DateTime DateOfBirth = this.DateSet.ToDate(dr[this.AppSchema.DonorProspects.DOBColumn.ColumnName].ToString(), false);
                                    CurrentBirthDayDate = new DateTime(DateTime.Now.Year, DateOfBirth.Month, DateOfBirth.Day);
                                    DateTime AnniversaryDate = this.DateSet.ToDate(dr[this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn.ColumnName].ToString(), false);
                                    CurrentMarriageDayDate = new DateTime(DateTime.Now.Year, AnniversaryDate.Month, AnniversaryDate.Day);
                                    if (DonorId > 0)
                                    {
                                        resultArgs = UpdateAnniversarySMSStatus();
                                    }
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Phone NO is empty to send anniversary Letter";
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private ResultArgs UpdateAnniversarySMSStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.UpdateAnniversarySMSStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                if (MaritalStatus == 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.CURRENT_BIRTHDAY_DATE_SMSColumn, CurrentBirthDayDate);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorAuditor.CURRENT_MARRIAGE_DATE_SMSColumn, CurrentMarriageDayDate);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Feast Day
        private ResultArgs SendFeastDayMail()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorEmailId = dr[this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();

                        if (!string.IsNullOrEmpty(DonorEmailId))
                        {
                            AlternateView emailbodyconent = GetMailContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (emailbodyconent != null)
                            {
                                resultArgs = Common.SendEmail(DonorEmailId, "Feast Day Regarding", string.Empty, true, emailbodyconent);
                                if (resultArgs.Success)
                                {
                                    DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.REF_IDColumn.ColumnName].ToString());
                                    TagId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.TAG_IDColumn.ColumnName].ToString());
                                    TypeId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.TYPE_IDColumn.ColumnName].ToString());
                                    Communicationmode = CommunicationMode.MailDesk;

                                    if (DonorId > 0 && TagId > 0)
                                    {
                                        resultArgs = UpdateFeastStatus();
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        /// <summary>
        ///send Thanks giving SMS ...
        ///Parameters are: donor id,phone no
        /// </summary>
        /// <returns></returns>
        private ResultArgs SendFeastSMS()
        {
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorPhoneNo = dr["MOBILE NO"].ToString();

                        if (!string.IsNullOrEmpty(DonorPhoneNo))
                        {
                            string Messageconent = GetSMSContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (!string.IsNullOrEmpty(Messageconent))
                            {
                                resultArgs = Common.SendSMS(DonorPhoneNo, Messageconent);
                                if (resultArgs.Success)
                                {
                                    DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.REF_IDColumn.ColumnName].ToString());
                                    TagId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.TAG_IDColumn.ColumnName].ToString());
                                    TypeId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorTags.TYPE_IDColumn.ColumnName].ToString());
                                    Communicationmode = CommunicationMode.ContactDesk;

                                    if (DonorId > 0 && TagId > 0)
                                    {
                                        resultArgs = UpdateFeastStatus();
                                    }
                                }
                            }
                        }

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private ResultArgs UpdateFeastStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.UpdateFeastStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.REF_IDColumn, DonorId);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TYPE_IDColumn, TypeId);
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
        #endregion

        #region Feast Day Methods
        public ResultArgs FetchFeastDonorTemplateTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchFeastDonorTemplateTypes))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorMailTemplateType.LETTER_TYPE_IDColumn, LetterTypeId);
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveTemplate()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.FrontOffice.SaveTemplate))
            {
                datamanager.Parameters.Add(this.AppSchema.DonorMailTemplateType.LETTER_TYPE_IDColumn, LetterTypeId);
                datamanager.Parameters.Add(this.AppSchema.DonorMailTemplateType.NAMEColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.DonorMailTemplateType.CONTENTColumn, Content);
                datamanager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int CheckFeastNameExists()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.FrontOffice.CheckFeastNameExists))
            {
                datamanager.Parameters.Add(this.AppSchema.DonorMailTemplateType.NAMEColumn, Name);
                datamanager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchNonPerformingDonors(string date)
        {
            using (DataManager dtManager = new DataManager(SQLCommand.FrontOffice.FetchNonPerformingDonors))
            {
                dtManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                dtManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, date);
                dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dtManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// To miantain mailing history in DB
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveSentLetters(int Type)
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.FrontOffice.InsertSentLetters))
            {
                dtmanager.Parameters.Add(this.AppSchema.DonorMailTemplateType.NAMEColumn, TemplateType);
                dtmanager.Parameters.Add(this.AppSchema.DonorProspects.DOBColumn, this.DateSet.ToDate(DateTime.Now.ToString(), false));
                dtmanager.Parameters.Add(this.AppSchema.DonorAuditor.DONOR_IDColumn, DonorId);
                dtmanager.Parameters.Add(this.AppSchema.DonorAuditor.TYPEColumn, Type);
                dtmanager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = dtmanager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region NewsLetter
        public ResultArgs SaveFeastTask()
        {
            resultArgs.Success = true;
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();

                    if (TagId > 0)
                    {
                        resultArgs = DeleteTagDetails();
                    }

                    resultArgs = SaveTaskDetails();
                    if (resultArgs.Success)
                    {
                        TagId = (TagId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : TagId;

                        resultArgs = MapTasks();
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        public ResultArgs MapTasks()
        {
            try
            {
                if (SelectedDonors.Count() > 0)
                {
                    foreach (int Donor in SelectedDonors)
                    {
                        DonorId = Donor;
                        TypeId = (int)MemberType.Donor;
                        resultArgs = MapTaskDonors();

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }

                if (SelectedProspects.Count() > 0)
                {
                    foreach (int prospectId in SelectedProspects)
                    {
                        DonorId = prospectId;
                        TypeId = (int)MemberType.Prospect;
                        resultArgs = MapTaskDonors();

                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        public ResultArgs MapTaskDonors()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.MapTagDonor))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TYPE_IDColumn, TypeId);
                dataManager.Parameters.Add(this.AppSchema.DonorMailTemplateType.LETTER_TYPE_IDColumn, (int)Communicationmode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteTagDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.DeleteTaskDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveTaskDetails()
        {
            using (DataManager dataManager = new DataManager(TagId == 0 ? SQLCommand.FrontOffice.InsertTask : SQLCommand.FrontOffice.UpdateTask))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId, true);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_NAMEColumn, TagName);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.NEWS_LETTER_PATHColumn, NewsLetterPath);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.NEWS_LETTERColumn, NewsLetter);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.LETTER_TYPE_IDColumn, (int)TemplateType);
                dataManager.Parameters.Add(this.AppSchema.DonorMailTemplateType.IDColumn, TemplateId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchTaskDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchTaskDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.LETTER_TYPE_IDColumn, (int)TemplateType);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTaskByTagId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchTaskByTagId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDonorMappedStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchDonorMappedStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProspectsMappedStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchProspectsMappedStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedDonorByTagId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchMappedDonorByTagId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TYPE_IDColumn, (int)Communicationmode);

                if (Status != 2) //Both ( Sent, Not Sent)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.STATUSColumn, Status);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAniversaryTypeDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchAnniversaryTypeDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.MARITAL_STATUS_IDsColumn, MaritalStatus); //(MaritalStatus == 0 ? "0,1" : MaritalStatus.ToString()));
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchAnniversaryTypeSMSDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchAnniversaryTypeSMSDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.MARITAL_STATUS_IDsColumn, MaritalStatus); // (MaritalStatus == 0 ? "0,1" : MaritalStatus.ToString()));
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)Communicationmode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }



        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchDonorsByTaskId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchDonorByTask))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);

                if (Status != 2) //Both ( Sent, Not Sent)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.STATUSColumn, Status);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNewsLetterByTaskId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FrontOffice.FetchNewsLetterByTask))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorTags.TAG_IDColumn, TagId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region SMS Methods
        public ResultArgs FetchDonorDetails()
        {
            using (DataManager dtManager = new DataManager(SQLCommand.FrontOffice.FetchDonorDetails))
            {
                resultArgs = dtManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region ExportDonorVouchers
        public ResultArgs FetchDonorMappedProjects()
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.FrontOffice.FetchDonorMappedProjects))
            {
                resultArgs = dtmanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region SMS Credits

        public DataTable ConsturctSMSCredits()
        {
            DataTable dtSMSCredits = GenerateSMSCretitSource();
            try
            {
                if (dtSelectedDataSource != null && dtSelectedDataSource.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDataSource.Rows)
                    {
                        DonorPhoneNo = dr["MOBILE NO"].ToString();

                        if (!string.IsNullOrEmpty(DonorPhoneNo))
                        {
                            string Messageconent = GetSMSContent(dtSelectedDataSource.Rows.IndexOf(dr));
                            if (!string.IsNullOrEmpty(Messageconent))
                            {
                                string MessageContentCount = Messageconent.TrimEnd('\n');
                                MessageContentCount = MessageContentCount.TrimEnd('\r');
                                decimal characterCount = MessageContentCount.Count();
                                decimal MsgCredits = Math.Ceiling(characterCount / 160);
                                int MessageCredits = this.NumberSet.ToInteger(MsgCredits.ToString());

                                //DonorId = this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString());

                                dtSMSCredits.Rows.Add(dr[this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString(), characterCount, MessageCredits);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return dtSMSCredits;
        }

        private DataTable GenerateSMSCretitSource()
        {
            DataTable dtSMSCredit = new DataTable();
            // dtSMSCredit.Columns.Add("DONAUD_ID", typeof(int));
            dtSMSCredit.Columns.Add("NAME", typeof(string));
            dtSMSCredit.Columns.Add("NO_OF_CHARACTERS", typeof(int));
            dtSMSCredit.Columns.Add("NO_OF_CREDITS", typeof(int));

            dtSMSCredit.NewRow();
            return dtSMSCredit;
        }

        #endregion
    }
}
