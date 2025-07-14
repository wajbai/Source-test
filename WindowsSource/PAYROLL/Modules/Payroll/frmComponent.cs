/*****************************************************************************************
*					Interface       : frmComponent
*					Purpose         : To Create New Component for Payroll
*					Object Involved : clsPrCompBuild
					Date from       : 18-Feb-2007
*					Author          : GP
*					Modified by     : PE ON 11-09-2008 to validate circulare references
*****************************************************************************************/


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Bosco.Utility.Common;

using System.Text.RegularExpressions;
using PAYROLL.Modules.Payroll;
using Payroll.Model.UIModel;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;

namespace PAYROLL.Modules.Payroll
{
    public class frmComponent : PAYROLL.UserControl.frmHMSTemplate
	{
		//DataHandling dh	= new DataHandling();

		private System.Windows.Forms.GroupBox grpComponent;
		private System.Windows.Forms.Label lblComponent;
		public  System.Windows.Forms.TextBox txtComponent;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.GroupBox grpDefault;
		private System.Windows.Forms.RadioButton rdoFixedValue;
		private System.Windows.Forms.RadioButton rdoLinkValue;
		public System.Windows.Forms.RadioButton rdoEquation;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFixedValue;
		public System.Windows.Forms.TextBox txtEquation;
		private System.Windows.Forms.Button btnBuild;
		public System.Windows.Forms.TextBox txtMaxSlab;
		private System.Windows.Forms.GroupBox grpRoundedOption;
		private System.Windows.Forms.RadioButton rdoCeiling;
		private System.Windows.Forms.RadioButton rdoFloor;
		private System.Windows.Forms.RadioButton rdoPreviousOrNext;
		private System.Windows.Forms.RadioButton rdoNone;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel pnlGroup;
		private System.Windows.Forms.ComboBox cboLinkValue;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTipComponent;
		private clsPrComponent objPRComp=new clsPrComponent();
		public int iCondition;
		private System.Windows.Forms.CheckBox chkShowinBrowse;
		private DataTable dt=new DataTable();
        private CheckBox chkIsEditable;
        ResultArgs resultArgs = null;
		public frmComponent()
		{
			InitializeComponent();
		}
		public frmComponent(string strAdd)
		{
			InitializeComponent();
			strOperation  = strAdd;
		}
		public frmComponent(frmPayrollBrowse frm,string op)
		{
			InitializeComponent();
			Parent        = frm;
			strOperation  = op;
		}

