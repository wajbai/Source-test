using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.Business;
using ACPP.Modules.Master;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using AcMEDSync.Model;
using System.Threading.Tasks;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmResetLedgerOPBalance : frmFinanceBaseAdd
    {
        bool ResetSuccessfully = false;
        #region Properties
        private Int32 ProjectId
        {
            get
            {
                Int32 projectid = glkpProject.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                return projectid;
            }
        }

        private Int32 NatureId
        {
            get
            {
                Int32 natureid = lkpNature.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(lkpNature.EditValue.ToString());
                return natureid;
            }
        }

        private Int32 MainGroupId
        {
            get
            {
                Int32 groupid = lkpLedgerGroup.EditValue == null ? -1 : this.UtilityMember.NumberSet.ToInteger(lkpLedgerGroup.EditValue.ToString());
                return groupid;
            }
        }
        #endregion

        public frmResetLedgerOPBalance(Int32 projectId)
        {
            InitializeComponent();
            LoadProject();
            LoadNature();
            lblNote.Text = "Selected Nature/Group Ledger's Opening Balance will be reset as 0.00. It will affect and refresh from " + UtilityMember.DateSet.ToDate(AppSetting.BookBeginFrom, false).ToShortDateString() + ".";
            this.Text = "Reset Ledger Opening Balance as on Books Begin (" + UtilityMember.DateSet.ToDate(AppSetting.BookBeginFrom, false).ToShortDateString() + ")";
            if (projectId > 0)
            {
                glkpProject.EditValue = projectId;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBalanceRefresh_Load(object sender, EventArgs e)
        {
            //LoadProject();
            //glkpProject.EditValue = ProjectId;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void lkpNature_EditValueChanged(object sender, EventArgs e)
        {
            LoadLedgerGroup();
            ShowCashBankLedgers();
        }

        private void lkpLedgerGroup_EditValueChanged(object sender, EventArgs e)
        {
            ShowCashBankLedgers();
        }

        private void BtnUpdateOpBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProjectId > 0)
                {
                    if (NatureId > 0)
                    {
                        if (MainGroupId >= 0)
                        {
                            bool bIncludeCashLedgers = chkIncludeCashLedgers.Checked;
                            bool bIncludeBankLedgers = chkIncludeBankLedgers.Checked;
                            bool bIncludeFDLedgers = false;
                            string strMessage = "Are you sure to update ledger Opening Balance?";
                            string cashbankledgers = string.Empty;
                            if (bIncludeCashLedgers && bIncludeBankLedgers)
                                cashbankledgers = "Which includes Cash and Bank Ledgers.";
                            else if (bIncludeCashLedgers)
                                cashbankledgers = "Which includes Cash Ledgers.";
                            else if (bIncludeBankLedgers)
                                cashbankledgers = "Which includes Bank Ledgers.";
                            
                            if (NatureId > 0 && MainGroupId == 0)
                                strMessage = "Are you sure to update all '" + lkpNature.Text + "' Ledger's Opening Balance as 0.0." + cashbankledgers + "?";
                            else if (MainGroupId > 0)
                                strMessage = "Are you sure to update all '" + lkpLedgerGroup.Text + "' Ledger's Opening Balance as 0.0." + cashbankledgers + "?";

                            strMessage += System.Environment.NewLine + System.Environment.NewLine;
                            strMessage += "Yes      : Update selected Nature/Main Group Ledger's Opening Balance is 0.00 and Refresh from Books Begin for selected Project." + System.Environment.NewLine;
                            strMessage += "No       : Cancel the process and leave as it is." + System.Environment.NewLine;
                            //strMessage += "Cancel : Cancel the process and leave as it is." + System.Environment.NewLine;
                            DialogResult result = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                this.ShowWaitDialog("Refreshing Ledger Balances");

                                using (BalanceSystem balanceSystem = new BalanceSystem())
                                {
                                    ResultArgs resultArg = balanceSystem.ResetLedgerOpeningBalance(ProjectId , NatureId, MainGroupId, 
                                                bIncludeCashLedgers, bIncludeBankLedgers, bIncludeFDLedgers);
                                    if (resultArg.Success)
                                    {
                                        DialogResult = System.Windows.Forms.DialogResult.Yes;
                                        ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.BALANCE_UPDATED));
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.ShowMessageBox("Select Ledger Main Group");
                            lkpLedgerGroup.Select();
                            lkpLedgerGroup.Focus();
                        }
                    }
                    else
                    {
                        this.ShowMessageBox("Select Nature of Ledgers");
                        lkpNature.Select();
                        lkpNature.Focus();
                    }
                }
                else
                {
                    this.ShowMessageBox("Project is empty");
                    glkpProject.Select();
                    glkpProject.Focus();
                }
            }
            catch (Exception ee)
            {
                ShowMessageBox(ee.Message);
            }
            finally { this.CloseWaitDialog(); }
        }

        #region Methods
        private void LoadProject()
        {
            try
            {
                using (ProjectSystem loadprojects = new ProjectSystem())
                {
                    //deDateFrom.DateTime = UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false);
                    ResultArgs resultArgs = loadprojects.FetchProjectlistDetails();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            DataTable dtProject = resultArgs.DataSource.Table;//SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, loadprojects.AppSchema.Project.PROJECT_IDColumn.ColumnName, loadprojects.AppSchema.Project.PROJECTColumn.ColumnName);
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, dtProject, loadprojects.AppSchema.Project.PROJECTColumn.ColumnName, loadprojects.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            //glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                        }
                    }
                    //else
                    //{
                    //    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Transaction.VocherTransaction.PROJECT_IS_NOT_CREATED), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //        ShowProjectForm();
                    //}
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// To load the Ledger Group
        /// </summary>
        private void LoadLedgerGroup()
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    ResultArgs resultArgs = ledgerSystem.FetchLedgerGroupByNature(NatureId);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtLedgerGroup = resultArgs.DataSource.Table;
                        DataRow dr = dtLedgerGroup.NewRow();
                        dr[ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName] = 0;
                        dr[ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName] = "<--All-->";
                        dtLedgerGroup.Rows.InsertAt(dr, 0);
                        
                        if (NatureId == (Int32)Natures.Assert)
                        {
                            dr = dtLedgerGroup.NewRow();
                            dr[ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName] = (Int32)FixedLedgerGroup.Cash;
                            dr[ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName] = FixedLedgerGroup.Cash.ToString();
                            dtLedgerGroup.Rows.InsertAt(dr, dtLedgerGroup.Rows.Count);

                            dr = dtLedgerGroup.NewRow();
                            dr[ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName] = (Int32)FixedLedgerGroup.BankAccounts;
                            dr[ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName] = FixedLedgerGroup.BankAccounts.ToString();
                            dtLedgerGroup.Rows.InsertAt(dr, dtLedgerGroup.Rows.Count);
                        }

                        this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpLedgerGroup, dtLedgerGroup, ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(),
                                    ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());

                        lkpLedgerGroup.ItemIndex = 0;
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

        /// <summary>
        /// To load the Ledger Group
        /// </summary>
        private void LoadNature()
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    ResultArgs resultArgs = ledgerSystem.FetchLedgerGroupNature();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpNature, resultArgs.DataSource.Table, ledgerSystem.AppSchema.LedgerGroup.NATUREColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.NATURE_IDColumn.ToString());
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

        private void ShowProjectForm()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                frmProject.ShowDialog();
                if (frmProject.DialogResult == DialogResult.Yes)
                {
                    LoadProject();
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private void ShowCashBankLedgers()
        {
            lcChkCashLedgers.Visibility = lcchkBankLedgers.Visibility = lcExcludeNote.Visibility =  DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            chkIncludeCashLedgers.Checked = chkIncludeBankLedgers.Checked = false;
            if ((NatureId == (int)Natures.Assert) && (MainGroupId == 0 || MainGroupId == 11)) //For Current Asset
            {
                lcChkCashLedgers.Visibility = lcchkBankLedgers.Visibility = lcExcludeNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
        #endregion

    }
}