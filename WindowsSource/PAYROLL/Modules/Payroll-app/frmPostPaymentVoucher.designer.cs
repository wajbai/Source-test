namespace PAYROLL.Modules.Payroll_app
{
    partial class frmPostPaymentVoucher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPostPaymentVoucher));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.cbDrPostComponent = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTotalNetAmount = new DevExpress.XtraEditors.LabelControl();
            this.gcPayrollCompDetails = new DevExpress.XtraGrid.GridControl();
            this.gvPayrollCompDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPayDetailPayGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayDetailComponentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayDetailLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayDetailPayGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayDetailComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayDetailEarnAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayDetailDedctionAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.chkCBPayGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtChequeRefNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtNarration = new DevExpress.XtraEditors.TextEdit();
            this.glkpCashBank = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvCashBankLedgers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCashBankLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCashBankLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCompLedgerAmount = new DevExpress.XtraGrid.GridControl();
            this.gvCompLedgerAmount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPayrollid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpLedger = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpComponent = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.dteDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGrpPayrollComponentDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPayrollGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPayrollDetails = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcDrPostComponent = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcApply = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGrpPayrollVoucher = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcCompLedgerAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcNarration = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcChequeDDRefNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCashBankLedger = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcLblCashBankBalance = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcTotalNETAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbDrPostComponent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayrollCompDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollCompDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCBPayGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChequeRefNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCashBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCashBankLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCompLedgerAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompLedgerAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpPayrollComponentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPayrollGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPayrollDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDrPostComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpPayrollVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompLedgerAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChequeDDRefNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCashBankLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLblCashBankBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTotalNETAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.cbDrPostComponent);
            this.layoutControl1.Controls.Add(this.lblTotalNetAmount);
            this.layoutControl1.Controls.Add(this.gcPayrollCompDetails);
            this.layoutControl1.Controls.Add(this.chkCBPayGroup);
            this.layoutControl1.Controls.Add(this.txtChequeRefNumber);
            this.layoutControl1.Controls.Add(this.txtNarration);
            this.layoutControl1.Controls.Add(this.glkpCashBank);
            this.layoutControl1.Controls.Add(this.gcCompLedgerAmount);
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.dteDate);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(699, 284, 250, 421);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cbDrPostComponent
            // 
            resources.ApplyResources(this.cbDrPostComponent, "cbDrPostComponent");
            this.cbDrPostComponent.Name = "cbDrPostComponent";
            this.cbDrPostComponent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cbDrPostComponent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cbDrPostComponent.Properties.Buttons"))))});
            this.cbDrPostComponent.Properties.Items.AddRange(new object[] {
            resources.GetString("cbDrPostComponent.Properties.Items"),
            resources.GetString("cbDrPostComponent.Properties.Items1")});
            this.cbDrPostComponent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbDrPostComponent.StyleController = this.layoutControl1;
            // 
            // lblTotalNetAmount
            // 
            this.lblTotalNetAmount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblTotalNetAmount.Appearance.Font")));
            this.lblTotalNetAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.lblTotalNetAmount, "lblTotalNetAmount");
            this.lblTotalNetAmount.Name = "lblTotalNetAmount";
            this.lblTotalNetAmount.StyleController = this.layoutControl1;
            // 
            // gcPayrollCompDetails
            // 
            resources.ApplyResources(this.gcPayrollCompDetails, "gcPayrollCompDetails");
            this.gcPayrollCompDetails.MainView = this.gvPayrollCompDetails;
            this.gcPayrollCompDetails.Name = "gcPayrollCompDetails";
            this.gcPayrollCompDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1});
            this.gcPayrollCompDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPayrollCompDetails});
            // 
            // gvPayrollCompDetails
            // 
            this.gvPayrollCompDetails.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvPayrollCompDetails.Appearance.FooterPanel.Font")));
            this.gvPayrollCompDetails.Appearance.FooterPanel.Options.UseFont = true;
            this.gvPayrollCompDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvPayrollCompDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPayDetailPayGroupId,
            this.colPayDetailComponentId,
            this.colPayDetailLedgerId,
            this.colPayDetailPayGroup,
            this.colPayDetailComponent,
            this.colPayDetailEarnAmount,
            this.colPayDetailDedctionAmount});
            this.gvPayrollCompDetails.GridControl = this.gcPayrollCompDetails;
            this.gvPayrollCompDetails.Name = "gvPayrollCompDetails";
            this.gvPayrollCompDetails.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvPayrollCompDetails.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.gvPayrollCompDetails.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvPayrollCompDetails.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvPayrollCompDetails.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvPayrollCompDetails.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvPayrollCompDetails.OptionsView.ShowFooter = true;
            this.gvPayrollCompDetails.OptionsView.ShowGroupPanel = false;
            // 
            // colPayDetailPayGroupId
            // 
            resources.ApplyResources(this.colPayDetailPayGroupId, "colPayDetailPayGroupId");
            this.colPayDetailPayGroupId.FieldName = "GROUP_ID";
            this.colPayDetailPayGroupId.Name = "colPayDetailPayGroupId";
            this.colPayDetailPayGroupId.OptionsColumn.AllowEdit = false;
            this.colPayDetailPayGroupId.OptionsColumn.AllowFocus = false;
            this.colPayDetailPayGroupId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailPayGroupId.OptionsColumn.AllowIncrementalSearch = false;
            this.colPayDetailPayGroupId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailPayGroupId.OptionsColumn.AllowMove = false;
            this.colPayDetailPayGroupId.OptionsColumn.AllowShowHide = false;
            this.colPayDetailPayGroupId.OptionsColumn.AllowSize = false;
            this.colPayDetailPayGroupId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailPayGroupId.OptionsColumn.FixedWidth = true;
            this.colPayDetailPayGroupId.OptionsFilter.AllowAutoFilter = false;
            this.colPayDetailPayGroupId.OptionsFilter.AllowFilter = false;
            // 
            // colPayDetailComponentId
            // 
            resources.ApplyResources(this.colPayDetailComponentId, "colPayDetailComponentId");
            this.colPayDetailComponentId.FieldName = "COMPONENT_ID";
            this.colPayDetailComponentId.Name = "colPayDetailComponentId";
            this.colPayDetailComponentId.OptionsColumn.AllowEdit = false;
            this.colPayDetailComponentId.OptionsColumn.AllowFocus = false;
            this.colPayDetailComponentId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailComponentId.OptionsColumn.AllowIncrementalSearch = false;
            this.colPayDetailComponentId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailComponentId.OptionsColumn.AllowMove = false;
            this.colPayDetailComponentId.OptionsColumn.AllowShowHide = false;
            this.colPayDetailComponentId.OptionsColumn.AllowSize = false;
            this.colPayDetailComponentId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colPayDetailComponentId.OptionsColumn.FixedWidth = true;
            this.colPayDetailComponentId.OptionsFilter.AllowAutoFilter = false;
            this.colPayDetailComponentId.OptionsFilter.AllowFilter = false;
            // 
            // colPayDetailLedgerId
            // 
            resources.ApplyResources(this.colPayDetailLedgerId, "colPayDetailLedgerId");
            this.colPayDetailLedgerId.FieldName = "LEDGER_ID";
            this.colPayDetailLedgerId.Name = "colPayDetailLedgerId";
            this.colPayDetailLedgerId.OptionsColumn.AllowEdit = false;
            this.colPayDetailLedgerId.OptionsColumn.AllowFocus = false;
            this.colPayDetailLedgerId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailLedgerId.OptionsColumn.AllowIncrementalSearch = false;
            this.colPayDetailLedgerId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailLedgerId.OptionsColumn.AllowMove = false;
            this.colPayDetailLedgerId.OptionsColumn.AllowShowHide = false;
            this.colPayDetailLedgerId.OptionsColumn.AllowSize = false;
            this.colPayDetailLedgerId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailLedgerId.OptionsColumn.FixedWidth = true;
            this.colPayDetailLedgerId.OptionsFilter.AllowAutoFilter = false;
            this.colPayDetailLedgerId.OptionsFilter.AllowFilter = false;
            // 
            // colPayDetailPayGroup
            // 
            this.colPayDetailPayGroup.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayDetailPayGroup.AppearanceHeader.Font")));
            this.colPayDetailPayGroup.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayDetailPayGroup, "colPayDetailPayGroup");
            this.colPayDetailPayGroup.FieldName = "GROUPNAME";
            this.colPayDetailPayGroup.Name = "colPayDetailPayGroup";
            this.colPayDetailPayGroup.OptionsColumn.AllowEdit = false;
            this.colPayDetailPayGroup.OptionsColumn.AllowFocus = false;
            this.colPayDetailPayGroup.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailPayGroup.OptionsColumn.AllowIncrementalSearch = false;
            this.colPayDetailPayGroup.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailPayGroup.OptionsColumn.AllowMove = false;
            this.colPayDetailPayGroup.OptionsColumn.AllowShowHide = false;
            this.colPayDetailPayGroup.OptionsColumn.AllowSize = false;
            this.colPayDetailPayGroup.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailPayGroup.OptionsColumn.FixedWidth = true;
            this.colPayDetailPayGroup.OptionsFilter.AllowAutoFilter = false;
            this.colPayDetailPayGroup.OptionsFilter.AllowFilter = false;
            // 
            // colPayDetailComponent
            // 
            this.colPayDetailComponent.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayDetailComponent.AppearanceHeader.Font")));
            this.colPayDetailComponent.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayDetailComponent, "colPayDetailComponent");
            this.colPayDetailComponent.FieldName = "COMPONENT";
            this.colPayDetailComponent.Name = "colPayDetailComponent";
            this.colPayDetailComponent.OptionsColumn.AllowEdit = false;
            this.colPayDetailComponent.OptionsColumn.AllowFocus = false;
            this.colPayDetailComponent.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailComponent.OptionsColumn.AllowIncrementalSearch = false;
            this.colPayDetailComponent.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailComponent.OptionsColumn.AllowMove = false;
            this.colPayDetailComponent.OptionsColumn.AllowShowHide = false;
            this.colPayDetailComponent.OptionsColumn.AllowSize = false;
            this.colPayDetailComponent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailComponent.OptionsColumn.FixedWidth = true;
            this.colPayDetailComponent.OptionsFilter.AllowAutoFilter = false;
            this.colPayDetailComponent.OptionsFilter.AllowFilter = false;
            // 
            // colPayDetailEarnAmount
            // 
            this.colPayDetailEarnAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayDetailEarnAmount.AppearanceHeader.Font")));
            this.colPayDetailEarnAmount.AppearanceHeader.Options.UseFont = true;
            this.colPayDetailEarnAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colPayDetailEarnAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colPayDetailEarnAmount, "colPayDetailEarnAmount");
            this.colPayDetailEarnAmount.DisplayFormat.FormatString = "n";
            this.colPayDetailEarnAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPayDetailEarnAmount.FieldName = "EARNING";
            this.colPayDetailEarnAmount.Name = "colPayDetailEarnAmount";
            this.colPayDetailEarnAmount.OptionsColumn.AllowEdit = false;
            this.colPayDetailEarnAmount.OptionsColumn.AllowFocus = false;
            this.colPayDetailEarnAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailEarnAmount.OptionsColumn.AllowIncrementalSearch = false;
            this.colPayDetailEarnAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailEarnAmount.OptionsColumn.AllowMove = false;
            this.colPayDetailEarnAmount.OptionsColumn.AllowShowHide = false;
            this.colPayDetailEarnAmount.OptionsColumn.AllowSize = false;
            this.colPayDetailEarnAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailEarnAmount.OptionsColumn.FixedWidth = true;
            this.colPayDetailEarnAmount.OptionsColumn.ReadOnly = true;
            this.colPayDetailEarnAmount.OptionsFilter.AllowAutoFilter = false;
            this.colPayDetailEarnAmount.OptionsFilter.AllowFilter = false;
            this.colPayDetailEarnAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colPayDetailEarnAmount.Summary"))), resources.GetString("colPayDetailEarnAmount.Summary1"), resources.GetString("colPayDetailEarnAmount.Summary2"))});
            // 
            // colPayDetailDedctionAmount
            // 
            this.colPayDetailDedctionAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayDetailDedctionAmount.AppearanceHeader.Font")));
            this.colPayDetailDedctionAmount.AppearanceHeader.Options.UseFont = true;
            this.colPayDetailDedctionAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colPayDetailDedctionAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colPayDetailDedctionAmount, "colPayDetailDedctionAmount");
            this.colPayDetailDedctionAmount.DisplayFormat.FormatString = "n";
            this.colPayDetailDedctionAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPayDetailDedctionAmount.FieldName = "DEDUCTION";
            this.colPayDetailDedctionAmount.Name = "colPayDetailDedctionAmount";
            this.colPayDetailDedctionAmount.OptionsColumn.AllowEdit = false;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowFocus = false;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowIncrementalSearch = false;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowMove = false;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowShowHide = false;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowSize = false;
            this.colPayDetailDedctionAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayDetailDedctionAmount.OptionsColumn.FixedWidth = true;
            this.colPayDetailDedctionAmount.OptionsFilter.AllowAutoFilter = false;
            this.colPayDetailDedctionAmount.OptionsFilter.AllowFilter = false;
            this.colPayDetailDedctionAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colPayDetailDedctionAmount.Summary"))), resources.GetString("colPayDetailDedctionAmount.Summary1"), resources.GetString("colPayDetailDedctionAmount.Summary2"))});
            // 
            // repositoryItemCalcEdit1
            // 
            resources.ApplyResources(this.repositoryItemCalcEdit1, "repositoryItemCalcEdit1");
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemCalcEdit1.Buttons"))))});
            this.repositoryItemCalcEdit1.DisplayFormat.FormatString = "C";
            this.repositoryItemCalcEdit1.Mask.EditMask = resources.GetString("repositoryItemCalcEdit1.Mask.EditMask");
            this.repositoryItemCalcEdit1.Mask.SaveLiteral = ((bool)(resources.GetObject("repositoryItemCalcEdit1.Mask.SaveLiteral")));
            this.repositoryItemCalcEdit1.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("repositoryItemCalcEdit1.Mask.ShowPlaceHolders")));
            this.repositoryItemCalcEdit1.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("repositoryItemCalcEdit1.Mask.UseMaskAsDisplayFormat")));
            this.repositoryItemCalcEdit1.MaxLength = 13;
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // chkCBPayGroup
            // 
            resources.ApplyResources(this.chkCBPayGroup, "chkCBPayGroup");
            this.chkCBPayGroup.Name = "chkCBPayGroup";
            this.chkCBPayGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.chkCBPayGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("chkCBPayGroup.Properties.Buttons"))))});
            this.chkCBPayGroup.StyleController = this.layoutControl1;
            this.chkCBPayGroup.EditValueChanged += new System.EventHandler(this.chkCBPayGroup_EditValueChanged);
            // 
            // txtChequeRefNumber
            // 
            resources.ApplyResources(this.txtChequeRefNumber, "txtChequeRefNumber");
            this.txtChequeRefNumber.Name = "txtChequeRefNumber";
            this.txtChequeRefNumber.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtChequeRefNumber.Properties.MaxLength = 25;
            this.txtChequeRefNumber.StyleController = this.layoutControl1;
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNarration.Properties.MaxLength = 300;
            this.txtNarration.StyleController = this.layoutControl1;
            // 
            // glkpCashBank
            // 
            this.glkpCashBank.EnterMoveNextControl = true;
            resources.ApplyResources(this.glkpCashBank, "glkpCashBank");
            this.glkpCashBank.Name = "glkpCashBank";
            this.glkpCashBank.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCashBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCashBank.Properties.Buttons"))))});
            this.glkpCashBank.Properties.ImmediatePopup = true;
            this.glkpCashBank.Properties.NullText = resources.GetString("glkpCashBank.Properties.NullText");
            this.glkpCashBank.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpCashBank.Properties.PopupFormMinSize = new System.Drawing.Size(382, 130);
            this.glkpCashBank.Properties.PopupFormSize = new System.Drawing.Size(382, 130);
            this.glkpCashBank.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpCashBank.Properties.View = this.gvCashBankLedgers;
            this.glkpCashBank.StyleController = this.layoutControl1;
            this.glkpCashBank.EditValueChanged += new System.EventHandler(this.glkpCashBank_EditValueChanged);
            // 
            // gvCashBankLedgers
            // 
            this.gvCashBankLedgers.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCashBankLedgers.Appearance.FocusedRow.Font")));
            this.gvCashBankLedgers.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCashBankLedgers.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCashBankLedgers.Appearance.FooterPanel.Font")));
            this.gvCashBankLedgers.Appearance.FooterPanel.Options.UseFont = true;
            this.gvCashBankLedgers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCashBankLedgerId,
            this.colCashBankLedgerName});
            this.gvCashBankLedgers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvCashBankLedgers.Name = "gvCashBankLedgers";
            this.gvCashBankLedgers.OptionsBehavior.Editable = false;
            this.gvCashBankLedgers.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCashBankLedgers.OptionsView.ShowColumnHeaders = false;
            this.gvCashBankLedgers.OptionsView.ShowGroupPanel = false;
            this.gvCashBankLedgers.OptionsView.ShowIndicator = false;
            // 
            // colCashBankLedgerId
            // 
            resources.ApplyResources(this.colCashBankLedgerId, "colCashBankLedgerId");
            this.colCashBankLedgerId.FieldName = "LEDGER_ID";
            this.colCashBankLedgerId.Name = "colCashBankLedgerId";
            // 
            // colCashBankLedgerName
            // 
            resources.ApplyResources(this.colCashBankLedgerName, "colCashBankLedgerName");
            this.colCashBankLedgerName.FieldName = "LEDGER_NAME";
            this.colCashBankLedgerName.Name = "colCashBankLedgerName";
            this.colCashBankLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcCompLedgerAmount
            // 
            resources.ApplyResources(this.gcCompLedgerAmount, "gcCompLedgerAmount");
            this.gcCompLedgerAmount.MainView = this.gvCompLedgerAmount;
            this.gcCompLedgerAmount.Name = "gcCompLedgerAmount";
            this.gcCompLedgerAmount.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpLedger,
            this.rglkpComponent,
            this.rtxtAmount});
            this.gcCompLedgerAmount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCompLedgerAmount});
            this.gcCompLedgerAmount.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcCompLedgerAmount_ProcessGridKey);
            // 
            // gvCompLedgerAmount
            // 
            this.gvCompLedgerAmount.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCompLedgerAmount.Appearance.FooterPanel.Font")));
            this.gvCompLedgerAmount.Appearance.FooterPanel.Options.UseFont = true;
            this.gvCompLedgerAmount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvCompLedgerAmount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPayrollid,
            this.colVoucherId,
            this.colVoucherNo,
            this.colTransMode,
            this.colComponent,
            this.colLedger,
            this.colTotalAmount,
            this.colAmount,
            this.colBalance});
            this.gvCompLedgerAmount.GridControl = this.gcCompLedgerAmount;
            this.gvCompLedgerAmount.Name = "gvCompLedgerAmount";
            this.gvCompLedgerAmount.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCompLedgerAmount.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.gvCompLedgerAmount.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvCompLedgerAmount.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvCompLedgerAmount.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvCompLedgerAmount.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvCompLedgerAmount.OptionsView.ShowFooter = true;
            this.gvCompLedgerAmount.OptionsView.ShowGroupPanel = false;
            this.gvCompLedgerAmount.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.gvCompLedgerAmount_CustomSummaryCalculate);
            // 
            // colPayrollid
            // 
            this.colPayrollid.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayrollid.AppearanceHeader.Font")));
            this.colPayrollid.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayrollid, "colPayrollid");
            this.colPayrollid.FieldName = "PAYROLLID";
            this.colPayrollid.Name = "colPayrollid";
            this.colPayrollid.OptionsColumn.AllowEdit = false;
            // 
            // colVoucherId
            // 
            this.colVoucherId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherId.AppearanceHeader.Font")));
            this.colVoucherId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            this.colVoucherId.OptionsColumn.AllowEdit = false;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colVoucherNo.AppearanceHeader.Font")));
            this.colVoucherNo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colVoucherNo, "colVoucherNo");
            this.colVoucherNo.FieldName = "VOUCHER_NO";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.OptionsColumn.AllowEdit = false;
            this.colVoucherNo.OptionsColumn.AllowFocus = false;
            this.colVoucherNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherNo.OptionsColumn.AllowMove = false;
            this.colVoucherNo.OptionsColumn.AllowSize = false;
            this.colVoucherNo.OptionsColumn.ReadOnly = true;
            // 
            // colTransMode
            // 
            this.colTransMode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTransMode.AppearanceHeader.Font")));
            this.colTransMode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTransMode, "colTransMode");
            this.colTransMode.FieldName = "TRANS_MODE";
            this.colTransMode.Name = "colTransMode";
            this.colTransMode.OptionsColumn.AllowEdit = false;
            this.colTransMode.OptionsColumn.AllowFocus = false;
            this.colTransMode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTransMode.OptionsColumn.AllowIncrementalSearch = false;
            this.colTransMode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTransMode.OptionsColumn.AllowMove = false;
            this.colTransMode.OptionsColumn.AllowShowHide = false;
            this.colTransMode.OptionsColumn.AllowSize = false;
            this.colTransMode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTransMode.OptionsColumn.FixedWidth = true;
            this.colTransMode.OptionsColumn.ReadOnly = true;
            this.colTransMode.OptionsFilter.AllowAutoFilter = false;
            this.colTransMode.OptionsFilter.AllowFilter = false;
            // 
            // colComponent
            // 
            this.colComponent.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colComponent.AppearanceHeader.Font")));
            this.colComponent.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colComponent, "colComponent");
            this.colComponent.FieldName = "COMPONENT";
            this.colComponent.Name = "colComponent";
            this.colComponent.OptionsColumn.AllowEdit = false;
            this.colComponent.OptionsColumn.AllowFocus = false;
            this.colComponent.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colComponent.OptionsColumn.AllowIncrementalSearch = false;
            this.colComponent.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colComponent.OptionsColumn.AllowMove = false;
            this.colComponent.OptionsColumn.AllowShowHide = false;
            this.colComponent.OptionsColumn.AllowSize = false;
            this.colComponent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colComponent.OptionsColumn.FixedWidth = true;
            this.colComponent.OptionsFilter.AllowAutoFilter = false;
            this.colComponent.OptionsFilter.AllowFilter = false;
            // 
            // colLedger
            // 
            this.colLedger.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedger.AppearanceHeader.Font")));
            this.colLedger.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedger, "colLedger");
            this.colLedger.ColumnEdit = this.rglkpLedger;
            this.colLedger.FieldName = "LEDGER_ID";
            this.colLedger.Name = "colLedger";
            this.colLedger.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colLedger.OptionsColumn.AllowIncrementalSearch = false;
            this.colLedger.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colLedger.OptionsColumn.AllowMove = false;
            this.colLedger.OptionsColumn.AllowShowHide = false;
            this.colLedger.OptionsColumn.AllowSize = false;
            this.colLedger.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLedger.OptionsColumn.FixedWidth = true;
            this.colLedger.OptionsFilter.AllowAutoFilter = false;
            this.colLedger.OptionsFilter.AllowFilter = false;
            // 
            // rglkpLedger
            // 
            resources.ApplyResources(this.rglkpLedger, "rglkpLedger");
            this.rglkpLedger.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.rglkpLedger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpLedger.Buttons"))))});
            this.rglkpLedger.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2);
            this.rglkpLedger.ImmediatePopup = true;
            this.rglkpLedger.Name = "rglkpLedger";
            this.rglkpLedger.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D;
            this.rglkpLedger.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpLedger.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpLedger.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colLedgerName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsBehavior.AllowIncrementalSearch = true;
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            this.colLedgerId.OptionsColumn.AllowEdit = false;
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsColumn.AllowEdit = false;
            this.colLedgerName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerName.OptionsColumn.AllowMove = false;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTotalAmount.AppearanceHeader.Font")));
            this.colTotalAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTotalAmount, "colTotalAmount");
            this.colTotalAmount.DisplayFormat.FormatString = "n";
            this.colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalAmount.FieldName = "TOTAL_AMOUNT";
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.OptionsColumn.AllowEdit = false;
            this.colTotalAmount.OptionsColumn.AllowFocus = false;
            this.colTotalAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalAmount.OptionsColumn.AllowIncrementalSearch = false;
            this.colTotalAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalAmount.OptionsColumn.AllowMove = false;
            this.colTotalAmount.OptionsColumn.AllowShowHide = false;
            this.colTotalAmount.OptionsColumn.AllowSize = false;
            this.colTotalAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalAmount.OptionsColumn.FixedWidth = true;
            this.colTotalAmount.OptionsColumn.ReadOnly = true;
            this.colTotalAmount.OptionsFilter.AllowAutoFilter = false;
            this.colTotalAmount.OptionsFilter.AllowFilter = false;
            this.colTotalAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colTotalAmount.Summary"))))});
            // 
            // colAmount
            // 
            this.colAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAmount.AppearanceHeader.Font")));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.ColumnEdit = this.rtxtAmount;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.AllowFocus = false;
            this.colAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAmount.OptionsColumn.AllowIncrementalSearch = false;
            this.colAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAmount.OptionsColumn.AllowMove = false;
            this.colAmount.OptionsColumn.AllowShowHide = false;
            this.colAmount.OptionsColumn.AllowSize = false;
            this.colAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.OptionsColumn.ReadOnly = true;
            this.colAmount.OptionsFilter.AllowAutoFilter = false;
            this.colAmount.OptionsFilter.AllowFilter = false;
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colAmount.Summary"))))});
            // 
            // rtxtAmount
            // 
            resources.ApplyResources(this.rtxtAmount, "rtxtAmount");
            this.rtxtAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rtxtAmount.Buttons"))))});
            this.rtxtAmount.DisplayFormat.FormatString = "C";
            this.rtxtAmount.Mask.EditMask = resources.GetString("rtxtAmount.Mask.EditMask");
            this.rtxtAmount.Mask.SaveLiteral = ((bool)(resources.GetObject("rtxtAmount.Mask.SaveLiteral")));
            this.rtxtAmount.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("rtxtAmount.Mask.ShowPlaceHolders")));
            this.rtxtAmount.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rtxtAmount.Mask.UseMaskAsDisplayFormat")));
            this.rtxtAmount.MaxLength = 13;
            this.rtxtAmount.Name = "rtxtAmount";
            // 
            // colBalance
            // 
            this.colBalance.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBalance.AppearanceHeader.Font")));
            this.colBalance.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBalance, "colBalance");
            this.colBalance.DisplayFormat.FormatString = "n";
            this.colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalance.FieldName = "BALANCE";
            this.colBalance.Name = "colBalance";
            this.colBalance.OptionsColumn.AllowEdit = false;
            this.colBalance.OptionsColumn.AllowFocus = false;
            this.colBalance.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBalance.OptionsColumn.AllowIncrementalSearch = false;
            this.colBalance.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBalance.OptionsColumn.AllowMove = false;
            this.colBalance.OptionsColumn.AllowShowHide = false;
            this.colBalance.OptionsColumn.AllowSize = false;
            this.colBalance.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBalance.OptionsColumn.FixedWidth = true;
            this.colBalance.OptionsFilter.AllowAutoFilter = false;
            this.colBalance.OptionsFilter.AllowFilter = false;
            this.colBalance.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colBalance.Summary"))))});
            // 
            // rglkpComponent
            // 
            resources.ApplyResources(this.rglkpComponent, "rglkpComponent");
            this.rglkpComponent.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpComponent.Buttons"))))});
            this.rglkpComponent.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2);
            this.rglkpComponent.ImmediatePopup = true;
            this.rglkpComponent.Name = "rglkpComponent";
            this.rglkpComponent.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D;
            this.rglkpComponent.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpComponent.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpComponent.View = this.gridView2;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormMinSize = new System.Drawing.Size(356, 130);
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(356, 130);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridView1;
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            this.glkpProject.Leave += new System.EventHandler(this.glkpProject_Leave);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.HeaderPanel.Font")));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProject});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            this.colProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dteDate
            // 
            resources.ApplyResources(this.dteDate, "dteDate");
            this.dteDate.EnterMoveNextControl = true;
            this.dteDate.Name = "dteDate";
            this.dteDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dteDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteDate.Properties.Buttons"))))});
            this.dteDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDate.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("dteDate.Properties.CalendarTimeProperties.Mask.EditMask");
            this.dteDate.Properties.CalendarTimeProperties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dteDate.Properties.CalendarTimeProperties.Mask.MaskType")));
            this.dteDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dteDate.Properties.Mask.MaskType")));
            this.dteDate.StyleController = this.layoutControl1;
            this.dteDate.TabStop = false;
            this.dteDate.EditValueChanged += new System.EventHandler(this.dteDate_EditValueChanged);
            this.dteDate.Leave += new System.EventHandler(this.dteDate_Leave);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem4,
            this.lcSave,
            this.lcClose,
            this.lcGrpPayrollComponentDetails,
            this.lcGrpPayrollVoucher});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(667, 508);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 472);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(447, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcSave
            // 
            this.lcSave.Control = this.btnSave;
            resources.ApplyResources(this.lcSave, "lcSave");
            this.lcSave.Location = new System.Drawing.Point(447, 472);
            this.lcSave.MaxSize = new System.Drawing.Size(105, 26);
            this.lcSave.MinSize = new System.Drawing.Size(105, 26);
            this.lcSave.Name = "lcSave";
            this.lcSave.Size = new System.Drawing.Size(105, 26);
            this.lcSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcSave.TextSize = new System.Drawing.Size(0, 0);
            this.lcSave.TextToControlDistance = 0;
            this.lcSave.TextVisible = false;
            // 
            // lcClose
            // 
            this.lcClose.Control = this.btnClose;
            resources.ApplyResources(this.lcClose, "lcClose");
            this.lcClose.Location = new System.Drawing.Point(552, 472);
            this.lcClose.MaxSize = new System.Drawing.Size(105, 26);
            this.lcClose.MinSize = new System.Drawing.Size(105, 26);
            this.lcClose.Name = "lcClose";
            this.lcClose.Size = new System.Drawing.Size(105, 26);
            this.lcClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcClose.TextSize = new System.Drawing.Size(0, 0);
            this.lcClose.TextToControlDistance = 0;
            this.lcClose.TextVisible = false;
            // 
            // lcGrpPayrollComponentDetails
            // 
            this.lcGrpPayrollComponentDetails.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpPayrollComponentDetails.AppearanceGroup.Font")));
            this.lcGrpPayrollComponentDetails.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpPayrollComponentDetails, "lcGrpPayrollComponentDetails");
            this.lcGrpPayrollComponentDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPayrollGroup,
            this.lcPayrollDetails,
            this.lcDrPostComponent,
            this.emptySpaceItem2,
            this.lcApply});
            this.lcGrpPayrollComponentDetails.Location = new System.Drawing.Point(0, 0);
            this.lcGrpPayrollComponentDetails.Name = "lcGrpPayrollComponentDetails";
            this.lcGrpPayrollComponentDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcGrpPayrollComponentDetails.Size = new System.Drawing.Size(657, 199);
            // 
            // lcPayrollGroup
            // 
            this.lcPayrollGroup.AllowHtmlStringInCaption = true;
            this.lcPayrollGroup.Control = this.chkCBPayGroup;
            this.lcPayrollGroup.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcPayrollGroup, "lcPayrollGroup");
            this.lcPayrollGroup.Location = new System.Drawing.Point(0, 0);
            this.lcPayrollGroup.MaxSize = new System.Drawing.Size(641, 24);
            this.lcPayrollGroup.MinSize = new System.Drawing.Size(641, 24);
            this.lcPayrollGroup.Name = "lcPayrollGroup";
            this.lcPayrollGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcPayrollGroup.Size = new System.Drawing.Size(641, 24);
            this.lcPayrollGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcPayrollGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcPayrollGroup.TextSize = new System.Drawing.Size(86, 13);
            this.lcPayrollGroup.TextToControlDistance = 5;
            this.lcPayrollGroup.TrimClientAreaToControl = false;
            // 
            // lcPayrollDetails
            // 
            this.lcPayrollDetails.Control = this.gcPayrollCompDetails;
            resources.ApplyResources(this.lcPayrollDetails, "lcPayrollDetails");
            this.lcPayrollDetails.Location = new System.Drawing.Point(0, 50);
            this.lcPayrollDetails.Name = "lcPayrollDetails";
            this.lcPayrollDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcPayrollDetails.Size = new System.Drawing.Size(641, 114);
            this.lcPayrollDetails.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcPayrollDetails.TextSize = new System.Drawing.Size(86, 13);
            this.lcPayrollDetails.TextToControlDistance = 5;
            // 
            // lcDrPostComponent
            // 
            this.lcDrPostComponent.AllowHtmlStringInCaption = true;
            this.lcDrPostComponent.Control = this.cbDrPostComponent;
            this.lcDrPostComponent.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcDrPostComponent, "lcDrPostComponent");
            this.lcDrPostComponent.Location = new System.Drawing.Point(0, 24);
            this.lcDrPostComponent.MaxSize = new System.Drawing.Size(439, 24);
            this.lcDrPostComponent.MinSize = new System.Drawing.Size(439, 24);
            this.lcDrPostComponent.Name = "lcDrPostComponent";
            this.lcDrPostComponent.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcDrPostComponent.Size = new System.Drawing.Size(439, 26);
            this.lcDrPostComponent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcDrPostComponent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcDrPostComponent.TextSize = new System.Drawing.Size(86, 13);
            this.lcDrPostComponent.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(515, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(126, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcApply
            // 
            this.lcApply.Control = this.btnApply;
            this.lcApply.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcApply, "lcApply");
            this.lcApply.Location = new System.Drawing.Point(439, 24);
            this.lcApply.MaxSize = new System.Drawing.Size(76, 26);
            this.lcApply.MinSize = new System.Drawing.Size(76, 26);
            this.lcApply.Name = "lcApply";
            this.lcApply.Size = new System.Drawing.Size(76, 26);
            this.lcApply.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcApply.TextSize = new System.Drawing.Size(0, 0);
            this.lcApply.TextToControlDistance = 0;
            this.lcApply.TextVisible = false;
            this.lcApply.TrimClientAreaToControl = false;
            // 
            // lcGrpPayrollVoucher
            // 
            this.lcGrpPayrollVoucher.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcGrpPayrollVoucher.AppearanceGroup.Font")));
            this.lcGrpPayrollVoucher.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcGrpPayrollVoucher, "lcGrpPayrollVoucher");
            this.lcGrpPayrollVoucher.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcCompLedgerAmount,
            this.lcNarration,
            this.lcChequeDDRefNumber,
            this.lcCashBankLedger,
            this.lcLblCashBankBalance,
            this.emptySpaceItem3,
            this.lcDate,
            this.lcProject,
            this.lcTotalNETAmount,
            this.emptySpaceItem1});
            this.lcGrpPayrollVoucher.Location = new System.Drawing.Point(0, 199);
            this.lcGrpPayrollVoucher.Name = "lcGrpPayrollVoucher";
            this.lcGrpPayrollVoucher.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcGrpPayrollVoucher.Size = new System.Drawing.Size(657, 273);
            // 
            // lcCompLedgerAmount
            // 
            this.lcCompLedgerAmount.AllowHtmlStringInCaption = true;
            this.lcCompLedgerAmount.Control = this.gcCompLedgerAmount;
            resources.ApplyResources(this.lcCompLedgerAmount, "lcCompLedgerAmount");
            this.lcCompLedgerAmount.Location = new System.Drawing.Point(0, 48);
            this.lcCompLedgerAmount.Name = "lcCompLedgerAmount";
            this.lcCompLedgerAmount.Size = new System.Drawing.Size(641, 118);
            this.lcCompLedgerAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcCompLedgerAmount.TextSize = new System.Drawing.Size(0, 0);
            this.lcCompLedgerAmount.TextToControlDistance = 0;
            this.lcCompLedgerAmount.TextVisible = false;
            // 
            // lcNarration
            // 
            this.lcNarration.Control = this.txtNarration;
            this.lcNarration.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcNarration, "lcNarration");
            this.lcNarration.Location = new System.Drawing.Point(0, 214);
            this.lcNarration.MaxSize = new System.Drawing.Size(633, 24);
            this.lcNarration.MinSize = new System.Drawing.Size(633, 24);
            this.lcNarration.Name = "lcNarration";
            this.lcNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcNarration.Size = new System.Drawing.Size(641, 24);
            this.lcNarration.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcNarration.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcNarration.TextSize = new System.Drawing.Size(80, 20);
            this.lcNarration.TextToControlDistance = 5;
            this.lcNarration.TrimClientAreaToControl = false;
            // 
            // lcChequeDDRefNumber
            // 
            this.lcChequeDDRefNumber.Control = this.txtChequeRefNumber;
            this.lcChequeDDRefNumber.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcChequeDDRefNumber, "lcChequeDDRefNumber");
            this.lcChequeDDRefNumber.Location = new System.Drawing.Point(0, 190);
            this.lcChequeDDRefNumber.MaxSize = new System.Drawing.Size(473, 24);
            this.lcChequeDDRefNumber.MinSize = new System.Drawing.Size(473, 24);
            this.lcChequeDDRefNumber.Name = "lcChequeDDRefNumber";
            this.lcChequeDDRefNumber.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcChequeDDRefNumber.Size = new System.Drawing.Size(473, 24);
            this.lcChequeDDRefNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcChequeDDRefNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcChequeDDRefNumber.TextSize = new System.Drawing.Size(80, 20);
            this.lcChequeDDRefNumber.TextToControlDistance = 5;
            this.lcChequeDDRefNumber.TrimClientAreaToControl = false;
            // 
            // lcCashBankLedger
            // 
            this.lcCashBankLedger.AllowHtmlStringInCaption = true;
            this.lcCashBankLedger.Control = this.glkpCashBank;
            this.lcCashBankLedger.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcCashBankLedger, "lcCashBankLedger");
            this.lcCashBankLedger.Location = new System.Drawing.Point(0, 166);
            this.lcCashBankLedger.MaxSize = new System.Drawing.Size(474, 24);
            this.lcCashBankLedger.MinSize = new System.Drawing.Size(474, 24);
            this.lcCashBankLedger.Name = "lcCashBankLedger";
            this.lcCashBankLedger.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcCashBankLedger.Size = new System.Drawing.Size(474, 24);
            this.lcCashBankLedger.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcCashBankLedger.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcCashBankLedger.TextSize = new System.Drawing.Size(80, 20);
            this.lcCashBankLedger.TextToControlDistance = 5;
            this.lcCashBankLedger.TrimClientAreaToControl = false;
            // 
            // lcLblCashBankBalance
            // 
            this.lcLblCashBankBalance.AllowHotTrack = false;
            this.lcLblCashBankBalance.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcLblCashBankBalance.AppearanceItemCaption.Font")));
            this.lcLblCashBankBalance.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lcLblCashBankBalance, "lcLblCashBankBalance");
            this.lcLblCashBankBalance.Location = new System.Drawing.Point(474, 166);
            this.lcLblCashBankBalance.Name = "lcLblCashBankBalance";
            this.lcLblCashBankBalance.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcLblCashBankBalance.Size = new System.Drawing.Size(167, 24);
            this.lcLblCashBankBalance.TextSize = new System.Drawing.Size(148, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(473, 190);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(168, 24);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcDate
            // 
            this.lcDate.AllowHtmlStringInCaption = true;
            this.lcDate.Control = this.dteDate;
            this.lcDate.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcDate, "lcDate");
            this.lcDate.Location = new System.Drawing.Point(0, 0);
            this.lcDate.MaxSize = new System.Drawing.Size(196, 24);
            this.lcDate.MinSize = new System.Drawing.Size(196, 24);
            this.lcDate.Name = "lcDate";
            this.lcDate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcDate.Size = new System.Drawing.Size(196, 24);
            this.lcDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcDate.TextSize = new System.Drawing.Size(86, 13);
            this.lcDate.TextToControlDistance = 5;
            this.lcDate.TrimClientAreaToControl = false;
            // 
            // lcProject
            // 
            this.lcProject.AllowHtmlStringInCaption = true;
            this.lcProject.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcProject.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lcProject.Control = this.glkpProject;
            this.lcProject.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcProject, "lcProject");
            this.lcProject.Location = new System.Drawing.Point(0, 24);
            this.lcProject.MaxSize = new System.Drawing.Size(641, 24);
            this.lcProject.MinSize = new System.Drawing.Size(641, 24);
            this.lcProject.Name = "lcProject";
            this.lcProject.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcProject.Size = new System.Drawing.Size(641, 24);
            this.lcProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcProject.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcProject.TextSize = new System.Drawing.Size(86, 13);
            this.lcProject.TextToControlDistance = 5;
            this.lcProject.TrimClientAreaToControl = false;
            // 
            // lcTotalNETAmount
            // 
            this.lcTotalNETAmount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcTotalNETAmount.AppearanceItemCaption.Font")));
            this.lcTotalNETAmount.AppearanceItemCaption.Options.UseFont = true;
            this.lcTotalNETAmount.Control = this.lblTotalNetAmount;
            this.lcTotalNETAmount.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight;
            resources.ApplyResources(this.lcTotalNETAmount, "lcTotalNETAmount");
            this.lcTotalNETAmount.Location = new System.Drawing.Point(452, 0);
            this.lcTotalNETAmount.Name = "lcTotalNETAmount";
            this.lcTotalNETAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
            this.lcTotalNETAmount.Size = new System.Drawing.Size(189, 24);
            this.lcTotalNETAmount.TextSize = new System.Drawing.Size(148, 16);
            this.lcTotalNETAmount.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(196, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(256, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmPostPaymentVoucher
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPostPaymentVoucher";
            this.Load += new System.EventHandler(this.frmPostPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbDrPostComponent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayrollCompDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollCompDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCBPayGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChequeRefNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCashBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCashBankLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCompLedgerAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompLedgerAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpPayrollComponentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPayrollGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPayrollDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDrPostComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGrpPayrollVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCompLedgerAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChequeDDRefNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCashBankLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLblCashBankBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTotalNETAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit dteDate;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlItem lcDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem lcSave;
        private DevExpress.XtraLayout.LayoutControlItem lcClose;
        private DevExpress.XtraLayout.LayoutControlItem lcProject;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpPayrollComponentDetails;
        private DevExpress.XtraGrid.GridControl gcCompLedgerAmount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCompLedgerAmount;
        private DevExpress.XtraLayout.LayoutControlItem lcCompLedgerAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollid;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colTransMode;
        private DevExpress.XtraGrid.Columns.GridColumn colComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCashBank;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCashBankLedgers;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBankLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBankLedgerName;
        private DevExpress.XtraLayout.LayoutControlItem lcCashBankLedger;
        private DevExpress.XtraEditors.TextEdit txtNarration;
        private DevExpress.XtraLayout.LayoutControlItem lcNarration;
        private DevExpress.XtraLayout.SimpleLabelItem lcLblCashBankBalance;
        private DevExpress.XtraEditors.TextEdit txtChequeRefNumber;
        private DevExpress.XtraLayout.LayoutControlItem lcChequeDDRefNumber;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpComponent;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit rtxtAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkCBPayGroup;
        private DevExpress.XtraLayout.LayoutControlItem lcPayrollGroup;
        private DevExpress.XtraGrid.GridControl gcPayrollCompDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPayrollCompDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colPayDetailComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colPayDetailEarnAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraLayout.LayoutControlItem lcPayrollDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colPayDetailPayGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colPayDetailDedctionAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPayDetailPayGroupId;
        private DevExpress.XtraGrid.Columns.GridColumn colPayDetailComponentId;
        private DevExpress.XtraGrid.Columns.GridColumn colPayDetailLedgerId;
        private DevExpress.XtraLayout.LayoutControlGroup lcGrpPayrollVoucher;
        private DevExpress.XtraEditors.LabelControl lblTotalNetAmount;
        private DevExpress.XtraLayout.LayoutControlItem lcTotalNETAmount;
        private DevExpress.XtraEditors.ComboBoxEdit cbDrPostComponent;
        private DevExpress.XtraLayout.LayoutControlItem lcDrPostComponent;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem lcApply;

    }
}