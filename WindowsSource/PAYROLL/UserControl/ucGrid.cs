/* Purpose			  : Grid Control in C#
 * Immediate Use	  : HMS and Anabond R&D Project
 * Date Started		  : 20th Dec 2004
 * Author			  : Moses Abraham
 * Features			  : The features available in VB ucGrid
 * Date of I release  : 21st Dec 2004
 * Date of II Release : 30th Jan 2005
 * */


using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
 
using System.Windows.Forms;
using Bosco.Utility.Common;

namespace PAYROLL.UserControl
{
	public class ucGrid : System.Windows.Forms.UserControl 
	{
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem mnuFilterSelection;
		private System.Windows.Forms.MenuItem mnuExcludeFilter;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuDelete;
		private System.Windows.Forms.MenuItem mnuPrint;
		private System.Windows.Forms.MenuItem mnuRemoveFilter;
		private System.Windows.Forms.MenuItem menuItem2;
        private DataTable ds = new DataTable();
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.ComboBox cboColumns;
		private System.Windows.Forms.Label label1;
		//private System.Windows.Forms.DataGrid dataGrid1;
		private PAYROLL.UserControl.clsGrid dataGrid1;
		private System.Windows.Forms.Panel pnlProperties;
		private System.Windows.Forms.TabControl tabProperties;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblFontCaption;
		private System.Windows.Forms.Button btnFontCaption;
		private System.Windows.Forms.Button btnFontGrid;
		private System.Windows.Forms.Label lblFontGrid;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnFontHeader;
		private System.Windows.Forms.Label lblFontHeader;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ListBox lstColumns;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtColumnName;
		private System.Windows.Forms.TextBox txtExpression;
		private System.Windows.Forms.Button btnCheckExpression;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblAlternateBackColor;
		private System.Windows.Forms.Label lblBackColor;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label lblBackGroundColor;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label lblCaptionBackColor;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label lblCaptionForeColor;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label lblForeColor;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lblGridLineColor;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label lblHeaderBackColor;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label lblHeaderForeColor;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label lblSelectionBackColor;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label lblSelectionForeColor;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mnuProperties;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.MenuItem mnuCopy;
		private string sTableName="";
		private string sHideColumns="";
		private bool bHideFilter=false;

		// Events ------------------------------------------------------------------------------------------------------
		public delegate void editHandler (DataRow dr);
		public new event EventHandler DoubleClick; // new keyword is inserted by PE to avoid warning
		public event EventHandler RowColChange;
		public event DataRowChangeEventHandler RowChanged;
		public new event EventHandler Click;  //Inserted by PE for Payroll purpose

		public new event System.Windows.Forms.KeyPressEventHandler KeyPress; //new keyword is inserted by PE to avoid warning
		public event editHandler EditRec;
		//public event editHandler DeleteRec; //commented by PE to avoid warning msg
		//------------------------------------------------------------------------------------------------------


		//================================================================================
		//Property Variables
		private string constr;
		private string sql;
		private string strTitle;

		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.MenuItem mnuInsertFields;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.Button btnInsertFields;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ComboBox cboColTypes;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private string gridID ="";
		private Font captionFont = DefaultFont;
		private Font gridFont=DefaultFont;
		private Font headerFont=DefaultFont;
		private Color alternativeColor;
		private Color backColor;
		private Color backGroundColor;
		private Color captionBackColor;
		private Color captionForeColor = Color.White;
		private Color foreColor;
		private Color gridLineColor;
		private Color headerBackColor;
		private Color headerForeColor;
		private Color selectionBackColor;
		private Color selectionForeColor;
		private string[,] ColumnWidth;
		
		//For Lookup=======================================================================
		private bool enableLookup;
		private string strSelectedRows="";
		private string strSelectColumn="";

		private const string strSelectCaption="SEL";
		private const string strStatusCaption ="Status";
		private bool showStatus = false;
		private const char Delimiter='ê';
		private string sRecordStatus="";
		private System.Windows.Forms.MenuItem mnuUnSelectAll;
		private System.Windows.Forms.MenuItem mnuSelectAll;

