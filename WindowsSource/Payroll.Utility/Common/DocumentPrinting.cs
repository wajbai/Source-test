using System;
using System.Collections;
//using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace Payroll.Utility.Common
{
	public class DocumentPrinting
	{
		private StringReader streamToPrint;
		private Font printFont;
		private Font normalFont;
		private Font boldFont;
		private Font boldLargeFont;

		public DocumentPrinting()
		{
			normalFont = new Font("Courier New", 10, FontStyle.Regular);
			boldFont = new Font("Courier New", 10, FontStyle.Bold);
			boldLargeFont = new Font("Courier New", 12, FontStyle.Bold);
			printFont = normalFont;
		}

		public void PrintStream(string printerName, string stringToPrint)
		{
			try
			{
				streamToPrint = new StringReader(stringToPrint);

				try
				{
					PrintDocument pd = new PrintDocument();
					pd.PrinterSettings.PrinterName = printerName;
					pd.PrintPage += new PrintPageEventHandler(this.Document_PrintPage);
					pd.Print();
				}
				finally
				{
					streamToPrint.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// The PrintPage event is raised for each page to be printed.
		private void Document_PrintPage(object sender, PrintPageEventArgs ev)
		{
			float linesPerPage = 0;
			float yPos = 0;
			float xPos = 0;
			int count = 0;
			float leftMargin = ev.MarginBounds.Left;
			float topMargin = ev.MarginBounds.Top;
			float fontHeight = printFont.GetHeight(ev.Graphics);
			float fontWidth = printFont.SizeInPoints;
			string lineText = null;
			PrintLineCollection printLineCollection;

			// Calculate the number of lines per page.
			linesPerPage = ev.MarginBounds.Height / fontHeight;

			// Print each line of the file.
			while (count < linesPerPage &&
				((lineText = streamToPrint.ReadLine()) != null))
			{
				xPos = leftMargin;
				printLineCollection = ParseText(lineText);

				foreach (PrintLineCollection pcColl in printLineCollection)
				{
					fontHeight = pcColl.PrintFont.GetHeight(ev.Graphics);
					fontWidth = pcColl.PrintFont.SizeInPoints;
					yPos = topMargin + (count * fontHeight);

					PrintLine(pcColl.PrintText, pcColl.PrintFont, xPos, yPos, ev);
					xPos += (pcColl.PrintText.Length * fontWidth) - 5;
				}

				count++;
				printLineCollection.Clear();
			}

			// If more lines exist, print another page.
			if (lineText != null)
				ev.HasMorePages = true;
			else
				ev.HasMorePages = false;
		}

		private void PrintLine(string stringToPrint, Font printFont, float xPos, float yPos, PrintPageEventArgs ev)
		{
			ev.Graphics.DrawString(stringToPrint, printFont, Brushes.Black, xPos, yPos, new StringFormat());
		}

		private PrintLineCollection ParseText(string lineText)
		{
			PrintLineCollection plcoll = new PrintLineCollection();

			string textItem = ClearCharacter(lineText);
			int posFromNext = 0;
			int posFrom = 0;
			int posTo = 0;
			Font textFont = normalFont;
            
			while (posFrom != -1)
			{
				textFont = normalFont;
                
				//Bold
				posFrom = lineText.IndexOf((char)15 + "" + (char)14, posFromNext);

				if (posFrom >= 0)
				{
					textFont = boldFont;
				}
				else //Title Bold
				{
					posFrom = lineText.IndexOf((char)14, posFromNext);
					if (posFrom >= 0)
					{
						textFont = boldLargeFont;
					}
				}

				if (posFrom >= 0)
				{
					//text precedes to font delimiter 
					if (posFrom > 0)
					{
						textItem = lineText.Substring(posFromNext, (posFrom - posFromNext));
						textItem = ClearCharacter(textItem);
						plcoll.Add(normalFont, textItem);
					}

					//text between font delimiter 
					posTo = lineText.IndexOf((char)18, posFrom);

					if (posTo > posFrom)
					{
						textItem = lineText.Substring(posFrom, ((posTo - posFrom) + 1));
						textItem = ClearCharacter(textItem);
						plcoll.Add(textFont, textItem);
					}
				}
				else
				{
					//text next to font delimiter 
					if (posFromNext > 0)
					{
						textItem = lineText.Substring(posFromNext);
						textItem = ClearCharacter(textItem);
						plcoll.Add(normalFont, textItem);
					}
				}
                
				posFromNext = (posTo + 1);
			}

			if (plcoll.Count == 0)
			{
				plcoll.Add(normalFont, textItem);
			}

			return plcoll;
		}

		private string ClearCharacter(string textItem)
		{
			textItem = textItem.Replace((char)15 + "", "").Replace((char)14 + "", "").Replace((char)18 + "", "");
			return textItem;
		}
	}


	public class PrintLineCollection
	{
		private ArrayList printLineCollections = new ArrayList();
		private Font printFont;
		private string printText;
        
        

		public Font PrintFont
		{
			get { return printFont; }
		}

		public string PrintText
		{
			get { return printText; }
		}

		// Add Exception into the Collection
		public void Add(Font printFont, string printText)
		{
			PrintLineCollection pc = new PrintLineCollection();
			pc.printFont = printFont;
			pc.printText = printText;
			printLineCollections.Add(pc);
		}

		public PrintLineCollection this[int index]
		{
			get { return (PrintLineCollection)printLineCollections[index]; }
		}

		public PrintLineCollection GetItem(int index)
		{
			return (PrintLineCollection)printLineCollections[index];
		}

		public void Clear()
		{
			printLineCollections.Clear();
		}

		public int Count
		{
			get { return printLineCollections.Count; }
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return printLineCollections.GetEnumerator();
		}
		#endregion
	}
}
