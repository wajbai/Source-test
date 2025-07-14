using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility.ConfigSetting;
using System.Threading;
using System.Globalization;
using Bosco.Utility;
using System.Data;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.Setting
{
    public class GlobalSetting : SystemBase, ISetting
    {
        ResultArgs resultArgs = null;
        public void ApplySetting()
        {
            try
            {
                SettingProperty setting = new SettingProperty();
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setting.Language);

                System.Globalization.NumberFormatInfo nformat = new CultureInfo(setting.Language, true).NumberFormat;
               
                //Currency Symbol,Code,Position
                nformat.CurrencySymbol = setting.Currency;
                nformat.CurrencyPositivePattern = this.NumberSet.ToInteger(setting.CurrencyPositivePattern);
                nformat.CurrencyNegativePattern = this.NumberSet.ToInteger(setting.CurrencyNegativePattern);
               
                //for Currency  x
                if (setting.DigitGrouping != "0,0,0")
                {
                      nformat.CurrencyGroupSizes = setting.DigitGrouping.Split(',').Select(n => this.NumberSet.ToInteger(n)).ToArray(); 
                }
                nformat.CurrencyGroupSeparator = setting.GroupingSeparator;
                nformat.CurrencyDecimalDigits = this.NumberSet.ToInteger(setting.DecimalPlaces);
                nformat.CurrencyDecimalSeparator = setting.DecimalSeparator;
                
                //for Numbers
                if (setting.DigitGrouping != "0,0,0")
                {
                    nformat.NumberGroupSizes = setting.DigitGrouping.Split(',').Select(n => this.NumberSet.ToInteger(n)).ToArray();
                }
                nformat.NumberGroupSeparator = setting.GroupingSeparator;
                nformat.NumberDecimalDigits = this.NumberSet.ToInteger(setting.DecimalPlaces);
                nformat.NumberDecimalSeparator = setting.DecimalSeparator;

                //Thread.CurrentThread.CurrentCulture.fdf
                Thread.CurrentThread.CurrentCulture.NumberFormat = nformat;
                //Thread.CurrentThread.CurrentCulture.NumberFormat = nformat;

                DateTimeFormatInfo date = new DateTimeFormatInfo();
                date.ShortDatePattern = setting.DateFormat;
                date.DateSeparator = setting.DateSeparator;

                Thread.CurrentThread.CurrentCulture.DateTimeFormat = date;

                new CommonMethod().ApplyTheme(setting.Themes);

                //Date format and date separator
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        public ResultArgs SaveSetting(DataTable dtSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdate))
            {
                dataManager.BeginTransaction();

                string SettingName = "";
                string Value = "";

                DataTable dtSet = dtSetting;

                if (dtSet != null)
                {
                    foreach (DataRow drSetting in dtSet.Rows)
                    {
                        SettingName = drSetting[this.AppSchema.Setting.NameColumn.ColumnName].ToString();
                        Value = drSetting[this.AppSchema.Setting.ValueColumn.ColumnName].ToString();

                        dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, SettingName);
                        dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, Value);

                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success) { break; }
                        else
                            dataManager.Parameters.Clear();
                    }
                    
                    // Module Level Settings

                    if (resultArgs.Success)
                    {
                        resultArgs = FetchSettingDetails(1);

                        if (resultArgs.Success && resultArgs.DataSource.TableView != null && resultArgs.DataSource.TableView.Count > 0)
                        {
                            this.SettingInfo = resultArgs.DataSource.TableView;
                        }
                    }
                }
                dataManager.EndTransaction();


                //20/06/2022, To update LC reference details in based db for enable request rightrs
                if (IS_SDB_INM)
                {
                    using (UISettingSystem uisystemsetting = new UISettingSystem())
                    {
                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef1, BaseLCRef1, LoginUserId);
                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef2, BaseLCRef2, LoginUserId);
                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef3, BaseLCRef3, LoginUserId);
                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef4, BaseLCRef4, LoginUserId);
                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef5, BaseLCRef5, LoginUserId);
                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef6, BaseLCRef6, LoginUserId);
                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.LCRef7, BaseLCRef7, LoginUserId);

                        uisystemsetting.BaseAcmeerpSaveUISettingDetails(FinanceSetting.BranchReceiptModuleStatus, ((Int32)FINAL_RECEIPT_MODULE_STATUS).ToString(), LoginUserId);

                    }
                }

            }
            return resultArgs;
        }

        public ResultArgs SaveSetting(Utility.Setting globalsetting, string value, Int32 userid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdate))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, userid);
                dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, globalsetting.ToString());
                dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, value);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchSettingDetails(int UserID)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.UISetting.FetchUI))
            {
                dataManager.Parameters.Add(this.AppSchema.Settings.USER_IDColumn, UserID);

                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public ResultArgs UpdateAccountingPeriod(string AccPeriodId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.UpdateStatus))
            {
                dataManager.Database = dataManager.Database;
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
    }
}
