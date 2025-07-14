using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraLayout.Utils;
using Bosco.Utility;
using Bosco.Model.UIModel;
using ACPP.Modules.Master;
using Bosco.Model.Transaction;
using ACPP.Modules;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;

namespace ACPP.Modules.Master
{
    public partial class frmSubCreationsSelectionLedger : frmFinanceBaseAdd
    {

        #region Event Handlers
        public event EventHandler UpdateHeld;
        #endregion

        #region Decelaration
        ResultArgs resultArgs = null;
        private int BudgetId = 0;
        private int ProjectId = 0;
        private int subLedgerId = 0;
        private int BudgetGroup = 0;
        private DataTable dtAllotedBudgetDetails = new DataTable();
        public string CheckSelected = "SELECT";
        public string TempSelect = "SELECT_TMP";

        #endregion

        #region Constructor
        public frmSubCreationsSelectionLedger()
        {
            InitializeComponent();
        }

        public frmSubCreationsSelectionLedger(int projectId, int budgetId, int budgetGroupType, DataTable dtallotedBudgetLedgers)
            : this()
        {
            this.Text = "Select Budget Ledgers - " + (budgetGroupType == 0 ? "(Recuring)" : "Non Recuring");
            this.BudgetId = budgetId;
            this.ProjectId = projectId;
            this.BudgetGroup = budgetGroupType;
            this.dtAllotedBudgetDetails = dtallotedBudgetLedgers;
        }
        #endregion

        #region Events
        private void frmSubCreationsSelectionLedger_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        /// <summary>
        /// To load all the mapped budget ledgers and based on Budget Group (Recuring /Non Recuring)
        /// </summary>
        private void loadBudgetLedger()
        {

        }

        /// <summary>
        /// List out all the un alloted unused budget Ledgers based on Budget Group (Recuring /Non Recuring)
        /// </summary>
        private void BindAvailableLedgers()
        {

        }

        /// <summary>
        /// 1. check sub ledger exist or not if exist get the its Id's
        /// 2. if not exist create new sub ledger and gets it id's
        /// 3. check selected ledgerid and subledgerid in ledger mapped sub ledger table if already exist alert message
        /// 4. if not availble, if not mapped to selected ledger and sub ledger kindly map it
        /// 5. refresh save details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLedgerSelectionDetails())
                {

                    using (SubLedgerSystem subLedger = new SubLedgerSystem())
                    {
                        subLedger.SubLedgerName = txtsubLedger.Text.Trim();
                        int isExistSubLedgerId = subLedger.isExistSubLedgerDetails();
                        if (isExistSubLedgerId == 0)
                        {

                            subLedger.SubLedgerId = subLedgerId == 0 ? (int)AddNewRow.NewRow : subLedgerId;
                            subLedger.SubLedgerName = txtsubLedger.Text.Trim();
                            //resultArgs = subLedger.SaveSubLedgerDetails();
                            subLedgerId = this.UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        }
                        else
                        {
                            subLedgerId = isExistSubLedgerId;
                        }
                        if (resultArgs.Success)
                        {
                            subLedger.SubLedgerId = subLedgerId;
                            subLedger.LedgerId = this.UtilityMember.NumberSet.ToInteger(glkpBudgetLedger.EditValue.ToString());
                            //resultArgs = subLedger.MapLedgerwithSubLedger();

                            //Show newly added Sub ledger in Avaialbe Grid
                            DataRow drNewSubLedger = dtAllotedBudgetDetails.NewRow();

                            drNewSubLedger["LEDGER_ID"] = subLedger.LedgerId;
                            drNewSubLedger["SUB_LEDGER_ID"] = subLedger.SubLedgerId;
                            drNewSubLedger["IS_ALLOTED"] = -1;

                            dtAllotedBudgetDetails.Rows.Add(drNewSubLedger);

                            foreach (DataRow drRow in dtAllotedBudgetDetails.Rows)
                            {
                                if (this.UtilityMember.NumberSet.ToInteger(drRow["SUB_LEDGER_ID"].ToString()) == subLedger.SubLedgerId && this.UtilityMember.NumberSet.ToInteger(drRow["LEDGER_ID"].ToString()) == subLedger.LedgerId)
                                {
                                    drRow["GROUP_ID"] = 0;
                                    drRow["NATURE_ID"] = 0;
                                    drRow["NATURE"] = string.Empty;
                                    drRow["MAIN_LEDGER_NAME"] = string.Empty;
                                    drRow["LEDGER_NAME"] = subLedger.SubLedgerName;
                                    drRow["PROPOSED_CURRENT_YR"] = 0.00;
                                    drRow["APPROVED_CURRENT_YR"] = 0.00;
                                    drRow["APPROVED_PREVIOUS_YR"] = 0.00;
                                    drRow["ACTUAL"] = 0.00;
                                    drRow["NARRATION"] = string.Empty;
                                    drRow["HO_NARRATION"] = string.Empty;
                                }

                            }

                            LoadLedgerProject();
                        }

                        SetTitle();
                        SetDefaults();
                        ClearControls();
                        if (UpdateHeld != null)
                        {
                            UpdateHeld(this, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Clear controls
        /// </summary>
        private void ClearControls()
        {
            txtsubLedger.Text = string.Empty;
        }

        private void txtsubLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtsubLedger);
            txtsubLedger.Text = this.UtilityMember.StringSet.ToSentenceCase(txtsubLedger.Text);
        }

        /// <summary>
        /// Close the form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 1. Get Selected Available Ledgers
        /// 2. Add those ledgers to alloted budget if not available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dtAllotedBudgetDetails != null && dtAllotedBudgetDetails.Rows.Count > 0)
            {
                DataTable dt = gcSubLedgerProject.DataSource as DataTable;
                DataView dvSelectedFiltered = new DataView(dt);
                dvSelectedFiltered.RowFilter = "SELECT=1";
                dt = dvSelectedFiltered.ToTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Int32 ledgerid = AppSetting.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                        Int32 subledgerid = AppSetting.NumberSet.ToInteger(dr["SUB_LEDGER_ID"].ToString());

                        //dtAllotedBudgetDetails.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND SUB_LEDGER_ID=" + subledgerid;
                        dtAllotedBudgetDetails.DefaultView.RowFilter = "LEDGER_ID = " + ledgerid + " AND IS_SUB_LEDGER=0"; //Allow add Main Ledger alone
                        if (dtAllotedBudgetDetails.DefaultView.Count > 0)
                        {
                            dtAllotedBudgetDetails.DefaultView[0]["PROPOSED_CURRENT_YR"] = 0.00;
                            dtAllotedBudgetDetails.DefaultView[0]["APPROVED_CURRENT_YR"] = 0.00;
                            dtAllotedBudgetDetails.DefaultView[0]["NARRATION"] = string.Empty;
                            dtAllotedBudgetDetails.DefaultView[0]["IS_ALLOTED"] = 1; // (BudgetId == 0 ? 0 : 1);
                            dtAllotedBudgetDetails.AcceptChanges();
                        }
                        dtAllotedBudgetDetails.DefaultView.RowFilter = string.Empty;
                    }
                }
            }

