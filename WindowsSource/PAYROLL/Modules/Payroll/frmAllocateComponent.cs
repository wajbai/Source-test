//******************************************************************************************
//Object Name     : frmAllocateComponent.cs
//Purpose         : To Allocate Components for the available Groups
//Date from       : 21-Feb-2007
//Author          : S.Chandrasekar
//Modified	By	  : GM
//******************************************************************************************
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Payroll.Model.UIModel;
using PAYROLL.UserControl;
using Bosco.Utility.Common;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;


namespace PAYROLL.Modules.Payroll
{
	/// <summary>
	/// Summary description for frmAllocateComponent.
	/// </summary>
	public class frmAllocateComponent : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel pnlTitle;
		private System.Windows.Forms.Label lblMonthYear;
		private System.Windows.Forms.Label lblUser;
		private System.Windows.Forms.Label lblComponent;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private UserControl.clsWorkSheet dgGroups;
		private clsAllocateComponent objAllocateComp=new clsAllocateComponent();
	//	private DataHandling objDBHand = new DataHandling();
		private DataTable dt=new DataTable();
		private DataTable dtable=new DataTable();
		private DataView dvComponentAllocate = new DataView();
		private clsEvalExpr objEvalExpr = new clsEvalExpr();
		private clsPrComponent objComponent = new clsPrComponent();
		private long igetId = 0,iCompId;
		private int getRowId = 0;
		private int iGroupId;
		private int getvalue=0,getDvalue=0;
		private int compIndex;
		private clsWorkSheet dgComponents;
		private int iPreviousRow = 0;
		private DataTable getds;
		//variable to fetch the constructor arguments..
		private  UserControl.ucGrid ucGetGroup;
		private Label lblGroup     = null;
		private ListBox lstGetGroup= null;
		private Button btnProcess  = null;
		private Button btnSave     = null;
		private Button btnIncrease = null;
		private Button btnDecrease = null;
		private Panel pnlBtns;
		//-----------------------------------------
		private clsPayrollComponent objPayrollComp=new clsPayrollComponent();
		private System.Windows.Forms.Label lblTips;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem mnuSelect;
		private System.Windows.Forms.MenuItem mnuSelectAll;
		private System.Windows.Forms.MenuItem mnuDeselect;
		private System.Windows.Forms.MenuItem mnuDeselectAll;
		private System.Windows.Forms.Button btnClose;
		private System.ComponentModel.IContainer components;
        ApplicationSchema.PRCOMPONENTDataTable dtComptbl = null;
		public frmAllocateComponent()
		{
			InitializeComponent();
		}
		public frmAllocateComponent(Button btn,Button btn1,ucGrid getucGroup,ListBox lst,Label lbl,Button btnInc,Button btnDec,Panel pnl,DataTable ds)
		{
			InitializeComponent();
			this.lstGetGroup = lst;
			this.ucGetGroup  = getucGroup;
			this.lblGroup    = lbl;
			this.btnProcess  = btn;
			this.btnSave     = btn1;
			this.btnIncrease = btnInc;
			this.btnDecrease = btnDec;
			this.pnlBtns	 = pnl;
			this.getds = ds;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblComponent = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.mnuSelect = new System.Windows.Forms.MenuItem();
            this.mnuSelectAll = new System.Windows.Forms.MenuItem();
            this.mnuDeselect = new System.Windows.Forms.MenuItem();
            this.mnuDeselectAll = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTips = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgComponents = new PAYROLL.UserControl.clsWorkSheet();
            this.dgGroups = new PAYROLL.UserControl.clsWorkSheet();
            this.pnlTitle.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgComponents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(225)))), ((int)(((byte)(251)))));
            this.pnlTitle.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.pnlTitle.Controls.Add(this.lblMonthYear);
            this.pnlTitle.Controls.Add(this.lblUser);
            this.pnlTitle.Controls.Add(this.lblComponent);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Location = new System.Drawing.Point(-76, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1095, 32);
            this.pnlTitle.TabIndex = 199;
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.AutoSize = true;
            this.lblMonthYear.BackColor = System.Drawing.Color.Transparent;
            this.lblMonthYear.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthYear.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMonthYear.Location = new System.Drawing.Point(353, 7);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(136, 18);
            this.lblMonthYear.TabIndex = 124;
            this.lblMonthYear.Text = "<Month Year>";
            // 
            // lblUser
            // 
            this.lblUser.BackColor = System.Drawing.Color.SeaShell;
            this.lblUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblUser.Location = new System.Drawing.Point(8, 4);
            this.lblUser.Name = "lblUser";
            this.lblUser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUser.Size = new System.Drawing.Size(59, 23);
            this.lblUser.TabIndex = 123;
            this.lblUser.Text = "User :";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.BackColor = System.Drawing.Color.Transparent;
            this.lblComponent.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComponent.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblComponent.Location = new System.Drawing.Point(736, 5);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(174, 18);
            this.lblComponent.TabIndex = 7;
            this.lblComponent.Text = "Payroll Component";
            this.lblComponent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTitle.Location = new System.Drawing.Point(80, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(267, 18);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Payroll Group Component For";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 18);
            this.label1.TabIndex = 200;
            this.label1.Text = "Components";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(388, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 18);
            this.label2.TabIndex = 201;
            this.label2.Text = "Payroll Groups";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSelect,
            this.mnuSelectAll,
            this.mnuDeselect,
            this.mnuDeselectAll});
            // 
            // mnuSelect
            // 
            this.mnuSelect.Index = 0;
            this.mnuSelect.Text = "Select";
            this.mnuSelect.Click += new System.EventHandler(this.mnuSelect_Click);
            // 
            // mnuSelectAll
            // 
            this.mnuSelectAll.Index = 1;
            this.mnuSelectAll.Text = "Select All";
            this.mnuSelectAll.Click += new System.EventHandler(this.mnuSelectAll_Click);
            // 
            // mnuDeselect
            // 
            this.mnuDeselect.Index = 2;
            this.mnuDeselect.Text = "Deselect";
            this.mnuDeselect.Click += new System.EventHandler(this.mnuDeselect_Click);
            // 
            // mnuDeselectAll
            // 
            this.mnuDeselectAll.Index = 3;
            this.mnuDeselectAll.Text = "Deselect All";
            this.mnuDeselectAll.Click += new System.EventHandler(this.mnuDeselectAll_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTips);
            this.panel1.Location = new System.Drawing.Point(1, 516);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 62);
            this.panel1.TabIndex = 204;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnClose.Location = new System.Drawing.Point(776, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 24);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.toolTip1.SetToolTip(this.btnClose, "Close Component Allocation Window");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTips.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTips.Location = new System.Drawing.Point(8, 24);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(287, 13);
            this.lblTips.TabIndex = 2;
            this.lblTips.Text = "Select a Component and Allocate  Group(s)";
            this.toolTip1.SetToolTip(this.lblTips, "Close component allotment window");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 578);
            this.splitter1.TabIndex = 205;
            this.splitter1.TabStop = false;
            // 
            // dgComponents
            // 
            this.dgComponents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgComponents.CaptionVisible = false;
            this.dgComponents.ColorReadOnlyColumns = true;
            this.dgComponents.DataMember = "";
            this.dgComponents.FlatMode = true;
            this.dgComponents.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgComponents.Location = new System.Drawing.Point(8, 67);
            this.dgComponents.Name = "dgComponents";
            this.dgComponents.Size = new System.Drawing.Size(380, 449);
            this.dgComponents.TabIndex = 206;
            this.dgComponents.Click += new System.EventHandler(this.dgComponents_Click);
            this.dgComponents.CurrentCellChanged += new System.EventHandler(this.dgComponents_CurrentCellChanged);
            // 
            // dgGroups
            // 
            this.dgGroups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgGroups.CaptionVisible = false;
            this.dgGroups.ColorReadOnlyColumns = true;
            this.dgGroups.ContextMenu = this.contextMenu1;
            this.dgGroups.DataMember = "";
            this.dgGroups.FlatMode = true;
            this.dgGroups.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgGroups.Location = new System.Drawing.Point(390, 67);
            this.dgGroups.Name = "dgGroups";
            this.dgGroups.Size = new System.Drawing.Size(458, 447);
            this.dgGroups.TabIndex = 202;
            this.dgGroups.ColumnChanged += new System.Data.DataColumnChangeEventHandler(this.dgGroups_ColumnChanged);
            // 
            // frmAllocateComponent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(849, 578);
            this.Controls.Add(this.dgComponents);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgGroups);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAllocateComponent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payroll Groupwise Component Allotment";
            this.Load += new System.EventHandler(this.frmAllocateComponent_Load);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgComponents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        ResultArgs resultArgs = null;
		private int GetVal
		{
			get{return getvalue;}
			set{getvalue=value;}
		}
		private int GetDVal
		{	
			get{return getDvalue;}
			set{getDvalue=value;}
		}
		//Make Grid Entry
		//0-COLUMNNAME			(NAME1, NAME2,...)
		//1-LOOKUPCOLUMN		(TRUE or FALSE) At Maximum of one column is possible
		//2-MANDATORYCOLUMN		(TRUE or FALSE)
		//3-VALIDATIONCOLUMN	(TRUE or FALSE)
		//4-READONLY			(TRUE or FALSE)
		//5-MAXLENGTH			(0, 20, 30, ...) Set Maxlength for only string column, for other datatypes set to 0
		//6-COLUMNWIDTH			(30, 40, 50, ...)
		//7-DATATYPE			(INT, BOOLEAN, , ...
		//8-Defailt Value		(				)						 
		//9-UNIQUE				(TRUE FALSE))
		//
		private void fillGrids()
		{
            DataTable dtComponent = null;
            object getQr = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMPONENT_LIST);
            using (DataManager dataManager = new DataManager(getQr, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtComponent = resultArgs.DataSource.Table;
            }
           // objDBHand = new DataHandling(getQr, "Component");
            //MessageBox.Show(objDBHand.getRecordCount().ToString()); //shows no of Components..
            if (dtComponent == null || dtComponent.Rows.Count <=0)
            {
                this.dgComponents.Enabled = false;
                this.dgGroups.Enabled = false;
                MessageBox.Show("No Records found for Components..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgComponents.DataSource = new DataView(dtComponent);
                string[,] TextBoxProperty1 = new string[,]{
                                                            {"COMPONENTID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"},
                                                            {"COMPONENTNAME","FALSE","TRUE","TRUE","FALSE","100","200","STRING","","FALSE"}
                                                          };
                dgComponents.AllowAdd = dgComponents.AllowDelete = dgComponents.AllowEdit = false;
                dgComponents.CreateGridTextBox("Component", TextBoxProperty1);
            }
            //string getQuery = "Select * from prloanpaid";
            DataTable dtGroup = null;
            object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_GETGROUP_LIST);
            using (DataManager dataManager = new DataManager(getQuery, "Group"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtGroup = resultArgs.DataSource.Table;
            }
            //objDBHand = new DataHandling(getQuery, "Group");
            //MessageBox.Show(objDBHand.getRecordCount().ToString()); //shows no of Grades..
            if (dtGroup == null || dtGroup.Rows.Count <= 0)
            {
                this.dgComponents.Enabled = false;
                this.dgGroups.Enabled = false;
                MessageBox.Show("No Records found for Grade..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                dgGroups.DataSource = new DataView(dtGroup);
                string[,] TextBoxProperty = new string[,]{
                                                            {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                                                            {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                                                            {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                                                         };
                dgGroups.CreateGridTextBox("Group", TextBoxProperty);
            }
		}
		//When first time the form is loaded, to view the selected groups for the first selected component.
		private void SelectGroup(object sender,System.EventArgs e)
		{
			dgComponents_CurrentCellChanged(sender,e);
		}
		private void frmAllocateComponent_Load(object sender, System.EventArgs e)
		{
            lblMonthYear.Text = clsGeneral.PAYROLL_MONTH;
            this.Top = 112;
            this.Left = 168;
            fillGrids();
            dgGroups.AllowAdd = false;
            SelectGroup(sender, e);
		}
		private void btnClose_Click(object sender, System.EventArgs e)
		{
            try
            {
                igetId = long.Parse(lstGetGroup.SelectedValue.ToString());
                lblGroup.Text = lstGetGroup.Text.ToString();
                ucGetGroup.HideFilter = true;
                ucGetGroup.ShowGrid(objPayrollComp.getComponentDetails(igetId), "Component");
                this.getds = objPayrollComp.getComponentDetails(igetId);
                try
                {
                    if (ucGetGroup.RecordCount >= 2)
                    {
                        btnIncrease.Visible = true;
                        btnDecrease.Visible = true;
                        pnlBtns.Visible = true;
                    }
                    else
                    {
                        btnIncrease.Visible = false;
                        btnDecrease.Visible = false;
                        pnlBtns.Visible = false;
                    }
                    if (ucGetGroup.RecordCount == 0)
                    {
                        btnProcess.Enabled = false;
                        btnSave.Enabled = false;
                        btnIncrease.Enabled = false;
                        btnDecrease.Enabled = false;
                        this.Close();
                    }
                    else
                    {
                        btnProcess.Enabled = true;
                        btnSave.Enabled = true;
                        btnProcess.Enabled = true;
                        btnProcess.Enabled = true;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    btnProcess.Enabled = false;
                    btnSave.Enabled = false;
                    btnIncrease.Enabled = false;
                    btnDecrease.Enabled = false;
                    btnIncrease.Visible = false;
                    btnDecrease.Visible = false;
                    pnlBtns.Visible = false;
                    this.Close();

                }
            }
            catch { this.Close(); }
		}
		//When a Group is Selected, To make entry in DB
		private void dgGroups_ColumnChanged(object sender, System.Data.DataColumnChangeEventArgs e)
		{
            dtable = ((DataView)dgGroups.DataSource).Table;
            igetId = Convert.ToInt32(clsGeneral.PAYROLL_ID);
            iCompId = Convert.ToInt32(dgComponents[iPreviousRow, 0]);

            if (dtable.Rows[dgGroups.CurrentRowIndex][0].ToString() == "1")
            {
                iGroupId = Convert.ToInt32(dtable.Rows[dgGroups.CurrentRowIndex][2]);

                //if(VerifyCircularReference(igetId, iGroupId,iCompId))return;

                dt = objAllocateComp.getCompDetails(iCompId);

                try
                {
                    objComponent.SaveGroupComponent(igetId, iCompId, dt, iGroupId.ToString());
                }
                catch
                {
                    MessageBox.Show("Component Not Mapped to a Grade", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (dtable.Rows[dgGroups.CurrentRowIndex][0].ToString() == "0")
            {
                iGroupId = Convert.ToInt32(dtable.Rows[dgGroups.CurrentRowIndex][2]);
                dt = objAllocateComp.getCompDetails(iCompId);
                try
                {
                    if (objAllocateComp.InsertSelect(igetId, iCompId, iGroupId) != 0)
                        objAllocateComp.DeleteInserted(igetId, iCompId, iGroupId);
                }
                catch
                {
                    MessageBox.Show(" Grade Not DeSeleted", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
		}
		
		private int getCompIndex
		{
			get{return compIndex;}
			set{compIndex=value;}
		}

		//When a Components is selected, to View already Selected Group(s)
		private void dgComponents_CurrentCellChanged(object sender, System.EventArgs e)
		{
			try
			{
                if (dgGroups.getSourceTable != null)
                    dgGroups.CurrentCell = dgGroups.getSourceTable.Rows.Count > 0 ? new DataGridCell(0, 0) : new DataGridCell(dgGroups.getSourceTable.Rows.Count, 0);
               if(dgComponents.getSourceTable!=null)
                    getRowId = Convert.ToInt32(dgComponents[dgComponents.CurrentRowIndex, 0]);
                //MessageBox.Show(compIndex.ToString());
                object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMP_CHANGE);
                DataTable dtGetGroup = null;
                //getQuery = getQuery.Replace("<compid>", getRowId.ToString());
                //MessageBox.Show(this.dgComponents[dgComponents.CurrentRowIndex,1].ToString());
                //getQuery = getQuery.Replace("<payrollid>", clsGeneral.PAYROLL_ID.ToString());
               /* using (DataManager dataManager = new DataManager(getQuery, "Group"))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtComptbl.PAYROLLIDColumn, clsGeneral.PAYROLL_ID.ToString());
                    dataManager.Parameters.Add(dtComptbl.COMPONENTIDColumn, getRowId.ToString());*/
                    clsPrComponent objcompallocate=new clsPrComponent();
                  DataTable dt = objcompallocate.CompAllocateQuery(long.Parse(clsGeneral.PAYROLL_ID.ToString()), long.Parse(getRowId.ToString()));
                  if (dt.Rows.Count>0)
                        dtGetGroup = dt;
               // }
               // DataHandling objDBHand = new DataHandling(getQuery, "Group");
                if (dtGetGroup != null && dtGetGroup.Rows.Count>0)
                {
                    dgGroups.DataSource = new DataView(dtGetGroup);
                    string[,] TextBoxProperty = new string[,]{
                                                            {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                                                            {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                                                            {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                                                             };
                    dgGroups.CreateGridTextBox("Group", TextBoxProperty);
                }
                else
                {
                    DataTable dtGRoup = null;
                    getQuery =objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_GETGROUP_LIST);
                    using (DataManager dataManager = new DataManager(getQuery, "Group"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                            dtGRoup = resultArgs.DataSource.Table;
                    }
                    //getQuery = getQuery.Replace("<compid>", compIndex.ToString());
                   // objDBHand = new DataHandling(getQuery, "Group");
                    dgGroups.DataSource = new DataView(dtGRoup);
                    string[,] TextBoxProperty = new string[,]{
                                                            {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                                                            {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                                                            {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                                                             };
                    dgGroups.CreateGridTextBox("Group", TextBoxProperty);
                }
                iPreviousRow = dgComponents.CurrentRowIndex;
			}
			catch
			{}
		}
		//To Select a Single Item from dgGroups
		private void mnuSelect_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataView dv = (DataView)dgGroups.DataSource;
				if(dv.Count < 0)
				{
					MessageBox.Show("No Grade Available","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				int currentRow = dgGroups.CurrentRowIndex;
				dv.Table.Rows[currentRow][0] = "1"; //calls dggroups_columnchanged automatically..
				dgGroups.Select(currentRow); 
			}
			catch{}
		}
		//To DeSelect a Single Item from dgGroups
		private void mnuDeselect_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataView dv = (DataView)dgGroups.DataSource;
				if(dv.Count < 0)
				{
					MessageBox.Show("No Grade Available","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				int currentRow = dgGroups.CurrentRowIndex;
				dv.Table.Rows[currentRow][0] = "0"; //calls dggroups_columnchanged automatically..
				dgGroups.Select(currentRow);
			}
			catch{}
		}
		//To Select All the Items from dgGroups
		private void mnuSelectAll_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataView dv = (DataView)dgGroups.DataSource;
				if(dv.Count < 0)
				{
					MessageBox.Show("No Grade Available","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				int currentRow = 0;
				for(int i=0;i<dv.Count;i++)
				{
					currentRow = i;
					dgGroups.CurrentRowIndex = currentRow;
					dv.Table.Rows[currentRow][0] = "1"; //calls dggroups_columnchanged automatically..
					dgGroups.Select(currentRow);
				}
			}
			catch{}
		}
		//To DeSelect All the Items from dgGroups
		private void mnuDeselectAll_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataView dv = (DataView)dgGroups.DataSource;
				if(dv.Count < 0)
				{
					MessageBox.Show("No Grade Available","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				int currentRow = 0;
				for(int i=0;i<dv.Count;i++)
				{
					currentRow = i;
					dgGroups.CurrentRowIndex = currentRow;
					dv.Table.Rows[currentRow][0] = "0"; //calls dggroups_columnchanged automatically..
					dgGroups.Select(currentRow);
				}
			}
			catch{}
		}

		private void dgComponents_Click(object sender, System.EventArgs e)
		{
			try
			{
				dgComponents.SelectionBackColor = Color.DarkBlue;
				dgComponents.Select(dgComponents.CurrentRowIndex);
			}
			catch{}
		}

		
	}
}
