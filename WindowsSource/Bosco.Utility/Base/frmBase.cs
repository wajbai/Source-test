using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Resources;
using System.Reflection;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;
using System.Windows.Forms;
using Bosco.Utility;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using Bosco.Utility.ConfigSetting;
using System.Data;
using DevExpress.XtraEditors.Repository;
using System.Text.RegularExpressions;
using System.Globalization;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Utility.Controls;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraGrid;
using DevExpress.XtraSpellChecker.Native;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Menu;
using System.IO;


namespace Bosco.Utility.Base
{
    public class frmBase : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration
        private Form appMain = null;
        CommonMember utilityMember = null;
        public event EventHandler ShowFilterClicked;
        public event EventHandler EnterClicked;
        bool CheckFilter = true;
        public bool isEditable = false;
        public bool isPrintable = false;
        public bool isAddable = false;
        public bool isDeleteable = false;
        public List<Enum> enumUserRights = new List<Enum>();
        public List<int> userRights = new List<int>();
        private DevExpress.XtraSpellChecker.SpellChecker spellchecker;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        public Int32 ADMIN_USER_DEFAULT_ID = 1; //it will be assinged 1 after login
        #endregion

        protected frmBase()
        {
            InitializeComponent();
        }

        protected virtual void OnUpdateHeld(object sender, EventArgs e)
        {

        }

        protected virtual void OnEditHeld(object sender, EventArgs e)
        {
        }

        protected string GetMessage(string keyCode)
        {
            //ResourceManager resourceManger = new ResourceManager("Bosco.Utility.Resources.Messages.Messages", Assembly.GetExecutingAssembly());

            //string msg = "";
            //try
            //{
            //    msg = resourceManger.GetString(keyCode);
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage("Resoure File is not available " + keyCode, false);
            //}

            string msg = "";
            msg = MessageRender.GetMessage(keyCode);
            //if (string.IsNullOrEmpty(msg))
            //{
            //MessageRender.ShowMessage("Resoure File is not available " + keyCode, false);
            //}

            return msg;
        }

        protected string GetReportMessage(string keyCode)
        {
            //ResourceManager resourceManger = new ResourceManager("Bosco.Utility.Resources.Messages.Messages", Assembly.GetExecutingAssembly());

            //string msg = "";
            //try
            //{
            //    msg = resourceManger.GetString(keyCode);
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage("Resoure File is not available " + keyCode, false);
            //}

            string msg = "";
            msg = MessageRender.GetReportMessage(keyCode);
            //if (string.IsNullOrEmpty(msg))
            //{
            //MessageRender.ShowMessage("Resoure File is not available " + keyCode, false);
            //}

            return msg;
        }

