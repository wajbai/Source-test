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
    public class TDSBookingSystem : SystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        private int Count = 0;
        string Booking_Id = string.Empty;
        public TDSTransType tdsTransType;
        #endregion

        #region Booking Properties
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int ProjectId { get; set; }
        public int ExpenseLedgerId { get; set; }
        public int PartyLedgerId { get; set; }
        public double Amount { get; set; }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public int DeducteeId { get; set; }
        public DataTable dtEditBookingTrans { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DataSet dsTDSBooking { get; set; }
        public int isDeductedAlready { get; set; }
        public double isDeductedAmount { get; set; }
        public Dictionary<int, double> TDSAmountDic = new Dictionary<int, double>();
        public DataTable dtVoucherMasterTrans { get; set; }

        //-------------------------------------------------------------------------------------//
        public decimal TDSDeductedAmount { get; set; }
        public string TransVoucherMethod { get; set; }
        public string VoucherType { get; set; }
        public string VoucherSubType { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedOn { get; set; }
        public DataTable dtTransInfo { get; set; }
        public int isTransVoucherMethod { get; set; }
        public bool hasTDSBookingTrans = true;

        #endregion

        #region Booking Detail Properties
        public int BookingDetailId { get; set; }
        public int NatureOfPaymentId { get; set; }
        public decimal AssessAmount { get; set; }
        public int IsTdsDeducted { get; set; }
        public DataTable dtBookingDetail { get; set; }
        public decimal TDSNetPayableAmount { get; set; }

        public int DeductionId { get; set; }
        public int TaxLedgerId { get; set; }
        public decimal TaxAmount { get; set; }
        public string Narration { get; set; }

        #endregion

        #region Constructor
        public TDSBookingSystem()
        {

        }
        public TDSBookingSystem(int BookingId, int VoucherId, int DeducteeTypeId, DateTime dtVoucherDate)
        {
            this.BookingId = BookingId;
            this.VoucherId = VoucherId;
            this.DeducteeId = DeducteeTypeId;
            this.BookingDate = dtVoucherDate;
            FillBookingProperties();
        }
        #endregion

        #region Booking Methods
        public ResultArgs SaveTDSBooking(DataManager dataManager)
        {
            using (DataManager TDSDataManager = new DataManager())
            {
                TDSDataManager.Database = dataManager.Database;
                if (VoucherId > 0 && BookingId > 0)
                {
                    hasTDSBookingTrans = false;
                    resultArgs = DeleteTDSBookingBeforeEdit();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        BookingId = 0;
                    }
                }
                if (VoucherId > 0 && BookingId.Equals(0))
                {
                    resultArgs = SaveBooking();
                    BookingId = 0;
                }
            }
            return resultArgs;
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
                        voucherTransSystem.VoucherNo = VoucherNo;
                        voucherTransSystem.TransVoucherMethod = isTransVoucherMethod;
                        voucherTransSystem.VoucherType = VoucherSubTypes.JN.ToString();
                        voucherTransSystem.VoucherSubType = VoucherSubTypes.TDS.ToString();
                        voucherTransSystem.Narration = Narration;
                        voucherTransSystem.CreatedBy = this.NumberSet.ToInteger(LoginUserId);
                        voucherTransSystem.ModifiedBy = this.NumberSet.ToInteger(LoginUserId);
                        voucherTransSystem.CreatedOn = this.DateSet.ToDate(DateTime.Now.ToShortDateString(), false);
                        voucherTransSystem.dtTransInfo = dtTransInfo;
                        this.TransInfo = dtTransInfo.DefaultView;
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

        /// <summary>
        /// Save Booking Details
        /// </summary>
        /// <returns>ResultArgs.Success It means that the record is saved in TDS_Booking Table If not Error in Inserting or updating the data</returns>
        public ResultArgs SaveBooking()
        {
            using (DataManager dataManager = new DataManager(BookingId.Equals((int)YesNo.No) ? SQLCommand.TDSBooking.Add : SQLCommand.TDSBooking.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_IDColumn, BookingId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_DATEColumn, BookingDate);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.PARTY_LEDGER_IDColumn, PartyLedgerId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.DEDUCTEE_TYPE_IDColumn, DeducteeId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.EXPENSE_LEDGER_IDColumn, ExpenseLedgerId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs != null && resultArgs.Success)
                {
                    BookingId = resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BookingId;
                    if (BookingId > (int)YesNo.No)
                    {
                        resultArgs = SaveBookingDetail();
                    }
                    BookingId = 0;
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteTDSBookingBeforeEdit()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = DeleteBookingBeforeEdit();
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = DeleteBookingDetail(dataManager);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        using (TDSDeductionSystem tdsDeductionSystem = new TDSDeductionSystem())
                        {
                            tdsDeductionSystem.VoucherId = VoucherId;
                            tdsDeductionSystem.DeductionId = tdsDeductionSystem.FetchDeductionId();
                            if (tdsDeductionSystem.DeductionId > 0)
                            {
                                resultArgs = tdsDeductionSystem.DeleteDeduction();
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = tdsDeductionSystem.DeleteDeductionDetail();
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteBookingBeforeEdit()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.DeleteBooking))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_IDColumn, BookingId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete Booking Details based on the Booking ID
        /// </summary>
        /// <returns>If resultArgs.Success Then (It is Delete) Not (Not Delete or Error in Deleting) </returns>
        public ResultArgs DeleteBooking()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_IDColumn, BookingId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private int CheckTDSBooking()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckBookingByVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs FetchBookingDetailByVoucherId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchBookingDetailByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs DeleteTDSDeduction(int DedId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.DeleteDeduction))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.DEDUCTION_IDColumn, DedId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteDeductionByVoucherId()
        {
            resultArgs = FetchBookingDetailByVoucherId();
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.UpdateIsTDSDeductedByBookingDetailId))
                    {
                        dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn, dr[this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn.ColumnName].ToString()) : 0);
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            int DedId = dr[this.AppSchema.TDSDeduction.DEDUCTION_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[this.AppSchema.TDSDeduction.DEDUCTION_IDColumn.ColumnName].ToString()) : 0;
                            resultArgs = DeleteTDSDeduction(DedId);
                            if (resultArgs != null && !resultArgs.Success)
                                break;
                        }
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteBookingByVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.DeleteBookingByVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Assign Values to the properties based on Booking id
        /// </summary>
        /// <returns>If resultArgs.Success and resultArgs.DataSource.Table  is greater than [One] Then Read Values for Table and Assign Values to the Properties
        /// if There is an error while assigning the values to the property then the problem is in conversion Types</returns>
        public ResultArgs FillBookingProperties()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = FetchTDSBooking();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > (int)YesNo.No)
                {
                    dtEditBookingTrans = resultArgs.DataSource.Table;
                    BookingId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.BOOKING_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.BOOKING_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    BookingDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.BOOKING_DATEColumn.ColumnName] != DBNull.Value ? DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.BOOKING_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                    ProjectId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.PROJECT_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.PROJECT_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    PartyLedgerId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.PARTY_LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.PARTY_LEDGER_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    ExpenseLedgerId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.EXPENSE_LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.EXPENSE_LEDGER_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    Amount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.AMOUNTColumn.ColumnName].ToString()) : (int)YesNo.No;
                    VoucherNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString() : string.Empty;
                    Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString() : string.Empty;
                    VoucherId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.VOUCHER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBooking.VOUCHER_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch All the booking details
        /// </summary>
        /// <returns>resultArgs.Success Then it returns Values from Booking Table Without any problem if not (Problem in Query) </returns>
        public DataSet FetchBooking()
        {
            DataSet dsTDSVoucher = new DataSet();
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchTDSMaster))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_DATEColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "Master";
                    dsTDSVoucher.Tables.Add(resultArgs.DataSource.Table);
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        Booking_Id += dr[this.AppSchema.TDSBooking.BOOKING_IDColumn.ColumnName].ToString() + ",";
                    }
                    Booking_Id = Booking_Id.TrimEnd(',');
                    resultArgs = FetchTDSVoucherTrans();

                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "TDS Deduction";
                        dsTDSVoucher.Tables.Add(resultArgs.DataSource.Table);
                        dsTDSVoucher.Relations.Add(dsTDSVoucher.Tables[1].TableName, dsTDSVoucher.Tables[0].Columns[this.AppSchema.TDSBooking.BOOKING_IDColumn.ColumnName], dsTDSVoucher.Tables[1].Columns[this.AppSchema.TDSBooking.BOOKING_IDColumn.ColumnName]);
                    }
                }
            }
            return dsTDSVoucher;
        }

        public ResultArgs FetchTDSVoucherTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchTDSVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_IDColumn, Booking_Id);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Booking Based on Booking ID
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBookingById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_IDColumn, BookingId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.DEDUCTEE_TYPE_IDColumn, DeducteeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete Booking Details Based on (Voucher Id)
        /// </summary>
        /// <returns>resultArgs.Success then the records are deleted from all the tables(Voucher_Master_trans,Voucher_trans,TDS_Booking,TDS_Booking_Detail,TDS_Deduction_TDS_Deduction_Detail)</returns>
        public ResultArgs DeleteTDSBooking()
        {
            using (DataManager dataManager = new DataManager())
            {
                using (VoucherTransactionSystem voucherTrans = new VoucherTransactionSystem())
                {
                    voucherTrans.VoucherId = VoucherId;
                    resultArgs = voucherTrans.RemoveVoucher(dataManager);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        resultArgs = DeleteBooking();
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchTDSBooking()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchTDSBooking))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_IDColumn, BookingId);
                dataManager.Parameters.Add(this.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn, DeducteeId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, BookingDate);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Booking Detail Methods

        /// <summary>
        /// Save Booking Detail in the following Table(TDS_Booking_Detail)
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveBookingDetail()
        {
            if (dtBookingDetail != null && dtBookingDetail.Rows.Count > (int)YesNo.No)
            {
                foreach (DataRow dr in dtBookingDetail.Rows)
                {
                    using (DataManager dataManager = new DataManager(BookingDetailId.Equals((int)YesNo.No) ? SQLCommand.TDSBookingDetail.Add : SQLCommand.TDSBookingDetail.Update))
                    {
                        dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn, BookingDetailId);
                        dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.BOOKING_IDColumn, BookingId);

                        dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.NATURE_OF_PAYMENT_IDColumn, dr[this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn.ColumnName] != null ?
                            this.NumberSet.ToInteger(dr[this.AppSchema.NatureofPayment.NATURE_PAY_IDColumn.ColumnName].ToString()) : (int)YesNo.No);

                        dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn, dr[this.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn.ColumnName] != null ?
                            this.NumberSet.ToDecimal(dr[this.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn.ColumnName].ToString()) : (int)YesNo.No);

                        IsTdsDeducted = dr[this.AppSchema.DeducteeTypes.IdColumn.ColumnName] != null ?
                           this.NumberSet.ToInteger(dr[this.AppSchema.DeducteeTypes.IdColumn.ColumnName].ToString()).Equals((int)YesNo.No) ? (int)YesNo.Yes : (int)YesNo.No : (int)YesNo.No;

                        dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.IS_TDS_DEDUCTEDColumn, IsTdsDeducted);

                        IsTdsDeducted = dr[this.AppSchema.DeducteeTypes.IdColumn.ColumnName] != null ?
                           this.NumberSet.ToInteger(dr[this.AppSchema.DeducteeTypes.IdColumn.ColumnName].ToString()).Equals((int)YesNo.No) ? (int)YesNo.Yes : (int)YesNo.No : (int)YesNo.No;

                        TaxLedgerId = dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != null ?
                            this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : (int)YesNo.No;

                        if (TDSAmountDic.Count > 0)
                        {
                            List<int> LedgerKeys = TDSAmountDic.Select(x => x.Key).ToList();
                            if (LedgerKeys.Count > 0)
                            {
                                TaxAmount = GetTaxLedgerAmount(LedgerKeys[0], hasTDSBookingTrans);
                                TDSNetPayableAmount = GetTaxLedgerAmount(LedgerKeys[1], hasTDSBookingTrans);
                            }
                            //TaxAmount = dr[this.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName] != null ?
                            //    this.NumberSet.ToDecimal(dr[this.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName].ToString()) : (int)YesNo.No;
                        }
                        else
                        {
                            TaxAmount = dr[this.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName] != null ?
                                this.NumberSet.ToDecimal(dr[this.AppSchema.TDSDeductionDetails.TDS_AMOUNTColumn.ColumnName].ToString()) : (int)YesNo.No;
                        }
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > (int)YesNo.No && IsTdsDeducted > (int)YesNo.No)
                        {
                            BookingDetailId = resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : BookingDetailId;
                            if (Count.Equals((int)YesNo.No))
                            {
                                resultArgs = SaveDeduction();
                                Count++;
                            }
                            if (resultArgs != null && resultArgs.Success && BookingDetailId > (int)YesNo.No && DeductionId > (int)YesNo.No)
                            {
                                resultArgs = SaveDeductionDetail();
                            }
                        }
                        else if (resultArgs != null && !resultArgs.Success)
                        {
                            break;
                        }
                        BookingDetailId = 0;
                    }
                }
            }
            return resultArgs;
        }

        private decimal GetTaxLedgerAmount(int TaxLedgerId, bool hasTDSBookingTrans)
        {
            decimal TDSTaxAmount = 0;
            if (dtVoucherMasterTrans != null && dtVoucherMasterTrans.Rows.Count > 0)
            {
                //var TaxLedgerAmount = (from m in dtVoucherMasterTrans.AsEnumerable() where m.Field<string>("LEDGER_ID") == TaxLedgerId.ToString() select m).FirstOrDefault();
                if (!hasTDSBookingTrans)
                {
                    var TaxLedgerAmount = (from m in dtVoucherMasterTrans.AsEnumerable() where m.Field<UInt32>("LEDGER_ID") == TaxLedgerId select m).FirstOrDefault();
                    if (TaxLedgerAmount != null)
                    {
                        TDSTaxAmount = this.NumberSet.ToDecimal(TaxLedgerAmount["CREDIT"].ToString());
                    }
                }
                else
                {
                    var TaxLedgerAmount = (from m in dtVoucherMasterTrans.AsEnumerable() where m.Field<string>("LEDGER_ID") == TaxLedgerId.ToString() select m).FirstOrDefault();
                    if (TaxLedgerAmount != null)
                    {
                        TDSTaxAmount = this.NumberSet.ToDecimal(TaxLedgerAmount["CREDIT"].ToString());
                    }
                }
            }
            return TDSTaxAmount;
        }
        /// <summary>
        /// Delete Booking Details based on the Voucher Id and Booking Detail ID
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteBookingDetail(DataManager dm)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBookingDetail.Delete))
            {
                dataManager.Database = dm.Database;
                dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.BOOKING_IDColumn, BookingId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Fill Properties based on the Booking Details Id
        /// </summary>
        /// <returns></returns>
        public ResultArgs FillBookingDetailProperties()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = FetchBookingDetailById();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > (int)YesNo.No)
                {
                    BookingDetailId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    BookingId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.BOOKING_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.BOOKING_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    NatureOfPaymentId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.NATURE_OF_PAYMENT_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.NATURE_OF_PAYMENT_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    AssessAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn.ColumnName].ToString()) : (int)YesNo.No;
                    IsTdsDeducted = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.IS_TDS_DEDUCTEDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSBookingDetails.IS_TDS_DEDUCTEDColumn.ColumnName].ToString()) : (int)YesNo.No;
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch All Booking Details 
        /// </summary>
        /// <returns></returns>
        public DataTable FetchBookingDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBookingDetail.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Fetch Booking Detail by Booking Details Id
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchBookingDetailById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBookingDetail.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn, BookingDetailId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string FetchLedgerName()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchLedgerName))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, ExpenseLedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data.ToString();
        }

        /// <summary>
        /// Save Deduction in (TDS_Deduction) Table
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveDeduction()
        {
            try
            {
                using (TDSDeductionSystem tdsDeductionSystem = new TDSDeductionSystem())
                {
                    tdsDeductionSystem.ProjectId = ProjectId;
                    tdsDeductionSystem.PartyLedgerId = PartyLedgerId;
                    tdsDeductionSystem.DeductionDate = BookingDate;
                    tdsDeductionSystem.Amount = TDSNetPayableAmount;
                    tdsDeductionSystem.VoucherId = VoucherId;
                    resultArgs = tdsDeductionSystem.SaveDeduction();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DeductionId = tdsDeductionSystem.DeductionId = resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : DeductionId;
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

        /// <summary>
        /// Save Deduction Detail in (TDS_Deduction_Detail)
        /// </summary>
        /// <returns></returns>
        private ResultArgs SaveDeductionDetail()
        {
            try
            {
                using (TDSDeductionSystem tdsDeductionSystem = new TDSDeductionSystem())
                {
                    tdsDeductionSystem.BookingDetailId = BookingDetailId;
                    tdsDeductionSystem.DeductionId = DeductionId;
                    tdsDeductionSystem.TaxLedgerId = TaxLedgerId;
                    tdsDeductionSystem.TaxAmount = TaxAmount;
                    resultArgs = tdsDeductionSystem.SaveDeductionDetail();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return resultArgs;
        }

        public int CheckTDSBookingExists()
        {
            int isExists = 0;
            using (DataManager dataManager = new DataManager())
            {
                using (TDSPaymentSystem tdsPaymentSystem = new TDSPaymentSystem())
                {
                    tdsPaymentSystem.VoucherId = VoucherId;
                    isExists = tdsPaymentSystem.CheckTDSPaymentById();
                    //if (isExists.Equals(0))
                    //{
                    //    //using (PartyPaymentSystem partyPaymentSystem = new PartyPaymentSystem())
                    //    //{
                    //    //    partyPaymentSystem.VoucherId = VoucherId;
                    //    //    isExists = partyPaymentSystem.CheckPartyPaymentById();
                    //    //}
                    //}
                }
            }
            return isExists;
        }

        public int CheckTDSBookingByVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckTDSBookingByID))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public DataTable FetchBookingIdByVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchBookingIdByVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable FetchLedgerDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchLedgerDetailsById))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, ExpenseLedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public int FetchBookingId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchBookingIdByVoucherId))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public bool IsTDSVouchers()
        {
            bool isTDSVoucher = false;
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckIsTDSPaymentVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
                if (resultArgs.DataSource.Sclar.ToInteger > 0)
                {
                    tdsTransType = TDSTransType.TDSPayment;
                    isTDSVoucher = true;
                }
                else
                {
                    if (IsTDSBookingVoucher() > 0)
                    {
                        tdsTransType = TDSTransType.TDSBooking;
                        isTDSVoucher = true;
                    }
                    else
                    {
                        if (IsTDSDeductionVoucher() > 0)
                        {
                            tdsTransType = TDSTransType.TDSBooking;
                            isTDSVoucher = true;
                        }
                    }
                }
            }
            return isTDSVoucher;
        }

        public int IsTDSBookingVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckIsTDSBookingVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int IsTDSDeductionVoucher()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckIsTDSDeductionVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public double getExpenseAmount()
        {
            double ExpAmount = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.ExpenseLedgerAmount))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.EXPENSE_LEDGER_IDColumn, ExpenseLedgerId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success)
                {
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            isDeductedAlready = dr[this.AppSchema.TDSBookingDetails.IS_TDS_DEDUCTEDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(dr[this.AppSchema.TDSBookingDetails.IS_TDS_DEDUCTEDColumn.ColumnName].ToString()) : 0;
                            if (isDeductedAlready == 0)
                            {
                                ExpAmount = dr[this.AppSchema.TDSBooking.AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDouble(dr[this.AppSchema.TDSBooking.AMOUNTColumn.ColumnName].ToString()) : 0;
                            }
                        }
                    }
                    else
                    {
                        isDeductedAlready = 1;
                    }
                }
            }
            return ExpAmount;
        }

        public int CheckVoucherInBooking()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckVoucherInBooking))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int HasTDSBooking()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckHasBooking))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.EXPENSE_LEDGER_IDColumn, ExpenseLedgerId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.PARTY_LEDGER_IDColumn, PartyLedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        public ResultArgs FetchTDSPartyVIDbyBookingVID()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.FetchBookingVIDbyPartyVID))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }

        #endregion
    }
}
