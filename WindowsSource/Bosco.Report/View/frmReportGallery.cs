using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Report.Base;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars;

using Bosco.Utility.ConfigSetting;
using DevExpress.Utils;

namespace Bosco.Report.View
{
    public partial class frmReportGallery : DevExpress.XtraEditors.XtraForm
    {
        private CommonMember utilityMember = null;
        UserProperty settinguserpropertyproperty = new UserProperty();
        private DevExpress.Utils.ToolTipController ToolTipDisabledController { get { return DevExpress.Utils.ToolTipController.DefaultController; } }
        private bool LockedAnyLicenseBasedReport = false;
        private DevExpress.XtraBars.BarSubItem barSubMenuItemLocked = null;
        private List<Control> ToolTipDisabledControls = new List<Control>();


        public frmReportGallery()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        /// This property is used to assign selected report name in mdi parent title
        /// It finds parent title property and assign dynamically.
        /// If we inherit base class, we can use directly
        /// </summary>
        public string MDIPageTitle
        {
            set
            {
                String property = "PageTitle";
                Form mdiMain = this.MdiParent;
                try
                {
                    if (mdiMain != null)
                    {
                        var prop = mdiMain.GetType().GetProperty(property);
                        if (prop != null)
                        {
                            var convertedValue = Convert.ChangeType(value, prop.PropertyType);
                            prop.SetValue(mdiMain, convertedValue, null);
                        }
                    }
                }
                catch (Exception err)
                {
                    string errDescription = err.Message;
                }
            }
        }
        #endregion

        private CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        public DockVisibility dockReportDescription
        {
            set { dpReportDescription.Visibility = value; }
        }

        private void frmReportGallery_Load(object sender, EventArgs e)
        {
            // CollapseDockManager();
            //  LoadReportMenu();
            LoadReportMenuBarMenu();
            FetchLedgalEntity();
            EnableProjectTitle();
        }
        private void LoadReportMenu()
        {
            DataView dvReports = ReportProperty.Current.ReportSettingInfo;
            string reportId = "";
            string reportGroupName = "";
            string reportGroupNameLast = "";
            string reportName = "";
            string reportTitle = "";
            string reportDescription = "";
            ReportSetting.ReportSettingDataTable reportSettingSchema = new ReportSetting.ReportSettingDataTable();
            DevExpress.XtraNavBar.NavBarGroup nvbGroup = null;
            nvbGroup1.Expanded = true;

            string ReportModule = UtilityMember.EnumSet.GetEnumItemNameByValue(typeof(ReportModule), UtilityMember.NumberSet.ToInteger(SettingProperty.ReportModuleId.ToString()));

            string DrilldownRpt = UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.DRILL_DOWN);
            dvReports.RowFilter = "ReportId<>'" + DrilldownRpt + "' AND Module='" + ReportModule + "'";
            foreach (DataRowView drvReport in dvReports)
            {
                reportId = drvReport[reportSettingSchema.ReportIdColumn.ColumnName].ToString();
                reportGroupName = drvReport[reportSettingSchema.ReportGroupColumn.ColumnName].ToString();
                reportName = drvReport[reportSettingSchema.ReportNameColumn.ColumnName].ToString();
                reportTitle = drvReport[reportSettingSchema.ReportTitleColumn.ColumnName].ToString();
                reportDescription = drvReport[reportSettingSchema.ReportDescriptionColumn.ColumnName].ToString();

                if (reportGroupName != reportGroupNameLast)
                {
                    if (reportGroupNameLast == "")
                    {
                        nvbGroup = nvbGroup1;
                    }
                    else
                    {
                        nvbGroup = new DevExpress.XtraNavBar.NavBarGroup(reportGroupName);
                        nvbGroup.Expanded = true;
                        nvbReport.Groups.Add(nvbGroup);
                        nvbGroup.Appearance.Font = nvbGroup1.Appearance.Font;
                    }

                    nvbGroup.Caption = reportGroupName;
                    reportGroupNameLast = reportGroupName;
                }

                DevExpress.XtraNavBar.NavBarItemLink linkItem = nvbGroup.AddItem();
                linkItem.Item.Caption = reportName;
                linkItem.Item.Tag = reportId;

                DevExpress.Utils.SuperToolTip superTip = new DevExpress.Utils.SuperToolTip();
                superTip.Items.AddTitle(reportTitle);
                superTip.Items.AddSeparator();
                superTip.Items.Add(reportDescription);
                linkItem.Item.SuperTip = superTip;
                linkItem.Item.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(Item_LinkClicked);
            }
        }

