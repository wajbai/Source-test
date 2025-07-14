using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using Bosco.Utility.Common;

namespace PAYROLL.UserControl
{	
	public delegate void CellBackColorHandler(int RowNumber, int ColumnNumber, ref Brush ForeBrush, ref Brush BackBrush);

	public class clsGridEntry : System.Windows.Forms.Form
	{
		private DataSet dsFind;
		private DataView dvFind;

		private DataTable dtGrid;
		private DataView dvGrid;
		private DataGrid dgGrid;

		private bool bHitDelete = false;
		private bool bBoundDataview = false;
		private bool bAllowAdd=true;
		private bool bAllowEdit=true;
		private bool bAllowDelete=true;

		//private bool bManualFind = false;				// Gerald
		public string strFilterText = "";

		private bool myChange=false;

		public bool bTextSelection = false;
		

		private string[,] TextBoxProperty;
		private string sAutoColumn="";

		//public event GridCellEnableEventHandler CellEanbleEvent;
			
		public delegate void ShowLookupHandler(int ColumnNumber);
		public event DataColumnChangeEventHandler GridColChanging;
		public event DataColumnChangeEventHandler GridColChanged;
		public event DataRowChangeEventHandler Grid_RowChanged;
		public event DataRowChangeEventHandler Grid_RowChanging;
		public event DataRowChangeEventHandler Grid_RowDeleted;
		public event DataRowChangeEventHandler Grid_RowDeleting;
		public event KeyEventHandler Text_KeyDown;
		public event KeyPressEventHandler Grid_KeyPress;

		

		private bool bColorReadOnlyCols=true;
		
		public event ShowLookupHandler ShowAutoLookup;
				
		//For Combo
		private string[,] ComboProperty;

		//For Lookup
		private string[,] LookupProperty;

		//For Primay Column
		private string[] PrimaryColumns;
		public bool EnableAutoLookup=false;
		private int iCurrentRowNumber=1;
		
		public bool ReplaceNullwithZero=false;

		public clsGridEntry()
		{
			
		}