            this.ReturnValue = dtAllotedBudgetDetails;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void glkpBudgetLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForGridLookUpEdit(glkpBudgetLedger);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validate the mandatory fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLedgerSelectionDetails()
        {
            bool isLedgerSelectionCreation = true;

            if (glkpBudgetLedger.EditValue == null || glkpBudgetLedger.EditValue.ToString() == "0")
            {
                this.ShowMessageBox("Ledger is empty");
                this.SetBorderColor(glkpBudgetLedger);
                glkpBudgetLedger.Focus();
                isLedgerSelectionCreation = false;
            }
            else if (string.IsNullOrEmpty(txtsubLedger.Text.Trim()))
            {
                this.ShowMessageBox("Sub Ledger is empty");
                this.SetBorderColor(txtsubLedger);
                txtsubLedger.Focus();
                isLedgerSelectionCreation = false;
            }
            return isLedgerSelectionCreation;
        }

        /// <summary>
        /// Load Ledger Details to lookup edit contrls in order to create Sub Ledgers
        /// </summary>
        private void LoadLedger()
        {
            try
            {
                using (BudgetSystem budgetsystem = new BudgetSystem())
                {
                    budgetsystem.ProjectId = ProjectId;
                    resultArgs = budgetsystem.FetchBudgetLedgerByGroup();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBudgetLedger, resultArgs.DataSource.Table, budgetsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, budgetsystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        glkpBudgetLedger.EditValue = glkpBudgetLedger.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// Set Project Start date from the account Period
        /// </summary>
        private void SetDefaults()
        {
            LoadLedger();
            LoadLedgerProject();
            SetTitle();
            lcgSubLedger.Visibility = LayoutVisibility.Never;
        }

        /// <summary>
        /// Set Caption of the title.
        /// </summary>
        private void SetTitle()
        {
            this.Text = this.subLedgerId == 0 ? "Sub Ledger Add" : "Sub Ledger Edit";
        }

        private void LoadLedgerProject()
        {
            try
            {
                if (dtAllotedBudgetDetails != null && dtAllotedBudgetDetails.Rows.Count > 0)
                {
                    DataTable dtBind = new DataTable();
                    //dtAllotedBudgetDetails.DefaultView.RowFilter = BudgetId == 0 ? "IS_ALLOTED IN (-1) AND IS_SUB_LEDGER=0" : "IS_ALLOTED IN (0,-1) AND IS_SUB_LEDGER=0";
                    dtAllotedBudgetDetails.DefaultView.RowFilter = BudgetId == 0 ? "IS_ALLOTED IN (0, -1) AND IS_SUB_LEDGER=0" : "IS_ALLOTED IN (0, -1) AND IS_SUB_LEDGER=0";
                    dtBind = dtAllotedBudgetDetails.DefaultView.ToTable();
                    gcSubLedgerProject.DataSource = dtBind;
                    dtAllotedBudgetDetails.DefaultView.RowFilter = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private DataTable AddColumns(DataTable dtLedgers)
        {
            DataTable dtAddedledger = dtLedgers;
            if (!dtAddedledger.Columns.Contains(CheckSelected))
            {
                dtAddedledger.Columns.Add(CheckSelected, typeof(int));
            }
            return dtAddedledger;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvSubLedgerProject.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvSubLedgerProject, gvColLedgerName);
            }
        }

        #endregion

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnselect = SelectAll(((DataTable)gcSubLedgerProject.DataSource), chkSelectAll);
            if (dtUnselect != null)
            {
                gcSubLedgerProject.DataSource = dtUnselect;
            }
        }

        private DataTable SelectAll(DataTable dtAllRecords, CheckEdit ctrlChekBox)
        {
            if (dtAllRecords != null && dtAllRecords.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAllRecords.Rows)
                {
                    dr[CheckSelected] = ctrlChekBox.Checked;
                }
            }
            return dtAllRecords;
        }
    }
}