using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;
using Bosco.Model.Business;

namespace Bosco.Model.TDS
{
    public class DeducteeTaxSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int TaxPolicyId { get; set; }
        public int DeducteeTypeId { get; set; }
        public int NaturePaymentId { get; set; }
        public int PartyPaymentId { get; set; }
        public int ProjectId { get; set; }
        public int PartyLedgerId { get; set; }
        public DateTime ApplicableFrom { get; set; }
        public int TaxTypeId { get; set; }
        public Decimal Rate { get; set; }
        public Decimal ExemptionLimit { get; set; }
        public DataTable dtTaxDetails { get; set; }

        public string TaxTypeName { get; set; }
        public int IsActive { get; set; }
        #endregion

        #region Constructor
        public DeducteeTaxSystem()
        {
        }
        public DeducteeTaxSystem(int DutyTaxId)
        {
            FillDutyTaxTypeProperties(DutyTaxId);
        }
        #endregion

        #region Methods
        #region Duty Tax Type Methods
        /// <summary>
        /// Save Tax Type  Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveTaxTypeDetails()
        {
            using (DataManager dataManager = new DataManager(TaxTypeId == 0 ? SQLCommand.DeducteeTax.DutyTaxTypeAdd : SQLCommand.DeducteeTax.DutyTaxTypeUpdate))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTaxType.TDS_DUTY_TAXTYPE_IDColumn, TaxTypeId);
                dataManager.Parameters.Add(this.AppSchema.DutyTaxType.TAX_TYPE_NAMEColumn, TaxTypeName);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.STATUSColumn, IsActive);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete Tax Type Detils
        /// </summary>
        /// <param name="Taxid"></param>
        /// <returns></returns>
        public ResultArgs DeleteDutyTaxTypeDetails(int Taxid)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.DeducteeTax.DutyTaxTypeDelete))
            {
                dataMember.Parameters.Add(this.AppSchema.DutyTaxType.TDS_DUTY_TAXTYPE_IDColumn, Taxid);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;

        }

        /// <summary>
        /// Fetch Duty Tax Types
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchDutyTaxTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxTypeFetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveDutyTaxTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.FetchActiveDutyTaxType))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fill Tax Type Details
        /// </summary>
        /// <param name="DutyTaxId"></param>
        private void FillDutyTaxTypeProperties(int DutyTaxId)
        {
            resultArgs = FetchDutyTaxTypeById(DutyTaxId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                TaxTypeName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DutyTaxType.TAX_TYPE_NAMEColumn.ColumnName].ToString();
                IsActive = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString());
            }
        }

        /// <summary>
        /// Fetch Tax type by ID
        /// </summary>
        /// <param name="TaxId"></param>
        /// <returns></returns>
        private ResultArgs FetchDutyTaxTypeById(int TaxId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxTypeFetchById))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTaxType.TDS_DUTY_TAXTYPE_IDColumn, TaxId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Duty Tax methods

        public ResultArgs FetchTaxPolicy()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.FetchTaxPolicy))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_DEDUCTEE_TYPE_IDColumn, DeducteeTypeId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_NATURE_PAYMENT_IDColumn, NaturePaymentId);
                dataManager.Parameters.Add(this.AppSchema.TDSPartyPayment.PARTY_LEDGER_IDColumn, PartyLedgerId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveDutyTaxDetails()
        {

            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteTaxDetails(dataManager);
                if (resultArgs != null && resultArgs.Success)
                {
                    TaxDetails(dataManager);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs DeleteTaxDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxRateDelete))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_DEDUCTEE_TYPE_IDColumn, DeducteeTypeId);
                dataManagers.Database = dataManagers.Database;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private void TaxDetails(DataManager dataManagers)
        {
            foreach (DataRow dr in dtTaxDetails.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxRateAdd))
                {
                    dataManager.Database = dataManagers.Database;
                    bool HasValues = false;
                    NaturePaymentId = NumberSet.ToInteger(dr["NATURE_PAY_ID"].ToString());
                    ApplicableFrom = DateSet.ToDate(dr["APPLICABLE_FROM"].ToString(), true);
                    resultArgs = SaveDeducteeTaxDetails();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        int TDSPolicyId = this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());

                        //decimal TDSRate = NumberSet.ToDecimal(dr["TDS_RATE"].ToString());
                        //decimal TDSExemption = NumberSet.ToDecimal(dr["TDS_EXEMPTION_LIMIT"].ToString());

                        //decimal SurRate = NumberSet.ToDecimal(dr["SUR_RATE"].ToString());
                        //decimal SurExemption = NumberSet.ToDecimal(dr["SUR_EXEMPTION"].ToString());

                        //decimal EdCessRate = NumberSet.ToDecimal(dr["ED_CESS_RATE"].ToString());
                        //decimal EdCessExemption = NumberSet.ToDecimal(dr["ED_CESS_EXEMPTION"].ToString());

                        //decimal SecEdCessRate = NumberSet.ToDecimal(dr["SEC_ED_CESS_RATE"].ToString());
                        //decimal SecEdCessExemption = NumberSet.ToDecimal(dr["SEC_ED_CESS_EXEMPTION"].ToString());

                        decimal TDSRate = NumberSet.ToDecimal(dr[AppSchema.DutyTax.TDS_RATEColumn.ColumnName].ToString());
                        decimal TDSExemption = NumberSet.ToDecimal(dr[AppSchema.DutyTax.TDS_EXEMPTION_LIMITColumn.ColumnName].ToString());

                        decimal SurRate = NumberSet.ToDecimal(dr[AppSchema.DutyTax.SUR_RATEColumn.ColumnName].ToString());
                        decimal SurExemption = NumberSet.ToDecimal(dr[AppSchema.DutyTax.SUR_EXEMPTIONColumn.ColumnName].ToString());

                        decimal EdCessRate = NumberSet.ToDecimal(dr[AppSchema.DutyTax.ED_CESS_RATEColumn.ColumnName].ToString());
                        decimal EdCessExemption = NumberSet.ToDecimal(dr[AppSchema.DutyTax.ED_CESS_EXEMPTIONColumn.ColumnName].ToString());

                        decimal SecEdCessRate = NumberSet.ToDecimal(dr[AppSchema.DutyTax.SEC_ED_CESS_RATEColumn.ColumnName].ToString());
                        decimal SecEdCessExemption = NumberSet.ToDecimal(dr[AppSchema.DutyTax.SEC_ED_CESS_EXEMPTIONColumn.ColumnName].ToString());

                        decimal TDSRateWithoutPan = NumberSet.ToDecimal(dr[AppSchema.DutyTax.TDSRATE_WITHOUT_PANColumn.ColumnName].ToString());
                        decimal TDSExemptionWithoutPan = NumberSet.ToDecimal(dr[AppSchema.DutyTax.TDSEXEMPTION_LIMIT_WITHOUT_PANColumn.ColumnName].ToString());

                        if (TDSRate > 0 || TDSExemption > 0 || SurRate > 0 || SurExemption > 0 || EdCessRate > 0 || EdCessExemption > 0 || SecEdCessRate > 0 ||
                            SecEdCessExemption > 0 || TDSRateWithoutPan > 0 || TDSExemptionWithoutPan > 0)
                        {
                            HasValues = true;
                        }
                        if (HasValues)
                        {
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;

                            dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TDSPolicyId, true);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_RATEColumn, TDSRate);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_EXEMPTION_LIMITColumn, TDSExemption);

                            dataManager.Parameters.Add(this.AppSchema.DutyTax.SUR_RATE_POLICY_IDColumn, TDSPolicyId, true);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.SUR_RATEColumn, SurRate);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.SUR_EXEMPTIONColumn, SurExemption);

                            dataManager.Parameters.Add(this.AppSchema.DutyTax.ED_CESS_RATE_POLICY_IDColumn, TDSPolicyId, true);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.ED_CESS_RATEColumn, EdCessRate);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.ED_CESS_EXEMPTIONColumn, EdCessExemption);

                            dataManager.Parameters.Add(this.AppSchema.DutyTax.SEC_ED_CESS_POLICY_IDColumn, TDSPolicyId, true);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.SEC_ED_CESS_RATEColumn, SecEdCessRate);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.SEC_ED_CESS_EXEMPTIONColumn, SecEdCessExemption);

                            dataManager.Parameters.Add(AppSchema.DutyTax.TDS_WITHOUT_PANT_POLICY_IDColumn, TDSPolicyId, true);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.TDSRATE_WITHOUT_PANColumn, TDSRateWithoutPan);
                            dataManager.Parameters.Add(this.AppSchema.DutyTax.TDSEXEMPTION_LIMIT_WITHOUT_PANColumn, TDSExemptionWithoutPan);
                            resultArgs = dataManager.UpdateData();
                        }
                    }
                }
            }
        }

        public ResultArgs FetchDeducteeTaxDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxFetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_DEDUCTEE_TYPE_IDColumn, DeducteeTypeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataTable NOP()
        {
            using (Bosco.DAO.Data.DataManager tax = new Bosco.DAO.Data.DataManager(SQLCommand.NatureofPayments.FetchNatureofPayments))
            {
                resultArgs = tax.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public ResultArgs FillDutyTaxProperties()
        {
            resultArgs = FetchDutyTaxById(TaxPolicyId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DeducteeTypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DutyTax.TDS_DEDUCTEE_TYPE_IDColumn.ColumnName].ToString());
                NaturePaymentId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DutyTax.TDS_NATURE_PAYMENT_IDColumn.ColumnName].ToString());
                ApplicableFrom = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DutyTax.APPLICABLE_FROMColumn.ColumnName].ToString(), false);
            }
            return resultArgs;
        }

        public ResultArgs DeleteTaxDetails()
        {
            resultArgs = DeleteTaxRateDetails();
            if (resultArgs.Success)
            {
                resultArgs = DeleteTaxPolicyDetails();
            }
            return resultArgs;
        }

        private ResultArgs DeleteTaxPolicyDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxDelete))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TaxPolicyId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FetchDutyTaxById(int TaxId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxFetchById))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TaxId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveDeducteeTaxDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TaxPolicyId, true);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_DEDUCTEE_TYPE_IDColumn, TaxPolicyId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_NATURE_PAYMENT_IDColumn, NaturePaymentId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, ApplicableFrom);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Tax Rate Methods
        public ResultArgs FetchTaxRateById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.FetchTaxRateById))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TaxPolicyId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveTaxRateDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxRateAdd))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TaxPolicyId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_RATEColumn, Rate);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_EXEMPTION_LIMITColumn, ExemptionLimit);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_TAX_TYPE_IDColumn, TaxTypeId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveTaxRateDetailsByType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxRateAddByTaxTypeId))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TaxPolicyId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_RATEColumn, Rate);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_EXEMPTION_LIMITColumn, ExemptionLimit);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_TAX_TYPE_IDColumn, TaxTypeId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteTaxRateDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeTax.DutyTaxRateDelete))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.TDS_POLICY_IDColumn, TaxPolicyId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Pending Transaction

        public ResultArgs FetchPendingTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.FetchPendingTransaction))
            {
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeTypeId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, PartyPaymentId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.NATURE_OF_PAYMENT_IDColumn, NaturePaymentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPendingTransactionForPartyment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.FetchPendingPartyPayment))
            {
                dataManager.Parameters.Add(AppSchema.TDSBooking.BOOKING_DATEColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, PartyPaymentId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
        #endregion
    }
}
