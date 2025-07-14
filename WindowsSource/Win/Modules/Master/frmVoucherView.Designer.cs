namespace ACPP.Modules.Master
{
    partial class frmVoucherView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVoucherView));
            this.ucToolBarVoucher = new Bosco.Utility.Controls.ucToolBar();
            this.gcVoucherDetails = new DevExpress.XtraGrid.GridControl();
            this.gvVoucherDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvColVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColVoucherName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColVoucherType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColVoucherMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColPreficCharacter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColSuffixCharacter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCountResult = new DevExpress.XtraEditors.LabelControl();
            this.lblVoucherRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcVoucherDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucherDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucToolBarVoucher
            // 
            this.ucToolBarVoucher.ChangeAddCaption = "&Add";
            this.ucToolBarVoucher.ChangeCaption = "&Edit";
            this.ucToolBarVoucher.ChangeDeleteCaption = "&Delete";
            this.ucToolBarVoucher.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarVoucher.ChangePrintCaption = "&Print";
            this.ucToolBarVoucher.DisableAddButton = true;
            this.ucToolBarVoucher.DisableCloseButton = true;
            this.ucToolBarVoucher.DisableDeleteButton = true;
            this.ucToolBarVoucher.DisableDownloadExcel = true;
            this.ucToolBarVoucher.DisableEditButton = true;
            this.ucToolBarVoucher.DisableMoveTransaction = true;
            this.ucToolBarVoucher.DisableNatureofPayments = true;
            this.ucToolBarVoucher.DisablePrintButton = true;
            this.ucToolBarVoucher.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarVoucher, "ucToolBarVoucher");
            this.ucToolBarVoucher.Name = "ucToolBarVoucher";
            this.ucToolBarVoucher.ShowHTML = true;
            this.ucToolBarVoucher.ShowMMT = true;
            this.ucToolBarVoucher.ShowPDF = true;
            this.ucToolBarVoucher.ShowRTF = true;
            this.ucToolBarVoucher.ShowText = true;
            this.ucToolBarVoucher.ShowXLS = true;
            this.ucToolBarVoucher.ShowXLSX = true;
            this.ucToolBarVoucher.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarVoucher.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarVoucher.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarVoucher.AddClicked += new System.EventHandler(this.ucToolBarVoucher_AddClicked);
            this.ucToolBarVoucher.EditClicked += new System.EventHandler(this.ucToolBarVoucher_EditClicked);
            this.ucToolBarVoucher.DeleteClicked += new System.EventHandler(this.ucToolBarVoucher_DeleteClicked);
            this.ucToolBarVoucher.PrintClicked += new System.EventHandler(this.ucToolBarVoucher_PrintClicked);
            this.ucToolBarVoucher.CloseClicked += new System.EventHandler(this.ucToolBarVoucher_CloseClicked);
            this.ucToolBarVoucher.RefreshClicked += new System.EventHandler(this.ucToolBarVoucher_RefreshClicked);
            // 
            // gcVoucherDetails
            // 
            resources.ApplyResources(this.gcVoucherDetails, "gcVoucherDetails");
            this.gcVoucherDetails.MainView = this.gvVoucherDetails;
            this.gcVoucherDetails.Name = "gcVoucherDetails";
            this.gcVoucherDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVoucherDetails});
            this.gcVoucherDetails.DoubleClick += new System.EventHandler(this.gcVoucherDetails_DoubleClick);
            // 
            // gvVoucherDetails
            // 
            this.gvVoucherDetails.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucherDetails.Appearance.FocusedRow.Font")));
            this.gvVoucherDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvVoucherDetails.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucherDetails.Appearance.HeaderPanel.Font")));
            this.gvVoucherDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvVoucherDetails.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvVoucherDetails.AppearancePrint.HeaderPanel.Font")));
            this.gvVoucherDetails.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvVoucherDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvVoucherDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvColVoucherId,
            this.gvColVoucherName,
            this.gvColVoucherType,
            this.gvColVoucherMethod,
            this.gvColPreficCharacter,
            this.gvColSuffixCharacter,
            this.gvColMonth});
            this.gvVoucherDetails.GridControl = this.gcVoucherDetails;
            this.gvVoucherDetails.Name = "gvVoucherDetails";
            this.gvVoucherDetails.OptionsBehavior.Editable = false;
            this.gvVoucherDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvVoucherDetails.OptionsView.ShowGroupPanel = false;
            this.gvVoucherDetails.RowCountChanged += new System.EventHandler(this.gvVoucherDetails_RowCountChanged);
            // 
            // gvColVoucherId
            // 
            resources.ApplyResources(this.gvColVoucherId, "gvColVoucherId");
            this.gvColVoucherId.FieldName = "VOUCHER_ID";
            this.gvColVoucherId.Name = "gvColVoucherId";
            // 
            // gvColVoucherName
            // 
            this.gvColVoucherName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColVoucherName.AppearanceHeader.Font")));
            this.gvColVoucherName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColVoucherName, "gvColVoucherName");
            this.gvColVoucherName.FieldName = "VOUCHER_NAME";
            this.gvColVoucherName.Name = "gvColVoucherName";
            this.gvColVoucherName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColVoucherType
            // 
            this.gvColVoucherType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColVoucherType.AppearanceHeader.Font")));
            this.gvColVoucherType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColVoucherType, "gvColVoucherType");
            this.gvColVoucherType.FieldName = "VOUCHER_TYPE";
            this.gvColVoucherType.Name = "gvColVoucherType";
            this.gvColVoucherType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColVoucherMethod
            // 
            this.gvColVoucherMethod.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColVoucherMethod.AppearanceHeader.Font")));
            this.gvColVoucherMethod.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColVoucherMethod, "gvColVoucherMethod");
            this.gvColVoucherMethod.FieldName = "VOUCHER_METHOD";
            this.gvColVoucherMethod.Name = "gvColVoucherMethod";
            this.gvColVoucherMethod.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColPreficCharacter
            // 
            this.gvColPreficCharacter.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColPreficCharacter.AppearanceHeader.Font")));
            this.gvColPreficCharacter.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColPreficCharacter, "gvColPreficCharacter");
            this.gvColPreficCharacter.FieldName = "PREFIX_CHAR";
            this.gvColPreficCharacter.Name = "gvColPreficCharacter";
            this.gvColPreficCharacter.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColSuffixCharacter
            // 
            this.gvColSuffixCharacter.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColSuffixCharacter.AppearanceHeader.Font")));
            this.gvColSuffixCharacter.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColSuffixCharacter, "gvColSuffixCharacter");
            this.gvColSuffixCharacter.FieldName = "SUFFIX_CHAR";
            this.gvColSuffixCharacter.Name = "gvColSuffixCharacter";
            this.gvColSuffixCharacter.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColMonth
            // 
            this.gvColMonth.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gvColMonth.AppearanceHeader.Font")));
            this.gvColMonth.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gvColMonth, "gvColMonth");
            this.gvColMonth.FieldName = "MONTH";
            this.gvColMonth.Name = "gvColMonth";
            this.gvColMonth.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("panelControl1.Appearance.BackColor")));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblRecordCountResult);
            this.panelControl1.Controls.Add(this.lblVoucherRecordCount);
            this.panelControl1.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // lblRecordCountResult
            // 
            resources.ApplyResources(this.lblRecordCountResult, "lblRecordCountResult");
            this.lblRecordCountResult.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCountResult.Appearance.Font")));
            this.lblRecordCountResult.Name = "lblRecordCountResult";
            // 
            // lblVoucherRecordCount
            // 
            resources.ApplyResources(this.lblVoucherRecordCount, "lblVoucherRecordCount");
            this.lblVoucherRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblVoucherRecordCount.Appearance.Font")));
            this.lblVoucherRecordCount.Name = "lblVoucherRecordCount";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gcVoucherDetails);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // frmVoucherView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ucToolBarVoucher);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVoucherView";
            this.ShowFilterClicked += new System.EventHandler(this.frmVoucherView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmVoucherView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmVoucherView_Activated);
            this.Load += new System.EventHandler(this.frmVoucherView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcVoucherDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucherDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bosco.Utility.Controls.ucToolBar ucToolBarVoucher;
        private DevExpress.XtraGrid.GridControl gcVoucherDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVoucherDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gvColVoucherName;
        private DevExpress.XtraGrid.Columns.GridColumn gvColVoucherType;
        private DevExpress.XtraGrid.Columns.GridColumn gvColVoucherMethod;
        private DevExpress.XtraGrid.Columns.GridColumn gvColMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gvColPreficCharacter;
        private DevExpress.XtraGrid.Columns.GridColumn gvColSuffixCharacter;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.Columns.GridColumn gvColVoucherId;
        private DevExpress.XtraEditors.LabelControl lblRecordCountResult;
        private DevExpress.XtraEditors.LabelControl lblVoucherRecordCount;
    }
}