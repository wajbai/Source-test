using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.IO;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.Setting;
using ACPP.Modules.Transaction;
using DevExpress.XtraBars.Alerter;
using System.Xml;
using AcMEDSync.Model;
using System.Configuration;

namespace ACPP
{
    public partial class frmTest : frmFinanceBase
    {
        SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();

        public frmTest()
        {
            InitializeComponent();
        }

        private void btnUpdateBalanceBS_Click(object sender, EventArgs e)
        {
            /*int projectId = this.UtilityMember.NumberSet.ToInteger(txtProjectId.Text);
            int ledgerId = this.UtilityMember.NumberSet.ToInteger(txtLedgerId.Text);
            string transDate = dtTransDateBS.Value.ToShortDateString();
            double amount = this.UtilityMember.NumberSet.ToDouble(txtAmountBS.Text);
            string transMode = cboTransModeBS.Text;

            amount = -amount;

            using (Bosco.Model.Business.BalanceSystem balanceSystem = new Bosco.Model.Business.BalanceSystem())
            {
                ResultArgs result = balanceSystem.UpdateBalance(transDate, projectId, ledgerId, amount, transMode, "TR");

                if (result.Success)
                {
                    MessageBox.Show("Balance updated successfully");
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }*/
        }

        private void btnUpdateBalanceAS_Click(object sender, EventArgs e)
        {
            /*int projectId = this.UtilityMember.NumberSet.ToInteger(txtProjectId.Text);
            int ledgerId = this.UtilityMember.NumberSet.ToInteger(txtLedgerId.Text);
            string transDate = dtTransDateAS.Value.ToShortDateString();
            double amount = this.UtilityMember.NumberSet.ToDouble(txtAmountAS.Text);
            string transMode = cboTransModeAS.Text;

            using (Bosco.Model.Business.BalanceSystem balanceSystem = new Bosco.Model.Business.BalanceSystem())
            {
                //ResultArgs result = balanceSystem.UpdateBalance(transDate, projectId, ledgerId, amount, transMode, "TR");
                ResultArgs result = balanceSystem.UpdateTransBalance(1, TransactionAction.New);

                if (result.Success)
                {
                    MessageBox.Show("Balance updated successfully");
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }*/
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            // Bosco.Report.Base.IReport report = new Bosco.Report.Base.ReportEntry(this.MdiParent);
            //report.VoucherPrint("62051", UtilityMember.EnumSet.GetDescriptionFromEnumValue(VoucherPrint.CASHBANKRECEIPTS));
            Showme();
        }

        private void btnShowBalance_Click(object sender, EventArgs e)
        {
            //Bosco.Model.Business.BalanceSystem balanceSystem = new Bosco.Model.Business.BalanceSystem();
            //Bosco.Model.Business.BalanceProperty balanceProperty = balanceSystem.GetBalance(1, 192, "2013/05/17", Bosco.Model.Business.BalanceSystem.BalanceType.CurrentBalance);
            //MessageBox.Show(balanceProperty.Amount + " " + balanceProperty.TransMode);
        }

