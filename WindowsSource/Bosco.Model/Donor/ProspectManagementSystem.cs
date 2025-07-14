using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model
{
    public class ProspectManagementSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ProspectManagementSystem()
        {
        }
        public ProspectManagementSystem(int ProspectId)
        {
            FillDonorProspectsProperties(ProspectId);
        }
        #endregion

        #region DonorProspect Properties
        public int ProspectId { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public string LastName { get; set; }
        public string Religion { get; set; }
        public string OrgEmployed { get; set; }
        public string Occupation { get; set; }
        public string Language { get; set; }
        public string ReferredStaff { get; set; }
        public int Type { get; set; }
        public DateTime DOB { get; set; }
        public DateTime AnniversaryDate { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public string Place { get; set; }
        public int InstitutionalTypeId { get; set; }
        public int RegistrationTypeId { get; set; }
        public string Title { get; set; }
        public int RegType { get; set; }
        public string InstitutionalType { get; set; }
        public string RegistrationType { get; set; }
        public string ReferenceNumber { get; set; }
        public int CountryId { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SourceInformation { get; set; }
        public string URL { get; set; }
        public int StateId { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string PAN { get; set; }

        #endregion

        #region Methods

        public ResultArgs FetchProspectsDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        private void FillDonorProspectsProperties(int ProspectId)
        {
            resultArgs = FetchDonorProspectDetailsById(ProspectId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.NAMEColumn.ColumnName].ToString();
                RegNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.REGISTER_NOColumn.ColumnName].ToString();
                LastName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.LASTNAMEColumn.ColumnName].ToString();
                Type = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.TYPEColumn.ColumnName].ToString());
                DOB = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DOBColumn.ColumnName].ToString()) ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DOBColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                AnniversaryDate = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn.ColumnName].ToString()) ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                InstitutionalTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.INSTITUTIONAL_TYPE_IDColumn.ColumnName].ToString());
                Place = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.PLACEColumn.ColumnName].ToString();
                CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.COUNTRY_IDColumn.ColumnName].ToString());
                Pincode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.PINCODEColumn.ColumnName].ToString();
                Phone = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.PHONEColumn.ColumnName].ToString();
                Fax = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.FAXColumn.ColumnName].ToString();
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.EMAILColumn.ColumnName].ToString();
                RegistrationTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.REGISTRATION_TYPE_IDColumn.ColumnName].ToString());
                URL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.URLColumn.ColumnName].ToString();
                StateId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.STATE_IDColumn.ColumnName].ToString());
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.ADDRESSColumn.ColumnName].ToString();
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.NOTESColumn.ColumnName].ToString();
                PAN = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.PANColumn.ColumnName].ToString();
                SourceInformation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.SOURCE_INFORMATIONColumn.ColumnName].ToString();
                ReferredStaff = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.REFERRED_STAFFColumn.ColumnName].ToString();
                ReferenceNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.REFERENCE_NUMBERColumn.ColumnName].ToString();
                Title = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.TITLEColumn.ColumnName].ToString();
                Gender = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.GENDERColumn.ColumnName].ToString());
                MaritalStatus = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.MARITAL_STATUSColumn.ColumnName].ToString());
                Language = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.LANGUAGEColumn.ColumnName].ToString();
                Religion = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.RELIGIONColumn.ColumnName].ToString();
                Occupation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.OCCUPATIONColumn.ColumnName].ToString();
                OrgEmployed = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.ORG_EMPLOYEDColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchDonorProspectDetailsById(int ProspectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PROSPECT_IDColumn, ProspectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetProspectId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetProspectId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.NAMEColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.ADDRESSColumn, Address);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        public ResultArgs SaveDonorProspect()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveDonorProspectInfo(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveDonorProspectInfo(DataManager dataManager)
        {
            resultArgs = GetProspectId();
            if (resultArgs.Success)
            {
                if (resultArgs.DataSource.Sclar.ToInteger == 0 || resultArgs.DataSource.Sclar.ToInteger == ProspectId)
                {
                    resultArgs = SaveDonorProspectsDetails(dataManager);
                    if (resultArgs.Success)
                    {
                        ProspectId = (ProspectId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : ProspectId;
                    }
                }
                else
                {
                    resultArgs.Message = "Record is Available " + "(" + Name + "," + Place + "," + Address + ")";
                }

            }
            return resultArgs;
        }

        public ResultArgs SaveDonorProspectsDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager((ProspectId == 0) ? SQLCommand.DonorProspect.Add : SQLCommand.DonorProspect.Update))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PROSPECT_IDColumn, ProspectId, true);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.NAMEColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.REGISTER_NOColumn, RegNo);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.LASTNAMEColumn, LastName);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.RELIGIONColumn, Religion);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.OCCUPATIONColumn, Occupation);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.LANGUAGEColumn, Language);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.REFEREDSTAFFColumn, ReferredStaff);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.TYPEColumn, Type);

                dataManager.Parameters.Add(this.AppSchema.DonorProspects.TITLEColumn, Title);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.GENDERColumn, Gender);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.MARITAL_STATUSColumn, MaritalStatus);
                if (DOB == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DOBColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DOBColumn, DOB);
                }

                if (AnniversaryDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn, AnniversaryDate);
                }

                dataManager.Parameters.Add(this.AppSchema.DonorProspects.INSTITUTIONAL_TYPE_IDColumn, InstitutionalTypeId);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PHONEColumn, Phone);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.TELEPHONEColumn, Telephone);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.FAXColumn, Fax);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.EMAILColumn, Email);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.REGISTRATION_TYPE_IDColumn, RegistrationTypeId);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.URLColumn, URL);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.STATE_IDColumn, StateId);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PANColumn, PAN);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.SOURCE_INFORMATIONColumn, SourceInformation);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.REFERENCE_NUMBERColumn, ReferenceNumber);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.ORG_EMPLOYEDColumn, OrgEmployed);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteDonorProspectDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.PROSPECT_IDColumn, ProspectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchDonorInstitutionalType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.FetchInstitutionalType))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDonorRegistrationType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.FetchRegistrationType))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDonorPaymentMode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.FetchDonorPaymentMode))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchProspectByname(string name)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.DonorProspect.FetchSearchByName))
            {
                datamanager.Parameters.Add(this.AppSchema.DonorProspects.NAMEColumn, name);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetRegistrationTypeId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetRegistrationId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.REGISTRATION_TYPEColumn, RegistrationType);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }
        public ResultArgs GetInsTypeId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetInstitutionId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.INSTITUTIONAL_TYPEColumn, InstitutionalType);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDonorByRegistrationType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorProspect.GetDonorRegStatus))
            {
                if (RegType != 0) //RegType Id =0 then it means that all Registration Types
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn, RegType);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