        private void Item_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            string reportId = "";

            if (e.Link.Item.Tag != null && e.Link.Item.ToString() != "")
            {
                reportId = e.Link.Item.Tag.ToString();
                this.Cursor = Cursors.WaitCursor;
                foreach (DevExpress.Utils.BaseToolTipItem toolTipItem in e.Link.Item.SuperTip.Items)
                {
                    if (toolTipItem.GetType() == typeof(DevExpress.Utils.ToolTipTitleItem))
                    {
                        DevExpress.Utils.ToolTipTitleItem tipTitle = (DevExpress.Utils.ToolTipTitleItem)toolTipItem;
                        lblReportTitle.Text = tipTitle.Text;
                        // lblTitle.Text = tipTitle.Text;
                    }
                    else if (toolTipItem.GetType() == typeof(DevExpress.Utils.ToolTipItem))
                    {
                        DevExpress.Utils.ToolTipItem tipContent = (DevExpress.Utils.ToolTipItem)toolTipItem;
                        lblDescription.Text = tipContent.Text;
                    }
                }
                rptViewer.ReportId = reportId;
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("No Report Found");
            }
        }

        public void CollapseDockManager()
        {
            dockManager1.BeginUpdate();
            dpReportMenu.Visibility = DockVisibility.AutoHide;
            // dpReportDescription.Visibility = DockVisibility.AutoHide;
            dpReportMenu.HideImmediately();
            dockManager1.EndUpdate();
        }

        private void LoadReportMenuBarMenu()
        {
            try
            {
                // string message = MessageRender.GetReportMessage(MessageCatalog.ReportCommonTitle.RPT_001_GROUP);
                DataView dvReports = ReportProperty.Current.ReportSettingInfo;
                string reportId = "";
                string reportGroupName = "";
                string reportGroupNameLast = "";
                string reportName = "";
                string reportTitle = "";
                string reportDescription = "";
                string ReportFilter = string.Empty;
                ReportSetting.ReportSettingDataTable reportSettingSchema = new ReportSetting.ReportSettingDataTable();
                DevExpress.XtraBars.BarSubItem barSubMenuItem = null;
                nvbGroup1.Expanded = true;

                string ReportModule = UtilityMember.EnumSet.GetEnumItemNameByValue(typeof(ReportModule), UtilityMember.NumberSet.ToInteger(SettingProperty.ReportModuleId.ToString()));

                string DrilldownRpt = UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.DRILL_DOWN);
                string TDSDrilldownRpt = UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.TDS_PARTY_DRILL_DOWN);
                string TDSLedgerDrilldownRpt = UtilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.TDS_LEDGER_DRILL_DOWN);
                string TDSNOP = utilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.TDS_NOP_DRILL_DOWN);
                string FCReport = utilityMember.EnumSet.GetDescriptionFromEnumValue(DrillDownType.FC_REPORT);

                // hide by chinna 23.07.2021
                // dvReports.RowFilter = String.Format("ReportId NOT IN('{0}') AND ReportId NOT IN('{1}') AND ReportId NOT IN('{3}') AND ReportId NOT IN('{4}') AND ReportId NOT IN('{5}') AND Module='{2}'",
                dvReports.RowFilter = String.Format("ReportId NOT IN('{0}') AND ReportId NOT IN('{1}') AND ReportId NOT IN('{3}') AND ReportId NOT IN('{4}') AND ReportId NOT IN('{5}') AND Module='{2}'",
                    DrilldownRpt, TDSDrilldownRpt, ReportModule, TDSLedgerDrilldownRpt, TDSNOP, FCReport);


                //dvReports.RowFilter = "ReportId NOT IN('" + DrilldownRpt + "') AND ReportId NOT IN('" + TDSDrilldownRpt + "') AND Module='" + ReportModule + "'";

                //On 04/03/2021, To load different Report format for Budget Annual Summary for Developmental Repors
                dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : "AND ReportId NOT IN ('RPT-189')";

                DataTable dtcheck = dvReports.ToTable();

                if (settinguserpropertyproperty.IS_ABEBEN_DIOCESE) //# 03/01/2019, Dont show all Budget reports for ABE license keys
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "ReportGroup NOT IN ('Budget')";
                }
                else if (settinguserpropertyproperty.IS_DIOMYS_DIOCESE) //# 03/01/2019, show all only few Budget reports for mysore license keys
                {
                    //dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + " (ReportGroup NOT IN ('Budget') OR ReportId IN ('RPT-163', 'RPT-180'))";
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + " (ReportGroup NOT IN ('Budget'))";
                }
                else if (settinguserpropertyproperty.IS_CMF_CONGREGATION) //# 03/09/2020, For CMF show only Their Budget reports
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "(ReportGroup NOT IN ('Budget') OR ReportID IN ('RPT-046', 'RPT-182', 'RPT-184', 'RPT-185', 'RPT-186'))";
                }
                else if (settinguserpropertyproperty.IsCountryOtherThanIndia) // To have few reports alone
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "(ReportGroup NOT IN ('Budget') OR ReportID IN ('RPT-046', 'RPT-182', 'RPT-184', 'RPT-185'))";
                }
                else //# 27/01/2020, For all, remove month and two months distribution reports 
                {
                    //Budget Monthly distribution (ABE, Mysore) reports
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + " ReportId NOT IN ('RPT-163','RPT-177', 'RPT-180')";
                }

                if ((!settinguserpropertyproperty.IS_FMA_CONGREGATION) && (!settinguserpropertyproperty.IS_FDCCSI))
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "(ReportGroup NOT IN ('Generalate'))";  //  OR ReportID IN ('RPT-191', 'RPT-218')
                }

                if ((!settinguserpropertyproperty.IS_SDB_INM))
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "(ReportGroup NOT IN ('Monitoring'))";  //  OR ReportID IN ('RPT-191', 'RPT-218')
                }

                if (settinguserpropertyproperty.IS_FMA_CONGREGATION)
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "(ReportId NOT IN ('RPT-222', 'RPT-223', 'RPT-224', 'RPT-225'))";
                }

                if (settinguserpropertyproperty.IS_FDCCSI)
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "(ReportId NOT IN ( 'RPT-191','RPT-218'))";
                }

                //On 11/09/2023, To show GST Invoice based on the settings-----------------------------------------------------------------
                if (settinguserpropertyproperty.IncludeGSTVendorInvoiceDetails == "1")//for R&P GST Invoice
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + " ReportId NOT IN ('RPT-207', 'RPT-235')"; //Hide Journal GST Invoice
                else if (settinguserpropertyproperty.IncludeGSTVendorInvoiceDetails == "2") //for Journal GST Invoice
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + " ReportId NOT IN ('RPT-212')"; //Hide R&P GST Invoice
                else
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + " ReportId NOT IN ('RPT-207', 'RPT-212', 'RPT-235')"; //Hide R&P GST Invoice

                //-------------------------------------------------------------------------------------------------------------------------

                //Remove Audit related reports for general users
                if (!settinguserpropertyproperty.IsFullRightsReservedUser)
                {
                    //dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + "(ReportGroup NOT IN ('Audit'))";
                    //For General user, let us lock real audit reports 
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : " AND " + " ReportId NOT IN ('RPT-142', 'RPT-194', 'RPT-195', 'RPT-196', 'RPT-197', 'RPT-211')";
                }

                if ((!settinguserpropertyproperty.IsFullRightsReservedUser) && ReportProperty.Current.NumberSet.ToInteger(settinguserpropertyproperty.LoginUserId) != 0 && !string.IsNullOrEmpty(settinguserpropertyproperty.LoginUserId))
                {
                    dvReports = dvReports.ToTable().DefaultView;
                    //if (ReportModule.Equals("Finance"))
                    if (ReportModule.Equals(MessageRender.GetMessage(MessageCatalog.Master.Module.MODULE_FINANCE_TITLE)))
                    {
                        DataTable dtReports = CommonMethod.ApplyUserRightsForForms((int)Bosco.Report.SQL.ReportSQLCommand.UserRights.Reports);
                        if (dtReports != null && dtReports.Rows.Count != 0)
                        {
                            dtReports.DefaultView.RowFilter = "ENUMTYPE NOT IN ('AssetReports')";
                            dtReports = dtReports.DefaultView.ToTable();
                            dtReports.AcceptChanges();
                            foreach (DataRow dr in dtReports.Rows)
                            {
                                ReportFilter += dr["ACTIVITY_ID"] != DBNull.Value ? dr["ACTIVITY_ID"].ToString() + "," : string.Empty;
                            }

                            //On 29/11/2023, To fix Audit group for all the user, it will be later converted user based by chinna
                            ReportFilter += ((int)Bosco.Report.SQL.ReportSQLCommand.UserRights.Audit).ToString();
                            ReportFilter = ReportFilter.TrimEnd(',');
                            dvReports.RowFilter = "ParentId IN (" + ReportFilter + ") ";
                        }
                    }
                    else if (ReportModule.Equals("FixedAsset"))
                    {
                        DataTable dtReports = CommonMethod.ApplyUserRightsForForms((int)Bosco.Report.SQL.ReportSQLCommand.UserRights.Reports);
                        if (dtReports != null && dtReports.Rows.Count != 0)
                        {
                            string ModuleEnumvalue = "AssetReports";
                            DataRow[] foundAuthors = dtReports.Select("ENUMTYPE = '" + ModuleEnumvalue + "'");

                            if (foundAuthors.Length != 0)
                            {
                                dtReports.DefaultView.RowFilter = "ENUMTYPE IN ('AssetReports')";
                            }
                            else
                            {
                                dtReports.DefaultView.RowFilter = "";
                            }
                            dtReports = dtReports.DefaultView.ToTable();
                            dtReports.AcceptChanges();
                            foreach (DataRow dr in dtReports.Rows)
                            {
                                ReportFilter += dr["ACTIVITY_ID"] != DBNull.Value ? dr["ACTIVITY_ID"].ToString() + "," : string.Empty;
                            }
                            ReportFilter = ReportFilter.TrimEnd(',');
                            dvReports.RowFilter = "ParentId IN (" + ReportFilter + ") ";
                        }
                    }
                    //else if (ReportModule.Equals("Stock"))
                    //{
                    //    DataTable dtReports = CommonMethod.ApplyUserRightsForForms((int)Bosco.Report.SQL.ReportSQLCommand.UserRights.Reports);
                    //    if (dtReports != null && dtReports.Rows.Count != 0)
                    //    {
                    //        string ModuleEnumvalue = "StockReports";
                    //        DataRow[] foundAuthors = dtReports.Select("ENUMTYPE = '" + ModuleEnumvalue + "'");

                    //        if (foundAuthors.Length != 0)
                    //        {
                    //            dtReports.DefaultView.RowFilter = "ENUMTYPE IN ('StockReports')";

                    //            dtReports = dtReports.DefaultView.ToTable();
                    //            dtReports.AcceptChanges();
                    //            foreach (DataRow dr in dtReports.Rows)
                    //            {
                    //                ReportFilter += dr["ACTIVITY_ID"] != DBNull.Value ? dr["ACTIVITY_ID"].ToString() + "," : string.Empty;
                    //            }
                    //            ReportFilter = ReportFilter.TrimEnd(',');
                    //            dvReports.RowFilter = "ParentId IN (" + ReportFilter + ") ";
                    //        }
                    //        else
                    //        {
                    //            dvReports.RowFilter = "";
                    //            dvReports = new DataView();
                    //        }
                    //    }
                    //}
                    //else if (ReportModule.Equals("Payroll"))
                    //{
                    //    DataTable dtReports = CommonMethod.ApplyUserRightsForForms((int)Bosco.Report.SQL.ReportSQLCommand.UserRights.Reports);
                    //    if (dtReports != null && dtReports.Rows.Count != 0)
                    //    {
                    //        string ModuleEnumvalue = "PayrollReports";
                    //        DataRow[] foundAuthors = dtReports.Select("ENUMTYPE = '" + ModuleEnumvalue + "'");

                    //        if (foundAuthors.Length != 0)
                    //        {
                    //            dtReports.DefaultView.RowFilter = "ENUMTYPE IN ('PayrollReports')";

                    //            dtReports = dtReports.DefaultView.ToTable();
                    //            dtReports.AcceptChanges();
                    //            foreach (DataRow dr in dtReports.Rows)
                    //            {
                    //                ReportFilter += dr["ACTIVITY_ID"] != DBNull.Value ? dr["ACTIVITY_ID"].ToString() + "," : string.Empty;
                    //            }
                    //            ReportFilter = ReportFilter.TrimEnd(',');
                    //            dvReports.RowFilter = "ParentId IN (" + ReportFilter + ") ";
                    //        }
                    //        else
                    //        {
                    //            dvReports.RowFilter = "";
                    //            dvReports = new DataView();
                    //        }
                    //    }
                    //}
                    //else if (ReportModule.Equals("NetWorking"))
                    //{
                    //    DataTable dtReports = CommonMethod.ApplyUserRightsForForms((int)Bosco.Report.SQL.ReportSQLCommand.UserRights.Reports);
                    //    if (dtReports != null && dtReports.Rows.Count != 0)
                    //    {
                    //        string ModuleEnumvalue = "NetworkingReports";
                    //        DataRow[] foundAuthors = dtReports.Select("ENUMTYPE = '" + ModuleEnumvalue + "'");

                    //        if (foundAuthors.Length != 0)
                    //        {
                    //            dtReports.DefaultView.RowFilter = "ENUMTYPE IN ('NetworkingReports')";

                    //            dtReports = dtReports.DefaultView.ToTable();
                    //            dtReports.AcceptChanges();
                    //            foreach (DataRow dr in dtReports.Rows)
                    //            {
                    //                ReportFilter += dr["ACTIVITY_ID"] != DBNull.Value ? dr["ACTIVITY_ID"].ToString() + "," : string.Empty;
                    //            }
                    //            ReportFilter = ReportFilter.TrimEnd(',');
                    //            dvReports.RowFilter = "ParentId IN (" + ReportFilter + ") ";
                    //        }
                    //        else
                    //        {
                    //            dvReports.RowFilter = "";
                    //            dvReports = new DataView();
                    //        }
                    //    }
                    //}
                }

                //On 03/09/3034, To show Currency enabled Reports alone ---------------------------------------------------------------------------
                if (settinguserpropertyproperty.AllowMultiCurrency == 1)
                {
                    //Abstract, Foreign Contribution
                    string repotids = "'RPT-013', 'RPT-014', 'RPT-183', 'RPT-159', 'RPT-160'," +
                                      "'RPT-016', 'RPT-017', 'RPT-062', 'RPT-012', 'RPT-052'," +
                                      "'RPT-030', 'RPT-027', 'RPT-028', 'RPT-031'," +
                                      "'RPT-024', 'RPT-025', 'RPT-151', 'RPT-026'," +
                                      "'RPT-015', 'RPT-047', 'RPT-094', 'RPT-150', 'RPT-161', 'RPT-204'," + //for fd related
                                      "'RPT-046', 'RPT-182', 'RPT-184', 'RPT-185'," + //for budget related , 'RPT-186','RPT-193'
                                      "'RPT-235'";   //Journal Invoice Status

                    string notusedreports = "'RPT-171','RPT-172', 'RPT-202', 'RPT-203'";

                    //"'RPT-078', 'RPT-051', 'RPT-041', 'RPT-050'"; //, 'Cost Centre'
                    string currencyenabledReports = "(" + reportSettingSchema.ReportGroupColumn.ColumnName + " IN ('Abstract', 'Foreign Contribution', 'Cost Centre') OR " +
                                 reportSettingSchema.ReportIdColumn.ColumnName + " IN (" + repotids + ") OR " + reportSettingSchema.ModuleColumn.ColumnName + " <> 'Finance') AND "+
                                 reportSettingSchema.ReportIdColumn.ColumnName + " NOT IN (" + notusedreports + ")";

                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : "AND " + currencyenabledReports;
                }
                
                //10/02/2025 Lock budget cc reports ----------------------------------------------------------------
                if (settinguserpropertyproperty.EnableCostCentreBudget == 0)
                {
                    dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : "AND " + reportSettingSchema.ReportIdColumn.ColumnName + " NOT IN ('RPT-236', 'RPT-237')";
                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------

                //Multi Abstract Receipts and Payments - Currency-wise (to be discussed)
                dvReports.RowFilter += string.IsNullOrEmpty(dvReports.RowFilter) ? "" : "AND " + reportSettingSchema.ReportIdColumn.ColumnName + " NOT IN ('RPT-220')";
                //---------------------------------------------------------------------------------------------------------------------------------

                foreach (DataRowView drvReport in dvReports)
                {
                    reportId = drvReport[reportSettingSchema.ReportIdColumn.ColumnName].ToString();
                    int ParentId = ReportProperty.Current.NumberSet.ToInteger(drvReport[reportSettingSchema.ParentIdColumn.ColumnName].ToString());
                    string ParentLanguageId = "RPT_GRP_" + ParentId;
                    string ReportLangTitleId = reportId.Replace("-", "_") + "_TITLE";
                    string ReportLangDescriptionId = reportId.Replace("-", "_") + "_DESCRIPTION";
                    int LicenseBased = ReportProperty.Current.NumberSet.ToInteger(drvReport[reportSettingSchema.LicenseBasedColumn.ColumnName].ToString());
                    string[] LicenseBasedReports = settinguserpropertyproperty.EnabledReports.Split('@');

                    //if (reportId == "RPT-216")
                    //{
                    //    Int32 a = Array.IndexOf(LicenseBasedReports, reportId);
                    //}

                    bool DisableReport = (settinguserpropertyproperty.IS_SDB_INM || LicenseBased == 0 ||
                                            (LicenseBased == 1 && Array.IndexOf(LicenseBasedReports, reportId) >= 0));

                    if (ParentId.Equals((int)ModuleActivities.BookofAccounts))
                    {
                        reportGroupName = "Boo&k of Accounts";
                    }
                    else if (ParentId.Equals((int)ModuleActivities.Budget))
                    {
                        reportGroupName = "Bu&dget";
                    }
                    else if (ParentId.Equals((int)ModuleActivities.FinancialRecords))
                    {
                        reportGroupName = "Financial &Records";
                    }
                    else if (ParentId.Equals((int)ModuleActivities.ForeignContribution))
                    {
                        reportGroupName = "&Contribution"; //Foreig&n Contribution";
                    }
                    else if (ParentId.Equals((int)ModuleActivities.AssetReports))
                    {
                        reportGroupName = "Asset &Reports";
                    }
                    else if (ParentId.Equals((int)ModuleActivities.StockReports))
                    {
                        reportGroupName = "Stock &Reports";
                    }
                    else if (ParentId.Equals(208))
                    {
                        reportGroupName = "C&hallan Reconciliation";
                    }
                    else if (ParentId.Equals((int)Moudule.Payroll))
                    {
                        reportGroupName = "Payroll";
                    }
                    else if (ParentId.Equals(212))
                    {
                        reportGroupName = "M&ailing";
                    }
                    else
                    {
                        reportGroupName = "&" + drvReport[reportSettingSchema.ReportGroupColumn.ColumnName].ToString();
                    }

                    ///
                    if (reportGroupName.IndexOf("&") >= 0)
                    {
                        string shortcurkey = reportGroupName.Substring(reportGroupName.IndexOf("&"), 2);
                        reportGroupName = reportGroupName.Replace(shortcurkey, "<b>" + shortcurkey + "</b>");
                    }

                    reportName = "&" + drvReport[reportSettingSchema.ReportNameColumn.ColumnName].ToString();
                    reportTitle = drvReport[reportSettingSchema.ReportTitleColumn.ColumnName].ToString();
                    reportDescription = drvReport[reportSettingSchema.ReportDescriptionColumn.ColumnName].ToString();

                    if (reportGroupName != reportGroupNameLast)
                    {
                        if (reportGroupNameLast == "")
                        {
                            barSubMenuItem = barSubItemReports;
                        }
                        else
                        {
                            barReportMenu.AllowHtmlText = true;
                            barSubMenuItem = new DevExpress.XtraBars.BarSubItem(barReportMenu, reportGroupName);
                            barReportMenu.Items.Add(barSubMenuItem);
                            barMenu.ItemLinks.Add(barSubMenuItem, true);
                            barSubMenuItem.ItemAppearance.Normal.Font = barSubItemReports.ItemAppearance.Normal.Font;
                        }
                        //barSubMenuItem.Caption = reportGroupName;
                        //reportGroupNameLast = reportGroupName;

                        if (this.settinguserpropertyproperty.UILanguage.Equals("fr-FR"))
                        {
                            //string GroupNames = "RPT_GRP_" + reportGroupName;
                            string RptMessages = MessageRender.GetReportMessage(ParentLanguageId);
                            barSubMenuItem.Caption = string.IsNullOrEmpty(RptMessages) ? reportGroupName : RptMessages;
                        }
                        else
                        {
                            barSubMenuItem.Caption = reportGroupName;
                        }
                        //For Audit group of reports, Show Hightlight image ----------------------------------------------
                        string auditgrpid = ((int)Bosco.Report.SQL.ReportSQLCommand.UserRights.Audit).ToString();
                        if (ParentId.ToString().Equals(auditgrpid) && string.IsNullOrEmpty(settinguserpropertyproperty.EnabledReports) && !settinguserpropertyproperty.IS_SDB_INM)
                        {
                            barSubMenuItem.Glyph = Report.Properties.Resources.LatestVersion;
                            barSubMenuItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                        }
                        //------------------------------------------------------------------------------------------------
                        reportGroupNameLast = reportGroupName;


                        //if (reportGroupNameLast == "")
                        //{
                        //    barSubMenuItem = barSubItemReports;
                        //}
                        //else
                        //{
                        //    barReportMenu.AllowHtmlText = true;
                        //    barSubMenuItem = new DevExpress.XtraBars.BarSubItem(barReportMenu, reportGroupName);
                        //    barReportMenu.Items.Add(barSubMenuItem);
                        //    barMenu.ItemLinks.Add(barSubMenuItem, true);
                        //    barSubMenuItem.ItemAppearance.Normal.Font = barSubItemReports.ItemAppearance.Normal.Font;
                        //}
                        //if (this.settinguserpropertyproperty.UILanguage.Equals("fr-FR"))
                        //{
                        //    string RptMessages = MessageRender.GetReportMessage(ParentLanguageId);
                        //    barSubMenuItem.Caption = string.IsNullOrEmpty(RptMessages) ? reportGroupName : RptMessages;
                        //}
                        //else
                        //{
                        //    barSubMenuItem.Caption = reportGroupName;
                        //}

                        //reportGroupNameLast = reportGroupName;

                    }

                    if (settinguserpropertyproperty.UILanguage == "fr-FR")
                    {
                        string RptTitleMessage = MessageRender.GetReportMessage(ReportLangTitleId);
                        reportTitle = string.IsNullOrEmpty(RptTitleMessage) ? reportTitle : RptTitleMessage;

                        string RptDescriptions = MessageRender.GetReportMessage(ReportLangDescriptionId);
                        reportDescription = string.IsNullOrEmpty(RptDescriptions) ? reportDescription : RptDescriptions;
                    }

                    BarButtonItem subMenu = new BarButtonItem(barReportMenu, reportTitle);
                    subMenu.ItemClick += new ItemClickEventHandler(subMenu_ItemClick);
                    barReportMenu.Items.Add(subMenu);

                    //Separator Line)
                    if (reportId == "RPT-004" || reportId == "RPT-013" || reportId == "RPT-015" || reportId == "RPT-012" || reportId == "RPT-018" || reportId == "RPT-043"
                        || reportId == "RPT-030" || reportId == "RPT-035" || reportId == "RPT-027" || reportId == "RPT-035" || reportId == "RPT-050" || reportId == "RPT-031" || reportId == "RPT-038"
                        || reportId == "RPT-139" || reportId == "RPT-041" || reportId == "RPT-044" || reportId == "RPT-051" || reportId == "RPT-052" || reportId == "RPT-057"
                        || reportId == "RPT-064" || reportId == "RPT-066" || reportId == "RPT-126" || reportId == "RPT-124" || reportId == "RPT-146" || reportId == "RPT-147"
                        || reportId == "RPT-148" || reportId == "RPT-149" || reportId == "RPT-153" || reportId == "RPT-155" || reportId == "RPT-159" || reportId == "RPT-142" || reportId == "RPT-053"
                        || reportId == "RPT-162" || reportId == "RPT-164" || reportId == "RPT-072" || reportId == "RPT-170" || reportId == "RPT-168" || reportId == "RPT-174"
                        || reportId == "RPT-184" || reportId == "RPT-189" || reportId == "RPT-200" || reportId == "RPT-203" || reportId == "RPT-209" || reportId == "RPT-146"
                        || reportId == "RPT-208" || reportId == "RPT-196" || reportId == "RPT-220" || reportId == "RPT-235" || reportId == "RPT-236")
                    {
                        barSubMenuItem.ItemLinks.Add(subMenu, true);
                    }
                    else
                    {
                        barSubMenuItem.ItemLinks.Add(subMenu);
                    }
                    subMenu.Tag = reportId;
                    DevExpress.Utils.SuperToolTip superTip = new DevExpress.Utils.SuperToolTip();

                    superTip.Items.AddTitle(reportTitle);
                    superTip.Items.AddSeparator();

                    superTip.Items.Add(reportDescription);
                    subMenu.SuperTip = superTip;

                    EnableReceiptModule(reportId, subMenu);
                    if (!DisableReport)
                    {
                        subMenu.Enabled = false;
                    }
                }
                dvReports.RowFilter = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        void subMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            ReportProperty.Current.CashBankVoucher = null;
            string reportId = "";
            if (e.Link.Item.Tag != null && e.Link.Item.ToString() != "")
            {
                reportId = e.Link.Item.Tag.ToString();
                this.Cursor = Cursors.WaitCursor;
                foreach (DevExpress.Utils.BaseToolTipItem toolTipItem in e.Link.Item.SuperTip.Items)
                {
                    if (toolTipItem.GetType() == typeof(DevExpress.Utils.ToolTipTitleItem))
                    {
                        DevExpress.Utils.ToolTipTitleItem tipTitle = (DevExpress.Utils.ToolTipTitleItem)toolTipItem;
                        lblReportTitle.Text = tipTitle.Text;
                        this.Text = "Report " + " - " + tipTitle.Text;
                        // lblTitle.Text = tipTitle.Text;
                    }
                    else if (toolTipItem.GetType() == typeof(DevExpress.Utils.ToolTipItem))
                    {
                        DevExpress.Utils.ToolTipItem tipContent = (DevExpress.Utils.ToolTipItem)toolTipItem;
                        lblDescription.Text = tipContent.Text;
                    }
                }
                ReportProperty.Current.ReportFlag = 1;
                rptViewer.ReportId = reportId;
                //On 04/03/2021, To load different Report format for Budget Annual Summary for Developmental Repors
                if (settinguserpropertyproperty.ConsiderBudgetNewProject == 1 && reportId == "RPT-179")
                {
                    rptViewer.ReportId = "RPT-189";
                }
                else if (SettingProperty.Current.ConsiderBudgetNewProject == 0 && reportId == "RPT-189")
                {
                    rptViewer.ReportId = "RPT-179";
                }

                MDIPageTitle = this.Text;
                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("No Report Found");
            }
        }

        public void FetchLedgalEntity()
        {
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.FetchAll))
            {
                ResultArgs resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
                ReportProperty.dtLedgerEntity = resultArgs.DataSource.Table;
            }
        }

        public void EnableProjectTitle()
        {
            if (settinguserpropertyproperty.HeadofficeCode.Equals("bsgnei"))
            {
                ReportProperty.Current.isShowProjectTitle = true;
            }
            else
            {
                ReportProperty.Current.isShowProjectTitle = false;
            }
        }

        public void EnableReceiptModule(string reportId, BarButtonItem subMenu)
        {
            if (this.settinguserpropertyproperty.IS_SDB_INM)
            {
                if (!settinguserpropertyproperty.ENABLE_TRACK_RECEIPT_MODULE)
                {
                    if (reportId == "RPT-024")
                    {
                        subMenu.Enabled = false;
                        subMenu.SuperTip.Items.Add(MessageCatalog.Common.COMMON_RECEIPT_DISABLED_MESSAGE);
                    }
                }
            }
        }

        private void frmReportGallery_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void barReportMenu_HighlightedLinkChanged(object sender, HighlightedLinkChangedEventArgs e)
        {
            if (sender != null)
            {
                ToolTipController tooltopcontroler = new ToolTipController();
                BarManager barmanager = sender as BarManager;
                tooltopcontroler.HideHint();
                if (barmanager.HighlightedLink != null && !barmanager.HighlightedLink.Enabled)
                {
                    tooltopcontroler.ShowHint("This Report will be enabled based on License, Contact Acme.erp Support Team.", "Licensed Report");
                }
            }
        }


    }
}
