using System;
using System.Data;
using System.Collections;
using Bosco.Utility.Common;
using Bosco.Utility.Validations;

namespace Bosco.Utility.Common
{
	/// <summary>
	/// author  : ntanand
	/// date	: 16-Nov-2005
	/// purpose	: To modify dataset that is send, to adapt features such as 
	///			  wordwrap and marge column.	
	/// </summary>
	public class clsCustomDataSet
	{
		/*
		 * contionDataView : holds the condtion patametes of each column available in sourceDataset
		 * This Contains following columns :
		 *		 "FIELDNAME"    -- Column name given in souce table
		 *		 "WIDTH"		-- Maximum with of the column in terms of characters
		 *		 "WORDWRAP"		-- Weather Column required wordwrap 1-- Yes; 0 -- No
		 *		 "MARGE"		-- Weather Column required Marge	1-- Yes; 0 -- No
		 *		 "REPEAT"		-- Weather want repeat same value to next row if wordwrap take place 1 -- Yes; 0 -- No
		 * */
		private DataView conditionDataView;
		private DataTable  sourceDataSet;
		private DataSet  finalDataSet;

		private ArrayList objArrayList;
		private bool bMargeColumn = false;
		private int	iRow = 0;
		private int nRow = 0;
		private string sGroupBy = "";
		private string sColumnTotal = "";

		public string ColumnTotal
		{
			set{this.sColumnTotal = value;}
			get{return this.sColumnTotal;}
		}

		public string GroupByColumn
		{
			set{this.sGroupBy = value;}
			get{return this.sGroupBy;}
		}

		public clsCustomDataSet()
		{
			
		}

		public DataTable SourceDataSet
		{
			get {return this.sourceDataSet;}
			set {this.sourceDataSet = value;}
		}

		public DataView ConditionDataView
		{
			get {return this.ConditionDataView;}
			set {this.conditionDataView = value;}
		}

		public DataSet FinalDataSet
		{
			get {return this.finalDataSet;}			
		}

		public DataSet enableSerialNo(DataTable sourceDataSet,String sSnoCaption)
		{
			int iColumnCount = 0;
			int iRowCount	 = 0;
			int iSno		 = 1;

			DataTable finalDT = new DataTable("Final DT");
			
			this.finalDataSet = new DataSet();
			this.sourceDataSet = sourceDataSet;
			iColumnCount = this.sourceDataSet.Columns.Count;
			iRowCount    = this.sourceDataSet.Rows.Count;	
			if (sSnoCaption == "")
				sSnoCaption = "#";

			//Creating Columns for the Final Table
			DataColumn objDC = new DataColumn(sSnoCaption);
			objDC.DataType = System.Type.GetType("System.Decimal");
			finalDT.Columns.Add(objDC);
			for (int j=0; j<iColumnCount; j++)
			{				
				objDC = new DataColumn(this.sourceDataSet.Columns[j].Caption);
				objDC.DataType =  this.sourceDataSet.Columns[j].DataType; //System.Type.GetType("System.String");
				finalDT.Columns.Add(objDC);
			}

			for (int j=0; j<iRowCount; j++)
			{
				DataRow objDR = finalDT.NewRow();
				finalDT.Rows.Add(objDR);
				finalDT.Rows[j][sSnoCaption] = iSno;
				iSno ++;
				for(int i=0;i<iColumnCount;i++)
				{
					finalDT.Rows[j][this.sourceDataSet.Columns[i].Caption] = this.sourceDataSet.Rows[j][i];
				}
			}
			this.finalDataSet.Tables.Add(finalDT);	
			return this.finalDataSet;
		}

		private bool isSameRecord(DataRow row, string [] sGroupByFields, string [] sGroupByValues)
		{
			bool bReturn = true;
			for(int i=0;i<sGroupByFields.Length;i++)
			{
				if(row[sGroupByFields[i]].ToString()!=sGroupByValues[i])
				{
					bReturn = false;
					break;
				}
			}
			return bReturn;
		}

