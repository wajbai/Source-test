namespace ACPP.Modules.Master
{
    partial class frmBankView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBankView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.pnlBankToolBar = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBarBankView = new Bosco.Utility.Controls.ucToolBar();
            this.pnlBankView = new DevExpress.XtraEditors.PanelControl();
            this.pnlGridControl = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcBankView = new DevExpress.XtraGrid.GridControl();
            this.gvBankView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBankID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAbbrevation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIFSCCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMICRCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSWIFTCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBankFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblBankRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.applicationSchema1 = new Bosco.DAO.Schema.ApplicationSchema();
            this.colBSRCode = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankToolBar)).BeginInit();
            this.pnlBankToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankView)).BeginInit();
            this.pnlBankView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridControl)).BeginInit();
            this.pnlGridControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBankView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBankView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankFooter)).BeginInit();
            this.pnlBankFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationSchema1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBankToolBar
            // 
            this.pnlBankToolBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankToolBar.Controls.Add(this.ucToolBarBankView);
            resources.ApplyResources(this.pnlBankToolBar, "pnlBankToolBar");
            this.pnlBankToolBar.Name = "pnlBankToolBar";
            // 
            // ucToolBarBankView
            // 
            this.ucToolBarBankView.ChangeAddCaption = "&Add";
            this.ucToolBarBankView.ChangeCaption = "&Edit";
            this.ucToolBarBankView.ChangeDeleteCaption = "&Delete";
            this.ucToolBarBankView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarBankView.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarBankView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarBankView.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBarBankView.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucToolBarBankView.ChangePrintCaption = "&Print";
            this.ucToolBarBankView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarBankView.DisableAddButton = true;
            this.ucToolBarBankView.DisableAMCRenew = true;
            this.ucToolBarBankView.DisableCloseButton = true;
            this.ucToolBarBankView.DisableDeleteButton = true;
            this.ucToolBarBankView.DisableDownloadExcel = true;
            this.ucToolBarBankView.DisableEditButton = true;
            this.ucToolBarBankView.DisableInsertVoucher = true;
            this.ucToolBarBankView.DisableMoveTransaction = true;
            this.ucToolBarBankView.DisableNatureofPayments = true;
            this.ucToolBarBankView.DisablePostInterest = true;
            this.ucToolBarBankView.DisablePrintButton = true;
            this.ucToolBarBankView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarBankView, "ucToolBarBankView");
            this.ucToolBarBankView.Name = "ucToolBarBankView";
            this.ucToolBarBankView.ShowHTML = true;
            this.ucToolBarBankView.ShowMMT = true;
            this.ucToolBarBankView.ShowPDF = true;
            this.ucToolBarBankView.ShowRTF = true;
            this.ucToolBarBankView.ShowText = true;
            this.ucToolBarBankView.ShowXLS = true;
            this.ucToolBarBankView.ShowXLSX = true;
            this.ucToolBarBankView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.AddClicked += new System.EventHandler(this.ucToolBarBankView_AddClicked);
            this.ucToolBarBankView.EditClicked += new System.EventHandler(this.ucToolBarBankView_EditClicked);
            this.ucToolBarBankView.DeleteClicked += new System.EventHandler(this.ucToolBarBankView_DeleteClicked);
            this.ucToolBarBankView.PrintClicked += new System.EventHandler(this.ucToolBarBankView_PrintClicked);
            this.ucToolBarBankView.CloseClicked += new System.EventHandler(this.ucToolBarBankView_CloseClicked);
            this.ucToolBarBankView.RefreshClicked += new System.EventHandler(this.ucToolBarBankView_RefreshClicked);
            // 
            // pnlBankView
            // 
            this.pnlBankView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankView.Controls.Add(this.pnlGridControl);
            resources.ApplyResources(this.pnlBankView, "pnlBankView");
            this.pnlBankView.Name = "pnlBankView";
            // 
            // pnlGridControl
            // 
            this.pnlGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlGridControl.Controls.Add(this.panelControl2);
            this.pnlGridControl.Controls.Add(this.pnlBankFooter);
            this.pnlGridControl.Controls.Add(this.pnlBankToolBar);
            resources.ApplyResources(this.pnlGridControl, "pnlGridControl");
            this.pnlGridControl.Name = "pnlGridControl";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gcBankView);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // gcBankView
            // 
            resources.ApplyResources(this.gcBankView, "gcBankView");
            this.gcBankView.MainView = this.gvBankView;
            this.gcBankView.Name = "gcBankView";
            this.gcBankView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBankView});
            // 
            // gvBankView
            // 
            this.gvBankView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvBankView.Appearance.FocusedRow.Font")));
            this.gvBankView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBankView.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvBankView.AppearancePrint.HeaderPanel.Font")));
            this.gvBankView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvBankView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvBankView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBankID,
            this.colAbbrevation,
            this.colBankName,
            this.colBranch,
            this.colAddress,
            this.colBSRCode,
            this.colIFSCCode,
            this.colMICRCode,
            this.colContactNumber,
            this.colAccountName,
            this.colSWIFTCode});
            this.gvBankView.GridControl = this.gcBankView;
            this.gvBankView.Name = "gvBankView";
            this.gvBankView.OptionsBehavior.Editable = false;
            this.gvBankView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvBankView.OptionsView.ShowGroupPanel = false;
            this.gvBankView.DoubleClick += new System.EventHandler(this.gvBankView_DoubleClick);
            this.gvBankView.RowCountChanged += new System.EventHandler(this.gvBankView_RowCountChanged);
            // 
            // colBankID
            // 
            this.colBankID.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBankID.AppearanceHeader.Font")));
            this.colBankID.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBankID, "colBankID");
            this.colBankID.FieldName = "BANK_ID";
            this.colBankID.Name = "colBankID";
            // 
            // colAbbrevation
            // 
            this.colAbbrevation.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAbbrevation.AppearanceHeader.Font")));
            this.colAbbrevation.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAbbrevation, "colAbbrevation");
            this.colAbbrevation.FieldName = "BANK_CODE";
            this.colAbbrevation.Name = "colAbbrevation";
            this.colAbbrevation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBankName
            // 
            this.colBankName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBankName.AppearanceHeader.Font")));
            this.colBankName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBankName, "colBankName");
            this.colBankName.FieldName = "BANK";
            this.colBankName.Name = "colBankName";
            this.colBankName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colBranch
            // 
            this.colBranch.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colBranch.AppearanceHeader.Font")));
            this.colBranch.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBranch, "colBranch");
            this.colBranch.FieldName = "BRANCH";
            this.colBranch.Name = "colBranch";
            this.colBranch.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAddress.AppearanceHeader.Font")));
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAddress, "colAddress");
            this.colAddress.FieldName = "ADDRESS";
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colIFSCCode
            // 
            this.colIFSCCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colIFSCCode.AppearanceHeader.Font")));
            this.colIFSCCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colIFSCCode, "colIFSCCode");
            this.colIFSCCode.FieldName = "IFSCCODE";
            this.colIFSCCode.Name = "colIFSCCode";
            this.colIFSCCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colMICRCode
            // 
            this.colMICRCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colMICRCode.AppearanceHeader.Font")));
            this.colMICRCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colMICRCode, "colMICRCode");
            this.colMICRCode.FieldName = "MICRCODE";
            this.colMICRCode.Name = "colMICRCode";
            this.colMICRCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colContactNumber
            // 
            this.colContactNumber.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colContactNumber.AppearanceHeader.Font")));
            this.colContactNumber.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colContactNumber, "colContactNumber");
            this.colContactNumber.FieldName = "CONTACTNUMBER";
            this.colContactNumber.Name = "colContactNumber";
            this.colContactNumber.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAccountName
            // 
            this.colAccountName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAccountName.AppearanceHeader.Font")));
            this.colAccountName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAccountName, "colAccountName");
            this.colAccountName.FieldName = "ACCOUNTNAME";
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colSWIFTCode
            // 
            this.colSWIFTCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSWIFTCode.AppearanceHeader.Font")));
            this.colSWIFTCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSWIFTCode, "colSWIFTCode");
            this.colSWIFTCode.FieldName = "SWIFTCODE";
            this.colSWIFTCode.Name = "colSWIFTCode";
            this.colSWIFTCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlBankFooter
            // 
            this.pnlBankFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBankFooter.Controls.Add(this.lblBankRecordCount);
            this.pnlBankFooter.Controls.Add(this.lblRecordCount);
            this.pnlBankFooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlBankFooter, "pnlBankFooter");
            this.pnlBankFooter.Name = "pnlBankFooter";
            // 
            // lblBankRecordCount
            // 
            resources.ApplyResources(this.lblBankRecordCount, "lblBankRecordCount");
            this.lblBankRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblBankRecordCount.Appearance.Font")));
            this.lblBankRecordCount.Name = "lblBankRecordCount";
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
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // applicationSchema1
            // 
            this.applicationSchema1.DataSetName = "ApplicationSchema";
            this.applicationSchema1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colBSRCode
            // 
            this.colBSRCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gridColumn1.AppearanceHeader.Font")));
            this.colBSRCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colBSRCode, "colBSRCode");
            this.colBSRCode.FieldName = "BSRCODE";
            this.colBSRCode.Name = "colBSRCode";
            this.colBSRCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // frmBankView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBankView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBankView";
            this.ShowFilterClicked += new System.EventHandler(this.frmBankView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmBankView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmBankView_Activated);
            this.Load += new System.EventHandler(this.frmBankView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankToolBar)).EndInit();
            this.pnlBankToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankView)).EndInit();
            this.pnlBankView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridControl)).EndInit();
            this.pnlGridControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBankView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBankView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBankFooter)).EndInit();
            this.pnlBankFooter.ResumeLayout(false);
            this.pnlBankFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationSchema1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBankToolBar;
        private Bosco.Utility.Controls.ucToolBar ucToolBarBankView;
        private DevExpress.XtraEditors.PanelControl pnlBankView;
        private DevExpress.XtraGrid.GridControl gcBankView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBankView;
        private DevExpress.XtraGrid.Columns.GridColumn colBankID;
      //  private DevExpress.XtraGrid.Columns.GridColumn colBudgetId;
        private DevExpress.XtraGrid.Columns.GridColumn colAbbrevation;
        private DevExpress.XtraGrid.Columns.GridColumn colBankName;
        private DevExpress.XtraGrid.Columns.GridColumn colBranch;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private Bosco.DAO.Schema.ApplicationSchema applicationSchema1;
        private DevExpress.XtraEditors.PanelControl pnlBankFooter;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.LabelControl lblBankRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colIFSCCode;
        private DevExpress.XtraGrid.Columns.GridColumn colMICRCode;
        private DevExpress.XtraGrid.Columns.GridColumn colContactNumber;
        private DevExpress.XtraEditors.PanelControl pnlGridControl;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colSWIFTCode;
        private DevExpress.XtraGrid.Columns.GridColumn colBSRCode;

    }
}