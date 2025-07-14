using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bosco.Utility.ConfigSetting;
using System.Threading;

namespace Bosco.Utility
{
    public static class ConvertRuppessInWord
    {
        // Function for conversion of a Indian Rupees into words
        //   Parameter - accept a Currency
        //   Returns the number in words format
        //====================================================
        private static object objLockRs = new Object();

        public static string VoucherCurrencySymbol = string.Empty;
        public static string VoucherCurrencyName = string.Empty;

        public static string GetRupeesToWord(string RupeesValue)
        {
            lock (objLockRs)
            {
                string rtn = RupeesValue;
                try
                {
                    bool isIndianrupee = true;
                    string currencysymbol = string.Empty;

                    //On 26/10/2024, To Set Currency Name ----------------------------------------------------------------
                    currencysymbol = SettingProperty.Current.Currency;
                    if (SettingProperty.Current.AllowMultiCurrency == 1)
                    {
                        currencysymbol = VoucherCurrencySymbol;
                    }
                    isIndianrupee = (string.IsNullOrEmpty(currencysymbol) || currencysymbol == "र"); //Fixed for Indian Currency
                    //----------------------------------------------------------------------------------------------------

                    if (isIndianrupee)
                    {
                        rtn = ConvertRupeesToWord(RupeesValue);
                    }
                    else
                    {
                        ConvertCurrencyInWord.VoucherCurrencySymbol = VoucherCurrencySymbol;
                        ConvertCurrencyInWord.VoucherCurrencyName = VoucherCurrencyName;
                        rtn = ConvertCurrencyInWord.ToWords(RupeesValue);
                    }
                }
                catch (Exception err)
                {
                    AcMELog.WriteLog("Error message Amount in Words - " +  err.Message);
                    rtn = ConvertRupeesToWord(RupeesValue);
                }

                return rtn;                
            }
        }

        private static string ConvertRupeesToWord(string RupeesValue)
        {
            string currencyname = SettingProperty.Current.CurrencyName;
            double AmtValue = 0;
            int DecimalPlace = 0;
            int FractionPart = 0;
            int iCount = 0;

            string AmtNumber = "";
            string TempWord = "";
            string Paisa = "";
            string Hundreds = "";
            string Words = "";
            string[] place = new string[11];

            place[0] = " Thousand ";
            place[2] = " Lakh ";
            place[4] = " Crore ";
            place[6] = " Arab ";
            place[8] = " Kharab ";
            place[10] = " ";

            //INSTANT C# TODO TASK: The 'On Error Resume Next' statement is not converted by Instant C#:
            //On Error Resume Next
            // Convert AmtNumber to a string, trimming extra spaces.

            double.TryParse((string.IsNullOrEmpty(RupeesValue) ? RupeesValue : RupeesValue.Trim()), out AmtValue);
            AmtNumber = AmtValue.ToString("#0.00");

            if (AmtValue == 0) return "Zero Only";

            // Find decimal place.
            DecimalPlace = AmtNumber.IndexOf(".");

            // If we find decimal place...
            if (DecimalPlace >= 0)
            {
                // Convert Paisa
                TempWord = AmtNumber.Substring(DecimalPlace + 1);
                int.TryParse(TempWord, out FractionPart);

                if (FractionPart > 0)
                {
                    Paisa = " and " + ConvertTens(TempWord) + " Paisa";
                }

                // Strip off paisa from remainder to convert.
                AmtNumber = AmtNumber.Substring(0, DecimalPlace);
            }

            //===============================================================
            string TM = ""; // If AmtNumber between Rs.1 To 99 Only.
            TM = AmtNumber;

            if (AmtNumber.Length >= 2)
            {
                TM = AmtNumber.Substring(AmtNumber.Length - 2);
            }

            if (AmtNumber.Length > 0 & AmtNumber.Length <= 2)
            {
                if (TM.Length == 1)
                {
                    Words = ConvertDigit(TM);
                    return " " + Words + Paisa + " Only";
                }
                else if (TM.Length == 2)
                {
                    Words = ConvertTens(TM);
                    return " " + Words + Paisa + " Only";
                }
            }
            //===============================================================


            // Convert last 3 digits of AmtNumber to ruppees in word.
            Hundreds = ConvertHundreds(AmtNumber.Substring(AmtNumber.Length - 3));

            // Strip off last three digits
            AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 3);

