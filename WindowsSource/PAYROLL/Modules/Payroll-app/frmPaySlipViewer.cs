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

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPaySlipViewer : frmPayrollBase
    {
        #region Declaration
        private clsModPay objModPay = new clsModPay();
        private clsPrinterSettings objprintersettings = new clsPrinterSettings();
        private SqlException objEx;
        CommonMember commonmem = new CommonMember();
        int currentIndex = 0;
        int iEditcurrentIndex = -1;
        string[] strAllLines = new string[100];
        private string[] strPrintArr;
        //	private clsAdmission objAdmission	= new clsAdmission();
        private clsPRPSReport paySlipReport = new clsPRPSReport();
        //private CheckBox chkAll;

        #endregion

        #region Constructors
        public frmPaySlipViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void btnShow_Click(object sender, EventArgs e)
        {
            string sPa = "";
            string strReportName = "";
            string sText = "";
            string sStaffId = "";
            if (!Validate())
                return;
            sPa = Application.StartupPath + "\\REGISTERS.dat";
            strReportName = "PAY-PAYSLILP";
            string newLine = strReportName + "@" + glkpPayPrinters.Text + "@" + txtPath.Text + "@" + chkPrintPay.Checked;

            if (File.Exists(sPa))
            {
                StreamReader sr = new StreamReader(sPa);
                sText = sr.ReadToEnd();
                sr.Close();
                //MessageBox.Show(sText.IndexOf(strReportName,0,sText.Length).ToString());
                if (sText.IndexOf(strReportName, 0, sText.Length) == -1)
                {
                    StreamWriter sw = new StreamWriter(sPa, true);
                    sw.WriteLine(newLine);
                    sw.Close();
                }

            }
            else
            {
                File.Create(sPa).Close();
                StreamWriter sw = new StreamWriter(sPa, true);
                sw.WriteLine(newLine);
                sw.Close();
            }
            for (int i = 0; i < chklstSelectStaff.ItemCount; i++)
            {
                if (chklstSelectStaff.GetItemChecked(i))
                {
                    chklstSelectStaff.SelectedIndex = i;
                    DataTable dtStaff = chklstSelectStaff.DataSource as DataTable;
                    sStaffId = sStaffId + dtStaff.Rows[i]["staffid"].ToString() + ",";
                    //sStaffId = sStaffId + ",";
                }
            }
            if (chklstSelectStaff.CheckedItems.Count > 0)
                sStaffId = sStaffId.Substring(0, sStaffId.Length - 1);
            //write multiple copies in the notepad
            //WriteNotepad(chklstStaff.CheckedItems.Count);
            clsPRPSReport objPRPaySlip = new clsPRPSReport();
            try
            {
                string[] strPrintMsg;
                DataView dvPRPaySlip = objPRPaySlip.CreateReportTable();
                if (chkAll.Checked && glkpGroupValue.SelectionStart == 0)
                    dvPRPaySlip.RowFilter = "StaffId IN(" + sStaffId + ") ";
                else
                    dvPRPaySlip.RowFilter = "StaffId IN(" + sStaffId + ") and GroupId = " + glkpGroupValue.EditValue.ToString();
                clsPayrollSlipPrinting objPrint = new clsPayrollSlipPrinting();

                objPrint.FieldWidth = 15;

                objPrint.TemplatePath = txtPath.Text.Trim();

                //Included By Jules on 20.05.2014
                DataTable dt = objPRPaySlip.FetchHeaderDetails();
                string criteria = string.Empty;
                if (chkAll.Checked && glkpGroupValue.SelectionStart == 0)
                    criteria = "AND SS.STAFFID IN(" + sStaffId + ") ";
                else
                    criteria = "AND SS.STAFFID IN(" + sStaffId + ") AND GROUPID = " + glkpGroupValue.EditValue.ToString();
                DataTable dvPaySlip = objPRPaySlip.FetchValuesForPaySlip(criteria);

                strPrintMsg = objPrint.PrintPayrollSlip(dvPRPaySlip, dt, dvPaySlip);

                string strPrint = "";

                for (int i = 0; i < strPrintMsg.Length; i++)
                {
                    strPrint = strPrint + strPrintMsg[i];
                }
                if (strPrintMsg.Length % 2 == 0)
                {
                    int n = strPrintMsg.Length / 2;
                    strPrintArr = new string[n];

                    for (int i = 0, j = 0; j < n; i += 2, j++)
                    {
                        strPrintArr[j] = strPrintMsg[i].ToString() + strPrintMsg[i + 1].ToString();
                    }
                }
                else
                {
                    int n = strPrintMsg.Length / 2;
                    strPrintArr = new string[n + 1];
                    for (int i = 0, j = 0; j < n; i += 2, j++)
                    {
                        strPrintArr[j] = strPrintMsg[i].ToString() + strPrintMsg[i + 1].ToString();
                    }
                    strPrintArr[n] = strPrintMsg[strPrintMsg.Length - 1];
                }
                //frmViewRegisters objViewPrint = new frmViewRegisters(strPrintArr, glkpPayPrinters.Text, "PaySlip");
                //objViewPrint.rtbReportViewer.Text = strPrint;
                //objViewPrint.ShowDialog();
                this.Close();
            }
            catch
            {
            }
        }

        //private void frmPaySlipViewer_Closed(object sender, EventArgs e)
        //{
        //    this.Dispose();
        //}

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAll.Checked)
            //{
            //    chklstSelectStaff.SelectAll();
            //}
            //else
            //{
            //    chklstSelectStaff.UnSelectAll();
            //}

            if (chkAll.Checked)     //To Select All Staffs
            {
                for (int i = 0; i < chklstSelectStaff.ItemCount; i++)
                {
                    chklstSelectStaff.SetItemChecked(i, true);
                }
            }
            else                  // To Unselect All Staffs
            {
                for (int i = 0; i < chklstSelectStaff.ItemCount; i++)
                {
                    chklstSelectStaff.SetItemChecked(i, false);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            txtPath.Text = openFileDialog1.FileName;
        }

        private void glkpGroupValue_EditValueChanged(object sender, EventArgs e)
        {
            FillStaff();
        }

        //private void FillCombo(int nGroup)
        //{
        //    paySlipReport.FillSalaryGroup(nGroup, glkpGroupValue);
        //}

        private void frmPaySlipViewer_Load(object sender, EventArgs e)
        {
            lblUserName.Text = clsGeneral.USER_NAME;
            FillCombo(0);
            string strPrinters;
            for (int l = 0; l < PrinterSettings.InstalledPrinters.Count; l++)
            {
                strPrinters = PrinterSettings.InstalledPrinters[l];
                glkpPayPrinters.Properties.Items.Add(strPrinters);

            }

            readFile();
        }

        //private void menuItem1_Click(object sender, System.EventArgs e)
        //{
        //    chklstStaff.SetItemChecked((chklstSelectStaff.SelectedIndex), true);
        //}

        //private void menuItem2_Click(object sender, System.EventArgs e)
        //{
        //    chklstStaff.SetItemChecked((chklstStaff.SelectedIndex), false);
        //}

        //private void menuItem3_Click(object sender, System.EventArgs e)
        //{
        //    for (int i = 0; i < chklstStaff.Items.Count; i++)
        //        chklstStaff.SetItemChecked(i, true);
        //}

        //private void menuItem4_Click(object sender, System.EventArgs e)
        //{
        //    for (int i = 0; i < chklstStaff.Items.Count; i++)
        //        chklstStaff.SetItemChecked(i, false);
        //}

        #endregion

        #region Methods

        private bool Validate()
        {

            if (glkpPayPrinters.Text == "")
            {
                //XtraMessageBox.Show("Select any printer ", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PaySlipViewer.PAY_SLIP_SELECT_PRINTER_INFO));
                glkpPayPrinters.Focus();
                return false;
            }

            if (txtPath.Text == "")
            {
                //XtraMessageBox.Show("Select the template for pay slip", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PaySlipViewer.PAY_SLIP_TEMPLATE_SELECT_INFO));
                txtPath.Focus();
                return false;
            }
            if (chklstSelectStaff.CheckedItems.Count == 0)
            {
                //XtraMessageBox.Show("Select staff for pay slip", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PaySlipViewer.PAY_SLIP_SEELCT_STAFF_INFO));
                chklstSelectStaff.Focus();
                return false;
            }
            return true;
        }

        private void WriteNotepad(int nTimes)
        {
            int NoOFTimes = nTimes;
            string NotepadContents = "";
            string ReplaceVal = "";
            if (NoOFTimes > 0)
            {
                string path = txtPath.Text;
                string ReplacedStr = "";
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        String line;
                        if ((line = sr.ReadToEnd()) != null)
                            NotepadContents = line;
                        sr.Close();
                    }
                    //Write into the file
                    using (StreamWriter sw = new StreamWriter(path))
                    {

                        for (int j = 1; j <= NoOFTimes; j++)
                        {
                            if (j < 10)
                                ReplaceVal = "0" + j;
                            else
                                ReplaceVal = j.ToString();
                            ReplacedStr = NotepadContents.Replace("01", ReplaceVal);
                            sw.WriteLine(ReplacedStr);
                        }
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    //XtraMessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE);
                    return;
                }
            }
        }

        private void FillCombo(int nGroup)
        {
            //paySlipReport.FillSalaryGroup(nGroup, glkpGroupValue);
            LoadGroupList();
        }


        private void FillStaff()
        {

            clsPRPSReport obj = new clsPRPSReport();
            int SelectedGrp = commonmem.NumberSet.ToInteger(glkpGroupValue.EditValue.ToString());
            obj.FillStaffListfroDevexpressChklst(SelectedGrp, chklstSelectStaff);
        }

        //public void GetStaffDetails()
        //{
        //    try
        //    {
        //        using (clsPayrollStaff objpayrollStaff=new clsPayrollStaff())
        //        {
        //            objpayrollStaff.getStaffDetails();


        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally { }
        //}


        private void LoadGroupList()
        {
            try
            {
                DataTable dtGradeList;
                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {
                    dtGradeList = Grade.getPayrollGradeList();
                    if (dtGradeList != null && dtGradeList.Rows.Count > 0)
                    {
                        dtGradeList.Columns["GROUP ID"].ColumnMapping = MappingType.Hidden;
                        glkpGroupValue.Properties.DataSource = dtGradeList;
                        glkpGroupValue.Properties.ValueMember = "GROUP ID";
                        glkpGroupValue.Properties.DisplayMember = "Group Name";
                        //glkpGroupValue.RefreshEditValue();
                    }
                    else
                    {
                        glkpGroupValue.Properties.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }

        }
        private void readFile()
        {

            glkpPayPrinters.Text = "";
            txtPath.Text = "";
            chkPrintPay.Checked = false;
            int i = 0;

            string sPa = Application.StartupPath + "\\REGISTERS.dat";
            if (!File.Exists(sPa))
            {
                File.Create(sPa).Close();
            }
            else
            {
                StreamReader SR = null;
                try
                {
                    SR = new StreamReader(sPa);

                    string[] strLine = new string[0];

                    string strTemp = "";
                    i = 0;
                    iEditcurrentIndex = -1;
                    while ((strTemp = SR.ReadLine()) != null && i < 100)
                    {
                        strLine = strTemp.Split('@');
                        strAllLines[i] = strTemp;

                        currentIndex = i + 1;
                        if (strLine[0] == "PAY-PAYSLILP")
                        {
                            iEditcurrentIndex = i;
                            glkpPayPrinters.Text = strLine[1];
                            txtPath.Text = strLine[2].ToString();
                            chkPrintPay.Checked = Convert.ToBoolean(strLine[3]);
                        }

                        i = i + 1;
                    }

                    SR.Close();
                }
                catch (Exception ex)
                {
                    string ss = ex.Message;
                    return;
                }
            }
        }
    }
}
        #endregion

