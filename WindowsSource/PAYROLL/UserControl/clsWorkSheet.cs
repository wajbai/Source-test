using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using Bosco.Utility.Common;
 
namespace PAYROLL.UserControl
{	
	public delegate void GridCellEnableEventHandler(int RowNumber, int ColumnNumber, ref bool isEnable);

	public class clsWorkSheet :PAYROLL.UserControl.clsGrid
	{
		private DataSet dsFind;
		private DataView dvFind;

		private DataTable dtGrid;
		private DataView dvGrid;

		private bool bHitDelete = false;
		private bool bBoundDataview = false;
		private bool bAllowAdd=true;
		private bool bAllowEdit=true;
		private bool bAllowDelete=true;

		//private bool bManualFind = false;
		public string strFilterText = "";

		private bool bIsQuantity = false;		// Name : Gerald J		Date : 08-Jul-2005 :: 10.45 AM
		public bool bIsBactchNumber = false;	// Name : Gerald J		Date : 24-Jul-2005 :: 05.30 PM
		private bool bIsTextChanged = false;	// Name : Anto A		Date : 21-Oct-2005

		public string sFieldName	= "";

		private bool myChange=false;

		private bool bShowDefaultDate = true;	//Anto	:: 13-Jul-2005	:: To avoid showing current date in order receive

		public bool bTextSelection = false;

		private string[,] TextBoxProperty;
		private string sAutoColumn="";

		//#########
		//Added By Amalraj on 13/10/2005 - Do not Remove
		public string TextBoxText = "";
		public TextBox CurrentTxtBox;
		//#########

		public delegate void ShowLookupHandler(int ColumnNumber);

		public event GridCellEnableEventHandler CellEanbleEvent;

		public event DataColumnChangeEventHandler ColumnChanging;
		public event DataColumnChangeEventHandler ColumnChanged;
		public event DataRowChangeEventHandler RowChanged;
		public event DataRowChangeEventHandler RowChanging;
		public event DataRowChangeEventHandler RowDeleted;
		public event DataRowChangeEventHandler RowDeleting;
		//public event KeyPressEventHandler TextKeyPress; //commented by PE to avoid warning msg

		public event DataGridCellButtonClickEventHandler HandleCellButtonClicked;

		public event CellBackColorHandler CellBackColor_Event;

		private bool bColorReadOnlyCols=true;
		private int AutoColNumber=0;

		public event ShowLookupHandler ShowAutoLookup;

		//For Data Combo
		private string[,] DBComboProperty;

		//For Combo
		private string[,] ComboProperty;

		//For Lookup
		private string[,] LookupProperty;
		
		//For Format
		private string[,] FormatProperty;

		//For Primay Column
		private string[] PrimaryColumns;
		public bool EnableAutoLookup=false;
		private int iCurrentRowNumber=1;
		
		public bool ReplaceNullwithZero=false;

		public clsWorkSheet()
		{			
		}

		//get Source manuplation table
		public DataTable getSourceTable
		{
			get{return dtGrid;}
		}

		public Font setGridFont
		{
			set{this.Font = value;	}
		}

		public bool ColorReadOnlyColumns
		{
			get{return bColorReadOnlyCols;}
			set {bColorReadOnlyCols=value;}
		}

		public string setAutoColumn
		{
			set{this.sAutoColumn=value;}
		}

		// This is for Data Combo
		public string[,] setDBComboProperty
		{
			set{this.DBComboProperty = value;}
		}

		// This is for Comobo
		public string[,] setComboProperty
		{
			set{this.ComboProperty = value;}
		}	

		// This is for Lookup
		public DataSet setLookupDataset
		{
			set 
			{
				if (dsFind ==null)
					dsFind = value;
				else
				{
					if (dsFind.Tables.Contains(value.Tables[0].TableName))
						dsFind.Tables.Remove(value.Tables[0].TableName);
					dsFind.Tables.Add(value.Tables[0].Copy());
				}
			}
		}

		public void AddTableInLookupDataSet(DataTable dTable, string tblName)
		{
			DataTable dt= new DataTable();
			dt=dTable;
			dt.TableName=tblName;
			if (dsFind.Tables.Contains(tblName))
				dsFind.Tables.Remove(tblName);
			dsFind.Tables.Add(dt.Copy());		
		}

		public string[,] setLookupProperty
		{
			set{this.LookupProperty = value;}
		}

