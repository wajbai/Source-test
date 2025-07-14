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
using ACPP.Modules.Master;
using Bosco.Model.Transaction;
using ACPP.Modules;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel.Master;

namespace ACPP.Modules.Master
{
    public partial class frmLedgerOptions : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        public string CheckSelected = "SELECT";
        public string TempSelect = "SELECT_TMP";
        DialogResult LedgeroptionsDialogResult = DialogResult.Cancel;
        #endregion

        #region Constructor
        public frmLedgerOptions()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void frmLedgerOptions_Load(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void glkLedgerOptions_EditValueChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void chkLedgerSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtAllLedger = (DataTable)gcLedgerOption.DataSource;
            if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAllLedger.Rows)
                {
                    dr["SELECT"] = chkLedgerSelectAll.Checked;
                }
                gcLedgerOption.DataSource = dtAllLedger;
            }
        }

        private void frmLedgerOptions_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLedgerOption.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLedgerOption, colLedgerName);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidInput())
                {
                    int SelectedIndex = this.UtilityMember.NumberSet.ToInteger(glkLedgerOptions.EditValue.ToString());
                    this.ShowWaitDialog();
                    int ledgerId = 0;
                    int GstServiceType = 0;
                    int GStsId = 0;
                    this.ShowMessageBox("Option will not be disabled for already used Ledger(s) in Vouchers");
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        this.ShowWaitDialog("Enabling Ledger options");
                        DataView dvLedgerOption = ((gcLedgerOption.DataSource) as DataTable).AsDataView();
                        if (SelectedIndex == (int)LedgerOptions.EnableCostCenter)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionCostcentre();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT=1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    //costcentre = this.UtilityMember.NumberSet.ToInteger(dr[CheckSelected].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOption(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableHighValuePayments)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionHighValues();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT=1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    //costcentre = this.UtilityMember.NumberSet.ToInteger(dr[CheckSelected].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionHighValuePayment(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableLocalDonations)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionLocalDonation();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT=1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    //costcentre = this.UtilityMember.NumberSet.ToInteger(dr[CheckSelected].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionLocalDonations(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableBankFDInterestLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionForBankInterestZero();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT=1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    //costcentre = this.UtilityMember.NumberSet.ToInteger(dr[CheckSelected].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionForbankInterestOne(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableBankFDPenaltyLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionsBankFDPenaltyLedgersSetDisableAll();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT= 1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionsBankFDPenaltyLedgers(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableBankSBInterestLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionsBankSBInterestsetDisableAll();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT= 1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionsBankSBInterest(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableBankCommissionLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionsBankCommissionDisableAll();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT= 1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionsBankCommissionByLedger(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        //else if (SelectedIndex == (int)LedgerOptions.EnableInkindLedger)
                        //{
                        //    resultArgs = ledgersystem.UpdateLedgerOptionForInkindZero();
                        //    if (resultArgs.Success)
                        //    {
                        //        dvLedgerOption.RowFilter = "SELECT=1";
                        //        foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                        //        {
                        //            ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                        //            resultArgs = ledgersystem.UpdateLedgerOptionForInkindOne(ledgerId);
                        //        }
                        //    }
                        //    dvLedgerOption.RowFilter = "";
                        //}
                        else if (SelectedIndex == (int)LedgerOptions.EnableAssetGainLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionForGainLedgers();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT=1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionForGainLedgerOne(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableAssetLossLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionForLossLedgers();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT=1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionLossLedgerOne(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableDepreciationLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionForDepreciationZero();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT=1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionForDepreciationOne(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableAssetDisposalLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionForDisposalZero();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT = 1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionForDisposalOne(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableSubsidyLedger)
                        {
                            resultArgs = ledgersystem.UpdateLedgerOptionForSubsidyZero();
                            if (resultArgs.Success)
                            {
                                dvLedgerOption.RowFilter = "SELECT = 1";
                                foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                {
                                    ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                    resultArgs = ledgersystem.UpdateLedgerOptionForSubsidyOne(ledgerId);
                                }
                            }
                            dvLedgerOption.RowFilter = "";
                        }
                        else if (SelectedIndex == (int)LedgerOptions.EnableGSTLedger) // this is to save the gst classification for the Ledgers
                        {
                            if (glkpGstType.EditValue != null)
                            {
                                GstServiceType = (glkpGstType.EditValue.Equals(GSTType.Goods.ToString()) ? (int)GSTType.Goods : (int)GSTType.Services);
                            }

                            if (glkpGST.EditValue != null)
                            {
                                GStsId = glkpGST.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpGST.EditValue.ToString()) : 0;
                            }
                            if (GStsId > 0)
                            {
                                resultArgs = ledgersystem.UpdateLedgerOptionForGSTZero(GstServiceType, GStsId);
                                if (resultArgs.Success)
                                {
                                    dvLedgerOption.RowFilter = "SELECT = 1";
                                    foreach (DataRow dr in dvLedgerOption.ToTable().Rows)
                                    {
                                        ledgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                                        resultArgs = ledgersystem.UpdateLedgerOptionForGSTOne(ledgerId, GstServiceType, GStsId);
                                    }
                                }
                                dvLedgerOption.RowFilter = "";
                            }
                        }
                    }
                    if (resultArgs.Success)
                    {
                        BindData();
                        this.CloseWaitDialog();
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally
            {
                this.CloseWaitDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rchkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chked = (CheckEdit)sender;
            if (!chked.Checked)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    int LedgerId = UtilityMember.NumberSet.ToInteger(gvLedgerOption.GetRowCellValue(gvLedgerOption.FocusedRowHandle, colLedgerId.FieldName).ToString());
                    int SelectedIndex = this.UtilityMember.NumberSet.ToInteger(glkLedgerOptions.EditValue.ToString());

                    if (SelectedIndex == (int)LedgerOptions.EnableCostCenter)
                    {
                        resultArgs = ledgersystem.MadeTransactionByLedger(LedgerId.ToString());
                        //if row count is more zero than  transaction is made
                        if (resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            chked.Checked = true;
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
                        }
                    }
                    else if (SelectedIndex == (int)LedgerOptions.EnableBankFDInterestLedger || SelectedIndex == (int)LedgerOptions.EnableBankFDPenaltyLedger)
                    {
                        resultArgs = ledgersystem.MadeFDTransactionByLedger(LedgerId.ToString());
                        //if row count is more zero than  transaction is made
                        if (resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            chked.Checked = true;
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
                        }
                    }
                    //else if (SelectedIndex == (int)LedgerOptions.EnableInkindLedger || SelectedIndex == (int)LedgerOptions.EnableDepreciationLedger)
                    else if (SelectedIndex == (int)LedgerOptions.EnableDepreciationLedger)
                    {
                        resultArgs = ledgersystem.MadeTransactionByLedger(LedgerId.ToString());
                        //if row count is more zero than  transaction is made
                        if (resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            chked.Checked = true;
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
                        }
                    }
                }
            }
        }

        private void gvLedgerOption_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = gvLedgerOption.RowCount.ToString();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load Project Category
        /// </summary>
        private void LoadLedgerOptions()
        {
            LedgerOptions ledgerType = new LedgerOptions();
            DataView dvReceiptType = this.UtilityMember.EnumSet.GetEnumDataSource(ledgerType, Sorting.None);
            if (dvReceiptType.Count > 0)
            {
                DataTable dtLedOption = dvReceiptType.ToTable();
                string EnumValforCostcentre = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableCostCenter);
                string EnumValforBankFDInterestLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableBankFDInterestLedger);
                string EnumValforBankFDPenaltyLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableBankFDPenaltyLedger);
                string EnumValforBankSBInterestLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableBankSBInterestLedger);
                string EnumValforBankCommissionLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableBankCommissionLedger);

                //string EnumValforInKindLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableInkindLedger);
                string EnumValforDepreciationledger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableDepreciationLedger);
                string EnumValforAssetGainLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableAssetGainLedger);
                string EnumValforAssetLossLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableAssetLossLedger);
                string EnumValforAssetDisposalLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableAssetDisposalLedger);
                string EnumValforSubsidyLedger = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableSubsidyLedger);
                string EnumValforGSTLedgers = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableGSTLedger);
                string EnumValforHighValuePayment = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableHighValuePayments);
                string EnumValforLocalDonation = this.UtilityMember.EnumSet.GetDescriptionFromEnumValue(LedgerOptions.EnableLocalDonations);

                dtLedOption.Rows[(int)LedgerOptions.EnableCostCenter]["Name"] = EnumValforCostcentre;
                dtLedOption.Rows[(int)LedgerOptions.EnableBankFDInterestLedger]["Name"] = EnumValforBankFDInterestLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableBankFDPenaltyLedger]["Name"] = EnumValforBankFDPenaltyLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableBankSBInterestLedger]["Name"] = EnumValforBankSBInterestLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableBankCommissionLedger]["Name"] = EnumValforBankCommissionLedger;
                //dtLedOption.Rows[2]["Name"] = EnumValforInKindLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableDepreciationLedger]["Name"] = EnumValforDepreciationledger;
                dtLedOption.Rows[(int)LedgerOptions.EnableAssetGainLedger]["Name"] = EnumValforAssetGainLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableAssetLossLedger]["Name"] = EnumValforAssetLossLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableAssetDisposalLedger]["Name"] = EnumValforAssetDisposalLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableSubsidyLedger]["Name"] = EnumValforSubsidyLedger;
                dtLedOption.Rows[(int)LedgerOptions.EnableGSTLedger]["Name"] = EnumValforGSTLedgers;
                dtLedOption.Rows[(int)LedgerOptions.EnableHighValuePayments]["Name"] = EnumValforHighValuePayment;
                dtLedOption.Rows[(int)LedgerOptions.EnableLocalDonations]["Name"] = EnumValforLocalDonation;

                //On 03/07/2019, to skip GST classificaton option when GST is disabled in setting
                if (AppSetting.EnableGST == "0")
                {
                    dtLedOption.DefaultView.RowFilter = "Id <> " + (int)LedgerOptions.EnableGSTLedger;
                    dtLedOption = dtLedOption.DefaultView.ToTable();
                }

                glkLedgerOptions.Properties.DataSource = dtLedOption;
                glkLedgerOptions.Properties.DisplayMember = "Name";
                glkLedgerOptions.Properties.ValueMember = "Id";
                glkLedgerOptions.EditValue = 0;

                glkLedgerOptions.Properties.ImmediatePopup = true;
                glkLedgerOptions.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                glkLedgerOptions.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;

            }
        }
        /// <summary>
        /// Bind Divison to lookup edit contrls
        /// </summary>
        private void BindCostcentreLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeCostCentre();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
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
        /// Bind Divison to lookup High Values edit contrls
        /// </summary>
        private void BindHighValueLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeHighValuePayments();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
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
        /// Bind Divison to lookup High Values edit contrls
        /// </summary>
        private void BindLocalDonationLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeLocalDonations();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
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
        /// Bind Divison to lookup edit contrls
        /// </summary>
        private void BindBankFDInterestLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeBankInterestLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void BindBankFDPenaltyLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeBankFDPenaltyLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void BindBankSBInterestLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeBankSBInterestLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void BindBankCommissionLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeBankCommissionLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        #region InKind Options
        /// <summary>
        /// Bind Divison to lookup edit contrls
        /// </summary>
        private void BindInKindLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeInKindLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        #region Gain Ledger Options
        /// <summary>
        /// Bind Divison to lookup edit contrls
        /// </summary>
        private void BindAssetGainLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByEnableAssetGainLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        #region Loss Ledger Options
        /// <summary>
        /// Bind Divison to lookup edit contrls
        /// </summary>
        private void BindAssetLossLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByEnableAssetLossLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void BindAssetDiposalLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByEnableSubsidyLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void BindSubsidyLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByEnableSubsidyLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
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
        /// Bind the gst values 
        /// </summary>
        private void BindGSTLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeGSTDetails();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;

                        Int32 isGSTType = (glkpGstType.EditValue == null || glkpGstType.EditValue.Equals(GSTType.Goods.ToString()) ? (int)GSTType.Goods : (int)GSTType.Services);
                        Int32 GSTClasss = glkpGST.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpGST.EditValue.ToString()) : 0;

                        dtLedgers.DefaultView.RowFilter = "(SELECT_TMP=0 OR (GST_SERVICE_TYPE=" + isGSTType + " AND GST_ID = " + GSTClasss + "))";
                        dtLedgers = dtLedgers.DefaultView.ToTable();


                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        #endregion

        #region Depreciation Options
        /// <summary>
        /// Bind Divison to lookup edit contrls
        /// </summary>
        private void BindDepreciationLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchLedgerByIncludeDepreciationLedger();
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtLedgers = resultArgs.DataSource.Table;
                        dtLedgers.Columns.Add(CheckSelected, typeof(Int32));
                        foreach (DataRow dr in dtLedgers.Rows)
                        {
                            dr[CheckSelected] = dr[TempSelect];
                        }
                        DataView dv = new DataView(AddColumns(dtLedgers));
                        dv.Sort = CheckSelected + " DESC";
                        gcLedgerOption.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion

        private DataTable AddColumns(DataTable dtLedgers)
        {
            DataTable dtAddedledger = dtLedgers;
            if (!dtAddedledger.Columns.Contains(CheckSelected))
            {
                dtAddedledger.Columns.Add(CheckSelected, typeof(int));
            }
            return dtAddedledger;
        }

        private void SetDefaults()
        {
            LoadLedgerOptions();
            chkLedgerSelectAll.Checked = false;
        }

        /// <summary>
        /// Validating the User Input
        /// </summary>
        /// <returns></returns>
        public bool IsValidInput()
        {
            bool isValid = true;
            int SelectedIndex = this.UtilityMember.NumberSet.ToInteger(glkLedgerOptions.EditValue.ToString());
            if (gvLedgerOption.DataRowCount == 0)
            {
                this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Master.CostCentre.COST_CENTER_LEDGER_OPTION_FAILURE));
                isValid = false;
            }
            else if (SelectedIndex == (int)LedgerOptions.EnableGSTLedger) // for gst
            {
                Int32 gstId = glkpGST.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpGST.EditValue.ToString()) : 0;
                if (gstId == 0)
                {
                    isValid = false;
                    this.ShowMessageBoxWarning("Select GST class for '" + glkpGstType.EditValue.ToString() + "'");
                    glkpGST.Focus();
                }
            }
            return isValid;
        }

        /// <summary>
        /// Load the Gst Type List
        /// </summary>
        private void FetchGSTType()
        {
            try
            {
                GSTType gstTypes = new GSTType();
                DataView dvGSTTypes = this.UtilityMember.EnumSet.GetEnumDataSource(gstTypes, Sorting.None);
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGstType, dvGSTTypes.ToTable(), "Name", "Name");
                glkpGstType.EditValue = glkpGstType.Properties.GetKeyValue(0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void FetchGstList()
        {
            using (GSTClassSystem GstClass = new GSTClassSystem())
            {
                glkpGST.EditValue = null;
                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGST, GstClass.FetchGSTList(), "GST_NAME", "GST_Id");
                //glkpGST.EditValue = glkpGST.Properties.GetKeyValue(0);
            }
        }

        private void BindData()
        {
            int selectedId = this.UtilityMember.NumberSet.ToInteger(glkLedgerOptions.EditValue.ToString());

            lcgGSTGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            chkLedgerSelectAll.Top = gcLedgerOption.Top;

            if (selectedId == (int)LedgerOptions.EnableCostCenter)
            {
                BindCostcentreLedgers();
            }
            if (selectedId == (int)LedgerOptions.EnableHighValuePayments)
            {
                BindHighValueLedgers();
            }
            if (selectedId == (int)LedgerOptions.EnableLocalDonations)
            {
                BindLocalDonationLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableBankFDInterestLedger)
            {
                BindBankFDInterestLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableBankFDPenaltyLedger)
            {
                BindBankFDPenaltyLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableBankSBInterestLedger)
            {
                BindBankSBInterestLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableBankCommissionLedger)
            {
                BindBankCommissionLedgers();
            }
            //else if (selectedId == (int)LedgerOptions.EnableInkindLedger)
            //{
            //    BindInKindLedgers();
            //}
            else if (selectedId == (int)LedgerOptions.EnableDepreciationLedger)
            {
                BindDepreciationLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableAssetGainLedger)
            {
                BindAssetGainLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableAssetLossLedger)
            {
                BindAssetLossLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableAssetDisposalLedger)
            {
                BindAssetDiposalLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableSubsidyLedger)
            {
                BindAssetDiposalLedgers();
            }
            else if (selectedId == (int)LedgerOptions.EnableGSTLedger) // this is to load the GST option
            {
                lcgGSTGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                chkLedgerSelectAll.Top = gcLedgerOption.Top;
                BindGSTLedgers();
                FetchGSTType();
                FetchGstList();
            }
        }
        #endregion

        /// <summary>
        /// this is to get the Gst Type Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpGstType_EditValueChanged(object sender, EventArgs e)
        {
            BindGSTLedgers();
        }

        /// <summary>
        ///  this is get the Gst Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpGST_EditValueChanged(object sender, EventArgs e)
        {
            BindGSTLedgers();
        }
    }
}