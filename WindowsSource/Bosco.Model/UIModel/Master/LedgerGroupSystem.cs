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
    public class LedgerGroupSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int GroupId { get; set; }
        public string Abbrevation { get; set; }
        public string Group { get; set; }
        public int ParentGroupId { get; set; }
        public int NatureId { get; set; }
        public int MainGroupId { get; set; }
        public string GroupIds { get; set; }
        public int ImageId { get; set; }
        public int SortOrder { get; set; }

        public int GeneralateId { get; set; }
        public string GeneralateCode { get; set; }
        public string GeneralateName { get; set; }
        public int GenParentId { get; set; }
        public int GenMainParentId { get; set; }



        #endregion

        #region Constructor
        public LedgerGroupSystem()
        {

        }

        public LedgerGroupSystem(int LedgerGroupId)
        {
            FillGroupProperties(LedgerGroupId);
        }
        #endregion

        public ResultArgs SaveLedgerGroupDetails()
        {
            using (DataManager dataManager = new DataManager((GroupId == 0) ? SQLCommand.LedgerGroup.Add : SQLCommand.LedgerGroup.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, Abbrevation);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, Group);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn, NatureId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, MainGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.IMAGE_IDColumn, ImageId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.SORT_ORDERColumn, SortOrder);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveGeneralateLedgerGroupDetails()
        {
            using (DataManager dataManager = new DataManager((GeneralateId == 0) ? SQLCommand.LedgerGroup.AddGeneralate : SQLCommand.LedgerGroup.UpdateGeneralate))
            {
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_CODEColumn, GeneralateCode);
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_NAMEColumn, GeneralateName);
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_PARENT_LEDGER_IDColumn, GenParentId);
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_MAIN_PARENT_IDColumn, GenMainParentId);
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn, GeneralateId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs UpdateParentGroupId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.UpdateParentGroupId))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, MainGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateGeneralateParentGroupId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.UpdateGeneralateParentGroupId))
            {
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_PARENT_LEDGER_IDColumn, GenParentId);
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_MAIN_PARENT_IDColumn, GenMainParentId);
                dataManager.Parameters.Add(this.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn, GeneralateId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs GetLedgerGroupSource()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs GetLedgerList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerList))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        public ResultArgs LoadLedgerGroupSource()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchforLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }


        public ResultArgs FetchLedgerGroupByNature(Int32 natureid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerGroupByNature))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn, natureid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerGroupNature()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerGroupNature))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadLedgerGroupforLedgerLoodkup(ledgerSubType ledgerType)
        {
            if (ledgerType == ledgerSubType.GN)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchforLedgerLookup))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else if (ledgerType == ledgerSubType.FD)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchFDLedger))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchforLedgerLookup))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }

            return resultArgs;
        }

        public ResultArgs GetLedgerGroupByIdList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchByGroupId))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, GroupIds);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs DeleteLedgerGroup(int LedgerGroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int GetNatureId(int LedgerGroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchNatureId))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs ValidateGroupId(int LedgerGroupId)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchValidateGroup))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public int GetAccessFlag(int LedgerGroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchAccessFlag))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs UpdateImageIndex(int GroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.UpdateImageIndex))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private void FillGroupProperties(int LedgerGroupId)
        {
            resultArgs = GetLedgerGroupById(LedgerGroupId);
            if (resultArgs.RowsAffected > 0)
            {
                Abbrevation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName].ToString();
                Group = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                ParentGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ColumnName].ToString());
                NatureId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString());
                MainGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn.ColumnName].ToString());
                ImageId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.IMAGE_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs GetLedgerGroupById(int LedgerGroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        public ResultArgs LoadFDLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchFDLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs GetAccountType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchAccoutType))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        public ResultArgs FecthLedgerGroupCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerGroupCodes))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FecthLedgerGroupByExistingCode(string Groupcode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerGroupByGroupCode))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, Groupcode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int FetchSortOrder()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchSortOrder))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int FetchMainGroupSortOrder()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchMainGroupSortOrder))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, ParentGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs GetLedgerGroupIdByGroupName(string GroupName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.GetLedgerGroupId))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, GroupName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            return resultArgs;
        }

        public Int32 GetGroupIdByGroupName(string GroupName)
        {
            Int32 groupid = 0;
            resultArgs = GetLedgerGroupIdByGroupName(GroupName);
            if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
            {
                groupid = resultArgs.DataSource.Sclar.ToInteger;
            }
            return groupid;
        }

        public ResultArgs UpdateLedgerGroupByLedgerGroupId( Int32 GroupId, string GroupName, DataManager updateDataManager=null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.UpdateLedgerGroupByLedgerGroupId))
            {
                if (updateDataManager != null) dataManager.Database = updateDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, GroupName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        /// <summary>
        /// On 14/08/2024, For other country of India, They Don't have Fixed Depoist, they have only investments
        /// # Change (Fixed Deposits) default group as "Investments"
        /// # If already "Investments" exists, change "Investments" to "Investments (General)"
        /// # If "Investments (General)" Change "Investments" with its Group Id
        /// </summary>
        /// <returns></returns>
        public ResultArgs ChangeFixedDepositToInvestments()
        {
            string ledgergroup_fixeddeposits = "Fixed Deposits";
            string ledgergroup_investments = "Investments";
            string ledgergroup_investments_General = "Investments (General)";
            Int32 ledgergroup_investments_id = 0;
            Int32 ledgergroup_investments_General_id = 0;

            if (this.IsCountryOtherThanIndia)
            {
                Int32 LedgerGroupId = (Int32)FixedLedgerGroup.FixedDeposit;
                if (LedgerGroupId > 0)
                {
                    FillGroupProperties(LedgerGroupId);

                    if (Group.ToString().ToUpper() == ledgergroup_fixeddeposits.ToUpper())
                    {
                        ledgergroup_investments_id = GetGroupIdByGroupName(ledgergroup_investments);
                        ledgergroup_investments_General_id = GetGroupIdByGroupName(ledgergroup_investments_General);
                        resultArgs.Success = true;

                        using (DataManager updateDM = new DataManager())
                        {
                            updateDM.BeginTransaction();
                            //# Change "Investments" to "Investments (General)"
                            if (ledgergroup_investments_id > 0)
                            {
                                if (ledgergroup_investments_General_id == 0) //"Investments (General)" not found 
                                    resultArgs = UpdateLedgerGroupByLedgerGroupId(ledgergroup_investments_id, ledgergroup_investments_General, updateDM);
                                else //"Investments (General)" exists, 
                                    resultArgs = UpdateLedgerGroupByLedgerGroupId(ledgergroup_investments_id, ledgergroup_investments + "_" + ledgergroup_investments_id.ToString(), updateDM);
                            }

                            //Change Fixed Deposits to Investments
                            if (resultArgs.Success)
                            {
                                resultArgs = UpdateLedgerGroupByLedgerGroupId((Int32)FixedLedgerGroup.FixedDeposit, ledgergroup_investments, updateDM);
                            }

                            if (!resultArgs.Success)
                            {
                                updateDM.TransExecutionMode = ExecutionMode.Fail;
                            }

                            updateDM.EndTransaction();
                        }
                    }
                }
            }

            return resultArgs;
        }

    }
}