/*
 * Purpose this class is used to Fetch data from Tally as well as Sending data to Tally
 *  
 * Preconditon : Before we Fetch/Send Data to Tally, 
 * 1. Tally should be running single instance with Admin Rights with one company is opened (Check IsMoreThanOneTallyRunningInstance)
 * 2. Tally should be acting as Server or Both in Client/Server Configuration and Port must be defined. Tally server IP and its ports can be defined in App.config
 * 3. If it is local tally, Server IP would be "Localhost:9000", if remote tally, remote system ip would be Tally server iP Address.
 * 4. For Sending data to tally, Tally should be Licensed version. 
 * 5. For Fetching data from tally, any version is ok.
 * 
 * Logic: Fetching Data from Tally
 * 1. Ledger Group
 * 2. Legers
 * 3. CostCenter
 * 4. CostCenter Category
 * 5. Vouchers 
 *    a. Receipt, Payment, Contra and Journal
 *    b. Purchase, Sales (We fetach those vouchers as Journal Entry without stock items
 *    c. If Cash/Bank affects in tally, treat as Journal Entry
 * 6. If Donor Mmodule enabled Tally (Specially for SDB Bangalore Province)
 *    Fetch Donor, Purpse, country 
 * 
 * Logic: Sending Data from Tally
 * 1. Tally must 
 * 
 * 
 * */

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
using Bosco.DAO.Schema;
using System.Text.RegularExpressions;
using System.Security;
using System.Diagnostics;


namespace Bosco.Model.TallyMigration
{
    public class TallyConnector : SystemBase
    {
        #region Decelaration
        public string MSG_TALLY_NOT_RUNNING = String.Format("Tally is not running.{0}Tally must be running with admin rights", Environment.NewLine);
        private CommonMember utilitymember = new CommonMember();
        private AppSchemaSet.ApplicationSchemaSet AppSchema = new AppSchemaSet.ApplicationSchemaSet();
        public enum TallyMasters
        {
            VoucherType,
            Groups,
            Ledgers,
            CostCategory,
            CostCenters,
            Donors,
            Sponsers,
            Purposes,
            States,
            Country,
            Vouchers
        }

        private string TBL_DonorType = "_UDF_788529213";
        private string TBL_DonorState = "_UDF_788567383";
        private string TBL_DonorCountry = "_UDF_788529212";

        private string TBL_DonorList = "_UDF_805306420";
        private string TBL_DonationAmount = "_UDF_687865911";
        private string TBL_DonorName = "_UDF_788529205";
        private string TBL_RecipientType = "_UDF_788567362";

        //In Tally Voucher Entry, Multiple CC category is not allowed, it accepts only for particular CC category, so we fix all CC are under "Acmeerp Cost Category"
        public string Def_CC_Category = "Primary Cost Category";

        public string GRP_CASH_IN_HAND = "Cash-in-hand";
        public string GRP_BANK_ACCOUNTS = "Bank Accounts";
        public string GRP_BANK_OD_AC = "Bank OD A/c";
        
        public string GRP_CAPTIAL_ACCOUNT = "Capital Account";
        public string LDR_PROFIT_LOSS = "Profit & Loss A/c";

        public DataTable dtTallyLedgers = null;
        public DataTable dtTallyVoucherTypes = null;
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
        /// Get Tally server from the config file
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
        /// Get Tally server port from the config file
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
        /// This property is used to return empty ledger entries table structure
        /// 
        /// this datable could be used to send data to tally
        /// </summary>
        public DataTable LedgerEntriesStructure
        {
            get
            {
                DataTable dtLedgerEntries = new DataTable();
                dtLedgerEntries.Columns.Add(AppSchema.Ledger.LEDGER_IDColumn.ColumnName, typeof(System.Int32));
                dtLedgerEntries.Columns.Add(AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName, typeof(System.String));
                dtLedgerEntries.Columns.Add(AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName, typeof(System.Int32));
                return dtLedgerEntries;
            }
        }

        /// <summary>
        /// This property is used to return empty ledger CC entries table structure
        ///  
        /// this datable could be used to send data to tally
        /// </summary>
        public DataTable LedgerCCDetailsStructure
        {
            get
            {
                DataTable dtLedgerCCDetails = new DataTable();
                dtLedgerCCDetails.Columns.Add(AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName, typeof(System.Int32));
                dtLedgerCCDetails.Columns.Add(AppSchema.Ledger.LEDGER_IDColumn.ColumnName, typeof(System.String));
                dtLedgerCCDetails.Columns.Add(AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, typeof(System.String));
                dtLedgerCCDetails.Columns.Add(AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName, typeof(System.String));
                dtLedgerCCDetails.Columns.Add(AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName, typeof(System.String));
                dtLedgerCCDetails.Columns.Add(AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn.ColumnName, typeof(System.Int32));
                return dtLedgerCCDetails;
            }
        }