        private void btnUpdateBulkBalance_Click(object sender, EventArgs e)
        {
            BalanceSystem balanceSystem = new BalanceSystem();
            ResultArgs result = balanceSystem.UpdateBulkTransBalance();
            if (result.Success)
            {
                MessageBox.Show("Balance Updated Successfully");
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void Showme()
        {
            //AlertInfo alertInfo = new AlertInfo("Alert", "Negativebalance");
            //AlertControl control = new AlertControl();
            //control.FormLocation = AlertFormLocation.BottomRight;
            //control.Show(null, alertInfo);

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionGroup conSecGroup = config.SectionGroups["system.serviceModel"];
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void btnReadXML_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string FilePath = string.Empty;
            //    string FileName = string.Empty;
            //    string FullFilePath = string.Empty;
            //    string BranchOfficeName = string.Empty;
            //    string LicenseName = string.Empty;

            //    DataSet dsReadXML = new DataSet();
            //    OpenFileDialog opendialog = new OpenFileDialog();
            //    opendialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            //    if (opendialog.ShowDialog() == DialogResult.OK)
            //    {
            //        if (this.ShowConfirmationMessage("Are you sure to import this records?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //        {
            //            FileName = System.IO.Path.GetFileName(opendialog.FileName);
            //            FilePath = System.IO.Path.GetFullPath(opendialog.FileName);
            //            FullFilePath = Path.GetDirectoryName(opendialog.FileName) + Path.DirectorySeparatorChar;
            //            LicenseName = ReadLicenseBranchOffice();
            //            BranchOfficeName = ReadBranchOffice(FilePath);

            //            if (!LicenseName.Equals(BranchOfficeName) && File.Exists(FilePath))
            //            {
            //                if (Directory.Exists(FullFilePath))
            //                {
            //                    DirectoryInfo dInfo = new DirectoryInfo(Path.GetDirectoryName(FullFilePath));
            //                    FileInfo[] file = dInfo.GetFiles("*.xml");

            //                    var lastCreatedFile = dInfo.GetFiles().OrderByDescending(f => f.LastWriteTime).First();

            //                    if (FileName.Equals(lastCreatedFile.ToString()))
            //                    {
            //                        dsReadXML = Bosco.Utility.XMLConverter.XMLConverter.ConvertXMLToDataSet(FilePath);
            //                        using (Bosco.Model.Transaction.DataSynchronization dataSync = new Bosco.Model.Transaction.DataSynchronization())
            //                        {
            //                            dataSync.FetchRecords(dsReadXML);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (this.ShowConfirmationMessage("The selected file is not the recent imported file to import.Are you sure to import?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //                        {
            //                            dsReadXML = Bosco.Utility.XMLConverter.XMLConverter.ConvertXMLToDataSet(FilePath);
            //                            using (Bosco.Model.Transaction.DataSynchronization dataSync = new Bosco.Model.Transaction.DataSynchronization())
            //                            {
            //                                dataSync.FetchRecords(dsReadXML);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                this.ShowMessageBox("The selected XML file is not to this branch office,please select the valid XML file to import.");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            //}
            //finally { }
        }

        private string ReadBranchOffice(string FileName)
        {
            string branchOffice = string.Empty;
            XmlDocument infodoc = new XmlDocument();
            infodoc.Load(FileName);
            XmlNodeList xnList = infodoc.SelectNodes("/NewDataSet/OfficeData");
            foreach (XmlNode xmlNode in xnList)
            {
                branchOffice = xmlNode["BRANCH_OFFICE_CODE"].InnerText != string.Empty ? xmlNode["BRANCH_OFFICE_CODE"].InnerText : string.Empty;
                branchOffice = branchOffice != string.Empty ? objDec.DecryptString(branchOffice) : branchOffice;
            }
            return branchOffice;
        }

        private string ReadLicenseBranchOffice()
        {
            string LicenseBranchOffice = string.Empty;
            XmlDocument infodoc = new XmlDocument();
            infodoc.Load("AcMEERPLicense.xml");
            XmlNodeList xnList = infodoc.SelectNodes("/DocumentElement/LicenseKey");
            foreach (XmlNode xmlNode in xnList)
            {
                LicenseBranchOffice = xmlNode["SocietyName"].InnerText != string.Empty ? xmlNode["SocietyName"].InnerText : string.Empty;
                LicenseBranchOffice = LicenseBranchOffice != string.Empty ? objDec.DecryptString(LicenseBranchOffice) : LicenseBranchOffice;
            }
            return LicenseBranchOffice;
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            DateTime dtFrom = dateEdit1.DateTime;
            DateTime dtTo = dateEdit2.DateTime;

            int days = (dtTo - dtFrom).Days;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEncText.Text))
            {
                txtEncAnsText.Text = objDec.EncryptString(txtEncText.Text);
            }
            else
            {
                XtraMessageBox.Show("Enter Text to Encrypt");
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDecryptText.Text))
            {
                txtDecAnsText.Text = objDec.DecryptString(txtDecryptText.Text);
            }
            else
            {
                XtraMessageBox.Show("Enter Text to Decrypt");
            }
        }

        private void btnEncClear_Click(object sender, EventArgs e)
        {
            txtEncText.Text = txtEncAnsText.Text = string.Empty;
        }

        private void btnDecClear_Click(object sender, EventArgs e)
        {
            txtDecryptText.Text = txtDecAnsText.Text = string.Empty;
        }
    }
}