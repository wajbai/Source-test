using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Windows.Forms;
 
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Payroll.Utility.Common
{
	/// <summary>
	/// Summary description for clsPayrollSlipPrinting.
	/// </summary>
    public class clsPayrollSlipPrinting 
	{
		public clsPayrollSlipPrinting()
		{
			PrintSetting();
		}

		private bool bPrintOnMsgBox = false;

		private string sSql = "";
		private DataSet ds = null;
		private DataView dv = null;
		private string sTemplatePath = "";
		private string sPrinterName = "";
		private string[] sPrintText;
		private int iFieldWidth = 5;

		public int FieldWidth
		{
			set{this.iFieldWidth = value;}
			get{return this.iFieldWidth;}
		}

		public string PrinterName
		{
			set{this.sPrinterName = value;}
			get{return this.sPrinterName;}
		}

		public string TemplatePath
		{
			set{this.sTemplatePath = value;}
			get{return this.sTemplatePath;}
		}

		private void PrintSetting()
		{
			string sPa = Application.StartupPath + "\\PrintOnMessageBox.dat";
			
			if (!File.Exists(sPa))
			{
				File.Create(sPa).Close();
				StreamWriter SW = new StreamWriter(sPa);
				SW.WriteLine("FALSE");
				SW.Close();
			}
			StreamReader SR = new StreamReader(sPa);
			bPrintOnMsgBox = SR.ReadLine().ToUpper()=="TRUE" ? true : false;
			SR.Close();
		}

		public string[] PrintPayrollSlip(string sSQL)
		{
            //this.sSql = sSQL;
            
            //if (createDataSet(sSQL, "PayrollSlip") == null)
                //return PrintPayrollSlip(getDataSet(), "PayrollSlip");
            return null;
		}

		public string[] PrintPayrollSlip(DataSet dsPrint,string sTableName)
		{
			this.ds = dsPrint;
            DataTable dtNull = new DataTable();
			return PrintPayrollSlip(new DataView(dsPrint.Tables[sTableName]),dtNull,dtNull);
		}

		public string[] PrintPayrollSlip(DataView dvPrint,DataTable dtHeader,DataTable dtPaySlip)
		{
			this.dv = dvPrint;
			string strTemp = "", colName = "";

			if (!File.Exists(this.sTemplatePath))
			{
				MessageBox.Show("Printer and Template are not selected for this payroll slip","Payroll",
					MessageBoxButtons.OK,MessageBoxIcon.Information);
				return null;
			}

			StreamReader SR = new StreamReader(this.sTemplatePath);	

			string sTemplate =  SR.ReadToEnd();
			sTemplate = sTemplate.Replace("<DATE>",System.DateTime.Now.ToShortDateString());
			sTemplate = sTemplate.Replace("<MONTH>",clsGeneral.PAYROLL_MONTH);
            if (dtHeader.Rows.Count > 0)
            {
                sTemplate = sTemplate.Replace("<FROM_DATE>", dtHeader.Rows[0]["FROMDATE"].ToString());
                sTemplate = sTemplate.Replace("<TO_DATE>", dtHeader.Rows[0]["TODATE"].ToString());
            }
            else
            {
                sTemplate = sTemplate.Replace("<FROM_DATE>", " ");
                sTemplate = sTemplate.Replace("<TO_DATE>", " ");
            }
			string[] strWhole = new string [dvPrint.Count];
			string[] strReturn = new string [dvPrint.Count];
			for(int i=0;i<dvPrint.Count;i++)
			{
				string sTemp =  sTemplate;
				for (int k=0;k<dvPrint.Table.Columns.Count;k++)
				{
					strTemp = dvPrint[i][k].ToString();
					colName = dvPrint.Table.Columns[k].ColumnName;
					colName = "<" + colName + "-1>";
					switch(dvPrint.Table.Columns[k].DataType.Name.ToString().ToUpper())
					{
						case "INT32":
							if(strTemp != "")
                                strTemp = int.Parse(strTemp).ToString("#,##,##,##0.00").ToString();
							else
								strTemp = "0";

							strTemp = FormatString(strTemp, this.iFieldWidth, "R");
							break;
						case "DOUBLE"://case "DECIMAL":case "LONG":case "INT64":case "INT32":
							if(strTemp != "")
								strTemp = double.Parse(strTemp).ToString("#,##,##,##0.00").ToString();
							else
								strTemp = "0.00";

							strTemp = FormatString(strTemp, this.iFieldWidth, "R");
							break;
						default:
							//strTemp = FormatString(strTemp, this.iFieldWidth, "L");
							break;
					}
					if (this.iFieldWidth > colName.Length)
						colName = colName.PadRight(this.iFieldWidth,' ');
					sTemp = sTemp.Replace(colName,strTemp);
				}
                for (int k = 0; k < dtPaySlip.Columns.Count; k++)
                {
                    strTemp = dtPaySlip.Rows[i][k].ToString();
                    colName = dtPaySlip.Columns[k].ColumnName;
                    colName = "<" + colName + "-1>";
                    switch (dtPaySlip.Columns[k].DataType.Name.ToString().ToUpper())
                    {
                        case "INT32":
                            if (strTemp != "")
                                strTemp = int.Parse(strTemp).ToString().ToString();
                            else
                                strTemp = "0";

                            strTemp = FormatString(strTemp, this.iFieldWidth, "R");
                            break;
                        case "DOUBLE"://case "DECIMAL":case "LONG":case "INT64":case "INT32":
                            if (strTemp != "")
                                strTemp = double.Parse(strTemp).ToString("#,##,##,##0.00").ToString();
                            else
                                strTemp = "0.00";

                            strTemp = FormatString(strTemp, this.iFieldWidth, "R");
                            break;
                        default:
                            if (colName == "<MINWAGESBASIC-1>" || colName == "<MINWAGESDA-1>"|| colName == "<TOTAL-1>" )
                            {
                                if (strTemp != "")
                                    strTemp = double.Parse(strTemp).ToString("#,##,##,##0.00").ToString();
                                else
                                    strTemp = "0.00";
                                strTemp = FormatString(strTemp, this.iFieldWidth, "R");
                            }
                            break;
                    }
                    if (this.iFieldWidth > colName.Length)
                        colName = colName.PadRight(this.iFieldWidth, ' ');
                    sTemp = sTemp.Replace(colName, strTemp);
                }
				strWhole[i] = sTemp;
			}
			this.sPrintText = strWhole;

			for(int i=0;i<strWhole.Length;i++)
				strReturn[i] = removeFontTags(strWhole[i]);
			return strReturn;
		}

		public string FormatString(string str,int width,string Alignment)
		{
			int strLen =0;
			strLen = str.Length;

			if (strLen>width)
			{
				//Truncating when string exceeds the width
				str=str.Substring(0,width);
			}
			else
				//Aligning the string with filling Empty Spaces
				str =(Alignment =="L")?str.PadRight(width,' '):str.PadLeft(width,' ');
			return str;

		}

		public void PrintText()
		{
			try
			{
				string sPrinter = "";
				if(this.PrinterName != "")
					sPrinter = this.PrinterName;
				else
					sPrinter = new PrinterSettings().DefaultPageSettings.PrinterSettings.PrinterName;
				for(int i=0;i<sPrintText.Length;i++)
				{
					if(bPrintOnMsgBox == false)
						RawPrinterHelper.SendStringToPrinter(sPrinter, replaceFontTags(sPrintText[i]));
					else
						MessageBox.Show(removeFontTags(sPrintText[i]));
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private string replaceFontTags(string str)
		{
			if(str=="")
				return "";
			string sSubStr = "";
			int iPos = 0;
			str = str.Replace("<TIT>","<C18><C14>");
			str = str.Replace("</TIT>","<C18>");
			str = str.Replace("<CON>","<C15>");
			str = str.Replace("</CON>","<C18>");
			str = str.Replace("<NOR>","<C18>");
			str = str.Replace("</NOR>","<C15>");
			str = str.Replace("<NRB>","<C15><C14>");
			str = str.Replace("</NRB>","<C18>");

			while(iPos < str.Length)
			{
				if(str.IndexOf("<C", iPos)>=0)
				{
					try
					{
						sSubStr = str.Substring(str.IndexOf("<C", iPos)+2, str.IndexOf(">",str.IndexOf("<C", iPos))-str.IndexOf("<C", iPos)-2);
						string [] sTwoChars = sSubStr.Split(',');
						sSubStr = str.Substring(str.IndexOf("<C", iPos), str.IndexOf(">",str.IndexOf("<C", iPos))-str.IndexOf("<C", iPos)+1);
						iPos++;
						char ch = new char();
						foreach(string sVal in sTwoChars)
							ch += (char)int.Parse(sVal);
						str = str.Replace(sSubStr, "" + ch);
					}
					catch
					{
						iPos = str.IndexOf(">",str.IndexOf("<C", iPos))+1;
					}
				}
				else
					iPos = str.Length;
			}
			return str;
		}

		private string removeFontTags(string str)
		{
			if(str=="")
				return "";
			string sSubStr = "";
			int iPos = 0;
			str = str.Replace("<TIT>","<C18><C14>");
			str = str.Replace("</TIT>","<C18>");
			str = str.Replace("<CON>","<C15>");
			str = str.Replace("</CON>","<C18>");
			str = str.Replace("<NOR>","<C18>");
			str = str.Replace("</NOR>","<C15>");
			str = str.Replace("<NRB>","<C15><C14>");
			str = str.Replace("</NRB>","<C18>");

			while(iPos < str.Length)
			{
				if(str.IndexOf("<C", iPos)>=0)
				{
					try
					{
						sSubStr = str.Substring(str.IndexOf("<C", iPos)+2, str.IndexOf(">",str.IndexOf("<C", iPos))-str.IndexOf("<C", iPos)-2);
						string [] sTwoChars = sSubStr.Split(',');
						sSubStr = str.Substring(str.IndexOf("<C", iPos), str.IndexOf(">",str.IndexOf("<C", iPos))-str.IndexOf("<C", iPos)+1);
						iPos++;
						char ch = new char();
						foreach(string sVal in sTwoChars)
							ch += (char)int.Parse(sVal);
						str = str.Replace(sSubStr, "");
					}
					catch
					{
						iPos = str.IndexOf(">",str.IndexOf("<C", iPos))+1;
					}
				}
				else
					iPos = str.Length;
			}
			return str;
		}

	}
}
