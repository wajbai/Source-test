using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Utility.ConfigSetting;
using System.IO;

namespace Bosco.Model.UIModel.Master
{
    public class LegalEntitySystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
        #endregion

        #region Properties
        public int CustomerId { get; set; }
        public string InstitueName { get; set; }
        public string SocietyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Place { get; set; }
        public string Fax { get; set; }
        public string GIRNo { get; set; }
        public string A12No { get; set; }
        public string PANNo { get; set; }
        public string TANNo { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string EMail { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public int LedgerId { get; set; }
        public string URL { get; set; }
        public string RegNo { get; set; }
        public string PermissionNo { get; set; }
        public string AssoicationNature { get; set; }
        public int Denomination { get; set; }
        public string OtherAssociationNature { get; set; }
        public string OtherDenomination { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime PermissionDate { get; set; }
        public string FCRINo { get; set; }
        public DateTime FCRIRegDate { get; set; }
        public string EightyGNo { get; set; }
        public DateTime EightyGNoRegDate { get; set; }
        public string GSTNo { get; set; }
        #endregion

        #region Constructor
        public LegalEntitySystem()
        {
        }

        public LegalEntitySystem(int CustomerId)
        {
            this.CustomerId = CustomerId;
            FillBankProperties();
        }
        #endregion

        #region Methods
        public ResultArgs SaveLegalEntityDetails()
        {
            using (DataManager dataManager = new DataManager(CustomerId.Equals(0) ? SQLCommand.LegalEntity.Add : SQLCommand.LegalEntity.Update))
            {
                dataManager.Parameters.Add(AppSchema.LegalEntity.CUSTOMERIDColumn, CustomerId, true);
                //  dataManager.Parameters.Add(AppSchema.LegalEntity.INSTITUTENAMEColumn, InstitueName);
                dataManager.Parameters.Add(AppSchema.LegalEntity.SOCIETYNAMEColumn, SocietyName);
                dataManager.Parameters.Add(AppSchema.LegalEntity.CONTACTPERSONColumn, ContactPerson);
                dataManager.Parameters.Add(AppSchema.LegalEntity.ADDRESSColumn, Address);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PLACEColumn, Place);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PHONEColumn, Phone);
                dataManager.Parameters.Add(AppSchema.LegalEntity.FAXColumn, Fax);
                dataManager.Parameters.Add(AppSchema.LegalEntity.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(AppSchema.LegalEntity.A12NOColumn, A12No);
                dataManager.Parameters.Add(AppSchema.LegalEntity.GIRNOColumn, GIRNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.TANNOColumn, TANNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PANNOColumn, PANNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.STATE_IDColumn, StateId);
                dataManager.Parameters.Add(AppSchema.LegalEntity.EMAILColumn, EMail);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(AppSchema.LegalEntity.URLColumn, URL);
                dataManager.Parameters.Add(AppSchema.LegalEntity.REGNOColumn, RegNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONNOColumn, PermissionNo);

                if (RegDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.REGDATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.REGDATEColumn, this.DateSet.ToDate(RegDate.ToShortDateString(), DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                }

                if (PermissionDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONDATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONDATEColumn, this.DateSet.ToDate(PermissionDate.ToShortDateString(), DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                }
                dataManager.Parameters.Add(AppSchema.LegalEntity.ASSOCIATIONNATUREColumn, AssoicationNature);
                dataManager.Parameters.Add(AppSchema.LegalEntity.DENOMINATIONColumn, Denomination);
                dataManager.Parameters.Add(AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn, OtherAssociationNature);
                dataManager.Parameters.Add(AppSchema.LegalEntity.OTHER_DENOMINATIONColumn, OtherDenomination);
                dataManager.Parameters.Add(AppSchema.LegalEntity.FCRINOColumn, FCRINo);
                if (FCRIRegDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.FCRIREGDATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.FCRIREGDATEColumn, this.DateSet.ToDate(FCRIRegDate.ToShortDateString(), DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                }
                dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTYGNOColumn, EightyGNo);
                if (EightyGNoRegDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn, this.DateSet.ToDate(EightyGNoRegDate.ToShortDateString(), DateFormatInfo.MySQLFormat.DateAdd.ToString()));
                }
                dataManager.Parameters.Add(AppSchema.LegalEntity.GST_NOColumn, GSTNo);
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLegalEntity()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FillBankProperties()
        {
            resultArgs = LegalEntityDetailsById();
            DataTable dtLegalEntityData = resultArgs.DataSource.Table;
            if (dtLegalEntityData != null && dtLegalEntityData.Rows.Count > 0)
            {
                // InstitueName = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.INSTITUTENAMEColumn.ColumnName].ToString();
                SocietyName = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString();
                ContactPerson = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString();
                Address = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString();
                Place = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString();
                Fax = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.FAXColumn.ColumnName].ToString();
                GIRNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.GIRNOColumn.ColumnName].ToString();
                A12No = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.A12NOColumn.ColumnName].ToString();
                PANNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PANNOColumn.ColumnName].ToString();
                TANNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.TANNOColumn.ColumnName].ToString();
                Phone = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString();
                StateId = this.NumberSet.ToInteger(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.STATE_IDColumn.ColumnName].ToString());
                EMail = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString();
                Pincode = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString();
                CountryId = this.NumberSet.ToInteger(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.COUNTRY_IDColumn.ColumnName].ToString());
                LedgerId = this.NumberSet.ToInteger(dtLegalEntityData.Rows[0][AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                URL = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.URLColumn.ColumnName].ToString();
                RegNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.REGNOColumn.ColumnName].ToString();
                PermissionNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PERMISSIONNOColumn.ColumnName].ToString();
                AssoicationNature = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.ASSOCIATIONNATUREColumn.ColumnName].ToString();
                Denomination = NumberSet.ToInteger(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.DENOMINATIONColumn.ColumnName].ToString());

                OtherAssociationNature = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn.ColumnName] != DBNull.Value ? dtLegalEntityData.Rows[0][AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn.ColumnName].ToString() : string.Empty;
                OtherDenomination = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.OTHER_DENOMINATIONColumn.ColumnName] != DBNull.Value ? dtLegalEntityData.Rows[0][AppSchema.LegalEntity.OTHER_DENOMINATIONColumn.ColumnName].ToString() : string.Empty;

                if (!string.IsNullOrEmpty(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString()))
                {
                    RegDate = this.DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString(), false);
                }
                if (!string.IsNullOrEmpty(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString()))
                {
                    PermissionDate = this.DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString(), false);
                }

                FCRINo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.FCRINOColumn.ColumnName].ToString();
                if (!string.IsNullOrEmpty(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName].ToString()))
                {
                    FCRIRegDate = this.DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName].ToString(), false);
                }

                EightyGNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.EIGHTYGNOColumn.ColumnName].ToString();
                if (!string.IsNullOrEmpty(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName].ToString()))
                {
                    EightyGNoRegDate = this.DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName].ToString(), false);
                }

                GSTNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.GST_NOColumn.ColumnName].ToString();
            }
        }

        private ResultArgs LegalEntityDetailsById()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LegalEntity.FetchByID))
            {
                dataMember.Parameters.Add(AppSchema.LegalEntity.CUSTOMERIDColumn, CustomerId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteLegalEntityData()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LegalEntity.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.LegalEntity.CUSTOMERIDColumn, CustomerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public DataTable CheckNoofLegalentity(string Projectid)
        {
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.CheckLegalEntity))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Projectid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable FetchLedgalEntity()
        {
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.FetchAll))
            {
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public ResultArgs ApplyLicensePeriod(string licensePath)
        {
            ResultArgs resultargs = new ResultArgs();
            try
            {
                if (File.Exists(licensePath))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(licensePath);
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        DataView dvLicense = DecryptLicenseDetails(ds.Tables[0].AsDataView());
                        DataTable dtLicense = dvLicense.ToTable();
                        // DataView dvLicense = ds.Tables[0].AsDataView();
                        using (UserSystem userSystem = new UserSystem())
                        {
                            if (!dtLicense.Columns.Contains(userSystem.AppSchema.LicenseDataTable.AccessToMultiDBColumn.ColumnName))
                                dtLicense.Columns.Add(userSystem.AppSchema.LicenseDataTable.AccessToMultiDBColumn.ColumnName);
                            foreach (DataRow dr in dtLicense.Rows)
                            {
                                resultargs.Success = true;
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_NAMEColumn.ColumnName))
                                {
                                    SettingProperty.Current.HeadOfficeName = dr[userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_NAMEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_NAMEColumn.ColumnName].ToString() : string.Empty;
                                }
                                SettingProperty.Current.SocietyName = dr[userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.InstituteName = dr[userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.NoOfNodes = dr[userSystem.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.NoOfModules = dr[userSystem.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.Address = dr[userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.Place = dr[userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.PinCode = dr[userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.State = dr[userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.Phone = dr[userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.Fax = dr[userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.Email = dr[userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.URL = dr[userSystem.AppSchema.LegalEntity.URLColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.URLColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.CountryInfo = dr[userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString() : string.Empty;
                                SettingProperty.Current.ContactPerson = dr[userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString() : string.Empty;

                                if (SettingProperty.ActiveDatabaseLicenseKeypath == "AcMEERPLicense.xml")
                                {
                                    SettingProperty.Current.AccesstoMultiDB = this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.AccessToMultiDBColumn.ColumnName].ToString());
                                }

                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LegalEntity.IS_MULTILOCATIONColumn.ColumnName))
                                {
                                    SettingProperty.Current.MultiLocation = dr[userSystem.AppSchema.LegalEntity.IS_MULTILOCATIONColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LegalEntity.IS_MULTILOCATIONColumn.ColumnName].ToString()) : 0;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LegalEntity.ENABLE_PORTALColumn.ColumnName))
                                {
                                    SettingProperty.Current.EnablePortal = dr[userSystem.AppSchema.LegalEntity.ENABLE_PORTALColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LegalEntity.ENABLE_PORTALColumn.ColumnName].ToString()) : 0;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.LOCK_MASTERColumn.ColumnName))
                                {
                                    SettingProperty.Current.LockMasters = dr[userSystem.AppSchema.LicenseDataTable.LOCK_MASTERColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.LOCK_MASTERColumn.ColumnName].ToString()) : 0;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.MAP_LEDGERColumn.ColumnName))
                                {
                                    SettingProperty.Current.MapHeadOfficeLedger = dr[userSystem.AppSchema.LicenseDataTable.MAP_LEDGERColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.MAP_LEDGERColumn.ColumnName].ToString()) : 0;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.LICENSE_KEY_NUMBERColumn.ColumnName))
                                {
                                    SettingProperty.Current.LicenseKeyNumber = dr[userSystem.AppSchema.LicenseDataTable.LICENSE_KEY_NUMBERColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.LICENSE_KEY_NUMBERColumn.ColumnName].ToString() : string.Empty;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName))
                                {
                                    SettingProperty.Current.LicenseKeyExprDate = dr[userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName].ToString() : string.Empty;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.LOCATIONColumn.ColumnName))
                                {
                                    SettingProperty.Current.BranchLocations = dr[userSystem.AppSchema.LicenseDataTable.LOCATIONColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.LOCATIONColumn.ColumnName].ToString() : DefaultLocation.Primary.ToString();
                                }
                                else
                                {
                                    SettingProperty.Current.BranchLocations = DefaultLocation.Primary.ToString();
                                }

                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.KEY_GENERATED_DATEColumn.ColumnName))
                                {
                                    SettingProperty.Current.LicenseKeyGeneratedDate = dr[userSystem.AppSchema.LicenseDataTable.KEY_GENERATED_DATEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.KEY_GENERATED_DATEColumn.ColumnName].ToString() : string.Empty;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.YEAR_FROMColumn.ColumnName))
                                {
                                    SettingProperty.Current.LicenseKeyYearFrom = dr[userSystem.AppSchema.LicenseDataTable.YEAR_FROMColumn.ColumnName] != DBNull.Value ? objDec.DecryptString(dr[userSystem.AppSchema.LicenseDataTable.YEAR_FROMColumn.ColumnName].ToString()) : string.Empty;
                                }
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName))
                                {
                                    SettingProperty.Current.LicenseKeyYearTo = dr[userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName].ToString() : string.Empty;
                                }
                                SettingProperty.Current.HeadofficeCode = dr[userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                                SettingProperty.Current.BranchOfficeCode = dr[userSystem.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();

                                // 21.06.2022 Third Party URL
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName))
                                {
                                    // SettingProperty.Current.ManagementURL = dr[userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName].ToString() : string.Empty;
                                    // SettingProperty.Current.code = dr[userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName].ToString() : string.Empty;
                                }

                                // 21.06.2022 Code for Third Party Code
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.THIRDPARTY_CODEColumn.ColumnName))
                                {
                                    SettingProperty.Current.ManagementCode = dr[userSystem.AppSchema.LicenseDataTable.THIRDPARTY_CODEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.THIRDPARTY_CODEColumn.ColumnName].ToString() : string.Empty;
                                }

                                // 21.06.2022 Third Party Mode (API / Service)
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.THIRDPARTY_MODEColumn.ColumnName))
                                {
                                    SettingProperty.Current.ManagementMode = dr[userSystem.AppSchema.LicenseDataTable.THIRDPARTY_MODEColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.THIRDPARTY_MODEColumn.ColumnName].ToString() : string.Empty;
                                }

                                // 21.06.2022 Third Party URL
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.THIRDPARTY_URLColumn.ColumnName))
                                {
                                    SettingProperty.Current.ManagementURL = dr[userSystem.AppSchema.LicenseDataTable.THIRDPARTY_URLColumn.ColumnName] != DBNull.Value ? dr[userSystem.AppSchema.LicenseDataTable.THIRDPARTY_URLColumn.ColumnName].ToString() : string.Empty;
                                }

                                //03/04/2017, to keep IS_LICENSE_MODEL value
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.IS_LICENSE_MODELColumn.ColumnName))
                                {
                                    SettingProperty.Current.IsLicenseModel = dr[userSystem.AppSchema.LicenseDataTable.IS_LICENSE_MODELColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.IS_LICENSE_MODELColumn.ColumnName].ToString()) : 1;
                                }
                                else
                                {
                                    SettingProperty.Current.IsLicenseModel = 1; //Default validate licnese
                                }

                                //10/03/2020, To set Is Two Month Budget for mysore
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.IS_TWO_MONTH_BUDGETColumn.ColumnName))
                                {
                                    SettingProperty.Current.IsTwoMonthBudget = dr[userSystem.AppSchema.LicenseDataTable.IS_TWO_MONTH_BUDGETColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.IS_TWO_MONTH_BUDGETColumn.ColumnName].ToString()) : 0;
                                }
                                else
                                {
                                    SettingProperty.Current.IsTwoMonthBudget = 0; //Default validate licnese
                                }
                                //for  Temp
                                //SettingProperty.Current.IsTwoMonthBudget = 1;

                                //09/05/2020, To set automatic backup

                                //10/03/2020, To set Is Two Month Budget for mysore
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.AUTOMATIC_BACKUP_PORTALColumn.ColumnName))
                                {
                                    SettingProperty.Current.AutomaticDBBackupToPortal = dr[userSystem.AppSchema.LicenseDataTable.AUTOMATIC_BACKUP_PORTALColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.AUTOMATIC_BACKUP_PORTALColumn.ColumnName].ToString()) : 0;
                                }
                                else
                                {
                                    SettingProperty.Current.AutomaticDBBackupToPortal = 0; //Default validate licnese
                                }

                                // for Budget Approved in Portal
                                // 26.08.2020
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_PORTALColumn.ColumnName))
                                {
                                    SettingProperty.Current.ApproveBudgetByPortal = dr[userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_PORTALColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_PORTALColumn.ColumnName].ToString()) : 0;
                                }
                                else
                                {
                                    SettingProperty.Current.ApproveBudgetByPortal = 0;
                                }

                                //22/03/2021, for Budget Approve by Excel
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_EXCELColumn.ColumnName))
                                {
                                    SettingProperty.Current.ApproveBudgetByExcel = dr[userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_EXCELColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_EXCELColumn.ColumnName].ToString()) : 0;
                                }
                                else
                                {
                                    SettingProperty.Current.ApproveBudgetByExcel = 0;
                                }

                                //11/11/2022, To assign state code ----------------------------------------------------------------
                                if (!string.IsNullOrEmpty(SettingProperty.Current.State))
                                {
                                    using (StateSystem sysState = new StateSystem())
                                    {
                                        ResultArgs result = sysState.FetchStateByStateName(SettingProperty.Current.State);
                                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                                        {
                                            DataTable dt = result.DataSource.Table;
                                            SettingProperty.Current.StateCode = dt.Rows[0][AppSchema.State.STATE_CODEColumn.ColumnName].ToString().Trim();
                                        }
                                    }
                                }
                                //--------------------------------------------------------------------------------------------------

                                //09/11/2023, To Assign Licensebased Reports -------------------------------------------------------
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.ENABLE_REPORTSColumn.ColumnName))
                                {
                                    SettingProperty.Current.EnabledReports = dr[userSystem.AppSchema.LicenseDataTable.ENABLE_REPORTSColumn.ColumnName] != DBNull.Value ?
                                            dr[userSystem.AppSchema.LicenseDataTable.ENABLE_REPORTSColumn.ColumnName].ToString() : string.Empty;
                                }
                                else
                                {
                                    SettingProperty.Current.EnabledReports = string.Empty;
                                }
                                //--------------------------------------------------------------------------------------------------

                                //09/09/2024, To Assing Multi Currency -------------------------------------------------------------
                                SettingProperty.Current.AllowMultiCurrency = 0;
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.ALLOW_MULTI_CURRENCYColumn.ColumnName))
                                {
                                    SettingProperty.Current.AllowMultiCurrency = dr[userSystem.AppSchema.LicenseDataTable.ALLOW_MULTI_CURRENCYColumn.ColumnName] != DBNull.Value ?
                                            NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.ALLOW_MULTI_CURRENCYColumn.ColumnName].ToString()) : 0;
                                }
                                //---------------------------------------------------------------------------------------------------

                                //15/11/2024, To Assign Attach Voucher Files -------------------------------------------------------------
                                SettingProperty.Current.AttachVoucherFiles = 0;
                                if (dr.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.ATTACH_VOUCHERS_FILESColumn.ColumnName))
                                {
                                    SettingProperty.Current.AttachVoucherFiles = dr[userSystem.AppSchema.LicenseDataTable.ATTACH_VOUCHERS_FILESColumn.ColumnName] != DBNull.Value ?
                                            NumberSet.ToInteger(dr[userSystem.AppSchema.LicenseDataTable.ATTACH_VOUCHERS_FILESColumn.ColumnName].ToString()) : 0;
                                }
                                //---------------------------------------------------------------------------------------------------
                            }
                        }
                    }
                }
                else
                {
                    resultargs.Message = "License file does not exist";
                }
            }
            catch (Exception ex)
            {
                resultargs.Message = ex.Message;
            }
            finally { }
            return resultargs;
        }

        private DataView DecryptLicenseDetails(DataView dvLicense)
        {
            if (dvLicense != null && dvLicense.Table.Rows.Count > 0)
            {
                using (UserSystem userSystem = new UserSystem())
                {
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_NAMEColumn.ColumnName))
                    {
                        string homapp = dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_NAMEColumn.ColumnName].ToString().Trim();
                        if (CommonMethod.IsEncryptedText(homapp))
                        {
                            dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.HEAD_OFFICE_NAMEColumn.ColumnName] = CommonMethod.Decrept(homapp);
                        }
                    }
                    dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.SocietyNameColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.InstituteNameColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.ADDRESSColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.PLACEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.PINCODEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.PHONEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.STATEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.STATEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.FAXColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.FAXColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.COUNTRYColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.EMAILColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LegalEntity.URLColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.URLColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfNodesColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.NoOfModulesColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.HEAD_OFFICE_CODEColumn.ColumnName].ToString());
                    dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.BRANCH_OFFICE_CODEColumn.ColumnName].ToString());
                    // dvLicense.Table.Rows[0][this.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName].ToString());
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LegalEntity.IS_MULTILOCATIONColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.IS_MULTILOCATIONColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.IS_MULTILOCATIONColumn.ColumnName].ToString());
                    }
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LegalEntity.ENABLE_PORTALColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.ENABLE_PORTALColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LegalEntity.ENABLE_PORTALColumn.ColumnName].ToString());
                    }
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.LOCK_MASTERColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.LOCK_MASTERColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.LOCK_MASTERColumn.ColumnName].ToString());
                    }
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.MAP_LEDGERColumn.ColumnName))
                    {
                        string mappledger = dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.MAP_LEDGERColumn.ColumnName].ToString().Trim();
                        if (CommonMethod.IsEncryptedText(mappledger))
                        {
                            dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.MAP_LEDGERColumn.ColumnName] = CommonMethod.Decrept(mappledger);
                        }

                    }
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.LOCATIONColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.LOCATIONColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.LOCATIONColumn.ColumnName].ToString());
                    }
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.KEY_GENERATED_DATEColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.KEY_GENERATED_DATEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.KEY_GENERATED_DATEColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.YEAR_TOColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.IS_LICENSE_MODELColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.IS_LICENSE_MODELColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.IS_LICENSE_MODELColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.LICENSE_KEY_NUMBERColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.LICENSE_KEY_NUMBERColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.LICENSE_KEY_NUMBERColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.CRISTO_PARISH_CODEColumn.ColumnName].ToString());
                    }

                    // it has to be enabled for Integration (15.10.2020) (01.06.2021)
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.THIRDPARTY_CODEColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.THIRDPARTY_CODEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.THIRDPARTY_CODEColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.THIRDPARTY_MODEColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.THIRDPARTY_MODEColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.THIRDPARTY_MODEColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.THIRDPARTY_URLColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.THIRDPARTY_URLColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.THIRDPARTY_URLColumn.ColumnName].ToString());
                    }

                    //On 22/03/2021, decrept license (Budget Approval by Portal, Budget Approval by Excell)
                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_PORTALColumn.ColumnName))
                    {
                        if (dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_PORTALColumn.ColumnName].ToString().Length > 1)
                        {
                            dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_PORTALColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_PORTALColumn.ColumnName].ToString());
                        }
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_EXCELColumn.ColumnName))
                    {
                        if (dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_EXCELColumn.ColumnName].ToString().Length > 1)
                        {
                            dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_EXCELColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.APPROVE_BUDGET_BY_EXCELColumn.ColumnName].ToString());
                        }
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.ENABLE_REPORTSColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.ENABLE_REPORTSColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.ENABLE_REPORTSColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.ALLOW_MULTI_CURRENCYColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.ALLOW_MULTI_CURRENCYColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.ALLOW_MULTI_CURRENCYColumn.ColumnName].ToString());
                    }

                    if (dvLicense.Table.Columns.Contains(userSystem.AppSchema.LicenseDataTable.ATTACH_VOUCHERS_FILESColumn.ColumnName))
                    {
                        dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.ATTACH_VOUCHERS_FILESColumn.ColumnName] = CommonMethod.Decrept(dvLicense.Table.Rows[0][userSystem.AppSchema.LicenseDataTable.ATTACH_VOUCHERS_FILESColumn.ColumnName].ToString());
                    }

                }
            }
            return dvLicense.Table.AsDataView();
        }


        /// <summary>
        /// On 27/08/2022, to get Legal Entity properties to be used in Receipt print
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetLegalEntityLegalProperties()
        {
            ResultArgs resultargs = new ResultArgs();
            try
            {
                //DataTable dtLedgalEntityProperties = AppSchema.LegalEntity.DefaultView.Table;
                DataTable dtLegalEntityProperties = new DataTable("LegalEntityProperties");
                dtLegalEntityProperties.Columns.Add("LEGALENTITY_FIELD_NAME", typeof(System.String));
                dtLegalEntityProperties.Columns.Add("LEGALENTITY_DISPLAY_NAME", typeof(System.String));
                dtLegalEntityProperties.Rows.Add(new object[] { AppSchema.LegalEntity.REGNOColumn.ColumnName, "Registration No" });
                dtLegalEntityProperties.Rows.Add(new object[] { AppSchema.LegalEntity.REGDATEColumn.ColumnName, "Registration Date" });
                dtLegalEntityProperties.Rows.Add(new object[] { AppSchema.LegalEntity.PANNOColumn.ColumnName, "PAN No" });
                dtLegalEntityProperties.Rows.Add(new object[] { AppSchema.LegalEntity.EIGHTYGNOColumn.ColumnName, "80G No" });
                //dtLegalEntityProperties.Rows.Add(new object[] { "80G Date", AppSchema.LegalEntity.REGDATEColumn.ColumnName });
                dtLegalEntityProperties.Rows.Add(new object[] { AppSchema.LegalEntity.FCRINOColumn.ColumnName, "FCRA No" });
                dtLegalEntityProperties.Rows.Add(new object[] { AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName, "FCRA Registration Date" });
                resultargs.DataSource.Data = dtLegalEntityProperties;
                resultargs.Success = true;
            }
            catch (Exception err)
            {
                resultargs.Message = err.Message;
            }
            return resultargs;
        }
        #endregion
    }
}
