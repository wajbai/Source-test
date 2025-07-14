using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Data;
using AcMEDSync.Model;
namespace Bosco.Model.UIModel
{
    public class AccouingPeriodSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AccouingPeriodSystem()
        {

        }
        public AccouingPeriodSystem(int accPeriodId)
        {
            AccPeriodId = accPeriodId;
            FillAccountingPeriodDetails();
        }
        #endregion

        #region Properties
        public int AccPeriodId { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public int Status { get; set; }
        public string BooksBeginingDate { get; set; }
        public bool IsFirstAccYear { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchAccountingPeriodDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckAccountingPeriod()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.CheckAccountingPeriod))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs VerifyAccountingPeriods()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.VerifyAccountingPeriods))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, FirstFYDateFrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtFYs = resultArgs.DataSource.Table;

                    dtFYs.DefaultView.RowFilter = "PYEAR_FROM=0 OR PYEAR_TO=0";
                    if (dtFYs.DefaultView.Count > 0)
                    {
                        resultArgs.Message = "Few Finance Year(s) are invalid in date range or duplicated.";
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs IsAccountingPeriodExists()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.IsAccountingPeriodExists))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckIsTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.CheckIstransacton))
            {
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAccountingPeriodDetailsForSettings()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchForSettings))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchAllDetails))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteAccountingPeriodDetials()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
                resultArgs = dataManager.UpdateData();
            }

            //#On 30/04/2021, Assign All FYs
            if (resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                resultArgs = FetchAccountingPeriodDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    this.AllAccountingPeriods = resultArgs.DataSource.Table;
                }
            }
            return resultArgs;
        }

        public ResultArgs SaveAccountingPeriodDetails(DataManager dmanager = null, bool updatesigndetails = true)
        {
            bool newaccyear = (AccPeriodId == 0);
            using (DataManager dataManager = new DataManager((AccPeriodId == 0) ? SQLCommand.AccountingPeriod.Add : SQLCommand.AccountingPeriod.Update))
            {
                if (dmanager != null)
                {
                    dataManager.Database = dmanager.Database;
                }
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId, true);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.STATUSColumn, Status);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    AccPeriodId = (Convert.ToInt32(resultArgs.RowUniqueId.ToString()) == 0) ? AccPeriodId : Convert.ToInt32(resultArgs.RowUniqueId.ToString());
                    int IsAccYear = IsFirstAccountingYear();
                    if (IsFirstAccYear == true || IsAccYear > 0)
                    {
                        UpdateBooksBeginningFrom(dmanager);
                    }

                    //Insert sign details from previous year
                    if (newaccyear && updatesigndetails)
                    {
                        SaveAccountingSignDetails(dmanager);
                    }

                    //Insert Auditor Note sign from previous year
                    if (newaccyear && updatesigndetails)
                    {
                        SaveAccountingAuditorNoteSign(dmanager);
                    }

                    //On 23/08/2024, to Update Exchagne Rate---------------------------
                    if (AllowMultiCurrency == 1 && newaccyear)
                    {
                        InsertACCountryCurrencyExchangeRate(dmanager);
                    }

                    //-----------------------------------------------------------------

                    //#On 30/04/2021, Assign All FYs
                    resultArgs = FetchAccountingPeriodDetails();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        this.AllAccountingPeriods = resultArgs.DataSource.Table;
                    }
                }
            }
            return resultArgs;
        }

        public void SaveAccountingSignDetails(DataManager dmanager = null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertACSignDetails))
            {
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                if (dmanager != null)
                {
                    dataManager.Database = dmanager.Database;
                }
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
                dataManager.UpdateData();
            }
        }


        public void SaveAccountingAuditorNoteSign(DataManager dmanager = null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertACAuditorNoteSignDetails))
            {
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                if (dmanager != null)
                {
                    dataManager.Database = dmanager.Database;
                }
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
                dataManager.UpdateData();
            }
        }

        public void InsertACCountryCurrencyExchangeRate(DataManager dmanager = null)
        {
            using (CountrySystem countryrate = new CountrySystem())
            {
                resultArgs = countryrate.InsertACCountryCurrencyExchangeRate(AccPeriodId, YearFrom, YearTo, dmanager);
            }
        }

        public ResultArgs DeleteACCountryCurrencyExchangeRate(string applicablefrom, string applicableto)
        {
            using (CountrySystem countryrate = new CountrySystem())
            {
                resultArgs = countryrate.DeleteACCountryCurrencyExchangeRate(applicablefrom, applicableto, null);
            }
            return resultArgs;
        }

        public ResultArgs DeleteAccountingSignDetails()
        {
            string msg = string.Empty;

            /*//1. Check Vouchers available for selected FY
            resultArgs = CheckIsTransaction();
            if (resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                resultArgs.Message = MessageRender.GetMessage(MessageCatalog.Master.AccountingPeriod.ACCOUNTING_PERIOD_CANNOT_DELETE);
            }

            //2. Check FY period in between FYs
            if (resultArgs.Success)
            {
                resultArgs = CheckAccountingPeriod();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    resultArgs.Message = "Cannot delete in between Accounting Period.";
                }
            }*/
            
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteACSignDetails))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs DeleteAccountingAuditorNoteSign()
        {
            string msg = string.Empty;

            using (DataManager dataManager = new DataManager(SQLCommand.Setting.DeleteAuditorNoteSign))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
                resultArgs = dataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs DeleteReportFYDevelopmentalBudgetDetails()
        {
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                budgetsystem.DeleteDevelopmentalNewProjects(0, AccPeriodId.ToString(), null);
            }
            return resultArgs;
        }

        public ResultArgs UpdateBooksBeginningFrom(DataManager dmanager = null)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.UpdateBooksbeginningDate))
            {
                if (dmanager != null)
                {
                    dataManager.Database = dmanager.Database;
                }

                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BooksBeginingDate);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs FillAccountingPeriodDetails()
        {
            resultArgs = AccountingYearId();
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                YearFrom = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString();
                YearTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        private ResultArgs AccountingYearId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.Fetch))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName, AccPeriodId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private int IsFirstAccountingYear()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchIsFirstAccountingyear))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName, AccPeriodId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchBooksBeginingFrom()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchBooksBeginingFrom))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchYearTo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchTransactionYearTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchPreviousYearAC(string currentyearfrom)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchPreviousYearAC))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, currentyearfrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ValidateBooksBeginning()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.ValidateBooksBegining))
            {
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BookBeginFrom);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveTransactionPeriod()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchActiveTransactionperiod))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 05/12/2024, Sometimes active FY is reset even if there are FYs exists
        /// </summary>
        /// <returns></returns>
        public ResultArgs ValidateAndSetActiveFY()
        {
           ResultArgs result  = FetchActiveTransactionPeriod();
           if (result != null && result.Success)
            {
                if (result.RowsAffected == 0)
                {
                    result = FetchAccountingPeriodDetails();
                    if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                     {
                         DataTable dt = result.DataSource.Table;
                         dt.DefaultView.Sort = this.AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName + " DESC";
                         Int32 accid = NumberSet.ToInteger(dt.DefaultView[0][this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName].ToString());
                         if (accid > 0)
                         {
                             using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.UpdateStatus))
                             {
                                 dataManager.Database = dataManager.Database;
                                 dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, accid);

                                 result = dataManager.UpdateData();
                             }
                         }
                     }
                }
            }
           return result;
        }

        public ResultArgs FetchRecentProjectDetails(string UserId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FecthRecentProjectDetails))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, UserId);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchRecentVoucherDate(int Projectid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchRecentVoucherDate))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Projectid);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckExistingDB()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.CheckExistingDB))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        /// <summary>
        /// On 05/05/2021, To generate FYs still given Date
        /// 1. Get actuall FY date rnage for given date
        /// 2. 
        /// 3.
        /// 4.
        /// </summary>
        /// <param name="ValidatePeriodDate"></param>
        /// <returns></returns>
        public ResultArgs GenerateFYPeriods(string ValidatePeriodDate)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                bool IsAprilMarch = DateSet.IsAprilMarchFYBranch(base.YearFrom);
                //ValidatePeriodDate = UtilityMember.DateSet.ToDate("01/01/2017",false);
                //As on 05/05/2021, to get based on FY April-March - Jan to Dec
                //DateTime  FYdate = UtilityMember.DateSet.GetFinancialYearByDate(ValidatePeriodDate);
                DateTime FYdate = DateSet.GetFinancialYearByDate(DateSet.ToDate(ValidatePeriodDate, false), base.YearFrom);

                //New FY date from is greater than target branch db's last FYs date from
                if (FYdate > LastFYDateFrom)
                {
                    int YearCount = FYdate.Year - LastFYDateFrom.Year;
                    DateTime lastFYDateFromAddYears = LastFYDateFrom;
                    for (int i = 0; i <= YearCount; i++)
                    {
                        AccPeriodId = 0;
                        YearFrom = lastFYDateFromAddYears.AddYears(i).ToShortDateString();
                        YearTo = DateSet.ToDate(YearFrom, false).AddMonths(12).AddDays(-1).ToShortDateString();

                        result = IsAccountingPeriodExists();
                        if (result.Success && result.DataSource.Table != null)
                        {
                            bool AcExists = (result.DataSource.Table.Rows.Count > 0);
                            if (!AcExists)
                            {
                                result = SaveAccountingPeriodDetails();
                                if (!result.Success)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception err)
            {
                result.Message = "Could not generate Transactions (Financial Years) periods " + err.Message;
            }

            return result;
        }


        /// <summary>
        /// On 31/05/2021 to move previous year FY
        /// </summary>
        /// <returns></returns>
        public ResultArgs MovePreviousFYPeriod()
        {
            string opbalancedate = base.BookBeginFrom;
            ResultArgs result = new ResultArgs();
            try
            {
                result = FetchAllDetails();

                if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtNewFYs = result.DataSource.Table;
                    DateTime newFY = FirstFYDateFrom.AddMonths(-12);
                    int YearCount = LastFYDateFrom.Year - newFY.Year;
                    DateTime lastFYDateFromAddYears = LastFYDateFrom;
                    for (int i = 0; i <= YearCount; i++)
                    {
                        AccPeriodId = 0;
                        YearFrom = newFY.AddYears(i).ToShortDateString();
                        YearTo = DateSet.ToDate(YearFrom, false).AddMonths(12).AddDays(-1).ToShortDateString();

                        if (dtNewFYs.Rows.Count > i)
                        {
                            dtNewFYs.Rows[i][AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName] = YearFrom;
                            dtNewFYs.Rows[i][AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName] = YearTo;
                            dtNewFYs.AcceptChanges();
                        }
                        else
                        {
                            DataRow dr = dtNewFYs.NewRow();
                            dr[AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName] = 0;
                            dr[AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName] = YearFrom;
                            dr[AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName] = YearTo;
                            dr[AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn.ColumnName] = DBNull.Value;
                            dr[AppSchema.AccountingPeriod.STATUSColumn.ColumnName] = 0;
                            dr[AppSchema.AccountingPeriod.IS_FIRST_ACCOUNTING_YEARColumn.ColumnName] = 0;
                            dtNewFYs.Rows.Add(dr);
                        }
                    }

                    //For rearrange FYs
                    using (DataManager dmanager = new DataManager())
                    {
                        dmanager.BeginTransaction();
                        foreach (DataRow dr in dtNewFYs.Rows)
                        {
                            AccPeriodId = NumberSet.ToInteger(dr[AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName].ToString());
                            YearFrom = DateSet.ToDate(dr[AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString(), false).ToShortDateString();
                            YearTo = DateSet.ToDate(dr[AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString(), false).ToShortDateString();
                            Status = NumberSet.ToInteger(dr[AppSchema.AccountingPeriod.STATUSColumn.ColumnName].ToString());
                            BooksBeginingDate = base.BookBeginFrom;
                            if (!String.IsNullOrEmpty(dr[AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn.ColumnName].ToString()))
                            {
                                DateTime BooksBegin = DateSet.ToDate(dr[AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn.ColumnName].ToString(), false);
                                if (DateSet.ToDate(YearFrom, false) < BooksBegin)
                                {
                                    BooksBeginingDate = YearFrom;
                                    opbalancedate = DateSet.ToDate(YearFrom, false).AddDays(-1).ToShortDateString();
                                }
                            }
                            IsFirstAccYear = (NumberSet.ToInteger(dr[AppSchema.AccountingPeriod.IS_FIRST_ACCOUNTING_YEARColumn.ColumnName].ToString()) == 1);

                            result = SaveAccountingPeriodDetails(dmanager, false);
                            if (!result.Success)
                            {
                                break;
                            }
                        }

                        if (result.Success && !string.IsNullOrEmpty(opbalancedate))
                        {
                            //Re-arrange opening balances
                            using (ImportVoucherSystem import = new ImportVoucherSystem())
                            {
                                result = import.UpdateLedgerOpBalanceDate(opbalancedate, dmanager);
                            }
                        }
                        dmanager.EndTransaction();

                        if (result.Success)
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                balanceSystem.ProjectId = 0;
                                balanceSystem.VoucherDate = opbalancedate;
                                result = balanceSystem.UpdateBulkTransBalance();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                result.Message = "Could not MovePreviousFYPeriod (Financial Years) period " + err.Message;
            }
            return result;
        }
        #endregion
    }
}