		#region Variables
		private string sCompStr                = "";
		private string StrValue                = "";
		public long ComponentId               = 0;
		private clsPayrollComponent objPayroll = new clsPayrollComponent();
		private clsPayrollLoan objLoan         = new clsPayrollLoan();
		private clsPayrollStaff objStaff       = new clsPayrollStaff();
		private clsprCompBuild objCompBuild    = new clsprCompBuild();
		private string strOperation            = "";
		public int IFCon                       = 0;			//IF Conditions
		public string CValue                   = "";	   //Component Equation Id
		private Regex objR                     = new Regex("[0-9]|\b");
		private string strLinkValue            = "";
		private frmPayrollBrowse Parent;
		public string strConditionValue        = "";
		private string sRelatedComponents      = "";
		private string sCircularComponentName  = "";
		private DataView dvComponents = null;
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
            this.grpComponent = new System.Windows.Forms.GroupBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtComponent = new System.Windows.Forms.TextBox();
            this.lblComponent = new System.Windows.Forms.Label();
            this.grpDefault = new System.Windows.Forms.GroupBox();
            this.btnBuild = new System.Windows.Forms.Button();
            this.rdoFixedValue = new System.Windows.Forms.RadioButton();
            this.cboLinkValue = new System.Windows.Forms.ComboBox();
            this.txtMaxSlab = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEquation = new System.Windows.Forms.TextBox();
            this.rdoEquation = new System.Windows.Forms.RadioButton();
            this.rdoLinkValue = new System.Windows.Forms.RadioButton();
            this.txtFixedValue = new System.Windows.Forms.TextBox();
            this.grpRoundedOption = new System.Windows.Forms.GroupBox();
            this.rdoNone = new System.Windows.Forms.RadioButton();
            this.rdoPreviousOrNext = new System.Windows.Forms.RadioButton();
            this.rdoFloor = new System.Windows.Forms.RadioButton();
            this.rdoCeiling = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlGroup = new System.Windows.Forms.Panel();
            this.chkIsEditable = new System.Windows.Forms.CheckBox();
            this.chkShowinBrowse = new System.Windows.Forms.CheckBox();
            this.toolTipComponent = new System.Windows.Forms.ToolTip(this.components);
            this.grpComponent.SuspendLayout();
            this.grpDefault.SuspendLayout();
            this.grpRoundedOption.SuspendLayout();
            this.pnlGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpComponent
            // 
            this.grpComponent.BackColor = System.Drawing.Color.White;
            this.grpComponent.Controls.Add(this.cboType);
            this.grpComponent.Controls.Add(this.lblType);
            this.grpComponent.Controls.Add(this.txtDescription);
            this.grpComponent.Controls.Add(this.lblDescription);
            this.grpComponent.Controls.Add(this.txtComponent);
            this.grpComponent.Controls.Add(this.lblComponent);
            this.grpComponent.Location = new System.Drawing.Point(8, 8);
            this.grpComponent.Name = "grpComponent";
            this.grpComponent.Size = new System.Drawing.Size(368, 104);
            this.grpComponent.TabIndex = 0;
            this.grpComponent.TabStop = false;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Items.AddRange(new object[] {
            "Income",
            "Deduction",
            "Text"});
            this.cboType.Location = new System.Drawing.Point(136, 72);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(224, 21);
            this.cboType.TabIndex = 2;
            this.toolTipComponent.SetToolTip(this.cboType, "Choose the type of the component");
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            this.cboType.Leave += new System.EventHandler(this.cboType_Leave);
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.White;
            this.lblType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.Red;
            this.lblType.Location = new System.Drawing.Point(8, 72);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(112, 24);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(136, 40);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(224, 20);
            this.txtDescription.TabIndex = 1;
            this.toolTipComponent.SetToolTip(this.txtDescription, "Enter the component description");
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.White;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(8, 40);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(112, 24);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            // 
            // txtComponent
            // 
            this.txtComponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComponent.Location = new System.Drawing.Point(136, 8);
            this.txtComponent.MaxLength = 40;
            this.txtComponent.Name = "txtComponent";
            this.txtComponent.Size = new System.Drawing.Size(224, 20);
            this.txtComponent.TabIndex = 0;
            this.toolTipComponent.SetToolTip(this.txtComponent, "Enter unique Component Name");
            this.txtComponent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComponent_KeyPress);
            // 
            // lblComponent
            // 
            this.lblComponent.BackColor = System.Drawing.Color.White;
            this.lblComponent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComponent.ForeColor = System.Drawing.Color.Red;
            this.lblComponent.Location = new System.Drawing.Point(8, 10);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(112, 24);
            this.lblComponent.TabIndex = 0;
            this.lblComponent.Text = "Component";
            // 
            // grpDefault
            // 
            this.grpDefault.BackColor = System.Drawing.Color.White;
            this.grpDefault.Controls.Add(this.btnBuild);
            this.grpDefault.Controls.Add(this.rdoFixedValue);
            this.grpDefault.Controls.Add(this.cboLinkValue);
            this.grpDefault.Controls.Add(this.txtMaxSlab);
            this.grpDefault.Controls.Add(this.label1);
            this.grpDefault.Controls.Add(this.txtEquation);
            this.grpDefault.Controls.Add(this.rdoEquation);
            this.grpDefault.Controls.Add(this.rdoLinkValue);
            this.grpDefault.Controls.Add(this.txtFixedValue);
            this.grpDefault.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDefault.ForeColor = System.Drawing.Color.Red;
            this.grpDefault.Location = new System.Drawing.Point(8, 120);
            this.grpDefault.Name = "grpDefault";
            this.grpDefault.Size = new System.Drawing.Size(368, 152);
            this.grpDefault.TabIndex = 1;
            this.grpDefault.TabStop = false;
            this.grpDefault.Text = "Default Value";
            this.toolTipComponent.SetToolTip(this.grpDefault, "Select a value");
            // 
            // btnBuild
            // 
            this.btnBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.btnBuild.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuild.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuild.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnBuild.Location = new System.Drawing.Point(128, 88);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(56, 24);
            this.btnBuild.TabIndex = 5;
            this.btnBuild.Text = "Build";
            this.toolTipComponent.SetToolTip(this.btnBuild, "Build Conditions in Equation");
            this.btnBuild.UseVisualStyleBackColor = false;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            this.btnBuild.Enter += new System.EventHandler(this.btnBuild_Enter);
            // 
            // rdoFixedValue
            // 
            this.rdoFixedValue.BackColor = System.Drawing.Color.White;
            this.rdoFixedValue.Checked = true;
            this.rdoFixedValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoFixedValue.Location = new System.Drawing.Point(8, 24);
            this.rdoFixedValue.Name = "rdoFixedValue";
            this.rdoFixedValue.Size = new System.Drawing.Size(104, 24);
            this.rdoFixedValue.TabIndex = 0;
            this.rdoFixedValue.TabStop = true;
            this.rdoFixedValue.Text = "Fixed Value";
            this.rdoFixedValue.UseVisualStyleBackColor = false;
            this.rdoFixedValue.Click += new System.EventHandler(this.rdoFixedValue_Click);
            // 
            // cboLinkValue
            // 
            this.cboLinkValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLinkValue.Location = new System.Drawing.Point(128, 56);
            this.cboLinkValue.Name = "cboLinkValue";
            this.cboLinkValue.Size = new System.Drawing.Size(232, 22);
            this.cboLinkValue.TabIndex = 3;
            this.toolTipComponent.SetToolTip(this.cboLinkValue, "Choose a link value");
            this.cboLinkValue.Enter += new System.EventHandler(this.cboLinkValue_Enter);
            // 
            // txtMaxSlab
            // 
            this.txtMaxSlab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxSlab.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxSlab.Location = new System.Drawing.Point(192, 120);
            this.txtMaxSlab.MaxLength = 5;
            this.txtMaxSlab.Name = "txtMaxSlab";
            this.txtMaxSlab.Size = new System.Drawing.Size(168, 20);
            this.txtMaxSlab.TabIndex = 7;
            this.toolTipComponent.SetToolTip(this.txtMaxSlab, "Enter Max Slab");
            this.txtMaxSlab.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxSlab_KeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(128, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Max Slab";
            // 
            // txtEquation
            // 
            this.txtEquation.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtEquation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEquation.Enabled = false;
            this.txtEquation.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEquation.Location = new System.Drawing.Point(192, 88);
            this.txtEquation.MaxLength = 700;
            this.txtEquation.Name = "txtEquation";
            this.txtEquation.ReadOnly = true;
            this.txtEquation.Size = new System.Drawing.Size(168, 20);
            this.txtEquation.TabIndex = 6;
            // 
            // rdoEquation
            // 
            this.rdoEquation.BackColor = System.Drawing.Color.White;
            this.rdoEquation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoEquation.Location = new System.Drawing.Point(8, 88);
            this.rdoEquation.Name = "rdoEquation";
            this.rdoEquation.Size = new System.Drawing.Size(104, 24);
            this.rdoEquation.TabIndex = 4;
            this.rdoEquation.Text = "Equation";
            this.rdoEquation.UseVisualStyleBackColor = false;
            this.rdoEquation.Click += new System.EventHandler(this.rdoEquation_Click);
            this.rdoEquation.CheckedChanged += new System.EventHandler(this.rdoEquation_CheckedChanged);
            // 
            // rdoLinkValue
            // 
            this.rdoLinkValue.BackColor = System.Drawing.Color.White;
            this.rdoLinkValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoLinkValue.Location = new System.Drawing.Point(8, 56);
            this.rdoLinkValue.Name = "rdoLinkValue";
            this.rdoLinkValue.Size = new System.Drawing.Size(104, 24);
            this.rdoLinkValue.TabIndex = 2;
            this.rdoLinkValue.Text = "Link Value";
            this.rdoLinkValue.UseVisualStyleBackColor = false;
            this.rdoLinkValue.Click += new System.EventHandler(this.rdoLinkValue_Click);
            // 
            // txtFixedValue
            // 
            this.txtFixedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFixedValue.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFixedValue.Location = new System.Drawing.Point(128, 24);
            this.txtFixedValue.MaxLength = 10;
            this.txtFixedValue.Name = "txtFixedValue";
            this.txtFixedValue.Size = new System.Drawing.Size(232, 20);
            this.txtFixedValue.TabIndex = 1;
            this.toolTipComponent.SetToolTip(this.txtFixedValue, "Enter a fixed value");
            this.txtFixedValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFixedValue_KeyPress);
            this.txtFixedValue.Enter += new System.EventHandler(this.txtFixedValue_Enter);
            // 
            // grpRoundedOption
            // 
            this.grpRoundedOption.BackColor = System.Drawing.Color.White;
            this.grpRoundedOption.Controls.Add(this.rdoNone);
            this.grpRoundedOption.Controls.Add(this.rdoPreviousOrNext);
            this.grpRoundedOption.Controls.Add(this.rdoFloor);
            this.grpRoundedOption.Controls.Add(this.rdoCeiling);
            this.grpRoundedOption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRoundedOption.Location = new System.Drawing.Point(8, 280);
            this.grpRoundedOption.Name = "grpRoundedOption";
            this.grpRoundedOption.Size = new System.Drawing.Size(368, 144);
            this.grpRoundedOption.TabIndex = 2;
            this.grpRoundedOption.TabStop = false;
            this.grpRoundedOption.Text = "Rounded Option";
            this.toolTipComponent.SetToolTip(this.grpRoundedOption, "Select a rounding option");
            // 
            // rdoNone
            // 
            this.rdoNone.BackColor = System.Drawing.Color.White;
            this.rdoNone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoNone.Location = new System.Drawing.Point(8, 112);
            this.rdoNone.Name = "rdoNone";
            this.rdoNone.Size = new System.Drawing.Size(176, 24);
            this.rdoNone.TabIndex = 3;
            this.rdoNone.TabStop = true;
            this.rdoNone.Text = "None";
            this.rdoNone.UseVisualStyleBackColor = false;
            // 
            // rdoPreviousOrNext
            // 
            this.rdoPreviousOrNext.BackColor = System.Drawing.Color.White;
            this.rdoPreviousOrNext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoPreviousOrNext.Location = new System.Drawing.Point(8, 72);
            this.rdoPreviousOrNext.Name = "rdoPreviousOrNext";
            this.rdoPreviousOrNext.Size = new System.Drawing.Size(352, 32);
            this.rdoPreviousOrNext.TabIndex = 2;
            this.rdoPreviousOrNext.TabStop = true;
            this.rdoPreviousOrNext.Text = "Rounded to Next or Previous Integer  (Eg. 51.59 => 51 or 51.50 =>52 )";
            this.rdoPreviousOrNext.UseVisualStyleBackColor = false;
            // 
            // rdoFloor
            // 
            this.rdoFloor.BackColor = System.Drawing.Color.White;
            this.rdoFloor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoFloor.Location = new System.Drawing.Point(8, 48);
            this.rdoFloor.Name = "rdoFloor";
            this.rdoFloor.Size = new System.Drawing.Size(184, 24);
            this.rdoFloor.TabIndex = 1;
            this.rdoFloor.TabStop = true;
            this.rdoFloor.Text = "Floor (Eg. 51.99 => 51";
            this.rdoFloor.UseVisualStyleBackColor = false;
            // 
            // rdoCeiling
            // 
            this.rdoCeiling.BackColor = System.Drawing.Color.White;
            this.rdoCeiling.Checked = true;
            this.rdoCeiling.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdoCeiling.Location = new System.Drawing.Point(8, 24);
            this.rdoCeiling.Name = "rdoCeiling";
            this.rdoCeiling.Size = new System.Drawing.Size(280, 24);
            this.rdoCeiling.TabIndex = 0;
            this.rdoCeiling.TabStop = true;
            this.rdoCeiling.Text = "Ceiling (Eg. 51.15 => 52";
            this.rdoCeiling.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.btnSave.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.Location = new System.Drawing.Point(224, 462);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.toolTipComponent.SetToolTip(this.btnSave, "Save payroll component");
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.btnCancel.BackgroundImage = global::PAYROLL.Properties.Resources.Wizard_4615A142_copy2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnCancel.Location = new System.Drawing.Point(304, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Close";
            this.toolTipComponent.SetToolTip(this.btnCancel, "Close payroll component");
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlGroup
            // 
            this.pnlGroup.BackColor = System.Drawing.Color.White;
            this.pnlGroup.Controls.Add(this.chkIsEditable);
            this.pnlGroup.Controls.Add(this.btnCancel);
            this.pnlGroup.Controls.Add(this.chkShowinBrowse);
            this.pnlGroup.Controls.Add(this.btnSave);
            this.pnlGroup.Location = new System.Drawing.Point(0, 0);
            this.pnlGroup.Name = "pnlGroup";
            this.pnlGroup.Size = new System.Drawing.Size(384, 488);
            this.pnlGroup.TabIndex = 5;
            this.pnlGroup.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGroup_Paint);
            // 
            // chkIsEditable
            // 
            this.chkIsEditable.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkIsEditable.Location = new System.Drawing.Point(139, 432);
            this.chkIsEditable.Name = "chkIsEditable";
            this.chkIsEditable.Size = new System.Drawing.Size(144, 24);
            this.chkIsEditable.TabIndex = 4;
            this.chkIsEditable.Text = "Non Editable";
            // 
            // chkShowinBrowse
            // 
            this.chkShowinBrowse.Checked = true;
            this.chkShowinBrowse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowinBrowse.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkShowinBrowse.Location = new System.Drawing.Point(16, 432);
            this.chkShowinBrowse.Name = "chkShowinBrowse";
            this.chkShowinBrowse.Size = new System.Drawing.Size(144, 24);
            this.chkShowinBrowse.TabIndex = 3;
            this.chkShowinBrowse.Text = "Show in Browse Form";
            // 
            // frmComponent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 492);
            this.Controls.Add(this.grpDefault);
            this.Controls.Add(this.grpRoundedOption);
            this.Controls.Add(this.grpComponent);
            this.Controls.Add(this.pnlGroup);
            this.MinimizeBox = false;
            this.Name = "frmComponent";
            this.Text = "Payroll Component";
            this.Load += new System.EventHandler(this.frmComponent_Load);
            this.grpComponent.ResumeLayout(false);
            this.grpComponent.PerformLayout();
            this.grpDefault.ResumeLayout(false);
            this.grpDefault.PerformLayout();
            this.grpRoundedOption.ResumeLayout(false);
            this.pnlGroup.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		

