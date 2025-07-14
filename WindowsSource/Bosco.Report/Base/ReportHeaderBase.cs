using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using DevExpress.XtraPrinting;
using System.Data;
using Bosco.DAO.Schema;
using System.Text.RegularExpressions;
using Bosco.DAO.Data;
using System.IO;
using System.Reflection;

namespace Bosco.Report.Base
{
    public partial class ReportHeaderBase : ReportBase
    {

        string CurrencyFormat = string.Empty;
        //  private bool isShowProjectTitle = true; // To show project title on the header (set true, not to show (set False))
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();

        private bool hideheaderfully = false; //14/01/2020, to hide header part fully

        public Font FieldColumnHeaderFont
        {
            get { return styleColumnHeader.Font; }
        }

        public ReportHeaderBase()
        {
            InitializeComponent();

            HideReportHeader = true;
            HidePrintDate = true;
            HidePageInfo = true;
            HideInstitute = true;
            HideReportTitle = true;
            HideReportSubTitle = true;
            HideReportLogoLeft = true;
            HideDateRange = true;
            HideCostCenter = true;
            HideBudgetName = true;
            // xrlblInstitute.Text = settingProperty.InstituteName;//Institute of Brothers of St. Gabriel//Don Bosco Center, Yelagiri Hills            

        }

        public override void ShowReport()
        {
            SetReportHeaderFooterSetting();

            //Attach print on event to all the table cells ----------------------------------------
            //Avoid to attach to all the reports
            if (this.ReportProperties.SupressZeroValues == (int)YesNo.Yes)
            {
                AttachPrintOnEvent(this);
            }
            //--------------------------------------------------------------------------------------

            //SetReportDate = DateTime.Now.ToString();
            //assign column header font style
            //AssignColumnHeaderFontStyle();

            base.ShowReport();
        }

        public string ReportHeaderTitle
        {
            set { xrlblInstituteProjectName.Text = value; }
        }

