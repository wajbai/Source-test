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
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace ACPP.Modules.Master
{
    public partial class frmLedgerAdd : frmbaseAdd
    {
        private ResultArgs resultArgs;
        AppSchemaSet appSchema = new AppSchemaSet();
        public frmLedgerAdd()
        {
            InitializeComponent();
        }

        private void LoadLedgerGroup()
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.LoadLedgerGroupSource();
                    lkpLedgerGroup.Properties.DataSource = null;
                    lkpLedgerGroup.Properties.Columns.Clear();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindLookUpEdit(lkpLedgerGroup, resultArgs.DataSource.Table, appSchema.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), appSchema.AppSchema.LedgerGroup.GROUP_IDColumn.ToString(), 0);
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void lkpLedgerGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpLedgerGroup.EditValue.ToString() == "12")
            {
                try
                {
                    frmBankAccountAdd frmBankAccount = new frmBankAccountAdd();
                    frmBankAccount.ShowDialog();
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message);
                }
                finally { }
            }
        }

        private void frmLedgerAdd_Load(object sender, EventArgs e)
        {
            LoadLedgerGroup();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }
    }
}