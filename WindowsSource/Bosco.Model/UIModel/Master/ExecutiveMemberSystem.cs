/*  Class Name      : CountrySystem.cs
 *  Purpose         : To have all the logic of Executive Member Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Collections;
using System.IO;

namespace Bosco.Model.UIModel
{
    public class ExecutiveMemberSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ExecutiveMemberSystem()
        {
        }
        public ExecutiveMemberSystem(int ExecutiveId)
        {
            FillExecutiveMember(ExecutiveId);
        }
        #endregion

        #region ExecutiveMember Property
        public int ExecutiveId { get; set; }
        public string ExecutiveName { get; set; }
        public string FatherName { get; set; }
        public string DateOfBirth { get; set; }
        public string Religion { get; set; }
        public string Role { get; set; }
        public string Nationality { get; set; }
        public string Occupation { get; set; }
        public string Association { get; set; }
        public string OfficeBearer { get; set; }
        public string Place { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Pan_SSN { get; set; }
        public string AadharNo { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string URL { get; set; }
        public string DateOfAppointment { get; set; }
        public string DateOfExit { get; set; }
        public byte[] ImageData { get; set; }
        public string Notes { get; set; }
        public int LegalEntityId { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchExecutiveMemberDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExecutiveMembers.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_FROMColumn, DateOfAppointment);
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.YEAR_TOColumn, DateOfExit);

                if (LegalEntityId != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.LegalEntity.CUSTOMERIDColumn, LegalEntityId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteExecuteMember(int ExecutiveId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExecutiveMembers.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn, ExecutiveId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteGoveringDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExecutiveMembers.DeleteAll))
            {
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveExecutiveMemberDetails()
        {
            using (DataManager dataManager = new DataManager((ExecutiveId == 0) ? SQLCommand.ExecutiveMembers.Add : SQLCommand.ExecutiveMembers.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn, ExecutiveId);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVEColumn, ExecutiveName);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.NAMEColumn, FatherName);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.DATE_OF_BIRTHColumn, DateOfBirth);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.RELIGIONColumn, Religion);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.ROLEColumn, Role);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.NATIONALITYColumn, Nationality);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.OCCUPATIONColumn, Occupation);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.ASSOCIATIONColumn, Association);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.OFFICE_BEARERColumn, OfficeBearer);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.STATE_IDColumn, StateId);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PIN_CODEColumn, PinCode);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PAN_SSNColumn, Pan_SSN);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.AADHAR_NOColumn, AadharNo);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PHONEColumn, Phone);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.FAXColumn, Fax);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EMAILColumn, EMail);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.URLColumn, URL);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.DATE_OF_APPOINTMENTColumn, DateOfAppointment);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.DATE_OF_EXITColumn, DateOfExit);
                //  dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.IMAGESColumn, ImageData);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.CUSTOMERIDColumn, LegalEntityId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FillExecutiveMember(int ExecutiveId)
        {
            resultArgs = FillDetailsbyId(ExecutiveId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ExecutiveName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.EXECUTIVEColumn.ColumnName].ToString();
                FatherName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.NAMEColumn.ColumnName].ToString();
                DateOfBirth = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.DATE_OF_BIRTHColumn.ColumnName].ToString();
                Religion = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.RELIGIONColumn.ColumnName].ToString();
                Role = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.ROLEColumn.ColumnName].ToString();
                Nationality = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.NATIONALITYColumn.ColumnName].ToString();
                Occupation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.OCCUPATIONColumn.ColumnName].ToString();
                Association = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.ASSOCIATIONColumn.ColumnName].ToString();
                OfficeBearer = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.OFFICE_BEARERColumn.ColumnName].ToString();
                Place = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PLACEColumn.ColumnName].ToString();
                StateId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.STATE_IDColumn.ColumnName].ToString());
                CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.COUNTRY_IDColumn.ColumnName].ToString());
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.ADDRESSColumn.ColumnName].ToString();
                PinCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PIN_CODEColumn.ColumnName].ToString();
                Pan_SSN = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PAN_SSNColumn.ColumnName].ToString();
                AadharNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.AADHAR_NOColumn.ColumnName].ToString();
                Phone = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PHONEColumn.ColumnName].ToString();
                Fax = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.FAXColumn.ColumnName].ToString();
                EMail = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.EMAILColumn.ColumnName].ToString();
                URL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.URLColumn.ColumnName].ToString();
                DateOfAppointment = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.DATE_OF_APPOINTMENTColumn.ColumnName].ToString();
                DateOfExit = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.DATE_OF_EXITColumn.ColumnName].ToString();
                //ImageData = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.IMAGESColumn.ColumnName].ToString().Equals(string.Empty) ? ImageData : (byte[])resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.IMAGESColumn.ColumnName];
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.NOTESColumn.ColumnName].ToString();
                LegalEntityId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.CUSTOMERIDColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        private ResultArgs FillDetailsbyId(int ExecutiveId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExecutiveMembers.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn, ExecutiveId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
