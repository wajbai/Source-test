using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using System.IO;
using Bosco.Utility;
using ACPP.Modules.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Globalization;
using AcMEDSync.Model;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraGrid.EditForm.Helpers;
using DevExpress.XtraGrid.Localization;
using DevExpress.Utils.Drawing;

namespace ACPP.Modules.Master
{
    public partial class frmBudgetUserDefined : frmFinanceBaseAdd
    {
        #region Variables
        private string DateFromY1 = string.Empty;
        private string DateToY1 = string.Empty;

        private string DateFromY2 = string.Empty;
        private string DateToY2 = string.Empty;

        #endregion

        #region Properties
        private DataTable dtUserDefinedBudgetBalances = new DataTable();
        private DataTable dtUserDefinedBudgetLedger = new DataTable();
        private DataTable dtUDIncomeBudgetLedger = new DataTable();
        private DataTable dtUDExpenseBudgetLedger = new DataTable();

        int projectid = 0;
        public int ProjectId 
        { 
            get{
             
                if (grdLkpProject.EditValue!=null)
                {
                    projectid = UtilityMember.NumberSet.ToInteger(grdLkpProject.EditValue.ToString());
                }
                return projectid;
            }
            set {
                projectid = value;
            }
        }
        #endregion

        #region Constructor
        public frmBudgetUserDefined()
        {
            InitializeComponent();
        }   
        #endregion

