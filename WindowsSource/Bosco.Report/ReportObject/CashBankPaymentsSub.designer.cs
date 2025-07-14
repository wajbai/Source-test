namespace Bosco.Report.ReportObject
{
    partial class CashBankPaymentsSub
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashBankPaymentsSub));
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrpicReportLogoLeft1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrlblInstituteAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblInstituteName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.xrHeaderProjectName = new DevExpress.XtraReports.UI.XRLabel();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.grpHeaderVoucher = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTblHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblVoucherNoCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVoucherNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVoucherDateCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblVoucherDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblParticularsCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblCreditAmt = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrSubSignFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrTblRequireSignNote = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow42 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCellRequireSignNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrlblTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblReceivedFromCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblReceivedFrom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblSumCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtblAmtInWords = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCashBankRecPayDetails = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrTblGeneralNarration = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.lblNarrationCaption = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTblLedgerDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrtblLedger = new DevExpress.XtraReports.UI.XRTableCell();
            this.xttblLedgerAmount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRowLedgerNarration = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellLedgerNarration = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCrossBandBox2 = new DevExpress.XtraReports.UI.XRCrossBandBox();
            this.xrSubreportCCDetails = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblRequireSignNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGeneralNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLedgerDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreportCCDetails,
            this.xrTblLedgerDetails});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            this.Detail.AfterPrint += new System.EventHandler(this.Detail_AfterPrint);
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrpicReportLogoLeft1
            // 
            resources.ApplyResources(this.xrpicReportLogoLeft1, "xrpicReportLogoLeft1");
            this.xrpicReportLogoLeft1.Name = "xrpicReportLogoLeft1";
            this.xrpicReportLogoLeft1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 1, 0, 100F);
            this.xrpicReportLogoLeft1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrpicReportLogoLeft1.StylePriority.UsePadding = false;
            // 
            // xrlblInstituteAddress
            // 
            resources.ApplyResources(this.xrlblInstituteAddress, "xrlblInstituteAddress");
            this.xrlblInstituteAddress.Name = "xrlblInstituteAddress";
            this.xrlblInstituteAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblInstituteAddress.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblInstituteAddress.StylePriority.UseFont = false;
            this.xrlblInstituteAddress.StylePriority.UseTextAlignment = false;
            // 
            // xrlblInstituteName
            // 
            resources.ApplyResources(this.xrlblInstituteName, "xrlblInstituteName");
            this.xrlblInstituteName.Name = "xrlblInstituteName";
            this.xrlblInstituteName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblInstituteName.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblInstituteName.StylePriority.UseFont = false;
            this.xrlblInstituteName.StylePriority.UseTextAlignment = false;
            // 
            // xrReportTitle
            // 
            resources.ApplyResources(this.xrReportTitle, "xrReportTitle");
            this.xrReportTitle.Name = "xrReportTitle";
            this.xrReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrReportTitle.StylePriority.UseFont = false;
            this.xrReportTitle.StylePriority.UsePadding = false;
            this.xrReportTitle.StylePriority.UseTextAlignment = false;
            // 
            // xrHeaderProjectName
            // 
            this.xrHeaderProjectName.CanShrink = true;
            resources.ApplyResources(this.xrHeaderProjectName, "xrHeaderProjectName");
            this.xrHeaderProjectName.Multiline = true;
            this.xrHeaderProjectName.Name = "xrHeaderProjectName";
            this.xrHeaderProjectName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrHeaderProjectName.StylePriority.UseFont = false;
            this.xrHeaderProjectName.StylePriority.UseForeColor = false;
            this.xrHeaderProjectName.StylePriority.UsePadding = false;
            this.xrHeaderProjectName.StylePriority.UseTextAlignment = false;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpHeaderVoucher
            // 
            this.grpHeaderVoucher.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTblHeader,
            this.xrHeaderProjectName,
            this.xrReportTitle,
            this.xrlblInstituteName,
            this.xrlblInstituteAddress,
            this.xrpicReportLogoLeft1});
            this.grpHeaderVoucher.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("VOUCHER_DATE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("VOUCHER_NO", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            resources.ApplyResources(this.grpHeaderVoucher, "grpHeaderVoucher");
            this.grpHeaderVoucher.Name = "grpHeaderVoucher";
            // 
            // xrTblHeader
            // 
            resources.ApplyResources(this.xrTblHeader, "xrTblHeader");
            this.xrTblHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTblHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblHeader.BorderWidth = 1F;
            this.xrTblHeader.Name = "xrTblHeader";
            this.xrTblHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow10});
            this.xrTblHeader.StylePriority.UseBorderColor = false;
            this.xrTblHeader.StylePriority.UseBorderDashStyle = false;
            this.xrTblHeader.StylePriority.UseBorders = false;
            this.xrTblHeader.StylePriority.UseBorderWidth = false;
            this.xrTblHeader.StylePriority.UseFont = false;
            this.xrTblHeader.StylePriority.UseForeColor = false;
            this.xrTblHeader.StylePriority.UsePadding = false;
            this.xrTblHeader.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow1.BorderWidth = 1F;
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblVoucherNoCaption,
            this.lblVoucherNo,
            this.lblVoucherDateCaption,
            this.lblVoucherDate});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow1.StylePriority.UseBorders = false;
            this.xrTableRow1.StylePriority.UseBorderWidth = false;
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // lblVoucherNoCaption
            // 
            this.lblVoucherNoCaption.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.lblVoucherNoCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.lblVoucherNoCaption, "lblVoucherNoCaption");
            this.lblVoucherNoCaption.Name = "lblVoucherNoCaption";
            this.lblVoucherNoCaption.StylePriority.UseBorderDashStyle = false;
            this.lblVoucherNoCaption.StylePriority.UseBorders = false;
            this.lblVoucherNoCaption.StylePriority.UseFont = false;
            // 
            // lblVoucherNo
            // 
            this.lblVoucherNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.VOUCHER_NO")});
            resources.ApplyResources(this.lblVoucherNo, "lblVoucherNo");
            this.lblVoucherNo.Name = "lblVoucherNo";
            this.lblVoucherNo.StylePriority.UseFont = false;
            this.lblVoucherNo.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblVoucherNo_EvaluateBinding);
            // 
            // lblVoucherDateCaption
            // 
            resources.ApplyResources(this.lblVoucherDateCaption, "lblVoucherDateCaption");
            this.lblVoucherDateCaption.Name = "lblVoucherDateCaption";
            this.lblVoucherDateCaption.StylePriority.UseFont = false;
            this.lblVoucherDateCaption.StylePriority.UseTextAlignment = false;
            // 
            // lblVoucherDate
            // 
            this.lblVoucherDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.VOUCHER_DATE", "{0:d}")});
            resources.ApplyResources(this.lblVoucherDate, "lblVoucherDate");
            this.lblVoucherDate.Name = "lblVoucherDate";
            this.lblVoucherDate.StylePriority.UseFont = false;
            this.lblVoucherDate.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblParticularsCaption,
            this.xrtblCreditAmt});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableRow10, "xrTableRow10");
            // 
            // lblParticularsCaption
            // 
            resources.ApplyResources(this.lblParticularsCaption, "lblParticularsCaption");
            this.lblParticularsCaption.Name = "lblParticularsCaption";
            this.lblParticularsCaption.StylePriority.UseFont = false;
            this.lblParticularsCaption.StylePriority.UseTextAlignment = false;
            // 
            // xrtblCreditAmt
            // 
            resources.ApplyResources(this.xrtblCreditAmt, "xrtblCreditAmt");
            this.xrtblCreditAmt.Name = "xrtblCreditAmt";
            this.xrtblCreditAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 5, 3, 3, 100F);
            this.xrtblCreditAmt.StylePriority.UseFont = false;
            this.xrtblCreditAmt.StylePriority.UsePadding = false;
            this.xrtblCreditAmt.StylePriority.UseTextAlignment = false;
            this.xrtblCreditAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrtblCreditAmt_BeforePrint);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubSignFooter,
            this.xrTblRequireSignNote,
            this.xrTable1,
            this.xrCashBankRecPayDetails,
            this.xrTblGeneralNarration});
            resources.ApplyResources(this.GroupFooter1, "GroupFooter1");
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrSubSignFooter
            // 
            resources.ApplyResources(this.xrSubSignFooter, "xrSubSignFooter");
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            // 
            // xrTblRequireSignNote
            // 
            this.xrTblRequireSignNote.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.xrTblRequireSignNote, "xrTblRequireSignNote");
            this.xrTblRequireSignNote.Name = "xrTblRequireSignNote";
            this.xrTblRequireSignNote.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow42});
            this.xrTblRequireSignNote.StylePriority.UseBorders = false;
            this.xrTblRequireSignNote.StylePriority.UseFont = false;
            this.xrTblRequireSignNote.StylePriority.UseTextAlignment = false;
            this.xrTblRequireSignNote.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblRequireSignNote_BeforePrint);
            // 
            // xrTableRow42
            // 
            this.xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellRequireSignNote});
            this.xrTableRow42.Name = "xrTableRow42";
            resources.ApplyResources(this.xrTableRow42, "xrTableRow42");
            // 
            // xrCellRequireSignNote
            // 
            resources.ApplyResources(this.xrCellRequireSignNote, "xrCellRequireSignNote");
            this.xrCellRequireSignNote.Name = "xrCellRequireSignNote";
            this.xrCellRequireSignNote.StylePriority.UseFont = false;
            this.xrCellRequireSignNote.StylePriority.UseTextAlignment = false;
            this.xrCellRequireSignNote.WordWrap = false;
            // 
            // xrTable1
            // 
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.BorderWidth = 1F;
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow2,
            this.xrTableRow12});
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorderDashStyle = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseBorderWidth = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            this.xrTable1.StylePriority.UsePadding = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrlblTotal,
            this.xrcellSum});
            this.xrTableRow3.Name = "xrTableRow3";
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            // 
            // xrlblTotal
            // 
            this.xrlblTotal.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            resources.ApplyResources(this.xrlblTotal, "xrlblTotal");
            this.xrlblTotal.Name = "xrlblTotal";
            this.xrlblTotal.StylePriority.UseBorderDashStyle = false;
            this.xrlblTotal.StylePriority.UseFont = false;
            this.xrlblTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrcellSum
            // 
            this.xrcellSum.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrcellSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.AMOUNT")});
            resources.ApplyResources(this.xrcellSum, "xrcellSum");
            this.xrcellSum.Name = "xrcellSum";
            this.xrcellSum.StylePriority.UseBorderDashStyle = false;
            this.xrcellSum.StylePriority.UseFont = false;
            this.xrcellSum.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrcellSum.Summary = xrSummary1;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblReceivedFromCaption,
            this.lblReceivedFrom});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableRow2, "xrTableRow2");
            // 
            // lblReceivedFromCaption
            // 
            resources.ApplyResources(this.lblReceivedFromCaption, "lblReceivedFromCaption");
            this.lblReceivedFromCaption.Name = "lblReceivedFromCaption";
            this.lblReceivedFromCaption.StylePriority.UseFont = false;
            // 
            // lblReceivedFrom
            // 
            this.lblReceivedFrom.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.NAME_ADDRESS")});
            resources.ApplyResources(this.lblReceivedFrom, "lblReceivedFrom");
            this.lblReceivedFrom.Name = "lblReceivedFrom";
            this.lblReceivedFrom.StylePriority.UseFont = false;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblSumCaption,
            this.xrtblAmtInWords});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableRow12, "xrTableRow12");
            // 
            // lblSumCaption
            // 
            this.lblSumCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.lblSumCaption, "lblSumCaption");
            this.lblSumCaption.Name = "lblSumCaption";
            this.lblSumCaption.StylePriority.UseBorders = false;
            this.lblSumCaption.StylePriority.UseFont = false;
            // 
            // xrtblAmtInWords
            // 
            this.xrtblAmtInWords.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblAmtInWords.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.RUPPEE_AMT")});
            resources.ApplyResources(this.xrtblAmtInWords, "xrtblAmtInWords");
            this.xrtblAmtInWords.Name = "xrtblAmtInWords";
            this.xrtblAmtInWords.StylePriority.UseBorders = false;
            this.xrtblAmtInWords.StylePriority.UseFont = false;
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblAmtInWords.Summary = xrSummary2;
            this.xrtblAmtInWords.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrtblAmtInWords_SummaryGetResult_1);
            this.xrtblAmtInWords.SummaryReset += new System.EventHandler(this.xrtblAmtInWords_SummaryReset_1);
            this.xrtblAmtInWords.SummaryRowChanged += new System.EventHandler(this.xrtblAmtInWords_SummaryRowChanged_1);
            // 
            // xrCashBankRecPayDetails
            // 
            resources.ApplyResources(this.xrCashBankRecPayDetails, "xrCashBankRecPayDetails");
            this.xrCashBankRecPayDetails.Name = "xrCashBankRecPayDetails";
            this.xrCashBankRecPayDetails.ReportSource = new Bosco.Report.ReportObject.CashBankReceiptPaymentDetails();
            this.xrCashBankRecPayDetails.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCashBankRecPayDetails_BeforePrint);
            // 
            // xrTblGeneralNarration
            // 
            resources.ApplyResources(this.xrTblGeneralNarration, "xrTblGeneralNarration");
            this.xrTblGeneralNarration.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTblGeneralNarration.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblGeneralNarration.BorderWidth = 1F;
            this.xrTblGeneralNarration.Name = "xrTblGeneralNarration";
            this.xrTblGeneralNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblGeneralNarration.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTblGeneralNarration.StylePriority.UseBorderColor = false;
            this.xrTblGeneralNarration.StylePriority.UseBorderDashStyle = false;
            this.xrTblGeneralNarration.StylePriority.UseBorders = false;
            this.xrTblGeneralNarration.StylePriority.UseBorderWidth = false;
            this.xrTblGeneralNarration.StylePriority.UseFont = false;
            this.xrTblGeneralNarration.StylePriority.UseForeColor = false;
            this.xrTblGeneralNarration.StylePriority.UsePadding = false;
            this.xrTblGeneralNarration.StylePriority.UseTextAlignment = false;
            this.xrTblGeneralNarration.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblGeneralNarration_BeforePrint);
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblNarrationCaption,
            this.lblNarration});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableRow7, "xrTableRow7");
            // 
            // lblNarrationCaption
            // 
            this.lblNarrationCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.lblNarrationCaption, "lblNarrationCaption");
            this.lblNarrationCaption.Name = "lblNarrationCaption";
            this.lblNarrationCaption.StylePriority.UseBorders = false;
            this.lblNarrationCaption.StylePriority.UseFont = false;
            // 
            // lblNarration
            // 
            this.lblNarration.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.GENERAL_NARRATION")});
            resources.ApplyResources(this.lblNarration, "lblNarration");
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.StylePriority.UseBorders = false;
            this.lblNarration.StylePriority.UseFont = false;
            // 
            // xrTblLedgerDetails
            // 
            resources.ApplyResources(this.xrTblLedgerDetails, "xrTblLedgerDetails");
            this.xrTblLedgerDetails.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTblLedgerDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblLedgerDetails.BorderWidth = 1F;
            this.xrTblLedgerDetails.Name = "xrTblLedgerDetails";
            this.xrTblLedgerDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblLedgerDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11,
            this.xrRowLedgerNarration});
            this.xrTblLedgerDetails.StyleName = "styleRow";
            this.xrTblLedgerDetails.StylePriority.UseBorderColor = false;
            this.xrTblLedgerDetails.StylePriority.UseBorderDashStyle = false;
            this.xrTblLedgerDetails.StylePriority.UseBorders = false;
            this.xrTblLedgerDetails.StylePriority.UseBorderWidth = false;
            this.xrTblLedgerDetails.StylePriority.UseFont = false;
            this.xrTblLedgerDetails.StylePriority.UseForeColor = false;
            this.xrTblLedgerDetails.StylePriority.UsePadding = false;
            this.xrTblLedgerDetails.StylePriority.UseTextAlignment = false;
            this.xrTblLedgerDetails.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblLedgerDetails_BeforePrint);
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrtblLedger,
            this.xttblLedgerAmount});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableRow11, "xrTableRow11");
            // 
            // xrtblLedger
            // 
            this.xrtblLedger.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.LEDGER_NAME")});
            resources.ApplyResources(this.xrtblLedger, "xrtblLedger");
            this.xrtblLedger.Name = "xrtblLedger";
            this.xrtblLedger.StylePriority.UseFont = false;
            // 
            // xttblLedgerAmount
            // 
            this.xttblLedgerAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.AMOUNT", "{0:n}")});
            resources.ApplyResources(this.xttblLedgerAmount, "xttblLedgerAmount");
            this.xttblLedgerAmount.Name = "xttblLedgerAmount";
            this.xttblLedgerAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 5, 3, 3, 100F);
            this.xttblLedgerAmount.StylePriority.UseFont = false;
            this.xttblLedgerAmount.StylePriority.UsePadding = false;
            this.xttblLedgerAmount.StylePriority.UseTextAlignment = false;
            this.xttblLedgerAmount.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.xttblLedgerAmount_EvaluateBinding);
            this.xttblLedgerAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xttblLedgerAmount_BeforePrint);
            // 
            // xrRowLedgerNarration
            // 
            this.xrRowLedgerNarration.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellLedgerNarration,
            this.xrTableCell2});
            this.xrRowLedgerNarration.Name = "xrRowLedgerNarration";
            resources.ApplyResources(this.xrRowLedgerNarration, "xrRowLedgerNarration");
            // 
            // xrcellLedgerNarration
            // 
            this.xrcellLedgerNarration.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrcellLedgerNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.NARRATION")});
            resources.ApplyResources(this.xrcellLedgerNarration, "xrcellLedgerNarration");
            this.xrcellLedgerNarration.Name = "xrcellLedgerNarration";
            this.xrcellLedgerNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.xrcellLedgerNarration.StylePriority.UseBorderDashStyle = false;
            this.xrcellLedgerNarration.StylePriority.UseFont = false;
            this.xrcellLedgerNarration.StylePriority.UsePadding = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderDashStyle = false;
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            // 
            // xrCrossBandBox2
            // 
            this.xrCrossBandBox2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCrossBandBox2.BorderWidth = 1F;
            this.xrCrossBandBox2.EndBand = this.GroupFooter1;
            resources.ApplyResources(this.xrCrossBandBox2, "xrCrossBandBox2");
            this.xrCrossBandBox2.Name = "xrCrossBandBox2";
            this.xrCrossBandBox2.StartBand = this.GroupFooter1;
            this.xrCrossBandBox2.WidthF = 143F;
            // 
            // xrSubreportCCDetails
            // 
            this.xrSubreportCCDetails.CanShrink = true;
            resources.ApplyResources(this.xrSubreportCCDetails, "xrSubreportCCDetails");
            this.xrSubreportCCDetails.Name = "xrSubreportCCDetails";
            this.xrSubreportCCDetails.ReportSource = new Bosco.Report.ReportObject.UcCCDetail();
            // 
            // CashBankPaymentsSub
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.PageHeader,
            this.Detail,
            this.grpHeaderVoucher,
            this.GroupFooter1});
            this.CrossBandControls.AddRange(new DevExpress.XtraReports.UI.XRCrossBandControl[] {
            this.xrCrossBandBox2});
            this.DataMember = "CashBankReceipts";
            this.DataSource = this.reportSetting1;
            resources.ApplyResources(this, "$this");
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupFooter1, 0);
            this.Controls.SetChildIndex(this.grpHeaderVoucher, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            this.Controls.SetChildIndex(this.PageHeader, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dtTOCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblRequireSignNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblGeneralNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTblLedgerDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRLabel xrReportTitle;
        private DevExpress.XtraReports.UI.XRLabel xrHeaderProjectName;
        private DevExpress.XtraReports.UI.GroupHeaderBand grpHeaderVoucher;
        private DevExpress.XtraReports.UI.XRTable xrTblHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherNoCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherNo;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherDate;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow10;
        private DevExpress.XtraReports.UI.XRTableCell lblParticularsCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrtblCreditAmt;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTable xrTblLedgerDetails;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow11;
        private DevExpress.XtraReports.UI.XRTableCell xrtblLedger;
        private DevExpress.XtraReports.UI.XRTableCell xttblLedgerAmount;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell lblReceivedFromCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblReceivedFrom;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        private DevExpress.XtraReports.UI.XRTableCell lblSumCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrtblAmtInWords;
        private DevExpress.XtraReports.UI.XRLabel xrlblInstituteAddress;
        private DevExpress.XtraReports.UI.XRLabel xrlblInstituteName;
        private DevExpress.XtraReports.UI.XRTable xrTblGeneralNarration;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell lblNarrationCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblNarration;
        private DevExpress.XtraReports.UI.XRSubreport xrCashBankRecPayDetails;
        private DevExpress.XtraReports.UI.XRPictureBox xrpicReportLogoLeft1;
        private DevExpress.XtraReports.UI.XRTableCell lblVoucherDateCaption;
        private DevExpress.XtraReports.UI.XRTable xrTblRequireSignNote;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow42;
        private DevExpress.XtraReports.UI.XRTableCell xrCellRequireSignNote;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrlblTotal;
        private DevExpress.XtraReports.UI.XRTableCell xrcellSum;
        private DevExpress.XtraReports.UI.XRCrossBandBox xrCrossBandBox2;
        private DevExpress.XtraReports.UI.XRSubreport xrSubSignFooter;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreportCCDetails;
        private DevExpress.XtraReports.UI.XRTableRow xrRowLedgerNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrcellLedgerNarration;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
    }
}
