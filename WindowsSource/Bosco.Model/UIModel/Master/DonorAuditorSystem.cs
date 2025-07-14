using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel.Master
{
    public class DonorAuditorSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public DonorAuditorSystem()
        {
        }
        public DonorAuditorSystem(int DonAudId)
        {
            FillDonorAuditorProperties(DonAudId);
        }
        #endregion

        #region DonorAuditor Properties
        public int DonAudId { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public int Type { get; set; }
        public string Place { get; set; }
        public string CompanyName { get; set; }
        public int CountryId { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int IdentityKey { get; set; }
        public string URL { get; set; }
        public int FcDonor { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string PAN { get; set; }
        public DataTable dtMapDonor { get; set; }
        public int MapDonorId { get; set; }
        public bool IsDonor { get; set; }


        public int DonAudInfoId { get; set; }
        public string Language { get; set; }
        public string Religion { get; set; }
        public string Occupation { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOE { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Duration { get; set; }
        public int Periodic { get; set; }
        public string ReferedStaff { get; set; }
        public string Title { get; set; }

        #endregion

        #region DonorNetworkingProperties
        public int InstitutionalType { get; set; }
        public int PaymentMode { get; set; }
        public int EcsDuration { get; set; }
        public int Isactive { get; set; }
        public string StatusFilter { get; set; }
        public int RegType { get; set; }
        public string RegistrationType { get; set; }
        public string NameTitle { get; set; }
        public DateTime DOB { get; set; }
        //public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime Anniversarydate { get; set; }
        //public string Language { get; set; }
        //public string Religion { get; set; }
        //public string Occupation { get; set; }
        //public string ReferredStaff { get; set; }
        public string Organizationemployed { get; set; }
        public string LastName { get; set; }
        public string ReasonForInactive { get; set; }
        public int Prospectid { get; set; }
        public int MailStatus { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public CommunicationMode communicationMode { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchDonorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchDonor))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDonorByStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchDonorByStatus))
            {
                if (RegType != 0) //RegType Id =0 then it means that all Registration Types
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn, RegType);
                }

                dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, StatusFilter);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMailingThanksDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchMailingContributedStatus))
            {

                dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, MailStatus);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.Donormailhistory.COMMUNICATION_MODEColumn, (int)communicationMode);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMailingThanksgivingByStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchMailingThanksByStatus))
            {
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, MailStatus);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchAuditor))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditorList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchAuditorList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteDonorAuditorDetails(int DonAudId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.DonorAuditor.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonAudId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveDonorAuditor()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveDonorInfo(dataManager);
                if (resultArgs.Success && Prospectid > 0)
                {
                    using (ProspectManagementSystem prospectsys = new ProspectManagementSystem())
                    {
                        prospectsys.ProspectId = Prospectid;
                        prospectsys.DeleteDonorProspectDetails();
                    }
                }

                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveDonorInfo(DataManager dataManager)
        {
            resultArgs = GetDonorId();
            if (resultArgs.Success)
            {
                if (resultArgs.DataSource.Sclar.ToInteger == 0 || resultArgs.DataSource.Sclar.ToInteger == DonAudId)
                {
                    resultArgs = SaveDonorAuditorDetails(dataManager);
                    if (resultArgs.Success)
                    {
                        DonAudInfoId = (DonAudId == 0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : DonAudId;
                        // resultArgs = SaveDonorAuditorReferenceDetails(dataManager);
                        if (resultArgs.Success && IsDonor)
                        {
                            MapDonorId = DonAudId = MapDonorId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapDonorId;
                            MappDonor(dataManager);
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Record is Available " + "(" + Name + "," + Place + "," + Address + ")";
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveDonorAuditorDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager((DonAudId == 0) ? SQLCommand.DonorAuditor.Add : SQLCommand.DonorAuditor.Update))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonAudId, true);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NAMEColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.REGISTER_NOColumn, RegNo);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.LASTNAMEColumn, LastName);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.TYPEColumn, Type);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.COMPANY_NAMEColumn, CompanyName);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PHONEColumn, Phone);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.TELEPHONEColumn, Telephone);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.FAXColumn, Fax);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.EMAILColumn, Email);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.IDENTITYKEYColumn, IdentityKey);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.URLColumn, URL);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.STATE_IDColumn, StateId);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PANColumn, PAN);

                //for Donor Networking Module
                dataManager.Parameters.Add(this.AppSchema.DonorInstitutionalType.INSTITUTIONAL_TYPE_IDColumn, InstitutionalType);
                dataManager.Parameters.Add(this.AppSchema.DonorPaymentMode.PAYMENT_MODE_IDColumn, PaymentMode);
                dataManager.Parameters.Add(this.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn, RegType);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.ECS_DURATIONColumn, EcsDuration);

                dataManager.Parameters.Add(this.AppSchema.DonorProspects.TITLEColumn, Title);
                if (DOB == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DOBColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DOBColumn, DOB);
                }
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.GENDERColumn, Gender);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.MARITAL_STATUSColumn, MaritalStatus);
                if (Anniversarydate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn, Anniversarydate);
                }
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.LANGUAGEColumn, Language);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.RELIGIONColumn, Religion);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.OCCUPATIONColumn, Occupation);
                if (DOJ == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DATE_OF_JOINColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DATE_OF_JOINColumn, DOJ);
                }

                if (DOE == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DATE_OF_EXITColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.DonorProspects.DATE_OF_EXITColumn, DOE);
                }
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.REFERRED_STAFFColumn, ReferedStaff);
                dataManager.Parameters.Add(this.AppSchema.DonorProspects.ORG_EMPLOYEDColumn, Organizationemployed);
                dataManager.Parameters.Add(this.AppSchema.FDRenewal.STATUSColumn, Isactive);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.REASON_FOR_ACTIVEColumn, ReasonForInactive);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MappDonor(DataManager dataManagers)
        {
            using (MappingSystem mappDonor = new MappingSystem())
            {
                mappDonor.DonorId = MapDonorId;
                mappDonor.dtDonorMapping = dtMapDonor;
                mappDonor.AccountMappingDonorByDonorId(dataManagers);
            }
            return resultArgs;
        }
        private void FillDonorAuditorProperties(int DonAudId)
        {
            resultArgs = FetchDonorAuditorDetailsById(DonAudId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString();
                RegNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.REGISTER_NOColumn.ColumnName].ToString();
                Type = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.TYPEColumn.ColumnName].ToString());
                EcsDuration = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.ECS_DURATIONColumn.ColumnName].ToString());
                Place = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PLACEColumn.ColumnName].ToString();
                CompanyName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.COMPANY_NAMEColumn.ColumnName].ToString();
                CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.COUNTRY_IDColumn.ColumnName].ToString());
                Pincode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PINCODEColumn.ColumnName].ToString();
                Phone = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PHONEColumn.ColumnName].ToString();
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();
                IdentityKey = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.IDENTITYKEYColumn.ColumnName].ToString());
                Fax = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.FAXColumn.ColumnName].ToString();
                URL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.URLColumn.ColumnName].ToString();
                StateId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.STATE_IDColumn.ColumnName].ToString());
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.ADDRESSColumn.ColumnName].ToString();
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.NOTESColumn.ColumnName].ToString();
                PAN = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PANColumn.ColumnName].ToString();

                InstitutionalType = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorInstitutionalType.INSTITUTIONAL_TYPE_IDColumn.ColumnName].ToString());
                PaymentMode = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorPaymentMode.PAYMENT_MODE_IDColumn.ColumnName].ToString());
                RegType = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorRegistrationType.REGISTRATION_TYPE_IDColumn.ColumnName].ToString());
                Title = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.TITLEColumn.ColumnName].ToString();
                DOB = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DOBColumn.ColumnName].ToString()) ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DOBColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                Gender = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.GENDERColumn.ColumnName].ToString());
                MaritalStatus = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.MARITAL_STATUSColumn.ColumnName].ToString());
                Anniversarydate = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn.ColumnName].ToString()) ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.ANNIVERSARY_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                Language = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.LANGUAGEColumn.ColumnName].ToString();
                Religion = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.RELIGIONColumn.ColumnName].ToString();
                Occupation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.OCCUPATIONColumn.ColumnName].ToString();
                DOJ = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DATE_OF_JOINColumn.ColumnName].ToString()) ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DATE_OF_JOINColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                DOE = !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DATE_OF_EXITColumn.ColumnName].ToString()) ? this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.DATE_OF_EXITColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                ReferedStaff = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.REFERRED_STAFFColumn.ColumnName].ToString();
                Organizationemployed = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorProspects.ORG_EMPLOYEDColumn.ColumnName].ToString();
                Isactive = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["STATUS"].ToString());
                LastName = resultArgs.DataSource.Table.Rows[0]["LASTNAME"].ToString();
                ReasonForInactive = resultArgs.DataSource.Table.Rows[0]["REASON_FOR_ACTIVE"].ToString();
            }
        }

        public ResultArgs GetDonorId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.GetDonorId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NAMEColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.ADDRESSColumn, Address);

                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }


        public ResultArgs GetDonorIdName()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.GetIdDonorName))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NAMEColumn, Name);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        private ResultArgs FetchDonorAuditorDetailsById(int DonAudId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonAudId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteDonorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.DeleteDonourDetails))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion



        #region New Features Methods
        public ResultArgs DeleteDonorInfoDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.DeleteDonorRefDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONOR_IDColumn, DonAudId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchDonorReferenceDetailsById(int DonId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchDonorReferenceDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONOR_IDColumn, DonAudId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveDonorAuditorReferenceDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager((DonAudId == 0) ? SQLCommand.DonorAuditor.AddDonorReferenceDetails : SQLCommand.DonorAuditor.UpdateDonorReferenceDetails))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONOR_IDColumn, DonAudInfoId, true);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.TITLEColumn, Title);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.LANGUAGEColumn, Language);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.RELIGIONColumn, Religion);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.OCCUPATIONColumn, Occupation);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DURATIONColumn, Duration);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.GENDERColumn, Gender);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DATE_OF_BIRTHColumn, DateOfBirth);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PERIODICColumn, Periodic);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.REFEREDSTAFFColumn, ReferedStaff);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public int FetchDonorInsttitutionalTypeByName(string InstittuionalTypeName)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.DonorAuditor.FetchInstitutionalTypeByName))
            {
                datamanager.Parameters.Add(this.AppSchema.DonorInstitutionalType.INSTITUTIONAL_TYPEColumn, InstittuionalTypeName);
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int FetchDonorPaymentModeId(string PaymentMode)
        {
            using (DataManager dtManager = new DataManager(SQLCommand.DonorAuditor.FetchPaymentModeIdByPaymentMode))
            {
                dtManager.Parameters.Add(this.AppSchema.DonorPaymentMode.PAYMENT_MODEColumn, PaymentMode);
                resultArgs = dtManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int FetchDonorRegTypeId(string RegType)
        {
            using (DataManager dtManager = new DataManager(SQLCommand.DonorAuditor.FetchRegTypeByRegTypeId))
            {
                dtManager.Parameters.Add(this.AppSchema.DonorRegistrationType.REGISTRATION_TYPEColumn, RegType);
                resultArgs = dtManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs AutoFetchReasonForActive()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchReasonForInactive))
            {
                //dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, LoginUserId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