		//get Source manuplation table
		public DataTable getSourceTable
		{
			get{return dtGrid;}
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
				if (dsFind == null)
					dsFind = value.Copy();
				else
				{
					if (dsFind.Tables.Contains(value.Tables[0].TableName))
						dsFind.Tables.Remove(value.Tables[0].TableName);
					dsFind.Tables.Add(value.Tables[0].Copy());
				}
			}
		}
		public string[,] setLookupProperty
		{
			set{this.LookupProperty = value;}
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

		private int AutoColNumber=0;

		public void CreateGridTextBox(string sSQL, string sSourceTable, System.Windows.Forms.DataGrid dgStyle, string[,] TextBoxProperty)
		{			
            //DataAccess.DataHandling objDh = new DataAccess.DataHandling();
            //objDh.createDataSet(sSQL, "List");
            //DataSet ds =  objDh.getDataSet();			
            //dvGrid = new DataView(ds.Tables["List"]);
            //dvGrid.AllowNew=bAllowAdd;
            //dvGrid.AllowEdit=bAllowEdit;
            //dvGrid.AllowDelete=bAllowDelete;			

            //bBoundDataview=true;
            //dgStyle.DataSource=dvGrid;
            //CreateGridTextBox(sSourceTable,dgStyle, TextBoxProperty);
		}

		public void CreateGridTextBox(string sSourceTable, System.Windows.Forms.DataGrid dgStyle, string[,] TextBoxProperty)
		{
			int i = 0;
			int iComboIndex = 0;
			string sDataType = "", sMapDataType = "";
			dgStyle.TableStyles.Clear();
			
			dgGrid = dgStyle;
			bBoundDataview = (dgStyle.DataSource.GetType().Name=="DataView")?true:false;
			if (!bBoundDataview)
				dtGrid = ((DataSet)dgStyle.DataSource).Tables[sSourceTable];
			else
			{
				dvGrid = (DataView)dgStyle.DataSource;
				dvGrid.AllowNew=bAllowAdd;
				dvGrid.AllowEdit=bAllowEdit;
				dvGrid.AllowDelete=bAllowDelete;
				dtGrid = dvGrid.Table;
			}

			DataGridTableStyle tableStyle = new DataGridTableStyle();
			
			tableStyle.AlternatingBackColor = System.Drawing.Color.White;
			tableStyle.BackColor = System.Drawing.Color.White;
			dgStyle.BackgroundColor = System.Drawing.Color.LightGray;
			dgStyle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgStyle.CaptionBackColor = System.Drawing.Color.Teal;
			dgStyle.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			dgStyle.CaptionForeColor = System.Drawing.Color.White;
			dgStyle.FlatMode = true;
			dgStyle.Font = new System.Drawing.Font("Tahoma", 10F);
			tableStyle.ForeColor = System.Drawing.Color.Black;
			tableStyle.GridLineColor = System.Drawing.Color.Silver;
            tableStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(206)))), ((int)(((byte)(252)))));
			tableStyle.HeaderFont = new Font("Tahoma",10F,FontStyle.Bold,GraphicsUnit.Point);
			tableStyle.HeaderForeColor = System.Drawing.Color.Black;
			tableStyle.LinkColor = System.Drawing.Color.Purple;
			dgStyle.ParentRowsBackColor = System.Drawing.Color.Gray;
			dgStyle.ParentRowsForeColor = System.Drawing.Color.White;
			tableStyle.SelectionBackColor = System.Drawing.Color.Maroon;
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
					//dtGrid.Columns[i].AutoIncrement=true;
					//dtGrid.Columns[i].AutoIncrementSeed=dtGrid.Rows.Count+1;
					//dtGrid.Columns[i].AutoIncrementStep=1;
					iCurrentRowNumber= dtGrid.Rows.Count+1;
					dtGrid.Columns[i].DefaultValue = iCurrentRowNumber;
					AutoColNumber=i;
				}
				else
					//dtGrid.Columns[i].DefaultValue = (sMapDataType == "DATE")? clsGeneral.getServerDate() DateTime.Today.Date.ToShortDateString():TextBoxProperty[i,8];
				//	dtGrid.Columns[i].DefaultValue = (sMapDataType == "DATE")? clsGeneral.getServerDate():TextBoxProperty[i,8];

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
					colStyle.NullText="0";	//Null Text;
					tableStyle.GridColumnStyles.Add(colStyle);
					colStyle.Dispose(); 
				}
				else if (sMapDataType=="DBCOMBO")	//Bound Combo
				{
					DataGridComboColumn cboColumnStyle = new DataGridComboColumn();
					cboColumnStyle.MappingName = dtGrid.Columns[i].ColumnName;
					cboColumnStyle.HeaderText = dtGrid.Columns[i].ColumnName;
					
					cboColumnStyle.SQL = ComboProperty[iComboIndex,0].ToString();
					cboColumnStyle.DisplayMember = ComboProperty[iComboIndex,1].ToString();
					cboColumnStyle.ValueMember = ComboProperty[iComboIndex,2].ToString();
					cboColumnStyle.ValueColumnIndex = Convert.ToInt32( ComboProperty[iComboIndex,3].ToString());
					cboColumnStyle.Width = int.Parse(TextBoxProperty[i,6]);
					cboColumnStyle.FillCombo();
					tableStyle.GridColumnStyles.Add(cboColumnStyle);
					cboColumnStyle.Dispose();
					iComboIndex++;
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
					
					//--------------------
					colStyle.TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridTextBox_KeyDown);
					colStyle.TextBox.TextChanged  += new System.EventHandler(this.GridTextBox_TextChanged);
					colStyle.TextBox.Enter +=new System.EventHandler(this.GridTextBox_Enter);
					colStyle.TextBox.KeyPress +=new System.Windows.Forms.KeyPressEventHandler(this.GridKeyPress);
					//this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);

					 
					//set Null Text --------------------------------------------------------------------------------
					colStyle.NullText="";	
					if (sMapDataType=="INT" & ReplaceNullwithZero) colStyle.NullText="0";
					if (sMapDataType=="DATE") colStyle.NullText=DateTime.Today.Date.ToShortDateString();
					//----------------------------------------------------------------------------------------------

					tableStyle.GridColumnStyles.Add(colStyle);
					colStyle.Dispose();
				}
			}
			
			dgStyle.TableStyles.Add(tableStyle);
			
			//set Primary columns to the table
			setPrimaryKeys();
		}		

		private string getFindText(string sFindValue)
		{
			string sVal = "";
			string sCurrentColName="";
			string sFindColumn="";
			string sFilter="";
			int iFindCol=0;
			
			sCurrentColName = dtGrid.Columns[dgGrid.CurrentCell.ColumnNumber].ToString();
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
						sFilter = sFilter + dgGrid[dgGrid.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString();
						break;
					default:
						sFilter = sFilter + "'" + dgGrid[dgGrid.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString() + "'";
						if (LookupProperty.GetUpperBound(1)==6)
						{
							if (LookupProperty[iFindCol,6]!="")
							{
								sFilter += " AND " +  LookupProperty[iFindCol,6] + "=";
								sFilter += "'" + dgGrid[dgGrid.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,5])].ToString() + "'";
							}
						}
						break;
				}
			}

			dvFind = new DataView(dsFind.Tables[sCurrentColName]);
			dvFind.RowFilter = sFindColumn + " LIKE '" + sFindValue + "%'" + sFilter;
			//dvFind.Sort = sFindColumn + " ASC";
			//if (dvFind.Count>0) sVal = dvFind[0][sFindColumn].ToString();
			
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

			sCurrentColName = dtGrid.Columns[dgGrid.CurrentCell.ColumnNumber].ToString();

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
						sFilter = sFilter + dgGrid[dgGrid.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString();
						break;
					default:
						sFilter = sFilter + "'" + dgGrid[dgGrid.CurrentRowIndex,int.Parse(LookupProperty[iFindCol,2])].ToString() + "'";
						break;
				}
			}

			dvFind = new DataView(dsFind.Tables[sCurrentColName]);
			dvFind.RowFilter = sFindColumn + " LIKE '" + sFindValue + "%'" + sFilter;
			dvFind.Sort = sFindColumn + " ASC";
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
			sCurrentColName = dtGrid.Columns[dgGrid.CurrentCell.ColumnNumber].ToString();
			
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
			int nCol = int.Parse(txtBox.Name.Replace("txtGridBox",""));
			bool bLookup = (this.TextBoxProperty[nCol,1] == "TRUE") ? true : false;
			if (bLookup == false) return;

			if (txtBox.Modified == true && bHitDelete == false)
			{						  
				int nSelPos = 1;
				string sVal = "", sVal1 = "";
				sVal = txtBox.Text;

				// --- Gerald ------------------

				if (nCol == 2)
					strFilterText = sVal;
				else
					strFilterText = "";

				// --- ENDS HERE ---------------

				nSelPos = txtBox.SelectionStart; 

				if (sVal != "" && bTextSelection  == false)
				{
					sVal1 = getFindText(sVal);  
				
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
			if (bHitDelete == true) bHitDelete = false;
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
						sFilter = sFilter + dgGrid[dgGrid.CurrentRowIndex,iDataCol].ToString();
						break;
					default:
						sFilter = sFilter + "'" + dgGrid[dgGrid.CurrentRowIndex,iDataCol].ToString() + "'";
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
		}
	
		private void dtGrid_ColumnChanging(object sender, DataColumnChangeEventArgs e)
		{
			if (GridColChanging != null)
				GridColChanging(sender,e);
		}

		private void dtGrid_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if (myChange) return;
			
			if (GridColChanged != null)
			{
				GridColChanged(sender,e);  //Invokes the delegates
				bTextSelection=false;
			}
			
			if (TextBoxProperty[dgGrid.CurrentCell.ColumnNumber,1]=="TRUE" & EnableAutoLookup)
			{
				myChange=true;
				string sLkp=e.Row[dgGrid.CurrentCell.ColumnNumber].ToString();
				if(sLkp!="") 
				{
					sLkp= getFindText(sLkp);
					e.Row[dgGrid.CurrentCell.ColumnNumber]=sLkp;				
					if (ShowAutoLookup!=null & sLkp=="") ShowAutoLookup(dgGrid.CurrentCell.ColumnNumber);
				}
				myChange=false;
			}			
		}

		private void GridRow_BeforeDelete( object sender, DataRowChangeEventArgs e )
		{	
			//			throw new Exception(" is not less than 10");
			if (Grid_RowDeleting!= null) Grid_RowDeleting(sender,e);
		}

		private void GridKeyPress( object sender, KeyPressEventArgs e )
		{	
			string SpecialChars="'[]|";
			if (e.KeyChar!=(char)Keys.Return | e.KeyChar!=(char)Keys.Back)
				e.Handled = ((SpecialChars.IndexOf(e.KeyChar.ToString()))!=-1);
			if (e.Handled) MessageBox.Show("Invalid Character",clsGeneral.MsgCaption);
			if (Grid_KeyPress!= null) Grid_KeyPress(sender,e);
		}

		private void GridRow_AfterDelete( object sender, DataRowChangeEventArgs e )
		{			
			if (Grid_RowDeleted!= null) Grid_RowDeleted(sender,e);
			refreshSerialNo();
		}

		private void GridRow_BeforeChange( object sender, DataRowChangeEventArgs e )
		{		
			if (Grid_RowChanging!= null) Grid_RowChanging(sender,e);
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
			
			if (sError!="") return;
			if (Grid_RowChanged!= null) Grid_RowChanged(sender,e);			
		}

		private void GridTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (Text_KeyDown!=null) Text_KeyDown(sender,e);
			bHitDelete = (e.KeyCode == Keys.Back | e.KeyCode == Keys.Delete);
			if (e.KeyCode == Keys.Up | e.KeyCode == Keys.Down | e.KeyCode == Keys.Left | e.KeyCode == Keys.Right | 
				e.KeyCode == Keys.Home | e.KeyCode == Keys.End | bHitDelete) 
				bTextSelection = false;
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
		//-------------- Modified by JV on 06-09-2005 ------------------------
		//-------------- To avoid the problem occuring when delete onvoked ---