		private void btnBuild_Click(object sender, System.EventArgs e)
		{
			
			string[] strEquation;
			if(txtEquation.Text!="")
			{
				strEquation=txtEquation.Text.Trim().Split(Convert.ToChar(160));
				switch(strEquation.Length)
				{
					case 6:
						IFCon = 1;
						break;
					case 10:
						IFCon = 2;
						break;
					case 8:
						IFCon = 3;
						break;	
					case 12:
						IFCon = 4;
						break;	
					case 1:
						IFCon = 0;
						break;
					default:
						IFCon = 0;
						break;
				}
			}
			if(strOperation=="Edit")
			{
				frmBuildIFCondition objBuild=new frmBuildIFCondition(this ,"Edit",rdoEquation);
				objBuild.ShowDialog();
			}
			if(strOperation=="Add")
			{
				frmBuildIFCondition objAdd=new frmBuildIFCondition(this,"Add",rdoEquation);
				objAdd.ShowDialog();
			}
			rdoEquation_CheckedChanged(sender, new EventArgs());
		}

		private void rdoFixedValue_Click(object sender, System.EventArgs e)
		{
			txtMaxSlab.Clear();
			txtEquation.Clear();
			cboLinkValue.Text       = "";
			txtFixedValue.Enabled   = true;
			cboLinkValue.Enabled    = false;
			cboLinkValue.BackColor  = Color.White;
			btnBuild.Enabled        = false;
			txtEquation.Enabled     = false;
			txtEquation.BackColor   = Color.White;
			txtMaxSlab.Enabled      = false;
			txtMaxSlab.BackColor    = Color.White;
		}

