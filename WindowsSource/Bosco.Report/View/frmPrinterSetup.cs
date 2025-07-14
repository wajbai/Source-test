using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Printing;
using Bosco.Report.Base;
using Bosco.Utility;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Native;
using DevExpress.XtraReports.UI;

namespace Bosco.Report.View
{
    public partial class frmPrinterSetup : Bosco.Utility.Base.frmBase
    {
        ReportViewer Rptviewer = null;
        ReportBase activeReport = null;
        Int32 CurrentPage = -1;
        public frmPrinterSetup(ReportViewer rptviewer, ReportBase ReportforPrint, int currentpage)
        {
            InitializeComponent();
            Rptviewer = rptviewer; //On 13/06/2022
            CurrentPage = currentpage;
            activeReport = ReportforPrint;

            chkAllPages.Checked = true;
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcPagestext.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblExamplePages.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.Height = this.layoutControl1.Height + this.layoutControl1.Top + 5;
        }

        private void frmPrinterSetup_Load(object sender, EventArgs e)
        {
            //Load list of printer names
            cbPrinterNames.Properties.Items.Clear();
            try
            {
                string defaultprintername = string.Empty;
                PrinterSettings printersetting = new PrinterSettings();
                foreach (string printername in PrinterSettings.InstalledPrinters)
                {
                    printersetting.PrinterName = printername;
                    if (printersetting.IsDefaultPrinter)
                    {
                        defaultprintername = printername;
                    }
                    cbPrinterNames.Properties.Items.Add(printername);
                }
                cbPrinterNames.SelectedItem = defaultprintername;
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Problem in loading list of Printers " + err.Message);        
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
              
        private void chkOddEven_CheckedChanged(object sender, EventArgs e)
        {
            lcOddevencombo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (chkOddEven.Checked)
            {
                lcOddevencombo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                if (cboOddEven.SelectedIndex <= 0)
                {
                    cboOddEven.SelectedIndex = 0;
                }
            }
        }

        private void chkPages_CheckedChanged(object sender, EventArgs e)
        {
            lcPagestext.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblExamplePages.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (chkPages.Checked)
            {
                lcPagestext.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblExamplePages.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                txtPages.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (activeReport != null)
                {
                    if (!string.IsNullOrEmpty(cbPrinterNames.Text))
                    {
                        //On 13/06/2022, refresh before split it pages
                        Rptviewer.LoadReport(activeReport.ReportId, true);

                        ReportBase rptPrint = new ReportBase();
                        rptPrint = GetPrintPages();

                        //send to selected printer
                        using (ReportPrintTool printTool = new ReportPrintTool(rptPrint))
                        {
                            printTool.Print(cbPrinterNames.SelectedText);
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageRender.ShowMessage("Select Printer");
                    }
                }
                else
                {
                    MessageRender.ShowMessage("Report is empty");
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage("Problem in printing " + err.Message);
            }
        }

        /// <summary>
        /// This method is used to 
        /// </summary>
        /// <returns></returns>
        private ReportBase GetPrintPages()
        {
            //0: Print Only Odd pages
            //1: Print Only Even pages
            bool isOddPages  = (cboOddEven.SelectedIndex == 0 ? true : false);
            
            ReportBase rptODDEVEN = new ReportBase();
            rptODDEVEN.CreateDocument();
            rptODDEVEN.PrintingSystem.ContinuousPageNumbering = false;

            if (chkOddEven.Checked)
            {
                foreach (Page p in activeReport.Pages)
                {
                    int pageindex = p.Index + 1;

                    if (isOddPages)
                    {
                        if ((pageindex % 2) != 0)
                        {
                            rptODDEVEN.Pages.Add(p);
                        }
                    }
                    else
                    {
                        if ((pageindex % 2) == 0)
                        {
                            rptODDEVEN.Pages.Add(p);
                        }
                    }
                }
            }
            else if (chkCurrentPage.Checked && CurrentPage >= 0)
            {
                rptODDEVEN.Pages.Add(activeReport.Pages[CurrentPage]);
            }
            else
            {
                rptODDEVEN = activeReport;
            }
            return rptODDEVEN;
        }
    }
}