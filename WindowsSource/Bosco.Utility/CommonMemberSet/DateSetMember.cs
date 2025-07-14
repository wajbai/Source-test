/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bosco.Utility.CommonMemberSet
{
    #region Date Set

    public class DateSetMember
    {
        public bool IsDate(string date)
        {
            DateTime dt;
            bool isDate = DateTime.TryParse(date, out dt);
            return isDate;
        }

        public DateTime ToDate(string date, bool isTime)
        {
            DateTime dateVal = DateTime.Now.Date;

            if (IsDate(date))
            {
                if (isTime)
                {
                    dateVal = DateTime.Parse(date);
                }
                else
                {
                    dateVal = DateTime.Parse(date).Date;
                }
            }
            return dateVal;
        }

        public string ToDate(string date)
        {
            string dateVal = "";

            if (IsDate(date))
            {
                dateVal = DateTime.Parse(date).ToShortDateString();
            }
            return dateVal;
        }

        public string ToDate(string date, string format)
        {
            string dateVal = "";

            if (IsDate(date))
            {
                dateVal = DateTime.Parse(date).ToString(format);
            }
            return dateVal;
        }

        public string ToTime(string date, string format)
        {
            string timeVal = "";

            if (IsDate(date))
            {
                timeVal = DateTime.Parse(date).ToString(format);
            }
            return timeVal;
        }

        public bool HasTime(string dateTime)
        {
            bool hasTime = false;

            if (IsDate(dateTime))
            {
                hasTime = (DateTime.Parse(dateTime).Hour > 0);
            }

            return hasTime;
        }

        public string ToDateTime(string date, string format, bool isTimeEnd)
        {
            string dateVal = "";

            if (IsDate(date))
            {
                dateVal = DateTime.Parse(date).ToString(format);
            }

            if (isTimeEnd)
            {
                dateVal += " 23:59:59";
            }
            else
            {
                dateVal += " 00:00:00";
            }

            return dateVal;
        }

        public string GetDateToday()
        {
            return GetDateToday(false);
        }

        public string GetDateToday(bool isDateTime)
        {
            string date = "";

            try
            {
                if (isDateTime)
                {
                    date = DateTime.Now.ToString();
                }
                else
                {
                    date = DateTime.Now.ToShortDateString();
                }
            }
            catch (Exception) { }

            return date;
        }

        public string ToCurrentTime()
        {
            string sTime = "";

            try
            {
                sTime = DateTime.Now.ToString("hh:mm tt");
            }
            catch (Exception) { }
            return sTime;
        }

        public string ToCurrentTime(string format)
        {
            string sTime = "";

            try
            {
                sTime = DateTime.Now.ToString(format);
            }
            catch (Exception) { }
            return sTime;
        }

        public string ToCurrentDateTime(string format)
        {
            string dateTime = "";

            try
            {
                dateTime = DateTime.Now.ToString(format);
            }
            catch (Exception) { }
            return dateTime;
        }

        public string ToTimePart12Hr(string dateTime)
        {
            string time = "";
            int hour = 0;
            int minute = 0;
            string timeMode = TimeMode.AM.ToString();
            time = ToTimePart12Hr(dateTime, out hour, out minute, out timeMode);

            return time;
        }

        public string ToTimePart12Hr(string dateTime, out int hour, out int minute, out string timeMode)
        {
            string time = "";
            hour = 0;
            minute = 0;
            timeMode = TimeMode.AM.ToString();

            if (IsDate(dateTime))
            {
                DateTime date = ToDate(dateTime, true);
                int.TryParse(date.ToString(DateFormatInfo.Time12FormatHour), out hour);
                int.TryParse(date.ToString(DateFormatInfo.TimeFormatMinute), out minute);
                timeMode = date.ToString(DateFormatInfo.TimeFormatAMPM);
                time = date.ToString(DateFormatInfo.Time12Format);
            }

            return time;
        }

        public string GetDOB(int ageYear, int ageMonth)
        {
            DateTime dateNow = DateTime.Parse(GetDateToday());
            string dob = dateNow.AddYears(-ageYear).AddMonths(-ageMonth).ToShortDateString();
            return dob;
        }

        public string GetAge(string DOB, out int year, out int month)
        {
            int day = 0;
            int ageMonth = 0;
            int ageDay = 0;

            int toMonth = 0;
            int toYear = 0;
            int no_month = 12;
            string age = "";

            year = 0;
            month = 0;

            DateTime dateFrom = DateTime.Parse(GetDateToday());
            if (IsDate(DOB)) { dateFrom = DateTime.Parse(DOB); };
            DateTime dateTo = DateTime.Parse(GetDateToday());

            toMonth = dateTo.Month;
            toYear = dateTo.Year;
            day = dateTo.Day - dateFrom.Day;

            if (day < 0)
            {
                toMonth -= 1;
                if (toMonth == 0)
                {
                    toMonth = no_month;
                    toYear -= 1;
                }

                day = DateTime.DaysInMonth(toYear, toMonth) + dateTo.Day - dateFrom.Day;
            }

            ageDay = day;
            month = toMonth - dateFrom.Month;

            if (month < 0)
            {
                toYear -= 1;
                month += no_month;
            }

            ageMonth = month;
            year = (toYear - dateFrom.Year);

            if (month == 0 && year == 0)
            {
                month = 1;
                day = 0;
            }

            if (year > 0)
            {
                age += year + " Y";
            }

            if (ageMonth > 0)
            {
                age += ((age != "") ? " - " : "") + ageMonth + " M";
            }

            if (ageDay > 0 && year == 0)
            {
                age += ((age != "") ? " " : "") + ageDay + " D";
            }

            return age;
        }

        public bool ValidateDate(DateTime DateFrom, DateTime DateTo)
        {
            bool successs = true;
            if (DateTo < DateFrom)
            {
                successs = false;
            }
            return successs;
        }

        public string GetMySQLDateTime(string dateTime, DateDataType dateType)
        {
            string date = dateTime;
            string formatDateUpdate = "yyyy-MM-dd";
            string formatDateAndTimeUpdate = "yyyy-MM-dd HH:mm:ss";


            if (String.IsNullOrEmpty(date)) { date = ""; }
            date = date.Trim();

            if (date != "")
            {
                DateTime dt;

                try
                {
                    if (DateTime.TryParse(date, out dt))
                    {
                        dt = DateTime.Parse(date);

                        switch (dateType)
                        {
                            case DateDataType.Date:
                                {
                                    date = dt.ToString(formatDateUpdate);
                                    break;
                                }
                            case DateDataType.DateTime:
                                {
                                    date = dt.ToString(formatDateAndTimeUpdate);
                                    break;
                                }
                            case DateDataType.DateNoFormatBegin:
                                {
                                    date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeNoformatBegin);
                                    break;
                                }
                            case DateDataType.DateNoFormatEnd:
                                {
                                    date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeNoformatEnd);
                                    break;
                                }
                        }
                    }
                }
                catch (Exception) { }
            }

            return date;
        }


        public static class ConvertDateToWords
        {
            static readonly string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            static readonly string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            static readonly string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            static readonly string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };

            private static string FriendlyInteger(int n, string leftDigits, int thousands)
            {
                if (n == 0)
                    return leftDigits;

                string friendlyInt = leftDigits;
                if (friendlyInt.Length > 0)
                    friendlyInt += " ";

                if (n < 10)
                    friendlyInt += ones[n];
                else if (n < 20)
                    friendlyInt += teens[n - 10];
                else if (n < 100)
                    friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
                else if (n < 1000)
                    friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Hundred"), 0);
                else
                    friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);

                return friendlyInt + thousandsGroups[thousands];
            }

            public static string DateToWritten(DateTime date)
            {
                return string.Format("{0} - {1} - {2}", IntegerToWritten(date.Day), date.ToString("MMMM"), IntegerToWritten(date.Year));
            }

            public static string IntegerToWritten(int n)
            {
                if (n == 0)
                    return "Zero";
                else if (n < 0)
                    return "Negative " + IntegerToWritten(-n);

                return FriendlyInteger(n, "", 0);
            }
        }

        /// <summary>
        /// Extension method to get the start of the financial year based on setting (April-March, Jan-Dec)
        /// </summary>    
        public DateTime GetFinancialYearByDate(DateTime date, string FYYearFrom)
        {
            int startMonthOfFinancialYear = 4; //by default Aprip-March
            
            //As on 05/05/2021, to get based on FY April-March - Jan to Dec
            if (!string.IsNullOrEmpty(FYYearFrom))
            {
                startMonthOfFinancialYear = ToDate(FYYearFrom, false).Month;
            }

            DateTime rtn = new DateTime(date.Year, startMonthOfFinancialYear, 1);
            if (date.Month < startMonthOfFinancialYear)
            {
                // Current FY starts last year - e.g. given April to March FY then 1st Feb 2013 FY starts 1st April 20*12*
                rtn = rtn.AddYears(-1);
            }

            return ToDate(rtn.ToShortDateString(), false);
        }

        /// <summary>
        /// On 05/05/2021
        /// </summary>
        /// <returns></returns>
        public bool IsAprilMarchFYBranch(string yearfrom)
        {
            bool Rtn = true;

            if (Rtn)
            {
                if (ToDate(yearfrom, false).Month == 4)
                {
                    Rtn = true;
                }
                else if (ToDate(yearfrom, false).Month == 1) // (Jan-Feb)
                {
                    Rtn = false;
                }
            }

            return Rtn;
        }
        /// <summary>
        /// Extension method to get the quater month of the start of the financial year based on setting (April-March, Jan-Dec)
        /// </summary>    
        public string GetQuaterMonthsFinancialYearByQuaterNo(Int32 QuaterNo, string YearFrom, string YearTo)
        {
            string Rtn = "Q" + QuaterNo.ToString();

            if (ToDate(YearFrom, false).Month == 1) //For Jan-Dec
            {
                switch (QuaterNo)
                {
                    case 1:
                        Rtn = "Jan" + ToDate(YearFrom, false).ToString("yy") + "-" + "Mar" + ToDate(YearFrom, false).ToString("yy");
                        break;
                    case 2:
                        Rtn = "Apr" + ToDate(YearFrom, false).ToString("yy") + "-" + "Jun" + ToDate(YearFrom, false).ToString("yy");
                        break;
                    case 3:
                        Rtn = "Jul" + ToDate(YearFrom, false).ToString("yy") + "-" + "Sep" + ToDate(YearFrom, false).ToString("yy");
                        break;
                    case 4:
                        Rtn = "Oct" + ToDate(YearTo, false).ToString("yy") + "-" + "Dec" + ToDate(YearTo, false).ToString("yy");
                        break;
                }
            }
            else //For April-March
            {
                switch (QuaterNo)
                {
                    case 1:
                        Rtn = "Apr'" + ToDate(YearFrom, false).ToString("yy") + " - " + "Jun'" + ToDate(YearFrom, false).ToString("yy");
                        break;
                    case 2:
                        Rtn = "Jul'" + ToDate(YearFrom, false).ToString("yy") + " - " + "Sep'" + ToDate(YearFrom, false).ToString("yy");
                        break;
                    case 3:
                        Rtn = "Oct'" + ToDate(YearFrom, false).ToString("yy") + " - " + "Dec'" + ToDate(YearFrom, false).ToString("yy");
                        break;
                    case 4:
                        Rtn = "Jan'" + ToDate(YearTo, false).ToString("yy") + " - " + "Mar'" + ToDate(YearTo, false).ToString("yy");
                        break;
                }
            }
            return Rtn;
        }

        public List<Tuple<DateTime, DateTime>> GetQuarterDates(DateTime startDate, DateTime endDate)
        {
            List<Tuple<DateTime, DateTime>> quarterDates = new List<Tuple<DateTime, DateTime>>();

            try
            {
                quarterDates = Enumerable.Range(0, (endDate - startDate).Days)
                    .Where(x => startDate.AddMonths(x).Date == new DateTime(startDate.AddMonths(x).Year, (((startDate.AddMonths(x).Month - 1) / 3 + 1) - 1) * 3 + 1, 1))
                    .Select(x => new Tuple<DateTime, DateTime>(startDate.AddMonths(x), startDate.AddMonths(x + 3).AddDays(-1)))
                    .Where(x => x.Item2 <= endDate).ToList();
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Finance Year is invalid (" + startDate.ToShortDateString()  + "-" + endDate.ToShortDateString() + ", " + err.Message);
            }
            return quarterDates;
        }

        public int GetQuarter(DateTime date, string YearFrom)
        {
            int QuterNo = 1;
            DateTime YrFrom = ToDate(YearFrom, false);
            if (YrFrom.Month == 4) //For FY April-March
            {
                if (date.Month >= 4 && date.Month <= 6)
                    QuterNo= 1;
                else if (date.Month >= 7 && date.Month <= 9)
                    QuterNo= 2;
                else if (date.Month >= 10 && date.Month <= 12)
                    QuterNo= 3;
                else
                    QuterNo= 4;
            }
            else if (YrFrom.Month == 1) //On 16/04/2020, For FY Jan-Dec
            {
                if (date.Month >= 1 && date.Month <= 3)
                    QuterNo = 1;
                else if (date.Month >= 4 && date.Month <= 6)
                    QuterNo = 2;
                else if (date.Month >= 7 && date.Month <= 9)
                    QuterNo = 3;
                else
                    QuterNo = 4;
            }
            return QuterNo;
        }

        public DateTime FirstDayOfQuater(DateTime date, string YearFrom, string YearTo)
        {
            int CurrentQuater = GetQuarter(date, YearFrom);
            List<Tuple<DateTime, DateTime>> quarterDates = GetQuarterDates(ToDate(YearFrom, false), ToDate(YearTo, false));

            DateTime dtFirstDay = quarterDates[CurrentQuater-1].Item1;
            return dtFirstDay;
        }

        public DateTime LastDayOfQuater(DateTime date, string YearFrom, string YearTo)
        {
            DateTime dtLastDay = ToDate(YearTo, false);
            try
            {
                int CurrentQuater = GetQuarter(date, YearFrom);
                List<Tuple<DateTime, DateTime>> quarterDates = GetQuarterDates(ToDate(YearFrom, false), ToDate(YearTo, false));

                dtLastDay = quarterDates[CurrentQuater - 1].Item2;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Finance Year is invalid (" + ToDate(YearFrom, false).ToShortDateString() + "-" + ToDate(YearTo, false).ToShortDateString() + ")");
                AcMELog.WriteLog(err.Message);
            }
            return dtLastDay;
        }

        public int GetDateDifferentInMonths(DateTime dateFrom, DateTime dateTo)
        {
            int NoOfMonths = 1;
            try
            {
                NoOfMonths = Math.Abs(12 * (dateTo.Year - dateFrom.Year)
                                     + dateTo.Month - dateFrom.Month);
                NoOfMonths++; 
            }
            catch(Exception e)
            {
                MessageRender.ShowMessage(e);
                NoOfMonths = 1;
            }
            return NoOfMonths;
        }
    }

    #endregion
}
