namespace ACPP.Modules.Master
{
    partial class frmAuditLockTransView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditLockTransView));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gcAuditLockTransView = new DevExpress.XtraGrid.GridControl();
            this.gvAuditLockTransView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLockTransId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLockType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLockByPortal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResetPassword = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnResetPassword = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtnReset = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.ucAuditLockTransView = new Bosco.Utility.Controls.ucToolBar();
            this.lcTransLock = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.lcMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditLockTransView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditLockTransView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnResetPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTransLock)).BeginInit();
            this.lcTransLock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            this.SuspendLayout();
            // 
            // gcAuditLockTransView
            // 
            resources.ApplyResources(this.gcAuditLockTransView, "gcAuditLockTransView");
            this.gcAuditLockTransView.MainView = this.gvAuditLockTransView;
            this.gcAuditLockTransView.Name = "gcAuditLockTransView";
            this.gcAuditLockTransView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rbtnReset,
            this.rbtnResetPassword});
            this.gcAuditLockTransView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAuditLockTransView});
            this.gcAuditLockTransView.DoubleClick += new System.EventHandler(this.gcAuditLockTransView_DoubleClick);
            // 
            // gvAuditLockTransView
            // 
            this.gvAuditLockTransView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditLockTransView.Appearance.FocusedRow.Font")));
            this.gvAuditLockTransView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAuditLockTransView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditLockTransView.Appearance.HeaderPanel.Font")));
            this.gvAuditLockTransView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAuditLockTransView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLockTransId,
            this.colProject,
            this.colDateFrom,
            this.colDateTo,
            this.colLockType,
            this.colReason,
            this.colLockByPortal,
            this.colResetPassword});
            this.gvAuditLockTransView.GridControl = this.gcAuditLockTransView;
            this.gvAuditLockTransView.Name = "gvAuditLockTransView";
            this.gvAuditLockTransView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAuditLockTransView.OptionsView.ShowGroupPanel = false;
            this.gvAuditLockTransView.RowCountChanged += new System.EventHandler(this.gvAuditLockTransView_RowCountChanged);
            // 
            // colLockTransId
            // 
            resources.ApplyResources(this.colLockTransId, "colLockTransId");
            this.colLockTransId.FieldName = "LOCK_TRANS_ID";
            this.colLockTransId.Name = "colLockTransId";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            // 
            // colDateFrom
            // 
            resources.ApplyResources(this.colDateFrom, "colDateFrom");
            this.colDateFrom.DisplayFormat.FormatString = "d";
            this.colDateFrom.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateFrom.FieldName = "DATE_FROM";
            this.colDateFrom.Name = "colDateFrom";
            this.colDateFrom.OptionsColumn.AllowEdit = false;
            // 
            // colDateTo
            // 
            resources.ApplyResources(this.colDateTo, "colDateTo");
            this.colDateTo.DisplayFormat.FormatString = "d";
            this.colDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateTo.FieldName = "DATE_TO";
            this.colDateTo.Name = "colDateTo";
            this.colDateTo.OptionsColumn.AllowEdit = false;
            // 
            // colLockType
            // 
            resources.ApplyResources(this.colLockType, "colLockType");
            this.colLockType.FieldName = "LOCK_TYPE";
            this.colLockType.Name = "colLockType";
            this.colLockType.OptionsColumn.AllowEdit = false;
            // 
            // colReason
            // 
            resources.ApplyResources(this.colReason, "colReason");
            this.colReason.FieldName = "REASON";
            this.colReason.Name = "colReason";
            this.colReason.OptionsColumn.AllowEdit = false;
            // 
            // colLockByPortal
            // 
            resources.ApplyResources(this.colLockByPortal, "colLockByPortal");
            this.colLockByPortal.FieldName = "LOCK_BY_PORTAL";
            this.colLockByPortal.Name = "colLockByPortal";
            this.colLockByPortal.OptionsColumn.AllowEdit = false;
            this.colLockByPortal.OptionsColumn.AllowFocus = false;
            // 
            // colResetPassword
            // 
            this.colResetPassword.ColumnEdit = this.rbtnResetPassword;
            this.colResetPassword.Name = "colResetPassword";
            this.colResetPassword.OptionsColumn.AllowMove = false;
            this.colResetPassword.OptionsColumn.FixedWidth = true;
            this.colResetPassword.OptionsColumn.ShowCaption = false;
            this.colResetPassword.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.colResetPassword, "colResetPassword");
            // 
            // rbtnResetPassword
            // 
            resources.ApplyResources(this.rbtnResetPassword, "rbtnResetPassword");
            this.rbtnResetPassword.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnResetPassword.Buttons"))), resources.GetString("rbtnResetPassword.Buttons1"), ((int)(resources.GetObject("rbtnResetPassword.Buttons2"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons3"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons4"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnResetPassword.Buttons6"))), global::ACPP.Properties.Resources.unlock, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbtnResetPassword.Buttons7"), ((object)(resources.GetObject("rbtnResetPassword.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnResetPassword.Buttons9"))), ((bool)(resources.GetObject("rbtnResetPassword.Buttons10"))))});
            this.rbtnResetPassword.Name = "rbtnResetPassword";
            this.rbtnResetPassword.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.rbtnResetPassword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rbtnResetPassword_ButtonClick);
            // 
            // rbtnReset
            // 
            resources.ApplyResources(this.rbtnReset, "rbtnReset");
            this.rbtnReset.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnReset.Buttons"))), resources.GetString("rbtnReset.Buttons1"), ((int)(resources.GetObject("rbtnReset.Buttons2"))), ((bool)(resources.GetObject("rbtnReset.Buttons3"))), ((bool)(resources.GetObject("rbtnReset.Buttons4"))), ((bool)(resources.GetObject("rbtnReset.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnReset.Buttons6"))), global::ACPP.Properties.Resources.TransLock1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("rbtnReset.Buttons7"), ((object)(resources.GetObject("rbtnReset.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnReset.Buttons9"))), ((bool)(resources.GetObject("rbtnReset.Buttons10"))))});
            this.rbtnReset.Name = "rbtnReset";
            // 
            // ucAuditLockTransView
            // 
            this.ucAuditLockTransView.ChangeAddCaption = "&Add";
            this.ucAuditLockTransView.ChangeCaption = "&Edit";
            this.ucAuditLockTransView.ChangeDeleteCaption = "&Delete";
            this.ucAuditLockTransView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAuditLockTransView.ChangePostInterestCaption = "P&ost Interest";
            this.ucAuditLockTransView.ChangePrintCaption = "&Print";
            this.ucAuditLockTransView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucAuditLockTransView.DisableAddButton = true;
            this.ucAuditLockTransView.DisableAMCRenew = true;
            this.ucAuditLockTransView.DisableCloseButton = true;
            this.ucAuditLockTransView.DisableDeleteButton = true;
            this.ucAuditLockTransView.DisableDownloadExcel = true;
            this.ucAuditLockTransView.DisableEditButton = true;
            this.ucAuditLockTransView.DisableMoveTransaction = true;
            this.ucAuditLockTransView.DisableNatureofPayments = true;
            this.ucAuditLockTransView.DisablePostInterest = true;
            this.ucAuditLockTransView.DisablePrintButton = true;
            this.ucAuditLockTransView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucAuditLockTransView, "ucAuditLockTransView");
            this.ucAuditLockTransView.Name = "ucAuditLockTransView";
            this.ucAuditLockTransView.ShowHTML = true;
            this.ucAuditLockTransView.ShowMMT = true;
            this.ucAuditLockTransView.ShowPDF = true;
            this.ucAuditLockTransView.ShowRTF = true;
            this.ucAuditLockTransView.ShowText = true;
            this.ucAuditLockTransView.ShowXLS = true;
            this.ucAuditLockTransView.ShowXLSX = true;
            this.ucAuditLockTransView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditLockTransView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditLockTransView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditLockTransView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditLockTransView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditLockTransView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditLockTransView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditLockTransView.AddClicked += new System.EventHandler(this.ucAuditLockTransView_AddClicked);
            this.ucAuditLockTransView.EditClicked += new System.EventHandler(this.ucAuditLockTransView_EditClicked);
            this.ucAuditLockTransView.DeleteClicked += new System.EventHandler(this.ucAuditLockTransView_DeleteClicked);
            this.ucAuditLockTransView.PrintClicked += new System.EventHandler(this.ucAuditLockTransView_PrintClicked);
            this.ucAuditLockTransView.CloseClicked += new System.EventHandler(this.ucAuditLockTransView_CloseClicked);
            this.ucAuditLockTransView.RefreshClicked += new System.EventHandler(this.ucAuditLockTransView_RefreshClicked);
            // 
            // lcTransLock
            // 
            this.lcTransLock.Controls.Add(this.chkShowFilter);
            this.lcTransLock.Controls.Add(this.gcAuditLockTransView);
            resources.ApplyResources(this.lcTransLock, "lcTransLock");
            this.lcTransLock.Name = "lcTransLock";
            this.lcTransLock.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(614, 199, 250, 350);
            this.lcTransLock.Root = this.lcMain;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.lcTransLock;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // lcMain
            // 
            resources.ApplyResources(this.lcMain, "lcMain");
            this.lcMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcMain.GroupBordersVisible = false;
            this.lcMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgMain,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.lblRowCount});
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "Root";
            this.lcMain.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcMain.Size = new System.Drawing.Size(752, 349);
            this.lcMain.TextVisible = false;
            // 
            // lcgMain
            // 
            this.lcgMain.Control = this.gcAuditLockTransView;
            resources.ApplyResources(this.lcgMain, "lcgMain");
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "lcgMain";
            this.lcgMain.Size = new System.Drawing.Size(752, 326);
            this.lcgMain.TextSize = new System.Drawing.Size(0, 0);
            this.lcgMain.TextToControlDistance = 0;
            this.lcgMain.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(115, 326);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(590, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 326);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(115, 23);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AllowHotTrack = false;
            this.lblRowCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCount.AppearanceItemCaption.Font")));
            this.lblRowCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Location = new System.Drawing.Point(705, 326);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(47, 23);
            this.lblRowCount.TextSize = new System.Drawing.Size(3, 13);
            // 
            // frmAuditLockTransView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcTransLock);
            this.Controls.Add(this.ucAuditLockTransView);
            this.Name = "frmAuditLockTransView";
            this.EnterClicked += new System.EventHandler(this.frmAuditLockTransView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmAuditLockTransView_Activated);
            this.Load += new System.EventHandler(this.frmAuditLockTransView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditLockTransView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditLockTransView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnResetPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcTransLock)).EndInit();
            this.lcTransLock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bosco.Utility.Controls.ucToolBar ucAuditLockTransView;
        private DevExpress.XtraGrid.GridControl gcAuditLockTransView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAuditLockTransView;
        private DevExpress.XtraGrid.Columns.GridColumn colLockTransId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colDateFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colDateTo;
        private DevExpress.XtraGrid.Columns.GridColumn colLockType;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraLayout.LayoutControl lcTransLock;
        private DevExpress.XtraLayout.LayoutControlGroup lcMain;
        private DevExpress.XtraLayout.LayoutControlItem lcgMain;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraGrid.Columns.GridColumn colResetPassword;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnResetPassword;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnReset;
        private DevExpress.XtraGrid.Columns.GridColumn colLockByPortal;
    }
}