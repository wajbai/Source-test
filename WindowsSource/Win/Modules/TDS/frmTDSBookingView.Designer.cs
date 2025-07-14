namespace ACPP.Modules.TDS
{
    partial class frmTDSBookingView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTDSBookingView));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gvTDSBooking = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNatureofPaymentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNatureOfPaymentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssesValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeducteeType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTAXLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChildBookingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChildVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBookingView = new DevExpress.XtraGrid.GridControl();
            this.gvBookingView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBookingID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpenseLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartyLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeductTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpenseLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartyLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeducteeTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucToolBarBooking = new Bosco.Utility.Controls.ucToolBar();
            this.pnlToolBar = new DevExpress.XtraEditors.PanelControl();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRowCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pncFilter = new DevExpress.XtraEditors.PanelControl();
            this.lcFilter = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.deDateTo = new DevExpress.XtraEditors.DateEdit();
            this.deDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgFilter = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBookingView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBookingView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).BeginInit();
            this.pnlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pncFilter)).BeginInit();
            this.pncFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcFilter)).BeginInit();
            this.lcFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvTDSBooking
            // 
            this.gvTDSBooking.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSBooking.Appearance.FocusedRow.Font")));
            this.gvTDSBooking.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTDSBooking.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSBooking.Appearance.HeaderPanel.Font")));
            this.gvTDSBooking.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTDSBooking.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSBooking.AppearancePrint.HeaderPanel.Font")));
            this.gvTDSBooking.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvTDSBooking.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNatureofPaymentId,
            this.colNatureOfPaymentName,
            this.colAssesValue,
            this.colDeducteeType,
            this.colTDSLedger,
            this.colTDSAmount,
            this.colTAXLedgerId,
            this.colDeductId,
            this.colChildBookingId,
            this.colChildVoucherId});
            this.gvTDSBooking.GridControl = this.gcBookingView;
            this.gvTDSBooking.Name = "gvTDSBooking";
            this.gvTDSBooking.OptionsBehavior.Editable = false;
            this.gvTDSBooking.OptionsPrint.PrintDetails = true;
            this.gvTDSBooking.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTDSBooking.OptionsView.ShowGroupPanel = false;
            this.gvTDSBooking.DoubleClick += new System.EventHandler(this.gvTDSBooking_DoubleClick);
            // 
            // colNatureofPaymentId
            // 
            resources.ApplyResources(this.colNatureofPaymentId, "colNatureofPaymentId");
            this.colNatureofPaymentId.FieldName = "NATURE_OF_PAYMENT_ID";
            this.colNatureofPaymentId.Name = "colNatureofPaymentId";
            // 
            // colNatureOfPaymentName
            // 
            resources.ApplyResources(this.colNatureOfPaymentName, "colNatureOfPaymentName");
            this.colNatureOfPaymentName.FieldName = "NAME";
            this.colNatureOfPaymentName.Name = "colNatureOfPaymentName";
            // 
            // colAssesValue
            // 
            resources.ApplyResources(this.colAssesValue, "colAssesValue");
            this.colAssesValue.DisplayFormat.FormatString = "N";
            this.colAssesValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAssesValue.FieldName = "ASSESS_AMOUNT";
            this.colAssesValue.GroupFormat.FormatString = "N";
            this.colAssesValue.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAssesValue.Name = "colAssesValue";
            // 
            // colDeducteeType
            // 
            resources.ApplyResources(this.colDeducteeType, "colDeducteeType");
            this.colDeducteeType.FieldName = "DEDUCTEE_TYPE";
            this.colDeducteeType.Name = "colDeducteeType";
            // 
            // colTDSLedger
            // 
            resources.ApplyResources(this.colTDSLedger, "colTDSLedger");
            this.colTDSLedger.FieldName = "LEDGER_NAME";
            this.colTDSLedger.Name = "colTDSLedger";
            // 
            // colTDSAmount
            // 
            resources.ApplyResources(this.colTDSAmount, "colTDSAmount");
            this.colTDSAmount.DisplayFormat.FormatString = "N";
            this.colTDSAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTDSAmount.FieldName = "TAX_AMOUNT";
            this.colTDSAmount.GroupFormat.FormatString = "N";
            this.colTDSAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTDSAmount.Name = "colTDSAmount";
            // 
            // colTAXLedgerId
            // 
            resources.ApplyResources(this.colTAXLedgerId, "colTAXLedgerId");
            this.colTAXLedgerId.FieldName = "TAX_LEDGER_ID";
            this.colTAXLedgerId.Name = "colTAXLedgerId";
            // 
            // colDeductId
            // 
            resources.ApplyResources(this.colDeductId, "colDeductId");
            this.colDeductId.FieldName = "DEDUCTEE_TYPE_ID";
            this.colDeductId.Name = "colDeductId";
            // 
            // colChildBookingId
            // 
            resources.ApplyResources(this.colChildBookingId, "colChildBookingId");
            this.colChildBookingId.FieldName = "BOOKING_ID";
            this.colChildBookingId.Name = "colChildBookingId";
            // 
            // colChildVoucherId
            // 
            resources.ApplyResources(this.colChildVoucherId, "colChildVoucherId");
            this.colChildVoucherId.FieldName = "VOUCHER_ID";
            this.colChildVoucherId.Name = "colChildVoucherId";
            // 
            // gcBookingView
            // 
            gridLevelNode1.LevelTemplate = this.gvTDSBooking;
            gridLevelNode1.RelationName = "TDS Deduction";
            this.gcBookingView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            resources.ApplyResources(this.gcBookingView, "gcBookingView");
            this.gcBookingView.MainView = this.gvBookingView;
            this.gcBookingView.Name = "gcBookingView";
            this.gcBookingView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBookingView,
            this.gvTDSBooking});
            // 
            // gvBookingView
            // 
            this.gvBookingView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvBookingView.Appearance.FocusedRow.Font")));
            this.gvBookingView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBookingView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBookingView.Appearance.HeaderPanel.Font")));
            this.gvBookingView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBookingView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBookingView.AppearancePrint.HeaderPanel.Font")));
            this.gvBookingView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvBookingView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvBookingView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBookingID,
            this.colVoucherId,
            this.colVoucherDate,
            this.colVoucherNo,
            this.colExpenseLedger,
            this.colPartyLedger,
            this.colProject,
            this.colAmount,
            this.colProjectId,
            this.colDeductTypeId,
            this.colExpenseLedgerId,
            this.colPartyLedgerId,
            this.colDeducteeTypeId});
            this.gvBookingView.GridControl = this.gcBookingView;
            this.gvBookingView.Name = "gvBookingView";
            this.gvBookingView.OptionsBehavior.Editable = false;
            this.gvBookingView.OptionsPrint.PrintDetails = true;
            this.gvBookingView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBookingView.OptionsView.ShowGroupPanel = false;
            this.gvBookingView.DoubleClick += new System.EventHandler(this.gvBookingView_DoubleClick);
            this.gvBookingView.RowCountChanged += new System.EventHandler(this.gvBookingView_RowCountChanged);
            // 
            // colBookingID
            // 
            resources.ApplyResources(this.colBookingID, "colBookingID");
            this.colBookingID.FieldName = "BOOKING_ID";
            this.colBookingID.Name = "colBookingID";
            // 
            // colVoucherId
            // 
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colVoucherDate
            // 
            resources.ApplyResources(this.colVoucherDate, "colVoucherDate");
            this.colVoucherDate.DisplayFormat.FormatString = "d";
            this.colVoucherDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colVoucherDate.FieldName = "BOOKING_DATE";
            this.colVoucherDate.Name = "colVoucherDate";
            // 
            // colVoucherNo
            // 
            resources.ApplyResources(this.colVoucherNo, "colVoucherNo");
            this.colVoucherNo.FieldName = "VOUCHER_NO";
            this.colVoucherNo.Name = "colVoucherNo";
            // 
            // colExpenseLedger
            // 
            resources.ApplyResources(this.colExpenseLedger, "colExpenseLedger");
            this.colExpenseLedger.FieldName = "EXPENSE_LEDGER";
            this.colExpenseLedger.Name = "colExpenseLedger";
            // 
            // colPartyLedger
            // 
            resources.ApplyResources(this.colPartyLedger, "colPartyLedger");
            this.colPartyLedger.FieldName = "PARTY_LEDGER";
            this.colPartyLedger.Name = "colPartyLedger";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.GroupFormat.FormatString = "n";
            this.colAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.Name = "colAmount";
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colDeductTypeId
            // 
            resources.ApplyResources(this.colDeductTypeId, "colDeductTypeId");
            this.colDeductTypeId.Name = "colDeductTypeId";
            // 
            // colExpenseLedgerId
            // 
            resources.ApplyResources(this.colExpenseLedgerId, "colExpenseLedgerId");
            this.colExpenseLedgerId.FieldName = "EXPENSE_LEDGER_ID";
            this.colExpenseLedgerId.Name = "colExpenseLedgerId";
            // 
            // colPartyLedgerId
            // 
            resources.ApplyResources(this.colPartyLedgerId, "colPartyLedgerId");
            this.colPartyLedgerId.FieldName = "PARTY_LEDGER_ID";
            this.colPartyLedgerId.Name = "colPartyLedgerId";
            // 
            // colDeducteeTypeId
            // 
            resources.ApplyResources(this.colDeducteeTypeId, "colDeducteeTypeId");
            this.colDeducteeTypeId.FieldName = "DEDUCTEE_TYPE_ID";
            this.colDeducteeTypeId.Name = "colDeducteeTypeId";
            // 
            // ucToolBarBooking
            // 
            this.ucToolBarBooking.ChangeAddCaption = "&Add";
            this.ucToolBarBooking.ChangeCaption = "&Edit";
            this.ucToolBarBooking.ChangeDeleteCaption = "&Delete";
            this.ucToolBarBooking.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarBooking.ChangePrintCaption = "&Print";
            this.ucToolBarBooking.DisableAddButton = true;
            this.ucToolBarBooking.DisableCloseButton = true;
            this.ucToolBarBooking.DisableDeleteButton = true;
            this.ucToolBarBooking.DisableDownloadExcel = true;
            this.ucToolBarBooking.DisableEditButton = true;
            this.ucToolBarBooking.DisableMoveTransaction = true;
            this.ucToolBarBooking.DisableNatureofPayments = true;
            this.ucToolBarBooking.DisablePrintButton = true;
            this.ucToolBarBooking.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarBooking, "ucToolBarBooking");
            this.ucToolBarBooking.Name = "ucToolBarBooking";
            this.ucToolBarBooking.ShowHTML = true;
            this.ucToolBarBooking.ShowMMT = true;
            this.ucToolBarBooking.ShowPDF = true;
            this.ucToolBarBooking.ShowRTF = true;
            this.ucToolBarBooking.ShowText = true;
            this.ucToolBarBooking.ShowXLS = true;
            this.ucToolBarBooking.ShowXLSX = true;
            this.ucToolBarBooking.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBooking.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBooking.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBooking.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBooking.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBooking.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBooking.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBooking.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBooking.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBooking.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBooking.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBooking.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBooking.AddClicked += new System.EventHandler(this.ucToolBarBooking_AddClicked);
            this.ucToolBarBooking.EditClicked += new System.EventHandler(this.ucToolBarBooking_EditClicked);
            this.ucToolBarBooking.DeleteClicked += new System.EventHandler(this.ucToolBarBooking_DeleteClicked);
            this.ucToolBarBooking.PrintClicked += new System.EventHandler(this.ucToolBarBooking_PrintClicked);
            this.ucToolBarBooking.CloseClicked += new System.EventHandler(this.ucToolBarBooking_CloseClicked);
            this.ucToolBarBooking.RefreshClicked += new System.EventHandler(this.ucToolBarBooking_RefreshClicked);
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolBar.Controls.Add(this.ucToolBarBooking);
            resources.ApplyResources(this.pnlToolBar, "pnlToolBar");
            this.pnlToolBar.Name = "pnlToolBar";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFooter.Controls.Add(this.lblRowCount);
            this.pnlFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Name = "pnlFooter";
            // 
            // lblRowCount
            // 
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCount.Appearance.Font")));
            this.lblRowCount.Name = "lblRowCount";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // pncFilter
            // 
            this.pncFilter.Controls.Add(this.lcFilter);
            resources.ApplyResources(this.pncFilter, "pncFilter");
            this.pncFilter.Name = "pncFilter";
            // 
            // lcFilter
            // 
            this.lcFilter.Controls.Add(this.btnApply);
            this.lcFilter.Controls.Add(this.gcBookingView);
            this.lcFilter.Controls.Add(this.deDateTo);
            this.lcFilter.Controls.Add(this.deDateFrom);
            this.lcFilter.Controls.Add(this.glkpProject);
            resources.ApplyResources(this.lcFilter, "lcFilter");
            this.lcFilter.Name = "lcFilter";
            this.lcFilter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(475, 273, 250, 350);
            this.lcFilter.Root = this.lcgFilter;
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.lcFilter;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // deDateTo
            // 
            resources.ApplyResources(this.deDateTo, "deDateTo");
            this.deDateTo.Name = "deDateTo";
            this.deDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.Buttons"))))});
            this.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateTo.Properties.CalendarTimeProperties.Buttons"))))});
            this.deDateTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateTo.Properties.Mask.MaskType")));
            this.deDateTo.StyleController = this.lcFilter;
            this.deDateTo.Leave += new System.EventHandler(this.deDateTo_Leave);
            // 
            // deDateFrom
            // 
            resources.ApplyResources(this.deDateFrom, "deDateFrom");
            this.deDateFrom.Name = "deDateFrom";
            this.deDateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateFrom.Properties.Buttons"))))});
            this.deDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDateFrom.Properties.CalendarTimeProperties.Buttons"))))});
            this.deDateFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDateFrom.Properties.Mask.MaskType")));
            this.deDateFrom.StyleController = this.lcFilter;
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
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(480, 300);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.StyleController = this.lcFilter;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProId,
            this.colProName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProId
            // 
            resources.ApplyResources(this.colProId, "colProId");
            this.colProId.FieldName = "PROJECT_ID";
            this.colProId.Name = "colProId";
            // 
            // colProName
            // 
            resources.ApplyResources(this.colProName, "colProName");
            this.colProName.FieldName = "PROJECT";
            this.colProName.Name = "colProName";
            this.colProName.OptionsColumn.ShowCaption = false;
            // 
            // lcgFilter
            // 
            resources.ApplyResources(this.lcgFilter, "lcgFilter");
            this.lcgFilter.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgFilter.GroupBordersVisible = false;
            this.lcgFilter.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject,
            this.lblDateFrom,
            this.lblDateTo,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.lcgFilter.Location = new System.Drawing.Point(0, 0);
            this.lcgFilter.Name = "Root";
            this.lcgFilter.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgFilter.Size = new System.Drawing.Size(891, 421);
            this.lcgFilter.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AllowHtmlStringInCaption = true;
            this.lblProject.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblProject.AppearanceItemCaption.Font")));
            this.lblProject.AppearanceItemCaption.Options.UseFont = true;
            this.lblProject.Control = this.glkpProject;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 4);
            this.lblProject.ShowInCustomizationForm = false;
            this.lblProject.Size = new System.Drawing.Size(541, 28);
            this.lblProject.TextSize = new System.Drawing.Size(46, 14);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AllowHtmlStringInCaption = true;
            this.lblDateFrom.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblDateFrom.AppearanceItemCaption.Font")));
            this.lblDateFrom.AppearanceItemCaption.Options.UseFont = true;
            this.lblDateFrom.Control = this.deDateFrom;
            resources.ApplyResources(this.lblDateFrom, "lblDateFrom");
            this.lblDateFrom.Location = new System.Drawing.Point(541, 0);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.ShowInCustomizationForm = false;
            this.lblDateFrom.Size = new System.Drawing.Size(135, 28);
            this.lblDateFrom.TextSize = new System.Drawing.Size(46, 14);
            // 
            // lblDateTo
            // 
            this.lblDateTo.AllowHtmlStringInCaption = true;
            this.lblDateTo.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblDateTo.AppearanceItemCaption.Font")));
            this.lblDateTo.AppearanceItemCaption.Options.UseFont = true;
            this.lblDateTo.Control = this.deDateTo;
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Location = new System.Drawing.Point(676, 0);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.ShowInCustomizationForm = false;
            this.lblDateTo.Size = new System.Drawing.Size(134, 28);
            this.lblDateTo.TextSize = new System.Drawing.Size(46, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnApply;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(820, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(71, 28);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(71, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.ShowInCustomizationForm = false;
            this.layoutControlItem2.Size = new System.Drawing.Size(71, 28);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcBookingView;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.ShowInCustomizationForm = false;
            this.layoutControlItem4.Size = new System.Drawing.Size(891, 393);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(810, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.ShowInCustomizationForm = false;
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmTDSBookingView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pncFilter);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlToolBar);
            this.Name = "frmTDSBookingView";
            this.EnterClicked += new System.EventHandler(this.frmTDSBookingView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmTDSBookingView_Activated);
            this.Load += new System.EventHandler(this.frmTDSBookingView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTDSBookingView_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBookingView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBookingView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).EndInit();
            this.pnlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pncFilter)).EndInit();
            this.pncFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcFilter)).EndInit();
            this.lcFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcBookingView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBookingView;
        private Bosco.Utility.Controls.ucToolBar ucToolBarBooking;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTDSBooking;
        private DevExpress.XtraGrid.Columns.GridColumn colNatureofPaymentId;
        private DevExpress.XtraGrid.Columns.GridColumn colNatureOfPaymentName;
        private DevExpress.XtraGrid.Columns.GridColumn colAssesValue;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTAXLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingID;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colExpenseLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colPartyLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colExpenseLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colPartyLedgerId;
        private DevExpress.XtraEditors.PanelControl pnlToolBar;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.LabelControl lblRowCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.Columns.GridColumn colDeducteeType;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colDeducteeTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colDeductId;
        private DevExpress.XtraGrid.Columns.GridColumn colChildBookingId;
        private DevExpress.XtraGrid.Columns.GridColumn colChildVoucherId;
        private DevExpress.XtraEditors.PanelControl pncFilter;
        private DevExpress.XtraLayout.LayoutControl lcFilter;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.DateEdit deDateTo;
        private DevExpress.XtraEditors.DateEdit deDateFrom;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup lcgFilter;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.LayoutControlItem lblDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblDateTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colProId;
        private DevExpress.XtraGrid.Columns.GridColumn colProName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}