        protected void ShowMessageBox(string Msg)
        {
            CloseWaitDialog();
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void ShowMessageBoxError(string Msg)
        {
            CloseWaitDialog();
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowMessageBoxWarning(string Msg)
        {
            CloseWaitDialog();
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region CommonInterface Properties
        //This variable is used to return any value (may be recent added id or item or anything ) from add form
        public object ReturnValue;
        public DialogResult ReturnDialog = DialogResult.Cancel;
        #endregion

        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForLookUpEdit(LookUpEdit lkpEdit)
        {
            lkpEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(lkpEdit.EditValue.ToString()) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForComboBoxEdit(ComboBoxEdit cboEdit)
        {
            cboEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(cboEdit.EditValue.ToString()) ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForCheckListBox(CheckedListBoxControl clstEdit)
        {
            clstEdit.Appearance.BorderColor = clstEdit.Items.Count == 0 ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForGridLookUpEdit(GridLookUpEdit glkpEdit)
        {
            glkpEdit.Properties.Appearance.BorderColor = glkpEdit.Text == string.Empty ? Color.Red : Color.Empty;
        }

        protected void SetBorderColorForDateTimeEdit(DateEdit dteEdit)
        {
            dteEdit.Properties.Appearance.BorderColor = dteEdit.Text == string.Empty && dteEdit.DateTime == DateTime.MinValue ? Color.Red : Color.Empty;
        }

        protected bool IsValidEmail(string EmailAddress)
        {
            bool IsValid = true;
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                var r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

                IsValid = r.IsMatch(EmailAddress);
            }
            return IsValid;
        }

        protected bool IsValidURL(string url)
        {
            bool IsValid = true;
            if (!string.IsNullOrEmpty(url))
            {
                var r = new Regex(@"http(s)://((([0-9a-zA-Z]([0-9a-zA-Z\-]*[0-9a-zA-Z])?\.)*[a-zA-Z]([0-9a-zA-Z\-]*[0-9a-zA-Z])?)|([0-2]?\d?\d\.[0-2]?\d?\d\.[0-2]?\d?\d\.[0-2]?\d?\d))?/[/a-zA-Z0-9$\-_.+!*'(),?:@&=]*");

                IsValid = r.IsMatch(url);
            }
            return IsValid;
        }

        protected Bosco.Utility.ConfigSetting.UserProperty LoginUser
        {
            get { return new Bosco.Utility.ConfigSetting.UserProperty(); }
        }

        protected Bosco.Utility.ConfigSetting.SettingProperty AppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.SettingProperty(); }
        }

        protected Bosco.Utility.ConfigSetting.UISettingProperty UIAppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.UISettingProperty(); }
        }

        protected Bosco.Utility.ConfigSetting.TransProperty Transaction
        {
            get { return new Bosco.Utility.ConfigSetting.TransProperty(); }
        }

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //08/10/2018 - To Enable Spell check for all the input text boxes in the application -----------------------------------------------
            //string dictionaryPath = Path.Combine(Path.Combine(NPM.Misc.IO.Utils.GetNPMRootPath(), "Main Application"), "Dictionaries");  

            //spellchecker.Culture = new System.Globalization.CultureInfo(Application.CurrentCulture.Name);
            //spellchecker.ParentContainer = this;
            //spellchecker.CheckContainer(this);
            //spellchecker.SpellCheckMode = DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType;
            //spellchecker.CheckAsYouTypeOptions.CheckControlsInParentContainer = true;
            //spellchecker.CheckAsYouTypeOptions.Color = Color.Red;
            //spellchecker.CheckAsYouTypeOptions.ShowSpellCheckForm = true;
            //spellchecker.CheckAsYouTypeOptions.SuggestionCount = 5;
            //spellchecker.CheckAsYouTypeOptions.UnderlineStyle = DevExpress.XtraSpellChecker.UnderlineStyle.WavyLine;
            //spellchecker.SpellingFormType = DevExpress.XtraSpellChecker.SpellingFormType.Word;

            //spellchecker.OptionsSpelling.IgnoreEmails = spellchecker.OptionsSpelling.IgnoreUpperCaseWords = DefaultBoolean.True;
            //spellchecker.OptionsSpelling.IgnoreUrls = spellchecker.OptionsSpelling.IgnoreWordsWithNumbers = DefaultBoolean.True;


            //-----------------------------------------------------------------------------------------------------------------------------------
        }

        protected virtual void SetLanguage()
        {

        }

        protected void ShowSuccessMessage(string Msg)
        {
            try
            {
                CloseWaitDialog();
                if (SplashScreenManager.Default == null)
                {
                    SplashScreenManager.ShowForm(typeof(frmSuccessWait));
                    SplashScreenManager.Default.SetWaitFormCaption(Msg.TrimEnd('.') + " .");
                    System.Threading.Thread.Sleep(1000);
                    CloseWaitDialog();
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
                CloseWaitDialog();
            }
        }

        public void ShowWaitDialog(string Message = "")
        {
            try
            {
                CloseWaitDialog();
                if (SplashScreenManager.Default == null)
                {
                    SplashScreenManager.ShowForm(this, typeof(frmWait), true, true, true);
                    SplashScreenManager.Default.SetWaitFormDescription(string.IsNullOrEmpty(Message) ? "Processing..." : Message + "...");
                }
            }
            catch (Exception ex)
            {
                AcMELog.WriteLog(ex.Message);
                CloseWaitDialog();
            }
        }

        protected void CloseWaitDialog()
        {
            try
            {
                if (SplashScreenManager.Default != null)
                {
                    if (SplashScreenManager.Default.IsSplashFormVisible)
                    {
                        SplashScreenManager.CloseForm(false);
                    }
                }
            }
            catch (Exception)
            {
                // Have to find out
            }
        }

        protected DialogResult ShowConfirmationMessage(string Message, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            CloseWaitDialog();
            DialogResult drResult = DialogResult.None;
            drResult = XtraMessageBox.Show(Message, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), messageBoxButtons, messageBoxIcon);
            return drResult;
        }

