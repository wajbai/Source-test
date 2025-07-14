namespace ACPP.Modules.TDS
{
    partial class frmNatureofPaymentsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNatureofPaymentsView));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucNatureofPayments = new Bosco.Utility.Controls.ucToolBar();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcNatureofPayments = new DevExpress.XtraGrid.GridControl();
            this.gvNoPayments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolNaturepaymentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolPaymentCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolPaymentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolSectionCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolSectionName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNatureofPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNoPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucNatureofPayments);
            this.layoutControl1.Controls.Add(this.lblCount);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcNatureofPayments);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(359, 335, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucNatureofPayments
            // 
            this.ucNatureofPayments.ChangeAddCaption = "&Add";
            this.ucNatureofPayments.ChangeCaption = "&Edit";
            this.ucNatureofPayments.ChangeDeleteCaption = "&Delete";
            this.ucNatureofPayments.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucNatureofPayments.ChangePostInterestCaption = "P&ost Interest";
            this.ucNatureofPayments.ChangePrintCaption = "&Print";
            this.ucNatureofPayments.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucNatureofPayments.DisableAddButton = true;
            this.ucNatureofPayments.DisableAMCRenew = true;
            this.ucNatureofPayments.DisableCloseButton = true;
            this.ucNatureofPayments.DisableDeleteButton = true;
            this.ucNatureofPayments.DisableDownloadExcel = true;
            this.ucNatureofPayments.DisableEditButton = true;
            this.ucNatureofPayments.DisableMoveTransaction = true;
            this.ucNatureofPayments.DisableNatureofPayments = false;
            this.ucNatureofPayments.DisablePostInterest = true;
            this.ucNatureofPayments.DisablePrintButton = true;
            this.ucNatureofPayments.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucNatureofPayments, "ucNatureofPayments");
            this.ucNatureofPayments.Name = "ucNatureofPayments";
            this.ucNatureofPayments.ShowHTML = true;
            this.ucNatureofPayments.ShowMMT = true;
            this.ucNatureofPayments.ShowPDF = true;
            this.ucNatureofPayments.ShowRTF = true;
            this.ucNatureofPayments.ShowText = true;
            this.ucNatureofPayments.ShowXLS = true;
            this.ucNatureofPayments.ShowXLSX = true;
            this.ucNatureofPayments.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucNatureofPayments.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucNatureofPayments.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucNatureofPayments.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucNatureofPayments.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucNatureofPayments.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucNatureofPayments.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucNatureofPayments.AddClicked += new System.EventHandler(this.ucNatureofPayments_AddClicked);
            this.ucNatureofPayments.EditClicked += new System.EventHandler(this.ucNatureofPayments_EditClicked);
            this.ucNatureofPayments.DeleteClicked += new System.EventHandler(this.ucNatureofPayments_DeleteClicked);
            this.ucNatureofPayments.PrintClicked += new System.EventHandler(this.ucNatureofPayments_PrintClicked);
            this.ucNatureofPayments.CloseClicked += new System.EventHandler(this.ucNatureofPayments_CloseClicked);
            this.ucNatureofPayments.RefreshClicked += new System.EventHandler(this.ucNatureofPayments_RefreshClicked);
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.Appearance.Font")));
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.StyleController = this.layoutControl1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.StyleController = this.layoutControl1;
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
            // gcNatureofPayments
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gcNatureofPayments.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            resources.ApplyResources(this.gcNatureofPayments, "gcNatureofPayments");
            this.gcNatureofPayments.MainView = this.gvNoPayments;
            this.gcNatureofPayments.Name = "gcNatureofPayments";
            this.gcNatureofPayments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNoPayments});
            // 
            // gvNoPayments
            // 
            this.gvNoPayments.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvNoPayments.Appearance.FocusedRow.Font")));
            this.gvNoPayments.Appearance.FocusedRow.Options.UseFont = true;
            this.gvNoPayments.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvNoPayments.Appearance.HeaderPanel.Font")));
            this.gvNoPayments.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvNoPayments.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolNaturepaymentId,
            this.gcolPaymentCode,
            this.gcolPaymentName,
            this.gcolSectionCode,
            this.gcolSectionName,
            this.colStatus});
            this.gvNoPayments.GridControl = this.gcNatureofPayments;
            this.gvNoPayments.Name = "gvNoPayments";
            this.gvNoPayments.OptionsBehavior.Editable = false;
            this.gvNoPayments.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvNoPayments.OptionsView.ShowGroupPanel = false;
            this.gvNoPayments.DoubleClick += new System.EventHandler(this.gvNoPayments_DoubleClick);
            this.gvNoPayments.RowCountChanged += new System.EventHandler(this.gvNoPayments_RowCountChanged);
            // 
            // gcolNaturepaymentId
            // 
            resources.ApplyResources(this.gcolNaturepaymentId, "gcolNaturepaymentId");
            this.gcolNaturepaymentId.FieldName = "NATURE_PAY_ID";
            this.gcolNaturepaymentId.Name = "gcolNaturepaymentId";
            this.gcolNaturepaymentId.OptionsColumn.AllowEdit = false;
            this.gcolNaturepaymentId.OptionsColumn.AllowFocus = false;
            // 
            // gcolPaymentCode
            // 
            resources.ApplyResources(this.gcolPaymentCode, "gcolPaymentCode");
            this.gcolPaymentCode.FieldName = "PAYMENT_CODE";
            this.gcolPaymentCode.Name = "gcolPaymentCode";
            this.gcolPaymentCode.OptionsColumn.AllowEdit = false;
            this.gcolPaymentCode.OptionsColumn.AllowFocus = false;
            this.gcolPaymentCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcolPaymentName
            // 
            resources.ApplyResources(this.gcolPaymentName, "gcolPaymentName");
            this.gcolPaymentName.FieldName = "NAME";
            this.gcolPaymentName.Name = "gcolPaymentName";
            this.gcolPaymentName.OptionsColumn.AllowEdit = false;
            this.gcolPaymentName.OptionsColumn.AllowFocus = false;
            this.gcolPaymentName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcolSectionCode
            // 
            resources.ApplyResources(this.gcolSectionCode, "gcolSectionCode");
            this.gcolSectionCode.FieldName = "SECTION";
            this.gcolSectionCode.Name = "gcolSectionCode";
            this.gcolSectionCode.OptionsColumn.AllowEdit = false;
            this.gcolSectionCode.OptionsColumn.AllowFocus = false;
            this.gcolSectionCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcolSectionName
            // 
            resources.ApplyResources(this.gcolSectionName, "gcolSectionName");
            this.gcolSectionName.FieldName = "SECTION_NAME";
            this.gcolSectionName.Name = "gcolSectionName";
            this.gcolSectionName.OptionsColumn.AllowEdit = false;
            this.gcolSectionName.OptionsColumn.AllowFocus = false;
            this.gcolSectionName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(887, 430);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcNatureofPayments;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(887, 369);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 403);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(97, 27);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(97, 27);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(97, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(97, 403);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 27);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 27);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(744, 27);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl1;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(841, 403);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(13, 27);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(13, 27);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(13, 27);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lblCount;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(854, 403);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(33, 27);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(33, 27);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(33, 27);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucNatureofPayments;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(887, 34);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmNatureofPaymentsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNatureofPaymentsView";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ShowFilterClicked += new System.EventHandler(this.frmNatureofPaymentsView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmNatureofPaymentsView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmNatureofPaymentsView_Activated);
            this.Load += new System.EventHandler(this.frmNatureofPaymentsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNatureofPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNoPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcNatureofPayments;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNoPayments;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gcolNaturepaymentId;
        private DevExpress.XtraGrid.Columns.GridColumn gcolPaymentName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolSectionCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcolPaymentCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcolSectionName;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Bosco.Utility.Controls.ucToolBar ucNatureofPayments;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;

    }
}