//		public void refreshSerialNo()
//		{
//			if (dtGrid.Columns[0].ColumnName !=sAutoColumn) return;
//			
//			int i,iCount;
//			iCount = dtGrid.Rows.Count;
//			int nDeleted = dtGrid.Select(null, null, DataViewRowState.Deleted).Length;
//			iCount = iCount-nDeleted;
//
//			myChange=true;
//			for(i=0; i<iCount; i++)  
//			{			
//				bool bUnchanged = (dtGrid.Rows[i].RowState.ToString()=="Unchanged");
//				dtGrid.Rows[i][AutoColNumber] = i+1;
//				if (bUnchanged) dtGrid.Rows[i].AcceptChanges();								
//			}
//			iCurrentRowNumber = iCount+1;
//			dtGrid.Columns[AutoColNumber].DefaultValue=iCurrentRowNumber.ToString();
//			myChange=false;
//		}
		public void refreshSerialNo()
		{
			if (dtGrid.Columns[0].ColumnName !=sAutoColumn) return;
			
			int i,iCount;
			iCount = dtGrid.Rows.Count;
			int nDeleted = dtGrid.Select(null, null, DataViewRowState.Deleted).Length;
			myChange=true;
			nDeleted = 0;
			for(i=0; i<iCount; i++)  
			{			
				bool bUnchanged = (dtGrid.Rows[i].RowState.ToString()=="Unchanged");
				bool bDeleteed = (dtGrid.Rows[i].RowState.ToString()=="Deleted");
				if (!bDeleteed)
				{
					if (bUnchanged) dtGrid.Rows[i].AcceptChanges();								
				}
				else
					nDeleted=1;
			}
			if(iCount != 0 )
			{
				if(nDeleted == 0)
					iCurrentRowNumber = iCurrentRowNumber-1;
				else
					iCurrentRowNumber = iCurrentRowNumber - nDeleted;
			}
			else
			{
				iCount = iCount - nDeleted;
				iCurrentRowNumber = iCount+1;
			}
			dtGrid.Columns[AutoColNumber].DefaultValue=iCurrentRowNumber.ToString();
			myChange=false;
		}

		//-----------------------------------------------------------------------------
	}

	/*public void refreshSerialNo()
	{
		if (dtGrid.Columns[0].ColumnName !=sAutoColumn) return;
		
		int i,iCount;
		iCount = dtGrid.Rows.Count;
		int nDeleted = dtGrid.Select(null, null, DataViewRowState.Deleted).Length;
		//iCount = iCount-nDeleted;

		myChange=true;
		int iNumber = 1;
		nDeleted = 0;
		for(i=0; i<iCount; i++)  
		{			
			bool bUnchanged = (dtGrid.Rows[i].RowState.ToString()=="Unchanged");
			bool bDeleteed = (dtGrid.Rows[i].RowState.ToString()=="Deleted");
			if (!bDeleteed)
			{
				dtGrid.Rows[i][AutoColNumber] = iNumber++;
				if (bUnchanged) dtGrid.Rows[i].AcceptChanges();								
			}
			else
				nDeleted++;
		}
		iCount = iCount - nDeleted;
		iCurrentRowNumber = iCount+1;
		dtGrid.Columns[AutoColNumber].DefaultValue=iCurrentRowNumber.ToString();
		myChange=false;
	}*/

	public class DataGridComboColumn : DataGridColumnStyle 
	{
		private ComboBox myComboBox = new ComboBox();
		// The isEditing field tracks whether or not the user is
		// editing data with the hosted control.
		private bool isEditing;
		private int iValueColumnIndex=0;
		private string strValueMember="";
		private string strSQL="";
		private string strDisplayMember="";

		public int ValueColumnIndex
		{
			get {return iValueColumnIndex;}
			set {iValueColumnIndex= value;}
		}

		public string SQL
		{
			get {return strSQL;}
			set {strSQL= value;}
		}

		public string DisplayMember
		{
			get {return strDisplayMember;}
			set {strDisplayMember= value;}
		}

		public string ValueMember
		{
			get {return strValueMember;}
			set {strValueMember= value;}
		}

		public DataGridComboColumn() : base() 
		{
			myComboBox.Visible = false;
			myComboBox.DropDownStyle=ComboBoxStyle.DropDownList;			
		}

		public DataGridComboColumn(string arrList) : base() 
		{
			myComboBox.Visible = false;
			myComboBox.DropDownStyle=ComboBoxStyle.DropDownList;			
			myComboBox.Items.Clear();

			string[] arr = arrList.Split(',');

			for (int i=0; i<arr.Length; i++)
			{
				myComboBox.Items.Add(arr[i]);
			}
		}

		public void FillCombo()
		{
			//clsGeneral.fillList(myComboBox,strSQL,strDisplayMember,strValueMember);
		}

		protected override void Abort(int rowNum)
		{
			isEditing = false;
			myComboBox.TextChanged -= new EventHandler(ComboValueChanged);
			Invalidate();
		}

		protected override bool Commit (CurrencyManager dataSource, int rowNum) 
		{
			myComboBox.Bounds = Rectangle.Empty;         
			myComboBox.TextChanged-= new EventHandler(ComboValueChanged);
			if (!isEditing)	return true;
			isEditing = false;

			DataRowView dr=(DataRowView)dataSource.Current;
			if (myComboBox.SelectedValue==null)
			{
				int iSel = myComboBox.SelectedIndex+1;
				dr[iValueColumnIndex]=iSel.ToString();
			}
			else
				dr[iValueColumnIndex]=myComboBox.SelectedValue.ToString();

			try
			{
				string value = myComboBox.Text;
				SetColumnValueAtRow(dataSource, rowNum, value);
			} 
			catch (Exception) 
			{				
				Abort(rowNum);
				return false;
			}
			Invalidate();
			return true;
		}

		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, 
			bool readOnly, string instantText, bool cellIsVisible) 
		{
			object cellvalue = GetColumnValueAtRow(source, rowNum);
			string value=cellvalue.ToString();
			
			if (cellIsVisible) 
			{
				myComboBox.Bounds = new Rectangle	(bounds.X + 2, bounds.Y + 2, 
					bounds.Width - 4, bounds.Height - 4);
				myComboBox.Text = value;
				myComboBox.Visible = true;			
				myComboBox.SelectedValueChanged+= new EventHandler(ComboValueChanged);
			} 
			else 
			{
				myComboBox.Text= value;
				myComboBox.Visible = false;
			}
			if (myComboBox.Visible) DataGridTableStyle.DataGrid.Invalidate(bounds);
		}

		protected override Size GetPreferredSize(Graphics g, object value) 
		{
			return new Size(100, myComboBox.PreferredHeight + 4);
		}

		protected override int GetMinimumHeight() 
		{
			return myComboBox.PreferredHeight + 4;
		}

		protected override int GetPreferredHeight(Graphics g,object value) 
		{
			return myComboBox.PreferredHeight + 4;
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum) 
		{
			Paint(g, bounds, source, rowNum, false);
		}

		protected override void Paint(Graphics g, Rectangle bounds,	CurrencyManager source, int rowNum,	bool alignToRight) 
		{
			Paint(g,bounds, source, rowNum, Brushes.Red, Brushes.Blue, alignToRight);
		}

		protected override void Paint(Graphics g, Rectangle bounds,	CurrencyManager source, int rowNum,
			Brush backBrush, Brush foreBrush,	bool alignToRight) 
		{
		
			object cellvalue=GetColumnValueAtRow(source, rowNum);
			string val= cellvalue.ToString();

			Rectangle rect = bounds;
			g.FillRectangle(backBrush,rect);
			rect.Offset(0, 2);
			rect.Height -= 2;
			g.DrawString(val.ToString(), this.DataGridTableStyle.DataGrid.Font, foreBrush, rect);
		}

		protected override void SetDataGridInColumn(DataGrid value) 
		{
			base.SetDataGridInColumn(value);
			if (myComboBox.Parent != null) 
			{
				myComboBox.Parent.Controls.Remove (myComboBox);
			}
			if (value != null)
			{
				value.Controls.Add(myComboBox);
			}
		}

		private void ComboValueChanged(object sender, EventArgs e) 
		{
			this.isEditing = true;
			base.ColumnStartedEditing(myComboBox);
		}
	}


	public class DGColumn : DataGridTextBoxColumn
	{
		Color m_BackGroundColor,m_ForeColor;
		int ColumnIndex;
		
		public event GridCellEnableEventHandler CellEanbleEvent;
		public event CellBackColorHandler CellBackColorEvent;

		bool EnabledCell = true;

		public DGColumn(Color BackGroundColor, int nColumnIndex)
		{
			ColumnIndex = nColumnIndex;
			m_BackGroundColor    = BackGroundColor;
			m_ForeColor=Color.Black;
			this.TextBox.BackColor    = m_BackGroundColor;
			this.TextBox.ForeColor=m_ForeColor;
		}

		protected override void ConcedeFocus() 
		{
			// Hide the TextBox when conceding focus.
			base.TextBox.Visible = false;
		}

		protected override void Paint(Graphics g,Rectangle Bounds,CurrencyManager Source,int RowNum, Brush	BackBrush ,
			Brush ForeBrush ,bool AlignToRight)
		{
			EnabledCell = true;

			if ( this.TextBox.BackColor == Color.White)
				if (CellEanbleEvent!=null) CellEanbleEvent(RowNum, ColumnIndex, ref EnabledCell);

			if (EnabledCell==false) 
			{
				Brush backBrush = new SolidBrush(Color.LightGray);
				base.Paint(g,Bounds,Source,RowNum,backBrush,ForeBrush,AlignToRight);
			}
			else
			{
				Brush backBrush = new SolidBrush(m_BackGroundColor);
				Brush foreBrush = new SolidBrush(m_ForeColor);
				if (CellBackColorEvent!=null) CellBackColorEvent(RowNum, ColumnIndex, ref foreBrush, ref backBrush);
				base.Paint(g,Bounds,Source,RowNum,backBrush,foreBrush,AlignToRight);
			}
		}

		protected override void Edit(CurrencyManager source, int rowNum , Rectangle bounds, bool readOnly , string instantText, bool cellIsVisible)
		{
			if (CellEanbleEvent!=null) CellEanbleEvent(rowNum, ColumnIndex, ref EnabledCell);
			if (EnabledCell) base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
		}
	}
}