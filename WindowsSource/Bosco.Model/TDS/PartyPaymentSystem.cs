using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Transaction;

namespace Bosco.Model.TDS
{
    public class PartyPaymentSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int IsAutomaticVoucherId { get; set; }
        public int VoucherId { get; set; }
        public int ProjectId { get; set; }
        public int VoucherNo { get; set; }
        public int PartyLedgerId { get; set; }
        public int PaymentLedgerId { get; set; }
        public int PartyPaymentId { get; set; }
        public int PartyId { get; set; }
        public int Amount { get; set; }
        private bool DefaultDelteMode = true;
        public bool IsPhysicalDelete { set { DefaultDelteMode = value; } get { return DefaultDelteMode; } }
        public string Narration { get; set; }
        public DateTime VocherDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DataTable dtPatyPayment { get; set; }
        public DataTable dtPaymentCash { get; set; }
        public DataTable dtDeductionDetails { get; set; }
        #endregion

        #region Constructor

        public PartyPaymentSystem()
        {

        }
        //public PartyPaymentSystem(bool IsEditMode)
        //{
        //    this.IsEditMode = IsEditMode;
        //}
        #endregion

        #region Party Payments

        #region Fetch Party Payments
        public DataSet FetchPartyPayment()
        {
            string ParyPaymentId = string.Empty;
            DataSet dsParyPayment = new DataSet();
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.FetchAllPartyPayment))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_DATEColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsParyPayment.Tables.Add(resultArgs.DataSource.Table);
                    foreach (DataRow drVoucher in resultArgs.DataSource.Table.Rows)
                    {
                        ParyPaymentId += drVoucher[this.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName].ToString() + ",";
                    }
                    ParyPaymentId = ParyPaymentId.TrimEnd(',');
                    // ParyPaymentId = GetPartyPaymentId(NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["PARTY_LEDGER_ID"].ToString()));

                    resultArgs = FetchPartyTrans(ParyPaymentId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSLedger";
                        dsParyPayment.Tables.Add(resultArgs.DataSource.Table);
                        dsParyPayment.Relations.Add(dsParyPayment.Tables[1].TableName, dsParyPayment.Tables[0].Columns[this.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName], dsParyPayment.Tables[1].Columns[this.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn.ColumnName]);
                    }
                }
            }
            return dsParyPayment;
        }

        private ResultArgs FetchPartyTrans(string PaymentId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.FetchPaymentByParyPaymentId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.IDsColumn, PaymentId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private string GetPartyPaymentId(int Projectid, int PartyLedger)
        {
            string PartyPaymentId = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.GetPartyPaymentId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSPartyPayment.PARTY_LEDGER_IDColumn, PartyLedger);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                PartyPaymentId = resultArgs.DataSource.Sclar.ToString;
            }
            return PartyPaymentId;
        }

        public ResultArgs FetchPartyPaymentId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.FetchPartyPaymentId))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region Party Payment Edit
        public ResultArgs FetchPendingTransactionForPartyment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.Update))
            {
                //dataManager.Parameters.Add(AppSchema.TDSPartyPaymentDetail.PARTY_PAYMENT_IDColumn, PartyId);
                //dataManager.Parameters.Add(AppSchema.TDSBooking.BOOKING_DATEColumn, VocherDate);
                //dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, PartyPaymentId);
                //dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Saving Paryt Payments
        public ResultArgs SavePartyPayments()
        {
            using (DataManager dataManager = new DataManager())
            {
                if (VoucherId > 0 && PartyPaymentId > 0)
                {
                    resultArgs = DeleteParyPayment();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = SaveTDSPartyPayment();
                    }
                }
                else
                {
                    resultArgs = SaveTDSPartyPayment();
                }
            }
            return resultArgs;
        }

        private ResultArgs SaveTDSPartyPayment()
        {
            resultArgs = SaveTDSPartyPaymentMaster();
            if (resultArgs != null && resultArgs.Success)
            {
                resultArgs = SaveTDSPartyPaymentDetails();
            }
            return resultArgs;
        }

        private ResultArgs SaveTDSPartyPaymentMaster()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.Add))
                {
                    dataManager.Parameters.Add(AppSchema.TDSPartyPayment.PAYMENT_DATEColumn, VocherDate);
                    dataManager.Parameters.Add(AppSchema.TDSPartyPayment.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, PartyLedgerId);
                    dataManager.Parameters.Add(AppSchema.TDSPartyPayment.PAYMENT_LEDGER_IDColumn, PaymentLedgerId);
                    dataManager.Parameters.Add(AppSchema.TDSPartyPayment.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManager.UpdateData();
                    PartyPaymentId = NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }

        private ResultArgs SaveTDSPartyPaymentDetails()
        {
            try
            {
                dtDeductionDetails = this.TDSPartyPayment;
                if (dtDeductionDetails != null)
                {
                    int IsAdvancePaid = 0, IsAvanceAdjusted = 0;
                    DataTable dtGroupByVoucherNo = dtDeductionDetails.AsEnumerable().GroupBy(r => r.Field<String>(AppSchema.VoucherMaster.VOUCHER_NOColumn.Caption)).Select(g => g.First()).CopyToDataTable();
                    decimal DebitAmount = NumberSet.ToDecimal(dtDeductionDetails.Compute("SUM(AMOUNT)", "TRANS_MODE='DR'").ToString());
                    decimal CreditAmount = NumberSet.ToDecimal(dtDeductionDetails.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                    decimal PayableAmount = NumberSet.ToDecimal(dtDeductionDetails.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                    decimal Amount = (DebitAmount - PayableAmount);

                    decimal PayAmount = Amount > 0 ? (DebitAmount - PayableAmount) + CreditAmount : CreditAmount;
                    DataRow[] drBookingId = dtDeductionDetails.Select("BOOKING_DETAIL_ID>0");
                    int BookingDetailId = drBookingId.Count() > 0 ? NumberSet.ToInteger(drBookingId[0]["BOOKING_DETAIL_ID"].ToString()) : 0;
                    DataRow[] drDeductionId = dtDeductionDetails.Select("DEDUCTION_DETAIL_ID>0");
                    int DeductionDetailId = drDeductionId.Count() > 0 ? NumberSet.ToInteger(drDeductionId[0]["DEDUCTION_DETAIL_ID"].ToString()) : 0;

                    using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPaymentDetail.Add))
                    {
                        dataManager.Parameters.Add(AppSchema.TDSPartyPaymentDetail.PARTY_PAYMENT_IDColumn, PartyPaymentId);
                        dataManager.Parameters.Add(AppSchema.TDSPartyPaymentDetail.BOOKING_DETAIL_IDColumn, BookingDetailId);
                        dataManager.Parameters.Add(AppSchema.TDSPartyPaymentDetail.DEDUCTION_DETAIL_IDColumn, DeductionDetailId);
                        dataManager.Parameters.Add(AppSchema.TDSPartyPaymentDetail.PAID_AMOUNTColumn, PayAmount);
                        dataManager.Parameters.Add(AppSchema.TDSPartyPaymentDetail.IS_ADVANCE_PAIDColumn, IsAdvancePaid);
                        dataManager.Parameters.Add(AppSchema.TDSPartyPaymentDetail.IS_ADVANCE_ADJUSTEDColumn, IsAvanceAdjusted);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
            return resultArgs;
        }
        #endregion

        #region Delete Pary Payments
        public ResultArgs DeletePartyPaymentTrans()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = DeleteParyPayment();
            }
            return resultArgs;
        }

        private ResultArgs DeleteParyPayment()
        {
            using (DataManager dataManager = new DataManager())
            {
                IsPhysicalDelete = false;
                if (IsPhysicalDelete)
                {
                    resultArgs = DeletePartyTransPhysicaly();
                }
                else
                {
                    resultArgs = DeletePartyTransLogicaly();
                }
            }
            return resultArgs;
        }

        private ResultArgs DeletePartyTransLogicaly()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.LogicalDelete))
            {
                //dataManager.Parameters.Add(AppSchema.TDSPartyPayment.PAYMENT_LEDGER_IDColumn, PartyPaymentId);
                dataManager.Parameters.Add(AppSchema.TDSPartyPayment.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeletePartyTransPhysicaly()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPayment.PhysicalDelete))
            {
                dataManager.Parameters.Add(AppSchema.TDSPartyPayment.PAYMENT_LEDGER_IDColumn, PartyPaymentId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int CheckPartyPaymentById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPaymentDetail.CheckPartyPayment))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int IsPartyVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPaymentDetail.CheckIsPartyVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int IsTDSPaymentVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPaymentDetail.CheckIsTDSPaymentVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int IsTDSBookingVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPartyPaymentDetail.CheckIsTDSBookingVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion

        #endregion
    }
}
