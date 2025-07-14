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
using System.IO;
using System.Threading;
using Bosco.Model.Dsync;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;

namespace ACPP.Modules.Dsync
{
    public partial class frmUnMappedLedgers : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        public DataTable dtUNMappedLedger = new DataTable();
        private bool IsMappingRequired = false;
        #endregion

        #region Constructor
        public frmUnMappedLedgers()
        {
            InitializeComponent();
        }
        public frmUnMappedLedgers(DataTable dtLedgers, bool mapAll)
            : this()
        {
            IsMappingRequired = mapAll;
            dtUNMappedLedger = dtLedgers;
        }

        #endregion

        #region Events
        /// <summary>
        /// Load the Headofffice and Branchoffice Mapped and Unmapped Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUnMappedLedgers_Load(object sender, EventArgs e)
        {
            LoadUnMappedLedgers();
            LoadMappingHeadOffice();
        }

        /// <summary>
        /// To Map Headoffice ledger to Branchoffice Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapLedgers_Click(object sender, EventArgs e)
        {
            if (IsMappingRequired)
            {
                if (IsValidMismatchedGrid())
                {
                    MapHeadOfficeBranchOfficeLedger();
                }
            }
            else
            {
                MapHeadOfficeBranchOfficeLedger();
            }
        }

        private bool IsValidMismatchedGrid()
        {
            DataTable dtNewLedgers = gcUnMappedLedgers.DataSource as DataTable;
            int LedgerId = 0;
            int RowPosition = 0;
            bool isValid = true;

            if (dtNewLedgers != null && dtNewLedgers.Rows.Count > 0)
            {
                foreach (DataRow drTrans in dtNewLedgers.Rows)
                {
                    LedgerId = this.UtilityMember.NumberSet.ToInteger(drTrans["HEADOFFICE_LEDGER_ID"].ToString());

                    if (LedgerId == 0) //&& !(Id == 0 && Amt == 0))
                    {
                        //XtraMessageBox.Show("Kindly map all mismatched branch ledgers with Head office ledger and try again.", this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MAP_MISMATCHED_LEDGER_WITH_HO_LEDGER), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;
                }

                if (!isValid)
                {
                    gvUnmappedLedgers.FocusedColumn = capHeadOfficeLedger;
                    gvUnmappedLedgers.CloseEditor();
                    gvUnmappedLedgers.FocusedRowHandle = gvUnmappedLedgers.GetRowHandle(RowPosition);
                    gvUnmappedLedgers.ShowEditor();
                }
            }
            return isValid;
        }

        /// <summary>
        /// load lookup of Edit and assign Members
        /// </summary>
        private void LoadMappingHeadOffice()
        {
            try
            {
                using (ExportVoucherSystem exportVoucher = new ExportVoucherSystem())
                {
                    resultArgs = exportVoucher.HeadOfficeLedgers();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        rglkpHOLedgers.DataSource = resultArgs.DataSource.Table;
                        rglkpHOLedgers.ValueMember = "HEADOFFICE_LEDGER_ID";
                        rglkpHOLedgers.DisplayMember = "HEADOFFICELEDGER";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        private void gvUnmappedLedgers_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                //Filter Head Office ledgers based on active brach office ledger group.
                // for ex: FD ledgers (GroupID=14), should map only head office fd leadgers 
                DataRow drBrach = null;
                int BranchLedgerGroupId = 0;
                int BranchFDInterestLedgerId = 0;
                int BranchFDPenaltyLedger = 0;
                int BranchSBInterestLedger = 0;
                int BranchBankCommissionLedger = 0;

                string filter = string.Empty;

                ColumnView cview = (ColumnView)sender;
                if (cview.FocusedColumn.FieldName == colHeadOfficeId.FieldName && cview.FocusedValue != null)
                {
                    GridLookUpEdit grdlkp = (GridLookUpEdit)cview.ActiveEditor;
                    drBrach = gvUnmappedLedgers.GetDataRow(gvUnmappedLedgers.FocusedRowHandle);
                    if (drBrach != null)
                    {
                        BranchLedgerGroupId = UtilityMember.NumberSet.ToInteger(drBrach["GROUP_ID"].ToString());
                        BranchFDInterestLedgerId = UtilityMember.NumberSet.ToInteger(drBrach["IS_BANK_INTEREST_LEDGER"].ToString());
                        
                        //On 13/09/2022, to Handle FD Penatly, SB interest and Commission Ledgers
                        BranchFDPenaltyLedger = UtilityMember.NumberSet.ToInteger(drBrach["IS_BANK_FD_PENALTY_LEDGER"].ToString());
                        BranchSBInterestLedger = UtilityMember.NumberSet.ToInteger(drBrach["IS_BANK_SB_INTEREST_LEDGER"].ToString());
                        BranchBankCommissionLedger= UtilityMember.NumberSet.ToInteger(drBrach["IS_BANK_COMMISSION_LEDGER"].ToString());

                        if (BranchFDInterestLedgerId > 0)
                        {
                            filter = "IS_BANK_INTEREST_LEDGER = 1";
                        }
                        else if (BranchFDPenaltyLedger > 0)
                        {
                            filter = "IS_BANK_FD_PENALTY_LEDGER = 1";
                        }
                        else if (BranchSBInterestLedger > 0)
                        {
                            filter = "IS_BANK_SB_INTEREST_LEDGER = 1";
                        }
                        else if (BranchBankCommissionLedger > 0)
                        {
                            filter = "IS_BANK_COMMISSION_LEDGER = 1";
                        }
                        else if (BranchLedgerGroupId == (int)FixedLedgerGroup.FixedDeposit)
                        {
                            filter = "GROUP_ID=" + (int)FixedLedgerGroup.FixedDeposit;
                        }
                        else //On 06/07/2017, Load all ho ledgers other than FD ledger
                        {
                            filter = "GROUP_ID <> " + (int)FixedLedgerGroup.FixedDeposit;
                        }

                        DataTable dtHeadOfficeLedgers = grdlkp.Properties.DataSource as DataTable;
                        if (dtHeadOfficeLedgers != null && dtHeadOfficeLedgers.Rows.Count > 0)
                        {
                            DataView dvHeadOfficeLedgers = new DataView(dtHeadOfficeLedgers);
                            dvHeadOfficeLedgers.RowFilter = filter;
                            if (dvHeadOfficeLedgers != null)
                            {
                                grdlkp.Properties.DataSource = dvHeadOfficeLedgers.ToTable();
                                this.gvUnmappedLedgers.PostEditor();
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
        #endregion

        #region Methods
        /// <summary>
        /// Load Ledgers Mapped and UnMapped ledgers
        /// </summary>
        public void LoadUnMappedLedgers()
        {
            try
            {
                if (dtUNMappedLedger.Rows.Count > 0)
                {
                    gcUnMappedLedgers.DataSource = dtUNMappedLedger;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Map Branchoffice and Headoffice Ledgers
        /// </summary>
        public void MapHeadOfficeBranchOfficeLedger()
        {
            resultArgs.Success = true;
            try
            {
                using (ImportMasterSystem importSystem = new ImportMasterSystem())
                {
                    //this.ShowWaitDialog("Mapping Branch Ledgers with Head Office Ledger..");
                    this.ShowWaitDialog(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MAP_BRANCH_LEDGER_WITH_HO_LEDGER));
                    DataTable dtLedgers = gcUnMappedLedgers.DataSource as DataTable;
                    dtLedgers = dtLedgers.GetChanges(DataRowState.Modified); // By aldrin to updte only the modified rows.
                    foreach (DataRow dr in dtLedgers.Rows)
                    {
                        int BranchLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());

                        if (BranchLedgerId > 0)
                        {
                            importSystem.LedgerId = BranchLedgerId;
                            resultArgs = importSystem.DeleteMappedLedger();
                        }

                        int HeadOfficeLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["HEADOFFICE_LEDGER_ID"].ToString());
                        if (HeadOfficeLedgerId > 0)
                        {
                            if (resultArgs.Success)
                            {
                                importSystem.LedgerId = BranchLedgerId;
                                importSystem.HeadOfficeLedgerId = HeadOfficeLedgerId;
                                resultArgs = importSystem.MapHeadOfficeLedger();

                                //On 06/07/2018, update newly created BO ledger's Budget Group and Budget Sub Group from HO ledger's Budget Group and Budget Sub Group
                                if (resultArgs.Success)
                                {
                                    resultArgs = importSystem.UpdateLedgerBudgetGroup();
                                }
                            }
                        }
                    }
                    CloseWaitDialog();
                }
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                CloseWaitDialog();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.ShowSuccessMessage("Branch Office Ledgers Mapped to Head Office Ledgers Successfully");
                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MAP_BO_LEDGER_MAPPED_WITH_HO_LEDGER_SUCCESS));
                this.Close();
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rglkpHOLedgers_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                (sender as GridLookUpEdit).EditValue = null;
            }
        }


    }
}