		public DataSet getFinalDataSet(DataTable sourceDataSet, DataView conditionDataView)
		{
			int iColumnCount = 0;
			this.finalDataSet = new DataSet();

			this.conditionDataView = conditionDataView;
			this.sourceDataSet	= sourceDataSet;
			
			DataTable finalDT = new DataTable("Final DT");
			//following tables holed data form DS for ref.
			DataTable PrevDT = new DataTable("Previous DT");
			DataTable CurrDT = new DataTable("Current DT");

			iColumnCount = this.sourceDataSet.Columns.Count;
				
			for (int j=0; j<iColumnCount; j++)
			{
				//Creating Columns for the Final Table
				DataColumn objDC = new DataColumn(this.sourceDataSet.Columns[j].Caption);
				objDC.DataType = System.Type.GetType("System.String");
				finalDT.Columns.Add(objDC);

				objDC = new DataColumn(this.sourceDataSet.Columns[j].Caption);
				objDC.DataType = System.Type.GetType("System.String");
				PrevDT.Columns.Add(objDC); 
				objDC = new DataColumn(this.sourceDataSet.Columns[j].Caption);
				objDC.DataType = System.Type.GetType("System.String");
				CurrDT.Columns.Add(objDC);
			}

			DataRow previousRow = PrevDT.NewRow();
			PrevDT.Rows.Add(previousRow);
			//initialize Previous PrevDT with Empty String
			DataRow currentRow = CurrDT.NewRow();
			CurrDT.Rows.Add(currentRow);

			bMargeColumn = isMargeRequired();

			string [] sFieldsName = sColumnTotal.Split('@');
			decimal [] iSubTotals = new decimal[sFieldsName.Length];
			for(int iSt=0; iSt<sFieldsName.Length;iSt++)
				iSubTotals[iSt] = 0;
			string [] sGroupByFields=null, sGroupByValues=null;
			if(sGroupBy != "")
			{
				string [] sGPfields = sGroupBy.Split('@');
				sGroupByFields = new string [sGPfields.Length];
				sGroupByValues = new string [sGPfields.Length];
				for(int iGP=0;iGP<sGroupByFields.Length & this.sourceDataSet.Rows.Count>0;iGP++)
				{
					sGroupByFields[iGP] = sGPfields[iGP];
					sGroupByValues[iGP] = this.sourceDataSet.Rows[0][sGroupByFields[iGP]].ToString();
				}
			}
			for (int i=0; i<this.sourceDataSet.Rows.Count; i++)
			{
				int l=0;
				System.Collections.IEnumerator myEnumerator= null;

				//Group By sub total operation here
				l=0;
				if(sGroupBy != "")
				{
					try
					{
						if(isSameRecord(this.sourceDataSet.Rows[i],sGroupByFields, sGroupByValues) == false)
						{
							//Creating Rows for the Final Table
							for (int j=1; j<=3; j++)
							{
								DataRow objDR = finalDT.NewRow();
								finalDT.Rows.Add(objDR);
							}

							getGroupByTotals(sFieldsName, iSubTotals);
							l=0;
							myEnumerator = objArrayList.GetEnumerator();
							while (myEnumerator.MoveNext())
							{
								string[] strT  = new string[3];
								int iTemp = iRow;

								strT = (string[]) myEnumerator.Current;

								for(int m=0; m<=strT.GetUpperBound(0); m++)
								{
									finalDT.Rows[iRow][l] = strT[m];
									iRow++;
								}

								iRow = iTemp;
								l++;
							}//while end
							for(int iGP=0;iGP<sGroupByFields.Length;iGP++)
								sGroupByValues[iGP] = this.sourceDataSet.Rows[i][sGroupByFields[iGP]].ToString();
							
							for(int iSTot=0;iSTot<sFieldsName.Length;iSTot++)
								iSubTotals[iSTot] = 0;
							iRow += 3;
						}
						for(int iSTotal=0;iSTotal<sFieldsName.Length;iSTotal++)
						{
							decimal iVal = 0;
							string sVal = this.sourceDataSet.Rows[i][sFieldsName[iSTotal]].ToString();
							sVal = sVal.Trim();
							iVal = clsValidation.isValidateAmount(sVal) ? (sVal=="" ? 0 : Convert.ToDecimal(sVal)) : 0;
							iSubTotals[iSTotal] += iVal;
						}
					}
					catch(Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}//Group By sub total ends

				for (int iCurr =0 ; iCurr < iColumnCount; iCurr++)
					CurrDT.Rows[0][iCurr] = this.sourceDataSet.Rows[i][iCurr].ToString();

				if (bMargeColumn) 
					CurrDT = getOperationRow(PrevDT,CurrDT);

				int iRowCount = getWrapRowCount(CurrDT.Rows[0]);

				//Creating Rows for the Final Table
				for (int j=0; j<=iRowCount; j++)
				{
					DataRow objDR = finalDT.NewRow();
					finalDT.Rows.Add(objDR);
				}

				l=0;
				myEnumerator = objArrayList.GetEnumerator();
				while (myEnumerator.MoveNext())
				{
					string strTemp = "";
					string[] strT  = new string[iRowCount+1];
					int iTemp = iRow;

					this.conditionDataView.RowFilter = "FIELDNAME= '" + this.sourceDataSet.Columns[l].Caption.ToString().Replace("'","''") + "'";
					if (this.conditionDataView.Count > 0) 
					{

						if(this.conditionDataView[0]["WordWrap"].ToString() == "1")
						{
							strT = (string[]) myEnumerator.Current;

							for(int m=0; m<=strT.GetUpperBound(0); m++)
							{
								finalDT.Rows[iRow][l] = strT[m];
								iRow++;
							}

							if(strT.GetUpperBound(0) < iRowCount)
							{
								for (int n=strT.GetUpperBound(0); n<iRowCount; n++)
								{
									finalDT.Rows[iRow][l] = " ";
									iRow++;
								}
							}
						}
						else
						{
							strTemp = myEnumerator.Current.ToString();

							finalDT.Rows[iRow][l] = strTemp;
							iRow++;

							if (this.conditionDataView[0]["repeat"].ToString() == "1")
							{
								for(int m=1; m<=iRowCount; m++)
								{
									finalDT.Rows[iRow][l] = strTemp;
									iRow++;
								}
							}
							else
							{
								for(int m=1; m<=iRowCount; m++)
								{
									finalDT.Rows[iRow][l] = " ";
									iRow++;
								}
							}
						}
					}
					else
					{
						strTemp = myEnumerator.Current.ToString();

						finalDT.Rows[iRow][l] = strTemp;
						iRow++;

						for(int m=1; m<=iRowCount; m++)
						{
							finalDT.Rows[iRow][l] = " ";
							iRow++;
						}
					}
					iRow = iTemp;
					l++;
					this.conditionDataView.RowFilter = "";
				}
							
				iRow = iRow + (iRowCount + 1);				

				//Fill PrevDT To Checkwith next row for marge operation
				for (int iPrev =0 ; iPrev < iColumnCount; iPrev++)
					PrevDT.Rows[0][iPrev] = this.sourceDataSet.Rows[i][iPrev].ToString();

				//Group By sub total operation here
				if(i+1 == this.sourceDataSet.Rows.Count)
				{
					l=0;
					if(sGroupBy != "")
					{
						try
						{
							for (int j=1; j<=3; j++)
							{
								DataRow objDR = finalDT.NewRow();
								finalDT.Rows.Add(objDR);
							}

							getGroupByTotals(sFieldsName, iSubTotals);
							l=0;
							myEnumerator = objArrayList.GetEnumerator();
							while (myEnumerator.MoveNext())
							{
								string[] strT  = new string[3];
								int iTemp = iRow;

								strT = (string[]) myEnumerator.Current;

								for(int m=0; m<=strT.GetUpperBound(0); m++)
								{
									finalDT.Rows[iRow][l] = strT[m];
									iRow++;
								}

								iRow = iTemp;
								l++;
							}//while end
							iRow += 3;
						}
						catch(Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
					}
				}//Group By sub total ends
			}
			this.finalDataSet.Tables.Add(finalDT);
			iRow = 0;
			return this.finalDataSet;
		}

		public DataSet getFinalDataSet()
		{
			return getFinalDataSet(this.sourceDataSet,this.conditionDataView);
		}

		private void getGroupByTotals(string [] sFieldsName,decimal [] iSubTotals)
		{
			objArrayList = new System.Collections.ArrayList();
			int iColCount = this.sourceDataSet.Columns.Count;

			for (int i=0; i<iColCount; i++)
			{
				bool bTotalColumn	= false;
				decimal iTotal			= 0;
				for(int j=0; j<sFieldsName.Length;j++) //To find whether this column has to be summed
				{
					if(sFieldsName[j] == this.sourceDataSet.Columns[i].Caption.ToString())
					{
						bTotalColumn	= true;
						iTotal			= iSubTotals[j];
						break;
					}
				}

				//To find the width of the column
				int iWidth = 0;
				this.conditionDataView.RowFilter = "FIELDNAME= '" + this.sourceDataSet.Columns[i].Caption.ToString().Replace("'","''") + "'";				
				if (this.conditionDataView.Count >0)
				{
					iWidth = Convert.ToInt32(this.conditionDataView[0]["Width"].ToString());
				}
				this.conditionDataView.RowFilter ="";

				string sLine = "---------------------------------------------------------------------------------";
				if(bTotalColumn)
				{
					string[] strTemp = new string[3];
					strTemp[0] = truncate(sLine,iWidth);
					strTemp[1] = iTotal.ToString();
					strTemp[2] = truncate(sLine,iWidth);
					objArrayList.Add(strTemp);
				}
				else
				{
					string[] strTemp = new string[3];
					strTemp[0] = truncate(sLine,iWidth);
					strTemp[1] = "";
					strTemp[2] = truncate(sLine,iWidth);
					objArrayList.Add(strTemp);
				}
			}//End for [each column in the source table]
		}

		private int getWrapRowCount(DataRow operatingDataRow)
		{
			objArrayList = new System.Collections.ArrayList();
			int iColCount = operatingDataRow.Table.Columns.Count;
			int iRowCount = 0;

			for (int i=0; i<iColCount; i++)
			{
				this.conditionDataView.RowFilter = "FIELDNAME= '" + operatingDataRow.Table.Columns[i].Caption.ToString().Replace("'","''") + "'";				
				if (this.conditionDataView.Count >0)
				{
					int iWidth = Convert.ToInt32(this.conditionDataView[0]["Width"].ToString());
					if(this.conditionDataView[0]["WordWrap"].ToString() == "1")
					{					
						if (operatingDataRow.Table.Rows[nRow][i].ToString().Trim() != "")
						{
							int iCol = operatingDataRow.Table.Rows[nRow][i].ToString().Length / iWidth;
							string[] strTemp = new string[iCol];
							strTemp = wordWrap(operatingDataRow.Table.Rows[nRow][i].ToString(), iWidth);
							objArrayList.Add(strTemp);
							iRowCount = (strTemp.GetUpperBound(0) > iRowCount) ? strTemp.GetUpperBound(0) : iRowCount;
						}
						else
						{
							string[] strTemp = new string[1];
							strTemp[0] = "";
							objArrayList.Add(strTemp);
						}
					}
					else
					{
						objArrayList.Add(truncate(operatingDataRow.Table.Rows[nRow][i].ToString(),iWidth));
					}
				}
				else
				{
					objArrayList.Add(operatingDataRow.Table.Rows[nRow][i].ToString());
				}

				this.conditionDataView.RowFilter ="";
			}

			return iRowCount;
		}

		private string truncate(string inputString, int maximumWidth)
		{
			string strTemp	= inputString;
			int iWidth		= maximumWidth;
			string strResult= "";

			strTemp = strTemp == null ? "" : strTemp;
			
			if (strTemp.Length > iWidth)
			{
				strResult = strTemp.Substring(0, iWidth);
			}
			else
			{
				strResult = strTemp;
			}

			return strResult;
		}

		private string[] wordWrap(string inputString, int maximumWidth)
		{
			string strTemp	= inputString;
			int	iWidth		= maximumWidth;

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

		private DataTable getOperationRow(DataTable previousDT,DataTable currentDT)
		{
			int iColCount = currentDT.Columns.Count;
			
			for (int i =0; i<iColCount; i++)
			{
				this.conditionDataView.RowFilter = "FIELDNAME= '" + currentDT.Columns[i].Caption.ToString().Replace("'","''") +"'";
			
				if (this.conditionDataView[0]["Merge"].ToString() =="1")
				{
					if (previousDT.Rows[0][i].ToString() == currentDT.Rows[0][i].ToString())
						currentDT.Rows[0][i]= "";
				}
				this.conditionDataView.RowFilter ="";
			}
			return currentDT;			
		}

		private bool isMargeRequired()
		{
			bool bResult;
			this.conditionDataView.RowFilter = "MERGE = 1";
			bResult = conditionDataView.Count > 0 ? true : false;			
			this.conditionDataView.RowFilter ="";
			return bResult;
		}
		
	}
}
