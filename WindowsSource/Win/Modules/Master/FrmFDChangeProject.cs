//This form is to show the List of renewals/Post Interest that the selected FD Account contains
//Make user to select recnt Renewal/Post Interest
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Transaction;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.UIModel;
using AcMEDSync.Model;

namespace ACPP.Modules.Master
{
    public partial class FrmFDChangeProject: frmFinanceBase
    {
        #region Variables
        private bool SuccessfullyUpdated = false;
        private int FDAccountId { get; set; }
        private string FDAccountNumber { get; set; }
        private int ProjectId { get; set; }
        private FDTypes fdtype { get; set; }
        private int FDLedgerId { get; set; }
        private int InvestedBankLedgerId { get; set; }
        private Int32 NewProjectId
        {
            get
            {
                Int32 newprojectid = glkpProDetails.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProDetails.EditValue.ToString()) : 0;
                return newprojectid;
            }
        }
        private string FDRefreshDate { get; set; }
        private DateTime FDCreatedOn { get; set; }
        private DateTime FDLastDate { get; set; }

        DataTable dtRenewalsBankLedgers = new DataTable();

        #endregion
                      

        #region Constructor

        public FrmFDChangeProject()
        {
            InitializeComponent();
        }
         
        public FrmFDChangeProject(int fdAccountId) : this()
        {
            FDAccountId = fdAccountId;
            FillFDAccountDetails();
            this.Text = "FD Project Change";            
        }

