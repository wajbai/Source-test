using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bosco.Model.UIModel;
using Bosco.Utility;
using AcMEDSync.Model;
using DevExpress.XtraEditors.Controls;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmMergeLedgers : frmFinanceBaseAdd
    {
        #region Variables
        public event EventHandler<ProgreesBarEvent> ProgressBarUpdate;
        public event EventHandler OnUpdateProgressBarMaimum;
        public event EventHandler OnUpdateProgressBar;

        ResultArgs resultArgs = null;
        LedgerSystem ledgerSystem = new LedgerSystem();
        DataTable dtMergedWithLedgers = new DataTable();
        DataTable dtTempTable = new DataTable();
        DataTable dtLookpUpLedgers = new DataTable();
        frmProgressBar setProgreessBar = new frmProgressBar();
        public bool IsMerged = false;
        public string LedgerIdCollection { get; set; }
        #endregion

        #region Constructor
        public frmMergeLedgers()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private DataTable dtLedgerComboBox { get; set; }

        #endregion

        #region Events
        private void frmMergeLedgers_Load(object sender, EventArgs e)
        {
            lcFilter.Visibility =  DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //lcFilterPanel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            BindListofLedger();
        }

        private void btnMoveLedgersForMergedWith_Click(object sender, EventArgs e)
        {
            try
            {
                if (chklistLedgersMerged.CheckedItems.Count > 0)
                {
                    if (glkpLegersMergedWith.EditValue != null && glkpLegersMergedWith.Text != string.Empty)
                    {
                        int LedgerId = UtilityMember.NumberSet.ToInteger(glkpLegersMergedWith.EditValue.ToString());
                        if (!dtTempTable.Columns.Contains("MAPPED_LEDGER_NAME"))
                        {
                            DataColumn newCol = new DataColumn("MAPPED_LEDGER_NAME", typeof(System.String));
                            newCol.AllowDBNull = true;
                            dtTempTable.Columns.Add(newCol);
                            DataColumn newColLedgerId = new DataColumn("MAPPED_LEDGER_ID", typeof(System.String));
                            newColLedgerId.AllowDBNull = true;
                            dtTempTable.Columns.Add(newColLedgerId);
                        }
                        DataTable dtLedger = chklistLedgersMerged.DataSource as DataTable;
                        dtLedger = SetPrimaryKey(dtLedger);
                        DataRow dr = dtLedger.Rows.Find(LedgerId);
                        dtTempTable.ImportRow(dr);
                        foreach (DataRow row in dtTempTable.Rows)
                        {
                            if (string.IsNullOrEmpty(row["MAPPED_LEDGER_NAME"].ToString()))
                            {
                                row["MAPPED_LEDGER_NAME"] = glkpLegersMergedWith.Text;
                                row["MAPPED_LEDGER_ID"] = UtilityMember.NumberSet.ToInteger(glkpLegersMergedWith.EditValue.ToString());
                            }
                        }

                        var CheckedLedgers = (from ledgers in dtMergedWithLedgers.AsEnumerable()
                                              where ((ledgers.Field<UInt32>("LEDGER_ID") != LedgerId))
                                              select ledgers);

                        var CheckedLookUpLedgers = (from ledgers in dtMergedWithLedgers.AsEnumerable()
                                                    where ((ledgers.Field<UInt32>("LEDGER_ID") == LedgerId))
                                                    select ledgers);

                        if (CheckedLookUpLedgers.Count() > 0)
                        {
                            if (dtLookpUpLedgers.Rows.Count > 0)
                                dtLookpUpLedgers.Merge(CheckedLookUpLedgers.CopyToDataTable());
                            else dtLookpUpLedgers = CheckedLookUpLedgers.CopyToDataTable();
                        }

                        if (CheckedLedgers.Count() > 0)
                            dtMergedWithLedgers = CheckedLedgers.CopyToDataTable();
                        else dtMergedWithLedgers = dtTempTable.Clone();

                        dtTempTable = SetPrimaryKey(dtTempTable);
                        DataRow drRemove = dtTempTable.Rows.Find(LedgerId);
                        if (drRemove != null)
                        {
                            dtTempTable.Rows.Remove(drRemove);
                            DataView dvOrder = new DataView(dtTempTable);
                            dvOrder.Sort = "MAPPED_LEDGER_NAME, LEDGER_ID";
                            gcMergingLedgers.DataSource = dvOrder.ToTable();
                            chklistLedgersMerged.DataSource = dtMergedWithLedgers;
                            DataView dvLedgers = new DataView(dtMergedWithLedgers);
                            if (chkShowHeadOfficeLedgers.Checked)
                            {
                                dvLedgers.RowFilter = "YES_NO='Yes'";
                            }
                            else
                            {
                                dvLedgers.RowFilter = "YES_NO='Yes' or YES_NO='No' ";
                            }
                            BindLedgerCombo(dvLedgers.ToTable());

                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLegersMergedWith, dvLedgers.ToTable(), ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                        }
                        SetLedgerCount();
                    }
                    else
                    {
                        ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.LEDGER_MERGED_WITH));
                        glkpLegersMergedWith.Focus();
                    }
                }
                else ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.LEDGER_MERGED));
            }
            catch (Exception ee)
            {
                ShowMessageBox(ee.Message);
            }
        }

        private void gvMergingLedgers_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column != gvMergingLedgers.Columns["MAPPED_LEDGER_NAME"]) return;
            string value1 = gvMergingLedgers.GetRowCellValue(e.RowHandle1, e.Column).ToString();
            string value2 = gvMergingLedgers.GetRowCellValue(e.RowHandle2, e.Column).ToString();
            if (value1 == value2)
            {
                e.Merge = true;
                e.Handled = true;
            }
        }

        private void chklistLedgersMerged_Click(object sender, EventArgs e)
        {
            RemoveLookupLedgers();
        }

        private void repLedgerDeleteSingle_Click(object sender, EventArgs e)
        {
            string Name = gvMergingLedgers.GetRowCellValue(gvMergingLedgers.FocusedRowHandle, "MAPPED_LEDGER_NAME").ToString();
            int MergedLedgerId = UtilityMember.NumberSet.ToInteger(gvMergingLedgers.GetRowCellValue(gvMergingLedgers.FocusedRowHandle, gvColMergedLedgerId).ToString());
            DataView dvTemp = new DataView(dtTempTable);
            dvTemp.RowFilter = String.Format("MAPPED_LEDGER_NAME='{0}'", Name);
            if (dvTemp.ToTable().Rows.Count > 0)
            {
                if (dvTemp.ToTable().Rows.Count == 1)
                {
                    if (dtLookpUpLedgers.Rows.Count > 0)
                    {
                        dtLookpUpLedgers = SetPrimaryKey(dtLookpUpLedgers);
                        DataRow drLookUp = dtLookpUpLedgers.Rows.Find(MergedLedgerId);
                        dtMergedWithLedgers.ImportRow(drLookUp);
                        dtLookpUpLedgers.Rows.Remove(drLookUp);
                    }
                }

                int LedgerId = UtilityMember.NumberSet.ToInteger(gvMergingLedgers.GetFocusedRowCellValue(gvColLedgerId).ToString());
                DataRow dr = dtTempTable.Rows.Find(LedgerId);
                dtMergedWithLedgers.ImportRow(dr);
                dtTempTable.Rows.Remove(dr);

                DataView dvOrder = new DataView(dtMergedWithLedgers);

                if (chkShowHeadOfficeLedgers.Checked)
                {
                    dvOrder.RowFilter = "YES_NO='Yes'";
                }
                else
                {
                    dvOrder.RowFilter = "YES_NO='Yes' or YES_NO='No' ";
                }
                dvOrder.Sort = "SORT_ID,LEDGER_NAME";
                BindLedgerCombo(dvOrder.ToTable());

                // this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLegersMergedWith, dvOrder.ToTable(), ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                DataTable dtAssignGrid = dtTempTable.Copy();
                gcMergingLedgers.DataSource = dtAssignGrid;
                chklistLedgersMerged.DataSource = dvOrder.ToTable();
                SetLedgerCount();
            }
        }

        private void btnUnmerge_Click(object sender, EventArgs e)
        {
            if (gvMergingLedgers.GetRowCellValue(gvMergingLedgers.FocusedRowHandle, "MAPPED_LEDGER_NAME") != null)
            {
                string Name = gvMergingLedgers.GetRowCellValue(gvMergingLedgers.FocusedRowHandle, "MAPPED_LEDGER_NAME").ToString();
                int MergedLedgerId = UtilityMember.NumberSet.ToInteger(gvMergingLedgers.GetRowCellValue(gvMergingLedgers.FocusedRowHandle, gvColMergedLedgerId).ToString());
                DataView dvTemp = new DataView(dtTempTable);
                dvTemp.RowFilter = String.Format("MAPPED_LEDGER_NAME='{0}'", Name);

                foreach (DataRow dr in dvTemp.ToTable().Rows)
                {
                    int LedgerId = UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                    DataRow import = dtTempTable.Rows.Find(LedgerId);
                    dtMergedWithLedgers.ImportRow(import);
                    dtTempTable.Rows.Remove(import);
                }
                if (dtLookpUpLedgers.Rows.Count > 0)
                {
                    dtLookpUpLedgers = SetPrimaryKey(dtLookpUpLedgers);
                    DataRow drLookUp = dtLookpUpLedgers.Rows.Find(MergedLedgerId);
                    dtMergedWithLedgers.ImportRow(drLookUp);
                    dtLookpUpLedgers.Rows.Remove(drLookUp);
                }
                DataView dvOrder = new DataView(dtMergedWithLedgers);
                if (chkShowHeadOfficeLedgers.Checked)
                {
                    dvOrder.RowFilter = "YES_NO='Yes'";
                }
                else
                {
                    dvOrder.RowFilter = "YES_NO='Yes' or YES_NO='No' ";
                }
                dvOrder.Sort = "SORT_ID,LEDGER_NAME";
                BindLedgerCombo(dvOrder.ToTable());
                //  this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLegersMergedWith, dvOrder.ToTable(), ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                DataTable dtAssingGrid = dtTempTable.Copy();
                gcMergingLedgers.DataSource = dtAssingGrid;
                chklistLedgersMerged.DataSource = dvOrder.ToTable();
                SetLedgerCount();
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            DataTable dtMergLedgers = gcMergingLedgers.DataSource as DataTable;
            if (dtMergLedgers != null && dtMergLedgers.Rows.Count > 0)
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Utility.LedgerMerging.MERGE_LEDGER_STRONG_MESSAGE), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (MappingSystem Merge = new MappingSystem())
                    {
                        gvColProgress.Visible = true;
                        Merge.dtMergeLedgers = dtTempTable;
                        if (Merge.dtMergeLedgers != null && Merge.dtMergeLedgers.Rows.Count > 0)
                        {
                            int IsExistsTDS = ValidateTDSLedger(dtTempTable);
                            if (IsExistsTDS == 0)
                            {
                                resultArgs = Merge.MergeLedgers();
                                if (resultArgs.Success)
                                {
                                    IsMerged = true;
                                    ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.MERGED_SUCCESSFULLY));
                                    gcMergingLedgers.DataSource = dtTempTable.Clone();
                                    dtLookpUpLedgers = dtLookpUpLedgers.Clone();
                                    dtTempTable = dtTempTable.Clone();
                                    dtMergedWithLedgers = dtMergedWithLedgers.Clone();
                                    BindListofLedger();
                                    layoutGroupWarning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                }
                            }
                            else
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Utility.LedgerMerging.MERGE_LEDGER_TDS_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    resultArgs = Merge.MergeLedgers();
                                    if (resultArgs.Success)
                                    {
                                        IsMerged = true;
                                        ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.MERGED_SUCCESSFULLY));
                                        gcMergingLedgers.DataSource = dtTempTable.Clone();
                                        dtLookpUpLedgers = dtLookpUpLedgers.Clone();
                                        dtTempTable = dtTempTable.Clone();
                                        dtMergedWithLedgers = dtMergedWithLedgers.Clone();
                                        BindListofLedger();
                                        layoutGroupWarning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.LEDGER_MERGED_WITH));
                glkpLegersMergedWith.Focus();
            }
        }
        private int ValidateTDSLedger(DataTable dtValidate)
        {
            foreach (DataRow dr in dtValidate.Rows)
            {
                using (MappingSystem Map = new MappingSystem())
                {
                    DataView dvFilter = new DataView(dtValidate);
                    dvFilter.RowFilter = String.Format("MAPPED_LEDGER_ID={0} AND LEDGER_ID<>{1}", UtilityMember.NumberSet.ToInteger(dr["MAPPED_LEDGER_ID"].ToString()), this.UtilityMember.NumberSet.ToInteger(dr["MAPPED_LEDGER_ID"].ToString()));
                    LedgerIdCollection = string.Empty;
                    foreach (DataRow drLedgerId in dvFilter.ToTable().Rows)
                    {
                        LedgerIdCollection += drLedgerId["LEDGER_ID"].ToString() + ',';

                    }
                    LedgerIdCollection = LedgerIdCollection.TrimEnd(',');
                    LedgerIdCollection = LedgerIdCollection + "," + dr["MAPPED_LEDGER_ID"].ToString();

                    resultArgs = Map.IsTDSExists(LedgerIdCollection);
                }
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }


        public void OnUpdate_ProgressBarMaimum(object sender, EventArgs e)
        {
            progressBar.Properties.Maximum = 2000;
        }

        public void OnUpdate_ProgressBar(object sender, EventArgs e)
        {
            progressBar.PerformStep();
            Application.DoEvents();
        }

        private void frmMergeLedgers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsMerged)
            {
                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.Mapping.UPDATING_BALANCE));
                    balanceSystem.VoucherDate = AppSetting.BookBeginFrom;
                    ResultArgs result = balanceSystem.UpdateBulkTransBalance();
                    this.CloseWaitDialog();
                }
            }
        }

        private void chkShowHeadOfficeLedgers_CheckedChanged(object sender, EventArgs e)
        {
            LoadHeadOfficeLedgerYesNo();
        }

        private void LoadHeadOfficeLedgerYesNo()
        {
            try
            {
                DataView dvLedgers = new DataView(dtLedgerComboBox);
                if (chkShowHeadOfficeLedgers.Checked)
                {
                    int LedgerId = UtilityMember.NumberSet.ToInteger(chklistLedgersMerged.SelectedValue.ToString());
                    if (chklistLedgersMerged.CheckedItems.Count > 0)
                    {
                        string SelectedLedgersMerged = string.Empty;
                        foreach (DataRowView item in chklistLedgersMerged.CheckedItems)
                        {
                            SelectedLedgersMerged += item["LEDGER_ID"].ToString() + ",";
                        }
                        SelectedLedgersMerged = SelectedLedgersMerged.TrimEnd(',');
                        dvLedgers.RowFilter = "YES_NO='Yes' AND LEDGER_ID NOT IN (" + SelectedLedgersMerged  + ")";
                    }
                    else
                    {
                        dvLedgers.RowFilter = "YES_NO='Yes'";
                    }

                    DataTable dt = dvLedgers.ToTable();
                    dvLedgers = new DataView(dt);
                }
                else
                {
                    string SelectedLedgersMerged = string.Empty;
                    if (chklistLedgersMerged.CheckedItems.Count > 0)
                    {
                        foreach (DataRowView item in chklistLedgersMerged.CheckedItems)
                        {
                            SelectedLedgersMerged += item["LEDGER_ID"].ToString() + ",";
                        }
                        SelectedLedgersMerged = SelectedLedgersMerged.TrimEnd(',');
                        dvLedgers.RowFilter = "LEDGER_ID NOT IN (" + SelectedLedgersMerged  + ")";
                        DataTable dt = dvLedgers.ToTable();
                        dvLedgers = new DataView(dt);
                    }
                }
                BindLedgerCombo(dvLedgers.ToTable());
            }
            catch (Exception ex){
                MessageRender.ShowMessage(ex.Message);
            }
        }
        #endregion

        #region Methods
        private void StartProgressBar()
        {
            setProgreessBar.ShowDialog();
        }

        private void BindListofLedger()
        {
            DataView dvLedgers = new DataView();
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                resultArgs = ledgerSystem.FetchMergeLedgers();
                if (resultArgs.Success && resultArgs != null)
                {
                    resultArgs.DataSource.Table.PrimaryKey = new DataColumn[] { resultArgs.DataSource.Table.Columns[ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName] };
                    //chklistLedgersMerged.DataSource = resultArgs.DataSource.Table;
                    dvLedgers = resultArgs.DataSource.Table.AsDataView();
                    string defaultFilter = "GROUP_ID NOT IN (12,13,14,24,26) AND ACCESS_FLAG NOT IN(2)";
                    dvLedgers.RowFilter = defaultFilter;
                    if (!String.IsNullOrEmpty(txtFilter.Text))
                    {
                        dvLedgers.RowFilter += " AND (LEDGER_NAME LIKE '%" + txtFilter.Text + "%')";
                    }
                    dtMergedWithLedgers = dvLedgers.ToTable();
                    chklistLedgersMerged.DataSource = dtMergedWithLedgers;

                    dvLedgers.RowFilter = defaultFilter;
                    dtMergedWithLedgers = dvLedgers.ToTable();

                    BindGridLookup(dtMergedWithLedgers);
                    //BindGridLookup(resultArgs.DataSource.Table);
                    //LayoutGroup.Text = String.Format(this.GetMessage(MessageCatalog.Master.Mapping.LEDGERS_TOBE_MERGED) + " <color=Green>Count: {0}</color> ", chklistLedgersMerged.ItemCount);
                    lblRecordCount.Text = "# " + chklistLedgersMerged.ItemCount.ToString(); ;
                }
            }
        }

        private void BindGridLookup(DataTable dtLedgers)
        {
            dtLedgerComboBox = dtLedgers;
            LoadHeadOfficeLedgerYesNo();
        }

        private void BindLedgerCombo(DataTable dtLedgers)
        {
            if (dtLedgers != null)
            {
                chklistLedgersMerged.DisplayMember = ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName;
                chklistLedgersMerged.ValueMember = ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName;
                glkpLegersMergedWith.Properties.DataSource = chklistLedgersMerged.DataSource as DataTable;
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLegersMergedWith, dtLedgers, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                glkpLegersMergedWith.EditValue = 0;
            }
        }

        private DataTable SetPrimaryKey(DataTable dtTable)
        {
            dtTable.PrimaryKey = new DataColumn[] { dtTable.Columns["LEDGER_ID"] };
            return dtTable;
        }

        private void SetLedgerCount()
        {
            //LayoutGroup.Text = String.Format(this.GetMessage(MessageCatalog.Master.Mapping.LEDGERS_TOBE_MERGED) + "<color=Green>Count: {0}/{1}</color>", chklistLedgersMerged.ItemCount, dtTempTable.Rows.Count + dtLookpUpLedgers.Rows.Count);
            lblRecordCount.Text = "# " + chklistLedgersMerged.ItemCount.ToString(); ;
        }

        private void RemoveLookupLedgers()
        {
            DataTable dtTable = chklistLedgersMerged.DataSource as DataTable;
            if (chklistLedgersMerged.SelectedValue != null)
            {
                int LedgerId = UtilityMember.NumberSet.ToInteger(chklistLedgersMerged.SelectedValue.ToString());
                if (chklistLedgersMerged.GetItemChecked(chklistLedgersMerged.SelectedIndex) == false)
                {
                    if (dtTable.Rows.Count == dtTempTable.Rows.Count + 1)
                    {
                        ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.CANNOT_SELECT_ALL_LEDGER));
                        return;
                    }
                }
                if (LedgerId != 0)
                {
                    if (dtTempTable.Columns.Count == 0)
                        dtTempTable = dtTable.Clone();
                    dtTable = SetPrimaryKey(dtTable);
                    dtMergedWithLedgers = SetPrimaryKey(dtMergedWithLedgers);
                    DataRow dr = dtTable.Rows.Find(LedgerId);
                    var UncheckLedger = (from ledgers in dtTempTable.AsEnumerable()
                                         where ((ledgers.Field<UInt32>("LEDGER_ID") == LedgerId))
                                         select ledgers);
                    if (UncheckLedger.Count() > 0)
                    {
                        DataRow drAvoidDuplicate = null;
                        if (dtMergedWithLedgers.Rows.Count > 0)
                            drAvoidDuplicate = dtMergedWithLedgers.Rows.Find(LedgerId);
                        if (drAvoidDuplicate == null)
                        {
                            dtMergedWithLedgers.ImportRow(dr);
                            dtTempTable = SetPrimaryKey(dtTempTable);
                            DataRow dr1 = dtTempTable.Rows.Find(LedgerId);
                            dtTempTable.Rows.Remove(dr1);
                        }
                    }
                    else
                    {
                        dtTempTable.ImportRow(dr);

                        var CheckedLedgers = (from ledgers in dtMergedWithLedgers.AsEnumerable()
                                              where ((ledgers.Field<UInt32>("LEDGER_ID") != LedgerId))
                                              select ledgers);
                        if (CheckedLedgers.Count() > 0)
                            dtMergedWithLedgers = CheckedLedgers.CopyToDataTable();
                    }
                    DataView dvOrder = new DataView(dtMergedWithLedgers);
                    dvOrder.Sort = "SORT_ID,LEDGER_NAME";
                    if (chkShowHeadOfficeLedgers.Checked)
                    {
                        dvOrder.RowFilter = "YES_NO='Yes'";
                    }
                    else
                    {
                        dvOrder.RowFilter = "YES_NO='Yes' or YES_NO='No' ";
                    }

                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLegersMergedWith, dvOrder.ToTable(), ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        #endregion

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            lcFilter.Visibility = (chkShowFilter.Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            if (lcFilter.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                txtFilter.Select();
                txtFilter.Focus();
            }
            else
            {
                txtFilter.Text = string.Empty;
                BindListofLedger();
            }
        }

        private void txtFilter_EditValueChanged(object sender, EventArgs e)
        {
            BindListofLedger();
        }
    }

    public class ProgreesBarEvent : EventArgs
    {
        public int MaxValue { get; set; }
        public ProgreesBarEvent(int MaxValue)
        {
            this.MaxValue = MaxValue;
        }

    }
}