		private void rdoLinkValue_Click(object sender, System.EventArgs e)
		{
			txtFixedValue.Clear();
			txtEquation.Clear();
            //CValue = string.Empty;
			cboLinkValue.Enabled    = true;
			txtFixedValue.Enabled   = false;
			txtEquation.Enabled     = false;
			txtFixedValue.BackColor = Color.White;
			txtEquation.BackColor   = Color.White;
			btnBuild.Enabled        = false;
			txtMaxSlab.Enabled      = false;
			txtMaxSlab.BackColor    = Color.White;
		}

		private void rdoEquation_Click(object sender, System.EventArgs e)
		{
			txtFixedValue.Clear();
			cboLinkValue.SelectedIndex    = -1;
			txtFixedValue.Enabled         = false;
			cboLinkValue.Enabled          = false;
			btnBuild.Enabled              = true;
			txtEquation.Enabled           = true;
			txtMaxSlab.Enabled            = true;
			txtFixedValue.BackColor       = Color.White;
			cboLinkValue.BackColor        = Color.White;
			txtMaxSlab.BackColor          = Color.White;
		}
		private void frmComponent_Load(object sender, System.EventArgs e)
		{
			
			DisableControls();
			
			
			try
			{
				if ( strOperation == "Edit" )
				{
					ComponentId            = long.Parse(Parent.ucGrdPayroll.ColValue(0));
					txtComponent.Text      = Parent.ucGrdPayroll.ColValue(1).ToString();
					txtDescription.Text    = Parent.ucGrdPayroll.ColValue(2).ToString();
					if ( Parent.ucGrdPayroll.ColValue(3).ToString() == "Income" )
						cboType.SelectedIndex = 0;
					if ( Parent.ucGrdPayroll.ColValue(3).ToString() == "Deduction" )
						cboType.SelectedIndex = 1;
					if ( Parent.ucGrdPayroll.ColValue(3).ToString() == "Text" )
						cboType.SelectedIndex = 2;
                    if (Parent.ucGrdPayroll.ColValue(4).ToString().Trim() != "")
					{
						DisableControls();
						rdoFixedValue.Enabled = true;
						rdoFixedValue.Checked = true;
						txtFixedValue.Text    = Parent.ucGrdPayroll.ColValue(4).ToString();
					}
                    if (Parent.ucGrdPayroll.ColValue(5).ToString().Trim() != "")
					{
						DisableControls();
						rdoLinkValue.Enabled  = true;
						rdoLinkValue.Checked  = true;
						cboLinkValue.Enabled  = true;
						string strLink        = objCompBuild.GetLinkName(Parent.ucGrdPayroll.ColValue(5),true);
						cboLinkValue.Text     = strLink;
						
					}
                    if (Parent.ucGrdPayroll.ColValue(6).ToString().Trim()!= "")
					{
						DisableControls();
						rdoEquation.Checked  = true;
						rdoEquation.Enabled  = true;
						txtEquation.Enabled  = true;
						txtMaxSlab.Enabled   = true;
						btnBuild.Enabled     = true;
						txtEquation.Text     = Parent.ucGrdPayroll.ColValue(6).ToString();
						txtMaxSlab.Text      = Parent.ucGrdPayroll.ColValue(8).ToString();
						CValue               = Parent.ucGrdPayroll.ColValue(7).ToString();
					}
					// Formula 
					string strEquation       = Parent.ucGrdPayroll.ColValue(6).ToString();
					
					if ( Parent.ucGrdPayroll.ColValue(9).ToString() == "1" )
						rdoCeiling.Checked        = true;
					if ( Parent.ucGrdPayroll.ColValue(9).ToString() == "2" )
						rdoFloor.Checked          = true;
					if ( Parent.ucGrdPayroll.ColValue(9).ToString() == "3" )
						rdoPreviousOrNext.Checked = true;
					if ( Parent.ucGrdPayroll.ColValue(9).ToString() == "0" )
						rdoNone.Checked           = true;
					IFCon                         = Convert.ToInt32(Parent.ucGrdPayroll.ColValue(10));
					iCondition                    = IFCon; 
					int iShow                         = Convert.ToInt32(Parent.ucGrdPayroll.ColValue(11));
					if (iShow == 1)
						chkShowinBrowse.Checked = true;
					else
						chkShowinBrowse.Checked = false;
                    string iseditable = Parent.ucGrdPayroll.ColValue(12).ToString();

                    // Enable and disable fields based on the edit type by Pragasam
                    if (iseditable.Equals("Editable"))
                    {
                        chkIsEditable.Checked = false;
                    }
                    else
                    {
                        chkIsEditable.Checked = true;
                    }
				}
				txtFixedValue.Enabled = true;
				cboLinkValue.Enabled  = true;
				txtMaxSlab.Enabled    = true;
				btnBuild.Enabled	  = true;
			}
			catch
			{
			}
		}
		private void DisableControls()
		{
			txtEquation.Enabled          = false;
			btnBuild.Enabled             = false;
			txtFixedValue.Enabled        = false;
			txtEquation.Enabled          = false;
			txtMaxSlab.Enabled           = false;
			cboLinkValue.Enabled         = false;
			txtFixedValue.BackColor      = Color.White;
			txtEquation.BackColor        = Color.White;
			cboLinkValue.BackColor       = Color.White;
			txtMaxSlab.BackColor         = Color.White;
		}
		
