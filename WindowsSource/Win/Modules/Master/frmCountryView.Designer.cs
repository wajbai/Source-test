namespace ACPP.Modules.Master
{
    partial class frmCountryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCountryView));
            this.pnlcountrytoolbar = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarCountryView = new Bosco.Utility.Controls.ucToolBar();
            this.gcCountry = new DevExpress.XtraGrid.GridControl();
            this.gvCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountryCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencySymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlCountryfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblCountryRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.pnlfill = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlcountrytoolbar)).BeginInit();
            this.pnlcountrytoolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCountryfooter)).BeginInit();
            this.pnlCountryfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).BeginInit();
            this.pnlfill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlcountrytoolbar
            // 
            this.pnlcountrytoolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlcountrytoolbar.Controls.Add(this.ucToolBarCountryView);
            resources.ApplyResources(this.pnlcountrytoolbar, "pnlcountrytoolbar");
            this.pnlcountrytoolbar.Name = "pnlcountrytoolbar";
            // 
            // ucToolBarCountryView
            // 
            this.ucToolBarCountryView.ChangeAddCaption = "&Add";
            this.ucToolBarCountryView.ChangeCaption = "&Edit";
            this.ucToolBarCountryView.ChangeDeleteCaption = "&Delete";
            this.ucToolBarCountryView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarCountryView.ChangePrintCaption = "&Print";
            this.ucToolBarCountryView.DisableAddButton = true;
            this.ucToolBarCountryView.DisableCloseButton = true;
            this.ucToolBarCountryView.DisableDeleteButton = true;
            this.ucToolBarCountryView.DisableDownloadExcel = true;
            this.ucToolBarCountryView.DisableEditButton = true;
            this.ucToolBarCountryView.DisableMoveTransaction = true;
            this.ucToolBarCountryView.DisableNatureofPayments = true;
            this.ucToolBarCountryView.DisablePrintButton = true;
            this.ucToolBarCountryView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarCountryView, "ucToolBarCountryView");
            this.ucToolBarCountryView.Name = "ucToolBarCountryView";
            this.ucToolBarCountryView.ShowHTML = true;
            this.ucToolBarCountryView.ShowMMT = true;
            this.ucToolBarCountryView.ShowPDF = true;
            this.ucToolBarCountryView.ShowRTF = true;
            this.ucToolBarCountryView.ShowText = true;
            this.ucToolBarCountryView.ShowXLS = true;
            this.ucToolBarCountryView.ShowXLSX = true;
            this.ucToolBarCountryView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarCountryView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarCountryView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarCountryView.AddClicked += new System.EventHandler(this.ucToolBarCountryView_AddClicked);
            this.ucToolBarCountryView.EditClicked += new System.EventHandler(this.ucToolBarCountryView_EditClicked);
            this.ucToolBarCountryView.DeleteClicked += new System.EventHandler(this.ucToolBarCountryView_DeleteClicked);
            this.ucToolBarCountryView.PrintClicked += new System.EventHandler(this.ucToolBarCountryView_PrintClicked);
            this.ucToolBarCountryView.CloseClicked += new System.EventHandler(this.ucToolBarCountryView_CloseClicked);
            this.ucToolBarCountryView.RefreshClicked += new System.EventHandler(this.ucToolBarCountryView_RefreshClicked);
            // 
            // gcCountry
            // 
            resources.ApplyResources(this.gcCountry, "gcCountry");
            this.gcCountry.MainView = this.gvCountry;
            this.gcCountry.Name = "gcCountry";
            this.gcCountry.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCountry});
            this.gcCountry.DoubleClick += new System.EventHandler(this.gcCountry_DoubleClick);
            // 
            // gvCountry
            // 
            this.gvCountry.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCountry.Appearance.FocusedRow.Font")));
            this.gvCountry.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCountry.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCountry.Appearance.HeaderPanel.Font")));
            this.gvCountry.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvCountry.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCountry.AppearancePrint.HeaderPanel.Font")));
            this.gvCountry.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvCountry.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvCountry.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCountryId,
            this.colCountryCode,
            this.colCountry,
            this.colCurrencyCode,
            this.colCurrencySymbol,
            this.colCurrencyName});
            this.gvCountry.GridControl = this.gcCountry;
            this.gvCountry.Name = "gvCountry";
            this.gvCountry.OptionsBehavior.Editable = false;
            this.gvCountry.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCountry.OptionsView.ShowGroupPanel = false;
            this.gvCountry.RowCountChanged += new System.EventHandler(this.gvCountry_RowCountChanged);
            // 
            // colCountryId
            // 
            this.colCountryId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCountryId.AppearanceHeader.Font")));
            this.colCountryId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCountryId, "colCountryId");
            this.colCountryId.FieldName = "COUNTRY_ID";
            this.colCountryId.Name = "colCountryId";
            // 
            // colCountryCode
            // 
            this.colCountryCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCountryCode.AppearanceHeader.Font")));
            this.colCountryCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCountryCode, "colCountryCode");
            this.colCountryCode.FieldName = "COUNTRY_CODE";
            this.colCountryCode.Name = "colCountryCode";
            this.colCountryCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCountry
            // 
            this.colCountry.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCountry.AppearanceHeader.Font")));
            this.colCountry.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCountry, "colCountry");
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            this.colCountry.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCurrencyCode
            // 
            this.colCurrencyCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCurrencyCode.AppearanceHeader.Font")));
            this.colCurrencyCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCurrencyCode, "colCurrencyCode");
            this.colCurrencyCode.FieldName = "CURRENCY_CODE";
            this.colCurrencyCode.Name = "colCurrencyCode";
            this.colCurrencyCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCurrencySymbol
            // 
            this.colCurrencySymbol.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCurrencySymbol.AppearanceHeader.Font")));
            this.colCurrencySymbol.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCurrencySymbol, "colCurrencySymbol");
            this.colCurrencySymbol.FieldName = "CURRENCY_SYMBOL";
            this.colCurrencySymbol.Name = "colCurrencySymbol";
            this.colCurrencySymbol.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCurrencyName.AppearanceHeader.Font")));
            this.colCurrencyName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCurrencyName, "colCurrencyName");
            this.colCurrencyName.FieldName = "CURRENCY_NAME";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlCountryfooter
            // 
            this.pnlCountryfooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCountryfooter.Controls.Add(this.lblRecordCount);
            this.pnlCountryfooter.Controls.Add(this.lblCountryRecordCount);
            this.pnlCountryfooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlCountryfooter, "pnlCountryfooter");
            this.pnlCountryfooter.Name = "pnlCountryfooter";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblCountryRecordCount
            // 
            resources.ApplyResources(this.lblCountryRecordCount, "lblCountryRecordCount");
            this.lblCountryRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCountryRecordCount.Appearance.Font")));
            this.lblCountryRecordCount.Name = "lblCountryRecordCount";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // pnlfill
            // 
            this.pnlfill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfill.Controls.Add(this.gcCountry);
            resources.ApplyResources(this.pnlfill, "pnlfill");
            this.pnlfill.Name = "pnlfill";
            // 
            // frmCountryView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlfill);
            this.Controls.Add(this.pnlCountryfooter);
            this.Controls.Add(this.pnlcountrytoolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCountryView";
            this.ShowFilterClicked += new System.EventHandler(this.frmCountryView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmCountryView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmCountryView_Activated);
            this.Load += new System.EventHandler(this.frmCountryView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlcountrytoolbar)).EndInit();
            this.pnlcountrytoolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCountryfooter)).EndInit();
            this.pnlCountryfooter.ResumeLayout(false);
            this.pnlCountryfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfill)).EndInit();
            this.pnlfill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlcountrytoolbar;
        private Bosco.Utility.Controls.ucToolBar ucToolBarCountryView;
        private DevExpress.XtraGrid.GridControl gcCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencySymbol;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryId;
        private DevExpress.XtraEditors.PanelControl pnlCountryfooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblCountryRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraEditors.PanelControl pnlfill;
    }
}