using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Transaction;

namespace Bosco.Model
{
    public class InsuranceRenewSystem : SystemBase
    {
        #region Constructor
        public InsuranceRenewSystem()
        {
        }
        public InsuranceRenewSystem(int InsDetailId, bool IsPurchase = false)
        {
            this.InsDetailId = InsDetailId;
            FillRenewProperties(InsDetailId, IsPurchase);
        }

        public InsuranceRenewSystem(int InsDetailId, int itemId, int projectId)
        {
            this.InsDetailId = InsDetailId;
            this.ProjectId = projectId;
        }

        #endregion

        #region Properties
        ResultArgs resultArgs = new ResultArgs();
        public int InsurancePlanId { get; set; }
        public int InsDetailId { get; set; }
        public int ItemDetailId { get; set; }
        public string PolicyNo { get; set; }
        public DateTime RenewalDate { get; set; }
        public decimal SumInsured { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public int VoucherId { get; set; }
        public int ProjectId { get; set; }
        public string Project { get; set; }
        public DateTime DateAsOn { get; set; }

        public string Narration { get; set; }
        public string VoucherNo { get; set; }

        #endregion
        #region Methods

        public ResultArgs SaveInsurancePlanDetails()
        {
            using (DataManager datamanager = new DataManager((ItemDetailId == 0) ? SQLCommand.AssetRenewInsurance.InsRenewAdd : SQLCommand.AssetRenewInsurance.InsRenewEdit))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, ItemDetailId, true);
                datamanager.Parameters.Add("POLICY_NO", PolicyNo);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveFinanceVoucher(DataTable dtCashLedger, DataManager dmanager = null)
        {
            using (DataManager datamanager = new DataManager())
            {
                if (dmanager != null)
                    datamanager.Database = dmanager.Database;
                else
                    datamanager.BeginTransaction();

                resultArgs = SaveInsuranceFinanceVouchers(datamanager, dtCashLedger);

                if (dmanager == null)
                    datamanager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveInsuranceFinanceVouchers(DataManager dmanager, DataTable DtCashBank)
        {
            if (DtCashBank != null && DtCashBank.Rows.Count > 0)
            {
                DataTable dtBankTrans = ConstructLedgerSource(DtCashBank, true);
                dtBankTrans.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                {
                    if (dr.RowState == DataRowState.Deleted)
                        dr.Delete();
                });
                dtBankTrans.AcceptChanges();

                DataTable dtSource = ConstructLedgerSource(DtCashBank, false);
                dtSource.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                {
                    if (dr.RowState == DataRowState.Deleted)
                        dr.Delete();
                });
                dtSource.AcceptChanges();

                using (AssetInwardOutwardSystem assetInwardOutwardSystem = new AssetInwardOutwardSystem())
                {
                    assetInwardOutwardSystem.InOutDate = RenewalDate;
                    assetInwardOutwardSystem.VoucherNo = VoucherNo;
                    assetInwardOutwardSystem.Narration = Narration;
                    assetInwardOutwardSystem.Flag = AssetInOut.INS.ToString();
                    assetInwardOutwardSystem.ProjectId = ProjectId;
                    assetInwardOutwardSystem.VoucherId = VoucherId;
                    assetInwardOutwardSystem.dtinoutword = dtSource;
                    assetInwardOutwardSystem.dtCashBank = dtBankTrans;
                    resultArgs = assetInwardOutwardSystem.SaveAssetVouchers(dmanager);

                    if (resultArgs.Success)
                    {
                        VoucherId = assetInwardOutwardSystem.VoucherId;
                    }
                }
            }
            return resultArgs;
        }

        private DataTable ConstructLedgerSource(DataTable dtCashbank, bool isCashBank)
        {
            DataTable dtBankTrans = dtCashbank;
            DataView dvcashbank = dtBankTrans.AsDataView();
            if (isCashBank)
            {
                dvcashbank.RowFilter = "GROUP_ID IN (12,13)";
            }
            else
            {
                dvcashbank.RowFilter = "GROUP_ID NOT IN (12,13)";
            }
            dtBankTrans = dvcashbank.ToTable();
            dtBankTrans.AcceptChanges();
            return dtBankTrans;
        }

        /// <summary>
        /// Save Insurance
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveRenewDetails()
        {
            using (DataManager datamanager = new DataManager((InsDetailId == 0) ? SQLCommand.AssetRenewInsurance.InsRenewAdd : SQLCommand.AssetRenewInsurance.InsRenewEdit))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn, InsDetailId, true);
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, ItemDetailId);
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.RENEWAL_DATEColumn, RenewalDate);
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.PERIOD_FROMColumn, PeriodFrom);
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.PERIOD_TOColumn, PeriodTo);
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.SUM_INSUREDColumn, SumInsured);
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.PREMIUM_AMOUNTColumn, PremiumAmount);
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.VOUCHER_IDColumn, VoucherId);
                datamanager.Parameters.Add(this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn, InsurancePlanId);
                datamanager.Parameters.Add(this.AppSchema.ASSETItem.POLICY_NOColumn, PolicyNo);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveInsuranceDetails(DataTable dtinsurance, int VoucherID = 0)
        {
            if (dtinsurance != null && dtinsurance.Rows.Count > 0)
            {
                foreach (DataRow drIns in dtinsurance.Rows)
                {
                    RenewalDate = DateSet.ToDate(drIns[this.AppSchema.AssetInsuranceDetail.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                    PeriodFrom = DateSet.ToDate(drIns[this.AppSchema.AssetInsuranceDetail.PERIOD_FROMColumn.ColumnName].ToString(), false);
                    PeriodTo = DateSet.ToDate(drIns[this.AppSchema.AssetInsuranceDetail.PERIOD_TOColumn.ColumnName].ToString(), false);
                    SumInsured = NumberSet.ToDecimal(drIns[this.AppSchema.AssetInsuranceDetail.SUM_INSUREDColumn.ColumnName].ToString());
                    PremiumAmount = NumberSet.ToDecimal(drIns[this.AppSchema.AssetInsuranceDetail.PREMIUM_AMOUNTColumn.ColumnName].ToString());
                    PolicyNo = drIns["POLICY_NO"].ToString();
                    InsurancePlanId = NumberSet.ToInteger(drIns[this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName].ToString());
                    InsDetailId = NumberSet.ToInteger(drIns[this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn.ColumnName].ToString());
                    VoucherId = VoucherID;
                    resultArgs = SaveRenewDetails();
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveInsuranceVoucherDetails(DataTable dtinsurance, int itemID, int itemdetailID, string Flag)
        {
            resultArgs.Success = true;
            DataTable dtInsuranceVoucherDetails = new DataTable();
            var Inskey = new Tuple<int, int>(itemID, itemdetailID);
            if (dtinsurance != null && dtinsurance.Rows.Count > 0)
            {
                using (DataManager dmanager = new DataManager())
                {
                    foreach (DataRow drIns in dtinsurance.Rows)
                    {
                        RenewalDate = DateSet.ToDate(drIns[this.AppSchema.AssetInsuranceDetail.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                        if (Flag == AssetInOut.PU.ToString() || Flag == AssetInOut.IK.ToString())
                        {
                            if (AssetMultiInsuranceCollection.ContainsKey(Inskey))
                            {
                                dtInsuranceVoucherDetails = AssetMultiInsuranceVoucherCollection[Inskey];

                                resultArgs = SaveFinanceVoucher(dtInsuranceVoucherDetails, dmanager);
                            }
                        }
                        if (resultArgs.Success)
                        {
                            PeriodFrom = DateSet.ToDate(drIns[this.AppSchema.AssetInsuranceDetail.PERIOD_FROMColumn.ColumnName].ToString(), false);
                            PeriodTo = DateSet.ToDate(drIns[this.AppSchema.AssetInsuranceDetail.PERIOD_TOColumn.ColumnName].ToString(), false);
                            SumInsured = NumberSet.ToDecimal(drIns[this.AppSchema.AssetInsuranceDetail.SUM_INSUREDColumn.ColumnName].ToString());
                            PremiumAmount = NumberSet.ToDecimal(drIns[this.AppSchema.AssetInsuranceDetail.PREMIUM_AMOUNTColumn.ColumnName].ToString());
                            PolicyNo = drIns["POLICY_NO"].ToString();
                            InsurancePlanId = NumberSet.ToInteger(drIns[this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName].ToString());
                            InsDetailId = NumberSet.ToInteger(drIns[this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn.ColumnName].ToString());
                            resultArgs = SaveRenewDetails();
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// To Find out whether Insurance Available 
        /// </summary>
        /// <param name="InsItemId"></param>
        /// <returns></returns>
        public int IsInsuranceExists(int InsItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.IsInsuranceMade))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteInsuranceDetails(int InsDetailId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.DeleteDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn, InsDetailId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchInsuranceVoucherIdByInsId(int InsDetailId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchVoucherIdByInsId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn, InsDetailId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteInsuranceVoucherByInsId(int InsDetailId)
        {
            resultArgs = FetchInsuranceVoucherIdByInsId(InsDetailId);
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                int VouchedID = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                    {
                        voucherSystem.VoucherId = VouchedID;
                        voucherSystem.RemoveVoucher(dataManager);
                    }
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteInsuranceVoucherByItemDetailId()
        {
            resultArgs = FetchInsuranceVoucherIdByItemId(ItemDetailId);
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                string VouchedIDs = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString();
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                    {
                        string[] VouchedIDCollection = VouchedIDs.Split(',');
                        foreach (string itemVid in VouchedIDCollection)
                        {
                            voucherSystem.VoucherId = NumberSet.ToInteger(itemVid);
                            voucherSystem.RemoveVoucher(dataManager);
                        }
                    }
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchInsuranceVoucherIdByItemId(int ItemDetailId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchVoucherIdByItemId))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, ItemDetailId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteInsuranceByItemDetailById(int ItemDetailId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.DeleteItemDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, ItemDetailId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchPreviousRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchPreviousRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, ItemDetailId);
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn, InsDetailId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNextRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchNextRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, ItemDetailId);
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn, InsDetailId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRegistrationDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchRegistrationDate))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, ItemDetailId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteInsuranceDetailsByItemDetail(string ItmDetailIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.DeleteInsuranceByDetailId))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDsColumn, ItmDetailIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// Fill the Details
        /// </summary>
        /// <param name="salesId"></param>
        private void FillRenewProperties(int insDetailId, bool ispurchase)
        {
            if (ispurchase)
                resultArgs = LoadHistoryDetailsById(insDetailId.ToString());
            else
                resultArgs = FetchById(insDetailId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ProjectId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                Project = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                RenewalDate = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInsuranceDetail.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                PeriodFrom = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInsuranceDetail.PERIOD_FROMColumn.ColumnName].ToString(), false);
                PeriodTo = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInsuranceDetail.PERIOD_TOColumn.ColumnName].ToString(), false);
                SumInsured = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInsuranceDetail.SUM_INSUREDColumn.ColumnName].ToString());
                PremiumAmount = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetInsuranceDetail.PREMIUM_AMOUNTColumn.ColumnName].ToString());
                PolicyNo = resultArgs.DataSource.Table.Rows[0]["POLICY_NO"].ToString();
                VoucherId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
                InsurancePlanId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName].ToString());
            }
        }

        /// <summary>
        /// Fetch the Id's
        /// </summary>
        /// <param name="salesId"></param>
        /// <returns></returns>
        private ResultArgs FetchById(int InsDetailId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetRenewInsurance.Fetch))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn, InsDetailId);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// Loadrenewal Details
        /// </summary>
        /// <returns></returns>
        public DataSet LoadFetchInsuranceDetails()
        {
            string ItemDetailId = string.Empty;
            DataSet dsInsurance = new DataSet();
            try
            {
                resultArgs = InsuranceMaster();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DataView dvInsurance = resultArgs.DataSource.Table.DefaultView;

                    dvInsurance.ToTable().TableName = "Master";

                    resultArgs.DataSource.Table.TableName = "Master";

                    dsInsurance.Tables.Add(dvInsurance.ToTable());
                    for (int i = 0; i < dvInsurance.ToTable().Rows.Count; i++)
                    {
                        ItemDetailId += dvInsurance.ToTable().Rows[i][AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString() + ",";
                    }
                    ItemDetailId = ItemDetailId.TrimEnd(',');
                    resultArgs = LoadHistoryDetailsById(ItemDetailId);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "History";
                        dsInsurance.Tables.Add(resultArgs.DataSource.Table);
                        dsInsurance.Relations.Add(dsInsurance.Tables[1].TableName, dsInsurance.Tables[0].Columns[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName], dsInsurance.Tables[1].Columns[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName]);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return dsInsurance;
        }

        /// <summary>
        /// Load Insurance Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs LoadInsRenewSystem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchInsRenewDetails))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int LoadRenewalDetailByAssetId(string AssetId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchItemDetailIdbyAssetId))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, AssetId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs LoadHistoryDetailsById(string itemDetailsId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchInsHistoryDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetInsuranceDetail.ITEM_DETAIL_IDColumn, itemDetailsId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs InsuranceMaster()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.FetchInsuranceDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);

                if (DateAsOn != DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.FDRegisters.DATE_FROMColumn, DateAsOn);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchInsurancePlan()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetRenewInsurance.LoadInsurancePLanDetails))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
