using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Model.TDS
{
    public class TDSCompanyDeductorSystem : SystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string TaxAccountNo { get; set; }
        public string HeadOfficeTANNo { get; set; }
        public string PANNo { get; set; }
        public string TaxRegistrationNo { get; set; }
        public string IncomeTaxCircle { get; set; }
        public string DeductorType { get; set; }
        public string ResponsiblePerson { get; set; }
        public string SonDaughterOf { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string FlatNo{ get; set; }
        public string Premises { get; set; }
        public string Street { get; set; }
        public string Location { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        #endregion

        #region Methods
        public ResultArgs SaveTDSCompanyDeductor()
        {
            resultArgs = FetchCompanyDeductor();
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Id = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TdsCompanyDeductor.IDColumn.ColumnName].ToString());
            }
            else
            {
                Id = (int)AddNewRow.NewRow;
            }

            using (DataManager dataManager = new DataManager(Id == 0 ? SQLCommand.TDSCompanyDeductor.Add : SQLCommand.TDSCompanyDeductor.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.IDColumn, Id);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.TAX_DEDUCTION_ACCOUNT_NOColumn, TaxAccountNo);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.HEAD_OFFICE_TAN_NOColumn, HeadOfficeTANNo);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.PAN_NOColumn, PANNo);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.TAN_REGISTRATION_NOColumn, TaxRegistrationNo);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.INCOME_TAX_CIRCLEColumn, IncomeTaxCircle);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.DEDUCTOR_TYPEColumn, DeductorType);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.RESPONSIBLE_PERSONColumn, ResponsiblePerson);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.SON_DAUGHTER_OFColumn, SonDaughterOf);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.DESIGNATIONColumn, Designation);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.FLAT_NOColumn, FlatNo);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.PREMISESColumn, Premises);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.STREETColumn, Street);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.LOCATIONColumn, Location);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.DISTRICTColumn, District);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.STATEColumn, State);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.TELEPHONE_NOColumn, TelephoneNo);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.EMAILColumn, Email);
                dataManager.Parameters.Add(this.AppSchema.TdsCompanyDeductor.FULL_NAMEColumn, FullName);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchCompanyDeductor()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSCompanyDeductor.Fetch))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
