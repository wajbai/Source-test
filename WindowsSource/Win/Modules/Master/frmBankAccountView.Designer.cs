namespace ACPP.Modules.Master
{
    partial class frmBankAccountView 
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBankAccountView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.pnlBankToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarBankView = new Bosco.Utility.Controls.ucToolBar();
            this.pnlBankView = new DevExpress.XtraEditors.PanelControl();
            this.pnlGridControl = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcLedgerView = new DevExpress.XtraGrid.GridControl();
            this.gvLedgerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBankAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBank = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateOpened = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCloseDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoOfProjects = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLEdgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBankFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblBankRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.applicationSchema1 = new Bosco.DAO.Schema.ApplicationSchema();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpExchangeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankToolBar)).BeginInit();
            this.pnlBankToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankView)).BeginInit();
            this.pnlBankView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridControl)).BeginInit();
            this.pnlGridControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankFooter)).BeginInit();
            this.pnlBankFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationSchema1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBankToolBar
            // 
            this.pnlBankToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankToolBar.Controls.Add(this.ucToolBarBankView);
            resources.ApplyResources(this.pnlBankToolBar, "pnlBankToolBar");
            this.pnlBankToolBar.Name = "pnlBankToolBar";
            // 
            // ucToolBarBankView
            // 
            this.ucToolBarBankView.ChangeAddCaption = "&Add";
            this.ucToolBarBankView.ChangeCaption = "&Edit";
            this.ucToolBarBankView.ChangeDeleteCaption = "&Delete";
            this.ucToolBarBankView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarBankView.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarBankView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarBankView.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBarBankView.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucToolBarBankView.ChangePrintCaption = "&Print";
            this.ucToolBarBankView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarBankView.DisableAddButton = true;
            this.ucToolBarBankView.DisableAMCRenew = true;
            this.ucToolBarBankView.DisableCloseButton = true;
            this.ucToolBarBankView.DisableDeleteButton = true;
            this.ucToolBarBankView.DisableDownloadExcel = true;
            this.ucToolBarBankView.DisableEditButton = true;
            this.ucToolBarBankView.DisableInsertVoucher = true;
            this.ucToolBarBankView.DisableMoveTransaction = true;
            this.ucToolBarBankView.DisableNatureofPayments = true;
            this.ucToolBarBankView.DisablePostInterest = true;
            this.ucToolBarBankView.DisablePrintButton = true;
            this.ucToolBarBankView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarBankView, "ucToolBarBankView");
            this.ucToolBarBankView.Name = "ucToolBarBankView";
            this.ucToolBarBankView.ShowHTML = true;
            this.ucToolBarBankView.ShowMMT = true;
            this.ucToolBarBankView.ShowPDF = true;
            this.ucToolBarBankView.ShowRTF = true;
            this.ucToolBarBankView.ShowText = true;
            this.ucToolBarBankView.ShowXLS = true;
            this.ucToolBarBankView.ShowXLSX = true;
            this.ucToolBarBankView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.AddClicked += new System.EventHandler(this.ucToolBarBankView_AddClicked);
            this.ucToolBarBankView.EditClicked += new System.EventHandler(this.ucToolBarBankView_EditClicked);
            this.ucToolBarBankView.DeleteClicked += new System.EventHandler(this.ucToolBarBankView_DeleteClicked);
            this.ucToolBarBankView.PrintClicked += new System.EventHandler(this.ucToolBarBankView_PrintClicked);
            this.ucToolBarBankView.CloseClicked += new System.EventHandler(this.ucToolBarBankView_CloseClicked);
            this.ucToolBarBankView.RefreshClicked += new System.EventHandler(this.ucToolBarBankView_RefreshClicked);
            // 
            // pnlBankView
            // 
            this.pnlBankView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankView.Controls.Add(this.pnlGridControl);
            resources.ApplyResources(this.pnlBankView, "pnlBankView");
            this.pnlBankView.Name = "pnlBankView";
            // 
            // pnlGridControl
            // 
            this.pnlGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlGridControl.Controls.Add(this.panelControl2);
            this.pnlGridControl.Controls.Add(this.pnlBankFooter);
            this.pnlGridControl.Controls.Add(this.pnlBankToolBar);
            resources.ApplyResources(this.pnlGridControl, "pnlGridControl");
            this.pnlGridControl.Name = "pnlGridControl";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gcLedgerView);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // gcLedgerView
            // 
            resources.ApplyResources(this.gcLedgerView, "gcLedgerView");
            this.gcLedgerView.MainView = this.gvLedgerView;
            this.gcLedgerView.Name = "gcLedgerView";
            this.gcLedgerView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedgerView});
            // 
            // gvLedgerView
            // 
            this.gvLedgerView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLedgerView.Appearance.FocusedRow.Font")));
            this.gvLedgerView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLedgerView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedgerView.Appearance.HeaderPanel.Font")));
            this.gvLedgerView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLedgerView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedgerView.AppearancePrint.HeaderPanel.Font")));
            this.gvLedgerView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvLedgerView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvLedgerView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBankAccountId,
            this.colAccountCode,
            this.colAccountNumber,
            this.colBank,
            this.colBranchName,
            this.colCurrencyName,
            this.colOpExchangeRate,
            this.colDateOpened,
            this.colCloseDate,
            this.colProject,
            this.colNoOfProjects,
            this.colLEdgerId});
            this.gvLedgerView.CustomizationFormBounds = new System.Drawing.Rectangle(677, 363, 216, 178);
            this.gvLedgerView.GridControl = this.gcLedgerView;
            this.gvLedgerView.Name = "gvLedgerView";
            this.gvLedgerView.OptionsBehavior.Editable = false;
            this.gvLedgerView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLedgerView.OptionsView.ShowGroupPanel = false;
            this.gvLedgerView.DoubleClick += new System.EventHandler(this.gvLedgerView_DoubleClick);
            this.gvLedgerView.RowCountChanged += new System.EventHandler(this.gvLedgerView_RowCountChanged);
            // 
            // colBankAccountId
            // 
            this.colBankAccountId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBankAccountId.AppearanceHeader.Font")));
            this.colBankAccountId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBankAccountId, "colBankAccountId");
            this.colBankAccountId.FieldName = "BANK_ACCOUNT_ID";
            this.colBankAccountId.Name = "colBankAccountId";
            // 
            // colAccountCode
            // 
            this.colAccountCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAccountCode.AppearanceHeader.Font")));
            this.colAccountCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAccountCode, "colAccountCode");
            this.colAccountCode.FieldName = "ACCOUNT_CODE";
            this.colAccountCode.Name = "colAccountCode";
            this.colAccountCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAccountNumber
            // 
            this.colAccountNumber.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAccountNumber.AppearanceHeader.Font")));
            this.colAccountNumber.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAccountNumber, "colAccountNumber");
            this.colAccountNumber.FieldName = "ACCOUNT_NUMBER";
            this.colAccountNumber.Name = "colAccountNumber";
            this.colAccountNumber.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBank
            // 
            this.colBank.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBank.AppearanceHeader.Font")));
            this.colBank.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBank, "colBank");
            this.colBank.FieldName = "BANK";
            this.colBank.Name = "colBank";
            this.colBank.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBranchName
            // 
            this.colBranchName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBranchName.AppearanceHeader.Font")));
            this.colBranchName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBranchName, "colBranchName");
            this.colBranchName.FieldName = "BRANCH";
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDateOpened
            // 
            this.colDateOpened.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDateOpened.AppearanceHeader.Font")));
            this.colDateOpened.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDateOpened, "colDateOpened");
            this.colDateOpened.FieldName = "DATE_OPENED";
            this.colDateOpened.Name = "colDateOpened";
            this.colDateOpened.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCloseDate
            // 
            this.colCloseDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCloseDate.AppearanceHeader.Font")));
            this.colCloseDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCloseDate, "colCloseDate");
            this.colCloseDate.FieldName = "DATE_CLOSED";
            this.colCloseDate.Name = "colCloseDate";
            this.colCloseDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colNoOfProjects
            // 
            this.colNoOfProjects.AppearanceHeader.Options.UseTextOptions = true;
            this.colNoOfProjects.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colNoOfProjects, "colNoOfProjects");
            this.colNoOfProjects.FieldName = "NO_PROJECTS";
            this.colNoOfProjects.Name = "colNoOfProjects";
            this.colNoOfProjects.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLEdgerId
            // 
            resources.ApplyResources(this.colLEdgerId, "colLEdgerId");
            this.colLEdgerId.FieldName = "LEDGER_ID";
            this.colLEdgerId.Name = "colLEdgerId";
            // 
            // pnlBankFooter
            // 
            this.pnlBankFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankFooter.Controls.Add(this.lblBankRecordCount);
            this.pnlBankFooter.Controls.Add(this.lblRecordCount);
            this.pnlBankFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlBankFooter, "pnlBankFooter");
            this.pnlBankFooter.Name = "pnlBankFooter";
            // 
            // lblBankRecordCount
            // 
            resources.ApplyResources(this.lblBankRecordCount, "lblBankRecordCount");
            this.lblBankRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblBankRecordCount.Appearance.Font")));
            this.lblBankRecordCount.Name = "lblBankRecordCount";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // applicationSchema1
            // 
            this.applicationSchema1.DataSetName = "ApplicationSchema";
            this.applicationSchema1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colCurrencyName
            // 
            resources.ApplyResources(this.colCurrencyName, "colCurrencyName");
            this.colCurrencyName.FieldName = "CURRENCY_NAME";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colOpExchangeRate
            // 
            resources.ApplyResources(this.colOpExchangeRate, "colOpExchangeRate");
            this.colOpExchangeRate.FieldName = "OP_EXCHANGE_RATE";
            this.colOpExchangeRate.Name = "colOpExchangeRate";
            this.colOpExchangeRate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // frmBankAccountView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBankView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBankAccountView";
            this.ShowFilterClicked += new System.EventHandler(this.frmBankAccountView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmBankAccountView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmBankAccountView_Activated);
            this.Load += new System.EventHandler(this.frmLedgerView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankToolBar)).EndInit();
            this.pnlBankToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankView)).EndInit();
            this.pnlBankView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridControl)).EndInit();
            this.pnlGridControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedgerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankFooter)).EndInit();
            this.pnlBankFooter.ResumeLayout(false);
            this.pnlBankFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationSchema1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBankToolBar;
        private Bosco.Utility.Controls.ucToolBar ucToolBarBankView;
        private DevExpress.XtraEditors.PanelControl pnlBankView;
        private DevExpress.XtraGrid.GridControl gcLedgerView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedgerView;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colBank;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private Bosco.DAO.Schema.ApplicationSchema applicationSchema1;
        private DevExpress.XtraEditors.PanelControl pnlBankFooter;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblBankRecordCount;
        private DevExpress.XtraEditors.PanelControl pnlGridControl;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colDateOpened;
        private DevExpress.XtraGrid.Columns.GridColumn colCloseDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLEdgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colNoOfProjects;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colOpExchangeRate;

    }
}