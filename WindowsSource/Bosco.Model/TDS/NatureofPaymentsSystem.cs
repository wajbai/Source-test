using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.TDS;
using Bosco.Model.Business;

namespace Bosco.Model.TDS
{
    public class NatureofPaymentsSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        public TaxPolicyId taxPolicyId;
        #endregion

        #region Constructor
        public NatureofPaymentsSystem()
        {
        }
        public NatureofPaymentsSystem(int NatureofPaymentId)
        {
            this.NatureofPaymentId = NatureofPaymentId;
            FillNatureofPaymentProperties();
        }
        #endregion

        #region Properties
        public int NatureofPaymentId { get; set; }
        public string PaymentName { get; set; }
        public string PaymentCode { get; set; }
        public string Notes { get; set; }
        public int TdsSectionID { get; set; }
        public int DeduteeTypeId { get; set; }
        public int IsActive { get; set; }
        public DateTime ApplicableFrom { get; set; }
        #endregion

        #region Methods
        private void FillNatureofPaymentProperties()
        {
            resultArgs = FetchPaymentById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                PaymentName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.NatureofPayment.PAYMENT_NAMEColumn.ColumnName].ToString();
                PaymentCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.NatureofPayment.PAYMENT_CODEColumn.ColumnName].ToString();
                TdsSectionID = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.NatureofPayment.TDS_SECTION_IDColumn.ColumnName].ToString());
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.NatureofPayment.NOTESColumn.ColumnName].ToString();
                IsActive = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString());

            }
        }

        private ResultArgs FetchPaymentById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATUREOFPAYMENTS_IDColumn, NatureofPaymentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNatureofPayments()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNatureofPaymentsSections()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.NatureofPayments.FetchNatureofPaymentsSection))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SavePaymentDetails()
        {
            using (DataManager dataManager = new DataManager(NatureofPaymentId == 0 ? SQLCommand.NatureofPayments.Add : SQLCommand.NatureofPayments.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATUREOFPAYMENTS_IDColumn, NatureofPaymentId);
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.PAYMENT_CODEColumn, PaymentCode);
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.PAYMENT_NAMEColumn, PaymentName);
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.TDS_SECTION_IDColumn, TdsSectionID);
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.STATUSColumn, IsActive);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteNatureofpaymentDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.NatureofPayments.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.NatureofPayment.NATUREOFPAYMENTS_IDColumn, NatureofPaymentId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;

        }

        public ResultArgs FetchSectionCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.FetchSectionCodes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTaxRateByNatureOfPayment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.FetchTaxRate))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn, NatureofPaymentId);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeduteeTypeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTDSWithoutPAN()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.FetchTDSWithoutPAN))
            {
                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, ApplicableFrom);
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn, NatureofPaymentId);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeduteeTypeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTDSLedgerByNOP()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.FetchTDSLedger))
            {
                //  dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn, NatureofPaymentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNatureofPaymentWithCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.FetchNatureOfPaymentWithCode))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int IsActiveNOP()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.NatureofPayments.IsActiveNOP))
            {
                dataManager.Parameters.Add(this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn, NatureofPaymentId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
