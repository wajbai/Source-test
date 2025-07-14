using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Payroll.Model.UIModel;
using Bosco.Utility;
using Bosco.Utility.Common;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using Bosco.Utility.ConfigSetting;
using System.Data;
using DevExpress.XtraEditors.Repository;
using System.Text.RegularExpressions;
using System.Globalization;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;
using System.IO;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace PAYROLL
{
    public partial class frmPayrollBase : DevExpress.XtraEditors.XtraForm
    {
        #region Varaible Declaration
        public event EventHandler ShowFilterClicked;
        public event EventHandler EnterClicked;
        CommonMember utilityMember = null;
        SettingProperty appsetting = null;
        UserProperty loginuser = null;
        bool CheckFilter = true;
        private string PrintPageTitle = string.Empty;
        public bool Iswaitformshown = false;

        public List<Enum> enumUserRights = new List<Enum>();
        public bool isAddable = false;
        public bool isEditable = false;
        public bool isDeleteable = false;
        public bool isPrintable = false;


        #endregion

        #region Constructor
        public frmPayrollBase()
        {
            InitializeComponent();
        }
        #endregion

        #region Property



        #endregion

        #region Methods
        
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            EventArgs e = new EventArgs();
            if (CheckFilter)
            {
                if (KeyData == (Keys.Control | Keys.F))
                {
                    if (ShowFilterClicked != null)
                    {
                        ShowFilterClicked(this, e);
                    }
                    CheckFilter = false;
                }
            }
            else
            {
                CheckFilter = true;
            }
            if (KeyData == (Keys.Enter))
            {
                if (EnterClicked != null)
                {
                    EnterClicked(this, e);
                }
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        protected void PrintGridViewDetails(DevExpress.XtraGrid.GridControl GridView, string Title, PrintType printType, DevExpress.XtraGrid.Views.Grid.GridView gvControl, bool isLandscape = false)
        {
            if (printType == PrintType.DT)
            {
                DataTable dtGridView = GridView.DataSource as DataTable;
                if (GridView.DataSource != null && dtGridView != null && dtGridView.Rows.Count != 0)
                {
                    PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                    link.Component = GridView;
                    PrintPageTitle = Title;
                    link.Landscape = isLandscape;
                    link.CreateMarginalHeaderArea += new CreateAreaEventHandler(link_CreateMarginalHeaderArea);
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
                else
                {
                    //XtraMessageBox.Show("There is no record to take printout.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_PRINT_INFO));
                }
            }
            else
            {
                DataSet dsDataView = GridView.DataSource as DataSet;

                if (GridView.DataSource != null && dsDataView != null && dsDataView.Tables.Count != 0)
                {
                    PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                    link.Component = GridView;
                    PrintPageTitle = Title;
                    link.Landscape = isLandscape;
                    link.CreateMarginalHeaderArea += new CreateAreaEventHandler(link_CreateMarginalHeaderArea);
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
                else
                {
                    //XtraMessageBox.Show("There is no record to take printout.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_PRINT_INFO));
                }
            }
        }
        
        void link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.DateTime, "", Color.DarkBlue,

      new RectangleF(0, 0, 100, 80), BorderSide.None);

            PageInfoBrick brick2 = e.Graph.DrawPageInfo(PageInfo.DateTime, PrintPageTitle, Color.DarkBlue,

      new RectangleF(0, 0, 100, 20), BorderSide.None);

            brick.LineAlignment = BrickAlignment.Center;
            brick2.LineAlignment = BrickAlignment.Center;

            brick.Alignment = BrickAlignment.Center;
            brick2.Alignment = BrickAlignment.Center;

            brick.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            brick.AutoWidth = true;

            brick2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            brick2.AutoWidth = true;
        }

        /// <summary>
        /// To show Dialogue in the Confirmation
        /// </summary>
        protected void ShowSuccessMessage(string Msg)
        {
            Msg = Msg.TrimEnd('.') + " .";
            Iswaitformshown = true;
            ShowsuccessDialog(Msg);
            System.Threading.Thread.Sleep(1000);
            CloseWaitDialog();
        }

        private void ShowsuccessDialog(string WaitFormCaption)
        {
            CloseWaitDialog();
            SplashScreenManager.ShowForm(typeof(frmPayWait));
            SplashScreenManager.Default.SetWaitFormCaption(WaitFormCaption);

        }
        public void ShowwaitDialog(string message)
        {
            CloseWaitDialog();
            Iswaitformshown = true;
            SplashScreenManager.ShowForm(typeof(frmPayWait));
            SplashScreenManager.Default.SetWaitFormDescription(string.IsNullOrEmpty(message) ? "Processing..." : message + "...");
        }
        protected void ShowMessageBox(string Msg)
        {
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        protected string GetMessage(string keyCode)
        {
            Assembly aacpp = Assembly.GetEntryAssembly();
            string patth = aacpp.Location;
            string newpath = Path.Combine(Path.GetDirectoryName(patth), "Bosco.Utility.dll");
            Assembly newassc = Assembly.LoadFrom(newpath);

            ResourceManager resourceManger = new ResourceManager("Bosco.Utility.Resources.Messages.Messages", newassc);
            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
            }
            catch (Exception ex)
            {
                //MessageRender.ShowMessage("Resoure File is not available " + keyCode, false);
                MessageRender.ShowMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_RESOURCE_FILE_NOT_AVAILABLE_INFO) + keyCode, false);
            }
            return msg;
        }
        protected void CloseWaitDialog()
        {
            try
            {
                Iswaitformshown = true;
                if (Iswaitformshown)
                {
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }
        protected void SetBorderColorForGridLookUpEdit(GridLookUpEdit glkpEdit)
        {
            glkpEdit.Properties.Appearance.BorderColor = glkpEdit.Text == string.Empty ? Color.Red : Color.Empty;
        }
        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        protected SettingProperty AppSetting
        {
            get
            {
                if (appsetting == null) { appsetting = new SettingProperty(); }
                return appsetting;
            }
        }

        protected UserProperty LoginUser
        {
            get
            {
                if (loginuser == null) { loginuser = new UserProperty(); }
                return loginuser;
            }
        }

        protected DialogResult ShowConfirmationMessage(string Message, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            DialogResult drResult = DialogResult.None;
            drResult = XtraMessageBox.Show(Message, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), messageBoxButtons, messageBoxIcon);
            return drResult;
        }


        /// <summary>
        /// On 25/03/2021,
        /// 
        /// For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
        /// </summary>
        public DataTable ForSDBINMSkipLedgers(string voucherdate, DataTable dtLedgers)
        {
            if (AppSetting.IS_SDB_INM)
            {
                if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                {
                    //#. For Skipping Auditor mentioned Ledgers ***********************************************************************
                    bool canSkipLedgers = false;
                    //For Few Places, Forcely skip those ledgers like Merge Ledgers
                    if (string.IsNullOrEmpty(voucherdate))
                    {
                        canSkipLedgers = true;
                    }
                    else
                    {
                        DateTime vdate = UtilityMember.DateSet.ToDate(voucherdate, false);
                        //canSkipLedgers = (vdate >= AppSetting.SDBINM_EnforceLedgersYearFrom && 
                        //        UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) >= AppSetting.SDBINM_EnforceLedgersYearFrom);

                        canSkipLedgers = (vdate >= AppSetting.SDBINM_EnforceLedgersYearFrom);
                    }

                    //1. If Voucher Date and Current FY is greater then 01/04/2021, We have to skip those ledgers
                    if (canSkipLedgers && !string.IsNullOrEmpty(AppSetting.SDBINM_SkippedLedgerIds))
                    {
                        string filter = "LEDGER_ID NOT IN (" + AppSetting.SDBINM_SkippedLedgerIds + ")";

                        //If already row filter assigned
                        dtLedgers = dtLedgers.DefaultView.ToTable();
                        dtLedgers.DefaultView.RowFilter = filter;
                        dtLedgers = dtLedgers.DefaultView.ToTable();
                    }
                    //********************************************************************************************************************

                    //#. For Skipping Fixed Asset Ledgers If Asset Module enabled ********************************************************
                    if (SettingProperty.EnableAsset && !string.IsNullOrEmpty(voucherdate))
                    {
                        DateTime vdate = UtilityMember.DateSet.ToDate(voucherdate, false);

                        if (vdate >= AppSetting.SDBINM_EnforceLedgersYearFrom)
                        {
                            dtLedgers.DefaultView.RowFilter = "GROUP_ID NOT IN (" + (Int32)TDSLedgerGroup.FixedAsset + ")";
                            dtLedgers = dtLedgers.DefaultView.ToTable();
                        }
                    }
                    //********************************************************************************************************************

                }

            }

            return dtLedgers;
        }


        /// <summary>
        ///  Added By Chinna
        /// </summary>
        /// <param name="ucToolBar"></param>
        /// <param name="userRights"></param>
        /// <param name="ParentId"></param>
        public void ApplyUserRights(UserControl.UcToolBar ucToolBar, List<Enum> userRights, int ParentId)
        {
            DataTable dtUserRights = new DataTable();
            Forms activityType;
            string Activities = string.Empty;
            try
            {
                if (!this.LoginUser.IsFullRightsReservedUser)
                {
                    dtUserRights = CommonMethod.ApplyUserRightsForForms(ParentId);
                    if (dtUserRights != null && dtUserRights.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dtUserRights.Rows)
                        {
                            for (int i = 0; i < userRights.Count; i++)
                            {
                                if (userRights[i].ToString() == dr["ENUMTYPE"].ToString())
                                {
                                    activityType = (Forms)UtilityMember.EnumSet.GetEnumItemType(typeof(Forms), userRights[i].ToString());
                                    Activities = UtilityMember.EnumSet.GetDescriptionFromEnumValue(activityType);
                                    if (Activities == OperationsList.ADD.ToString())
                                    {
                                        ucToolBar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isAddable = true;
                                    }
                                    else if (Activities == OperationsList.EDIT.ToString())
                                    {
                                        ucToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isEditable = true;
                                    }
                                    else if (Activities == OperationsList.DELETE.ToString())
                                    {
                                        ucToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isDeleteable = true;
                                    }
                                    else if (Activities == OperationsList.MOVE.ToString())
                                    {
                                        //ucToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Always;
                                    }
                                    else if (Activities == OperationsList.PRINT.ToString())
                                    {
                                        ucToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.INSERT.ToString())
                                    {
                                        //ucToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Always;
                                        // isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.NAGATIVEBALANCE.ToString())
                                    {
                                        //ucToolBar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Always;
                                        //  isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.IMPORT.ToString())
                                    {
                                        //ucToolBar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Always;
                                        // isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.CONVERT.ToString())
                                    {
                                        //  ucToolBar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Always;
                                        // isPrintable = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.enumUserRights.Count == 5) //For Journal View Screen
                    {
                        ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = ucToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    if (this.enumUserRights.Count == 4) //For Journal View Screen
                    {
                        ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = ucToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else //For Receipts/Payments/Contra
                    {
                        // ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = ucToolBar.VisiblePrintButton =
                        //  ucToolBar.VisibleMoveTrans = ucToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Always;
                        //ucToolBar.VisibleDownloadExcel   -- Removed by chinna
                    }
                    isEditable = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// On 19/10/2023, To attach copy operation in the grid
        /// </summary>
        /// <param name="grid"></param>
        public void AttachGridContextMenu(GridControl grid)
        {
            ContextMenuStrip context = new System.Windows.Forms.ContextMenuStrip();
            ToolStripItem toolmnu = context.Items.Add("Copy Value", global::PAYROLL.Properties.Resources.copy);

            toolmnu.Tag = 0;
            toolmnu.Click += new EventHandler(toolmnu_Click);

            toolmnu = context.Items.Add("Copy Row", global::PAYROLL.Properties.Resources.copy2);
            toolmnu.Tag = 1;
            toolmnu.Click += new EventHandler(toolmnu_Click);

            toolmnu = context.Items.Add("Copy all Data", global::PAYROLL.Properties.Resources.copy1);
            toolmnu.Tag = 2;
            toolmnu.Click += new EventHandler(toolmnu_Click);
            context.Items.Add("-");

            toolmnu = context.Items.Add("Export Excel", global::PAYROLL.Properties.Resources.save1);
            toolmnu.Tag = 3;
            toolmnu.Click += new EventHandler(toolmnu_Click);

            grid.ContextMenuStrip = context;
        }

        /// <summary>
        /// On 19/10/2023, To attach copy operation events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolmnu_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ToolStripMenuItem mnutoolstrip = sender as ToolStripMenuItem;
                    Int32 options = utilityMember.NumberSet.ToInteger(mnutoolstrip.Tag.ToString());
                    ContextMenuStrip contxmnu = mnutoolstrip.GetCurrentParent() as ContextMenuStrip;

                    if (contxmnu.SourceControl != null)
                    {
                        GridControl grd = contxmnu.SourceControl as GridControl;
                        if (grd.FocusedView != null)
                        {
                            GridView gv = grd.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView;

                            switch (options)
                            {
                                case 0: //Copy focsued cell value
                                    {
                                        Clipboard.Clear();
                                        if (gv.FocusedColumn != null)
                                        {
                                            GridColumn gc = gv.FocusedColumn;
                                            string txt = "";
                                            if (gv.GetFocusedRowCellValue(gc) != null && !string.IsNullOrEmpty(gv.GetFocusedRowCellValue(gc).ToString()))
                                            {
                                                txt = gv.GetFocusedRowCellValue(gc).ToString();
                                                Clipboard.SetText(txt);
                                            }
                                        }
                                        break;
                                    }
                                case 1: //Copy focused row data
                                    {
                                        if (gv.GetFocusedRow() != null)
                                        {
                                            gv.CopyToClipboard();
                                        }
                                        break;
                                    }
                                case 2: //Copy focused full data
                                    {
                                        Int32 focusedrow = gv.FocusedRowHandle;
                                        gv.OptionsSelection.MultiSelect = true;
                                        gv.SelectAll();
                                        gv.CopyToClipboard();
                                        gv.OptionsSelection.MultiSelect = false;
                                        gv.FocusedRowHandle = focusedrow;
                                        break;
                                    }
                                case 3: // Export to xL
                                    {
                                        SaveFileDialog mySaving = new SaveFileDialog();
                                        mySaving.Title = "Export Excel File To";
                                        string Filename = "Report_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                                        Filename = Filename.Replace("-", "");
                                        mySaving.FileName = Filename;
                                        //or .xlsx *.xlsx
                                        mySaving.Filter = "Excel 97-2003 WorkBook|*.xls|Excel WorkBook|*.xlsx";
                                        if (mySaving.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                        {
                                            string file = mySaving.FileName;
                                            string Extension = Path.GetExtension(file);

                                            if (Extension.Equals(".xls"))
                                            {
                                                gv.ExportToXls(file);
                                            }
                                            else
                                            {
                                                grd.Text = "List of Vouchers";
                                                DevExpress.XtraPrinting.XlsxExportOptions advOptions = new DevExpress.XtraPrinting.XlsxExportOptions();
                                                advOptions.SheetName = "Exported from Data Grid";
                                                gv.ExportToXlsx(file);
                                            }
                                        }

                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
            }
        }
        #endregion
    }
}