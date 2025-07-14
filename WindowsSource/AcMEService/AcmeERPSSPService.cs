using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proshot.UtilityLib.CommonDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DevExpress.XtraSplashScreen;
using Bosco.Utility.Base;
using Newtonsoft.Json.Linq;
using System.Data;
using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using Bosco.Utility.ConfigSetting;
using System.IO;
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using System.Windows.Forms;
using System.Net;
using System.Configuration;


namespace AcMEService
{
    public class AcmeERPSSPService : IAcmeERPSSPService
    {
        #region Variable
        AcMEService.SSPAcmeIntegration.SSPAcmeIntegrationSoapClient objService = new AcMEService.SSPAcmeIntegration.SSPAcmeIntegrationSoapClient();
        SSPAcmeIntegration.AuthHeader objAuthHead = new SSPAcmeIntegration.AuthHeader();
        ResultArgs resultArgs = new ResultArgs();

        string HiGradeURL = SettingProperty.ThirdPartyURL;
        // ConfigurationManager.AppSettings[AppSettingName.HiGradeURL.ToString()].ToString();

        SettingProperty setting = new SettingProperty();

        #endregion

        #region Properties

        private string AcMEUserName = "test";
        private string AcMEPassword = "test";

        #endregion

        #region Methods

        /// <summary>
        /// Construct the Source Methods
        /// </summary>
        /// <param name="dtConstructDatasource"></param>
        /// <returns></returns>
        public string ConstructMasters(DataSet dtConstructDatasource)
        {
            string Jsonscript = ConvertDatasetToJson(dtConstructDatasource);
            return Jsonscript;
        }

        /// <summary>
        /// Post Master details
        /// </summary>
        /// <param name="ManagementCode"></param>
        /// <param name="JsonScript"></param>
        /// <returns></returns>
        public string PostMaster(string ManagementCode, string JsonScriptMaster)
        {
            string status = "";
            try
            {
                objAuthHead.UserName = AcMEUserName;
                objAuthHead.Password = AcMEPassword;

                AcMELog.WriteLog("Started Post Master..");
                status = objService.GetMasters(objAuthHead, ManagementCode, JsonScriptMaster);
            }
            catch (Exception ex)
            {
                status = "Status: Could not proceed. Post Master.." + ex.Message;
            }
            return status;
        }