        #region Events
        private void frmBudgetUserDefined_Load(object sender, EventArgs e)
        {
            LoadDefaults(0);
        }
        
        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvIncomeLedger.PostEditor();
            gvIncomeLedger.UpdateCurrentRow();
            if (gvIncomeLedger.ActiveEditor == null)
            {
                gvIncomeLedger.ShowEditor();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                if (!IsNotValid())
                {
                    using (BudgetSystem budgetsystem = new BudgetSystem())
                    {
                        if (gcIncomeLedgers.DataSource != null && gcExpenseLedgers.DataSource != null)
                        {
                            this.ShowWaitDialog("Updating User defined Budget details");
                            DataTable dtIncomeLedgers = (gcIncomeLedgers.DataSource as DataTable).DefaultView.ToTable();
                            DataTable dtExpenseLedgers = (gcExpenseLedgers.DataSource as DataTable).DefaultView.ToTable();
                            DataTable dtOpeningBalance = GetBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.OpeningBalance);
                            DataTable dtClosingBalance = GetBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.ClosingBalance);
                            if (cbUserDefinedYear.SelectedIndex == 0)
                            {
                                result = budgetsystem.UpdateUserDefinedBudgetDetails(ProjectId, cbUserDefinedYear.SelectedIndex, DateFromY1, DateToY1, dtIncomeLedgers, dtExpenseLedgers, dtOpeningBalance, dtClosingBalance);
                            }
                            else if (cbUserDefinedYear.SelectedIndex == 1)
                            {

                                result = budgetsystem.UpdateUserDefinedBudgetDetails(ProjectId, cbUserDefinedYear.SelectedIndex, DateFromY2, DateToY2, dtIncomeLedgers, dtExpenseLedgers, dtOpeningBalance, dtClosingBalance);
                            }
                            this.CloseWaitDialog();
                            if (!result.Success)
                            {
                                MessageRender.ShowMessage(result.Message);
                            }
                            LoadDefaults(cbUserDefinedYear.SelectedIndex);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvIncomeLedger.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            gvExpenseLedger.OptionsView.ShowAutoFilterRow = chkFilter.Checked;
            if (chkFilter.Checked)
            {
                this.SetFocusRowFilter(gvExpenseLedger, colExpenseLedger);
                this.SetFocusRowFilter(gvIncomeLedger, colIncomeLedger);
            }
        }

              
        private void gcIncomeLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvIncomeLedger.FocusedColumn == colExpenseActual17_18 || gvIncomeLedger.FocusedColumn == colExpenseActual18_19))
            {
                gvIncomeLedger.PostEditor();
                gvIncomeLedger.UpdateCurrentRow();

                if (gvIncomeLedger.FocusedColumn == colExpenseActual18_19 && gvIncomeLedger.IsLastRow)
                {
                    FoucsExpenseLedgerGrid(0);
                }

            }
        }

        private void gcExpenseLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvExpenseLedger.FocusedColumn == colExpenseActual17_18 || gvExpenseLedger.FocusedColumn == colExpenseActual18_19))
            {
                gvExpenseLedger.PostEditor();
                gvExpenseLedger.UpdateCurrentRow();

                if (gvExpenseLedger.FocusedColumn == colExpenseActual18_19)
                {
                    if (gvExpenseLedger.IsLastRow)
                    {
                        btnSave.Select();
                        btnSave.Focus();
                    }
                    else
                    {
                        gvExpenseLedger.MoveNext();
                        gvExpenseLedger.FocusedColumn = colExpenseActual17_18;
                        e.Handled = true;
                    }
                }
            }
            else if (e.Shift && e.KeyCode == Keys.Tab && gvExpenseLedger.FocusedColumn == colExpenseActual17_18)
            {
                gvExpenseLedger.PostEditor();
                gvExpenseLedger.UpdateCurrentRow();
                if (gvExpenseLedger.IsFirstRow && e.Shift && e.KeyCode == Keys.Tab)
                {
                    DataTable dtRC = gcExpenseLedgers.DataSource as DataTable;
                    if (dtRC != null)
                    {
                        FoucsIncomeGrid(dtRC.Rows.Count - 1);
                        e.Handled = true;
                    }
                }
            }
        }

        private void frmAddAnnualBudget_Shown(object sender, EventArgs e)
        {
            gvIncomeLedger.PostEditor();
            gvIncomeLedger.UpdateCurrentRow();
            if (gvIncomeLedger.ActiveEditor == null)
            {
                gvIncomeLedger.ShowEditor();
                FoucsIncomeGrid(0);                
            }
        }
        #endregion

        #region Methods
        private void LoadDefaults(int selectindex)
        {
            DateFromY1 = UtilityMember.DateSet.ToDate("01/04/2017", false).ToShortDateString();
            DateToY1 = UtilityMember.DateSet.ToDate("31/03/2018", false).ToShortDateString();

            DateFromY2 = UtilityMember.DateSet.ToDate("01/04/2018", false).ToShortDateString();
            DateToY2 = UtilityMember.DateSet.ToDate("31/03/2019", false).ToShortDateString();

            cbUserDefinedYear.Properties.Items.Clear();
            cbUserDefinedYear.Properties.Items.Add("2017-18");
            cbUserDefinedYear.Properties.Items.Add("2018-19");
            LoadProjectDetails();
            cbUserDefinedYear.SelectedIndex = selectindex;
        }

        private void LoadProjectDetails()
        {
            try
            {
                using (BudgetSystem budgetSystem = new BudgetSystem())
                {
                    ResultArgs resultArgs = budgetSystem.FetchBudgetProjectsLookup();
                    grdLkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(grdLkpProject, dtProject, budgetSystem.AppSchema.Project.PROJECTColumn.ColumnName, budgetSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        if (dtProject.Rows.Count > 0)
                        {
                            if (ProjectId == 0)
                            {
                                grdLkpProject.EditValue = grdLkpProject.Properties.GetKeyValue(0);
                            }
                            else
                            {
                                grdLkpProject.EditValue = ProjectId;
                            }
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
        
        private void AssingCaptions()
        {
            this.Text = "User Defined Budget Details  - " + cbUserDefinedYear.Text;
            int cbUserDefinedYearSelectedIndex = cbUserDefinedYear.SelectedIndex;
            colIncomeActualAmount.Caption = "Actual Amount - " + cbUserDefinedYear.Text;
            colExpenseActualAmount.Caption = "Actual Amount - " + cbUserDefinedYear.Text;

            lblOPCaption.Text = "Opening Balance as on ";
            lblCLCaption.Text = "Opening Balance as on ";

            if (cbUserDefinedYearSelectedIndex == 0)
            {
                lblOPCaption.Text = "Opening Balance as on " + DateFromY1;
                lblCLCaption.Text = "Closing Balance as on " + DateToY1;
            }
            else if (cbUserDefinedYearSelectedIndex == 1)
            {
                lblOPCaption.Text = "Opening Balance as on " + DateFromY2;
                lblCLCaption.Text = "Closing Balance as on " + DateToY2;
            }
        }

        private void BindUserDefinedBudgets()
        {
            string datefrom = DateFromY1;
            string dateto = DateToY1;
            string fldacualamount = "ACTUAL_AMOUNT";
            
            if (cbUserDefinedYear.SelectedIndex == 1)
            {
                datefrom = DateFromY2;
                dateto = DateToY2;
                fldacualamount = "ACTUAL_AMOUNT";
            }

            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                ResultArgs resultarg = budgetsystem.FetchUserDefinedBudgetDetails(ProjectId, datefrom, dateto, TransactionMode.CR);
                if (resultarg.Success && resultarg.DataSource.Table != null)
                {
                    dtUDIncomeBudgetLedger = resultarg.DataSource.Table;
                    dtUDIncomeBudgetLedger.DefaultView.Sort = "LEDGER_CODE, LEDGER_GROUP";
                    colIncomeActualAmount.FieldName = fldacualamount;
                    gcIncomeLedgers.DataSource = dtUDIncomeBudgetLedger;
                }

                resultarg = budgetsystem.FetchUserDefinedBudgetDetails(ProjectId, datefrom, dateto, TransactionMode.DR);
                if (resultarg.Success && resultarg.DataSource.Table != null)
                {
                    dtUDExpenseBudgetLedger= resultarg.DataSource.Table;
                    dtUDExpenseBudgetLedger.DefaultView.Sort = "LEDGER_CODE, LEDGER_GROUP";
                    colExpenseActualAmount.FieldName = fldacualamount;
                    gcExpenseLedgers.DataSource = dtUDExpenseBudgetLedger;
                }

                resultarg = budgetsystem.FetchUserDefinedBalances(ProjectId, DateFromY1, DateToY1, DateFromY2, DateToY2);
                if (resultarg.Success && resultarg.DataSource.Table != null)
                {
                    dtUserDefinedBudgetBalances = resultarg.DataSource.Table;
                }
            }

            if (dtUserDefinedBudgetLedger.Rows.Count > 0)
            {
                dtUserDefinedBudgetLedger.DefaultView.RowFilter = string.Empty;
                //string filter = "NATURE_ID= 1 OR (NATURE_ID IN (3, 4) AND (TRANS_MODE = 'CR' OR TRANS_MODE IS NULL))"; //AND TRANS_MODE='CR' OR TRANS_MODE IS NULL
                string filter = "(NATURE_ID IN (1, 3, 4))"; //AND TRANS_MODE='CR' OR TRANS_MODE IS NULL
                dtUserDefinedBudgetLedger.DefaultView.RowFilter = filter;
                dtUserDefinedBudgetLedger.DefaultView.Sort = "LEDGER_CODE, LEDGER_GROUP";
                dtUDIncomeBudgetLedger = dtUserDefinedBudgetLedger.DefaultView.ToTable();
                ResetAssetLiabilityLedgers(dtUDIncomeBudgetLedger, TransactionMode.DR);
                colIncomeActualAmount.FieldName = fldacualamount;
                gcIncomeLedgers.DataSource = dtUDIncomeBudgetLedger;
                
                dtUserDefinedBudgetLedger.DefaultView.RowFilter = string.Empty;
                //filter = "NATURE_ID= 2 OR (NATURE_ID IN (3, 4) AND (TRANS_MODE = 'DR' OR TRANS_MODE IS NULL))"; // AND TRANS_MODE='DR' OR TRANS_MODE IS NULL
                filter = "(NATURE_ID IN (2, 3, 4))"; //AND TRANS_MODE='CR' OR TRANS_MODE IS NULL
                dtUserDefinedBudgetLedger.DefaultView.RowFilter = filter;
                dtUserDefinedBudgetLedger.DefaultView.Sort = "LEDGER_CODE, LEDGER_GROUP";
                dtUDExpenseBudgetLedger = dtUserDefinedBudgetLedger.DefaultView.ToTable();
                ResetAssetLiabilityLedgers(dtUDExpenseBudgetLedger, TransactionMode.CR);
                colExpenseActualAmount.FieldName = fldacualamount;
                gcExpenseLedgers.DataSource = dtUDExpenseBudgetLedger;
            }

            AssingCaptions();
            //Opening Balance
            double dAmouont = GetFixedLedgerGroupBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.OpeningBalance, FixedLedgerGroup.Cash);
            txtOPBalanceCash.Text =null;
            if (dAmouont > 0)
            {
                txtOPBalanceCash.Text = dAmouont.ToString();
            }
            
            dAmouont = GetFixedLedgerGroupBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.OpeningBalance, FixedLedgerGroup.BankAccounts);
            txtBankOpBalanceAmount.Text = null;
            if (dAmouont > 0)
            {
                txtBankOpBalanceAmount.Text = dAmouont.ToString();
            }

            dAmouont = GetFixedLedgerGroupBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.OpeningBalance, FixedLedgerGroup.FixedDeposit);
            txtOpBalanceFD.Text = null;
            if (dAmouont > 0)
            {
                txtOpBalanceFD.Text = dAmouont.ToString();
            }

            //Closing Balance
            dAmouont = GetFixedLedgerGroupBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.ClosingBalance, FixedLedgerGroup.Cash);
            txtCLBalanceCash.Text = null;
            if (dAmouont > 0)
            {
                txtCLBalanceCash.Text = dAmouont.ToString();
            }

            dAmouont = GetFixedLedgerGroupBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.ClosingBalance, FixedLedgerGroup.BankAccounts);
            txtCLBalanceBank.Text = null;
            if (dAmouont > 0)
            {
                txtCLBalanceBank.Text = dAmouont.ToString();
            }

            dAmouont = GetFixedLedgerGroupBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType.ClosingBalance, FixedLedgerGroup.FixedDeposit);
            txtCLBalanceFD.Text = null;
            if (dAmouont > 0)
            {
                txtCLBalanceFD.Text = dAmouont.ToString();
            }
            dtUserDefinedBudgetLedger.DefaultView.RowFilter = string.Empty;
            
            txtOPBalanceCash.Select();
            txtOPBalanceCash.Focus();
        }
               
        /// <summary>
        /// Check input valid data
        /// </summary>
        /// <returns></returns>
        private bool IsNotValid()
        {
            bool rtn = false;
            DataTable dtIncomeLedgers = (gcIncomeLedgers.DataSource as DataTable).DefaultView.ToTable();
            DataTable dtExpenseLedgers = (gcExpenseLedgers.DataSource as DataTable).DefaultView.ToTable();
            string column = (cbUserDefinedYear.SelectedIndex == 0 ? "ACTUAL_AMOUNT" : "ACTUAL_AMOUNT");            
            dtIncomeLedgers.DefaultView.RowFilter = column + ">0";
            dtExpenseLedgers.DefaultView.RowFilter = column + ">0";

            if (!rtn && ProjectId == 0) //Check Proejcts
            {
                rtn = true;
                MessageRender.ShowMessage("Select Project");
                grdLkpProject.Select();
                grdLkpProject.Focus();
            }
            //There is no actual amount, alert
            else if (dtIncomeLedgers.DefaultView.Count == 0 && dtExpenseLedgers.DefaultView.Count == 0)
            {
                rtn = true;
                MessageRender.ShowMessage("Fill Actual Amount for any one of the Ledgers");   
            }

            
            dtIncomeLedgers.DefaultView.RowFilter = string.Empty;
            dtExpenseLedgers.DefaultView.RowFilter = string.Empty;
            return rtn;
        }

        /// <summary>
        /// For income grid show Asset and Liabbitity Ledgers values if trans mode = Dr
        /// For expense grid show Asset and Liabbitity Ledgers values if trans mode = cr
        /// </summary>
        /// <returns></returns>
        private void ResetAssetLiabilityLedgers(DataTable dt, TransactionMode resettransmode)
        {
            dt.DefaultView.RowFilter = "NATURE_ID IN (3, 4) AND TRANS_MODE= '" + resettransmode.ToString() + "' AND ACTUAL_AMOUNT >0";
            foreach (DataRowView drv in dt.DefaultView)
            {
                drv.BeginEdit();
                drv["ACTUAL_AMOUNT"] = 0;
                drv.EndEdit();
            }
            dt.DefaultView.RowFilter = string.Empty;
        }

        /// <summary>
        /// Focus to income ledger grid
        /// </summary>
        /// <param name="RowIndex"></param>
        private void FoucsIncomeGrid(int RowIndex)
        {
            if (gvIncomeLedger.RowCount > 0)
            {
                gvIncomeLedger.Focus();
                gvIncomeLedger.FocusedRowHandle = RowIndex;
                gvIncomeLedger.ShowEditor();
            }
        }

        /// <summary>
        /// Focus to non recurring grid
        /// </summary>
        /// <param name="RowIndex"></param>
        private void FoucsExpenseLedgerGrid(int RowIndex)
        {
            if (gvExpenseLedger.RowCount > 0)
            {
                gvExpenseLedger.Focus();
                gvExpenseLedger.FocusedRowHandle = RowIndex;
                gvExpenseLedger.ShowEditor();
            }
        }

        private DataTable GetBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType balanceType)
        {
            DataTable dtBalance = new DataTable();

            if (!dtBalance.Columns.Contains("GROUP_ID")) dtBalance.Columns.Add(new DataColumn("GROUP_ID", typeof(Int32)));
            if (!dtBalance.Columns.Contains("LEDGER_NAME")) dtBalance.Columns.Add(new DataColumn("LEDGER_NAME", typeof(string)));
            if (!dtBalance.Columns.Contains("AMOUNT")) dtBalance.Columns.Add(new DataColumn("AMOUNT", typeof(double)));

            double dOPCashBalance = this.UtilityMember.NumberSet.ToDouble(txtOPBalanceCash.Text);
            double dOPBankBalance = this.UtilityMember.NumberSet.ToDouble(txtBankOpBalanceAmount.Text);
            double dOPFDBalance = this.UtilityMember.NumberSet.ToDouble(txtOpBalanceFD.Text);

            double dCLCashBalance = this.UtilityMember.NumberSet.ToDouble(txtCLBalanceCash.Text);
            double dCLBankBalance = this.UtilityMember.NumberSet.ToDouble(txtCLBalanceBank.Text);
            double dCLFDBalance = this.UtilityMember.NumberSet.ToDouble(txtCLBalanceFD.Text);

            //For Opening and Closing Balance
            if (balanceType ==Bosco.Report.Base.BalanceSystem1.BalanceType.OpeningBalance)
            {
                dtBalance.Rows.Add(new object[] { (int)FixedLedgerGroup.Cash, "Cash in Hand", dOPCashBalance });
                dtBalance.Rows.Add(new object[] { (int)FixedLedgerGroup.BankAccounts, "Cash at Bank", dOPBankBalance });
                dtBalance.Rows.Add(new object[] { (int)FixedLedgerGroup.FixedDeposit,"FD/RD/Post Office Savings/ Flexi", dOPFDBalance});
            }
            else if (balanceType == Bosco.Report.Base.BalanceSystem1.BalanceType.ClosingBalance)
            {
                dtBalance.Rows.Add(new object[] { (int)FixedLedgerGroup.Cash, "Cash in Hand", dCLCashBalance });
                dtBalance.Rows.Add(new object[] { (int)FixedLedgerGroup.BankAccounts, "Cash at Bank", dCLBankBalance });
                dtBalance.Rows.Add(new object[] { (int)FixedLedgerGroup.FixedDeposit, "FD/RD/Post Office Savings/ Flexi", dCLFDBalance });
            }
            return dtBalance;
        }

        private double GetFixedLedgerGroupBalancesLedgers(Bosco.Report.Base.BalanceSystem1.BalanceType balanceType, FixedLedgerGroup group)
        {
            Double rtn = 0;
            if (dtUserDefinedBudgetBalances.Rows.Count>0)
            {
                string datefrom = DateFromY1;
                string dateto = DateToY1;
                if (cbUserDefinedYear.SelectedIndex == 1)
                {
                    datefrom = DateFromY2;
                    dateto = DateToY2;
                }
                string transmode = (balanceType==Bosco.Report.Base.BalanceSystem1.BalanceType.OpeningBalance?"CR":"DR");
                Int32 groupid = (int)group;

                //For Opening Balance
                dtUserDefinedBudgetBalances.DefaultView.RowFilter = "DATE_FROM='" + datefrom + "' AND DATE_TO='" + dateto + "' AND TRANS_MODE='" + transmode + "' AND GROUP_ID =" + groupid;
                if (dtUserDefinedBudgetBalances.DefaultView.Count == 1)
                {
                    rtn = UtilityMember.NumberSet.ToDouble(dtUserDefinedBudgetBalances.DefaultView[0]["ACTUAL_AMOUNT"].ToString());
                }
            }
            return rtn;
        }

        #endregion

        private void cbUserDefinedYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindUserDefinedBudgets();
        }

        private void gcIncomeLedgers_Click(object sender, EventArgs e)
        {

        }

        private void txtOPBalanceCash_Enter(object sender, EventArgs e)
        {
            //TextEdit edit = sender as TextEdit;
            //edit.SelectionStart = 0;
            //edit.SelectionLength = edit.Text.Length
            //if (edit.SelectionStart == 0 && edit.SelectionLength == edit.Text.Length)
            //    edit.Select(0, edit.Text.Length);  
        }

        private void grdLkpProject_EditValueChanged(object sender, EventArgs e)
        {
            BindUserDefinedBudgets();
        }

        
    }
}

