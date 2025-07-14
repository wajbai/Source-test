using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Model.Inventory;
using Bosco.DAO.Schema;
using Bosco.Model.Dsync;
using Bosco.Model.Donor;
using System.Windows.Forms;


namespace Bosco.Model.UIModel.Master
{
    public class ExcelSupport : SystemBase
    {
        #region VariableDeclaration
        FormMode FrmMode;
        public AssetStockProduct.IGroup Igroup = null;
        ResultArgs resultArgs = new ResultArgs();
        string CONTACT_NUMBER = "CONTACT NUMBER";
        string PAN_NUMBER = "PAN NUMBER";
        string MANUFACTURER_NAME = "MANUFACTURER NAME";
        string VENDOR_NAME = "VENDOR NAME";
        string Custodian = "CUSTODIAN NAME";
        //string Role = "ROLE";
        string START_DATE = "START DATE";
        string END_DATE = "END DATE";
        string FORMAL_NAME = "FORMAL NAME";
        string DECIMAL_PLACE = "DECIMAL PLACE";
        string PARENT_LOCATION_NAME = "PARENT LOCATION NAME";
        string LOCATION_NAME = "LOCATION NAME";
        string LOCATION_TYPE = "LOCATION TYPE";
        string PROJECT = "PROJECT";
        string CUSTODIAN_OR_OWNER = "CUSTODIAN OR OWNER";
        string MODE = "MODE";

        // Asset Item
        string STARTING_NUMBER = "STARTING NO";
        string PARENT_GROUP_NAME = "PARENT GROUP NAME";
        string ASSET_CLASS = "ASSET CLASS";
        string RETENTION_YRS = "RETENTION YRS";
        string DEPRECIATION_YRS = "DEPRECIATION YRS";
        string IS_INSURED = "IS INSURED";
        string IS_AMC = "IS AMC";
        string ASSET_ITEM = "ASSET ITEM";
        string ACCOUNT_LEDGER = "ACCOUNT LEDGER";
        string DEPRECIATION_LEDGER = "DEPRECIATION LEDGER";
        string DISPOSAL_LEDGER = "DISPOSAL LEDGER";
        string METHOD = "METHOD";
        string PREFIX = "PREFIX";
        string SUFFIX = "SUFFIX";
        string QUANTITY = "QUANTITY";
        string AMOUNT = "AMOUNT";

        int LocationID = 0;
        int BlockID = 0;
        int ParentLocationId = 0;
        int ClassID = 0;
        int CustodianID = 0;
        int VendorID = 0;
        int ManufactureID = 0;
        int UnitID = 0;
        int ParentGroupID = 0;
        int ProjectID = 0;
        int ItemID = 0;
        int Quantity = 0;
        double Amount = 0;
        public int RetentionYrs = 0;
        public int DepreciationYrs = 0;
        public int AMCApplicable = 0;
        public int InsuranceApplicable = 0;

        public string Name = string.Empty;
        public string Locationtype = string.Empty;
        public string Role = string.Empty;
        string UnitName = string.Empty;
        public string ItemName = string.Empty;
        public string CustudianName = string.Empty;
        public string Description = string.Empty;

        public string ProjectName = string.Empty;
        public string VendarName = string.Empty;
        public string ManufactureName = string.Empty;
        public string LocationName = string.Empty;
        public string AssetClass = string.Empty;
        public string ParentClass = string.Empty;
        string LedgerName = string.Empty;
        public string Prefix = string.Empty;
        public string Suffix = string.Empty;
        public string BlockName = string.Empty;

        public bool CanOverwrite = false;
        public string DateFrom = "";
        public string DateTo = "";
        #endregion

        #region Constructor

        #endregion

        #region Properties
        public DataTable dtDonor { set; get; }
        public DataTable dtProspects { get; set; }
        public DataTable dtGroup { get; set; }
        public DataTable dtCustodian { get; set; }
        public DataTable DtDetails { get; set; }
        public string ErrorMsg { set; get; }
        public FinanceModule module { get; set; }
        public DataTable dtItems { get; set; }
        public DataTable dtVendor { get; set; }
        public DataTable dtLocation { get; set; }
        public DataTable dtOPBalance { get; set; }
        #endregion

        #region Methods
        public ResultArgs ImportDonor()
        {
            resultArgs = DonorImport();
            return resultArgs;
        }

        public ResultArgs ImportDonorTransaction()
        {
            resultArgs = DonorTransactionImport();
            return resultArgs;
        }

