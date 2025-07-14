using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors.Repository;
using Bosco.Model.UIModel.Master;


namespace ACPP.Modules.Transaction
{
    public partial class frmTransaction : frmBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        public event EventHandler TransactionChanged;
        #endregion

        #region Properties
        private int sourceId = 0;
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }
        #endregion

        #region Constructor
        public frmTransaction()
        {
            InitializeComponent();
        }

        public frmTransaction(int TransactionType)
            : this()
        {
            if (TransactionType == 1)
            {
                rboTransactionType.SelectedIndex = 0;
            }
            else if (TransactionType == 2)
            {
                rboTransactionType.SelectedIndex = 1;
            }
            else
            {
                rboTransactionType.SelectedIndex = 2;
            }
        }
        #endregion

        #region Events
        private void frmTransaction_Load(object sender, EventArgs e)
        {
            LoadProject();
            ShowHideDonor();
            LoadDonorDetails();
            LoadPurposeDetails();
            LoadReceiptType();
            LoadCashBankLedger();
            LoadCostCenter();
            dteTransactionDate.Text = this.UtilityMember.DateSet.GetDateToday();
            // rboTransactionType.SelectedIndex = 0;
        }

        private void rboTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TransactionChanged != null)
            {
                TransactionChanged(this, e);
            }
            ShowHideDonor();
            
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ShowHideDonor();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtReceiptAmount_TextChanged(object sender, EventArgs e)
        {
            ConvertCurrency();
        }

        private void txtExchangeRate_TextChanged(object sender, EventArgs e)
        {
            ConvertCurrency();
        }

        private void glkpReceiptType_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkpCashBankLedger_EditValueChanged(object sender, EventArgs e)
        {
            HideShowBankDetails(rboTransactionType.SelectedIndex);
        }



        #endregion

        #region Methods
        /// <summary>
        /// To load the project Details
        /// </summary>
        private void LoadProject()
        {
            //try
            //{
            //    using (ProjectSystem projectSystem = new ProjectSystem())
            //    {
            //        resultArgs = projectSystem.FetchProjectsLookup();
            //        glkpProject.Properties.DataSource = null;
            //        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
            //        {
            //            this.UtilityMember.ComboSet.BindGridLookUpEdit(glkpProject, resultArgs.DataSource.Table, projectSystem.AppSchema.Project.PROJECTColumn.ColumnName, projectSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
            //            glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
            //        }
            //        else
            //        {
            //            XtraMessageBox.Show(resultArgs.Message);
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageRender.ShowMessage(Ex.Message);
            //}
            //finally { }
        }

        private void LoadLedger(string NatureId)
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchLedgerByGroup();
                    glkpLedger.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpLedger.Properties.DataSource = resultArgs.DataSource.Table;
                        glkpLedger.Properties.DisplayMember = "LEDGER_NAME";
                        glkpLedger.Properties.ValueMember = "LEDGER_ID";
                        glkpLedger.EditValue = glkpLedger.Properties.GetKeyValue(0);
                        colLedgerCode.Visible = true;
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadCashBankLedger()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    glkpCashBankLedger.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpCashBankLedger.Properties.DataSource = resultArgs.DataSource.Table;
                        glkpCashBankLedger.Properties.DisplayMember = "LEDGER_NAME";
                        glkpCashBankLedger.Properties.ValueMember = "LEDGER_ID";
                        glkpCashBankLedger.EditValue = glkpCashBankLedger.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadCashBankLedger1()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.FetchCashBankLedger();
                    glkpLedger.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        glkpLedger.Properties.DataSource = resultArgs.DataSource.Table;
                        glkpLedger.Properties.DisplayMember = "LEDGER_NAME";
                        glkpLedger.Properties.ValueMember = "LEDGER_ID";
                        glkpLedger.EditValue = glkpLedger.Properties.GetKeyValue(0);

                        colLedgerCode.Visible = false;
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        public void LoadPurposeDetails()
        {
            try
            {
                using (PurposeSystem purposeSystem = new PurposeSystem())
                {
                    resultArgs = purposeSystem.FetchPurposeDetails();
                    this.UtilityMember.ComboSet.BindGridLookUpEdit(glkpPurpose, resultArgs.DataSource.Table, purposeSystem.AppSchema.Purposes.HEADColumn.Caption, purposeSystem.AppSchema.Purposes.CONTRIBUTION_HEAD_IDColumn.Caption);
                    glkpPurpose.EditValue = glkpPurpose.Properties.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadDonorDetails()
        {
            try
            {
                using (DonorAuditorSystem donaudSystem = new DonorAuditorSystem())
                {
                    resultArgs = donaudSystem.FetchDonorDetails();
                    this.UtilityMember.ComboSet.BindGridLookUpEdit(glkpDonor, resultArgs.DataSource.Table, donaudSystem.AppSchema.DonorAuditor.NAMEColumn.Caption, donaudSystem.AppSchema.DonorAuditor.DONAUD_IDColumn.Caption);
                    glkpDonor.EditValue = glkpDonor.Properties.GetKeyValue(0);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void LoadReceiptType()
        {
            DataTable dtReceipt = new DataTable();
            dtReceipt.Columns.Add(new DataColumn("Id", typeof(int)));
            dtReceipt.Columns.Add(new DataColumn("ReceiptType", typeof(string)));
            dtReceipt.Rows.Add(1, "First");
            dtReceipt.Rows.Add(2, "Subsequent");

            glkpReceiptType.Properties.DataSource = dtReceipt;
            glkpReceiptType.Properties.DisplayMember = "ReceiptType";
            glkpReceiptType.Properties.ValueMember = "Id";

            glkpReceiptType.EditValue = glkpReceiptType.Properties.GetKeyValue(0);


        }

        private void ShowHideDonor()
        {
            if (glkpProject.Text.Contains("Foreign"))
            {
                if (rboTransactionType.SelectedIndex == 0)
                {
                    HideShowBankDetails(rboTransactionType.SelectedIndex);
                    //this.Size = new Size(479, 384);
                    //this.CenterToParent();
                    //Receipts
                    EnableColumns();

                    lblReconciledOn.Text = "Reconciled On";
                    string Title = new frmTransaction().Text;
                    this.Text = Title + " " + "(Receipts)";
                }
                else if (rboTransactionType.SelectedIndex == 1)
                {

                    HideShowBankDetails(rboTransactionType.SelectedIndex);
                    //this.Size = new Size(479, 330);
                    //this.CenterToParent();
                    //Payments
                    lblReceiptAmount.Visibility = LayoutVisibility.Never;
                    esiExchangeRate.Visibility = LayoutVisibility.Never;
                    lblExchangeRate.Visibility = LayoutVisibility.Never;
                    esiActualAmount.Visibility = LayoutVisibility.Never;
                    lblReceiptActualAmount.Visibility = LayoutVisibility.Never;
                    esiCurrenySymbol.Visibility = LayoutVisibility.Never;
                    lblCurrencySymbol.Visibility = LayoutVisibility.Never;

                    lblPurpose.Visibility = LayoutVisibility.Always;
                    lblDonor.Visibility = LayoutVisibility.Always;
                    lblReceiptType.Visibility = LayoutVisibility.Always;

                    lblReconciledOn.Text = "Cleared On";

                    string Title = new frmTransaction().Text;
                    this.Text = Title + " " + "(Payments)";
                }
                else
                {
                    //this.Size = new Size(479, 280);
                    //this.CenterToParent();
                    //Contra
                    HideColumns();
                    string Title = new frmTransaction().Text;
                    lblReconciledOn.Visibility = LayoutVisibility.Never;
                    this.Text = Title + " " + "(Contra)";
                }
            }
            else
            {
                HideColumns();
            }
            LoadSelectedLedger();
        }

        private void EnableColumns()
        {
            lblDonor.Visibility = LayoutVisibility.Always;
            lblReceiptType.Visibility = LayoutVisibility.Always;
            lblPurpose.Visibility = LayoutVisibility.Always;
            lblReceiptAmount.Visibility = LayoutVisibility.Always;
            esiExchangeRate.Visibility = LayoutVisibility.Always;
            lblExchangeRate.Visibility = LayoutVisibility.Always;
            esiActualAmount.Visibility = LayoutVisibility.Always;
            lblReceiptActualAmount.Visibility = LayoutVisibility.Always;
            esiCurrenySymbol.Visibility = LayoutVisibility.Always;
            lblCurrencySymbol.Visibility = LayoutVisibility.Always;
        }

        private void HideColumns()
        {

            if (rboTransactionType.SelectedIndex == 0)
            {
                HideShowBankDetails(rboTransactionType.SelectedIndex);
                lblReconciledOn.Text = "Reconciled On";
                string Title = new frmTransaction().Text;
                this.Text = Title + " " + "(Receipts)";
            }
            else if (rboTransactionType.SelectedIndex == 1)
            {
                HideShowBankDetails(rboTransactionType.SelectedIndex);
                lblReconciledOn.Text = "Cleared On";

                string Title = new frmTransaction().Text;
                this.Text = Title + " " + "(Payments)";
            }
            else
            {
                HideShowBankDetails(rboTransactionType.SelectedIndex);
                lblReconciledOn.Visibility = LayoutVisibility.Never;
                string Title = new frmTransaction().Text;
                this.Text = Title + " " + "(Contra)";
            }
            lblDonor.Visibility = LayoutVisibility.Never;
            lblReceiptType.Visibility = LayoutVisibility.Never;
            lblPurpose.Visibility = LayoutVisibility.Never;
            lblReceiptAmount.Visibility = LayoutVisibility.Never;
            esiExchangeRate.Visibility = LayoutVisibility.Never;
            lblExchangeRate.Visibility = LayoutVisibility.Never;
            esiActualAmount.Visibility = LayoutVisibility.Never;
            lblReceiptActualAmount.Visibility = LayoutVisibility.Never;
            esiCurrenySymbol.Visibility = LayoutVisibility.Never;
            lblCurrencySymbol.Visibility = LayoutVisibility.Never;
        }

        private void ConvertCurrency()
        {
            Double ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtAmount.Text) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
            lblActualAmount.Text = ActualAmt.ToString();
        }

        private void LoadSelectedLedger()
        {
            if (rboTransactionType.SelectedIndex == 0 && SourceId == 0)
            {
                LoadLedger("1");
            }
            else if (rboTransactionType.SelectedIndex == 0 && SourceId == 1)
            {
                LoadCashBankLedger();
            }
            else if (rboTransactionType.SelectedIndex == 1 && SourceId == 0)
            {
                LoadCashBankLedger();
                LoadLedger("2");
            }
            else if (rboTransactionType.SelectedIndex == 1 && SourceId == 1)
            {
                LoadLedger("2");
            }
            else if (rboTransactionType.SelectedIndex == 2)
            {
                LoadCashBankLedger();
                LoadCashBankLedger1();
            }
        }

        private void LoadCostCenter()
        {
            using (CostCentreSystem costCenterSystem = new CostCentreSystem())
            {
                resultArgs = costCenterSystem.FetchCostCentreDetails();
                this.UtilityMember.ComboSet.BindGridLookUpEdit(glkpCostCentre, resultArgs.DataSource.Table, costCenterSystem.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.Caption, costCenterSystem.AppSchema.CostCentre.COST_CENTRE_IDColumn.Caption);
                glkpCostCentre.EditValue = glkpCostCentre.Properties.GetKeyValue(0);
            }
        }

        private void HideShowBankDetails(int SelectedIndex)
        {
            if (glkpProject.Text.Contains("Foreign"))
            {
                if (glkpCashBankLedger.Text.Contains("BankAccount"))
                {
                    lblChequeNo.Visibility = LayoutVisibility.Always;
                    emptySpaceItem3.Visibility = LayoutVisibility.Always;
                    lblReconciledOn.Visibility = LayoutVisibility.Always;
                    switch (SelectedIndex)
                    {
                        case 0:
                            this.Size = new Size(485, 386);
                            this.CenterToParent();
                            break;
                        case 1:
                            this.Size = new Size(485, 345);
                            this.CenterToParent();
                            break;
                        case 2:
                            lblReconciledOn.Visibility = LayoutVisibility.Never;
                            this.Size = new Size(485, 300);
                            this.CenterToParent();
                            break;
                    }
                }
                else
                {

                    lblChequeNo.Visibility = LayoutVisibility.Never;
                    emptySpaceItem3.Visibility = LayoutVisibility.Never;
                    lblReconciledOn.Visibility = LayoutVisibility.Never;

                    switch (SelectedIndex)
                    {
                        case 0:
                            this.Size = new Size(479, 366);
                            this.CenterToParent();
                            break;
                        case 1:
                            this.Size = new Size(479, 325);
                            this.CenterToParent();
                            break;
                        case 2:
                            lblReconciledOn.Visibility = LayoutVisibility.Never;
                            this.Size = new Size(479, 277);
                            this.CenterToParent();
                            break;
                    }
                    
                }
            }
            else
            {
                if (glkpCashBankLedger.Text.Contains("BankAccount"))
                {
                    lblChequeNo.Visibility = LayoutVisibility.Always;
                    emptySpaceItem3.Visibility = LayoutVisibility.Always;
                    lblReconciledOn.Visibility = LayoutVisibility.Always;
                    this.Size = new Size(479, 300);
                    this.CenterToParent();
                }
                else
                {

                    lblChequeNo.Visibility = LayoutVisibility.Never;
                    emptySpaceItem3.Visibility = LayoutVisibility.Never;
                    lblReconciledOn.Visibility = LayoutVisibility.Never;
                    this.Size = new Size(479, 280);
                    this.CenterToParent();
                }
            }
        }
        #endregion


    }
}