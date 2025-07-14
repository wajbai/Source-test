namespace ACPP.Modules.Master
{
    partial class frmRenewals
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRenewals));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.lycRenewals = new DevExpress.XtraLayout.LayoutControl();
            this.lblFDType = new DevExpress.XtraEditors.LabelControl();
            this.lblNote = new DevExpress.XtraEditors.LabelControl();
            this.pncRenewals = new DevExpress.XtraEditors.PanelControl();
            this.gcRenewalsView = new DevExpress.XtraGrid.GridControl();
            this.gvRenewals = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colSequenceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRenewalDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaturedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWithdrawalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterestAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterestRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDTransMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRenewalType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRenewalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterestLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDRenewalId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblProjectCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFDLedgerCaptoin = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFDLedger = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFDAccount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAccountCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcFDType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFDTypeCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.lycRenewals)).BeginInit();
            this.lycRenewals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pncRenewals)).BeginInit();
            this.pncRenewals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRenewalsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRenewals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProjectCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDLedgerCaptoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFDType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDTypeCaption)).BeginInit();
            this.SuspendLayout();
            // 
            // lycRenewals
            // 
            this.lycRenewals.AllowCustomizationMenu = false;
            this.lycRenewals.Controls.Add(this.lblFDType);
            this.lycRenewals.Controls.Add(this.lblNote);
            this.lycRenewals.Controls.Add(this.pncRenewals);
            this.lycRenewals.Controls.Add(this.btnOk);
            resources.ApplyResources(this.lycRenewals, "lycRenewals");
            this.lycRenewals.Name = "lycRenewals";
            this.lycRenewals.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(936, 67, 250, 350);
            this.lycRenewals.Root = this.layoutControlGroup1;
            // 
            // lblFDType
            // 
            this.lblFDType.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblFDType.Appearance.Font")));
            this.lblFDType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblFDType.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.lblFDType, "lblFDType");
            this.lblFDType.Name = "lblFDType";
            this.lblFDType.StyleController = this.lycRenewals;
            // 
            // lblNote
            // 
            this.lblNote.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblNote.Appearance.Font")));
            this.lblNote.Appearance.Image = global::ACPP.Properties.Resources.info;
            this.lblNote.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNote.Appearance.PressedImage = global::ACPP.Properties.Resources.info;
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblNote.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            this.lblNote.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal;
            this.lblNote.LineVisible = true;
            this.lblNote.Name = "lblNote";
            this.lblNote.StyleController = this.lycRenewals;
            // 
            // pncRenewals
            // 
            this.pncRenewals.Controls.Add(this.gcRenewalsView);
            resources.ApplyResources(this.pncRenewals, "pncRenewals");
            this.pncRenewals.Name = "pncRenewals";
            // 
            // gcRenewalsView
            // 
            resources.ApplyResources(this.gcRenewalsView, "gcRenewalsView");
            gridLevelNode1.RelationName = "Level1";
            this.gcRenewalsView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcRenewalsView.MainView = this.gvRenewals;
            this.gcRenewalsView.Name = "gcRenewalsView";
            this.gcRenewalsView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkSelect});
            this.gcRenewalsView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRenewals});
            // 
            // gvRenewals
            // 
            this.gvRenewals.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelect,
            this.colSequenceNo,
            this.colRenewalDate,
            this.colMaturedOn,
            this.colReceiptNo,
            this.colWithdrawalAmount,
            this.colInterestAmount,
            this.colTDSAmount,
            this.colChargeMode,
            this.colChargeAmount,
            this.colInterestRate,
            this.colFDTransMode,
            this.colRenewalType,
            this.colRenewalAmount,
            this.colInterestLedgerId,
            this.colBankLedgerId,
            this.colFDAccountId,
            this.colFDRenewalId});
            this.gvRenewals.GridControl = this.gcRenewalsView;
            this.gvRenewals.Name = "gvRenewals";
            this.gvRenewals.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRenewals.OptionsView.ShowGroupPanel = false;
            this.gvRenewals.OptionsView.ShowIndicator = false;
            this.gvRenewals.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvRenewals_CustomRowCellEdit);
            this.gvRenewals.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvRenewals_ShowingEditor);
            this.gvRenewals.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvRenewals_CustomColumnDisplayText);
            // 
            // colSelect
            // 
            this.colSelect.ColumnEdit = this.chkSelect;
            this.colSelect.FieldName = "SELECT";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.FixedWidth = true;
            this.colSelect.OptionsColumn.ShowCaption = false;
            resources.ApplyResources(this.colSelect, "colSelect");
            // 
            // chkSelect
            // 
            resources.ApplyResources(this.chkSelect, "chkSelect");
            this.chkSelect.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkSelect.ValueChecked = 1;
            this.chkSelect.ValueGrayed = 2;
            this.chkSelect.ValueUnchecked = 0;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // colSequenceNo
            // 
            this.colSequenceNo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSequenceNo.AppearanceHeader.Font")));
            this.colSequenceNo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSequenceNo, "colSequenceNo");
            this.colSequenceNo.Name = "colSequenceNo";
            this.colSequenceNo.OptionsColumn.AllowEdit = false;
            this.colSequenceNo.OptionsColumn.FixedWidth = true;
            // 
            // colRenewalDate
            // 
            this.colRenewalDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRenewalDate.AppearanceHeader.Font")));
            this.colRenewalDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRenewalDate, "colRenewalDate");
            this.colRenewalDate.DisplayFormat.FormatString = "d";
            this.colRenewalDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colRenewalDate.FieldName = "RENEWAL_DATE";
            this.colRenewalDate.Name = "colRenewalDate";
            this.colRenewalDate.OptionsColumn.AllowEdit = false;
            // 
            // colMaturedOn
            // 
            this.colMaturedOn.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colMaturedOn.AppearanceHeader.Font")));
            this.colMaturedOn.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colMaturedOn, "colMaturedOn");
            this.colMaturedOn.DisplayFormat.FormatString = "d";
            this.colMaturedOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colMaturedOn.FieldName = "MATURITY_DATE";
            this.colMaturedOn.Name = "colMaturedOn";
            this.colMaturedOn.OptionsColumn.AllowEdit = false;
            // 
            // colReceiptNo
            // 
            this.colReceiptNo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colReceiptNo.AppearanceHeader.Font")));
            this.colReceiptNo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colReceiptNo, "colReceiptNo");
            this.colReceiptNo.FieldName = "RECEIPT_NO";
            this.colReceiptNo.Name = "colReceiptNo";
            this.colReceiptNo.OptionsColumn.AllowEdit = false;
            // 
            // colWithdrawalAmount
            // 
            this.colWithdrawalAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colWithdrawalAmount.AppearanceHeader.Font")));
            this.colWithdrawalAmount.AppearanceHeader.Options.UseFont = true;
            this.colWithdrawalAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colWithdrawalAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colWithdrawalAmount, "colWithdrawalAmount");
            this.colWithdrawalAmount.DisplayFormat.FormatString = "n";
            this.colWithdrawalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWithdrawalAmount.FieldName = "WITHDRAWAL_AMOUNT";
            this.colWithdrawalAmount.Name = "colWithdrawalAmount";
            this.colWithdrawalAmount.OptionsColumn.AllowEdit = false;
            // 
            // colInterestAmount
            // 
            this.colInterestAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colInterestAmount.AppearanceHeader.Font")));
            this.colInterestAmount.AppearanceHeader.Options.UseFont = true;
            this.colInterestAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colInterestAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInterestAmount, "colInterestAmount");
            this.colInterestAmount.DisplayFormat.FormatString = "n";
            this.colInterestAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInterestAmount.FieldName = "INTEREST_AMOUNT";
            this.colInterestAmount.Name = "colInterestAmount";
            this.colInterestAmount.OptionsColumn.AllowEdit = false;
            // 
            // colTDSAmount
            // 
            this.colTDSAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTDSAmount.AppearanceHeader.Font")));
            this.colTDSAmount.AppearanceHeader.Options.UseFont = true;
            this.colTDSAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colTDSAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colTDSAmount, "colTDSAmount");
            this.colTDSAmount.DisplayFormat.FormatString = "n";
            this.colTDSAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTDSAmount.FieldName = "TDS_AMOUNT";
            this.colTDSAmount.Name = "colTDSAmount";
            this.colTDSAmount.OptionsColumn.AllowEdit = false;
            // 
            // colChargeMode
            // 
            this.colChargeMode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colChargeMode.AppearanceHeader.Font")));
            this.colChargeMode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colChargeMode, "colChargeMode");
            this.colChargeMode.FieldName = "CHARGE_MODE_NAME";
            this.colChargeMode.Name = "colChargeMode";
            this.colChargeMode.OptionsColumn.AllowEdit = false;
            // 
            // colChargeAmount
            // 
            this.colChargeAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colChargeAmount.AppearanceHeader.Font")));
            this.colChargeAmount.AppearanceHeader.Options.UseFont = true;
            this.colChargeAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colChargeAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colChargeAmount, "colChargeAmount");
            this.colChargeAmount.DisplayFormat.FormatString = "n";
            this.colChargeAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colChargeAmount.FieldName = "CHARGE_AMOUNT";
            this.colChargeAmount.Name = "colChargeAmount";
            this.colChargeAmount.OptionsColumn.AllowEdit = false;
            // 
            // colInterestRate
            // 
            this.colInterestRate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colInterestRate.AppearanceHeader.Font")));
            this.colInterestRate.AppearanceHeader.Options.UseFont = true;
            this.colInterestRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colInterestRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInterestRate, "colInterestRate");
            this.colInterestRate.DisplayFormat.FormatString = "n";
            this.colInterestRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInterestRate.FieldName = "INTEREST_RATE";
            this.colInterestRate.Name = "colInterestRate";
            this.colInterestRate.OptionsColumn.AllowEdit = false;
            // 
            // colFDTransMode
            // 
            this.colFDTransMode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colFDTransMode.AppearanceHeader.Font")));
            this.colFDTransMode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colFDTransMode, "colFDTransMode");
            this.colFDTransMode.FieldName = "FD_TRANS_MODE";
            this.colFDTransMode.Name = "colFDTransMode";
            this.colFDTransMode.OptionsColumn.AllowEdit = false;
            // 
            // colRenewalType
            // 
            this.colRenewalType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRenewalType.AppearanceHeader.Font")));
            this.colRenewalType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRenewalType, "colRenewalType");
            this.colRenewalType.FieldName = "RENEWAL_TYPE";
            this.colRenewalType.Name = "colRenewalType";
            this.colRenewalType.OptionsColumn.AllowEdit = false;
            // 
            // colRenewalAmount
            // 
            this.colRenewalAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRenewalAmount.AppearanceHeader.Font")));
            this.colRenewalAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRenewalAmount, "colRenewalAmount");
            this.colRenewalAmount.DisplayFormat.FormatString = "n";
            this.colRenewalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRenewalAmount.Name = "colRenewalAmount";
            // 
            // colInterestLedgerId
            // 
            resources.ApplyResources(this.colInterestLedgerId, "colInterestLedgerId");
            this.colInterestLedgerId.FieldName = "INTEREST_LEDGER_ID";
            this.colInterestLedgerId.Name = "colInterestLedgerId";
            // 
            // colBankLedgerId
            // 
            resources.ApplyResources(this.colBankLedgerId, "colBankLedgerId");
            this.colBankLedgerId.FieldName = "BANK_LEDGER_ID";
            this.colBankLedgerId.Name = "colBankLedgerId";
            // 
            // colFDAccountId
            // 
            resources.ApplyResources(this.colFDAccountId, "colFDAccountId");
            this.colFDAccountId.FieldName = "FD_ACCOUNT_ID";
            this.colFDAccountId.Name = "colFDAccountId";
            // 
            // colFDRenewalId
            // 
            resources.ApplyResources(this.colFDRenewalId, "colFDRenewalId");
            this.colFDRenewalId.FieldName = "FD_RENEWAL_ID";
            this.colFDRenewalId.Name = "colFDRenewalId";
            // 
            // btnOk
            // 
            this.btnOk.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.StyleController = this.lycRenewals;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject,
            this.lblProjectCaption,
            this.lblFDLedgerCaptoin,
            this.lblFDLedger,
            this.layoutControlItem1,
            this.lblFDAccount,
            this.lblAccountCaption,
            this.layoutControlItem2,
            this.lcClose,
            this.lcFDType,
            this.lblFDTypeCaption});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(811, 317);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AllowHotTrack = false;
            this.lblProject.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblProject.AppearanceItemCaption.Font")));
            this.lblProject.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(99, 0);
            this.lblProject.MinSize = new System.Drawing.Size(95, 17);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(712, 26);
            this.lblProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblProject.TextSize = new System.Drawing.Size(91, 13);
            // 
            // lblProjectCaption
            // 
            this.lblProjectCaption.AllowHotTrack = false;
            resources.ApplyResources(this.lblProjectCaption, "lblProjectCaption");
            this.lblProjectCaption.Location = new System.Drawing.Point(0, 0);
            this.lblProjectCaption.MaxSize = new System.Drawing.Size(99, 26);
            this.lblProjectCaption.MinSize = new System.Drawing.Size(99, 26);
            this.lblProjectCaption.Name = "lblProjectCaption";
            this.lblProjectCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 2, 2);
            this.lblProjectCaption.Size = new System.Drawing.Size(99, 26);
            this.lblProjectCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblProjectCaption.TextSize = new System.Drawing.Size(91, 13);
            // 
            // lblFDLedgerCaptoin
            // 
            this.lblFDLedgerCaptoin.AllowHotTrack = false;
            resources.ApplyResources(this.lblFDLedgerCaptoin, "lblFDLedgerCaptoin");
            this.lblFDLedgerCaptoin.Location = new System.Drawing.Point(0, 26);
            this.lblFDLedgerCaptoin.MaxSize = new System.Drawing.Size(99, 26);
            this.lblFDLedgerCaptoin.MinSize = new System.Drawing.Size(99, 26);
            this.lblFDLedgerCaptoin.Name = "lblFDLedgerCaptoin";
            this.lblFDLedgerCaptoin.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 2, 2);
            this.lblFDLedgerCaptoin.Size = new System.Drawing.Size(99, 26);
            this.lblFDLedgerCaptoin.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFDLedgerCaptoin.TextSize = new System.Drawing.Size(91, 13);
            // 
            // lblFDLedger
            // 
            this.lblFDLedger.AllowHotTrack = false;
            this.lblFDLedger.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblFDLedger.AppearanceItemCaption.Font")));
            this.lblFDLedger.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblFDLedger, "lblFDLedger");
            this.lblFDLedger.Location = new System.Drawing.Point(99, 26);
            this.lblFDLedger.MinSize = new System.Drawing.Size(60, 17);
            this.lblFDLedger.Name = "lblFDLedger";
            this.lblFDLedger.Size = new System.Drawing.Size(267, 26);
            this.lblFDLedger.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFDLedger.TextSize = new System.Drawing.Size(91, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pncRenewals;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(811, 230);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblFDAccount
            // 
            this.lblFDAccount.AllowHotTrack = false;
            this.lblFDAccount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblFDAccount.AppearanceItemCaption.Font")));
            this.lblFDAccount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblFDAccount, "lblFDAccount");
            this.lblFDAccount.Location = new System.Drawing.Point(411, 26);
            this.lblFDAccount.MinSize = new System.Drawing.Size(95, 17);
            this.lblFDAccount.Name = "lblFDAccount";
            this.lblFDAccount.Size = new System.Drawing.Size(203, 26);
            this.lblFDAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFDAccount.TextSize = new System.Drawing.Size(91, 13);
            // 
            // lblAccountCaption
            // 
            this.lblAccountCaption.AllowHotTrack = false;
            resources.ApplyResources(this.lblAccountCaption, "lblAccountCaption");
            this.lblAccountCaption.Location = new System.Drawing.Point(366, 26);
            this.lblAccountCaption.MaxSize = new System.Drawing.Size(45, 26);
            this.lblAccountCaption.MinSize = new System.Drawing.Size(45, 26);
            this.lblAccountCaption.Name = "lblAccountCaption";
            this.lblAccountCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 2, 2);
            this.lblAccountCaption.Size = new System.Drawing.Size(45, 26);
            this.lblAccountCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAccountCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblAccountCaption.TextSize = new System.Drawing.Size(32, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lblNote;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 282);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(739, 35);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lcClose
            // 
            this.lcClose.Control = this.btnOk;
            this.lcClose.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcClose, "lcClose");
            this.lcClose.Location = new System.Drawing.Point(739, 282);
            this.lcClose.Name = "lcClose";
            this.lcClose.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 5, 2, 2);
            this.lcClose.Size = new System.Drawing.Size(72, 35);
            this.lcClose.TextSize = new System.Drawing.Size(0, 0);
            this.lcClose.TextToControlDistance = 0;
            this.lcClose.TextVisible = false;
            this.lcClose.TrimClientAreaToControl = false;
            // 
            // lcFDType
            // 
            this.lcFDType.Control = this.lblFDType;
            this.lcFDType.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcFDType, "lcFDType");
            this.lcFDType.Location = new System.Drawing.Point(705, 26);
            this.lcFDType.MinSize = new System.Drawing.Size(55, 23);
            this.lcFDType.Name = "lcFDType";
            this.lcFDType.Size = new System.Drawing.Size(106, 26);
            this.lcFDType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcFDType.TextSize = new System.Drawing.Size(0, 0);
            this.lcFDType.TextToControlDistance = 0;
            this.lcFDType.TextVisible = false;
            this.lcFDType.TrimClientAreaToControl = false;
            // 
            // lblFDTypeCaption
            // 
            this.lblFDTypeCaption.AllowHotTrack = false;
            resources.ApplyResources(this.lblFDTypeCaption, "lblFDTypeCaption");
            this.lblFDTypeCaption.Location = new System.Drawing.Point(614, 26);
            this.lblFDTypeCaption.MaxSize = new System.Drawing.Size(91, 26);
            this.lblFDTypeCaption.MinSize = new System.Drawing.Size(91, 26);
            this.lblFDTypeCaption.Name = "lblFDTypeCaption";
            this.lblFDTypeCaption.Size = new System.Drawing.Size(91, 26);
            this.lblFDTypeCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFDTypeCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblFDTypeCaption.TextSize = new System.Drawing.Size(82, 13);
            // 
            // frmRenewals
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lycRenewals);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRenewals";
            this.Load += new System.EventHandler(this.frmRenewals_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lycRenewals)).EndInit();
            this.lycRenewals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pncRenewals)).EndInit();
            this.pncRenewals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRenewalsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRenewals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProjectCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDLedgerCaptoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcFDType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDTypeCaption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lycRenewals;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleLabelItem lblProjectCaption;
        private DevExpress.XtraLayout.SimpleLabelItem lblProject;
        private DevExpress.XtraEditors.PanelControl pncRenewals;
        private DevExpress.XtraGrid.GridControl gcRenewalsView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRenewals;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSelect;
        private DevExpress.XtraLayout.SimpleLabelItem lblFDLedgerCaptoin;
        private DevExpress.XtraLayout.SimpleLabelItem lblFDLedger;
        private DevExpress.XtraLayout.SimpleLabelItem lblAccountCaption;
        private DevExpress.XtraLayout.SimpleLabelItem lblFDAccount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colSequenceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colRenewalDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRenewalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colInterestRate;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptNo;
        private DevExpress.XtraGrid.Columns.GridColumn colInterestAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colMaturedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colInterestLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colBankLedgerId;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem lcClose;
        private DevExpress.XtraGrid.Columns.GridColumn colFDAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colFDRenewalId;
        private DevExpress.XtraGrid.Columns.GridColumn colWithdrawalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRenewalType;
        private DevExpress.XtraEditors.LabelControl lblNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl lblFDType;
        private DevExpress.XtraLayout.LayoutControlItem lcFDType;
        private DevExpress.XtraLayout.SimpleLabelItem lblFDTypeCaption;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeMode;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colFDTransMode;
    }
}