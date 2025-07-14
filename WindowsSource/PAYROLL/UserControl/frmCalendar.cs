using System;
using System.Windows.Forms;
using Bosco.Utility.Common;
using Bosco.Utility;


namespace PAYROLL.UserControl
{
	public class frmCalendar : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MonthCalendar monthCalendar1;
		private System.ComponentModel.Container components = null;
        clsGeneral clsGeneral = new clsGeneral();
		public frmCalendar()
		{
			InitializeComponent();
            monthCalendar1.SetDate(Convert.ToDateTime(clsGeneral.GetMySQLDateTime(DateTime.Now.ToString(), DateDataType.Date)));
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
			this.SuspendLayout();
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Location = new System.Drawing.Point(0, 0);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 0;
			this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
			// 
			// frmCalendar
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(188)), ((System.Byte)(201)), ((System.Byte)(203)));
			this.ClientSize = new System.Drawing.Size(178, 152);
			this.Controls.Add(this.monthCalendar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCalendar";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Calendar";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalendar_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		private string strDate = "";
		private string strDateTime = "";

		public string getSelectedDate()
		{
			return strDate;
		}
		public string getSelectedDateTime()
		{
			return strDateTime;
		}

		private void monthCalendar1_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			if (e.Start.Day.ToString().Length == 1)
				strDate += "0" + e.Start.Day + "/";
			else
				strDate += e.Start.Day + "/";

			if (e.Start.Month.ToString().Length == 1)
				strDate += "0" + e.Start.Month + "/" + e.Start.Year;
			else
				strDate += e.Start.Month + "/" + e.Start.Year;
			strDateTime=strDate +" " + e.Start.ToShortTimeString();
//			//Modified by	: PE
//			//Date			: 05-02-2007
//			//Purpose		: To facilitate the long date format
//			strDate = strDate + " " + e.Start.ToShortTimeString();
			this.Close();
		}

		private void frmCalendar_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Escape)
				this.Close();
			if (e.KeyCode == System.Windows.Forms.Keys.Enter)
				this.monthCalendar1_DateSelected(sender, new DateRangeEventArgs(monthCalendar1.SelectionStart ,monthCalendar1.SelectionEnd));
		}
		
	}
}
