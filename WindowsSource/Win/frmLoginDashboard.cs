using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;

using Bosco.Model.UIModel;
using Bosco.Utility;
using ACPP.Modules.Master;
using ACPP.Modules.Transaction;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Business;
using DevExpress.XtraCharts;
using DevExpress.Utils.Frames;
using Bosco.Model.Setting;
using DevExpress.XtraCharts.Printing;
using DevExpress.Utils;
using AcMEDSync.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.Dsync;
using System.Net;
using System.Diagnostics;
using System.ServiceModel;
using ACPP.Modules.Data_Utility;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using DevExpress.Data;
using Bosco.Utility.ConfigSetting;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace ACPP
{
    public partial class frmLoginDashboard : frmFinanceBase
    {
        #region Declaration
        Timer timer = new Timer();
        ResultArgs resultArgs = null;
        private string CashTransMode = "";
        private string BankTransMode = "";
        private string FDTransMode = "";
        public string RecentVoucherDate = "";
        public string ProjectName = "";
        private bool IsDashboardLoaded = false;
        public bool IsSelected = false;
        SimpleEncrypt.SimpleEncDec objSimpleEncrypt = new SimpleEncrypt.SimpleEncDec();

        public const string SHOWALLCAPTION = "Show All";
        public const string SHOWLATESTCAPTION = "Latest";

        #endregion

        #region PostTicket variables
        int pTickID = 0;
        int pRepTickID = 0;
        string pTickDate = string.Empty;
        string pSubject = string.Empty;
        string pDescription = string.Empty;
        int pPriority = 0;
        int pPostedBy = 0;
        string pUserName = string.Empty;
        #endregion

        #region Constructor
        public frmLoginDashboard()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int projectId = 0;
        private int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }

        private DateTime projectStartDate;
        private DateTime ProjectStartDate
        {
            get { return projectStartDate; }
            set { projectStartDate = value; }
        }

        private int brs = 0;
        private int BRS
        {
            get { return brs; }
            set { brs = value; }
        }
        private DataTable Fdmature = null;
        private DataTable dtBankAlerts = null;

        private DateTime dtyearfrom
        {
            get { return UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false); }
        }
        private DateTime dtbookbeginfrom
        {
            get { return UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false); }
        }
        private DateTime dtRecentVoucherDate
        {
            get { return UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false); }
        }
        private DateTime dtYearTo
        {
            get { return UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false); }
        }
        #endregion

        #region Events

        private void frmLoginHome_Load(object sender, EventArgs e)
        {
            SetVisibileShortCuts(true, true, true);
            this.ShowHideLeftMenuBar = true;
            this.ShowHideDockPanel = DockVisibility.Visible;
            this.ShowRibbonHomePage = true;
            rpcpeDetailBalance.AllowFocused = false;

            //if (this.LoginUser.LoginUserId != "1")
            //{
            //    if (CommonMethod.ApplyUserRightsForTransaction((int)DashBoard.DashBoard) == 0)
            //    {
            //      //  this.Close();
            //    }
            //}

            //On 29/03/2018, IF enable port is disabled, hide upload voucher status, portal messages and amendmeents 
            if (this.AppSetting.EnablePortal == 0)
            {
                lcgAmendmeents.Visibility = LayoutVisibility.Never;
                lcgPortalmessges.Visibility = LayoutVisibility.Never;
                lcgUploadVoucher.Visibility = LayoutVisibility.Never;

                lblFooterRefreshDate.Visibility = LayoutVisibility.Never;
                lcbtnRefersh.Visibility = LayoutVisibility.Never;
                UcTickets.Dock = DockStyle.Fill;
                LayoutItemDragController dragController = new LayoutItemDragController(lcgTickets, lcgUploadVoucher, MoveType.Inside, InsertLocation.Before, LayoutType.Horizontal);
                lcgTickets.Move(dragController);
            }

            //On 18/10/2024, To Hide Ticket and Show user manual/feature
            lcTroubleTickets.Visibility = LayoutVisibility.Never;
            
            //peAddTicket.Visible = false;
            lcUsermanualFeature.Visibility = LayoutVisibility.Always;
            
            LoadUsermanualFeature();
            
        }

        private void LoadViewTypes()
        {
            string[] types = Enum.GetNames(typeof(ChartViewType));
            foreach (string name in types)
            {
                if (name.ToUpper() != ChartViewType.None.ToString().ToUpper())
                {
                    cmbChartType.Properties.Items.Add(name);
                }
            }
            cmbChartType.SelectedIndex = 0;
        }

        private void lstProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedProject = 0;
            if (lstProject.SelectedValue != null)
            { SelectedProject = this.UtilityMember.NumberSet.ToInteger(lstProject.SelectedValue.ToString()); }

            if (SelectedProject > 0)
            {
                Cursor.Current = Cursors.WaitCursor;

                ProjectName = lstProject.GetItemText(lstProject.SelectedIndex);
                ProjectId = SelectedProject;

                //On 09/02/2022, To get Project Properties --------
                using (ProjectSystem proSys = new ProjectSystem(ProjectId))
                {
                    projectStartDate = proSys.StartedOn;
                }
                //-------------------------------------------------

                if (!IsSelected)
                {
                    LoadDashBoardDefaults();
                    //LoadPortalMessages();
                }


                //On 11/12/2019, To Set Recent Project and Voucher date info if project selection is disabled--------------------------------
                if (string.IsNullOrEmpty(this.AppSetting.ProjSelection) || this.AppSetting.ProjSelection == "0")
                {
                    if (this.ParentForm != null)
                    {
                        (this.ParentForm as frmMain).RecentProject = ProjectName;
                        (this.ParentForm as frmMain).RecentProjectId = ProjectId;
                        (this.ParentForm as frmMain).RecentVoucherDate = this.UtilityMember.DateSet.ToDate(lblDateTime.Text);
                    }
                }
                //-----------------------------------------------------------------------------------------------------------------------------

                Cursor.Current = Cursors.Default;
            }
        }

        private void lstProject_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            if (lstProject.SelectedIndex == e.Index)
            {
                e.Appearance.Font = new Font(lstProject.Font, FontStyle.Bold);
            }
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbChartType.SelectedItem.ToString()))
            {
                chtRecPay.Series[0].ChangeView((ViewType)Enum.Parse(typeof(ViewType), cmbChartType.SelectedItem.ToString()));
                chtRecPay.Series[1].ChangeView((ViewType)Enum.Parse(typeof(ViewType), cmbChartType.SelectedItem.ToString()));

                if (cmbChartType.SelectedItem.ToString() == ChartViewType.Pie.ToString() ||
                    cmbChartType.SelectedItem.ToString() == ChartViewType.Pie3D.ToString())
                {
                    PiePointOptions rec = chtRecPay.Series[0].Label.PointOptions as PiePointOptions;
                    PiePointOptions pay = chtRecPay.Series[1].Label.PointOptions as PiePointOptions;
                    if (rec != null && pay != null)
                    {
                        rec.PercentOptions.ValueAsPercent = pay.PercentOptions.ValueAsPercent = false;
                    }
                    chtRecPay.Series[0].ShowInLegend = false;
                    chtRecPay.Series[0].LabelsVisibility = chtRecPay.Series[1].LabelsVisibility = DefaultBoolean.True;
                    chtRecPay.Series[0].ToolTipEnabled = chtRecPay.Series[0].ToolTipEnabled = DefaultBoolean.True;
                    //chtRecPay.Series[1].ToolTipEnabled = chtRecPay.Series[1].ToolTipEnabled = DefaultBoolean.True;
                    chtRecPay.Titles[0].Visible = false;
                }
                else
                {
                    chtRecPay.Series[0].LabelsVisibility = chtRecPay.Series[1].LabelsVisibility = DefaultBoolean.False;
                    chtRecPay.Series[0].ToolTipEnabled = chtRecPay.Series[0].ToolTipEnabled = DefaultBoolean.False;
                    //chtRecPay.Series[1].ToolTipEnabled = chtRecPay.Series[1].ToolTipEnabled = DefaultBoolean.False;
                    chtRecPay.Series[0].ShowInLegend = true;
                    chtRecPay.Titles[0].Visible = false;
                }
            }
        }

        private void perintChart_Click(object sender, EventArgs e)
        {
            if (chtRecPay != null)
            {
                string leftColumn = string.Empty;//Pages: [Page # of Pages #]";
                string middleColumn = this.GetMessage(MessageCatalog.Master.DashBoard.DASH_REC_PAY_CAP) + " " + ShowInTransYear();
                string rightColumn = string.Empty; //"Date: [Date Printed]";

                chtRecPay.OptionsPrint.SizeMode = PrintSizeMode.Zoom;
                //chtRecPay.ShowPrintPreview();
                //chtRecPay.ShowRibbonPrintPreview();

                PrintingSystem printsystem = new PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink();
                compositeLink.PrintingSystem = printsystem;
                PrintableComponentLink link = new PrintableComponentLink();

                // Create a PageHeaderFooter object and initializing it with
                // the link's PageHeaderFooter.
                PageHeaderFooter phf = compositeLink.PageHeaderFooter as PageHeaderFooter;
                phf.Header.Content.Clear();

                // Add custom information to the link's header.
                phf.Header.Content.AddRange(new string[] { leftColumn, middleColumn, rightColumn });
                phf.Header.Font = new System.Drawing.Font(phf.Header.Font.FontFamily, 14, FontStyle.Bold);
                phf.Header.LineAlignment = BrickAlignment.Far;

                link.Component = chtRecPay;
                compositeLink.Links.Add(link);
                link = new PrintableComponentLink();
                link.Component = gcReceiptPayment;
                compositeLink.Links.Add(link);
                compositeLink.CreateDocument();
                compositeLink.ShowPreview();



            }
        }
        private void peRefresh_Click(object sender, EventArgs e)
        {
            IsSelected = true;
            LoadDashBoardDefaults();
            LoadPortalMessages();
            IsSelected = false;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.R))
            {
                IsSelected = true;
                LoadDashBoardDefaults();
                IsSelected = false;
            }
            if (KeyData == (Keys.Alt | Keys.P))
            {
                if (chtRecPay != null)
                {
                    chtRecPay.OptionsPrint.SizeMode = PrintSizeMode.Zoom;
                    chtRecPay.ShowRibbonPrintPreview();
                }
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
        #endregion

        #region Methods

        public void LoadDashBoardDefaults()
        {
            SetTitle();
            LoadDate();
            if (IsSelected)
            {
                LoadProjects();
            }
            CheckList();
            LoadSumaryData();
            LoadBRS();
            LoadFD();
            AlertCB();
            CheckAlerts();
            LoadChartInfo();
            AssignProjectId();
            this.TransacationPeriod();
            RefreshOverAllBalance();
            IsDashboardLoaded = true;
        }

        private void RefreshOverAllBalance()
        {
            DateTime baldate = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false); //(!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
            using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
            {
                string lastdate = vsystem.FetchLastVoucherDate();
                if (!string.IsNullOrEmpty(lastdate))
                {
                    baldate = this.UtilityMember.DateSet.ToDate(lastdate, false);
                }
            }

            ucTransviewClosingBalance1.ProjectId = 0;
            ucTransviewClosingBalance1.ClosingDateFrom = baldate.ToShortDateString();
            ucTransviewClosingBalance1.ClosingDateTo = baldate.ToShortDateString();
            ucTransviewClosingBalance1.BankClosedDate = baldate.ToShortDateString();
            ucTransviewClosingBalance1.GetClosingBalance();
            ucTransviewClosingBalance1.ShowClosingBalNegative();
            ucTransviewClosingBalance1.TitleCaption = "Over All Balance as on " + baldate.ToShortDateString();
            lcOverAllBalance.Text = "";
        }

        public void CheckList()
        {
            if (lstProject.ItemCount == 1)
            {
                ProjectName = lstProject.GetItemText(lstProject.SelectedIndex);
                ProjectId = this.UtilityMember.NumberSet.ToInteger(lstProject.SelectedValue.ToString());
            }
        }

        public void LoadDate()
        {
            // System.DateTime currentDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : (dtbookbeginfrom > dtyearfrom) ? dtbookbeginfrom : dtyearfrom;
            lblDateTime.Text = this.UtilityMember.DateSet.ToDate(ApplyRecentPrjectDetails(ProjectId).ToString(), false).ToString("D");
        }

        private string ApplyRecentPrjectDetails(int proid)
        {
            string Rdate = string.Empty;
            try
            {
                DateTime dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                DateTime dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                using (AccouingPeriodSystem accountingSystem = new AccouingPeriodSystem())
                {
                    accountingSystem.YearFrom = this.AppSetting.YearFrom;
                    accountingSystem.YearTo = this.AppSetting.YearTo;
                    resultArgs = accountingSystem.FetchRecentVoucherDate(proid);
                    if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            if (!string.IsNullOrEmpty(dr[accountingSystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString()))
                            {
                                Rdate = dr[accountingSystem.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString();
                            }
                        }
                    }
                    else
                    {
                        Rdate = (dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom).ToString();

                        //On 09/02/2022, To show proper balance date. If date is less than project date, show project start date---------------
                        Rdate = (projectStartDate > this.UtilityMember.DateSet.ToDate(Rdate, false) ? this.UtilityMember.DateSet.ToDate(projectStartDate.ToShortDateString(), false).ToShortDateString() : Rdate);
                        //---------------------------------------------------------------------------------------------------------------------
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return Rdate;
        }


        private void SetTitle()
        {
            ucProjectCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_PROJECT_TITLE);

            //On 25/10/2021, To change proper text
            //ucReceiptCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_RECEIPT_PAYMENT_TITLE) + " " + ShowInTransYear();
            //ucReceiptCap.Caption = "Receipts and Payments for the month " + ShowInTransYear(); 
            ucReceiptCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASH_REC_PAY_CAP) + " " + ShowInTransYear();

            // ucBRSCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BANK_RECONCILIATION_TITLE) + " " + ShowInMonth();
            //ucSummaryCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BALANCE_TITLE) + " " + ShowInDate();
            //ucCashCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CASH_TITLE);
            //ucBankCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BANK_TITLE);
            //ucFDCap.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_TITLE);
            ucReceiptCap.CaptionSize = 9;
            ucProjectCap.CaptionSize = 9;

            ucPortalMessages.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_PORTALMESSAGE_TITLE);
            ucPortalMessages.CaptionSize = 9;

            ucAmedments.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_LICENSE_TITLE);
            ucLicense.CaptionSize = 9;

            ucLicense.Caption = this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_AMEDMENTS_TITLE);
            ucAmedments.CaptionSize = 9;

            //UcTickets.Caption = "Ticket Status";
            //UcTickets.Caption = this.GetMessage(MessageCatalog.Master.LoginDashBoard.TICKET_STATUS_CAPTION);
            UcTickets.Caption = "User Manuals / Feature";
            UcTickets.CaptionSize = 9;

            //  ucBRSCap.CaptionSize = 9;
            //ucSummaryCap.CaptionSize = 9;
            //ucCashCap.CaptionSize = 8;
            //ucBankCap.CaptionSize = 8;
            //ucFDCap.CaptionSize = 8;
            //ucCashBalance.CaptionSize = 8;
            //ucBankBalance.CaptionSize = 8;
            //ucFDBalance.CaptionSize = 8;
        }

        public string ShowInMonth()
        {
            System.DateTime currentDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
            return currentDate.ToString("MMM") + " " + currentDate.ToString(currentDate.Year.ToString());
        }

        public string ShowInDate()
        {
            System.DateTime currentDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
            return currentDate.ToShortDateString();
        }

        public string ShowInTransYear()
        {
            //On 25/10/2021, To change proper text
            //return dtyearfrom.ToString("MMM") + " " + dtyearfrom.ToString(dtyearfrom.Year.ToString()) + " - " + dtYearTo.ToString("MMM") + " " + dtYearTo.ToString(dtYearTo.Year.ToString());
            return dtyearfrom.ToString("MMM") + " " + dtyearfrom.ToString(dtyearfrom.Year.ToString()) + " to " + dtYearTo.ToString("MMM") + " " + dtYearTo.ToString(dtYearTo.Year.ToString());
        }

        public void LoadSummary()
        {
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                //BalanceProperty CashBalanceProperty = balanceSystem.GetCashBalance(ProjectId, null, BalanceSystem.BalanceType.CurrentBalance);
                //ucCashBalance.Caption = this.UtilityMember.NumberSet.ToCurrency(CashBalanceProperty.Amount) + " " + CashBalanceProperty.TransMode;
                //CashTransMode = CashBalanceProperty.TransMode;
                //BalanceProperty bankBalanceProperty = balanceSystem.GetBankBalance(ProjectId, null, BalanceSystem.BalanceType.CurrentBalance);
                //ucBankBalance.Caption = this.UtilityMember.NumberSet.ToCurrency(bankBalanceProperty.Amount) + " " + bankBalanceProperty.TransMode;
                //BankTransMode = bankBalanceProperty.TransMode;
                //BalanceProperty FDBalanceProperty = balanceSystem.GetFDBalance(ProjectId, null, BalanceSystem.BalanceType.CurrentBalance);
                //ucFDBalance.Caption = this.UtilityMember.NumberSet.ToCurrency(FDBalanceProperty.Amount) + " " + FDBalanceProperty.TransMode;
                //FDTransMode = FDBalanceProperty.TransMode;

                //lblCashBalance.AppearanceItemCaption.ForeColor = (CashBalanceProperty.TransMode == "CR") ? Color.Red : Color.Green;
                //lblBankBalance.AppearanceItemCaption.ForeColor = (bankBalanceProperty.TransMode == "CR") ? Color.Red : Color.Green;
                //lblFDBalance.AppearanceItemCaption.ForeColor = (FDBalanceProperty.TransMode == "CR") ? Color.Red : Color.Green;

            }
        }
        public void LoadSumaryData()
        {
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                balanceSystem.BankClosedDate = (string.IsNullOrEmpty(lblDateTime.Text) ? string.Empty : this.UtilityMember.DateSet.ToDate(lblDateTime.Text));
                BalanceProperty CashBalanceProperty = balanceSystem.GetCashBalance(ProjectId, dtYearTo.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                //CashAmount = this.UtilityMember.NumberSet.ToCurrency(CashBalanceProperty.Amount) + " " + CashBalanceProperty.TransMode;
                CashTransMode = CashBalanceProperty.Amount != 0 || CashBalanceProperty.Amount < 0 ? CashBalanceProperty.TransMode : TransactionMode.DR.ToString();

                BalanceProperty bankBalanceProperty = balanceSystem.GetBankBalance(ProjectId, dtYearTo.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                //BankAmount = this.UtilityMember.NumberSet.ToCurrency(bankBalanceProperty.Amount) + " " + bankBalanceProperty.TransMode;
                BankTransMode = bankBalanceProperty.Amount != 0 || CashBalanceProperty.Amount < 0 ? bankBalanceProperty.TransMode : TransactionMode.DR.ToString();

                BalanceProperty FDBalanceProperty = balanceSystem.GetFDBalance(ProjectId, dtYearTo.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                //FDAmount = this.UtilityMember.NumberSet.ToCurrency(FDBalanceProperty.Amount) + " " + FDBalanceProperty.TransMode;
                FDTransMode = FDBalanceProperty.Amount != 0 || FDBalanceProperty.Amount < 0 ? FDBalanceProperty.TransMode : TransactionMode.DR.ToString();


                DataTable dtsummary = new DataTable();
                dtsummary.Columns.Add("Ledger", typeof(string));
                dtsummary.Columns.Add("Amount", typeof(double));
                dtsummary.Columns.Add("TransMode", typeof(string));

                dtsummary.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CASH_TITLE), CashBalanceProperty.Amount, CashTransMode);
                dtsummary.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BANK_TITLE), bankBalanceProperty.Amount, BankTransMode);
                dtsummary.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_TITLE), FDBalanceProperty.Amount, FDTransMode);

                gcBalance.DataSource = dtsummary;

                string BalanceType = this.AppSetting.CreditBalance;
                string ProjectIds = string.Empty;
                using (DashBoardSystem dashBoardSystem = new DashBoardSystem())
                {
                    switch (BalanceType)
                    {
                        case "1":
                            ProjectIds = ProjectId.ToString();
                            break;
                        case "2":
                            ProjectIds = dashBoardSystem.FetchProjectsbySociety(ProjectId);
                            break;
                        case "3":
                            ProjectIds = dashBoardSystem.FetchAllProjectId();
                            break;
                    }
                }

                CashBalanceProperty = balanceSystem.GetCashBalance(ProjectIds, dtYearTo.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                CashTransMode = CashBalanceProperty.Amount != 0 || CashBalanceProperty.Amount < 0 ? CashBalanceProperty.TransMode : TransactionMode.DR.ToString();

                bankBalanceProperty = balanceSystem.GetBankBalance(ProjectIds, dtYearTo.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                BankTransMode = bankBalanceProperty.Amount != 0 || CashBalanceProperty.Amount < 0 ? bankBalanceProperty.TransMode : TransactionMode.DR.ToString();

                FDBalanceProperty = balanceSystem.GetFDBalance(ProjectIds, dtYearTo.ToString(), BalanceSystem.BalanceType.ClosingBalance);
                FDTransMode = FDBalanceProperty.Amount != 0 || FDBalanceProperty.Amount < 0 ? FDBalanceProperty.TransMode : TransactionMode.DR.ToString();

                //if (CashBalanceProperty.TransMode =="CR")
                //{
                //    colAmount.AppearanceCell.ForeColor = Color.Red;
                //}
                //if (bankBalanceProperty.TransMode =="CR")
                //{
                //    colAmount.AppearanceCell.ForeColor = Color.Red;
                //}
                //if (FDBalanceProperty.TransMode =="CR")
                //{
                //    colAmount.AppearanceCell.ForeColor = Color.Red;
                //}
            }
        }
        public void LoadBRS()
        {
            try
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    resultArgs = voucherSystem.FetchBRSDetails(this.UtilityMember.NumberSet.ToInteger(projectId.ToString()), this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false));
                    if (resultArgs.Success)
                    {
                        DataView dv = new DataView();
                        dv = resultArgs.DataSource.Table.DefaultView;
                        dv.RowFilter = "MATERIALIZED_ON is null";
                        brs = dv.Count;
                        // gcBRS.DataSource = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public void LoadReceipts(DataTable dtReceipt)
        {
            chtRecPay.Series[0].DataSource = dtReceipt;
            chtRecPay.Series[0].ArgumentDataMember = "MONTH_NAME";
            chtRecPay.Series[0].ValueDataMembers.AddRange(new string[] { "RECEIPT" });
        }

        public void LoadPayments(DataTable dtPayment)
        {
            chtRecPay.Series[1].DataSource = dtPayment;
            chtRecPay.Series[1].ArgumentDataMember = "MONTH_NAME";
            chtRecPay.Series[1].ValueDataMembers.AddRange(new string[] { "PAYMENT" });
        }

        public void LoadChartInfo()
        {
            try
            {

                using (DashBoardSystem Dashboardsystem = new DashBoardSystem())
                {
                    DateTime dtFrom = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
                    DateTime dtTo = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                    resultArgs = Dashboardsystem.FetchCharts(projectId, dtFrom, dtTo);
                    DataTable dtChart = resultArgs.DataSource.Table;
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        //DataColumn dcolRec = new DataColumn("RECEIPT1", typeof(string));
                        //DataColumn dcolPay = new DataColumn("PAYMENT1", typeof(string));
                        //dtChart.Columns.Add(dcolRec);
                        //dtChart.Columns.Add(dcolPay);
                        //foreach (DataRow dr in dtChart.Rows)
                        //{
                        //    double rec = this.UtilityMember.NumberSet.ToDouble(dr["RECEIPT"].ToString());
                        //    double pay = this.UtilityMember.NumberSet.ToDouble(dr["PAYMENT"].ToString());


                        //    dr["RECEIPT1"] = this.UtilityMember.NumberSet.ToCurrency(rec);
                        //    dr["PAYMENT1"] = this.UtilityMember.NumberSet.ToCurrency(pay);
                        //}
                        //dtChart.Rows[3].Delete();
                        //dtChart.Rows[4].Delete();

                        gcReceiptPayment.DataSource = dtChart;
                        LoadReceipts(dtChart);
                        LoadPayments(dtChart);
                    }
                    else
                    {
                        gcReceiptPayment.DataSource = null;
                        chtRecPay.Series[0].DataSource = chtRecPay.Series[1].DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        public void LoadFD()
        {
            try
            {
                using (DashBoardSystem Dashboardsystem = new DashBoardSystem())
                {

                    resultArgs = Dashboardsystem.FetchFD(projectId, DateTime.Now.Date);//, (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom);
                    DataTable dtChart = resultArgs.DataSource.Table;
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        Fdmature = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        Fdmature = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        public void LoadProjects()
        {
            try
            {
                using (DashBoardSystem Dashboardsystem = new DashBoardSystem())
                {
                    Dashboardsystem.ProjectClosedDate = this.AppSetting.YearFrom;
                    resultArgs = Dashboardsystem.FetchProjects();
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        lstProject.ValueMember = Dashboardsystem.AppSchema.Project.PROJECT_IDColumn.ColumnName;
                        lstProject.DisplayMember = Dashboardsystem.AppSchema.Project.PROJECTColumn.ColumnName;
                        lstProject.DataSource = resultArgs.DataSource.Table;

                        lstProject.SelectedValue = this.AppSetting.UserProjectId != string.Empty ? this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) : this.UtilityMember.NumberSet.ToInteger(lstProject.SelectedValue.ToString());
                        //if (IsSelected)
                        //{
                        //    ProjectId = this.UtilityMember.NumberSet.ToInteger(lstProject.SelectedValue.ToString());
                        //}
                        ProjectId = this.AppSetting.UserProjectId != string.Empty ? this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId) : this.UtilityMember.NumberSet.ToInteger(lstProject.SelectedValue.ToString()); //this.UtilityMember.NumberSet.ToInteger(this.AppSetting.UserProjectId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {

            }
        }

        public void CheckAlerts()
        {
            DataTable dtAlert = new DataTable();
            dtAlert.Columns.Add("CAPTION", typeof(string));
            dtAlert.Columns.Add("MESSAGE", typeof(string));
            dtAlert.Columns.Add("TYPE", typeof(string));

            DateTime dtMatured = DateTime.Now;

            if (CashTransMode == TransactionMode.CR.ToString() && FDTransMode == TransactionMode.CR.ToString())
            {
                //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_CASHBANK), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CASH_BANK_FD_TITLE));
                dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_CASHBANK), NavigationType.CashBank);
            }
            if (CashTransMode == TransactionMode.CR.ToString())
            {
                //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_CASH), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CASH_TITLE));
                dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_CASH), NavigationType.Cash);
            }
            if (dtBankAlerts != null)
            {
                foreach (DataRow drBank in dtBankAlerts.Rows)
                {
                    string BankName = drBank["LEDGER_NAME"].ToString();
                    //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_BANK) + " :-" + BankName, this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BANK_TITLE));
                    dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_BANK) + " (" + dtBankAlerts.Rows.Count + ")", NavigationType.Bank);
                }
            }
            if (FDTransMode == TransactionMode.CR.ToString())
            {
                //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_FD), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_TITLE));
                dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_NEGATIVE_BALANCE_TITLE), this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NEGATIVE_BALANCE_FD), NavigationType.Cash);
            }
            else
            {
                if (brs == 1)
                {
                    //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BRS_TITLE), BRS + " " + this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CHEQUE_HAS_TITLE), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BRS_TITLE));
                    dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BRS_TITLE), BRS + " " + this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CHEQUE_HAS_TITLE), NavigationType.BRS);
                }
                else if (brs > 1)
                {
                    //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BRS_TITLE), BRS + " " + this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CHEQUE_HAVE_TITLE), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BRS_TITLE));
                    dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_BRS_TITLE), BRS + " " + this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_CHEQUE_HAVE_TITLE), NavigationType.BRS);
                }
                //dtAlert.Rows.Add("AcME ERP", "Welcome To AcME ERP", "AcME ERP");
            }
            if (Fdmature != null)
            {
                foreach (DataRow drFD in Fdmature.Rows)
                {
                    dtMatured = this.UtilityMember.DateSet.ToDate(drFD["MATURED_ON"].ToString(), false);
                    //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FIXED_DEPOSIT_TITLE + " "), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_ACCOUNT_TITLE + " ") + " " + drFD["FD_ACCOUNT_NUMBER"].ToString() + " " + this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_MATURES_ON_TITLE + " ") + dtMatured.ToShortDateString(), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_TITLE));
                    //dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FIXED_DEPOSIT_TITLE) + " ", this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_ACCOUNT_TITLE) + " " + drFD["FD_ACCOUNT_NUMBER"].ToString() + " " + this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_MATURES_ON_TITLE) + " " + dtMatured.ToShortDateString(), this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_TITLE));
                    dtAlert.Rows.Add(this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FIXED_DEPOSIT_TITLE) + " ", this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_FD_TITLE) + " - " + drFD["FD_ACCOUNT_NUMBER"].ToString() + " " + this.GetMessage(MessageCatalog.Master.DashBoard.DASHBOARD_MATURES_ON_TITLE) + " " + dtMatured.ToString("dd-MMM-yy"), NavigationType.FD);
                }
            }


            //On 09/05/2020, to autobackuptoportal based on settings
            //On 13/06/2016, show last uploadDb to portal
            //if ((this.AppSetting.IS_SDB_CONGREGATION || this.AppSetting.IS_SCCGSB_CONGREGATION || this.AppSetting.IS_BOSCOS_DEMO) && (!String.IsNullOrEmpty(this.AppSetting.DBUploadedOn)))
            if ((AppSetting.AutomaticDBBackupToPortal == 1) && (!String.IsNullOrEmpty(this.AppSetting.DBUploadedOn)))
            {
                dtAlert.Rows.Add("Upload Database", "Database uploaded on : " + this.AppSetting.DBUploadedOn, "");
            }

            //On 26/11/2024 - To attach Last db restored date
            if (!String.IsNullOrEmpty(this.AppSetting.DBRestoredOn))
            {
                dtAlert.Rows.Add("Restoed Database", "Database Restored on : " + this.AppSetting.DBRestoredOn, "");
            }

            ShowAlert(dtAlert);
            if (AppSetting.IS_SDB_INM)
            {
                emptySpaceItem5.Visibility = lciLastSync.Visibility = LayoutVisibility.Always;

                ShowLastSyncDateAlert();
            }
        }

        private DataTable AlertCB()
        {
            try
            {
                using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                {
                    voucherSystem.ProjectId = ProjectId;
                    voucherSystem.GroupId = ((int)FixedLedgerGroup.BankAccounts);
                    voucherSystem.BalanceDate = dtYearTo;
                    resultArgs = voucherSystem.FetchTransNegativeClosingBalance();
                    if (resultArgs.Success)
                    {
                        dtBankAlerts = resultArgs.DataSource.Table;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
            return dtBankAlerts;
        }

        public void ShowAlert(DataTable dtAlert)
        {
            try
            {
                string Caption = string.Empty;
                string Message = string.Empty;
                string Type = string.Empty;
                if (dtAlert != null && dtAlert.Rows.Count > 0)
                {
                    //this.tileControl1.ItemSize = this.tileControl1.Width - 100;
                    tltItem.Frames.Clear();
                    tltItem.Visible = true;
                    foreach (DataRow dr in dtAlert.Rows)
                    {
                        Caption = dr["CAPTION"].ToString();
                        Message = dr["MESSAGE"].ToString();
                        Type = dr["TYPE"].ToString();
                        tltItem.Frames.Add(GetAlertEmptyTileItem(Caption, Message, Type));
                    }
                    //tileControl1.ItemSize = new TileControlViewInfoAce(tileControl1).ItemSize;
                }
                else
                {
                    tltItem.Frames.Clear();
                    tltItem.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private DevExpress.XtraEditors.TileItemFrame GetAlertEmptyTileItem(string caption, string message, string Type)
        {

            DevExpress.XtraEditors.TileItemFrame tileFrame = new DevExpress.XtraEditors.TileItemFrame();
            DevExpress.XtraEditors.TileItemElement tileMessage = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileCaption = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileType = new DevExpress.XtraEditors.TileItemElement();

            tileCaption.Text = caption;
            tileMessage.Text = message;
            tileType.Text = Type;

            // Add TileItem to the Frame
            tileFrame.Elements.Add(tileCaption);
            tileFrame.Elements.Add(tileMessage);
            tileFrame.Elements.Add(tileType);
            //Set Properties for Tile Frame
            tileFrame.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollTop;
            tileType.Appearance.Normal.Options.UseFont = true;
            // Set Properties for Caption
            //tileCaption.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            //tileCaption.UseTextInTransition = true;
            //tileCaption.Appearance.Normal.ForeColor = Color.Blue;
            //tileCaption.Appearance.Normal.Font = new System.Drawing.Font("", 11F);  // new System.Drawing.Font("", 15F, System.Drawing.FontStyle.Bold);
            // Set Properties for Message
            tileCaption.Appearance.Normal.ForeColor = tileType.Appearance.Normal.ForeColor = Color.Thistle;
            tileCaption.Appearance.Normal.Options.UseFont = true;

            tileMessage.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileMessage.UseTextInTransition = true;
            tileMessage.Appearance.Normal.ForeColor = Color.DarkBlue;
            tileMessage.Appearance.Normal.Font = new System.Drawing.Font("", 10F);
            tileMessage.Appearance.Normal.Options.UseFont = true;

            //tileMessage.Appearance.Normal.TextOptions.WordWrap = WordWrap.Wrap;
            tileMessage.MaxWidth = this.tileControl1.Width;
            return tileFrame;
        }

        private void ShowLastSyncDateAlert()
        {
            string Caption = string.Empty;
            string Message = string.Empty;
            string CombinedMsg = string.Empty;
            using (Bosco.Model.Transaction.VoucherTransactionSystem vouchersystem = new Bosco.Model.Transaction.VoucherTransactionSystem())
            {
                resultArgs = vouchersystem.GetlastSyncDate();

                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        Caption = dr["PROJECT"].ToString();
                        Message = dr["VOUCHER_DATE"].ToString();
                        CombinedMsg = Caption + " - " + Message;
                        tileItemShowLastSync.Frames.Add(GetAlertEmptyTileItemNew(" ", CombinedMsg, " "));
                    }
                }
            }
        }

        private DevExpress.XtraEditors.TileItemFrame GetAlertEmptyTileItemNew(string caption, string message, string Type)
        {

            DevExpress.XtraEditors.TileItemFrame tileFrame = new DevExpress.XtraEditors.TileItemFrame();
            DevExpress.XtraEditors.TileItemElement tileMessage = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileCaption = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileType = new DevExpress.XtraEditors.TileItemElement();

            tileCaption.Text = caption;
            tileMessage.Text = message;
            tileType.Text = Type;

            // Add TileItem to the Frame
            tileFrame.Elements.Add(tileCaption);
            tileFrame.Elements.Add(tileMessage);
            tileFrame.Elements.Add(tileType);
            //Set Properties for Tile Frame
            tileFrame.Animation = DevExpress.XtraEditors.TileItemContentAnimationType.ScrollTop;
            tileType.Appearance.Normal.Options.UseFont = true;
            // Set Properties for Caption
            //tileCaption.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            //tileCaption.UseTextInTransition = true;
            //tileCaption.Appearance.Normal.ForeColor = Color.Blue;
            //tileCaption.Appearance.Normal.Font = new System.Drawing.Font("", 11F);  // new System.Drawing.Font("", 15F, System.Drawing.FontStyle.Bold);
            // Set Properties for Message
            tileCaption.Appearance.Normal.ForeColor = tileType.Appearance.Normal.ForeColor = Color.Thistle;
            tileCaption.Appearance.Normal.Options.UseFont = true;

            tileMessage.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileMessage.UseTextInTransition = true;
            tileMessage.Appearance.Normal.ForeColor = Color.DarkBlue;
            tileMessage.Appearance.Normal.Font = new System.Drawing.Font("", 8F);
            tileMessage.Appearance.Normal.Options.UseFont = true;

            tileItemShowLastSync.ItemSize = new TileControlViewInfoAcen(tileItemShowLastSync).Item.ItemSize;

            //tileMessage.Appearance.Normal.TextOptions.WordWrap = WordWrap.Wrap;
            //tileMessage.MaxWidth = this.tlShowLastSync.Width;
            return tileFrame;
        }



        private void AssignProjectId()
        {
            if (!string.IsNullOrEmpty(this.AppSetting.YearFrom) && !string.IsNullOrEmpty(this.AppSetting.YearTo) && !string.IsNullOrEmpty(this.AppSetting.BookBeginFrom) && !string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PROJECT_ID");
                dt.Columns.Add("PROJECT");
                dt.Columns.Add("VOUCHER_DATE");
                DataRow dr = dt.NewRow();
                dr["PROJECT_ID"] = ProjectId;
                dr["PROJECT"] = ProjectName;
                //On 03/02/2017, ReAssign last voucher date for selected Project
                //dr["VOUCHER_DATE"] = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
                dr["VOUCHER_DATE"] = ApplyRecentPrjectDetails(ProjectId);
                dt.Rows.Add(dr);
                this.AppSetting.UserProjectInfor = dt.DefaultView;
            }
        }

        #endregion

        #region TransBalance Methods
        public ResultArgs FetchTransFDClosingBalance(int GroupId, FixedLedgerGroup TransType)
        {
            using (VoucherTransactionSystem voucherTransSystem = new VoucherTransactionSystem())
            {
                voucherTransSystem.ProjectId = projectId;
                voucherTransSystem.GroupId = GroupId;
                voucherTransSystem.BalanceDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom; ;

                //On 07/04/2021 **********************************************************************************
                //To have common date for closing balance In (Closing Total and detail)
                if (string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate))
                {
                    if (!string.IsNullOrEmpty(lblDateTime.Text))
                    {
                        voucherTransSystem.BalanceDate = this.UtilityMember.DateSet.ToDate(lblDateTime.Text, false);
                    }
                }
                //************************************************************************************************
                colBaseLedgerName.Visible = false;
                gvTransBalance.GroupFormat = "";

                switch (TransType)
                {
                    case FixedLedgerGroup.Cash:
                        {
                            colLedgerName.Caption = "Cash Ledger";
                            resultArgs = voucherTransSystem.FetchTransClosingBalance();
                            break;
                        }
                    case FixedLedgerGroup.BankAccounts:
                        {
                            colLedgerName.Caption = "Bank Ledger";
                            voucherTransSystem.BankClosedDate = voucherTransSystem.BalanceDate.ToString();
                            resultArgs = voucherTransSystem.FetchTransClosingBalance();
                            break;
                        }
                    case FixedLedgerGroup.FixedDeposit:
                        {
                            colLedgerName.Caption = "FD Ledger";
                            resultArgs = voucherTransSystem.FetchTransFDClosingBalance();
                            voucherTransSystem.GroupId = (int)FixedLedgerGroup.FixedDeposit;
                            
                            //On 08/05/2024, To show FD Ledger name
                            colBaseLedgerName.Visible = true;
                            if (resultArgs.Success && resultArgs.DataSource.Table != null)
                            {
                                string sumBalance = "AMOUNT * IIF(TRANSMODE='CR', -1, 1)";
                                resultArgs.DataSource.Table.Columns.Add("BALANCE", typeof(System.Double), sumBalance);

                                gvTransBalance.GroupFormat = "{1}  :  {2}";
                                gvTransBalance.OptionsBehavior.AutoExpandAllGroups = true;
                            }

                            break;
                        }
                }
                //On 11/10/2024, To Skip default ledgers for multi currency or other than country
                using (MappingSystem mappingsystem = new MappingSystem())
                {
                    resultArgs = mappingsystem.EnforceSkipDefaultLedgers(resultArgs, "ID");
                }

                gcTransBalance.DataSource = null;
                gcTransBalance.DataSource = resultArgs.DataSource.Table;
                gcTransBalance.RefreshDataSource();
                colCurrency.Visible = (this.AppSetting.AllowMultiCurrency == 1);
            }
            return resultArgs;
        }
        #endregion

        private void rbtnBalance_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void rpcpeDetailBalance_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //string LedgerName = gvBalance.GetFocusedRowCellValue(colDeafultLedgers) != null ? gvBalance.GetFocusedRowCellValue(colDeafultLedgers).ToString() : string.Empty;
            //FixedLedgerGroup fixedLedgerGroup = LedgerName.Equals("Cash") ? FixedLedgerGroup.Cash : LedgerName.Equals("Bank") ? FixedLedgerGroup.BankAccounts : FixedLedgerGroup.FixedDeposit;
            //FetchTransFDClosingBalance((int)fixedLedgerGroup, fixedLedgerGroup);
        }

        //private void ShowDetailBalance()
        //{
        //string LedgerName = gvBalance.GetFocusedRowCellValue(colDeafultLedgers) != null ? gvBalance.GetFocusedRowCellValue(colDeafultLedgers).ToString() : string.Empty;
        //    //Modules.UIControls.ucTransBalance ucTransactionBalance = new Modules.UIControls.ucTransBalance();
        //    rpcpeDetailBalance.PopupControl = pceTransBalance;
        //    ucTransactionBalance.ProjectId = lstProject.SelectedValue != null ? this.UtilityMember.NumberSet.ToInteger(lstProject.SelectedValue.ToString()) : 0;
        //    FixedLedgerGroup fixedLedgerGroup = LedgerName.Equals("Cash") ? FixedLedgerGroup.Cash : LedgerName.Equals("Bank") ? FixedLedgerGroup.BankAccounts : FixedLedgerGroup.FixedDeposit;
        //    ucTransactionBalance.dteRecentDate = (!string.IsNullOrEmpty(this.AppSetting.RecentVoucherDate)) ? this.UtilityMember.DateSet.ToDate(this.AppSetting.RecentVoucherDate, false) : dtbookbeginfrom > dtyearfrom ? dtbookbeginfrom : dtyearfrom;
        //    ucTransactionBalance.FetchTransFDClosingBalance((int)fixedLedgerGroup, fixedLedgerGroup);
        //}

        private void rpcpeDetailBalance_Click(object sender, EventArgs e)
        {
            string LedgerName = gvBalance.GetFocusedRowCellValue(colDeafultLedgers) != null ? gvBalance.GetFocusedRowCellValue(colDeafultLedgers).ToString() : string.Empty;
            FixedLedgerGroup fixedLedgerGroup = LedgerName.Equals(CashFlag.Cash.ToString()) ? FixedLedgerGroup.Cash : LedgerName.Equals(CashFlag.Bank.ToString()) ? FixedLedgerGroup.BankAccounts : FixedLedgerGroup.FixedDeposit;
            FetchTransFDClosingBalance((int)fixedLedgerGroup, fixedLedgerGroup);
        }

        private void tltItem_ItemClick(object sender, TileItemEventArgs e)
        {
            //string FrameType = string.Empty;
            //FrameType = e.Item.CurrentFrame.Elements[2].Text;
            //if (NavigationType.Cash.ToString().Equals(FrameType) || NavigationType.Bank.ToString().Equals(FrameType.Trim()) || NavigationType.CashBank.ToString().Equals(FrameType))
            //{
            //    frmTransactionView Transactionview = new frmTransactionView(this.AppSetting.RecentVoucherDate, ProjectId, ProjectName, 0, 1);
            //    Transactionview.MdiParent = this.ParentForm;
            //    Transactionview.Show();
            //}
            //else if (NavigationType.BRS.ToString().Equals(FrameType.Trim()))
            //{
            //    frmBRS BRS = new frmBRS();
            //    BRS.ShowDialog();

            //}
            //else if (NavigationType.FD.ToString().Equals(FrameType))
            //{
            //    frmRenewalView RenewalView = new frmRenewalView(FDTypes.WD);
            //    RenewalView.MdiParent = this.ParentForm;
            //    RenewalView.Show();
            //}
        }
        private void gvBalance_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colAmount)
            {
                var TMode = gvBalance.GetRowCellValue(e.RowHandle, colTransMode);
                if (TMode.ToString() == "CR" && e.Column == colAmount)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.DarkGreen;
                }
            }
        }

        private void btnGetMessage_Click(object sender, EventArgs e)
        {
            GetPortalMessages();
        }

        public void GetPortalMessages()
        {
            try
            {
                DataSet dsMessages = new DataSet();
                this.Cursor = Cursors.WaitCursor;
                DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                if (IsServiceAlive(dataClient.Endpoint.Address.ToString()))
                {
                    dsMessages = dataClient.GetHeadOfficeMessages(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                    if (dsMessages != null && dsMessages.Tables.Count > 0)
                    {
                        using (PortalMessagesSystem portalMesssagesSystem = new PortalMessagesSystem())
                        {
                            resultArgs = portalMesssagesSystem.SavePortalMessage(dsMessages);
                            this.Cursor = Cursors.Default;
                            if (resultArgs != null && resultArgs.Success)
                            {
                                //this.ShowMessageBox(this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_DATASYN_MESSAGE_SAVED));
                                LoadPortalMessages();
                            }
                            else
                            {
                                //this.ShowMessageBox(this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_NO_RECORD));
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_NO_RECORD));
                    }

                    //On 25/10/2024, To Get User Manuals and Paid Featuer details
                    using (PortalMessagesSystem portalMesssagesSystem = new PortalMessagesSystem())
                    {
                        this.Cursor = Cursors.WaitCursor;
                        resultArgs = portalMesssagesSystem.FetchUserMaualsPaidFeaturesFromPortal();
                        if (resultArgs.Success)
                        {
                            LoadUsermanualFeature();
                        }
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_SERVICE_NOT_AVIALABLE));
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                MessageRender.ShowMessage(ex.Detail.Message.ToString());
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// On 26/06/2019, Get portal messages from portal and update it local
        /// 
        /// On 05/07/ 2019, For Background Process
        /// </summary>
        public ResultArgs UpdateMessagesFromPortalByBackgroundProcess()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                DataSet dsMessages = new DataSet();
                DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                if (IsServiceAlive(dataClient.Endpoint.Address.ToString()))
                {
                    dsMessages = dataClient.GetHeadOfficeMessages(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode);
                    if (dsMessages != null && dsMessages.Tables.Count > 0)
                    {
                        using (PortalMessagesSystem portalMesssagesSystem = new PortalMessagesSystem())
                        {
                            resultArgs = portalMesssagesSystem.SavePortalMessageByBackgroundProcess(dsMessages); //portalMesssagesSystem.SavePortalMessage(dsMessages);
                        }
                    }
                    else
                    {
                        AcMELog.WriteLog("Background Process:" + this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_NO_RECORD));
                    }
                }
                else
                {
                    AcMELog.WriteLog("Background Process:" + this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_SERVICE_NOT_AVIALABLE));
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                AcMELog.WriteLog("Background Process:" + ex.Detail.ToString());
            }
            finally
            {

            }
            return resultArgs;
        }

        //public bool IsServiceAlive(string URL)
        //{
        //    try
        //    {
        //        using (var client = new WebClient())
        //        using (var stream = client.OpenRead(URL))
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public void LoadPortalMessages()
        {
            DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
            if (IsServiceAlive(dataClient.Endpoint.Address.ToString()))
            {
                LoadMessages();
                LoadAmendments();
                LoadBroadcatmessages();

                //On 18/10/2024, To lock tickets and show user manuals and featues
                //LoadTickets();
            }
        }

        public ResultArgs LoadMessages(string ToggleOption = "")
        {
            try
            {
                gcPopupMessages.DataSource = null;
                using (PortalMessagesSystem portalMessagesSystem = new PortalMessagesSystem())
                {
                    resultArgs = portalMessagesSystem.FetchPortalDataSynMesseage();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ToggleOption) && ToggleOption == SHOWALLCAPTION)
                        {
                            gcPopupMessages.DataSource = resultArgs.DataSource.Table;
                        }
                        else // To show only latest 4 records if empty and the test is Latest
                        {
                            var tempLatest = resultArgs.DataSource.Table.AsEnumerable().Take(4);
                            if (tempLatest != null && tempLatest.Count() > 0)
                            {
                                gcPopupMessages.DataSource = tempLatest.CopyToDataTable();
                            }
                        }

                        lblFooterRefreshDate.Text =
                       (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0][portalMessagesSystem.AppSchema.PortalMessage.REFRESH_DATEColumn.ColumnName].ToString()) ?
                      "Last Refreshed on " + this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][portalMessagesSystem.AppSchema.PortalMessage.REFRESH_DATEColumn.ColumnName].ToString(), false).ToString("dd-MM-yyyy") :
                       "Portal Messages are not refreshed.");
                        gcPopupMessages.UseEmbeddedNavigator = true;
                    }
                    else
                    {
                        gcPopupMessages.DataSource = null;
                        lblFooterRefreshDate.Text = "Portal Messages are not refreshed.";
                        gcPopupMessages.UseEmbeddedNavigator = false;
                    }
                }
            }
            catch (Exception ep)
            {
                MessageRender.ShowMessage(ep.Message);
            }
            return resultArgs;
        }

        public ResultArgs LoadBroadcatmessages()
        {
            try
            {
                gcPortalMessages.DataSource = null;
                using (PortalMessagesSystem portalMessagesSystem = new PortalMessagesSystem())
                {
                    resultArgs = portalMessagesSystem.FetchPortalBroadcastMesseage();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcPortalMessages.DataSource = resultArgs.DataSource.Table;
                        gcPortalMessages.UseEmbeddedNavigator = true;
                    }
                    else
                    {
                        gcPortalMessages.DataSource = null;
                        gcPortalMessages.UseEmbeddedNavigator = false;
                    }
                }
            }
            catch (Exception ep)
            {
                MessageRender.ShowMessage(ep.Message);
            }
            return resultArgs;
        }

        public ResultArgs LoadAmendments()
        {
            try
            {
                gcAmendments.DataSource = null;
                using (PortalMessagesSystem portalMessagesSystem = new PortalMessagesSystem())
                {
                    //   portalMessagesSystem.ProjecID = ProjectId;
                    resultArgs = portalMessagesSystem.FetchPortalAmendmentMesseage();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcAmendments.DataSource = resultArgs.DataSource.Table;
                        gcAmendments.UseEmbeddedNavigator = true;
                    }
                    else
                    {
                        gcAmendments.DataSource = null;
                        gcAmendments.UseEmbeddedNavigator = false;
                    }
                }
            }
            catch (Exception ec)
            {
                MessageRender.ShowMessage(ec.Message);
            }
            return resultArgs;
        }

        public ResultArgs LoadTickets()
        {
            //try
            //{
            //    gcTroubleTickets.DataSource = null;
            //    using (PortalMessagesSystem portalMessagesSystem = new PortalMessagesSystem())
            //    {
            //        resultArgs = portalMessagesSystem.FetchPortalTickets();
            //        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            //        {
            //            DataTable dtOriginalTickets = new DataTable();
            //            DataTable dtTickets = new DataTable();
            //            DataTable dtRepliedTicket = new DataTable();
            //            DataSet dsTickets = new DataSet();

            //            dtOriginalTickets = resultArgs.DataSource.Table;
            //            DataView dvTic = dtOriginalTickets.AsDataView();
            //            if (dvTic != null && dvTic.ToTable().Rows.Count > 0)
            //            {
            //                dvTic.RowFilter = "REPLIED_TICKET_ID=0";
            //                dtTickets = dvTic.ToTable();
            //                if (dtTickets != null && dtTickets.Rows.Count > 0)
            //                {
            //                    dtTickets.TableName = "Tickets";
            //                    dsTickets.Tables.Add(dtTickets);
            //                }
            //                dvTic.RowFilter = string.Empty;
            //                dvTic.RowFilter = "REPLIED_TICKET_ID<>0";
            //                dtRepliedTicket = dvTic.ToTable();
            //                if (dtRepliedTicket != null && dtRepliedTicket.Rows.Count > 0)
            //                {
            //                    dtRepliedTicket.TableName = "Rep_Tickets";
            //                    dsTickets.Tables.Add(dtRepliedTicket);
            //                }
            //                if (dsTickets != null && dsTickets.Tables.Count > 1)
            //                {
            //                  //  dsTickets.Relations.Add("Replied Tickets", dsTickets.Tables[0].Columns["TICKET_ID"], dsTickets.Tables[1].Columns["REPLIED_TICKET_ID"]);  // REPLIED_TICKET_ID
            //                    gcTroubleTickets.DataSource = dsTickets.Tables[0];
            //                    gcTroubleTickets.DataMember = "Tickets";
            //                    gcTroubleTickets.UseEmbeddedNavigator = true;
            //                }
            //                else
            //                {
            //                    gcTroubleTickets.DataSource = null;
            //                    gcTroubleTickets.UseEmbeddedNavigator = false;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            gcTroubleTickets.DataSource = null;
            //            gcTroubleTickets.UseEmbeddedNavigator = false;
            //        }
            //    }
            //}
            //catch (Exception ec)
            //{
            //    MessageRender.ShowMessage(ec.Message);
            //}
            //return resultArgs;

            try
            {
                gcTroubleTickets.DataSource = null;
                using (PortalMessagesSystem portalMessagesSystem = new PortalMessagesSystem())
                {
                    resultArgs = portalMessagesSystem.FetchPortalTickets();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        gcTroubleTickets.DataSource = resultArgs.DataSource.Table;
                        gcTroubleTickets.UseEmbeddedNavigator = true;
                    }
                    else
                    {
                        gcTroubleTickets.DataSource = null;
                        gcTroubleTickets.UseEmbeddedNavigator = false;
                    }
                }
            }
            catch (Exception ec)
            {
                MessageRender.ShowMessage(ec.Message);
            }
            return resultArgs;
        }

        public ResultArgs LoadUsermanualFeature()
        {
            try
            {
                using (PortalMessagesSystem portalMessagesSystem = new PortalMessagesSystem())
                {
                    DataSet dsUsermanualPaidFeatures = new DataSet();
                    string path = Path.Combine(SettingProperty.ApplicationStartUpPath, "UserManualAndPaidFeatures.xml");
                    if (File.Exists(path))
                    {
                        dsUsermanualPaidFeatures.ReadXml(path);
                    }
                    resultArgs = portalMessagesSystem.FetchUserManualFeature();
                    gcUsermanualFeatures.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtUserManuals = resultArgs.DataSource.Table;
                        string fitler = "(" + portalMessagesSystem.AppSchema.UsermanualFeature.FEATURE_GROUP_CODEColumn.ColumnName + " = 'PF' OR " +
                                            portalMessagesSystem.AppSchema.UsermanualFeature.FEATURE_CODEColumn.ColumnName + " = 'UM001')";
                        dtUserManuals.DefaultView.RowFilter = fitler;
                        gcUsermanualFeatures.DataSource = dtUserManuals.DefaultView.ToTable();
                        colShowUserManuals.Width = 26;
                        colShowUserManuals.OptionsColumn.FixedWidth = true;

                        fitler = portalMessagesSystem.AppSchema.UsermanualFeature.FEATURE_GROUPColumn.ColumnName + " = 'User Manual'";
                        dtUserManuals.DefaultView.RowFilter = fitler;
                        gcUserManuals.DataSource = dtUserManuals.DefaultView.ToTable();
                        colDownlaodUserManual.Width = 26;
                        colDownlaodUserManual.OptionsColumn.FixedWidth = true;

                        //this.gvUsermanualFeatures.OptionsView.ShowHorizontalLines = DefaultBoolean.False;
                        //this.gvUsermanualFeatures.OptionsView.ShowVerticalLines = DefaultBoolean.False;
                    }
                }
            }
            catch (Exception ec)
            {
                MessageRender.ShowMessage(ec.Message);
            }
            return resultArgs;
        }

        private void btnMessageRefresh_Click(object sender, EventArgs e)
        {
            GetPortalMessages();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            flpMessages.HidePopup();
        }
        private void btnAmedmentClose_Click(object sender, EventArgs e)
        {
            flpAmendments.HidePopup();
        }

        private void gvPopupMessages_Click(object sender, EventArgs e)
        {
            // LoadUploadVoucherPopup();
        }

        private void LoadUploadVoucherPopup()
        {
            DataTable dtMessage = (DataTable)gcPopupMessages.DataSource;
            if (dtMessage != null && dtMessage.Rows.Count > 0)
            {
                simpleLabelItem2.Visibility = simpleLabelItem7.Visibility = simpleLabelItem9.Visibility =
                    lblPopupStatus.Visibility = lblMessDateFrom.Visibility = lblTransDateTo.Visibility = LayoutVisibility.Always;

                string remarks = gvPopupMessages.GetFocusedRowCellValue(colMessremarks) != null ? gvPopupMessages.GetFocusedRowCellValue(colMessremarks).ToString() : " ";
                
                using (ImportVoucherSystem importsystem = new ImportVoucherSystem())
                {
                    string rtn = importsystem.GetProjectVoucherDetailsByDataSycErrorMessage(this.AppSetting.HeadofficeCode, this.AppSetting.BranchOfficeCode, remarks);
                    if (!string.IsNullOrEmpty(rtn))
                    {
                        remarks = rtn;
                    }
                }
                memMessageremarks.Text = remarks;
                
                lblPopupStatus.Text = gvPopupMessages.GetFocusedRowCellValue(colMessStatus) != null ? gvPopupMessages.GetFocusedRowCellValue(colMessStatus).ToString() : " ";

                lblPopupUpDate.Text = gvPopupMessages.GetFocusedRowCellValue(ColMessUploadedOn) != null ?
                    UtilityMember.DateSet.ToDate(gvPopupMessages.GetFocusedRowCellValue(ColMessUploadedOn).ToString(),false).ToShortDateString() : " ";

                lblMessDateFrom.Text = gvPopupMessages.GetFocusedRowCellValue(colMessTransDateFrom) != null &&
                   !string.IsNullOrEmpty(gvPopupMessages.GetFocusedRowCellValue(colMessTransDateFrom).ToString()) ?
                this.UtilityMember.DateSet.ToDate(gvPopupMessages.GetFocusedRowCellValue(colMessTransDateFrom).ToString(), false).ToString("dd-MM-yyyy") : " ";

                lblTransDateTo.Text = gvPopupMessages.GetFocusedRowCellValue(colTransDateTo) != null &&
                   !string.IsNullOrEmpty(gvPopupMessages.GetFocusedRowCellValue(colTransDateTo).ToString()) ?
                this.UtilityMember.DateSet.ToDate(gvPopupMessages.GetFocusedRowCellValue(colTransDateTo).ToString(), false).ToString("dd-MM-yyyy") : " ";

                lblPopupTitle.Text = "Upload Voucher Status";
                simpleLabelItem4.Text = "Uploaded On";
                simpleLabelItem10.Text = "Remarks";
                simpleLabelItem7.Text = "Date From ";// "Started On";
                simpleLabelItem9.Text = "Date To"; // "Completed On";
                lciReply.Visibility = lciEditTicket.Visibility = LayoutVisibility.Never;
                if (!string.IsNullOrEmpty(lblPopupStatus.Text) && lblPopupStatus.Text.Equals("Closed") || lblPopupStatus.Text.Equals("CLOSED"))
                {
                    memMessageremarks.ForeColor = Color.DarkGreen;
                }
                else
                {
                    memMessageremarks.ForeColor = Color.Red;
                }

                flpMessages.Height = layoutControlItem3.Height;
                flpMessages.Width = layoutControlItem3.Width - 200;
                flpMessages.ShowPopup();
            }
        }

        private void gvPortalMessages_Click(object sender, EventArgs e)
        {
            //  LoadPortalMessagePopup();
        }

        private void LoadPortalMessagePopup()
        {
            DataTable dtMessage = (DataTable)gcPortalMessages.DataSource;
            if (dtMessage != null && dtMessage.Rows.Count > 0)
            {
                memMessageremarks.Text = gvPortalMessages.GetFocusedRowCellValue(colPortalContent) != null ? gvPortalMessages.GetFocusedRowCellValue(colPortalContent).ToString() : " ";

                //lblPopupUpDate.Text = gvPortalMessages.GetFocusedRowCellValue(colPortalUploadedOn) != null ?
                //    this.UtilityMember.DateSet.ToDate(gvPortalMessages.GetFocusedRowCellValue(colPortalUploadedOn).ToString(), false).ToShortDateString() : " ";
                lblPopupUpDate.Text = gvPortalMessages.GetFocusedRowCellValue(colPortalUploadedOn) != null ?
                    gvPortalMessages.GetFocusedRowCellValue(colPortalUploadedOn).ToString() : " ";

                lblMessDateFrom.Text = gvPortalMessages.GetFocusedRowCellValue(colPortalcastSubject) != null ?
                   gvPortalMessages.GetFocusedRowCellValue(colPortalcastSubject).ToString() : " ";

                simpleLabelItem2.Visibility = simpleLabelItem9.Visibility =
                    lblPopupStatus.Visibility = lblTransDateTo.Visibility = LayoutVisibility.Never;
                lciReply.Visibility = lciEditTicket.Visibility = LayoutVisibility.Never;
                //simpleLabelItem4.Text = "Date";
                //simpleLabelItem10.Text = "Message";
                //simpleLabelItem7.Text = "Subject";
                //lblPopupTitle.Text = "Portal Message";

                simpleLabelItem4.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.DATE_CAPTION);
                simpleLabelItem10.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.MESSAGE_CAPTION);
                simpleLabelItem7.Text = simpleLabelItem10.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.SUBJECT_CAPTION);
                lblPopupTitle.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.PORATL_MESSAGE_CAPTION);
                memMessageremarks.ForeColor = Color.DarkGreen;
                flpMessages.Height = layoutControlItem3.Height;
                flpMessages.Width = layoutControlItem3.Width - 200;
                flpMessages.ShowPopup();
            }
        }

        private void LoadPortalTickets()
        {
            DataTable dtMessage = (DataTable)gcTroubleTickets.DataSource;
            if (dtMessage != null && dtMessage.Rows.Count > 0)
            {
                memMessageremarks.Text = gvTroubleTickets.GetFocusedRowCellValue(gcolTicDescription) != null ? gvTroubleTickets.GetFocusedRowCellValue(gcolTicDescription).ToString() : " ";

                //lblPopupUpDate.Text = gvTroubleTickets.GetFocusedRowCellValue(gcolPostedDate) != null ?
                //    this.UtilityMember.DateSet.ToDate(gvTroubleTickets.GetFocusedRowCellValue(gcolPostedDate).ToString(), false).ToShortDateString() : " ";
                lblPopupUpDate.Text = gvTroubleTickets.GetFocusedRowCellValue(gcolPostedDate) != null ?
                    gvTroubleTickets.GetFocusedRowCellValue(gcolPostedDate).ToString() : " ";

                lblMessDateFrom.Text = gvTroubleTickets.GetFocusedRowCellValue(gcolSubject) != null ?
                   gvTroubleTickets.GetFocusedRowCellValue(gcolSubject).ToString() : " ";

                string ticno = gvTroubleTickets.GetFocusedRowCellValue(gcolTicketId) != null ?
                 gvTroubleTickets.GetFocusedRowCellValue(gcolTicketId).ToString() : " ";

                simpleLabelItem2.Visibility = lblPopupStatus.Visibility = simpleLabelItem9.Visibility = lblTransDateTo.Visibility = LayoutVisibility.Never;
                //  lciReply.Visibility = LayoutVisibility.Always;

                // Ticket Id
                pTickID = gvTroubleTickets.GetFocusedRowCellValue(gcolTicketId) != null ?
                this.UtilityMember.NumberSet.ToInteger(gvTroubleTickets.GetFocusedRowCellValue(gcolTicketId).ToString()) : 0;
                //Reply  Ticket Id
                pRepTickID = gvTroubleTickets.GetFocusedRowCellValue(gcolRepliedTicketID) != null ?
                this.UtilityMember.NumberSet.ToInteger(gvTroubleTickets.GetFocusedRowCellValue(gcolRepliedTicketID).ToString()) : 0;
                //Subject
                pSubject = gvTroubleTickets.GetFocusedRowCellValue(gcolSubject) != null ?
                gvTroubleTickets.GetFocusedRowCellValue(gcolSubject).ToString().Trim() : string.Empty;
                //Description
                pDescription = gvTroubleTickets.GetFocusedRowCellValue(gcolTicDescription) != null ?
                gvTroubleTickets.GetFocusedRowCellValue(gcolTicDescription).ToString().Trim() : string.Empty;
                //Posted By
                pPostedBy = gvTroubleTickets.GetFocusedRowCellValue(gcolPostedBy) != null ?
                this.UtilityMember.NumberSet.ToInteger(gvTroubleTickets.GetFocusedRowCellValue(gcolPostedBy).ToString()) : 0;
                //User Name
                pUserName = gvTroubleTickets.GetFocusedRowCellValue(gcolTicUserName) != null ?
                gvTroubleTickets.GetFocusedRowCellValue(gcolTicUserName).ToString().Trim() : string.Empty;
                //Priority
                pPriority = gvTroubleTickets.GetFocusedRowCellValue(gcolPriority) != null ?
                this.UtilityMember.NumberSet.ToInteger(gvTroubleTickets.GetFocusedRowCellValue(gcolPriority).ToString()) : 0;

                lciEditTicket.Visibility = pRepTickID == 0 ? LayoutVisibility.Always : LayoutVisibility.Never;

                //simpleLabelItem4.Text = "Date";
                //simpleLabelItem10.Text = "Description";
                //simpleLabelItem7.Text = "Subject";
                lblPopupTitle.Text = " <color=Blue>(Ticket No - " + ticno + ")</color>";  // Trouble Ticket 

                simpleLabelItem4.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.DATE_CAPTION);
                simpleLabelItem10.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.DESCRIPTION_CAPTION);
                simpleLabelItem7.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.SUBJECT_CAPTION);
                //lblPopupTitle.Text = this.GetMessage(MessageCatalog.Master.LoginDashBoard.TICKET_NO_CAPTION) + " -" + ticno + ")</color>";  // Trouble Ticket 
                lblPopupTitle.AllowHtmlStringInCaption = true;
                memMessageremarks.ForeColor = Color.DarkGreen;
                flpMessages.Height = layoutControlItem3.Height;
                flpMessages.Width = layoutControlItem3.Width - 200;
                flpMessages.ShowPopup();
            }
        }

        private void gvAmendments_Click(object sender, EventArgs e)
        {
            // LoadAmendmentPoup();
        }

        private void LoadAmendmentPoup()
        {
            DataTable dtMessage = (DataTable)gcAmendments.DataSource;
            if (dtMessage != null && dtMessage.Rows.Count > 0)
            {
                memAmendmentremarks.Text = gvAmendments.GetFocusedRowCellValue(colAmendRemarks) != null ? gvAmendments.GetFocusedRowCellValue(colAmendRemarks).ToString() : " ";

                lblAmnedProject.Text = (!string.IsNullOrEmpty(gvAmendments.GetFocusedRowCellValue(colAmendProject).ToString())) && (gvAmendments.GetFocusedRowCellValue(colAmendProject) != null)
                ? gvAmendments.GetFocusedRowCellValue(colAmendProject).ToString() : " ";

                lblVoucher.Text = gvAmendments.GetFocusedRowCellValue(colAmedVoucherType) != null ? gvAmendments.GetFocusedRowCellValue(colAmedVoucherType).ToString() : " ";

                lblAmendVoucheNo.Text = gvAmendments.GetFocusedRowCellValue(colVoucherNo) != null ? gvAmendments.GetFocusedRowCellValue(colVoucherNo).ToString() : " ";

                //lblAmendDate.Text = gvAmendments.GetFocusedRowCellValue(colAmendmentDate) != null ?
                //    this.UtilityMember.DateSet.ToDate(gvAmendments.GetFocusedRowCellValue(colAmendmentDate).ToString(), false).ToShortDateString() : " ";
                lblAmendDate.Text = gvAmendments.GetFocusedRowCellValue(colAmendmentDate) != null ?
                    gvAmendments.GetFocusedRowCellValue(colAmendmentDate).ToString() : " ";

                //lblVoucherDate.Text = gvAmendments.GetFocusedRowCellValue(colAmendVoucherDate) != null ?
                //   this.UtilityMember.DateSet.ToDate(gvAmendments.GetFocusedRowCellValue(colAmendVoucherDate).ToString(), false).ToShortDateString() : " ";
                lblVoucherDate.Text = gvAmendments.GetFocusedRowCellValue(colAmendVoucherDate) != null ?
                   gvAmendments.GetFocusedRowCellValue(colAmendVoucherDate).ToString() : " ";

                memMessageremarks.ForeColor = Color.DarkGreen;
                flpAmendments.Height = layoutControlItem3.Height;
                flpAmendments.Width = layoutControlItem3.Width - 200;

                flpAmendments.ShowPopup();
            }
        }

        private void gvPopupMessages_DoubleClick(object sender, EventArgs e)
        {
            LoadUploadVoucherPopup();
        }

        private void gvPortalMessages_DoubleClick(object sender, EventArgs e)
        {
            LoadPortalMessagePopup();
        }

        private void gvAmendments_DoubleClick(object sender, EventArgs e)
        {
            LoadAmendmentPoup();
        }

        private void tltItem_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            string FrameType = string.Empty;
            FrameType = e.Item.CurrentFrame.Elements[2].Text;
            if (NavigationType.Cash.ToString().Equals(FrameType) || NavigationType.Bank.ToString().Equals(FrameType.Trim()) || NavigationType.CashBank.ToString().Equals(FrameType))
            {
                frmTransactionView Transactionview = new frmTransactionView(this.AppSetting.RecentVoucherDate, ProjectId, ProjectName, 0, 1);
                Transactionview.MdiParent = this.ParentForm;
                Transactionview.Show();
            }
            else if (NavigationType.BRS.ToString().Equals(FrameType.Trim()))
            {
                frmBRS BRS = new frmBRS();
                BRS.ShowDialog();

            }
            else if (NavigationType.FD.ToString().Equals(FrameType))
            {
                frmRenewalView RenewalView = new frmRenewalView(FDTypes.WD);
                RenewalView.MdiParent = this.ParentForm;
                RenewalView.Show();
            }
        }

        private void gvTroubleTickets_DoubleClick(object sender, EventArgs e)
        {
            LoadPortalTickets();
        }

        private void peReply_Click(object sender, EventArgs e)
        {
            LoadPostTicketForm(pTickID, pRepTickID);
        }

        private void peAddTicket_Click(object sender, EventArgs e)
        {
            LoadPostTicketForm();
        }

        private void peEditTicket_Click(object sender, EventArgs e)
        {
            LoadPostTicketForm(pTickID, pRepTickID);
        }

        private void LoadPostTicketForm(int TicId = 0, int repTickId = 0)
        {
            try
            {
                DataSyncService.DataSynchronizerClient dataClient = new DataSyncService.DataSynchronizerClient();
                if (IsServiceAlive(dataClient.Endpoint.Address.ToString()))
                {
                    frmPostTicket posttick = new frmPostTicket(TicId, repTickId, pSubject, pDescription, pPriority, pPostedBy, pUserName);
                    posttick.TopMost = true;
                    posttick.ShowDialog();
                    // if (posttick.DialogResult == DialogResult.OK)
                    //  {
                    GetPortalMessages();
                    LoadPortalMessages();
                    //  }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.PortalMessage.PortalDataSynMessage.PORTAL_SERVICE_NOT_AVIALABLE));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void frmLoginDashboard_Activated(object sender, EventArgs e)
        {
            if (ProjectId.ToString() != this.AppSetting.UserProjectId)
            {
                IsSelected = true;
                LoadProjects();
                LoadDashBoardDefaults();
                LoadViewTypes();
                //LoadPortalMessages();
                IsSelected = false;
            }
        }

        private void lnkShowAll_Click(object sender, EventArgs e)
        {
            if (lnkShowAll.Text == SHOWALLCAPTION)
            {
                LoadMessages(SHOWALLCAPTION);
                lnkShowAll.Text = SHOWLATESTCAPTION;
            }
            else
            {
                LoadMessages(string.Empty);
                lnkShowAll.Text = SHOWALLCAPTION;
            }
        }

        private void btnFooterRefresh1_Click(object sender, EventArgs e)
        {
            GetPortalMessages();
        }

        private void gvTransBalance_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (gcTransBalance.DataSource != null)
            {
                if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    
                }     

                
                DataTable dtSummary =  gcTransBalance.DataSource as DataTable;
                dtSummary.Compute("SUM(AMOUNT)", string.Empty);
                e.TotalValue = 500;
            }
        }

        private void pecMessage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gvUsermanualFeatures_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (gvUsermanualFeatures.RowCount > 0)
            {
                GridView view = sender as GridView;
                if (e.Column.Name == colFeature.Name && gvUsermanualFeatures.GetRowCellValue(e.RowHandle, colFeatureGroup)!=null)
                {
                    string grp = gvUsermanualFeatures.GetRowCellValue(e.RowHandle, colFeatureGroup).ToString();
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                    if (grp.ToUpper() == "USER MANUAL")
                    {
                        e.Appearance.ForeColor = Color.Blue;
                        //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Underline);
                    }
                }
            }
        }

        private void gvUsermanualFeatures_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (gcUsermanualFeatures.DataSource != null)
            {
                if (e.Column == colShowUserManuals && gvUsermanualFeatures.GetRowCellValue(e.RowHandle, colFeatureGroup)!=null)
                {
                    string grp = gvUsermanualFeatures.GetRowCellValue(e.RowHandle, colFeatureGroup).ToString();

                    if (grp.ToUpper() != "USER MANUAL")
                    {
                        RepositoryItemButtonEdit emptyEditor = new RepositoryItemButtonEdit();
                        emptyEditor.Click += new EventHandler(emptyEditor_Click);
                        emptyEditor.Buttons.Clear();
                        emptyEditor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                        e.RepositoryItem = emptyEditor;
                    }
                }
            }
        }

        private void emptyEditor_Click(object sender, EventArgs e)
        {
            MessageRender.ShowMessage("Pay and get this feature. Contact Acme.erp Support Team", true);
        }

        private void gvUsermanualFeatures_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            /*if (e.Column == coUserManualdownload)
            {
                 string grp = gvUsermanualFeatures.GetRowCellValue(e.RowHandle, colFeatureGroup).ToString();
                    if (grp.ToUpper() == "USER MANUAL")
                 {
                     //e.Cache.Graphics.DrawImage(rbibtnUsermanualDownlaod.Buttons[0].Image, e.Bounds.X,  e.Bounds.Y + 2);
                 }
            }*/
        }

        private void gcUsermanualFeatures_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gcUserManuals_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gvUsermanualFeatures_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void gvUserManuals_ShownEditor(object sender, EventArgs e)
        {
           
        }

        private void gvUserManuals_RowClick(object sender, RowClickEventArgs e)
        {
            //if (gvUserManuals.RowCount > 0 && gvUserManuals.GetRowCellValue(e.RowHandle, colFeatureGroup) != null)
            //{
            //    string grp = gvUserManuals.GetRowCellValue(e.RowHandle, colFeatureGroup).ToString();
            //    if (grp.ToUpper() == "USER MANUAL")
            //    {
            //        bool isFileExists = false;
            //        string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_UserManuals);
            //        string linkpath = gvUserManuals.GetRowCellValue(e.RowHandle, colLinkFilename).ToString();
            //        if (!string.IsNullOrEmpty(linkpath))
            //        {
            //            VoucherUploadPath = Path.Combine(VoucherUploadPath, linkpath);
            //            if (File.Exists(VoucherUploadPath))
            //            {
            //                isFileExists = true;
            //            }
            //        }

            //        if (isFileExists)
            //        {
            //            System.Diagnostics.Process.Start(VoucherUploadPath);
            //        }
            //        else
            //        {
            //            this.ShowMessageBoxWarning("User Manual is not available, Download from Acme.erp Portal");
            //        }
            //    }
            //}
        }

        private void gvUserManuals_DoubleClick(object sender, EventArgs e)
        {
            if (gvUserManuals.RowCount > 0 )
            {
                DXMouseEventArgs ea = e as DXMouseEventArgs;
                GridView view = sender as GridView;
                GridHitInfo info = view.CalcHitInfo(ea.Location);
                if (info.HitTest == GridHitTest.RowCell)
                {
                    Int32 activerow = info.RowHandle;

                    string grp = gvUserManuals.GetRowCellValue(activerow, colFeatureGroup).ToString();
                    if (grp.ToUpper() == "USER MANUAL")
                    {
                        bool isFileExists = false;
                        string VoucherUploadPath = Path.Combine(SettingProperty.ApplicationStartUpPath, SettingProperty.Folder_UserManuals);
                        string linkpath = gvUserManuals.GetRowCellValue(activerow, colLinkFilename).ToString();
                        if (!string.IsNullOrEmpty(linkpath))
                        {
                            VoucherUploadPath = Path.Combine(VoucherUploadPath, linkpath);
                            if (File.Exists(VoucherUploadPath))
                            {
                                isFileExists = true;
                            }
                        }

                        if (isFileExists)
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = VoucherUploadPath;
                            startInfo.WindowStyle = ProcessWindowStyle.Normal;
                            Process.Start(startInfo);
                        }
                        else
                        {
                            this.ShowMessageBoxWarning("User Manual is not available, Download from Acme.erp Portal");
                        }
                    }
                }
            }
        }


    }

    public class TileControlViewInfoAce : TileControlViewInfo
    {
        private const int TileHeight = 50;
        private const int TileWidth = 850;

        public TileControlViewInfoAce(ITileControl control)
            : base(control)
        {
        }


        public override Size GetItemSize(TileItemViewInfo itemInfo)
        {
            return new Size(200, TileHeight);
        }
    }

    public class TileControlViewInfoAcen : TileItemViewInfo
    {
        private const int TileHeight = 70;
        private const int TileWidth = 1500;

        public TileControlViewInfoAcen(TileItem control)
            : base(control)
        {
        }

        //public override Size GetItemSize(TileItemViewInfo itemInfo)
        //{
        //    return new Size(200, TileHeight);
        //}
    }
}