		public string[,] setFormatProperty
		{
			set{this.FormatProperty = value;}
		}
		
		public bool AllowAdd
		{
			set{bAllowAdd=value;}
		}

		public bool AllowEdit
		{
			set{bAllowEdit=value;}
		}

		public bool AllowDelete
		{
			set{bAllowDelete=value;}
		}

		public string[] setPrimaryColumns
		{
			set{PrimaryColumns=value;}
		}

		//public bool ManualFind
		//{
		//	set{ bManualFind = value;}
		//}

		public bool setQuantity_Amount
		{
			set {bIsQuantity =  value;}
		}
		
		public bool ShowDefaultDate
		{
			set {bShowDefaultDate = value;}
		}

		public void CreateGridTextBox(string sSQL, string sSourceTable, string[,] TextBoxProperty)
		{
            //DataAccess.DataHandling objDh = new DataAccess.DataHandling();

            //objDh.createDataSet(sSQL, "List");
            DataSet ds = new DataSet();

            dvGrid = new DataView(ds.Tables["List"]);
            dvGrid.AllowNew = bAllowAdd;
            dvGrid.AllowEdit = bAllowEdit;
            dvGrid.AllowDelete = bAllowDelete;

            bBoundDataview = true;
            try
            {
                this.DataSource = dvGrid;
            }
            catch { }
            CreateGridTextBox(sSourceTable, TextBoxProperty);
		}

