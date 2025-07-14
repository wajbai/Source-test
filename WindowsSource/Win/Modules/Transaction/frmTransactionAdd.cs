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
    public partial class frmTransactionAdd : frmBaseAdd
    {
        ResultArgs resultArgs = null;

        #region Properties
        private int sourceId = 0;
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        #endregion

        public frmTransactionAdd()
        {
            InitializeComponent();
        }
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
                    rglkpLedger.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        rglkpLedger.DataSource = resultArgs.DataSource.Table;
                        rglkpLedger.DisplayMember = "LEDGER_NAME";
                        rglkpLedger.ValueMember = "LEDGER_ID";
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
                    rglkpLedger.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        rglkpLedger.DataSource = resultArgs.DataSource.Table;
                        rglkpLedger.DisplayMember = "LEDGER_NAME";
                        rglkpLedger.ValueMember = "LEDGER_ID";
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


        private void frmTransactionAdd_Load(object sender, EventArgs e)
        {
            //LoadLedger("1");
            LoadProject();
            ConstructGridTable(false);
            ShowHideDonor();
            LoadProject();
            LoadDonorDetails();
            LoadPurposeDetails();
            LoadSource();
            LoadCashFlag();
            SetSourceDefaultValue();
            LoadReceiptType();
            dteTransactionDate.Text = this.UtilityMember.DateSet.GetDateToday();
        }

        private void ConstructGridTable(bool IsHideColumns)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SOURCE", typeof(string));
            dt.Columns.Add("FLAG", typeof(string));
            dt.Columns.Add("LEDGER_NAME", typeof(string));
            dt.Columns.Add("CHEQUE", typeof(string));
            dt.Columns.Add("RECONCILED_ON", typeof(string));
            dt.Columns.Add("AMOUNT", typeof(string));
            DataRow dr = dt.NewRow();
            DataRow dr1 = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows.Add(dr1);
            gcTransaction.DataSource = dt;
        }

        private void HideGridColumns(bool IsHideColumns)
        {
            if (!IsHideColumns)
            {
                colCheque.Visible = false;
                colReconciled.Visible = false;
            }
            else
            {
                colCheque.Visible = true;
                colReconciled.Visible = true;
            }
        }


        private void SetSourceDefaultValue()
        {
            rglkpSource.NullText = rglkpSource.GetDisplayValue((int)Source.By).ToString();
            rglkpFlag.NullText = rglkpFlag.GetDisplayValue((int)CashFlag.Cash).ToString();
        }

        private void ShowHideDonor()
        {
            if (glkpProject.Text.Contains("Foreign"))
            {
                if (rgTransactionType.SelectedIndex == 0)
                {
                    //Receipts
                    EnableColumns();
                    string Title = new frmTransactionAdd().Text;
                    this.Text = Title + " - " + "Receipts";
                    //rglkpSource.NullText = rglkpSource.GetDisplayValue((int)Source.From).ToString();
                    //rglkpFlag.NullText = rglkpFlag.GetDisplayValue((int)CashFlag.Cash).ToString();

                }
                else if (rgTransactionType.SelectedIndex == 1)
                {
                    //Payments
                    lblAmount.Visibility = LayoutVisibility.Never;
                    lblActualAmount.Visibility = LayoutVisibility.Never;
                    lblActualAmt.Visibility = LayoutVisibility.Never;
                    lblExchangeRate.Visibility = LayoutVisibility.Never;
                    lblSymbol.Visibility = LayoutVisibility.Never;
                    lblCurrencySymbol.Visibility = LayoutVisibility.Never;
                    lblPurpose.Visibility = LayoutVisibility.Always;
                    lblDonor.Visibility = LayoutVisibility.Always;
                    lblReceiptType.Visibility = LayoutVisibility.Always;
                    esiDonor.Visibility = LayoutVisibility.Always;
                    esiPurpose.Visibility = LayoutVisibility.Always;
                    //rglkpSource.NullText = rglkpSource.GetDisplayValue((int)Source.To).ToString();
                    //rglkpFlag.NullText = rglkpFlag.GetDisplayValue((int)CashSource.From).ToString();
                    string Title = new frmTransactionAdd().Text;
                    this.Text = Title + " - " + "Payments";
                }
                else
                {
                    //Contra
                    HideColumns();
                    string Title = new frmTransactionAdd().Text;
                    this.Text = Title + " - " + "Contra";
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
            lblAmount.Visibility = LayoutVisibility.Always;
            lblActualAmount.Visibility = LayoutVisibility.Always;
            lblActualAmt.Visibility = LayoutVisibility.Always;
            lblExchangeRate.Visibility = LayoutVisibility.Always;
            lblSymbol.Visibility = LayoutVisibility.Always;
            lblCurrencySymbol.Visibility = LayoutVisibility.Always;
            lblPurpose.Visibility = LayoutVisibility.Always;
            lblDonor.Visibility = LayoutVisibility.Always;
            lblReceiptType.Visibility = LayoutVisibility.Always;
            esiDonor.Visibility = LayoutVisibility.Always;
            esiPurpose.Visibility = LayoutVisibility.Always;
        }

        private void HideColumns()
        {
            lblAmount.Visibility = LayoutVisibility.Never;
            lblActualAmount.Visibility = LayoutVisibility.Never;
            lblActualAmt.Visibility = LayoutVisibility.Never;
            lblExchangeRate.Visibility = LayoutVisibility.Never;
            lblSymbol.Visibility = LayoutVisibility.Never;
            lblCurrencySymbol.Visibility = LayoutVisibility.Never;
            lblPurpose.Visibility = LayoutVisibility.Never;
            lblDonor.Visibility = LayoutVisibility.Never;
            lblReceiptType.Visibility = LayoutVisibility.Never;
            esiDonor.Visibility = LayoutVisibility.Never;
            esiPurpose.Visibility = LayoutVisibility.Never;
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            ShowHideDonor();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideDonor();
            //SetSourceDefaultValue();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            ConvertCurrency();
        }

        private void txtExchangeRate_TextChanged(object sender, EventArgs e)
        {
            ConvertCurrency();
        }

        private void ConvertCurrency()
        {
            Double ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtAmount.Text) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
            lblActualAmt.Text = ActualAmt.ToString();
        }

        private void gvTransaction_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void rcboFlag_EditValueChanged(object sender, EventArgs e)
        {
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

        private void LoadSource()
        {
            Source source = new Source();
            DataView dvSource = this.UtilityMember.EnumSet.GetEnumDataSource(source, Sorting.Ascending);
            rglkpSource.DataSource = dvSource.Table;
            rglkpSource.DisplayMember = "Name";
            rglkpSource.ValueMember = "Id";
        }

        private void LoadCashFlag()
        {
            CashFlag cashflag = new CashFlag();
            DataView dvSource = this.UtilityMember.EnumSet.GetEnumDataSource(cashflag, Sorting.Ascending);
            rglkpFlag.DataSource = dvSource.Table;
            rglkpFlag.DisplayMember = "Name";
            rglkpFlag.ValueMember = "Id";
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

        private void rglkpFlag_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit grdFlag = new GridLookUpEdit();
            grdFlag = (GridLookUpEdit)sender;
            if (grdFlag.EditValue.ToString() == "0")
            {
                HideGridColumns(false);
            }
            else
            {
                HideGridColumns(true);
            }
        }

        private void rglkpSource_EditValueChanged(object sender, EventArgs e)
        {
            //GridLookUpEdit grdSource = new GridLookUpEdit();
            //grdSource = (GridLookUpEdit)sender;
            //SourceId = this.UtilityMember.NumberSet.ToInteger(grdSource.EditValue.ToString());
            //  LoadSelectedLedger();
        }
        private void LoadSelectedLedger()
        {
            if (rgTransactionType.SelectedIndex == 0 && SourceId == 0)
            {
                LoadLedger("1");
            }
            else if (rgTransactionType.SelectedIndex == 0 && SourceId == 1)
            {
                LoadCashBankLedger();
            }
            else if (rgTransactionType.SelectedIndex == 1 && SourceId == 0)
            {
                LoadCashBankLedger();
            }
            else if (rgTransactionType.SelectedIndex == 1 && SourceId == 1)
            {
                LoadLedger("2");
            }
            else if (rgTransactionType.SelectedIndex == 2)
            {
                LoadCashBankLedger();
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
    }
}