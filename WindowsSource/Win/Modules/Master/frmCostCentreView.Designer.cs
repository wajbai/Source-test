namespace ACPP.Modules.Master
{
	partial class frmCostCentreView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCostCentreView));
            this.ucToolBarCostCentre = new Bosco.Utility.Controls.ucToolBar();
            this.pnlGridView = new DevExpress.XtraEditors.PanelControl();
            this.pnlCostCenterDetails = new DevExpress.XtraEditors.PanelControl();
            this.pnlShowFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblCostCentreRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcCostCentre = new DevExpress.XtraGrid.GridControl();
            this.gvCostCentre = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCostCentreID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAbbrevation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCentreName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCentreCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridView)).BeginInit();
            this.pnlGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenterDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlShowFooter)).BeginInit();
            this.pnlShowFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucToolBarCostCentre
            // 
            this.ucToolBarCostCentre.ChangeAddCaption = "&Add";
            this.ucToolBarCostCentre.ChangeCaption = "&Edit";
            this.ucToolBarCostCentre.ChangeDeleteCaption = "&Delete";
            this.ucToolBarCostCentre.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarCostCentre.ChangePrintCaption = "&Print";
            this.ucToolBarCostCentre.DisableAddButton = true;
            this.ucToolBarCostCentre.DisableCloseButton = true;
            this.ucToolBarCostCentre.DisableDeleteButton = false;
            this.ucToolBarCostCentre.DisableDownloadExcel = true;
            this.ucToolBarCostCentre.DisableEditButton = false;
            this.ucToolBarCostCentre.DisableMoveTransaction = true;
            this.ucToolBarCostCentre.DisableNatureofPayments = true;
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
            this.ucToolBarCostCentre.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarCostCentre.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentre.AddClicked += new System.EventHandler(this.ucToolBarCostCentre_AddClicked);
            this.ucToolBarCostCentre.EditClicked += new System.EventHandler(this.ucToolBarCostCentre_EditClicked);
            this.ucToolBarCostCentre.DeleteClicked += new System.EventHandler(this.ucToolBarCostCentre_DeleteClicked);
            this.ucToolBarCostCentre.PrintClicked += new System.EventHandler(this.ucToolBarCostCentre_PrintClicked);
            this.ucToolBarCostCentre.CloseClicked += new System.EventHandler(this.ucToolBarCostCentre_CloseClicked);
            this.ucToolBarCostCentre.RefreshClicked += new System.EventHandler(this.ucToolBarCostCentre_RefreshClicked);
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
            // gcCostCentre
            // 
            resources.ApplyResources(this.gcCostCentre, "gcCostCentre");
            this.gcCostCentre.MainView = this.gvCostCentre;
            this.gcCostCentre.Name = "gcCostCentre";
            this.gcCostCentre.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCostCentre});
            this.gcCostCentre.DoubleClick += new System.EventHandler(this.gcCostCentre_DoubleClick);
            // 
            // gvCostCentre
            // 
            this.gvCostCentre.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCentre.Appearance.FocusedRow.Font")));
            this.gvCostCentre.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCostCentre.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCentre.Appearance.HeaderPanel.Font")));
            this.gvCostCentre.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvCostCentre.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCentre.AppearancePrint.HeaderPanel.Font")));
            this.gvCostCentre.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvCostCentre.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvCostCentre.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCostCentreID,
            this.colAbbrevation,
            this.colCostCentreName,
            this.colCostCentreCategory,
            this.colNotes});
            this.gvCostCentre.GridControl = this.gcCostCentre;
            this.gvCostCentre.Name = "gvCostCentre";
            this.gvCostCentre.OptionsBehavior.Editable = false;
            this.gvCostCentre.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvCostCentre.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCostCentre.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gvCostCentre.OptionsView.ShowGroupPanel = false;
            this.gvCostCentre.RowCountChanged += new System.EventHandler(this.gvCostCentre_RowCountChanged);
            // 
            // colCostCentreID
            // 
            this.colCostCentreID.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCostCentreID.AppearanceHeader.Font")));
            this.colCostCentreID.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCostCentreID, "colCostCentreID");
            this.colCostCentreID.FieldName = "COST_CENTRE_ID";
            this.colCostCentreID.Name = "colCostCentreID";
            this.colCostCentreID.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAbbrevation
            // 
            this.colAbbrevation.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAbbrevation.AppearanceHeader.Font")));
            this.colAbbrevation.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAbbrevation, "colAbbrevation");
            this.colAbbrevation.FieldName = "ABBREVATION";
            this.colAbbrevation.Name = "colAbbrevation";
            this.colAbbrevation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCostCentreName
            // 
            this.colCostCentreName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCostCentreName.AppearanceHeader.Font")));
            this.colCostCentreName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCostCentreName, "colCostCentreName");
            this.colCostCentreName.FieldName = "COST_CENTRE_NAME";
            this.colCostCentreName.Name = "colCostCentreName";
            this.colCostCentreName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCostCentreCategory
            // 
            resources.ApplyResources(this.colCostCentreCategory, "colCostCentreCategory");
            this.colCostCentreCategory.FieldName = "COST_CENTRE_CATEGORY_NAME";
            this.colCostCentreCategory.Name = "colCostCentreCategory";
            // 
            // colNotes
            // 
            this.colNotes.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colNotes.AppearanceHeader.Font")));
            this.colNotes.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colNotes, "colNotes");
            this.colNotes.FieldName = "NOTES";
            this.colNotes.Name = "colNotes";
            this.colNotes.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcCostCentre);
            this.panelControl1.Controls.Add(this.pnlCostCenterDetails);
            this.panelControl1.Controls.Add(this.pnlShowFooter);
            this.panelControl1.Controls.Add(this.pnlGridView);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // frmCostCentreView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCostCentreView";
            this.ShowFilterClicked += new System.EventHandler(this.frmCostCentreView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmCostCentreView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmCostCentreView_Activated);
            this.Load += new System.EventHandler(this.frmCostCentreView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridView)).EndInit();
            this.pnlGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenterDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlShowFooter)).EndInit();
            this.pnlShowFooter.ResumeLayout(false);
            this.pnlShowFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private Bosco.Utility.Controls.ucToolBar ucToolBarCostCentre;
        private DevExpress.XtraEditors.PanelControl pnlGridView;
        private DevExpress.XtraGrid.GridControl gcCostCentre;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCostCentre;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreID;
        private DevExpress.XtraGrid.Columns.GridColumn colAbbrevation;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreName;
        private DevExpress.XtraEditors.PanelControl pnlShowFooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblCostCentreRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.PanelControl pnlCostCenterDetails;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreCategory;
	}
}