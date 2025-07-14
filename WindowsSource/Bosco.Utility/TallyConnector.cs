using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Net;
using System.IO;

using Bosco.Utility;
using System.Xml;


namespace Bosco.Utility
{
    public class TallyConnector
    {
        #region Decelaration
        public string MSG_TITLE = "Tally.ERP 9 Viewer";
        public string MSG_TALLY_NOT_RUNNING = String.Format("Tally is not Running.{0}Tally must be running with admin rights", Environment.NewLine);
        public enum TallyMasters
        {
            Groups,
            Ledgers,
            CostCategory,
            CostCenters,
            VoucherType,
            Donors,
            Sponsers,
            Purposes,
            States,
            Country
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return Current/Active Tally Company Name
        /// </summary>
        private ResultArgs CurrentCompnayName
        {
            get
            {
                ResultArgs resultarg = new ResultArgs();
                resultarg = FetchCurrentCompanyName();
                return resultarg;
            }
        }

        /// <summary>
        /// Get Tally odbc server from the config file
        /// </summary>
        private string GetTallyServer
        {
            get
            {
                string tallyserver = string.Empty;
                if (ConfigurationManager.AppSettings["TallyServer"] != null)
                {
                    tallyserver = ConfigurationManager.AppSettings["TallyServer"].ToString();
                }
                return tallyserver;
            }
        }

        /// <summary>
        /// Get Tally odbc server port from the config file
        /// </summary>
        private string GetTallyPort
        {
            get
            {
                string port = string.Empty;
                if (ConfigurationManager.AppSettings["Port"] != null)
                {
                    port = ConfigurationManager.AppSettings["Port"].ToString();
                }
                return port;
            }
        }

        /// <summary>
        /// Form tally odbc connection string with help of tally odbc server and its port
        /// </summary>
        private ResultArgs GetTallyConnectionString
        {
            get
            {
                ResultArgs resultarg = new ResultArgs();
                string tallyConnectionString = string.Empty;
                string tallyserver = GetTallyServer;
                string tallyport = GetTallyPort;

                if (tallyserver != string.Empty && tallyport != string.Empty)
                {
                    tallyConnectionString = "PORT=" + tallyport + ";DRIVER=Tally ODBC Driver;SERVER=" + tallyserver;
                    resultarg.ReturnValue = tallyConnectionString;
                    resultarg.Success = true;
                }

                if (tallyConnectionString == string.Empty)
                {
                    resultarg.Message = "TallyConnection is not avilable in Config";
                }
                return resultarg;
            }
        }

        /// <summary>
        /// Return Tally server connected or not
        /// </summary>
        public ResultArgs IsTallyConnected
        {
            get
            {
                ResultArgs resultArg = new ResultArgs();
                try
                {
                    resultArg = GetTallyConnectionString;
                    string connectionString = string.Empty;
                    if (resultArg.Success)
                    {
                        connectionString = resultArg.ReturnValue.ToString();
                        using (OdbcConnection con = new OdbcConnection(connectionString))
                        {
                            con.Open();
                            //if (con != null && con.State == ConnectionState.Open) con.Close();
                        }
                    }
                }
                catch (Exception err)
                {
                    if (err.Message.Contains("Data source name not found"))
                    {
                        resultArg.Message = MSG_TALLY_NOT_RUNNING;

                    }
                    else
                    {
                        resultArg.Message = err.Message;
                    }
                }

                return resultArg;
            }
        }

        /// <summary>
        /// Return whehter donor module enabled or not
        /// </summary>
        public bool IsDonorModuleEnabled
        {
            get
            {
                Boolean Rtn = false;
                ResultArgs resultarg = FetchTallyGeneralCCMaster();

                if (resultarg.Success)
                {
                    DataSet dsCCMaster = resultarg.DataSource.TableSet;
                    if (dsCCMaster.Tables["_UDF_788529213"] != null)
                    {
                        Rtn = true;
                    }
                }
                return Rtn;
            }
        }
        #endregion