		private void cboType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( cboType.SelectedIndex == 2 )
			{
				txtEquation.Enabled  = false;
				txtMaxSlab.Enabled   = false;
				btnBuild.Enabled     = false;
				rdoEquation.Enabled  = false;
			}
			else
			{
				txtEquation.Enabled  = true;
				txtMaxSlab.Enabled   = true;
				txtMaxSlab.ReadOnly  = false;
				btnBuild.Enabled     = true;
				rdoEquation.Enabled  = true;
			}
			if ( cboType.SelectedIndex == 1 )
			{
				cboLinkValue.Items.Clear();
				cboLinkValue.Items.Add("");
				
			}
			if ( cboType.SelectedIndex == 0 )
			{
				cboLinkValue.Items.Clear();
				cboLinkValue.Items.Add("");
			}
			try
			{
				objCompBuild.FillLinkValue(cboLinkValue,cboType.SelectedIndex,"Basic Pay");
			}
			catch
			{
			}
		}
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		private bool ValidateComponent()
		{
			if ( txtComponent.Text == "" )
			{
				MessageBox.Show("Component field can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtComponent.Focus();
				return false;
			}
			if ( cboType.Text == "" )
			{
				MessageBox.Show("Select the type !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				cboType.Focus();
				return false;
			}
			if ( rdoFixedValue.Checked == true && txtFixedValue.Text == "" )
			{
				MessageBox.Show("Fixed value can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				txtFixedValue.Focus();
				return false;
			}
			if( rdoLinkValue.Checked == true && cboLinkValue.Text == "" )
			{
				MessageBox.Show("Link value can't be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				cboLinkValue.Focus();
				return false;
			}
			if ( rdoEquation.Checked == true && txtEquation.Text == "" )
			{
				MessageBox.Show("Equation can not be empty !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				btnBuild.Focus();
				return false;
			}
			
			return true;
		}
		private void ClearControls()
		{
			txtComponent.Text          = "";
			txtDescription.Text        = "";
			cboType.SelectedIndex      = -1;
			cboLinkValue.SelectedIndex = -1;
			txtFixedValue.Text         = "";
			txtMaxSlab.Text            = "";
			txtEquation.Text           = "";
            CValue = string.Empty;
			
		}
		private void AssignValues()
		{
			
			if ( IFCon == 0 )
			{
				IFCon = 0;
			}
			if ( txtFixedValue.Text.Trim() == "" )
			{
				txtFixedValue.Text = "0";
			}
			if ( txtDescription.Text.Trim() == "" )
			{
				txtDescription.Text  = "#";
			}
			if ( txtEquation.Text == "" )
			{
				txtEquation.Text    = "#";
			}
			if (CValue == "" )
			{
				CValue  = "#";
			}
			if ( txtMaxSlab.Text == "" )
			{
				txtMaxSlab.Text  = "0";
			}
			if ( cboLinkValue.Text != "" )
			{
				try
				{
					strLinkValue = objCompBuild.GetLinkName(cboLinkValue.Text.Trim(),false);
				}
				catch
				{
				}
			}
			if ( cboLinkValue.Text.Trim() == "" )
			{
				strLinkValue = "#";
			}
			sCompStr = sCompStr + "COMPONENT|" + txtComponent.Text.Trim() + "@DESCRIPTION|" + txtDescription.Text.Trim() + "@TYPE|" + cboType.SelectedIndex.ToString();
			if ( rdoFixedValue.Checked == true )
				sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text.Trim()+"@LNKVALUE|" +strLinkValue+"@EQUATION|" + txtEquation.Text.Trim() + "@EQUATIONID|" + CValue;
			if ( rdoLinkValue.Checked == true )
				sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text.Trim()+"@LNKVALUE|" + strLinkValue+"@EQUATION|" + txtEquation.Text.Trim() + "@EQUATIONID|" + CValue;
			if ( rdoEquation.Checked == true )
			{
                sCompStr = sCompStr + "@DEFVALUE|" + txtFixedValue.Text.Trim() + "@LNKVALUE|" + strLinkValue + "@EQUATION|" + txtEquation.Text.Trim() + "@EQUATIONID|" + CValue + "@MAXSLAP|" + txtMaxSlab.Text.ToString();
			}
			else
			{
                sCompStr = sCompStr + "@MAXSLAP|" + txtMaxSlab.Text.ToString();
			}
			if ( rdoCeiling.Checked == true )
				sCompStr = sCompStr + "@COMPROUND|" + "1";
			else if ( rdoFloor.Checked == true )
				sCompStr = sCompStr + "@COMPROUND|" + "2";
			else if ( rdoPreviousOrNext.Checked == true )
				sCompStr = sCompStr + "@COMPROUND|" + "3";
			else 
				sCompStr = sCompStr + "@COMPROUND|" + "0";
			
			sCompStr = sCompStr + "@IFCONDITION|" + IFCon.ToString();
			if (chkShowinBrowse.Checked == true)
				sCompStr = sCompStr + "@SHOWINBROWSE|" + "1";
			else
				sCompStr = sCompStr + "@SHOWINBROWSE|" + "0" ;

			sRelatedComponents = new clsEvalExpr().BuildComponentIdFromFormula(txtEquation.Text);
			sCompStr += "@RELATEDCOMPONENTS|" + (sRelatedComponents == "" ? "#" : sRelatedComponents) ; 
			
		}
		private bool checkExistComponent()
		{
			dt=objCompBuild.CheckDuplicateComponent();
			bool isExist=false;
			if(dt.Rows.Count > 0)
			{
				foreach(DataRow dr in dt.Rows)
				{
					if(dr[0].ToString().ToUpper()==txtComponent.Text.Trim().ToUpper())
					{
						return true;
					}
					isExist = false;
				}
				isExist =false;
			}
			return isExist;
		}
		private bool checkEditComponent()
		{
			dt=objCompBuild.CheckEditComponent(ComponentId);
			bool isExist=false;
			if(dt.Rows.Count > 0)
			{
				foreach(DataRow dr in dt.Rows)
				{
					if(dr[0].ToString().ToUpper()==txtComponent.Text.Trim().ToUpper())
					{
						return true;
					}
					isExist = false;
				}
				isExist =false;
			}
			return isExist;
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
                int isEditable;
                /* set edit type 
                 * 1 - not editable
                 * 0 - Editable
                 */
                if (chkIsEditable.Checked.Equals(true))
                {
                    isEditable = 1;
                }
                else
                {
                    isEditable = 0;
                }

				if ( ValidateComponent() )
				{
					if( checkExistComponent()& (strOperation == "Add")  )
					{
						MessageBox.Show("The Component Name Exists Already!","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
						ClearControls();
						sCompStr="";
						txtComponent.Focus();
						return;
					}
					
					sCompStr =  sRelatedComponents = "";
					
					AssignValues();

					if (VerifyInterReference())
					{
						MessageBox.Show( txtComponent.Text  + " can not refer itself in formula !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Error);
						return;
					}
					
					if(VerifyCircularReference()){return;}
                    
					if ( strOperation == "Add" )
					{						
                    //    //Save data	
                    //    if (objCompBuild.SaveComponent(ComponentId,sCompStr,txtComponent.Text.Trim(),txtDescription.Text.Trim(),isEditable))
                    //    {
                    //        MessageBox.Show(" Component Details are saved Successfully  !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //        ClearControls();
                    //        sCompStr="";
                    //        txtComponent.Focus();
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        txtEquation.Text    = "";
                    //        txtDescription.Text = "";
                    //        MessageBox.Show("The Component Exists Already!","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //        sCompStr="";
                    //        ClearControls();
                    //        return;
                    //    }
                    //}
                    //if ( strOperation == "Edit" )
                    //{
                    //    if( checkEditComponent() )
                    //    {
                    //        MessageBox.Show("The Component Exists Already!","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //        txtComponent.Clear();
                    //        txtComponent.Focus();
                    //        return;
                    //    }
                    //    //Edit data
                    //    if (objCompBuild.UpdateComponent(ComponentId,sCompStr,txtComponent.Text.Trim(),txtDescription.Text.Trim(),isEditable) )
                    //    {
                    //        long payrollId = new clsPrGateWay().GetCurrentPayroll();
                    //        bool refCompUpdateStatus = false;

                    //        if(VerifyCurrentPayrollDependency(ComponentId,payrollId))
                    //        {
                    //            if(UpdateComponentChanges(sCompStr,ComponentId,payrollId))
                    //            {
                    //                refCompUpdateStatus = true;
                    //            }
                    //        }
                    //        else 
                    //            refCompUpdateStatus = true;
                    //        if(refCompUpdateStatus)
                    //            MessageBox.Show("Component is Updated Successfully !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //        else
                    //            MessageBox.Show("Could not Updated the component !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Error);
							
                    //        this.Close();
                    //        //================================
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("This Component exists Already !","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //        txtComponent.Clear();
                    //        txtComponent.Focus();
                    //        return;
                    //    }
					}
				}
			}
			catch(Exception ex)
			{
				string msg = ex.Message;
			}
		}

		// By Peter 11-09-2008 To introuduce validation of circular refernce
		
		#region 1.Verify Inter Reference it means that if the same component is used in the formula

		private bool VerifyInterReference()
		{
			if(sRelatedComponents == "") return false;
			if(sRelatedComponents.IndexOf("ê"+ComponentId+"ê") > 0) return true;
			else return false;
		}

		#endregion

		#region 2.Verifying if any circular Dependecy across components for the component created it means that <A> refers <B> and <B> refers <A>

		private bool VerifyCircularReference()
		{
			if(sRelatedComponents == "")return false;

			if(dvComponents == null) 
				dvComponents = objPayroll.getPayrollComponent().DefaultView;

			sCircularComponentName = "";
			if(VerifyFormulaReference(sRelatedComponents,txtComponent.Text))
			{
				MessageBox.Show( txtComponent.Text +" circularly references with " + sCircularComponentName + " ,hence can not proceed, remove it from the formula" ,"Payroll",MessageBoxButtons.OK,MessageBoxIcon.Error); 
				sCircularComponentName = "";
				return true;
			}
			return false;
		}

		private bool VerifyFormulaReference(string relatedComponents,string sourceComponentName)
		{
			try
			{
				//1.If the related component list is empty it contains no reference
				if(relatedComponents == "" || relatedComponents == "#")return false; 

				string[] aRelatedComp = relatedComponents.Split('ê');
 
				foreach(string relatedComponent in aRelatedComp)
				{
					if(relatedComponent == "") continue;

					if(relatedComponent == ComponentId.ToString())
					{
						//2.This is the one identifies if the component is the same as the current component
						sCircularComponentName = sourceComponentName; 
						return true;
					}

					dvComponents.RowFilter = "[COMPONENTID]='" + relatedComponent + "'";
					if(dvComponents.Count == 1)
					{
						//3.To call recursively until the current referring component finishes its life cycle
						if (VerifyFormulaReference(dvComponents[0]["RELATEDCOMPONENTS"].ToString(),dvComponents[0]["COMPONENT"].ToString()))
						{
							return true;
						}
					}
				}
				return false;
			}
			catch(Exception ex)
			{
				string msg = ex.Message;
				return false;
			}
		}

		#endregion

		#region Verifying if the formula edited is already processed for the current payroll

		private bool VerifyCurrentPayrollDependency(long componentId,long payrollId)
		{
            object strQuery = objPayroll.getPayrollComponentQuery(clsPayrollConstants.PAYROLL_EDIT_VERIFY_COMP_LINK);
			
            //strQuery = strQuery.Replace("<PAYROLLID>",payrollId.ToString());
            //strQuery = strQuery.Replace("<COMPONENTID>",componentId.ToString());
            using (DataManager dataManager = new DataManager(strQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            string componentMapped = resultArgs.DataSource.Sclar.ToString;
			if(componentMapped == "" || componentMapped == "0") return false;
			else return true;
			
		}

		#endregion

		#region Updating the edited Component Values in the current Payroll

		private bool UpdateComponentChanges(string sCompStr,long componentId,long payrollId)
		{
            return objPayroll.UpdateComponentChanges(sCompStr,componentId,payrollId);
            //object strQuery = objPayroll.getPayrollComponentQuery(clsPayrollConstants.PAYROLL_EDIT_COMP_UPDATE);
            //string[] aFields = sCompStr.Split('@');
            //string[] aFieldDetails =  new string[2];
			
            //foreach(string field in  aFields)
            //{
            //    aFieldDetails = field.Split('|');
				
            //    if(aFieldDetails[1]== "#")
            //        aFieldDetails[1] = ""; 

            //    switch(aFieldDetails[0])
            //    {
            //        case "TYPE":
            //            //strQuery = strQuery.Replace("<TYPE>",aFieldDetails[1]);
            //            break;
            //        case "DEFVALUE":
            //            //strQuery = strQuery.Replace("<DEFVALUE>",aFieldDetails[1]);
            //            break;
            //        case "EQUATION":
            //            //strQuery = strQuery.Replace("<EQUATION>",aFieldDetails[1]);
            //            break;
            //        case "EQUATIONID":
            //            //strQuery = strQuery.Replace("<EQUATIONID>",aFieldDetails[1]);
            //            break;
            //        case "MAXSLAP":
            //            //strQuery = strQuery.Replace("<MAXSLAP>",aFieldDetails[1]);
            //            break;
            //        case "LNKVALUE":
            //            //strQuery = strQuery.Replace("<LNKVALUE>",aFieldDetails[1]);
            //            break;    
            //        case "COMPROUND":
            //            //strQuery = strQuery.Replace("<COMPROUND>",aFieldDetails[1]);
            //            break;   
            //        case "IFCONDITION":
            //            //strQuery = strQuery.Replace("<IFCONDITION>",aFieldDetails[1]);
            //            break; 
            //    }
            //}
			
		//	strQuery = strQuery.Replace("<PAYROLLID>",payrollId.ToString());
		//	strQuery = strQuery.Replace("<COMPONENTID>",componentId.ToString());
            //using (DataManager dataManager = new DataManager(strQuery))
            //{
            //    dataManager.Parameters.Add();
            //    dataManager.Parameters.Add();
            //    dataManager.Parameters.Add();
            //    dataManager.Parameters.Add();
            //    dataManager.Parameters.Add();
            //    dataManager.Parameters.Add();
            //    dataManager.Parameters.Add();
            //    dataManager.Parameters.Add();
            //    resultArgs = dataManager.UpdateData();
            //}
			
		}

		#endregion

		//======================================================================

		private void rdoEquation_CheckedChanged(object sender, System.EventArgs e)
		{
			btnBuild.Enabled    = true;
			txtMaxSlab.Enabled  = true;
		}

		private void txtMaxSlab_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled =! objR.IsMatch(e.KeyChar.ToString());
		}
		private void txtFixedValue_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled =! objR.IsMatch(e.KeyChar.ToString());
			
		}
		private void txtComponent_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			Regex objText = new Regex("([a-zA-Z 0-9 ( ) + - * /])|-|\b"); 
			e.Handled     =! objText.IsMatch(e.KeyChar.ToString());
		}

		private void txtDescription_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			Regex objText = new Regex("([a-zA-Z ])|\b");
			e.Handled     =! objText.IsMatch(e.KeyChar.ToString());
		}
		private void cboType_Leave(object sender, System.EventArgs e)
		{
			rdoFixedValue.Checked = true;
			txtFixedValue.Focus();
            rdoFixedValue_Click(sender, e);
		}

		private void txtFixedValue_Enter(object sender, System.EventArgs e)
		{
			rdoFixedValue.Checked = true;
            rdoFixedValue_Click(sender, e);
		}

		private void cboLinkValue_Enter(object sender, System.EventArgs e)
		{
			rdoLinkValue.Checked = true;
            rdoLinkValue_Click(sender, e);
		}

		private void btnBuild_Enter(object sender, System.EventArgs e)
		{
			rdoEquation.Checked = true;
            
		}

		private void pnlGroup_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

	}
}
