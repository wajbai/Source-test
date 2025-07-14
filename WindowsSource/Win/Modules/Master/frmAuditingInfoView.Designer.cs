namespace ACPP.Modules.Master
{
    partial class frmAuditingInfoView
    {
        private DevExpress.XtraEditors.PanelControl pnlAuditingtop;

        #region Windows Form Designer generated code


        #endregion

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditingInfoView));
            this.pnlAuditingtop = new DevExpress.XtraEditors.PanelControl();
            this.ucToolbarAuditingInfo = new Bosco.Utility.Controls.ucToolBar();
            this.pnlAuditingfooter = new DevExpress.XtraEditors.PanelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblAuditingInfo = new DevExpress.XtraEditors.LabelControl();
            this.pnlAuditingViewFill = new DevExpress.XtraEditors.PanelControl();
            this.gcAuditingInfoView = new DevExpress.XtraGrid.GridControl();
            this.gvAuditingInfoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAuditInfoId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBegin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditingtop)).BeginInit();
            this.pnlAuditingtop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditingfooter)).BeginInit();
            this.pnlAuditingfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditingViewFill)).BeginInit();
            this.pnlAuditingViewFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditingInfoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditingInfoView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlAuditingtop
            // 
            this.pnlAuditingtop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlAuditingtop.Controls.Add(this.ucToolbarAuditingInfo);
            resources.ApplyResources(this.pnlAuditingtop, "pnlAuditingtop");
            this.pnlAuditingtop.Name = "pnlAuditingtop";
            // 
            // ucToolbarAuditingInfo
            // 
            this.ucToolbarAuditingInfo.ChangeAddCaption = "&Add";
            this.ucToolbarAuditingInfo.ChangeCaption = "&Edit";
            this.ucToolbarAuditingInfo.ChangeDeleteCaption = "&Delete";
            this.ucToolbarAuditingInfo.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolbarAuditingInfo.ChangePrintCaption = "&Print";
            this.ucToolbarAuditingInfo.DisableAddButton = true;
            this.ucToolbarAuditingInfo.DisableCloseButton = true;
            this.ucToolbarAuditingInfo.DisableDeleteButton = true;
            this.ucToolbarAuditingInfo.DisableDownloadExcel = true;
            this.ucToolbarAuditingInfo.DisableEditButton = true;
            this.ucToolbarAuditingInfo.DisableMoveTransaction = true;
            this.ucToolbarAuditingInfo.DisableNatureofPayments = true;
            this.ucToolbarAuditingInfo.DisablePrintButton = true;
            this.ucToolbarAuditingInfo.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolbarAuditingInfo, "ucToolbarAuditingInfo");
            this.ucToolbarAuditingInfo.Name = "ucToolbarAuditingInfo";
            this.ucToolbarAuditingInfo.ShowHTML = true;
            this.ucToolbarAuditingInfo.ShowMMT = true;
            this.ucToolbarAuditingInfo.ShowPDF = true;
            this.ucToolbarAuditingInfo.ShowRTF = true;
            this.ucToolbarAuditingInfo.ShowText = true;
            this.ucToolbarAuditingInfo.ShowXLS = true;
            this.ucToolbarAuditingInfo.ShowXLSX = true;
            this.ucToolbarAuditingInfo.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolbarAuditingInfo.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolbarAuditingInfo.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolbarAuditingInfo.AddClicked += new System.EventHandler(this.ucToolbarAuditingInfo_AddClicked);
            this.ucToolbarAuditingInfo.EditClicked += new System.EventHandler(this.ucToolbarAuditingInfo_EditClicked);
            this.ucToolbarAuditingInfo.DeleteClicked += new System.EventHandler(this.ucToolbarAuditingInfo_DeleteClicked);
            this.ucToolbarAuditingInfo.PrintClicked += new System.EventHandler(this.ucToolbarAuditingInfo_PrintClicked);
            this.ucToolbarAuditingInfo.CloseClicked += new System.EventHandler(this.ucToolbarAuditingInfo_CloseClicked);
            this.ucToolbarAuditingInfo.RefreshClicked += new System.EventHandler(this.ucToolbarAuditingInfo_RefreshClicked);
            // 
            // pnlAuditingfooter
            // 
            this.pnlAuditingfooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlAuditingfooter.Controls.Add(this.chkShowFilter);
            this.pnlAuditingfooter.Controls.Add(this.lblRecordCount);
            this.pnlAuditingfooter.Controls.Add(this.lblAuditingInfo);
            resources.ApplyResources(this.pnlAuditingfooter, "pnlAuditingfooter");
            this.pnlAuditingfooter.Name = "pnlAuditingfooter";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged_1);
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblAuditingInfo
            // 
            resources.ApplyResources(this.lblAuditingInfo, "lblAuditingInfo");
            this.lblAuditingInfo.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblAuditingInfo.Appearance.Font")));
            this.lblAuditingInfo.Name = "lblAuditingInfo";
            // 
            // pnlAuditingViewFill
            // 
            this.pnlAuditingViewFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlAuditingViewFill.Controls.Add(this.gcAuditingInfoView);
            resources.ApplyResources(this.pnlAuditingViewFill, "pnlAuditingViewFill");
            this.pnlAuditingViewFill.Name = "pnlAuditingViewFill";
            // 
            // gcAuditingInfoView
            // 
            resources.ApplyResources(this.gcAuditingInfoView, "gcAuditingInfoView");
            this.gcAuditingInfoView.MainView = this.gvAuditingInfoView;
            this.gcAuditingInfoView.Name = "gcAuditingInfoView";
            this.gcAuditingInfoView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAuditingInfoView});
            // 
            // gvAuditingInfoView
            // 
            this.gvAuditingInfoView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditingInfoView.Appearance.FocusedRow.Font")));
            this.gvAuditingInfoView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAuditingInfoView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditingInfoView.Appearance.HeaderPanel.Font")));
            this.gvAuditingInfoView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAuditingInfoView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditingInfoView.AppearancePrint.HeaderPanel.Font")));
            this.gvAuditingInfoView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvAuditingInfoView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAuditInfoId,
            this.colProject,
            this.colBegin,
            this.colEnd,
            this.colAuditedOn,
            this.colAuditType,
            this.colAuditor,
            this.colNotes});
            this.gvAuditingInfoView.GridControl = this.gcAuditingInfoView;
            this.gvAuditingInfoView.Name = "gvAuditingInfoView";
            this.gvAuditingInfoView.OptionsBehavior.Editable = false;
            this.gvAuditingInfoView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAuditingInfoView.OptionsView.ShowGroupPanel = false;
            this.gvAuditingInfoView.DoubleClick += new System.EventHandler(this.gvAuditingInfoView_DoubleClick);
            this.gvAuditingInfoView.RowCountChanged += new System.EventHandler(this.gvAuditingInfoView_RowCountChanged);
            // 
            // colAuditInfoId
            // 
            this.colAuditInfoId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAuditInfoId.AppearanceHeader.Font")));
            this.colAuditInfoId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAuditInfoId, "colAuditInfoId");
            this.colAuditInfoId.FieldName = "AUDIT_INFO_ID";
            this.colAuditInfoId.Name = "colAuditInfoId";
            // 
            // colProject
            // 
            this.colProject.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProject.AppearanceHeader.Font")));
            this.colProject.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBegin
            // 
            this.colBegin.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBegin.AppearanceHeader.Font")));
            this.colBegin.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBegin, "colBegin");
            this.colBegin.FieldName = "AUDIT_BEGIN";
            this.colBegin.Name = "colBegin";
            this.colBegin.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colEnd
            // 
            this.colEnd.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colEnd.AppearanceHeader.Font")));
            this.colEnd.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colEnd, "colEnd");
            this.colEnd.FieldName = "AUDIT_END";
            this.colEnd.Name = "colEnd";
            this.colEnd.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAuditedOn
            // 
            this.colAuditedOn.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAuditedOn.AppearanceHeader.Font")));
            this.colAuditedOn.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAuditedOn, "colAuditedOn");
            this.colAuditedOn.FieldName = "AUDITED_ON";
            this.colAuditedOn.Name = "colAuditedOn";
            this.colAuditedOn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAuditType
            // 
            this.colAuditType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAuditType.AppearanceHeader.Font")));
            this.colAuditType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAuditType, "colAuditType");
            this.colAuditType.FieldName = "AUDIT_TYPE";
            this.colAuditType.Name = "colAuditType";
            this.colAuditType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAuditor
            // 
            this.colAuditor.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAuditor.AppearanceHeader.Font")));
            this.colAuditor.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAuditor, "colAuditor");
            this.colAuditor.FieldName = "NAME";
            this.colAuditor.Name = "colAuditor";
            this.colAuditor.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
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
            // frmAuditingInfoView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlAuditingViewFill);
            this.Controls.Add(this.pnlAuditingfooter);
            this.Controls.Add(this.pnlAuditingtop);
            this.Name = "frmAuditingInfoView";
            this.ShowFilterClicked += new System.EventHandler(this.frmAuditingInfoView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmAuditingInfoView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmAuditingInfoView_Activated);
            this.Load += new System.EventHandler(this.frmAuditingInfoView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditingtop)).EndInit();
            this.pnlAuditingtop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditingfooter)).EndInit();
            this.pnlAuditingfooter.ResumeLayout(false);
            this.pnlAuditingfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditingViewFill)).EndInit();
            this.pnlAuditingViewFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditingInfoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditingInfoView)).EndInit();
            this.ResumeLayout(false);

        }

        private Bosco.Utility.Controls.ucToolBar ucToolbarAuditingInfo;
        private DevExpress.XtraEditors.PanelControl pnlAuditingfooter;
        private DevExpress.XtraEditors.PanelControl pnlAuditingViewFill;
        private DevExpress.XtraGrid.GridControl gcAuditingInfoView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAuditingInfoView;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditInfoId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colBegin;
        private DevExpress.XtraGrid.Columns.GridColumn colEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraEditors.LabelControl lblAuditingInfo;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditType;
        private DevExpress.XtraGrid.Columns.GridColumn colNotes;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditedOn;
    }
}