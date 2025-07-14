using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Utility;
using System.IO;
using System.Threading;
using Bosco.Model.Dsync;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using Bosco.Model.UIModel;
using AcMEDSync.Model;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmMergeAllLedgers : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        string defaultLedgerFilter = string.Empty;
        DataTable dtMergeProcessedLedgers = new DataTable();
        private bool IsMerged = false;

        private Int32 ProjectId
        {
            get
            {
                Int32 id = (glkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                return id;
            }

        }

        #endregion

        #region Constructor
        public frmMergeAllLedgers()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        /// <summary>
        /// Load the Headofffice and Branchoffice Mapped and Unmapped Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUnMappedLedgers_Load(object sender, EventArgs e)
        {
            //Skip Cash/Bank/FD, SundryCreditor and DutiesandTax ledgers
            defaultLedgerFilter = "(" + (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit
                                            + "," + (int)TDSLedgerGroup.DutiesAndTax + "," + (int)TDSLedgerGroup.SundryCreditors + ") AND ACCESS_FLAG NOT IN (2)";
            BindListofLedger();
            
            colProjectCatogoryName.Visible = false;
            if (AppSetting.EnablePortal == 1)
            {
                radioMergeType.SelectedIndex = 1; //For HO
                colProjectCatogoryName.Visible = true;
            }
            else
            {
                radioMergeType.Properties.Items[1].Enabled = false;
                radioMergeType.SelectedIndex = 0;
            }

            LoadProject(glkpProject);
        }

        /// <summary>
        /// To Map Headoffice ledger to Branchoffice Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapLedgers_Click(object sender, EventArgs e)
        {
            DataTable dtLedgers = gcMergeLedgers.DataSource as DataTable;
            DataTable dtMergeLedgers = dtLedgers.Copy();
            dtMergeLedgers.DefaultView.RowFilter = "MERGE_LEDGER_ID > 0";
            dtMergeLedgers = dtMergeLedgers.DefaultView.ToTable();
            
            if (dtMergeLedgers != null && dtMergeLedgers.Rows.Count > 0)
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Utility.LedgerMerging.MERGE_LEDGER_STRONG_MESSAGE), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.None;

                    if (CheckSelectedLedgers(dtMergeLedgers))
                    {
                        using (MappingSystem Merge = new MappingSystem())
                        {
                            Merge.ProjectId = ProjectId;
                            Merge.dtMergeLedgers = dtMergeLedgers;
                            if (Merge.dtMergeLedgers != null && Merge.dtMergeLedgers.Rows.Count > 0)
                            {
                                bool IsExistsTDS = ValidateTDSLedger(dtMergeLedgers);
                                if (!IsExistsTDS)
                                {
                                    resultArgs = Merge.MergeLedgersNew();
                                    if (resultArgs.Success)
                                    {
                                        IsMerged = true;
                                        dtMergeProcessedLedgers.Merge(dtMergeLedgers.Copy());
                                        ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.MERGED_SUCCESSFULLY));
                                        BindListofLedger();
                                        LoadBranchOfficeLedger();
                                    }
                                }
                                else
                                {
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Utility.LedgerMerging.MERGE_LEDGER_TDS_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        resultArgs = Merge.MergeLedgersNew();
                                        if (resultArgs.Success)
                                        {
                                            IsMerged = true;
                                            dtMergeProcessedLedgers.Merge(dtMergeLedgers.Copy());
                                            ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.MERGED_SUCCESSFULLY));
                                            BindListofLedger();
                                            LoadBranchOfficeLedger();
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.LEDGER_MERGED_WITH));
                gcMergeLedgers.Focus();
            }
        }
        
        /// <summary>
        /// Load list of BO Ledgers or HO Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioMergeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            if (edit.SelectedIndex == 0)
            {
                this.capMergeLedger.Caption = "Merge with (Branch Office Ledgers)";
                LoadBranchOfficeLedger();
                BindListofLedger();
            }
            else
            {
                this.capMergeLedger.Caption = "Merge with (Head Office Ledgers)";
                LoadBranchOfficeLedger();
                BindListofLedger();
            }
        }

        private void gvUnmappedLedgers_ShownEditor(object sender, EventArgs e)
        {
           try
            {
                //Filter Head Office ledgers based on active brach office ledger group.
                // for ex: FD ledgers (GroupID=14), should map only head office fd leadgers 
                DataRow drBrach = null;
                int BranchLedgerId = 0;
                int BranchLedgerGroupId = 0;
                int IsBankInterestLedgerId = 0;
                int MergeLedgerId = 0;
                string filter = string.Empty;

                ColumnView cview = (ColumnView)sender;
                string text = cview.FocusedColumn.Name;
                if (cview.FocusedColumn.FieldName == colMergeLedgerId.FieldName && cview.FocusedValue != null)
                {
                    GridLookUpEdit grdlkp = (GridLookUpEdit)cview.ActiveEditor;
                    drBrach = gvMergeLedgers.GetDataRow(gvMergeLedgers.FocusedRowHandle);
                    
                    if (drBrach != null)
                    {
                        BranchLedgerId = UtilityMember.NumberSet.ToInteger(drBrach["LEDGER_ID"].ToString());
                        BranchLedgerGroupId = UtilityMember.NumberSet.ToInteger(drBrach["GROUP_ID"].ToString());
                        IsBankInterestLedgerId = UtilityMember.NumberSet.ToInteger(drBrach["IS_BANK_INTEREST_LEDGER"].ToString());
                        MergeLedgerId = UtilityMember.NumberSet.ToInteger(drBrach["MERGE_LEDGER_ID"].ToString());
                        string selectedBOLedges = GetSelectedLedger();

                        //1. same ledger should not be selected in both side (BOledger should not be merged with same BOledger)
                        filter = "MERGE_LEDGER_ID NOT IN (" + BranchLedgerId + ")";
                        
                        //2. remove already selected BO ledgers in merge ledger
                        if (!string.IsNullOrEmpty(selectedBOLedges))
                        {
                            filter += " AND MERGE_LEDGER_ID NOT IN (" + selectedBOLedges + ")";
                        }

                        //3.Bank interest ledger should only merge with bankn interest ledger
                        if (IsBankInterestLedgerId > 0)
                        {
                            filter += " AND IS_BANK_INTEREST_LEDGER = 1";
                        }
                        else
                        {
                            filter += " AND IS_BANK_INTEREST_LEDGER = 0";
                        }
                                                
                        DataTable dtHeadOfficeLedgers = grdlkp.Properties.DataSource as DataTable;
                        if (dtHeadOfficeLedgers != null && dtHeadOfficeLedgers.Rows.Count > 0)
                        {
                            DataView dvHeadOfficeLedgers = new DataView(dtHeadOfficeLedgers);
                            dvHeadOfficeLedgers.RowFilter = filter;
                            if (dvHeadOfficeLedgers != null)
                            {
                                grdlkp.Properties.DataSource = dvHeadOfficeLedgers.ToTable();
                                this.gvMergeLedgers.PostEditor();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void frmMergeAllLedgers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsMerged)
            {
                List<Int32[]> refreshedprojectledger = new List<Int32[]>();
                List<Int32> refreshedproject = new List<Int32>();
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.Mapping.UPDATING_BALANCE));
                    balanceSystem.VoucherDate = AppSetting.BookBeginFrom;
                    if (dtMergeProcessedLedgers != null)
                    {
                        foreach (DataRow dr in dtMergeProcessedLedgers.Rows)
                        {
                            Int32 mergelid = UtilityMember.NumberSet.ToInteger(dr["MERGE_LEDGER_ID"].ToString());
                            Int32 fromlid = UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                            string projectids = dr["PROJECT_ID"].ToString();
                            string ledgerids = mergelid + "," + fromlid;
                            string[] pids = projectids.Split(',');
                            string[] lids = ledgerids.Split(',');

                            foreach (string strpid in pids)
                            {
                                Int32 pid = UtilityMember.NumberSet.ToInteger(strpid);
                                if (!refreshedproject.Contains(pid))
                                {
                                    balanceSystem.ProjectId = pid;
                                    ResultArgs result = balanceSystem.UpdateBulkTransBalance();
                                    refreshedproject.Add(pid);
                                }
                                else
                                {
                                    string skippedpids = string.Empty;
                                    skippedpids +=pid.ToString();
                                }
                                /*foreach (string strlid in lids)
                                {
                                    Int32 lid = UtilityMember.NumberSet.ToInteger(strlid);
                                    Int32[] projectledger = { pid, lid };
                                    //if (!refreshedprojectledger.Contains<Int32[]>(projectledger))
                                    //{
                                    balanceSystem.ProjectId = pid;
                                    balanceSystem.LedgerId = lid;
                                    ResultArgs result = balanceSystem.UpdateBulkTransBalance();
                                    refreshedprojectledger.Add(projectledger);
                                    //}                               
                                }*/

                            }
                        }
                    }
                    this.CloseWaitDialog();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Bind Branch ledgers
        /// </summary>
        private void BindListofLedger()
        {
            DataView dvLedgers = new DataView();
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ResultArgs  resultArgs = ledgerSystem.FetchMergeLedgers();
                if (resultArgs.Success && resultArgs.DataSource.Table!=null)
                {
                    resultArgs.DataSource.Table.PrimaryKey = new DataColumn[] { resultArgs.DataSource.Table.Columns[ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] };
                    dvLedgers = resultArgs.DataSource.Table.AsDataView();
                    dvLedgers.RowFilter = ledgerSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName + " NOT IN " + defaultLedgerFilter ;

                    //For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
                    if (AppSetting.IS_SDB_INM && !string.IsNullOrEmpty(AppSetting.SDBINM_SkippedLedgerIds))
                    {
                        dvLedgers.RowFilter += " AND (" + ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + " NOT IN (" + AppSetting.SDBINM_SkippedLedgerIds +"))";
                    }

                    //On 11/03/2024, For Merge selected project alone
                    if (ProjectId > 0)
                    {
                        dvLedgers.RowFilter += " AND (" + ledgerSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName + " LIKE '%" + ProjectId.ToString() + "%')";
                    }

                    dvLedgers.Sort = ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName + "," + ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                    gcMergeLedgers.DataSource  = dvLedgers.ToTable();
                }
                else
                {
                    MessageRender.ShowMessage(resultArgs.Message);
                }
            }
        }
        
        /// <summary>
        /// load lookup of Edit and assign Members
        /// </summary>
        private void LoadBranchOfficeLedger()
        {
            try
            {
                bool onlyHOledgers = (radioMergeType.SelectedIndex==1);
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    ResultArgs resultArgs = ledgersystem.FetchBOLedgersForMerge();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtMergeLedgers = resultArgs.DataSource.Table;
                        string ledgerfilter = "MERGE_GROUP_ID NOT IN " + defaultLedgerFilter;
                        if (onlyHOledgers)
                        {
                            ledgerfilter += " AND (IS_BRANCH_LEDGER = 1)";
                        }
                        else
                        {
                            ledgerfilter += "AND (IS_BRANCH_LEDGER = 0)";
                        }

                        //For SDBINM Auditors suggested to skip below mentioned Ledgers for Voucher Entries
                        if (AppSetting.IS_SDB_INM && !string.IsNullOrEmpty(AppSetting.SDBINM_SkippedLedgerIds))
                        {
                            ledgerfilter += " AND (MERGE_LEDGER_ID NOT IN (" + AppSetting.SDBINM_SkippedLedgerIds + "))";
                        }

                        dtMergeLedgers.DefaultView.RowFilter = ledgerfilter;
                        rglkpMergeLedgers.DataSource = dtMergeLedgers.DefaultView.ToTable();
                        rglkpMergeLedgers.ValueMember = "MERGE_LEDGER_ID";
                        rglkpMergeLedgers.DisplayMember = "MERGE_LEDGER_NAME";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }


        /// <summary>
        /// Check selected ledgers and merged ledgers are tds leders
        /// </summary>
        /// <param name="dtValidate"></param>
        /// <returns></returns>
        private bool ValidateTDSLedger(DataTable dtValidate)
        {
            bool Rtn = false;
            try
            {
                DataView dvFilter = new DataView(dtValidate);
                dvFilter.RowFilter = "MERGE_LEDGER_ID > 0";
                DataTable dtMergedLedgersLedger = dvFilter.ToTable(true, new string[] { "MERGE_LEDGER_ID" });
                dvFilter.RowFilter = "";

                foreach (DataRow dr in dtMergedLedgersLedger.Rows)
                {
                    using (MappingSystem Map = new MappingSystem())
                    {
                        dvFilter.RowFilter = String.Format("MERGE_LEDGER_ID={0} AND LEDGER_ID<>{1}", UtilityMember.NumberSet.ToInteger(dr["MERGE_LEDGER_ID"].ToString()), this.UtilityMember.NumberSet.ToInteger(dr["MERGE_LEDGER_ID"].ToString()));
                        string LedgerIdCollection = string.Empty;
                        foreach (DataRow drLedgerId in dvFilter.ToTable().Rows)
                        {
                            LedgerIdCollection += drLedgerId["LEDGER_ID"].ToString() + ',';
                        }
                        LedgerIdCollection = LedgerIdCollection.TrimEnd(',');
                        LedgerIdCollection = LedgerIdCollection + "," + dr["MERGE_LEDGER_ID"].ToString();

                        Map.ProjectId = ProjectId;
                        resultArgs = Map.IsTDSExists(LedgerIdCollection);
                        if (resultArgs.Success )
                        {
                            Rtn  = (resultArgs.DataSource.Sclar.ToInteger==1);
                            if (Rtn)
                            {
                                break;
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultArgs.Message);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// Validate selected Ledgers, Right now it is validated budget enabled ledger
        /// 
        /// later other validations would be covered here...
        /// </summary>
        /// <param name="dtValidate"></param>
        /// <returns></returns>
        private bool CheckSelectedLedgers(DataTable dtValidate)
        {
            //On 04/02/2020, dont check budget enabled ledger, because budgeted amount was covered while merging vouchers
            /*bool Rtn = false;

            if (IsBudgetEnabledLedgers(dtValidate))
            {
                Rtn = false;
                MessageRender.ShowMessage("Few of the selected Ledger(s) are Budgeted Ledgers, You can't Merge");
            }
            else
            {
                Rtn = true;
            }*/
            bool Rtn = true;
            return Rtn;
        }

        /// <summary>
        /// Validate selected ledgers 
        /// </summary>
        /// <param name="dtValidate"></param>
        /// <returns></returns>
        private bool IsBudgetEnabledLedgers(DataTable dtValidate)
        {
            bool Rtn = true;
            string LedgerIdCollection = string.Empty;
            try
            {
                using (MappingSystem map = new MappingSystem())
                {
                    foreach (DataRow dr in dtValidate.Rows)
                    {
                        LedgerIdCollection += dr[map.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() + ',';
                    }
                    LedgerIdCollection = LedgerIdCollection.TrimEnd(',');

                    if (!string.IsNullOrEmpty(LedgerIdCollection))
                    {
                        resultArgs = map.IsBudgetEnabledLedger(LedgerIdCollection);
                        if (resultArgs.Success)
                        {
                            Rtn = (resultArgs.DataSource.Sclar.ToInteger !=0);
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultArgs.Message);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// Check selected ledgers
        /// </summary>
        /// <param name="dtValidate"></param>
        /// <returns></returns>
        private string GetSelectedLedger()
        {
            string Rtn = string.Empty;
            DataTable dtLedgers = gcMergeLedgers.DataSource as DataTable;
            
            try
            {
                if (dtLedgers != null && dtLedgers.Rows.Count > 0)
                {
                    DataView dvFilter = new DataView(dtLedgers);
                    dvFilter.RowFilter = "MERGE_LEDGER_ID > 0";
                    DataTable dtMergedLedgersLedger = dvFilter.ToTable();
                    foreach (DataRow dr in dtMergedLedgersLedger.Rows)
                    {
                        Rtn += dr["LEDGER_ID"].ToString() + ',';
                    }
                    Rtn = Rtn.TrimEnd(',');
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
            return Rtn;
        }

        private void LoadProject(GridLookUpEdit lkpProject)
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = AppSetting.YearFrom;
                    ResultArgs resultArgs = mappingSystem.FetchProjectsLookup();
                    lkpProject.Properties.DataSource = null;

                    Int32 projectId = (lkpProject.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(lkpProject.EditValue.ToString()) : 0;
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(lkpProject, resultArgs.DataSource.Table, 
                            mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName, true,"-All-");
                        lkpProject.EditValue = 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        #endregion

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMergeLedgers.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvMergeLedgers, colLedgerName);
            }
        }

        private void gvMergeLedgers_RowCountChanged(object sender, EventArgs e)
        {
            if (gvMergeLedgers.RowCount > 0)
            {
                lblRecordCount.Text = "# "+ gvMergeLedgers.RowCount.ToString();
                lblRecordCount.AppearanceItemCaption.Font = new Font(lblRecordCount.AppearanceItemCaption.Font, FontStyle.Bold);
            }
            else
            {
                lblRecordCount.Text = string.Empty;
            }
        }

        private void rglkpMergeLedgers_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                (sender as GridLookUpEdit).EditValue = null;
            }
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            BindListofLedger();
        }
    }
}