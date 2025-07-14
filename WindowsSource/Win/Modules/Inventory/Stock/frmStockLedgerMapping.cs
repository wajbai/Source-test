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

namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockLedgerMapping : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructor
        public frmStockLedgerMapping()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmStockLedgerMapping_Load(object sender, EventArgs e)
        {
            LoadIncomeLedgers();
            LoadExpenseLedgers();
            AssignStockLedgers();
            IsLedgerCreationAllowed();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (glkExpenseLedger.EditValue != null || glkIncomeLedger.EditValue != null)
            {
                resultArgs = ConstructStockLedgers();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    using (StockLedgerMapping ledgerMapping = new StockLedgerMapping())
                    {
                        resultArgs = ledgerMapping.SaveStockLedger(resultArgs.DataSource.Table);
                    }
                }

                if (resultArgs.Success)
                {
                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Stock.StockItem.MAPPED_SUCESSFULLY));
                    this.Close();
                }
                else
                {
                    this.ShowMessageBoxError(resultArgs.Message);
                }
            }
        }

        #endregion

        #region Methods

        private void LoadIncomeLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Income;
                resultArgs = ledgerSystem.FetchLedgerByNature();

                if (resultArgs != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkIncomeLedger, resultArgs.DataSource.Table,
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
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkExpenseLedger, resultArgs.DataSource.Table,
                        ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private ResultArgs ConstructStockLedgers()
        {
            DataView dvstockLedgers = null;
            DataTable dtstockLedgers = null;
            try
            {
                StockLedgerType stockLedgerType = new StockLedgerType();
                dvstockLedgers = this.UtilityMember.EnumSet.GetEnumDataSource(stockLedgerType, Sorting.Ascending);
                dtstockLedgers = dvstockLedgers.ToTable();
                if (dtstockLedgers != null)
                {
                    dtstockLedgers.Columns.Add("VALUE", typeof(int));
                    for (int i = 0; i < dtstockLedgers.Rows.Count; i++)
                    {
                        StockLedgerType stockLedgerName = (StockLedgerType)Enum.Parse(typeof(StockLedgerType), dtstockLedgers.Rows[i][1].ToString());
                        int LedgerId = 0;
                        switch (stockLedgerName)
                        {
                            case StockLedgerType.IncomeLedger:
                                {
                                    LedgerId = (glkIncomeLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkIncomeLedger.EditValue.ToString()) : 0;
                                    break;
                                }
                            case StockLedgerType.ExpenseLedger:
                                {
                                    LedgerId = (glkExpenseLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkExpenseLedger.EditValue.ToString()) : 0;
                                    break;
                                }
                        }
                        dtstockLedgers.Rows[i][2] = LedgerId;
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
                resultArgs.DataSource.Data = dtstockLedgers;
            }
            return resultArgs;
        }

        private void AssignStockLedgers()
        {
            using (StockLedgerMapping LedgerMapping = new StockLedgerMapping())
            {
                glkExpenseLedger.EditValue = LedgerMapping.DisposalLedgerId;
                glkIncomeLedger.EditValue = LedgerMapping.AccountLedgerId;
            }
        }

        #endregion

        private void glkExpenseLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
            }
        }

        private void glkIncomeLedger_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                frmBank.ShowDialog();
                LoadIncomeLedgers();
            }
        }

        private void IsLedgerCreationAllowed()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.Yes)
            {
                glkExpenseLedger.Properties.Buttons[1].Visible = false;
                glkIncomeLedger.Properties.Buttons[1].Visible = false;
            }
        }
    }
}