        protected virtual void ShowSuccessMessageOnToolTip(string Message)
        {
            ToolTipController toolTipControllerMessage = new ToolTipController();
            ToolTipControllerShowEventArgs args = toolTipControllerMessage.CreateShowArgs();
            args.ToolTip = Message;
            args.ToolTipType = ToolTipType.SuperTip;
            args.ToolTipLocation = ToolTipLocation.TopCenter;
            toolTipControllerMessage.ShowHint(args, this);
            toolTipControllerMessage.AutoPopDelay = 5000;
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        /// <summary>
        /// Method to predict the next code
        /// </summary>
        /// <param name="parCode"></param>
        /// <returns></returns>
        public static string CodePredictor(string parCode, DataTable dtCode)
        {
            parCode = parCode != string.Empty ? parCode : "000";
            string finalCode = "";
            string tempstr = "";
            string Code = string.Empty;
            try
            {
                string prefixCode = Regex.Match(parCode, @"^[A-Z|a-z]+").Value;
                string digitCode = Regex.Match(parCode, @"\d+").Value;
                string suffixCode = Regex.Match(parCode, @"[A-Z|a-z]+$").Value;
                int tempCode = Convert.ToInt32(digitCode) + 1;
                if (prefixCode.Length != parCode.Length)
                {
                    if (digitCode.Length != parCode.Length)
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                    else
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                }
                // To check the generated code is present already or not
                for (int i = 0; i < dtCode.Rows.Count && dtCode != null && dtCode.Rows.Count > 0; i++)
                {
                    if (finalCode.Equals(dtCode.Rows[i][0].ToString())) { finalCode = CodePredictor(finalCode, dtCode); i = 0; }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }

            return finalCode;
        }

        /// <summary>
        /// To add zero to string 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string AddZero(int length)
        {
            string tempstr = "";
            for (int i = 1; i <= length; i++)
                tempstr = tempstr + "0";
            return tempstr;
        }

        /// <summary>
        /// To count zeros present in the digitCode
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        private static int ZeroCount(string digit)
        {
            Regex reg = new Regex(@"0+");
            Match mat = reg.Match(digit);
            string tempstr = mat.Value;
            return tempstr.Length;
        }

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
            if (KeyData == Keys.Enter)
            {
                if (EnterClicked != null)
                {
                    EnterClicked(this, e);
                }
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        public void LockMasters(ucToolBar ucToolBar)
        {
            if (this.AppSetting.LockMasters == (int)YesNo.Yes)
            {
                ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        public bool CheckForInternetConnection()
        {
            AcMELog.WriteLog("Checking for the Internet Connection..");
            bool hasInternet = false;
            ResultArgs result = new ResultArgs();
            using (AcMEERPFTP ftpUpload = new AcMEERPFTP())
            {
                result = ftpUpload.CheckIfFileExistsbyFTP();
                if (result.Success)
                {
                    hasInternet = true;
                    AcMELog.WriteLog("Connection established successfully.");
                }
                else
                {
                    AcMELog.WriteLog("Problem in connecting to Internet." + result.Message);
                }
            }
            return hasInternet;
        }

        public bool CheckForInternetConnectionhttp()
        {
            AcMELog.WriteLog("Checking for the Internet Connection..");
            bool hasInternet = false;
            ResultArgs result = new ResultArgs();
            using (AcMEERPFTP http = new AcMEERPFTP())
            {
                result = http.CheckIfFileExistsbyHTTP();
                if (result.Success)
                {
                    hasInternet = true;
                    AcMELog.WriteLog("Connection established successfully.");
                }
                else
                {
                    AcMELog.WriteLog("Problem in connecting to Internet." + result.Message);
                }
            }
            return hasInternet;
        }

        /// <summary>
        /// by Alex for Currencr formate
        /// </summary>
        public string SetCurrencyFormat(string Caption)
        {
            SettingProperty settingProperty = new SettingProperty();
            string CurrencyFormat = string.Empty;
            if (!Caption.Contains(settingProperty.Currency))
                CurrencyFormat = String.Format("{0} ({1})", Caption, settingProperty.Currency);
            else { CurrencyFormat = Caption; }
            return CurrencyFormat;
        }

        public void ApplyUserRights(ucToolBar ucToolBar, List<Enum> userRights, int ParentId)
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
                                        ucToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Always;
                                    }
                                    else if (Activities == OperationsList.PRINT.ToString())
                                    {
                                        ucToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.INSERT.ToString())
                                    {
                                        ucToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.NAGATIVEBALANCE.ToString())
                                    {
                                        ucToolBar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.IMPORT.ToString())
                                    {
                                        ucToolBar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
                                    }
                                    else if (Activities == OperationsList.CONVERT.ToString())
                                    {
                                        ucToolBar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Always;
                                        isPrintable = true;
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
                    else //For Receipts/Payments/Contra
                    {
                        ucToolBar.VisibleAddButton = ucToolBar.VisibleEditButton = ucToolBar.VisibleDeleteButton = ucToolBar.VisiblePrintButton =
                        ucToolBar.VisibleMoveTrans = ucToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Always;
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
        /// Get Local system IPv4 IP Address
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            try
            {
                foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                output = ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Problem in getting local ip address " + err.Message);
                output = "";
            }
            return output;
        }

        /// <summary>
        /// This method is used to get mac address
        /// </summary>
        /// <returns></returns>
        public string GetIPAddress()
        {
            String IPAddress = string.Empty;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();

                    //Get IP Address
                    foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            IPAddress += ip.Address.ToString();
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(IPAddress))
                    {
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in getting IP address details " + err.Message);
            }

            IPAddress = IPAddress.TrimEnd(',');

            return IPAddress;
        }

        /// <summary>
        /// This method is used to get mac address
        /// </summary>
        /// <returns></returns>
        public string GetMACAddress(bool allMACAddress = false)
        {
            String MacAddress = string.Empty;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (MacAddress == String.Empty || allMACAddress)// only return MAC Address from first card
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();

                        //1. Get Mac Address
                        if (adapter.GetPhysicalAddress() != null)
                        {
                            if (allMACAddress)
                            {
                                if (!String.IsNullOrEmpty(adapter.GetPhysicalAddress().ToString()))
                                {
                                    MacAddress += adapter.GetPhysicalAddress().ToString() + Delimiter.ECap;
                                }
                            }
                            else
                            {
                                MacAddress = adapter.GetPhysicalAddress().ToString() + ",";
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Error in getting MAC address details :: updating branch logged details " + err.Message);
            }

            MacAddress = MacAddress.TrimEnd(',');
            MacAddress = MacAddress.TrimEnd('ê');
            return MacAddress;
        }

        protected void ShowCustomFilter(GridControl grid)
        {
            frmFilter frmfilter = new frmFilter(grid);
            frmfilter.ShowDialog();

        }

        /// <summary>
        /// On 19/10/2023, To attach copy operation in the grid
        /// </summary>
        /// <param name="grid"></param>
        public void AttachGridContextMenu(GridControl grid)
        {
            ContextMenuStrip context = new System.Windows.Forms.ContextMenuStrip();
            ToolStripItem toolmnu = context.Items.Add("Copy Value", global::Bosco.Utility.Properties.Resources.copy);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 0;
            toolmnu.ToolTipText = "Copy Selected/Active cell value";
            toolmnu.Click += new EventHandler(toolmnu_Click);

            toolmnu = context.Items.Add("Copy Row", global::Bosco.Utility.Properties.Resources.copy2);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 1;
            toolmnu.ToolTipText = "Copy Selected/Active row values";
            toolmnu.Click += new EventHandler(toolmnu_Click);

            toolmnu = context.Items.Add("Copy all Data", global::Bosco.Utility.Properties.Resources.copy1);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 2;
            toolmnu.ToolTipText = "Copy entire grid data";
            toolmnu.Click += new EventHandler(toolmnu_Click);
            context.Items.Add("-");

            toolmnu = context.Items.Add("Export Excel", global::Bosco.Utility.Properties.Resources.save);
            toolmnu.ImageAlign = ContentAlignment.MiddleCenter;
            toolmnu.Tag = 3;
            toolmnu.ToolTipText = "Export to Excel";
            toolmnu.Click += new EventHandler(toolmnu_Click);
            grid.ContextMenuStrip = context;

            /*if (grid.FocusedView != null)
            {
                GridView gv = grid.FocusedView as GridView;
                if (gv.RowCount > 0)
                {
                    grid.ContextMenuStrip = context;
                }
            }*/
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
                            GridView gv = grd.FocusedView as GridView;

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
                this.ShowMessageBoxError(err.Message);
            }
        }

        private void InitializeComponent()
        {
            this.spellchecker = new DevExpress.XtraSpellChecker.SpellChecker();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // spellchecker
            // 
            this.spellchecker.Culture = new System.Globalization.CultureInfo("en-US");
            this.spellchecker.OptionsSpelling.CheckFromCursorPos = DevExpress.Utils.DefaultBoolean.True;
            this.spellchecker.OptionsSpelling.CheckSelectedTextFirst = DevExpress.Utils.DefaultBoolean.True;
            this.spellchecker.OptionsSpelling.IgnoreEmails = DevExpress.Utils.DefaultBoolean.True;
            this.spellchecker.OptionsSpelling.IgnoreMixedCaseWords = DevExpress.Utils.DefaultBoolean.True;
            this.spellchecker.OptionsSpelling.IgnoreUpperCaseWords = DevExpress.Utils.DefaultBoolean.True;
            this.spellchecker.OptionsSpelling.IgnoreUrls = DevExpress.Utils.DefaultBoolean.True;
            this.spellchecker.OptionsSpelling.IgnoreWordsWithNumbers = DevExpress.Utils.DefaultBoolean.True;
            this.spellchecker.ParentContainer = null;
            this.spellchecker.SpellCheckMode = DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType;
            this.spellchecker.SpellingFormType = DevExpress.XtraSpellChecker.SpellingFormType.Word;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // frmBase
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "frmBase";
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        public DialogResult InputBox(string title, string promptText, ref string value, bool enablePasswordChar = false)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            if (enablePasswordChar)
            {
                textBox.PasswordChar = '*';
            }

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }


    }
}
