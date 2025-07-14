using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb; 
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Drawing.Printing;
using Bosco.Utility.Common;

using System.Diagnostics;
//using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Demo
	//namespace Utility.UserControl
{
	public class ucReport : System.Windows.Forms.UserControl
	{
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.ToolBarButton toolBarButton7;
		private System.Windows.Forms.ToolBarButton toolBarButton8;
		private System.Windows.Forms.ToolBarButton toolBarButton9;
		private System.ComponentModel.IContainer components;
		//=======================================================================
		//Properties Variables
		private string strTitle1;
		private string strTitle2;
		private string strTitle3;
		private string strTitle4;
        private string head1;
        private string head2;
        private string head3;
		private string strHeaderDateTime = "";
		private int iIsAmt=0;
		private string strPaperSize="";
		private Font fontTitle1 ;
		private Font fontTitle2;
		private Font fontTitle3;
		private string strSql;
		private int nRows=1;
		private int nCols=1;
		private string strHideColumns="";
		private bool bPrintNetcollection = false;

		private OleDbConnection objOleCon;
		private SqlConnection objSqlCon;
		private OracleConnection objOracle;
		
		private OleDbDataAdapter objOleDa;
		private SqlDataAdapter objSqlDa;
		private OracleDataAdapter objOracleDa;
		
		private DataSet objDs;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.ToolBarButton toolBarButton11;
		private System.Windows.Forms.ToolBarButton toolBarButton12;
		private System.Windows.Forms.ToolBarButton toolBarButton10;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.ToolBarButton toolBarButton13;
		private System.Windows.Forms.ToolBarButton toolBarButton14;
		private System.Windows.Forms.ToolBarButton toolBarButton15;
		private System.Windows.Forms.ToolBarButton toolBarButton16;
		private System.Windows.Forms.ToolBarButton toolBarButton17;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nUDnPageFrom;
		private System.Windows.Forms.NumericUpDown nUDnPageTo;
		public System.Windows.Forms.Button btnStart;
		public System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboPages;
		private System.Windows.Forms.Label lblCaption;

		//private bool bSqlConnection = false;
		private bool bDrawVerticalLines=false;
		private bool bDrawHorizontalLines=false;
		private string columnTotalColumns=""; 
		private int nPages = 1;
		private int nLinesPerPage = 20;
		private int nMaxRows = 1;
		private int iRowSpaceHeight = 1;
		
		//Default -----------------------------------------------------------------
		private const string strDummy="Report Control";
		private Font defTitle1Font  = new Font("Arial", 13.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		private Font defTitle2Font  = new Font("Arial", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		private Font defTitle3Font  = new Font("Arial", 9.00F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

		private Font FontHeaderDate  = new Font("Verdana", 8.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
        private Font FontHeader = new Font("Verdana", 8.00F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		private Font fntTextFont     = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		
		//--------------------------------------------------------------------------


		//---------------- Export ------------------

		private Excel.Application m_Excel;
		private Excel.Workbook m_ExcelWorkbook; 
		// Create a word object that we can manipulate
		private Word.Application Word_App = null;
		private Word.Document Word_doc = null;
		private bool bWord;

		private bool bExpColTotalEnabled = false; // ->Export - Column Total Enabled
		private string[] sExpColTotal;  // ->Export - Column Total
		private string[] sExpGrandTotal; // ->Export - Column Grand Total
		private bool[] bExpProcessColTotal; // ->Export - Process Column Total
		private bool bPropertiesSet = false;
		
		private int iTotalColumnWidth = 0;

		private int iRowHeight = 0;
		private System.Windows.Forms.Panel pnlExport;
		private System.Windows.Forms.ProgressBar progressBar1;
		//----------------------------------

		//Property View 
		private DataView dvProperties;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.Button btnMarCancel;
		public System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.NumericUpDown nUdnWidth;
		private System.Windows.Forms.NumericUpDown nUdnHeight;
		private System.Windows.Forms.Panel pnlMargins;
		private System.Windows.Forms.ToolBarButton toolBarButton18;
		private System.Windows.Forms.Label lblPaperName;
		private System.Windows.Forms.ComboBox cboPaperSizes;
        public System.Windows.Forms.Button btnDelete;

		//set Properties for Connection Type
		private int nConnectionType=0;	//0-OLE DB, 1-SQL Server Connecton, 2-Oracle Connection.

		public ucReport()
		{
			InitializeComponent();
		}

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucReport));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton7 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton8 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton18 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton9 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton11 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton12 = new System.Windows.Forms.ToolBarButton();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.toolBarButton10 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton13 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton14 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton15 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton16 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton17 = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.cboPages = new System.Windows.Forms.ComboBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.pnlExport = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.nUDnPageTo = new System.Windows.Forms.NumericUpDown();
            this.nUDnPageFrom = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMargins = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cboPaperSizes = new System.Windows.Forms.ComboBox();
            this.lblPaperName = new System.Windows.Forms.Label();
            this.btnMarCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.nUdnWidth = new System.Windows.Forms.NumericUpDown();
            this.nUdnHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.pnlExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDnPageTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDnPageFrom)).BeginInit();
            this.pnlMargins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUdnWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUdnHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.printPreviewControl1.AutoZoom = false;
            this.printPreviewControl1.Document = this.printDocument1;
            this.printPreviewControl1.Location = new System.Drawing.Point(8, 40);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(704, 376);
            this.printPreviewControl1.TabIndex = 0;
            this.printPreviewControl1.Zoom = 1;
            // 
            // toolBar1
            // 
            this.toolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.toolBarButton2,
            this.toolBarButton3,
            this.toolBarButton4,
            this.toolBarButton5,
            this.toolBarButton6,
            this.toolBarButton7,
            this.toolBarButton8,
            this.toolBarButton18,
            this.toolBarButton9,
            this.toolBarButton11,
            this.toolBarButton12,
            this.toolBarButton10,
            this.toolBarButton13,
            this.toolBarButton14,
            this.toolBarButton15,
            this.toolBarButton16,
            this.toolBarButton17});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(712, 29);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 0;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Tag = "Print";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.ImageIndex = 2;
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Tag = "Properties";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.toolBarButton3.Tag = "";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.ImageIndex = 1;
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Tag = "Copy";
            this.toolBarButton4.Visible = false;
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton6
            // 
            this.toolBarButton6.ImageIndex = 6;
            this.toolBarButton6.Name = "toolBarButton6";
            this.toolBarButton6.Tag = "XL";
            // 
            // toolBarButton7
            // 
            this.toolBarButton7.ImageIndex = 7;
            this.toolBarButton7.Name = "toolBarButton7";
            this.toolBarButton7.Tag = "Word";
            // 
            // toolBarButton8
            // 
            this.toolBarButton8.ImageIndex = 4;
            this.toolBarButton8.Name = "toolBarButton8";
            this.toolBarButton8.Tag = "Notepad";
            // 
            // toolBarButton18
            // 
            this.toolBarButton18.ImageIndex = 9;
            this.toolBarButton18.Name = "toolBarButton18";
            this.toolBarButton18.Tag = "Margins";
            // 
            // toolBarButton9
            // 
            this.toolBarButton9.Name = "toolBarButton9";
            this.toolBarButton9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton11
            // 
            this.toolBarButton11.Name = "toolBarButton11";
            this.toolBarButton11.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton12
            // 
            this.toolBarButton12.DropDownMenu = this.contextMenu1;
            this.toolBarButton12.ImageIndex = 8;
            this.toolBarButton12.Name = "toolBarButton12";
            this.toolBarButton12.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.toolBarButton12.Tag = "Zoom";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7,
            this.menuItem8,
            this.menuItem9});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Auto";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "500%";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "200%";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "150%";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Checked = true;
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "100%";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
            this.menuItem6.Text = "75%";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 6;
            this.menuItem7.Text = "50%";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 7;
            this.menuItem8.Text = "25%";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 8;
            this.menuItem9.RadioCheck = true;
            this.menuItem9.Text = "10%";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // toolBarButton10
            // 
            this.toolBarButton10.Name = "toolBarButton10";
            this.toolBarButton10.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton13
            // 
            this.toolBarButton13.ImageIndex = 9;
            this.toolBarButton13.Name = "toolBarButton13";
            this.toolBarButton13.Tag = "Z-1";
            this.toolBarButton13.Visible = false;
            // 
            // toolBarButton14
            // 
            this.toolBarButton14.ImageIndex = 10;
            this.toolBarButton14.Name = "toolBarButton14";
            this.toolBarButton14.Tag = "Z-2";
            this.toolBarButton14.Visible = false;
            // 
            // toolBarButton15
            // 
            this.toolBarButton15.ImageIndex = 11;
            this.toolBarButton15.Name = "toolBarButton15";
            this.toolBarButton15.Tag = "Z-3";
            this.toolBarButton15.Visible = false;
            // 
            // toolBarButton16
            // 
            this.toolBarButton16.ImageIndex = 12;
            this.toolBarButton16.Name = "toolBarButton16";
            this.toolBarButton16.Tag = "Z-4";
            this.toolBarButton16.Visible = false;
            // 
            // toolBarButton17
            // 
            this.toolBarButton17.ImageIndex = 13;
            this.toolBarButton17.Name = "toolBarButton17";
            this.toolBarButton17.Tag = "Z-6";
            this.toolBarButton17.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            // 
            // dataGrid1
            // 
            this.dataGrid1.AlternatingBackColor = System.Drawing.Color.White;
            this.dataGrid1.BackColor = System.Drawing.Color.Silver;
            this.dataGrid1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataGrid1.CaptionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.dataGrid1.CaptionFont = new System.Drawing.Font("Tahoma", 8F);
            this.dataGrid1.CaptionForeColor = System.Drawing.Color.White;
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.FlatMode = true;
            this.dataGrid1.ForeColor = System.Drawing.Color.Black;
            this.dataGrid1.GridLineColor = System.Drawing.Color.White;
            this.dataGrid1.HeaderBackColor = System.Drawing.Color.White;
            this.dataGrid1.HeaderForeColor = System.Drawing.Color.Black;
            this.dataGrid1.LinkColor = System.Drawing.Color.DarkSlateBlue;
            this.dataGrid1.Location = new System.Drawing.Point(8, 40);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.Black;
            this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.White;
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGrid1.Size = new System.Drawing.Size(704, 376);
            this.dataGrid1.TabIndex = 2;
            this.dataGrid1.Visible = false;
            // 
            // cboPages
            // 
            this.cboPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPages.Location = new System.Drawing.Point(576, 4);
            this.cboPages.Name = "cboPages";
            this.cboPages.Size = new System.Drawing.Size(128, 21);
            this.cboPages.TabIndex = 3;
            this.cboPages.Visible = false;
            this.cboPages.SelectedIndexChanged += new System.EventHandler(this.cboPages_SelectedIndexChanged);
            // 
            // lblCaption
            // 
            this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.Location = new System.Drawing.Point(488, 104);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(112, 16);
            this.lblCaption.TabIndex = 4;
            this.lblCaption.Text = "label1";
            this.lblCaption.Visible = false;
            // 
            // pnlExport
            // 
            this.pnlExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlExport.Controls.Add(this.progressBar1);
            this.pnlExport.Controls.Add(this.btnCancel);
            this.pnlExport.Controls.Add(this.btnStart);
            this.pnlExport.Controls.Add(this.nUDnPageTo);
            this.pnlExport.Controls.Add(this.nUDnPageFrom);
            this.pnlExport.Controls.Add(this.label3);
            this.pnlExport.Controls.Add(this.label2);
            this.pnlExport.Controls.Add(this.label1);
            this.pnlExport.Location = new System.Drawing.Point(288, 144);
            this.pnlExport.Name = "pnlExport";
            this.pnlExport.Size = new System.Drawing.Size(312, 104);
            this.pnlExport.TabIndex = 5;
            this.pnlExport.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 63);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(296, 8);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(241, 76);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Location = new System.Drawing.Point(169, 76);
            this.btnStart.Name = "btnStart";
            this.btnStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnStart.Size = new System.Drawing.Size(64, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // nUDnPageTo
            // 
            this.nUDnPageTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nUDnPageTo.Location = new System.Drawing.Point(241, 27);
            this.nUDnPageTo.Name = "nUDnPageTo";
            this.nUDnPageTo.Size = new System.Drawing.Size(64, 20);
            this.nUDnPageTo.TabIndex = 4;
            this.nUDnPageTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nUDnPageFrom
            // 
            this.nUDnPageFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nUDnPageFrom.Location = new System.Drawing.Point(96, 28);
            this.nUDnPageFrom.Name = "nUDnPageFrom";
            this.nUDnPageFrom.Size = new System.Drawing.Size(64, 20);
            this.nUDnPageFrom.TabIndex = 3;
            this.nUDnPageFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Page To";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Highlight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(343, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Export";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Page From";
            // 
            // pnlMargins
            // 
            this.pnlMargins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMargins.Controls.Add(this.btnDelete);
            this.pnlMargins.Controls.Add(this.cboPaperSizes);
            this.pnlMargins.Controls.Add(this.lblPaperName);
            this.pnlMargins.Controls.Add(this.btnMarCancel);
            this.pnlMargins.Controls.Add(this.btnOk);
            this.pnlMargins.Controls.Add(this.nUdnWidth);
            this.pnlMargins.Controls.Add(this.nUdnHeight);
            this.pnlMargins.Controls.Add(this.label4);
            this.pnlMargins.Controls.Add(this.lblTitle);
            this.pnlMargins.Controls.Add(this.label6);
            this.pnlMargins.Location = new System.Drawing.Point(288, 144);
            this.pnlMargins.Name = "pnlMargins";
            this.pnlMargins.Size = new System.Drawing.Size(312, 104);
            this.pnlMargins.TabIndex = 6;
            this.pnlMargins.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDelete.Location = new System.Drawing.Point(95, 75);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDelete.Size = new System.Drawing.Size(64, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cboPaperSizes
            // 
            this.cboPaperSizes.Location = new System.Drawing.Point(67, 21);
            this.cboPaperSizes.Name = "cboPaperSizes";
            this.cboPaperSizes.Size = new System.Drawing.Size(237, 21);
            this.cboPaperSizes.TabIndex = 2;
            this.cboPaperSizes.SelectedIndexChanged += new System.EventHandler(this.cboPaperSizes_SelectedIndexChanged);
            // 
            // lblPaperName
            // 
            this.lblPaperName.AutoSize = true;
            this.lblPaperName.Location = new System.Drawing.Point(25, 24);
            this.lblPaperName.Name = "lblPaperName";
            this.lblPaperName.Size = new System.Drawing.Size(35, 13);
            this.lblPaperName.TabIndex = 7;
            this.lblPaperName.Text = "Name";
            // 
            // btnMarCancel
            // 
            this.btnMarCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnMarCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMarCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarCancel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMarCancel.Location = new System.Drawing.Point(241, 75);
            this.btnMarCancel.Name = "btnMarCancel";
            this.btnMarCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMarCancel.Size = new System.Drawing.Size(64, 23);
            this.btnMarCancel.TabIndex = 6;
            this.btnMarCancel.Text = "&Cancel";
            this.btnMarCancel.UseVisualStyleBackColor = false;
            this.btnMarCancel.Click += new System.EventHandler(this.btnMarCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOk.Location = new System.Drawing.Point(169, 75);
            this.btnOk.Name = "btnOk";
            this.btnOk.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOk.Size = new System.Drawing.Size(64, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Save";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // nUdnWidth
            // 
            this.nUdnWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nUdnWidth.Location = new System.Drawing.Point(216, 48);
            this.nUdnWidth.Name = "nUdnWidth";
            this.nUdnWidth.Size = new System.Drawing.Size(88, 20);
            this.nUdnWidth.TabIndex = 4;
            this.nUdnWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nUdnHeight
            // 
            this.nUdnHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nUdnHeight.Location = new System.Drawing.Point(68, 48);
            this.nUdnHeight.Name = "nUdnHeight";
            this.nUdnHeight.Size = new System.Drawing.Size(92, 20);
            this.nUdnHeight.TabIndex = 3;
            this.nUdnHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Width";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTitle.Location = new System.Drawing.Point(1, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(343, 17);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Paper Size";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Height";
            // 
            // ucReport
            // 
            this.Controls.Add(this.pnlMargins);
            this.Controls.Add(this.pnlExport);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.cboPages);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.printPreviewControl1);
            this.Controls.Add(this.dataGrid1);
            this.Name = "ucReport";
            this.Size = new System.Drawing.Size(712, 424);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucReport_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.pnlExport.ResumeLayout(false);
            this.pnlExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDnPageTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDnPageFrom)).EndInit();
            this.pnlMargins.ResumeLayout(false);
            this.pnlMargins.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUdnWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUdnHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
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

		//private double dPayment			= 0;
		//private double dDueCollection	= 0;
		//private double dRefund			= 0;
		private double dAPayment		= 0;
		private string sNetColExpression = "";

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void nitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ucReport));
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton7 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton8 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton18 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton9 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton11 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton12 = new System.Windows.Forms.ToolBarButton();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.toolBarButton10 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton13 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton14 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton15 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton16 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton17 = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.cboPages = new System.Windows.Forms.ComboBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.pnlExport = new System.Windows.Forms.Panel();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.nUDnPageTo = new System.Windows.Forms.NumericUpDown();
			this.nUDnPageFrom = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlMargins = new System.Windows.Forms.Panel();
			this.btnDelete = new System.Windows.Forms.Button();
			this.cboPaperSizes = new System.Windows.Forms.ComboBox();
			this.lblPaperName = new System.Windows.Forms.Label();
			this.btnMarCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.nUdnWidth = new System.Windows.Forms.NumericUpDown();
			this.nUdnHeight = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.pnlExport.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nUDnPageTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nUDnPageFrom)).BeginInit();
			this.pnlMargins.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nUdnWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nUdnHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// printPreviewControl1
			// 
			this.printPreviewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.printPreviewControl1.AutoZoom = false;
			this.printPreviewControl1.Document = this.printDocument1;
			this.printPreviewControl1.Location = new System.Drawing.Point(8, 40);
			this.printPreviewControl1.Name = "printPreviewControl1";
			this.printPreviewControl1.Size = new System.Drawing.Size(704, 376);
			this.printPreviewControl1.TabIndex = 0;
			this.printPreviewControl1.Zoom = 1;
			// 
			// toolBar1
			// 
			this.toolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButton1,
																						this.toolBarButton2,
																						this.toolBarButton3,
																						this.toolBarButton4,
																						this.toolBarButton5,
																						this.toolBarButton6,
																						this.toolBarButton7,
																						this.toolBarButton8,
																						this.toolBarButton18,
																						this.toolBarButton9,
																						this.toolBarButton11,
																						this.toolBarButton12,
																						this.toolBarButton10,
																						this.toolBarButton13,
																						this.toolBarButton14,
																						this.toolBarButton15,
																						this.toolBarButton16,
																						this.toolBarButton17});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(712, 29);
			this.toolBar1.TabIndex = 1;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.Tag = "Print";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.ImageIndex = 2;
			this.toolBarButton2.Tag = "Properties";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.toolBarButton3.Tag = "";
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.ImageIndex = 1;
			this.toolBarButton4.Tag = "Copy";
			this.toolBarButton4.Visible = false;
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.ImageIndex = 6;
			this.toolBarButton6.Tag = "XL";
			// 
			// toolBarButton7
			// 
			this.toolBarButton7.ImageIndex = 7;
			this.toolBarButton7.Tag = "Word";
			// 
			// toolBarButton8
			// 
			this.toolBarButton8.ImageIndex = 4;
			this.toolBarButton8.Tag = "Notepad";
			// 
			// toolBarButton18
			// 
			this.toolBarButton18.ImageIndex = 9;
			this.toolBarButton18.Tag = "Margins";
			// 
			// toolBarButton9
			// 
			this.toolBarButton9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton11
			// 
			this.toolBarButton11.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton12
			// 
			this.toolBarButton12.DropDownMenu = this.contextMenu1;
			this.toolBarButton12.ImageIndex = 8;
			this.toolBarButton12.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.toolBarButton12.Tag = "Zoom";
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem5,
																						 this.menuItem6,
																						 this.menuItem7,
																						 this.menuItem8,
																						 this.menuItem9});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Auto";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "500%";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "200%";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "150%";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Checked = true;
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "100%";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 5;
			this.menuItem6.Text = "75%";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 6;
			this.menuItem7.Text = "50%";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 7;
			this.menuItem8.Text = "25%";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 8;
			this.menuItem9.RadioCheck = true;
			this.menuItem9.Text = "10%";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// toolBarButton10
			// 
			this.toolBarButton10.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton13
			// 
			this.toolBarButton13.ImageIndex = 9;
			this.toolBarButton13.Tag = "Z-1";
			this.toolBarButton13.Visible = false;
			// 
			// toolBarButton14
			// 
			this.toolBarButton14.ImageIndex = 10;
			this.toolBarButton14.Tag = "Z-2";
			this.toolBarButton14.Visible = false;
			// 
			// toolBarButton15
			// 
			this.toolBarButton15.ImageIndex = 11;
			this.toolBarButton15.Tag = "Z-3";
			this.toolBarButton15.Visible = false;
			// 
			// toolBarButton16
			// 
			this.toolBarButton16.ImageIndex = 12;
			this.toolBarButton16.Tag = "Z-4";
			this.toolBarButton16.Visible = false;
			// 
			// toolBarButton17
			// 
			this.toolBarButton17.ImageIndex = 13;
			this.toolBarButton17.Tag = "Z-6";
			this.toolBarButton17.Visible = false;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// dataGrid1
			// 
			this.dataGrid1.AlternatingBackColor = System.Drawing.Color.White;
			this.dataGrid1.BackColor = System.Drawing.Color.Silver;
			this.dataGrid1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dataGrid1.CaptionBackColor = System.Drawing.Color.DarkSlateBlue;
			this.dataGrid1.CaptionFont = new System.Drawing.Font("Tahoma", 8F);
			this.dataGrid1.CaptionForeColor = System.Drawing.Color.White;
			this.dataGrid1.CaptionVisible = false;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.FlatMode = true;
			this.dataGrid1.ForeColor = System.Drawing.Color.Black;
			this.dataGrid1.GridLineColor = System.Drawing.Color.White;
			this.dataGrid1.HeaderBackColor = System.Drawing.Color.White;
			this.dataGrid1.HeaderForeColor = System.Drawing.Color.Black;
			this.dataGrid1.LinkColor = System.Drawing.Color.DarkSlateBlue;
			this.dataGrid1.Location = new System.Drawing.Point(8, 40);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.Black;
			this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.White;
			this.dataGrid1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
			this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
			this.dataGrid1.Size = new System.Drawing.Size(704, 376);
			this.dataGrid1.TabIndex = 2;
			this.dataGrid1.Visible = false;
			// 
			// cboPages
			// 
			this.cboPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPages.Location = new System.Drawing.Point(576, 4);
			this.cboPages.Name = "cboPages";
			this.cboPages.Size = new System.Drawing.Size(128, 21);
			this.cboPages.TabIndex = 3;
			this.cboPages.Visible = false;
			this.cboPages.SelectedIndexChanged += new System.EventHandler(this.cboPages_SelectedIndexChanged);
			// 
			// lblCaption
			// 
			this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCaption.Location = new System.Drawing.Point(488, 80);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(112, 16);
			this.lblCaption.TabIndex = 4;
			this.lblCaption.Text = "label1";
			this.lblCaption.Visible = false;
			// 
			// pnlExport
			// 
			this.pnlExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlExport.Controls.Add(this.progressBar1);
			this.pnlExport.Controls.Add(this.btnCancel);
			this.pnlExport.Controls.Add(this.btnStart);
			this.pnlExport.Controls.Add(this.nUDnPageTo);
			this.pnlExport.Controls.Add(this.nUDnPageFrom);
			this.pnlExport.Controls.Add(this.label3);
			this.pnlExport.Controls.Add(this.label2);
			this.pnlExport.Controls.Add(this.label1);
			this.pnlExport.Location = new System.Drawing.Point(288, 144);
			this.pnlExport.Name = "pnlExport";
			this.pnlExport.Size = new System.Drawing.Size(312, 104);
			this.pnlExport.TabIndex = 5;
			this.pnlExport.Visible = false;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(8, 63);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(296, 8);
			this.progressBar1.TabIndex = 7;
			this.progressBar1.Visible = false;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCancel.Location = new System.Drawing.Point(241, 76);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnCancel.Size = new System.Drawing.Size(64, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnStart
			// 
			this.btnStart.BackColor = System.Drawing.SystemColors.Control;
			this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStart.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnStart.Location = new System.Drawing.Point(169, 76);
			this.btnStart.Name = "btnStart";
			this.btnStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnStart.Size = new System.Drawing.Size(64, 23);
			this.btnStart.TabIndex = 5;
			this.btnStart.Text = "&Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// nUDnPageTo
			// 
			this.nUDnPageTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nUDnPageTo.Location = new System.Drawing.Point(241, 27);
			this.nUDnPageTo.Name = "nUDnPageTo";
			this.nUDnPageTo.Size = new System.Drawing.Size(64, 20);
			this.nUDnPageTo.TabIndex = 4;
			this.nUDnPageTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// nUDnPageFrom
			// 
			this.nUDnPageFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nUDnPageFrom.Location = new System.Drawing.Point(96, 28);
			this.nUDnPageFrom.Name = "nUDnPageFrom";
			this.nUDnPageFrom.Size = new System.Drawing.Size(64, 20);
			this.nUDnPageFrom.TabIndex = 3;
			this.nUDnPageFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(184, 29);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Page To";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.Highlight;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label2.Location = new System.Drawing.Point(1, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(343, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Export";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Page From";
			// 
			// pnlMargins
			// 
			this.pnlMargins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMargins.Controls.Add(this.btnDelete);
			this.pnlMargins.Controls.Add(this.cboPaperSizes);
			this.pnlMargins.Controls.Add(this.lblPaperName);
			this.pnlMargins.Controls.Add(this.btnMarCancel);
			this.pnlMargins.Controls.Add(this.btnOk);
			this.pnlMargins.Controls.Add(this.nUdnWidth);
			this.pnlMargins.Controls.Add(this.nUdnHeight);
			this.pnlMargins.Controls.Add(this.label4);
			this.pnlMargins.Controls.Add(this.lblTitle);
			this.pnlMargins.Controls.Add(this.label6);
			this.pnlMargins.Location = new System.Drawing.Point(288, 144);
			this.pnlMargins.Name = "pnlMargins";
			this.pnlMargins.Size = new System.Drawing.Size(312, 104);
			this.pnlMargins.TabIndex = 6;
			this.pnlMargins.Visible = false;
			// 
			// btnDelete
			// 
			this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
			this.btnDelete.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDelete.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDelete.Location = new System.Drawing.Point(95, 75);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnDelete.Size = new System.Drawing.Size(64, 23);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// cboPaperSizes
			// 
			this.cboPaperSizes.Location = new System.Drawing.Point(67, 21);
			this.cboPaperSizes.Name = "cboPaperSizes";
			this.cboPaperSizes.Size = new System.Drawing.Size(237, 21);
			this.cboPaperSizes.TabIndex = 2;
			this.cboPaperSizes.SelectedIndexChanged += new System.EventHandler(this.cboPaperSizes_SelectedIndexChanged);
			// 
			// lblPaperName
			// 
			this.lblPaperName.AutoSize = true;
			this.lblPaperName.Location = new System.Drawing.Point(25, 24);
			this.lblPaperName.Name = "lblPaperName";
			this.lblPaperName.Size = new System.Drawing.Size(34, 16);
			this.lblPaperName.TabIndex = 7;
			this.lblPaperName.Text = "Name";
			// 
			// btnMarCancel
			// 
			this.btnMarCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnMarCancel.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnMarCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMarCancel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnMarCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnMarCancel.Location = new System.Drawing.Point(241, 75);
			this.btnMarCancel.Name = "btnMarCancel";
			this.btnMarCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnMarCancel.Size = new System.Drawing.Size(64, 23);
			this.btnMarCancel.TabIndex = 6;
			this.btnMarCancel.Text = "&Cancel";
			this.btnMarCancel.Click += new System.EventHandler(this.btnMarCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnOk.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnOk.Location = new System.Drawing.Point(169, 75);
			this.btnOk.Name = "btnOk";
			this.btnOk.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.btnOk.Size = new System.Drawing.Size(64, 23);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "&Save";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// nUdnWidth
			// 
			this.nUdnWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nUdnWidth.Location = new System.Drawing.Point(216, 48);
			this.nUdnWidth.Name = "nUdnWidth";
			this.nUdnWidth.Size = new System.Drawing.Size(88, 20);
			this.nUdnWidth.TabIndex = 4;
			this.nUdnWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// nUdnHeight
			// 
			this.nUdnHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nUdnHeight.Location = new System.Drawing.Point(68, 48);
			this.nUdnHeight.Name = "nUdnHeight";
			this.nUdnHeight.Size = new System.Drawing.Size(92, 20);
			this.nUdnHeight.TabIndex = 3;
			this.nUdnHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(176, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Width";
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.SystemColors.Highlight;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lblTitle.Location = new System.Drawing.Point(1, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(343, 17);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Paper Size";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(25, 51);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 16);
			this.label6.TabIndex = 0;
			this.label6.Text = "Height";
			// 
			// ucReport
			// 
			this.Controls.Add(this.pnlMargins);
			this.Controls.Add(this.pnlExport);
			this.Controls.Add(this.lblCaption);
			this.Controls.Add(this.cboPages);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.printPreviewControl1);
			this.Controls.Add(this.dataGrid1);
			this.Name = "ucReport";
			this.Size = new System.Drawing.Size(712, 424);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucReport_Paint);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.pnlExport.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nUDnPageTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nUDnPageFrom)).EndInit();
			this.pnlMargins.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.nUdnWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nUdnHeight)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ucReport_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			//			fontTitle1 = this.Font;
			//			fontTitle2 = this.Font;
			//			fontTitle3 = this.Font;
		}

		#region Properties for ucReport
		//Property to get the Title1
		public string Title1
		{
			get
			{
				return strTitle1;
			}
			set
			{
				strTitle1 = value;
			}
		}
		public string HeaderDateTime
		{
			get
			{
				return strHeaderDateTime;
			}
			set
			{
				strHeaderDateTime = value;
			}
		}

		//Property to get the Title2
		public string Title2
		{
			get
			{
				return strTitle2;
			}
			set
			{
				strTitle2 = value;
			}
		}

		//Property to get the Title3
		public string Title3
		{
			get
			{
				return strTitle3;
			}
			set
			{
				strTitle3 = value;
			}
		}
		//Property to get the title4
		public string Title4
		{
			get
			{
				return strTitle4;
			}
			set
			{
				strTitle4 = value;
			}
		}
        public string Head1
        {
            get
            {
                return head1;
            }
            set
            {
                head1 = value;
            }
        }
        public string Head2
        {
            get
            {
                return head2;
            }
            set
            {
                head2 = value;
            }
        }
        public string Head3
        {
            get
            {
                return head3;
            }
            set
            {
                head3 = value;
            }
        }
       
		public int IsAmount
		{
			get
			{
				return iIsAmt;
			}
			set
			{
				iIsAmt = value;
			}
		}
		//Property for Paper Size
		public string PaperSize
		{
			get
			{
				return this.strPaperSize;
			}
			set
			{
				this.strPaperSize = value;
			}
		}

		//Property to get the Font for Title1
		public Font FontTitle1
		{
			get
			{
				return fontTitle1;
			}
			set
			{
				fontTitle1 = value;
			}
		}
		//Property to get the Font for Title2
		public Font FontTitle2
		{
			get
			{
				return fontTitle2;
			}
			set
			{
				fontTitle2 = value;
			}
		}

		//Property to get the Font for Title1
		public Font FontTitle3
		{
			get
			{
				return fontTitle3;
			}
			set
			{
				fontTitle3 = value;
			}
		}

		//DB Connection(OLE, SQL Server, Oracel)-----------------------------------------------------------------------------------------
		//Property for OLE Connection
		public OleDbConnection   OLEConnection
		{
			get
			{
				return objOleCon ;
			}
			set
			{
				nConnectionType = 0;//OLE Connection
				objOleCon = value;
			}
		}

		//Property for SQL Connection
		public SqlConnection  SQLConnection
		{
			get
			{
				return objSqlCon;
			}
			set
			{
				nConnectionType = 1;	//SQL Server Connection
				objSqlCon = value;
			}
		}

		//Property for Oracle Connection
		public OracleConnection OracleConnection
		{
			get
			{
				return objOracle;
			}
			set
			{
				nConnectionType = 2;	//Oracle Connection
				objOracle = value;
			}
		}
		//-----------------------------------------------------------------------------------------------------------

		//Property to get the Column Total Columns
		public string ColumnTotal
		{
			get
			{
				return columnTotalColumns ;
			}
			set
			{
				columnTotalColumns = value;
			}
		}

		//Property for SQL
		public string   SQL
		{
			get
			{
				return strSql;
			}
			set
			{
				strSql = value;
			}
		}

		//Property for Rows
		public int   Rows
		{
			get
			{
				return nRows;
			}
			set
			{
				nRows = value;
			}
		}
		//Property for Cols
		public int   Cows
		{
			get
			{
				return nCols;
			}
			set
			{
				nCols = value;
			}
		}

		//Property for HideColumns
		public string HideColumns
		{
			get
			{
				return strHideColumns;
			}
			set
			{
				strHideColumns = value;
			}
		}
		//Property for Horizontal Lines
		public bool DrawHorizontalLines
		{
			get
			{
				return bDrawHorizontalLines;
			}
			set
			{
				bDrawHorizontalLines = value;
			}
		}
		//Property for Vertical Lines
		public bool DrawVerticalLines
		{
			get
			{
				return bDrawVerticalLines ;
			}
			set
			{
				bDrawVerticalLines = value;
			}
		}
		//Property for Printing Net Collection
		public bool PrintNetCollection
		{
			get{return bPrintNetcollection;}
			set{bPrintNetcollection = value;}
		}
		
		//Property for Net Collection expression
		public string NetCollectionExpression
		{
			get{return sNetColExpression;}
			set{sNetColExpression = value;}
		}
		//Propery for formating columns
		public DataView PropertyView
		{
			get{return dvProperties;}
			set{dvProperties = value;}
		}
		//Propery for printing rows with space
		public int RowSpaceHeight
		{
			get{return iRowSpaceHeight;}
			set{iRowSpaceHeight = value;}
		}
		#endregion

		#region Methods for ucReport
		public void ShowReport(DataTable newDataTable)
		{
			bPropertiesSet = false;
			PaperSize pkCustom = new PaperSize("First custom size",1000,printDocument1.DefaultPageSettings.PaperSize.Height); 
			printDocument1.DefaultPageSettings.PaperSize=pkCustom;
			printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = pkCustom;
			setPaperSize();

			objDs = new DataSet();
			DataTable thisTable = newDataTable.Copy();
			objDs.Tables.Add(thisTable);
			nMaxRows = objDs.Tables[0].Rows.Count;
			try
			{
				SizeColumnsToContent(dataGrid1,-1); 
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message.ToString());
			}
			calculatePages();

		}

		private void setPaperSize()
		{
			foreach(PaperSize size in printDocument1.DefaultPageSettings.PrinterSettings.PaperSizes)
			{
				if(size.PaperName == this.strPaperSize)
					printDocument1.DefaultPageSettings.PaperSize = size;
			}
		}
		
		public void ShowReport()
		{
			bPropertiesSet = false;
			PaperSize pkCustom = new PaperSize("First custom size",1000,printDocument1.DefaultPageSettings.PaperSize.Height); 
			printDocument1.DefaultPageSettings.PaperSize=pkCustom;
			printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = pkCustom;
			setPaperSize();
			createDataSet();
		}
		//Create the dataset for Ole as well as Sql type of Connections
		private void createDataSet()
		{ 
            DataTable dtTable = null;
			objDs = new DataSet();
			switch(nConnectionType)
			{
				case 0:		//OLE DB Connection
					objOleDa = new OleDbDataAdapter(SQL,objOleCon);
					objOleDa.Fill(objDs,"Report");
					break;
				case 1:		//SQL Server Connection
                    //objSqlDa = new SqlDataAdapter(SQL,objSqlCon);
                    //objSqlDa.Fill(objDs,"Report");
                    ResultArgs resultArgs = null;
                    using (DataManager dataManager = new DataManager())
                    {
                        resultArgs = dataManager.FetchData(DataSource.DataTable, SQL);
                        dtTable  = resultArgs.DataSource.Table;
                        dtTable.TableName = "Report";
                    }
					break;
				case 2:		//Oracle Connection
					objOracleDa = new OracleDataAdapter(SQL,objOracle);
					objOracleDa.Fill(objDs,"Report");
					break;
			}
            objDs.Tables.Add(dtTable);
			dataGrid1.SetDataBinding(objDs,"Report");
			nMaxRows = objDs.Tables[0].Rows.Count;
			try
			{
				SizeColumnsToContent(dataGrid1,-1); 
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message.ToString());
			}
			calculatePages();
		}
		public void setReportFont(string sfontBody,int iBodyfontsize,string scoltitle,int icolSize,int bodyFontStyle,int ipagesetting)
		{
			if(bodyFontStyle==1)
				this.Font= new Font(sfontBody, iBodyfontsize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			else
				this.Font= new Font(sfontBody, iBodyfontsize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			
			
			this.fntTextFont=new Font(scoltitle, icolSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			 
			if (ipagesetting==1)
			{
				this.printDocument1.PrinterSettings.DefaultPageSettings.Landscape= true;
				printPreviewControl1.Document.DefaultPageSettings.Landscape = true;
			}
			
		}
		//Added by PE on 16-08-2008
		//To incorporate the custom style for the report details fonts
		public void setReportFont(string sfontBody,int iBodyfontsize,string scoltitle,int icolSize,int bodyFontStyle,int ipagesetting,int style)
		{
			if(style ==1)
			{
				this.Font= new Font(sfontBody, iBodyfontsize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.fntTextFont=new Font(scoltitle, iBodyfontsize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			}
			else if(style == 2)
			{
				this.Font= new Font(sfontBody, iBodyfontsize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.fntTextFont=new Font(scoltitle, iBodyfontsize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			}
			else if(style == 3)
			{
				this.Font= new Font(sfontBody, iBodyfontsize, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.fntTextFont=new Font(scoltitle, iBodyfontsize, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			}
			
			if (ipagesetting==1)
			{
				this.printDocument1.PrinterSettings.DefaultPageSettings.Landscape= true;
				printPreviewControl1.Document.DefaultPageSettings.Landscape = true;
			}
			
		}

		//To Calculate the Pages
		private void calculatePages()
		{
			//To Calculate the Text Height/Width for the  Fonts
			Graphics grp = dataGrid1.CreateGraphics();
			int nRowHeight =(int)(grp.MeasureString("Grid Control",fntTextFont).Height)+iRowSpaceHeight+2;
			// Name     : James 15-05-06
			// purpose  : when landscape is chosen in printer properties the number of pages would vary
			int pageHeight = 0;
			if(printDocument1.PrinterSettings.DefaultPageSettings.Landscape == false)
				pageHeight = printDocument1.DefaultPageSettings.PaperSize.Height - printDocument1.DefaultPageSettings.Margins.Top-printDocument1.DefaultPageSettings.Margins.Bottom;
			else
				pageHeight = printDocument1.DefaultPageSettings.PaperSize.Width - printDocument1.DefaultPageSettings.Margins.Top-printDocument1.DefaultPageSettings.Margins.Bottom;
			
			if(FontTitle1==null) FontTitle1 = defTitle1Font;
			if(FontTitle2==null) FontTitle2 = defTitle2Font;
			if(FontTitle3==null) FontTitle3 = defTitle3Font;
			pageHeight = pageHeight - (int) grp.MeasureString(Title1,FontTitle1).Height - (int)grp.MeasureString(Title2,FontTitle2).Height - (int)grp.MeasureString(Title3,FontTitle3).Height; 

			int i;
			nLinesPerPage = (int)(pageHeight/nRowHeight);
			nPages = (int)(nMaxRows/nLinesPerPage);
			if(nLinesPerPage*nPages !=nMaxRows)
			{
				nPages++;
			}			
			cboPages.Items.Clear(); 
			for(i=1;i<=nPages;i++)
			{
				cboPages.Items.Add("Page "+i.ToString() + " of " + nPages.ToString());	
			}
			if(nPages>1)
			{
				cboPages.Visible=true;
			}
			else
			{
				cboPages.Visible=false;
			}

			//Clear Print document object
			if (nPages<=0) printPreviewControl1.InvalidatePreview(); 

			if (nPages>0) cboPages.SelectedIndex = 0; 
		}

		//Resize the Column to best fix
		private void SizeColumnsToContent(DataGrid dataGrid, int nRowsToScan) 
		{
			// Create graphics object for measuring widths.
			Graphics Graphics = dataGrid.CreateGraphics();

			//colAlignment[objDs.Tables[0].Columns.Count] = HorizontalAlignment.Left ;

			// Define new table style.
			DataGridTableStyle tableStyle = new DataGridTableStyle();

			try
			{
				DataTable dataTable = objDs.Tables[0]; 
				
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
				int iWidth;
				iTotalColumnWidth = 0; //inserted by Pe on 10-02-2006 for Payroll
				for (int iCurrCol = 0; iCurrCol < dataTable.Columns.Count; 
					iCurrCol++)
				{
					DataColumn dataColumn = dataTable.Columns[iCurrCol];
					columnStyle = new DataGridTextBoxColumn();
					if (dataTable.Columns[iCurrCol].DataType==typeof(System.Int32)||  dataTable.Columns[iCurrCol].DataType==typeof(System.Int64) ||dataTable.Columns[iCurrCol].DataType==typeof(System.Int16) || dataTable.Columns[iCurrCol].DataType==typeof(System.Decimal) ||dataTable.Columns[iCurrCol].DataType==typeof(System.Double))
					{
						columnStyle.Alignment = HorizontalAlignment.Right;
					}
					/* -------------------------------------------------------------------------------------------
					 * Name : Gerald J
					 * Date	: 02-Aug-2005 :: 05.00 PM
					 * Purpose : Since we are taking the amounts as characters,
					 *			 the alignments are changed while printing the report. In order to avoid it
					 *			 we manually setting the alignment for all the amount fields.
					 * */

					if (dataTable.Columns[iCurrCol].Caption == "Charges" | dataTable.Columns[iCurrCol].Caption == "Payment" |
						dataTable.Columns[iCurrCol].Caption == "Discount" | dataTable.Columns[iCurrCol].Caption == "Due" |
						dataTable.Columns[iCurrCol].Caption == "Due Coll." | dataTable.Columns[iCurrCol].Caption == "Refunds" |
						dataTable.Columns[iCurrCol].Caption == "Advance" | dataTable.Columns[iCurrCol].Caption == "Adv. Adj." |
						dataTable.Columns[iCurrCol].Caption == "Adv. Refunds")
						columnStyle.Alignment = HorizontalAlignment.Right;
					
					//Added by Pe on 15-03-2006 for the same purpose mentioned above for the Payroll 
					if (clsGeneral.PAYROLL_RPT_COL_TOTAL_FIELDS != "")
					{
						string[]  sFieldsAndTypes   = clsGeneral.PAYROLL_RPT_COL_TOTAL_FIELDS.Split('@');
						for(int iRec_Count = 0; iRec_Count < sFieldsAndTypes.Length  ;iRec_Count++)
						{
							try
							{
								if(sFieldsAndTypes[iRec_Count].ToString()!= "")
								{
									if(sFieldsAndTypes[iRec_Count].ToString().ToUpper() == dataTable.Columns[iCurrCol].Caption.ToUpper())
									{
										columnStyle.Alignment = HorizontalAlignment.Right;
										break;
									}	
								}
							}
							catch
							{
								
							}
						}
					}
					// -------------------------------------------------------------------------------------------
					
					//by Anto A on 20-Jan-2006 to Fix the alignment according to the Dataview set in "PropertyView" Property
					if(dvProperties != null)
					{
						dvProperties.RowFilter = "FIELDNAME = '" + dataTable.Columns[iCurrCol].Caption + "'";
						if(dvProperties.Count > 0)
						{
							string sAlignment = dvProperties[0]["Alignment"].ToString();
							if(sAlignment.ToUpper() == "R")
								columnStyle.Alignment = HorizontalAlignment.Right;
							else if(sAlignment.ToUpper() == "C")
								columnStyle.Alignment = HorizontalAlignment.Center;
							else
								columnStyle.Alignment = HorizontalAlignment.Left;
						}
						dvProperties.RowFilter = "";
					}
					//--------------------------------------------

					string strCol = "@"+ dataColumn.ToString()+"@"; 
					
					if(strHideColumns.IndexOf(strCol)==-1)
					{
					
						columnStyle.TextBox.Enabled = true;
						columnStyle.HeaderText = dataColumn.ColumnName;
						columnStyle.MappingName = dataColumn.ColumnName;
    
						// Set width to header text width.
						iWidth = (int)(Graphics.MeasureString(columnStyle.HeaderText, 
							dataGrid.Font).Width);
						iTotalColumnWidth += iWidth ; ////inserted by Pe on 10-02-2006 for Payroll

						// Change width, if data width is wider than header text width.
						// Check the width of the data in the first X rows.
						DataRow dataRow;
						for (int iRow = 0; iRow < nRowsToScan; iRow++)
						{
							dataRow = dataTable.Rows[iRow];
      
							if (null != dataRow[dataColumn.ColumnName])
							{
								int iColWidth = (int)(Graphics.MeasureString(dataRow.
									ItemArray[iCurrCol].ToString(), 
									dataGrid.Font).Width);
								iTotalColumnWidth -= iWidth ; //Discard the header length and add the Field length - //inserted by Pe on 10-02-2006 for Payroll
								iWidth = (int)System.Math.Max(iWidth, iColWidth);
								iTotalColumnWidth += iWidth ;

							}
						}
					}
					else
					{
						iWidth = -4;
					}
					columnStyle.Width = iWidth + 4;
					if (clsGeneral.PAYROLL_CUSTOMIZED_REPORT )
					{
						columnStyle.Width = iWidth + 10; //due to customize report this change was incorporated
					}
					// Add the new column style to the table style.
					tableStyle.GridColumnStyles.Add(columnStyle);
				}  
				dataGrid.TableStyles.Add(tableStyle);
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

		//Print a Particular Text
		private void printText(Font fnt, PrintPageEventArgs e, string toPrint,int nWidth,int nHeight,float curX,float curY,bool bCaption,int nColPos,int nRow,bool bTitle,StringFormat sft )
		{
			RectangleF rect = new RectangleF(curX,curY,nWidth,nHeight);
			e.Graphics.DrawString(toPrint, fnt,new SolidBrush(Color.Black),rect,sft);
		}
			
		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if(objDs !=null && objDs.Tables.Count != 0)
			{
				if(objDs.Tables[0].Rows.Count > 0)
				{
					printGrid(e.Graphics,e);
				}
			}
		
		}
		#endregion

		private void printPreviewDialog1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch (e.Button.Tag.ToString())
			{
				case "Print":
				{
					//printDocument1.Print(); 
					//MessageBox.Show("Print");

					try
					{
						int i;
						PrintDialog pd  = new PrintDialog();
						PrinterSettings ps = new PrinterSettings();
						ps = printDocument1.PrinterSettings;
						ps.MinimumPage = 1;
						ps.MaximumPage = cboPages.Items.Count;
						 
						ps.FromPage = 1;
						ps.ToPage = cboPages.Items.Count;
						pd.AllowSomePages = true;

						pd.PrinterSettings = ps;
						if( DialogResult.OK == pd.ShowDialog(this))
						{
							//Modified by JV on 20-12-2005 - To set the printer setting as landscape wasn't applied
							printDocument1.PrinterSettings = pd.PrinterSettings;
							//-----
							for(i=pd.PrinterSettings.FromPage;i<=pd.PrinterSettings.ToPage;i++)
							{
								cboPages.SelectedIndex = i-1;
								MessageBox.Show("Press any key to print Page # " + i.ToString());
								printDocument1.Print();
							}
						}
					}
					catch (Exception exc)
					{
						MessageBox.Show(exc.Message.ToString());
					}

					break;
				}
				case "Properties":
				{
					//Modified by JV on 20-12-2005
					//To set the page settings
					bPropertiesSet = true;

					PageSetupDialog psDlg = new PageSetupDialog();
					PageSettings ps = new PageSettings();
					ps.PrinterSettings = printDocument1.PrinterSettings;
					psDlg.Document = printDocument1;
					psDlg.ShowDialog();
					printPreviewControl1.Document.DefaultPageSettings.PrinterSettings = printDocument1.DefaultPageSettings.PrinterSettings;
					calculatePages();	// Added by James 15-05-06
					printPreviewControl1.InvalidatePreview();					
					break;
				}
				case "Z-4":
				{
					printPreviewControl1.StartPage = 0;
					printPreviewControl1.Columns =2;
					printPreviewControl1.InvalidatePreview();
					break;
				}

				case "Word":
				{
					bWord = true;
					DisplayExportDialog(true);
					break;
				}

				case "XL":
				{
					bWord = false;
					DisplayExportDialog(true);
					break;
				}
				
				case "Notepad":
				{
					ExportToNotepad();
					break;
				}
				case "Margins":
					DisplayMarginDialog(true);
					break;
			}
		
		}
		//Modified by : PE
		//Purpose     : Setting printer settings for Payroll reports
		public void SetPrintSetting()
		{
			try
			{
				if ( objDs.Tables["Report"].Columns.Count < 8)
				{
					printDocument1.DefaultPageSettings.PaperSize.PaperName = "A4";
				}
				else
				{
					//The width is getting calculated from the TotalColumnwidth and adjustment values
					//PaperSize psize = new PaperSize("Custom",iTotalColumnWidth + 418,(printDocument1.DefaultPageSettings.PaperSize.Height)); //1848,1116);
					PaperSize psize = new PaperSize("Custom",(printDocument1.DefaultPageSettings.PaperSize.Height),iTotalColumnWidth + 418); //1848,1116);
					printDocument1.DefaultPageSettings.PaperSize = psize;
				}
				printPreviewControl1.Document.DefaultPageSettings.PrinterSettings = printDocument1.DefaultPageSettings.PrinterSettings;
				printPreviewControl1.InvalidatePreview();	
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"Payroll",MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
				
		}

		private void printGrid(Graphics g, PrintPageEventArgs e)
		{
			 
			printPreviewControl1.CreateGraphics();    
			g.Clear(Color.White);
			DataGrid dataGrid = dataGrid1;
			DataTable printTable = objDs.Tables[0];
			StringFormat sf = new StringFormat();
			
			int nRows = printTable.Rows.Count;
			int nCols = printTable.Columns.Count;
			float nPageTop = 0,nPageBottom = 0;
			int colWidth = 0;
			int rowHeight = (int)(g.MeasureString("Grid Control",dataGrid1.Font).Height)+1;
			int i,j=0;
			int nCurRow;
			int nTotalWidth=0;
			DataGridTableStyle tableStyle = dataGrid.TableStyles[0]; 
			string textToPrint = "";
			DataGridTextBoxColumn[] columnStyle = new DataGridTextBoxColumn[nCols];
			float[] aCurX = new float[nCols];
			int[] aColWidth = new int[nCols];
			float curX = e.PageSettings.Margins.Left ,curY = e.PageSettings.Margins.Top;
			string[] colTotal = new string[nCols];
			string[] grandTotal = new string[nCols];	//Anto on 29/07/2005
			bool[] bProcessColTotal = new bool[nCols];
			bool bColumnTotalEnabled = false;
			string strCol;


            float curYTime = 125;
            float curYPage = 135;
            sf.Alignment = StringAlignment.Center;
            //float curXX = e.PageSettings.Margins.Left+500, curYY = e.PageSettings.Margins.Top-200;
            if (head1 != "")
            {
                printText(FontHeader, e, head1, (int)g.MeasureString(head1, FontHeader).Width + 200, (int)g.MeasureString(head1, FontHeader).Height + 20, curX + 290, curY-90, false, -1, -1, true, sf);
                curY += g.MeasureString(head1, FontHeader).Height;
            }

            sf.Alignment = StringAlignment.Center;
            //float curXX = e.PageSettings.Margins.Left+500, curYY = e.PageSettings.Margins.Top-200;
            if (head2 != "")
            {
                printText(FontHeader, e, head2, (int)g.MeasureString(head2, FontHeader).Width + 200, (int)g.MeasureString(head2, FontHeader).Height + 20, curX + 250, curY-80, false, -1, -1, true, sf);
                curY += g.MeasureString(head2, FontHeader).Height;
            }
            sf.Alignment = StringAlignment.Center;
            //float curXX = e.PageSettings.Margins.Left+500, curYY = e.PageSettings.Margins.Top-200;
            if (head3 != "")
            {
                printText(FontHeader, e, head3, (int)g.MeasureString(head3, FontHeader).Width + 200, (int)g.MeasureString(head3, FontHeader).Height + 20, curX + 270, curY-70, false, -1, -1, true, sf);
                curY += g.MeasureString(head3, FontHeader).Height;
            }
			//Print Title1
			 curYTime=125;
			 curYPage=135;
			sf.Alignment = StringAlignment.Near;
			if(strTitle1 !="")
			{
				printText(FontTitle1,e,strTitle1, (int)g.MeasureString(strTitle1,FontTitle1).Width+50 ,(int)g.MeasureString(strTitle1,FontTitle1).Height,curX,curY,false,-1,-1,true,sf);
				curY+=g.MeasureString(strTitle1,FontTitle1).Height;
			}

			
			//Print Title2
			if(strTitle2 !="")
			{
				printText(FontTitle2,e,strTitle2,(int)g.MeasureString(strTitle2,FontTitle2).Width+50,(int)g.MeasureString(strTitle2,FontTitle2).Height,curX,curY,false,-1,-1,true,sf);
				curYTime=curY;
				curY+=g.MeasureString(strTitle2,FontTitle2).Height;
			}
			
			//Print Title3
			if(strTitle3 !="")
			{
				printText(FontTitle3,e,strTitle3,(int)g.MeasureString(strTitle3,FontTitle3).Width+50,(int)g.MeasureString(strTitle3,FontTitle3).Height,curX,curY,false,-1,-1,true,sf);
				curYPage=curY;
				curY+=g.MeasureString(strTitle3,FontTitle3).Height;
			}
			//To leave space 
			printText(FontTitle3,e,strTitle4,(int)g.MeasureString(strTitle4,FontTitle3).Width+50,(int)g.MeasureString(strTitle4,FontTitle3).Height,curX,curY,false,-1,-1,true,sf);
			curY+=g.MeasureString(strTitle4,FontTitle3).Height;
			
			nTotalWidth+= printDocument1.DefaultPageSettings.Margins.Left; 

			for(i=0;i<nCols;i++)
			{
				
				switch(tableStyle.GridColumnStyles[i].GetType().ToString())
				{
					case "System.Windows.Forms.DataGridTextBoxColumn":
					{
						columnStyle[i] =(DataGridTextBoxColumn)  tableStyle.GridColumnStyles[i];
						break;
					}
				}
				
				if (nRows>0) 
					textToPrint = printTable.Rows[0][i].ToString();
				else
					textToPrint = strDummy;

				colWidth=(int)e.Graphics.MeasureString(textToPrint,fntTextFont).Width;
				aColWidth [i] = columnStyle[i].Width+2;
				if (aColWidth[i] < colWidth)
				{
					aColWidth[i]=colWidth;
				}
				nTotalWidth+=aColWidth[i];
			}
			
			//To print Header date and time
			if(strHeaderDateTime!= "")
			{
				sf.Alignment = StringAlignment.Near;
                //printText(FontHeaderDate,e,"Date:"+clsGeneral.getServerDateyyFormat()+" ",(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Width,(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Height,nTotalWidth-(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Width,100,false,-1,-1,true,sf);
			
                //printText(FontHeaderDate,e,"Time:"+clsGeneral.getServerTime(),(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Width,(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Height,nTotalWidth-(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Width,curYTime,false,-1,-1,true,sf);

                //printText(FontHeaderDate,e,"Page:"+cboPages.Text,(int)g.MeasureString("Page:"+cboPages.Text,FontHeaderDate).Width,(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Height,nTotalWidth-(int)g.MeasureString("Date:"+clsGeneral.getServerDate(),FontHeaderDate).Width,curYPage,false,-1,-1,true,sf);
                printText(FontHeaderDate, e, "Date:" + System.DateTime.Now.ToString() + " ", (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Width, (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Height, nTotalWidth - (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Width, 100, false, -1, -1, true, sf);

                printText(FontHeaderDate, e, "Time:" + System.DateTime.Now.ToString(), (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Width, (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Height, nTotalWidth - (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Width, curYTime, false, -1, -1, true, sf);

                printText(FontHeaderDate, e, "Page:" + cboPages.Text, (int)g.MeasureString("Page:" + cboPages.Text, FontHeaderDate).Width, (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Height, nTotalWidth - (int)g.MeasureString("Date:" + System.DateTime.Now.ToString(), FontHeaderDate).Width, curYPage, false, -1, -1, true, sf);
			}

			if(nTotalWidth > printDocument1.DefaultPageSettings.PaperSize.Width & !bPropertiesSet)
			{
				PaperSize pkCustom = new PaperSize("First custom size",nTotalWidth+100,printDocument1.DefaultPageSettings.PaperSize.Height); 
				printDocument1.DefaultPageSettings.PaperSize=pkCustom;
				printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = pkCustom;
				cboPages.SelectedIndex = 0;

			}

			curX = e.PageSettings.Margins.Left;
			nPageTop = curY;
			g.DrawLine(new Pen(new SolidBrush(Color.Black)),curX,curY,nTotalWidth,curY);
			
			

			//Print the Column Captions
			for(i=0;i<nCols;i++)
			{
				textToPrint = columnStyle[i].MappingName.ToString() ;
				strCol= "@"+ textToPrint+"@"; 

				/* -----------------------------------------------------------------------------------------------
				 * Name	: Gerald J
				 * Date	: 02-Aug-2005 :: 02.15 PM
				 * Purpose : In the query all the number fields are converted to characters in order to show the
				 *			 amounts in the format of "99990.00" Thus while sum the amount we have to convert the
				 *			 characters to number.
				 * */

				if(ColumnTotal.ToLower().IndexOf(strCol.ToLower())!=-1)
				{
					bProcessColTotal[i] = true;
					bColumnTotalEnabled= true;
					colTotal[i]="";

					printTable.Columns.Add("Temp", typeof(Double));
					//Modified by Anto on 30-Nov-2005
					printTable.Columns["Temp"].Expression = "IIF([" + textToPrint + "]='',0,[" + textToPrint + "])";
					//printTable.Columns["Temp"].Expression = "convert([" + textToPrint + "],'System.Double')";
					grandTotal[i] = Convert.ToString(printTable.Compute("SUM(Temp)", ""));

					if(iIsAmt==0)
					{
						if (grandTotal[i].IndexOf(".") < 0)
							grandTotal[i] = grandTotal[i] + ".00";
						if (grandTotal[i].Substring(grandTotal[i].IndexOf(".") + 1).Length == 1)
							grandTotal[i] = grandTotal[i] + "0";
					}
					else
					{
						grandTotal[i].Replace(".00"," ");
					}

					printTable.Columns.Remove("Temp");
				}

				// -----------------------------------------------------------------------------------------------
				if(columnStyle[i].Alignment==HorizontalAlignment.Left)
					sf.Alignment = StringAlignment.Near;
				else
					sf.Alignment = StringAlignment.Far;
				printText(fntTextFont,e,textToPrint,aColWidth[i],rowHeight,curX,curY,true,i,-1,false,sf);
				curX+=aColWidth[i];
				colTotal[i]="0";
			}

			//---------------------------------------------
			//By Anto on 9-Jan-2006
			if(sNetColExpression != "")
			{
				for(i=0;i<nCols;i++)
				{
					sNetColExpression = sNetColExpression.Replace("ê" + columnStyle[i].MappingName.ToString() + "ê",(grandTotal[i] == null ? "" : grandTotal[i]));
				}
				string[] sExpressions = sNetColExpression.Split('@');
				double dNetCol = Convert.ToDouble(sExpressions[0]);
				for(int iIndex = 1; iIndex<sExpressions.Length; iIndex++)
				{
					if(sExpressions[iIndex] == "+")
					{
						dNetCol += Convert.ToDouble(sExpressions[iIndex+1]);
					}
					else if(sExpressions[iIndex] == "-")
					{
						dNetCol -= Convert.ToDouble(sExpressions[iIndex+1]);
					}
				}
				dAPayment = dNetCol;
			}
			/* -------------------------------------------------
			 * Name		: Gerald J
			 * Date		: 31-Aug-2005 :: 5.00 PM
			 * */
			/*
						try
						{
							dPayment		= Convert.ToDouble(grandTotal[4]);
							dDueCollection	= Convert.ToDouble(grandTotal[7]);
							dRefund			= Convert.ToDouble(grandTotal[8]);
							dAPayment		= (dPayment + dDueCollection) + dRefund;
						}
						catch
						{
							dPayment		= 0;
							dDueCollection	= 0;
							dRefund			= 0;
							dAPayment		= 0;
						}
			*/
			//---------------------------------------------
			// -------------------------------------------------

			//curY += g.MeasureString(printTable.Rows[i][0].ToString(),dataGrid1.Font).Height+1;
			//Modified by Anto on 29-Aug-2005
			//if (nRows>0) 
			if (nRows > 0 & printTable.Rows[0][0] != DBNull.Value)
			{
				if(printTable.Rows[0][0].ToString() == "")
				{
					curY += g.MeasureString(strDummy,dataGrid1.Font).Height+1;
				}
				else
				{
					curY += g.MeasureString(printTable.Rows[0][0].ToString(),dataGrid1.Font).Height+1;
				}
			}
			else
				curY += g.MeasureString(strDummy,dataGrid1.Font).Height+1;

			curX = e.PageSettings.Margins.Left; 
			g.DrawLine(new Pen(new SolidBrush(Color.Black)),curX,curY,nTotalWidth,curY);
			curY+=2;
			
			nCurRow = (cboPages.SelectedIndex * nLinesPerPage );

			//Print the Rows
			for(i=nCurRow;i<nCurRow+nLinesPerPage;i++)
			{
				if((i>=objDs.Tables[0].Rows.Count) || (i<0))
				{
					break;
				}
				curX = e.PageSettings.Margins.Left;
				for(j=0;j<nCols;j++)
				{ 
					//Modified by Anto on March 14 to hide columns
					string sColName = columnStyle[j].MappingName.ToString();
					if(strHideColumns.IndexOf(sColName)>-1)
						textToPrint = "";
					else
						textToPrint = printTable.Rows[i][j].ToString();
					if(bProcessColTotal[j]==true)
					{
						//Modified by Anto on 30-Nov-2005
						//colTotal[j]= Convert.ToString((Convert.ToDouble(colTotal[j])+Convert.ToDouble(textToPrint)));
						string sTemp = (textToPrint.Trim() == "")? "0" : textToPrint;
						colTotal[j]= Convert.ToString((Convert.ToDouble(colTotal[j])+Convert.ToDouble(sTemp)));
						if(printTable.Columns[j].DataType ==typeof(System.Decimal) || printTable.Columns[j].DataType ==typeof(System.Double ))
						{
							colTotal[j]= Convert.ToString((Convert.ToDouble(colTotal[j])));
						}

						/* ---------------------------------------------------------------------------------------
						 * Name	: Gerald
						 * Date	: 02-Aug-2005 :: 04.30 PM
						 * Purpose : To show the amounts in the format of "99990.00"
						 * */

						if (iIsAmt==0) 
						{
							if (colTotal[j].IndexOf(".") < 0)
								colTotal[j] = colTotal[j] + ".00";
							if (colTotal[j].Substring(colTotal[j].IndexOf(".") + 1).Length == 1)
								colTotal[j] = colTotal[j] + "0";
						}
						else
						{
							colTotal[j].Replace(".00"," ");

						}

						// ---------------------------------------------------------------------------------------
					}
					sf.Alignment =StringAlignment.Near;

					if(columnStyle[j].Alignment == HorizontalAlignment.Right)
					{
						sf.Alignment =StringAlignment.Far;
					}

					printText(dataGrid1.Font,e,textToPrint,aColWidth[j],rowHeight,curX,curY,false,j,i ,false,sf);
					curX+=aColWidth[j];
				}
				curX = e.PageSettings.Margins.Left;
				if (nRows > 0 & printTable.Rows[i][0] != DBNull.Value)
				{
					//curY += g.MeasureString(printTable.Rows[i][0].ToString(),dataGrid1.Font).Height+1;
					if(printTable.Rows[i][0].ToString() == "")
					{
						curY += g.MeasureString(strDummy,dataGrid1.Font).Height+iRowSpaceHeight;
					}
					else
					{
						curY += g.MeasureString(printTable.Rows[i][0].ToString(),dataGrid1.Font).Height+iRowSpaceHeight;
					}
				}
				else
					curY += g.MeasureString(strDummy,dataGrid1.Font).Height+iRowSpaceHeight;

				//Draw Horizontal Lines
				if(DrawHorizontalLines==true)
				{
					g.DrawLine(new Pen(new SolidBrush(Color.Black)),curX,curY,nTotalWidth,curY);
					curY +=1;
				}
			}
			curX = e.PageSettings.Margins.Left;

			g.DrawLine(new Pen(new SolidBrush(Color.Black)),curX,curY,nTotalWidth,curY);
			
			//Print the Column Totals
			if(bColumnTotalEnabled==true)
			{
				curX = e.PageSettings.Margins.Left;
				for(j=0;j<nCols;j++)
				{
					for(j=0;j<nCols;j++)
					{
						textToPrint="";
						if(bProcessColTotal[j]==true)
						{
							//Modified by PE on 20-03-2007 for Formatting purpose
//							if (clsGeneral.PAYROLL_CUSTOMIZED_REPORT )
//								textToPrint = Convert.ToDouble(colTotal[j]).ToString("#,##,##,##,##,##0.00").ToString();
//							else
								textToPrint = Convert.ToDouble(colTotal[j]).ToString("#,##,##,##,##,##0.00").ToString();

						}
						sf.Alignment =StringAlignment.Far;
						printText(fntTextFont,e,textToPrint.ToString(),aColWidth[j],rowHeight,curX,curY,false,j,i ,false,sf);
						curX+=aColWidth[j];
					}

				}
				curX = e.PageSettings.Margins.Left;
				//curY += g.MeasureString(printTable.Rows[i][0].ToString(),dataGrid1.Font).Height+1;
				//Modified by Anto on 29-Aug-2005
				//if (nRows>0)
				if (nRows > 0 & printTable.Rows[0][0] != DBNull.Value)
				{
					//curY += g.MeasureString(printTable.Rows[0][0].ToString(),dataGrid1.Font).Height+1;
					if(printTable.Rows[0][0].ToString() == "")
					{
						curY += g.MeasureString(strDummy,dataGrid1.Font).Height+1;
					}
					else
					{
						curY += g.MeasureString(printTable.Rows[0][0].ToString(),dataGrid1.Font).Height+1;
					}
				}
				else
					curY += g.MeasureString(strDummy,dataGrid1.Font).Height+1;

				g.DrawLine(new Pen(new SolidBrush(Color.Black)),curX,curY,nTotalWidth,curY);
				curY += 1;
			}
			
			//Anto on 29/07/2005 for printing Grand Total
			if(bColumnTotalEnabled==true & (cboPages.SelectedIndex + 1 == cboPages.Items.Count))
			{
				curX = e.PageSettings.Margins.Left;
				for(j=0;j<nCols;j++)
				{
					for(j=0;j<nCols;j++)
					{
						textToPrint="";
						if(bProcessColTotal[j]==true)
						{
							//Modified by PE on 20-03-2007 for Formatting purpose
//							if (clsGeneral.PAYROLL_CUSTOMIZED_REPORT )
//								textToPrint = Convert.ToDouble(grandTotal[j]).ToString();
//							else
							textToPrint = Convert.ToDouble(grandTotal[j]).ToString("##,##,##,##,##,##0.00").ToString();
						}
						sf.Alignment =StringAlignment.Far;
						printText(fntTextFont,e,textToPrint,aColWidth[j],rowHeight,curX,curY,false,j,i ,false,sf);
						curX+=aColWidth[j];
					}

				}
				curX = e.PageSettings.Margins.Left;
				//curY += g.MeasureString(printTable.Rows[i][0].ToString(),dataGrid1.Font).Height+1;
				//Modified by Anto on 29-Aug-2005
				//if (nRows>0)
				if (nRows > 0 & printTable.Rows[0][0] != DBNull.Value)
				{
					if(printTable.Rows[0][0].ToString() == "")
					{
						curY += g.MeasureString(strDummy,dataGrid1.Font).Height+ 1;
					}
					else
					{
						curY += g.MeasureString(printTable.Rows[0][0].ToString(),dataGrid1.Font).Height+1;
					}
				}
				else
				{
					curY += g.MeasureString(strDummy,dataGrid1.Font).Height+1;
				}

				g.DrawLine(new Pen(new SolidBrush(Color.Black)),curX,curY,nTotalWidth,curY);

				curY += 1;
			}
			//-------------------------------------------

			/* ---------------------------------------------------------------------------------------------------
			 * Name		: Gerald J
			 * Date		: 31-Aug-2005 :: 05.00 PM
			 * */
			//Modified by Anto on 24-Oct-2005 to not to show the Net collection when total is not calculated
			//if(cboPages.SelectedIndex + 1 == cboPages.Items.Count)
			if(bColumnTotalEnabled==true & (cboPages.SelectedIndex + 1 == cboPages.Items.Count) & bPrintNetcollection == true)
			{		
				curX = e.PageSettings.Margins.Left;

				textToPrint = "Net Collection : " + dAPayment.ToString("##,##,##,##,##,##0.00");
				//Modified by PE on 20-03-2007 for Formatting purpose
				//dAPayment.ToString("############0.00"); 
				sf.Alignment =StringAlignment.Far;
				printText(fntTextFont, e, textToPrint, (int)g.MeasureString(textToPrint,fntTextFont).Width+20, rowHeight, curX, curY, false, 0, 1, false, sf);
				
				curX = e.PageSettings.Margins.Left;
				if (nRows > 0 & printTable.Rows[0][0] != DBNull.Value)
				{
					if(printTable.Rows[0][0].ToString() == "")
					{
						curY += g.MeasureString(strDummy,dataGrid1.Font).Height+1;
					}
					else
					{
						curY += g.MeasureString(printTable.Rows[0][0].ToString(),dataGrid1.Font).Height+1;
					}
				}
				else
				{
					curY += g.MeasureString(strDummy,dataGrid1.Font).Height+1;
				}
			}

			// ---------------------------------------------------------------------------------------------------

			nPageBottom = curY;
			//Draw Vertical Lines
			if(DrawVerticalLines ==true)
			{
				for(j=0;j<=nCols;j++)
				{
					g.DrawLine(new Pen(new SolidBrush(Color.Black)),curX,nPageTop,curX,nPageBottom);
					
					if(j==nCols)
					{
						curX=printDocument1.DefaultPageSettings.Margins.Left+printDocument1.DefaultPageSettings.PaperSize.Width;
					}
					else
					{
						curX+=aColWidth[j];
					}
					
				}

			}

			curX = e.PageSettings.Margins.Left;
			textToPrint = cboPages.Text;
			curY = nPageBottom;
			if(curY > printDocument1.DefaultPageSettings.PaperSize.Height -printDocument1.DefaultPageSettings.Margins.Bottom )
			{
				curY = nPageBottom;
			}
			curY += 1;
			printText(dataGrid1.Font,e,textToPrint,(int)g.MeasureString(textToPrint,dataGrid1.Font).Width+20 ,rowHeight,curX,curY,false,j,i ,false,sf);
			  
		}

		private void changeZoomLevel(MenuItem mnu)
		{
			string zoomLevel = mnu.Text;
			menuItem1.Checked = false;
			menuItem2.Checked = false;
			menuItem3.Checked = false;
			menuItem4.Checked = false;
			menuItem5.Checked = false;
			menuItem6.Checked = false;
			menuItem7.Checked = false;
			menuItem7.Checked = false;
			mnu.Checked = false;
			if(zoomLevel=="Auto")
			{
				printPreviewControl1.AutoZoom = true;  
			}
			else
			{
				Double  zoom = Convert.ToDouble(zoomLevel.Replace("%","").ToString())/100 ;
				printPreviewControl1.Zoom=zoom;
			}

		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem2);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem1);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem3);
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem4);
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem5);
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem6);
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem7);
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem8);
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			changeZoomLevel(menuItem9);
		}

		private void cboPages_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			printPreviewControl1.InvalidatePreview();
		}
	
		//============================================================================================================
		//					Exporting to MS WORD & MS EXCEL
		//============================================================================================================

		//Modified By   : JV
		//Modified Date : 08-12-2005 01:50 PM
		//Purpose		: To export the specified pages to Word
		private void ExportToWord()
		{
			
			object missing = Missing.Value;
			int nPageFrom = (int) nUDnPageFrom.Value;
			int nPageTo = (int) nUDnPageTo.Value;;
			int nNoOfColTotal = 0;
			int nCurRow = 1;
			int nCurPage = 1;
			int nColCount = 0;
			int nRecordsPerPage = 20;

			try
			{
				DataTable objTable = objDs.Tables[0];
				nRecordsPerPage = nLinesPerPage;

				Word_App = new Word.Application();
				Word_doc = new Word.Document();

				Word.AutoCorrect autocorrect = Word_App.AutoCorrect;
				Word.AutoCorrectEntries autoEntries = autocorrect.Entries; 

				Word.Documents Docs = Word_App.Documents;
				Word._Document my_Doc = (Word._Document) Word_doc;
				Word_doc=Docs.Add(ref missing, ref missing, ref missing, ref missing);
			
				PaperSize pkCustom = new PaperSize("First custom size",1000,printDocument1.DefaultPageSettings.PaperSize.Height); 
				Word_doc.PageSetup.PageWidth = pkCustom.Width;
				Word_doc.PageSetup.PageHeight = pkCustom.Height;

				Word.Range range = Word_App.ActiveWindow.Selection.Range;
                if (string.Concat(head1, "").Trim() != "") AddHeadings(head1, 12);
                if (string.Concat(head2, "").Trim() != "") AddHeadings(head2, 12);
                if (string.Concat(head3, "").Trim() != "") AddHeadings(head3, 12);
				//Export Titles (Title 1, 2 and 3)
				if (string.Concat(strTitle1, "").Trim() != "") AddHeadings(strTitle1, 16);
				if (string.Concat(strTitle2, "").Trim() != "") AddHeadings(strTitle2, 14);
				if (string.Concat(strTitle3, "").Trim() != "") AddHeadings(strTitle3, 14);
			
				Word_App.ActiveWindow.Selection.Font.Bold = 0;			
				//------------------------------------------------------
				int nRowCount = objTable.Rows.Count;
				nCurPage = nPageFrom-1;
				nCurRow = nCurPage * nRecordsPerPage;

				nRowCount = ((nPageTo-nPageFrom+1) * nRecordsPerPage) <= nRowCount ? ((nPageTo-nPageFrom+1) * nRecordsPerPage): nRowCount;
				nRowCount += nCurRow;
				if (nRowCount > objTable.Rows.Count) nRowCount = objTable.Rows.Count;
				nColCount = objTable.Columns.Count;

				CalculateTotal(nCurPage++);
				int nTableRows = (nRowCount+ (bExpColTotalEnabled == true & nPageTo == cboPages.Items.Count? 2 : 1)-nCurRow);
				Word.Table tbl = Word_App.ActiveWindow.Selection.Tables.Add(Word_App.ActiveWindow.Selection.Range, nTableRows, nColCount, ref missing, ref missing);
				CreateTableCaption(tbl);

				progressBar1.Visible = true;
				progressBar1.Minimum = nCurRow;
				progressBar1.Maximum = nRowCount+nCurRow;

				int i = 0;
				for (int iRow = 0; iRow < nRowCount - nCurRow; iRow++)
				{
					i = iRow + nCurRow;
					for (int iCol = 0; iCol < nColCount; iCol++)
					{
						InsertTextToWord(tbl, iRow+2, iCol+1, objTable.Rows[i][iCol].ToString(), false);
					}
					progressBar1.Value = iRow+nCurRow;
				}

				if (bExpColTotalEnabled)
				{
					nNoOfColTotal = (nPageTo - nPageFrom) + 1;
					if (nPageFrom == 1 & nPageTo == cboPages.Items.Count)  nRowCount = nRowCount + 1; //Include a row for Grand Total

					if (nPageFrom == 1 & nPageTo == cboPages.Items.Count)
					{
						for (int iCol = 1; iCol <= nColCount; iCol++)
						{
							if (bExpProcessColTotal[iCol-1] == true)
							{
								InsertTextToWord(tbl, nRowCount+1, iCol, sExpGrandTotal[iCol-1], true);
							}					
						}
					}
				}
				progressBar1.Value = progressBar1.Maximum;
				progressBar1.Visible = false;
				Word_App.Visible = true;
				//			Word_doc.Select();
				//			Word_doc.Application.Selection.Copy();
				Word_App.ActiveWindow.Selection.Start = 0;
				Word_App.ActiveWindow.Selection.End = 0;
				Word_App.Activate();
			}
			catch
			{}
		}

		//Modified By   : JV
		//Modified Date : 08-12-2005 01:50 PM
		//Purpose		: To export the specified pages to Excel
		private void ExportToExcel()
		{
			int sheetIndex = 1;			
		
			m_Excel = new Excel.ApplicationClass();
			m_ExcelWorkbook = m_Excel.Workbooks.Add(Type.Missing);
			Excel.Worksheet objWorkSheet;

			int COLOR_RGB = System.Drawing.Color.Gray.ToArgb();

			if(sheetIndex <= m_ExcelWorkbook.Worksheets.Count)
				objWorkSheet = (Excel.Worksheet) m_ExcelWorkbook.Worksheets[sheetIndex];
			else
				objWorkSheet =(Excel.Worksheet) m_ExcelWorkbook.Worksheets.Add(Type.Missing, 
					m_ExcelWorkbook.Worksheets[sheetIndex-1], Type.Missing, Type.Missing);
					
			sheetIndex = sheetIndex + 1;

			object missing = Missing.Value;
			int nPageFrom = (int) nUDnPageFrom.Value;
			int nPageTo = (int) nUDnPageTo.Value;;
			int nNoOfColTotal = 0;
			int nCurRow = 1;
			int nCurPage = 1;
			int nColCount = 0;
			int nRecordsPerPage = 20;

			try
			{
				
				DataTable objTable = objDs.Tables[0];
				nRecordsPerPage = nLinesPerPage;
			
				AddTitle(objWorkSheet);
			
				int nRowCount = objTable.Rows.Count;
				nCurPage = nPageFrom-1;
				nCurRow = nCurPage * nRecordsPerPage;

				nRowCount = ((nPageTo-nPageFrom+1) * nRecordsPerPage) <= nRowCount ? ((nPageTo-nPageFrom+1) * nRecordsPerPage): nRowCount;
				nRowCount += nCurRow;
				if (nRowCount > objTable.Rows.Count) nRowCount = objTable.Rows.Count;
				nColCount = objTable.Columns.Count;

				for (int iCol = 0; iCol < nColCount; iCol++)
					AddHeaders(objTable.Columns[iCol].ColumnName.ToString(), 7, iCol, objWorkSheet);


				CalculateTotal(nCurPage++);

				progressBar1.Visible = true;
				progressBar1.Minimum = nCurRow;
				progressBar1.Maximum = nRowCount+nCurRow;
			
				int i = 0;
				for (int iRow = 0; iRow < nRowCount - nCurRow; iRow++)
				{
					i = iRow + nCurRow;
					for (int iCol = 0; iCol < nColCount; iCol++) 
						AddCellText(objWorkSheet, iRow+8, iCol, objTable.Rows[i][iCol].ToString(), false);

					progressBar1.Value = iRow+nCurRow;
				}

				if (bExpColTotalEnabled)
				{
					nNoOfColTotal = (nPageTo - nPageFrom) + 1;
					if (nPageFrom == 1 & nPageTo == cboPages.Items.Count)  nRowCount = nRowCount + 1; //Include a row for Grand Total

					if (nPageFrom == 1 & nPageTo == cboPages.Items.Count)
					{
						for (int iCol = 0; iCol < nColCount; iCol++)
						{
							if (bExpProcessColTotal[iCol] == true) AddCellText(objWorkSheet, nRowCount+7, iCol, sExpGrandTotal[iCol], true);
						}
					}
				}

				progressBar1.Value = progressBar1.Maximum;
				m_Excel.Visible = true;
				progressBar1.Visible = false;
			}
			catch{}
		}

		//Modified By   : JV
		//Modified Date : 19-12-2005 05:30 PM
		//Purpose		: To export the specified pages to Notepad
		private void ExportToNotepad()
		{
			object missing = Missing.Value;
			string strValue = "";
			string strTemp = "";
			string sLine = "";
			string sPath = "";
			int nPageFrom = (int) nUDnPageFrom.Value;
			int nPageTo = (int) nUDnPageTo.Value;;
			int nNoOfColTotal = 0;
			int nRowCount = 0;
			int nCurRow = 1;
			int nCurPage = 1;
			int nColCount = 0;
			int nRecordsPerPage = 20;

			try
			{
				DataGrid dataGrid = dataGrid1;
				DataTable objTable = objDs.Tables[0];

				nRecordsPerPage = nLinesPerPage;
			
				//AddTitle(objWorkSheet);
				nPageFrom = 1;
				nPageTo = cboPages.Items.Count;
				nRowCount = objTable.Rows.Count;
				nCurPage = nPageFrom-1;
				nCurRow = nCurPage * nRecordsPerPage;

				nRowCount = ((nPageTo-nPageFrom+1) * nRecordsPerPage) <= nRowCount ? ((nPageTo-nPageFrom+1) * nRecordsPerPage): nRowCount;
				nRowCount += nCurRow;
				if (nRowCount > objTable.Rows.Count) nRowCount = objTable.Rows.Count;
				nColCount = objTable.Columns.Count;

				DataGridTableStyle tableStyle = dataGrid.TableStyles[0]; 
				DataGridTextBoxColumn[] columnStyle = new DataGridTextBoxColumn[nColCount];

				sPath = Application.StartupPath + "\\Report_Export.txt";
				StreamWriter sw = File.CreateText(sPath);
                sw.WriteLine(head1 + "");
                sw.WriteLine(head2 + "");
                sw.WriteLine(head3 + "");
				sw.WriteLine(Title1 + "");
				sw.WriteLine(Title2 + "");
				sw.WriteLine(Title3 + "");
				int[] nColLength = new int[nColCount];
				for (int iCol = 0; iCol < nColCount; iCol++)
				{
					switch(tableStyle.GridColumnStyles[iCol].GetType().ToString())
					{
						case "System.Windows.Forms.DataGridTextBoxColumn":
						{
							nColLength[iCol] = 30; //((DataGridTextBoxColumn)  tableStyle.GridColumnStyles[iCol]).Width-50;
							break;
						}
					}
					strTemp = objTable.Columns[iCol].ColumnName.ToString();
					strTemp = strTemp.PadRight(nColLength[iCol], ' ');
					strTemp = strTemp.Substring(0, nColLength[iCol]);
					strValue += strTemp;
				}
				sLine = sLine.PadRight(strValue.Length, '-');
				sw.WriteLine(sLine);
				sw.WriteLine(strValue);
				sw.WriteLine(sLine);

				CalculateTotal(nCurPage++);
			
				int i = 0;
				for (int iRow = 0; iRow < nRowCount - nCurRow; iRow++)
				{
					i = iRow + nCurRow;
					strValue = "";
					strTemp = "";
					for (int iCol = 0; iCol < nColCount; iCol++)
					{
						strTemp = objTable.Rows[i][iCol].ToString();
						strTemp = strTemp.Trim().PadRight(nColLength[iCol], ' ');
						strTemp = strTemp.Substring(0, nColLength[iCol]);
						strValue += strTemp;
					}
					sw.WriteLine(strValue);
				}

				strValue = "";
				strTemp = "";
				if (bExpColTotalEnabled)
				{
					nNoOfColTotal = (nPageTo - nPageFrom) + 1;
					if (nPageFrom == 1 & nPageTo == cboPages.Items.Count)
					{
						for (int iCol = 0; iCol < nColCount; iCol++)
						{
							if (bExpProcessColTotal[iCol] == true) 
								strTemp = sExpGrandTotal[iCol];
							strTemp = strTemp.PadRight(nColLength[iCol], ' ');
							strTemp = strTemp.Substring(0, nColLength[iCol]);
							strValue += strTemp;
						}
					}
					sw.WriteLine(sLine);
					sw.WriteLine(strValue);
					sw.WriteLine(sLine);
				}
				sw.Close();
		
				Process myProcess = new Process();
				myProcess.StartInfo.FileName = sPath;
				myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
				myProcess.StartInfo.CreateNoWindow = true;
				myProcess.Start();
			}
			catch{}
		}

		//====================================================================================================
		//						Sub-Procedures for Export to Word
		//====================================================================================================
		private void AddHeadings(string sStr, int iSize)
		{
			Word_App.ActiveWindow.Selection.Font.Bold = 1;
			Word_App.ActiveWindow.Selection.Font.Size = iSize;
			Word_App.ActiveWindow.Selection.TypeText(sStr);
			Word_App.ActiveWindow.Selection.TypeParagraph();
		}

		private void InsertTextToWord(Word.Table objTbl, int iRow, int iCol, string sValue, bool Bold)
		{
			objTbl.Cell(iRow, iCol).Range.InsertAfter(sValue);
			objTbl.Cell(iRow, iCol).Range.Font.Size = 10;
			if (Bold) objTbl.Cell(iRow, iCol).Range.Font.Bold = 1;
		}

		//====================================================================================================
		//						Sub-Procedures for Export to Excel
		//====================================================================================================

		private void CreateTableCaption(Word.Table objTbl)
		{	
			for (int iCol = 1; iCol <= objDs.Tables[0].Columns.Count; iCol++)
			{
				objTbl.Cell(1, iCol).Range.InsertAfter(objDs.Tables[0].Columns[iCol-1].ColumnName.ToString());
				objTbl.Cell(1, iCol).Range.Font.Size = 10;
				objTbl.Cell(1, iCol).Range.Bold = 1;
				objTbl.Cell(1, iCol).Shading.ForegroundPatternColor = Word.WdColor.wdColorAutomatic;
				objTbl.Cell(1, iCol).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray35;
			}
		}

		private void AddTitle(Excel.Worksheet objWorkSheet)
		{
			Excel.Range objRange;
            //Heading  1
            if (string.Concat(head1, "").Trim() != "")
            {
                objRange = objWorkSheet.get_Range("F1", Type.Missing);
                objRange.Font.Name = "Verdana";
                objRange.Font.Size = 10;
                objRange.Cells.ColumnWidth = 20;
                objRange.Value2 = head1;
            }

            //Heading 2
            if (string.Concat(head2, "").Trim() != "")
            {
                objRange = objWorkSheet.get_Range("F2", Type.Missing);
                objRange.Font.Name = "Verdana";
                objRange.Font.Size = 10;
                objRange.Cells.ColumnWidth = 20;
                objRange.Value2 = head2;
            }

            //Heading 3
            if (string.Concat(head3, "").Trim() != "")
            {
                objRange = objWorkSheet.get_Range("F3", Type.Missing);
                objRange.Font.Name = "Verdana";
                objRange.Font.Size = 11;
                objRange.Cells.ColumnWidth = 20;
                objRange.Value2 = head3;
            }

			//Title 1
			if (string.Concat(strTitle1, "").Trim() != "")
			{
				objRange=objWorkSheet.get_Range("A4", Type.Missing);
				objRange.Font.Name = "Verdana";
				objRange.Font.Size = 14;
				objRange.Cells.ColumnWidth = 20;
				objRange.Value2 = strTitle1;
			}

			//Title 2
			if (string.Concat(strTitle2, "").Trim() != "")
			{
				objRange=objWorkSheet.get_Range("A5", Type.Missing);
				objRange.Font.Name = "Verdana";
				objRange.Font.Size = 12;
				objRange.Cells.ColumnWidth = 20;
				objRange.Value2 = strTitle2;
			}

			//Title 3
			if (string.Concat(strTitle3, "").Trim() != "")
			{
				objRange=objWorkSheet.get_Range("A6", Type.Missing);
				objRange.Font.Name = "Verdana";
				objRange.Font.Size = 11;
				objRange.Cells.ColumnWidth = 20;
				objRange.Value2 = strTitle3;
			}
		}

		private int AddHeaders(string columnHeader, int iRow, int iCol, Excel.Worksheet objWorksheet)
		{
			string columnName;
			string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			columnName = alphabets.Substring(iCol, 1)+ iRow;
			Excel.Range objRange = objWorksheet.get_Range(columnName,Type.Missing);
			objRange.Cells.ColumnWidth = 15;
			objRange.Font.Name = "Verdana";
			objRange.Font.Bold = true;
			objRange.Font.Size = 10;
			objRange.HorizontalAlignment = Excel.Constants.xlCenter;
			objRange.Cells.RowHeight = 25;
			objRange.WrapText = true;
			objRange.Value2 = columnHeader;
			objRange.BorderAround(Type.Missing,Excel.XlBorderWeight.xlThin, 
				Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());

			return 0;
		}

		private Excel.Range AddCellText(Excel.Worksheet objWorkSheet, int iRow, int iCol, object cellvalue, bool bBold)
		{
			string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string columnName;
			Excel.Range objRange = null;
			columnName = alphabets.Substring(iCol, 1) + iRow;
			objRange = objWorkSheet.get_Range(columnName,Type.Missing);
			objRange.Font.Bold = bBold;
			objRange.Font.Name = "Verdana";
			objRange.Font.Size = 10;
			objRange.NumberFormat = "0";
			objRange.Value2 = cellvalue;
			return objRange;
		}

		//====================================================================================================
		//						Sub-Procedures for Both Export to Word & Excel
		//====================================================================================================

		private void CalculateTotal(int iCurPage)
		{
			 
			Graphics g = dataGrid1.CreateGraphics();
			DataGrid dataGrid = dataGrid1;
			DataTable printTable = objDs.Tables[0];
						
			int nRows = printTable.Rows.Count;
			int nCols = printTable.Columns.Count;
			
			int rowHeight = (int)(g.MeasureString("Grid Control",dataGrid1.Font).Height)+1;
			int i, j = 0;
			int nCurRow;
			
			DataGridTableStyle tableStyle = dataGrid.TableStyles[0]; 
			string textToPrint = "";
			DataGridTextBoxColumn[] columnStyle = new DataGridTextBoxColumn[nCols];

			string strCol;

			sExpColTotal = new string[nCols];		// ->Export - Column Total
			sExpGrandTotal = new string[nCols];		// ->Export - Column Grand Total
			bExpProcessColTotal = new bool[nCols];	// ->Export - Process Column Total
			
			iRowHeight = rowHeight;

			//Print the Column Captions
			for(i = 0; i < nCols; i++)
			{
				textToPrint = printTable.Columns[i].ColumnName.ToString();
				strCol= "@" + textToPrint + "@"; 
				if(ColumnTotal.ToLower().IndexOf(strCol.ToLower())!= -1)
				{
					bExpProcessColTotal[i] = true;
					bExpColTotalEnabled = true;
					sExpColTotal[i] = "";

					printTable.Columns.Add("Temp", typeof(Double));
					printTable.Columns["Temp"].Expression = "IIF([" + textToPrint + "]='',0,[" + textToPrint + "])";
					sExpGrandTotal[i] = Convert.ToString(printTable.Compute("SUM(Temp)", ""));

					if (sExpGrandTotal[i].IndexOf(".") < 0)
						sExpGrandTotal[i] = sExpGrandTotal[i] + ".00";
					if (sExpGrandTotal[i].Substring(sExpGrandTotal[i].IndexOf(".") + 1).Length == 1)
						sExpGrandTotal[i] = sExpGrandTotal[i] + "0";

					printTable.Columns.Remove("Temp");
				}
				sExpColTotal[i] = "0";
			}

			nCurRow = (iCurPage * nLinesPerPage);

			//Print the Rows
			for(i = nCurRow; i < nCurRow+nLinesPerPage; i++)
			{
				if((i >= objDs.Tables[0].Rows.Count) || (i < 0)) break;
				
				for(j = 0; j < nCols;j++)
				{
					textToPrint = printTable.Rows[i][j].ToString();
					if(bExpProcessColTotal[j] == true)
					{
						string sTemp = (textToPrint.Trim() == "")? "0" : textToPrint;
						sExpColTotal[j] = Convert.ToString((Convert.ToDouble(sExpColTotal[j]) + Convert.ToDouble(sTemp)));
						if(printTable.Columns[j].DataType == typeof(System.Decimal) || printTable.Columns[j].DataType == typeof(System.Double ))
						{
							sExpColTotal[j] = Convert.ToString((Convert.ToDouble(sExpColTotal[j])));
						}
						if (sExpColTotal[j].IndexOf(".") < 0)
							sExpColTotal[j] = sExpColTotal[j] + ".00";
						if (sExpColTotal[j].Substring(sExpColTotal[j].IndexOf(".") + 1).Length == 1)
							sExpColTotal[j] = sExpColTotal[j] + "0";
					}
					
				}
				
			}
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			if ((int) nUDnPageFrom.Value > (int) nUDnPageTo.Value | 
				(int) nUDnPageFrom.Maximum < (int) nUDnPageFrom.Value |
				(int) nUDnPageTo.Maximum < (int) nUDnPageTo.Value |
				(int) nUDnPageFrom.Minimum > (int) nUDnPageFrom.Value |
				(int) nUDnPageTo.Minimum > (int) nUDnPageTo.Value)
			{
				MessageBox.Show ("Invalid range of pages", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				nUDnPageFrom.Focus();
				return;
			}
			btnStart.Enabled = false;
			btnCancel.Enabled = false;
			if (bWord) 
				ExportToWord();
			else
				ExportToExcel();

			DisplayExportDialog(false);
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DisplayExportDialog(false);
		}

		private void DisplayExportDialog(bool Display)
		{
			btnStart.Enabled = true;
			btnCancel.Enabled = true;
			if (Display)
			{
				nUDnPageFrom.Minimum = 1;
				nUDnPageTo.Minimum = 1;
				nUDnPageFrom.Maximum = cboPages.Items.Count;
				nUDnPageTo.Maximum = cboPages.Items.Count;
				nUDnPageTo.Value = cboPages.Items.Count;
			}
			cboPages.Enabled = !Display;
			toolBar1.Enabled = !Display;
			pnlExport.Visible = Display;
		}

		private void DisplayMarginDialog(bool Display)
		{
			if (Display)
			{
				loadPaperSizes();
				nUdnHeight.Minimum	= 0;
				nUdnWidth.Minimum	= 0;
				nUdnHeight.Maximum	= 10000;
				nUdnWidth.Maximum	= 10000;
				nUdnHeight.Value	= printDocument1.DefaultPageSettings.PaperSize.Height / (decimal)3.93666666667;
				nUdnWidth.Value		= printDocument1.DefaultPageSettings.PaperSize.Width / (decimal)3.93666666667;
			}
			cboPages.Enabled = !Display;
			toolBar1.Enabled = !Display;
			pnlMargins.Visible = Display;

			cboPaperSizes.Font =nUdnHeight.Font =nUdnWidth.Font =label4.Font = label6.Font = lblPaperName.Font = this.lblCaption.Font;
		}

		private void loadPaperSizes()
		{
			cboPaperSizes.Items.Clear();
			cboPaperSizes.Text = "";
			cboPaperSizes.DisplayMember = "PaperName";
			foreach(PaperSize size in printDocument1.DefaultPageSettings.PrinterSettings.PaperSizes)
			{
				if(size.Kind == PaperKind.Custom)
					cboPaperSizes.Items.Add(size);
			}
		}

		private void btnMarCancel_Click(object sender, System.EventArgs e)
		{
			DisplayMarginDialog(false);
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if(cboPaperSizes.Text.Trim()=="")
			{
				MessageBox.Show("Kindly enter the Paper Name, the paper name should not be empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			string sTempSize = cboPaperSizes.Text;
			MJMCustomPrintForm.MJMCustomPrintForm.AddCustomPaperSize(printDocument1.PrinterSettings.PrinterName, cboPaperSizes.Text, float.Parse(nUdnWidth.Value.ToString()), float.Parse(nUdnHeight.Value.ToString()));
			loadPaperSizes();
			cboPaperSizes.Text = sTempSize;
			printDocument1.DefaultPageSettings.PaperSize = (PaperSize)cboPaperSizes.SelectedItem;
			printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = (PaperSize)cboPaperSizes.SelectedItem;
			calculatePages();	// Added by James 12-10-06
			printPreviewControl1.InvalidatePreview();
			
			DisplayMarginDialog(false);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if(cboPaperSizes.Text.Trim()=="")
			{
				MessageBox.Show("Kindly enter the Paper Name to delete.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if(MessageBox.Show("Are you sure to delete this paper size?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				MJMCustomPrintForm.MJMCustomPrintForm.DeleteCustomPaperSize(printDocument1.PrinterSettings.PrinterName, cboPaperSizes.Text, float.Parse(nUdnWidth.Value.ToString()), float.Parse(nUdnHeight.Value.ToString()));
				loadPaperSizes();
			}
		}

		private void cboPaperSizes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PaperSize size = (PaperSize)cboPaperSizes.SelectedItem;
			nUdnHeight.Value	= (decimal)size.Height / (decimal)3.93666666667;
			nUdnWidth.Value		= (decimal)size.Width / (decimal)3.93666666667;
		}

	}
}