        /// <summary>
        /// Post Master details
        /// </summary>
        /// <param name="ManagementCode"></param>
        /// <param name="JsonScript"></param>
        /// <returns></returns>
        public string APIPostMaster(string ManagementCode, string JsonScriptMaster)
        {
            string status = "";

            string Data = "{\"params\": " + JsonScriptMaster + "}";
            string urlName = string.Empty;
            // string urlName = string.Format("http://testing.higrade.live:8069/api/acme/configuration/data");
            // string urlName = string.Format("http://192.168.1.83:8000/api/acme/configuration/data");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            if (!setting.IS_SDB_RMG)
            {
                urlName = string.Format(HiGradeURL + "api/acme/configuration/data");
            }
            else
            {
                //urlName = string.Format("https://7b2a-182-60-21-40.ngrok-free.app/api/acme/store/data");
                // urlName = string.Format("https://6b43-182-60-21-40.ngrok-free.app/api/acme/store/data");
                //urlName = string.Format("https://udeg.demoboscosoft.com/testing/public/api/acme/store/data");
                urlName = string.Format("https://economato.sdb.org/api/acme/store/data");
            }

            try
            {
                //Request
                WebRequest RequestService = WebRequest.Create(urlName);
                RequestService.Method = "POST";
                RequestService.ContentType = "application/json";

                using (var StreamReceivedData = new StreamWriter(RequestService.GetRequestStream()))
                {
                    StreamReceivedData.Write(Data);
                    StreamReceivedData.Flush();
                    StreamReceivedData.Close();
                }

                // Response
                WebResponse ResponseData = RequestService.GetResponse();
                string resposne = (((HttpWebResponse)ResponseData).StatusDescription);
                Stream Sr = ResponseData.GetResponseStream();
                using (StreamReader SR = new StreamReader(Sr))
                {
                    status = SR.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog("Could not proceed" + ex.Message);
                status = "Status: Could not proceed. Get Voucher..." + ex.Message;
            }
            return status;
        }

        /// <summary>
        /// Get Voucher
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="ManagementCode"></param>
        /// <param name="JsonScriptMaster"></param>
        /// <returns></returns>
        public string GetVoucher(string DateFrom, string DateTo, string ManagementCode, string JsonScriptMaster, string JsonFilterProjectMaster)
        {
            string status = "";
            // DateFrom = "01/09/2022";
            // DateTo = "07/09/2022";

            // StreamReader reder = new StreamReader(@"c:\json7.txt");
            // status = reder.ReadToEnd();

            try
            {
                objAuthHead.UserName = AcMEUserName;
                objAuthHead.Password = AcMEPassword;

                AcMELog.WriteLog("Started Get Voucher..");
                status = objService.GetTrasnsactionDetails(objAuthHead, ManagementCode, DateFrom, DateTo, JsonScriptMaster, JsonFilterProjectMaster);

                //status = @"{'Vouchers:[{'SOURCE':'SSP','VOUCHER_DATE':'01/09/2022','VOUCHER_TYPE':'RC','CLIENT_CODE':'DSAVIOTPT','CLIENT_REFERENCE_ID':'484','PROJECT_NAME':'Don Bosco Centre (SISS) - Yellagiri Hills','LEDGER':'Admission Fees','LEDGER_AMOUNT':43000,'LEDGER_TRANS_MODE':'CR','CASHBANK_LEDGER':'Cash','CASHBANK_AMOUNT':43000,'CASHBANK_TRANS_MODE':'DR','FLAG':'CASH','MATERIALIZED_ON':'','BANK_REF_NO':'','Narration':'','TRANSACTION_ID':'0@484','CLIENT_TRANSACTION_MODE':'Cash','RECEIPTNO':' 2022 (R.1021,1031,424,428,494,529,560,618,767,768,786,802,832,974 )'},{'SOURCE':'SSP','VOUCHER_DATE':'03/09/2022','VOUCHER_TYPE':'RC','CLIENT_CODE':'DSAVIOTPT','CLIENT_REFERENCE_ID':'2107','PROJECT_NAME':'Don Bosco Centre (SISS) - Yellagiri Hills','LEDGER':'Admission Fees','LEDGER_AMOUNT':3000,'LEDGER_TRANS_MODE':'CR','CASHBANK_LEDGER':'000001','CASHBANK_AMOUNT':3000,'CASHBANK_TRANS_MODE':'DR','FLAG':'BANK','MATERIALIZED_ON':'09/02/2023','BANK_REF_NO':'1898(Cheque)','Narration':'','TRANSACTION_ID':'2107@486','CLIENT_TRANSACTION_MODE':'Cheque','RECEIPTNO':'0'},{'SOURCE':'SSP','VOUCHER_DATE':'02/09/2022','VOUCHER_TYPE':'RC','CLIENT_CODE':'DSAVIOTPT','CLIENT_REFERENCE_ID':'2106','PROJECT_NAME':'Don Bosco Centre (SISS) - Yellagiri Hills','LEDGER':'Admission Fees','LEDGER_AMOUNT':2000,'LEDGER_TRANS_MODE':'CR','CASHBANK_LEDGER':'000001','CASHBANK_AMOUNT':2000,'CASHBANK_TRANS_MODE':'DR','FLAG':'BANK','MATERIALIZED_ON':'','BANK_REF_NO':'4444(Cheque)','Narration':'','TRANSACTION_ID':'2106@485','CLIENT_TRANSACTION_MODE':'Cheque','RECEIPTNO':'0'}]}";
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog("Could not proceed" + ex.Message);
                status = "Status: Could not proceed. Get Voucher..." + ex.Message;
            }
            return status;
        }

        /// <summary>
        /// Get Voucher
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="ManagementCode"></param>
        /// <param name="JsonScriptMaster"></param>
        /// <returns></returns>
        public string APIGetVoucher(string DateFrom, string DateTo, string ManagementCode, string JsonScriptMaster, string projectname = "", string ProjectId = "")
        {
            string Status = "";
            //  string postdatda = "{\"Project\": [{\"PROJECT\": \"Bosco Hostel - Tirupattur\"}]}";
            // http://testing.higrade.live:8069/api/acme/transaction/data/21-05-2022
            // string urlName = string.Format("http://testing.higrade.live:8069/api/acme/transaction/data/25-05-2022");
            //urlName = string.Format("https://9c31-117-193-15-140.ngrok-free.app/api/acme/requests");

            string urlName = string.Empty;
            if (!setting.IS_SDB_RMG)
            {
                // urlName = string.Format(HiGradeURL + "api/acme/transaction/data/" + DateTime.Parse(DateFrom).Date.ToString("dd-MM-yyyy") + "/" + DateTime.Parse(DateTo).Date.ToString("dd-MM-yyyy")?project=[" + projectname + "]);

                // Project Ids list
                //string formattedProjectIds = "[" + ProjectId + "]";
                //string projectIdQuery = "?project=" + formattedProjectIds;
                //urlName = HiGradeURL + "api/acme/transaction/data/" + DateTime.Parse(DateFrom).ToString("dd-MM-yyyy") + "/" + DateTime.Parse(DateTo).ToString("dd-MM-yyyy") + projectIdQuery;

                // Project list Details
                string formattedProjectList = "[" + projectname + "]";
                string encodedProjectList = Uri.EscapeDataString(formattedProjectList);
                string projectQuery = "?project=" + encodedProjectList;
                urlName = HiGradeURL + "api/acme/transaction/data/" + DateTime.Parse(DateFrom).ToString("dd-MM-yyyy") + "/" + DateTime.Parse(DateTo).ToString("dd-MM-yyyy") + projectQuery;

                //urlName = "http://localhost:9393/api/acme/transaction/data/01-05-2025?project=['Don Bosco College, Yellagiri Hills - General A/c']";

                // it was working
                //urlName = "http://192.168.1.117:9393/api/acme/transaction/data/01-05-2025?project=[" + projectname + "]";

                // https://dbcy.higrade.live/api/acme/transaction/data/26-04-2025/25-05-2025/'Direzione Generale','Ispettorie & Altri'
            }
            else
            {
                //urlName = string.Format("https://18d5-182-60-17-52.ngrok-free.app/api/acme/transaction/data/" + DateTime.Parse(DateFrom).Date.ToString("dd-MM-yyyy") + "/" + DateTime.Parse(DateTo).Date.ToString("dd-MM-yyyy") + "/" + projectname + "/" + RCPY);
                //urlName = string.Format("https://bd37-182-60-21-40.ngrok-free.app/api/acme/transaction/data/" + DateTime.Parse(DateFrom).Date.ToString("dd-MM-yyyy") + "/" + DateTime.Parse(DateTo).Date.ToString("dd-MM-yyyy") + "/" + projectname);
                //urlName = string.Format("https://udeg.demoboscosoft.com/testing/public/api/acme/transaction/data/" + DateTime.Parse(DateFrom).Date.ToString("dd-MM-yyyy") + "/" + DateTime.Parse(DateTo).Date.ToString("dd-MM-yyyy") + "/" + projectname);

                AcMELog.WriteLog("getting vouchers. Started -API ");
                urlName = string.Format("https://economato.sdb.org/api/acme/transaction/data/" + DateTime.Parse(DateFrom).Date.ToString("dd-MM-yyyy") + "/" + DateTime.Parse(DateTo).Date.ToString("dd-MM-yyyy") + "/" + projectname);
                AcMELog.WriteLog("getting vouchers. Ended -API");
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            try
            {
                WebRequest RequestData = WebRequest.Create(urlName);
                RequestData.Method = "GET";

                if (!setting.IS_SDB_RMG)
                {
                    RequestData.ContentType = "application/http";
                }
                else
                {
                    RequestData.ContentType = "application/json";
                }

                WebResponse ResponseData = RequestData.GetResponse();
                string resposne = (((HttpWebResponse)ResponseData).StatusDescription);
                Stream srData = ResponseData.GetResponseStream();
                using (StreamReader Sw = new StreamReader(srData))
                {
                    Status = Sw.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog("Could not proceed" + ex.Message);
                Status = "Status: Could not proceed. Get Voucher..." + ex.Message;
            }
            return Status;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void EnableTLS12()
        {
            const int Tls12 = 3072;
            const int Tls11 = 768;
            const int Tls10 = 192;

            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)Tls12;
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("TLS 1.2 is not supported directly, applying dynamic workaround...");
            }
            try
            {
                Type type = typeof(ServicePointManager).Assembly.GetType("System.Net.SecurityProtocolType");

                if (type != null)
                {
                    System.Reflection.PropertyInfo securityProtocolProperty = typeof(ServicePointManager).GetProperty("SecurityProtocol");
                    if (securityProtocolProperty != null)
                    {
                        object currentProtocols = securityProtocolProperty.GetValue(null, null);

                        object newProtocols = Enum.ToObject(type, Convert.ToInt32(currentProtocols) | Tls12 | Tls11 | Tls10);

                        securityProtocolProperty.SetValue(null, newProtocols, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not enable TLS 1.2: " + ex.Message);
            }

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
        }

        /// <summary>
        /// send the Inserted details to SSP
        /// </summary>
        /// <param name="ManagementCode"></param>
        /// <param name="JsonSavedScriptTransaction"></param>
        /// <returns></returns>
        public string UpdateGetVouchers(string ManagementCode, string JsonSavedScriptTransaction)
        {
            string status = "";
            try
            {
                objAuthHead.UserName = AcMEUserName;
                objAuthHead.Password = AcMEPassword;

                AcMELog.WriteLog("Started Update Get Voucher..");
                status = objService.UpdateTransactionDetails(objAuthHead, ManagementCode, JsonSavedScriptTransaction);
                AcMELog.WriteLog("Ended Update Get Voucher..");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return status;
        }

        /// <summary>
        /// send the Inserted details to SSP
        /// </summary>
        /// <param name="ManagementCode"></param>
        /// <param name="JsonSavedScriptTransaction"></param>
        /// <returns></returns>
        public string APIUpdateGetVouchers(string ManagementCode, string JsonSavedScriptTransaction)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            string status = "";
            try
            {
                objAuthHead.UserName = AcMEUserName;
                objAuthHead.Password = AcMEPassword;

                string urlName = string.Empty;
                if (!setting.IS_SDB_RMG)
                {
                    urlName = string.Format(HiGradeURL + "api/acme/transaction/update");
                }
                else
                {
                    //urlName = string.Format("https://91fc-59-93-247-16.ngrok-free.app/api/acme/transaction/update");
                    //urlName = string.Format("https://udeg.demoboscosoft.com/testing/public/api/acme/transaction/update");
                    urlName = string.Format("https://economato.sdb.org/api/acme/transaction/update");
                }

                AcMELog.WriteLog("Started Update Get Voucher..");
                JsonSavedScriptTransaction = "{\"params\": " + JsonSavedScriptTransaction + "}";

                WebRequest RequestService = WebRequest.Create(urlName);
                RequestService.Method = "POST";
                RequestService.ContentType = "application/json";

                using (var StreamReceivedData = new StreamWriter(RequestService.GetRequestStream()))
                {
                    StreamReceivedData.Write(JsonSavedScriptTransaction);
                    StreamReceivedData.Flush();
                    StreamReceivedData.Close();
                }

                WebResponse ResponseData = RequestService.GetResponse();
                string resposne = (((HttpWebResponse)ResponseData).StatusDescription);
                Stream Sr = ResponseData.GetResponseStream();
                using (StreamReader SR = new StreamReader(Sr))
                {
                    status = SR.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return status;
        }

        /// <summary>
        /// To Convert Dataset to Json string Details
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public string ConvertDatasetToJson(DataSet dataset)
        {
            string RtnMaster = JsonConvert.SerializeObject(dataset, Formatting.Indented);
            return RtnMaster;
        }

        /// <summary>
        /// Convert Json to Datatable ( Not Used)
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        public DataTable ConvertJsonToDatatable(string JsonData)
        {
            // DataTable dtJsonValue = (DataTable)JsonConvert.DeserializeObject(JsonData,(typeof(DataTable)));
            DataTable dtJsonValue = JsonConvert.DeserializeObject<DataTable>(JsonData);
            return dtJsonValue;
        }

        /// <summary>
        /// Converting
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        public DataTable ConvertDeserializedstringToDatatable(string JsonData)
        {
            DataTable DeserilizedData = new DataTable();
            if (this.setting.IS_SDB_RMG)
            {
                DeserilizedData = ConvertJsonStringDatatbleforDecimal(JsonData);
            }
            else
            {
                DeserilizedData = ConvertJsonStringDatatble(JsonData);
            }
            return DeserilizedData;

        }

        /// <summary>
        /// Convert Json string Datatable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ConvertJsonStringDatatble(string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first array using Linq
            var SourceDataArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in SourceDataArray.Children<JObject>())
            {
                var NewObjectClean = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        NewObjectClean.Add(column.Name, column.Value);
                    }
                }
                trgArray.Add(NewObjectClean);
            }
            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }


        // if Multiple values 
        /// <summary>
        /// Convert JSON string to DataTable, preserving decimal precision.
        /// </summary>
        /// <param name="json">JSON string containing an object with an array of data.</param>
        /// <returns>DataTable constructed from the JSON data.</returns>
        public static DataTable ConvertJsonStringDatatbleforDecimal(string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first JArray in the JSON structure
            var SourceDataArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();

            foreach (JObject row in SourceDataArray.Children<JObject>())
            {
                var NewObjectClean = new JObject();

                // Loop through each property (column) in the row
                foreach (JProperty column in row.Properties())
                {
                    if (column.Value is JValue)
                    {
                        var jValue = (JValue)column.Value;

                        // Preserve decimal precision for numeric values
                        if (jValue.Type == JTokenType.Integer || jValue.Type == JTokenType.Float)
                        {
                            decimal decimalValue;
                            if (decimal.TryParse(jValue.ToString(), out decimalValue))
                            {
                                NewObjectClean.Add(column.Name, decimalValue);
                            }
                            else
                            {
                                NewObjectClean.Add(column.Name, column.Value);
                            }
                        }
                        else
                        {
                            NewObjectClean.Add(column.Name, column.Value);
                        }
                    }
                    else
                    {
                        // In case of non-JValue (e.g., nested arrays or objects)
                        NewObjectClean.Add(column.Name, column.Value);
                    }
                }
                trgArray.Add(NewObjectClean);
            }
            // Convert the cleaned JArray to DataTable
            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }

        #endregion
    }
}
