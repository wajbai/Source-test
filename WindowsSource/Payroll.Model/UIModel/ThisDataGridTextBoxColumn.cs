using System;
using System.Drawing;

namespace DataGridCustomCellFont
{
	/// <summary>
	/// Summary description for ThisDataGrid.
	/// </summary>
	/// 

	public delegate void DataGridCustomEventHandler(object sender, DataGridCustomEventArgs dataGridCustomEventArgs);

	public class DataGridCustomEventArgs: System.EventArgs
	{
		private System.Drawing.Graphics _g;
		private System.Drawing.Rectangle _bounds; 
		private System.Windows.Forms.CurrencyManager _source; 
		private int _rowNum;
		private string _cellData;
		private System.Drawing.Brush _backBrush; 
		private System.Drawing.Brush _foreBrush;
		private bool _alignToRight;
		private bool _isRendered = false;


		public DataGridCustomEventArgs()
		{
		}

		public DataGridCustomEventArgs(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, 
			System.Windows.Forms.CurrencyManager source, 
			int rowNum, System.Drawing.Brush backBrush, 
			System.Drawing.Brush foreBrush, bool alignToRight, string cellData)
		{
			this._g = g;
			this._bounds = bounds;
			this._source = source;
			this._rowNum = rowNum;
			this._backBrush = backBrush;
			this._foreBrush = foreBrush;
			this._alignToRight = alignToRight;
			this._cellData = cellData;
		}

		public System.Drawing.Graphics g
		{
			get{return _g;}
			set {_g = value;}
		}

		public System.Drawing.Rectangle bounds
		{
			get{return _bounds;}
			set {_bounds = value;}
		}

		public System.Windows.Forms.CurrencyManager source
		{
			get{return _source;}
			set {_source = value;}
		}

		public int rowNum
		{
			get{return _rowNum;}
			set {_rowNum = value;}
		}

		public string cellData
		{
			get{return _cellData;}
			set {_cellData = value;}
		}

		public System.Drawing.Brush backBrush
		{
			get{return _backBrush;}
			set {_backBrush = value;}
		}

		public System.Drawing.Brush foreBrush
		{
			get{return _foreBrush;}
			set {_foreBrush = value;}
		}

		public bool alignToRight
		{
			get{return _alignToRight;}
			set {_alignToRight = value;}
		}

		public bool isRendered
		{
			get{return _isRendered;}
			set {_isRendered = value;}
		}
	}

	public class ThisDataGridTextBoxColumn : System.Windows.Forms.DataGridTextBoxColumn
	{
		public event DataGridCustomEventHandler SetDataGridCellFormat;

		public ThisDataGridTextBoxColumn()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, 
			System.Windows.Forms.CurrencyManager source, 
			int rowNum, System.Drawing.Brush backBrush, 
			System.Drawing.Brush foreBrush, bool alignToRight)
		{
			bool isRendered = false;

			if ( SetDataGridCellFormat != null )
			{
				object data = (object)GetColumnValueAtRow(source, rowNum);
				String cellData = data.ToString() ; 

				DataGridCustomEventArgs e = new DataGridCustomEventArgs(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight, cellData);
				SetDataGridCellFormat(this, e);
				g = e.g;
				bounds = e.bounds;
				source = e.source;
				rowNum = e.rowNum;
				backBrush = e.backBrush;
				foreBrush = e.foreBrush;
				alignToRight = e.alignToRight;
				isRendered = e.isRendered;
			}
			
			if (!isRendered)
			{
				base.Paint (g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
			}
		}
	}
}
