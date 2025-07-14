using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Transaction;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Transaction
{
    public partial class frmDenomination : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        private int VoucherId = 0;
        private double ContraAmount = 0;
        private int LedgerId = 0;
        private string BankLedgerName = string.Empty;
        private int RowIndex = 0;
        public DataTable dtValues = new DataTable();
        #endregion

        #region Properties

        public DataTable dtDenomination
        {
            set { dtValues = value; }
            get { return dtValues; }

        }
        private double AmountSummaryVal
        {
            get { return colAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) : 0; }
        }

        private double Denomination
        {
            get
            {
                return gvDenomination.GetFocusedRowCellValue(colDenomination) != null ? this.UtilityMember.NumberSet.ToDouble(gvDenomination.GetFocusedRowCellValue(colDenomination).ToString()) : 0;
            }
        }

        private int count = 0;
        public int Count
        {
            get
            {
                RowIndex = gvDenomination.FocusedRowHandle;
                count = gvDenomination.GetFocusedRowCellValue(colCount) != null ? this.UtilityMember.NumberSet.ToInteger(gvDenomination.GetFocusedRowCellValue(colCount).ToString()) : 0;
                return count;
            }
            set
            {
                count = value;
            }
        }

        private DataView dvEditDenomination = null;
        private DataView DvEditDenomination
        {
            set { dvEditDenomination = value; }
            get { return dvEditDenomination; }
        }
        #endregion

        #region Constructor
        public frmDenomination(int Voucherid, double amount, string BankLedgername, int Ledgerid, DataView dvDenomination)
            : this()
        {
            VoucherId = Voucherid;
            ContraAmount = amount;
            LedgerId = Ledgerid;
            BankLedgerName = BankLedgername;
            DvEditDenomination = dvDenomination;
            RealColumnEditTransAmount();
        }
        public frmDenomination()
        {
            InitializeComponent();
        }
        #endregion

        #region Event
        private void frmDenomination_Load(object sender, EventArgs e)
        {
            LoadConstructor();
            LoadDenomination();
            SetDefault();
            AssignProperties();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDenominationValidation())
                {
                    using (DenominationSystem denominationSystem = new DenominationSystem())
                    {
                        gvDenomination.UpdateCurrentRow();
                        denominationSystem.VoucherID = VoucherId;
                        denominationSystem.DenominationLedgerID = LedgerId;
                        DataTable dtSource = gcDenomination.DataSource as DataTable;
                        DataTable dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        denominationSystem.dtDenomination = dtFilteredRows;
                        if (dtSource != null && dtSource.Rows.Count > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                            dtDenomination = dtSource;
                            dtDenomination.TableName = LedgerId.ToString();
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtxtAmount_Leave(object sender, EventArgs e)
        {
        }
        #endregion

        #region Method

        private void LoadDenomination()
        {
            using (DenominationSystem denominationSystem = new DenominationSystem())
            {
                denominationSystem.VoucherID = VoucherId;
                denominationSystem.DenominationLedgerID = LedgerId;
                resultArgs = denominationSystem.FetchDenominationByID();
                gcDenomination.DataSource = resultArgs.DataSource.Table;
                gcDenomination.RefreshDataSource();
                {

                }
            }
        }

        private void SetDefault()
        {
            lblAmount.Text = this.UtilityMember.NumberSet.ToCurrency(ContraAmount);
            lblBankName.Text = BankLedgerName;
        }

        private void LoadConstructor()
        {
            DataTable dtDenomination = new DataTable();
            dtDenomination.Columns.Add("DENOMINATION_ID", typeof(int));
            dtDenomination.Columns.Add("DENOMINATION", typeof(double));
            dtDenomination.Columns.Add("MULTIPLE", typeof(string));
            dtDenomination.Columns.Add("AMOUNT", typeof(double));
            gcDenomination.DataSource = dtDenomination;
        }

        private void RealColumnEditTransAmount()
        {
            colCount.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnAmount_EditValueChanged);
            this.gvDenomination.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvDenomination.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colCount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvDenomination.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            gvDenomination.ShowEditor();
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvDenomination.PostEditor();
            gvDenomination.UpdateCurrentRow();
            if (gvDenomination.ActiveEditor == null)
            {
                gvDenomination.ShowEditor();
            }
            else
            {
                double TotalAmount = Denomination * Count;
                gvDenomination.SetFocusedRowCellValue(colAmount, TotalAmount);
            }
        }



        private bool IsDenominationValidation()
        {
            bool isValid = true;
            if (AmountSummaryVal > this.UtilityMember.NumberSet.ToDouble(ContraAmount.ToString()))
            {
                //ShowMessageBox("Total Amount cannot be greater than the Allocated Amount!");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.DENOMINATION_TOT_AMT_GRATERTHAN_ALLOT_AMT));
                isValid = false;
            }
            else if (AmountSummaryVal < this.UtilityMember.NumberSet.ToDouble(ContraAmount.ToString()))
            {
                //ShowMessageBox("Total Amount cannot be Lesser than the Allocated Amount!");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Transaction.DENOMINATION_TOT_AMT_LESSERHTAN_ALLOT_AMT));
                isValid = false;
            }
            return isValid;
        }

        private void AssignProperties()
        {
            try
            {
                if (DvEditDenomination != null && DvEditDenomination.Count > 0)
                {
                    gcDenomination.DataSource = DvEditDenomination.ToTable();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
        }

        #endregion

        private void gcDenomination_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (ContraAmount == AmountSummaryVal)
            {
                btnOK.Focus();
            }
        }
    }
}