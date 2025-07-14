namespace ACPP.Modules.Master
{
    partial class frmAuditorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditorView));
            this.pnlToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucAuditorView = new Bosco.Utility.Controls.ucToolBar();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.pnlAuditor = new DevExpress.XtraEditors.PanelControl();
            this.gcAuditorDetails = new DevExpress.XtraGrid.GridControl();
            this.gvAuditorDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonAudId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.lblAuditorRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).BeginInit();
            this.pnlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditor)).BeginInit();
            this.pnlAuditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditorDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditorDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolBar.Controls.Add(this.ucAuditorView);
            resources.ApplyResources(this.pnlToolBar, "pnlToolBar");
            this.pnlToolBar.Name = "pnlToolBar";
            // 
            // ucAuditorView
            // 
            this.ucAuditorView.ChangeAddCaption = "&Add";
            this.ucAuditorView.ChangeCaption = "&Edit";
            this.ucAuditorView.ChangeDeleteCaption = "&Delete";
            this.ucAuditorView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAuditorView.ChangePrintCaption = "&Print";
            this.ucAuditorView.DisableAddButton = true;
            this.ucAuditorView.DisableCloseButton = true;
            this.ucAuditorView.DisableDeleteButton = true;
            this.ucAuditorView.DisableDownloadExcel = true;
            this.ucAuditorView.DisableEditButton = true;
            this.ucAuditorView.DisableMoveTransaction = true;
            this.ucAuditorView.DisableNatureofPayments = true;
            this.ucAuditorView.DisablePrintButton = true;
            this.ucAuditorView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucAuditorView, "ucAuditorView");
            this.ucAuditorView.Name = "ucAuditorView";
            this.ucAuditorView.ShowHTML = true;
            this.ucAuditorView.ShowMMT = true;
            this.ucAuditorView.ShowPDF = true;
            this.ucAuditorView.ShowRTF = true;
            this.ucAuditorView.ShowText = true;
            this.ucAuditorView.ShowXLS = true;
            this.ucAuditorView.ShowXLSX = true;
            this.ucAuditorView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditorView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAuditorView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAuditorView.AddClicked += new System.EventHandler(this.ucAuditorView_AddClicked);
            this.ucAuditorView.EditClicked += new System.EventHandler(this.ucAuditorView_EditClicked);
            this.ucAuditorView.DeleteClicked += new System.EventHandler(this.ucAuditorView_DeleteClicked);
            this.ucAuditorView.PrintClicked += new System.EventHandler(this.ucAuditorView_PrintClicked);
            this.ucAuditorView.CloseClicked += new System.EventHandler(this.ucAuditorView_CloseClicked);
            this.ucAuditorView.RefreshClicked += new System.EventHandler(this.ucAuditorView_RefreshClicked);
            // 
            // pnlFill
            // 
            this.pnlFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFill.Controls.Add(this.pnlAuditor);
            this.pnlFill.Controls.Add(this.pnlFooter);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // pnlAuditor
            // 
            this.pnlAuditor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlAuditor.Controls.Add(this.gcAuditorDetails);
            resources.ApplyResources(this.pnlAuditor, "pnlAuditor");
            this.pnlAuditor.Name = "pnlAuditor";
            // 
            // gcAuditorDetails
            // 
            resources.ApplyResources(this.gcAuditorDetails, "gcAuditorDetails");
            this.gcAuditorDetails.MainView = this.gvAuditorDetails;
            this.gcAuditorDetails.Name = "gcAuditorDetails";
            this.gcAuditorDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAuditorDetails});
            // 
            // gvAuditorDetails
            // 
            this.gvAuditorDetails.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditorDetails.Appearance.FocusedRow.Font")));
            this.gvAuditorDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAuditorDetails.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditorDetails.Appearance.HeaderPanel.Font")));
            this.gvAuditorDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAuditorDetails.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvAuditorDetails.AppearancePrint.HeaderPanel.Font")));
            this.gvAuditorDetails.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvAuditorDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvAuditorDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonAudId,
            this.colName,
            this.colPlace,
            this.colCompanyName,
            this.colState,
            this.colCountry,
            this.colPhone});
            this.gvAuditorDetails.GridControl = this.gcAuditorDetails;
            this.gvAuditorDetails.Name = "gvAuditorDetails";
            this.gvAuditorDetails.OptionsBehavior.Editable = false;
            this.gvAuditorDetails.OptionsCustomization.AllowGroup = false;
            this.gvAuditorDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAuditorDetails.OptionsView.ShowGroupPanel = false;
            this.gvAuditorDetails.DoubleClick += new System.EventHandler(this.gvAuditorDetails_DoubleClick);
            this.gvAuditorDetails.RowCountChanged += new System.EventHandler(this.gvAuditorDetails_RowCountChanged);
            // 
            // colDonAudId
            // 
            this.colDonAudId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDonAudId.AppearanceHeader.Font")));
            this.colDonAudId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDonAudId, "colDonAudId");
            this.colDonAudId.FieldName = "DONAUD_ID";
            this.colDonAudId.Name = "colDonAudId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colName.AppearanceHeader.Font")));
            this.colName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPlace
            // 
            this.colPlace.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPlace.AppearanceHeader.Font")));
            this.colPlace.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPlace, "colPlace");
            this.colPlace.FieldName = "PLACE";
            this.colPlace.Name = "colPlace";
            this.colPlace.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCompanyName
            // 
            resources.ApplyResources(this.colCompanyName, "colCompanyName");
            this.colCompanyName.FieldName = "COMPANY_NAME";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colState
            // 
            this.colState.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colState.AppearanceHeader.Font")));
            this.colState.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colState, "colState");
            this.colState.FieldName = "STATE";
            this.colState.Name = "colState";
            this.colState.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
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
            // colPhone
            // 
            this.colPhone.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPhone.AppearanceHeader.Font")));
            this.colPhone.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPhone, "colPhone");
            this.colPhone.FieldName = "PHONE";
            this.colPhone.Name = "colPhone";
            this.colPhone.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFooter.Controls.Add(this.chkShowFilter);
            this.pnlFooter.Controls.Add(this.lblAuditorRecordCount);
            this.pnlFooter.Controls.Add(this.lblRecordCount);
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Name = "pnlFooter";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // lblAuditorRecordCount
            // 
            resources.ApplyResources(this.lblAuditorRecordCount, "lblAuditorRecordCount");
            this.lblAuditorRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblAuditorRecordCount.Appearance.Font")));
            this.lblAuditorRecordCount.Name = "lblAuditorRecordCount";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // frmAuditorView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlToolBar);
            this.Name = "frmAuditorView";
            this.ShowFilterClicked += new System.EventHandler(this.frmAuditorView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmAuditorView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmAuditorView_Activated);
            this.Load += new System.EventHandler(this.frmAuditorView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolBar)).EndInit();
            this.pnlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlAuditor)).EndInit();
            this.pnlAuditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditorDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditorDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlToolBar;
        private Bosco.Utility.Controls.ucToolBar ucAuditorView;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraEditors.PanelControl pnlAuditor;
        private DevExpress.XtraGrid.GridControl gcAuditorDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAuditorDetails;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblAuditorRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colDonAudId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlace;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName;
    }
}