		public void CreateGridTextBox(string sSourceTable, string[,] TextBoxProperty)
		{
			int i = 0;
			int iComboIndex = 0;
			int iDBComboIndex = 0;
			string sDataType = "", sMapDataType = "";
			this.TableStyles.Clear();
			
			//dgGrid = dgStyle;
			bBoundDataview = (this.DataSource.GetType().Name=="DataView")?true:false;
			if (!bBoundDataview)
				dtGrid = ((DataSet)this.DataSource).Tables[sSourceTable];
			else
			{
				dvGrid = (DataView)this.DataSource;
				dvGrid.AllowNew=bAllowAdd;
				dvGrid.AllowEdit=bAllowEdit;
				dvGrid.AllowDelete=bAllowDelete;
				dtGrid = dvGrid.Table;
			}

			DataGridTableStyle tableStyle = new DataGridTableStyle();
			
			tableStyle.AlternatingBackColor = System.Drawing.Color.White;
			tableStyle.BackColor = System.Drawing.Color.White;
			this.BackgroundColor = System.Drawing.Color.LightGray;
			this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CaptionBackColor = System.Drawing.Color.White; //Color.Teal;
			this.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.CaptionForeColor = System.Drawing.Color.White;
			this.FlatMode = true;
			this.Font = new System.Drawing.Font("Tahoma", 10F);
			tableStyle.ForeColor = System.Drawing.Color.Black;
			tableStyle.GridLineColor = System.Drawing.Color.Silver;
            tableStyle.HeaderBackColor = System.Drawing.Color.Gainsboro; // Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(206)))), ((int)(((byte)(252)))));
			tableStyle.HeaderFont = new Font("Tahoma",10F,FontStyle.Bold,GraphicsUnit.Point);
			tableStyle.HeaderForeColor = System.Drawing.Color.Black;
			tableStyle.LinkColor = System.Drawing.Color.Purple;
			this.ParentRowsBackColor = System.Drawing.Color.Gray;
			this.ParentRowsForeColor = System.Drawing.Color.White;
            tableStyle.SelectionBackColor = System.Drawing.Color.Black; //Color.Maroon;
			tableStyle.SelectionForeColor = System.Drawing.Color.White;
			//---------------------------------------

			tableStyle.MappingName = dtGrid.TableName;
			this.TextBoxProperty = TextBoxProperty;

			dtGrid.ColumnChanging +=new DataColumnChangeEventHandler(dtGrid_ColumnChanging);
			dtGrid.ColumnChanged += new DataColumnChangeEventHandler(dtGrid_ColumnChanged);

			dtGrid.RowDeleting+= new DataRowChangeEventHandler( GridRow_BeforeDelete );
			dtGrid.RowDeleted+= new DataRowChangeEventHandler( GridRow_AfterDelete );

			dtGrid.RowChanging+= new DataRowChangeEventHandler(GridRow_BeforeChange);
			dtGrid.RowChanged+= new DataRowChangeEventHandler(GridRow_AfterChange);

			//-------- TextBoxProperty --------------------
			//0-COLUMNNAME			((NAME1, NAME2,...)
			//1-LOOKUPCOLUMN		(TRUE or FALSE) At Maximum of one column is possible
			//2-MANDATORYCOLUMN		(TRUE or FALSE)
			//3-VALIDATIONCOLUMN	(TRUE or FALSE)
			//4-READONLY			(TRUE or FALSE)
			//5-MAXLENGTH			(0, 20, 30, ...) Set Maxlength for only string column, for other datatypes set to 0
			//6-COLUMNWIDTH			(30, 40, 50, ...)
			//7-DATATYPE			(INT, BOOLEAN, , ...) 
			//8-DEFAULTVALUE		(VALUE1, VALUE2, ...) 
			//9-UNIQUE				(TRUE FALSE))
			//---------------------------------------------

			//-------- ComoboProperty --------------------
			//0-SQL					((SQl Statement)
			//1-DisplayMember		(Display column in the Grid)
			//2-ValueMember			(Column of the store value)
			//3-ValueColumnIndex	(stroe column index in Grid))
			//---------------------------------------------
			
			for (i = 0;i<dtGrid.Columns.Count;i++)
			{
				sDataType = dtGrid.Columns[i].DataType.ToString().Replace("System.","").ToUpper();
				sMapDataType = TextBoxProperty[i,7].ToString().ToUpper(); 
				
				//Set Allow Column
				if (TextBoxProperty[i,2] == "TRUE") 
					dtGrid.Columns[i].AllowDBNull = false; //mandatory column

				//Set Auto increment Columns
				if (dtGrid.Columns[i].ColumnName == sAutoColumn) 
				{
					iCurrentRowNumber= dtGrid.Rows.Count+1;
					dtGrid.Columns[i].DefaultValue = iCurrentRowNumber;
					AutoColNumber=i;
				}
				else
				{
					//By Anto on 3-Jan-2006 to show the date and time
					//dtGrid.Columns[i].DefaultValue = (sMapDataType == "DATE" && bShowDefaultDate)? clsGeneral.getServerDate():TextBoxProperty[i,8];
                    if (sMapDataType == "DATE" & bShowDefaultDate)
                        dtGrid.Columns[i].DefaultValue = System.DateTime.Now.ToShortDateString();
                    else if (sMapDataType == "DATETIME" & bShowDefaultDate)
                        dtGrid.Columns[i].DefaultValue = System.DateTime.Now;
                    else
                        dtGrid.Columns[i].DefaultValue = TextBoxProperty[i, 8];
					//---------------
				}

				//Set Unique Column
				if (TextBoxProperty[i,9]=="TRUE") 
					dtGrid.Columns[i].Unique=true;
				
				//Set MAX Length
				if (sDataType == "STRING")  
					dtGrid.Columns[i].MaxLength = int.Parse(TextBoxProperty[i,5]);
				
				if (sDataType == "BOOLEAN" | (sMapDataType == "BOOLEAN" & sDataType != sMapDataType))
				{
					DataGridBoolColumn colStyle = new DataGridBoolColumn();
					colStyle.HeaderText = dtGrid.Columns[i].ColumnName;
					colStyle.MappingName = dtGrid.Columns[i].ColumnName;
					colStyle.AllowNull = false;

					if (sDataType != sMapDataType)
					{
						colStyle.TrueValue = "1";
						colStyle.FalseValue = "0";
					}
				
					//---Array Property
					colStyle.ReadOnly = (TextBoxProperty[i,4] == "TRUE") ? true : false;
					colStyle.Width = int.Parse(TextBoxProperty[i,6]);
					//-----------------
					//colStyle.CellEanbleEvent +=new GridCellEnableEventHandler(dtGrid_CellEnable);
					colStyle.NullText="0";	//Null Text;
					tableStyle.GridColumnStyles.Add(colStyle);
					colStyle.Dispose(); 
				}
				else if (sMapDataType=="DBCOMBO")	//Bound Combo
				{
					DataGridComboColumn cboColumnStyle = new DataGridComboColumn();
					cboColumnStyle.MappingName = dtGrid.Columns[i].ColumnName;
					cboColumnStyle.HeaderText = dtGrid.Columns[i].ColumnName;
					
					cboColumnStyle.SQL = DBComboProperty[iDBComboIndex,0].ToString();
					cboColumnStyle.DisplayMember = DBComboProperty[iDBComboIndex,1].ToString();
					cboColumnStyle.ValueMember = DBComboProperty[iDBComboIndex,2].ToString();
					cboColumnStyle.ValueColumnIndex = Convert.ToInt32( DBComboProperty[iDBComboIndex,3].ToString());
					cboColumnStyle.Width = int.Parse(TextBoxProperty[i,6]);
					cboColumnStyle.FillCombo();
					tableStyle.GridColumnStyles.Add(cboColumnStyle);
					cboColumnStyle.Dispose();
					iDBComboIndex++;
				}
				else if (sMapDataType=="COMBO")		//UnBound Combo
				{
					DataGridComboColumn cboColumnStyle = new DataGridComboColumn(ComboProperty[iComboIndex,0]);
					cboColumnStyle.MappingName = dtGrid.Columns[i].ColumnName;
					cboColumnStyle.HeaderText = dtGrid.Columns[i].ColumnName;
					cboColumnStyle.Width = int.Parse(TextBoxProperty[i,6]);
					cboColumnStyle.ValueColumnIndex=Convert.ToInt32( ComboProperty[iComboIndex,1].ToString());
					tableStyle.GridColumnStyles.Add(cboColumnStyle);
					cboColumnStyle.Dispose();
					iComboIndex++;
				}
				else if (sMapDataType=="BUTTON")
				{
					DataGridButtonColumn textButtonColStyle =  new DataGridButtonColumn(i, this);
					textButtonColStyle.HeaderText = dtGrid.Columns[i].ColumnName;
					textButtonColStyle.MappingName = dtGrid.Columns[i].ColumnName;
					textButtonColStyle.Width = int.Parse(TextBoxProperty[i,6]);
					textButtonColStyle.NullText = "";
					textButtonColStyle.CellButtonClicked += new DataGridCellButtonClickEventHandler(CellButtonClick);
					tableStyle.GridColumnStyles.Add(textButtonColStyle);
					textButtonColStyle.Dispose();
				}
				else
				{
					DGColumn  colStyle;
					if (TextBoxProperty[i,4] == "TRUE" & bColorReadOnlyCols)
						colStyle = new DGColumn(Color.LightGray, i);
					else
						colStyle = new DGColumn(Color.White, i);

					//DataGridTextBoxColumn colStyle = new DataGridTextBoxColumn();
					colStyle.HeaderText = dtGrid.Columns[i].ColumnName;
					colStyle.MappingName = dtGrid.Columns[i].ColumnName;  
					colStyle.TextBox.Name = "txtGridBox" + i;
					colStyle.TextBox.Tag = sDataType; //Tag contains Data Type of each column
				
					//---Array Property
					colStyle.ReadOnly = (TextBoxProperty[i,4] == "TRUE") ? true : false;
					colStyle.TextBox.Enabled = (!colStyle.ReadOnly);
					colStyle.Width = int.Parse(TextBoxProperty[i,6]);
					
					//Added by Anto on 29-11-2005 for setting format for a column

					if(FormatProperty != null)
					{
						for(int k = 0; k<FormatProperty.GetLongLength(0); k++)
						{
							if(FormatProperty[k,0] == dtGrid.Columns[i].ColumnName)
							{
								colStyle.Format = FormatProperty[k,1];
							}
						}
					}

					//--------------------
					colStyle.TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridTextBox_KeyDown);
					colStyle.TextBox.TextChanged  += new System.EventHandler(this.GridTextBox_TextChanged);
					colStyle.TextBox.Enter +=new System.EventHandler(this.GridTextBox_Enter);
					colStyle.TextBox.KeyPress +=new System.Windows.Forms.KeyPressEventHandler(this.Grid_KeyPress);
					
					colStyle.CellBackColorEvent += new CellBackColorHandler(DataGridCellColor);				
					colStyle.CellEanbleEvent +=new GridCellEnableEventHandler(dtGrid_CellEnable);
					 
					//set Null Text --------------------------------------------------------------------------------
					colStyle.NullText="";	
					if (sMapDataType=="INT" & ReplaceNullwithZero) colStyle.NullText="0";
					if (sMapDataType=="DATE" & bShowDefaultDate) colStyle.NullText=DateTime.Today.Date.ToShortDateString();
					//----------------------------------------------------------------------------------------------

					tableStyle.GridColumnStyles.Add(colStyle);
					colStyle.Dispose();
				}
			}
			
