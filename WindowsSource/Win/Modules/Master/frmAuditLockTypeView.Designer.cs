namespace ACPP.Modules.Master
{
    partial class frmAuditLockTypeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditLockTypeView));
            this.lcAuditTypeView = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcAuditTypeView = new DevExpress.XtraGrid.GridControl();
            this.gvAuditTypeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLockTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLockType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblGridControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ucToolBarAuditType = new Bosco.Utility.Controls.ucToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.lcAuditTypeView)).BeginInit();
            this.lcAuditTypeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditTypeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditTypeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // lcAuditTypeView
            // 
            this.lcAuditTypeView.Controls.Add(this.chkShowFilter);
            this.lcAuditTypeView.Controls.Add(this.gcAuditTypeView);
            resources.ApplyResources(this.lcAuditTypeView, "lcAuditTypeView");
            this.lcAuditTypeView.Name = "lcAuditTypeView";
            this.lcAuditTypeView.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(634, 203, 250, 350);
            this.lcAuditTypeView.Root = this.layoutControlGroup1;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.lcAuditTypeView;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcAuditTypeView
            // 
            resources.ApplyResources(this.gcAuditTypeView, "gcAuditTypeView");
            this.gcAuditTypeView.MainView = this.gvAuditTypeView;
            this.gcAuditTypeView.Name = "gcAuditTypeView";
            this.gcAuditTypeView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAuditTypeView});
            this.gcAuditTypeView.DoubleClick += new System.EventHandler(this.gcAuditTypeView_DoubleClick);
            // 
            // gvAuditTypeView
            // 
            this.gvAuditTypeView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditTypeView.Appearance.FocusedRow.Font")));
            this.gvAuditTypeView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAuditTypeView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditTypeView.Appearance.HeaderPanel.Font")));
            this.gvAuditTypeView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAuditTypeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLockTypeId,
            this.colLockType});
            this.gvAuditTypeView.GridControl = this.gcAuditTypeView;
            this.gvAuditTypeView.Name = "gvAuditTypeView";
            this.gvAuditTypeView.OptionsBehavior.Editable = false;
            this.gvAuditTypeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAuditTypeView.OptionsView.ShowGroupPanel = false;
            this.gvAuditTypeView.RowCountChanged += new System.EventHandler(this.gvAuditTypeView_RowCountChanged);
            // 
            // colLockTypeId
            // 
            resources.ApplyResources(this.colLockTypeId, "colLockTypeId");
            this.colLockTypeId.FieldName = "LOCK_TYPE_ID";
            this.colLockTypeId.Name = "colLockTypeId";
            // 
            // colLockType
            // 
            resources.ApplyResources(this.colLockType, "colLockType");
            this.colLockType.FieldName = "LOCK_TYPE";
            this.colLockType.Name = "colLockType";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblGridControl,
            this.lblRowCount,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(732, 352);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblGridControl
            // 
            this.lblGridControl.Control = this.gcAuditTypeView;
            resources.ApplyResources(this.lblGridControl, "lblGridControl");
            this.lblGridControl.Location = new System.Drawing.Point(0, 0);
            this.lblGridControl.Name = "lblGridControl";
            this.lblGridControl.Size = new System.Drawing.Size(732, 329);
            this.lblGridControl.TextSize = new System.Drawing.Size(0, 0);
            this.lblGridControl.TextToControlDistance = 0;
            this.lblGridControl.TextVisible = false;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AllowHotTrack = false;
            this.lblRowCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCount.AppearanceItemCaption.Font")));
            this.lblRowCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Location = new System.Drawing.Point(677, 329);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(55, 23);
            this.lblRowCount.TextSize = new System.Drawing.Size(3, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 329);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(111, 23);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(111, 329);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(566, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucToolBarAuditType
            // 
            this.ucToolBarAuditType.ChangeAddCaption = "&Add";
            this.ucToolBarAuditType.ChangeCaption = "&Edit";
            this.ucToolBarAuditType.ChangeDeleteCaption = "&Delete";
            this.ucToolBarAuditType.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarAuditType.ChangePrintCaption = "&Print";
            this.ucToolBarAuditType.DisableAddButton = true;
            this.ucToolBarAuditType.DisableCloseButton = true;
            this.ucToolBarAuditType.DisableDeleteButton = true;
            this.ucToolBarAuditType.DisableDownloadExcel = true;
            this.ucToolBarAuditType.DisableEditButton = true;
            this.ucToolBarAuditType.DisableMoveTransaction = true;
            this.ucToolBarAuditType.DisableNatureofPayments = true;
            this.ucToolBarAuditType.DisablePrintButton = true;
            this.ucToolBarAuditType.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarAuditType, "ucToolBarAuditType");
            this.ucToolBarAuditType.Name = "ucToolBarAuditType";
            this.ucToolBarAuditType.ShowHTML = true;
            this.ucToolBarAuditType.ShowMMT = true;
            this.ucToolBarAuditType.ShowPDF = true;
            this.ucToolBarAuditType.ShowRTF = true;
            this.ucToolBarAuditType.ShowText = true;
            this.ucToolBarAuditType.ShowXLS = true;
            this.ucToolBarAuditType.ShowXLSX = true;
            this.ucToolBarAuditType.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAuditType.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAuditType.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAuditType.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAuditType.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAuditType.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAuditType.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAuditType.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAuditType.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAuditType.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAuditType.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAuditType.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAuditType.AddClicked += new System.EventHandler(this.ucToolBarAuditType_AddClicked);
            this.ucToolBarAuditType.EditClicked += new System.EventHandler(this.ucToolBarAuditType_EditClicked);
            this.ucToolBarAuditType.DeleteClicked += new System.EventHandler(this.ucToolBarAuditType_DeleteClicked);
            this.ucToolBarAuditType.PrintClicked += new System.EventHandler(this.ucToolBarAuditType_PrintClicked);
            this.ucToolBarAuditType.CloseClicked += new System.EventHandler(this.ucToolBarAuditType_CloseClicked);
            this.ucToolBarAuditType.RefreshClicked += new System.EventHandler(this.ucToolBarAuditType_RefreshClicked);
            // 
            // frmAuditLockTypeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcAuditTypeView);
            this.Controls.Add(this.ucToolBarAuditType);
            this.Name = "frmAuditLockTypeView";
            this.Activated += new System.EventHandler(this.frmAuditLockTypeView_Activated);
            this.Load += new System.EventHandler(this.frmAuditLockTypeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcAuditTypeView)).EndInit();
            this.lcAuditTypeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditTypeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditTypeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcAuditTypeView;
        private DevExpress.XtraGrid.GridControl gcAuditTypeView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAuditTypeView;
        private Bosco.Utility.Controls.ucToolBar ucToolBarAuditType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn colLockTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colLockType;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem lblGridControl;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}