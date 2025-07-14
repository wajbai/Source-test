/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace Payroll.Utility.CommonMemberSet
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
    }

    #endregion
}
