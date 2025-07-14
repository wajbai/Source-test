/*  Class Name      : CommonMemberData.cs
 *  Purpose         : Global member data
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;

namespace Bosco.Utility
{
    public abstract class Delimiter
    {
        public const string PipeLine = "|";
        public const string AtCap = "@";
        public const string Comma = ",";
        public const string Mew = "µ";
        public const string ECap = "ê";
    }

    public abstract class Currency
    {
        public static string Name = "INR";
        public static string Symbol = "Rs.";
        public static string Format = "en-GB";
    }

    public abstract class DefaultItem
    {
        public const string Empty = "";
        public const string All = "All";
        public const string AllWithLine = "-------All-------";
        public const string AllWithLineArrow = "<-------All------->";
    }

    public abstract class NumberFormatInfo
    {
        public const string Currency = "C";
        public const string General = "G";
        public const string Number = "D";
        public const string Amount = "#########.00";
    }

    public abstract class DateFormatInfo
    {
        private static string dateCulture = "en-GB";
        private static string dateFormat = "dd/MM/yyyy";

        public static string DateCulture
        {
            set { dateCulture = value; }
            get { return dateCulture; }
        }

        public static string DateFormat
        {
            set { dateFormat = value; }
            get { return dateFormat; }
        }

        public static string DateAndTimeFormat
        {
            get { return DateFormat + " " + TimeFormat; }
        }

        public static string DateAndTimeFormatLong
        {
            get { return DateFormat + " " + TimeFormatLong; }
        }

        public static string DateAndTimeFormatShort
        {
            get { return DateFormat + " " + TimeFormat; }
        }

        public static string DateAndTime12Format
        {
            get { return DateFormat + " " + Time12Format; }
        }

        public static string DateFormatMYD
        {
            get { return "MM/yyyy/dd"; }
        }

        public static string DateFormatYMD
        {
            get { return "yyyy/MM/dd"; }
        }

        public static string TimeFormat
        {
            get { return "HH:mm:ss"; }
        }

        public static string TimeFormatLong
        {
            get { return "HH:mm:ss:fff"; }
        }

        public static string Time12Format
        {
            get { return "hh:mm:ss tt"; }
        }

        public static string Time12FormatHM
        {
            get { return "hh:mm tt"; }
        }

        public static string TimeFormatHour
        {
            get { return "HH"; }
        }

        public static string Time12FormatHour
        {
            get { return "hh"; }
        }

        public static string TimeFormatMinute
        {
            get { return "mm"; }
        }

        public static string TimeFormatSecond
        {
            get { return "ss"; }
        }

        public static string TimeFormatAMPM
        {
            get { return "tt"; }
        }

        public static string StartTime
        {
            get { return " 00:00:00"; }
        }

        public static string EndTime
        {
            get { return " 23:59:59"; }
        }

        public static string Start12Time
        {
            get { return " 00:00:00"; }
        }

        public static string End12Time
        {
            get { return " 11:59:59 PM"; }
        }

        public class MySQLFormat
        {
            //date format for SQL insert/update
            public const string DateAdd = "yyyy/MM/dd";
            public const string DateTimeAdd = "yyyy/MM/dd HH:mm:ss";
            public const string DateUpdate = "yyyy-MM-dd";
            public const string TimeStampUpdate = "0000-00-00 HH:mm:ss";
            public const string DateAndTimeUpdate = "yyyy-MM-dd HH:mm:ss";
            public const string DateAndTimeNoformatBegin = "yyyyMMdd" + "000000";
            public const string DateAndTimeNoformatEnd = "yyyyMMdd" + "235959";

            //date format for SQL select statement
            public static string DateFormat = "yyyy/MM/dd";
            public static string DateFormat2 = "yyyy-MM-dd";

            public static string DateAndTime
            {
                get { return DateFormat2 + " HH:mm:ss"; }
            }
        }
    }

    public class PagePath
    {
        public static string ApplicationPhysicalPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        private static string errorPage = "";
        public static string ErrorPagePath
        {
            get { return errorPage; }
            set { errorPage = value; }
        }
    }
}