        /// <summary>
        /// to get prospects details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportProspects()
        {
            resultArgs = DonorProspects();
            return resultArgs;
        }
        /// <summary>
        /// Import Excel Template Donor 
        /// </summary>
        /// <returns></returns>
        private ResultArgs DonorImport()
        {

            using (DataManager dataManager = new DataManager())
            {
                try
                {
                    int CountryId = 0;
                    string MismatchedNames = string.Empty;
                    if (dtDonor != null)
                    {
                        dataManager.BeginTransaction();
                        IEnumerable<DataRow> EnumurableDonor = dtDonor.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["FIRST NAME"].ToString())
                            && string.IsNullOrEmpty(row["ADDRESS"].ToString()) ? false : true);

                        if (EnumurableDonor.Count() > 0)
                        {
                            dtDonor = EnumurableDonor.CopyToDataTable();
                            // DataView dvDonor = dtDonor.DefaultView;
                            //  dvDonor.RowFilter = "FLAG='d'";
                            if (dtDonor.Rows.Count > 0)
                            {
                                foreach (DataRow drItem in dtDonor.Rows)
                                {
                                    using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
                                    {
                                        using (CountrySystem CountrySystem = new CountrySystem())
                                        {
                                            donorSystem.Name = drItem["FIRST NAME"].ToString().Trim();
                                            donorSystem.LastName = drItem["LAST NAME"].ToString().Trim();
                                            donorSystem.Type = drItem["TYPE"].ToString().ToLower().Trim().Equals("personal") ? 2 : 1;
                                            donorSystem.Place = drItem["CITY"].ToString();
                                            donorSystem.Pincode = drItem["PINCODE"].ToString();
                                            donorSystem.Phone = drItem["MOBILE"].ToString();
                                            donorSystem.Telephone = drItem["TELEPHONE"].ToString();
                                            donorSystem.Fax = drItem["FAX"].ToString();
                                            donorSystem.Email = drItem["EMAIL"].ToString();
                                            donorSystem.URL = drItem["URL"].ToString();
                                            donorSystem.Notes = drItem["NOTES"].ToString();
                                            donorSystem.Address = drItem["ADDRESS"].ToString();
                                            donorSystem.PAN = drItem["PAN"].ToString();
                                            donorSystem.IdentityKey = 0;//0-Donor and 1-Auditor 
                                            CountryId = (!string.IsNullOrEmpty(drItem["COUNTRY"].ToString())) ? CountrySystem.GetCountryId(drItem["COUNTRY"].ToString()) : 1;
                                            donorSystem.CountryId = CountryId;
                                            donorSystem.StateId = GetStateId(drItem["STATE"].ToString());
                                            donorSystem.Notes = string.Empty;

                                            //Additional Info
                                            string InstitutionalType = drItem["INSTITUTION TYPE"].ToString();
                                            int InstitutionalTypeId = donorSystem.FetchDonorInsttitutionalTypeByName(InstitutionalType);
                                            donorSystem.InstitutionalType = InstitutionalTypeId != 0 ? InstitutionalTypeId : 0;
                                            donorSystem.RegNo = drItem["REGISTER NO"].ToString();
                                            //string PaymentMode = drItem["PAYMENT MODE"].ToString();
                                            //int PaymentModeID = donorSystem.FetchDonorPaymentModeId(PaymentMode);
                                            //donorSystem.PaymentMode = PaymentModeID != 0 ? PaymentModeID : 0;

                                            string RegistrationType = drItem["REGISTRATION TYPE"].ToString();

                                            int RegTypeId = donorSystem.FetchDonorRegTypeId(RegistrationType);
                                            donorSystem.RegType = RegTypeId != 0 ? RegTypeId : 0;

                                            donorSystem.MaritalStatus = (drItem["MARITAL STATUS"].ToString().Equals(MaritalStatus.Single.ToString())) ? 0 : 1;
                                            if (!string.IsNullOrEmpty(drItem["ANNIVERSARY DATE"].ToString()))
                                            {
                                                donorSystem.Anniversarydate = this.DateSet.ToDate(drItem["ANNIVERSARY DATE"].ToString(), false);
                                            }
                                            donorSystem.Organizationemployed = drItem["ORGANIZATION EMPLOYEED"].ToString();
                                            donorSystem.Language = drItem["LANGUAGE"].ToString();
                                            donorSystem.Religion = drItem["RELIGION"].ToString();
                                            donorSystem.Occupation = drItem["OCCUPATION"].ToString();
                                            donorSystem.Gender = (drItem["GENDER"].ToString().Equals(Gender.Male.ToString())) ? 0 : 1;
                                            if (!string.IsNullOrEmpty(drItem["DOB"].ToString()))
                                            {
                                                donorSystem.DOB = this.DateSet.ToDate(drItem["DOB"].ToString(), false);
                                            }

                                            donorSystem.ReferedStaff = drItem["REFERRED STAFF"].ToString();
                                            donorSystem.Title = drItem["TITLE"].ToString();
                                            donorSystem.Isactive = 1;

                                            if (CountryId == 0)
                                            {
                                                donorSystem.CountryId = 1; //India
                                            }

                                            if (donorSystem.Type == 0)
                                            {
                                                //Prospect Type  is empty
                                                continue;
                                            }
                                            if (string.IsNullOrEmpty(donorSystem.Name))
                                            {
                                                //Prospect Name is Empty
                                                continue;
                                            }
                                            else
                                            {
                                                donorSystem.DonAudId = 0;
                                                resultArgs = GetDonorId(donorSystem.Name, donorSystem.Address, donorSystem.Place);
                                                if (resultArgs.Success)
                                                {
                                                    if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                                    {
                                                        donorSystem.DonAudId = resultArgs.DataSource.Sclar.ToInteger;
                                                        MismatchedNames += donorSystem.Name + Environment.NewLine;
                                                    }
                                                    resultArgs = donorSystem.SaveDonorInfo(dataManager);
                                                    if (!resultArgs.Success)
                                                    {
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
                            // dvDonor.RowFilter = "";
                        }
                        else
                        {
                            resultArgs.Message = "Donor list is empty to import";
                        }
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
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        private ResultArgs DonorTransactionImport()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    using (DonorManagementSystem donorSystem = new DonorManagementSystem())
                    {
                        IEnumerable<DataRow> EnumurableTransaction = dtDonor.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["PROJECT"].ToString())
                                 && string.IsNullOrEmpty(row["VOUCHER_DATE"].ToString()) ? false : true);

                        if (EnumurableTransaction.Count() > 0)
                        {
                            dtDonor = EnumurableTransaction.CopyToDataTable();
                            donorSystem.dtTransaction = dtDonor;
                            donorSystem.canOverwrite = CanOverwrite;
                            donorSystem.DateFrom = DateFrom;
                            donorSystem.DateTo = DateTo;
                            resultArgs = donorSystem.ImportDonorTransactions(dataManager);
                        }
                        else
                        {
                            resultArgs.Message = "Transactions list empty to import";
                        }

                        if (!resultArgs.Success)
                        {
                            dataManager.TransExecutionMode = ExecutionMode.Fail;
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "File is Invalid";
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Excel Template Prospects 
        /// </summary>
        /// <returns></returns>
        private ResultArgs DonorProspects()
        {
            try
            {
                int CountryId = 0;
                string MismatchedNames = string.Empty;
                int NameCount = 0;
                int IdGreater = 0;
                if (dtProspects != null)
                {
                    using (DataManager dataManager = new DataManager())
                    {
                        dataManager.BeginTransaction();
                        IEnumerable<DataRow> EnumurableProspects = dtProspects.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["NAME"].ToString()) ? false : true);

                        //dtProspects = dtProspects.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        if (EnumurableProspects.Count() > 0)
                        {
                            dtProspects = EnumurableProspects.CopyToDataTable();
                            foreach (DataRow drItem in dtProspects.Rows)
                            {
                                using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
                                {
                                    using (CountrySystem CountrySystem = new CountrySystem())
                                    {
                                        prospectSystem.Name = drItem["NAME"].ToString().Trim();
                                        prospectSystem.RegNo = drItem["REGISTER NO"].ToString().Trim();
                                        prospectSystem.LastName = drItem["LASTNAME"].ToString().Trim();
                                        prospectSystem.Type = drItem["TYPE"].ToString().ToLower().Trim().Equals("personal") ? 2 : 1;  //(string.IsNullOrEmpty(drItem["TYPE"].ToString())) ? 1 : (drItem["DONOR TYPE"].ToString().ToLower().Equals("institutional") ? 1 : 2);
                                        if (!string.IsNullOrEmpty(drItem["DOB"].ToString()))
                                        {
                                            prospectSystem.DOB = this.DateSet.ToDate(drItem["DOB"].ToString(), false);
                                        }

                                        prospectSystem.InstitutionalTypeId = GetInstitionalTypeId(drItem["INSTITUTION_TYPE"].ToString()) != 0 ? GetInstitionalTypeId(drItem["INSTITUTION_TYPE"].ToString()) : 0;

                                        string RegistrationType = drItem["REGISTRATION_TYPE"].ToString();

                                        int RegTypeId = GetRegistrationTypeId(RegistrationType);
                                        prospectSystem.RegistrationTypeId = RegTypeId != 0 ? RegTypeId : 0;

                                        // prospectSystem.RegistrationTypeId = GetRegistrationTypeId(drItem["REGISTRATION_TYPE"].ToString()) != 0 ? GetRegistrationTypeId(drItem["REGISTRATION_TYPE"].ToString()) : 0;
                                        prospectSystem.Religion = drItem["RELIGION"].ToString();
                                        prospectSystem.Language = drItem["LANGUAGE"].ToString();
                                        prospectSystem.ReferredStaff = drItem["Refer Staff/File"].ToString();
                                        prospectSystem.ReferenceNumber = drItem["REFERENCE_NUMBER"].ToString();
                                        prospectSystem.SourceInformation = drItem["SOURCE_INFORMATION"].ToString();
                                        CountryId = (!string.IsNullOrEmpty(drItem["COUNTRY"].ToString())) ? CountrySystem.GetCountryId(drItem["COUNTRY"].ToString()) : 1;
                                        prospectSystem.CountryId = CountryId;
                                        prospectSystem.StateId = GetStateId(drItem["STATE"].ToString()) != 0 ? GetStateId(drItem["STATE"].ToString()) : 0;
                                        prospectSystem.Place = drItem["PLACE"].ToString();
                                        prospectSystem.Pincode = drItem["PINCODE"].ToString();
                                        prospectSystem.Address = drItem["ADDRESS"].ToString();
                                        prospectSystem.Phone = drItem["MOBILE"].ToString();
                                        prospectSystem.Telephone = drItem["TELEPHONE"].ToString();
                                        prospectSystem.Fax = drItem["FAX"].ToString();
                                        prospectSystem.Email = drItem["EMAIL"].ToString();
                                        prospectSystem.URL = drItem["URL"].ToString();
                                        prospectSystem.PAN = drItem["PAN"].ToString();
                                        prospectSystem.Notes = drItem["NOTES"].ToString();

                                        if (CountryId == 0)
                                        {
                                            prospectSystem.CountryId = 1; //India
                                        }

                                        //if (prospectSystem.Type == 0)
                                        //{
                                        //    //Prospect Type  is empty
                                        //    continue;
                                        //}
                                        //if (string.IsNullOrEmpty(prospectSystem.Address))
                                        //{
                                        //    //Address is Empty
                                        //    continue;
                                        //}
                                        if (string.IsNullOrEmpty(prospectSystem.Name))
                                        {
                                            //Prospect Name is Empty
                                            NameCount = NameCount + 1;
                                            continue;
                                        }
                                        else
                                        {
                                            prospectSystem.ProspectId = 0;
                                            resultArgs = prospectSystem.GetProspectId();
                                            if (resultArgs.Success)
                                            {
                                                if (resultArgs.DataSource.Sclar.ToInteger > 0)
                                                {
                                                    prospectSystem.ProspectId = resultArgs.DataSource.Sclar.ToInteger;
                                                    IdGreater = IdGreater + 1;
                                                }
                                                resultArgs = prospectSystem.SaveDonorProspectInfo(dataManager);
                                            }
                                        }
                                    }
                                }

                                if (!resultArgs.Success)
                                {
                                    resultArgs.Message = "Problem in importing Prospects. " + resultArgs.Message;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Prospect list is empty to import";
                        }

                        if (!resultArgs.Success)
                        {
                            dataManager.TransExecutionMode = ExecutionMode.Fail;
                        }
                        dataManager.EndTransaction();
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
        /// Get Donor ID
        /// </summary>
        /// <param name="donorName"></param>
        /// <returns></returns>
        private ResultArgs GetDonorId(string donorName, string address, string place)
        {
            using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
            {
                donorSystem.Name = donorName;
                donorSystem.Place = place;
                donorSystem.Address = address;
                resultArgs = donorSystem.GetDonorId();
            }
            return resultArgs;
        }

        /// <summary>
        /// Get State ID
        /// </summary>
        /// <param name="StateName"></param>
        /// <returns></returns>
        private int GetStateId(string StateName)
        {
            using (StateSystem stateSystem = new StateSystem())
            {
                stateSystem.StateName = StateName;
                resultArgs = stateSystem.GetStateId();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Get Reg ID
        /// </summary>
        /// <param name="StateName"></param>
        /// <returns></returns>
        private int GetRegistrationTypeId(string RegistrationType)
        {
            //int REGISTERTYPEID = 0;
            using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
            {
                prospectSystem.RegistrationType = RegistrationType;
                resultArgs = prospectSystem.GetRegistrationTypeId();
                // REGISTERTYPEID = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["REGISTRATION_TYPE_ID"].ToString());
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Get Ins Type ID
        /// </summary>
        /// <param name="StateName"></param>
        /// <returns></returns>
        private int GetInstitionalTypeId(string InstitutionType)
        {
            using (ProspectManagementSystem prospectSystem = new ProspectManagementSystem())
            {
                prospectSystem.InstitutionalType = InstitutionType;
                resultArgs = prospectSystem.GetInsTypeId();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Import Excel Template Import Class Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportGroupDetails()
        {
            try
            {
                if (dtGroup != null && dtGroup.Rows.Count > 0)
                {
                    if (dtGroup.Columns.Contains(ASSET_CLASS) && dtGroup.Columns.Contains(PARENT_GROUP_NAME))
                    {
                        if (module.Equals(FinanceModule.Asset))
                        {
                            if (!dtGroup.Columns.Contains(MasterImport.DEPRECIATION.ToString()))
                            {
                                MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.GROUP_NOT_AVAIL);
                            }
                        }
                        foreach (DataRow dtitem in dtGroup.Rows)
                        {
                            using (AssetClassSystem assetGroupSystem = new AssetClassSystem())
                            {
                                Name = dtitem[ASSET_CLASS].ToString();
                                Igroup.Name = dtitem[PARENT_GROUP_NAME].ToString();
                                ParentGroupID = Igroup.FetchParentGroupId();
                                if (ParentGroupID != 0 && Name != string.Empty)
                                {
                                    Igroup.Name = dtitem[ASSET_CLASS].ToString();
                                    ClassID = Igroup.FetchAssetClassId();
                                    Igroup.AssetClassId = this.ClassID;
                                    Igroup.Name = dtitem[ASSET_CLASS].ToString();
                                    Igroup.ParentClassId = ParentGroupID;
                                    if (module.Equals(FinanceModule.Asset))
                                    {
                                        Igroup.Method = this.NumberSet.ToInteger(dtitem[MasterImport.METHOD.ToString()].ToString());
                                        Igroup.Depreciation = this.NumberSet.ToInteger(dtitem[MasterImport.DEPRECIATION.ToString()].ToString());
                                    }

                                    resultArgs = Igroup.SaveClassDetails();
                                    if (!resultArgs.Success) { break; }
                                    else
                                        resultArgs.Success = true;
                                }
                                else
                                {
                                    AcMELog.WriteLog(dtitem[PARENT_GROUP_NAME].ToString() + "has skiped, Because" + dtitem[ASSET_CLASS].ToString() + "is not exit and");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.GROUP_NOT_AVAIL);
                    }
                }
                else
                {
                    MessageRender.ShowMessage(MessageCatalog.Common.EMPTY_SHEET);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }

            return resultArgs;
        }

        /// <summary>
        /// Import Excel Template Custodian Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportCustodianDetails()
        {
            try
            {
                if (dtCustodian != null && dtCustodian.Rows.Count > 0)
                {
                    //  if (dtCustodian.Columns.Contains(Custodian) && dtCustodian.Columns.Contains(Role))
                    if (dtCustodian.Columns.Contains(Custodian))
                    {
                        foreach (DataRow dtitem in dtCustodian.Rows)
                        {
                            using (CustodiansSystem custodianSystem = new CustodiansSystem())
                            {
                                CustudianName = dtitem[Custodian].ToString();
                                CustodianID = GetId(ID.CustodianID);
                                custodianSystem.CustodiansId = CustodianID;//For Add Mode
                                custodianSystem.Name = dtitem[Custodian].ToString();
                                // custodianSystem.Role = dtitem[Role].ToString();
                                custodianSystem.Role = dtitem[MasterImport.ROLE.ToString()] != null ? dtitem[MasterImport.ROLE.ToString()].ToString() : null; // 1 owner 2 custodian
                                resultArgs = custodianSystem.SaveCustodiansDetails();
                                if (!resultArgs.Success) { break; }
                                else
                                    resultArgs.Success = true;
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.CUSTODIAN_NOT_AVAIL);
                    }
                }
                else
                {
                    MessageRender.ShowMessage(MessageCatalog.Common.EMPTY_SHEET);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Excel Template Manufacture Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportManufactureDetails()
        {
            try
            {
                if (DtDetails != null && DtDetails.Rows.Count > 0)
                {
                    if (DtDetails.Columns.Contains(MANUFACTURER_NAME))
                    {
                        foreach (DataRow drManufacture in DtDetails.Rows)
                        {
                            using (ManufactureInfoSystem ManufactureSystem = new ManufactureInfoSystem())
                            {
                                ManufactureName = drManufacture[MANUFACTURER_NAME].ToString();
                                ManufactureID = GetId(ID.ManufactureID);
                                ManufactureSystem.Id = ManufactureID;
                                ManufactureSystem.Name = ManufactureName;
                                ManufactureSystem.PanNo = drManufacture[PAN_NUMBER].ToString();
                                ManufactureSystem.TelephoneNo = drManufacture[CONTACT_NUMBER].ToString();
                                ManufactureSystem.Email = drManufacture[MasterImport.EMAIL.ToString()].ToString();
                                ManufactureSystem.Address = drManufacture[MasterImport.ADDRESS.ToString()].ToString();
                                resultArgs = ManufactureSystem.SaveDetails();
                                if (!resultArgs.Success) { break; }
                                else
                                    resultArgs.Success = true;
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.SELECT_VALID_FILE);
                    }
                }
                else
                {
                    MessageRender.ShowMessage(MessageCatalog.Common.EMPTY_SHEET);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Excel Template Asset Location
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportAssetLocation()
        {
            try
            {
                if (dtLocation != null && dtLocation.Rows.Count > 0)
                {
                    if (dtLocation.Columns.Contains(PARENT_LOCATION_NAME) && dtLocation.Columns.Contains(LOCATION_NAME))
                    {
                        foreach (DataRow drlocationItem in dtLocation.Rows)
                        {
                            using (LocationSystem locationSystem = new LocationSystem())
                            {
                                Name = drlocationItem[PARENT_LOCATION_NAME].ToString();
                                ParentLocationId = GetId(ID.LocationID);
                                if (ParentLocationId > 0)
                                {
                                    Name = drlocationItem[LOCATION_NAME].ToString();
                                    LocationID = GetId(ID.LocationID);
                                    Locationtype = drlocationItem[Description].ToString();

                                    locationSystem.LocationId = LocationID;
                                    locationSystem.Name = Name;
                                    locationSystem.BlockId = ParentLocationId;
                                    locationSystem.LocationType = Convert.ToInt32(Locationtype);
                                    resultArgs = locationSystem.SaveLocationDetails(LocationID);
                                    if (!resultArgs.Success) { break; }
                                    else
                                        resultArgs.Success = true;
                                }
                                else
                                {
                                    AcMELog.WriteLog(drlocationItem[LOCATION_NAME].ToString() + "" + "has skiped, Because" + drlocationItem[PARENT_LOCATION_NAME].ToString() + "" + "is not exit");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.LOCATION_NOT_AVAIL);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }

            return resultArgs;
        }

        /// <summary>
        /// Import Asset Opening Balance
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportAssetOpBalance()
        {
            int rowcount = 0;
            try
            {
                if (dtOPBalance != null && dtOPBalance.Rows.Count > 0)
                {
                    if (dtOPBalance.Columns.Contains(ASSET_CLASS) && dtLocation.Columns.Contains(ASSET_ITEM))
                    {
                        foreach (DataRow drAssetOpening in dtOPBalance.Rows)
                        {
                            using (LocationSystem locationSystem = new LocationSystem())
                            {
                                ProjectName = drAssetOpening[PROJECT].ToString();
                                ProjectID = GetId(ID.ProjectId);
                                if (ProjectID > 0)
                                {
                                    AssetClass = drAssetOpening[ASSET_CLASS].ToString();
                                    if (!string.IsNullOrEmpty(AssetClass))
                                    {
                                        ClassID = GetId(ID.ClassID);
                                        ItemName = drAssetOpening[ASSET_ITEM].ToString();
                                        if (!string.IsNullOrEmpty(ItemName))
                                        {
                                            ItemID = GetId(ID.ItemID);
                                            Prefix = drAssetOpening[PREFIX].ToString();
                                            Prefix = !string.IsNullOrEmpty(ItemName) ? Prefix : rowcount + "ITM";
                                            Suffix = drAssetOpening[SUFFIX].ToString();
                                            Quantity = this.NumberSet.ToInteger(drAssetOpening[QUANTITY].ToString());
                                            Amount = this.NumberSet.ToDouble(drAssetOpening[AMOUNT].ToString());

                                            rowcount++;
                                        }
                                        else
                                        {
                                            AcMELog.WriteLog(drAssetOpening[ASSET_ITEM].ToString() + "" + "has skiped, Because" + "Asset Item does not exists");
                                        }
                                    }
                                    else
                                    {
                                        AcMELog.WriteLog(drAssetOpening[ASSET_CLASS].ToString() + "" + "has skiped, Because" + "class does not exists");
                                    }
                                }
                                else
                                {
                                    AcMELog.WriteLog(drAssetOpening[LOCATION_NAME].ToString() + "" + "has skiped, Because" + drAssetOpening[PARENT_LOCATION_NAME].ToString() + "" + "is not exit");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.LOCATION_NOT_AVAIL);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Stock Item Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportStockItemDetails()
        {
            try
            {
                if (dtItems != null)
                {
                    if (dtItems.Columns.Contains(ASSET_ITEM) && dtItems.Columns.Contains(ASSET_CLASS))
                    {
                        foreach (DataRow drItem in dtItems.Rows)
                        {
                            using (StockItemSystem StockSystem = new StockItemSystem())
                            {
                                Igroup.Name = drItem[ASSET_CLASS].ToString();
                                ClassID = Igroup.FetchAssetClassId();
                                UnitName = drItem[MasterImport.UNIT.ToString()].ToString();
                                UnitID = GetId(ID.UnitID);
                                if (ClassID > 0 && UnitID > 0)
                                {
                                    ItemName = drItem[ASSET_ITEM].ToString();
                                    ItemID = GetId(ID.StockItemID);
                                    StockSystem.ItemId = ItemID;
                                    StockSystem.Name = ItemName;
                                    StockSystem.GroupId = ClassID;
                                    StockSystem.UnitId = UnitID;
                                    //StockSystem.ReOrder = this.NumberSet.ToInteger(drItem[REORDER].ToString());
                                    //StockSystem.Rate = this.NumberSet.ToDecimal(drItem[RATE_PER_ITEM].ToString());
                                    resultArgs = StockSystem.SaveStockItems();
                                    if (!resultArgs.Success) { break; }
                                    else
                                        resultArgs.Success = true;
                                }
                                else
                                {
                                    AcMELog.WriteLog(drItem[ASSET_ITEM].ToString() + "has skiped, Because" + drItem[ASSET_CLASS].ToString() + "is not exit and" + drItem[MasterImport.UNIT.ToString()].ToString() + "is not exit");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.ITEM__NOT_AVAIL);
                    }
                }
                else
                {
                    MessageRender.ShowMessage(MessageCatalog.Common.EMPTY_SHEET);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        /// <summary>
        /// Import Asset Item Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportAssetItemDetails()
        {
            int accountLedgerId = 0;
            try
            {
                if (dtItems != null)
                {

                    foreach (DataRow drItem in dtItems.Rows)
                    {
                        if (dtItems.Columns.Contains(ASSET_CLASS) && dtItems.Columns.Contains(ASSET_ITEM))
                        {
                            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                            {
                                AssetClass = drItem[ASSET_CLASS].ToString();
                                ClassID = GetId(ID.ClassID);
                                UnitName = drItem[MasterImport.UNIT.ToString()].ToString();
                                UnitID = GetId(ID.UnitID);
                                ItemName = drItem[ASSET_ITEM].ToString();
                                ItemID = GetId(ID.ItemID);
                                LedgerName = drItem[ACCOUNT_LEDGER].ToString();
                                accountLedgerId = GetId(ID.AccountLedgerId);
                                if (ClassID > 0 && UnitID > 0) //&& accountLedgerId > 0)
                                {
                                    //assetItemSystem.ItemId = ItemID;
                                    assetItemSystem.AssetClassId = ClassID;
                                    assetItemSystem.Name = ItemName;
                                    //assetItemSystem.DepreciationLedger = 1;
                                    //assetItemSystem.DisposalLedger = 1;
                                    assetItemSystem.AccountLeger = accountLedgerId;
                                    assetItemSystem.Unit = UnitID;
                                    assetItemSystem.Prefix = drItem[MasterImport.PREFIX.ToString()].ToString();
                                    assetItemSystem.Suffix = drItem[MasterImport.SUFFIX.ToString()].ToString();
                                    assetItemSystem.StartingNo = this.NumberSet.ToInteger(drItem[STARTING_NUMBER].ToString());
                                    assetItemSystem.RetentionYrs = this.NumberSet.ToInteger(drItem[RETENTION_YRS].ToString());
                                    assetItemSystem.DepreciationYrs = this.NumberSet.ToInteger(drItem[DEPRECIATION_YRS].ToString());
                                    assetItemSystem.AssetItemMode = this.NumberSet.ToInteger(drItem[METHOD].ToString());
                                    assetItemSystem.InsuranceApplicable = this.NumberSet.ToInteger(drItem[IS_INSURED].ToString());
                                    assetItemSystem.AMCApplicable = this.NumberSet.ToInteger(drItem[IS_AMC].ToString());
                                    resultArgs = assetItemSystem.SaveItemDetails();
                                    if (!resultArgs.Success) { break; }
                                    else
                                        resultArgs.Success = true;
                                }
                                else
                                {
                                    AcMELog.WriteLog(drItem[ASSET_ITEM].ToString() + "has skiped, Because" + drItem[ASSET_CLASS].ToString() + "is not exit and" + drItem[MasterImport.UNIT.ToString()].ToString() + "is not exit");
                                }
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.ITEM__NOT_AVAIL);
                        }
                    }
                }
                else
                {
                    MessageRender.ShowMessage(MessageCatalog.Common.EMPTY_SHEET);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return resultArgs;
        }

        /// <summary>
        /// Import Vendor Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ImportVendorDetails()
        {
            try
            {
                if (DtDetails != null && DtDetails.Rows.Count > 0)
                {
                    if (DtDetails.Columns.Contains(VENDOR_NAME))
                    {
                        foreach (DataRow drVendor in DtDetails.Rows)
                        {
                            using (VendorInfoSystem VendorSystem = new VendorInfoSystem())
                            {
                                VendarName = drVendor[VENDOR_NAME].ToString();
                                VendorID = GetId(ID.VendorID);
                                VendorSystem.Id = VendorID;
                                VendorSystem.Name = VendarName;
                                VendorSystem.PanNo = drVendor[PAN_NUMBER].ToString();
                                VendorSystem.TelephoneNo = drVendor[CONTACT_NUMBER].ToString();
                                VendorSystem.Email = drVendor[MasterImport.EMAIL.ToString()].ToString();
                                VendorSystem.Address = drVendor[MasterImport.ADDRESS.ToString()].ToString();
                                resultArgs = VendorSystem.SaveDetails();
                                if (!resultArgs.Success) { break; }
                                else
                                    resultArgs.Success = true;
                            }
                        }
                    }
                    else
                    {
                        MessageRender.ShowMessage(MessageCatalog.DataSynchronization.ImportExcel.SELECT_VALID_FILE);
                    }
                }
                else
                {
                    MessageRender.ShowMessage(MessageCatalog.Common.EMPTY_SHEET);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            return resultArgs;
        }

        public ResultArgs DeleteGroupDetails()
        {
            resultArgs = Igroup.DeleteAll();
            return resultArgs;
        }

        /// <summary>
        /// Get the ID
        /// </summary>
        /// <param name="queryId"></param>
        /// <returns></returns>
        public int GetId(ID queryId)
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    switch (queryId)
                    {
                        case ID.ItemID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.AssetItem.FetchAssetItemIDByName;
                                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_ITEMColumn, ItemName);
                                //sudhakar
                                // dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_CLASS_IDColumn, ClassID);
                                break;
                            }
                        case ID.StockItemID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.StockItem.FetchStockItemNameByID;
                                dataManager.Parameters.Add(this.AppSchema.StockItem.NAMEColumn, ItemName);
                                break;
                            }
                        case ID.LocationID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.AssetLocation.FetchAssetLocationNameByID;
                                dataManager.Parameters.Add(this.AppSchema.ASSETLocationDetails.LOCATIONColumn, Name);
                                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIAN_IDColumn, CustodianID);
                                break;
                            }

                        case ID.UnitID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.AssetUnitOfMeasure.FetchUnitOfMeasureId;
                                dataManager.Parameters.Add(this.AppSchema.ASSETUnitOfMeassure.SYMBOLColumn, UnitName);
                                break;
                            }

                        case ID.VendorID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.VendorInfo.FetchVendorNameByID;
                                dataManager.Parameters.Add(this.AppSchema.Vendors.VENDORColumn, VendarName);
                                break;
                            }
                        case ID.ManufactureID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.ManufactureInfo.FetchManufactureNameByID;
                                dataManager.Parameters.Add(this.AppSchema.Manufactures.MANUFACTURERColumn, ManufactureName);
                                break;
                            }

                        case ID.CustodianID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.AssetCustodians.FetchCustodianNameByID;
                                dataManager.Parameters.Add(this.AppSchema.AssetCustodians.CUSTODIANColumn, CustudianName);
                                break;
                            }
                        case ID.ProjectId:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.Project.FetchProjectId;
                                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                                break;
                            }
                        case ID.ClassID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.AssetClass.FetchAssetClassNameByID;
                                dataManager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn, AssetClass);
                                break;
                            }
                        case ID.ParentClassId:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.AssetClass.FetchAssetParentClassIdByParentClassName;
                                dataManager.Parameters.Add(this.AppSchema.ASSETClassDetails.ASSET_CLASSColumn, ParentClass);
                                break;
                            }
                        case ID.AccountLedgerId:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.LedgerBank.FetchLedgerIdByName;
                                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                                break;
                            }
                        case ID.BlockID:
                            {
                                dataManager.DataCommandArgs.SQLCommandId = SQLCommand.Block.FetchBlockIDByName;
                                dataManager.Parameters.Add(this.AppSchema.Block.BLOCKColumn, BlockName);
                                break;
                            }
                    }
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
            }

            // return resultArgs.DataSource.Sclar.ToInteger != 0 ? resultArgs.DataSource.Sclar.ToInteger : 0;
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// Delete the Location Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteLocationDetails()
        {
            using (LocationSystem LocationSystem = new LocationSystem())
            {
                resultArgs = LocationSystem.DeleteLocationDetails();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Stock Item Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteStockItemDetails()
        {
            using (StockItemSystem itemSystem = new StockItemSystem())
            {
                resultArgs = itemSystem.DeleteStockItemDetails();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Asset Item Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteAssetItemDetails()
        {
            using (AssetItemSystem itemsystem = new AssetItemSystem())
            {
                resultArgs = itemsystem.DeleteAssetItems();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Vendor Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteVendorDetails()
        {
            using (VendorInfoSystem vondorSystem = new VendorInfoSystem())
            {
                resultArgs = vondorSystem.DeleteVendorDetails();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Manufacture Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteManufactureDetails()
        {
            using (ManufactureInfoSystem ManufactureSystem = new ManufactureInfoSystem())
            {
                resultArgs = ManufactureSystem.DeleteManufactureDetails();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Custodian Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteCustodianDetails()
        {
            using (CustodiansSystem CustodianSystem = new CustodiansSystem())
            {
                resultArgs = CustodianSystem.DeleteCutodianDetail();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Donor Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteDonorDetails()
        {
            using (DonorAuditorSystem DonorSystem = new DonorAuditorSystem())
            {
                resultArgs = DonorSystem.DeleteDonorDetails();
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch the class name and return the Calss ID 
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public int FetchAssetParentClassID(string ParentClassName)
        {
            int PrClassID = 0;
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    if (!string.IsNullOrEmpty(ParentClassName))
                    {
                        AssetClass = ParentClassName;
                        PrClassID = GetId(ID.ClassID);
                        if (PrClassID == 0)
                        {
                            assetClassSystem.AssetClassId = 0;
                            assetClassSystem.AssetClass = ParentClassName;
                            assetClassSystem.ParentClassId = (int)FixedAssetClass.Primary;

                            resultArgs = assetClassSystem.SaveClassDetails();
                            if (resultArgs.Success)
                            {
                                assetClassSystem.AssetClass = AssetClass;
                                PrClassID = assetClassSystem.FetchAssetClassNameById();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return PrClassID;
        }

        /// <summary>
        /// Fetch the class name and return the Calss ID 
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public int FetchAssetClassID(string ClassName, string ParentClassname = "")
        {
            try
            {
                using (AssetClassSystem assetClassSystem = new AssetClassSystem())
                {
                    if (ParentClassname != "")
                    {
                        if (!string.IsNullOrEmpty(ClassName))
                        {
                            ClassID = GetId(ID.ClassID);
                            if (ClassID == 0)
                            {
                                assetClassSystem.AssetClassId = 0;
                                assetClassSystem.AssetClass = ClassName;
                                int prId = FetchAssetParentClassID(ParentClassname);
                                assetClassSystem.ParentClassId = prId == 0 ? (int)FixedAssetClass.Primary : prId;

                                resultArgs = assetClassSystem.SaveClassDetails();
                                if (resultArgs.Success)
                                {
                                    assetClassSystem.AssetClass = ClassName;
                                    ClassID = assetClassSystem.FetchAssetClassNameById();
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return ClassID;
        }

        /// <summary>
        /// Fetch the Class name and Asset item, return the ItemID
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="AssetItem"></param>
        /// <returns></returns>
        public ResultArgs FetchAssetItemID(string ClassName, string AssetItem, string ParentClassname)
        {
            int accountLedgerId = 0;
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    // Asset Class Checking & Update if does not exists
                    //  if (ClassName != "" && ParentClassname != "")
                    if (ParentClassname != "")
                    {
                        ParentClass = ParentClassname;
                        ClassID = GetId(ID.ParentClassId);
                        if (ClassID != 0)
                        {
                            AssetClass = ClassName;
                            ClassID = GetId(ID.ClassID);
                            if (ClassID == 0)
                                ClassID = FetchAssetClassID(AssetClass);
                            // Asset UOM Checking & Update if does not exists
                            UnitName = FixedAssetDefaultUOM.Nos.ToString();
                            UnitID = GetId(ID.UnitID);
                            if (UnitID == 0)
                            {
                                using (AssetUnitOfMeassureSystem UnitOfMeasure = new AssetUnitOfMeassureSystem())
                                {
                                    UnitOfMeasure.unitId = 0;
                                    UnitOfMeasure.NAME = "Numbers";
                                    UnitOfMeasure.SYMBOL = UnitName;
                                    resultArgs = UnitOfMeasure.SaveMeasureDetails();
                                }
                            }
                            if (resultArgs.Success)
                            {
                                ItemName = AssetItem.Replace("'", "");
                                ItemID = GetId(ID.ItemID);
                                LedgerName = EnumSet.GetDescriptionFromEnumValue(FixedAssetDefaultLedger.FixedAssetLedger);
                                accountLedgerId = GetId(ID.AccountLedgerId);

                                if (accountLedgerId == 0)
                                {
                                    accountLedgerId = SaveFixedAssetLedger();
                                }

                                if (ItemID == 0)
                                {
                                    assetItemSystem.ItemId = 0;
                                    assetItemSystem.AssetClassId = ClassID;
                                    assetItemSystem.Name = ItemName;
                                    assetItemSystem.AccountLeger = accountLedgerId;
                                    assetItemSystem.Unit = UnitID;
                                    assetItemSystem.Prefix = Prefix;
                                    assetItemSystem.Suffix = Suffix;
                                    assetItemSystem.StartingNo = 1;
                                    assetItemSystem.RetentionYrs = RetentionYrs;
                                    assetItemSystem.DepreciationYrs = DepreciationYrs;
                                    assetItemSystem.AssetItemMode = 1;
                                    assetItemSystem.AMCApplicable = AMCApplicable;

                                    assetItemSystem.InsuranceApplicable = InsuranceApplicable;
                                    assetItemSystem.DepreciatonApplicable = DepreciationYrs > 0 ? 1 : 0;

                                    resultArgs = assetItemSystem.SaveItemDetails();
                                }
                                else
                                {
                                    assetItemSystem.ItemId = ItemID;
                                    assetItemSystem.AssetClassId = ClassID;
                                    assetItemSystem.Name = ItemName;
                                    assetItemSystem.AccountLeger = accountLedgerId;
                                    assetItemSystem.Unit = UnitID;
                                    assetItemSystem.Prefix = Prefix;
                                    assetItemSystem.Suffix = Suffix;
                                    assetItemSystem.StartingNo = 1;
                                    assetItemSystem.RetentionYrs = RetentionYrs;
                                    assetItemSystem.DepreciationYrs = DepreciationYrs;
                                    assetItemSystem.AssetItemMode = 1;
                                    assetItemSystem.AMCApplicable = AMCApplicable;
                                    assetItemSystem.InsuranceApplicable = InsuranceApplicable;
                                    assetItemSystem.DepreciatonApplicable = DepreciationYrs > 0 ? 1 : 0;
                                    resultArgs = assetItemSystem.SaveItemDetails();
                                }
                            }
                        }

                    }
                    //else
                    //{

                    //    MessageBox.Show("Parent Class is Empty or Mismatched");

                    //}
                    //   this.CloseWaitDialog(); 


                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return resultArgs;
        }

        private void CloseWaitDialog()
        {
            throw new NotImplementedException();
        }


        public int SaveFixedAssetLedger()
        {
            int LedID = 0;
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.LedgerCode = "FA001";
                ledgerSystem.LedgerName = EnumSet.GetDescriptionFromEnumValue(FixedAssetDefaultLedger.FixedAssetLedger);
                ledgerSystem.GroupId = 11;
                ledgerSystem.LedgerType = ledgerSystem.LedgerSubType = "GN";// CA-Cash Leger, GN -General Ledger
                ledgerSystem.LedgerId = 0;
                resultArgs = ledgerSystem.SaveLedger();

                if (resultArgs.Success)
                {
                    LedID = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                }
            }
            return LedID;
        }
        //changed by sudhakar to get Location type and LocationId
        public int FetchLocationID(string Location, string BlockName, int custID, string LocationType, int ProId)
        {
            try
            {
                // DataTable dt = new DataTable();
                using (LocationSystem locationSystem = new LocationSystem())
                {
                    if (!string.IsNullOrEmpty(Location))
                    {
                        CustodianID = custID;
                        LocationID = GetId(ID.LocationID);
                        if (LocationID == 0)
                        {
                            locationSystem.LocationId = 0;
                            locationSystem.Name = Location;

                            if (custID == 0)
                            {

                                locationSystem.CustodianId = FetchCustodianID(FixedAssetCustodian.Unknown.ToString(), Role);//,Role
                            }
                            else
                            {
                                locationSystem.CustodianId = custID;
                            }

                            int BlId = FetchBlockId(BlockName);
                            locationSystem.BlockId = BlId == 0 ? FetchBlockId(FixedAssetBlock.Unknown.ToString()) : BlId;
                            locationSystem.LocationType = LocationType == "Own" ? 0 : 1;
                            locationSystem.ResponsibleDate = DateSet.ToDate(this.YearFrom, false);
                            locationSystem.ResponsibleToDate = DateSet.ToDate(this.YearTo, false);
                            resultArgs = locationSystem.SaveLocationDetails(LocationID);

                            if (resultArgs.Success)
                            {
                                locationSystem.Name = Location;
                                LocationID = locationSystem.FetchLocationNameByID();

                                // To set the Location for the Project which we imported // 13/09/2024
                                locationSystem.LocationId = LocationID;
                                locationSystem.ProjectId = ProId;
                                locationSystem.MultipleProjectIds = ProId.ToString();
                                locationSystem.SaveAssetMapLocation();
                            }
                        }//

                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return LocationID;
        }
        public int FetchCustodianID(string Custodian, string Role)//string Role
        {
            try
            {
                using (CustodiansSystem custodianSystem = new CustodiansSystem())
                {
                    if (!string.IsNullOrEmpty(Custodian))
                    {
                        CustodianID = GetId(ID.CustodianID);
                        if (CustodianID == 0)
                        {
                            custodianSystem.CustodiansId = 0;
                            custodianSystem.Name = Custodian;
                            //sudhakar
                            custodianSystem.Role = Role;


                            resultArgs = custodianSystem.SaveCustodiansDetails();
                            if (resultArgs.Success)
                            {
                                custodianSystem.Name = Custodian;
                                //sudhakar
                                //custodianSystem.Role = Role;
                                CustodianID = custodianSystem.FetchCustodiansNameById();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return CustodianID;
        }

        /// <summary>
        /// Fetch the Block name and return the Block ID
        /// </summary>
        /// <param name="ClassName"></param>
        /// <param name="AssetItem"></param>
        /// <returns></returns>
        public int FetchBlockId(string Blockname)
        {
            try
            {
                // Asset Class Checking & Update if does not exists
                BlockName = Blockname;
                BlockID = GetId(ID.BlockID);
                if (BlockID == 0)
                {
                    using (BlockSystem blockSystem = new BlockSystem())
                    {
                        blockSystem.BlockId = 0;
                        blockSystem.Block = BlockName;
                        resultArgs = blockSystem.SaveBlockDetails();
                        if (resultArgs.Success)
                        {
                            BlockID = blockSystem.FetchBlockNameById();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return BlockID;
        }
        public int FetchManufacturerID(string ManufacturerName)
        {
            try
            {
                // Asset Class Checking & Update if does not exists
                ManufactureName = ManufacturerName;
                ManufactureID = GetId(ID.ManufactureID);
                if (ManufactureID == 0)
                {
                    using (ManufactureInfoSystem manufacturerSystem = new ManufactureInfoSystem())
                    {
                        manufacturerSystem.Id = 0;
                        manufacturerSystem.Name = ManufactureName;
                        manufacturerSystem.Address = "";
                        manufacturerSystem.TelephoneNo = "";
                        manufacturerSystem.Email = "";
                        //sudhakar
                        manufacturerSystem.PanNo = "";
                        manufacturerSystem.GSTNo = "";

                        resultArgs = manufacturerSystem.SaveDetails();
                        if (resultArgs.Success)
                        {
                            ManufactureID = manufacturerSystem.FetchManufactureNameByID();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Source + Environment.NewLine + ex.Message);
                AcMELog.WriteLog(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
            return ManufactureID;
        }
        #endregion
    }
}