        #region Public tally connector Methods
        /// <summary>
        /// Get Current/Active company name in tally
        /// </summary>
        /// <returns>ResultArgs with company name and its result sucess or failure</returns>
        public ResultArgs FetchCurrentCompanyName()
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            string xml = "<ENVELOPE>" +
                            "<HEADER>" +
                            "<TALLYREQUEST>Export Data</TALLYREQUEST>" +
                            "</HEADER>" +
                            "<BODY>" +
                            "<EXPORTDATA>" +
                            "<REQUESTDESC>" +
                            "<REPORTNAME>List of Accounts</REPORTNAME>" +
                            "<STATICVARIABLES>" +
                            "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                            "<ACCOUNTTYPE>GROUPS</ACCOUNTTYPE>" +    //GROUPS
                            "<ENCODINGTYPE>UNICODE</ENCODINGTYPE>" +
                            "</STATICVARIABLES>" +
                            "</REQUESTDESC>" +
                            "</EXPORTDATA>" +
                            "</BODY>" +
                            "</ENVELOPE>";
            try
            {
                resultarg = ExecuteTallyXML(xml, TallyMasters.Groups);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        if (TallyResponseDataSet.Tables["STATICVARIABLES"] != null)
                        {
                            DataTable dtCurCompanyName = TallyResponseDataSet.Tables["STATICVARIABLES"];
                            if (dtCurCompanyName.Rows.Count > 0)
                            {
                                resultarg.ReturnValue = dtCurCompanyName.Rows[0]["SVCURRENTCOMPANY"].ToString();
                                resultarg.Success = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {

            }

            return resultarg;
        }

        /// <summary>
        /// Get Current/Active company information 
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchCompanyInfo()
        {
            ResultArgs resultarg = new ResultArgs();
            resultarg = IsTallyConnected;
            if (resultarg.Success)
            {
                string connectionstring = resultarg.ReturnValue.ToString();
                resultarg = CurrentCompnayName;
                if (resultarg.Success)
                {
                    if (resultarg.ReturnValue != null)
                    {

                        string currentCompanyname = resultarg.ReturnValue.ToString();
                        string query = "SELECT $NAME, $StartingFrom, $BooksFrom, " +
                                       "$_Address1,$_Address2, $_Address3, $_Address4, $_Address5" +
                                       " FROM company";
                        try
                        {
                            if (resultarg.Success)
                            {
                                resultarg = ExecuteTally(query);
                                if (resultarg.Success)
                                {
                                    DataTable dtODBCcompany = resultarg.DataSource.Table;
                                    dtODBCcompany.DefaultView.RowFilter = string.Format("$NAME='{0}'", currentCompanyname.Replace(@"'", @"''"));
                                    DataTable dtCompnayInfo = dtODBCcompany.DefaultView.ToTable();
                                    dtCompnayInfo.TableName = "Company";

                                    //Add one column in company info table Is DONORS Moulde enabled or not---------------------------------------------------------------------------
                                    if (dtODBCcompany.Rows.Count > 0)
                                    {
                                        DataColumn dcdonorenabled = new DataColumn("IsDonorModuleEnabled", typeof(Boolean));
                                        dtCompnayInfo.Columns.Add(dcdonorenabled);
                                        dtCompnayInfo.Rows[0][dcdonorenabled] = IsDonorModuleEnabled;
                                    }
                                    //---------------------------------------------------------------------------

                                    resultarg.DataSource.Data = dtCompnayInfo;
                                    resultarg.Success = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Data source name not found"))
                            {
                                resultarg.Message = MSG_TALLY_NOT_RUNNING;
                            }
                            else
                            {
                                resultarg.Message = ex.Message;
                            }
                        }
                    }
                    else
                    {
                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
                    }
                }
            }
            return resultarg;
        }

        /// <summary>
        /// Get all defined masters from tally
        /// 1. Groups 2. Ledgers 3. CostCategory 4. CostCenters 5. VoucherType
        /// 6. Donors 7. Sponsers 8. Purposes 9. States 10 Country 
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        public ResultArgs FetchMasters()
        {
            string query = string.Empty;
            DataSet dsMaster = new DataSet();
            ResultArgs resultarg = new ResultArgs();
            var valuesAsList = Enum.GetValues(typeof(TallyMasters)).Cast<TallyMasters>().ToList();

            try
            {
                dsMaster.Tables.Clear();
                for (int i = 0; i < valuesAsList.Count; i++)
                {
                    TallyMasters TallyEnum = ((TallyMasters)valuesAsList[i]);
                    resultarg = FetchTally(TallyEnum);
                    if (resultarg.Success)
                    {
                        dsMaster.Tables.Add(resultarg.DataSource.Table);
                    }
                    else
                    {
                        break;
                    }
                }

                //Attach Company Info
                if (resultarg.Success)
                {
                    resultarg = FetchCompanyInfo();
                    if (resultarg.Success)
                    {
                        dsMaster.Tables.Add(resultarg.DataSource.Table);
                    }
                }

                resultarg.DataSource.Data = dsMaster;
                resultarg.Success = true;
            }
            catch (Exception err)
            {
                resultarg.Message = err.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Retriew Voucher Entries from Tally odbc server
        /// (Mastet Voucher, Map Voucher base type Child Vouhcer, Cost Cener Allocaton for each child Voucher enteries
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        public ResultArgs FetchVouchers()
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            DataSet dsVouchers = new DataSet();

            string xml = "<ENVELOPE>" +
                            "<HEADER>" +
                              "<TALLYREQUEST>Export Data</TALLYREQUEST>" +
                            "</HEADER>" +
                            "<BODY>" +
                            "<EXPORTDATA>" +
                                "<REQUESTDESC>" +
                                    "<REPORTNAME>Voucher Register</REPORTNAME>" +
                                "</REQUESTDESC>" +
                            "</EXPORTDATA>" +
                            "</BODY>" +
                         "</ENVELOPE>";

            //string xml = "<ENVELOPE>" +
            //                "<HEADER>" +
            //                "<TALLYREQUEST>Export Data</TALLYREQUEST>" +
            //                "</HEADER>" +
            //                "<BODY>" +
            //                "<EXPORTDATA>" +
            //                "<REQUESTDESC>" +
            //                "<REPORTNAME>List of Accounts</REPORTNAME>" +
            //                "</REQUESTDESC>" +
            //                "</EXPORTDATA>" +
            //                "</BODY>" +
            //                "</ENVELOPE>";
            try
            {
                resultarg = ExecuteTallyXML(xml, TallyMasters.Groups);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        if (TallyResponseDataSet.Tables["VOUCHER"] != null && TallyResponseDataSet.Tables["ALLLEDGERENTRIES.LIST"] != null)
                        {
                            string[] masterVoucher = { "VOUCHER_ID", "DATE", "VOUCHERTYPENAME", "VOUCHERNUMBER", "PARTYLEDGERNAME", "NARRATION", "VCHTYPE" };
                            string[] childVoucher = { "VOUCHER_ID", "ALLLEDGERENTRIES.LIST_Id", "LEDGERNAME", "AMOUNT", "NARRATION", "ISDEEMEDPOSITIVE" };
                            DataTable dtMasterVoucher = new DataTable("MASTER VOUCHER");
                            dtMasterVoucher.Columns.Add("VOUCHER_ID", typeof(Int32));
                            dtMasterVoucher.Columns.Add("DATE", typeof(string));
                            dtMasterVoucher.Columns.Add("VOUCHERTYPENAME", typeof(string));
                            dtMasterVoucher.Columns.Add("VOUCHERNUMBER", typeof(string));
                            dtMasterVoucher.Columns.Add("PARTYLEDGERNAME", typeof(string));
                            dtMasterVoucher.Columns.Add("NARRATION", typeof(string));
                            dtMasterVoucher.Columns.Add("BASEVOUCHERTYPE", typeof(string));

                            DataTable dtChildVoucher = new DataTable("VOUCHER DETAILS");
                            dtChildVoucher.Columns.Add("VOUCHER_ID", typeof(Int32));
                            dtChildVoucher.Columns.Add("ALLLEDGERENTRIES.LIST_ID", typeof(Int32));
                            dtChildVoucher.Columns.Add("LEDGERNAME", typeof(string));
                            dtChildVoucher.Columns.Add("AMOUNT", typeof(string));
                            dtChildVoucher.Columns.Add("NARRATION", typeof(string));
                            dtChildVoucher.Columns.Add("ISDEEMEDPOSITIVE", typeof(string));
                            dtChildVoucher.Columns.Add("TRANSACTIONTYPE", typeof(string));
                            dtChildVoucher.Columns.Add("INSTRUMENTDATE", typeof(string));
                            dtChildVoucher.Columns.Add("INSTRUMENTNUMBER", typeof(string));

                            DataTable dtMasterVouchers = TallyResponseDataSet.Tables["VOUCHER"]; //Master Vouchers
                            DataTable dtTallyChildVochers = TallyResponseDataSet.Tables["ALLLEDGERENTRIES.LIST"]; //Child Vouchers
                            DataTable dtCCCategory = TallyResponseDataSet.Tables["CATEGORYALLOCATIONS.LIST"]; //Cost center category list
                            DataTable dtCC = TallyResponseDataSet.Tables["COSTCENTREALLOCATIONS.LIST"]; //Cost center Allocation List 
                            DataTable dtBankReference = TallyResponseDataSet.Tables["BANKALLOCATIONS.LIST"]; //get bank dd, cheque, interbank reference number

                            //Map Vouhcer enty with its base voucher type (RECEIPTS, PAYMENTS, CONTRA, JOURNAL) ----------------------
                            ResultArgs resultargVoucherType = FetchTally(TallyMasters.VoucherType);
                            DataTable dtVoucherType = new DataTable();
                            if (resultargVoucherType.Success)
                            {
                                dtVoucherType = resultargVoucherType.DataSource.Table;
                                var resultMasterVoucher = from drVoucherType in dtVoucherType.AsEnumerable()
                                                          join drMasterVoucher in dtMasterVouchers.AsEnumerable() on drVoucherType.Field<string>("VOUCHERTYPE")
                                                          equals drMasterVoucher.Field<string>("VCHTYPE")
                                                          select dtMasterVoucher.LoadDataRow(new object[]
                                 {
                                    drMasterVoucher.Field<Int32>("VOUCHER_ID"),
                                    ConvertTallyDate(drMasterVoucher.Field<string>("DATE")),
                                    drMasterVoucher.Field<string>("VOUCHERTYPENAME"),
                                    drMasterVoucher.Field<string>("VOUCHERNUMBER"),
                                    drMasterVoucher.Field<string>("PARTYLEDGERNAME"),
                                    drMasterVoucher.Field<string>("NARRATION"),
                                    drVoucherType.Field<string>("BASEVOUCHERTYPE"),
                                  }, true);
                                dtMasterVoucher = resultMasterVoucher.CopyToDataTable();
                                dtMasterVoucher.TableName = "MASTER VOUCHER";
                            }
                            //--------------------------------------------------------------------------------------------------------

                            //Map bank details (dd, cheque, interbank transfer reference number with child vouhcer entries
                            var resultChildVouhcer = from drChildVochers in dtTallyChildVochers.AsEnumerable()
                                                     //join drBankReference in dtBankReference.AsEnumerable()
                                                     // on drChildVochers.Field<Int32>("ALLLEDGERENTRIES.LIST_Id") equals drBankReference.Field<Int32>("ALLLEDGERENTRIES.LIST_Id")
                                                     // into bankreferencejoin
                                                     //from drBankReference in bankreferencejoin.DefaultIfEmpty()
                                                     select dtChildVoucher.LoadDataRow(new object[]
                                                     {
                                                         drChildVochers.Field<Int32>("VOUCHER_ID"),
                                                         drChildVochers.Field<Int32>("ALLLEDGERENTRIES.LIST_ID"),
                                                         drChildVochers.Field<string>("LEDGERNAME"),
                                                         drChildVochers.Field<string>("AMOUNT"),
                                                         drChildVochers.Field<string>("NARRATION"),
                                                         drChildVochers.Field<string>("ISDEEMEDPOSITIVE"),
                                                         string.Empty,//(drBankReference==null?string.Empty:drBankReference.Field<string>("TransactionType")),
                                                         string.Empty,//(drBankReference==null?string.Empty:ConvertTallyDate(drBankReference.Field<string>("InstrumentDate"))),
                                                         string.Empty,//(drBankReference==null?string.Empty:drBankReference.Field<string>("InstrumentNumber"))
                                                     }, true);

                            dtChildVoucher = resultChildVouhcer.CopyToDataTable();
                            dtChildVoucher.TableName = "VOUCHER DETAILS";
                            //------------------------------------------------------------------------------------------------

                            //Map CC with CC category Allocation for each child voucher entries---------------------------------------------------------
                            DataTable dtCCVoucher = new DataTable("CCVoucher");
                            dtCCVoucher.Columns.Add("ALLLEDGERENTRIES.LIST_Id", typeof(Int32));
                            dtCCVoucher.Columns.Add("CATEGORYALLOCATIONS.LIST_ID", typeof(Int32));
                            dtCCVoucher.Columns.Add("COSTCENTREALLOCATIONS.LIST_ID", typeof(Int32));
                            dtCCVoucher.Columns.Add("CATEGORY", typeof(string));
                            dtCCVoucher.Columns.Add("NAME", typeof(string));
                            dtCCVoucher.Columns.Add("AMOUNT", typeof(string));
                            if (dtCCCategory != null)
                            {
                                var result = from drCCCategory in dtCCCategory.AsEnumerable()
                                             join drCC in dtCC.AsEnumerable() on drCCCategory.Field<Int32>("CATEGORYALLOCATIONS.LIST_ID")
                                              equals drCC.Field<Int32>("CATEGORYALLOCATIONS.LIST_ID")
                                             select dtCCVoucher.LoadDataRow(new object[]
                                 {
                                    drCCCategory.Field<Int32>("ALLLEDGERENTRIES.LIST_Id"),
                                    drCCCategory.Field<Int32>("CATEGORYALLOCATIONS.LIST_ID"),
                                    drCC.Field<Int32>("COSTCENTREALLOCATIONS.LIST_ID"),
                                    drCCCategory.Field<string>("CATEGORY"),
                                    drCC.Field<string>("NAME"),
                                    drCC.Field<string>("AMOUNT"),
                                  }, true);
                                dtCCVoucher = result.CopyToDataTable();
                                dtCCVoucher.TableName = "CCVoucher";
                            }


                            // Donor Voucher Infomration ***************************************************************
                            DataTable dtDonorVocuerEntry = new DataTable();
                            dtDonorVocuerEntry.Columns.Add("COSTCENTREALLOCATIONS.LIST_ID", typeof(Int32));
                            dtDonorVocuerEntry.Columns.Add("UDF_805306420_LIST_Id", typeof(Int32));
                            dtDonorVocuerEntry.Columns.Add("DONOR", typeof(string));
                            dtDonorVocuerEntry.Columns.Add("DONATIONAMOUNT", typeof(string));
                            dtDonorVocuerEntry.Columns.Add("RECEIPTTYPE", typeof(string));
                            DataTable dtUDFDonorInfoList = TallyResponseDataSet.Tables["_UDF_805306420.LIST"];
                            //dtUDFDonorInfoList.DefaultView.RowFilter = "COSTCENTREALLOCATIONS.LIST_ID=0";

                            if (dtUDFDonorInfoList != null)
                            {
                                //Donor Amount
                                DataTable dtUDFDonationAmtList = TallyResponseDataSet.Tables["_UDF_687865911.LIST"];
                                DataTable dtUDFDonationAmt = TallyResponseDataSet.Tables["_UDF_687865911"];
                                var resultDonationAmt = from drUDFDonationAmtList in dtUDFDonationAmtList.AsEnumerable()
                                                        join drUDFDonationAmt in dtUDFDonationAmt.AsEnumerable()
                                                        on drUDFDonationAmtList.Field<Int32>("_UDF_687865911.LIST_Id") equals drUDFDonationAmt.Field<Int32>("_UDF_687865911.LIST_Id")
                                                        select new
                                                        {
                                                            UDF_805306420_LIST_Id = drUDFDonationAmtList.Field<Int32>("_UDF_805306420.LIST_Id"),
                                                            UDF_687865911_LIST_Id = drUDFDonationAmtList.Field<Int32>("_UDF_687865911.LIST_Id"),
                                                            AMOUNT = drUDFDonationAmt.Field<string>("_UDF_687865911_TEXT"),
                                                        };
                                //Name of the Donor
                                DataTable dtUDFDonorNameList = TallyResponseDataSet.Tables["_UDF_788529205.LIST"];
                                DataTable dtUDFDonorName = TallyResponseDataSet.Tables["_UDF_788529205"];
                                var resultDonationName = from drUDFDonorNameList in dtUDFDonorNameList.AsEnumerable()
                                                         join drUDFDonorName in dtUDFDonorName.AsEnumerable()
                                                            on drUDFDonorNameList.Field<Int32>("_UDF_788529205.LIST_Id") equals drUDFDonorName.Field<Int32>("_UDF_788529205.LIST_Id")
                                                            into udfdonornamejoin
                                                         from drUDFDonorName in udfdonornamejoin.DefaultIfEmpty()
                                                         select new
                                                         {
                                                             UDF_805306420_LIST_Id = drUDFDonorNameList.Field<Int32>("_UDF_805306420.LIST_Id"),
                                                             UDF_788529205_LIST_Id = drUDFDonorNameList.Field<Int32>("_UDF_788529205.LIST_Id"),
                                                             DONORNAME = drUDFDonorName.Field<string>("_UDF_788529205_TEXT"),
                                                         };

                                //Donor Receipt Type
                                DataTable dtUDFReceiptTypeList = TallyResponseDataSet.Tables["_UDF_788567362.LIST"];
                                DataTable dtUDFReceiptType = TallyResponseDataSet.Tables["_UDF_788567362"];
                                var resultReceiptTypeList = from drUDFReceiptTypeList in dtUDFReceiptTypeList.AsEnumerable()
                                                            join drUDFReceiptType in dtUDFReceiptType.AsEnumerable()
                                                                on drUDFReceiptTypeList.Field<Int32>("_UDF_788567362.LIST_Id") equals drUDFReceiptType.Field<Int32>("_UDF_788567362.LIST_Id")
                                                                into udfreceipttypejoin
                                                            from drUDFReceiptType in udfreceipttypejoin.DefaultIfEmpty()
                                                            select new
                                                            {
                                                                UDF_805306420_LIST_Id = drUDFReceiptTypeList.Field<Int32>("_UDF_805306420.LIST_Id"),
                                                                UDF_788567362_LIST_Id = drUDFReceiptTypeList.Field<Int32>("_UDF_788567362.LIST_Id"),
                                                                RECEIPTTYPE = drUDFReceiptType.Field<string>("_UDF_788567362_TEXT"),
                                                            };


                                var resultDonorVocuerEntry = from drUDFDonorInfoList in dtUDFDonorInfoList.AsEnumerable()
                                                             join drUDFDonationAmount in resultDonationAmt.AsEnumerable()
                                                                 on drUDFDonorInfoList.Field<Int32>("_UDF_805306420.LIST_Id") equals drUDFDonationAmount.UDF_805306420_LIST_Id
                                                                 into donationamountjoin
                                                             from drUDFDonationAmount in donationamountjoin.DefaultIfEmpty()
                                                             join drUDFDonationName in resultDonationName.AsEnumerable()
                                                                 on drUDFDonationAmount.UDF_805306420_LIST_Id equals drUDFDonationName.UDF_805306420_LIST_Id
                                                                 into donornamejoin
                                                             from drUDFDonationName in donornamejoin.DefaultIfEmpty()
                                                             join drUDFReceiptType in resultReceiptTypeList.AsEnumerable()
                                                                 on drUDFDonationAmount.UDF_805306420_LIST_Id equals drUDFReceiptType.UDF_805306420_LIST_Id
                                                                 into receipttypejoin
                                                             from drUDFReceiptType in receipttypejoin.DefaultIfEmpty()
                                                             select dtDonorVocuerEntry.LoadDataRow(new object[]
                                                             {
                                                                 drUDFDonorInfoList.Field<Int32>("COSTCENTREALLOCATIONS.LIST_ID"),
                                                                 drUDFDonorInfoList.Field<Int32>("_UDF_805306420.LIST_Id"),
                                                                 drUDFDonationName.DONORNAME,
                                                                 drUDFDonationAmount.AMOUNT,
                                                                 drUDFReceiptType.RECEIPTTYPE
                                                             }, true);
                                dtDonorVocuerEntry = resultDonorVocuerEntry.CopyToDataTable();
                                dtDonorVocuerEntry.TableName = "DonorVoucher";
                            }
                            //**********************************************************************************

                            //------------------------------------------------------------------------------------

                            //Add Vouchers to dataset ----------------------------------------------------------------
                            //DataTable dtchildvouchers = dtTallyChildVochers.DefaultView.ToTable("VOUCHER DETAILS", false, childVoucher);
                            dsVouchers.Tables.Add(dtMasterVoucher);
                            dsVouchers.Tables.Add(dtChildVoucher);
                            dsVouchers.Tables.Add(dtCCVoucher);
                            dsVouchers.Tables.Add(dtVoucherType);
                            dsVouchers.Tables.Add(dtDonorVocuerEntry);

                            dsVouchers.Relations.Add(dtChildVoucher.TableName, dsVouchers.Tables[0].Columns["VOUCHER_ID"], dsVouchers.Tables[1].Columns["VOUCHER_id"], false);
                            //-----------------------------------------------------------------------------------------
                            resultarg.Success = true;
                            resultarg.DataSource.Data = dsVouchers;
                        }
                        //else
                        //{
                        //    resultarg.Message = "Vouchers not avilable";
                        //}
                    }
                    //else
                    //{
                    //    resultarg.Message = "Vouchers not avilable";
                    //}
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, ex.StackTrace);
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {
            }

            return resultarg;
        }

        /// <summary>
        /// Get LEdger list with opening balance from tally odbc
        /// </summary>
        /// <returns>ResultArgs with ledger and its result sucess or failure</returns>
        public ResultArgs FetchLedger()
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            string xml = "<ENVELOPE>" +
                            "<HEADER>" +
                            "<TALLYREQUEST>Export Data</TALLYREQUEST>" +
                            "</HEADER>" +
                            "<BODY>" +
                            "<EXPORTDATA>" +
                            "<REQUESTDESC>" +
                            "<REPORTNAME>List of Accounts</REPORTNAME>" +
                            "<VARIABLES>" +
                            "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                            "<ACCOUNTTYPE>LEDGER</ACCOUNTTYPE>" +
                            "<ENCODINGTYPE>UNICODE</ENCODINGTYPE>" +
                            "</VARIABLES>" +
                            "</REQUESTDESC>" +
                            "</EXPORTDATA>" +
                            "</BODY>" +
                            "</ENVELOPE>";
            try
            {
                resultarg = ExecuteTally("SELECT $Name, $Parent, $_PrimaryGroup FROM Ledger");
                if (resultarg.Success)
                {
                    DataTable dtTallyODBCLedger = resultarg.DataSource.Table;
                    resultarg = ExecuteTallyXML(xml, TallyMasters.Ledgers);
                    if (resultarg.Success)
                    {
                        TallyResponseDataSet = resultarg.DataSource.TableSet;
                        if (TallyResponseDataSet != null)
                        {
                            if (TallyResponseDataSet.Tables["LEDGER"] != null)
                            {
                                DataTable dtLedger = new DataTable();
                                dtLedger.Columns.Add("LEDGER_ID", typeof(Int32));
                                dtLedger.Columns.Add("NAME", typeof(string));
                                dtLedger.Columns.Add("PARENT", typeof(string));
                                dtLedger.Columns.Add("PRIMARYGROUP", typeof(string));
                                dtLedger.Columns.Add("OPENINGBALANCE", typeof(string));
                                dtLedger.Columns.Add("ISCOSTCENTRESON", typeof(string));
                                dtLedger.Columns.Add("BANKACCHOLDERNAME", typeof(string));
                                dtLedger.Columns.Add("BANKDETAILS", typeof(string));
                                dtLedger.Columns.Add("BANKBRANCHNAME", typeof(string));
                                dtLedger.Columns.Add("BANKACTYPE", typeof(string));
                                dtLedger.Columns.Add("IFSCODE", typeof(string));
                                dtLedger.Columns.Add("ADDRESS", typeof(string));
                                dtLedger.Columns.Add("PAN/IT", typeof(string));
                                dtLedger.Columns.Add("NAMEONPAN", typeof(string));
                                dtLedger.Columns.Add("TDSDEDUCTEETYPE", typeof(string));

                                //string[] ledgercolumns = { "LEDGER_ID", "NAME", "OPENINGBALANCE", "PARENT", "ISCOSTCENTRESON", 
                                //                        "BANKACCHOLDERNAME", "BANKDETAILS", "BANKBRANCHNAME", "IFSCODE",
                                //                         "INCOMETAXNUMBER","NAMEONPAN","TDSDEDUCTEETYPE"};
                                DataTable dtTallyXMLLedger = TallyResponseDataSet.Tables["LEDGER"];

                                //4. Address ------------------------------------------------------------------------------------
                                // suntry creditor/debtpr have multi address line (address1, address2), make them into single line with (,)
                                DataTable dtTallyAddressList = TallyResponseDataSet.Tables["ADDRESS.LIST"];
                                DataTable dtTallyAddress = TallyResponseDataSet.Tables["ADDRESS"];
                                //Make multi addess into single Address field          
                                if (dtTallyAddressList != null)
                                {
                                    var dtTallySingleAddress = dtTallyAddress.AsEnumerable()
                                                .GroupBy(row => row.Field<Int32>("ADDRESS.LIST_ID"))
                                                .Select(g =>
                                                {
                                                    var row = dtTallyAddress.NewRow();
                                                    row.ItemArray = new object[]
                                            {
                                                string.Join(",", 
                                                          g.Select(r => r.Field<string>("ADDRESS_TEXT"))),
                                                g.Key 
                                            };
                                                    return row;
                                                }).CopyToDataTable();

                                    var resultAddress = from drTallyAddressList in dtTallyAddressList.AsEnumerable()
                                                        join drTallySingleAddress in dtTallySingleAddress.AsEnumerable()
                                                            on drTallyAddressList.Field<Int32>("ADDRESS.LIST_ID") equals drTallySingleAddress.Field<Int32>("ADDRESS.LIST_ID")
                                                        select new
                                                        {
                                                            LEDGER_ID = drTallyAddressList.Field<Int32>("LEDGER_ID"),
                                                            ADDRESS = drTallySingleAddress.Field<string>("ADDRESS_TEXT"),
                                                        };
                                }
                                //Address ------------------------------------------------------------------------------------

                                // Bank Account Ledger Type (SB, FD)
                                DataTable dtTallyAccountTypeList = TallyResponseDataSet.Tables["_UDF_788567360.LIST"];
                                DataTable dtTallyAccountType = TallyResponseDataSet.Tables["_UDF_788567360"];

                                if (dtTallyAccountTypeList != null)
                                {
                                    var resultBankAccountType = from drTallyAccountTypeList in dtTallyAccountTypeList.AsEnumerable()
                                                                join drTallyAccountType in dtTallyAccountType.AsEnumerable()
                                                                on drTallyAccountTypeList.Field<Int32>("_UDF_788567360.LIST_ID") equals drTallyAccountType.Field<Int32>("_UDF_788567360.LIST_ID")
                                                                into bankaccounttypejoin
                                                                from drTallyAccountType in bankaccounttypejoin.DefaultIfEmpty()
                                                                select new
                                                                {
                                                                    LEDGER_ID = drTallyAccountTypeList.Field<Int32>("LEDGER_ID"),
                                                                    UDF_788567360_LIST_ID = drTallyAccountTypeList.Field<Int32>("_UDF_788567360.LIST_ID"),
                                                                    BANK_AC_TYPE = (drTallyAccountType == null ? string.Empty : drTallyAccountType.Field<String>("_UDF_788567360_TEXT"))
                                                                };
                                }
                                //------------------------------------------------------------------------------
                                bool isdonorenabled = IsDonorModuleEnabled;
                                var resultledger = from drTallyXMLLedger in dtTallyXMLLedger.AsEnumerable()
                                                   join drTallyODBCLedger in dtTallyODBCLedger.AsEnumerable()
                                                   on drTallyXMLLedger.Field<string>("NAME") equals drTallyODBCLedger.Field<string>("$Name")
                                                   into ledgerjoin
                                                   from drTallyODBCLedger in ledgerjoin.AsEnumerable()
                                                   //join drAddress in resultAddress.AsEnumerable()
                                                   //on drTallyXMLLedger.Field<Int32>("LEDGER_ID") equals drAddress.LEDGER_ID
                                                   //into addressjoin
                                                   //from drAddress in addressjoin.DefaultIfEmpty()
                                                   //join drBankAccountType in resultBankAccountType.AsEnumerable()
                                                   //on drTallyXMLLedger.Field<Int32>("LEDGER_ID") equals drBankAccountType.LEDGER_ID
                                                   //into bankaccounttypejoin
                                                   //from drBankAccountType in bankaccounttypejoin.DefaultIfEmpty()
                                                   select dtLedger.LoadDataRow(new object[] 
                                                   {
                                                       drTallyXMLLedger.Field<Int32>("LEDGER_ID"),
                                                       drTallyXMLLedger.Field<string>("NAME"),
                                                       drTallyXMLLedger.Field<string>("PARENT"),
                                                       (drTallyODBCLedger==null?string.Empty:drTallyODBCLedger.Field<string>("$_PrimaryGroup")),
                                                       drTallyXMLLedger.Field<string>("OPENINGBALANCE"),
                                                       drTallyXMLLedger.Field<string>("ISCOSTCENTRESON"),
                                                       (isdonorenabled?drTallyXMLLedger.Field<string>("BANKACCHOLDERNAME"):string.Empty),
                                                       drTallyXMLLedger.Field<string>("BANKDETAILS"),
                                                       drTallyXMLLedger.Field<string>("BANKBRANCHNAME"),
                                                       string.Empty, //(drBankAccountType==null?string.Empty:drBankAccountType.BANK_AC_TYPE),
                                                       drTallyXMLLedger.Field<string>("IFSCODE"),
                                                       string.Empty, //(drAddress==null?string.Empty:drAddress.ADDRESS),
                                                       drTallyXMLLedger.Field<string>("INCOMETAXNUMBER"),
                                                       (isdonorenabled?drTallyXMLLedger.Field<string>("NAMEONPAN"):string.Empty),
                                                       drTallyXMLLedger.Field<string>("TDSDEDUCTEETYPE")
                                                   }, false);
                                dtLedger = resultledger.CopyToDataTable();
                                resultarg.DataSource.Data = dtLedger;
                                resultarg.Success = true;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = "Tally.ERP 9 is not running";
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {

            }

            return resultarg;
        }

        /// <summary>
        /// To Retriew groups from tally 
        /// 1. Get all the groups from tally odbc
        /// 2. Map groups which are attached with primary (Nature of Groups)
        /// (Assets, Liabilities, Incomes, Expenses)
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchGroups()
        {
            ResultArgs resultarg = new ResultArgs();
            //Retrivew groups list from Tally ODBC SQL
            resultarg = ExecuteTally("SELECT $Name, $Parent FROM groups");

            if (resultarg.Success)
            {
                DataTable dtGroups = resultarg.DataSource.Table;
                DataTable dtprimaryGroups = GetTallyPrimaryGroups();

                try
                {
                    DataTable dtTallyGroups = new DataTable();
                    dtTallyGroups.Columns.Add("Group", typeof(string));
                    dtTallyGroups.Columns.Add("Parent", typeof(string));
                    dtTallyGroups.Columns.Add("Nature", typeof(string));
                    // Primary
                    var result = from drGroups in dtGroups.AsEnumerable()
                                 join drprimaryGroups in dtprimaryGroups.AsEnumerable()
                                    on drGroups.Field<string>("$Name") equals drprimaryGroups.Field<string>("PrimaryGroup")
                                    into primaryjoin
                                 from drprimaryGroups in primaryjoin.DefaultIfEmpty()
                                 select dtTallyGroups.LoadDataRow(new object[]
                                {
                                    drGroups.Field<string>("$Name"),
                                    drGroups.Field<string>("$Parent"),
                                    (drprimaryGroups==null?string.Empty:drprimaryGroups.Field<string>("Nature")),
                                }, false);
                    dtTallyGroups = result.CopyToDataTable();
                    dtTallyGroups.DefaultView.Sort = "Parent";
                    resultarg.DataSource.Data = dtTallyGroups.DefaultView.Table;
                    resultarg.Success = true;
                    //-----------------------------------------------------------------------
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.Contains("Unable to connect to the remote server"))
                    {
                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
                    }
                    else
                    {
                        resultarg.Message = ex.Message;
                    }

                }
                finally
                {

                }
            }
            return resultarg;
        }

        /// <summary>
        /// To Retriew Voucer Types from Tally ODBC
        /// 1. Select voucher type with its setting by using tally sql odbc
        /// 2. Select voucher type with its setting by using tally xml (for map base voucher type)
        /// 3. select recent prefix and suffix settings
        /// 4. mapp all seetings to voucher type
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchVoucherTypes()
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            DataTable dtSQLTallyVoucherType = new DataTable();

            //Retrivew Voucher list from Tally ODBC SQL
            resultarg = ExecuteTally("SELECT $Name,$Parent, $NumberingMethod, $BeginningNumber, $PrefillZero, " +
                                    "$WidthofNumber, $_TotalVouchers FROM VoucherType " +
                                    "WHERE $Parent IN ('RECEIPT','PAYMENT','CONTRA','JOURNAL')");

            if (resultarg.Success)
            {
                dtSQLTallyVoucherType = resultarg.DataSource.Table;

                //Retrivew Voucher list from Tally XML(to get base voucher type
                string xml = "<ENVELOPE>" +
                                "<HEADER>" +
                                "<TALLYREQUEST>Export Data</TALLYREQUEST>" +
                                "</HEADER>" +
                                "<BODY>" +
                                "<EXPORTDATA>" +
                                "<REQUESTDESC>" +
                                "<REPORTNAME>List of Accounts</REPORTNAME>" +
                                "<STATICVARIABLES>" +
                                "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                                "<ACCOUNTTYPE>Voucher Types</ACCOUNTTYPE>" +    //Groups
                                "<ENCODINGTYPE>UNICODE</ENCODINGTYPE>" +
                                "</STATICVARIABLES>" +
                                "</REQUESTDESC>" +
                                "</EXPORTDATA>" +
                                "</BODY>" +
                                "</ENVELOPE>";
                try
                {
                    resultarg = ExecuteTallyXML(xml, TallyMasters.VoucherType);
                    if (resultarg.Success)
                    {
                        TallyResponseDataSet = resultarg.DataSource.TableSet;
                        if (TallyResponseDataSet != null)
                        {
                            if (TallyResponseDataSet.Tables["VOUCHERTYPE"] != null)
                            {
                                DataTable dtXMLTallyVoucherType = TallyResponseDataSet.Tables["VOUCHERTYPE"];

                                //Map Voucher Type which was taken by Tally ODBC SQL and Tally XML -----------------------------------------------------------
                                // For map Voucher base voucher type (RECEIPTS, PAYMENTS, CONTRA and JOURNAL
                                DataTable dtTallyVoucherType = new DataTable();
                                dtTallyVoucherType.Columns.Add("VOUCHERTYPE_ID", typeof(Int32));
                                dtTallyVoucherType.Columns.Add("NAME", typeof(string));
                                dtTallyVoucherType.Columns.Add("BASEVOUCHERTYPE", typeof(string));
                                dtTallyVoucherType.Columns.Add("NUMBERINGMETHOD", typeof(string));
                                dtTallyVoucherType.Columns.Add("BEGINNINGNUMBER", typeof(double));
                                dtTallyVoucherType.Columns.Add("PREFILLZERO", typeof(double));
                                dtTallyVoucherType.Columns.Add("WIDTHOFNUMBER", typeof(double));
                                dtTallyVoucherType.Columns.Add("RECENTVOUCHER", typeof(double));

                                var result = from drSQLTallyVoucherType in dtSQLTallyVoucherType.AsEnumerable()
                                             join drXMLTallyVoucherType in dtXMLTallyVoucherType.AsEnumerable()
                                             on drSQLTallyVoucherType.Field<string>("$Name") equals drXMLTallyVoucherType.Field<string>("Name")
                                             select dtTallyVoucherType.LoadDataRow(new object[]
                                             {
                                                drXMLTallyVoucherType.Field<Int32>("VOUCHERTYPE_Id"),
                                                drXMLTallyVoucherType.Field<string>("NAME"),
                                                drXMLTallyVoucherType.Field<string>("PARENT"),
                                                drSQLTallyVoucherType.Field<string>("$NUMBERINGMETHOD"),
                                                drSQLTallyVoucherType.Field<double>("$BEGINNINGNUMBER"),
                                                drSQLTallyVoucherType.Field<double>("$PREFILLZERO"),
                                                drSQLTallyVoucherType.Field<double>("$WIDTHOFNUMBER"),
                                                drSQLTallyVoucherType.Field<double>("$_TOTALVOUCHERS"),
                                              }, false);
                                dtTallyVoucherType = result.CopyToDataTable();
                                //-----------------------------------------------------------------------

                                //Select Recent PREFIX, SUFIX Settings---------------------------------------------------------
                                DataTable dtPreFix = new DataTable();
                                dtPreFix.Columns.Add("VOUCHERTYPE_Id", typeof(Int32));
                                dtPreFix.Columns.Add("NAME", typeof(string));

                                DataTable dtSurfix = new DataTable();
                                dtSurfix.Columns.Add("VOUCHERTYPE_Id", typeof(Int32));
                                dtSurfix.Columns.Add("NAME", typeof(string));

                                if (TallyResponseDataSet.Tables["PREFIXLIST.LIST"] != null &&
                                   TallyResponseDataSet.Tables["SUFFIXLIST.LIST"] != null)
                                {
                                    DataTable dtPrefixVoucherType = TallyResponseDataSet.Tables["PREFIXLIST.LIST"];
                                    DataTable dtSuffixVoucherType = TallyResponseDataSet.Tables["SUFFIXLIST.LIST"];

                                    var chk = from r in dtPrefixVoucherType.AsEnumerable()
                                              let id = r.Field<int>("VOUCHERTYPE_Id")
                                              group r by id into g
                                              select g.OrderByDescending(r => r.Field<string>("DATE")).First();
                                    dtPreFix = chk.CopyToDataTable();

                                    chk = from r in dtPrefixVoucherType.AsEnumerable()
                                          let id = r.Field<int>("VOUCHERTYPE_Id")
                                          group r by id into g
                                          select g.OrderByDescending(r => r.Field<string>("DATE")).First();
                                    dtSurfix = chk.CopyToDataTable();
                                }
                                else
                                {
                                    DataRow drPrefix = dtPreFix.NewRow();
                                    drPrefix["VOUCHERTYPE_Id"] = -1;
                                    drPrefix["NAME"] = string.Empty;
                                    dtPreFix.Rows.Add(drPrefix);

                                    DataRow drSurfix = dtSurfix.NewRow();
                                    drSurfix["VOUCHERTYPE_Id"] = -1;
                                    drSurfix["NAME"] = string.Empty;
                                    dtSurfix.Rows.Add(drSurfix);
                                }
                                //-----------------------------------------------------------------------

                                //Prepare final Voucher Setting--------------------------------------------------
                                DataTable dtVoucherSetting = new DataTable("VoucherType");
                                dtVoucherSetting.Columns.Add("VOUCHERTYPE", typeof(string));
                                dtVoucherSetting.Columns.Add("BASEVOUCHERTYPE", typeof(string));
                                dtVoucherSetting.Columns.Add("NUMBERINGMETHOD", typeof(string));
                                dtVoucherSetting.Columns.Add("BEGINNINGNUMBER", typeof(double));
                                dtVoucherSetting.Columns.Add("PREFILLZERO", typeof(double));
                                dtVoucherSetting.Columns.Add("WIDTHOFNUMBER", typeof(double));
                                dtVoucherSetting.Columns.Add("PREFIX", typeof(string));
                                dtVoucherSetting.Columns.Add("SUFFIX", typeof(string));
                                dtVoucherSetting.Columns.Add("RECENTVOUCHER", typeof(double));

                                result = from drTallyVoucherType in dtTallyVoucherType.AsEnumerable()
                                         join drPreFix in dtPreFix.AsEnumerable()
                                            on drTallyVoucherType.Field<Int32>("VOUCHERTYPE_Id") equals drPreFix.Field<Int32>("VOUCHERTYPE_Id")
                                            into prefixjoin
                                         from drPreFix in prefixjoin.DefaultIfEmpty()
                                         join drSurfix in dtSurfix.AsEnumerable()
                                            on drTallyVoucherType.Field<Int32>("VOUCHERTYPE_Id") equals drSurfix.Field<Int32>("VOUCHERTYPE_Id")
                                            into sufixjoin
                                         from drSufix in sufixjoin.DefaultIfEmpty()
                                         select dtVoucherSetting.LoadDataRow(new object[]
                                             {
                                                drTallyVoucherType.Field<string>("NAME"),
                                                drTallyVoucherType.Field<string>("BASEVOUCHERTYPE"),
                                                drTallyVoucherType.Field<string>("NUMBERINGMETHOD"),
                                                drTallyVoucherType.Field<double>("BEGINNINGNUMBER"),
                                                drTallyVoucherType.Field<double>("PREFILLZERO"),
                                                drTallyVoucherType.Field<double>("WIDTHOFNUMBER"),
                                                (drPreFix==null?string.Empty:drPreFix.Field<string>("NAME")), //Prefix
                                                 (drSufix==null?string.Empty:drSufix.Field<string>("NAME")),  //Sufix
                                                drTallyVoucherType.Field<double>("RECENTVOUCHER"),
                                              }, false);
                                dtVoucherSetting = result.CopyToDataTable();
                                dtVoucherSetting.TableName = "VoucherType";
                                //--------------------------------------------------------------------------

                                resultarg.DataSource.Data = dtVoucherSetting;
                                resultarg.Success = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.Contains("Unable to connect to the remote server"))
                    {
                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
                    }
                    else
                    {
                        resultarg.Message = ex.Message;
                    }

                }
                finally
                {
                }
            }
            return resultarg;
        }

        /// <summary>
        ///Get Donor Master list if donor module is enabled in tally (CC CATEGORY='DONORS')
        ///Donor extra information like (Donor Type, State, Country, Address)
        ///Those information are avilable in extra tables in tally
        ///1. _UDF_788529213.LIST, _UDF_788529213: DONOR TYPE
        ///2. _UDF_788567383.LIST, _UDF_788567383: STATES
        ///3. _UDF_788529212.LIST, _UDF_788529212: COUNTRIES
        ///4. ADDRESS.LIST, ADDRESS : ADDRESS
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchDonorsMaster()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                //Get General CC list from tally odbc
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    //Filter cc only category is donors
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                    dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='DONORS'";
                    dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                    if (dtCCTallyMaster != null)
                    {
                        DataTable dtDonorMaster = new DataTable();
                        dtDonorMaster.Columns.Add("DONOR_ID", typeof(Int32));
                        dtDonorMaster.Columns.Add("NAME", typeof(String));
                        dtDonorMaster.Columns.Add("CATEGORY", typeof(String));
                        dtDonorMaster.Columns.Add("PARENT", typeof(String));
                        dtDonorMaster.Columns.Add("DONORTYPE", typeof(String));
                        dtDonorMaster.Columns.Add("PANNUMBER", typeof(String));
                        dtDonorMaster.Columns.Add("ADDRESS", typeof(String));
                        dtDonorMaster.Columns.Add("STATE", typeof(String));
                        dtDonorMaster.Columns.Add("COUNTRY", typeof(String));
                        dtDonorMaster.Columns.Add("MOBILENUMBER", typeof(String));
                        dtDonorMaster.Columns.Add("EMAILID", typeof(String));

                        //Get donor extra information like (Donor Type, State, Country, Address)
                        //Those information are avilable in extra tables in tally
                        //1. _UDF_788529213.LIST, _UDF_788529213: DONOR TYPE
                        //2. _UDF_788567383.LIST, _UDF_788567383: STATES
                        //3. _UDF_788529212.LIST, _UDF_788529212: COUNTRIES
                        //4. ADDRESS.LIST, ADDRESS : ADDRESS
                        DataTable dtDonorExtraInfo = new DataTable();
                        dtDonorExtraInfo.Columns.Add("COSTCENTRE_Id", typeof(Int32));
                        dtDonorExtraInfo.Columns.Add("DONORTYPE", typeof(string));
                        dtDonorExtraInfo.Columns.Add("STATE", typeof(string));
                        dtDonorExtraInfo.Columns.Add("COUNTRY", typeof(string));
                        dtDonorExtraInfo.Columns.Add("ADDRESS", typeof(string));

                        //Check whether donor information module enabled in tally
                        if (TallyResponseDataSet.Tables["_UDF_788529213"] != null)
                        {
                            //1. Donor Type -------------------------------------------------------------------------------
                            DataTable dtTallyDonorTypeList = TallyResponseDataSet.Tables["_UDF_788529213.LIST"];
                            DataTable dtTallyDonorType = TallyResponseDataSet.Tables["_UDF_788529213"];
                            var resultDonorType = from drDonorTypeList in dtTallyDonorTypeList.AsEnumerable()
                                                  join drDonorType in dtTallyDonorType.AsEnumerable()
                                                  on drDonorTypeList.Field<Int32>("_UDF_788529213.LIST_Id") equals drDonorType.Field<Int32>("_UDF_788529213.LIST_Id")
                                                  select new
                                                  {
                                                      COSTCENTRE_ID = drDonorTypeList.Field<Int32>("COSTCENTRE_ID"),
                                                      DONORTYPE = drDonorType.Field<string>("_UDF_788529213_TEXT"),
                                                  };
                            //Donor Type -------------------------------------------------------------------------------

                            //2. State ----------------------------------------------------------------------------------
                            DataTable dtTallyStateList = TallyResponseDataSet.Tables["_UDF_788567383.LIST"];
                            DataTable dtTallyState = TallyResponseDataSet.Tables["_UDF_788567383"];
                            var resultState = from drTallyStateList in dtTallyStateList.AsEnumerable()
                                              join drTallyState in dtTallyState.AsEnumerable()
                                                   on drTallyStateList.Field<Int32>("_UDF_788567383.LIST_Id") equals drTallyState.Field<Int32>("_UDF_788567383.LIST_Id")
                                              select new
                                              {
                                                  COSTCENTRE_ID = drTallyStateList.Field<Int32>("COSTCENTRE_ID"),
                                                  STATE = drTallyState.Field<string>("_UDF_788567383_TEXT"),
                                              };
                            //State ----------------------------------------------------------------------------------

                            //3. Country -----------------------------------------------------------------------------------
                            DataTable dtTallyCountryList = TallyResponseDataSet.Tables["_UDF_788529212.LIST"];
                            DataTable dtTallyCountry = TallyResponseDataSet.Tables["_UDF_788529212"];
                            var resultCountry = from drTallyCountryList in dtTallyCountryList.AsEnumerable()
                                                join drTallyCountry in dtTallyCountry.AsEnumerable()
                                                   on drTallyCountryList.Field<Int32>("_UDF_788529212.LIST_Id") equals drTallyCountry.Field<Int32>("_UDF_788529212.LIST_Id")
                                                select new
                                                {
                                                    COSTCENTRE_ID = drTallyCountryList.Field<Int32>("COSTCENTRE_ID"),
                                                    COUNTRY = drTallyCountry.Field<string>("_UDF_788529212_TEXT"),
                                                };
                            //Country ----------------------------------------------------------------------------------------

                            //4. Address ------------------------------------------------------------------------------------
                            // Each CC(DONOR) has multi address line (address1, address2), make them into single line with (,)
                            DataTable dtTallyAddressList = TallyResponseDataSet.Tables["ADDRESS.LIST"];
                            DataTable dtTallyAddress = TallyResponseDataSet.Tables["ADDRESS"];
                            //Make multi addess into single Address field          
                            var dtTallySingleAddress = dtTallyAddress.AsEnumerable()
                                        .GroupBy(row => row.Field<Int32>("ADDRESS.LIST_ID"))
                                        .Select(g =>
                                        {
                                            var row = dtTallyAddress.NewRow();
                                            row.ItemArray = new object[]
                                            {
                                                string.Join(",", 
                                                          g.Select(r => r.Field<string>("ADDRESS_TEXT"))),
                                                g.Key 
                                            };
                                            return row;
                                        }).CopyToDataTable();

                            var resultAddress = from drTallyAddressList in dtTallyAddressList.AsEnumerable()
                                                join drTallySingleAddress in dtTallySingleAddress.AsEnumerable()
                                                    on drTallyAddressList.Field<Int32>("ADDRESS.LIST_ID") equals drTallySingleAddress.Field<Int32>("ADDRESS.LIST_ID")
                                                select new
                                                {
                                                    COSTCENTRE_ID = drTallyAddressList.Field<Int32>("COSTCENTRE_ID"),
                                                    ADDRESS = drTallySingleAddress.Field<string>("ADDRESS_TEXT"),
                                                };
                            //Address ------------------------------------------------------------------------------------

                            // Combin all extration information into single datatable(dtDonorExtraInfo)
                            var resultDonorExtraInfo = from drDonorType in resultDonorType.AsEnumerable()
                                                       join drCountry in resultCountry.AsEnumerable()
                                                            on drDonorType.COSTCENTRE_ID equals drCountry.COSTCENTRE_ID
                                                            into countryjoin
                                                       from drCountry in countryjoin.DefaultIfEmpty()
                                                       join drState in resultState.AsEnumerable()
                                                            on drDonorType.COSTCENTRE_ID equals drState.COSTCENTRE_ID
                                                            into statejoin
                                                       from drState in statejoin.DefaultIfEmpty()
                                                       join drAddress in resultAddress.AsEnumerable()
                                                            on drDonorType.COSTCENTRE_ID equals drAddress.COSTCENTRE_ID
                                                            into addressjoin
                                                       from drAddress in addressjoin.DefaultIfEmpty()
                                                       select dtDonorExtraInfo.LoadDataRow(new object[]
                                                    {
                                                        drDonorType.COSTCENTRE_ID,
                                                        drDonorType.DONORTYPE,
                                                        (drState==null)?string.Empty:drState.STATE,
                                                        (drCountry==null)?string.Empty:drCountry.COUNTRY, 
                                                        (drAddress==null)?string.Empty:drAddress.ADDRESS, 
                                                    }, false);

                            dtDonorExtraInfo = resultDonorExtraInfo.CopyToDataTable();

                            var ccList = dtDonorExtraInfo.AsEnumerable();
                            var bannedCCList = dtCCTallyMaster.AsEnumerable();

                            //var rows = from t1 in bannedCCList.AsEnumerable()
                            //           join t2 in ccList.AsEnumerable()
                            //                on t1.Field<int>("COSTCENTRE_ID") equals t2.Field<int>("COSTCENTRE_ID") into tg
                            //                from tcheck in tg.DefaultIfEmpty()
                            //           where tcheck == null
                            //           select t1;
                            //DataTable boundTable = rows.CopyToDataTable<DataRow>();

                            //Combin CC(Donor basic info) with extration information into single datatable (dtDonorMaster)
                            var result = from drCC in dtCCTallyMaster.AsEnumerable()
                                         join drDonorExtraInfo in dtDonorExtraInfo.AsEnumerable()
                                             on drCC.Field<Int32>("COSTCENTRE_ID") equals drDonorExtraInfo.Field<Int32>("COSTCENTRE_ID")
                                             into donorextrainfo
                                         from drDonorExtraInfo in donorextrainfo.DefaultIfEmpty()
                                         select dtDonorMaster.LoadDataRow(new object[]
                             {
                                drCC.Field<Int32>("COSTCENTRE_ID"),
                                drCC.Field<string>("NAME"),
                                drCC.Field<string>("CATEGORY"),
                                drCC.Field<string>("PARENT"),
                                (drDonorExtraInfo==null?string.Empty:drDonorExtraInfo.Field<string>("DONORTYPE")),
                                drCC.Field<string>("PANNUMBER"),
                                (drDonorExtraInfo==null?string.Empty:drDonorExtraInfo.Field<string>("ADDRESS")),
                                (drDonorExtraInfo==null?string.Empty:drDonorExtraInfo.Field<string>("STATE")),
                                (drDonorExtraInfo==null?string.Empty:drDonorExtraInfo.Field<string>("COUNTRY")),
                                drCC.Field<string>("MOBILENUMBER"),
                                drCC.Field<string>("EMAILID"),
                              }, false);
                            dtDonorMaster = result.CopyToDataTable();
                        }
                        resultarg.DataSource.Data = dtDonorMaster;
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }
            }
            finally
            {

            }

            return resultarg;
        }

        /// <summary>
        /// Get Sponsers List if donor module is enabled in tally  (CC CATEGORY='SPONSERS')
        /// Yet to be finalized sponors extra infomration
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchSponsers()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                    dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='SPONSERS'";
                    dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                    if (dtCCTallyMaster != null)
                    {
                        DataTable dtSponsers = new DataTable();
                        dtSponsers.Columns.Add("SPONSER_ID", typeof(Int32));
                        dtSponsers.Columns.Add("SPONSER", typeof(String));
                        dtSponsers.Columns.Add("CATEGORY", typeof(String));

                        //Check whether donor module enabled in tally
                        if (TallyResponseDataSet.Tables["_UDF_788529213"] != null)
                        {
                            string[] sponserscols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                            dtSponsers = dtCCTallyMaster.DefaultView.ToTable("SPONSERS", false, sponserscols);
                        }
                        resultarg.DataSource.Data = dtSponsers;
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {

            }

            return resultarg;
        }

        /// <summary>
        /// Get Purpose list if donor module enabled in tally (CC CATEGORY='PURPOSES')
        /// Purpose information are avilable in _UDF_687904103
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchPurposes()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                    dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='PURPOSES'";
                    dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                    if (dtCCTallyMaster != null)
                    {
                        DataTable dtPurposes = new DataTable();
                        dtPurposes.Columns.Add("PURPOSE_ID", typeof(Int32));
                        dtPurposes.Columns.Add("PURPOSE", typeof(String));
                        dtPurposes.Columns.Add("CATEGORY", typeof(String));

                        //Check whether donor/purpose module enabled in tally
                        if (TallyResponseDataSet.Tables["_UDF_687904103"] != null)
                        {
                            string[] purposecols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                            dtPurposes = dtCCTallyMaster.DefaultView.ToTable("PURPOSES", false, purposecols);
                        }
                        resultarg.DataSource.Data = dtPurposes;
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {

            }

            return resultarg;
        }

        /// <summary>
        /// Get States List if donor module enabled in tally (CC CATEGORY='STATES')
        /// State and its country details are avilable in tally UDF_788567382
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchStates()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                    dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='STATES'";
                    dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                    if (dtCCTallyMaster != null)
                    {
                        DataTable dtStates = new DataTable();
                        dtStates.Columns.Add("STATE_ID", typeof(Int32));
                        dtStates.Columns.Add("STATE", typeof(String));
                        dtStates.Columns.Add("COUNTRY", typeof(String));
                        dtStates.Columns.Add("CATEGORY", typeof(String));


                        //Check whether donor/state information module enabled in tally
                        if (TallyResponseDataSet.Tables["_UDF_788567382"] != null)
                        {
                            string[] statecols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                            DataTable dtTallyStates = dtCCTallyMaster.DefaultView.ToTable("STATES", false, statecols);

                            //1. Country ----------------------------------------------------------------------------
                            DataTable dtTallyCountryList = TallyResponseDataSet.Tables["_UDF_788567382.LIST"];
                            DataTable dtTallyCountry = TallyResponseDataSet.Tables["_UDF_788567382"];
                            var resultCountry = from drTallyCountryList in dtTallyCountryList.AsEnumerable()
                                                join drTallyCountry in dtTallyCountry.AsEnumerable()
                                                   on drTallyCountryList.Field<Int32>("_UDF_788567382.LIST_Id") equals drTallyCountry.Field<Int32>("_UDF_788567382.LIST_Id")
                                                select new
                                                {
                                                    COSTCENTRE_ID = drTallyCountryList.Field<Int32>("COSTCENTRE_ID"),
                                                    COUNTRY = drTallyCountry.Field<string>("_UDF_788567382_TEXT"),
                                                };
                            //Country -------------------------------------------------------------------------------------------

                            var result = from drTallyStates in dtTallyStates.AsEnumerable()
                                         join drCountry in resultCountry.AsEnumerable()
                                             on drTallyStates.Field<Int32>("COSTCENTRE_ID") equals drCountry.COSTCENTRE_ID
                                             into countryjoin
                                         from drCountry in countryjoin.DefaultIfEmpty()
                                         select dtStates.LoadDataRow(new object[]
                             {
                                drTallyStates.Field<Int32>("COSTCENTRE_ID"),
                                drTallyStates.Field<string>("NAME"),
                                (drCountry==null?string.Empty:drCountry.COUNTRY),
                                drTallyStates.Field<string>("CATEGORY")
                              }, false);
                            dtStates = result.CopyToDataTable();
                        }
                        resultarg.DataSource.Data = dtStates;
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {

            }

            return resultarg;
        }

        /// <summary>
        /// Get Countries list if donor module enabled in tally (CC CATEGORY='COUNTRIES')
        /// which is avilabe in _UDF_553648184
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchCountries()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                    dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='COUNTRIES'";
                    dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                    if (dtCCTallyMaster != null)
                    {
                        DataTable dtCountries = new DataTable();
                        dtCountries.Columns.Add("COUNTRY_ID", typeof(Int32));
                        dtCountries.Columns.Add("COUNTRY", typeof(String));
                        dtCountries.Columns.Add("CATEGORY", typeof(String));

                        //Check whether donor/country information module enabled in tally
                        if (TallyResponseDataSet.Tables["_UDF_553648184"] != null)
                        {
                            string[] countrycols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                            dtCountries = dtCCTallyMaster.DefaultView.ToTable("COUNTRIES", false, countrycols);
                        }
                        resultarg.DataSource.Data = dtCountries;
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {

            }

            return resultarg;
        }

        /// <summary>
        /// Retrive records from tally for given master tables
        /// </summary>
        /// <param name="AcMEERPobject">enum master tables</param>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchTally(TallyMasters AcMEERPobject)
        {
            string query = string.Empty;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ResultArgs resultarg = new ResultArgs();
            switch (AcMEERPobject)
            {
                case TallyMasters.Groups:
                    query = "SELECT $Name, $Parent FROM groups";
                    break;
                //case TallyMasters.Ledgers:
                //    query = "SELECT $Name, $Parent, $_PrimaryGroup, $StartingFrom, $OpeningBalance, $BankDetails, $BankBranchName," +
                //            "$BankBSRCode, $BranchCode, $IFSCode, $BankAccHolderName, $IsCostCentresOn FROM Ledger";
                //    break;
                case TallyMasters.CostCategory:
                    query = "SELECT $Name FROM CostCategory";
                    break;
                case TallyMasters.CostCenters:
                    query = "SELECT $Name,$Category FROM AllCostCentre " +
                            " WHERE $Category NOT IN ('DONORS','SPONSERS','PURPOSES','STATES','COUNTRIES')";
                    break;
                default:
                    query = "SELECT $Name,$Parent FROM group";
                    break;
            }
            try
            {
                resultarg = IsTallyConnected;
                if (resultarg.Success)
                {
                    switch (AcMEERPobject)
                    {
                        case TallyMasters.CostCategory:
                        case TallyMasters.CostCenters:
                            resultarg = ExecuteTally(query);
                            break;
                        case TallyMasters.Groups:
                            resultarg = FetchGroups();
                            break;
                        case TallyMasters.Ledgers:
                            resultarg = FetchLedger();
                            break;
                        case TallyMasters.VoucherType:
                            resultarg = FetchVoucherTypes();
                            break;
                        case TallyMasters.Donors:
                            resultarg = FetchDonorsMaster();
                            break;
                        case TallyMasters.Sponsers:
                            resultarg = FetchSponsers();
                            break;
                        case TallyMasters.Purposes:
                            resultarg = FetchPurposes();
                            break;
                        case TallyMasters.States:
                            resultarg = FetchStates();
                            break;
                        case TallyMasters.Country:
                            resultarg = FetchCountries();
                            break;
                        default:
                            resultarg = FetchVoucherTypes();
                            break;
                    }

                    if (resultarg.Success)
                    {
                        resultarg.DataSource.Table.TableName = AcMEERPobject.ToString();
                        resultarg.Success = true;
                    }
                }
                else
                {
                    resultarg.Message = "Tally.ERP ODBC is not exists";
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
            }
            return resultarg;
        }

        #endregion

        #region private methods
        /// <summary>
        /// Execute sql in tally odbc
        /// </summary>
        /// <param name="query">sql query to be executed in tally odbc</param>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        private ResultArgs ExecuteTally(string query)
        {
            DataTable dt = new DataTable();
            ResultArgs resultarg = new ResultArgs();

            try
            {
                resultarg = IsTallyConnected;
                if (resultarg.Success)
                {
                    using (OdbcConnection con = new OdbcConnection(resultarg.ReturnValue.ToString()))
                    {
                        using (OdbcCommand cmd = new OdbcCommand(query, con))
                        {
                            con.Open();
                            using (OdbcDataAdapter da = new OdbcDataAdapter(query, con))
                            {
                                da.Fill(dt);
                            }
                            //if (con != null && con.State == ConnectionState.Open) con.Close();
                            resultarg.DataSource.Data = dt;
                            resultarg.Success = true;
                        }
                    }
                }
                else
                {
                    resultarg.Message = "Tally.ERP ODBC is not exists";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Data source name not found"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }
            }
            return resultarg;
        }

        /// <summary>
        /// Execute Tally xml in tally odbc server
        /// if cc xml, remove cc category from xml for duplicate error (Remove CostCategory for the error  
        /// (Cannot add a column named 'NAME':a nested table with the same name already belongs to this DataTable)
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="enumTallyMaster"></param>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        private ResultArgs ExecuteTallyXML(string xml, TallyMasters enumTallyMaster)
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();

            if (!string.IsNullOrEmpty(xml))
            {
                WebRequest TallyRequest;
                byte[] byteArray;
                WebResponse response;
                Stream dataStream;
                try
                {
                    TallyRequest = WebRequest.Create("http://" + GetTallyServer + ":" + GetTallyPort);
                    ((HttpWebRequest)TallyRequest).UserAgent = ".NET Framework Example Client";
                    TallyRequest.Method = "POST";
                    string postData = xml;
                    byteArray = Encoding.UTF8.GetBytes(postData);
                    TallyRequest.ContentType = "application/x-www-form-urlencoded";
                    TallyRequest.ContentLength = byteArray.Length;
                    dataStream = TallyRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    response = TallyRequest.GetResponse();
                    string Response = (((HttpWebResponse)response).StatusDescription).ToString();
                    dataStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        string responseFromTallyServer = reader.ReadToEnd().ToString();
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(responseFromTallyServer);


                        //Remove CostCategory/GROUP for the error (Cannot add a column named 'NAME': 
                        //a nested table with the same name already belongs to this DataTable)
                        //for when we take CC and Ledger
                        if (enumTallyMaster == TallyMasters.CostCenters ||
                            enumTallyMaster == TallyMasters.Ledgers)
                        {
                            string removeTag = (enumTallyMaster == TallyMasters.CostCenters ?
                                                "//TALLYMESSAGE//COSTCATEGORY" : "//TALLYMESSAGE//GROUP");
                            XmlNodeList xmlchild = xmldoc.SelectNodes(removeTag);
                            foreach (XmlNode node in xmlchild)
                            {
                                node.ParentNode.RemoveChild(node);
                            }
                        }


                        XmlNodeReader xmlReader = new XmlNodeReader(xmldoc);
                        TallyResponseDataSet.ReadXml(xmlReader);
                    }
                    dataStream.Close();
                    response.Close();

                    resultarg.DataSource.Data = TallyResponseDataSet;
                    resultarg.Success = true;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.Contains("Unable to connect to the remote server"))
                    {
                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
                    }
                    else
                    {
                        resultarg.Message = ex.Message;
                    }

                }
                finally
                {
                    byteArray = null;
                    response = null;
                    dataStream = null;
                }

                return resultarg;
            }
            return resultarg;
        }

        /// <summary>
        /// Retrive CC Master from tally xml odbc for splitting DONORS, PURPOSES, STATES, COUNTRIES and SPONSERS
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        private ResultArgs FetchTallyGeneralCCMaster()
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();

            try
            {
                string xml = "<ENVELOPE>" +
                            "<HEADER>" +
                            "<TALLYREQUEST>Export Data</TALLYREQUEST>" +
                            "</HEADER>" +
                            "<BODY>" +
                            "<EXPORTDATA>" +
                            "<REQUESTDESC>" +
                            "<REPORTNAME>List of Accounts</REPORTNAME>" +
                            "<STATICVARIABLES>" +
                            "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                            "<ACCOUNTTYPE>COSTCENTRES</ACCOUNTTYPE>" +    //Groups
                            "<ENCODINGTYPE>UNICODE</ENCODINGTYPE>" +
                            "</STATICVARIABLES>" +
                            "</REQUESTDESC>" +
                            "</EXPORTDATA>" +
                            "</BODY>" +
                            "</ENVELOPE>";
                resultarg = ExecuteTallyXML(xml, TallyMasters.CostCenters);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        if (TallyResponseDataSet.Tables["COSTCENTRE"] != null)
                        {
                            resultarg.DataSource.Data = TallyResponseDataSet;
                            resultarg.Success = true;
                        }
                        else
                        {
                            resultarg.Message = "Cost Centres are not avilable";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.Contains("Unable to connect to the remote server"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }

            }
            finally
            {

            }

            return resultarg;
        }

        private static DataTable GetTallyPrimaryGroups()
        {
            DataTable dtPrimaryGroup = new DataTable();
            dtPrimaryGroup.Columns.Add("PrimaryGroup", typeof(string));
            dtPrimaryGroup.Columns.Add("Nature", typeof(string));

            //15 Primary Groups  13 Sub Groups
            string[,] primarygroups = new string[,] { { "Branch / Divisions", "Liabilities" },  { "Capital Account", "Liabilities" },
                                        { "Current Assets", "Assets" },  { "Current Liabilities", "Liabilities" },
                                        { "Direct Expenses", "Expenses" },  { "Direct Incomes", "Incomes" },
                                        { "Fixed Assets", "Assets" },  { "Indirect Expenses", "Expenses" },
                                        { "Indirect Incomes", "Incomes" },  { "Investments", "Assets" },
                                        { "Loans (Liability)", "Liabilities" },  { "Misc. Expenses (ASSET)", "Assets" },
                                        { "Purchase Accounts", "Expenses" },  { "Sales Accounts", "Incomes" },
                                        { "Suspense A/c", "Liabilities" } };

            for (int i = 0; i < primarygroups.GetLength(0); i++)
            {
                string primaryGrp = primarygroups[i, 0].ToString();
                string nature = primarygroups[i, 1].ToString();
                DataRow dr = dtPrimaryGroup.NewRow();
                dr["PrimaryGroup"] = primaryGrp;
                dr["Nature"] = nature;
                dtPrimaryGroup.Rows.Add(dr);
            }
            return dtPrimaryGroup;
        }

        private string ConvertTallyDate(string tallydate)
        {
            string converteddate = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(tallydate))
                {
                    converteddate = Convert.ToDateTime(tallydate.Substring(0, 4) + "-" +
                                        tallydate.Substring(4, 2) + "-" +
                                        tallydate.Substring(6, 2)).ToString("yyyy-MM-dd");
                }
            }
            catch
            {
                converteddate = string.Empty;
            }
            return converteddate;
        }
        #endregion

        #region General Tally Connector methods
        public ResultArgs GetTallyTables()
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = IsTallyConnected;
                if (resultarg.Success)
                {
                    using (OdbcConnection con = new OdbcConnection(resultarg.ReturnValue.ToString()))
                    {
                        con.Open();
                        string[] restrictions = new string[1];
                        resultarg.DataSource.Data = con.GetSchema("Tables");
                        //if (con != null && con.State == ConnectionState.Open) con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Data source name not found"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }
            }
            return resultarg;
        }
        public ResultArgs FetchTallyTableContent(string tblName)
        {
            DataTable dt = new DataTable();
            ResultArgs resultarg = new ResultArgs();
            string tablcoumns = GetTallyTableColumns(tblName);
            if (tablcoumns != string.Empty)
            {
                string query = "SELECT " + tablcoumns + " FROM " + tblName;

                try
                {
                    resultarg = IsTallyConnected;
                    if (resultarg.Success)
                    {
                        using (OdbcConnection con = new OdbcConnection(resultarg.ReturnValue.ToString()))
                        {
                            using (OdbcCommand cmd = new OdbcCommand(query, con))
                            {
                                con.Open();
                                using (OdbcDataAdapter da = new OdbcDataAdapter(query, con))
                                {
                                    da.Fill(dt);
                                }
                                //if (con != null && con.State == ConnectionState.Open) con.Close();
                                resultarg.DataSource.Data = dt;
                            }
                        }
                    }
                    else
                    {
                        resultarg.Message = "Tally.ERP ODBC is not exists";
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Data source name not found"))
                    {
                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
                    }
                    else
                    {
                        resultarg.Message = ex.Message;
                    }
                }
            }
            else
            {
                resultarg.Message = "Tally.ERP table columns not found";
            }
            return resultarg;
        }
        private string GetTallyTableColumns(string tblname)
        {
            ResultArgs resultarg = new ResultArgs();
            string columns = string.Empty;
            try
            {
                resultarg = IsTallyConnected;
                if (resultarg.Success)
                {
                    using (OdbcConnection con = new OdbcConnection(resultarg.ReturnValue.ToString()))
                    {
                        con.Open();
                        string[] objArrRestrict = new string[] { null, null, tblname, null };
                        DataTable dtTallyColumns = con.GetSchema("Columns", objArrRestrict);

                        foreach (DataRow drcolumn in dtTallyColumns.Rows)
                        {
                            columns += drcolumn["COLUMN_NAME"].ToString() + ",";
                        }
                        columns = columns.TrimEnd(',');
                        //if (con != null && con.State == ConnectionState.Open) con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Data source name not found"))
                {
                    resultarg.Message = MSG_TALLY_NOT_RUNNING;
                }
                else
                {
                    resultarg.Message = ex.Message;
                }
            }
            return columns;
        }


        #endregion
    }
}