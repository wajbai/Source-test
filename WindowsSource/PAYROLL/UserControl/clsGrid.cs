using System;
using System.Windows.Forms;
using System.Drawing;

namespace PAYROLL.UserControl
{
	public delegate void DataGridCellButtonClickEventHandler(object sender ,  DataGridCellButtonClickEventArgs e);

	public class clsGrid:DataGrid
	{
		private bool bMyChange=false;
		public bool DisableAutoFocus = false;
		public event KeyPressEventHandler GridKeyPress;
		public event KeyEventHandler GridKeyDown;
		public string CurrentText ="";

		public clsGrid()
		{

		}

		protected override bool ProcessDialogKey(Keys keyData)
		{			
			DataGridColumnStyle dc;
			// By Anto on 16-Nov-2006 to avoid run time error with pressing keys before setting datasource
			if(this.TableStyles.Count == 0) return true;
			//------------------------------------------
			dc = this.TableStyles[0].GridColumnStyles[this.CurrentCell.ColumnNumber];

			/* ----------------------------------------------------------------
			 * Name : Gerald J
			 * Date : 06-Aug-2005 :: 02.50 PM
			 *		: Don't delete the following commented lines
			 * */

			if (GridKeyDown!=null) 
			{				
				//if (keyData.ToString()=="F3")
				//	this.EndEdit(dc, this.CurrentCell.RowNumber,true);
				GridKeyDown(this,new  KeyEventArgs(keyData));
				//if (CurrentText !="" & this[this.CurrentCell.RowNumber,this.CurrentCell.ColumnNumber].ToString()=="")
				//	this[this.CurrentCell.RowNumber,this.CurrentCell.ColumnNumber]=CurrentText;
			}

			// ----------------------------------------------------------------

			if (keyData.ToString()=="Tab")
			{
				if (!DisableAutoFocus)
				{
					if (dc.ReadOnly)
						SetColumnFocusForward(this.CurrentCell.RowNumber,this.CurrentCell.ColumnNumber);
					else
						SetColumnFocusForward(this.CurrentCell.RowNumber,this.CurrentCell.ColumnNumber+1);
				}
				return true;
			}
			else if (keyData.ToString()=="F11")
			{
				SendKeys.Send("^{TAB}");
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
		
		protected override bool ProcessKeyPreview(ref System.Windows.Forms.Message Messagem )
		{

			if (GridKeyPress!=null) GridKeyPress(this,new KeyPressEventArgs((char)Messagem.WParam.ToInt32()));

			if (bMyChange)
			{
				bMyChange=false;

				//Changed on 09/12/2205
				//return base.ProcessKeyPreview(ref Messagem);
				return false;
			}
			
			DataGridColumnStyle dc;
			dc = this.TableStyles[0].GridColumnStyles[this.CurrentCell.ColumnNumber];

			if (Messagem.WParam.ToInt32()==13)
			{
				if (dc.ReadOnly)
					SetColumnFocusForward(this.CurrentCell.RowNumber,this.CurrentCell.ColumnNumber);
				else
					SetColumnFocusForward(this.CurrentCell.RowNumber,this.CurrentCell.ColumnNumber+1);
			}
			return base.ProcessKeyPreview(ref Messagem);
		}

		private void SetColumnFocusForward(int RowNumber,int ColNumber)
		{
			if (this.TableStyles.Count>0)
			{
				DataGridColumnStyle dd;
				int i=0;

				int colcount=this.TableStyles[0].GridColumnStyles.Count;

				for (i=ColNumber; i<colcount; i++)
				{
					dd=this.TableStyles[0].GridColumnStyles[i];
					if (!dd.ReadOnly & dd.Width>0) 
					{
						bMyChange=true;
						this.CurrentCell=new DataGridCell(RowNumber,i);
						break;
					}
				}			
				if (i==colcount) SetColumnFocusForward(this.CurrentCell.RowNumber+1,0);
			}
		}

		private void SetColumnFocusBackward(int RowNumber,int ColNumber)
		{
			DataGridColumnStyle dd;
			int i=0, colcount=this.TableStyles[0].GridColumnStyles.Count;

			for (i=ColNumber; i>=0; i--)
			{
				dd=this.TableStyles[0].GridColumnStyles[i];
				if (!dd.ReadOnly & dd.Width>0) 
				{
					bMyChange=true;
					this.CurrentCell=new DataGridCell(RowNumber,i);
					break;
				}
			}
			if (i==-1) SetColumnFocusBackward(this.CurrentCell.RowNumber-1,colcount);
		}
	}

	public class DataGridButtonColumn:DataGridTextBoxColumn
	{
		public event DataGridCellButtonClickEventHandler CellButtonClicked;
		private Bitmap _buttonFace;
		private Bitmap _buttonFacePressed;
		private int _columnNum ;
		private int _pressedRow;
		private DataGrid objDataGrid;

		public DataGridButtonColumn(int colNum, DataGrid parent)
		{
			_columnNum = colNum;
			_pressedRow = -1;

			try
			{
				System.Reflection.Assembly thisExe; 
				thisExe = System.Reflection.Assembly.GetExecutingAssembly();
				string exeName = thisExe.GetName().Name;

				System.IO.Stream strm = this.GetType().Assembly.GetManifestResourceStream("Utility.Common.fullbuttonface.bmp");
				_buttonFace = new Bitmap(strm);
				strm = this.GetType().Assembly.GetManifestResourceStream("Utility.Common.fullbuttonfacepressed.bmp");
				_buttonFacePressed = new Bitmap(strm);

				objDataGrid =  parent;
				if (objDataGrid.Tag==null)
				{
					objDataGrid.MouseDown += new MouseEventHandler(HandleMouseDown);
					objDataGrid.MouseUp += new MouseEventHandler(HandleMouseUp);
					objDataGrid.Tag = "a";
				}
			}
			catch{}
		}

		private void DrawButton (Graphics g, Bitmap bm, Rectangle bounds, int row)
		{
			DataGrid dg = this.DataGridTableStyle.DataGrid;

			string s = dg[row,this._columnNum].ToString();

			SizeF sz = g.MeasureString(s, dg.Font, bounds.Width-4, StringFormat.GenericTypographic);

			int x = Convert.ToInt32(bounds.Left + Math.Max(0, (bounds.Width-sz.Width)/2));
			g.DrawImage(bm, bounds, 0,0, bm.Width, bm.Height, GraphicsUnit.Pixel);
			
			if(sz.Height<bounds.Height)
			{
				int y = Convert.ToInt32( bounds.Top + ((bounds.Height/2) - (sz.Height/2)));
				if (_buttonFacePressed == bm)
					x++;
				g.DrawString(s, dg.Font, new SolidBrush(dg.ForeColor), x, y);
			}
		}

		protected override void Edit( CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, 
			string instantText, bool cellIsVisible)
		{
		}

		public void HandleMouseUp(object sender, MouseEventArgs e)
		{
			DataGrid dg = this.DataGridTableStyle.DataGrid;
			DataGrid.HitTestInfo hti = dg.HitTest(new Point(e.X, e.Y));
			bool isClickInCell = (hti.Column==this._columnNum & hti.Row>-1 & hti.Column>-1);

			_pressedRow =-1;

			Rectangle rect = new Rectangle(0,0,0,0);
			
			if (isClickInCell)
			{
				if (objDataGrid.TableStyles[0].GridColumnStyles[_columnNum].ToString().ToUpper().IndexOf("DATAGRIDBUTTONCOLUMN")!=-1)
				{
					rect = dg.GetCellBounds(hti.Row, hti.Column);
					isClickInCell = e.X > rect.Right - this._buttonFace.Width;

					Graphics g= Graphics.FromHwnd(dg.Handle);
					DrawButton(g, this._buttonFace, rect, hti.Row);
					g.Dispose();

					if (CellButtonClicked!=null)
						CellButtonClicked(this, new DataGridCellButtonClickEventArgs(hti.Row, hti.Column));
				}
			}
		}

		public void HandleMouseDown(object sender, MouseEventArgs e)
		{
			DataGrid dg = this.DataGridTableStyle.DataGrid;
			DataGrid.HitTestInfo hti = dg.HitTest(new Point(e.X, e.Y));
			bool isClickInCell = (hti.Column==this._columnNum & hti.Row>-1 & hti.Column>-1);
			Rectangle rect = new Rectangle(0,0,0,0);
			if (isClickInCell)
			{
				if (objDataGrid.TableStyles[0].GridColumnStyles[_columnNum].ToString().ToUpper().IndexOf("DATAGRIDBUTTONCOLUMN")!=-1)
				{
					rect = dg.GetCellBounds(hti.Row, hti.Column);
					isClickInCell = e.X>rect.Right-this._buttonFace.Width;

					Graphics g = Graphics.FromHwnd(dg.Handle);
					DrawButton(g, this._buttonFacePressed, rect, hti.Row);
					g.Dispose();
					_pressedRow = hti.Row;
				}
			}
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, 
			int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			DataGrid parent = this.DataGridTableStyle.DataGrid;
			bool current = parent.IsSelected(rowNum) | (parent.CurrentRowIndex==rowNum & parent.CurrentCell.ColumnNumber==this._columnNum);

			Color BackColor;
			if(current) BackColor = parent.SelectionBackColor; else BackColor = parent.BackColor;

			Color ForeColor;
			if (current) ForeColor = parent.SelectionForeColor; else ForeColor = parent.ForeColor;

			g.FillRectangle(new SolidBrush(BackColor), bounds);

			string s = this.GetColumnValueAtRow(source,rowNum).ToString();
			
			Bitmap bm;

			if (_pressedRow==rowNum) bm=this._buttonFacePressed; else bm=this._buttonFace;

			DrawButton(g,bm, bounds,rowNum);
		}
	}

	public class DataGridCellButtonClickEventArgs:EventArgs
	{
		private int _row, _col;

		public DataGridCellButtonClickEventArgs(int row , int col)
		{
			_row = row;
			_col = col;
		}

		public int RowIndex
		{
			get{return _row;}
		}

		public int ColIndex
		{
			get{return _col;}
		}			
	}//class DataGridCellButtonClickEventArgs
}