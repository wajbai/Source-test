using System;
using System.ComponentModel;
using System.Windows.Forms;
using Bosco.Utility.Common;
using Bosco.Utility.Validations;
using System.Drawing.Printing;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

using Payroll.Model;
using Bosco.Utility.Common;
using Payroll.Model.UIModel;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Report.Base;
using PAYROLL.Modules;
using System.Linq;
using Bosco.Utility.ConfigSetting;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using DevExpress.Utils.Win;
using DevExpress.XtraLayout.Utils;
using DevExpress.Utils.Menu;
using System.Reflection;
using System.Collections.Generic;

namespace Bosco.Report.View
{
    public partial class frmPaySlipViewer : Bosco.Utility.Base.frmBase
    {
        #region Declaration
        CommonMember UtilityMember = new CommonMember();
        clsPayrollBase paybase = new clsPayrollBase();

        //string SelectedGroupId = string.Empty;
        private AppSchemaSet.ApplicationSchemaSet appSchema = new AppSchemaSet.ApplicationSchemaSet();
        private Payroll.DAO.Schema.ApplicationSchema.PRCOMPMONTHDataTable dtcomp = new Payroll.DAO.Schema.ApplicationSchema.PRCOMPMONTHDataTable();
        string selectedGroupName = string.Empty;
        string selectedDepartmentName = string.Empty;

        #endregion

        #region Constructors
        public frmPaySlipViewer()
        {
            InitializeComponent();
        }
        #endregion

        private string SelectedPayrollGroupIds
        {
            get
            {
                string paygroupids = string.Empty;
                selectedGroupName = string.Empty;
                chkLstPayGroup.RefreshEditValue();
                List<object> selecteditems = chkLstPayGroup.Properties.Items.GetCheckedValues();

                foreach (object item in selecteditems)
                {
                    if (chkLstPayGroup.Properties.Items[item] != null)
                    {
                        paygroupids += item.ToString() + ",";
                        selectedGroupName += chkLstPayGroup.Properties.Items[item].Description + ",";
                    }
                }
                paygroupids = paygroupids.TrimEnd(',');
                selectedGroupName = selectedGroupName.TrimEnd(',');
                return paygroupids;
            }
            set
            {
                chkLstPayGroup.SetEditValue(value);
            }
        }

        private string SelectedPayrollDepartmentIds
        {
            get
            {
                string paydepartmentids = string.Empty;
                selectedDepartmentName = string.Empty;
                if (lcPayrollDepartment.Visibility == LayoutVisibility.Always)
                {
                    chkLstPayDepartment.RefreshEditValue();
                    List<object> selecteditems = chkLstPayDepartment.Properties.Items.GetCheckedValues();

                    foreach (object item in selecteditems)
                    {
                        if (chkLstPayDepartment.Properties.Items[item] != null)
                        {
                            paydepartmentids += item.ToString() + ",";
                            selectedDepartmentName += chkLstPayDepartment.Properties.Items[item].Description + ",";
                        }
                    }
                    paydepartmentids = paydepartmentids.TrimEnd(',');
                    selectedDepartmentName = selectedDepartmentName.TrimEnd(',');
                }
                return paydepartmentids;
            }
            set
            {
                chkLstPayDepartment.SetEditValue(value);
            }
        }

        private string SelectedPayrollComponendsIds
        {
            get
            {

                string SelectedPayrollComponends = string.Empty;
                int[] SelectedRows = gvComponent.GetSelectedRows();
                if (SelectedRows.Count() > 0)
                {
                    //Getting all the checked paycomponend Ids 
                    DataRow drSelecRow;
                    string SSIDId = "";
                    selectedGroupName = string.Empty;
                    foreach (int item in SelectedRows)
                    {
                        drSelecRow = gvComponent.GetDataRow(item);
                        if (drSelecRow != null)
                        {
                            SSIDId += drSelecRow[dtcomp.COMPONENTIDColumn.ColumnName].ToString() + ",";
                        }
                    }
                    SelectedPayrollComponends = SSIDId.TrimEnd(',');
                }
                return SelectedPayrollComponends;
            }
        }

        private string SelectedPayrollComponendsIds1
        {
            get
            {

                string SelectedPayrollComponends = string.Empty;
                int[] SelectedRows = gvComponent1.GetSelectedRows();
                if (SelectedRows.Count() > 0)
                {
                    //Getting all the checked paycomponend Ids 
                    DataRow drSelecRow;
                    string SSIDId = "";
                    selectedGroupName = string.Empty;
                    foreach (int item in SelectedRows)
                    {
                        drSelecRow = gvComponent1.GetDataRow(item);
                        if (drSelecRow != null)
                        {
                            SSIDId += drSelecRow[dtcomp.COMPONENTIDColumn.ColumnName].ToString() + ",";
                        }
                    }
                    SelectedPayrollComponends = SSIDId.TrimEnd(',');
                }
                return SelectedPayrollComponends;
            }
        }

        private string SelectedPayrollComponendsIds2
        {
            get
            {

                string SelectedPayrollComponends = string.Empty;
                int[] SelectedRows = gvComponent2.GetSelectedRows();
                if (SelectedRows.Count() > 0)
                {
                    //Getting all the checked paycomponend Ids 
                    DataRow drSelecRow;
                    string SSIDId = "";
                    selectedGroupName = string.Empty;
                    foreach (int item in SelectedRows)
                    {
                        drSelecRow = gvComponent2.GetDataRow(item);
                        if (drSelecRow != null)
                        {
                            SSIDId += drSelecRow[dtcomp.COMPONENTIDColumn.ColumnName].ToString() + ",";
                        }
                    }
                    SelectedPayrollComponends = SSIDId.TrimEnd(',');
                }
                return SelectedPayrollComponends;
            }
        }

        private string SelectedPayrollComponendsName
        {
            get
            {

                string SelectedPayrollComponends = string.Empty;
                int[] SelectedRows = gvComponent.GetSelectedRows();
                if (SelectedRows.Count() > 0)
                {
                    //Getting all the checked paycomponend Ids 
                    DataRow drSelecRow;
                    string SSIDId = "";
                    selectedGroupName = string.Empty;
                    foreach (int item in SelectedRows)
                    {
                        drSelecRow = gvComponent.GetDataRow(item);
                        if (drSelecRow != null)
                        {
                            SSIDId += drSelecRow["COMPONENT"].ToString() + ",";
                        }
                    }
                    SelectedPayrollComponends = SSIDId.TrimEnd(',');
                }
                return SelectedPayrollComponends;
            }
        }

        private string SelectedPayrollComponendsName1
        {
            get
            {

                string SelectedPayrollComponends = string.Empty;
                int[] SelectedRows = gvComponent1.GetSelectedRows();
                if (SelectedRows.Count() > 0)
                {
                    //Getting all the checked paycomponend Ids 
                    DataRow drSelecRow;
                    string SSIDId = "";
                    selectedGroupName = string.Empty;
                    foreach (int item in SelectedRows)
                    {
                        drSelecRow = gvComponent1.GetDataRow(item);
                        if (drSelecRow != null)
                        {
                            SSIDId += drSelecRow["COMPONENT"].ToString() + ",";
                        }
                    }
                    SelectedPayrollComponends = SSIDId.TrimEnd(',');
                }
                return SelectedPayrollComponends;
            }
        }

        private string SelectedPayrollComponendsName2
        {
            get
            {

                string SelectedPayrollComponends = string.Empty;
                int[] SelectedRows = gvComponent2.GetSelectedRows();
                if (SelectedRows.Count() > 0)
                {
                    //Getting all the checked paycomponend Ids 
                    DataRow drSelecRow;
                    string SSIDId = "";
                    selectedGroupName = string.Empty;
                    foreach (int item in SelectedRows)
                    {
                        drSelecRow = gvComponent2.GetDataRow(item);
                        if (drSelecRow != null)
                        {
                            SSIDId += drSelecRow["COMPONENT"].ToString() + ",";
                        }
                    }
                    SelectedPayrollComponends = SSIDId.TrimEnd(',');
                }
                return SelectedPayrollComponends;
            }
        }

        #region Events

