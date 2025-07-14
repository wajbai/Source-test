using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Acme.erpSupport
{
    public static class LicenseDetails
    {
        public static string HeadOfficeCode
        { get; set; }

        public static string HeadOfficeName
        { get; set; }
        
        public static string BranchOfficeCode
        { get; set; }
        
        public static string BranchKeyCode
        { get; set; }

        public static string BranchName
        { get; set; }
        
        public static string SocietyName
        { get; set; }

        public static string InstituteName
        { get; set; }

        public static string ContactPerson
        { get; set; }

        public static string Address
        { get; set; }

        public static string Place
        { get; set; }

        public static string PinCode
        { get; set; }

        public static int AccesstoMultiDB
        { get; set; }

        public static int MultiLocation
        { get; set; }

        public static int EnablePortal
        { get; set; }

        public static int LockMasters
        { get; set; }

        public static int MapHeadOfficeLedger
        { get; set; }

        public static string BranchLocations
        { get; set; }

        public static string IsLicenseModel
        { get; set; }

        public static string LicenseKeyNumber
        { get; set; }

        public static string LicenseKeyExprDate
        { get; set; }

        public static string IsLockOnLicense
        { get; set; }

        public static string LicenseKeyGeneratedDate
        { get; set; }

        public static string LicenseKeyYearFrom
        { get; set; }

        public static string LicenseKeyYearTo
        { get; set; }

        public static string DeploymentType
        { get; set; }
             
        public static string NoOfNodes
        { get; set; }

        public static string NoOfModules
        { get; set; }

        public static string Phone
        { get; set; }

        public static string Mobile
        { get; set; }

        public static string CountryInfo
        { get; set; }

        public static string State
        { get; set; }

        public static string Fax
        { get; set; }

        public static string Email
        { get; set; }

        public static string URL
        { get; set; }

        public static ResultArgs GetLicenseDetailsInfo(string licensefilename)
        {
            ResultArgs result = new ResultArgs();
            DataView dvRtn = new DataView();
            DataView dvLicense = new DataView();

            try
            {
                string licensefilepath = Path.Combine(General.ACMEERP_INSTALLED_PATH, licensefilename);
                if (File.Exists(licensefilepath))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(licensefilepath );
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        dvLicense = ds.Tables[0].AsDataView();
                        dsSupport.LicenseDataTable licensetable = new dsSupport.LicenseDataTable();
                        if (dvLicense != null && dvLicense.Table.Rows.Count > 0)
                        {
                            if (dvLicense.Table.Columns.Contains(licensetable.BRANCH_OFFICE_NAMEColumn.ColumnName))
                            {
                                BranchName = General.DecryptString(dvLicense.Table.Rows[0][licensetable.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.BRANCH_OFFICE_NAMEColumn.ColumnName] = BranchName;
                            }

                            SocietyName = General.DecryptString(dvLicense.Table.Rows[0][licensetable.SocietyNameColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.SocietyNameColumn.ColumnName] = SocietyName;

                            InstituteName = General.DecryptString(dvLicense.Table.Rows[0][licensetable.InstituteNameColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.InstituteNameColumn.ColumnName] = InstituteName;

                            Address = General.DecryptString(dvLicense.Table.Rows[0][licensetable.ADDRESSColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.ADDRESSColumn.ColumnName] = Address;

                            Place = General.DecryptString(dvLicense.Table.Rows[0][licensetable.PLACEColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.PLACEColumn.ColumnName] = Place;

                            PinCode = General.DecryptString(dvLicense.Table.Rows[0][licensetable.PINCODEColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.PINCODEColumn.ColumnName] = PinCode;

                            ContactPerson = General.DecryptString(dvLicense.Table.Rows[0][licensetable.CONTACTPERSONColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.CONTACTPERSONColumn.ColumnName] = ContactPerson;

                            Phone = General.DecryptString(dvLicense.Table.Rows[0][licensetable.PHONEColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.PHONEColumn.ColumnName] = Phone;

                            Mobile = General.DecryptString(dvLicense.Table.Rows[0][licensetable.MOBILE_NOColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.MOBILE_NOColumn.ColumnName] = Mobile;
                            
                            State = General.DecryptString(dvLicense.Table.Rows[0][licensetable.STATEColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.STATEColumn.ColumnName] = State;

                            Fax = General.DecryptString(dvLicense.Table.Rows[0][licensetable.FAXColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.FAXColumn.ColumnName] = Fax;

                            CountryInfo = General.DecryptString(dvLicense.Table.Rows[0][licensetable.COUNTRYColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.COUNTRYColumn.ColumnName] = CountryInfo;

                            Email = General.DecryptString(dvLicense.Table.Rows[0][licensetable.EMAILColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.EMAILColumn.ColumnName] = Email;

                            URL = General.DecryptString(dvLicense.Table.Rows[0][licensetable.URLColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.URLColumn.ColumnName] = URL;

                            NoOfNodes = General.DecryptString(dvLicense.Table.Rows[0][licensetable.NoOfNodesColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.NoOfNodesColumn.ColumnName] = NoOfNodes;

                            NoOfModules = General.DecryptString(dvLicense.Table.Rows[0][licensetable.NoOfModulesColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.NoOfModulesColumn.ColumnName] = NoOfModules;

                            HeadOfficeCode = General.DecryptString(dvLicense.Table.Rows[0][licensetable.HEAD_OFFICE_CODEColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.HEAD_OFFICE_CODEColumn.ColumnName] = HeadOfficeCode;

                            BranchOfficeCode = General.DecryptString(dvLicense.Table.Rows[0][licensetable.BRANCH_OFFICE_CODEColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.BRANCH_OFFICE_CODEColumn.ColumnName] = BranchOfficeCode;

                            BranchKeyCode= General.DecryptString(dvLicense.Table.Rows[0][licensetable.BRANCH_KEY_CODEColumn.ColumnName].ToString());
                            dvLicense.Table.Rows[0][licensetable.BRANCH_KEY_CODEColumn.ColumnName] = BranchKeyCode;


                            if (dvLicense.Table.Columns.Contains(licensetable.IS_MULTILOCATIONColumn.ColumnName))
                            {
                                MultiLocation = Int32.Parse(General.DecryptString(dvLicense.Table.Rows[0][licensetable.IS_MULTILOCATIONColumn.ColumnName].ToString()));
                                dvLicense.Table.Rows[0][licensetable.IS_MULTILOCATIONColumn.ColumnName] = (MultiLocation == 0 ? "NO" : "YES"); 
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.ENABLE_PORTALColumn.ColumnName))
                            {
                                EnablePortal = Int32.Parse(General.DecryptString(dvLicense.Table.Rows[0][licensetable.ENABLE_PORTALColumn.ColumnName].ToString()));
                                dvLicense.Table.Rows[0][licensetable.ENABLE_PORTALColumn.ColumnName] = (EnablePortal == 0 ? "NO" : "YES");
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.LOCK_MASTERColumn.ColumnName))
                            {
                                LockMasters = Int32.Parse(General.DecryptString(dvLicense.Table.Rows[0][licensetable.LOCK_MASTERColumn.ColumnName].ToString()));
                                dvLicense.Table.Rows[0][licensetable.LOCK_MASTERColumn.ColumnName] = (LockMasters == 0 ? "NO" : "YES");
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.MAP_LEDGERColumn.ColumnName))
                            {
                                MapHeadOfficeLedger = Int32.Parse(General.DecryptString(dvLicense.Table.Rows[0][licensetable.MAP_LEDGERColumn.ColumnName].ToString()));
                                dvLicense.Table.Rows[0][licensetable.MAP_LEDGERColumn.ColumnName] = (MapHeadOfficeLedger == 0 ? "NO" : "YES");
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.LOCATIONColumn.ColumnName))
                            {
                                BranchLocations = General.DecryptString(dvLicense.Table.Rows[0][licensetable.LOCATIONColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.LOCATIONColumn.ColumnName] = BranchLocations;
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.LICENSE_KEY_NUMBERColumn.ColumnName))
                            {
                                LicenseKeyNumber = General.DecryptString(dvLicense.Table.Rows[0][licensetable.LICENSE_KEY_NUMBERColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.LICENSE_KEY_NUMBERColumn.ColumnName] = LicenseKeyNumber;
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.KEY_GENERATED_DATEColumn.ColumnName))
                            {
                                LicenseKeyGeneratedDate = General.DecryptString(dvLicense.Table.Rows[0][licensetable.KEY_GENERATED_DATEColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.KEY_GENERATED_DATEColumn.ColumnName] = LicenseKeyGeneratedDate;
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.IS_LICENSE_MODELColumn.ColumnName))
                            {
                                IsLicenseModel = General.DecryptString(dvLicense.Table.Rows[0][licensetable.IS_LICENSE_MODELColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.IS_LICENSE_MODELColumn.ColumnName] = (IsLicenseModel == "0" ? "NO" : "YES"); ;
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.YEAR_FROMColumn.ColumnName))
                            {
                                LicenseKeyYearFrom = General.DecryptString(dvLicense.Table.Rows[0][licensetable.YEAR_FROMColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.YEAR_FROMColumn.ColumnName] = LicenseKeyYearFrom;
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.YEAR_TOColumn.ColumnName))
                            {
                                LicenseKeyYearTo = General.DecryptString(dvLicense.Table.Rows[0][licensetable.YEAR_TOColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.YEAR_TOColumn.ColumnName] = LicenseKeyYearTo;
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.DEPLOYMENT_TYPEColumn.ColumnName))
                            {
                                DeploymentType = General.DecryptString(dvLicense.Table.Rows[0][licensetable.DEPLOYMENT_TYPEColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.DEPLOYMENT_TYPEColumn.ColumnName] = DeploymentType;
                            }
                            
                            if (dvLicense.Table.Columns.Contains(licensetable.IS_LOCK_ON_EXPIRYColumn.ColumnName))
                            {
                                IsLockOnLicense= General.DecryptString(dvLicense.Table.Rows[0][licensetable.IS_LOCK_ON_EXPIRYColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.IS_LOCK_ON_EXPIRYColumn.ColumnName] = (IsLockOnLicense == "0" ? "NO" : "YES");
                            }

                            if (dvLicense.Table.Columns.Contains(licensetable.ACCESSTOMULTIDBColumn.ColumnName))
                            {
                                //AccesstoMultiDB = Int32.Parse(General.DecryptString(dvLicense.Table.Rows[0][licensetable.ACCESSTOMULTIDBColumn.ColumnName].ToString()));
                                AccesstoMultiDB = Int32.Parse(dvLicense.Table.Rows[0][licensetable.ACCESSTOMULTIDBColumn.ColumnName].ToString());
                                dvLicense.Table.Rows[0][licensetable.ACCESSTOMULTIDBColumn.ColumnName] = (AccesstoMultiDB == 0? "NO":"YES");
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Error in getting GetLicenseDetailsInfo " + err.Message;
            }

            if (dvLicense.Table != null)
            {
                dvRtn = dvLicense.Table.AsDataView();
                result.Success = true;
                result.DataSource.Data = dvRtn;
            }
            else
            {
                result.Message = "License Details are not found";
            }

            return result;
        }
    }
}