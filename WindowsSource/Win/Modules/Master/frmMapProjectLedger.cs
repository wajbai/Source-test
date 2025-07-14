/************************************************************************************************************************
 *                                              Form Name  :frmMapProjectLedger.cs
 *                                              Purpose    :To map project,ledgers,cost center and Donor
 *                                              Author     : Carmel Raj M
 ************************************************************************************************************************/
using System;
using System.Data;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;
using System.Linq;
using DevExpress.XtraGrid;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

using Bosco.Model.UIModel.Master;
using ACPP.Modules.Data_Utility;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraEditors.ViewInfo;

namespace ACPP.Modules.Master
{
    public partial class frmMapProjectLedger : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        DialogResult mappingDialogResultLedger = DialogResult.Cancel;
        DialogResult mappingDialogResultProject = DialogResult.Cancel;
        DialogResult mappingDialogResultCostCentre = DialogResult.Cancel;
        DialogResult MappingDialogResultPurpose = DialogResult.Cancel;
        DialogResult mappingDialogResultDonor = DialogResult.Cancel;
        DialogResult mappingDialogResultVoucher = DialogResult.Cancel;
        DialogResult mappingDialogResultCostCategory = DialogResult.Cancel;
        DialogResult mappingDialogResultGeneralate = DialogResult.Cancel;
        DialogResult mappingDialogResultCurrency = DialogResult.Cancel;
        BankAccountSystem FDUpdation = new BankAccountSystem();
        DataTable dtFDBreakUp;
        LedgerSystem dataManager = new LedgerSystem();
        DataTable dtBalanceUpdate;
        public DataTable dtFDDetailsForBreakup;
        public DataTable dtBreakUpdetails;
        double Credit = 0.00;
        double Debit = 0.00;
        bool IsFDLedger = false;
        const int LEDGER_FOCUS_COL_INDEX = 4; //Index of the Amount Column
        const int PROJECT_FOCUS_COL_INDEX = 2; //Index of the Amount Column
        const int COSTCENTER_FOCUS_COL_INDEX = 2;//Index of the Cost center Amount Column
        const string SELECT_COL = "SELECT";
        string NO_DIFFERENCE = string.Empty; //"No difference";
        string ProjectName = string.Empty;
        private DataTable dtVoucherTypes = new DataTable();
        private string frmName = string.Empty;
        #endregion

        #region Properties
        private DataTable dtLedgerDetails { get; set; }
        private int LedgerId { get; set; }
        private int ProjectId { get; set; }
        private int CostCenterId { get; set; }
        private Int32 CCMappLedgerId { get; set; }
        private double CCMappLedgerOpAmount { get; set; }
        private string CCMappLedgerOpTransMode { get; set; }
        private double CCTotalDistributedAmount { get; set; }
        private int CostCenterCategoryId { get; set; }
        private int DonorId { get; set; }
        private DataTable dtAllDonor { get; set; }
        private DataTable dtAllPurpose { get; set; }
        private DataTable dtAllProjectPurposeDistributed { get; set; }
        private DataTable dtAllProjectLedgerCCDistributed { get; set; }
        private DataTable dtCheckedItems { get; set; }
        // public DataTable dtBreakUpdetails { get; set; }
        private DataTable dtTransactionFD { get; set; }
        public string BreakUpAccountNo { get; set; }
        private string VoucherProjectId { get; set; }
        private string FrmName { get; set; }
        private DataTable dtAllGeneralateLedger { get; set; }

        private DataTable dtAllProjectLedgerApplicable { get; set; }

        #endregion

        #region Constructor
        public frmMapProjectLedger()
        {
            InitializeComponent();
        }

        public frmMapProjectLedger(MapForm IsForm, int MappingId, string ProjectName = "")
            : this()
        {
            FrmName = IsForm.ToString();
            switch (IsForm)
            {
                case MapForm.Project:
                    this.ProjectId = MappingId;
                    xtpProject.PageVisible = false;
                    xtpCostCentre.PageVisible = false;
                    xtpDonor.PageVisible = false;
                    xtpCostCategory.PageVisible = false;
                    xtpPurpose.PageVisible = false;
                    this.ProjectName = ProjectName;
                    break;
                case MapForm.Ledger:
                    this.LedgerId = MappingId;
                    xtpMapLedger.PageVisible = false;
                    xtpCostCentre.PageVisible = false;
                    xtpDonor.PageVisible = false;
                    xtpCostCategory.PageVisible = false;
                    xtpPurpose.PageVisible = false;
                    this.ProjectName = ProjectName;
                    break;
                case MapForm.CostCentre:
                    this.CostCenterId = MappingId;
                    xtpProject.PageVisible = false;
                    xtpMapLedger.PageVisible = false;
                    xtpDonor.PageVisible = false;
                    xtpCostCategory.PageVisible = false;
                    xtpPurpose.PageVisible = false;
                    this.ProjectName = ProjectName;
                    break;
                case MapForm.Donor:
                    this.DonorId = MappingId;
                    xtpProject.PageVisible = false;
                    xtpMapLedger.PageVisible = false;
                    xtpCostCentre.PageVisible = false;
                    xtpCostCategory.PageVisible = false;
                    xtpPurpose.PageVisible = false;
                    this.ProjectName = ProjectName;
                    break;

                //by aldrin for asset
                case MapForm.Asset:
                    //this.DonorId = MappingId;
                    xtpProject.PageVisible = false;
                    xtpCostCentre.PageVisible = true;
                    xtpCostCategory.PageVisible = false;
                    xtpMapvoucher.PageVisible = false;
                    xtpDonor.PageVisible = true;
                    xtpPurpose.PageVisible = true;
                    xtpMapLedger.PageVisible = true;
                    xtpMapBudgetLedger.PageVisible = true;
                    this.ProjectName = ProjectName;
                    break;
            }
        }
        #endregion

