namespace ACPP.Modules.User_Management
{
    partial class frmUserView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserView));
            this.pnlUserMenu = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarUserMenu = new Bosco.Utility.Controls.ucToolBar();
            this.pnlUserMenuBottom = new DevExpress.XtraEditors.PanelControl();
            this.lblUserRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pnlUserFill = new DevExpress.XtraEditors.PanelControl();
            this.gcUserView = new DevExpress.XtraGrid.GridControl();
            this.gvUserView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmailAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserPhoto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryPeUserPhoto = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserMenu)).BeginInit();
            this.pnlUserMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserMenuBottom)).BeginInit();
            this.pnlUserMenuBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserFill)).BeginInit();
            this.pnlUserFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUserView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryPeUserPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUserMenu
            // 
            this.pnlUserMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlUserMenu.Controls.Add(this.ucToolBarUserMenu);
            resources.ApplyResources(this.pnlUserMenu, "pnlUserMenu");
            this.pnlUserMenu.Name = "pnlUserMenu";
            // 
            // ucToolBarUserMenu
            // 
            this.ucToolBarUserMenu.ChangeAddCaption = "&Add";
            this.ucToolBarUserMenu.ChangeCaption = "&Edit";
            this.ucToolBarUserMenu.ChangeDeleteCaption = "&Delete";
            this.ucToolBarUserMenu.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarUserMenu.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarUserMenu.ChangePrintCaption = "&Print";
            this.ucToolBarUserMenu.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarUserMenu.DisableAddButton = true;
            this.ucToolBarUserMenu.DisableAMCRenew = true;
            this.ucToolBarUserMenu.DisableCloseButton = true;
            this.ucToolBarUserMenu.DisableDeleteButton = true;
            this.ucToolBarUserMenu.DisableDownloadExcel = true;
            this.ucToolBarUserMenu.DisableEditButton = true;
            this.ucToolBarUserMenu.DisableMoveTransaction = true;
            this.ucToolBarUserMenu.DisableNatureofPayments = true;
            this.ucToolBarUserMenu.DisablePostInterest = true;
            this.ucToolBarUserMenu.DisablePrintButton = true;
            this.ucToolBarUserMenu.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarUserMenu, "ucToolBarUserMenu");
            this.ucToolBarUserMenu.Name = "ucToolBarUserMenu";
            this.ucToolBarUserMenu.ShowHTML = true;
            this.ucToolBarUserMenu.ShowMMT = true;
            this.ucToolBarUserMenu.ShowPDF = true;
            this.ucToolBarUserMenu.ShowRTF = true;
            this.ucToolBarUserMenu.ShowText = true;
            this.ucToolBarUserMenu.ShowXLS = true;
            this.ucToolBarUserMenu.ShowXLSX = true;
            this.ucToolBarUserMenu.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarUserMenu.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarUserMenu.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarUserMenu.AddClicked += new System.EventHandler(this.ucToolBarUserMenu_AddClicked);
            this.ucToolBarUserMenu.EditClicked += new System.EventHandler(this.ucToolBarUserMenu_EditClicked);
            this.ucToolBarUserMenu.DeleteClicked += new System.EventHandler(this.ucToolBarUserMenu_DeleteClicked);
            this.ucToolBarUserMenu.PrintClicked += new System.EventHandler(this.ucToolBarUserMenu_PrintClicked);
            this.ucToolBarUserMenu.CloseClicked += new System.EventHandler(this.ucToolBarUserMenu_CloseClicked);
            // 
            // pnlUserMenuBottom
            // 
            this.pnlUserMenuBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlUserMenuBottom.Controls.Add(this.lblUserRecordCount);
            this.pnlUserMenuBottom.Controls.Add(this.lblRecordCount);
            this.pnlUserMenuBottom.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlUserMenuBottom, "pnlUserMenuBottom");
            this.pnlUserMenuBottom.Name = "pnlUserMenuBottom";
            // 
            // lblUserRecordCount
            // 
            resources.ApplyResources(this.lblUserRecordCount, "lblUserRecordCount");
            this.lblUserRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblUserRecordCount.Appearance.Font")));
            this.lblUserRecordCount.Name = "lblUserRecordCount";
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
            // pnlUserFill
            // 
            this.pnlUserFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlUserFill.Controls.Add(this.gcUserView);
            resources.ApplyResources(this.pnlUserFill, "pnlUserFill");
            this.pnlUserFill.Name = "pnlUserFill";
            // 
            // gcUserView
            // 
            resources.ApplyResources(this.gcUserView, "gcUserView");
            this.gcUserView.MainView = this.gvUserView;
            this.gcUserView.Name = "gcUserView";
            this.gcUserView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryPeUserPhoto});
            this.gcUserView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUserView});
            this.gcUserView.DoubleClick += new System.EventHandler(this.gcUserView_DoubleClick);
            // 
            // gvUserView
            // 
            this.gvUserView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvUserView.Appearance.FocusedRow.Font")));
            this.gvUserView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvUserView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvUserView.Appearance.HeaderPanel.Font")));
            this.gvUserView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvUserView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvUserView.AppearancePrint.HeaderPanel.Font")));
            this.gvUserView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvUserView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvUserView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserId,
            this.colUserName,
            this.colFirstName,
            this.colLastName,
            this.colUserRole,
            this.colAddress,
            this.colContactNumber,
            this.colEmailAddress,
            this.colUserPhoto});
            this.gvUserView.GridControl = this.gcUserView;
            this.gvUserView.Name = "gvUserView";
            this.gvUserView.OptionsBehavior.Editable = false;
            this.gvUserView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvUserView.OptionsView.ShowGroupPanel = false;
            this.gvUserView.RowCountChanged += new System.EventHandler(this.gvUserView_RowCountChanged);
            // 
            // colUserId
            // 
            this.colUserId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUserId.AppearanceHeader.Font")));
            this.colUserId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUserId, "colUserId");
            this.colUserId.FieldName = "USER_ID";
            this.colUserId.Name = "colUserId";
            // 
            // colUserName
            // 
            this.colUserName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUserName.AppearanceHeader.Font")));
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUserName, "colUserName");
            this.colUserName.FieldName = "USER_NAME";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colFirstName
            // 
            resources.ApplyResources(this.colFirstName, "colFirstName");
            this.colFirstName.FieldName = "FIRSTNAME";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLastName
            // 
            resources.ApplyResources(this.colLastName, "colLastName");
            this.colLastName.FieldName = "LASTNAME";
            this.colLastName.Name = "colLastName";
            this.colLastName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colUserRole
            // 
            this.colUserRole.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUserRole.AppearanceHeader.Font")));
            this.colUserRole.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUserRole, "colUserRole");
            this.colUserRole.FieldName = "USERROLE";
            this.colUserRole.Name = "colUserRole";
            this.colUserRole.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAddress.AppearanceHeader.Font")));
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAddress, "colAddress");
            this.colAddress.FieldName = "ADDRESS";
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colContactNumber
            // 
            this.colContactNumber.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colContactNumber.AppearanceHeader.Font")));
            this.colContactNumber.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colContactNumber, "colContactNumber");
            this.colContactNumber.FieldName = "CONTACT_NO";
            this.colContactNumber.Name = "colContactNumber";
            this.colContactNumber.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colEmailAddress
            // 
            this.colEmailAddress.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colEmailAddress.AppearanceHeader.Font")));
            this.colEmailAddress.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colEmailAddress, "colEmailAddress");
            this.colEmailAddress.FieldName = "EMAIL_ID";
            this.colEmailAddress.Name = "colEmailAddress";
            this.colEmailAddress.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colUserPhoto
            // 
            this.colUserPhoto.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colUserPhoto.AppearanceHeader.Font")));
            this.colUserPhoto.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colUserPhoto, "colUserPhoto");
            this.colUserPhoto.ColumnEdit = this.repositoryPeUserPhoto;
            this.colUserPhoto.FieldName = "USER_PHOTO";
            this.colUserPhoto.Name = "colUserPhoto";
            this.colUserPhoto.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // repositoryPeUserPhoto
            // 
            this.repositoryPeUserPhoto.Name = "repositoryPeUserPhoto";
            this.repositoryPeUserPhoto.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // frmUserView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlUserFill);
            this.Controls.Add(this.pnlUserMenuBottom);
            this.Controls.Add(this.pnlUserMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmUserView";
            this.ShowFilterClicked += new System.EventHandler(this.frmUserView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmUserView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmUserView_Activated);
            this.Load += new System.EventHandler(this.frmUserView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserMenu)).EndInit();
            this.pnlUserMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserMenuBottom)).EndInit();
            this.pnlUserMenuBottom.ResumeLayout(false);
            this.pnlUserMenuBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUserFill)).EndInit();
            this.pnlUserFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUserView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryPeUserPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlUserMenu;
        private Bosco.Utility.Controls.ucToolBar ucToolBarUserMenu;
        private DevExpress.XtraEditors.PanelControl pnlUserMenuBottom;
        private DevExpress.XtraEditors.LabelControl lblUserRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.PanelControl pnlUserFill;
        private DevExpress.XtraGrid.GridControl gcUserView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUserView;
        private DevExpress.XtraGrid.Columns.GridColumn colUserId;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colUserRole;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colContactNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colEmailAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colUserPhoto;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryPeUserPhoto;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstName;
        private DevExpress.XtraGrid.Columns.GridColumn colLastName;
    }
}