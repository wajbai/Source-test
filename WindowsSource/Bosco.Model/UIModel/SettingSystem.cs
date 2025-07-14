using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using Bosco.Utility.ConfigSetting;
using System.IO;

namespace Bosco.Model
{
    public class SettingSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        public SettingSystem()
        {
        }

        public ResultArgs SaveSettingDetails(DataTable dvSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdate))
            {
                for (int i = 0; i < dvSetting.Rows.Count; i++)
                {
                    dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, dvSetting.Rows[i][1]);
                    dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, dvSetting.Rows[i][2]);

                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        //Keep the setting Info into session
                        this.SettingInfo = dvSetting.DefaultView;
                    }
                    else
                    {
                        this.SettingInfo = null;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchSettingDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.Fetch))
            {
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }
        public string FetchCurrentDate()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.Setting.FetchCurrentDate))
            {
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data.ToString();
        }

        /// <summary>
        /// On 24/01/2024, To process Acmeerp Diagnose Details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ProcessAcmeerpDiagnoseDetails(bool optimizeMainTables = false)
        {
            string rtn = string.Empty;
            string linebreak = System.Environment.NewLine + System.Environment.NewLine;
            resultArgs = new ResultArgs();

            try
            {
                //> License Key Expiried
                if (IsLicenseExpired)
                {
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> License Key is expired.";
                }

                //> License key mismatching with databse
                if (IsLicenseKeyMismatchedByHoProjects)
                {
                    //Current Acme.erp Branch Database Projects are mismatching with Acme.erp Portal Projects.
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> License Key is mismatching with Database.";
                }

                //> License key Location mismatching with databse Location
                if (IsLicenseKeyMismatchedByLicenseKeyDBLocation)
                {
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> License Key Location is mismatching with Database Location " +
                              BranchLocations + " (" + Location + ").";
                }
                
                //> Check Books Begin
                if (DateSet.ToDate(BookBeginFrom, false) != FirstFYDateFrom)
                {
                    rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> Mismatching between Books Begining and First Finance Year " +
                            DateSet.ToDate(BookBeginFrom, false).ToShortDateString() + " (" + FirstFYDateFrom.ToShortDateString() + ").";
                }

                //> Diagnose Transaction Period
                using (AccouingPeriodSystem acccountransperiod = new AccouingPeriodSystem())
                {
                    resultArgs = acccountransperiod.VerifyAccountingPeriods();
                    if (!resultArgs.Success)
                    {
                        rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + resultArgs.Message;
                    }
                }

                //> Checking Ledgers and its Mapping
                //1. Diagnose Ledger Opening Balance which are not mapped wtih concern Project
                //2. Diagnose Voucher Ledgers which are not mapped wtih concern Project
                //3. Diagnose FD Ledger Opening Balance which are correct with FD Opening Account
                //4. Diagnose list down more than opening balacne date
                //5. Diagnose Project Budget Ledgers which are not mapped wtih concern Project
                //6. Diagnose Budget Ledgers which are not mapped wtih concern Project Budget Ledger
                using (LedgerSystem legersystem = new LedgerSystem())
                {

                    //Clear Invlaid Ledger Balance Data
                    resultArgs = legersystem.ClearInvalidLedgerBalanceData();
                    if (resultArgs.Success)
                    {
                        if (resultArgs.RowsAffected > 0)
                        {
                            string msg = "Few Invalid Ledger Balance details are removed.";
                            rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + msg;
                        }
                    }

                    //1. Diagnose Ledger Opening Balance which are not mapped wtih concern Project
                    resultArgs = legersystem.FetchLedgerOpeningBalanceNotMappedWithProject();
                    if (resultArgs.Success)
                    {
                        DataTable dt = resultArgs.DataSource.Table;
                        dt.DefaultView.RowFilter = AppSchema.LedgerBalance.AMOUNTColumn.ColumnName + ">0";
                        if (dt.DefaultView.Count > 0)
                        {
                            string msg = "Few Ledger(s) have Opening Balance but those Ledger(s) are not mapped with concern Project(s).";
                            rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + msg;
                        }
                    }

                    //2. Diagnose Voucher Ledgers which are not mapped wtih concern Project
                    resultArgs = legersystem.FetchVoucherLedgersNotMappedWithProject();
                    if (resultArgs.Success)
                    {
                        DataTable dt = resultArgs.DataSource.Table;
                        if (dt.Rows.Count > 0)
                        {
                            string msg = "Few Ledger(s) have Vouchers details but those Ledger(s) are not mapped with concern Project(s).";
                            rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + msg;
                        }
                    }

                    //3. Diagnose FD Ledger Opening Balance which are not correct with FD Opening Account
                    resultArgs = legersystem.FetchFDLedgersMismatchingOpeningBalance();
                    if (resultArgs.Success)
                    {
                        DataTable dt = resultArgs.DataSource.Table;
                        if (dt.Rows.Count > 0)
                        {
                            string msg = "Few FD Ledger(s) Opening Balance are mismatching with FD Opening Account.";
                            rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + msg;
                        }
                    }

                    //4. Diagnose list down more than opening balacne date
                    resultArgs = legersystem.FetchMorethanOneLedgerBalanceDate();
                    if (resultArgs.Success)
                    {
                        DataTable dt = resultArgs.DataSource.Table;
                        if (dt.Rows.Count > 1)
                        {
                            string msg = "More than one Ledger opening balance Date is available.";
                            rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + msg;
                        }
                    }

                    //5. Diagnose Project Budget Ledgers which are not mapped wtih concern Project
                    resultArgs = legersystem.FetchProjectBudgetLedgersNotMappedWithProject();
                    if (resultArgs.Success)
                    {
                        DataTable dt = resultArgs.DataSource.Table;
                        if (dt.Rows.Count > 0)
                        {
                            string msg = "Few Ledger(s) are mapped in Project Budget Ledger but those Ledger(s) are not mapped with concern Project(s).";
                            rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + msg;
                        }
                    }

                    //6. Diagnose Budget Ledgers which are not mapped wtih concern Project Budget Ledger
                    resultArgs = legersystem.FetchBudgetLedgersNotMappedWithProjectBudgetLedger();
                    if (resultArgs.Success)
                    {
                        DataTable dt = resultArgs.DataSource.Table;
                        if (dt.Rows.Count > 0)
                        {
                            string msg = "Few Ledger(s) have Budget details but those Ledger(s) are not mapped with concern Project Budget Ledger.";
                            rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + msg;
                        }
                    }

                   


                    if (resultArgs.Success)
                    {
                        if (optimizeMainTables)
                        {
                            using (VoucherTransactionSystem vouchertranssystem = new VoucherTransactionSystem())
                            {
                                resultArgs = vouchertranssystem.OptimizeMainTables();
                            }
                        }
                    }

                    
                }
            }
            catch (Exception err)
            {
                rtn += (string.IsNullOrEmpty(rtn) ? string.Empty : linebreak) + "> " + err.Message;
            }
            finally
            {
                resultArgs.Success = true;
                if (!string.IsNullOrEmpty(rtn))
                {
                    resultArgs.Message = rtn;
                }
            }

            return resultArgs;
        }

        /// <summary>
        /// On 06/05/2024, To fetch multi db xml details 
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchMultiDBXMLConfigurationInAcperp()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.FetchMultiDBXMLConfigurationInAcperp))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
             

        /// <summary>
        /// On 06/05/2024, To fetch multi db xml details 
        /// </summary>
        /// <returns></returns>
        public ResultArgs UpdateMultiDBXMLConfigurationFromBackup()
        {
            bool acperpdbexists = !IsExistsDefaultAcmeerpDB();
            if (this.AccesstoMultiDB == 1 && File.Exists(SettingProperty.RestoreMultipleDBPath) && acperpdbexists)
            {
                resultArgs = FetchMultiDBXMLConfigurationInAcperp();
                if (resultArgs.Success && resultArgs.DataSource.Table!=null )
                {
                    DataTable dtBackupMultDBXML = resultArgs.DataSource.Table;
                    if (dtBackupMultDBXML.Rows.Count > 0 )
                    {
                        string backupmutidbxml = dtBackupMultDBXML.Rows[0]["MULTI_DB_XML"].ToString();
                        if (!string.IsNullOrEmpty(backupmutidbxml))
                        {
                            using (StreamWriter strmwriter = new StreamWriter(SettingProperty.RestoreMultipleDBPath))
                            {
                                strmwriter.Write(backupmutidbxml);
                            }
                        }
                    }
                }
            }

            return resultArgs;
        }

        public bool IsExistsDefaultAcmeerpDB()
        {
            bool rtn = false;
            using (BackupAndRestore backuprestore = new BackupAndRestore())
            {
                rtn = backuprestore.isACPERPDatabase("acperp");
            }
            return rtn;
        }

        /// <summary>
        /// On 03/05/2024, Update Multi DB xml file into database
        /// </summary>
        public ResultArgs UpdateMultiDBXMLConfigurationInAcperp()
        {
            string multidbxml = string.Empty;
            bool acperpdbexists = !IsExistsDefaultAcmeerpDB();

            if (this.AccesstoMultiDB == 1 && File.Exists(SettingProperty.RestoreMultipleDBPath) && acperpdbexists)
            {
                resultArgs = FetchMultiDBXMLConfigurationInAcperp();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtBackupMultDBXML = resultArgs.DataSource.Table;
                    bool exists = (dtBackupMultDBXML.Rows.Count > 0);

                    using (StreamReader strmreader = new StreamReader(SettingProperty.RestoreMultipleDBPath))
                    {
                        multidbxml = strmreader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(multidbxml))
                    {
                        //It will be updated in acmeerp default base database (acperp)
                        using (DataManager datamanager = new DataManager(exists ? SQLCommand.Setting.UpdateMultiDBXMLConfigurationInAcperp
                                                : SQLCommand.Setting.InsertMultiDBXMLConfigurationInAcperp))
                        {
                            datamanager.Parameters.Add("MULTI_DB_XML", multidbxml, DataType.String);
                            resultArgs = datamanager.UpdateData();
                        }

                        if (resultArgs.Success)
                        {
                            AcMELog.WriteLog("Updated Multi DB XML file into acperp database");
                        }
                    }
                }
             }

            if (!acperpdbexists)
            {
                AcMELog.WriteLog("Acmeerp default database 'acperp' is not found");
            }
            return resultArgs;
        }
    }
}
