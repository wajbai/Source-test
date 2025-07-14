namespace ACPP.Modules.Master
{
    partial class frmProjectCategoryView
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectCategoryView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.pnltop = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarProjectCategory = new Bosco.Utility.Controls.ucToolBar();
            this.pnlfill = new DevExpress.XtraEditors.PanelControl();
            this.gcProjectCategory = new DevExpress.XtraGrid.GridControl();
            this.gvProjectCategory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colITRGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblProjectCategory = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).BeginInit();
            this.pnltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).BeginInit();
            this.pnlfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).BeginInit();
            this.pnlfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnltop
            // 
            this.pnltop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnltop.Controls.Add(this.ucToolBarProjectCategory);
            resources.ApplyResources(this.pnltop, "pnltop");
            this.pnltop.Name = "pnltop";
            // 
            // ucToolBarProjectCategory
            // 
            this.ucToolBarProjectCategory.ChangeAddCaption = "&Add";
            this.ucToolBarProjectCategory.ChangeCaption = "&Edit";
            this.ucToolBarProjectCategory.ChangeDeleteCaption = "&Delete";
            this.ucToolBarProjectCategory.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarProjectCategory.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarProjectCategory.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarProjectCategory.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBarProjectCategory.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucToolBarProjectCategory.ChangePrintCaption = "&Print";
            this.ucToolBarProjectCategory.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarProjectCategory.DisableAddButton = true;
            this.ucToolBarProjectCategory.DisableAMCRenew = true;
            this.ucToolBarProjectCategory.DisableCloseButton = true;
            this.ucToolBarProjectCategory.DisableDeleteButton = true;
            this.ucToolBarProjectCategory.DisableDownloadExcel = true;
            this.ucToolBarProjectCategory.DisableEditButton = true;
            this.ucToolBarProjectCategory.DisableInsertVoucher = true;
            this.ucToolBarProjectCategory.DisableMoveTransaction = true;
            this.ucToolBarProjectCategory.DisableNatureofPayments = true;
            this.ucToolBarProjectCategory.DisablePostInterest = true;
            this.ucToolBarProjectCategory.DisablePrintButton = true;
            this.ucToolBarProjectCategory.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarProjectCategory, "ucToolBarProjectCategory");
            this.ucToolBarProjectCategory.Name = "ucToolBarProjectCategory";
            this.ucToolBarProjectCategory.ShowHTML = true;
            this.ucToolBarProjectCategory.ShowMMT = true;
            this.ucToolBarProjectCategory.ShowPDF = true;
            this.ucToolBarProjectCategory.ShowRTF = true;
            this.ucToolBarProjectCategory.ShowText = true;
            this.ucToolBarProjectCategory.ShowXLS = true;
            this.ucToolBarProjectCategory.ShowXLSX = true;
            this.ucToolBarProjectCategory.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarProjectCategory.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarProjectCategory.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarProjectCategory.AddClicked += new System.EventHandler(this.ucToolBarProjectCategory_AddClicked);
            this.ucToolBarProjectCategory.EditClicked += new System.EventHandler(this.ucToolBarProjectCategory_EditClicked);
            this.ucToolBarProjectCategory.DeleteClicked += new System.EventHandler(this.ucToolBarProjectCategory_DeleteClicked);
            this.ucToolBarProjectCategory.PrintClicked += new System.EventHandler(this.ucToolBarProjectCategory_PrintClicked);
            this.ucToolBarProjectCategory.CloseClicked += new System.EventHandler(this.ucToolBarProjectCategory_CloseClicked);
            this.ucToolBarProjectCategory.RefreshClicked += new System.EventHandler(this.ucToolBarProjectCategory_RefreshClicked);
            // 
            // pnlfill
            // 
            this.pnlfill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfill.Controls.Add(this.gcProjectCategory);
            resources.ApplyResources(this.pnlfill, "pnlfill");
            this.pnlfill.Name = "pnlfill";
            // 
            // gcProjectCategory
            // 
            resources.ApplyResources(this.gcProjectCategory, "gcProjectCategory");
            this.gcProjectCategory.MainView = this.gvProjectCategory;
            this.gcProjectCategory.Name = "gcProjectCategory";
            this.gcProjectCategory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProjectCategory});
            // 
            // gvProjectCategory
            // 
            this.gvProjectCategory.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvProjectCategory.Appearance.FocusedRow.Font")));
            this.gvProjectCategory.Appearance.FocusedRow.Options.UseFont = true;
            this.gvProjectCategory.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvProjectCategory.Appearance.HeaderPanel.Font")));
            this.gvProjectCategory.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvProjectCategory.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvProjectCategory.AppearancePrint.HeaderPanel.Font")));
            this.gvProjectCategory.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvProjectCategory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProCategoryId,
            this.colProjectCategoryName,
            this.colITRGroup});
            this.gvProjectCategory.GridControl = this.gcProjectCategory;
            this.gvProjectCategory.Name = "gvProjectCategory";
            this.gvProjectCategory.OptionsBehavior.Editable = false;
            this.gvProjectCategory.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvProjectCategory.OptionsView.ShowGroupPanel = false;
            this.gvProjectCategory.DoubleClick += new System.EventHandler(this.gvProjectCategory_DoubleClick);
            this.gvProjectCategory.RowCountChanged += new System.EventHandler(this.gvProjectCategory_RowCountChanged);
            // 
            // colProCategoryId
            // 
            resources.ApplyResources(this.colProCategoryId, "colProCategoryId");
            this.colProCategoryId.FieldName = "PROJECT_CATOGORY_ID";
            this.colProCategoryId.Name = "colProCategoryId";
            // 
            // colProjectCategoryName
            // 
            this.colProjectCategoryName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colProjectCategoryName.AppearanceHeader.Font")));
            this.colProjectCategoryName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colProjectCategoryName, "colProjectCategoryName");
            this.colProjectCategoryName.FieldName = "PROJECT_CATOGORY_NAME";
            this.colProjectCategoryName.Name = "colProjectCategoryName";
            this.colProjectCategoryName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colITRGroup
            // 
            resources.ApplyResources(this.colITRGroup, "colITRGroup");
            this.colITRGroup.FieldName = "PROJECT_CATOGORY_ITRGROUP";
            this.colITRGroup.Name = "colITRGroup";
            this.colITRGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlfooter
            // 
            this.pnlfooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfooter.Controls.Add(this.lblRecordCount);
            this.pnlfooter.Controls.Add(this.lblProjectCategory);
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
            // lblProjectCategory
            // 
            resources.ApplyResources(this.lblProjectCategory, "lblProjectCategory");
            this.lblProjectCategory.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblProjectCategory.Appearance.Font")));
            this.lblProjectCategory.Name = "lblProjectCategory";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // frmProjectCategoryView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlfill);
            this.Controls.Add(this.pnlfooter);
            this.Controls.Add(this.pnltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProjectCategoryView";
            this.ShowFilterClicked += new System.EventHandler(this.frmProjectCategoryView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmProjectCategoryView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmProjectCategoryView_Activated);
            this.Load += new System.EventHandler(this.ProjectCategoryView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).EndInit();
            this.pnltop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).EndInit();
            this.pnlfill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnltop;
        private Bosco.Utility.Controls.ucToolBar ucToolBarProjectCategory;
        private DevExpress.XtraEditors.PanelControl pnlfill;
        private DevExpress.XtraGrid.GridControl gcProjectCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProjectCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colProCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCategoryName;
        private DevExpress.XtraEditors.PanelControl pnlfooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblProjectCategory;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colITRGroup;
    }
}