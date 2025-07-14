using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Sql;
using System.Data.SqlClient;

using System.Data;

using System.Configuration;
using System.Reflection;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using Bosco.Utility;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace AcMEDSync
{
    public class Common
    {
        public static string ACMEDS_TITLE = "AcME Data Synchronization Service";
        public static string ACMEDS_SERVICE_NAME = "AcMEDS";
        public static string ACMEDS_SERVICE_FILE = "AcMEDS.exe";
        public static int ACMEDS_SERVICE_TIMEOUT = 5000;

        public static string GetMailTemplate(string Header, string content, string name)
        {
            string mailContent = string.Empty;
            if (!(string.IsNullOrEmpty(Header) && string.IsNullOrEmpty(content)))
            {
                mailContent = @"<html><body><div style=""width: 90%; border: 1px solid #28688E; font-family: Verdana,Calibri;font-size: 12px;"">
                            <div style=""width: 100%; background-color: #C4DAF9; height: 60px;"">
                            <div style=""float: left;"">
                            <img src=""http://s30.postimg.org/uj0u01sfx/logo.png"" alt=""AcME ERP"" width=""100%"" height=""50""></div>
                            <div style=""float: right;""><img src=""http://s12.postimg.org/s4r8ngj5l/Picture1.png"" alt=""BoscoSoft"" width=""100%"" height=""50""></div></div>";

                mailContent += @"<div style=""width: 100%; background-color: #ffffff;""><div style=""padding: 5px;""><p><b>Dear " + name + " , </b></p></div>";
                mailContent += @"<div style=""padding: 5px;"">Welcome to AcME ERP Portal.<br /><p>" + Header + "</p></div>";
                mailContent += @"<div style=""text-align:left;background-color:#F2F6FD;border-top:solid 1px #BDD0F2;border-bottom:solid 1px #BDD0F2; padding:  25px;"">" + content + "</div>";

                mailContent += @"<div style=""padding: 5px;""><p><b>Regards,</b><br /><br />AcME ERP Team</p></div>
                              <div><p>This email can't receive replies, please do not reply to this email. For more information,visit AcME ERP website</p></div></div>
                              <div style=""width: 100%; background-color: #f1f1f1;""><div style=""text-align: center; font-weight: bold;"">You can reach us at</div><div style=""padding: 5px;"">
                              <div>&nbsp;&nbsp;Email:support@acmeerp.org</div><div>&nbsp;&nbsp;http://www.acmeerp.org</div>
                              <div>&nbsp;&nbsp;</div></div></div></div></body></html>";

            }
            return mailContent;
        }

        public static string GetMailTemplate(string Header, string content, string name, bool IsBranchOffice)
        {
            string mailContent = string.Empty;
            if (!(string.IsNullOrEmpty(Header) && string.IsNullOrEmpty(content)))
            {
                mailContent = @"<html><body><div style=""width: 90%; border: 1px solid #28688E; font-family: Verdana,Calibri;font-size: 12px;"">
                            <div style=""width: 100%; background-color: #C4DAF9; height: 60px;"">
                            <div style=""float: left;"">
                            <img src=""http://s30.postimg.org/uj0u01sfx/logo.png"" alt=""AcME ERP"" width=""100%"" height=""50""></div>
                            <div style=""float: right;""><img src=""http://s12.postimg.org/s4r8ngj5l/Picture1.png"" alt=""BoscoSoft"" width=""100%"" height=""50""></div></div>";

                mailContent += @"<div style=""width: 100%; background-color: #ffffff;""><div style=""padding: 5px;""><p><b>Dear " + name + " , </b></p></div>";
                mailContent += @"<div style=""padding: 5px;"">Welcome to AcME ERP Portal.<br /><p>" + Header + "</p></div>";
                mailContent += @"<div style=""text-align:left;background-color:#F2F6FD;border-top:solid 1px #BDD0F2;border-bottom:solid 1px #BDD0F2; padding:  25px;"">" + content + "</div>";

                mailContent += @"<div style=""padding: 5px;""><p><b>Regards,</b><br /><br />AcME ERP Team</p></div>
                              <div><p>This email can't receive replies, please do not reply to this email. For more information, 
                               contact your Head Office Admin. </p></div></div>
                              <div style=""width: 100%; background-color: #f1f1f1;""><div style=""text-align: center; font-weight: bold;"">You can reach us at</div><div style=""padding: 5px;"">
                              <div>&nbsp;&nbsp;Email:support@acmeerp.org</div><div>&nbsp;&nbsp;http://www.acmeerp.org</div>
                              <div>&nbsp;&nbsp;</div></div></div></div></body></html>";

            }
            return mailContent;
        }

        public static string GetDonorTemplate(string Header, string content, string name)
        {
            string mailContent = string.Empty;
            if (!(string.IsNullOrEmpty(Header) && string.IsNullOrEmpty(content)))
            {
                mailContent = @"<html><body><div style=""width: 90%; border: 1px solid #28688E; font-family: Verdana,Calibri;font-size: 12px;"">
                            <div style=""width: 100%; background-color: #C4DAF9; height: 60px;"">
                            <div style=""float: left;"">
                            <img src=""http://s30.postimg.org/uj0u01sfx/logo.png"" alt=""AcME ERP"" width=""100%"" height=""50""></div>
                            <div style=""float: right;""><img src=""http://s12.postimg.org/s4r8ngj5l/Picture1.png"" alt=""BoscoSoft"" width=""100%"" height=""50""></div></div>";

                mailContent += @"<div style=""width: 100%; background-color: #ffffff;""><div style=""padding: 5px;""><p><b>Dear " + name + " , </b></p></div>";
                mailContent += @"<div style=""padding: 5px;"">Welcome to BOSCONET<br /><p>" + Header + "</p></div>";
                mailContent += @"<div style=""text-align:left;background-color:#F2F6FD;border-top:solid 1px #BDD0F2;border-bottom:solid 1px #BDD0F2; padding:  25px;"">" + content + "</div>";

                mailContent += @"<div style=""padding: 5px;""><p><b>Regards,</b><br /><br />AcME ERP Team</p></div>
                              <div><p>This email can't receive replies, please do not reply to this email. For more information,visit AcME ERP website</p></div></div>
                              <div style=""width: 100%; background-color: #f1f1f1;""><div style=""text-align: center; font-weight: bold;"">You can reach us at</div><div style=""padding: 5px;"">
                              <div>&nbsp;&nbsp;Email:support@acmeerp.org</div><div>&nbsp;&nbsp;http://www.acmeerp.org</div>
                              <div>&nbsp;&nbsp;</div></div></div></div></body></html>";

            }
            return mailContent;
        }
        #region Get First Value in Comma Separated String
        /// <summary>
        /// This method is to get first value from the comma separated string
        /// </summary>
        public static string GetFirstValue(string commaSeparatedString)
        {
            string returnString = commaSeparatedString;
            if ((!string.IsNullOrEmpty(commaSeparatedString) && commaSeparatedString.Contains(Delimiter.Comma)))
            {
                returnString = commaSeparatedString.Substring(0, commaSeparatedString.IndexOf(Delimiter.Comma));
            }
            return returnString;
        }
        #endregion

        #region Remove First Value in Comma Separated String
        /// <summary>
        /// This method is to get first value from the comma separated string
        /// </summary>
        public static string RemoveFirstValue(string commaSeparatedString)
        {
            string returnString = commaSeparatedString;
            if ((!string.IsNullOrEmpty(commaSeparatedString) && commaSeparatedString.Contains(Delimiter.Comma)))
            {
                returnString = commaSeparatedString.Remove(0, commaSeparatedString.IndexOf(Delimiter.Comma) + 1);
            }
            else
            {
                returnString = "";
            }
            return string.IsNullOrEmpty(returnString) ? "" : returnString;
        }
        #endregion

        public static ResultArgs SendSMS(string PhoneNo, string Message)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;
            try
            {
                if (!string.IsNullOrEmpty(PhoneNo) && !string.IsNullOrEmpty(Message))
                {
                    string strUrl = "http://smsboscoit.tk/submitsms.jsp?user=acmeerp&key=18d9ca86c2XX&mobile=" + PhoneNo + " &message=" + Message + "&senderid=BOSNET&accusage=1";
                    //string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=prems.infocom@gmail.com:Infocom@2015&senderID=BOSNET&receipientno=" + PhoneNo + "&msgtxt=" + Message + "&state=4";
                    WebRequest request = HttpWebRequest.Create(strUrl);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = (Stream)response.GetResponseStream();
                    StreamReader readStream = new StreamReader(s);
                    string dataString = readStream.ReadToEnd();
                    response.Close();
                    s.Close();
                    readStream.Close();
                }
                else
                {
                    if (string.IsNullOrEmpty(PhoneNo))
                    {
                        resultArgs.Message = "Phone no is empty";
                    }
                    else
                    {
                        resultArgs.Message = "Message is empty";
                    }
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = err.Message;
            }
            return resultArgs;
        }

        public static ResultArgs SendEmail(string toEmailId, string toCCEmailId, string subject, string message, bool sendMailBCC)
        {
            return SendEmail(string.Empty, toEmailId, toCCEmailId, subject, message, string.Empty, sendMailBCC);
        }

        public static ResultArgs SendEmail(string fromEmailId, string toEmailId, string subject, string message, string attachmentFile, bool sendMailBCC)
        {
            return SendEmail(fromEmailId, toEmailId, string.Empty, subject, message, attachmentFile, sendMailBCC);
        }

        public static ResultArgs SendEmail(string toEmailId, string subject, string attachmentFile, bool sendMailBCC, AlternateView mailview)
        {
            return SendEmail(string.Empty, toEmailId, string.Empty, subject, string.Empty, attachmentFile, sendMailBCC, mailview);
        }

        public static ResultArgs SendEmail(string toEmailId, string subject, bool sendMailBCC, AlternateView mailview, MemoryStream memoryStream, string attachmentName)
        {
            return SendEmail(string.Empty, toEmailId, string.Empty, subject, string.Empty, string.Empty, sendMailBCC, mailview, memoryStream, attachmentName);
        }

        public static ResultArgs SendEmail(string fromEmailId, string toEmailId, string toCCEmailId,
          string subject, string message, string attachmentFile, bool sendMailBCC, AlternateView view = null, MemoryStream memoryStream = null, string attachmentName = "")
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;

            string mailFrom = fromEmailId;
            if (string.IsNullOrEmpty(mailFrom)) { mailFrom = "Acme.erp <" + ConfigurationManager.AppSettings["DefaultSenderEmailId"] + ">"; }

            string mailTo = toEmailId;
            string mailToCC = toCCEmailId;
            string mailToBCC = (sendMailBCC) ? ConfigurationManager.AppSettings["DefaultBCCEmailId"].ToString() : string.Empty;

            if (string.IsNullOrEmpty(subject)) { subject = "Mail from Acme.erp"; }

            if (!string.IsNullOrEmpty(mailFrom) && !string.IsNullOrEmpty(mailTo))
            {
                if (!String.IsNullOrEmpty(message) || view != null) //Check Message content or mail view
                {
                    try
                    {
                        MailMessage mail = new MailMessage();

                        mail.Headers.Add("MIME-Version", "1.0");
                        mail.Headers.Add("Content-Type", "text/html; charset=iso-8859-1");
                        mail.Headers.Add("Content-Type", "image/png; name=logo.png");
                        mail.Headers.Add("Content-Type", "image/png; name=Picture1.png");
                        mail.Headers.Add("Mailed-by", "acmeerp.org");
                        mail.Headers.Add("Signed-by", "acmeerp.org");


                        mail.From = new MailAddress(mailFrom);
                        mail.ReplyTo = new MailAddress("AcME ERP <support@acmeerp.org>");
                        
                        foreach (string mailid in mailTo.Split(','))
                        {
                            mail.To.Add(new MailAddress(mailid));
                        }
                        mail.Subject = subject;
                        mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        mail.Priority = MailPriority.High;

                        //if message is empty, take view as mail content else message as mail content
                        if (!String.IsNullOrEmpty(message))
                        {
                            AlternateView plainView = AlternateView.CreateAlternateViewFromString
                            (Regex.Replace(message, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message, null, "text/html");
                            mail.AlternateViews.Add(plainView);
                            mail.AlternateViews.Add(htmlView);
                        }
                        else
                        {
                            mail.AlternateViews.Add(view);
                        }

                        if (toCCEmailId != "")
                        {
                            foreach (string mailid in toCCEmailId.Split(','))
                            {
                                mail.CC.Add(toCCEmailId);
                            }
                        }

                        if (!string.IsNullOrEmpty(mailToBCC))
                        {
                            foreach (string mailid in mailToBCC.Split(','))
                            {
                                mail.Bcc.Add(mailToBCC);
                            }
                        }

                        if (attachmentFile != string.Empty || memoryStream != null)
                        {
                            if (attachmentFile != string.Empty)
                            {
                                Attachment attachment = new Attachment(attachmentFile);
                                mail.Attachments.Add(attachment);
                            }
                            else if (memoryStream != null)
                            {
                                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                                //System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                                Attachment attachment = new Attachment(memoryStream, !string.IsNullOrEmpty(attachmentName) ? attachmentName : "Attachment", "application/pdf");
                                mail.Attachments.Add(attachment);
                            }
                        }

                        mail.IsBodyHtml = true;

                        //Send the message.
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Timeout = 30000;

                        //0n 29/04/2017, somtimes we get operation timeout error, 
                        //if we get this error, we try to send one more time.
                        try
                        {
                            smtpClient.Send(mail);
                        }
                        catch (SmtpException smptperr)
                        {
                            //OperationTimeedout
                            if (smptperr.StatusCode == SmtpStatusCode.GeneralFailure ||
                                smptperr.StatusCode == SmtpStatusCode.MailboxBusy ||
                                smptperr.StatusCode == SmtpStatusCode.MailboxUnavailable)
                            {
                                smtpClient.Timeout = 100000;

                                smtpClient.Send(mail);
                            }
                        }

                        if (memoryStream != null)
                        {
                            memoryStream.Close();
                            memoryStream.Dispose();
                        }
                    }
                    catch (Exception err)
                    {
                        resultArgs.Message = err.Message;
                    }
                }
                else
                {
                    resultArgs.Message = "Mail content is empty";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(mailFrom))
                {
                    resultArgs.Message = "Sender Email Id not found";
                }
                else
                {
                    resultArgs.Message = "Receiver Email Id not found";
                }
            }

            return resultArgs;
        }


        /// <summary>
        /// If email ids more than one and separated by ,
        /// </summary>
        /// <param name="mailids"></param>
        /// <returns></returns>
        private static MailAddressCollection GetMoreThanOneMailIds(string mailids)
        {
            MailAddressCollection maildidscollection = new MailAddressCollection();
            string[] mailToids = mailids.Split(',');

            foreach (string mailid in mailToids)
            {
                maildidscollection.Add(new MailAddress(mailid));
            }

            return maildidscollection;
        }
    }
}
