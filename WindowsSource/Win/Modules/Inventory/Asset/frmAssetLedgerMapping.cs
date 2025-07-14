using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.Model;


namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetLedgerMapping : frmFinanceBaseAdd
    {
        ResultArgs resultArgs = new ResultArgs();
        public frmAssetLedgerMapping()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetLedgerMapping_Load(object sender, EventArgs e)
        {
            LoadAssetLedgers();
            LoadExpenseLedgers();
            AssignAssetLedgers();
            IsLedgerCreationAllowed();
        }

        private void AssignAssetLedgers()
        {
            using (AssetLedgerMapping assetLedgerMapping = new AssetLedgerMapping())
            {
                glkpAccountLedger.EditValue = assetLedgerMapping.AccountLedgerId;
                glkpDepLedger.EditValue = assetLedgerMapping.DepLedgerId;
                glkpDisposalLedger.EditValue = assetLedgerMapping.DisposalLedgerId;
            }
        }

        private void LoadAssetLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Assert;
                resultArgs = ledgerSystem.FetchLedgerByNature();
                DataView dvAccountLedger = new DataView(resultArgs.DataSource.Table);
                dvAccountLedger.RowFilter = "(GROUP_ID <> 13)";
                if (dvAccountLedger != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAccountLedger, dvAccountLedger.ToTable(),
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private void LoadExpenseLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Expenses;
                resultArgs = ledgerSystem.FetchLedgerByNature();

                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDepLedger, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDisposalLedger, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                resultArgs = ConstructAssetLedgers();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    using (AssetLedgerMapping ledgerMapping = new AssetLedgerMapping())
                    {
                        resultArgs = ledgerMapping.SaveAssetLedger(resultArgs.DataSource.Table);
                    }
                }

                if (resultArgs.Success)
                {
                    this.ShowSuccessMessage("Asset Ledgers have been mapped successfully.");
                    this.Close();
                }
                else
                {
                    this.ShowMessageBoxError(resultArgs.Message);
                }
            }
        }

        private bool Validation()
        {
            bool isLedgerTrue = true;
            //if (string.IsNullOrEmpty(glkpAccountLedger.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_ACCOUNTLEDGER_EMPTY));
            //    this.SetBorderColor(glkpAccountLedger);
            //    isLedgerTrue = false;
            //    this.glkpAccountLedger.Focus();
            //}
            //else if (string.IsNullOrEmpty(glkpDepLedger.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_DEPRECIATIONLEDGER_EMPTY));
            //    this.SetBorderColor(glkpDepLedger);
            //    isLedgerTrue = false;
            //    this.glkpDepLedger.Focus();
            //}
            //else if (string.IsNullOrEmpty(glkpDisposalLedger.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETITEM_DISPOSALLEDGER_EMPTY));
            //    this.SetBorderColor(glkpDisposalLedger);
            //    isLedgerTrue = false;
            //    this.glkpDisposalLedger.Focus();
            //}
            return isLedgerTrue;
        }
        private ResultArgs ConstructAssetLedgers()
        {
            DataView dvAssetLedgers = null;
            DataTable dtAssetLedgers = null;
            try
            {
                AssetLedgerType assetLedgerType = new AssetLedgerType();
                dvAssetLedgers = this.UtilityMember.EnumSet.GetEnumDataSource(assetLedgerType, Sorting.Ascending);
                dtAssetLedgers = dvAssetLedgers.ToTable();
                if (dtAssetLedgers != null)
                {
                    dtAssetLedgers.Columns.Add("VALUE", typeof(int));
                    for (int i = 0; i < dtAssetLedgers.Rows.Count; i++)
                    {
                        AssetLedgerType assetLedgerName = (AssetLedgerType)Enum.Parse(typeof(AssetLedgerType), dtAssetLedgers.Rows[i][1].ToString());
                        int LedgerId = 0;
                        switch (assetLedgerName)
                        {
                            case AssetLedgerType.AccountLedgerId:
                                {
                                    LedgerId = (glkpAccountLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpAccountLedger.EditValue.ToString()) : 0;
                                    break;
                                }
                            case AssetLedgerType.DepreciationLedgerId:
                                {
                                    LedgerId = (glkpDepLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpDepLedger.EditValue.ToString()) : 0;
                                    break;
                                }
                            case AssetLedgerType.DisposalLedgerId:
                                {
                                    LedgerId = (glkpDisposalLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpDisposalLedger.EditValue.ToString()) : 0;
                                    break;
                                }
                        }
                        dtAssetLedgers.Rows[i][2] = LedgerId;
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                resultArgs.DataSource.Data = dtAssetLedgers;
            }
            return resultArgs;
        }

        private void glkpAccountLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, 0);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
                LoadAssetLedgers();
            }
        }

        private void glkpDepLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, 0);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
                LoadAssetLedgers();
            }
        }

        private void glkpDisposalLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, 0);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
                LoadAssetLedgers();
            }
        }
        private void IsLedgerCreationAllowed()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.Yes)
            {
                glkpAccountLedger.Properties.Buttons[1].Visible = false;
                glkpDepLedger.Properties.Buttons[1].Visible = false;
                glkpDisposalLedger.Properties.Buttons[1].Visible = false;
            }
        }

        private void glkpAccountLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpAccountLedger);
        }

        private void glkpDepLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpDepLedger);
        }

        private void glkpDisposalLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpDisposalLedger);
        }
    }
}