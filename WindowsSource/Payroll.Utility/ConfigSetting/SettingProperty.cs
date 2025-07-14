using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using Payroll.Utility;

namespace Payroll.Utility.ConfigSetting
{
    public class SettingProperty : UISettingProperty
    {
        private static DataView dvSetting = null;
        private const string SettingNameField = "Name";
        private const string SettingValueField = "Value";
        // Payroll-App Common Members
        public static string PayrollMonth = string.Empty;
        public static DataTable dtData = new DataTable();
        public static DataTable dtOpen = new DataTable();

        private string GetSettingInfo(string name)
        {
            string val = "";
            try
            {
                if (dvSetting != null && dvSetting.Count > 0)
                {
                    for (int i = 0; i < dvSetting.Count; i++)
                    {
                        string record = dvSetting[i][SettingNameField].ToString();
                        if (name == record)
                        {
                            val = dvSetting[i][SettingValueField].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return val;
        }

        /// <summary>
        /// Set Setting Info as Dataview
        /// </summary>
        public DataView SettingInfo
        {
            set
            {
                SettingProperty.dvSetting = value;
            }
        }

        public string Language
        {
            get
            {
                return GetSettingInfo(Setting.UILanguage.ToString());
            }
        }

        public string DateFormat
        {
            get
            {
                return GetSettingInfo(Setting.UIDateFormat.ToString());
            }
        }

        public string DateSeparator
        {
            get
            {
                return GetSettingInfo(Setting.UIDateSeparator.ToString());
            }
        }

        public string Themes
        {
            get
            {
                return GetSettingInfo(Setting.UIThemes.ToString());
            }
        }

        public string Country
        {
            get
            {
                return GetSettingInfo(Setting.Country.ToString());
            }
        }

        public string Currency
        {
            get
            {
                return GetSettingInfo(Setting.Currency.ToString());
            }
        }

        public string CurrencyCode
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyCode.ToString());
            }
        }

        public string CurrencyPosition
        {
            get
            {
                return GetSettingInfo(Setting.CurrencyPosition.ToString());
            }
        }

        public string CodePosition
        {
            get
            {
                return GetSettingInfo(Setting.CodePosition.ToString());
            }
        }

        public string DigitGrouping
        {
            get
            {
                return GetSettingInfo(Setting.DigitGrouping.ToString());
            }
        }

        public string GroupingSeparator
        {
            get
            {
                return GetSettingInfo(Setting.GroupingSeparator.ToString());
            }
        }

        public string DecimalPlaces
        {
            get
            {
                return GetSettingInfo(Setting.DecimalPlaces.ToString());
            }
        }

        public string DecimalSeparator
        {
            get
            {
                return GetSettingInfo(Setting.DecimalSeparator.ToString());
            }
        }

        public string NegativeSign
        {
            get
            {
                return GetSettingInfo(Setting.NegativeSign.ToString());
            }
        }

        public string HighNaturedAmt
        {
            get
            {
                return GetSettingInfo(Setting.HighNaturedAmt.ToString());
            }
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            GC.Collect();
        }

        #endregion
    }
}