			this.TableStyles.Add(tableStyle);
			
			//set Primary columns to the table
			setPrimaryKeys();
		}		

		private void DataGridCellColor(int Row, int Col, ref Brush ForeColor , ref Brush BackColor)
		{
			if (CellBackColor_Event!=null) CellBackColor_Event(Row, Col, ref ForeColor, ref  BackColor);
		}

		private string getFindText(string sFindValue)
		{
			string sVal = "";
			string sCurrentColName="";
			string sFindColumn="";
			string sFilter="";
			int iFindCol=0;
			
			sCurrentColName = dtGrid.Columns[this.CurrentCell.ColumnNumber].ToString();
			iFindCol = findLookupIndex();
			if (iFindCol == -1) return "";

			sFindColumn = LookupProperty[iFindCol,1];
			sFindValue = sFindValue.Replace("'","''");

			if (Convert.ToInt32(LookupProperty[iFindCol,2])>=0)
			{
				sFilter = " AND " +  LookupProperty[iFindCol,3] + "=";		
				switch(LookupProperty[iFindCol,4])
				{
					case "int":
						sFilter = sFilter + this[this.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString();
						break;
					default:
						sFilter = sFilter + "'" + this[this.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString() + "'";
						if (LookupProperty.GetUpperBound(1)==6)
						{
							if (LookupProperty[iFindCol,6]!="")
							{
								sFilter += " AND " +  LookupProperty[iFindCol,6] + "=";
								sFilter += "'" + this[this.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,5])].ToString() + "'";
							}
						}
						if (LookupProperty.GetUpperBound(1)==8)
						{
							if (LookupProperty[iFindCol,8]!="")
							{
								sFilter += " AND " +  LookupProperty[iFindCol,8] + "=";
								sFilter += "'" + this[this.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,7])].ToString() + "'";
							}
						}
						break;
				}
			}

			sFindColumn = sFindColumn.Replace("[","").Replace("]","");
			dvFind = new DataView(dsFind.Tables[sCurrentColName]);
			sFindValue = sFindValue.Replace("*","[*]");
			sFindValue = sFindValue.Replace("%","[%]");
			sFindValue = sFindValue.Replace("[","[[");
			sFindValue = sFindValue.Replace("]","]]");
			sFindValue = sFindValue.Replace("[[","[[]");
			sFindValue = sFindValue.Replace("]]","[]]");

			dvFind.RowFilter = "[" + sFindColumn + "] LIKE '" + sFindValue + "%'" + sFilter;
			if(bIsTextChanged)
				dvFind.Sort = "[" + sFindColumn + "] DESC";
			else
				dvFind.Sort = "[" + sFindColumn + "] ASC";
			
			if (dvFind.Count>0) 
			{
				sFindColumn = sFindColumn.Replace("[","").Replace("]","");
				sVal = dvFind[0][sFindColumn].ToString();
				switch(dsFind.Tables[sCurrentColName].Columns[sFindColumn].DataType.Name)
				{
					case "Int16": case "Int32": case "Int64": case "Decimal":
						sVal = (sVal.Trim()!="")?sVal:"0";
						break;
					default:
						break;
				}
			}


			return sVal;
		}

		public string getFindText(string sFindValue, string sColName)
		{
			string sVal = "";
			string sFindColumn="";
			string sCurrentColName="";
			string sFilter ="";

			int iFindCol=0;
			if(sFindValue == "") return ""; // by Anto on 11-Apr-2007
			sCurrentColName = dtGrid.Columns[this.CurrentCell.ColumnNumber].ToString();

			iFindCol = findLookupIndex();
			if (iFindCol == -1) return "";

			sFindColumn = LookupProperty[iFindCol,1];
			
			sFindValue = sFindValue.Replace("'","''");

			if (Convert.ToInt32(LookupProperty[iFindCol,2])>=0)
			{
				sFilter = " AND " +  LookupProperty[iFindCol,3] + "=";		
				switch(LookupProperty[iFindCol,4])
				{
					case "int":
						sFilter = sFilter + this[this.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString();
						break;
					default:
						sFilter = sFilter + "'" + this[this.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString() + "'";
						break;
				}
			}

			dvFind = new DataView(dsFind.Tables[sCurrentColName]);

			/* ---------------------------------------------------------------
			 * Name	: Gerald & Anto
			 * Date	: 24-Jul-2005
			 * */

			if (bIsBactchNumber){
					sFilter += " AND TYPE_DESC = '" + sFieldName + "'";
				bIsBactchNumber = false;
			}

			sFindColumn = sFindColumn.Replace("[","").Replace("]","");

			// ---------------------------------------------------------------
			
			sFindValue = sFindValue.Replace("[","[[");
			sFindValue = sFindValue.Replace("]","]]");
			sFindValue = sFindValue.Replace("[[","[[]");
			sFindValue = sFindValue.Replace("]]","[]]");
			sFindValue = sFindValue.Replace("*","[*]");
			sFindValue = sFindValue.Replace("%","[%]");

			dvFind.RowFilter = "[" + sFindColumn + "] LIKE '" + sFindValue + "%'" + sFilter;
			dvFind.Sort = "[" + sFindColumn + "] ASC";
			if (dvFind.Count>0) 
			{
				sVal = dvFind[0][sColName].ToString();
				switch(dsFind.Tables[sCurrentColName].Columns[sColName].DataType.Name)
				{
					case "Int16": case "Int32": case "Int64": case "Decimal":
						sVal = (sVal.Trim()!="")?sVal:"0";
						break;
					default:
						break;
				}
			}

			return sVal;
		}

		private int findLookupIndex()
		{
			int nRtn =-1;
			string sCurrentColName="";
			sCurrentColName = dtGrid.Columns[this.CurrentCell.ColumnNumber].ToString();
			
			for(int i=0;i<=LookupProperty.GetUpperBound(0);i++)
			{	
				if (sCurrentColName.ToUpper()==LookupProperty[i,0].ToUpper())
				{
					nRtn =i;
					break;
				}
			}
			return nRtn;
		}

		private void GridTextBox_TextChanged(object sender, System.EventArgs e)
		{			
			TextBox txtBox = ((TextBox)(sender));
			TextBoxText = "";			
			int nCol = int.Parse(txtBox.Name.Replace("txtGridBox",""));
			bool bLookup = (this.TextBoxProperty[nCol,1] == "TRUE") ? true : false;
			TextBoxText = txtBox.Text;
			CurrentTxtBox = txtBox;
			if (bLookup == false) return;

			if (txtBox.Modified == true && bHitDelete == false)
			{						  
				int nSelPos = 1;
				string sVal = "", sVal1 = "";
				sVal = txtBox.Text;	
				strFilterText = sVal;

				nSelPos = txtBox.SelectionStart; 

				if (sVal != "" && bTextSelection  == false)
				{
					bIsTextChanged = true;			//Added by Anto on 21-Oct-2005	
					sVal1 = getFindText(sVal);
					
					//strFilterText = sVal1.Substring(0,nSelPos);
					CurrentText= sVal1;

					if (sVal1 != "")
					{
						txtBox.SelectAll(); 
						txtBox.Text = sVal1;
						txtBox.SelectAll();
						txtBox.SelectionStart = nSelPos;
						bTextSelection = true;
					}
				}
				else
				{
					bTextSelection = false;
				}
			}
			if (bHitDelete == true)
			{
				bHitDelete = false;
				strFilterText = txtBox.Text;
			}
		}

		public string getFindTextByMultiColumn(string GridColumns, string TableColumns,string TableName,string FindColumn)
		{
			string sVal = "",sFilter ="", sConcat="";
			int iDataCol=0;
			
			string[] arrTableCol=TableColumns.Split(',');
			string[] arrGridCol=GridColumns.Split(',');

			for (int i=0; i<arrTableCol.Length; i++)
			{
				iDataCol = Convert.ToInt32(arrGridCol[i]);				
				sFilter += sConcat + arrTableCol[i].ToString() + "=";
				switch(dsFind.Tables[TableName].Columns[arrTableCol[i].ToString()].DataType.Name)
				{
					case "INT":
						sFilter = sFilter + this[this.CurrentRowIndex,iDataCol].ToString();
						break;
					default:
						sFilter = sFilter + "'" + this[this.CurrentRowIndex,iDataCol].ToString() + "'";
						break;
				}
				sConcat=" AND ";
			}

			dvFind = new DataView(dsFind.Tables[TableName]);
			dvFind.RowFilter = sFilter;
			//dvFind.Sort = sFindColumn + " ASC";
			if (dvFind.Count>0) 
			{
				sVal = dvFind[0][FindColumn].ToString();
				switch(dsFind.Tables[TableName].Columns[FindColumn].DataType.Name)
				{
					case "Int16": case "Int32": case "Int64": case "Decimal":
						sVal = (sVal.Trim()!="")?sVal:"0";
						break;
					default:
						break;
				}
			}

			return sVal;
		}

		private void GridTextBox_Enter(object sender, System.EventArgs e)
		{
			TextBox ObjTxt = (TextBox) sender;
			if (ObjTxt.Width==0)
				SendKeys.Send("{TAB}");
			TextBoxText = ObjTxt.Text;
		}
	
		private void dtGrid_ColumnChanging(object sender, DataColumnChangeEventArgs e)
		{
			if (ColumnChanging != null)
				ColumnChanging(sender,e);
		}

		private void dtGrid_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if (myChange) return;
			
			if (ColumnChanged != null)
			{
				ColumnChanged(sender,e);  //Invokes the delegates
				bTextSelection=false;
			}
			
			if (TextBoxProperty[this.CurrentCell.ColumnNumber,1]=="TRUE" & EnableAutoLookup)
			{
				myChange=true;
				string sLkp=e.Row[this.CurrentCell.ColumnNumber].ToString();
				if(sLkp!="") 
				{
					bIsTextChanged = false;		//Added by Anto on 21-Oct-2005
					sLkp= getFindText(sLkp);
					e.Row[this.CurrentCell.ColumnNumber]=sLkp;				
					if (ShowAutoLookup!=null & sLkp=="") ShowAutoLookup(this.CurrentCell.ColumnNumber);
				}
				myChange=false;
			}
			TextBoxText = "";
		}

		private void GridRow_BeforeDelete( object sender, DataRowChangeEventArgs e )
		{	
			if (RowDeleting!= null) RowDeleting(sender,e);
		}

		private void Grid_KeyPress( object sender, KeyPressEventArgs e )
		{
			/* ------------------------------------------------------------------------------------------
			 * Name : Gerald J
			 * Date	: 08-Jul-2005 :: 10.45 AM
			 * Purpose : Not to allow the user to enter the negative amount as well as negative quantity.
			 * */

			string SpecialChars = "";

			if (bIsQuantity == false)
			{
				SpecialChars="'[]|";
			}
			else
			{
				SpecialChars="'[]|-";
			}

			// ------------------------------------------------------------------------------------------

			if (e.KeyChar!=(char)Keys.Return | e.KeyChar!=(char)Keys.Back)
				e.Handled = ((SpecialChars.IndexOf(e.KeyChar.ToString()))!=-1);
			if (e.Handled) MessageBox.Show("Invalid Character",clsGeneral.MsgCaption);
		}

		private void GridRow_AfterDelete( object sender, DataRowChangeEventArgs e )
		{			
			if (RowDeleted!= null) RowDeleted(sender,e);
			refreshSerialNo();
		}

		private void GridRow_BeforeChange( object sender, DataRowChangeEventArgs e )
		{		
			if (RowChanging!= null) RowChanging(sender,e);
		}

		private void GridRow_AfterChange( object sender, DataRowChangeEventArgs e )
		{					
			if (myChange) return;
			
			string sError="";
			e.Row.RowError= "";

			for (int i=0; i<dtGrid.Columns.Count; i++)
			{
				if (TextBoxProperty[i,2].ToString().ToUpper()=="TRUE")
				{
					if (e.Row[i]==DBNull.Value)
					{
						sError += dtGrid.Columns[i].ColumnName.ToString() + ", ";
					}

					if (TextBoxProperty[i,7].ToString().ToUpper()=="STRING" )
					{
						if (e.Row[i].ToString().Trim()=="")
							sError += dtGrid.Columns[i].ColumnName.ToString() + ", ";
					}

					else if (TextBoxProperty[i,7].ToString().ToUpper()=="INT" )
					{
						if (Convert.ToDouble( e.Row[i].ToString().Trim())==0)
							sError += dtGrid.Columns[i].ColumnName.ToString() + ", ";
					}
				}
			}			

			if (sError!="")
			{
				e.Row.RowError= "Mandatory Columns ("+  sError.Substring(0,sError.Length-2) +") are not filled";
				MessageBox.Show("Mandatory Columns ("+  sError.Substring(0,sError.Length-2) +") are not filled",clsGeneral.MsgCaption);
			}

			if (dtGrid.Columns[AutoColNumber].ColumnName == sAutoColumn)
			{
				if (e.Row[AutoColNumber].ToString()==iCurrentRowNumber.ToString())
				{
					iCurrentRowNumber++;
					dtGrid.Columns[AutoColNumber].DefaultValue=iCurrentRowNumber.ToString();
				}
			}
			
//			if (sError!="") return;
			if (RowChanged!= null) RowChanged(sender,e);			
		}

		private void GridTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			bHitDelete = (e.KeyCode == Keys.Back | e.KeyCode == Keys.Delete);
			if (e.KeyCode == Keys.Up | e.KeyCode == Keys.Down | e.KeyCode == Keys.Left | e.KeyCode == Keys.Right | 
				e.KeyCode == Keys.Home | e.KeyCode == Keys.End | bHitDelete) 
				bTextSelection = false;
		}

		private void dtGrid_CellEnable(int iRow, int iCol, ref bool bEnable)
		{
			if (CellEanbleEvent!=null) CellEanbleEvent(iRow, iCol,ref  bEnable);
		}

		private void setPrimaryKeys()
		{
			if (PrimaryColumns!=null)
			{
				int iCols=PrimaryColumns.GetUpperBound(0);
				DataColumn[] PrimaryKeys= new DataColumn[iCols+1];
				for(int i=0;i<=iCols;i++)
					PrimaryKeys[i]= dtGrid.Columns[PrimaryColumns[i]];

				dtGrid.PrimaryKey = PrimaryKeys;
			}
		}

		public bool removeRows(string sFindColumn, string sFindData)
		{
			bool bRtn=false;
			DataView dv = new DataView(dtGrid);
			for(int i=0;i<dtGrid.Rows.Count;i++)
			{
				if (dtGrid.Rows[i][sFindColumn].ToString().ToUpper() == sFindData.ToUpper())
				{
					dtGrid.Rows[i].RowError="Deleted";
				}
			}	
			return bRtn;
		}

		public void refreshSerialNo()
		{			
			if (dtGrid.Columns[0].ColumnName !=sAutoColumn) return;
			
			int i,iCount;
			iCount = dtGrid.Rows.Count;
			int nDeleted = dtGrid.Select(null, null, DataViewRowState.Deleted).Length;

			myChange=true;
			int iNumber = 1;
			nDeleted = 0;
			for(i=0; i<iCount; i++)  
			{			
				bool bUnchanged = (dtGrid.Rows[i].RowState.ToString() == "Unchanged");
				bool bDeleteed	= (dtGrid.Rows[i].RowState.ToString() == "Deleted");

				// --- JG on 26-Jul-2006 01:45 PM -----------------------------------
				// If the row state is DELETED OR DETACHED, value cannot be assigned 
				// or retrieved from that row. Suppose, if we assign value for it,
				// "No value at index i" error will occur.
				string sTemp	= dtGrid.Rows[i].RowState.ToString();	// It can be deleted
				bool bDetached	= (dtGrid.Rows[i].RowState.ToString() == "Detached");
				
				try
				{
					if (bDeleteed == false & bDetached == false)
					{
						dtGrid.Rows[i][AutoColNumber] = iNumber++;
						if (bUnchanged) dtGrid.Rows[i].AcceptChanges();								
					}
					else
						nDeleted++;				
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				// ------------------------------------------------------------------
			}
			iCount = iCount - nDeleted;
			iCurrentRowNumber = iCount+1;
			dtGrid.Columns[AutoColNumber].DefaultValue=iCurrentRowNumber.ToString();
			myChange=false;
		}

		/* ---------------------------------------------------------------------------
		 * Name		: AK & JG
		 * Date		: 09-Nov-2005 05:00 PM
		 * Purpose	: To create a new row by invoking the ColumnStartedEditing Protected
		 *			  Method of DataGrid when the values are chosen from the Lookup Form.
		 * */

		public void startEditing(object sender)
		{
			base.ColumnStartedEditing((DataGrid)sender);
		}
		//-----------------------------------------------------------------------------

		private void CellButtonClick(object sender , DataGridCellButtonClickEventArgs e)
		{
			if (HandleCellButtonClicked!=null)
				HandleCellButtonClicked(sender, e);
		}
	}
}