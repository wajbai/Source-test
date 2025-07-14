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
using Bosco.Model.UIModel;
using System.Collections;
using Bosco.Model.Setting;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AcMEDSync.Model;
using Bosco.Model.Business;

namespace ACPP.Modules.Master
{
    public partial class frmMapLedgersWithGeneralate : frmFinanceBaseAdd
    {
        #region Variables

        double CashBankFDOPBalance = 0;
        DataTable dtCongregationMappedLedgers = new DataTable();
        DataTable dtFAdetails = new DataTable();
        DataTable dtAllYearFAdetails = new DataTable();
        bool FromReportModule = false;
        DateTime FromReportDate;
        DateTime ToReportDate;

        ResultArgs resultArgs = new ResultArgs();

        private Int32 ConLedgerId
        {
            get
            {
                Int32 id = (glkpGeneralateLedgers.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpGeneralateLedgers.EditValue.ToString()) : 0;
                return id;
            }

        }

        private string ConLedgerCode
        {
            get
            {
                string rtn = string.Empty;
                if (glkpGeneralateLedgers.EditValue != null)
                {
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        DevExpress.XtraGrid.Views.Grid.GridView view = glkpGeneralateLedgers.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                        DataRowView drConLedger = glkpGeneralateLedgers.GetSelectedDataRow() as DataRowView;
                        if (drConLedger != null)
                        {
                            rtn = string.IsNullOrEmpty(drConLedger[ledgersystem.AppSchema.GeneralateGroupLedger.CON_LEDGER_CODEColumn.ColumnName].ToString()) ?
                                                        string.Empty : drConLedger[ledgersystem.AppSchema.GeneralateGroupLedger.CON_LEDGER_CODEColumn.ColumnName].ToString();
                        }
                    }
                }

                return rtn;
            }

        }

