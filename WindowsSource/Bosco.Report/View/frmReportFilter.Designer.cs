using DevExpress.XtraEditors;
namespace Bosco.Report.View
{
    partial class frmReportFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportFilter));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.rchkState = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.pnlReportTitle = new DevExpress.XtraEditors.PanelControl();
            this.lcTitle = new DevExpress.XtraLayout.LayoutControl();
            this.lcgReportTitle = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblReportTitle = new DevExpress.XtraLayout.SimpleLabelItem();
            this.pnlReportCriteria = new DevExpress.XtraEditors.PanelControl();
            this.lcReportCriteria = new DevExpress.XtraLayout.LayoutControl();
            this.btnUpdateAssetDepreciation = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.xtbLocation = new DevExpress.XtraTab.XtraTabControl();
            this.xtpCostCentre = new DevExpress.XtraTab.XtraTabPage();
            this.lcCostCentre = new DevExpress.XtraLayout.LayoutControl();
            this.glkpCCCategory = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvCCCategory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCCCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCCCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilterCostCenter = new DevExpress.XtraEditors.CheckEdit();
            this.gcCostCentre = new DevExpress.XtraGrid.GridControl();
            this.gvCostCentre = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCostCentreID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCentreCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCostCentreName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgCostCentre = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgGroupCostCentre = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblcostCentreGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgCCCategory = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCCCategory = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpDate = new DevExpress.XtraTab.XtraTabPage();
            this.lcDateCriteria = new DevExpress.XtraLayout.LayoutControl();
            this.rboInKind = new DevExpress.XtraEditors.RadioGroup();
            this.DateTo = new DevExpress.XtraEditors.DateEdit();
            this.DateFrom = new DevExpress.XtraEditors.DateEdit();
            this.gcReportCriteria = new DevExpress.XtraGrid.GridControl();
            this.gvReportCriteria = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCriteriaName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCriteriaType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkReportCriteria = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.lcgDateProperty = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgDate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptyDateFrom = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblGridControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblWithInKind = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.xtpProject = new DevExpress.XtraTab.XtraTabPage();
            this.lcProject = new DevExpress.XtraLayout.LayoutControl();
            this.glkpITRGroup = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colITRGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colITRGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkBankColumn1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvMultiColumn1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolMultiBank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolMultiLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolMultibankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkbankColumn2 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvMultiColumn2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolMultiColBank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolMulticolLedgerid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolMulticolumnbankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpBudgetCompare = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpSociety = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSocietyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSocietyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkBankFilter = new DevExpress.XtraEditors.CheckEdit();
            this.chkProjectFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcBank = new DevExpress.XtraGrid.GridControl();
            this.gvBank = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBankId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkBankCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProject = new DevExpress.XtraGrid.GridControl();
            this.gvProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkSelectProject = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpBudget = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgProject = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgProjectwithDivision = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProjectGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgMultiColumnBankChooser = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgBankAccount = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblBankGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgSociety = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcSociety = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciITRGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgBudgetCompare = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCompareBudget1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCompareBudget2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpLedger = new DevExpress.XtraTab.XtraTabPage();
            this.lcLedger = new DevExpress.XtraLayout.LayoutControl();
            this.chkLedgerGroupFilter = new DevExpress.XtraEditors.CheckEdit();
            this.chkLedgerFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcLedgerDetail = new DevExpress.XtraGrid.GridControl();
            this.gvLedgerDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkledger = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLedger = new DevExpress.XtraGrid.GridControl();
            this.gvLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colledgerGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgLedgerGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpLedgerGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblLedgerGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGrpLedger = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xrtpPartyLedger = new DevExpress.XtraTab.XtraTabPage();
            this.lcTDSParties = new DevExpress.XtraLayout.LayoutControl();
            this.chkPartyFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcTDSParties = new DevExpress.XtraGrid.GridControl();
            this.gvTDSParties = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTDSPartiesId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartyLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkPartyLedger = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTDSPartiesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgTDSParties = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lclTDSParties = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpNarration = new DevExpress.XtraTab.XtraTabPage();
            this.lcNarration = new DevExpress.XtraLayout.LayoutControl();
            this.chkNarration = new DevExpress.XtraEditors.CheckEdit();
            this.gcNarration = new DevExpress.XtraGrid.GridControl();
            this.gvNarration = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNarrationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNarrationCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkNarration = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colNarration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgNarration = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgGroupNarration = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblNarrationGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xrtabDeducteeType = new DevExpress.XtraTab.XtraTabPage();
            this.lcDeducteeType = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcDeducteeType = new DevExpress.XtraGrid.GridControl();
            this.gvDeducteeType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDeducteeTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkDeducteeType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDeducteeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgDeducteeType = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpPayroll = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpPayroll = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPayrollId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGroups = new DevExpress.XtraGrid.GridControl();
            this.gvGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayroll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkPayrollCom = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colGrpName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgPayrollGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem13 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcPayroll = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpPayrollComponent = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcPayStaff = new DevExpress.XtraGrid.GridControl();
            this.gvPayrollStaff = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStaffId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollStaff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkPayrollStaff = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colStaffName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPayComponent = new DevExpress.XtraGrid.GridControl();
            this.gvPayComponent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPayrollComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkPayroll = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgComponent = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgPayRollStaff = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpItem = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.chkStockShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcItem = new DevExpress.XtraGrid.GridControl();
            this.gvItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStockSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkStockSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgStockItem = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpLocation = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.chkLocationShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcLocation = new DevExpress.XtraGrid.GridControl();
            this.gvLocation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkLocationSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lgGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtbDynamicConditions = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.cboCondition = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem14 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem16 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem17 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpBankFDAccount = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.chkbankFDFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcBankFDAccounts = new DevExpress.XtraGrid.GridControl();
            this.gvBankFDAccounts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolaccBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolaccFDAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolAccType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolAccLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpAssetClass = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl7 = new DevExpress.XtraLayout.LayoutControl();
            this.chkAssetShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcAssetClass = new DevExpress.XtraGrid.GridControl();
            this.gvAssetClass = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAssetClassId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChkSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkAssetclass = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAssetClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgAssetClass = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtbRegistrationType = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl8 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowRegistrationFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcRegistrationType = new DevExpress.XtraGrid.GridControl();
            this.gvRegistrationType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRegistrationTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelectReg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkSelectRegisterType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colRegistrationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgRegistrationType = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem43 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtbCountry = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl9 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowStateFilter = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowCountryFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcState = new DevExpress.XtraGrid.GridControl();
            this.gvState = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStateId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStateSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkStateSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCountry = new DevExpress.XtraGrid.GridControl();
            this.gvCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountryCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkCountrySelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup15 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgCountry = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgState = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem45 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpLanguage = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl10 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowLanguageFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcLanguage = new DevExpress.XtraGrid.GridControl();
            this.gvLanguage = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckLanguage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkLanguage = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colLanguage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup16 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem18 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgLanguage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem47 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpStateDonaud = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl11 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowState = new DevExpress.XtraEditors.CheckEdit();
            this.gcStateDonaud = new DevExpress.XtraGrid.GridControl();
            this.gvStateDonaud = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStateDonauId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchStateCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colStateDonaudName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup17 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem20 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgStatName = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpDonaud = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl12 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowDonaud = new DevExpress.XtraEditors.CheckEdit();
            this.gcDonaud = new DevExpress.XtraGrid.GridControl();
            this.gvDonaud = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonaudId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonaudCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkDonaud = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDonaud = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup18 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgDonar = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem49 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem51 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpFeestDayTask = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl13 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpFeestDatTask = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTagID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTagName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup19 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblFeestDayTask = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem21 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.xtpNetworkingTasks = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl17 = new DevExpress.XtraLayout.LayoutControl();
            this.gcTasks = new DevExpress.XtraGrid.GridControl();
            this.gvTasks = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coltaskCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkSelectTask = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colTaskId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaskName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkTaskFilter = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup23 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem58 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup24 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem57 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpAnniversaryType = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl14 = new DevExpress.XtraLayout.LayoutControl();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.rgAnniversaryType = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup20 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem19 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem24 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem54 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem23 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem55 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtbBudget = new DevExpress.XtraTab.XtraTabPage();
            this.lcBudget = new DevExpress.XtraLayout.LayoutControl();
            this.gcBudgetNewProject = new DevExpress.XtraGrid.GridControl();
            this.gvBudgetNewProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcAcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetIncludeReports = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkIncludeReports = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ColBudgetNewProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtBudgetNewProject = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ColBudgetPExpenseAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtPExpenseAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColBudgetPIncomeAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtPIncomeAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColBudgetPGovtIncomeAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtGHelpAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColPProvinceHelp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rctxtPHelpAmt = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.ColBudgetPRemakrs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtBudgetNewProjectRemarks = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDeleteBudgetNewProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.chkBudgetFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcBudget = new DevExpress.XtraGrid.GridControl();
            this.gvBudget = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBudgetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetSelection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBudgetMonthRowName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetDateFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetDateTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup21 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcGrpBudgetGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGrpBudgetNewProject = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcBudgetNewProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtpReportSetup = new DevExpress.XtraTab.XtraTabPage();
            this.lcReport = new DevExpress.XtraLayout.LayoutControl();
            this.glkpReportSetupCurrencyCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvCurrencyCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrencyCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencySymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkIncludeAllBudgetLedgers = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowTOC = new DevExpress.XtraEditors.CheckEdit();
            this.cboReportType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkSocietyWithInstutionName = new DevExpress.XtraEditors.CheckEdit();
            this.cboColumnHeaderFontStyle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkShowIndividualProjects = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowZeroValues = new DevExpress.XtraEditors.CheckEdit();
            this.rgbAddress = new DevExpress.XtraEditors.RadioGroup();
            this.cboBorderStyle = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkShowProjectinFooter = new DevExpress.XtraEditors.CheckEdit();
            this.rgbReportTitle = new DevExpress.XtraEditors.RadioGroup();
            this.chkPageNumber = new DevExpress.XtraEditors.CheckEdit();
            this.chkVerticalLine = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowReportLogo = new DevExpress.XtraEditors.CheckEdit();
            this.chkDisplayTitles = new DevExpress.XtraEditors.CheckEdit();
            this.ReportDate = new DevExpress.XtraEditors.DateEdit();
            this.cboSoryByGroup = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboSortByLedger = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkGroupCode = new DevExpress.XtraEditors.CheckEdit();
            this.chkLedgerCode = new DevExpress.XtraEditors.CheckEdit();
            this.cboTitleAlignment = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkHorizontalLine = new DevExpress.XtraEditors.CheckEdit();
            this.lblShowGroupCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgReportSetup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgReportPageHeader = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblLedgerCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblHorizontalLine = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem12 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblReportDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblBorderStyle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblShowReportLogo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem59 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblColumnHeaderFontStyle = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSocietyWithInstutionName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciReportCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcShowTOC = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcIncludeAllBudgetLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcReportSetupCurrency = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtbSign = new DevExpress.XtraTab.XtraTabPage();
            this.lcSignDetails = new DevExpress.XtraLayout.LayoutControl();
            this.chkIncludeAuditorSignNote = new DevExpress.XtraEditors.CheckEdit();
            this.btnSign3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSign2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSign1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtRole3 = new DevExpress.XtraEditors.TextEdit();
            this.txtRoleName3 = new DevExpress.XtraEditors.TextEdit();
            this.txtRole2 = new DevExpress.XtraEditors.TextEdit();
            this.txtRoleName2 = new DevExpress.XtraEditors.TextEdit();
            this.txtRole1 = new DevExpress.XtraEditors.TextEdit();
            this.txtRoleName1 = new DevExpress.XtraEditors.TextEdit();
            this.chkIncludeSignDetails = new DevExpress.XtraEditors.CheckEdit();
            this.picSign1 = new DevExpress.XtraEditors.PictureEdit();
            this.picSign2 = new DevExpress.XtraEditors.PictureEdit();
            this.picSign3 = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcIncludeSignDetails = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGrpSign1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcRole = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcRoleName1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSign1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSign1Btn = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGrpSign2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcRoleName2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcRole2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSign2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSign2Btn = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGrpSign3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcRoleName3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcRole3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSign3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSign3Btn = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcIncludeAuditorSignNote = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgReportCriteria = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCriteria = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblclose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcUpdateAssetDepreciation = new DevExpress.XtraLayout.LayoutControlItem();
            this.colAttachAllLeger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDailyBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imgCollection = new DevExpress.Utils.ImageCollection();
            this.chkProject = new DevExpress.XtraEditors.CheckEdit();
            this.chkBank = new DevExpress.XtraEditors.CheckEdit();
            this.chkLedger = new DevExpress.XtraEditors.CheckEdit();
            this.chkLedgerGroup = new DevExpress.XtraEditors.CheckEdit();
            this.chkCostCentre = new DevExpress.XtraEditors.CheckEdit();
            this.chkPartyLedger = new DevExpress.XtraEditors.CheckEdit();
            this.chkNatureofPayments = new DevExpress.XtraEditors.CheckEdit();
            this.chkDeducteeType = new DevExpress.XtraEditors.CheckEdit();
            this.chkPayroll = new DevExpress.XtraEditors.CheckEdit();
            this.chkPayrollStaff = new DevExpress.XtraEditors.CheckEdit();
            this.chkPayrollComponents = new DevExpress.XtraEditors.CheckEdit();
            this.chkItems = new DevExpress.XtraEditors.CheckEdit();
            this.chkLocationSelectAll = new DevExpress.XtraEditors.CheckEdit();
            this.chkselectAllAssetclass = new DevExpress.XtraEditors.CheckEdit();
            this.chkRegistrationType = new DevExpress.XtraEditors.CheckEdit();
            this.chkCountry = new DevExpress.XtraEditors.CheckEdit();
            this.chkState = new DevExpress.XtraEditors.CheckEdit();
            this.chkLanguage = new DevExpress.XtraEditors.CheckEdit();
            this.chkStateDonaud = new DevExpress.XtraEditors.CheckEdit();
            this.chkDonaud = new DevExpress.XtraEditors.CheckEdit();
            this.chkSelectAllTask = new DevExpress.XtraEditors.CheckEdit();
            this.chkBudget = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlReportTitle)).BeginInit();
            this.pnlReportTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReportTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlReportCriteria)).BeginInit();
            this.pnlReportCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcReportCriteria)).BeginInit();
            this.lcReportCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtbLocation)).BeginInit();
            this.xtbLocation.SuspendLayout();
            this.xtpCostCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcCostCentre)).BeginInit();
            this.lcCostCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCCCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCCCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterCostCenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroupCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblcostCentreGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCCCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCCCategory)).BeginInit();
            this.xtpDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcDateCriteria)).BeginInit();
            this.lcDateCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rboInKind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcReportCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReportCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkReportCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDateProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWithInKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.xtpProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).BeginInit();
            this.lcProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpITRGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkBankColumn1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiColumn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkbankColumn2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiColumn2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBudgetCompare.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpSociety.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBankFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProjectFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkBankCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelectProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBudget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProjectwithDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProjectGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMultiColumnBankChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBankAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSociety)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSociety)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciITRGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBudgetCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompareBudget1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompareBudget2)).BeginInit();
            this.xtpLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedger)).BeginInit();
            this.lcLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerGroupFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkledger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.xrtpPartyLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcTDSParties)).BeginInit();
            this.lcTDSParties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPartyFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSParties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSParties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPartyLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTDSParties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lclTDSParties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            this.xtpNarration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcNarration)).BeginInit();
            this.lcNarration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroupNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarrationGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            this.xrtabDeducteeType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcDeducteeType)).BeginInit();
            this.lcDeducteeType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDeducteeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeducteeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkDeducteeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDeducteeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            this.xtpPayroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPayroll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPayrollCom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPayrollGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPayroll)).BeginInit();
            this.xtpPayrollComponent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPayrollStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPayroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPayRollStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            this.xtpItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkStockShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkStockSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgStockItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            this.xtpLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocationShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkLocationSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            this.xtbDynamicConditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            this.xtpBankFDAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkbankFDFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBankFDAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBankFDAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).BeginInit();
            this.xtpAssetClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).BeginInit();
            this.layoutControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAssetShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkAssetclass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAssetClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            this.xtbRegistrationType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).BeginInit();
            this.layoutControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowRegistrationFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRegistrationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRegistrationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelectRegisterType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgRegistrationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).BeginInit();
            this.xtbCountry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).BeginInit();
            this.layoutControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowStateFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowCountryFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkStateSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCountrySelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).BeginInit();
            this.xtpLanguage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).BeginInit();
            this.layoutControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowLanguageFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).BeginInit();
            this.xtpStateDonaud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).BeginInit();
            this.layoutControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStateDonaud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStateDonaud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchStateCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgStatName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            this.xtpDonaud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl12)).BeginInit();
            this.layoutControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowDonaud.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDonaud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonaud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkDonaud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDonar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).BeginInit();
            this.xtpFeestDayTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl13)).BeginInit();
            this.layoutControl13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpFeestDatTask.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFeestDayTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem21)).BeginInit();
            this.xtpNetworkingTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl17)).BeginInit();
            this.layoutControl17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelectTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTaskFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem58)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem57)).BeginInit();
            this.xtpAnniversaryType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl14)).BeginInit();
            this.layoutControl14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgAnniversaryType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem55)).BeginInit();
            this.xtbBudget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcBudget)).BeginInit();
            this.lcBudget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetNewProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetNewProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkIncludeReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPExpenseAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPIncomeAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtGHelpAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPHelpAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProjectRemarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBudgetFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpBudgetGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpBudgetNewProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBudgetNewProject)).BeginInit();
            this.xtpReportSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcReport)).BeginInit();
            this.lcReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpReportSetupCurrencyCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeAllBudgetLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowTOC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReportType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSocietyWithInstutionName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboColumnHeaderFontStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowIndividualProjects.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowZeroValues.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBorderStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowProjectinFooter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbReportTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPageNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVerticalLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowReportLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisplayTitles.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSoryByGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSortByLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGroupCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTitleAlignment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHorizontalLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShowGroupCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportSetup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportPageHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHorizontalLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReportDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBorderStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShowReportLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem59)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColumnHeaderFontStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSocietyWithInstutionName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciReportCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShowTOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcIncludeAllBudgetLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReportSetupCurrency)).BeginInit();
            this.xtbSign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcSignDetails)).BeginInit();
            this.lcSignDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeAuditorSignNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeSignDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSign1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSign2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSign3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcIncludeSignDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpSign1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRoleName1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign1Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpSign2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRoleName2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRole2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign2Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpSign3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRoleName3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRole3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign3Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcIncludeAuditorSignNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCriteria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblclose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpdateAssetDepreciation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCostCentre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPartyLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNatureofPayments.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDeducteeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayroll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayrollStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayrollComponents.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkItems.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocationSelectAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkselectAllAssetclass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRegistrationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLanguage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStateDonaud.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDonaud.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAllTask.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBudget.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rchkState
            // 
            this.rchkState.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkState, "rchkState");
            this.rchkState.Name = "rchkState";
            this.rchkState.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkState.ValueChecked = 1;
            this.rchkState.ValueGrayed = 2;
            this.rchkState.ValueUnchecked = 0;
            // 
            // pnlReportTitle
            // 
            this.pnlReportTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlReportTitle.Controls.Add(this.lcTitle);
            resources.ApplyResources(this.pnlReportTitle, "pnlReportTitle");
            this.pnlReportTitle.Name = "pnlReportTitle";
            // 
            // lcTitle
            // 
            resources.ApplyResources(this.lcTitle, "lcTitle");
            this.lcTitle.Name = "lcTitle";
            this.lcTitle.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(480, 168, 250, 350);
            this.lcTitle.Root = this.lcgReportTitle;
            // 
            // lcgReportTitle
            // 
            resources.ApplyResources(this.lcgReportTitle, "lcgReportTitle");
            this.lcgReportTitle.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgReportTitle.GroupBordersVisible = false;
            this.lcgReportTitle.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblReportTitle});
            this.lcgReportTitle.Location = new System.Drawing.Point(0, 0);
            this.lcgReportTitle.Name = "Root";
            this.lcgReportTitle.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgReportTitle.Size = new System.Drawing.Size(512, 30);
            this.lcgReportTitle.TextVisible = false;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.AllowHotTrack = false;
            this.lblReportTitle.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblReportTitle.AppearanceItemCaption.Font")));
            this.lblReportTitle.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblReportTitle, "lblReportTitle");
            this.lblReportTitle.Location = new System.Drawing.Point(0, 0);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0);
            this.lblReportTitle.Size = new System.Drawing.Size(512, 30);
            this.lblReportTitle.TextSize = new System.Drawing.Size(158, 19);
            // 
            // pnlReportCriteria
            // 
            this.pnlReportCriteria.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlReportCriteria.Controls.Add(this.lcReportCriteria);
            resources.ApplyResources(this.pnlReportCriteria, "pnlReportCriteria");
            this.pnlReportCriteria.Name = "pnlReportCriteria";
            // 
            // lcReportCriteria
            // 
            this.lcReportCriteria.Controls.Add(this.btnUpdateAssetDepreciation);
            this.lcReportCriteria.Controls.Add(this.btnOk);
            this.lcReportCriteria.Controls.Add(this.btnClose);
            this.lcReportCriteria.Controls.Add(this.xtbLocation);
            resources.ApplyResources(this.lcReportCriteria, "lcReportCriteria");
            this.lcReportCriteria.Name = "lcReportCriteria";
            this.lcReportCriteria.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(692, 180, 250, 350);
            this.lcReportCriteria.Root = this.lcgReportCriteria;
            // 
            // btnUpdateAssetDepreciation
            // 
            resources.ApplyResources(this.btnUpdateAssetDepreciation, "btnUpdateAssetDepreciation");
            this.btnUpdateAssetDepreciation.Name = "btnUpdateAssetDepreciation";
            this.btnUpdateAssetDepreciation.StyleController = this.lcReportCriteria;
            this.btnUpdateAssetDepreciation.Click += new System.EventHandler(this.btnUpdateAssetDepreciation_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.StyleController = this.lcReportCriteria;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcReportCriteria;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // xtbLocation
            // 
            resources.ApplyResources(this.xtbLocation, "xtbLocation");
            this.xtbLocation.Name = "xtbLocation";
            this.xtbLocation.SelectedTabPage = this.xtpCostCentre;
            this.xtbLocation.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpDate,
            this.xtpProject,
            this.xtpLedger,
            this.xtpCostCentre,
            this.xrtpPartyLedger,
            this.xtpNarration,
            this.xrtabDeducteeType,
            this.xtpPayroll,
            this.xtpPayrollComponent,
            this.xtpItem,
            this.xtpLocation,
            this.xtbDynamicConditions,
            this.xtpBankFDAccount,
            this.xtpAssetClass,
            this.xtbRegistrationType,
            this.xtbCountry,
            this.xtpLanguage,
            this.xtpStateDonaud,
            this.xtpDonaud,
            this.xtpFeestDayTask,
            this.xtpNetworkingTasks,
            this.xtpAnniversaryType,
            this.xtbBudget,
            this.xtpReportSetup,
            this.xtbSign});
            this.xtbLocation.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtbReportCriteria_SelectedPageChanged);
            this.xtbLocation.Click += new System.EventHandler(this.xtbLocation_Click);
            // 
            // xtpCostCentre
            // 
            this.xtpCostCentre.Controls.Add(this.lcCostCentre);
            this.xtpCostCentre.Name = "xtpCostCentre";
            this.xtpCostCentre.PageVisible = false;
            resources.ApplyResources(this.xtpCostCentre, "xtpCostCentre");
            // 
            // lcCostCentre
            // 
            this.lcCostCentre.Controls.Add(this.glkpCCCategory);
            this.lcCostCentre.Controls.Add(this.chkShowFilterCostCenter);
            this.lcCostCentre.Controls.Add(this.gcCostCentre);
            resources.ApplyResources(this.lcCostCentre, "lcCostCentre");
            this.lcCostCentre.Name = "lcCostCentre";
            this.lcCostCentre.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(708, 132, 250, 350);
            this.lcCostCentre.Root = this.lcgCostCentre;
            // 
            // glkpCCCategory
            // 
            resources.ApplyResources(this.glkpCCCategory, "glkpCCCategory");
            this.glkpCCCategory.Name = "glkpCCCategory";
            this.glkpCCCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCCCategory.Properties.Buttons"))))});
            this.glkpCCCategory.Properties.PopupFormMinSize = new System.Drawing.Size(427, 0);
            this.glkpCCCategory.Properties.PopupFormSize = new System.Drawing.Size(427, 0);
            this.glkpCCCategory.Properties.View = this.gvCCCategory;
            this.glkpCCCategory.StyleController = this.lcCostCentre;
            this.glkpCCCategory.EditValueChanged += new System.EventHandler(this.glkpCCCategory_EditValueChanged);
            // 
            // gvCCCategory
            // 
            this.gvCCCategory.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCCCategory.Appearance.FocusedRow.Font")));
            this.gvCCCategory.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCCCategory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCCCategoryId,
            this.gcCCCategory});
            this.gvCCCategory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvCCCategory.Name = "gvCCCategory";
            this.gvCCCategory.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCCCategory.OptionsView.ShowColumnHeaders = false;
            this.gvCCCategory.OptionsView.ShowGroupPanel = false;
            this.gvCCCategory.OptionsView.ShowIndicator = false;
            // 
            // gcCCCategoryId
            // 
            resources.ApplyResources(this.gcCCCategoryId, "gcCCCategoryId");
            this.gcCCCategoryId.FieldName = "COST_CENTRECATEGORY_ID";
            this.gcCCCategoryId.Name = "gcCCCategoryId";
            // 
            // gcCCCategory
            // 
            resources.ApplyResources(this.gcCCCategory, "gcCCCategory");
            this.gcCCCategory.FieldName = "COST_CENTRE_CATEGORY_NAME";
            this.gcCCCategory.Name = "gcCCCategory";
            this.gcCCCategory.OptionsColumn.ShowCaption = false;
            // 
            // chkShowFilterCostCenter
            // 
            resources.ApplyResources(this.chkShowFilterCostCenter, "chkShowFilterCostCenter");
            this.chkShowFilterCostCenter.Name = "chkShowFilterCostCenter";
            this.chkShowFilterCostCenter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilterCostCenter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkShowFilterCostCenter.Properties.Caption = resources.GetString("chkShowFilterCostCenter.Properties.Caption");
            this.chkShowFilterCostCenter.StyleController = this.lcCostCentre;
            this.chkShowFilterCostCenter.CheckedChanged += new System.EventHandler(this.chkShowFilterCostCenter_CheckedChanged);
            // 
            // gcCostCentre
            // 
            resources.ApplyResources(this.gcCostCentre, "gcCostCentre");
            this.gcCostCentre.MainView = this.gvCostCentre;
            this.gcCostCentre.Name = "gcCostCentre";
            this.gcCostCentre.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkSelect});
            this.gcCostCentre.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCostCentre});
            // 
            // gvCostCentre
            // 
            this.gvCostCentre.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCentre.Appearance.HeaderPanel.Font")));
            this.gvCostCentre.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvCostCentre.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCostCentreID,
            this.colCostCentreCheck,
            this.colCostCentreName});
            this.gvCostCentre.GridControl = this.gcCostCentre;
            this.gvCostCentre.Name = "gvCostCentre";
            this.gvCostCentre.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCostCentre.OptionsView.ShowGroupPanel = false;
            this.gvCostCentre.OptionsView.ShowIndicator = false;
            this.gvCostCentre.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvCostCentre_CustomRowCellEdit);
            this.gvCostCentre.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvCostCentre_MouseDown);
            this.gvCostCentre.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvCostCentre_MouseUp);
            // 
            // colCostCentreID
            // 
            resources.ApplyResources(this.colCostCentreID, "colCostCentreID");
            this.colCostCentreID.FieldName = "COST_CENTRE_ID";
            this.colCostCentreID.Name = "colCostCentreID";
            // 
            // colCostCentreCheck
            // 
            resources.ApplyResources(this.colCostCentreCheck, "colCostCentreCheck");
            this.colCostCentreCheck.ColumnEdit = this.rchkSelect;
            this.colCostCentreCheck.FieldName = "SELECT";
            this.colCostCentreCheck.Name = "colCostCentreCheck";
            this.colCostCentreCheck.OptionsColumn.AllowMove = false;
            this.colCostCentreCheck.OptionsColumn.AllowSize = false;
            this.colCostCentreCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCostCentreCheck.OptionsColumn.ShowCaption = false;
            this.colCostCentreCheck.OptionsFilter.AllowAutoFilter = false;
            this.colCostCentreCheck.OptionsFilter.AllowFilter = false;
            // 
            // rchkSelect
            // 
            resources.ApplyResources(this.rchkSelect, "rchkSelect");
            this.rchkSelect.Name = "rchkSelect";
            this.rchkSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkSelect.ValueChecked = 1;
            this.rchkSelect.ValueGrayed = 2;
            this.rchkSelect.ValueUnchecked = 0;
            this.rchkSelect.CheckedChanged += new System.EventHandler(this.rchkSelect_CheckedChanged);
            // 
            // colCostCentreName
            // 
            resources.ApplyResources(this.colCostCentreName, "colCostCentreName");
            this.colCostCentreName.FieldName = "COST_CENTRE_NAME";
            this.colCostCentreName.Name = "colCostCentreName";
            this.colCostCentreName.OptionsColumn.AllowEdit = false;
            this.colCostCentreName.OptionsColumn.AllowFocus = false;
            this.colCostCentreName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // lcgCostCentre
            // 
            resources.ApplyResources(this.lcgCostCentre, "lcgCostCentre");
            this.lcgCostCentre.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgCostCentre.GroupBordersVisible = false;
            this.lcgCostCentre.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgGroupCostCentre,
            this.lcgCCCategory});
            this.lcgCostCentre.Location = new System.Drawing.Point(0, 0);
            this.lcgCostCentre.Name = "lcgCostCentre";
            this.lcgCostCentre.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgCostCentre.Size = new System.Drawing.Size(492, 278);
            this.lcgCostCentre.TextVisible = false;
            // 
            // lcgGroupCostCentre
            // 
            this.lcgGroupCostCentre.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgGroupCostCentre.AppearanceGroup.Font")));
            this.lcgGroupCostCentre.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgGroupCostCentre, "lcgGroupCostCentre");
            this.lcgGroupCostCentre.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblcostCentreGrid,
            this.layoutControlItem11});
            this.lcgGroupCostCentre.Location = new System.Drawing.Point(0, 30);
            this.lcgGroupCostCentre.Name = "lcgGroupCostCentre";
            this.lcgGroupCostCentre.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgGroupCostCentre.Size = new System.Drawing.Size(492, 248);
            this.lcgGroupCostCentre.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            // 
            // lblcostCentreGrid
            // 
            this.lblcostCentreGrid.Control = this.gcCostCentre;
            resources.ApplyResources(this.lblcostCentreGrid, "lblcostCentreGrid");
            this.lblcostCentreGrid.Location = new System.Drawing.Point(0, 0);
            this.lblcostCentreGrid.Name = "lblcostCentreGrid";
            this.lblcostCentreGrid.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblcostCentreGrid.Size = new System.Drawing.Size(470, 184);
            this.lblcostCentreGrid.TextSize = new System.Drawing.Size(0, 0);
            this.lblcostCentreGrid.TextToControlDistance = 0;
            this.lblcostCentreGrid.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chkShowFilterCostCenter;
            resources.ApplyResources(this.layoutControlItem11, "layoutControlItem11");
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 184);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(470, 23);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // lcgCCCategory
            // 
            this.lcgCCCategory.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgCCCategory.AppearanceGroup.Font")));
            this.lcgCCCategory.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgCCCategory, "lcgCCCategory");
            this.lcgCCCategory.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCCCategory});
            this.lcgCCCategory.Location = new System.Drawing.Point(0, 0);
            this.lcgCCCategory.Name = "lcgCCCategory";
            this.lcgCCCategory.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcgCCCategory.Size = new System.Drawing.Size(492, 30);
            this.lcgCCCategory.TextVisible = false;
            // 
            // lcCCCategory
            // 
            this.lcCCCategory.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcCCCategory.AppearanceItemCaption.Font")));
            this.lcCCCategory.AppearanceItemCaption.Options.UseFont = true;
            this.lcCCCategory.Control = this.glkpCCCategory;
            this.lcCCCategory.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcCCCategory, "lcCCCategory");
            this.lcCCCategory.Location = new System.Drawing.Point(0, 0);
            this.lcCCCategory.MinSize = new System.Drawing.Size(109, 20);
            this.lcCCCategory.Name = "lcCCCategory";
            this.lcCCCategory.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcCCCategory.Size = new System.Drawing.Size(482, 20);
            this.lcCCCategory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcCCCategory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcCCCategory.TextSize = new System.Drawing.Size(60, 20);
            this.lcCCCategory.TextToControlDistance = 5;
            this.lcCCCategory.TrimClientAreaToControl = false;
            // 
            // xtpDate
            // 
            this.xtpDate.Controls.Add(this.lcDateCriteria);
            this.xtpDate.Name = "xtpDate";
            this.xtpDate.PageVisible = false;
            resources.ApplyResources(this.xtpDate, "xtpDate");
            // 
            // lcDateCriteria
            // 
            this.lcDateCriteria.Controls.Add(this.rboInKind);
            this.lcDateCriteria.Controls.Add(this.DateTo);
            this.lcDateCriteria.Controls.Add(this.DateFrom);
            this.lcDateCriteria.Controls.Add(this.gcReportCriteria);
            resources.ApplyResources(this.lcDateCriteria, "lcDateCriteria");
            this.lcDateCriteria.Name = "lcDateCriteria";
            this.lcDateCriteria.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(524, 119, 250, 350);
            this.lcDateCriteria.Root = this.lcgDateProperty;
            // 
            // rboInKind
            // 
            resources.ApplyResources(this.rboInKind, "rboInKind");
            this.rboInKind.Name = "rboInKind";
            this.rboInKind.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rboInKind.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rboInKind.Properties.Items"))), resources.GetString("rboInKind.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rboInKind.Properties.Items2"))), resources.GetString("rboInKind.Properties.Items3"))});
            this.rboInKind.StyleController = this.lcDateCriteria;
            // 
            // DateTo
            // 
            resources.ApplyResources(this.DateTo, "DateTo");
            this.DateTo.Name = "DateTo";
            this.DateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("DateTo.Properties.Buttons"))))});
            this.DateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("DateTo.Properties.Mask.MaskType")));
            this.DateTo.StyleController = this.lcDateCriteria;
            this.DateTo.EditValueChanged += new System.EventHandler(this.DateTo_EditValueChanged);
            // 
            // DateFrom
            // 
            resources.ApplyResources(this.DateFrom, "DateFrom");
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.DateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("DateFrom.Properties.Buttons"))))});
            this.DateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("DateFrom.Properties.Mask.MaskType")));
            this.DateFrom.StyleController = this.lcDateCriteria;
            this.DateFrom.EditValueChanged += new System.EventHandler(this.DateFrom_EditValueChanged);
            this.DateFrom.Leave += new System.EventHandler(this.DateFrom_Leave);
            // 
            // gcReportCriteria
            // 
            resources.ApplyResources(this.gcReportCriteria, "gcReportCriteria");
            this.gcReportCriteria.MainView = this.gvReportCriteria;
            this.gcReportCriteria.Name = "gcReportCriteria";
            this.gcReportCriteria.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkReportCriteria,
            this.repositoryItemCheckEdit,
            this.repositoryItemComboBox,
            this.repositoryItemDateEdit,
            this.repositoryItemRadioGroup1});
            this.gcReportCriteria.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReportCriteria});
            // 
            // gvReportCriteria
            // 
            this.gvReportCriteria.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvReportCriteria.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCriteriaName,
            this.colCriteriaType});
            this.gvReportCriteria.GridControl = this.gcReportCriteria;
            this.gvReportCriteria.Name = "gvReportCriteria";
            this.gvReportCriteria.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvReportCriteria.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvReportCriteria.OptionsView.ShowColumnHeaders = false;
            this.gvReportCriteria.OptionsView.ShowGroupPanel = false;
            this.gvReportCriteria.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvReportCriteria.OptionsView.ShowIndicator = false;
            this.gvReportCriteria.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvReportCriteria.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvReportCriteria_CustomRowCellEdit);
            this.gvReportCriteria.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.gvReportCriteria_CalcRowHeight);
            this.gvReportCriteria.RowCellDefaultAlignment += new DevExpress.XtraGrid.Views.Base.RowCellAlignmentEventHandler(this.gvReportCriteria_RowCellDefaultAlignment);
            // 
            // colID
            // 
            resources.ApplyResources(this.colID, "colID");
            this.colID.FieldName = "CriteriaValue";
            this.colID.Name = "colID";
            // 
            // colCriteriaName
            // 
            resources.ApplyResources(this.colCriteriaName, "colCriteriaName");
            this.colCriteriaName.FieldName = "Name";
            this.colCriteriaName.Name = "colCriteriaName";
            this.colCriteriaName.OptionsColumn.AllowEdit = false;
            this.colCriteriaName.OptionsColumn.AllowFocus = false;
            // 
            // colCriteriaType
            // 
            resources.ApplyResources(this.colCriteriaType, "colCriteriaType");
            this.colCriteriaType.FieldName = "Value";
            this.colCriteriaType.Name = "colCriteriaType";
            // 
            // rchkReportCriteria
            // 
            resources.ApplyResources(this.rchkReportCriteria, "rchkReportCriteria");
            this.rchkReportCriteria.Name = "rchkReportCriteria";
            this.rchkReportCriteria.ValueChecked = 1;
            this.rchkReportCriteria.ValueUnchecked = 0;
            // 
            // repositoryItemCheckEdit
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit, "repositoryItemCheckEdit");
            this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
            // 
            // repositoryItemComboBox
            // 
            resources.ApplyResources(this.repositoryItemComboBox, "repositoryItemComboBox");
            this.repositoryItemComboBox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.repositoryItemComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemComboBox.Buttons"))))});
            this.repositoryItemComboBox.Items.AddRange(new object[] {
            resources.GetString("repositoryItemComboBox.Items"),
            resources.GetString("repositoryItemComboBox.Items1"),
            resources.GetString("repositoryItemComboBox.Items2")});
            this.repositoryItemComboBox.Name = "repositoryItemComboBox";
            this.repositoryItemComboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemDateEdit
            // 
            this.repositoryItemDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemDateEdit.Buttons"))))});
            this.repositoryItemDateEdit.Name = "repositoryItemDateEdit";
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemRadioGroup1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repositoryItemRadioGroup1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemRadioGroup1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.repositoryItemRadioGroup1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("repositoryItemRadioGroup1.Items"))), resources.GetString("repositoryItemRadioGroup1.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("repositoryItemRadioGroup1.Items2"))), resources.GetString("repositoryItemRadioGroup1.Items3"))});
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            // 
            // lcgDateProperty
            // 
            resources.ApplyResources(this.lcgDateProperty, "lcgDateProperty");
            this.lcgDateProperty.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgDateProperty.GroupBordersVisible = false;
            this.lcgDateProperty.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgDate,
            this.emptySpaceItem3,
            this.lblGridControl,
            this.lblWithInKind,
            this.emptySpaceItem2});
            this.lcgDateProperty.Location = new System.Drawing.Point(0, 0);
            this.lcgDateProperty.Name = "lcgDateProperty";
            this.lcgDateProperty.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgDateProperty.Size = new System.Drawing.Size(492, 278);
            this.lcgDateProperty.TextVisible = false;
            // 
            // lcgDate
            // 
            resources.ApplyResources(this.lcgDate, "lcgDate");
            this.lcgDate.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblDateFrom,
            this.lblDateTo,
            this.emptyDateFrom,
            this.emptySpaceItem8});
            this.lcgDate.Location = new System.Drawing.Point(0, 0);
            this.lcgDate.Name = "lcgDate";
            this.lcgDate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgDate.Size = new System.Drawing.Size(492, 42);
            this.lcgDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgDate.TextVisible = false;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Control = this.DateFrom;
            resources.ApplyResources(this.lblDateFrom, "lblDateFrom");
            this.lblDateFrom.Location = new System.Drawing.Point(0, 0);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblDateFrom.Size = new System.Drawing.Size(155, 20);
            this.lblDateFrom.TextSize = new System.Drawing.Size(50, 13);
            // 
            // lblDateTo
            // 
            this.lblDateTo.Control = this.DateTo;
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Location = new System.Drawing.Point(305, 0);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblDateTo.Size = new System.Drawing.Size(154, 20);
            this.lblDateTo.TextSize = new System.Drawing.Size(50, 13);
            // 
            // emptyDateFrom
            // 
            this.emptyDateFrom.AllowHotTrack = false;
            resources.ApplyResources(this.emptyDateFrom, "emptyDateFrom");
            this.emptyDateFrom.Location = new System.Drawing.Point(155, 0);
            this.emptyDateFrom.Name = "emptyDateFrom";
            this.emptyDateFrom.Size = new System.Drawing.Size(150, 20);
            this.emptyDateFrom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem8, "emptySpaceItem8");
            this.emptySpaceItem8.Location = new System.Drawing.Point(459, 0);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(11, 20);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 42);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(492, 11);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblGridControl
            // 
            this.lblGridControl.Control = this.gcReportCriteria;
            resources.ApplyResources(this.lblGridControl, "lblGridControl");
            this.lblGridControl.Location = new System.Drawing.Point(0, 53);
            this.lblGridControl.Name = "lblGridControl";
            this.lblGridControl.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lblGridControl.Size = new System.Drawing.Size(492, 180);
            this.lblGridControl.TextSize = new System.Drawing.Size(0, 0);
            this.lblGridControl.TextToControlDistance = 0;
            this.lblGridControl.TextVisible = false;
            // 
            // lblWithInKind
            // 
            this.lblWithInKind.Control = this.rboInKind;
            resources.ApplyResources(this.lblWithInKind, "lblWithInKind");
            this.lblWithInKind.Location = new System.Drawing.Point(0, 244);
            this.lblWithInKind.Name = "lblWithInKind";
            this.lblWithInKind.Size = new System.Drawing.Size(492, 34);
            this.lblWithInKind.TextSize = new System.Drawing.Size(0, 0);
            this.lblWithInKind.TextToControlDistance = 0;
            this.lblWithInKind.TextVisible = false;
            this.lblWithInKind.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 233);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(492, 11);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // xtpProject
            // 
            this.xtpProject.Controls.Add(this.lcProject);
            this.xtpProject.Name = "xtpProject";
            this.xtpProject.PageVisible = false;
            resources.ApplyResources(this.xtpProject, "xtpProject");
            // 
            // lcProject
            // 
            this.lcProject.Controls.Add(this.glkpITRGroup);
            this.lcProject.Controls.Add(this.glkBankColumn1);
            this.lcProject.Controls.Add(this.glkbankColumn2);
            this.lcProject.Controls.Add(this.glkpBudgetCompare);
            this.lcProject.Controls.Add(this.glkpSociety);
            this.lcProject.Controls.Add(this.chkBankFilter);
            this.lcProject.Controls.Add(this.chkProjectFilter);
            this.lcProject.Controls.Add(this.gcBank);
            this.lcProject.Controls.Add(this.gcProject);
            this.lcProject.Controls.Add(this.glkpBudget);
            resources.ApplyResources(this.lcProject, "lcProject");
            this.lcProject.Name = "lcProject";
            this.lcProject.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(555, 130, 250, 350);
            this.lcProject.Root = this.lcgProject;
            // 
            // glkpITRGroup
            // 
            resources.ApplyResources(this.glkpITRGroup, "glkpITRGroup");
            this.glkpITRGroup.Name = "glkpITRGroup";
            this.glkpITRGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpITRGroup.Properties.Buttons"))))});
            this.glkpITRGroup.Properties.PopupFormMinSize = new System.Drawing.Size(200, 0);
            this.glkpITRGroup.Properties.PopupFormSize = new System.Drawing.Size(200, 0);
            this.glkpITRGroup.Properties.View = this.gridView5;
            this.glkpITRGroup.StyleController = this.lcProject;
            this.glkpITRGroup.EditValueChanged += new System.EventHandler(this.glkpITRGroup_EditValueChanged);
            // 
            // gridView5
            // 
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colITRGroupId,
            this.colITRGroupName});
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowColumnHeaders = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            // 
            // colITRGroupId
            // 
            resources.ApplyResources(this.colITRGroupId, "colITRGroupId");
            this.colITRGroupId.FieldName = "PROJECT_CATOGORY_ITRGROUP_ID";
            this.colITRGroupId.Name = "colITRGroupId";
            // 
            // colITRGroupName
            // 
            resources.ApplyResources(this.colITRGroupName, "colITRGroupName");
            this.colITRGroupName.FieldName = "PROJECT_CATOGORY_ITRGROUP";
            this.colITRGroupName.Name = "colITRGroupName";
            // 
            // glkBankColumn1
            // 
            resources.ApplyResources(this.glkBankColumn1, "glkBankColumn1");
            this.glkBankColumn1.Name = "glkBankColumn1";
            this.glkBankColumn1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkBankColumn1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkBankColumn1.Properties.Buttons"))))});
            this.glkBankColumn1.Properties.ImmediatePopup = true;
            this.glkBankColumn1.Properties.NullText = resources.GetString("glkBankColumn1.Properties.NullText");
            this.glkBankColumn1.Properties.PopupFormMinSize = new System.Drawing.Size(380, 0);
            this.glkBankColumn1.Properties.PopupFormSize = new System.Drawing.Size(380, 20);
            this.glkBankColumn1.Properties.View = this.gvMultiColumn1;
            this.glkBankColumn1.StyleController = this.lcProject;
            this.glkBankColumn1.EditValueChanged += new System.EventHandler(this.glkBankColumn1_EditValueChanged);
            // 
            // gvMultiColumn1
            // 
            this.gvMultiColumn1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMultiColumn1.Appearance.FocusedRow.Font")));
            this.gvMultiColumn1.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMultiColumn1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolMultiBank,
            this.gcolMultiLedgerId,
            this.gcolMultibankName});
            this.gvMultiColumn1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvMultiColumn1.Name = "gvMultiColumn1";
            this.gvMultiColumn1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMultiColumn1.OptionsView.ShowColumnHeaders = false;
            this.gvMultiColumn1.OptionsView.ShowGroupPanel = false;
            this.gvMultiColumn1.OptionsView.ShowIndicator = false;
            // 
            // gcolMultiBank
            // 
            this.gcolMultiBank.FieldName = "BANK";
            this.gcolMultiBank.Name = "gcolMultiBank";
            // 
            // gcolMultiLedgerId
            // 
            this.gcolMultiLedgerId.FieldName = "LEDGER_ID";
            this.gcolMultiLedgerId.Name = "gcolMultiLedgerId";
            // 
            // gcolMultibankName
            // 
            this.gcolMultibankName.FieldName = "BANK";
            this.gcolMultibankName.Name = "gcolMultibankName";
            resources.ApplyResources(this.gcolMultibankName, "gcolMultibankName");
            // 
            // glkbankColumn2
            // 
            resources.ApplyResources(this.glkbankColumn2, "glkbankColumn2");
            this.glkbankColumn2.Name = "glkbankColumn2";
            this.glkbankColumn2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkbankColumn2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkbankColumn2.Properties.Buttons"))))});
            this.glkbankColumn2.Properties.NullText = resources.GetString("glkbankColumn2.Properties.NullText");
            this.glkbankColumn2.Properties.PopupFormMinSize = new System.Drawing.Size(380, 0);
            this.glkbankColumn2.Properties.PopupFormSize = new System.Drawing.Size(380, 20);
            this.glkbankColumn2.Properties.View = this.gvMultiColumn2;
            this.glkbankColumn2.StyleController = this.lcProject;
            this.glkbankColumn2.EditValueChanged += new System.EventHandler(this.glkbankColumn2_EditValueChanged);
            // 
            // gvMultiColumn2
            // 
            this.gvMultiColumn2.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMultiColumn2.Appearance.FocusedRow.Font")));
            this.gvMultiColumn2.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMultiColumn2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolMultiColBank,
            this.gcolMulticolLedgerid,
            this.gcolMulticolumnbankName});
            this.gvMultiColumn2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvMultiColumn2.Name = "gvMultiColumn2";
            this.gvMultiColumn2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMultiColumn2.OptionsView.ShowColumnHeaders = false;
            this.gvMultiColumn2.OptionsView.ShowGroupPanel = false;
            this.gvMultiColumn2.OptionsView.ShowIndicator = false;
            // 
            // gcolMultiColBank
            // 
            this.gcolMultiColBank.FieldName = "BANK";
            this.gcolMultiColBank.Name = "gcolMultiColBank";
            // 
            // gcolMulticolLedgerid
            // 
            this.gcolMulticolLedgerid.FieldName = "LEDGER_ID";
            this.gcolMulticolLedgerid.Name = "gcolMulticolLedgerid";
            // 
            // gcolMulticolumnbankName
            // 
            this.gcolMulticolumnbankName.FieldName = "BANK";
            this.gcolMulticolumnbankName.Name = "gcolMulticolumnbankName";
            resources.ApplyResources(this.gcolMulticolumnbankName, "gcolMulticolumnbankName");
            // 
            // glkpBudgetCompare
            // 
            resources.ApplyResources(this.glkpBudgetCompare, "glkpBudgetCompare");
            this.glkpBudgetCompare.Name = "glkpBudgetCompare";
            this.glkpBudgetCompare.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpBudgetCompare.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpBudgetCompare.Properties.Buttons"))))});
            this.glkpBudgetCompare.Properties.NullText = resources.GetString("glkpBudgetCompare.Properties.NullText");
            this.glkpBudgetCompare.Properties.PopupFormMinSize = new System.Drawing.Size(132, 20);
            this.glkpBudgetCompare.Properties.View = this.gridView1;
            this.glkpBudgetCompare.StyleController = this.lcProject;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "BANK_ID";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "BANK";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // glkpSociety
            // 
            resources.ApplyResources(this.glkpSociety, "glkpSociety");
            this.glkpSociety.Name = "glkpSociety";
            this.glkpSociety.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpSociety.Properties.Buttons"))))});
            this.glkpSociety.Properties.PopupFormMinSize = new System.Drawing.Size(427, 0);
            this.glkpSociety.Properties.PopupFormSize = new System.Drawing.Size(427, 0);
            this.glkpSociety.Properties.View = this.gridLookUpEdit1View;
            this.glkpSociety.StyleController = this.lcProject;
            this.glkpSociety.EditValueChanged += new System.EventHandler(this.glkpSociety_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSocietyId,
            this.colSocietyName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colSocietyId
            // 
            resources.ApplyResources(this.colSocietyId, "colSocietyId");
            this.colSocietyId.FieldName = "SOCIETY_ID";
            this.colSocietyId.Name = "colSocietyId";
            // 
            // colSocietyName
            // 
            resources.ApplyResources(this.colSocietyName, "colSocietyName");
            this.colSocietyName.FieldName = "SOCIETYNAME";
            this.colSocietyName.Name = "colSocietyName";
            this.colSocietyName.OptionsColumn.ShowCaption = false;
            // 
            // chkBankFilter
            // 
            resources.ApplyResources(this.chkBankFilter, "chkBankFilter");
            this.chkBankFilter.Name = "chkBankFilter";
            this.chkBankFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkBankFilter.Properties.Caption = resources.GetString("chkBankFilter.Properties.Caption");
            this.chkBankFilter.StyleController = this.lcProject;
            this.chkBankFilter.CheckedChanged += new System.EventHandler(this.chkBankFilter_CheckedChanged);
            // 
            // chkProjectFilter
            // 
            resources.ApplyResources(this.chkProjectFilter, "chkProjectFilter");
            this.chkProjectFilter.Name = "chkProjectFilter";
            this.chkProjectFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkProjectFilter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkProjectFilter.Properties.Caption = resources.GetString("chkProjectFilter.Properties.Caption");
            this.chkProjectFilter.StyleController = this.lcProject;
            this.chkProjectFilter.CheckedChanged += new System.EventHandler(this.chkProjectFilter_CheckedChanged);
            // 
            // gcBank
            // 
            resources.ApplyResources(this.gcBank, "gcBank");
            this.gcBank.MainView = this.gvBank;
            this.gcBank.Name = "gcBank";
            this.gcBank.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkBankCheck});
            this.gcBank.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBank});
            this.gcBank.Click += new System.EventHandler(this.gcBank_Click);
            // 
            // gvBank
            // 
            this.gvBank.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBank.Appearance.HeaderPanel.Font")));
            this.gvBank.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBank.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBankId,
            this.colBankCheck,
            this.colBankName,
            this.colBankGroupId,
            this.colBankProjectId,
            this.colFDAccountId});
            this.gvBank.GridControl = this.gcBank;
            this.gvBank.Name = "gvBank";
            this.gvBank.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBank.OptionsView.ShowGroupPanel = false;
            this.gvBank.OptionsView.ShowIndicator = false;
            this.gvBank.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvBank_CustomRowCellEdit);
            this.gvBank.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvBank_MouseDown);
            this.gvBank.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvBank_MouseUp);
            // 
            // colBankId
            // 
            resources.ApplyResources(this.colBankId, "colBankId");
            this.colBankId.FieldName = "BANK_ID";
            this.colBankId.Name = "colBankId";
            // 
            // colBankCheck
            // 
            resources.ApplyResources(this.colBankCheck, "colBankCheck");
            this.colBankCheck.ColumnEdit = this.rchkBankCheck;
            this.colBankCheck.FieldName = "SELECT";
            this.colBankCheck.Name = "colBankCheck";
            this.colBankCheck.OptionsColumn.AllowMove = false;
            this.colBankCheck.OptionsColumn.AllowSize = false;
            this.colBankCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBankCheck.OptionsColumn.ShowCaption = false;
            this.colBankCheck.OptionsFilter.AllowAutoFilter = false;
            this.colBankCheck.OptionsFilter.AllowFilter = false;
            // 
            // rchkBankCheck
            // 
            this.rchkBankCheck.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkBankCheck, "rchkBankCheck");
            this.rchkBankCheck.Name = "rchkBankCheck";
            this.rchkBankCheck.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkBankCheck.ValueChecked = 1;
            this.rchkBankCheck.ValueGrayed = 2;
            this.rchkBankCheck.ValueUnchecked = 0;
            this.rchkBankCheck.CheckedChanged += new System.EventHandler(this.rchkBankCheck_CheckedChanged);
            // 
            // colBankName
            // 
            resources.ApplyResources(this.colBankName, "colBankName");
            this.colBankName.FieldName = "BANK";
            this.colBankName.Name = "colBankName";
            this.colBankName.OptionsColumn.AllowEdit = false;
            this.colBankName.OptionsColumn.AllowFocus = false;
            this.colBankName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBankGroupId
            // 
            resources.ApplyResources(this.colBankGroupId, "colBankGroupId");
            this.colBankGroupId.FieldName = "GROUP_ID";
            this.colBankGroupId.Name = "colBankGroupId";
            // 
            // colBankProjectId
            // 
            resources.ApplyResources(this.colBankProjectId, "colBankProjectId");
            this.colBankProjectId.FieldName = "PROJECT_ID";
            this.colBankProjectId.Name = "colBankProjectId";
            // 
            // colFDAccountId
            // 
            resources.ApplyResources(this.colFDAccountId, "colFDAccountId");
            this.colFDAccountId.FieldName = "FD_ACCOUNT_ID";
            this.colFDAccountId.Name = "colFDAccountId";
            // 
            // gcProject
            // 
            resources.ApplyResources(this.gcProject, "gcProject");
            this.gcProject.MainView = this.gvProject;
            this.gcProject.Name = "gcProject";
            this.gcProject.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkSelectProject});
            this.gcProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProject});
            this.gcProject.Click += new System.EventHandler(this.gcProject_Click);
            // 
            // gvProject
            // 
            this.gvProject.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvProject.Appearance.HeaderPanel.Font")));
            this.gvProject.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colCheck,
            this.colProjectName});
            this.gvProject.GridControl = this.gcProject;
            this.gvProject.Name = "gvProject";
            this.gvProject.OptionsView.ShowGroupPanel = false;
            this.gvProject.OptionsView.ShowIndicator = false;
            this.gvProject.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvProject_CustomRowCellEdit);
            this.gvProject.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvProject_SelectionChanged);
            this.gvProject.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvProject_CellValueChanged);
            this.gvProject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvProject_MouseDown);
            this.gvProject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvProject_MouseUp);
            this.gvProject.Click += new System.EventHandler(this.gvProject_Click);
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            this.colProjectId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCheck
            // 
            resources.ApplyResources(this.colCheck, "colCheck");
            this.colCheck.ColumnEdit = this.rchkSelectProject;
            this.colCheck.FieldName = "SELECT";
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.AllowMove = false;
            this.colCheck.OptionsColumn.AllowSize = false;
            this.colCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCheck.OptionsColumn.FixedWidth = true;
            this.colCheck.OptionsColumn.ShowCaption = false;
            this.colCheck.OptionsFilter.AllowAutoFilter = false;
            this.colCheck.OptionsFilter.AllowFilter = false;
            // 
            // rchkSelectProject
            // 
            this.rchkSelectProject.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkSelectProject, "rchkSelectProject");
            this.rchkSelectProject.Name = "rchkSelectProject";
            this.rchkSelectProject.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkSelectProject.ValueChecked = 1;
            this.rchkSelectProject.ValueGrayed = 2;
            this.rchkSelectProject.ValueUnchecked = 0;
            this.rchkSelectProject.CheckedChanged += new System.EventHandler(this.rchkSelectProject_CheckedChanged);
            // 
            // colProjectName
            // 
            resources.ApplyResources(this.colProjectName, "colProjectName");
            this.colProjectName.FieldName = "PROJECT";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowEdit = false;
            this.colProjectName.OptionsColumn.AllowFocus = false;
            this.colProjectName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // glkpBudget
            // 
            resources.ApplyResources(this.glkpBudget, "glkpBudget");
            this.glkpBudget.Name = "glkpBudget";
            this.glkpBudget.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpBudget.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpBudget.Properties.Buttons"))))});
            this.glkpBudget.Properties.NullText = resources.GetString("glkpBudget.Properties.NullText");
            this.glkpBudget.Properties.PopupFormMinSize = new System.Drawing.Size(80, 20);
            this.glkpBudget.Properties.View = this.gridView2;
            this.glkpBudget.StyleController = this.lcProject;
            this.glkpBudget.EditValueChanged += new System.EventHandler(this.glkpBudget_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView2.Appearance.FocusedRow.Font")));
            this.gridView2.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowColumnHeaders = false;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "BANK_ID";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.FieldName = "BANK";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.ShowCaption = false;
            // 
            // lcgProject
            // 
            resources.ApplyResources(this.lcgProject, "lcgProject");
            this.lcgProject.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgProject.GroupBordersVisible = false;
            this.lcgProject.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgProjectwithDivision,
            this.lcgBankAccount,
            this.lcgSociety,
            this.lcgBudgetCompare});
            this.lcgProject.Location = new System.Drawing.Point(0, 0);
            this.lcgProject.Name = "lcgProject";
            this.lcgProject.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgProject.Size = new System.Drawing.Size(492, 278);
            this.lcgProject.TextVisible = false;
            // 
            // lcgProjectwithDivision
            // 
            this.lcgProjectwithDivision.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgProjectwithDivision.AppearanceGroup.Font")));
            this.lcgProjectwithDivision.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgProjectwithDivision, "lcgProjectwithDivision");
            this.lcgProjectwithDivision.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProjectGrid,
            this.layoutControlItem6,
            this.lcgMultiColumnBankChooser});
            this.lcgProjectwithDivision.Location = new System.Drawing.Point(0, 30);
            this.lcgProjectwithDivision.Name = "lcgProjectwithDivision";
            this.lcgProjectwithDivision.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgProjectwithDivision.Size = new System.Drawing.Size(243, 248);
            this.lcgProjectwithDivision.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            // 
            // lblProjectGrid
            // 
            this.lblProjectGrid.Control = this.gcProject;
            resources.ApplyResources(this.lblProjectGrid, "lblProjectGrid");
            this.lblProjectGrid.Location = new System.Drawing.Point(0, 0);
            this.lblProjectGrid.Name = "lblProjectGrid";
            this.lblProjectGrid.Size = new System.Drawing.Size(221, 108);
            this.lblProjectGrid.TextSize = new System.Drawing.Size(0, 0);
            this.lblProjectGrid.TextToControlDistance = 0;
            this.lblProjectGrid.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkProjectFilter;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(221, 23);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lcgMultiColumnBankChooser
            // 
            resources.ApplyResources(this.lcgMultiColumnBankChooser, "lcgMultiColumnBankChooser");
            this.lcgMultiColumnBankChooser.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem35,
            this.layoutControlItem34});
            this.lcgMultiColumnBankChooser.Location = new System.Drawing.Point(0, 131);
            this.lcgMultiColumnBankChooser.Name = "lcgMultiColumnBankChooser";
            this.lcgMultiColumnBankChooser.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgMultiColumnBankChooser.Size = new System.Drawing.Size(221, 76);
            this.lcgMultiColumnBankChooser.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.lcgMultiColumnBankChooser.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem35.AppearanceItemCaption.Font")));
            this.layoutControlItem35.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem35.Control = this.glkbankColumn2;
            resources.ApplyResources(this.layoutControlItem35, "layoutControlItem35");
            this.layoutControlItem35.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(215, 24);
            this.layoutControlItem35.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem35.TextSize = new System.Drawing.Size(52, 13);
            this.layoutControlItem35.TextToControlDistance = 5;
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem34.AppearanceItemCaption.Font")));
            this.layoutControlItem34.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem34.Control = this.glkBankColumn1;
            resources.ApplyResources(this.layoutControlItem34, "layoutControlItem34");
            this.layoutControlItem34.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(215, 24);
            this.layoutControlItem34.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem34.TextSize = new System.Drawing.Size(52, 13);
            this.layoutControlItem34.TextToControlDistance = 5;
            // 
            // lcgBankAccount
            // 
            this.lcgBankAccount.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgBankAccount.AppearanceGroup.Font")));
            this.lcgBankAccount.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgBankAccount, "lcgBankAccount");
            this.lcgBankAccount.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblBankGrid,
            this.layoutControlItem8});
            this.lcgBankAccount.Location = new System.Drawing.Point(243, 30);
            this.lcgBankAccount.Name = "lcgBankAccount";
            this.lcgBankAccount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgBankAccount.Size = new System.Drawing.Size(249, 159);
            this.lcgBankAccount.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgBankAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lblBankGrid
            // 
            this.lblBankGrid.Control = this.gcBank;
            resources.ApplyResources(this.lblBankGrid, "lblBankGrid");
            this.lblBankGrid.Location = new System.Drawing.Point(0, 0);
            this.lblBankGrid.Name = "lblBankGrid";
            this.lblBankGrid.Size = new System.Drawing.Size(227, 95);
            this.lblBankGrid.TextSize = new System.Drawing.Size(0, 0);
            this.lblBankGrid.TextToControlDistance = 0;
            this.lblBankGrid.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.chkBankFilter;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 95);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(227, 23);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // lcgSociety
            // 
            resources.ApplyResources(this.lcgSociety, "lcgSociety");
            this.lcgSociety.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcSociety,
            this.lciITRGroup});
            this.lcgSociety.Location = new System.Drawing.Point(0, 0);
            this.lcgSociety.Name = "lcgSociety";
            this.lcgSociety.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgSociety.Size = new System.Drawing.Size(492, 30);
            this.lcgSociety.TextVisible = false;
            // 
            // lcSociety
            // 
            this.lcSociety.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcSociety.AppearanceItemCaption.Font")));
            this.lcSociety.AppearanceItemCaption.Options.UseFont = true;
            this.lcSociety.Control = this.glkpSociety;
            resources.ApplyResources(this.lcSociety, "lcSociety");
            this.lcSociety.Location = new System.Drawing.Point(0, 0);
            this.lcSociety.Name = "lcSociety";
            this.lcSociety.Size = new System.Drawing.Size(245, 24);
            this.lcSociety.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcSociety.TextSize = new System.Drawing.Size(50, 20);
            this.lcSociety.TextToControlDistance = 5;
            // 
            // lciITRGroup
            // 
            this.lciITRGroup.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lciITRGroup.AppearanceItemCaption.Font")));
            this.lciITRGroup.AppearanceItemCaption.Options.UseFont = true;
            this.lciITRGroup.Control = this.glkpITRGroup;
            resources.ApplyResources(this.lciITRGroup, "lciITRGroup");
            this.lciITRGroup.Location = new System.Drawing.Point(245, 0);
            this.lciITRGroup.Name = "lciITRGroup";
            this.lciITRGroup.Size = new System.Drawing.Size(241, 24);
            this.lciITRGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciITRGroup.TextSize = new System.Drawing.Size(60, 20);
            this.lciITRGroup.TextToControlDistance = 5;
            this.lciITRGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcgBudgetCompare
            // 
            this.lcgBudgetCompare.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgBudgetCompare.AppearanceGroup.Font")));
            this.lcgBudgetCompare.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgBudgetCompare, "lcgBudgetCompare");
            this.lcgBudgetCompare.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCompareBudget1,
            this.lcCompareBudget2});
            this.lcgBudgetCompare.Location = new System.Drawing.Point(243, 189);
            this.lcgBudgetCompare.Name = "lcgBudgetCompare";
            this.lcgBudgetCompare.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgBudgetCompare.Size = new System.Drawing.Size(249, 89);
            this.lcgBudgetCompare.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgBudgetCompare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcCompareBudget1
            // 
            this.lcCompareBudget1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcCompareBudget1.AppearanceItemCaption.Font")));
            this.lcCompareBudget1.AppearanceItemCaption.Options.UseFont = true;
            this.lcCompareBudget1.Control = this.glkpBudget;
            resources.ApplyResources(this.lcCompareBudget1, "lcCompareBudget1");
            this.lcCompareBudget1.Location = new System.Drawing.Point(0, 0);
            this.lcCompareBudget1.Name = "lcCompareBudget1";
            this.lcCompareBudget1.Size = new System.Drawing.Size(227, 24);
            this.lcCompareBudget1.TextSize = new System.Drawing.Size(94, 13);
            // 
            // lcCompareBudget2
            // 
            this.lcCompareBudget2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcCompareBudget2.AppearanceItemCaption.Font")));
            this.lcCompareBudget2.AppearanceItemCaption.Options.UseFont = true;
            this.lcCompareBudget2.Control = this.glkpBudgetCompare;
            resources.ApplyResources(this.lcCompareBudget2, "lcCompareBudget2");
            this.lcCompareBudget2.Location = new System.Drawing.Point(0, 24);
            this.lcCompareBudget2.Name = "lcCompareBudget2";
            this.lcCompareBudget2.Size = new System.Drawing.Size(227, 24);
            this.lcCompareBudget2.TextSize = new System.Drawing.Size(94, 13);
            // 
            // xtpLedger
            // 
            this.xtpLedger.Controls.Add(this.lcLedger);
            this.xtpLedger.Name = "xtpLedger";
            this.xtpLedger.PageVisible = false;
            resources.ApplyResources(this.xtpLedger, "xtpLedger");
            // 
            // lcLedger
            // 
            this.lcLedger.Controls.Add(this.chkLedgerGroupFilter);
            this.lcLedger.Controls.Add(this.chkLedgerFilter);
            this.lcLedger.Controls.Add(this.gcLedgerDetail);
            this.lcLedger.Controls.Add(this.gcLedger);
            resources.ApplyResources(this.lcLedger, "lcLedger");
            this.lcLedger.Name = "lcLedger";
            this.lcLedger.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(708, 132, 250, 350);
            this.lcLedger.Root = this.lcgLedgerGroup;
            // 
            // chkLedgerGroupFilter
            // 
            resources.ApplyResources(this.chkLedgerGroupFilter, "chkLedgerGroupFilter");
            this.chkLedgerGroupFilter.Name = "chkLedgerGroupFilter";
            this.chkLedgerGroupFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkLedgerGroupFilter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkLedgerGroupFilter.Properties.Caption = resources.GetString("chkLedgerGroupFilter.Properties.Caption");
            this.chkLedgerGroupFilter.StyleController = this.lcLedger;
            this.chkLedgerGroupFilter.CheckedChanged += new System.EventHandler(this.chkLedgerGroupFilter_CheckedChanged_1);
            // 
            // chkLedgerFilter
            // 
            resources.ApplyResources(this.chkLedgerFilter, "chkLedgerFilter");
            this.chkLedgerFilter.Name = "chkLedgerFilter";
            this.chkLedgerFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkLedgerFilter.Properties.Caption = resources.GetString("chkLedgerFilter.Properties.Caption");
            this.chkLedgerFilter.StyleController = this.lcLedger;
            this.chkLedgerFilter.CheckedChanged += new System.EventHandler(this.chkLedgerFilter_CheckedChanged_1);
            // 
            // gcLedgerDetail
            // 
            resources.ApplyResources(this.gcLedgerDetail, "gcLedgerDetail");
            this.gcLedgerDetail.MainView = this.gvLedgerDetails;
            this.gcLedgerDetail.Name = "gcLedgerDetail";
            this.gcLedgerDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkledger});
            this.gcLedgerDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedgerDetails});
            // 
            // gvLedgerDetails
            // 
            this.gvLedgerDetails.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedgerDetails.Appearance.HeaderPanel.Font")));
            this.gvLedgerDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLedgerDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colLedgerSelect,
            this.colLedgerName,
            this.colLedgerGroupId});
            this.gvLedgerDetails.GridControl = this.gcLedgerDetail;
            this.gvLedgerDetails.Name = "gvLedgerDetails";
            this.gvLedgerDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLedgerDetails.OptionsView.ShowGroupPanel = false;
            this.gvLedgerDetails.OptionsView.ShowIndicator = false;
            this.gvLedgerDetails.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvLedgerDetails_CustomRowCellEdit);
            this.gvLedgerDetails.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvLedgerDetails_CellValueChanged);
            this.gvLedgerDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvLedgerDetails_MouseDown);
            this.gvLedgerDetails.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvLedgerDetails_MouseUp);
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colLedgerSelect
            // 
            resources.ApplyResources(this.colLedgerSelect, "colLedgerSelect");
            this.colLedgerSelect.ColumnEdit = this.rchkledger;
            this.colLedgerSelect.FieldName = "SELECT";
            this.colLedgerSelect.Name = "colLedgerSelect";
            this.colLedgerSelect.OptionsColumn.AllowMove = false;
            this.colLedgerSelect.OptionsColumn.AllowSize = false;
            this.colLedgerSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerSelect.OptionsColumn.ShowCaption = false;
            this.colLedgerSelect.OptionsFilter.AllowAutoFilter = false;
            this.colLedgerSelect.OptionsFilter.AllowFilter = false;
            // 
            // rchkledger
            // 
            this.rchkledger.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkledger, "rchkledger");
            this.rchkledger.Name = "rchkledger";
            this.rchkledger.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkledger.ValueChecked = 1;
            this.rchkledger.ValueGrayed = 2;
            this.rchkledger.ValueUnchecked = 0;
            this.rchkledger.CheckedChanged += new System.EventHandler(this.rchkledger_CheckedChanged);
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsColumn.AllowEdit = false;
            this.colLedgerName.OptionsColumn.AllowFocus = false;
            this.colLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLedgerGroupId
            // 
            resources.ApplyResources(this.colLedgerGroupId, "colLedgerGroupId");
            this.colLedgerGroupId.FieldName = "GROUP_ID";
            this.colLedgerGroupId.Name = "colLedgerGroupId";
            // 
            // gcLedger
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gcLedger.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            resources.ApplyResources(this.gcLedger, "gcLedger");
            this.gcLedger.MainView = this.gvLedger;
            this.gcLedger.Name = "gcLedger";
            this.gcLedger.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkCheck});
            this.gcLedger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedger});
            // 
            // gvLedger
            // 
            this.gvLedger.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedger.Appearance.HeaderPanel.Font")));
            this.gvLedger.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLedger.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGroupId,
            this.colLedgerCheck,
            this.colledgerGroup});
            this.gvLedger.GridControl = this.gcLedger;
            this.gvLedger.Name = "gvLedger";
            this.gvLedger.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLedger.OptionsView.ShowGroupPanel = false;
            this.gvLedger.OptionsView.ShowIndicator = false;
            this.gvLedger.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvLedger_CustomRowCellEdit);
            this.gvLedger.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvLedger_CellValueChanged);
            this.gvLedger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvLedger_MouseDown);
            this.gvLedger.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvLedger_MouseUp);
            this.gvLedger.Click += new System.EventHandler(this.gvLedger_Click);
            // 
            // colGroupId
            // 
            resources.ApplyResources(this.colGroupId, "colGroupId");
            this.colGroupId.FieldName = "GROUP_ID";
            this.colGroupId.Name = "colGroupId";
            // 
            // colLedgerCheck
            // 
            resources.ApplyResources(this.colLedgerCheck, "colLedgerCheck");
            this.colLedgerCheck.ColumnEdit = this.rchkCheck;
            this.colLedgerCheck.FieldName = "SELECT";
            this.colLedgerCheck.Name = "colLedgerCheck";
            this.colLedgerCheck.OptionsColumn.AllowMove = false;
            this.colLedgerCheck.OptionsColumn.AllowSize = false;
            this.colLedgerCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerCheck.OptionsColumn.ShowCaption = false;
            this.colLedgerCheck.OptionsFilter.AllowAutoFilter = false;
            this.colLedgerCheck.OptionsFilter.AllowFilter = false;
            // 
            // rchkCheck
            // 
            this.rchkCheck.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkCheck, "rchkCheck");
            this.rchkCheck.Name = "rchkCheck";
            this.rchkCheck.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkCheck.RadioGroupIndex = 0;
            this.rchkCheck.ValueChecked = 1;
            this.rchkCheck.ValueGrayed = 2;
            this.rchkCheck.ValueUnchecked = 0;
            this.rchkCheck.CheckedChanged += new System.EventHandler(this.rchkCheck_CheckedChanged);
            // 
            // colledgerGroup
            // 
            resources.ApplyResources(this.colledgerGroup, "colledgerGroup");
            this.colledgerGroup.FieldName = "GROUP";
            this.colledgerGroup.Name = "colledgerGroup";
            this.colledgerGroup.OptionsColumn.AllowEdit = false;
            this.colledgerGroup.OptionsColumn.AllowFocus = false;
            this.colledgerGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // lcgLedgerGroup
            // 
            resources.ApplyResources(this.lcgLedgerGroup, "lcgLedgerGroup");
            this.lcgLedgerGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgLedgerGroup.GroupBordersVisible = false;
            this.lcgLedgerGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.grpLedgerGroup,
            this.lcGrpLedger});
            this.lcgLedgerGroup.Location = new System.Drawing.Point(0, 0);
            this.lcgLedgerGroup.Name = "lcgLedgerGroup";
            this.lcgLedgerGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgLedgerGroup.Size = new System.Drawing.Size(492, 278);
            this.lcgLedgerGroup.TextVisible = false;
            // 
            // grpLedgerGroup
            // 
            this.grpLedgerGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("grpLedgerGroup.AppearanceGroup.Font")));
            this.grpLedgerGroup.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.grpLedgerGroup, "grpLedgerGroup");
            this.grpLedgerGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblLedgerGroup,
            this.layoutControlItem9});
            this.grpLedgerGroup.Location = new System.Drawing.Point(0, 0);
            this.grpLedgerGroup.Name = "grpLedgerGroup";
            this.grpLedgerGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.grpLedgerGroup.Size = new System.Drawing.Size(244, 278);
            this.grpLedgerGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            // 
            // lblLedgerGroup
            // 
            this.lblLedgerGroup.Control = this.gcLedger;
            resources.ApplyResources(this.lblLedgerGroup, "lblLedgerGroup");
            this.lblLedgerGroup.Location = new System.Drawing.Point(0, 0);
            this.lblLedgerGroup.Name = "lblLedgerGroup";
            this.lblLedgerGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lblLedgerGroup.Size = new System.Drawing.Size(222, 214);
            this.lblLedgerGroup.TextSize = new System.Drawing.Size(0, 0);
            this.lblLedgerGroup.TextToControlDistance = 0;
            this.lblLedgerGroup.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.chkLedgerGroupFilter;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(222, 23);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // lcGrpLedger
            // 
            this.lcGrpLedger.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpLedger.AppearanceGroup.Font")));
            this.lcGrpLedger.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpLedger, "lcGrpLedger");
            this.lcGrpLedger.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem10});
            this.lcGrpLedger.Location = new System.Drawing.Point(244, 0);
            this.lcGrpLedger.Name = "lcGrpLedger";
            this.lcGrpLedger.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcGrpLedger.Size = new System.Drawing.Size(248, 278);
            this.lcGrpLedger.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gcLedgerDetail;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem7.Size = new System.Drawing.Size(226, 214);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.chkLedgerFilter;
            resources.ApplyResources(this.layoutControlItem10, "layoutControlItem10");
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(226, 23);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // xrtpPartyLedger
            // 
            this.xrtpPartyLedger.Controls.Add(this.lcTDSParties);
            this.xrtpPartyLedger.Name = "xrtpPartyLedger";
            this.xrtpPartyLedger.PageVisible = false;
            resources.ApplyResources(this.xrtpPartyLedger, "xrtpPartyLedger");
            // 
            // lcTDSParties
            // 
            this.lcTDSParties.Controls.Add(this.chkPartyFilter);
            this.lcTDSParties.Controls.Add(this.gcTDSParties);
            resources.ApplyResources(this.lcTDSParties, "lcTDSParties");
            this.lcTDSParties.Name = "lcTDSParties";
            this.lcTDSParties.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(752, 131, 250, 350);
            this.lcTDSParties.Root = this.lcgTDSParties;
            // 
            // chkPartyFilter
            // 
            resources.ApplyResources(this.chkPartyFilter, "chkPartyFilter");
            this.chkPartyFilter.Name = "chkPartyFilter";
            this.chkPartyFilter.Properties.Caption = resources.GetString("chkPartyFilter.Properties.Caption");
            this.chkPartyFilter.StyleController = this.lcTDSParties;
            this.chkPartyFilter.CheckedChanged += new System.EventHandler(this.chkPartyFilter_CheckedChanged);
            // 
            // gcTDSParties
            // 
            resources.ApplyResources(this.gcTDSParties, "gcTDSParties");
            this.gcTDSParties.MainView = this.gvTDSParties;
            this.gcTDSParties.Name = "gcTDSParties";
            this.gcTDSParties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkPartyLedger});
            this.gcTDSParties.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTDSParties});
            // 
            // gvTDSParties
            // 
            this.gvTDSParties.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSParties.Appearance.FocusedRow.Font")));
            this.gvTDSParties.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTDSParties.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSParties.Appearance.HeaderPanel.Font")));
            this.gvTDSParties.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTDSParties.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTDSPartiesId,
            this.colPartyLedger,
            this.colTDSPartiesName});
            this.gvTDSParties.GridControl = this.gcTDSParties;
            this.gvTDSParties.Name = "gvTDSParties";
            this.gvTDSParties.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTDSParties.OptionsView.ShowGroupPanel = false;
            this.gvTDSParties.OptionsView.ShowIndicator = false;
            this.gvTDSParties.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvTDSParties_CustomRowCellEdit);
            this.gvTDSParties.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvTDSParties_MouseDown);
            this.gvTDSParties.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvTDSParties_MouseUp);
            // 
            // colTDSPartiesId
            // 
            resources.ApplyResources(this.colTDSPartiesId, "colTDSPartiesId");
            this.colTDSPartiesId.FieldName = "LEDGER_ID";
            this.colTDSPartiesId.Name = "colTDSPartiesId";
            // 
            // colPartyLedger
            // 
            resources.ApplyResources(this.colPartyLedger, "colPartyLedger");
            this.colPartyLedger.ColumnEdit = this.rchkPartyLedger;
            this.colPartyLedger.FieldName = "SELECT";
            this.colPartyLedger.Name = "colPartyLedger";
            this.colPartyLedger.OptionsColumn.AllowMove = false;
            this.colPartyLedger.OptionsColumn.AllowSize = false;
            this.colPartyLedger.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPartyLedger.OptionsColumn.FixedWidth = true;
            this.colPartyLedger.OptionsColumn.ShowCaption = false;
            this.colPartyLedger.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkPartyLedger
            // 
            resources.ApplyResources(this.rchkPartyLedger, "rchkPartyLedger");
            this.rchkPartyLedger.Name = "rchkPartyLedger";
            this.rchkPartyLedger.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkPartyLedger.ValueChecked = 1;
            this.rchkPartyLedger.ValueGrayed = 2;
            this.rchkPartyLedger.ValueUnchecked = 0;
            // 
            // colTDSPartiesName
            // 
            resources.ApplyResources(this.colTDSPartiesName, "colTDSPartiesName");
            this.colTDSPartiesName.FieldName = "LEDGER";
            this.colTDSPartiesName.Name = "colTDSPartiesName";
            this.colTDSPartiesName.OptionsColumn.AllowEdit = false;
            this.colTDSPartiesName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // lcgTDSParties
            // 
            resources.ApplyResources(this.lcgTDSParties, "lcgTDSParties");
            this.lcgTDSParties.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgTDSParties.GroupBordersVisible = false;
            this.lcgTDSParties.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
            this.lcgTDSParties.Location = new System.Drawing.Point(0, 0);
            this.lcgTDSParties.Name = "lcgTDSParties";
            this.lcgTDSParties.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgTDSParties.Size = new System.Drawing.Size(492, 278);
            this.lcgTDSParties.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup3.AppearanceGroup.Font")));
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup3, "layoutControlGroup3");
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lclTDSParties,
            this.layoutControlItem19});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup3.Size = new System.Drawing.Size(482, 268);
            // 
            // lclTDSParties
            // 
            this.lclTDSParties.Control = this.gcTDSParties;
            resources.ApplyResources(this.lclTDSParties, "lclTDSParties");
            this.lclTDSParties.Location = new System.Drawing.Point(0, 0);
            this.lclTDSParties.Name = "lclTDSParties";
            this.lclTDSParties.Size = new System.Drawing.Size(470, 213);
            this.lclTDSParties.TextSize = new System.Drawing.Size(0, 0);
            this.lclTDSParties.TextToControlDistance = 0;
            this.lclTDSParties.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.chkPartyFilter;
            resources.ApplyResources(this.layoutControlItem19, "layoutControlItem19");
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 213);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(470, 23);
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // xtpNarration
            // 
            this.xtpNarration.Controls.Add(this.lcNarration);
            this.xtpNarration.Name = "xtpNarration";
            this.xtpNarration.PageVisible = false;
            resources.ApplyResources(this.xtpNarration, "xtpNarration");
            // 
            // lcNarration
            // 
            this.lcNarration.Controls.Add(this.chkNarration);
            this.lcNarration.Controls.Add(this.gcNarration);
            resources.ApplyResources(this.lcNarration, "lcNarration");
            this.lcNarration.Name = "lcNarration";
            this.lcNarration.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(609, 119, 250, 350);
            this.lcNarration.Root = this.lcgNarration;
            // 
            // chkNarration
            // 
            resources.ApplyResources(this.chkNarration, "chkNarration");
            this.chkNarration.Name = "chkNarration";
            this.chkNarration.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkNarration.Properties.Caption = resources.GetString("chkNarration.Properties.Caption");
            this.chkNarration.StyleController = this.lcNarration;
            this.chkNarration.CheckedChanged += new System.EventHandler(this.chkNarration_CheckedChanged);
            // 
            // gcNarration
            // 
            resources.ApplyResources(this.gcNarration, "gcNarration");
            this.gcNarration.MainView = this.gvNarration;
            this.gcNarration.Name = "gcNarration";
            this.gcNarration.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkNarration});
            this.gcNarration.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNarration});
            // 
            // gvNarration
            // 
            this.gvNarration.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvNarration.Appearance.HeaderPanel.Font")));
            this.gvNarration.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvNarration.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNarrationID,
            this.colNarrationCheck,
            this.colNarration,
            this.colPaymentcode,
            this.colDescription});
            this.gvNarration.GridControl = this.gcNarration;
            this.gvNarration.Name = "gvNarration";
            this.gvNarration.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvNarration.OptionsView.ShowGroupPanel = false;
            this.gvNarration.OptionsView.ShowIndicator = false;
            this.gvNarration.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvNarration_CustomRowCellEdit);
            this.gvNarration.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvNarration_MouseDown);
            this.gvNarration.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvNarration_MouseUp);
            // 
            // colNarrationID
            // 
            resources.ApplyResources(this.colNarrationID, "colNarrationID");
            this.colNarrationID.FieldName = "NATURE_PAY_ID";
            this.colNarrationID.Name = "colNarrationID";
            this.colNarrationID.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colNarrationCheck
            // 
            this.colNarrationCheck.ColumnEdit = this.rchkNarration;
            this.colNarrationCheck.FieldName = "SELECT";
            this.colNarrationCheck.Name = "colNarrationCheck";
            this.colNarrationCheck.OptionsColumn.AllowMove = false;
            this.colNarrationCheck.OptionsColumn.AllowSize = false;
            this.colNarrationCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNarrationCheck.OptionsColumn.ShowCaption = false;
            resources.ApplyResources(this.colNarrationCheck, "colNarrationCheck");
            // 
            // rchkNarration
            // 
            resources.ApplyResources(this.rchkNarration, "rchkNarration");
            this.rchkNarration.Name = "rchkNarration";
            this.rchkNarration.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkNarration.ValueChecked = 1;
            this.rchkNarration.ValueUnchecked = 0;
            // 
            // colNarration
            // 
            resources.ApplyResources(this.colNarration, "colNarration");
            this.colNarration.FieldName = "NAME";
            this.colNarration.Name = "colNarration";
            this.colNarration.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPaymentcode
            // 
            resources.ApplyResources(this.colPaymentcode, "colPaymentcode");
            this.colPaymentcode.FieldName = "PAYMENT_CODE";
            this.colPaymentcode.Name = "colPaymentcode";
            // 
            // colDescription
            // 
            resources.ApplyResources(this.colDescription, "colDescription");
            this.colDescription.FieldName = "DESCRIPTION";
            this.colDescription.Name = "colDescription";
            // 
            // lcgNarration
            // 
            resources.ApplyResources(this.lcgNarration, "lcgNarration");
            this.lcgNarration.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgNarration.GroupBordersVisible = false;
            this.lcgNarration.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgGroupNarration});
            this.lcgNarration.Location = new System.Drawing.Point(0, 0);
            this.lcgNarration.Name = "lcgNarration";
            this.lcgNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgNarration.Size = new System.Drawing.Size(492, 278);
            this.lcgNarration.TextVisible = false;
            // 
            // lcgGroupNarration
            // 
            this.lcgGroupNarration.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgGroupNarration.AppearanceGroup.Font")));
            this.lcgGroupNarration.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgGroupNarration, "lcgGroupNarration");
            this.lcgGroupNarration.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblNarrationGrid,
            this.layoutControlItem12});
            this.lcgGroupNarration.Location = new System.Drawing.Point(0, 0);
            this.lcgGroupNarration.Name = "lcgGroupNarration";
            this.lcgGroupNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgGroupNarration.Size = new System.Drawing.Size(492, 278);
            this.lcgGroupNarration.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            // 
            // lblNarrationGrid
            // 
            this.lblNarrationGrid.Control = this.gcNarration;
            resources.ApplyResources(this.lblNarrationGrid, "lblNarrationGrid");
            this.lblNarrationGrid.Location = new System.Drawing.Point(0, 0);
            this.lblNarrationGrid.Name = "lblNarrationGrid";
            this.lblNarrationGrid.Size = new System.Drawing.Size(470, 214);
            this.lblNarrationGrid.TextSize = new System.Drawing.Size(0, 0);
            this.lblNarrationGrid.TextToControlDistance = 0;
            this.lblNarrationGrid.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.chkNarration;
            resources.ApplyResources(this.layoutControlItem12, "layoutControlItem12");
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(470, 23);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // xrtabDeducteeType
            // 
            this.xrtabDeducteeType.Controls.Add(this.lcDeducteeType);
            this.xrtabDeducteeType.Name = "xrtabDeducteeType";
            this.xrtabDeducteeType.PageVisible = false;
            resources.ApplyResources(this.xrtabDeducteeType, "xrtabDeducteeType");
            // 
            // lcDeducteeType
            // 
            this.lcDeducteeType.Controls.Add(this.chkShowFilter);
            this.lcDeducteeType.Controls.Add(this.gcDeducteeType);
            resources.ApplyResources(this.lcDeducteeType, "lcDeducteeType");
            this.lcDeducteeType.Name = "lcDeducteeType";
            this.lcDeducteeType.Root = this.lcgDeducteeType;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.lcDeducteeType;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcDeducteeType
            // 
            resources.ApplyResources(this.gcDeducteeType, "gcDeducteeType");
            this.gcDeducteeType.MainView = this.gvDeducteeType;
            this.gcDeducteeType.Name = "gcDeducteeType";
            this.gcDeducteeType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkDeducteeType});
            this.gcDeducteeType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDeducteeType});
            // 
            // gvDeducteeType
            // 
            this.gvDeducteeType.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvDeducteeType.Appearance.FocusedRow.Font")));
            this.gvDeducteeType.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDeducteeType.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvDeducteeType.Appearance.HeaderPanel.Font")));
            this.gvDeducteeType.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDeducteeType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDeducteeTypeId,
            this.colDeductee,
            this.colDeducteeName});
            this.gvDeducteeType.GridControl = this.gcDeducteeType;
            this.gvDeducteeType.Name = "gvDeducteeType";
            this.gvDeducteeType.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDeducteeType.OptionsView.ShowGroupPanel = false;
            this.gvDeducteeType.OptionsView.ShowIndicator = false;
            this.gvDeducteeType.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvDeducteeType_CustomRowCellEdit);
            this.gvDeducteeType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvDeducteeType_MouseDown);
            this.gvDeducteeType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvDeducteeType_MouseUp);
            // 
            // colDeducteeTypeId
            // 
            resources.ApplyResources(this.colDeducteeTypeId, "colDeducteeTypeId");
            this.colDeducteeTypeId.FieldName = "DEDUCTEE_TYPE_ID";
            this.colDeducteeTypeId.Name = "colDeducteeTypeId";
            // 
            // colDeductee
            // 
            resources.ApplyResources(this.colDeductee, "colDeductee");
            this.colDeductee.ColumnEdit = this.rchkDeducteeType;
            this.colDeductee.FieldName = "SELECT";
            this.colDeductee.Name = "colDeductee";
            this.colDeductee.OptionsColumn.AllowMove = false;
            this.colDeductee.OptionsColumn.AllowSize = false;
            this.colDeductee.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDeductee.OptionsColumn.FixedWidth = true;
            this.colDeductee.OptionsColumn.ShowCaption = false;
            this.colDeductee.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDeductee.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rchkDeducteeType
            // 
            resources.ApplyResources(this.rchkDeducteeType, "rchkDeducteeType");
            this.rchkDeducteeType.Name = "rchkDeducteeType";
            this.rchkDeducteeType.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkDeducteeType.ValueChecked = 1;
            this.rchkDeducteeType.ValueGrayed = 2;
            this.rchkDeducteeType.ValueUnchecked = 0;
            // 
            // colDeducteeName
            // 
            resources.ApplyResources(this.colDeducteeName, "colDeducteeName");
            this.colDeducteeName.FieldName = "NAME";
            this.colDeducteeName.Name = "colDeducteeName";
            this.colDeducteeName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // lcgDeducteeType
            // 
            resources.ApplyResources(this.lcgDeducteeType, "lcgDeducteeType");
            this.lcgDeducteeType.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgDeducteeType.GroupBordersVisible = false;
            this.lcgDeducteeType.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgGroup});
            this.lcgDeducteeType.Location = new System.Drawing.Point(0, 0);
            this.lcgDeducteeType.Name = "lcgDeducteeType";
            this.lcgDeducteeType.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcgDeducteeType.Size = new System.Drawing.Size(492, 278);
            this.lcgDeducteeType.TextVisible = false;
            // 
            // lcgGroup
            // 
            this.lcgGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgGroup.AppearanceGroup.Font")));
            this.lcgGroup.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgGroup, "lcgGroup");
            this.lcgGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem17,
            this.layoutControlItem18});
            this.lcgGroup.Location = new System.Drawing.Point(0, 0);
            this.lcgGroup.Name = "lcgGroup";
            this.lcgGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgGroup.Size = new System.Drawing.Size(486, 272);
            this.lcgGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgGroup.TextLocation = DevExpress.Utils.Locations.Default;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.gcDeducteeType;
            resources.ApplyResources(this.layoutControlItem17, "layoutControlItem17");
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(474, 218);
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem18, "layoutControlItem18");
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 218);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(474, 23);
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // xtpPayroll
            // 
            this.xtpPayroll.Controls.Add(this.layoutControl1);
            this.xtpPayroll.Name = "xtpPayroll";
            this.xtpPayroll.PageVisible = false;
            resources.ApplyResources(this.xtpPayroll, "xtpPayroll");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkpPayroll);
            this.layoutControl1.Controls.Add(this.gcGroups);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(730, 178, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup4;
            // 
            // glkpPayroll
            // 
            resources.ApplyResources(this.glkpPayroll, "glkpPayroll");
            this.glkpPayroll.Name = "glkpPayroll";
            this.glkpPayroll.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpPayroll.Properties.Buttons"))))});
            this.glkpPayroll.Properties.PopupFormMinSize = new System.Drawing.Size(226, 20);
            this.glkpPayroll.Properties.PopupFormSize = new System.Drawing.Size(226, 110);
            this.glkpPayroll.Properties.View = this.gridView3;
            this.glkpPayroll.StyleController = this.layoutControl1;
            this.glkpPayroll.EditValueChanged += new System.EventHandler(this.glkpPayroll_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView3.Appearance.FocusedRow.Font")));
            this.gridView3.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPayrollId,
            this.colPayrollName});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowColumnHeaders = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // colPayrollId
            // 
            resources.ApplyResources(this.colPayrollId, "colPayrollId");
            this.colPayrollId.FieldName = "PAYROLLID";
            this.colPayrollId.Name = "colPayrollId";
            // 
            // colPayrollName
            // 
            resources.ApplyResources(this.colPayrollName, "colPayrollName");
            this.colPayrollName.FieldName = "PRNAME";
            this.colPayrollName.Name = "colPayrollName";
            // 
            // gcGroups
            // 
            resources.ApplyResources(this.gcGroups, "gcGroups");
            this.gcGroups.MainView = this.gvGroups;
            this.gcGroups.Name = "gcGroups";
            this.gcGroups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkPayrollCom});
            this.gcGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGroups});
            // 
            // gvGroups
            // 
            this.gvGroups.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvGroups.Appearance.FocusedRow.Font")));
            this.gvGroups.Appearance.FocusedRow.Options.UseFont = true;
            this.gvGroups.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvGroups.Appearance.HeaderPanel.Font")));
            this.gvGroups.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGid,
            this.colPayroll,
            this.colGrpName});
            this.gvGroups.GridControl = this.gcGroups;
            this.gvGroups.Name = "gvGroups";
            this.gvGroups.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvGroups.OptionsView.ShowGroupPanel = false;
            this.gvGroups.OptionsView.ShowIndicator = false;
            this.gvGroups.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvGroups_CustomRowCellEdit);
            this.gvGroups.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvGroups_MouseDown);
            this.gvGroups.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvGroups_MouseUp);
            // 
            // colGid
            // 
            resources.ApplyResources(this.colGid, "colGid");
            this.colGid.FieldName = "GROUPID";
            this.colGid.Name = "colGid";
            this.colGid.OptionsColumn.AllowSize = false;
            // 
            // colPayroll
            // 
            resources.ApplyResources(this.colPayroll, "colPayroll");
            this.colPayroll.ColumnEdit = this.rchkPayrollCom;
            this.colPayroll.FieldName = "SELECT";
            this.colPayroll.Name = "colPayroll";
            this.colPayroll.OptionsColumn.AllowMove = false;
            this.colPayroll.OptionsColumn.AllowSize = false;
            this.colPayroll.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayroll.OptionsColumn.FixedWidth = true;
            this.colPayroll.OptionsColumn.ShowCaption = false;
            this.colPayroll.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rchkPayrollCom
            // 
            resources.ApplyResources(this.rchkPayrollCom, "rchkPayrollCom");
            this.rchkPayrollCom.Name = "rchkPayrollCom";
            this.rchkPayrollCom.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkPayrollCom.ValueChecked = 1;
            this.rchkPayrollCom.ValueGrayed = 2;
            this.rchkPayrollCom.ValueUnchecked = 0;
            // 
            // colGrpName
            // 
            resources.ApplyResources(this.colGrpName, "colGrpName");
            this.colGrpName.FieldName = "GROUPNAME";
            this.colGrpName.Name = "colGrpName";
            this.colGrpName.OptionsColumn.AllowEdit = false;
            this.colGrpName.OptionsColumn.AllowSize = false;
            this.colGrpName.OptionsColumn.ReadOnly = true;
            // 
            // layoutControlGroup4
            // 
            resources.ApplyResources(this.layoutControlGroup4, "layoutControlGroup4");
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgPayrollGroup,
            this.emptySpaceItem13,
            this.lcPayroll});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup4.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // lcgPayrollGroup
            // 
            this.lcgPayrollGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgPayrollGroup.AppearanceGroup.Font")));
            this.lcgPayrollGroup.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgPayrollGroup, "lcgPayrollGroup");
            this.lcgPayrollGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem23});
            this.lcgPayrollGroup.Location = new System.Drawing.Point(0, 26);
            this.lcgPayrollGroup.Name = "lcgPayrollGroup";
            this.lcgPayrollGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgPayrollGroup.Size = new System.Drawing.Size(482, 242);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.gcGroups;
            resources.ApplyResources(this.layoutControlItem23, "layoutControlItem23");
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(476, 217);
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextToControlDistance = 0;
            this.layoutControlItem23.TextVisible = false;
            // 
            // emptySpaceItem13
            // 
            this.emptySpaceItem13.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem13, "emptySpaceItem13");
            this.emptySpaceItem13.Location = new System.Drawing.Point(287, 0);
            this.emptySpaceItem13.Name = "emptySpaceItem13";
            this.emptySpaceItem13.Size = new System.Drawing.Size(195, 26);
            this.emptySpaceItem13.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcPayroll
            // 
            this.lcPayroll.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcPayroll.AppearanceItemCaption.Font")));
            this.lcPayroll.AppearanceItemCaption.Options.UseFont = true;
            this.lcPayroll.Control = this.glkpPayroll;
            resources.ApplyResources(this.lcPayroll, "lcPayroll");
            this.lcPayroll.Location = new System.Drawing.Point(0, 0);
            this.lcPayroll.Name = "lcPayroll";
            this.lcPayroll.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcPayroll.Size = new System.Drawing.Size(287, 26);
            this.lcPayroll.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcPayroll.TextSize = new System.Drawing.Size(50, 20);
            this.lcPayroll.TextToControlDistance = 5;
            // 
            // xtpPayrollComponent
            // 
            this.xtpPayrollComponent.Controls.Add(this.layoutControl2);
            this.xtpPayrollComponent.Name = "xtpPayrollComponent";
            this.xtpPayrollComponent.PageVisible = false;
            resources.ApplyResources(this.xtpPayrollComponent, "xtpPayrollComponent");
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcPayStaff);
            this.layoutControl2.Controls.Add(this.gcPayComponent);
            resources.ApplyResources(this.layoutControl2, "layoutControl2");
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup6;
            // 
            // gcPayStaff
            // 
            resources.ApplyResources(this.gcPayStaff, "gcPayStaff");
            this.gcPayStaff.MainView = this.gvPayrollStaff;
            this.gcPayStaff.Name = "gcPayStaff";
            this.gcPayStaff.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkPayrollStaff});
            this.gcPayStaff.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPayrollStaff});
            // 
            // gvPayrollStaff
            // 
            this.gvPayrollStaff.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStaffId,
            this.colPayrollStaff,
            this.colStaffName});
            this.gvPayrollStaff.GridControl = this.gcPayStaff;
            this.gvPayrollStaff.Name = "gvPayrollStaff";
            this.gvPayrollStaff.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPayrollStaff.OptionsView.ShowGroupPanel = false;
            this.gvPayrollStaff.OptionsView.ShowIndicator = false;
            this.gvPayrollStaff.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvPayrollStaff_CustomRowCellEdit);
            this.gvPayrollStaff.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvPayrollStaff_MouseDown);
            this.gvPayrollStaff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvPayrollStaff_MouseUp);
            // 
            // colStaffId
            // 
            resources.ApplyResources(this.colStaffId, "colStaffId");
            this.colStaffId.FieldName = "STAFFID";
            this.colStaffId.Name = "colStaffId";
            // 
            // colPayrollStaff
            // 
            resources.ApplyResources(this.colPayrollStaff, "colPayrollStaff");
            this.colPayrollStaff.ColumnEdit = this.rchkPayrollStaff;
            this.colPayrollStaff.FieldName = "SELECT";
            this.colPayrollStaff.Name = "colPayrollStaff";
            this.colPayrollStaff.OptionsColumn.AllowMove = false;
            this.colPayrollStaff.OptionsColumn.AllowSize = false;
            this.colPayrollStaff.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayrollStaff.OptionsColumn.FixedWidth = true;
            this.colPayrollStaff.OptionsColumn.ShowCaption = false;
            this.colPayrollStaff.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rchkPayrollStaff
            // 
            resources.ApplyResources(this.rchkPayrollStaff, "rchkPayrollStaff");
            this.rchkPayrollStaff.Name = "rchkPayrollStaff";
            this.rchkPayrollStaff.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkPayrollStaff.ValueChecked = 1;
            this.rchkPayrollStaff.ValueGrayed = 2;
            this.rchkPayrollStaff.ValueUnchecked = 0;
            // 
            // colStaffName
            // 
            this.colStaffName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStaffName.AppearanceHeader.Font")));
            this.colStaffName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStaffName, "colStaffName");
            this.colStaffName.FieldName = "STAFFNAME";
            this.colStaffName.Name = "colStaffName";
            this.colStaffName.OptionsColumn.AllowEdit = false;
            // 
            // gcPayComponent
            // 
            resources.ApplyResources(this.gcPayComponent, "gcPayComponent");
            this.gcPayComponent.MainView = this.gvPayComponent;
            this.gcPayComponent.Name = "gcPayComponent";
            this.gcPayComponent.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkPayroll});
            this.gcPayComponent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPayComponent});
            // 
            // gvPayComponent
            // 
            this.gvPayComponent.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPayrollComponent,
            this.colSelect,
            this.colComponent});
            this.gvPayComponent.GridControl = this.gcPayComponent;
            this.gvPayComponent.Name = "gvPayComponent";
            this.gvPayComponent.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPayComponent.OptionsView.ShowGroupPanel = false;
            this.gvPayComponent.OptionsView.ShowIndicator = false;
            this.gvPayComponent.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvPayComponent_CustomRowCellEdit);
            this.gvPayComponent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvPayComponent_MouseDown);
            this.gvPayComponent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvPayComponent_MouseUp);
            // 
            // colPayrollComponent
            // 
            resources.ApplyResources(this.colPayrollComponent, "colPayrollComponent");
            this.colPayrollComponent.FieldName = "COMPONENTID";
            this.colPayrollComponent.Name = "colPayrollComponent";
            // 
            // colSelect
            // 
            resources.ApplyResources(this.colSelect, "colSelect");
            this.colSelect.ColumnEdit = this.rchkPayroll;
            this.colSelect.FieldName = "SELECT";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.AllowMove = false;
            this.colSelect.OptionsColumn.AllowSize = false;
            this.colSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSelect.OptionsColumn.FixedWidth = true;
            this.colSelect.OptionsColumn.ShowCaption = false;
            this.colSelect.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rchkPayroll
            // 
            resources.ApplyResources(this.rchkPayroll, "rchkPayroll");
            this.rchkPayroll.Name = "rchkPayroll";
            this.rchkPayroll.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkPayroll.ValueChecked = 1;
            this.rchkPayroll.ValueGrayed = 2;
            this.rchkPayroll.ValueUnchecked = 0;
            // 
            // colComponent
            // 
            this.colComponent.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colComponent.AppearanceHeader.Font")));
            this.colComponent.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colComponent, "colComponent");
            this.colComponent.FieldName = "COMPONENT";
            this.colComponent.Name = "colComponent";
            this.colComponent.OptionsColumn.AllowEdit = false;
            // 
            // layoutControlGroup6
            // 
            resources.ApplyResources(this.layoutControlGroup6, "layoutControlGroup6");
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.GroupBordersVisible = false;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem15,
            this.lcgComponent,
            this.lcgPayRollStaff});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // emptySpaceItem15
            // 
            this.emptySpaceItem15.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem15, "emptySpaceItem15");
            this.emptySpaceItem15.Location = new System.Drawing.Point(0, 249);
            this.emptySpaceItem15.Name = "emptySpaceItem15";
            this.emptySpaceItem15.Size = new System.Drawing.Size(492, 29);
            this.emptySpaceItem15.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgComponent
            // 
            this.lcgComponent.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgComponent.AppearanceGroup.Font")));
            this.lcgComponent.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgComponent, "lcgComponent");
            this.lcgComponent.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem24});
            this.lcgComponent.Location = new System.Drawing.Point(0, 0);
            this.lcgComponent.Name = "lcgComponent";
            this.lcgComponent.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgComponent.Size = new System.Drawing.Size(246, 249);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.gcPayComponent;
            resources.ApplyResources(this.layoutControlItem24, "layoutControlItem24");
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(240, 224);
            this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem24.TextToControlDistance = 0;
            this.layoutControlItem24.TextVisible = false;
            // 
            // lcgPayRollStaff
            // 
            this.lcgPayRollStaff.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgPayRollStaff.AppearanceGroup.Font")));
            this.lcgPayRollStaff.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgPayRollStaff, "lcgPayRollStaff");
            this.lcgPayRollStaff.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem25});
            this.lcgPayRollStaff.Location = new System.Drawing.Point(246, 0);
            this.lcgPayRollStaff.Name = "lcgPayRollStaff";
            this.lcgPayRollStaff.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgPayRollStaff.Size = new System.Drawing.Size(246, 249);
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.gcPayStaff;
            resources.ApplyResources(this.layoutControlItem25, "layoutControlItem25");
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(240, 224);
            this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem25.TextToControlDistance = 0;
            this.layoutControlItem25.TextVisible = false;
            // 
            // xtpItem
            // 
            this.xtpItem.Controls.Add(this.layoutControl3);
            this.xtpItem.Name = "xtpItem";
            this.xtpItem.PageVisible = false;
            resources.ApplyResources(this.xtpItem, "xtpItem");
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.chkStockShowFilter);
            this.layoutControl3.Controls.Add(this.gcItem);
            resources.ApplyResources(this.layoutControl3, "layoutControl3");
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(747, 159, 250, 350);
            this.layoutControl3.Root = this.layoutControlGroup7;
            // 
            // chkStockShowFilter
            // 
            resources.ApplyResources(this.chkStockShowFilter, "chkStockShowFilter");
            this.chkStockShowFilter.Name = "chkStockShowFilter";
            this.chkStockShowFilter.Properties.Caption = resources.GetString("chkStockShowFilter.Properties.Caption");
            this.chkStockShowFilter.StyleController = this.layoutControl3;
            this.chkStockShowFilter.CheckedChanged += new System.EventHandler(this.chkStockShowFilter_CheckedChanged);
            // 
            // gcItem
            // 
            resources.ApplyResources(this.gcItem, "gcItem");
            this.gcItem.MainView = this.gvItem;
            this.gcItem.Name = "gcItem";
            this.gcItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkStockSelect});
            this.gcItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItem});
            // 
            // gvItem
            // 
            this.gvItem.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvItem.Appearance.FocusedRow.Font")));
            this.gvItem.Appearance.FocusedRow.Options.UseFont = true;
            this.gvItem.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvItem.Appearance.HeaderPanel.Font")));
            this.gvItem.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemId,
            this.colItemName,
            this.colStockSelect});
            this.gvItem.GridControl = this.gcItem;
            this.gvItem.Name = "gvItem";
            this.gvItem.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvItem.OptionsView.ShowGroupPanel = false;
            this.gvItem.OptionsView.ShowIndicator = false;
            // 
            // colItemId
            // 
            resources.ApplyResources(this.colItemId, "colItemId");
            this.colItemId.FieldName = "ITEM_ID";
            this.colItemId.Name = "colItemId";
            // 
            // colItemName
            // 
            resources.ApplyResources(this.colItemName, "colItemName");
            this.colItemName.FieldName = "ITEM_NAME";
            this.colItemName.Name = "colItemName";
            this.colItemName.OptionsColumn.AllowEdit = false;
            // 
            // colStockSelect
            // 
            resources.ApplyResources(this.colStockSelect, "colStockSelect");
            this.colStockSelect.ColumnEdit = this.rchkStockSelect;
            this.colStockSelect.FieldName = "SELECT";
            this.colStockSelect.Name = "colStockSelect";
            this.colStockSelect.OptionsColumn.AllowMove = false;
            this.colStockSelect.OptionsColumn.AllowSize = false;
            this.colStockSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStockSelect.OptionsColumn.ShowCaption = false;
            // 
            // rchkStockSelect
            // 
            resources.ApplyResources(this.rchkStockSelect, "rchkStockSelect");
            this.rchkStockSelect.Name = "rchkStockSelect";
            this.rchkStockSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkStockSelect.ValueChecked = 1;
            this.rchkStockSelect.ValueGrayed = 2;
            this.rchkStockSelect.ValueUnchecked = 0;
            // 
            // layoutControlGroup7
            // 
            resources.ApplyResources(this.layoutControlGroup7, "layoutControlGroup7");
            this.layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup7.GroupBordersVisible = false;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgStockItem});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup7.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup7.TextVisible = false;
            // 
            // lcgStockItem
            // 
            this.lcgStockItem.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgStockItem.AppearanceGroup.Font")));
            this.lcgStockItem.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgStockItem, "lcgStockItem");
            this.lcgStockItem.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem26,
            this.layoutControlItem27});
            this.lcgStockItem.Location = new System.Drawing.Point(0, 0);
            this.lcgStockItem.Name = "lcgStockItem";
            this.lcgStockItem.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcgStockItem.Size = new System.Drawing.Size(482, 268);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.gcItem;
            resources.ApplyResources(this.layoutControlItem26, "layoutControlItem26");
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(470, 212);
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextToControlDistance = 0;
            this.layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.chkStockShowFilter;
            resources.ApplyResources(this.layoutControlItem27, "layoutControlItem27");
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 212);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(470, 23);
            this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem27.TextToControlDistance = 0;
            this.layoutControlItem27.TextVisible = false;
            // 
            // xtpLocation
            // 
            this.xtpLocation.Controls.Add(this.layoutControl4);
            this.xtpLocation.Name = "xtpLocation";
            this.xtpLocation.PageVisible = false;
            resources.ApplyResources(this.xtpLocation, "xtpLocation");
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.chkLocationShowFilter);
            this.layoutControl4.Controls.Add(this.gcLocation);
            resources.ApplyResources(this.layoutControl4, "layoutControl4");
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup9;
            // 
            // chkLocationShowFilter
            // 
            resources.ApplyResources(this.chkLocationShowFilter, "chkLocationShowFilter");
            this.chkLocationShowFilter.Name = "chkLocationShowFilter";
            this.chkLocationShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkLocationShowFilter.Properties.Caption = resources.GetString("chkLocationShowFilter.Properties.Caption");
            this.chkLocationShowFilter.StyleController = this.layoutControl4;
            this.chkLocationShowFilter.CheckedChanged += new System.EventHandler(this.chkLocationShowFilter_CheckedChanged);
            // 
            // gcLocation
            // 
            resources.ApplyResources(this.gcLocation, "gcLocation");
            this.gcLocation.MainView = this.gvLocation;
            this.gcLocation.Name = "gcLocation";
            this.gcLocation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkLocationSelect});
            this.gcLocation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLocation});
            // 
            // gvLocation
            // 
            this.gvLocation.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLocation.Appearance.FocusedRow.Font")));
            this.gvLocation.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLocation.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLocationId,
            this.colLocationName,
            this.colLocationSelect});
            this.gvLocation.GridControl = this.gcLocation;
            this.gvLocation.Name = "gvLocation";
            this.gvLocation.OptionsView.ShowGroupPanel = false;
            this.gvLocation.OptionsView.ShowIndicator = false;
            // 
            // colLocationId
            // 
            resources.ApplyResources(this.colLocationId, "colLocationId");
            this.colLocationId.FieldName = "LOCATION_ID";
            this.colLocationId.Name = "colLocationId";
            // 
            // colLocationName
            // 
            this.colLocationName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLocationName.AppearanceHeader.Font")));
            this.colLocationName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLocationName, "colLocationName");
            this.colLocationName.FieldName = "LOCATION";
            this.colLocationName.Name = "colLocationName";
            // 
            // colLocationSelect
            // 
            resources.ApplyResources(this.colLocationSelect, "colLocationSelect");
            this.colLocationSelect.ColumnEdit = this.rchkLocationSelect;
            this.colLocationSelect.FieldName = "SELECT";
            this.colLocationSelect.Name = "colLocationSelect";
            this.colLocationSelect.OptionsColumn.AllowMove = false;
            this.colLocationSelect.OptionsColumn.AllowSize = false;
            this.colLocationSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLocationSelect.OptionsColumn.ShowCaption = false;
            // 
            // rchkLocationSelect
            // 
            resources.ApplyResources(this.rchkLocationSelect, "rchkLocationSelect");
            this.rchkLocationSelect.Name = "rchkLocationSelect";
            this.rchkLocationSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkLocationSelect.ValueChecked = 1;
            this.rchkLocationSelect.ValueUnchecked = 0;
            // 
            // layoutControlGroup9
            // 
            resources.ApplyResources(this.layoutControlGroup9, "layoutControlGroup9");
            this.layoutControlGroup9.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup9.GroupBordersVisible = false;
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lgGroup});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup9.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup9.TextVisible = false;
            // 
            // lgGroup
            // 
            this.lgGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lgGroup.AppearanceGroup.Font")));
            this.lgGroup.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lgGroup, "lgGroup");
            this.lgGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem28,
            this.emptySpaceItem11,
            this.layoutControlItem29});
            this.lgGroup.Location = new System.Drawing.Point(0, 0);
            this.lgGroup.Name = "lgGroup";
            this.lgGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lgGroup.Size = new System.Drawing.Size(486, 272);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.gcLocation;
            resources.ApplyResources(this.layoutControlItem28, "layoutControlItem28");
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(480, 224);
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextToControlDistance = 0;
            this.layoutControlItem28.TextVisible = false;
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem11, "emptySpaceItem11");
            this.emptySpaceItem11.Location = new System.Drawing.Point(81, 224);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Size = new System.Drawing.Size(399, 23);
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.chkLocationShowFilter;
            resources.ApplyResources(this.layoutControlItem29, "layoutControlItem29");
            this.layoutControlItem29.Location = new System.Drawing.Point(0, 224);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(81, 23);
            this.layoutControlItem29.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem29.TextToControlDistance = 0;
            this.layoutControlItem29.TextVisible = false;
            // 
            // xtbDynamicConditions
            // 
            this.xtbDynamicConditions.Controls.Add(this.layoutControl5);
            this.xtbDynamicConditions.Name = "xtbDynamicConditions";
            this.xtbDynamicConditions.PageVisible = false;
            resources.ApplyResources(this.xtbDynamicConditions, "xtbDynamicConditions");
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.txtAmount);
            this.layoutControl5.Controls.Add(this.cboCondition);
            resources.ApplyResources(this.layoutControl5, "layoutControl5");
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(803, 159, 250, 350);
            this.layoutControl5.Root = this.layoutControlGroup10;
            // 
            // txtAmount
            // 
            resources.ApplyResources(this.txtAmount, "txtAmount");
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAmount.Properties.Mask.EditMask = resources.GetString("txtAmount.Properties.Mask.EditMask");
            this.txtAmount.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtAmount.Properties.Mask.MaskType")));
            this.txtAmount.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtAmount.Properties.Mask.UseMaskAsDisplayFormat")));
            this.txtAmount.StyleController = this.layoutControl5;
            // 
            // cboCondition
            // 
            resources.ApplyResources(this.cboCondition, "cboCondition");
            this.cboCondition.Name = "cboCondition";
            this.cboCondition.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboCondition.Properties.Buttons"))))});
            this.cboCondition.Properties.Items.AddRange(new object[] {
            resources.GetString("cboCondition.Properties.Items"),
            resources.GetString("cboCondition.Properties.Items1"),
            resources.GetString("cboCondition.Properties.Items2"),
            resources.GetString("cboCondition.Properties.Items3"),
            resources.GetString("cboCondition.Properties.Items4"),
            resources.GetString("cboCondition.Properties.Items5"),
            resources.GetString("cboCondition.Properties.Items6")});
            this.cboCondition.StyleController = this.layoutControl5;
            // 
            // layoutControlGroup10
            // 
            resources.ApplyResources(this.layoutControlGroup10, "layoutControlGroup10");
            this.layoutControlGroup10.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup10.GroupBordersVisible = false;
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem14,
            this.emptySpaceItem16,
            this.emptySpaceItem17,
            this.layoutControlItem32,
            this.layoutControlItem31});
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup10.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup10.TextVisible = false;
            // 
            // emptySpaceItem14
            // 
            this.emptySpaceItem14.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem14, "emptySpaceItem14");
            this.emptySpaceItem14.Location = new System.Drawing.Point(381, 0);
            this.emptySpaceItem14.Name = "emptySpaceItem14";
            this.emptySpaceItem14.Size = new System.Drawing.Size(101, 24);
            this.emptySpaceItem14.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem16
            // 
            this.emptySpaceItem16.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem16, "emptySpaceItem16");
            this.emptySpaceItem16.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem16.Name = "emptySpaceItem16";
            this.emptySpaceItem16.Size = new System.Drawing.Size(482, 244);
            this.emptySpaceItem16.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem17
            // 
            this.emptySpaceItem17.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem17, "emptySpaceItem17");
            this.emptySpaceItem17.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem17.Name = "emptySpaceItem17";
            this.emptySpaceItem17.Size = new System.Drawing.Size(58, 24);
            this.emptySpaceItem17.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.cboCondition;
            resources.ApplyResources(this.layoutControlItem32, "layoutControlItem32");
            this.layoutControlItem32.Location = new System.Drawing.Point(58, 0);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(222, 24);
            this.layoutControlItem32.TextSize = new System.Drawing.Size(37, 13);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.txtAmount;
            resources.ApplyResources(this.layoutControlItem31, "layoutControlItem31");
            this.layoutControlItem31.Location = new System.Drawing.Point(280, 0);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(101, 24);
            this.layoutControlItem31.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem31.TextToControlDistance = 0;
            this.layoutControlItem31.TextVisible = false;
            // 
            // xtpBankFDAccount
            // 
            this.xtpBankFDAccount.Controls.Add(this.layoutControl6);
            this.xtpBankFDAccount.Name = "xtpBankFDAccount";
            this.xtpBankFDAccount.PageVisible = false;
            resources.ApplyResources(this.xtpBankFDAccount, "xtpBankFDAccount");
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.chkbankFDFilter);
            this.layoutControl6.Controls.Add(this.gcBankFDAccounts);
            resources.ApplyResources(this.layoutControl6, "layoutControl6");
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(790, 159, 250, 350);
            this.layoutControl6.Root = this.layoutControlGroup11;
            // 
            // chkbankFDFilter
            // 
            resources.ApplyResources(this.chkbankFDFilter, "chkbankFDFilter");
            this.chkbankFDFilter.Name = "chkbankFDFilter";
            this.chkbankFDFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkbankFDFilter.Properties.Caption = resources.GetString("chkbankFDFilter.Properties.Caption");
            this.chkbankFDFilter.StyleController = this.layoutControl6;
            this.chkbankFDFilter.CheckedChanged += new System.EventHandler(this.chkbankFDFilter_CheckedChanged);
            // 
            // gcBankFDAccounts
            // 
            resources.ApplyResources(this.gcBankFDAccounts, "gcBankFDAccounts");
            this.gcBankFDAccounts.MainView = this.gvBankFDAccounts;
            this.gcBankFDAccounts.Name = "gcBankFDAccounts";
            this.gcBankFDAccounts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBankFDAccounts});
            // 
            // gvBankFDAccounts
            // 
            this.gvBankFDAccounts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolaccBankName,
            this.gcolaccFDAccountId,
            this.gcolAccType,
            this.gcolAccLedgerId});
            this.gvBankFDAccounts.GridControl = this.gcBankFDAccounts;
            this.gvBankFDAccounts.GroupCount = 1;
            resources.ApplyResources(this.gvBankFDAccounts, "gvBankFDAccounts");
            this.gvBankFDAccounts.Name = "gvBankFDAccounts";
            this.gvBankFDAccounts.OptionsFind.AllowFindPanel = false;
            this.gvBankFDAccounts.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvBankFDAccounts.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBankFDAccounts.OptionsSelection.MultiSelect = true;
            this.gvBankFDAccounts.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvBankFDAccounts.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvBankFDAccounts.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.gvBankFDAccounts.OptionsView.ShowColumnHeaders = false;
            this.gvBankFDAccounts.OptionsView.ShowGroupPanel = false;
            this.gvBankFDAccounts.OptionsView.ShowIndicator = false;
            this.gvBankFDAccounts.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolAccType, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gcolaccBankName
            // 
            resources.ApplyResources(this.gcolaccBankName, "gcolaccBankName");
            this.gcolaccBankName.FieldName = "BANK";
            this.gcolaccBankName.Name = "gcolaccBankName";
            this.gcolaccBankName.OptionsColumn.AllowEdit = false;
            this.gcolaccBankName.OptionsColumn.AllowFocus = false;
            // 
            // gcolaccFDAccountId
            // 
            resources.ApplyResources(this.gcolaccFDAccountId, "gcolaccFDAccountId");
            this.gcolaccFDAccountId.FieldName = "FD_ACCOUNT_ID";
            this.gcolaccFDAccountId.Name = "gcolaccFDAccountId";
            // 
            // gcolAccType
            // 
            resources.ApplyResources(this.gcolAccType, "gcolAccType");
            this.gcolAccType.FieldName = "TYPE_NAME";
            this.gcolAccType.Name = "gcolAccType";
            this.gcolAccType.OptionsColumn.AllowEdit = false;
            this.gcolAccType.OptionsColumn.AllowFocus = false;
            // 
            // gcolAccLedgerId
            // 
            resources.ApplyResources(this.gcolAccLedgerId, "gcolAccLedgerId");
            this.gcolAccLedgerId.FieldName = "LEDGER_ID";
            this.gcolAccLedgerId.Name = "gcolAccLedgerId";
            // 
            // layoutControlGroup11
            // 
            resources.ApplyResources(this.layoutControlGroup11, "layoutControlGroup11");
            this.layoutControlGroup11.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup11.GroupBordersVisible = false;
            this.layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup12});
            this.layoutControlGroup11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup11.Name = "layoutControlGroup11";
            this.layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup11.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup11.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            this.layoutControlGroup12.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup12.AppearanceGroup.Font")));
            this.layoutControlGroup12.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup12, "layoutControlGroup12");
            this.layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem36,
            this.emptySpaceItem4,
            this.layoutControlItem37});
            this.layoutControlGroup12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup12.Name = "layoutControlGroup12";
            this.layoutControlGroup12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup12.Size = new System.Drawing.Size(488, 274);
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.gcBankFDAccounts;
            resources.ApplyResources(this.layoutControlItem36, "layoutControlItem36");
            this.layoutControlItem36.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(482, 226);
            this.layoutControlItem36.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem36.TextToControlDistance = 0;
            this.layoutControlItem36.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(83, 226);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(399, 23);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem37
            // 
            this.layoutControlItem37.Control = this.chkbankFDFilter;
            resources.ApplyResources(this.layoutControlItem37, "layoutControlItem37");
            this.layoutControlItem37.Location = new System.Drawing.Point(0, 226);
            this.layoutControlItem37.Name = "layoutControlItem37";
            this.layoutControlItem37.Size = new System.Drawing.Size(83, 23);
            this.layoutControlItem37.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem37.TextToControlDistance = 0;
            this.layoutControlItem37.TextVisible = false;
            // 
            // xtpAssetClass
            // 
            this.xtpAssetClass.Controls.Add(this.layoutControl7);
            this.xtpAssetClass.Name = "xtpAssetClass";
            this.xtpAssetClass.PageVisible = false;
            resources.ApplyResources(this.xtpAssetClass, "xtpAssetClass");
            // 
            // layoutControl7
            // 
            this.layoutControl7.Controls.Add(this.chkAssetShowFilter);
            this.layoutControl7.Controls.Add(this.gcAssetClass);
            resources.ApplyResources(this.layoutControl7, "layoutControl7");
            this.layoutControl7.Name = "layoutControl7";
            this.layoutControl7.Root = this.layoutControlGroup13;
            // 
            // chkAssetShowFilter
            // 
            resources.ApplyResources(this.chkAssetShowFilter, "chkAssetShowFilter");
            this.chkAssetShowFilter.Name = "chkAssetShowFilter";
            this.chkAssetShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkAssetShowFilter.Properties.Caption = resources.GetString("chkAssetShowFilter.Properties.Caption");
            this.chkAssetShowFilter.StyleController = this.layoutControl7;
            this.chkAssetShowFilter.CheckedChanged += new System.EventHandler(this.chkAssetShowFilter_CheckedChanged);
            // 
            // gcAssetClass
            // 
            resources.ApplyResources(this.gcAssetClass, "gcAssetClass");
            this.gcAssetClass.MainView = this.gvAssetClass;
            this.gcAssetClass.Name = "gcAssetClass";
            this.gcAssetClass.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkAssetclass});
            this.gcAssetClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetClass});
            // 
            // gvAssetClass
            // 
            this.gvAssetClass.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetClass.Appearance.HeaderPanel.Font")));
            this.gvAssetClass.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetClass.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAssetClassId,
            this.colChkSelect,
            this.colAssetClass});
            this.gvAssetClass.GridControl = this.gcAssetClass;
            this.gvAssetClass.Name = "gvAssetClass";
            this.gvAssetClass.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetClass.OptionsView.ShowGroupPanel = false;
            this.gvAssetClass.OptionsView.ShowIndicator = false;
            // 
            // colAssetClassId
            // 
            resources.ApplyResources(this.colAssetClassId, "colAssetClassId");
            this.colAssetClassId.FieldName = "ASSET_CLASS_ID";
            this.colAssetClassId.Name = "colAssetClassId";
            // 
            // colChkSelect
            // 
            resources.ApplyResources(this.colChkSelect, "colChkSelect");
            this.colChkSelect.ColumnEdit = this.rchkAssetclass;
            this.colChkSelect.FieldName = "SELECT";
            this.colChkSelect.Name = "colChkSelect";
            this.colChkSelect.OptionsColumn.AllowMove = false;
            this.colChkSelect.OptionsColumn.AllowSize = false;
            this.colChkSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colChkSelect.OptionsColumn.FixedWidth = true;
            this.colChkSelect.OptionsColumn.ShowCaption = false;
            this.colChkSelect.OptionsFilter.AllowAutoFilter = false;
            this.colChkSelect.OptionsFilter.AllowFilter = false;
            this.colChkSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkAssetclass
            // 
            this.rchkAssetclass.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkAssetclass, "rchkAssetclass");
            this.rchkAssetclass.Name = "rchkAssetclass";
            this.rchkAssetclass.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkAssetclass.ValueChecked = 1;
            this.rchkAssetclass.ValueGrayed = "2";
            this.rchkAssetclass.ValueUnchecked = 0;
            // 
            // colAssetClass
            // 
            resources.ApplyResources(this.colAssetClass, "colAssetClass");
            this.colAssetClass.FieldName = "ASSET_CLASS";
            this.colAssetClass.Name = "colAssetClass";
            this.colAssetClass.OptionsColumn.AllowEdit = false;
            this.colAssetClass.OptionsColumn.AllowFocus = false;
            this.colAssetClass.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // layoutControlGroup13
            // 
            resources.ApplyResources(this.layoutControlGroup13, "layoutControlGroup13");
            this.layoutControlGroup13.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup13.GroupBordersVisible = false;
            this.layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgAssetClass});
            this.layoutControlGroup13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup13.Name = "layoutControlGroup13";
            this.layoutControlGroup13.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup13.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup13.TextVisible = false;
            // 
            // lcgAssetClass
            // 
            this.lcgAssetClass.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgAssetClass.AppearanceGroup.Font")));
            this.lcgAssetClass.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgAssetClass, "lcgAssetClass");
            this.lcgAssetClass.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem38,
            this.layoutControlItem39});
            this.lcgAssetClass.Location = new System.Drawing.Point(0, 0);
            this.lcgAssetClass.Name = "lcgAssetClass";
            this.lcgAssetClass.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgAssetClass.Size = new System.Drawing.Size(492, 278);
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.Control = this.gcAssetClass;
            resources.ApplyResources(this.layoutControlItem38, "layoutControlItem38");
            this.layoutControlItem38.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Size = new System.Drawing.Size(486, 230);
            this.layoutControlItem38.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem38.TextToControlDistance = 0;
            this.layoutControlItem38.TextVisible = false;
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.Control = this.chkAssetShowFilter;
            resources.ApplyResources(this.layoutControlItem39, "layoutControlItem39");
            this.layoutControlItem39.Location = new System.Drawing.Point(0, 230);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Size = new System.Drawing.Size(486, 23);
            this.layoutControlItem39.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem39.TextToControlDistance = 0;
            this.layoutControlItem39.TextVisible = false;
            // 
            // xtbRegistrationType
            // 
            this.xtbRegistrationType.Controls.Add(this.layoutControl8);
            this.xtbRegistrationType.Name = "xtbRegistrationType";
            this.xtbRegistrationType.PageVisible = false;
            resources.ApplyResources(this.xtbRegistrationType, "xtbRegistrationType");
            // 
            // layoutControl8
            // 
            this.layoutControl8.Controls.Add(this.chkShowRegistrationFilter);
            this.layoutControl8.Controls.Add(this.gcRegistrationType);
            resources.ApplyResources(this.layoutControl8, "layoutControl8");
            this.layoutControl8.Name = "layoutControl8";
            this.layoutControl8.Root = this.layoutControlGroup14;
            // 
            // chkShowRegistrationFilter
            // 
            resources.ApplyResources(this.chkShowRegistrationFilter, "chkShowRegistrationFilter");
            this.chkShowRegistrationFilter.Name = "chkShowRegistrationFilter";
            this.chkShowRegistrationFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowRegistrationFilter.Properties.Caption = resources.GetString("chkShowRegistrationFilter.Properties.Caption");
            this.chkShowRegistrationFilter.StyleController = this.layoutControl8;
            this.chkShowRegistrationFilter.CheckedChanged += new System.EventHandler(this.chkShowRegistrationFilter_CheckedChanged);
            // 
            // gcRegistrationType
            // 
            resources.ApplyResources(this.gcRegistrationType, "gcRegistrationType");
            this.gcRegistrationType.MainView = this.gvRegistrationType;
            this.gcRegistrationType.Name = "gcRegistrationType";
            this.gcRegistrationType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkSelectRegisterType});
            this.gcRegistrationType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRegistrationType});
            // 
            // gvRegistrationType
            // 
            this.gvRegistrationType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRegistrationTypeId,
            this.colSelectReg,
            this.colRegistrationType});
            this.gvRegistrationType.GridControl = this.gcRegistrationType;
            this.gvRegistrationType.Name = "gvRegistrationType";
            this.gvRegistrationType.OptionsView.ShowGroupPanel = false;
            this.gvRegistrationType.OptionsView.ShowIndicator = false;
            // 
            // colRegistrationTypeId
            // 
            resources.ApplyResources(this.colRegistrationTypeId, "colRegistrationTypeId");
            this.colRegistrationTypeId.FieldName = "REGISTRATION_TYPE_ID";
            this.colRegistrationTypeId.Name = "colRegistrationTypeId";
            // 
            // colSelectReg
            // 
            resources.ApplyResources(this.colSelectReg, "colSelectReg");
            this.colSelectReg.ColumnEdit = this.rchkSelectRegisterType;
            this.colSelectReg.FieldName = "SELECT";
            this.colSelectReg.Name = "colSelectReg";
            this.colSelectReg.OptionsColumn.AllowMove = false;
            this.colSelectReg.OptionsColumn.AllowSize = false;
            this.colSelectReg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSelectReg.OptionsColumn.FixedWidth = true;
            this.colSelectReg.OptionsColumn.ShowCaption = false;
            this.colSelectReg.OptionsFilter.AllowAutoFilter = false;
            this.colSelectReg.OptionsFilter.AllowFilter = false;
            this.colSelectReg.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkSelectRegisterType
            // 
            this.rchkSelectRegisterType.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkSelectRegisterType, "rchkSelectRegisterType");
            this.rchkSelectRegisterType.Name = "rchkSelectRegisterType";
            this.rchkSelectRegisterType.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkSelectRegisterType.ValueChecked = 1;
            this.rchkSelectRegisterType.ValueGrayed = 2;
            this.rchkSelectRegisterType.ValueUnchecked = 0;
            // 
            // colRegistrationType
            // 
            this.colRegistrationType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRegistrationType.AppearanceHeader.Font")));
            this.colRegistrationType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRegistrationType, "colRegistrationType");
            this.colRegistrationType.FieldName = "REGISTRATION_TYPE";
            this.colRegistrationType.Name = "colRegistrationType";
            // 
            // layoutControlGroup14
            // 
            resources.ApplyResources(this.layoutControlGroup14, "layoutControlGroup14");
            this.layoutControlGroup14.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup14.GroupBordersVisible = false;
            this.layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgRegistrationType});
            this.layoutControlGroup14.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup14.Name = "layoutControlGroup14";
            this.layoutControlGroup14.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup14.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup14.TextVisible = false;
            // 
            // lcgRegistrationType
            // 
            this.lcgRegistrationType.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgRegistrationType.AppearanceGroup.Font")));
            this.lcgRegistrationType.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgRegistrationType, "lcgRegistrationType");
            this.lcgRegistrationType.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem40,
            this.layoutControlItem43});
            this.lcgRegistrationType.Location = new System.Drawing.Point(0, 0);
            this.lcgRegistrationType.Name = "lcgRegistrationType";
            this.lcgRegistrationType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgRegistrationType.Size = new System.Drawing.Size(492, 278);
            // 
            // layoutControlItem40
            // 
            this.layoutControlItem40.Control = this.gcRegistrationType;
            resources.ApplyResources(this.layoutControlItem40, "layoutControlItem40");
            this.layoutControlItem40.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem40.Name = "layoutControlItem40";
            this.layoutControlItem40.Size = new System.Drawing.Size(486, 230);
            this.layoutControlItem40.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem40.TextToControlDistance = 0;
            this.layoutControlItem40.TextVisible = false;
            // 
            // layoutControlItem43
            // 
            this.layoutControlItem43.Control = this.chkShowRegistrationFilter;
            resources.ApplyResources(this.layoutControlItem43, "layoutControlItem43");
            this.layoutControlItem43.Location = new System.Drawing.Point(0, 230);
            this.layoutControlItem43.Name = "layoutControlItem43";
            this.layoutControlItem43.Size = new System.Drawing.Size(486, 23);
            this.layoutControlItem43.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem43.TextToControlDistance = 0;
            this.layoutControlItem43.TextVisible = false;
            // 
            // xtbCountry
            // 
            this.xtbCountry.Controls.Add(this.layoutControl9);
            this.xtbCountry.Name = "xtbCountry";
            this.xtbCountry.PageVisible = false;
            resources.ApplyResources(this.xtbCountry, "xtbCountry");
            // 
            // layoutControl9
            // 
            this.layoutControl9.Controls.Add(this.chkShowStateFilter);
            this.layoutControl9.Controls.Add(this.chkShowCountryFilter);
            this.layoutControl9.Controls.Add(this.gcState);
            this.layoutControl9.Controls.Add(this.gcCountry);
            resources.ApplyResources(this.layoutControl9, "layoutControl9");
            this.layoutControl9.Name = "layoutControl9";
            this.layoutControl9.Root = this.layoutControlGroup15;
            // 
            // chkShowStateFilter
            // 
            resources.ApplyResources(this.chkShowStateFilter, "chkShowStateFilter");
            this.chkShowStateFilter.Name = "chkShowStateFilter";
            this.chkShowStateFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowStateFilter.Properties.Caption = resources.GetString("chkShowStateFilter.Properties.Caption");
            this.chkShowStateFilter.StyleController = this.layoutControl9;
            this.chkShowStateFilter.CheckedChanged += new System.EventHandler(this.chkShowStateFilter_CheckedChanged);
            // 
            // chkShowCountryFilter
            // 
            resources.ApplyResources(this.chkShowCountryFilter, "chkShowCountryFilter");
            this.chkShowCountryFilter.Name = "chkShowCountryFilter";
            this.chkShowCountryFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowCountryFilter.Properties.Caption = resources.GetString("chkShowCountryFilter.Properties.Caption");
            this.chkShowCountryFilter.StyleController = this.layoutControl9;
            this.chkShowCountryFilter.CheckedChanged += new System.EventHandler(this.chkShowCountryFilter_CheckedChanged);
            // 
            // gcState
            // 
            resources.ApplyResources(this.gcState, "gcState");
            this.gcState.MainView = this.gvState;
            this.gcState.Name = "gcState";
            this.gcState.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkStateSelect});
            this.gcState.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvState});
            // 
            // gvState
            // 
            this.gvState.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStateId,
            this.colStateSelect,
            this.colState});
            this.gvState.GridControl = this.gcState;
            this.gvState.Name = "gvState";
            this.gvState.OptionsView.ShowGroupPanel = false;
            this.gvState.OptionsView.ShowIndicator = false;
            // 
            // colStateId
            // 
            resources.ApplyResources(this.colStateId, "colStateId");
            this.colStateId.FieldName = "STATE_ID";
            this.colStateId.Name = "colStateId";
            // 
            // colStateSelect
            // 
            resources.ApplyResources(this.colStateSelect, "colStateSelect");
            this.colStateSelect.ColumnEdit = this.rchkStateSelect;
            this.colStateSelect.FieldName = "SELECT";
            this.colStateSelect.Name = "colStateSelect";
            this.colStateSelect.OptionsColumn.AllowMove = false;
            this.colStateSelect.OptionsColumn.AllowSize = false;
            this.colStateSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStateSelect.OptionsColumn.FixedWidth = true;
            this.colStateSelect.OptionsColumn.ShowCaption = false;
            this.colStateSelect.OptionsFilter.AllowAutoFilter = false;
            this.colStateSelect.OptionsFilter.AllowFilter = false;
            this.colStateSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkStateSelect
            // 
            this.rchkStateSelect.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkStateSelect, "rchkStateSelect");
            this.rchkStateSelect.Name = "rchkStateSelect";
            this.rchkStateSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkStateSelect.ValueChecked = 1;
            this.rchkStateSelect.ValueGrayed = 2;
            this.rchkStateSelect.ValueUnchecked = 0;
            // 
            // colState
            // 
            this.colState.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colState.AppearanceHeader.Font")));
            this.colState.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colState, "colState");
            this.colState.FieldName = "STATE_NAME";
            this.colState.Name = "colState";
            // 
            // gcCountry
            // 
            resources.ApplyResources(this.gcCountry, "gcCountry");
            this.gcCountry.MainView = this.gvCountry;
            this.gcCountry.Name = "gcCountry";
            this.gcCountry.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkCountrySelect});
            this.gcCountry.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCountry});
            // 
            // gvCountry
            // 
            this.gvCountry.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCountryId,
            this.colCountryCheck,
            this.colCountry});
            this.gvCountry.GridControl = this.gcCountry;
            this.gvCountry.Name = "gvCountry";
            this.gvCountry.OptionsView.ShowGroupPanel = false;
            this.gvCountry.OptionsView.ShowIndicator = false;
            this.gvCountry.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvCountry_CellValueChanged);
            this.gvCountry.Click += new System.EventHandler(this.gvCountry_Click);
            // 
            // colCountryId
            // 
            resources.ApplyResources(this.colCountryId, "colCountryId");
            this.colCountryId.FieldName = "COUNTRY_ID";
            this.colCountryId.Name = "colCountryId";
            // 
            // colCountryCheck
            // 
            resources.ApplyResources(this.colCountryCheck, "colCountryCheck");
            this.colCountryCheck.ColumnEdit = this.rchkCountrySelect;
            this.colCountryCheck.FieldName = "SELECT";
            this.colCountryCheck.Name = "colCountryCheck";
            this.colCountryCheck.OptionsColumn.AllowMove = false;
            this.colCountryCheck.OptionsColumn.AllowSize = false;
            this.colCountryCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCountryCheck.OptionsColumn.FixedWidth = true;
            this.colCountryCheck.OptionsColumn.ShowCaption = false;
            this.colCountryCheck.OptionsFilter.AllowAutoFilter = false;
            this.colCountryCheck.OptionsFilter.AllowFilter = false;
            this.colCountryCheck.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkCountrySelect
            // 
            this.rchkCountrySelect.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkCountrySelect, "rchkCountrySelect");
            this.rchkCountrySelect.Name = "rchkCountrySelect";
            this.rchkCountrySelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkCountrySelect.ValueChecked = 1;
            this.rchkCountrySelect.ValueGrayed = 2;
            this.rchkCountrySelect.ValueUnchecked = 0;
            this.rchkCountrySelect.CheckedChanged += new System.EventHandler(this.rchkCountrySelect_CheckedChanged);
            // 
            // colCountry
            // 
            this.colCountry.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCountry.AppearanceHeader.Font")));
            this.colCountry.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCountry, "colCountry");
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            this.colCountry.OptionsColumn.AllowEdit = false;
            this.colCountry.OptionsColumn.AllowFocus = false;
            // 
            // layoutControlGroup15
            // 
            resources.ApplyResources(this.layoutControlGroup15, "layoutControlGroup15");
            this.layoutControlGroup15.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup15.GroupBordersVisible = false;
            this.layoutControlGroup15.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgCountry,
            this.lcgState});
            this.layoutControlGroup15.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup15.Name = "layoutControlGroup15";
            this.layoutControlGroup15.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup15.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup15.TextVisible = false;
            // 
            // lcgCountry
            // 
            this.lcgCountry.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgCountry.AppearanceGroup.Font")));
            this.lcgCountry.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgCountry, "lcgCountry");
            this.lcgCountry.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem41,
            this.layoutControlItem44});
            this.lcgCountry.Location = new System.Drawing.Point(0, 0);
            this.lcgCountry.Name = "lcgCountry";
            this.lcgCountry.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgCountry.Size = new System.Drawing.Size(246, 278);
            // 
            // layoutControlItem41
            // 
            this.layoutControlItem41.Control = this.gcCountry;
            resources.ApplyResources(this.layoutControlItem41, "layoutControlItem41");
            this.layoutControlItem41.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem41.Name = "layoutControlItem41";
            this.layoutControlItem41.Size = new System.Drawing.Size(240, 230);
            this.layoutControlItem41.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem41.TextToControlDistance = 0;
            this.layoutControlItem41.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            this.layoutControlItem44.Control = this.chkShowCountryFilter;
            resources.ApplyResources(this.layoutControlItem44, "layoutControlItem44");
            this.layoutControlItem44.Location = new System.Drawing.Point(0, 230);
            this.layoutControlItem44.Name = "layoutControlItem44";
            this.layoutControlItem44.Size = new System.Drawing.Size(240, 23);
            this.layoutControlItem44.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem44.TextToControlDistance = 0;
            this.layoutControlItem44.TextVisible = false;
            // 
            // lcgState
            // 
            this.lcgState.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgState.AppearanceGroup.Font")));
            this.lcgState.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgState, "lcgState");
            this.lcgState.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem42,
            this.layoutControlItem45});
            this.lcgState.Location = new System.Drawing.Point(246, 0);
            this.lcgState.Name = "lcgState";
            this.lcgState.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgState.Size = new System.Drawing.Size(246, 278);
            // 
            // layoutControlItem42
            // 
            this.layoutControlItem42.Control = this.gcState;
            resources.ApplyResources(this.layoutControlItem42, "layoutControlItem42");
            this.layoutControlItem42.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem42.Name = "layoutControlItem42";
            this.layoutControlItem42.Size = new System.Drawing.Size(240, 230);
            this.layoutControlItem42.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem42.TextToControlDistance = 0;
            this.layoutControlItem42.TextVisible = false;
            // 
            // layoutControlItem45
            // 
            this.layoutControlItem45.Control = this.chkShowStateFilter;
            resources.ApplyResources(this.layoutControlItem45, "layoutControlItem45");
            this.layoutControlItem45.Location = new System.Drawing.Point(0, 230);
            this.layoutControlItem45.Name = "layoutControlItem45";
            this.layoutControlItem45.Size = new System.Drawing.Size(240, 23);
            this.layoutControlItem45.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem45.TextToControlDistance = 0;
            this.layoutControlItem45.TextVisible = false;
            // 
            // xtpLanguage
            // 
            this.xtpLanguage.Controls.Add(this.layoutControl10);
            this.xtpLanguage.Name = "xtpLanguage";
            this.xtpLanguage.PageVisible = false;
            resources.ApplyResources(this.xtpLanguage, "xtpLanguage");
            // 
            // layoutControl10
            // 
            this.layoutControl10.Controls.Add(this.chkShowLanguageFilter);
            this.layoutControl10.Controls.Add(this.gcLanguage);
            resources.ApplyResources(this.layoutControl10, "layoutControl10");
            this.layoutControl10.Name = "layoutControl10";
            this.layoutControl10.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(547, 153, 250, 350);
            this.layoutControl10.Root = this.layoutControlGroup16;
            // 
            // chkShowLanguageFilter
            // 
            resources.ApplyResources(this.chkShowLanguageFilter, "chkShowLanguageFilter");
            this.chkShowLanguageFilter.Name = "chkShowLanguageFilter";
            this.chkShowLanguageFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowLanguageFilter.Properties.Caption = resources.GetString("chkShowLanguageFilter.Properties.Caption");
            this.chkShowLanguageFilter.StyleController = this.layoutControl10;
            this.chkShowLanguageFilter.CheckedChanged += new System.EventHandler(this.chkShowLanguageFilter_CheckedChanged);
            // 
            // gcLanguage
            // 
            resources.ApplyResources(this.gcLanguage, "gcLanguage");
            this.gcLanguage.MainView = this.gvLanguage;
            this.gcLanguage.Name = "gcLanguage";
            this.gcLanguage.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkLanguage});
            this.gcLanguage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLanguage});
            // 
            // gvLanguage
            // 
            this.gvLanguage.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheckLanguage,
            this.colLanguage});
            this.gvLanguage.GridControl = this.gcLanguage;
            this.gvLanguage.Name = "gvLanguage";
            this.gvLanguage.OptionsView.ShowGroupPanel = false;
            this.gvLanguage.OptionsView.ShowIndicator = false;
            // 
            // colCheckLanguage
            // 
            resources.ApplyResources(this.colCheckLanguage, "colCheckLanguage");
            this.colCheckLanguage.ColumnEdit = this.rchkLanguage;
            this.colCheckLanguage.FieldName = "SELECT";
            this.colCheckLanguage.Name = "colCheckLanguage";
            this.colCheckLanguage.OptionsColumn.AllowMove = false;
            this.colCheckLanguage.OptionsColumn.AllowSize = false;
            this.colCheckLanguage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCheckLanguage.OptionsColumn.FixedWidth = true;
            this.colCheckLanguage.OptionsColumn.ShowCaption = false;
            this.colCheckLanguage.OptionsFilter.AllowAutoFilter = false;
            this.colCheckLanguage.OptionsFilter.AllowFilter = false;
            this.colCheckLanguage.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkLanguage
            // 
            this.rchkLanguage.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkLanguage, "rchkLanguage");
            this.rchkLanguage.Name = "rchkLanguage";
            this.rchkLanguage.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkLanguage.ValueChecked = 1;
            this.rchkLanguage.ValueGrayed = 2;
            this.rchkLanguage.ValueUnchecked = 0;
            // 
            // colLanguage
            // 
            this.colLanguage.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLanguage.AppearanceHeader.Font")));
            this.colLanguage.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLanguage, "colLanguage");
            this.colLanguage.FieldName = "LANGUAGE";
            this.colLanguage.Name = "colLanguage";
            this.colLanguage.OptionsColumn.AllowEdit = false;
            this.colLanguage.OptionsColumn.AllowFocus = false;
            // 
            // layoutControlGroup16
            // 
            resources.ApplyResources(this.layoutControlGroup16, "layoutControlGroup16");
            this.layoutControlGroup16.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup16.GroupBordersVisible = false;
            this.layoutControlGroup16.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem18,
            this.lcgLanguage,
            this.layoutControlItem47});
            this.layoutControlGroup16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup16.Name = "layoutControlGroup16";
            this.layoutControlGroup16.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup16.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup16.TextVisible = false;
            // 
            // emptySpaceItem18
            // 
            this.emptySpaceItem18.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem18, "emptySpaceItem18");
            this.emptySpaceItem18.Location = new System.Drawing.Point(205, 255);
            this.emptySpaceItem18.Name = "emptySpaceItem18";
            this.emptySpaceItem18.Size = new System.Drawing.Size(287, 23);
            this.emptySpaceItem18.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgLanguage
            // 
            this.lcgLanguage.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgLanguage.AppearanceGroup.Font")));
            this.lcgLanguage.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgLanguage, "lcgLanguage");
            this.lcgLanguage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem46});
            this.lcgLanguage.Location = new System.Drawing.Point(0, 0);
            this.lcgLanguage.Name = "lcgLanguage";
            this.lcgLanguage.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgLanguage.Size = new System.Drawing.Size(492, 255);
            // 
            // layoutControlItem46
            // 
            this.layoutControlItem46.Control = this.gcLanguage;
            resources.ApplyResources(this.layoutControlItem46, "layoutControlItem46");
            this.layoutControlItem46.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem46.Name = "layoutControlItem46";
            this.layoutControlItem46.Size = new System.Drawing.Size(486, 230);
            this.layoutControlItem46.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem46.TextToControlDistance = 0;
            this.layoutControlItem46.TextVisible = false;
            // 
            // layoutControlItem47
            // 
            this.layoutControlItem47.Control = this.chkShowLanguageFilter;
            resources.ApplyResources(this.layoutControlItem47, "layoutControlItem47");
            this.layoutControlItem47.Location = new System.Drawing.Point(0, 255);
            this.layoutControlItem47.Name = "layoutControlItem47";
            this.layoutControlItem47.Size = new System.Drawing.Size(205, 23);
            this.layoutControlItem47.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem47.TextToControlDistance = 0;
            this.layoutControlItem47.TextVisible = false;
            // 
            // xtpStateDonaud
            // 
            this.xtpStateDonaud.Controls.Add(this.layoutControl11);
            this.xtpStateDonaud.Name = "xtpStateDonaud";
            this.xtpStateDonaud.PageVisible = false;
            resources.ApplyResources(this.xtpStateDonaud, "xtpStateDonaud");
            // 
            // layoutControl11
            // 
            this.layoutControl11.Controls.Add(this.chkShowState);
            this.layoutControl11.Controls.Add(this.gcStateDonaud);
            resources.ApplyResources(this.layoutControl11, "layoutControl11");
            this.layoutControl11.Name = "layoutControl11";
            this.layoutControl11.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(547, 153, 250, 350);
            this.layoutControl11.Root = this.layoutControlGroup17;
            // 
            // chkShowState
            // 
            resources.ApplyResources(this.chkShowState, "chkShowState");
            this.chkShowState.Name = "chkShowState";
            this.chkShowState.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowState.Properties.Caption = resources.GetString("chkShowState.Properties.Caption");
            this.chkShowState.StyleController = this.layoutControl11;
            this.chkShowState.CheckedChanged += new System.EventHandler(this.chkShowState_CheckedChanged);
            // 
            // gcStateDonaud
            // 
            resources.ApplyResources(this.gcStateDonaud, "gcStateDonaud");
            this.gcStateDonaud.MainView = this.gvStateDonaud;
            this.gcStateDonaud.Name = "gcStateDonaud";
            this.gcStateDonaud.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchStateCheck});
            this.gcStateDonaud.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStateDonaud});
            // 
            // gvStateDonaud
            // 
            this.gvStateDonaud.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStateDonauId,
            this.colCheckState,
            this.colStateDonaudName});
            this.gvStateDonaud.GridControl = this.gcStateDonaud;
            this.gvStateDonaud.Name = "gvStateDonaud";
            this.gvStateDonaud.OptionsView.ShowGroupPanel = false;
            this.gvStateDonaud.OptionsView.ShowIndicator = false;
            this.gvStateDonaud.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvStateDonaud_CellValueChanged);
            this.gvStateDonaud.Click += new System.EventHandler(this.gvStateDonaud_Click);
            // 
            // colStateDonauId
            // 
            resources.ApplyResources(this.colStateDonauId, "colStateDonauId");
            this.colStateDonauId.FieldName = "STATE_ID";
            this.colStateDonauId.Name = "colStateDonauId";
            // 
            // colCheckState
            // 
            resources.ApplyResources(this.colCheckState, "colCheckState");
            this.colCheckState.ColumnEdit = this.rchStateCheck;
            this.colCheckState.FieldName = "SELECT";
            this.colCheckState.Name = "colCheckState";
            this.colCheckState.OptionsColumn.AllowMove = false;
            this.colCheckState.OptionsColumn.AllowSize = false;
            this.colCheckState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCheckState.OptionsColumn.FixedWidth = true;
            this.colCheckState.OptionsColumn.ShowCaption = false;
            this.colCheckState.OptionsFilter.AllowAutoFilter = false;
            this.colCheckState.OptionsFilter.AllowFilter = false;
            this.colCheckState.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchStateCheck
            // 
            this.rchStateCheck.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchStateCheck, "rchStateCheck");
            this.rchStateCheck.Name = "rchStateCheck";
            this.rchStateCheck.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchStateCheck.ValueChecked = 1;
            this.rchStateCheck.ValueGrayed = 2;
            this.rchStateCheck.ValueUnchecked = 0;
            this.rchStateCheck.Click += new System.EventHandler(this.rchStateCheck_Click);
            // 
            // colStateDonaudName
            // 
            this.colStateDonaudName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStateDonaudName.AppearanceHeader.Font")));
            this.colStateDonaudName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStateDonaudName, "colStateDonaudName");
            this.colStateDonaudName.FieldName = "STATE_NAME";
            this.colStateDonaudName.Name = "colStateDonaudName";
            this.colStateDonaudName.OptionsColumn.AllowEdit = false;
            this.colStateDonaudName.OptionsColumn.AllowFocus = false;
            // 
            // layoutControlGroup17
            // 
            resources.ApplyResources(this.layoutControlGroup17, "layoutControlGroup17");
            this.layoutControlGroup17.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup17.GroupBordersVisible = false;
            this.layoutControlGroup17.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem20,
            this.lcgStatName,
            this.layoutControlItem50});
            this.layoutControlGroup17.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup17.Name = "layoutControlGroup17";
            this.layoutControlGroup17.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup17.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup17.TextVisible = false;
            // 
            // emptySpaceItem20
            // 
            this.emptySpaceItem20.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem20, "emptySpaceItem20");
            this.emptySpaceItem20.Location = new System.Drawing.Point(246, 255);
            this.emptySpaceItem20.Name = "emptySpaceItem20";
            this.emptySpaceItem20.Size = new System.Drawing.Size(246, 23);
            this.emptySpaceItem20.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgStatName
            // 
            this.lcgStatName.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgStatName.AppearanceGroup.Font")));
            this.lcgStatName.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgStatName, "lcgStatName");
            this.lcgStatName.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem48});
            this.lcgStatName.Location = new System.Drawing.Point(0, 0);
            this.lcgStatName.Name = "lcgStatName";
            this.lcgStatName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgStatName.Size = new System.Drawing.Size(492, 255);
            // 
            // layoutControlItem48
            // 
            this.layoutControlItem48.Control = this.gcStateDonaud;
            resources.ApplyResources(this.layoutControlItem48, "layoutControlItem48");
            this.layoutControlItem48.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem48.Name = "layoutControlItem48";
            this.layoutControlItem48.Size = new System.Drawing.Size(486, 230);
            this.layoutControlItem48.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem48.TextToControlDistance = 0;
            this.layoutControlItem48.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.chkShowState;
            resources.ApplyResources(this.layoutControlItem50, "layoutControlItem50");
            this.layoutControlItem50.Location = new System.Drawing.Point(0, 255);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(246, 23);
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextToControlDistance = 0;
            this.layoutControlItem50.TextVisible = false;
            // 
            // xtpDonaud
            // 
            this.xtpDonaud.Controls.Add(this.layoutControl12);
            this.xtpDonaud.Name = "xtpDonaud";
            this.xtpDonaud.PageVisible = false;
            resources.ApplyResources(this.xtpDonaud, "xtpDonaud");
            // 
            // layoutControl12
            // 
            this.layoutControl12.Controls.Add(this.chkShowDonaud);
            this.layoutControl12.Controls.Add(this.gcDonaud);
            resources.ApplyResources(this.layoutControl12, "layoutControl12");
            this.layoutControl12.Name = "layoutControl12";
            this.layoutControl12.Root = this.layoutControlGroup18;
            // 
            // chkShowDonaud
            // 
            resources.ApplyResources(this.chkShowDonaud, "chkShowDonaud");
            this.chkShowDonaud.Name = "chkShowDonaud";
            this.chkShowDonaud.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowDonaud.Properties.Caption = resources.GetString("chkShowDonaud.Properties.Caption");
            this.chkShowDonaud.StyleController = this.layoutControl12;
            this.chkShowDonaud.CheckedChanged += new System.EventHandler(this.chkShowDonaud_CheckedChanged);
            // 
            // gcDonaud
            // 
            resources.ApplyResources(this.gcDonaud, "gcDonaud");
            this.gcDonaud.MainView = this.gvDonaud;
            this.gcDonaud.Name = "gcDonaud";
            this.gcDonaud.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkDonaud});
            this.gcDonaud.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDonaud});
            // 
            // gvDonaud
            // 
            this.gvDonaud.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonaudId,
            this.colDonaudCheck,
            this.colDonaud});
            this.gvDonaud.GridControl = this.gcDonaud;
            this.gvDonaud.Name = "gvDonaud";
            this.gvDonaud.OptionsView.ShowGroupPanel = false;
            this.gvDonaud.OptionsView.ShowIndicator = false;
            // 
            // colDonaudId
            // 
            resources.ApplyResources(this.colDonaudId, "colDonaudId");
            this.colDonaudId.FieldName = "DONAUD_ID";
            this.colDonaudId.Name = "colDonaudId";
            // 
            // colDonaudCheck
            // 
            resources.ApplyResources(this.colDonaudCheck, "colDonaudCheck");
            this.colDonaudCheck.ColumnEdit = this.rchkDonaud;
            this.colDonaudCheck.FieldName = "SELECT";
            this.colDonaudCheck.Name = "colDonaudCheck";
            this.colDonaudCheck.OptionsColumn.AllowMove = false;
            this.colDonaudCheck.OptionsColumn.AllowSize = false;
            this.colDonaudCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDonaudCheck.OptionsColumn.FixedWidth = true;
            this.colDonaudCheck.OptionsColumn.ShowCaption = false;
            this.colDonaudCheck.OptionsFilter.AllowAutoFilter = false;
            this.colDonaudCheck.OptionsFilter.AllowFilter = false;
            this.colDonaudCheck.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkDonaud
            // 
            this.rchkDonaud.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkDonaud, "rchkDonaud");
            this.rchkDonaud.Name = "rchkDonaud";
            this.rchkDonaud.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkDonaud.ValueChecked = 1;
            this.rchkDonaud.ValueGrayed = 2;
            this.rchkDonaud.ValueUnchecked = 0;
            // 
            // colDonaud
            // 
            this.colDonaud.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDonaud.AppearanceHeader.Font")));
            this.colDonaud.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDonaud, "colDonaud");
            this.colDonaud.FieldName = "NAME";
            this.colDonaud.Name = "colDonaud";
            this.colDonaud.OptionsColumn.AllowEdit = false;
            this.colDonaud.OptionsColumn.AllowFocus = false;
            // 
            // layoutControlGroup18
            // 
            resources.ApplyResources(this.layoutControlGroup18, "layoutControlGroup18");
            this.layoutControlGroup18.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup18.GroupBordersVisible = false;
            this.layoutControlGroup18.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgDonar,
            this.layoutControlItem51});
            this.layoutControlGroup18.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup18.Name = "layoutControlGroup18";
            this.layoutControlGroup18.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup18.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup18.TextVisible = false;
            // 
            // lcgDonar
            // 
            this.lcgDonar.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgDonar.AppearanceGroup.Font")));
            this.lcgDonar.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgDonar, "lcgDonar");
            this.lcgDonar.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem49});
            this.lcgDonar.Location = new System.Drawing.Point(0, 0);
            this.lcgDonar.Name = "lcgDonar";
            this.lcgDonar.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgDonar.Size = new System.Drawing.Size(492, 255);
            // 
            // layoutControlItem49
            // 
            this.layoutControlItem49.Control = this.gcDonaud;
            resources.ApplyResources(this.layoutControlItem49, "layoutControlItem49");
            this.layoutControlItem49.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem49.Name = "layoutControlItem49";
            this.layoutControlItem49.Size = new System.Drawing.Size(486, 230);
            this.layoutControlItem49.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem49.TextToControlDistance = 0;
            this.layoutControlItem49.TextVisible = false;
            // 
            // layoutControlItem51
            // 
            this.layoutControlItem51.Control = this.chkShowDonaud;
            resources.ApplyResources(this.layoutControlItem51, "layoutControlItem51");
            this.layoutControlItem51.Location = new System.Drawing.Point(0, 255);
            this.layoutControlItem51.Name = "layoutControlItem51";
            this.layoutControlItem51.Size = new System.Drawing.Size(492, 23);
            this.layoutControlItem51.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem51.TextToControlDistance = 0;
            this.layoutControlItem51.TextVisible = false;
            // 
            // xtpFeestDayTask
            // 
            this.xtpFeestDayTask.Controls.Add(this.layoutControl13);
            this.xtpFeestDayTask.Name = "xtpFeestDayTask";
            this.xtpFeestDayTask.PageVisible = false;
            resources.ApplyResources(this.xtpFeestDayTask, "xtpFeestDayTask");
            // 
            // layoutControl13
            // 
            this.layoutControl13.Controls.Add(this.glkpFeestDatTask);
            resources.ApplyResources(this.layoutControl13, "layoutControl13");
            this.layoutControl13.Name = "layoutControl13";
            this.layoutControl13.Root = this.layoutControlGroup19;
            // 
            // glkpFeestDatTask
            // 
            resources.ApplyResources(this.glkpFeestDatTask, "glkpFeestDatTask");
            this.glkpFeestDatTask.Name = "glkpFeestDatTask";
            this.glkpFeestDatTask.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpFeestDatTask.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpFeestDatTask.Properties.Buttons"))))});
            this.glkpFeestDatTask.Properties.NullText = resources.GetString("glkpFeestDatTask.Properties.NullText");
            this.glkpFeestDatTask.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpFeestDatTask.Properties.View = this.gridView4;
            this.glkpFeestDatTask.StyleController = this.layoutControl13;
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTagID,
            this.colTagName});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowColumnHeaders = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.OptionsView.ShowIndicator = false;
            // 
            // colTagID
            // 
            resources.ApplyResources(this.colTagID, "colTagID");
            this.colTagID.FieldName = "TAG_ID";
            this.colTagID.Name = "colTagID";
            // 
            // colTagName
            // 
            resources.ApplyResources(this.colTagName, "colTagName");
            this.colTagName.FieldName = "TAG_NAME";
            this.colTagName.Name = "colTagName";
            // 
            // layoutControlGroup19
            // 
            resources.ApplyResources(this.layoutControlGroup19, "layoutControlGroup19");
            this.layoutControlGroup19.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup19.GroupBordersVisible = false;
            this.layoutControlGroup19.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblFeestDayTask,
            this.emptySpaceItem21});
            this.layoutControlGroup19.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup19.Name = "layoutControlGroup19";
            this.layoutControlGroup19.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup19.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup19.TextVisible = false;
            // 
            // lblFeestDayTask
            // 
            this.lblFeestDayTask.Control = this.glkpFeestDatTask;
            resources.ApplyResources(this.lblFeestDayTask, "lblFeestDayTask");
            this.lblFeestDayTask.Location = new System.Drawing.Point(0, 0);
            this.lblFeestDayTask.Name = "lblFeestDayTask";
            this.lblFeestDayTask.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lblFeestDayTask.Size = new System.Drawing.Size(492, 30);
            this.lblFeestDayTask.TextSize = new System.Drawing.Size(22, 13);
            // 
            // emptySpaceItem21
            // 
            this.emptySpaceItem21.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem21, "emptySpaceItem21");
            this.emptySpaceItem21.Location = new System.Drawing.Point(0, 30);
            this.emptySpaceItem21.Name = "emptySpaceItem21";
            this.emptySpaceItem21.Size = new System.Drawing.Size(492, 248);
            this.emptySpaceItem21.TextSize = new System.Drawing.Size(0, 0);
            // 
            // xtpNetworkingTasks
            // 
            this.xtpNetworkingTasks.Controls.Add(this.layoutControl17);
            this.xtpNetworkingTasks.Name = "xtpNetworkingTasks";
            this.xtpNetworkingTasks.PageVisible = false;
            resources.ApplyResources(this.xtpNetworkingTasks, "xtpNetworkingTasks");
            // 
            // layoutControl17
            // 
            this.layoutControl17.Controls.Add(this.gcTasks);
            this.layoutControl17.Controls.Add(this.chkTaskFilter);
            resources.ApplyResources(this.layoutControl17, "layoutControl17");
            this.layoutControl17.Name = "layoutControl17";
            this.layoutControl17.Root = this.layoutControlGroup23;
            // 
            // gcTasks
            // 
            resources.ApplyResources(this.gcTasks, "gcTasks");
            this.gcTasks.MainView = this.gvTasks;
            this.gcTasks.Name = "gcTasks";
            this.gcTasks.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkSelectTask});
            this.gcTasks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTasks});
            // 
            // gvTasks
            // 
            this.gvTasks.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTasks.Appearance.HeaderPanel.Font")));
            this.gvTasks.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTasks.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coltaskCheck,
            this.colTaskId,
            this.colRefId,
            this.colTaskName});
            this.gvTasks.GridControl = this.gcTasks;
            this.gvTasks.Name = "gvTasks";
            this.gvTasks.OptionsView.ShowGroupPanel = false;
            this.gvTasks.OptionsView.ShowIndicator = false;
            // 
            // coltaskCheck
            // 
            resources.ApplyResources(this.coltaskCheck, "coltaskCheck");
            this.coltaskCheck.ColumnEdit = this.rchkSelectTask;
            this.coltaskCheck.FieldName = "SELECT";
            this.coltaskCheck.Name = "coltaskCheck";
            this.coltaskCheck.OptionsColumn.AllowMove = false;
            this.coltaskCheck.OptionsColumn.AllowSize = false;
            this.coltaskCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.coltaskCheck.OptionsColumn.FixedWidth = true;
            this.coltaskCheck.OptionsColumn.ShowCaption = false;
            this.coltaskCheck.OptionsFilter.AllowAutoFilter = false;
            this.coltaskCheck.OptionsFilter.AllowFilter = false;
            this.coltaskCheck.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkSelectTask
            // 
            this.rchkSelectTask.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.rchkSelectTask, "rchkSelectTask");
            this.rchkSelectTask.Name = "rchkSelectTask";
            this.rchkSelectTask.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkSelectTask.ValueChecked = 1;
            this.rchkSelectTask.ValueGrayed = 2;
            this.rchkSelectTask.ValueUnchecked = 0;
            this.rchkSelectTask.CheckedChanged += new System.EventHandler(this.rchkSelectTask_CheckedChanged);
            // 
            // colTaskId
            // 
            resources.ApplyResources(this.colTaskId, "colTaskId");
            this.colTaskId.FieldName = "TAG_ID";
            this.colTaskId.Name = "colTaskId";
            // 
            // colRefId
            // 
            resources.ApplyResources(this.colRefId, "colRefId");
            this.colRefId.FieldName = "REF_ID";
            this.colRefId.Name = "colRefId";
            // 
            // colTaskName
            // 
            this.colTaskName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTaskName.AppearanceHeader.Font")));
            this.colTaskName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTaskName, "colTaskName");
            this.colTaskName.FieldName = "TAG_NAME";
            this.colTaskName.Name = "colTaskName";
            this.colTaskName.OptionsColumn.AllowEdit = false;
            this.colTaskName.OptionsColumn.AllowFocus = false;
            // 
            // chkTaskFilter
            // 
            resources.ApplyResources(this.chkTaskFilter, "chkTaskFilter");
            this.chkTaskFilter.Name = "chkTaskFilter";
            this.chkTaskFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkTaskFilter.Properties.Caption = resources.GetString("chkTaskFilter.Properties.Caption");
            this.chkTaskFilter.StyleController = this.layoutControl17;
            this.chkTaskFilter.CheckedChanged += new System.EventHandler(this.chkTaskFilter_CheckedChanged);
            // 
            // layoutControlGroup23
            // 
            resources.ApplyResources(this.layoutControlGroup23, "layoutControlGroup23");
            this.layoutControlGroup23.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup23.GroupBordersVisible = false;
            this.layoutControlGroup23.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem58,
            this.layoutControlGroup24});
            this.layoutControlGroup23.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup23.Name = "layoutControlGroup23";
            this.layoutControlGroup23.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup23.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup23.TextVisible = false;
            // 
            // layoutControlItem58
            // 
            this.layoutControlItem58.Control = this.chkTaskFilter;
            resources.ApplyResources(this.layoutControlItem58, "layoutControlItem58");
            this.layoutControlItem58.Location = new System.Drawing.Point(0, 255);
            this.layoutControlItem58.Name = "layoutControlItem58";
            this.layoutControlItem58.Size = new System.Drawing.Size(492, 23);
            this.layoutControlItem58.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem58.TextToControlDistance = 0;
            this.layoutControlItem58.TextVisible = false;
            // 
            // layoutControlGroup24
            // 
            this.layoutControlGroup24.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup24.AppearanceGroup.Font")));
            this.layoutControlGroup24.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup24, "layoutControlGroup24");
            this.layoutControlGroup24.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem57});
            this.layoutControlGroup24.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup24.Name = "layoutControlGroup24";
            this.layoutControlGroup24.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup24.Size = new System.Drawing.Size(492, 255);
            // 
            // layoutControlItem57
            // 
            this.layoutControlItem57.Control = this.gcTasks;
            resources.ApplyResources(this.layoutControlItem57, "layoutControlItem57");
            this.layoutControlItem57.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem57.Name = "layoutControlItem57";
            this.layoutControlItem57.Size = new System.Drawing.Size(486, 230);
            this.layoutControlItem57.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem57.TextToControlDistance = 0;
            this.layoutControlItem57.TextVisible = false;
            // 
            // xtpAnniversaryType
            // 
            this.xtpAnniversaryType.Controls.Add(this.layoutControl14);
            this.xtpAnniversaryType.Name = "xtpAnniversaryType";
            this.xtpAnniversaryType.PageVisible = false;
            resources.ApplyResources(this.xtpAnniversaryType, "xtpAnniversaryType");
            // 
            // layoutControl14
            // 
            this.layoutControl14.Controls.Add(this.lblType);
            this.layoutControl14.Controls.Add(this.rgAnniversaryType);
            resources.ApplyResources(this.layoutControl14, "layoutControl14");
            this.layoutControl14.Name = "layoutControl14";
            this.layoutControl14.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(547, 159, 250, 350);
            this.layoutControl14.Root = this.layoutControlGroup20;
            // 
            // lblType
            // 
            this.lblType.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblType.Appearance.Font")));
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            this.lblType.StyleController = this.layoutControl14;
            // 
            // rgAnniversaryType
            // 
            resources.ApplyResources(this.rgAnniversaryType, "rgAnniversaryType");
            this.rgAnniversaryType.Name = "rgAnniversaryType";
            this.rgAnniversaryType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgAnniversaryType.Properties.Items"))), resources.GetString("rgAnniversaryType.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgAnniversaryType.Properties.Items2"))), resources.GetString("rgAnniversaryType.Properties.Items3"))});
            this.rgAnniversaryType.StyleController = this.layoutControl14;
            // 
            // layoutControlGroup20
            // 
            resources.ApplyResources(this.layoutControlGroup20, "layoutControlGroup20");
            this.layoutControlGroup20.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup20.GroupBordersVisible = false;
            this.layoutControlGroup20.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem19,
            this.emptySpaceItem24,
            this.layoutControlItem54,
            this.emptySpaceItem23,
            this.layoutControlItem55});
            this.layoutControlGroup20.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup20.Name = "layoutControlGroup20";
            this.layoutControlGroup20.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup20.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup20.TextVisible = false;
            // 
            // emptySpaceItem19
            // 
            this.emptySpaceItem19.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem19, "emptySpaceItem19");
            this.emptySpaceItem19.Location = new System.Drawing.Point(236, 0);
            this.emptySpaceItem19.Name = "emptySpaceItem19";
            this.emptySpaceItem19.Size = new System.Drawing.Size(10, 29);
            this.emptySpaceItem19.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem24
            // 
            this.emptySpaceItem24.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem24, "emptySpaceItem24");
            this.emptySpaceItem24.Location = new System.Drawing.Point(0, 29);
            this.emptySpaceItem24.Name = "emptySpaceItem24";
            this.emptySpaceItem24.Size = new System.Drawing.Size(482, 239);
            this.emptySpaceItem24.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem54
            // 
            this.layoutControlItem54.Control = this.rgAnniversaryType;
            resources.ApplyResources(this.layoutControlItem54, "layoutControlItem54");
            this.layoutControlItem54.Location = new System.Drawing.Point(246, 0);
            this.layoutControlItem54.MinSize = new System.Drawing.Size(54, 29);
            this.layoutControlItem54.Name = "layoutControlItem54";
            this.layoutControlItem54.Size = new System.Drawing.Size(236, 29);
            this.layoutControlItem54.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem54.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem54.TextToControlDistance = 0;
            this.layoutControlItem54.TextVisible = false;
            // 
            // emptySpaceItem23
            // 
            this.emptySpaceItem23.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem23, "emptySpaceItem23");
            this.emptySpaceItem23.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem23.Name = "emptySpaceItem23";
            this.emptySpaceItem23.Size = new System.Drawing.Size(169, 29);
            this.emptySpaceItem23.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem55
            // 
            this.layoutControlItem55.Control = this.lblType;
            resources.ApplyResources(this.layoutControlItem55, "layoutControlItem55");
            this.layoutControlItem55.Location = new System.Drawing.Point(169, 0);
            this.layoutControlItem55.MinSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem55.Name = "layoutControlItem55";
            this.layoutControlItem55.Size = new System.Drawing.Size(67, 29);
            this.layoutControlItem55.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem55.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem55.TextToControlDistance = 0;
            this.layoutControlItem55.TextVisible = false;
            // 
            // xtbBudget
            // 
            this.xtbBudget.Controls.Add(this.lcBudget);
            this.xtbBudget.Name = "xtbBudget";
            this.xtbBudget.PageVisible = false;
            resources.ApplyResources(this.xtbBudget, "xtbBudget");
            // 
            // lcBudget
            // 
            this.lcBudget.Controls.Add(this.gcBudgetNewProject);
            this.lcBudget.Controls.Add(this.chkBudgetFilter);
            this.lcBudget.Controls.Add(this.gcBudget);
            resources.ApplyResources(this.lcBudget, "lcBudget");
            this.lcBudget.Name = "lcBudget";
            this.lcBudget.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(526, 159, 250, 350);
            this.lcBudget.Root = this.layoutControlGroup21;
            // 
            // gcBudgetNewProject
            // 
            resources.ApplyResources(this.gcBudgetNewProject, "gcBudgetNewProject");
            this.gcBudgetNewProject.MainView = this.gvBudgetNewProject;
            this.gcBudgetNewProject.Name = "gcBudgetNewProject";
            this.gcBudgetNewProject.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtBudgetNewProject,
            this.repositoryItemCalcEdit1,
            this.rctxtPIncomeAmt,
            this.rctxtPExpenseAmt,
            this.rctxtPHelpAmt,
            this.rbtnDelete,
            this.rctxtGHelpAmt,
            this.rchkIncludeReports,
            this.rtxtBudgetNewProjectRemarks});
            this.gcBudgetNewProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBudgetNewProject});
            this.gcBudgetNewProject.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcBudgetNewProject_ProcessGridKey);
            this.gcBudgetNewProject.Click += new System.EventHandler(this.gcBudgetNewProject_Click);
            // 
            // gvBudgetNewProject
            // 
            this.gvBudgetNewProject.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBudgetNewProject.Appearance.HeaderPanel.Font")));
            this.gvBudgetNewProject.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBudgetNewProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcAcId,
            this.colBudgetIncludeReports,
            this.ColBudgetNewProject,
            this.ColBudgetPExpenseAmount,
            this.ColBudgetPIncomeAmount,
            this.ColBudgetPGovtIncomeAmount,
            this.ColPProvinceHelp,
            this.ColBudgetPRemakrs,
            this.colDeleteBudgetNewProject});
            this.gvBudgetNewProject.GridControl = this.gcBudgetNewProject;
            this.gvBudgetNewProject.Name = "gvBudgetNewProject";
            this.gvBudgetNewProject.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvBudgetNewProject.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvBudgetNewProject.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvBudgetNewProject.OptionsBehavior.AutoPopulateColumns = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowColumnMoving = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowFilter = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowGroup = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvBudgetNewProject.OptionsCustomization.AllowSort = false;
            this.gvBudgetNewProject.OptionsNavigation.AutoFocusNewRow = true;
            this.gvBudgetNewProject.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvBudgetNewProject.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gvBudgetNewProject.OptionsView.ShowGroupPanel = false;
            this.gvBudgetNewProject.OptionsView.ShowIndicator = false;
            // 
            // gcAcId
            // 
            resources.ApplyResources(this.gcAcId, "gcAcId");
            this.gcAcId.FieldName = "ACC_YEAR_ID";
            this.gcAcId.Name = "gcAcId";
            this.gcAcId.OptionsColumn.AllowEdit = false;
            this.gcAcId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcAcId.OptionsColumn.AllowMove = false;
            this.gcAcId.OptionsColumn.AllowSize = false;
            this.gcAcId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colBudgetIncludeReports
            // 
            resources.ApplyResources(this.colBudgetIncludeReports, "colBudgetIncludeReports");
            this.colBudgetIncludeReports.ColumnEdit = this.rchkIncludeReports;
            this.colBudgetIncludeReports.FieldName = "INCLUDE_REPORTS";
            this.colBudgetIncludeReports.Name = "colBudgetIncludeReports";
            this.colBudgetIncludeReports.OptionsColumn.AllowIncrementalSearch = false;
            this.colBudgetIncludeReports.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetIncludeReports.OptionsColumn.AllowMove = false;
            this.colBudgetIncludeReports.OptionsColumn.AllowSize = false;
            this.colBudgetIncludeReports.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetIncludeReports.OptionsColumn.FixedWidth = true;
            this.colBudgetIncludeReports.OptionsColumn.ShowCaption = false;
            // 
            // rchkIncludeReports
            // 
            resources.ApplyResources(this.rchkIncludeReports, "rchkIncludeReports");
            this.rchkIncludeReports.Name = "rchkIncludeReports";
            this.rchkIncludeReports.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkIncludeReports.ValueChecked = 1;
            this.rchkIncludeReports.ValueGrayed = 2;
            this.rchkIncludeReports.ValueUnchecked = 0;
            // 
            // ColBudgetNewProject
            // 
            resources.ApplyResources(this.ColBudgetNewProject, "ColBudgetNewProject");
            this.ColBudgetNewProject.ColumnEdit = this.rtxtBudgetNewProject;
            this.ColBudgetNewProject.FieldName = "NEW_PROJECT";
            this.ColBudgetNewProject.Name = "ColBudgetNewProject";
            this.ColBudgetNewProject.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetNewProject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetNewProject.OptionsColumn.AllowMove = false;
            this.ColBudgetNewProject.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtBudgetNewProject
            // 
            resources.ApplyResources(this.rtxtBudgetNewProject, "rtxtBudgetNewProject");
            this.rtxtBudgetNewProject.MaxLength = 100;
            this.rtxtBudgetNewProject.Name = "rtxtBudgetNewProject";
            this.rtxtBudgetNewProject.Validating += new System.ComponentModel.CancelEventHandler(this.rtxtBudgetNewProject_Validating);
            // 
            // ColBudgetPExpenseAmount
            // 
            this.ColBudgetPExpenseAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.ColBudgetPExpenseAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.ColBudgetPExpenseAmount, "ColBudgetPExpenseAmount");
            this.ColBudgetPExpenseAmount.ColumnEdit = this.rctxtPExpenseAmt;
            this.ColBudgetPExpenseAmount.FieldName = "PROPOSED_EXPENSE_AMOUNT";
            this.ColBudgetPExpenseAmount.Name = "ColBudgetPExpenseAmount";
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowMove = false;
            this.ColBudgetPExpenseAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rctxtPExpenseAmt
            // 
            resources.ApplyResources(this.rctxtPExpenseAmt, "rctxtPExpenseAmt");
            this.rctxtPExpenseAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rctxtPExpenseAmt.Buttons"))))});
            this.rctxtPExpenseAmt.Mask.EditMask = resources.GetString("rctxtPExpenseAmt.Mask.EditMask");
            this.rctxtPExpenseAmt.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rctxtPExpenseAmt.Mask.UseMaskAsDisplayFormat")));
            this.rctxtPExpenseAmt.Name = "rctxtPExpenseAmt";
            // 
            // ColBudgetPIncomeAmount
            // 
            this.ColBudgetPIncomeAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.ColBudgetPIncomeAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.ColBudgetPIncomeAmount, "ColBudgetPIncomeAmount");
            this.ColBudgetPIncomeAmount.ColumnEdit = this.rctxtPIncomeAmt;
            this.ColBudgetPIncomeAmount.FieldName = "PROPOSED_INCOME_AMOUNT";
            this.ColBudgetPIncomeAmount.Name = "ColBudgetPIncomeAmount";
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowMove = false;
            this.ColBudgetPIncomeAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rctxtPIncomeAmt
            // 
            resources.ApplyResources(this.rctxtPIncomeAmt, "rctxtPIncomeAmt");
            this.rctxtPIncomeAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rctxtPIncomeAmt.Buttons"))))});
            this.rctxtPIncomeAmt.Mask.EditMask = resources.GetString("rctxtPIncomeAmt.Mask.EditMask");
            this.rctxtPIncomeAmt.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rctxtPIncomeAmt.Mask.UseMaskAsDisplayFormat")));
            this.rctxtPIncomeAmt.MaxLength = 13;
            this.rctxtPIncomeAmt.Name = "rctxtPIncomeAmt";
            // 
            // ColBudgetPGovtIncomeAmount
            // 
            this.ColBudgetPGovtIncomeAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.ColBudgetPGovtIncomeAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.ColBudgetPGovtIncomeAmount, "ColBudgetPGovtIncomeAmount");
            this.ColBudgetPGovtIncomeAmount.ColumnEdit = this.rctxtGHelpAmt;
            this.ColBudgetPGovtIncomeAmount.FieldName = "GN_HELP_PROPOSED_AMOUNT";
            this.ColBudgetPGovtIncomeAmount.Name = "ColBudgetPGovtIncomeAmount";
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowMove = false;
            this.ColBudgetPGovtIncomeAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rctxtGHelpAmt
            // 
            resources.ApplyResources(this.rctxtGHelpAmt, "rctxtGHelpAmt");
            this.rctxtGHelpAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rctxtGHelpAmt.Buttons"))))});
            this.rctxtGHelpAmt.Mask.EditMask = resources.GetString("rctxtGHelpAmt.Mask.EditMask");
            this.rctxtGHelpAmt.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rctxtGHelpAmt.Mask.UseMaskAsDisplayFormat")));
            this.rctxtGHelpAmt.MaxLength = 13;
            this.rctxtGHelpAmt.Name = "rctxtGHelpAmt";
            // 
            // ColPProvinceHelp
            // 
            this.ColPProvinceHelp.AppearanceHeader.Options.UseTextOptions = true;
            this.ColPProvinceHelp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.ColPProvinceHelp, "ColPProvinceHelp");
            this.ColPProvinceHelp.ColumnEdit = this.rctxtPHelpAmt;
            this.ColPProvinceHelp.FieldName = "HO_HELP_PROPOSED_AMOUNT";
            this.ColPProvinceHelp.Name = "ColPProvinceHelp";
            this.ColPProvinceHelp.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColPProvinceHelp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColPProvinceHelp.OptionsColumn.AllowMove = false;
            this.ColPProvinceHelp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColPProvinceHelp.UnboundExpression = "Province Help";
            // 
            // rctxtPHelpAmt
            // 
            resources.ApplyResources(this.rctxtPHelpAmt, "rctxtPHelpAmt");
            this.rctxtPHelpAmt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rctxtPHelpAmt.Buttons"))))});
            this.rctxtPHelpAmt.Mask.EditMask = resources.GetString("rctxtPHelpAmt.Mask.EditMask");
            this.rctxtPHelpAmt.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rctxtPHelpAmt.Mask.UseMaskAsDisplayFormat")));
            this.rctxtPHelpAmt.Name = "rctxtPHelpAmt";
            // 
            // ColBudgetPRemakrs
            // 
            resources.ApplyResources(this.ColBudgetPRemakrs, "ColBudgetPRemakrs");
            this.ColBudgetPRemakrs.ColumnEdit = this.rtxtBudgetNewProjectRemarks;
            this.ColBudgetPRemakrs.FieldName = "REMARKS";
            this.ColBudgetPRemakrs.Name = "ColBudgetPRemakrs";
            this.ColBudgetPRemakrs.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPRemakrs.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPRemakrs.OptionsColumn.AllowMove = false;
            this.ColBudgetPRemakrs.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ColBudgetPRemakrs.UnboundExpression = "Remarks";
            // 
            // rtxtBudgetNewProjectRemarks
            // 
            resources.ApplyResources(this.rtxtBudgetNewProjectRemarks, "rtxtBudgetNewProjectRemarks");
            this.rtxtBudgetNewProjectRemarks.MaxLength = 100;
            this.rtxtBudgetNewProjectRemarks.Name = "rtxtBudgetNewProjectRemarks";
            // 
            // colDeleteBudgetNewProject
            // 
            this.colDeleteBudgetNewProject.ColumnEdit = this.rbtnDelete;
            this.colDeleteBudgetNewProject.Name = "colDeleteBudgetNewProject";
            this.colDeleteBudgetNewProject.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowMove = false;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowShowHide = false;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowSize = false;
            this.colDeleteBudgetNewProject.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDeleteBudgetNewProject.OptionsColumn.FixedWidth = true;
            this.colDeleteBudgetNewProject.OptionsColumn.ShowCaption = false;
            this.colDeleteBudgetNewProject.OptionsColumn.TabStop = false;
            this.colDeleteBudgetNewProject.OptionsFilter.AllowAutoFilter = false;
            this.colDeleteBudgetNewProject.OptionsFilter.AllowFilter = false;
            this.colDeleteBudgetNewProject.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.colDeleteBudgetNewProject, "colDeleteBudgetNewProject");
            // 
            // rbtnDelete
            // 
            resources.ApplyResources(this.rbtnDelete, "rbtnDelete");
            this.rbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnDelete.Buttons"))), resources.GetString("rbtnDelete.Buttons1"), ((int)(resources.GetObject("rbtnDelete.Buttons2"))), ((bool)(resources.GetObject("rbtnDelete.Buttons3"))), ((bool)(resources.GetObject("rbtnDelete.Buttons4"))), ((bool)(resources.GetObject("rbtnDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnDelete.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("rbtnDelete.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbtnDelete.Buttons8"), ((object)(resources.GetObject("rbtnDelete.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnDelete.Buttons10"))), ((bool)(resources.GetObject("rbtnDelete.Buttons11"))))});
            this.rbtnDelete.Name = "rbtnDelete";
            this.rbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.rbtnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rbtnDelete_ButtonClick);
            // 
            // repositoryItemCalcEdit1
            // 
            resources.ApplyResources(this.repositoryItemCalcEdit1, "repositoryItemCalcEdit1");
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemCalcEdit1.Buttons"))))});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // chkBudgetFilter
            // 
            resources.ApplyResources(this.chkBudgetFilter, "chkBudgetFilter");
            this.chkBudgetFilter.Name = "chkBudgetFilter";
            this.chkBudgetFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkBudgetFilter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkBudgetFilter.Properties.Caption = resources.GetString("chkBudgetFilter.Properties.Caption");
            this.chkBudgetFilter.StyleController = this.lcBudget;
            this.chkBudgetFilter.CheckedChanged += new System.EventHandler(this.chkBudgetFilter_CheckedChanged);
            // 
            // gcBudget
            // 
            resources.ApplyResources(this.gcBudget, "gcBudget");
            this.gcBudget.MainView = this.gvBudget;
            this.gcBudget.Name = "gcBudget";
            this.gcBudget.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcBudget.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBudget});
            // 
            // gvBudget
            // 
            this.gvBudget.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBudget.Appearance.HeaderPanel.Font")));
            this.gvBudget.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBudget.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBudgetId,
            this.colBudgetProjectId,
            this.colBudgetSelection,
            this.colBudgetMonthRowName,
            this.colBudgetName,
            this.colBudgetType,
            this.colBudgetDateFrom,
            this.colBudgetDateTo,
            this.colBudgetProject,
            this.colBudgetStatus,
            this.colBudgetAction});
            this.gvBudget.GridControl = this.gcBudget;
            this.gvBudget.Name = "gvBudget";
            this.gvBudget.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBudget.OptionsView.ShowGroupPanel = false;
            this.gvBudget.OptionsView.ShowIndicator = false;
            // 
            // colBudgetId
            // 
            resources.ApplyResources(this.colBudgetId, "colBudgetId");
            this.colBudgetId.FieldName = "BUDGET_ID";
            this.colBudgetId.Name = "colBudgetId";
            // 
            // colBudgetProjectId
            // 
            resources.ApplyResources(this.colBudgetProjectId, "colBudgetProjectId");
            this.colBudgetProjectId.FieldName = "PROJECT_ID";
            this.colBudgetProjectId.Name = "colBudgetProjectId";
            // 
            // colBudgetSelection
            // 
            resources.ApplyResources(this.colBudgetSelection, "colBudgetSelection");
            this.colBudgetSelection.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colBudgetSelection.FieldName = "SELECT";
            this.colBudgetSelection.MinWidth = 10;
            this.colBudgetSelection.Name = "colBudgetSelection";
            this.colBudgetSelection.OptionsColumn.AllowMove = false;
            this.colBudgetSelection.OptionsColumn.AllowSize = false;
            this.colBudgetSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetSelection.OptionsColumn.ShowCaption = false;
            this.colBudgetSelection.OptionsFilter.AllowAutoFilter = false;
            this.colBudgetSelection.OptionsFilter.AllowFilter = false;
            this.colBudgetSelection.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = 1;
            this.repositoryItemCheckEdit1.ValueGrayed = 2;
            this.repositoryItemCheckEdit1.ValueUnchecked = 0;
            // 
            // colBudgetMonthRowName
            // 
            resources.ApplyResources(this.colBudgetMonthRowName, "colBudgetMonthRowName");
            this.colBudgetMonthRowName.FieldName = "MONTH_ROW_NAME";
            this.colBudgetMonthRowName.Name = "colBudgetMonthRowName";
            this.colBudgetMonthRowName.OptionsColumn.AllowEdit = false;
            this.colBudgetMonthRowName.OptionsColumn.AllowFocus = false;
            this.colBudgetMonthRowName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBudgetName
            // 
            resources.ApplyResources(this.colBudgetName, "colBudgetName");
            this.colBudgetName.FieldName = "BUDGET_NAME";
            this.colBudgetName.Name = "colBudgetName";
            this.colBudgetName.OptionsColumn.AllowEdit = false;
            this.colBudgetName.OptionsColumn.AllowFocus = false;
            this.colBudgetName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBudgetType
            // 
            resources.ApplyResources(this.colBudgetType, "colBudgetType");
            this.colBudgetType.FieldName = "BUDGET_TYPE";
            this.colBudgetType.Name = "colBudgetType";
            // 
            // colBudgetDateFrom
            // 
            resources.ApplyResources(this.colBudgetDateFrom, "colBudgetDateFrom");
            this.colBudgetDateFrom.FieldName = "DATE_FROM";
            this.colBudgetDateFrom.Name = "colBudgetDateFrom";
            this.colBudgetDateFrom.OptionsColumn.AllowEdit = false;
            this.colBudgetDateFrom.OptionsColumn.AllowFocus = false;
            this.colBudgetDateFrom.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBudgetDateTo
            // 
            resources.ApplyResources(this.colBudgetDateTo, "colBudgetDateTo");
            this.colBudgetDateTo.FieldName = "DATE_TO";
            this.colBudgetDateTo.Name = "colBudgetDateTo";
            this.colBudgetDateTo.OptionsColumn.AllowEdit = false;
            this.colBudgetDateTo.OptionsColumn.AllowFocus = false;
            this.colBudgetDateTo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBudgetProject
            // 
            resources.ApplyResources(this.colBudgetProject, "colBudgetProject");
            this.colBudgetProject.FieldName = "PROJECT";
            this.colBudgetProject.Name = "colBudgetProject";
            this.colBudgetProject.OptionsColumn.AllowEdit = false;
            this.colBudgetProject.OptionsColumn.AllowFocus = false;
            this.colBudgetProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBudgetStatus
            // 
            resources.ApplyResources(this.colBudgetStatus, "colBudgetStatus");
            this.colBudgetStatus.FieldName = "BUDGET_STATUS";
            this.colBudgetStatus.Name = "colBudgetStatus";
            this.colBudgetStatus.OptionsColumn.AllowEdit = false;
            this.colBudgetStatus.OptionsColumn.AllowFocus = false;
            this.colBudgetStatus.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetStatus.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetStatus.OptionsColumn.AllowMove = false;
            this.colBudgetStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBudgetAction
            // 
            resources.ApplyResources(this.colBudgetAction, "colBudgetAction");
            this.colBudgetAction.FieldName = "BUDGET_ACTION";
            this.colBudgetAction.Name = "colBudgetAction";
            this.colBudgetAction.OptionsColumn.AllowEdit = false;
            this.colBudgetAction.OptionsColumn.AllowFocus = false;
            this.colBudgetAction.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetAction.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetAction.OptionsColumn.AllowMove = false;
            this.colBudgetAction.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // layoutControlGroup21
            // 
            resources.ApplyResources(this.layoutControlGroup21, "layoutControlGroup21");
            this.layoutControlGroup21.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup21.GroupBordersVisible = false;
            this.layoutControlGroup21.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGrpBudgetGroup,
            this.lcGrpBudgetNewProject});
            this.layoutControlGroup21.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup21.Name = "layoutControlGroup21";
            this.layoutControlGroup21.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup21.Size = new System.Drawing.Size(492, 278);
            this.layoutControlGroup21.TextVisible = false;
            // 
            // lcGrpBudgetGroup
            // 
            this.lcGrpBudgetGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpBudgetGroup.AppearanceGroup.Font")));
            this.lcGrpBudgetGroup.AppearanceGroup.Options.UseFont = true;
            this.lcGrpBudgetGroup.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpBudgetGroup.AppearanceItemCaption.Font")));
            this.lcGrpBudgetGroup.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpBudgetGroup, "lcGrpBudgetGroup");
            this.lcGrpBudgetGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem21,
            this.layoutControlItem20});
            this.lcGrpBudgetGroup.Location = new System.Drawing.Point(0, 0);
            this.lcGrpBudgetGroup.Name = "lcGrpBudgetGroup";
            this.lcGrpBudgetGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcGrpBudgetGroup.Size = new System.Drawing.Size(482, 145);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.gcBudget;
            resources.ApplyResources(this.layoutControlItem21, "layoutControlItem21");
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(466, 87);
            this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem21.TextToControlDistance = 0;
            this.layoutControlItem21.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.chkBudgetFilter;
            resources.ApplyResources(this.layoutControlItem20, "layoutControlItem20");
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 87);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(466, 23);
            this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem20.TextToControlDistance = 0;
            this.layoutControlItem20.TextVisible = false;
            // 
            // lcGrpBudgetNewProject
            // 
            this.lcGrpBudgetNewProject.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpBudgetNewProject.AppearanceGroup.Font")));
            this.lcGrpBudgetNewProject.AppearanceGroup.Options.UseFont = true;
            this.lcGrpBudgetNewProject.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpBudgetNewProject.AppearanceItemCaption.Font")));
            this.lcGrpBudgetNewProject.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpBudgetNewProject, "lcGrpBudgetNewProject");
            this.lcGrpBudgetNewProject.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcBudgetNewProject});
            this.lcGrpBudgetNewProject.Location = new System.Drawing.Point(0, 145);
            this.lcGrpBudgetNewProject.Name = "lcGrpBudgetNewProject";
            this.lcGrpBudgetNewProject.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcGrpBudgetNewProject.Size = new System.Drawing.Size(482, 123);
            this.lcGrpBudgetNewProject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcBudgetNewProject
            // 
            this.lcBudgetNewProject.Control = this.gcBudgetNewProject;
            resources.ApplyResources(this.lcBudgetNewProject, "lcBudgetNewProject");
            this.lcBudgetNewProject.Location = new System.Drawing.Point(0, 0);
            this.lcBudgetNewProject.Name = "lcBudgetNewProject";
            this.lcBudgetNewProject.Size = new System.Drawing.Size(472, 94);
            this.lcBudgetNewProject.TextSize = new System.Drawing.Size(0, 0);
            this.lcBudgetNewProject.TextToControlDistance = 0;
            this.lcBudgetNewProject.TextVisible = false;
            // 
            // xtpReportSetup
            // 
            this.xtpReportSetup.Controls.Add(this.lcReport);
            this.xtpReportSetup.Name = "xtpReportSetup";
            resources.ApplyResources(this.xtpReportSetup, "xtpReportSetup");
            // 
            // lcReport
            // 
            this.lcReport.Controls.Add(this.glkpReportSetupCurrencyCountry);
            this.lcReport.Controls.Add(this.chkIncludeAllBudgetLedgers);
            this.lcReport.Controls.Add(this.chkShowTOC);
            this.lcReport.Controls.Add(this.cboReportType);
            this.lcReport.Controls.Add(this.chkSocietyWithInstutionName);
            this.lcReport.Controls.Add(this.cboColumnHeaderFontStyle);
            this.lcReport.Controls.Add(this.chkShowIndividualProjects);
            this.lcReport.Controls.Add(this.chkShowZeroValues);
            this.lcReport.Controls.Add(this.rgbAddress);
            this.lcReport.Controls.Add(this.cboBorderStyle);
            this.lcReport.Controls.Add(this.chkShowProjectinFooter);
            this.lcReport.Controls.Add(this.rgbReportTitle);
            this.lcReport.Controls.Add(this.chkPageNumber);
            this.lcReport.Controls.Add(this.chkVerticalLine);
            this.lcReport.Controls.Add(this.chkShowReportLogo);
            this.lcReport.Controls.Add(this.chkDisplayTitles);
            this.lcReport.Controls.Add(this.ReportDate);
            this.lcReport.Controls.Add(this.cboSoryByGroup);
            this.lcReport.Controls.Add(this.cboSortByLedger);
            this.lcReport.Controls.Add(this.chkGroupCode);
            this.lcReport.Controls.Add(this.chkLedgerCode);
            this.lcReport.Controls.Add(this.cboTitleAlignment);
            this.lcReport.Controls.Add(this.chkHorizontalLine);
            resources.ApplyResources(this.lcReport, "lcReport");
            this.lcReport.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblShowGroupCode,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.lcReport.Name = "lcReport";
            this.lcReport.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(874, 143, 398, 459);
            this.lcReport.Root = this.lcgReportSetup;
            // 
            // glkpReportSetupCurrencyCountry
            // 
            resources.ApplyResources(this.glkpReportSetupCurrencyCountry, "glkpReportSetupCurrencyCountry");
            this.glkpReportSetupCurrencyCountry.Name = "glkpReportSetupCurrencyCountry";
            this.glkpReportSetupCurrencyCountry.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.glkpReportSetupCurrencyCountry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpReportSetupCurrencyCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpReportSetupCurrencyCountry.Properties.Buttons"))))});
            this.glkpReportSetupCurrencyCountry.Properties.NullText = resources.GetString("glkpReportSetupCurrencyCountry.Properties.NullText");
            this.glkpReportSetupCurrencyCountry.Properties.PopupFormMinSize = new System.Drawing.Size(123, 0);
            this.glkpReportSetupCurrencyCountry.Properties.PopupFormSize = new System.Drawing.Size(123, 50);
            this.glkpReportSetupCurrencyCountry.Properties.View = this.gvCurrencyCountry;
            this.glkpReportSetupCurrencyCountry.StyleController = this.lcReport;
            this.glkpReportSetupCurrencyCountry.EditValueChanged += new System.EventHandler(this.glkpReportSetupCurrencyCountry_EditValueChanged);
            // 
            // gvCurrencyCountry
            // 
            this.gvCurrencyCountry.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCurrencyCountry.Appearance.FocusedRow.Font")));
            this.gvCurrencyCountry.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCurrencyCountry.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrencyCountryId,
            this.colCurrency,
            this.colCurrencyName,
            this.colCurrencySymbol});
            this.gvCurrencyCountry.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvCurrencyCountry.Name = "gvCurrencyCountry";
            this.gvCurrencyCountry.OptionsBehavior.Editable = false;
            this.gvCurrencyCountry.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCurrencyCountry.OptionsView.RowAutoHeight = true;
            this.gvCurrencyCountry.OptionsView.ShowColumnHeaders = false;
            this.gvCurrencyCountry.OptionsView.ShowGroupPanel = false;
            this.gvCurrencyCountry.OptionsView.ShowIndicator = false;
            // 
            // colCurrencyCountryId
            // 
            resources.ApplyResources(this.colCurrencyCountryId, "colCurrencyCountryId");
            this.colCurrencyCountryId.FieldName = "COUNTRY_ID";
            this.colCurrencyCountryId.Name = "colCurrencyCountryId";
            // 
            // colCurrency
            // 
            resources.ApplyResources(this.colCurrency, "colCurrency");
            this.colCurrency.FieldName = "CURRENCY_NAME";
            this.colCurrency.Name = "colCurrency";
            // 
            // colCurrencyName
            // 
            resources.ApplyResources(this.colCurrencyName, "colCurrencyName");
            this.colCurrencyName.FieldName = "CURRENCY_NAME";
            this.colCurrencyName.Name = "colCurrencyName";
            // 
            // colCurrencySymbol
            // 
            resources.ApplyResources(this.colCurrencySymbol, "colCurrencySymbol");
            this.colCurrencySymbol.FieldName = "CURRENCY_SYMBOL";
            this.colCurrencySymbol.Name = "colCurrencySymbol";
            // 
            // chkIncludeAllBudgetLedgers
            // 
            resources.ApplyResources(this.chkIncludeAllBudgetLedgers, "chkIncludeAllBudgetLedgers");
            this.chkIncludeAllBudgetLedgers.Name = "chkIncludeAllBudgetLedgers";
            this.chkIncludeAllBudgetLedgers.Properties.Caption = resources.GetString("chkIncludeAllBudgetLedgers.Properties.Caption");
            this.chkIncludeAllBudgetLedgers.StyleController = this.lcReport;
            // 
            // chkShowTOC
            // 
            resources.ApplyResources(this.chkShowTOC, "chkShowTOC");
            this.chkShowTOC.Name = "chkShowTOC";
            this.chkShowTOC.Properties.Caption = resources.GetString("chkShowTOC.Properties.Caption");
            this.chkShowTOC.StyleController = this.lcReport;
            // 
            // cboReportType
            // 
            resources.ApplyResources(this.cboReportType, "cboReportType");
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboReportType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboReportType.Properties.Buttons"))))});
            this.cboReportType.Properties.Items.AddRange(new object[] {
            resources.GetString("cboReportType.Properties.Items"),
            resources.GetString("cboReportType.Properties.Items1")});
            this.cboReportType.StyleController = this.lcReport;
            // 
            // chkSocietyWithInstutionName
            // 
            resources.ApplyResources(this.chkSocietyWithInstutionName, "chkSocietyWithInstutionName");
            this.chkSocietyWithInstutionName.Name = "chkSocietyWithInstutionName";
            this.chkSocietyWithInstutionName.Properties.Caption = resources.GetString("chkSocietyWithInstutionName.Properties.Caption");
            this.chkSocietyWithInstutionName.StyleController = this.lcReport;
            // 
            // cboColumnHeaderFontStyle
            // 
            resources.ApplyResources(this.cboColumnHeaderFontStyle, "cboColumnHeaderFontStyle");
            this.cboColumnHeaderFontStyle.Name = "cboColumnHeaderFontStyle";
            this.cboColumnHeaderFontStyle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboColumnHeaderFontStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboColumnHeaderFontStyle.Properties.Buttons"))))});
            this.cboColumnHeaderFontStyle.Properties.Items.AddRange(new object[] {
            resources.GetString("cboColumnHeaderFontStyle.Properties.Items"),
            resources.GetString("cboColumnHeaderFontStyle.Properties.Items1")});
            this.cboColumnHeaderFontStyle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboColumnHeaderFontStyle.StyleController = this.lcReport;
            // 
            // chkShowIndividualProjects
            // 
            resources.ApplyResources(this.chkShowIndividualProjects, "chkShowIndividualProjects");
            this.chkShowIndividualProjects.Name = "chkShowIndividualProjects";
            this.chkShowIndividualProjects.Properties.Caption = resources.GetString("chkShowIndividualProjects.Properties.Caption");
            this.chkShowIndividualProjects.StyleController = this.lcReport;
            // 
            // chkShowZeroValues
            // 
            resources.ApplyResources(this.chkShowZeroValues, "chkShowZeroValues");
            this.chkShowZeroValues.Name = "chkShowZeroValues";
            this.chkShowZeroValues.Properties.Caption = resources.GetString("chkShowZeroValues.Properties.Caption");
            this.chkShowZeroValues.StyleController = this.lcReport;
            // 
            // rgbAddress
            // 
            resources.ApplyResources(this.rgbAddress, "rgbAddress");
            this.rgbAddress.Name = "rgbAddress";
            this.rgbAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgbAddress.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgbAddress.Properties.Items"))), resources.GetString("rgbAddress.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgbAddress.Properties.Items2"))), resources.GetString("rgbAddress.Properties.Items3"))});
            this.rgbAddress.StyleController = this.lcReport;
            // 
            // cboBorderStyle
            // 
            resources.ApplyResources(this.cboBorderStyle, "cboBorderStyle");
            this.cboBorderStyle.Name = "cboBorderStyle";
            this.cboBorderStyle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboBorderStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboBorderStyle.Properties.Buttons"))))});
            this.cboBorderStyle.Properties.Items.AddRange(new object[] {
            resources.GetString("cboBorderStyle.Properties.Items"),
            resources.GetString("cboBorderStyle.Properties.Items1"),
            resources.GetString("cboBorderStyle.Properties.Items2")});
            this.cboBorderStyle.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboBorderStyle.StyleController = this.lcReport;
            // 
            // chkShowProjectinFooter
            // 
            resources.ApplyResources(this.chkShowProjectinFooter, "chkShowProjectinFooter");
            this.chkShowProjectinFooter.Name = "chkShowProjectinFooter";
            this.chkShowProjectinFooter.Properties.Caption = resources.GetString("chkShowProjectinFooter.Properties.Caption");
            this.chkShowProjectinFooter.StyleController = this.lcReport;
            // 
            // rgbReportTitle
            // 
            resources.ApplyResources(this.rgbReportTitle, "rgbReportTitle");
            this.rgbReportTitle.Name = "rgbReportTitle";
            this.rgbReportTitle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgbReportTitle.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgbReportTitle.Properties.Items"))), resources.GetString("rgbReportTitle.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgbReportTitle.Properties.Items2"))), resources.GetString("rgbReportTitle.Properties.Items3"))});
            this.rgbReportTitle.StyleController = this.lcReport;
            this.rgbReportTitle.SelectedIndexChanged += new System.EventHandler(this.rgbReportTitle_SelectedIndexChanged);
            // 
            // chkPageNumber
            // 
            resources.ApplyResources(this.chkPageNumber, "chkPageNumber");
            this.chkPageNumber.Name = "chkPageNumber";
            this.chkPageNumber.Properties.Caption = resources.GetString("chkPageNumber.Properties.Caption");
            this.chkPageNumber.StyleController = this.lcReport;
            // 
            // chkVerticalLine
            // 
            resources.ApplyResources(this.chkVerticalLine, "chkVerticalLine");
            this.chkVerticalLine.Name = "chkVerticalLine";
            this.chkVerticalLine.Properties.Caption = resources.GetString("chkVerticalLine.Properties.Caption");
            this.chkVerticalLine.StyleController = this.lcReport;
            // 
            // chkShowReportLogo
            // 
            resources.ApplyResources(this.chkShowReportLogo, "chkShowReportLogo");
            this.chkShowReportLogo.Name = "chkShowReportLogo";
            this.chkShowReportLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkShowReportLogo.Properties.Caption = resources.GetString("chkShowReportLogo.Properties.Caption");
            this.chkShowReportLogo.StyleController = this.lcReport;
            // 
            // chkDisplayTitles
            // 
            resources.ApplyResources(this.chkDisplayTitles, "chkDisplayTitles");
            this.chkDisplayTitles.Name = "chkDisplayTitles";
            this.chkDisplayTitles.Properties.Caption = resources.GetString("chkDisplayTitles.Properties.Caption");
            this.chkDisplayTitles.StyleController = this.lcReport;
            // 
            // ReportDate
            // 
            resources.ApplyResources(this.ReportDate, "ReportDate");
            this.ReportDate.Name = "ReportDate";
            this.ReportDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.ReportDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ReportDate.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ReportDate.Properties.Buttons1"))), resources.GetString("ReportDate.Properties.Buttons2"), ((int)(resources.GetObject("ReportDate.Properties.Buttons3"))), ((bool)(resources.GetObject("ReportDate.Properties.Buttons4"))), ((bool)(resources.GetObject("ReportDate.Properties.Buttons5"))), ((bool)(resources.GetObject("ReportDate.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("ReportDate.Properties.Buttons7"))), global::Bosco.Report.Properties.Resources.CheckBox, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("ReportDate.Properties.Buttons8"), ((object)(resources.GetObject("ReportDate.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("ReportDate.Properties.Buttons10"))), ((bool)(resources.GetObject("ReportDate.Properties.Buttons11"))))});
            this.ReportDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ReportDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("ReportDate.Properties.Mask.MaskType")));
            this.ReportDate.StyleController = this.lcReport;
            this.ReportDate.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ReportDate_ButtonClick);
            this.ReportDate.EditValueChanged += new System.EventHandler(this.ReportDate_EditValueChanged);
            // 
            // cboSoryByGroup
            // 
            resources.ApplyResources(this.cboSoryByGroup, "cboSoryByGroup");
            this.cboSoryByGroup.Name = "cboSoryByGroup";
            this.cboSoryByGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboSoryByGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboSoryByGroup.Properties.Buttons"))))});
            this.cboSoryByGroup.Properties.Items.AddRange(new object[] {
            resources.GetString("cboSoryByGroup.Properties.Items"),
            resources.GetString("cboSoryByGroup.Properties.Items1")});
            this.cboSoryByGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboSoryByGroup.StyleController = this.lcReport;
            // 
            // cboSortByLedger
            // 
            resources.ApplyResources(this.cboSortByLedger, "cboSortByLedger");
            this.cboSortByLedger.Name = "cboSortByLedger";
            this.cboSortByLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboSortByLedger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboSortByLedger.Properties.Buttons"))))});
            this.cboSortByLedger.Properties.Items.AddRange(new object[] {
            resources.GetString("cboSortByLedger.Properties.Items"),
            resources.GetString("cboSortByLedger.Properties.Items1")});
            this.cboSortByLedger.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboSortByLedger.StyleController = this.lcReport;
            // 
            // chkGroupCode
            // 
            resources.ApplyResources(this.chkGroupCode, "chkGroupCode");
            this.chkGroupCode.Name = "chkGroupCode";
            this.chkGroupCode.Properties.Caption = resources.GetString("chkGroupCode.Properties.Caption");
            this.chkGroupCode.StyleController = this.lcReport;
            // 
            // chkLedgerCode
            // 
            resources.ApplyResources(this.chkLedgerCode, "chkLedgerCode");
            this.chkLedgerCode.Name = "chkLedgerCode";
            this.chkLedgerCode.Properties.Caption = resources.GetString("chkLedgerCode.Properties.Caption");
            this.chkLedgerCode.StyleController = this.lcReport;
            // 
            // cboTitleAlignment
            // 
            resources.ApplyResources(this.cboTitleAlignment, "cboTitleAlignment");
            this.cboTitleAlignment.Name = "cboTitleAlignment";
            this.cboTitleAlignment.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboTitleAlignment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cboTitleAlignment.Properties.Buttons"))))});
            this.cboTitleAlignment.Properties.Items.AddRange(new object[] {
            resources.GetString("cboTitleAlignment.Properties.Items"),
            resources.GetString("cboTitleAlignment.Properties.Items1"),
            resources.GetString("cboTitleAlignment.Properties.Items2")});
            this.cboTitleAlignment.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTitleAlignment.StyleController = this.lcReport;
            // 
            // chkHorizontalLine
            // 
            resources.ApplyResources(this.chkHorizontalLine, "chkHorizontalLine");
            this.chkHorizontalLine.Name = "chkHorizontalLine";
            this.chkHorizontalLine.Properties.Caption = resources.GetString("chkHorizontalLine.Properties.Caption");
            this.chkHorizontalLine.StyleController = this.lcReport;
            // 
            // lblShowGroupCode
            // 
            this.lblShowGroupCode.Control = this.chkGroupCode;
            resources.ApplyResources(this.lblShowGroupCode, "lblShowGroupCode");
            this.lblShowGroupCode.Location = new System.Drawing.Point(0, 23);
            this.lblShowGroupCode.MinSize = new System.Drawing.Size(113, 22);
            this.lblShowGroupCode.Name = "lblShowGroupCode";
            this.lblShowGroupCode.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblShowGroupCode.Size = new System.Drawing.Size(123, 23);
            this.lblShowGroupCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblShowGroupCode.TextSize = new System.Drawing.Size(0, 0);
            this.lblShowGroupCode.TextToControlDistance = 0;
            this.lblShowGroupCode.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cboSoryByGroup;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(50, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem2.Size = new System.Drawing.Size(255, 23);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkDisplayTitles;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 46);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(229, 22);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcgReportSetup
            // 
            resources.ApplyResources(this.lcgReportSetup, "lcgReportSetup");
            this.lcgReportSetup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgReportSetup.GroupBordersVisible = false;
            this.lcgReportSetup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgReportPageHeader});
            this.lcgReportSetup.Location = new System.Drawing.Point(0, 0);
            this.lcgReportSetup.Name = "lcgReportSetup";
            this.lcgReportSetup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgReportSetup.Size = new System.Drawing.Size(492, 278);
            this.lcgReportSetup.TextVisible = false;
            // 
            // lcgReportPageHeader
            // 
            this.lcgReportPageHeader.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgReportPageHeader.AppearanceGroup.Font")));
            this.lcgReportPageHeader.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgReportPageHeader, "lcgReportPageHeader");
            this.lcgReportPageHeader.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblLedgerCode,
            this.lblHorizontalLine,
            this.layoutControlItem1,
            this.emptySpaceItem9,
            this.emptySpaceItem10,
            this.emptySpaceItem12,
            this.lblReportDate,
            this.lblTitle,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.emptySpaceItem5,
            this.emptySpaceItem7,
            this.lblBorderStyle,
            this.layoutControlItem16,
            this.lblShowReportLogo,
            this.layoutControlItem33,
            this.layoutControlItem59,
            this.lblColumnHeaderFontStyle,
            this.lcSocietyWithInstutionName,
            this.lciReportCode,
            this.lcShowTOC,
            this.lcIncludeAllBudgetLedgers,
            this.lcReportSetupCurrency});
            this.lcgReportPageHeader.Location = new System.Drawing.Point(0, 0);
            this.lcgReportPageHeader.Name = "lcgReportPageHeader";
            this.lcgReportPageHeader.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgReportPageHeader.Size = new System.Drawing.Size(492, 278);
            this.lcgReportPageHeader.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            // 
            // lblLedgerCode
            // 
            this.lblLedgerCode.Control = this.chkLedgerCode;
            resources.ApplyResources(this.lblLedgerCode, "lblLedgerCode");
            this.lblLedgerCode.Location = new System.Drawing.Point(0, 0);
            this.lblLedgerCode.MinSize = new System.Drawing.Size(117, 22);
            this.lblLedgerCode.Name = "lblLedgerCode";
            this.lblLedgerCode.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblLedgerCode.Size = new System.Drawing.Size(123, 26);
            this.lblLedgerCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblLedgerCode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.lblLedgerCode.TextSize = new System.Drawing.Size(0, 0);
            this.lblLedgerCode.TextToControlDistance = 0;
            this.lblLedgerCode.TextVisible = false;
            // 
            // lblHorizontalLine
            // 
            this.lblHorizontalLine.Control = this.chkHorizontalLine;
            resources.ApplyResources(this.lblHorizontalLine, "lblHorizontalLine");
            this.lblHorizontalLine.Location = new System.Drawing.Point(0, 72);
            this.lblHorizontalLine.MinSize = new System.Drawing.Size(93, 22);
            this.lblHorizontalLine.Name = "lblHorizontalLine";
            this.lblHorizontalLine.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblHorizontalLine.Size = new System.Drawing.Size(205, 22);
            this.lblHorizontalLine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblHorizontalLine.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.lblHorizontalLine.TextSize = new System.Drawing.Size(0, 0);
            this.lblHorizontalLine.TextToControlDistance = 0;
            this.lblHorizontalLine.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cboSortByLedger;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(123, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem1.Size = new System.Drawing.Size(132, 26);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem9, "emptySpaceItem9");
            this.emptySpaceItem9.Location = new System.Drawing.Point(255, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(47, 26);
            this.emptySpaceItem9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem10, "emptySpaceItem10");
            this.emptySpaceItem10.Location = new System.Drawing.Point(300, 26);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem10.Size = new System.Drawing.Size(36, 24);
            this.emptySpaceItem10.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem12
            // 
            this.emptySpaceItem12.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem12, "emptySpaceItem12");
            this.emptySpaceItem12.Location = new System.Drawing.Point(91, 50);
            this.emptySpaceItem12.Name = "emptySpaceItem12";
            this.emptySpaceItem12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem12.Size = new System.Drawing.Size(212, 22);
            this.emptySpaceItem12.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.emptySpaceItem12.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblReportDate
            // 
            this.lblReportDate.Control = this.ReportDate;
            resources.ApplyResources(this.lblReportDate, "lblReportDate");
            this.lblReportDate.Location = new System.Drawing.Point(302, 0);
            this.lblReportDate.MaxSize = new System.Drawing.Size(168, 23);
            this.lblReportDate.MinSize = new System.Drawing.Size(168, 23);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0);
            this.lblReportDate.Size = new System.Drawing.Size(168, 26);
            this.lblReportDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblReportDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.lblReportDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblReportDate.TextSize = new System.Drawing.Size(48, 13);
            this.lblReportDate.TextToControlDistance = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.Control = this.cboTitleAlignment;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Location = new System.Drawing.Point(336, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(134, 24);
            this.lblTitle.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.lblTitle.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblTitle.TextSize = new System.Drawing.Size(70, 13);
            this.lblTitle.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkVerticalLine;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.layoutControlItem4.Size = new System.Drawing.Size(91, 22);
            this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkPageNumber;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(303, 50);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(167, 22);
            this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.rgbReportTitle;
            resources.ApplyResources(this.layoutControlItem13, "layoutControlItem13");
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 139);
            this.layoutControlItem13.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(159, 37);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.layoutControlItem13.Size = new System.Drawing.Size(347, 37);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(100, 13);
            this.layoutControlItem13.TextToControlDistance = 5;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.chkShowProjectinFooter;
            resources.ApplyResources(this.layoutControlItem14, "layoutControlItem14");
            this.layoutControlItem14.Location = new System.Drawing.Point(303, 72);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem14.Size = new System.Drawing.Size(167, 22);
            this.layoutControlItem14.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(205, 72);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem5.Size = new System.Drawing.Size(98, 22);
            this.emptySpaceItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem7, "emptySpaceItem7");
            this.emptySpaceItem7.Location = new System.Drawing.Point(223, 213);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(10, 24);
            this.emptySpaceItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblBorderStyle
            // 
            this.lblBorderStyle.Control = this.cboBorderStyle;
            resources.ApplyResources(this.lblBorderStyle, "lblBorderStyle");
            this.lblBorderStyle.Location = new System.Drawing.Point(0, 213);
            this.lblBorderStyle.Name = "lblBorderStyle";
            this.lblBorderStyle.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblBorderStyle.Size = new System.Drawing.Size(223, 24);
            this.lblBorderStyle.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.lblBorderStyle.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblBorderStyle.TextSize = new System.Drawing.Size(100, 13);
            this.lblBorderStyle.TextToControlDistance = 5;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.rgbAddress;
            resources.ApplyResources(this.layoutControlItem16, "layoutControlItem16");
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 176);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(158, 37);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.layoutControlItem16.Size = new System.Drawing.Size(470, 37);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem16.TextSize = new System.Drawing.Size(101, 13);
            // 
            // lblShowReportLogo
            // 
            this.lblShowReportLogo.Control = this.chkShowReportLogo;
            resources.ApplyResources(this.lblShowReportLogo, "lblShowReportLogo");
            this.lblShowReportLogo.Location = new System.Drawing.Point(0, 26);
            this.lblShowReportLogo.Name = "lblShowReportLogo";
            this.lblShowReportLogo.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblShowReportLogo.Size = new System.Drawing.Size(123, 24);
            this.lblShowReportLogo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.lblShowReportLogo.TextSize = new System.Drawing.Size(0, 0);
            this.lblShowReportLogo.TextToControlDistance = 0;
            this.lblShowReportLogo.TextVisible = false;
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.chkShowZeroValues;
            resources.ApplyResources(this.layoutControlItem33, "layoutControlItem33");
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 94);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.layoutControlItem33.Size = new System.Drawing.Size(303, 22);
            this.layoutControlItem33.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem33.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem33.TextToControlDistance = 0;
            this.layoutControlItem33.TextVisible = false;
            // 
            // layoutControlItem59
            // 
            this.layoutControlItem59.Control = this.chkShowIndividualProjects;
            resources.ApplyResources(this.layoutControlItem59, "layoutControlItem59");
            this.layoutControlItem59.Location = new System.Drawing.Point(0, 116);
            this.layoutControlItem59.Name = "layoutControlItem59";
            this.layoutControlItem59.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.layoutControlItem59.Size = new System.Drawing.Size(151, 23);
            this.layoutControlItem59.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem59.TextToControlDistance = 0;
            this.layoutControlItem59.TextVisible = false;
            // 
            // lblColumnHeaderFontStyle
            // 
            this.lblColumnHeaderFontStyle.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblColumnHeaderFontStyle.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblColumnHeaderFontStyle.Control = this.cboColumnHeaderFontStyle;
            resources.ApplyResources(this.lblColumnHeaderFontStyle, "lblColumnHeaderFontStyle");
            this.lblColumnHeaderFontStyle.Location = new System.Drawing.Point(233, 213);
            this.lblColumnHeaderFontStyle.MinSize = new System.Drawing.Size(161, 20);
            this.lblColumnHeaderFontStyle.Name = "lblColumnHeaderFontStyle";
            this.lblColumnHeaderFontStyle.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 2, 0);
            this.lblColumnHeaderFontStyle.Size = new System.Drawing.Size(237, 24);
            this.lblColumnHeaderFontStyle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblColumnHeaderFontStyle.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblColumnHeaderFontStyle.TextSize = new System.Drawing.Size(101, 13);
            this.lblColumnHeaderFontStyle.TextToControlDistance = 5;
            // 
            // lcSocietyWithInstutionName
            // 
            this.lcSocietyWithInstutionName.Control = this.chkSocietyWithInstutionName;
            this.lcSocietyWithInstutionName.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcSocietyWithInstutionName, "lcSocietyWithInstutionName");
            this.lcSocietyWithInstutionName.Location = new System.Drawing.Point(347, 139);
            this.lcSocietyWithInstutionName.Name = "lcSocietyWithInstutionName";
            this.lcSocietyWithInstutionName.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 3, 8, 2);
            this.lcSocietyWithInstutionName.Size = new System.Drawing.Size(123, 37);
            this.lcSocietyWithInstutionName.TextSize = new System.Drawing.Size(0, 0);
            this.lcSocietyWithInstutionName.TextToControlDistance = 0;
            this.lcSocietyWithInstutionName.TextVisible = false;
            // 
            // lciReportCode
            // 
            this.lciReportCode.Control = this.cboReportType;
            resources.ApplyResources(this.lciReportCode, "lciReportCode");
            this.lciReportCode.Location = new System.Drawing.Point(304, 116);
            this.lciReportCode.Name = "lciReportCode";
            this.lciReportCode.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 1);
            this.lciReportCode.Size = new System.Drawing.Size(166, 23);
            this.lciReportCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciReportCode.TextSize = new System.Drawing.Size(25, 13);
            this.lciReportCode.TextToControlDistance = 5;
            this.lciReportCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcShowTOC
            // 
            this.lcShowTOC.Control = this.chkShowTOC;
            this.lcShowTOC.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcShowTOC, "lcShowTOC");
            this.lcShowTOC.Location = new System.Drawing.Point(303, 94);
            this.lcShowTOC.Name = "lcShowTOC";
            this.lcShowTOC.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcShowTOC.Size = new System.Drawing.Size(167, 22);
            this.lcShowTOC.TextSize = new System.Drawing.Size(0, 0);
            this.lcShowTOC.TextToControlDistance = 0;
            this.lcShowTOC.TextVisible = false;
            this.lcShowTOC.TrimClientAreaToControl = false;
            // 
            // lcIncludeAllBudgetLedgers
            // 
            this.lcIncludeAllBudgetLedgers.Control = this.chkIncludeAllBudgetLedgers;
            resources.ApplyResources(this.lcIncludeAllBudgetLedgers, "lcIncludeAllBudgetLedgers");
            this.lcIncludeAllBudgetLedgers.Location = new System.Drawing.Point(151, 116);
            this.lcIncludeAllBudgetLedgers.Name = "lcIncludeAllBudgetLedgers";
            this.lcIncludeAllBudgetLedgers.Size = new System.Drawing.Size(153, 23);
            this.lcIncludeAllBudgetLedgers.TextSize = new System.Drawing.Size(0, 0);
            this.lcIncludeAllBudgetLedgers.TextToControlDistance = 0;
            this.lcIncludeAllBudgetLedgers.TextVisible = false;
            // 
            // lcReportSetupCurrency
            // 
            this.lcReportSetupCurrency.Control = this.glkpReportSetupCurrencyCountry;
            resources.ApplyResources(this.lcReportSetupCurrency, "lcReportSetupCurrency");
            this.lcReportSetupCurrency.Location = new System.Drawing.Point(123, 26);
            this.lcReportSetupCurrency.Name = "lcReportSetupCurrency";
            this.lcReportSetupCurrency.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcReportSetupCurrency.Size = new System.Drawing.Size(177, 24);
            this.lcReportSetupCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcReportSetupCurrency.TextSize = new System.Drawing.Size(44, 13);
            this.lcReportSetupCurrency.TextToControlDistance = 5;
            // 
            // xtbSign
            // 
            this.xtbSign.Controls.Add(this.lcSignDetails);
            this.xtbSign.Name = "xtbSign";
            resources.ApplyResources(this.xtbSign, "xtbSign");
            // 
            // lcSignDetails
            // 
            this.lcSignDetails.Controls.Add(this.chkIncludeAuditorSignNote);
            this.lcSignDetails.Controls.Add(this.btnSign3);
            this.lcSignDetails.Controls.Add(this.btnSign2);
            this.lcSignDetails.Controls.Add(this.btnSign1);
            this.lcSignDetails.Controls.Add(this.txtRole3);
            this.lcSignDetails.Controls.Add(this.txtRoleName3);
            this.lcSignDetails.Controls.Add(this.txtRole2);
            this.lcSignDetails.Controls.Add(this.txtRoleName2);
            this.lcSignDetails.Controls.Add(this.txtRole1);
            this.lcSignDetails.Controls.Add(this.txtRoleName1);
            this.lcSignDetails.Controls.Add(this.chkIncludeSignDetails);
            this.lcSignDetails.Controls.Add(this.picSign1);
            this.lcSignDetails.Controls.Add(this.picSign2);
            this.lcSignDetails.Controls.Add(this.picSign3);
            resources.ApplyResources(this.lcSignDetails, "lcSignDetails");
            this.lcSignDetails.Name = "lcSignDetails";
            this.lcSignDetails.Root = this.layoutControlGroup1;
            // 
            // chkIncludeAuditorSignNote
            // 
            resources.ApplyResources(this.chkIncludeAuditorSignNote, "chkIncludeAuditorSignNote");
            this.chkIncludeAuditorSignNote.Name = "chkIncludeAuditorSignNote";
            this.chkIncludeAuditorSignNote.Properties.Caption = resources.GetString("chkIncludeAuditorSignNote.Properties.Caption");
            this.chkIncludeAuditorSignNote.StyleController = this.lcSignDetails;
            // 
            // btnSign3
            // 
            resources.ApplyResources(this.btnSign3, "btnSign3");
            this.btnSign3.Name = "btnSign3";
            this.btnSign3.StyleController = this.lcSignDetails;
            this.btnSign3.Click += new System.EventHandler(this.btnSign3_Click);
            // 
            // btnSign2
            // 
            resources.ApplyResources(this.btnSign2, "btnSign2");
            this.btnSign2.Name = "btnSign2";
            this.btnSign2.StyleController = this.lcSignDetails;
            this.btnSign2.Click += new System.EventHandler(this.btnSign2_Click);
            // 
            // btnSign1
            // 
            resources.ApplyResources(this.btnSign1, "btnSign1");
            this.btnSign1.Name = "btnSign1";
            this.btnSign1.StyleController = this.lcSignDetails;
            this.btnSign1.Click += new System.EventHandler(this.btnSign1_Click);
            // 
            // txtRole3
            // 
            resources.ApplyResources(this.txtRole3, "txtRole3");
            this.txtRole3.Name = "txtRole3";
            this.txtRole3.Properties.MaxLength = 75;
            this.txtRole3.StyleController = this.lcSignDetails;
            // 
            // txtRoleName3
            // 
            resources.ApplyResources(this.txtRoleName3, "txtRoleName3");
            this.txtRoleName3.Name = "txtRoleName3";
            this.txtRoleName3.Properties.MaxLength = 75;
            this.txtRoleName3.StyleController = this.lcSignDetails;
            // 
            // txtRole2
            // 
            resources.ApplyResources(this.txtRole2, "txtRole2");
            this.txtRole2.Name = "txtRole2";
            this.txtRole2.Properties.MaxLength = 75;
            this.txtRole2.StyleController = this.lcSignDetails;
            // 
            // txtRoleName2
            // 
            resources.ApplyResources(this.txtRoleName2, "txtRoleName2");
            this.txtRoleName2.Name = "txtRoleName2";
            this.txtRoleName2.Properties.MaxLength = 75;
            this.txtRoleName2.StyleController = this.lcSignDetails;
            // 
            // txtRole1
            // 
            resources.ApplyResources(this.txtRole1, "txtRole1");
            this.txtRole1.Name = "txtRole1";
            this.txtRole1.Properties.MaxLength = 75;
            this.txtRole1.StyleController = this.lcSignDetails;
            // 
            // txtRoleName1
            // 
            resources.ApplyResources(this.txtRoleName1, "txtRoleName1");
            this.txtRoleName1.Name = "txtRoleName1";
            this.txtRoleName1.Properties.MaxLength = 75;
            this.txtRoleName1.StyleController = this.lcSignDetails;
            // 
            // chkIncludeSignDetails
            // 
            resources.ApplyResources(this.chkIncludeSignDetails, "chkIncludeSignDetails");
            this.chkIncludeSignDetails.Name = "chkIncludeSignDetails";
            this.chkIncludeSignDetails.Properties.Caption = resources.GetString("chkIncludeSignDetails.Properties.Caption");
            this.chkIncludeSignDetails.StyleController = this.lcSignDetails;
            // 
            // picSign1
            // 
            resources.ApplyResources(this.picSign1, "picSign1");
            this.picSign1.Name = "picSign1";
            this.picSign1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picSign1.StyleController = this.lcSignDetails;
            this.picSign1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picSign1_MouseUp);
            // 
            // picSign2
            // 
            resources.ApplyResources(this.picSign2, "picSign2");
            this.picSign2.Name = "picSign2";
            this.picSign2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picSign2.StyleController = this.lcSignDetails;
            this.picSign2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picSign2_MouseUp);
            // 
            // picSign3
            // 
            resources.ApplyResources(this.picSign3, "picSign3");
            this.picSign3.Name = "picSign3";
            this.picSign3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picSign3.StyleController = this.lcSignDetails;
            this.picSign3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picSign3_MouseUp);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcIncludeSignDetails,
            this.lcGrpSign1,
            this.lcGrpSign2,
            this.lcGrpSign3,
            this.lcIncludeAuditorSignNote});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(492, 281);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcIncludeSignDetails
            // 
            this.lcIncludeSignDetails.Control = this.chkIncludeSignDetails;
            resources.ApplyResources(this.lcIncludeSignDetails, "lcIncludeSignDetails");
            this.lcIncludeSignDetails.Location = new System.Drawing.Point(0, 0);
            this.lcIncludeSignDetails.Name = "lcIncludeSignDetails";
            this.lcIncludeSignDetails.Size = new System.Drawing.Size(233, 23);
            this.lcIncludeSignDetails.TextSize = new System.Drawing.Size(0, 0);
            this.lcIncludeSignDetails.TextToControlDistance = 0;
            this.lcIncludeSignDetails.TextVisible = false;
            // 
            // lcGrpSign1
            // 
            this.lcGrpSign1.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpSign1.AppearanceGroup.Font")));
            this.lcGrpSign1.AppearanceGroup.Options.UseFont = true;
            this.lcGrpSign1.CaptionImagePadding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            resources.ApplyResources(this.lcGrpSign1, "lcGrpSign1");
            this.lcGrpSign1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcRole,
            this.lcRoleName1,
            this.lcSign1,
            this.lcSign1Btn});
            this.lcGrpSign1.Location = new System.Drawing.Point(0, 23);
            this.lcGrpSign1.Name = "lcGrpSign1";
            this.lcGrpSign1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcGrpSign1.Size = new System.Drawing.Size(472, 79);
            // 
            // lcRole
            // 
            this.lcRole.Control = this.txtRole1;
            resources.ApplyResources(this.lcRole, "lcRole");
            this.lcRole.Location = new System.Drawing.Point(0, 24);
            this.lcRole.MaxSize = new System.Drawing.Size(206, 26);
            this.lcRole.MinSize = new System.Drawing.Size(206, 26);
            this.lcRole.Name = "lcRole";
            this.lcRole.Size = new System.Drawing.Size(206, 26);
            this.lcRole.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcRole.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcRole.TextSize = new System.Drawing.Size(50, 13);
            this.lcRole.TextToControlDistance = 5;
            // 
            // lcRoleName1
            // 
            this.lcRoleName1.Control = this.txtRoleName1;
            resources.ApplyResources(this.lcRoleName1, "lcRoleName1");
            this.lcRoleName1.Location = new System.Drawing.Point(0, 0);
            this.lcRoleName1.Name = "lcRoleName1";
            this.lcRoleName1.Size = new System.Drawing.Size(462, 24);
            this.lcRoleName1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcRoleName1.TextSize = new System.Drawing.Size(50, 13);
            this.lcRoleName1.TextToControlDistance = 5;
            // 
            // lcSign1
            // 
            this.lcSign1.Control = this.picSign1;
            resources.ApplyResources(this.lcSign1, "lcSign1");
            this.lcSign1.Location = new System.Drawing.Point(206, 24);
            this.lcSign1.Name = "lcSign1";
            this.lcSign1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcSign1.Size = new System.Drawing.Size(227, 26);
            this.lcSign1.TextSize = new System.Drawing.Size(29, 13);
            // 
            // lcSign1Btn
            // 
            this.lcSign1Btn.Control = this.btnSign1;
            resources.ApplyResources(this.lcSign1Btn, "lcSign1Btn");
            this.lcSign1Btn.Location = new System.Drawing.Point(433, 24);
            this.lcSign1Btn.MaxSize = new System.Drawing.Size(29, 26);
            this.lcSign1Btn.MinSize = new System.Drawing.Size(29, 26);
            this.lcSign1Btn.Name = "lcSign1Btn";
            this.lcSign1Btn.Size = new System.Drawing.Size(29, 26);
            this.lcSign1Btn.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcSign1Btn.TextSize = new System.Drawing.Size(0, 0);
            this.lcSign1Btn.TextToControlDistance = 0;
            this.lcSign1Btn.TextVisible = false;
            // 
            // lcGrpSign2
            // 
            this.lcGrpSign2.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpSign2.AppearanceGroup.Font")));
            this.lcGrpSign2.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpSign2, "lcGrpSign2");
            this.lcGrpSign2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcRoleName2,
            this.lcRole2,
            this.lcSign2,
            this.lcSign2Btn});
            this.lcGrpSign2.Location = new System.Drawing.Point(0, 102);
            this.lcGrpSign2.Name = "lcGrpSign2";
            this.lcGrpSign2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcGrpSign2.Size = new System.Drawing.Size(472, 79);
            // 
            // lcRoleName2
            // 
            this.lcRoleName2.Control = this.txtRoleName2;
            resources.ApplyResources(this.lcRoleName2, "lcRoleName2");
            this.lcRoleName2.Location = new System.Drawing.Point(0, 0);
            this.lcRoleName2.Name = "lcRoleName2";
            this.lcRoleName2.Size = new System.Drawing.Size(462, 24);
            this.lcRoleName2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcRoleName2.TextSize = new System.Drawing.Size(50, 20);
            this.lcRoleName2.TextToControlDistance = 5;
            // 
            // lcRole2
            // 
            this.lcRole2.Control = this.txtRole2;
            resources.ApplyResources(this.lcRole2, "lcRole2");
            this.lcRole2.Location = new System.Drawing.Point(0, 24);
            this.lcRole2.MaxSize = new System.Drawing.Size(202, 26);
            this.lcRole2.MinSize = new System.Drawing.Size(202, 26);
            this.lcRole2.Name = "lcRole2";
            this.lcRole2.Size = new System.Drawing.Size(202, 26);
            this.lcRole2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcRole2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcRole2.TextSize = new System.Drawing.Size(50, 20);
            this.lcRole2.TextToControlDistance = 5;
            // 
            // lcSign2
            // 
            this.lcSign2.Control = this.picSign2;
            resources.ApplyResources(this.lcSign2, "lcSign2");
            this.lcSign2.Location = new System.Drawing.Point(202, 24);
            this.lcSign2.Name = "lcSign2";
            this.lcSign2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcSign2.Size = new System.Drawing.Size(230, 26);
            this.lcSign2.TextSize = new System.Drawing.Size(29, 13);
            // 
            // lcSign2Btn
            // 
            this.lcSign2Btn.Control = this.btnSign2;
            resources.ApplyResources(this.lcSign2Btn, "lcSign2Btn");
            this.lcSign2Btn.Location = new System.Drawing.Point(432, 24);
            this.lcSign2Btn.MaxSize = new System.Drawing.Size(30, 26);
            this.lcSign2Btn.MinSize = new System.Drawing.Size(30, 26);
            this.lcSign2Btn.Name = "lcSign2Btn";
            this.lcSign2Btn.Size = new System.Drawing.Size(30, 26);
            this.lcSign2Btn.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcSign2Btn.TextSize = new System.Drawing.Size(0, 0);
            this.lcSign2Btn.TextToControlDistance = 0;
            this.lcSign2Btn.TextVisible = false;
            // 
            // lcGrpSign3
            // 
            this.lcGrpSign3.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpSign3.AppearanceGroup.Font")));
            this.lcGrpSign3.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpSign3, "lcGrpSign3");
            this.lcGrpSign3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcRoleName3,
            this.lcRole3,
            this.lcSign3,
            this.lcSign3Btn});
            this.lcGrpSign3.Location = new System.Drawing.Point(0, 181);
            this.lcGrpSign3.Name = "lcGrpSign3";
            this.lcGrpSign3.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcGrpSign3.Size = new System.Drawing.Size(472, 80);
            // 
            // lcRoleName3
            // 
            this.lcRoleName3.Control = this.txtRoleName3;
            resources.ApplyResources(this.lcRoleName3, "lcRoleName3");
            this.lcRoleName3.Location = new System.Drawing.Point(0, 0);
            this.lcRoleName3.Name = "lcRoleName3";
            this.lcRoleName3.Size = new System.Drawing.Size(462, 24);
            this.lcRoleName3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcRoleName3.TextSize = new System.Drawing.Size(50, 20);
            this.lcRoleName3.TextToControlDistance = 5;
            // 
            // lcRole3
            // 
            this.lcRole3.Control = this.txtRole3;
            resources.ApplyResources(this.lcRole3, "lcRole3");
            this.lcRole3.Location = new System.Drawing.Point(0, 24);
            this.lcRole3.MaxSize = new System.Drawing.Size(209, 27);
            this.lcRole3.MinSize = new System.Drawing.Size(209, 27);
            this.lcRole3.Name = "lcRole3";
            this.lcRole3.Size = new System.Drawing.Size(209, 27);
            this.lcRole3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcRole3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcRole3.TextSize = new System.Drawing.Size(50, 20);
            this.lcRole3.TextToControlDistance = 5;
            // 
            // lcSign3
            // 
            this.lcSign3.Control = this.picSign3;
            resources.ApplyResources(this.lcSign3, "lcSign3");
            this.lcSign3.Location = new System.Drawing.Point(209, 24);
            this.lcSign3.Name = "lcSign3";
            this.lcSign3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcSign3.Size = new System.Drawing.Size(224, 27);
            this.lcSign3.TextSize = new System.Drawing.Size(29, 13);
            // 
            // lcSign3Btn
            // 
            this.lcSign3Btn.Control = this.btnSign3;
            this.lcSign3Btn.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcSign3Btn, "lcSign3Btn");
            this.lcSign3Btn.Location = new System.Drawing.Point(433, 24);
            this.lcSign3Btn.MaxSize = new System.Drawing.Size(29, 27);
            this.lcSign3Btn.MinSize = new System.Drawing.Size(29, 27);
            this.lcSign3Btn.Name = "lcSign3Btn";
            this.lcSign3Btn.Size = new System.Drawing.Size(29, 27);
            this.lcSign3Btn.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcSign3Btn.TextSize = new System.Drawing.Size(0, 0);
            this.lcSign3Btn.TextToControlDistance = 0;
            this.lcSign3Btn.TextVisible = false;
            // 
            // lcIncludeAuditorSignNote
            // 
            this.lcIncludeAuditorSignNote.Control = this.chkIncludeAuditorSignNote;
            resources.ApplyResources(this.lcIncludeAuditorSignNote, "lcIncludeAuditorSignNote");
            this.lcIncludeAuditorSignNote.Location = new System.Drawing.Point(233, 0);
            this.lcIncludeAuditorSignNote.Name = "lcIncludeAuditorSignNote";
            this.lcIncludeAuditorSignNote.Size = new System.Drawing.Size(239, 23);
            this.lcIncludeAuditorSignNote.TextSize = new System.Drawing.Size(0, 0);
            this.lcIncludeAuditorSignNote.TextToControlDistance = 0;
            this.lcIncludeAuditorSignNote.TextVisible = false;
            // 
            // lcgReportCriteria
            // 
            resources.ApplyResources(this.lcgReportCriteria, "lcgReportCriteria");
            this.lcgReportCriteria.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgReportCriteria.GroupBordersVisible = false;
            this.lcgReportCriteria.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblCriteria,
            this.lblclose,
            this.lblSave,
            this.emptySpaceItem1,
            this.lcUpdateAssetDepreciation});
            this.lcgReportCriteria.Location = new System.Drawing.Point(0, 0);
            this.lcgReportCriteria.Name = "Root";
            this.lcgReportCriteria.OptionsItemText.TextToControlDistance = 5;
            this.lcgReportCriteria.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgReportCriteria.Size = new System.Drawing.Size(512, 346);
            this.lcgReportCriteria.TextVisible = false;
            // 
            // lblCriteria
            // 
            this.lblCriteria.Control = this.xtbLocation;
            resources.ApplyResources(this.lblCriteria, "lblCriteria");
            this.lblCriteria.Location = new System.Drawing.Point(0, 0);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(502, 310);
            this.lblCriteria.TextSize = new System.Drawing.Size(0, 0);
            this.lblCriteria.TextToControlDistance = 0;
            this.lblCriteria.TextVisible = false;
            // 
            // lblclose
            // 
            this.lblclose.Control = this.btnClose;
            resources.ApplyResources(this.lblclose, "lblclose");
            this.lblclose.Location = new System.Drawing.Point(429, 310);
            this.lblclose.MaxSize = new System.Drawing.Size(73, 26);
            this.lblclose.MinSize = new System.Drawing.Size(73, 26);
            this.lblclose.Name = "lblclose";
            this.lblclose.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 4, 2, 2);
            this.lblclose.Size = new System.Drawing.Size(73, 26);
            this.lblclose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblclose.TextSize = new System.Drawing.Size(0, 0);
            this.lblclose.TextToControlDistance = 0;
            this.lblclose.TextVisible = false;
            // 
            // lblSave
            // 
            this.lblSave.Control = this.btnOk;
            resources.ApplyResources(this.lblSave, "lblSave");
            this.lblSave.Location = new System.Drawing.Point(359, 310);
            this.lblSave.MaxSize = new System.Drawing.Size(70, 26);
            this.lblSave.MinSize = new System.Drawing.Size(70, 26);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(70, 26);
            this.lblSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblSave.TextSize = new System.Drawing.Size(0, 0);
            this.lblSave.TextToControlDistance = 0;
            this.lblSave.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(196, 310);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(163, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcUpdateAssetDepreciation
            // 
            this.lcUpdateAssetDepreciation.Control = this.btnUpdateAssetDepreciation;
            resources.ApplyResources(this.lcUpdateAssetDepreciation, "lcUpdateAssetDepreciation");
            this.lcUpdateAssetDepreciation.Location = new System.Drawing.Point(0, 310);
            this.lcUpdateAssetDepreciation.Name = "lcUpdateAssetDepreciation";
            this.lcUpdateAssetDepreciation.Size = new System.Drawing.Size(196, 26);
            this.lcUpdateAssetDepreciation.TextSize = new System.Drawing.Size(0, 0);
            this.lcUpdateAssetDepreciation.TextToControlDistance = 0;
            this.lcUpdateAssetDepreciation.TextVisible = false;
            // 
            // colAttachAllLeger
            // 
            resources.ApplyResources(this.colAttachAllLeger, "colAttachAllLeger");
            this.colAttachAllLeger.FieldName = "ATTACHLEDGER";
            this.colAttachAllLeger.Name = "colAttachAllLeger";
            // 
            // colLedger
            // 
            resources.ApplyResources(this.colLedger, "colLedger");
            this.colLedger.FieldName = "LEDGER";
            this.colLedger.Name = "colLedger";
            // 
            // colGroup
            // 
            resources.ApplyResources(this.colGroup, "colGroup");
            this.colGroup.FieldName = "GROUP";
            this.colGroup.Name = "colGroup";
            // 
            // colDailyBalance
            // 
            resources.ApplyResources(this.colDailyBalance, "colDailyBalance");
            this.colDailyBalance.FieldName = "DAILYBALANCE";
            this.colDailyBalance.Name = "colDailyBalance";
            // 
            // imgCollection
            // 
            this.imgCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgCollection.ImageStream")));
            this.imgCollection.Images.SetKeyName(0, "CheckBox.jpg");
            this.imgCollection.Images.SetKeyName(1, "CheckBoxChecked.jpg");
            // 
            // chkProject
            // 
            resources.ApplyResources(this.chkProject, "chkProject");
            this.chkProject.Name = "chkProject";
            this.chkProject.Properties.Caption = resources.GetString("chkProject.Properties.Caption");
            this.chkProject.CheckedChanged += new System.EventHandler(this.chkProject_CheckedChanged);
            // 
            // chkBank
            // 
            resources.ApplyResources(this.chkBank, "chkBank");
            this.chkBank.Name = "chkBank";
            this.chkBank.Properties.Caption = resources.GetString("chkBank.Properties.Caption");
            this.chkBank.CheckedChanged += new System.EventHandler(this.chkBank_CheckedChanged);
            // 
            // chkLedger
            // 
            resources.ApplyResources(this.chkLedger, "chkLedger");
            this.chkLedger.Name = "chkLedger";
            this.chkLedger.Properties.Caption = resources.GetString("chkLedger.Properties.Caption");
            this.chkLedger.CheckedChanged += new System.EventHandler(this.chkLedger_CheckedChanged);
            // 
            // chkLedgerGroup
            // 
            resources.ApplyResources(this.chkLedgerGroup, "chkLedgerGroup");
            this.chkLedgerGroup.Name = "chkLedgerGroup";
            this.chkLedgerGroup.Properties.Caption = resources.GetString("chkLedgerGroup.Properties.Caption");
            this.chkLedgerGroup.CheckedChanged += new System.EventHandler(this.chkLedgerGroup_CheckedChanged);
            // 
            // chkCostCentre
            // 
            resources.ApplyResources(this.chkCostCentre, "chkCostCentre");
            this.chkCostCentre.Name = "chkCostCentre";
            this.chkCostCentre.Properties.Caption = resources.GetString("chkCostCentre.Properties.Caption");
            this.chkCostCentre.CheckedChanged += new System.EventHandler(this.chkCostCentre_CheckedChanged);
            // 
            // chkPartyLedger
            // 
            resources.ApplyResources(this.chkPartyLedger, "chkPartyLedger");
            this.chkPartyLedger.Name = "chkPartyLedger";
            this.chkPartyLedger.Properties.Caption = resources.GetString("chkPartyLedger.Properties.Caption");
            this.chkPartyLedger.CheckedChanged += new System.EventHandler(this.chkPartyLedger_CheckedChanged);
            // 
            // chkNatureofPayments
            // 
            resources.ApplyResources(this.chkNatureofPayments, "chkNatureofPayments");
            this.chkNatureofPayments.Name = "chkNatureofPayments";
            this.chkNatureofPayments.Properties.Caption = resources.GetString("chkNatureofPayments.Properties.Caption");
            this.chkNatureofPayments.CheckedChanged += new System.EventHandler(this.chkNatureofPayments_CheckedChanged);
            // 
            // chkDeducteeType
            // 
            resources.ApplyResources(this.chkDeducteeType, "chkDeducteeType");
            this.chkDeducteeType.Name = "chkDeducteeType";
            this.chkDeducteeType.Properties.Caption = resources.GetString("chkDeducteeType.Properties.Caption");
            this.chkDeducteeType.CheckedChanged += new System.EventHandler(this.chkDeducteeType_CheckedChanged);
            // 
            // chkPayroll
            // 
            resources.ApplyResources(this.chkPayroll, "chkPayroll");
            this.chkPayroll.Name = "chkPayroll";
            this.chkPayroll.Properties.Caption = resources.GetString("chkPayroll.Properties.Caption");
            this.chkPayroll.CheckedChanged += new System.EventHandler(this.chkPayroll_CheckedChanged);
            // 
            // chkPayrollStaff
            // 
            resources.ApplyResources(this.chkPayrollStaff, "chkPayrollStaff");
            this.chkPayrollStaff.Name = "chkPayrollStaff";
            this.chkPayrollStaff.Properties.Caption = resources.GetString("chkPayrollStaff.Properties.Caption");
            this.chkPayrollStaff.CheckedChanged += new System.EventHandler(this.chkPayrollStaff_CheckedChanged);
            // 
            // chkPayrollComponents
            // 
            resources.ApplyResources(this.chkPayrollComponents, "chkPayrollComponents");
            this.chkPayrollComponents.Name = "chkPayrollComponents";
            this.chkPayrollComponents.Properties.Caption = resources.GetString("chkPayrollComponents.Properties.Caption");
            this.chkPayrollComponents.CheckedChanged += new System.EventHandler(this.chkPayrollComponents_CheckedChanged);
            // 
            // chkItems
            // 
            resources.ApplyResources(this.chkItems, "chkItems");
            this.chkItems.Name = "chkItems";
            this.chkItems.Properties.Caption = resources.GetString("chkItems.Properties.Caption");
            this.chkItems.CheckedChanged += new System.EventHandler(this.chkItems_CheckedChanged);
            // 
            // chkLocationSelectAll
            // 
            resources.ApplyResources(this.chkLocationSelectAll, "chkLocationSelectAll");
            this.chkLocationSelectAll.Name = "chkLocationSelectAll";
            this.chkLocationSelectAll.Properties.Caption = resources.GetString("chkLocationSelectAll.Properties.Caption");
            this.chkLocationSelectAll.CheckedChanged += new System.EventHandler(this.chkLocationSelectAll_CheckedChanged);
            // 
            // chkselectAllAssetclass
            // 
            resources.ApplyResources(this.chkselectAllAssetclass, "chkselectAllAssetclass");
            this.chkselectAllAssetclass.Name = "chkselectAllAssetclass";
            this.chkselectAllAssetclass.Properties.Caption = resources.GetString("chkselectAllAssetclass.Properties.Caption");
            this.chkselectAllAssetclass.CheckedChanged += new System.EventHandler(this.chkselectAllAssetclass_CheckedChanged);
            // 
            // chkRegistrationType
            // 
            resources.ApplyResources(this.chkRegistrationType, "chkRegistrationType");
            this.chkRegistrationType.Name = "chkRegistrationType";
            this.chkRegistrationType.Properties.Caption = resources.GetString("chkRegistrationType.Properties.Caption");
            this.chkRegistrationType.CheckedChanged += new System.EventHandler(this.chkRegistrationType_CheckedChanged);
            // 
            // chkCountry
            // 
            resources.ApplyResources(this.chkCountry, "chkCountry");
            this.chkCountry.Name = "chkCountry";
            this.chkCountry.Properties.Caption = resources.GetString("chkCountry.Properties.Caption");
            this.chkCountry.CheckedChanged += new System.EventHandler(this.chkCountry_CheckedChanged);
            // 
            // chkState
            // 
            resources.ApplyResources(this.chkState, "chkState");
            this.chkState.Name = "chkState";
            this.chkState.Properties.Caption = resources.GetString("chkState.Properties.Caption");
            this.chkState.CheckedChanged += new System.EventHandler(this.chkState_CheckedChanged);
            // 
            // chkLanguage
            // 
            resources.ApplyResources(this.chkLanguage, "chkLanguage");
            this.chkLanguage.Name = "chkLanguage";
            this.chkLanguage.Properties.Caption = resources.GetString("chkLanguage.Properties.Caption");
            this.chkLanguage.CheckedChanged += new System.EventHandler(this.chkLanguage_CheckedChanged);
            // 
            // chkStateDonaud
            // 
            resources.ApplyResources(this.chkStateDonaud, "chkStateDonaud");
            this.chkStateDonaud.Name = "chkStateDonaud";
            this.chkStateDonaud.Properties.Caption = resources.GetString("chkStateDonaud.Properties.Caption");
            this.chkStateDonaud.CheckedChanged += new System.EventHandler(this.chkStateDonaud_CheckedChanged);
            // 
            // chkDonaud
            // 
            resources.ApplyResources(this.chkDonaud, "chkDonaud");
            this.chkDonaud.Name = "chkDonaud";
            this.chkDonaud.Properties.Caption = resources.GetString("chkDonaud.Properties.Caption");
            this.chkDonaud.CheckedChanged += new System.EventHandler(this.chkDonaud_CheckedChanged);
            // 
            // chkSelectAllTask
            // 
            resources.ApplyResources(this.chkSelectAllTask, "chkSelectAllTask");
            this.chkSelectAllTask.Name = "chkSelectAllTask";
            this.chkSelectAllTask.Properties.Caption = resources.GetString("chkSelectAllTask.Properties.Caption");
            this.chkSelectAllTask.CheckedChanged += new System.EventHandler(this.chkSelectAllTask_CheckedChanged);
            // 
            // chkBudget
            // 
            resources.ApplyResources(this.chkBudget, "chkBudget");
            this.chkBudget.Name = "chkBudget";
            this.chkBudget.Properties.Caption = resources.GetString("chkBudget.Properties.Caption");
            this.chkBudget.StyleController = this.lcBudget;
            this.chkBudget.CheckedChanged += new System.EventHandler(this.chkBudget_CheckedChanged);
            // 
            // frmReportFilter
            // 
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("frmReportFilter.Appearance.Font")));
            this.Appearance.Options.UseFont = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.chkBudget);
            this.Controls.Add(this.chkSelectAllTask);
            this.Controls.Add(this.chkDonaud);
            this.Controls.Add(this.chkStateDonaud);
            this.Controls.Add(this.chkLanguage);
            this.Controls.Add(this.chkState);
            this.Controls.Add(this.chkCountry);
            this.Controls.Add(this.chkRegistrationType);
            this.Controls.Add(this.chkselectAllAssetclass);
            this.Controls.Add(this.chkLocationSelectAll);
            this.Controls.Add(this.chkItems);
            this.Controls.Add(this.chkPayrollComponents);
            this.Controls.Add(this.chkPayrollStaff);
            this.Controls.Add(this.chkPayroll);
            this.Controls.Add(this.chkDeducteeType);
            this.Controls.Add(this.chkNatureofPayments);
            this.Controls.Add(this.chkPartyLedger);
            this.Controls.Add(this.chkCostCentre);
            this.Controls.Add(this.chkLedgerGroup);
            this.Controls.Add(this.chkLedger);
            this.Controls.Add(this.chkBank);
            this.Controls.Add(this.chkProject);
            this.Controls.Add(this.pnlReportCriteria);
            this.Controls.Add(this.pnlReportTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportFilter";
            this.Load += new System.EventHandler(this.frmReportFilter_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportFilter_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.rchkState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlReportTitle)).EndInit();
            this.pnlReportTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReportTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlReportCriteria)).EndInit();
            this.pnlReportCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcReportCriteria)).EndInit();
            this.lcReportCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtbLocation)).EndInit();
            this.xtbLocation.ResumeLayout(false);
            this.xtpCostCentre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcCostCentre)).EndInit();
            this.lcCostCentre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpCCCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCCCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterCostCenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroupCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblcostCentreGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCCCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCCCategory)).EndInit();
            this.xtpDate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcDateCriteria)).EndInit();
            this.lcDateCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rboInKind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcReportCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvReportCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkReportCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDateProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWithInKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.xtpProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).EndInit();
            this.lcProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpITRGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkBankColumn1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiColumn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkbankColumn2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiColumn2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBudgetCompare.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpSociety.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBankFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProjectFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkBankCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelectProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpBudget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProjectwithDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProjectGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMultiColumnBankChooser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBankAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSociety)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSociety)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciITRGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBudgetCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompareBudget1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompareBudget2)).EndInit();
            this.xtpLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcLedger)).EndInit();
            this.lcLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerGroupFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkledger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.xrtpPartyLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcTDSParties)).EndInit();
            this.lcTDSParties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPartyFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSParties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSParties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPartyLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTDSParties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lclTDSParties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            this.xtpNarration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcNarration)).EndInit();
            this.lcNarration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroupNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarrationGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            this.xrtabDeducteeType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcDeducteeType)).EndInit();
            this.lcDeducteeType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDeducteeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeducteeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkDeducteeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDeducteeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            this.xtpPayroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpPayroll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPayrollCom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPayrollGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPayroll)).EndInit();
            this.xtpPayrollComponent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPayStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPayrollStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkPayroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPayRollStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            this.xtpItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkStockShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkStockSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgStockItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            this.xtpLocation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkLocationShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkLocationSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            this.xtbDynamicConditions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            this.xtpBankFDAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkbankFDFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBankFDAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBankFDAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).EndInit();
            this.xtpAssetClass.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).EndInit();
            this.layoutControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAssetShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAssetClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkAssetclass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAssetClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            this.xtbRegistrationType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).EndInit();
            this.layoutControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowRegistrationFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRegistrationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRegistrationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelectRegisterType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgRegistrationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).EndInit();
            this.xtbCountry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).EndInit();
            this.layoutControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowStateFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowCountryFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkStateSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCountrySelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).EndInit();
            this.xtpLanguage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).EndInit();
            this.layoutControl10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowLanguageFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).EndInit();
            this.xtpStateDonaud.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).EndInit();
            this.layoutControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStateDonaud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStateDonaud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchStateCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgStatName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            this.xtpDonaud.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl12)).EndInit();
            this.layoutControl12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowDonaud.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDonaud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonaud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkDonaud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDonar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).EndInit();
            this.xtpFeestDayTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl13)).EndInit();
            this.layoutControl13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpFeestDatTask.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFeestDayTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem21)).EndInit();
            this.xtpNetworkingTasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl17)).EndInit();
            this.layoutControl17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelectTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTaskFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem58)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem57)).EndInit();
            this.xtpAnniversaryType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl14)).EndInit();
            this.layoutControl14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgAnniversaryType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem55)).EndInit();
            this.xtbBudget.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcBudget)).EndInit();
            this.lcBudget.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetNewProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetNewProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkIncludeReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPExpenseAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPIncomeAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtGHelpAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rctxtPHelpAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtBudgetNewProjectRemarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBudgetFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpBudgetGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpBudgetNewProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBudgetNewProject)).EndInit();
            this.xtpReportSetup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcReport)).EndInit();
            this.lcReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpReportSetupCurrencyCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeAllBudgetLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowTOC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboReportType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSocietyWithInstutionName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboColumnHeaderFontStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowIndividualProjects.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowZeroValues.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBorderStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowProjectinFooter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbReportTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPageNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVerticalLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowReportLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisplayTitles.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSoryByGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSortByLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGroupCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTitleAlignment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHorizontalLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShowGroupCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportSetup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportPageHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHorizontalLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReportDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBorderStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShowReportLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem59)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColumnHeaderFontStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSocietyWithInstutionName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciReportCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcShowTOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcIncludeAllBudgetLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcReportSetupCurrency)).EndInit();
            this.xtbSign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcSignDetails)).EndInit();
            this.lcSignDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeAuditorSignNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeSignDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSign1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSign2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSign3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcIncludeSignDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpSign1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRoleName1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign1Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpSign2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRoleName2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRole2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign2Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpSign3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRoleName3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRole3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSign3Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcIncludeAuditorSignNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgReportCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCriteria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblclose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpdateAssetDepreciation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCostCentre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPartyLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNatureofPayments.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDeducteeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayroll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayrollStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPayrollComponents.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkItems.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocationSelectAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkselectAllAssetclass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRegistrationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLanguage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStateDonaud.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDonaud.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectAllTask.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBudget.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlReportTitle;
        private DevExpress.XtraEditors.PanelControl pnlReportCriteria;
        private DevExpress.XtraLayout.LayoutControl lcReportCriteria;
        private DevExpress.XtraLayout.LayoutControlGroup lcgReportCriteria;
        private DevExpress.XtraTab.XtraTabControl xtbLocation;
        private DevExpress.XtraTab.XtraTabPage xtpDate;
        private DevExpress.XtraTab.XtraTabPage xtpProject;
        private DevExpress.XtraLayout.LayoutControlItem lblCriteria;
        private DevExpress.XtraTab.XtraTabPage xtpLedger;
        private DevExpress.XtraTab.XtraTabPage xtpCostCentre;
        private DevExpress.XtraTab.XtraTabPage xtpNarration;
        private DevExpress.XtraTab.XtraTabPage xtpReportSetup;
        private DevExpress.XtraLayout.LayoutControl lcTitle;
        private DevExpress.XtraLayout.LayoutControlGroup lcgReportTitle;
        private DevExpress.XtraLayout.SimpleLabelItem lblReportTitle;
        private DevExpress.XtraLayout.LayoutControl lcDateCriteria;
        private DevExpress.XtraEditors.DateEdit DateTo;
        private DevExpress.XtraEditors.DateEdit DateFrom;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDateProperty;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDate;
        private DevExpress.XtraLayout.LayoutControlItem lblDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblDateTo;
        private DevExpress.XtraLayout.EmptySpaceItem emptyDateFrom;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem lblclose;
        private DevExpress.XtraLayout.LayoutControlItem lblSave;
        private DevExpress.XtraLayout.LayoutControl lcProject;
        private DevExpress.XtraLayout.LayoutControlGroup lcgProject;
        private DevExpress.XtraGrid.Columns.GridColumn colAttachAllLeger;
        private DevExpress.XtraGrid.Columns.GridColumn colLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colDailyBalance;
        private DevExpress.XtraGrid.GridControl gcReportCriteria;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReportCriteria;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colCriteriaName;
        private DevExpress.XtraGrid.Columns.GridColumn colCriteriaType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkReportCriteria;
        private DevExpress.XtraLayout.LayoutControlItem lblGridControl;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox;
        private DevExpress.XtraLayout.LayoutControlGroup lcgProjectwithDivision;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBankAccount;
        private DevExpress.XtraGrid.GridControl gcProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProject;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelectProject;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraLayout.LayoutControlItem lblProjectGrid;
        private DevExpress.XtraGrid.GridControl gcBank;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBank;
        private DevExpress.XtraGrid.Columns.GridColumn colBankId;
        private DevExpress.XtraGrid.Columns.GridColumn colBankCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkBankCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colBankName;
        private DevExpress.XtraLayout.LayoutControlItem lblBankGrid;
        private DevExpress.XtraLayout.LayoutControl lcLedger;
        private DevExpress.XtraLayout.LayoutControlGroup lcgLedgerGroup;
        private DevExpress.XtraLayout.LayoutControl lcCostCentre;
        private DevExpress.XtraLayout.LayoutControlGroup lcgCostCentre;
        private DevExpress.XtraLayout.LayoutControlGroup lcgGroupCostCentre;
        private DevExpress.XtraGrid.GridControl gcCostCentre;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCostCentre;
        private DevExpress.XtraLayout.LayoutControlItem lblcostCentreGrid;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreID;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreName;
        private DevExpress.XtraLayout.LayoutControl lcNarration;
        private DevExpress.XtraGrid.GridControl gcNarration;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNarration;
        private DevExpress.XtraLayout.LayoutControlGroup lcgNarration;
        private DevExpress.XtraLayout.LayoutControlGroup lcgGroupNarration;
        private DevExpress.XtraLayout.LayoutControlItem lblNarrationGrid;
        private DevExpress.XtraGrid.Columns.GridColumn colNarrationID;
        private DevExpress.XtraGrid.Columns.GridColumn colNarrationCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkNarration;
        private DevExpress.XtraGrid.Columns.GridColumn colNarration;
        private DevExpress.XtraLayout.LayoutControl lcReport;
        private DevExpress.XtraEditors.CheckEdit chkGroupCode;
        private DevExpress.XtraEditors.CheckEdit chkLedgerCode;
        private DevExpress.XtraLayout.LayoutControlGroup lcgReportSetup;
        private DevExpress.XtraEditors.CheckEdit chkHorizontalLine;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraEditors.ComboBoxEdit cboSoryByGroup;
        private DevExpress.XtraEditors.ComboBoxEdit cboSortByLedger;
        private DevExpress.XtraEditors.ComboBoxEdit cboTitleAlignment;
        private DevExpress.XtraEditors.DateEdit ReportDate;
        private DevExpress.XtraLayout.LayoutControlGroup lcgReportPageHeader;
        private DevExpress.XtraLayout.LayoutControlItem lblLedgerCode;
        private DevExpress.XtraLayout.LayoutControlItem lblShowGroupCode;
        private DevExpress.XtraLayout.LayoutControlItem lblHorizontalLine;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblTitle;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraLayout.LayoutControlItem lblReportDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem12;
        private DevExpress.Utils.ImageCollection imgCollection;
        private DevExpress.XtraGrid.Columns.GridColumn colBankGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colBankProjectId;
        private DevExpress.XtraGrid.GridControl gcLedgerDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedgerDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkledger;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.GridControl gcLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colledgerGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerGroupId;
        private DevExpress.XtraEditors.CheckEdit chkPageNumber;
        private DevExpress.XtraEditors.CheckEdit chkVerticalLine;
        private DevExpress.XtraEditors.CheckEdit chkShowReportLogo;
        private DevExpress.XtraEditors.CheckEdit chkDisplayTitles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem lblShowReportLogo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private RadioGroup rboInKind;
        private DevExpress.XtraLayout.LayoutControlItem lblWithInKind;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private CheckEdit chkProjectFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private CheckEdit chkBankFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem lblLedgerGroup;
        private CheckEdit chkShowFilterCostCenter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlGroup grpLedgerGroup;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpLedger;
        private CheckEdit chkLedgerFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private CheckEdit chkLedgerGroupFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private CheckEdit chkNarration;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private RadioGroup rgbReportTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private CheckEdit chkShowProjectinFooter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private ComboBoxEdit cboBorderStyle;
        private DevExpress.XtraLayout.LayoutControlItem lblBorderStyle;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentcode;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraLayout.LayoutControlGroup lcgSociety;
        private GridLookUpEdit glkpSociety;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colSocietyId;
        private DevExpress.XtraGrid.Columns.GridColumn colSocietyName;
        private DevExpress.XtraLayout.LayoutControlItem lcSociety;
        private RadioGroup rgbAddress;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraTab.XtraTabPage xrtabDeducteeType;
        private DevExpress.XtraLayout.LayoutControl lcDeducteeType;
        private DevExpress.XtraGrid.GridControl gcDeducteeType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDeducteeType;
        private DevExpress.XtraGrid.Columns.GridColumn colDeducteeTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colDeducteeName;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDeducteeType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlGroup lcgGroup;
        private CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraTab.XtraTabPage xrtpPartyLedger;
        private DevExpress.XtraLayout.LayoutControl lcTDSParties;
        private CheckEdit chkPartyFilter;
        private DevExpress.XtraGrid.GridControl gcTDSParties;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTDSParties;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSPartiesId;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSPartiesName;
        private DevExpress.XtraLayout.LayoutControlGroup lcgTDSParties;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem lclTDSParties;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBudgetCompare;
        private GridLookUpEdit glkpBudgetCompare;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lcCompareBudget1;
        private DevExpress.XtraLayout.LayoutControlItem lcCompareBudget2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private GridLookUpEdit glkpBudget;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraTab.XtraTabPage xtpPayroll;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraGrid.GridControl gcGroups;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGroups;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraGrid.Columns.GridColumn colGid;
        private DevExpress.XtraGrid.Columns.GridColumn colGrpName;
        private DevExpress.XtraLayout.LayoutControlGroup lcgPayrollGroup;
        private GridLookUpEdit glkpPayroll;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem13;
        private DevExpress.XtraLayout.LayoutControlItem lcPayroll;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollId;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollName;
        private DevExpress.XtraTab.XtraTabPage xtpPayrollComponent;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraGrid.GridControl gcPayStaff;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPayrollStaff;
        private DevExpress.XtraGrid.GridControl gcPayComponent;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPayComponent;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem15;
        private DevExpress.XtraLayout.LayoutControlGroup lcgComponent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlGroup lcgPayRollStaff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colStaffId;
        private DevExpress.XtraGrid.Columns.GridColumn colStaffName;
        private DevExpress.XtraGrid.Columns.GridColumn colPartyLedger;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkPartyLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductee;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkDeducteeType;
        private DevExpress.XtraGrid.Columns.GridColumn colPayroll;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkPayrollCom;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollStaff;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkPayrollStaff;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkPayroll;
        private CheckEdit chkProject;
        private CheckEdit chkBank;
        private CheckEdit chkLedger;
        private CheckEdit chkLedgerGroup;
        private CheckEdit chkCostCentre;
        private CheckEdit chkPartyLedger;
        private CheckEdit chkNatureofPayments;
        private CheckEdit chkDeducteeType;
        private CheckEdit chkPayroll;
        private CheckEdit chkPayrollStaff;
        private CheckEdit chkPayrollComponents;
        private DevExpress.XtraTab.XtraTabPage xtpItem;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraGrid.GridControl gcItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItem;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colStockSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkStockSelect;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlGroup lcgStockItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private CheckEdit chkItems;
        private CheckEdit chkStockShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraTab.XtraTabPage xtpLocation;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private CheckEdit chkLocationShowFilter;
        private DevExpress.XtraGrid.GridControl gcLocation;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationId;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkLocationSelect;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlGroup lgGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private CheckEdit chkLocationSelectAll;
        private DevExpress.XtraTab.XtraTabPage xtbDynamicConditions;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem14;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem16;
        private ComboBoxEdit cboCondition;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private TextEdit txtAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraGrid.Columns.GridColumn colFDAccountId;
        private CheckEdit chkShowZeroValues;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private GridLookUpEdit glkbankColumn2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMultiColumn2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMultiColumnBankChooser;
        private GridLookUpEdit glkBankColumn1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMultiColumn1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMultiBank;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMultiLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMultibankName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMultiColBank;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMulticolLedgerid;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMulticolumnbankName;
        private DevExpress.XtraTab.XtraTabPage xtpBankFDAccount;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraGrid.GridControl gcBankFDAccounts;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBankFDAccounts;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraGrid.Columns.GridColumn gcolaccBankName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolaccFDAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn gcolAccType;
        private DevExpress.XtraGrid.Columns.GridColumn gcolAccLedgerId;
        private CheckEdit chkbankFDFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraTab.XtraTabPage xtpAssetClass;
        private DevExpress.XtraLayout.LayoutControl layoutControl7;
        private DevExpress.XtraGrid.GridControl gcAssetClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetClass;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClassId;
        private DevExpress.XtraGrid.Columns.GridColumn colChkSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClass;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkAssetclass;
        private CheckEdit chkselectAllAssetclass;
        private DevExpress.XtraLayout.LayoutControlGroup lcgAssetClass;
        private CheckEdit chkAssetShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraTab.XtraTabPage xtbRegistrationType;
        private DevExpress.XtraTab.XtraTabPage xtbCountry;
        private DevExpress.XtraLayout.LayoutControl layoutControl8;
        private DevExpress.XtraGrid.GridControl gcRegistrationType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRegistrationType;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistrationTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colSelectReg;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelectRegisterType;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistrationType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup14;
        private DevExpress.XtraLayout.LayoutControlGroup lcgRegistrationType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private CheckEdit chkRegistrationType;
        private DevExpress.XtraLayout.LayoutControl layoutControl9;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup15;
        private DevExpress.XtraLayout.LayoutControlGroup lcgCountry;
        private DevExpress.XtraLayout.LayoutControlGroup lcgState;
        private DevExpress.XtraGrid.GridControl gcState;
        private DevExpress.XtraGrid.Views.Grid.GridView gvState;
        private DevExpress.XtraGrid.GridControl gcCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCountry;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
        private DevExpress.XtraGrid.Columns.GridColumn colStateId;
        private DevExpress.XtraGrid.Columns.GridColumn colStateSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkStateSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkCountrySelect;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private CheckEdit chkCountry;
        private CheckEdit chkState;
        private CheckEdit chkShowRegistrationFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem43;
        private CheckEdit chkShowStateFilter;
        private CheckEdit chkShowCountryFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem44;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem45;
        private DevExpress.XtraTab.XtraTabPage xtpLanguage;
        private DevExpress.XtraLayout.LayoutControl layoutControl10;
        private DevExpress.XtraGrid.GridControl gcLanguage;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLanguage;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckLanguage;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkLanguage;
        private DevExpress.XtraGrid.Columns.GridColumn colLanguage;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup16;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem18;
        private DevExpress.XtraLayout.LayoutControlGroup lcgLanguage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private CheckEdit chkLanguage;
        private CheckEdit chkShowLanguageFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem47;
        private DevExpress.XtraTab.XtraTabPage xtpStateDonaud;
        private DevExpress.XtraLayout.LayoutControl layoutControl11;
        private DevExpress.XtraGrid.GridControl gcStateDonaud;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStateDonaud;
        private DevExpress.XtraGrid.Columns.GridColumn colStateDonauId;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckState;
        private DevExpress.XtraGrid.Columns.GridColumn colStateDonaudName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup17;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem20;
        private DevExpress.XtraLayout.LayoutControlGroup lcgStatName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem48;
        private DevExpress.XtraTab.XtraTabPage xtpDonaud;
        private CheckEdit chkStateDonaud;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkState;
        private DevExpress.XtraLayout.LayoutControl layoutControl12;
        private DevExpress.XtraGrid.GridControl gcDonaud;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDonaud;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup18;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDonar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem49;
        private DevExpress.XtraGrid.Columns.GridColumn colDonaudId;
        private DevExpress.XtraGrid.Columns.GridColumn colDonaudCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkDonaud;
        private DevExpress.XtraGrid.Columns.GridColumn colDonaud;
        private CheckEdit chkDonaud;
        private CheckEdit chkShowState;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private CheckEdit chkShowDonaud;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem51;
        private DevExpress.XtraTab.XtraTabPage xtpFeestDayTask;
        private DevExpress.XtraLayout.LayoutControl layoutControl13;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup19;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchStateCheck;
        private DevExpress.XtraTab.XtraTabPage xtpAnniversaryType;
        private DevExpress.XtraLayout.LayoutControl layoutControl14;
        private RadioGroup rgAnniversaryType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem54;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem19;
        private LabelControl lblType;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem24;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem55;
        private GridLookUpEdit glkpFeestDatTask;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn colTagID;
        private DevExpress.XtraGrid.Columns.GridColumn colTagName;
        private DevExpress.XtraLayout.LayoutControlItem lblFeestDayTask;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem21;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraTab.XtraTabPage xtpNetworkingTasks;
        private DevExpress.XtraLayout.LayoutControl layoutControl17;
        private DevExpress.XtraGrid.GridControl gcTasks;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTasks;
        private DevExpress.XtraGrid.Columns.GridColumn coltaskCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelectTask;
        private DevExpress.XtraGrid.Columns.GridColumn colTaskId;
        private DevExpress.XtraGrid.Columns.GridColumn colRefId;
        private DevExpress.XtraGrid.Columns.GridColumn colTaskName;
        private CheckEdit chkTaskFilter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem57;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem58;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup24;
        private CheckEdit chkSelectAllTask;
        private CheckEdit chkShowIndividualProjects;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem59;
        private ComboBoxEdit cboColumnHeaderFontStyle;
        private DevExpress.XtraLayout.LayoutControlItem lblColumnHeaderFontStyle;
        private DevExpress.XtraTab.XtraTabPage xtbSign;
        private DevExpress.XtraLayout.LayoutControl lcSignDetails;
        private CheckEdit chkIncludeSignDetails;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lcIncludeSignDetails;
        private TextEdit txtRole1;
        private TextEdit txtRoleName1;
        private DevExpress.XtraLayout.LayoutControlItem lcRoleName1;
        private DevExpress.XtraLayout.LayoutControlItem lcRole;
        private TextEdit txtRole2;
        private TextEdit txtRoleName2;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpSign1;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpSign2;
        private DevExpress.XtraLayout.LayoutControlItem lcRoleName2;
        private DevExpress.XtraLayout.LayoutControlItem lcRole2;
        private TextEdit txtRole3;
        private TextEdit txtRoleName3;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpSign3;
        private DevExpress.XtraLayout.LayoutControlItem lcRoleName3;
        private DevExpress.XtraLayout.LayoutControlItem lcRole3;
        private DevExpress.XtraTab.XtraTabPage xtbBudget;
        private DevExpress.XtraLayout.LayoutControl lcBudget;
        private CheckEdit chkBudgetFilter;
        private DevExpress.XtraGrid.GridControl gcBudget;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBudget;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetId;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetSelection;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetName;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetType;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetDateFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetDateTo;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetProject;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup21;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpBudgetGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private CheckEdit chkBudget;
        private CheckEdit chkSocietyWithInstutionName;
        private DevExpress.XtraLayout.LayoutControlItem lcSocietyWithInstutionName;
        private DevExpress.XtraGrid.GridControl gcBudgetNewProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBudgetNewProject;
        private DevExpress.XtraGrid.Columns.GridColumn gcAcId;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetNewProject;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPIncomeAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPExpenseAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ColPProvinceHelp;
        private DevExpress.XtraLayout.LayoutControlItem lcBudgetNewProject;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpBudgetNewProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtBudgetNewProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtPIncomeAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtPExpenseAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtPHelpAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDeleteBudgetNewProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetMonthRowName;
        private DevExpress.XtraLayout.LayoutControlItem lcSign1;
        private DevExpress.XtraLayout.LayoutControlItem lcSign2;
        private DevExpress.XtraLayout.LayoutControlItem lcSign3;
        private PictureEdit picSign1;
        private PictureEdit picSign2;
        private PictureEdit picSign3;
        private SimpleButton btnSign1;
        private DevExpress.XtraLayout.LayoutControlItem lcSign1Btn;
        private SimpleButton btnSign2;
        private DevExpress.XtraLayout.LayoutControlItem lcSign2Btn;
        private SimpleButton btnSign3;
        private DevExpress.XtraLayout.LayoutControlItem lcSign3Btn;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPGovtIncomeAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rctxtGHelpAmt;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetIncludeReports;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkIncludeReports;
        private DevExpress.XtraGrid.Columns.GridColumn ColBudgetPRemakrs;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtBudgetNewProjectRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetAction;
        private ComboBoxEdit cboReportType;
        private DevExpress.XtraLayout.LayoutControlItem lciReportCode;
        private CheckEdit chkShowTOC;
        private DevExpress.XtraLayout.LayoutControlItem lcShowTOC;
        private CheckEdit chkIncludeAllBudgetLedgers;
        private DevExpress.XtraLayout.LayoutControlItem lcIncludeAllBudgetLedgers;
        private CheckEdit chkIncludeAuditorSignNote;
        private DevExpress.XtraLayout.LayoutControlItem lcIncludeAuditorSignNote;
        private SimpleButton btnUpdateAssetDepreciation;
        private DevExpress.XtraLayout.LayoutControlItem lcUpdateAssetDepreciation;
        private GridLookUpEdit glkpITRGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraLayout.LayoutControlItem lciITRGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colITRGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colITRGroupName;
        private GridLookUpEdit glkpReportSetupCurrencyCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCurrencyCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencySymbol;
        private DevExpress.XtraLayout.LayoutControlItem lcReportSetupCurrency;
        private GridLookUpEdit glkpCCCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCCCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcCCCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn gcCCCategory;
        private DevExpress.XtraLayout.LayoutControlItem lcCCCategory;
        private DevExpress.XtraLayout.LayoutControlGroup lcgCCCategory;

    }
}