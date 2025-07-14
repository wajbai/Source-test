namespace Bosco.Report.ReportObject
{
    partial class PayPayslip
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayPayslip));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.grpStaffHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblDepartment = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBankAccount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblBankAccount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblPaySlipTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrHNetSalaryAmt = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrHeadingRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellEarningAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCellDeductionAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLblName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblDesignation = new DevExpress.XtraReports.UI.XRLabel();
            this.xrDesignation = new DevExpress.XtraReports.UI.XRLabel();
            this.xrHeaderProjectName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.xrUAN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblUAN = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.lblEmployerSign = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrossEarning = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrGrossDeduction = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRow = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellEarning = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrEarnAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellDeduction = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrDeductionAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.grpStaffFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrdotline = new DevExpress.XtraReports.UI.XRLine();
            this.xrNetSalary = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSignNote = new DevExpress.XtraReports.UI.XRLabel();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpPGHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.grpPGFooter = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrRow});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // grpStaffHeader
            // 
            this.grpStaffHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrDepartment,
            this.xrLblDepartment,
            this.xrBankAccount,
            this.xrLblBankAccount,
            this.xrCode,
            this.xrLblCode,
            this.xrlblPaySlipTitle,
            this.xrHNetSalaryAmt,
            this.xrTable2,
            this.xrLblName,
            this.xrName,
            this.xrLblDesignation,
            this.xrDesignation,
            this.xrHeaderProjectName,
            this.xrlblAddress,
            this.xrUAN,
            this.xrLblUAN});
            resources.ApplyResources(this.grpStaffHeader, "grpStaffHeader");
            this.grpStaffHeader.Name = "grpStaffHeader";
            this.grpStaffHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpStaffHeader_BeforePrint);
            // 
            // xrDepartment
            // 
            resources.ApplyResources(this.xrDepartment, "xrDepartment");
            this.xrDepartment.Name = "xrDepartment";
            this.xrDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrDepartment.StylePriority.UseFont = false;
            this.xrDepartment.StylePriority.UseTextAlignment = false;
            // 
            // xrLblDepartment
            // 
            resources.ApplyResources(this.xrLblDepartment, "xrLblDepartment");
            this.xrLblDepartment.Name = "xrLblDepartment";
            this.xrLblDepartment.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblDepartment.StylePriority.UseFont = false;
            this.xrLblDepartment.StylePriority.UsePadding = false;
            this.xrLblDepartment.StylePriority.UseTextAlignment = false;
            // 
            // xrBankAccount
            // 
            resources.ApplyResources(this.xrBankAccount, "xrBankAccount");
            this.xrBankAccount.Name = "xrBankAccount";
            this.xrBankAccount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrBankAccount.StylePriority.UseFont = false;
            this.xrBankAccount.StylePriority.UseTextAlignment = false;
            // 
            // xrLblBankAccount
            // 
            resources.ApplyResources(this.xrLblBankAccount, "xrLblBankAccount");
            this.xrLblBankAccount.Name = "xrLblBankAccount";
            this.xrLblBankAccount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblBankAccount.StylePriority.UseFont = false;
            this.xrLblBankAccount.StylePriority.UsePadding = false;
            this.xrLblBankAccount.StylePriority.UseTextAlignment = false;
            // 
            // xrCode
            // 
            resources.ApplyResources(this.xrCode, "xrCode");
            this.xrCode.Name = "xrCode";
            this.xrCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrCode.StylePriority.UseFont = false;
            this.xrCode.StylePriority.UseTextAlignment = false;
            this.xrCode.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.xrCode_PrintOnPage);
            // 
            // xrLblCode
            // 
            resources.ApplyResources(this.xrLblCode, "xrLblCode");
            this.xrLblCode.Name = "xrLblCode";
            this.xrLblCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblCode.StylePriority.UseFont = false;
            this.xrLblCode.StylePriority.UseTextAlignment = false;
            // 
            // xrlblPaySlipTitle
            // 
            resources.ApplyResources(this.xrlblPaySlipTitle, "xrlblPaySlipTitle");
            this.xrlblPaySlipTitle.Name = "xrlblPaySlipTitle";
            this.xrlblPaySlipTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblPaySlipTitle.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblPaySlipTitle.StylePriority.UseFont = false;
            this.xrlblPaySlipTitle.StylePriority.UseTextAlignment = false;
            // 
            // xrHNetSalaryAmt
            // 
            this.xrHNetSalaryAmt.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrHNetSalaryAmt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYWages.NET PAY")});
            resources.ApplyResources(this.xrHNetSalaryAmt, "xrHNetSalaryAmt");
            this.xrHNetSalaryAmt.Name = "xrHNetSalaryAmt";
            this.xrHNetSalaryAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrHNetSalaryAmt.StylePriority.UseBorders = false;
            this.xrHNetSalaryAmt.StylePriority.UseFont = false;
            this.xrHNetSalaryAmt.StylePriority.UseForeColor = false;
            this.xrHNetSalaryAmt.StylePriority.UseTextAlignment = false;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTable2, "xrTable2");
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrHeadingRow});
            this.xrTable2.StylePriority.UseBorders = false;
            // 
            // xrHeadingRow
            // 
            this.xrHeadingRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrCellEarningAmt,
            this.xrTableCell10,
            this.xrCellDeductionAmt});
            this.xrHeadingRow.Name = "xrHeadingRow";
            resources.ApplyResources(this.xrHeadingRow, "xrHeadingRow");
            // 
            // xrTableCell4
            // 
            resources.ApplyResources(this.xrTableCell4, "xrTableCell4");
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseForeColor = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            // 
            // xrCellEarningAmt
            // 
            this.xrCellEarningAmt.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrCellEarningAmt, "xrCellEarningAmt");
            this.xrCellEarningAmt.Name = "xrCellEarningAmt";
            this.xrCellEarningAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellEarningAmt.StylePriority.UseBorders = false;
            this.xrCellEarningAmt.StylePriority.UseFont = false;
            this.xrCellEarningAmt.StylePriority.UseForeColor = false;
            this.xrCellEarningAmt.StylePriority.UsePadding = false;
            this.xrCellEarningAmt.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableCell10, "xrTableCell10");
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseForeColor = false;
            this.xrTableCell10.StylePriority.UsePadding = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            // 
            // xrCellDeductionAmt
            // 
            resources.ApplyResources(this.xrCellDeductionAmt, "xrCellDeductionAmt");
            this.xrCellDeductionAmt.Name = "xrCellDeductionAmt";
            this.xrCellDeductionAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrCellDeductionAmt.StylePriority.UseFont = false;
            this.xrCellDeductionAmt.StylePriority.UseForeColor = false;
            this.xrCellDeductionAmt.StylePriority.UsePadding = false;
            this.xrCellDeductionAmt.StylePriority.UseTextAlignment = false;
            // 
            // xrLblName
            // 
            resources.ApplyResources(this.xrLblName, "xrLblName");
            this.xrLblName.Name = "xrLblName";
            this.xrLblName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblName.StylePriority.UseFont = false;
            this.xrLblName.StylePriority.UseTextAlignment = false;
            // 
            // xrName
            // 
            resources.ApplyResources(this.xrName, "xrName");
            this.xrName.Name = "xrName";
            this.xrName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrName.StylePriority.UseFont = false;
            this.xrName.StylePriority.UseTextAlignment = false;
            this.xrName.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrName_BeforePrint);
            this.xrName.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.xrName_PrintOnPage);
            // 
            // xrLblDesignation
            // 
            resources.ApplyResources(this.xrLblDesignation, "xrLblDesignation");
            this.xrLblDesignation.Name = "xrLblDesignation";
            this.xrLblDesignation.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblDesignation.StylePriority.UseFont = false;
            this.xrLblDesignation.StylePriority.UseTextAlignment = false;
            // 
            // xrDesignation
            // 
            resources.ApplyResources(this.xrDesignation, "xrDesignation");
            this.xrDesignation.Name = "xrDesignation";
            this.xrDesignation.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrDesignation.StylePriority.UseFont = false;
            this.xrDesignation.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderProjectName
            // 
            resources.ApplyResources(this.xrHeaderProjectName, "xrHeaderProjectName");
            this.xrHeaderProjectName.Name = "xrHeaderProjectName";
            this.xrHeaderProjectName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrHeaderProjectName.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrHeaderProjectName.StylePriority.UseFont = false;
            this.xrHeaderProjectName.StylePriority.UseTextAlignment = false;
            // 
            // xrlblAddress
            // 
            resources.ApplyResources(this.xrlblAddress, "xrlblAddress");
            this.xrlblAddress.Name = "xrlblAddress";
            this.xrlblAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblAddress.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblAddress.StylePriority.UseFont = false;
            this.xrlblAddress.StylePriority.UseTextAlignment = false;
            // 
            // xrUAN
            // 
            resources.ApplyResources(this.xrUAN, "xrUAN");
            this.xrUAN.Name = "xrUAN";
            this.xrUAN.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrUAN.StylePriority.UseFont = false;
            this.xrUAN.StylePriority.UseTextAlignment = false;
            // 
            // xrLblUAN
            // 
            resources.ApplyResources(this.xrLblUAN, "xrLblUAN");
            this.xrLblUAN.Name = "xrLblUAN";
            this.xrLblUAN.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.xrLblUAN.StylePriority.UseFont = false;
            this.xrLblUAN.StylePriority.UsePadding = false;
            this.xrLblUAN.StylePriority.UseTextAlignment = false;
            // 
            // xrLine2
            // 
            resources.ApplyResources(this.xrLine2, "xrLine2");
            this.xrLine2.Name = "xrLine2";
            // 
            // lblEmployerSign
            // 
            resources.ApplyResources(this.lblEmployerSign, "lblEmployerSign");
            this.lblEmployerSign.Name = "lblEmployerSign";
            this.lblEmployerSign.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblEmployerSign.StylePriority.UseFont = false;
            this.lblEmployerSign.StylePriority.UseTextAlignment = false;
            // 
            // xrTable3
            // 
            resources.ApplyResources(this.xrTable3, "xrTable3");
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrGrossEarning,
            this.xrTableCell8,
            this.xrGrossDeduction});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseForeColor = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            // 
            // xrGrossEarning
            // 
            this.xrGrossEarning.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrGrossEarning, "xrGrossEarning");
            this.xrGrossEarning.Name = "xrGrossEarning";
            this.xrGrossEarning.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrossEarning.StylePriority.UseBorders = false;
            this.xrGrossEarning.StylePriority.UseFont = false;
            this.xrGrossEarning.StylePriority.UsePadding = false;
            this.xrGrossEarning.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            this.xrGrossEarning.Summary = xrSummary2;
            this.xrGrossEarning.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrGrossEarning_BeforePrint);
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableCell8, "xrTableCell8");
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseForeColor = false;
            this.xrTableCell8.StylePriority.UsePadding = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            // 
            // xrGrossDeduction
            // 
            this.xrGrossDeduction.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrGrossDeduction, "xrGrossDeduction");
            this.xrGrossDeduction.Name = "xrGrossDeduction";
            this.xrGrossDeduction.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrGrossDeduction.StylePriority.UseBorders = false;
            this.xrGrossDeduction.StylePriority.UseFont = false;
            this.xrGrossDeduction.StylePriority.UsePadding = false;
            this.xrGrossDeduction.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary3, "xrSummary3");
            this.xrGrossDeduction.Summary = xrSummary3;
            this.xrGrossDeduction.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrGrossDeduction_BeforePrint);
            // 
            // xrRow
            // 
            this.xrRow.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrRow, "xrRow");
            this.xrRow.Name = "xrRow";
            this.xrRow.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrRow.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellEarning,
            this.xrEarnAmount,
            this.xrcellDeduction,
            this.xrDeductionAmount});
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrcellEarning
            // 
            this.xrcellEarning.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrcellEarning.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYWages.EARNINGS")});
            resources.ApplyResources(this.xrcellEarning, "xrcellEarning");
            this.xrcellEarning.Name = "xrcellEarning";
            this.xrcellEarning.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellEarning.StylePriority.UseBorders = false;
            this.xrcellEarning.StylePriority.UseFont = false;
            this.xrcellEarning.StylePriority.UsePadding = false;
            this.xrcellEarning.StylePriority.UseTextAlignment = false;
            this.xrcellEarning.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrcellEarning_BeforePrint);
            // 
            // xrEarnAmount
            // 
            this.xrEarnAmount.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrEarnAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYWages.EAMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrEarnAmount, "xrEarnAmount");
            this.xrEarnAmount.Name = "xrEarnAmount";
            this.xrEarnAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrEarnAmount.StylePriority.UseBorders = false;
            this.xrEarnAmount.StylePriority.UseFont = false;
            this.xrEarnAmount.StylePriority.UsePadding = false;
            this.xrEarnAmount.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.xrEarnAmount.Summary = xrSummary1;
            this.xrEarnAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrEarnAmount_BeforePrint);
            // 
            // xrcellDeduction
            // 
            this.xrcellDeduction.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrcellDeduction.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYWages.DEDUCTIONS")});
            resources.ApplyResources(this.xrcellDeduction, "xrcellDeduction");
            this.xrcellDeduction.Name = "xrcellDeduction";
            this.xrcellDeduction.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrcellDeduction.StylePriority.UseBorders = false;
            this.xrcellDeduction.StylePriority.UseFont = false;
            this.xrcellDeduction.StylePriority.UsePadding = false;
            this.xrcellDeduction.StylePriority.UseTextAlignment = false;
            this.xrcellDeduction.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrcellDeduction_BeforePrint);
            // 
            // xrDeductionAmount
            // 
            this.xrDeductionAmount.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrDeductionAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PAYWages.DAMOUNT", "{0:n}")});
            resources.ApplyResources(this.xrDeductionAmount, "xrDeductionAmount");
            this.xrDeductionAmount.Name = "xrDeductionAmount";
            this.xrDeductionAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrDeductionAmount.StylePriority.UseBorders = false;
            this.xrDeductionAmount.StylePriority.UseFont = false;
            this.xrDeductionAmount.StylePriority.UsePadding = false;
            this.xrDeductionAmount.StylePriority.UseTextAlignment = false;
            this.xrDeductionAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrDeductionAmount_BeforePrint);
            // 
            // grpStaffFooter
            // 
            this.grpStaffFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubSignFooter,
            this.xrdotline,
            this.xrNetSalary,
            this.xrLabel5,
            this.lblEmployerSign,
            this.xrLine2,
            this.xrTable3,
            this.lblSignNote});
            resources.ApplyResources(this.grpStaffFooter, "grpStaffFooter");
            this.grpStaffFooter.Name = "grpStaffFooter";
            this.grpStaffFooter.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.grpStaffFooter_BeforePrint);
            // 
            // xrSubSignFooter
            // 
            resources.ApplyResources(this.xrSubSignFooter, "xrSubSignFooter");
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            // 
            // xrdotline
            // 
            this.xrdotline.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrdotline.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            resources.ApplyResources(this.xrdotline, "xrdotline");
            this.xrdotline.Name = "xrdotline";
            this.xrdotline.StylePriority.UseBorderDashStyle = false;
            // 
            // xrNetSalary
            // 
            this.xrNetSalary.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrNetSalary, "xrNetSalary");
            this.xrNetSalary.Name = "xrNetSalary";
            this.xrNetSalary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrNetSalary.StylePriority.UseBorders = false;
            this.xrNetSalary.StylePriority.UseFont = false;
            this.xrNetSalary.StylePriority.UseForeColor = false;
            this.xrNetSalary.StylePriority.UseTextAlignment = false;
            this.xrNetSalary.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrNetSalary_BeforePrint);
            // 
            // xrLabel5
            // 
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            // 
            // lblSignNote
            // 
            resources.ApplyResources(this.lblSignNote, "lblSignNote");
            this.lblSignNote.Name = "lblSignNote";
            this.lblSignNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSignNote.StylePriority.UseFont = false;
            this.lblSignNote.StylePriority.UseTextAlignment = false;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpPGHeader
            // 
            resources.ApplyResources(this.grpPGHeader, "grpPGHeader");
            this.grpPGHeader.Level = 1;
            this.grpPGHeader.Name = "grpPGHeader";
            // 
            // grpPGFooter
            // 
            resources.ApplyResources(this.grpPGFooter, "grpPGFooter");
            this.grpPGFooter.Level = 1;
            this.grpPGFooter.Name = "grpPGFooter";
            // 
            // PayPayslip
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.grpStaffHeader,
            this.grpStaffFooter,
            this.grpPGHeader,
            this.grpPGFooter});
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.grpPGFooter, 0);
            this.Controls.SetChildIndex(this.grpPGHeader, 0);
            this.Controls.SetChildIndex(this.grpStaffFooter, 0);
            this.Controls.SetChildIndex(this.grpStaffHeader, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand grpStaffHeader;
        private DevExpress.XtraReports.UI.XRLabel xrDesignation;
        private DevExpress.XtraReports.UI.XRLabel xrLblDesignation;
        private DevExpress.XtraReports.UI.XRLabel xrName;
        private DevExpress.XtraReports.UI.XRLabel xrLblName;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel lblEmployerSign;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTable xrRow;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrcellEarning;
        private DevExpress.XtraReports.UI.XRTableCell xrEarnAmount;
        private DevExpress.XtraReports.UI.XRTableCell xrcellDeduction;
        private DevExpress.XtraReports.UI.XRTableCell xrDeductionAmount;
        private DevExpress.XtraReports.UI.GroupFooterBand grpStaffFooter;
        private DevExpress.XtraReports.UI.XRTableCell xrGrossEarning;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrHeadingRow;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrCellEarningAmt;
        private DevExpress.XtraReports.UI.XRTableCell xrGrossDeduction;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrNetSalary;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrCellDeductionAmt;
        private DevExpress.XtraReports.UI.XRLabel xrHNetSalaryAmt;
        private DevExpress.XtraReports.UI.XRLabel xrHeaderProjectName;
        private DevExpress.XtraReports.UI.XRLabel xrlblAddress;
        private DevExpress.XtraReports.UI.XRLabel xrlblPaySlipTitle;
        private DevExpress.XtraReports.UI.XRLabel xrUAN;
        private DevExpress.XtraReports.UI.XRLabel xrLblUAN;
        private DevExpress.XtraReports.UI.XRLine xrdotline;
        private DevExpress.XtraReports.UI.XRLabel xrLblCode;
        private DevExpress.XtraReports.UI.XRLabel xrCode;
        private DevExpress.XtraReports.UI.XRLabel xrBankAccount;
        private DevExpress.XtraReports.UI.XRLabel xrLblBankAccount;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
        private DevExpress.XtraReports.UI.XRLabel lblSignNote;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpPGHeader;
        private DevExpress.XtraReports.UI.GroupFooterBand grpPGFooter;
        private DevExpress.XtraReports.UI.XRLabel xrDepartment;
        private DevExpress.XtraReports.UI.XRLabel xrLblDepartment;
    }
}
