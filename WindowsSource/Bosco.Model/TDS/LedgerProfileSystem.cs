using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;
using Bosco.Model.Business;

namespace Bosco.Model.TDS
{
    public class LedgerProfileSystem : SystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        int DefaultValue = 0;
        #endregion

        #region Properties
        public int LedgerID { get; set; }
        public int NatureofPaymentsId { get; set; }
        public int CreditorsProfileId { get; set; }
        public int BankTransId { get; set; }
        public int LedgerProfileId { get; set; }
        public bool isBankDetails { get; set; }
        public string NickName { get; set; }
        public string FavouringName { get; set; }
        public string BankName { get; set; }
        public string BankAcNo { get; set; }
        public string IFSNo { get; set; }
        public string MailName { get; set; }
        public string MailAddress { get; set; }
        public string PANNo { get; set; }
        public string PANName { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string PinCode { get; set; }
        public string SalesNo { get; set; }
        public int ProfileGStId { get; set; }
        public string GSTNo { get; set; }
        public int GSTServiceType { get; set; }
        public string ProfileGSts { get; set; }

        public string CSTNo { get; set; }
        public string MobileNumber { get; set; }
        public string ContactPerson { get; set; }
        public int BankTransType { get; set; }
        public DataTable dtLedgerProfile { get; set; }
        public TDSLedgerTypes tdsLedgerTypes { get; set; }
        #endregion

        #region Constructor
        public LedgerProfileSystem()
        {

        }

        public LedgerProfileSystem(int LedgerId)
        {
            FillLedgerProfileDetails(LedgerId);
        }
        #endregion

        #region Methods
        public ResultArgs SaveLedgeProfile(DataManager dataManagerLedgerProfile)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerProfile.Add))
            {
                dataManager.Database = dataManagerLedgerProfile.Database;
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.CREDITORS_PROFILE_IDColumn, CreditorsProfileId);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.LEDGER_IDColumn, LedgerID);
                if (!tdsLedgerTypes.Equals(TDSLedgerTypes.SunderyCreditors))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn, DefaultValue);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.NATURE_OF_PAYMENT_IDColumn, NatureofPaymentsId);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn, NatureofPaymentsId);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.NATURE_OF_PAYMENT_IDColumn, DefaultValue);
                }
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.IS_BANK_DETAILSColumn, isBankDetails ? 1 : 0);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.NICK_NAMEColumn, NickName);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.FAVOURING_NAMEColumn, FavouringName);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.TRANSACTION_TYPEColumn, DefaultValue);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.BANK_NAMEColumn, BankName);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.ACCOUNT_NUMBERColumn, BankAcNo);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.IFS_CODEColumn, IFSNo);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.NAMEColumn, FavouringName);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.State.STATE_IDColumn, State);
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, Country);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.GST_IdColumn, ProfileGStId);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.GST_NOColumn, GSTNo);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.PAN_NUMBERColumn, PANNo);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.PAN_IT_HOLDER_NAMEColumn, PANName);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.PIN_CODEColumn, PinCode);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.SALES_TAX_NOColumn, SalesNo);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.CST_NUMBERColumn, CSTNo);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.CONTACT_PERSONColumn, ContactPerson);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.CONTACT_NUMBERColumn, MobileNumber);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.EMAILColumn, Email);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerProfile(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerProfile.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private void FillLedgerProfileDetails(int LedgerID)
        {
            resultArgs = FetchLedgerProfile(LedgerID);
            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                NatureofPaymentsId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn.ColumnName].ToString()) : 0;
                LedgerProfileId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                CreditorsProfileId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.CREDITORS_PROFILE_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.CREDITORS_PROFILE_IDColumn.ColumnName].ToString()) : 0;
                NickName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.NICK_NAMEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.NICK_NAMEColumn.ColumnName].ToString() : string.Empty;
                FavouringName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.FAVOURING_NAMEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.FAVOURING_NAMEColumn.ColumnName].ToString() : string.Empty;
                BankTransType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.TRANSACTION_TYPEColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.TRANSACTION_TYPEColumn.ColumnName].ToString()) : 0;
                BankName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.BANK_NAMEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.BANK_NAMEColumn.ColumnName].ToString() : string.Empty;
                IFSNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.IFS_CODEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.IFS_CODEColumn.ColumnName].ToString() : string.Empty;
                BankAcNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.ACCOUNT_NUMBERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.ACCOUNT_NUMBERColumn.ColumnName].ToString() : string.Empty;
                MailName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.NAMEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.NAMEColumn.ColumnName].ToString() : string.Empty;
                MailAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.ADDRESSColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.ADDRESSColumn.ColumnName].ToString() : string.Empty;
                State = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.STATEColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.STATEColumn.ColumnName].ToString()) : 0;
                PinCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PIN_CODEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PIN_CODEColumn.ColumnName].ToString() : string.Empty;
                PANNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PAN_NUMBERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PAN_NUMBERColumn.ColumnName].ToString() : string.Empty;
                PANName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PAN_IT_HOLDER_NAMEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.PAN_IT_HOLDER_NAMEColumn.ColumnName].ToString() : string.Empty;
                SalesNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.SALES_TAX_NOColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.SALES_TAX_NOColumn.ColumnName].ToString() : string.Empty;
                CSTNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.CST_NUMBERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.CST_NUMBERColumn.ColumnName].ToString() : string.Empty;
                ProfileGStId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.GST_IdColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.GST_IdColumn.ColumnName].ToString()) : 0;
                int isBankDetailsInfo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.IS_BANK_DETAILSColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfileData.IS_BANK_DETAILSColumn.ColumnName].ToString()) : 0;
                isBankDetails = isBankDetailsInfo > 0 ? true : false;
            }
        }

        public ResultArgs DeleteLedgerProfile()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerProfile.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.LEDGER_IDColumn, LedgerID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteLedgerProfileByGST()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerProfile.DeleteGStProfile))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.LEDGER_IDColumn, LedgerID);
                //  dataManager.Parameters.Add(this.AppSchema.LedgerProfileData.GST_IdColumn, ProfileGStId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        public int HasPanNumber()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerProfile.CheckPANNumber))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerID);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion
    }
}