        public Bosco.Utility.ConfigSetting.UISettingProperty UIAppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.UISettingProperty(); }
        }

        public Bosco.Utility.ConfigSetting.SettingProperty AppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.SettingProperty(); }
        }
        public string ReportTitle
        {
            set
            {
                xrlblReportTitle.Text = value;

                //Set export file name
                SetExportFileName(value);
            }

            get { return xrlblReportTitle.Text; }
        }

        public string InstituteName
        {
            set
            {
                //15/10/2019, to include institute name with society
                xrInstituteName.Multiline = false;
                if (ReportProperty.Current.HeaderInstituteSocietyName == 1 && ReportProperty.Current.HeaderWithInstituteName == 1)
                {
                    xrInstituteName.Multiline = true;
                }
                xrInstituteName.Text = value;
            }
        }
        public string LegalEntityAddress
        {
            set { xrInstituteAddress.Text = value; }
        }

        public float SetLandscapeHeader
        {
            set { xrlblReportSubTitle.WidthF = value; }
        }

        public float SetLandscapeCostCentreWidth
        {
            set { xrlblCostCenter.WidthF = value; }
        }

        public float SetLandscapeBudgetNameWidth
        {
            set { xrlblBudgetname.WidthF = value; }
        }


        public float SetLandscapeFooterDateWidth
        {
            set
            {

                //For Temp 07/06/2023
                //this.xrlblReportDate.LocationF = new DevExpress.Utils.PointFloat(value, 7.291667F); 
                this.xrlblReportDate.LeftF = this.xrlblReportDate.LeftF;
            }
        }

        public float SetLandscapeFooter
        {
            set { xrlnFooter.WidthF = value; }
        }

        public string ReportSubTitle
        {
            set { xrlblReportSubTitle.Text = value.Trim(); }
        }

        public string CosCenterName
        {
            set { xrlblCostCenter.Text = value; }
        }
        public string BudgetName
        {
            set { xrlblBudgetname.Text = value; }
        }
        public string ReportPeriod
        {
            set
            {
                xrDateRange.Text = value;
                xrDateRange.Visible = true;
            }
            get
            {
                return xrDateRange.Text;
            }
        }

        public float TopMarginHeight
        {
            set { TopMargin.HeightF = value; }
        }

        public float BottomMarginHeight
        {
            set { BottomMargin.HeightF = value; }
        }

        public bool HideDateRange
        {
            set { xrDateRange.Visible = value; }
        }

        public bool HideCostCenter
        {
            set { xrlblCostCenter.Visible = value; }
        }
        public bool HideBudgetName
        {
            set { xrlblBudgetname.Visible = value; }
        }
        public bool HidePrintDate
        {
            set { xrlblReportDate.Visible = value; }
        }

        public bool HidePageInfo
        {
            set { xrPageInfo.Visible = value; }
        }

        public bool HideInstitute
        {
            set { xrlblInstituteProjectName.Visible = value; }
        }

        public bool HideReportTitle
        {
            set
            {
                xrlblReportTitle.Visible = value;
            }
        }

        public bool HideFooterLine
        {
            set
            {
                xrlnFooter.Visible = value;
            }
        }

        public bool HideReportSubTitle
        {
            set { xrlblReportSubTitle.Visible = value; }
        }

        public bool HideInstituteName
        {
            set { xrInstituteName.Visible = value; }
        }

        public bool HideLegalAddress
        {
            set { xrInstituteAddress.Visible = value; }
        }

        public float PageInfoWidth
        {
            set { xrPageInfo.WidthF = value; }
        }

        public void SetTitleWidth(float width)
        {
            float contentwidth = this.PageWidth - this.Margins.Left - this.Margins.Right - 5;
            xrlblReportSubTitle.WidthF = contentwidth;
            xrlnFooter.WidthF = contentwidth - 20;
            xrlblReportDate.LeftF = xrlnFooter.LeftF + xrlnFooter.WidthF - xrlblReportDate.WidthF;
            xrlblBudgetname.WidthF = contentwidth;
            xrlblCostCenter.WidthF = contentwidth;

            //realign few titles which are near report logo
            if (xrpicReportLogoLeft.Visible)
            {
                contentwidth = contentwidth - xrpicReportLogoLeft.LeftF;
            }
            xrlblInstituteProjectName.WidthF = width;
            xrInstituteName.WidthF = width;
            xrInstituteAddress.WidthF = width;
            xrlblReportTitle.WidthF = width;
        }

        public void SetTitleActualWidth(float width)
        {
            float contentwidth = width;
            xrlblReportSubTitle.WidthF = contentwidth;
            xrlnFooter.WidthF = contentwidth;
            xrlblReportDate.LeftF = xrlnFooter.LeftF + xrlnFooter.WidthF - xrlblReportDate.WidthF;
            xrlblBudgetname.WidthF = contentwidth;
            xrlblCostCenter.WidthF = contentwidth;

            //realign few titles which are near report logo
            if (xrpicReportLogoLeft.Visible)
            {
                contentwidth = contentwidth - xrpicReportLogoLeft.LeftF;
            }
            xrlblInstituteProjectName.WidthF = width;
            xrInstituteName.WidthF = width;
            xrInstituteAddress.WidthF = width;
            xrlblReportTitle.WidthF = width;
        }

        public void SetTitleLeftWithoutMargin(bool isOnlyTitle = false)
        {
            if (!isOnlyTitle) this.Margins.Left = 0;

            xrlblReportSubTitle.LeftF = 0;
            xrlnFooter.LeftF = 0;
            //xrlblReportDate.LeftF = xrlnFooter.LeftF + xrlnFooter.WidthF - xrlblReportDate.WidthF;
            xrlblBudgetname.LeftF = 0;
            xrlblCostCenter.LeftF = 0;


            xrlblInstituteProjectName.LeftF = 0;
            xrInstituteName.LeftF = 0;
            xrInstituteAddress.LeftF = 0;
            xrlblReportTitle.LeftF = 0;

            xrPageInfo.LeftF = 0;
        }

        public string SetReportDate
        {
            set { xrlblReportDate.Text = value; }
        }
        public bool HideReportDate
        {
            set { xrlblReportDate.Visible = value; }
        }

        public bool HideReportLogoLeft
        {
            set
            {
                xrpicReportLogoLeft.Visible = value;
                xrpicReportLogoLeft.Image = ImageProcessing.ByteArrayToImage(settingProperty.AcMeERPLogo);
                if (value == true)
                {
                    xrDateRange.LeftF = (xrpicReportLogoLeft.LeftF + xrpicReportLogoLeft.WidthF);
                    xrDateRange.WidthF = (xrlblReportSubTitle.WidthF - xrpicReportLogoLeft.WidthF);
                }
                else
                {
                    xrDateRange.LeftF = xrlblReportSubTitle.LeftF;
                    xrDateRange.WidthF = xrlblReportSubTitle.WidthF;
                }

                xrlblInstituteProjectName.LeftF = xrDateRange.LeftF;
                xrlblInstituteProjectName.WidthF = xrDateRange.WidthF;
                xrlblReportTitle.LeftF = xrDateRange.LeftF;
                xrlblReportTitle.WidthF = xrDateRange.WidthF;

                xrInstituteAddress.LeftF = xrDateRange.LeftF;
                xrInstituteAddress.WidthF = xrDateRange.WidthF;

                xrInstituteName.LeftF = xrDateRange.LeftF;
                xrInstituteName.WidthF = xrDateRange.WidthF;
            }
        }

        public bool HideReportHeader
        {
            set { ReportHeader.Visible = value; }
        }

        public bool HidePageFooter
        {
            set { PageFooter.Visible = value; }
        }

        public void SetHeaderplain()
        {
            xrlblBudgetname.Borders = xrlblCostCenter.Borders = xrlblReportSubTitle.Borders = Borders = BorderSide.None;
            xrlblBudgetname.BackColor = xrlblCostCenter.BackColor = xrlblReportSubTitle.BackColor = xrDateRange.BackColor = Color.Transparent;
        }

        public void SetHeaderColumnTitleAlignment(XRTable xrtTable)
        {
            foreach (XRTableCell xrCellHeader in xrtTable.Rows.FirstRow.Cells)
            {
                switch (ReportProperty.Current.TitleAlignment)
                {
                    case 0:
                        {
                            xrCellHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            break;
                        }
                    case 1:
                        {
                            xrCellHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            break;
                        }
                    case 2:
                        {
                            xrCellHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            break;
                        }
                }
            }
        }


        private string SetCurrencyFormat(string caption)
        {
            if (!caption.Contains(settingProperty.Currency))
                CurrencyFormat = String.Format("{0} ({1})", caption, settingProperty.Currency);
            else { CurrencyFormat = caption; }
            return CurrencyFormat;
        }

        private string SetCurrencyFormat(string caption, string currencysymbol)
        {
            if (!caption.Contains(settingProperty.Currency))
                CurrencyFormat = String.Format("{0} ({1})", caption, currencysymbol);
            else { CurrencyFormat = caption; }
            return CurrencyFormat;
        }

        private string SetCurrencyFormatBefore(string caption)
        {
            if (!caption.Contains(settingProperty.Currency))
                CurrencyFormat = String.Format("{1} {0}", caption, settingProperty.Currency);
            else { CurrencyFormat = caption; }
            return CurrencyFormat;
        }

        private string SetCurrencyFormatBefore(string caption, string currencysymbol)
        {
            if (!caption.Contains(settingProperty.Currency))
                CurrencyFormat = String.Format("{1} {0}", caption, currencysymbol);
            else { CurrencyFormat = caption; }
            return CurrencyFormat;
        }

        /// <summary>
        /// On 04/07/2019, Since Indian currency font is not displaying while we exporting pdf.
        /// so font unicode font
        /// </summary>
        /// <param name="fnt"></param>
        /// <returns></returns>
        private Font SetCurrencyUnicodeFont(Font fnt)
        {
            if (fnt.Name != "Arial Unicode MS")
            {
                if (this.ReportProperties.ColumnCaptionFontStyle == 0)
                    fnt = new System.Drawing.Font("Arial Unicode MS", fnt.Size, FontStyle.Bold);
                else
                    fnt = new System.Drawing.Font("Arial Unicode MS", fnt.Size, FontStyle.Regular);
            }
            return fnt;
        }

        public void SetCurrencyFormat(string Caption, XRLabel tbllbl)
        {
            tbllbl.Text = SetCurrencyFormat(tbllbl.Text);
            tbllbl.Font = SetCurrencyUnicodeFont(tbllbl.Font);
        }

        public void SetCurrencyFormat(string Caption, XRTableCell tblcell)
        {
            tblcell.Text = SetCurrencyFormat(tblcell.Text);
            tblcell.Font = SetCurrencyUnicodeFont(tblcell.Font);
        }

        public void SetCurrencyFormat(string Caption, XRTableCell tblcell, string currencysymbol)
        {
            tblcell.Text = SetCurrencyFormat(tblcell.Text, currencysymbol);
            tblcell.Font = SetCurrencyUnicodeFont(tblcell.Font);
        }


        public void SetCurrencyFormatPrefix(string Caption, XRTableCell tblcell)
        {
            tblcell.Text = SetCurrencyFormatBefore(tblcell.Text);
            tblcell.Font = SetCurrencyUnicodeFont(tblcell.Font);
        }

        public void SetCurrencyFormatPrefix(string Caption, XRLabel tbllbl)
        {
            tbllbl.Text = SetCurrencyFormatBefore(tbllbl.Text);
            tbllbl.Font = SetCurrencyUnicodeFont(tbllbl.Font);
        }

        public string GetGroupName(int GroupId)
        {
            // string groupname = "";
            ResultArgs resultArgs = null;
            //using (BalanceSystem balanceSystem = new BalanceSystem())
            //{
            //    groupname = balanceSystem.GetGroupName(GroupId);
            //}

            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LedgerBank.FetchGroupName))
            {
                dataManager.Parameters.Add(this.appSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;

        }

        public string GetLedgerName(int LedgerId)
        {
            // string Ledgername = "";
            ResultArgs resultArgs = null;
            //using (BalanceSystem balanceSystem = new BalanceSystem())
            //{
            //    Ledgername = balanceSystem.GetLegerName(LedgerId);
            //}
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LedgerBank.FetchLedgerName))
            {
                dataManager.Parameters.Add(this.appSchema.LedgerBalance.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToString;
        }

        /// <summary>
        /// Fetch ledger id by ledger name
        /// </summary>
        /// <param name="dataManagerLedger"></param>
        /// <returns></returns>
        public int FetchLedgerGroupId(string LedgerName)
        {
            int GroupId = 0;
            try
            {
                ResultArgs resultArgs = new ResultArgs();
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.FetchGroupIdByLedgerName))
                {
                    dataManager.Parameters.Add(this.appSchema.Ledger.LEDGER_NAMEColumn, LedgerName);
                    resultArgs = dataManager.FetchData(Bosco.DAO.Data.DataSource.Scalar);
                    if (resultArgs.Success)
                    {
                        GroupId = resultArgs.DataSource.Sclar.ToInteger;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.TallyMigration.WriteLog(ex.Message, ex.Source.ToString(), ex.StackTrace.ToString(), ex.StackTrace.ToString(), true);
            }
            finally { }
            return GroupId;
        }



        public bool ShowProjectinFooter
        {
            set
            {
                xrlblProjectName.Visible = value;
                if (value == true)
                {
                    xrlblProjectName.Text = ReportProperty.Current.ProjectTitle;
                }
                else
                {
                    xrlblProjectName.Text = string.Empty;
                }
            }
        }

        public bool HideHeaderFully
        {
            set { hideheaderfully = value; }
        }

        public bool ShowTitleAtEachPage
        {
            set
            {
                //Hide Header Section for FDCCSI Annual report
                if (hideheaderfully)
                {
                    HideReportHeader = HidePageFooter = false;
                }
                else
                {
                    if (value == true)
                    {
                        HideReportHeader = false;
                    }
                    else
                    {
                        HideReportHeader = true;
                    }
                }
            }
        }

        protected void SetReportHeaderFooterSetting()
        {
            this.HideReportLogoLeft = (ReportProperties.ShowLogo == 1);
            this.HidePageInfo = (ReportProperties.ShowPageNumber == 1);
            this.HidePrintDate = (ReportProperties.ShowPrintDate == 1);
            this.ShowProjectinFooter = (ReportProperties.ShowProjectsinFooter == 1);
        }

        public void setHeaderTitleAlignment()
        {

            switch (ReportProperty.Current.TitleAlignment)
            {
                case 0:
                    {
                        xrlblInstituteProjectName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrlblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrInstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        xrDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        break;
                    }
                case 1:
                    {
                        xrlblInstituteProjectName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrlblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrInstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        xrDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        break;
                    }
                case 2:
                    {
                        xrlblInstituteProjectName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrlblReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrInstituteAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrInstituteName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        xrDateRange.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                        break;
                    }
            }


        }

        public void RemoveVerticaLine(XRTable xrTableName)
        {
            foreach (XRTableCell xrCellHeader in xrTableName.Rows.FirstRow.Cells)
            {
                switch (ReportProperty.Current.ShowVerticalLine)
                {
                    case 0:
                        {
                            xrCellHeader.BorderWidth = 0;
                            xrCellHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                            xrCellHeader.BorderWidth = 1;
                            break;
                        }
                }
            }
        }
        public void RemoveHorizontalLine(XRTable xrTableName)
        {
            foreach (XRTableCell xrCellHeader in xrTableName.Rows.FirstRow.Cells)
            {
                switch (ReportProperty.Current.ShowHorizontalLine)
                {
                    case 0:
                        {
                            xrCellHeader.BorderWidth = 0;
                            xrCellHeader.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left;
                            // xrCellHeader.BorderWidth = 1;
                            break;
                        }
                }
            }
        }

        public string GetInstituteName()
        {
            string Rtn = string.Empty;
            try
            {
                if (ReportProperty.dtLedgerEntity != null && ReportProperty.dtLedgerEntity.Rows.Count != 0)
                {
                    DataView dvFilter = ReportProperty.dtLedgerEntity.DefaultView;

                    if (ReportProperty.Current.LedgalEntityId == null || string.IsNullOrEmpty(ReportProperty.Current.LedgalEntityId))
                    {
                        foreach (DataRow dr in dvFilter.ToTable().Rows)
                        {
                            ReportProperty.Current.LedgalEntityId += dr["CUSTOMERID"] != DBNull.Value ? dr["CUSTOMERID"] + "," : string.Empty;
                        }
                        ReportProperty.Current.LedgalEntityId = ReportProperty.Current.LedgalEntityId.TrimEnd(',');
                    }

                    dvFilter.RowFilter = "CUSTOMERID IN (" + ReportProperty.Current.LedgalEntityId + ")";

                    //if (ReportProperty.Current.SelectedProjectCount == 1 && dvFilter.Count != 0)
                    //{
                    //    ReportProperty.Current.InstituteName = dvFilter.ToTable().Rows[0]["SOCIETYNAME"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["SOCIETYNAME"].ToString() : settingProperty.InstituteName;
                    //    ReportProperty.Current.LegalAddress = dvFilter.ToTable().Rows[0]["ADDRESS"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["ADDRESS"].ToString() : string.Empty;
                    //}
                    //else if (dvFilter.Count != 0)
                    //{
                    //    ReportProperty.Current.InstituteName = dvFilter.ToTable().Rows[0]["INSTITUTENAME"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["INSTITUTENAME"].ToString() : settingProperty.InstituteName;
                    //    ReportProperty.Current.LegalAddress = dvFilter.ToTable().Rows[0]["ADDRESS"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["ADDRESS"].ToString() : string.Empty;
                    //}
                    if (ReportProperty.Current.SelectedProjectCount >= 1 && dvFilter.Count != 0)
                    {
                        if (ReportProperty.Current.HeaderInstituteSocietyName == 0)
                        {
                            ReportProperty.Current.InstituteName = settingProperty.InstituteName;
                            //ReportProperty.Current.InstituteName = dvFilter.ToTable().Rows[0]["INSTITUTENAME"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["INSTITUTENAME"].ToString() : string.Empty;
                            ReportProperty.Current.LegalAddress = !string.IsNullOrEmpty(settingProperty.Address.ToString()) ? settingProperty.Address.ToString() : string.Empty;// dvFilter.ToTable().Rows[0]["ADDRESS"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["ADDRESS"].ToString() : string.Empty;
                        }
                        else
                        {
                            ReportProperty.Current.InstituteName = dvFilter.ToTable().Rows[0]["SOCIETYNAME"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["SOCIETYNAME"].ToString() : string.Empty;
                            ReportProperty.Current.GSTStateCode = dvFilter.ToTable().Rows[0]["STATE_CODE"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["STATE_CODE"].ToString() : string.Empty;
                        }
                        if (ReportProperty.Current.HeaderInstituteSocietyAddress == 0)
                        {
                            ReportProperty.Current.LegalAddress = !string.IsNullOrEmpty(settingProperty.Address.ToString()) ? settingProperty.Address.ToString() : string.Empty;
                        }
                        else
                        {
                            ReportProperty.Current.LegalAddress = dvFilter.ToTable().Rows[0]["ADDRESS"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["ADDRESS"].ToString() : string.Empty;
                        }

                        ReportProperty.Current.LegalTelephone = dvFilter.ToTable().Rows[0]["PHONE"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["PHONE"].ToString() : string.Empty;
                        ReportProperty.Current.LegalGSTNo = dvFilter.ToTable().Rows[0]["GST_NO"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["GST_NO"].ToString() : string.Empty;
                    }
                    else
                    {
                        // ReportProperty.Current.InstituteName = "Institute of the Brothers of St. Gabriel Society";
                        ReportProperty.Current.InstituteName = settingProperty.InstituteName;
                        ReportProperty.Current.LegalAddress = string.Empty;

                        ReportProperty.Current.LegalTelephone = settingProperty.Phone;
                        ReportProperty.Current.LegalGSTNo = string.Empty;
                        ReportProperty.Current.GSTStateCode = settingProperty.StateCode;
                    }

                    ReportProperty.Current.GSTStateCode = settingProperty.StateCode;

                    //On 12/05/2022, Get Society name always if seleceted project count is 1 -------------
                    ReportProperty.Current.LegalName = string.Empty;
                    if (ReportProperty.Current.SelectedProjectCount >= 1 && dvFilter.Count != 0)
                    {
                        ReportProperty.Current.LegalName = dvFilter.ToTable().Rows[0]["SOCIETYNAME"] != DBNull.Value ? dvFilter.ToTable().Rows[0]["SOCIETYNAME"].ToString() : string.Empty;
                    }
                    //-------------------------------------------------------------------------------------
                    dvFilter.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            Rtn = ReportProperty.Current.InstituteName;

            if (ReportProperty.Current.HeaderInstituteSocietyName == 1 && ReportProperty.Current.HeaderWithInstituteName == 1)
            {
                Rtn += System.Environment.NewLine + settingProperty.InstituteName;
            }

            return Rtn;
        }
        public void CheckLegalEntity()
        {
            int legalCount = 0;
            ResultArgs resultArgs = null;
            string Pid = ReportProperty.Current.Project;

            //On 12/05/2022, Get Society name always if seleceted project count is 1 -------------
            ReportProperty.Current.LegalName = string.Empty;
            //-------------------------------------------------------------------------------------

            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.CheckLegalEntity))
            {
                dataManager.Parameters.Add(this.appSchema.Project.PROJECT_IDColumn, Pid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                legalCount = (resultArgs.DataSource.Table.Rows.Count == 0) ? 0 : (resultArgs.DataSource.Table.Rows.Count == 1) ? 1 : 2;
            }
            //using (BalanceSystem balanceSystem = new BalanceSystem())
            //{
            //    DataTable dtres = balanceSystem.CheckNoofLegalentity(Pid);
            //this.ReportProperties.ShowTitleSocietyName = 1;
            //legalCount = (dtres.Rows.Count == 0) ? 0 : (dtres.Rows.Count == 1) ? 1 : 2;
            if (legalCount == 1 && resultArgs.DataSource.Table != null)
            {
                if (!string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0]["CUSTOMERID"].ToString()))
                {
                    this.InstituteName = this.GetInstituteName();
                }
                else
                {
                    this.InstituteName = this.settingProperty.InstituteName;
                }
            }
            else if (legalCount == 0)
            {
                this.InstituteName = this.settingProperty.InstituteName;
                //this.ReportProperties.ShowTitleSocietyName = 1;
            }
            else if (legalCount >= 2 && resultArgs.DataSource.Table != null)
            {
                this.InstituteName = this.settingProperty.InstituteName;
            }
            // }
        }
        public void SetReportTitle()
        {
            this.ReportTitle = ReportProperty.Current.ReportTitle;

            //On 04/12/2024 to set currency in the report title if and if only currency based reports ---------------------------------------------------
            if (this.AppSetting.AllowMultiCurrency == 1 && !string.IsNullOrEmpty(this.ReportProperties.CurrencyCountry) && this.ReportProperties.CurrencyCountryId > 0)
            {
                this.ReportTitle += " - " + this.ReportProperties.CurrencyCountry;
            }
            //--------------------------------------------------------------------------------------------------------------------------------------------

            if (ReportProperty.Current.SelectedProjectCount != 1)
            {
                this.HideReportSubTitle = true;
                this.ReportHeaderTitle = null;
                this.InstituteName = null;
                //if (!isShowProjectTitle)
                //{
                //    this.ReportSubTitle = !string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle) ? ReportProperty.Current.ProjectTitle : string.Empty;
                //}
                //else
                //{
                this.ReportSubTitle = !string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle) ? ReportProperty.Current.ProjectTitle : string.Empty;
                //  }
                this.xrInstituteName.Font = new System.Drawing.Font("Tahoma", 16.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                CheckLegalEntity();
                // this.InstituteName = (ReportProperty.Current.HeaderInstituteSocietyName == 0) ? this.GetInstituteName() : this.settingProperty.SocietyName;
            }
            else
            {
                this.HideInstitute = true;
                this.HideReportSubTitle = true;
                if (!string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle))
                {
                    string[] projectTitle = ReportProperty.Current.ProjectTitle.Split('-');
                    if (this.ReportProperties.isShowProjectTitle)
                    {
                        this.ReportHeaderTitle = projectTitle[0];
                    }
                }
                if (!this.ReportProperties.isShowProjectTitle)
                {
                    this.ReportSubTitle = !string.IsNullOrEmpty(ReportProperty.Current.ProjectTitle) ? ReportProperty.Current.ProjectTitle : string.Empty;
                    // this.ReportSubTitle = null;
                }
                else
                {
                    this.ReportSubTitle = string.Empty; //null
                }
                this.HideLegalAddress = true;
                this.HideInstituteName = true;

                //On 08/08/2020, to set Community/Instution Header Font Size
                //this.xrInstituteName.Font = new System.Drawing.Font("Tahoma", 13F);
                this.xrInstituteName.Font = new System.Drawing.Font("Tahoma", 16.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                CheckLegalEntity();
                //this.InstituteName = (ReportProperty.Current.HeaderInstituteSocietyName == 0) ? this.GetInstituteName() : this.settingProperty.SocietyName;
            }
            this.LegalEntityAddress = (ReportProperty.Current.HeaderInstituteSocietyAddress == 0) ? settingProperty.Address : ReportProperty.Current.LegalAddress;

            setHeaderTitleAlignment();
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.PERIOD + " " + this.ReportProperties.DateFrom + " - " + this.ReportProperties.DateTo;
            ReportProperty.Current.GSTState = settingProperty.State;

            ReportProperty.Current.GSTStateCode = settingProperty.StateCode;
        }

        public void CommonReportHeaderTitle()
        {
            this.ReportTitle = ReportProperty.Current.ReportTitle;
            this.ReportSubTitle = ReportProperty.Current.ProjectTitle;
            this.InstituteName = settingProperty.InstituteName;
            setHeaderTitleAlignment();
            this.ReportPeriod = MessageCatalog.ReportCommonTitle.ASON + " " + this.ReportProperties.DateAsOn;
        }

        //public void SetSortOrderbyLedger(GroupBand grpBandView, string FieldName)
        //{
        //    if (grpBandView.Visible)
        //    {
        //        if (this.ReportProperties.SortByLedger == 0)
        //        {

        //        }
        //        else
        //        {
        //        }
        //    }
        //}

        //public void SetSortOrderbyGroup(GroupBand grpGroupBandView, string FieldName)
        //{
        //    if (grpGroupBandView.Visible)
        //    {
        //        if (this.ReportProperties.SortByGroup == 0)
        //        {

        //        }
        //        else
        //        {
        //        }
        //    }
        //}

        /// <summary>
        /// This method is used to attach PrintonEvent to all the table cells, (1.supress zero value)
        /// </summary>
        private void AttachPrintOnEvent(XtraReport rpt)
        {
            foreach (XRControl RptCtrl in rpt.AllControls<XRControl>())
            {
                if (RptCtrl.GetType() == typeof(XRTable))
                {
                    XRTable xrTbl = RptCtrl as XRTable;

                    //Attach xtratable no of rows header row or design level rows alone (not all rows)
                    //if (xrTbl.Rows.Count > 2)
                    //{

                    //}
                    foreach (XRTableRow xrtblrow in xrTbl.Rows)
                    {
                        foreach (XRTableCell RptField in xrtblrow.Cells)
                        {
                            RptField.PrintOnPage -= new PrintOnPageEventHandler(RptField_PrintOnPage);
                            RptField.PrintOnPage += new PrintOnPageEventHandler(RptField_PrintOnPage);
                            //RptField.BeforePrint += new System.Drawing.Printing.PrintEventHandler(RptField_BeforePrint);
                        }
                    }
                }
                //If control is subreport, attach RptField_PrintOnPage to its child controls too
                else if (RptCtrl.GetType() == typeof(XRSubreport))
                {
                    XRSubreport subrpt = RptCtrl as XRSubreport;
                    XtraReport subreport = subrpt.ReportSource;
                    if (subreport != null)
                    {
                        AttachPrintOnEvent(subreport);
                    }
                }
            }
        }

        /// <summary>
        /// This event is used to supresss if decimal, double and inter field cells values is zero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RptField_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            XRTableCell cell = (XRTableCell)sender;

            if (cell != null && this.ReportProperties.SupressZeroValues == (int)YesNo.Yes)
            {
                String cellvalue = cell.Text;

                //Check cell text value whether, numberic or albpa number
                //if numberic, chekc value is zero and make to empty 
                Regex regex = new Regex("^-?\\d*(\\.\\d+)?$");
                if (regex.IsMatch(cellvalue))
                {
                    double value = this.UtilityMember.NumberSet.ToDouble(cell.Text);
                    if (value == 0)
                    {
                        cell.Text = string.Empty;
                    }
                }
            }
        }

        //    XRBinding xrbinding = cell.DataBindings["Text"];               
        //    if (xrbinding != null)
        //    {
        //        string bindingName = xrbinding.DataMember;
        //        //Temp...have to find actual column name and get its data type
        //        string[] fields = bindingName.Split('.');
        //        if (fields.Length > 1)
        //        {
        //            string boundcolumnnmae = fields.GetValue(1).ToString();
        //            DataRowView drv = GetCurrentRow() as DataRowView;
        //            if (drv.DataView.Table.Columns.IndexOf(boundcolumnnmae) > 0)
        //            {
        //                Type type = drv.DataView.Table.Columns[boundcolumnnmae].DataType;

        //                if (type == typeof(System.Decimal) || type == typeof(System.Double) || type == typeof(System.Int32))
        //                {
        //                    double value = this.UtilityMember.NumberSet.ToDouble(cell.Text);
        //                    if (value == 0)
        //                    {
        //                        cell.Text = string.Empty;
        //                    }
        //                }
        //            }
        //        }
        //    }


        /// <summary>
        /// To get the collection of record with comma seperator
        /// </summary>
        /// <param name="dtValue"></param>
        /// <param name="OutputColumnName"></param>
        /// <returns></returns>
        public string GetCommaSeparatedValue(DataTable dtValue, string OutputColumnName)
        {
            string retValue = String.Empty;
            if (dtValue != null && dtValue.Rows.Count > 0)
            {
                var rowVal = dtValue.AsEnumerable();
                retValue = String.Format("'{0}'", String.Join("','", (from r in rowVal
                                                                      select r.Field<string>(OutputColumnName).Replace("'", "''"))));
            }
            return retValue;
        }

        /// <summary>
        /// On 10/09/2018, 
        /// This method is used to assign column header captions font style (Bold, Regular), those controls are only in Page header in all fields
        /// 
        /// Remark : WE USE STYLES initially, but many reports are not implemented STYLES, otherwise we would have assinged in STYLE it self
        /// </summary>
        private void AssignColumnHeaderFontStyle()
        {
            if (this.Bands[BandKind.PageHeader] != null)
            {
                PageHeaderBand band = this.Bands[BandKind.PageHeader] as PageHeaderBand;
                foreach (DevExpress.XtraReports.UI.XRControl control in band)
                {
                    if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                    {
                        DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                        foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                        {
                            foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                            {
                                switch (ReportProperty.Current.ColumnCaptionFontStyle)
                                {
                                    case 0: // 0 - default, 2 - Bold
                                        cell.Font = styleColumnHeader.Font;
                                        break;
                                    case 1:
                                        cell.Font = new Font(styleColumnHeader.Font, FontStyle.Regular);
                                        break;
                                }
                            }
                        }
                    }
                }
                /*
                foreach (XRControl xrctl in this.Bands[BandKind.PageHeader].Controls)
                {
                    foreach (XRControl xtratable in xrctl.AllControls<XRControl>())
                    {
                        if (xtratable.GetType() == typeof(DevExpress.XtraReports.UI.XRTableCell))
                        {
                            MessageRender.ShowMessage(xrctl.Name + "-" + xtratable.GetType().Name, true);
                            if (xtratable.GetType() == typeof(XRTableCell))
                            {
                                switch (ReportProperty.Current.ColumnCaptionFontStyle)
                                {
                                    case 0: // 0 - default, 2 - Bold
                                        xtratable.Font = styleColumnHeader.Font;
                                        break;
                                    case 1:
                                        xtratable.Font = new Font(xrctl.Font, FontStyle.Regular);
                                        break;
                                }
                            }
                            else if (xrctl.GetType() == typeof(XRSubreport))
                            {
                                AssignColumnHeaderFontStyle(xrctl.Report as ReportBase);
                            }
                        }
                    }
                }*/
            }

        }

        public string AssignBudgetDateRangeTitle()
        {
            string rtn = string.Empty;
            if (AppSetting.IS_ABEBEN_DIOCESE)
            {
                if (ReportProperty.Current.ReportId.Equals("RPT-046"))
                {
                    this.SetReportDate = AppSetting.YearFrom + "-" + AppSetting.YearTo;
                }
                else
                {
                    this.ReportPeriod = string.Empty;
                }
            }
            else if (AppSetting.IS_DIOMYS_DIOCESE)
            {
                if (ReportProperty.Current.ReportId.Equals("RPT-163"))
                {
                    this.ReportPeriod = "Month : Budget for the month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, "MMM yyyy");
                }
                else
                {
                    this.ReportPeriod = "Month : Budget for the month of " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, "MMM") + " & " + UtilityMember.DateSet.ToDate(ReportProperty.Current.DateTo, "MMM yyyy");
                }
                this.ReportSubTitle = "Institution Name : " + this.AppSetting.InstituteName;
                string bankname = GetBankNamesByProject();
                if (string.IsNullOrEmpty(bankname))
                {
                    this.BudgetName = ReportProperty.Current.BudgetProject;
                }
                else
                {
                    this.BudgetName = ReportProperty.Current.BudgetProject + " (" + GetBankNamesByProject() + ")";
                }
            }
            else
            {
                if ((ReportProperty.Current.ReportId.Equals("RPT-048") || ReportProperty.Current.ReportId.Equals("RPT-163") && (!string.IsNullOrEmpty(ReportProperty.Current.BudgetName))))
                {
                    if (ReportProperty.Current.BudgetName.Contains(","))
                    {
                        this.ReportPeriod = string.Empty;
                    }

                }
            }
            return rtn;
        }


        /// <summary>
        /// To get projectsids for selected repor date
        /// </summary>
        /// <returns></returns>
        public string GetBudgetProjectsByDateRange()
        {
            ResultArgs resultArgs = new ResultArgs();
            string Rtn = string.Empty;
            string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetProjectsByDate);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToShortDateString());
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString());
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable dtBudgets = resultArgs.DataSource.Table;
                //string Projects = dtBudgetInfo.Rows[0]["PROJECT_NAME"].ToString();
                string ProjectsIds = dtBudgets.Rows[0][this.reportSetting1.BUDGETVARIANCE.PROJECTColumn.ColumnName].ToString();
                Rtn = ProjectsIds;
            }

            return Rtn;
        }

        /// <summary>
        /// To get budgeted projects for given selected projects
        /// </summary>
        /// <returns></returns>
        public string GetBudgetProjectsByDateProjects()
        {
            ResultArgs resultArgs = new ResultArgs();
            string Rtn = "0";
            string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetProjectsByDateProjects);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToShortDateString());
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString());
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable dtBudgets = resultArgs.DataSource.Table;
                //string Projects = dtBudgetInfo.Rows[0]["PROJECT_NAME"].ToString();
                string ProjectsIds = dtBudgets.Rows[0][this.reportSetting1.BUDGETVARIANCE.PROJECTColumn.ColumnName].ToString();
                Rtn = string.IsNullOrEmpty(ProjectsIds) ? "0" : ProjectsIds;
            }

            return Rtn;
        }

        /// <summary>
        /// To get budgeted projects for given selected projects
        /// </summary>
        /// <returns></returns>
        public string GetBudgetProjectsNamesByDateProjects()
        {
            ResultArgs resultArgs = new ResultArgs();
            string Rtn = string.Empty;
            string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetProjectsByDateProjects);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToShortDateString());
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString());
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable dtBudgets = resultArgs.DataSource.Table;
                string Projects = dtBudgets.Rows[0]["PROJECT_NAME"].ToString();
                //string ProjectsIds = dtBudgets.Rows[0][this.reportSetting1.BUDGETVARIANCE.PROJECTColumn.ColumnName].ToString();
                Rtn = Projects;
            }

            return Rtn;
        }

        /// <summary>
        /// To get budgeted names for given selected projects and date
        /// </summary>
        /// <returns></returns>
        public string GetBudgetNamesByDateProjects()
        {
            ResultArgs resultArgs = new ResultArgs();
            string Rtn = string.Empty;
            string budgetInfo = this.GetBudgetvariance(SQL.ReportSQLCommand.BudgetVariance.BudgetProjectsByDateProjects);
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_FROMColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToShortDateString());
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.DATE_TOColumn, UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToShortDateString());
                dataManager.Parameters.Add(this.reportSetting1.BUDGETVARIANCE.PROJECT_IDColumn, this.ReportProperties.Project);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, budgetInfo);
            }

            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DataTable dtBudgets = resultArgs.DataSource.Table;
                //string Projects = dtBudgetInfo.Rows[0]["PROJECT_NAME"].ToString();
                string budgetname = dtBudgets.Rows[0][this.reportSetting1.BUDGETVARIANCE.BUDGET_NAMEColumn.ColumnName].ToString();
                Rtn = budgetname;
            }

            return Rtn;
        }

        public void ValidateAndAssignBudgetedProjects()
        {
            string annualbudgetsprojects = GetBudgetProjectsByDateRange();

            if (string.IsNullOrEmpty(this.ReportProperties.Project) && string.IsNullOrEmpty(annualbudgetsprojects))
            {
                string[] projects = this.ReportProperties.Project.Split(',');
            }
        }

        /// <summary>
        /// Include cash/bank/fd opening and closing balance
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="dtRpt"></param>
        /// <param name="IsOpBalance"></param>
        /// <param name="IncludePreviousYear"></param>
        public void AttachBalancesForBudget(DataTable dtRpt, bool IsOpBalance, bool IncludePreviousYear, bool IsAnnualBalancesheetReport)
        {
            string projects = projects = this.ReportProperties.Project;
            string balanceFYDate = AppSetting.YearFrom;
            //If Head office is CMF, we have to show all cash/bank/fd ledgers
            /*if (AppSetting.IS_CMF_CONGREGATION)
            {
                //For CMF congregation, if Cash/Bank/FD is enabled. Take all projets except Social Service cash/bank/fd balances
                ResultArgs result =  this.GetProjects();
                projects = string.Empty;
                if (result.Success && result.DataSource.Table != null)
                {
                    DataTable dtAllProjects = result.DataSource.Table;

                    dtAllProjects.DefaultView.RowFilter = this.appSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName + " <> 'Social Service'";
                    dtAllProjects = dtAllProjects.DefaultView.ToTable();
                    if (dtAllProjects.Rows.Count > 0)
                    {
                        projects = String.Join(",", dtAllProjects.AsEnumerable().Select(x => x.Field<UInt32>(this.appSchema.Project.PROJECT_IDColumn.ColumnName).ToString()));
                    }
                }
                
            }*/
            string fixedgrp = (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit;
            string balancedate = IsOpBalance ? this.ReportProperties.DateFrom : this.ReportProperties.DateTo;
            string balancecaption = IsOpBalance ? "OPENING BALANCES" : "CLOSING BALANCES";

            //For Balancesheet budget report, group under Income/Expenditure for all ledger groups (Cash/Bank/FD);
            if (IsAnnualBalancesheetReport)
            {
                //balancecaption = IsOpBalance ? "Income" : "Expenditure";
            }

            //For Current year Columns with balances
            ResultArgs resultBalance = new ResultArgs();
            if (IsAnnualBalancesheetReport)
                resultBalance = GetBalanceDetailProjectWise(IsOpBalance, balancedate, projects, fixedgrp, balanceFYDate);
            else
                resultBalance = GetBalanceDetail(IsOpBalance, balancedate, projects, fixedgrp, balanceFYDate);

            if (resultBalance.Success && resultBalance.DataSource.Table != null)
            {
                DataTable dtBalance = resultBalance.DataSource.Table;
                DataColumn dcNature = new DataColumn("NATURE_ID", typeof(System.UInt32));
                dcNature.DefaultValue = IsOpBalance ? 0 : 5; //on 07/07/2020, to show ledger group opening and closing balances in first and last of the report
                dtBalance.Columns.Add(dcNature);
                if (dtBalance.Columns.Contains(this.reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName))
                {
                    dtBalance.Columns["TRANS_MODE"].ColumnName = "TRANS_MODE1";
                    dtBalance.Columns.Add("AMOUNT1", dtBalance.Columns["AMOUNT"].DataType, "IIF(TRANS_MODE1='CR', AMOUNT *-1, AMOUNT * 1)");

                    DataColumn dc = new DataColumn(reportSetting1.BUDGETVARIANCE.TRANS_MODEColumn.ColumnName, typeof(System.String));
                    dc.DefaultValue = IsOpBalance ? TransMode.CR.ToString() : TransMode.DR.ToString();
                    //dtBalance.Columns.Remove(dc.ColumnName);
                    dtBalance.Columns.Add(dc);
                }

                if (IsOpBalance) //Opening Balance
                {
                    dtBalance.Columns["AMOUNT1"].ColumnName = "APPROVED_AMOUNT1";
                    dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName, typeof(decimal), "APPROVED_AMOUNT1");
                    dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName, typeof(decimal), "APPROVED_AMOUNT1");

                    //to get FY starting opening balance for approved column alone
                    dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName, typeof(decimal));

                    ResultArgs resultFYBalance = new ResultArgs();
                    if (IsAnnualBalancesheetReport)
                    {
                        resultFYBalance = GetBalanceDetailProjectWise(IsOpBalance, balanceFYDate, projects, fixedgrp, balanceFYDate);
                    }
                    else
                    {
                        resultFYBalance = GetBalanceDetail(IsOpBalance, balanceFYDate, projects, fixedgrp, balanceFYDate);
                    }

                    if (resultFYBalance.Success && resultFYBalance.DataSource.Table != null)
                    {
                        DataTable dtFYOPBalance = resultFYBalance.DataSource.Table;
                        foreach (DataRow dr in dtFYOPBalance.Rows)
                        {
                            Int32 ledgerid = UtilityMember.NumberSet.ToInteger(dr[reportSetting1.Receipts.LEDGER_IDColumn.ColumnName].ToString());
                            Int32 projectid = 0;
                            if (IsAnnualBalancesheetReport)
                            {
                                projectid = UtilityMember.NumberSet.ToInteger(dr["PROJECT_ID"].ToString());
                            }
                            string transmode = dr[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                            Double amt = UtilityMember.NumberSet.ToDouble(dr[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                            amt = amt * (transmode.ToUpper() == TransMode.CR.ToString().ToUpper() ? -1 : 1);
                            string filter = reportSetting1.Receipts.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                            if (IsAnnualBalancesheetReport && projectid > 0)
                            {
                                filter += " AND PROJECT_ID = " + projectid;
                            }
                            dtBalance.DefaultView.RowFilter = string.Empty;
                            dtBalance.DefaultView.RowFilter = filter;
                            if (dtBalance.DefaultView.Count == 1)
                            {
                                dtBalance.DefaultView.BeginInit();
                                if (IsOpBalance) //Opening Balance
                                {
                                    dtBalance.DefaultView[0][reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName] = amt;
                                }
                                dtBalance.DefaultView.EndInit();
                            }
                            dtBalance.DefaultView.RowFilter = string.Empty;
                        }
                    }
                }
                else //Closing Balance
                {
                    if (AppSetting.IS_CMF_CONGREGATION) //For CMF
                    {
                        dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName, typeof(decimal));
                        dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName, typeof(decimal), "AMOUNT1");
                    }
                    else//For All
                    {
                        dtBalance.Columns["AMOUNT1"].ColumnName = reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName;
                        dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.PROPOSED_AMOUNTColumn.ColumnName, typeof(decimal), reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName);
                        dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.ACTUAL_AMOUNTColumn.ColumnName, typeof(decimal), reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName);
                    }
                }

                //Include Previous year Columns with balances
                if (IncludePreviousYear)
                {
                    balancedate = IsOpBalance ? UtilityMember.DateSet.ToDate(this.ReportProperties.DateFrom, false).AddYears(-1).ToShortDateString()
                                        : UtilityMember.DateSet.ToDate(this.ReportProperties.DateTo, false).AddYears(-1).ToShortDateString();
                    dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName, typeof(System.Decimal));
                    dtBalance.Columns.Add(reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName, typeof(System.Decimal));
                    if (IsAnnualBalancesheetReport)
                        resultBalance = GetBalanceDetailProjectWise(IsOpBalance, balancedate, projects, fixedgrp);
                    else
                        resultBalance = GetBalanceDetail(IsOpBalance, balancedate, projects, fixedgrp);

                    if (resultBalance.Success && resultBalance.DataSource.Table != null)
                    {
                        DataTable dtPrevBalance = resultBalance.DataSource.Table;
                        foreach (DataRow dr in dtPrevBalance.Rows)
                        {
                            Int32 ledgerid = UtilityMember.NumberSet.ToInteger(dr[reportSetting1.Receipts.LEDGER_IDColumn.ColumnName].ToString());
                            Int32 projectid = 0;
                            if (IsAnnualBalancesheetReport)
                            {
                                projectid = UtilityMember.NumberSet.ToInteger(dr["PROJECT_ID"].ToString());
                            }
                            string transmode = dr[reportSetting1.AccountBalance.TRANS_MODEColumn.ColumnName].ToString();
                            Double amt = UtilityMember.NumberSet.ToDouble(dr[reportSetting1.AccountBalance.AMOUNTColumn.ColumnName].ToString());
                            amt = amt * (transmode.ToUpper() == TransMode.CR.ToString().ToUpper() ? -1 : 1);
                            string filter = reportSetting1.Receipts.LEDGER_IDColumn.ColumnName + " = " + ledgerid;
                            if (IsAnnualBalancesheetReport && projectid > 0)
                            {
                                filter += " AND PROJECT_ID = " + projectid;
                            }
                            dtBalance.DefaultView.RowFilter = string.Empty;
                            dtBalance.DefaultView.RowFilter = filter;
                            if (dtBalance.DefaultView.Count == 1)
                            {
                                dtBalance.DefaultView.BeginInit();
                                if (IsOpBalance) //Opening Balance
                                {
                                    dtBalance.DefaultView[0][reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName] = amt;
                                }
                                else //for Closing Balance
                                {
                                    if (!AppSetting.IS_CMF_CONGREGATION) //For CMF alone
                                    {
                                        dtBalance.DefaultView[0][reportSetting1.BUDGETVARIANCE.PREV_APPROVED_AMOUNTColumn.ColumnName] = amt;
                                    }
                                }
                                dtBalance.DefaultView[0][reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName] = amt;
                                dtBalance.DefaultView.EndInit();
                            }
                            dtBalance.DefaultView.RowFilter = string.Empty;
                        }
                    }
                }

                //If Balance Sheet Budget Report, all the cash/bank/fd captions will be changed as Opening and Closing 
                if (IsAnnualBalancesheetReport)
                {
                    //foreach (DataRow dr in dtBalance.Rows)
                    //{
                    //    dr[reportSetting1.BUDGETVARIANCE.LEDGER_GROUPColumn.ColumnName] = IsOpBalance ? "Opening Balance" : "Closing Balance";
                    //    dtBalance.AcceptChanges();
                    //}
                }

                //On 20/06/2023 - To Attach COST CENTRE ID -------------------------------------------------------------------------------
                if (this.ReportProperties.ShowCCDetails == 1)
                {
                    if (!dtBalance.Columns.Contains(this.reportSetting1.BUDGETVARIANCE.COST_CENTRE_IDColumn.ColumnName))
                    {
                        DataColumn dcCCId = new DataColumn(reportSetting1.BUDGETVARIANCE.COST_CENTRE_IDColumn.ColumnName, typeof(System.Decimal));
                        dcCCId.DefaultValue = 0;
                        dtBalance.Columns.Add(dcCCId);
                    }
                }
                //------------------------------------------------------------------------------------------------------------------------

                dtRpt.Merge(dtBalance.DefaultView.ToTable());

                if (IncludePreviousYear)
                {
                    //dtRpt.DefaultView.RowFilter = "(" + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + "<>0 OR " + reportSetting1.BUDGETVARIANCE.PREV_ACTUAL_SPENTColumn.ColumnName + "<>0)";
                }
                else
                {
                    //dtRpt.DefaultView.RowFilter = "(" + reportSetting1.BUDGETVARIANCE.APPROVED_AMOUNTColumn.ColumnName + "<>0)";
                }
            }
        }

        /// <summary>
        /// On 26/08/2020, to fix few properties for CMF by default
        /// </summary>
        public void FixReportPropertyForCMF()
        {
            // 21/05/2025, *Chinna
            if (this.AppSetting.IS_CMF_CONGREGATION && (!this.AppSetting.IS_CMFBPI))
            {
                this.ReportProperties.SortByLedger = 1;
                //On 26/09/2023, Don't force "Include All Ledgers" for Monthly Abstact Receipts and Payments
                if (ReportProperty.Current.ReportId != "RPT-003")
                    this.ReportProperties.IncludeAllLedger = 1;
                this.ReportProperties.ShowPageNumber = 1;
                this.ReportProperties.ShowProjectsinFooter = 1;
                this.ReportProperties.ShowReportDate = 1;
                this.SetReportDate = this.ReportProperties.ReportDate = this.UtilityMember.DateSet.ToDate(DateTime.Today.ToShortDateString());
                this.ReportProperties.IncludeSignDetails = 1;
                this.ReportProperties.ShowLedgerCode = 0;
            }
        }

        public DataTable GetReportSetting(string RptId)
        {
            ReportSetting.ReportSettingDataTable dtReportSettingSchema = new ReportSetting.ReportSettingDataTable();
            string reportSettingFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ReportSetting.xml"); //"ReportSetting.xml"; //Application.StartupPath.ToString()// Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            DataTable dtReportSettingInfo = new DataTable();
            dtReportSettingInfo = dtReportSettingSchema.Copy();
            dtReportSettingInfo.ReadXml(reportSettingFile);
            DataView dvReportSettingInfo = dtReportSettingInfo.DefaultView;
            dvReportSettingInfo.RowFilter = dtReportSettingSchema.ReportIdColumn.ColumnName + "='" + RptId + "'";
            DataTable dtRptSetting = dvReportSettingInfo.ToTable();
            return dtRptSetting;
        }

        public ResultArgs GetProjects(DateTime date)
        {
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjects))
            {
                //On 28/06/2018, This property is used to skip bank ledger's which is closed on or equal to this date
                dataManager.Parameters.Add(ReportParameters.DATE_CLOSEDColumn, date);
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 24/09/2020, Fix Export File Name  
        /// </summary>
        private void SetExportFileName(string rpttitle)
        {
            //On 24/09/2020, Fix Export File Name  -------------------------------------------------
            string RptTitle = rpttitle.Trim();
            int RptYearFrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).Year;
            int RptYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).Year;
            string RptYearDuriation = RptYearFrom.ToString();
            this.DisplayName = AppSetting.InstituteName + " - " + RptTitle;

            //Year From
            if (!String.IsNullOrEmpty(ReportProperty.Current.DateAsOn) && ReportProperty.Current.ReportCriteria.Contains("DAÿ"))
            {
                RptYearFrom = UtilityMember.DateSet.ToDate(ReportProperty.Current.DateAsOn, false).Year;
            }
            else if (!String.IsNullOrEmpty(ReportProperty.Current.DateFrom) && ReportProperty.Current.ReportCriteria.Contains("DFÿ"))
            {
                RptYearFrom = UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false).Year;
            }
            else if (!String.IsNullOrEmpty(ReportProperty.Current.ReportDate) && ReportProperty.Current.ReportCriteria.Contains("DFÿ"))
            {
                RptYearFrom = UtilityMember.DateSet.ToDate(ReportProperty.Current.ReportDate, false).Year;
            }

            //Year To
            if (!String.IsNullOrEmpty(ReportProperty.Current.DateTo) && ReportProperty.Current.ReportCriteria.Contains("DTÿ"))
            {
                RptYearTo = UtilityMember.DateSet.ToDate(ReportProperty.Current.DateTo, false).Year;
            }

            if (RptYearFrom != RptYearTo)
            {
                RptYearDuriation = RptYearFrom.ToString() + "-" + RptYearTo.ToString();
            }
            else
            {
                RptYearDuriation = RptYearFrom.ToString();
            }


            if (ReportProperty.Current.SelectedProjectCount > 1)
            {
                this.DisplayName = AppSetting.InstituteName + " - Consolidated " + RptTitle + " - " + RptYearDuriation;
            }
            else
            {
                this.DisplayName = string.IsNullOrEmpty(ReportProperty.Current.ProjectNameWithoutDivision) ? "" : ReportProperty.Current.ProjectNameWithoutDivision.Trim() + " - " + RptTitle + " - " + RptYearDuriation;
            }

            //replace special characters which are not valid for file names
            this.DisplayName = this.DisplayName.Replace("/", "").Replace("*", "");
            //--------------------------------------------------------------------------------------
        }

        public void HideTableCell(XRTable tbl, XRTableRow row, XRTableCell cell)
        {
            tbl.SuspendLayout();
            if (row.Cells.Contains(cell))
                row.Cells.Remove(row.Cells[cell.Name]);
            tbl.PerformLayout();
        }

        public void RemoveTOCFromReport()
        {
            if (ReportHeader.Controls["ReportTOC"] != null)
            {
                XRControl ctlTOC = ReportHeader.Controls["ReportTOC"];
                ReportHeader.Controls.Remove(ctlTOC);
            }
        }

        public void AttachTOCToReport()
        {
            RemoveTOCFromReport();

            XRTableOfContents toc = new XRTableOfContents();
            XRTableOfContentsLevel tocLevel1 = new XRTableOfContentsLevel();
            XRTableOfContentsLevel tocLevel2 = new XRTableOfContentsLevel();
            toc.BeforePrint += new System.Drawing.Printing.PrintEventHandler(toc_BeforePrint);
            toc.Name = "ReportTOC";

            string TOCtitle = xrInstituteName.Text + System.Environment.NewLine +
                              xrInstituteAddress.Text + System.Environment.NewLine +
                              xrlblReportTitle.Text + System.Environment.NewLine +
                              xrDateRange.Text + System.Environment.NewLine +
                              xrlblReportSubTitle.Text + System.Environment.NewLine;

            toc.LevelTitle.Text = TOCtitle + System.Environment.NewLine + "Table of Content";

            toc.LevelTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            toc.LevelTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            toc.LevelTitle.Padding = new PaddingInfo(25, 25, 0, 0);

            tocLevel1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tocLevel1.Padding = new PaddingInfo(25, 25, 0, 0);
            toc.Levels.Add(tocLevel1);

            toc.Borders = BorderSide.Bottom | BorderSide.Left | BorderSide.Right;
            toc.BorderColor = Color.Silver;
            //toc.BookmarkParent.Borders = BorderSide.Top | BorderSide.Left | BorderSide.Right;
            // toc.BookmarkParent.BorderColor = Color.Silver;

            ReportHeader.Controls.Add(toc);
        }

        public void toc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }



        public void RemoveTableCell(XRTable tbl, XRTableRow row, XRTableCell cell)
        {
            tbl.BeginInit();
            tbl.SuspendLayout();
            if (row.Cells.Contains(cell))
                row.Cells.Remove(row.Cells[cell.Name]);
            tbl.PerformLayout();
            tbl.EndInit();
        }
    }
}