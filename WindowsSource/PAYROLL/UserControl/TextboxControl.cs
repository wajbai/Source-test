using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PAYROLL.UserControl
{
	public enum enumTextType
	{
		DecimalNumeral,
		Numeric,
		Alphabetic,
		AlphaNumeric,
		AlphaNumericSpecial
	}

	public class TextboxControl : System.Windows.Forms.TextBox 
	{
		private System.ComponentModel.Container components = null;

		public TextboxControl():base()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		private void InitializeComponent()
		{
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxControl_KeyPress);
			this.Enter += new System.EventHandler(this.TextboxControl_Enter);
			this.Validating += new System.ComponentModel.CancelEventHandler( this.TextboxControl_Validating);
		}

		private enumTextType m_enumTextEntryTpe=enumTextType.AlphaNumericSpecial;		
		private const int WM_PASTE =0x302;
		private const int WM_COPY =0x301;	

		private const int DECIMALNUMERAL=0;
		private const int NUMERIC=1;
		private const int ALPHABETIC=2;
		private const int ALPHANUMERIC=3;
		private const int ALPHANUMERICSPECIAL=4;

		private bool bRequired = false;

		public bool Required
		{
			set{bRequired = value;}
			get{return bRequired;}
		}

    	public enumTextType TextType
		{
			get
			{
				return this.m_enumTextEntryTpe;
			}
			set
			{
				this.m_enumTextEntryTpe=value;
				switch(value)
				{
					case enumTextType.Alphabetic:
						break;
				}
			}
		}
	
		private bool IsAlphabetic(string datastring)
		{
			int index ;
		
			for(index=0; index < datastring.Length; index++)
			{
				if(! char.IsDigit(datastring[index]))
				{
					return false;
				}
			}
			return true;
		}

		private bool IsAlphaNumeric(string datastring)
		{
			int index;

			for(index=0; index < datastring.Length ;index++)
			{
				if(! char.IsLetterOrDigit(datastring[index]))
				{
					return false;
				}
			}
			return true;
		}

		private bool IsNonDecimalNumeric(string datastring)
		{
			int index;

			for(index=0;index < datastring.Length;index++)
			{
				if(! char.IsNumber(datastring[index]))
				{
					return false;
				}
			}
			return true;
		}

		private bool IsNumeric(string datastring)
		{
			try
			{
				int.Parse(datastring);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private void TextboxControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			int keyascii=e.KeyChar;
			const int CTRL_CHAR=25;
			int BACKSPACE_CHAR= Convert.ToInt32(Keys.Back);

			if(keyascii ==BACKSPACE_CHAR || keyascii <= CTRL_CHAR)
			{
				e.Handled=false;
			}
			else
			{
				switch(m_enumTextEntryTpe)
				{
					case enumTextType.Alphabetic:
						if(! char.IsLetter(e.KeyChar))
						{
							e.Handled=true;
						}
						break;

					case enumTextType.AlphaNumeric:
						if(! char.IsLetterOrDigit(e.KeyChar)  && e.KeyChar != '_')
						{
							e.Handled=true;
						}
						break;

					case enumTextType.DecimalNumeral:
						if( e.KeyChar == '.')
						{
							//Searching for dot already exits 
							if( this.Text.IndexOf(".") > 0)
							{
								e.Handled = true;
							}
						}
						else if(e.KeyChar == '-')
						{
							//'Searching for - exists also see its start character
							if( this.SelectionStart != 0 || this.Text.IndexOf("-") > 0)
							{
								e.Handled = true;
							}
						}
						else if( ! char.IsNumber(e.KeyChar) )
						{
							e.Handled = true;
						}						
						break;

					case enumTextType.Numeric:
						if(e.KeyChar == '-') 
						{
							//Searching for - exists also see its start character
							if( this.SelectionStart != 0 || this.Text.IndexOf(".") > 0)
							{
								e.Handled = true;
							}
						}
						else if(! char.IsNumber(e.KeyChar))
						{
							e.Handled = true;
						}
						break;
					default:
						break;
				}
			}			
		}

		protected override void WndProc(ref Message m)
		{
			string datastring;

			//Make sure its the own window message
			if(m.HWnd.Equals(this.Handle))
			{
				switch(m.Msg)
				{
					case WM_PASTE:
						if (Clipboard.GetDataObject().GetDataPresent(typeof(string)))
						{
							datastring=(string) Clipboard.GetDataObject().GetData(typeof(string));
							if (datastring != null)
							{
								switch(m_enumTextEntryTpe)
								{
									case enumTextType.Alphabetic:
										if( IsAlphabetic(datastring))
										{
											base.WndProc(ref m);
										}
										break;

									case enumTextType.AlphaNumeric:
										if(IsAlphaNumeric(datastring))
										{
											base.WndProc(ref m);																		 
										}
										break;
							
									case enumTextType.Numeric:
										if (IsNonDecimalNumeric(datastring))
										{
											base.WndProc(ref m);
										}
										break;

									case enumTextType.DecimalNumeral:
										if( IsNumeric(datastring))
										{
											base.WndProc(ref m);
										}
										break;

									default:
										base.WndProc(ref m);
										break;
								}
							}
						}
						break;
					default:
						base.WndProc(ref m);
						break;
				}
			}
			else
			{
				base.WndProc (ref m);
			}
		}

		public override string Text
		{
			get
			{
				return base.Text.Replace("'","''");
			}
			set
			{
				base.Text = value.Replace("''","'");
			}
		}

		private void TextboxControl_Enter(object sender, System.EventArgs e)
		{
			this.SelectAll();
		}

		private void TextboxControl_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
		}
	}
}