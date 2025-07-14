namespace Bosco.Report.ReportObject
{
    partial class CashBankPayments
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
            this.xrDuplicatePageBreak = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrlnDuplicatedDot = new DevExpress.XtraReports.UI.XRLine();
            this.xrDuplicateCopy = new DevExpress.XtraReports.UI.XRSubreport();
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
            this.xrVouhcerPageBreak = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrTblLedgerDetails = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrcellLedgerName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrcellLedgerAmount = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.Detail.HeightF = 67.12501F;
            this.Detail.Visible = true;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            this.Detail.AfterPrint += new System.EventHandler(this.Detail_AfterPrint);
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 20.8333F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Visible = false;
            // 
            // xrpicReportLogoLeft1
            // 
            this.xrpicReportLogoLeft1.LocationFloat = new DevExpress.Utils.PointFloat(2F, 17F);
            this.xrpicReportLogoLeft1.Name = "xrpicReportLogoLeft1";
            this.xrpicReportLogoLeft1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 1, 0, 100F);
            this.xrpicReportLogoLeft1.SizeF = new System.Drawing.SizeF(70F, 70F);
            this.xrpicReportLogoLeft1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrpicReportLogoLeft1.StylePriority.UsePadding = false;
            // 
            // xrlblInstituteAddress
            // 
            this.xrlblInstituteAddress.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrlblInstituteAddress.LocationFloat = new DevExpress.Utils.PointFloat(73F, 29.24999F);
            this.xrlblInstituteAddress.Name = "xrlblInstituteAddress";
            this.xrlblInstituteAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblInstituteAddress.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblInstituteAddress.SizeF = new System.Drawing.SizeF(675.7999F, 29.08332F);
            this.xrlblInstituteAddress.StylePriority.UseFont = false;
            this.xrlblInstituteAddress.StylePriority.UseTextAlignment = false;
            this.xrlblInstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrlblInstituteName
            // 
            this.xrlblInstituteName.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.xrlblInstituteName.LocationFloat = new DevExpress.Utils.PointFloat(73F, 0F);
            this.xrlblInstituteName.Name = "xrlblInstituteName";
            this.xrlblInstituteName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrlblInstituteName.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.xrlblInstituteName.SizeF = new System.Drawing.SizeF(675.7999F, 29.25F);
            this.xrlblInstituteName.StylePriority.UseFont = false;
            this.xrlblInstituteName.StylePriority.UseTextAlignment = false;
            this.xrlblInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrReportTitle
            // 
            this.xrReportTitle.Font = new System.Drawing.Font("Tahoma", 12F);
            this.xrReportTitle.LocationFloat = new DevExpress.Utils.PointFloat(73F, 86.4583F);
            this.xrReportTitle.Name = "xrReportTitle";
            this.xrReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrReportTitle.SizeF = new System.Drawing.SizeF(676.7968F, 28.12501F);
            this.xrReportTitle.StylePriority.UseFont = false;
            this.xrReportTitle.StylePriority.UsePadding = false;
            this.xrReportTitle.StylePriority.UseTextAlignment = false;
            this.xrReportTitle.Text = "Payment Voucher";
            this.xrReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrHeaderProjectName
            // 
            this.xrHeaderProjectName.CanShrink = true;
            this.xrHeaderProjectName.Font = new System.Drawing.Font("Tahoma", 13F);
            this.xrHeaderProjectName.ForeColor = System.Drawing.Color.Black;
            this.xrHeaderProjectName.LocationFloat = new DevExpress.Utils.PointFloat(73F, 58.3333F);
            this.xrHeaderProjectName.Multiline = true;
            this.xrHeaderProjectName.Name = "xrHeaderProjectName";
            this.xrHeaderProjectName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrHeaderProjectName.SizeF = new System.Drawing.SizeF(675.7999F, 28.125F);
            this.xrHeaderProjectName.StylePriority.UseFont = false;
            this.xrHeaderProjectName.StylePriority.UseForeColor = false;
            this.xrHeaderProjectName.StylePriority.UsePadding = false;
            this.xrHeaderProjectName.StylePriority.UseTextAlignment = false;
            this.xrHeaderProjectName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
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
            this.xrlblInstituteAddress,
            this.xrlblInstituteName,
            this.xrpicReportLogoLeft1,
            this.xrHeaderProjectName,
            this.xrReportTitle});
            this.grpHeaderVoucher.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("VOUCHER_DATE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("VOUCHER_NO", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.grpHeaderVoucher.HeightF = 167.5833F;
            this.grpHeaderVoucher.Name = "grpHeaderVoucher";
            // 
            // xrTblHeader
            // 
            this.xrTblHeader.BorderColor = System.Drawing.Color.Silver;
            this.xrTblHeader.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTblHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblHeader.BorderWidth = 1F;
            this.xrTblHeader.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblHeader.ForeColor = System.Drawing.Color.Black;
            this.xrTblHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 117.5833F);
            this.xrTblHeader.Name = "xrTblHeader";
            this.xrTblHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow10});
            this.xrTblHeader.SizeF = new System.Drawing.SizeF(757F, 50F);
            this.xrTblHeader.StylePriority.UseBorderColor = false;
            this.xrTblHeader.StylePriority.UseBorderDashStyle = false;
            this.xrTblHeader.StylePriority.UseBorders = false;
            this.xrTblHeader.StylePriority.UseBorderWidth = false;
            this.xrTblHeader.StylePriority.UseFont = false;
            this.xrTblHeader.StylePriority.UseForeColor = false;
            this.xrTblHeader.StylePriority.UsePadding = false;
            this.xrTblHeader.StylePriority.UseTextAlignment = false;
            this.xrTblHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
            this.xrTableRow1.Weight = 11.5D;
            // 
            // lblVoucherNoCaption
            // 
            this.lblVoucherNoCaption.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.lblVoucherNoCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblVoucherNoCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherNoCaption.Name = "lblVoucherNoCaption";
            this.lblVoucherNoCaption.StylePriority.UseBorderDashStyle = false;
            this.lblVoucherNoCaption.StylePriority.UseBorders = false;
            this.lblVoucherNoCaption.StylePriority.UseFont = false;
            this.lblVoucherNoCaption.Text = "Voucher No";
            this.lblVoucherNoCaption.Weight = 0.80431079942963479D;
            // 
            // lblVoucherNo
            // 
            this.lblVoucherNo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.VOUCHER_NO")});
            this.lblVoucherNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherNo.Name = "lblVoucherNo";
            this.lblVoucherNo.StylePriority.UseFont = false;
            this.lblVoucherNo.Text = "xrtblVoucherNoVal";
            this.lblVoucherNo.Weight = 3.0941849835804787D;
            this.lblVoucherNo.EvaluateBinding += new DevExpress.XtraReports.UI.BindingEventHandler(this.lblVoucherNo_EvaluateBinding);
            // 
            // lblVoucherDateCaption
            // 
            this.lblVoucherDateCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherDateCaption.Name = "lblVoucherDateCaption";
            this.lblVoucherDateCaption.StylePriority.UseFont = false;
            this.lblVoucherDateCaption.StylePriority.UseTextAlignment = false;
            this.lblVoucherDateCaption.Text = "Date";
            this.lblVoucherDateCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblVoucherDateCaption.Weight = 0.29195130714877721D;
            // 
            // lblVoucherDate
            // 
            this.lblVoucherDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.VOUCHER_DATE", "{0:d}")});
            this.lblVoucherDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherDate.Name = "lblVoucherDate";
            this.lblVoucherDate.StylePriority.UseFont = false;
            this.lblVoucherDate.StylePriority.UseTextAlignment = false;
            this.lblVoucherDate.Text = "xrtblAmt";
            this.lblVoucherDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblVoucherDate.Weight = 0.62338536334010375D;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblParticularsCaption,
            this.xrtblCreditAmt});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow10.Weight = 11.5D;
            // 
            // lblParticularsCaption
            // 
            this.lblParticularsCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblParticularsCaption.Name = "lblParticularsCaption";
            this.lblParticularsCaption.StylePriority.UseFont = false;
            this.lblParticularsCaption.StylePriority.UseTextAlignment = false;
            this.lblParticularsCaption.Text = "Particulars";
            this.lblParticularsCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblParticularsCaption.Weight = 3.8984960295369904D;
            // 
            // xrtblCreditAmt
            // 
            this.xrtblCreditAmt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrtblCreditAmt.Name = "xrtblCreditAmt";
            this.xrtblCreditAmt.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 5, 3, 3, 100F);
            this.xrtblCreditAmt.StylePriority.UseFont = false;
            this.xrtblCreditAmt.StylePriority.UsePadding = false;
            this.xrtblCreditAmt.StylePriority.UseTextAlignment = false;
            this.xrtblCreditAmt.Text = " Amount";
            this.xrtblCreditAmt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrtblCreditAmt.Weight = 0.91533642396200454D;
            this.xrtblCreditAmt.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrtblCreditAmt_BeforePrint);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubSignFooter,
            this.xrDuplicatePageBreak,
            this.xrlnDuplicatedDot,
            this.xrDuplicateCopy,
            this.xrTblRequireSignNote,
            this.xrTable1,
            this.xrCashBankRecPayDetails,
            this.xrTblGeneralNarration,
            this.xrVouhcerPageBreak});
            this.GroupFooter1.HeightF = 259.3333F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter1_BeforePrint);
            // 
            // xrSubSignFooter
            // 
            this.xrSubSignFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 124.0416F);
            this.xrSubSignFooter.Name = "xrSubSignFooter";
            this.xrSubSignFooter.ReportSource = new Bosco.Report.ReportObject.SignReportFooter();
            this.xrSubSignFooter.SizeF = new System.Drawing.SizeF(757.0001F, 55.29169F);
            // 
            // xrDuplicatePageBreak
            // 
            this.xrDuplicatePageBreak.LocationFloat = new DevExpress.Utils.PointFloat(0F, 229.9167F);
            this.xrDuplicatePageBreak.Name = "xrDuplicatePageBreak";
            this.xrDuplicatePageBreak.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrDuplicatePageBreak_BeforePrint);
            // 
            // xrlnDuplicatedDot
            // 
            this.xrlnDuplicatedDot.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.xrlnDuplicatedDot.LineWidth = 2;
            this.xrlnDuplicatedDot.LocationFloat = new DevExpress.Utils.PointFloat(0F, 221.2533F);
            this.xrlnDuplicatedDot.Name = "xrlnDuplicatedDot";
            this.xrlnDuplicatedDot.SizeF = new System.Drawing.SizeF(753.9584F, 2.083344F);
            this.xrlnDuplicatedDot.StylePriority.UseForeColor = false;
            this.xrlnDuplicatedDot.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrlnDuplicatedDot_BeforePrint);
            // 
            // xrDuplicateCopy
            // 
            this.xrDuplicateCopy.LocationFloat = new DevExpress.Utils.PointFloat(2.004822F, 233.3333F);
            this.xrDuplicateCopy.Name = "xrDuplicateCopy";
            this.xrDuplicateCopy.ReportSource = new Bosco.Report.ReportObject.CashBankPaymentsSub();
            this.xrDuplicateCopy.SizeF = new System.Drawing.SizeF(754.9953F, 23F);
            this.xrDuplicateCopy.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrDuplicateCopy_BeforePrint);
            // 
            // xrTblRequireSignNote
            // 
            this.xrTblRequireSignNote.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTblRequireSignNote.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTblRequireSignNote.LocationFloat = new DevExpress.Utils.PointFloat(1.999982F, 197.3366F);
            this.xrTblRequireSignNote.Name = "xrTblRequireSignNote";
            this.xrTblRequireSignNote.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow42});
            this.xrTblRequireSignNote.SizeF = new System.Drawing.SizeF(755.0001F, 15F);
            this.xrTblRequireSignNote.StylePriority.UseBorders = false;
            this.xrTblRequireSignNote.StylePriority.UseFont = false;
            this.xrTblRequireSignNote.StylePriority.UseTextAlignment = false;
            this.xrTblRequireSignNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTblRequireSignNote.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblRequireSignNote_BeforePrint);
            // 
            // xrTableRow42
            // 
            this.xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCellRequireSignNote});
            this.xrTableRow42.Name = "xrTableRow42";
            this.xrTableRow42.Weight = 1.7647001649235097D;
            // 
            // xrCellRequireSignNote
            // 
            this.xrCellRequireSignNote.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrCellRequireSignNote.Name = "xrCellRequireSignNote";
            this.xrCellRequireSignNote.StylePriority.UseFont = false;
            this.xrCellRequireSignNote.StylePriority.UseTextAlignment = false;
            this.xrCellRequireSignNote.Text = "This is an electronically produced document and does not require any signature";
            this.xrCellRequireSignNote.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrCellRequireSignNote.Weight = 7.3802825927734395D;
            // 
            // xrTable1
            // 
            this.xrTable1.BorderColor = System.Drawing.Color.Silver;
            this.xrTable1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.BorderWidth = 1F;
            this.xrTable1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTable1.ForeColor = System.Drawing.Color.Black;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow2,
            this.xrTableRow12});
            this.xrTable1.SizeF = new System.Drawing.SizeF(757F, 75F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorderDashStyle = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseBorderWidth = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseForeColor = false;
            this.xrTable1.StylePriority.UsePadding = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrlblTotal,
            this.xrcellSum});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 11.5D;
            // 
            // xrlblTotal
            // 
            this.xrlblTotal.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrlblTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblTotal.Name = "xrlblTotal";
            this.xrlblTotal.StylePriority.UseBorderDashStyle = false;
            this.xrlblTotal.StylePriority.UseFont = false;
            this.xrlblTotal.StylePriority.UseTextAlignment = false;
            this.xrlblTotal.Text = "Total";
            this.xrlblTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrlblTotal.Weight = 3.8873250790489542D;
            // 
            // xrcellSum
            // 
            this.xrcellSum.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrcellSum.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.AMOUNT")});
            this.xrcellSum.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrcellSum.Name = "xrcellSum";
            this.xrcellSum.StylePriority.UseBorderDashStyle = false;
            this.xrcellSum.StylePriority.UseFont = false;
            this.xrcellSum.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrcellSum.Summary = xrSummary1;
            this.xrcellSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrcellSum.Weight = 0.91271347865877628D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblReceivedFromCaption,
            this.lblReceivedFrom});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow2.Weight = 11.5D;
            // 
            // lblReceivedFromCaption
            // 
            this.lblReceivedFromCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceivedFromCaption.Name = "lblReceivedFromCaption";
            this.lblReceivedFromCaption.StylePriority.UseFont = false;
            this.lblReceivedFromCaption.Text = "Paid to ";
            this.lblReceivedFromCaption.Weight = 0.81163136716153972D;
            // 
            // lblReceivedFrom
            // 
            this.lblReceivedFrom.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.NAME_ADDRESS")});
            this.lblReceivedFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceivedFrom.Name = "lblReceivedFrom";
            this.lblReceivedFrom.StylePriority.UseFont = false;
            this.lblReceivedFrom.Weight = 3.9884071905461909D;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.lblSumCaption,
            this.xrtblAmtInWords});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow12.Weight = 11.5D;
            // 
            // lblSumCaption
            // 
            this.lblSumCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblSumCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSumCaption.Name = "lblSumCaption";
            this.lblSumCaption.StylePriority.UseBorders = false;
            this.lblSumCaption.StylePriority.UseFont = false;
            this.lblSumCaption.Text = "Amount in Words";
            this.lblSumCaption.Weight = 0.81163136091727139D;
            // 
            // xrtblAmtInWords
            // 
            this.xrtblAmtInWords.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtblAmtInWords.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.RUPPEE_AMT")});
            this.xrtblAmtInWords.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrtblAmtInWords.Name = "xrtblAmtInWords";
            this.xrtblAmtInWords.StylePriority.UseBorders = false;
            this.xrtblAmtInWords.StylePriority.UseFont = false;
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrtblAmtInWords.Summary = xrSummary2;
            this.xrtblAmtInWords.Text = "xrtblAmtInWords";
            this.xrtblAmtInWords.Weight = 3.9884071967904582D;
            this.xrtblAmtInWords.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrtblAmtInWords_SummaryGetResult_1);
            this.xrtblAmtInWords.SummaryReset += new System.EventHandler(this.xrtblAmtInWords_SummaryReset_1);
            this.xrtblAmtInWords.SummaryRowChanged += new System.EventHandler(this.xrtblAmtInWords_SummaryRowChanged_1);
            // 
            // xrCashBankRecPayDetails
            // 
            this.xrCashBankRecPayDetails.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrCashBankRecPayDetails.Name = "xrCashBankRecPayDetails";
            this.xrCashBankRecPayDetails.ReportSource = new Bosco.Report.ReportObject.CashBankReceiptPaymentDetails();
            this.xrCashBankRecPayDetails.SizeF = new System.Drawing.SizeF(757F, 23F);
            this.xrCashBankRecPayDetails.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrCashBankRecPayDetails_BeforePrint);
            // 
            // xrTblGeneralNarration
            // 
            this.xrTblGeneralNarration.BorderColor = System.Drawing.Color.Silver;
            this.xrTblGeneralNarration.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTblGeneralNarration.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblGeneralNarration.BorderWidth = 1F;
            this.xrTblGeneralNarration.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblGeneralNarration.ForeColor = System.Drawing.Color.Black;
            this.xrTblGeneralNarration.LocationFloat = new DevExpress.Utils.PointFloat(0F, 98.04161F);
            this.xrTblGeneralNarration.Name = "xrTblGeneralNarration";
            this.xrTblGeneralNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblGeneralNarration.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.xrTblGeneralNarration.SizeF = new System.Drawing.SizeF(757F, 25F);
            this.xrTblGeneralNarration.StylePriority.UseBorderColor = false;
            this.xrTblGeneralNarration.StylePriority.UseBorderDashStyle = false;
            this.xrTblGeneralNarration.StylePriority.UseBorders = false;
            this.xrTblGeneralNarration.StylePriority.UseBorderWidth = false;
            this.xrTblGeneralNarration.StylePriority.UseFont = false;
            this.xrTblGeneralNarration.StylePriority.UseForeColor = false;
            this.xrTblGeneralNarration.StylePriority.UsePadding = false;
            this.xrTblGeneralNarration.StylePriority.UseTextAlignment = false;
            this.xrTblGeneralNarration.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
            this.xrTableRow7.Weight = 11.5D;
            // 
            // lblNarrationCaption
            // 
            this.lblNarrationCaption.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblNarrationCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNarrationCaption.Name = "lblNarrationCaption";
            this.lblNarrationCaption.StylePriority.UseBorders = false;
            this.lblNarrationCaption.StylePriority.UseFont = false;
            this.lblNarrationCaption.Text = "Being";
            this.lblNarrationCaption.Weight = 0.81396359802545792D;
            // 
            // lblNarration
            // 
            this.lblNarration.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.GENERAL_NARRATION")});
            this.lblNarration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.StylePriority.UseBorders = false;
            this.lblNarration.StylePriority.UseFont = false;
            this.lblNarration.Weight = 3.9998680427127726D;
            // 
            // xrVouhcerPageBreak
            // 
            this.xrVouhcerPageBreak.LocationFloat = new DevExpress.Utils.PointFloat(0F, 257.3333F);
            this.xrVouhcerPageBreak.Name = "xrVouhcerPageBreak";
            // 
            // xrTblLedgerDetails
            // 
            this.xrTblLedgerDetails.BorderColor = System.Drawing.Color.Silver;
            this.xrTblLedgerDetails.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrTblLedgerDetails.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTblLedgerDetails.BorderWidth = 1F;
            this.xrTblLedgerDetails.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrTblLedgerDetails.ForeColor = System.Drawing.Color.Black;
            this.xrTblLedgerDetails.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTblLedgerDetails.Name = "xrTblLedgerDetails";
            this.xrTblLedgerDetails.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrTblLedgerDetails.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11,
            this.xrRowLedgerNarration});
            this.xrTblLedgerDetails.SizeF = new System.Drawing.SizeF(757F, 40F);
            this.xrTblLedgerDetails.StyleName = "styleRow";
            this.xrTblLedgerDetails.StylePriority.UseBorderColor = false;
            this.xrTblLedgerDetails.StylePriority.UseBorderDashStyle = false;
            this.xrTblLedgerDetails.StylePriority.UseBorders = false;
            this.xrTblLedgerDetails.StylePriority.UseBorderWidth = false;
            this.xrTblLedgerDetails.StylePriority.UseFont = false;
            this.xrTblLedgerDetails.StylePriority.UseForeColor = false;
            this.xrTblLedgerDetails.StylePriority.UsePadding = false;
            this.xrTblLedgerDetails.StylePriority.UseTextAlignment = false;
            this.xrTblLedgerDetails.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTblLedgerDetails.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTblLedgerDetails_BeforePrint);
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellLedgerName,
            this.xrcellLedgerAmount});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.StylePriority.UseBorderDashStyle = false;
            this.xrTableRow11.Weight = 11.5D;
            // 
            // xrcellLedgerName
            // 
            this.xrcellLedgerName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.LEDGER_NAME")});
            this.xrcellLedgerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcellLedgerName.Name = "xrcellLedgerName";
            this.xrcellLedgerName.StylePriority.UseFont = false;
            this.xrcellLedgerName.Text = "xrcellLedgerName";
            this.xrcellLedgerName.Weight = 3.6502332603997276D;
            // 
            // xrcellLedgerAmount
            // 
            this.xrcellLedgerAmount.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.AMOUNT", "{0:n}")});
            this.xrcellLedgerAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcellLedgerAmount.Name = "xrcellLedgerAmount";
            this.xrcellLedgerAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 5, 3, 3, 100F);
            this.xrcellLedgerAmount.StylePriority.UseFont = false;
            this.xrcellLedgerAmount.StylePriority.UsePadding = false;
            this.xrcellLedgerAmount.StylePriority.UseTextAlignment = false;
            this.xrcellLedgerAmount.Text = "xrtblProFundCode";
            this.xrcellLedgerAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrcellLedgerAmount.Weight = 0.85704620844750279D;
            this.xrcellLedgerAmount.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xttblLedgerAmount_BeforePrint);
            // 
            // xrRowLedgerNarration
            // 
            this.xrRowLedgerNarration.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrcellLedgerNarration,
            this.xrTableCell2});
            this.xrRowLedgerNarration.Name = "xrRowLedgerNarration";
            this.xrRowLedgerNarration.Weight = 6.8999999999999995D;
            // 
            // xrcellLedgerNarration
            // 
            this.xrcellLedgerNarration.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrcellLedgerNarration.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CashBankReceipts.NARRATION")});
            this.xrcellLedgerNarration.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrcellLedgerNarration.Name = "xrcellLedgerNarration";
            this.xrcellLedgerNarration.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 3, 3, 3, 100F);
            this.xrcellLedgerNarration.StylePriority.UseBorderDashStyle = false;
            this.xrcellLedgerNarration.StylePriority.UseFont = false;
            this.xrcellLedgerNarration.StylePriority.UsePadding = false;
            this.xrcellLedgerNarration.Weight = 3.6502332603997276D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorderDashStyle = false;
            this.xrTableCell2.Weight = 0.85704620844750279D;
            // 
            // xrCrossBandBox2
            // 
            this.xrCrossBandBox2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCrossBandBox2.BorderWidth = 1F;
            this.xrCrossBandBox2.EndBand = this.GroupFooter1;
            this.xrCrossBandBox2.EndPointFloat = new DevExpress.Utils.PointFloat(612F, 25F);
            this.xrCrossBandBox2.LocationFloat = new DevExpress.Utils.PointFloat(612F, 1F);
            this.xrCrossBandBox2.Name = "xrCrossBandBox2";
            this.xrCrossBandBox2.StartBand = this.GroupFooter1;
            this.xrCrossBandBox2.StartPointFloat = new DevExpress.Utils.PointFloat(612F, 1F);
            this.xrCrossBandBox2.WidthF = 143F;
            // 
            // xrSubreportCCDetails
            // 
            this.xrSubreportCCDetails.CanShrink = true;
            this.xrSubreportCCDetails.LocationFloat = new DevExpress.Utils.PointFloat(0F, 40F);
            this.xrSubreportCCDetails.Name = "xrSubreportCCDetails";
            this.xrSubreportCCDetails.ReportSource = new Bosco.Report.ReportObject.UcCCDetail();
            this.xrSubreportCCDetails.SizeF = new System.Drawing.SizeF(613.0587F, 27.12501F);
            this.xrSubreportCCDetails.Visible = false;
            // 
            // CashBankPayments
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
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Margins = new System.Drawing.Printing.Margins(48, 12, 20, 20);
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
        private DevExpress.XtraReports.UI.XRTableCell xrcellLedgerName;
        private DevExpress.XtraReports.UI.XRTableCell xrcellLedgerAmount;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell lblReceivedFromCaption;
        private DevExpress.XtraReports.UI.XRTableCell lblReceivedFrom;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        private DevExpress.XtraReports.UI.XRTableCell lblSumCaption;
        private DevExpress.XtraReports.UI.XRTableCell xrtblAmtInWords;
        private DevExpress.XtraReports.UI.XRLabel xrlblInstituteAddress;
        private DevExpress.XtraReports.UI.XRLabel xrlblInstituteName;
        private DevExpress.XtraReports.UI.XRPageBreak xrVouhcerPageBreak;
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
        private DevExpress.XtraReports.UI.XRSubreport xrDuplicateCopy;
        private DevExpress.XtraReports.UI.XRLine xrlnDuplicatedDot;
        private DevExpress.XtraReports.UI.XRPageBreak xrDuplicatePageBreak;
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
