using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.Report.Base;
using System.Data;
using Bosco.Utility.ConfigSetting;
using System.IO;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.HtmlExport.Controls;
using DevExpress.XtraPrinting.HtmlExport;
//using DevExpress.XtraRichEdit.API.Word;
//using DevExpress.XtraRichEdit.API.Native;

namespace Bosco.Report.ReportObject
{
    public partial class SignReportFooter : ReportBase
    {
        bool IsVoucerReceiptsPrint = false;
        SettingProperty settingProperty = new SettingProperty();
        public float SignWidth
        {
            set {
                float reportwidth = value;
                this.xrTblSingFooter.WidthF = reportwidth;
                this.xrTblSingFooter1.WidthF = reportwidth;
                this.lblSignNoteAbove.WidthF = reportwidth;
                this.lblSignNoteBelow.WidthF = reportwidth;

                //this.xrrtxAuditorSignNote.LeftF = 0;
                //this.xrrtxAuditorSignNote.WidthF = reportwidth;
                this.xrTblAuditorNoteSign.WidthF = reportwidth;
                //lblSign1.WidthF = lblSign2.WidthF  = lblSign3.WidthF = (reportwidth / 3);
                //lblRole1.WidthF = lblRole2.WidthF = lblRole3.WidthF = (reportwidth / 3);
                //lblRoleName1.WidthF = lblRoleName2.WidthF = lblRoleName3.WidthF = (reportwidth / 3);
            }
        }

        public bool KeepExtractHeight
        {
             get; set; 
        }

              
        /// <summary>
        /// 17/09/2020, Hide Role2 for CMF Congregation
        /// </summary>
        private bool hideRole2;
        public bool HideRole2
        {
            set
            {
                hideRole2 = value;
            }
        }

        public float Role2Width
        {
            set
            {
                lblSign2.WidthF=  lblName2.WidthF = lblRole2.WidthF  =value;
                lblSignEmpty.WidthF = lblNameEmpty.WidthF = lblRoleEmpty.WidthF = value;
                xrpicSign2.WidthF = value;
            }
            get
            {
                return lblSign2.WidthF;
            }
        }

        /// <summary>
        /// 17/09/2020, CMF - Show Approved Sign
        /// </summary>
        private bool showApprovedSign;
        public bool ShowApprovedSign
        {
            set
            {
                showApprovedSign = value;
            }
        }


        /// <summary>
        /// On 01/03/2021, assing project/common project details
        /// </summary>
        public Int32 ProjectId
        {
            set
            {
                ReportProperty.Current.AssignSignDetails(value.ToString());
            }
        }

        /// <summary>
        /// On 23/09/2022, to set extract Height of the sign details
        /// </summary>
        private void MakeExtractHeight()
        {
            xrpicSign1.TopF = xrpicSign2.TopF = xrpicSign3.TopF = 8;
            xrRowSing.HeightF = 35;
            xrRowRole.HeightF = 10;
            xrRowName.HeightF = 10;
            Detail.HeightF = 60;

            if (DetailReport.Visible)
            {
                xrpicSign4.TopF = xrpicSign5.TopF = 8;
                xrRowSing1.HeightF =  35;
                xrRowRole1.HeightF = 10;
                xrRowName1.HeightF = 10;
                DetailReport.HeightF = 60;
            }
        }

        public void ShowSignDetails(bool isVoucerReceiptsPrint= false)
        {
            IsVoucerReceiptsPrint = isVoucerReceiptsPrint;
            FillSignDetails();

            //On 02/03/2021, To reduce and fix height only for voucher print
            if (isVoucerReceiptsPrint)
            {
                xrpicSign1.TopF = xrpicSign2.TopF = xrpicSign3.TopF = xrpicSign4.TopF = xrpicSign5.TopF = 30;

                //22/04/2021, For SDBINM to have extra space between Name and Role
                if (settingProperty.IS_SDB_INM)
                {
                    float fontsize = float.Parse("8.75");
                    lblName1.Font = new Font(lblRole1.Font.FontFamily, fontsize, lblRole1.Font.Style);
                    xrRowSing.HeightF = xrRowSing1.HeightF = 2;
                    xrRowRole.HeightF = xrRowRole1.HeightF = 2;
                    xrRowName.HeightF = xrRowName1.HeightF = 5;
                }
                else
                {
                    xrRowSing.HeightF = xrRowSing1.HeightF = 65;
                    xrRowRole.HeightF = xrRowRole1.HeightF = 15;
                    xrRowName.HeightF = xrRowName1.HeightF = 15;
                }

                xrTblSingFooter.HeightF = xrTblSingFooter1.HeightF = 95;
                Detail.HeightF = Detail1.HeightF = 95;
            }
            else if (KeepExtractHeight)
            {
                MakeExtractHeight();
            }
            else
            {   //For Temp purpose on 06/12/2023
                MakeExtractHeight();
            }
        }

