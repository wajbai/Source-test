namespace PAYROLL.Modules.Payroll_app
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpPayroll = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgGateway = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiCreate = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiOpen = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiReprocess = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiDelete = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiLock = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgDefinition = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiPayrollgrp = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiLoans = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPayrollComponent = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgAllotement = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiLoanMgt = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiGrpAllocation = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiComponentAllocation = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPayroll = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgStaff = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiStaffDetails = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgReports = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiPayRegister = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiLoanLedger = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPaySlip = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiCustomizeReport = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiAbstractPayroll = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiAbstractComponent = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiWagesReport = new DevExpress.XtraNavBar.NavBarItem();
            this.nbipfDailyReport = new DevExpress.XtraNavBar.NavBarItem();
            this.nbipfmonthlyreport = new DevExpress.XtraNavBar.NavBarItem();
            this.nbipfYearlyReport = new DevExpress.XtraNavBar.NavBarItem();
            this.nbipfEmployeeReport = new DevExpress.XtraNavBar.NavBarItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblPayrollMonth = new DevExpress.XtraEditors.LabelControl();
            this.lblcurrPayroll = new DevExpress.XtraEditors.LabelControl();
            this.lblCaption = new System.Windows.Forms.Label();
            this.nbiStaffOrder = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpPayroll.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeaderAndOnMouseHover;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.ShowHeaderFocus = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabbedMdiManager1.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager1_PageRemoved_1);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpPayroll});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dpPayroll
            // 
            this.dpPayroll.Controls.Add(this.dockPanel1_Container);
            this.dpPayroll.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpPayroll.FloatSize = new System.Drawing.Size(200, 291);
            this.dpPayroll.ID = new System.Guid("d9e956c9-6089-4461-8c0f-3be5e0b0282a");
            this.dpPayroll.Location = new System.Drawing.Point(0, 0);
            this.dpPayroll.Name = "dpPayroll";
            this.dpPayroll.Options.AllowFloating = false;
            this.dpPayroll.Options.FloatOnDblClick = false;
            this.dpPayroll.Options.ShowCloseButton = false;
            this.dpPayroll.Options.ShowMaximizeButton = false;
            this.dpPayroll.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpPayroll.Size = new System.Drawing.Size(200, 364);
            this.dpPayroll.Text = "Payroll";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(192, 337);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbgGateway;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgGateway,
            this.nbgDefinition,
            this.nbgAllotement,
            this.nbgStaff,
            this.nbgReports});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiCreate,
            this.nbiOpen,
            this.nbiReprocess,
            this.nbiDelete,
            this.nbiLock,
            this.nbiPayrollgrp,
            this.nbiLoans,
            this.nbiPayrollComponent,
            this.nbiLoanMgt,
            this.nbiGrpAllocation,
            this.nbiComponentAllocation,
            this.nbiPayroll,
            this.nbiStaffDetails,
            this.nbiPayRegister,
            this.nbiLoanLedger,
            this.nbiPaySlip,
            this.nbiCustomizeReport,
            this.nbiAbstractPayroll,
            this.nbiAbstractComponent,
            this.nbiWagesReport,
            this.nbipfDailyReport,
            this.nbipfmonthlyreport,
            this.nbipfYearlyReport,
            this.nbipfEmployeeReport,
            this.nbiStaffOrder});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 192;
            this.navBarControl1.Size = new System.Drawing.Size(192, 337);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbgGateway
            // 
            this.nbgGateway.Caption = "Gateway";
            this.nbgGateway.Expanded = true;
            this.nbgGateway.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.nbgGateway.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiCreate),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiOpen),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiReprocess),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDelete),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiLock)});
            this.nbgGateway.LargeImage = global::PAYROLL.Properties.Resources.bank;
            this.nbgGateway.LargeImageSize = new System.Drawing.Size(16, 16);
            this.nbgGateway.Name = "nbgGateway";
            // 
            // nbiCreate
            // 
            this.nbiCreate.Caption = "Create";
            this.nbiCreate.Name = "nbiCreate";
            this.nbiCreate.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiCreate.SmallImage")));
            this.nbiCreate.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiCreate.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiCreate_LinkClicked);
            // 
            // nbiOpen
            // 
            this.nbiOpen.Caption = "Open";
            this.nbiOpen.Name = "nbiOpen";
            this.nbiOpen.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiOpen.SmallImage")));
            this.nbiOpen.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiOpen.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiOpen_LinkClicked);
            // 
            // nbiReprocess
            // 
            this.nbiReprocess.Caption = "Reprocess";
            this.nbiReprocess.Name = "nbiReprocess";
            this.nbiReprocess.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiReprocess.SmallImage")));
            this.nbiReprocess.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiReprocess.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiReprocess_LinkClicked);
            // 
            // nbiDelete
            // 
            this.nbiDelete.Caption = "Delete";
            this.nbiDelete.Name = "nbiDelete";
            this.nbiDelete.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDelete.SmallImage")));
            this.nbiDelete.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiDelete.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiDelete_LinkClicked);
            // 
            // nbiLock
            // 
            this.nbiLock.Caption = "Lock";
            this.nbiLock.Name = "nbiLock";
            this.nbiLock.SmallImage = global::PAYROLL.Properties.Resources._lock;
            this.nbiLock.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiLock.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiLock_LinkClicked);
            // 
            // nbgDefinition
            // 
            this.nbgDefinition.Caption = "Definition";
            this.nbgDefinition.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPayrollgrp),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiLoans),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPayrollComponent)});
            this.nbgDefinition.LargeImage = global::PAYROLL.Properties.Resources.info;
            this.nbgDefinition.LargeImageSize = new System.Drawing.Size(16, 16);
            this.nbgDefinition.Name = "nbgDefinition";
            // 
            // nbiPayrollgrp
            // 
            this.nbiPayrollgrp.Caption = "Payroll Group";
            this.nbiPayrollgrp.Name = "nbiPayrollgrp";
            this.nbiPayrollgrp.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiPayrollgrp.SmallImage")));
            this.nbiPayrollgrp.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiPayrollgrp.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiPayrollGroup_LinkClicked);
            // 
            // nbiLoans
            // 
            this.nbiLoans.Caption = "Loans";
            this.nbiLoans.Name = "nbiLoans";
            this.nbiLoans.SmallImage = global::PAYROLL.Properties.Resources._02;
            this.nbiLoans.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiLoans.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiLoans_LinkClicked);
            // 
            // nbiPayrollComponent
            // 
            this.nbiPayrollComponent.Caption = "Payroll Component";
            this.nbiPayrollComponent.Name = "nbiPayrollComponent";
            this.nbiPayrollComponent.SmallImage = global::PAYROLL.Properties.Resources.Acc;
            this.nbiPayrollComponent.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiPayrollComponent.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.mbiPayrollComponent_LinkClicked);
            // 
            // nbgAllotement
            // 
            this.nbgAllotement.Caption = "Allotment";
            this.nbgAllotement.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiLoanMgt),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiGrpAllocation),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiComponentAllocation),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPayroll)});
            this.nbgAllotement.LargeImage = global::PAYROLL.Properties.Resources.Configuration;
            this.nbgAllotement.LargeImageSize = new System.Drawing.Size(16, 16);
            this.nbgAllotement.Name = "nbgAllotement";
            // 
            // nbiLoanMgt
            // 
            this.nbiLoanMgt.Caption = "Loan Management";
            this.nbiLoanMgt.Name = "nbiLoanMgt";
            this.nbiLoanMgt.SmallImage = global::PAYROLL.Properties.Resources.aiga_currency_exchange_32;
            this.nbiLoanMgt.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiLoanMgt.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiLoanManagement_LinkClicked);
            // 
            // nbiGrpAllocation
            // 
            this.nbiGrpAllocation.Caption = "Group Allocation";
            this.nbiGrpAllocation.Name = "nbiGrpAllocation";
            this.nbiGrpAllocation.SmallImage = global::PAYROLL.Properties.Resources.group_half_add_16;
            this.nbiGrpAllocation.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiGrpAllocation.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiGroupAllocation_LinkClicked);
            // 
            // nbiComponentAllocation
            // 
            this.nbiComponentAllocation.Caption = "Component Allocation";
            this.nbiComponentAllocation.Name = "nbiComponentAllocation";
            this.nbiComponentAllocation.SmallImage = global::PAYROLL.Properties.Resources.TDS;
            this.nbiComponentAllocation.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiComponentAllocation.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiComponentAllocation_LinkClicked);
            // 
            // nbiPayroll
            // 
            this.nbiPayroll.Caption = "Payroll";
            this.nbiPayroll.Name = "nbiPayroll";
            this.nbiPayroll.SmallImage = global::PAYROLL.Properties.Resources.Custom_Reports;
            this.nbiPayroll.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiPayroll.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.mbiPayroll_LinkClicked);
            // 
            // nbgStaff
            // 
            this.nbgStaff.Caption = "Staff";
            this.nbgStaff.Expanded = true;
            this.nbgStaff.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiStaffDetails),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiStaffOrder)});
            this.nbgStaff.LargeImage = global::PAYROLL.Properties.Resources.Accounting_Ledger1;
            this.nbgStaff.Name = "nbgStaff";
            // 
            // nbiStaffDetails
            // 
            this.nbiStaffDetails.Caption = "Staff Details";
            this.nbiStaffDetails.Name = "nbiStaffDetails";
            this.nbiStaffDetails.SmallImage = global::PAYROLL.Properties.Resources.account_contact;
            this.nbiStaffDetails.SmallImageSize = new System.Drawing.Size(16, 16);
            this.nbiStaffDetails.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiStaffDetails_LinkClicked);
            // 
            // nbgReports
            // 
            this.nbgReports.Caption = "Reports";
            this.nbgReports.Expanded = true;
            this.nbgReports.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPayRegister),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiLoanLedger),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPaySlip),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiCustomizeReport),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiAbstractPayroll),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiAbstractComponent),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiWagesReport),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbipfDailyReport),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbipfmonthlyreport),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbipfYearlyReport),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbipfEmployeeReport)});
            this.nbgReports.Name = "nbgReports";
            this.nbgReports.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbgReports.SmallImage")));
            // 
            // nbiPayRegister
            // 
            this.nbiPayRegister.Caption = "Pay Register";
            this.nbiPayRegister.Name = "nbiPayRegister";
            this.nbiPayRegister.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiPayRegister.SmallImage")));
            this.nbiPayRegister.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiPayRegister_LinkClicked);
            // 
            // nbiLoanLedger
            // 
            this.nbiLoanLedger.Caption = "Loan Ledger";
            this.nbiLoanLedger.Name = "nbiLoanLedger";
            this.nbiLoanLedger.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiLoanLedger.SmallImage")));
            this.nbiLoanLedger.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiLoanLedger_LinkClicked);
            // 
            // nbiPaySlip
            // 
            this.nbiPaySlip.Caption = "Pay Slip";
            this.nbiPaySlip.Name = "nbiPaySlip";
            this.nbiPaySlip.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiPaySlip.SmallImage")));
            this.nbiPaySlip.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiPaySlip_LinkClicked);
            // 
            // nbiCustomizeReport
            // 
            this.nbiCustomizeReport.Caption = "Customize Report";
            this.nbiCustomizeReport.Name = "nbiCustomizeReport";
            this.nbiCustomizeReport.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiCustomizeReport.SmallImage")));
            this.nbiCustomizeReport.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiCustomizeReport_LinkClicked);
            // 
            // nbiAbstractPayroll
            // 
            this.nbiAbstractPayroll.Caption = "Abstract Payroll";
            this.nbiAbstractPayroll.Name = "nbiAbstractPayroll";
            this.nbiAbstractPayroll.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiAbstractPayroll.SmallImage")));
            this.nbiAbstractPayroll.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiAbstractPayroll_LinkClicked);
            // 
            // nbiAbstractComponent
            // 
            this.nbiAbstractComponent.Caption = "Abstract Component";
            this.nbiAbstractComponent.Name = "nbiAbstractComponent";
            this.nbiAbstractComponent.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiAbstractComponent.SmallImage")));
            this.nbiAbstractComponent.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiAbstractComponent_LinkClicked);
            // 
            // nbiWagesReport
            // 
            this.nbiWagesReport.Caption = "Wages Report";
            this.nbiWagesReport.Name = "nbiWagesReport";
            this.nbiWagesReport.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiWagesReport.SmallImage")));
            this.nbiWagesReport.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiWagesReport_LinkClicked);
            // 
            // nbipfDailyReport
            // 
            this.nbipfDailyReport.Caption = "PF - Daily Report";
            this.nbipfDailyReport.Name = "nbipfDailyReport";
            this.nbipfDailyReport.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbipfDailyReport.SmallImage")));
            this.nbipfDailyReport.Visible = false;
            this.nbipfDailyReport.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbipfDailyReport_LinkClicked);
            // 
            // nbipfmonthlyreport
            // 
            this.nbipfmonthlyreport.Caption = "PF - Monthly Report";
            this.nbipfmonthlyreport.Name = "nbipfmonthlyreport";
            this.nbipfmonthlyreport.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbipfmonthlyreport.SmallImage")));
            this.nbipfmonthlyreport.Visible = false;
            this.nbipfmonthlyreport.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbipfmonthlyreport_LinkClicked);
            // 
            // nbipfYearlyReport
            // 
            this.nbipfYearlyReport.Caption = "PF - Yearly Report";
            this.nbipfYearlyReport.Name = "nbipfYearlyReport";
            this.nbipfYearlyReport.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbipfYearlyReport.SmallImage")));
            this.nbipfYearlyReport.Visible = false;
            this.nbipfYearlyReport.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbipfYearlyReport_LinkClicked);
            // 
            // nbipfEmployeeReport
            // 
            this.nbipfEmployeeReport.Caption = "Employee PF Report";
            this.nbipfEmployeeReport.Name = "nbipfEmployeeReport";
            this.nbipfEmployeeReport.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbipfEmployeeReport.SmallImage")));
            this.nbipfEmployeeReport.Visible = false;
            this.nbipfEmployeeReport.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbipfEmployeeReport_LinkClicked);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.lblCaption);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(200, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(595, 35);
            this.panelControl1.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.lblPayrollMonth);
            this.panelControl2.Controls.Add(this.lblcurrPayroll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(267, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(326, 31);
            this.panelControl2.TabIndex = 3;
            // 
            // lblPayrollMonth
            // 
            this.lblPayrollMonth.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblPayrollMonth.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblPayrollMonth.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPayrollMonth.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPayrollMonth.Location = new System.Drawing.Point(105, 0);
            this.lblPayrollMonth.Name = "lblPayrollMonth";
            this.lblPayrollMonth.Size = new System.Drawing.Size(221, 31);
            this.lblPayrollMonth.TabIndex = 2;
            // 
            // lblcurrPayroll
            // 
            this.lblcurrPayroll.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblcurrPayroll.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblcurrPayroll.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblcurrPayroll.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblcurrPayroll.Location = new System.Drawing.Point(0, 0);
            this.lblcurrPayroll.Name = "lblcurrPayroll";
            this.lblcurrPayroll.Size = new System.Drawing.Size(116, 31);
            this.lblcurrPayroll.TabIndex = 1;
            this.lblcurrPayroll.Text = "Payroll for";
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 16F);
            this.lblCaption.Location = new System.Drawing.Point(6, 2);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(154, 27);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Payroll System";
            // 
            // nbiStaffOrder
            // 
            this.nbiStaffOrder.Caption = "Staff Order";
            this.nbiStaffOrder.Name = "nbiStaffOrder";
            this.nbiStaffOrder.SmallImage = global::PAYROLL.Properties.Resources.group_half_add_16;
            this.nbiStaffOrder.ItemChanged += new System.EventHandler(this.nbiStaffOrder_ItemChanged);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(795, 364);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.dpPayroll);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payroll";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpPayroll.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpPayroll;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbgGateway;
        private DevExpress.XtraNavBar.NavBarItem nbiCreate;
        private DevExpress.XtraNavBar.NavBarItem nbiOpen;
        private DevExpress.XtraNavBar.NavBarItem nbiReprocess;
        private DevExpress.XtraNavBar.NavBarItem nbiDelete;
        private DevExpress.XtraNavBar.NavBarItem nbiLock;
        private DevExpress.XtraNavBar.NavBarGroup nbgDefinition;
        private DevExpress.XtraNavBar.NavBarItem nbiPayrollgrp;
        private DevExpress.XtraNavBar.NavBarItem nbiLoans;
        private DevExpress.XtraNavBar.NavBarItem nbiPayrollComponent;
        private DevExpress.XtraNavBar.NavBarGroup nbgAllotement;
        private DevExpress.XtraNavBar.NavBarItem nbiLoanMgt;
        private DevExpress.XtraNavBar.NavBarItem nbiGrpAllocation;
        private DevExpress.XtraNavBar.NavBarItem nbiComponentAllocation;
        private DevExpress.XtraNavBar.NavBarItem nbiPayroll;
        private DevExpress.XtraNavBar.NavBarGroup nbgStaff;
        private DevExpress.XtraNavBar.NavBarItem nbiStaffDetails;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblCaption;
        private DevExpress.XtraNavBar.NavBarGroup nbgReports;
        private DevExpress.XtraNavBar.NavBarItem nbiPayRegister;
        private DevExpress.XtraNavBar.NavBarItem nbiLoanLedger;
        private DevExpress.XtraNavBar.NavBarItem nbiPaySlip;
        private DevExpress.XtraNavBar.NavBarItem nbiCustomizeReport;
        private DevExpress.XtraNavBar.NavBarItem nbiAbstractPayroll;
        private DevExpress.XtraNavBar.NavBarItem nbiAbstractComponent;
        private DevExpress.XtraNavBar.NavBarItem nbiWagesReport;
        private DevExpress.XtraNavBar.NavBarItem nbipfDailyReport;
        private DevExpress.XtraNavBar.NavBarItem nbipfmonthlyreport;
        private DevExpress.XtraNavBar.NavBarItem nbipfYearlyReport;
        private DevExpress.XtraNavBar.NavBarItem nbipfEmployeeReport;
        public DevExpress.XtraEditors.LabelControl lblcurrPayroll;
        public DevExpress.XtraEditors.LabelControl lblPayrollMonth;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraNavBar.NavBarItem nbiStaffOrder;
    }
}