        private void frmPaySlipViewer_Load(object sender, EventArgs e)
        {
            LoadTitleTypes();

            EnableOptions();
            LoadProjectsAutoCompleteTitle();
            LoadGroupList();
            
            //On 27/02/2021, to lock setting sign Image option
            lcSign1Btn.Visibility = LayoutVisibility.Never;
            lcSign2Btn.Visibility = LayoutVisibility.Never;
            lcSign3Btn.Visibility = LayoutVisibility.Never;
            //On 03/03/2021, to lock sing details in Report page
            txtRoleName1.Properties.ReadOnly = txtRoleName2.Properties.ReadOnly = txtRoleName3.Properties.ReadOnly = true;

            txtRole1.Properties.ReadOnly = txtRole2.Properties.ReadOnly = txtRole3.Properties.ReadOnly = true;

            if (this.AppSetting.BranchOfficeCode == "bsofttsccinb" || this.AppSetting.BranchOfficeCode == "bsofttmmhs01")
            {
                lcChkShowSignDetails.Visibility = lcGrpSign1.Visibility = lcGrpSign2.Visibility = lcGrpSign3.Visibility = LayoutVisibility.Never;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (ApplyReportCriteria())
            {
                ReportProperty.Current.ReportFlag = 0;
                ReportProperty.Current.PayrollTitleType = glkTitleType.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(glkTitleType.EditValue.ToString());
                ReportProperty.Current.PayrollProjectTitle = txtPayslipProjectTitle.Text.Trim();
                ReportProperty.Current.PayrollProjectAddress = txtProjectAddress.Text.Trim();
                ReportProperty.Current.PayrollSignOfEmployer = txtAuthSign1.Text.Trim();
                ReportProperty.Current.PayrollAuthorisedSignatory2= txtAuthSign2.Text.Trim();

                FillSignDetails();

                if (ReportProperty.Current.ReportId == "RPT-170")
                {
                    if (glkpPaymentMode.EditValue != null)
                    {
                        DevExpress.XtraGrid.Views.Grid.GridView view = glkpPaymentMode.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                        DataRowView drvPaymentMode = glkpPaymentMode.GetSelectedDataRow() as DataRowView;
                        if (drvPaymentMode != null)
                        {
                            string PaymentMode = string.IsNullOrEmpty(drvPaymentMode["PAYMENT_MODE"].ToString()) ?
                                                        string.Empty : drvPaymentMode["PAYMENT_MODE"].ToString();
                            Int32 isbank  = string.IsNullOrEmpty(drvPaymentMode["IS_BANK"].ToString()) ? 0 :  
                                    UtilityMember.NumberSet.ToInteger(drvPaymentMode["IS_BANK"].ToString());

                            ReportProperty.Current.PayrollPaymentMode = PaymentMode;
                            ReportProperty.Current.PayrollPaymentModeId = UtilityMember.NumberSet.ToInteger(glkpPaymentMode.EditValue.ToString());
                            ReportProperty.Current.PayrollPaymentModeByBank = isbank;
                            //ReportProperty.Current.SaveReportSetting();
                        }
                    }

                    if (glkBankAc.EditValue != null)
                    {
                        DevExpress.XtraGrid.Views.Grid.GridView view = glkBankAc.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                        DataRowView drvBankAc = glkBankAc.GetSelectedDataRow() as DataRowView;
                        if (drvBankAc != null)
                        {
                            string bankAddress = (string.IsNullOrEmpty(drvBankAc[this.appSchema.Bank.BANKColumn.ColumnName].ToString()) ?
                                                        string.Empty : drvBankAc[this.appSchema.Bank.BANKColumn.ColumnName]) + System.Environment.NewLine
                                                 + (string.IsNullOrEmpty(drvBankAc[this.appSchema.Bank.BRANCHColumn.ColumnName].ToString()) ?
                                                        string.Empty : drvBankAc[this.appSchema.Bank.BRANCHColumn.ColumnName].ToString()) + System.Environment.NewLine;
                            // commanded by chinna 20.01.2022
                            //+ (string.IsNullOrEmpty(drvBankAc[this.appSchema.Bank.ADDRESSColumn.ColumnName].ToString()) ?
                            //       string.Empty : drvBankAc[this.appSchema.Bank.ADDRESSColumn.ColumnName].ToString());
                            string BankAccNumber = string.IsNullOrEmpty(drvBankAc[this.appSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString()) ?
                                                        string.Empty : drvBankAc[this.appSchema.BankAccount.ACCOUNT_NUMBERColumn.ColumnName].ToString();

                            ReportProperty.Current.PayrollPaymentBankAccountNo = BankAccNumber;
                            ReportProperty.Current.PayrollPaymentBankAddress = bankAddress;
                            ReportProperty.Current.PayrollPaymentBankAccountLedgerId = glkBankAc.EditValue.ToString();
                            //ReportProperty.Current.SaveReportSetting();
                        }
                    }
                }

                ReportProperty.Current.SaveReportSetting();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void GetSignDetails(string payrollprojecttitle)
        {
            Int32 projectid = FetchProjectIdByProjectName(payrollprojecttitle);
            ReportProperty.Current.AssignSignDetails(projectid.ToString());

            //Assign report sign setting
            txtRoleName1.Text = ReportProperty.Current.RoleName1; txtRole1.Text = ReportProperty.Current.Role1;
            txtRoleName2.Text = ReportProperty.Current.RoleName2; txtRole2.Text = ReportProperty.Current.Role2;
            txtRoleName3.Text = ReportProperty.Current.RoleName3; txtRole3.Text = ReportProperty.Current.Role3;

            lcSign1.Text = "Sign 1";
            lcSign2.Text = "Sign 2";
            lcSign3.Text = "Sign 3";

            //For Sign1
            picSign1.Image = null;
            if (ReportProperty.Current.Sign1Image != null)
            {
                picSign1.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign1Image);
            }

            //For Sign2
            picSign2.Image = null;
            if (ReportProperty.Current.Sign2Image != null)
            {
                picSign2.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign2Image);
            }

            //For Sign3
            picSign3.Image = null;
            if (ReportProperty.Current.Sign3Image != null)
            {
                picSign3.Image = ImageProcessing.ByteArrayToImage(ReportProperty.Current.Sign3Image);
            }
        }

        private void FillSignDetails()
        {
            //Assign sign details
            ReportProperty.Current.RoleName1 = txtRoleName1.Text.Trim();
            ReportProperty.Current.Role1 = txtRole1.Text.Trim();
            ReportProperty.Current.RoleName2 = txtRoleName2.Text.Trim();
            ReportProperty.Current.Role2 = txtRole2.Text.Trim();
            ReportProperty.Current.RoleName3 = txtRoleName3.Text.Trim();
            ReportProperty.Current.Role3 = txtRole3.Text.Trim();

            if (lcSign1.Visible)
            {
                //For Sign 1
                ReportProperty.Current.Sign1Image = null;
                if (picSign1.Image != null)
                {
                    picSign1.BorderStyle = BorderStyles.NoBorder;
                    byte[] signImage = ImageProcessing.ImageToByteArray(picSign1.Image as Bitmap);
                    ReportProperty.Current.Sign1Image = signImage;
                }

                //For Sign 2
                ReportProperty.Current.Sign2Image = null;
                if (picSign2.Image != null)
                {
                    picSign2.BorderStyle = BorderStyles.NoBorder;
                    byte[] signImage = ImageProcessing.ImageToByteArray(picSign2.Image as Bitmap);
                    ReportProperty.Current.Sign2Image = signImage;
                }

                //For Sign 3
                ReportProperty.Current.Sign3Image = null;
                if (picSign3.Image != null)
                {
                    picSign3.BorderStyle = BorderStyles.NoBorder;
                    byte[] signImage = ImageProcessing.ImageToByteArray(picSign3.Image as Bitmap);
                    ReportProperty.Current.Sign3Image = signImage;
                }

            }
        }

        private void SetSignImage(Int32 signnumber)
        {
            //string sign = Path.Combine(AcmeerpInstalledPath, "Sign" + signnumber + ".jpg");
            OpenFileDialog openfileSign = new OpenFileDialog();
            //openfileSign.InitialDirectory = AcmeerpInstalledPath;
            openfileSign.Title = "Select Sign Image (Width <= 800 and Height <= 260)";

            //Filter the filedialog, so that it will show only the mentioned format images
            openfileSign.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.ico";

            if (openfileSign.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string signSelected = openfileSign.FileName;
                if (!string.IsNullOrEmpty(signSelected))
                {
                    //Bitmap Selectimage = new Bitmap(signSelected);
                    //Bitmap signimage = new Bitmap(350, 200);
                    //Graphics g = Graphics.FromImage(signimage);
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.FillRectangle(Brushes.White, 0, 0, 350, 200);
                    //g.DrawImage(Selectimage, 0, 0, 350, 200);

                    Bitmap signimage = new Bitmap(signSelected);
                    FileInfo file = new FileInfo(signSelected);
                    double sizeInBytes = file.Length;
                    double filesize = Math.Round(sizeInBytes / 1024);
                    var imageHeight = signimage.Height;
                    var imageWidth = signimage.Width;

                    if (filesize > 50)
                    {
                        MessageRender.ShowMessage("Sign Image file size big, please select a file less than or equal 50 KB");
                    }
                    else if (imageWidth > 800 || imageHeight > 260)
                    {
                        MessageRender.ShowMessage("Sign Image file size must be (Width is <=800 and Height is <=260)");
                    }
                    else
                    {
                        byte[] byteSignImage = ImageProcessing.ImageToByteArray(signimage);
                        if (signnumber == 1)
                            picSign1.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                        else if (signnumber == 2)
                            picSign2.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                        else if (signnumber == 3)
                            picSign3.Image = ImageProcessing.ByteArrayToImage(byteSignImage);
                    }
                }
            }
        }

        public Int32 FetchProjectIdByProjectName(string ProjectName)
        {
            Int32 rnt = 0;
            ResultArgs resultArgs = new ResultArgs();
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectIdByProjectName))
            {
                dataManager.Parameters.Add(appSchema.Project.PROJECTColumn, ProjectName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            if (resultArgs.Success || resultArgs.DataSource.Sclar.ToInteger > 0)
            {
                rnt = resultArgs.DataSource.Sclar.ToInteger;
            }
            return rnt;
        }

        public void LoadPayroll()
        {
            DataTable dtPayroll = paybase.PayrollList;
            if (dtPayroll != null && dtPayroll.Rows.Count > 0)
            {
                UtilityMember.ComboSet.BindGridLookUpCombo(glkPayroll, dtPayroll, "PRNAME", "PAYROLLID");
                if (UtilityMember.NumberSet.ToInteger(ReportProperty.Current.PayrollId) > 0)
                {
                    glkPayroll.EditValue = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.PayrollId);
                }
                else
                {
                    glkPayroll.EditValue = glkPayroll.Properties.GetKeyValue(0);
                }


            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
        }

        private void glkpGroupValue_EditValueChanged(object sender, EventArgs e)
        {
            //FillStaff();
        }

        private void glkPayroll_EditValueChanged(object sender, EventArgs e)
        {
            LoadGroupList();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStaff.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                gvStaff.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvStaff.FocusedColumn = colSName;
                gvStaff.OptionsFind.AllowFindPanel = false;
                gvStaff.ShowEditor();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Control | Keys.F))
            {
                chkShowFilter.Checked = true;
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
        private void gvStaff_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvStaff.RowCount.ToString();
        }

        //private void glkpGroupValue_Popup(object sender, EventArgs e)
        //{
        //    GridLookUpEdit edit = sender as GridLookUpEdit;

        //    string[] payrollGroupIds = ReportProperty.Current.PayrollGroupId.Split(',');
        //    foreach (string project in payrollGroupIds)
        //    {
        //        for (int i = 0; i < gvPayrollGroup.DataRowCount; i++)
        //        {
        //            string getvalue = gvPayrollGroup.GetRowCellValue(i, colGroupId).ToString();
        //            if (getvalue != null && getvalue.Equals(project))
        //            {
        //                int rowHandle = gvPayrollGroup.GetRowHandle(i);

        //                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
        //                {
        //                    edit.Properties.View.SelectRow(rowHandle);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void glkpGroupValue_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        //{
        //    int[] selectedrows = gvPayrollGroup.GetSelectedRows();
        //    e.DisplayText = "(" + selectedrows.Length.ToString() + ") Payroll Group(s) are selected";
        //}

        private void btnShowStaff_Click(object sender, EventArgs e)
        {
            //glkpGroupValue.ClosePopup();
        }

        private void chkIncludeSignDetails_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Methods

        private string GetSelectedStaffId()
        {
            string SelectedStaffId = string.Empty;
            int[] SelRow = gvStaff.GetSelectedRows();
            if (SelRow.Count() > 0)
            {
                //Getting all the checked staff Ids 
                DataRow drSelecRow;
                string SSIDId = "";
                foreach (int item in SelRow)
                {
                    drSelecRow = gvStaff.GetDataRow(item);
                    if (drSelecRow != null)
                    {
                        SSIDId += drSelecRow["STAFFID"].ToString() + ",";
                    }
                }
                SelectedStaffId = SSIDId.TrimEnd(',');
            }
            return SelectedStaffId;
        }

        private void FillStaff(string selectedGrps)
        {
            gcStaff.DataSource = null;
            DataTable dtStaff = new DataTable();
            //Int32 SelectedPayGrp = glkpGroupValue.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpGroupValue.EditValue.ToString()) : 0;
            //string selectedGrps = (string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupId) ? SelectedPayrollGroupIds : ReportProperty.Current.PayrollGroupId);
            if (!String.IsNullOrEmpty(selectedGrps))
            {
                Int32 payrollid = glkPayroll.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkPayroll.EditValue.ToString()) : 0;
                if (payrollid > 0)
                {
                    using (clsPRPSReport payreport = new clsPRPSReport())
                    {
                        dtStaff = payreport.GetStaffByGroup(selectedGrps, payrollid);
                    }
                }
                else
                {
                    using (clsPayrollBase paybase = new clsPayrollBase())
                    {
                        ResultArgs result = paybase.FetchPayrollStaff(selectedGrps, string.Empty, string.Empty);
                        if (result.Success)
                        {
                            dtStaff = result.DataSource.Table;
                            if (dtStaff != null)
                            {
                                dtStaff.Columns["STAFFNAME"].ColumnName = "Name";

                                dtStaff.DefaultView.Sort = "GroupName, STAFFORDER";
                                dtStaff = dtStaff.DefaultView.ToTable();
                            }
                        }
                    }
                }


                if (dtStaff != null && dtStaff.Rows.Count > 0)
                {
                    string[] SelectedStaffid = ReportProperty.Current.PayrollStaffId.Split(',');
                    gcStaff.DataSource = dtStaff;

                    for (int i = 0; i < gvStaff.DataRowCount; i++)
                    {
                        if (gvStaff.GetDataRow(i) != null)
                        {
                            DataRow dr = (DataRow)gvStaff.GetDataRow(i);
                            string staffid = dr["STAFFID"].ToString();
                            int pos = Array.IndexOf(SelectedStaffid, staffid);
                            if (pos > -1)
                            {
                                gvStaff.SelectRow(i);
                            }
                        }
                    }
                }
            }
            // obj.FillStaffListfroDevexpressChklst(SelectedGrp.ToString(), glkPayroll.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkPayroll.EditValue.ToString()) : 0, chklstSelectStaff);
        }