        public SignReportFooter()
        {
            InitializeComponent();

            //On 16/03/2022 by default
            xrTblSingFooter.Visible = false;
        }

        private void FillSignDetails()
        {
            //Signatures
            string Name1 = ReportProperty.Current.RoleName1, Role1 = ReportProperty.Current.Role1;
            string Name2 = ReportProperty.Current.RoleName2, Role2 = ReportProperty.Current.Role2;
            string Name3 = ReportProperty.Current.RoleName3, Role3 = ReportProperty.Current.Role3;
            string Name4 = ReportProperty.Current.RoleName4, Role4 = ReportProperty.Current.Role4;
            string Name5 = ReportProperty.Current.RoleName5, Role5 = ReportProperty.Current.Role5;
            xrTblSingFooter.Visible = false;
            GrpHeaderSignNote.Visible = false;
            detailFooterSignNote.Visible = false;
            detailFooterAuditorSignNote.Visible = false;
            lblSignNoteAbove.Text = "";
            lblSignNoteBelow.Text = "";

            try
            {
                if (ReportProperty.Current.IncludeSignDetails == 1)
                {
                    xrTblSingFooter.Visible = (!string.IsNullOrEmpty(Role1) || !string.IsNullOrEmpty(Name1) || ReportProperty.Current.Sign1Image != null
                                               || !string.IsNullOrEmpty(Role2) || !string.IsNullOrEmpty(Name2) || ReportProperty.Current.Sign2Image != null
                                               || !string.IsNullOrEmpty(Role3) || !string.IsNullOrEmpty(Name3) || ReportProperty.Current.Sign3Image != null
                                               || !string.IsNullOrEmpty(Role4) || !string.IsNullOrEmpty(Name4) || ReportProperty.Current.Sign4Image != null
                                               || !string.IsNullOrEmpty(Role5) || !string.IsNullOrEmpty(Name5) || ReportProperty.Current.Sign5Image != null);

                    if (xrTblSingFooter.Visible)
                    {
                        lblSign1.Text = lblSign2.Text = lblSign3.Text = lblSign4.Text = lblSign5.Text  = " ";
                        
                        //Role 1
                        lblName1.Text = lblRole1.Text = string.Empty;
                        if (IncludeSignDetails(1))
                        {
                            lblName1.Text = Name1; lblRole1.Text = Role1;
                        }

                        //Role 2
                        //For CMF 17/09/2020
                        if (!hideRole2)
                        {
                            lblName2.Text = lblRole2.Text = string.Empty;
                            if (IncludeSignDetails(2))
                            {
                                lblName2.Text = Name2; lblRole2.Text = Role2;
                            }
                        }
                        else
                        {
                            lblName2.Text = string.Empty; lblRole2.Text = string.Empty;
                        }

                        //Role 3,4,5
                        lblName3.Text = lblRole3.Text = string.Empty;
                        if (IncludeSignDetails(3))
                        {
                            lblName3.Text = Name3; lblRole3.Text = Role3;
                        }

                        lblName4.Text = Name4; lblRole4.Text = Role4;
                        lblName5.Text = Name5; lblRole5.Text = Role5;

                        //Show Sign Images for Sign1, Sign2 and Sign3
                        //For Sign 1
                        xrpicSign1.Visible = false;
                        //string sign1 =  Path.Combine(AcmeerpInstalledPath,"Sign1.jpg");
                        if (ReportProperty.Current.Sign1Image!=null && IncludeSignDetails(1))
                        {
                            xrpicSign1.Visible = true;
                            xrpicSign1.WidthF = lblSign1.WidthF;
                            //xrpicSign1.Image = Image.FromFile(sign1);
                            xrpicSign1.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign1Image);
                            xrpicSign1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage; //DevExpress.XtraPrinting.ImageSizeMode.CenterImage;    
                        }

                        //For Sign 2
                        xrpicSign2.Visible = false;
                        //string sign2 = Path.Combine(AcmeerpInstalledPath, "Sign2.jpg");
                        //if (File.Exists(sign2))
                        if (ReportProperty.Current.Sign2Image != null && IncludeSignDetails(2))
                        {
                            xrpicSign2.Visible = true;
                            xrpicSign2.WidthF = lblSign2.WidthF;
                            xrpicSign2.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign2Image);
                            xrpicSign2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;// DevExpress.XtraPrinting.ImageSizeMode.CenterImage;
                        }

                        //For Sign 3
                        xrpicSign3.Visible = false;
                        if (ReportProperty.Current.Sign3Image != null && IncludeSignDetails(3))
                        {
                            xrpicSign3.Visible = true;
                            xrpicSign3.WidthF = lblSign3.WidthF;
                            //xrpicSign3.Image = Image.FromFile(sign3);
                            /*using (Bitmap bmp = new Bitmap(sign3))
                            {
                                byte[] signImage = ImageProcessing.ImageToByteArray(bmp);
                                xrpicSign3.Image = ImageProcessing.ByteArrayToImage(signImage);
                            }*/
                            xrpicSign3.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign3Image);
                            xrpicSign3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;// DevExpress.XtraPrinting.ImageSizeMode.CenterImage;
                        }
                    }


