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
    public class TDSPaymentSystem : SystemBase
    {
        #region Constructor
        public TDSPaymentSystem()
        {
        }
        #endregion

        #region Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region TDS Payment Properties
        public int TDSPaymentId { get; set; }
        public int VoucherId { get; set; }
        public int ProjectId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PartyLedgerId { get; set; }
        public int IsVoucherType { get; set; }
        public string VoucherNo { get; set; }
        public string Narration { get; set; }
        public int LedgerId { get; set; }
        public int Flag { get; set; }
        #endregion


        #region TDS Payment Detail Properties
        public int TDSPaymentDetailId { get; set; }
        public int DeductionDetailId { get; set; }
        public decimal PaidAmount { get; set; }
        public int IsAdvancedPaid { get; set; }
        public int IsAdvanceAdjusted { get; set; }
        public DataTable TDSPaymentDetail { get; set; }
        public DataTable CashBankDetail { get; set; }
        #endregion

        #region Common Methods
        public ResultArgs SaveTDS()
        {
            using (DataManager dataManager = new DataManager())
            {
                if (TDSPaymentId > (int)ValueGreaterType.GreaterorLess && VoucherId > (int)ValueGreaterType.GreaterorLess)
                {
                    resultArgs = DeleteTDSPaymentAtEdit();
                    if (resultArgs != null && !resultArgs.Success)
                    {
                        return resultArgs;
                    }
                    TDSPaymentId = 0;
                }
                resultArgs = SaveTDSPayment();
                if (resultArgs != null && resultArgs.Success && TDSPaymentId > (int)ValueGreaterType.GreaterorLess)
                {
                    resultArgs = SaveTDSPaymentDetail();
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteTDS()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = DeleteTDSPayment();
            }
            return resultArgs;
        }

        public DataTable FetchTDSPendingPayment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.FetchPendingTDSPayment))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSPayment.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_DATEColumn, BookingDate);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, PartyLedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Save Voucher Details in the following Tables (Voucher_Master_trans,Voucher_Trans,Ledger_Balance)
        /// </summary>
        /// <returns>ResultArg.Success It means that Record are saved in </returns>
        private ResultArgs SaveVoucherDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager())
                {
                    using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
                    {
                        voucherTransSystem.VoucherId = VoucherId;
                        voucherTransSystem.ProjectId = ProjectId;
                        voucherTransSystem.VoucherDate = BookingDate;
                        voucherTransSystem.TransVoucherMethod = IsVoucherType;
                        voucherTransSystem.VoucherType = VoucherSubTypes.PY.ToString();
                        voucherTransSystem.VoucherSubType = VoucherSubTypes.TDS.ToString();
                        voucherTransSystem.Narration = Narration;
                        voucherTransSystem.CreatedBy = this.NumberSet.ToInteger(LoginUserId);
                        voucherTransSystem.ModifiedBy = this.NumberSet.ToInteger(LoginUserId);
                        voucherTransSystem.CreatedOn = this.DateSet.ToDate(DateTime.Now.ToShortDateString(), false);
                        voucherTransSystem.dtTransInfo = TDSPaymentDetail;
                        voucherTransSystem.Status = (int)YesNo.Yes;
                        this.TransInfo = TDSPaymentDetail.DefaultView;
                        this.CashTransInfo = CashBankDetail.DefaultView;
                        resultArgs = voucherTransSystem.SaveVoucherDetails(dataManager);
                        VoucherId = VoucherId.Equals((int)YesNo.No) && resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : VoucherId > (int)YesNo.No ? voucherTransSystem.FDVoucherId : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs;
        }
        #endregion

        #region TDS Payment Methods
        private ResultArgs SaveTDSPayment()
        {
            TDSPaymentId = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.Add))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSPartyPayment.PAYMENT_DATEColumn, BookingDate);
                dataManager.Parameters.Add(this.AppSchema.TDSPartyPayment.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSPartyPayment.PAYMENT_LEDGER_IDColumn, PartyLedgerId);
                dataManager.Parameters.Add(this.AppSchema.TDSPartyPayment.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs != null && resultArgs.Success && NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) > 0)
                {
                    TDSPaymentId = resultArgs.RowUniqueId != null && NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) > 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0;
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchTDSPayment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataTable FetchTDSPaymentById(bool IsTDSEnabledLedger)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn, TDSPaymentId);
                dataManager.Parameters.Add(this.AppSchema.TDSPayment.PROJECT_IDColumn, ProjectId);

                if (IsTDSEnabledLedger)
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.STATUSColumn, 1);
                }


                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable FetchTDSInterestPenaltyDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.FetchTDSInterest))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn, TDSPaymentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public DataSet FetchTDSPaymentTrans()
        {
            string VoucherId = string.Empty;
            DataSet dsTDSPayment = new DataSet();
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.FetchTDSPayment))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_DATEColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > (int)ValueGreaterType.GreaterorLess)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsTDSPayment.Tables.Add(resultArgs.DataSource.Table);
                    foreach (DataRow drVoucher in resultArgs.DataSource.Table.Rows)
                    {
                        VoucherId += drVoucher[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString() + ",";
                    }
                    VoucherId = VoucherId.TrimEnd(',');
                    resultArgs = FetchTDSTrans(VoucherId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDSLedger";
                        dsTDSPayment.Tables.Add(resultArgs.DataSource.Table);
                        dsTDSPayment.Relations.Add(dsTDSPayment.Tables[1].TableName, dsTDSPayment.Tables[0].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName], dsTDSPayment.Tables[1].Columns[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName]);
                    }
                }
            }
            return dsTDSPayment;
        }

        private ResultArgs FetchTDSTrans(string voucherId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.FetchTDSPaymentDetail))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, voucherId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs DeleteTDSPayment()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn, TDSPaymentId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeletePaymentTDSAtEdit(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.DeleteTDSPayment))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.TDSPayment.TDS_PAYMENT_IDColumn, TDSPaymentId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteTDSPaymentAtEdit()
        {
            using (DataManager dataManager = new DataManager())
            {
                using (VoucherTransactionSystem VoucherSystem = new VoucherTransactionSystem())
                {
                    resultArgs = DeletePaymentTDSAtEdit(dataManager);
                    if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        resultArgs = DeleteTDSPaymentDetail(dataManager);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchTDSPaymentId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.FetchPaymentId))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTDSBookingMappedPaymentId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayment.FetchBookingMappedPaymentId))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion

        #region TDS Payment Detail Methods
        private ResultArgs SaveTDSPaymentDetail()
        {
            if (TDSPaymentDetail != null && TDSPaymentDetail.Rows.Count > (int)ValueGreaterType.GreaterorLess)
            {
                foreach (DataRow drTDSPayment in TDSPaymentDetail.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.TDSPayemtDetail.Add))
                    {
                        dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.TDS_PAYMENT_IDColumn, TDSPaymentId);
                        DeductionDetailId = drTDSPayment[this.AppSchema.TDSPaymentDetail.DEDUCTION_DETAIL_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(drTDSPayment[this.AppSchema.TDSPaymentDetail.DEDUCTION_DETAIL_IDColumn.ColumnName].ToString()) : 0;
                        dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.DEDUCTION_DETAIL_IDColumn, DeductionDetailId);
                        PaidAmount = drTDSPayment[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToDecimal(drTDSPayment[this.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) : 0;
                        dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.PAID_AMOUNTColumn, PaidAmount);
                        LedgerId = drTDSPayment[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(drTDSPayment[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        Flag = drTDSPayment[this.AppSchema.TDSPaymentDetail.FLAGColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(drTDSPayment[this.AppSchema.TDSPaymentDetail.FLAGColumn.ColumnName].ToString()) : 0;
                        dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.FLAGColumn, Flag);
                        dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.IS_ADVANCE_PAIDColumn, IsAdvancedPaid);
                        dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.IS_ADVANCE_ADJUSTEDColumn, IsAdvanceAdjusted);
                        if (DeductionDetailId > 0)
                        {
                            resultArgs = dataManager.UpdateData();
                            if (resultArgs != null && resultArgs.Success && NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) > 0)
                            {
                                TDSPaymentDetailId = resultArgs.RowUniqueId != null && NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) > 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0;
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchTDSPaymentDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayemtDetail.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private DataTable FetchTDSPaymentDetailById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayemtDetail.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.TDS_PAYMENT_DETAIL_IDColumn, TDSPaymentDetailId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        private ResultArgs DeleteTDSPaymentDetail(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayemtDetail.Delete))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.TDSPaymentDetail.TDS_PAYMENT_IDColumn, TDSPaymentId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int CheckTDSPaymentById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayemtDetail.CheckTDSPayment))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int HasTDSVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayemtDetail.HasTDSVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int HasTDSLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSPayemtDetail.HasTDSLedger))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
