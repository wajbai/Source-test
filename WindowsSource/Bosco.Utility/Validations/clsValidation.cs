using System;
using System.Globalization;
using System.Windows.Forms;



namespace Bosco.Utility.Validations
{
	public class clsValidation
	{
		public clsValidation()
		{
			
		}

		KeyPressEventArgs keyPressEventArgs;

		public void allowNumbers(KeyPressEventArgs e)
		{
			this.keyPressEventArgs = e;
			keyPressEventArgs.Handled = true;

			if ((e.KeyChar <= 57) && (e.KeyChar >= 48) || (e.KeyChar == 13) || 
				(e.KeyChar == 8) || (e.KeyChar == 46))
				e.Handled = false;
		}

		public static string getFormattedDate(string sDate)
		{
			string sfDate = "";

			try
			{
				if (sDate != "")
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime d = DateTime.Parse(sDate,f);
					sfDate = d.ToString("dd-MMM-yyyy"); 
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return sfDate;
		}

		public static string getFormattedDateTime(string sDate)
		{
			string sfDate = "";

			try
			{
				if (sDate != "")
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime d = DateTime.Parse(sDate,f);
					sfDate = d.ToString("dd-MMM-yyyy hh:mm tt");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return sfDate;
		}
		public static string getFormattedDateTimeWithmmss(string sDate)
		{
			string sfDate = "";

			try
			{
				if (sDate != "")
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime d = DateTime.Parse(sDate,f);
					sfDate = d.ToString("dd-MMM-yyyy hh:mm:ss tt");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return sfDate;
		}
		public static string getDDMMYYYYFormattedDate(string sDate)
		{
			string sfDate = "";

			try
			{
				if (sDate != "")
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime d = DateTime.Parse(sDate,f);
					sfDate = d.ToString("dd/MM/yyyy"); 
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return sfDate;
		}

		public static bool isValidateDateTime(string sDateTime, string strCaption)
		{
			bool bIsDateTime = false;

			try
			{
				if (sDateTime != "")
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime d = DateTime.Parse(sDateTime,f);
				}
				bIsDateTime = true;
			}
			catch
			{
				MessageBox.Show("Invalid Date Format.\nEnter " + strCaption + " in DD/MM/YYYY HH:MM AM Format", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return bIsDateTime;
		}

		public static bool isValidateDate(string sDate, string strCaption)
		{
			bool bIsDate = false;

			try
			{
				if (sDate != "")
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime d = DateTime.Parse(sDate,f);
				}
				bIsDate = true;
			}
			catch
			{
				MessageBox.Show("Invalid Date. Enter " + strCaption + " in DD/MM/YYYY Format","Payroll",MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return bIsDate;
		}

		public static bool isValidateDate(string sDate)
		{
			bool bIsDate = false;

			try
			{
				DateTime d = DateTime.Parse(sDate);				
				bIsDate = true;
			}
			catch (Exception )
			{
				bIsDate = false;
			}
			return bIsDate;
		}

		public static bool isValidateAmount(string sTemp)
		{
			if (sTemp != "")
			{
				if (sTemp.EndsWith("."))	sTemp += "00";

				try
				{
					Convert.ToDecimal(sTemp);
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		public static bool isValidateAmount(TextBox textBox, string sCaption)
		{
			string sTemp = textBox.Text;

			if (sTemp != "")
			{
				if (sTemp.EndsWith("."))	sTemp += "00";

				try
				{
					Convert.ToDecimal(sTemp);
				}
				catch
				{
					MessageBox.Show("Enter valid Amount for " + sCaption, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}
			return true;
		}
		
		public static bool isGreaterThan( string sDateGreater, string sDateSmaller)
		{
			bool bReturn = true;
			try
			{
				if(sDateSmaller == "" | sDateGreater == "")
					bReturn = false;
				else
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime dS = DateTime.Parse(sDateSmaller, f);
					DateTime dG = DateTime.Parse(sDateGreater, f);
					bReturn = (dG > dS);
				}
			}
			catch
			{
				bReturn = false;
			}
			return bReturn;
		}

		public static bool isGreaterOrEqualTo( string sDateGreater, string sDateSmaller)
		{
			bool bReturn = true;
			try
			{
				if(sDateSmaller == "" | sDateGreater == "")
					bReturn = false;
				else
				{
					IFormatProvider f =	new CultureInfo("en-GB", true);
					DateTime dS = DateTime.Parse(sDateSmaller, f);
					DateTime dG = DateTime.Parse(sDateGreater, f);
					bReturn = (dG >= dS);
				}
			}
			catch
			{
				bReturn = false;
			}
			return bReturn;
		}

		public static string getValidDateTime(string sDateTime)
		{
			try
			{
                if (sDateTime != "")
                {
                    IFormatProvider f = new CultureInfo("en-GB", true);
                    DateTime d = DateTime.Parse(sDateTime, f);
                    return d.ToString("dd/MM/yyyy hh:mm tt");
                }
                else
                    return System.DateTime.Now.ToString();
			}
			catch
			{
                return System.DateTime.Now.ToString();
			}
		}
	}
}
