namespace ACPP.Modules.TDS
{
    partial class frmTDSSectionView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTDSSectionView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTDSRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcTDSSection = new DevExpress.XtraGrid.GridControl();
            this.gvTDSSection = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTDS_Section_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucToolBarTDSSection = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTDSRecordCount);
            this.layoutControl1.Controls.Add(this.lblRecordCount);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcTDSSection);
            this.layoutControl1.Controls.Add(this.ucToolBarTDSSection);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(251, 112, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblTDSRecordCount
            // 
            resources.ApplyResources(this.lblTDSRecordCount, "lblTDSRecordCount");
            this.lblTDSRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblTDSRecordCount.Appearance.Font")));
            this.lblTDSRecordCount.Name = "lblTDSRecordCount";
            this.lblTDSRecordCount.StyleController = this.layoutControl1;
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.StyleController = this.layoutControl1;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcTDSSection
            // 
            resources.ApplyResources(this.gcTDSSection, "gcTDSSection");
            this.gcTDSSection.MainView = this.gvTDSSection;
            this.gcTDSSection.Name = "gcTDSSection";
            this.gcTDSSection.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTDSSection});
            // 
            // gvTDSSection
            // 
            this.gvTDSSection.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSSection.Appearance.FocusedRow.Font")));
            this.gvTDSSection.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTDSSection.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSSection.Appearance.HeaderPanel.Font")));
            this.gvTDSSection.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTDSSection.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvTDSSection.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTDS_Section_Id,
            this.colCode,
            this.colName,
            this.colStatus});
            this.gvTDSSection.GridControl = this.gcTDSSection;
            this.gvTDSSection.Name = "gvTDSSection";
            this.gvTDSSection.OptionsBehavior.Editable = false;
            this.gvTDSSection.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTDSSection.OptionsView.ShowGroupPanel = false;
            this.gvTDSSection.DoubleClick += new System.EventHandler(this.gvTDSSection_DoubleClick);
            this.gvTDSSection.RowCountChanged += new System.EventHandler(this.gvTDSSection_RowCountChanged);
            // 
            // colTDS_Section_Id
            // 
            resources.ApplyResources(this.colTDS_Section_Id, "colTDS_Section_Id");
            this.colTDS_Section_Id.FieldName = "TDS_SECTION_ID";
            this.colTDS_Section_Id.Name = "colTDS_Section_Id";
            // 
            // colCode
            // 
            this.colCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCode.AppearanceHeader.Font")));
            this.colCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCode, "colCode");
            this.colCode.FieldName = "CODE";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.OptionsColumn.FixedWidth = true;
            this.colCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colName.AppearanceHeader.Font")));
            this.colName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "SECTION_NAME";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            // 
            // ucToolBarTDSSection
            // 
            this.ucToolBarTDSSection.ChangeAddCaption = "&Add";
            this.ucToolBarTDSSection.ChangeCaption = "&Edit";
            this.ucToolBarTDSSection.ChangeDeleteCaption = "&Delete";
            this.ucToolBarTDSSection.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarTDSSection.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarTDSSection.ChangePrintCaption = "&Print";
            this.ucToolBarTDSSection.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarTDSSection.DisableAddButton = true;
            this.ucToolBarTDSSection.DisableAMCRenew = true;
            this.ucToolBarTDSSection.DisableCloseButton = true;
            this.ucToolBarTDSSection.DisableDeleteButton = true;
            this.ucToolBarTDSSection.DisableDownloadExcel = true;
            this.ucToolBarTDSSection.DisableEditButton = true;
            this.ucToolBarTDSSection.DisableMoveTransaction = true;
            this.ucToolBarTDSSection.DisableNatureofPayments = true;
            this.ucToolBarTDSSection.DisablePostInterest = true;
            this.ucToolBarTDSSection.DisablePrintButton = true;
            this.ucToolBarTDSSection.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarTDSSection, "ucToolBarTDSSection");
            this.ucToolBarTDSSection.Name = "ucToolBarTDSSection";
            this.ucToolBarTDSSection.ShowHTML = true;
            this.ucToolBarTDSSection.ShowMMT = true;
            this.ucToolBarTDSSection.ShowPDF = true;
            this.ucToolBarTDSSection.ShowRTF = true;
            this.ucToolBarTDSSection.ShowText = true;
            this.ucToolBarTDSSection.ShowXLS = true;
            this.ucToolBarTDSSection.ShowXLSX = true;
            this.ucToolBarTDSSection.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarTDSSection.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarTDSSection.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarTDSSection.AddClicked += new System.EventHandler(this.ucToolBarTDSSection_AddClicked);
            this.ucToolBarTDSSection.EditClicked += new System.EventHandler(this.ucToolBarTDSSection_EditClicked);
            this.ucToolBarTDSSection.DeleteClicked += new System.EventHandler(this.ucToolBarTDSSection_DeleteClicked);
            this.ucToolBarTDSSection.PrintClicked += new System.EventHandler(this.ucToolBarTDSSection_PrintClicked);
            this.ucToolBarTDSSection.CloseClicked += new System.EventHandler(this.ucToolBarTDSSection_CloseClicked);
            this.ucToolBarTDSSection.RefreshClicked += new System.EventHandler(this.ucToolBarTDSSection_RefreshClicked);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(745, 368);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBarTDSSection;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(745, 34);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcTDSSection;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(745, 310);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 344);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(80, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(84, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblRecordCount;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(729, 344);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(7, 13);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem4.Size = new System.Drawing.Size(16, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lblTDSRecordCount;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(720, 344);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(9, 13);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(9, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(84, 344);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(636, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmTDSSectionView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTDSSectionView";
            this.ShowFilterClicked += new System.EventHandler(this.frmTDSSectionView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmTDSSectionView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmTDSSectionView_Activated);
            this.Load += new System.EventHandler(this.frmTDSSectionView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcTDSSection;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTDSSection;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.Columns.GridColumn colTDS_Section_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblTDSRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Bosco.Utility.Controls.ucToolBar ucToolBarTDSSection;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
    }
}