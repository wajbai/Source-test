namespace Bosco.Report.ReportObject
{
    partial class ChequePrint
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
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrlblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblParty = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblAmountInWords = new DevExpress.XtraReports.UI.XRLabel();
            this.xrlblAmount = new DevExpress.XtraReports.UI.XRLabel();
            this.lblD1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblD2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblM1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblM2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblY1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblY2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblY3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblY4 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblY1,
            this.lblM2,
            this.lblM1,
            this.lblY4,
            this.lblY3,
            this.lblY2,
            this.xrlblAmountInWords,
            this.xrlblParty,
            this.xrlblDate,
            this.lblD2,
            this.lblD1,
            this.xrlblAmount});
            this.Detail.HeightF = 86.45834F;
            this.Detail.Visible = true;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.drChequePrinting_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrlblDate
            // 
            this.xrlblDate.AutoWidth = true;
            this.xrlblDate.LocationFloat = new DevExpress.Utils.PointFloat(463.2822F, 10F);
            this.xrlblDate.Name = "xrlblDate";
            this.xrlblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblDate.SizeF = new System.Drawing.SizeF(44.79163F, 23F);
            this.xrlblDate.StylePriority.UsePadding = false;
            this.xrlblDate.StylePriority.UseTextAlignment = false;
            this.xrlblDate.Text = "Date";
            this.xrlblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrlblDate.WordWrap = false;
            // 
            // xrlblParty
            // 
            this.xrlblParty.AutoWidth = true;
            this.xrlblParty.LocationFloat = new DevExpress.Utils.PointFloat(42.49982F, 10F);
            this.xrlblParty.Name = "xrlblParty";
            this.xrlblParty.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblParty.SizeF = new System.Drawing.SizeF(210.4167F, 23F);
            this.xrlblParty.StylePriority.UsePadding = false;
            this.xrlblParty.StylePriority.UseTextAlignment = false;
            this.xrlblParty.Text = "Name of the party";
            this.xrlblParty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrlblParty.WordWrap = false;
            // 
            // xrlblAmountInWords
            // 
            this.xrlblAmountInWords.AutoWidth = true;
            this.xrlblAmountInWords.LocationFloat = new DevExpress.Utils.PointFloat(42.49982F, 54.79168F);
            this.xrlblAmountInWords.Name = "xrlblAmountInWords";
            this.xrlblAmountInWords.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblAmountInWords.SizeF = new System.Drawing.SizeF(210.4167F, 23.00001F);
            this.xrlblAmountInWords.StylePriority.UsePadding = false;
            this.xrlblAmountInWords.StylePriority.UseTextAlignment = false;
            this.xrlblAmountInWords.Text = "Amount in words";
            this.xrlblAmountInWords.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrlblAmountInWords.WordWrap = false;
            // 
            // xrlblAmount
            // 
            this.xrlblAmount.AutoWidth = true;
            this.xrlblAmount.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblAmount.LocationFloat = new DevExpress.Utils.PointFloat(411.1987F, 45.41668F);
            this.xrlblAmount.Name = "xrlblAmount";
            this.xrlblAmount.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrlblAmount.SizeF = new System.Drawing.SizeF(110F, 23.00001F);
            this.xrlblAmount.StylePriority.UseFont = false;
            this.xrlblAmount.StylePriority.UsePadding = false;
            this.xrlblAmount.StylePriority.UseTextAlignment = false;
            this.xrlblAmount.Text = "Amount";
            this.xrlblAmount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrlblAmount.WordWrap = false;
            // 
            // lblD1
            // 
            this.lblD1.AutoWidth = true;
            this.lblD1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblD1.BorderWidth = 1F;
            this.lblD1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblD1.LocationFloat = new DevExpress.Utils.PointFloat(514.7081F, 10F);
            this.lblD1.Name = "lblD1";
            this.lblD1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblD1.SizeF = new System.Drawing.SizeF(10F, 23F);
            this.lblD1.StylePriority.UseBorders = false;
            this.lblD1.StylePriority.UseBorderWidth = false;
            this.lblD1.StylePriority.UseFont = false;
            this.lblD1.StylePriority.UsePadding = false;
            this.lblD1.StylePriority.UseTextAlignment = false;
            this.lblD1.Text = "1";
            this.lblD1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblD1.WordWrap = false;
            // 
            // lblD2
            // 
            this.lblD2.AutoWidth = true;
            this.lblD2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblD2.BorderWidth = 1F;
            this.lblD2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblD2.LocationFloat = new DevExpress.Utils.PointFloat(526.7081F, 10F);
            this.lblD2.Name = "lblD2";
            this.lblD2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblD2.SizeF = new System.Drawing.SizeF(10F, 23F);
            this.lblD2.StylePriority.UseBorders = false;
            this.lblD2.StylePriority.UseBorderWidth = false;
            this.lblD2.StylePriority.UseFont = false;
            this.lblD2.StylePriority.UsePadding = false;
            this.lblD2.StylePriority.UseTextAlignment = false;
            this.lblD2.Text = "9";
            this.lblD2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblD2.WordWrap = false;
            // 
            // lblM1
            // 
            this.lblM1.AutoWidth = true;
            this.lblM1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblM1.BorderWidth = 1F;
            this.lblM1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblM1.LocationFloat = new DevExpress.Utils.PointFloat(538.7081F, 10F);
            this.lblM1.Name = "lblM1";
            this.lblM1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblM1.SizeF = new System.Drawing.SizeF(12F, 23F);
            this.lblM1.StylePriority.UseBorders = false;
            this.lblM1.StylePriority.UseBorderWidth = false;
            this.lblM1.StylePriority.UseFont = false;
            this.lblM1.StylePriority.UsePadding = false;
            this.lblM1.StylePriority.UseTextAlignment = false;
            this.lblM1.Text = "0";
            this.lblM1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblM1.WordWrap = false;
            // 
            // lblM2
            // 
            this.lblM2.AutoWidth = true;
            this.lblM2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblM2.BorderWidth = 1F;
            this.lblM2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblM2.LocationFloat = new DevExpress.Utils.PointFloat(550.7081F, 10F);
            this.lblM2.Name = "lblM2";
            this.lblM2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblM2.SizeF = new System.Drawing.SizeF(10F, 23F);
            this.lblM2.StylePriority.UseBorders = false;
            this.lblM2.StylePriority.UseBorderWidth = false;
            this.lblM2.StylePriority.UseFont = false;
            this.lblM2.StylePriority.UsePadding = false;
            this.lblM2.StylePriority.UseTextAlignment = false;
            this.lblM2.Text = "2";
            this.lblM2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblM2.WordWrap = false;
            // 
            // lblY1
            // 
            this.lblY1.AutoWidth = true;
            this.lblY1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblY1.BorderWidth = 1F;
            this.lblY1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY1.LocationFloat = new DevExpress.Utils.PointFloat(562.7081F, 10F);
            this.lblY1.Name = "lblY1";
            this.lblY1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblY1.SizeF = new System.Drawing.SizeF(10F, 23F);
            this.lblY1.StylePriority.UseBorders = false;
            this.lblY1.StylePriority.UseBorderWidth = false;
            this.lblY1.StylePriority.UseFont = false;
            this.lblY1.StylePriority.UsePadding = false;
            this.lblY1.StylePriority.UseTextAlignment = false;
            this.lblY1.Text = "2";
            this.lblY1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblY1.WordWrap = false;
            // 
            // lblY2
            // 
            this.lblY2.AutoWidth = true;
            this.lblY2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblY2.BorderWidth = 1F;
            this.lblY2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY2.LocationFloat = new DevExpress.Utils.PointFloat(574.7081F, 10F);
            this.lblY2.Name = "lblY2";
            this.lblY2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblY2.SizeF = new System.Drawing.SizeF(10F, 23F);
            this.lblY2.StylePriority.UseBorders = false;
            this.lblY2.StylePriority.UseBorderWidth = false;
            this.lblY2.StylePriority.UseFont = false;
            this.lblY2.StylePriority.UsePadding = false;
            this.lblY2.StylePriority.UseTextAlignment = false;
            this.lblY2.Text = "0";
            this.lblY2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblY2.WordWrap = false;
            // 
            // lblY3
            // 
            this.lblY3.AutoWidth = true;
            this.lblY3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblY3.BorderWidth = 1F;
            this.lblY3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY3.LocationFloat = new DevExpress.Utils.PointFloat(586.7081F, 10F);
            this.lblY3.Name = "lblY3";
            this.lblY3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblY3.SizeF = new System.Drawing.SizeF(10F, 23F);
            this.lblY3.StylePriority.UseBorders = false;
            this.lblY3.StylePriority.UseBorderWidth = false;
            this.lblY3.StylePriority.UseFont = false;
            this.lblY3.StylePriority.UsePadding = false;
            this.lblY3.StylePriority.UseTextAlignment = false;
            this.lblY3.Text = "1";
            this.lblY3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblY3.WordWrap = false;
            // 
            // lblY4
            // 
            this.lblY4.AutoWidth = true;
            this.lblY4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblY4.BorderWidth = 1F;
            this.lblY4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY4.LocationFloat = new DevExpress.Utils.PointFloat(598.7081F, 10F);
            this.lblY4.Name = "lblY4";
            this.lblY4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lblY4.SizeF = new System.Drawing.SizeF(10F, 23F);
            this.lblY4.StylePriority.UseBorders = false;
            this.lblY4.StylePriority.UseBorderWidth = false;
            this.lblY4.StylePriority.UseFont = false;
            this.lblY4.StylePriority.UsePadding = false;
            this.lblY4.StylePriority.UseTextAlignment = false;
            this.lblY4.Text = "7";
            this.lblY4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblY4.WordWrap = false;
            // 
            // ChequePrint
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail});
            this.BorderWidth = 0F;
            this.DefaultPrinterSettingsUsing.UseLandscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 20, 20);
            this.PageHeight = 380;
            this.PageWidth = 800;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel lblY1;
        private DevExpress.XtraReports.UI.XRLabel lblM2;
        private DevExpress.XtraReports.UI.XRLabel lblM1;
        private DevExpress.XtraReports.UI.XRLabel lblY4;
        private DevExpress.XtraReports.UI.XRLabel lblY3;
        private DevExpress.XtraReports.UI.XRLabel lblY2;
        private DevExpress.XtraReports.UI.XRLabel xrlblAmountInWords;
        private DevExpress.XtraReports.UI.XRLabel xrlblParty;
        private DevExpress.XtraReports.UI.XRLabel xrlblDate;
        private DevExpress.XtraReports.UI.XRLabel lblD2;
        private DevExpress.XtraReports.UI.XRLabel lblD1;
        private DevExpress.XtraReports.UI.XRLabel xrlblAmount;
    }
}
