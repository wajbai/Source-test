namespace ACPP.Modules.TDS
{
    partial class frmDeducteeTypesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeducteeTypesView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucDeducteeType = new Bosco.Utility.Controls.ucToolBar();
            this.lblRowcount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcDeducteeTypes = new DevExpress.XtraGrid.GridControl();
            this.gvDeductee = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolDeducteeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDeducteeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolResidentialStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDeducteeStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDeducteeTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeductee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucDeducteeType);
            this.layoutControl1.Controls.Add(this.lblRowcount);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcDeducteeTypes);
            this.layoutControl1.Controls.Add(this.labelControl1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(92, 226, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucDeducteeType
            // 
            this.ucDeducteeType.ChangeAddCaption = "&Add";
            this.ucDeducteeType.ChangeCaption = "&Edit";
            this.ucDeducteeType.ChangeDeleteCaption = "&Delete";
            this.ucDeducteeType.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucDeducteeType.ChangePostInterestCaption = "P&ost Interest";
            this.ucDeducteeType.ChangePrintCaption = "&Print";
            this.ucDeducteeType.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucDeducteeType.DisableAddButton = true;
            this.ucDeducteeType.DisableAMCRenew = true;
            this.ucDeducteeType.DisableCloseButton = true;
            this.ucDeducteeType.DisableDeleteButton = true;
            this.ucDeducteeType.DisableDownloadExcel = true;
            this.ucDeducteeType.DisableEditButton = true;
            this.ucDeducteeType.DisableMoveTransaction = false;
            this.ucDeducteeType.DisableNatureofPayments = true;
            this.ucDeducteeType.DisablePostInterest = true;
            this.ucDeducteeType.DisablePrintButton = true;
            this.ucDeducteeType.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucDeducteeType, "ucDeducteeType");
            this.ucDeducteeType.Name = "ucDeducteeType";
            this.ucDeducteeType.ShowHTML = true;
            this.ucDeducteeType.ShowMMT = true;
            this.ucDeducteeType.ShowPDF = true;
            this.ucDeducteeType.ShowRTF = true;
            this.ucDeducteeType.ShowText = true;
            this.ucDeducteeType.ShowXLS = true;
            this.ucDeducteeType.ShowXLSX = true;
            this.ucDeducteeType.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeType.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeType.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeType.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeType.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeType.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDeducteeType.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDeducteeType.AddClicked += new System.EventHandler(this.ucDeducteeType_AddClicked);
            this.ucDeducteeType.EditClicked += new System.EventHandler(this.ucDeducteeType_EditClicked);
            this.ucDeducteeType.DeleteClicked += new System.EventHandler(this.ucDeducteeType_DeleteClicked);
            this.ucDeducteeType.PrintClicked += new System.EventHandler(this.ucDeducteeType_PrintClicked);
            this.ucDeducteeType.CloseClicked += new System.EventHandler(this.ucDeducteeType_CloseClicked);
            this.ucDeducteeType.RefreshClicked += new System.EventHandler(this.ucDeducteeType_RefreshClicked);
            // 
            // lblRowcount
            // 
            this.lblRowcount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRowcount.Appearance.Font")));
            resources.ApplyResources(this.lblRowcount, "lblRowcount");
            this.lblRowcount.Name = "lblRowcount";
            this.lblRowcount.StyleController = this.layoutControl1;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcDeducteeTypes
            // 
            resources.ApplyResources(this.gcDeducteeTypes, "gcDeducteeTypes");
            this.gcDeducteeTypes.MainView = this.gvDeductee;
            this.gcDeducteeTypes.Name = "gcDeducteeTypes";
            this.gcDeducteeTypes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDeductee});
            // 
            // gvDeductee
            // 
            this.gvDeductee.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvDeductee.Appearance.FocusedRow.Font")));
            this.gvDeductee.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDeductee.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvDeductee.Appearance.HeaderPanel.Font")));
            this.gvDeductee.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDeductee.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolDeducteeName,
            this.gcolDeducteeId,
            this.gcolResidentialStatus,
            this.gcolDeducteeStatus,
            this.gcolActive});
            this.gvDeductee.GridControl = this.gcDeducteeTypes;
            this.gvDeductee.Name = "gvDeductee";
            this.gvDeductee.OptionsBehavior.Editable = false;
            this.gvDeductee.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDeductee.OptionsView.ShowGroupPanel = false;
            this.gvDeductee.DoubleClick += new System.EventHandler(this.gvDeductee_DoubleClick);
            this.gvDeductee.RowCountChanged += new System.EventHandler(this.gvDeductee_RowCountChanged);
            // 
            // gcolDeducteeName
            // 
            resources.ApplyResources(this.gcolDeducteeName, "gcolDeducteeName");
            this.gcolDeducteeName.FieldName = "NAME";
            this.gcolDeducteeName.Name = "gcolDeducteeName";
            this.gcolDeducteeName.OptionsColumn.AllowEdit = false;
            this.gcolDeducteeName.OptionsColumn.AllowFocus = false;
            this.gcolDeducteeName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcolDeducteeId
            // 
            resources.ApplyResources(this.gcolDeducteeId, "gcolDeducteeId");
            this.gcolDeducteeId.FieldName = "DEDUCTEE_TYPE_ID";
            this.gcolDeducteeId.Name = "gcolDeducteeId";
            this.gcolDeducteeId.OptionsColumn.AllowEdit = false;
            this.gcolDeducteeId.OptionsColumn.AllowFocus = false;
            // 
            // gcolResidentialStatus
            // 
            resources.ApplyResources(this.gcolResidentialStatus, "gcolResidentialStatus");
            this.gcolResidentialStatus.FieldName = "RESIDENTIAL_STATUS";
            this.gcolResidentialStatus.Name = "gcolResidentialStatus";
            this.gcolResidentialStatus.OptionsColumn.AllowEdit = false;
            this.gcolResidentialStatus.OptionsColumn.AllowFocus = false;
            this.gcolResidentialStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcolDeducteeStatus
            // 
            resources.ApplyResources(this.gcolDeducteeStatus, "gcolDeducteeStatus");
            this.gcolDeducteeStatus.FieldName = "DEDUCTEE_TYPE";
            this.gcolDeducteeStatus.Name = "gcolDeducteeStatus";
            this.gcolDeducteeStatus.OptionsColumn.AllowEdit = false;
            this.gcolDeducteeStatus.OptionsColumn.AllowFocus = false;
            this.gcolDeducteeStatus.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcolActive
            // 
            resources.ApplyResources(this.gcolActive, "gcolActive");
            this.gcolActive.FieldName = "STATUS";
            this.gcolActive.Name = "gcolActive";
            this.gcolActive.OptionsColumn.AllowEdit = false;
            this.gcolActive.OptionsColumn.AllowFocus = false;
            this.gcolActive.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(712, 490);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcDeducteeTypes;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(712, 432);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 466);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(95, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem5.AppearanceItemCaption.Font")));
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.lblRowcount;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(680, 466);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(32, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(32, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(32, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font")));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.labelControl1;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(667, 466);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(13, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(13, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(13, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucDeducteeType;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(712, 34);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(95, 466);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(572, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmDeducteeTypesView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDeducteeTypesView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ShowFilterClicked += new System.EventHandler(this.frmDeducteeTypesView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmDeducteeTypesView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmDeducteeTypesView_Activated);
            this.Load += new System.EventHandler(this.frmDeducteeTypesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDeducteeTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDeductee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcDeducteeTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDeductee;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDeducteeName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDeducteeId;
        private DevExpress.XtraGrid.Columns.GridColumn gcolResidentialStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDeducteeStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gcolActive;
        private DevExpress.XtraEditors.LabelControl lblRowcount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Bosco.Utility.Controls.ucToolBar ucDeducteeType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}