        /// <summary>
        /// This method is used to auto popup projects names for title
        /// </summary>
        private void LoadProjectsAutoCompleteTitle()
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
                {
                    if (!this.LoginUser.IsFullRightsReservedUser)
                    {
                        dataManager.Parameters.Add(this.appSchema.UserRole.USERROLE_IDColumn, AppSetting.RoleId);
                    }
                    dataManager.Parameters.Add(this.appSchema.Project.DATE_CLOSEDColumn, ReportProperty.Current.DateSet.ToDate(AppSetting.YearTo, false));

                    ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                        {
                            DataTable dtProjectDetails = resultArgs.DataSource.Table;
                            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                            foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                            {
                                collection.Add(dr["PROJECT_NAME"].ToString());
                            }

                            txtPayslipProjectTitle.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtPayslipProjectTitle.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            txtPayslipProjectTitle.MaskBox.AutoCompleteCustomSource = collection;
                        }
                    }
                }

                txtPayslipProjectTitle.Text = ReportProperty.Current.PayrollProjectTitle.Trim();
                txtProjectAddress.Text = ReportProperty.Current.PayrollProjectAddress.Trim();
                txtAuthSign1.Text = ReportProperty.Current.PayrollSignOfEmployer.Trim();
                txtAuthSign2.Text = string.IsNullOrEmpty(ReportProperty.Current.PayrollAuthorisedSignatory2) ? string.Empty:   
                                    ReportProperty.Current.PayrollAuthorisedSignatory2.Trim();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadGroupList()
        {
            try
            {
                DataTable dtGradeList;
                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {
                    //On 01/11/2023
                    dtGradeList = Grade.getPayrollGroupByPayrollId(glkPayroll.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkPayroll.EditValue.ToString()) : UtilityMember.NumberSet.ToInteger(clsGeneral.PAYROLL_ID.ToString()));
                    if (dtGradeList != null && dtGradeList.Rows.Count > 0)
                    {
                        chkLstPayGroup.Properties.DataSource = dtGradeList;
                        chkLstPayGroup.Properties.ValueMember = "GROUPID";
                        chkLstPayGroup.Properties.DisplayMember = "GROUPNAME";
                        SelectedPayrollGroupIds = string.Empty;
                        if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollGroupId))
                        {
                            SelectedPayrollGroupIds = ReportProperty.Current.PayrollGroupId;
                        }

                        //dtGradeList.Columns["GROUPID"].ColumnMapping = MappingType.Hidden;
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            //DataTable dtProject = SelectAll.AddHeaderColumn(dtGradeList, "GROUPID", "GROUPNAME");
                            //UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroupValue, dtGradeList, "GROUPNAME", "GROUPID");

                            /*string[] proIds = ReportProperty.Current.PayrollGroupId.Split(','); ;// SelectedPayrollGroupIds.Split(',');
                            foreach (string project in proIds)
                            {
                                for (int i = 0; i < gvPayrollGroup.RowCount ; i++)
                                {
                                    string getvalue = gvPayrollGroup.GetRowCellValue(i,"GROUPID").ToString();
                                    if (getvalue != null && getvalue.Equals(project))
                                    {
                                        int rowHandle = gvPayrollGroup.GetRowHandle(i);

                                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                                        {
                                            glkpGroupValue.Properties.View.SelectRow(rowHandle);
                                        }
                                    }
                                }
                            }*/

                            //if (UtilityMember.NumberSet.ToInteger( ReportProperty.Current.PayrollGroupId) > 0)
                            //    glkpGroupValue.EditValue = UtilityMember.NumberSet.ToInteger(ReportProperty.Current.PayrollGroupId);
                            //else
                            //    glkpGroupValue.EditValue = 0; //glkPayroll.Properties.GetKeyValue(0);

                            //this.BeginInvoke(new MethodInvoker(glkpGroupValue.ShowPopup));
                            //this.BeginInvoke(new MethodInvoker(glkpGroupValue.ClosePopup));

                            if (lcComponents.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                            {
                                FillPayrollComponent(ReportProperty.Current.PayrollGroupId);
                            }

                            if (lcComponents1.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                            {
                                FillPayrollComponent1(ReportProperty.Current.PayrollGroupId);
                            }

                            if (lcComponents2.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                            {
                                FillPayrollComponent2(ReportProperty.Current.PayrollGroupId);
                            }

                            if (lcPayrollDepartment.Visibility == LayoutVisibility.Always)
                            {
                                LoadPayrollDepartment();
                            }

                            if (gcStaff.Visible)
                            {
                                FillStaff(ReportProperty.Current.PayrollGroupId);
                            }
                        }
                    }
                    else
                    {
                        chkLstPayGroup.Properties.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// On 23/08/2015, to Enable Payroll Report Criteria based on reports
        /// </summary>
        private void EnableOptions()
        {
            lcDateFrom.Visibility = lcDateTo.Visibility = lcDateEmpty.Visibility =
               lcPayRoll.Visibility = lcGrpStaffGroup.Visibility = lcAuthSign1.Visibility = lcAuthSign2.Visibility =
               lcBankAc.Visibility = lcPaymentMode.Visibility =  lcComponents.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            lcComponents1.Visibility = lcPayrollDepartment.Visibility = lcComponents2.Visibility = LayoutVisibility.Never;

            lcGrpSign1.Visibility = lcGrpSign2.Visibility = lcGrpSign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcChkShowCompDescription.Visibility = lcEachPayslipSeparatePage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcChkShowSignDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcShowPayGroupConsolidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblReportTitle.Text = ReportProperty.Current.ReportTitle;
            string reportCriteria = ReportProperty.Current.ReportCriteria;
            string[] aReportCriteria = reportCriteria.Split('ÿ');

            for (int i = 0; i < aReportCriteria.Length; i++)
            {
                switch (aReportCriteria[i])
                {
                    case "DF": //Date From 
                        {
                            lcDateFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            DateFrom.DateTime = UtilityMember.DateSet.ToDate(ReportProperty.Current.DateFrom, false);
                            break;
                        }
                    case "DT": //Date To
                        {
                            lcDateTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            DateTo.DateTime = UtilityMember.DateSet.ToDate(ReportProperty.Current.DateTo, false);
                            break;
                        }
                    case "PY": //Show Payroll
                        {
                            LoadPayroll();
                            lcPayRoll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        }
                    case "PS": //Show Staff
                        {
                            lcGrpStaffGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        }
                    case "APC":   //All Payroll component
                    case "EPC":   //Earning Payroll component
                    case "DPC":   //Deduction Payroll component
                    case "ECPC":  //Earning and Calculation Payroll component
                    case "DCPC":  //Deduction and Calculation Payroll component
                    case "ECIPC":  //Earning, Calculation and ESI Payroll component
                        {
                            lcComponents.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        }
                    case "EEC":   //Payroll Employee component
                        {
                            lcComponents1.Text = "Employee Share";
                            lcComponents1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        }
                    case "ERC":   ////Payroll Employer component
                        {
                            lcComponents2.Text = "Employer Share";
                            lcComponents2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        }
                    case "PPD": //Payroll Department
                        {
                            lcPayrollDepartment.Visibility = LayoutVisibility.Always;
                            break;
                        }
                    case "SD": //Show Sign Details
                        {
                            lcChkShowSignDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            lcGrpSign1.Visibility = lcGrpSign2.Visibility = lcGrpSign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            if (ReportProperty.Current.IncludeSignDetails == 1)
                            {
                                chkIncludeSignDetails.Checked = true;
                            }
                            GetSignDetails(ReportProperty.Current.PayrollProjectTitle);

                            break;
                        }
                    case "PCD": //Show Payroll Component Description
                        {
                            lcChkShowCompDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            chkShowComDescription.Checked = (ReportProperty.Current.PayrollShowComponentDescription == 1);
                            lcEachPayslipSeparatePage.Visibility = LayoutVisibility.Always;
                            chkEachPayslipSeparatePage.Checked = (ReportProperty.Current.PayrollPayslipInSeparatePages == 1);

                            break;
                        }
                    case "PGC": //Show Paygroup Consolidation
                        {
                            lcShowPayGroupConsolidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            ChkShowPayGroupConsolidation.Checked = (ReportProperty.Current.PayrollGroupConsolidation== 1);
                            break;
                        }

                }
            }

            if (ReportProperty.Current.ReportId == "RPT-170") //Advice Payment
            {
                lcAuthSign1.Visibility = lcAuthSign2.Visibility = lcBankAc.Visibility = lcPaymentMode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                LoadPaymentModes();
                LoadAllBanksAccounts();
                this.Height = 315;//285;
            }
            else if (ReportProperty.Current.ReportId == "RPT-069" || ReportProperty.Current.ReportId == "RPT-168"
                   || ReportProperty.Current.ReportId == "RPT-071" || ReportProperty.Current.ReportId == "RPT-171") //Pay Register, Yealy Register, Pay wages, epf format2  
            {
                if (ReportProperty.Current.ReportId == "RPT-171") //Staff EPF
                {
                    lcComponents.Text = "PF Salary Comp.";
                    this.Height = 275;
                }
                else
                {
                    this.Height = ((ReportProperty.Current.ReportId == "RPT-069" || ReportProperty.Current.ReportId == "RPT-071") ? 450 : 575);
                }
            }
            else if (ReportProperty.Current.ReportId == "RPT-172") //PT Register
            {
                this.Height = 250;;
            }
            else if (ReportProperty.Current.ReportId == "RPT-070") //PaySlip
            {
                //lcSignOfEmployer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (ReportProperty.Current.ReportId == "RPT-203") //EPF Register Format2 
            {
                this.Height = 325; ;
            }
            else if (ReportProperty.Current.ReportId == "RPT-217") //Posted Finance Vouchers
            {
                lblPayGroup.Visibility = LayoutVisibility.Never;
                this.Height = 230;
            }
            else
            {
                this.Height = 275;
            }

            this.CenterToParent();
        }

        private bool ApplyReportCriteria()
        {
            bool rtn = false;
            string ReportCriteria = ReportProperty.Current.ReportCriteria;
            string[] aReportCriteria = ReportCriteria.Split('ÿ');
            ReportProperty.Current.PayrollDepartmentId = string.Empty;
            ReportProperty.Current.PayrollDepartmentName = string.Empty;

            for (int i = 0; i < aReportCriteria.Length; i++)
            {
                switch (aReportCriteria[i])
                {
                    case "DF":
                    case "DT":
                        if (DateFrom.DateTime > DateTo.DateTime)
                        {
                            DateTime dateTo = DateTo.DateTime;
                            DateTo.DateTime = DateFrom.DateTime;
                            DateFrom.DateTime = dateTo.Date;
                        }

                        ReportProperty.Current.DateFrom = DateFrom.Text;
                        ReportProperty.Current.DateTo = DateTo.Text;
                        rtn = true;
                        break;
                    case "PG":
                        {
                            //if (glkpGroupValue.EditValue == null || UtilityMember.NumberSet.ToInteger(glkpGroupValue.EditValue.ToString()) <= 0)
                            if (string.IsNullOrEmpty(SelectedPayrollGroupIds))
                            {
                                rtn = false;
                                MessageRender.ShowMessage("Select Payroll Group to proceed");
                                chkLstPayGroup.Select();
                                chkLstPayGroup.Focus();
                                //glkpGroupValue.Select();
                                //glkpGroupValue.Focus();
                                break;
                            }
                            else
                            {
                                ReportProperty.Current.PayrollGroupName = selectedGroupName;
                                ReportProperty.Current.PayrollGroupId = SelectedPayrollGroupIds; //glkpGroupValue.EditValue.ToString();
                            }
                            break;
                        }
                    case "PY":
                        {
                            string payrollid = (glkPayroll.EditValue != null ? glkPayroll.EditValue.ToString() : string.Empty);
                            if (payrollid == string.Empty)
                            {
                                rtn = false;
                                MessageRender.ShowMessage("A Payroll must be selected.");
                                glkPayroll.Select();
                                glkPayroll.Focus();
                                break;
                            }
                            else
                            {
                                ReportProperty.Current.PayrollName = glkPayroll.Text;
                                ReportProperty.Current.PayrollId = payrollid;

                                if (glkPayroll.Properties.DataSource != null)
                                {
                                    int PayrollId = UtilityMember.NumberSet.ToInteger(payrollid);
                                    DataTable dtPayroll = glkPayroll.Properties.DataSource as DataTable;
                                    if (PayrollId > 0 && dtPayroll != null && dtPayroll.Rows.Count > 0)
                                    {
                                        dtPayroll.DefaultView.RowFilter = "";
                                        dtPayroll.DefaultView.RowFilter = "PAYROLLID = " + PayrollId;
                                        if (dtPayroll.DefaultView.Count > 0)
                                        {
                                            ReportProperty.Current.PayrollPayrollDate = dtPayroll.DefaultView[0]["PRDATE"].ToString();
                                        }
                                        dtPayroll.DefaultView.RowFilter = "";
                                    }
                                }
                            }
                            rtn = true;
                            break;
                        }
                    case "PS":
                        {
                            string PayrollStaffIds = GetSelectedStaffId();
                            if (PayrollStaffIds == string.Empty)
                            {
                                rtn = false;
                                MessageRender.ShowMessage("A Payroll Staff must be selected.");
                                gcStaff.Select();
                                gvStaff.Focus();
                                break;
                            }
                            else
                            {
                                ReportProperty.Current.PayrollStaffId = PayrollStaffIds;
                            }
                            rtn = true;
                            break;
                        }
                    case "APC":   //All Payroll component
                    case "EPC":   //Earning Payroll component
                    case "DPC":   //Deduction Payroll component
                    case "ECPC":  //Earning and Calculation Payroll component
                    case "DCPC":  //Deduction and Calculation Payroll component
                    case "ECIPC":  //Earning, Calculation and ESI Payroll component
                        {
                            string SelectedComponents = SelectedPayrollComponendsIds;
                            if (SelectedComponents == string.Empty)
                            {
                                rtn = false;
                                MessageRender.ShowMessage("A Payroll Component must be selected.");
                                glkComponents.Select();
                                glkComponents.Focus();
                                break;
                            }
                            else
                            {
                                ReportProperty.Current.PayrollComponentId = SelectedPayrollComponendsIds;
                                ReportProperty.Current.PayrollComponentName = SelectedPayrollComponendsName;
                            }
                            rtn = true;
                            break;
                        }
                    case "EEC":  //Payroll Employee component
                        {
                            string SelectedComponents = SelectedPayrollComponendsIds1;
                            if (SelectedComponents == string.Empty)
                            {
                                rtn = false;
                                MessageRender.ShowMessage("A Payroll Employee Component must be selected.");
                                glkComponents1.Select();
                                glkComponents1.Focus();
                                break;
                            }
                            else
                            {
                                ReportProperty.Current.PayrollComponentId1 = SelectedComponents;
                                ReportProperty.Current.PayrollComponentName1 = SelectedPayrollComponendsName1;
                            }
                            rtn = true;
                            break;
                        }
                    case "ERC":   //Payroll Employer component
                        {
                            string SelectedComponents = SelectedPayrollComponendsIds2;
                            if (SelectedComponents == string.Empty && ReportProperty.Current.ReportId !="RPT-203")
                            {
                                rtn = false;
                                MessageRender.ShowMessage("A Payroll Employer Component must be selected.");
                                glkComponents2.Select();
                                glkComponents2.Focus();
                                break;
                            }
                            else
                            {
                                ReportProperty.Current.PayrollComponentId2 = SelectedComponents;
                                ReportProperty.Current.PayrollComponentName2 = SelectedPayrollComponendsName2;
                            }
                            rtn = true;
                            break;
                        }
                    case "SD": //Sing details
                        {
                            ReportProperty.Current.IncludeSignDetails = 0;
                            if (chkIncludeSignDetails.Checked)
                            {
                                ReportProperty.Current.IncludeSignDetails = 1;
                            }
                            break;
                        }
                    case "PCD": //Show Payroll Component Description
                        {
                            ReportProperty.Current.PayrollShowComponentDescription = 0;
                            if (chkShowComDescription.Checked)
                            {
                                ReportProperty.Current.PayrollShowComponentDescription = 1;
                            }
                            
                            ReportProperty.Current.PayrollPayslipInSeparatePages= 0;
                            if (chkEachPayslipSeparatePage.Checked)
                            {
                                ReportProperty.Current.PayrollPayslipInSeparatePages = 1;
                            }
                            break;
                        }
                    case "PGC": //Show Paygroup Consolidation
                        {
                            ReportProperty.Current.PayrollGroupConsolidation= 0;
                            if (ChkShowPayGroupConsolidation.Checked)
                            {
                                ReportProperty.Current.PayrollGroupConsolidation = 1;
                            }

                            break;
                        }
                    case "PPD": //Payroll department
                        {
                            ReportProperty.Current.PayrollDepartmentId = SelectedPayrollDepartmentIds; 
                            ReportProperty.Current.PayrollDepartmentName= selectedDepartmentName;
                            break;
                        }
                }

                if (!rtn)
                {
                    break;
                }
            }

            if (rtn)
            {
                //Validate Title and BanK Details are mandatory for Advice Payment to Bank reports----------------------------------
                if (ReportProperty.Current.ReportId == "RPT-170")
                {
                    if (string.IsNullOrEmpty(txtPayslipProjectTitle.Text))
                    {
                        rtn = false;
                        MessageRender.ShowMessage("Title is empty");
                        txtPayslipProjectTitle.Select();
                        txtPayslipProjectTitle.Focus();
                    }
                    else if (lcBankAc.Visibility==LayoutVisibility.Always && (glkBankAc.EditValue == null || String.IsNullOrEmpty(glkBankAc.EditValue.ToString())))
                    {
                        rtn = false;
                        MessageRender.ShowMessage("Bank Account is empty");
                        glkBankAc.Select();
                        glkBankAc.Focus();
                    }
                }
                else if (ReportProperty.Current.ReportId == "RPT-203")
                {
                    string[] SelComponent = ReportProperty.Current.PayrollComponentId.Split(',');
                    string[] EmployeeSelComponent = ReportProperty.Current.PayrollComponentId1.Split(',');
                    string[] EmployerSelComponent = ReportProperty.Current.PayrollComponentId2.Split(',');

                    //For Earnining Component
                    bool same1 = SelComponent.Any(EmployeeSelComponent.Contains);
                    bool same2 = SelComponent.Any(EmployerSelComponent.Contains);
                    bool same3 = EmployeeSelComponent.Any(EmployerSelComponent.Contains);

                    if (same1 || same2)
                    {
                        rtn = false;
                        MessageRender.ShowMessage("Same Earnining Components are selected in Employee and Employer");
                        glkComponents1.Select();
                        glkComponents1.Focus();
                        glkComponents.Select();
                        glkComponents.Focus();
                    }
                    else if (same3)
                    {
                        rtn = false;
                        MessageRender.ShowMessage("Same Components are selected between Employee and Employer");
                        glkComponents1.Select();
                        glkComponents1.Focus();
                    }


                }
                //------------------------------------------------------------------------------------------------------------------
            }

            return rtn;
        }

        private void LoadAllBanksAccounts()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.BankAccountFetchAll))
            {
                //using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchAllBankAccounts))
                //dataManager.Parameters.Add(this.appSchema.BankAccount.DATE_CLOSEDColumn, ReportProperty.Current.p);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    DataTable dtBankAccounts = resultArgs.DataSource.Table;
                    UtilityMember.ComboSet.BindGridLookUpCombo(glkBankAc, dtBankAccounts, "BANK_BRANCH", this.appSchema.BankAccount.LEDGER_IDColumn.ColumnName);
                    if (dtBankAccounts.Rows.Count > 0)
                    {
                        glkBankAc.EditValue = ReportProperty.Current.PayrollPaymentBankAccountLedgerId;
                    }
                }
            }
        }

