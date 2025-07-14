namespace ACPP.Modules.Master
{
    partial class frmStateView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStateView));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.pnltop = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBar1 = new Bosco.Utility.Controls.ucToolBar();
            this.pnlContent = new DevExpress.XtraEditors.PanelControl();
            this.gcState = new DevExpress.XtraGrid.GridControl();
            this.gvState = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStateId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCountResult = new DevExpress.XtraEditors.LabelControl();
            this.lblCountryRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.colStateCode = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).BeginInit();
            this.pnltop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContent)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).BeginInit();
            this.pnlfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnltop
            // 
            this.pnltop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnltop.Controls.Add(this.ucToolBar1);
            resources.ApplyResources(this.pnltop, "pnltop");
            this.pnltop.Name = "pnltop";
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "&Add";
            this.ucToolBar1.ChangeCaption = "&Edit";
            this.ucToolBar1.ChangeDeleteCaption = "&Delete";
            this.ucToolBar1.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBar1.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBar1.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBar1.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBar1.ChangePrintCaption = "&Print";
            this.ucToolBar1.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBar1.DisableAddButton = true;
            this.ucToolBar1.DisableAMCRenew = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = true;
            this.ucToolBar1.DisableDownloadExcel = true;
            this.ucToolBar1.DisableEditButton = true;
            this.ucToolBar1.DisableInsertVoucher = true;
            this.ucToolBar1.DisableMoveTransaction = true;
            this.ucToolBar1.DisableNatureofPayments = true;
            this.ucToolBar1.DisablePostInterest = true;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBar1, "ucToolBar1");
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.ShowHTML = true;
            this.ucToolBar1.ShowMMT = true;
            this.ucToolBar1.ShowPDF = true;
            this.ucToolBar1.ShowRTF = true;
            this.ucToolBar1.ShowText = true;
            this.ucToolBar1.ShowXLS = true;
            this.ucToolBar1.ShowXLSX = true;
            this.ucToolBar1.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.AddClicked += new System.EventHandler(this.ucToolBar1_AddClicked_1);
            this.ucToolBar1.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked_1);
            this.ucToolBar1.DeleteClicked += new System.EventHandler(this.ucToolBar1_DeleteClicked_1);
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked_1);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlContent.Controls.Add(this.gcState);
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.Name = "pnlContent";
            // 
            // gcState
            // 
            resources.ApplyResources(this.gcState, "gcState");
            this.gcState.MainView = this.gvState;
            this.gcState.Name = "gcState";
            this.gcState.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvState});
            // 
            // gvState
            // 
            this.gvState.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvState.Appearance.HeaderPanel.Font")));
            this.gvState.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvState.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStateId,
            this.colcountry,
            this.colStateCode,
            this.colState});
            this.gvState.GridControl = this.gcState;
            this.gvState.Name = "gvState";
            this.gvState.OptionsBehavior.Editable = false;
            this.gvState.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvState.OptionsView.ShowGroupPanel = false;
            this.gvState.DoubleClick += new System.EventHandler(this.gvState_DoubleClick);
            this.gvState.RowCountChanged += new System.EventHandler(this.gvState_RowCountChanged);
            // 
            // colStateId
            // 
            resources.ApplyResources(this.colStateId, "colStateId");
            this.colStateId.FieldName = "STATE_ID";
            this.colStateId.Name = "colStateId";
            // 
            // colcountry
            // 
            resources.ApplyResources(this.colcountry, "colcountry");
            this.colcountry.FieldName = "COUNTRY";
            this.colcountry.Name = "colcountry";
            this.colcountry.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colState
            // 
            resources.ApplyResources(this.colState, "colState");
            this.colState.FieldName = "STATE_NAME";
            this.colState.Name = "colState";
            this.colState.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // pnlfooter
            // 
            this.pnlfooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfooter.Controls.Add(this.lblRecordCountResult);
            this.pnlfooter.Controls.Add(this.lblCountryRecordCount);
            this.pnlfooter.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.pnlfooter, "pnlfooter");
            this.pnlfooter.Name = "pnlfooter";
            // 
            // lblRecordCountResult
            // 
            resources.ApplyResources(this.lblRecordCountResult, "lblRecordCountResult");
            this.lblRecordCountResult.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCountResult.Appearance.Font")));
            this.lblRecordCountResult.Name = "lblRecordCountResult";
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
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // colStateCode
            // 
            resources.ApplyResources(this.colStateCode, "colStateCode");
            this.colStateCode.FieldName = "STATE_CODE";
            this.colStateCode.Name = "colStateCode";
            this.colStateCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // frmStateView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlfooter);
            this.Controls.Add(this.pnltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStateView";
            this.Activated += new System.EventHandler(this.frmStateView_Activated);
            this.Load += new System.EventHandler(this.frmStateView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnltop)).EndInit();
            this.pnltop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContent)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnltop;
        private DevExpress.XtraEditors.PanelControl pnlContent;
        private DevExpress.XtraEditors.PanelControl pnlfooter;
        private Bosco.Utility.Controls.ucToolBar ucToolBar1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.GridControl gcState;
        private DevExpress.XtraGrid.Views.Grid.GridView gvState;
        private DevExpress.XtraGrid.Columns.GridColumn colStateId;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colcountry;
        private DevExpress.XtraEditors.LabelControl lblCountryRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCountResult;
        private DevExpress.XtraGrid.Columns.GridColumn colStateCode;

    }
}