using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing;
using System.Collections.Specialized;

namespace Bosco.Utility.Common
{
	public class PrintTemplate
	{
		public PrintTemplate()
		{
			PrintSetting();
		}

		//private OracleConnection objCon;
		//private OracleDataAdapter objDa;

		private DataSet ds = new DataSet();
		private DataTable  dsTemplate = new DataTable ();
		private DataSet dsWordWrap = new DataSet();
		private DataSet dsAdvanced = new DataSet();
		private bool bWordWrap = false;
		private bool bMargeColumn = false;
		private string _BillType ="OPD";
		private string _BillNo = "";
		private bool _Eject = false;
		private bool _Condensed = true;
		private bool bPrintOnMsgBox = false;	// Added by James, true - MessageBox, false - PrintToPrinter

		//private string tableName;
		
		private string _Template ="";
		private string _Sql1 ="" ;
		private string _Sql2 ="";
		private string _Sql3 ="";
		private Int32 _PaperSize =0;
		private Int32 _ListItems =0;
		private Int32 nList=0;
		private string printername ="";
		private string _ReportModule= "";
		private string strDataToPrint = "";
		PrintDocument pdoc = new PrintDocument();
		// For Lab Module
		private DataTable _dtLab = null;
		private DataSet _dsLab =null;
		private bool _Print_Lab_Report = false;
		private bool _Print_Lab_Report_in_LPQ_Mode = false;
		private bool _Print_Register = false;
		private bool _Avoid_Empty_Lines = false;
		private string _Lab_Report_Path = "";
		private bool _Print_Header_All_Pages = true;
        private bool _Print_Footer_All_Pages = true;

		private bool bFreePrinting = false;
		private string strHeader = "";
		private string strFooter = "";
		private DataTable dtMainContent = null;
		//-----------------------------------------------
		private string sAdmPrinterName = "";		// --- CHANGES DONE IN GUNTUR -----------

		private System.Collections.ArrayList objAL;		// --- Gerald on 13-Oct-2005 12:15 PM ---------------
		private int iRow = 0;
		private string strTemplateText = "";
		private string [] sPreviewText;
		private bool bPreview = false;

		public bool GetPreviewText
		{
			set{this.bPreview = value;}
		}

		public string [] PreviewText
		{
			get
			{
				this.bPreview = false;
				return this.sPreviewText;
			}
		}

		public string BillType
		{
			get {return _BillType;}
			set {_BillType = value;}			
		}

		public string BillNo
		{
			get {return _BillNo;}
			set {_BillNo = value;}
		}

		public string Template
		{
			get {return _Template;}
			set {_Template = value;}
		}

		public string Sql1
		{
			get {return _Sql1;}
			set {_Sql1 = value;}
		}

		public string Sql2
		{
			get {return _Sql2;}
			set {_Sql2 = value;}
		}

		public string Sql3
		{
			get {return _Sql3;}
			set {_Sql3 = value;}
		}

		public Int32 PaperSize
		{
			get {return _PaperSize;}
			set {_PaperSize = value;}
		}

		public Int32 ListItems
		{
			get {return _ListItems;}
			set {_ListItems = value;}
		}

		public bool Eject
		{
			get {return _Eject;}
			set {_Eject= value;}
		}

		public bool Condensed
		{
			get {return _Condensed;}
			set {_Condensed= value;}
		}

		public bool AvoidEmptyLines
		{
			get{return _Avoid_Empty_Lines;}
			set{_Avoid_Empty_Lines = value;}
		}

		public string ReportModule
		{
			get {return _ReportModule;}
			set {_ReportModule = value;}
		}

		public DataTable dtLab
		{
			get {return _dtLab;}
			set {_dtLab = value;}
		}

		public DataSet dsLab
		{
			get {return _dsLab;}
			set {_dsLab = value;}
		}
		
		public DataTable MainContent
		{
			get {return dtMainContent;}
			set {dtMainContent = value;}
		}

		public bool PrintLabReport
		{
			get {return _Print_Lab_Report;}
			set {_Print_Lab_Report = value;}
		}

		//======By Peter on 25-03-2009 To incorporate Laser Printing
		public bool PrintLabReportInLPQMode
		{
			get {return _Print_Lab_Report_in_LPQ_Mode;}
			set {_Print_Lab_Report_in_LPQ_Mode = value;}
		}
		//=========

		public string LabReportPath
		{
			get {return _Lab_Report_Path;}
			set {_Lab_Report_Path = value;}
		}

		public bool PrintRegister
		{
			get {return _Print_Register;}
			set {_Print_Register= value;}
		}
		
		public bool FreePrinting
		{
			get {return bFreePrinting;}
			set {bFreePrinting= value;}
		}
		
		public string Header
		{
			get {return strHeader;}
			set {strHeader = value;}
		}

		public string Footer
		{
			get {return strFooter;}
			set {strFooter = value;}
		}

		public bool PrintHeaderInAllPages
		{
			get{return _Print_Header_All_Pages;}
			set{_Print_Header_All_Pages = value;}
		}

