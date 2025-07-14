using System;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Data;
using Bosco.Model.Transaction;
using Bosco.Model.Dsync;
using Bosco.Model.UIModel.Master;
using AcMEDSync.Model;
using AcMEDSync;

namespace Bosco.Model.Donor
{
    public class DonorManagementSystem : SystemBase
    {
        ResultArgs resultArgs = new ResultArgs();
        public DataTable dtTransaction = new DataTable();

        #region Properties
        public DataTable dtDonor { set; get; }
        public bool canOverwrite = false;
        public string DateFrom = "";
        public string DateTo = "";
        private string ProjectIds { get; set; }
        private int BranchId { get; set; }
        private string Location { get; set; }
        private int LocationId { get; set; }
        #endregion

        public ResultArgs ImportDonorTransactions(DataManager dm)
        {
            resultArgs.Success = true;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dm.Database;
                // resultArgs = ImportPurpose();
                if (resultArgs.Success)
                {
                    if (canOverwrite) resultArgs = DeleteTransaction();
                    if (resultArgs.Success)
                    {
                        resultArgs = ImportVouchers(dataManager);
                    }

                }

                //if (!resultArgs.Success)
                //{
                //    dataManager.TransExecutionMode = ExecutionMode.Fail;
                //}
            }
            return resultArgs;
        }

        public ResultArgs ImportPurpose()
        {
            using (ImportMasterSystem importSystem = new ImportMasterSystem())
            {
                importSystem.dtFCPurpose = dtTransaction;
                resultArgs = importSystem.ImportFCPurpose();
            }
            return resultArgs;
        }

        private ResultArgs DeleteDonorVouchers()
        {
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
            {
                resultArgs = voucherSystem.DeleteDonorVouchers();
            }
            return resultArgs;
        }

        //private ResultArgs DeleteTransaction()
        //{
        //    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
        //    {
        //        DataView dvValues = new DataView(dtTransaction);
        //        DataTable distinctProjects = new DataTable();
        //        distinctProjects = dvValues.ToTable(true, "PROJECT");

        //        resultArgs = GetProjectIds(distinctProjects);

        //        if (resultArgs.Success && (resultArgs.ReturnValue != null))
        //        {
        //            ProjectIds = resultArgs.ReturnValue != null ? resultArgs.ReturnValue.ToString() : "0";
        //            if (!string.IsNullOrEmpty(ProjectIds))
        //            {
        //                resultArgs = voucherSystem.DeleteVouchersforImport(DateFrom, DateTo, ProjectIds);
        //            }
        //        }
        //    }
        //    return resultArgs;
        //}