        #endregion
       
        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(FDAccountNumber)) && ProjectId > 0 && NewProjectId > 0 && FDLedgerId > 0 && FDAccountId > 0)
            {
                if (this.ShowConfirmationMessage("Are you sure to change FD Account's Project to '" + glkpProDetails.Text + "'?" + System.Environment.NewLine + System.Environment.NewLine
                    + "(It will map affected Ledgers and refresh Project Balance from " + FDRefreshDate + ")", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (ValidateBankLedgerMapped())
                    {
                        bool rtnExistingProjectLock = this.IsVoucherLockedForDateRange(ProjectId, FDCreatedOn, FDLastDate, true, lblProject.Text);
                        if (!rtnExistingProjectLock)
                        {
                            bool rtnNewProject = this.IsVoucherLockedForDateRange(NewProjectId, FDCreatedOn, FDLastDate, true, glkpProDetails.Text);
                            if (!rtnNewProject)
                            {
                                using (FDAccountSystem fdsystem = new FDAccountSystem())
                                {
                                    ResultArgs result = fdsystem.ChangeFDProject(FDAccountId, FDAccountNumber, fdtype, ProjectId, NewProjectId, FDLedgerId);
                                    bool refreshed = false;
                                    if (result.Success)
                                    {
                                        using (BalanceSystem balanceSystem = new BalanceSystem())
                                        {
                                            this.ShowWaitDialog("Refresh Project's Balance from " + FDRefreshDate);
                                            balanceSystem.ProjectId = ProjectId;
                                            balanceSystem.VoucherDate = FDRefreshDate;
                                            result = balanceSystem.UpdateBulkTransBalance();
                                            if (result.Success)
                                            {
                                                balanceSystem.ProjectId = NewProjectId;
                                                result = balanceSystem.UpdateBulkTransBalance();
                                                refreshed = true;
                                            }
                                            this.CloseWaitDialog();
                                        }
                                        if (refreshed)
                                        {
                                            SuccessfullyUpdated = true;
                                            DialogResult = System.Windows.Forms.DialogResult.OK;
                                            this.ShowMessageBox("Successfully changed FD Account's Project");
                                        }
                                        else
                                        {
                                            this.ShowMessageBox("Successfully changed FD Account's Project, but not refreshed, " + result.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        glkpProDetails.Select();
                        glkpProDetails.Focus();
                    }
                }
            }
            else
            {
                this.ShowMessageBox("Select Project");
                glkpProDetails.Select();
                glkpProDetails.Focus();
            }
        }

      
       
        #endregion

        #region Methods

        private void FillFDAccountDetails()
        {
            using (FDAccountSystem fdsystem = new FDAccountSystem())
            {
                fdsystem.FillFDAccountDetails(FDAccountId);

                lblProject.Text = string.Empty;
                lblFDLedger.Text = string.Empty;
                lblFDAccount.Text = string.Empty;
                lblOP_Invest.Text = string.Empty;
                lblBankLedger.Text =  " " ;
                ProjectId = InvestedBankLedgerId = FDLedgerId = 0;
                FDAccountNumber = string.Empty;
                fdtype = FDTypes.NONE;
                FDRefreshDate = string.Empty;
                FDCreatedOn = DateTime.MinValue;
                FDLastDate = DateTime.MinValue;

                if (fdsystem.FDAccountId > 0)
                {
                    FDAccountNumber = fdsystem.FDAccountNumber;
                    if (fdsystem.FDTransType.ToUpper() == FDTypes.OP.ToString().ToUpper())
                    {
                        fdtype = FDTypes.OP;
                    }
                    else if (fdsystem.FDTransType.ToUpper() == FDTypes.IN.ToString().ToUpper())
                    {
                        fdtype = FDTypes.IN;
                    }

                    ProjectId = fdsystem.ProjectId;
                    FDLedgerId = fdsystem.LedgerId;
                    lblProject.Text = fdsystem.FDProjectName;
                    lblFDLedger.Text = fdsystem.FDLedgerName;
                    lblFDAccount.Text = fdsystem.FDAccountNumber;
                    InvestedBankLedgerId = fdsystem.BankLedgerId;

                    if (InvestedBankLedgerId > 0)
                    {
                        using (LedgerSystem ledgersystem = new LedgerSystem())
                        {
                            lblBankLedger.Text = ledgersystem.GetLegerName(InvestedBankLedgerId);
                        }
                    }

                    double fdamount = fdsystem.FdAmount;
                    FDRefreshDate = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false).ToShortDateString();
                    if (fdsystem.FDTransType.ToUpper() == FDTypes.OP.ToString().ToUpper())
                    {
                        lblOP_Invest.Text = "As Opening " + " on " + FDRefreshDate + " (Amount : " + UtilityMember.NumberSet.ToNumber(fdamount) + ")"; 
                    }
                    else if (fdsystem.FDTransType.ToUpper() == FDTypes.IN.ToString().ToUpper())
                    {
                        FDRefreshDate = UtilityMember.DateSet.ToDate(fdsystem.CreatedOn.ToShortDateString(), false).ToShortDateString();
                        lblOP_Invest.Text = "As Invested " + " on " + FDRefreshDate + " (Amount : " + UtilityMember.NumberSet.ToNumber(fdamount) + ")"; 
                    }

                    lblNote.Text = "Both Projects Balance's will refresh from " + FDRefreshDate;
                    FDCreatedOn = UtilityMember.DateSet.ToDate(FDRefreshDate, false);
                    FDLastDate = FDCreatedOn;
                }

                ResultArgs result = fdsystem.FetchFDHistoryByFDId(FDAccountId.ToString());
                if (result.Success && result.DataSource.Table!=null)
                {
                    DataTable dtRenewals = result.DataSource.Table;
                    gcRenewalsView.DataSource = dtRenewals;

                    if (dtRenewals != null && dtRenewals.Rows.Count > 0)
                    {
                        dtRenewalsBankLedgers = dtRenewals.DefaultView.ToTable(true, new string[] { "BANK_LEDGER_ID" });
                        
                        //Last date of Renewals
                        FDLastDate = UtilityMember.DateSet.ToDate(dtRenewals.Compute("MAX(RENEWAL_DATE)", string.Empty).ToString(), false);
                        
                    }
                                        
                }

                LoadProject();
            }
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingProject = new MappingSystem())
                {
                    mappingProject.ProjectClosedDate = this.AppSetting.YearFrom;
                    ResultArgs resultArgs = mappingProject.FetchProjectsLookup();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataTable dtChangeProjects = resultArgs.DataSource.Table;
                        dtChangeProjects.DefaultView.RowFilter = "PROJECT_ID <> " + ProjectId;
                        dtChangeProjects = dtChangeProjects.DefaultView.ToTable();
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProDetails, resultArgs.DataSource.Table, mappingProject.AppSchema.Project.PROJECTColumn.ColumnName, mappingProject.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }


        private bool isBankLedgerMapped(int bankledgerid)
        {
            bool rtn = false;
            if (bankledgerid > 0)
            {
                using (VoucherTransactionSystem vouchertrans = new VoucherTransactionSystem())
                {
                    ResultArgs resultArgs = vouchertrans.CheckLedgerMappedByProject(bankledgerid, NewProjectId);
                    if (resultArgs.Success && resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                    {
                        rtn = true;
                    }
                }
            }
            else
            {
                rtn = true;
            }
            return rtn;
        }

        private bool ValidateBankLedgerMapped()
        {
            bool rtn = false;
            bool IsInvestedBankLedgerMapped = isBankLedgerMapped(InvestedBankLedgerId);
            rtn = IsInvestedBankLedgerMapped;
 
            if (IsInvestedBankLedgerMapped)
            {
                using (VoucherTransactionSystem vouchertrans = new VoucherTransactionSystem())
                {
                    foreach (DataRow dr in dtRenewalsBankLedgers.Rows)
                    {
                        int renewalsbankledgerid = UtilityMember.NumberSet.ToInteger(dr["BANK_LEDGER_ID"].ToString());
                        rtn = isBankLedgerMapped(renewalsbankledgerid);
                        if (rtn==false)
                        {
                            break;
                        }
                    }
                }
            }

            if (rtn == false && this.ShowConfirmationMessage("Bank Ledger '" + lblBankLedger.Text + "' is not mapped for selected Project '" + glkpProDetails.Text + "'." + System.Environment.NewLine + System.Environment.NewLine
                        + "Do you want to map Bank Ledger and Proceed ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                rtn = true;
            }

            return rtn;
        }

        #endregion

    }
}
