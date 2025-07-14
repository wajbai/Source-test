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
    public class TDSDeductionSystem : SystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public TDSDeductionSystem()
        {

        }

        public TDSDeductionSystem(int DeductionId)
        {
            this.DeductionId = DeductionId;
            FillDeductionProperties();
        }
        #endregion

        #region Deduction Properties
        public DateTime DeductionDate { get; set; }
        public int ProjectId { get; set; }
        public int PartyLedgerId { get; set; }
        public decimal Amount { get; set; }
        public int VoucherId { get; set; }
        public DateTime ApplicableFrom { get; set; }
        #endregion

        #region Deduction Detail Properties
        public int DeductionDetailId { get; set; }
        public int DeductionId { get; set; }
        public int BookingDetailId { get; set; }
        public int BookingId { get; set; }
        public int DeducteeTypeId { get; set; }
        public int TaxLedgerId { get; set; }
        public decimal TaxAmount { get; set; }
        public DataTable dtDeductionDetails { get; set; }
        #endregion

        #region Deduction Methods
        public ResultArgs SaveTDSDeductionLater()
        {
            using (DataManager dataManager = new DataManager())
            {
                if (VoucherId > 0 && BookingId > 0)
                {
                    resultArgs = UpdateIsTDSDeducted();
                }
                resultArgs = SaveDeductionLater();
                if (resultArgs != null && resultArgs.Success)
                {
                    resultArgs = PendingDeductionComplete();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Insert / Update TDS Deduction Based on DEDUCTION_ID 
        /// If DEDUCTION_ID Equals to Zero Then Insert If Not Update
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveDeduction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.Add))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.DEDUCTION_IDColumn, DeductionId, true);
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.DEDUCTION_DATEColumn, DeductionDate);
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.PARTY_LEDGER_IDColumn, PartyLedgerId);
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Insert / Update TDS Deduction Based on DEDUCTION_ID 
        /// If DEDUCTION_ID Equals to Zero Then Insert If Not Update
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveDeductionLater()
        {
            foreach (DataRow dr in dtDeductionDetails.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.Add))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.DEDUCTION_IDColumn, DeductionId, true);
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.DEDUCTION_DATEColumn, DeductionDate);
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.PROJECT_IDColumn, ProjectId);
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.PARTY_LEDGER_IDColumn, PartyLedgerId);
                    decimal AssessAmount = dr[this.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToDecimal(dr[this.AppSchema.TDSBookingDetails.ASSESS_AMOUNTColumn.ColumnName].ToString()) : 0;
                    decimal TDSTaxAmount = dr["BALANCE"] != DBNull.Value ? this.NumberSet.ToDecimal(dr["BALANCE"].ToString()) : 0;
                    Amount = AssessAmount - TDSTaxAmount;
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.AMOUNTColumn, Amount);
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        BookingDetailId = dr[this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[this.AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn.ColumnName].ToString()) : 0;
                        TaxLedgerId = dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                        TaxAmount = TDSTaxAmount;
                        DeductionId = resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0;
                        resultArgs = SaveDeductionDetail();
                        if (!resultArgs.Success)
                            break;
                    }
                    else { break; }
                }
            }
            return resultArgs;
        }

        private ResultArgs PendingDeductionComplete()
        {
            foreach (DataRow dr in dtDeductionDetails.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TDSBookingDetail.UpdateTaxDeductStatus))
                {
                    dataManager.Parameters.Add(AppSchema.TDSBookingDetails.IS_TDS_DEDUCTEDColumn, 1);
                    dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.BOOKING_DETAIL_IDColumn, NumberSet.ToInteger(dr[AppSchema.TDSBookingDetails.BOOKING_DETAIL_IDColumn.ColumnName].ToString()));
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }
        /// <summary>
        /// Delete Deduction Based on Deduction ID
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteDeduction()
        {
            if (resultArgs != null && resultArgs.Success && DeductionId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.Delete))
                {
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.VOUCHER_IDColumn, VoucherId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        public ResultArgs UpdateIsTDSDeducted()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.UpdateIsTDSDeductable))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Assign Value sto Deduction Properties Based on Deduction ID
        /// </summary>
        /// <returns></returns>
        public ResultArgs FillDeductionProperties()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = FetchDeductionById();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > (int)YesNo.No)
                {
                    DeductionId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.DEDUCTION_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.DEDUCTION_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    DeductionDate = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.DEDUCTION_DATEColumn.ColumnName] != DBNull.Value ? DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.DEDUCTION_DATEColumn.ColumnName].ToString(), false) : DateTime.MinValue;
                    ProjectId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.PROJECT_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.PROJECT_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    PartyLedgerId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.PARTY_LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.PARTY_LEDGER_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    Amount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.AMOUNTColumn.ColumnName].ToString()) : (int)YesNo.No;
                    VoucherId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.VOUCHER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeduction.VOUCHER_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch All Deduction Details 
        /// </summary>
        /// <returns></returns>
        public DataTable FetchDeduction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Fetch Deduction Details By Deduction ID
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchDeductionById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.DEDUCTION_IDColumn, DeductionId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchDeductionId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.FetchDeductionId))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSDeduction.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public DataTable FetchByBooking()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeduction.FetchByBooking))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.BOOKING_IDColumn, BookingId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.TDSBooking.DEDUCTEE_TYPE_IDColumn, DeducteeTypeId);
                dataManager.Parameters.Add(this.AppSchema.DutyTax.APPLICABLE_FROMColumn, ApplicableFrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }
        #endregion

        #region Deduction Detail Methods
        /// <summary>
        /// Insert / Update TDS Deductoin Details Based on  DEDUCTION_DETAIL_ID
        /// If DEDUCTION_DETAIL_ID equals to Zero then [Insert] If Not [Update]
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveDeductionDetail()
        {
            using (DataManager dataManager = new DataManager(DeductionDetailId.Equals((int)YesNo.No) ? SQLCommand.TDSDeductionDetail.Add : SQLCommand.TDSDeductionDetail.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.DEDUCTION_DETAIL_IDColumn, DeductionDetailId);
                dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.DEDUCTION_IDColumn, DeductionId);
                dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.BOOKING_DETAIL_IDColumn, BookingDetailId);
                dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.TAX_AMOUNTColumn, TaxAmount);
                dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.TAX_LEDGER_IDColumn, TaxLedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete TDS Deduction Based on DEDUCTION ID
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteDeductionDetail()
        {
            if (DeductionId > 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.TDSDeductionDetail.Delete))
                {
                    dataManager.Parameters.Add(this.AppSchema.TDSDeduction.DEDUCTION_IDColumn, DeductionId);
                    resultArgs = dataManager.UpdateData();
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Assign Values to Deduction Properties While Editing
        /// </summary>
        /// <returns></returns>
        public ResultArgs FillDeductionDetailProperties()
        {
            using (DataManager dataManager = new DataManager())
            {
                resultArgs = FetchDeductionDetailById();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > (int)YesNo.No)
                {
                    DeductionDetailId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.DEDUCTION_DETAIL_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.DEDUCTION_DETAIL_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    DeductionId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.DEDUCTION_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.DEDUCTION_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    BookingDetailId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.BOOKING_DETAIL_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.BOOKING_DETAIL_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    TaxLedgerId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.TAX_LEDGER_IDColumn.ColumnName] != DBNull.Value ? NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.TAX_LEDGER_IDColumn.ColumnName].ToString()) : (int)YesNo.No;
                    TaxAmount = resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.TAX_AMOUNTColumn.ColumnName] != DBNull.Value ? NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.TDSDeductionDetails.TAX_AMOUNTColumn.ColumnName].ToString()) : (int)YesNo.No;
                }
            }
            return resultArgs;
        }

        /// <summary>
        ///  Fetch All Deduction Details
        /// </summary>
        /// <returns></returns>
        public DataTable FetchDeductionDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeductionDetail.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Fetch Deduction Details Based on DEDUCTION_DETAILS_ID
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchDeductionDetailById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSDeductionDetail.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.DEDUCTION_DETAIL_IDColumn, DeductionDetailId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int HasDeduction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDSBooking.CheckHasDeduction))
            {
                dataManager.Parameters.Add(this.AppSchema.TDSDeductionDetails.TAX_LEDGER_IDColumn, TaxLedgerId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        #endregion
    }
}
