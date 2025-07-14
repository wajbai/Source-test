namespace ACPP.Modules.Master
{
    partial class frmAccountingPeriodView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountingPeriodView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.ucToolBarCostCentre = new Bosco.Utility.Controls.ucToolBar();
            this.pnlGridView = new DevExpress.XtraEditors.PanelControl();
            this.pnlCostCenterDetails = new DevExpress.XtraEditors.PanelControl();
            this.pnlShowFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblCostCentreRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcAccPeriod = new DevExpress.XtraGrid.GridControl();
            this.gvAccPeriod = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colaccPeriodId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYearFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYearTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridView)).BeginInit();
            this.pnlGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenterDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlShowFooter)).BeginInit();
            this.pnlShowFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAccPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucToolBarCostCentre
            // 
            this.ucToolBarCostCentre.ChangeAddCaption = "&Add";
            this.ucToolBarCostCentre.ChangeCaption = "&Edit";
            this.ucToolBarCostCentre.ChangeDeleteCaption = "&Delete";
            this.ucToolBarCostCentre.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarCostCentre.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarCostCentre.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarCostCentre.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarCostCentre.ChangePrintCaption = "&Print";
            this.ucToolBarCostCentre.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarCostCentre.DisableAddButton = true;
            this.ucToolBarCostCentre.DisableAMCRenew = true;
            this.ucToolBarCostCentre.DisableCloseButton = true;
            this.ucToolBarCostCentre.DisableDeleteButton = true;
            this.ucToolBarCostCentre.DisableDownloadExcel = true;
            this.ucToolBarCostCentre.DisableEditButton = true;
            this.ucToolBarCostCentre.DisableMoveTransaction = true;
            this.ucToolBarCostCentre.DisableNatureofPayments = true;
            this.ucToolBarCostCentre.DisablePostInterest = true;
            this.ucToolBarCostCentre.DisablePrintButton = true;
            this.ucToolBarCostCentre.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarCostCentre, "ucToolBarCostCentre");
            this.ucToolBarCostCentre.Name = "ucToolBarCostCentre";
            this.ucToolBarCostCentre.ShowHTML = true;
            this.ucToolBarCostCentre.ShowMMT = true;
            this.ucToolBarCostCentre.ShowPDF = true;
            this.ucToolBarCostCentre.ShowRTF = true;
            this.ucToolBarCostCentre.ShowText = true;
            this.ucToolBarCostCentre.ShowXLS = true;
            this.ucToolBarCostCentre.ShowXLSX = true;
            this.ucToolBarCostCentre.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarCostCentre.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarCostCentre.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.AddClicked += new System.EventHandler(this.ucToolBarCostCentre_AddClicked);
            this.ucToolBarCostCentre.EditClicked += new System.EventHandler(this.ucToolBarCostCentre_EditClicked);
            this.ucToolBarCostCentre.DeleteClicked += new System.EventHandler(this.ucToolBarCostCentre_DeleteClicked);
            this.ucToolBarCostCentre.PrintClicked += new System.EventHandler(this.ucToolBarCostCentre_PrintClicked);
            this.ucToolBarCostCentre.CloseClicked += new System.EventHandler(this.ucToolBarCostCentre_CloseClicked);
            this.ucToolBarCostCentre.RefreshClicked += new System.EventHandler(this.ucToolBarCostCentre_RefreshClicked);
            this.ucToolBarCostCentre.MoveTransaction += new System.EventHandler(this.ucToolBarCostCentre_MoveTransaction);
            // 
            // pnlGridView
            // 
            this.pnlGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlGridView.Controls.Add(this.ucToolBarCostCentre);
            resources.ApplyResources(this.pnlGridView, "pnlGridView");
            this.pnlGridView.Name = "pnlGridView";
            // 
            // pnlCostCenterDetails
            // 
            this.pnlCostCenterDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            resources.ApplyResources(this.pnlCostCenterDetails, "pnlCostCenterDetails");
            this.pnlCostCenterDetails.Name = "pnlCostCenterDetails";
            // 
            // pnlShowFooter
            // 
            this.pnlShowFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlShowFooter.Controls.Add(this.lblCostCentreRecordCount);
            this.pnlShowFooter.Controls.Add(this.lblRecordCount);
            this.pnlShowFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlShowFooter, "pnlShowFooter");
            this.pnlShowFooter.Name = "pnlShowFooter";
            // 
            // lblCostCentreRecordCount
            // 
            resources.ApplyResources(this.lblCostCentreRecordCount, "lblCostCentreRecordCount");
            this.lblCostCentreRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCostCentreRecordCount.Appearance.Font")));
            this.lblCostCentreRecordCount.Name = "lblCostCentreRecordCount";
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
            // gcAccPeriod
            // 
            resources.ApplyResources(this.gcAccPeriod, "gcAccPeriod");
            this.gcAccPeriod.MainView = this.gvAccPeriod;
            this.gcAccPeriod.Name = "gcAccPeriod";
            this.gcAccPeriod.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkCheck});
            this.gcAccPeriod.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAccPeriod});
            this.gcAccPeriod.DoubleClick += new System.EventHandler(this.gcCostCentre_DoubleClick);
            // 
            // gvAccPeriod
            // 
            this.gvAccPeriod.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAccPeriod.Appearance.FocusedRow.Font")));
            this.gvAccPeriod.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAccPeriod.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAccPeriod.Appearance.HeaderPanel.Font")));
            this.gvAccPeriod.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAccPeriod.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAccPeriod.AppearancePrint.HeaderPanel.Font")));
            this.gvAccPeriod.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvAccPeriod.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvAccPeriod.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colaccPeriodId,
            this.colYearFrom,
            this.colYearTo,
            this.colStatus});
            this.gvAccPeriod.GridControl = this.gcAccPeriod;
            this.gvAccPeriod.Name = "gvAccPeriod";
            this.gvAccPeriod.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvAccPeriod.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAccPeriod.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gvAccPeriod.OptionsView.ShowGroupPanel = false;
            this.gvAccPeriod.OptionsView.ShowIndicator = false;
            this.gvAccPeriod.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvAccPeriod_RowStyle);
            this.gvAccPeriod.RowCountChanged += new System.EventHandler(this.gvCostCentre_RowCountChanged);
            // 
            // colCheck
            // 
            this.colCheck.ColumnEdit = this.rchkCheck;
            this.colCheck.FieldName = "FLAG";
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.ShowCaption = false;
            this.colCheck.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            resources.ApplyResources(this.colCheck, "colCheck");
            // 
            // rchkCheck
            // 
            resources.ApplyResources(this.rchkCheck, "rchkCheck");
            this.rchkCheck.Name = "rchkCheck";
            this.rchkCheck.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkCheck.ValueChecked = 1;
            this.rchkCheck.ValueUnchecked = 0;
            this.rchkCheck.CheckedChanged += new System.EventHandler(this.rchkCheck_CheckedChanged);
            // 
            // colaccPeriodId
            // 
            this.colaccPeriodId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colaccPeriodId.AppearanceHeader.Font")));
            this.colaccPeriodId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colaccPeriodId, "colaccPeriodId");
            this.colaccPeriodId.FieldName = "ACC_YEAR_ID";
            this.colaccPeriodId.Name = "colaccPeriodId";
            this.colaccPeriodId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colYearFrom
            // 
            this.colYearFrom.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colYearFrom.AppearanceHeader.Font")));
            this.colYearFrom.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colYearFrom, "colYearFrom");
            this.colYearFrom.FieldName = "YEAR_FROM";
            this.colYearFrom.Name = "colYearFrom";
            this.colYearFrom.OptionsColumn.AllowEdit = false;
            this.colYearFrom.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colYearTo
            // 
            this.colYearTo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colYearTo.AppearanceHeader.Font")));
            this.colYearTo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colYearTo, "colYearTo");
            this.colYearTo.FieldName = "YEAR_TO";
            this.colYearTo.Name = "colYearTo";
            this.colYearTo.OptionsColumn.AllowEdit = false;
            this.colYearTo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStatus.AppearanceHeader.Font")));
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcAccPeriod);
            this.panelControl1.Controls.Add(this.pnlCostCenterDetails);
            this.panelControl1.Controls.Add(this.pnlShowFooter);
            this.panelControl1.Controls.Add(this.pnlGridView);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // frmAccountingPeriodView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAccountingPeriodView";
            this.ShowFilterClicked += new System.EventHandler(this.frmAccountingPeriodView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmAccountingPeriodView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmAccountingPeriodView_Activated);
            this.Load += new System.EventHandler(this.frmAccountingPeriodView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridView)).EndInit();
            this.pnlGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenterDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlShowFooter)).EndInit();
            this.pnlShowFooter.ResumeLayout(false);
            this.pnlShowFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAccPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private Bosco.Utility.Controls.ucToolBar ucToolBarCostCentre;
        private DevExpress.XtraEditors.PanelControl pnlGridView;
        private DevExpress.XtraGrid.GridControl gcAccPeriod;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAccPeriod;
        private DevExpress.XtraGrid.Columns.GridColumn colaccPeriodId;
        private DevExpress.XtraGrid.Columns.GridColumn colYearFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colYearTo;
        private DevExpress.XtraEditors.PanelControl pnlShowFooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblCostCentreRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.PanelControl pnlCostCenterDetails;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkCheck;
	}
}