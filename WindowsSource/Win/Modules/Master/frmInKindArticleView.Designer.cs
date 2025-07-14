namespace ACPP.Modules.Master
{
    partial class frmInKindArticleView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInKindArticleView));
            this.pnlToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarInKindArticle = new Bosco.Utility.Controls.ucToolBar();
            this.pnlInKindArticle = new DevExpress.XtraEditors.PanelControl();
            this.gcInKindArticle = new DevExpress.XtraGrid.GridControl();
            this.gvInKindArticle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colArticleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAbbrevation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArticle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOPQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblInKindArticleRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).BeginInit();
            this.pnlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInKindArticle)).BeginInit();
            this.pnlInKindArticle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcInKindArticle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInKindArticle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolBar.Controls.Add(this.ucToolBarInKindArticle);
            resources.ApplyResources(this.pnlToolBar, "pnlToolBar");
            this.pnlToolBar.Name = "pnlToolBar";
            // 
            // ucToolBarInKindArticle
            // 
            this.ucToolBarInKindArticle.ChangeAddCaption = "&Add";
            this.ucToolBarInKindArticle.ChangeCaption = "&Edit";
            this.ucToolBarInKindArticle.ChangeDeleteCaption = "&Delete";
            this.ucToolBarInKindArticle.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarInKindArticle.ChangePrintCaption = "&Print";
            this.ucToolBarInKindArticle.DisableAddButton = true;
            this.ucToolBarInKindArticle.DisableCloseButton = true;
            this.ucToolBarInKindArticle.DisableDeleteButton = false;
            this.ucToolBarInKindArticle.DisableDownloadExcel = true;
            this.ucToolBarInKindArticle.DisableEditButton = false;
            this.ucToolBarInKindArticle.DisableMoveTransaction = true;
            this.ucToolBarInKindArticle.DisableNatureofPayments = true;
            this.ucToolBarInKindArticle.DisablePrintButton = true;
            this.ucToolBarInKindArticle.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarInKindArticle, "ucToolBarInKindArticle");
            this.ucToolBarInKindArticle.Name = "ucToolBarInKindArticle";
            this.ucToolBarInKindArticle.ShowHTML = true;
            this.ucToolBarInKindArticle.ShowMMT = true;
            this.ucToolBarInKindArticle.ShowPDF = true;
            this.ucToolBarInKindArticle.ShowRTF = true;
            this.ucToolBarInKindArticle.ShowText = true;
            this.ucToolBarInKindArticle.ShowXLS = true;
            this.ucToolBarInKindArticle.ShowXLSX = true;
            this.ucToolBarInKindArticle.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarInKindArticle.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarInKindArticle.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarInKindArticle.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarInKindArticle.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarInKindArticle.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarInKindArticle.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarInKindArticle.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarInKindArticle.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarInKindArticle.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarInKindArticle.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarInKindArticle.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarInKindArticle.AddClicked += new System.EventHandler(this.ucToolBarInKindArticle_AddClicked);
            this.ucToolBarInKindArticle.EditClicked += new System.EventHandler(this.ucToolBarInKindArticle_EditClicked);
            this.ucToolBarInKindArticle.DeleteClicked += new System.EventHandler(this.ucToolBarInKindArticle_DeleteClicked);
            this.ucToolBarInKindArticle.PrintClicked += new System.EventHandler(this.ucToolBarInKindArticle_PrintClicked);
            this.ucToolBarInKindArticle.CloseClicked += new System.EventHandler(this.ucToolBarInKindArticle_CloseClicked);
            this.ucToolBarInKindArticle.RefreshClicked += new System.EventHandler(this.ucToolBarInKindArticle_RefreshClicked);
            // 
            // pnlInKindArticle
            // 
            this.pnlInKindArticle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlInKindArticle.Controls.Add(this.gcInKindArticle);
            this.pnlInKindArticle.Controls.Add(this.pnlFooter);
            resources.ApplyResources(this.pnlInKindArticle, "pnlInKindArticle");
            this.pnlInKindArticle.Name = "pnlInKindArticle";
            // 
            // gcInKindArticle
            // 
            resources.ApplyResources(this.gcInKindArticle, "gcInKindArticle");
            this.gcInKindArticle.MainView = this.gvInKindArticle;
            this.gcInKindArticle.Name = "gcInKindArticle";
            this.gcInKindArticle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInKindArticle});
            // 
            // gvInKindArticle
            // 
            this.gvInKindArticle.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvInKindArticle.Appearance.FocusedRow.Font")));
            this.gvInKindArticle.Appearance.FocusedRow.Options.UseFont = true;
            this.gvInKindArticle.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvInKindArticle.Appearance.HeaderPanel.Font")));
            this.gvInKindArticle.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvInKindArticle.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvInKindArticle.AppearancePrint.HeaderPanel.Font")));
            this.gvInKindArticle.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvInKindArticle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colArticleId,
            this.colAbbrevation,
            this.colArticle,
            this.colOPQuantity,
            this.colOpValue});
            this.gvInKindArticle.GridControl = this.gcInKindArticle;
            this.gvInKindArticle.Name = "gvInKindArticle";
            this.gvInKindArticle.OptionsBehavior.Editable = false;
            this.gvInKindArticle.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvInKindArticle.OptionsView.ShowGroupPanel = false;
            this.gvInKindArticle.DoubleClick += new System.EventHandler(this.gvInKindArticle_DoubleClick);
            this.gvInKindArticle.RowCountChanged += new System.EventHandler(this.gvInKindArticle_RowCountChanged);
            // 
            // colArticleId
            // 
            this.colArticleId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colArticleId.AppearanceHeader.Font")));
            this.colArticleId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colArticleId, "colArticleId");
            this.colArticleId.FieldName = "ARTICLE_ID";
            this.colArticleId.Name = "colArticleId";
            // 
            // colAbbrevation
            // 
            this.colAbbrevation.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAbbrevation.AppearanceHeader.Font")));
            this.colAbbrevation.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAbbrevation, "colAbbrevation");
            this.colAbbrevation.FieldName = "ABBREVATION";
            this.colAbbrevation.Name = "colAbbrevation";
            this.colAbbrevation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colArticle
            // 
            this.colArticle.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colArticle.AppearanceHeader.Font")));
            this.colArticle.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colArticle, "colArticle");
            this.colArticle.FieldName = "ARTICLE";
            this.colArticle.Name = "colArticle";
            this.colArticle.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colOPQuantity
            // 
            this.colOPQuantity.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colOPQuantity.AppearanceHeader.Font")));
            this.colOPQuantity.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colOPQuantity, "colOPQuantity");
            this.colOPQuantity.FieldName = "OP_QUANTITY";
            this.colOPQuantity.Name = "colOPQuantity";
            this.colOPQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colOpValue
            // 
            this.colOpValue.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colOpValue.AppearanceHeader.Font")));
            this.colOpValue.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colOpValue, "colOpValue");
            this.colOpValue.DisplayFormat.FormatString = "n";
            this.colOpValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOpValue.FieldName = "OP_VALUE";
            this.colOpValue.GroupFormat.FormatString = "n";
            this.colOpValue.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOpValue.Name = "colOpValue";
            this.colOpValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFooter.Controls.Add(this.lblInKindArticleRecordCount);
            this.pnlFooter.Controls.Add(this.lblRecordCount);
            this.pnlFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Name = "pnlFooter";
            // 
            // lblInKindArticleRecordCount
            // 
            resources.ApplyResources(this.lblInKindArticleRecordCount, "lblInKindArticleRecordCount");
            this.lblInKindArticleRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblInKindArticleRecordCount.Appearance.Font")));
            this.lblInKindArticleRecordCount.Name = "lblInKindArticleRecordCount";
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
            this.chkShowFilter.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("chkShowFilter.Properties.Appearance.Font")));
            this.chkShowFilter.Properties.Appearance.Options.UseFont = true;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // frmInKindArticleView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlInKindArticle);
            this.Controls.Add(this.pnlToolBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmInKindArticleView";
            this.ShowFilterClicked += new System.EventHandler(this.frmInKindArticleView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmInKindArticleView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmInKindArticleView_Activated);
            this.Load += new System.EventHandler(this.frmInKindArticleView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).EndInit();
            this.pnlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlInKindArticle)).EndInit();
            this.pnlInKindArticle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcInKindArticle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInKindArticle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlToolBar;
        private Bosco.Utility.Controls.ucToolBar ucToolBarInKindArticle;
        private DevExpress.XtraEditors.PanelControl pnlInKindArticle;
        private DevExpress.XtraGrid.GridControl gcInKindArticle;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInKindArticle;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraGrid.Columns.GridColumn colArticleId;
        private DevExpress.XtraGrid.Columns.GridColumn colAbbrevation;
        private DevExpress.XtraGrid.Columns.GridColumn colArticle;
        private DevExpress.XtraGrid.Columns.GridColumn colOPQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colOpValue;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblInKindArticleRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
    }
}