            iCount = 0;
            while (AmtNumber != "")
            {
                //Strip last two digits
                TempWord = AmtNumber;

                if (AmtNumber.Length >= 2)
                {
                    TempWord = AmtNumber.Substring(AmtNumber.Length - 2);
                }

                if (AmtNumber.Length == 1)
                {
                    if (Words.Trim() == "Thousand" || Words.Trim() == "Lakh  Thousand"
                        || Words.Trim() == "Lakh" || Words.Trim() == "Crore"
                        || Words.Trim() == "Crore  Lakh  Thousand"
                        || Words.Trim() == "Arab  Crore  Lakh  Thousand"
                        || Words.Trim() == "Arab" || Words.Trim() == "Kharab  Arab  Crore  Lakh  Thousand"
                        || Words.Trim() == "Kharab")
                    {
                        Words = ConvertDigit(TempWord) + place[iCount];
                        AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 1);
                    }
                    else
                    {
                        Words = ConvertDigit(TempWord) + place[iCount] + Words;
                        AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 1);
                    }
                }
                else
                {
                    if (Words.Trim() == "Thousand" || Words.Trim() == "Lakh  Thousand"
                        || Words.Trim() == "Lakh" || Words.Trim() == "Crore"
                        || Words.Trim() == "Crore  Lakh  Thousand"
                        || Words.Trim() == "Arab  Crore  Lakh  Thousand" || Words.Trim() == "Arab")
                    {
                        Words = ConvertTens(TempWord) + place[iCount];
                        AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 2);
                    }
                    else
                    {
                        //=================================================================
                        // if only Lakh, Crore, Arab, Kharab
                        string wrd = (ConvertTens(TempWord) + place[iCount]);

                        if (wrd.Trim() == "Lakh" || wrd.Trim() == "Crore" || wrd.Trim() == "Arab")
                        {
                            //Words = Words;
                            AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 2);
                        }
                        else
                        {
                            Words = wrd + Words;
                            AmtNumber = AmtNumber.Substring(0, AmtNumber.Length - 2);
                        }
                    }
                }

                iCount += 2;
            }

            return "" + Words + Hundreds + Paisa + " Only";
        }

        // Conversion for hundreds
        //*****************************************
        private static string ConvertHundreds(string AmtNumber)
        {
            string Result = "";
            double amt = 0;
            double.TryParse(AmtNumber, out amt);

            // Exit if there is nothing to convert.
            if (amt == 0)
            {
                return "";
            }

            // Append leading zeros to number.
            AmtNumber = ("000" + AmtNumber).Substring(("000" + AmtNumber).Length - 3);

            // Do we have a hundreds place digit to convert?
            if (AmtNumber.ToString().Substring(0, 1) != "0")
            {
                Result = ConvertDigit(AmtNumber.Substring(0, 1)) + " Hundred ";
            }

            // Do we have a tens place digit to convert?
            if (AmtNumber.Substring(1, 1) != "0")
            {
                Result = Result + ConvertTens(AmtNumber.Substring(1));
            }
            else
            {
                // If not, then convert the ones place digit.
                Result = Result + ConvertDigit(AmtNumber.Substring(2));
            }

            return Result.Trim();
        }

        // Conversion for tens
        //*****************************************
        private static string ConvertTens(string MyTens)
        {
            string Result = "";

            // Is value between 10 and 19?
            if (MyTens.Substring(0, 1) == "1")
            {
                switch (MyTens)
                {
                    case "10":
                        Result = "Ten";
                        break;
                    case "11":
                        Result = "Eleven";
                        break;
                    case "12":
                        Result = "Twelve";
                        break;
                    case "13":
                        Result = "Thirteen";
                        break;
                    case "14":
                        Result = "Fourteen";
                        break;
                    case "15":
                        Result = "Fifteen";
                        break;
                    case "16":
                        Result = "Sixteen";
                        break;
                    case "17":
                        Result = "Seventeen";
                        break;
                    case "18":
                        Result = "Eighteen";
                        break;
                    case "19":
                        Result = "Nineteen";
                        break;
                }
            }
            else
            {
                // .. otherwise it's between 20 and 99.
                switch (MyTens.Substring(0, 1))
                {
                    case "2":
                        Result = "Twenty ";
                        break;
                    case "3":
                        Result = "Thirty ";
                        break;
                    case "4":
                        Result = "Forty ";
                        break;
                    case "5":
                        Result = "Fifty ";
                        break;
                    case "6":
                        Result = "Sixty ";
                        break;
                    case "7":
                        Result = "Seventy ";
                        break;
                    case "8":
                        Result = "Eighty ";
                        break;
                    case "9":
                        Result = "Ninety ";
                        break;
                    default:
                        break;
                }

                // Convert ones place digit.
                Result = Result + ConvertDigit(MyTens.Substring(MyTens.Length - 1));
            }

            return Result;
        }

        private static string ConvertDigit(string MyDigit)
        {
            string tempConvertDigit = "";

            switch (MyDigit)
            {
                case "1":
                    tempConvertDigit = "One";
                    break;
                case "2":
                    tempConvertDigit = "Two";
                    break;
                case "3":
                    tempConvertDigit = "Three";
                    break;
                case "4":
                    tempConvertDigit = "Four";
                    break;
                case "5":
                    tempConvertDigit = "Five";
                    break;
                case "6":
                    tempConvertDigit = "Six";
                    break;
                case "7":
                    tempConvertDigit = "Seven";
                    break;
                case "8":
                    tempConvertDigit = "Eight";
                    break;
                case "9":
                    tempConvertDigit = "Nine";
                    break;
            }
            return tempConvertDigit;
        }


        //Function for conversion of Numbers into Readable words like first, second, third

        public static string GetRupeesToReadableWord(string RupeesValue)
        {

            string amountInWords = GetRupeesToWord(RupeesValue);
            amountInWords = amountInWords.Replace("Only", "");
            amountInWords = amountInWords.Replace("Rupees", "");
            amountInWords = amountInWords.Trim();

            if (amountInWords == "One")
                amountInWords = "First ";
            else if (amountInWords == "Two")
                amountInWords = "Second ";
            else if (amountInWords == "Three")
                amountInWords = "Third ";
            else if (amountInWords == "Five")
                amountInWords = "Fifth ";
            else
                amountInWords = amountInWords + "th ";

            return amountInWords;
        }
    }

    public static class ConvertCurrencyInWord
    {
        public static string VoucherCurrencySymbol = string.Empty;
        public static string VoucherCurrencyName = string.Empty;

        /// <summary>
        /// To the words.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToWords(string value)
        {
            string currencyname = "Dollars";
            string decimals = "";
            string decimalsep = ".";
            string strWords = string.Empty;
            string input = SettingProperty.Current.NumberSet.ToDouble( value).ToString("#.#0");// Math.Round(value, 2).ToString();
            try
            {
                if (!string.IsNullOrEmpty(SettingProperty.Current.DecimalSeparator))
                {
                    decimalsep = SettingProperty.Current.DecimalSeparator;
                }

                decimalsep = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

                //On 26/10/2024, To Set Currency Name ---------------------------------------------------------------
                if (!string.IsNullOrEmpty(SettingProperty.Current.Currency))
                {
                    currencyname = SettingProperty.Current.CurrencyName;
                }
                if (SettingProperty.Current.AllowMultiCurrency == 1 && !string.IsNullOrEmpty(VoucherCurrencyName))
                {
                    currencyname = VoucherCurrencyName;
                }
                //----------------------------------------------------------------------------------------------------

                if (input.Contains(decimalsep))
                {
                    decimals = input.Substring(input.IndexOf(decimalsep) + 1);
                    if (SettingProperty.Current.NumberSet.ToDouble(decimals) == 0)
                        decimals = string.Empty;

                    // remove decimal part from input
                    input = input.Remove(input.IndexOf(decimalsep));
                }

                // Convert input into words. save it into strWords
                if (!string.IsNullOrEmpty(input))
                {
                    strWords = GetWords(input) + " " + currencyname;


                    if (decimals.Length > 0)
                    {
                        // if there is any decimal part convert it to words and add it to strWords.
                        strWords += " and " + GetWords(decimals) + " Cents";
                    }

                    strWords = strWords + " only";
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error message Amount in Words - " + err.Message);
                strWords = SettingProperty.Current.NumberSet.ToNumber(SettingProperty.Current.NumberSet.ToDouble(value));
            }
            return strWords ;
        }

        private static string GetWords(string input)
	{
		// these are seperators for each 3 digit in numbers. you can add more if you want convert beigger numbers.
		string[] seperators = { "", " Thousand ", " Million ", " Billion " };

		// Counter is indexer for seperators. each 3 digit converted this will count.
		int i = 0;

		string strWords = "";

		while (input.Length > 0)
		{
			// get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
			string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
			// remove the 3 last digits from input. if there is not 3 numbers just remove it.
			input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

			int no = int.Parse(_3digits);
			// Convert 3 digit number into words.
			_3digits = GetWord(no);

			// apply the seperator.
			if(_3digits!="")
			{
			    _3digits += seperators[i];
			}
			// since we are getting numbers from right to left then we must append resault to strWords like this.
			strWords = _3digits + strWords;

			// 3 digits converted. count and go for next 3 digits
			i++;
		}
		return Regex.Replace(strWords, @"\s+", " ");
	}

        // your method just to convert 3digit number into words.
        private static string GetWord(int no)
        {
            string[] Ones =
		{
			"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
			"Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
		};

            string[] Tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string word = "";

            if (no > 99 && no < 1000)
            {
                int i = no / 100;
                word = word + Ones[i - 1] + " Hundred ";
                no = no % 100;
            }

            if (no > 19 && no < 100)
            {
                int i = no / 10;
                word = word + Tens[i - 1] + " ";
                no = no % 10;
            }

            if (no > 0 && no < 20)
            {
                word = word + Ones[no - 1];
            }

            return word;
        }
    }
}

