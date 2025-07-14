namespace ACPP.Modules.Inventory
{
    partial class frmBlockView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBlockView));
            this.pnltop = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarBlock = new ACPP.Modules.UIControls.ucToolBar();
            this.pnlfill = new DevExpress.XtraEditors.PanelControl();
            this.gcBlock = new DevExpress.XtraGrid.GridControl();
            this.gvBlock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBlockId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBlock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblProjectCategory = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).BeginInit();
            this.pnltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).BeginInit();
            this.pnlfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).BeginInit();
            this.pnlfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnltop
            // 
            this.pnltop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnltop.Controls.Add(this.ucToolBarBlock);
            resources.ApplyResources(this.pnltop, "pnltop");
            this.pnltop.Name = "pnltop";
            // 
            // ucToolBarBlock
            // 
            this.ucToolBarBlock.ChangeAddCaption = "&Add";
            this.ucToolBarBlock.ChangeCaption = "&Edit";
            this.ucToolBarBlock.ChangeDeleteCaption = "&Delete";
            this.ucToolBarBlock.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarBlock.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBarBlock.ChangePrintCaption = "&Print";
            this.ucToolBarBlock.DisableAddButton = true;
            this.ucToolBarBlock.DisableCloseButton = true;
            this.ucToolBarBlock.DisableDeleteButton = true;
            this.ucToolBarBlock.DisableDownloadExcel = true;
            this.ucToolBarBlock.DisableEditButton = true;
            this.ucToolBarBlock.DisableMoveTransaction = true;
            this.ucToolBarBlock.DisableNatureofPayments = true;
            this.ucToolBarBlock.DisablePostInterest = true;
            this.ucToolBarBlock.DisablePrintButton = true;
            this.ucToolBarBlock.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarBlock, "ucToolBarBlock");
            this.ucToolBarBlock.Name = "ucToolBarBlock";
            this.ucToolBarBlock.ShowHTML = true;
            this.ucToolBarBlock.ShowMMT = true;
            this.ucToolBarBlock.ShowPDF = true;
            this.ucToolBarBlock.ShowRTF = true;
            this.ucToolBarBlock.ShowText = true;
            this.ucToolBarBlock.ShowXLS = true;
            this.ucToolBarBlock.ShowXLSX = true;
            this.ucToolBarBlock.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBlock.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBlock.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBlock.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBlock.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBlock.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBlock.AddClicked += new System.EventHandler(this.ucToolBarBlock_AddClicked);
            this.ucToolBarBlock.EditClicked += new System.EventHandler(this.ucToolBarBlock_EditClicked);
            this.ucToolBarBlock.DeleteClicked += new System.EventHandler(this.ucToolBarBlock_DeleteClicked);
            this.ucToolBarBlock.PrintClicked += new System.EventHandler(this.ucToolBarBlock_PrintClicked);
            this.ucToolBarBlock.CloseClicked += new System.EventHandler(this.ucToolBarBlock_CloseClicked);
            this.ucToolBarBlock.RefreshClicked += new System.EventHandler(this.ucToolBarBlock_RefreshClicked);
            // 
            // pnlfill
            // 
            this.pnlfill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfill.Controls.Add(this.gcBlock);
            resources.ApplyResources(this.pnlfill, "pnlfill");
            this.pnlfill.Name = "pnlfill";
            // 
            // gcBlock
            // 
            resources.ApplyResources(this.gcBlock, "gcBlock");
            this.gcBlock.MainView = this.gvBlock;
            this.gcBlock.Name = "gcBlock";
            this.gcBlock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBlock});
            // 
            // gvBlock
            // 
            this.gvBlock.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvBlock.Appearance.FocusedRow.Font")));
            this.gvBlock.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBlock.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBlock.Appearance.HeaderPanel.Font")));
            this.gvBlock.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBlock.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBlock.AppearancePrint.HeaderPanel.Font")));
            this.gvBlock.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvBlock.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBlockId,
            this.colBlock});
            this.gvBlock.GridControl = this.gcBlock;
            this.gvBlock.Name = "gvBlock";
            this.gvBlock.OptionsBehavior.Editable = false;
            this.gvBlock.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBlock.OptionsView.ShowGroupPanel = false;
            this.gvBlock.DoubleClick += new System.EventHandler(this.gvBlock_DoubleClick);
            this.gvBlock.RowCountChanged += new System.EventHandler(this.gvProjectCategory_RowCountChanged);
            // 
            // colBlockId
            // 
            resources.ApplyResources(this.colBlockId, "colBlockId");
            this.colBlockId.FieldName = "BLOCK_ID";
            this.colBlockId.Name = "colBlockId";
            // 
            // colBlock
            // 
            this.colBlock.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBlock.AppearanceHeader.Font")));
            this.colBlock.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBlock, "colBlock");
            this.colBlock.FieldName = "BLOCK";
            this.colBlock.Name = "colBlock";
            this.colBlock.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
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
            // frmBlockView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlfill);
            this.Controls.Add(this.pnlfooter);
            this.Controls.Add(this.pnltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBlockView";
            this.Activated += new System.EventHandler(this.frmBlockView_Activated);
            this.Load += new System.EventHandler(this.frmBlockView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).EndInit();
            this.pnltop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).EndInit();
            this.pnlfill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnltop;
        private UIControls.ucToolBar ucToolBarBlock;
        private DevExpress.XtraEditors.PanelControl pnlfill;
        private DevExpress.XtraGrid.GridControl gcBlock;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBlock;
        private DevExpress.XtraGrid.Columns.GridColumn colBlockId;
        private DevExpress.XtraGrid.Columns.GridColumn colBlock;
        private DevExpress.XtraEditors.PanelControl pnlfooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblProjectCategory;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
    }
}