        private void LoadPaymentModes()
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    ResultArgs resultArgs = Paysystem.FetchPayrollPaymentMode();
                    glkpPaymentMode.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpPaymentMode, resultArgs.DataSource.Table, Paysystem.AppSchema.STFPERSONAL.PAYMENT_MODEColumn.ColumnName,
                                Paysystem.AppSchema.STFPERSONAL.PAYMENT_MODE_IDColumn.ColumnName, true, " ");

                        if (resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            glkpPaymentMode.EditValue = ReportProperty.Current.PayrollPaymentModeId;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadPayrollDepartment()
        {
            try
            {
                using (PayrollDepartmentSystem payrolldepartmentsystem = new PayrollDepartmentSystem())
                {
                    ResultArgs resultArgs = payrolldepartmentsystem.FetchPayrollDepartments();
                    if (resultArgs.Success)
                    {
                        DataTable dtDepartment = resultArgs.DataSource.Table;

                        chkLstPayDepartment.Properties.DataSource = dtDepartment;
                        chkLstPayDepartment.Properties.ValueMember = payrolldepartmentsystem.AppSchema.PayrollDepartment.DEPARTMENT_IDColumn.ColumnName;
                        chkLstPayDepartment.Properties.DisplayMember = payrolldepartmentsystem.AppSchema.PayrollDepartment.DEPARTMENTColumn.ColumnName;
                        SelectedPayrollDepartmentIds = string.Empty;
                        if (!string.IsNullOrEmpty(ReportProperty.Current.PayrollDepartmentId))
                        {
                            SelectedPayrollDepartmentIds = ReportProperty.Current.PayrollDepartmentId;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public void FillPayrollComponent(string GroupIds)
        {
            ResultArgs result = new ResultArgs();
            Int32 payrollid = glkPayroll.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkPayroll.EditValue.ToString()) : 0;
            if (String.IsNullOrEmpty(GroupIds))
            {
                GroupIds = "0";
            }
            using (DataManager dataManager = new DataManager(Payroll.DAO.Schema.SQLCommand.Payroll.FetchComponentByGroupIds))
            {
                dataManager.Parameters.Add(dtcomp.IDsColumn, GroupIds);
                dataManager.Parameters.Add(dtcomp.PAYROLLIDColumn, payrollid);
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {
                DataTable dtPayComponents = result.DataSource.Table;
                string componentfilter = "(" + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " <>" + (Int32)PayRollProcessComponent.GrossWages + " AND "
                                           + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " <>" + (Int32)PayRollProcessComponent.Deductions + " AND "
                                           + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + "<>" + (Int32)PayRollProcessComponent.NetPay + ")";
                string sordflds = dtcomp.COMPORDERColumn.ColumnName;

                //On 01/03/2022, To load compontens based on setings
                string ReportCriteria = ReportProperty.Current.ReportCriteria;
                string[] aReportCriteria = ReportCriteria.Split('ÿ');
                if (Array.IndexOf(aReportCriteria, "EPC") >= 0 || Array.IndexOf(aReportCriteria, "ECPC") >= 0)
                {  //Load all Earning Payroll components/Earnining and Calcuation
                    componentfilter += string.IsNullOrEmpty(componentfilter) ? string.Empty : " AND ";
                    componentfilter += "(" + dtcomp.TYPEColumn.ColumnName + " IN (" + ((Array.IndexOf(aReportCriteria, "EPC") >= 0)? "0" : "0,3")  + "))";

                    if (ReportProperty.Current.ReportId == "RPT-203" || ReportProperty.Current.ReportId == "RPT-171")
                    { //For EPF Register format 1, format 2 (load only earnining calculation)
                        componentfilter += string.IsNullOrEmpty(componentfilter) ? string.Empty : " AND ";
                        componentfilter += "(" + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = " + (Int32)PayRollProcessComponent.None + ")";
                    }
                    dtPayComponents.DefaultView.RowFilter = componentfilter;
                }
                else if (Array.IndexOf(aReportCriteria, "DPC") >= 0 || Array.IndexOf(aReportCriteria, "DCPC") >= 0)
                {   //Load all Deduction Payroll components/ Deduction and Calculation
                    componentfilter += string.IsNullOrEmpty(componentfilter) ? string.Empty : " AND ";
                    componentfilter += "(" + dtcomp.TYPEColumn.ColumnName + " IN (" + ((Array.IndexOf(aReportCriteria, "DCPC") >= 0) ? "1" : "1,3") + "))";
                    dtPayComponents.DefaultView.RowFilter = componentfilter;
                }
                else if (Array.IndexOf(aReportCriteria, "ECIPC") >= 0) //Earning, Calculation and ESI Payroll component
                {
                    componentfilter += string.IsNullOrEmpty(componentfilter) ? string.Empty : " AND ";
                    componentfilter += "( (" + dtcomp.TYPEColumn.ColumnName + " IN (0, 3) AND  " 
                                           + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = " + (Int32)PayRollProcessComponent.None + ") OR " +
                            dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = " + (Int32)PayRollProcessComponent.ESI + ")";
                    dtPayComponents.DefaultView.RowFilter = componentfilter;

                    //Fix Incomes and Orders (All Component first and then ESI)
                    string reportorder = "IIF(" + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = " + (Int32)PayRollProcessComponent.None +
                                            ", 0, 1)";
                    dtPayComponents.Columns.Add("ReportOrder", typeof(System.Int32), reportorder);
                    sordflds = "ReportOrder, " + dtcomp.COMPORDERColumn.ColumnName;
                }
                else
                {
                    dtPayComponents.DefaultView.RowFilter = componentfilter;
                }
                dtPayComponents.DefaultView.Sort = sordflds;
                dtPayComponents = dtPayComponents.DefaultView.ToTable();

                UtilityMember.ComboSet.BindGridLookUpCombo(glkComponents, dtPayComponents, "COMPONENT", dtcomp.COMPONENTIDColumn.ColumnName);
                this.BeginInvoke(new MethodInvoker(glkComponents.ShowPopup));
                this.BeginInvoke(new MethodInvoker(glkComponents.ClosePopup));

                if (dtPayComponents.Rows.Count > 0)
                {
                    glkComponents.EditValue = ReportProperty.Current.PayrollComponentId;
                }
            }
        }

        public void FillPayrollComponent1(string GroupIds)
        {
            ResultArgs result = new ResultArgs();
            Int32 payrollid = glkPayroll.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkPayroll.EditValue.ToString()) : 0;
            if (String.IsNullOrEmpty(GroupIds))
            {
                GroupIds = "0";
            }
            using (DataManager dataManager = new DataManager(Payroll.DAO.Schema.SQLCommand.Payroll.FetchComponentByGroupIds))
            {
                dataManager.Parameters.Add(dtcomp.IDsColumn, GroupIds);
                dataManager.Parameters.Add(dtcomp.PAYROLLIDColumn, payrollid);
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {
                DataTable dtPayComponents = result.DataSource.Table;
                string componentfilter = "(" + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " <>" + (Int32)PayRollProcessComponent.GrossWages + " AND "
                                           + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " <>" + (Int32)PayRollProcessComponent.Deductions + " AND "
                                           + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + "<>" + (Int32)PayRollProcessComponent.NetPay + ")";
                string sordflds = dtcomp.COMPORDERColumn.ColumnName;

                //On 01/03/2022, To load compontens based on setings
                string ReportCriteria = ReportProperty.Current.ReportCriteria;
                string[] aReportCriteria = ReportCriteria.Split('ÿ');
                if (Array.IndexOf(aReportCriteria, "ERC") >= 0)
                {   //Payroll Employer component
                    componentfilter += string.IsNullOrEmpty(componentfilter) ? string.Empty : " AND ";
                    componentfilter += "(" +  dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = " + (Int32)PayRollProcessComponent.EPF + ")";
                    dtPayComponents.DefaultView.RowFilter = componentfilter;
                }
                else
                {
                    dtPayComponents.DefaultView.RowFilter = componentfilter;
                }
                dtPayComponents.DefaultView.Sort = sordflds;
                dtPayComponents = dtPayComponents.DefaultView.ToTable();

                UtilityMember.ComboSet.BindGridLookUpCombo(glkComponents1, dtPayComponents, "COMPONENT", dtcomp.COMPONENTIDColumn.ColumnName);
                this.BeginInvoke(new MethodInvoker(glkComponents1.ShowPopup));
                this.BeginInvoke(new MethodInvoker(glkComponents1.ClosePopup));

                if (dtPayComponents.Rows.Count > 0)
                {
                    glkComponents1.EditValue = ReportProperty.Current.PayrollComponentId1;
                }
            }
        }

        public void FillPayrollComponent2(string GroupIds)
        {
            ResultArgs result = new ResultArgs();
            Int32 payrollid = glkPayroll.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkPayroll.EditValue.ToString()) : 0;
            if (String.IsNullOrEmpty(GroupIds))
            {
                GroupIds = "0";
            }
            using (DataManager dataManager = new DataManager(Payroll.DAO.Schema.SQLCommand.Payroll.FetchComponentByGroupIds))
            {
                dataManager.Parameters.Add(dtcomp.IDsColumn, GroupIds);
                dataManager.Parameters.Add(dtcomp.PAYROLLIDColumn, payrollid);
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                result = dataManager.FetchData(DataSource.DataTable);
            }

            if (result.Success)
            {
                DataTable dtPayComponents = result.DataSource.Table;
                string componentfilter = "(" + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " <>" + (Int32)PayRollProcessComponent.GrossWages + " AND "
                                           + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " <>" + (Int32)PayRollProcessComponent.Deductions + " AND "
                                           + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + "<>" + (Int32)PayRollProcessComponent.NetPay + ")";
                string sordflds = dtcomp.COMPORDERColumn.ColumnName;

                //On 01/03/2022, To load compontens based on setings
                string ReportCriteria = ReportProperty.Current.ReportCriteria;
                string[] aReportCriteria = ReportCriteria.Split('ÿ');
                if (Array.IndexOf(aReportCriteria, "EEC") >= 0)
                {  //Payroll Employee component
                    componentfilter += string.IsNullOrEmpty(componentfilter) ? string.Empty : " AND ";
                    componentfilter += "(" +  dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = " + (Int32)PayRollProcessComponent.EPF + ")";
                    dtPayComponents.DefaultView.RowFilter = componentfilter;
                }
                else
                {
                    dtPayComponents.DefaultView.RowFilter = componentfilter;
                }
                dtPayComponents.DefaultView.Sort = sordflds;
                dtPayComponents = dtPayComponents.DefaultView.ToTable();

                UtilityMember.ComboSet.BindGridLookUpCombo(glkComponents2, dtPayComponents, "COMPONENT", dtcomp.COMPONENTIDColumn.ColumnName);
                this.BeginInvoke(new MethodInvoker(glkComponents2.ShowPopup));
                this.BeginInvoke(new MethodInvoker(glkComponents2.ClosePopup));

                if (dtPayComponents.Rows.Count > 0)
                {
                    glkComponents2.EditValue = ReportProperty.Current.PayrollComponentId2;
                }
            }
        }

        /// <summary>
        /// On 25/02/2022, To load Title Types which includes
        /// 1. Custom
        /// 2. Institution/Community
        /// 3. Legal Entity/Society 
        /// </summary>
        private void LoadTitleTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchAll))
            {
                //using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchAllBankAccounts))
                //dataManager.Parameters.Add(this.appSchema.BankAccount.DATE_CLOSEDColumn, ReportProperty.Current.p);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                ResultArgs resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    DataTable dtTitleTypes = resultArgs.DataSource.Table;
                    DataColumn dc = new DataColumn("Title_Type", typeof(Int32));
                    dc.DefaultValue = 3;
                    dtTitleTypes.Columns.Add(dc);

                    Int32 MaxTitleTypeid = UtilityMember.NumberSet.ToInteger(dtTitleTypes.Compute("MAX(" + this.appSchema.LegalEntity.CUSTOMERIDColumn.ColumnName + ")", string.Empty).ToString());

                    //2. Insert Institution Type
                    DataRow drDefaultTitle = dtTitleTypes.NewRow();
                    drDefaultTitle[this.appSchema.LegalEntity.CUSTOMERIDColumn.ColumnName] = MaxTitleTypeid + 2;
                    drDefaultTitle[this.appSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName] = "Institution/Community";
                    drDefaultTitle["Title_Type"] = 2;
                    dtTitleTypes.Rows.InsertAt(drDefaultTitle, 0);

                    //1. Insert Custom Type
                    drDefaultTitle = dtTitleTypes.NewRow();
                    drDefaultTitle[this.appSchema.LegalEntity.CUSTOMERIDColumn.ColumnName] = MaxTitleTypeid + 1;
                    drDefaultTitle[this.appSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName] = "Custom";
                    drDefaultTitle["Title_Type"] = 1;
                    dtTitleTypes.Rows.InsertAt(drDefaultTitle, 0);

                    UtilityMember.ComboSet.BindGridLookUpCombo(glkTitleType, dtTitleTypes, this.appSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName,
                                    this.appSchema.LegalEntity.CUSTOMERIDColumn.ColumnName);

                    if (dtTitleTypes.Rows.Count > 0)
                    {
                        if (ReportProperty.Current.PayrollTitleType != null)
                        {
                            glkTitleType.EditValue = ReportProperty.Current.PayrollTitleType;
                        }
                        else
                        {
                            glkTitleType.EditValue = 0;
                        }
                    }
                }
            }
        }

        //private void gvPayrollGroup_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    if (lcComponents.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        //    {
        //        FillPayrollComponent(SelectedPayrollGroupIds);
        //    }
            
        //    if (lcComponents1.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        //    {
        //        FillPayrollComponent1(SelectedPayrollGroupIds);
        //    }
        //    if (lcComponents2.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        //    {
        //        FillPayrollComponent2(SelectedPayrollGroupIds);
        //    }

        //    if (gcStaff.Visible)
        //    {
        //        FillStaff(SelectedPayrollGroupIds);
        //    }
        //}

        private void glkComponents_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            int[] selectedrows = gvComponent.GetSelectedRows();
            e.DisplayText = "(" + selectedrows.Length.ToString() + ") Payroll Component(s) are selected";
        }

        private void glkComponents_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;

            string[] payrollComponendIds = ReportProperty.Current.PayrollComponentId.Split(',');
            foreach (string compid in payrollComponendIds)
            {
                for (int i = 0; i < gvComponent.DataRowCount; i++)
                {
                    if (gvComponent.GetRowCellValue(i, colComponentId) != null)
                    {
                        string getvalue = gvComponent.GetRowCellValue(i, colComponentId).ToString();
                        if (getvalue != null && getvalue.Equals(compid))
                        {
                            int rowHandle = gvComponent.GetRowHandle(i);

                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            {
                                edit.Properties.View.SelectRow(rowHandle);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        private void picSign1_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void picSign2_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void picSign3_MouseUp(object sender, MouseEventArgs e)
        {
            PictureEdit picedit = sender as PictureEdit;
            HideContextMenu(picedit);
        }

        private void HideContextMenu(PictureEdit picedit)
        {
            DXPopupMenu menu = null;
            if (menu == null)
            {
                PropertyInfo info = typeof(PictureEdit).GetProperty("Menu", BindingFlags.NonPublic | BindingFlags.Instance);
                menu = info.GetValue(picedit, null) as DXPopupMenu;
                foreach (DXMenuItem item in menu.Items)
                {
                    //if (item.Caption.ToUpper() == "LOAD" || item.Caption.ToUpper() == "SAVE")
                    //{
                    item.Visible = false;
                    //}
                }
            }
        }

        private void btnSign1_Click(object sender, EventArgs e)
        {
            SetSignImage(1);
        }

        private void btnSign2_Click(object sender, EventArgs e)
        {
            SetSignImage(2);
        }

        private void btnSign3_Click(object sender, EventArgs e)
        {
            SetSignImage(3);
        }

        private void txtPayslipProjectTitle_Validating(object sender, CancelEventArgs e)
        {
            GetSignDetails(txtPayslipProjectTitle.Text.Trim());
        }

        private void glkTitleType_EditValueChanged(object sender, EventArgs e)
        {
            if (glkTitleType.GetSelectedDataRow() != null)
            {
                DataRowView drv = (glkTitleType.GetSelectedDataRow() as DataRowView);
                Int32 titletype = UtilityMember.NumberSet.ToInteger(drv["Title_Type"].ToString());

                txtPayslipProjectTitle.Text = ReportProperty.Current.PayrollProjectTitle.Trim();
                txtProjectAddress.Text = ReportProperty.Current.PayrollProjectAddress.Trim();

                if (titletype == 3) //Society Title
                {
                    string Society = (drv[appSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName] == null ? string.Empty :
                                drv[appSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString().Trim());
                    string SocietyAddress = (drv[appSchema.LegalEntity.ADDRESSColumn.ColumnName] == null ? string.Empty :
                                drv[appSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString().Trim());
                    txtPayslipProjectTitle.Text = Society;
                    txtProjectAddress.Text = SocietyAddress;
                }
                else if (titletype == 2) //Instution/Community ITitle
                {
                    txtPayslipProjectTitle.Text = SettingProperty.Current.InstituteName;
                    txtProjectAddress.Text = SettingProperty.Current.Address;
                }
            }
        }

        private void glkComponents1_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            int[] selectedrows = gvComponent1.GetSelectedRows();
            e.DisplayText = "(" + selectedrows.Length.ToString() + ") Payroll Employee Component(s) are selected";
        }

        private void glkComponents2_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            int[] selectedrows = gvComponent2.GetSelectedRows();
            e.DisplayText = "(" + selectedrows.Length.ToString() + ") Payroll Employer Component(s) are selected";
        }

        private void glkComponents1_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;

            string[] payrollComponendIds = ReportProperty.Current.PayrollComponentId1.Split(',');
            foreach (string compid in payrollComponendIds)
            {
                for (int i = 0; i < gvComponent1.DataRowCount; i++)
                {
                    DataRow dr = gvComponent1.GetDataRow(i); 
                    if (gvComponent1.GetRowCellValue(i, colComponentId1) != null)
                    {
                        string getvalue = gvComponent1.GetRowCellValue(i, colComponentId1).ToString();
                        if (getvalue != null && getvalue.Equals(compid))
                        {
                            int rowHandle = gvComponent1.GetRowHandle(i);

                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            {
                                edit.Properties.View.SelectRow(rowHandle);
                            }
                        }
                    }
                }
            }
        }

        private void glkComponents2_Popup(object sender, EventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;

            string[] payrollComponendIds = ReportProperty.Current.PayrollComponentId2.Split(',');
            foreach (string compid in payrollComponendIds)
            {
                for (int i = 0; i < gvComponent2.DataRowCount; i++)
                {
                    if (gvComponent2.GetRowCellValue(i, colComponentId2) != null)
                    {
                        string getvalue = gvComponent2.GetRowCellValue(i, colComponentId2).ToString();
                        if (getvalue != null && getvalue.Equals(compid))
                        {
                            int rowHandle = gvComponent2.GetRowHandle(i);

                            if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            {
                                edit.Properties.View.SelectRow(rowHandle);
                            }
                        }
                    }
                }
            }
        }

        private void glkComponents1_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void gvComponent1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (glkComponents1.Properties.DataSource != null)
            //{
            //    DataTable dt = glkComponents2.Properties.DataSource as DataTable;
            //    dt.DefaultView.RowFilter = string.Empty;

            //    string compselected1 = SelectedPayrollComponendsIds1;
            //    if (!string.IsNullOrEmpty(compselected1))
            //    {
            //        string componentfilter = dtcomp.COMPONENTIDColumn.ColumnName + " NOT IN (" + compselected1 + ")";
            //        dt.DefaultView.RowFilter = componentfilter;
            //        glkComponents2.Properties.DataSource = dt.DefaultView.ToTable();
            //    }
            //}
        }

        private void chkLstPayGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (lcComponents.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                FillPayrollComponent(SelectedPayrollGroupIds);
            }

            if (lcComponents1.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                FillPayrollComponent1(SelectedPayrollGroupIds);
            }
            if (lcComponents2.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                FillPayrollComponent2(SelectedPayrollGroupIds);
            }

            if (gcStaff.Visible)
            {
                FillStaff(SelectedPayrollGroupIds);
            }
        }

        private void glkpLeavingReason_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

        }

        private void glkComponents_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkpPaymentMode_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpPaymentMode.EditValue != null)
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = glkpPaymentMode.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                DataRowView drvPaymentMode = glkpPaymentMode.GetSelectedDataRow() as DataRowView;
                if (drvPaymentMode != null)
                {
                    string PaymentMode = string.IsNullOrEmpty(drvPaymentMode["PAYMENT_MODE"].ToString()) ?
                                                string.Empty : drvPaymentMode["PAYMENT_MODE"].ToString();
                    Int32 isbank = string.IsNullOrEmpty(drvPaymentMode["IS_BANK"].ToString()) ? 0 :
                            UtilityMember.NumberSet.ToInteger(drvPaymentMode["IS_BANK"].ToString());
                    lcBankAc.Visibility = isbank==1 ? LayoutVisibility.Always :  LayoutVisibility.Never;  
                    

                }
            }
        }

    }
}