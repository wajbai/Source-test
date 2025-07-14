using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PAYROLL.UserControl
{
	public class ucDatePicker : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private PAYROLL.UserControl.TextboxControl txtYear;
        private PAYROLL.UserControl.TextboxControl txtMonth;
        private PAYROLL.UserControl.TextboxControl txtDay;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblBack;
		private System.ComponentModel.Container components = null;

		public ucDatePicker()
		{
			InitializeComponent();
		}

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtYear = new PAYROLL.UserControl.TextboxControl();
            this.txtMonth = new PAYROLL.UserControl.TextboxControl();
            this.txtDay = new PAYROLL.UserControl.TextboxControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblBack = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
			this.dateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.DarkSeaGreen;
			this.dateTimePicker1.CalendarTrailingForeColor = System.Drawing.Color.Silver;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePicker1.Location = new System.Drawing.Point(78, 0);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(19, 20);
			this.dateTimePicker1.TabIndex = 3;
			this.dateTimePicker1.TabStop = false;
			this.dateTimePicker1.Value = new System.DateTime(2006, 2, 21, 15, 36, 21, 93);
			this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label2.Location = new System.Drawing.Point(39, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(7, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "/";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(16, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(7, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "/";
			// 
			// txtYear
			// 
			this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtYear.Location = new System.Drawing.Point(48, 3);
			this.txtYear.MaxLength = 4;
			this.txtYear.Name = "txtYear";
			this.txtYear.Required = false;
			this.txtYear.Size = new System.Drawing.Size(32, 13);
			this.txtYear.TabIndex = 2;
			this.txtYear.Text = "";
            this.txtYear.TextType = PAYROLL.UserControl.enumTextType.Numeric;
			this.txtYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleKeyDown);
			this.txtYear.Leave += new System.EventHandler(this.txtYear_Leave);
			this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleKeyPress);
			this.txtYear.Enter += new System.EventHandler(this.HandleEnter);
			// 
			// txtMonth
			// 
			this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMonth.Location = new System.Drawing.Point(26, 3);
			this.txtMonth.MaxLength = 2;
			this.txtMonth.Name = "txtMonth";
			this.txtMonth.Required = false;
			this.txtMonth.Size = new System.Drawing.Size(16, 13);
			this.txtMonth.TabIndex = 1;
			this.txtMonth.Text = "";
			this.txtMonth.TextType = PAYROLL.UserControl.enumTextType.Numeric;
			this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleKeyDown);
			this.txtMonth.Leave += new System.EventHandler(this.txtMonth_Leave);
			this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleKeyPress);
			this.txtMonth.Enter += new System.EventHandler(this.HandleEnter);
			// 
			// txtDay
			// 
			this.txtDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDay.Location = new System.Drawing.Point(3, 3);
			this.txtDay.MaxLength = 2;
			this.txtDay.Name = "txtDay";
			this.txtDay.Required = false;
			this.txtDay.Size = new System.Drawing.Size(16, 13);
			this.txtDay.TabIndex = 0;
			this.txtDay.Text = "";
            this.txtDay.TextType = PAYROLL.UserControl.enumTextType.Numeric;
			this.txtDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleKeyDown);
			this.txtDay.Leave += new System.EventHandler(this.txtDay_Leave);
			this.txtDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleKeyPress);
			this.txtDay.Enter += new System.EventHandler(this.HandleEnter);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.dateTimePicker1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.txtYear);
			this.panel1.Controls.Add(this.txtMonth);
			this.panel1.Controls.Add(this.txtDay);
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(97, 19);
			this.panel1.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label6.Location = new System.Drawing.Point(94, -1);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(5, 23);
			this.label6.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label5.Location = new System.Drawing.Point(80, 17);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(23, 4);
			this.label5.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label4.Location = new System.Drawing.Point(79, -1);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(23, 4);
			this.label4.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.label3.Location = new System.Drawing.Point(77, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(3, 23);
			this.label3.TabIndex = 1;
			// 
			// lblBack
			// 
			this.lblBack.BackColor = System.Drawing.Color.Gray;
			this.lblBack.Location = new System.Drawing.Point(-7, -24);
			this.lblBack.Name = "lblBack";
			this.lblBack.Size = new System.Drawing.Size(180, 150);
			this.lblBack.TabIndex = 1;
			this.lblBack.Text = "label7";
			// 
			// ucDatePicker
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lblBack);
			this.Name = "ucDatePicker";
			this.Size = new System.Drawing.Size(99, 21);
			this.Resize += new System.EventHandler(this.ucDatePicker_Resize);
			this.Validating += new System.ComponentModel.CancelEventHandler(this.ucDatePicker_Validating);
			this.Enter += new System.EventHandler(this.ucDatePicker_Enter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleKeyDown);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private string mvar_Maximum;
		private string mvar_Minimum;


		public string ValuePrevious
		{
			get
			{
				if (txtDay.Text.Trim()!="" & txtMonth.Text.Trim()!=""  & txtYear.Text.Trim()!="")
				{
					DateTime dtValue = Convert.ToDateTime(txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text).AddDays(-1);
					return dtValue.Day.ToString() + "/" + dtValue.Month.ToString() + "/" + dtValue.Year.ToString();
				}
				else
					return "";
			}
		}

		
		public string ValueNext
		{
			get
			{
				if (txtDay.Text.Trim()!="" & txtMonth.Text.Trim()!=""  & txtYear.Text.Trim()!="")
				{
					DateTime dtValue = Convert.ToDateTime(txtDay.Text + "/" + txtMonth.Text + "/" + txtYear.Text).AddDays(1);
					return dtValue.Day.ToString() + "/" + dtValue.Month.ToString() + "/" + dtValue.Year.ToString();
				}
				else
					return "";
			}
		}

		public string Value
		{
			get
			{
				if (txtDay.Text.Trim()!="" & txtMonth.Text.Trim()!=""  & txtYear.Text.Trim()!="")
					return txtDay.Text.ToString() + "/" + txtMonth.Text.ToString() + "/" + txtYear.Text;
				else
					return "";
			}
			set
			{
				txtDay.Text = "";
				txtMonth.Text = "";
				txtYear.Text = "";

				if (value!="")
				{
					string[] arr = value.Split('/');
					txtDay.Text = arr[0];
					txtMonth.Text = arr[1];
					txtYear.Text = arr[2];
					dateTimePicker1.Value = Convert.ToDateTime(Value);
				}
				txtDay_Leave(txtDay, new EventArgs());
				txtMonth_Leave(txtMonth, new EventArgs());
				txtYear_Leave(txtYear, new EventArgs());		
			}
		}

		public string Year
		{
			get{return txtYear.Text;}
		}

		public string Month
		{
			get{return txtMonth.Text;}
		}

		public string Day
		{
			get{return txtDay.Text;}
		}
		
		public string Maximum
		{
			get{return mvar_Maximum;}

			set{mvar_Maximum = value;}
		}

		public string Minimum
		{
			get{return mvar_Minimum;}

			set{mvar_Minimum = value;}				
		}

		public string MaximumErrMsg;
		public string MinimumErrMsg;

		private bool bRequired = false;

		public bool Required
		{
			set{bRequired = value;}
			get{return bRequired;}
		}

		private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
		{
			txtDay.Text = dateTimePicker1.Value.Day.ToString();
			txtMonth.Text = dateTimePicker1.Value.Month.ToString();
			txtYear.Text = dateTimePicker1.Value.Year.ToString();

			txtDay_Leave(txtDay, new EventArgs());
			txtMonth_Leave(txtMonth, new EventArgs());
			txtYear_Leave(txtYear, new EventArgs());
		}

		private void HandleKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			TextBox txt = ((TextBox)sender);

			int keyascii = e.KeyChar;

			if(keyascii == Convert.ToInt32(Keys.Back))
			{
				if (txt.SelectionStart<1 & txt.Name !="txtDay")
				{
					System.Windows.Forms.SendKeys.Send("+{TAB}");
				}
			}
			else if(char.IsDigit(e.KeyChar))
			{
				if (txt.SelectionStart < txt.MaxLength & txt.SelectionStart < txt.Text.Length)
				{
					int nSS = txt.SelectionStart;
					txt.Text = txt.Text.Remove(txt.SelectionStart,1);
					txt.SelectionStart = nSS;
				}

				if (txt.SelectionStart== txt.MaxLength-1)
				{
					System.Windows.Forms.SendKeys.Send("{TAB}");
				}
			}
		}

		private void HandleKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			TextBox txt = ((TextBox)sender);

			if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				string PreviousValue = txt.Text;
				int iVal = 0;

				if (txt.Text !="") iVal = Convert.ToInt32(txt.Text);

				if(e.KeyCode == Keys.Up) iVal ++; else iVal --;

				txt.Text = iVal.ToString();
				if (txt.Name =="txtDay")
				{
					if (isValidDay() == false) txt.Text = PreviousValue;
					if (txtDay.Text.Length ==1) txtDay.Text = "0" + txtDay.Text;
				}
				else if (txt.Name =="txtMonth")
				{
					if (isValidMonth() == false) txt.Text = PreviousValue;
					if (txtMonth.Text.Length ==1) txtMonth.Text = "0" + txtMonth.Text;
				}
				else if (txt.Name =="txtYear")
				{
					if (iVal +1 <1980) txt.Text = PreviousValue;
				}
				e.Handled = true;
			}
			if (txt.Text.Trim()!="" && txt.SelectionLength == txt.Text.Length)
			{
				if (e.KeyCode == Keys.Right)
				{
					txt.SelectionStart = 0;
					txt.SelectionLength = 0;
				}
				else if (e.KeyCode == Keys.Left)
				{
					txt.SelectionStart = 0;
				}
			}
			else if (e.KeyCode == Keys.Right)
			{
				if (txt.SelectionStart == txt.Text.Length)
				{
					System.Windows.Forms.SendKeys.Send("{TAB}");
				}
			}
			else if (e.KeyCode == Keys.Left)
			{
				if (txt.SelectionStart == 0)
				{
					System.Windows.Forms.SendKeys.Send("+{TAB}");
				}
			}
			else if(e.KeyCode == Keys.Delete)
			{
				if (txt.Text =="" & txt.Name !="txtYear")
				{
					System.Windows.Forms.SendKeys.Send("{TAB}");
				}
			}
		}

		private void Invalid_Date(bool bValue)
		{
			if (bValue)
			{
				txtDay.ForeColor = Color.Red;
				txtMonth.ForeColor = Color.Red;
				txtYear.ForeColor = Color.Red;
			}
			else
			{
				txtDay.ForeColor = Color.Black;
				txtMonth.ForeColor = Color.Black;
				txtYear.ForeColor = Color.Black;

				if (this.Value!="")
					dateTimePicker1.Value = Convert.ToDateTime(this.Value);
			}
		}

		private void ucDatePicker_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				if (!((txtDay.Text.Trim()!="" & txtMonth.Text.Trim()!=""  & txtYear.Text.Trim()!="") | 
					(txtDay.Text.Trim()=="" & txtMonth.Text.Trim()==""  & txtYear.Text.Trim()=="")))
				{
					e.Cancel = true;
				}
				else if (txtDay.Text.Trim()!="" & txtMonth.Text.Trim()!=""  & txtYear.Text.Trim()!="")
				{
					if (!isValidMonth())
					{
						txtMonth.ForeColor = Color.Red;					
						e.Cancel = true;
						return;
					}
					else
					{					
						if (!isValidDay())
						{
							txtDay.ForeColor = Color.Red;
							e.Cancel = true;
							return;
						}
					}
				}

				Invalid_Date(e.Cancel);
