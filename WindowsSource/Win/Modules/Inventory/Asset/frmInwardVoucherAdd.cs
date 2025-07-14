using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using Bosco.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ACPP.Modules.Inventory;
using Bosco.Model.Transaction;
using ACPP.Modules.Transaction;
using Bosco.Model.Inventory;
using ACPP.Modules.Inventory.Asset;
using DevExpress.XtraEditors.Repository;
using ACPP.Modules.Master;
using Bosco.Model.UIModel.Master;
using Bosco.Utility.ConfigSetting;
using System.Drawing;
using AcMEDSync.Model;
using DevExpress.XtraGrid.Views.Base;
using System.Collections.Generic;
using DevExpress.XtraBars;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Bosco.Report.Base;

namespace ACPP.Modules.Asset
{
    public partial class frmInwardVoucherAdd : frmFinanceBaseAdd
    {

        #region Declaration
        public event EventHandler UpdateHeld;
        ResultArgs resultArgs = new ResultArgs();
        int InwardId = 0;
        int cashLedgerId = 0;
        private const string LEDGER_ID = "LEDGER_ID";
        private const string ITEM_ID = "ITEM_ID";
        private const string QUANTITY = "QUANTITY";
        private const string AMOUNT = "AMOUNT";
        private const string SALVAGEVALUE = "SALVAGE_VALUE";
        private const string CUSTODIANS_ID = "CUSTODIANS_ID";
        bool isMouseClicked = false;
        public bool isvalidQty = true;
        private DataSet dsCostCentre = new DataSet();
        #endregion

        #region Properties

        private int InOutId { get; set; }

        public int VoucherId { get; set; }

        private AssetInOut Flag { get; set; }

        public int ProjectId { get; set; }

        public int AssetTempQuantity { get; set; }

        public string ProjectName { get; set; }

        private string RecentVoucherDate { get; set; }

        private DataTable dtAssetItem { get; set; }

        private int transVoucherMethod = 0;
        private int TransVoucherMethod
        {
            set
            {
                transVoucherMethod = value;
            }
            get
            {
                return transVoucherMethod;
            }
        }

        int Aid = 0;
        public int AccLedgerId  // For Costcentre usage
        {
            get
            {
                return Aid;
            }
            set { Aid = value; }
        }

        double ledgerccamount;
        private double LedgerCCAmount
        {
            get
            {
                return ledgerccamount;
            }
            set
            {
                ledgerccamount = value;
            }
        }


        //private int CashBankGroupId
        //{
        //    get
        //    {
        //        int GroupId = 0;
        //        if (CashLedgerId > 0)
        //        {
        //            DataRowView dv = rglkpCashBankLedgers.GetRowByKeyValue(CashLedgerId) as DataRowView;
        //            if (dv != null)
        //                GroupId = this.UtilityMember.NumberSet.ToInteger(dv.Row["Group_ID"].ToString());
        //        }
        //        return GroupId;
        //    }
        //}

        private int AccountLedgerId
        {
            get
            {
                int ledgerId = 0;
                if (ItemId > 0)
                {
                    DataRowView dv = rglkpAssetName.GetRowByKeyValue(ItemId) as DataRowView;
                    if (dv != null)
                        ledgerId = this.UtilityMember.NumberSet.ToInteger(dv.Row["ACCOUNT_LEDGER_ID"].ToString());
                }
                return ledgerId;
            }
        }

        //private int CashLedgerId
        //{
        //    get
        //    {
        //        cashLedgerId = gvCashBank.GetRowCellValue(gvCashBank.FocusedRowHandle, colCashLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvCashBank.GetRowCellValue(gvCashBank.FocusedRowHandle, colCashLedgerId).ToString()) : 0;
        //        return cashLedgerId;
        //    }
        //}