		public bool PrintFooterInAllPages
		{
			get{return _Print_Footer_All_Pages;}
			set{_Print_Footer_All_Pages = value;}
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

		public bool PrintBillingNormal()
		{
			string str ="";
			//StreamReader Sr= File.OpenText(@"c:\OPD.txt");

			string strTemp = "";
			string colName = "";
			string strpath = "";
			bool bPrint = false;
			StreamReader Sr;
			
			DataSet dsPath = new DataSet();
			DataView dv = new DataView();

			if (!File.Exists(Application.StartupPath + "\\" + ReportModule + ".dat"))
			{
				MessageBox.Show("Printer and Template are not selected for this report","Payroll",
					MessageBoxButtons.OK,MessageBoxIcon.Information);
				goto EndLine;
			}

			StreamReader TempPath = new StreamReader(Application.StartupPath + "\\" + ReportModule + ".dat");	
			
			string strWhole = TempPath.ReadToEnd();
			string [] strTPWhole = strWhole.Split('\n');

			for (int i = 0; i<=strTPWhole.GetUpperBound(0);i++)
			{
				string [] strLine = strTPWhole[i].Split('@');
				if (strLine[0]==BillType)
				{
					try
					{
						printername = strLine[1];

						// --- CHANGES DONE IN GUNTUR ------------------------------------------

						if (_BillType == "ADM")	sAdmPrinterName = printername;

						// --- ENDS HERE -------------------------------------------------------

						strpath = strLine[2];
						bPrint = Convert.ToBoolean(strLine[3]);
						try
						{
							_Condensed = Convert.ToBoolean(strLine[4]);
						}
						catch{}
						break;
					}
					catch
					{
						return false;
					}
				}
			}

			if (bPrint==false) goto EndLine;

			if (printername=="")
			{
				MessageBox.Show("Printer is not selected for the Report","Payroll",MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				goto EndLine;
			}

			if (strpath=="")
			{
				MessageBox.Show("Template path is not selected for the Report","Payroll",
					MessageBoxButtons.OK,MessageBoxIcon.Information);
				goto EndLine;
			}
			
		
			try
			{
				Sr= File.OpenText(strpath);
			}
			catch
			{
				MessageBox.Show("Template is not available","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return false;
			}
			int width =0;
			string align ="";

			DataSet ds1 = new DataSet();
			DataSet ds2 = new DataSet();
			DataSet ds3 = new DataSet();
		

			int Bills = 0;
			int nItems =0;

            //base.createDataSet("SELECT distinct * FROM PRINTBILLING WHERE BILLTYPE = '" + _BillType
            //    + "'", "BILLTYPE");
            //dsTemplate = base.getDataSet();			//dsTemplate = ds;

            //if (_Sql1 != "")
            //{
            //    base.createDataSet(_Sql1, "Table1");
            //    ds1 = base.getDataSet();			//ds1 = ds;
            //}
            //if (_Sql2 != "")
            //{
            //    base.createDataSet(_Sql2, "Table2");
            //    ds2 = base.getDataSet();			//ds2 = ds;
            //    nList = ds2.Tables["Table2"].Rows.Count;
            //}
            //else
            //{
            //    nList = 0;
            //}
            //if (_Sql3 != "")
            //{
            //    base.createDataSet(_Sql3, "Table3");
            //    ds3 = base.getDataSet();			//ds3=ds;
            //}

            //nItems = ds1.Tables[0].Rows.Count;
            //if (nItems == 0)
            //{
            //    MessageBox.Show("No Records Found", "Payroll");
            //    goto EndLine;
            //}

			//Calculate the possible number of bills
			Bills = 1;

			try
			{
				if (nList<=_ListItems)
				{
					Bills =1;	
				}
				else
				{
					Bills = nList/_ListItems;
					if (nList%_ListItems>0) 
						Bills++;
				}
			}
			catch
			{
				Bills = 1;
			}

			string [] strArr = new string[Bills];
			for (int i=0;i<Bills;i++)
			{
				str = Sr.ReadToEnd();
				strTemplateText = str;
				dv = new DataView(dsTemplate);

				//Fillig the Header Items
				if (_Sql1=="")
					goto ReplaceFooter;

				for (int k=0;k<ds1.Tables["Table1"].Columns.Count;k++)
				{	
					strTemp = ds1.Tables["Table1"].Rows[0][k].ToString();
					colName = ds1.Tables[0].Columns[k].ColumnName;
					dv.RowFilter = "FieldName = '" + colName + "'";
					if(dv.Count == 0) continue;
					width = (dv.Count==0) ? 10 : Convert.ToInt32(dv[0][2].ToString());
					align = (dv.Count==0) ? "L" : dv[0][3].ToString();
					strTemp = FormatString(strTemp,width,align);
					colName = "<" + colName + "-1>";
					if (width>colName.Length) 
						colName = colName.PadRight(width,' ');
					str =str.Replace(colName,strTemp);
					dv.RowFilter="";
				}

			ReplaceFooter:
				if (_Sql3=="")
					goto ReplaceList;
				if(ds3.Tables[0].Rows.Count == 0) 
					goto ReplaceList;
				//Filling the Footer  Items
				for (int k=0;k<ds3.Tables[0].Columns.Count;k++)
				{	
					strTemp = ds3.Tables[0].Rows[0][k].ToString();
					colName = ds3.Tables[0].Columns[k].ColumnName;
					dv.RowFilter = "FieldName = '" + colName + "'";
					if(dv.Count == 0) continue;
					width = (dv.Count==0) ? 10 :Convert.ToInt32(dv[0][2].ToString());
					align = (dv.Count==0) ? "L" : dv[0][3].ToString();
					if (i<Bills-1)
					{
						if ("<TOTALRS-3>,<PAIDRS-3>,<CONCESRS-3>,<NETRS-3>".IndexOf("<" + colName + "-3>")>0)
							strTemp = FormatString("-",width,align);
						else
							strTemp = FormatString(strTemp,width,align);
					}
					else
						strTemp = FormatString(strTemp,width,align);

					colName = "<" + colName + "-3>";
					if (width>colName.Length)
						colName = colName.PadRight(width,' ');
					str =str.Replace(colName,strTemp);
					dv.RowFilter="";
				}

			ReplaceList:
				if (_Sql2=="")
					goto PrintLine;

				int Sno =0;
				string strSerial = "";
				string sZero ="";
				for (int j=0;j<ds2.Tables[0].Rows.Count;j++)
				{
					Sno++;
					sZero = (j<10)?"0":"";
					strSerial ="<SNO-2>" +  sZero + Sno.ToString();
					string sRep = Sno.ToString().PadRight(strSerial.Length,' ');
					str =str.Replace(strSerial, sRep);
					for (int k=0;k<ds2.Tables[0].Columns.Count;k++)
					{		
						strTemp = ds2.Tables[0].Rows[j][k].ToString();
						colName = ds2.Tables[0].Columns[k].ColumnName;
						dv.RowFilter = "FieldName = '" + colName + "'";
						if(dv.Count == 0) continue;
						width = (dv.Count==0) ? 10 : Convert.ToInt32(dv[0][2].ToString());
						align = (dv.Count==0) ? "L" : dv[0][3].ToString();
						strTemp = FormatString(strTemp,width,align);
						colName = "<" + colName + "-2>" + sZero + Sno.ToString();
						if (width>colName.Length) 
							colName = colName.PadRight(width,' ');
						str =str.Replace(colName,strTemp);
						dv.RowFilter="";
					}
				}

				//Clearing the List
				for (int j=Sno;j<=ListItems;j++)
				{
					string sRep ="";					
					sZero = (j<10)?"0":"";

					for (int k=0;k<ds2.Tables[0].Columns.Count;k++)
					{		
						//strTemp = ds2.Tables[0].Rows[0][k].ToString();
						sRep ="<SNO-2>" + sZero + j.ToString();
						str=str.Replace(sRep,"".PadRight(sRep.Length,' '));

						colName = ds2.Tables[0].Columns[k].ColumnName;
						dv.RowFilter = "FieldName = '" + colName + "'";
						sRep ="<" + colName + "-2>" + sZero + j.ToString();
						str=str.Replace(sRep,"".PadRight(sRep.Length,' '));
					}
				}

			PrintLine:
				//nList = nList;
				//Console.WriteLine(str);
				if(bPrintOnMsgBox == true)
                    MessageBox.Show(str);
				else
					PrintBillingNormal(str);
				return true;
			}
			EndLine:
				Console.WriteLine("No records found");
			return false;
		}

		// --------- Gerald on 13-Oct-2005 :: 12.15 PM ----------------------------------------------------

		public bool checkWordWrap(DataTable objDS)
		{
			DataView objDV = new DataView(objDS);
			objDV.RowFilter = "WordWrap = 1" ;
			return (objDV.Count > 0);
		}

		/*
		* Function : getAdvancedDataSet(DataSet, DataView)
		*	Author : ntanand
		*	  Date : 21-Oct-2005
		*  Purpose : To Create New Data Set with Features like Wardwrap and Marge, 
		*			 based on the setting available in PrintBilling Table.
		**/

		public DataSet getAdvancedDataSet(DataSet ds, DataView dv)
		{
			int iColumnCount = 0;

			DataSet objDS = new DataSet();

			DataTable objDT = new DataTable("Word Wrap DS");
			//following tables holed data form DS for ref.
			DataTable PrevDT = new DataTable("Previous DT");
			DataTable CurrDT = new DataTable("Current DT");

			iColumnCount = ds.Tables[0].Columns.Count;

			for (int j=0; j<iColumnCount; j++)
			{
				//Creating Columns for the Final Table
				DataColumn objDC = new DataColumn(ds.Tables[0].Columns[j].Caption);
				objDC.DataType = System.Type.GetType("System.String");
				objDT.Columns.Add(objDC);

				objDC = new DataColumn(ds.Tables[0].Columns[j].Caption);
				objDC.DataType = System.Type.GetType("System.String");
				PrevDT.Columns.Add(objDC); 
				objDC = new DataColumn(ds.Tables[0].Columns[j].Caption);
				objDC.DataType = System.Type.GetType("System.String");
				CurrDT.Columns.Add(objDC);
			}

			DataRow previousRow = PrevDT.NewRow();
			PrevDT.Rows.Add(previousRow);
			//initialize Previous PrevDT with Empty String
			DataRow currentRow = CurrDT.NewRow();
			CurrDT.Rows.Add(currentRow);

			for (int i=0; i<ds.Tables[0].Rows.Count; i++)
			{
				for (int iCurr =0 ; iCurr < iColumnCount; iCurr++)
					CurrDT.Rows[0][iCurr] = ds.Tables[0].Rows[i][iCurr].ToString();

				if (bMargeColumn) 
                    CurrDT = getOperationRow(PrevDT,CurrDT,dv);

				int iRowCount = splitColValues(CurrDT.Rows[0], dv,0);

				//Creating Rows for the Final Table
				for (int j=0; j<=iRowCount; j++)
				{
					DataRow objDR = objDT.NewRow();
					objDT.Rows.Add(objDR);
				}

				int l=0;
				System.Collections.IEnumerator myEnumerator = objAL.GetEnumerator();
				while (myEnumerator.MoveNext())
				{
					string strTemp = "";
					string[] strT  = new string[iRowCount+1];
					int iTemp = iRow;
					dv.RowFilter = "";
					dv.RowFilter = "FIELDNAME= '" + ds.Tables[0].Columns[l].Caption + "'";
//					if(dv.Count == 0)
//					{
//						continue;
//					}
					if(dv[0]["WordWrap"].ToString() == "1")
					{
						strT = (string[]) myEnumerator.Current;

						for(int m=0; m<=strT.GetUpperBound(0); m++)
						{
							objDT.Rows[iRow][l] = strT[m];
							iRow++;
						}

						if(strT.GetUpperBound(0) < iRowCount)
						{
							for (int n=strT.GetUpperBound(0); n<iRowCount; n++)
							{
								objDT.Rows[iRow][l] = " ";
								iRow++;
							}
						}
					}
					else
					{
						strTemp = myEnumerator.Current.ToString();

						objDT.Rows[iRow][l] = strTemp;
						iRow++;

						for(int m=1; m<=iRowCount; m++)
						{
							objDT.Rows[iRow][l] = " ";
							iRow++;
						}
					}
					iRow = iTemp;
					l++;
				}

				int iVal = iRow;
				objDT.Rows[iRow]["SNO"] = (i+1).ToString();
				iRow++;

				for (int a=1; a<=iRowCount; a++)
				{
					objDT.Rows[iRow]["SNO"] = " ";
					iRow++;
				}

				iRow = iVal;

				iRow = iRow + (iRowCount + 1);

				//Fill PrevDT To Checkwith next row for marge operation
				for (int iPrev =0 ; iPrev < iColumnCount; iPrev++)
                    PrevDT.Rows[0][iPrev] = ds.Tables[0].Rows[i][iPrev].ToString();
			}
			objDS.Tables.Add(objDT);
			iRow = 0;
			return objDS;
		}

		private DataSet getWordWrapDataSet(DataSet ds, DataView dv)
		{
			DataSet objDS = new DataSet();
			DataTable objDT = new DataTable("Word Wrap DS");

			for (int i=0; i<ds.Tables[0].Rows.Count; i++)
			{

				int iRowCount = splitColValues(ds.Tables[0].Rows[i], dv,i);

				//Creating Columns for the Final Table
				if (i == 0)
				{
					for (int j=0; j<ds.Tables[0].Columns.Count; j++)
					{
						DataColumn objDC = new DataColumn(ds.Tables[0].Columns[j].Caption);
						objDC.DataType = System.Type.GetType("System.String");
						objDT.Columns.Add(objDC);
					}

//					DataColumn objDataColumn = new DataColumn("SNO");
//					objDataColumn.DataType = System.Type.GetType("System.String");
//					objDT.Columns.Add(objDataColumn);
				}

				//Creating Rows for the Final Table
				for (int j=0; j<=iRowCount; j++)
				{
					DataRow objDR = objDT.NewRow();
					objDT.Rows.Add(objDR);
				}

				int l=0;
				System.Collections.IEnumerator myEnumerator = objAL.GetEnumerator();
				while (myEnumerator.MoveNext())
				{
					string strTemp = "";
					string[] strT  = new string[iRowCount+1];
					int iTemp = iRow;

					dv.RowFilter = "FIELDNAME= '" + ds.Tables[0].Columns[l].Caption + "'";
					if(dv[0]["WordWrap"].ToString() == "1")
					{
						strT = (string[]) myEnumerator.Current;

						for(int m=0; m<=strT.GetUpperBound(0); m++)
						{
							objDT.Rows[iRow][l] = strT[m];
							iRow++;
						}

						if(strT.GetUpperBound(0) < iRowCount)
						{
							for (int n=strT.GetUpperBound(0); n<iRowCount; n++)
							{
								objDT.Rows[iRow][l] = " ";
								iRow++;
							}
						}
					}
					else
					{
						strTemp = myEnumerator.Current.ToString();

						objDT.Rows[iRow][l] = strTemp;
						iRow++;

						for(int m=1; m<=iRowCount; m++)
						{
							objDT.Rows[iRow][l] = " ";
							iRow++;
						}
					}
					iRow = iTemp;
					l++;
					dv.RowFilter = "";
				}

				int iVal = iRow;
				objDT.Rows[iRow]["SNO"] = (i+1).ToString();
				iRow++;

				for (int a=1; a<=iRowCount; a++)
				{
					objDT.Rows[iRow]["SNO"] = " ";
					iRow++;
				}

				iRow = iVal;
				iRow = iRow + (iRowCount + 1);
			}
			objDS.Tables.Add(objDT);
			iRow = 0;
			return objDS;
		}

		/*
		* Function : getOperationRow(DataRow,DataRow,DataView)
		*	Author : ntanand
		*	  Date : 21-Oct-2005
		*  Purpose : To Modify DataRow baes on the marge condition set.
		*/
		private DataTable getOperationRow(DataTable preDT,DataTable curDT , DataView condiDV  )
		{
			int iColCount = curDT.Columns.Count;
			
			for (int i =0; i<iColCount; i++)
			{
				condiDV.RowFilter = "FIELDNAME= '" + curDT.Columns[i].Caption +"'";

				if (condiDV[0]["Merge"].ToString() =="1")
				{
					if (preDT.Rows[0][i].ToString() == curDT.Rows[0][i].ToString())
						curDT.Rows[0][i]= "";
				}
				condiDV.RowFilter ="";
			}
			return curDT;
		}

		/*
		 * Function : checkMargeColumn(DataSet)
		 * Author	: ntanand
		 * Date		: 21-Oct-2005
		 * purpose	: To check weather given template required Marge Feature
		 **/
		public bool checkMargeColumn(DataTable objDS)
		{
			DataView objDV = new DataView(objDS);
			objDV.RowFilter = "Merge = 1" ;
			return (objDV.Count > 0);
		}

		private int splitColValues(DataRow dr, DataView dv,int nRow)
		{
			objAL = new System.Collections.ArrayList();
			int iColCount = dr.Table.Columns.Count;
			int iRowCount = 0;

			for (int i=0; i<iColCount; i++)
			{
				dv.RowFilter = "FIELDNAME= '" + dr.Table.Columns[i].Caption + "'"; 
//				if(dv.Count == 0)
//				{
//					objAL.Add(dr.Table.Rows[nRow][i].ToString());
//					continue;
//				}
				if(dv[0]["WordWrap"].ToString() == "1")
				{
					int iWidth = Convert.ToInt32(dv[0]["Width"].ToString());

					if (dr.Table.Rows[nRow][i].ToString().Trim() != "")
					{
						//By Anto to wordwrap by not spliting the word
						//int iCol = dr.Table.Rows[nRow][i].ToString().Length / iWidth;
						//string[] strTemp = new string[iCol];
						//strTemp = splitString(dr.Table.Rows[nRow][i].ToString(), iWidth);
						string[] strTemp = getWordWrapString(dr.Table.Rows[nRow][i].ToString(), iWidth);
						objAL.Add(strTemp);
						iRowCount = (strTemp.GetUpperBound(0) > iRowCount) ? strTemp.GetUpperBound(0) : iRowCount;
					}
					else
					{
						string[] strTemp = new string[1];
						strTemp[0] = "";
						objAL.Add(strTemp);
					}
				}
				else
				{
					objAL.Add(dr.Table.Rows[nRow][i].ToString());
				}
				dv.RowFilter ="";
			}

			return iRowCount;
		}

		private string[] splitString(string strTemp, int iWidth)
		{
			int i = strTemp.Length;
			int j = 0;
			int iSize = (i % iWidth) == 0 ? (i / iWidth) : (i / iWidth) + 1;
			string[] str = new string[iSize];

			do
			{
				if (i > iWidth)
				{
					str[j] = strTemp.Substring(0, iWidth);
					strTemp = strTemp.Substring(iWidth);
				}
				else
				{
					//str[j] = strTemp.Substring(0, i);
					str[j] = strTemp +"";
					strTemp = "";
				}
				i = strTemp.Length;
				j++;
			}while(i>0);

			return str;
		}
		
		private string[] getWordWrapString(string strTemp, int iWidth)
		{
			StringCollection objSC	= new StringCollection();
			string strFrontPortion	= "";
			string strEndPortion	= "";

			int iLength = strTemp.Length;

			do
			{
				if (iLength > iWidth)
				{
					string sTemp = splitLine(strTemp, iWidth);

					if(sTemp.IndexOf("\r\n") < 0)
						objSC.Add(sTemp);
					else
					{
						strFrontPortion = sTemp.Substring(0, sTemp.IndexOf("\r\n"));
						strEndPortion	= sTemp.Substring(sTemp.IndexOf("\r\n") + "\r\n".Length);

						if (strFrontPortion.Length > 0)	
							objSC.Add(strFrontPortion);
						else
							objSC.Add("");
					}
					
					if (strEndPortion == "\r\n")
					{
						string str = strTemp.Remove(0,sTemp.Length);

						while(str.Substring(0, 1) == " ")
							str = str.Substring(1);

						strTemp = strEndPortion + str;
					}
					else if (sTemp != "")
						strTemp = strEndPortion + strTemp.Remove(0, sTemp.Length);
				}
				else
				{
					if(strTemp.IndexOf("\r\n") < 0)
						objSC.Add(strTemp);
					else
					{
						strFrontPortion = strTemp.Substring(0, strTemp.IndexOf("\r\n"));
						strEndPortion	= strTemp.Substring(strTemp.IndexOf("\r\n") + "\r\n".Length);

						if (strFrontPortion.Length > 0)	
							objSC.Add(strFrontPortion);
						else
							objSC.Add("");
					}
					strTemp = strEndPortion;
				}

				strFrontPortion = "";
				strEndPortion	= "";
				iLength = strTemp.Length;
			}while(iLength > 0);

			string[] strArray = new string[objSC.Count];
			objSC.CopyTo(strArray, 0);

			return strArray;
		}
		private string splitLine(string strLine, int iMaxLength)
		{
			if (strLine.Length <= iMaxLength)
				return strLine;
			else
			{
				string strTemp = strLine.Substring(0, iMaxLength);

				if (strTemp.EndsWith("\r")) strTemp += "\n";

				if ((strLine.Substring(iMaxLength - 1, 1) == " ") | (strLine.Substring(iMaxLength, 1) == " "))
					return strTemp;
				else
				{
					if (strTemp.IndexOf(" ") < 0)
						return strTemp;
					else
					{
						if(strTemp.Trim().LastIndexOf(" ") < 0)
							return strTemp.Trim();
						else
							return strTemp.Substring(0, strTemp.LastIndexOf(" "));
					}
				}
			}
		}

		public DataSet getPurchaseRegisterDataset(DataSet dsList)
		{
			DataTable dt = new DataTable("NEW");
			DataSet dsPurchase = new DataSet();
			dt= dsList.Tables[0].Copy();
			string sItem ="";
			bool bEmpty = false;

			for (int i=0; i<dt.Rows.Count; i++)
			{
				bEmpty = false;
				if (sItem==dt.Rows[i]["VENDOR"].ToString())
				{
					dt.Rows[i]["VENDOR"] = "";
					bEmpty = true;
				}
				if (!bEmpty) sItem = dt.Rows[i]["VENDOR"].ToString();
			}
			dsPurchase.Tables.Add(dt);
			return dsPurchase;
		}

		// ------------------------------------------------- xxxxx -------------------------------------------

		public bool PrintBilling()
		{
			string str ="";
			//StreamReader Sr= File.OpenText(@"c:\OPD.txt");

			string strTemp = "";
			string colName = "";
			string strpath = "";
			bool bPrint = false;

			string strTemplate = "";		//Read the template file and assign to this

			StreamReader Sr;

			DataSet dsPath = new DataSet();
			DataView dv = new DataView();

			if (!File.Exists(Application.StartupPath + "\\" + ReportModule + ".dat"))
			{
				MessageBox.Show("Printer and Template are not selected for this report","Payroll",
					MessageBoxButtons.OK,MessageBoxIcon.Information);
				goto EndLine;
			}

			StreamReader TempPath = new StreamReader(Application.StartupPath + "\\" + ReportModule + ".dat");	
			string strWhole = TempPath.ReadToEnd();
			string [] strTPWhole = strWhole.Split('\n');

			for (int i = 0; i<=strTPWhole.GetUpperBound(0);i++)
			{
				string [] strLine = strTPWhole[i].Split('@');
				if (strLine[0]==BillType)
				{
					try
					{
						printername = strLine[1];
						if (_BillType == "ADM")	
							sAdmPrinterName = printername;

						if (PrintLabReport) 
							strpath = LabReportPath;
						else
							strpath = strLine[2];

						bPrint = Convert.ToBoolean(strLine[3]);
						//by Anto on 08-Apr-2006
						try
						{
							_Condensed = Convert.ToBoolean(strLine[4]);
							_ListItems = Convert.ToInt32(strLine[5]);
							_Avoid_Empty_Lines = Convert.ToBoolean(strLine[6]);
						}
						catch
						{
							_ListItems = (_ListItems == 0) ? 12 : _ListItems;
						}
						//--------------------------------------
						break;
					}
					catch
					{
						return false;
					}
				}
			}

			if (bPrint==false) goto EndLine;

			if (printername=="")
			{
				MessageBox.Show("Printer is not selected for the Report","Payroll",MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				goto EndLine;
			}

			if (strpath=="")
			{
				MessageBox.Show("Template path is not selected for the Report","Payroll",
					MessageBoxButtons.OK,MessageBoxIcon.Information);
				goto EndLine;
			}

			try
			{
				Sr= File.OpenText(strpath);
			}
			catch
			{
				MessageBox.Show("Template is not available","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return false;
			}
			int width =0;
			string align ="";

			DataSet ds1 = new DataSet();
			DataSet ds2 = new DataSet();
			DataSet ds3 = new DataSet();

			int Bills = 0;
			int nItems =0;

            //base.createDataSet("SELECT distinct * FROM PRINTBILLING WHERE BILLTYPE = '" + _BillType + "'",
            //                  "BILLTYPE");
            //dsTemplate = base.getDataSet();

            //if (_Sql1!="")
            //{
            //    base.createDataSet(_Sql1,"Table1");
            //    ds1 = base.getDataSet();
            //}
            //if (_Sql2!="")
            //{
            //    base.createDataSet(_Sql2,"Table2");
            //    ds2 = base.getDataSet();
            //    nList = ds2.Tables["Table2"].Rows.Count;
            //}
            //else
            //{
            //    nList = 0;
            //}
            //if (_Sql3!="")
            //{
            //    base.createDataSet(_Sql3,"Table3");
            //    ds3 = base.getDataSet();
            //}

			/*--------------------------------------------------------
			 * Name: JG
			 * Date: 08-Aug-2007
			 * */
			//if(!PrintRegister)
			if(PrintRegister == false & ds1.Tables.Count > 0)
			{
				nItems = ds1.Tables[0].Rows.Count;
				if (nItems ==0)
				{
					MessageBox.Show("No Records Found","Payroll");
					goto EndLine;
				}
			}
			//--------------------------------------------------------

			//Calculate the possible number of bills
			Bills = 1;
			bWordWrap = checkWordWrap(dsTemplate);
			bMargeColumn = checkMargeColumn(dsTemplate);

			if(bWordWrap | bMargeColumn)
			{
				dsAdvanced = getAdvancedDataSet(ds2,new DataView(dsTemplate));
				nList = dsAdvanced.Tables[0].Rows.Count;
			}

			/*
			if(bWordWrap)
			{
				dsWordWrap = getWordWrapDataSet(ds2,new DataView(dsTemplate.Tables[0]));
				nList = dsWordWrap.Tables[0].Rows.Count;
			}

			if(PrintRegister)
			{
				dsWordWrap = getPurchaseRegisterDataset(ds2);
				nList = dsWordWrap.Tables[0].Rows.Count;
			}
			*/

			try
			{
				if (nList<=_ListItems)
				{
					Bills =1;
				}
				else
				{
					Bills = nList/_ListItems;
					if (nList%_ListItems>0) 
						Bills++;
				}
			}
			catch
			{
				Bills = 1;
			}

			string [] strArr = new string[Bills];
			strTemplate = Sr.ReadToEnd();
			sPreviewText = new string[Bills];

			for (int i=0;i<Bills;i++)
			{
				str = strTemplate;
				dv = new DataView(dsTemplate);

				//Fillig the Header Items
				if (_Sql1=="")
					goto ReplaceFooter;

				for (int k=0;k<ds1.Tables["Table1"].Columns.Count;k++)
				{
					strTemp = ds1.Tables["Table1"].Rows[0][k].ToString();
					strTemp = replaceTags(strTemp);
					colName = ds1.Tables[0].Columns[k].ColumnName;
					dv.RowFilter = "FieldName = '" + colName + "'";
					if(dv.Count == 0) continue;
					width = (dv.Count==0) ? 10 : Convert.ToInt32(dv[0][2].ToString());
					align = (dv.Count==0) ? "L" : dv[0][3].ToString();
					strTemp = FormatString(strTemp,width,align);
					colName = "<" + colName + "-1>";
					if (width>colName.Length) 
						colName = colName.PadRight(width,' ');
					str =str.Replace(colName,strTemp);
					dv.RowFilter="";
				}

				ReplaceFooter:
				if (_Sql3=="")
					goto ReplaceList;
				if(ds3.Tables[0].Rows.Count == 0)
					goto ReplaceList;
				//Filling the Footer  Items
				for (int k=0;k<ds3.Tables[0].Columns.Count;k++)
				{
					strTemp = ds3.Tables[0].Rows[0][k].ToString();
					colName = ds3.Tables[0].Columns[k].ColumnName;
					dv.RowFilter = "FieldName = '" + colName + "'";
					if(dv.Count == 0) continue;
					width = (dv.Count==0) ? 10: Convert.ToInt32(dv[0][2].ToString());
					align = (dv.Count==0) ? "L" : dv[0][3].ToString();
					if (i<Bills-1)
					{
						//to print the totals only in the last page
						string sTags = "<NETPAYRS-3>,<TOTALRS-3>,<PAIDRS-3>,<CONCESRS-3>,<NETRS-3><TOTAL-3>,<BILLAMOUNT-3>,<DISCOUNT-3>,<TOTALAMOUNT-3>,<AMOUNTPAID-3>";
						if (sTags.IndexOf("<" + colName + "-3>")>0)
							strTemp = FormatString("-",width,align);
						else
							strTemp = FormatString(strTemp,width,align);
					}
					else
						strTemp = FormatString(strTemp,width,align);

					colName = "<" + colName + "-3>";
					if (width>colName.Length) 
						colName = colName.PadRight(width,' ');
					str =str.Replace(colName,strTemp);
					dv.RowFilter="";
				}

				ReplaceList:
					if (_Sql2=="")
						goto PrintLine;

				if(bWordWrap | bMargeColumn)
					goto ReplaceWordWrapList;

				int Sno =0;
				string strSerial = "";
				string sZero ="";
				int iTemp = 0;

				if (i!=0)
					Sno = ListItems * i;

				for (int j = Sno; j < ds2.Tables[0].Rows.Count; j++)
				{
					Sno++;
					iTemp++;

					if (i == 0)
					{
						sZero = (j<9)?"0":"";
						strSerial = "<SNO-2>" +  sZero + Sno.ToString();
					}
					else
					{
						sZero = (iTemp < 10)?"0":"";
						strSerial = "<SNO-2>" +  sZero + iTemp.ToString();
					}

					//string sRep = Sno.ToString().PadRight(strSerial.Length,' ');
					string sRep =string.Empty;
                    
                    //= Sno.ToString().PadRight((clsGeneral.getSnoLength() >= 3) ? clsGeneral.getSnoLength() : strSerial.Length,' ');
					str =str.Replace(strSerial, sRep);
					for (int k=0;k<ds2.Tables[0].Columns.Count;k++)
					{
						strTemp = ds2.Tables[0].Rows[j][k].ToString();
						colName = ds2.Tables[0].Columns[k].ColumnName;
						dv.RowFilter = "FieldName = '" + colName + "'";
						if(dv.Count == 0) continue;
						width = (dv.Count == 0) ? 10: Convert.ToInt32(dv[0][2].ToString());
						align = (dv.Count == 0) ? "L" : dv[0][3].ToString();
						strTemp = FormatString(strTemp,width,align);

						if (i == 0)
							colName = "<" + colName + "-2>" + sZero + Sno.ToString();
						else
							colName = "<" + colName + "-2>" + sZero + iTemp.ToString();

						if (width>colName.Length) 
							colName = colName.PadRight(width,' ');
						str =str.Replace(colName,strTemp);
						dv.RowFilter="";
					}
				}

				//Clearing the List
				string sReplace = (_Avoid_Empty_Lines) ? "<@DEL@>" : "";
				for (int j=0;j<=ListItems;j++)
				{
					string sRep ="";
					sZero = (j<10)?"0":"";

					for (int k=0;k<ds2.Tables[0].Columns.Count;k++)
					{
						sRep ="<SNO-2>" + sZero + j.ToString();
						str=str.Replace(sRep,sReplace.PadRight(sRep.Length,' '));

						colName = ds2.Tables[0].Columns[k].ColumnName;
						dv.RowFilter = "FieldName = '" + colName + "'";
						sRep ="<" + colName + "-2>" + sZero + j.ToString();
						str=str.Replace(sRep,sReplace.PadRight(sRep.Length,' '));
					}
				}
				if(_Avoid_Empty_Lines)
				{
					int iSPos = str.IndexOf(sReplace);
					if(iSPos > 0)
					{
						int iEPos  = str.LastIndexOf(sReplace);
						int iLen = iEPos - iSPos;
						str = str.Remove(iSPos,iLen + sReplace.Length);
					}
				}

			ReplaceWordWrapList:
				/*
				if(bWordWrap)
				{
					int SNo =0;
					int sSNo =0;
					int nLimit =0;

					if (i!=0)
						SNo = ListItems * i;

					if (SNo + ListItems<nList)
						nLimit = SNo+ListItems;
					else
						nLimit = nList;


					for (int j = SNo; j < nLimit; j++)
					{
						sSNo++;

						for (int k=0;k<dsWordWrap.Tables[0].Columns.Count;k++)
						{
							strTemp = dsWordWrap.Tables[0].Rows[j][k].ToString();
							colName = dsWordWrap.Tables[0].Columns[k].ColumnName;
							colName = colName.ToUpper();
							dv.RowFilter = "FieldName = '" + colName + "'";
							width = (dv.Count == 0) ? 10 : Convert.ToInt32(dv[0][2].ToString());
							align = (dv.Count == 0) ? "L" : dv[0][3].ToString();
							strTemp = FormatString(strTemp,width,align);

							if (sSNo <10)
								colName = "<" + colName + "-2>" + "0" +  sSNo.ToString();
							else
								colName = "<" + colName + "-2>" +   sSNo.ToString();

							if (width>colName.Length) 
								colName = colName.PadRight(width,' ');
							str =str.Replace(colName,strTemp);
							dv.RowFilter="";
						}
					}

					//Clearing the List
					for (int j=0;j<=ListItems;j++)
					{
						string sRep ="";
						sZero = (j<10)?"0":"";

						for (int k=0;k<dsWordWrap.Tables[0].Columns.Count;k++)
						{
							sRep ="<SNO-2>" + sZero + j.ToString();
							str=str.Replace(sRep,"".PadRight(sRep.Length,' '));

							colName = dsWordWrap.Tables[0].Columns[k].ColumnName;
							dv.RowFilter = "FieldName = '" + colName + "'";
							sRep ="<" + colName + "-2>" + sZero + j.ToString();
							str=str.Replace(sRep,"".PadRight(sRep.Length,' '));
						}
					}
				}*/

				if(bWordWrap|bMargeColumn)
				{
					int SNo =0;
					int sSNo =0;
					int nLimit =0;

					if (i!=0)
						SNo = ListItems * i;

					if (SNo + ListItems<nList)
						nLimit = SNo+ListItems;
					else
						nLimit = nList;


					for (int j = SNo; j < nLimit; j++)
					{
						sSNo++;

						for (int k=0;k<dsAdvanced.Tables[0].Columns.Count;k++)
						{
							strTemp = dsAdvanced.Tables[0].Rows[j][k].ToString();
							colName = dsAdvanced.Tables[0].Columns[k].ColumnName;
							colName = colName.ToUpper();
							dv.RowFilter = "FieldName = '" + colName + "'";
							if(dv.Count == 0) continue;
							width = (dv.Count == 0) ? 10 : Convert.ToInt32(dv[0][2].ToString());
							align = (dv.Count == 0) ? "L" : dv[0][3].ToString();
							strTemp = FormatString(strTemp,width,align);

							if (sSNo <10)
								colName = "<" + colName + "-2>" + "0" +  sSNo.ToString();
							else
								colName = "<" + colName + "-2>" +   sSNo.ToString();

							if (width>colName.Length) 
								colName = colName.PadRight(width,' ');
							str =str.Replace(colName,strTemp);
							dv.RowFilter="";
						}
					}

					//Clearing the List
					sReplace = (_Avoid_Empty_Lines) ? "<@DEL@>" : "";
					for (int j=0;j<=ListItems;j++)
					{
						string sRep ="";
						sZero = (j<10)?"0":"";

						for (int k=0;k<dsAdvanced.Tables[0].Columns.Count;k++)
						{
							sRep ="<SNO-2>" + sZero + j.ToString();
							str=str.Replace(sRep,sReplace.PadRight(sRep.Length,' '));

							colName = dsAdvanced.Tables[0].Columns[k].ColumnName;
							dv.RowFilter = "FieldName = '" + colName + "'";
							sRep ="<" + colName + "-2>" + sZero + j.ToString();
							str=str.Replace(sRep,sReplace.PadRight(sRep.Length,' '));
						}
					}
					if(_Avoid_Empty_Lines)
					{
						int iSPos = str.IndexOf(sReplace);
						if(iSPos > 0)
						{
							int iEPos  = str.LastIndexOf(sReplace);
							int iLen = iEPos - iSPos;
							str = str.Remove(iSPos,iLen + sReplace.Length);
						}
					}
				}

				PrintLine:
					//	nList = nList;
					//Console.WriteLine(str);

				if(!_Print_Lab_Report)
				{ 
					if (this.BillType == "LABGRID")
						MessageBox.Show("Please press enter key to print", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
					if(PrintFooterInAllPages==false)
					{
						int iStart = str.IndexOf("<FT>",0);
						int iEnd = str.IndexOf("</FT>",iStart);
						string strFooter = str.Substring(iStart, iEnd - iStart + 5);
						string sRep = "";
						string []sCount = strFooter.Split("\n".ToCharArray());
						for(int a=1;a<sCount.Length;a++)
							sRep += "\r\n";
						if(i<Bills-1)
						{
							str = str.Replace(strFooter, sRep);
							str = str.Replace("<CONTD-2>", "Contd --" + (i+2)  + "/");
						}
						str = str.Replace("<CONTD-2>", "");
						str = str.Replace("<FT>", "").Replace("</FT>","");
					}
					if(bPreview==true)
						sPreviewText[i] = replaceFontTagsWithSpace(str);
					else
					{
						if(bPrintOnMsgBox == true)
							MessageBox.Show(replaceFontTagsWithSpace(str));
						else
							PrintBilling(replaceFontTagsWithSpace(str));
					}
				}
				else
					PrintLaboratoryBill(str);

				Sr.Close();

				if (i==Bills-1) return true;
			}

			EndLine:
				Console.WriteLine("No records found");
				return false;
		}

		public void PrintBilling(string str)
		{
			if (_Condensed)
				str = (char) 15 + str + (char)18;
			else
				str = (char) 18 + str + (char)18;
			if (_Eject)
				str = str + (char) 12;
			str = replaceFontTags(str);
			if(bPrintOnMsgBox == true)
				MessageBox.Show(replaceFontTagsWithSpace(str));
			else
			{
				//======By Peter on 19-03-2009 To incorporate Laser Printing
				if(PrintLabReportInLPQMode && PrintLabReport) 
				{
					DocumentPrinting docPrint = new DocumentPrinting();
					docPrint.PrintStream(printername,str);
				}
				else
				{
					RawPrinterHelper.SendStringToPrinter(printername,str);
				}
			}
		}

		public void printRegisters(string strPrinterName, string str, bool bCondensed, bool bEject)
		{
			if (bCondensed)
				str = (char) 15 + str + (char)18;
			else
				str = (char) 18 + str + (char)18;
			if (bEject)
				str = str + (char) 12;

			RawPrinterHelper.SendStringToPrinter(strPrinterName, str);
		}

		private void PrintBillingNormal(string str)
		{
			if (_Condensed)
				str = (char) 15 + str + (char)18;
			else
				str = (char) 18 + str + (char)18;
			if (_Eject)
				str = str + (char) 12;
			str = replaceFontTags(str);
			RawPrinterHelper.SendStringToPrinter(printername,str);
		}

		private string replaceFontTagsWithSpace(string str)
		{
			if(str=="")
				return "";
			string sSubStr = "";
			int iPos = 0;
			str = str.Replace("<TIT>","     <C18><C14>");
			str = str.Replace("</TIT>","     <C18>");
			str = str.Replace("<CON>","     <C15>");
			str = str.Replace("</CON>","     <C18>");
			str = str.Replace("<NOR>","     <C18>");
			str = str.Replace("</NOR>","     <C15>");
			str = str.Replace("<NRB>","     <C15><C14>");
			str = str.Replace("</NRB>","     <C18>");

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

		private void PrintLaboratoryBill(string str)
		{
			DataView dv = new DataView(dsLab.Tables[0]);// Dataset of sql2
			string strTemp ="";
			string colName = "";
			int width =0;

			for(int i=0;i<dtLab.Rows.Count; i++)  //data table of field name and width
			{
				colName = dtLab.Rows[i]["Field"].ToString();
				width = Convert.ToInt32(dtLab.Rows[i]["Length"]);

				dv.RowFilter = "FieldName = '" + colName + "'";

				if (dv.Count>0)
				{
					strTemp = dv[0][1].ToString();
					strTemp = FormatString(strTemp,width,"L");

					colName ="<@" + colName + "@" + width.ToString() + "@>";

					if (width>colName.Length) 
						colName = colName.PadRight(width,' ');
					str =str.Replace(colName,strTemp);
				}
				dv.RowFilter="";
			}
			if(bPrintOnMsgBox == true)
				MessageBox.Show(replaceFontTagsWithSpace(str));
			else
				PrintBilling(str);
		}

		public void PrintEmptyLines(int iNoLines)
		{
			string sEmptyLines = "";

			for (int i = 0; i< iNoLines; i++)
				sEmptyLines += "\n";

			RawPrinterHelper.SendStringToPrinter(sAdmPrinterName,sEmptyLines);
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

		public string PrintFreePrinting()
		{
            //int width = 0, Bills = 0, nlist = 0;;
            //string align = "", strTemp = "", colName = "", strEmptyFooter = "";
            //bool bPrint = false;
            //DataView dv;

            //StreamReader TempPath = new StreamReader(Application.StartupPath + "\\" + ReportModule + ".dat");	

            //string strWhole = TempPath.ReadToEnd();
            //string [] strTPWhole = strWhole.Split('\n');

            //for (int i = 0; i<=strTPWhole.GetUpperBound(0);i++)
            //{
            //    string [] strLine = strTPWhole[i].Split('@');
            //    if (strLine[0]==BillType)
            //    {
            //        try
            //        {
            //            printername = strLine[1];
            //            bPrint = Convert.ToBoolean(strLine[3]);	
            //            try
            //            {
            //                _Condensed = Convert.ToBoolean(strLine[4]);
            //            }
            //            catch{}
            //            break;
            //        }
            //        catch
            //        {
            //            return "Printer is not available";
            //        }
            //    }
            //}
            //if(!bPrint) return "";

            //base.createDataSet("SELECT distinct * FROM PRINTBILLING WHERE BILLTYPE = '" + _BillType 
            //    + "'","BILLTYPE");
            //dsTemplate = base.getDataSet();

            //DataSet dsHeader = new DataSet();
            //DataSet dsFooter = new DataSet();

            //if (_Sql1!="")
            //{
            //    base.createDataSet(_Sql1,"Table1");
            //    dsHeader = base.getDataSet();
            //}

            //if (_Sql3!="")
            //{
            //    base.createDataSet(_Sql3,"Table3");
            //    dsFooter = base.getDataSet();
            //}

            //dv = new DataView(dsTemplate.Tables[0]);

            ////Fillig the Header Items
            //if (_Sql1 !="")
            //{
            //    for (int k=0;k<dsHeader.Tables["Table1"].Columns.Count;k++)
            //    {
            //        strTemp = dsHeader.Tables["Table1"].Rows[0][k].ToString();
            //        colName = dsHeader.Tables[0].Columns[k].ColumnName;
            //        dv.RowFilter = "FieldName = '" + colName + "'";
            //        if(dv.Count == 0) continue;
            //        width = (dv.Count==0) ? 10 : Convert.ToInt32(dv[0][2].ToString());
            //        align = (dv.Count==0) ? "L" : dv[0][3].ToString();
            //        strTemp = FormatString(strTemp,width,align);
            //        colName = "<" + colName + "-1>";
            //        if (width>colName.Length)
            //            colName = colName.PadRight(width,' ');
            //        strHeader = strHeader.Replace(colName,strTemp);
            //        dv.RowFilter="";
            //    }
            //}

            //strEmptyFooter = ReplaceWithSpace(strFooter);
            //if (_Sql3 !="" & dsFooter.Tables[0].Rows.Count > 0)
            //{
            //    //Filling the Footer  Items
            //    for (int k=0;k<dsFooter.Tables[0].Columns.Count;k++)
            //    {
            //        strTemp = dsFooter.Tables[0].Rows[0][k].ToString();
            //        colName = dsFooter.Tables[0].Columns[k].ColumnName;
            //        dv.RowFilter = "FieldName = '" + colName + "'";
            //        if(dv.Count == 0) continue;
            //        width = (dv.Count==0) ? 10: Convert.ToInt32(dv[0][2].ToString());
            //        align = (dv.Count==0) ? "L" : dv[0][3].ToString();
            //        colName = "<" + colName + "-3>";
            //        if (width>colName.Length) 
            //            colName = colName.PadRight(width,' ');
            //        strFooter = strFooter.Replace(colName,strTemp);
            //        dv.RowFilter="";
            //    }
            //}

            //Bills = 1;
            ////nlist = str.Length;
            //nlist = dtMainContent.Rows.Count;
            //if (nlist<=_ListItems)
            //{
            //    Bills =1;
            //}
            //else
            //{
            //    Bills = nlist/_ListItems;
            //    if (nlist%_ListItems>0) 
            //        Bills++;
            //}
            //int iPage = 0;
            //for (int i=0;i<Bills;i++)
            //{
            //    iPage +=1;
            //    string strBody = "";
            //    //int iLines = 0;
            //    for(int j = 0; j < _ListItems; j++)
            //    {
            //        int iIndex = (i*_ListItems)+j;
            //        if(iIndex < nlist)
            //        {
            //            //strBody = strBody + str[iIndex] + "\n";
            //            strBody = strBody + dtMainContent.Rows[iIndex][0].ToString() + "\n";
            //        }
            //        else
            //            strBody = strBody + "\n";
            //    }
            //    strDataToPrint = "";
            //    if(iPage == Bills)
            //        strDataToPrint = strHeader + "\n" + strBody + strFooter;
            //    else
            //        strDataToPrint = strHeader + "\n" + strBody + strEmptyFooter;
            //    //by Anto on 18-07-2007 to print the text which is in <COMPRESSED> tag.
            //    bool bFlag	= true;
            //    int iStart	= 0;
            //    int iEnd	= 0;
            //    do
            //    {
            //        iStart	= strDataToPrint.IndexOf("<COMPRESSED>",iStart);
            //        if(iStart < 0)
            //        {
            //            bFlag = false;
            //            break;
            //        }
            //        iEnd = strDataToPrint.IndexOf("</COMPRESSED>",iStart);
            //        string strCom = strDataToPrint.Substring(iStart + 12, iEnd - iStart - 12);
            //        string sBefore = strDataToPrint.Substring(0, iStart);
            //        string sAfter = strDataToPrint.Substring(iEnd + 13);
            //        strCom = (char) 15 + strCom + (char)18;
            //        strDataToPrint = sBefore + strCom + sAfter;

            //        iStart = iEnd;
            //        if(iStart >= strDataToPrint.Length)
            //            bFlag = false;
            //    }
            //    while(bFlag);
            //    //--------------------------------------------------------------------
            //    Console.Write(strDataToPrint);
            //    if(bPrintOnMsgBox == true)
            //        MessageBox.Show(strDataToPrint);
            //    else
            //        PrintBilling(strDataToPrint);
            //}
			return "";
		}
		public string PrintLabTestResult()
		{
            //int width = 0, Bills = 0;
            //string align = "", strTemp = "", colName = "", strEmptyFooter = "";
            //bool bPrint = false;
            //DataView dv;

            //StreamReader TempPath = new StreamReader(Application.StartupPath + "\\" + ReportModule + ".dat");	

            //string strWhole = TempPath.ReadToEnd();
            //string [] strTPWhole = strWhole.Split('\n');

            //for (int i = 0; i<=strTPWhole.GetUpperBound(0);i++)
            //{
            //    string [] strLine = strTPWhole[i].Split('@');
            //    if (strLine[0]==BillType)
            //    {
            //        try
            //        {
            //            printername = strLine[1];
            //            bPrint = Convert.ToBoolean(strLine[3]);	
            //            try
            //            {
            //                _Condensed = Convert.ToBoolean(strLine[4]);
            //            }
            //            catch{}
            //            break;
            //        }
            //        catch
            //        {
            //            return "Printer is not available";
            //        }
            //    }
            //}
            //if(!bPrint) return "";

            //if(clsGeneral.CHOOSE_PRINTER == true)
            //    printername = clsGeneral.PRINTER_NAME != "" ? clsGeneral.PRINTER_NAME : printername;

            //base.createDataSet("SELECT distinct * FROM PRINTBILLING WHERE BILLTYPE = '" + _BillType 
            //    + "'","BILLTYPE");
            //dsTemplate = base.getDataSet();

            //DataSet dsHeader = new DataSet();
            //DataSet dsFooter = new DataSet();

            //if (_Sql1!="")
            //{
            //    base.createDataSet(_Sql1,"Table1");
            //    dsHeader = base.getDataSet();
            //}

            //if (_Sql3!="")
            //{
            //    base.createDataSet(_Sql3,"Table3");
            //    dsFooter = base.getDataSet();
            //}

            //dv = new DataView(dsTemplate.Tables[0]);

            ////Fillig the Header Items
            //if (_Sql1 !="")
            //{
            //    for (int k=0;k<dsHeader.Tables["Table1"].Columns.Count;k++)
            //    {
            //        strTemp = dsHeader.Tables["Table1"].Rows[0][k].ToString();
            //        colName = dsHeader.Tables[0].Columns[k].ColumnName;
            //        dv.RowFilter = "FieldName = '" + colName + "'";
            //        if(dv.Count == 0) continue;
            //        width = (dv.Count==0) ? 10 : Convert.ToInt32(dv[0][2].ToString());
            //        align = (dv.Count==0) ? "L" : dv[0][3].ToString();
            //        strTemp = FormatString(strTemp,width,align);
            //        colName = "<" + colName + "-1>";
            //        if (width>colName.Length)
            //            colName = colName.PadRight(width,' ');
            //        strHeader = strHeader.Replace(colName,strTemp);
            //        dv.RowFilter="";
            //    }
            //}

            //strEmptyFooter = ReplaceWithSpace(strFooter);
            //if (_Sql3 !="" & dsFooter.Tables[0].Rows.Count > 0)
            //{
            //    //Filling the Footer  Items
            //    for (int k=0;k<dsFooter.Tables[0].Columns.Count;k++)
            //    {
            //        strTemp = dsFooter.Tables[0].Rows[0][k].ToString();
            //        colName = dsFooter.Tables[0].Columns[k].ColumnName;
            //        dv.RowFilter = "FieldName = '" + colName + "'";
            //        if(dv.Count == 0) continue;
            //        width = (dv.Count==0) ? 10: Convert.ToInt32(dv[0][2].ToString());
            //        align = (dv.Count==0) ? "L" : dv[0][3].ToString();
            //        colName = "<" + colName + "-3>";
            //        if (width>colName.Length) 
            //            colName = colName.PadRight(width,' ');
            //        strFooter = strFooter.Replace(colName,strTemp);
            //        dv.RowFilter="";
            //    }
            //}

            //Bills = 1;
            //int iHeadSize = getNoOfLines(strHeader);
            //int iFootSize = getNoOfLines(strFooter);
            //DataTable dtPageSetting = getPageSettings(dtMainContent.Rows.Count,iHeadSize,iFootSize);
            //Bills = dtPageSetting.Rows.Count;
            //int iPage = 0;
            //int iPrintedLines = 0;
            //for (int i=0;i<Bills;i++)
            //{
            //    iPage +=1;
            //    string strBody = "";
            //    int iMainConSize = int.Parse(dtPageSetting.Rows[i]["Main Content Size"].ToString());
            //    for(int j = 0; j < iMainConSize; j++)
            //    {
            //        int iIndex = iPrintedLines + j;
            //        if(iIndex < dtMainContent.Rows.Count)
            //        {
            //            strBody = strBody + dtMainContent.Rows[iIndex][0].ToString() + "\n";
            //        }
            //        else
            //            strBody = strBody + "\n";
            //    }
            //    iPrintedLines += iMainConSize;
            //    strDataToPrint = "";
            //    strDataToPrint = strBody;
            //    if(iPage == 1 | (iPage != 1 & _Print_Header_All_Pages))
            //        strDataToPrint = strHeader + "\n" + strDataToPrint;

            //    if(iPage == Bills | (iPage != Bills & _Print_Footer_All_Pages))
            //        strDataToPrint = strDataToPrint + strFooter;

            //    //To print the text which is in <COMPRESSED> tag in Compress Mode.
            //    bool bFlag	= true;
            //    int iStart	= 0;
            //    int iEnd	= 0;
            //    do
            //    {
            //        iStart	= strDataToPrint.IndexOf("<COMPRESSED>",iStart);
            //        if(iStart < 0)
            //        {
            //            bFlag = false;
            //            break;
            //        }
            //        iEnd = strDataToPrint.IndexOf("</COMPRESSED>",iStart);
            //        string strCom = strDataToPrint.Substring(iStart + 12, iEnd - iStart - 12);
            //        string sBefore = strDataToPrint.Substring(0, iStart);
            //        string sAfter = strDataToPrint.Substring(iEnd + 13);
            //        strCom = (char) 15 + strCom + (char)18;
            //        strDataToPrint = sBefore + strCom + sAfter;

            //        iStart = iEnd;
            //        if(iStart >= strDataToPrint.Length)
            //            bFlag = false;
            //    }
            //    while(bFlag);
            //    //--------------------------------------------------------------------
            //    //Check setting and print accordingly
            //    if(PrintResultInNormal())
            //    {
            //        return printUsingWord(strDataToPrint);
            //    }
            //    else
            //    {
            //        Console.Write(strDataToPrint);
            //        if(bPrintOnMsgBox == true)
            //            MessageBox.Show(strDataToPrint);
            //        else
            //            PrintBilling(strDataToPrint);
            //    }
            //    //PrintNormal();
            //}
			return "";
		}

		private bool PrintNormal()
		{
			//PrintDocument pc = new PrintDocument();
			pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
			pdoc.PrinterSettings.PrinterName = printername;
			pdoc.Print();
			return true;
		}
		private void pdoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			// Initialize the font to be used for printing.

			Font font = new Font("Microsoft Sans Serif", 11);

			int intPrintAreaHeight;
			int intPrintAreaWidth;
			int marginLeft;
			int marginTop;

			// Initialize local variables that contain the bounds of the printing 
			// area rectangle.
			intPrintAreaHeight = pdoc.DefaultPageSettings.PaperSize.Height - pdoc.DefaultPageSettings.Margins.Top - pdoc.DefaultPageSettings.Margins.Bottom;
			intPrintAreaWidth = pdoc.DefaultPageSettings.PaperSize.Width - pdoc.DefaultPageSettings.Margins.Left - pdoc.DefaultPageSettings.Margins.Right;
			// Initialize local variables to hold margin values that will serve
			// the X and Y coordinates for the upper left corner of the printing 
			// area rectangle.
			marginLeft = pdoc.DefaultPageSettings.Margins.Left; // X coordinate
			marginTop = pdoc.DefaultPageSettings.Margins.Top; // Y coordinate

			// if the user selected Landscape mode, swap the printing area height 
			// and width.

			if (pdoc.DefaultPageSettings.Landscape) 
			{
				int intTemp = intPrintAreaHeight;
				intPrintAreaHeight = intPrintAreaWidth;
				intPrintAreaWidth = intTemp;
			}
			// Calculate the total number of lines in the document based on the height of
			// the printing area and the height of the font.

			int intLineCount= (int)(intPrintAreaHeight / font.Height);

			// Initialize the rectangle structure that defines the printing area.

			RectangleF rectPrintingArea = new RectangleF(marginLeft, marginTop, intPrintAreaWidth, intPrintAreaHeight);

			// Instantiate the stringFormat class, which encapsulates text layout 
			// information (such alignment and line spacing), display manipulations 
			// (such ellipsis insertion and national digit substitution) and OpenType 
			// features. Use of stringFormat causes Measurestring and Drawstring to use
			// only an integer number of lines when printing each page, ignoring partial
			// lines that would otherwise likely be printed if the number of lines per 
			// page do not divide up cleanly for each page (which is usually the case).
			// See further discussion in the SDK documentation about stringFormatFlags.

			StringFormat fmt = new StringFormat(StringFormatFlags.LineLimit);

			// Call Measurestring to determine the number of characters that will fit in
			// the printing area rectangle. The CharFitted Int32 is passed ref and used
			// later when calculating intCurrentChar and thus HasMorePages. LinesFilled 
			// is ! needed for this sample but must be passed when passing CharsFitted.
			// Mid is used to pass the segment of remaining text left off from the 
			// previous page of printing (recall that intCurrentChar was declared 
			// static.

			int intLinesFilled;
			int intCharsFitted;

			e.Graphics.MeasureString(strDataToPrint, font,new SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,out intCharsFitted, out intLinesFilled);

			// Print the text to the page.
			e.Graphics.DrawString(strDataToPrint, font,Brushes.Black, rectPrintingArea, fmt);
		}
		private bool PrintResultInNormal()
		{
            //try
            //{
            //    //string sSql	= "SELECT NVL(PRINT_RESULT_IN_NORMAL,0) FROM LAB_CONFIG";
            //    //return int.Parse(ExecuteScalar(sSql)) == 1;
            //}
            //catch
            //{
                return false;
            //}
		}
		private string printUsingWord(string sData)
		{
			DataTable dtFormat = new DataTable("Format");
			int iStart = 0;
			int iEnd = 0;

			bool bFormatExists = true;
			string sTempData = "";
			Word.Application Word_App = new Word.Application();
			Word.Document Word_doc = new Word.Document();
			object missing = Missing.Value;
			Word.WdUnderline uLine = new Word.WdUnderline();

			DataRow drNew = null;
			DataColumn dc = new DataColumn("#",typeof(int));
			dc.AutoIncrement = true;
			dtFormat.Columns.Add(dc);
			dtFormat.Columns.Add("StartPos",typeof(long));
			dtFormat.Columns.Add("EndPos",typeof(long));
			dtFormat.Columns.Add("Length",typeof(int));
			dtFormat.Columns.Add("Format",typeof(string));
			//Build format Table

			// Bold
			sTempData = sData.Replace("\r\n"," ").Replace("<U>","").Replace("</U>","").Replace("<I>","").Replace("</I>","");
			do
			{
				iStart = sTempData.IndexOf("<B>",0);
				if(iStart >= 0)
				{
					drNew = dtFormat.NewRow();
					drNew["Format"] = "B";
					drNew["StartPos"] = iStart;
					sTempData = sTempData.Remove(iStart,3);
					iEnd = sTempData.IndexOf("</B>",iStart);
					drNew["EndPos"] = iEnd;
					drNew["Length"] = iEnd - iStart;
					sTempData = sTempData.Remove(iEnd,4);
					dtFormat.Rows.Add(drNew);
				}
				else
					bFormatExists = false;
			}while(bFormatExists);

			// Italic
			sTempData = sData.Replace("\r\n"," ").Replace("<U>","").Replace("</U>","").Replace("<B>","").Replace("</B>","");
			bFormatExists = true;
			do{
				iStart = sTempData.IndexOf("<I>",0);
				if(iStart >= 0)
				{
					drNew = dtFormat.NewRow();
					drNew["Format"] = "I";
					drNew["StartPos"] = iStart;
					sTempData = sTempData.Remove(iStart,3);
					iEnd = sTempData.IndexOf("</I>",iStart);
					drNew["EndPos"] = iEnd;
					drNew["Length"] = iEnd - iStart;
					sTempData = sTempData.Remove(iEnd,4);
					dtFormat.Rows.Add(drNew);
				}
				else
					bFormatExists = false;
			}while(bFormatExists);

			// Underline
			sTempData = sData.Replace("\r\n"," ").Replace("<B>","").Replace("</B>","").Replace("<I>","").Replace("</I>","");
			bFormatExists = true;
			do{
				iStart = sTempData.IndexOf("<U>",0);
				if(iStart >= 0)
				{
					drNew = dtFormat.NewRow();
					drNew["Format"] = "U";
					drNew["StartPos"] = iStart;
					sTempData = sTempData.Remove(iStart,3);
					iEnd = sTempData.IndexOf("</U>",iStart);
					drNew["EndPos"] = iEnd;
					drNew["Length"] = iEnd - iStart;
					sTempData = sTempData.Remove(iEnd,4);
					dtFormat.Rows.Add(drNew);
				}
				else
					bFormatExists = false;
			}while(bFormatExists);
			sData = sData.Replace("<B>","").Replace("</B>","").Replace("<I>","").Replace("</I>","").Replace("<U>","").Replace("</U>","");
			//Export to Word
			Word.Documents Docs = Word_App.Documents;
			//Word._Document my_Doc = (Word._Document) Word_doc;
			Word_doc=Docs.Add(ref missing, ref missing, ref missing, ref missing);

			PaperSize pkCustom = new PaperSize("First custom size",1000,1100);
			Word.Range range = Word_App.ActiveWindow.Selection.Range;
			Word_App.ActiveWindow.Selection.TypeText(sData);
			//Format the text
			//Word_App.Visible = true;
			foreach(DataRow dr in dtFormat.Rows)
			{
				Word_App.ActiveWindow.Selection.HomeKey(ref missing, ref missing);
				iStart = int.Parse(dr["StartPos"].ToString());
				iEnd = int.Parse(dr["EndPos"].ToString());
				Word_App.ActiveWindow.Selection.SetRange(iStart,iEnd);
				if(dr["Format"].ToString() == "B")
					Word_App.ActiveWindow.Selection.Range.Font.Bold = 1;
				else if(dr["Format"].ToString() == "I")
					Word_App.ActiveWindow.Selection.Range.Font.Italic = 1;
				else if(dr["Format"].ToString() == "U")
					Word_App.ActiveWindow.Selection.Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
			}
			Word_App.ActiveWindow.Selection.HomeKey(ref missing, ref missing);
			object NoOfCopies = 1;
			object PrintDoc =  Word.WdPrintOutRange.wdPrintAllDocument;
			object PrintDocContent = Word.WdPrintOutItem.wdPrintDocumentContent;
			object PageRange = Word.WdPrintOutPages.wdPrintAllPages;
			Word_App.ActivePrinter = printername;
			Word_App.ActiveWindow.PrintOut(ref missing, ref missing, ref PrintDoc, ref missing, ref missing , ref missing, ref PrintDocContent, ref NoOfCopies, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
			object SaveChanges = false;
			Word_App.ActiveWindow.Close(ref SaveChanges,ref missing);
			//Word_App.Application.Quit(ref SaveChanges, ref missing, ref missing);
			return "";
		}

		//--Anto To construct the page settings
		public DataTable getPageSettings(int iNoOfLines, int iHeaderSize, int iFooterSize)
		{
			bool bFlag = true;
			bool bFooterAtBottom = clsGeneral.PrintFooterAtBottom();
			DataTable dtPage = new DataTable("Page Settings");
			dtPage.Columns.Add("Page",typeof(int));
			dtPage.Columns.Add("Has Header",typeof(int));
			dtPage.Columns.Add("Has Footer",typeof(int));
			dtPage.Columns.Add("Main Content Size",typeof(int));

			dtPage.Columns["Has Header"].DefaultValue = 0;
			dtPage.Columns["Has Footer"].DefaultValue = 0;

			int iMainConSize = _ListItems;

			bool bHeaderInAllPages = _Print_Header_All_Pages;
			bool bFooderInAllPages = _Print_Footer_All_Pages;

			int iPage = 1;
			int iAddedContent = 0;
			do
			{
				int iSize = iMainConSize;
				DataRow dr = dtPage.NewRow();
				dr["Page"]	= iPage;
				if(iPage == 1 | (iPage != 1 & bHeaderInAllPages))
					dr["Has Header"] = 1;
				if(bFooderInAllPages)
					dr["Has Footer"] = 1;
				if(iNoOfLines <= iMainConSize)
				{
					//dr["Main Content Size"] = iNoOfLines;
					dr["Main Content Size"] = bFooterAtBottom ? iMainConSize : iNoOfLines;
					dr["Has Footer"] = 1;
					bFlag = false;
				}
				else
				{
					if(iPage != 1 & bHeaderInAllPages == false)
						iSize += iHeaderSize;
					if(iAddedContent + iSize < iNoOfLines)
					{
						if(bFooderInAllPages == false)
							iSize += iFooterSize;
						if(iAddedContent + iSize > iNoOfLines)
							iSize = iNoOfLines - iAddedContent;
					}
					else
					{
						//iSize = iNoOfLines - iAddedContent;
						if(bFooterAtBottom)
							iSize = iMainConSize;
						else
							iSize = iNoOfLines - iAddedContent;
						dr["Has Footer"] = 1;
						bFlag = false;
					}
					dr["Main Content Size"] = iSize;
					iAddedContent += iSize;
				}
				dtPage.Rows.Add(dr);
				iPage += 1;
			}
			while(bFlag);

			return dtPage;
		}
		private int getNoOfLines(string sText)
		{
			Console.Write(sText);
			string[] sTemp = sText.Split('\n');
			return sTemp.Length;
		}
		//-------------------------------

		private string ReplaceWithSpace(string str)
		{
			bool bFlag = true;
			int iStart = 0;
			int iEnd = 0;

			do
			{
				iStart = str.IndexOf("<",iStart);
				if(iStart < 0)
				{
					bFlag = true;
					break;
				}
				iEnd = str.IndexOf(">",iStart);
				string strTag = str.Substring(iStart, iEnd - iStart + 1);
				string strTemp = " ";
				strTemp = FormatString(strTemp,strTag.Length,"L");
				str = str.Replace(strTag,strTemp);
				iStart = iEnd;
			}
			while(bFlag);
			return str;
		}

		public string replaceTags(string str)
		{
			if (str.IndexOf("\t",0) < 0) return str;

			if (str.IndexOf("\n",0) < 0)
				str = replaceTabs(str);
			else
			{
				string[] sTempArr = str.Split('\n');
				str = "";

				for (int i=0; i<sTempArr.Length; i++)
					str += replaceTabs(sTempArr[i]) + "\n";

				if (str.EndsWith("\n"))
					str = str.Substring(0, str.Length - 1);
			}

			return str;
		}

		private string replaceTabs(string str)
		{
			int iCursorPoint		= 0;

			while (iCursorPoint < str.Length)
			{
				int iPosition	= 0;
				int iNoofSpaces	= 0;
				string sTemp	= "";

				iPosition = str.IndexOf("\t",iCursorPoint);

				if (iPosition >= 0)
				{
					iNoofSpaces = 5 - (iPosition % 5);

					for (int i=1; i<=iNoofSpaces; i++)
						sTemp += " ";

					string sFirstPortion = str.Substring(0, iPosition);
					string sLastPortion = str.Substring(iPosition + 1, str.Length - (iPosition + 1));

					str = sFirstPortion + sTemp + sLastPortion;

					iCursorPoint = iPosition + iNoofSpaces;
				}
				else
					iCursorPoint = str.Length;
			}

			return str;
		}
	}


	public class RawPrinterHelper
	{
		// Structure and API declarions:
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]

			public class DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)] public string pDocName;
			[MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
			[MarshalAs(UnmanagedType.LPStr)] public string pDataType;
		}
		[DllImport("winspool.Drv", EntryPoint="OpenPrinterA", SetLastError=true, CharSet=CharSet.Ansi, 
			 ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, 
			out IntPtr hPrinter, long pd);

		[DllImport("winspool.Drv", EntryPoint="ClosePrinter", SetLastError=true, 
			 ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="StartDocPrinterA", SetLastError=true, 
			 CharSet=CharSet.Ansi, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool StartDocPrinter( IntPtr hPrinter, Int32 level,  
			[In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

		[DllImport("winspool.Drv", EntryPoint="EndDocPrinter", SetLastError=true, 
			 ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="StartPagePrinter", SetLastError=true,
			 ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="EndPagePrinter", SetLastError=true, 
			 ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="WritePrinter", SetLastError=true, 
			 ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, 
			out Int32 dwWritten );

		// SendBytesToPrinter()
		// When the function is given a printer name and an unmanaged array
		// of bytes, the function sends those bytes to the print queue.
		// Returns true on success, false on failure.
		public static bool SendBytesToPrinter( string szPrinterName, IntPtr pBytes, Int32 dwCount)
		{
			Int32    dwError = 0, dwWritten = 0;
			IntPtr    hPrinter = new IntPtr(0);
			DOCINFOA    di = new DOCINFOA();
			bool    bSuccess = false; // Assume failure unless you specifically succeed.

			di.pDocName = "My C#.NET RAW Document";
			di.pDataType = "RAW";

			// Open the printer.
			if( OpenPrinter( szPrinterName, out hPrinter, 0 ) )
			{
				// Start a document.
				if( StartDocPrinter(hPrinter, 1, di) )
				{
					// Start a page.
					if( StartPagePrinter(hPrinter) )
					{
						// Write your bytes.
						bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
						EndPagePrinter(hPrinter);
					}
					EndDocPrinter(hPrinter);
				}
				ClosePrinter(hPrinter);
			}
			// If you did not succeed, GetLastError may give more information
			// about why not.
			if( bSuccess == false )
			{
				dwError = Marshal.GetLastWin32Error();
			}

			return bSuccess;
		}

		public static bool SendStringToPrinter( string szPrinterName, string szString )
		{
			IntPtr pBytes;
			Int32 dwCount;
			// How many characters are in the string?
			dwCount = szString.Length;
			// Assume that the printer is expecting ANSI text, and then convert
			// the string to ANSI text.
			pBytes = Marshal.StringToCoTaskMemAnsi(szString);
			// Send the converted ANSI string to the printer.
			SendBytesToPrinter(szPrinterName, pBytes, dwCount);
			Marshal.FreeCoTaskMem(pBytes);
			return true;
		}

		public static bool SendFileToPrinter( string szPrinterName, string szFileName )
		{
			// Open the file.
			FileStream fs = new FileStream(szFileName, FileMode.Open);
			// Create a BinaryReader on the file.
			BinaryReader br = new BinaryReader(fs);
			// Dim an array of bytes big enough to hold the file's contents.
			Byte []bytes = new Byte[fs.Length];
			bool bSuccess = false;
			// Your unmanaged pointer.
			IntPtr pUnmanagedBytes = new IntPtr(0);
			int nLength;

			nLength = Convert.ToInt32(fs.Length);
			// Read the contents of the file into the array.
			bytes = br.ReadBytes( nLength );
			// Allocate some unmanaged memory for those bytes.
			pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
			// Copy the managed byte array into the unmanaged array.
			Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
			// Send the unmanaged bytes to the printer.
			bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
			// Free the unmanaged memory that you allocated earlier.
			Marshal.FreeCoTaskMem(pUnmanagedBytes);
			return bSuccess;
		}
	}
}
