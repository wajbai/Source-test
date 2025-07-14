namespace ACPP.Modules.User_Management
{
    partial class frmUserRoleView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserRoleView));
            this.pnltop = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarUserRoleView = new Bosco.Utility.Controls.ucToolBar();
            this.pnlfill = new DevExpress.XtraEditors.PanelControl();
            this.gcUserRole = new DevExpress.XtraGrid.GridControl();
            this.gvUserRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserRoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblUserRole = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).BeginInit();
            this.pnltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).BeginInit();
            this.pnlfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUserRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).BeginInit();
            this.pnlfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnltop
            // 
            this.pnltop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnltop.Controls.Add(this.ucToolBarUserRoleView);
            resources.ApplyResources(this.pnltop, "pnltop");
            this.pnltop.Name = "pnltop";
            // 
            // ucToolBarUserRoleView
            // 
            this.ucToolBarUserRoleView.ChangeAddCaption = "&Add";
            this.ucToolBarUserRoleView.ChangeCaption = "&Edit";
            this.ucToolBarUserRoleView.ChangeDeleteCaption = "&Delete";
            this.ucToolBarUserRoleView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarUserRoleView.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarUserRoleView.ChangePrintCaption = "&Print";
            this.ucToolBarUserRoleView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarUserRoleView.DisableAddButton = true;
            this.ucToolBarUserRoleView.DisableAMCRenew = true;
            this.ucToolBarUserRoleView.DisableCloseButton = true;
            this.ucToolBarUserRoleView.DisableDeleteButton = true;
            this.ucToolBarUserRoleView.DisableDownloadExcel = true;
            this.ucToolBarUserRoleView.DisableEditButton = true;
            this.ucToolBarUserRoleView.DisableMoveTransaction = true;
            this.ucToolBarUserRoleView.DisableNatureofPayments = true;
            this.ucToolBarUserRoleView.DisablePostInterest = true;
            this.ucToolBarUserRoleView.DisablePrintButton = true;
            this.ucToolBarUserRoleView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarUserRoleView, "ucToolBarUserRoleView");
            this.ucToolBarUserRoleView.Name = "ucToolBarUserRoleView";
            this.ucToolBarUserRoleView.ShowHTML = true;
            this.ucToolBarUserRoleView.ShowMMT = true;
            this.ucToolBarUserRoleView.ShowPDF = true;
            this.ucToolBarUserRoleView.ShowRTF = true;
            this.ucToolBarUserRoleView.ShowText = true;
            this.ucToolBarUserRoleView.ShowXLS = true;
            this.ucToolBarUserRoleView.ShowXLSX = true;
            this.ucToolBarUserRoleView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarUserRoleView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarUserRoleView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserRoleView.AddClicked += new System.EventHandler(this.ucToolBarUserRoleView_AddClicked);
            this.ucToolBarUserRoleView.EditClicked += new System.EventHandler(this.ucToolBarUserRoleView_EditClicked);
            this.ucToolBarUserRoleView.DeleteClicked += new System.EventHandler(this.ucToolBarUserRoleView_DeleteClicked);
            this.ucToolBarUserRoleView.PrintClicked += new System.EventHandler(this.ucToolBarUserRoleView_PrintClicked);
            this.ucToolBarUserRoleView.CloseClicked += new System.EventHandler(this.ucToolBarUserRoleView_CloseClicked);
            // 
            // pnlfill
            // 
            this.pnlfill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfill.Controls.Add(this.gcUserRole);
            resources.ApplyResources(this.pnlfill, "pnlfill");
            this.pnlfill.Name = "pnlfill";
            // 
            // gcUserRole
            // 
            resources.ApplyResources(this.gcUserRole, "gcUserRole");
            this.gcUserRole.MainView = this.gvUserRole;
            this.gcUserRole.Name = "gcUserRole";
            this.gcUserRole.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUserRole});
            // 
            // gvUserRole
            // 
            this.gvUserRole.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvUserRole.Appearance.FocusedRow.Font")));
            this.gvUserRole.Appearance.FocusedRow.Options.UseFont = true;
            this.gvUserRole.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvUserRole.AppearancePrint.HeaderPanel.Font")));
            this.gvUserRole.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvUserRole.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserRoleId,
            this.colUserRoleName});
            this.gvUserRole.GridControl = this.gcUserRole;
            this.gvUserRole.Name = "gvUserRole";
            this.gvUserRole.OptionsBehavior.Editable = false;
            this.gvUserRole.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvUserRole.OptionsView.ShowGroupPanel = false;
            this.gvUserRole.DoubleClick += new System.EventHandler(this.gvUserRole_DoubleClick);
            this.gvUserRole.RowCountChanged += new System.EventHandler(this.gvUserRole_RowCountChanged);
            // 
            // colUserRoleId
            // 
            resources.ApplyResources(this.colUserRoleId, "colUserRoleId");
            this.colUserRoleId.FieldName = "USERROLE_ID";
            this.colUserRoleId.Name = "colUserRoleId";
            // 
            // colUserRoleName
            // 
            this.colUserRoleName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUserRoleName.AppearanceHeader.Font")));
            this.colUserRoleName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUserRoleName, "colUserRoleName");
            this.colUserRoleName.FieldName = "USERROLE";
            this.colUserRoleName.Name = "colUserRoleName";
            this.colUserRoleName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlfooter
            // 
            this.pnlfooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfooter.Controls.Add(this.lblRecordCount);
            this.pnlfooter.Controls.Add(this.lblUserRole);
            this.pnlfooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlfooter, "pnlfooter");
            this.pnlfooter.Name = "pnlfooter";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblUserRole
            // 
            resources.ApplyResources(this.lblUserRole, "lblUserRole");
            this.lblUserRole.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblUserRole.Appearance.Font")));
            this.lblUserRole.Name = "lblUserRole";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // frmUserRoleView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlfill);
            this.Controls.Add(this.pnlfooter);
            this.Controls.Add(this.pnltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUserRoleView";
            this.ShowFilterClicked += new System.EventHandler(this.frmUserRoleView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmUserRoleView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmUserRoleView_Activated);
            this.Load += new System.EventHandler(this.frmUserRoleView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).EndInit();
            this.pnltop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).EndInit();
            this.pnlfill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUserRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnltop;
        private DevExpress.XtraEditors.PanelControl pnlfill;
        private DevExpress.XtraGrid.GridControl gcUserRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUserRole;
        private DevExpress.XtraGrid.Columns.GridColumn colUserRoleId;
        private DevExpress.XtraGrid.Columns.GridColumn colUserRoleName;
        private DevExpress.XtraEditors.PanelControl pnlfooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private Bosco.Utility.Controls.ucToolBar ucToolBarUserRoleView;
        private DevExpress.XtraEditors.LabelControl lblUserRole;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
    }
}