        /// <summary>
        /// Return Tally server connected or not
        /// </summary>
        public ResultArgs IsMoreThanOneTallyRunningInstance
        {
            get
            {
                ResultArgs resultArg = new ResultArgs();
                try
                {
                    Process[] processByName = Process.GetProcessesByName("Tally");


                    resultArg.Success = processByName.Length > 1;
                }
                catch (Exception err)
                {
                    resultArg.Message = err.Message;
                }

                return resultArg;
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
                    resultArg = CurrentCompnayName;
                    if (resultArg.Success)
                    {
                        if (resultArg.ReturnValue == null)
                        {
                            resultArg.Message = "Tally company is not yet loaded";
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
                        //For Master
                        TBL_DonorType = "_UDF_788529213";
                        TBL_DonorState = "_UDF_788567383";
                        TBL_DonorCountry = "_UDF_788529212";
                        //For Voucher
                        TBL_DonorList = "_UDF_805306420";
                        TBL_DonationAmount = "_UDF_687865911";
                        TBL_DonorName = "_UDF_788529205";
                        TBL_RecipientType = "_UDF_788567362";
                        Rtn = true;
                    }
                    else if (dsCCMaster.Tables["DONORTYPE.LIST"] != null)
                    {
                        //For Master
                        TBL_DonorType = "DONORTYPE";
                        TBL_DonorState = "DONORSTATE";
                        TBL_DonorCountry = "DONORCOUNTRY";

                        //For Voucher
                        TBL_DonorList = "DONORALLOCATIONS";
                        TBL_DonationAmount = "DONORAMOUNT";
                        TBL_DonorName = "PAYMENTFAVOURINGDONOR";
                        TBL_RecipientType = "TYPEOFRECIPIENT";
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
        /// Get all defined masters from tally
        /// 1. Groups 2. Ledgers 3. CostCategory 4. CostCenters 5. VoucherType
        /// 6. Donors 7. Sponsers 8. Purposes 9. States 10 Country 
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        public ResultArgs FetchMasters(string balancedate)
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
                    resultarg = FetchTally(TallyEnum, balancedate);
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
                    resultarg = FetchCurrentCompanyDetails();
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
        /// Get all defined masters from tally
        /// 1. Groups 2. Ledgers 3. CostCategory 4. CostCenters 5. VoucherType
        /// 6. Donors 7. Sponsers 8. Purposes 9. States 10 Country 
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        public ResultArgs FetchMasters()
        {
            return FetchMasters(string.Empty);
        }

        /// <summary>
        /// Retriew Voucher Entries from Tally server
        /// (Mastet Voucher, Map Voucher base type Child Vouhcer, Cost Cener Allocaton for each child Voucher enteries
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        public ResultArgs FetchVouchers(string fromdate, string todate)
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            DataSet dsVouchers = new DataSet();

            string xml = "<ENVELOPE>"
                            + "<HEADER>"
                            + "<VERSION>1</VERSION>"
                            + "<TALLYREQUEST>Export</TALLYREQUEST>"
                            + "<TYPE>Data</TYPE>"
                            + "<ID>Voucher Register</ID>" //Voucher Register
                            + "</HEADER>"
                            + "<BODY>"
                            + "<DESC>"
                                + "<STATICVARIABLES>"
                                    + "<SVFROMDATE TYPE=\"Date\">" + fromdate + "</SVFROMDATE>"
                                    + "<SVTODATE TYPE=\"Date\">" + todate + "</SVTODATE>"
                                    + "<EXPLODEFLAG>Yes</EXPLODEFLAG>"
                                    + "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>"
                                + "</STATICVARIABLES>"
                            + "</DESC>"
                            + "</BODY>"
                            + "</ENVELOPE>";
            try
            {
                resultarg = ExecuteTallyXML(xml, TallyMasters.Vouchers);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        resultarg = GetAlreadyLoadedLedgers(fromdate);
                        if (resultarg.Success)
                        {
                            resultarg = GetAlreadyLoadedVoucherType();
                            resultarg.DataSource.Data = null;
                            if (resultarg.Success)
                            {
                                if (TallyResponseDataSet.Tables["VOUCHER"] != null && TallyResponseDataSet.Tables["ALLLEDGERENTRIES.LIST"] != null)
                                {
                                    DataTable dtMasterVoucher = new dsTallyConnector.MASTERVOUCHERDataTable();
                                    DataTable dtChildVoucher = new dsTallyConnector.VOUCHERDETAILDataTable();
                                    DataTable dtCCVoucher = new dsTallyConnector.CCVOUCHERDataTable();

                                    DataTable dtTallyMasterVouchers = TallyResponseDataSet.Tables["VOUCHER"]; //Master Vouchers
                                    DataTable dtTallyChildVochers = TallyResponseDataSet.Tables["ALLLEDGERENTRIES.LIST"]; //Child Vouchers
                                    DataTable dtTallyCCCategory = TallyResponseDataSet.Tables["CATEGORYALLOCATIONS.LIST"]; //Cost center category list
                                    DataTable dtTallyCC = TallyResponseDataSet.Tables["COSTCENTREALLOCATIONS.LIST"]; //Cost center Allocation List 
                                    DataTable dtTallyBankReference = TallyResponseDataSet.Tables["BANKALLOCATIONS.LIST"]; //get bank dd, cheque, interbank reference number

                                    //For Temp: We could not add columns in dsTallyConnector, so we new columns here
                                    dtMasterVoucher.Columns.Add("TALLY_VOUCHER_TYPE", typeof(string));

                                    //For Temp: We could not add columns in dsTallyConnector, so we new columns here
                                    dtChildVoucher.Columns.Add("BANKERSDATE", typeof(string));
                                    dtChildVoucher.Columns.Add("BANKNAME", typeof(string));
                                    dtChildVoucher.Columns.Add("BANKBRANCHNAME", typeof(string));

                                    if (dtTallyBankReference == null)
                                    {
                                        dtTallyBankReference = new DataTable();
                                        dtTallyBankReference.Columns.Add("ALLLEDGERENTRIES.LIST_Id", typeof(Int32));
                                        dtTallyBankReference.Columns.Add("TransactionType", typeof(string));
                                        dtTallyBankReference.Columns.Add("InstrumentDate", typeof(string));
                                        dtTallyBankReference.Columns.Add("InstrumentNumber", typeof(string));
                                        dtTallyBankReference.Columns.Add("BANKERSDATE", typeof(string));
                                        dtTallyBankReference.Columns.Add("BANKBRANCHNAME", typeof(string));
                                        dtTallyBankReference.Columns.Add("BANKNAME", typeof(string));
                                    }

                                    // Donor Voucher Infomration ***************************************************************
                                    DataTable dtDonorVocuerEntry = new DataTable();
                                    dtDonorVocuerEntry.Columns.Add("COSTCENTREALLOCATIONS.LIST_ID", typeof(Int32));
                                    dtDonorVocuerEntry.Columns.Add("UDF_805306420_LIST_Id", typeof(Int32));
                                    dtDonorVocuerEntry.Columns.Add("DONOR", typeof(string));
                                    dtDonorVocuerEntry.Columns.Add("DONATIONAMOUNT", typeof(string));
                                    dtDonorVocuerEntry.Columns.Add("RECEIPTTYPE", typeof(string));

                                    //Map Vouhcer enty with its base voucher type (RECEIPTS, PAYMENTS, CONTRA, JOURNAL) ----------------------
                                    //On 10/10/2017, To skip optional Vouchers (feature in Tally)
                                    dtTallyMasterVouchers.DefaultView.RowFilter = "ISOPTIONAL = 'No' AND ISCANCELLED = 'No'";
                                    dtTallyMasterVouchers = dtTallyMasterVouchers.DefaultView.ToTable();

                                    var resultMasterVoucher = from drVoucherType in dtTallyVoucherTypes.AsEnumerable()
                                                              join drMasterVoucher in dtTallyMasterVouchers.AsEnumerable() on drVoucherType.Field<string>("NAME")
                                                              equals drMasterVoucher.Field<string>("VCHTYPE")
                                                              join drLedger in dtTallyLedgers.AsEnumerable() on drMasterVoucher.Field<string>("PARTYLEDGERNAME")
                                                              equals drLedger.Field<string>("LEDGERNAME")
                                                              into ledgerggroupjoin
                                                              from drLedger in ledgerggroupjoin.DefaultIfEmpty()
                                                              select dtMasterVoucher.LoadDataRow(new object[]
                                 {
                                    drMasterVoucher.Field<Int32>("VOUCHER_ID"),
                                    ConvertTallyDate(drMasterVoucher.Field<string>("DATE")),
                                    //drMasterVoucher.Field<string>("VOUCHERTYPENAME"),
                                    ((drMasterVoucher.Field<string>("VOUCHERTYPENAME") == "Sales" && (drLedger.Field<string>("Parent") == GRP_CASH_IN_HAND || drLedger.Field<string>("Parent") == GRP_BANK_ACCOUNTS)) ? "Receipt" : 
                                    (drMasterVoucher.Field<string>("VOUCHERTYPENAME")=="Purchase" && (drLedger.Field<string>("Parent") == GRP_CASH_IN_HAND || drLedger.Field<string>("Parent") == GRP_BANK_ACCOUNTS)) ? "Payment" : 
                                    (drMasterVoucher.Field<string>("VOUCHERTYPENAME")=="Sales" || drMasterVoucher.Field<string>("VOUCHERTYPENAME")=="Purchase") ? "Journal" :drMasterVoucher.Field<string>("VOUCHERTYPENAME")),                                        
                                    drMasterVoucher.Field<string>("VOUCHERNUMBER"),
                                    drMasterVoucher.Field<string>("PARTYLEDGERNAME"),
                                    drMasterVoucher.Field<string>("NARRATION"),
                                    //drVoucherType.Field<string>("BASEVOUCHERTYPE"),
                                    ((drVoucherType.Field<string>("PARENT") == "Sales" && (drLedger.Field<string>("Parent") == GRP_CASH_IN_HAND || drLedger.Field<string>("Parent") == GRP_BANK_ACCOUNTS)) ? "Receipt" : 
                                    (drVoucherType.Field<string>("PARENT") =="Purchase" && (drLedger.Field<string>("Parent") == GRP_CASH_IN_HAND || drLedger.Field<string>("Parent") == GRP_BANK_ACCOUNTS)) ? "Payment" : 
                                    (drVoucherType.Field<string>("PARENT") =="Sales" || drMasterVoucher.Field<string>("VOUCHERTYPENAME")=="Purchase") ? "Journal" :drVoucherType.Field<string>("PARENT")),
                                    drVoucherType.Field<string>("PARENT"), //Tally Base Voucher Type
                                 }, true);
                                    dtMasterVoucher = resultMasterVoucher.CopyToDataTable();

                                    dtMasterVoucher.TableName = "MASTER VOUCHER";
                                    //--------------------------------------------------------------------------------------------------------

                                    //Map bank details (dd, cheque, interbank transfer reference number with child vouhcer entries

                                    //*** On 01/09/2017 In few Tally version, if CC is not enabled, field "ALLLEDGERENTRIES.LIST_Id" is not availble in Child Voucher details
                                    //so add "ALLLEDGERENTRIES.LIST_Id" field and by default value is 0
                                    if (!dtTallyChildVochers.Columns.Contains("ALLLEDGERENTRIES.LIST_Id"))
                                    {
                                        DataColumn dcAllLedgersEntries = new DataColumn("ALLLEDGERENTRIES.LIST_Id", typeof(Int32));
                                        dcAllLedgersEntries.DefaultValue = 0;
                                        dtTallyChildVochers.Columns.Add(dcAllLedgersEntries);
                                    }

                                    if (!dtTallyBankReference.Columns.Contains("ALLLEDGERENTRIES.LIST_Id"))
                                    {
                                        DataColumn dcAllLedgersEntries = new DataColumn("ALLLEDGERENTRIES.LIST_Id", typeof(Int32));
                                        //dcAllLedgersEntries.DefaultValue = 0;
                                        dtTallyBankReference.Columns.Add(dcAllLedgersEntries);
                                    }

                                    //On 05/04/2018, In few Tally (Egmore DB), bank reference detials are repeating for bank
                                    if (dtTallyBankReference != null)
                                    {
                                        var res = from element in dtTallyBankReference.AsEnumerable()
                                                  group element by element["ALLLEDGERENTRIES.LIST_Id"]
                                                      into groups
                                                      select groups.OrderBy(p => p["ALLLEDGERENTRIES.LIST_Id"]).First();
                                        if (res != null && res.Count() > 0)
                                        {
                                            dtTallyBankReference = res.CopyToDataTable();
                                        }
                                    }

                                    //As On 03/01/2023, In TALLY Prime, there is no "BANKBRANCHNAME" they have changed as "BRANCHNAME" ------------
                                    string bankbranchname = "BANKBRANCHNAME";
                                    if (dtTallyBankReference !=null && !dtTallyBankReference.Columns.Contains(bankbranchname))
                                    {
                                        bankbranchname = "BRANCHNAME";
                                    }
                                    //--------------------------------------------------------------------------------------------------------------

                                    var resultChildVouhcer = from drChildVochers in dtTallyChildVochers.AsEnumerable()
                                                             join drBankReference in dtTallyBankReference.AsEnumerable()
                                                             on drChildVochers.Field<Int32?>("ALLLEDGERENTRIES.LIST_Id") equals
                                                             (dtTallyBankReference == null ? null : drBankReference.Field<Int32?>("ALLLEDGERENTRIES.LIST_Id"))
                                                             into bankreferencejoin
                                                             from drBankReference in bankreferencejoin.DefaultIfEmpty()
                                                             select dtChildVoucher.LoadDataRow(new object[]
                                                    {
                                                        drChildVochers.Field<Int32?>("VOUCHER_ID"),
                                                        (dtTallyCC!=null?drChildVochers.Field<Int32?>("ALLLEDGERENTRIES.LIST_ID"):0),
                                                        drChildVochers.Field<string>("LEDGERNAME"),
                                                        drChildVochers.Field<string>("AMOUNT"),
                                                        drChildVochers.Field<string>("NARRATION"),
                                                        drChildVochers.Field<string>("ISDEEMEDPOSITIVE"),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>("TransactionType")),
                                                        (drBankReference==null?string.Empty:ConvertTallyDate(drBankReference.Field<string>("InstrumentDate"))),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>("InstrumentNumber")),
                                                        (drBankReference==null?string.Empty:ConvertTallyDate(drBankReference.Field<string>("BANKERSDATE"))),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>("BANKNAME")),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>(bankbranchname))
                                                    }, true);
                                    dtChildVoucher = resultChildVouhcer.CopyToDataTable();
                                    dtChildVoucher.TableName = "VOUCHER DETAILS";
                                    dtChildVoucher.DefaultView.RowFilter = "LEDGERNAME<>''";
                                    dtChildVoucher = dtChildVoucher.DefaultView.ToTable();

                                    //On 10/10/2017, Check Cashbank Ledgers in Journal Entry ------------------------------------------
                                    resultarg = CheckCashBankInJournalVoucher(dtMasterVoucher, dtChildVoucher, dtTallyLedgers);
                                    //------------------------------------------------------------------------------------------------
                                    if (resultarg.Success)
                                    {
                                        //On 09/12/2017, Check Bank OD Ledgers in Contra Entry ------------------------------------------
                                        resultarg = CheckBankODInVouchers(dtMasterVoucher, dtChildVoucher, dtTallyLedgers);
                                        //------------------------------------------------------------------------------------------------

                                        if (resultarg.Success)
                                        {
                                            //Map CC with CC category Allocation for each child voucher entries---------------------------------------------------------
                                            if (dtTallyCCCategory != null)
                                            {
                                                //*** On 01/09/2017, in few tally versions, If CC is not enabled but Inventory is available 
                                                //field "ALLLEDGERENTRIES.LIST_Id" is not avilable in CC category talbe
                                                if (!dtTallyCCCategory.Columns.Contains("ALLLEDGERENTRIES.LIST_Id"))
                                                {
                                                    DataColumn dcAllLedgersEntries = new DataColumn("ALLLEDGERENTRIES.LIST_Id", typeof(Int32));
                                                    //dcAllLedgersEntries.DefaultValue = 0;
                                                    dtTallyCCCategory.Columns.Add(dcAllLedgersEntries);
                                                }

                                                var result = from drCCCategory in dtTallyCCCategory.AsEnumerable()
                                                             join drCC in dtTallyCC.AsEnumerable() on drCCCategory.Field<Int32>("CATEGORYALLOCATIONS.LIST_ID")
                                                             equals drCC.Field<Int32?>("CATEGORYALLOCATIONS.LIST_ID")
                                                             into ccjoin
                                                             from drCC in ccjoin.DefaultIfEmpty()
                                                             select dtCCVoucher.LoadDataRow(new object[]
                                                        {
                                                        drCCCategory.Field<Int32?>("ALLLEDGERENTRIES.LIST_Id"),
                                                        drCCCategory.Field<Int32?>("CATEGORYALLOCATIONS.LIST_ID"),
                                                        (dtTallyCC.Columns.Contains("COSTCENTREALLOCATIONS.LIST_ID")?drCC.Field<Int32>("COSTCENTREALLOCATIONS.LIST_ID"):-1),
                                                        drCCCategory.Field<string>("CATEGORY"),
                                                        drCC.Field<string>("NAME"),
                                                        drCC.Field<string>("AMOUNT"),
                                                        }, true);
                                                dtCCVoucher = result.CopyToDataTable();
                                                dtCCVoucher.TableName = "CCVoucher";
                                            }


                                            // Donor Voucher Infomration ***************************************************************
                                            DataTable dtUDFDonorInfoList = TallyResponseDataSet.Tables[TBL_DonorList + ".LIST"];
                                            //dtUDFDonorInfoList.DefaultView.RowFilter = "COSTCENTREALLOCATIONS.LIST_ID=0";

                                            if (dtUDFDonorInfoList != null)
                                            {
                                                //Donor Amount
                                                DataTable dtUDFDonationAmtList = TallyResponseDataSet.Tables[TBL_DonationAmount + ".LIST"];
                                                DataTable dtUDFDonationAmt = TallyResponseDataSet.Tables[TBL_DonationAmount];
                                                var resultDonationAmt = from drUDFDonationAmtList in dtUDFDonationAmtList.AsEnumerable()
                                                                        join drUDFDonationAmt in dtUDFDonationAmt.AsEnumerable()
                                                                        on drUDFDonationAmtList.Field<Int32?>(TBL_DonationAmount + ".LIST_Id") equals drUDFDonationAmt.Field<Int32?>(TBL_DonationAmount + ".LIST_Id")
                                                                        select new
                                                                        {
                                                                            UDF_805306420_LIST_Id = drUDFDonationAmtList.Field<Int32?>(TBL_DonorList + ".LIST_Id"),
                                                                            UDF_687865911_LIST_Id = drUDFDonationAmtList.Field<Int32?>(TBL_DonationAmount + ".LIST_Id"),
                                                                            AMOUNT = drUDFDonationAmt.Field<string>(TBL_DonationAmount + "_TEXT"),
                                                                        };
                                                //Name of the Donor
                                                DataTable dtUDFDonorNameList = TallyResponseDataSet.Tables[TBL_DonorName + ".LIST"];
                                                DataTable dtUDFDonorName = TallyResponseDataSet.Tables[TBL_DonorName];
                                                var resultDonationName = from drUDFDonorNameList in dtUDFDonorNameList.AsEnumerable()
                                                                         join drUDFDonorName in dtUDFDonorName.AsEnumerable()
                                                                            on drUDFDonorNameList.Field<Int32?>(TBL_DonorName + ".LIST_Id") equals drUDFDonorName.Field<Int32?>(TBL_DonorName + ".LIST_Id")
                                                                            into udfdonornamejoin
                                                                         from drUDFDonorName in udfdonornamejoin.DefaultIfEmpty()
                                                                         select new
                                                                         {
                                                                             UDF_805306420_LIST_Id = drUDFDonorNameList.Field<Int32?>(TBL_DonorList + ".LIST_Id"),
                                                                             UDF_788529205_LIST_Id = drUDFDonorNameList.Field<Int32?>(TBL_DonorName + ".LIST_Id"),
                                                                             DONORNAME = drUDFDonorName.Field<string>(TBL_DonorName + "_TEXT"),
                                                                         };

                                                //Donor Receipt Type
                                                DataTable dtUDFReceiptTypeList = TallyResponseDataSet.Tables[TBL_RecipientType + ".LIST"];
                                                DataTable dtUDFReceiptType = TallyResponseDataSet.Tables[TBL_RecipientType];
                                                var resultReceiptTypeList = from drUDFReceiptTypeList in dtUDFReceiptTypeList.AsEnumerable()
                                                                            join drUDFReceiptType in dtUDFReceiptType.AsEnumerable()
                                                                                on drUDFReceiptTypeList.Field<Int32?>(TBL_RecipientType + ".LIST_Id") equals drUDFReceiptType.Field<Int32?>(TBL_RecipientType + ".LIST_Id")
                                                                                into udfreceipttypejoin
                                                                            from drUDFReceiptType in udfreceipttypejoin.DefaultIfEmpty()
                                                                            select new
                                                                            {
                                                                                UDF_805306420_LIST_Id = drUDFReceiptTypeList.Field<Int32?>(TBL_DonorList + ".LIST_Id"),
                                                                                UDF_788567362_LIST_Id = drUDFReceiptTypeList.Field<Int32?>(TBL_RecipientType + ".LIST_Id"),
                                                                                RECEIPTTYPE = drUDFReceiptType.Field<string>(TBL_RecipientType + "_TEXT"),
                                                                            };


                                                var resultDonorVocuerEntry = from drUDFDonorInfoList in dtUDFDonorInfoList.AsEnumerable()
                                                                             join drUDFDonationAmount in resultDonationAmt.AsEnumerable()
                                                                                 on drUDFDonorInfoList.Field<Int32?>(TBL_DonorList + ".LIST_Id") equals drUDFDonationAmount.UDF_805306420_LIST_Id
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
                                                                 drUDFDonorInfoList.Field<Int32?>("COSTCENTREALLOCATIONS.LIST_ID"),
                                                                 drUDFDonorInfoList.Field<Int32?>(TBL_DonorList+ ".LIST_Id"),
                                                                 (drUDFDonationName==null?string.Empty:drUDFDonationName.DONORNAME),
                                                                 (drUDFDonationAmount==null?string.Empty:drUDFDonationAmount.AMOUNT),
                                                                 (drUDFReceiptType==null?string.Empty:drUDFReceiptType.RECEIPTTYPE)
                                                             }, true);
                                                dtDonorVocuerEntry = resultDonorVocuerEntry.CopyToDataTable();
                                                dtDonorVocuerEntry.TableName = "DonorVoucher";
                                            }
                                            //**********************************************************************************

                                            //------------------------------------------------------------------------------------

                                            //Add Vouchers to dataset ----------------------------------------------------------------
                                            //DataTable dtchildvouchers = dtTallyChildVochers.DefaultView.ToTable("VOUCHER DETAILS", false, childVoucher);
                                            resultarg = FetchInventoryVouchers(TallyResponseDataSet, dtChildVoucher, fromdate, todate);
                                        }
                                    }

                                    dsVouchers.Tables.Add(dtMasterVoucher);
                                    dsVouchers.Tables.Add(dtChildVoucher);
                                    dsVouchers.Tables.Add(dtCCVoucher);
                                    dsVouchers.Tables.Add(dtTallyVoucherTypes);
                                    dsVouchers.Tables.Add(dtDonorVocuerEntry);

                                    dsVouchers.Relations.Add(dtChildVoucher.TableName, dsVouchers.Tables[0].Columns["VOUCHER_ID"], dsVouchers.Tables[1].Columns["VOUCHER_id"], false);
                                    //-----------------------------------------------------------------------------------------
                                    resultarg.Success = resultarg.Success;
                                    resultarg.DataSource.Data = dsVouchers;
                                }
                            }
                        }
                    }
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

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Vouchers.\n" + resultarg.Message;
            }
            return resultarg;
        }


        private ResultArgs GetAlreadyLoadedLedgers(string frmdate)
        {
            ResultArgs resultarg = new ResultArgs();
            if (dtTallyLedgers == null)
            {
                resultarg = FetchLedger(frmdate);
                if (resultarg.Success)
                {
                    dtTallyLedgers = resultarg.DataSource.Table;
                }
            }
            else
            {
                resultarg.Success = true;
            }
            return resultarg;
        }

        private ResultArgs GetAlreadyLoadedVoucherType()
        {
            ResultArgs resultarg = new ResultArgs();
            if (dtTallyVoucherTypes == null)
            {
                resultarg = FetchVoucherTypes();
                if (resultarg.Success)
                {
                    dtTallyVoucherTypes = resultarg.DataSource.Table;
                }
            }
            else
            {
                resultarg.Success = true;
            }
            return resultarg;
        }

        /// <summary>
        /// Get LEdger list with opening balance from tally 
        /// </summary>
        /// <returns>ResultArgs with ledger and its result sucess or failure</returns>
        private ResultArgs FetchLedger(string balancedate)
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            DataTable dtLedger = new dsTallyConnector.LEDGERDataTable();

            //For temp, could not change column in dsTallyConnector.LEDGERDataTable
            //Change  CLOSINGBALANCE to DATEOPENINGBALANCE
            if (!dtLedger.Columns.Contains("DATEOPENINGBALANCE") &&
                dtLedger.Columns.Contains("CLOSINGBALANCE"))
            {
                dtLedger.Columns["CLOSINGBALANCE"].ColumnName = "DATEOPENINGBALANCE";
            }

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
                resultarg = ExecuteTallyXML(xml, TallyMasters.Ledgers);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        if (TallyResponseDataSet.Tables["LEDGER"] != null)
                        {
                            DataTable dtTallyXMLLedger = TallyResponseDataSet.Tables["LEDGER"];
                            DataTable dtLedgerOPBalance = new DataTable();
                            
                            //On 16/02/2018, "Profit & Loss A/c" is predefined ledger in Tally under primary group --------------------------------
                            //We make it "Profit & Loss A/c" under Captial Account Group
                            dtTallyXMLLedger.DefaultView.RowFilter = "NAME = '" + LDR_PROFIT_LOSS + "'";
                            if (dtTallyXMLLedger.DefaultView.Count > 0)
                            {
                                dtTallyXMLLedger.DefaultView[0]["PARENT"] = GRP_CAPTIAL_ACCOUNT;
                                dtTallyXMLLedger.DefaultView.Table.AcceptChanges();
                            }
                            dtTallyXMLLedger.DefaultView.RowFilter = string.Empty;
                            //----------------------------------------------------------------------------------------------------------------------


                            //1. Get ledger opening balance with Primary group
                            resultarg = FetchLedgerOpeningBalance(balancedate);
                            if (resultarg.Success)
                            {
                                dtLedgerOPBalance = resultarg.DataSource.Table;

                                //2. Address ------------------------------------------------------------------------------------
                                // suntry creditor/debtpr have multi address line (address1, address2), make them into single line with (,)
                                DataTable dtTallyAddressList = TallyResponseDataSet.Tables["ADDRESS.LIST"];
                                DataTable dtTallyAddress = TallyResponseDataSet.Tables["ADDRESS"];
                                DataTable resultdtAddress = new DataTable();
                                resultdtAddress.Columns.Add("LEDGER_ID", typeof(Int32));
                                resultdtAddress.Columns.Add("ADDRESS", typeof(string));
                                string ledgeridcolum = "LEDGER_ID";
                                
                                //Make multi addess into single Address field          
                                if (dtTallyAddressList != null)
                                {
                                    //On 20/09/2023, for TALLY PRIME
                                    if (!dtTallyAddressList.Columns.Contains("LEDGER_ID"))
                                    {
                                        ledgeridcolum = "LEDMAILINGDETAILS.LIST_Id";
                                    }
                                    
                                    if (dtTallyAddressList.Columns.Contains("ADDRESS.LIST_ID"))
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
                                                            select resultdtAddress.LoadDataRow(new object[] 
                                                        {
                                                            (drTallyAddressList.Field<Nullable<Int32>>(ledgeridcolum)==null? 0 : drTallyAddressList.Field<Nullable<Int32>>(ledgeridcolum)), 
                                                            //drTallyAddressList.Field<Int32>("LEDGER_ID"),
                                                            drTallySingleAddress.Field<string>("ADDRESS_TEXT"),
                                                        }, false);
                                        resultdtAddress = resultAddress.CopyToDataTable();
                                    }
                                    else
                                    {

                                        resultdtAddress = dtTallyAddressList;
                                    }
                                }
                                //Address ------------------------------------------------------------------------------------

                                //3. Bank Account Ledger Type (SB, FD)
                                DataTable dtTallyAccountTypeList = TallyResponseDataSet.Tables["_UDF_788567360.LIST"];
                                DataTable dtTallyAccountType = TallyResponseDataSet.Tables["_UDF_788567360"];
                                DataTable resultdtBankAccountType = new DataTable();
                                resultdtBankAccountType.Columns.Add("LEDGER_ID", typeof(Int32));
                                resultdtBankAccountType.Columns.Add("UDF_788567360_LIST_ID", typeof(Int32));
                                resultdtBankAccountType.Columns.Add("BANK_AC_TYPE", typeof(string));
                                if (dtTallyAccountTypeList != null)
                                {
                                    var resultBankAccountType = from drTallyAccountTypeList in dtTallyAccountTypeList.AsEnumerable()
                                                                join drTallyAccountType in dtTallyAccountType.AsEnumerable()
                                                                on drTallyAccountTypeList.Field<Int32>("_UDF_788567360.LIST_ID") equals drTallyAccountType.Field<Int32>("_UDF_788567360.LIST_ID")
                                                                into bankaccounttypejoin
                                                                from drTallyAccountType in bankaccounttypejoin.DefaultIfEmpty()
                                                                select resultdtBankAccountType.LoadDataRow(new object[] 
                                                                {
                                                                    drTallyAccountTypeList.Field<Int32>("LEDGER_ID"),
                                                                    drTallyAccountTypeList.Field<Int32>("_UDF_788567360.LIST_ID"),
                                                                    (drTallyAccountType == null ? string.Empty : drTallyAccountType.Field<String>("_UDF_788567360_TEXT"))
                                                                }, false);
                                    resultdtBankAccountType = resultBankAccountType.CopyToDataTable();
                                }
                                //------------------------------------------------------------------------------
                                bool isdonorenabled = IsDonorModuleEnabled;
                                //As On 03/01/2023, In TALLY Prime, there is no "BANKBRANCHNAME" they have changed as "BRANCHNAME" ------------
                                string bankbranchname = "BANKBRANCHNAME";
                                if (!dtTallyXMLLedger.Columns.Contains(bankbranchname))
                                {
                                    bankbranchname = "BRANCHNAME";
                                }
                                //--------------------------------------------------------------------------------------------------------------

                                var resultledger = from drTallyXMLLedger in dtTallyXMLLedger.AsEnumerable()
                                                   join drAddress in resultdtAddress.AsEnumerable()
                                                   on drTallyXMLLedger.Field<Int32>("LEDGER_ID") equals drAddress.Field<Int32>("LEDGER_ID") //"LEDGER_ID"//ledgeridcolum
                                                   into addressjoin
                                                   from drAddress in addressjoin.DefaultIfEmpty()
                                                   join drBankAccountType in resultdtBankAccountType.AsEnumerable()
                                                   on drTallyXMLLedger.Field<Int32>("LEDGER_ID") equals drBankAccountType.Field<Int32>("LEDGER_ID")
                                                   into bankaccounttypejoin
                                                   from drBankAccountType in bankaccounttypejoin.DefaultIfEmpty()
                                                   join drLedgerOpeningBalance in dtLedgerOPBalance.AsEnumerable()
                                                   on drTallyXMLLedger.Field<string>("NAME") equals drLedgerOpeningBalance.Field<string>("LEDGER_NAME")
                                                   into ledgeropeningjoin
                                                   from drLedgerOpeningBalance in ledgeropeningjoin.AsEnumerable()
                                                   select dtLedger.LoadDataRow(new object[] 
                                                   {
                                                       drTallyXMLLedger.Field<Int32>("LEDGER_ID"),
                                                       drTallyXMLLedger.Field<string>("NAME"),
                                                       drTallyXMLLedger.Field<string>("PARENT"),
                                                       (drLedgerOpeningBalance==null?string.Empty:drLedgerOpeningBalance.Field<string>("PRIMARY_GROUP")),
                                                       drTallyXMLLedger.Field<string>("OPENINGBALANCE"),
                                                       (drLedgerOpeningBalance==null?string.Empty:drLedgerOpeningBalance.Field<string>("OPENING_BALANCE")),
                                                       drTallyXMLLedger.Field<string>("ISCOSTCENTRESON"),
                                                       (isdonorenabled?drTallyXMLLedger.Field<string>("BANKACCHOLDERNAME"):string.Empty),
                                                       drTallyXMLLedger.Field<string>("BANKDETAILS"),
                                                       drTallyXMLLedger.Field<string>(bankbranchname),
                                                       (drBankAccountType==null?string.Empty:drBankAccountType.Field<string>("BANK_AC_TYPE")),
                                                       //drTallyXMLLedger.Field<string>("IFSCODE"), (On 22/12/2020 Lower version does not have ifscode)
                                                       (dtTallyXMLLedger.Columns.Contains("IFSCODE")? drTallyXMLLedger.Field<string>("IFSCODE") : ""),
                                                       (drAddress==null?string.Empty:drAddress.Field<string>("ADDRESS")),
                                                       drTallyXMLLedger.Field<string>("INCOMETAXNUMBER"),
                                                       (isdonorenabled?drTallyXMLLedger.Field<string>("NAMEONPAN"):string.Empty),
                                                       drTallyXMLLedger.Field<string>("TDSDEDUCTEETYPE")
                                                   }, false);
                                dtLedger = resultledger.CopyToDataTable();
                                dtTallyLedgers = dtLedger;
                                dtTallyLedgers.TableName = "TallyLedgers";
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
                resultarg.DataSource.Data = dtLedger;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Ledgers.\n" + resultarg.Message;
            }
            return resultarg;
        }



        public ResultArgs FetchGroups()
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            DataTable dtTallyGroups = new dsTallyConnector.GROUPDataTable();

            try
            {
                //1. Get Base Ledger details (Ledger Name, Parent, PrimaryGroup, OpeningBalance, IsCCOn, Revenew and Deemped)
                string LedgerXML = "<ENVELOPE>" +
                                "<HEADER>" +
                                        "<VERSION>1</VERSION>" +
                                        "<TALLYREQUEST>Export</TALLYREQUEST>" +
                                        "<TYPE>Collection</TYPE>" +
                                        "<ID>Collection of Ledger Groups</ID>" +
                                "</HEADER>" +
                            "<BODY>" +
                            "<DESC>" +
                            "<TDL>" +
                            "<TDLMESSAGE>" +
                             "<COLLECTION NAME='Collection of Ledger Groups' ISMODIFY='No' ISFIXED='No' ISINITIALIZE='No' ISOPTION='No' ISINTERNAL='No'>" +
                                "<TYPE>Groups</TYPE>" +  //$_PrimaryGroup
                                "<NATIVEMETHOD>Name</NATIVEMETHOD>" +
                                "<NATIVEMETHOD>Parent</NATIVEMETHOD>" +
                                "<NATIVEMETHOD>_PrimaryGroup</NATIVEMETHOD>" +
                                "<NATIVEMETHOD>ISREVENUE</NATIVEMETHOD>" +
                                "<NATIVEMETHOD>ISDEEMEDPOSITIVE</NATIVEMETHOD>" +
                              "</COLLECTION>" +
                              "</TDLMESSAGE>" +
                              "</TDL>" +
                               "<STATICVARIABLES>" +
                                "</STATICVARIABLES>" +
                            "</DESC>" +
                            "</BODY>" +
                            "</ENVELOPE>";

                resultarg = ExecuteTallyXML(LedgerXML, TallyMasters.Vouchers);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        if (TallyResponseDataSet.Tables["GROUP"] != null && TallyResponseDataSet.Tables["PARENT"] != null && TallyResponseDataSet.Tables["_PRIMARYGROUP"] != null
                            && TallyResponseDataSet.Tables["ISREVENUE"] != null && TallyResponseDataSet.Tables["ISDEEMEDPOSITIVE"] != null)
                        {
                            DataTable dtTallyLedgerGroup = TallyResponseDataSet.Tables["GROUP"];
                            DataTable dtParent = TallyResponseDataSet.Tables["PARENT"];
                            DataTable dtPRIMARYGROUP = TallyResponseDataSet.Tables["_PRIMARYGROUP"];
                            DataTable dtISREVENUE = TallyResponseDataSet.Tables["ISREVENUE"];
                            DataTable dtISDEEMEDPOSITIVE = TallyResponseDataSet.Tables["ISDEEMEDPOSITIVE"];

                            //1. Get Base Ledger group details (Group, Parent, Nature, PrimaryGroup and Grand Parent)
                            var resultLedger = from drTallyLedger in dtTallyLedgerGroup.AsEnumerable()
                                               join drParent in dtParent.AsEnumerable() on drTallyLedger.Field<Int32>("GROUP_ID") equals drParent.Field<Int32>("GROUP_ID")
                                               join drPrimary in dtPRIMARYGROUP.AsEnumerable() on drTallyLedger.Field<Int32>("GROUP_ID") equals drPrimary.Field<Int32>("GROUP_ID")
                                               join drISREVENUE in dtISREVENUE.AsEnumerable() on drTallyLedger.Field<Int32>("GROUP_ID") equals drISREVENUE.Field<Int32>("GROUP_ID")
                                               join drISDEEMEDPOSITIVE in dtISDEEMEDPOSITIVE.AsEnumerable() on drTallyLedger.Field<Int32>("GROUP_ID") equals drISDEEMEDPOSITIVE.Field<Int32>("GROUP_ID")
                                               select dtTallyGroups.LoadDataRow(new object[]
                                                   {
                                                       drTallyLedger.Field<string>("NAME"),
                                                       drParent.Field<string>("PARENT_text"),
                                                       (drISREVENUE.Field<string>("ISREVENUE_text").ToUpper()=="YES"?(drISDEEMEDPOSITIVE.Field<string>("ISDEEMEDPOSITIVE_text").ToUpper() == "YES" ? "Expenses" : "Incomes"):(drISDEEMEDPOSITIVE.Field<string>("ISDEEMEDPOSITIVE_text").ToUpper() == "YES" ? "Assets" : "Liabilities")),
                                                       (drPrimary==null?string.Empty:drPrimary.Field<string>("_PRIMARYGROUP_text")),
                                                       drParent.Field<string>("PARENT_text")
                                                   }, false);
                            dtTallyGroups = resultLedger.CopyToDataTable();
                            dtTallyGroups.DefaultView.Sort = "Parent";
                            dtTallyGroups.TableName = "Groups";
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
                resultarg.DataSource.Data = dtTallyGroups.DefaultView.Table;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Groups.\n" + resultarg.Message;
            }

            return resultarg;
        }

        /// <summary>
        /// Get Parent for TEMP purpose, finding if ledger group under primary
        /// /// </summary>
        /// <returns></returns>
        public string GetLedgerGroupParent(string LedgerGroup)
        {
            string Rtn = string.Empty;
            ResultArgs resultarg = new ResultArgs();

            try
            {
                string IsMasterExists = "<ENVELOPE>" +
                                            "<HEADER>" +
                                              "<VERSION>1</VERSION>" +
                                              "<TALLYREQUEST>EXPORT</TALLYREQUEST>" +
                                               "<TYPE>OBJECT</TYPE>" +
                                               "<SUBTYPE>Groups</SUBTYPE>" +
                                               "<ID TYPE='Name'>" + ReplaceXML(LedgerGroup) + "</ID>" +
                                            "</HEADER>" +
                                            "<BODY>" +
                                            "<DESC>" +
                                            "<STATICVARIABLES>" +
                                                "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                                            "</STATICVARIABLES>" +
                                                   "<FETCHLIST>" +
                                                       "<FETCH>Name</FETCH>" +
                                                       "<FETCH>Parent</FETCH>" +
                                                       "<FETCH>ISREVENUE</FETCH>" +
                                                       "<FETCH>ISDEEMEDPOSITIVE</FETCH>" +
                                                   "</FETCHLIST>" +
                                                "</DESC>" +
                                            "</BODY>" +
                                        "</ENVELOPE>";
                resultarg = FetchTallyXML(IsMasterExists);
                if (resultarg.Success)
                {
                    //Check Masters's parent in tally, if parent is differed in tally, update in tally
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        //Parent of the master
                        string parent = string.Empty;
                        if (TallyResponseDataSet.Tables["Parent"] != null)
                        {
                            if (TallyResponseDataSet.Tables["Parent"].Rows.Count > 0)
                            {
                                Rtn = TallyResponseDataSet.Tables["Parent"].Rows[0]["Parent_text"].ToString();

                                //If primary ledger group, Return its primary nature based on its renenew and deeped tag
                                if (Rtn == " Primary")
                                {
                                    string ISREVENUE = "NO";
                                    if (TallyResponseDataSet.Tables.Contains("ISREVENUE") && TallyResponseDataSet.Tables["ISREVENUE"].Rows.Count > 0)
                                    {
                                        ISREVENUE = TallyResponseDataSet.Tables["ISREVENUE"].Rows[0]["ISREVENUE_text"].ToString();
                                    }

                                    string ISDEEMEDPOSITIVE = "NO";
                                    if (TallyResponseDataSet.Tables.Contains("ISDEEMEDPOSITIVE") && TallyResponseDataSet.Tables["ISDEEMEDPOSITIVE"].Rows.Count > 0)
                                    {
                                        ISDEEMEDPOSITIVE = TallyResponseDataSet.Tables["ISDEEMEDPOSITIVE"].Rows[0]["ISDEEMEDPOSITIVE_text"].ToString();
                                    }

                                    //For Primary Groups
                                    if (ISREVENUE.ToUpper() == "YES") //For I&E groups
                                    {
                                        Rtn = (ISDEEMEDPOSITIVE.ToUpper() == "YES" ? "Expenses" : "Incomes");
                                    }
                                    else if (ISREVENUE.ToUpper() == "NO") // ASSET & LIABILITY
                                    {
                                        Rtn = (ISDEEMEDPOSITIVE.ToUpper() == "YES" ? "Assets" : "Liabilities");
                                    }

                                    //string ISREVENUE = (nature == Natures.Income || nature == Natures.Expenses ? "YES" : "NO"); //For All Income and Expenses groups
                                    //string ISDEEMEDPOSITIVE = (nature == Natures.Assert || nature == Natures.Expenses ? "YES" : "NO"); //For All Asset and Expenses groups
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Rtn = string.Empty; // if any unexpected error occurs, Add those ledger groups our of (I,E,A,L)
            }
            return Rtn;
        }

        /// <summary>
        /// To Retriew Voucer Types from Tally
        /// 1. Select voucher type with its setting by using tally xml (for map base voucher type)
        /// 2. Fitler only 'RECEIPT','PAYMENT','CONTRA','JOURNAL','Cash Receipt','Cash Payment','Sales','Purchase'
        /// 'Cash Receipt', 'Cash Payment' (For Tally 8.0 converted database)
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchVoucherTypes()
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            DataTable dtSQLTallyVoucherType = new DataTable();
            DataTable dtVoucherType = new dsTallyConnector.VOUCHERTYPEDataTable();

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
                                "<ACCOUNTTYPE>Voucher Types</ACCOUNTTYPE>" +
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

                            // For map Voucher base voucher type (RECEIPTS, PAYMENTS, CONTRA, JOURNAL, Cash Receipt, Cash Payment, Purchase and SALES----------
                            //'Cash Receipt', 'Cash Payment' (For Tally 8.0 converted database)
                            dtXMLTallyVoucherType.DefaultView.RowFilter = "[PARENT] IN ('RECEIPT','PAYMENT','CONTRA','JOURNAL','Cash Receipt','Cash Payment','Sales','Purchase')";
                            dtVoucherType = dtXMLTallyVoucherType.DefaultView.ToTable(false, new string[] { "NAME", "PARENT", "NUMBERINGMETHOD", "BEGINNINGNUMBER", "WIDTHOFNUMBER" });
                            //-----------------------------------------------------------------------
                            resultarg.Success = true;
                            dtTallyVoucherTypes = dtVoucherType;
                            dtTallyVoucherTypes.TableName = "TallyVoucherTypes";
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
                resultarg.DataSource.Data = dtVoucherType;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Voucher Types.\n" + resultarg.Message;
            }
            return resultarg;
        }

        /// <summary>
        ///Get Donor Master list if donor module is enabled in tally (CC CATEGORY='DONORS')
        ///Donor extra information like (Donor Type, State, Country, Address)
        ///Those information are available in extra tables in tally
        ///1. _UDF_788529213.LIST, _UDF_788529213: DONOR TYPE
        ///2. _UDF_788567383.LIST, _UDF_788567383: STATES
        ///3. _UDF_788529212.LIST, _UDF_788529212: COUNTRIES
        ///4. ADDRESS.LIST, ADDRESS : ADDRESS
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchDonorsMaster()
        {
            ResultArgs resultarg = new ResultArgs();
            DataTable dtDonorMaster = new dsTallyConnector.DONORSDataTable();
            try
            {
                //Get General CC master list from tally
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    //Filter cc only category is donors
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet.Tables.Contains("COSTCENTRE"))
                    {
                        DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                        dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='DONORS'";
                        dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                        if (dtCCTallyMaster != null)
                        {
                            //Get donor extra information like (Donor Type, State, Country, Address)
                            //Those information are available in extra tables in tally
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
                            if (TallyResponseDataSet.Tables[TBL_DonorType] != null)
                            {
                                //1. Donor Type -------------------------------------------------------------------------------
                                DataTable dtTallyDonorTypeList = TallyResponseDataSet.Tables[TBL_DonorType + ".LIST"];
                                DataTable dtTallyDonorType = TallyResponseDataSet.Tables[TBL_DonorType];
                                var resultDonorType = from drDonorTypeList in dtTallyDonorTypeList.AsEnumerable()
                                                      join drDonorType in dtTallyDonorType.AsEnumerable()
                                                      on drDonorTypeList.Field<Int32>(TBL_DonorType + ".LIST_Id") equals drDonorType.Field<Int32>(TBL_DonorType + ".LIST_Id")
                                                      select new
                                                      {
                                                          COSTCENTRE_ID = drDonorTypeList.Field<Int32>("COSTCENTRE_ID"),
                                                          DONORTYPE = drDonorType.Field<string>(TBL_DonorType + "_TEXT"),
                                                      };
                                //Donor Type -------------------------------------------------------------------------------

                                //2. State ----------------------------------------------------------------------------------
                                DataTable dtTallyStateList = TallyResponseDataSet.Tables[TBL_DonorState + ".LIST"];
                                DataTable dtTallyState = TallyResponseDataSet.Tables[TBL_DonorState];
                                var resultState = from drTallyStateList in dtTallyStateList.AsEnumerable()
                                                  join drTallyState in dtTallyState.AsEnumerable()
                                                       on drTallyStateList.Field<Int32>(TBL_DonorState + ".LIST_Id") equals drTallyState.Field<Int32>(TBL_DonorState + ".LIST_Id")
                                                  select new
                                                  {
                                                      COSTCENTRE_ID = drTallyStateList.Field<Int32>("COSTCENTRE_ID"),
                                                      STATE = drTallyState.Field<string>(TBL_DonorState + "_TEXT"),
                                                  };
                                //State ----------------------------------------------------------------------------------

                                //3. Country -----------------------------------------------------------------------------------
                                DataTable dtTallyCountryList = TallyResponseDataSet.Tables[TBL_DonorCountry + ".LIST"];
                                DataTable dtTallyCountry = TallyResponseDataSet.Tables[TBL_DonorCountry];
                                var resultCountry = from drTallyCountryList in dtTallyCountryList.AsEnumerable()
                                                    join drTallyCountry in dtTallyCountry.AsEnumerable()
                                                       on drTallyCountryList.Field<Int32>(TBL_DonorCountry + ".LIST_Id") equals drTallyCountry.Field<Int32>(TBL_DonorCountry + ".LIST_Id")
                                                    select new
                                                    {
                                                        COSTCENTRE_ID = drTallyCountryList.Field<Int32>("COSTCENTRE_ID"),
                                                        COUNTRY = drTallyCountry.Field<string>(TBL_DonorCountry + "_TEXT"),
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
                                dtDonorMaster.TableName = "DonorMaster";
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
                resultarg.DataSource.Data = dtDonorMaster;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Donor Master.\n" + resultarg.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get Sponsers List if donor module is enabled in tally  (CC CATEGORY='SPONSERS')
        /// Yet to be finalized sponors extra infomration
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchSponsors()
        {
            ResultArgs resultarg = new ResultArgs();
            DataTable dtSponsers = new dsTallyConnector.SPONSORSDataTable();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet.Tables.Contains("COSTCENTRE"))
                    {
                        DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                        dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='SPONSERS'";
                        dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                        if (dtCCTallyMaster != null)
                        {
                            //Check whether donor module enabled in tally
                            if (TallyResponseDataSet.Tables[TBL_DonorType] != null)
                            {
                                string[] sponserscols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                                dtSponsers = dtCCTallyMaster.DefaultView.ToTable("SPONSERS", false, sponserscols);
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
                resultarg.DataSource.Data = dtSponsers;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Sponsors.\n" + resultarg.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get Purpose list if donor module enabled in tally (CC CATEGORY='PURPOSES')
        /// Purpose information are available in _UDF_687904103
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchPurposes()
        {
            ResultArgs resultarg = new ResultArgs();
            DataTable dtPurposes = new dsTallyConnector.PURPOSESDataTable();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet.Tables.Contains("COSTCENTRE"))
                    {
                        DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                        dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='PURPOSES'";
                        dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                        if (dtCCTallyMaster != null)
                        {
                            //Check whether donor/purpose module enabled in tally
                            if (TallyResponseDataSet.Tables["_UDF_687904103"] != null ||
                                TallyResponseDataSet.Tables["FORPURPOSE"] != null)
                            {
                                string[] purposecols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                                dtPurposes = dtCCTallyMaster.DefaultView.ToTable("PURPOSES", false, purposecols);
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
                resultarg.DataSource.Data = dtPurposes;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Purposes.\n" + resultarg.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Get States List if donor module enabled in tally (CC CATEGORY='STATES')
        /// State and its country details are available in tally UDF_788567382
        /// </summary>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchStates()
        {
            ResultArgs resultarg = new ResultArgs();
            DataTable dtStates = new dsTallyConnector.STATEDataTable();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet.Tables.Contains("COSTCENTRE"))
                    {
                        DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                        dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='STATES'";
                        dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                        if (dtCCTallyMaster != null)
                        {
                            //Check whether donor/state information module enabled in tally
                            string statecountry = "_UDF_788567382";
                            if (TallyResponseDataSet.Tables["_UDF_788567382.LIST"] != null)
                            {
                                statecountry = "_UDF_788567382";
                            }
                            else
                            {
                                statecountry = "STATECOUNTRY";
                            }

                            if (TallyResponseDataSet.Tables[statecountry] != null)
                            {
                                string[] statecols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                                DataTable dtTallyStates = dtCCTallyMaster.DefaultView.ToTable("STATES", false, statecols);

                                //1. Country ----------------------------------------------------------------------------
                                DataTable dtTallyCountryList = TallyResponseDataSet.Tables[statecountry + ".LIST"];
                                DataTable dtTallyCountry = TallyResponseDataSet.Tables[statecountry];
                                var resultCountry = from drTallyCountryList in dtTallyCountryList.AsEnumerable()
                                                    join drTallyCountry in dtTallyCountry.AsEnumerable()
                                                       on drTallyCountryList.Field<Int32>(statecountry + ".LIST_Id") equals drTallyCountry.Field<Int32>(statecountry + ".LIST_Id")
                                                    select new
                                                    {
                                                        COSTCENTRE_ID = drTallyCountryList.Field<Int32>("COSTCENTRE_ID"),
                                                        COUNTRY = drTallyCountry.Field<string>(statecountry + "_TEXT"),
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
                resultarg.DataSource.Data = dtStates;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching States.\n" + resultarg.Message;
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
            DataTable dtCountries = new dsTallyConnector.COUNTRYDataTable();
            try
            {
                resultarg = FetchTallyGeneralCCMaster();
                if (resultarg.Success)
                {
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet.Tables.Contains("COSTCENTRE"))
                    {
                        DataTable dtCCTallyMaster = TallyResponseDataSet.Tables["COSTCENTRE"];
                        dtCCTallyMaster.DefaultView.RowFilter = "CATEGORY='COUNTRIES'";
                        dtCCTallyMaster = dtCCTallyMaster.DefaultView.ToTable();

                        if (dtCCTallyMaster != null)
                        {
                            //Check whether donor/country information module enabled in tally
                            if (TallyResponseDataSet.Tables["_UDF_553648184"] != null ||
                                TallyResponseDataSet.Tables["FORCOUNTRY"] != null)
                            {
                                string[] countrycols = { "COSTCENTRE_ID", "NAME", "CATEGORY" };
                                dtCountries = dtCCTallyMaster.DefaultView.ToTable("COUNTRIES", false, countrycols);
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
                resultarg.DataSource.Data = dtCountries;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching Countries.\n" + resultarg.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// Retrive records from tally for given master tables
        /// </summary>
        /// <param name="AcMEERPobject">enum master tables</param>
        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
        public ResultArgs FetchTally(TallyMasters AcMEERPobject, string balancedate)
        {
            string query = string.Empty;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = IsTallyConnected;
                if (resultarg.Success)
                {
                    switch (AcMEERPobject)
                    {
                        case TallyMasters.CostCategory:
                            resultarg = FetchCCCategory();
                            break;
                        case TallyMasters.CostCenters:
                            resultarg = FetchTallyGeneralCCMaster();
                            DataTable dtCC = new dsTallyConnector.COSTCENTERDataTable();
                            if (resultarg.Success && resultarg.DataSource.Data != null)
                            {
                                DataSet dsCC = (DataSet)resultarg.DataSource.Data;
                                if (dsCC.Tables["COSTCENTRE"] != null)
                                {
                                    dtCC = dsCC.Tables["COSTCENTRE"].DefaultView.ToTable(AcMEERPobject.ToString(), false, new string[] { "NAME", "CATEGORY" });
                                    dtCC.DefaultView.RowFilter = "CATEGORY NOT IN ('DONORS','SPONSERS','PURPOSES','STATES','COUNTRIES')";
                                    dtCC = dtCC.DefaultView.ToTable();
                                }
                                resultarg.DataSource.Data = dtCC;
                            }
                            break;
                        case TallyMasters.Groups:
                            resultarg = FetchGroups();
                            break;
                        case TallyMasters.Ledgers:
                            resultarg = FetchLedger(balancedate);
                            break;
                        case TallyMasters.VoucherType:
                            resultarg = FetchVoucherTypes();
                            break;
                        case TallyMasters.Donors:
                            resultarg = FetchDonorsMaster();
                            break;
                        case TallyMasters.Sponsers:
                            resultarg = FetchSponsors();
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
            }
            catch (Exception ex)
            {
                resultarg.Message = ex.Message;
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
            return FetchTally(AcMEERPobject, string.Empty);
        }
        #endregion

        #region private methods

        /// <summary>
        /// Execute Tally xml in tally server
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

                    //For timeout excetpion test by Carmel Raj
                    TallyRequest.Timeout = System.Threading.Timeout.Infinite;

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
                        //string responseFromTallyServer = reader.ReadToEnd().ToString();
                        string responseFromTallyServer = reader.ReadToEnd();

                        //string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
                        string r = "[\x00-\x08\x0B\x0C\x0E-\x1F]"; //(x00-Null Byte, x08-Backspace, x0B-Vertical tab, x0C-Form feed, x0E-Shift out, x1F-Unit separator)
                        responseFromTallyServer = Regex.Replace(responseFromTallyServer, r, "", RegexOptions.Compiled);

                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(responseFromTallyServer);

                        //Remove CostCategory/GROUP for the error (Cannot add a column named 'NAME': 
                        //a nested table with the same name already belongs to this DataTable)
                        //for when we take CC and Ledger
                        if (enumTallyMaster == TallyMasters.CostCenters || enumTallyMaster == TallyMasters.CostCategory ||
                            enumTallyMaster == TallyMasters.Ledgers)
                        {
                            string removeTag = "//TALLYMESSAGE//GROUP";
                            removeTag = (enumTallyMaster == TallyMasters.CostCenters ?
                                                "//TALLYMESSAGE//COSTCATEGORY" : enumTallyMaster == TallyMasters.CostCategory ?
                                                "//TALLYMESSAGE//NAME" : "//TALLYMESSAGE//GROUP");

                            XmlNodeList xmlchild = xmldoc.SelectNodes(removeTag);
                            foreach (XmlNode node in xmlchild)
                            {
                                node.ParentNode.RemoveChild(node);
                            }
                        }

                        //On 02/09/2017, Remove CostCategory/GROUP for the error (Cannot add a column named 'BILLCREDITPERIOD': 
                        //a nested table with the same name already belongs to this DataTable)
                        //for when we take CC and Ledger
                        if (enumTallyMaster == TallyMasters.Vouchers)
                        {
                            string removeTag = "//BILLCREDITPERIOD";
                            XmlNodeList xmlchild = xmldoc.SelectNodes(removeTag);
                            foreach (XmlNode node in xmlchild)
                            {
                                node.ParentNode.RemoveChild(node);
                            }
                        }
                        else if (enumTallyMaster == TallyMasters.VoucherType)
                        { // On 09/09/2023 'LASTNUMBERLIST.LIST': a nested table with the same name already belongs to this DataTable
                            string removeTag = "//TALLYMESSAGE//RESTARTFROMLIST.LIST";
                            XmlNodeList xmlchild = xmldoc.SelectNodes(removeTag);
                            foreach (XmlNode node in xmlchild)
                            {
                                node.ParentNode.RemoveChild(node);
                            }
                        }

                        //On 13/04/2018, In few latest version of Tally (GST enabled), 
                        //for error Nested '_UDF_788529254.LIST--------------------------------------------------
                        //It has to be verfififed in SDBINK - Donnor enabled Tally DB
                        var nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                        nsmgr.AddNamespace("UDF", "TallyUDF");
                        XmlNodeList xmlNodeList = xmldoc.SelectNodes("//UDF:*", nsmgr);

                        for (int i = 0; i <= xmlNodeList.Count - 1; i++)
                        {
                            xmlNodeList[i].ParentNode.RemoveChild(xmlNodeList[i]);
                        }
                        //--------------------------------------------------

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
        /// Retrive CC Master from tally xml for splitting DONORS, PURPOSES, STATES, COUNTRIES and SPONSERS
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
        /// Retrive CC Category Master from tally xml 
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        private ResultArgs FetchCCCategory()
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
                resultarg = ExecuteTallyXML(xml, TallyMasters.CostCategory);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        if (TallyResponseDataSet.Tables["COSTCATEGORY"] != null)
                        {
                            resultarg.DataSource.Data = TallyResponseDataSet.Tables["COSTCATEGORY"].DefaultView.ToTable(false, "NAME");
                            resultarg.Success = true;
                        }
                        else
                        {
                            resultarg.Message = "COSTCATEGORY are not available";
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

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in Fetching CostCategory.\n" + resultarg.Message;
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
                                        { "Suspense A/c", "Liabilities" }, {"Project Receipts/ Transfers","Liabilities"},{"Investment","Assets"} };

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

        /// <summary>
        /// Get Ledger Opening balance for given date and its primary group
        /// </summary>
        /// <param name="balancedate"></param>
        /// <returns></returns>
        private ResultArgs FetchLedgerOpeningBalance(string balancedate)
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            //DataTable LedgerCLBalance = new dsTallyConnector.LEDGERCLOSINGBALANCEDataTable();
            DataTable LedgerOPBalance = new DataTable();
            LedgerOPBalance.Columns.Add("LEDGER_NAME", typeof(string));
            LedgerOPBalance.Columns.Add("OPENING_BALANCE", typeof(string));
            LedgerOPBalance.Columns.Add("PRIMARY_GROUP", typeof(string));

            string ledgerxml = "<ENVELOPE>" +
                                "<HEADER>" +
                                        "<VERSION>1</VERSION>" +
                                        "<TALLYREQUEST>Export</TALLYREQUEST>" +
                                        "<TYPE>Collection</TYPE>" +
                                        "<ID>Collection of Ledgers</ID>" +
                                "</HEADER>" +
                            "<BODY>" +
                            "<DESC>" +
                            "<TDL>" +
                            "<TDLMESSAGE>" +
                             "<COLLECTION NAME='Collection of Ledgers' ISMODIFY='No' ISFIXED='No' ISINITIALIZE='No' ISOPTION='No' ISINTERNAL='No'>" +
                                "<TYPE>Ledger</TYPE>" +  //$_PrimaryGroup
                                "<NATIVEMETHOD>Name</NATIVEMETHOD>" +
                                "<NATIVEMETHOD>_PrimaryGroup</NATIVEMETHOD>" +
                                "<NATIVEMETHOD>OpeningBalance</NATIVEMETHOD>" +
                              "</COLLECTION>" +
                              "</TDLMESSAGE>" +
                              "</TDL>" +
                               "<STATICVARIABLES>" +
                                    "<SVFROMDATE TYPE='Date'>" + balancedate + "</SVFROMDATE>" +
                                "</STATICVARIABLES>" +
                            "</DESC>" +
                            "</BODY>" +
                            "</ENVELOPE>";

            try
            {
                resultarg = ExecuteTallyXML(ledgerxml, TallyMasters.Groups);
                if (resultarg.Success)
                {
                    TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        if (TallyResponseDataSet.Tables["LEDGER"] != null && TallyResponseDataSet.Tables["OPENINGBALANCE"] != null && TallyResponseDataSet.Tables["_PRIMARYGROUP"] != null) //CLOSINGBALANCE
                        {
                            DataTable dtLedgerName = TallyResponseDataSet.Tables["LEDGER"];
                            DataTable dtOpeningBalance = TallyResponseDataSet.Tables["OPENINGBALANCE"];
                            DataTable dtPRIMARYGROUP = TallyResponseDataSet.Tables["_PRIMARYGROUP"];

                            //On 16/02/2018, "Profit & Loss A/c" is predefined ledger in Tally under primary group --------------------------------
                            //We make it "Profit & Loss A/c" under Captial Account Group
                            dtLedgerName.DefaultView.RowFilter = "NAME ='" + LDR_PROFIT_LOSS + "'";
                            if (dtLedgerName.DefaultView.Count > 0)
                            {
                                Int32 Tally_Profit_Loss_LedgerId = utilitymember.NumberSet.ToInteger(dtLedgerName.DefaultView[0]["LEDGER_ID"].ToString());
                                DataRow drPrfitLossParent = dtPRIMARYGROUP.NewRow();
                                drPrfitLossParent["Type"] = "String";
                                drPrfitLossParent["_PRIMARYGROUP_TEXT"] = GRP_CAPTIAL_ACCOUNT;
                                drPrfitLossParent["LEDGER_ID"] = Tally_Profit_Loss_LedgerId.ToString();
                                dtPRIMARYGROUP.Rows.Add(drPrfitLossParent);
                            }
                            dtLedgerName.DefaultView.RowFilter = string.Empty;
                            //-----------------------------------------------------------------------------------------------------------------------


                            if (!dtOpeningBalance.Columns.Contains("OPENINGBALANCE_TEXT")) //CLOSINGBALANCE_TEXT
                            {
                                dtOpeningBalance.Columns.Add("OPENINGBALANCE_TEXT", typeof(System.Double)); //CLOSINGBALANCE_TEXT
                            }
                            dtLedgerName.Columns[0].ColumnMapping = MappingType.Attribute;
                            dtOpeningBalance.Columns[0].ColumnMapping = MappingType.Attribute;
                            dtPRIMARYGROUP.Columns[0].ColumnMapping = MappingType.Attribute;

                            var resultOpening = from drLedgerName in dtLedgerName.AsEnumerable()
                                                join drOPBalance in dtOpeningBalance.AsEnumerable() on drLedgerName.Field<Int32>("LEDGER_ID") equals drOPBalance.Field<Int32>("LEDGER_ID")
                                                join drPrimaryGroup in dtPRIMARYGROUP.AsEnumerable() on drLedgerName.Field<Int32>("LEDGER_ID") equals drPrimaryGroup.Field<Int32>("LEDGER_ID")
                                                select LedgerOPBalance.LoadDataRow(new object[]
                                        {
                                            drLedgerName.Field<string>("NAME"),
                                            drOPBalance.Field<string>("OPENINGBALANCE_TEXT"),
                                            drPrimaryGroup.Field<string>("_PRIMARYGROUP_TEXT"),
                                        }, false);

                            LedgerOPBalance = resultOpening.CopyToDataTable();
                            resultarg.Success = true;
                        }
                    }
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
                resultarg.DataSource.Data = LedgerOPBalance;
            }

            return resultarg;

        }

        /// <summary>
        /// Retriev Inventory Voucher Entries (Purhcase and Sales Vouchers)
        /// Sum item amount and make it as its child vouchers
        /// </summary>
        /// <returns>ResultArgs with dataset and its result sucess or failure</returns>
        public ResultArgs FetchInventoryVouchers(DataSet TallyResponseDataSet, DataTable dtChildVouchers, string fromdate, string todate)
        {
            ResultArgs resultarg = new ResultArgs();

            try
            {
                if (TallyResponseDataSet != null)
                {
                    if (TallyResponseDataSet.Tables["VOUCHER"] != null && TallyResponseDataSet.Tables["LEDGERENTRIES.LIST"] != null)
                    {
                        DataTable dtInventoryAllocation = new DataTable();
                        DataTable dtChildAccountVoucher = new dsTallyConnector.VOUCHERDETAILDataTable();

                        //For Temp: We could not add columns in dsTallyConnector, so we new columns here
                        dtInventoryAllocation.Columns.Add("VOUCHER_ID", typeof(Int32));
                        dtInventoryAllocation.Columns.Add("ALLINVENTORYENTRIES.LIST_ID", typeof(Int32));
                        dtInventoryAllocation.Columns.Add("ACCOUNTINGALLOCATIONS.LIST_ID", typeof(Int32));
                        //dtInventoryAllocation.Columns.Add("STOCKITEMNAME", typeof(String));
                        dtInventoryAllocation.Columns.Add("LEDGERNAME", typeof(String));
                        dtInventoryAllocation.Columns.Add("AMOUNT", typeof(decimal));
                        dtInventoryAllocation.Columns.Add("NARRATION", typeof(String));
                        dtInventoryAllocation.Columns.Add("ISDEEMEDPOSITIVE", typeof(String));

                        //For Temp: We could not add columns in dsTallyConnector, so we new columns here
                        dtChildAccountVoucher.Columns.Add("BANKERSDATE", typeof(string));
                        dtChildAccountVoucher.Columns.Add("BANKNAME", typeof(string));
                        dtChildAccountVoucher.Columns.Add("BANKBRANCHNAME", typeof(string));

                        DataTable dtInventoryMasterVoucher = TallyResponseDataSet.Tables["VOUCHER"]; //Master Vouchers
                        DataTable dtChildLedgerEntriesList = TallyResponseDataSet.Tables["LEDGERENTRIES.LIST"]; //lEDGER ENTRIES LIST
                        DataTable dtInventoryList = TallyResponseDataSet.Tables["ALLINVENTORYENTRIES.LIST"]; //INVENTORIES ENTRIES LIST
                        DataTable dtAccountingAllocationList = TallyResponseDataSet.Tables["ACCOUNTINGALLOCATIONS.LIST"];   //INVENTORY RELATED ACCOUNT LIST
                        DataTable dtBankReference = TallyResponseDataSet.Tables["BANKALLOCATIONS.LIST"]; //get bank dd, cheque, interbank reference number
                        dtInventoryMasterVoucher.DefaultView.RowFilter = "VCHTYPE IN ('Sales','Purchase') AND (ISOPTIONAL = 'No' AND ISCANCELLED = 'No')";
                        dtInventoryMasterVoucher = dtInventoryMasterVoucher.DefaultView.ToTable();

                        if (dtBankReference == null)
                        {
                            dtBankReference = new DataTable();
                            dtBankReference.Columns.Add("LEDGERENTRIES.LIST_Id", typeof(Int32));
                            dtBankReference.Columns.Add("TransactionType", typeof(string));
                            dtBankReference.Columns.Add("InstrumentDate", typeof(string));
                            dtBankReference.Columns.Add("InstrumentNumber", typeof(string));
                            dtBankReference.Columns.Add("BANKERSDATE", typeof(string));
                            dtBankReference.Columns.Add("BANKBRANCHNAME", typeof(string));
                            dtBankReference.Columns.Add("BANKNAME", typeof(string));
                        }


                        if (dtInventoryList != null)
                        {
                            var resultInventryLedgerEntries = from drInventoryList in dtInventoryList.AsEnumerable()
                                                              join drAccountingAllocationList in dtAccountingAllocationList.AsEnumerable()
                                                     on drInventoryList.Field<Int32?>("ALLINVENTORYENTRIES.LIST_ID") equals
                                                     (drAccountingAllocationList == null ? null : drAccountingAllocationList.Field<Int32?>("ALLINVENTORYENTRIES.LIST_ID"))
                                                              select dtInventoryAllocation.LoadDataRow(new object[]
                                                    {
                                                        drInventoryList.Field<Int32?>("VOUCHER_ID"),
                                                        drInventoryList.Field<Int32?>("ALLINVENTORYENTRIES.LIST_ID"),
                                                        drAccountingAllocationList.Field<Int32?>("ACCOUNTINGALLOCATIONS.LIST_ID"),
                                                        //drInventoryList.Field<string>("STOCKITEMNAME"),
                                                        drAccountingAllocationList.Field<string>("LEDGERNAME"),
                                                        Convert.ToDecimal(drAccountingAllocationList.Field<string>("AMOUNT")),
                                                        drAccountingAllocationList.Field<string>("NARRATION"),
                                                        drAccountingAllocationList.Field<string>("ISDEEMEDPOSITIVE")
                                                    }, true);
                            dtInventoryAllocation = resultInventryLedgerEntries.CopyToDataTable();
                        }

                        //Other Ledger
                        var resultChildVouhcer = from drChildVochers in dtChildLedgerEntriesList.AsEnumerable()
                                                 join drBankReference in dtBankReference.AsEnumerable()
                                                 on drChildVochers.Field<Int32?>("LEDGERENTRIES.LIST_Id") equals
                                                 (dtBankReference == null || !dtBankReference.Columns.Equals("LEDGERENTRIES.LIST_Id") ? null : drBankReference.Field<Int32?>("LEDGERENTRIES.LIST_Id"))
                                                 into bankreferencejoin
                                                 from drBankReference in bankreferencejoin.DefaultIfEmpty()
                                                 select dtChildAccountVoucher.LoadDataRow(new object[]
                                                    {
                                                        drChildVochers.Field<Int32?>("VOUCHER_ID"),
                                                        drChildVochers.Field<Int32?>("LEDGERENTRIES.LIST_ID"),
                                                        drChildVochers.Field<string>("LEDGERNAME"),
                                                        drChildVochers.Field<string>("AMOUNT"),
                                                        drChildVochers.Field<string>("NARRATION"),
                                                        drChildVochers.Field<string>("ISDEEMEDPOSITIVE"),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>("TransactionType")),
                                                        (drBankReference==null?string.Empty:ConvertTallyDate(drBankReference.Field<string>("InstrumentDate"))),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>("InstrumentNumber")),
                                                        (drBankReference==null?string.Empty:ConvertTallyDate(drBankReference.Field<string>("BANKERSDATE"))),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>("BANKNAME")),
                                                        (drBankReference==null?string.Empty:drBankReference.Field<string>("BANKBRANCHNAME"))
                                                    }, true);
                        dtChildAccountVoucher = resultChildVouhcer.CopyToDataTable();

                        //Party Ledger
                        DataTable dtPartyLedgers = new DataTable();
                        if (dtInventoryAllocation != null)
                        {
                            //In Purhcase and Sales, entreis will be repeated for every stock item, so we sum all items amount into single ledger and sum the amount
                            string[] unique = { "VOUCHER_ID", "LEDGERNAME" };
                            dtPartyLedgers = dtInventoryAllocation.DefaultView.ToTable(true, unique);

                            dtPartyLedgers.Columns.Add("ALLLEDGERENTRIES.LIST_Id", typeof(Int32));
                            dtPartyLedgers.Columns.Add("AMOUNT", typeof(decimal));
                            dtPartyLedgers.Columns.Add("ISDEEMEDPOSITIVE", typeof(string));

                            foreach (DataRow dr in dtPartyLedgers.Rows)
                            {
                                string computefilter = "VOUCHER_ID=" + dr["VOUCHER_ID"].ToString() + " AND LEDGERNAME = '" + dr["LEDGERNAME"].ToString().Replace(@"'", @"''") + "'";
                                string partyamount = dtInventoryAllocation.Compute("SUM(AMOUNT)", computefilter).ToString();
                                decimal Amount = utilitymember.NumberSet.ToDecimal(partyamount);
                                dr["ALLLEDGERENTRIES.LIST_Id"] = (Amount < 0 ? 1 : 0);
                                dr["AMOUNT"] = Amount;
                                dr["ISDEEMEDPOSITIVE"] = (Amount < 0 ? "Yes" : "No");
                                dtChildAccountVoucher.ImportRow(dr);
                                //dtChildLedgerEntriesList.ImportRow(dr);
                            }
                        }

                        //Attach Inventory Vouchers ---------------------
                        // Child Voucher 
                        //foreach (DataRow dr in dtChildLedgerEntriesList.Rows)
                        foreach (DataRow dr in dtChildAccountVoucher.Rows)
                        {
                            dtChildVouchers.ImportRow(dr);
                        }
                        //-----------------------------------------------
                    }
                    //else
                    //{
                    //    resultarg.Message = "Vouchers not available";
                    //}
                }
                //else
                //{
                //    resultarg.Message = "Vouchers not available";
                //}
                resultarg.Success = true;
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

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in getting Inventory Vouchers, " + resultarg.Message;
            }

            return resultarg;
        }

        /// <summary>
        /// On 10/10/2017, In Tally, they provide Cash/Bank Ledgers in journal entry 
        /// in Acme.erp, we dont allow so
        /// 
        /// we convert Journal entry if it affects Cash/Bank ledger as Receipt/Payment
        /// 1. Cash Bank in DR - Receipt Voucher
        /// 2. Cash Bank in CR - Payment Voucher
        /// 
        /// This method is used to change voucher type based on its child voucher details
        /// </summary>
        private ResultArgs CheckCashBankInJournalVoucher(DataTable dtMaster, DataTable dtChild, DataTable dtLedgers)
        {
            ResultArgs resultarg = new ResultArgs();
            DataTable dtCashBankJournal = new DataTable();
            dtCashBankJournal.Columns.Add("VOUCHER_ID", typeof(Int32));
            dtCashBankJournal.Columns.Add("LEDGERNAME", typeof(string));
            dtCashBankJournal.Columns.Add("LEDGERGROUP", typeof(string));
            dtCashBankJournal.Columns.Add("AMOUNT", typeof(string));
            dtCashBankJournal.Columns.Add("ISDEEMEDPOSITIVE", typeof(string));

            //1.Get Child Child entries which is affected by Bank/Cash
            var resultChildVoucher = from drchild in dtChild.AsEnumerable()
                                     join drLedger in dtLedgers.AsEnumerable() on drchild.Field<string>("LEDGERNAME")
                                     equals drLedger.Field<string>("LEDGERNAME")
                                     into ledgerggroupjoin
                                     from drLedger in ledgerggroupjoin.DefaultIfEmpty()
                                     where (drLedger.Field<string>("Parent").Equals(GRP_CASH_IN_HAND) || drLedger.Field<string>("Parent").Equals(GRP_BANK_ACCOUNTS))
                                     select dtCashBankJournal.LoadDataRow(new object[]
                                        {
                                            drchild.Field<Int32>("VOUCHER_ID"),
                                            drchild.Field<string>("LEDGERNAME"),
                                            drLedger.Field<string>("Parent"),
                                            drchild.Field<string>("AMOUNT"),
                                            drchild.Field<string>("ISDEEMEDPOSITIVE")
                                        }, true);
            try
            {

                if (resultChildVoucher.Any())
                {
                    dtCashBankJournal = resultChildVoucher.CopyToDataTable();

                    //2 Change Journal voucher's type  to receipt or payment if child entries are affected by Bank/Cash 
                    dtMaster.DefaultView.RowFilter = "TALLY_VOUCHER_TYPE='JOURNAL' AND PARTYLEDGERNAME <> '' ";
                    foreach (DataRowView drv in dtMaster.DefaultView)
                    {
                        string vid = drv["VOUCHER_ID"].ToString();
                        dtCashBankJournal.DefaultView.RowFilter = "VOUCHER_ID =" + vid;
                        string ChangedVoucherType = "JOURNAL";

                        if (dtCashBankJournal.DefaultView.Count > 0)
                        {
                            //if (dtCashBankJournal.DefaultView[0]["ISDEEMEDPOSITIVE"].ToString() == "No" || dtCashBankJournal.DefaultView[0]["LEDGERGROUP"].ToString() == GRP_BANK_OD_AC)
                            if (dtCashBankJournal.DefaultView[0]["ISDEEMEDPOSITIVE"].ToString() == "No")
                            {
                                ChangedVoucherType = "Payment";  //Cash Bank in DR - Receipt Voucher
                            }
                            else
                            {
                                ChangedVoucherType = "Receipt";  //Cash Bank in CR - Payment Voucher
                            }
                            drv["VOUCHERTYPENAME"] = ChangedVoucherType;
                            drv["BASEVOUCHERTYPE"] = ChangedVoucherType;
                            dtMaster.AcceptChanges();
                        }
                        dtChild.DefaultView.RowFilter = string.Empty;
                    }
                    dtMaster.DefaultView.RowFilter = string.Empty;
                }
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
                dtMaster.DefaultView.RowFilter = string.Empty;
                dtChild.DefaultView.RowFilter = string.Empty;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in checking Cash/Bank ledgers in Journal Vouchers, " + resultarg.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// On 09/12/2017, In Tally, they treat Bank OD Ledgers as bank Accounts
        /// in Acme.erp, we treat Bank OD ledgers as liability ledgers
        /// CASE 1 : we convert contra entry which affects Bank OD, as Receipt/Payment
        /// 1. Cash Bank in DR AND Bank OD in CR - Receipt Voucher
        /// 2. Cash Bank in CR AND Bank OD in DR - Payment Voucher
        /// 
        /// CASE 2 : we convert receipt/payment entry which affects Bank OD, as journal
        /// 
        /// This method is used to change voucher type based on its child voucher details
        /// </summary>
        private ResultArgs CheckBankODInVouchers(DataTable dtMaster, DataTable dtChild, DataTable dtLedgers)
        {
            ResultArgs resultarg = new ResultArgs();
            DataTable dtBankODContra = new DataTable();
            dtBankODContra.Columns.Add("VOUCHER_ID", typeof(Int32));
            dtBankODContra.Columns.Add("LEDGERNAME", typeof(string));
            dtBankODContra.Columns.Add("LEDGERGROUP", typeof(string));
            dtBankODContra.Columns.Add("AMOUNT", typeof(string));
            dtBankODContra.Columns.Add("ISDEEMEDPOSITIVE", typeof(string));

            //1. Get Child Child entries which is affected by Bank/Cash and Bank Od ledger
            //var resultBankODVoucher = from drchild in dtChild.AsEnumerable()
            //                          join drLedger in dtLedgers.AsEnumerable() on drchild.Field<string>("LEDGERNAME")
            //                          equals drLedger.Field<string>("$Name")
            //                          into ledgerggroupjoin
            //                          from drLedger in ledgerggroupjoin.DefaultIfEmpty()
            //                          where drLedger.Field<string>("$Parent").Equals(GRP_BANK_OD_AC)
            //                          select dtBankODContra.LoadDataRow(new object[]
            //                            {
            //                                drchild.Field<Int32>("VOUCHER_ID"),
            //                                drchild.Field<string>("LEDGERNAME"),
            //                                drLedger.Field<string>("$Parent"),
            //                                drchild.Field<string>("AMOUNT"),
            //                                drchild.Field<string>("ISDEEMEDPOSITIVE")
            //                            }, true);

            var resultBankODVoucher = from drchild in dtChild.AsEnumerable()
                                      join drLedger in dtLedgers.AsEnumerable() on drchild.Field<string>("LEDGERNAME")
                                      equals drLedger.Field<string>("LEDGERNAME")
                                      into ledgerggroupjoin
                                      from drLedger in ledgerggroupjoin.DefaultIfEmpty()
                                      join drODVoucher in
                                          (
                                              from drchild1 in dtChild.AsEnumerable()
                                              join drLedger in dtLedgers.AsEnumerable() on drchild1.Field<string>("LEDGERNAME")
                                              equals drLedger.Field<string>("LEDGERNAME")
                                              into ledgerggroupjoin
                                              from drLedger in ledgerggroupjoin.DefaultIfEmpty()
                                              where drLedger.Field<string>("Parent").Equals(GRP_BANK_OD_AC)
                                              select dtBankODContra.LoadDataRow(new object[]
                                                            {
                                                                drchild1.Field<Int32>("VOUCHER_ID"),
                                                            }, true)
                                              ) on drchild.Field<Int32>("VOUCHER_ID") equals drODVoucher.Field<Int32>("VOUCHER_ID")
                                      select dtBankODContra.LoadDataRow(new object[]
                                        {
                                            drchild.Field<Int32>("VOUCHER_ID"),
                                            drchild.Field<string>("LEDGERNAME"),
                                            drLedger.Field<string>("Parent"),
                                            drchild.Field<string>("AMOUNT"),
                                            drchild.Field<string>("ISDEEMEDPOSITIVE")
                                        }, true);

            try
            {
                if (resultBankODVoucher.Any())
                {
                    dtBankODContra = resultBankODVoucher.CopyToDataTable();

                    //2 Change contra voucher's type to receipt or payment if child entries are affected by Bank/Cash and bank OD
                    string ChangedVoucherType = "CONTRA";
                    dtMaster.DefaultView.RowFilter = "TALLY_VOUCHER_TYPE IN ('CONTRA','RECEIPT','PAYMENT')";
                    foreach (DataRowView drv in dtMaster.DefaultView)
                    {
                        string vid = drv["VOUCHER_ID"].ToString();
                        string vtype = drv["TALLY_VOUCHER_TYPE"].ToString();
                        dtBankODContra.DefaultView.RowFilter = "VOUCHER_ID =" + vid;
                        if (dtBankODContra.DefaultView.Count > 0)
                        {
                            //For Contra Voucher, check voucher type based on Bank/Cash in CR/DR 
                            if (vtype.ToUpper() == "CONTRA")
                            {
                                if (dtBankODContra.DefaultView[0]["ISDEEMEDPOSITIVE"].ToString() == "No" &&
                                   (dtBankODContra.DefaultView[0]["LEDGERGROUP"].ToString() == GRP_BANK_ACCOUNTS || dtBankODContra.DefaultView[0]["LEDGERGROUP"].ToString() == GRP_CASH_IN_HAND))
                                {
                                    ChangedVoucherType = "Payment";  //Cash Bank in CR AND Bank OD in DR
                                }
                                else
                                {
                                    ChangedVoucherType = "Receipt";  //Cash Bank in DR AND Bank OD in CR 
                                }
                            }
                            else //For Receipt/Payment Voucher, fix as Journal Voucher
                            {
                                ChangedVoucherType = "Journal";
                            }
                            drv["VOUCHERTYPENAME"] = ChangedVoucherType;
                            drv["BASEVOUCHERTYPE"] = ChangedVoucherType;
                            dtMaster.AcceptChanges();
                        }
                        dtChild.DefaultView.RowFilter = string.Empty;
                    }
                    dtMaster.DefaultView.RowFilter = string.Empty;
                }
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
                dtMaster.DefaultView.RowFilter = string.Empty;
                dtChild.DefaultView.RowFilter = string.Empty;
            }

            if (!resultarg.Success)
            {
                resultarg.Message = "Problem in checking Bank OD ledgers in Contra Vouchers, " + resultarg.Message;
            }

            return resultarg;
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

        #region ExportToTally

        /// <summary>
        /// 1. This method is used to replace &, ', "", > , < characters to xml format
        /// </summary>
        private string ReplaceXML(string xmlvalue)
        {
            string Rtn = xmlvalue;
            xmlvalue = xmlvalue.Replace("&", "&amp;");
            xmlvalue = xmlvalue.Replace("'", "&apos;");
            xmlvalue = xmlvalue.Replace("\"" + "" + "\"", "&quot;");
            xmlvalue = xmlvalue.Replace(">", "&gt;");
            xmlvalue = xmlvalue.Replace("<", "&lt;");
            Rtn = xmlvalue;
            return Rtn;
        }


        /// <summary>
        /// This method is ued to replace noise characters ("()*+,-./;=?{}~&"), in tally those characters are not considered in tally,
        /// so when we send data to tally, we replace those characters as empty
        /// </summary>
        /// <param name="xmlvalue"></param>
        /// <returns></returns>
        private string ReplaceNoiseCharacters(string xmlvalue)
        {
            string Rtn = xmlvalue;

            //string NoiseCharacters = "()*+,-./;=?{}~&";
            //char[] arrNoiseCharacters = NoiseCharacters.ToCharArray();

            //if (xmlvalue.IndexOfAny(arrNoiseCharacters) >= 0)
            //{
            //    foreach (var singleChar in arrNoiseCharacters)
            //    {
            //        xmlvalue = xmlvalue.Replace(singleChar.ToString(),string.Empty);
            //    }
            //}
            //Rtn = xmlvalue;
            return Rtn;
        }

        /// <summary>
        /// This property returns, tally is running or not
        /// </summary>
        public ResultArgs IsTallyConnectedByXML
        {
            get
            {
                ResultArgs resultArg = new ResultArgs();
                try
                {
                    string strRequestXML = "<ENVELOPE>" +
                    "<HEADER>" +
                    "<VERSION>1</VERSION>" +
                    "<TALLYREQUEST>EXPORT</TALLYREQUEST>" +
                    "<TYPE>FUNCTION</TYPE>" +
                        //<!-- Platform Function Name in Tally.ERP 9 -->
                    "<ID>$$LicenseInfo</ID>" + //$$SysInfo
                    "</HEADER>" +
                    "<BODY>" +
                    "<DESC>" +
                    "<FUNCPARAMLIST>" +
                        //<!-- Parameter for the function LicenseInfo -->
                    "<PARAM>Serial Number</PARAM>" + //ApplicationPath, Serial Number
                    "</FUNCPARAMLIST>" +
                    "</DESC>" +
                    "</BODY>" +
                    "</ENVELOPE>";
                    resultArg = FetchTallyXML(strRequestXML);
                }
                catch (Exception err)
                {
                    resultArg.Message = "Problem in connecting Tally, " + err.Message;
                }

                return resultArg;
            }
        }

        /// <summary>
        /// This method is used to execute xml in tally and check the return value and capture error message
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public ResultArgs ExecuteTallyXML(string xml)
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            bool Rtn = false;
            DataTable dtResult = null;
            string errorMsg = string.Empty;

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

                    //For timeout excetpion test by Carmel Raj
                    TallyRequest.Timeout = System.Threading.Timeout.Infinite;

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
                        string r = "[\x00-\x08\x0B\x0C\x0E-\x1F]"; //(x00-Null Byte, x08-Backspace, x0B-Vertical tab, x0C-Form feed, x0E-Shift out, x1F-Unit separator)
                        responseFromTallyServer = Regex.Replace(responseFromTallyServer, r, "", RegexOptions.Compiled);

                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(responseFromTallyServer);

                        XmlNodeReader xmlReader = new XmlNodeReader(xmldoc);
                        TallyResponseDataSet.ReadXml(xmlReader);
                    }
                    dataStream.Close();
                    response.Close();

                    if (TallyResponseDataSet.Tables.Count > 0)
                    {
                        if (TallyResponseDataSet.Tables.IndexOf("IMPORTRESULT") >= 0)
                        {
                            dtResult = TallyResponseDataSet.Tables["IMPORTRESULT"];
                        }
                        else if (TallyResponseDataSet.Tables.IndexOf("DATA") >= 0)
                        {
                            dtResult = TallyResponseDataSet.Tables["DATA"];
                        }
                        else if (TallyResponseDataSet.Tables.IndexOf("RESPONSE") >= 0)
                        {
                            dtResult = TallyResponseDataSet.Tables["RESPONSE"];
                        }

                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            if (dtResult.Columns.Contains("Errors") && dtResult.Rows[0]["Errors"].ToString() == "1")
                            {
                                if (dtResult.Columns.Contains("LineError"))
                                {
                                    errorMsg = dtResult.Rows[0]["LineError"].ToString();
                                }
                                else
                                {
                                    errorMsg = "Unable to process, Problem in Tally connector";
                                }
                                Rtn = false;
                            }
                            else
                            {
                                Rtn = true;
                            }
                        }
                    }
                    else
                    {
                        errorMsg = "Unable to process, Problem in Tally connector";
                    }
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        resultarg.Message = errorMsg;
                    }
                    resultarg.DataSource.Data = dtResult;
                    resultarg.Success = Rtn;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.Contains("Unable to connect to the remote server"))
                    {
                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
                    }
                    else if (msg.Contains("The remote server returned an error: (502) Bad Gateway"))
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
        /// This method is used to get data from tally 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public ResultArgs FetchTallyXML(string xml)
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();
            bool Rtn = false;
            string errorMsg = string.Empty;

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

                    //For timeout excetpion test by Carmel Raj
                    TallyRequest.Timeout = System.Threading.Timeout.Infinite;

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
                        string r = "[\x00-\x08\x0B\x0C\x0E-\x1F]"; //(x00-Null Byte, x08-Backspace, x0B-Vertical tab, x0C-Form feed, x0E-Shift out, x1F-Unit separator)
                        responseFromTallyServer = Regex.Replace(responseFromTallyServer, r, "", RegexOptions.Compiled);

                        //When we execute Tally Functions, we get error in terms in <ERRORMSG> TAG, so we trap that error and return
                        Int32 errorStartIndex = responseFromTallyServer.IndexOf("<ERRORMSG>") + "<ERRORMSG>".Length;
                        Int32 errorEndIndex = responseFromTallyServer.IndexOf("</ERRORMSG>");
                        if (errorStartIndex > 0 && errorEndIndex > errorStartIndex)
                        {
                            errorMsg = responseFromTallyServer.Substring(errorStartIndex, errorEndIndex - errorStartIndex).ToString();
                        }

                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.LoadXml(responseFromTallyServer);

                            //Remove CostCategory/GROUP for the error (Cannot add a column named 'NAME': 
                            //a nested table with the same name already belongs to this DataTable)
                            //for when we take CC and Ledger
                            //"//TALLYMESSAGE//COSTCATEGORY"
                            string removeTag = "//TALLYMESSAGE//COMPANY//NAME";
                            XmlNodeList xmlchild = xmldoc.SelectNodes(removeTag);
                            foreach (XmlNode node in xmlchild)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            removeTag = "//TALLYMESSAGE//GROUP//RESERVEDNAME";
                            xmlchild = xmldoc.SelectNodes(removeTag);
                            foreach (XmlNode node in xmlchild)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            XmlNodeReader xmlReader = new XmlNodeReader(xmldoc);
                            TallyResponseDataSet.ReadXml(xmlReader, XmlReadMode.Auto);
                            Rtn = true;
                        }
                        else
                        {
                            Rtn = false;
                        }
                    }
                    dataStream.Close();
                    response.Close();

                    resultarg.Message = errorMsg;
                    resultarg.Success = Rtn;
                    resultarg.DataSource.Data = TallyResponseDataSet;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.Contains("Unable to connect to the remote server"))
                    {
                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
                    }
                    else if (msg.Contains("The remote server returned an error: (502) Bad Gateway"))
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
        /// This property will return tally serial number
        /// </summary>
        public ResultArgs IsLicensedTally
        {
            get
            {
                ResultArgs resultArg = new ResultArgs();
                try
                {
                    string strRequestXML = "<ENVELOPE>" +
                                   "<HEADER>" +
                                   "<VERSION>1</VERSION>" +
                                   "<TALLYREQUEST>EXPORT</TALLYREQUEST>" +
                                   "<TYPE>FUNCTION</TYPE>" +
                        //<!-- Platform Function Name in Tally.ERP 9 -->
                                   "<ID>$$LicenseInfo</ID>" + //$$SysInfo
                                   "</HEADER>" +
                                   "<BODY>" +
                                   "<DESC>" +
                                   "<FUNCPARAMLIST>" +
                        //<!-- Parameter for the function LicenseInfo -->
                                   "<PARAM>Serial Number</PARAM>" + //ApplicationPath, Serial Number, IsLicensedMode, IsEducationalMode, IsAdmin
                                   "</FUNCPARAMLIST>" +
                                   "</DESC>" +
                                   "</BODY>" +
                                   "</ENVELOPE>";
                    resultArg = FetchTallyXML(strRequestXML);
                    if (resultArg.Success)
                    {
                        DataSet TallyResponseDataSet = resultArg.DataSource.TableSet;
                        if (TallyResponseDataSet.Tables.Count > 0)
                        {
                            if (TallyResponseDataSet.Tables.IndexOf("RESULT") > 0)
                            {
                                DataTable dtResult = TallyResponseDataSet.Tables["RESULT"];
                                if (dtResult != null && dtResult.Rows.Count > 0)
                                {
                                    string serialnumber = dtResult.Rows[0]["RESULT_text"].ToString();
                                    if (serialnumber == "0" || string.IsNullOrEmpty(serialnumber))
                                    {
                                        resultArg.Success = false;
                                    }
                                    else
                                    {
                                        resultArg.ReturnValue = serialnumber;
                                        resultArg.Success = true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    resultArg.Message = "Problem in checking licensed Tally, " + err.Message;
                }

                return resultArg;
            }
        }

        /// <summary>
        /// This method is used to get current company information like name of the company, its books beginng
        /// 1. get current company name
        /// 2. get its books of begining
        /// 3. LicenseMode
        /// </summary>
        public ResultArgs FetchCurrentCompanyDetails()
        {
            DataTable dtCompanyInfo = new DataTable();
            dtCompanyInfo.TableName = "Company";
            dtCompanyInfo.Columns.Add("COMPANY_NAME", typeof(System.String));
            dtCompanyInfo.Columns.Add("STARTING_FROM", typeof(System.String));
            dtCompanyInfo.Columns.Add("BOOKS_BEGIN", typeof(System.String));
            dtCompanyInfo.Columns.Add("LICENSEDTALLY", typeof(System.String));
            dtCompanyInfo.Columns.Add("ISDONORMODULEENABLED", typeof(Boolean));
            ResultArgs resultArg = new ResultArgs();
            try
            {
                //string companyXML = "<ENVELOPE>" +
                //                "<HEADER>" +
                //                    "<TALLYREQUEST>Export Data</TALLYREQUEST>" +
                //                "</HEADER>" +
                //                "<BODY>" +
                //                    "<EXPORTDATA>" +
                //                    "<REQUESTDESC>" +
                //                        "<REPORTNAME>List of Companies</REPORTNAME>" +
                //                    "</REQUESTDESC>" +
                //                    "</EXPORTDATA>" +
                //                "</BODY>" +
                //                "</ENVELOPE>";

                string companyXML = "<ENVELOPE>" +
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

                resultArg = FetchTallyXML(companyXML);
                if (resultArg.Success)
                {
                    DataSet TallyResponseDataSet = resultArg.DataSource.TableSet;

                    if (TallyResponseDataSet.Tables["STATICVARIABLES"] != null)
                    {
                        DataTable dtCurCompanyName = TallyResponseDataSet.Tables["STATICVARIABLES"];
                        if (dtCurCompanyName.Rows.Count > 0)
                        {
                            //1. Get company name
                            string companyname = string.Empty;
                            DataTable dtResult = TallyResponseDataSet.Tables["STATICVARIABLES"];
                            if (dtResult != null && dtResult.Rows.Count > 0)
                            {
                                companyname = dtResult.Rows[0]["SVCURRENTCOMPANY"].ToString();
                            }

                            //2. Get Boooks bening date
                            if (!string.IsNullOrEmpty(companyname))
                            {
                                string strBooksBeginXML = "<ENVELOPE>" +
                                       "<HEADER>" +
                                       "<VERSION>1</VERSION>" +
                                       "<TALLYREQUEST>EXPORT</TALLYREQUEST>" +
                                       "<TYPE>OBJECT</TYPE>" +
                                       "<SUBTYPE>Company</SUBTYPE>" +
                                       "<ID TYPE='Name'>" + ReplaceXML(companyname) + "</ID>" +
                                       "</HEADER>" +
                                       "<BODY>" +
                                       "<DESC>" +
                                       "<STATICVARIABLES>" +
                                       "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                                       "</STATICVARIABLES>" +
                                       "<FETCHLIST>" +
                                       "<FETCH>BooksFrom</FETCH>" +
                                       "<FETCH>StartingFrom</FETCH>" +
                                       "</FETCHLIST>" +
                                       "</DESC>" +
                                       "</BODY>" +
                                       "</ENVELOPE>";

                                resultArg = FetchTallyXML(strBooksBeginXML);
                                if (resultArg.Success)
                                {
                                    TallyResponseDataSet = resultArg.DataSource.TableSet;
                                    if (TallyResponseDataSet.Tables.Count > 0)
                                    {
                                        if (TallyResponseDataSet.Tables.IndexOf("BOOKSFROM") >= 0)
                                        {
                                            DataTable dtBooksBegin = TallyResponseDataSet.Tables["BOOKSFROM"];
                                            DataTable dtStartingFrom = TallyResponseDataSet.Tables["STARTINGFROM"];
                                            string booksfrom = string.Empty;
                                            string startingfrom = string.Empty;

                                            //1. Books Begin
                                            if (dtBooksBegin != null && dtBooksBegin.Rows.Count > 0)
                                            {
                                                booksfrom = dtBooksBegin.Rows[0]["BOOKSFROM_text"].ToString();
                                                if (booksfrom == "0" || string.IsNullOrEmpty(booksfrom))
                                                {
                                                    resultArg.Message = "Books Begin is empty";
                                                }
                                            }

                                            //2. Starting from
                                            if (dtStartingFrom != null && dtStartingFrom.Rows.Count > 0)
                                            {
                                                startingfrom = dtStartingFrom.Rows[0]["STARTINGFROM_text"].ToString();
                                                if (startingfrom == "0" || string.IsNullOrEmpty(startingfrom))
                                                {
                                                    resultArg.Message = "Starting from is empty";
                                                }
                                            }

                                            if (resultArg.Success)
                                            {
                                                DataRow dr = dtCompanyInfo.NewRow();
                                                dr["COMPANY_NAME"] = companyname;
                                                dr["BOOKS_BEGIN"] = ConvertTallyDate(booksfrom); //booksfrmom;
                                                dr["STARTING_FROM"] = ConvertTallyDate(startingfrom); //booksfrmom;
                                                ResultArgs result = IsLicensedTally;
                                                dr["LICENSEDTALLY"] = result.Success; //if always fase, if error occured
                                                dr["ISDONORMODULEENABLED"] = IsDonorModuleEnabled; //if always fase, if error occured
                                                dtCompanyInfo.Rows.Add(dr);

                                                resultArg.DataSource.Data = dtCompanyInfo;
                                                resultArg.Success = true;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        resultArg.Message = "Company name is not found";
                                    }

                                }
                            }
                            else
                            {
                                resultArg.Message = "Company name is not found";
                            }
                        }
                    }
                    else
                    {
                        resultArg.Message = "Tally company is not yet loaded";
                    }
                }

            }
            catch (Exception err)
            {
                resultArg.Message = "Problem in getting company detials, " + err.Message;
            }

            return resultArg;
        }

        /// <summary>
        /// This method is used to insert given ledger groups to tally
        /// 1. Get default Tally groups, its Parent should not be changted, so we skip those ledger groups
        /// 2. Define Revenue, IsdeemedPositive tags based on its nature
        /// 3.If it is first lever means, groups will be under "Primary" in Taly, so change parent gropup to Primary
        /// 
        /// 1. Check given ledger group is exists in tally, if not exists, create it
        /// 2. if given ledger group is exits but parent is differ in tally, create it (it will alter given parent)
        /// 
        /// ASSET - <ISREVENUE>No</ISREVENUE>
        ///        <ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>
        /// LIB - <ISREVENUE>No</ISREVENUE>
        ///     - <ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>
        /// INC - <ISREVENUE>Yes</ISREVENUE>
        ///     - <ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>
        /// EXP - <ISREVENUE>Yes</ISREVENUE>
        ///     - <ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>
        /// </summary>
        /// <param name="LedgerGroup"></param>
        /// <param name="LedgerGroupParent"></param>
        /// <param name="nature"></param>
        /// <param name="isFirstLevel"></param>
        /// <returns></returns>
        public ResultArgs InsertLedgerGroup(string LedgerGroup, string LedgerGroupParent, Natures nature, bool isFirstLevel)
        {
            ResultArgs resultarg = new ResultArgs();
            if (LedgerGroup.ToUpper() == "TEST_IN")
            {

            }

            try
            {
                //1. Get default Tally groups, its Parent should not be changted, so we skip those ledger groups
                string[] primarygroups = new string[] {  "Branch / Divisions", "Capital Account",
                                            "Current Assets",   "Current Liabilities",
                                            "Direct Expenses",   "Direct Incomes",
                                            "Fixed Assets", "Indirect Expenses",
                                            "Indirect Incomes", "Investments",
                                            "Loans (Liability)", "Misc. Expenses (ASSET)",
                                            "Purchase Accounts", "Sales Accounts",
                                            "Suspense A/c", "Project Receipts/ Transfers", "Investment",
                                            "Deposits (Asset)", "Bank OD A/c", "Secured Loans", 
                                            "Unsecured Loans", "Loans & Advances (Asset)", "Reserves & Surplus"};

                //2. Check is it Primay/Default ledger group, if so, skip, dont create or update it
                if (Array.IndexOf(primarygroups, LedgerGroup) < 0)
                {
                    string ISREVENUE = (nature == Natures.Income || nature == Natures.Expenses ? "YES" : "NO"); //For All Income and Expenses groups
                    string ISDEEMEDPOSITIVE = (nature == Natures.Assert || nature == Natures.Expenses ? "YES" : "NO"); //For All Asset and Expenses groups
                    //If it is first lever means, groups will be under "Primary" in Taly, so change parent gropup to Primary
                    LedgerGroupParent = (isFirstLevel ? " Primary" : LedgerGroupParent);

                    //Check duplication 
                    resultarg = IsMasterExists(LedgerGroup, LedgerGroupParent, TallyMasters.Groups);
                    if ((resultarg.Success && (bool)resultarg.ReturnValue == false))
                    {
                        string LedgerGroupInsertXML = "<ENVELOPE>" +
                                 "<HEADER>" +
                                    "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
                                    "</HEADER>" +
                                    "<BODY>" +
                                    "<IMPORTDATA>" +
                                    "<REQUESTDESC>" +
                                    "<REPORTNAME>All Masters</REPORTNAME>" +
                                    "</REQUESTDESC>" +
                                    "<REQUESTDATA>" +
                                    "<TALLYMESSAGE xmlns:UDF='TallyUDF'>" +
                                    "<GROUP NAME='" + ReplaceXML(LedgerGroup) + "' ACTION='Create'>" +
                                    "<NAME.LIST>" +
                                    "<NAME>" + ReplaceXML(LedgerGroup) + "</NAME>" +
                                    "</NAME.LIST>" +
                                    "<PARENT>" + ReplaceXML(LedgerGroupParent) + "</PARENT>" +
                                    "<ISREVENUE>" + ISREVENUE + "</ISREVENUE>" +
                                    "<ISDEEMEDPOSITIVE>" + ISDEEMEDPOSITIVE + "</ISDEEMEDPOSITIVE>" +
                            //"<ISSUBLEDGER>No</ISSUBLEDGER>" +
                            //"<ISBILLWISEON>No</ISBILLWISEON>" +
                            //"<ISCOSTCENTRESON>No</ISCOSTCENTRESON>" +
                                    "</GROUP>" +
                                    "</TALLYMESSAGE>" +
                                    "</REQUESTDATA>" +
                                    "</IMPORTDATA>" +
                                    "</BODY>" +
                                    "</ENVELOPE>";
                        resultarg = ExecuteTallyXML(LedgerGroupInsertXML);
                    }
                }
                else //If it is default tally group, return as success
                {
                    resultarg.Success = true;
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in inserting ledger group in Tally Connector, " + err.Message;
            }

            //Attach record details in the error
            if (!resultarg.Success)
            {
                string errormsg = resultarg.Message;
                errormsg += Environment.NewLine + "Ledger Group (" + LedgerGroup + "), Parent (" + LedgerGroupParent + ")";
                resultarg.Message = errormsg;
            }
            return resultarg;
        }

        /// <summary>
        /// This method is used to Expot List of ledgers to Tally
        /// 
        /// 1. Check given ledger is exists in tally, if not exists, create it
        /// 2. if given ledger is exits but parent is differ in tally, create it (it will alter given parent)
        /// </summary>
        /// <param name="LedgerGroup"></param>
        /// <param name="LedgerGroupParent"></param>
        /// <param name="nature"></param>
        /// <param name="isFirstLevel"></param>
        /// <returns></returns>
        public ResultArgs InsertLedger(string LedgerName, string LedgerGroup, bool IsCCEnabled, string AddressName, string Address, string State,
                    string Pincode, string Bank, string BankBranch, string AccountHolderName, string PanNumber, bool updateOPBalance, string OpAmount)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                //Check duplication
                resultarg = IsMasterExists(LedgerName, LedgerGroup, TallyMasters.Ledgers);
                if ((resultarg.Success && (bool)resultarg.ReturnValue == false) || updateOPBalance)
                {
                    //string ISCOSTCENTRESON = (IsCCEnabled==true?"YES":"NO");
                    string ISCOSTCENTRESON = (IsCCEnabled == true ? "<ISCOSTCENTRESON>YES</ISCOSTCENTRESON>" : string.Empty);
                    string OPENINGBALANCE = (updateOPBalance == true ? "<OPENINGBALANCE>" + OpAmount + "</OPENINGBALANCE>" : String.Empty);
                    string LedgerInsertXML = "<ENVELOPE>" +
                                                 "<HEADER>" +
                                                    "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
                                                    "</HEADER>" +
                                                    "<BODY>" +
                                                    "<IMPORTDATA>" +
                                                    "<REQUESTDESC>" +
                                                    "<REPORTNAME>All Masters</REPORTNAME>" +
                                                    "</REQUESTDESC>" +
                                                    "<REQUESTDATA>" +
                                                    "<TALLYMESSAGE xmlns:UDF='TallyUDF'>" +
                                                    "<LEDGER NAME='" + ReplaceXML(LedgerName) + "' ACTION='Create'>" +
                                                        OPENINGBALANCE +
                                                        "<NAME.LIST>" +
                                                        "<NAME>" + ReplaceXML(LedgerName) + "</NAME>" +
                                                        "</NAME.LIST>" +
                                                        "<MAILINGNAME.LIST>" +
                                                         "<MAILINGNAME>" + ReplaceXML(AddressName) + "</MAILINGNAME>" + //Only for Bank Accounts, SunderyCreditors, SundryDebtors
                                                        "</MAILINGNAME.LIST>" +
                                                        "<ADDRESS.LIST>" +
                                                            "<ADDRESS>" + ReplaceXML(Address) + "</ADDRESS>" +
                        //"<ADDRESS>addess2</ADDRESS>" +
                                                        "</ADDRESS.LIST>" +
                        //"<STATENAME>" + ReplaceXML(State) + "</STATENAME>" +
                                                        "<PINCODE>" + ReplaceXML(Pincode) + "</PINCODE>" +
                                                        "<BANKDETAILS>" + ReplaceXML(LedgerName) + "</BANKDETAILS>" +
                                                        "<BANKBRANCHNAME>" + ReplaceXML(BankBranch) + "</BANKBRANCHNAME>" +
                                                        "<BANKACCHOLDERNAME>" + ReplaceXML(AccountHolderName) + "</BANKACCHOLDERNAME>" +
                                                        "<INCOMETAXNUMBER>" + ReplaceXML(PanNumber) + "</INCOMETAXNUMBER>" +
                        //"<ISSUBLEDGER>No</ISSUBLEDGER>" +
                        //"<ISBILLWISEON>No</ISBILLWISEON>" +
                                                        ISCOSTCENTRESON +
                                                        "<PARENT>" + ReplaceXML(LedgerGroup) + "</PARENT>" +
                                                        "</LEDGER>" +
                                                    "</TALLYMESSAGE>" +
                                                    "</REQUESTDATA>" +
                                                    "</IMPORTDATA>" +
                                                "</BODY>" +
                                            "</ENVELOPE>";
                    resultarg = ExecuteTallyXML(LedgerInsertXML);
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in inserting ledger in Tally Connector, " + err.Message;
            }

            //Attach record details in the error
            if (!resultarg.Success)
            {
                string errormsg = resultarg.Message;
                errormsg += Environment.NewLine + "Ledger (" + LedgerName + "), Parent (" + LedgerGroup + ")";
                resultarg.Message = errormsg;
            }
            return resultarg;
        }

        /// <summary>
        /// This method is used to insert ledgers into Tally
        /// 
        /// In Tally Voucher Entry, Multiple CC category is not allowed, it accepts only for particular CC category, so we fix all CC are under "Primary Category"
        /// 
        /// Check Bank_OD ledgers and Bank Ledgers in Receipt and Payment, it will convert as Contra Voucher Type
        /// </summary>
        /// <param name="VoucherDate"></param>
        /// <param name="VoucherType"></param>
        /// <param name="VoucherTypeName">If it has values, it means multi voucher type data sync (ABE)</param>
        /// <param name="Narration"></param>
        /// <param name="dtCRs">It should be be DataTable for all CR ledgers 
        ///             (LEDGER_ID, LEDGER_GROUP, LEDGER_NAME, AMOUNT, CHEQUE_NO, MATERIALIZED_DATE, CHEQUE_REF_DATE, CHEQUE_REF_BANKNAME, CHEQUE_REF_BRANCH), 
        ///   contains all CR Ledgers</param>
        /// <param name="dtDRs">It should be be DataTable  for all DR ledgers 
        ///             (LEDGER_ID, LEDGER_GROUP, LEDGER_NAME, AMOUNT, CHEQUE_NO, MATERIALIZED_DATE, CHEQUE_REF_DATE, CHEQUE_REF_BANKNAME, CHEQUE_REF_BRANCH), 
        /// contains all DR Ledgers</param>
        /// <param name="dtCCdetails">It should be DataTable for CC details (VOUCHER_ID, LEDGER_ID, LEDGER_NAME, COST_CENTER_NAME, AMOUNT), contains cc voucher details</param>
        /// <returns></returns>
        public ResultArgs InsertVoucher(DateTime VoucherDate, DefaultVoucherTypes VoucherType, string VoucherNumber, string Narration, string NameAddress,
            DataTable dtCRs, DataTable dtDRs, DataTable dtCCdetails = null, string VoucherTypeName= "")
        {
            ResultArgs resultarg = new ResultArgs();
            
            try
            {
                //Check Bank_OD ledgers and Bank Ledgers in Receipt and Payment, it will convert as Contra Voucher Type
                resultarg = GetBankODLedgerVoucherType(VoucherType, dtCRs, dtDRs);
                if (resultarg.Success)
                {
                    //Get new voucher type based on its bank Od ledger
                    DefaultVoucherTypes TallyVouhcherType = (DefaultVoucherTypes)utilitymember.EnumSet.GetEnumItemType(typeof(DefaultVoucherTypes), resultarg.ReturnValue.ToString());
                    bool isVoucherTypeChangedByBankOD = (resultarg.RowsAffected==1);

                    //On 13/02/2019, for multi voucher type, if VoucherTypeName has values -------------------------------------------------------------
                    //it means multi voucher sync, it bank od means, take, as it is vouhcer type otherwise it will take multi voucher type name
                    //For bank od ledgers, system decides what is voucher type name
                    string TallyVouhcherTypeName = TallyVouhcherType.ToString();
                    if (!string.IsNullOrEmpty(VoucherTypeName) && !isVoucherTypeChangedByBankOD)
                    {
                        TallyVouhcherTypeName = VoucherTypeName;
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------

                    //1. Prepare CR ledgers
                    resultarg = GetLedgerEntries(VoucherDate, NameAddress, dtCRs, TransSource.Cr, dtCCdetails);
                    if (resultarg.Success)
                    {
                        string CRLedgers = resultarg.ReturnValue as string;
                        resultarg = GetLedgerEntries(VoucherDate, NameAddress, dtDRs, TransSource.Dr, dtCCdetails);
                        if (resultarg.Success)
                        {
                            //2. Prepare DR ledgers
                            string DRLedgers = resultarg.ReturnValue as string;

                            string VoucherInsertXml = "<ENVELOPE>" +
                                                "<HEADER>" +
                                                "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
                                                "</HEADER>" +
                                                "<BODY>" +
                                                "<IMPORTDATA>" +
                                                "<REQUESTDESC>" +
                                                "<REPORTNAME>Vouchers</REPORTNAME>" +
                                                "<STATICVARIABLES>" +
                                                "<SVCURRENTCOMPANY>" + "##SVCURRENTCOMPANY" + "</SVCURRENTCOMPANY>" +
                                                "</STATICVARIABLES>" +
                                                "</REQUESTDESC>" +
                                                "<REQUESTDATA>" +
                                                "<TALLYMESSAGE>" +
                                                //"<VOUCHER VCHTYPE=" + "\"" + "" + VoucherType.ToString() + "" + "\" ACTION=" + "\"" + "Create" + "\">" +
                                                "<VOUCHER VCHTYPE=" + "\"" + "" + TallyVouhcherTypeName + "" + "\" ACTION=" + "\"" + "Create" + "\">" +
                                                //"<VOUCHERNUMBER>" + strVchNumber + "</VOUCHERNUMBER>" +
                                                "<DATE>" + VoucherDate.ToShortDateString() + "</DATE>" +
                                                "<EFFECTIVEDATE>" + VoucherDate.ToShortDateString() + "</EFFECTIVEDATE>" +
                                                "<NARRATION>" + ReplaceXML(Narration) + "</NARRATION>" +
                                                //"<VOUCHERTYPENAME>" + ReplaceXML(VoucherType.ToString()) + "</VOUCHERTYPENAME>" +
                                                "<VOUCHERTYPENAME>" + ReplaceXML(TallyVouhcherTypeName) + "</VOUCHERTYPENAME>" +

                                                //If CR, DR ledgers order should be based on type of voucher
                                                //Credit Ledgers
                                                (TallyVouhcherType == DefaultVoucherTypes.Receipt || TallyVouhcherType == DefaultVoucherTypes.Contra ? CRLedgers : DRLedgers) +

                                                //Debit Ledgers
                                                (TallyVouhcherType == DefaultVoucherTypes.Receipt || TallyVouhcherType == DefaultVoucherTypes.Contra ? DRLedgers : CRLedgers) +
                                                "</VOUCHER>" +
                                                "</TALLYMESSAGE>" +
                                                "</REQUESTDATA>" +
                                                "</IMPORTDATA>" +
                                                "</BODY>" +
                                                "</ENVELOPE>";
                            resultarg = ExecuteTallyXML(VoucherInsertXml);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in inserting vouchers in Tally Connector,  " + err.Message;
            }

            //Attach record details in the error
            if (!resultarg.Success)
            {
                string errormsg = resultarg.Message;
                errormsg += Environment.NewLine + "Voucher Type (" + VoucherType.ToString() + ") " +
                                        "Voucher Date (" + VoucherDate.ToShortDateString() + ") " +
                                        "Voucher Number (" + VoucherNumber + ")";
                resultarg.Message = errormsg;
            }
            return resultarg;
        }


        /// <summary>
        /// This method is used to prepare ledger entires for both CRs and DRs ledgers and its Bank allocation detials for BRS and CC
        /// </summary>
        /// <param name="dtLedgerEntries"></param>
        /// <param name="transsource"></param>
        /// <returns></returns>
        public ResultArgs GetLedgerEntries(DateTime voucherdate, string nameaddress, DataTable dtLedgerEntries, TransSource transsource, DataTable dtCCLedgerDetails = null)
        {
            ResultArgs resultarg = new ResultArgs();
            string Rtn = string.Empty;
            Int32 LedgerId = 0;
            Int32 LedgerSequenceNo = 0;
            try
            {
                foreach (DataRow dr in dtLedgerEntries.Rows)
                {
                    LedgerId = utilitymember.NumberSet.ToInteger(dr[AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                    LedgerSequenceNo = utilitymember.NumberSet.ToInteger(dr[AppSchema.VoucherTransaction.SEQUENCE_NOColumn.ColumnName].ToString());
                    string LEDGER_NAME = dr[AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                    string LEDGER_GROUP = dr[AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();

                    string AMOUNT = dr[AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString();
                    string DEEMEDPOSITIVE = (transsource == TransSource.Cr ? "NO" : "YES");
                    string CHEQUENUMBER = dr[AppSchema.VoucherTransaction.CHEQUE_NOColumn.ColumnName].ToString();
                    string MATERIALIZEDDATE = dr[AppSchema.VoucherTransaction.MATERIALIZED_ONColumn.ColumnName].ToString();
                    MATERIALIZEDDATE = (!String.IsNullOrEmpty(MATERIALIZEDDATE) ? utilitymember.DateSet.ToDate(MATERIALIZEDDATE) : string.Empty);

                    //CHEQUE_REF_DATE is mandatory in tally
                    //Check ref date should be less than or equal to voucher date
                    string CHEQUE_REF_DATE = dr[AppSchema.VoucherTransaction.CHEQUE_REF_DATEColumn.ColumnName].ToString();
                    if (!String.IsNullOrEmpty(CHEQUE_REF_DATE))
                    {
                        CHEQUE_REF_DATE = utilitymember.DateSet.ToDate(CHEQUE_REF_DATE);
                        if (DateTime.Compare(utilitymember.DateSet.ToDate(CHEQUE_REF_DATE, false), voucherdate) > 0)
                        {
                            CHEQUE_REF_DATE = voucherdate.ToShortDateString();
                        }
                    }
                    else
                    {
                        CHEQUE_REF_DATE = voucherdate.ToShortDateString();
                    }

                    //Materialised date should be greater than or equal to cheque date and voucher date
                    if (!String.IsNullOrEmpty(MATERIALIZEDDATE))
                    {
                        MATERIALIZEDDATE = utilitymember.DateSet.ToDate(MATERIALIZEDDATE);
                        if (DateTime.Compare(utilitymember.DateSet.ToDate(MATERIALIZEDDATE, false), voucherdate) < 0)
                        {
                            MATERIALIZEDDATE = voucherdate.ToShortDateString();
                        }
                        else if (!String.IsNullOrEmpty(CHEQUE_REF_DATE) &&
                                    DateTime.Compare(utilitymember.DateSet.ToDate(MATERIALIZEDDATE, false),
                                    utilitymember.DateSet.ToDate(CHEQUE_REF_DATE, false)) < 0)
                        {
                            MATERIALIZEDDATE = voucherdate.ToShortDateString();
                        }
                    }

                    string CHEQUE_REF_BANKNAME = dr[AppSchema.VoucherTransaction.CHEQUE_REF_BANKNAMEColumn.ColumnName].ToString();
                    string CHEQUE_REF_BRANCH = dr[AppSchema.VoucherTransaction.CHEQUE_REF_BRANCHColumn.ColumnName].ToString();
                    AMOUNT = (DEEMEDPOSITIVE.ToUpper() == "NO" ? AMOUNT : "-" + AMOUNT);

                    //For Bank cheque/dd/interbank details, if ledger group is bank accoutns, updtae bank allocation details
                    string BankLedgerDetails = string.Empty;
                    //if (!string.IsNullOrEmpty(CHEQUENUMBER) || !string.IsNullOrEmpty(MATERIALIZEDDATE))
                    if (LEDGER_GROUP == GRP_BANK_ACCOUNTS)
                    {
                        BankLedgerDetails = "<BANKALLOCATIONS.LIST>" +
                                        "<DATE>" + CHEQUE_REF_DATE + "</DATE>" +
                                        (string.IsNullOrEmpty(MATERIALIZEDDATE) ? string.Empty : "<BANKERSDATE>" + MATERIALIZEDDATE + "</BANKERSDATE>") +
                                        "<BANKBRANCHNAME>" + ReplaceXML(CHEQUE_REF_BANKNAME) + "</BANKBRANCHNAME>" +
                                        "<TRANSACTIONTYPE>Others</TRANSACTIONTYPE>" + //In Acme.erp not transaction types like Cheque, DD, Inter bank
                                        "<BANKNAME>" + ReplaceXML(CHEQUE_REF_BRANCH) + "</BANKNAME>" +
                                        "<INSTRUMENTDATE>" + CHEQUE_REF_DATE + "</INSTRUMENTDATE>" +
                                        "<INSTRUMENTNUMBER>" + ReplaceXML(CHEQUENUMBER) + "</INSTRUMENTNUMBER>" +
                                        "<AMOUNT>" + AMOUNT + "</AMOUNT>" +
                                        "<STATUS>No</STATUS>" +
                                        "<PAYMENTFAVOURING>" + ReplaceXML(nameaddress) + "</PAYMENTFAVOURING>" +
                                        "<PAYMENTMODE>Transacted</PAYMENTMODE>" +
                                        "<NAME>" + Guid.NewGuid().ToString() + "</NAME>" +
                                    "</BANKALLOCATIONS.LIST>";
                    }

                    //For CC transaction details
                    string CCLedgerDetails = string.Empty;
                    if (dtCCLedgerDetails != null)
                    {
                        //To make sum of single CC name as tally does nt accept CC duplication and must be cc category order
                        dtCCLedgerDetails.DefaultView.RowFilter = string.Empty;
                        //30/04/2019, to check ledger sequence no in CC
                        //dtCCLedgerDetails.DefaultView.RowFilter = AppSchema.Ledger.LEDGER_IDColumn.ColumnName + "=" + LedgerId.ToString();
                        dtCCLedgerDetails.DefaultView.RowFilter = AppSchema.Ledger.LEDGER_IDColumn.ColumnName + "=" + LedgerId.ToString() +
                                                 " AND " + AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn.ColumnName + "=" + LedgerSequenceNo.ToString();
                        dtCCLedgerDetails.DefaultView.Sort = AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName;
                        string[] distinctCC = { AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName, AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName, 
                                              AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName};
                        DataTable dtUniqueCC = dtCCLedgerDetails.DefaultView.ToTable(true, distinctCC);

                        string CCPreviousCategoryName = string.Empty;
                        if (dtUniqueCC.Rows.Count > 0)
                        {
                            foreach (DataRow drCC in dtUniqueCC.Rows)
                            {
                                //In Tally Voucher Entry, Multiple CC category is not allowed, it accepts only for particular CC category, so we fix all CC are under "Acmeerp Cost Category"
                                string CCCategory = Def_CC_Category; //drCC[AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName].ToString();                                
                                string CC = drCC[AppSchema.VoucherCostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                                Int32 CCId = utilitymember.NumberSet.ToInteger(drCC[AppSchema.VoucherCostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());
                                //30/04/2019, to check ledger sequence no in CC
                                //object CCAMount = dtCCLedgerDetails.Compute("SUM(AMOUNT)", AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName + "=" + CCId.ToString() + " AND LEDGER_SEQUENCE_NO="+ LedgerSequenceNo);
                                object CCAMount = dtCCLedgerDetails.Compute("SUM(AMOUNT)",
                                      AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName + "=" + CCId.ToString() + " AND " + AppSchema.VoucherCostCentre.LEDGER_SEQUENCE_NOColumn.ColumnName + "=" + LedgerSequenceNo.ToString());

                                CCAMount = (DEEMEDPOSITIVE.ToUpper() == "NO" ? CCAMount : "-" + CCAMount);

                                if (CCPreviousCategoryName != CCCategory)
                                {
                                    CCLedgerDetails += (!string.IsNullOrEmpty(CCLedgerDetails) ? "</CATEGORYALLOCATIONS.LIST>" : string.Empty);
                                    CCLedgerDetails += "<CATEGORYALLOCATIONS.LIST>" +
                                                            "<CATEGORY>" + ReplaceXML(CCCategory) + "</CATEGORY>" +
                                                             "<ISDEEMEDPOSITIVE>" + DEEMEDPOSITIVE + "</ISDEEMEDPOSITIVE>";
                                }

                                CCLedgerDetails += "<COSTCENTREALLOCATIONS.LIST>" +
                                                    "<NAME>" + ReplaceXML(CC) + "</NAME>" +
                                                     "<AMOUNT>" + CCAMount.ToString() + "</AMOUNT>" +
                                                   "</COSTCENTREALLOCATIONS.LIST>";
                                CCPreviousCategoryName = CCCategory;
                            }
                            CCLedgerDetails += "</CATEGORYALLOCATIONS.LIST>";
                        }
                        dtCCLedgerDetails.DefaultView.RowFilter = string.Empty;
                    }
                    //CCLedgerDetails = string.Empty;
                    Rtn += "<ALLLEDGERENTRIES.LIST>" +
                                "<LEDGERNAME>" + ReplaceXML(LEDGER_NAME) + "</LEDGERNAME>" +
                                "<ISDEEMEDPOSITIVE>" + DEEMEDPOSITIVE + "</ISDEEMEDPOSITIVE>" +
                                "<AMOUNT>" + AMOUNT + "</AMOUNT>" +

                                //Bank Allocation Details
                                BankLedgerDetails +

                                //Costcenter Allocation Details
                                CCLedgerDetails +
                           "</ALLLEDGERENTRIES.LIST>";
                }

                resultarg.ReturnValue = Rtn;
                resultarg.Success = true;
            }
            catch (Exception err)
            {
                dtCCLedgerDetails.DefaultView.RowFilter = string.Empty;
                resultarg.Message = "Problem in inserting vouchers (Preparing ledger entries) in Tally Connector,  " + err.Message;
            }
            return resultarg;
        }


        /// <summary>
        /// This method is used to delete delete vouchers
        /// 1. Deletion : based on Voucher Number and Voucher, Voucher Type
        /// 2. If Voucher Number generation is automatic, if one voucher is deleted, next voucher's will be regenerated, so we chage order
        /// VCHTYPE , VOUCHER_ID DESC, it will give last voucher and delete it. (Deletes from Last Record to First Record)
        /// 
        /// *** For temp: Take all Vouchers (Receipt, Payments, Contra and Journal) and delete one by one (by passing Voucher Date, Voucher Number and Voucher Type)
        /// *** We have to study how to do bulk delete in tally (like Ctl + Space bar in tally)
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public ResultArgs DeleteVouchers(DateTime fromdate, DateTime todate)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                resultarg = FetchVouchersRegister(fromdate, todate);
                if (resultarg.Success)
                {
                    //1. Get vouchers from tally (Receipt, Payment, Contra and Journal)
                    DataTable dtMasterVouchers = resultarg.DataSource.Table;

                    //2. Filter only (Receipt, Payment, Contra and Journal) and change the order for deletion (VO No is getting regenerated when one voucer is deleted)
                    dtMasterVouchers.DefaultView.RowFilter = "VCHTYPE IN ('" + DefaultVoucherTypes.Receipt + "','" + DefaultVoucherTypes.Payment + "'," +
                                                              "'" + DefaultVoucherTypes.Contra + "','" + DefaultVoucherTypes.Journal + "')";
                    //dtMasterVouchers.DefaultView.Sort = "VCHTYPE, VOUCHER_ID DESC";
                    dtMasterVouchers = dtMasterVouchers.DefaultView.ToTable();

                    string errormsg = string.Empty;
                    string vouchersXML = string.Empty;
                    int deleteedrows = 0;

                    foreach (DataRow dr in dtMasterVouchers.Rows)
                    {
                        string tallydate = dr["DATE"].ToString(); ;
                        string vdate = tallydate;
                        string vtype = dr["VCHTYPE"].ToString();
                        string vno = dr["VOUCHERNUMBER"].ToString();
                        string remoteid = dr["RemoteId"].ToString();
                        string vchkey = dr["VCHKEY"].ToString();
                        string voucher_id = dr["VOUCHER_ID"].ToString();
                        string guid = dr["GUID"].ToString();
                        string masterid = dr["MasterId"].ToString();
                        //vouchersXML = "<VOUCHER Date ='" + vdate + "' TAGNAME = 'Voucher Number' TAGVALUE='" + vno + "' VCHTYPE = '" + vtype + "' ACTION='Delete'></VOUCHER>";                       
                        vouchersXML = "<VOUCHER Date ='" + vdate + "' TAGNAME = 'Masterid' TAGVALUE='" + masterid + "' VCHTYPE = '" + vtype + "' ACTION='Delete'></VOUCHER>";

                        string DeleteVoucherXML = "<ENVELOPE>" +
                                            "<HEADER>" +
                                             "<VERSION>1</VERSION>" +
                                             "<TALLYREQUEST>Import</TALLYREQUEST>" +
                                             "<TYPE>Data</TYPE>" +
                                             "<ID>Vouchers</ID>" +
                                             "</HEADER>" +
                                             "<BODY>" +
                                            "<DESC>" +
                                             "</DESC>" +
                                             "<DATA>" +
                                             "<TALLYMESSAGE>" + vouchersXML +
                                            "</TALLYMESSAGE>" +
                                            "</DATA>" +
                                            "</BODY>" +
                                            "</ENVELOPE>";
                        resultarg = ExecuteTallyXML(DeleteVoucherXML);
                        if (resultarg.Success)
                        {
                            deleteedrows++;
                        }
                        else
                        {
                            errormsg = resultarg.Message;
                            break;
                        }
                    }

                    if (dtMasterVouchers.Rows.Count != deleteedrows)
                    {
                        resultarg.Message = "Problem in deleting vouchers in Tally Connector,  " + errormsg;
                    }
                    else if (dtMasterVouchers.Rows.Count == deleteedrows)
                    {
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = "Problem in deleting vouchers in Tally Connector, " + ex.Message;
            }
            finally
            {
            }

            return resultarg;
        }


        /// <summary>
        /// This method is used to get voucher register for given date range
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public ResultArgs FetchVouchersRegister(DateTime fromdate, DateTime todate)
        {
            ResultArgs resultarg = new ResultArgs();
            DataSet TallyResponseDataSet = new DataSet();

            string VoucherRegisterXML = "<ENVELOPE>"
                            + "<HEADER>"
                            + "<VERSION>1</VERSION>"
                            + "<TALLYREQUEST>Export</TALLYREQUEST>"
                            + "<TYPE>Data</TYPE>"
                            + "<ID>Voucher Register</ID>" //Voucher Register
                            + "</HEADER>"
                            + "<BODY>"
                            + "<DESC>"
                                + "<STATICVARIABLES>"
                                    + "<SVFROMDATE TYPE=\"Date\">" + fromdate.ToShortDateString() + "</SVFROMDATE>"
                                    + "<SVTODATE TYPE=\"Date\">" + todate.ToShortDateString() + "</SVTODATE>"
                                    + "<EXPLODEFLAG>Yes</EXPLODEFLAG>"
                                    + "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>"
                                + "</STATICVARIABLES>"
                            + "</DESC>"
                            + "</BODY>"
                            + "</ENVELOPE>";
            try
            {
                resultarg = ExecuteTallyXML(VoucherRegisterXML, TallyMasters.Vouchers);
                TallyResponseDataSet = resultarg.DataSource.TableSet;
                if (TallyResponseDataSet != null)
                {
                    if (TallyResponseDataSet.Tables["VOUCHER"] != null)
                    {
                        resultarg.DataSource.Data = TallyResponseDataSet.Tables["VOUCHER"];
                        resultarg.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultarg.Message = "Problem in fetching vouchers in Tally Connector, " + ex.Message;
            }
            finally
            {
            }
            return resultarg;
        }


        /// <summary>
        /// This method is used to enable, Company featues like 
        /// 1. Enable COST CENTER, 2. COST CATEGORY
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="ENABLECOSTCENTRE"></param>
        /// <returns></returns>
        public ResultArgs UpdateCompanyFeatures(string CompanyName, bool ENABLECOSTCENTRE)
        {
            ResultArgs resultarg = new ResultArgs();

            try
            {
                //For Temp:, enable comapny features only if cc enabled
                if (ENABLECOSTCENTRE)
                {
                    string UpdateCompanyFeaturesXML =
                                  "<ENVELOPE>" +
                                    "<HEADER>" +
                                        "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
                                        "</HEADER>" +
                                        "<BODY>" +
                                        "<IMPORTDATA>" +
                                        "<REQUESTDESC>" +
                                        "<REPORTNAME>All Masters</REPORTNAME>" +
                                        "</REQUESTDESC>" +
                                        "<REQUESTDATA>" +
                                        "<TALLYMESSAGE xmlns:UDF='TallyUDF'>" +
                                        "<!— TO DO: Specify the company Name as it appears in Tally -->" +
                                        "<COMPANY NAME='" + ReplaceXML(CompanyName) + "' ACTION='Alter'>" +
                                        "<!—  enable Maintain Cost Center -->" +
                                        "<IsCostCentresOn>" + (ENABLECOSTCENTRE == true ? "YES" : "NO") + "</IsCostCentresOn>" +
                                        "<IsCostCategoryOn>" + (ENABLECOSTCENTRE == true ? "YES" : "NO") + "</IsCostCategoryOn>" +
                                        "</COMPANY>" +
                                        "</TALLYMESSAGE>" +
                                        "</REQUESTDATA>" +
                                        "</IMPORTDATA>" +
                                        "</BODY>" +
                                        "</ENVELOPE>";
                    resultarg = ExecuteTallyXML(UpdateCompanyFeaturesXML);
                }
                else
                {
                    resultarg.Success = true;
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in updating company featues in Tally Connector,  " + err.Message;
            }
            return resultarg;
        }


        /// <summary>
        /// This method is used to insert Voucher Type
        /// </summary>
        /// <param name="VoucherTypeName"></param>
        /// <param name="BaseVoucherType"></param>
        /// <param name="beginingnumber"></param>
        /// <param name="isPreFillZero"></param>
        /// <param name="widthnumber"></param>
        /// <returns></returns>
        public ResultArgs InsertVoucherType(string VoucherTypeName, string BaseVoucherType, Int32 beginingnumber, bool isPreFillZero, Int32 widthnumber)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                //Check duplication 
                //resultarg = IsMasterExists(VoucherTypeName, BaseVoucherType, TallyMasters.VoucherType);
                //if ((resultarg.Success && (bool)resultarg.ReturnValue == false))
                //For Temp purpose
                if (1==1)
                {
                    string VoucherTypeInsertXML =
                                "<ENVELOPE>" +
                                    "<HEADER>" +
                                      "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
                                    "</HEADER>" +
                                    "<BODY>" +
                                       "<IMPORTDATA>" +
                                        "<REQUESTDESC>" +
                                          "<REPORTNAME>All Masters</REPORTNAME>" +
                                        "</REQUESTDESC>" +
                                        "<REQUESTDATA>" +
                                            "<TALLYMESSAGE xmlns:UDF='TallyUDF'>" +
                                                "<VOUCHERTYPE NAME = '"+ ReplaceXML(VoucherTypeName) +"' RESERVEDNAME='' ACTION='Create'>" +
                                                //"<VOUCHERTYPENAME>testrpt</VOUCHERTYPENAME>" +
                                                "<PARENT>"+ BaseVoucherType +"</PARENT>" +
                                                "<MAILINGNAME>" + BaseVoucherType.Substring(0, 4) + "</MAILINGNAME>" +
                                                "<NUMBERINGMETHOD>Automatic</NUMBERINGMETHOD>" +
                                                "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" +
                                                "<PREFILLZERO>" + (isPreFillZero ? "Yes" : "No") + "</PREFILLZERO>" +
                                                "<BEGINNINGNUMBER>" + beginingnumber.ToString() + "</BEGINNINGNUMBER>" +
                                                "<WIDTHOFNUMBER>" + widthnumber.ToString() + "</WIDTHOFNUMBER>" +
                                                "<SORTPOSITION> 30</SORTPOSITION>" +
                                                //"<LANGUAGENAME.LIST>" +
                                                //"<NAME.LIST TYPE='String'>" +
                                                //  "<NAME>'testrpt'</NAME>" +
                                                // "</NAME.LIST>" +
                                                //"<LANGUAGEID> 1033</LANGUAGEID>" +
                                                //"</LANGUAGENAME.LIST>" +
                                                "</VOUCHERTYPE>" +
                                            "</TALLYMESSAGE>" +
                                        "</REQUESTDATA>" +
                                       "</IMPORTDATA>" +
                                    "</BODY>" +
                                "</ENVELOPE>";
                    resultarg = ExecuteTallyXML(VoucherTypeInsertXML.ToLower());
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in inserting Voucher Type in Tally Connector, " + err.Message;
            }

            //Attach record details in the error
            if (!resultarg.Success)
            {
                string errormsg = resultarg.Message;
                if (errormsg.ToUpper().Contains("NO VALID NAMES!"))
                {
                    errormsg = "Voucher Type is not available in TALLY.";
                }
                errormsg += Environment.NewLine + " Create Voucher Type (" + VoucherTypeName + "), Base Voucher (" + BaseVoucherType + ") in TALLY";
                //resultarg.Message = errormsg;
                resultarg.Message = string.Empty;
                resultarg.Success = false;
            }
            return resultarg;
        }

        /// <summary>
        /// This method is used to insert Cost center
        /// 
        /// In Tally Voucher Entry, Multiple CC category is not allowed, it accepts only for particular CC category, so we fix all CC are under "Primary Category"
        /// </summary>
        /// <param name="CostCenter"></param>
        /// <param name="CostCategory"></param>
        /// <returns></returns>
        public ResultArgs InsertCostCenter(string CostCenter, string CostCategory)
        {
            //In Tally Voucher Entry, Multiple CC category is not allowed, it accepts only for particular CC category, so we fix all CC are under "Acmeerp Cost Category"
            CostCategory = Def_CC_Category;

            ResultArgs resultarg = new ResultArgs();
            try
            {
                //Check duplication 
                resultarg = IsMasterExists(CostCenter, CostCategory, TallyMasters.CostCenters);
                if ((resultarg.Success && (bool)resultarg.ReturnValue == false))
                {
                    string CostCenterInsertXML =
                                "<ENVELOPE>" +
                                        "<HEADER>" +
                                        "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
                                        "</HEADER>" +
                                        "<BODY>" +
                                        "<IMPORTDATA>" +
                                        "<REQUESTDESC>" +
                                        "<REPORTNAME>All Masters</REPORTNAME>" +
                                        "</REQUESTDESC>" +
                                        "<REQUESTDATA>" +
                                        "<TALLYMESSAGE xmlns:UDF='TallyUDF'>" +
                                        "<COSTCENTRE NAME='" + ReplaceXML(CostCenter) + "' ACTION='Create'>" +
                                            "<NAME.LIST>" +
                                                "<NAME>" + ReplaceXML(CostCenter) + "</NAME>" +
                                            "</NAME.LIST>" +
                                            "<CATEGORY>" + ReplaceXML(CostCategory) + "</CATEGORY>" +
                                            "</COSTCENTRE>" +
                                        "</TALLYMESSAGE>" +
                                        "</REQUESTDATA>" +
                                        "</IMPORTDATA>" +
                                    "</BODY>" +
                                "</ENVELOPE>";
                    resultarg = ExecuteTallyXML(CostCenterInsertXML);
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in inserting costcenter in Tally Connector, " + err.Message;
            }

            //Attach record details in the error
            if (!resultarg.Success)
            {
                string errormsg = resultarg.Message;
                errormsg += Environment.NewLine + "CostCenter (" + CostCenter + "), Category (" + CostCategory + ")";
                resultarg.Message = errormsg;
            }
            return resultarg;
        }

        /// <summary>
        /// This method is used to insert CC Category
        /// </summary>
        /// <param name="CostCategory"></param>
        /// <returns></returns>
        public ResultArgs InsertCostCategory(string CostCategory)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                string CostCategoryInsertXML =
                                "<ENVELOPE>" +
                                 "<HEADER>" +
                                    "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
                                    "</HEADER>" +
                                    "<BODY>" +
                                    "<IMPORTDATA>" +
                                    "<REQUESTDESC>" +
                                    "<REPORTNAME>All Masters</REPORTNAME>" +
                                    "</REQUESTDESC>" +
                                    "<REQUESTDATA>" +
                                    "<TALLYMESSAGE xmlns:UDF='TallyUDF'>" +
                                    "<COSTCATEGORY NAME='" + ReplaceXML(CostCategory) + "' ACTION='Create'>" +
                                    "<ALLOCATEREVENUE>Yes</ALLOCATEREVENUE>" +
                                    "<ALLOCATENONREVENUE>Yes</ALLOCATENONREVENUE>" +
                                        "<NAME.LIST>" +
                                            "<NAME>" + ReplaceXML(CostCategory) + "</NAME>" +
                                        "</NAME.LIST>" +
                                        "</COSTCATEGORY>" +
                                    "</TALLYMESSAGE>" +
                                    "</REQUESTDATA>" +
                                    "</IMPORTDATA>" +
                                "</BODY>" +
                            "</ENVELOPE>";
                resultarg = ExecuteTallyXML(CostCategoryInsertXML);
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in inserting costcategory in Tally Connector, " + err.Message;
            }

            //Attach record details in the error
            if (!resultarg.Success)
            {
                string errormsg = resultarg.Message;
                errormsg += Environment.NewLine + "Category (" + CostCategory + ")";
                resultarg.Message = errormsg;
            }

            return resultarg;
        }

        /// <summary>
        /// This method is used to Check Master (Ledger Group, Ledger, CC, Voucher TYpe) exists are not
        /// 
        /// if Parent of master is different in tally, return as not exist, it will overwrite in tally
        /// </summary>
        /// <param name="Name">Ledger Group, Ledger, CC</param>
        /// <param name="Parent">Its Parent</param>
        /// <returns>if return value of resultarug is true, master is already exists</returns>
        public ResultArgs IsMasterExists(string Name, string Parent, TallyMasters enmaster)
        {
            ResultArgs resultarg = new ResultArgs();
            try
            {
                string TallyObject = string.Empty;
                string TallyMasterTable = string.Empty;
                string TallyParentTable = string.Empty;
                switch (enmaster)
                {
                    case TallyMasters.Ledgers:
                        {
                            TallyObject = "Ledger";
                            TallyMasterTable = TallyObject;
                            TallyParentTable = "Parent";
                            break;
                        }
                    case TallyMasters.Groups:
                        {
                            TallyObject = "Groups";
                            TallyMasterTable = "Group";
                            TallyParentTable = "Parent";
                            break;
                        }
                    case TallyMasters.CostCenters:
                        {
                            TallyObject = "CostCentre";
                            TallyMasterTable = TallyObject;
                            TallyParentTable = "Category";
                            break;
                        }
                    case TallyMasters.VoucherType:
                        {
                            TallyObject = "VoucherType";
                            TallyMasterTable = TallyObject;
                            TallyParentTable = "Parent";
                            break;
                        }

                }

                string IsMasterExists = "<ENVELOPE>" +
                                         "<HEADER>" +
                                           "<VERSION>1</VERSION>" +
                                           "<TALLYREQUEST>EXPORT</TALLYREQUEST>" +
                                            "<TYPE>OBJECT</TYPE>" +
                                            "<SUBTYPE>" + TallyObject + "</SUBTYPE>" +
                                            "<ID TYPE='Name'>" + ReplaceXML(Name) + "</ID>" +
                                         "</HEADER>" +
                                         "<BODY>" +
                                         "<DESC>" +
                                         "<STATICVARIABLES>" +
                                             "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                                         "</STATICVARIABLES>" +
                                                "<FETCHLIST>" +
                                                    "<FETCH>Name</FETCH>" +
                                                    "<FETCH>Parent</FETCH>" +
                                                "</FETCHLIST>" +
                                             "</DESC>" +
                                         "</BODY>" +
                                     "</ENVELOPE>";
                resultarg = FetchTallyXML(IsMasterExists);
                resultarg.ReturnValue = true;

                if (resultarg.Success)
                {
                    //Check Masters's parent in tally, if parent is differed in tally, update in tally
                    DataSet TallyResponseDataSet = resultarg.DataSource.TableSet;
                    if (TallyResponseDataSet != null)
                    {
                        //Name of the master
                        string name = string.Empty;
                        if (TallyResponseDataSet.Tables[TallyMasterTable] != null)
                        {
                            if (TallyResponseDataSet.Tables[TallyMasterTable].Rows.Count > 0 && TallyResponseDataSet.Tables[TallyMasterTable].Columns.Contains("NAME"))
                            {
                                name = TallyResponseDataSet.Tables[TallyMasterTable].Rows[0]["NAME"].ToString();
                            }

                        }

                        //Parent of the master
                        string parent = string.Empty;
                        if (TallyResponseDataSet.Tables[TallyParentTable] != null)
                        {
                            if (TallyResponseDataSet.Tables[TallyParentTable].Rows.Count > 0)
                            {
                                parent = TallyResponseDataSet.Tables[TallyParentTable].Rows[0][TallyParentTable + "_text"].ToString();
                            }
                        }

                        //Check Masters's parent in tally, if parent is differed in tally, update in tally
                        if (name == Name && parent != Parent)
                        {
                            resultarg.Success = true;
                            resultarg.ReturnValue = false;
                        }
                        else if (string.IsNullOrEmpty(name)) //For Capital Fund ledger group
                        {
                            resultarg.Success = true;
                            resultarg.ReturnValue = false;
                        }
                    }
                }
                else if (!resultarg.Success && resultarg.Message.ToUpper().Contains("COULD NOT FIND " + TallyObject.ToUpper() + ":"))
                {
                    resultarg.Success = true;
                    resultarg.ReturnValue = false;
                }
            }
            catch (Exception err)
            {
                resultarg.Message = "Problem in checking master item in Tally Connector, " + err.Message;
            }
            return resultarg;
        }

        /// <summary>
        /// On 11/12/2017, In Tally, they treat Bank OD Ledgers as bank Accounts
        ///         /// in Acme.erp, we treat Bank OD ledgers as liability ledgers
        /// 
        /// For Receipt/Payments, If Both (CR, DR) ledgers are Bank/Cash/Bank OD, we change voucher type from Receipt/Payment to Contra
        /// else we treat as Journal, otherwise it will given vouchertype
        /// 
        /// This method is used to change voucher type based on its child voucher details
        /// </summary>
        private ResultArgs GetBankODLedgerVoucherType(DefaultVoucherTypes VoucherType, DataTable dtCrs, DataTable dtDrs)
        {
            ResultArgs resultarg = new ResultArgs();
            resultarg.ReturnValue = null;
            resultarg.RowsAffected = 0;
            DefaultVoucherTypes newVtype = VoucherType;
            Int32 IsAffectedRows = 0;
            try
            {
                dtCrs.DefaultView.RowFilter = AppSchema.LedgerGroup.LEDGER_GROUPColumn.Caption + " IN ('" + GRP_BANK_ACCOUNTS + "','" + GRP_BANK_OD_AC + "','" + GRP_CASH_IN_HAND + "')";
                dtDrs.DefaultView.RowFilter = AppSchema.LedgerGroup.LEDGER_GROUPColumn.Caption + " IN ('" + GRP_BANK_ACCOUNTS + "','" + GRP_BANK_OD_AC + "','" + GRP_CASH_IN_HAND + "')";
                if (dtCrs.DefaultView.Count > 0 && dtDrs.DefaultView.Count > 0)
                {
                    if (VoucherType == DefaultVoucherTypes.Receipt || VoucherType == DefaultVoucherTypes.Payment)
                    {
                        newVtype = DefaultVoucherTypes.Contra;
                        IsAffectedRows = 1;
                    }
                    else if (VoucherType == DefaultVoucherTypes.Journal)
                    {
                        newVtype = DefaultVoucherTypes.Journal;
                        IsAffectedRows = 1; 
                    }
                }
                dtCrs.DefaultView.RowFilter = string.Empty;
                dtDrs.DefaultView.RowFilter = string.Empty;

                resultarg.ReturnValue = newVtype;
                resultarg.RowsAffected = IsAffectedRows;
                resultarg.Success = true;
            }
            catch (Exception err)
            {
                dtCrs.DefaultView.RowFilter = string.Empty;
                dtDrs.DefaultView.RowFilter = string.Empty;
                resultarg.ReturnValue = VoucherType;
                resultarg.Message = "Problem in checking Bank OD ledgers in Vouchsers in Tally Connector, " + err.Message;
            }
            return resultarg;
        }
        #endregion
    }
}

//On 28/10/2017, Get opening for given date
//private ResultArgs FetchLedgerClosingBalance(string balancedate)
//       {
//           ResultArgs resultarg = new ResultArgs();
//           DataSet TallyResponseDataSet = new DataSet();
//           DataTable LedgerCLBalance = new dsTallyConnector.LEDGERCLOSINGBALANCEDataTable();

//           string xml = "<ENVELOPE>"
//                     + "<HEADER>"
//                       + "<VERSION>1</VERSION>"
//                       + "<TALLYREQUEST>Export</TALLYREQUEST>"
//                       + "<TYPE>Data</TYPE>"
//                       + "<ID>Simple Trial balance</ID>"
//                     + "</HEADER>"
//                     + "<BODY>"
//                       + "<DESC>"
//                         + "<STATICVARIABLES>"
//                           + "<EXPLODEFLAG>Yes</EXPLODEFLAG>"
//                           + "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>"
//                           + "<SVFROMDATE TYPE=\"Date\">" + balancedate + "</SVFROMDATE>"
//                           + "<SVTODATE TYPE=\"Date\">" + balancedate + "</SVTODATE>"
//                         + "</STATICVARIABLES>"
//                         + "<TDL>"
//                           + "<TDLMESSAGE>"
//                             + "<REPORT NAME=\"Simple Trial balance\">"
//                               + "<FORMS>Simple Trial balance</FORMS>"
//                               + "<TITLE>\"Trial Balance\"</TITLE>"
//                             + "</REPORT>"
//                             + "<FORM NAME=\"Simple Trial balance\">"
//                               + "<TOPPARTS>Simple TB Part</TOPPARTS>"
//                             + "</FORM>"
//                             + "<PART NAME=\"Simple TB Part\">"
//                               + "<TOPLINES>Simple TB Details</TOPLINES>"
//                               + "<REPEAT>Simple TB Details : Simple TB Ledgers</REPEAT>"
//                               + "<SCROLLED>Vertical</SCROLLED>"
//                               + "<COMMONBORDERS>Yes</COMMONBORDERS>"
//                             + "</PART>"
//                             + "<LINE NAME=\"Simple TB Details\">"
//                               + "<FIELDS>Name</FIELDS>"
//                               + "<Fields>OpeningBalance</Fields>" //ClosingBalance
//                             + "</LINE>"
//                             + "<FIELD NAME=\"Name\">"
//                               + "<USE>Name Field</USE>"
//                               + "<SET>$Name</SET>"
//                             + "</FIELD>"
//                             + "<FIELD NAME=\"OpeningBalance\">" //CLOSINGBALANCE
//                               + "<USE>Amount Field</USE>"
//                               + "<SET>$OpeningBalance</SET>" //ClosingBalance
//                             + "</FIELD>"
//                             + "<COLLECTION NAME=\"Simple TB Ledgers\">"
//                               + "<TYPE>Ledger</TYPE>"
//                               + "<SORT>Default : $Name</SORT>"
//                             + "</COLLECTION>"
//                           + "</TDLMESSAGE>"
//                         + "</TDL>"
//                       + "</DESC>"
//                     + "</BODY>"
//                   + "</ENVELOPE>";
//           try
//           {
//               resultarg = ExecuteTallyXML(xml, TallyMasters.Groups);
//               if (resultarg.Success)
//               {
//                   TallyResponseDataSet = resultarg.DataSource.TableSet;
//                   if (TallyResponseDataSet != null)
//                   {
//                       if (TallyResponseDataSet.Tables["NAME"] != null && TallyResponseDataSet.Tables["OPENINGBALANCE"] != null) //CLOSINGBALANCE
//                       {
//                           DataTable dtLedgerName = TallyResponseDataSet.Tables["NAME"];
//                           DataTable dtClosingBalance = TallyResponseDataSet.Tables["OPENINGBALANCE"]; //CLOSINGBALANCE

//                           if (!dtClosingBalance.Columns.Contains("OPENINGBALANCE_TEXT")) //CLOSINGBALANCE_TEXT
//                           {
//                               dtClosingBalance.Columns.Add("OPENINGBALANCE_TEXT", typeof(System.Double)); //CLOSINGBALANCE_TEXT
//                           }
//                           dtLedgerName.Columns[0].ColumnMapping = MappingType.Attribute;
//                           dtClosingBalance.Columns[0].ColumnMapping = MappingType.Attribute;

//                           var resLedgerClosingBalance = from drLedgerName in dtLedgerName.AsEnumerable()
//                                                         join drLedgerClosingBalance in dtClosingBalance.AsEnumerable()
//                                                             on dtLedgerName.Rows.IndexOf(drLedgerName) equals
//                                                             dtClosingBalance.Rows.IndexOf(drLedgerClosingBalance)
//                                                         select LedgerCLBalance.LoadDataRow(new object[]
//                                       {
//                                           drLedgerName.Field<string>("NAME_TEXT"),
//                                           drLedgerClosingBalance.Field<string>("OPENINGBALANCE_TEXT"), //CLOSINGBALANCE_TEXT
//                                       }, false);
//                           LedgerCLBalance = resLedgerClosingBalance.CopyToDataTable();
//                           resultarg.Success = true;
//                       }
//                   }
//               }
//           }
//           catch (Exception ex)
//           {
//               //MessageBox.Show(ex.Message, ex.StackTrace);
//               string msg = ex.Message;
//               if (msg.Contains("Unable to connect to the remote server"))
//               {
//                   resultarg.Message = MSG_TALLY_NOT_RUNNING;
//               }
//               else
//               {
//                   resultarg.Message = ex.Message;
//               }

//           }
//           finally
//           {
//               resultarg.DataSource.Data = LedgerCLBalance;
//           }

//           return resultarg;

//       }

///// <summary>
//       /// To Retriew groups from tally 
//       /// 1. Get all the groups from tally odbc
//       /// 2. Map groups which are attached with primary (Nature of Groups)
//       /// (Assets, Liabilities, Incomes, Expenses)
//       /// </summary>
//       /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
//       public ResultArgs FetchGroups1()
//       {
//           ResultArgs resultarg = new ResultArgs();
//           DataTable dtTallyGroups = new dsTallyConnector.GROUPDataTable();

//           //Retrivew groups list from Tally ODBC SQL
//           resultarg = ExecuteTally("SELECT $Name, $Parent,$_PrimaryGroup,$_GrandParent FROM groups");

//           if (resultarg.Success)
//           {
//               DataTable dtGroups = resultarg.DataSource.Table;
//               DataTable dtprimaryGroups = GetTallyPrimaryGroups();
//               try
//               {
//                   // Primary
//                   var result = from drGroups in dtGroups.AsEnumerable()
//                                join drprimaryGroups in dtprimaryGroups.AsEnumerable()
//                                   on drGroups.Field<string>("$Name") equals drprimaryGroups.Field<string>("PrimaryGroup")
//                                   into primaryjoin
//                                from drprimaryGroups in primaryjoin.DefaultIfEmpty()
//                                select dtTallyGroups.LoadDataRow(new object[]
//                               {
//                                   drGroups.Field<string>("$Name"),
//                                   drGroups.Field<string>("$Parent"),
//                                   (drprimaryGroups==null?string.Empty:drprimaryGroups.Field<string>("Nature")),
//                                     drGroups.Field<string>("$_PrimaryGroup"),
//                                  drGroups.Field<string>("$_GrandParent"),
//                               }, false);
//                   dtTallyGroups = result.CopyToDataTable();
//                   dtTallyGroups.DefaultView.Sort = "Parent";
//                   resultarg.Success = true;
//                   //-----------------------------------------------------------------------
//               }
//               catch (Exception ex)
//               {
//                   string msg = ex.Message;
//                   if (msg.Contains("Unable to connect to the remote server"))
//                   {
//                       resultarg.Message = MSG_TALLY_NOT_RUNNING;
//                   }
//                   else
//                   {
//                       resultarg.Message = ex.Message;
//                   }

//               }
//               finally
//               {
//                   resultarg.DataSource.Data = dtTallyGroups.DefaultView.Table;
//               }
//           }
//           return resultarg;
//       }

///// <summary>
//        /// Get Current/Active company information 
//        /// </summary>
//        /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
//        public ResultArgs FetchCompanyInfo()
//        {
//            ResultArgs resultarg = new ResultArgs();
//            resultarg = IsTallyConnected;
//            if (resultarg.Success)
//            {
//                resultarg = FetchCurrentCompanyDetails();
//                string connectionstring = resultarg.ReturnValue.ToString();
//                resultarg = CurrentCompnayName;
//                if (resultarg.Success)
//                {
//                    if (resultarg.ReturnValue != null)
//                    {

//                        string currentCompanyname = resultarg.ReturnValue.ToString();
//                        string query = "SELECT $NAME, $StartingFrom, $BooksFrom, " +
//                                       "$_Address1,$_Address2, $_Address3, $_Address4, $_Address5" +
//                                       " FROM company";

//                        query = "SELECT $NAME, $StartingFrom, $BooksFrom FROM company";

//                        try
//                        {
//                            if (resultarg.Success)
//                            {
//                                resultarg = ExecuteTally(query);
//                                if (resultarg.Success)
//                                {
//                                    DataTable dtODBCcompany = resultarg.DataSource.Table;
//                                    dtODBCcompany.DefaultView.RowFilter = string.Format("$NAME='{0}'", currentCompanyname.Replace(@"'", @"''"));
//                                    DataTable dtCompnayInfo = dtODBCcompany.DefaultView.ToTable();
//                                    dtCompnayInfo.TableName = "Company";

//                                    //Add one column in company info table Is DONORS Moulde enabled or not---------------------------------------------------------------------------
//                                    if (dtODBCcompany.Rows.Count > 0)
//                                    {
//                                        DataColumn dcdonorenabled = new DataColumn("IsDonorModuleEnabled", typeof(Boolean));
//                                        dtCompnayInfo.Columns.Add(dcdonorenabled);
//                                        dtCompnayInfo.Rows[0][dcdonorenabled] = IsDonorModuleEnabled;
//                                    }
//                                    //---------------------------------------------------------------------------

//                                    resultarg.DataSource.Data = dtCompnayInfo;
//                                    resultarg.Success = true;
//                                }
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            if (ex.Message.Contains("Data source name not found"))
//                            {
//                                resultarg.Message = MSG_TALLY_NOT_RUNNING;
//                            }
//                            else
//                            {
//                                resultarg.Message = ex.Message;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        resultarg.Message = MSG_TALLY_NOT_RUNNING;
//                    }
//                }
//            }
//            return resultarg;
//        }


///// <summary>
//       /// Execute sql in tally odbc
//       /// </summary>
//       /// <param name="query">sql query to be executed in tally odbc</param>
//       /// <returns>ResultArgs with datatable and its result sucess or failure</returns>
//       private ResultArgs ExecuteTally(string query)
//       {
//           DataTable dt = new DataTable();
//           ResultArgs resultarg = new ResultArgs();

//           try
//           {
//               resultarg = IsTallyConnected;
//               if (resultarg.Success)
//               {
//                   using (OdbcConnection con = new OdbcConnection(resultarg.ReturnValue.ToString()))
//                   {
//                       using (OdbcCommand cmd = new OdbcCommand(query, con))
//                       {
//                           con.Open();
//                           using (OdbcDataAdapter da = new OdbcDataAdapter(query, con))
//                           {
//                               da.Fill(dt);
//                           }
//                           //if (con != null && con.State == ConnectionState.Open) con.Close();
//                           resultarg.DataSource.Data = dt;
//                           resultarg.Success = true;
//                       }
//                   }
//               }
//               else
//               {
//                   resultarg.Message = "Tally.ERP ODBC is not exists";
//               }
//           }
//           catch (Exception ex)
//           {
//               if (ex.Message.Contains("Data source name not found"))
//               {
//                   resultarg.Message = MSG_TALLY_NOT_RUNNING;
//               }
//               else
//               {
//                   resultarg.Message = ex.Message;
//               }
//           }
//           return resultarg;
//       }

///// <summary>
//       /// Form tally odbc connection string with help of tally odbc server and its port
//       /// </summary>
//       private ResultArgs GetTallyConnectionString
//       {
//           get
//           {
//               ResultArgs resultarg = new ResultArgs();
//               string tallyConnectionString = string.Empty;
//               string tallyserver = GetTallyServer;
//               string tallyport = GetTallyPort;

//               if (tallyserver != string.Empty && tallyport != string.Empty)
//               {
//                   //Based on Applcation Targetplatform
//                   if (IntPtr.Size == 8)
//                   {
//                       tallyConnectionString = "PORT=" + tallyport + ";DRIVER=Tally ODBC Driver64;SERVER=" + tallyserver;
//                   }
//                   else
//                   {
//                       tallyConnectionString = "PORT=" + tallyport + ";DRIVER=Tally ODBC Driver;SERVER=" + tallyserver;
//                   }

//                   //tallyConnectionString = "PORT=" + tallyport + ";DRIVER=Tally ODBC Driver64;SERVER=" + tallyserver;
//                   resultarg.ReturnValue = tallyConnectionString;
//                   resultarg.Success = true;
//               }

//               if (tallyConnectionString == string.Empty)
//               {
//                   resultarg.Message = "TallyConnection is not available in Config";
//               }
//               return resultarg;
//           }
//       }
//xml = "<ENVELOPE>"
//               + "<HEADER>"
//               + "<VERSION>1</VERSION>"
//               + "<TALLYREQUEST>Export</TALLYREQUEST>"
//               + "<TYPE>Data</TYPE>"
//               + "<ID>List of Ledgers</ID>"
//               + "</HEADER>"
//               + "<BODY>"
//    //+ "<DESC>"
//    //    + "<STATICVARIABLES>"
//    //        + "<SVFROMDATE TYPE=\"Date\">" + fromdate + "</SVFROMDATE>"
//    //        + "<SVTODATE TYPE=\"Date\">" + todate + "</SVTODATE>"
//    //        + "<EXPLODEFLAG>Yes</EXPLODEFLAG>"
//    //        + "<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>"
//    //    + "</STATICVARIABLES>"
//    //+ "</DESC>"
//               + "</BODY>"
//               + "</ENVELOPE>";