        #region Common Events
        private void frmMapProjectLedger_Load(object sender, EventArgs e)
        {
            LoadDefaultValues();
            NO_DIFFERENCE = this.GetMessage(MessageCatalog.Master.Mapping.NO_DIFFERENCE);
            gvLedgerDetail.ExpandAllGroups();
            if (!this.LoginUser.IsFullRightsReservedUser)
            {
                ApplyRights();
                xtcProject.SelectedTabPageIndex = 0;
            }
            else
            {
                // by aldrin for asset
                if (FrmName != MapForm.Asset.ToString())
                {
                    xtcProject.TabPages[0].PageVisible = xtcProject.TabPages[2].PageVisible = xtcProject.TabPages[3].PageVisible =
                    xtcProject.TabPages[4].PageVisible = xtcProject.TabPages[6].PageVisible = true;
                    xtcProject.TabPages[1].PageVisible = false; // to make unvisible commanded by chinna
                    xtcProject.TabPages[5].PageVisible = false; // Invisible Map Cost Center to Cost category
                    xtcProject.SelectedTabPageIndex = 0;
                }
                else
                {
                    chkLedgerSelectAll.Visible = false;
                    chkUnMapSelectAllLedgers.Visible = false;
                }
            }

            //On 11/05/2020, To show/hide "Reset Ledger Opening Balance" button based on finance setting
            lcRestLedgerOpeningBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.ShowResetLedgerOpeningBalance == 1)
            {
                lcRestLedgerOpeningBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            //On 05/09/2025, To show currency exchagne rate page
            xtcCurrencyExchangeRate.PageVisible = false;
            if (this.AppSetting.AllowMultiCurrency == 1)
            {
                xtcCurrencyExchangeRate.PageVisible = true;

                lcgCurrencyExchangeRate.Text = lcgCurrencyExchangeRate.Text + " for the period of " + UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false).ToShortDateString() +
                    " - " + UtilityMember.DateSet.ToDate(AppSetting.YearTo, false).ToShortDateString();
            }
           // xtpMapGeneralate.PageVisible = false;
        }

        private void frmMapProjectLedger_ShowFilterClicked(object sender, EventArgs e)
        {
            if (xtcProject.SelectedTabPageIndex == 0)
            {
                chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
            }
            if (xtcProject.SelectedTabPageIndex == 1)
            {
                chkShowFilterLedger.Checked = (chkShowFilterLedger.Checked) ? false : true;
            }
            if (xtcProject.SelectedTabPageIndex == 2)
            {
                chkCostCentreFilter.Checked = (chkCostCentreFilter.Checked) ? false : true;
            }
            if (xtcProject.SelectedTabPageIndex == 3)
            {
                chkShowFilterDonor.Checked = (chkShowFilterDonor.Checked) ? false : true;
            }
            if (xtcProject.SelectedTabPageIndex == 4)
            {
                chkShowFilterPurpose.Checked = (chkShowFilterPurpose.Checked) ? false : true;
            }
        }

        private void xtcProject_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtcProject.SelectedTabPage.Text.Equals(xtpMapLedger.Text))
            {
                SetAcceptCancelButton(btnSave, btnClose);
                GetTransactionFD();
                LoadProject(grdlProjectName);
                GetGridViewFocus(gvLedgerDetail, LEDGER_FOCUS_COL_INDEX, true);
                chkUnMapSelectAllLedgers.Visible = true;
                chkUnMapCostCenter.Visible = chkCCSelectedCostcentres.Visible = chkUnMapDonor.Visible = chkUnMapPurpose.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;

                lcLedgerCCDistributionNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (this.AppSetting.CostCeterMapping == 1)
                {
                    lcLedgerCCDistributionNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpProject.Text))
            {
                SetAcceptCancelButton(btnSaveProject, btnCloseProject);
                GetTransactionFD();
                LoadLedger();
                GetGridViewFocus(gvProjectDetails, PROJECT_FOCUS_COL_INDEX);
                chkUnMapSelectAllLedgers.Visible = chkCCSelectedCostcentres.Visible = chkUnMapCostCenter.Visible = chkUnMapDonor.Visible = chkUnMapPurpose.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpCostCentre.Text))
            {
                SetAcceptCancelButton(btnCostCenterSave, btnCostCenterClose);
                LoadProject(glkpCCProject);
                GetGridViewFocus(gvCostCentre, COSTCENTER_FOCUS_COL_INDEX);
                chkUnMapSelectAllLedgers.Visible = chkCCSelectedCostcentres.Visible = chkUnMapDonor.Visible = chkUnMapPurpose.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
                chkUnMapCostCenter.Visible = false;

                //On 18/11/2022, To show Ledger based on CC mapping -------------------------------------------------------------------
                chkUnMapCostCenter.Visible = true;
                lblCCLedgerOpBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCCLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcCCDistributionNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                chkUnMapCostCenter.Top = 100;
                if (this.AppSetting.CostCeterMapping == 1)
                {
                    chkUnMapCostCenter.Top = 127;
                    lblCCLedgerOpBalance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcCCLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcCCDistributionNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    LoadCCLedgerByProject();
                    //FetchMappedCostCenter();
                }
                //------------------------------------------------------------------------------------------------------------------------

                gvCostCentre.FocusedColumn = gvCostCentre.VisibleColumns[3];
                gvCostCentre.ShowEditor();
            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpDonor.Text))
            {
                SetAcceptCancelButton(btnDonorSave, btnDonorClose);
                LoadProject(glkpDonorProject);
                LoadAllDonors();
                FetchMappedDonor();
                chkLedgerSelectAll.Visible = chkCCSelectedCostcentres.Visible = chkUnMapCostCenter.Visible = chkUnMapSelectAllLedgers.Visible = chkUnMapPurpose.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
                chkUnMapDonor.Visible = true;
                lblAvailabledonorRecordCount.Text = GetCountListBox(chkAvailableDonors);


            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpMapvoucher.Text))
            {
                SetAcceptCancelButton(btnVoucherSave, btnVoucherclose);
                chkLedgerSelectAll.Visible = chkUnMapDonor.Visible = chkCCSelectedCostcentres.Visible = chkUnMapCostCenter.Visible = chkUnMapPurpose.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
                LoadProject(glkpProjectVoucher);
                FetchVoucherDetails();
                FetchDefaultProjectVouchers();
                chkUnMapSelectAllLedgers.Visible = false;
            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpCostCategory.Text))
            {
                chkUnMapSelectAllLedgers.Visible = chkUnMapCostCenter.Visible = chkCCSelectedCostcentres.Visible = chkUnMapDonor.Visible = chkUnMapPurpose.Visible = false; chkUnMapCostCenter.Visible = chkUnMapDonor.Visible = chkLedgerSelectAll.Visible = chkUnMapPurpose.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
                chkCCSelectedCostcentres.Visible = true;
                SetAcceptCancelButton(btnCCSave, btnCCClose);
                LoadCostCategory();
                lblCostCategoryMapRecordCount.Text = GetCountListBox(chkAvailableCategoryCostCenter);
            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpPurpose.Text))
            {
                SetAcceptCancelButton(btnPurposeSave, btnPurposeClose);
                chkUnMapSelectAllLedgers.Visible = chkUnMapCostCenter.Visible = chkCCSelectedCostcentres.Visible = chkUnMapDonor.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
                chkUnMapPurpose.Visible = true;
                GetGridViewFocus(gvPurpose, COSTCENTER_FOCUS_COL_INDEX);
                LoadProject(glkpPurposeProject);
                LoadAllPurpose();
                FetchMappedPurpose();
                lblAvailablePurposeRecordCount.Text = GetCountListBox(chkAvailablePurpose);
            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpMapBudgetLedger.Text))
            {
                SetAcceptCancelButton(btnSave, btnClose);
                chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = true;
                chkUnMapSelectAllLedgers.Visible = chkUnMapCostCenter.Visible = chkCCSelectedCostcentres.Visible = chkUnMapDonor.Visible = chkUnMapPurpose.Visible = chkLedgerSelectAll.Visible = chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
                LoadProject(glkpBudgetProject);

            }
            else if (xtcProject.SelectedTabPage.Text.Equals(xtpMapGeneralate.Text))
            {
                SetAcceptCancelButton(btnSaveGeneralate, btnGenClose);
                chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = true;
                chkUnMapSelectAllLedgers.Visible = chkUnMapCostCenter.Visible = chkCCSelectedCostcentres.Visible = chkUnMapDonor.Visible = chkUnMapPurpose.Visible = chkLedgerSelectAll.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = false;
                LoadGeneralateLedger();
            }
            else if (this.AppSetting.AllowMultiCurrency==1 && xtcProject.SelectedTabPage.Text.Equals(xtcCurrencyExchangeRate.Text))
            {   //For Currency Exchange Rate
                
                SetAcceptCancelButton(btnCurrencySave, btnCurrencyClose);
                chkUnSelectGenLedgers.Visible = chkAvailableGenLedger.Visible = false;
                chkUnMapSelectAllLedgers.Visible = chkUnMapCostCenter.Visible = chkCCSelectedCostcentres.Visible = false;
                chkUnMapDonor.Visible = chkUnMapPurpose.Visible = chkLedgerSelectAll.Visible = chkunMapSelectedBudgetLedger.Visible = chkAllAvailableBudgetLedger.Visible = false;
                LoadCurrencyCountry();
            }

            gvLedgerDetail.ExpandAllGroups();
        }
        #endregion

        #region Map Project

        #region Events
        private void gvLedgerDetail_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int accessFlag = 0;
            Int32 Lid = 0;
            DataRow drLedger = null;
            DataTable dtTransFDId = new DataTable();
            if (gvLedgerDetail.GetRowCellValue(gvLedgerDetail.FocusedRowHandle, colLedgerId) != null)
            {
                drLedger = gvLedgerDetail.GetDataRow(gvLedgerDetail.FocusedRowHandle);
                accessFlag = drLedger != null ? UtilityMember.NumberSet.ToInteger(drLedger["ACCESS_FLAG"].ToString()) : 0;
                Lid = drLedger != null ? UtilityMember.NumberSet.ToInteger(drLedger["LEDGER_ID"].ToString()) : 0;
                if (gvLedgerDetail.GetRowCellValue(gvLedgerDetail.FocusedRowHandle, colLedgerId.FieldName).ToString() != string.Empty)
                {
                    UInt32 BankId = (UInt32)gvLedgerDetail.GetRowCellValue(gvLedgerDetail.FocusedRowHandle, colLedgerId.FieldName);
                    if (dtTransactionFD != null && dtTransactionFD.Rows.Count > 0)
                    {
                        var TransFD = (from ledgers in dtTransactionFD.AsEnumerable()
                                       where ((ledgers.Field<UInt32>(colLedgerId.FieldName) == BankId))
                                       select ledgers);
                        if (TransFD.Count() > 0)
                        {
                            dtTransFDId = dtTransactionFD.Clone();
                            dtTransFDId = TransFD.CopyToDataTable();
                        }
                        if (dtTransFDId != null && dtTransFDId.Rows.Count > 0)
                        {
                            //UInt32 Bank = Convert.ToUInt32(dtTransFDId.Rows[0][0]);
                            //if (Bank.Equals(BankId))
                            //{
                            //    e.Cancel = true; //Disabling the editing of the cell
                            //    this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.TRANSACTION_FD));
                            //}
                            e.Cancel = true;
                            if (gvLedgerDetail.FocusedColumn.Name.Equals("colOPBalance"))
                            {
                                //this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.MAPPING_LEDGER_CANNOT_SET_OP_FD_LEDGER));
                            }
                        }
                        else
                        {
                            //if (gvLedgerDetail.FocusedColumn.Name.Equals(grdChkCol.Name) && BankId.Equals(1))
                            //{
                            //    e.Cancel = true; //Disabling the editing of the cell
                            //    this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.UNMAP_CASH_LEDGER));
                            //}

                            //By Aldrin. To validate default ledgers with acces flag.
                            if (gvLedgerDetail.FocusedColumn.Name.Equals(grdChkCol.Name) && accessFlag != 0 && accessFlag == (int)AccessFlag.Readonly)
                            {
                                e.Cancel = true;
                                //this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.UNMAP_CASH_LEDGER));
                                this.ShowMessageBox("Default Ledger(s) can't be unmapped");
                            }
                            else if (gvLedgerDetail.FocusedColumn.Name.Equals(grdChkCol.Name) && IsNotCCLedgerDistributed(Lid))
                            {
                                e.Cancel = true;
                                ShowMessageBox("As Ledger(s) are mapped with Cost Centre, Can't unmap Ledger");
                            }
                        }
                    }
                    else
                    {
                        //if (gvLedgerDetail.FocusedColumn.Name.Equals(grdChkCol.Name) && BankId.Equals(1))
                        //{
                        //    e.Cancel = true; //Disabling the editing of the cell
                        //    this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.UNMAP_CASH_LEDGER));
                        //}

                        //By Aldrin. To validate default ledgers with acces flag.
                        if (gvLedgerDetail.FocusedColumn.Name.Equals(grdChkCol.Name) && accessFlag != 0 && accessFlag == (int)AccessFlag.Readonly)
                        {
                            e.Cancel = true;
                            this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.UNMAP_CASH_LEDGER));
                        }
                        else if (this.AppSetting.CostCeterMapping == 1 && IsNotCCLedgerDistributed(Lid))
                        {
                            ShowMessageBox("As Ledger(s) are mapped with Cost Centre, Can't unmap Ledger");
                        }
                    }
                }
            }
        }

        private void gvLedgerDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                DataTable dtTransFDId = new DataTable();
                if (gvLedgerDetail.GetRowCellValue(e.RowHandle, colLedgerId) != null)
                {
                    if (gvLedgerDetail.GetRowCellValue(e.RowHandle, colLedgerId).ToString() != string.Empty)
                    {
                        UInt32 BankId = (UInt32)gvLedgerDetail.GetRowCellValue(e.RowHandle, colLedgerId);
                        //UInt32 BankId = (UInt32)gvLedgerDetail.GetRowCellValue(e.RowHandle, "BANK_ID");
                        if (dtTransactionFD != null && dtTransactionFD.Rows.Count > 0)
                        {
                            var TransFD = (from ledgers in dtTransactionFD.AsEnumerable()
                                           where ((ledgers.Field<UInt32>(colLedgerId.FieldName) == BankId))
                                           select ledgers);
                            if (TransFD.Count() > 0)
                                dtTransFDId = TransFD.CopyToDataTable();

                            if (dtTransFDId != null && dtTransFDId.Rows.Count > 0)
                            {
                                UInt32 Bank = Convert.ToUInt32(dtTransFDId.Rows[0][0]);
                                if (Bank.Equals(BankId))
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

        private void frmMapProjectLedger_Shown(object sender, EventArgs e)
        {
            gvProjectName.Select();
            gvProjectDetails.FocusedRowHandle = GridControl.NewItemRowHandle;
            gvProjectDetails.FocusedColumn = gvLedgerDetail.VisibleColumns[2];
            gvProjectDetails.ShowEditor();

            gcCostCentre.Select();
            gvCostCentre.FocusedRowHandle = GridControl.NewItemRowHandle;
            gvCostCentre.FocusedColumn = gvCostCentre.VisibleColumns[3];
            gvCostCentre.ShowEditor();

            gcProjectLedger.Select();
            gvLedgerDetail.FocusedRowHandle = GridControl.NewItemRowHandle;
            gvLedgerDetail.FocusedColumn = gvLedgerDetail.VisibleColumns[4];
            gvLedgerDetail.ShowEditor();

        }

        private void grdlProjectName_EditValueChanged(object sender, EventArgs e)
        {
            LoadLedgerDetails();
            FetchMappedLedgers();
            GetTransactionFD();
            FetchProjectLedgerApplicable();
            gvLedgerDetail.ExpandAllGroups();
            chkLedgerSelectAll.Checked = false;
        }

        private void gvProjectDetails_RowClick(object sender, RowClickEventArgs e)
        {
            DataTable dtProject = (DataTable)gvProjectName.DataSource;
            if (dtProject != null && dtProject.Rows.Count > 0)
            {
                if (grdlederName.EditValue != null)
                {
                    if (gvProjectDetails.GetFocusedRowCellValue(colSelect) != null)
                    {
                        if (UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString()) != 1)
                        {
                            int select = gvProjectDetails.GetFocusedRowCellValue(colSelect) != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(gvProjectDetails.GetFocusedRowCellValue(colSelect).ToString()) : 0;
                            gvProjectDetails.SetFocusedRowCellValue(colSelect, select == 0 ? 1 : 0);
                        }
                        else this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.UNMAP_CASH_LEDGER));
                    }
                }
            }
        }

        private void gvLedgerDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            UpdateBalanceLedger();
        }

        private void rtxtOpBalance_Leave(object sender, EventArgs e)
        {
            DataTable dtNegaiveBalance = gcProjectLedger.DataSource as DataTable;
            Credit = UtilityMember.NumberSet.ToDouble(dtNegaiveBalance.Compute(String.Format("MIN({0})", dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName), "").ToString());
            if (Credit >= 0)
            {
                UpdateBalanceLedger();
                // UpdateFDBalance();
            }
            else
                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_NEGATIVE_BALANCE));
        }

        private void gvLedgerDetail_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void gvAvailableLedgerDetails_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void btnMoveIn_Click(object sender, EventArgs e)
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            MoveInLedgerledgers();
            gvLedgerDetail.ExpandAllGroups();
            GetGridViewFocus(gvLedgerDetail, 4, true);
            chkLedgerSelectAll.Checked = false;
            lblCostCategoryMapRecordCount.Text = GetCountListBox(chkAvailableCategoryCostCenter);
            //}
            //else
            //{
            //    //this.ShowMessageBox("Ledger Mapping is locked by Head Office Admin." + Environment.NewLine + "Contact Head Office Admin for further assistance");
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.LEDGER_MAPPING_LOCKED_HEADOFFICE_ADMIN) + Environment.NewLine + this.GetMessage(MessageCatalog.Master.Mapping.CONTACT_HEADOFFICE_ADMIN_FURTHER_ASSISTANCE));
            //}
        }

        private void btnMoveOut_Click(object sender, EventArgs e)
        {
            //if (this.AppSetting.LockMasters == (int)YesNo.No)
            //{
            this.ShowWaitDialog("Checking and Unmapping Ledgers....");
            MoveOutLedger();
            gvLedgerDetail.ExpandAllGroups();
            chkLedgerSelectAll.Checked = false;
            lblCostCategoryMapRecordCount.Text = GetCountListBox(chkAvailableCategoryCostCenter);
            this.CloseWaitDialog();
            //}
            //else
            //{
            //    //this.ShowMessageBox("Ledger UnMapping is locked by Head Office Admin." + Environment.NewLine + "Contact Head Office Admin for further assistance");
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.LEDGER_UNMAPPING_HEADOFFICE_ADMIN) + Environment.NewLine + this.GetMessage(MessageCatalog.Master.Mapping.CONTACT_HEADOFFICE_ADMIN_FURTHER_ASSISTANCE));
            //}
        }

        private void chkAvailableFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAvailableLedgerDetails.OptionsView.ShowAutoFilterRow = chkAvailableFilter.Checked;
            if (chkAvailableFilter.Checked)
            {
                gvAvailableLedgerDetails.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvAvailableLedgerDetails.FocusedColumn = gvColLedgerName;
                gvAvailableLedgerDetails.ShowEditor();
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvLedgerDetail.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvLedgerDetail, colLedger);
                //gvLedgerDetail.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                //gvLedgerDetail.FocusedColumn = colLedger;
                //gvLedgerDetail.ShowEditor();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtAllLedger = (DataTable)gvAvailableLedger.DataSource;
            if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAllLedger.Rows)
                {
                    dr[SELECT_COL] = chkLedgerSelectAll.Checked;
                }
                gvAvailableLedger.DataSource = dtAllLedger;
            }
        }

        private void chkUnMapSelectAllLedgers_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnMapLedger = SelectAll(((DataTable)gcProjectLedger.DataSource), chkUnMapSelectAllLedgers);
            if (dtUnMapLedger != null)
            {
                gcProjectLedger.DataSource = dtUnMapLedger;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtNegaiveBalance = gcProjectLedger.DataSource as DataTable;
            if (dtNegaiveBalance != null)
            {
                DataView dvLegalEntityFilter = new DataView(dtNegaiveBalance);
                dvLegalEntityFilter.RowFilter = "LEGAL_ENTITY_LEDGER_ID<>0";
                DataTable dtLegalEntityFiltered = dvLegalEntityFilter.ToTable();
                if (dtLegalEntityFiltered != null)
                {
                    int LegalEntityId = dtLegalEntityFiltered.Rows.Count > 0 ? UtilityMember.NumberSet.ToInteger(dtLegalEntityFiltered.Rows[0]["CUSTOMER_ID"].ToString()) : 0;
                    DataView dvLeagaEntityFilter1 = new DataView(dtLegalEntityFiltered);
                    dvLeagaEntityFilter1.RowFilter = String.Format("CUSTOMER_ID={0}", LegalEntityId);
                    DataTable dtFilteredLegalEntity = dvLeagaEntityFilter1.ToTable();
                    if (dtFilteredLegalEntity.Rows.Count == dtLegalEntityFiltered.Rows.Count)
                    {
                        string ProjectDivision = grdlProjectName.EditValue != null ? grdlProjectName.Text : string.Empty;
                        ProjectDivision = ProjectDivision.Substring(ProjectDivision.IndexOf('-'));
                        int DivisionId = ProjectDivision.Contains("Local") ? 1 : 2;
                        if (DivisionId == 1)
                        {
                            DataView dvDivisionFilter = new DataView(dtFilteredLegalEntity);
                            dvDivisionFilter.RowFilter = "LEGAL_ENTITY_LEDGER_ID<>0";
                            DataTable dtDivsionFiltered = dvDivisionFilter.ToTable();
                            if (dtDivsionFiltered.Rows.Count == 0)
                            {
                                double NagativeBalance = UtilityMember.NumberSet.ToDouble(dtNegaiveBalance.Compute(String.Format("MIN({0})", dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName), string.Empty).ToString());
                                if (NagativeBalance >= 0)
                                    LedgerProjectMapping();
                                else
                                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_NEGATIVE_BALANCE));
                            }
                            else
                            {
                                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.LOCAL_PROJECT_RESTRICTED_WITH_BANK_ACCOUNT));
                            }
                        }
                        else
                        {
                            double NagativeBalance = UtilityMember.NumberSet.ToDouble(dtNegaiveBalance.Compute(String.Format("MIN({0})", dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName), string.Empty).ToString());
                            if (NagativeBalance >= 0)
                                LedgerProjectMapping();
                            else
                                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_NEGATIVE_BALANCE));
                        }
                    }
                    else
                    {
                        ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.BANKACCOUNT_NOT_IN_SAME_LEGALENTITY));
                    }
                }

                if (gcProjectLedger.DataSource != null && gvLedgerDetail.RowCount > 0)
                {
                    gvLedgerDetail.FocusedColumn = colOPBalance;
                    gvLedgerDetail.ShowEditor();
                }
            }
            else
                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResultLedger;
            this.Close();
        }

        #endregion

        #region Methods

        private void LoadLedgerDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    //if (grdlProjectName.EditValue != null)
                    //{
                    //  mappingSystem.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                    resultArgs = mappingSystem.FetchLedgerDetails();
                    DataTable dt = resultArgs.DataSource.Table;
                    dt = AddColumns(dt);
                    if (dt != null)
                    {
                        if (!chkShowAllLedger.Checked)
                        {
                            resultArgs = FetchCategoryLedgers();
                            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0
                                && !string.IsNullOrEmpty(resultArgs.DataSource.Table.Rows[0]["LEDGER_ID"].ToString()))
                            {
                                DataView dvFilter = dt.AsDataView();
                                string LEdIDs = resultArgs.DataSource.Table.Rows[0]["LEDGER_ID"].ToString();
                                if (!string.IsNullOrEmpty(LEdIDs))
                                {
                                    dvFilter.RowFilter = "LEDGER_ID IN (" + LEdIDs + ")";
                                    if (dvFilter != null && dvFilter.ToTable().Rows.Count > 0)
                                    {
                                        gvAvailableLedger.DataSource = dvFilter.ToTable();
                                    }
                                }
                            }
                            else
                            {
                                gvAvailableLedger.DataSource = dt;
                            }
                        }
                        else
                        {
                            gvAvailableLedger.DataSource = dt;
                        }
                    }

                    gvColType.UnGroup();
                    gvColType.Visible = false;
                    // }
                }

                ShowAvailableLedgerCount();

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// Load Ledgers on 03.01.2020
        /// </summary>
        private void LoadBudgetLedgerDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    if (grdlProjectName.EditValue != null)
                    {
                        mappingSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpBudgetProject.EditValue.ToString());
                        if (this.AppSetting.IS_ABEBEN_DIOCESE || this.AppSetting.IS_DIOMYS_DIOCESE)
                        {
                            resultArgs = mappingSystem.FetchExpenseMappedLedgerBudget();
                        }
                        else
                        {
                            resultArgs = mappingSystem.FetchMappedLedgers();
                        }
                        DataTable dtMapped = resultArgs.DataSource.Table;
                        dtMapped = AddColumns(dtMapped);
                        if (dtMapped != null)
                        {
                            //On 24/09/2024, To skip Cash, Bank and FD Ledger for Budget ----------------------------------
                            string ledgercashbankfds = mappingSystem.AppSchema.Ledger.GROUP_IDColumn.ColumnName + " NOT IN (" + (Int32)FixedLedgerGroup.Cash + "," +
                                        (Int32)FixedLedgerGroup.BankAccounts + "," + (Int32)FixedLedgerGroup.FixedDeposit + ")";

                            dtMapped.DefaultView.RowFilter = ledgercashbankfds;
                            dtMapped = dtMapped.DefaultView.ToTable();

                            //---------------------------------------------------------------------------------------------

                            gcABudLedger.DataSource = dtMapped;
                        }

                        gvcolBudgetType.UnGroup();
                        gvcolBudgetType.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        public ResultArgs FetchCategoryLedgers()
        {
            using (MappingSystem mapsystem = new MappingSystem())
            {
                mapsystem.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                resultArgs = mapsystem.FetchCategoryLedger();
            }
            return resultArgs;
        }


        //private void UpdateFDBalance()
        //{
        //    if (gvLedgerDetail.DataSource != null)
        //    {
        //        if (gvLedgerDetail.FocusedRowHandle > 0)
        //        {
        //            string FD = gvLedgerDetail.GetFocusedRowCellValue(colGroup).ToString();
        //            int LedgerId = UtilityMember.NumberSet.ToInteger(gvLedgerDetail.GetFocusedRowCellValue(colLedgerId).ToString());
        //            double Amount = UtilityMember.NumberSet.ToDouble(gvLedgerDetail.GetFocusedRowCellValue(colOPBalance).ToString());
        //            using (LedgerSystem FindFD = new LedgerSystem())
        //            {
        //                FindFD.LedgerId = LedgerId;
        //                int LedgerGroup = FindFD.FetchLedgerGroupById();
        //                if (LedgerGroup.Equals((int)FixedLedgerGroup.FixedDeposit))
        //                {
        //                    if (Amount > 0.00)
        //                    {
        //                        DataTable dtFDDetails = gvLedger.DataSource as DataTable;
        //                        var Remove = (from d in dtFDDetails.AsEnumerable()
        //                                      where ((d.Field<UInt32>(dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName) == LedgerId))
        //                                      select d);
        //                        if (Remove.Count() > 0)
        //                            dtFDDetails = Remove.CopyToDataTable();
        //                        if (dtFDDetails != null && dtFDDetails.Rows.Count > 0)
        //                        {
        //                            if (grdlProjectName.EditValue != null)
        //                                this.ProjectName = grdlProjectName.Text;
        //                            using (frmBreakUp FDAdd = new frmBreakUp(Amount, UtilityMember.NumberSet.ToInteger(dtFDDetails.Rows[0]["BANK_ACCOUNT_ID"].ToString()), FDUpdation, this.ProjectName))
        //                            {
        //                                if (dtFDBreakUp != null) { FDAdd.dtFixedDeposite = dtFDBreakUp; }
        //                                FDAdd.ShowDialog();
        //                                dtFDBreakUp = FDAdd.dtFixedDeposite;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void UpdateBalanceLedger()
        {
            try
            {
                dtBalanceUpdate = gcProjectLedger.DataSource as DataTable;
                if (dtBalanceUpdate != null && dtBalanceUpdate.Rows.Count > 0)
                {
                    Credit = Debit = 0.00;
                    dtBalanceUpdate.AcceptChanges();
                    if (this.AppSetting.AllowMultiCurrency == 1)
                    {
                        decimal credit = dtBalanceUpdate.AsEnumerable().Where(r => r.Field<string>("TRANS_MODE") == "CR" &&
                        r.Field<string>("GROUP") == "General").Sum(r => r.Field<decimal>("AMOUNT") * r.Field<decimal>("EXCHANGE_RATE"));
                        decimal debit = dtBalanceUpdate.AsEnumerable().Where(r => r.Field<string>("TRANS_MODE") == "DR" &&
                            r.Field<string>("GROUP") == "General").Sum(r => r.Field<decimal>("AMOUNT") * r.Field<decimal>("EXCHANGE_RATE"));
                        Credit = UtilityMember.NumberSet.ToDouble(credit.ToString());
                        Debit = UtilityMember.NumberSet.ToDouble(debit.ToString());
                        //Credit = UtilityMember.NumberSet.ToDouble(dtBalanceUpdate.Compute("SUM(AMOUNT * EXCHANGE_RATE)", "TRANS_MODE='CR' AND GROUP='General'").ToString());
                        //Debit = UtilityMember.NumberSet.ToDouble(dtBalanceUpdate.Compute("SUM(AMOUNT * EXCHANGE_RATE)", "TRANS_MODE='DR' AND GROUP='General'").ToString());
                    }
                    else
                    {
                        Credit = UtilityMember.NumberSet.ToDouble(dtBalanceUpdate.Compute("SUM(AMOUNT)", "TRANS_MODE='CR' AND GROUP='General'").ToString());
                        Debit = UtilityMember.NumberSet.ToDouble(dtBalanceUpdate.Compute("SUM(AMOUNT)", "TRANS_MODE='DR' AND GROUP='General'").ToString());
                    }

                    if (Credit > Debit)
                    {
                        lblDifferencesLedger.Text = UtilityMember.NumberSet.ToCurrency(Credit - Debit);
                        lblDifferenceFlag.Text = " Cr";
                    }
                    else if (Credit.Equals(Debit))
                    {
                        lblDifferencesLedger.Text = NO_DIFFERENCE;
                        lblDifferenceFlag.Text = string.Empty;
                    }
                    else
                    {
                        lblDifferencesLedger.Text = UtilityMember.NumberSet.ToCurrency(Debit - Credit);
                        lblDifferenceFlag.Text = " Dr";
                    }
                    lblCRLedger.Text = UtilityMember.NumberSet.ToCurrency(Credit);
                    lblDRLedger.Text = UtilityMember.NumberSet.ToCurrency(Debit);
                }
                else
                {
                    lblCRLedger.Text = UtilityMember.NumberSet.ToCurrency(0);
                    lblDRLedger.Text = UtilityMember.NumberSet.ToCurrency(0);
                    lblDifferencesLedger.Text = NO_DIFFERENCE;
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
            }
             
        }

        private void FetchMappedLedgers()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    if (grdlProjectName != null)
                    {
                        mappingSystem.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                        resultArgs = mappingSystem.FetchMappedLedgers();
                        if (resultArgs.DataSource.Table != null)
                        {
                            gcProjectLedger.DataSource = AddColumns(resultArgs.DataSource.Table);
                            DataTable dtAllLedgers = gvAvailableLedger.DataSource as DataTable;

                            gvLedgerDetail.FocusedColumn = colOPLedgerBal;
                            gvLedgerDetail.ShowEditor();

                            if (dtAllLedgers != null)
                            {
                                foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                                {
                                    var MappedLedgers = (from ledger in dtAllLedgers.AsEnumerable()
                                                         where (ledger.Field<UInt32>(dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()))
                                                         select ledger);
                                    if (MappedLedgers.Count() > 0)
                                        dtAllLedgers = MappedLedgers.CopyToDataTable();
                                    else
                                        dtAllLedgers = dtAllLedgers.Clone();
                                }
                                gvAvailableLedger.DataSource = dtAllLedgers;
                            }
                            UpdateBalanceLedger();
                        }
                    }

                    //To get ledger cc distributed amount
                    FetchLedgerCCDistributionAmount(mappingSystem.ProjectId);


                }

                ShowSelectedLedgerCount();
                ShowAvailableLedgerCount();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        /// On 17/11/2022, To get distributed ledger opening amount with CC
        /// </summary>
        private void FetchLedgerCCDistributionAmount(Int32 Pid)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                dtAllProjectLedgerCCDistributed = null;
                if (AppSetting.CostCeterMapping == 1)
                {
                    resultArgs = mappingSystem.FetchProjectLedgerCostCentreDistribution(Pid);
                    if (resultArgs != null)
                    {
                        dtAllProjectLedgerCCDistributed = resultArgs.DataSource.Table;
                    }
                }
            }
        }

        /// <summary>
        /// On 21/09/2023, To get Project Ledger's Applicable 
        /// </summary>
        private void FetchProjectLedgerApplicable()
        {
            colProjectLedgerApplicableDate.Visible = false;
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                dtAllProjectLedgerApplicable = null;
                int Pid = grdlProjectName.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                resultArgs = mappingSystem.FetchProjectLedgerApplicableByProjectId(Pid);
                if (resultArgs != null && resultArgs.Success)
                {
                    dtAllProjectLedgerApplicable = resultArgs.DataSource.Table;
                    dtAllProjectLedgerApplicable.DefaultView.Sort = mappingSystem.AppSchema.Ledger.APPLICABLE_FROMColumn.ColumnName;
                    dtAllProjectLedgerApplicable = dtAllProjectLedgerApplicable.DefaultView.ToTable();
                }
            }
        }

        /// <summary>
        ///  fetch mapped Ledger on 03.01.2020
        /// </summary>
        private void FetchMappedLedgersBudget()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    if (glkpBudgetProject != null)
                    {
                        mappingSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpBudgetProject.EditValue.ToString());
                        resultArgs = mappingSystem.FetchMappedBudgetLedgers();
                        if (resultArgs.DataSource.Table != null)
                        {
                            gcSBudLedger.DataSource = AddColumns(resultArgs.DataSource.Table);
                            DataTable dtAllLedgers = gcABudLedger.DataSource as DataTable;
                            if (dtAllLedgers != null)
                            {
                                foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                                {
                                    var MappedLedgers = (from ledger in dtAllLedgers.AsEnumerable()
                                                         where (ledger.Field<UInt32>(dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()))
                                                         select ledger);
                                    if (MappedLedgers.Count() > 0)
                                        dtAllLedgers = MappedLedgers.CopyToDataTable();
                                    else
                                        dtAllLedgers = dtAllLedgers.Clone();
                                }
                                gcABudLedger.DataSource = dtAllLedgers;
                            }
                            UpdateBalanceLedger();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void LedgerProjectMapping()
        {
            DataTable dtLedgerIDCollection = gcProjectLedger.DataSource as DataTable;
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                if (grdlProjectName.EditValue != null)
                {
                    if (gvAvailableLedger.DataSource != null && dtLedgerIDCollection != null)
                    {
                        if ((gvAvailableLedger.DataSource as DataTable).Rows.Count > 0 || dtLedgerIDCollection.Rows.Count > 0)
                        {
                            if (ValidateLedgerCCDistributionAmount(dtLedgerIDCollection))
                            {
                                mappingSystem.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                                mappingSystem.dtLedgerIDCollection = dtLedgerIDCollection;
                                mappingSystem.dtMovedLedgerIDCollection = dtCheckedItems;
                                mappingSystem.OpeningBalanceDate = this.AppSetting.BookBeginFrom;
                                if (!string.IsNullOrEmpty(this.AppSetting.BookBeginFrom))
                                {
                                    this.ShowWaitDialog();
                                    mappingSystem.dtMappingLedger = dtFDBreakUp;
                                    mappingSystem.dtLedgerCCDistribution = dtAllProjectLedgerCCDistributed;
                                    mappingSystem.dtProjectLedgerApplicableDetails = dtAllProjectLedgerApplicable;
                                    resultArgs = mappingSystem.MappingLedgers();
                                    if (resultArgs != null)
                                    {
                                        if (resultArgs.Success)
                                        {
                                            mappingDialogResultLedger = DialogResult.OK;
                                            ShowSuccessMessage(GetMessage(MessageCatalog.Master.Mapping.LEDGER_MAPPING_SUCCESS));
                                            dtCheckedItems = null;
                                            FetchProjectLedgerApplicable();
                                        }
                                    }
                                    this.CloseWaitDialog();
                                }
                                else
                                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.BOOK_BEGINNING_DATE_EMPTY));
                            }

                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
            }
        }

        /// <summary>
        /// Budget Ledger Mapping 04.01.2020
        /// </summary>
        private void BudgetLedgerProjectMapping()
        {
            // gvLedger, gvLedgerdetails - gcsbledger,gvsbledger -selected 
            // gvavailableLedger, gvAvailableLedgerdetails- gcabudgetledger,gvabudgetledger - available

            DataTable dtLedgerIDCollection = gcSBudLedger.DataSource as DataTable;
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                if (glkpBudgetProject.EditValue != null)
                {
                    if (gcABudLedger.DataSource != null && dtLedgerIDCollection != null)
                    {
                        if ((gcABudLedger.DataSource as DataTable).Rows.Count > 0 || dtLedgerIDCollection.Rows.Count > 0)
                        {
                            mappingSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpBudgetProject.EditValue.ToString());
                            mappingSystem.dtLedgerIDCollection = dtLedgerIDCollection;
                            mappingSystem.dtMovedLedgerIDCollection = dtCheckedItems;
                            mappingSystem.OpeningBalanceDate = this.AppSetting.BookBeginFrom;
                            if (!string.IsNullOrEmpty(this.AppSetting.BookBeginFrom))
                            {
                                this.ShowWaitDialog();
                                mappingSystem.dtMappingLedger = dtFDBreakUp;
                                resultArgs = mappingSystem.MappingBudgetLedgers();
                                if (resultArgs != null)
                                {
                                    if (resultArgs.Success)
                                    {
                                        mappingDialogResultLedger = DialogResult.OK;
                                        ShowSuccessMessage(GetMessage(MessageCatalog.Master.Mapping.LEDGER_MAPPING_SUCCESS));
                                        dtCheckedItems = null;
                                    }
                                }
                                this.CloseWaitDialog();
                            }
                            else
                                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.BOOK_BEGINNING_DATE_EMPTY));
                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
            }
        }


        /// <summary>
        /// On 03/02/2021, To Show selected Ledgers count
        /// </summary>
        private void ShowSelectedLedgerCount()
        {
            lgrpSelectedLedgers.Text = "Selected Ledger(s)";
            lblSelecteLedgersCount.Text = "0";
            if (gvLedgerDetail.RowCount > 0)
            {
                lgrpSelectedLedgers.Text = gvLedgerDetail.RowCount.ToString() + " Selected Ledger(s)";
                lblSelecteLedgersCount.Text = "# " + gvLedgerDetail.RowCount.ToString();
            }
        }

        /// <summary>
        /// On 03/02/2021, To Show Available Ledgers count
        /// </summary>
        private void ShowAvailableLedgerCount()
        {
            lgrpAvailableLedgers.Text = "Available Ledger(s)";
            if (gvAvailableLedgerDetails.RowCount > 0)
            {
                lgrpAvailableLedgers.Text = gvAvailableLedgerDetails.RowCount.ToString() + " Selected Ledger(s)";
            }
        }


        #endregion
        #endregion

        #region Map Ledger

        #region Events
        private void chkLedgerSelect_CheckedChanged(object sender, EventArgs e)
        {

            this.chkLedgerSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
            chkLedgerSelectAll.Checked = false;
            this.chkLedgerSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);


        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.chkUnMapSelectAllLedgers.CheckedChanged -= new System.EventHandler(this.chkUnMapSelectAllLedgers_CheckedChanged);
            chkUnMapSelectAllLedgers.Checked = false;
            this.chkUnMapSelectAllLedgers.CheckedChanged += new System.EventHandler(this.chkUnMapSelectAllLedgers_CheckedChanged);
        }

        private void gvProjectDetails_RowStyle(object sender, RowStyleEventArgs e)
        {
            using (LedgerSystem FindFD = new LedgerSystem())
            {
                if (grdlederName.EditValue != null)
                {
                    FindFD.LedgerId = UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString());
                    int LedgerGroup = FindFD.FetchLedgerGroupById();
                    if (LedgerGroup.Equals((int)FixedLedgerGroup.FixedDeposit))
                    {
                        DataTable dtTransFDId = new DataTable();
                        if (dtTransactionFD != null && dtTransactionFD.Rows.Count > 0)
                        {
                            using (MappingSystem FDTransaction = new MappingSystem())
                            {
                                e.Appearance.BackColor = Color.LightGray;
                                //resultArgs = FDTransaction.FetchBankId(FindFD.LedgerId);
                                //if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                //{
                                //    UInt32 BankId = (UInt32)resultArgs.DataSource.Table.Rows[0][dataManager.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn.ColumnName];
                                //    var TransFD = (from ledgers in dtTransactionFD.AsEnumerable()
                                //                   where ((ledgers.Field<UInt32>(gvColBankAccountId.FieldName) == BankId))
                                //                   select ledgers);
                                //    if (TransFD.Count() > 0)
                                //        dtTransFDId = TransFD.CopyToDataTable();

                                //    if (dtTransFDId != null && dtTransFDId.Rows.Count > 0)
                                //    {
                                //        UInt32 Bank = Convert.ToUInt32(dtTransFDId.Rows[0][0]);
                                //        if (Bank.Equals(BankId))
                                //        {
                                //            e.Appearance.BackColor = Color.LightGray;
                                //        }
                                //        else
                                //        {
                                //            if (gvProjectDetails.FocusedColumn.Name.Equals(colSelect.Name) && UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString()).Equals(1))
                                //                e.Appearance.BackColor = Color.LightGray;
                                //        }
                                //    }
                                //}
                            }
                        }
                    }
                    else
                    {
                        if (UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString()).Equals(1))
                            e.Appearance.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void gvProjectDetails_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (LedgerSystem FindFD = new LedgerSystem())
            {
                FindFD.LedgerId = UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString());
                int LedgerGroup = FindFD.FetchLedgerGroupById();
                if (LedgerGroup.Equals((int)FixedLedgerGroup.FixedDeposit))
                {
                    DataTable dtTransFDId = new DataTable();
                    if (dtTransactionFD != null && dtTransactionFD.Rows.Count > 0)
                    {
                        if (gvProjectDetails.FocusedColumn.Name.Equals(colOPLedgerBal.Name))
                        {
                            e.Cancel = true; //Disabling the editing of the cell
                            //this.ShowMessageBox("Cannot set opening balance for this Fixed Deposit ledger");
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.MAPPING_LEDGER_CANNOT_SET_OP_FD_LEDGER));
                        }
                        //using (MappingSystem FDTransaction = new MappingSystem())
                        //{
                        //resultArgs = FDTransaction.FetchBankId(FindFD.LedgerId);
                        //if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        //{
                        //    UInt32 BankId = (UInt32)resultArgs.DataSource.Table.Rows[0][dataManager.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn.ColumnName];
                        //    var TransFD = (from ledgers in dtTransactionFD.AsEnumerable()
                        //                   where ((ledgers.Field<UInt32>(gvColBankAccountId.FieldName) == BankId))
                        //                   select ledgers);
                        //    if (TransFD.Count() > 0)
                        //        dtTransFDId = TransFD.CopyToDataTable();
                        //    if (dtTransFDId != null && dtTransFDId.Rows.Count > 0)
                        //    {
                        //        UInt32 Bank = Convert.ToUInt32(dtTransFDId.Rows[0][0]);
                        //        if (Bank.Equals(BankId))
                        //        {
                        //            e.Cancel = true; //Disabling the editing of the cell
                        //            this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.TRANSACTION_FD));
                        //        }
                        //    }
                        //}
                        //  }
                    }
                }
                else
                {
                    if (gvProjectDetails.FocusedColumn.Name.Equals(colSelect.Name) && UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString()).Equals(1))
                    {
                        e.Cancel = true; //Disabling the editing of the cell
                        this.ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.UNMAP_CASH_LEDGER));
                    }
                }
            }
        }

        private void grdlederName_EditValueChanged(object sender, EventArgs e)
        {
            BindProjectCheckedListBox();
            FetchMappedProject();
        }

        private void gvLedgerDetail_RowClick(object sender, RowClickEventArgs e)
        {

        }

        private void gvAvailableLedgerDetails_RowClick(object sender, RowClickEventArgs e)
        {
            DataTable dtLedger = (DataTable)gvAvailableLedger.DataSource;
            if (dtLedger != null && dtLedger.Rows.Count > 0)
            {
                if (gvAvailableLedgerDetails.GetFocusedRowCellValue(grdChkCol) != null)
                {
                    int select = gvAvailableLedgerDetails.GetFocusedRowCellValue(grdChkCol) != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(gvAvailableLedgerDetails.GetFocusedRowCellValue(grdChkCol).ToString()) : 0;
                    gvAvailableLedgerDetails.SetFocusedRowCellValue(grdChkCol, select == 0 ? 1 : 0);
                }
            }
        }

        private void rtxtOPLedgerBal_Leave(object sender, EventArgs e)
        {
            using (LedgerSystem FindFD = new LedgerSystem())
            {
                if (grdlederName.Visible)
                {
                    if (gvProjectDetails.FocusedRowHandle >= 0)
                    {
                        double Amount = UtilityMember.NumberSet.ToDouble(gvProjectDetails.GetFocusedRowCellValue(colOPLedgerBal).ToString());
                        FindFD.LedgerId = UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString());
                        int LedgerGroup = FindFD.FetchLedgerGroupById();
                        if (LedgerGroup.Equals((int)FixedLedgerGroup.FixedDeposit))
                        {
                            if (Amount > 0)
                            {
                                using (MappingSystem FDTransaction = new MappingSystem())
                                {
                                    resultArgs = FDTransaction.FetchBankId(FindFD.LedgerId);
                                    if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                    {
                                        int Bank_Id = UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][dataManager.AppSchema.BankAccount.BANK_ACCOUNT_IDColumn.ColumnName].ToString());
                                        using (frmBreakUp FDAdd = new frmBreakUp(Amount, Bank_Id, FDUpdation, grdlProjectName.Text))
                                        {
                                            IsFDLedger = true;
                                            FDAdd.ShowDialog();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void gvProjectDetails_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void btnMapProjectMoveIn_Click(object sender, EventArgs e)
        {
            MoveInItems(gvProjectName, chkMapProject, dataManager.AppSchema.Project.PROJECT_IDColumn.ColumnName, dataManager.AppSchema.Project.PROJECTColumn.ColumnName);
        }

        private void btnMapProjectMoveOut_Click(object sender, EventArgs e)
        {
            // MoveOutItems(gvProjectName, chkMapProject, dataManager.AppSchema.Project.PROJECT_IDColumn.ColumnName, dataManager.AppSchema.Project.PROJECTColumn.ColumnName);
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            ProjectLedgerMapping();
        }

        private void chkShowFilterLedger_CheckedChanged(object sender, EventArgs e)
        {
            gvProjectDetails.OptionsView.ShowAutoFilterRow = chkShowFilterLedger.Checked;
            if (chkShowFilterLedger.Checked)
            {
                this.SetFocusRowFilter(gvProjectDetails, colProjectName);
                //gvProjectDetails.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                //gvProjectDetails.FocusedColumn = colProjectName;
                //gvProjectDetails.ShowEditor();
            }

        }

        private void btnCloseProject_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResultProject;
            this.Close();
        }

        #endregion

        #region Methods
        private void BindProjectCheckedListBox()
        {
            using (MappingSystem project = new MappingSystem())
            {
                resultArgs = LoadProjects(project);
                if (resultArgs.DataSource.Table != null)
                {
                    int i = 0;
                    if (grdlederName.EditValue != null)
                    {
                        DataView dv = new DataView(dtLedgerDetails) { RowFilter = "LEDGER_ID=" + UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString()) };
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            resultArgs.DataSource.Table.Rows[i++][dataManager.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName] = dv.ToTable().Rows[0][dataManager.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName];
                        }
                        CheckedListBoxBindDataSource(resultArgs.DataSource.Table, chkMapProject, dataManager.AppSchema.Project.PROJECT_IDColumn.ColumnName, dataManager.AppSchema.Project.PROJECTColumn.ColumnName);
                    }
                }
            }
        }

        private ResultArgs LoadProjects(MappingSystem mappingSystem)
        {
            return resultArgs = mappingSystem.FetchProjectsLookup();
        }

        private void ProjectLedgerMapping()
        {
            DataTable dtProjectIDCollection = gvProjectName.DataSource as DataTable;
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.dtProjectIDCollection = dtProjectIDCollection;
                if (grdlederName.EditValue != null)
                {
                    if (chkMapProject.DataSource != null && mappingSystem.dtProjectIDCollection != null)
                    {
                        if ((chkMapProject.DataSource as DataTable).Rows.Count > 0 || mappingSystem.dtProjectIDCollection.Rows.Count > 0)
                        {
                            mappingSystem.LedgerId = UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString());
                            mappingSystem.OpeningBalanceDate = this.AppSetting.BookBeginFrom;
                            if (!string.IsNullOrEmpty(this.AppSetting.BookBeginFrom))
                            {
                                this.ShowWaitDialog();
                                resultArgs = mappingSystem.AccountMappingProject(dtBreakUpdetails, dtFDDetailsForBreakup, FDUpdation, IsFDLedger);
                                if (resultArgs.Success)
                                {
                                    mappingDialogResultProject = DialogResult.OK;
                                    ShowSuccessMessage(GetMessage(MessageCatalog.Master.Mapping.PROJECT_MAPPING_SUCESS));
                                }
                                this.CloseWaitDialog();
                            }
                            else
                                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.BOOK_BEGINNING_DATE_EMPTY));
                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
                else
                    ShowMessageBox(GetMessage(MessageCatalog.Master.Ledger.LEDGER_NAME_EMPTY));
            }
        }

        private void FetchMappedProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    if (grdlederName.EditValue != null)
                    {
                        mappingSystem.LedgerId = UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString());
                        resultArgs = mappingSystem.FetchMappedProjects();
                        DataTable dtProject = resultArgs.DataSource.Table;
                        if (dtProject != null)
                        {
                            gvProjectName.DataSource = AddColumns(resultArgs.DataSource.Table);
                            DataTable dtAllProjects = chkMapProject.DataSource as DataTable;
                            if (dtAllProjects != null)
                            {
                                foreach (DataRow dr in dtProject.Rows)
                                {
                                    var MappedLedgers = (from ledger in dtAllProjects.AsEnumerable()
                                                         where (ledger.Field<UInt32>(dataManager.AppSchema.Project.PROJECT_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()))
                                                         select ledger);
                                    if (MappedLedgers.Count() > 0)
                                        dtAllProjects = MappedLedgers.CopyToDataTable();
                                    else
                                        dtAllProjects = dtAllProjects.Clone();
                                }
                                CheckedListBoxBindDataSource(dtAllProjects, chkMapProject, dataManager.AppSchema.Project.PROJECT_IDColumn.ColumnName, dataManager.AppSchema.Project.PROJECTColumn.ColumnName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }
        #endregion
        #endregion

        #region Map Cost Center

        #region Events


        private void gvCostCentre_RowClick(object sender, RowClickEventArgs e)
        {
            DataTable dtCostCentre = (DataTable)gcCostCentre.DataSource;
            if (dtCostCentre != null && dtCostCentre.Rows.Count > 0)
            {
                if (gvCostCentre.FocusedColumn == colCostCentreSelect)
                {
                    if (gvCostCentre.GetFocusedRowCellValue(colCostCentreSelect) != null)
                    {
                        int select = gvCostCentre.GetFocusedRowCellValue(colCostCentreSelect) != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(gvCostCentre.GetFocusedRowCellValue(colCostCentreSelect).ToString()) : 0;
                        gvCostCentre.SetFocusedRowCellValue(colCostCentreSelect, select == 0 ? 1 : 0);

                    }
                }

            }
        }

        private void gvCostCentre_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }


        }

        private void btnCostCenterMoveIn_Click(object sender, EventArgs e)
        {
            MoveInCostCentreGridItems();
            gvCostCentre.ExpandAllGroups();
            gvAvailableCostCentre.ExpandAllGroups();
        }

        private void btnCostCenterMoveOut_Click(object sender, EventArgs e)
        {
            MoveOutCostCentreGridItems();
            gvCostCentre.ExpandAllGroups();
            gvAvailableCostCentre.ExpandAllGroups();
        }

        private void chkCostCentreFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCostCentre.OptionsView.ShowAutoFilterRow = chkCostCentreFilter.Checked;
            if (chkCostCentreFilter.Checked)
            {
                this.SetFocusRowFilter(gvCostCentre, colCostCentreName);
                //gvCostCentre.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                //gvCostCentre.FocusedColumn = colCostCentreName;
                //gvCostCentre.ShowEditor();
            }

        }

        private void chkUnMapCostCenter_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnMapCostCenter = SelectAll(((DataTable)gcCostCentre.DataSource), chkUnMapCostCenter);
            if (dtUnMapCostCenter != null)
            {
                gcCostCentre.DataSource = dtUnMapCostCenter;
            }
        }

        //private void chkCostCentreSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkCostCentreSelectAll.Checked)
        //        {
        //            SetCheckBoxSelection(chkAvailableCostCenter, "UnSelect All", true);
        //        }
        //        else
        //        {
        //            SetCheckBoxSelection(chkAvailableCostCenter, "Select All", false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
        //    }
        //    finally { }
        //}

        private void btnCostCenterSave_Click(object sender, EventArgs e)
        {
            if (gcCostCentre.DataSource != null)
            {
                DataTable dt = gcCostCentre.DataSource as DataTable;
                //if (dt.Rows.Count > 0)
                if ((gcAvailableCostCentre.DataSource as DataTable).Rows.Count > 0 || (gcCostCentre.DataSource as DataTable).Rows.Count > 0)
                {
                    var CostcenterType = (from ledgers in (gcCostCentre.DataSource as DataTable).AsEnumerable()
                                          where ((ledgers.Field<String>(dataManager.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName).Equals(string.Empty)))
                                          select ledgers);
                    if (CostcenterType.Count() <= 0)
                        ProjectCostCentreMapping();
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.COST_CENTER_TYPE_EMPTY));
                }
                else
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
            }
            else
            {
                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
            }
        }

        /// <summary>
        /// To Save Purpose Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurposeSave_Click(object sender, EventArgs e)
        {
            if (gcpurpose.DataSource != null)
            {
                var PurposeType = (from ledgers in (gcpurpose.DataSource as DataTable).AsEnumerable()
                                   where ((ledgers.Field<String>(dataManager.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName).Equals(string.Empty)))
                                   select ledgers);
                if (PurposeType.Count() <= 0)
                    ProjectPurposeMapping();
                else
                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.SELECT_THE_PURPOSE_TYPE));

                if (gvPurpose.DataSource != null && gvPurpose.RowCount > 0)
                {
                    gvPurpose.FocusedColumn = colPurposeOpeningBalance;
                    gvPurpose.ShowEditor();
                }
            }
            else
                ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.NO_RECORDS_TO_SAVE));
        }

        private void btnPurposeClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = MappingDialogResultPurpose;
            this.Close();
        }

        private void btnCostCenterClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResultCostCentre;
            this.Close();
        }

        private void gcCostCentre_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Tab)
                {
                    if (gvCostCentre.IsLastRow)
                    {
                        if (gvCostCentre.FocusedColumn == colCostCentreOpBalance)
                        {
                            btnCostCenterMoveOut.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
        }

        private void gvAvailableCostCentre_RowClick(object sender, RowClickEventArgs e)
        {
            //DataTable dtCostCnetre = (DataTable)gcAvailableCostCentre.DataSource;
            //if (dtCostCnetre != null && dtCostCnetre.Rows.Count > 0)
            //{
            //    if (gvAvailableCostCentre.GetFocusedRowCellValue(gcolcsSelect) != null)
            //    {
            //        int select = gvAvailableCostCentre.GetFocusedRowCellValue(gcolcsSelect) != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(gvAvailableCostCentre.GetFocusedRowCellValue(gcolcsSelect).ToString()) : 0;
            //        gvAvailableCostCentre.SetFocusedRowCellValue(gcolcsSelect, select == 0 ? 1 : 0);
            //    }
            //}
        }
        #endregion

        #region Methods
        private void BindCosteCenterCheckedListBox()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchCostCentreDetails();
                    DataTable dt = resultArgs.DataSource.Table;
                    gcAvailableCostCentre.DataSource = dt;
                    //CheckedListBoxBindDataSource(dt, chkAvailableCostCenter, dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName, dataManager.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void ProjectCostCentreMapping()
        {
            //On 21/11/2022, To Project-wise CC or Ledger-wise CC
            CCMappLedgerId = glkpCCLedger.EditValue == null || this.AppSetting.CostCeterMapping == 0 ? 0 : UtilityMember.NumberSet.ToInteger(glkpCCLedger.EditValue.ToString());
            using (MappingSystem projectSystem = new MappingSystem())
            {
                projectSystem.dtCostCenterIDCollection = gcCostCentre.DataSource as DataTable;
                if (gcAvailableCostCentre.DataSource != null && projectSystem.dtCostCenterIDCollection != null)
                {
                    if ((gcAvailableCostCentre.DataSource as DataTable).Rows.Count > 0 || projectSystem.dtCostCenterIDCollection.Rows.Count > 0)
                    {
                        if (AppSetting.CostCeterMapping == 0 ||
                            (AppSetting.CostCeterMapping == 1 && ((CCTotalDistributedAmount == CCMappLedgerOpAmount) || CCTotalDistributedAmount == 0)))
                        {
                            if (glkpCCProject.EditValue != null)
                            {
                                this.ShowWaitDialog();
                                projectSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpCCProject.EditValue.ToString());
                                projectSystem.OpeningBalanceDate = this.AppSetting.BookBeginFrom;
                                projectSystem.LedgerId = CCMappLedgerId;
                                resultArgs = projectSystem.AccountMappingCostCenter();
                                if (resultArgs.Success)
                                {
                                    mappingDialogResultCostCentre = DialogResult.OK;
                                    //To get ledger cc distributed amount
                                    FetchLedgerCCDistributionAmount(projectSystem.ProjectId);
                                    ShowSuccessMessage(GetMessage(MessageCatalog.Master.Mapping.COST_CENTER_MAPPING_SUCESS));
                                }
                                this.CloseWaitDialog();
                            }
                        }
                        else if (AppSetting.CostCeterMapping == 1 && CCTotalDistributedAmount != CCMappLedgerOpAmount)
                        {
                            MessageRender.ShowMessage("Ledger Opening Amount is not yet fully distributed with Cost Centre");
                        }
                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
                else
                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
            }
        }

        private void GeneralateHOLedgerMapping()
        {
            using (MappingSystem projectSystem = new MappingSystem())
            {
                projectSystem.dtGeneralateIDCollection = gcGenSelected.DataSource as DataTable;
                if (gcGenAvailable.DataSource != null && projectSystem.dtGeneralateIDCollection != null)
                {
                    if ((gcGenAvailable.DataSource as DataTable).Rows.Count > 0 || projectSystem.dtGeneralateIDCollection.Rows.Count > 0)
                    {
                        if (glkpGenLedger.EditValue != null)
                        {
                            this.ShowWaitDialog();
                            projectSystem.GeneralateId = UtilityMember.NumberSet.ToInteger(glkpGenLedger.EditValue.ToString());
                            projectSystem.OpeningBalanceDate = this.AppSetting.BookBeginFrom;
                            resultArgs = projectSystem.AccountMappingGeneralate();
                            if (resultArgs.Success)
                            {
                                mappingDialogResultGeneralate = DialogResult.OK;
                                ShowSuccessMessage(GetMessage("Generalate Mapping is Successful"));
                            }
                            this.CloseWaitDialog();
                        }

                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
                else
                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
            }
        }

        /// <summary>
        /// Project Purpose Mapping
        /// </summary>
        private void ProjectPurposeMapping()
        {
            using (MappingSystem projectSystem = new MappingSystem())
            {
                projectSystem.dtPurposeIDCollection = gcpurpose.DataSource as DataTable;
                if (chkAvailablePurpose.DataSource != null && projectSystem.dtPurposeIDCollection != null)
                {
                    if ((chkAvailablePurpose.DataSource as DataTable).Rows.Count > 0 || projectSystem.dtPurposeIDCollection.Rows.Count > 0)
                    {
                        if (glkpPurposeProject.EditValue != null)
                        {
                            if (ValidatePurpseCCDistributionAmount(projectSystem.dtPurposeIDCollection))
                            {
                                this.ShowWaitDialog();
                                projectSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpPurposeProject.EditValue.ToString());
                                projectSystem.OpeningBalanceDate = this.AppSetting.BookBeginFrom;
                                projectSystem.dtPurposeCCDistribution = dtAllProjectPurposeDistributed;
                                resultArgs = projectSystem.AccountMappingPurpose();
                                if (resultArgs.Success)
                                {
                                    MappingDialogResultPurpose = DialogResult.OK;
                                    ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.PURPOSE_MAPPED));
                                }
                                this.CloseWaitDialog();
                            }
                        }
                        {
                            gvPurpose.ShowEditor();
                        }
                    }
                    else
                        ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.NO_RECORD_IS_AVAILABLE));
                }
                else
                    ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.NO_RECORD_IS_AVAILABLE));
            }
        }

        /// <summary>
        /// On 08/07/2022, To check all the Purpose Opening Balnace with CC distribtuon amount
        /// # May not be distributed to CC
        /// # Fully distributed to CC
        /// # Suould not be partially distributed to CC
        /// 
        /// </summary>
        /// <param name="dtPurpose"></param>
        /// <returns></returns>
        private bool ValidatePurpseCCDistributionAmount(DataTable dtMappedPurpose)
        {
            bool rtn = false;
            bool mismatch = false;
            try
            {
                if (dtMappedPurpose != null && dtMappedPurpose.Rows.Count > 0)
                {
                    DataTable dtPurposeList = dtMappedPurpose.DefaultView.ToTable();
                    //dtPurposeList.DefaultView.RowFilter = "AMOUNT > 0";
                    foreach (DataRowView drv in dtPurposeList.DefaultView)
                    {
                        Int32 purposeid = UtilityMember.NumberSet.ToInteger(drv["CONTRIBUTION_ID"].ToString());
                        string purposename = drv["FC_PURPOSE"].ToString();
                        double opamount = UtilityMember.NumberSet.ToDouble(drv["AMOUNT"].ToString());
                        double opCCDistributedAmount = UtilityMember.NumberSet.ToDouble(dtAllProjectPurposeDistributed.Compute("SUM(AMOUNT)", "CONTRIBUTION_ID=" + purposeid).ToString());
                        if (opCCDistributedAmount > 0 && opamount != opCCDistributedAmount)
                        {
                            mismatch = true;
                            MessageRender.ShowMessage("Purpose '" + purposename + "' is not yet fully distributed with Cost Centre");
                            break;
                        }
                    }
                }
                rtn = !mismatch;
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
            }

            return rtn;
        }

        private void FetchMappedCostCenter()
        {
            //On 21/11/2022, To Project-wise CC or Ledger-wise CC
            CCMappLedgerId = glkpCCLedger.EditValue == null || this.AppSetting.CostCeterMapping == 0 ? 0 : UtilityMember.NumberSet.ToInteger(glkpCCLedger.EditValue.ToString());

            using (MappingSystem MappedCostCenter = new MappingSystem())
            {
                if (glkpCCProject.EditValue != null)
                {
                    MappedCostCenter.ProjectId = UtilityMember.NumberSet.ToInteger(glkpCCProject.EditValue.ToString());
                    MappedCostCenter.LedgerId = CCMappLedgerId;
                    resultArgs = MappedCostCenter.FetchMappedCostCenter();
                    if (resultArgs.DataSource.Table != null)
                    {
                        gcCostCentre.DataSource = AddColumns(resultArgs.DataSource.Table);

                        DataTable dtAllCostCenter = gcAvailableCostCentre.DataSource as DataTable;
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            if (dtAllCostCenter != null)
                            {
                                var RemoveMappedCostCenter = (from coscenter in dtAllCostCenter.AsEnumerable()
                                                              where (coscenter.Field<UInt32>(dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString()))
                                                              select coscenter);
                                if (RemoveMappedCostCenter.Count() > 0)
                                    dtAllCostCenter = RemoveMappedCostCenter.CopyToDataTable();
                                else
                                    dtAllCostCenter = dtAllCostCenter.Clone();
                            }
                        }
                        //CheckedListBoxBindDataSource(dtAllCostCenter, chkAvailableCostCenter, dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName, dataManager.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName);
                        gcAvailableCostCentre.DataSource = dtAllCostCenter;
                        ShowLedgerCCDistributedAmount();


                    }
                }
            }
        }

        private void ShowLedgerCCDistributedAmount()
        {
            if (AppSetting.CostCeterMapping == 1)
            {
                //Assign Total CC distributed Amount
                CCTotalDistributedAmount = 0;
                if (gcCostCentre.DataSource != null)
                {
                    DataTable dt = gcCostCentre.DataSource as DataTable;
                    double ccSumDRAmt = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(AMOUNT)", "TRANS_MODE='DR'").ToString());
                    double ccSumCRAmt = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                    CCTotalDistributedAmount = (ccSumDRAmt - ccSumCRAmt);
                    CCTotalDistributedAmount = Math.Abs(CCTotalDistributedAmount);
                }

                //Assign Ledger Opening Balance
                if (glkpCCLedger.EditValue != null && glkpCCLedger.GetSelectedDataRow() != null)
                {
                    using (LedgerSystem ledgersys = new LedgerSystem())
                    {
                        DataRowView drv = glkpCCLedger.GetSelectedDataRow() as DataRowView;
                        double ledgeropeningBalane = UtilityMember.NumberSet.ToDouble(drv[ledgersys.AppSchema.LedgerBalance.AMOUNTColumn.ColumnName].ToString());
                        string transmode = drv[ledgersys.AppSchema.LedgerBalance.TRANS_MODEColumn.ColumnName].ToString();
                        lblCCLedgerOpBalance.Text = UtilityMember.NumberSet.ToNumber(ledgeropeningBalane) + " " + transmode;

                        CCMappLedgerOpAmount = ledgeropeningBalane;
                        CCMappLedgerOpTransMode = transmode;
                    }
                }
            }
        }

        #endregion
        #endregion

        #region Map Cost Category

        #region Events
        private void glkCostCategory_EditValueChanged(object sender, EventArgs e)
        {
            BindCostCategoryCosCheckedListBox();
            FectMappedCostCategory();


        }
        private void repositoryItemCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            this.chkCCSelectedCostcentres.CheckedChanged -= new System.EventHandler(this.chkCCSelectedCostcentres_CheckedChanged);
            chkCCSelectedCostcentres.Checked = false;
            this.chkCCSelectedCostcentres.CheckedChanged += new System.EventHandler(this.chkCCSelectedCostcentres_CheckedChanged);
        }
        private void chkAvailableCategoryCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkCostCategorySelectAll.CheckedChanged -= new System.EventHandler(this.chkCostCategorySelectAll_CheckedChanged);
            chkCostCategorySelectAll.Checked = false;
            this.chkCostCategorySelectAll.CheckedChanged += new System.EventHandler(this.chkCostCategorySelectAll_CheckedChanged);
        }

        private void btnCCMoveOut_Click(object sender, EventArgs e)
        {
            MoveOutItems(gcCCCostCentre, chkAvailableCategoryCostCenter, dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName, dataManager.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName, glkCostCategory, false, true);
            lblCostCategoryMapRecordCount.Text = GetCountListBox(chkAvailableCategoryCostCenter);
        }

        private void btnCCMoceIn_Click(object sender, EventArgs e)
        {
            MoveInItems(gcCCCostCentre, chkAvailableCategoryCostCenter, dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName, dataManager.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName);
            chkCostCategorySelectAll.Checked = false;
            lblCostCategoryMapRecordCount.Text = GetCountListBox(chkAvailableCategoryCostCenter);
        }

        private void chkCostCategorySelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCostCategorySelectAll.Checked)
                {
                    SetCheckBoxSelection(chkAvailableCategoryCostCenter, this.GetMessage(MessageCatalog.Master.Mapping.UNSELECT_ALL), true);
                }
                else
                {
                    SetCheckBoxSelection(chkAvailableCategoryCostCenter, this.GetMessage(MessageCatalog.Master.Mapping.SELECT_ALL), false);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void btnCCSave_Click(object sender, EventArgs e)
        {
            using (MappingSystem MapCostCategory = new MappingSystem())
            {
                MapCostCategory.dtCostCategoryIDCollection = gcCCCostCentre.DataSource as DataTable;
                if (glkpDonorProject.EditValue != null)
                {
                    if (chkAvailableCategoryCostCenter.DataSource != null && MapCostCategory.dtCostCategoryIDCollection != null)
                    {
                        if ((chkAvailableCategoryCostCenter.DataSource as DataTable).Rows.Count > 0 || MapCostCategory.dtCostCategoryIDCollection.Rows.Count > 0)
                        {
                            MapCostCategory.CostCentreCategoryId = UtilityMember.NumberSet.ToInteger(glkCostCategory.EditValue.ToString());

                            this.ShowWaitDialog();
                            resultArgs = MapCostCategory.AccountMappingCostCategory();
                            if (resultArgs.Success)
                            {
                                mappingDialogResultCostCategory = DialogResult.OK;
                                ShowSuccessMessage(GetMessage(MessageCatalog.Master.Mapping.MAPPING_COST_CATEGORY_SUCCESS));
                            }
                            this.CloseWaitDialog();
                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
            }
        }

        private void btnCCClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResultCostCategory;
            this.Close();
        }

        private void chkCCShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvCCCostCentre.OptionsView.ShowAutoFilterRow = chkCCShowFilter.Checked;
            if (chkCCShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvCCCostCentre, gcolCCCostCentreName);
            }
        }

        /// <summary>
        /// Removed by chinna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void chkCCSelectedCostcentre_CheckedChanged(object sender, EventArgs e)
        //{
        //    DataTable dtUnMapCostCenter = SelectAll(((DataTable)gcCCCostCentre.DataSource), chkCCSelectedCostcentre);
        //    if (dtUnMapCostCenter != null)
        //    {
        //        gcCCCostCentre.DataSource = dtUnMapCostCenter;
        //    }
        //}

        #endregion

        #region Methods
        private void BindCostCategoryCosCheckedListBox()
        {
            try
            {
                chkAvailableCategoryCostCenter.Items.Clear();
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchCostCentreDetailsforCostCategory();
                    DataTable dt = resultArgs.DataSource.Table;
                    CheckedListBoxBindDataSource(dt, chkAvailableCategoryCostCenter, dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName, dataManager.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }



        private void LoadCostCategory()
        {
            try
            {
                using (CostCentreCategorySystem costcategorysystem = new CostCentreCategorySystem())
                {
                    resultArgs = costcategorysystem.FetchCostCentreCatogoryDetails();
                    glkCostCategory.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkCostCategory, resultArgs.DataSource.Table, costcategorysystem.AppSchema.CostCentreCategory.COST_CENTRE_CATEGORY_NAMEColumn.ColumnName, costcategorysystem.AppSchema.CostCentreCategory.COST_CENTRECATEGORY_IDColumn.ColumnName);
                        glkCostCategory.EditValue = glkCostCategory.Properties.GetKeyValue(0);
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void FectMappedCostCategory()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.CostCategoryId = UtilityMember.NumberSet.ToInteger(glkCostCategory.EditValue.ToString());
                    resultArgs = mappingSystem.FetchMappedCostCategory();
                    if (resultArgs.DataSource.Table != null)
                    {
                        gcCCCostCentre.DataSource = AddColumns(resultArgs.DataSource.Table);

                        DataTable dtAllCostCenter = chkAvailableCategoryCostCenter.DataSource as DataTable;
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            if (dtAllCostCenter != null)
                            {
                                var RemoveMappedCostCenter = (from coscenter in dtAllCostCenter.AsEnumerable()
                                                              where (coscenter.Field<UInt32>(dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString()))
                                                              select coscenter);
                                if (RemoveMappedCostCenter.Count() > 0)
                                    dtAllCostCenter = RemoveMappedCostCenter.CopyToDataTable();
                                else
                                    dtAllCostCenter = dtAllCostCenter.Clone();
                            }
                        }
                        CheckedListBoxBindDataSource(dtAllCostCenter, chkAvailableCategoryCostCenter, dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName, dataManager.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        #endregion

        #endregion

        #region Map Donor

        #region Events
        private void glkpDonorProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadAllDonors();
            FetchMappedDonor();
        }

        private void gvDonor_RowClick(object sender, RowClickEventArgs e)
        {
            DataTable dtDonor = (DataTable)gcDonor.DataSource;
            if (dtDonor != null && dtDonor.Rows.Count > 0)
            {
                if (gvDonor.GetFocusedRowCellValue(gvColDonorSelect) != null)
                {
                    if (gvDonor.FocusedColumn == gvColDonorSelect)
                    {
                        int select = gvDonor.GetFocusedRowCellValue(gvColDonorSelect) != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(gvDonor.GetFocusedRowCellValue(gvColDonorSelect).ToString()) : 0;
                        gvDonor.SetFocusedRowCellValue(gvColDonorSelect, select == 0 ? 1 : 0);
                    }
                }
            }

        }

        private void gvDonor_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        private void btnDonorMoveIn_Click(object sender, EventArgs e)
        {
            MoveInItems(gcDonor, chkAvailableDonors, dataManager.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName, dataManager.AppSchema.DonorAuditor.NAMEColumn.ColumnName);
            chkAvailableDonor.Checked = false;
            lblAvailabledonorRecordCount.Text = GetCountListBox(chkAvailableDonors);
        }

        private void btnDonorMoveOut_Click(object sender, EventArgs e)
        {
            MoveOutItems(gcDonor, chkAvailableDonors, dataManager.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName, dataManager.AppSchema.DonorAuditor.NAMEColumn.ColumnName, glkpDonorProject, true);
            lblAvailabledonorRecordCount.Text = GetCountListBox(chkAvailableDonors);
        }

        private void chkShowFilterDonor_CheckedChanged(object sender, EventArgs e)
        {
            gvDonor.OptionsView.ShowAutoFilterRow = chkShowFilterDonor.Checked;
            if (chkShowFilterDonor.Checked)
            {
                this.SetFocusRowFilter(gvDonor, gvColDonorName);
                //gvDonor.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                //gvDonor.FocusedColumn = gvColDonorName;
                //gvDonor.ShowEditor();
            }

        }

        private void chkUnMapDonor_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnMapDonor = SelectAll(((DataTable)gcDonor.DataSource), chkUnMapDonor);
            if (dtUnMapDonor != null)
            {
                gcDonor.DataSource = dtUnMapDonor;
            }
        }

        private void chkDonorCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.chkUnMapDonor.CheckedChanged -= new System.EventHandler(this.chkUnMapDonor_CheckedChanged);
            chkUnMapDonor.Checked = false;
            this.chkUnMapDonor.CheckedChanged += new System.EventHandler(this.chkUnMapDonor_CheckedChanged);
        }

        private void chkAvailableDonor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAvailableDonor.Checked)
                {
                    SetCheckBoxSelection(chkAvailableDonors, this.GetMessage(MessageCatalog.Master.Mapping.UNSELECT_ALL), true);
                }
                else
                {
                    SetCheckBoxSelection(chkAvailableDonors, this.GetMessage(MessageCatalog.Master.Mapping.SELECT_ALL), false);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void chkAvailableDonors_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkAvailableDonor.CheckedChanged -= new System.EventHandler(this.chkAvailableDonor_CheckedChanged);
            chkAvailableDonor.Checked = false;
            this.chkAvailableDonor.CheckedChanged += new System.EventHandler(this.chkAvailableDonor_CheckedChanged);
        }

        private void btnDonorSave_Click(object sender, EventArgs e)
        {
            using (MappingSystem MapDonor = new MappingSystem())
            {
                MapDonor.dtDonorMapping = gcDonor.DataSource as DataTable;
                if (glkpDonorProject.EditValue != null)
                {
                    if (chkAvailableDonors.DataSource != null && MapDonor.dtDonorMapping != null)
                    {
                        if ((chkAvailableDonors.DataSource as DataTable).Rows.Count > 0 || MapDonor.dtDonorMapping.Rows.Count > 0)
                        {
                            MapDonor.ProjectId = UtilityMember.NumberSet.ToInteger(glkpDonorProject.EditValue.ToString());
                            this.ShowWaitDialog();
                            resultArgs = MapDonor.MappDonor();
                            if (resultArgs.Success)
                            {
                                mappingDialogResultDonor = DialogResult.OK;
                                ShowSuccessMessage(GetMessage(MessageCatalog.Master.Mapping.MAPPING_DONOR_SUCCESS));
                            }
                            this.CloseWaitDialog();
                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
            }
        }

        private void btnDonorClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResultDonor;
            this.Close();
        }


        #endregion

        #region Methods
        /// <summary>
        /// To get all the Available Donors and bind them to checkedListBoxControl(chkAvailableDonors)
        /// </summary>
        private void LoadAllDonors()
        {
            try
            {
                using (MappingSystem donorSystem = new MappingSystem())
                {
                    resultArgs = donorSystem.LoadAllDonors();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAllDonor = resultArgs.DataSource.Table;
                        CheckedListBoxBindDataSource(dtAllDonor, chkAvailableDonors, dataManager.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName, dataManager.AppSchema.DonorAuditor.NAMEColumn.ColumnName);

                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }


        /// <summary>
        /// To get all the Available Purpose and bind them to checkedListBoxControl(chkAvailablePurpose)
        /// </summary>
        private void LoadAllPurpose()
        {
            try
            {
                using (MappingSystem donorSystem = new MappingSystem())
                {
                    resultArgs = donorSystem.LoadAllPurpose();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtAllPurpose = resultArgs.DataSource.Table;
                        CheckedListBoxBindDataSource(dtAllPurpose, chkAvailablePurpose, dataManager.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName, dataManager.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }




        private void FetchMappedDonor()
        {
            using (MappingSystem MappedDonor = new MappingSystem())
            {
                if (glkpDonorProject.EditValue != null)
                {
                    MappedDonor.ProjectId = UtilityMember.NumberSet.ToInteger(glkpDonorProject.EditValue.ToString());
                    resultArgs = MappedDonor.FetchMappedDonor();
                    if (resultArgs != null)
                    {
                        gcDonor.DataSource = AddColumns(resultArgs.DataSource.Table);
                        DataTable dtAllDonor = chkAvailableDonors.DataSource as DataTable;
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            if (dtAllDonor != null)
                            {
                                var RemoveMappedDonor = (from donor in dtAllDonor.AsEnumerable()
                                                         where (donor.Field<Int32?>(dataManager.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString()))
                                                         select donor);
                                if (RemoveMappedDonor.Count() > 0)
                                    dtAllDonor = RemoveMappedDonor.CopyToDataTable();
                                else
                                    dtAllDonor = dtAllDonor.Clone();
                            }
                        }
                        CheckedListBoxBindDataSource(dtAllDonor, chkAvailableDonors, dataManager.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName, dataManager.AppSchema.DonorAuditor.NAMEColumn.ColumnName);

                    }
                }
            }
        }

        /// <summary>
        /// To get fetch Purpose Details
        /// </summary>
        private void FetchMappedPurpose()
        {
            using (MappingSystem MappedDonor = new MappingSystem())
            {
                if (glkpPurposeProject.EditValue != null)
                {
                    MappedDonor.ProjectId = UtilityMember.NumberSet.ToInteger(glkpPurposeProject.EditValue.ToString());
                    resultArgs = MappedDonor.FetchMappedPurpose();
                    if (resultArgs != null)
                    {
                        gcpurpose.DataSource = AddColumns(resultArgs.DataSource.Table);
                        DataTable dtAllPurpose = chkAvailablePurpose.DataSource as DataTable;
                        gvPurpose.FocusedColumn = colPurposeOpeningBalance;
                        gvPurpose.ShowEditor();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            if (dtAllPurpose != null)
                            {
                                var RemoveMappedDonor = (from purpose in dtAllPurpose.AsEnumerable()
                                                         where (purpose.Field<Int32?>(dataManager.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName].ToString()))
                                                         select purpose);
                                if (RemoveMappedDonor.Count() > 0)
                                    dtAllPurpose = RemoveMappedDonor.CopyToDataTable();
                                else
                                    dtAllPurpose = dtAllPurpose.Clone();
                            }
                        }
                        CheckedListBoxBindDataSource(dtAllPurpose, chkAvailablePurpose, dataManager.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName, dataManager.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName);

                        //On 05/07/2022, To get distributed purpose amount
                        resultArgs = MappedDonor.FetchPurposeCostCentreDistribution();
                        if (resultArgs != null)
                        {
                            dtAllProjectPurposeDistributed = resultArgs.DataSource.Table;
                        }
                    }
                }
            }
        }

        #endregion
        #endregion

        #region Map Voucher Types

        #region Events
        private void btnVoucherMoveOut_Click(object sender, EventArgs e)
        {
            string VoucherName = string.Empty;
            string VoucherId = string.Empty;
            string VoucherType = string.Empty;
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    DataTable dtProjectVoucher = CreateColumns();

                    if (clstAvailableVouchers.CheckedItems.Count != 0)
                    {
                        foreach (DataRowView drvVoucher in clstAvailableVouchers.CheckedItems)
                        {
                            VoucherId += drvVoucher[0].ToString() + ",";
                            VoucherName += drvVoucher[1].ToString() + ",";
                            VoucherType += drvVoucher[2].ToString() + ",";
                        }
                        VoucherName = VoucherName.TrimEnd(',');
                        VoucherId = VoucherId.TrimEnd(',');
                        VoucherType = VoucherType.TrimEnd(',');
                        string[] Voucher_Name = VoucherName.Split(',');
                        string[] VoucherID = VoucherId.Split(',');
                        string[] Voucher_Type = VoucherType.Split(',');

                        for (int i = 0; i < Voucher_Name.Length; i++)
                        {
                            DataRow drRow = dtProjectVoucher.NewRow();
                            drRow[projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString()] = Voucher_Name[i].ToString();
                            drRow[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(VoucherID[i]);
                            drRow[projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(Voucher_Type[i]);
                            dtProjectVoucher.Rows.Add(drRow);
                        }
                        if (this.ProjectId == 0)
                        {
                            DataTable dtTable = new DataTable();
                            if (ValidateVouchersDetails(dtProjectVoucher))
                            {
                                //if (ValidateProjectVouchers(dtProjectVoucher, clstProjectVouchers.DataSource as DataTable))
                                //{
                                dtTable = clstProjectVouchers.DataSource as DataTable;
                                if (dtTable != null && dtTable.Rows.Count != 0 && dtProjectVoucher != null && dtProjectVoucher.Rows.Count != 0)
                                {
                                    for (int i = 0; i < dtProjectVoucher.Rows.Count; i++)
                                    {
                                        DataRow drRow = dtTable.NewRow();
                                        drRow[projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString()] = dtProjectVoucher.Rows[i][1].ToString();
                                        drRow[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(dtProjectVoucher.Rows[i][0].ToString());
                                        drRow[projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(dtProjectVoucher.Rows[i][2].ToString());
                                        dtTable.Rows.Add(drRow);
                                    }
                                    RemoveAvailableVouchers(clstAvailableVouchers.DataSource as DataTable, dtProjectVoucher, ProjectVoucher.MoveIn);
                                    AssignProjectVouchers(dtTable);
                                }
                                else
                                {
                                    RemoveAvailableVouchers(clstAvailableVouchers.DataSource as DataTable, dtProjectVoucher, ProjectVoucher.MoveIn);
                                    AssignProjectVouchers(dtProjectVoucher);
                                }
                                //}
                                //else
                                //{
                                //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_VOUCHER));
                                //}
                            }
                        }
                        else
                        {
                            if (ValidateVouchersDetails(dtProjectVoucher))
                            {
                                DataTable dtProVouchers = clstProjectVouchers.DataSource as DataTable;
                                DataTable dtAvailableVouchers = clstAvailableVouchers.DataSource as DataTable;
                                if (dtProVouchers != null && dtProVouchers.Rows.Count != 0)
                                {
                                    //if (ValidateProjectVouchers(dtProjectVoucher, dtProVouchers))
                                    //{
                                    for (int i = 0; i < dtProjectVoucher.Rows.Count; i++)
                                    {
                                        DataRow drRow = dtProVouchers.NewRow();
                                        drRow[projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString()] = dtProjectVoucher.Rows[i][1].ToString();
                                        drRow[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(dtProjectVoucher.Rows[i][0].ToString());
                                        drRow[projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(dtProjectVoucher.Rows[i][2].ToString());
                                        dtProVouchers.Rows.Add(drRow);
                                    }
                                    RemoveAvailableVouchers(dtAvailableVouchers, dtProjectVoucher, ProjectVoucher.MoveIn);
                                    AssignProjectVouchers(dtProVouchers);
                                    //}
                                    //else
                                    //{
                                    //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_PROJECT_VOUCHERS));
                                    //}
                                }
                                else
                                {
                                    RemoveAvailableVouchers(dtAvailableVouchers, dtProjectVoucher, ProjectVoucher.MoveIn);
                                    AssignProjectVouchers(dtProjectVoucher);
                                }
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

        private void btnVoucherMoveIn_Click(object sender, EventArgs e)
        {
            string VoucherName = string.Empty;
            string VoucherId = string.Empty;
            string VoucherType = string.Empty;
            try
            {
                DataTable dtProjectVoucher = CreateColumns();
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    if (clstProjectVouchers.CheckedItems.Count != 0)
                    {
                        foreach (DataRowView drvVoucher in clstProjectVouchers.CheckedItems)
                        {
                            VoucherId += drvVoucher[0].ToString() + ",";
                            VoucherName += drvVoucher[1].ToString() + ",";
                            VoucherType += drvVoucher[2].ToString() + ",";
                        }
                        VoucherName = VoucherName.TrimEnd(',');
                        VoucherId = VoucherId.TrimEnd(',');
                        VoucherType = VoucherType.TrimEnd(',');
                        string[] Voucher_Name = VoucherName.Split(',');
                        string[] VoucherID = VoucherId.Split(',');
                        string[] Voucher_Type = VoucherType.Split(',');

                        for (int i = 0; i < Voucher_Name.Length; i++)
                        {
                            DataRow drRow = dtProjectVoucher.NewRow();
                            drRow[projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString()] = Voucher_Name[i].ToString();
                            drRow[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(VoucherID[i].ToString());
                            drRow[projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(Voucher_Type[i].ToString());
                            dtProjectVoucher.Rows.Add(drRow);
                        }

                        if (this.ProjectId == 0)
                        {
                            BindAvailableVoucherDetails(dtProjectVoucher);
                        }
                        else
                        {
                            BindAvailableVoucherDetails(dtProjectVoucher);
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

        private void glkpProjectVoucher_EditValueChanged(object sender, EventArgs e)
        {
            FetchVoucherDetails();
            FetchDefaultProjectVouchers();
        }

        private void cboVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvVouchers = null;
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    if (cboVoucherType.SelectedIndex != 0)
                    {
                        dvVouchers = dtVoucherTypes.DefaultView;
                        dvVouchers.RowFilter = projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName + "=" + cboVoucherType.SelectedIndex;
                        clstAvailableVouchers.DataSource = dvVouchers.ToTable();
                        clstAvailableVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstAvailableVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
                    }
                    else
                    {
                        FetchDefaultProjectVouchers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally
            {
            }
        }

        private void btnVoucherSave_Click(object sender, EventArgs e)
        {
            DataTable dtProjectVouchers = new DataTable();
            if (ValidateVoucher())
            {
                dtProjectVouchers = clstProjectVouchers.DataSource as DataTable;
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    projectSystem.ProjectId = glkpProjectVoucher.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProjectVoucher.EditValue.ToString()) : 0;
                    projectSystem.dtProjectVouchers = dtProjectVouchers;
                    // projectSystem.VoucherId = drProject[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] != null ? this.UtilityMember.NumberSet.ToInteger(drProject[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                    resultArgs = projectSystem.SaveProjectVouchers();

                    if (resultArgs.Success)
                    {
                        if (mappingDialogResultVoucher.Equals(DialogResult.Cancel))
                            mappingDialogResultVoucher = DialogResult.OK;
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Master.Mapping.MASTER_VOUCHER_MAPPING));
                    }
                }
            }
        }

        private void btnVoucherclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Filter the records based on the Voucher Type.
        /// </summary>
        /// <param name="VoucherTypes"></param>

        private void FetchVoucherDetails()
        {
            VoucherProjectId = string.Empty;
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    this.ProjectId = glkpProjectVoucher.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProjectVoucher.EditValue.ToString()) : 0;
                    resultArgs = projectSystem.FetchSelectedProjectVouchers(this.ProjectId);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        foreach (DataRow drVouchers in resultArgs.DataSource.Table.Rows)
                        {
                            VoucherProjectId += drVouchers[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] + ",";
                        }
                        VoucherProjectId = VoucherProjectId.TrimEnd(',');
                        clstProjectVouchers.DataSource = resultArgs.DataSource.Table;
                        clstProjectVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstProjectVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
                    }
                    else
                    {
                        resultArgs = projectSystem.FetchVoucherTypes();
                        foreach (DataRow drVouchers in resultArgs.DataSource.Table.Rows)
                        {
                            VoucherProjectId += drVouchers[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] + ",";
                        }
                        VoucherProjectId = VoucherProjectId.TrimEnd(',');
                        clstProjectVouchers.DataSource = resultArgs.DataSource.Table;
                        clstProjectVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstProjectVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
                    }
                    cboVoucherType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Assign available voucher details based on project id.
        /// </summary>
        private void AssignVouchersForProjects()
        {
            try
            {
                if (this.ProjectId != 0)
                {
                    using (ProjectSystem projectSystem = new ProjectSystem())
                    {
                        resultArgs = projectSystem.ProjectVouchers(this.ProjectId);
                        clstAvailableVouchers.DataSource = resultArgs.DataSource.Table;
                        clstAvailableVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstAvailableVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
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
        /// Validate Voucher details.
        /// </summary>
        /// <param name="dtVouchers"></param>
        /// <returns></returns>
        private bool ValidateVouchersDetails(DataTable dtVouchers)
        {
            bool isVoucherTrue = true;
            int ReceiptCount = 0;
            int PaymentCount = 0;
            int ContraCount = 0;
            int JournalCount = 0;

            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    for (int i = 0; i < dtVouchers.Rows.Count; i++)
                    {
                        if (this.UtilityMember.NumberSet.ToInteger(dtVouchers.Rows[i][projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString()) == (int)DefaultVoucherTypes.Receipt)
                        {
                            ReceiptCount++;
                        }
                        else if (this.UtilityMember.NumberSet.ToInteger(dtVouchers.Rows[i][projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString()) == (int)DefaultVoucherTypes.Payment)
                        {
                            PaymentCount++;
                        }
                        else if (this.UtilityMember.NumberSet.ToInteger(dtVouchers.Rows[i][projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString()) == (int)DefaultVoucherTypes.Contra)
                        {
                            ContraCount++;
                        }
                        else if (this.UtilityMember.NumberSet.ToInteger(dtVouchers.Rows[i][projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString()) == (int)DefaultVoucherTypes.Journal)
                        {
                            JournalCount++;
                        }
                    }
                    //if (ReceiptCount > 1 || PaymentCount > 1 || ContraCount > 1 || JournalCount > 1)
                    //{
                    //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Project.PROJECT_VOUCHER));
                    //    isVoucherTrue = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }

            return isVoucherTrue;
        }

        /// <summary>
        /// Validate voucher details based on voucher type
        /// </summary>
        /// <param name="dtPVouchers"></param>
        /// <param name="dtProVoucher"></param>
        /// <returns></returns>
        private bool ValidateProjectVouchers(DataTable dtProVoucher)
        {
            bool isTrue = true;
            try
            {
                DataRow[] rows = dtProVoucher.Select("VOUCHER_ID IN (1,2,3,4)", "", System.Data.DataViewRowState.CurrentRows);
                if (rows.Length != 4)
                {
                    isTrue = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return isTrue;

            //bool isTrue = true;
            //try
            //{
            //    if (dtPVouchers != null && dtPVouchers.Rows.Count != 0 && dtProVoucher != null && dtProVoucher.Rows.Count != 0)
            //    {
            //        for (int i = 0; i < dtPVouchers.Rows.Count; i++)
            //        {
            //            for (int j = 0; j < dtProVoucher.Rows.Count; j++)
            //            {
            //                if (dtProVoucher.Rows[j][2].ToString() == dtPVouchers.Rows[i][2].ToString())
            //                {
            //                    isTrue = false;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally { }
            //return isTrue;
        }

        private bool isExistTransaction()
        {
            bool isValid = true;
            DataTable dtAvailable = clstAvailableVouchers.DataSource as DataTable;
            if (dtAvailable != null)
            {
                using (MappingSystem MapSystem = new MappingSystem())
                {
                    MapSystem.ProjectId = glkpProjectVoucher.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProjectVoucher.EditValue.ToString()) : 0;
                    foreach (DataRow dr in dtAvailable.Rows)
                    {
                        MapSystem.VoucherId = this.UtilityMember.NumberSet.ToInteger(dr["VOUCHER_ID"].ToString());
                        resultArgs = MapSystem.IsVoucherTransactionExists();
                        if (resultArgs != null && resultArgs.DataSource.Sclar.ToInteger > 0)
                        {
                            isValid = false;
                        }
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Reassign Project voucher details
        /// </summary>
        /// <param name="dtPVouchers"></param>
        private void AssignProjectVouchers(DataTable dtPVouchers)
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    if (dtPVouchers != null && dtPVouchers.Rows.Count != 0)
                    {
                        DataView dvProjectVouchers = dtPVouchers.DefaultView;
                        dvProjectVouchers.Sort = "VOUCHER_ID ASC";
                        dtPVouchers = dvProjectVouchers.Table;

                        clstProjectVouchers.DataSource = dtPVouchers;
                        clstProjectVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstProjectVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
                    }
                    else
                    {
                        clstProjectVouchers.DataSource = dtPVouchers;
                        clstProjectVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstProjectVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
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
        /// ReAssign AvailableVoucher details
        /// </summary>
        /// <param name="dtAvailableVoucher"></param>
        /// <param name="dtProjectVouchers"></param>
        private void RemoveAvailableVouchers(DataTable dtAvailableVoucher, DataTable dtProjectVouchers, ProjectVoucher projectVoucherStatus)
        {
            try
            {
                if (dtAvailableVoucher.Rows.Count == dtProjectVouchers.Rows.Count)
                {
                    dtAvailableVoucher = null;
                    if (projectVoucherStatus == ProjectVoucher.MoveIn)
                    {
                        AssignAvailableVouchers(dtAvailableVoucher);
                    }
                    else
                    {
                        AssignProjectVouchers(dtAvailableVoucher);
                    }
                }
                else
                {
                    DataTable dtTables = RemoveCheckdVoucher(dtAvailableVoucher, dtProjectVouchers);

                    if (dtTables != null && dtTables.Rows.Count != 0)
                    {
                        if (projectVoucherStatus == ProjectVoucher.MoveIn)
                        {
                            AssignAvailableVouchers(dtTables);
                        }
                        else
                        {
                            AssignProjectVouchers(dtTables);
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
        /// Construct table for voucher details
        /// </summary>
        /// <returns></returns>
        private DataTable CreateColumns()
        {
            DataTable dtVouchers = new DataTable();
            dtVouchers.Columns.Add(new DataColumn("VOUCHER_ID", typeof(int)));
            dtVouchers.Columns.Add(new DataColumn("VOUCHER_NAME", typeof(string)));
            dtVouchers.Columns.Add(new DataColumn("VOUCHER_TYPE", typeof(int)));
            return dtVouchers;
        }

        /// <summary>
        /// Assign Available voucher details
        /// </summary>
        /// <param name="dtAvailablevouchers"></param>
        private void AssignAvailableVouchers(DataTable dtAvailablevouchers)
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    if (dtAvailablevouchers != null && dtAvailablevouchers.Rows.Count != 0)
                    {
                        DataView dvAvailableVoucher = dtAvailablevouchers.DefaultView;
                        dvAvailableVoucher.Sort = "VOUCHER_ID ASC";
                        dtAvailablevouchers = dvAvailableVoucher.Table;

                        clstAvailableVouchers.DataSource = dtAvailablevouchers;
                        clstAvailableVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstAvailableVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
                    }
                    else
                    {
                        clstAvailableVouchers.DataSource = dtAvailablevouchers;
                        clstAvailableVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                        clstAvailableVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
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
        /// Remove voucher based on checked and unchecked vouchers.
        /// </summary>
        /// <param name="dtAvailableVouchers"></param>
        /// <param name="dtCheckVouchers"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public DataTable RemoveCheckdVoucher(DataTable dtAvailableVouchers, DataTable dtCheckVouchers)
        {
            DataTable dtVouchers = new DataTable();
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    var UncheckVouchers = dtAvailableVouchers.AsEnumerable()
                          .Where(row => !dtCheckVouchers.AsEnumerable()
                                                .Select(r => r.Field<string>(projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName))
                                                .Any(x => x == row.Field<string>(projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName))
                         ).CopyToDataTable();
                    dtVouchers = UncheckVouchers.DefaultView.Table;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return dtVouchers;
        }

        /// <summary>
        /// Bind voucher Details and push out the voucher details from selected projects to available projects.
        /// </summary>
        /// <param name="dtProjectVoucher"></param>
        private void BindAvailableVoucherDetails(DataTable dtProjectVoucher)
        {
            DataTable dtAvailableVouchers = new DataTable();
            using (ProjectSystem projectSystem = new ProjectSystem())
            {
                if (dtProjectVoucher != null && dtProjectVoucher.Rows.Count > 0)
                {
                    dtAvailableVouchers = clstAvailableVouchers.DataSource as DataTable;

                    if (dtAvailableVouchers != null && dtAvailableVouchers.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtProjectVoucher.Rows.Count; i++)
                        {
                            DataRow drProject = dtAvailableVouchers.NewRow();
                            drProject[projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString()] = dtProjectVoucher.Rows[i][1];
                            drProject[projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(dtProjectVoucher.Rows[i][0].ToString());
                            drProject[projectSystem.AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(dtProjectVoucher.Rows[i][2].ToString());
                            dtAvailableVouchers.Rows.Add(drProject);
                        }
                        RemoveAvailableVouchers(clstProjectVouchers.DataSource as DataTable, dtProjectVoucher, ProjectVoucher.MoveOut);
                        AssignAvailableVouchers(dtAvailableVouchers);
                    }
                    else
                    {
                        RemoveAvailableVouchers(clstProjectVouchers.DataSource as DataTable, dtProjectVoucher, ProjectVoucher.MoveOut);
                        AssignAvailableVouchers(dtProjectVoucher);
                    }
                }
            }
        }

        private bool ValidateVoucher()
        {
            DataTable dtProVouchers = clstProjectVouchers.DataSource as DataTable;
            int ProjectId = glkpProjectVoucher.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProjectVoucher.EditValue.ToString()) : 0;
            bool isProject = true;
            if (dtProVouchers != null && dtProVouchers.Rows.Count > 0)
            {
                if (!(ValidateProjectVouchers(dtProVouchers)))
                {
                    this.ShowMessageBox("Can not unmap Basic Voucher Type like Receipt,Payment,Contra,Journal");
                    clstProjectVouchers.Focus();
                    isProject = false;
                }
                else if (!(isExistTransaction()))
                {
                    this.ShowMessageBox("Voucher Type has Transaction. You can not unmap the Voucher Type");
                    clstAvailableVouchers.Focus();
                    isProject = false;
                }
            }

            if (isProject && ProjectId == 0)
            {
                this.ShowMessageBox("Select Project");
                isProject = false;
                glkpProjectVoucher.Select();
                glkpProjectVoucher.Focus();
            }

            return isProject;
        }

        private void FetchDefaultProjectVouchers()
        {
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    projectSystem.VoucherProjectId = VoucherProjectId;
                    resultArgs = projectSystem.FetchDefaultProjectVouchers();
                    dtVoucherTypes = resultArgs.DataSource.Table;
                    clstAvailableVouchers.DataSource = resultArgs.DataSource.Table;
                    clstAvailableVouchers.DisplayMember = projectSystem.AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName.ToString();
                    clstAvailableVouchers.ValueMember = projectSystem.AppSchema.Voucher.VOUCHER_IDColumn.ColumnName.ToString();
                    cboVoucherType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }
        #endregion


        #endregion

        #region Common Methods
        /// <summary>
        /// To set focus in the one of the GridView Columns and if check all checkbox should be shown or not
        /// </summary>
        /// <param name="gvName">Grid Name</param>
        /// <param name="GridColumnindex">Column index to be focused</param>
        /// <param name="IsProjectLedger">
        /// Optional parameter if the Check all CheckBox should be shown or not
        /// By default it is false
        /// </param>
        private void GetGridViewFocus(GridView gvName, int GridColumnindex, bool IsProjectLedger = false)
        {
            gvName.Focus();
            gvName.FocusedColumn = gvName.VisibleColumns[GridColumnindex];
            chkLedgerSelectAll.Visible = IsProjectLedger;
        }

        /// <summary>
        /// To set AcceptButton and Cancel button of the Tab page
        /// </summary>
        /// <param name="AcceptButton">object button of the AcceptButton</param>
        /// <param name="CancelButton">object button of the CancelButton</param>
        private void SetAcceptCancelButton(SimpleButton AcceptButton, SimpleButton CancelButton)
        {
            this.AcceptButton = AcceptButton;
            this.CancelButton = CancelButton;
        }

        /// <summary>
        /// To set all the rows of SELECT columns as zero 
        /// that is unchecking the CheckBox
        /// </summary>
        /// <param name="dtCheckedColumnSource">DataTable that is having the all row of SELECT columns as One</param>
        /// <returns>SELECT column with zero value</returns>
        private DataTable MakeSelectColumnZero(DataTable dtCheckedColumnSource)
        {
            if (dtCheckedColumnSource != null)
            {
                int i = 0;
                if (dtCheckedColumnSource.Columns.Contains(SELECT_COL))
                {
                    foreach (DataRow dr in dtCheckedColumnSource.Rows)
                    {
                        dtCheckedColumnSource.Rows[i++][SELECT_COL] = 0;
                    }
                }
                else
                    dtCheckedColumnSource = AddColumns(dtCheckedColumnSource);
            }
            return dtCheckedColumnSource;
        }

        private void MoveInItems(GridControl gcSource, CheckedListBoxControl chkAllItems, string FieldName, string DisplayField)
        {
            try
            {
                DataTable dtItemBindGrid = gcSource.DataSource as DataTable;
                DataTable dtItemBindCheckedListBox = chkAllItems.DataSource as DataTable;
                using (LedgerSystem FindFD = new LedgerSystem())
                {
                    if (grdlederName.Visible)
                    {
                        if (grdlederName.EditValue != null)
                        {
                            FindFD.LedgerId = UtilityMember.NumberSet.ToInteger(grdlederName.EditValue.ToString());
                            int LedgerGroup = FindFD.FetchLedgerGroupById();
                            if ((LedgerGroup.Equals((int)FixedLedgerGroup.FixedDeposit) && chkAllItems.CheckedItems.Count > 1) || (LedgerGroup.Equals((int)FixedLedgerGroup.FixedDeposit) && chkAllItems.CheckedItems.Count >= 1 && dtItemBindGrid.Rows.Count >= 1))
                                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_FD_LEDGER_RESTRICTION));
                            else
                                MoveInSubItems(chkAllItems, FieldName, ref dtItemBindGrid, ref dtItemBindCheckedListBox);
                        }
                    }
                    else
                        MoveInSubItems(chkAllItems, FieldName, ref dtItemBindGrid, ref dtItemBindCheckedListBox);
                    if (dtItemBindGrid != null)
                        gcSource.DataSource = AddColumns(dtItemBindGrid); //Binding the Grid control with the selected values
                    else
                        gcSource.DataSource = dtItemBindCheckedListBox;
                    //Binding the checkedListBoxControl with the unselected values
                    CheckedListBoxBindDataSource(dtItemBindCheckedListBox, chkAllItems, FieldName, DisplayField);
                }

            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            finally { }
        }

        private void MoveInSubItems(CheckedListBoxControl chkAllItems, string FieldName, ref DataTable dtItemBindGrid, ref DataTable dtItemBindCheckedListBox)
        {
            foreach (DataRowView drContribution in chkAllItems.CheckedItems)
            {
                //Getting the Checked Items Id one by one.
                string ItemCode = drContribution[FieldName].ToString();
                var SelectedItems = (from item in dtItemBindCheckedListBox.AsEnumerable()
                                     where Convert.ToString(item[FieldName]).Trim().Equals(ItemCode)
                                     select item);

                var RemoveSelectedItems = (from item in dtItemBindCheckedListBox.AsEnumerable()
                                           where !Convert.ToString(item[FieldName]).Trim().Equals(ItemCode)
                                           select item);

                if (RemoveSelectedItems.Count() > 0)
                    dtItemBindCheckedListBox = RemoveSelectedItems.CopyToDataTable();
                else
                {
                    //If the Row count is zero than the structure of the Datasource is assigned
                    DataTable dtContributionClone = (DataTable)chkAllItems.DataSource;
                    dtItemBindCheckedListBox = dtContributionClone.Clone();
                }
                if (SelectedItems.Count() > 0)
                {
                    if (dtItemBindGrid != null)
                    {
                        dtItemBindGrid.Merge(SelectedItems.CopyToDataTable());//when the Grid has some values the DatatTable is merged.
                    }
                    else
                        dtItemBindGrid = SelectedItems.CopyToDataTable(); //this is executed when the grid is empty
                }
                else
                {
                    DataTable dtContributionClone = (DataTable)chkAllItems.DataSource;
                    dtItemBindGrid = dtContributionClone.Clone();
                }
            }
        }

        private void MoveOutItems(GridControl gcSource, CheckedListBoxControl chkAllItems, string FieldName, string DisplayField, GridLookUpEdit grdlProjectName, bool IsDonor = false, bool IsCostCategory = false, bool IsFCPurpose = false)
        {
            DataTable dtMappedItems = gcSource.DataSource as DataTable;
            DataTable dtUnMappedItems = chkAllItems.DataSource as DataTable;
            string CheckedItemID = string.Empty;
            bool DeleteOPBal = false;
            bool Distributedfully = true;
            if (dtMappedItems != null)
            {
                var SelectedPurpose = (from donor in dtMappedItems.AsEnumerable()
                                       where (donor.Field<Int32?>(SELECT_COL).Equals(1))
                                       select donor);

                var UnSelectedPurpose = (from donor in dtMappedItems.AsEnumerable()
                                         where !(donor.Field<Int32?>(SELECT_COL).Equals(1))
                                         select donor);
                if (SelectedPurpose.Count() > 0)
                {
                    DataTable dtCheckedItems = SelectedPurpose.CopyToDataTable();
                    foreach (DataRow dr in dtCheckedItems.Rows)
                    {
                        CheckedItemID += dr[FieldName].ToString() + ',';
                    }
                    CheckedItemID = CheckedItemID.TrimEnd(',');
                    //On 15/11/2024
                    //if (glkpCCProject.EditValue != null)
                    //{
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                            if (IsDonor)
                            {
                                resultArgs = voucherTransaction.MadeTransactionDonor(CheckedItemID.TrimEnd(','));
                            }
                            else if (IsCostCategory)
                            {
                                using (MappingSystem mappingsystem = new MappingSystem())
                                {
                                    mappingsystem.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                                    mappingsystem.CostCenterIDs = CheckedItemID;
                                    resultArgs = mappingsystem.FetchCostCentreCategoryTransaction();
                                }
                            }
                            else if (IsFCPurpose)
                            {
                                using (MappingSystem mappingsystem = new MappingSystem())
                                {
                                    mappingsystem.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                                    mappingsystem.FCPurposeIDs = CheckedItemID;
                                    resultArgs = mappingsystem.FetchFCPurposeTransaction();
                                }
                            }
                            else
                            {
                                resultArgs = voucherTransaction.MadeTransaction(CheckedItemID.TrimEnd(','));
                            }
                            //if row count is zero than no transaction is made
                            if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                            {
                                if (dtCheckedItems.Columns.Contains(voucherTransaction.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName))
                                {
                                    //Check Fc purpose distribution amount
                                    Distributedfully = true;
                                    if (IsFCPurpose)
                                    {
                                        if (dtAllProjectPurposeDistributed != null && dtAllProjectPurposeDistributed.Rows.Count > 0)
                                        {
                                            DataTable dtPurposeDistribute = dtAllProjectPurposeDistributed.DefaultView.ToTable();
                                            dtPurposeDistribute.DefaultView.RowFilter = "CONTRIBUTION_ID IN (" + CheckedItemID + ")";
                                            Distributedfully = (dtPurposeDistribute.DefaultView.Count == 0);
                                            if (!Distributedfully)
                                            {
                                                ShowMessageBox("As Purpose opening amount is distributed, Can't unmap purpose");
                                            }
                                        }
                                    }

                                    //Check OpBalance
                                    if (Distributedfully)
                                    {
                                        foreach (DataRow dr in dtCheckedItems.Rows)
                                        {
                                            if (UtilityMember.NumberSet.ToDecimal(dr[voucherTransaction.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) > 0)
                                            {
                                                DeleteOPBal = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!DeleteOPBal && Distributedfully)
                                {
                                    if (dtUnMappedItems != null)
                                        dtUnMappedItems.Merge(SelectedPurpose.CopyToDataTable());
                                    else
                                        dtUnMappedItems = SelectedPurpose.CopyToDataTable();

                                    if (UnSelectedPurpose.Count() > 0)
                                        dtMappedItems = UnSelectedPurpose.CopyToDataTable();
                                    else
                                        dtMappedItems = dtMappedItems.Clone();
                                }
                                else if (DeleteOPBal)
                                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_MAKE_AMOUNT_ZERO));
                            }
                            else
                                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_TRANSACTION_MADE_ALREADY));
                        }
                    //}
                }
            }
            if (dtMappedItems != null)
            {
                gcSource.DataSource = dtMappedItems;
                if (dtMappedItems != null && dtMappedItems.Rows.Count != 0 && dtUnMappedItems != null && dtUnMappedItems.Rows.Count != 0)
                {
                    DataView dv = new DataView(dtUnMappedItems) { Sort = DisplayField };
                    CheckedListBoxBindDataSource(MakeSelectColumnZero(dv.ToTable()), chkAllItems, FieldName, DisplayField);
                }
            }
        }

        private void CheckedListBoxBindDataSource(DataTable BindDataSource, CheckedListBoxControl chkCtrl, string ValueMember, string DisplayMember)
        {
            if (BindDataSource != null)
            {
                chkCtrl.DataSource = BindDataSource;
                chkCtrl.ValueMember = ValueMember;
                chkCtrl.DisplayMember = DisplayMember;
            }
        }

        private void LoadProject(GridLookUpEdit grdlProject)
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = AppSetting.YearFrom;
                    resultArgs = LoadProjects(mappingSystem);
                    grdlProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(grdlProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        //  grdlProject.EditValue = grdlProject.Properties.GetKeyValue(0);
                        grdlProject.EditValue = this.AppSetting.UserProjectId;
                    }
                    //else
                    //{
                    //    if (this.ShowConfirmationMessage("Project is not yet created. Do you want to create Project now?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //    {
                    //        frmProjectAdd frmProject = new frmProjectAdd((int)AddNewRow.NewRow);
                    //        frmProject.ShowDialog();
                    //        if (frmProject.DialogResult == DialogResult.OK)
                    //        {
                    //            LoadProject(grdlProjectName);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private DataTable AddColumns(DataTable NewColumns)
        {
            if (!NewColumns.Columns.Contains(SELECT_COL))
                NewColumns.Columns.Add(SELECT_COL, typeof(Int32));
            return NewColumns;
        }

        private void LoadLedger()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchLedgerFD();
                    grdlederName.Properties.DataSource = null;
                    if (resultArgs != null)
                    {
                        dtLedgerDetails = resultArgs.DataSource.Table;
                        if (resultArgs.Success && resultArgs.DataSource != null && dtLedgerDetails.Rows.Count > 0)
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(grdlederName, dtLedgerDetails, mappingSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, mappingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                            grdlederName.EditValue = grdlederName.Properties.GetKeyValue(0);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadCCLedgerByProject()
        {
            Int32 PrevLedgerId = 0;
            try
            {
                if (this.AppSetting.CostCeterMapping == 1)
                {
                    using (MappingSystem mappingSystem = new MappingSystem())
                    {
                        Int32 Pid = glkpCCProject == null ? 0 : UtilityMember.NumberSet.ToInteger(glkpCCProject.EditValue.ToString());
                        //resultArgs = mappingSystem.LoadAllLedgerByProjectId(Pid);
                        resultArgs = mappingSystem.LoadCCLedgerByProjectId(Pid);
                        glkpCCLedger.Properties.DataSource = null;
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource != null)
                        {
                            PrevLedgerId = glkpCCLedger.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpCCLedger.EditValue.ToString()) : 0;
                            DataTable dtProjectCCLedger = resultArgs.DataSource.Table;
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCCLedger, dtProjectCCLedger, mappingSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, mappingSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                            glkpCCLedger.EditValue = null;

                            if (PrevLedgerId > 0)
                            {
                                if (glkpCCLedger.Properties.GetIndexByKeyValue(PrevLedgerId) >= 0)
                                {
                                    glkpCCLedger.EditValue = PrevLedgerId;
                                }
                                else
                                {
                                    glkpCCLedger.EditValue = glkpCCLedger.Properties.GetKeyValue(0);
                                }
                            }
                            else
                            {
                                glkpCCLedger.EditValue = glkpCCLedger.Properties.GetKeyValue(0);
                            }
                        }
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
        /// Generalate Ledger 19.07.2024
        /// </summary>
        private void LoadGeneralateLedger()
        {
            try
            {
                using (MappingSystem GeneralateLedgerMapping = new MappingSystem())
                {
                    if (grdlProjectName.EditValue != null)
                    {
                        resultArgs = GeneralateLedgerMapping.LoadGeneralateLedger();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            dtAllGeneralateLedger = resultArgs.DataSource.Table;
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpGenLedger, dtAllGeneralateLedger, "CON_LEDGER_NAME", "CON_LEDGER_ID");
                            glkpGenLedger.EditValue = glkpGenLedger.Properties.GetKeyValue(0);
                        }
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
        /// Load Ledger Common (Available which is mapped in the Project Category Ledgers
        /// </summary>
        private void LoadLedgerAllforGeneralateMap()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    if (grdlProjectName.EditValue != null && glkpGenLedger.EditValue != null)
                    {
                        mappingSystem.GeneralateId = UtilityMember.NumberSet.ToInteger(glkpGenLedger.EditValue.ToString());
                        mappingSystem.ProjectId = 0;
                        resultArgs = mappingSystem.FetchMappedLedgersGeneralate();
                        DataTable dtMapped = resultArgs.DataSource.Table;
                        dtMapped = AddColumns(dtMapped);
                        if (dtMapped != null)
                        {
                            gcGenAvailable.DataSource = dtMapped;
                        }

                        //gvcolBudgetType.UnGroup();
                        //gvcolBudgetType.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        /// <summary>
        ///  Fetch Mapped HO Ledger with Generalate and separated
        /// </summary>
        private void FetchMappedGeneralateLedger()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    if (glkpGenLedger != null)
                    {
                        // mappingSystem.ProjectId = UtilityMember.NumberSet.ToInteger(glkpBudgetProject.EditValue.ToString());
                        mappingSystem.GeneralateId = UtilityMember.NumberSet.ToInteger(glkpGenLedger.EditValue.ToString());
                        resultArgs = mappingSystem.FetchMappedGeneralateLedgers();
                        if (resultArgs.DataSource.Table != null)
                        {
                            gcGenSelected.DataSource = AddColumns(resultArgs.DataSource.Table);
                            DataTable dtAllLedgers = gcGenAvailable.DataSource as DataTable;
                            if (dtAllLedgers != null)
                            {
                                foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                                {
                                    var MappedLedgers = (from ledger in dtAllLedgers.AsEnumerable()
                                                         where (ledger.Field<UInt32>(dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName) != UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()))
                                                         select ledger);
                                    if (MappedLedgers.Count() > 0)
                                        dtAllLedgers = MappedLedgers.CopyToDataTable();
                                    else
                                        dtAllLedgers = dtAllLedgers.Clone();
                                }
                                gcGenAvailable.DataSource = dtAllLedgers;
                            }
                            //UpdateBalanceLedger();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void MoveInLedgerledgers()
        {
            DataTable dtMappedLedgers = gcProjectLedger.DataSource as DataTable;
            DataTable dtAvailableLedgers = gvAvailableLedger.DataSource as DataTable;
            if (dtAvailableLedgers != null)
            {
                //To get all the checked items from Available ledgers
                var CheckedLedgers = (from ledgers in dtAvailableLedgers.AsEnumerable()
                                      where ((ledgers.Field<Int32?>(SELECT_COL) == 1))
                                      select ledgers);
                //To get all the Unchecked item of Available ledgers
                var UnCheckedLedgers = (from ledgers in dtAvailableLedgers.AsEnumerable()
                                        where ((ledgers.Field<Int32?>(SELECT_COL) != 1))
                                        select ledgers);
                if (UnCheckedLedgers.Count() > 0)
                    dtAvailableLedgers = UnCheckedLedgers.CopyToDataTable();
                else
                    dtAvailableLedgers = (gvAvailableLedger.DataSource as DataTable).Clone();
                if (CheckedLedgers.Count() > 0)
                {
                    if (dtMappedLedgers != null)
                        dtMappedLedgers.Merge(CheckedLedgers.CopyToDataTable());
                    else
                        dtMappedLedgers = CheckedLedgers.CopyToDataTable();
                }
                else
                {
                    if (dtMappedLedgers == null)
                        dtMappedLedgers = (gcProjectLedger.DataSource as DataTable).Clone();
                }

                gcProjectLedger.DataSource = MakeSelectColumnZero(dtMappedLedgers);
                gvAvailableLedger.DataSource = dtAvailableLedgers;

                ShowSelectedLedgerCount();
                ShowAvailableLedgerCount();
            }
        }

        /// <summary>
        /// Move IN Budget Ledger 04.01.2020
        /// </summary>
        private void MoveInBudgetLedger()
        {
            // gvLedger, gvLedgerdetails - gcsbledger,gvsbledger -selected 
            // gvavailableLedger, gvAvailableLedgerdetails- gcabudgetledger,gvabudgetledger - available
            DataTable dtMappedLedgers = gcSBudLedger.DataSource as DataTable;
            DataTable dtAvailableLedgers = gcABudLedger.DataSource as DataTable;
            if (dtAvailableLedgers != null)
            {
                //To get all the checked items from Available ledgers
                var CheckedLedgers = (from ledgers in dtAvailableLedgers.AsEnumerable()
                                      where ((ledgers.Field<Int32?>(SELECT_COL) == 1))
                                      select ledgers);
                //To get all the Unchecked item of Available ledgers
                var UnCheckedLedgers = (from ledgers in dtAvailableLedgers.AsEnumerable()
                                        where ((ledgers.Field<Int32?>(SELECT_COL) != 1))
                                        select ledgers);
                if (UnCheckedLedgers.Count() > 0)
                    dtAvailableLedgers = UnCheckedLedgers.CopyToDataTable();
                else
                    dtAvailableLedgers = (gcABudLedger.DataSource as DataTable).Clone();
                if (CheckedLedgers.Count() > 0)
                {
                    if (dtMappedLedgers != null)
                        dtMappedLedgers.Merge(CheckedLedgers.CopyToDataTable());
                    else
                        dtMappedLedgers = CheckedLedgers.CopyToDataTable();
                }
                else
                {
                    if (dtMappedLedgers == null)
                        dtMappedLedgers = (gcSBudLedger.DataSource as DataTable).Clone();
                }

                gcSBudLedger.DataSource = MakeSelectColumnZero(dtMappedLedgers);
                gcABudLedger.DataSource = dtAvailableLedgers;
            }
        }

        private void MoveOutLedger()
        {
            try
            {
                string CheckedItemID = string.Empty;
                bool DeleteOPBal = false;
                DataTable dtMappedItems = gcProjectLedger.DataSource as DataTable;
                DataTable dtUnMappedItems = gvAvailableLedger.DataSource as DataTable;
                if (dtMappedItems != null)
                {
                    var CheckedItems = (from Checkeditems in dtMappedItems.AsEnumerable()
                                        where (Checkeditems.Field<Int32?>(SELECT_COL) == 1)
                                        select Checkeditems);

                    var UnCheckedItems = (from Uncheckeditems in dtMappedItems.AsEnumerable()
                                          where (Uncheckeditems.Field<Int32?>(SELECT_COL) != 1)
                                          select Uncheckeditems);
                    if (CheckedItems.Count() > 0)
                    {
                        dtCheckedItems = CheckedItems.CopyToDataTable();
                        //Getting all the checked ledger Ids to check if any tranaction is already done.
                        foreach (DataRow dr in dtCheckedItems.Rows)
                        {
                            CheckedItemID += dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() + ',';
                        }
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                            resultArgs = voucherTransaction.MadeTransaction(CheckedItemID.TrimEnd(','));
                            //if row count is zero than no transaction is made
                            if (resultArgs.DataSource.Table.Rows.Count == 0)
                            {
                                resultArgs = voucherTransaction.MadeTransactionForBudget(CheckedItemID.TrimEnd(','));
                                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                                {
                                    foreach (DataRow dr in dtCheckedItems.Rows)
                                    {
                                        int ledgerId = UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                        bool isNotCCDistributed = IsNotCCLedgerDistributed(ledgerId);

                                        if (UtilityMember.NumberSet.ToDecimal(dr[dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) == 0
                                            && isNotCCDistributed == false)
                                        {
                                            DeleteOPBal = true;
                                        }
                                        else
                                        {
                                            DeleteOPBal = false;
                                            break;
                                        }
                                    }
                                    if (DeleteOPBal)
                                    {
                                        if (dtUnMappedItems != null)
                                            dtUnMappedItems.Merge(dtCheckedItems);
                                        else
                                            dtUnMappedItems = dtCheckedItems;

                                        if (UnCheckedItems.Count() > 0)
                                            dtMappedItems = UnCheckedItems.CopyToDataTable();
                                        else
                                            dtMappedItems = dtMappedItems.Clone();
                                    }
                                    else
                                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_MAKE_AMOUNT_ZERO));
                                }
                                else
                                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_BUDGET_MADE));
                            }
                            else
                            {
                                string[] IdList = CheckedItemID.Split(',');
                                DataTable dtItem = dtCheckedItems;
                                foreach (DataRow drledger in dtCheckedItems.Rows)
                                {
                                    voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                                    int ledgerId = UtilityMember.NumberSet.ToInteger(drledger[dataManager.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                    resultArgs = voucherTransaction.MadeTransaction(ledgerId.ToString());
                                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 0)
                                    {
                                        resultArgs = voucherTransaction.MadeTransactionForBudget(ledgerId.ToString());
                                        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                                        {
                                            DataTable dtItems = dtCheckedItems;
                                            DataView dvItems = new DataView();
                                            dvItems = dtItems.DefaultView;
                                            dvItems.RowFilter = "LEDGER_ID ='" + ledgerId.ToString() + "'";
                                            dtItems = dvItems.ToTable();
                                            if (dtItems != null && dtItems.Rows.Count > 0)
                                            {
                                                double amount = UtilityMember.NumberSet.ToDouble(dtItems.Rows[0][dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                                                bool isNotCCDistributed = IsNotCCLedgerDistributed(ledgerId);
                                                if (amount == 0 && isNotCCDistributed == false)
                                                {
                                                    DeleteOPBal = true;
                                                }
                                                else
                                                {
                                                    DeleteOPBal = false;
                                                }
                                            }

                                            if (DeleteOPBal)
                                            {
                                                if (dtUnMappedItems != null)
                                                {
                                                    dtUnMappedItems.ImportRow(drledger);
                                                    DataView dvmapped = dtMappedItems.DefaultView;
                                                    dvmapped.RowFilter = "LEDGER_ID <>" + ledgerId.ToString();
                                                    dtMappedItems = dvmapped.ToTable();
                                                }
                                                else
                                                    dtUnMappedItems = dtCheckedItems;

                                                //if (UnCheckedItems.Count() > 0)
                                                //    dtMappedItems = UnCheckedItems.CopyToDataTable();
                                                //else
                                                //    dtMappedItems = dtMappedItems.Clone();
                                            }
                                            //else
                                            //    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_MAKE_AMOUNT_ZERO));
                                        }
                                        //else
                                        //    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_BUDGET_MADE));
                                    }
                                }
                            }
                            // ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_TRANSACTION_MADE_ALREADY));
                        }

                    }

                }
                if (dtMappedItems != null)
                {
                    gcProjectLedger.DataSource = dtMappedItems;
                    DataView dv = new DataView(dtUnMappedItems) { Sort = "SORT_ID" };
                    gvAvailableLedger.DataSource = MakeSelectColumnZero(dv.ToTable());
                }
                ShowSelectedLedgerCount();
                ShowAvailableLedgerCount();
            }
            catch (Exception ex)
            {

            }
        }

        private void MoveOutLedger1()
        {
            try
            {
                string CheckedItemID = string.Empty;
                bool DeleteOPBal = false;
                DataTable dtMappedItems = gcProjectLedger.DataSource as DataTable;
                DataTable dtUnMappedItems = gvAvailableLedger.DataSource as DataTable;

                if (dtMappedItems != null)
                {
                    var CheckedItems = (from Checkeditems in dtMappedItems.AsEnumerable()
                                        where (Checkeditems.Field<Int32?>(SELECT_COL) == 1 && Checkeditems.Field<decimal?>("AMOUNT") == 0
                                                && Checkeditems.Field<UInt32?>("ACCESS_FLAG") != 2)
                                        select Checkeditems);

                    var UnCheckedItems = (from Uncheckeditems in dtMappedItems.AsEnumerable()
                                          where (Uncheckeditems.Field<Int32?>(SELECT_COL) != 1 && Uncheckeditems.Field<decimal?>("AMOUNT") > 0
                                                        && Uncheckeditems.Field<UInt32?>("ACCESS_FLAG") == 0)
                                          select Uncheckeditems);
                    if (CheckedItems.Count() > 0)
                    {
                        this.ShowMessageBox("Default Ledger(s)/Used Ledger(s) will not be unmapped");
                        this.ShowWaitDialog("Processing unmap selected Ledger(s)");
                        dtCheckedItems = CheckedItems.CopyToDataTable();
                        //Getting all the checked ledger Ids to check if any tranaction is already done.
                        foreach (DataRow dr in dtCheckedItems.Rows)
                        {
                            CheckedItemID += dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() + ',';
                        }
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                            resultArgs = voucherTransaction.MadeTransaction(CheckedItemID.TrimEnd(','));
                            //if row count is zero than no transaction is made
                            if (resultArgs.DataSource.Table.Rows.Count == 0)
                            {
                                resultArgs = voucherTransaction.MadeTransactionForBudget(CheckedItemID.TrimEnd(','));
                                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                                {
                                    foreach (DataRow dr in dtCheckedItems.Rows)
                                    {
                                        int ledgerId = UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                        bool isNotCCDistributed = IsNotCCLedgerDistributed(ledgerId);

                                        if (UtilityMember.NumberSet.ToDecimal(dr[dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) == 0
                                            && !isNotCCDistributed)
                                        {
                                            DeleteOPBal = true;
                                        }
                                        else
                                        {
                                            DeleteOPBal = false;
                                            break;
                                        }
                                    }
                                    if (DeleteOPBal)
                                    {
                                        if (dtUnMappedItems != null)
                                            dtUnMappedItems.Merge(dtCheckedItems);
                                        else
                                            dtUnMappedItems = dtCheckedItems;

                                        if (UnCheckedItems.Count() > 0)
                                            dtMappedItems = UnCheckedItems.CopyToDataTable();
                                        else
                                            dtMappedItems = dtMappedItems.Clone();
                                    }
                                    else
                                        ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_MAKE_AMOUNT_ZERO));
                                }
                                else
                                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_BUDGET_MADE));
                            }
                            else
                            {
                                string[] IdList = CheckedItemID.Split(',');
                                DataTable dtItem = dtCheckedItems;
                                foreach (DataRow drledger in dtCheckedItems.Rows)
                                {
                                    voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                                    int ledgerId = UtilityMember.NumberSet.ToInteger(drledger[dataManager.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                    resultArgs = voucherTransaction.MadeTransaction(ledgerId.ToString());
                                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 0)
                                    {
                                        resultArgs = voucherTransaction.MadeTransactionForBudget(ledgerId.ToString());
                                        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                                        {
                                            DataTable dtItems = dtCheckedItems;
                                            DataView dvItems = new DataView();
                                            dvItems = dtItems.DefaultView;
                                            dvItems.RowFilter = "LEDGER_ID ='" + ledgerId.ToString() + "'";
                                            dtItems = dvItems.ToTable();
                                            if (dtItems != null && dtItems.Rows.Count > 0)
                                            {
                                                double amount = UtilityMember.NumberSet.ToDouble(dtItems.Rows[0][dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                                                bool isNotCCDistributed = IsNotCCLedgerDistributed(ledgerId);
                                                if (amount == 0 && isNotCCDistributed)
                                                {
                                                    DeleteOPBal = true;
                                                }
                                                else
                                                {
                                                    DeleteOPBal = false;
                                                }
                                            }

                                            if (DeleteOPBal)
                                            {
                                                if (dtUnMappedItems != null)
                                                {
                                                    dtUnMappedItems.ImportRow(drledger);
                                                    DataView dvmapped = dtMappedItems.DefaultView;
                                                    dvmapped.RowFilter = "LEDGER_ID <>" + ledgerId.ToString();
                                                    dtMappedItems = dvmapped.ToTable();
                                                }
                                                else
                                                    dtUnMappedItems = dtCheckedItems;

                                                //if (UnCheckedItems.Count() > 0)
                                                //    dtMappedItems = UnCheckedItems.CopyToDataTable();
                                                //else
                                                //    dtMappedItems = dtMappedItems.Clone();
                                            }
                                            //else
                                            //    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_MAKE_AMOUNT_ZERO));
                                        }
                                        //else
                                        //    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_BUDGET_MADE));
                                    }
                                }
                            }
                            // ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_TRANSACTION_MADE_ALREADY));
                        }

                    }

                }
                if (dtMappedItems != null)
                {
                    gcProjectLedger.DataSource = dtMappedItems;
                    DataView dv = new DataView(dtUnMappedItems) { Sort = "SORT_ID" };
                    gvAvailableLedger.DataSource = MakeSelectColumnZero(dv.ToTable());
                }
                ShowSelectedLedgerCount();
                ShowAvailableLedgerCount();
                this.CloseWaitDialog();
            }
            catch (Exception ex)
            {
                this.CloseWaitDialog();
            }
        }

        /// <summary>
        /// On 21/11/2022, To check Ledger Opening Balnace with CC distribtuon amount
        /// # not distributed to CC
        /// # Fully distributed to CC
        /// # Should not be partially distributed to CC
        /// </summary>
        /// <param name="dtPurpose"></param>
        /// <returns></returns>
        private bool ValidateLedgerCCDistributionAmount(DataTable dtMappedLedgers)
        {
            bool rtn = false;
            bool mismatch = false;
            try
            {
                if (this.AppSetting.CostCeterMapping == 1 && dtMappedLedgers != null && dtMappedLedgers.Rows.Count > 0)
                {
                    DataTable dtPurposeList = dtMappedLedgers.DefaultView.ToTable();
                    //dtPurposeList.DefaultView.RowFilter = "AMOUNT > 0";
                    foreach (DataRowView drv in dtPurposeList.DefaultView)
                    {
                        Int32 ledgerid = UtilityMember.NumberSet.ToInteger(drv["LEDGER_ID"].ToString());
                        string ledgername = drv["LEDGER_NAME"].ToString();
                        double opamount = UtilityMember.NumberSet.ToDouble(drv["AMOUNT"].ToString());
                        double opCCDistributedAmount = UtilityMember.NumberSet.ToDouble(dtAllProjectLedgerCCDistributed.Compute("SUM(AMOUNT)", "LEDGER_ID=" + ledgerid).ToString());
                        if (opCCDistributedAmount > 0 && opamount != opCCDistributedAmount)
                        {
                            mismatch = true;

                            if (opamount == 0 && opCCDistributedAmount > 0)
                            {
                                MessageRender.ShowMessage("Ledger '" + ledgername + "' Opening Balance is already distributed with Cost Centre, Clear Cost Centre distribution");
                            }
                            else
                            {
                                MessageRender.ShowMessage("Ledger '" + ledgername + "' Opening Balance is not yet fully distributed with Cost Centre");
                            }
                            break;
                        }
                    }
                }
                rtn = !mismatch;
            }
            catch (Exception err)
            {
                this.ShowMessageBox(err.Message);
            }

            return rtn;
        }

        /// <summary>
        /// On 22/11/2022, To check given ledger op distributed or not
        /// </summary>
        /// <param name="Ledgerid"></param>
        /// <returns></returns>
        private bool IsNotCCLedgerDistributed(Int32 Ledgerid)
        {
            bool rtn = true;
            bool CCLedgerDistributedfully = false;
            if (this.AppSetting.CostCeterMapping == 1)
            {
                if (dtAllProjectLedgerCCDistributed != null && dtAllProjectLedgerCCDistributed.Rows.Count > 0)
                {
                    DataTable dtLedgerDistribute = dtAllProjectLedgerCCDistributed.DefaultView.ToTable();
                    //dtAllProjectLedgerCCDistributed.DefaultView.RowFilter = "LEDGER_ID = " + Ledgerid;
                    dtLedgerDistribute.DefaultView.RowFilter = "LEDGER_ID = " + Ledgerid;
                    CCLedgerDistributedfully = (dtLedgerDistribute.DefaultView.Count == 0);
                    if (CCLedgerDistributedfully)
                    {
                        rtn = false;
                    }
                }
                else
                {
                    rtn = false;
                }
            }
            else
            {
                rtn = false;
            }
            return rtn;
        }

        /// <summary>
        /// Move Out Budget Ledger on 04.01.2020
        /// </summary>
        private void MoveOutBudgetLedger()
        {
            // gvLedger, gvLedgerdetails - gcsbledger,gvsbledger -selected 
            // gvavailableLedger, gvAvailableLedgerdetails- gcabudgetledger,gvabudgetledger - available
            try
            {
                string CheckedItemID = string.Empty;
                bool DeleteOPBal = false;
                DataTable dtMappedItems = gcSBudLedger.DataSource as DataTable;
                DataTable dtUnMappedItems = gcABudLedger.DataSource as DataTable;
                if (dtMappedItems != null)
                {
                    var CheckedItems = (from Checkeditems in dtMappedItems.AsEnumerable()
                                        where (Checkeditems.Field<Int32?>(SELECT_COL) == 1)
                                        select Checkeditems);

                    var UnCheckedItems = (from Uncheckeditems in dtMappedItems.AsEnumerable()
                                          where (Uncheckeditems.Field<Int32?>(SELECT_COL) != 1)
                                          select Uncheckeditems);
                    if (CheckedItems.Count() > 0)
                    {
                        dtCheckedItems = CheckedItems.CopyToDataTable();
                        //Getting all the checked ledger Ids to check if any tranaction is already done.
                        foreach (DataRow dr in dtCheckedItems.Rows)
                        {
                            CheckedItemID += dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString() + ',';
                        }
                        using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                        {
                            voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(glkpBudgetProject.EditValue.ToString());
                            resultArgs = voucherTransaction.MadeTransaction(CheckedItemID.TrimEnd(','));
                            //if row count is zero than no transaction is made
                            if (resultArgs.Success)
                            {
                                resultArgs = voucherTransaction.MadeTransactionForBudget(CheckedItemID.TrimEnd(','));
                                if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                                {
                                    if (dtUnMappedItems != null)
                                        dtUnMappedItems.Merge(dtCheckedItems);
                                    else
                                        dtUnMappedItems = dtCheckedItems;

                                    if (UnCheckedItems.Count() > 0)
                                        dtMappedItems = UnCheckedItems.CopyToDataTable();
                                    else
                                        dtMappedItems = dtMappedItems.Clone();

                                    //foreach (DataRow dr in dtCheckedItems.Rows)
                                    //{
                                    //    if (UtilityMember.NumberSet.ToDecimal(dr[dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) == 0)
                                    //    {
                                    //        DeleteOPBal = true;
                                    //    }
                                    //    else
                                    //    {
                                    //        DeleteOPBal = false;
                                    //        break;
                                    //    }
                                    //}
                                    //if (DeleteOPBal)
                                    //{
                                    //    if (dtUnMappedItems != null)
                                    //        dtUnMappedItems.Merge(dtCheckedItems);
                                    //    else
                                    //        dtUnMappedItems = dtCheckedItems;

                                    //    if (UnCheckedItems.Count() > 0)
                                    //        dtMappedItems = UnCheckedItems.CopyToDataTable();
                                    //    else
                                    //        dtMappedItems = dtMappedItems.Clone();
                                    //}
                                    //else
                                    //    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_MAKE_AMOUNT_ZERO));
                                }
                                else
                                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_BUDGET_MADE));
                            }
                            else
                            {
                                string[] IdList = CheckedItemID.Split(',');
                                DataTable dtItem = dtCheckedItems;
                                foreach (DataRow drledger in dtCheckedItems.Rows)
                                {
                                    voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                                    int ledgerId = UtilityMember.NumberSet.ToInteger(drledger[dataManager.AppSchema.VoucherTransaction.LEDGER_IDColumn.ColumnName].ToString());
                                    resultArgs = voucherTransaction.MadeTransaction(ledgerId.ToString());
                                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count == 0)
                                    {
                                        resultArgs = voucherTransaction.MadeTransactionForBudget(ledgerId.ToString());
                                        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                                        {
                                            DataTable dtItems = dtCheckedItems;
                                            DataView dvItems = new DataView();
                                            dvItems = dtItems.DefaultView;
                                            dvItems.RowFilter = "LEDGER_ID ='" + ledgerId.ToString() + "'";
                                            dtItems = dvItems.ToTable();
                                            if (dtItems != null && dtItems.Rows.Count > 0)
                                            {
                                                double amount = UtilityMember.NumberSet.ToDouble(dtItems.Rows[0][dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString());
                                                if (amount == 0)
                                                {
                                                    DeleteOPBal = true;
                                                }
                                                else
                                                {
                                                    DeleteOPBal = false;
                                                }
                                            }

                                            if (DeleteOPBal)
                                            {
                                                if (dtUnMappedItems != null)
                                                {
                                                    dtUnMappedItems.ImportRow(drledger);
                                                    DataView dvmapped = dtMappedItems.DefaultView;
                                                    dvmapped.RowFilter = "LEDGER_ID <>" + ledgerId.ToString();
                                                    dtMappedItems = dvmapped.ToTable();
                                                }
                                                else
                                                    dtUnMappedItems = dtCheckedItems;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                if (dtMappedItems != null)
                {
                    gcSBudLedger.DataSource = dtMappedItems;
                    //04.01.2020
                    //DataView dv = new DataView(dtUnMappedItems) { Sort = "SORT_ID" };
                    DataView dv = new DataView(dtUnMappedItems);
                    gcABudLedger.DataSource = MakeSelectColumnZero(dv.ToTable());
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void MoveOutCostCentreGridItems()
        {
            string CheckedItemID = string.Empty;
            bool DeleteOPBal = false;
            DataTable dtMappedItems = gcCostCentre.DataSource as DataTable;
            DataTable dtUnMappedItems = gcAvailableCostCentre.DataSource as DataTable;
            DataTable dtChkdCosrCentre = new DataTable();
            DataTable dtunmapCcentre = dtMappedItems.Clone();
            bool IsMoveIn = true;
            if (dtMappedItems != null)
            {
                DataTable dtCCAMppedSelected = dtMappedItems.DefaultView.ToTable();
                dtCCAMppedSelected.DefaultView.RowFilter = "SELECT=1";
                //int[] SelRow = gvCostCentre.GetSelectedRows();
                //if (SelRow.Count() > 0)
                if (dtCCAMppedSelected.DefaultView.Count > 0)
                {
                    //Getting all the checked Cost Centre Ids to check if any tranaction is already done.
                    DataRow drSelecRow;
                    foreach (DataRowView drv in dtCCAMppedSelected.DefaultView)
                    {
                        drSelecRow = drv.Row;
                        string LastCCId = drSelecRow[dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString();
                        if (!CheckedItemID.Contains(LastCCId))
                        {
                            dtunmapCcentre.ImportRow(drSelecRow);
                            CheckedItemID += drSelecRow[dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString() + ',';
                        }

                        /*drSelecRow = gvCostCentre.GetDataRow(item);
                        if (drSelecRow != null)
                        {
                            
                            if (!CheckedItemID.Contains(LastCCId))
                            {
                                dtunmapCcentre.ImportRow(drSelecRow);
                                CheckedItemID += drSelecRow[dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString() + ',';
                            }
                        }*/
                    }
                    using (MappingSystem mappingsystem = new MappingSystem())
                    {
                        mappingsystem.CostCenterIDs = CheckedItemID.TrimEnd(',');
                        mappingsystem.ProjectId = glkpCCProject != null ? UtilityMember.NumberSet.ToInteger(glkpCCProject.EditValue.ToString()) : 0;

                        mappingsystem.LedgerId = 0;
                        if (this.AppSetting.CostCeterMapping == 1)
                        {
                            mappingsystem.LedgerId = glkpCCLedger != null ? UtilityMember.NumberSet.ToInteger(glkpCCLedger.EditValue.ToString()) : 0;
                        }
                        resultArgs = mappingsystem.FetchCostCentreTransaction();
                        //if row count is zero than no transaction is made
                        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count == 0)
                        {
                            foreach (DataRow dr in dtunmapCcentre.Rows)
                            {
                                if (UtilityMember.NumberSet.ToDecimal(dr[dataManager.AppSchema.VoucherTransaction.AMOUNTColumn.ColumnName].ToString()) == 0)
                                {
                                    DeleteOPBal = true;
                                }
                                else
                                {
                                    DeleteOPBal = false;
                                    break;
                                }
                            }
                            if (DeleteOPBal)
                            {
                                if (dtUnMappedItems != null)
                                    dtUnMappedItems.Merge(dtunmapCcentre);
                                else
                                    dtUnMappedItems = dtunmapCcentre;
                            }
                            else
                            {
                                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_MAKE_AMOUNT_ZERO));
                                IsMoveIn = false;
                            }

                        }
                        else
                        {
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.MAPPING_TRANSACTION_MADE_ALREADY));
                            IsMoveIn = false;
                        }
                    }
                }
            }
            if (dtMappedItems != null && dtUnMappedItems != null && IsMoveIn)
            {
                DataView dvmpp = dtMappedItems.AsDataView();
                gcCostCentre.DataSource = null;
                if (dvmpp != null)
                {
                    dvmpp.RowFilter = dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName + " NOT IN (" + CheckedItemID.Trim(',') + ")";
                    if (dvmpp != null && dvmpp.Table.Rows.Count > 0)
                    {
                        dtChkdCosrCentre.Merge(dvmpp.ToTable());
                    }
                }
                gcCostCentre.DataSource = dtChkdCosrCentre;

                DataView dv = new DataView(dtUnMappedItems);
                gcAvailableCostCentre.DataSource = MakeSelectColumnZero(dv.ToTable());
            }
        }

        private void MoveInCostCentreGridItems()
        {
            DataTable dtMappedLedgers = gcCostCentre.DataSource as DataTable;
            DataTable dtAvailableLedgers = gcAvailableCostCentre.DataSource as DataTable;
            DataTable dtChkdCosrCentre = new DataTable();
            DataTable dtunmapCcentre = dtAvailableLedgers.Clone();
            string CheckedItemID = string.Empty;
            if (dtAvailableLedgers != null)
            {
                int[] SelRow = gvAvailableCostCentre.GetSelectedRows();
                if (SelRow.Count() > 0)
                {
                    //Getting all the checked Cost Centre Ids to check if any tranaction is already done.
                    DataRow drSelecRow;
                    string LastCCId = "";
                    foreach (int item in SelRow)
                    {
                        drSelecRow = gvAvailableCostCentre.GetDataRow(item);
                        //On 22/11/2022, to define Ledger CC Mode
                        if (AppSetting.CostCeterMapping == 1) drSelecRow["TRANS_MODE"] = CCMappLedgerOpTransMode;

                        if (drSelecRow != null)
                        {
                            LastCCId = drSelecRow[dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString();
                            if (!CheckedItemID.Contains(LastCCId))
                            {
                                dtunmapCcentre.ImportRow(drSelecRow);
                                CheckedItemID += drSelecRow[dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName].ToString() + ',';
                            }
                        }
                    }
                    if (dtunmapCcentre != null)
                    {
                        dtMappedLedgers.Merge(dtunmapCcentre);
                    }
                    gcCostCentre.DataSource = MakeSelectColumnZero(dtMappedLedgers);
                    gvCostCentre.FocusedColumn = gvCostCentre.VisibleColumns[3];
                    gvCostCentre.ShowEditor();

                    if (dtAvailableLedgers != null)
                    {
                        DataView dvmpp = dtAvailableLedgers.AsDataView();
                        gcAvailableCostCentre.DataSource = null;
                        if (dvmpp != null)
                        {
                            dvmpp.RowFilter = dataManager.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName + " NOT IN (" + CheckedItemID.Trim(',') + ")";
                            if (dvmpp != null && dvmpp.Table.Rows.Count > 0)
                            {
                                dtChkdCosrCentre.Merge(dvmpp.ToTable());
                            }
                        }
                        gcAvailableCostCentre.DataSource = dtChkdCosrCentre;

                    }

                }
            }
        }

        private void LoadDefaultValues()
        {
            if (ProjectId.Equals(0) && LedgerId.Equals(0) && CostCenterId.Equals(0))
            {
                lblOpeningBalanceLedger.Text = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false).ToString("d");
                LoadProject(grdlProjectName);
                GetGridViewFocus(gvLedgerDetail, LEDGER_FOCUS_COL_INDEX, true);
                chkUnMapSelectAllLedgers.Visible = true;
            }
            else if (!ProjectId.Equals(0))
            {
                lblOpeningBalanceLedger.Text = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false).ToString("d");
                LoadProject(grdlProjectName);
                GetGridViewFocus(gvLedgerDetail, LEDGER_FOCUS_COL_INDEX, true);
                grdlProjectName.EditValue = ProjectId.ToString();
                grdlProjectName.Properties.ReadOnly = true;
            }
            else if (!LedgerId.Equals(0))
            {
                grdlederName.EditValue = LedgerId.ToString();
                grdlederName.Properties.ReadOnly = true;
            }
            else
            {
                LoadProject(glkpCCProject);
            }
        }

        /// <summary>
        /// On 05/09/2024, To load currency details
        /// </summary>
        private void LoadCurrencyCountry()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryCurrencyExchangeRateByFY();
                    
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        string selectioncondition = "IIF(" + countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " > 0, 1, 0)";
                        DataTable dtCurencyCountry = resultArgs.DataSource.Table;
                        dtCurencyCountry.Columns.Add(SELECT_COL, typeof(int));
                        

                        dtCurencyCountry.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " > 0";
                        dtCurencyCountry.DefaultView.Sort = countrySystem.AppSchema.Country.COUNTRYColumn.ColumnName;
                        gcCurrencyExRate.DataSource = dtCurencyCountry.DefaultView.ToTable();

                        dtCurencyCountry.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " = 0";
                        dtCurencyCountry.DefaultView.Sort = countrySystem.AppSchema.Country.COUNTRYColumn.ColumnName;
                        gcCurrencyAvailableExRate.DataSource = dtCurencyCountry.DefaultView.ToTable();

                        lblCurrencyCount.Text = "# " + gvCurrencyExRate.RowCount.ToString();
                        lblAvailalbeCurrencyCount.Text = "# " + gvCurrencyAvailableExRate.RowCount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void GetTransactionFD()
        {
            using (MappingSystem TransFD = new MappingSystem())
            {
                resultArgs = TransFD.FixedDeposit();
                if (resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    dtTransactionFD = resultArgs.DataSource.Table;
            }
        }

        private DataTable SelectAll(DataTable dtAllRecords, CheckEdit ctrlChekBox)
        {

            if (dtAllRecords != null && dtAllRecords.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAllRecords.Rows)
                {
                    if (dtAllRecords.Columns.Contains(dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName))
                    {
                        Int32 accessFlag = dr != null ? UtilityMember.NumberSet.ToInteger(dr["ACCESS_FLAG"].ToString()) : 0;
                        Int32 lid = UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString());
                        //if (UtilityMember.NumberSet.ToInteger(dr[dataManager.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) != 1)
                        if (accessFlag == (int)AccessFlag.Accessable && lid != 1 && lid != 2) //For dont allow to unmap Deafult ledgers/cash/FD
                        {
                            dr[SELECT_COL] = ctrlChekBox.Checked;
                        }
                    }
                    else
                    {
                        dr[SELECT_COL] = ctrlChekBox.Checked;
                    }
                }
            }
            return dtAllRecords;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 0)
            //{
            //    chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
            //}
            if (KeyData == (Keys.Control | Keys.H) && xtcProject.SelectedTabPageIndex == 0)
            {
                chkAvailableFilter.Checked = (chkAvailableFilter.Checked) ? false : true;
            }
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 1)
            //{
            //    chkShowFilterLedger.Checked = (chkShowFilterLedger.Checked) ? false : true;
            //}
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 2)
            //{
            //    chkCostCentreFilter.Checked = (chkCostCentreFilter.Checked) ? false : true;
            //}
            //if (KeyData == (Keys.Control | Keys.W) && xtcProject.SelectedTabPageIndex == 3)
            //{
            //    chkShowFilterDonor.Checked = (chkShowFilterDonor.Checked) ? false : true;
            //}
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void SetCheckBoxSelection(CheckedListBoxControl chkListBox, string Caption, bool IsCheckAll)
        {
            if (IsCheckAll)
                chkListBox.CheckAll();
            else
                chkListBox.UnCheckAll();
            chkListBox.Text = Caption;
        }
        #endregion

        #region User Rights
        private void ApplyRights()
        {
            DataTable dtAccountMapping = new DataTable();
            try
            {
                dtAccountMapping = CommonMethod.ApplyUserRightsForForms((int)Menus.AccountMapping);
                if (dtAccountMapping != null && dtAccountMapping.Rows.Count != 0)
                {
                    foreach (DataRow dr in dtAccountMapping.Rows)
                    {
                        if ((int)AccountMapping.MapLedger == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            xtcProject.TabPages[0].PageVisible = true;
                        }
                        else if ((int)AccountMapping.MapProject == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            xtcProject.TabPages[1].PageVisible = true;
                        }
                        else if ((int)AccountMapping.MapCostCentre == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            xtcProject.TabPages[2].PageVisible = true;
                        }
                        else if ((int)AccountMapping.MapDonor == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            xtcProject.TabPages[3].PageVisible = true;
                        }
                        else if ((int)AccountMapping.MapVouchers == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            xtcProject.TabPages[4].PageVisible = true;
                        }
                        else if ((int)AccountMapping.MapCostCentreCategory == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            xtcProject.TabPages[5].PageVisible = true;
                        }
                        else if ((int)AccountMapping.MapFCPurpose == this.UtilityMember.NumberSet.ToInteger(dr["ACTIVITY_ID"].ToString()))
                        {
                            xtcProject.TabPages[6].PageVisible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }
        #endregion

        private void gvAvailableLedgerDetails_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailableLedgerRecordCount.Text = gvAvailableLedgerDetails.RowCount.ToString();
        }

        private void gvProjectDetails_RowCountChanged(object sender, EventArgs e)
        {
            lblMapProjectRecordCount.Text = gvProjectDetails.RowCount.ToString();
        }


        private void gvAvailableCostCentre_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailableCostCentreRecordCount.Text = gcAvailableCostCentre.DataSource != null ? ((DataTable)gcAvailableCostCentre.DataSource).Rows.Count.ToString() : "0";
        }
        private void gvDonor_RowCountChanged(object sender, EventArgs e)
        {
            lblMapDonorRecordCount.Text = gvDonor.RowCount.ToString();
        }
        private void gvCCCostCentre_RowCountChanged(object sender, EventArgs e)
        {
            lblMapcostCategoryRecordCount.Text = gvCCCostCentre.RowCount.ToString();
        }

        /// <summary>
        /// This is to get Overall Count the List Boxes 
        /// </summary>
        /// <param name="clb"></param>
        /// <returns></returns>
        private string GetCountListBox(CheckedListBoxControl clb)
        {
            return clb.ItemCount.ToString();
        }

        /// <summary>
        /// To get Purpose Count details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurpose_RowCountChanged(object sender, EventArgs e)
        {
            lblMapPurposeRecordCount.Text = gvPurpose.RowCount.ToString();
        }

        /// <summary>
        /// to get Purpose checked Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPurpose_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPurpose.Checked)
                {
                    SetCheckBoxSelection(chkAvailablePurpose, this.GetMessage(MessageCatalog.Master.Mapping.UNSELECT_ALL), true);
                }
                else
                {
                    SetCheckBoxSelection(chkAvailablePurpose, this.GetMessage(MessageCatalog.Master.Mapping.SELECT_ALL), false);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void chkAvailablePurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkPurpose.CheckedChanged -= new System.EventHandler(this.chkPurpose_CheckedChanged);
            chkPurpose.Checked = false;
            this.chkPurpose.CheckedChanged += new System.EventHandler(this.chkPurpose_CheckedChanged);
        }

        private void btnMouIn_Click(object sender, EventArgs e)
        {
            MoveInItems(gcpurpose, chkAvailablePurpose, dataManager.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName, dataManager.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName);
            chkPurpose.Checked = false;
            lblAvailablePurposeRecordCount.Text = GetCountListBox(chkAvailablePurpose);
        }

        private void btnMouOut_Click(object sender, EventArgs e)
        {
            MoveOutItems(gcpurpose, chkAvailablePurpose, dataManager.AppSchema.Purposes.CONTRIBUTION_IDColumn.ColumnName, dataManager.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName, glkpPurposeProject, false, false, true);
            lblAvailablePurposeRecordCount.Text = GetCountListBox(chkAvailablePurpose);
        }

        /// <summary>
        /// To check Indidual row in the Purpose Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurpose_RowClick(object sender, RowClickEventArgs e)
        {
            DataTable dtPurpose = (DataTable)gcpurpose.DataSource;
            if (dtPurpose != null && dtPurpose.Rows.Count > 0)
            {
                if (gvPurpose.GetFocusedRowCellValue(gvColSelectPurpose) != null)
                {
                    if (gvPurpose.FocusedColumn == gvColSelectPurpose)
                    {
                        int select = gvPurpose.GetFocusedRowCellValue(gvColSelectPurpose) != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(gvPurpose.GetFocusedRowCellValue(gvColSelectPurpose).ToString()) : 0;
                        gvPurpose.SetFocusedRowCellValue(gvColSelectPurpose, select == 0 ? 1 : 0);
                    }
                }
            }
        }

        /// <summary>
        /// To Edit the Purpose Coloum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPurpose_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (e.Column.FieldName == SELECT_COL)
                {
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextEdit = new RepositoryItemTextEdit();
                    e.RepositoryItem = repositoryTextEdit;
                }
            }
        }

        /// <summary>
        /// This is to Check the Purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilterPurpose_CheckedChanged(object sender, EventArgs e)
        {
            gvPurpose.OptionsView.ShowAutoFilterRow = chkShowFilterPurpose.Checked;
            if (chkShowFilterPurpose.Checked)
            {
                this.SetFocusRowFilter(gvPurpose, colPurpose);
            }
        }

        /// <summary>
        /// This is to Check the Purpose details All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUnMapPurpose_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnMapPurpose = SelectAll(((DataTable)gcpurpose.DataSource), chkUnMapPurpose);
            if (dtUnMapPurpose != null)
            {
                gcpurpose.DataSource = dtUnMapPurpose;
            }
        }

        /// <summary>
        /// To Select the Cost Category costCentre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCCSelectedCostcentres_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnMapCostCenter = SelectAll(((DataTable)gcCCCostCentre.DataSource), chkCCSelectedCostcentres);
            if (dtUnMapCostCenter != null)
            {
                gcCCCostCentre.DataSource = dtUnMapCostCenter;
            }
        }

        /// <summary>
        /// This is to select the Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpPurposeProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadAllPurpose();
            FetchMappedPurpose();
            lblAvailablePurposeRecordCount.Text = GetCountListBox(chkAvailablePurpose);
        }

        /// <summary>
        /// This to Uncheck the Purpose Select All Check Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckEdit3_CheckedChanged(object sender, EventArgs e)
        {
            this.chkUnMapPurpose.CheckedChanged -= new System.EventHandler(this.chkUnMapPurpose_CheckedChanged);
            chkUnMapPurpose.Checked = false;
            this.chkUnMapPurpose.CheckedChanged += new System.EventHandler(this.chkUnMapPurpose_CheckedChanged);
        }

        private void gvCostCentre_RowCountChanged(object sender, EventArgs e)
        {
            lblMapCostCentreRecordCount.Text = gcCostCentre.DataSource != null ? ((DataTable)gcCostCentre.DataSource).Rows.Count.ToString() : "0";
        }

        private void chkShowAllLedger_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowAllLedger.Checked)
            {
                //chkShowAllLedger.Text = "<color='Blue'><b> Show All Ledgers </b></color>";
                chkShowAllLedger.Text = this.GetMessage(MessageCatalog.Master.Mapping.SHOWALL_LEDGERS_BLUE);
                LoadLedgerDetails();
                FetchMappedLedgers();
            }
            else
            {
                //chkShowAllLedger.Text = "<color='DarkGreen'><b> Show All Ledgers</b></color>";
                chkShowAllLedger.Text = this.GetMessage(MessageCatalog.Master.Mapping.SHOWALL_LEDGERS_DARKGREEN);
                LoadLedgerDetails();
                FetchMappedLedgers();
            }
        }

        private void glkpBudgetProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadBudgetLedgerDetails();
            FetchMappedLedgersBudget();
            gvABudLedger.ExpandAllGroups();
        }

        private void glkpGenLedger_EditValueChanged(object sender, EventArgs e)
        {
            LoadLedgerAllforGeneralateMap();
            FetchMappedGeneralateLedger();
        }

        private void chkShowFilterGenSelected_CheckedChanged(object sender, EventArgs e)
        {
            gvGenSelected.OptionsView.ShowAutoFilterRow = chkGenShowFilterSelected.Checked;
            if (chkGenShowFilterSelected.Checked)
            {
                this.SetFocusRowFilter(gvGenSelected, gccolGenLedgerName);
            }
        }

        private void chkGenAvaila_CheckedChanged(object sender, EventArgs e)
        {
            gvGenAvailable.OptionsView.ShowAutoFilterRow = chkGenAvailaFilter.Checked;
            if (chkGenAvailaFilter.Checked)
            {
                this.SetFocusRowFilter(gvGenAvailable, gccolGenAvailLedger);
            }
        }

        /// <summary>
        /// Available ledger can be moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveInAvaiGenToSelectedGeneralate_Click(object sender, EventArgs e)
        {
            DataTable dtMappedLedgers = gcGenSelected.DataSource as DataTable;
            DataTable dtAvailableLedgers = gcGenAvailable.DataSource as DataTable;
            if (dtAvailableLedgers != null)
            {
                //To get all the checked items from Available ledgers
                var CheckedLedgers = (from ledgers in dtAvailableLedgers.AsEnumerable()
                                      where ((ledgers.Field<Int32?>(SELECT_COL) == 1))
                                      select ledgers);
                //To get all the Unchecked item of Available ledgers
                var UnCheckedLedgers = (from ledgers in dtAvailableLedgers.AsEnumerable()
                                        where ((ledgers.Field<Int32?>(SELECT_COL) != 1))
                                        select ledgers);
                if (UnCheckedLedgers.Count() > 0)
                    dtAvailableLedgers = UnCheckedLedgers.CopyToDataTable();
                else
                    dtAvailableLedgers = (gcGenAvailable.DataSource as DataTable).Clone();
                if (CheckedLedgers.Count() > 0)
                {
                    if (dtMappedLedgers != null)
                        dtMappedLedgers.Merge(CheckedLedgers.CopyToDataTable());
                    else
                        dtMappedLedgers = CheckedLedgers.CopyToDataTable();
                }
                else
                {
                    if (dtMappedLedgers == null)
                        dtMappedLedgers = (gcGenSelected.DataSource as DataTable).Clone();
                }

                gcGenSelected.DataSource = MakeSelectColumnZero(dtMappedLedgers);
                gcGenAvailable.DataSource = dtAvailableLedgers;
            }
        }

        /// <summary>
        /// Move out from Selected to Available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveOutSeleGenToAvailableGen_Click(object sender, EventArgs e)
        {
            try
            {
                string CheckedItemID = string.Empty;
                DataTable dtMappedItems = gcGenSelected.DataSource as DataTable;
                DataTable dtUnMappedItems = gcGenAvailable.DataSource as DataTable;
                if (dtMappedItems != null)
                {
                    var CheckedItems = (from Checkeditems in dtMappedItems.AsEnumerable()
                                        where (Checkeditems.Field<Int32?>(SELECT_COL) == 1)
                                        select Checkeditems);

                    var UnCheckedItems = (from Uncheckeditems in dtMappedItems.AsEnumerable()
                                          where (Uncheckeditems.Field<Int32?>(SELECT_COL) != 1)
                                          select Uncheckeditems);

                    if (CheckedItems.Count() > 0)
                    {
                        dtCheckedItems = CheckedItems.CopyToDataTable();

                        if (dtUnMappedItems != null)
                            dtUnMappedItems.Merge(dtCheckedItems);
                        else
                            dtUnMappedItems = dtCheckedItems;

                        if (UnCheckedItems.Count() > 0)
                            dtMappedItems = UnCheckedItems.CopyToDataTable();
                        else
                            dtMappedItems = dtMappedItems.Clone();
                    }
                }

                if (dtMappedItems != null)
                {
                    gcGenSelected.DataSource = dtMappedItems;
                    //04.01.2020
                    //DataView dv = new DataView(dtUnMappedItems) { Sort = "SORT_ID" };
                    DataView dv = new DataView(dtUnMappedItems);
                    gcGenAvailable.DataSource = MakeSelectColumnZero(dv.ToTable());
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Selected 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUnSelectGenLedgers_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnMapGeneralateLedger = SelectAll(((DataTable)gcGenSelected.DataSource), chkUnSelectGenLedgers);
            if (dtUnMapGeneralateLedger != null)
            {
                gcGenSelected.DataSource = dtUnMapGeneralateLedger;
            }
        }

        /// <summary>
        /// Available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAvailableGenLedger_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtAllLedger = (DataTable)gcGenAvailable.DataSource;
            if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAllLedger.Rows)
                {
                    dr[SELECT_COL] = chkAvailableGenLedger.Checked;
                }
                gcGenAvailable.DataSource = dtAllLedger;
            }
        }

        private void gvGenAvailable_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailGenCount.Text = gvGenAvailable.RowCount.ToString();
        }

        private void gvGenSelected_RowCountChanged(object sender, EventArgs e)
        {
            lblSelectedGeneralateCount.Text = gvGenSelected.RowCount.ToString();
        }

        private void btnSaveGeneralate_Click(object sender, EventArgs e)
        {
            if (gcGenSelected.DataSource != null)
            {
                GeneralateHOLedgerMapping();
            }
            else
            {
                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
            }
        }

        private void btnGenClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Move In Budget Ledger 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveInBudgetLedger_Click(object sender, EventArgs e)
        {
            MoveInBudgetLedger();
            gvSBudLedger.ExpandAllGroups();
            GetGridViewFocus(gvSBudLedger, 4, true);
            chkLedgerSelectAll.Checked = chkLedgerSelectAll.Visible = false;
            //  lblCostCategoryMapRecordCount.Text = GetCountListBox(chkAvailableCategoryCostCenter);
        }

        /// <summary>
        /// Move Out Budget Ledger 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveoutBudgetLedger_Click(object sender, EventArgs e)
        {
            MoveOutBudgetLedger();
            gvSBudLedger.ExpandAllGroups();
            chkLedgerSelectAll.Checked = chkLedgerSelectAll.Visible = false;
            // lblCostCategoryMapRecordCount.Text = GetCountListBox(chkAvailableCategoryCostCenter);
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkunMapSelectedBudgetLedger_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtUnMapBudgetLedger = SelectAll(((DataTable)gcSBudLedger.DataSource), chkunMapSelectedBudgetLedger);
            if (dtUnMapBudgetLedger != null)
            {
                gcSBudLedger.DataSource = dtUnMapBudgetLedger;
            }
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAllAvailableBudgetLedger_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtAllLedger = (DataTable)gcABudLedger.DataSource;
            if (dtAllLedger != null && dtAllLedger.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAllLedger.Rows)
                {
                    dr[SELECT_COL] = chkAllAvailableBudgetLedger.Checked;
                }
                gcABudLedger.DataSource = dtAllLedger;
            }
        }



        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSBShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvSBudLedger.OptionsView.ShowAutoFilterRow = chkSBShowFilter.Checked;
            if (chkSBShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvSBudLedger, gvcolSBudLedgerName);
            }
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkABShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvABudLedger.OptionsView.ShowAutoFilterRow = chkABShowFilter.Checked;
            if (chkABShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvABudLedger, gvcolABudLedgerName);
            }
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBudLedgerSave_Click(object sender, EventArgs e)
        {
            if (gcSBudLedger.DataSource != null)
            {
                BudgetLedgerProjectMapping();
            }
            else
            {
                ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
            }
        }

        /// <summary>
        /// 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBudLedgerClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Row Count 04.01.2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvABudLedger_RowCountChanged(object sender, EventArgs e)
        {
            lblAvailableBudgetLedgerCount.Text = gvABudLedger.RowCount.ToString();
        }

        private void btnResetLedgerOpBalance_Click(object sender, EventArgs e)
        {
            //On 08/05/2020, To rest ledger opening balance
            int projectid = grdlProjectName.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());

            string projectname = grdlProjectName.Text;
            frmResetLedgerOPBalance frmresetopbalance = new frmResetLedgerOPBalance(projectid);
            frmresetopbalance.ShowDialog();

            //refresh current opening balance grid
            if (frmresetopbalance.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                grdlProjectName_EditValueChanged(sender, e);
            }
        }

        private void gcpurpose_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //On 23/11/2022, If CC mapping based on Project-wise, FC distribute option is forced to Cost
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && (!e.Shift && !e.Alt && !e.Control) &&
                (gvPurpose.FocusedColumn == colPurposeOpeningBalance))
            {
                gvPurpose.PostEditor();
                gvPurpose.UpdateCurrentRow();

                //On 05/07/2022, To get distributed purpose amount
                int projectid = UtilityMember.NumberSet.ToInteger(glkpPurposeProject.EditValue.ToString());
                int pid = gvPurpose.GetFocusedRowCellValue(colPurposeId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurpose.GetFocusedRowCellValue(colPurposeId).ToString()) : 0;
                string pname = gvPurpose.GetFocusedRowCellValue(colPurpose) != null ? gvPurpose.GetFocusedRowCellValue(colPurpose).ToString() : string.Empty;
                double pamt = gvPurpose.GetFocusedRowCellValue(colPurposeOpeningBalance) != null ? this.UtilityMember.NumberSet.ToDouble(gvPurpose.GetFocusedRowCellValue(colPurposeOpeningBalance).ToString()) : 0;
                string transmode = gvPurpose.GetFocusedRowCellValue(colTransMode) != null ? gvPurpose.GetFocusedRowCellValue(colTransMode).ToString() : TransSource.Dr.ToString();
                frmProjectPurposeLedgerDistribution frmPurposeDistribution = new frmProjectPurposeLedgerDistribution(projectid, pid, pamt, pname, dtAllProjectPurposeDistributed, transmode);
                DialogResult dialogresult = frmPurposeDistribution.ShowDialog();
                if (dialogresult == System.Windows.Forms.DialogResult.OK)
                {
                    if (frmPurposeDistribution.ReturnValue != null)
                    {
                        dtAllProjectPurposeDistributed = frmPurposeDistribution.ReturnValue as DataTable;
                    }
                }
            }
        }

        private void glkpCCProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadCCLedgerByProject();
            BindCosteCenterCheckedListBox();
            FetchMappedCostCenter();
            //gvCostCentre.ExpandAllGroups();
            //gvAvailableCostCentre.ExpandAllGroups();
        }

        private void glkpCCLedger_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AppSetting.CostCeterMapping == 1)
            {

                lblCCLedgerOpBalance.Text = "0.00 Dr";
                BindCosteCenterCheckedListBox();
                FetchMappedCostCenter();
            }
        }

        private void gcCostCentre_Click(object sender, EventArgs e)
        {

        }

        private void gvLedger_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //On 18/11/2022, To show CC distribution based on Ledger based on the settings
            if (this.AppSetting.CostCeterMapping == 1)
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && (!e.Shift && !e.Alt && !e.Control) &&
                    (gvLedgerDetail.FocusedColumn == colOPBalance))
                {
                    gvLedgerDetail.PostEditor();
                    gvLedgerDetail.UpdateCurrentRow();

                    int isCCLedger = gvLedgerDetail.GetFocusedRowCellValue(colMappedIsCostCentre) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerDetail.GetFocusedRowCellValue(colMappedIsCostCentre).ToString()) : 0;
                    if (isCCLedger == 1)
                    {
                        int projectid = UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                        int lid = gvLedgerDetail.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedgerDetail.GetFocusedRowCellValue(colLedgerId).ToString()) : 0;
                        string lname = gvLedgerDetail.GetFocusedRowCellValue(colLedger) != null ? gvLedgerDetail.GetFocusedRowCellValue(colLedger).ToString() : string.Empty;
                        double lopamt = gvLedgerDetail.GetFocusedRowCellValue(colOPBalance) != null ? this.UtilityMember.NumberSet.ToDouble(gvLedgerDetail.GetFocusedRowCellValue(colOPBalance).ToString()) : 0;
                        string ltransmode = gvLedgerDetail.GetFocusedRowCellValue(gvColLedgerType) != null ? gvLedgerDetail.GetFocusedRowCellValue(gvColLedgerType).ToString() : TransSource.Dr.ToString();
                        frmProjectLedgerOPBalanceCCDistribution frmCCDistribution = new frmProjectLedgerOPBalanceCCDistribution(projectid, lid, lopamt, lname, dtAllProjectLedgerCCDistributed, ltransmode);
                        DialogResult dialogresult = frmCCDistribution.ShowDialog();
                        if (dialogresult == System.Windows.Forms.DialogResult.OK)
                        {
                            if (frmCCDistribution.ReturnValue != null)
                            {
                                dtAllProjectLedgerCCDistributed = frmCCDistribution.ReturnValue as DataTable;
                            }
                        }
                    }
                }
            }
        }

        private void btnBulkCCMap_Click(object sender, EventArgs e)
        {
            frmMapBulkCostcentre frmbulkCCMapp = new frmMapBulkCostcentre();
            frmbulkCCMapp.ShowDialog();

            glkpCCProject_EditValueChanged(null, null);
        }

        private void gvCostCentre_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            CCTotalDistributedAmount = 0;
            if (gcCostCentre.DataSource != null)
            {
                DataTable dt = gcCostCentre.DataSource as DataTable;
                double ccSumDRAmt = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(AMOUNT)", "TRANS_MODE='DR'").ToString());
                double ccSumCRAmt = UtilityMember.NumberSet.ToDouble(dt.Compute("SUM(AMOUNT)", "TRANS_MODE='CR'").ToString());
                CCTotalDistributedAmount = (ccSumDRAmt - ccSumCRAmt);
            }
            e.TotalValue = UtilityMember.NumberSet.ToNumber(Math.Abs(CCTotalDistributedAmount)) + " " + (CCTotalDistributedAmount >= 0 ? "DR" : "CR");
            CCTotalDistributedAmount = Math.Abs(CCTotalDistributedAmount);
        }

        private void gvCostCentre_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void gvCostCentre_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //gvCostCentre.UnselectRow(gvCostCentre.FocusedRowHandle);
            /*if (gvCostCentre.FocusedColumn.Name != "DX$CheckboxSelectorColumn")
            {
                var view = (GridView)sender;
                view.BeginSelection();
                view.ClearSelection();
                view.SelectRow(gridView1.FocusedRowHandle);
                view.EndSelection();
            }*/
        }

        private void gvCostCentre_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName != "DX$CheckboxSelectorColumn")
            //{
            //    CheckEditViewInfo info = (e.Cell as GridCellInfo).ViewInfo as CheckEditViewInfo;  
            //    info.EditValue = false;
            //}  
        }

        private void rbnProjectLedgerApplicableDate_Click(object sender, EventArgs e)
        {
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                if (gvLedgerDetail.GetRowCellValue(gvLedgerDetail.FocusedRowHandle, colLedgerId) != null)
                {
                    DataRow drLedger = gvLedgerDetail.GetDataRow(gvLedgerDetail.FocusedRowHandle);
                    int accessFlag = drLedger != null ? UtilityMember.NumberSet.ToInteger(drLedger["ACCESS_FLAG"].ToString()) : 0;
                    int Lid = drLedger != null ? UtilityMember.NumberSet.ToInteger(drLedger[ledgersystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                    int Pid = grdlProjectName.EditValue == null ? 0 : UtilityMember.NumberSet.ToInteger(grdlProjectName.EditValue.ToString());
                    string PName = grdlProjectName.EditValue == null ? string.Empty : grdlProjectName.EditValue.ToString();
                    string LName = drLedger["LEDGER_NAME"].ToString();

                    if (accessFlag != (int)AccessFlag.Readonly)
                    {
                        frmProjectLedgerApplicableDate frmProjectLedgerapplicabledate = new frmProjectLedgerApplicableDate(MapForm.Ledger, Lid, Pid,
                                PName, LName, dtAllProjectLedgerApplicable, string.Empty, string.Empty);
                        DialogResult dialogresult = frmProjectLedgerapplicabledate.ShowDialog();

                        if (dialogresult == System.Windows.Forms.DialogResult.OK)
                        {
                            if (frmProjectLedgerapplicabledate.ReturnValue != null)
                            {
                                dtAllProjectLedgerApplicable = frmProjectLedgerapplicabledate.ReturnValue as DataTable;
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox("Default Ledger(s) can't be modified");
                    }
                }
            }
        }

        private void chkShowfilterCurrency_CheckedChanged(object sender, EventArgs e)
        {
            gvCurrencyExRate.OptionsView.ShowAutoFilterRow = chkShowfilterCurrency.Checked;
            if (chkShowfilterCurrency.Checked)
            {
                this.SetFocusRowFilter(gvCurrencyExRate, colCurrency);
            }
        }

        private void chkShowfilterAvailableCurrency_CheckedChanged(object sender, EventArgs e)
        {
            gvCurrencyAvailableExRate.OptionsView.ShowAutoFilterRow = chkShowfilterAvailableCurrency.Checked;
            if (chkShowfilterAvailableCurrency.Checked)
            {
                this.SetFocusRowFilter(gvCurrencyAvailableExRate, colAvailableCurrency);
            }
        }

        private void btnCurrencySave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gcCurrencyExRate.DataSource != null)
                {
                    DataTable dtDefinedCurrencyExchangeRate = gcCurrencyExRate.DataSource as DataTable;
                    if (dtDefinedCurrencyExchangeRate != null && dtDefinedCurrencyExchangeRate.Rows.Count > 0)
                    {
                        DataTable dt = dtDefinedCurrencyExchangeRate.DefaultView.ToTable();
                        dt.DefaultView.RowFilter = "EXCHANGE_RATE=0";
                        if (dt.DefaultView.Count == 0)
                        {
                            /*DialogResult msgDialog = System.Windows.Forms.DialogResult.Yes;
                            if (UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) == this.AppSetting.FirstFYDateFrom)
                            {
                                msgDialog = this.ShowConfirmationMessage("Since this is first Finance Year, Are you sure to update all " +
                                        "Cash and Bank Opening Balance too ?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                            }*/
                            //if (msgDialog == System.Windows.Forms.DialogResult.Yes)
                            //{
                                using (MappingSystem mappingsystem = new MappingSystem())
                                {
                                    this.ShowWaitDialog("Updating Exchange Rate");
                                    ResultArgs result = mappingsystem.UpdateCurrentFYExchangeRate(dtDefinedCurrencyExchangeRate);
                                    this.CloseWaitDialog();
                                    if (result.Success)
                                    {
                                        LoadCurrencyCountry();
                                        ShowSuccessMessage("Exchange Rate Updated");
                                    }
                                    else
                                    {
                                        MessageRender.ShowMessage(result.Message);
                                    }
                                }
                                MappingDialogResultPurpose = DialogResult.OK;
                            //}
                        }
                        else
                        {
                            this.ShowMessageBoxWarning("All Mapped Currency should have Exchange Rate.");
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.NO_RECORD));
                }
            }
            catch (Exception err)
            {
                this.CloseWaitDialog();
                this.ShowMessageBox(err.Message);
            }
        }

        private void btnCurrencyClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResultCurrency;
            this.Close();
        }

        private void gcCurrencyExRate_Click(object sender, EventArgs e)
        {

        }

        private void btnCurrencyMoveIn_Click(object sender, EventArgs e)
        {
            DataTable dtMappedCurrency = gcCurrencyExRate.DataSource as DataTable;
            DataTable dtAvailableCurrency = gcCurrencyAvailableExRate.DataSource as DataTable;
            if (dtAvailableCurrency != null)
            {
                var CheckedLedgers = (from ledgers in dtAvailableCurrency.AsEnumerable()
                                      where ((ledgers.Field<Int32?>(SELECT_COL) == 1))
                                      select ledgers);
                
                var UnCheckedLedgers = (from ledgers in dtAvailableCurrency.AsEnumerable()
                                        where ((ledgers.Field<Int32?>(SELECT_COL) != 1))
                                        select ledgers);
                if (UnCheckedLedgers.Count() > 0)
                    dtAvailableCurrency = UnCheckedLedgers.CopyToDataTable();
                else
                    dtAvailableCurrency = (gvAvailableLedger.DataSource as DataTable).Clone();

                if (CheckedLedgers.Count() > 0)
                {
                    if (dtMappedCurrency != null)
                        dtMappedCurrency.Merge(CheckedLedgers.CopyToDataTable());
                    else
                        dtMappedCurrency = CheckedLedgers.CopyToDataTable();
                }
                else
                {
                    if (dtMappedCurrency == null)
                        dtMappedCurrency = (gcCurrencyExRate.DataSource as DataTable).Clone();
                }

                gcCurrencyExRate.DataSource = MakeSelectColumnZero(dtMappedCurrency);
                gcCurrencyAvailableExRate.DataSource = dtAvailableCurrency;
            }

            lblCurrencyCount.Text = "# " + gvCurrencyExRate.RowCount.ToString();
            lblAvailalbeCurrencyCount.Text = "# " + gvCurrencyAvailableExRate.RowCount.ToString();
        }

        private void btnCurrencyMoveOut_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCantUnmap = false;
                DataTable dtMappedItems = gcCurrencyExRate.DataSource as DataTable;
                DataTable dtUnMappedItems = gcCurrencyAvailableExRate.DataSource as DataTable;
                if (dtMappedItems != null)
                {
                    using (VoucherTransactionSystem vsystem = new VoucherTransactionSystem())
                    {
                        foreach (DataRow dr in dtMappedItems.Rows)
                        {
                            Int32 countryid = UtilityMember.NumberSet.ToInteger(dr[vsystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName].ToString());
                            Int32 isselected = UtilityMember.NumberSet.ToInteger(dr[SELECT_COL].ToString());
                            if (isselected==1 && vsystem.IsVoucherMadeForCountry(countryid))
                            {
                                dr.BeginEdit();
                                dr[SELECT_COL] = 0;
                                dr.EndEdit();
                                dtUnMappedItems.AcceptChanges();
                                isCantUnmap = true;
                            }
                        }
                    }
                    
                    var CheckedItems = (from Checkeditems in dtMappedItems.AsEnumerable()
                                        where (Checkeditems.Field<Int32?>(SELECT_COL) == 1)
                                        select Checkeditems);

                    var UnCheckedItems = (from Uncheckeditems in dtMappedItems.AsEnumerable()
                                          where (Uncheckeditems.Field<Int32?>(SELECT_COL) != 1)
                                          select  Uncheckeditems);

                    
                    if (CheckedItems.Count() > 0)
                    {
                        dtCheckedItems = CheckedItems.CopyToDataTable();

                        if (dtUnMappedItems != null)
                            dtUnMappedItems.Merge(dtCheckedItems);
                        else
                            dtUnMappedItems = dtCheckedItems;

                        if (UnCheckedItems.Count() > 0)
                            dtMappedItems = UnCheckedItems.CopyToDataTable();
                        else
                            dtMappedItems = dtMappedItems.Clone();
                    }

                    gcCurrencyExRate.DataSource = dtMappedItems;
                    DataView dv = new DataView(dtUnMappedItems);
                    gcCurrencyAvailableExRate.DataSource = MakeSelectColumnZero(dv.ToTable());
                }
                lblCurrencyCount.Text = "# " + gvCurrencyExRate.RowCount.ToString();
                lblAvailalbeCurrencyCount.Text = "# " + gvCurrencyAvailableExRate.RowCount.ToString();


                if (isCantUnmap)
                {
                    MessageRender.ShowMessage("As Voucher(s) are available for few currency, you can't be unmapped.");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
        }

        private void gcCurrencyAvailableExRate_Click(object sender, EventArgs e)
        {

        }


    }
}