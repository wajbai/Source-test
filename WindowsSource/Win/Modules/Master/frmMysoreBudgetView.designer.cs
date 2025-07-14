namespace ACPP.Modules.Master
{
    partial class frmMysoreBudgetView
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
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, null, true, true);
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMysoreBudgetView));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpProjectDetails = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Project = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OptOneTwoMonth = new DevExpress.XtraEditors.RadioGroup();
            this.ucBudget = new Bosco.Utility.Controls.ucToolBar();
            this.pnlBudgetFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblcount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkFilterRow = new DevExpress.XtraEditors.CheckEdit();
            this.gcBudget = new DevExpress.XtraGrid.GridControl();
            this.gvBudget = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBudgetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetMonthRow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetLevelType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectIds = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolDistributionIcon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnBudgetDistributeIcon = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colBudgetAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcMysoreMonths = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBudgetProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProjectDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptOneTwoMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBudgetFooter)).BeginInit();
            this.pnlBudgetFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilterRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBudgetDistributeIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMysoreMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBudgetProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkpProjectDetails);
            this.layoutControl1.Controls.Add(this.OptOneTwoMonth);
            this.layoutControl1.Controls.Add(this.ucBudget);
            this.layoutControl1.Controls.Add(this.pnlBudgetFooter);
            this.layoutControl1.Controls.Add(this.gcBudget);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(329, 235, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(998, 495);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // glkpProjectDetails
            // 
            this.glkpProjectDetails.Location = new System.Drawing.Point(627, 31);
            this.glkpProjectDetails.Name = "glkpProjectDetails";
            this.glkpProjectDetails.Properties.AutoHeight = false;
            this.glkpProjectDetails.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.glkpProjectDetails.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProjectDetails.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpProjectDetails.Properties.ImmediatePopup = true;
            this.glkpProjectDetails.Properties.NullText = "";
            this.glkpProjectDetails.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProjectDetails.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProjectDetails.Properties.View = this.gridLookUpEdit1View;
            this.glkpProjectDetails.Size = new System.Drawing.Size(368, 22);
            this.glkpProjectDetails.StyleController = this.layoutControl1;
            this.glkpProjectDetails.TabIndex = 9;
            this.glkpProjectDetails.EditValueChanged += new System.EventHandler(this.glkpProjectDetails_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.Project});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            this.colProjectId.Caption = "ProjectId";
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // Project
            // 
            this.Project.Caption = "colProject";
            this.Project.FieldName = "PROJECT";
            this.Project.Name = "Project";
            this.Project.Visible = true;
            this.Project.VisibleIndex = 0;
            // 
            // OptOneTwoMonth
            // 
            this.OptOneTwoMonth.Location = new System.Drawing.Point(820, 2);
            this.OptOneTwoMonth.Name = "OptOneTwoMonth";
            this.OptOneTwoMonth.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "One Month"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Two Months")});
            this.OptOneTwoMonth.Size = new System.Drawing.Size(176, 25);
            this.OptOneTwoMonth.StyleController = this.layoutControl1;
            this.OptOneTwoMonth.TabIndex = 8;
            this.OptOneTwoMonth.Visible = false;
            this.OptOneTwoMonth.SelectedIndexChanged += new System.EventHandler(this.OptOneTwoMonth_SelectedIndexChanged);
            // 
            // ucBudget
            // 
            this.ucBudget.ChangeAddCaption = "&Add";
            this.ucBudget.ChangeCaption = "&Edit";
            this.ucBudget.ChangeDeleteCaption = "&Delete";
            this.ucBudget.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucBudget.ChangePostInterestCaption = "P&ost Interest";
            this.ucBudget.ChangePrintCaption = "&Print";
            this.ucBudget.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucBudget.DisableAddButton = true;
            this.ucBudget.DisableAMCRenew = true;
            this.ucBudget.DisableCloseButton = true;
            this.ucBudget.DisableDeleteButton = true;
            this.ucBudget.DisableDownloadExcel = true;
            this.ucBudget.DisableEditButton = true;
            this.ucBudget.DisableMoveTransaction = true;
            this.ucBudget.DisableNatureofPayments = true;
            this.ucBudget.DisablePostInterest = true;
            this.ucBudget.DisablePrintButton = true;
            this.ucBudget.DisableRestoreVoucher = false;
            this.ucBudget.Location = new System.Drawing.Point(0, 2);
            this.ucBudget.Name = "ucBudget";
            this.ucBudget.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucBudget.ShowHTML = true;
            this.ucBudget.ShowMMT = true;
            this.ucBudget.ShowPDF = true;
            this.ucBudget.ShowRTF = true;
            this.ucBudget.ShowText = true;
            this.ucBudget.ShowXLS = true;
            this.ucBudget.ShowXLSX = true;
            this.ucBudget.Size = new System.Drawing.Size(818, 27);
            this.ucBudget.TabIndex = 7;
            this.ucBudget.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucBudget.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucBudget.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucBudget.AddClicked += new System.EventHandler(this.ucBudget_AddClicked);
            this.ucBudget.EditClicked += new System.EventHandler(this.ucBudget_EditClicked);
            this.ucBudget.DeleteClicked += new System.EventHandler(this.ucBudget_DeleteClicked);
            this.ucBudget.PrintClicked += new System.EventHandler(this.ucBudget_PrintClicked);
            this.ucBudget.CloseClicked += new System.EventHandler(this.ucBudget_CloseClicked);
            this.ucBudget.RefreshClicked += new System.EventHandler(this.ucBudget_RefreshClicked);
            // 
            // pnlBudgetFooter
            // 
            this.pnlBudgetFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBudgetFooter.Controls.Add(this.lblcount);
            this.pnlBudgetFooter.Controls.Add(this.lblRecordCount);
            this.pnlBudgetFooter.Controls.Add(this.chkFilterRow);
            this.pnlBudgetFooter.Location = new System.Drawing.Point(2, 448);
            this.pnlBudgetFooter.Name = "pnlBudgetFooter";
            this.pnlBudgetFooter.Size = new System.Drawing.Size(994, 45);
            this.pnlBudgetFooter.TabIndex = 6;
            // 
            // lblcount
            // 
            this.lblcount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblcount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblcount.Location = new System.Drawing.Point(960, 4);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(9, 13);
            this.lblcount.TabIndex = 2;
            this.lblcount.Text = "#";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.Location = new System.Drawing.Point(977, 4);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(7, 13);
            this.lblRecordCount.TabIndex = 1;
            this.lblRecordCount.Text = "0";
            // 
            // chkFilterRow
            // 
            this.chkFilterRow.Location = new System.Drawing.Point(5, 1);
            this.chkFilterRow.Name = "chkFilterRow";
            this.chkFilterRow.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkFilterRow.Properties.Caption = "Show <b>F</b>ilter";
            this.chkFilterRow.Size = new System.Drawing.Size(161, 19);
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolTipTitleItem2.Image")));
            toolTipTitleItem2.Text = "Show Filter (Alt + F)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Check this filter the records";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.chkFilterRow.SuperTip = superToolTip2;
            this.chkFilterRow.TabIndex = 0;
            this.chkFilterRow.CheckedChanged += new System.EventHandler(this.chkFilterRow_CheckedChanged);
            // 
            // gcBudget
            // 
            this.gcBudget.Location = new System.Drawing.Point(2, 57);
            this.gcBudget.MainView = this.gvBudget;
            this.gcBudget.Name = "gcBudget";
            this.gcBudget.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rbtnBudgetDistributeIcon});
            this.gcBudget.Size = new System.Drawing.Size(994, 387);
            this.gcBudget.TabIndex = 1;
            this.gcBudget.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBudget});
            // 
            // gvBudget
            // 
            this.gvBudget.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudget.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBudget.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.gvBudget.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBudget.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBudgetId,
            this.colBudgetMonthRow,
            this.colBudgetName,
            this.colBudgetLevelType,
            this.colBudgetType,
            this.colProject,
            this.colDateFrom,
            this.colDateTo,
            this.colStatus,
            this.colIsActive,
            this.colProjectIds,
            this.colBudgetTypeId,
            this.gccolDistributionIcon,
            this.colBudgetAction});
            this.gvBudget.GridControl = this.gcBudget;
            this.gvBudget.Name = "gvBudget";
            this.gvBudget.OptionsBehavior.Editable = false;
            this.gvBudget.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBudget.OptionsView.ShowGroupPanel = false;
            this.gvBudget.OptionsView.ShowIndicator = false;
            this.gvBudget.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvBudget_RowStyle);
            this.gvBudget.DoubleClick += new System.EventHandler(this.gvBudget_DoubleClick);
            this.gvBudget.RowCountChanged += new System.EventHandler(this.gvBudget_RowCountChanged);
            // 
            // colBudgetId
            // 
            this.colBudgetId.Caption = "Budget Id";
            this.colBudgetId.FieldName = "BUDGET_ID";
            this.colBudgetId.Name = "colBudgetId";
            // 
            // colBudgetMonthRow
            // 
            this.colBudgetMonthRow.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBudgetMonthRow.AppearanceCell.Options.UseFont = true;
            this.colBudgetMonthRow.AppearanceCell.Options.UseTextOptions = true;
            this.colBudgetMonthRow.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBudgetMonthRow.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBudgetMonthRow.AppearanceHeader.Options.UseFont = true;
            this.colBudgetMonthRow.Caption = "S.No";
            this.colBudgetMonthRow.FieldName = "MONTH_ROW";
            this.colBudgetMonthRow.Name = "colBudgetMonthRow";
            this.colBudgetMonthRow.OptionsColumn.AllowEdit = false;
            this.colBudgetMonthRow.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetMonthRow.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colBudgetMonthRow.OptionsColumn.AllowMove = false;
            this.colBudgetMonthRow.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colBudgetMonthRow.Visible = true;
            this.colBudgetMonthRow.VisibleIndex = 0;
            // 
            // colBudgetName
            // 
            this.colBudgetName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBudgetName.AppearanceHeader.Options.UseFont = true;
            this.colBudgetName.Caption = "Budget";
            this.colBudgetName.FieldName = "BUDGET_NAME";
            this.colBudgetName.Name = "colBudgetName";
            this.colBudgetName.OptionsColumn.AllowEdit = false;
            this.colBudgetName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetName.OptionsColumn.AllowMove = false;
            this.colBudgetName.OptionsColumn.ReadOnly = true;
            this.colBudgetName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colBudgetName.Visible = true;
            this.colBudgetName.VisibleIndex = 1;
            this.colBudgetName.Width = 304;
            // 
            // colBudgetLevelType
            // 
            this.colBudgetLevelType.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBudgetLevelType.AppearanceHeader.Options.UseFont = true;
            this.colBudgetLevelType.Caption = "Budget Type";
            this.colBudgetLevelType.FieldName = "BUDGET_LEVEL_NAME";
            this.colBudgetLevelType.Name = "colBudgetLevelType";
            this.colBudgetLevelType.Width = 103;
            // 
            // colBudgetType
            // 
            this.colBudgetType.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBudgetType.AppearanceHeader.Options.UseFont = true;
            this.colBudgetType.Caption = "Type";
            this.colBudgetType.FieldName = "BUDGET_TYPE";
            this.colBudgetType.Name = "colBudgetType";
            this.colBudgetType.OptionsColumn.AllowEdit = false;
            this.colBudgetType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetType.OptionsColumn.ReadOnly = true;
            this.colBudgetType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colBudgetType.Visible = true;
            this.colBudgetType.VisibleIndex = 2;
            this.colBudgetType.Width = 128;
            // 
            // colProject
            // 
            this.colProject.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProject.AppearanceHeader.Options.UseFont = true;
            this.colProject.Caption = "Project";
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            this.colProject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colProject.OptionsColumn.AllowMove = false;
            this.colProject.OptionsColumn.AllowSize = false;
            this.colProject.OptionsColumn.ReadOnly = true;
            this.colProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colProject.Width = 285;
            // 
            // colDateFrom
            // 
            this.colDateFrom.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDateFrom.AppearanceHeader.Options.UseFont = true;
            this.colDateFrom.Caption = "Date From";
            this.colDateFrom.FieldName = "DATE_FROM";
            this.colDateFrom.Name = "colDateFrom";
            this.colDateFrom.OptionsColumn.AllowEdit = false;
            this.colDateFrom.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDateFrom.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDateFrom.OptionsColumn.AllowMove = false;
            this.colDateFrom.OptionsColumn.AllowSize = false;
            this.colDateFrom.OptionsColumn.FixedWidth = true;
            this.colDateFrom.OptionsColumn.ReadOnly = true;
            this.colDateFrom.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDateFrom.Visible = true;
            this.colDateFrom.VisibleIndex = 4;
            this.colDateFrom.Width = 80;
            // 
            // colDateTo
            // 
            this.colDateTo.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDateTo.AppearanceHeader.Options.UseFont = true;
            this.colDateTo.Caption = "Date To";
            this.colDateTo.FieldName = "DATE_TO";
            this.colDateTo.Name = "colDateTo";
            this.colDateTo.OptionsColumn.AllowEdit = false;
            this.colDateTo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDateTo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDateTo.OptionsColumn.AllowMove = false;
            this.colDateTo.OptionsColumn.AllowSize = false;
            this.colDateTo.OptionsColumn.FixedWidth = true;
            this.colDateTo.OptionsColumn.ReadOnly = true;
            this.colDateTo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDateTo.Visible = true;
            this.colDateTo.VisibleIndex = 5;
            this.colDateTo.Width = 80;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.OptionsColumn.AllowMove = false;
            this.colStatus.OptionsColumn.AllowSize = false;
            this.colStatus.OptionsColumn.FixedWidth = true;
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 6;
            this.colStatus.Width = 90;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "IsActive";
            this.colIsActive.FieldName = "IS_ACTIVE";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProjectIds
            // 
            this.colProjectIds.Caption = "Id";
            this.colProjectIds.FieldName = "PROJECT_ID";
            this.colProjectIds.Name = "colProjectIds";
            // 
            // colBudgetTypeId
            // 
            this.colBudgetTypeId.FieldName = "BUDGET_TYPE_ID";
            this.colBudgetTypeId.Name = "colBudgetTypeId";
            // 
            // gccolDistributionIcon
            // 
            this.gccolDistributionIcon.ColumnEdit = this.rbtnBudgetDistributeIcon;
            this.gccolDistributionIcon.FieldName = "IS_MONTH_WISE";
            this.gccolDistributionIcon.Name = "gccolDistributionIcon";
            this.gccolDistributionIcon.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gccolDistributionIcon.OptionsColumn.AllowIncrementalSearch = false;
            this.gccolDistributionIcon.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gccolDistributionIcon.OptionsColumn.AllowMove = false;
            this.gccolDistributionIcon.OptionsColumn.AllowSize = false;
            this.gccolDistributionIcon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gccolDistributionIcon.OptionsColumn.FixedWidth = true;
            this.gccolDistributionIcon.OptionsColumn.ShowCaption = false;
            this.gccolDistributionIcon.OptionsColumn.ShowInCustomizationForm = false;
            this.gccolDistributionIcon.OptionsColumn.ShowInExpressionEditor = false;
            this.gccolDistributionIcon.OptionsColumn.TabStop = false;
            this.gccolDistributionIcon.OptionsFilter.AllowAutoFilter = false;
            this.gccolDistributionIcon.OptionsFilter.AllowFilter = false;
            this.gccolDistributionIcon.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gccolDistributionIcon.ToolTip = "To distribute Annual Budget by Monthly";
            this.gccolDistributionIcon.Width = 28;
            // 
            // rbtnBudgetDistributeIcon
            // 
            this.rbtnBudgetDistributeIcon.AutoHeight = false;
            this.rbtnBudgetDistributeIcon.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("rbtnBudgetDistributeIcon.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.rbtnBudgetDistributeIcon.Name = "rbtnBudgetDistributeIcon";
            // 
            // colBudgetAction
            // 
            this.colBudgetAction.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBudgetAction.AppearanceHeader.Options.UseFont = true;
            this.colBudgetAction.Caption = "Action";
            this.colBudgetAction.FieldName = "BUDGET_ACTION";
            this.colBudgetAction.Name = "colBudgetAction";
            this.colBudgetAction.OptionsColumn.AllowEdit = false;
            this.colBudgetAction.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetAction.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetAction.OptionsColumn.AllowMove = false;
            this.colBudgetAction.OptionsColumn.FixedWidth = true;
            this.colBudgetAction.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colBudgetAction.Visible = true;
            this.colBudgetAction.VisibleIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.lcMysoreMonths,
            this.lciBudgetProject,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(998, 495);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcBudget;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 55);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(998, 391);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.pnlBudgetFooter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 446);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(998, 49);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ucBudget;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.layoutControlItem4.Size = new System.Drawing.Size(818, 29);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lcMysoreMonths
            // 
            this.lcMysoreMonths.Control = this.OptOneTwoMonth;
            this.lcMysoreMonths.CustomizationFormText = "lcMysoreMonths";
            this.lcMysoreMonths.Location = new System.Drawing.Point(818, 0);
            this.lcMysoreMonths.MaxSize = new System.Drawing.Size(180, 29);
            this.lcMysoreMonths.MinSize = new System.Drawing.Size(180, 29);
            this.lcMysoreMonths.Name = "lcMysoreMonths";
            this.lcMysoreMonths.Size = new System.Drawing.Size(180, 29);
            this.lcMysoreMonths.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcMysoreMonths.Text = "lcMysoreMonths";
            this.lcMysoreMonths.TextSize = new System.Drawing.Size(0, 0);
            this.lcMysoreMonths.TextToControlDistance = 0;
            this.lcMysoreMonths.TextVisible = false;
            this.lcMysoreMonths.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lciBudgetProject
            // 
            this.lciBudgetProject.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lciBudgetProject.AppearanceItemCaption.Options.UseFont = true;
            this.lciBudgetProject.Control = this.glkpProjectDetails;
            this.lciBudgetProject.CustomizationFormText = "Project";
            this.lciBudgetProject.Location = new System.Drawing.Point(578, 29);
            this.lciBudgetProject.MinSize = new System.Drawing.Size(95, 24);
            this.lciBudgetProject.Name = "lciBudgetProject";
            this.lciBudgetProject.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 3, 2, 2);
            this.lciBudgetProject.Size = new System.Drawing.Size(420, 26);
            this.lciBudgetProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBudgetProject.Text = "Project";
            this.lciBudgetProject.TextSize = new System.Drawing.Size(41, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 29);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(578, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmMysoreBudgetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 495);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmMysoreBudgetView";
            this.Text = "Budget";
            this.ShowFilterClicked += new System.EventHandler(this.frmBudgetView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmBudgetView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmBudgetView_Activated);
            this.Load += new System.EventHandler(this.frmBudgetView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpProjectDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptOneTwoMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBudgetFooter)).EndInit();
            this.pnlBudgetFooter.ResumeLayout(false);
            this.pnlBudgetFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilterRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBudgetDistributeIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMysoreMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBudgetProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcBudget;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBudget;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PanelControl pnlBudgetFooter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetName;
        private DevExpress.XtraGrid.Columns.GridColumn colDateFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colDateTo;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetId;
        private DevExpress.XtraEditors.CheckEdit chkFilterRow;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraEditors.LabelControl lblcount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectIds;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetType;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetLevelType;
        private Bosco.Utility.Controls.ucToolBar ucBudget;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gccolDistributionIcon;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnBudgetDistributeIcon;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetMonthRow;
        private DevExpress.XtraEditors.RadioGroup OptOneTwoMonth;
        private DevExpress.XtraLayout.LayoutControlItem lcMysoreMonths;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetAction;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProjectDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lciBudgetProject;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn Project;




    }
}