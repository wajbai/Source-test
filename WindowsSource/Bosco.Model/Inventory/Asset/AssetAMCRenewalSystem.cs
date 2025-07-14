using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Transaction;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Model.Inventory.Asset
{
    public class AssetAMCRenewalSystem : SystemBase
    {
        #region Variable Declaration
        ResultArgs resultArgs = new ResultArgs();

        #endregion

        #region Property
        public int AMCId { get; set; }
        public int AmcRenewalId { get; set; }
        public string AMCGroup { get; set; }
        public string Provider { get; set; }
        public int ProjectId { get; set; }
        public int ItemDetailId { get; set; }
        public string AssetId { get; set; }
        public string AssetItem { get; set; }
        public string Manufacturer { get; set; }
        public string Location { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateTime AmcFrom { get; set; }
        public DateTime AmcTo { get; set; }
        public DateTime RenewalDate { get; set; }
        public DataTable dtAmcDetails { get; set; }
        public int Mode { get; set; }
        public int VoucherId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int ExpenseLedgerId { get; set; }
        public int CashBankLedgerId { get; set; }
        public string ChequeNo { get; set; }
        public DateTime MaterialisedOn { get; set; }
        public string Narration { get; set; }
        public string VoucherNo { get; set; }
        public string Flag { get; set; }
        public DataView dvTransInfo = null;
        public DataView dvCashTransInfo = null;

        /// <summary>
        /// Set Transaction Info as Dataview
        /// </summary>
        public DataView TransInfo
        {
            set
            {
                dvTransInfo = value;
            }
            get
            {
                return dvTransInfo;
            }
        }

        /// <summary>
        /// Set Transaction Info as Dataview
        /// </summary>
        public DataView CashTransInfo
        {
            set
            {
                dvCashTransInfo = value;
            }
            get
            {
                return dvCashTransInfo;
            }
        }
        #endregion

        #region Constructor
        public AssetAMCRenewalSystem()
        {
        }
        public AssetAMCRenewalSystem(int amcId, int RenewalId)
        {
            this.AMCId = amcId;
            this.AmcRenewalId = RenewalId;
            FillRenewProperties(AMCId, AmcRenewalId);

        }
        #endregion

        #region Methods


        public ResultArgs FetchAMCRenewalMaster()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.FetchAMCRenewalMasterDetails))
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs;
        }

        public ResultArgs SaveAMC()
        {
            using (DataManager dataManager = new DataManager())
            {
                //resultArgs.Success = true;
                dataManager.BeginTransaction();
                //using (AssetInwardOutwardSystem InwardOutward = new AssetInwardOutwardSystem())
                //{
                //    InwardOutward.VoucherId = VoucherId;
                //    InwardOutward.ProjectId = ProjectId;
                //    InwardOutward.Flag = AssetInOut.AMC.ToString();
                //    InwardOutward.NameAddress = Provider;
                //    InwardOutward.Narration = Narration;
                //    InwardOutward.VoucherNo = VoucherNo;
                //    InwardOutward.TransInfo = TransInfo;
                //    InwardOutward.CashTransInfo = CashTransInfo;
                //    //resultArgs = InwardOutward.SaveAssetVouchers(dataManager);
                //    if (resultArgs.Success)
                //        VoucherId = VoucherId == 0 ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : VoucherId;
                //    if (resultArgs.Success)
                //    {
                if (AMCId > 0)
                {
                    if (!(Mode == (int)AssetAmc.Renew || Mode == (int)AssetAmc.Edit))
                        resultArgs = DeleteAMCHistoryDetails();
                    if (resultArgs.Success)
                    {
                        if (!(Mode == (int)AssetAmc.Renew || Mode == (int)AssetAmc.Edit))
                            DeleteAMCMasterDetails();
                        if (resultArgs.Success)
                        {
                            resultArgs = DeleteAMCItemMapping();
                            if (resultArgs.Success && resultArgs.RowsAffected > 0 && VoucherId > 0)
                            {
                                resultArgs = DeleteAMCVoucherDetails(dataManager);
                                //VoucherId = 0;
                            }
                        }
                    }
                }
                if (Mode != (int)AssetAmc.Renew || (Mode == (int)AssetAmc.Edit))
                    resultArgs = SaveAssetAmcMaster();
                if (resultArgs != null && resultArgs.Success)
                {

                    if (Mode != (int)AssetAmc.Renew && Mode != (int)AssetAmc.Edit)
                        AMCId = resultArgs.RowUniqueId != null ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : 0;

                    if (AMCId > 0)
                    {
                        resultArgs = SaveAssetAmcHistory();
                        if (resultArgs.Success)
                        {
                            resultArgs = SaveItemMappingDetails();
                        }
                    }
                }
                //}
                dataManager.EndTransaction();
                //}
            }

            return resultArgs;
        }

        public ResultArgs SaveFinanceVoucher(DataTable dtCashLedger)
        {
            using (DataManager datamanager = new DataManager())
            {
                datamanager.BeginTransaction();
                resultArgs = SaveAmcFinanceVouchers(datamanager, dtCashLedger);
                datamanager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs SaveAmcFinanceVouchers(DataManager dmanager, DataTable DtCashBank)
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
                dtBankTrans.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                {
                    if (dr.RowState == DataRowState.Deleted)
                        dr.Delete();
                });
                dtBankTrans.AcceptChanges();

                using (AssetInwardOutwardSystem assetInwardOutwardSystem = new AssetInwardOutwardSystem())
                {
                    assetInwardOutwardSystem.InOutDate = VoucherDate;
                    assetInwardOutwardSystem.VoucherNo = VoucherNo;
                    assetInwardOutwardSystem.Narration = Narration;
                    assetInwardOutwardSystem.Flag = AssetInOut.AMC.ToString();
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


        public ResultArgs SaveAMCVouchers(DataManager dm)
        {
            try
            {
                //DataView dvTransaction = null;
                resultArgs.Success = true;

                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    voucherTransaction.ProjectId = ProjectId;
                    voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                    voucherTransaction.VoucherType = DefaultVoucherTypes.Payment.ToString();
                    voucherTransaction.VoucherDate = VoucherDate;
                    voucherTransaction.VoucherSubType = VoucherSubTypes.AST.ToString();
                    voucherTransaction.Narration = string.Empty;
                    voucherTransaction.NameAddress = string.Empty;
                    voucherTransaction.DonorId = 0;
                    voucherTransaction.PurposeId = 0;
                    voucherTransaction.CreatedBy = NumberSet.ToInteger(this.LoginUserId);
                    voucherTransaction.ModifiedBy = NumberSet.ToInteger(this.LoginUserId);
                    voucherTransaction.Status = 1;
                    voucherTransaction.VoucherNo = string.Empty;


                    DataTable dtGeneralLedger = ConstructGeneralLedger();
                    //dtGeneralLedger.Rows.Add();
                    dtGeneralLedger.Rows[0]["LEDGER_ID"] = ExpenseLedgerId;
                    dtGeneralLedger.Rows[0]["CREDIT"] = 0;
                    dtGeneralLedger.Rows[0]["DEBIT"] = PremiumAmount;
                    dtGeneralLedger.Rows[0]["AMOUNT"] = PremiumAmount;

                    DataTable dtcashLedger = ConstructGeneralLedger();
                    //dtcashLedger.Rows.Add();
                    dtcashLedger.Rows[0]["LEDGER_ID"] = CashBankLedgerId;
                    dtcashLedger.Rows[0]["CREDIT"] = PremiumAmount;
                    dtcashLedger.Rows[0]["DEBIT"] = 0;
                    dtcashLedger.Rows[0]["AMOUNT"] = PremiumAmount;

                    this.TransInfo = dtGeneralLedger.DefaultView;
                    this.CashTransInfo = dtcashLedger.DefaultView;
                    resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                    if (resultArgs.Success)
                    {
                        VoucherId = voucherTransaction.VoucherId;
                    }

                }

            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        private DataTable ConstructGeneralLedger()
        {
            DataTable dtPurchaseVouhcerDetail = new DataTable();
            dtPurchaseVouhcerDetail.Columns.Add("SOURCE", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("ITEM_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("LEDGER_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("CREDIT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("DEBIT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("CHEQUE_NO", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtPurchaseVouhcerDetail.Rows.Add(dtPurchaseVouhcerDetail.NewRow());
            return dtPurchaseVouhcerDetail;
        }
        public ResultArgs SaveAssetAmcMaster()
        {
            using (DataManager dataManager = new DataManager(Mode == (int)AssetAmc.Renew || Mode == (int)AssetAmc.Create ? SQLCommand.AssetAMCRenewal.AddAMCRenewalMaster : SQLCommand.AssetAMCRenewal.EditAMCRenewalMaster))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, AMCId, true);
                dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_GROUPColumn, AMCGroup);
                dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.PROVIDERColumn, Provider);
                dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.PROJECT_IDColumn, ProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveItemMappingDetails()
        {
            if (dtAmcDetails != null && dtAmcDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAmcDetails.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.AddAMCItemMapping))
                    {
                        dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AMCId);
                        dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, dr[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveAssetAmcHistory()
        {
            using (DataManager dataManager = new DataManager(Mode == (int)AssetAmc.Renew || Mode == (int)AssetAmc.Create ? SQLCommand.AssetAMCRenewal.AddAMCRenewalHistory : SQLCommand.AssetAMCRenewal.EditAMCRenewalHistory))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, AMCId);
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.RENEWAL_DATEColumn, RenewalDate);
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_FROMColumn, AmcFrom);
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_TOColumn, AmcTo);
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.PREMIUM_AMOUNTColumn, PremiumAmount);
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_RENEWAL_IDColumn, AmcRenewalId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAMCMasterDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.DeleteAMCRenewMaster))
            {
                dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AMCId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteAMCHistoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.DeleteAMCRenewlHistory))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, AMCId);
                resultArgs = dataManager.UpdateData();
                DeleteAMCVoucherDetails(dataManager);
            }
            return resultArgs;
        }
        public ResultArgs DeleteAMCItemMapping()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.DeleteAmcItemMapping))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCItemMapping.AMC_IDColumn, AMCId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs LoadItemDetails()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchItemDetails))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private void FillRenewProperties(int AmcId, int AmcRenewalId)
        {
            resultArgs = FetchById(AmcId, AmcRenewalId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                AMCGroup = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AmcRenewalMaster.AMC_GROUPColumn.ColumnName].ToString();
                Provider = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AmcRenewalMaster.PROVIDERColumn.ColumnName].ToString();
                ProjectId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                PremiumAmount = NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AMCRenewalHistory.PREMIUM_AMOUNTColumn.ColumnName].ToString());
                AmcFrom = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AMCRenewalHistory.AMC_FROMColumn.ColumnName].ToString(), false);
                AmcTo = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AMCRenewalHistory.AMC_TOColumn.ColumnName].ToString(), false);
                RenewalDate = DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AMCRenewalHistory.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                VoucherId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs FetchById(int AmcId, int RenewalId)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCRenewal.Fetch))
            {
                datamanager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AmcId);
                if (RenewalId != 0)
                {
                    datamanager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_RENEWAL_IDColumn, RenewalId);
                }
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchAMCMappedItems(string AMcId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.FetchAMCRenewalMappedItems))
                {
                    dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AMcId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs;
        }
        public DataSet LoadAMCRenewalDetails()
        {
            string AMCId = string.Empty;
            DataSet dsAMCRenewal = new DataSet();
            try
            {
                resultArgs = FetchAMCRenewalMaster();
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    DataView dvAMCRenewalMaster = resultArgs.DataSource.Table.DefaultView;

                    dvAMCRenewalMaster.ToTable().TableName = "Master";

                    resultArgs.DataSource.Table.TableName = "Master";

                    dsAMCRenewal.Tables.Add(dvAMCRenewalMaster.ToTable());
                    for (int i = 0; i < dvAMCRenewalMaster.ToTable().Rows.Count; i++)
                    {
                        AMCId += dvAMCRenewalMaster.ToTable().Rows[i][AppSchema.AmcRenewalMaster.AMC_IDColumn.ColumnName].ToString() + ",";
                    }
                    AMCId = AMCId.TrimEnd(',');

                    resultArgs = FetchAMCRenewalHistory(AMCId);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "AMC Renewal";
                        dsAMCRenewal.Tables.Add(resultArgs.DataSource.Table);
                        dsAMCRenewal.Relations.Add(dsAMCRenewal.Tables[1].TableName, dsAMCRenewal.Tables[0].Columns[this.AppSchema.AmcRenewalMaster.AMC_IDColumn.ColumnName], dsAMCRenewal.Tables[1].Columns[this.AppSchema.AmcRenewalMaster.AMC_IDColumn.ColumnName]);
                    }
                    resultArgs = FetchAMCMappedItems(AMCId);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        resultArgs.DataSource.Table.TableName = "AMC Items";
                        dsAMCRenewal.Tables.Add(resultArgs.DataSource.Table);
                        dsAMCRenewal.Relations.Add(dsAMCRenewal.Tables[2].TableName, dsAMCRenewal.Tables[0].Columns[this.AppSchema.AmcRenewalMaster.AMC_IDColumn.ColumnName], dsAMCRenewal.Tables[2].Columns[this.AppSchema.AmcRenewalMaster.AMC_IDColumn.ColumnName]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return dsAMCRenewal;
        }
        public ResultArgs FetchAMCRenewalHistory(string amcid)
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchAMCRenewalHistoryByAmCId))
                {
                    datamanager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, amcid);
                    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs;
        }
        public ResultArgs FetchAvailableMappedItems()
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchMappedItemAvailableList))
                {
                    datamanager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, AMCId);
                    datamanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, this.ProjectId);
                    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedMappedItems()
        {
            try
            {
                using (DataManager datamanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchMappedItemSelectedList))
                {
                    datamanager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, AMCId);
                    datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = datamanager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs;
        }
        public ResultArgs DeleteRenewalHistoryByamcRenewalId()
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.AssetAMCRenewal.DeleteAMCRenewalHistoryByAMCRenewalId))
                {
                    dtmanager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, AMCId);
                    dtmanager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_RENEWAL_IDColumn, AmcRenewalId);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.UpdateData();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        DeleteAMCVoucherDetails(dtmanager);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs;
        }
        public int GetmaxAMCRenewalIdByAMCId(int amcid)
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchMaximumRenewalIdByAMCId))
                {
                    dtmanager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_IDColumn, amcid);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchUnmappedItems()
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchUnmappedItems))
                {
                    dtmanager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, this.ProjectId);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, false);
            }
            return resultArgs;
        }
        public string FetchLastRenewldateByAMCId(int Amcid)
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchLastRenewaldateByAMCId))
                {
                    dtmanager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, Amcid);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultArgs.DataSource.Sclar.ToString();
        }
        public int FetchVoucherIdByamcRenewalId(int amcrenewalid)
        {
            using (DataManager dtmanger = new DataManager(SQLCommand.AssetAMCRenewal.FetchVocuherIdByAMCRenewalId))
            {
                dtmanger.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_RENEWAL_IDColumn, amcrenewalid);
                resultArgs = dtmanger.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchLedgerIdByvoucherId(int voucherid)
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchledgerIdByVoucherID))
            {
                dtmanager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, voucherid);
                resultArgs = dtmanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteAMCVoucherDetails(DataManager dm)
        {
            using (VoucherTransactionSystem VoucherTran = new VoucherTransactionSystem())
            {
                VoucherTran.VoucherId = VoucherId;
                resultArgs = VoucherTran.RemoveVoucher(dm);
            }
            return resultArgs;
        }
        public int FetchRenewalHistoryCount()
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchRenewalHistoryCountByAMCId))
            {
                dtmanager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AMCId);
                resultArgs = dtmanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchVoucherIdbyAmcId()
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.AssetAMCRenewal.FetchVoucherIdByAMCId))
            {
                dtmanager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AMCId);
                resultArgs = dtmanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPreviousRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.FetchPreviousRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_RENEWAL_IDColumn, AmcRenewalId);
                dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AMCId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchNextRenewal()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetAMCRenewal.FetchNextRenewal))
            {
                dataManager.Parameters.Add(this.AppSchema.AMCRenewalHistory.AMC_RENEWAL_IDColumn, AmcRenewalId);
                dataManager.Parameters.Add(this.AppSchema.AmcRenewalMaster.AMC_IDColumn, AMCId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
