using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using Payroll.Utility;
using Payroll.Utility.CommonMemberSet;
using System.Globalization;
using Payroll.Utility.ConfigSetting;
using System.Threading;



namespace Payroll.Utility
{
    public class CommonMethod:IDisposable
    {
        #region Decelaration
        VoucherType enumVoucherType = new VoucherType();
        #endregion
        #region Methods
        /// <summary>
        /// This is to get the list of Currency symbol for the all the country
        /// </summary>
        /// <returns></returns>
        //public DataTable GetCurrencySymbolList()
        //{
        //    DataTable dtCurrencySymbol = new DataTable();
        //    dtCurrencySymbol.Columns.Add(new DataColumn("Currency Symbol", typeof(string)));
        //   // dtCurrencySymbol.Columns.Add(new DataColumn("Name", typeof(string)));
        //    dtCurrencySymbol.Columns.Add(new DataColumn("Currency Code", typeof(string)));
        //    dtCurrencySymbol.Columns.Add(new DataColumn("Country Code", typeof(string)));
        //    dtCurrencySymbol.Columns.Add(new DataColumn("Country", typeof(string)));
        //    dtCurrencySymbol.Columns.Add(new DataColumn("Currency Name", typeof(string)));
        //    try
        //    {
        //        CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
        //        foreach (CultureInfo culture in cultures)
        //        {
        //            var regionInfo = new RegionInfo(culture.Name);
        //            dtCurrencySymbol.Rows.Add(regionInfo.CurrencySymbol, regionInfo.ISOCurrencySymbol,regionInfo.ThreeLetterISORegionName,regionInfo.DisplayName,regionInfo.CurrencyEnglishName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    return dtCurrencySymbol;
        //}

        /// <summary>
        /// This is to get the list of voucher types.
        /// </summary>
        /// <param name="cboVoucherType"></param>
        //public void GetVoucherType(DevExpress.XtraEditors.ComboBoxEdit cboVoucherType)
        //{
        //    EnumSetMember enumSetMember = new EnumSetMember();
        //    DataView dvVoucherType = enumSetMember.GetEnumDataSource(enumVoucherType, Sorting.None);
        //    if (dvVoucherType != null && dvVoucherType.Count != 0)
        //    {
        //        for (int i = 0; i < dvVoucherType.Count; i++)
        //        {
        //            cboVoucherType.Properties.Items.Add(dvVoucherType.Table.Rows[i]["Name"].ToString());
        //        }
        //    }
        //}

        public void SetAppSetting()
        {
            try
            {
                SettingProperty setting = new SettingProperty();
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setting.Language);

                System.Globalization.NumberFormatInfo nformat = new CultureInfo(setting.Language, true).NumberFormat;
                //Digit Grouping and Digit Separator
              //  nformat.CurrencyGroupSizes = setting.DigitGrouping.Split(',').Select(n => Convert.ToInt32(n)).ToArray(); 
                nformat.CurrencyGroupSeparator = setting.GroupingSeparator;
                nformat.CurrencyPositivePattern = Convert.ToInt32(setting.CurrencyPosition);
                nformat.CurrencyNegativePattern = Convert.ToInt32(setting.NegativeSign);

                //Currency Symbol,Code,Position
                nformat.CurrencySymbol = setting.Currency;

                //Number of digits after decimal and decimal separator
                nformat.CurrencyDecimalDigits =Convert.ToInt32(setting.DecimalPlaces);
                nformat.CurrencyDecimalSeparator = setting.DecimalSeparator;

                Thread.CurrentThread.CurrentCulture.NumberFormat = nformat;

                DateTimeFormatInfo date = new DateTimeFormatInfo();
                date.ShortDatePattern = setting.DateFormat;
                date.DateSeparator = setting.DateSeparator;

                Thread.CurrentThread.CurrentCulture.DateTimeFormat = date;

                //UserLookAndFeel.Default.SetSkinStyle(setting.Themes);
                
                //Date format and date separator

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        public void SetUIAppSetting()
        {
            try
            {
                UISettingProperty setting = new UISettingProperty();
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(setting.UILanguage);

                //Date format and date separator
                DateTimeFormatInfo date = new DateTimeFormatInfo();
                date.ShortDatePattern = setting.UIDateFormat;
                date.DateSeparator = setting.UIDateSeparator;

                Thread.CurrentThread.CurrentCulture.DateTimeFormat = date;

                //UserLookAndFeel.Default.SetSkinStyle(setting.UIThemes);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }
        #endregion
       
        #region IDisposable Members

        public virtual void Dispose()
        {
            GC.Collect();
        }
        #endregion

        void IDisposable.Dispose()
        {
            GC.Collect();
        }
    }
}
