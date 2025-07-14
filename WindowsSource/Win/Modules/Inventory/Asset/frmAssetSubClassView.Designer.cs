namespace ACPP.Modules.Inventory
{
    partial class frmAssetSubClassView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetSubClassView));
            this.pnltop = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarAssetSubcls = new Bosco.Utility.Controls.ucToolBar();
            this.pnlfill = new DevExpress.XtraEditors.PanelControl();
            this.gcSubClass = new DevExpress.XtraGrid.GridControl();
            this.gvAssetSubcls = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAssetClassID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepreMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeprePercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblProjectCategory = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.colParentClassId = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).BeginInit();
            this.pnltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).BeginInit();
            this.pnlfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSubClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetSubcls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).BeginInit();
            this.pnlfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnltop
            // 
            this.pnltop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnltop.Controls.Add(this.ucToolBarAssetSubcls);
            resources.ApplyResources(this.pnltop, "pnltop");
            this.pnltop.Name = "pnltop";
            // 
            // ucToolBarAssetSubcls
            // 
            this.ucToolBarAssetSubcls.ChangeAddCaption = "&Add";
            this.ucToolBarAssetSubcls.ChangeCaption = "&Edit";
            this.ucToolBarAssetSubcls.ChangeDeleteCaption = "&Delete";
            this.ucToolBarAssetSubcls.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarAssetSubcls.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarAssetSubcls.ChangePrintCaption = "&Print";
            this.ucToolBarAssetSubcls.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarAssetSubcls.DisableAddButton = true;
            this.ucToolBarAssetSubcls.DisableAMCRenew = true;
            this.ucToolBarAssetSubcls.DisableCloseButton = true;
            this.ucToolBarAssetSubcls.DisableDeleteButton = true;
            this.ucToolBarAssetSubcls.DisableDownloadExcel = true;
            this.ucToolBarAssetSubcls.DisableEditButton = true;
            this.ucToolBarAssetSubcls.DisableMoveTransaction = true;
            this.ucToolBarAssetSubcls.DisableNatureofPayments = true;
            this.ucToolBarAssetSubcls.DisablePostInterest = true;
            this.ucToolBarAssetSubcls.DisablePrintButton = true;
            this.ucToolBarAssetSubcls.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarAssetSubcls, "ucToolBarAssetSubcls");
            this.ucToolBarAssetSubcls.Name = "ucToolBarAssetSubcls";
            this.ucToolBarAssetSubcls.ShowHTML = true;
            this.ucToolBarAssetSubcls.ShowMMT = true;
            this.ucToolBarAssetSubcls.ShowPDF = true;
            this.ucToolBarAssetSubcls.ShowRTF = true;
            this.ucToolBarAssetSubcls.ShowText = true;
            this.ucToolBarAssetSubcls.ShowXLS = true;
            this.ucToolBarAssetSubcls.ShowXLSX = true;
            this.ucToolBarAssetSubcls.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAssetSubcls.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAssetSubcls.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAssetSubcls.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAssetSubcls.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAssetSubcls.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAssetSubcls.AddClicked += new System.EventHandler(this.ucToolBarAssetSubcls_AddClicked);
            this.ucToolBarAssetSubcls.EditClicked += new System.EventHandler(this.ucToolBarAssetSubcls_EditClicked);
            this.ucToolBarAssetSubcls.DeleteClicked += new System.EventHandler(this.ucToolBarAssetSubcls_DeleteClicked);
            this.ucToolBarAssetSubcls.CloseClicked += new System.EventHandler(this.ucToolBarAssetSubcls_CloseClicked);
            this.ucToolBarAssetSubcls.RefreshClicked += new System.EventHandler(this.ucToolBarAssetSubcls_RefreshClicked);
            // 
            // pnlfill
            // 
            this.pnlfill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfill.Controls.Add(this.gcSubClass);
            resources.ApplyResources(this.pnlfill, "pnlfill");
            this.pnlfill.Name = "pnlfill";
            // 
            // gcSubClass
            // 
            resources.ApplyResources(this.gcSubClass, "gcSubClass");
            this.gcSubClass.MainView = this.gvAssetSubcls;
            this.gcSubClass.Name = "gcSubClass";
            this.gcSubClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAssetSubcls});
            this.gcSubClass.DoubleClick += new System.EventHandler(this.gvAssetSubcls_DoubleClick);
            // 
            // gvAssetSubcls
            // 
            this.gvAssetSubcls.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetSubcls.Appearance.FocusedRow.Font")));
            this.gvAssetSubcls.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAssetSubcls.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetSubcls.Appearance.HeaderPanel.Font")));
            this.gvAssetSubcls.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAssetSubcls.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAssetSubcls.AppearancePrint.HeaderPanel.Font")));
            this.gvAssetSubcls.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvAssetSubcls.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAssetClassID,
            this.colAssetClass,
            this.colDepreMethod,
            this.colDeprePercentage,
            this.colParentClassId});
            this.gvAssetSubcls.GridControl = this.gcSubClass;
            this.gvAssetSubcls.Name = "gvAssetSubcls";
            this.gvAssetSubcls.OptionsBehavior.Editable = false;
            this.gvAssetSubcls.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAssetSubcls.OptionsView.ShowGroupPanel = false;
            this.gvAssetSubcls.DoubleClick += new System.EventHandler(this.gvAssetSubcls_DoubleClick);
            this.gvAssetSubcls.RowCountChanged += new System.EventHandler(this.gvProjectCategory_RowCountChanged);
            // 
            // colAssetClassID
            // 
            resources.ApplyResources(this.colAssetClassID, "colAssetClassID");
            this.colAssetClassID.FieldName = "ASSET_CLASS_ID";
            this.colAssetClassID.Name = "colAssetClassID";
            // 
            // colAssetClass
            // 
            this.colAssetClass.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAssetClass.AppearanceHeader.Font")));
            this.colAssetClass.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAssetClass, "colAssetClass");
            this.colAssetClass.FieldName = "ASSET_CLASS";
            this.colAssetClass.Name = "colAssetClass";
            this.colAssetClass.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDepreMethod
            // 
            resources.ApplyResources(this.colDepreMethod, "colDepreMethod");
            this.colDepreMethod.FieldName = "DEP_METHOD";
            this.colDepreMethod.Name = "colDepreMethod";
            // 
            // colDeprePercentage
            // 
            resources.ApplyResources(this.colDeprePercentage, "colDeprePercentage");
            this.colDeprePercentage.FieldName = "DEP_PERCENTAGE";
            this.colDeprePercentage.Name = "colDeprePercentage";
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
            // colParentClassId
            // 
            resources.ApplyResources(this.colParentClassId, "colParentClassId");
            this.colParentClassId.FieldName = "PARENT_CLASS_ID";
            this.colParentClassId.Name = "colParentClassId";
            // 
            // frmAssetSubClassView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlfill);
            this.Controls.Add(this.pnlfooter);
            this.Controls.Add(this.pnltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAssetSubClassView";
            this.EnterClicked += new System.EventHandler(this.frmAssetSubClassView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmAssetSubClassView_Activated);
            this.Load += new System.EventHandler(this.frmAssetSubClassView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).EndInit();
            this.pnltop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).EndInit();
            this.pnlfill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSubClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAssetSubcls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnltop;
        private Bosco.Utility.Controls.ucToolBar ucToolBarAssetSubcls;
        private DevExpress.XtraEditors.PanelControl pnlfill;
        private DevExpress.XtraGrid.GridControl gcSubClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAssetSubcls;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClassID;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClass;
        private DevExpress.XtraEditors.PanelControl pnlfooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblProjectCategory;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colDepreMethod;
        private DevExpress.XtraGrid.Columns.GridColumn colDeprePercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colParentClassId;
    }
}