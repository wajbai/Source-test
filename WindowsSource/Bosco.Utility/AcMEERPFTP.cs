using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

using System.Windows.Forms;
using System.Dynamic;
using System.Diagnostics;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using System.Collections.Specialized;
using System.Configuration;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Configuration;


namespace Bosco.Utility
{
    public class AcMEERPFTP : IDisposable
    {
        private string host = "";
        private string user = "";
        private string pass = "";
        private FtpWebResponse ftpResponse = null;
        private HttpWebResponse httpResponse = null;
        private Stream ftpStream = null;
        private Stream httpStream = null;
        private int bufferSize = 2048;
        public event EventHandler<ProgressStatusEventArgs> OnProgress;
        public long FileSize = 0;

        /* Construct Object */
        public AcMEERPFTP()
        {
            //host = @"ftp://acmeerp.org/";
            //user = "testftp";
            //pass = "Nl_81x9x7";

            host = @"ftp://acmeerp.org/";
            user = "acmeerp";
            pass = "Rt6yx00!";
        }

        public AcMEERPFTP(string hostIP, string userName, string password)
        {
            host = hostIP;
            user = userName;
            pass = password;
        }

        /* Download File */
        public ResultArgs download(string remoteFile, string localFile)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = GetUpdaterFileSizebyFTP(remoteFile);
                if (resultarg.Success)
                {
                    /* Create an FTP Request */
                    FtpWebRequest ftpRequest = CreateAnFTPwebrequest(remoteFile);

                    /* Specify the Type of FTP Request */
                    ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    /* Establish Return Communication with the FTP Server */
                    using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                    {
                        /* Get the FTP Server's Response Stream */
                        using (ftpStream = ftpResponse.GetResponseStream())
                        {
                            /* Open a File Stream to Write the Downloaded File */
                            using (FileStream localFileStream = new FileStream(localFile, FileMode.Create))
                            {
                                /* Buffer for the Downloaded Data */
                                byte[] byteBuffer = new byte[bufferSize];
                                int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                                /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
                                long sofordownloaded = 0;
                                while (bytesRead > 0)
                                {
                                    localFileStream.Write(byteBuffer, 0, bytesRead);
                                    bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                                    if (OnProgress != null)
                                    {
                                        sofordownloaded += bytesRead;
                                        ProgressStatusEventArgs args = new ProgressStatusEventArgs();
                                        args.Status = "<COLOR='BLUE'> Downloading Updates - " + BytesToString(sofordownloaded) + " of " + BytesToString(FileSize);
                                        Application.DoEvents();
                                        OnProgress(this, args);
                                    }
                                }

                                /* Resource Cleanup */
                                localFileStream.Close();
                                ftpStream.Close();
                                ftpResponse.Close();
                                ftpRequest = null;
                                resultarg.Success = true;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(resultarg.Message);
                }
            }
            catch (WebException ex)
            {
                resultarg.Success = false;
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    resultarg.Message = "Check FTP download rights. Contact your System Administrator";
                }
                else
                {
                    response.Close();
                    resultarg.Message = ex.ToString();
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                if (ex.Message.Contains("File unavailable"))
                {
                    resultarg.Message = "AcMEERP Updater is not found";
                }
                else
                {
                    resultarg.Message = ex.ToString();
                }
            }
            return resultarg;
        }

        public ResultArgs downloadhttp(string remoteFile, string localFile)
        {
            ResultArgs resultarg = new ResultArgs();
            //using (WebClient webClient = new WebClient())
            //{
            //    webClient.Proxy = GetAcmeerpProxy();
            //    webClient.DownloadFile(remoteFile, localFile);
            //}
            //resultarg.Success = true;
            //return resultarg;

            try
            {
                resultarg = GetUpdaterFileSizebyHTTP(remoteFile);
                if (resultarg.Success)
                {
                    /* Create an FTP Request */
                    HttpWebRequest httpRequest = CreateAnHTTPwebrequest(remoteFile);

                    /* Specify the Type of FTP Request */
                    httpRequest.Method = WebRequestMethods.File.DownloadFile;
                    /* Establish Return Communication with the FTP Server */
                    using (httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                    {
                        /* Get the FTP Server's Response Stream */
                        using (httpStream = httpResponse.GetResponseStream())
                        {
                            /* Open a File Stream to Write the Downloaded File */
                            using (FileStream localFileStream = new FileStream(localFile, FileMode.Create))
                            {
                                /* Buffer for the Downloaded Data */
                                byte[] byteBuffer = new byte[bufferSize];
                                int bytesRead = httpStream.Read(byteBuffer, 0, bufferSize);
                                /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
                                long sofordownloaded = 0;
                                while (bytesRead > 0)
                                {
                                    localFileStream.Write(byteBuffer, 0, bytesRead);
                                    bytesRead = httpStream.Read(byteBuffer, 0, bufferSize);
                                    if (OnProgress != null)
                                    {
                                        sofordownloaded += bytesRead;
                                        ProgressStatusEventArgs args = new ProgressStatusEventArgs();
                                        args.Status = "<COLOR='BLUE'> Downloading Updates - " + BytesToString(sofordownloaded) + " of " + BytesToString(FileSize);
                                        Application.DoEvents();
                                        OnProgress(this, args);
                                    }
                                }

                                /* Resource Cleanup */
                                localFileStream.Close();
                                httpStream.Close();
                                httpResponse.Close();
                                httpRequest = null;
                                resultarg.Success = true;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(resultarg.Message);
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                if (ex.Message.Contains("File unavailable"))
                {
                    resultarg.Message = "AcMEERP Updater is not found";
                }
                else
                {
                    resultarg.Message = ex.ToString();
                }
            }
            return resultarg;
        }

        /* Upload File */
        public ResultArgs upload(string remoteFile, string localFile)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest(remoteFile);

                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                using (ftpStream = ftpRequest.GetRequestStream())
                {
                    /* Open a File Stream to Read the File for Upload */
                    using (FileStream localFileStream = File.OpenRead(localFile)) // new FileStream(localFile, FileMode.o))
                    {
                        /* Buffer for the Downloaded Data */
                        byte[] byteBuffer = new byte[bufferSize];
                        int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                        /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                        long soforuploaded = 0;
                        while (bytesSent != 0)
                        {
                            ftpStream.Write(byteBuffer, 0, bytesSent);
                            bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                            if (OnProgress != null)
                            {
                                soforuploaded += bytesSent;
                                ProgressStatusEventArgs args = new ProgressStatusEventArgs();
                                args.FileLength = localFileStream.Length;
                                args.ByteSent = soforuploaded.ToString();
                                args.Status = "Uploading Vouchers <COLOR='BLUE'>" + BytesToString(soforuploaded) + "</COLOR> of <COLOR='BLUE'>" + BytesToString(localFileStream.Length);
                                Application.DoEvents();
                                OnProgress(this, args);
                            }
                        }
                        /* Resource Cleanup */
                        localFileStream.Close();
                        ftpStream.Close();
                        ftpRequest = null;
                        resultarg.Success = true;
                    }
                }
            }
            catch (WebException ex)
            {
                resultarg.Success = false;
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    resultarg.Message = "Check FTP upload rights. Contact your System Administrator";
                }
                else
                {
                    response.Close();
                    resultarg.Message = ex.ToString();
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;
        }

        ///* Upload File */
        //public ResultArgs uploadbyHTTP(string remoteFile, string localFile)
        //{
        //    ResultArgs resultarg = new ResultArgs();
        //    try
        //    {
        //        //WebClient Client = new WebClient();
        //        //Client.Credentials = CredentialCache.DefaultCredentials;
        //        //Client.Proxy = GetAcmeerpProxy();
        //        //Client.UploadFile("http://acmeerp.org/Module/Software/Uploads/" + remoteFile, localFile);

        //        NameValueCollection nvc = new NameValueCollection();
        //        nvc.Add("id", "TTR");
        //        nvc.Add("btn-submit-photo", "Upload");
        //        //HttpUploadFile(@"http://acmeerp.org/module/Software/Uploads/Acmeerp_Vouchers/kalis-CRs.txt", @"C:\Users\Alwar\Desktop\kalis-CRs.txt", "file", "text/xml", nvc);
        //        UploadFile(@"C:\Users\Alwar\Desktop\kalis-CRs.txt", @"http://acmeerp.org/module/Software/Uploads/Acmeerp_Vouchers/kalis-CRs.txt");
        //        resultarg.Success = true;    
        //    }
        //    catch (Exception ex)
        //    {
        //        resultarg.Success = false;
        //        resultarg.Message = ex.ToString();
        //    }
        //    return resultarg;
        //}


        /* Get AcMEERP Server Date and Time */
        public ResultArgs GetAcMEERPServerDateTime()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                ///* Create an FTP Request */
                //FtpWebRequest ftpRequest = CreateAnFTPwebrequest("httpdocs/Module/Software/Uploads/acmeerpversion.txt");

                ///* Specify the Type of FTP Request */
                //ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                ///* Establish Return Communication with the FTP Server */
                //using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                //{
                //    using (TextReader Reader = new StringReader(ftpResponse.StatusDescription))
                //    {
                //        string DateString = Reader.ReadLine().Substring(4);
                //        DateTime DateValue = DateTime.ParseExact(DateString, "yyyyMMddHHmmss", CultureInfo.InvariantCulture.DateTimeFormat);
                //    }
                //}
                Utility.CommonMemberSet.DateSetMember dateset = new CommonMemberSet.DateSetMember();
                resultarg.ReturnValue = dateset.ToCurrentDateTime("dd/MM/yyyy h:mm:ss tt"); //(true);// DateTime.Now;
                resultarg.Success = true;
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }

            return resultarg;
        }


        /* Upload File */
        public ResultArgs uploadAcpERPDataBase(string remoteFile, string localFile)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest(remoteFile);

                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                using (ftpStream = ftpRequest.GetRequestStream())
                {
                    /* Open a File Stream to Read the File for Upload */
                    using (FileStream localFileStream = File.OpenRead(localFile)) // new FileStream(localFile, FileMode.o))
                    {
                        /* Buffer for the Downloaded Data */
                        byte[] byteBuffer = new byte[bufferSize];
                        int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                        /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                        long soforuploaded = 0;
                        while (bytesSent != 0)
                        {
                            ftpStream.Write(byteBuffer, 0, bytesSent);
                            bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                            if (OnProgress != null)
                            {
                                soforuploaded += bytesSent;
                                ProgressStatusEventArgs args = new ProgressStatusEventArgs();
                                args.FileLength = localFileStream.Length;
                                args.ByteSent = soforuploaded.ToString();
                                args.Status = "Uploading Database <COLOR='BLUE'>" + BytesToString(soforuploaded) + "</COLOR> of <COLOR='BLUE'>" + BytesToString(localFileStream.Length);
                                Application.DoEvents();
                                OnProgress(this, args);
                            }
                        }
                        /* Resource Cleanup */
                        localFileStream.Close();
                        ftpStream.Close();
                        ftpRequest = null;
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;
        }

        /* Upload File */
        public ResultArgs uploadfilebyFTP(string remoteFile, MemoryStream memoryStream)
        {
            ResultArgs resultarg = new ResultArgs();
            FtpWebRequest ftpRequest = null;
            try
            {
                /* Create an FTP Request */
                ftpRequest = CreateAnFTPwebrequest(remoteFile);

                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                using (ftpStream = ftpRequest.GetRequestStream())
                {
                    /* Open a File Stream to Read the File for Upload */
                    /* Buffer for the Upload Data */
                    byte[] byteBuffer = new byte[bufferSize];
                    memoryStream.Position = 0;
                    int bytesSent = memoryStream.Read(byteBuffer, 0, bufferSize);
                    /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                    long soforuploaded = 0;
                    while (bytesSent != 0)
                    {
                        ftpStream.Write(byteBuffer, 0, bytesSent);
                        bytesSent = memoryStream.Read(byteBuffer, 0, bufferSize);
                        if (OnProgress != null)
                        {
                            soforuploaded += bytesSent;
                            ProgressStatusEventArgs args = new ProgressStatusEventArgs();
                            args.FileLength = memoryStream.Length;
                            args.ByteSent = soforuploaded.ToString();
                            args.Status = "Uploading file <COLOR='BLUE'>" + BytesToString(soforuploaded) + "</COLOR> of <COLOR='BLUE'>" + BytesToString(memoryStream.Length);
                            Application.DoEvents();
                            OnProgress(this, args);
                        }
                    }
                    resultarg.Success = true;
                }
            }
            catch (WebException webex)
            {
                resultarg.Success = false;
                if (webex.Message.Contains("(550) File unavailable"))
                {
                    resultarg.Message = "Upload folder is not available in Acme.erp portal";
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.Message;
            }
            finally
            {
                /* Resource Cleanup */
                if (ftpStream != null) ftpStream.Close();
                ftpRequest = null;
            }
            return resultarg;
        }


        /// <summary>
        /// On 24/03/2023, To upload files/memory stream to Acme.erp portal
        /// </summary>
        /// <param name="branchuploadaction"></param>
        /// <param name="hoadofficeCode"></param>
        /// <param name="branchofficepartCode"></param>
        /// <param name="branchlocation"></param>
        /// <param name="filenameinserver"></param>
        /// <param name="filedescription"></param>
        /// <param name="currentfy"></param>
        /// <param name="ms"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public ResultArgs uploadDatabyWebClient(BranchUploadAction branchuploadaction, string hoadofficeCode, string branchofficepartCode, string branchlocation,
                        string filenameinserver, string filedescription, string currentfy, MemoryStream ms = null, string filename = "")
        {
            ResultArgs result = new ResultArgs();
            SettingProperty settingproperties = new SettingProperty();
            string responsemsg = string.Empty;
            bool uploadbydata = false;
            bool uploadbyfile = false;

            try
            {

                if (!settingproperties.IsLicenseKeyMismatchedByLicenseKeyDBLocation)
                {
                    if (!settingproperties.IsLicenseKeyMismatchedByHoProjects)
                    {
                        string uploadWebUrl = GetAcmeerpPortalURL();
                        uploadWebUrl += "/Module/Software/BranchSync.aspx";
                        if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                        {
                            uploadbyfile = true;
                        }
                        else if (ms != null)
                        {
                            uploadbydata = true;
                        }

                        if (uploadbydata || uploadbyfile)
                        {
                            using (WebClient client = new WebClient())
                            {
                                client.Headers.Add("HeadOfficeCode", hoadofficeCode);
                                client.Headers.Add("BranchOfficeCode", branchofficepartCode);
                                client.Headers.Add("BranchLocation", branchlocation);
                                client.Headers.Add("BranchAction", branchuploadaction.ToString());
                                client.Headers.Add("FileName", filenameinserver);
                                client.Headers.Add("FileDescription", filedescription);
                                client.Headers.Add("CurrentFY", currentfy);

                                if (uploadbyfile)
                                {
                                    //client.UploadProgressChanged += new UploadProgressChangedEventHandler(client_UploadProgressChanged);
                                    byte[] response = client.UploadFile(uploadWebUrl, filenameinserver);
                                }
                                else if (ms != null)
                                {
                                    byte[] uploadfilebyte = ms.ToArray();
                                    byte[] response = client.UploadData(uploadWebUrl, uploadfilebyte);
                                    responsemsg = System.Text.Encoding.ASCII.GetString(response);
                                    ms.Close();
                                    ms = null;
                                }
                            }
                        }
                        else
                        {
                            responsemsg = "Invalid Data/File is not exists to upload to Acme.erp portal";
                        }
                    }
                    else
                    {
                        responsemsg = "Acme.erp License key and Database are mismatching (Branch Office Projects and Head Office Projects are mismatching)";
                    }
                }
                else
                {
                    responsemsg = "License key Location(s) and Database Location are mismatching, Check your License Key";
                }
            }
            catch (WebException ex)
            {
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(responsemsg))
                {
                    result.Message = responsemsg;
                }
            }
            return result;
        }

        private void client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {

        }

        public ResultArgs DownloadDataByWebClient(string remoteFileName, string LocalFilePath)
        {
            ResultArgs result = new ResultArgs();
            SettingProperty settingproperties = new SettingProperty();
            string responsemsg = string.Empty;
            bool uploadbydata = false;
            bool uploadbyfile = false;

            try
            {

                if (!settingproperties.IsLicenseKeyMismatchedByLicenseKeyDBLocation)
                {
                    if (!settingproperties.IsLicenseKeyMismatchedByHoProjects)
                    {
                        string downloadWebUrl = GetAcmeerpPortalURL();
                        downloadWebUrl += "/Module/Software/Uploads/Acmeerp_Branch_Reports/" + settingproperties.HeadofficeCode + "/2024/" + remoteFileName;
                        using (WebClient client = new WebClient())
                        {
                            using (Stream stream = client.OpenRead(downloadWebUrl))
                            {
                                //client.DownloadFile(downloadWebUrl, LocalFilePath);

                                using (FileStream outputFileStream = new FileStream(LocalFilePath, FileMode.Create))
                                {
                                    stream.CopyTo(outputFileStream);
                                }
                            }
                        }
                    }
                    else
                    {
                        responsemsg = "Acme.erp License key and Database are mismatching (Branch Office Projects and Head Office Projects are mismatching)";
                    }
                }
                else
                {
                    responsemsg = "License key Location(s) and Database Location are mismatching, Check your License Key";
                }
            }
            catch (WebException ex)
            {
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(responsemsg))
                {
                    result.Message = responsemsg;
                }
            }
            return result;
        }

        public ResultArgs DownloadManualsByWebClient()
        {
            ResultArgs result = new ResultArgs();
            string responsemsg = string.Empty;

            try
            {
                string downloadWebUrl = GetAcmeerpPortalURL();
                downloadWebUrl += "/Module/Manual/User Manual/UserManualAndPaidFeatures.xml";
                using (WebClient client = new WebClient())
                {
                    using (Stream stream = client.OpenRead(downloadWebUrl))
                    {
                        //client.DownloadFile(downloadWebUrl, LocalFilePath);
                        if (stream != null)
                        {
                            using (DataSet ds = new DataSet())
                            {
                                ds.ReadXml(stream);
                                if (ds.Tables.Count > 0)
                                {
                                    result.DataSource.Data = ds.Tables[0];
                                    result.Success = true;
                                }
                                else
                                {
                                    result.Message = "User Manual not available";
                                }
                            }
                        }
                        else
                        {
                            result.Message = "User Manual not available";
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(responsemsg))
                {
                    result.Message = responsemsg;
                }
            }
            return result;
        }

        public ResultArgs DownloadManualFileByWebClient(string filename)
        {
            ResultArgs result = new ResultArgs();
            string responsemsg = string.Empty;

            try
            {
                string downloadWebUrl = GetAcmeerpPortalURL();
                downloadWebUrl += "/Module/Manual/User Manual/" + filename;
                string LocalFilePath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_UserManuals);
                LocalFilePath = Path.Combine(LocalFilePath, filename);
                using (WebClient client = new WebClient())
                {
                    using (Stream stream = client.OpenRead(downloadWebUrl))
                    {
                        //client.DownloadFile(downloadWebUrl, LocalFilePath);
                        if (stream != null)
                        {
                            using (FileStream outputFileStream = new FileStream(LocalFilePath, FileMode.Create))
                            {
                                stream.CopyTo(outputFileStream);
                            }
                        }
                        else
                        {
                            result.Message = "User Manual not available";
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(responsemsg))
                {
                    result.Message = responsemsg;
                }
            }
            return result;
        }

        public string GetAcmeerpPortalURL()
        {
            string rtn = "http://acmeerp.org/";
            try
            {
                ClientSection serviceModelClientConfigSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
                foreach (ChannelEndpointElement endpoint in serviceModelClientConfigSection.Endpoints)
                {
                    if (endpoint.Address.AbsoluteUri.Contains("acmeerp.org"))
                    {
                        Uri uri = endpoint.Address;
                        rtn = uri.GetLeftPart(System.UriPartial.Authority);
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return rtn;
        }

        /* Delete File */
        public ResultArgs delete(string deleteFile)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest(deleteFile);
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                /* Establish Return Communication with the FTP Server */
                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    /* Resource Cleanup */
                    ftpResponse.Close();
                    ftpRequest = null;
                    resultarg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        /* Rename File */
        public ResultArgs rename(string currentFileNameAndPath, string newFileName)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest(currentFileNameAndPath);
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                /* Rename the File */
                ftpRequest.RenameTo = newFileName;
                /* Establish Return Communication with the FTP Server */
                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    /* Resource Cleanup */
                    ftpResponse.Close();
                    ftpRequest = null;
                    resultarg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();

            }
            return resultarg;
        }

        /* Does FTP Directory exist*/
        public bool DoesFtpDirectoryExist(string dirPath)
        {
            try
            {
                FtpWebRequest request = CreateAnFTPwebrequest(dirPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// To get List of available Module Name,Template Name and Template file source path
        /// </summary>
        /// <param name="ModuleList">List of available modules</param>
        /// <returns></returns>
        //Added by Carmel Raj 
        public List<dynamic> DownloadTemplates(List<string> ModuleList)
        {
            string rootPath = "httpdocs/Module/Software/Uploads/AcMEERPTemplate/";
            dynamic t = new ExpandoObject();
            List<dynamic> TemplateList = new List<dynamic>();
            try
            {
                foreach (string Mod in ModuleList)
                {
                    var requestModule = CreateAnFTPwebrequest(Path.Combine(rootPath, Mod));
                    requestModule.Credentials = new NetworkCredential(user, pass);
                    requestModule.Method = WebRequestMethods.Ftp.ListDirectory;
                    using (var responseModule = (FtpWebResponse)requestModule.GetResponse())
                    {
                        using (var streamModule = responseModule.GetResponseStream())
                        {
                            using (var readerModule = new StreamReader(streamModule, true))
                            {
                                var FileName = readerModule.ReadLine();
                                if (FileName != null)
                                {
                                    //Creating anonymous type
                                    var AvailableTemplates = new
                                    {
                                        Module = Mod,
                                        Name = FileName,
                                        SourcePath = Path.Combine(Path.Combine(rootPath, Mod), FileName)
                                    };
                                    TemplateList.Add(AvailableTemplates);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return TemplateList;
        }

        /// <summary>
        /// To Applying filter
        /// Since dynamic type returns internal as accessmodifer filtering can only be done in the same assembly
        /// </summary>
        /// <param name="source">List of all the available Template</param>
        /// <param name="Condition">Filter conditon</param>
        /// <returns>Filtered Template List</returns>

        //Added by Carmel Raj M
        public List<dynamic> ApplyFilter(List<dynamic> source, string Condition)
        {
            var SelectedModule = (from m in source.ToList()
                                  where m.Module == Condition
                                  select m).ToList();
            return SelectedModule;
        }

        /* Does FTP file exist */
        public ResultArgs CheckIfFileExistsbyFTP()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest("httpdocs/Module/Software/Uploads/acmeerpversion.txt");  //remoteFile
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory; //WebRequestMethods.Ftp.GetFileSize 
                /* Establish Return Communication with the FTP Server */
                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    /* Get File size */
                    FileSize = ftpResponse.ContentLength;
                    /* Resource Cleanup */
                    ftpResponse.Close();
                    ftpRequest = null;
                    resultarg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;

        }

        /* Does HTTP file exist */
        public ResultArgs CheckIfFileExistsbyHTTP()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                HttpWebRequest httpRequest = CreateAnHTTPwebrequest("http://acmeerp.org/Module/Software/Uploads/acmeerpversion.txt");  //remoteFile

                /* Specify the Type of FTP Request */
                httpRequest.Method = WebRequestMethods.Http.Get;  //. .Ftp.GetFileSize;
                /* Establish Return Communication with the FTP Server */
                using (httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    /* Get File size */
                    FileSize = httpResponse.ContentLength;
                    /* Resource Cleanup */

                    httpResponse.Close();
                    httpResponse = null;
                    resultarg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;

        }

        /* Create a New Directory on the FTP Server */
        public void createDirectory(string newDirectory)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest(newDirectory);
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                /* Establish Return Communication with the FTP Server */
                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    /* Resource Cleanup */
                    ftpResponse.Close();
                    ftpRequest = null;
                    resultarg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return;
        }

        private FtpWebRequest CreateAnFTPwebrequest(string remoteFile)
        {
            string remotefilepath = Path.Combine(host, remoteFile);
            /* Create an FTP Request */
            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(remotefilepath);

            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = false;
            ftpRequest.Proxy = null;
            ftpRequest.UseBinary = false;
            ftpRequest.Timeout = 90000;
            return ftpRequest;
        }

        private HttpWebRequest CreateAnHTTPwebrequest(string remoteFile)
        {
            // string remotefilepath = Path.Combine(host, remoteFile);
            /* Create an FTP Request */
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(remoteFile);

            /* Log in to the FTP Server with the User Name and Password Provided */
            // httpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            //httpRequest.use = true;
            httpRequest.KeepAlive = false;
            httpRequest.Proxy = null;
            //httpRequest.Proxy = GetAcmeerpProxy();
            //httpRequest.Proxy = GetAcmeerpProxy(); //30/08/2017, set proxy
            //httpRequest.use= false;
            httpRequest.Timeout = 90000;
            return httpRequest;
        }

        private string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
        }

        public virtual void Dispose()
        {
            GC.Collect();
        }

        /*Get File size of the AcME Updater */
        public ResultArgs GetUpdaterFileSizebyFTP(string remoteFile)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest(remoteFile);
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory; //WebRequestMethods.Ftp.GetFileSize
                /* Establish Return Communication with the FTP Server */
                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    /* Get File size */
                    FileSize = ftpResponse.ContentLength;
                    if (FileSize <= 0) //On 02/02/2018, sometimes files size is could not be received from server by FTP, so we fix it
                    {
                        FileSize = GetLatestUpdaterFileSize();
                    }
                    /* Resource Cleanup */
                    ftpResponse.Close();
                    ftpRequest = null;
                    resultarg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;
        }

        /*Get File size of the AcME Updater */
        public ResultArgs GetUpdaterFileSizebyHTTP(string remoteFile)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                /* Create an FTP Request */
                HttpWebRequest httpRequest = CreateAnHTTPwebrequest(remoteFile);
                /* Specify the Type of FTP Request */
                httpRequest.Method = WebRequestMethods.Http.Get;
                /* Establish Return Communication with the FTP Server */
                using (httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    /* Get File size */
                    FileSize = httpResponse.ContentLength;
                    /* Resource Cleanup */
                    httpResponse.Close();
                    httpRequest = null;
                    resultarg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;
        }

        public long GetLatestUpdaterFileSize()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = GetUpdateDetailsFromPortal(false);
                if (resultarg.Success)
                {
                    long.TryParse(resultarg.ReturnValue.ToString(), out FileSize);
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return FileSize;
        }


        /// <summary>
        /// Get List of features from portal
        /// </summary>
        /// <param name="isGetFeatureList"></param>
        /// <returns></returns>
        public ResultArgs GetLatestUpdaterFeaturesFromPortal()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = GetUpdateDetailsFromPortal(true);
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;
        }

        /// <summary>
        /// This method is used to get list of featues from portal or get latest updater file size,
        /// 
        /// For sometime, we could not get file size or we fix it in acmeerpversion.txt
        /// </summary>
        /// <param name="isGetFeatureList"> if it is true get list of new features or return latest updtaer file size </param>
        /// <returns></returns>
        private ResultArgs GetUpdateDetailsFromPortal(bool isGetFeatureList)
        {
            string rtn = string.Empty;
            ResultArgs resultarg = new ResultArgs();
            try
            {
                Application.DoEvents();
                if (File.Exists("newfeaturesinportal.txt"))
                {
                    using (var file = new FileStream("newfeaturesinportal.txt", FileMode.Truncate))
                    {
                        file.Close();
                    }
                }

                /* Create an FTP Request */
                FtpWebRequest ftpRequest = CreateAnFTPwebrequest("httpdocs/Module/Software/Uploads/acmeerpversion.txt");

                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                /* Establish Return Communication with the FTP Server */
                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    /* Get the FTP Server's Response Stream */
                    using (httpStream = ftpResponse.GetResponseStream())
                    {
                        /* Open a File Stream to Write the Downloaded File */
                        using (FileStream localFileStream = new FileStream("newfeaturesinportal.txt", FileMode.Create))
                        {
                            /* Buffer for the Downloaded Data */
                            byte[] byteBuffer = new byte[bufferSize];
                            int bytesRead = httpStream.Read(byteBuffer, 0, bufferSize);
                            /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
                            while (bytesRead > 0)
                            {
                                localFileStream.Write(byteBuffer, 0, bytesRead);
                                bytesRead = httpStream.Read(byteBuffer, 0, bufferSize);
                            }
                            /* Resource Cleanup */
                            localFileStream.Close();
                            httpStream.Close();
                            ftpResponse.Close();

                            if (File.Exists("newfeaturesinportal.txt"))
                            {
                                using (StreamReader strmreader = new StreamReader("newfeaturesinportal.txt"))
                                {
                                    if (isGetFeatureList)
                                    {
                                        rtn = strmreader.ReadToEnd();
                                        rtn = rtn.Substring(rtn.IndexOf("\r\n") + "\r\n".Length);
                                    }
                                    else
                                    {
                                        rtn = strmreader.ReadLine();
                                    }
                                }
                            }

                            ftpRequest = null;
                            resultarg.ReturnValue = rtn;
                            resultarg.Success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultarg.Success = false;
                resultarg.Message = ex.ToString();
            }
            return resultarg;
        }

        private WebProxy GetAcmeerpProxy()
        {
            WebProxy proxy = null;
            proxy = new WebProxy("192.168.1.21", 8080);
            /*if (SettingProperty.Current.ProxyUse == "1")
            {
                proxy = new WebProxy();
                string proxyport = (String.IsNullOrEmpty(SettingProperty.Current.ProxyPort) ? string.Empty : ":" + SettingProperty.Current.ProxyPort);
                proxy.Address = new Uri("http://" + SettingProperty.Current.ProxyAddress + proxyport);
                if (SettingProperty.Current.ProxyAuthenticationUse == "1")
                {
                    proxy.Credentials = new NetworkCredential(SettingProperty.Current.ProxyUserName, SettingProperty.Current.ProxyPassword);
                }
            }*/
            return proxy;
        }

        public void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);

            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "PUT";
            wr.KeepAlive = true;
            //wr.UseDefaultCredentials = true;
            //wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //wr.Credentials = new System.Net.NetworkCredential(user, pass);          
            //wr.PreAuthenticate = true;
            //wr.Credentials = CredentialCache.DefaultCredentials;

            //wr.UseDefaultCredentials = true;
            //wr.PreAuthenticate = true;
            //wr.Credentials = CredentialCache.DefaultCredentials;
            //wr.Credentials = new NetworkCredential("Administrator", "Mn*(oikJHUYr#EwQ!K"); 

            //string base64Credentials = "";// GetEncodedCredentials();
            //wr.Headers.Add("Authorization", "Basic " + base64Credentials);
            string privateKey = "acmeerpbranch";
            byte[] authBytes = Encoding.UTF8.GetBytes(privateKey);
            wr.PreAuthenticate = true;
            //wr.Headers.Add("Authorization", "Basic " + Base64.EncodeToString(authBytes, Base64Flags.Default));

            wr.Proxy = GetAcmeerpProxy();
            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();
            rs = null;

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                wr = null;
            }
        }

        public bool UploadFile(string localFile, string uploadUrl)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uploadUrl);

            try
            {
                req.Method = "PUT";
                req.AllowWriteStreamBuffering = true;
                req.UseDefaultCredentials = true;
                req.Credentials = System.Net.CredentialCache.DefaultCredentials;
                req.SendChunked = false;
                req.KeepAlive = true;
                Stream reqStream = req.GetRequestStream();
                FileStream rdr = new FileStream(localFile, FileMode.Open, FileAccess.Read);
                byte[] inData = new byte[4096];
                int bytesRead = rdr.Read(inData, 0, inData.Length);

                while (bytesRead > 0)
                {
                    reqStream.Write(inData, 0, bytesRead);
                    bytesRead = rdr.Read(inData, 0, inData.Length);
                }

                reqStream.Close();
                rdr.Close();

                System.Net.HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
                {
                    MessageBox.Show("Couldn't upload file");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
    }

    public class ProgressStatusEventArgs : EventArgs
    {
        public string Status { get; set; }
        public long FileLength { get; set; }
        public string ByteSent { get; set; }
    }
}
