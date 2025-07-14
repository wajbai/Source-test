using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using Payroll.Utility;

namespace Payroll.Utility.ConfigSetting
{
    public class UISettingProperty : CommonMember,IDisposable
    {
        private static DataView dvUISetting = null;
        private const string SettingNameField = "Name";
        private const string SettingValueField = "Value";

        private string GetUISettingInfo(string name)
        {
            string val = "";
            try
            {
                if (dvUISetting != null && dvUISetting.Count > 0)
                {
                    for (int i = 0; i < dvUISetting.Count; i++)
                    {
                        string record = dvUISetting[i][SettingNameField].ToString();
                        if (name == record)
                        {
                            val = dvUISetting[i][SettingValueField].ToString();
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

        public DataView UISettingInfo
        {
            set
            {
                UISettingProperty.dvUISetting = value;
            }
        }

        public string UILanguage
        {
            get
            {
                return GetUISettingInfo(UISetting.UILanguage.ToString());
            }
        }

        public string UIDateFormat
        {
            get
            {
                return GetUISettingInfo(UISetting.UIDateFormat.ToString());
            }
        }

        public string UIDateSeparator
        {
            get
            {
                return GetUISettingInfo(UISetting.UIDateSeparator.ToString());
            }
        }

        public string UIThemes
        {
            get
            {
                return GetUISettingInfo(UISetting.UIThemes.ToString());
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