        public int ItemId
        {
            get { return gvPurchase.GetFocusedRowCellValue(colAssItemID) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colAssetName).ToString()) : 0; }
        }

        public int LedgerId
        {
            get
            {
                int id = 0;
                id = gvPurchase.GetRowCellValue(gvPurchase.FocusedRowHandle, colAccountLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetRowCellValue(gvPurchase.FocusedRowHandle, colAccountLedgerId).ToString()) : 0;
                return id;
            }
        }

        public int quantiry = 0;
        public int Quantity
        {
            get
            {
                quantiry = 0;
                quantiry = gvPurchase.GetFocusedRowCellValue(colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colQuantity).ToString()) : 0;
                return quantiry;
            }
            set { quantiry = value; }

        }

        public int InOutDetailId
        {
            get { return gvPurchase.GetFocusedRowCellValue(colInoutDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colInoutDetailId).ToString()) : 0; }
        }

        private double AmountSummaryVal
        {
            get { return colAmount.SummaryItem.SummaryValue != null ? this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()) : 0; }
        }

        private double BankTransSummaryVal
        {
            get { return ucAssetJournal1.BankTransSummaryVal; }
        }

        //private double CashLedgerAmount
        //{
        //    get
        //    {
        //        double cashLedgerAmount;
        //        cashLedgerAmount =ucAssetJournal1.LedgerAmount;
        //        return cashLedgerAmount;
        //    }
        //}

        private double LedgerAmount
        {
            get
            {
                double ledgerAmount;
                ledgerAmount = gvPurchase.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPurchase.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return ledgerAmount;
            }
        }

        private int contributionId = 0;
        private int ContributionId
        {
            set { contributionId = value; }
            get { return contributionId; }
        }

        public int tmpQuantity { get; set; }

        public string NameAddress { get; set; }

        public int AvailQty { get; set; }

        #endregion

        #region Constructors
        public frmInwardVoucherAdd()
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width,
                            Screen.PrimaryScreen.WorkingArea.Height);
            gvPurchase.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //gvCashBank.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            //RealColumnEditTransAmount();
            RealColumnEditQuantity();
            //RealColumnEditCashTransAmount();
        }
        public frmInwardVoucherAdd(string recentVoucherDate, int projectId, string projectName, int inwardId, AssetInOut Flag)
            : this()
        {
            this.RecentVoucherDate = recentVoucherDate;
            this.ProjectId = projectId;
            this.ProjectName = projectName;
            this.InwardId = inwardId;
            //this.Flag = Flag;
            this.Flag = Flag == AssetInOut.PU ? AssetInOut.PU : AssetInOut.IK;
            ucAssetJournal1.ProjectId = this.ProjectId;
            ucAssetJournal1.MinDate = this.RecentVoucherDate;
            ucAssetJournal1.NextFocusControl = txtNarration;
            ucAssetJournal1.BeforeFocusControl = gcPurchase;
            ucAssetJournal1.Flag = Flag;
            ucAssetJournal1.PurchaseTransSummary = this.AmountSummaryVal;
            //RealColumnEditBankAmount();
        }
        #endregion

        #region Events

        #region Purchase grid

        private void gcPurchase_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                //   && (gvOPBalance.FocusedColumn == colQuantity))
                {
                    gvPurchase.PostEditor();
                    gvPurchase.UpdateCurrentRow();
                    gvPurchase.SetFocusedRowCellValue(colLedgerId, this.AccountLedgerId);
                    AccLedgerId = this.AccountLedgerId;
                    string salvagevalue = gvPurchase.GetFocusedRowCellValue(colSalvagevalue) != null ? gvPurchase.GetFocusedRowCellValue(colSalvagevalue).ToString() : string.Empty;
                    //int item = this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colAssItemID) != null ? gvPurchase.GetFocusedRowCellValue(colAssItemID).ToString() : string.Empty);
                    string AssetName = gvPurchase.GetFocusedRowCellValue(colAssItemID) != null ? gvPurchase.GetFocusedRowCellValue(colAssetName).ToString() : string.Empty;
                    if (gvPurchase.FocusedColumn == colAssetName)
                    {
                        gvPurchase.FocusedColumn = gvPurchase.Columns.ColumnByName(colAssetName.Name);
                        gvPurchase.ShowEditor();
                        AvailQty = BindAvailQty();
                        gvPurchase.SetFocusedRowCellValue(colAvailableQty, AvailQty);
                    }
                    GenerationList();
                    if (!string.IsNullOrEmpty(AssetName) && this.Quantity > 0)
                    {
                        ShowCostCentre(LedgerCCAmount, false);

                        //if (salvagevalue == string.Empty)
                        //{
                        //    gvPurchase.Focus();
                        //    gvPurchase.FocusedColumn = colSalvagevalue;
                        //}
                        //else 
                        if (gvPurchase.IsLastRow)
                        {
                            DataTable dtCashTransaction = gcPurchase.DataSource as DataTable;
                            dtCashTransaction.Rows.Add(dtCashTransaction.NewRow());
                            gcPurchase.DataSource = dtCashTransaction;
                            gvPurchase.MoveNext();
                            int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.By : (int)Source.To;
                            gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colSource, sourceId);
                            gvPurchase.FocusedColumn = colAssetName;
                            gvPurchase.ShowEditor();
                        }
                        //else if (gvPurchase.FocusedColumn == colSalvagevalue)
                        //{
                        //    int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.By : (int)Source.To;
                        //    gvPurchase.MoveNext();
                        //    gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colSource, sourceId);
                        //    gvPurchase.FocusedColumn = colAssetName;
                        //}
                        int Id = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.By : (int)Source.To;
                        gvPurchase.MoveNext();
                        gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colSource, Id);
                        gvPurchase.FocusedColumn = colAssetName;
                        gvPurchase.FocusedColumn = gvPurchase.Columns["0"];//sudhakar
                    }
                    if (gvPurchase.FocusedColumn == colSalvagevalue && !string.IsNullOrEmpty(AssetName) && this.Quantity > 0 && !string.IsNullOrEmpty(salvagevalue))
                    {
                        gvPurchase.CloseEditor();
                        FocusCashTransactionGrid();
                    }
                    if (string.IsNullOrEmpty(AssetName))
                    {
                        gvPurchase.CloseEditor();
                        FocusCashTransactionGrid();
                    }
                }
                else if (gvPurchase.IsFirstRow && gvPurchase.FocusedColumn == colAssetName && e.Shift && e.KeyCode == Keys.Tab)
                {
                    if (glkpPurpose.Visible && glkpPurpose.Enabled)
                    {
                        glkpPurpose.Select();
                        glkpPurpose.Focus();
                    }
                    else if (glkpDonor.Enabled && glkpDonor.Visible)
                    {
                        glkpDonor.Select();
                        glkpDonor.Focus();
                    }
                    else
                    {
                        glkVendor.Select();
                        glkVendor.Focus();
                    }
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// Onm 04/09/2024, To check currency based voucher details
        /// </summary>
        private bool IsCurrencyEnabledVoucher
        {
            get
            {
                Int32 currencycountry = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                double currencyamt = UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text);
                double exchagnerate = UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                double liveexchangerate = UtilityMember.NumberSet.ToDouble(lblAvgRate.Text);
                double actalamount = UtilityMember.NumberSet.ToDouble(txtActualAmt.Text);

                return (currencycountry > 0 && currencyamt > 0 && exchagnerate > 0 && actalamount > 0 && liveexchangerate > 0);
            }
        }
        private void GenerationList()
        {
            string Amount = string.Empty;
            string AssetName = string.Empty;
            string salvagevalue = string.Empty;
            //string Quantity = string.Empty;
            if (gvPurchase.FocusedColumn == colQuantity || gvPurchase.FocusedColumn == colViewDetails)
            {
                gvPurchase.FocusedColumn = gvPurchase.Columns.ColumnByName(colQuantity.Name);
                gvPurchase.ShowEditor();
                int rowid = gvPurchase.GetFocusedDataSourceRowIndex();
                DataRow dr;
                DataTable dtFilterdAsset = new DataTable();
                DataView dvAssetItem = new DataView(dtAssetItem);
                AssetName = gvPurchase.GetFocusedRowCellValue(colAssItemID) != null ? gvPurchase.GetFocusedRowCellValue(colAssetName).ToString() : string.Empty;
                Amount = gvPurchase.GetFocusedRowCellValue(colAmount) != null ? gvPurchase.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;
                salvagevalue = gvPurchase.GetFocusedRowCellValue(colSalvagevalue) != null ? gvPurchase.GetFocusedRowCellValue(colSalvagevalue).ToString() : string.Empty;

                if (!string.IsNullOrEmpty(gvPurchase.GetFocusedRowCellValue(colAssItemID).ToString())
                    && !string.IsNullOrEmpty(gvPurchase.GetFocusedRowCellValue(colQuantity).ToString()))
                {
                    gvPurchase.CloseEditor();
                    dvAssetItem.RowFilter = "ITEM_ID=" + ItemId;
                    dtFilterdAsset = dvAssetItem.ToTable();
                    if (dtFilterdAsset != null && dtFilterdAsset.Rows.Count > 0)
                    {
                        dr = dtFilterdAsset.Rows[0];
                        if (Quantity > 0)
                        {
                            frmAssetItemList AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowid, InOutDetailId, this.Flag, ProjectId, ProjectName);
                            AssetItemList.ShowDialog();
                            if (AssetItemList.Dialogresult == DialogResult.OK)
                            {
                                if (AssetItemList.Quantity > 0)
                                {
                                    gvPurchase.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
                                    tmpQuantity = AssetItemList.Quantity;
                                    int PrvQty = gvPurchase.GetRowCellValue(gvPurchase.FocusedRowHandle, colQuantity) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetRowCellValue(gvPurchase.FocusedRowHandle, colQuantity).ToString()) : 0;
                                }
                                else if (AssetItemList.Quantity == 0)
                                {
                                    gvPurchase.SetFocusedRowCellValue(colQuantity, AssetItemList.Quantity);
                                    gvPurchase.SetFocusedRowCellValue(colAmount, AssetItemList.Amount);
                                }
                                decimal amount = AssetItemList.Amount;
                                LedgerCCAmount = UtilityMember.NumberSet.ToDouble(amount.ToString());  // for costcentre purpose
                                if (amount > 0)
                                {
                                    gvPurchase.SetFocusedRowCellValue(colAmount, amount.ToString());
                                    gvPurchase.UpdateTotalSummary();
                                    CalculateFirstRowValue();
                                    gvPurchase.FocusedColumn = gvPurchase.Columns["0"];//sudhakar
                                }
                            }
                            else
                            {
                                if (InwardId == 0)
                                {
                                    gvPurchase.SetFocusedRowCellValue(colQuantity, tmpQuantity);
                                    gvPurchase.SetFocusedRowCellValue(colAvailableQty, BindAvailQty());
                                }
                                else
                                {
                                    gvPurchase.SetFocusedRowCellValue(colQuantity, tmpQuantity);
                                    gvPurchase.SetFocusedRowCellValue(colAvailableQty, BindAvailQty());
                                }
                                if (AssetItemList.Amount == 0 && SettingProperty.AssetListCollection.ContainsKey(rowid))
                                    SettingProperty.AssetListCollection.Remove(rowid);
                            }
                            gvPurchase.FocusedColumn = colSalvagevalue;
                        }
                    }
                }
            }

            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                txtCurrencyAmount.Text = ucAssetJournal1.BankTransSummaryVal.ToString();
                Double ActualValues = this.UtilityMember.NumberSet.ToDouble(ucAssetJournal1.BankTransSummaryVal.ToString()) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                lblCalAmt.Text = this.UtilityMember.NumberSet.ToNumber(ActualValues).ToString();
                txtActualAmt.Text = this.UtilityMember.NumberSet.ToNumber(ActualValues).ToString();
            }
        }

        private void gvPurchase_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ITEM_ID" && IsDuplicatedValue((sender as GridView), e.Column, e.Value))
            {
                (sender as GridView).CancelUpdateCurrentRow();
            }
        }

        private void gcPurchasePrticulars_Enter(object sender, EventArgs e)
        {
            gvPurchase.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcPurchasePrticulars_valid(object sender, EventArgs e)
        {
            gvPurchase.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void gvPurchase_GotFocus(object sender, EventArgs e)
        {
            BaseView view = sender as BaseView;
            if (view == null)
                return;

            if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
                return;
            view.ShowEditor();
            TextEdit editor = view.ActiveEditor as TextEdit;
            if (editor != null)
            {
                editor.SelectAll();
                editor.Focus();
            }
        }

        private void RealColumnEditTransAmount()
        {
            colQuantity.RealColumnEdit.Leave += new System.EventHandler(RealColumnEditPurchaseAmount_EditValueChanged);
            this.gvPurchase.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchase.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchase.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditQuantity()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditQuantity_EditValueChanged);
            this.gvPurchase.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurchase.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurchase.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditPurchaseAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurchase.PostEditor();
            gvPurchase.UpdateCurrentRow();
            if (gvPurchase.ActiveEditor == null)
            {
                gvPurchase.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

            if (rgVoucherType.SelectedIndex == 0)
                CalculateFirstRowValue();
        }

        void RealColumnEditQuantity_EditValueChanged(object sender, System.EventArgs e)
        {
            int TempcalculateQty = 0;
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurchase.PostEditor();
            gvPurchase.UpdateCurrentRow();
            if (gvPurchase.ActiveEditor == null)
            {
                gvPurchase.ShowEditor();
            }

            AvailQty = TempcalculateQty = BindAvailQty();
            if (InwardId == 0)
            {
                if (AvailQty == 0)
                {
                    gvPurchase.SetFocusedRowCellValue(colAvailableQty, Quantity);
                }
                else
                {
                    AvailQty = AvailQty - tmpQuantity;
                    AvailQty = AvailQty + Quantity;
                    gvPurchase.SetFocusedRowCellValue(colAvailableQty, AvailQty == 0 ? TempcalculateQty : AvailQty);
                }
            }
            else
            {
                AvailQty = AvailQty - tmpQuantity;
                AvailQty = AvailQty + Quantity;
                gvPurchase.SetFocusedRowCellValue(colAvailableQty, AvailQty == 0 ? TempcalculateQty : AvailQty);
            }
            if (rgVoucherType.SelectedIndex == 0)
                CalculateFirstRowValue();
        }

        private void rbtnViewDetails_Click(object sender, EventArgs e)
        {
            GenerationList();
        }

        private void rtxtAmount_Validating(object sender, CancelEventArgs e)
        {
            //if (IsValidaTransactionRow())
            //{
            //    //        BalanceProperty Balance = FetchCurrentBalance(LedgerId);
            //    //        if (dAmt < (Balance.Amount + LedgerAmount))
            //    //        {
            //    //            gvPurchase.FocusedColumn = colAssName;
            //    //            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.TRANSACTION_AMOUNT_EXCEEDS));
            //    //        }
            //}
        }

        private void rtxtAmount_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal amount = this.UtilityMember.NumberSet.ToDecimal(gvPurchase.GetFocusedRowCellValue(colAmount).ToString());
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }

        private void rglkpAssetName_EditValueChanged(object sender, EventArgs e)
        {
            //To retain the Asset item in  the purchasegrid Grid
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}");
                isMouseClicked = false;
            }
        }

        private void rtxtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            //int rowid = gvPurchase.GetVisibleIndex(gvPurchase.FocusedRowHandle);
            //if (Quantity > 0)
            //{
            //    frmAssetItemList AssetItemList = new frmAssetItemList(ItemId, Quantity, rowid, InOutDetailId, rgVoucherType.SelectedIndex == 0 ? AssetInOut.PU : AssetInOut.IK);
            //    AssetItemList.ShowDialog();
            //}
            //SendKeys.Send("{tab}");
        }

        private void rglkpAssetName_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        #endregion

        #region Bank Grid

        //private void gcCashBank_ProcessGridKey(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        string amount = gvCashBank.GetFocusedRowCellValue(colBankAmount).ToString();
        //        bool canFocusOtherCharges = false;
        //        if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
        //            && !e.Shift && !e.Alt && !e.Control
        //            && (gvCashBank.FocusedColumn == colBankAmount || gvCashBank.FocusedColumn == colMaterializedOn))//&& (gvBank.IsLastRow))
        //        {
        //            if (CashLedgerId == 0 && CashLedgerAmount == 0) { canFocusOtherCharges = true; }
        //            if (BankTransSummaryVal == AmountSummaryVal)
        //            {
        //                if ((gvCashBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
        //                    || (gvCashBank.FocusedColumn == colBankAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash))
        //                {
        //                    if (gvCashBank.IsLastRow && !string.IsNullOrEmpty(amount))
        //                    {
        //                        gvCashBank.PostEditor();
        //                        gvCashBank.UpdateCurrentRow();
        //                        gvCashBank.AddNewRow();
        //                        gvCashBank.MoveNext();
        //                        gvCashBank.FocusedColumn = colCashBank;
        //                    }
        //                    //else { canFocusOtherCharges = true; }
        //                }
        //            }
        //            else if ((gvCashBank.FocusedColumn == colMaterializedOn && CashBankGroupId == (int)FixedLedgerGroup.BankAccounts)
        //                    || (gvCashBank.FocusedColumn == colBankAmount && CashBankGroupId == (int)FixedLedgerGroup.Cash))
        //            {
        //                if (gvCashBank.IsLastRow && !string.IsNullOrEmpty(amount))
        //                {
        //                    gvCashBank.PostEditor();
        //                    gvCashBank.UpdateCurrentRow();
        //                    gvCashBank.AddNewRow();
        //                    gvCashBank.MoveNext();
        //                    gvCashBank.FocusedColumn = colCashBank;// { CashBankTransGridNewItem = TransEntryMethod; }
        //                }
        //                else
        //                {
        //                    gvCashBank.MoveNext();
        //                    gvCashBank.FocusedColumn = colCashBank;
        //                }
        //                if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
        //                {
        //                    e.SuppressKeyPress = true;
        //                }
        //            }
        //            if (canFocusOtherCharges)
        //            {
        //                gvCashBank.CloseEditor();
        //                e.Handled = true;
        //                e.SuppressKeyPress = true;

        //                txtOtherCharges.Select();
        //                txtOtherCharges.Focus();
        //            }
        //        }
        //        else if (gvCashBank.IsFirstRow && gvCashBank.FocusedColumn == colCashBank && e.Shift && e.KeyCode == Keys.Tab)
        //        {
        //            gcPurchase.Select();
        //            gcPurchase.Focus();
        //            gvPurchase.FocusedColumn = colQuantity;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //    }
        //    finally { }
        //}

        //private void gvCashBank_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        //{
        //    try
        //    {
        //        if (gvCashBank.GetRowCellValue(e.RowHandle, colCashBank) != null)
        //        {
        //            int GroupId = 0;
        //            int LedgerId = gvCashBank.GetRowCellValue(e.RowHandle, colCashLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvCashBank.GetRowCellValue(e.RowHandle, colCashLedgerId).ToString()) : 0;
        //            if (LedgerId > 0)
        //            {
        //                DataRowView drvLedger = rglkpCashBankLedgers.GetRowByKeyValue(LedgerId) as DataRowView;
        //                if (drvLedger != null)
        //                {
        //                    GroupId = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString());  //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
        //                }

        //                if ((e.Column == colRefNo || e.Column == colMaterializedOn) &&
        //                (GroupId == (int)FixedLedgerGroup.Cash))
        //                {
        //                    e.Appearance.BackColor = Color.LightGray;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString());
        //    }
        //    finally { }
        //}

        //private void gvCashBank_GotFocus(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        BaseView view = sender as BaseView;
        //        if (view == null)
        //            return;

        //        if (MouseButtons == System.Windows.Forms.MouseButtons.Left)
        //            return;
        //        view.ShowEditor();
        //        TextEdit editor = view.ActiveEditor as TextEdit;
        //        if (editor != null)
        //        {
        //            editor.SelectAll();
        //            editor.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessageBox(ex.Message);
        //    }
        //}

        //private void gvCashBank_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        if (CashLedgerId > 0)
        //        {
        //            if ((CashBankGroupId == (int)FixedLedgerGroup.Cash)
        //                && (gvCashBank.FocusedColumn == colRefNo || gvCashBank.FocusedColumn == colMaterializedOn))
        //            {
        //                e.Cancel = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString());
        //    }
        //    finally { }
        //}

        //private void RealColumnEditCashTransAmount()
        //{
        //    colBankAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditCashTransAmount_EditValueChanged);
        //    this.gvCashBank.MouseDown += (object sender, MouseEventArgs e) =>
        //    {
        //        GridHitInfo hitInfo = gvCashBank.CalcHitInfo(e.Location);
        //        if (hitInfo.Column != null && hitInfo.Column == colBankAmount)
        //        {
        //            this.BeginInvoke(new MethodInvoker(delegate
        //            {
        //                gvCashBank.ShowEditorByMouse();
        //            }));
        //        }
        //    };
        //}

        //void RealColumnEditCashTransAmount_EditValueChanged(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        BaseEdit edit = sender as BaseEdit;
        //        if (edit.EditValue == null) return;
        //        gvCashBank.PostEditor();
        //        gvCashBank.UpdateCurrentRow();
        //        if (gvCashBank.ActiveEditor == null)
        //            gvCashBank.ShowEditor();

        //        TextEdit txtTransAmount = edit as TextEdit;
        //        int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
        //        if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
        //            txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);

        //        if (CashLedgerId > 0)
        //        {
        //            DataTable dtCashTrans = gcCashBank.DataSource as DataTable;
        //            string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
        //            if (Balance != string.Empty)
        //            {
        //                gvCashBank.SetRowCellValue(gvCashBank.FocusedRowHandle, colBankCurrentBalance, Balance);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessageBox(ex.Message);
        //    }

        //}

        private void rbtnBankDelete_Click(object sender, EventArgs e)
        {
            //DeleteTransaction();
        }

        //private void rglkpCashBankLedgers_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (gvCashBank.IsFirstRow)
        //        {
        //            CalculateFirstRowValue();
        //        }
        //        if (this.CashLedgerId > 0)
        //        {
        //            if (VoucherId == 0)
        //            {
        //                gvCashBank.PostEditor();
        //                gvCashBank.UpdateCurrentRow();
        //                DataTable dtCashTrans = gcCashBank.DataSource as DataTable;
        //                string Balance = GetLedgerBalanceValues(dtCashTrans, CashLedgerId); //ShowLedgerBalance(CashLedgerId, dtCashTrans, false);
        //                if (Balance != string.Empty)
        //                {
        //                    gvCashBank.SetRowCellValue(gvCashBank.FocusedRowHandle, colBankCurrentBalance, Balance);
        //                }
        //            }

        //        }
        //        else { gvCashBank.UpdateCurrentRow(); }

        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessageBox(ex.Message);
        //    }
        //}

        //private void rglkpCashBankLedgers_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
        //        int Group = 0;
        //        if (gridLKPEdit.EditValue != null)
        //        {
        //            int LedgerID = this.UtilityMember.NumberSet.ToInteger(gridLKPEdit.EditValue.ToString());

        //            DataRowView drvLedger = rglkpCashBankLedgers.GetRowByKeyValue(LedgerID) as DataRowView;
        //            if (drvLedger != null)
        //            {
        //                Group = this.UtilityMember.NumberSet.ToInteger(drvLedger["GROUP_ID"].ToString()); //CashBankGroupId;//ledgerSystem.FetchLedgerGroupById();
        //            }

        //            gvCashBank.SetFocusedRowCellValue(colCashBank, LedgerID);

        //            if (AmountSummaryVal > 0 && BankTransSummaryVal <= AmountSummaryVal && CashLedgerAmount < 1)
        //            {
        //                double Amt = AmountSummaryVal - BankTransSummaryVal;
        //                gvCashBank.SetFocusedRowCellValue(colBankAmount, Amt.ToString());
        //                gvCashBank.SetFocusedRowCellValue(colCashBank, LedgerID);
        //                gvCashBank.PostEditor();
        //                gvCashBank.UpdateCurrentRow();
        //            }

        //            EnableCashBankFields();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessageBox(ex.Message);
        //    }
        //}

        private void rglkpCashBankLedgers_EditValueChanged(object sender, EventArgs e)
        {
            //To retain the ledger in  the Cash/Bank Grid
            //if (isMouseClicked)
            //{
            //    SendKeys.Send("{tab}"); isMouseClicked = false;
            //}
        }

        private void rglkpCashBankLedgers_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        #endregion

        #region User Control

        private void ucAssetVoucherShortcut_VendorClicked(object sender, EventArgs e)
        {
            frmVendorInfoAdd vendorAdd = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
            vendorAdd.ShowDialog();
            LoadVendor();
        }

        private void ucAssetVoucherShortcut_ManufacturerClicked(object sender, EventArgs e)
        {
            frmVendorInfoAdd Manufacturer = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
            Manufacturer.ShowDialog();
        }

        private void ucAssetVoucherShortcut_CustodianClicked(object sender, EventArgs e)
        {
            frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
            custodianAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcut_LocationClicked(object sender, EventArgs e)
        {
            frmLocationsAdd locationAdd = new frmLocationsAdd();
            locationAdd.ShowDialog();
            LoadLocation();
        }

        private void ucAssetVoucherShortcut_BankAccountClicked(object sender, EventArgs e)
        {
            ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
            frmBank.ShowDialog();
            //LoadCashBankLedger();
        }

        private void ucAssetVoucherShortcut_DateClicked(object sender, EventArgs e)
        {
            deInwardDate.Focus();
        }

        private void ucAssetVoucherShortcut_ProjectClicked(object sender, EventArgs e)
        {
            ShowProjectSelectionWindow();
        }

        private void ucAssetVoucherShortcut_ConfigureClicked(object sender, EventArgs e)
        {
            frmAssetSettings assetsetting = new frmAssetSettings();
            assetsetting.ShowDialog();
        }

        private void ucAssetVoucherShortcut_AssetItemClicked(object sender, EventArgs e)
        {
            frmAssetItemAdd itemAdd = new frmAssetItemAdd();
            itemAdd.ShowDialog();
        }

        private void ucAssetVoucherShortcut_NextDateClicked(object sender, EventArgs e)
        {
            deInwardDate.Focus();
            DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deInwardDate.DateTime = (deInwardDate.DateTime < dtYearTo) ? deInwardDate.DateTime.AddDays(1) : dtYearTo;
        }

        private void ucAssetVoucherShortcut_DonorClicked(object sender, EventArgs e)
        {
            frmDonorAdd donorAdd = new frmDonorAdd(ViewDetails.Donor, (int)AddNewRow.NewRow, ProjectId);
            donorAdd.ShowDialog();
            LoadDonors();
        }

        private void ucAssetVoucherShortcut_AssetVoucherView(object sender, EventArgs e)
        {
            frmAssetVoucherView assetVoucherView = new frmAssetVoucherView(ProjectName, ProjectId, deInwardDate.DateTime, Flag);
            assetVoucherView.VoucherEditHeld += new EventHandler(OnEditHeld);
            assetVoucherView.ShowDialog();
        }

        #endregion

        #region Donor

        private void glkpDonor_Leave(object sender, EventArgs e)
        {
            if (rgVoucherType.SelectedIndex == 1)
                SetBorderColor(glkpDonor);
        }

        private void glkpDonor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ResultArgs result = FetchProjectDetails();
                DataView dvResult = result.DataSource.Table.DefaultView;
                dvResult.RowFilter = "PROJECT_ID=" + ProjectId;
                DataTable dtResult = dvResult.ToTable();
                ContributionId = this.UtilityMember.NumberSet.ToInteger(dtResult.Rows[0]["CONTRIBUTION_ID"].ToString());
                dvResult.RowFilter = "";
                if (!string.IsNullOrEmpty(glkpDonor.Text))
                {
                    glkpPurpose.Enabled = true;
                    this.NameAddress = glkpDonor.Text;
                    DataRow dr = (glkpDonor.Properties.GetRowByKeyValue(glkpDonor.EditValue) as DataRowView).Row;
                    if (dr != null)
                    {
                        glkpPurpose.EditValue = ContributionId > 0 ? glkpPurpose.Properties.GetKeyValue(ContributionId - 1)
                            : glkpPurpose.Properties.GetKeyValue(0);
                    }
                }
                else { glkpPurpose.Enabled = false; }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
        }

        private void glkpDonor_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadDonorCombo();
            }
        }

        private void glkpDonor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {

                glkVendor.Select();
                glkVendor.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (glkpPurpose.Enabled)
                {
                    glkpPurpose.Select();
                    glkpPurpose.Focus();
                }
                else
                {
                    FocusPurchaseGrid();
                }
                e.SuppressKeyPress = true;
            }
            else
                ProcessShortcutKeys(e);
        }

        private void glkpPurpose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                glkpDonor.Select();
                glkpDonor.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                FocusPurchaseGrid();
                e.SuppressKeyPress = true;
            }
            else
                ProcessShortcutKeys(e);
        }

        private void glkpDonor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void glkpPurpose_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtsample = new DataTable();
            if (Flag == AssetInOut.IK)
            {
                dtsample = ConstructJournalEntry();
            }

            DataTable dtFilteredRows = new DataTable();
            if (ValidatePurchaseDetials())
            {
                try
                {
                    using (AssetInwardOutwardSystem inwardvouchersystem = new AssetInwardOutwardSystem())
                    {
                        inwardvouchersystem.InoutId = InwardId;
                        inwardvouchersystem.InoutDetailId = InOutDetailId;
                        inwardvouchersystem.ProjectId = ProjectId;
                        inwardvouchersystem.VoucherNo = txtVoucherNo.Text;
                        inwardvouchersystem.DonorId = glkpDonor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpDonor.EditValue.ToString()) : 0;
                        inwardvouchersystem.InOutDate = this.UtilityMember.DateSet.ToDate(deInwardDate.Text, false);
                        inwardvouchersystem.VendorId = glkVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString()) : 0;
                        inwardvouchersystem.BillInvoiceNo = txtReceiptBillNo.Text.Trim();
                        inwardvouchersystem.Flag = this.Flag == AssetInOut.PU ? AssetInOut.PU.ToString() : AssetInOut.IK.ToString();
                        inwardvouchersystem.VoucherId = VoucherId;
                        inwardvouchersystem.SoldTo = "";
                        inwardvouchersystem.Narration = txtNarration.Text;
                        inwardvouchersystem.NameAddress = this.NameAddress;
                        inwardvouchersystem.CashLedgerId = ucAssetJournal1.CashLedgerId;
                        inwardvouchersystem.purpose = glkpPurpose.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpPurpose.EditValue.ToString()) : 0;

                        DataTable dtSource = gcPurchase.DataSource as DataTable;
                        dtSource.AcceptChanges();
                        dtFilteredRows = dtSource.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        inwardvouchersystem.TotalAmount = UtilityMember.NumberSet.ToDouble(dtSource.Compute("SUM(AMOUNT)", string.Empty).ToString());
                        inwardvouchersystem.dtinoutword = dtFilteredRows;
                        this.Transaction.CostCenterInfo = dsCostCentre;

                        DataTable dtBankTrans = ucAssetJournal1.DtCashBank;

                        if (!dtBankTrans.Columns.Contains("EXCHANGE_RATE"))
                            dtBankTrans.Columns.Add("EXCHANGE_RATE", typeof(decimal));
                        if (!dtBankTrans.Columns.Contains("LIVE_EXCHANGE_RATE"))
                            dtBankTrans.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));

                        if (dtBankTrans.Rows.Count > 0 && dtBankTrans.Columns.Contains("ITEM_ID"))
                            dtFilteredRows = dtBankTrans.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();
                        inwardvouchersystem.dtCashBank = Flag == AssetInOut.PU ? dtBankTrans : dtsample;

                        inwardvouchersystem.AssetCurrencyCountryId = ucAssetJournal1.CurrencyCountryId = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                        inwardvouchersystem.AssetExchangeRate = this.UtilityMember.NumberSet.ToDecimal(txtExchangeRate.Text);
                        // inwardvouchersystem.AssetCurrencyAmount = this.UtilityMember.NumberSet.ToDecimal(txtCurrencyAmount.Text);
                        inwardvouchersystem.AssetExchageCountryId = this.glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                        inwardvouchersystem.AssetLiveExchangeRate = this.UtilityMember.NumberSet.ToDecimal(lblAvgRate.Text);

                        resultArgs = inwardvouchersystem.SaveAssetInwardOutward();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            ClearAssetCommonProperties();
                            ClearControls();
                            LoadNarrationAutoComplete();
                            if (UpdateHeld != null)
                            {
                                UpdateHeld(this, e);
                            }
                        }
                        else
                        {
                            MessageRender.ShowMessage(resultArgs.Message);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
                }
                finally
                {
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            InwardId = 0;
            ClearControls();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucherNo.Text = string.Empty;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dePurchaseDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(deInwardDate);
        }

        private void rglkpAssName_Validating(object sender, CancelEventArgs e)
        {
            //gvPurchase.SetFocusedRowCellValue(colLedgerId, this.AccountLedgerId);
            //gvPurchase.UpdateCurrentRow();
            int itemId = 0;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;

                if (drv != null)
                {
                    itemId = this.UtilityMember.NumberSet.ToInteger(drv[ITEM_ID].ToString());
                    gvPurchase.SetFocusedRowCellValue(colAssItemID, itemId);
                    gvPurchase.UpdateCurrentRow();
                }
            }
        }

        private void dePurchaseDate_EditValueChanged(object sender, EventArgs e)
        {
            //rgdtMaterializedDate.MinValue = rgdtMaterializedDate.MinValue = 
            ucAssetJournal1.MinDate = deInwardDate.DateTime.ToString();
            FetchVoucherMethod();
            if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic && VoucherId == 0)
            {
                LoadVoucherNo();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && VoucherId == 0)
            {
                txtVoucherNo.Enabled = true;
                txtVoucherNo.Text = string.Empty;
            }

            // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                AssignLiveExchangeRate();
            }
        }

        private void frmInwardVoucherAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearAssetCommonProperties();
        }

        private void glkVendor_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkVendor);

        }

        private void txtBillNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtReceiptBillNo);
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucAssetJournal1.DeleteTransaction();
        }

        private void rgVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangingVoucherType();
        }

        private void ChangingVoucherType()
        {
            //FetchVoucherMethod();
            //SettingProperty.AssetListCollection.Clear();
            //SettingProperty.AssetInsuranceCollection.Clear();
            //LoadVoucherNo();
            //LoadAccountingDate();/
            //if (Flag == AssetInOut.PU)
            //{
            //    ShowPurchaseProperties();
            //    ucAssetVoucherShortcut.DisableBankAccount = BarItemVisibility.Always;
            //    ucAssetVoucherShortcut.DisableVendor = BarItemVisibility.Always;
            //    bbiDeleteCashBankRow.Visibility = BarItemVisibility.Always;
            //    bbiMoveToCashBank.Visibility = BarItemVisibility.Always;
            //    glkpDonor.EditValue = "";
            //    glkpPurpose.EditValue = "";
            //    lctgPurchase.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_DETAILS);
            //}
            //else if (Flag == AssetInOut.IK)
            //{
            //    ShowInkindProperties();
            //    ucAssetVoucherShortcut.DisableBankAccount = BarItemVisibility.Never;
            //    bbiDeleteCashBankRow.Visibility = BarItemVisibility.Never;
            //    bbiMoveToCashBank.Visibility = BarItemVisibility.Never;
            //    glkVendor.EditValue = "";
            //    lctgPurchase.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.INKIND_DETAILS);
            //}
            //SetBackColorByAssetVoucherType();
            //deInwardDate.Focus();
        }

        private void frmInwardVoucherAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
            LoadDefaults();
            LoadNarrationAutoComplete();
            deInwardDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            if (this.Flag == AssetInOut.PU)
            {
                //rgVoucherType.SelectedIndex = 0;

                ShowPurchaseProperties();
                //lblDonor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //lblPurpose.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                glkpDonor.Enabled = false;
                glkpPurpose.Enabled = false;
            }
            else if (this.Flag == AssetInOut.IK)
            {
                //rgVoucherType.SelectedIndex = 1;
                ShowInkindProperties();
                lblDonor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblPurpose.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                glkpDonor.Enabled = true;
                glkpPurpose.Enabled = false;
                lblBillno.Text = "Bill/Invoice No";
                lblVendor.Text = " Vendor / Service Provider";

                //  lblVendor.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.INWARD_SERVICE_PROVIDER_INFO) +"<Color='red'>*</color>";
            }
            AssignValuestoControls();

            if (VoucherId != 0)
            {
                ucAssetVoucherShortcut.DisableProject = BarItemVisibility.Never;
            }
            //rgdtMaterializedDate.MinValue = deInwardDate.DateTime;
            //ShowDonorAdditionalInfo();
            HideShowCurrency();
        }

        private void HideShowCurrency()
        {
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                lciCurrencyGroup.Visibility = LayoutVisibility.Always;
            }
            else
            {
                lciCurrencyGroup.Visibility = LayoutVisibility.Never;

            }
        }


        //private void SetBGColor()
        //{

        //}

        private void frmInwardVoucherAdd_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        private void frmInwardVoucherAdd_Shown(object sender, EventArgs e)
        {
            deInwardDate.Focus();
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            RemoveColor(txtVoucherNo);
        }

        private void deInwardDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (txtVoucherNo.Enabled)
                {
                    txtVoucherNo.Focus();
                }
                else
                {
                    txtReceiptBillNo.Focus();
                }
                e.SuppressKeyPress = true;
            }
            //else
            //    ProcessShortcutKeys(e);
        }

        private void txtVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                deInwardDate.Select();
                deInwardDate.Focus();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtReceiptBillNo.Select();
                txtReceiptBillNo.Focus();
                e.SuppressKeyPress = true;
            }
            else
                ProcessShortcutKeys(e);

        }

        private void txtInvoiceBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (txtVoucherNo.Enabled)
                {
                    txtVoucherNo.Select();
                    txtVoucherNo.Focus();
                }
                else
                {
                    deInwardDate.Select();
                    deInwardDate.Focus();
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                glkVendor.Focus();

                e.SuppressKeyPress = true;
            }
        }

        private void deInwardDate_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void txtVoucherNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void txtInvoiceBillNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void glkVendor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            //if (e.KeyCode == Keys.Tab)  // || e.KeyCode == Keys.Enter
            //{
            //    FocusPurchaseGrid();
            //    // SendKeys.Send("{TAB}");
            //}
        }

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            SetTextEditBackColor(txtNarration);
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            RemoveColor(txtNarration);
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab) { FocusCashTransactionGrid(); e.SuppressKeyPress = true; }
        }

        private void glkVendor_EditValueChanged(object sender, EventArgs e)
        {
            this.NameAddress = glkVendor.Text;
        }

        private void glkVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                txtReceiptBillNo.Select();
                txtReceiptBillNo.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (this.AppSetting.AllowMultiCurrency == 1)
                {
                    glkpCurrencyCountry.Select();
                    glkpCurrencyCountry.Focus();
                    e.SuppressKeyPress = true;
                }
                else
                {
                    if (glkpDonor.Visible)
                    {
                        glkpDonor.Select();
                        glkpDonor.Focus();
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        FocusPurchaseGrid();
                        e.SuppressKeyPress = true;
                    }
                }
            }
            else
                ProcessShortcutKeys(e);
        }

        #endregion

        #region Methods

        // Load the Vendor details to show the immediate added value in to the Vendor Combo

        public void LoadVerdorCombo()
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            frmVendorInfoAdd frmVendorInfoAdd = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
            frmVendorInfoAdd.ShowDialog();
            if (frmVendorInfoAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadVendor();
                if (frmVendorInfoAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmVendorInfoAdd.ReturnValue.ToString()) > 0)
                {
                    glkVendor.EditValue = this.UtilityMember.NumberSet.ToInteger(frmVendorInfoAdd.ReturnValue.ToString());
                }
            }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            //}
        }

        // Load the Donor details to show the immediate added value in to the Vendor Combo

        public void LoadDonorCombo()
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            frmDonorAdd frmDonorAdd = new frmDonorAdd();
            frmDonorAdd.ShowDialog();
            if (frmDonorAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadDonors();
                if (frmDonorAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmDonorAdd.ReturnValue.ToString()) > 0)
                {
                    glkpDonor.EditValue = this.UtilityMember.NumberSet.ToInteger(frmDonorAdd.ReturnValue.ToString());
                }
            }
            //}
            //else
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            //}
        }

        #region Purchase grid

        private void LoadReceiptType()
        {
            TransSource transSource = new TransSource();
            DataView dvtransSource = this.UtilityMember.EnumSet.GetEnumDataSource(transSource, Sorting.None);
            rglkpSource.DataSource = dvtransSource.ToTable();
            rglkpSource.DisplayMember = "Name";
            rglkpSource.ValueMember = "Id";
        }

        private bool ValidatePurchaseDetials()
        {
            bool isPurchaseTrue = true;
            if (string.IsNullOrEmpty(deInwardDate.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_DATE_EMPTY));
                this.SetBorderColor(deInwardDate);
                isPurchaseTrue = false;
                this.deInwardDate.Focus();
            }
            else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual && string.IsNullOrEmpty(txtVoucherNo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_NUMBER_EMPTY));
                isPurchaseTrue = false;
                txtVoucherNo.Focus();
            }

            else if (Flag != AssetInOut.IK)
            {

                if (string.IsNullOrEmpty(txtReceiptBillNo.Text))
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.INWARD_BILL_NO_EMPTY));
                    this.SetBorderColor(txtReceiptBillNo);
                    isPurchaseTrue = false;
                    this.txtReceiptBillNo.Focus();
                }
                else if (string.IsNullOrEmpty(glkVendor.Text))
                {
                    //this.ShowMessageBox("Vendor is empty");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_VENDOR_EMPTY));
                    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.INWARD_BILL_NO_EMPTY));
                    this.SetBorderColor(glkVendor);
                    isPurchaseTrue = false;
                    this.glkVendor.Focus();
                }
            }
            else if (!IsvalidVendor())
            {
                isPurchaseTrue = false;
            }
            else if (!IsValidTransGrid())
            {
                isPurchaseTrue = false;
            }
            else if (!IsvalidAmount())
            {
                isPurchaseTrue = false;
            }
            else if (this.Flag == AssetInOut.PU && !ucAssetJournal1.IsValidBankGrid())
            {
                isPurchaseTrue = false;
            }
            else if (!IsQuantitymatch())
            {
                //this.ShowMessageBox("Quantity Mismatch with the Asset Item List.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.INWARD_ASSET_ITEM_MISMATCH_INFO));
                isPurchaseTrue = false;
            }
            else if (this.AppSetting.AllowMultiCurrency == 1 && !IsCurrencyEnabledVoucher)
            { //On 04/09/2024, If multi currency enabled, all the currency details must be filled
                MessageRender.ShowMessage("As Multi Currency option is enabled, All the Currecny details should be filled.");
                glkpCurrencyCountry.Select();
                glkpCurrencyCountry.Focus();
                isPurchaseTrue = false;
            }
            return isPurchaseTrue;
        }

        private bool IsvalidVendor()
        {
            bool isPurchaseTrue = true;
            if (this.Flag == AssetInOut.PU && string.IsNullOrEmpty(glkVendor.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_VENDOR_EMPTY));
                isPurchaseTrue = false;
                this.glkVendor.Focus();
                this.SetBorderColor(glkVendor);
            }
            return isPurchaseTrue;
        }

        private bool IsvalidAmount()
        {
            double cramount = 0, dramount = 0;
            ucAssetJournal1.CalculateAmount();
            cramount = ucAssetJournal1.CrTotal;
            dramount = ucAssetJournal1.DrTotal + AmountSummaryVal;
            bool isPurchaseTrue = true;
            //if (this.Flag == AssetInOut.PU && AmountSummaryVal != BankTransSummaryVal)
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_MISMATCH));
            //    isPurchaseTrue = false;
            //    FocusCashTransactionGrid();
            //}
            if (cramount != dramount)
            {
                //this.ShowMessageBox("Amount mismatched.");
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.INWARD_AMOUNT_MISMATCH_INFO));
                isPurchaseTrue = false;
                ucAssetJournal1.Select();
                ucAssetJournal1.Focus();
            }
            return isPurchaseTrue;
        }

        private void LoadAssetItem()
        {
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    resultArgs = assetItemSystem.FetchAssetItemDetails();
                    dtAssetItem = resultArgs.DataSource.Table;
                    if (dtAssetItem != null && dtAssetItem.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpAssetName, dtAssetItem, assetItemSystem.AppSchema.ASSETItem.ASSET_ITEMColumn.ColumnName, assetItemSystem.AppSchema.ASSETItem.ITEM_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ConstructPurchaseVoucherDetail()
        {
            DataTable dtPurchaseVouhcerDetail = new DataTable();
            dtPurchaseVouhcerDetail.Columns.Add("SOURCE", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("ITEM_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("QUANTITY", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("RATE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("IN_OUT_DETAIL_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("LEDGER_ID", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("CHEQUE_NO", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            dtPurchaseVouhcerDetail.Columns.Add("AVAILABLE_QUANTITY", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("SALVAGE_VALUE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("EXCHANGE_RATE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
            dtPurchaseVouhcerDetail.Rows.Add(dtPurchaseVouhcerDetail.NewRow());
            gcPurchase.DataSource = dtPurchaseVouhcerDetail;
            gvPurchase.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.By : (int)Source.To;
            gvPurchase.MoveFirst();
            gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colSource, sourceId);

        }

        private int BindAvailQty()
        {
            using (AssetInwardOutwardSystem inwardSystem = new AssetInwardOutwardSystem())
            {
                //this.ItemId=gvPurchase.GetFocusedRowCellValue(colAssItemID);
                inwardSystem.LocationId = 0;
                inwardSystem.ItemId = this.ItemId;
                inwardSystem.ProjectId = this.ProjectId;
                inwardSystem.InOutDate = deInwardDate.DateTime;
                AvailQty = inwardSystem.FetchAvailableQty();
                gvPurchase.SetFocusedRowCellValue(colAvailableQty, AvailQty);
            }
            return AvailQty;
        }

        private bool IsValidTransGrid()
        {
            bool isValid = true;
            try
            {
                DataTable dtPurchase = gcPurchase.DataSource as DataTable;
                int ItemId = 0;
                int RowPosition = 0;
                string AssetId = string.Empty;
                int Quantity = 0;
                decimal Amount = 0;
                decimal salvagevalue = 0;
                DataView dv = new DataView(dtPurchase);
                dv.RowFilter = "(ITEM_ID>0 OR QUANTITY>0)";
                gvPurchase.FocusedColumn = colAssetName;
                if (dv.Count > 0)
                {
                    foreach (DataRowView drPurchase in dv)
                    {
                        ItemId = this.UtilityMember.NumberSet.ToInteger(drPurchase[ITEM_ID].ToString());
                        Quantity = this.UtilityMember.NumberSet.ToInteger(drPurchase[QUANTITY].ToString());
                        Amount = this.UtilityMember.NumberSet.ToDecimal(drPurchase[AMOUNT].ToString());
                        salvagevalue = this.UtilityMember.NumberSet.ToDecimal(drPurchase[SALVAGEVALUE].ToString());

                        if (ItemId == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                            gvPurchase.FocusedColumn = colAssetName;
                            isValid = false;
                        }
                        else if (Quantity == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_QUANTITY_EMPTY));
                            gvPurchase.FocusedColumn = colQuantity;
                            isValid = false;
                        }
                        else if (Amount == 0)
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.REQUIRED_INFORMATION_AMOUNT_EMPTY));
                            isValid = false;
                        }
                        //else if (salvagevalue == 0)
                        //{
                        //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InwardVoucher.SALAVAGE_VALUE_EMPTY));
                        //    isValid = false;
                        //}
                        if (!isValid) break;
                        RowPosition = RowPosition + 1;
                    }
                }
                else
                {
                    isValid = false;
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.SalesVoucher.SALES_ASSET_NAME_EMPTY));
                    gvPurchase.FocusedColumn = colAssetName;
                }

                if (!isValid)
                {
                    gvPurchase.CloseEditor();
                    gvPurchase.FocusedRowHandle = gvPurchase.GetRowHandle(RowPosition);
                    gvPurchase.ShowEditor();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
                isValid = false;
            }
            return isValid;
        }

        private void ShowPurchaseProperties()
        {
            lblVoucherType.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE);
            ucAssetVoucherShortcut.DisableDonor = BarItemVisibility.Never;
            //LoadCashBankLedger();
        }

        private void FocusPurchaseGrid()
        {
            //DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvPurchase.FocusedColumn = gvPurchase.Columns.ColumnByName(colAssetName.Name);
            gvPurchase.ShowEditor();
            gvPurchase.MoveFirst();
        }

        bool IsDuplicatedValue(GridView currentView, GridColumn currentColumn, object someValue)
        {
            bool isexist = true;
            for (int i = 0; i < currentView.DataRowCount; i++)
            {
                if (currentView.GetRowCellValue(currentView.GetRowHandle(i), currentColumn).ToString() == someValue.ToString())
                {
                    gvPurchase.FocusedRowHandle = i;
                    gvPurchase.FocusedColumn = colQuantity;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Bank Grid

        private DataTable BindCurrentBalance(DataTable dtTrans)
        {
            double currentbal = 0;
            int LedgerId = 0;
            string amount = string.Empty;
            foreach (DataRow dr in dtTrans.Rows)
            {
                currentbal = 0;
                LedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());

                BalanceProperty balance = FetchCurrentBalance(LedgerId);
                currentbal = UtilityMember.NumberSet.ToDouble(balance.Amount.ToString());
                amount = UtilityMember.NumberSet.ToCurrency(currentbal);
                amount = amount.Remove(0, 1);
                dr["LEDGER_BALANCE"] = amount + " " + balance.TransMode;
            }
            return dtTrans;
        }

        //private bool IsValidBankGrid()
        //{
        //    bool isValid = true;
        //    try
        //    {
        //        gcCashBank.RefreshDataSource();
        //        gvCashBank.UpdateCurrentRow();
        //        DataTable dtBank = gcCashBank.DataSource as DataTable;
        //        int RowPosition = 0;
        //        int LedgerId = 0;
        //        decimal Amount = 0;
        //        DataView dv = new DataView(dtBank);
        //        dv.RowFilter = "(LEDGER_ID>0 OR AMOUNT>0)";
        //        dtBank = dv.ToTable();
        //        gvPurchase.FocusedColumn = colAssetName;
        //        if (dtBank.Rows.Count > 0)
        //        {
        //            foreach (DataRow drPurchase in dtBank.Rows)
        //            {
        //                LedgerId = this.UtilityMember.NumberSet.ToInteger(drPurchase[LEDGER_ID].ToString());
        //                Amount = this.UtilityMember.NumberSet.ToDecimal(drPurchase[AMOUNT].ToString());
        //                if (LedgerId == 0)
        //                {
        //                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_LEDGER_EMPTY));
        //                    gvCashBank.FocusedColumn = colCashBank;
        //                    isValid = false;
        //                }
        //                else if (Amount == 0)
        //                {
        //                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.CASHBANK_AMOUNT_EMPTY));
        //                    gvCashBank.FocusedColumn = colBankAmount;
        //                    isValid = false;
        //                }
        //                if (!isValid) break;
        //                RowPosition = RowPosition + 1;
        //            }
        //        }
        //        else
        //        {
        //            isValid = false;
        //            this.ShowMessageBox("Select Cash/Bank.");
        //            gvCashBank.FocusedColumn = colCashBank;
        //        }

        //        if (!isValid)
        //        {
        //            gvCashBank.CloseEditor();
        //            gvCashBank.FocusedRowHandle = gvCashBank.GetRowHandle(RowPosition);
        //            gvCashBank.ShowEditor();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //        isValid = false;
        //    }
        //    return isValid;
        //}

        //private void DeleteTransaction()
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(gvCashBank.GetFocusedRowCellValue(colCashBank).ToString()))
        //        {
        //            if (gvCashBank.RowCount > 1)
        //            {
        //                if (gvCashBank.FocusedRowHandle != GridControl.NewItemRowHandle)
        //                {
        //                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                    {
        //                        gvCashBank.DeleteRow(gvCashBank.FocusedRowHandle);
        //                        gvCashBank.UpdateCurrentRow();
        //                        gcCashBank.RefreshDataSource();
        //                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
        //                        gvCashBank.FocusedColumn = colCashBank;
        //                    }

        //                }
        //            }
        //            else if (gvCashBank.RowCount == 1)
        //            {
        //                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                {
        //                    constractBankDatasource();
        //                    gvCashBank.FocusedColumn = colCashBank;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
        //            gvCashBank.FocusedColumn = colCashBank;
        //        }
        //        CalculateFirstRowValue();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //    }
        //}

        private void CalculateFirstRowValue()
        {
            try
            {
                if (LedgerAmount >= 0 && AmountSummaryVal != BankTransSummaryVal && VoucherId >= 0)
                {
                    double Amount = ucAssetJournal1.BankAmount;//asset.le
                    if (Amount >= 0)
                    {
                        double dAmount = 0.0;
                        if (BankTransSummaryVal <= AmountSummaryVal)
                        {
                            dAmount = Math.Abs((AmountSummaryVal - BankTransSummaryVal) + Amount);
                        }
                        else if (BankTransSummaryVal >= AmountSummaryVal)
                        {
                            dAmount = Math.Abs(Amount - (BankTransSummaryVal - AmountSummaryVal));
                        }
                        if (dAmount >= 0)
                        {
                            ucAssetJournal1.SetCashLedger(AmountSummaryVal);
                            ucAssetJournal1.PurchaseTransSummary = AmountSummaryVal;
                        }
                        gvPurchase.FocusedColumn = colSalvagevalue;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private bool isCashLedgerExists()
        {
            bool isValid = true;
            int LedId = 0;
            using (LedgerSystem ledger = new LedgerSystem())
            {
                LedId = ledger.IsCashLedgerExists();
                if (LedId != 1)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        //private DataTable constractBankDatasource()
        //{
        //    DataTable dtBank = new DataTable();
        //    dtBank.Columns.Add("LEDGER_ID", typeof(int));
        //    dtBank.Columns.Add("AMOUNT", typeof(Decimal));
        //    dtBank.Columns.Add("CHEQUE_NO", typeof(string));
        //    dtBank.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
        //    dtBank.Columns.Add("LEDGER_BALANCE", typeof(string));
        //    dtBank.Rows.Add(dtBank.NewRow());
        //    gcCashBank.DataSource = dtBank;
        //    gvCashBank.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        //    gvCashBank.MoveFirst();
        //    return dtBank;
        //}

        private void ShowInkindProperties()
        {
            //rgVoucherType.SelectedIndex = 1;
            lblVoucherType.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.INKIND);
            //ucAssetJournal1.Visible = false;
            //lciBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //ucAssetVoucherShortcut.DisableBankAccount = BarItemVisibility.Never;
            // bbiDeleteCashBankRow.Visibility = BarItemVisibility.Never;
            //bbiMoveToCashBank.Visibility = BarItemVisibility.Never;
            LoadDonors();
            LoadPurposeDetails();
        }

        //private void LoadCashBankLedger()
        //{
        //    if (this.Flag == AssetInOut.PU)
        //    {
        //        LoadCashBankLedger(rglkpCashBankLedgers);
        //    }
        //}

        //private void EnableCashBankFields()
        //{
        //    int iLedgerId = 0;
        //    int Group = 0;
        //    gvCashBank.UpdateCurrentRow();
        //    DataTable dtTrans = gcCashBank.DataSource as DataTable;

        //    foreach (DataRow dr in dtTrans.Rows)
        //    {
        //        if (dr.RowState != DataRowState.Deleted)
        //        {
        //            iLedgerId = dr["LEDGER_ID"] != null ? this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString()) : 0;
        //            using (LedgerSystem ledger = new LedgerSystem())
        //            {
        //                ledger.LedgerId = iLedgerId;
        //                Group = ledger.FetchLedgerGroupById();
        //                if (Group == (int)FixedLedgerGroup.BankAccounts)
        //                {
        //                    VisibleCashBankAdditionalFields(true);
        //                    break;
        //                }
        //                else
        //                {
        //                    VisibleCashBankAdditionalFields(false);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void VisibleCashBankAdditionalFields(bool Visible)
        //{
        //    colCashBank.VisibleIndex = 1;
        //    colBankAmount.VisibleIndex = 2;
        //    colRefNo.VisibleIndex = 3;
        //    colMaterializedOn.VisibleIndex = 4;
        //    colBankCurrentBalance.VisibleIndex = 5;
        //    if (Visible)
        //    {
        //        colRefNo.Visible = true;
        //        colMaterializedOn.Visible = true;
        //    }
        //    else
        //    {
        //        colRefNo.Visible = false;
        //        colMaterializedOn.Visible = false;
        //        rgdtMaterializedDate.NullText = "";
        //        rgtxtRefNo.NullText = "";
        //    }
        //}

        private void FocusCashTransactionGrid()
        {
            if (lciBank.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            {
                gvPurchase.UpdateCurrentRow();
                txtOtherCharges.Select();
                txtOtherCharges.Focus();
            }
            else
            {
                ucAssetJournal1.FocusCashTransaction();
            }
        }

        private BalanceProperty FetchCurrentBalance(int LedgerId)
        {
            BalanceProperty balProperty;
            using (BalanceSystem balancesystem = new BalanceSystem())
            {
                balProperty = balancesystem.GetBalance(ProjectId, LedgerId, "", BalanceSystem.BalanceType.CurrentBalance);
            }
            return balProperty;
        }

        private string GetLedgerBalanceValues(DataTable dtTrans, int LedgerId)
        {
            string balanceamount = string.Empty;
            double LedgerBalance = 0.00;
            try
            {
                if (dtTrans != null)
                {
                    BalanceProperty balance = FetchCurrentBalance(LedgerId);
                    LedgerBalance = (balance.Amount - BankTransSummaryVal);
                    balanceamount = UtilityMember.NumberSet.ToCurrency(LedgerBalance) + " " + balance.TransMode;
                    balanceamount = balanceamount.Remove(0, 1);
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

            return balanceamount;
        }

        //private void ShowDonorAdditionalInfo()
        //{
        //    if (rgVoucherType.SelectedIndex == 1)
        //    {
        //        ucDonor.DiableDonor = BarItemVisibility.Always;
        //        ucDonor.DisableDeleteVocuher = ucDonor.DisableEntryMethod = BarItemVisibility.Never;
        //        if (lciDonorInfo.Visibility == LayoutVisibility.Always)
        //        {
        //            flyoutPanel1.HidePopup();
        //            lciDonorInfo.Visibility = LayoutVisibility.Never;
        //            glkpDonor.Enabled = false;
        //        }
        //        else
        //        {
        //            lciDonorInfo.Visibility = LayoutVisibility.Always;
        //            flyoutPanel1.ShowPopup();
        //            glkpDonor.Enabled = true;
        //            glkpDonor.Select();
        //            glkpDonor.Focus();
        //        }
        //    }
        //    else
        //    {
        //        lciDonorInfo.Visibility = LayoutVisibility.Never;
        //        flyoutPanel1.HidePopup();
        //        ucDonor.DiableDonor = ucDonor.DisableDeleteVocuher = ucDonor.DisableEntryMethod = BarItemVisibility.Never;
        //    }
        //}

        //private void LoadCashBankLedger(RepositoryItemGridLookUpEdit glkpLedger)
        //{
        //    try
        //    {
        //        using (LedgerSystem ledgerSystem = new LedgerSystem())
        //        {
        //            ledgerSystem.ProjectId = ProjectId;
        //            resultArgs = ledgerSystem.FetchCashBankLedger();
        //            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                rglkpCashBankLedgers.ValueMember = "LEDGER_ID";
        //                rglkpCashBankLedgers.DisplayMember = "LEDGER_NAME";
        //                DataTable dtCashBankLedger = resultArgs.DataSource.Table; // FetchLedgerByDateClosed(resultArgs.DataSource.Table);
        //                rglkpCashBankLedgers.DataSource = dtCashBankLedger; //resultArgs.DataSource.Table;
        //            }
        //            else
        //            {
        //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_CASHBANK_MAPPING_TO_PROJECT) + ProjectName + "");
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }
        //}

        #endregion

        #region Donor
        private void LoadDonors()
        {
            try
            {
                using (MappingSystem ms = new MappingSystem())
                {
                    ms.ProjectId = ProjectId;
                    resultArgs = ms.FetchMappedDonor();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDonor, resultArgs.DataSource.Table, ms.AppSchema.DonorAuditor.NAMEColumn.Caption, ms.AppSchema.DonorAuditor.DONAUD_IDColumn.Caption);
                    }
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

        public void LoadPurposeDetails()
        {
            try
            {
                using (PurposeSystem purposeSystem = new PurposeSystem())
                {
                    purposeSystem.ProjectId = ProjectId;
                    resultArgs = purposeSystem.FetchPurposeMappedDetails();
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPurpose, resultArgs.DataSource.Table, purposeSystem.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName, purposeSystem.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        #endregion

        private void LoadVoucherNo()
        {
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = this.Flag == AssetInOut.PU ? VoucherSubTypes.PY.ToString() : VoucherSubTypes.JN.ToString();
                voucherTransaction.ProjectId = ProjectId;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(deInwardDate.Text, false);
                txtVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void AssignValuestoControls()
        {
            try
            {
                DataTable dt = null;
                if (InwardId > 0)
                {
                    ucAssetVoucherShortcut.DisableProject = BarItemVisibility.Never;
                    using (AssetInwardOutwardSystem InwardOutwardSystem = new AssetInwardOutwardSystem())
                    {
                        InwardOutwardSystem.InoutId = InwardId;
                        InwardOutwardSystem.AssigntoProperties();
                        this.Flag = InwardOutwardSystem.Flag == "PU" ? AssetInOut.PU : AssetInOut.IK;
                        ChangingVoucherType();
                        deInwardDate.DateTime = InwardOutwardSystem.InOutDate;
                        txtVoucherNo.Text = InwardOutwardSystem.VoucherNo;
                        glkVendor.EditValue = InwardOutwardSystem.VendorId;
                        glkpDonor.EditValue = InwardOutwardSystem.DonorId;
                        glkpPurpose.Enabled = InwardOutwardSystem.DonorId > 0 ? true : false;
                        glkpPurpose.EditValue = InwardOutwardSystem.purpose;
                        txtReceiptBillNo.Text = InwardOutwardSystem.BillInvoiceNo;
                        txtNarration.Text = InwardOutwardSystem.Narration;
                        this.NameAddress = InwardOutwardSystem.NameAddress;
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            glkpCurrencyCountry.EditValue = InwardOutwardSystem.AssetCurrencyCountryId;
                            txtCurrencyAmount.Text = this.UtilityMember.NumberSet.ToNumber(this.UtilityMember.NumberSet.ToDouble(InwardOutwardSystem.AssetCurrencyAmount.ToString())).ToString();
                            txtExchangeRate.Text = InwardOutwardSystem.AssetExchangeRate.ToString();
                            lblAvgRate.Text = InwardOutwardSystem.AssetExchangeRate.ToString();
                            lblCalAmt.Text = InwardOutwardSystem.AssetCalcAmount.ToString();
                        }
                        VoucherId = InwardOutwardSystem.VoucherId;
                        //this.Flag = InwardOutwardSystem.Flag != null ? InwardOutwardSystem.Flag.Equals(AssetInOut.PU.ToString()) ? AssetInOut.PU : AssetInOut.IK : AssetInOut.PU;
                        resultArgs = InwardOutwardSystem.FetchAssetInOutDetailById();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            if (!resultArgs.DataSource.Table.Columns.Contains("CHEQUE_NO"))
                                resultArgs.DataSource.Table.Columns.Add("CHEQUE_NO", typeof(string));
                            if (!resultArgs.DataSource.Table.Columns.Contains("MATERIALIZED_ON"))
                                resultArgs.DataSource.Table.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
                            if (this.AppSetting.AllowMultiCurrency == 1)
                            {
                                if (!resultArgs.DataSource.Table.Columns.Contains("EXCHANGE_RATE"))
                                    resultArgs.DataSource.Table.Columns.Add("EXCHANGE_RATE", typeof(decimal));
                                if (!resultArgs.DataSource.Table.Columns.Contains("LIVE_EXCHANGE_RATE"))
                                    resultArgs.DataSource.Table.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
                            }
                            resultArgs.DataSource.Table.Columns.Add("SOURCE", typeof(string));
                            gcPurchase.DataSource = BindSource(resultArgs.DataSource.Table as DataTable);
                            gcPurchase.RefreshDataSource();
                            gvPurchase.MoveFirst();
                        }

                        DataTable dtList = resultArgs.DataSource.Table;
                        int itmID = 0;
                        int Qunty = 0;
                        int InOutDetID = 0;
                        int RNo = 0;
                        foreach (DataRow dritem in dtList.Rows)
                        {
                            itmID = UtilityMember.NumberSet.ToInteger(dritem["ITEM_ID"].ToString());
                            Qunty = UtilityMember.NumberSet.ToInteger(dritem["QUANTITY"].ToString());
                            InOutDetID = UtilityMember.NumberSet.ToInteger(dritem["IN_OUT_DETAIL_ID"].ToString());

                            if (Qunty > 0)
                            {
                                frmAssetItemList frmlist = new frmAssetItemList(itmID, Qunty, RNo, InOutDetID,
                                    InwardOutwardSystem.Flag == AssetInOut.PU.ToString() ? AssetInOut.PU
                                    : AssetInOut.IK, ProjectId);
                                frmlist.AssignItemDetails();
                            }
                            RNo++;
                        }

                        ucAssetJournal1.AssignValues(VoucherId);
                        //resultArgs = InwardOutwardSystem.FetchCashBankByVoucherId(VoucherId);
                        //if (resultArgs != null && resultArgs.Success)
                        //{
                        //    DataTable dtBank = BindCurrentBalance(resultArgs.DataSource.Table);
                        //    gcCashBank.DataSource = resultArgs.DataSource.Table;
                        //    BindCurrentBalance(dtBank);
                        //    EnableCashBankFields();
                        //}

                        ShowCurrencyDetails();
                    }
                    BindAvailQty();
                    gcPurchase.RefreshDataSource();
                    ucAssetJournal1.PurchaseTransSummary = AmountSummaryVal;

                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem(VoucherId))
                    {
                        resultArgs = voucherSystem.FetchTransDetails();
                        if (resultArgs.Success)
                        {
                            dt = resultArgs.DataSource.Table;
                            dsCostCentre.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int LedId = this.UtilityMember.NumberSet.ToInteger(dt.Rows[i][voucherSystem.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                voucherSystem.LedgerId = LedId;
                                voucherSystem.CostCenterTable = i + "LDR" + LedId;
                                resultArgs = voucherSystem.GetCostCentreDetails();
                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                                {
                                    DataTable CostCentreInfo = resultArgs.DataSource.Table;
                                    CostCentreInfo.TableName = i + "LDR" + LedId;
                                    if (CostCentreInfo != null) { dsCostCentre.Tables.Add(CostCentreInfo); }
                                }
                            }
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally
            {
            }
        }

        private DataTable BindSource(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.By : (int)Source.To;
                dr["SOURCE"] = sourceId;
            }
            return dt;
        }

        private void SetTitle()
        {
            colAmount.Caption = colAmount.Caption; //this.SetCurrencyFormat(colAmount.Caption);
            //colBankAmount.Caption = this.SetCurrencyFormat(colBankAmount.Caption);
            //colBankCurrentBalance.Caption = this.SetCurrencyFormat(colBankCurrentBalance.Caption);
            if (this.Flag == AssetInOut.PU)
            {
                this.Text = InwardId == 0 ? this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_EDIT_CAPTION);
                lctgPurchase.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.PURCHASE_DETAILS);
            }
            else
            {
                lctgPurchase.Text = this.GetMessage(MessageCatalog.Asset.InwardVoucher.INKIND_DETAILS);
                this.Text = InwardId == 0 ? this.GetMessage(MessageCatalog.Asset.InwardVoucher.RECEIVE_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.InwardVoucher.RECEIVE_EDIT_CAPTION);
            }
            ucCaptionProject.Caption = ProjectName;
        }

        private void LoadVendor()
        {
            try
            {
                using (VendorInfoSystem vendorSystem = new VendorInfoSystem())
                {
                    resultArgs = vendorSystem.FetchDetails();
                    glkVendor.Properties.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkVendor, resultArgs.DataSource.Table, "NAME", vendorSystem.AppSchema.Vendors.IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void ShowProjectSelectionWindow()
        {
            frmProjectSelection projectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
            projectSelection.ShowDialog();
            if (projectSelection.DialogResult == DialogResult.OK)
            {
                if (projectSelection.ProjectName != string.Empty)
                {
                    ProjectId = projectSelection.ProjectId;
                    ucCaptionProject.Caption = ProjectName = projectSelection.ProjectName;
                    LoadAccountingDate();
                    ClearAssetCommonProperties();
                }
            }
        }

        private void ClearControls()
        {
            if (InwardId == 0)
            {
                glkpCurrencyCountry.EditValue = null;
                txtActualAmt.Text = string.Empty;
                txtCurrencyAmount.Text = string.Empty;
                txtExchangeRate.Text = string.Empty;
                lblAvgRate.Text = "0.0";
                lblCalAmt.Text = "0.0";
                txtVoucherNo.Text = string.Empty;
                InwardId = 0;
                txtReceiptBillNo.Text = string.Empty;
                glkpDonor.EditValue = "";
                glkVendor.EditValue = "";
                glkpPurpose.EditValue = "";
                txtOtherCharges.Text = txtReceiptBillNo.Text = txtNarration.Text = string.Empty;
                ucAssetJournal1.ConstructCashTransEmptySournce();
                ConstructPurchaseVoucherDetail();
                deInwardDate.Focus();
                LoadVoucherNo();
            }
            else
            {
                if (this.UIAppSetting.UITransClose == "1")
                {
                    this.Close();
                }
            }
        }

        //private void ConstructEmptyDataSource()
        //{
        //    ConstructPurchaseVoucherDetail();
        //    if (Flag == AssetInOut.PU)
        //    {
        //        constractBankDatasource();
        //    }
        //}

        private void LoadDefaults()
        {
            LoadReceiptType();
            LoadDonors();
            LoadVendor();
            LoadPurposeDetails();
            LoadAccountingDate();
            //ucAssetJournal1.ConstructCashTransEmptySournce();
            ConstructPurchaseVoucherDetail();
            LoadAssetItem();
            SetBackColorByAssetVoucherType();
            LoadCountry();
        }

        private void ShowCurrencyDetails()
        {
            int CountryId = ucAssetJournal1.CurrencyCountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            lblDonorCurrency.Text = string.Empty;
            txtExchangeRate.Text = "1";
            lblAvgRate.Text = "1.00";
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        //On 22/08/2024, To Multi Currency property
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                            if (result.Success)
                            {
                                lblDonorCurrency.Text = countrySystem.CurrencySymbol;
                                txtExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                                lblAvgRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);

                                // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
                                AssignLiveExchangeRate();
                            }
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
        // On 12/12/2024 - get Live exchange rate, if we received live exchange rate, let us have live exchange rate
        /// </summary>
        private void AssignLiveExchangeRate()
        {
            lblAvgRate.ForeColor = Color.Black;
            lblliveAvgRate.AppearanceItemCaption.ForeColor = Color.Black;
            lblAvgRate.Font = new System.Drawing.Font(lblAvgRate.Font.FontFamily, lblAvgRate.Font.Size, FontStyle.Regular);
            lblliveAvgRate.AppearanceItemCaption.Font = new System.Drawing.Font(lblliveAvgRate.AppearanceItemCaption.Font.FontFamily,
                    lblliveAvgRate.AppearanceItemCaption.Font.Size, FontStyle.Regular);

            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            using (CountrySystem countrySystem = new CountrySystem())
            {
                ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
                if (result.Success)
                {
                    lblAvgRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                    this.ShowWaitDialog("Fetching Live Exchange Rate");
                    double liveExchangeAmount = this.AppSetting.CurrencyLiveExchangeRate(deInwardDate.DateTime.Date, countrySystem.CurrencyCode, AppSetting.CurrencyCode);
                    this.CloseWaitDialog();
                    if (liveExchangeAmount > 0)
                    {
                        lblAvgRate.Text = UtilityMember.NumberSet.ToNumber(liveExchangeAmount);

                        lblAvgRate.ForeColor = Color.Green;
                        lblliveAvgRate.AppearanceItemCaption.ForeColor = Color.Green;
                        lblAvgRate.Font = new System.Drawing.Font(lblAvgRate.Font.FontFamily, lblAvgRate.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                        lblliveAvgRate.AppearanceItemCaption.Font = new System.Drawing.Font(lblliveAvgRate.AppearanceItemCaption.Font.FontFamily,
                                lblliveAvgRate.AppearanceItemCaption.Font.Size, (FontStyle.Bold | FontStyle.Underline));
                    }
                }
            }
        }

        private void CalculateExchangeRate()
        {
            try
            {
                Double ActualAmt = this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) * this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text);
                lblCalAmt.Text = ActualAmt.ToString();
                txtActualAmt.Text = ActualAmt.ToString();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadCountry()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    //On 26/08/2024, If multi currency enabled, let us load only currencies with have exchange rate for voucher date
                    if (this.AppSetting.AllowMultiCurrency == 1)
                        resultArgs = countrySystem.FetchCountryCurrencyDetails(this.UtilityMember.DateSet.ToDate(deInwardDate.Text, false));
                    else
                        resultArgs = countrySystem.FetchCountryDetails();

                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtCurencyCountry = resultArgs.DataSource.Table;
                        dtCurencyCountry.DefaultView.RowFilter = "";

                        //26/08/2024, Load Currecny which have exchange rate
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            dtCurencyCountry.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " >0"; ;
                            dtCurencyCountry = dtCurencyCountry.DefaultView.ToTable();
                        }
                        //this.UtilityMember.ComboSet.BindLookUpEditCombo(glkpCurrencyCountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCurrencyCountry, resultArgs.DataSource.Table,
                            countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());

                        //26/08/2024, For new voucher, set default currecny (global setting)
                        if (VoucherId == 0 && this.AppSetting.AllowMultiCurrency == 1)
                        {
                            glkpCurrencyCountry.EditValue = string.IsNullOrEmpty(this.AppSetting.Country) ? 0 : UtilityMember.NumberSet.ToInteger(this.AppSetting.Country);

                            object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                            if (findcountry == null) glkpCurrencyCountry.EditValue = null;
                        }

                        ucAssetJournal1.CurrencyCountryId = glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        private void LoadAccountingDate()
        {
            deInwardDate.DateTime = UtilityMember.DateSet.ToDate(RecentVoucherDate, false);
            deInwardDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deInwardDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deInwardDate.DateTime = (!string.IsNullOrEmpty(RecentVoucherDate)) ? UtilityMember.DateSet.ToDate(RecentVoucherDate, false) : UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            //deInwardDate.DateTime = deInwardDate.DateTime.AddMonths(1).AddDays(-1);
        }

        private void LoadNarrationAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.AutoFetchNarration();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString());
                        }
                        txtNarration.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNarration.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNarration.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private ResultArgs LoadLocation()
        {
            resultArgs = null;
            LocationSystem locationsystem = new LocationSystem();
            resultArgs = locationsystem.FetchLocationDetails();
            return resultArgs;
        }

        private ResultArgs LoadCustodian()
        {
            resultArgs = null;
            CustodiansSystem custodianssystem = new CustodiansSystem();
            resultArgs = custodianssystem.FetchAllCustodiansDetails();
            return resultArgs;
        }

        private void SetBackColorByAssetVoucherType()
        {

            if (this.Flag == AssetInOut.PU)
            {
                lblVoucherType.Appearance.BackColor = rgVoucherType.BackColor = gvPurchase.Appearance.Row.BackColor = Color.LightSteelBlue;
                ucAssetJournal1.setBgColor();
            }
            else if (this.Flag == AssetInOut.IK)
            {
                lblVoucherType.Appearance.BackColor = rgVoucherType.BackColor = gvPurchase.Appearance.Row.BackColor =
                    gvPurchase.Appearance.FocusedRow.BackColor = Color.Wheat;
            }
        }

        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode.Equals(Keys.F5))
            {
                if (VoucherId == 0)
                {
                    ShowProjectSelectionWindow();
                    if (TransVoucherMethod == (int)TransactionVoucherMethod.Automatic)
                    {
                        LoadVoucherNo();
                    }
                    else if (TransVoucherMethod == (int)TransactionVoucherMethod.Manual)
                    {
                        txtVoucherNo.Text = string.Empty;
                    }
                }
            }
            if (e.KeyCode.Equals(Keys.F12))
            {
                frmAssetSettings setting = new frmAssetSettings();
                setting.ShowDialog();
            }
            if (e.KeyData == (Keys.Alt | Keys.D))
            {
                if (this.Flag == AssetInOut.PU)
                    ucAssetJournal1.DeleteTransaction();
            }
            if (e.KeyData == (Keys.Control | Keys.D))
            {
                DeletePurchaseTransaction();
            }
            if (e.KeyData == (Keys.Control | Keys.L))
            {
                GenerationList();
            }
            if (e.KeyCode.Equals(Keys.F3))
            {
                deInwardDate.Focus();
            }
            if (e.KeyCode.Equals(Keys.F4))
            {
                DateTime dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
                deInwardDate.DateTime = (deInwardDate.DateTime < dtYearTo) ? deInwardDate.DateTime.AddDays(1) : dtYearTo;
            }
            //if (e.KeyData == (Keys.Alt | Keys.E))
            //{
            //    frmAssetItemAdd itemadd = new frmAssetItemAdd();
            //    itemadd.ShowDialog();
            //    LoadAssetItem();
            //}
            if (e.KeyData == (Keys.Alt | Keys.T))
            {
                frmMapLocation maplocation = new frmMapLocation();
                maplocation.ShowDialog();
            }
            if (e.KeyData == (Keys.Alt | Keys.M))
            {
                frmMapProjectLedger mapping = new frmMapProjectLedger(MapForm.Asset, (int)AddNewRow.NewRow, ProjectName);
                mapping.ShowDialog();
                LoadPurposeDetails();
            }
            if (e.KeyData == (Keys.Alt | Keys.U))
            {
                frmCustodiansAdd custodianAdd = new frmCustodiansAdd();
                custodianAdd.ShowDialog();
            }
            if (e.KeyData == (Keys.Alt | Keys.F))
            {
                frmVendorInfoAdd vendorAdd = new frmVendorInfoAdd(0, VendorManufacture.Manufacture);
                vendorAdd.ShowDialog();
            }
            if (e.KeyData == (Keys.Alt | Keys.V))
            {
                frmVendorInfoAdd vendorAdd = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
                vendorAdd.ShowDialog();
                LoadVendor();
            }
            if (e.KeyData == (Keys.Alt | Keys.B))
            {
                if (this.Flag == AssetInOut.PU)
                {
                    frmLedgerDetailAdd BankAdd = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.BK, ProjectId);
                    BankAdd.ShowDialog();
                    //LoadCashBankLedger();
                }
            }
            else if (e.KeyData == (Keys.Alt | Keys.A))
            {
                frmAssetItemAdd ItemAdd = new frmAssetItemAdd((int)AddNewRow.NewRow);
                ItemAdd.ShowDialog();
                LoadAssetItem();
            }
            if (e.KeyData == (Keys.Alt | Keys.L))
            {
                frmLocationsAdd LocationAdd = new frmLocationsAdd();
                LocationAdd.ShowDialog();
            }
            //if (e.KeyData == (Keys.Alt | Keys.O))
            //{
            //    frmLedgerOptions ledgerOptions = new frmLedgerOptions();
            //    ledgerOptions.ShowDialog();
            //}

            if (e.KeyData == (Keys.Alt | Keys.I))
            {
                if (this.Flag == AssetInOut.PU)
                    FocusCashTransactionGrid();
            }
            if (e.KeyData == (Keys.Control | Keys.I))
            {
                FocusPurchaseGrid();
            }
            //if (e.KeyCode.Equals(Keys.F6))
            //{
            //    rgVoucherType.SelectedIndex = 0;
            //    FetchVoucherMethod();
            //    SettingProperty.AssetListCollection.Clear();
            //    SettingProperty.AssetAMCCollection.Clear();
            //    LoadVoucherNo();
            //    LoadAccountingDate();
            //    constractBankDatasource();
            //    ShowPurchaseProperties();
            //    ucAssetVoucherShortcut.DisableBankAccount = BarItemVisibility.Always;
            //    ucAssetVoucherShortcut.DisableVendor = BarItemVisibility.Always;
            //    bbiDeleteCashBankRow.Visibility = BarItemVisibility.Always;
            //    bbiMoveToCashBank.Visibility = BarItemVisibility.Always;
            //    glkpDonor.EditValue = "";
            //    glkpPurpose.EditValue = "";
            //}
            //if (e.KeyCode.Equals(Keys.F7))
            //{
            //    rgVoucherType.SelectedIndex = 1;
            //    FetchVoucherMethod();
            //    SettingProperty.AssetListCollection.Clear();
            //    SettingProperty.AssetAMCCollection.Clear();
            //    LoadVoucherNo();
            //    LoadAccountingDate();
            //    ShowInkindProperties();
            //    ucAssetVoucherShortcut.DisableBankAccount = BarItemVisibility.Never;
            //    ucAssetVoucherShortcut.DisableVendor = BarItemVisibility.Never;
            //    bbiDeleteCashBankRow.Visibility = BarItemVisibility.Never;
            //    bbiMoveToCashBank.Visibility = BarItemVisibility.Never;
            //    glkVendor.EditValue = "";
            //}
            if (e.KeyData == (Keys.Alt | Keys.O))
            {
                frmDonorAdd donor = new frmDonorAdd();
                donor.ShowDialog();
                LoadDonors();
            }

            if (e.KeyData == (Keys.Alt | Keys.G))
            {
                frmLedgerDetailAdd LedgerAdd = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId, 0);
                LedgerAdd.ShowDialog();
                ucAssetJournal1.LoadLedger();
            }

            if (e.KeyData == (Keys.Alt | Keys.E))
            {
                frmCostCentreAdd costcentre = new frmCostCentreAdd((int)AddNewRow.NewRow, ProjectId);
                costcentre.ShowDialog();
            }
            if (e.KeyData == (Keys.Alt | Keys.R))
            {
                frmLedgerOptions ledgeroption = new frmLedgerOptions();
                ledgeroption.ShowDialog();
                ucAssetJournal1.Flag = this.Flag;
                ucAssetJournal1.LoadLedger();
            }
        }

        private void FetchVoucherMethod()
        {
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                string VoucherMethod = this.Flag == AssetInOut.PU ? "2" : "4";
                int voucherdefinitionid = this.Flag == AssetInOut.PU ? 2 : 4;
                resultArgs = projectSystem.FetchVoucherByProjectId(ProjectId, VoucherMethod, voucherdefinitionid);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    TransVoucherMethod = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][projectSystem.AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString());
                    if (TransVoucherMethod == 1)
                    {
                        txtVoucherNo.Enabled = false;
                    }
                    else
                    {
                        txtVoucherNo.Enabled = true;
                        txtVoucherNo.Select();
                        txtVoucherNo.Focus();
                    }
                }
                else
                {
                    TransVoucherMethod = 2;
                }
            }
        }

        private ResultArgs FetchProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return resultArgs;
        }

        private void SetTextEditBackColor(TextEdit txtEdit)
        {
            txtEdit.BackColor = Color.Thistle;
        }

        private void RemoveColor(TextEdit txtEdit)
        {
            txtEdit.BackColor = Color.Empty;
        }

        private void rtxtQuantity_Enter(object sender, EventArgs e)
        {
            // if (tmpQuantity == 0)
            tmpQuantity = Quantity;
        }

        private void ucAssetVoucherShortcut_AccountMappingClicked(object sender, EventArgs e)
        {
            frmMapProjectLedger mapping = new frmMapProjectLedger(MapForm.Asset, (int)AddNewRow.NewRow, "");
            mapping.ShowDialog();
            LoadPurposeDetails();
            LoadDonors();
            ucAssetJournal1.LoadLedger();
        }

        private void rglkpAssetName_Leave(object sender, EventArgs e)
        {
            //gvPurchase.SetFocusedRowCellValue(colLedgerId, AccountLedgerId);
            //gvPurchase.UpdateCurrentRow();
            //gvPurchase.PostEditor();
            //BindAvailQty();
        }

        private void ucAssetVoucherShortcut_LocationMappingClicked(object sender, EventArgs e)
        {
            frmMapLocation maplocation = new frmMapLocation();
            maplocation.ShowDialog();
        }

        private void ucAssetVoucherShortcut_PurchaseClicked(object sender, EventArgs e)
        {
            rgVoucherType.SelectedIndex = 0;
            FetchVoucherMethod();
            ClearAssetCommonProperties();
            LoadVoucherNo();
            LoadAccountingDate();
            //constractBankDatasource();
            ShowPurchaseProperties();
            ucAssetVoucherShortcut.DisableBankAccount = BarItemVisibility.Always;
            ucAssetVoucherShortcut.DisableVendor = BarItemVisibility.Always;
            glkpDonor.EditValue = "";
            glkpPurpose.EditValue = "";
        }

        private void ucAssetVoucherShortcut_InkindClicked(object sender, EventArgs e)
        {
            rgVoucherType.SelectedIndex = 1;
            FetchVoucherMethod();
            ClearAssetCommonProperties();
            LoadVoucherNo();
            LoadAccountingDate();
            //constractBankDatasource();
            ShowInkindProperties();
            ucAssetVoucherShortcut.DisableBankAccount = BarItemVisibility.Never;
            ucAssetVoucherShortcut.DisableVendor = BarItemVisibility.Never;
            glkVendor.EditValue = "";
            //constractBankDatasource();
        }

        private void rbtnPurchaseDelete_Click(object sender, EventArgs e)
        {
            DeletePurchaseTransaction();
        }

        private void DeletePurchaseTransaction()
        {
            try
            {
                //if (!string.IsNullOrEmpty(gvPurchase.GetFocusedRowCellValue(colAssItemID).ToString()))
                //{
                int rowNo = 0;
                frmAssetItemList AssetItemList;
                int soldItemcount = 0;

                if (gvPurchase.RowCount > 1)
                {
                    if (gvPurchase.FocusedRowHandle != GridControl.NewItemRowHandle)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (AssetInwardOutwardSystem inoutwardSystem = new AssetInwardOutwardSystem())
                            {
                                bool InsuranceMade = false;
                                bool SalesMade = false;
                                int insurance = inoutwardSystem.CheckInsuranceByItemID(InwardId, ProjectId, InOutDetailId, ItemId);
                                soldItemcount = inoutwardSystem.CheckSoldAssetIdByItemID(InwardId, InOutDetailId, ItemId);
                                if (insurance > 0)
                                {
                                    //if (this.ShowConfirmationMessage("Insurance is made for this Entry. Do you want to delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.InwardVoucher.INSURANCE_MADE_ENTRY_DELETE_CONFIRMATION_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        InsuranceMade = true;
                                        if (soldItemcount > 0)
                                        {
                                            //if (this.ShowConfirmationMessage("Sold Item can not be deleted.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.InwardVoucher.SOLD_ITEM_CANNOT_CANNOT_DELETE_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                SalesMade = true;
                                                AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowNo, InOutDetailId, this.Flag, ProjectId);
                                                AssetItemList.ShowDialog();
                                            }
                                        }
                                    }
                                }
                                else if (soldItemcount > 0)
                                {
                                    //if (this.ShowConfirmationMessage("Sold Item can not be deleted.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.InwardVoucher.SOLD_ITEM_CANNOT_CANNOT_DELETE_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        SalesMade = true;
                                        AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowNo, InOutDetailId, this.Flag, ProjectId);
                                        AssetItemList.ShowDialog();
                                    }
                                }
                                if (InsuranceMade)// || !InsuranceMade && !SalesMade)
                                {
                                    DeleteRowTransaction();
                                }
                                else if (insurance == 0 && soldItemcount == 0)
                                {
                                    DeleteRowTransaction();
                                }
                            }
                        }
                    }
                }
                else if (gvPurchase.RowCount == 1)
                {
                    if (LedgerId > 0 || Quantity > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //gvPurchase.DeleteRow(gvPurchase.FocusedRowHandle);
                            //ConstructPurchaseVoucherDetail();
                            //int sourceId = (Flag == AssetInOut.PU || Flag == AssetInOut.IK) ? (int)Source.By : (int)Source.To;
                            //gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colSource, sourceId);
                            //gvPurchase.FocusedColumn = colAssetName;
                            //CalculateFirstRowValue();

                            using (AssetInwardOutwardSystem inoutwardSystem = new AssetInwardOutwardSystem())
                            {
                                bool InsuranceMade = false;
                                bool SalesMade = false;

                                int insurance = inoutwardSystem.CheckInsuranceByItemID(InwardId, ProjectId, InOutDetailId, ItemId);
                                soldItemcount = inoutwardSystem.CheckSoldAssetIdByItemID(InwardId, InOutDetailId, ItemId);
                                if (insurance > 0)
                                {
                                    //if (this.ShowConfirmationMessage("Insurance is made for this Entry. Do you want to delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.InwardVoucher.INSURANCE_MADE_ENTRY_DELETE_CONFIRMATION_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        InsuranceMade = true;
                                        if (soldItemcount > 0)
                                        {
                                            //if (this.ShowConfirmationMessage("Sold Item can not be deleted.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.InwardVoucher.SOLD_ITEM_CANNOT_CANNOT_DELETE_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                SalesMade = true;
                                                AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowNo, InOutDetailId, this.Flag, ProjectId);
                                                AssetItemList.ShowDialog();
                                            }
                                        }
                                    }
                                }
                                else if (soldItemcount > 0)
                                {
                                    //if (this.ShowConfirmationMessage("Sold Item can not be deleted.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.InwardVoucher.SOLD_ITEM_CANNOT_CANNOT_DELETE_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        SalesMade = true;
                                        AssetItemList = new frmAssetItemList(ItemId, this.Quantity, rowNo, InOutDetailId, this.Flag, ProjectId);
                                        AssetItemList.ShowDialog();
                                    }
                                }
                                if (InsuranceMade)// || !InsuranceMade && !SalesMade)
                                {
                                    DeleteRowTransaction();
                                    if (gvPurchase.RowCount == 0)
                                    {
                                        gvPurchase.AddNewRow();
                                    }
                                }
                                else if (insurance == 0 && soldItemcount == 0)
                                {
                                    DeleteRowTransaction();
                                    if (gvPurchase.RowCount == 0)
                                    {
                                        gvPurchase.AddNewRow();
                                    }
                                }
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

        private void DeleteRowTransaction()
        {
            frmAssetItemList AssetItemList;
            int rowNo = gvPurchase.FocusedRowHandle;
            AssetItemList = new frmAssetItemList(ItemId, Quantity, rowNo, InOutDetailId, this.Flag, ProjectId);
            AssetItemList.DeleteAssetList();

            gvPurchase.DeleteRow(gvPurchase.FocusedRowHandle);
            gvPurchase.UpdateCurrentRow();
            gcPurchase.RefreshDataSource();
            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
            gvPurchase.FocusedColumn = colAssetName;
            CalculateFirstRowValue();
        }

        private void bbiDeletePurchase_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeletePurchaseTransaction();
        }

        private void bbiDeleteCashBankRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            ucAssetJournal1.DeleteTransaction();
        }

        private void bbiMovetoAsset_ItemClick(object sender, ItemClickEventArgs e)
        {
            FocusPurchaseGrid();
        }

        private void bbiMoveToCashBank_ItemClick(object sender, ItemClickEventArgs e)
        {
            FocusCashTransactionGrid();
        }

        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            txtReceiptBillNo.Focus();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void bbiAssetGenerationList_ItemClick(object sender, ItemClickEventArgs e)
        {
            GenerationList();
        }

        private void rglkpAssetName_Enter(object sender, EventArgs e)
        {
            AssetTempQuantity = this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetFocusedRowCellValue(colQuantity) != null ? gvPurchase.GetFocusedRowCellValue(colQuantity).ToString() : string.Empty);
        }

        private void rtxtQuantity_Leave(object sender, EventArgs e)
        {
            isvalidQty = true;
            if (Quantity > 0)
            {
                int rowid = gvPurchase.GetVisibleIndex(gvPurchase.FocusedRowHandle);
                if (SettingProperty.AssetListCollection.ContainsKey(rowid))
                {
                    int ListCount = SettingProperty.AssetListCollection[rowid].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                                UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 0 : false);
                    string DCount = ListCount.ToString();

                    if (Quantity.ToString() != DCount)
                    {
                        //  this.ShowMessageBox("Quantity Mismatch with the Asset Item List.");
                        isvalidQty = false;
                    }
                }
                gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colQuantity, Quantity);
            }
            else
            {
                gvPurchase.SetRowCellValue(gvPurchase.FocusedRowHandle, colQuantity, 0);
            }
        }

        private bool IsQuantitymatch()
        {
            isvalidQty = true;
            DataTable dtItem = (DataTable)gcPurchase.DataSource;
            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                int RId = 0;
                foreach (DataRow drItem in dtItem.Rows)
                {
                    if (drItem.RowState != DataRowState.Deleted)
                    {
                        if (!string.IsNullOrEmpty(drItem["ITEM_ID"].ToString()))
                        {
                            if (SettingProperty.AssetListCollection.ContainsKey(RId))
                            {
                                int StatusCount = SettingProperty.AssetListCollection[RId].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                                    UtilityMember.NumberSet.ToInteger(r["STATUS"].ToString()) != 1 : false);
                                string DCount = StatusCount.ToString();

                                int SelectCount = SettingProperty.AssetListCollection[RId].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                                    UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) != 1 : false);
                                string sCount = SelectCount.ToString();

                                string ActualCount = (StatusCount + SelectCount).ToString();
                                DataTable dtItems = SettingProperty.AssetListCollection[RId];
                                if (drItem["QUANTITY"].ToString() != ActualCount.ToString())
                                {
                                    gvPurchase.SetRowCellValue(RId, colQuantity, drItem["QUANTITY"].ToString());
                                    isvalidQty = false;
                                    break;
                                }
                            }
                        }
                    }
                    RId++;
                }
            }
            return isvalidQty;
        }

        private DataTable Construct()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LEDGER_ID", typeof(string));
            dt.Columns.Add("NARRATION", typeof(string));
            dt.Columns.Add("DEBIT", typeof(decimal));
            dt.Columns.Add("CREDIT", typeof(decimal));
            dt.Columns.Add("TEMP_CREDIT", typeof(decimal));
            dt.Columns.Add("TEMP_DEBIT", typeof(decimal));
            dt.Columns.Add("LEDGER_BALANCE", typeof(string));
            dt.Columns.Add("EXCHANGE_RATE", typeof(decimal));
            dt.Columns.Add("LIVE_EXCHANGE_RATE", typeof(decimal));
            dt.Rows.Add();
            return dt;
        }


        public DataTable ConstructJournalEntry()
        {
            DataTable dt = Construct();

            DataTable dtItem = (DataTable)gcPurchase.DataSource;
            DataTable dtJLedger = ucAssetJournal1.DtCashBank;

            foreach (DataRow dr in dtItem.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    dt.Rows.Add(dr["LEDGER_ID"].ToString(), "", UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString()), 0, 0, 0, "");
                }
            }

            foreach (DataRow drj in dtJLedger.Rows)
            {
                if (drj.RowState != DataRowState.Deleted)
                {
                    dt.Rows.Add(drj["LEDGER_ID"].ToString(), "", 0, UtilityMember.NumberSet.ToDouble(drj["AMOUNT"].ToString()), 0, 0, "");
                }
            }
            if (dt.Rows.Count > 0 && dt.Columns.Contains("LEDGER_ID") || dt.Columns.Contains("ACCOUNT_LEDGER_ID"))
            {
                IEnumerable<DataRow> EnumurableInoutword = dt.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row["LEDGER_ID"].ToString()) ? false : true);
                if (EnumurableInoutword.Count() > 0)
                {
                    dt = EnumurableInoutword.CopyToDataTable();
                }
            }
            return dt;
        }

        #region Costcentre

        private void rbtnCostcentre_Click(object sender, EventArgs e)
        {
            ShowCostCentre(this.UtilityMember.NumberSet.ToDouble(LedgerAmount.ToString()), true);
        }

        private void ShowCostCentre(double LedgerAmount, bool isCCClicked)
        {
            try
            {
                if (LedgerAmount > 0)
                {
                    int CostCentre = 0;
                    string LedgerName = string.Empty;
                    DataView dvCostCentre = null;
                    using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
                    {
                        voucherSystem.LedgerId = isCCClicked == true ? AccountLedgerId : AccLedgerId;
                        AccLedgerId = voucherSystem.LedgerId;
                        resultArgs = voucherSystem.FetchCostCentreLedger();
                        if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            LedgerName = resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                            CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                            if (CostCentre != 0 && !string.IsNullOrEmpty(LedgerName))
                            {
                                int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvPurchase.GetDataSourceRowIndex(gvPurchase.FocusedRowHandle).ToString()) - 1;
                                RowIndex = RowIndex < 0 ? 0 : RowIndex;

                                if (dsCostCentre.Tables.Contains(RowIndex + "LDR" + AccLedgerId))
                                {
                                    dvCostCentre = dsCostCentre.Tables[RowIndex + "LDR" + AccLedgerId].DefaultView;
                                }

                                frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dvCostCentre, AccLedgerId, LedgerAmount, LedgerName);
                                frmCostCentre.ShowDialog();
                                if (frmCostCentre.DialogResult == DialogResult.OK)
                                {
                                    DataTable dtValues = frmCostCentre.dtRecord;
                                    if (dtValues != null)
                                    {
                                        dtValues.TableName = RowIndex + "LDR" + AccLedgerId;
                                        if (dsCostCentre.Tables.Contains(dtValues.TableName))
                                        {
                                            dsCostCentre.Tables.Remove(dtValues.TableName);
                                        }
                                        dsCostCentre.Tables.Add(dtValues);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
        }

        #endregion

        private void ucAssetVoucherShortcut_CostCentreClicked(object sender, EventArgs e)
        {
            frmCostCentreAdd costcentre = new frmCostCentreAdd((int)AddNewRow.NewRow, ProjectId);
            costcentre.Show();
        }

        private void ucAssetVoucherShortcut_LedgerClicked(object sender, EventArgs e)
        {
            frmLedgerDetailAdd frmLedger = new frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, ProjectId);
            frmLedger.ShowDialog();
            ucAssetJournal1.LoadLedger();
        }
        #endregion

        private void ucAssetVoucherShortcut_LedgerOptionClicked(object sender, EventArgs e)
        {
            frmLedgerOptions ledgerOption = new frmLedgerOptions();
            ledgerOption.ShowDialog();
            ucAssetJournal1.Flag = this.Flag;
            ucAssetJournal1.LoadLedger();
        }

        public void ClearAssetCommonProperties()
        {
            SettingProperty.AssetListCollection.Clear();
            SettingProperty.AssetInsuranceCollection.Clear();
            SettingProperty.AssetMultiInsuranceCollection.Clear();
            SettingProperty.AssetMultiInsuranceVoucherCollection.Clear();
            SettingProperty.AssetDeletedInoutIds = string.Empty;
            SettingProperty.AssetDeletedItemDetailIds = string.Empty;
        }

        private void gvPurchase_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (gvPurchase.GetRowCellValue(gvPurchase.FocusedRowHandle, colAccountLedgerId) != null) // Added By praveen to highlight the row the ledgers that are costcentre enabled
            {
                if (AccountLedgerId > 0)
                {
                    if (e.Column == colcostcnetre)
                    {
                        if (CheckCostcentreEnabled(AccountLedgerId))
                            e.Appearance.BackColor = Color.DarkOrange;
                        else
                            e.Appearance.BackColor = Color.Empty;
                    }
                }
            }
        }

        private void gvPurchase_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvPurchase.FocusedColumn == colcostcnetre) // Added By praveen to restrict the costcentre form to be shown
            {
                if (AccountLedgerId > 0)
                {
                    if (!CheckCostcentreEnabled(AccountLedgerId))
                        e.Cancel = true;
                }
            }
        }

        public bool CheckCostcentreEnabled(int LedgerId)
        {
            int CostCentre = 0;
            bool IsExists = false;
            using (VoucherTransactionSystem voucherSystem = new VoucherTransactionSystem())
            {
                voucherSystem.LedgerId = LedgerId;
                resultArgs = voucherSystem.FetchCostCentreLedger();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    CostCentre = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][voucherSystem.AppSchema.Ledger.IS_COST_CENTERColumn.ColumnName].ToString());
                    IsExists = CostCentre > 0 ? true : false;
                }
            }
            return IsExists;
        }

        protected override void OnEditHeld(object sender, EventArgs e)
        {
            this.Close();
        }

        private void glkVendor_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                LoadVerdorCombo();
            }
        }

        private void glkpCurrencyCountry_EditValueChanged(object sender, EventArgs e)
        {
            ShowCurrencyDetails();
            ucAssetJournal1.LoadLedger();
        }

        private void txtCurrencyAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtCurrencyAmount.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtCurrencyAmount.Text = "0";
                CalculateExchangeRate();
            }
        }

        private void txtExchangeRate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.UtilityMember.NumberSet.ToDouble(txtExchangeRate.Text) >= 0)
            {
                CalculateExchangeRate();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.VOUCHER_AMOUNT_LESS_THAN_ZERO));
                txtExchangeRate.Text = "1";
                CalculateExchangeRate();
            }
        }
    }
}
