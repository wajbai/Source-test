namespace ACPP.Modules.Master
{
    partial class frmCostCentreCategoryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCostCentreCategoryView));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ucToolBarCostCentreCategory = new Bosco.Utility.Controls.ucToolBar();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.gcCostCentreCategory = new DevExpress.XtraGrid.GridControl();
            this.gvCostCentreCategory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCostCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCentrecategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblCostCentreCategory = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCentreCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentreCategory)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ucToolBarCostCentreCategory);
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            // 
            // ucToolBarCostCentreCategory
            // 
            this.ucToolBarCostCentreCategory.ChangeAddCaption = "&Add";
            this.ucToolBarCostCentreCategory.ChangeCaption = "&Edit";
            this.ucToolBarCostCentreCategory.ChangeDeleteCaption = "&Delete";
            this.ucToolBarCostCentreCategory.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarCostCentreCategory.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarCostCentreCategory.ChangePrintCaption = "&Print";
            this.ucToolBarCostCentreCategory.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarCostCentreCategory.DisableAddButton = true;
            this.ucToolBarCostCentreCategory.DisableAMCRenew = true;
            this.ucToolBarCostCentreCategory.DisableCloseButton = true;
            this.ucToolBarCostCentreCategory.DisableDeleteButton = true;
            this.ucToolBarCostCentreCategory.DisableDownloadExcel = true;
            this.ucToolBarCostCentreCategory.DisableEditButton = true;
            this.ucToolBarCostCentreCategory.DisableMoveTransaction = true;
            this.ucToolBarCostCentreCategory.DisableNatureofPayments = true;
            this.ucToolBarCostCentreCategory.DisablePostInterest = true;
            this.ucToolBarCostCentreCategory.DisablePrintButton = true;
            this.ucToolBarCostCentreCategory.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarCostCentreCategory, "ucToolBarCostCentreCategory");
            this.ucToolBarCostCentreCategory.Name = "ucToolBarCostCentreCategory";
            this.ucToolBarCostCentreCategory.ShowHTML = true;
            this.ucToolBarCostCentreCategory.ShowMMT = true;
            this.ucToolBarCostCentreCategory.ShowPDF = true;
            this.ucToolBarCostCentreCategory.ShowRTF = true;
            this.ucToolBarCostCentreCategory.ShowText = true;
            this.ucToolBarCostCentreCategory.ShowXLS = true;
            this.ucToolBarCostCentreCategory.ShowXLSX = true;
            this.ucToolBarCostCentreCategory.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarCostCentreCategory.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarCostCentreCategory.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCostCentreCategory.AddClicked += new System.EventHandler(this.ucToolBarCostCentreCategory_AddClicked);
            this.ucToolBarCostCentreCategory.EditClicked += new System.EventHandler(this.ucToolBarCostCentreCategory_EditClicked);
            this.ucToolBarCostCentreCategory.DeleteClicked += new System.EventHandler(this.ucToolBarCostCentreCategory_DeleteClicked);
            this.ucToolBarCostCentreCategory.PrintClicked += new System.EventHandler(this.ucToolBarCostCentreCategory_PrintClicked);
            this.ucToolBarCostCentreCategory.CloseClicked += new System.EventHandler(this.ucToolBarCostCentreCategory_CloseClicked);
            this.ucToolBarCostCentreCategory.RefreshClicked += new System.EventHandler(this.ucToolBarCostCentreCategory_RefreshClicked);
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.gcCostCentreCategory);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // gcCostCentreCategory
            // 
            resources.ApplyResources(this.gcCostCentreCategory, "gcCostCentreCategory");
            this.gcCostCentreCategory.MainView = this.gvCostCentreCategory;
            this.gcCostCentreCategory.Name = "gcCostCentreCategory";
            this.gcCostCentreCategory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCostCentreCategory});
            // 
            // gvCostCentreCategory
            // 
            this.gvCostCentreCategory.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCentreCategory.Appearance.FocusedRow.Font")));
            this.gvCostCentreCategory.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCostCentreCategory.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCentreCategory.Appearance.HeaderPanel.Font")));
            this.gvCostCentreCategory.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvCostCentreCategory.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvCostCentreCategory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCostCategoryId,
            this.colCostCentrecategoryName});
            this.gvCostCentreCategory.GridControl = this.gcCostCentreCategory;
            this.gvCostCentreCategory.Name = "gvCostCentreCategory";
            this.gvCostCentreCategory.OptionsBehavior.Editable = false;
            this.gvCostCentreCategory.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCostCentreCategory.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gvCostCentreCategory.OptionsView.ShowGroupPanel = false;
            this.gvCostCentreCategory.DoubleClick += new System.EventHandler(this.gvCostCentreCategory_DoubleClick);
            this.gvCostCentreCategory.RowCountChanged += new System.EventHandler(this.gvCostCentreCategory_RowCountChanged);
            // 
            // colCostCategoryId
            // 
            resources.ApplyResources(this.colCostCategoryId, "colCostCategoryId");
            this.colCostCategoryId.FieldName = "COST_CENTRECATEGORY_ID";
            this.colCostCategoryId.Name = "colCostCategoryId";
            this.colCostCategoryId.OptionsColumn.AllowEdit = false;
            this.colCostCategoryId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCostCentrecategoryName
            // 
            this.colCostCentrecategoryName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCostCentrecategoryName.AppearanceHeader.Font")));
            this.colCostCentrecategoryName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCostCentrecategoryName, "colCostCentrecategoryName");
            this.colCostCentrecategoryName.FieldName = "COST_CENTRE_CATEGORY_NAME";
            this.colCostCentrecategoryName.Name = "colCostCentrecategoryName";
            this.colCostCentrecategoryName.OptionsColumn.AllowEdit = false;
            this.colCostCentrecategoryName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblRecordCount);
            this.pnlFooter.Controls.Add(this.lblCostCentreCategory);
            this.pnlFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Name = "pnlFooter";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblCostCentreCategory
            // 
            resources.ApplyResources(this.lblCostCentreCategory, "lblCostCentreCategory");
            this.lblCostCentreCategory.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCostCentreCategory.Appearance.Font")));
            this.lblCostCentreCategory.Name = "lblCostCentreCategory";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // frmCostCentreCategoryView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCostCentreCategoryView";
            this.EnterClicked += new System.EventHandler(this.frmCostCentreCategoryView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmCostCentreCategoryView_Activated);
            this.Load += new System.EventHandler(this.frmCostCentreCategoryView_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCentreCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCentreCategory)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private Bosco.Utility.Controls.ucToolBar ucToolBarCostCentreCategory;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Panel pnlFooter;
        private DevExpress.XtraGrid.GridControl gcCostCentreCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCostCentreCategory;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentrecategoryName;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl lblCostCentreCategory;
    }
}