		private bool myChange=false;
		private string GridFilterCriteria="";
		private System.Windows.Forms.ComboBox cboSearchType;
		private bool bSelectAll=false;		
		private bool bFitColsToGridWidth=false;
		private bool bHidePopupMenu=false;
		private System.Windows.Forms.CheckBox chkSelectAllRows;
		private System.Windows.Forms.MenuItem mnuSelectRow;
		private System.Windows.Forms.MenuItem mnuUnSelectRow;
		private System.Windows.Forms.MenuItem mnuSelectAllRows;
		private System.Windows.Forms.MenuItem mnuUnSelectAllRows;
		private System.Windows.Forms.MenuItem mnuSelectRowsSep;
        private DataView dvGrid = new DataView();
		//=================================================================================

		
		//=================================================================================
		public ucGrid()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private bool bItemsFiltered = false;
		private bool bLabTestLookup = false;

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.mnuFilterSelection = new System.Windows.Forms.MenuItem();
            this.mnuExcludeFilter = new System.Windows.Forms.MenuItem();
            this.mnuRemoveFilter = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.mnuPrint = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.mnuSelectAll = new System.Windows.Forms.MenuItem();
            this.mnuUnSelectAll = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuSelectRow = new System.Windows.Forms.MenuItem();
            this.mnuUnSelectRow = new System.Windows.Forms.MenuItem();
            this.mnuSelectAllRows = new System.Windows.Forms.MenuItem();
            this.mnuUnSelectAllRows = new System.Windows.Forms.MenuItem();
            this.mnuSelectRowsSep = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuProperties = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboSearchType = new System.Windows.Forms.ComboBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboColumns = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGrid1 = new PAYROLL.UserControl.clsGrid();
            this.pnlProperties = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabProperties = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.lstColumns = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFontHeader = new System.Windows.Forms.Button();
            this.lblFontHeader = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnFontGrid = new System.Windows.Forms.Button();
            this.lblFontGrid = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFontCaption = new System.Windows.Forms.Button();
            this.lblFontCaption = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.cboColTypes = new System.Windows.Forms.ComboBox();
            this.btnInsertFields = new System.Windows.Forms.Button();
            this.contextMenu2 = new System.Windows.Forms.ContextMenu();
            this.mnuInsertFields = new System.Windows.Forms.MenuItem();
            this.label23 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCheckExpression = new System.Windows.Forms.Button();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.txtColumnName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblSelectionForeColor = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblSelectionBackColor = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblHeaderForeColor = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblHeaderBackColor = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblGridLineColor = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblForeColor = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblCaptionForeColor = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblCaptionBackColor = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblBackGroundColor = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAlternateBackColor = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkSelectAllRows = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.pnlProperties.SuspendLayout();
            this.tabProperties.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFilterSelection,
            this.mnuExcludeFilter,
            this.mnuRemoveFilter,
            this.menuItem7,
            this.mnuAdd,
            this.mnuEdit,
            this.mnuDelete,
            this.mnuPrint,
            this.menuItem12,
            this.mnuSelectAll,
            this.mnuUnSelectAll,
            this.menuItem2,
            this.mnuSelectRow,
            this.mnuUnSelectRow,
            this.mnuSelectAllRows,
            this.mnuUnSelectAllRows,
            this.mnuSelectRowsSep,
            this.mnuCopy,
            this.menuItem3,
            this.mnuProperties});
            // 
            // mnuFilterSelection
            // 
            this.mnuFilterSelection.Index = 0;
            this.mnuFilterSelection.Text = "Filter by Selection";
            this.mnuFilterSelection.Click += new System.EventHandler(this.mnuFilter);
            // 
            // mnuExcludeFilter
            // 
            this.mnuExcludeFilter.Index = 1;
            this.mnuExcludeFilter.Text = "Filter Excluding Selection";
            this.mnuExcludeFilter.Click += new System.EventHandler(this.mnuExcludeFilter_Click);
            // 
            // mnuRemoveFilter
            // 
            this.mnuRemoveFilter.Enabled = false;
            this.mnuRemoveFilter.Index = 2;
            this.mnuRemoveFilter.Text = "Remove Filter";
            this.mnuRemoveFilter.Click += new System.EventHandler(this.mnuRemoveFilter_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 3;
            this.menuItem7.Text = "-";
            this.menuItem7.Visible = false;
            // 
            // mnuAdd
            // 
            this.mnuAdd.Index = 4;
            this.mnuAdd.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuAdd.Text = "New";
            this.mnuAdd.Visible = false;
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 5;
            this.mnuEdit.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.mnuEdit.Text = "Edit";
            this.mnuEdit.Visible = false;
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 6;
            this.mnuDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.mnuDelete.Text = "Delete";
            this.mnuDelete.Visible = false;
            // 
            // mnuPrint
            // 
            this.mnuPrint.Index = 7;
            this.mnuPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.mnuPrint.Text = "Print";
            this.mnuPrint.Visible = false;
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 8;
            this.menuItem12.Text = "-";
            // 
            // mnuSelectAll
            // 
            this.mnuSelectAll.Index = 9;
            this.mnuSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.mnuSelectAll.Text = "Select All";
            this.mnuSelectAll.Click += new System.EventHandler(this.mnuSelectAll_Click);
            // 
            // mnuUnSelectAll
            // 
            this.mnuUnSelectAll.Enabled = false;
            this.mnuUnSelectAll.Index = 10;
            this.mnuUnSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.mnuUnSelectAll.Text = "Unselect All";
            this.mnuUnSelectAll.Click += new System.EventHandler(this.mnuUnSelectAll_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 11;
            this.menuItem2.Text = "-";
            // 
            // mnuSelectRow
            // 
            this.mnuSelectRow.Index = 12;
            this.mnuSelectRow.Text = "Select Row";
            this.mnuSelectRow.Visible = false;
            this.mnuSelectRow.Click += new System.EventHandler(this.mnuSelectRow_Click);
            // 
            // mnuUnSelectRow
            // 
            this.mnuUnSelectRow.Index = 13;
            this.mnuUnSelectRow.Text = "Un Select Row";
            this.mnuUnSelectRow.Visible = false;
            this.mnuUnSelectRow.Click += new System.EventHandler(this.mnuUnSelectRow_Click);
            // 
            // mnuSelectAllRows
            // 
            this.mnuSelectAllRows.Index = 14;
            this.mnuSelectAllRows.Text = "Select All Rows";
            this.mnuSelectAllRows.Visible = false;
            this.mnuSelectAllRows.Click += new System.EventHandler(this.mnuSelectAllRows_Click);
            // 
            // mnuUnSelectAllRows
            // 
            this.mnuUnSelectAllRows.Index = 15;
            this.mnuUnSelectAllRows.Text = "Un Select All Rows";
            this.mnuUnSelectAllRows.Visible = false;
            this.mnuUnSelectAllRows.Click += new System.EventHandler(this.mnuUnSelectAllRows_Click);
            // 
            // mnuSelectRowsSep
            // 
            this.mnuSelectRowsSep.Index = 16;
            this.mnuSelectRowsSep.Text = "-";
            this.mnuSelectRowsSep.Visible = false;
            // 
            // mnuCopy
            // 
            this.mnuCopy.Index = 17;
            this.mnuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 18;
            this.menuItem3.Text = "-";
            // 
            // mnuProperties
            // 
            this.mnuProperties.Index = 19;
            this.mnuProperties.Text = "Properties";
            this.mnuProperties.Click += new System.EventHandler(this.mnuProperties_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboSearchType);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.cboColumns);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 328);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 34);
            this.panel1.TabIndex = 14;
            // 
            // cboSearchType
            // 
            this.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchType.Location = new System.Drawing.Point(144, 8);
            this.cboSearchType.Name = "cboSearchType";
            this.cboSearchType.Size = new System.Drawing.Size(152, 21);
            this.cboSearchType.TabIndex = 14;
            // 
            // btnGo
            // 
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGo.Location = new System.Drawing.Point(600, 8);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(32, 20);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "&Go";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(296, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(304, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            // 
            // cboColumns
            // 
            this.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumns.Location = new System.Drawing.Point(40, 8);
            this.cboColumns.Name = "cboColumns";
            this.cboColumns.Size = new System.Drawing.Size(104, 21);
            this.cboColumns.TabIndex = 1;
            this.cboColumns.SelectedIndexChanged += new System.EventHandler(this.cboColumns_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Filter";
            // 
            // dataGrid1
            // 
            this.dataGrid1.AlternatingBackColor = System.Drawing.Color.SteelBlue;
            this.dataGrid1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataGrid1.CaptionBackColor = System.Drawing.Color.LightSteelBlue;
            this.dataGrid1.CaptionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(226)))), ((int)(((byte)(250)))));
            this.dataGrid1.DataMember = "";
            this.dataGrid1.FlatMode = true;
            this.dataGrid1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.dataGrid1.GridLineColor = System.Drawing.Color.Gainsboro;
            this.dataGrid1.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(206)))), ((int)(((byte)(252)))));
            this.dataGrid1.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.HeaderForeColor = System.Drawing.Color.Black;
            this.dataGrid1.LinkColor = System.Drawing.Color.Teal;
            this.dataGrid1.Location = new System.Drawing.Point(0, 8);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.Gainsboro;
            this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.Teal;
            this.dataGrid1.SelectionForeColor = System.Drawing.Color.PaleGreen;
            this.dataGrid1.Size = new System.Drawing.Size(648, 312);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.GridKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid1_GridKeyPress);
            this.dataGrid1.DoubleClick += new System.EventHandler(this.dataGrid1_DoubleClick);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid1_KeyPress);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // pnlProperties
            // 
            this.pnlProperties.Controls.Add(this.label22);
            this.pnlProperties.Controls.Add(this.btnClose);
            this.pnlProperties.Controls.Add(this.btnSave);
            this.pnlProperties.Controls.Add(this.tabProperties);
            this.pnlProperties.Location = new System.Drawing.Point(120, 48);
            this.pnlProperties.Name = "pnlProperties";
            this.pnlProperties.Size = new System.Drawing.Size(384, 240);
            this.pnlProperties.TabIndex = 16;
            this.pnlProperties.Visible = false;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.SystemColors.Highlight;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label22.Location = new System.Drawing.Point(2, 2);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(382, 18);
            this.label22.TabIndex = 3;
            this.label22.Text = "Properties";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(320, 216);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 20);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(248, 216);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 20);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.tabPage1);
            this.tabProperties.Controls.Add(this.tabPage2);
            this.tabProperties.Controls.Add(this.tabPage3);
            this.tabProperties.Location = new System.Drawing.Point(8, 26);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.SelectedIndex = 0;
            this.tabProperties.Size = new System.Drawing.Size(368, 184);
            this.tabProperties.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnFontHeader);
            this.tabPage1.Controls.Add(this.lblFontHeader);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnFontGrid);
            this.tabPage1.Controls.Add(this.lblFontGrid);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnFontCaption);
            this.tabPage1.Controls.Add(this.lblFontCaption);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(360, 158);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.vScrollBar1);
            this.panel2.Controls.Add(this.lstColumns);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 78);
            this.panel2.TabIndex = 11;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(88, 33);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(16, 32);
            this.vScrollBar1.TabIndex = 2;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // lstColumns
            // 
            this.lstColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstColumns.Location = new System.Drawing.Point(104, 0);
            this.lstColumns.MultiColumn = true;
            this.lstColumns.Name = "lstColumns";
            this.lstColumns.Size = new System.Drawing.Size(248, 67);
            this.lstColumns.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Column Positions";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Grid Lines";
            // 
            // btnFontHeader
            // 
            this.btnFontHeader.Location = new System.Drawing.Point(320, 56);
            this.btnFontHeader.Name = "btnFontHeader";
            this.btnFontHeader.Size = new System.Drawing.Size(24, 16);
            this.btnFontHeader.TabIndex = 9;
            this.btnFontHeader.Click += new System.EventHandler(this.btnFontHeader_Click);
            // 
            // lblFontHeader
            // 
            this.lblFontHeader.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFontHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFontHeader.Location = new System.Drawing.Point(120, 56);
            this.lblFontHeader.Name = "lblFontHeader";
            this.lblFontHeader.Size = new System.Drawing.Size(200, 16);
            this.lblFontHeader.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(72, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Header";
            // 
            // btnFontGrid
            // 
            this.btnFontGrid.Location = new System.Drawing.Point(320, 32);
            this.btnFontGrid.Name = "btnFontGrid";
            this.btnFontGrid.Size = new System.Drawing.Size(24, 16);
            this.btnFontGrid.TabIndex = 6;
            this.btnFontGrid.Click += new System.EventHandler(this.btnFontGrid_Click);
            // 
            // lblFontGrid
            // 
            this.lblFontGrid.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFontGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFontGrid.Location = new System.Drawing.Point(120, 32);
            this.lblFontGrid.Name = "lblFontGrid";
            this.lblFontGrid.Size = new System.Drawing.Size(200, 16);
            this.lblFontGrid.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(72, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Grid";
            // 
            // btnFontCaption
            // 
            this.btnFontCaption.Location = new System.Drawing.Point(320, 8);
            this.btnFontCaption.Name = "btnFontCaption";
            this.btnFontCaption.Size = new System.Drawing.Size(24, 16);
            this.btnFontCaption.TabIndex = 3;
            this.btnFontCaption.Click += new System.EventHandler(this.btnFontCaption_Click);
            // 
            // lblFontCaption
            // 
            this.lblFontCaption.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFontCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFontCaption.Location = new System.Drawing.Point(120, 8);
            this.lblFontCaption.Name = "lblFontCaption";
            this.lblFontCaption.Size = new System.Drawing.Size(200, 16);
            this.lblFontCaption.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(72, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Caption";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fonts";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.cboColTypes);
            this.tabPage2.Controls.Add(this.btnInsertFields);
            this.tabPage2.Controls.Add(this.label23);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.btnCheckExpression);
            this.tabPage2.Controls.Add(this.txtExpression);
            this.tabPage2.Controls.Add(this.txtColumnName);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(360, 158);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Add Columns";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(120, 96);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(32, 20);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(16, 96);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(104, 16);
            this.label21.TabIndex = 10;
            this.label21.Text = "Decimal Places";
            // 
            // cboColTypes
            // 
            this.cboColTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColTypes.Items.AddRange(new object[] {
            "Text",
            "Number",
            "Date",
            "Boolean"});
            this.cboColTypes.Location = new System.Drawing.Point(120, 64);
            this.cboColTypes.Name = "cboColTypes";
            this.cboColTypes.Size = new System.Drawing.Size(232, 21);
            this.cboColTypes.TabIndex = 9;
            this.cboColTypes.SelectedIndexChanged += new System.EventHandler(this.cboColTypes_SelectedIndexChanged);
            // 
            // btnInsertFields
            // 
            this.btnInsertFields.ContextMenu = this.contextMenu2;
            this.btnInsertFields.Location = new System.Drawing.Point(320, 32);
            this.btnInsertFields.Name = "btnInsertFields";
            this.btnInsertFields.Size = new System.Drawing.Size(32, 20);
            this.btnInsertFields.TabIndex = 8;
            this.btnInsertFields.Text = "Ins";
            this.toolTip1.SetToolTip(this.btnInsertFields, "Click to Insert Fields");
            this.btnInsertFields.Click += new System.EventHandler(this.btnInsertFields_Click);
            // 
            // contextMenu2
            // 
            this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuInsertFields});
            // 
            // mnuInsertFields
            // 
            this.mnuInsertFields.Enabled = false;
            this.mnuInsertFields.Index = 0;
            this.mnuInsertFields.Text = "Insert Fields";
            this.mnuInsertFields.Click += new System.EventHandler(this.mnuInsertFields_Click);
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(16, 64);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 16);
            this.label23.TabIndex = 7;
            this.label23.Text = "Column Data Type";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "Expression";
            // 
            // btnCheckExpression
            // 
            this.btnCheckExpression.Location = new System.Drawing.Point(240, 104);
            this.btnCheckExpression.Name = "btnCheckExpression";
            this.btnCheckExpression.Size = new System.Drawing.Size(112, 20);
            this.btnCheckExpression.TabIndex = 3;
            this.btnCheckExpression.Text = "Save Expression";
            this.btnCheckExpression.Click += new System.EventHandler(this.btnCheckExpression_Click);
            // 
            // txtExpression
            // 
            this.txtExpression.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpression.Location = new System.Drawing.Point(120, 32);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(200, 20);
            this.txtExpression.TabIndex = 2;
            // 
            // txtColumnName
            // 
            this.txtColumnName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColumnName.Location = new System.Drawing.Point(120, 8);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(232, 20);
            this.txtColumnName.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Column Name";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(360, 158);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Colors";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblSelectionForeColor);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.lblSelectionBackColor);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.lblHeaderForeColor);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.lblHeaderBackColor);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.lblGridLineColor);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.lblForeColor);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Location = new System.Drawing.Point(184, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(168, 144);
            this.panel4.TabIndex = 1;
            // 
            // lblSelectionForeColor
            // 
            this.lblSelectionForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelectionForeColor.Location = new System.Drawing.Point(136, 116);
            this.lblSelectionForeColor.Name = "lblSelectionForeColor";
            this.lblSelectionForeColor.Size = new System.Drawing.Size(24, 16);
            this.lblSelectionForeColor.TabIndex = 13;
            this.lblSelectionForeColor.Click += new System.EventHandler(this.lblSelectionForeColor_Click);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(8, 116);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(128, 16);
            this.label20.TabIndex = 12;
            this.label20.Text = "Selection Fore Color";
            // 
            // lblSelectionBackColor
            // 
            this.lblSelectionBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelectionBackColor.Location = new System.Drawing.Point(136, 94);
            this.lblSelectionBackColor.Name = "lblSelectionBackColor";
            this.lblSelectionBackColor.Size = new System.Drawing.Size(24, 16);
            this.lblSelectionBackColor.TabIndex = 11;
            this.lblSelectionBackColor.Click += new System.EventHandler(this.lblSelectionBackColor_Click);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(8, 94);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(128, 16);
            this.label19.TabIndex = 10;
            this.label19.Text = "Selection Back Color";
            // 
            // lblHeaderForeColor
            // 
            this.lblHeaderForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeaderForeColor.Location = new System.Drawing.Point(136, 74);
            this.lblHeaderForeColor.Name = "lblHeaderForeColor";
            this.lblHeaderForeColor.Size = new System.Drawing.Size(24, 16);
            this.lblHeaderForeColor.TabIndex = 9;
            this.lblHeaderForeColor.Click += new System.EventHandler(this.lblHeaderForeColor_Click);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(8, 74);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 16);
            this.label18.TabIndex = 8;
            this.label18.Text = "Header Fore Color";
            // 
            // lblHeaderBackColor
            // 
            this.lblHeaderBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeaderBackColor.Location = new System.Drawing.Point(136, 52);
            this.lblHeaderBackColor.Name = "lblHeaderBackColor";
            this.lblHeaderBackColor.Size = new System.Drawing.Size(24, 16);
            this.lblHeaderBackColor.TabIndex = 7;
            this.lblHeaderBackColor.Click += new System.EventHandler(this.lblHeaderBackColor_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(8, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(128, 16);
            this.label17.TabIndex = 6;
            this.label17.Text = "Header Back Color";
            // 
            // lblGridLineColor
            // 
            this.lblGridLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGridLineColor.Location = new System.Drawing.Point(136, 30);
            this.lblGridLineColor.Name = "lblGridLineColor";
            this.lblGridLineColor.Size = new System.Drawing.Size(24, 16);
            this.lblGridLineColor.TabIndex = 5;
            this.lblGridLineColor.Click += new System.EventHandler(this.lblGridLineColor_Click);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(128, 16);
            this.label16.TabIndex = 4;
            this.label16.Text = "GridLine Color";
            // 
            // lblForeColor
            // 
            this.lblForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblForeColor.Location = new System.Drawing.Point(136, 8);
            this.lblForeColor.Name = "lblForeColor";
            this.lblForeColor.Size = new System.Drawing.Size(24, 16);
            this.lblForeColor.TabIndex = 3;
            this.lblForeColor.Click += new System.EventHandler(this.lblForeColor_Click);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 16);
            this.label15.TabIndex = 2;
            this.label15.Text = "Fore Color";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblCaptionForeColor);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.lblCaptionBackColor);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.lblBackGroundColor);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.lblBackColor);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.lblAlternateBackColor);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Location = new System.Drawing.Point(0, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(176, 144);
            this.panel3.TabIndex = 0;
            // 
            // lblCaptionForeColor
            // 
            this.lblCaptionForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCaptionForeColor.Location = new System.Drawing.Point(136, 112);
            this.lblCaptionForeColor.Name = "lblCaptionForeColor";
            this.lblCaptionForeColor.Size = new System.Drawing.Size(24, 16);
            this.lblCaptionForeColor.TabIndex = 9;
            this.lblCaptionForeColor.Click += new System.EventHandler(this.lblCaptionForeColor_Click);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(8, 112);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 16);
            this.label14.TabIndex = 8;
            this.label14.Text = "Caption Fore Color";
            // 
            // lblCaptionBackColor
            // 
            this.lblCaptionBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCaptionBackColor.Location = new System.Drawing.Point(136, 88);
            this.lblCaptionBackColor.Name = "lblCaptionBackColor";
            this.lblCaptionBackColor.Size = new System.Drawing.Size(24, 16);
            this.lblCaptionBackColor.TabIndex = 7;
            this.lblCaptionBackColor.Click += new System.EventHandler(this.lblCaptionBackColor_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "Caption Back Color";
            // 
            // lblBackGroundColor
            // 
            this.lblBackGroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackGroundColor.Location = new System.Drawing.Point(136, 64);
            this.lblBackGroundColor.Name = "lblBackGroundColor";
            this.lblBackGroundColor.Size = new System.Drawing.Size(24, 16);
            this.lblBackGroundColor.TabIndex = 5;
            this.lblBackGroundColor.Click += new System.EventHandler(this.lblBackGroundColor_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(8, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "Background Color";
            // 
            // lblBackColor
            // 
            this.lblBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackColor.Location = new System.Drawing.Point(136, 40);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(24, 16);
            this.lblBackColor.TabIndex = 3;
            this.lblBackColor.Click += new System.EventHandler(this.lblBackColor_Click);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 16);
            this.label11.TabIndex = 2;
            this.label11.Text = "Back Color";
            // 
            // lblAlternateBackColor
            // 
            this.lblAlternateBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlternateBackColor.Location = new System.Drawing.Point(136, 16);
            this.lblAlternateBackColor.Name = "lblAlternateBackColor";
            this.lblAlternateBackColor.Size = new System.Drawing.Size(24, 16);
            this.lblAlternateBackColor.TabIndex = 1;
            this.lblAlternateBackColor.Click += new System.EventHandler(this.lblAlternateBackColor_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "Alternate Back Color";
            // 
            // fontDialog1
            // 
            this.fontDialog1.ShowColor = true;
            // 
            // chkSelectAllRows
            // 
            this.chkSelectAllRows.BackColor = System.Drawing.Color.LightSteelBlue;
            this.chkSelectAllRows.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAllRows.ForeColor = System.Drawing.Color.Black;
            this.chkSelectAllRows.Location = new System.Drawing.Point(8, 10);
            this.chkSelectAllRows.Name = "chkSelectAllRows";
            this.chkSelectAllRows.Size = new System.Drawing.Size(144, 20);
            this.chkSelectAllRows.TabIndex = 201;
            this.chkSelectAllRows.Text = "Select All Rows";
            this.chkSelectAllRows.UseVisualStyleBackColor = false;
            this.chkSelectAllRows.Visible = false;
            this.chkSelectAllRows.CheckedChanged += new System.EventHandler(this.chkSelectAllRows_CheckedChanged);
            // 
            // ucGrid
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.chkSelectAllRows);
            this.Controls.Add(this.pnlProperties);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.Name = "ucGrid";
            this.Size = new System.Drawing.Size(648, 368);
            this.LocationChanged += new System.EventHandler(this.ucGrid_Resize);
            this.Resize += new System.EventHandler(this.ucGrid_Resize);
            this.SizeChanged += new System.EventHandler(this.ucGrid_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.pnlProperties.ResumeLayout(false);
            this.tabProperties.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public bool LabTestLookup
		{
			set {bLabTestLookup = value;}
		}

		public bool FitColsToGridWidth
		{
			get{return bFitColsToGridWidth;}
			set{bFitColsToGridWidth=value;}
		}

		//Property to pass the Database Name
		public string DataBase
		{
			get{return null;}
			set{}
		}

		public bool HidePopupMenu
		{			
			set	{bHidePopupMenu=value;}
		}

		public DataTable DataTable
		{
			get{return ds;}
		}

		public DataRow CurrentRow
		{
			get
			{
				BindingManagerBase bm = BindingContext[dataGrid1.DataSource, dataGrid1.DataMember];
				DataRowView drv = (DataRowView) bm.Current;
				return drv.Row;
			}
		}
		/* Name    : GM 
		 * Date    : 19/03/2007
		 * purpose : to select Next record to current record
		 */
		public object[] NextToCurrentRow
		{
			get
			{
				return ds.Rows[dataGrid1.CurrentRowIndex + 1].ItemArray;
			}
		}
		/* Name    : GM 
		 * Date    : 19/03/2007
		 * purpose : to select Previous record to current record
		 */
		public object[] PrevToCurrentRow
		{
			get
			{
				return ds.Rows[dataGrid1.CurrentRowIndex - 1].ItemArray;
			}
		}
		/* Name    : GM 
		 * Date    : 19/03/2007
		 * purpose : to select an Incremented Record.
		 */
		public void setIncRecordFocus(int getRowIndex)
		{
			BindingManagerBase bmb = BindingContext[dataGrid1.DataSource,dataGrid1.DataMember];
			while(bmb.Position != getRowIndex)
			{
				this.dataGrid1.UnSelect(bmb.Position);
				bmb.Position = bmb.Position + 1;
			}
			this.dataGrid1.Select(bmb.Position);
		}
		/* Name    : GM 
		 * Date    : 19/03/2007
		 * purpose : to select a Decremented Record.
		 */
		public void setDecRecordFocus(int getRowIndex)
		{
			BindingManagerBase bmb = BindingContext[dataGrid1.DataSource,dataGrid1.DataMember];
			while(bmb.Position != getRowIndex-1)
			{
				this.dataGrid1.UnSelect(bmb.Position);
				bmb.Position = bmb.Position + 1;
			}
			this.dataGrid1.Select(bmb.Position);
		}
		/* Name    : GM 
		 * Date    : 19/03/2007
		 * purpose : To return current row index
		 */
		public int CurrentRowIndex
		{
			get{return this.dataGrid1.CurrentRowIndex;}
		}
		public string RecordStatus
		{
			get{return sRecordStatus;}
		}
		
		//Specify column names by comma separator and assign this property before calling showgrid method
		public string HideColumns
		{
			get{return sHideColumns;}
			set{sHideColumns= "," +  value + ",";}
		}

		// To Hide Filter panel
		public bool HideFilter
		{
			get{return bHideFilter;}
			set{bHideFilter = value;}
		}


		//Property to Pass the Query
		public string SQL
		{
			get{return sql;}
			set{sql = value;}
		}

		//Property to Pass the Connection String
		public string ConnectionString
		{
			get{return constr;}
			set{constr = value;}
		}

		//Property to get the Grid Title
		public string Title
		{
			get{return strTitle;}
			set{strTitle = value;}
		}
		//Property to get the Grid ID
		public string GridID
		{
			get{return gridID;}
			set{gridID = value;}
		}
		
		//Property - Font Caption
		public Font FontCaption
		{
			get{return dataGrid1.CaptionFont;}
			set
			{
				dataGrid1.CaptionFont = value;
				captionFont = value;
			}
		}

		//Property - Font Grid
		public Font FontGrid
		{
			get{return dataGrid1.Font;}
			set
			{
				dataGrid1.Font = value;
				gridFont = value;
			}
		}

		//Property - Font Header
		public Font FontHeader
		{
			get{return dataGrid1.HeaderFont;}
			set
			{
				dataGrid1.HeaderFont = value;
				headerFont = value;
			}
		}

		//Property - AlternativeBackColor
		public Color AlternativeBackColor
		{
			get{return dataGrid1.AlternatingBackColor;}
			set
			{
				dataGrid1.AlternatingBackColor = value;
				alternativeColor = value;
			}
		}

		//Property - BackColor
		public override Color BackColor
		{
			get{return dataGrid1.BackColor;}
			set
			{
				dataGrid1.BackColor = value;
				backColor = value;
			}
		}
		//Property - Background Color
		public Color BackgroundColor
		{
			get{return dataGrid1.BackgroundColor;}
			set
			{
				dataGrid1.BackgroundColor  = value;
				backGroundColor = value;
			}
		}

		//Property - CaptionBackColor
		public Color CaptionBackColor
		{
			get{return dataGrid1.CaptionBackColor;}
			set
			{
				dataGrid1.CaptionBackColor = value;
				captionBackColor = value;
			}
		}

		//Property - CaptionForeColor
		public Color CaptionForeColor
		{
			get{return dataGrid1.CaptionForeColor;}
			set
			{
				dataGrid1.CaptionForeColor = value;
				captionForeColor = value;
			}
		}

		//Property - ForeColor
		public override Color ForeColor
		{
			get{return dataGrid1.ForeColor ;}
			set
			{
				dataGrid1.ForeColor = value;
				foreColor = value;
			}
		}

		//Property - GridLineColor
		public Color GridLineColor
		{
			get{return dataGrid1.GridLineColor;}
			set
			{
				dataGrid1.GridLineColor  = value;
				gridLineColor = value;
			}
		}

		//Property - HeaderBackColor
		public Color HeaderBackColor
		{
			get{return dataGrid1.HeaderBackColor ;}
			set
			{
				dataGrid1.HeaderBackColor  = value;
				headerBackColor = value;
			}
		}
		//Property - HeaderForeColor
		public Color HeaderForeColor
		{
			get{return dataGrid1.HeaderForeColor;}
			set
			{
				dataGrid1.HeaderForeColor  = value;
				headerForeColor = value;
			}
		}

		//Property - SelectionBackColor
		public Color SelectionBackColor
		{
			get{return dataGrid1.SelectionBackColor;}
			set
			{
				dataGrid1.SelectionBackColor = value;
				selectionBackColor = value;
			}
		}

		//Property - SelectionBackColor
		public Color SelectionForeColor
		{
			get{return dataGrid1.SelectionForeColor;}
			set
			{
				dataGrid1.SelectionForeColor = value;
				selectionForeColor = value;
			}
		}

		/*
		 * Name		: George Joseph
		 * Date		: 16-APR-2005
		 * Purpose	: Property to get the column value
		 *  */

		public string ColValue(int ColumnNumber)
		{
			if (dataGrid1.CurrentRowIndex < 0 )
				return "";
			else
				return dataGrid1[dataGrid1.CurrentRowIndex,ColumnNumber].ToString();
		}

		public string ColData(string ColumnName)
		{
			if (dataGrid1.CurrentRowIndex < 0 )
				return "";
			else
				return CurrentRow[ColumnName].ToString();
		}
		
		//Property to Enable Lookup Window
		public bool EnableLookup
		{
			get{return enableLookup;}
			set{enableLookup  = value;}
		}
		
		public bool ShowStatusColumn
		{
			get{return showStatus;}
			set{showStatus = value;}
		}
		//Property - SelectedRows
		public string SelectedRows
		{
			get{return strSelectedRows;}
			set{strSelectedRows = value;}
		}

		//Property - SelectColumn
		public string SelectColumn
		{
			get{return strSelectColumn;}
			set{strSelectColumn = value;}
		}
		public string[,] setColumnWidth
		{
			set{this.ColumnWidth = value;}
		}
		//Method to show the Grid
		public void ShowGrid()
		{
			try
            {
            //    //Include Select Column as first column ==========================================================
            //    if(enableLookup == true) 
            //    {
            //        //string tmpSQL = SQL.ToUpper();
            //        string tmpSQL = SQL;
            //        tmpSQL = "Select '0' as " + strSelectCaption + ", 'Unselected' as " + strStatusCaption + ", " + tmpSQL.Substring(7);
            //        SQL = tmpSQL;
            //        if(!showStatus)
            //        {
            //            if(sHideColumns == "")
            //                sHideColumns = "," + strStatusCaption + ",";
            //            else
            //                sHideColumns+=  strStatusCaption + ",";
            //        }
            //    }
            //    //=================================================================================================

            //    OracleConnection dbCon = new OracleConnection(constr);
            //    OracleDataAdapter dtAd = new OracleDataAdapter(SQL, dbCon);

            //    sTableName="Check";
            //    dtAd.Fill(ds,sTableName);

            //    ShowGrid(ds,sTableName);
            //    dbCon.Close();
            //    dbCon=null;
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message.ToString());
			}

			//--- Modified by Gerald J on 27-Feb-2007 --------------------
            //if (bLabTestLookup & clsGeneral.showMultipleLookup_LabTests())
            //    cboColumns.TabStop = false;
			//------------------------------------------------------------
		}

		//==============================================================================================
		/* 
		 * Name		: Gerald J.
		 * Date		: 11/Apr/2005 :: 10.30 PM
		 * Purpose	: Method to show the Grid by passing the DataSet as argument.
		 **/

		public void ShowGrid(DataTable objDS, string strTableName)
		{
          //  this.ds = null;
			try
			{
				sTableName=strTableName;
                //DataTable dts = objDS;
				this.ds = objDS;
                if (ds == null) 
				{
					dataGrid1.SetDataBinding(null, sTableName);
					return;
				}
				loadColumns();
				if (cboSearchType.Items.Count>0) cboSearchType.SelectedIndex = 0;
				setProperties();
				dataGrid1.CaptionText = Title;
                // ds.TableName = "PRSALARYGROUP";
                DataSet dsSet = new DataSet();
                //dsSet.Tables.Clear();
                dsSet.Tables.Add(ds);
                dsSet.AcceptChanges();
                dataGrid1.SetDataBinding(dsSet, sTableName);
				dataGrid1.AllowNavigation = false;

				if (!bHidePopupMenu) dataGrid1.ContextMenu=contextMenu1;
				//Commented by AN after getting suggestion from Mr.Ananad.
				//				for(int i=0;i<=ds.Tables[0].Columns.Count-1;i++)
				//				{
				//					if (!bHidePopupMenu) dataGrid1.Controls[i].ContextMenu=contextMenu1;
				//				}

				SizeColumnsToContent(dataGrid1,-1);
				btnSave.Visible = false;
				if (enableLookup)
				{
					setSelectedRows();
					chkSelectAllRows.Visible = true;
					mnuSelectRow.Visible = true;
					mnuUnSelectRow.Visible = true;
					mnuSelectAllRows.Visible = true;
					mnuUnSelectAllRows.Visible = true;
					mnuSelectRowsSep.Visible = true;
				}
				if(gridID !="") btnSave.Visible = true;

				panel1.Visible = (!bHideFilter);
				if (panel1.Visible == false) dataGrid1.Height = this.Height; //dataGrid1.Height + panel1.Height;

				//dvGrid=new DataView(ds.Tables[sTableName]);
                DataView dv = ds.DefaultView;
                dvGrid = dv;
				bool bEnable=RecordCount>0;
				mnuFilterSelection.Enabled	=bEnable;
				mnuExcludeFilter.Enabled	=bEnable;
				mnuCopy.Enabled				=bEnable;
				mnuRemoveFilter.Enabled		=false;
				mnuSelectAll.Enabled		=bEnable;
				mnuUnSelectAll.Enabled		=false;
				DataTable dt;
				//dt = ((DataSet)dataGrid1.DataSource).Tables[sTableName];
                dt = ds;
				dt.RowChanged+= new DataRowChangeEventHandler(GridRow_AfterChange);
				dataGrid1_CurrentCellChanged (dataGrid1,new System.EventArgs());
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message.ToString());
			}
		}

		//Loading the Default Properties of the Control
		private void setProperties()
		{
			FontGrid= new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			FontCaption= new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			FontHeader= new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

			lblFontCaption.Font = FontCaption;
			lblFontCaption.Text = FontCaption.Name;

			lblFontGrid.Font = FontGrid;
			lblFontGrid.Text = FontGrid.Name;

			lblFontHeader.Font = FontHeader;
			lblFontHeader.Text = FontHeader.Name;

			lblAlternateBackColor.BackColor = AlternativeBackColor;
			lblBackColor.BackColor = BackColor;
			lblBackGroundColor.BackColor = BackgroundColor;
			lblCaptionBackColor.BackColor = CaptionBackColor;
			lblCaptionForeColor.BackColor = CaptionForeColor;
			lblForeColor.BackColor = ForeColor;
			lblGridLineColor.BackColor = GridLineColor;
			lblHeaderBackColor.BackColor = HeaderBackColor;
			lblHeaderForeColor.BackColor = HeaderForeColor;
			lblSelectionBackColor.BackColor = SelectionBackColor;
			lblSelectionForeColor.BackColor = SelectionForeColor; 

			dataGrid1.CaptionFont  = lblFontCaption.Font;

			dataGrid1.Font = lblFontGrid.Font ;

			dataGrid1.HeaderFont = lblFontHeader.Font;

			dataGrid1.AlternatingBackColor = lblAlternateBackColor.BackColor;

			dataGrid1.BackColor =lblBackColor.BackColor ;
			dataGrid1.BackgroundColor =lblBackGroundColor.BackColor ;
			dataGrid1.CaptionBackColor = lblCaptionBackColor.BackColor ;
			dataGrid1.CaptionForeColor =lblCaptionForeColor.BackColor ;
			dataGrid1.ForeColor =lblForeColor.BackColor;
			dataGrid1.GridLineColor =lblGridLineColor.BackColor;
			dataGrid1.HeaderBackColor =lblHeaderBackColor.BackColor;
			dataGrid1.HeaderForeColor =lblHeaderForeColor.BackColor;
			dataGrid1.SelectionBackColor =lblSelectionBackColor.BackColor ;
			dataGrid1.SelectionForeColor =lblSelectionForeColor.BackColor; 

		}

		//Loading the Properties
		private void loadProperties()
		{
			dataGrid1.CaptionFont  = lblFontCaption.Font;

			dataGrid1.Font = lblFontGrid.Font ;

			dataGrid1.HeaderFont = lblFontHeader.Font;
			

			dataGrid1.AlternatingBackColor = lblAlternateBackColor.BackColor;
			
			dataGrid1.BackColor =lblBackColor.BackColor ;
			dataGrid1.BackgroundColor =lblBackGroundColor.BackColor ;
			dataGrid1.CaptionBackColor = lblCaptionBackColor.BackColor ;
			dataGrid1.CaptionForeColor =lblCaptionForeColor.BackColor ;
			dataGrid1.ForeColor =lblForeColor.BackColor;
			dataGrid1.GridLineColor =lblGridLineColor.BackColor;
			dataGrid1.HeaderBackColor =lblHeaderBackColor.BackColor;
			dataGrid1.HeaderForeColor =lblHeaderForeColor.BackColor;
			dataGrid1.SelectionBackColor =lblSelectionBackColor.BackColor ;
			dataGrid1.SelectionForeColor =lblSelectionForeColor.BackColor; 
		}

		//Function to load the Columns in the Combo Box for Find
		private void loadColumns()
		{
			int i;
			//Added by James on 27-07-2006,
			//Purpose : when the datasource is changed, the "GridFilterCriteria" and the "txtSearch" is not cleared
			txtSearch.Text = "";
			GridFilterCriteria = "";
			cboColumns.Items.Clear();
			lstColumns.Items.Clear();
			for(i=0;i<=ds.Columns.Count-1;i++)
			{
				if (sHideColumns.ToLower().IndexOf("," + ds.Columns[i].Caption.ToString().ToLower() + ",")<0)
				{
					cboColumns.Items.Add(ds.Columns[i].Caption);
					lstColumns.Items.Add(ds.Columns[i].Caption);
					MenuItem mnu = new MenuItem();
					contextMenu2.MenuItems.Add(ds.Columns[i].Caption,new EventHandler(this.mnuInsertFields_Click)); 
				}
			}
			if (cboColumns.Items.Count>0) cboColumns.SelectedIndex = 0;
		}

		//Resizing the Controls
		private void ucGrid_Resize(object sender, System.EventArgs e)
		{
			dataGrid1.Height = this.Height-panel1.Height-2;
			dataGrid1.Top = 0;
			panel1.Top = dataGrid1.Top+dataGrid1.Height+2;
			dataGrid1.Width = this.Width-2;
			panel1.Width = dataGrid1.Width;
			btnGo.Left = panel1.Width-btnGo.Width-2;
			txtSearch.Width = btnGo.Left-txtSearch.Left-2;
			chkSelectAllRows.Left = dataGrid1.Left + 5;
			chkSelectAllRows.Top = 2;
		}

		//Find the text
		private void btnGo_Click(object sender, System.EventArgs e)
		{
            if (bLabTestLookup) //& clsGeneral.showMultipleLookup_LabTests()
            {
                try
                {
                    DataView dvGrid = new DataView(ds);
                    int iRowPosition = 0;
                    for (int i = 0; i < dvGrid.Table.Rows.Count; i++)
                    {
                        if (dvGrid.Table.Rows[i][cboColumns.Text].ToString().ToLower().StartsWith(txtSearch.Text.Trim().ToLower()))
                        {
                            iRowPosition = i;
                            break;
                        }
                    }

                    for (int i = 0; i < dvGrid.Table.Rows.Count; i++)
                        dataGrid1.UnSelect(i);

                    dataGrid1.Select(iRowPosition);
                    dataGrid1.CurrentRowIndex = iRowPosition;
                    dataGrid1.CurrentCell = new DataGridCell(iRowPosition, 0);
                }
                catch
                {
                    dataGrid1.Select(0);
                }
            }
            else
            {
                if (txtSearch.Text == "")
                    mnuRemoveFilter_Click(sender, new EventArgs());
                else
                {
                    DataTable myTable = ds;
                    string columnSelected = cboColumns.Text;
                    searchData(columnSelected, cboSearchType.Text, txtSearch.Text.ToString());
                    txtSearch.Text = "";
                }
            }
		}

		public void CallFilterGrid(string Fcolumn, string sFtrText)
		{
			if (Fcolumn!="" & sFtrText !="")
			{
				//Modified by Anto on 05/08/2005 
				//cboColumns.Text=Fcolumn;
				//txtSearch.Text=sFtrText;
				//btnGo_Click(btnGo,new EventArgs());

				searchData(Fcolumn, "Starts",sFtrText);
			}
		}

		//Filtering Data
		private void mnuFilter(object sender, System.EventArgs e)
		{
			DataGridCell myCell;
			DataTable myTable;
			myCell = dataGrid1.CurrentCell;
			myTable = ds;
			string columnSelected = myTable.Columns[myCell.ColumnNumber].Caption;
			searchData(columnSelected, "Equals",dataGrid1[myCell.RowNumber,myCell.ColumnNumber].ToString());
		}

		//Searching data
		private void searchData(string columnSelected, string searchCriteria,string strInput)
		{
			this.Cursor=Cursors.WaitCursor;
			string strSQL;
			string RelationalOpr = " = ";
			strInput = strInput.Replace("'","''");			// Name: Gerald J	Date: 02-Jul-2005 :: 11.15 AM
			strSQL = "";

			//Constructing the Query basing on the Filter Type
			if ((ds.Columns[columnSelected].DataType.Name == "String"))
			{
				switch (searchCriteria)
				{
					case "Starts":
						strSQL = "[" + columnSelected + "] Like '" + strInput + "*'";
						break;
					case "Contains":
						strSQL = "[" + columnSelected + "] Like '*" + strInput + "*'";
						break;
					case "Ends":
						strSQL = "[" + columnSelected + "] Like '*" + strInput + "'";
						break;
					case "Equals":
						strSQL = "[" + columnSelected + "] = '" +  strInput + "'";
						break;
					case "Not Equals":
						strSQL = "[" + columnSelected + "] <> '" + strInput + "'";
						break;
				}
			}

			//Converting to the corresponding datatype

			/*
			 * Name	: Joseph Gerald J.
			 * Date	: 25-APR-2005 :: 05.00 PM
			 * Purpose : When the input value is null, FortmatException will occur
			 *				while converting it to Interger, Decimal or Double.
			 * */

			switch (searchCriteria)
			{
				case "Not Equals":
					RelationalOpr = "<>";
					break;
				case "Greater Than":
					RelationalOpr = ">";
					break;
				case "Greater Than or Equal To":
					RelationalOpr = ">=";
					break;
				case "Less Than":
					RelationalOpr = "<";
					break;
				case "Less Than or Equal To":
					RelationalOpr = "<=";
					break;
			}

			switch (ds.Columns[columnSelected].DataType.Name)
			{
				case "Int32":
					if (strInput != "" && isValidNumber(strInput))
						strSQL = "[" + columnSelected + "] " + RelationalOpr + Convert.ToInt32(strInput);
					break;
				case "Int16":
					if (strInput != "" && isValidNumber(strInput))
						strSQL = "[" + columnSelected + "] " + RelationalOpr + Convert.ToInt32(strInput);
					break;
				case "Decimal":
					if (strInput != "" && isValidNumber(strInput))
						strSQL = "[" + columnSelected + "] " + RelationalOpr + Convert.ToDecimal(strInput);
					break;
				case "Double":
					if (strInput != "" && isValidNumber(strInput)) 
						strSQL = "[" + columnSelected + "] " + RelationalOpr + Convert.ToDouble(strInput);
					break;
				case "Date":
					strSQL = "[" + columnSelected + "] " + RelationalOpr + Convert.ToDateTime(strInput);
					break;
				case "DateTime":
					strSQL = "[" + columnSelected + "] " + RelationalOpr + "'" + Convert.ToDateTime(strInput) + "'";
					break;
				case "Boolean":
					strSQL = "[" + columnSelected + "] " + RelationalOpr + Convert.ToBoolean(strInput);
					break;
			}

			//*********************************************************************************
			if(strSQL!="")
			{
				if (GridFilterCriteria !="")
					GridFilterCriteria +=" AND " + strSQL;
				else
					GridFilterCriteria =strSQL;

				dvGrid = new DataView(ds, GridFilterCriteria,  columnSelected,DataViewRowState.CurrentRows);										
				dataGrid1.DataSource = dvGrid ;

				mnuFilterSelection.Enabled		=dvGrid.Count>0;
				mnuExcludeFilter.Enabled		=dvGrid.Count>0;
				mnuSelectAll.Enabled			=dvGrid.Count>0;
				mnuCopy.Enabled					=dvGrid.Count>0;
				mnuRemoveFilter.Enabled			=true;
				dataGrid1_CurrentCellChanged(cboColumns,new EventArgs());
			}
			this.Cursor=Cursors.Default;
		}

		/*
		 * Name		: Joseph Gerald J
		 * Date		: 26-APR-2005 :: 05.00 PM
		 * Purpose	: To find out whether the input value is valid number or not.
		 *  */

		private bool isValidNumber(string strInput)
		{
			try
			{
				Convert.ToDouble(strInput);
				return true;
			}
			catch //(Exception ex)
			{
				return false;
			}
		}

		//**************************************************************************************

		/*
		 *  By Anto on 01-Nov-2006 To Filter by a condition
		 */

		public void filterData(string filterCriteria)
		{
			this.Cursor=Cursors.WaitCursor;
		
//			if(filterCriteria!="")
//			{
//				if (GridFilterCriteria != "")
//					GridFilterCriteria += " AND " + filterCriteria;
//				else
//					GridFilterCriteria = filterCriteria;
//			}
//			else
//				GridFilterCriteria = "";
			GridFilterCriteria = filterCriteria;
			dvGrid = new DataView(ds, GridFilterCriteria,"",DataViewRowState.CurrentRows);
			dataGrid1.DataSource = dvGrid ;

			mnuFilterSelection.Enabled		= dvGrid.Count>0;
			mnuExcludeFilter.Enabled		= dvGrid.Count>0;
			mnuSelectAll.Enabled			= dvGrid.Count>0;
			mnuCopy.Enabled					= dvGrid.Count>0;
			mnuRemoveFilter.Enabled			= true;
			bItemsFiltered					= true;

			dataGrid1_CurrentCellChanged(cboColumns,new EventArgs());

			this.Cursor=Cursors.Default;
		}
		// ******************************************************** //

		//Sorting
		private void sortColumn(string columnSelected)
		{
			dvGrid = new DataView(ds);
			dvGrid.Sort = columnSelected;
			dataGrid1.DataSource = dvGrid;
		}

		//Filtering with Exclude option	
		private void mnuExcludeFilter_Click(object sender, System.EventArgs e)
		{
			DataGridCell myCell;
			DataTable myTable;
			myCell = dataGrid1.CurrentCell;
			myTable = ds;
			string columnSelected = myTable.Columns[myCell.ColumnNumber].Caption;
			searchData(columnSelected, "Not Equals",dataGrid1[myCell.RowNumber,myCell.ColumnNumber].ToString());
		}

		//Remnove Filter
		private void mnuRemoveFilter_Click(object sender, System.EventArgs e)
		{
			GridFilterCriteria="";
			dvGrid = new DataView(ds);
			dataGrid1.DataSource = dvGrid;

			mnuRemoveFilter.Enabled=false;
			mnuFilterSelection.Enabled		= RecordCount>0;
			mnuExcludeFilter.Enabled		= RecordCount>0;
			mnuCopy.Enabled					= RecordCount>0;
			mnuSelectAll.Enabled			= RecordCount>0;
			dataGrid1_CurrentCellChanged(cboColumns,new EventArgs());
		}

		//Printing
		private void mnuPrint_Click(object sender, System.EventArgs e)
		{
		}

		//Converting the grid content to Text-Table format for Copy event
		private string TableToString(DataTable dt)
		{
			string strData = dt.TableName + "\r\n";
			string sep = string.Empty;
			int i;
			int row=0;
			bool selectedRows = false;
			for(i=1;i<=dt.Rows.Count-1;i++)
			{
				if(dataGrid1.IsSelected(i) ==true)
				{
					selectedRows = true;
					break;
				}
			}
			if(selectedRows == false)
			{
				for(i=0;i<=ds.Rows.Count-1;i++) dataGrid1.Select(i);
			}

			if (dt.Rows.Count > 0)
			{
				foreach (DataColumn c in dt.Columns)
				{
					if(c.DataType != typeof(System.Guid) &&
						c.DataType != typeof(System.Byte[]))
					{
						strData += sep + c.ColumnName;
						sep = "\t";
					}
				}
				strData += "\r\n";
				foreach(DataRow r in dt.Rows)
				{
					sep = string.Empty;
					if(dataGrid1.IsSelected(row))
					{
						foreach(DataColumn c in dt.Columns)
						{
							if(c.DataType != typeof(System.Guid) &&
								c.DataType != typeof(System.Byte[]))
							{
								if(!Convert.IsDBNull(r[c.ColumnName]))
									strData += sep +
										r[c.ColumnName].ToString();
								else
									strData += sep + "";
								sep = "\t";
							}
						}
						strData += "\r\n";
					}
					row++;
				}
			}
			else
				strData += "\r\n---> Table was empty!";
			return strData;
		}

		private void mnuAdd_Click(object sender, System.EventArgs e)
		{

		}

		private void mnuUnSelectAll_Click(object sender, System.EventArgs e)
		{
			int i=0;
			for(i=0;i<=ds.Rows.Count-1;i++) dataGrid1.UnSelect(i);			
			bSelectAll=false;
		}

		private void mnuSelectAll_Click(object sender, System.EventArgs e)
		{
			/* ---------------------------------------------
			 * Name : JG
			 * Date : 23-Aug-2005 :: 11.15 AM
			 * Purpose : After filter the records, if we click "Select All" Option, runtime error occurs.
			 *			 In order to avoid this error, we remove the filter.
			 * */

			mnuRemoveFilter_Click (sender,new EventArgs());

			// ---------------------------------------------

			int i=0;
			for(i=0;i<=ds.Rows.Count-1;i++) dataGrid1.Select(i);
			bSelectAll=true;
			mnuUnSelectAll.Enabled=true;
		}

		private void mnuCopy_Click(object sender, System.EventArgs e)
		{
			if (bSelectAll)
			{
				DataTable dt = ds;
				Clipboard.SetDataObject(TableToString (dt), true );
				bSelectAll=false;
				mnuUnSelectAll_Click(sender,e);
			}
			else
				Clipboard.SetDataObject( dataGrid1[dataGrid1.CurrentRowIndex,dataGrid1.CurrentCell.ColumnNumber].ToString(), true);
		}

		private void mnuProperties_Click(object sender, System.EventArgs e)
		{
			pnlProperties.Visible=true;
			pnlProperties.BringToFront();
			if(cboColTypes.SelectedIndex == -1)
			{
				cboColTypes.SelectedIndex = 0;
			}
		}

		private void btnFontCaption_Click(object sender, System.EventArgs e)
		{
			dataGrid1.CaptionFont = selectFont(dataGrid1.CaptionFont); 
			setProperties();
		}

		private Color selectColor(Color col)
		{
			Color returnColor = col;
			colorDialog1.Color = col;
			colorDialog1.ShowDialog();
			returnColor = colorDialog1.Color;
			return returnColor;
		}

		private Font selectFont(Font fnt)
		{
			Font returnFont = fnt;
			fontDialog1.Font = fnt;
			fontDialog1.ShowDialog();
			returnFont = fontDialog1.Font;
			return returnFont;
		}

		private void lblAlternateBackColor_Click(object sender, System.EventArgs e)
		{
			AlternativeBackColor = selectColor(dataGrid1.AlternatingBackColor);
			setProperties();
		}

		private void lblBackColor_Click(object sender, System.EventArgs e)
		{
			BackColor = selectColor(dataGrid1.BackColor);
			setProperties();
		}

		private void lblBackGroundColor_Click(object sender, System.EventArgs e)
		{
			BackgroundColor = selectColor(dataGrid1.BackgroundColor);
			setProperties();
		}

		private void lblCaptionBackColor_Click(object sender, System.EventArgs e)
		{
			CaptionBackColor = selectColor(dataGrid1.CaptionBackColor);
			setProperties();
		}

		private void lblCaptionForeColor_Click(object sender, System.EventArgs e)
		{
			CaptionForeColor= selectColor(dataGrid1.CaptionForeColor);
			setProperties();
		}

		private void lblForeColor_Click(object sender, System.EventArgs e)
		{
			ForeColor = selectColor(dataGrid1.ForeColor);
			setProperties();
		}

		private void lblGridLineColor_Click(object sender, System.EventArgs e)
		{
			GridLineColor = selectColor(dataGrid1.GridLineColor);
			setProperties();
		}

		private void lblHeaderBackColor_Click(object sender, System.EventArgs e)
		{
			HeaderBackColor = selectColor(dataGrid1.HeaderBackColor);
			setProperties();
		}

		private void lblHeaderForeColor_Click(object sender, System.EventArgs e)
		{
			HeaderForeColor= selectColor(dataGrid1.HeaderForeColor);
			setProperties();
		}

		private void lblSelectionBackColor_Click(object sender, System.EventArgs e)
		{
			SelectionBackColor= selectColor(dataGrid1.SelectionBackColor);
			setProperties();
		}

		private void lblSelectionForeColor_Click(object sender, System.EventArgs e)
		{
			SelectionForeColor= selectColor(dataGrid1.SelectionForeColor);
			setProperties();
		}

		private void dataGrid1_Click(object sender, System.EventArgs e)
		{
			pnlProperties.Visible = false;
			dataGrid1.BringToFront();
			chkSelectAllRows.BringToFront();
			mnuUnSelectAll.Enabled=false;
			if (Click != null) Click(sender,e);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			pnlProperties.Visible = false;
			dataGrid1.BringToFront();
		}

		private void btnFontGrid_Click(object sender, System.EventArgs e)
		{
			dataGrid1.Font = selectFont(dataGrid1.Font); 
			setProperties();
		}

		public Int32  RecordCount
		{
			get {return dvGrid.Count;}
		}

		private void btnFontHeader_Click(object sender, System.EventArgs e)
		{
			dataGrid1.HeaderFont = selectFont(dataGrid1.HeaderFont); 
			setProperties();

		}

		private void btnCheckExpression_Click(object sender, System.EventArgs e)
		{
			DataColumn dc = new DataColumn();

			try
			{
				switch (cboColTypes.Text)
				{
					case "Text":
						dc = new DataColumn(txtColumnName.Text,typeof(string),txtExpression.Text);
						break;
					case "Number":
						if(numericUpDown1.Value==0)
						{
							dc = new DataColumn(txtColumnName.Text,typeof(System.Int32),txtExpression.Text);
						}
						else
						{
							dc = new DataColumn(txtColumnName.Text,typeof(System.Double),txtExpression.Text);
						}
						break;
					case "Date":
						dc = new DataColumn(txtColumnName.Text,typeof(string),txtExpression.Text);
						break;
					case "Bool":
						dc = new DataColumn(txtColumnName.Text,typeof(string),txtExpression.Text);
						break;
				}

				//DataColumn dc = new DataColumn(txtColumnName.Text,typeof(string),txtExpression.Text);
				ds.Columns.Add(dc);
				dataGrid1.SetDataBinding(ds,sTableName);
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}

		private void mnuInsertFields_Click(object sender, System.EventArgs e)
		{

			MenuItem mnu = (MenuItem) sender;

			txtExpression.Text += mnu.Text;
		}
		private void insertFields(string strField)
		{
			MessageBox.Show(strField);
		}

		private void btnInsertFields_Click(object sender, System.EventArgs e)
		{
			Point pnt = new Point(0,0);
			contextMenu2.Show(btnInsertFields,pnt);
		}

		private void cboColTypes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cboColTypes.SelectedIndex ==1)
				numericUpDown1.Enabled =true; 
			else numericUpDown1.Enabled =false;
		}

		private void vScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{

		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			DataView dv = new DataView(ds);
			MessageBox.Show(dv.DataViewManager.DataViewSettingCollectionString.ToString());
		}

		//Resize the Column to best fix
		private void SizeColumnsToContent(DataGrid dataGrid, int nRowsToScan) 
		{
			string sDataType="";
			// Create graphics object for measuring widths.
			Graphics Graphics = dataGrid.CreateGraphics();

			// Define new table style.
			DataGridTableStyle tableStyle = new DataGridTableStyle();

			try
			{
				DataTable dataTable = ds; 

				if (-1 == nRowsToScan)
				{
					nRowsToScan = dataTable.Rows.Count;
				}
				else
				{
					// Can only scan rows if they exist.
					nRowsToScan = System.Math.Min(nRowsToScan, 
						dataTable.Rows.Count);
				}

				// Clear any existing table styles.
				dataGrid.TableStyles.Clear();
  
				// Use mapping name that is defined in the data source.
				tableStyle.MappingName = dataTable.TableName;

				// Now create the column styles within the table style.
				DataGridTextBoxColumn columnStyle;
				DataGridBoolColumn columnBolStyle =new DataGridBoolColumn();				

				int iWidth;

				for (int iCurrCol = 0; iCurrCol < dataTable.Columns.Count; 
					iCurrCol++)
				{
					DataColumn dataColumn = dataTable.Columns[iCurrCol];
					sDataType=dataTable.Columns[iCurrCol].DataType.Name.ToString();
					columnStyle = new DataGridTextBoxColumn();

					if (enableLookup && dataColumn.ColumnName==strSelectCaption)
					{
						columnBolStyle.HeaderText = dataColumn.ColumnName;
						columnBolStyle.MappingName = dataColumn.ColumnName;
						columnBolStyle.TrueValue="1";
						columnBolStyle.FalseValue="0";
						columnBolStyle.AllowNull=false;

						// Set width to header text width.
						iWidth = (int)(Graphics.MeasureString(columnBolStyle.HeaderText, 
							dataGrid.HeaderFont).Width);
					}
					else
					{
						columnStyle.TextBox.Enabled = true;
						columnStyle.HeaderText = dataColumn.ColumnName;
						columnStyle.MappingName = dataColumn.ColumnName;

						// Set width to header text width.
						iWidth = (int)(Graphics.MeasureString(columnStyle.HeaderText, 
							dataGrid.HeaderFont).Width);
						columnStyle.ReadOnly=(enableLookup)?true:false;
					}

					// Change width, if data width is wider than header text width.
					// Check the width of the data in the first X rows.
					bool bHideColumn = (sHideColumns.ToLower().IndexOf("," + dataColumn.ColumnName.ToString().ToLower() + "," )>=0);
					if (!bHideColumn)
					{
						DataRow dataRow;
						for (int iRow = 0; iRow < nRowsToScan; iRow++)
						{
							dataRow = dataTable.Rows[iRow];

							if (null != dataRow[dataColumn.ColumnName])
							{
								int iColWidth = (int)(Graphics.MeasureString(dataRow.
									ItemArray[iCurrCol].ToString(),
									dataGrid.Font).Width);
								iWidth = (int)System.Math.Max(iWidth, iColWidth);
							}
						}
						//By Anto on 18-Jan-2006 to set the column with according to the setting
						if(ColumnWidth != null)
						{
							for(int k = 0; k<ColumnWidth.GetLongLength(0); k++)
							{
								if(ColumnWidth[k,0] == dataColumn.ColumnName)
								{
									columnStyle.Width = int.Parse(ColumnWidth[k,1]);
								}
							}
						}
						else
							columnStyle.Width = iWidth + 6;
					}
					else
						columnStyle.Width =0;

					columnStyle.TextBox.DoubleClick+= new EventHandler(this.dataGrid1_DoubleClick);
					columnStyle.TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid1_KeyPress);

					tableStyle.BackColor = Color.WhiteSmoke;
					tableStyle.SelectionBackColor = this.SelectionBackColor;
					tableStyle.SelectionForeColor = this.SelectionForeColor;
					tableStyle.HeaderBackColor = this.HeaderBackColor;
					tableStyle.HeaderForeColor = this.HeaderForeColor;
					tableStyle.HeaderFont = this.FontHeader;
					tableStyle.AlternatingBackColor = this.AlternativeBackColor;
					tableStyle.ForeColor =this.ForeColor;

					columnStyle.NullText="";
					if (!bHidePopupMenu) columnStyle.TextBox.ContextMenu=contextMenu1;
					columnStyle.TextBox.TabStop=false;

					// Add the new column style to the table style.
					if (enableLookup && dataColumn.ColumnName==strSelectCaption)
						tableStyle.GridColumnStyles.Add(columnBolStyle);
					else
						tableStyle.GridColumnStyles.Add(columnStyle);
				}
				// Add the new table style to the data grid.
				fitColumnWidth(tableStyle);

				dataGrid.TableStyles.Add(tableStyle);
				dataGrid.ReadOnly=(enableLookup)?false:true;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
			finally
			{
				Graphics.Dispose();
			}
		}

		//For Lookup Methods =======================================================================================

		// Method to return the Selected Rows
		public string returnSelectedRows(string columnName)
		{
			string strReturn=""; 

			if (enableLookup)
			{
				DataView dv = new DataView(ds);
				dv.RowFilter = strSelectCaption + " = '1'";
				strReturn = "";
				for(int i=0;i<dv.Count;i++)
				{
					strReturn = strReturn + dv[i][columnName].ToString() + Delimiter;
				}
				strSelectedRows = strReturn;
				if (strReturn.Trim()!="") strReturn = strReturn.Substring(0,strReturn.Length-1);
			}
			else
				MessageBox.Show("This is not lookup mode","Payroll");
			return strReturn;
		}

		public void setSelectedRows()
		{
			if (enableLookup)
			{
				string[] sSelectVal= strSelectedRows.Split(Delimiter);
				myChange = true;
				for(int i=0;i<ds.Rows.Count;i++)
				{
					for(int j=0;j<=sSelectVal.GetUpperBound(0);j++)
					{
						if (ds.Rows[i][strSelectColumn].ToString()==sSelectVal[j].ToString())
						{
							ds.Rows[i][strSelectCaption]="1";
							ds.Rows[i][strStatusCaption]="Selected";
							break;
						}
					}
				}
				myChange = false;
			}
			else
				MessageBox.Show("This is not lookup mode","Payroll");
		}

		/*
		 * By Anto on 01-Nov-2006 to select and Un select all the rows
		 */
		public void SelectAllRows()
		{
//			myChange = true;
//			if (enableLookup)
//			{
//				for(int i=0;i<ds.Tables[sTableName].Rows.Count;i++)
//					ds.Tables[sTableName].Rows[i][strSelectCaption]="1";
//			}
//			myChange = false;

			myChange = true;
			if (enableLookup)
			{
				if (bItemsFiltered == false)
				{
					for(int i=0;i<ds.Rows.Count;i++)
						ds.Rows[i][strSelectCaption]="1";
				}
				else
				{
					for (int i=0; i<dvGrid.Count; i++)
						dvGrid[i][strSelectCaption] = "1";

					dataGrid1.DataSource = dvGrid;
				}
			}
			myChange = false;
		}
		public void UnSelectAllRows()
		{
//			myChange = true;
//			if (enableLookup)
//			{
//				for(int i=0;i<ds.Tables[sTableName].Rows.Count;i++)
//					ds.Tables[sTableName].Rows[i][strSelectCaption]="0";
//			}
//			myChange = false;

			myChange = true;
			if (enableLookup)
			{
				if (bItemsFiltered == false)
				{
					for (int i=0; i<ds.Rows.Count; i++)
						ds.Rows[i][strSelectCaption] = "0";
				}
				else
				{
					for (int i=0; i<dvGrid.Count; i++)
						dvGrid[i][strSelectCaption] = "0";

					dataGrid1.DataSource = dvGrid;
				}
			}
			myChange = false;
		}
		// -- *********************************** -- //
		private void dataGrid1_DoubleClick(object sender, System.EventArgs e)
		{
			if (DoubleClick!=null) DoubleClick(sender, e);
			if (EditRec!=null) EditRec(ds.Rows[dataGrid1.CurrentRowIndex]);			
		}

		private void ucGrid_DoubleClick(object sender, System.EventArgs e)
		{
			if (DoubleClick!=null) DoubleClick(sender, e);
		}

		private void dataGrid1_CurrentCellChanged(object sender, System.EventArgs e)
		{
			sRecordStatus ="# " + (dataGrid1.CurrentRowIndex + 1) + "/" + RecordCount.ToString();

			if(RowColChange!=null) RowColChange(sender,e);

			if (sender.GetType().Name!="ComboBox")
			{
				try
				{
					cboColumns.Text=ds.Columns[dataGrid1.CurrentCell.ColumnNumber].ColumnName.ToString();
				}
				catch(Exception){}
			}
			if (dataGrid1.CurrentRowIndex>=0) dataGrid1.Select(dataGrid1.CurrentRowIndex);
		}
		
		private void GridRow_AfterChange( object sender, DataRowChangeEventArgs e )
		{
			if(myChange) return;
			if (RowChanged != null)
			{
				RowChanged(sender,e);  //Invokes the delegates
			}
		}

		private void dataGrid1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(KeyPress!=null) KeyPress(sender,e);
		}		

		//===========================================================================		
		/*
		 * Name		: Anto A
		 * Date		: 05-May-2005 :: 05.25 PM
		 * Purpose	: To focus the cursor in the search text box.
		 */

		public void focusFilter()
		{
			if(bHideFilter)
			{
				bHideFilter = false;
				panel1.Visible = true;
				dataGrid1.Height = dataGrid1.Height - panel1.Height;
			}
			txtSearch.Focus();
		}

		private void fitColumnWidth(DataGridTableStyle tableStyle)
		{
			double TotalColWidth=0;
			double dGridWidth=dataGrid1.Width-75;

			foreach(DataGridColumnStyle dgc in tableStyle.GridColumnStyles)
			{
				TotalColWidth +=dgc.Width;
			}

			if ((TotalColWidth<=dGridWidth+200 & tableStyle.GridColumnStyles.Count<5) | bFitColsToGridWidth)
			{
				foreach(DataGridColumnStyle dgc in tableStyle.GridColumnStyles)
				{
					double nWidth=dgc.Width;
					int iWidth= (int) ((nWidth/TotalColWidth) * dGridWidth) + 10;
					iWidth = (iWidth==10)?0:iWidth;
					dgc.Width=iWidth;
				}
			}
		}

		private void txtSearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			/* ---------------------------------------------------------------------------------------------------
			 * Name : Gerald J
			 * Date : 02-Jul-2005 :: 11.45 AM
			 * Purpose : Not to allow the characters like "* and %". Because if we allow those characters,
			 *			 Error will be raised saying "Invalid String Pattern".
			 * */

			if (e.KeyChar == 37 || e.KeyChar == 42)
			{
				e.Handled = true;
				return;
			}

			// ---------------------------------------------------------------------------------------------------

			if (e.KeyChar==(char)Keys.Return)
			{
				if (txtSearch.Text=="")
					mnuRemoveFilter_Click (sender,new EventArgs());
				else
				{
					btnGo_Click(sender,new EventArgs());

					//--- Modified by Gerald J on 27-Feb-2007 ---------------------
                    //if (bLabTestLookup & clsGeneral.showMultipleLookup_LabTests())
                    //    dataGrid1.Focus();
					//-------------------------------------------------------------
				}
			}
		}

		private void cboColumns_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string[] cboStringOpr=new string[] {"Starts","Contains","Ends","Equals","Not Equals"};
			string[] cboNumOpr=new string[] {"Equals","Not Equals","Greater Than","Greater Than or Equal To",
												"Less Than","Less Than or Equal To"};

			cboSearchType.Items.Clear();
			string[] ColType=new string[] {""};

			if (cboColumns.Text!="")
			{
				if ((ds.Columns[cboColumns.Text].DataType.Name == "String"))
				{
					ColType=cboStringOpr;
					cboSearchType.Width=80;
					txtSearch.Left=cboSearchType.Left+cboSearchType.Width;
					txtSearch.Width=btnGo.Left-txtSearch.Left-5;
				}
				else
				{
					ColType=cboNumOpr;
					cboSearchType.Width=150;
					txtSearch.Left=cboSearchType.Left+cboSearchType.Width;
					txtSearch.Width=btnGo.Left-txtSearch.Left-5;
				}

				for (int i=0; i<ColType.Length; i++)
					cboSearchType.Items.Add(ColType[i]);
			}
			if (cboSearchType.Items.Count>0) cboSearchType.SelectedIndex=0;
		}

		private void dataGrid1_GridKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (KeyPress!=null) KeyPress(sender,e);
		}

		private void chkSelectAllRows_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkSelectAllRows.Checked)
				SelectAllRows();
			else
				UnSelectAllRows();
		}

		private void mnuSelectRow_Click(object sender, System.EventArgs e)
		{
			ds.Rows[dataGrid1.CurrentRowIndex][strSelectCaption]="1";
		}

		private void mnuUnSelectRow_Click(object sender, System.EventArgs e)
		{
			ds.Rows[dataGrid1.CurrentRowIndex][strSelectCaption]="0";
		}

		private void mnuSelectAllRows_Click(object sender, System.EventArgs e)
		{
			chkSelectAllRows.Checked = true;
		}

		private void mnuUnSelectAllRows_Click(object sender, System.EventArgs e)
		{
			chkSelectAllRows.Checked = false;
		}

		//--- Modified by Gerald J on 27-Feb-2007 -----------------------------------------------------
		private void txtSearch_Enter(object sender, System.EventArgs e)
		{
            //if (bLabTestLookup & clsGeneral.showMultipleLookup_LabTests())
            //    cboColumns.Text = ds.Tables[sTableName].Columns[4].ColumnName.ToString(); //Report Name
		}
		//---------------------------------------------------------------------------------------------
	}
}