using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Bosco.Utility;
using Bosco.Model.UIModel;
using Bosco.DAO.Schema;

namespace ACPP.Modules.Master
{
    public partial class frmAccountLedgerAdd : frmbaseAdd
    {
        ResultArgs resultArgs = null;
        AppSchemaSet appSchema = new AppSchemaSet();
        public event EventHandler UpdateHeld;
        private int FormWidth = 0;
        private int LedgerAccId = 0;
        public frmAccountLedgerAdd()
        {
            InitializeComponent();
        }

        public frmAccountLedgerAdd(int LedgerId) :this()
        {
            LedgerAccId = LedgerId;
        }

        private void frmBankAccountAdd_Load(object sender, EventArgs e)
        {
            LoadBankAccoutType();
            LoadLedgerGroup();
            ShowLayoutControl(false, LayoutVisibility.Never);
            //pnlBankInfo.Visible = false;
            //lcgBankAccounts.Visibility = LayoutVisibility.Never;
        }

        private void lkpAccountType_EditValueChanged(object sender, EventArgs e)
        {
            ShowControls(this.UtilityMember.NumberSet.ToInteger(lkpAccountType.EditValue.ToString()));
        }

        private void lkpLedgerGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpLedgerGroup.EditValue.ToString() == "12")
            {
                //pnlBankInfo.Visible= true;
                //lcgBankAccounts.Visibility = LayoutVisibility.Always;
                ShowLayoutControl(true, LayoutVisibility.Always);
                FormWidth = 150;
                ShowControls(this.UtilityMember.NumberSet.ToInteger(lkpAccountType.EditValue.ToString()));
            }
            else
            {
                //pnlBankInfo.Visible = false;
                //lcgBankAccounts.Visibility = LayoutVisibility.Never;
                ShowLayoutControl(false, LayoutVisibility.Never);
                this.Size = new Size(430, 180);
            }
        }

        #region Methods
        private void LoadBankAccoutType()
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.GetAccountType();
                    lkpLedgerGroup.Properties.DataSource = null;
                    lkpLedgerGroup.Properties.Columns.Clear();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindLookUpEdit(lkpAccountType, resultArgs.DataSource.Table, "ACCOUNT_TYPE", "ACCOUNT_TYPE_ID", 0);
                        lkpAccountType.EditValue = lkpAccountType.Properties.GetDataSourceValue(lkpAccountType.Properties.ValueMember, 0);
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

        private void ShowControls(int AccountType)
        {
            if (AccountType == (int)BankAccoutType.SavingAccount)
            {
                lblDateClosed.Visibility = LayoutVisibility.Always;


                lblPeriod.Visibility = LayoutVisibility.Never;
                lblMaturityDate.Visibility = LayoutVisibility.Never;
                lblIntetestDate.Visibility = LayoutVisibility.Never;
                lblPremiumDuration.Visibility = LayoutVisibility.Never;
                lblPremiumAmt.Visibility = LayoutVisibility.Never;
                lblIntetestDate.Visibility = LayoutVisibility.Never;
                lblShares.Visibility = LayoutVisibility.Never;

                this.lblMaturityDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblIntetestDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPremiumDuration.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPremiumAmt.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblDateClosed.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPeriod.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.Size = new Size(430, FormWidth + 170);
            }
            else if (AccountType == (int)BankAccoutType.FixedDeposit)
            {
                lblPeriod.Visibility = LayoutVisibility.Always;
                lblMaturityDate.Visibility = LayoutVisibility.Always;
                lblIntetestDate.Visibility = LayoutVisibility.Always;

                this.lblPeriod.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
                this.lblMaturityDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
                this.lblIntetestDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);

                this.lblDateClosed.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPremiumDuration.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPremiumAmt.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblDateClosed.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);

                lblDateClosed.Visibility = LayoutVisibility.Never;
                lblPremiumDuration.Visibility = LayoutVisibility.Never;
                lblPremiumAmt.Visibility = LayoutVisibility.Never;
                lblShares.Visibility = LayoutVisibility.Never;
                this.Size = new Size(430, FormWidth + 220);
            }
            else if (AccountType == (int)BankAccoutType.MutualFund || AccountType == (int)BankAccoutType.RecurringDeposit)
            {
                lblPremiumDuration.Visibility = LayoutVisibility.Always;
                lblPremiumAmt.Visibility = LayoutVisibility.Always;
                lblMaturityDate.Visibility = LayoutVisibility.Always;
                lblIntetestDate.Visibility = LayoutVisibility.Always;
                lblDateClosed.Visibility = LayoutVisibility.Never;
                lblPeriod.Visibility = LayoutVisibility.Never;
                lblShares.Visibility = LayoutVisibility.Never;
                this.lblPeriod.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblMaturityDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
                this.lblIntetestDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);

                this.lblDateClosed.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPremiumDuration.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
                this.lblPremiumAmt.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
                this.lblDateClosed.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
                this.Size = new Size(430, FormWidth + 241);
            }
            else if (AccountType == (int)BankAccoutType.Equity)
            {
                lblShares.Visibility = LayoutVisibility.Always;
                lblPeriod.Visibility = LayoutVisibility.Never;
                lblMaturityDate.Visibility = LayoutVisibility.Never;
                lblIntetestDate.Visibility = LayoutVisibility.Never;
                lblPremiumDuration.Visibility = LayoutVisibility.Never;
                lblPremiumAmt.Visibility = LayoutVisibility.Never;
                lblIntetestDate.Visibility = LayoutVisibility.Never;
                lblDateClosed.Visibility = LayoutVisibility.Never;
                this.lblDateClosed.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblMaturityDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblIntetestDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPremiumDuration.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPremiumAmt.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblDateClosed.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblPeriod.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.lblShares.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
                this.Size = new Size(430, FormWidth + 180);
            }
            this.CenterToParent();
        }

        private void ShowLayoutControl(bool Show, LayoutVisibility Visible)
        {
            pnlBankInfo.Visible = Show;
            lcgBankAccounts.Visibility = Visible;
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.ShowWaitDialog();
            if (ValidateLedger())
            {
                try
                {
                    using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                    {

                        ledgerSystem.Abbrevation = txtLedgerCode.Text.Trim();
                        ledgerSystem.Group = txtLedgerName.Text.Trim();
                        ledgerSystem.ParentGroupId = this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString());
                        ledgerSystem.NatureId = ledgerSystem.GetNatureId(this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString()));
                        ledgerSystem.MainGroupId = this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString());
                        ledgerSystem.GroupId = (LedgerAccId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : LedgerAccId;
                        ledgerSystem.ImageId = 1;

                        //resultArgs = ledgerSystem.s
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Group.GROUP_SAVE_SUCCESS));
                            txtLedgerCode.Focus();
                            LoadLedgerGroup();
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
                    MessageRender.ShowMessage(Ex.Message);
                }
                finally
                {
                    this.CloseWaitDialog();
                }
            }
        }


        private bool ValidateLedger()
        {
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

    }
}