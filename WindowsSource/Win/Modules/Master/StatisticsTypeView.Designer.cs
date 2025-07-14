namespace ACPP.Modules.Master
{
    partial class StatisticsTypeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsTypeView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.pnlStatostocsType = new DevExpress.XtraEditors.PanelControl();
            this.ucStatisticsTypeToolBar = new Bosco.Utility.Controls.ucToolBar();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblPurpose = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pnlPurposefill = new DevExpress.XtraEditors.PanelControl();
            this.gcStatisticsTypeView = new DevExpress.XtraGrid.GridControl();
            this.gvStatisticsTypeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStatisticsTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatisticsType = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatostocsType)).BeginInit();
            this.pnlStatostocsType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPurposefill)).BeginInit();
            this.pnlPurposefill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStatisticsTypeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStatisticsTypeView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlStatostocsType
            // 
            this.pnlStatostocsType.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlStatostocsType.Controls.Add(this.ucStatisticsTypeToolBar);
            resources.ApplyResources(this.pnlStatostocsType, "pnlStatostocsType");
            this.pnlStatostocsType.Name = "pnlStatostocsType";
            // 
            // ucStatisticsTypeToolBar
            // 
            this.ucStatisticsTypeToolBar.ChangeAddCaption = "&Add";
            this.ucStatisticsTypeToolBar.ChangeCaption = "&Edit";
            this.ucStatisticsTypeToolBar.ChangeDeleteCaption = "&Delete";
            this.ucStatisticsTypeToolBar.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucStatisticsTypeToolBar.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucStatisticsTypeToolBar.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucStatisticsTypeToolBar.ChangePostInterestCaption = "P&ost Interest";
            this.ucStatisticsTypeToolBar.ChangePrintCaption = "&Print";
            this.ucStatisticsTypeToolBar.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucStatisticsTypeToolBar.DisableAddButton = true;
            this.ucStatisticsTypeToolBar.DisableAMCRenew = true;
            this.ucStatisticsTypeToolBar.DisableCloseButton = true;
            this.ucStatisticsTypeToolBar.DisableDeleteButton = true;
            this.ucStatisticsTypeToolBar.DisableDownloadExcel = true;
            this.ucStatisticsTypeToolBar.DisableEditButton = true;
            this.ucStatisticsTypeToolBar.DisableInsertVoucher = true;
            this.ucStatisticsTypeToolBar.DisableMoveTransaction = true;
            this.ucStatisticsTypeToolBar.DisableNatureofPayments = true;
            this.ucStatisticsTypeToolBar.DisablePostInterest = true;
            this.ucStatisticsTypeToolBar.DisablePrintButton = true;
            this.ucStatisticsTypeToolBar.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucStatisticsTypeToolBar, "ucStatisticsTypeToolBar");
            this.ucStatisticsTypeToolBar.Name = "ucStatisticsTypeToolBar";
            this.ucStatisticsTypeToolBar.ShowHTML = true;
            this.ucStatisticsTypeToolBar.ShowMMT = true;
            this.ucStatisticsTypeToolBar.ShowPDF = true;
            this.ucStatisticsTypeToolBar.ShowRTF = true;
            this.ucStatisticsTypeToolBar.ShowText = true;
            this.ucStatisticsTypeToolBar.ShowXLS = true;
            this.ucStatisticsTypeToolBar.ShowXLSX = true;
            this.ucStatisticsTypeToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStatisticsTypeToolBar.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStatisticsTypeToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStatisticsTypeToolBar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStatisticsTypeToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStatisticsTypeToolBar.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucStatisticsTypeToolBar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucStatisticsTypeToolBar.AddClicked += new System.EventHandler(this.ucStatisticsTypeToolBar_AddClicked);
            this.ucStatisticsTypeToolBar.EditClicked += new System.EventHandler(this.ucStatisticsTypeToolBar_EditClicked);
            this.ucStatisticsTypeToolBar.DeleteClicked += new System.EventHandler(this.ucStatisticsTypeToolBar_DeleteClicked);
            this.ucStatisticsTypeToolBar.PrintClicked += new System.EventHandler(this.ucStatisticsTypeToolBar_PrintClicked);
            this.ucStatisticsTypeToolBar.CloseClicked += new System.EventHandler(this.ucStatisticsTypeToolBar_CloseClicked);
            this.ucStatisticsTypeToolBar.RefreshClicked += new System.EventHandler(this.ucStatisticsTypeToolBar_RefreshClicked);
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblPurpose
            // 
            resources.ApplyResources(this.lblPurpose, "lblPurpose");
            this.lblPurpose.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblPurpose.Appearance.Font")));
            this.lblPurpose.Name = "lblPurpose";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.chkShowFilter);
            this.panelControl2.Controls.Add(this.lblRecordCount);
            this.panelControl2.Controls.Add(this.lblPurpose);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // pnlPurposefill
            // 
            this.pnlPurposefill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlPurposefill.Controls.Add(this.gcStatisticsTypeView);
            resources.ApplyResources(this.pnlPurposefill, "pnlPurposefill");
            this.pnlPurposefill.Name = "pnlPurposefill";
            // 
            // gcStatisticsTypeView
            // 
            resources.ApplyResources(this.gcStatisticsTypeView, "gcStatisticsTypeView");
            this.gcStatisticsTypeView.MainView = this.gvStatisticsTypeView;
            this.gcStatisticsTypeView.Name = "gcStatisticsTypeView";
            this.gcStatisticsTypeView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStatisticsTypeView});
            this.gcStatisticsTypeView.DoubleClick += new System.EventHandler(this.gcStatisticsTypeView_DoubleClick);
            // 
            // gvStatisticsTypeView
            // 
            this.gvStatisticsTypeView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvStatisticsTypeView.Appearance.FocusedRow.Font")));
            this.gvStatisticsTypeView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvStatisticsTypeView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvStatisticsTypeView.Appearance.HeaderPanel.Font")));
            this.gvStatisticsTypeView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvStatisticsTypeView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvStatisticsTypeView.AppearancePrint.HeaderPanel.Font")));
            this.gvStatisticsTypeView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvStatisticsTypeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatisticsTypeId,
            this.colStatisticsType});
            this.gvStatisticsTypeView.GridControl = this.gcStatisticsTypeView;
            this.gvStatisticsTypeView.Name = "gvStatisticsTypeView";
            this.gvStatisticsTypeView.OptionsBehavior.Editable = false;
            this.gvStatisticsTypeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvStatisticsTypeView.OptionsView.ShowGroupPanel = false;
            this.gvStatisticsTypeView.RowCountChanged += new System.EventHandler(this.gvStatisticsTypeView_RowCountChanged);
            // 
            // colStatisticsTypeId
            // 
            this.colStatisticsTypeId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStatisticsTypeId.AppearanceHeader.Font")));
            this.colStatisticsTypeId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStatisticsTypeId, "colStatisticsTypeId");
            this.colStatisticsTypeId.FieldName = "STATISTICS_TYPE_ID";
            this.colStatisticsTypeId.Name = "colStatisticsTypeId";
            this.colStatisticsTypeId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStatisticsType
            // 
            this.colStatisticsType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStatisticsType.AppearanceHeader.Font")));
            this.colStatisticsType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStatisticsType, "colStatisticsType");
            this.colStatisticsType.FieldName = "STATISTICS_TYPE";
            this.colStatisticsType.Name = "colStatisticsType";
            this.colStatisticsType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // StatisticsTypeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPurposefill);
            this.Controls.Add(this.pnlStatostocsType);
            this.Controls.Add(this.panelControl2);
            this.Name = "StatisticsTypeView";
            this.Activated += new System.EventHandler(this.StatisticsTypeView_Activated);
            this.Load += new System.EventHandler(this.StatisticsTypeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatostocsType)).EndInit();
            this.pnlStatostocsType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPurposefill)).EndInit();
            this.pnlPurposefill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStatisticsTypeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStatisticsTypeView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlStatostocsType;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl lblPurpose;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Bosco.Utility.Controls.ucToolBar ucStatisticsTypeToolBar;
        private DevExpress.XtraEditors.PanelControl pnlPurposefill;
        private DevExpress.XtraGrid.GridControl gcStatisticsTypeView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStatisticsTypeView;
        private DevExpress.XtraGrid.Columns.GridColumn colStatisticsTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colStatisticsType;
    }
}