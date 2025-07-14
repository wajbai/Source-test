namespace ACPP.Modules.Master
{
    partial class FrmFDChangeProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFDChangeProject));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.lycRenewals = new DevExpress.XtraLayout.LayoutControl();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblNote = new DevExpress.XtraEditors.LabelControl();
            this.glkpProDetails = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pncRenewals = new DevExpress.XtraEditors.PanelControl();
            this.gcRenewalsView = new DevExpress.XtraGrid.GridControl();
            this.gvRenewals = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSequenceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRenewalDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRenewalType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterestAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaturedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterestLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFDRenewalId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrinicipalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblProjectCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFDLedgerCaptoin = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFDLedger = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAccountCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblFDAccount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblOP_Invest = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblNewProjectCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblBankLedger = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcchangeproject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblBankLedgerCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.lycRenewals)).BeginInit();
            this.lycRenewals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOP_Invest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewProjectCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcchangeproject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankLedgerCaption)).BeginInit();
            this.SuspendLayout();
            // 
            // lycRenewals
            // 
            this.lycRenewals.AllowCustomizationMenu = false;
            this.lycRenewals.Controls.Add(this.btnUpdate);
            this.lycRenewals.Controls.Add(this.btnClose);
            this.lycRenewals.Controls.Add(this.lblNote);
            this.lycRenewals.Controls.Add(this.glkpProDetails);
            this.lycRenewals.Controls.Add(this.pncRenewals);
            resources.ApplyResources(this.lycRenewals, "lycRenewals");
            this.lycRenewals.Name = "lycRenewals";
            this.lycRenewals.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(624, 145, 250, 350);
            this.lycRenewals.Root = this.layoutControlGroup1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.StyleController = this.lycRenewals;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lycRenewals;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblNote
            // 
            this.lblNote.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblNote.Appearance.Font")));
            this.lblNote.Appearance.Image = global::ACPP.Properties.Resources.info;
            this.lblNote.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNote.Appearance.PressedImage = global::ACPP.Properties.Resources.info;
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblNote.Name = "lblNote";
            this.lblNote.StyleController = this.lycRenewals;
            // 
            // glkpProDetails
            // 
            resources.ApplyResources(this.glkpProDetails, "glkpProDetails");
            this.glkpProDetails.Name = "glkpProDetails";
            this.glkpProDetails.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProDetails.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProDetails.Properties.Buttons"))))});
            this.glkpProDetails.Properties.NullText = resources.GetString("glkpProDetails.Properties.NullText");
            this.glkpProDetails.Properties.PopupFormMinSize = new System.Drawing.Size(674, 0);
            this.glkpProDetails.Properties.PopupFormSize = new System.Drawing.Size(674, 50);
            this.glkpProDetails.Properties.View = this.gridView1;
            this.glkpProDetails.StyleController = this.lycRenewals;
            this.glkpProDetails.Tag = "PR";
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
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
            this.colSequenceNo,
            this.colRenewalDate,
            this.colReceiptNo,
            this.colRenewalType,
            this.colAmount,
            this.colInterestAmount,
            this.colTDSAmount,
            this.colMaturedOn,
            this.colInterestLedgerId,
            this.colBankLedgerId,
            this.colFDAccountId,
            this.colFDRenewalId,
            this.colPrinicipalAmount});
            this.gvRenewals.CustomizationFormBounds = new System.Drawing.Rectangle(548, 266, 216, 183);
            this.gvRenewals.GridControl = this.gcRenewalsView;
            this.gvRenewals.Name = "gvRenewals";
            this.gvRenewals.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRenewals.OptionsView.ShowGroupPanel = false;
            this.gvRenewals.OptionsView.ShowIndicator = false;
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
            // colReceiptNo
            // 
            this.colReceiptNo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colReceiptNo.AppearanceHeader.Font")));
            this.colReceiptNo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colReceiptNo, "colReceiptNo");
            this.colReceiptNo.FieldName = "RECEIPT_NO";
            this.colReceiptNo.Name = "colReceiptNo";
            this.colReceiptNo.OptionsColumn.AllowEdit = false;
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
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAmount.AppearanceHeader.Font")));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            // 
            // colInterestAmount
            // 
            this.colInterestAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colInterestAmount.AppearanceHeader.Font")));
            this.colInterestAmount.AppearanceHeader.Options.UseFont = true;
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
            resources.ApplyResources(this.colTDSAmount, "colTDSAmount");
            this.colTDSAmount.DisplayFormat.FormatString = "n";
            this.colTDSAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTDSAmount.FieldName = "TDS_AMOUNT";
            this.colTDSAmount.Name = "colTDSAmount";
            this.colTDSAmount.OptionsColumn.AllowEdit = false;
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
            // colPrinicipalAmount
            // 
            resources.ApplyResources(this.colPrinicipalAmount, "colPrinicipalAmount");
            this.colPrinicipalAmount.DisplayFormat.FormatString = "n";
            this.colPrinicipalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrinicipalAmount.FieldName = "PRINICIPAL_AMOUNT";
            this.colPrinicipalAmount.Name = "colPrinicipalAmount";
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
            this.lblAccountCaption,
            this.lblFDAccount,
            this.lblOP_Invest,
            this.lblNewProjectCaption,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.lblBankLedger,
            this.lcClose,
            this.lcchangeproject,
            this.lblBankLedgerCaption});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(742, 317);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AllowHotTrack = false;
            this.lblProject.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblProject.AppearanceItemCaption.Font")));
            this.lblProject.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(119, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(623, 22);
            this.lblProject.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lblProjectCaption
            // 
            this.lblProjectCaption.AllowHotTrack = false;
            this.lblProjectCaption.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblProjectCaption.AppearanceItemCaption.Font")));
            this.lblProjectCaption.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblProjectCaption, "lblProjectCaption");
            this.lblProjectCaption.Location = new System.Drawing.Point(0, 0);
            this.lblProjectCaption.MaxSize = new System.Drawing.Size(119, 22);
            this.lblProjectCaption.MinSize = new System.Drawing.Size(119, 22);
            this.lblProjectCaption.Name = "lblProjectCaption";
            this.lblProjectCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 4, 4);
            this.lblProjectCaption.Size = new System.Drawing.Size(119, 22);
            this.lblProjectCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblProjectCaption.TextSize = new System.Drawing.Size(103, 14);
            // 
            // lblFDLedgerCaptoin
            // 
            this.lblFDLedgerCaptoin.AllowHotTrack = false;
            this.lblFDLedgerCaptoin.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblFDLedgerCaptoin.AppearanceItemCaption.Font")));
            this.lblFDLedgerCaptoin.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblFDLedgerCaptoin, "lblFDLedgerCaptoin");
            this.lblFDLedgerCaptoin.Location = new System.Drawing.Point(0, 22);
            this.lblFDLedgerCaptoin.MaxSize = new System.Drawing.Size(119, 22);
            this.lblFDLedgerCaptoin.MinSize = new System.Drawing.Size(119, 22);
            this.lblFDLedgerCaptoin.Name = "lblFDLedgerCaptoin";
            this.lblFDLedgerCaptoin.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 4, 4);
            this.lblFDLedgerCaptoin.Size = new System.Drawing.Size(119, 22);
            this.lblFDLedgerCaptoin.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFDLedgerCaptoin.TextSize = new System.Drawing.Size(103, 14);
            // 
            // lblFDLedger
            // 
            this.lblFDLedger.AllowHotTrack = false;
            this.lblFDLedger.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblFDLedger.AppearanceItemCaption.Font")));
            this.lblFDLedger.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblFDLedger, "lblFDLedger");
            this.lblFDLedger.Location = new System.Drawing.Point(119, 22);
            this.lblFDLedger.MaxSize = new System.Drawing.Size(213, 22);
            this.lblFDLedger.MinSize = new System.Drawing.Size(213, 22);
            this.lblFDLedger.Name = "lblFDLedger";
            this.lblFDLedger.Size = new System.Drawing.Size(213, 22);
            this.lblFDLedger.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFDLedger.TextSize = new System.Drawing.Size(103, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pncRenewals;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(742, 195);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblAccountCaption
            // 
            this.lblAccountCaption.AllowHotTrack = false;
            this.lblAccountCaption.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblAccountCaption.AppearanceItemCaption.Font")));
            this.lblAccountCaption.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblAccountCaption, "lblAccountCaption");
            this.lblAccountCaption.Location = new System.Drawing.Point(0, 44);
            this.lblAccountCaption.MaxSize = new System.Drawing.Size(119, 22);
            this.lblAccountCaption.MinSize = new System.Drawing.Size(119, 22);
            this.lblAccountCaption.Name = "lblAccountCaption";
            this.lblAccountCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 4, 4);
            this.lblAccountCaption.Size = new System.Drawing.Size(119, 22);
            this.lblAccountCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAccountCaption.TextSize = new System.Drawing.Size(103, 14);
            // 
            // lblFDAccount
            // 
            this.lblFDAccount.AllowHotTrack = false;
            this.lblFDAccount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblFDAccount.AppearanceItemCaption.Font")));
            this.lblFDAccount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblFDAccount, "lblFDAccount");
            this.lblFDAccount.Location = new System.Drawing.Point(119, 44);
            this.lblFDAccount.MaxSize = new System.Drawing.Size(213, 22);
            this.lblFDAccount.MinSize = new System.Drawing.Size(213, 22);
            this.lblFDAccount.Name = "lblFDAccount";
            this.lblFDAccount.Size = new System.Drawing.Size(213, 22);
            this.lblFDAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFDAccount.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lblOP_Invest
            // 
            this.lblOP_Invest.AllowHotTrack = false;
            this.lblOP_Invest.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblOP_Invest.AppearanceItemCaption.Font")));
            this.lblOP_Invest.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblOP_Invest, "lblOP_Invest");
            this.lblOP_Invest.Location = new System.Drawing.Point(332, 44);
            this.lblOP_Invest.Name = "lblOP_Invest";
            this.lblOP_Invest.Size = new System.Drawing.Size(410, 22);
            this.lblOP_Invest.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lblNewProjectCaption
            // 
            this.lblNewProjectCaption.AllowHotTrack = false;
            this.lblNewProjectCaption.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblNewProjectCaption.AppearanceItemCaption.Font")));
            this.lblNewProjectCaption.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblNewProjectCaption, "lblNewProjectCaption");
            this.lblNewProjectCaption.Location = new System.Drawing.Point(0, 66);
            this.lblNewProjectCaption.MaxSize = new System.Drawing.Size(119, 24);
            this.lblNewProjectCaption.MinSize = new System.Drawing.Size(119, 24);
            this.lblNewProjectCaption.Name = "lblNewProjectCaption";
            this.lblNewProjectCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 4, 4);
            this.lblNewProjectCaption.Size = new System.Drawing.Size(119, 24);
            this.lblNewProjectCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblNewProjectCaption.TextSize = new System.Drawing.Size(103, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.glkpProDetails;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(119, 66);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(623, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblNote;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 285);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(569, 32);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblBankLedger
            // 
            this.lblBankLedger.AllowHotTrack = false;
            this.lblBankLedger.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblBankLedger.AppearanceItemCaption.Font")));
            this.lblBankLedger.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblBankLedger, "lblBankLedger");
            this.lblBankLedger.Location = new System.Drawing.Point(439, 22);
            this.lblBankLedger.Name = "lblBankLedger";
            this.lblBankLedger.Size = new System.Drawing.Size(303, 22);
            this.lblBankLedger.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lcClose
            // 
            this.lcClose.Control = this.btnClose;
            this.lcClose.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcClose, "lcClose");
            this.lcClose.Location = new System.Drawing.Point(673, 285);
            this.lcClose.MaxSize = new System.Drawing.Size(69, 32);
            this.lcClose.MinSize = new System.Drawing.Size(69, 32);
            this.lcClose.Name = "lcClose";
            this.lcClose.Size = new System.Drawing.Size(69, 32);
            this.lcClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcClose.TextSize = new System.Drawing.Size(0, 0);
            this.lcClose.TextToControlDistance = 0;
            this.lcClose.TextVisible = false;
            this.lcClose.TrimClientAreaToControl = false;
            // 
            // lcchangeproject
            // 
            this.lcchangeproject.Control = this.btnUpdate;
            resources.ApplyResources(this.lcchangeproject, "lcchangeproject");
            this.lcchangeproject.Location = new System.Drawing.Point(569, 285);
            this.lcchangeproject.MaxSize = new System.Drawing.Size(104, 32);
            this.lcchangeproject.MinSize = new System.Drawing.Size(104, 32);
            this.lcchangeproject.Name = "lcchangeproject";
            this.lcchangeproject.Size = new System.Drawing.Size(104, 32);
            this.lcchangeproject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcchangeproject.TextSize = new System.Drawing.Size(0, 0);
            this.lcchangeproject.TextToControlDistance = 0;
            this.lcchangeproject.TextVisible = false;
            // 
            // lblBankLedgerCaption
            // 
            this.lblBankLedgerCaption.AllowHotTrack = false;
            this.lblBankLedgerCaption.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.lblBankLedgerCaption.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblBankLedgerCaption, "lblBankLedgerCaption");
            this.lblBankLedgerCaption.Location = new System.Drawing.Point(332, 22);
            this.lblBankLedgerCaption.MaxSize = new System.Drawing.Size(107, 22);
            this.lblBankLedgerCaption.MinSize = new System.Drawing.Size(107, 22);
            this.lblBankLedgerCaption.Name = "lblBankLedgerCaption";
            this.lblBankLedgerCaption.Size = new System.Drawing.Size(107, 22);
            this.lblBankLedgerCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblBankLedgerCaption.TextSize = new System.Drawing.Size(103, 14);
            // 
            // FrmFDChangeProject
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lycRenewals);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmFDChangeProject";
            ((System.ComponentModel.ISupportInitialize)(this.lycRenewals)).EndInit();
            this.lycRenewals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpProDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFDAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOP_Invest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewProjectCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcchangeproject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankLedgerCaption)).EndInit();
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
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSelect;
        private DevExpress.XtraLayout.SimpleLabelItem lblFDLedgerCaptoin;
        private DevExpress.XtraLayout.SimpleLabelItem lblFDLedger;
        private DevExpress.XtraLayout.SimpleLabelItem lblAccountCaption;
        private DevExpress.XtraLayout.SimpleLabelItem lblFDAccount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colSequenceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colRenewalDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colMaturedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colInterestLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colBankLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colFDAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colFDRenewalId;
        private DevExpress.XtraGrid.Columns.GridColumn colPrinicipalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRenewalType;
        private DevExpress.XtraLayout.SimpleLabelItem lblOP_Invest;
        private DevExpress.XtraLayout.SimpleLabelItem lblNewProjectCaption;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SimpleLabelItem lblBankLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSAmount;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem lcClose;
        private DevExpress.XtraGrid.Columns.GridColumn colInterestAmount;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraLayout.LayoutControlItem lcchangeproject;
        private DevExpress.XtraLayout.SimpleLabelItem lblBankLedgerCaption;
    }
}