using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel
{
    public class NumberSystem : SystemBase
    {
        ResultArgs resultArgs = new ResultArgs();

        #region Properties
        public int VoucherDefinitionId { get; set; }
        private int NumberId { get; set; }
        private int VoucherNumberFormatId { get; set; }
        private string VoucherLastNumber { get; set; }
        private int VoucherRunningNumber { get; set; }
        private string VoucherNumberFormat { get; set; }
        private string NumericalWidth { get; set; }
        private int Prefilwitzero { get; set; }
        public int VoucherMonth { get; set; }
        public int VoucherYear { get; set; }
        public int VoucherDuration { get; set; }
        private int VoucherApplicableMonth { get; set; }
        private int VoucherNumberFormatmonth { get; set; }
        private int VoucherNumberFormatyear { get; set; }
        private int ResetVoucherMonth { get; set; }
        private int ResetVoucherYear { get; set; }
        public int PreviourRunningDigit { get; set; }
        public bool IsInsertVoucher { get; set; }
        #endregion

        public string GetAssetID(int ItemId)
        {
            string formatedValue = "";
            try
            {
                string returnValue = "";
                string Prifix = "";
                string Sufix = "";
                int StartingNumber = 0;
                int LastRunningDigit = 0;

                DataView dvNoFormat = new DataView();
                // To fetch the asset item details
                using (AssetItemSystem assetItem = new AssetItemSystem())
                {
                    assetItem.ItemId = ItemId;
                    resultArgs = assetItem.FetchAssetItemDetailsById();
                }

                if (!resultArgs.Success | resultArgs.RowsAffected < 1)
                    return returnValue;
                dvNoFormat = ((DataView)resultArgs.DataSource.Table.DefaultView);
                if (dvNoFormat[0][this.AppSchema.ASSETItem.STARTING_NOColumn.ColumnName].ToString() == "")
                    return returnValue;
                Prifix = dvNoFormat[0][this.AppSchema.ASSETItem.PREFIXColumn.ColumnName].ToString();
                Sufix = dvNoFormat[0][this.AppSchema.ASSETItem.SUFFIXColumn.ColumnName].ToString();
                StartingNumber = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.ASSETItem.STARTING_NOColumn.ColumnName].ToString());
                LastRunningDigit = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn.ColumnName].ToString());

                if (LastRunningDigit >= StartingNumber)
                {
                    LastRunningDigit = LastRunningDigit + 1;
                }
                else
                {
                    LastRunningDigit = StartingNumber;
                }

                VoucherRunningNumber = LastRunningDigit;
                formatedValue = (Prifix + LastRunningDigit + Sufix).ToString();
                UpdateLastAssetRunningDigit(ItemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return formatedValue;
        }

        public ResultArgs RegenerateAssetID(int ItemId)
        {
            string formatedValue = "";
            try
            {
                string Prifix = "";
                string Sufix = "";
                int StartingNumber = 0;
                int LastRunningDigit = 0;
                int ItemDetailId = 0;
                DataView dvNoFormat = new DataView();
                using (AssetItemSystem assetItem = new AssetItemSystem())
                {
                    assetItem.ItemId = ItemId;
                    resultArgs = assetItem.FetchAssetItemDetailsById();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dvNoFormat = ((DataView)resultArgs.DataSource.Table.DefaultView);
                        Prifix = dvNoFormat[0][this.AppSchema.ASSETItem.PREFIXColumn.ColumnName].ToString();
                        Sufix = dvNoFormat[0][this.AppSchema.ASSETItem.SUFFIXColumn.ColumnName].ToString();
                        StartingNumber = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.ASSETItem.STARTING_NOColumn.ColumnName].ToString());

                        resultArgs = assetItem.FetchAssetItemDetailById();

                        if (resultArgs != null && resultArgs.Success)
                        {
                            DataTable dtAssetItem = resultArgs.DataSource.Table;
                            if (dtAssetItem != null && dtAssetItem.Rows.Count > 0)
                            {
                                foreach (DataRow drAsset in dtAssetItem.Rows)
                                {
                                    if (LastRunningDigit >= StartingNumber)
                                    {
                                        LastRunningDigit = LastRunningDigit + 1;
                                    }
                                    else
                                    {
                                        LastRunningDigit = StartingNumber;
                                    }
                                    VoucherRunningNumber = LastRunningDigit;
                                    formatedValue = VoucherLastNumber = (Prifix + LastRunningDigit + Sufix).ToString();
                                    ItemDetailId = this.NumberSet.ToInteger(drAsset[this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName].ToString());
                                    if (ItemDetailId > 0)
                                    {
                                        resultArgs = UpdateAssetId(ItemDetailId, formatedValue);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = UpdateLastAssetRunningDigit(ItemId);
                                        }
                                    }
                                }
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

        public ResultArgs GetTempRegenerateAssetID(DataTable dtAsset, int ItemId)
        {
            string formatedValue = "";
            DataTable dtAssetItem = dtAsset;
            try
            {
                string Prifix = "";
                string Sufix = "";
                int StartingNumber = 0;
                int LastRunningDigit = 0;
                DataView dvNoFormat = new DataView();
                using (AssetItemSystem assetItem = new AssetItemSystem())
                {
                    assetItem.ItemId = ItemId;
                    resultArgs = assetItem.FetchAssetItemDetailsById();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dvNoFormat = ((DataView)resultArgs.DataSource.Table.DefaultView);
                        Prifix = dvNoFormat[0][this.AppSchema.ASSETItem.PREFIXColumn.ColumnName].ToString();
                        Sufix = dvNoFormat[0][this.AppSchema.ASSETItem.SUFFIXColumn.ColumnName].ToString();
                        StartingNumber = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.ASSETItem.STARTING_NOColumn.ColumnName].ToString());

                        if (dtAssetItem != null && dtAssetItem.Rows.Count > 0)
                        {
                            foreach (DataRow drAsset in dtAssetItem.Rows)
                            {
                                if (LastRunningDigit >= StartingNumber)
                                {
                                    LastRunningDigit = LastRunningDigit + 1;
                                }
                                else
                                {
                                    LastRunningDigit = StartingNumber;
                                }
                                VoucherRunningNumber = LastRunningDigit;
                                formatedValue = VoucherLastNumber = (Prifix + LastRunningDigit + Sufix).ToString();

                                drAsset[this.AppSchema.ASSETItem.ASSET_IDColumn.ColumnName] = formatedValue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dtAssetItem;
            }
            return resultArgs;
        }

        public ResultArgs UpdateAssetId(int ItemDetailID, string AssetID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.UpdateAssetID))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_DETAIL_IDColumn, ItemDetailID);
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ASSET_IDColumn, AssetID);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateLastAssetRunningDigit(int ItemId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.UpdateLastAssetRunning))
            {
                dataManager.Parameters.Add(this.AppSchema.ASSETItem.ITEM_IDColumn, ItemId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn, VoucherRunningNumber);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public string getNewNumber(DataManager dataManage, NumberFormat numberFormatId, string projectId, int TransType, DateTime VoucherDate, bool IsTempVoucherNo, Int32 voucherdefinitionid)//,
        {
            string formatedValue = "";
            try
            {
                string numbFormat = "";
                string returnValue = "";
                string Prifix = "";
                string Sufix = "";
                int StartingNumber = 0;
                int LastRunningDigit = PreviourRunningDigit;
                //int Month = 0;
                //int Duration = 0;
                //int VoucherMonth = 0;

                DataView dvNoFormat = new DataView();
                // To fetch the Active Voucher Number definition
                using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchVoucherNumberFormat))
                {
                    dataManager.Database = dataManage.Database;
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId); //Project Id
                    dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, TransType);//Voucher Type
                    dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, voucherdefinitionid);//Voucher Type
                    resultArgs = dataManager.FetchData(DataSource.DataView);
                }
                //return resultArgs;


                if (!resultArgs.Success | resultArgs.RowsAffected < 1)
                    return returnValue;
                dvNoFormat = ((DataView)resultArgs.DataSource.TableView);
                if (dvNoFormat[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString() == "")
                    return returnValue;
                Prifix = dvNoFormat[0][this.AppSchema.Voucher.PREFIX_CHARColumn.ColumnName].ToString();
                Sufix = dvNoFormat[0][this.AppSchema.Voucher.SUFFIX_CHARColumn.ColumnName].ToString();
                StartingNumber = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString());
                Prefilwitzero = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.PREFIX_WITH_ZEROColumn.ColumnName].ToString());
                NumericalWidth = dvNoFormat[0][this.AppSchema.Voucher.NUMBERICAL_WITHColumn.ColumnName].ToString();
                numbFormat = (Prifix + Sufix).ToString();
                VoucherApplicableMonth = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.MONTHColumn.ColumnName].ToString()) + 1;
                VoucherMonth = VoucherDate.Month;
                VoucherYear = VoucherDate.Year;
                VoucherDuration = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.DURATIONColumn.ColumnName].ToString());
                VoucherDefinitionId = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
                NumberId = (int)numberFormatId;
                // Fetch Existing Number Format
                resultArgs = GetResetStartMonth();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    ResetVoucherMonth = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["MONTH"].ToString());
                    ResetVoucherYear = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["YEAR"].ToString());
                }
                else
                {
                    return returnValue;
                }

                DataView dvNumbFormat = FetchVoucherNumber(dataManage, NumberId.ToString(), numbFormat, projectId);
                if (dvNumbFormat.Table.Rows.Count > 0)
                {
                    for (int i = 0; i <= dvNumbFormat.Count - 1; i++)
                    {
                        VoucherNumberFormat = dvNumbFormat[i][this.AppSchema.VoucherNumber.NUMBER_FORMATColumn.ColumnName].ToString();
                        VoucherNumberFormatmonth = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.Voucher.VOUCHER_MONTHColumn.ColumnName].ToString());
                        VoucherNumberFormatyear = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.Voucher.VOUCHER_YEARColumn.ColumnName].ToString());
                        if (numbFormat == VoucherNumberFormat) //&& VoucherMonth == VoucherNumberFormatmonth && VoucherYear == VoucherNumberFormatyear)// && !IsResetVoucher()  VoucherMonth == VoucherNumberFormatmonth 
                        {
                            VoucherLastNumber = dvNumbFormat[i][this.AppSchema.VoucherNumber.LAST_VOUCHER_NUMBERColumn.ColumnName].ToString();
                            if (LastRunningDigit == 0 && !IsInsertVoucher) { LastRunningDigit = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn.ColumnName].ToString()); }
                            //if (!IsResetVoucher() || IsResetVoucher() && LastRunningDigit >0)
                            //{
                            if (LastRunningDigit >= StartingNumber)
                            {
                                LastRunningDigit = LastRunningDigit + 1;
                            }
                            else
                            {
                                LastRunningDigit = StartingNumber;
                            }
                            // LastRunningDigit = (LastRunningDigit < StartingNumber) ? LastRunningDigit + 1 : (LastRunningDigit == StartingNumber) ? StartingNumber + 1 : StartingNumber;
                            VoucherRunningNumber = LastRunningDigit;
                            formatedValue = VoucherLastNumber = (Prefilwitzero == (int)YesNo.No) ? (Prifix + LastRunningDigit.ToString().PadLeft(int.Parse(NumericalWidth), '0') + Sufix).ToString() : (Prifix + LastRunningDigit + Sufix).ToString();
                            NumberId = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.VoucherNumber.NUMBER_IDColumn.ColumnName].ToString());
                            VoucherNumberFormatId = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn.ColumnName].ToString());
                            if (!IsTempVoucherNo)
                                updateLastVoucherNumber(dataManage, this.NumberSet.ToInteger(projectId));
                            break;
                            //}
                        }
                    }
                }
                else if (!IsResetVoucher())
                {
                    string type = (TransType == 1) ? "RC" : (TransType == 2) ? "PY" : (TransType == 3) ? "CN" : "JN";
                    int resetMth = GetResetMonth(type, VoucherDate);//To take the last voucher between accounting year from & CurrentVoucherMonth.
                    DataView dvNumb = FetchVoucherNumber(dataManage, NumberId.ToString(), numbFormat, projectId);
                    if (dvNumb.Table.Rows.Count > 0)
                    {
                        for (int j = 0; j <= dvNumb.Count - 1; j++)
                        {
                            VoucherNumberFormat = dvNumb[j][this.AppSchema.VoucherNumber.NUMBER_FORMATColumn.ColumnName].ToString();
                            VoucherNumberFormatmonth = this.NumberSet.ToInteger(dvNumb[j][this.AppSchema.Voucher.VOUCHER_MONTHColumn.ColumnName].ToString());
                            VoucherNumberFormatyear = this.NumberSet.ToInteger(dvNumb[j][this.AppSchema.Voucher.VOUCHER_YEARColumn.ColumnName].ToString());
                            if (numbFormat == VoucherNumberFormat)// && VoucherYear == VoucherNumberFormatyear)// && !IsResetVoucher()  VoucherMonth == VoucherNumberFormatmonth 
                            {
                                VoucherLastNumber = dvNumb[j][this.AppSchema.VoucherNumber.LAST_VOUCHER_NUMBERColumn.ColumnName].ToString();
                                if (LastRunningDigit == 0 && !IsInsertVoucher) { LastRunningDigit = this.NumberSet.ToInteger(dvNumb[j][this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn.ColumnName].ToString()); }
                                //if (!IsResetVoucher() || IsResetVoucher() && LastRunningDigit >0)
                                //{
                                if (LastRunningDigit >= StartingNumber)
                                {
                                    LastRunningDigit = LastRunningDigit + 1;
                                }
                                else
                                {
                                    LastRunningDigit = StartingNumber;
                                }
                                // LastRunningDigit = (LastRunningDigit < StartingNumber) ? LastRunningDigit + 1 : (LastRunningDigit == StartingNumber) ? StartingNumber + 1 : StartingNumber;
                                VoucherRunningNumber = LastRunningDigit;
                                formatedValue = VoucherLastNumber = (Prefilwitzero == (int)YesNo.No) ? (Prifix + LastRunningDigit.ToString().PadLeft(int.Parse(NumericalWidth), '0') + Sufix).ToString() : (Prifix + LastRunningDigit + Sufix).ToString();
                                NumberId = this.NumberSet.ToInteger(dvNumb[j][this.AppSchema.VoucherNumber.NUMBER_IDColumn.ColumnName].ToString());
                                VoucherNumberFormatId = this.NumberSet.ToInteger(dvNumb[j][this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn.ColumnName].ToString());
                                VoucherMonth = VoucherNumberFormatmonth;
                                if (!IsTempVoucherNo)
                                    updateLastVoucherNumber(dataManage, this.NumberSet.ToInteger(projectId));
                                break;
                                //}
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(formatedValue))
                {
                    formatedValue = (Prefilwitzero == (int)YesNo.No) ? (Prifix + StartingNumber.ToString().PadLeft(int.Parse(NumericalWidth), '0') + Sufix).ToString() : (Prifix + StartingNumber + Sufix).ToString();
                    VoucherNumberFormat = (Prifix + Sufix).ToString();
                    VoucherRunningNumber = StartingNumber;
                    VoucherNumberFormatId = NumberId;
                    VoucherLastNumber = formatedValue;
                    if (!IsTempVoucherNo)
                        InsertVoucherNumber(dataManage, this.NumberSet.ToInteger(projectId));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return formatedValue;
        }

        //public string getRegeneratedNumber(DataManager dataManage, NumberFormat numberFormatId, string projectId, int TransType, DateTime VoucherDate, int RunningDigit, int ResetMth)//,
        //{
        //    string numbFormat = "";
        //    string formatedValue = "";
        //    string returnValue = "";
        //    string Prifix = "";
        //    string Sufix = "";
        //    int StartingNumber = 0;
        //    int LastRunningDigit = RunningDigit;
        //    //int Month = 0;
        //    //int Duration = 0;
        //    //int VoucherMonth = 0;

        //    DataView dvNoFormat = new DataView();
        //    // To fetch the Active Voucher Number definition
        //    using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchVoucherNumberFormat))
        //    {
        //        dataManager.Database = dataManage.Database;
        //        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId); //Project Id
        //        dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, TransType);//Voucher Type
        //        resultArgs = dataManager.FetchData(DataSource.DataView);
        //    }

        //    if (!resultArgs.Success | resultArgs.RowsAffected < 1)
        //        return returnValue;
        //    dvNoFormat = ((DataView)resultArgs.DataSource.TableView);
        //    if (dvNoFormat[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString() == "")
        //        return returnValue;
        //    Prifix = dvNoFormat[0][this.AppSchema.Voucher.PREFIX_CHARColumn.ColumnName].ToString();
        //    Sufix = dvNoFormat[0][this.AppSchema.Voucher.SUFFIX_CHARColumn.ColumnName].ToString();
        //    StartingNumber = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString());
        //    Prefilwitzero = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.PREFIX_WITH_ZEROColumn.ColumnName].ToString());
        //    NumericalWidth = dvNoFormat[0][this.AppSchema.Voucher.NUMBERICAL_WITHColumn.ColumnName].ToString();
        //    numbFormat = VoucherNumberFormat = (Prifix + Sufix).ToString();
        //    VoucherApplicableMonth = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.MONTHColumn.ColumnName].ToString()) + 1;
        //    VoucherMonth = VoucherNumberFormatmonth = VoucherDate.Month;
        //    VoucherYear = VoucherNumberFormatyear = VoucherDate.Year;
        //    VoucherDuration = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.DURATIONColumn.ColumnName].ToString());

        //    NumberId = (int)numberFormatId;

        //    if (numbFormat == VoucherNumberFormat && VoucherMonth == VoucherNumberFormatmonth && VoucherYear == VoucherNumberFormatyear)// && !IsResetVoucher()  VoucherMonth == VoucherNumberFormatmonth 
        //    {
        //        if (LastRunningDigit >= StartingNumber)
        //        {
        //            LastRunningDigit = LastRunningDigit + 1;
        //        }
        //        else
        //        {
        //            LastRunningDigit = StartingNumber;
        //        }
        //        formatedValue = VoucherLastNumber = (Prefilwitzero == (int)YesNo.No) ? (Prifix + LastRunningDigit.ToString().PadLeft(int.Parse(NumericalWidth), '0') + Sufix).ToString() : (Prifix + LastRunningDigit + Sufix).ToString();
        //        VoucherNumberFormatId = NumberId;
        //        VoucherRunningNumber = LastRunningDigit;
        //        VoucherMonth = ResetMth;
        //        DataView dvNumbFormat = FetchVoucherNumber(dataManage, NumberId.ToString(), numbFormat, projectId);
        //        if (dvNumbFormat.Table.Rows.Count > 0)
        //        {
        //            NumberId = this.NumberSet.ToInteger(dvNumbFormat[0][this.AppSchema.VoucherNumber.NUMBER_IDColumn.ColumnName].ToString());
        //            updateLastVoucherNumber(dataManage, this.NumberSet.ToInteger(projectId));
        //        }
        //        else
        //        {
        //            InsertVoucherNumber(dataManage, this.NumberSet.ToInteger(projectId));
        //        }

        //    }

        //    return formatedValue;
        //}

        private ResultArgs updateLastVoucherNumber(DataManager dataManage, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.UpdateLastVoucherNumber))
            {
                dataManager.Database = dataManage.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_IDColumn, NumberId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, VoucherNumberFormatId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.LAST_VOUCHER_NUMBERColumn, VoucherLastNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn, VoucherRunningNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMATColumn, VoucherNumberFormat);
                dataManager.Parameters.Add(this.AppSchema.Voucher.MONTHColumn, VoucherApplicableMonth);
                dataManager.Parameters.Add(this.AppSchema.Voucher.DURATIONColumn, VoucherDuration);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_MONTHColumn, ResetVoucherMonth);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_YEARColumn, ResetVoucherYear);
                dataManager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs InsertVoucherNumber(DataManager dataManage, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.InsertVoucherNumber))
            {
                dataManager.Database = dataManage.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, VoucherNumberFormatId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.LAST_VOUCHER_NUMBERColumn, VoucherLastNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn, VoucherRunningNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMATColumn, VoucherNumberFormat);
                dataManager.Parameters.Add(this.AppSchema.Voucher.MONTHColumn, VoucherApplicableMonth);
                dataManager.Parameters.Add(this.AppSchema.Voucher.DURATIONColumn, VoucherDuration);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_MONTHColumn, ResetVoucherMonth);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_YEARColumn, ResetVoucherYear);
                dataManager.Parameters.Add(this.AppSchema.Voucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteVoucherNumberFormat(NumberFormat numberFormatId, string projectId, int ResetMth, int ResetYear, Int32 voucherdefinitionid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.DeleteVoucherNumberFormat))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, (int)numberFormatId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_MONTHColumn, ResetMth);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_YEARColumn, ResetYear);
                //dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMATColumn, NumbFormat);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, voucherdefinitionid);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private DataView FetchVoucherNumber(DataManager dataManage, string VoucherNumberFormatId, string NumbFormat, string projectid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchVoucherNumberFormatExist))
            {
                dataManager.Database = dataManage.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, VoucherNumberFormatId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_MONTHColumn, ResetVoucherMonth);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_YEARColumn, ResetVoucherYear);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMATColumn, NumbFormat);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectid);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.VOUCHER_DEFINITION_IDColumn, VoucherDefinitionId);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs.DataSource.TableView;
        }

        public ResultArgs GetResetStartMonth()
        {
            try
            {
                DataTable dtMonthYear = new DataTable();
                dtMonthYear.Columns.Add("Month", typeof(int));
                dtMonthYear.Columns.Add("Year", typeof(int));
                dtMonthYear.Columns.Add("YearMonth", typeof(string));

                DateTime dtFrom = this.DateSet.ToDate(this.YearFrom, false);
                DateTime dtTo = this.DateSet.ToDate(this.YearTo, false);

                VoucherDuration = VoucherDuration > 0 ? VoucherDuration : 1;
                //To get all the start month and Year of active the accounting period based on the Voucher Duration
                do
                {
                    dtMonthYear.Rows.Add(dtFrom.Month, dtFrom.Year, string.Concat(dtFrom.Year, dtFrom.Month <= 9 && dtFrom.Month > 0 ? "0" + dtFrom.Month.ToString() : dtFrom.Month.ToString()));
                }
                while ((dtFrom = dtFrom.AddMonths(VoucherDuration)) <= dtTo);

                if (dtMonthYear != null && dtMonthYear.Rows.Count > 0)
                {
                    DataView dvMonthYear = dtMonthYear.DefaultView;
                    //Concatenated Voucher Year and Voucher Month for filtering.
                    string MthYear = string.Concat(VoucherYear.ToString(), VoucherMonth > 0 && VoucherMonth <= 9 ? "0" + VoucherMonth.ToString() : VoucherMonth.ToString());
                    dvMonthYear.RowFilter = "YEARMONTH<='" + MthYear + "'";  // Get the start month and end month
                    if (dvMonthYear != null && dvMonthYear.Count > 0)
                    {
                        DataTable dtResult = dvMonthYear.ToTable().AsEnumerable().Reverse().Take(1).CopyToDataTable();
                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            resultArgs.Success = true;
                            resultArgs.DataSource.Data = dtResult;
                        }
                    }
                    else
                    {
                        // If start month and Year not available, Accounting Year from month and year is taken
                        DataTable dtTemp = dtMonthYear.Clone();
                        dtTemp.Rows.Add(dtFrom.Month, dtFrom.Year, MthYear);
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            resultArgs.Success = true;
                            resultArgs.DataSource.Data = dtTemp;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Success = false;
            }
            return resultArgs;
        }

        public ResultArgs GetResetEndMonth()
        {
            try
            {
                DataTable dtMonthYear = new DataTable();
                dtMonthYear.Columns.Add("Month", typeof(int));
                dtMonthYear.Columns.Add("Year", typeof(int));
                dtMonthYear.Columns.Add("YearMonth", typeof(string));

                DateTime dtFrom = this.DateSet.ToDate(this.YearFrom, false);
                DateTime dtTo = this.DateSet.ToDate(this.YearTo, false);
                DateTime dtTempDate = this.DateSet.ToDate(this.YearFrom, false);

                //To get all the end month and Year of active the accounting period based on the Voucher Duration
                while ((dtFrom = dtFrom.AddMonths(VoucherDuration).AddMonths(-1)) <= dtTo)
                {
                    dtMonthYear.Rows.Add(dtFrom.Month, dtFrom.Year, string.Concat(dtFrom.Year, dtFrom.Month <= 9 && dtFrom.Month > 0 ? "0" + dtFrom.Month.ToString() : dtFrom.Month.ToString()));
                    dtFrom = dtFrom.AddMonths(1);
                }

                if (dtMonthYear != null && dtMonthYear.Rows.Count > 0)
                {
                    DataView dvMonthYear = dtMonthYear.DefaultView;
                    string MthYear = string.Concat(VoucherYear.ToString(), VoucherMonth > 0 && VoucherMonth <= 9 ? "0" + VoucherMonth.ToString() : VoucherMonth.ToString());
                    dvMonthYear.RowFilter = "YEARMONTH >='" + MthYear + "'";
                    if (dvMonthYear != null && dvMonthYear.Count > 0)
                    {
                        DataTable dtResult = dvMonthYear.ToTable().AsEnumerable().Take(1).CopyToDataTable();
                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            resultArgs.Success = true;
                            resultArgs.DataSource.Data = dtResult;
                        }
                    }
                    else
                    {
                        //If End month and Year not available for the duration, Accounting Date To month and year is taken.
                        DataTable dtTemp = dtMonthYear.Clone();
                        dtTemp.Rows.Add(dtTo.Month, dtTo.Year, MthYear);
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            resultArgs.Success = true;
                            resultArgs.DataSource.Data = dtTemp;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Success = false;
            }
            return resultArgs;
        }

        /// <summary>
        /// To check the  Voucher Number  wheather to reset or not.
        /// </summary>
        /// <returns></returns>
        private bool IsResetVoucher()
        {
            bool isReset = false;
            DateTime dtFrom = this.DateSet.ToDate(this.YearFrom, false);
            DateTime dtTo = this.DateSet.ToDate(this.YearTo, false);
            do
            {
                if (VoucherYear == dtFrom.Year && VoucherMonth == dtFrom.Month)
                {
                    isReset = true;
                    break;
                }
            }
            while ((dtFrom = dtFrom.AddMonths(VoucherDuration)) <= dtTo);
            return isReset;
        }

        /// <summary>
        /// To get the reset month 
        /// </summary>
        /// <returns></returns>
        private int GetResetMonth(string transType, DateTime voucherDate)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchLastResetMonth))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, this.DateSet.ToDate(this.YearFrom, false));
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.VOUCHER_DATEColumn, voucherDate);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, transType);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
    }

    public static class SequenceNumber
    {
        private static int SerialNumber = 1;
        public static int GetSequenceNumber()
        {
            return SerialNumber++;
        }
        public static void ReSetSequenceNumber()
        {
            SerialNumber = 1;
        }
    }
}
