namespace ACPP.Modules.Inventory.Asset
{
    partial class frmInsuranceRenewalView
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gvInsrenewal = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRenId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRenAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRenewal = new DevExpress.XtraGrid.GridControl();
            this.gvRenewal = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRenewalId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInsId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVouNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVouDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNarration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.dtDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucToolBar = new ACPP.Modules.UIControls.ucToolBar();
            this.dtDateTo = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRecord = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvInsrenewal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRenewal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRenewal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // gvInsrenewal
            // 
            this.gvInsrenewal.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvInsrenewal.Appearance.FocusedRow.Options.UseFont = true;
            this.gvInsrenewal.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvInsrenewal.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvInsrenewal.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRenId,
            this.colItemId,
            this.colAssetId,
            this.colRenAmt,
            this.colDueDate});
            this.gvInsrenewal.GridControl = this.gcRenewal;
            this.gvInsrenewal.Name = "gvInsrenewal";
            this.gvInsrenewal.OptionsBehavior.Editable = false;
            this.gvInsrenewal.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvInsrenewal.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.gvInsrenewal.OptionsView.ShowGroupPanel = false;
            this.gvInsrenewal.OptionsView.ShowIndicator = false;
            this.gvInsrenewal.DoubleClick += new System.EventHandler(this.gvInsrenewal_DoubleClick);
            // 
            // colRenId
            // 
            this.colRenId.Caption = "Renewal Id";
            this.colRenId.FieldName = "RENEWAL_ID";
            this.colRenId.Name = "colRenId";
            // 
            // colItemId
            // 
            this.colItemId.Caption = "Asset Name";
            this.colItemId.FieldName = "ASSET_NAME";
            this.colItemId.Name = "colItemId";
            this.colItemId.Visible = true;
            this.colItemId.VisibleIndex = 0;
            // 
            // colAssetId
            // 
            this.colAssetId.Caption = "Asset Id";
            this.colAssetId.FieldName = "ASSET_ID";
            this.colAssetId.Name = "colAssetId";
            this.colAssetId.Visible = true;
            this.colAssetId.VisibleIndex = 1;
            // 
            // colRenAmt
            // 
            this.colRenAmt.Caption = "Renewal Amount";
            this.colRenAmt.FieldName = "RENEWAL_AMOUNT";
            this.colRenAmt.Name = "colRenAmt";
            this.colRenAmt.Visible = true;
            this.colRenAmt.VisibleIndex = 2;
            // 
            // colDueDate
            // 
            this.colDueDate.Caption = "Due Date";
            this.colDueDate.FieldName = "DUE_DATE";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.Visible = true;
            this.colDueDate.VisibleIndex = 3;
            // 
            // gcRenewal
            // 
            gridLevelNode2.LevelTemplate = this.gvInsrenewal;
            gridLevelNode2.RelationName = "RenewalDetail";
            this.gcRenewal.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gcRenewal.Location = new System.Drawing.Point(2, 62);
            this.gcRenewal.MainView = this.gvRenewal;
            this.gcRenewal.Name = "gcRenewal";
            this.gcRenewal.Size = new System.Drawing.Size(1003, 439);
            this.gcRenewal.TabIndex = 9;
            this.gcRenewal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRenewal,
            this.gvInsrenewal});
            // 
            // gvRenewal
            // 
            this.gvRenewal.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvRenewal.Appearance.FocusedRow.Options.UseFont = true;
            this.gvRenewal.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvRenewal.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvRenewal.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRenewalId,
            this.colInsId,
            this.colVouNo,
            this.colVouDate,
            this.colNameAddress,
            this.colNarration});
            this.gvRenewal.GridControl = this.gcRenewal;
            this.gvRenewal.Name = "gvRenewal";
            this.gvRenewal.OptionsBehavior.Editable = false;
            this.gvRenewal.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRenewal.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.gvRenewal.OptionsView.ShowGroupPanel = false;
            this.gvRenewal.DoubleClick += new System.EventHandler(this.gvRenewal_DoubleClick);
            this.gvRenewal.RowCountChanged += new System.EventHandler(this.gvRenewal_RowCountChanged);
            // 
            // colRenewalId
            // 
            this.colRenewalId.Caption = "Renewal Id";
            this.colRenewalId.FieldName = "RENEWAL_ID";
            this.colRenewalId.Name = "colRenewalId";
            // 
            // colInsId
            // 
            this.colInsId.Caption = "Ins Id";
            this.colInsId.FieldName = "INS_ID";
            this.colInsId.Name = "colInsId";
            this.colInsId.OptionsColumn.AllowEdit = false;
            this.colInsId.OptionsColumn.AllowFocus = false;
            // 
            // colVouNo
            // 
            this.colVouNo.Caption = "V.No";
            this.colVouNo.FieldName = "VOUCHER_NO";
            this.colVouNo.Name = "colVouNo";
            this.colVouNo.OptionsColumn.AllowEdit = false;
            this.colVouNo.OptionsColumn.AllowFocus = false;
            this.colVouNo.Visible = true;
            this.colVouNo.VisibleIndex = 0;
            // 
            // colVouDate
            // 
            this.colVouDate.Caption = "Voucher Date";
            this.colVouDate.FieldName = "VOUCHER_DATE";
            this.colVouDate.Name = "colVouDate";
            this.colVouDate.OptionsColumn.AllowEdit = false;
            this.colVouDate.OptionsColumn.AllowFocus = false;
            this.colVouDate.Visible = true;
            this.colVouDate.VisibleIndex = 1;
            // 
            // colNameAddress
            // 
            this.colNameAddress.Caption = "Name & Address";
            this.colNameAddress.FieldName = "NAME_ADDRESS";
            this.colNameAddress.Name = "colNameAddress";
            this.colNameAddress.Visible = true;
            this.colNameAddress.VisibleIndex = 2;
            // 
            // colNarration
            // 
            this.colNarration.Caption = "Narration";
            this.colNarration.FieldName = "NARRATION";
            this.colNarration.Name = "colNarration";
            this.colNarration.Visible = true;
            this.colNarration.VisibleIndex = 3;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcRenewal);
            this.layoutControl1.Controls.Add(this.dtDateFrom);
            this.layoutControl1.Controls.Add(this.glkpProject);
            this.layoutControl1.Controls.Add(this.ucToolBar);
            this.layoutControl1.Controls.Add(this.dtDateTo);
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(239, 211, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1007, 527);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Location = new System.Drawing.Point(2, 505);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(129, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 10;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(751, 36);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(59, 22);
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dtDateFrom
            // 
            this.dtDateFrom.EditValue = null;
            this.dtDateFrom.EnterMoveNextControl = true;
            this.dtDateFrom.Location = new System.Drawing.Point(528, 36);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtDateFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtDateFrom.Size = new System.Drawing.Size(83, 20);
            this.dtDateFrom.StyleController = this.layoutControl1;
            this.dtDateFrom.TabIndex = 6;
            // 
            // glkpProject
            // 
            this.glkpProject.EnterMoveNextControl = true;
            this.glkpProject.Location = new System.Drawing.Point(47, 36);
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = "";
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(425, 350);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.Size = new System.Drawing.Size(425, 20);
            this.glkpProject.StyleController = this.layoutControl1;
            this.glkpProject.TabIndex = 5;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProject});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            this.colProjectId.Caption = "Project Id";
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            this.colProject.Caption = "Project";
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 0;
            // 
            // ucToolBar
            // 
            this.ucToolBar.ChangeAddCaption = "&Add";
            this.ucToolBar.ChangeCaption = "&Edit";
            this.ucToolBar.ChangeDeleteCaption = "&Delete";
            this.ucToolBar.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBar.ChangePrintCaption = "&Print";
            this.ucToolBar.DisableAddButton = true;
            this.ucToolBar.DisableCloseButton = true;
            this.ucToolBar.DisableDeleteButton = true;
            this.ucToolBar.DisableDownloadExcel = true;
            this.ucToolBar.DisableEditButton = true;
            this.ucToolBar.DisableMoveTransaction = true;
            this.ucToolBar.DisableNatureofPayments = true;
            this.ucToolBar.DisablePrintButton = true;
            this.ucToolBar.DisableRestoreVoucher = true;
            this.ucToolBar.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar.Name = "ucToolBar";
            this.ucToolBar.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucToolBar.ShowHTML = true;
            this.ucToolBar.ShowMMT = true;
            this.ucToolBar.ShowPDF = true;
            this.ucToolBar.ShowRTF = true;
            this.ucToolBar.ShowText = true;
            this.ucToolBar.ShowXLS = true;
            this.ucToolBar.ShowXLSX = true;
            this.ucToolBar.Size = new System.Drawing.Size(1007, 34);
            this.ucToolBar.TabIndex = 4;
            this.ucToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar.AddClicked += new System.EventHandler(this.ucToolBar_AddClicked);
            this.ucToolBar.EditClicked += new System.EventHandler(this.ucToolBar_EditClicked);
            this.ucToolBar.DeleteClicked += new System.EventHandler(this.ucToolBar_DeleteClicked);
            this.ucToolBar.PrintClicked += new System.EventHandler(this.ucToolBar_PrintClicked);
            this.ucToolBar.CloseClicked += new System.EventHandler(this.ucToolBar_CloseClicked);
            this.ucToolBar.RefreshClicked += new System.EventHandler(this.ucToolBar_RefreshClicked);
            // 
            // dtDateTo
            // 
            this.dtDateTo.EditValue = null;
            this.dtDateTo.EnterMoveNextControl = true;
            this.dtDateTo.Location = new System.Drawing.Point(648, 36);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtDateTo.Size = new System.Drawing.Size(85, 20);
            this.dtDateTo.StyleController = this.layoutControl1;
            this.dtDateTo.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem3,
            this.emptySpaceItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.lblRecordCount,
            this.lblRecord,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1007, 527);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBar;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1007, 34);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.glkpProject;
            this.layoutControlItem2.CustomizationFormText = "Project";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(474, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(474, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(474, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Project";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(40, 20);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(812, 34);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(195, 26);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(195, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(195, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.dtDateFrom;
            this.layoutControlItem3.CustomizationFormText = "From";
            this.layoutControlItem3.Location = new System.Drawing.Point(486, 34);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(127, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(127, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(127, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "From";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(35, 20);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.dtDateTo;
            this.layoutControlItem4.CustomizationFormText = "To";
            this.layoutControlItem4.Location = new System.Drawing.Point(613, 34);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(87, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 2, 2);
            this.layoutControlItem4.Size = new System.Drawing.Size(122, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "To";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(24, 20);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnApply;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(749, 34);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(474, 34);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(12, 26);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(12, 26);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(12, 26);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(735, 34);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(14, 26);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(14, 26);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(14, 26);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcRenewal;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1007, 443);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(133, 503);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(828, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblRecordCount.CustomizationFormText = "0";
            this.lblRecordCount.Location = new System.Drawing.Point(972, 503);
            this.lblRecordCount.MinSize = new System.Drawing.Size(11, 13);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0);
            this.lblRecordCount.Size = new System.Drawing.Size(35, 24);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.Text = "0";
            this.lblRecordCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblRecord
            // 
            this.lblRecord.AllowHotTrack = false;
            this.lblRecord.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecord.AppearanceItemCaption.Options.UseFont = true;
            this.lblRecord.CustomizationFormText = "#";
            this.lblRecord.Location = new System.Drawing.Point(961, 503);
            this.lblRecord.MinSize = new System.Drawing.Size(11, 13);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0);
            this.lblRecord.Size = new System.Drawing.Size(11, 24);
            this.lblRecord.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecord.Text = "#";
            this.lblRecord.TextSize = new System.Drawing.Size(9, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.chkShowFilter;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 503);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(133, 24);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // frmInsuranceRenewalView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 527);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInsuranceRenewalView";
            this.Text = "Insurance Renewal";
            this.Load += new System.EventHandler(this.frmInsuranceRenewalView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvInsrenewal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRenewal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRenewal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UIControls.ucToolBar ucToolBar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.DateEdit dtDateTo;
        private DevExpress.XtraEditors.DateEdit dtDateFrom;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraGrid.GridControl gcRenewal;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRenewal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colRenewalId;
        private DevExpress.XtraGrid.Columns.GridColumn colInsId;
        private DevExpress.XtraGrid.Columns.GridColumn colVouNo;
        private DevExpress.XtraGrid.Columns.GridColumn colVouDate;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInsrenewal;
        private DevExpress.XtraGrid.Columns.GridColumn colNameAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colNarration;
        private DevExpress.XtraGrid.Columns.GridColumn colRenId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetId;
        private DevExpress.XtraGrid.Columns.GridColumn colRenAmt;
        private DevExpress.XtraGrid.Columns.GridColumn colDueDate;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}