/*****************************************************************************************
*					Interface       : frmBuildIFCondition
*					Purpose         : Construct the New equation for PayRoll.
*					Object Involved : clsPrCompBuild
					Date from       : 18-Feb-2007
*					Author          : GP
*					Modified by     : GP
*****************************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Bosco.Utility.Common;
using Bosco.Utility.CommonMemberSet;
using Payroll.Model.UIModel;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;


namespace PAYROLL.Modules.Payroll
{
	public class frmBuildIFCondition : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoIfThen;
		private System.Windows.Forms.RadioButton rdoIfAndThen;
		private System.Windows.Forms.RadioButton rdoIfElseThen;
		private System.Windows.Forms.RadioButton rdoFormula;
		private System.Windows.Forms.GroupBox grpIfCondition;
		private System.Windows.Forms.Label lblIf;
		private System.Windows.Forms.TextBox txtOperator1;
		private System.Windows.Forms.TextBox txtValue1;
		private System.Windows.Forms.TextBox txtLogicalOperator;
		private System.Windows.Forms.Label lblThen;
		private System.Windows.Forms.Label lblElse;
		public System.Windows.Forms.TextBox txtFormula1;
		public System.Windows.Forms.TextBox txtFormula2;
		private System.Windows.Forms.Label lblEndIf;
		private System.Windows.Forms.Button btnBuildFormula1;
		private System.Windows.Forms.Button btnBuildFormula2;
		public System.Windows.Forms.Label lblComponentName;
		private System.Windows.Forms.TextBox txtValue2;
		private System.Windows.Forms.Label lblThen2;
		private System.Windows.Forms.RadioButton rdoIfAndElseThen;
		private System.Windows.Forms.TextBox txtOperator2;
		private System.Windows.Forms.TextBox txtComponent;
		private System.Windows.Forms.TextBox txtComponent1;
		private System.Windows.Forms.Label lblComponentName1;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.ListBox lbComponent;
		private System.Windows.Forms.ListBox lbLogicalOperator;
		private System.Windows.Forms.ListBox lstbOperator2;
		private System.Windows.Forms.ListBox lstbOperator1;
		private System.Windows.Forms.Button btnCancel;
		private System.ComponentModel.IContainer components;
		private string strOperator="";
		private string strOperator1="";
		public string strEquation="";
		private string strCheck="";
		public Hashtable htFormula=new Hashtable();
		private Hashtable htID=new Hashtable();
		private clsPayrollComponent objComp=new clsPayrollComponent();
		private System.Windows.Forms.ToolTip toolTipConditions;
		private clsprCompBuild objBuild =new clsprCompBuild();
		private RadioButton rdoActiveButton = new RadioButton();
		private System.Windows.Forms.ListBox lstFormula;
		private System.Windows.Forms.ComboBox cboCategory;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Panel pnlIncreaseDecrease;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button btnAllocateFormulaGroup;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnRemoveFromList;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnSavetoList;
		private System.Windows.Forms.Button btnSave;

		private bool newFormula = true;
		

		#region Variables
        ListSetMember objCommon = new ListSetMember();
		public frmComponent Parent;
		private Regex objR=new Regex("[0-9]|\b");
		private string ActiveControl="";
		private string strOption="";
		public string ComponentValue1="";
		public string ComponentValue2="";
		private string strLogical="";
		private System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.Button btnMoveUp;
		public int Comp=1;	//1- To build the equation for formula(comp1), 2- To build the equation for comp(2)
		private long CompId = 0;

		#endregion

		#region Constructor
		public frmBuildIFCondition()
		{
			InitializeComponent();

		}

		public frmBuildIFCondition(frmComponent frm,string strOperation,RadioButton rdoActiveControl)
		{
			
			InitializeComponent();
			Parent                 = frm;
			lblComponentName.Text  = Parent.txtComponent.Text+" =";
			rdoActiveButton        = rdoActiveControl ;
			
			strCheck               = strOperation;
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

		#region Windows Form Designer generated code
		
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuildIFCondition));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoFormula = new System.Windows.Forms.RadioButton();
            this.rdoIfAndElseThen = new System.Windows.Forms.RadioButton();
            this.rdoIfElseThen = new System.Windows.Forms.RadioButton();
            this.rdoIfAndThen = new System.Windows.Forms.RadioButton();
            this.rdoIfThen = new System.Windows.Forms.RadioButton();
            this.grpIfCondition = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSavetoList = new System.Windows.Forms.Button();
            this.lstbOperator1 = new System.Windows.Forms.ListBox();
            this.lstbOperator2 = new System.Windows.Forms.ListBox();
            this.lbLogicalOperator = new System.Windows.Forms.ListBox();
            this.lbComponent = new System.Windows.Forms.ListBox();
            this.lblComponentName1 = new System.Windows.Forms.Label();
            this.txtComponent1 = new System.Windows.Forms.TextBox();
            this.lblThen2 = new System.Windows.Forms.Label();
            this.lblComponentName = new System.Windows.Forms.Label();
            this.btnBuildFormula2 = new System.Windows.Forms.Button();
            this.btnBuildFormula1 = new System.Windows.Forms.Button();
            this.lblEndIf = new System.Windows.Forms.Label();
            this.txtFormula2 = new System.Windows.Forms.TextBox();
            this.txtFormula1 = new System.Windows.Forms.TextBox();
            this.lblElse = new System.Windows.Forms.Label();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.txtOperator2 = new System.Windows.Forms.TextBox();
            this.txtLogicalOperator = new System.Windows.Forms.TextBox();
            this.txtValue1 = new System.Windows.Forms.TextBox();
            this.txtOperator1 = new System.Windows.Forms.TextBox();
            this.txtComponent = new System.Windows.Forms.TextBox();
            this.lblIf = new System.Windows.Forms.Label();
            this.lblThen = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.toolTipConditions = new System.Windows.Forms.ToolTip(this.components);
            this.btnAllocateFormulaGroup = new System.Windows.Forms.Button();
            this.btnRemoveFromList = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lstFormula = new System.Windows.Forms.ListBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.pnlIncreaseDecrease = new System.Windows.Forms.Panel();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpIfCondition.SuspendLayout();
            this.pnlIncreaseDecrease.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.rdoFormula);
            this.groupBox1.Controls.Add(this.rdoIfAndElseThen);
            this.groupBox1.Controls.Add(this.rdoIfElseThen);
            this.groupBox1.Controls.Add(this.rdoIfAndThen);
            this.groupBox1.Controls.Add(this.rdoIfThen);
            this.groupBox1.Location = new System.Drawing.Point(2, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.toolTipConditions.SetToolTip(this.groupBox1, "Select a Condition");
            // 
            // rdoFormula
            // 
            this.rdoFormula.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rdoFormula.Checked = true;
            this.rdoFormula.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoFormula.Location = new System.Drawing.Point(12, 151);
            this.rdoFormula.Name = "rdoFormula";
            this.rdoFormula.Size = new System.Drawing.Size(84, 17);
            this.rdoFormula.TabIndex = 4;
            this.rdoFormula.TabStop = true;
            this.rdoFormula.Text = "FORMULA";
            this.rdoFormula.UseVisualStyleBackColor = false;
            this.rdoFormula.Click += new System.EventHandler(this.rdoFormula_Click);
            this.rdoFormula.CheckedChanged += new System.EventHandler(this.rdoFormula_CheckedChanged);
            // 
            // rdoIfAndElseThen
            // 
            this.rdoIfAndElseThen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rdoIfAndElseThen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoIfAndElseThen.Location = new System.Drawing.Point(12, 118);
            this.rdoIfAndElseThen.Name = "rdoIfAndElseThen";
            this.rdoIfAndElseThen.Size = new System.Drawing.Size(176, 17);
            this.rdoIfAndElseThen.TabIndex = 3;
            this.rdoIfAndElseThen.Text = "IF .... AND....ELSE ....THEN";
            this.rdoIfAndElseThen.UseVisualStyleBackColor = false;
            this.rdoIfAndElseThen.Click += new System.EventHandler(this.IfAndElseThen_Click);
            this.rdoIfAndElseThen.CheckedChanged += new System.EventHandler(this.rdoIfAndElseThen_CheckedChanged);
            // 
            // rdoIfElseThen
            // 
            this.rdoIfElseThen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rdoIfElseThen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoIfElseThen.Location = new System.Drawing.Point(12, 85);
            this.rdoIfElseThen.Name = "rdoIfElseThen";
            this.rdoIfElseThen.Size = new System.Drawing.Size(176, 17);
            this.rdoIfElseThen.TabIndex = 2;
            this.rdoIfElseThen.Text = "IF .... ELSE ....THEN";
            this.rdoIfElseThen.UseVisualStyleBackColor = false;
            this.rdoIfElseThen.Click += new System.EventHandler(this.rdoIfElseThen_Click);
            this.rdoIfElseThen.CheckedChanged += new System.EventHandler(this.rdoIfElseThen_CheckedChanged);
            // 
            // rdoIfAndThen
            // 
            this.rdoIfAndThen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rdoIfAndThen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoIfAndThen.Location = new System.Drawing.Point(12, 52);
            this.rdoIfAndThen.Name = "rdoIfAndThen";
            this.rdoIfAndThen.Size = new System.Drawing.Size(176, 17);
            this.rdoIfAndThen.TabIndex = 1;
            this.rdoIfAndThen.Text = "IF .... AND ....THEN";
            this.rdoIfAndThen.UseVisualStyleBackColor = false;
            this.rdoIfAndThen.Click += new System.EventHandler(this.rdoIfAndThen_Click);
            this.rdoIfAndThen.CheckedChanged += new System.EventHandler(this.rdoIfAndThen_CheckedChanged);
            // 
            // rdoIfThen
            // 
            this.rdoIfThen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rdoIfThen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoIfThen.Location = new System.Drawing.Point(12, 24);
            this.rdoIfThen.Name = "rdoIfThen";
            this.rdoIfThen.Size = new System.Drawing.Size(176, 17);
            this.rdoIfThen.TabIndex = 0;
            this.rdoIfThen.Text = "IF .... THEN";
            this.rdoIfThen.UseVisualStyleBackColor = false;
            this.rdoIfThen.Click += new System.EventHandler(this.rdoIfThen_Click);
            this.rdoIfThen.CheckedChanged += new System.EventHandler(this.rdoIfThen_CheckedChanged);
            // 
            // grpIfCondition
            // 
            this.grpIfCondition.BackColor = System.Drawing.Color.White;
            this.grpIfCondition.Controls.Add(this.btnNew);
            this.grpIfCondition.Controls.Add(this.btnSavetoList);
            this.grpIfCondition.Controls.Add(this.lstbOperator1);
            this.grpIfCondition.Controls.Add(this.lstbOperator2);
            this.grpIfCondition.Controls.Add(this.lbLogicalOperator);
            this.grpIfCondition.Controls.Add(this.lbComponent);
            this.grpIfCondition.Controls.Add(this.lblComponentName1);
            this.grpIfCondition.Controls.Add(this.txtComponent1);
            this.grpIfCondition.Controls.Add(this.lblThen2);
            this.grpIfCondition.Controls.Add(this.lblComponentName);
            this.grpIfCondition.Controls.Add(this.btnBuildFormula2);
            this.grpIfCondition.Controls.Add(this.btnBuildFormula1);
            this.grpIfCondition.Controls.Add(this.lblEndIf);
            this.grpIfCondition.Controls.Add(this.txtFormula2);
            this.grpIfCondition.Controls.Add(this.txtFormula1);
            this.grpIfCondition.Controls.Add(this.lblElse);
            this.grpIfCondition.Controls.Add(this.txtValue2);
            this.grpIfCondition.Controls.Add(this.txtOperator2);
            this.grpIfCondition.Controls.Add(this.txtLogicalOperator);
            this.grpIfCondition.Controls.Add(this.txtValue1);
            this.grpIfCondition.Controls.Add(this.txtOperator1);
            this.grpIfCondition.Controls.Add(this.txtComponent);
            this.grpIfCondition.Controls.Add(this.lblIf);
            this.grpIfCondition.Controls.Add(this.lblThen);
            this.grpIfCondition.Location = new System.Drawing.Point(194, 136);
            this.grpIfCondition.Name = "grpIfCondition";
            this.grpIfCondition.Size = new System.Drawing.Size(478, 188);
            this.grpIfCondition.TabIndex = 2;
            this.grpIfCondition.TabStop = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNew.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnNew.Enabled = false;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnNew.Location = new System.Drawing.Point(407, 160);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(64, 24);
            this.btnNew.TabIndex = 27;
            this.btnNew.Text = "&New";
            this.toolTipConditions.SetToolTip(this.btnNew, "Save record");
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSavetoList
            // 
            this.btnSavetoList.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSavetoList.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnSavetoList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavetoList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSavetoList.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSavetoList.Location = new System.Drawing.Point(312, 160);
            this.btnSavetoList.Name = "btnSavetoList";
            this.btnSavetoList.Size = new System.Drawing.Size(88, 24);
            this.btnSavetoList.TabIndex = 26;
            this.btnSavetoList.Text = "&Save to list";
            this.toolTipConditions.SetToolTip(this.btnSavetoList, "Save record");
            this.btnSavetoList.UseVisualStyleBackColor = false;
            this.btnSavetoList.Click += new System.EventHandler(this.btnSavetoList_Click);
            // 
            // lstbOperator1
            // 
            this.lstbOperator1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstbOperator1.Items.AddRange(new object[] {
            "=",
            ">",
            "<",
            ">=",
            "<=",
            "<>"});
            this.lstbOperator1.Location = new System.Drawing.Point(157, 36);
            this.lstbOperator1.Name = "lstbOperator1";
            this.lstbOperator1.Size = new System.Drawing.Size(35, 80);
            this.lstbOperator1.TabIndex = 3;
            this.lstbOperator1.SelectedIndexChanged += new System.EventHandler(this.lstbOperator1_SelectedIndexChanged);
            this.lstbOperator1.MouseLeave += new System.EventHandler(this.lstbOperator1_MouseLeave);
            // 
            // lstbOperator2
            // 
            this.lstbOperator2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstbOperator2.Items.AddRange(new object[] {
            "=",
            ">",
            "<",
            ">=",
            "<=",
            "<>"});
            this.lstbOperator2.Location = new System.Drawing.Point(157, 60);
            this.lstbOperator2.Name = "lstbOperator2";
            this.lstbOperator2.Size = new System.Drawing.Size(35, 80);
            this.lstbOperator2.TabIndex = 6;
            this.lstbOperator2.SelectedIndexChanged += new System.EventHandler(this.lstbOperator2_SelectedIndexChanged);
            this.lstbOperator2.MouseLeave += new System.EventHandler(this.lstbOperator2_MouseLeave);
            // 
            // lbLogicalOperator
            // 
            this.lbLogicalOperator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLogicalOperator.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.lbLogicalOperator.Location = new System.Drawing.Point(280, 37);
            this.lbLogicalOperator.Name = "lbLogicalOperator";
            this.lbLogicalOperator.Size = new System.Drawing.Size(35, 28);
            this.lbLogicalOperator.TabIndex = 8;
            this.lbLogicalOperator.SelectedIndexChanged += new System.EventHandler(this.lbLogicalOperator_SelectedIndexChanged);
            this.lbLogicalOperator.MouseLeave += new System.EventHandler(this.lbLogicalOperator_MouseLeave);
            // 
            // lbComponent
            // 
            this.lbComponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbComponent.Location = new System.Drawing.Point(48, 36);
            this.lbComponent.Name = "lbComponent";
            this.lbComponent.Size = new System.Drawing.Size(107, 132);
            this.lbComponent.TabIndex = 1;
            this.lbComponent.SelectedIndexChanged += new System.EventHandler(this.lbComponent_SelectedIndexChanged);
            this.lbComponent.MouseLeave += new System.EventHandler(this.lbComponent_MouseLeave);
            // 
            // lblComponentName1
            // 
            this.lblComponentName1.AutoSize = true;
            this.lblComponentName1.Location = new System.Drawing.Point(8, 126);
            this.lblComponentName1.Name = "lblComponentName1";
            this.lblComponentName1.Size = new System.Drawing.Size(56, 13);
            this.lblComponentName1.TabIndex = 20;
            this.lblComponentName1.Text = "<NAME1>";
            // 
            // txtComponent1
            // 
            this.txtComponent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComponent1.Location = new System.Drawing.Point(48, 40);
            this.txtComponent1.Name = "txtComponent1";
            this.txtComponent1.ReadOnly = true;
            this.txtComponent1.Size = new System.Drawing.Size(107, 20);
            this.txtComponent1.TabIndex = 5;
            this.txtComponent1.Text = "{COMPONENT2}";
            this.toolTipConditions.SetToolTip(this.txtComponent1, "Choose component 2");
            this.txtComponent1.DoubleClick += new System.EventHandler(this.txtComponent1_DoubleClick);
            // 
            // lblThen2
            // 
            this.lblThen2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThen2.Location = new System.Drawing.Point(280, 40);
            this.lblThen2.Name = "lblThen2";
            this.lblThen2.Size = new System.Drawing.Size(43, 16);
            this.lblThen2.TabIndex = 18;
            this.lblThen2.Text = "THEN";
            // 
            // lblComponentName
            // 
            this.lblComponentName.AutoSize = true;
            this.lblComponentName.Location = new System.Drawing.Point(8, 72);
            this.lblComponentName.Name = "lblComponentName";
            this.lblComponentName.Size = new System.Drawing.Size(50, 13);
            this.lblComponentName.TabIndex = 15;
            this.lblComponentName.Text = "<NAME>";
            // 
            // btnBuildFormula2
            // 
            this.btnBuildFormula2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBuildFormula2.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnBuildFormula2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuildFormula2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuildFormula2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnBuildFormula2.Location = new System.Drawing.Point(386, 126);
            this.btnBuildFormula2.Name = "btnBuildFormula2";
            this.btnBuildFormula2.Size = new System.Drawing.Size(85, 24);
            this.btnBuildFormula2.TabIndex = 10;
            this.btnBuildFormula2.Text = "Build Formula";
            this.toolTipConditions.SetToolTip(this.btnBuildFormula2, "Construct Formula");
            this.btnBuildFormula2.UseVisualStyleBackColor = false;
            this.btnBuildFormula2.Click += new System.EventHandler(this.btnBuildFormula2_Click);
            // 
            // btnBuildFormula1
            // 
            this.btnBuildFormula1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBuildFormula1.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnBuildFormula1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuildFormula1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuildFormula1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnBuildFormula1.Location = new System.Drawing.Point(386, 72);
            this.btnBuildFormula1.Name = "btnBuildFormula1";
            this.btnBuildFormula1.Size = new System.Drawing.Size(85, 24);
            this.btnBuildFormula1.TabIndex = 9;
            this.btnBuildFormula1.Text = "Build Formula";
            this.toolTipConditions.SetToolTip(this.btnBuildFormula1, "To Construct Formula");
            this.btnBuildFormula1.UseVisualStyleBackColor = false;
            this.btnBuildFormula1.Click += new System.EventHandler(this.btnBuildFormula1_Click);
            // 
            // lblEndIf
            // 
            this.lblEndIf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndIf.Location = new System.Drawing.Point(8, 164);
            this.lblEndIf.Name = "lblEndIf";
            this.lblEndIf.Size = new System.Drawing.Size(43, 16);
            this.lblEndIf.TabIndex = 12;
            this.lblEndIf.Text = "END IF";
            // 
            // txtFormula2
            // 
            this.txtFormula2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFormula2.Location = new System.Drawing.Point(157, 126);
            this.txtFormula2.MaxLength = 600;
            this.txtFormula2.Name = "txtFormula2";
            this.txtFormula2.Size = new System.Drawing.Size(227, 20);
            this.txtFormula2.TabIndex = 10;
            // 
            // txtFormula1
            // 
            this.txtFormula1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFormula1.Location = new System.Drawing.Point(157, 72);
            this.txtFormula1.MaxLength = 600;
            this.txtFormula1.Name = "txtFormula1";
            this.txtFormula1.Size = new System.Drawing.Size(227, 20);
            this.txtFormula1.TabIndex = 8;
            // 
            // lblElse
            // 
            this.lblElse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElse.Location = new System.Drawing.Point(8, 103);
            this.lblElse.Name = "lblElse";
            this.lblElse.Size = new System.Drawing.Size(43, 14);
            this.lblElse.TabIndex = 9;
            this.lblElse.Text = "ELSE";
            // 
            // txtValue2
            // 
            this.txtValue2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValue2.Location = new System.Drawing.Point(205, 40);
            this.txtValue2.Name = "txtValue2";
            this.txtValue2.Size = new System.Drawing.Size(67, 20);
            this.txtValue2.TabIndex = 7;
            this.txtValue2.Text = "{VALUE2}";
            this.toolTipConditions.SetToolTip(this.txtValue2, "Enter value");
            this.txtValue2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValue2_KeyPress);
            // 
            // txtOperator2
            // 
            this.txtOperator2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperator2.Location = new System.Drawing.Point(157, 40);
            this.txtOperator2.Name = "txtOperator2";
            this.txtOperator2.ReadOnly = true;
            this.txtOperator2.Size = new System.Drawing.Size(43, 20);
            this.txtOperator2.TabIndex = 6;
            this.txtOperator2.Text = "{OPR2}";
            this.toolTipConditions.SetToolTip(this.txtOperator2, "Choose operator2");
            this.txtOperator2.DoubleClick += new System.EventHandler(this.txtOperator2_DoubleClick);
            // 
            // txtLogicalOperator
            // 
            this.txtLogicalOperator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogicalOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogicalOperator.Location = new System.Drawing.Point(280, 16);
            this.txtLogicalOperator.Name = "txtLogicalOperator";
            this.txtLogicalOperator.ReadOnly = true;
            this.txtLogicalOperator.Size = new System.Drawing.Size(35, 20);
            this.txtLogicalOperator.TabIndex = 4;
            this.txtLogicalOperator.Text = "AND";
            this.txtLogicalOperator.DoubleClick += new System.EventHandler(this.txtLogicalOperator_DoubleClick);
            // 
            // txtValue1
            // 
            this.txtValue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValue1.Location = new System.Drawing.Point(205, 16);
            this.txtValue1.Name = "txtValue1";
            this.txtValue1.Size = new System.Drawing.Size(67, 20);
            this.txtValue1.TabIndex = 3;
            this.txtValue1.Text = "{VALUE1}";
            this.toolTipConditions.SetToolTip(this.txtValue1, "Enter value");
            this.txtValue1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValue1_KeyPress);
            // 
            // txtOperator1
            // 
            this.txtOperator1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperator1.Location = new System.Drawing.Point(157, 16);
            this.txtOperator1.Name = "txtOperator1";
            this.txtOperator1.ReadOnly = true;
            this.txtOperator1.Size = new System.Drawing.Size(35, 20);
            this.txtOperator1.TabIndex = 2;
            this.txtOperator1.Text = "{OPR1}";
            this.toolTipConditions.SetToolTip(this.txtOperator1, "Choose operator");
            this.txtOperator1.DoubleClick += new System.EventHandler(this.txtOperator1_DoubleClick);
            // 
            // txtComponent
            // 
            this.txtComponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComponent.Location = new System.Drawing.Point(48, 16);
            this.txtComponent.Name = "txtComponent";
            this.txtComponent.ReadOnly = true;
            this.txtComponent.Size = new System.Drawing.Size(107, 20);
            this.txtComponent.TabIndex = 0;
            this.txtComponent.Text = "{COMPONENT1}";
            this.toolTipConditions.SetToolTip(this.txtComponent, "Choose component1");
            this.txtComponent.DoubleClick += new System.EventHandler(this.txtComponent_DoubleClick);
            // 
            // lblIf
            // 
            this.lblIf.AutoSize = true;
            this.lblIf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIf.Location = new System.Drawing.Point(8, 16);
            this.lblIf.Name = "lblIf";
            this.lblIf.Size = new System.Drawing.Size(17, 14);
            this.lblIf.TabIndex = 0;
            this.lblIf.Text = "IF";
            // 
            // lblThen
            // 
            this.lblThen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThen.Location = new System.Drawing.Point(320, 16);
            this.lblThen.Name = "lblThen";
            this.lblThen.Size = new System.Drawing.Size(43, 16);
            this.lblThen.TabIndex = 8;
            this.lblThen.Text = "THEN";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.Location = new System.Drawing.Point(680, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.Location = new System.Drawing.Point(680, 264);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.toolTipConditions.SetToolTip(this.btnSave, "Save record");
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.White;
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(2, 336);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(742, 29);
            this.lblDescription.TabIndex = 20;
            this.lblDescription.Text = "<Description>";
            // 
            // btnAllocateFormulaGroup
            // 
            this.btnAllocateFormulaGroup.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAllocateFormulaGroup.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnAllocateFormulaGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllocateFormulaGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllocateFormulaGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnAllocateFormulaGroup.Location = new System.Drawing.Point(304, 104);
            this.btnAllocateFormulaGroup.Name = "btnAllocateFormulaGroup";
            this.btnAllocateFormulaGroup.Size = new System.Drawing.Size(128, 24);
            this.btnAllocateFormulaGroup.TabIndex = 214;
            this.btnAllocateFormulaGroup.Text = "&Add Restricted Staff";
            this.toolTipConditions.SetToolTip(this.btnAllocateFormulaGroup, "Save record");
            this.btnAllocateFormulaGroup.UseVisualStyleBackColor = false;
            this.btnAllocateFormulaGroup.Click += new System.EventHandler(this.btnAllocateFormulaGroup_Click);
            // 
            // btnRemoveFromList
            // 
            this.btnRemoveFromList.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRemoveFromList.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnRemoveFromList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFromList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveFromList.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnRemoveFromList.Location = new System.Drawing.Point(544, 104);
            this.btnRemoveFromList.Name = "btnRemoveFromList";
            this.btnRemoveFromList.Size = new System.Drawing.Size(180, 24);
            this.btnRemoveFromList.TabIndex = 216;
            this.btnRemoveFromList.Text = "&Remove item from the list";
            this.toolTipConditions.SetToolTip(this.btnRemoveFromList, "Remove the item from the list");
            this.btnRemoveFromList.UseVisualStyleBackColor = false;
            this.btnRemoveFromList.Click += new System.EventHandler(this.btnRemoveFromList_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnEdit.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnEdit.Location = new System.Drawing.Point(438, 104);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(96, 24);
            this.btnEdit.TabIndex = 216;
            this.btnEdit.Text = "&Edit Formula";
            this.toolTipConditions.SetToolTip(this.btnEdit, "Save record");
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lstFormula
            // 
            this.lstFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstFormula.Location = new System.Drawing.Point(2, 0);
            this.lstFormula.Name = "lstFormula";
            this.lstFormula.Size = new System.Drawing.Size(722, 93);
            this.lstFormula.TabIndex = 3;
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(160, 104);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(144, 21);
            this.cboCategory.TabIndex = 0;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.Red;
            this.lblCategory.Location = new System.Drawing.Point(0, 104);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(158, 14);
            this.lblCategory.TabIndex = 23;
            this.lblCategory.Text = "This formula is restricted for";
            // 
            // pnlIncreaseDecrease
            // 
            this.pnlIncreaseDecrease.Controls.Add(this.btnMoveDown);
            this.pnlIncreaseDecrease.Controls.Add(this.btnMoveUp);
            this.pnlIncreaseDecrease.Location = new System.Drawing.Point(726, 0);
            this.pnlIncreaseDecrease.Name = "pnlIncreaseDecrease";
            this.pnlIncreaseDecrease.Size = new System.Drawing.Size(18, 92);
            this.pnlIncreaseDecrease.TabIndex = 213;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackColor = System.Drawing.Color.Beige;
            this.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveDown.ImageIndex = 0;
            this.btnMoveDown.ImageList = this.imageList1;
            this.btnMoveDown.Location = new System.Drawing.Point(1, 0);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(16, 47);
            this.btnMoveDown.TabIndex = 0;
            this.btnMoveDown.UseVisualStyleBackColor = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackColor = System.Drawing.Color.Beige;
            this.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.ImageIndex = 1;
            this.btnMoveUp.ImageList = this.imageList1;
            this.btnMoveUp.Location = new System.Drawing.Point(1, 48);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(16, 43);
            this.btnMoveUp.TabIndex = 1;
            this.btnMoveUp.UseVisualStyleBackColor = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.ImageIndex = 2;
            this.label1.ImageList = this.imageList1;
            this.label1.Location = new System.Drawing.Point(360, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 215;
            // 
            // frmBuildIFCondition
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(746, 370);
            this.Controls.Add(this.btnRemoveFromList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAllocateFormulaGroup);
            this.Controls.Add(this.pnlIncreaseDecrease);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.lstFormula);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpIfCondition);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuildIFCondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Build Formula";
            this.Load += new System.EventHandler(this.frmBuildIFCondition_Load);
            this.Closed += new System.EventHandler(this.frmBuildIFCondition_Closed);
            this.Click += new System.EventHandler(this.frmBuildIFCondition_Click);
            this.groupBox1.ResumeLayout(false);
            this.grpIfCondition.ResumeLayout(false);
            this.grpIfCondition.PerformLayout();
            this.pnlIncreaseDecrease.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void frmBuildIFCondition_Load(object sender, System.EventArgs e)
		{

            objCommon.BindCombo(cboCategory, FetchRecords(SQLCommand.Payroll.PayrollCategoryFetch), "FORMULA_DESC", "FORMULAGROUPID");
            //clsGeneral.(cboCategory,"SELECT 0 AS FORMULAGROUPID, '<All>' AS FORMULA_DESC FROM DUAL UNION ALL SELECT FORMULAGROUPID,FORMULA_DESC FROM PRFORMULAGROUP ORDER BY FORMULA_DESC","FORMULA_DESC","FORMULAGROUPID");
			cboCategory.SelectedIndex = 0;
			DisableControls();

			try
			{
				/*if( strCheck == "Add" )
				{
						AddFormula();					
				}*/

                //clsGeneral.fillList(lbComponent,"SELECT COMPONENTID AS \"COMPONENTID\",COMPONENT AS \"COMPONENT\" FROM PRCOMPONENT ORDER BY COMPONENT","COMPONENT","COMPONENTID");
                //To store the ComponentID and Component.
                objCommon.BindDataList(lbComponent, FetchRecords(SQLCommand.Payroll.PayrollComponentFetch), "COMPONENT", "COMPONENTID");
                DataTable dt = objComp.getPayrollComponent();   

				if ( dt.Rows.Count > 0 )
				{
					foreach ( DataRow dr in dt.Rows )
					{
						if ( !htFormula.Contains(dr[1].ToString()) )
							htFormula.Add(dr[1].ToString(),dr[0].ToString());
						htID.Add(dr[0].ToString(),dr[1].ToString());
					}
				}
				else
				{
					MessageBox.Show("There is no component available. Add Default Components","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					this.Close();
					return;
				}

				FillFormula();
				rdoFormula_CheckedChanged(sender, e);

				/*if ( strCheck == "Edit" )
				{
					EditFormula();
				}*/
			}
			catch
			{

			}
		}
        private object FetchRecords(object sqlQuery)
        {
            ResultArgs resultArgs = null;
            object dtCategory = null;
            using (DataManager dataManager = new DataManager(sqlQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtCategory = resultArgs.DataSource.Table;
            }
            return dtCategory;
        }

		private void SelectIFCondition(int ifOption)
		{
			switch(ifOption)
				{
					case 1:
						rdoIfThen.Checked=true;
						break;
					case 2:
						rdoIfAndThen.Checked=true;
						break;
					case 3:
						rdoIfElseThen.Checked=true;
						lblComponentName1.Text = Parent.txtComponent.Text+" =";
						break;
					case 4:
						rdoIfAndElseThen.Checked=true;
						lblComponentName1.Text = Parent.txtComponent.Text+" =";
						break;
					case 0:
						rdoFormula.Checked=true;
						break;
				}
		}

		private void rdoIfThen_CheckedChanged(object sender, System.EventArgs e)
		{
			//Parent.IFCon =  1;
			strOption    = "IFTHEN";
			rdoIfThen_Click(sender, e);
		}

		private void rdoIfAndThen_CheckedChanged(object sender, System.EventArgs e)
		{
			//Parent.IFCon = 2;
			strOption    = "IFANDTHEN";
			rdoIfAndThen_Click(sender, e);
		}
		private void rdoIfElseThen_CheckedChanged(object sender, System.EventArgs e)
		{
			//Parent.IFCon           = 3;
			strOption              = "IFELSETHEN";
			lblComponentName1.Text = Parent.txtComponent.Text+" =";
			rdoIfElseThen_Click(sender, e);
		}
		private void rdoIfAndElseThen_CheckedChanged(object sender, System.EventArgs e)
		{
			//Parent.IFCon             = 4;
			strOption                = "IFANDELSETHEN";
			lblComponentName1.Text   = Parent.txtComponent.Text+" =";
			IfAndElseThen_Click(sender, e);
		}

		private void rdoFormula_CheckedChanged(object sender, System.EventArgs e)
		{
			//Parent.IFCon = 0;
			strOption    = "FORMULA";
			rdoFormula_Click(sender, e);
		}

		private void rdoIfThen_Click(object sender, System.EventArgs e)
		{
			grpIfCondition.Text         = rdoIfThen.Text;
			lblIf.Visible               = true;
			txtComponent.Visible        = true;
			txtOperator1.Visible        = true;
			txtValue1.Visible           = true;
			lblComponentName.Visible    = true;
			txtLogicalOperator.Visible  = false;
			lblThen.Visible             = true;
			lblEndIf.Visible            = true;
			txtComponent1.Visible       = false;
			txtOperator2.Visible        = false;
			txtValue2.Visible           = false;
			lblComponentName1.Visible   = false;
			txtFormula2.Visible         = false;
			btnBuildFormula2.Visible    = false;
			lblElse.Visible             = false;
			lblThen2.Visible            = false;
			lblDescription.Text         = "The formula which you Construct Between the Statement"+
										  " IF and END IF that is Evaluated based on the selected "+
										  " Component which is logically matches with the given value for "+
										  " one set of Condition.";

		}

		private void rdoIfAndThen_Click(object sender, System.EventArgs e)
		{
			grpIfCondition.Text         = rdoIfAndThen.Text;
			txtComponent.Visible        = true;
			txtOperator1.Visible        = true;
			txtValue1.Visible           = true;
			txtLogicalOperator.Visible  = true;
			lblThen2.Visible            = true;
			txtComponent1.Visible       = true;
			txtOperator2.Visible        = true;
			txtValue2.Visible           = true;
			txtFormula1.Visible         = true;
			lblComponentName.Visible    = true;
			lblEndIf.Visible            = true;
			lblComponentName1.Visible   = false;
			txtFormula2.Visible         = false;
			btnBuildFormula2.Visible    = false;
			lblElse.Visible             = false;
			lblThen.Visible             = false;
			lblDescription.Text         = "The formula which you Construct Between the Statement "+
										  " IF and END IF that is Evaluated based on the selected "+
										  " Component which is logically matches with the given value for"+
										  " one set of Condition.";
		}

		private void rdoIfElseThen_Click(object sender, System.EventArgs e)
		{
			grpIfCondition.Text       =  rdoIfElseThen.Text;
			lblIf.Visible			  =  true;
			txtComponent.Visible      =  true;
			txtOperator1.Visible      =  true;
			txtValue1.Visible         =  true;
			lblComponentName.Visible  =  true;
			txtLogicalOperator.Visible=  false;
			lblThen.Visible           =  true;
			lblEndIf.Visible          =  true;
			lblElse.Visible           =  true;
			lblComponentName1.Visible =  true;
			txtComponent1.Visible     =  false;
			txtOperator2.Visible      =  false;
			txtValue2.Visible         =  false;
			txtFormula1.Visible       =  true;
			txtFormula2.Visible       =  true;
			btnBuildFormula1.Visible  =  true;
			btnBuildFormula2.Visible  =  true;

			lblThen2.Visible          =  false;
			lblDescription.Text       =  "The formula which you Construct Between the Statement "+
									     " IF and END IF that is Evaluated based on the selected "+
									     " Component which is logically matches with the given value."+
									     " Otherwise the formula lies between the statement \'ELSE\' and \'END IF\' set of Condition.";

		}

		private void IfAndElseThen_Click(object sender, System.EventArgs e)
		{
			grpIfCondition.Text=rdoIfAndElseThen.Text;
			lblIf.Visible			=  true;
			txtComponent.Visible    =  true;
			txtOperator1.Visible    =  true;
			txtValue1.Visible       =  true;
			lblComponentName.Visible=  true;
			txtLogicalOperator.Visible=true;
			lblThen.Visible          = true;
			lblEndIf.Visible        =  true;
			lblElse.Visible         =  true;

			lblComponentName1.Visible= true;
			txtComponent1.Visible   = true;
			txtOperator2.Visible    = true;
			txtValue2.Visible       = true;
			txtFormula1.Visible     = true;
			txtFormula2.Visible     = true;
			btnBuildFormula1.Visible= true;
			btnBuildFormula2.Visible= true;

			lblThen2.Visible        = true;
			lblThen.Visible         = false;
			lblDescription.Text     = "The formula which you Construct Between the Statement "+
				                      " IF and END IF that is Evaluated based on the selected "+
				                      " Component which is logically matches with the given value."+
				                      " Otherwise the formula lies between the statement \'ELSE\' and \'END IF\' set of Condition.";

		}

		private void rdoFormula_Click(object sender, System.EventArgs e)
		{
			grpIfCondition.Text=rdoFormula.Text;
			lblIf.Visible			=  false;
			txtComponent.Visible    =  false;
			txtOperator1.Visible    =  false;
			txtValue1.Visible       =  false;
			lblComponentName.Visible=  true;
			txtLogicalOperator.Visible=false;
			lblThen.Visible          = false;
			lblEndIf.Visible        =  false;
			lblElse.Visible         =  false;

			lblComponentName1.Visible= false;
			txtComponent1.Visible   = false;
			txtOperator2.Visible    = false;
			txtValue2.Visible       = false;
			txtFormula1.Visible     = true;
			txtFormula2.Visible     = false;
			btnBuildFormula1.Visible= true;
			btnBuildFormula2.Visible= false;

			lblThen2.Visible        = false;
			lblThen.Visible			= false;
			lblDescription.Text     ="The constructed formula is evaluated without any condition.";
		
		}

		private void btnBuildFormula1_Click(object sender, System.EventArgs e)
		{
			Comp=1;
            frmConstructFormula objConstruct = new frmConstructFormula(this, 1, Parent.ComponentId);
            objConstruct.ShowDialog();
		
		}

		private void btnBuildFormula2_Click(object sender, System.EventArgs e)
		{
			Comp=2;
            frmConstructFormula objConstruct = new frmConstructFormula(this, 2);
            objConstruct.ShowDialog();
		}
		private void DisableControls()
		{
			lblIf.Visible			    =  false;
			txtComponent.Visible        =  false;
			txtOperator1.Visible        =  false;
			txtValue1.Visible           =  false;
			txtLogicalOperator.Visible  =  false;
			lblComponentName.Visible    =  true;
			txtFormula1.Visible         =  true;
			btnBuildFormula1.Visible    =  true;
			txtComponent1.Visible       =  false;
			txtOperator2.Visible        =  false;
			txtValue2.Visible           =  false;
			lblThen.Visible             =  false;
			lblElse.Visible             =  false;
			lblComponentName1.Visible   =  false;
			btnBuildFormula2.Visible    =  false;
			txtFormula2.Visible         =  false;
			lblEndIf.Visible            =  false;
			lbComponent.Visible         =  false;
			lstbOperator1.Visible       =  false;
			lstbOperator2.Visible       =  false;
			lbLogicalOperator.Visible   =  false;

			txtComponent.BackColor       = Color.White;
			txtOperator1.BackColor       = Color.White;
			txtLogicalOperator.BackColor = Color.White;
			txtFormula1.BackColor        = Color.White;
			txtComponent1.BackColor      = Color.White;
			txtOperator2.BackColor       = Color.White;
			txtFormula2.BackColor        = Color.White;

		}

		private void txtComponent1_DoubleClick(object sender, System.EventArgs e)
		{
			ActiveControl             = "Comp1";
			this.lbComponent.Location = new System.Drawing.Point(48, 60);//48, 64
			lbComponent.Visible       = true;
		}
		private void lbComponent_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( ActiveControl == "Comp" )
			{
				txtComponent.Text   = lbComponent.Text;
				lbComponent.Visible = false;
			}
			if ( ActiveControl == "Comp1" )
			{
				txtComponent1.Text  = lbComponent.Text;
				lbComponent.Visible = false;
			}
		}
		private void txtComponent_DoubleClick(object sender, System.EventArgs e)
		{
			ActiveControl       = "Comp";
			lbComponent.Visible = true;
		}
		private void txtOperator1_DoubleClick(object sender, System.EventArgs e)
		{
			lstbOperator1.Visible = true;
		}
		private void txtLogicalOperator_DoubleClick(object sender, System.EventArgs e)
		{
			lbLogicalOperator.Visible = true;
		}
		private void txtOperator2_DoubleClick(object sender, System.EventArgs e)
		{
			lstbOperator2.Visible = true;
		}
		private void lstbOperator1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtOperator1.Text      = lstbOperator1.Text;
			lstbOperator1.Visible  = false;
		}
		private void lstbOperator2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtOperator2.Text     = lstbOperator2.Text;
			lstbOperator2.Visible = false;
		}
		private void lbLogicalOperator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtLogicalOperator.Text    = lbLogicalOperator.Text;
			lbLogicalOperator.Visible  = false;
		}

		private void checkIfThen()
		{
			if ( ( txtComponent.Text.Trim() == "{COMPONENT1}") ||( txtComponent.Text.Trim() == ""))
			{
				MessageBox.Show("Componenet field can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtComponent.Focus();
				return ;
			}
			if( ( txtOperator1.Text.Trim() == "{OPR1}") ||( txtOperator1.Text.Trim() == "" ) )
			{
				MessageBox.Show("Operator field can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtOperator1.Focus();
				return;
			}
			if ( ( txtValue1.Text.Trim() == "{VALUE1}") ||( txtValue1.Text.Trim() == "" ) )
			{
				MessageBox.Show("Value field can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtValue1.Focus();
				return;
			}
			if ( txtFormula1.Text.Trim() == "" )
			{
				MessageBox.Show("Formula Can not be Empty","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				btnBuildFormula1.Focus();
				return;
			}
			
		}
		private bool ValidateIfThen()
		{
			if ( ( txtComponent.Text.Trim() == "{COMPONENT1}" ) ||( txtComponent.Text.Trim() == "" ) )
			{
				txtComponent.Focus();
				return false;
			}
			if ( ( txtOperator1.Text.Trim() == "{OPR1}" ) ||( txtOperator1.Text.Trim() == "" ) )
			{
				txtOperator1.Focus();
				return false;
			}
			if ( ( txtValue1.Text.Trim() == "{VALUE1}" ) ||( txtValue1.Text.Trim() == "" ) )
			{
				txtValue1.Focus();
				return false;
			}
			if ( txtFormula1.Text.Trim() == "" )
			{
				btnBuildFormula1.Focus();
				return false;
			}
			return true;
			
		}
		private void checkIfAndThen()
		{
			if ( ( txtComponent1.Text.Trim() == "{COMPONENT2}" ) || ( txtComponent1.Text.Trim() == "" ) )
			{
				MessageBox.Show("Componenet2 field can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtComponent1.Focus();
				return ;
			}
			if ( ( txtOperator2.Text.Trim() == "{OPR2}" ) ||( txtOperator2.Text.Trim() == "" ) )
			{
				MessageBox.Show("Operator field can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtOperator2.Focus();
				return ;
			}
			if ( ( txtValue2.Text.Trim() == "{VALUE2}" ) ||( txtValue2.Text.Trim() == "" ) )
			{
				MessageBox.Show("Value field can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtValue2.Focus();
				return ;
			}
		}
		private bool ValidateIfAndThen()
		{
			if ( ( txtComponent1.Text.Trim() == "{COMPONENT2}" ) ||( txtComponent1.Text.Trim() == "" ) )
			{
				txtComponent1.Focus();
				return false;
			}
			if ( ( txtOperator2.Text.Trim() == "{OPR2}" ) ||( txtOperator2.Text.Trim() == "" ) )
			{
				txtOperator2.Focus();
				return false;
			}
			if ( ( txtValue2.Text.Trim() == "{VALUE2}") ||( txtValue2.Text.Trim() == "" ) )
			{
				txtValue2.Focus();
				return false;
			}
			return true;		
		}

		private bool validateFields()
		{
			
			if ( rdoFormula.Checked == true )
			{
				if ( txtFormula1.Text.Trim() == "" )
					return false;
				return true;
			}
			else if ( rdoIfThen.Checked == true )
			{
				if ( ValidateIfThen() )
					return true;
				return false;
			}
			else if( rdoIfAndThen.Checked == true )
			{
				if ( ValidateIfThen() & ValidateIfAndThen() )
					return true;
				return false;
			}
			else if( rdoIfElseThen.Checked == true )
			{
				if ( ValidateIfThen() & (txtFormula2.Text != "" ) )
					return true;
				return false;
			}
			else
			{
				if ( ValidateIfThen() & ValidateIfAndThen() & txtFormula2.Text != "" )
					return true;
				return false;
			}
		}

		private void FillFormula()
		{
			string formulaGroup = "";
			string[] aformulaGroup;
			string formulaGroupId = "";
			string[] aformulaGroupId;
			string formula = "";

			formulaGroup = Parent.txtEquation.Text;
			formulaGroupId = Parent.CValue;

			aformulaGroup = formulaGroup.Split('$');
			aformulaGroupId = formulaGroupId.Split('$');
			
			if (formulaGroup != "")
			{
				for(int i=0;i<aformulaGroup.Length;i++)
				{
					formula = aformulaGroup[i] + "$" + aformulaGroupId[i];
					lstFormula.Items.Add(formula);
				}
			}
		}

		private bool ConstructFormula(ref string strFormulaConditions, ref string strFinalFormulaId)
		{
			bool validFormula = false;
			strFormulaConditions	= "";
			strFinalFormulaId = "";

			if ( rdoIfThen.Checked == true )
			{
				checkIfThen();
			}
			else if ( rdoIfAndThen.Checked == true )
			{
				checkIfThen();
				checkIfAndThen();
			}
			else if ( rdoIfElseThen.Checked == true )
			{
				checkIfThen();
				if ( txtFormula2.Text == "" )
				{
					MessageBox.Show("Formula2 Can not be Empty","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					btnBuildFormula2.Focus();
					return false;
				}
			}
			else if ( rdoIfAndElseThen.Checked == true )
			{
				checkIfThen();
				checkIfAndThen();
				if ( txtFormula2.Text == "" )
				{
					MessageBox.Show("Formula2 Can not be Empty","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					btnBuildFormula2.Focus();
					return false;
				}
			}
			else
			{
				if ( txtFormula1.Text == "" )
				{
					MessageBox.Show("Formula Can not be Empty","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
					btnBuildFormula1.Focus();
					return false;
				}
			}
			if ( txtFormula1.Text != "" )
			{
				ComponentValue1 = BuildFormulaId(txtFormula1.Text.Trim());
				ComponentValue1 = ComponentValue1.Replace(" ","");
				ComponentValue1 = ComponentValue1.Replace(Convert.ToChar(160) + "<","<");
				ComponentValue1 = ComponentValue1.Replace(">" + Convert.ToChar(160),">");
			}
			if ( txtFormula2.Text != "" )
			{
				ComponentValue2 = BuildFormulaId(txtFormula2.Text.Trim());
				ComponentValue2 = ComponentValue2.Replace(" ","");
				ComponentValue2 = ComponentValue2.Replace(Convert.ToChar(160) + "<","<");
				ComponentValue2 = ComponentValue2.Replace(">" + Convert.ToChar(160),">");
			}
			//  Relational Operators (nOpr1) Id : 1 =, 2 >, 3 <, 4 >=, 5 <=, 6 <>
			//change the operator type
			if ( txtOperator1.Text != "" )
			{
				switch(txtOperator1.Text.Trim())
				{
					case "=":
						strOperator = "1";
						break;
					case ">":
						strOperator = "2";
						break;
					case "<":
						strOperator = "3";
						break;
					case ">=":
						strOperator = "4";
						break;
					case "<=":
						strOperator = "5";
						break;
					case "<>":
						strOperator = "6";
						break;
				}
			}
			if ( txtOperator2.Text != "" )
			{
				switch(txtOperator2.Text.Trim())
				{
					case "=":
						strOperator1 = "1";
						break;
					case ">":
						strOperator1 = "2";
						break;
					case "<":
						strOperator1 = "3";
						break;
					case ">=":
						strOperator1 = "4";
						break;
					case "<=":
						strOperator1 = "5";
						break;
					case "<>":
						strOperator1 = "6";
						break;
				}
			}

			if ( txtLogicalOperator.Text == "AND" )
			{
				strLogical = "1";
			}
			if ( txtLogicalOperator.Text == "OR" )
			{
				strLogical = "2";
			}
			if ( strOption == "IFTHEN" )
			{
				strFormulaConditions = "{IF}"+ Convert.ToChar(160) +"{"+ txtComponent.Text + "}" +Convert.ToChar(160) + "{"+  txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160) +"{"+  txtFormula1.Text + "}";
				strFinalFormulaId  = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+  strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+  ComponentValue1 + "}~1";

				//Parent.txtEquation.Text = "{IF}"+ Convert.ToChar(160) +"{"+ txtComponent.Text + "}" +Convert.ToChar(160) + "{"+  txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160) +"{"+  txtFormula1.Text + "}";
				//Parent.CValue           = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+  strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+  ComponentValue1 + "}";

			}
			else if ( strOption == "IFANDTHEN" )
			{
				strFormulaConditions        = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" +Convert.ToChar(160) + "{"+txtValue1.Text + "}"+Convert.ToChar(160) + "{"+txtLogicalOperator.Text + "}" +Convert.ToChar(160)+"{"+txtComponent1.Text + "}" +Convert.ToChar(160)+ "{"+txtOperator2.Text + "}" + Convert.ToChar(160) + "{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160)+"{" + txtFormula1.Text + "}";
				strFinalFormulaId			= "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical + "}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+ strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}~2";

				//Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" +Convert.ToChar(160) + "{"+txtValue1.Text + "}"+Convert.ToChar(160) + "{"+txtLogicalOperator.Text + "}" +Convert.ToChar(160)+"{"+txtComponent1.Text + "}" +Convert.ToChar(160)+ "{"+txtOperator2.Text + "}" + Convert.ToChar(160) + "{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+ Convert.ToChar(160)+"{" + txtFormula1.Text + "}";
				//Parent.txtEquation.ReadOnly = true;
				//Parent.rdoEquation.Checked  = true;
				//Parent.txtMaxSlab.Focus();
				//Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical + "}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+ strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}";
			}
			else if ( strOption == "IFELSETHEN" )
			{
				strFormulaConditions        = "{IF}"+Convert.ToChar(160)+"{"+ txtComponent.Text + "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}" + Convert.ToChar(160)+"{ELSE}"+Convert.ToChar(160) +"{"+ txtFormula2.Text + "}";
				strFinalFormulaId			= "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{" + ComponentValue1 + "}" + Convert.ToChar(160) +"{"+ ComponentValue2 + "}~3";

				//Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+ txtComponent.Text + "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}" + Convert.ToChar(160)+"{ELSE}"+Convert.ToChar(160) +"{"+ txtFormula2.Text + "}";
				//Parent.txtEquation.ReadOnly = true;
				//Parent.rdoEquation.Checked  = true;
				//Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{" + txtValue1.Text + "}" + Convert.ToChar(160) +"{" + ComponentValue1 + "}" + Convert.ToChar(160) +"{"+ ComponentValue2 + "}";
			}
			else if ( strOption == "IFANDELSETHEN" )
			{
				strFormulaConditions        = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+txtLogicalOperator.Text + "}" + Convert.ToChar(160) +"{"+txtComponent1.Text + "}" + Convert.ToChar(160) +"{"+txtOperator2.Text + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}"+ Convert.ToChar(160) +"{ELSE}"+Convert.ToChar(160)+"{" + txtFormula2.Text + "}";
				strFinalFormulaId 			= "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical +"}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160)+"{"+strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}"+ Convert.ToChar(160) +"{" + ComponentValue2 + "}~4";

				//Parent.txtEquation.Text     = "{IF}"+Convert.ToChar(160)+"{"+txtComponent.Text+ "}" + Convert.ToChar(160) +"{"+txtOperator1.Text + "}" + Convert.ToChar(160) +"{"+txtValue1.Text + "}" + Convert.ToChar(160) +"{"+txtLogicalOperator.Text + "}" + Convert.ToChar(160) +"{"+txtComponent1.Text + "}" + Convert.ToChar(160) +"{"+txtOperator2.Text + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{THEN}"+Convert.ToChar(160)+"{" + txtFormula1.Text + "}"+ Convert.ToChar(160) +"{ELSE}"+Convert.ToChar(160)+"{" + txtFormula2.Text + "}";
				//Parent.txtEquation.ReadOnly = true;
				//Parent.rdoEquation.Checked  = true;
				//Parent.txtMaxSlab.Focus();
				//Parent.CValue               = "{"+ htFormula[txtComponent.Text.ToString()] + "}" + Convert.ToChar(160) +"{"+strOperator + "}" + Convert.ToChar(160) +"{"+ txtValue1.Text + "}" + Convert.ToChar(160) +"{"+ strLogical +"}" + Convert.ToChar(160) +"{"+ htFormula[txtComponent1.Text.ToString()] + "}" + Convert.ToChar(160)+"{"+strOperator1 + "}" + Convert.ToChar(160) +"{"+txtValue2.Text + "}"+ Convert.ToChar(160) +"{" + ComponentValue1 + "}"+ Convert.ToChar(160) +"{" + ComponentValue2 + "}";
			}
			else
			{
				//BuildFormulaId(txtFormula1.Text);
				strFormulaConditions         = txtFormula1.Text;
				//strFinalFormulaId			 = txtFormula1.Text + "~0"; // + "!0";
				strFinalFormulaId            = BuildFormulaId(txtFormula1.Text)+ "~0";

				//Parent.txtEquation.Text      = txtFormula1.Text;
				//Parent.txtEquation.ReadOnly  = true;
				//Parent.CValue                = ComponentValue1;
			}

			/*if(lstFormula.Items.Count > 0)
			{
				for(int i =0; i < lstFormula.Items.Count; i++)
				{
					strFinalString += lstFormula.Items[i].ToString()+ " ";
				}
				string[] sFormula ;
				string[] strValue;
				sFormula = strFinalString.Split('$');
				for ( int i=0; i< sFormula.Length; i++)
				{
					strValue = sFormula[i].Split('~');
					if(strValue.Length == 2)
					{
						Parent.txtEquation.Text += strValue[0].ToString()+ " ";
						Parent.CValue          += strValue[1].ToString() + " ";
					}
				}
			}*/

			//this.Close();
			if(validateFields())
			{	
				validFormula = true;
				MessageBox.Show("Formula is added","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Invalid formula","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			
			return validFormula;
		}

		private void txtValue1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			e.Handled =! objR.IsMatch(e.KeyChar.ToString());
		}
		private void txtValue2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled =! objR.IsMatch(e.KeyChar.ToString());
		}
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		private void frmBuildIFCondition_Click(object sender, System.EventArgs e)
		{
			lbComponent.Visible       = false;
			lstbOperator1.Visible     = false;
			lstbOperator2.Visible     = false;
			lbLogicalOperator.Visible = false;
		}

		private void EditFormula(string formula, string formulaId, int ifCondition)
		{
			int nIdx;
			string[] aFormulaId = new string[50];
			try
			{
				if(ifCondition == 0)
				{
					txtFormula1.Visible = true;
					txtFormula1.Text    = formula;
				}
				else
				{
					if(formulaId != "")
					{
						aFormulaId        = formulaId.Split(Convert.ToChar(160));
						nIdx              = Int32.Parse(RemoveBrace(aFormulaId[0].ToString(),false));
						txtComponent.Text = htID[nIdx.ToString()].ToString();
						nIdx              = Int32.Parse(RemoveBrace(aFormulaId[1].ToString(),false));
						
						if (nIdx>0)
							txtOperator1.Text = lstbOperator1.Items[nIdx-1].ToString();
						txtValue1.Text = RemoveBrace(aFormulaId[2].ToString(),false).ToString();

						if((ifCondition == 1)||(ifCondition == 3 ))
						{
							if( ifCondition == 1)
							{
								txtFormula1.Tag = RemoveBrace(aFormulaId[3].ToString(),false);
								if(txtFormula1.Tag.ToString()!= "")
								{
									txtFormula1.Text = BuildFormulaId(txtFormula1.Tag.ToString());
									txtFormula1.Text = txtFormula1.Text.Substring(0,txtFormula1.Text.IndexOf('~'));
								}
							}
							if(ifCondition == 3)
							{
								txtFormula1.Tag = RemoveBrace(aFormulaId[3].ToString(),false);
								if(txtFormula1.Tag.ToString()!= "")
								{
									txtFormula1.Text = BuildFormulaId(txtFormula1.Tag.ToString());
									//txtFormula1.Text = txtFormula1.Text.Substring(0,txtFormula1.Text.IndexOf('~'));
								}
								txtFormula2.Tag = RemoveBrace(aFormulaId[4].ToString(),false);
								if(txtFormula2.Tag.ToString()!= "")
								{
									txtFormula2.Text = BuildFormulaId(txtFormula2.Tag.ToString());
									txtFormula2.Text = txtFormula2.Text.Substring(0,txtFormula2.Text.IndexOf('~'));
								}
							}
						}
						else if(ifCondition == 2 || ifCondition == 4)
						{
							nIdx = Int32.Parse(RemoveBrace(aFormulaId[3].ToString(),false));
							if( nIdx > 0 )
							{
								txtLogicalOperator.Text = lbLogicalOperator.Items[nIdx-1].ToString();
							}
							nIdx = Int32.Parse(RemoveBrace(aFormulaId[4].ToString(),false));
							if (nIdx>0) txtComponent1.Text = htID[nIdx.ToString()].ToString();
							nIdx = Int32.Parse(RemoveBrace(aFormulaId[5].ToString(),false));
							if( nIdx > 0 )
							{
								txtOperator2.Text = lstbOperator1.Items[nIdx-1].ToString();
							}
							txtValue2.Text = RemoveBrace(aFormulaId[6].ToString(),false);
							txtFormula1.Tag = RemoveBrace(aFormulaId[7].ToString(),false);
							if(txtFormula1.Tag.ToString() != "" )
							{
								txtFormula1.Text = BuildFormulaId(txtFormula1.Tag.ToString());
								//txtFormula1.Text = txtFormula1.Text.Substring(0,txtFormula1.Text.IndexOf('~'));
							}
							if (ifCondition == 4 ) 
							{
								txtFormula2.Tag = RemoveBrace(aFormulaId[8].ToString(),false);
								if(txtFormula2.Tag.ToString()!= "" )
								{
									txtFormula2.Text = BuildFormulaId(txtFormula2.Tag.ToString());
									txtFormula2.Text = txtFormula2.Text.Substring(0,txtFormula2.Text.IndexOf('~'));
								}
	
							}
						
						}
					
					}
				}
			}
			catch
			{
			}
		}

		private string RemoveBrace(string sText, bool bSeparator)
		{
			string sVal="";
			sVal = sText;
			sVal = sVal.Replace("{","");
			sVal = sVal.Replace("}","");
			if(bSeparator)
			{
				sVal = sVal.Replace("<","");
				sVal = sVal.Replace(">","");
			}
			return sVal;
		}
		private bool getComponentValue(ref string compId)
		{
			string strCom = RemoveBrace(compId,true);
			try
			{
				int i = Convert.ToInt32(strCom.ToString());
				if( i > 0 )
					compId = Convert.ToChar(160).ToString() + "<" + htID[i.ToString()].ToString()+ ">" + Convert.ToChar(160).ToString();
			}
			catch
			{
				compId = Convert.ToChar(160).ToString() + "<" + htFormula[strCom.ToString()].ToString()+ ">" + Convert.ToChar(160).ToString();
				return true;
			}
			return true;
		}
/*
		private int GetIFArgumentIndex(ListBox lstBox, string sText)
		{
			int index;
			for ( index =1; index < lstBox.Items.Count; index++)
			{
				if ( lstBox.Items[index].ToString() == sText )
				{
					lstBox.SelectedIndex = index;
					return int.Parse(lstBox.SelectedValue.ToString());
				}
			}
			return 0;
		}
		private string GetFormula(ListBox lstBox, int iId)
		{
			int index;
			for ( index =1; index < lstBox.Items.Count; index++)
			{
					lstBox.SelectedIndex = index;
				if (lstBox.SelectedValue.ToString()== iId.ToString())
				{
					return lstBox.ValueMember.ToString();
				}
			}
			return "";
		} */

		private string BuildFormulaId(string strFormula)
		{
			int i,nPos,nPos1,nStartPos;
			string sCompId  = "";
			string sCompId1 = "";
			string sComp    = "";
			sCompId         = strFormula;
			nStartPos       = 0;
			try
			{
				for( i = 0; i < strFormula.Length; i++ )
				{
					nPos=strFormula.IndexOf("<",nStartPos);	
					
					if(nPos >= 0)
					{
						nStartPos = nPos + 1;
						nPos1 = strFormula.IndexOf(">",nStartPos);
						nStartPos = nPos1 + 1;
						if(nPos1>0)
						{
							sComp=strFormula.Substring(nPos,nPos1-(nPos-1));
							sCompId1 = sComp;
							getComponentValue(ref sComp);
							sCompId = sCompId.Replace(sCompId1,sComp.Trim());	
							nStartPos = nPos + sCompId1.Length;
						}
						else
							break;
					}
					else
						break;
				}
			
			}
			catch(Exception ex)
			{
				string str = ex.Message ;
				return sCompId;
			}
			return sCompId;
		}

		private void lbComponent_MouseLeave(object sender, System.EventArgs e)
		{
			lbComponent.Visible = false;
		}

		private void lstbOperator1_MouseLeave(object sender, System.EventArgs e)
		{
			lstbOperator1.Visible = false;
		}

		private void lstbOperator2_MouseLeave(object sender, System.EventArgs e)
		{
			lstbOperator2.Visible = false;
		}

		private void lbLogicalOperator_MouseLeave(object sender, System.EventArgs e)
		{
			lbLogicalOperator.Visible = false;
		}

		private void frmBuildIFCondition_Closed(object sender, System.EventArgs e)
		{
			rdoActiveButton.Enabled = true;
		}

		private void btnAllocateFormulaGroup_Click(object sender, System.EventArgs e)
		{
            new frmFormulaGroup().ShowDialog();
            //clsGeneral.fillList(cboCategory, "SELECT 0 AS FORMULAGROUPID, '<All>' AS FORMULA_DESC FROM DUAL UNION ALL SELECT FORMULAGROUPID,FORMULA_DESC FROM PRFORMULAGROUP ORDER BY FORMULA_DESC", "FORMULA_DESC", "FORMULAGROUPID");
            objCommon.BindCombo(cboCategory, FetchRecords(SQLCommand.Payroll.PayrollCategoryFetch), "FORMULA_DESC", "FORMULAGROUPID");
		}

		/*private void lstFormula_DoubleClick(object sender, System.EventArgs e)
		{
			string[] strFormula = new string[100];
			string setValue     = "";
			if (lstFormula.Items.Count != 0)
			{
				strEquation = lstFormula.SelectedItem.ToString();
				if(strEquation != "")
				{
					strFormula = strEquation.Split(Convert.ToChar(160));
					if( (strFormula.Length == 1 ) & ( Parent.IFCon == 0 ) )
					{
						txtFormula1.Visible = true;
						txtFormula1.Text = Parent.txtEquation.Text;
					}
					setValue = RemoveBrace(strFormula[1].ToString(),false);
					txtComponent.Text = setValue;
					setValue          = RemoveBrace(strFormula[2].ToString(),false);
					if( setValue != "" )
						txtOperator1.Text = setValue;
					txtValue1.Text = RemoveBrace(strFormula[3].ToString(),false).ToString();
					if( ( Parent.IFCon == 1 ) || ( Parent.IFCon == 3 ) )
					{
						txtFormula1.Text = RemoveBrace(strFormula[5].ToString(),false).ToString();
					}
					if( ( Parent.IFCon == 3) & (strFormula.Length == 8))
					{
						rdoIfAndElseThen.Checked = true;
						txtFormula2.Text         = RemoveBrace(strFormula[7].ToString(),false).ToString();
					}

					else if( Parent.IFCon == 2 || Parent.IFCon == 4)
					{
						setValue = RemoveBrace(strFormula[4].ToString(),false);
						if( setValue != " " )
						{
							txtLogicalOperator.Text = setValue;
						}
						setValue = RemoveBrace(strFormula[5].ToString(),false);
						if( setValue != "" ) txtComponent1.Text = setValue;
						setValue = RemoveBrace(strFormula[6].ToString(),false);
						if( setValue != "" )
						{
							txtOperator2.Text = setValue;
						}
						txtValue2.Text = RemoveBrace(strFormula[7].ToString(),false);
						setValue = RemoveBrace(strFormula[9].ToString(),false);
						if( setValue != "" )
						{
							txtFormula1.Text = setValue;
						}
						if (( Parent.IFCon == 4) & ( strFormula.Length==12 ))
						{
							rdoIfAndElseThen.Checked = true;
							setValue = RemoveBrace(strFormula[11].ToString(),false);
							if(setValue != "")
							{
								txtFormula2.Text = setValue;
							}
						}
						
					}
				}
			}
		}*/

		//Modified by CS ::28-apr-2007

		private void btnSavetoList_Click(object sender, System.EventArgs e)
		{
			string strFormula	= "";
			string strFormulaId = "";
			string formulaStaffGroupId = "0";

			if (cboCategory.SelectedIndex >= 0)
			{
				formulaStaffGroupId = cboCategory.SelectedValue.ToString();
			}

			if (ConstructFormula(ref strFormula,  ref strFormulaId))
			{
				if (strFormula != "")
				{
					strFormula += "$" + strFormulaId + "~" + formulaStaffGroupId; 

					if (newFormula)
					{
						lstFormula.Items.Add(strFormula);
					}
					else
					{
						if (lstFormula.SelectedIndex >= 0 )
						{
							int idx = lstFormula.SelectedIndex;
							lstFormula.Items.RemoveAt(idx);
							lstFormula.Items.Insert(idx,strFormula);
						}
					}
				}
			}
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			string formulaGroup = "";
			string formula = "";
			string formulaId = "";
			string[] aformulaGroup;
			string[] aformulaId;
			
			int ifId = 0;
			string formulaGroupId = "";

			if (lstFormula.SelectedIndex >= 0)
			{
				newFormula = false;

				formulaGroup	= lstFormula.Items[lstFormula.SelectedIndex].ToString();
				aformulaGroup	= formulaGroup.Split('$');
				aformulaId		= aformulaGroup[1].Split('~');

				formula		= aformulaGroup[0];
				formulaId	= aformulaGroup[1];
				
				if (aformulaId.Length > 1)
				{
					ifId = Convert.ToInt32(aformulaId[1]);
					formulaGroupId = aformulaId[2];
				}

				if (formulaGroupId != "")
				{
					cboCategory.SelectedValue = formulaGroupId;
				}
				else
				{
					cboCategory.SelectedIndex = 0;
				}

				SelectIFCondition(ifId);
				EditFormula(formula, formulaId, ifId);
			}

			btnNew.Enabled = true;
		}

		private void btnRemoveFromList_Click(object sender, System.EventArgs e)
		{
			if (lstFormula.SelectedIndex >= 0)
			{
				lstFormula.Items.RemoveAt(lstFormula.SelectedIndex);
			}

			newFormula = true;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string formulaGroup = "";
			string formula = "";
			string formulaId = "";
			string formula1 = "";
			string formulaId1 = "";
			string[] aformulaGroup;
			string[] aformulaId;
			int formulaGroupId = 0;
			
			for(int i=0;i<lstFormula.Items.Count;i++)
			{
				formulaGroup = lstFormula.Items[i].ToString();
				aformulaGroup = formulaGroup.Split('$');
				
				aformulaId = aformulaGroup[1].Split('~');
				
				formulaGroupId = Convert.ToInt32(aformulaId[2]);
				
				//for restricted staff
				if ( formulaGroupId > 0 ) 
				{
					formula1 += ((formula1 != "") ? "$" : "") + aformulaGroup[0];
					formulaId1 += ((formulaId1 != "") ? "$" : "") + aformulaGroup[1];
				}
				else  //formula for all staff
				{
					formula += ((formula != "") ? "$" : "") + aformulaGroup[0];
					formulaId += ((formulaId != "") ? "$" : "") + aformulaGroup[1];
				}
			}
			
			if (formula1 != "" )
			{
				formula = formula1 + (formula=="" ? "" : ("$" + formula));
				formulaId = formulaId1 + (formulaId=="" ? "" : ("$" + formulaId));
			}
			
			Parent.txtEquation.Text = formula;
			Parent.CValue = formulaId;
			Parent.IFCon = 1;
			this.Close();
		}

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			newFormula = true;
		}

		private void btnMoveUp_Click(object sender, System.EventArgs e)
		{
			string itemtoUp = "";
			string itemtoDown = "";
			
			int selIndex = lstFormula.SelectedIndex;
			try
			{
				if (selIndex > 0)
				{
					itemtoUp = lstFormula.Items[selIndex].ToString();
					itemtoDown = lstFormula.Items[selIndex-1].ToString();
					
					lstFormula.Items.RemoveAt(selIndex-1);
					lstFormula.Items.RemoveAt(selIndex-1);

					lstFormula.Items.Insert(selIndex-1, itemtoUp);
					lstFormula.Items.Insert(selIndex, itemtoDown);

				}
			}
			catch{}
		}

		private void btnMoveDown_Click(object sender, System.EventArgs e)
		{
			string itemtoUp = "";
			string itemtoDown = "";
			
			int selIndex = lstFormula.SelectedIndex;
	
			try
			{
				if (selIndex < (lstFormula.Items.Count - 1))
				{
					itemtoUp = lstFormula.Items[selIndex+1].ToString();
					itemtoDown = lstFormula.Items[selIndex].ToString();
				
					lstFormula.Items.RemoveAt(selIndex);
					lstFormula.Items.RemoveAt(selIndex);

					lstFormula.Items.Insert(selIndex, itemtoUp);
					lstFormula.Items.Insert(selIndex+1, itemtoDown);
				}
			}
			catch{}
		}
	}
}
			
		
