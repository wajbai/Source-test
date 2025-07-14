namespace ACPP.Modules.Master
{
    partial class frmPurposeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurposeView));
            this.pnlPurposeHeader = new DevExpress.XtraEditors.PanelControl();
            this.ucPurposeToolBar = new Bosco.Utility.Controls.ucToolBar();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblPurpose = new DevExpress.XtraEditors.LabelControl();
            this.pnlPurposefill = new DevExpress.XtraEditors.PanelControl();
            this.gcPurposeView = new DevExpress.XtraGrid.GridControl();
            this.gvPurposeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colContributionHead = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurposes = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPurposeHeader)).BeginInit();
            this.pnlPurposeHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPurposefill)).BeginInit();
            this.pnlPurposefill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPurposeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurposeView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPurposeHeader
            // 
            this.pnlPurposeHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlPurposeHeader.Controls.Add(this.ucPurposeToolBar);
            resources.ApplyResources(this.pnlPurposeHeader, "pnlPurposeHeader");
            this.pnlPurposeHeader.Name = "pnlPurposeHeader";
            // 
            // ucPurposeToolBar
            // 
            this.ucPurposeToolBar.ChangeAddCaption = "&Add";
            this.ucPurposeToolBar.ChangeCaption = "&Edit";
            this.ucPurposeToolBar.ChangeDeleteCaption = "&Delete";
            this.ucPurposeToolBar.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucPurposeToolBar.ChangePostInterestCaption = "P&ost Interest";
            this.ucPurposeToolBar.ChangePrintCaption = "&Print";
            this.ucPurposeToolBar.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucPurposeToolBar.DisableAddButton = true;
            this.ucPurposeToolBar.DisableAMCRenew = true;
            this.ucPurposeToolBar.DisableCloseButton = true;
            this.ucPurposeToolBar.DisableDeleteButton = true;
            this.ucPurposeToolBar.DisableDownloadExcel = true;
            this.ucPurposeToolBar.DisableEditButton = true;
            this.ucPurposeToolBar.DisableMoveTransaction = true;
            this.ucPurposeToolBar.DisableNatureofPayments = true;
            this.ucPurposeToolBar.DisablePostInterest = true;
            this.ucPurposeToolBar.DisablePrintButton = true;
            this.ucPurposeToolBar.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucPurposeToolBar, "ucPurposeToolBar");
            this.ucPurposeToolBar.Name = "ucPurposeToolBar";
            this.ucPurposeToolBar.ShowHTML = true;
            this.ucPurposeToolBar.ShowMMT = true;
            this.ucPurposeToolBar.ShowPDF = true;
            this.ucPurposeToolBar.ShowRTF = true;
            this.ucPurposeToolBar.ShowText = true;
            this.ucPurposeToolBar.ShowXLS = true;
            this.ucPurposeToolBar.ShowXLSX = true;
            this.ucPurposeToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPurposeToolBar.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPurposeToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPurposeToolBar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPurposeToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPurposeToolBar.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucPurposeToolBar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucPurposeToolBar.AddClicked += new System.EventHandler(this.ucPurposeToolBar_AddClicked);
            this.ucPurposeToolBar.EditClicked += new System.EventHandler(this.ucPurposeToolBar_EditClicked);
            this.ucPurposeToolBar.DeleteClicked += new System.EventHandler(this.ucPurposeToolBar_DeleteClicked);
            this.ucPurposeToolBar.PrintClicked += new System.EventHandler(this.ucPurposeToolBar_PrintClicked);
            this.ucPurposeToolBar.CloseClicked += new System.EventHandler(this.ucPurposeToolBar_CloseClicked);
            this.ucPurposeToolBar.RefreshClicked += new System.EventHandler(this.ucPurposeToolBar_RefreshClicked);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.chkShowFilter);
            this.panelControl2.Controls.Add(this.lblRecordCount);
            this.panelControl2.Controls.Add(this.lblPurpose);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblPurpose
            // 
            resources.ApplyResources(this.lblPurpose, "lblPurpose");
            this.lblPurpose.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblPurpose.Appearance.Font")));
            this.lblPurpose.Name = "lblPurpose";
            // 
            // pnlPurposefill
            // 
            this.pnlPurposefill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlPurposefill.Controls.Add(this.gcPurposeView);
            resources.ApplyResources(this.pnlPurposefill, "pnlPurposefill");
            this.pnlPurposefill.Name = "pnlPurposefill";
            // 
            // gcPurposeView
            // 
            resources.ApplyResources(this.gcPurposeView, "gcPurposeView");
            this.gcPurposeView.MainView = this.gvPurposeView;
            this.gcPurposeView.Name = "gcPurposeView";
            this.gcPurposeView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPurposeView});
            this.gcPurposeView.DoubleClick += new System.EventHandler(this.gcPurposeView_DoubleClick);
            // 
            // gvPurposeView
            // 
            this.gvPurposeView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvPurposeView.Appearance.FocusedRow.Font")));
            this.gvPurposeView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPurposeView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvPurposeView.Appearance.HeaderPanel.Font")));
            this.gvPurposeView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPurposeView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvPurposeView.AppearancePrint.HeaderPanel.Font")));
            this.gvPurposeView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvPurposeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colContributionHead,
            this.colCode,
            this.colPurposes});
            this.gvPurposeView.GridControl = this.gcPurposeView;
            this.gvPurposeView.Name = "gvPurposeView";
            this.gvPurposeView.OptionsBehavior.Editable = false;
            this.gvPurposeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPurposeView.OptionsView.ShowGroupPanel = false;
            this.gvPurposeView.RowCountChanged += new System.EventHandler(this.gvPurposeView_RowCountChanged);
            // 
            // colContributionHead
            // 
            this.colContributionHead.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colContributionHead.AppearanceHeader.Font")));
            this.colContributionHead.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colContributionHead, "colContributionHead");
            this.colContributionHead.FieldName = "CONTRIBUTION_ID";
            this.colContributionHead.Name = "colContributionHead";
            this.colContributionHead.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCode
            // 
            this.colCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCode.AppearanceHeader.Font")));
            this.colCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCode, "colCode");
            this.colCode.FieldName = "CODE";
            this.colCode.Name = "colCode";
            this.colCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPurposes
            // 
            this.colPurposes.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPurposes.AppearanceHeader.Font")));
            this.colPurposes.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPurposes, "colPurposes");
            this.colPurposes.FieldName = "FC_PURPOSE";
            this.colPurposes.Name = "colPurposes";
            this.colPurposes.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // frmPurposeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPurposefill);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.pnlPurposeHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPurposeView";
            this.ShowFilterClicked += new System.EventHandler(this.frmPurposeView_ShowFilterClicked);
            this.Activated += new System.EventHandler(this.frmPurposeView_Activated);
            this.Load += new System.EventHandler(this.frmPurposeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlPurposeHeader)).EndInit();
            this.pnlPurposeHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPurposefill)).EndInit();
            this.pnlPurposefill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPurposeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurposeView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlPurposeHeader;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Bosco.Utility.Controls.ucToolBar ucPurposeToolBar;
        private DevExpress.XtraEditors.PanelControl pnlPurposefill;
        private DevExpress.XtraEditors.LabelControl lblPurpose;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.GridControl gcPurposeView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPurposeView;
        private DevExpress.XtraGrid.Columns.GridColumn colContributionHead;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPurposes;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
    }
}