        private Natures ConNature
        {
            get
            {
                Natures rtn = Natures.Expenses; //Default Expenses Ledgers
                if (glkpGeneralateLedgers.EditValue != null)
                {
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        DevExpress.XtraGrid.Views.Grid.GridView view = glkpGeneralateLedgers.Properties.View as DevExpress.XtraGrid.Views.Grid.GridView;
                        DataRowView drConLedger = glkpGeneralateLedgers.GetSelectedDataRow() as DataRowView;
                        if (drConLedger != null)
                        {
                            rtn = string.IsNullOrEmpty(drConLedger[ledgersystem.AppSchema.GenerlateReport.CON_NATURE_IDColumn.ColumnName].ToString()) ?
                                  Natures.Expenses : (Natures)UtilityMember.NumberSet.ToInteger(drConLedger[ledgersystem.AppSchema.GenerlateReport.CON_NATURE_IDColumn.ColumnName].ToString());

                        }
                    }
                }

                return rtn;
            }

        }

        private bool IsCashBankFD
        {
            get
            {
                return ((ConLedgerCode == "A"));
            }
        }

        private Int32 FixeAssetGenerlateLedgerId
        { get; set; }
        

        private Int32 DepreciationGenerlateLedgerId
        { get; set; }
        

        private bool IsFixedAsset
        {
            get
            {
                return ((ConLedgerCode == "B"));
            }
        }

        private bool IsDepreciation
        {
            get
            {
                return ((ConLedgerCode == "G"));
            }
        }

        #endregion

        #region Constructor
        public frmMapLedgersWithGeneralate()
        {
            InitializeComponent();
            FromReportDate = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
            ToReportDate = UtilityMember.DateSet.ToDate(AppSetting.YearTo, false);

            RealColumnEditOPAmount();
            RealColumnFACreditAmount();
            RealColumnFADebitAmount();
        }

        public frmMapLedgersWithGeneralate(bool fromReportModule) : this()
        {
            FromReportModule = fromReportModule;

            FromReportDate = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
            ToReportDate = UtilityMember.DateSet.ToDate(AppSetting.YearTo, false);
        }

        public frmMapLedgersWithGeneralate(bool fromReportModule, DateTime fromReportDate, DateTime toReportDate ) : this()
        {
            FromReportModule = fromReportModule;
            FromReportDate = fromReportDate;
            ToReportDate = toReportDate;
        }

        #endregion

        #region Events

        private void frmMapLedgersWithGeneralate_Load(object sender, EventArgs e)
        {
            CheckAndUpdateDefaultCongregationLedger();
            BindCongregationLedgers();
            BindLedgers();
            BindFADetails();
            lblOpBalanceTitle.Text = "Opening Balance as on " + UtilityMember.DateSet.ToDate(AppSetting.FirstFYDateFrom.ToShortDateString(), false).ToShortDateString();

            //lcgFixedAssetDetails.Text = "Fixed Asset && Depreciation - " + UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).ToShortDateString() +
            //                                            " - " + UtilityMember.DateSet.ToDate(AppSetting.YearTo, false).ToShortDateString();

            lcgFixedAssetDetails.Text = "Fixed Asset && Depreciation - " + UtilityMember.DateSet.ToDate(FromReportDate.ToShortDateString(), false).ToShortDateString() +
                                                        " - " + UtilityMember.DateSet.ToDate(ToReportDate.ToShortDateString(), false).ToShortDateString();

            txtOpeningIEBalance.Text = AppSetting.GeneralateOpeningIEBalance.ToString();
            cboTransMode.SelectedIndex = AppSetting.GeneralateOpeningIEBalanceMode;
            lcgMapWithCongregation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcShowMapLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            chkLedgerSelectAll.Visible = false;

            if (FromReportModule)
            {
                lcShowMapLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgFixedAssetDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcFACancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                if (UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).ToShortDateString() == AppSetting.FirstFYDateFrom.ToShortDateString())
                {
                    lcgMapWithCongregation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcShowMapLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcgFixedAssetDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    chkLedgerSelectAll.Visible = true;
                }
                else
                {
                    lcgFixedAssetDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                lcFACancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLedger.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLedger, colLedgerName);
            }
        }

        private void gvLedgers_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvLedger.RowCount.ToString();
        }

        private void glkpGeneralateLedgers_EditValueChanged(object sender, EventArgs e)
        {
            BindLedgers();
            gvLedger.ActiveFilterString = string.Empty;
            chkShowFilter.Checked = false;

            colOPAmountTransMode.Visible = !IsCashBankFD;
            colOPAmount.Visible = !IsCashBankFD;
            lcBalanceMode.Visibility = (IsCashBankFD? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            lcOpeningIEBalance.Visibility = (IsCashBankFD? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            ShowNoteDetails();
            if(ConLedgerId == FixeAssetGenerlateLedgerId || ConLedgerId == DepreciationGenerlateLedgerId)
            {
                lcgFixedAssetDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lcgFixedAssetDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            
        }

        private void chkLedgerSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (gcLedger.DataSource != null)
            {
                DataTable dtAllLedger = (DataTable)gcLedger.DataSource;
                if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
                {
                    string gridfilter = gvLedger.ActiveFilterString;
                    dtAllLedger.DefaultView.RowFilter = gridfilter;

                    foreach (DataRowView drv in dtAllLedger.DefaultView)
                    {
                        drv.BeginEdit();
                        Int32 ledgerid = UtilityMember.NumberSet.ToInteger(drv[colLedgerId.FieldName].ToString());
                        if (HasFADepreciationDetails(ledgerid))
                            drv["SELECT"] = 1;
                        else
                            drv["SELECT"] = chkLedgerSelectAll.Checked;
                        drv.EndEdit();
                    }
                    dtAllLedger.DefaultView.RowFilter = string.Empty;
                    gcLedger.DataSource = null;
                    gcLedger.DataSource = dtAllLedger;

                }
            }
        }

        private void gcLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool CanFocusOk = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && (!e.Shift && !e.Alt && !e.Control)
                && (gvLedger.FocusedColumn == colOPAmount || gvLedger.FocusedColumn == colOPAmountTransMode || gvLedger.FocusedColumn == colSelect))
            {
                if (gvLedger.IsLastRow)
                {
                    CanFocusOk = true;
                }
                else
                {
                    gvLedger.MoveNext();
                    gvLedger.FocusedColumn = colOPAmount;
                    gvLedger.ShowEditor();
                    e.SuppressKeyPress = true;
                }

                if (CanFocusOk)
                {
                    btnFAUpdate.Select();
                    btnFAUpdate.Focus();
                    btnFAUpdate.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
                }
            }
        }

        private void RealColumnEditOPAmount()
        {
            colOPAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditOPAmount_EditValueChanged);
            this.gvLedger.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvLedger.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colOPAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvLedger.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditOPAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvLedger.PostEditor();
            gvLedger.UpdateCurrentRow();
            if (gvLedger.ActiveEditor == null)
            {
                gvLedger.ShowEditor();
            }

            TextEdit txtOPAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtOPAmount.Text.Length > grpCounts && txtOPAmount.SelectionLength == txtOPAmount.Text.Length)
                txtOPAmount.Select(txtOPAmount.Text.Length - grpCounts, 0);
        }

        private void RealColumnFACreditAmount()
        {
            colFAConCredit.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditFACredit_EditValueChanged);
            this.gvCYFADepreciation.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvCYFADepreciation.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colFAConCredit)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvCYFADepreciation.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditFACredit_EditValueChanged(object sender, EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvCYFADepreciation.PostEditor();
            gvLedger.UpdateCurrentRow();
            if (gvCYFADepreciation.ActiveEditor == null)
            {
                gvCYFADepreciation.ShowEditor();
            }

            TextEdit txtFACreditAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtFACreditAmount.Text.Length > grpCounts && txtFACreditAmount.SelectionLength == txtFACreditAmount.Text.Length)
                txtFACreditAmount.Select(txtFACreditAmount.Text.Length - grpCounts, 0);
        }

        private void RealColumnFADebitAmount()
        {
            colFAConDebit.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditFADebit_EditValueChanged);
            this.gvCYFADepreciation.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvCYFADepreciation.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colFAConCredit)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvCYFADepreciation.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditFADebit_EditValueChanged(object sender, EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvCYFADepreciation.PostEditor();
            gvLedger.UpdateCurrentRow();
            if (gvCYFADepreciation.ActiveEditor == null)
            {
                gvCYFADepreciation.ShowEditor();
            }

            TextEdit txtFADebitAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtFADebitAmount.Text.Length > grpCounts && txtFADebitAmount.SelectionLength == txtFADebitAmount.Text.Length)
                txtFADebitAmount.Select(txtFADebitAmount.Text.Length - grpCounts, 0);
        }

        private void btnLoadFromFinance_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmationMessage("Are you sure to get Fixed Asset and Depreciation general voucher(s) between " + FromReportDate.ToShortDateString() + " and " + ToReportDate.ToShortDateString() + "?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (gcCYFADepreciation.DataSource != null)
                {
                    using (LedgerSystem ledsystem = new LedgerSystem())
                    {
                        DataTable dtCYFA = gcCYFADepreciation.DataSource as DataTable;
                        foreach (DataRow dr in dtCYFA.Rows)
                        {
                            dr.BeginEdit();
                            dr[ledsystem.AppSchema.GenerlateReport.DEBITColumn.ColumnName] = dr[ledsystem.AppSchema.GenerlateReport.FN_DEBITColumn.ColumnName];
                            dr[ledsystem.AppSchema.GenerlateReport.CREDITColumn.ColumnName] = dr[ledsystem.AppSchema.GenerlateReport.FN_CREDITColumn.ColumnName];

                            dr[ledsystem.AppSchema.GenerlateReport.CON_CL_AMOUNTColumn.ColumnName] = (UtilityMember.NumberSet.ToDouble(dr[ledsystem.AppSchema.GenerlateReport.CON_OP_AMOUNTColumn.ColumnName].ToString()) + UtilityMember.NumberSet.ToDouble(dr[ledsystem.AppSchema.GenerlateReport.DEBITColumn.ColumnName].ToString()))
                                                  - UtilityMember.NumberSet.ToDouble(dr[ledsystem.AppSchema.GenerlateReport.CREDITColumn.ColumnName].ToString());
                            dr.EndEdit();
                        }
                        dtCYFA.AcceptChanges();

                        BindFADetails(dtCYFA);
                    }
                }
            }
        }

        private void gcCYFADepreciation_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && (!e.Shift && !e.Alt && !e.Control) &&
                   (gvCYFADepreciation.FocusedColumn == colFAConCredit))
            {
                gvCYFADepreciation.PostEditor();
                gvCYFADepreciation.UpdateCurrentRow();
                ShowFADepreciationFooterSummary();      
            }
        }

        private void gvLedger_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (gvLedger.GetRowCellValue(e.RowHandle, colLedgerId) != null)
                {
                    if (gvLedger.GetRowCellValue(e.RowHandle, colLedgerId).ToString() != string.Empty)
                    {
                        Int32 ledgerid = UtilityMember.NumberSet.ToInteger(gvLedger.GetRowCellValue(e.RowHandle, colLedgerId).ToString());
                        if (HasFADepreciationDetails(ledgerid))
                        {
                            //e.Appearance.BackColor = Color.LightGray;
                            if (e.Column == colSelect)
                            {
                                e.Appearance.BackColor = Color.LightGray;
                            }  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void gvLedger_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                if (gvLedger.FocusedColumn == colSelect && (IsFixedAsset || IsDepreciation))
                {
                    if (gvLedger.GetRowCellValue(gvLedger.FocusedRowHandle, colLedgerId) != null)
                    {
                        if (gvLedger.GetRowCellValue(gvLedger.FocusedRowHandle, colLedgerId).ToString() != string.Empty)
                        {
                            Int32 ledgerid = UtilityMember.NumberSet.ToInteger(gvLedger.GetRowCellValue(gvLedger.FocusedRowHandle, colLedgerId).ToString());
                            if (HasFADepreciationDetails(ledgerid))
                            {
                                e.Cancel = true;
                                ShowMessageBox("As Ledger has Fixed Asset and Depreciation details, not allowed to unmap Ledger.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void BindCongregationLedgers()
        {
            using (LedgerSystem ledsystem = new LedgerSystem())
            {
                resultArgs = ledsystem.FetchCongregationLedgers();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    DataTable dtCongregationLedgers = resultArgs.DataSource.Table;
                    //Get Fixed Asset and Drpreciation Generalate Ledgder--------------------------------------------------------------------
                    dtCongregationLedgers.DefaultView.RowFilter = string.Empty;
                    dtCongregationLedgers.DefaultView.RowFilter = ledsystem.AppSchema.GeneralateGroupLedger.CON_LEDGER_CODEColumn.ColumnName + " ='B'";
                    if (dtCongregationLedgers.DefaultView.Count > 0)
                    {
                        FixeAssetGenerlateLedgerId = UtilityMember.NumberSet.ToInteger(dtCongregationLedgers.DefaultView[0][ledsystem.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn.ColumnName].ToString());
                    }

                    dtCongregationLedgers.DefaultView.RowFilter = ledsystem.AppSchema.GeneralateGroupLedger.CON_LEDGER_CODEColumn.ColumnName + " ='G'";
                    if (dtCongregationLedgers.DefaultView.Count > 0)
                    {
                        DepreciationGenerlateLedgerId =  UtilityMember.NumberSet.ToInteger(dtCongregationLedgers.DefaultView[0][ledsystem.AppSchema.GeneralateGroupLedger.CON_LEDGER_IDColumn.ColumnName].ToString());
                    }

                    dtCongregationLedgers.DefaultView.RowFilter = string.Empty;
                    //-----------------------------------------------------------------------------------------------------------------------

                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGeneralateLedgers, dtCongregationLedgers,
                                ledsystem.AppSchema.GenerlateReport.CON_LEDGER_NAMEColumn.ColumnName, ledsystem.AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName);

                    glkpGeneralateLedgers.EditValue = (dtCongregationLedgers.Rows.Count > 0 ?
                        UtilityMember.NumberSet.ToInteger(dtCongregationLedgers.Rows[0][ledsystem.AppSchema.GenerlateReport.CON_LEDGER_IDColumn.ColumnName].ToString()) : 0);

                }
            }
        }

        /// <summary>
        /// Bind all list of ledgers
        /// </summary>
        private void BindLedgers()
        {
            using (BalanceSystem balancesystem = new BalanceSystem()) //BalanceSystem1
            {
                ResultArgs result = balancesystem.FetchTotalLedgerOpBalance();
                if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                {
                    CashBankFDOPBalance = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Rows[0][balancesystem.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                }
            }

            using (LedgerSystem ledsystem = new LedgerSystem())
            {
                resultArgs = ledsystem.FetchLedgersMappedWithCongregationLedgers(ConLedgerId, ConLedgerCode, ConNature, false);

                if (resultArgs != null && resultArgs.Success)
                {
                    dtCongregationMappedLedgers = resultArgs.DataSource.Table;
                    gcLedger.DataSource = dtCongregationMappedLedgers.DefaultView.ToTable();
                    gcLedger.RefreshDataSource();
                    gcLedger.Select();
                    gvLedger.MoveFirst();
                    gvLedger.FocusedColumn = colOPAmount;
                    gvLedger.ShowEditor();
                }
                else
                {
                    gcLedger.DataSource = null;
                }
            }

        }
        
        private void BindFADetails(DataTable dtCYFADetails = null)
        {
            using (LedgerSystem ledsystem = new LedgerSystem())
            {
                if (dtCYFADetails == null)
                {
                    resultArgs = ledsystem.FetchCongregationFixedAssetDetails(FromReportDate, ToReportDate);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dtFAdetails = resultArgs.DataSource.Table;
                    }
                }
                else
                {
                    dtFAdetails = dtCYFADetails;
                }

                if (dtFAdetails != null && dtFAdetails.Rows.Count > 0)
                {
                    dtFAdetails.DefaultView.Sort = ledsystem.AppSchema.GeneralateGroupLedger.CON_LEDGER_CODEColumn.ColumnName + "," + 
                                                    ledsystem.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName;
                    dtFAdetails = dtFAdetails.DefaultView.ToTable();
                    gcCYFADepreciation.DataSource = dtFAdetails;
                    
                    gcCYFADepreciation.RefreshDataSource();
                    gcCYFADepreciation.Select();
                    gvCYFADepreciation.MoveFirst();
                    gvCYFADepreciation.FocusedColumn = colFAConDebit;
                    gvCYFADepreciation.ShowEditor();
                }
                else
                {
                    gcCYFADepreciation.DataSource = null;
                }

                resultArgs = ledsystem.FetchAllCongregationFixedAssetDetails();
                if (resultArgs.Success)
                {
                    dtAllYearFAdetails = resultArgs.DataSource.Table;
                }
            }
            ShowFADepreciationFooterSummary();
        }




        /// <summary>
        /// This methoid is used to update Congregation Ledgers list and Mapp by default branch ledgers to Congregation Ledgers
        /// </summary>
        private void CheckAndUpdateDefaultCongregationLedger()
        {
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                ledgersystem.CheckAndUpdateDefaultCongregationLedger(ConNature);
            }
        }

        private bool ValidInput()
        {
            bool rtn = false;

            if (ConLedgerId == 0)
            {
                this.ShowMessageBox("Generalate Ledger is empty");
                glkpGeneralateLedgers.Select();
                glkpGeneralateLedgers.Focus();
            }
            //else if (!IsLedgersSelected())
            //{
            //    this.ShowMessageBox("Select any one of the Ledgers in the list");
            //    gcLedger.Select();
            //    gcLedger.Focus();
            //}
            else
            {
                /*if (string.IsNullOrEmpty(ContributionFromLedgers) || string.IsNullOrEmpty(ContributionToLedgers))
                {
                    this.ShowMessageBox("Contribution From/To Ledgers are empty");
                }*/

                rtn = true;
            }

            return rtn;
        }

        private bool IsLedgersSelected()
        {
            bool rtn = false;
            if (gcLedger.DataSource != null)
            {
               DataTable dtSelectedLedgers = gcLedger.DataSource as DataTable;
               dtSelectedLedgers.DefaultView.RowFilter = "SELECT =1";
               rtn = (dtSelectedLedgers.DefaultView.Count > 0);
               dtSelectedLedgers.DefaultView.RowFilter = string.Empty;
            }
            return rtn;
        }


        /// <summary>
        /// Show Opening balance sum
        /// </summary>
        private void ShowNoteDetails()
        {
            //On 10/03/2023-----------------------------------------------------------------------------------------------
            if (gcLedger.DataSource != null)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    DataTable dtSource = gcLedger.DataSource as DataTable;
                    double balance = 0;
                    if (IsCashBankFD)
                    {
                        balance = CashBankFDOPBalance;
                    }
                    else
                    {
                        double dr = UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(" + ledgersystem.AppSchema.GenerlateReport.CON_OP_AMOUNTColumn + ")", ledgersystem.AppSchema.GenerlateReport.CON_OP_TRANS_MODEColumn.ColumnName + "= 'DR'" ).ToString());
                        double cr = UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(" + ledgersystem.AppSchema.GenerlateReport.CON_OP_AMOUNTColumn + ")", ledgersystem.AppSchema.GenerlateReport.CON_OP_TRANS_MODEColumn.ColumnName + "= 'CR'").ToString());
                        balance = dr-cr;
                    }

                    string transmode = (balance == 0 && (ConNature == Natures.Assert || ConNature == Natures.Expenses)) ? "Dr" : "Cr";
                    if (ConNature==Natures.Assert || ConNature==Natures.Expenses )
                    {
                        transmode = (balance >= 0 ? "Dr" : "Cr");
                    }
                    else
                    {
                        transmode = (balance <= 0 ? "Cr" : "Dr");
                    }
                    
                    lblOpBalance.Text = "Balance : " + UtilityMember.NumberSet.ToNumber(Math.Abs(balance)) + " " + transmode;

                    lblNote.Text = "* ";
                    if (ConNature == Natures.Assert)
                    {
                        lblNote.Text += IsCashBankFD ? "Cash/Bank/FD Ledgers only (Openining Balance are from Finance Map Accounts)" : 
                                  IsFixedAsset? "Map Fixed Asset Ledgers only (Can't be unmapped if it has Fixed Asset Details)" : "Map Asset Ledgers only";
                    }
                    else if (ConNature == Natures.Libilities)
                    {
                        lblNote.Text += "Map Liability Ledgers only";
                    }
                    else if (ConNature == Natures.Income)
                    {
                        lblNote.Text += "Map Income Ledgers only";
                    }
                    else if (ConNature == Natures.Expenses)
                    {
                        lblNote.Text += (IsDepreciation ? "Map Depreciation Ledgers only (Can't be unmapped if it has Fixed Asset Details)" : "Map Expenses Ledgers only");
                    }
                }
            }


            //------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// Assing Total Balance in the Footer
        /// </summary>
        private void ShowFADepreciationFooterSummary()
        {
            double op = 0;
            double dr = 0;
            double cr = 0;
            double cl = 0;

            lblFixedAssetOP.Text = UtilityMember.NumberSet.ToNumber(op);
            lblFixedAssetDR.Text = UtilityMember.NumberSet.ToNumber(dr);
            lblFixedAssetCR.Text = UtilityMember.NumberSet.ToNumber(cr);
            lblFixedAssetTotal.Text = UtilityMember.NumberSet.ToNumber(cl);

            lblDepreciationOP.Text = UtilityMember.NumberSet.ToNumber(op); 
            lblDepreciationDR.Text = UtilityMember.NumberSet.ToNumber(dr);
            lblDepreciationCR.Text = UtilityMember.NumberSet.ToNumber(cr);
            lblDepreciationTotal.Text = UtilityMember.NumberSet.ToNumber(cl); 

            lblDepreciationAccuOP.Text = UtilityMember.NumberSet.ToNumber(op);
            lblDepreciationAccuDR.Text = UtilityMember.NumberSet.ToNumber(dr);
            lblDepreciationAccuCR.Text = UtilityMember.NumberSet.ToNumber(cr);
            lblDepreciationAccuTotal.Text = UtilityMember.NumberSet.ToNumber(cl);
            
            if (gcCYFADepreciation.DataSource != null)
            {
                DataTable dtData = gcCYFADepreciation.DataSource as DataTable;
                using (LedgerSystem ledsystem = new LedgerSystem())
                {
                    string condition = ledsystem.AppSchema.GenerlateReport.CON_LEDGER_CODEColumn.ColumnName + "='B'";
                    op = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.CON_OP_AMOUNTColumn.ColumnName + ")", condition).ToString());
                    dr = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.DEBITColumn.ColumnName + ")", condition).ToString());
                    cr = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.CREDITColumn.ColumnName + ")", condition).ToString());
                    cl = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.CON_CL_AMOUNTColumn.ColumnName + ")", condition).ToString());

                    lblFixedAssetOP.Text = UtilityMember.NumberSet.ToNumber(op);
                    lblFixedAssetDR.Text = UtilityMember.NumberSet.ToNumber(dr);
                    lblFixedAssetCR.Text = UtilityMember.NumberSet.ToNumber(cr);
                    lblFixedAssetTotal.Text = UtilityMember.NumberSet.ToNumber(cl);

                    condition = ledsystem.AppSchema.GenerlateReport.CON_LEDGER_CODEColumn.ColumnName + "='G'";
                    op = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.CON_OP_AMOUNTColumn.ColumnName + ")", condition).ToString());
                    dr = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.DEBITColumn.ColumnName + ")", condition).ToString());
                    cr = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.CREDITColumn.ColumnName + ")", condition).ToString());
                    cl = UtilityMember.NumberSet.ToDouble(dtData.Compute("SUM(" + ledsystem.AppSchema.GenerlateReport.CON_CL_AMOUNTColumn.ColumnName + ")", condition).ToString());

                    lblDepreciationOP.Text = (UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).ToShortDateString() == AppSetting.FirstFYDateFrom.ToShortDateString() ? UtilityMember.NumberSet.ToNumber(op) : "0.00");
                    lblDepreciationDR.Text = UtilityMember.NumberSet.ToNumber(dr);
                    lblDepreciationCR.Text = UtilityMember.NumberSet.ToNumber(cr);
                    //lblDepreciationTotal.Text = (UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).ToShortDateString() == AppSetting.FirstFYDateFrom.ToShortDateString() ? UtilityMember.NumberSet.ToNumber((op + dr) - cr) : UtilityMember.NumberSet.ToNumber(dr - cr));
                    lblDepreciationTotal.Text = (UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).ToShortDateString() == AppSetting.FirstFYDateFrom.ToShortDateString() ? UtilityMember.NumberSet.ToNumber(dr - cr) : UtilityMember.NumberSet.ToNumber(dr - cr));

                    lblDepreciationAccuOP.Text = UtilityMember.NumberSet.ToNumber(op);
                    lblDepreciationAccuDR.Text = UtilityMember.NumberSet.ToNumber(dr);
                    lblDepreciationAccuCR.Text = UtilityMember.NumberSet.ToNumber(cr);
                    lblDepreciationAccuTotal.Text = UtilityMember.NumberSet.ToNumber(cl);
                }
            }
        }

        private bool HasFADepreciationDetails(Int32 ledgerid)
        {
            bool rtn = false;
            try
            {
                if (dtFAdetails != null && dtFAdetails.Rows.Count > 0 && (IsFixedAsset || IsDepreciation))
                {
                    using (LedgerSystem ledgersys = new LedgerSystem())
                    {
                        //Check in current FY year
                        DataTable dtCheckFADetails = dtFAdetails.DefaultView.ToTable();
                        dtCheckFADetails.DefaultView.RowFilter = ledgersys.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + "= " + ledgerid;

                        if (dtCheckFADetails != null && dtCheckFADetails.DefaultView.Count > 0)
                        {
                            double drprev = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.DEBIT_PREVIOUSColumn.ColumnName].ToString());
                            double crprev = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.CREDIT_PREVIOUSColumn.ColumnName].ToString());

                            double dr = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.DEBITColumn.ColumnName].ToString());
                            double cr = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.CREDITColumn.ColumnName].ToString());
                            rtn = (crprev > 0 || drprev > 0  || cr > 0 || dr > 0);
                        }

                        //Check in all the FY years
                        if (!rtn)
                        {
                            dtCheckFADetails = dtAllYearFAdetails.DefaultView.ToTable();
                            dtCheckFADetails.DefaultView.RowFilter = ledgersys.AppSchema.Ledger.LEDGER_IDColumn.ColumnName + "= " + ledgerid;
                            if (dtCheckFADetails != null && dtCheckFADetails.DefaultView.Count > 0)
                            {
                                double drprev = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.DEBIT_PREVIOUSColumn.ColumnName].ToString());
                                double crprev = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.CREDIT_PREVIOUSColumn.ColumnName].ToString());

                                double dr = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.DEBITColumn.ColumnName].ToString());
                                double cr = UtilityMember.NumberSet.ToDouble(dtCheckFADetails.DefaultView[0][ledgersys.AppSchema.GenerlateReport.CREDITColumn.ColumnName].ToString());
                                rtn = (crprev > 0 || drprev > 0 || cr > 0 || dr > 0);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                rtn = true;
                this.ShowMessageBoxError(err.Message);
            }
            return rtn;
        }

        private ResultArgs MapCongregationLedgers()
        {
            resultArgs = new ResultArgs();
            try
            {
                if (lcgMapWithCongregation.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && gcLedger.DataSource as DataTable != null)
                {
                    if (ValidInput())
                    {
                        this.ShowWaitDialog();
                        double OpeningIEBalance = UtilityMember.NumberSet.ToDouble(txtOpeningIEBalance.Text);
                        Int32 OpeningIEBalanceMode = cboTransMode.SelectedIndex;
                        DataTable dtSelectedLedgers = gcLedger.DataSource as DataTable;
                        using (LedgerSystem ledsystem = new LedgerSystem())
                        {
                            resultArgs = ledsystem.MapLedgerWithCongregationLedgers(ConNature, ConLedgerId, dtSelectedLedgers);
                            if (resultArgs.Success)
                            {
                                using (UISetting uisetting = new UISetting())
                                {
                                    resultArgs = uisetting.SaveSettingDetails(FinanceSetting.GeneralateOpeningIEBalance.ToString(), OpeningIEBalance.ToString(), this.ADMIN_USER_DEFAULT_ID);
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = uisetting.SaveSettingDetails(FinanceSetting.GeneralateOpeningIEBalanceMode.ToString(), OpeningIEBalanceMode.ToString(), this.ADMIN_USER_DEFAULT_ID);
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = uisetting.FetchSettingDetails(this.ADMIN_USER_DEFAULT_ID);
                                            if (resultArgs.Success && resultArgs.DataSource.TableView != null && resultArgs.DataSource.TableView.Count != 0)
                                            {
                                                this.UIAppSetting.UISettingInfo = resultArgs.DataSource.TableView;
                                            }
                                        }
                                        //resultArgs.Success = true;
                                    }
                                }
                                ShowNoteDetails();
                                CloseWaitDialog();
                            }

                        }
                    }
                    else
                    {
                        resultArgs.Message = "Fill all the details";
                    }
                }
                else
                {
                    resultArgs.Success = true;
                }
            }
            catch (Exception err)
            {
                ShowMessageBoxError(err.Message);
                resultArgs.Message = err.Message;
            }
            finally
            {
                CloseWaitDialog();
            }
            return resultArgs;
        }
        #endregion

        private void btnFAUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ShowConfirmationMessage("Are you sure to update Fixed Asset & Depreciation details?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (gcCYFADepreciation.DataSource != null)
                    {
                        this.ShowWaitDialog();
                        //On 20/03/2023, To updtae FA and depreciaton detials for current year
                        using (LedgerSystem ledsystem = new LedgerSystem())
                        {
                            resultArgs = ledsystem.UpdateCongregationCurrentFADetails(gcCYFADepreciation.DataSource as DataTable);
                        }

                        if (resultArgs.Success)
                        {
                            this.ShowMessageBox("Fixed Asset and Depreciation details are updated for current finance year");
                        }
                        this.CloseWaitDialog();

                        if (FromReportModule)
                        {
                            this.Close();
                        }
                        else
                        {
                            BindLedgers();
                            BindFADetails();
                        }
                    }
                }
            }
            catch (Exception ef)
            {
                CloseWaitDialog();
                this.ShowMessageBoxError(ef.Message);
            }
        }

        private void btnMapLedgers_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ShowConfirmationMessage("Are you sure to update Generalate Ledgers Map Setting ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    resultArgs = MapCongregationLedgers();
                    if (resultArgs.Success)
                    {
                        this.ShowMessageBox("Selected Ledger(s) are mapped with '" + glkpGeneralateLedgers.Text + "'");
                    }
                    else
                    {
                        this.ShowMessageBox(resultArgs.Message);
                    }
                    BindLedgers();
                    BindFADetails();
                }
            }
            catch (Exception ef)
            {
                CloseWaitDialog();
                this.ShowMessageBoxError(ef.Message);
            }
        }

        private void btnShowMapLedgers_Click(object sender, EventArgs e)
        {
            lcgMapWithCongregation.Visibility = (lcgMapWithCongregation.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ?
                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            chkLedgerSelectAll.Visible = chkLedgerSelectAll.Visible ? false :true ;

            glkpGeneralateLedgers.EditValue = FixeAssetGenerlateLedgerId;
        }

        private void gcLedger_Click(object sender, EventArgs e)
        {

        }

        private void gcCYFADepreciation_Validating(object sender, CancelEventArgs e)
        {
               
        }

        private void gvCYFADepreciation_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colFAConCredit || e.Column == colFAConDebit)
            {
                double op = UtilityMember.NumberSet.ToDouble(gvCYFADepreciation.GetFocusedRowCellValue(colFAConOpAmount).ToString());
                double dr = UtilityMember.NumberSet.ToDouble(gvCYFADepreciation.GetFocusedRowCellValue(colFAConDebit).ToString());
                double cr = UtilityMember.NumberSet.ToDouble(gvCYFADepreciation.GetFocusedRowCellValue(colFAConCredit).ToString());
                gvCYFADepreciation.SetRowCellValue(e.RowHandle, colFAConCLBalance, (op + dr) - cr);
                gvCYFADepreciation.UpdateCurrentRow();

                ShowFADepreciationFooterSummary();
            }
        }

        

    }
}