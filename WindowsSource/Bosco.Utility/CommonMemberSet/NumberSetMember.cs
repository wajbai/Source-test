/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace Bosco.Utility.CommonMemberSet
{
    #region Number Set

    public class NumberSetMember
    {
        public int ToInteger(string value)
        {
            int val = 0;
            int.TryParse(value, out val);
            return val;
        }

        public double ToDouble(string value)
        {
            double val = 0;
            double.TryParse(value, out val);
            return val;
        }

        public decimal ToDecimal(string value)
        {
            decimal val = 0;
            decimal.TryParse(value, out val);
            return val;
        }

        public string ToFormattedNumber(int value, int noOfDigit, string prefix, string suffix)
        {
            string val = prefix + value.ToString().PadLeft(noOfDigit, '0') + suffix;
            return val;
        }
        public string ToCurrency(double value)
        {
            string val = value.ToString("C");
            return val;
        }
        public string ToNumber(double value)
        {
            string val = value.ToString("n");
            return val;
        }

        /// <summary>
        /// On 10/05/2021
        /// This method is used to truncate double values without rounding by given precision
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public double TruncateDoubleByPrecision(double value, int precision)
        {
            double rtn = value;
            try
            {
                double step = (double)Math.Pow(10, precision);
                double tmp = Math.Truncate(step * value);
                rtn = tmp / step;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return rtn;
        }
    }

    #endregion
}
