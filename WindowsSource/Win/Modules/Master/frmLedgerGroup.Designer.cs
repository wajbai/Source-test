namespace ACPP.Modules.Master
{
    partial class frmLedgerGroup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLedgerGroup));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.spiltContainer = new DevExpress.XtraEditors.SplitContainerControl();
            this.trlLedgerGroup = new DevExpress.XtraTreeList.TreeList();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlLedgerGrpFilter = new System.Windows.Forms.Panel();
            this.chkLedgerGroupFilter = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcLedgerList = new DevExpress.XtraGrid.GridControl();
            this.gvLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCountSymbol = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.lblSelectedGroup = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ucToolBar1 = new Bosco.Utility.Controls.ucToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.spiltContainer)).BeginInit();
            this.spiltContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            this.pnlLedgerGrpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerGroupFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // spiltContainer
            // 
            resources.ApplyResources(this.spiltContainer, "spiltContainer");
            this.spiltContainer.Name = "spiltContainer";
            this.spiltContainer.Panel1.Controls.Add(this.trlLedgerGroup);
            this.spiltContainer.Panel1.Controls.Add(this.pnlLedgerGrpFilter);
            resources.ApplyResources(this.spiltContainer.Panel1, "spiltContainer.Panel1");
            this.spiltContainer.Panel2.Controls.Add(this.panelControl1);
            resources.ApplyResources(this.spiltContainer.Panel2, "spiltContainer.Panel2");
            this.spiltContainer.SplitterPosition = 424;
            // 
            // trlLedgerGroup
            // 
            this.trlLedgerGroup.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("trlLedgerGroup.Appearance.FocusedCell.Font")));
            this.trlLedgerGroup.Appearance.FocusedCell.Options.UseFont = true;
            this.trlLedgerGroup.Appearance.FocusedRow.BackColor = ((System.Drawing.Color)(resources.GetObject("trlLedgerGroup.Appearance.FocusedRow.BackColor")));
            this.trlLedgerGroup.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("trlLedgerGroup.Appearance.FocusedRow.Font")));
            this.trlLedgerGroup.Appearance.FocusedRow.Options.UseBackColor = true;
            this.trlLedgerGroup.Appearance.FocusedRow.Options.UseFont = true;
            this.trlLedgerGroup.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("trlLedgerGroup.Appearance.HeaderPanel.Font")));
            this.trlLedgerGroup.Appearance.HeaderPanel.Options.UseFont = true;
            this.trlLedgerGroup.Appearance.Row.ForeColor = ((System.Drawing.Color)(resources.GetObject("trlLedgerGroup.Appearance.Row.ForeColor")));
            this.trlLedgerGroup.Appearance.Row.Options.UseFont = true;
            this.trlLedgerGroup.Appearance.Row.Options.UseForeColor = true;
            this.trlLedgerGroup.Appearance.SelectedRow.BackColor = ((System.Drawing.Color)(resources.GetObject("trlLedgerGroup.Appearance.SelectedRow.BackColor")));
            this.trlLedgerGroup.Appearance.SelectedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("trlLedgerGroup.Appearance.SelectedRow.BackColor2")));
            this.trlLedgerGroup.Appearance.SelectedRow.BorderColor = ((System.Drawing.Color)(resources.GetObject("trlLedgerGroup.Appearance.SelectedRow.BorderColor")));
            this.trlLedgerGroup.Appearance.SelectedRow.Font = ((System.Drawing.Font)(resources.GetObject("trlLedgerGroup.Appearance.SelectedRow.Font")));
            this.trlLedgerGroup.Appearance.SelectedRow.Options.UseBackColor = true;
            this.trlLedgerGroup.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.trlLedgerGroup.Appearance.SelectedRow.Options.UseFont = true;
            resources.ApplyResources(this.trlLedgerGroup, "trlLedgerGroup");
            this.trlLedgerGroup.FixedLineWidth = 5;
            this.trlLedgerGroup.ImageIndexFieldName = "IMAGE_ID";
            this.trlLedgerGroup.KeyFieldName = "GROUP_ID";
            this.trlLedgerGroup.Name = "trlLedgerGroup";
            this.trlLedgerGroup.OptionsBehavior.Editable = false;
            this.trlLedgerGroup.OptionsBehavior.EnableFiltering = true;
            this.trlLedgerGroup.OptionsView.ShowHorzLines = false;
            this.trlLedgerGroup.OptionsView.ShowVertLines = false;
            this.trlLedgerGroup.ParentFieldName = "PARENT_GROUP_ID";
            this.trlLedgerGroup.PreviewFieldName = "Ledger Group";
            this.trlLedgerGroup.SelectImageList = this.imageSmall;
            this.trlLedgerGroup.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.trlLedgerGroup_FocusedNodeChanged);
            this.trlLedgerGroup.FilterNode += new DevExpress.XtraTreeList.FilterNodeEventHandler(this.OnFilterNode);
            this.trlLedgerGroup.DoubleClick += new System.EventHandler(this.trlLedgerGroup_DoubleClick);
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            // 
            // pnlLedgerGrpFilter
            // 
            this.pnlLedgerGrpFilter.Controls.Add(this.chkLedgerGroupFilter);
            resources.ApplyResources(this.pnlLedgerGrpFilter, "pnlLedgerGrpFilter");
            this.pnlLedgerGrpFilter.Name = "pnlLedgerGrpFilter";
            // 
            // chkLedgerGroupFilter
            // 
            resources.ApplyResources(this.chkLedgerGroupFilter, "chkLedgerGroupFilter");
            this.chkLedgerGroupFilter.Name = "chkLedgerGroupFilter";
            this.chkLedgerGroupFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkLedgerGroupFilter.Properties.Caption = resources.GetString("chkLedgerGroupFilter.Properties.Caption");
            this.chkLedgerGroupFilter.CheckedChanged += new System.EventHandler(this.chkLedgerGroupFilter_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcLedgerList);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl4);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // gcLedgerList
            // 
            resources.ApplyResources(this.gcLedgerList, "gcLedgerList");
            this.gcLedgerList.MainView = this.gvLedger;
            this.gcLedgerList.Name = "gcLedgerList";
            this.gcLedgerList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedger});
            // 
            // gvLedger
            // 
            this.gvLedger.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLedger.Appearance.FocusedRow.Font")));
            this.gvLedger.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLedger.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLedger.Appearance.HeaderPanel.Font")));
            this.gvLedger.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLedger.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerCode,
            this.colLedgerName});
            this.gvLedger.GridControl = this.gcLedgerList;
            this.gvLedger.Name = "gvLedger";
            this.gvLedger.OptionsBehavior.Editable = false;
            this.gvLedger.OptionsCustomization.AllowColumnMoving = false;
            this.gvLedger.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLedger.OptionsView.ShowGroupPanel = false;
            this.gvLedger.RowCountChanged += new System.EventHandler(this.gvLedger_RowCountChanged);
            // 
            // colLedgerCode
            // 
            resources.ApplyResources(this.colLedgerCode, "colLedgerCode");
            this.colLedgerCode.FieldName = "LEDGER_CODE";
            this.colLedgerCode.Name = "colLedgerCode";
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("panelControl3.Appearance.BackColor")));
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.lblRecordCount);
            this.panelControl3.Controls.Add(this.lblRecordCountSymbol);
            this.panelControl3.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.panelControl3, "panelControl3");
            this.panelControl3.Name = "panelControl3";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblRecordCountSymbol
            // 
            resources.ApplyResources(this.lblRecordCountSymbol, "lblRecordCountSymbol");
            this.lblRecordCountSymbol.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCountSymbol.Appearance.Font")));
            this.lblRecordCountSymbol.Name = "lblRecordCountSymbol";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.lblSelectedGroup);
            resources.ApplyResources(this.panelControl4, "panelControl4");
            this.panelControl4.Name = "panelControl4";
            // 
            // lblSelectedGroup
            // 
            this.lblSelectedGroup.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblSelectedGroup.Appearance.Font")));
            resources.ApplyResources(this.lblSelectedGroup, "lblSelectedGroup");
            this.lblSelectedGroup.Name = "lblSelectedGroup";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.ucToolBar1);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
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
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBar1.ChangePostInterestSuperToolTip = superToolTip2;
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
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.AddClicked += new System.EventHandler(this.ucToolBar1_AddClicked_1);
            this.ucToolBar1.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked_1);
            this.ucToolBar1.DeleteClicked += new System.EventHandler(this.ucToolBar1_DeleteClicked_1);
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked_1);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
            // 
            // frmLedgerGroup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spiltContainer);
            this.Controls.Add(this.panelControl2);
            this.KeyPreview = true;
            this.Name = "frmLedgerGroup";
            this.ShowFilterClicked += new System.EventHandler(this.frmLedgerGroup_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmLedgerGroup_EnterClicked);
            this.Load += new System.EventHandler(this.frmLedgerGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spiltContainer)).EndInit();
            this.spiltContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            this.pnlLedgerGrpFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerGroupFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLedgerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Bosco.Utility.Controls.ucToolBar ucToolBar1;
        private DevExpress.XtraEditors.SplitContainerControl spiltContainer;
        private DevExpress.Utils.ImageCollection imageSmall;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcLedgerList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCountSymbol;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LabelControl lblSelectedGroup;
        private DevExpress.XtraTreeList.TreeList trlLedgerGroup;
        private System.Windows.Forms.Panel pnlLedgerGrpFilter;
        private DevExpress.XtraEditors.CheckEdit chkLedgerGroupFilter;

    }
}