//				CheckRequired();
			}
			catch (Exception err)
			{
				e.Cancel = true;
				MessageBox.Show(err.Message, "ucDatePicker", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CheckRequired()
		{
			if (Required & (txtDay.Text.Trim()=="" & txtMonth.Text.Trim()==""  & txtYear.Text.Trim()==""))
			{
				txtDay.BackColor = Color.Red;
				txtMonth.BackColor= Color.Red;
				txtYear.BackColor= Color.Red;
			}
		}

		private bool isValidDay()
		{
			if (txtDay.Text.Trim()!="")
			{
				if (isValidMonth() & txtYear.Text !="" &  txtMonth.Text !="")
				{
					int nDays =  DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text));			
					return Convert.ToInt32(txtDay.Text)<=nDays & Convert.ToInt32(txtDay.Text)>0;
				}
				else
					return Convert.ToInt32(txtDay.Text)<27  & Convert.ToInt32(txtDay.Text)>0;
			}
			else
				return true;
		}

		private bool isValidMonth()
		{
			if (txtMonth.Text.Trim()!="")
				return (!(Convert.ToInt32(txtMonth.Text)<=0 | Convert.ToInt32(txtMonth.Text)>12));
			else
				return true;
		}

		private void txtDay_Leave(object sender, System.EventArgs e)
		{			
			txtDay.ForeColor = (!isValidDay())? Color.Red:Color.Black;

			if (txtDay.Text.Length ==1) txtDay.Text = "0" + txtDay.Text;
		}

		private void txtMonth_Leave(object sender, System.EventArgs e)
		{
			txtMonth.ForeColor = ((!isValidMonth())? Color.Red : Color.Black);
			
			if (txtMonth.Text.Length ==1) txtMonth.Text = "0" + txtMonth.Text;
		}

		private void txtYear_Leave(object sender, System.EventArgs e)
		{
			if (txtYear.Text.Trim()!="")
				txtYear.Text = DateTime.Now.Year.ToString().Substring(0,(4-txtYear.Text.Length)) + txtYear.Text;
		}

		private void HandleEnter(object sender, System.EventArgs e)
		{
			//
		}

		private void ucDatePicker_Resize(object sender, System.EventArgs e)
		{
			this.Height=21;
			this.Width =99;
		}

		private void ucDatePicker_Enter(object sender, System.EventArgs e)
		{
//			txtDay.BackColor = Color.White;
//			txtMonth.BackColor= Color.White;
//			txtYear.BackColor= Color.White;
		}
	}
}