        private ResultArgs DeleteTransaction()
        {
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
            {
                int VoucherId = 0;
                DataTable dtTrans = new DataTable();
                DataView dvValues = new DataView(dtTransaction);
                DataTable distinctProjects = new DataTable();
                distinctProjects = dvValues.ToTable(true, "PROJECT");

                resultArgs = GetProjectIds(distinctProjects);

                if (resultArgs.Success && (resultArgs.ReturnValue != null))
                {
                    ProjectIds = resultArgs.ReturnValue != null ? resultArgs.ReturnValue.ToString() : "0";
                    if (!string.IsNullOrEmpty(ProjectIds))
                    {
                        resultArgs = FetchTransactions();
                        if (resultArgs.Success)
                        {
                            dtTrans = resultArgs.DataSource.Table;
                            if (dtTrans != null && dtTrans.Rows.Count > 0)
                            {
                                foreach (DataRow drTrans in dtTrans.Rows)
                                {
                                    VoucherId = this.NumberSet.ToInteger(drTrans[this.AppSchema.VoucherMaster.VOUCHER_IDColumn.ColumnName].ToString());

                                    if (VoucherId > 0)
                                    {
                                        using (BalanceSystem balance = new BalanceSystem())
                                        {
                                            resultArgs = balance.UpdateTransBalance(BranchId, LocationId, VoucherId, TransactionAction.Cancel);
                                        }

                                        if (resultArgs.Success)
                                        {
                                            resultArgs = DeleteVoucherTransaction(VoucherId);
                                            if (resultArgs.Success)
                                            {
                                                resultArgs = DeleteVoucherCostCentre(VoucherId);
                                                if (resultArgs.Success)
                                                {
                                                    resultArgs = DeleteVoucherMasterTransaction(VoucherId);
                                                }
                                            }
                                        }
                                    }

                                    if (!resultArgs.Success)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch the Transaction between the Date range.
        /// </summary>
        /// <returns></returns>
        private ResultArgs FetchTransactions()
        {
            AcMEDataSynLog.WriteLog("FetchTransactions Started..");
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.FetchTransactions, DataBaseType.BranchOffice, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_FROMColumn, DateFrom);
                    dataManager.Parameters.Add(this.AppSchema.ExportVouchers.DATE_TOColumn, DateTo);
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectIds ?? "0");
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in FetchTransactions: " + ex.ToString();
            }
            finally { }

            if (resultArgs.Success)
            {
                AcMEDataSynLog.WriteLog("FetchTransactions Ended.");
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Voucher Transaction based on the voucher Id
        /// </summary>
        /// <param name="VoucherId">Voucher Id</param>
        /// <returns>resultargs whether success or failure</returns>
        private ResultArgs DeleteVoucherTransaction(int VoucherId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteVoucherTrans, DataBaseType.BranchOffice, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Transaction: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Transaction." + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the Voucher Cost Centre based on the Voucher Id
        /// </summary>
        /// <param name="VoucherId">Voucher Id</param>
        /// <returns></returns>
        private ResultArgs DeleteVoucherCostCentre(int VoucherId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteVoucherCostCentre, DataBaseType.BranchOffice, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Cost Centre: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Cost Centre. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete the voucher Master Transaction based on the Voucher Id
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        private ResultArgs DeleteVoucherMasterTransaction(int VoucherId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.ImportVoucher.DeleteVoucherMasterTrans, DataBaseType.BranchOffice, SQLAdapterType.HOSQL))
                {
                    dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_IDColumn, VoucherId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(this.AppSchema.LicenseDataTable.LOCATION_IDColumn, LocationId);

                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Error in Deleting Voucher Master Transaction: " + ex.ToString();
            }
            finally { }

            if (!resultArgs.Success)
            {
                resultArgs.Message = "Error in Deleting Voucher Master Transaction. " + resultArgs.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is to get the Project Id's in the XML that is passed.
        /// These Id's will be used to fetch transaction of those projects and used for deletion.
        /// </summary>
        /// <returns></returns>
        private ResultArgs GetProjectIds(DataTable dtProjects)
        {
            try
            {
                string ProjectIds = string.Empty;
                if (dtProjects != null && dtProjects.Rows.Count > 0)
                {
                    foreach (DataRow drProject in dtProjects.Rows)
                    {
                        string ProjectName = drProject[this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                        int ProjectId = GetProjectId(ProjectName);

                        if (ProjectId > 0)
                        {
                            ProjectIds += ProjectId.ToString();
                            ProjectIds += ',';
                        }
                    }

                    if (!string.IsNullOrEmpty(ProjectIds))
                    {
                        resultArgs.ReturnValue = ProjectIds.TrimEnd(',');
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }



        // I keep the Donor and Purpose is empty if not needed 
        //else if (voucherTransaction.DonorId == 0)
        //{
        //    resultArgs.Message = "Donor is not available : '" + DonorName + "' '" + City + " ' '" + Address + "'";
        //}
        //else if (voucherTransaction.PurposeId == 0)
        //{
        //    resultArgs.Message = "Purpose is not available. The required Purpose is '" + PurposeName + "'";
        //}
        //if (string.IsNullOrEmpty(voucherTransaction.VoucherNo))
        //{
        //    resultArgs.Message = "Voucher No is Empty";
        //}
        // voucherTransaction.VoucherNo = drTrans["VOUCHER NO"].ToString();
        // voucherTransaction.ContributionType = "F";
        // voucherTransaction.CurrencyCountryId = voucherTransaction.ExchageCountryId = voucherTransaction.ExchangeRate = 1;
        //voucherTransaction.ContributionAmount = voucherTransaction.CalculatedAmount = voucherTransaction.ActualAmount = this.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());
        private ResultArgs ImportVouchers(DataManager dataManager)
        {
            string ProjectName = string.Empty;
            string DonorName = string.Empty;
            string VoucherType = string.Empty;
            string PurposeName = string.Empty;
            string GeneralLedger = string.Empty;
            string CashBankLedger = string.Empty;
            int GeneralLedgerId = 0;
            int CashBankLedgerId = 0;
            string MismatchedNames = string.Empty;
            decimal GeneralLedgerAmout = 0;
            try
            {
                if (dtTransaction != null && dtTransaction.Rows.Count > 0)
                {
                    using (DataManager dm = new DataManager())
                    {
                        foreach (DataRow drTrans in dtTransaction.Rows)
                        {
                            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                            {
                                dm.Database = dataManager.Database;
                                voucherTransaction.VoucherId = 0;
                                voucherTransaction.VoucherSubType = LedgerTypes.GN.ToString();
                                voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                                voucherTransaction.ContributionType = "N";
                                voucherTransaction.ExchangeRate = 1;
                                voucherTransaction.CurrencyCountryId = voucherTransaction.ExchageCountryId = 0;
                                voucherTransaction.ContributionAmount = 0.00m;
                                voucherTransaction.VoucherDate = (!string.IsNullOrEmpty(drTrans["VOUCHER_DATE"].ToString())) ? this.DateSet.ToDate(drTrans["VOUCHER_DATE"].ToString(), false) : DateTime.MinValue;
                                VoucherType = drTrans["VOUCHER TYPE"].ToString();
                                if (VoucherType == DefaultVoucherTypes.Receipt.ToString())
                                {
                                    VoucherType = "RC";
                                }
                                else if (VoucherType == DefaultVoucherTypes.Payment.ToString())
                                {
                                    VoucherType = "PY";
                                }
                                voucherTransaction.VoucherType = VoucherType;
                                voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                                if (VoucherType == "RC") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Receipt;
                                else if (VoucherType == "PY") voucherTransaction.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Payment;

                                ProjectName = drTrans["PROJECT"].ToString();
                                voucherTransaction.ProjectId = GetProjectId(ProjectName);

                                GeneralLedger = drTrans["LEDGER HEAD"].ToString();
                                GeneralLedgerId = GetLedgerId(GeneralLedger);

                                CashBankLedger = drTrans["CASH/BANK"].ToString();
                                CashBankLedgerId = GetLedgerId(CashBankLedger);

                                GeneralLedgerAmout = this.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());

                                DonorName = drTrans["DONOR "].ToString().Trim();
                                voucherTransaction.DonorId = string.IsNullOrEmpty(DonorName) ? 0 : GetDonorIdByName(DonorName);

                                PurposeName = drTrans["FC_PURPOSE"].ToString().Trim();
                                voucherTransaction.PurposeId = GetPurposeId(PurposeName);

                                if (voucherTransaction.DonorId > 0 && voucherTransaction.PurposeId > 0)
                                {
                                    voucherTransaction.ContributionType = "F";
                                    voucherTransaction.CurrencyCountryId = voucherTransaction.ExchageCountryId = 1;
                                    voucherTransaction.ExchangeRate = 1;
                                    voucherTransaction.ContributionAmount = voucherTransaction.CalculatedAmount = voucherTransaction.ActualAmount = GeneralLedgerAmout;
                                }

                                voucherTransaction.Narration = drTrans["NARRATION"].ToString();
                                voucherTransaction.NameAddress = drTrans["NAME & ADDRESS"].ToString();
                                voucherTransaction.ChequeNo = CashBankLedger.Equals(CashFlag.Cash.ToString()) ? string.Empty : drTrans["CHEQUE NO"].ToString();
                                voucherTransaction.MaterializedOn = CashBankLedger.Equals(CashFlag.Cash.ToString()) ? string.Empty : drTrans["MATERIALIZED DATE"].ToString();

                                voucherTransaction.Status = 1;
                                voucherTransaction.ClientCode = "EMTV";
                                voucherTransaction.CreatedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());
                                voucherTransaction.ModifiedBy = this.NumberSet.ToInteger(this.LoginUserId.ToString());

                                if (voucherTransaction.ProjectId == 0)
                                {
                                    resultArgs.Message = "Project is not created. The required Project is '" + ProjectName + "'";
                                }
                                else if (voucherTransaction.VoucherDate.Equals(DateTime.MinValue))
                                {
                                    resultArgs.Message = "Voucher Date is empty";
                                }
                                else if (string.IsNullOrEmpty(VoucherType))
                                {
                                    resultArgs.Message = "Voucher Type is empty";
                                }
                                else if (GeneralLedgerId == 0)
                                {
                                    resultArgs.Message = "Ledger is not available. The required Ledger is '" + GeneralLedger + "'";
                                }
                                else if (CashBankLedgerId == 0)
                                {
                                    resultArgs.Message = "Cash/Bank Account is not available. The required Cash/Bank Account is '" + CashBankLedger + "'";
                                }
                                else if (GeneralLedgerAmout <= 0)
                                {
                                    resultArgs.Message = "Transaction Amount is empty";
                                }
                                else if ((!string.IsNullOrEmpty(DonorName)) && voucherTransaction.DonorId == 0)
                                {
                                    resultArgs.Message = "Donor Name is not available. The required Donor is '" + DonorName + "' if Not Needed Remove it from the excel";
                                }
                                else if (voucherTransaction.DonorId > 0 && voucherTransaction.PurposeId == 0)
                                {
                                    resultArgs.Message = "Purpose is empty";
                                }
                                else if ((!CashBankLedger.Equals(CashFlag.Cash.ToString())) && this.DateSet.ToDate(voucherTransaction.MaterializedOn, false) < voucherTransaction.VoucherDate && (!voucherTransaction.MaterializedOn.Equals("")))
                                {
                                    resultArgs.Message = "Materilized Date should not be less than the Voucher Date";
                                }
                                else
                                {
                                    using (ImportVoucherSystem importVoucher = new ImportVoucherSystem())
                                    {
                                        importVoucher.ProjectId = voucherTransaction.ProjectId;
                                        importVoucher.DonorId = voucherTransaction.DonorId;

                                        resultArgs = importVoucher.MapProject(SQLCommand.ImportVoucher.MapProjectDonor);
                                        if (resultArgs.Success)
                                        {
                                            importVoucher.LedgerId = GeneralLedgerId;
                                            resultArgs = importVoucher.MapProject(SQLCommand.ImportVoucher.MapProjectLedger);
                                            if (resultArgs.Success)
                                            {
                                                importVoucher.LedgerId = CashBankLedgerId;
                                                resultArgs = importVoucher.MapProject(SQLCommand.ImportVoucher.MapProjectLedger);
                                                if (resultArgs.Success)
                                                {
                                                    importVoucher.ContributionId = voucherTransaction.PurposeId;
                                                    resultArgs = importVoucher.MapProject(SQLCommand.ImportVoucher.MapProjectPurpose);
                                                    if (resultArgs.Success)
                                                    {
                                                        resultArgs = voucherTransaction.ConstructVoucherData(GeneralLedgerId, GeneralLedgerAmout);
                                                        if (resultArgs.Success)
                                                        {
                                                            this.TransInfo = resultArgs.DataSource.Table.DefaultView;
                                                            resultArgs = voucherTransaction.ConstructVoucherData(CashBankLedgerId, GeneralLedgerAmout, voucherTransaction.ChequeNo, voucherTransaction.MaterializedOn);
                                                            if (resultArgs.Success)
                                                            {
                                                                this.CashTransInfo = resultArgs.DataSource.Table.DefaultView;
                                                                resultArgs = voucherTransaction.SaveVoucherDetails(dataManager);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            return resultArgs;
        }

        private int GetProjectId(string projectName)
        {
            using (ImportMasterSystem importSystem = new ImportMasterSystem())
            {
                importSystem.ProjectName = projectName;
                return importSystem.GetMasterId(DataSync.Project);
            }
        }

        private int GetDonorId(string donorName, string place, string address)
        {
            using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
            {
                donorSystem.Name = donorName;
                donorSystem.Place = place;
                donorSystem.Address = address;
                resultArgs = donorSystem.GetDonorId();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private int GetDonorIdByName(string donorName)
        {
            using (DonorAuditorSystem donorSystem = new DonorAuditorSystem())
            {
                donorSystem.Name = donorName;
                resultArgs = donorSystem.GetDonorIdName();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private int GetPurposeId(string PurposeName)
        {
            using (ImportMasterSystem importSystem = new ImportMasterSystem())
            {
                importSystem.FCPurpose = PurposeName;
                return importSystem.GetMasterId(DataSync.FCPurpose);
            }
        }

        private int GetLedgerId(string LedgerName)
        {
            using (ImportMasterSystem importSystem = new ImportMasterSystem())
            {
                importSystem.LedgerName = LedgerName;
                return importSystem.GetMasterId(DataSync.Ledger);
            }
        }

        /// <summary>
        /// Get State ID
        /// </summary>
        /// <param name="StateName"></param>
        /// <returns></returns>
        private int GetStateId(string StateName)
        {
            using (StateSystem stateSystem = new StateSystem())
            {
                stateSystem.StateName = StateName;
                resultArgs = stateSystem.GetStateId();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
    }
}
