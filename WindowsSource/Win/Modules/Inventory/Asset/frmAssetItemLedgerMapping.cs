using System;
using System.Data;
using System.Linq;

using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO.Schema;
using Bosco.Model.UIModel;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.ComponentModel;
using System.Collections.Generic;
using AcMEDSync.Model;


namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetItemLedgerMapping : frmFinanceBaseAdd
    {
        #region Declaration

        ResultArgs resultArgs = null;
        ApplicationSchema appSchema = new ApplicationSchema();
        List<int> selectedRows = new List<int>();

        #endregion

        #region Constructors

        public frmAssetItemLedgerMapping()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void LoadAssetClassDetails()
        {
            try
            {
                ResultArgs resultArgs = null;
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    resultArgs = assetItemSystem.FetchAssetItemDetails();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        DataTable dtLedgerAssetClass = resultArgs.DataSource.Table;
                        gcAssetItemLedgerMapping.DataSource = dtLedgerAssetClass;
                    }
                    else
                    {
                        gcAssetItemLedgerMapping.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally
            { }
        }

        private void LoadAssetLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Assert;
                resultArgs = ledgerSystem.FetchLedgerByNature();
                DataView dvAccountLeger = new DataView(resultArgs.DataSource.Table);
                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedgerMapping, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);

                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkAccountLedger, resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.AssetItemLedgerMapping.ASSET_LEDGER_MAPPING_TITLE);
        }

        private DataTable UpdateDataTableValues(DataTable dtAssetItemMapping)
        {
            try
            {
                if (dtAssetItemMapping != default(DataTable))
                {
                    dtAssetItemMapping.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                    {
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            dr[appSchema.Ledger.LEDGER_IDColumn.ColumnName] = glkpLedgerMapping.EditValue.ToString();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return dtAssetItemMapping;
        }

        private void UpdateLedgers()
        {
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                assetItemSystem.dtMappedAssetItems = gcAssetItemLedgerMapping.DataSource as DataTable;
                resultArgs = assetItemSystem.UpdateMappedAccountLedgerToItems();
                if (resultArgs != null && resultArgs.Success)
                {
                    //this.ShowMessageBox("Updated successfully.");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItemLedgerMapping.ASSET_LEDGER_MAPPING_SUCCESS));
                }
            }
        }

        private DataTable UpdateAccountLedgers()
        {
            DataTable dtSelectedList = new DataTable();
            DataTable dtDataSource = gcAssetItemLedgerMapping.DataSource as DataTable;
            if (!string.IsNullOrEmpty(glkpLedgerMapping.Text))
            {
                if (dtDataSource != null && dtDataSource.Rows.Count > 0)
                {
                    int[] SelectedIds = gvAssetItemLedgerMapping.GetSelectedRows();
                    if (gvAssetItemLedgerMapping.SelectedRowsCount > 0)
                    {
                        dtSelectedList = dtDataSource.Clone();
                        if (SelectedIds.Count() > 0)
                        {
                            foreach (int RowIndex in SelectedIds)
                            {
                                DataRow drLedger = gvAssetItemLedgerMapping.GetDataRow(RowIndex);
                                if (drLedger != null)
                                {
                                    drLedger[appSchema.Ledger.LEDGER_IDColumn.ColumnName] = glkpLedgerMapping.EditValue;
                                }
                            }
                            dtSelectedList.DefaultView.Sort = dtDataSource.DefaultView.Sort;
                        }
                    }
                    else
                    {
                        UpdateDataTableValues(dtDataSource);
                    }
                }
            }
            return dtSelectedList;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAssetItemLedgerMapping.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvAssetItemLedgerMapping, colAssetItem);
            }
        }

        private void RestoreSelection(GridView view)
        {
            BeginInvoke(new Action(() =>
            {
                view.ClearSelection();
                for (int i = 0; i < selectedRows.Count; i++)
                {
                    int rowhandle = view.GetRowHandle(selectedRows[i]);
                    view.SelectRow(rowhandle);
                }
            }));
        }

        #endregion

        #region Events

        private void glkpLedgerMapping_EditValueChanged(object sender, EventArgs e)
        {
            //DataTable dtAssetItem = gcAssetItemLedgerMapping.DataSource as DataTable;
            //UpdateDataTableValues(dtAssetItem);
        }

        private void frmAssetItemLedgerMapping_Load(object sender, EventArgs e)
        {
            LoadAssetLedgers();
            LoadAssetClassDetails();
            SetTitle();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            resultArgs = UpdateAccountLedgerOpBalance();
            if (resultArgs != null && resultArgs.Success)
            {

                UpdateLedgers();
            }
        }

        private ResultArgs UpdateAccountLedgerOpBalance()
        {
            int ProjectId = 0;
            int PrevAssetLedgerId = 0;
            int AssetLedgerId = 0;
            int ItemId = 0;
            double Amount = 0;
            double LedOPAmount = 0;
            double LedTempOPAmount = 0;

            BalanceProperty BalanceProperty = new AcMEDSync.Model.BalanceProperty();

            gvAssetItemLedgerMapping.UpdateCurrentRow();
            DataTable dtItems = (DataTable)gcAssetItemLedgerMapping.DataSource;
            if (dtItems != null && dtItems.Rows.Count > 0)
            {
                foreach (DataRow dritem in dtItems.Rows)
                {
                    AssetLedgerId = this.UtilityMember.NumberSet.ToInteger(dritem["LEDGER_ID"].ToString());
                    PrevAssetLedgerId = this.UtilityMember.NumberSet.ToInteger(dritem["ACCOUNT_LEDGER_ID"].ToString());
                    ItemId = this.UtilityMember.NumberSet.ToInteger(dritem["ITEM_ID"].ToString());

                    if (PrevAssetLedgerId > 0 && PrevAssetLedgerId != AssetLedgerId)
                    {
                        using (AssetInwardOutwardSystem inwardoutward = new AssetInwardOutwardSystem())
                        {
                            using (BalanceSystem balancesystem = new BalanceSystem())
                            {
                                resultArgs = inwardoutward.FetchTransactionDetailsByItemId(ItemId);
                                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    DataTable dtItemsProject = resultArgs.DataSource.Table;
                                    if (dtItemsProject != null && dtItemsProject.Rows.Count > 0)
                                    {
                                        foreach (DataRow drItems in dtItemsProject.Rows)
                                        {
                                            ProjectId = this.UtilityMember.NumberSet.ToInteger(drItems["PROJECT_ID"].ToString());
                                            Amount = this.UtilityMember.NumberSet.ToDouble(drItems["AMOUNT"].ToString());

                                            LedOPAmount = GetOpeningBalance(this.AppSetting.BookBeginFrom, ProjectId, PrevAssetLedgerId);
                                            LedTempOPAmount = LedOPAmount > 0 ? (LedOPAmount - Amount) > 0 ? LedOPAmount - Amount : 0 : 0;
                                            resultArgs = UpdateOpBalance(ProjectId, PrevAssetLedgerId.ToString(), (LedOPAmount - Amount) > 0 ? LedOPAmount - Amount : 0);

                                            if (!resultArgs.Success)
                                                break;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return resultArgs;
        }

        public double GetOpeningBalance(string OpDate, int ProjectId, int LedgerId)
        {
            double Amount = 0;
            using (BalanceSystem balanceSystem = new BalanceSystem())
            {
                BalanceProperty balProperty = balanceSystem.GetBalance(ProjectId, LedgerId, OpDate, BalanceSystem.BalanceType.OpeningBalance);
                Amount = balProperty.Amount;
            }
            return Amount;
        }

        private ResultArgs UpdateOpBalance(int ProjectId, string LedId, double Amount)
        {
            if (LedId != "0")
            {
                using (MappingSystem mapsystem = new MappingSystem())
                {
                    mapsystem.ProjectId = ProjectId;
                    mapsystem.LedgerId = UtilityMember.NumberSet.ToInteger(LedId);
                    resultArgs = mapsystem.MapProjectLedger();

                    if (resultArgs != null && resultArgs.Success)
                    {
                        using (BalanceSystem balanceSystem = new BalanceSystem())
                        {
                            if (balanceSystem.HasBalance(ProjectId, UtilityMember.NumberSet.ToInteger(LedId)))
                            {
                                resultArgs = balanceSystem.UpdateOpBalance(this.AppSetting.BookBeginFrom, ProjectId,
                                       UtilityMember.NumberSet.ToInteger(LedId), Amount, TransactionMode.DR.ToString(), TransactionAction.EditBeforeSave);
                            }
                        }
                    }
                }

            }
            return resultArgs;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetItemLedgerMapping_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UpdateAccountLedgers();
        }

        private void gvAssetItemLedgerMapping_ColumnFilterChanged(object sender, EventArgs e)
        {
            RestoreSelection(sender as GridView);
        }

        private void gvAssetItemLedgerMapping_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = sender as GridView;

            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.Column != null && hi.Column.FieldName == "DX$CheckboxSelectorColumn")
            {
                if (!hi.InRow)
                {
                    //DataView dv = view.DataSource as DataView;
                    bool allSelected = view.DataController.Selection.Count - view.DataController.GroupRowCount == view.DataRowCount;
                    if (!allSelected)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            int sourceHandle = view.GetRowHandle(i);

                            if (view.IsDataRow(sourceHandle))
                            {
                                // string donoremail = view.GetDataRow(sourceHandle)["email"].ToString();
                                if (!selectedRows.Contains(sourceHandle))  //&& !string.IsNullOrEmpty(donoremail))
                                    selectedRows.Add(sourceHandle);
                            }
                        }
                    }
                    else selectedRows.Clear();
                }
                else
                {
                    int sourceHandle = view.GetDataSourceRowIndex(hi.RowHandle);
                    if (!selectedRows.Contains(sourceHandle))
                        selectedRows.Add(sourceHandle);
                    else
                        selectedRows.Remove(sourceHandle);
                }
            }
            RestoreSelection(view);
        }

        private void gvAssetItemLedgerMapping_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = sender as GridView;
            RestoreSelection(view);
        }

        private void gvAssetItemLedgerMapping_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh)
            {
                gvAssetItemLedgerMapping.UnselectRow(gvAssetItemLedgerMapping.FocusedRowHandle);
            }
        }

        private void glkpLedgerMapping_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    ACPP.Modules.Master.frmLedgerDetailAdd frmLedgerDetailAdd = new ACPP.Modules.Master.frmLedgerDetailAdd(); //int)AddNewRow.NewRow, ledgerSubType.GN
                    frmLedgerDetailAdd.ShowDialog();
                    if (frmLedgerDetailAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadAssetLedgers();
                        if (frmLedgerDetailAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString()) > 0)
                        {
                            glkpLedgerMapping.EditValue = this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }

        #endregion
    }
}