                    //On 17/09/2020 for sing 4 and sing 5 for Budget Approval - Sign details
                    //For CMF Congregation
                    lblName4.Text = string.Empty;
                    lblRole4.Text = string.Empty;
                    lblName5.Text = string.Empty;
                    lblRole5.Text = string.Empty;
                    
                    DetailReport.Visible = showApprovedSign;
                    if ((settingProperty.IS_CMF_CONGREGATION || settingProperty.IS_BSG_CONGREGATION ) && showApprovedSign)
                    {
                        DetailReport.Visible = true;
                        lblName4.Text = Name4; 
                        lblRole4.Text = Role4;
                        xrpicSign4.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign4Image);
                        xrpicSign4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage; // DevExpress.XtraPrinting.ImageSizeMode.CenterImage;
                        xrpicSign4.LeftF = 20;
                        xrpicSign4.WidthF = 175;

                        lblName5.Text = Name5; 
                        lblRole5.Text = Role5;
                        
                        xrpicSign5.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign5Image);
                        xrpicSign5.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage; // DevExpress.XtraPrinting.ImageSizeMode.CenterImage;
                        xrpicSign5.LeftF = 20;
                        xrpicSign5.WidthF = 175;

                    }

                    //On 11/05/2022, to show for the society name - Receipt print
                    if (settingProperty.IS_SDB_INM && IsVoucerReceiptsPrint)
                    {
                        xrTblSingFooter.Visible = true;
                        if (string.IsNullOrEmpty(Name1) &&  string.IsNullOrEmpty(Role1))
                        {
                            lblName1.Text = lblRole1.Text = string.Empty;
                        }
                        if (string.IsNullOrEmpty(Name2) && string.IsNullOrEmpty(Role2))
                        {
                            lblName2.Text = lblRole2.Text = string.Empty;
                        }

                        //For the Society Name
                        lblName3.Text = " For " + this.ReportProperties.LegalName;
                        lblRole3.Text = "Authorized Signature";
                    }
                }

                //For Sign Note
                if (!string.IsNullOrEmpty(ReportProperty.Current.SignNote) && !IsVoucerReceiptsPrint)
                {
                    if (ReportProperty.Current.SignNoteLocation == 1)
                    {
                        detailFooterSignNote.Visible = true;
                        lblSignNoteBelow.Text = ReportProperty.Current.SignNote;
                        lblSignNoteBelow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        if (ReportProperty.Current.SignNoteAlignment != 1)
                        {
                            lblSignNoteBelow.TextAlignment = (ReportProperty.Current.SignNoteAlignment == 0) ?
                                DevExpress.XtraPrinting.TextAlignment.MiddleLeft : DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        }
                    }
                    else
                    {
                        GrpHeaderSignNote.Visible = true;
                        lblSignNoteAbove.Text = ReportProperty.Current.SignNote;

                        lblSignNoteAbove.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        if (ReportProperty.Current.SignNoteAlignment != 1)
                        {
                            lblSignNoteAbove.TextAlignment = (ReportProperty.Current.SignNoteAlignment == 0) ?
                                DevExpress.XtraPrinting.TextAlignment.MiddleLeft : DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        }
                    }
                }

                //Auditor Sign Note
                if (ReportProperty.Current.IncludeAuditorSignNote == 1)
                {
                    AssignAuditorNoteSignDetails();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), false);
            }
        }

        private void AssignAuditorNoteSignDetails()
        {
            try
            {
                bool row1visible = false;
                bool row2visible = false;
                bool row3visible = false;

                xrcellAuditorNote.Text = string.Empty;
                xrcellAuditorDate.Text = string.Empty;
                xrcellAuditorPlace.Text = string.Empty;
                xrcellAuditorSign1.Text = string.Empty;
                xrcellAuditorSign2.Text = string.Empty;
                xrcellAuditorSign3.Text = string.Empty;

                //Auditor Sign Note
                if (ReportProperty.Current.IncludeAuditorSignNote == 1)
                {
                    DataTable dtAuditorNoteSignDetails = ReportProperty.Current.dtAuditorNoteSign;
                    foreach (DataRow dr in dtAuditorNoteSignDetails.Rows)
                    {
                        AuditorSignNote SettingName = (AuditorSignNote)Enum.Parse(typeof(AuditorSignNote), dr[reportSetting1.AuditorNoteSignDetails.AUDITOR_NOTE_SETTINGColumn.ColumnName].ToString());
                        string value = dr[reportSetting1.AuditorNoteSignDetails.AUDITOR_NOTE_SETTING_VALUEColumn.ColumnName].ToString().Trim();
                       
                        switch (SettingName)
                        {
                            case AuditorSignNote.AuditorNote:
                                {

                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        xrcellAuditorNote.Text = value;
                                    }
                                    break;
                                }
                            case AuditorSignNote.ShowDate:
                                {
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        if (UtilityMember.DateSet.IsDate(value) && UtilityMember.DateSet.ToDate(value, false).Date != DateTime.MinValue)
                                        {
                                            xrcellAuditorDate.Text = "Date  : " + UtilityMember.DateSet.ToDate(value, false).ToShortDateString();
                                        }
                                    }
                                    break;
                                }
                            case AuditorSignNote.Place:
                                {
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        xrcellAuditorPlace.Text = "Place : " + value;
                                    }
                                    break;
                                }
                            case AuditorSignNote.Sign1:
                                {
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        xrcellAuditorSign1.Text = value;
                                    }
                                    break;
                                }
                            case AuditorSignNote.Sign2:
                                {
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        xrcellAuditorSign2.Text = value;
                                    }
                                    break;
                                }
                            case AuditorSignNote.Sign3:
                                {
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        xrcellAuditorSign3.Text = value;
                                    }
                                    break;
                                }
                        }
                    }
                }

                row1visible = !string.IsNullOrEmpty(xrcellAuditorNote.Text);
                row2visible = true; //(!string.IsNullOrEmpty(xrcellAuditorDate.Text));
                row3visible = (!string.IsNullOrEmpty(xrcellAuditorPlace.Text) || !string.IsNullOrEmpty(xrcellAuditorSign1.Text) || !string.IsNullOrEmpty(xrcellAuditorSign2.Text) ||
                               !string.IsNullOrEmpty(xrcellAuditorSign3.Text));

                xrRowAuditorNote.Visible =  row1visible;
                xrRowAuditorNote1.Visible = row2visible;
                xrRowAuditorNote2.Visible = row3visible;

                //xrTblAuditorNoteSign.Borders = BorderSide.All;
                detailFooterAuditorSignNote.Visible = (row1visible || row2visible || row2visible);
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }
        
               
        /// <summary>
        /// On 16/03/2021, to check Sign details can be shown or not
        /// </summary>
        private bool IncludeSignDetails(Int32 singorder)
        {
            //By default to show sign 1 details for all reports when report sign is included
            bool rtn = false;
            if (ReportProperty.Current.IncludeSignDetails == 1)
            {
                rtn = true;

                //For Voucner/Receipts Prints...decide based on settings (Receipts/Payments/Journal, contrta)
                if (ReportProperty.Current.ReportId == "RPT-024" || ReportProperty.Current.ReportId == "RPT-025" ||
                        ReportProperty.Current.ReportId == "RPT-026" || ReportProperty.Current.ReportId == "RPT-151")
                {
                    if (!string.IsNullOrEmpty(ReportProperty.Current.VoucherPrintIncludeSigns))
                    {
                        rtn = (ReportProperty.Current.VoucherPrintIncludeSigns.IndexOf(singorder.ToString()) >= 0);
                    }
                }
            }
            return rtn;
        }

       
    }
}
